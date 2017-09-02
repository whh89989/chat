namespace EasyChat
{
    partial class chat_frm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(chat_frm));
            this.chatrcd_rtb = new System.Windows.Forms.RichTextBox();
            this.msg_tb = new System.Windows.Forms.TextBox();
            this.clear_btn = new System.Windows.Forms.Button();
            this.close_btn = new System.Windows.Forms.Button();
            this.send_btn = new System.Windows.Forms.Button();
            this.save_btn = new System.Windows.Forms.Button();
            this.user_lb = new System.Windows.Forms.Label();
            this.svrskt_lb = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.online_cb = new System.Windows.Forms.ComboBox();
            this.private_rb = new System.Windows.Forms.RadioButton();
            this.broadcast_rb = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.hide_cb = new System.Windows.Forms.CheckBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.comeback_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.close_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.nudge_pb = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudge_pb)).BeginInit();
            this.SuspendLayout();
            // 
            // chatrcd_rtb
            // 
            this.chatrcd_rtb.AutoWordSelection = true;
            this.chatrcd_rtb.BackColor = System.Drawing.SystemColors.Window;
            this.chatrcd_rtb.Location = new System.Drawing.Point(13, 30);
            this.chatrcd_rtb.Name = "chatrcd_rtb";
            this.chatrcd_rtb.ReadOnly = true;
            this.chatrcd_rtb.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.chatrcd_rtb.Size = new System.Drawing.Size(494, 253);
            this.chatrcd_rtb.TabIndex = 0;
            this.chatrcd_rtb.Text = "";
            this.chatrcd_rtb.TextChanged += new System.EventHandler(this.chatrcd_rtb_TextChanged);
            // 
            // msg_tb
            // 
            this.msg_tb.Location = new System.Drawing.Point(13, 326);
            this.msg_tb.Multiline = true;
            this.msg_tb.Name = "msg_tb";
            this.msg_tb.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.msg_tb.Size = new System.Drawing.Size(494, 90);
            this.msg_tb.TabIndex = 1;
            // 
            // clear_btn
            // 
            this.clear_btn.Location = new System.Drawing.Point(120, 431);
            this.clear_btn.Name = "clear_btn";
            this.clear_btn.Size = new System.Drawing.Size(92, 23);
            this.clear_btn.TabIndex = 3;
            this.clear_btn.Text = "清除聊天记录";
            this.clear_btn.UseVisualStyleBackColor = true;
            this.clear_btn.Click += new System.EventHandler(this.clear_btn_Click);
            // 
            // close_btn
            // 
            this.close_btn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.close_btn.Location = new System.Drawing.Point(354, 431);
            this.close_btn.Name = "close_btn";
            this.close_btn.Size = new System.Drawing.Size(69, 23);
            this.close_btn.TabIndex = 4;
            this.close_btn.Text = "关 闭";
            this.close_btn.UseVisualStyleBackColor = true;
            this.close_btn.Click += new System.EventHandler(this.close_btn_Click);
            // 
            // send_btn
            // 
            this.send_btn.Location = new System.Drawing.Point(438, 431);
            this.send_btn.Name = "send_btn";
            this.send_btn.Size = new System.Drawing.Size(69, 23);
            this.send_btn.TabIndex = 5;
            this.send_btn.Text = "发 送";
            this.send_btn.UseVisualStyleBackColor = true;
            this.send_btn.Click += new System.EventHandler(this.send_btn_Click);
            // 
            // save_btn
            // 
            this.save_btn.Location = new System.Drawing.Point(14, 431);
            this.save_btn.Name = "save_btn";
            this.save_btn.Size = new System.Drawing.Size(92, 23);
            this.save_btn.TabIndex = 6;
            this.save_btn.Text = "保存聊天记录";
            this.save_btn.UseVisualStyleBackColor = true;
            this.save_btn.Click += new System.EventHandler(this.save_btn_Click);
            // 
            // user_lb
            // 
            this.user_lb.AutoSize = true;
            this.user_lb.Location = new System.Drawing.Point(12, 9);
            this.user_lb.Name = "user_lb";
            this.user_lb.Size = new System.Drawing.Size(29, 12);
            this.user_lb.TabIndex = 12;
            this.user_lb.Text = "name";
            this.user_lb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // svrskt_lb
            // 
            this.svrskt_lb.AutoSize = true;
            this.svrskt_lb.Location = new System.Drawing.Point(203, 9);
            this.svrskt_lb.Name = "svrskt_lb";
            this.svrskt_lb.Size = new System.Drawing.Size(41, 12);
            this.svrskt_lb.TabIndex = 13;
            this.svrskt_lb.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 300);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "请选择接受方：";
            // 
            // online_cb
            // 
            this.online_cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.online_cb.FormattingEnabled = true;
            this.online_cb.Location = new System.Drawing.Point(99, 296);
            this.online_cb.Name = "online_cb";
            this.online_cb.Size = new System.Drawing.Size(126, 20);
            this.online_cb.TabIndex = 16;
            this.online_cb.DropDown += new System.EventHandler(this.online_cb_DropDown);
            // 
            // private_rb
            // 
            this.private_rb.AutoSize = true;
            this.private_rb.Location = new System.Drawing.Point(380, 298);
            this.private_rb.Name = "private_rb";
            this.private_rb.Size = new System.Drawing.Size(59, 16);
            this.private_rb.TabIndex = 17;
            this.private_rb.Text = "悄悄话";
            this.private_rb.UseVisualStyleBackColor = true;
            // 
            // broadcast_rb
            // 
            this.broadcast_rb.AutoSize = true;
            this.broadcast_rb.Checked = true;
            this.broadcast_rb.Location = new System.Drawing.Point(449, 298);
            this.broadcast_rb.Name = "broadcast_rb";
            this.broadcast_rb.Size = new System.Drawing.Size(59, 16);
            this.broadcast_rb.TabIndex = 18;
            this.broadcast_rb.TabStop = true;
            this.broadcast_rb.Text = "聊天室";
            this.broadcast_rb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.broadcast_rb.UseVisualStyleBackColor = true;
            this.broadcast_rb.CheckedChanged += new System.EventHandler(this.broadcast_rb_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(309, 301);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 19;
            this.label2.Text = "聊天模式：";
            // 
            // hide_cb
            // 
            this.hide_cb.AutoSize = true;
            this.hide_cb.Location = new System.Drawing.Point(412, 8);
            this.hide_cb.Name = "hide_cb";
            this.hide_cb.Size = new System.Drawing.Size(96, 16);
            this.hide_cb.TabIndex = 20;
            this.hide_cb.Text = "最小化到托盘";
            this.hide_cb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.hide_cb.UseVisualStyleBackColor = true;
            this.hide_cb.CheckedChanged += new System.EventHandler(this.hide_cb_CheckedChanged);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "EasyChat";
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.comeback_tsmi,
            this.toolStripSeparator1,
            this.close_tsmi});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(119, 54);
            // 
            // comeback_tsmi
            // 
            this.comeback_tsmi.Name = "comeback_tsmi";
            this.comeback_tsmi.Size = new System.Drawing.Size(118, 22);
            this.comeback_tsmi.Text = "还原窗口";
            this.comeback_tsmi.Click += new System.EventHandler(this.comeback_tsmi_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(115, 6);
            // 
            // close_tsmi
            // 
            this.close_tsmi.Name = "close_tsmi";
            this.close_tsmi.Size = new System.Drawing.Size(118, 22);
            this.close_tsmi.Text = "关闭连接";
            this.close_tsmi.Click += new System.EventHandler(this.close_tsmi_Click);
            // 
            // nudge_pb
            // 
            this.nudge_pb.Image = ((System.Drawing.Image)(resources.GetObject("nudge_pb.Image")));
            this.nudge_pb.Location = new System.Drawing.Point(254, 295);
            this.nudge_pb.Name = "nudge_pb";
            this.nudge_pb.Size = new System.Drawing.Size(34, 22);
            this.nudge_pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.nudge_pb.TabIndex = 21;
            this.nudge_pb.TabStop = false;
            this.toolTip1.SetToolTip(this.nudge_pb, "振你没商量！");
            this.nudge_pb.Click += new System.EventHandler(this.nudge_pb_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 100;
            this.toolTip1.AutoPopDelay = 3000;
            this.toolTip1.InitialDelay = 100;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 20;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "发送闪屏振动";
            // 
            // chat_frm
            // 
            this.AcceptButton = this.send_btn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.close_btn;
            this.ClientSize = new System.Drawing.Size(520, 471);
            this.Controls.Add(this.nudge_pb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.online_cb);
            this.Controls.Add(this.hide_cb);
            this.Controls.Add(this.broadcast_rb);
            this.Controls.Add(this.private_rb);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.svrskt_lb);
            this.Controls.Add(this.user_lb);
            this.Controls.Add(this.save_btn);
            this.Controls.Add(this.send_btn);
            this.Controls.Add(this.close_btn);
            this.Controls.Add(this.clear_btn);
            this.Controls.Add(this.msg_tb);
            this.Controls.Add(this.chatrcd_rtb);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "chat_frm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "聊天窗口";
            this.SizeChanged += new System.EventHandler(this.chat_frm_SizeChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.chat_frm_FormClosing);
            this.Load += new System.EventHandler(this.chat_frm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudge_pb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox chatrcd_rtb;
        private System.Windows.Forms.TextBox msg_tb;
        private System.Windows.Forms.Button clear_btn;
        private System.Windows.Forms.Button close_btn;
        private System.Windows.Forms.Button send_btn;
        private System.Windows.Forms.Button save_btn;
        private System.Windows.Forms.Label user_lb;
        private System.Windows.Forms.Label svrskt_lb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox online_cb;
        private System.Windows.Forms.RadioButton private_rb;
        private System.Windows.Forms.RadioButton broadcast_rb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox hide_cb;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem comeback_tsmi;
        private System.Windows.Forms.ToolStripMenuItem close_tsmi;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.PictureBox nudge_pb;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}