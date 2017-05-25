namespace DataLogger
{
    partial class frmNewMain
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
        public void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewMain));
            this.serialPortTN = new System.IO.Ports.SerialPort(this.components);
            this.serialPortTP = new System.IO.Ports.SerialPort(this.components);
            this.serialPortTOC = new System.IO.Ports.SerialPort(this.components);
            this.serialPortSAMP = new System.IO.Ports.SerialPort(this.components);
            this.bgwMonthlyReport = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerMain = new System.ComponentModel.BackgroundWorker();
            this.panel30 = new System.Windows.Forms.Panel();
            this.panel20 = new System.Windows.Forms.Panel();
            this.lblMainMenuTitle = new System.Windows.Forms.Label();
            this.pnSoftwareInfo = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.lblSurfaceWaterQuality = new System.Windows.Forms.Label();
            this.lblAutomaticMonitoring = new System.Windows.Forms.Label();
            this.lblThaiNguyenStation = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox52 = new System.Windows.Forms.PictureBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblWaterLevel = new System.Windows.Forms.Label();
            this.lblHeaderNationName = new System.Windows.Forms.Label();
            this.txtData = new System.Windows.Forms.RichTextBox();
            this.picSamplerTank = new System.Windows.Forms.PictureBox();
            this.btnLanguage = new System.Windows.Forms.Button();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.listen = new DataLogger.FlatButton();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblAutorSampler = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnAutoSamplerTesting = new System.Windows.Forms.Button();
            this.btnAutoSamplerHistoryData = new System.Windows.Forms.Button();
            this.picAutoSamplerStatus = new System.Windows.Forms.PictureBox();
            this.pnbottlePosition = new System.Windows.Forms.Panel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.txtAutoSamplerTemp = new System.Windows.Forms.TextBox();
            this.pnLeftSide = new System.Windows.Forms.Panel();
            this.vprgMonthlyReport = new VerticalProgressBar.VerticalProgressBar();
            this.btnMaintenance = new System.Windows.Forms.Button();
            this.btnMonthlyReport = new System.Windows.Forms.Button();
            this.btnSetting = new System.Windows.Forms.Button();
            this.btnUsers = new System.Windows.Forms.Button();
            this.btnAllHistory = new System.Windows.Forms.Button();
            this.pnHeader = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel18 = new System.Windows.Forms.Panel();
            this.btnLoginLogout = new System.Windows.Forms.Button();
            this.lblLoginDisplayName = new System.Windows.Forms.Label();
            this.lblHeadingTime = new System.Windows.Forms.Label();
            this.panel24 = new System.Windows.Forms.Panel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.vprgTNValue = new VerticalProgressBar.VerticalProgressBar();
            this.btnTN1Hour = new System.Windows.Forms.Button();
            this.btnTN5Minute = new System.Windows.Forms.Button();
            this.btnTNHistoryData = new System.Windows.Forms.Button();
            this.txtTNValue = new System.Windows.Forms.TextBox();
            this.label52 = new System.Windows.Forms.Label();
            this.btnTNCalibrate = new System.Windows.Forms.Button();
            this.picTNStatus = new System.Windows.Forms.PictureBox();
            this.label53 = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.vprgTPValue = new VerticalProgressBar.VerticalProgressBar();
            this.label49 = new System.Windows.Forms.Label();
            this.btnTP5Minute = new System.Windows.Forms.Button();
            this.btnTPCalibrate = new System.Windows.Forms.Button();
            this.btnTP1Hour = new System.Windows.Forms.Button();
            this.picTPStatus = new System.Windows.Forms.PictureBox();
            this.txtTPValue = new System.Windows.Forms.TextBox();
            this.btnTPHistoryData = new System.Windows.Forms.Button();
            this.label47 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.vprgTOCValue = new VerticalProgressBar.VerticalProgressBar();
            this.txtTOCValue = new System.Windows.Forms.TextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.picTOCStatus = new System.Windows.Forms.PictureBox();
            this.label43 = new System.Windows.Forms.Label();
            this.btnTOCCalibrate = new System.Windows.Forms.Button();
            this.btnTOC5Minute = new System.Windows.Forms.Button();
            this.btnTOCHistoryData = new System.Windows.Forms.Button();
            this.btnTOC1Hour = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel23 = new System.Windows.Forms.Panel();
            this.panel30.SuspendLayout();
            this.panel20.SuspendLayout();
            this.pnSoftwareInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox52)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSamplerTank)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAutoSamplerStatus)).BeginInit();
            this.pnbottlePosition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.pnLeftSide.SuspendLayout();
            this.pnHeader.SuspendLayout();
            this.panel18.SuspendLayout();
            this.panel24.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTNStatus)).BeginInit();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTPStatus)).BeginInit();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTOCStatus)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPortTN
            // 
            this.serialPortTN.PortName = "COM100";
            this.serialPortTN.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortTN_DataReceived);
            // 
            // serialPortTP
            // 
            this.serialPortTP.PortName = "COM100";
            this.serialPortTP.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortTP_DataReceived);
            // 
            // serialPortTOC
            // 
            this.serialPortTOC.PortName = "COM100";
            this.serialPortTOC.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortTOC_DataReceived);
            // 
            // serialPortSAMP
            // 
            this.serialPortSAMP.PortName = "COM100";
            this.serialPortSAMP.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortSAMP_DataReceived);
            // 
            // bgwMonthlyReport
            // 
            this.bgwMonthlyReport.WorkerReportsProgress = true;
            this.bgwMonthlyReport.WorkerSupportsCancellation = true;
            this.bgwMonthlyReport.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerMonthlyReport_DoWork);
            this.bgwMonthlyReport.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerMonthlyReport_ProgressChanged);
            this.bgwMonthlyReport.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerMonthlyReport_RunWorkerCompleted);
            // 
            // backgroundWorkerMain
            // 
            this.backgroundWorkerMain.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerMain_DoWork);
            // 
            // panel30
            // 
            this.panel30.AutoSize = true;
            this.panel30.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel30.BackColor = System.Drawing.Color.Transparent;
            this.panel30.BackgroundImage = global::DataLogger.Properties.Resources.main;
            this.panel30.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel30.Controls.Add(this.panel20);
            this.panel30.Controls.Add(this.panel5);
            this.panel30.Controls.Add(this.panel3);
            this.panel30.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel30.Location = new System.Drawing.Point(86, 54);
            this.panel30.Margin = new System.Windows.Forms.Padding(10);
            this.panel30.Name = "panel30";
            this.panel30.Size = new System.Drawing.Size(205, 397);
            this.panel30.TabIndex = 65;
            this.panel30.Paint += new System.Windows.Forms.PaintEventHandler(this.panel30_Paint);
            // 
            // panel20
            // 
            this.panel20.Controls.Add(this.lblMainMenuTitle);
            this.panel20.Controls.Add(this.pnSoftwareInfo);
            this.panel20.Controls.Add(this.pictureBox52);
            this.panel20.Controls.Add(this.btnExit);
            this.panel20.Controls.Add(this.lblWaterLevel);
            this.panel20.Controls.Add(this.lblHeaderNationName);
            this.panel20.Controls.Add(this.txtData);
            this.panel20.Controls.Add(this.picSamplerTank);
            this.panel20.Controls.Add(this.btnLanguage);
            this.panel20.Controls.Add(this.pictureBox5);
            this.panel20.Controls.Add(this.listen);
            this.panel20.Controls.Add(this.button5);
            this.panel20.Controls.Add(this.button4);
            this.panel20.Location = new System.Drawing.Point(218, 311);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(29, 10);
            this.panel20.TabIndex = 70;
            // 
            // lblMainMenuTitle
            // 
            this.lblMainMenuTitle.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMainMenuTitle.ForeColor = System.Drawing.Color.White;
            this.lblMainMenuTitle.Location = new System.Drawing.Point(-51, 33);
            this.lblMainMenuTitle.Name = "lblMainMenuTitle";
            this.lblMainMenuTitle.Size = new System.Drawing.Size(150, 22);
            this.lblMainMenuTitle.TabIndex = 3;
            this.lblMainMenuTitle.Text = "MAIN MENU";
            this.lblMainMenuTitle.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblMainMenuTitle.Visible = false;
            // 
            // pnSoftwareInfo
            // 
            this.pnSoftwareInfo.BackColor = System.Drawing.Color.White;
            this.pnSoftwareInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnSoftwareInfo.Controls.Add(this.label6);
            this.pnSoftwareInfo.Controls.Add(this.lblSurfaceWaterQuality);
            this.pnSoftwareInfo.Controls.Add(this.lblAutomaticMonitoring);
            this.pnSoftwareInfo.Controls.Add(this.lblThaiNguyenStation);
            this.pnSoftwareInfo.Controls.Add(this.pictureBox3);
            this.pnSoftwareInfo.Controls.Add(this.pictureBox2);
            this.pnSoftwareInfo.Location = new System.Drawing.Point(-416, 23);
            this.pnSoftwareInfo.Name = "pnSoftwareInfo";
            this.pnSoftwareInfo.Size = new System.Drawing.Size(34, 13);
            this.pnSoftwareInfo.TabIndex = 4;
            this.pnSoftwareInfo.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(145, 179);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 14);
            this.label6.TabIndex = 6;
            this.label6.Text = "2015";
            // 
            // lblSurfaceWaterQuality
            // 
            this.lblSurfaceWaterQuality.AutoSize = true;
            this.lblSurfaceWaterQuality.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSurfaceWaterQuality.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblSurfaceWaterQuality.Location = new System.Drawing.Point(43, 117);
            this.lblSurfaceWaterQuality.Name = "lblSurfaceWaterQuality";
            this.lblSurfaceWaterQuality.Size = new System.Drawing.Size(214, 19);
            this.lblSurfaceWaterQuality.TabIndex = 5;
            this.lblSurfaceWaterQuality.Text = "SURFACE WATER QUALITY";
            // 
            // lblAutomaticMonitoring
            // 
            this.lblAutomaticMonitoring.AutoSize = true;
            this.lblAutomaticMonitoring.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAutomaticMonitoring.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblAutomaticMonitoring.Location = new System.Drawing.Point(43, 97);
            this.lblAutomaticMonitoring.Name = "lblAutomaticMonitoring";
            this.lblAutomaticMonitoring.Size = new System.Drawing.Size(220, 19);
            this.lblAutomaticMonitoring.TabIndex = 4;
            this.lblAutomaticMonitoring.Text = "AUTOMATIC MONITORING";
            // 
            // lblThaiNguyenStation
            // 
            this.lblThaiNguyenStation.AutoSize = true;
            this.lblThaiNguyenStation.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThaiNguyenStation.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblThaiNguyenStation.Location = new System.Drawing.Point(53, 66);
            this.lblThaiNguyenStation.Name = "lblThaiNguyenStation";
            this.lblThaiNguyenStation.Size = new System.Drawing.Size(126, 19);
            this.lblThaiNguyenStation.TabIndex = 3;
            this.lblThaiNguyenStation.Text = "DMM STATION";
            // 
            // pictureBox3
            // 
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox3.Image = global::DataLogger.Properties.Resources.Flag_of_South_Korea_48x32;
            this.pictureBox3.Location = new System.Drawing.Point(202, 20);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(48, 32);
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Image = global::DataLogger.Properties.Resources.Flag_of_Vietnam_43x32;
            this.pictureBox2.Location = new System.Drawing.Point(76, 20);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(43, 32);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox52
            // 
            this.pictureBox52.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox52.BackgroundImage = global::DataLogger.Properties.Resources.SamplerTank_Ruler;
            this.pictureBox52.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox52.Location = new System.Drawing.Point(138, 32);
            this.pictureBox52.Name = "pictureBox52";
            this.pictureBox52.Size = new System.Drawing.Size(10, 25);
            this.pictureBox52.TabIndex = 63;
            this.pictureBox52.TabStop = false;
            this.pictureBox52.Visible = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnExit.BackgroundImage = global::DataLogger.Properties.Resources.Shutdown_Box_Red;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.Enabled = false;
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Location = new System.Drawing.Point(154, 32);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(40, 10);
            this.btnExit.TabIndex = 7;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Visible = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblWaterLevel
            // 
            this.lblWaterLevel.AutoSize = true;
            this.lblWaterLevel.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWaterLevel.Location = new System.Drawing.Point(-47, 36);
            this.lblWaterLevel.Name = "lblWaterLevel";
            this.lblWaterLevel.Size = new System.Drawing.Size(67, 15);
            this.lblWaterLevel.TabIndex = 31;
            this.lblWaterLevel.Text = "Water level";
            this.lblWaterLevel.Visible = false;
            // 
            // lblHeaderNationName
            // 
            this.lblHeaderNationName.AutoSize = true;
            this.lblHeaderNationName.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderNationName.ForeColor = System.Drawing.Color.White;
            this.lblHeaderNationName.Location = new System.Drawing.Point(-316, 34);
            this.lblHeaderNationName.Name = "lblHeaderNationName";
            this.lblHeaderNationName.Size = new System.Drawing.Size(84, 17);
            this.lblHeaderNationName.TabIndex = 1;
            this.lblHeaderNationName.Text = "Vietnamese";
            this.lblHeaderNationName.Visible = false;
            // 
            // txtData
            // 
            this.txtData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtData.ForeColor = System.Drawing.Color.Maroon;
            this.txtData.Location = new System.Drawing.Point(94, 41);
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(48, 10);
            this.txtData.TabIndex = 62;
            this.txtData.Text = "";
            this.txtData.Visible = false;
            // 
            // picSamplerTank
            // 
            this.picSamplerTank.BackColor = System.Drawing.Color.White;
            this.picSamplerTank.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picSamplerTank.Image = global::DataLogger.Properties.Resources.SamplerTankerWater;
            this.picSamplerTank.Location = new System.Drawing.Point(110, 31);
            this.picSamplerTank.Name = "picSamplerTank";
            this.picSamplerTank.Size = new System.Drawing.Size(12, 26);
            this.picSamplerTank.TabIndex = 31;
            this.picSamplerTank.TabStop = false;
            this.picSamplerTank.Visible = false;
            // 
            // btnLanguage
            // 
            this.btnLanguage.BackColor = System.Drawing.Color.Transparent;
            this.btnLanguage.BackgroundImage = global::DataLogger.Properties.Resources.Flag_of_Vietnam_43x32;
            this.btnLanguage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLanguage.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnLanguage.FlatAppearance.BorderSize = 0;
            this.btnLanguage.Location = new System.Drawing.Point(-365, 29);
            this.btnLanguage.Name = "btnLanguage";
            this.btnLanguage.Size = new System.Drawing.Size(43, 16);
            this.btnLanguage.TabIndex = 50;
            this.btnLanguage.UseVisualStyleBackColor = false;
            this.btnLanguage.Visible = false;
            this.btnLanguage.Click += new System.EventHandler(this.btnLanguage_Click);
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::DataLogger.Properties.Resources.WaterLevel;
            this.pictureBox5.Location = new System.Drawing.Point(-107, 34);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(64, 21);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 32;
            this.pictureBox5.TabStop = false;
            this.pictureBox5.Visible = false;
            // 
            // listen
            // 
            this.listen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(71)))), ((int)(((byte)(117)))));
            this.listen.BoderWidthActive = 2;
            this.listen.BoderWidthNormal = 1;
            this.listen.ColorBoderActive = System.Drawing.Color.Green;
            this.listen.ColorBoderHover = System.Drawing.Color.Gray;
            this.listen.ColorBoderNormal = System.Drawing.Color.Silver;
            this.listen.IsActive = false;
            this.listen.Location = new System.Drawing.Point(-487, 26);
            this.listen.Margin = new System.Windows.Forms.Padding(4);
            this.listen.Name = "listen";
            this.listen.Size = new System.Drawing.Size(60, 10);
            this.listen.TabIndex = 68;
            this.listen.ToolTipHint = "";
            this.listen.Visible = false;
            this.listen.Click += new System.EventHandler(this.listen_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Transparent;
            this.button5.BackgroundImage = global::DataLogger.Properties.Resources.logo;
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button5.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(141)))), ((int)(((byte)(196)))));
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Location = new System.Drawing.Point(-226, 31);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(47, 22);
            this.button5.TabIndex = 67;
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Visible = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Transparent;
            this.button4.BackgroundImage = global::DataLogger.Properties.Resources.clock;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(141)))), ((int)(((byte)(196)))));
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Location = new System.Drawing.Point(-173, 26);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(37, 24);
            this.button4.TabIndex = 66;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Visible = false;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(113)))), ((int)(((byte)(170)))));
            this.panel5.Controls.Add(this.lblAutorSampler);
            this.panel5.Location = new System.Drawing.Point(9, 92);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(154, 37);
            this.panel5.TabIndex = 68;
            // 
            // lblAutorSampler
            // 
            this.lblAutorSampler.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblAutorSampler.AutoSize = true;
            this.lblAutorSampler.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(113)))), ((int)(((byte)(170)))));
            this.lblAutorSampler.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAutorSampler.ForeColor = System.Drawing.Color.White;
            this.lblAutorSampler.Location = new System.Drawing.Point(20, 4);
            this.lblAutorSampler.Name = "lblAutorSampler";
            this.lblAutorSampler.Padding = new System.Windows.Forms.Padding(5);
            this.lblAutorSampler.Size = new System.Drawing.Size(120, 29);
            this.lblAutorSampler.TabIndex = 4;
            this.lblAutorSampler.Text = "Auto Sampler";
            // 
            // panel3
            // 
            this.panel3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BackgroundImage = global::DataLogger.Properties.Resources.Auto_Samper;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.btnAutoSamplerTesting);
            this.panel3.Controls.Add(this.btnAutoSamplerHistoryData);
            this.panel3.Controls.Add(this.picAutoSamplerStatus);
            this.panel3.Controls.Add(this.pnbottlePosition);
            this.panel3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel3.Location = new System.Drawing.Point(7, 105);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(190, 275);
            this.panel3.TabIndex = 0;
            // 
            // btnAutoSamplerTesting
            // 
            this.btnAutoSamplerTesting.BackColor = System.Drawing.Color.Transparent;
            this.btnAutoSamplerTesting.BackgroundImage = global::DataLogger.Properties.Resources._9;
            this.btnAutoSamplerTesting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAutoSamplerTesting.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnAutoSamplerTesting.FlatAppearance.BorderSize = 0;
            this.btnAutoSamplerTesting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAutoSamplerTesting.Location = new System.Drawing.Point(132, 218);
            this.btnAutoSamplerTesting.Name = "btnAutoSamplerTesting";
            this.btnAutoSamplerTesting.Size = new System.Drawing.Size(48, 48);
            this.btnAutoSamplerTesting.TabIndex = 50;
            this.btnAutoSamplerTesting.UseVisualStyleBackColor = false;
            this.btnAutoSamplerTesting.Click += new System.EventHandler(this.btnAutoSamplerTesting_Click);
            this.btnAutoSamplerTesting.MouseHover += new System.EventHandler(this.btnAutoSamplerTesting_MouseHover);
            // 
            // btnAutoSamplerHistoryData
            // 
            this.btnAutoSamplerHistoryData.BackColor = System.Drawing.Color.Transparent;
            this.btnAutoSamplerHistoryData.BackgroundImage = global::DataLogger.Properties.Resources._8;
            this.btnAutoSamplerHistoryData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAutoSamplerHistoryData.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnAutoSamplerHistoryData.FlatAppearance.BorderSize = 0;
            this.btnAutoSamplerHistoryData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAutoSamplerHistoryData.Location = new System.Drawing.Point(76, 218);
            this.btnAutoSamplerHistoryData.Name = "btnAutoSamplerHistoryData";
            this.btnAutoSamplerHistoryData.Size = new System.Drawing.Size(48, 48);
            this.btnAutoSamplerHistoryData.TabIndex = 50;
            this.btnAutoSamplerHistoryData.UseVisualStyleBackColor = false;
            this.btnAutoSamplerHistoryData.Click += new System.EventHandler(this.btnAutoSamplerHistoryData_Click);
            // 
            // picAutoSamplerStatus
            // 
            this.picAutoSamplerStatus.BackColor = System.Drawing.Color.Transparent;
            this.picAutoSamplerStatus.BackgroundImage = global::DataLogger.Properties.Resources.Normal;
            this.picAutoSamplerStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picAutoSamplerStatus.Location = new System.Drawing.Point(11, 217);
            this.picAutoSamplerStatus.Name = "picAutoSamplerStatus";
            this.picAutoSamplerStatus.Size = new System.Drawing.Size(48, 48);
            this.picAutoSamplerStatus.TabIndex = 7;
            this.picAutoSamplerStatus.TabStop = false;
            this.picAutoSamplerStatus.MouseHover += new System.EventHandler(this.picAutoSamplerStatus_MouseHover);
            // 
            // pnbottlePosition
            // 
            this.pnbottlePosition.BackColor = System.Drawing.Color.Transparent;
            this.pnbottlePosition.BackgroundImage = global::DataLogger.Properties.Resources.model;
            this.pnbottlePosition.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnbottlePosition.Controls.Add(this.pictureBox4);
            this.pnbottlePosition.Controls.Add(this.txtAutoSamplerTemp);
            this.pnbottlePosition.Location = new System.Drawing.Point(4, 22);
            this.pnbottlePosition.Name = "pnbottlePosition";
            this.pnbottlePosition.Size = new System.Drawing.Size(178, 178);
            this.pnbottlePosition.TabIndex = 10;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox4.BackgroundImage")));
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox4.Location = new System.Drawing.Point(104, 77);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(27, 27);
            this.pictureBox4.TabIndex = 66;
            this.pictureBox4.TabStop = false;
            // 
            // txtAutoSamplerTemp
            // 
            this.txtAutoSamplerTemp.BackColor = System.Drawing.Color.White;
            this.txtAutoSamplerTemp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAutoSamplerTemp.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAutoSamplerTemp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(160)))), ((int)(((byte)(186)))));
            this.txtAutoSamplerTemp.Location = new System.Drawing.Point(68, 82);
            this.txtAutoSamplerTemp.Name = "txtAutoSamplerTemp";
            this.txtAutoSamplerTemp.ReadOnly = true;
            this.txtAutoSamplerTemp.Size = new System.Drawing.Size(30, 20);
            this.txtAutoSamplerTemp.TabIndex = 67;
            this.txtAutoSamplerTemp.Text = "---";
            this.txtAutoSamplerTemp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // pnLeftSide
            // 
            this.pnLeftSide.AutoSize = true;
            this.pnLeftSide.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(95)))), ((int)(((byte)(133)))));
            this.pnLeftSide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnLeftSide.Controls.Add(this.vprgMonthlyReport);
            this.pnLeftSide.Controls.Add(this.btnMaintenance);
            this.pnLeftSide.Controls.Add(this.btnMonthlyReport);
            this.pnLeftSide.Controls.Add(this.btnSetting);
            this.pnLeftSide.Controls.Add(this.btnUsers);
            this.pnLeftSide.Controls.Add(this.btnAllHistory);
            this.pnLeftSide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnLeftSide.Location = new System.Drawing.Point(0, 44);
            this.pnLeftSide.Margin = new System.Windows.Forms.Padding(0);
            this.pnLeftSide.Name = "pnLeftSide";
            this.tableLayoutPanel1.SetRowSpan(this.pnLeftSide, 2);
            this.pnLeftSide.Size = new System.Drawing.Size(76, 499);
            this.pnLeftSide.TabIndex = 1;
            // 
            // vprgMonthlyReport
            // 
            this.vprgMonthlyReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(95)))), ((int)(((byte)(133)))));
            this.vprgMonthlyReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.vprgMonthlyReport.BorderStyle = VerticalProgressBar.BorderStyles.None;
            this.vprgMonthlyReport.Color = System.Drawing.Color.Maroon;
            this.vprgMonthlyReport.Dock = System.Windows.Forms.DockStyle.Top;
            this.vprgMonthlyReport.Location = new System.Drawing.Point(0, 70);
            this.vprgMonthlyReport.Maximum = 100;
            this.vprgMonthlyReport.Minimum = 0;
            this.vprgMonthlyReport.Name = "vprgMonthlyReport";
            this.vprgMonthlyReport.Size = new System.Drawing.Size(76, 123);
            this.vprgMonthlyReport.Step = 1;
            this.vprgMonthlyReport.Style = VerticalProgressBar.Styles.Solid;
            this.vprgMonthlyReport.TabIndex = 67;
            this.vprgMonthlyReport.Value = 90;
            this.vprgMonthlyReport.Visible = false;
            this.vprgMonthlyReport.Load += new System.EventHandler(this.vprgMonthlyReport_Load);
            // 
            // btnMaintenance
            // 
            this.btnMaintenance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(95)))), ((int)(((byte)(133)))));
            this.btnMaintenance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMaintenance.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnMaintenance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(95)))), ((int)(((byte)(133)))));
            this.btnMaintenance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaintenance.Image = global::DataLogger.Properties.Resources.world_clock;
            this.btnMaintenance.Location = new System.Drawing.Point(0, 243);
            this.btnMaintenance.Name = "btnMaintenance";
            this.btnMaintenance.Size = new System.Drawing.Size(76, 64);
            this.btnMaintenance.TabIndex = 50;
            this.btnMaintenance.UseVisualStyleBackColor = false;
            this.btnMaintenance.Click += new System.EventHandler(this.btnMaintenance_Click);
            // 
            // btnMonthlyReport
            // 
            this.btnMonthlyReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(67)))), ((int)(((byte)(96)))));
            this.btnMonthlyReport.BackgroundImage = global::DataLogger.Properties.Resources.MonthlyReportButton;
            this.btnMonthlyReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMonthlyReport.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMonthlyReport.FlatAppearance.BorderSize = 0;
            this.btnMonthlyReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnMonthlyReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMonthlyReport.Location = new System.Drawing.Point(0, 0);
            this.btnMonthlyReport.Name = "btnMonthlyReport";
            this.btnMonthlyReport.Size = new System.Drawing.Size(76, 70);
            this.btnMonthlyReport.TabIndex = 49;
            this.btnMonthlyReport.UseVisualStyleBackColor = false;
            this.btnMonthlyReport.Click += new System.EventHandler(this.btnMonthlyReport_Click);
            // 
            // btnSetting
            // 
            this.btnSetting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(95)))), ((int)(((byte)(133)))));
            this.btnSetting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSetting.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSetting.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(95)))), ((int)(((byte)(133)))));
            this.btnSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetting.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnSetting.Image = global::DataLogger.Properties.Resources.applications_system_60x60;
            this.btnSetting.Location = new System.Drawing.Point(0, 307);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(76, 64);
            this.btnSetting.TabIndex = 5;
            this.btnSetting.UseVisualStyleBackColor = false;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // btnUsers
            // 
            this.btnUsers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(95)))), ((int)(((byte)(133)))));
            this.btnUsers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUsers.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnUsers.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(95)))), ((int)(((byte)(133)))));
            this.btnUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUsers.Image = global::DataLogger.Properties.Resources.user;
            this.btnUsers.Location = new System.Drawing.Point(0, 371);
            this.btnUsers.Name = "btnUsers";
            this.btnUsers.Size = new System.Drawing.Size(76, 64);
            this.btnUsers.TabIndex = 4;
            this.btnUsers.UseVisualStyleBackColor = false;
            this.btnUsers.Click += new System.EventHandler(this.btnUsers_Click);
            // 
            // btnAllHistory
            // 
            this.btnAllHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(95)))), ((int)(((byte)(133)))));
            this.btnAllHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAllHistory.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnAllHistory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(95)))), ((int)(((byte)(133)))));
            this.btnAllHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAllHistory.Image = global::DataLogger.Properties.Resources.maintenance;
            this.btnAllHistory.Location = new System.Drawing.Point(0, 435);
            this.btnAllHistory.Name = "btnAllHistory";
            this.btnAllHistory.Size = new System.Drawing.Size(76, 64);
            this.btnAllHistory.TabIndex = 3;
            this.btnAllHistory.UseVisualStyleBackColor = false;
            this.btnAllHistory.Click += new System.EventHandler(this.btnAllHistory_Click);
            // 
            // pnHeader
            // 
            this.pnHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(56)))), ((int)(((byte)(60)))));
            this.tableLayoutPanel1.SetColumnSpan(this.pnHeader, 3);
            this.pnHeader.Controls.Add(this.label9);
            this.pnHeader.Controls.Add(this.label4);
            this.pnHeader.Controls.Add(this.label3);
            this.pnHeader.Controls.Add(this.panel18);
            this.pnHeader.Controls.Add(this.lblHeadingTime);
            this.pnHeader.Location = new System.Drawing.Point(0, 0);
            this.pnHeader.Margin = new System.Windows.Forms.Padding(0);
            this.pnHeader.Name = "pnHeader";
            this.pnHeader.Size = new System.Drawing.Size(780, 44);
            this.pnHeader.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label9.Location = new System.Drawing.Point(45, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(273, 31);
            this.label9.TabIndex = 70;
            this.label9.Text = "STATION";
            // 
            // label4
            // 
            this.label4.Image = global::DataLogger.Properties.Resources.clock;
            this.label4.Location = new System.Drawing.Point(401, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 37);
            this.label4.TabIndex = 69;
            // 
            // label3
            // 
            this.label3.Image = global::DataLogger.Properties.Resources.logo;
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Location = new System.Drawing.Point(4, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 37);
            this.label3.TabIndex = 68;
            // 
            // panel18
            // 
            this.panel18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(39)))));
            this.panel18.Controls.Add(this.btnLoginLogout);
            this.panel18.Controls.Add(this.lblLoginDisplayName);
            this.panel18.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel18.Location = new System.Drawing.Point(607, 0);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(173, 44);
            this.panel18.TabIndex = 65;
            // 
            // btnLoginLogout
            // 
            this.btnLoginLogout.BackColor = System.Drawing.Color.Transparent;
            this.btnLoginLogout.BackgroundImage = global::DataLogger.Properties.Resources.login;
            this.btnLoginLogout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLoginLogout.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(141)))), ((int)(((byte)(196)))));
            this.btnLoginLogout.FlatAppearance.BorderSize = 0;
            this.btnLoginLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoginLogout.Location = new System.Drawing.Point(133, 5);
            this.btnLoginLogout.Name = "btnLoginLogout";
            this.btnLoginLogout.Size = new System.Drawing.Size(37, 37);
            this.btnLoginLogout.TabIndex = 64;
            this.btnLoginLogout.UseVisualStyleBackColor = false;
            this.btnLoginLogout.Click += new System.EventHandler(this.btnLoginLogout_Click);
            // 
            // lblLoginDisplayName
            // 
            this.lblLoginDisplayName.AutoSize = true;
            this.lblLoginDisplayName.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoginDisplayName.ForeColor = System.Drawing.Color.White;
            this.lblLoginDisplayName.Location = new System.Drawing.Point(8, 15);
            this.lblLoginDisplayName.Name = "lblLoginDisplayName";
            this.lblLoginDisplayName.Size = new System.Drawing.Size(120, 17);
            this.lblLoginDisplayName.TabIndex = 51;
            this.lblLoginDisplayName.Text = "Welcome, Guest!";
            // 
            // lblHeadingTime
            // 
            this.lblHeadingTime.AutoSize = true;
            this.lblHeadingTime.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeadingTime.ForeColor = System.Drawing.Color.White;
            this.lblHeadingTime.Location = new System.Drawing.Point(450, 14);
            this.lblHeadingTime.Name = "lblHeadingTime";
            this.lblHeadingTime.Size = new System.Drawing.Size(143, 17);
            this.lblHeadingTime.TabIndex = 2;
            this.lblHeadingTime.Text = "25-09-2015 12:11:19";
            // 
            // panel24
            // 
            this.panel24.Controls.Add(this.panel12);
            this.panel24.Controls.Add(this.panel8);
            this.panel24.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel24.Location = new System.Drawing.Point(304, 47);
            this.panel24.Name = "panel24";
            this.panel24.Size = new System.Drawing.Size(473, 411);
            this.panel24.TabIndex = 72;
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(113)))), ((int)(((byte)(170)))));
            this.panel12.Controls.Add(this.label1);
            this.panel12.Location = new System.Drawing.Point(8, 2);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(126, 37);
            this.panel12.TabIndex = 63;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(20, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Analyzer";
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Transparent;
            this.panel8.BackgroundImage = global::DataLogger.Properties.Resources.Aplyzer;
            this.panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel8.Controls.Add(this.panel11);
            this.panel8.Controls.Add(this.panel10);
            this.panel8.Controls.Add(this.panel7);
            this.panel8.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel8.Location = new System.Drawing.Point(7, 12);
            this.panel8.Margin = new System.Windows.Forms.Padding(10);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(456, 402);
            this.panel8.TabIndex = 0;
            this.panel8.Paint += new System.Windows.Forms.PaintEventHandler(this.panel8_Paint);
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.label8);
            this.panel11.Controls.Add(this.vprgTNValue);
            this.panel11.Controls.Add(this.btnTN1Hour);
            this.panel11.Controls.Add(this.btnTN5Minute);
            this.panel11.Controls.Add(this.btnTNHistoryData);
            this.panel11.Controls.Add(this.txtTNValue);
            this.panel11.Controls.Add(this.label52);
            this.panel11.Controls.Add(this.btnTNCalibrate);
            this.panel11.Controls.Add(this.picTNStatus);
            this.panel11.Controls.Add(this.label53);
            this.panel11.Location = new System.Drawing.Point(12, 280);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(436, 110);
            this.panel11.TabIndex = 61;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(100)))), ((int)(((byte)(98)))));
            this.label8.Location = new System.Drawing.Point(52, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 22);
            this.label8.TabIndex = 68;
            this.label8.Text = "MONI-SS";
            // 
            // vprgTNValue
            // 
            this.vprgTNValue.BackColor = System.Drawing.Color.White;
            this.vprgTNValue.BorderStyle = VerticalProgressBar.BorderStyles.None;
            this.vprgTNValue.Color = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(228)))), ((int)(((byte)(255)))));
            this.vprgTNValue.Location = new System.Drawing.Point(12, 1);
            this.vprgTNValue.Maximum = 100;
            this.vprgTNValue.Minimum = 0;
            this.vprgTNValue.Name = "vprgTNValue";
            this.vprgTNValue.Size = new System.Drawing.Size(20, 95);
            this.vprgTNValue.Step = 1;
            this.vprgTNValue.Style = VerticalProgressBar.Styles.Solid;
            this.vprgTNValue.TabIndex = 67;
            this.vprgTNValue.Value = 90;
            // 
            // btnTN1Hour
            // 
            this.btnTN1Hour.BackColor = System.Drawing.Color.Transparent;
            this.btnTN1Hour.BackgroundImage = global::DataLogger.Properties.Resources._11;
            this.btnTN1Hour.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTN1Hour.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnTN1Hour.FlatAppearance.BorderSize = 0;
            this.btnTN1Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTN1Hour.Location = new System.Drawing.Point(375, 11);
            this.btnTN1Hour.Name = "btnTN1Hour";
            this.btnTN1Hour.Size = new System.Drawing.Size(42, 42);
            this.btnTN1Hour.TabIndex = 50;
            this.btnTN1Hour.UseVisualStyleBackColor = false;
            this.btnTN1Hour.Click += new System.EventHandler(this.btnTN1Hour_Click);
            // 
            // btnTN5Minute
            // 
            this.btnTN5Minute.BackColor = System.Drawing.Color.Transparent;
            this.btnTN5Minute.BackgroundImage = global::DataLogger.Properties.Resources._10;
            this.btnTN5Minute.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTN5Minute.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnTN5Minute.FlatAppearance.BorderSize = 0;
            this.btnTN5Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTN5Minute.Location = new System.Drawing.Point(315, 11);
            this.btnTN5Minute.Name = "btnTN5Minute";
            this.btnTN5Minute.Size = new System.Drawing.Size(42, 42);
            this.btnTN5Minute.TabIndex = 50;
            this.btnTN5Minute.UseVisualStyleBackColor = false;
            this.btnTN5Minute.Click += new System.EventHandler(this.btnTN5Minute_Click);
            // 
            // btnTNHistoryData
            // 
            this.btnTNHistoryData.BackColor = System.Drawing.Color.Transparent;
            this.btnTNHistoryData.BackgroundImage = global::DataLogger.Properties.Resources._8;
            this.btnTNHistoryData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTNHistoryData.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnTNHistoryData.FlatAppearance.BorderSize = 0;
            this.btnTNHistoryData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTNHistoryData.Location = new System.Drawing.Point(315, 59);
            this.btnTNHistoryData.Name = "btnTNHistoryData";
            this.btnTNHistoryData.Size = new System.Drawing.Size(40, 40);
            this.btnTNHistoryData.TabIndex = 50;
            this.btnTNHistoryData.UseVisualStyleBackColor = false;
            this.btnTNHistoryData.Click += new System.EventHandler(this.btnTNHistoryData_Click);
            // 
            // txtTNValue
            // 
            this.txtTNValue.BackColor = System.Drawing.Color.White;
            this.txtTNValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTNValue.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTNValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(160)))), ((int)(((byte)(186)))));
            this.txtTNValue.Location = new System.Drawing.Point(88, 70);
            this.txtTNValue.Name = "txtTNValue";
            this.txtTNValue.ReadOnly = true;
            this.txtTNValue.Size = new System.Drawing.Size(64, 22);
            this.txtTNValue.TabIndex = 51;
            this.txtTNValue.Text = "7.20";
            this.txtTNValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.ForeColor = System.Drawing.Color.Black;
            this.label52.Location = new System.Drawing.Point(168, 73);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(43, 19);
            this.label52.TabIndex = 60;
            this.label52.Text = "mg/L";
            this.label52.Click += new System.EventHandler(this.label52_Click);
            // 
            // btnTNCalibrate
            // 
            this.btnTNCalibrate.BackColor = System.Drawing.Color.Transparent;
            this.btnTNCalibrate.BackgroundImage = global::DataLogger.Properties.Resources._12;
            this.btnTNCalibrate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTNCalibrate.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnTNCalibrate.FlatAppearance.BorderSize = 0;
            this.btnTNCalibrate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTNCalibrate.Location = new System.Drawing.Point(375, 59);
            this.btnTNCalibrate.Name = "btnTNCalibrate";
            this.btnTNCalibrate.Size = new System.Drawing.Size(40, 40);
            this.btnTNCalibrate.TabIndex = 50;
            this.btnTNCalibrate.UseVisualStyleBackColor = false;
            this.btnTNCalibrate.Visible = false;
            this.btnTNCalibrate.Click += new System.EventHandler(this.btnTNCalibrate_Click);
            // 
            // picTNStatus
            // 
            this.picTNStatus.BackColor = System.Drawing.Color.Transparent;
            this.picTNStatus.BackgroundImage = global::DataLogger.Properties.Resources.Normal_status;
            this.picTNStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picTNStatus.Location = new System.Drawing.Point(225, 38);
            this.picTNStatus.Name = "picTNStatus";
            this.picTNStatus.Size = new System.Drawing.Size(42, 42);
            this.picTNStatus.TabIndex = 59;
            this.picTNStatus.TabStop = false;
            this.picTNStatus.MouseHover += new System.EventHandler(this.picTNStatus_MouseHover);
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.ForeColor = System.Drawing.Color.Black;
            this.label53.Location = new System.Drawing.Point(58, 73);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(24, 17);
            this.label53.TabIndex = 45;
            this.label53.Text = "SS";
            this.label53.Click += new System.EventHandler(this.label53_Click);
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.label7);
            this.panel10.Controls.Add(this.vprgTPValue);
            this.panel10.Controls.Add(this.label49);
            this.panel10.Controls.Add(this.btnTP5Minute);
            this.panel10.Controls.Add(this.btnTPCalibrate);
            this.panel10.Controls.Add(this.btnTP1Hour);
            this.panel10.Controls.Add(this.picTPStatus);
            this.panel10.Controls.Add(this.txtTPValue);
            this.panel10.Controls.Add(this.btnTPHistoryData);
            this.panel10.Controls.Add(this.label47);
            this.panel10.Location = new System.Drawing.Point(12, 162);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(436, 110);
            this.panel10.TabIndex = 62;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(100)))), ((int)(((byte)(98)))));
            this.label7.Location = new System.Drawing.Point(52, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 22);
            this.label7.TabIndex = 65;
            this.label7.Text = "MONI-pH";
            // 
            // vprgTPValue
            // 
            this.vprgTPValue.BackColor = System.Drawing.Color.White;
            this.vprgTPValue.BorderStyle = VerticalProgressBar.BorderStyles.None;
            this.vprgTPValue.Color = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(228)))), ((int)(((byte)(255)))));
            this.vprgTPValue.Location = new System.Drawing.Point(12, 3);
            this.vprgTPValue.Maximum = 100;
            this.vprgTPValue.Minimum = 0;
            this.vprgTPValue.Name = "vprgTPValue";
            this.vprgTPValue.Size = new System.Drawing.Size(20, 96);
            this.vprgTPValue.Step = 1;
            this.vprgTPValue.Style = VerticalProgressBar.Styles.Solid;
            this.vprgTPValue.TabIndex = 64;
            this.vprgTPValue.Value = 10;
            this.vprgTPValue.Load += new System.EventHandler(this.vprgTPValue_Load);
            // 
            // label49
            // 
            this.label49.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.ForeColor = System.Drawing.Color.Black;
            this.label49.Location = new System.Drawing.Point(58, 66);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(43, 22);
            this.label49.TabIndex = 45;
            this.label49.Text = "pH";
            this.label49.Click += new System.EventHandler(this.label49_Click);
            // 
            // btnTP5Minute
            // 
            this.btnTP5Minute.BackColor = System.Drawing.Color.Transparent;
            this.btnTP5Minute.BackgroundImage = global::DataLogger.Properties.Resources._10;
            this.btnTP5Minute.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTP5Minute.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnTP5Minute.FlatAppearance.BorderSize = 0;
            this.btnTP5Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTP5Minute.Location = new System.Drawing.Point(315, 9);
            this.btnTP5Minute.Name = "btnTP5Minute";
            this.btnTP5Minute.Size = new System.Drawing.Size(42, 42);
            this.btnTP5Minute.TabIndex = 50;
            this.btnTP5Minute.UseVisualStyleBackColor = false;
            this.btnTP5Minute.Click += new System.EventHandler(this.btnTP5Minute_Click);
            // 
            // btnTPCalibrate
            // 
            this.btnTPCalibrate.BackColor = System.Drawing.Color.Transparent;
            this.btnTPCalibrate.BackgroundImage = global::DataLogger.Properties.Resources._12;
            this.btnTPCalibrate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTPCalibrate.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnTPCalibrate.FlatAppearance.BorderSize = 0;
            this.btnTPCalibrate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTPCalibrate.Location = new System.Drawing.Point(375, 61);
            this.btnTPCalibrate.Name = "btnTPCalibrate";
            this.btnTPCalibrate.Size = new System.Drawing.Size(40, 40);
            this.btnTPCalibrate.TabIndex = 50;
            this.btnTPCalibrate.UseVisualStyleBackColor = false;
            this.btnTPCalibrate.Visible = false;
            this.btnTPCalibrate.Click += new System.EventHandler(this.btnTPCalibrate_Click);
            // 
            // btnTP1Hour
            // 
            this.btnTP1Hour.BackColor = System.Drawing.Color.Transparent;
            this.btnTP1Hour.BackgroundImage = global::DataLogger.Properties.Resources._11;
            this.btnTP1Hour.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTP1Hour.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnTP1Hour.FlatAppearance.BorderSize = 0;
            this.btnTP1Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTP1Hour.Location = new System.Drawing.Point(375, 9);
            this.btnTP1Hour.Name = "btnTP1Hour";
            this.btnTP1Hour.Size = new System.Drawing.Size(42, 42);
            this.btnTP1Hour.TabIndex = 50;
            this.btnTP1Hour.UseVisualStyleBackColor = false;
            this.btnTP1Hour.Click += new System.EventHandler(this.btnTP1Hour_Click);
            // 
            // picTPStatus
            // 
            this.picTPStatus.BackColor = System.Drawing.Color.Transparent;
            this.picTPStatus.BackgroundImage = global::DataLogger.Properties.Resources.Normal_status;
            this.picTPStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picTPStatus.Location = new System.Drawing.Point(224, 33);
            this.picTPStatus.Name = "picTPStatus";
            this.picTPStatus.Size = new System.Drawing.Size(42, 42);
            this.picTPStatus.TabIndex = 59;
            this.picTPStatus.TabStop = false;
            this.picTPStatus.Click += new System.EventHandler(this.picTPStatus_Click);
            this.picTPStatus.MouseHover += new System.EventHandler(this.picTPStatus_MouseHover);
            // 
            // txtTPValue
            // 
            this.txtTPValue.BackColor = System.Drawing.Color.White;
            this.txtTPValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTPValue.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTPValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(160)))), ((int)(((byte)(186)))));
            this.txtTPValue.Location = new System.Drawing.Point(95, 66);
            this.txtTPValue.Name = "txtTPValue";
            this.txtTPValue.ReadOnly = true;
            this.txtTPValue.Size = new System.Drawing.Size(57, 22);
            this.txtTPValue.TabIndex = 51;
            this.txtTPValue.Text = "7.20";
            this.txtTPValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTPValue.TextChanged += new System.EventHandler(this.txtTPValue_TextChanged);
            // 
            // btnTPHistoryData
            // 
            this.btnTPHistoryData.BackColor = System.Drawing.Color.Transparent;
            this.btnTPHistoryData.BackgroundImage = global::DataLogger.Properties.Resources._8;
            this.btnTPHistoryData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTPHistoryData.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnTPHistoryData.FlatAppearance.BorderSize = 0;
            this.btnTPHistoryData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTPHistoryData.Location = new System.Drawing.Point(315, 59);
            this.btnTPHistoryData.Name = "btnTPHistoryData";
            this.btnTPHistoryData.Size = new System.Drawing.Size(40, 40);
            this.btnTPHistoryData.TabIndex = 50;
            this.btnTPHistoryData.UseVisualStyleBackColor = false;
            this.btnTPHistoryData.Click += new System.EventHandler(this.btnTPHistoryData_Click);
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.ForeColor = System.Drawing.Color.Black;
            this.label47.Location = new System.Drawing.Point(168, 66);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(43, 19);
            this.label47.TabIndex = 60;
            this.label47.Text = "mg/L";
            this.label47.Visible = false;
            this.label47.Click += new System.EventHandler(this.label47_Click);
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Transparent;
            this.panel7.Controls.Add(this.label5);
            this.panel7.Controls.Add(this.vprgTOCValue);
            this.panel7.Controls.Add(this.txtTOCValue);
            this.panel7.Controls.Add(this.label48);
            this.panel7.Controls.Add(this.picTOCStatus);
            this.panel7.Controls.Add(this.label43);
            this.panel7.Controls.Add(this.btnTOCCalibrate);
            this.panel7.Controls.Add(this.btnTOC5Minute);
            this.panel7.Controls.Add(this.btnTOCHistoryData);
            this.panel7.Controls.Add(this.btnTOC1Hour);
            this.panel7.Location = new System.Drawing.Point(12, 39);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(436, 110);
            this.panel7.TabIndex = 61;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(100)))), ((int)(((byte)(98)))));
            this.label5.Location = new System.Drawing.Point(52, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 22);
            this.label5.TabIndex = 62;
            this.label5.Text = "MONI-TOC";
            // 
            // vprgTOCValue
            // 
            this.vprgTOCValue.BackColor = System.Drawing.Color.White;
            this.vprgTOCValue.BorderStyle = VerticalProgressBar.BorderStyles.None;
            this.vprgTOCValue.Color = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(228)))), ((int)(((byte)(255)))));
            this.vprgTOCValue.Location = new System.Drawing.Point(13, 3);
            this.vprgTOCValue.Maximum = 100;
            this.vprgTOCValue.Minimum = 0;
            this.vprgTOCValue.Name = "vprgTOCValue";
            this.vprgTOCValue.Size = new System.Drawing.Size(20, 104);
            this.vprgTOCValue.Step = 1;
            this.vprgTOCValue.Style = VerticalProgressBar.Styles.Solid;
            this.vprgTOCValue.TabIndex = 61;
            this.vprgTOCValue.Value = 10;
            // 
            // txtTOCValue
            // 
            this.txtTOCValue.BackColor = System.Drawing.Color.White;
            this.txtTOCValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTOCValue.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTOCValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(160)))), ((int)(((byte)(186)))));
            this.txtTOCValue.Location = new System.Drawing.Point(95, 68);
            this.txtTOCValue.Name = "txtTOCValue";
            this.txtTOCValue.ReadOnly = true;
            this.txtTOCValue.Size = new System.Drawing.Size(57, 22);
            this.txtTOCValue.TabIndex = 51;
            this.txtTOCValue.Text = "7.20";
            this.txtTOCValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTOCValue.TextChanged += new System.EventHandler(this.txtTOCValue_TextChanged);
            // 
            // label48
            // 
            this.label48.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.ForeColor = System.Drawing.Color.Black;
            this.label48.Location = new System.Drawing.Point(58, 71);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(43, 15);
            this.label48.TabIndex = 45;
            this.label48.Text = "TOC";
            this.label48.Click += new System.EventHandler(this.label48_Click);
            // 
            // picTOCStatus
            // 
            this.picTOCStatus.BackColor = System.Drawing.Color.Transparent;
            this.picTOCStatus.BackgroundImage = global::DataLogger.Properties.Resources.Normal_status;
            this.picTOCStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picTOCStatus.Location = new System.Drawing.Point(225, 35);
            this.picTOCStatus.Name = "picTOCStatus";
            this.picTOCStatus.Size = new System.Drawing.Size(42, 42);
            this.picTOCStatus.TabIndex = 59;
            this.picTOCStatus.TabStop = false;
            this.picTOCStatus.Click += new System.EventHandler(this.picTOCStatus_Click);
            this.picTOCStatus.MouseHover += new System.EventHandler(this.picTOCStatus_MouseHover);
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.ForeColor = System.Drawing.Color.Black;
            this.label43.Location = new System.Drawing.Point(168, 70);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(43, 19);
            this.label43.TabIndex = 60;
            this.label43.Text = "mg/L";
            this.label43.Click += new System.EventHandler(this.label43_Click);
            // 
            // btnTOCCalibrate
            // 
            this.btnTOCCalibrate.BackColor = System.Drawing.Color.Transparent;
            this.btnTOCCalibrate.BackgroundImage = global::DataLogger.Properties.Resources._12;
            this.btnTOCCalibrate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTOCCalibrate.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnTOCCalibrate.FlatAppearance.BorderSize = 0;
            this.btnTOCCalibrate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTOCCalibrate.Location = new System.Drawing.Point(375, 65);
            this.btnTOCCalibrate.Name = "btnTOCCalibrate";
            this.btnTOCCalibrate.Size = new System.Drawing.Size(40, 40);
            this.btnTOCCalibrate.TabIndex = 50;
            this.btnTOCCalibrate.UseVisualStyleBackColor = false;
            this.btnTOCCalibrate.Click += new System.EventHandler(this.btnTOCCalibrate_Click);
            // 
            // btnTOC5Minute
            // 
            this.btnTOC5Minute.BackColor = System.Drawing.Color.Transparent;
            this.btnTOC5Minute.BackgroundImage = global::DataLogger.Properties.Resources._10;
            this.btnTOC5Minute.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTOC5Minute.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnTOC5Minute.FlatAppearance.BorderSize = 0;
            this.btnTOC5Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTOC5Minute.Location = new System.Drawing.Point(315, 6);
            this.btnTOC5Minute.Name = "btnTOC5Minute";
            this.btnTOC5Minute.Size = new System.Drawing.Size(42, 42);
            this.btnTOC5Minute.TabIndex = 50;
            this.btnTOC5Minute.UseVisualStyleBackColor = false;
            this.btnTOC5Minute.Click += new System.EventHandler(this.btnTOC5Minute_Click);
            // 
            // btnTOCHistoryData
            // 
            this.btnTOCHistoryData.BackColor = System.Drawing.Color.Transparent;
            this.btnTOCHistoryData.BackgroundImage = global::DataLogger.Properties.Resources._8;
            this.btnTOCHistoryData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTOCHistoryData.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnTOCHistoryData.FlatAppearance.BorderSize = 0;
            this.btnTOCHistoryData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTOCHistoryData.Location = new System.Drawing.Point(315, 61);
            this.btnTOCHistoryData.Name = "btnTOCHistoryData";
            this.btnTOCHistoryData.Size = new System.Drawing.Size(40, 40);
            this.btnTOCHistoryData.TabIndex = 50;
            this.btnTOCHistoryData.UseVisualStyleBackColor = false;
            this.btnTOCHistoryData.Click += new System.EventHandler(this.btnTOCHistoryData_Click);
            // 
            // btnTOC1Hour
            // 
            this.btnTOC1Hour.BackColor = System.Drawing.Color.Transparent;
            this.btnTOC1Hour.BackgroundImage = global::DataLogger.Properties.Resources._11;
            this.btnTOC1Hour.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTOC1Hour.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnTOC1Hour.FlatAppearance.BorderSize = 0;
            this.btnTOC1Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTOC1Hour.Location = new System.Drawing.Point(375, 7);
            this.btnTOC1Hour.Name = "btnTOC1Hour";
            this.btnTOC1Hour.Size = new System.Drawing.Size(42, 42);
            this.btnTOC1Hour.TabIndex = 50;
            this.btnTOC1Hour.UseVisualStyleBackColor = false;
            this.btnTOC1Hour.Click += new System.EventHandler(this.btnTOC1Hour_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.4902F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.5098F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 478F));
            this.tableLayoutPanel1.Controls.Add(this.panel23, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel24, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnHeader, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnLeftSide, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel30, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.619239F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.38076F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(780, 543);
            this.tableLayoutPanel1.TabIndex = 69;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // panel23
            // 
            this.panel23.BackgroundImage = global::DataLogger.Properties.Resources.footer;
            this.panel23.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tableLayoutPanel1.SetColumnSpan(this.panel23, 2);
            this.panel23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel23.Location = new System.Drawing.Point(86, 471);
            this.panel23.Margin = new System.Windows.Forms.Padding(10);
            this.panel23.Name = "panel23";
            this.panel23.Size = new System.Drawing.Size(684, 62);
            this.panel23.TabIndex = 73;
            // 
            // frmNewMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(780, 543);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmNewMain";
            this.RightToLeftLayout = true;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Data Logger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmNewMain_FormClosing);
            this.Load += new System.EventHandler(this.frmNewMain_Load);
            this.panel30.ResumeLayout(false);
            this.panel20.ResumeLayout(false);
            this.panel20.PerformLayout();
            this.pnSoftwareInfo.ResumeLayout(false);
            this.pnSoftwareInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox52)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSamplerTank)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picAutoSamplerStatus)).EndInit();
            this.pnbottlePosition.ResumeLayout(false);
            this.pnbottlePosition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.pnLeftSide.ResumeLayout(false);
            this.pnHeader.ResumeLayout(false);
            this.pnHeader.PerformLayout();
            this.panel18.ResumeLayout(false);
            this.panel18.PerformLayout();
            this.panel24.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTNStatus)).EndInit();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTPStatus)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTOCStatus)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        public System.IO.Ports.SerialPort serialPortTN;
        public System.IO.Ports.SerialPort serialPortTP;
        public System.IO.Ports.SerialPort serialPortTOC;
        public System.ComponentModel.BackgroundWorker bgwMonthlyReport;
        public System.IO.Ports.SerialPort serialPortSAMP;
        private System.ComponentModel.BackgroundWorker backgroundWorkerMain;
        public System.Windows.Forms.Panel panel30;
        public System.Windows.Forms.Panel panel5;
        public System.Windows.Forms.Label lblAutorSampler;
        public System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.Button btnAutoSamplerTesting;
        public System.Windows.Forms.Button btnAutoSamplerHistoryData;
        public System.Windows.Forms.PictureBox picAutoSamplerStatus;
        public System.Windows.Forms.Panel pnbottlePosition;
        public System.Windows.Forms.PictureBox pictureBox4;
        public System.Windows.Forms.TextBox txtAutoSamplerTemp;
        public System.Windows.Forms.Panel pnLeftSide;
        public VerticalProgressBar.VerticalProgressBar vprgMonthlyReport;
        public System.Windows.Forms.Button btnMaintenance;
        public System.Windows.Forms.Button btnMonthlyReport;
        public System.Windows.Forms.Button btnSetting;
        public System.Windows.Forms.Button btnUsers;
        public System.Windows.Forms.Button btnAllHistory;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel24;
        public System.Windows.Forms.Panel panel12;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Panel panel8;
        public System.Windows.Forms.Panel panel11;
        public System.Windows.Forms.Label label8;
        public VerticalProgressBar.VerticalProgressBar vprgTNValue;
        public System.Windows.Forms.Button btnTN1Hour;
        public System.Windows.Forms.Button btnTN5Minute;
        public System.Windows.Forms.Button btnTNHistoryData;
        public System.Windows.Forms.TextBox txtTNValue;
        public System.Windows.Forms.Label label52;
        public System.Windows.Forms.Button btnTNCalibrate;
        public System.Windows.Forms.PictureBox picTNStatus;
        public System.Windows.Forms.Label label53;
        public System.Windows.Forms.Panel panel10;
        public System.Windows.Forms.Label label7;
        public VerticalProgressBar.VerticalProgressBar vprgTPValue;
        public System.Windows.Forms.Label label49;
        public System.Windows.Forms.Button btnTP5Minute;
        public System.Windows.Forms.Button btnTPCalibrate;
        public System.Windows.Forms.Button btnTP1Hour;
        public System.Windows.Forms.PictureBox picTPStatus;
        public System.Windows.Forms.TextBox txtTPValue;
        public System.Windows.Forms.Button btnTPHistoryData;
        public System.Windows.Forms.Label label47;
        public System.Windows.Forms.Panel panel7;
        public System.Windows.Forms.Label label5;
        public VerticalProgressBar.VerticalProgressBar vprgTOCValue;
        public System.Windows.Forms.TextBox txtTOCValue;
        public System.Windows.Forms.Label label48;
        public System.Windows.Forms.PictureBox picTOCStatus;
        public System.Windows.Forms.Label label43;
        public System.Windows.Forms.Button btnTOCCalibrate;
        public System.Windows.Forms.Button btnTOC5Minute;
        public System.Windows.Forms.Button btnTOCHistoryData;
        public System.Windows.Forms.Button btnTOC1Hour;
        public System.Windows.Forms.Panel pnHeader;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel18;
        public System.Windows.Forms.Button btnLoginLogout;
        public System.Windows.Forms.Label lblLoginDisplayName;
        public System.Windows.Forms.Label lblHeadingTime;
        private System.Windows.Forms.Panel panel20;
        public System.Windows.Forms.Label lblMainMenuTitle;
        public System.Windows.Forms.Panel pnSoftwareInfo;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label lblSurfaceWaterQuality;
        public System.Windows.Forms.Label lblAutomaticMonitoring;
        public System.Windows.Forms.Label lblThaiNguyenStation;
        public System.Windows.Forms.PictureBox pictureBox3;
        public System.Windows.Forms.PictureBox pictureBox2;
        public System.Windows.Forms.PictureBox pictureBox52;
        public System.Windows.Forms.Button btnExit;
        public System.Windows.Forms.Label lblWaterLevel;
        public System.Windows.Forms.Label lblHeaderNationName;
        public System.Windows.Forms.RichTextBox txtData;
        public System.Windows.Forms.PictureBox picSamplerTank;
        public System.Windows.Forms.Button btnLanguage;
        public System.Windows.Forms.PictureBox pictureBox5;
        public FlatButton listen;
        public System.Windows.Forms.Button button5;
        public System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel panel23;
    }
}