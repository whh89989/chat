using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace EasyChat
{
    public partial class login_frm : Form
    {
        /// <summary>
        /// IP地址
        /// </summary>
        private IPAddress _ipAddr;

        #region 登录窗体构造函数

        /// <summary>
        /// 构造函数，自动生成
        /// </summary>
        public login_frm()
        {
            InitializeComponent();
        }

        #endregion

        #region 登录窗体的私有方法

        /// <summary>
        /// 验证登录信息
        /// </summary>
        /// <returns>验证结果</returns>
        private bool ValidateInfo()
        {
            if (user_tb.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请填写用户名！",
                                "提示",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return false;
            }

            if (!IPAddress.TryParse(svrip_tb.Text, out _ipAddr))
            {
                MessageBox.Show("IP地址不合法!",
                                "提示",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return false;
            }
            int _port;
            if (!int.TryParse(svrport_tb.Text, out _port))
            {
                MessageBox.Show("端口号不合法!",
                                "提示",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return false;
            }
            else
            {
                if (_port < 1024 || _port > 65535)
                {
                    MessageBox.Show("端口号不合法!",
                                    "提示",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 取消，关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void login_btn_Click(object sender, EventArgs e)
        {
            //验证数据合法性
            if (!ValidateInfo())
            {
                return;
            }
            int port = int.Parse(svrport_tb.Text);
            //向服务器发出连接请求
            TCPConnection conn = new TCPConnection(_ipAddr, port);
            TcpClient _tcpc = conn.Connect();
            if (_tcpc == null)
            {
                MessageBox.Show("无法连接到服务器，请重试！",
                                "错误",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }
            else
            {
                NetworkStream netstream = _tcpc.GetStream();
                //向服务器发送用户名以确认身份
                netstream.Write(Encoding.Unicode.GetBytes(user_tb.Text), 0, Encoding.Unicode.GetBytes(user_tb.Text).Length);
                //得到登录结果
                byte[] buffer = new byte[50];
                netstream.Read(buffer, 0, buffer.Length);
                string connResult = Encoding.Unicode.GetString(buffer).TrimEnd('\0');
                if (connResult.Equals("cmd::Failed"))
                {
                    MessageBox.Show("您的用户名已经被使用，请尝试其他用户名!",
                                    "提示",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    string svrskt = svrip_tb.Text + ":" + svrport_tb.Text;
                    chat_frm chatFrm = new chat_frm(user_tb.Text, netstream, svrskt);
                    chatFrm.Owner = this;
                    this.Hide();
                    chatFrm.Show();
                }
            }
        }

        /// <summary>
        /// 初始化登录信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void login_frm_Load(object sender, EventArgs e)
        {
            svrip_tb.Text = "127.0.0.1";
            svrport_tb.Text = "8888";
            user_tb.Focus();
        }

        #endregion
    }
}