using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace EasyChat_Server
{
    class Program
    {
        static void Main(string[] args)
        {
           // Form1 frm = new Form1();
           // Application.Run(frm);
            Listener listener = new Listener();
            try
            {
                listener.GetConfig();
                listener.StartUp();
            }
            catch (Exception e)

            {
               // Console.WriteLine("\n服务器发生异常,消息：" + e.Message);

                listener.Close();
            }
        }
    }
}
