﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLogger.Data;
using System.Windows.Forms.DataVisualization.Charting;
using System.Resources;
using System.Reflection;
using System.Globalization;
using DataLogger.Utils;

namespace DataLogger
{
    public partial class frmHistoryAutoSampler : Form
    {
        //ResourceManager res_man;    // declare Resource manager to access to specific cultureinfo
        //CultureInfo cul;            //declare culture info
        LanguageService lang;

        private enum DataViewType
        {
            List,
            Graph
        }

        private enum DataTimeType
        {
            _5Minute,
            _60Minute
        }

        private readonly data_5minute_value_repository db5m = new data_5minute_value_repository();
        private readonly data_60minute_value_repository db60m = new data_60minute_value_repository();

        // Lưu kiểu hiển thị hiện tại: List/Graph
        private DataViewType currentViewType = DataViewType.List;

        // Lưu inteval hiện tại: 5minute/60minute
        private DataTimeType currentTimeType = DataTimeType._5Minute;


        /// <summary>
        /// contructor
        /// </summary>
        public frmHistoryAutoSampler(LanguageService _lang)
        {
            InitializeComponent();
            //res_man = obj_res_man;
            //cul = obj_cul;
            lang = _lang;
            switch_language();
        }
        private void switch_language()
        {
            this.Text = lang.getText("form_history_title");
            this.grbSelect.Text = lang.getText("form_history_select_group_box_title");
            this.grbPreview.Text = lang.getText("form_history_preview_group_box_title");
            // 5min, 1hour            
            lang.setText(lbl5MinData, "form_history_select_group_5_min_data", btn5Minute, EAlign.Center);
            lang.setText(lbl1HourData, "form_history_select_group_1_hour_data", btn60Minute, EAlign.Center);
            // list type, graph type            
            lang.setText(lblListType, "form_history_select_group_list_type", btnListType, EAlign.Center);
            lang.setText(lblGraphType, "form_history_select_group_graph_type", btnGraphType, EAlign.Center);
            
            this.lblFrom.Text = lang.getText("form_history_select_group_from");
            this.lblTo.Text = lang.getText("form_history_select_group_to");
            this.btnOK.Text = lang.getText("form_history_select_group_button_ok");
            this.btnCancel.Text = lang.getText("form_history_select_group_button_cancel");            

            // preview                        
            this.lblName.Text = lang.getText("auto_sampler_title");
            lang.setText(lblIntevalTimeName, "form_history_preview_interval_time", lblName, EAlign.Left);
            lang.setText(lblDateFromName, "form_history_select_group_from", lblName, EAlign.Left);
            lang.setText(lblDateToName, "form_history_select_group_to", lblName, EAlign.Left);

            //lblIntevalTimeVal.Text = btn5Minute.IsActive ? "5 Minutes" : "60 Minutes";
            if (btn5Minute.IsActive)
            {
                lang.setText(lblIntevalTimeVal, "form_history_select_group_5_min_data");

            }
            else
            {
                lang.setText(lblIntevalTimeVal, "form_history_select_group_1_hour_data");
            }
            //lblViewTypeVal.Text = btnGraphType.IsActive ? "Graph type" : "List type";
            if (btnGraphType.IsActive)
            {
                lang.setText(lblViewType, "form_history_select_group_graph_type");
            }
            else
            {
                lang.setText(lblViewType, "form_history_select_group_list_type");
            }

        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmHistoryAutoSampler_Load(object sender, EventArgs e)
        {
            dtpDateTo.Value = DateTime.Now;
            dtpDateFrom.Value = DateTime.Now.AddDays(-1);
        }
        

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnListType_Click(object sender, EventArgs e)
        {
            //lblViewType.Text = "List type";
            //lblDateFromVal.Text = dtpDateFrom.Value.ToString("dd/MM/yyyy hh:mm:ss");
            //lblDateToVal.Text = dtpDateTo.Value.ToString("dd/MM/yyyy hh:mm:ss");

            currentViewType = DataViewType.List;

            //reloadViewData(currentViewType, currentTimeType);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGraphType_Click(object sender, EventArgs e)
        {
            //lblViewType.Text = "Graph type";
            //lblDateFromVal.Text = dtpDateFrom.Value.ToString("dd/MM/yyyy hh:mm:ss");
            //lblDateToVal.Text = dtpDateTo.Value.ToString("dd/MM/yyyy hh:mm:ss");

            currentViewType = DataViewType.Graph;

            //reloadViewData(currentViewType, currentTimeType);
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn5Minute_Click(object sender, EventArgs e)
        {
            //lblIntevalTimeVal.Text = "5 Minutes";
            //lblDateFromVal.Text = dtpDateFrom.Value.ToString("dd/MM/yyyy hh:mm:ss");
            //lblDateToVal.Text = dtpDateTo.Value.ToString("dd/MM/yyyy hh:mm:ss");

            currentTimeType = DataTimeType._5Minute;

            //reloadViewData(currentViewType, currentTimeType);
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn60Minute_Click(object sender, EventArgs e)
        {
            //lblIntevalTimeVal.Text = "60 Minutes";
            //lblDateFromVal.Text = dtpDateFrom.Value.ToString("dd/MM/yyyy hh:mm:ss");
            //lblDateToVal.Text = dtpDateTo.Value.ToString("dd/MM/yyyy hh:mm:ss");

            currentTimeType = DataTimeType._60Minute;

            //reloadViewData(currentViewType, currentTimeType);
        }


        /// <summary>
        /// Hàm hiển thị, tùy vào viewType là List/Graph và timeType là 5Min/60Min để hiển thị cho phù hợp.
        /// </summary>
        /// <param name="_viewType"></param>
        /// <param name="_timeType"></param>
        private void reloadViewData(DataViewType _viewType, DataTimeType _timeType)
        {
            if (_viewType == DataViewType.List)
            {
                chtData.Visible = false;
                dgvData.Columns.Clear();
                dgvData.Visible = true;

                DataTable dt_source = null;
                if (_timeType == DataTimeType._5Minute)
                {
                    dt_source = db5m.get_all_sampler(dtpDateFrom.Value, dtpDateTo.Value);
                }
                else //if (_timeType == DataTimeType._60Minute)
                {
                    dt_source = db60m.get_all_sampler(dtpDateFrom.Value, dtpDateTo.Value);
                }

                // Do dt_source chứa date,hour,minute riêng nhau nên phải tạo một dt_view mới gộp các thành phần lại hiển thị cho đẹp hơn
                DataTable dt_view = new DataTable();
                dt_view.Columns.Add("Date");
                dt_view.Columns.Add("Time");
                dt_view.Columns.Add("Refrigeration_Temperature");
                dt_view.Columns.Add("Bottle_Position");
                dt_view.Columns.Add("Door_Status");
                dt_view.Columns.Add("Equipment_Status");

                dt_view.Columns.Add("Status_Val");

                // dữ liệu dt_source sang dt_view để hiển thị
                DataRow viewrow = null;
                if (dt_source == null)
                {
                    return;
                }
                foreach (DataRow row in dt_source.Rows)
                {
                    viewrow = dt_view.NewRow();
                    //viewrow["Date"] = row["stored_date"].ToString().Substring(0, 10);
                    //viewrow["Date"] = (Convert.ToDateTime(row["stored_date"].ToString().Substring(0, 10))).ToString("dd/MM/yyyy");
                    //viewrow["Date"] = (Convert.ToDateTime(row["stored_date"].ToString().Substring(0, 10))).ToString("dd/MM/yyyy");
                    viewrow["Date"] = (Convert.ToDateTime(row["stored_date"].ToString())).ToString("dd/MM/yyyy");
                    //viewrow["Date"] = (Convert.ToDateTime((row["stored_date"].ToString().Split(' '))[0])).ToString("dd/MM/yyyy");
                    viewrow["Time"] = ((int)row["stored_hour"]).ToString("00") + ":" + ((int)row["stored_minute"]).ToString("00") + ":00";

                    viewrow["Refrigeration_Temperature"] = row["refrigeration_temperature"];
                    viewrow["Bottle_Position"] = row["bottle_position"];
                    viewrow["Door_Status"] = row["door_status"];
                    viewrow["Equipment_Status"] = row["equipment_status"];

                    viewrow["Status_Val"] = row["equipment_status"];

                    dt_view.Rows.Add(viewrow);
                }
                dgvData.DataSource = dt_view;

                // thêm cột Status có màu phù hợp với status
                DataGridViewImageColumn imgColumnStatus = new DataGridViewImageColumn();
                imgColumnStatus.Name = "Status";
                dgvData.Columns.Add(imgColumnStatus);
                dgvData.Columns["Status_Val"].Visible = false; // ẩn cột status bằng số, chỉ để lại cột status có màu

                int status_val = 0;
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    if (row.Cells["Status_Val"].Value != null)
                    {
                        Int32.TryParse(row.Cells["Status_Val"].Value.ToString(), out status_val);

                        if (status_val == 0)
                        {
                            row.Cells["Status"].Value = (System.Drawing.Image)Properties.Resources.Normal_status_x16;
                        }
                        else if (status_val == 4)
                        {
                            row.Cells["Status"].Value = (System.Drawing.Image)Properties.Resources.bottle_position_18x18;
                        }
                        else
                        {
                            row.Cells["Status"].Value = (System.Drawing.Image)Properties.Resources.Fault_status_x16;
                        }
                    }
                    if (row.Cells["Refrigeration_Temperature"].Value != null)
                    {
                        if (Convert.ToDouble(row.Cells["Refrigeration_Temperature"].Value) < 0)
                        {
                            row.Cells["Refrigeration_Temperature"].Value = "---";
                        }
                    }
                    if (row.Cells["Bottle_Position"].Value != null)
                    {
                        if (Convert.ToDouble(row.Cells["Bottle_Position"].Value) < 0)
                        {
                            row.Cells["Bottle_Position"].Value = "---";
                        }
                    }
                    if (row.Cells["Door_Status"].Value != null)
                    {
                        if (Convert.ToDouble(row.Cells["Door_Status"].Value) < 0)
                        {
                            row.Cells["Door_Status"].Value = "---";
                        }
                    }
                    if (row.Cells["Equipment_Status"].Value != null)
                    {
                        if (Convert.ToDouble(row.Cells["Equipment_Status"].Value) < 0)
                        {
                            row.Cells["Equipment_Status"].Value = "---";
                        }
                    }
                }

            }
            else //if(_viewType == DataViewType.Graph)
            {
                dgvData.Visible = false;
                chtData.Series.Clear();
                chtData.Visible = true;

                DataTable dt_source = null;
                if (_timeType == DataTimeType._5Minute)
                {
                    dt_source = db5m.get_all_sampler(dtpDateFrom.Value, dtpDateTo.Value);
                }
                else //if (_timeType == DataTimeType._60Minute)
                {
                    dt_source = db60m.get_all_sampler(dtpDateFrom.Value, dtpDateTo.Value);
                }

                DataTable dt_view = new DataTable();
                dt_view.Columns.Add("StoredDate");
                dt_view.Columns.Add("Refrigeration_Temperature");
                dt_view.Columns.Add("Bottle_Position");
                dt_view.Columns.Add("Door_Status");
                dt_view.Columns.Add("Equipment_Status");
                dt_view.Columns.Add("Status_Val");
                // chuyển dữ liệu dt_source sang dt_view để hiển thị
                DataRow viewrow = null;
                if (dt_source == null)
                {
                    return;
                }
                foreach (DataRow row in dt_source.Rows)
                {
                    // kiểm tra status, chỉ lấy chững status normal
                    int _status = (int)row["equipment_status"];
                    if (_status == 0)
                    {
                        viewrow = dt_view.NewRow();
                        DateTime _date = (DateTime)row["stored_date"];
                        int _hour = (int)row["stored_hour"];
                        int _minute = (int)row["stored_minute"];
                        DateTime _rdate = new DateTime(_date.Year, _date.Month, _date.Day, _hour, _minute, 0);

                        viewrow["StoredDate"] = _rdate;
                        viewrow["Refrigeration_Temperature"] = row["refrigeration_temperature"];
                        viewrow["Bottle_Position"] = row["bottle_position"];
                        viewrow["Door_Status"] = row["door_status"];
                        viewrow["Equipment_Status"] = row["equipment_status"];

                        viewrow["Status_Val"] = row["equipment_status"];

                        dt_view.Rows.Add(viewrow);
                    }
                }

                // tạo biểu đồ mới
                chtData.Series.Add("Refrigeration_Temperature");
                chtData.Series["Refrigeration_Temperature"].XValueMember = "StoredDate";
                chtData.Series["Refrigeration_Temperature"].YValueMembers = "Refrigeration_Temperature";
                chtData.Series["Refrigeration_Temperature"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                chtData.Series["Refrigeration_Temperature"].Color = Color.Blue;
                chtData.Series["Refrigeration_Temperature"].BorderWidth = 3;

                chtData.Series.Add("Bottle_Position");
                chtData.Series["Bottle_Position"].XValueMember = "StoredDate";
                chtData.Series["Bottle_Position"].YValueMembers = "Bottle_Position";
                chtData.Series["Bottle_Position"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                chtData.Series["Bottle_Position"].Color = Color.Red;
                chtData.Series["Bottle_Position"].BorderWidth = 3;

                chtData.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
                chtData.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
                chtData.ChartAreas[0].AxisX.MinorGrid.Enabled = true;
                chtData.ChartAreas[0].AxisX.MinorGrid.LineColor = Color.WhiteSmoke;
                chtData.ChartAreas[0].AxisX.MinorGrid.LineDashStyle = ChartDashStyle.Dot;
                chtData.ChartAreas[0].AxisX.Title = "Date";
                chtData.ChartAreas[0].AxisX.ArrowStyle = AxisArrowStyle.Lines;

                chtData.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
                chtData.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
                chtData.ChartAreas[0].AxisY.MinorGrid.Enabled = true;
                chtData.ChartAreas[0].AxisY.MinorGrid.LineColor = Color.WhiteSmoke;
                chtData.ChartAreas[0].AxisY.Title = "Auto Sampler (mg/L)";
                chtData.ChartAreas[0].AxisY.ArrowStyle = AxisArrowStyle.Lines;

                chtData.DataSource = dt_view;
                chtData.DataBind();

            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpDateFrom_ValueChanged(object sender, EventArgs e)
        {
            // không cho phép DateFrom lớn hơn DateTo
            if (dtpDateFrom.Value > dtpDateTo.Value) dtpDateFrom.Value = dtpDateTo.Value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpDateTo_ValueChanged(object sender, EventArgs e)
        {
            // không cho phép DateFrom lớn hơn DateTo
            if (dtpDateFrom.Value > dtpDateTo.Value) dtpDateTo.Value = dtpDateFrom.Value;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            lblIntevalTimeVal.Text = btn5Minute.IsActive ? "5 Minutes" : "60 Minutes";
            lblViewType.Text = btnGraphType.IsActive ? "Graph type" : "List type";

            //lblIntevalTimeVal.Text = btn5Minute.IsActive ? "5 Minutes" : "60 Minutes";
            if (btn5Minute.IsActive)
            {
                lang.setText(lblIntevalTimeVal, "form_history_select_group_5_min_data");

            }
            else
            {
                lang.setText(lblIntevalTimeVal, "form_history_select_group_1_hour_data");
            }
            //lblViewTypeVal.Text = btnGraphType.IsActive ? "Graph type" : "List type";
            if (btnGraphType.IsActive)
            {
                lang.setText(lblViewType, "form_history_select_group_graph_type");
            }
            else
            {
                lang.setText(lblViewType, "form_history_select_group_list_type");
            }

            lblDateFromVal.Text = dtpDateFrom.Value.ToString("dd/MM/yyyy hh:mm:ss");
            lblDateToVal.Text = dtpDateTo.Value.ToString("dd/MM/yyyy hh:mm:ss");

            reloadViewData(currentViewType, currentTimeType);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
