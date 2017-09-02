using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace EasyChat_Server
{
    class Listener
    {

        #region 字段定义

        /// <summary>
        /// 服务器程序使用的端口，默认为8888
        /// 默认ip是本地环回网
        /// </summary>
        private IPAddress _ip = IPAddress.Parse("127.0.0.1");        
        private int _port = 8888;
       // private IPAddress _ip = IPAddress.Parse("127.0.0.1");

        /// <summary>
        /// 接收数据缓冲区大小64K 
        /// </summary>
        private const int _maxPacket = 64 * 1024; //缓冲区最大

        /// <summary>
        /// 服务器端的监听器
        /// </summary>
        private TcpListener _tcpl = null;

        /// <summary>
        /// 保存所有客户端会话的哈希表
        /// </summary>
        private Hashtable _transmit_tb = new Hashtable();

        #endregion
        

        #region 服务器方法

        /// <summary>
        /// 关闭监听器并释放资源
        /// </summary>
        public void Close()
        {
            if (_tcpl != null)
            {
                _tcpl.Stop();
            }
            //关闭客户端连接并清理资源
            if (_transmit_tb.Count != 0)
            {
                foreach (Socket session in _transmit_tb.Values)
                {
                    session.Shutdown(SocketShutdown.Both);
                }
                _transmit_tb.Clear();
                _transmit_tb = null;
            }
        }

        /// <summary>
        /// 配置监听端口号
        /// </summary>
        public void GetConfig()
        {
            string portParam;
            Console.Write("请输入IP地址直接回车则接受默认IP 127.0.0.1 ");
            portParam = Console.ReadLine();
            if (portParam != string.Empty)
            {
                if (!IPAddress.TryParse(portParam, out _ip))
                {
                    _ip = IPAddress.Parse("127.0.0.1");
                    Console.WriteLine("IP不合法,默认IP127.0.0.1被接受!");
                }
            }
            Console.Write("请输入所要监听的端口号，直接回车则接受默认端口号：8888 ");
            portParam = Console.ReadLine();
            if (portParam != string.Empty)
            {
                if (!int.TryParse(portParam, out _port) || _port < 1023 || _port > 65535)
                {
                    _port = 8888;
                    Console.WriteLine("端口号不合法,默认端口号被接受!");
                }
            }
        }

         //<summary>
        /// 序列化在线列表，向客户端返回序列化后的字节数组
        /// </summary>
        /// <returns>序列化后的字节数组</returns>
        private byte[] SerializeOnlineList()
        {
            StringCollection onlineList = new StringCollection();
            foreach (object o in _transmit_tb.Keys)
            {
                onlineList.Add(o as string);
            }
            IFormatter format = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            format.Serialize(stream, onlineList);
            byte[] ret = stream.ToArray();
            stream.Close();
            return ret;
        }

        /// <summary>
        /// 提取命令
        /// 格式为两个一位整数拼接成的字符串。
        /// 第一位为0表示客户机向服务器发送的命令，为1表示服务器向客户机发送的命令。
        /// 第二位表示命令的含义，具体如下：
        /// "01"-离线
        /// "02"-请求在线列表
        /// "03"-请求对所有人闪屏振动
        /// "04"-请求对指定用户闪屏振动
        /// "05"-请求广播消息
        /// default-转发给指定用户
        /// </summary>
        /// <param name="s">要解析的包含命令的byte数组，只提取前两个字节</param>
        /// <returns>拼接成的命令</returns>
        private string DecodingBytes(byte[] s)
        {
            return string.Concat(s[0].ToString(), s[1].ToString());
        }

        /// <summary>
        /// 线程执行体,转发消息
        /// </summary>
        /// <param name="obj">传递给线程执行体的用户名，用以与用户通信</param>
        private void ThreadFunc(object obj)
        {
            //通过转发表得到当前用户套接字
            Socket clientSkt = _transmit_tb[obj] as Socket;
            while (true)
            {
                try
                {
                    //接受第一个数据包。
                    //仅有两种情况，一为可以识别的命令格式，否则为要求转发给指定用户的用户名。
                    //这里的实现不够优雅，但不失为此简单模型的一个解决之道。
                    byte[] _cmdBuff = new byte[128];
                    clientSkt.Receive(_cmdBuff);
                    string _cmd = DecodingBytes(_cmdBuff);
                    /// "01"-离线
                    /// "02"-请求在线列表
                    /// "03"-请求对所有人闪屏振动
                    /// "04"-请求对指定用户闪屏振动
                    /// "05"-请求广播消息
                    /// default-转发给指定用户
                    switch (_cmd)
                    {
                        case "01":
                            {
                                _transmit_tb.Remove(obj);
                                string svrlog = string.Format("[系统消息]用户 {0} 在 {1} 已断开... 当前在线人数: {2}\r\n\r\n", obj, DateTime.Now, _transmit_tb.Count);
                                Console.WriteLine(svrlog);
                                //向所有客户机发送系统消息
                                foreach (DictionaryEntry de in _transmit_tb)
                                {
                                    string _clientName = de.Key as string;
                                    Socket _clientSkt = de.Value as Socket;
                                    _clientSkt.Send(Encoding.Unicode.GetBytes(svrlog));
                                }
                                Thread.CurrentThread.Abort();
                                break;
                            }
                        case "02":
                            {
                                byte[] onlineBuff = SerializeOnlineList();
                                //先发送响应信号，用于客户机的判断，"11"表示服务发给客户机的更新在线列表的命令
                                clientSkt.Send(new byte[] { 1, 1 });
                                clientSkt.Send(onlineBuff);
                                break;
                            }
                        case "03":
                            {
                                string displayTxt = string.Format("[系统提示]用户 {0} 向您发送了一个闪屏振动。\r\n\r\n", obj);
                                foreach (DictionaryEntry de in _transmit_tb)
                                {
                                    string _clientName = de.Key as string;
                                    Socket _clientSkt = de.Value as Socket;
                                    if (!_clientName.Equals(obj))
                                    {
                                        //先发送响应信号，用于客户机的判断，"12"表示服务发给客户机的闪屏振动的命令
                                        _clientSkt.Send(new byte[] { 1, 2 });
                                        _clientSkt.Send(Encoding.Unicode.GetBytes(displayTxt));
                                    }
                                }
                                break;
                            }
                        case "04":
                            {
                                string _receiver = null;
                                byte[] _receiverBuff = new byte[128];
                                clientSkt.Receive(_receiverBuff);
                                _receiver = Encoding.Unicode.GetString(_receiverBuff).TrimEnd('\0');
                                string displayTxt = string.Format("[系统提示]用户 {0} 向您发送了一个闪屏振动。\r\n\r\n", obj);
                                //通过转发表查找接收方的套接字
                                if (_transmit_tb.ContainsKey(_receiver))
                                {
                                    Socket receiverSkt = _transmit_tb[_receiver] as Socket;
                                    receiverSkt.Send(new byte[] { 1, 2 });
                                    receiverSkt.Send(Encoding.Unicode.GetBytes(displayTxt));
                                }
                                else
                                {
                                    string sysMessage = string.Format("[系统消息]您刚才的闪屏振动没有发送成功。\r\n可能原因：用户 {0} 已离线或者网络阻塞。\r\n\r\n", _receiver);
                                    clientSkt.Send(Encoding.Unicode.GetBytes(sysMessage));
                                }
                                break;
                            }
                        case "05":
                            {
                                byte[] _msgBuff = new byte[_maxPacket];
                                clientSkt.Receive(_msgBuff);
                                foreach (DictionaryEntry de in _transmit_tb)
                                {
                                    string _clientName = de.Key as string;
                                    Socket _clientSkt = de.Value as Socket;
                                    if (!_clientName.Equals(obj))
                                    {
                                        _clientSkt.Send(_msgBuff);
                                    }
                                }
                                break;
                            }
                        default:
                            {
                                //以上都不是则表明第一个包是接收方用户名，继续接受第二个消息正文数据包
                                string _receiver = Encoding.Unicode.GetString(_cmdBuff).TrimEnd('\0');
                                byte[] _packetBuff = new byte[_maxPacket];
                                clientSkt.Receive(_packetBuff);
                                if (_transmit_tb.ContainsKey(_receiver))
                                {
                                    //通过转发表查找接收方的套接字
                                    Socket receiverSkt = _transmit_tb[_receiver] as Socket;
                                    receiverSkt.Send(_packetBuff);
                                }
                                else
                                {
                                    string sysMessage = string.Format("[系统消息]您刚才的内容没有发送成功。\r\n可能原因：用户 {0} 已离线或者网络阻塞。\r\n\r\n", _receiver);
                                    clientSkt.Send(Encoding.Unicode.GetBytes(sysMessage));
                                }
                                break;
                            }
                    }
                }
                catch (SocketException)
                {
                    _transmit_tb.Remove(obj);
                    string svrlog = string.Format("[系统消息]用户 {0} 的客户端在 {1} 意外终止！当前在线人数：{2}\r\n\r\n", obj, DateTime.Now, _transmit_tb.Count);
                    Console.WriteLine(svrlog);
                    //向所有客户机发送系统消息
                    foreach (DictionaryEntry de in _transmit_tb)
                    {
                        string _clientName = de.Key as string;
                        Socket _clientSkt = de.Value as Socket;
                        _clientSkt.Send(Encoding.Unicode.GetBytes(svrlog));
                    }
                    Console.WriteLine();
                    Thread.CurrentThread.Abort();
                }
            }
        }

        /// <summary>
        /// 启动监听，轮询监听客户机请求并将客户端套接字存入转发表
        /// </summary>
        public void StartUp()
        {
          //  IPAddress _ip = Dns.GetHostAddresses(Dns.GetHostName())[0];
            _tcpl = new TcpListener(_ip, _port);
            _tcpl.Start();
            Console.WriteLine("服务器已启动，正在监听...\n");
            Console.WriteLine(string.Format("服务器IP：{0}\t端口号：{1}\n", _ip, _port));
            while (true)
            {
                byte[] packetBuff = new byte[_maxPacket];
                Socket newClient = _tcpl.AcceptSocket();
                newClient.Receive(packetBuff);
                string userName = Encoding.Unicode.GetString(packetBuff).TrimEnd('\0');
                //验证是否为唯一用户
                if (_transmit_tb.Count != 0 && _transmit_tb.ContainsKey(userName))
                {
                    newClient.Send(Encoding.Unicode.GetBytes("cmd::Failed"));
                    continue;
                }
                else
                {
                    newClient.Send(Encoding.Unicode.GetBytes("cmd::Successful"));
                }
                //将新连接加入转发表并创建线程为其服务
                _transmit_tb.Add(userName, newClient);
                string svrlog = string.Format("[系统消息]新用户 {0} 在 {1} 已连接... 当前在线人数: {2}\r\n\r\n", userName, DateTime.Now, _transmit_tb.Count);
                Console.WriteLine(svrlog);
                
                Thread clientThread = new Thread(new ParameterizedThreadStart(ThreadFunc));
                clientThread.Start(userName);
                //向所有客户机发送系统消息
                foreach (DictionaryEntry de in _transmit_tb)
                {
                    string _clientName = de.Key as string;
                    Socket _clientSkt = de.Value as Socket;
                    if (!_clientName.Equals(userName))
                    {
                        _clientSkt.Send(Encoding.Unicode.GetBytes(svrlog));
                    }
                }
            }
        }

        #endregion
    }
}