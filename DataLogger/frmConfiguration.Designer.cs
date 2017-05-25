namespace DataLogger
{
    partial class frmConfiguration
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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.lblSamplerComport = new System.Windows.Forms.Label();
            this.lblTNComport = new System.Windows.Forms.Label();
            this.lblTPComport = new System.Windows.Forms.Label();
            this.lblTOCComport = new System.Windows.Forms.Label();
            this.lblMPSComport = new System.Windows.Forms.Label();
            this.lblModuleComport = new System.Windows.Forms.Label();
            this.cbSampler = new System.Windows.Forms.ComboBox();
            this.cbTN = new System.Windows.Forms.ComboBox();
            this.cbTP = new System.Windows.Forms.ComboBox();
            this.cbTOC = new System.Windows.Forms.ComboBox();
            this.cbMPS = new System.Windows.Forms.ComboBox();
            this.cbModule = new System.Windows.Forms.ComboBox();
            this.lblComport = new System.Windows.Forms.Label();
            this.cbTNProtocol = new System.Windows.Forms.ComboBox();
            this.cbTPProtocol = new System.Windows.Forms.ComboBox();
            this.cbTOCProtocol = new System.Windows.Forms.ComboBox();
            this.cbMPSProtocol = new System.Windows.Forms.ComboBox();
            this.lblCommProtocol = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblStationName = new System.Windows.Forms.Label();
            this.lblStationID = new System.Windows.Forms.Label();
            this.lblSocketPort = new System.Windows.Forms.Label();
            this.txtStationName = new System.Windows.Forms.TextBox();
            this.txtStationID = new System.Windows.Forms.TextBox();
            this.txtSocketPort = new System.Windows.Forms.TextBox();
            this.btnSOCKET = new System.Windows.Forms.Button();
            this.btnShow = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImage = global::DataLogger.Properties.Resources.Save_Button_1;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.SystemColors.Window;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(536, 459);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(115, 49);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.BackgroundImage = global::DataLogger.Properties.Resources.Refesh_button;
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.SystemColors.Window;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefresh.Location = new System.Drawing.Point(413, 459);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(117, 49);
            this.btnRefresh.TabIndex = 12;
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.lblCommProtocol);
            this.tabPage4.Controls.Add(this.cbMPSProtocol);
            this.tabPage4.Controls.Add(this.cbTOCProtocol);
            this.tabPage4.Controls.Add(this.cbTPProtocol);
            this.tabPage4.Controls.Add(this.cbTNProtocol);
            this.tabPage4.Controls.Add(this.lblComport);
            this.tabPage4.Controls.Add(this.cbModule);
            this.tabPage4.Controls.Add(this.cbMPS);
            this.tabPage4.Controls.Add(this.cbTOC);
            this.tabPage4.Controls.Add(this.cbTP);
            this.tabPage4.Controls.Add(this.cbTN);
            this.tabPage4.Controls.Add(this.cbSampler);
            this.tabPage4.Controls.Add(this.lblModuleComport);
            this.tabPage4.Controls.Add(this.lblMPSComport);
            this.tabPage4.Controls.Add(this.lblTOCComport);
            this.tabPage4.Controls.Add(this.lblTPComport);
            this.tabPage4.Controls.Add(this.lblTNComport);
            this.tabPage4.Controls.Add(this.lblSamplerComport);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage4.Size = new System.Drawing.Size(654, 412);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Comport";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // lblSamplerComport
            // 
            this.lblSamplerComport.AutoSize = true;
            this.lblSamplerComport.Location = new System.Drawing.Point(12, 18);
            this.lblSamplerComport.Name = "lblSamplerComport";
            this.lblSamplerComport.Size = new System.Drawing.Size(45, 13);
            this.lblSamplerComport.TabIndex = 18;
            this.lblSamplerComport.Text = "Sampler";
            // 
            // lblTNComport
            // 
            this.lblTNComport.AutoSize = true;
            this.lblTNComport.Location = new System.Drawing.Point(12, 108);
            this.lblTNComport.Name = "lblTNComport";
            this.lblTNComport.Size = new System.Drawing.Size(21, 13);
            this.lblTNComport.TabIndex = 19;
            this.lblTNComport.Text = "SS";
            // 
            // lblTPComport
            // 
            this.lblTPComport.AutoSize = true;
            this.lblTPComport.Location = new System.Drawing.Point(12, 135);
            this.lblTPComport.Name = "lblTPComport";
            this.lblTPComport.Size = new System.Drawing.Size(21, 13);
            this.lblTPComport.TabIndex = 20;
            this.lblTPComport.Text = "pH";
            // 
            // lblTOCComport
            // 
            this.lblTOCComport.AutoSize = true;
            this.lblTOCComport.Location = new System.Drawing.Point(12, 161);
            this.lblTOCComport.Name = "lblTOCComport";
            this.lblTOCComport.Size = new System.Drawing.Size(29, 13);
            this.lblTOCComport.TabIndex = 21;
            this.lblTOCComport.Text = "TOC";
            // 
            // lblMPSComport
            // 
            this.lblMPSComport.AutoSize = true;
            this.lblMPSComport.Location = new System.Drawing.Point(12, 188);
            this.lblMPSComport.Name = "lblMPSComport";
            this.lblMPSComport.Size = new System.Drawing.Size(30, 13);
            this.lblMPSComport.TabIndex = 22;
            this.lblMPSComport.Text = "MPS";
            this.lblMPSComport.Visible = false;
            // 
            // lblModuleComport
            // 
            this.lblModuleComport.AutoSize = true;
            this.lblModuleComport.Location = new System.Drawing.Point(12, 49);
            this.lblModuleComport.Name = "lblModuleComport";
            this.lblModuleComport.Size = new System.Drawing.Size(42, 13);
            this.lblModuleComport.TabIndex = 23;
            this.lblModuleComport.Text = "Module";
            this.lblModuleComport.Visible = false;
            // 
            // cbSampler
            // 
            this.cbSampler.FormattingEnabled = true;
            this.cbSampler.Location = new System.Drawing.Point(89, 15);
            this.cbSampler.Name = "cbSampler";
            this.cbSampler.Size = new System.Drawing.Size(117, 21);
            this.cbSampler.TabIndex = 24;
            // 
            // cbTN
            // 
            this.cbTN.FormattingEnabled = true;
            this.cbTN.Location = new System.Drawing.Point(89, 105);
            this.cbTN.Name = "cbTN";
            this.cbTN.Size = new System.Drawing.Size(117, 21);
            this.cbTN.TabIndex = 25;
            // 
            // cbTP
            // 
            this.cbTP.FormattingEnabled = true;
            this.cbTP.Location = new System.Drawing.Point(89, 132);
            this.cbTP.Name = "cbTP";
            this.cbTP.Size = new System.Drawing.Size(117, 21);
            this.cbTP.TabIndex = 26;
            // 
            // cbTOC
            // 
            this.cbTOC.FormattingEnabled = true;
            this.cbTOC.Location = new System.Drawing.Point(89, 158);
            this.cbTOC.Name = "cbTOC";
            this.cbTOC.Size = new System.Drawing.Size(117, 21);
            this.cbTOC.TabIndex = 27;
            // 
            // cbMPS
            // 
            this.cbMPS.FormattingEnabled = true;
            this.cbMPS.Location = new System.Drawing.Point(89, 185);
            this.cbMPS.Name = "cbMPS";
            this.cbMPS.Size = new System.Drawing.Size(117, 21);
            this.cbMPS.TabIndex = 28;
            this.cbMPS.Visible = false;
            // 
            // cbModule
            // 
            this.cbModule.FormattingEnabled = true;
            this.cbModule.Location = new System.Drawing.Point(90, 46);
            this.cbModule.Name = "cbModule";
            this.cbModule.Size = new System.Drawing.Size(117, 21);
            this.cbModule.TabIndex = 29;
            this.cbModule.Visible = false;
            // 
            // lblComport
            // 
            this.lblComport.AutoSize = true;
            this.lblComport.Location = new System.Drawing.Point(87, 89);
            this.lblComport.Name = "lblComport";
            this.lblComport.Size = new System.Drawing.Size(46, 13);
            this.lblComport.TabIndex = 30;
            this.lblComport.Text = "Comport";
            // 
            // cbTNProtocol
            // 
            this.cbTNProtocol.FormattingEnabled = true;
            this.cbTNProtocol.Location = new System.Drawing.Point(223, 105);
            this.cbTNProtocol.Name = "cbTNProtocol";
            this.cbTNProtocol.Size = new System.Drawing.Size(117, 21);
            this.cbTNProtocol.TabIndex = 31;
            // 
            // cbTPProtocol
            // 
            this.cbTPProtocol.FormattingEnabled = true;
            this.cbTPProtocol.Location = new System.Drawing.Point(223, 132);
            this.cbTPProtocol.Name = "cbTPProtocol";
            this.cbTPProtocol.Size = new System.Drawing.Size(117, 21);
            this.cbTPProtocol.TabIndex = 32;
            // 
            // cbTOCProtocol
            // 
            this.cbTOCProtocol.FormattingEnabled = true;
            this.cbTOCProtocol.Location = new System.Drawing.Point(223, 158);
            this.cbTOCProtocol.Name = "cbTOCProtocol";
            this.cbTOCProtocol.Size = new System.Drawing.Size(117, 21);
            this.cbTOCProtocol.TabIndex = 33;
            // 
            // cbMPSProtocol
            // 
            this.cbMPSProtocol.FormattingEnabled = true;
            this.cbMPSProtocol.Location = new System.Drawing.Point(223, 185);
            this.cbMPSProtocol.Name = "cbMPSProtocol";
            this.cbMPSProtocol.Size = new System.Drawing.Size(117, 21);
            this.cbMPSProtocol.TabIndex = 34;
            this.cbMPSProtocol.Visible = false;
            // 
            // lblCommProtocol
            // 
            this.lblCommProtocol.AutoSize = true;
            this.lblCommProtocol.Location = new System.Drawing.Point(220, 89);
            this.lblCommProtocol.Name = "lblCommProtocol";
            this.lblCommProtocol.Size = new System.Drawing.Size(46, 13);
            this.lblCommProtocol.TabIndex = 35;
            this.lblCommProtocol.Text = "Protocol";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnShow);
            this.tabPage1.Controls.Add(this.btnSOCKET);
            this.tabPage1.Controls.Add(this.txtSocketPort);
            this.tabPage1.Controls.Add(this.txtStationID);
            this.tabPage1.Controls.Add(this.txtStationName);
            this.tabPage1.Controls.Add(this.lblSocketPort);
            this.tabPage1.Controls.Add(this.lblStationID);
            this.tabPage1.Controls.Add(this.lblStationName);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(654, 412);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Station";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblStationName
            // 
            this.lblStationName.AutoSize = true;
            this.lblStationName.Location = new System.Drawing.Point(13, 24);
            this.lblStationName.Name = "lblStationName";
            this.lblStationName.Size = new System.Drawing.Size(69, 13);
            this.lblStationName.TabIndex = 13;
            this.lblStationName.Text = "Station name";
            // 
            // lblStationID
            // 
            this.lblStationID.AutoSize = true;
            this.lblStationID.Location = new System.Drawing.Point(13, 66);
            this.lblStationID.Name = "lblStationID";
            this.lblStationID.Size = new System.Drawing.Size(54, 13);
            this.lblStationID.TabIndex = 14;
            this.lblStationID.Text = "Station ID";
            // 
            // lblSocketPort
            // 
            this.lblSocketPort.AutoSize = true;
            this.lblSocketPort.Location = new System.Drawing.Point(13, 116);
            this.lblSocketPort.Name = "lblSocketPort";
            this.lblSocketPort.Size = new System.Drawing.Size(62, 13);
            this.lblSocketPort.TabIndex = 15;
            this.lblSocketPort.Text = "Socket port";
            // 
            // txtStationName
            // 
            this.txtStationName.Location = new System.Drawing.Point(88, 21);
            this.txtStationName.Name = "txtStationName";
            this.txtStationName.Size = new System.Drawing.Size(118, 20);
            this.txtStationName.TabIndex = 16;
            // 
            // txtStationID
            // 
            this.txtStationID.Location = new System.Drawing.Point(88, 63);
            this.txtStationID.Name = "txtStationID";
            this.txtStationID.Size = new System.Drawing.Size(238, 20);
            this.txtStationID.TabIndex = 17;
            // 
            // txtSocketPort
            // 
            this.txtSocketPort.Location = new System.Drawing.Point(88, 113);
            this.txtSocketPort.Name = "txtSocketPort";
            this.txtSocketPort.Size = new System.Drawing.Size(118, 20);
            this.txtSocketPort.TabIndex = 18;
            // 
            // btnSOCKET
            // 
            this.btnSOCKET.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSOCKET.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSOCKET.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSOCKET.ForeColor = System.Drawing.SystemColors.Window;
            this.btnSOCKET.Image = global::DataLogger.Properties.Resources.ON_switch_96x25;
            this.btnSOCKET.Location = new System.Drawing.Point(245, 102);
            this.btnSOCKET.Name = "btnSOCKET";
            this.btnSOCKET.Size = new System.Drawing.Size(81, 40);
            this.btnSOCKET.TabIndex = 19;
            this.btnSOCKET.UseVisualStyleBackColor = true;
            this.btnSOCKET.Click += new System.EventHandler(this.btnSOCKET_Click);
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(363, 113);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(75, 23);
            this.btnShow.TabIndex = 20;
            this.btnShow.Text = "Hide";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Visible = false;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(9, 10);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(662, 438);
            this.tabControl1.TabIndex = 88;
            // 
            // frmConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 520);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmConfiguration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuration";
            this.Load += new System.EventHandler(this.frmConfiguration_Load);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label lblCommProtocol;
        private System.Windows.Forms.ComboBox cbMPSProtocol;
        private System.Windows.Forms.ComboBox cbTOCProtocol;
        private System.Windows.Forms.ComboBox cbTPProtocol;
        private System.Windows.Forms.ComboBox cbTNProtocol;
        private System.Windows.Forms.Label lblComport;
        private System.Windows.Forms.ComboBox cbModule;
        private System.Windows.Forms.ComboBox cbMPS;
        private System.Windows.Forms.ComboBox cbTOC;
        private System.Windows.Forms.ComboBox cbTP;
        private System.Windows.Forms.ComboBox cbTN;
        private System.Windows.Forms.ComboBox cbSampler;
        private System.Windows.Forms.Label lblModuleComport;
        private System.Windows.Forms.Label lblMPSComport;
        private System.Windows.Forms.Label lblTOCComport;
        private System.Windows.Forms.Label lblTPComport;
        private System.Windows.Forms.Label lblTNComport;
        private System.Windows.Forms.Label lblSamplerComport;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnShow;
        public System.Windows.Forms.Button btnSOCKET;
        private System.Windows.Forms.TextBox txtSocketPort;
        private System.Windows.Forms.TextBox txtStationID;
        private System.Windows.Forms.TextBox txtStationName;
        private System.Windows.Forms.Label lblSocketPort;
        private System.Windows.Forms.Label lblStationID;
        private System.Windows.Forms.Label lblStationName;
        private System.Windows.Forms.TabControl tabControl1;
    }
}