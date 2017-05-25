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
    public partial class frm5MinuteMPS : Form
    {
        //ResourceManager res_man;    // declare Resource manager to access to specific cultureinfo
        //CultureInfo cul;            //declare culture info
        LanguageService lang;
        public data_value obj_data_value { get; set; }
        public frm5MinuteMPS()
        {
            InitializeComponent();
        }

        public frm5MinuteMPS(data_value obj, LanguageService _lang)
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


        private void Form1_Load(object sender, EventArgs e)
        {
            string temp = "";
            try
            {

                if (obj_data_value.MPS_EC > -1)
                {
                    txtCond.Text = obj_data_value.MPS_EC.ToString("##0.00");
                }
                else
                {
                    txtCond.Text = "---";
                }
                if (obj_data_value.MPS_DO > -1)
                {
                    txtDO.Text = obj_data_value.MPS_DO.ToString("##0.00");
                }
                else
                {
                    txtDO.Text = "---";
                }
                if (obj_data_value.MPS_ORP > -1)
                {
                    txtORP.Text = obj_data_value.MPS_ORP.ToString("##0.00");
                }
                else
                {
                    txtORP.Text = "---";
                }
                if (obj_data_value.MPS_pH > -1)
                {
                    txtpH.Text = obj_data_value.MPS_pH.ToString("##0.00");
                }
                else
                {
                    txtpH.Text = "---";
                }
                if (obj_data_value.MPS_Temp > -1)
                {
                    txtTemp.Text = obj_data_value.MPS_Temp.ToString("##0.00");
                }
                else
                {
                    txtTemp.Text = "---";
                }
                if (obj_data_value.MPS_Turbidity > -1)
                {
                    txtTurb.Text = obj_data_value.MPS_Turbidity.ToString("##0.00");
                }
                else
                {
                    txtTurb.Text = "---";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(temp + ex.Message);
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
