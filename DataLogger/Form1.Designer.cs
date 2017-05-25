namespace WinformProtocol
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textLog = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textIP = new System.Windows.Forms.TextBox();
            this.chkRun = new System.Windows.Forms.CheckBox();
            this.textPort = new System.Windows.Forms.TextBox();
            this.btnListen = new System.Windows.Forms.Button();
            this.btnRefesh = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Restore = new System.Windows.Forms.ToolStripMenuItem();
            this.Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.Push = new System.Windows.Forms.TabPage();
            this.DataTranfer = new System.Windows.Forms.TabControl();
            this.Pull = new System.Windows.Forms.TabPage();
            this.textLog1 = new System.Windows.Forms.TextBox();
            this.textIPFTP = new System.Windows.Forms.TextBox();
            this.textPortFTP = new System.Windows.Forms.TextBox();
            this.textFolder = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.Push.SuspendLayout();
            this.DataTranfer.SuspendLayout();
            this.Pull.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(685, 405);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.DataTranfer);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 71);
            this.panel3.Margin = new System.Windows.Forms.Padding(10);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(685, 334);
            this.panel3.TabIndex = 8;
            // 
            // textLog
            // 
            this.textLog.BackColor = System.Drawing.SystemColors.Window;
            this.textLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textLog.Location = new System.Drawing.Point(3, 3);
            this.textLog.Margin = new System.Windows.Forms.Padding(10);
            this.textLog.Multiline = true;
            this.textLog.Name = "textLog";
            this.textLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textLog.Size = new System.Drawing.Size(671, 302);
            this.textLog.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textFolder);
            this.panel2.Controls.Add(this.textPortFTP);
            this.panel2.Controls.Add(this.textIPFTP);
            this.panel2.Controls.Add(this.textIP);
            this.panel2.Controls.Add(this.chkRun);
            this.panel2.Controls.Add(this.textPort);
            this.panel2.Controls.Add(this.btnListen);
            this.panel2.Controls.Add(this.btnRefesh);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(685, 68);
            this.panel2.TabIndex = 7;
            // 
            // textIP
            // 
            this.textIP.Location = new System.Drawing.Point(20, 10);
            this.textIP.Name = "textIP";
            this.textIP.Size = new System.Drawing.Size(182, 20);
            this.textIP.TabIndex = 0;
            // 
            // chkRun
            // 
            this.chkRun.AutoSize = true;
            this.chkRun.Enabled = false;
            this.chkRun.Location = new System.Drawing.Point(577, 12);
            this.chkRun.Name = "chkRun";
            this.chkRun.Size = new System.Drawing.Size(58, 17);
            this.chkRun.TabIndex = 6;
            this.chkRun.Text = "startup";
            this.chkRun.UseVisualStyleBackColor = true;
            this.chkRun.CheckedChanged += new System.EventHandler(this.chkRun_CheckedChanged);
            // 
            // textPort
            // 
            this.textPort.Location = new System.Drawing.Point(228, 9);
            this.textPort.Name = "textPort";
            this.textPort.Size = new System.Drawing.Size(100, 20);
            this.textPort.TabIndex = 1;
            // 
            // btnListen
            // 
            this.btnListen.Location = new System.Drawing.Point(365, 7);
            this.btnListen.Name = "btnListen";
            this.btnListen.Size = new System.Drawing.Size(75, 23);
            this.btnListen.TabIndex = 3;
            this.btnListen.Text = "Listen";
            this.btnListen.UseVisualStyleBackColor = true;
            this.btnListen.Click += new System.EventHandler(this.btnListen_Click);
            // 
            // btnRefesh
            // 
            this.btnRefesh.Location = new System.Drawing.Point(474, 7);
            this.btnRefesh.Name = "btnRefesh";
            this.btnRefesh.Size = new System.Drawing.Size(75, 23);
            this.btnRefesh.TabIndex = 4;
            this.btnRefesh.Text = "Refesh";
            this.btnRefesh.UseVisualStyleBackColor = true;
            this.btnRefesh.Click += new System.EventHandler(this.btnRefesh_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "notifyIcon";
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Restore,
            this.Exit});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(114, 48);
            // 
            // Restore
            // 
            this.Restore.Name = "Restore";
            this.Restore.Size = new System.Drawing.Size(113, 22);
            this.Restore.Text = "Restore";
            this.Restore.Click += new System.EventHandler(this.Restore_Click);
            // 
            // Exit
            // 
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(113, 22);
            this.Exit.Text = "Exit";
            // 
            // Push
            // 
            this.Push.Controls.Add(this.textLog1);
            this.Push.Location = new System.Drawing.Point(4, 22);
            this.Push.Name = "Push";
            this.Push.Padding = new System.Windows.Forms.Padding(3);
            this.Push.Size = new System.Drawing.Size(677, 308);
            this.Push.TabIndex = 1;
            this.Push.Text = "Push";
            this.Push.UseVisualStyleBackColor = true;
            // 
            // DataTranfer
            // 
            this.DataTranfer.Controls.Add(this.Pull);
            this.DataTranfer.Controls.Add(this.Push);
            this.DataTranfer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataTranfer.Location = new System.Drawing.Point(0, 0);
            this.DataTranfer.Name = "DataTranfer";
            this.DataTranfer.SelectedIndex = 0;
            this.DataTranfer.Size = new System.Drawing.Size(685, 334);
            this.DataTranfer.TabIndex = 6;
            // 
            // Pull
            // 
            this.Pull.Controls.Add(this.textLog);
            this.Pull.Location = new System.Drawing.Point(4, 22);
            this.Pull.Name = "Pull";
            this.Pull.Padding = new System.Windows.Forms.Padding(3);
            this.Pull.Size = new System.Drawing.Size(677, 308);
            this.Pull.TabIndex = 0;
            this.Pull.Text = "Pull";
            this.Pull.UseVisualStyleBackColor = true;
            // 
            // textLog1
            // 
            this.textLog1.BackColor = System.Drawing.SystemColors.Window;
            this.textLog1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textLog1.Location = new System.Drawing.Point(3, 3);
            this.textLog1.Margin = new System.Windows.Forms.Padding(10);
            this.textLog1.Multiline = true;
            this.textLog1.Name = "textLog1";
            this.textLog1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textLog1.Size = new System.Drawing.Size(671, 302);
            this.textLog1.TabIndex = 6;
            // 
            // textIPFTP
            // 
            this.textIPFTP.Location = new System.Drawing.Point(20, 38);
            this.textIPFTP.Name = "textIPFTP";
            this.textIPFTP.Size = new System.Drawing.Size(182, 20);
            this.textIPFTP.TabIndex = 7;
            // 
            // textPortFTP
            // 
            this.textPortFTP.Location = new System.Drawing.Point(228, 38);
            this.textPortFTP.Name = "textPortFTP";
            this.textPortFTP.Size = new System.Drawing.Size(100, 20);
            this.textPortFTP.TabIndex = 8;
            // 
            // textFolder
            // 
            this.textFolder.Location = new System.Drawing.Point(353, 38);
            this.textFolder.Name = "textFolder";
            this.textFolder.Size = new System.Drawing.Size(196, 20);
            this.textFolder.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 405);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Protocol";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.Push.ResumeLayout(false);
            this.Push.PerformLayout();
            this.DataTranfer.ResumeLayout(false);
            this.Pull.ResumeLayout(false);
            this.Pull.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnListen;
        private System.Windows.Forms.TextBox textPort;
        private System.Windows.Forms.TextBox textIP;
        private System.Windows.Forms.Button btnRefesh;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Restore;
        private System.Windows.Forms.ToolStripMenuItem Exit;
        public System.Windows.Forms.TextBox textLog;
        private System.Windows.Forms.CheckBox chkRun;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl DataTranfer;
        private System.Windows.Forms.TabPage Pull;
        private System.Windows.Forms.TabPage Push;
        public System.Windows.Forms.TextBox textLog1;
        private System.Windows.Forms.TextBox textPortFTP;
        private System.Windows.Forms.TextBox textIPFTP;
        private System.Windows.Forms.TextBox textFolder;
    }
}

