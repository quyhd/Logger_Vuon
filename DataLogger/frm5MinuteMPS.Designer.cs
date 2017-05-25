namespace DataLogger
{
    partial class frm5MinuteMPS
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
            this.btnExit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.txtCond = new System.Windows.Forms.TextBox();
            this.txtTurb = new System.Windows.Forms.TextBox();
            this.txtDO = new System.Windows.Forms.TextBox();
            this.txtTemp = new System.Windows.Forms.TextBox();
            this.txtORP = new System.Windows.Forms.TextBox();
            this.txtpH = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnExit.BackgroundImage = global::DataLogger.Properties.Resources.Shutdown_Box_Red;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.Enabled = false;
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Location = new System.Drawing.Point(184, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(40, 43);
            this.btnExit.TabIndex = 87;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Visible = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(71)))), ((int)(((byte)(117)))));
            this.panel1.Controls.Add(this.lblHeaderTitle);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Location = new System.Drawing.Point(10, 11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(152, 43);
            this.panel1.TabIndex = 104;
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeaderTitle.Location = new System.Drawing.Point(13, 12);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(124, 19);
            this.lblHeaderTitle.TabIndex = 87;
            this.lblHeaderTitle.Text = "5 Minute Data";
            // 
            // txtCond
            // 
            this.txtCond.BackColor = System.Drawing.SystemColors.Window;
            this.txtCond.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCond.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCond.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(160)))), ((int)(((byte)(186)))));
            this.txtCond.Location = new System.Drawing.Point(248, 284);
            this.txtCond.Name = "txtCond";
            this.txtCond.ReadOnly = true;
            this.txtCond.Size = new System.Drawing.Size(74, 23);
            this.txtCond.TabIndex = 103;
            this.txtCond.Text = "---";
            this.txtCond.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTurb
            // 
            this.txtTurb.BackColor = System.Drawing.SystemColors.Window;
            this.txtTurb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTurb.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTurb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(160)))), ((int)(((byte)(186)))));
            this.txtTurb.Location = new System.Drawing.Point(258, 245);
            this.txtTurb.Name = "txtTurb";
            this.txtTurb.ReadOnly = true;
            this.txtTurb.Size = new System.Drawing.Size(64, 23);
            this.txtTurb.TabIndex = 102;
            this.txtTurb.Text = "---";
            this.txtTurb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDO
            // 
            this.txtDO.BackColor = System.Drawing.SystemColors.Window;
            this.txtDO.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDO.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDO.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(160)))), ((int)(((byte)(186)))));
            this.txtDO.Location = new System.Drawing.Point(258, 205);
            this.txtDO.Name = "txtDO";
            this.txtDO.ReadOnly = true;
            this.txtDO.Size = new System.Drawing.Size(64, 23);
            this.txtDO.TabIndex = 101;
            this.txtDO.Text = "---";
            this.txtDO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTemp
            // 
            this.txtTemp.BackColor = System.Drawing.SystemColors.Window;
            this.txtTemp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTemp.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTemp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(160)))), ((int)(((byte)(186)))));
            this.txtTemp.Location = new System.Drawing.Point(258, 169);
            this.txtTemp.Name = "txtTemp";
            this.txtTemp.ReadOnly = true;
            this.txtTemp.Size = new System.Drawing.Size(64, 23);
            this.txtTemp.TabIndex = 100;
            this.txtTemp.Text = "---";
            this.txtTemp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtORP
            // 
            this.txtORP.BackColor = System.Drawing.SystemColors.Window;
            this.txtORP.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtORP.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtORP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(160)))), ((int)(((byte)(186)))));
            this.txtORP.Location = new System.Drawing.Point(258, 133);
            this.txtORP.Name = "txtORP";
            this.txtORP.ReadOnly = true;
            this.txtORP.Size = new System.Drawing.Size(64, 23);
            this.txtORP.TabIndex = 99;
            this.txtORP.Text = "---";
            this.txtORP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtpH
            // 
            this.txtpH.BackColor = System.Drawing.SystemColors.Window;
            this.txtpH.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtpH.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpH.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(160)))), ((int)(((byte)(186)))));
            this.txtpH.Location = new System.Drawing.Point(258, 87);
            this.txtpH.Name = "txtpH";
            this.txtpH.ReadOnly = true;
            this.txtpH.Size = new System.Drawing.Size(64, 23);
            this.txtpH.TabIndex = 98;
            this.txtpH.Text = "---";
            this.txtpH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(160)))), ((int)(((byte)(186)))));
            this.label4.Location = new System.Drawing.Point(338, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 19);
            this.label4.TabIndex = 96;
            this.label4.Text = "°C";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(160)))), ((int)(((byte)(186)))));
            this.label3.Location = new System.Drawing.Point(338, 209);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 19);
            this.label3.TabIndex = 97;
            this.label3.Text = "mg/ L";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(160)))), ((int)(((byte)(186)))));
            this.label2.Location = new System.Drawing.Point(338, 249);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 19);
            this.label2.TabIndex = 95;
            this.label2.Text = "NTU";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(160)))), ((int)(((byte)(186)))));
            this.label5.Location = new System.Drawing.Point(338, 290);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 19);
            this.label5.TabIndex = 94;
            this.label5.Text = "mS/cm";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.BackColor = System.Drawing.Color.White;
            this.label40.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(53)))), ((int)(((byte)(56)))));
            this.label40.Location = new System.Drawing.Point(196, 286);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(30, 19);
            this.label40.TabIndex = 93;
            this.label40.Text = "EC";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.BackColor = System.Drawing.Color.White;
            this.label41.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(53)))), ((int)(((byte)(56)))));
            this.label41.Location = new System.Drawing.Point(196, 246);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(40, 19);
            this.label41.TabIndex = 92;
            this.label41.Text = "TSS";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.BackColor = System.Drawing.Color.White;
            this.label38.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(53)))), ((int)(((byte)(56)))));
            this.label38.Location = new System.Drawing.Point(196, 209);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(33, 19);
            this.label38.TabIndex = 91;
            this.label38.Text = "DO";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.BackColor = System.Drawing.Color.White;
            this.label39.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(53)))), ((int)(((byte)(56)))));
            this.label39.Location = new System.Drawing.Point(196, 171);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(55, 19);
            this.label39.TabIndex = 90;
            this.label39.Text = "Temp";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(100)))), ((int)(((byte)(98)))));
            this.label1.Location = new System.Drawing.Point(45, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 35);
            this.label1.TabIndex = 88;
            this.label1.Text = "MPS";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.BackColor = System.Drawing.Color.White;
            this.label37.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(53)))), ((int)(((byte)(56)))));
            this.label37.Location = new System.Drawing.Point(196, 133);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(44, 19);
            this.label37.TabIndex = 89;
            this.label37.Text = "ORP";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.BackColor = System.Drawing.Color.White;
            this.label36.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(53)))), ((int)(((byte)(56)))));
            this.label36.Location = new System.Drawing.Point(196, 90);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(31, 19);
            this.label36.TabIndex = 87;
            this.label36.Text = "pH";
            // 
            // frm5MinuteMPS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.BackgroundImage = global::DataLogger.Properties.Resources._5_min_Data;
            this.ClientSize = new System.Drawing.Size(430, 330);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtCond);
            this.Controls.Add(this.txtTurb);
            this.Controls.Add(this.txtDO);
            this.Controls.Add(this.txtTemp);
            this.Controls.Add(this.txtORP);
            this.Controls.Add(this.txtpH);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label40);
            this.Controls.Add(this.label41);
            this.Controls.Add(this.label38);
            this.Controls.Add(this.label39);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label37);
            this.Controls.Add(this.label36);
            this.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm5MinuteMPS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.TextBox txtCond;
        private System.Windows.Forms.TextBox txtTurb;
        private System.Windows.Forms.TextBox txtDO;
        private System.Windows.Forms.TextBox txtTemp;
        private System.Windows.Forms.TextBox txtORP;
        private System.Windows.Forms.TextBox txtpH;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label36;


    }
}