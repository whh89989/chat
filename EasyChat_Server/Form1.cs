using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Specialized;
using System.Threading;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace EasyChat_Server
{

    public partial class Form1 : Form
    {

        #region 字段定义

        /// <summary>
        /// 服务器程序使用的端口，默认为8888
        /// 默认ip是本地环回网
        /// </summary>
        // private int _port = frm.;
        // private IPAddress _ip = IPAddress.Parse("127.0.0.1");

        /// <summary>
        /// 接收数据缓冲区大小64K 
        /// </summary>
        private const int _maxPacket = 64 * 1024;

        /// <summary>
        /// 服务器端的监听器
        /// </summary>
        private TcpListener _tcpl = null;

        /// <summary>
        /// 保存所有客户端会话的哈希表
        /// </summary>
        private Hashtable _transmit_tb = new Hashtable();

        #endregion

        /// <summary>
        /// //////////

        #region 服务器方法

        private int _port = 8888;
        private IPAddress _ipAddr;
        public Form1()
        {
            InitializeComponent();
        }

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
        /// ////////////////////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        private bool ValidateInfo()  //判断IP和端口号是否合法
        {
            if (!IPAddress.TryParse(ip_tb.Text, out _ipAddr))
            {
                MessageBox.Show("IP地址不合法!默认127.0.0.1",
                                "提示",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return false;
            }
            if (!int.TryParse(port_tb.Text, out _port))
            {
                MessageBox.Show("端口号不合法!默认8888",
                                "提示",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return false;
            }
            else
            {
                if (_port < 1024 || _port > 65535)
                {
                    MessageBox.Show("端口号不合法!默认8888",
                                    "提示",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    return false;
                }
            }
            return true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ip_tb.Text = "127.0.0.1";
            port_tb.Text = "8888";
        }

        private void ip_tb_TextChanged(object sender, EventArgs e)
        {

        }

        private void open_Click(object sender, EventArgs e)
        {
            if (!ValidateInfo())//不合法
            {
                _ipAddr = IPAddress.Parse("127.0.0.1");
                _port = 8888;
            }
            _ipAddr = IPAddress.Parse(ip_tb.Text);
            _port = int.Parse(port_tb.Text);


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
                   msg_tb.Text = svrlog;
                    // Console.WriteLine(svrlog);
                    //向所有客户机发送系统消息
                    foreach (DictionaryEntry de in _transmit_tb)
                    {
                        string _clientName = de.Key as string;
                        Socket _clientSkt = de.Value as Socket;
                        _clientSkt.Send(Encoding.Unicode.GetBytes(svrlog));
                    }
                   //msg_tb.Text.;
                    //Console.WriteLine();
                    Thread.CurrentThread.Abort();
                }
            }
        #endregion
    }

        private void msg_tb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
