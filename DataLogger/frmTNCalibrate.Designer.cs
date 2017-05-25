namespace DataLogger
{
    partial class frmTNCalibrate
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
            this.txta = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtb = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCalibrate = new System.Windows.Forms.Button();
            this.serialPortTN = new System.IO.Ports.SerialPort(this.components);
            this.lblReCalibration = new System.Windows.Forms.Label();
            this.lblFormula = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txta
            // 
            this.txta.BackColor = System.Drawing.Color.White;
            this.txta.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txta.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(16)))), ((int)(((byte)(33)))));
            this.txta.Location = new System.Drawing.Point(97, 244);
            this.txta.Name = "txta";
            this.txta.Size = new System.Drawing.Size(134, 20);
            this.txta.TabIndex = 70;
            this.txta.Text = "---";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(16)))), ((int)(((byte)(33)))));
            this.label5.Location = new System.Drawing.Point(196, 187);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 19);
            this.label5.TabIndex = 69;
            this.label5.Text = "y = a.x + b";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(16)))), ((int)(((byte)(33)))));
            this.label1.Location = new System.Drawing.Point(35, 244);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 19);
            this.label1.TabIndex = 69;
            this.label1.Text = "a =";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(16)))), ((int)(((byte)(33)))));
            this.label2.Location = new System.Drawing.Point(35, 283);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 19);
            this.label2.TabIndex = 69;
            this.label2.Text = "b =";
            // 
            // txtb
            // 
            this.txtb.BackColor = System.Drawing.Color.White;
            this.txtb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtb.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(16)))), ((int)(((byte)(33)))));
            this.txtb.Location = new System.Drawing.Point(97, 283);
            this.txtb.Name = "txtb";
            this.txtb.Size = new System.Drawing.Size(134, 20);
            this.txtb.TabIndex = 70;
            this.txtb.Text = "---";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(145, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 24);
            this.label3.TabIndex = 69;
            // 
            // txtDate
            // 
            this.txtDate.BackColor = System.Drawing.Color.White;
            this.txtDate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDate.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(113)))), ((int)(((byte)(170)))));
            this.txtDate.Location = new System.Drawing.Point(267, 85);
            this.txtDate.Name = "txtDate";
            this.txtDate.ReadOnly = true;
            this.txtDate.Size = new System.Drawing.Size(106, 20);
            this.txtDate.TabIndex = 70;
            this.txtDate.Text = "12-34-2222";
            this.txtDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTime
            // 
            this.txtTime.BackColor = System.Drawing.Color.White;
            this.txtTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTime.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(113)))), ((int)(((byte)(170)))));
            this.txtTime.Location = new System.Drawing.Point(267, 127);
            this.txtTime.Name = "txtTime";
            this.txtTime.ReadOnly = true;
            this.txtTime.Size = new System.Drawing.Size(88, 20);
            this.txtTime.TabIndex = 70;
            this.txtTime.Text = "12:33:33";
            this.txtTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Tahoma", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(100)))), ((int)(((byte)(98)))));
            this.label4.Location = new System.Drawing.Point(59, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 42);
            this.label4.TabIndex = 71;
            this.label4.Text = "TN";
            // 
            // btnCalibrate
            // 
            this.btnCalibrate.BackColor = System.Drawing.Color.Transparent;
            this.btnCalibrate.BackgroundImage = global::DataLogger.Properties.Resources.Calibaration;
            this.btnCalibrate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCalibrate.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnCalibrate.FlatAppearance.BorderSize = 0;
            this.btnCalibrate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalibrate.Location = new System.Drawing.Point(324, 222);
            this.btnCalibrate.Name = "btnCalibrate";
            this.btnCalibrate.Size = new System.Drawing.Size(70, 70);
            this.btnCalibrate.TabIndex = 72;
            this.btnCalibrate.UseVisualStyleBackColor = false;
            this.btnCalibrate.Click += new System.EventHandler(this.btnCalibrate_Click);
            // 
            // serialPortTN
            // 
            //this.serialPortTN.PortName = "COM101";
            this.serialPortTN.PortName = frmNewMain.TNComname;
            this.serialPortTN.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortTN_DataReceived);
            // 
            // lblReCalibration
            // 
            this.lblReCalibration.AutoSize = true;
            this.lblReCalibration.BackColor = System.Drawing.Color.White;
            this.lblReCalibration.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReCalibration.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(16)))), ((int)(((byte)(33)))));
            this.lblReCalibration.Location = new System.Drawing.Point(306, 297);
            this.lblReCalibration.Name = "lblReCalibration";
            this.lblReCalibration.Size = new System.Drawing.Size(104, 19);
            this.lblReCalibration.TabIndex = 73;
            this.lblReCalibration.Text = "Re-calibration";
            // 
            // lblFormula
            // 
            this.lblFormula.AutoSize = true;
            this.lblFormula.BackColor = System.Drawing.Color.White;
            this.lblFormula.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormula.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(16)))), ((int)(((byte)(33)))));
            this.lblFormula.Location = new System.Drawing.Point(35, 187);
            this.lblFormula.Name = "lblFormula";
            this.lblFormula.Size = new System.Drawing.Size(118, 19);
            this.lblFormula.TabIndex = 74;
            this.lblFormula.Text = "Formula used";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(113)))), ((int)(((byte)(170)))));
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(11, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(127, 34);
            this.label6.TabIndex = 76;
            this.label6.Text = "Calibration Data";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(113)))), ((int)(((byte)(170)))));
            this.label8.Location = new System.Drawing.Point(197, 125);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 22);
            this.label8.TabIndex = 79;
            this.label8.Text = "TIME :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(113)))), ((int)(((byte)(170)))));
            this.label7.Location = new System.Drawing.Point(197, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 22);
            this.label7.TabIndex = 78;
            this.label7.Text = "DATE :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmTNCalibrate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.BackgroundImage = global::DataLogger.Properties.Resources._5_min_Data;
            this.ClientSize = new System.Drawing.Size(432, 332);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblFormula);
            this.Controls.Add(this.lblReCalibration);
            this.Controls.Add(this.btnCalibrate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtb);
            this.Controls.Add(this.txtTime);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.txta);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTNCalibrate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calibration data";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTNCalibrate_FormClosing);
            this.Load += new System.EventHandler(this.frmTNCalibrate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txta;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCalibrate;
        private System.IO.Ports.SerialPort serialPortTN;
        private System.Windows.Forms.Label lblReCalibration;
        private System.Windows.Forms.Label lblFormula;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
    }
}