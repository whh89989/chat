namespace EasyChat_Server
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.msg_tb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ip_tb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.port_tb = new System.Windows.Forms.TextBox();
            this.open = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // msg_tb
            // 
            this.msg_tb.Location = new System.Drawing.Point(35, 132);
            this.msg_tb.Multiline = true;
            this.msg_tb.Name = "msg_tb";
            this.msg_tb.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.msg_tb.Size = new System.Drawing.Size(372, 123);
            this.msg_tb.TabIndex = 1;
            this.msg_tb.TextChanged += new System.EventHandler(this.msg_tb_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "系统消息";
            // 
            // ip_tb
            // 
            this.ip_tb.Location = new System.Drawing.Point(242, 35);
            this.ip_tb.Name = "ip_tb";
            this.ip_tb.Size = new System.Drawing.Size(100, 21);
            this.ip_tb.TabIndex = 3;
            this.ip_tb.TextChanged += new System.EventHandler(this.ip_tb_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(197, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "请输入IP地址（默认为127.0.0.1）:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(173, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "请输入端口号（默认是8888）：";
            // 
            // port_tb
            // 
            this.port_tb.Location = new System.Drawing.Point(242, 70);
            this.port_tb.Name = "port_tb";
            this.port_tb.Size = new System.Drawing.Size(100, 21);
            this.port_tb.TabIndex = 6;
            // 
            // open
            // 
            this.open.Location = new System.Drawing.Point(438, 59);
            this.open.Name = "open";
            this.open.Size = new System.Drawing.Size(75, 23);
            this.open.TabIndex = 7;
            this.open.Text = "启动服务器";
            this.open.UseVisualStyleBackColor = true;
            this.open.Click += new System.EventHandler(this.open_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(438, 195);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 8;
            this.close.Text = "断开服务器";
            this.close.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 281);
            this.Controls.Add(this.close);
            this.Controls.Add(this.open);
            this.Controls.Add(this.port_tb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ip_tb);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.msg_tb);
            this.Name = "Form1";
            this.Text = "服务器";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox msg_tb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ip_tb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox port_tb;
        private System.Windows.Forms.Button open;
        private System.Windows.Forms.Button close;
    }
}