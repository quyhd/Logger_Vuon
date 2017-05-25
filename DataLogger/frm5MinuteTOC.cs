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
    public partial class frm5MinuteTOC : Form
    {
        ResourceManager res_man;    // declare Resource manager to access to specific cultureinfo
        CultureInfo cul;            //declare culture info
        LanguageService lang;

        public data_value obj_data_value { get; set; }
        public frm5MinuteTOC()
        {
            InitializeComponent();
        }

        public frm5MinuteTOC(data_value obj, LanguageService _lang)
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

        private void frm5MinuteTOC_Load(object sender, EventArgs e)
        {
            if (obj_data_value.TOC > -1)
            {
                txtTOC.Text = obj_data_value.TOC.ToString("##0.00");
            }
            else
            {
                txtTOC.Text = "---";
            }
        }
    }
}
