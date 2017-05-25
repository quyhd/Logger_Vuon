using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLogger.Entities;
using System.Resources;
using System.Reflection;
using System.Globalization;
using DataLogger.Utils;

namespace DataLogger
{
    public partial class frm5MinuteTP : Form
    {
        //ResourceManager res_man;    // declare Resource manager to access to specific cultureinfo
        //CultureInfo cul;            //declare culture info
        LanguageService lang;

        public data_value obj_data_value { get; set; }
        public frm5MinuteTP()
        {
            InitializeComponent();
        }

        public frm5MinuteTP(data_value obj, LanguageService _lang)
        {
            InitializeComponent();
            obj_data_value = obj;
            //res_man = obj_res_man;
            //cul = obj_cul;
            lang = _lang;
            switch_language();
        }
        private void switch_language()
        {
            this.lblHeaderTitle.Text = lang.getText("form_5_minute_title");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm5MinuteTP_Load(object sender, EventArgs e)
        {
            if (obj_data_value.TP > -1)
            {
                txtTP.Text = obj_data_value.TP.ToString("##0.000");
            }
            else
            {
                txtTP.Text = "---";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
