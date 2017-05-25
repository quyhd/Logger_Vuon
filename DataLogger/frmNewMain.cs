using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO.Ports;
using DataLogger.Entities;
using System.Globalization;
using DataLogger.Data;
using System.Diagnostics;
using System.IO;
using Excel = ClosedXML.Excel;
//Microsoft.Office.Interop.Excel;
using System.Reflection;
using DataLogger.Utils;
using System.Resources;
using System.Net.Sockets;
using System.Net;
using WinformProtocol;

namespace DataLogger
{
    public partial class frmNewMain : Form
    {
        LanguageService lang = new LanguageService(typeof(frmNewMain).Assembly);
        //ResourceManager res_man;    // declare Resource manager to access to specific cultureinfo
        //CultureInfo cul;            //declare culture info
        public static string language_code = "en";

        public bool is_close_form = false;

        public static TcpListener tcpListener = null;
        public static DateTime datetime00;
        private System.Threading.Timer tmrThreadingTimer;
        private System.Threading.Timer tmrThreadingTimer_HeadingTime;
        private System.Threading.Timer tmrThreadingTimerStationStatus;
        private System.Threading.Timer tmrThreadingTimerFor5Minute;
        private System.Threading.Timer tmrThreadingTimerFor60Minute;

        public CalculationDataValue objCalCulationDataValue5Minute = new CalculationDataValue();
        public CalculationDataValue objCalCulationDataValue60Minute = new CalculationDataValue();

        public const int TRANSACTION_ADD_NEW = 1;
        public const int TRANSACTION_UPDATE = 2;

        public const int PERIOD_CHECK_COMMUNICATION_ERROR = 35;

        public const string ADAM_4050 = "ADAM4050";
        public const string ADAM_4051 = "ADAM4051";
        public const string ADAM_4017_1 = "ADAM40171";
        public const string ADAM_4017_2 = "ADAM40172";
        public static int _GROUP = 1;

        public const string CODE_MPS_PH = "CODE_MPS_PH";
        public const string CODE_MPS_EC = "CODE_MPS_EC";
        public const string CODE_MPS_DO = "CODE_MPS_DO";
        public const string CODE_MPS_TURBIDITY = "CODE_MPS_TURBIDITY";
        public const string CODE_MPS_ORP = "CODE_MPS_ORP";
        public const string CODE_MPS_TEMP = "CODE_MPS_TEMP";
        public const string CODE_TN = "CODE_TN";
        public const string CODE_TP = "CODE_TP";
        public const string CODE_TOC = "CODE_TOC";

        public const string STATUS_ERROR = "Error";
        public const string STATUS_Normal = "Normal";
        public const string STATUS_WARNING = "Warning";
        public const string STATUS_MEASURING = "Measuring";
        public const string STATUS_CALIBRATE = "Calibrate";

        public const int INT_STATUS_NORMAL = 0;
        public const int INT_STATUS_MEASURING_STOP = 1;
        public const int INT_STATUS_EMPTY_SAMPLER_RESERVOIR = 2;
        public const int INT_STATUS_CALIBRATING = 3;
        public const int INT_STATUS_MAINTENANCE = 4;
        public const int INT_STATUS_COMMUNICATION_ERROR = 5;
        public const int INT_STATUS_INSTRUMENT_ERROR = 6;

        // global dataValue
        int countingRequest = 0;
        //data_value objCurrentDataValue = new data_value();
        public int firstTimeForIOControl = 0;

        public static relay_io_control objRelayIOControlGlobal = new relay_io_control();
        public static station_status objStationStatusGlobal = new station_status();
        public static water_sampler objWaterSamplerGLobal = new water_sampler();
        public static measured_data objMeasuredDataGlobal = new measured_data();
        public static string TNComname = "COM100";
        public static string TPComname = "COM100";
        public static string TOCComname = "COM100";
        public static string SAMPComname = "COM100";
        data_value obj5MinuteDataValue = new data_value();
        data_value obj60MinuteDataValue = new data_value();

        // delegate used for Invoke
        internal delegate void StringDelegate(string data);
        internal delegate void HeadingTimerDelegate(string data);
        private delegate void ProcessDataCallback(string text);
        internal delegate void SetHeadingLoginNameDelegate(string data);
        // TOC
        private int TOC_rx_write = 0;
        private int TOC_rx_counter = 0;
        private byte[] TOC_rx_buffer = null;

        private const int PACKET_LENGTH = 92;
        private const int PACKET_LENGTH_1 = 93;

        private byte[] TOC_receive_buffer = new byte[2048];
        private int TOC_buffer_counter = 0;

        // TP
        private int TP_rx_write = 0;
        private int TP_rx_counter = 0;
        private byte[] TP_rx_buffer = null;

        private byte[] TP_receive_buffer = new byte[2048];
        private int TP_buffer_counter = 0;

        // TN
        private int TN_rx_write = 0;
        private int TN_rx_counter = 0;
        private byte[] TN_rx_buffer = null;

        private byte[] TN_receive_buffer = new byte[2048];
        private int TN_buffer_counter = 0;

        // MPS
        private int MPS_rx_write = 0;
        private int MPS_rx_counter = 0;
        private byte[] MPS_rx_buffer = new byte[2048];

        private const int MPS_PACKET_LENGTH = 29;
        private byte[] MPS_receive_buffer = new byte[2048];
        private int MPS_buffer_counter = 0;

        // Sampling Test
        private int SAMP_rx_write = 0;
        private int SAMP_rx_counter = 0;
        //private byte[] SAMP_rx_buffer = new byte[2048];
        private byte[] SAMP_rx_buffer = null;

        private const int SAMP_PACKET_LENGTH = 87;
        private byte[] SAMP_receive_buffer = new byte[2048];
        private int SAMP_buffer_counter = 0;

        public Boolean ADAM4050Sign = false;
        public Boolean ADAM4051Sign = false;
        public Boolean ADAM4017Sign = false;
        // ADAM 4050
        private int ADAM405x_rx_write = 0;
        private int ADAM405x_rx_counter = 0;
        //private byte[] ADAM405x_rx_buffer = null;
        private byte[] ADAM405x_rx_buffer = new byte[2048];

        private const int ADAM405x_PACKET_LENGTH = 8;
        private const int ADAM_TEMP_HUMIDITY_PACKET_LENGTH = 58;
        private const string ADAM_TEMP_HUMIDITY = "";
        private byte[] ADAM405x_receive_buffer = new byte[2048];
        private int ADAM405x_buffer_counter = 0;

        private readonly data_60minute_value_repository db60m = new data_60minute_value_repository();
        private readonly maintenance_log_repository _maintenance_logs = new maintenance_log_repository();

        public string tooltipTOCInfo = "";
        public string tooltipTPInfo = "";
        public string tooltipTNInfo = "";
        public string tooltipSAMPInfo = "";

        public string tooltipTOC = "";
        public string tooltipTP = "";
        public string tooltipTN = "";
        public string tooltipSAMP = "";

        public static Form1 protocol;
        public static Boolean isSamp;
        #region Form event
        public frmNewMain()
        {
            InitializeComponent();
        }

        private void frmNewMain_Load(object sender, EventArgs e)
        {
            //frmConfiguration.protocol = new Form1(this.serialPortSAMP);
            frmConfiguration.protocol = new Form1(this);
            //frmConfiguration.protocol.Show();          

            GlobalVar.maintenanceLog = new maintenance_log();

            backgroundWorkerMain.RunWorkerAsync();
            initUserInterface();
            tmrThreadingTimer = new System.Threading.Timer(new TimerCallback(tmrThreadingTimer_TimerCallback), null, System.Threading.Timeout.Infinite, 2000);
            tmrThreadingTimer.Change(0, 2000);
            tmrThreadingTimer_HeadingTime = new System.Threading.Timer(new TimerCallback(tmrThreadingTimer_HeadingTime_TimerCallback), null, System.Threading.Timeout.Infinite, 700);
            tmrThreadingTimer_HeadingTime.Change(0, 700);
            tmrThreadingTimerStationStatus = new System.Threading.Timer(new TimerCallback(tmrThreadingTimerStationStatus_TimerCallback), null, System.Threading.Timeout.Infinite, 2000);
            tmrThreadingTimerStationStatus.Change(0, 2000);
            tmrThreadingTimerFor5Minute = new System.Threading.Timer(new TimerCallback(tmrThreadingTimerFor5Minute_TimerCallback), null, System.Threading.Timeout.Infinite, 50000);
            //tmrThreadingTimerFor5Minute.Change(0, 2500);
            tmrThreadingTimerFor5Minute.Change(0, 50000);
            tmrThreadingTimerFor60Minute = new System.Threading.Timer(new TimerCallback(tmrThreadingTimerFor60Minute_TimerCallback), null, System.Threading.Timeout.Infinite, 2000);
            //tmrThreadingTimerFor60Minute.Change(0, 3000);
            tmrThreadingTimerFor60Minute.Change(0, 240000);
            initConfig(true);

        }

        private void initConfig(bool isConfigCOM = false)
        {
            GlobalVar.stationSettings = new station_repository().get_info();
            GlobalVar.moduleSettings = new module_repository().get_all();

            label9.Text = Convert.ToString(GlobalVar.stationSettings.station_name);

            if (init(isConfigCOM))
            {
            }
            else
            {
                if (!serialPortTOC.IsOpen)
                {
                    serialPortTOC.PortName = "COM100";
                }
                if (!serialPortTN.IsOpen)
                {
                    serialPortTN.PortName = "COM100";
                }
                if (!serialPortTP.IsOpen)
                {
                    serialPortTP.PortName = "COM100";
                }
                //if (!serialPortADAM.IsOpen)
                //{
                //    serialPortADAM.PortName = "COM100";
                //}
                if (!serialPortSAMP.IsOpen)
                {
                    serialPortSAMP.PortName = "COM100";
                }
                MessageBox.Show(lang.getText("please_check_system"));
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                tcpListener.Stop();
            }
            catch (Exception ex)
            {
            }
            this.Close();
        }
        #endregion


        private void backgroundWorkerMain_DoWork(object sender, DoWorkEventArgs e)
        {
            station existedStationsSetting = new station_repository().get_info();
            if (existedStationsSetting == null)
            {

            }
            else
            {
                //Int32 port = existedStationsSetting.socket_port;
                //IPAddress localAddr = IPAddress.Parse(frmConfiguration.GetLocalIPAddress());

                //IPAddress localAddr = IPAddress.Parse("10.239.164.254");
                //IPAddress localAddr = IPAddress.Parse("192.168.1.62");
                //TcpListener server = new TcpListener(port);

                //tcpListener = new TcpListener(localAddr, port);


            }
            //Protocol.MyTcpListener.Protocol(serialPortTN, serialPortTP, serialPortTOC, serialPortSAMP, tcpListener, objRelayIOControlGlobal, objStationStatusGlobal, objWaterSamplerGLobal, objMeasuredDataGlobal);
        }
        //        public static void Protocol(frmNewMain frmNewMain, TcpListener server, relay_io_control objRelayIOControlGlobal, station_status objStationStatusGlobal, water_sampler objWaterSamplerGLobal, measured_data objMeasuredDataGlobal) {

        //}
        #region initial method
        /// <summary>
        /// init open comport
        /// </summary>
        /// <returns></returns>
        private bool init(bool isConfigCOM = false)
        {
            int a = 0;
            int b = 0;
            int c = 0;
            int d = 0;
            int e = 0;
            try
            {
                //txtMPSpHValue.Text = "---";
                //txtMPSORPValue.Text = "---";
                //txtMPSTempValue.Text = "---";
                //txtMPSDOValue.Text = "---";
                //txtMPSTurbValue.Text = "---";
                //txtMPSCondValue.Text = "---";

                txtTOCValue.Text = "---";
                txtTPValue.Text = "---";
                txtTNValue.Text = "---";

                //if (language_code == "en")
                //{
                //    lblDO1.Text = Convert.ToString(GlobalVar.stationSettings.do1_caption);
                //    lblDO2.Text = Convert.ToString(GlobalVar.stationSettings.do2_caption);
                //    lblDO3.Text = Convert.ToString(GlobalVar.stationSettings.do3_caption);
                //    lblDO4.Text = Convert.ToString(GlobalVar.stationSettings.do4_caption);
                //    lblDO5.Text = Convert.ToString(GlobalVar.stationSettings.do5_caption);
                //    lblDO6.Text = Convert.ToString(GlobalVar.stationSettings.do6_caption);
                //    lblDO7.Text = Convert.ToString(GlobalVar.stationSettings.do7_caption);
                //    lblDO8.Text = Convert.ToString(GlobalVar.stationSettings.do8_caption);
                //}
                //else
                //{
                //    lblDO1.Text = Convert.ToString(GlobalVar.stationSettings.do1_caption_vi);
                //    lblDO2.Text = Convert.ToString(GlobalVar.stationSettings.do2_caption_vi);
                //    lblDO3.Text = Convert.ToString(GlobalVar.stationSettings.do3_caption_vi);
                //    lblDO4.Text = Convert.ToString(GlobalVar.stationSettings.do4_caption_vi);
                //    lblDO5.Text = Convert.ToString(GlobalVar.stationSettings.do5_caption_vi);
                //    lblDO6.Text = Convert.ToString(GlobalVar.stationSettings.do6_caption_vi);
                //    lblDO7.Text = Convert.ToString(GlobalVar.stationSettings.do7_caption_vi);
                //    lblDO8.Text = Convert.ToString(GlobalVar.stationSettings.do8_caption_vi);
                //}
                //this.picDO1Status.Image = global::DataLogger.Properties.Resources.Stop_2_28x28;
                //this.picDO2Status.Image = global::DataLogger.Properties.Resources.Stop_2_28x28;
                //this.picDO3Status.Image = global::DataLogger.Properties.Resources.Stop_2_28x28;
                //this.picDO4Status.Image = global::DataLogger.Properties.Resources.Stop_2_28x28;
                //this.picDO5Status.Image = global::DataLogger.Properties.Resources.Stop_2_28x28;
                //this.picDO6Status.Image = global::DataLogger.Properties.Resources.Stop_2_28x28;
                //this.picDO7Status.Image = global::DataLogger.Properties.Resources.Stop_2_28x28;
                //this.picDO8Status.Image = global::DataLogger.Properties.Resources.Stop_2_28x28;

                //this.btnDO1.Image = global::DataLogger.Properties.Resources.OFF_switch_96x25;
                //this.btnDO2.Image = global::DataLogger.Properties.Resources.OFF_switch_96x25;
                //this.btnDO3.Image = global::DataLogger.Properties.Resources.OFF_switch_96x25;
                //this.btnDO4.Image = global::DataLogger.Properties.Resources.OFF_switch_96x25;
                //this.btnDO5.Image = global::DataLogger.Properties.Resources.OFF_switch_96x25;
                //this.btnDO6.Image = global::DataLogger.Properties.Resources.OFF_switch_96x25;
                //this.btnDO7.Image = global::DataLogger.Properties.Resources.OFF_switch_96x25;
                //this.btnDO8.Image = global::DataLogger.Properties.Resources.OFF_switch_96x25;

                if (isConfigCOM)
                {
                    if (serialPortTOC.IsOpen)
                        serialPortTOC.Close();
                    if (serialPortTN.IsOpen)
                        serialPortTN.Close();
                    if (serialPortTP.IsOpen)
                        serialPortTP.Close();
                    //if (serialPortADAM.IsOpen)
                    //    serialPortADAM.Close();
                    if (serialPortSAMP.IsOpen)
                        serialPortSAMP.Close();

                    //if (Convert.ToString(GlobalVar.stationSettings.module_comport) != "")
                    //{
                    //    serialPortADAM.PortName = GlobalVar.stationSettings.module_comport;
                    //    //MessageBox.Show("serialPortSAMP open success");
                    //    serialPortADAM.Open();
                    //}
                    if (Convert.ToString(GlobalVar.stationSettings.tn_comport) != "")
                    {
                        serialPortTN.PortName = GlobalVar.stationSettings.tn_comport;

                        // TN connect
                        serialPortTN.Open();
                    }
                    if (Convert.ToString(GlobalVar.stationSettings.toc_comport) != "")
                    {
                        serialPortTOC.PortName = GlobalVar.stationSettings.toc_comport;
                        // TOC connect
                        serialPortTOC.Open();   //exception
                    }

                    if (Convert.ToString(GlobalVar.stationSettings.tp_comport) != "")
                    {
                        serialPortTP.PortName = GlobalVar.stationSettings.tp_comport;
                        // TP connect
                        serialPortTP.Open();
                    }

                    if (Convert.ToString(GlobalVar.stationSettings.sampler_comport) != "")
                    {
                        serialPortSAMP.PortName = GlobalVar.stationSettings.sampler_comport;
                        // SAMPLER connect
                        serialPortSAMP.Open();
                    }
                    TNComname = serialPortTN.PortName;
                    TPComname = serialPortTP.PortName;
                    TOCComname = serialPortTOC.PortName;
                    SAMPComname = serialPortSAMP.PortName;
                }
                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Excep" + Convert.ToString(ex));
                return false;
            }
        }
        /// <summary>
        /// initial interface setting
        /// </summary>
        private void initUserInterface()
        {
            //pnHeader.BackColor = System.Drawing.ColorTranslator.FromHtml("#343399");
            //pnLeftSide.BackColor = System.Drawing.ColorTranslator.FromHtml("#c0c0c0");
            //pnPumpingSystem.BackColor = System.Drawing.ColorTranslator.FromHtml("#008081");
            //pnAutoSampler.BackColor = System.Drawing.ColorTranslator.FromHtml("#993365");

            // LANGUAGE
            // Default is English

            switch_language();
        }

        private void switch_language()
        {
            //Assembly a = Assembly.Load("DataLogger");
            //res_man = new ResourceManager("DataLogger.Resources.Res", typeof(frmNewMain).Assembly);
            lang.nextLanguage();
            switch (lang.CurrentLanguage.Language)
            {
                case ELanguage.English:
                    //cul = CultureInfo.CreateSpecificCulture("en");
                    language_code = "en";
                    this.btnMonthlyReport.BackgroundImage = global::DataLogger.Properties.Resources.MonthlyReportButton;
                    break;
                case ELanguage.Vietnamese:
                    //cul = CultureInfo.CreateSpecificCulture("vi");
                    language_code = "vi";
                    this.btnMonthlyReport.BackgroundImage = global::DataLogger.Properties.Resources.MonthlyReportButton;
                    break;
                default:
                    break;
            }

            this.btnLanguage.BackgroundImage = lang.CurrentLanguage.Icon;


            // heading menu
            lang.setText(lblHeaderNationName, "main_menu_language");
            lang.setText(lblMainMenuTitle, "main_menu_title");
            settingForLoginStatus();
            // left menu buttong
            //lang.setText(btnMonthlyReport, "left_menu_monthly_report");
            //// pumping system
            //lang.setText(lblPumpingSystem, "pumping_system_title", EAlign.Center);
            //lang.setText(lblPump1, "pumping_system_pump1", picPump1, EAlign.Center);
            //lang.setText(lblPump2, "pumping_system_pump2", picPump2, EAlign.Center);
            //// Filtering system
            //lang.setText(lblFilteringSystem, "filtering_system_title", EAlign.Center);
            //lang.setText(lblCleaning, "filtering_system_cleaning", EAlign.Center);
            lang.setText(lblWaterLevel, "filtering_system_water_level", EAlign.Center);
            // Auto sampler
            lang.setText(lblAutorSampler, "auto_sampler_title", EAlign.Center);
            //// MPS
            //lang.setText(lblSamplerTank, "sampler_tank_title");
            //lang.setText(lblDrainValve, "drain_valve");

            lang.setText(lblThaiNguyenStation, "thai_nguyen_station_text", EAlign.Center);
            lang.setText(lblAutomaticMonitoring, "automatic_monitoring_text", EAlign.Center);
            lang.setText(lblSurfaceWaterQuality, "surface_water_quality_text", EAlign.Center);

            //// station status
            //lang.setText(lblStationStatus, "station_status_title", pnStationStatus, EAlign.Center);
            //lang.setText(lblMainPower, "station_status_main_power");
            //lang.setText(lblair2, "station_status_air2");
            //lang.setText(lblMainDoor, "station_status_main_door");
            //lang.setText(lblFireDetector, "station_status_fire_detector");
            //lang.setText(lblair1, "station_status_air1");
            //// control panel
            //lang.setText(lblControlPanel, "control_panel_title", EAlign.Center);

            lang.setText(this, "data_logger_system");
        }
        #endregion

        #region Comport receive

        private void serialPortTOC_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                if (!serialPortTOC.IsOpen)
                    return;
                int bytes = serialPortTOC.BytesToRead;
                byte[] buffer = new byte[bytes];
                serialPortTOC.Read(buffer, 0, bytes);
                //if (buffer.Length == 5)
                //{
                //    foreach (byte b in buffer) Console.WriteLine("TOC " + b);
                //}
                //Console.Write("buffer \t"+ _encoder.GetString(buffer) + "\n");
                //Console.Write("buffer.length \t" + buffer.Length + "\n");
                //for (int i = 0; i < bytes; i++)
                //{
                //    TOC_rx_buffer[TOC_rx_write++] = buffer[i];
                //}
                //Console.Write("buffer.length TOC \t" + buffer.Length + "\n");
                if (TOC_rx_buffer == null)
                {
                    TOC_rx_buffer = buffer;
                }
                else TOC_rx_buffer = TOC_rx_buffer.Concat(buffer).ToArray();
                //Console.WriteLine(_encoder.GetString(TOC_rx_buffer));
                //Console.WriteLine(_encoder.GetString(TOC_rx_buffer).Length);
                TOC_rx_counter += bytes;
                var new_data = TOC_rx_buffer.TakeWhile((v, index) => TOC_rx_buffer.Skip(index).Any(w => w != 0x00)).ToArray();
                //string raw_data2 = encoding.utf8.getstring(adam405x_rx_buffer);
                //console.writeline("adam405x_rx_counter : " + adam405x_rx_counter);
                //Console.WriteLine("data : " + new_data.Length + " : " + Encoding.UTF8.GetString(new_data));
                if (TOC_rx_buffer[TOC_rx_buffer.Length - 1] == 10 && TOC_rx_buffer.Length >= PACKET_LENGTH)
                {
                    TOC_rx_buffer = TOC_rx_buffer.Take(TOC_rx_buffer.Count() - 1).ToArray();
                }
                if (TOC_rx_buffer.Length == PACKET_LENGTH)
                {
                    //Console.Write("TRUE TOC");
                    string raw_data2 = Encoding.UTF8.GetString(TOC_rx_buffer);
                    //Console.WriteLine("data : " + raw_data2.Length + " : " + Encoding.UTF8.GetString(raw_data2));
                    ProcessDataTOC("");
                }
                if (TOC_rx_buffer != null)
                {
                    if (TOC_rx_buffer.Length > PACKET_LENGTH)
                    {
                        TOC_rx_buffer = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        private void serialPortTP_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                if (!serialPortTP.IsOpen)
                    return;
                int bytes = serialPortTP.BytesToRead;
                byte[] buffer = new byte[bytes];
                serialPortTP.Read(buffer, 0, bytes);
                //if (buffer.Length == 4) {
                //    foreach (byte b in buffer) Console.WriteLine(b);
                //}
                if (TP_rx_buffer == null)
                {
                    TP_rx_buffer = buffer;
                }
                else TP_rx_buffer = TP_rx_buffer.Concat(buffer).ToArray();
                TP_rx_counter += bytes;
                if (TP_rx_buffer[TP_rx_buffer.Length - 1] == 10 && TP_rx_buffer.Length >= PACKET_LENGTH)
                {
                    TP_rx_buffer = TP_rx_buffer.Take(TP_rx_buffer.Count() - 1).ToArray();
                }
                //var new_data = TP_rx_buffer.TakeWhile((v, index) => TP_rx_buffer.Skip(index).Any(w => w != 0x00)).ToArray();
                //string raw_data2 = encoding.utf8.getstring(adam405x_rx_buffer);
                //console.writeline("adam405x_rx_counter : " + adam405x_rx_counter);
                //Console.WriteLine("data : " + new_data.Length + " : " + Encoding.UTF8.GetString(new_data));
                if (TP_rx_buffer.Length == PACKET_LENGTH)
                {
                    //string raw_data2 = Encoding.UTF8.GetString(ADAM405x_rx_buffer);
                    //Console.WriteLine("TP_counter : " + ADAM405x_rx_counter);
                    //Console.WriteLine("TPdata : " + raw_data2
                    //Console.Write("TRUE TP");
                    ProcessDataTP("");
                }
                if (TP_rx_buffer != null)
                {
                    if (TP_rx_buffer.Length > PACKET_LENGTH)
                    {
                        TP_rx_buffer = null;
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.StackTrace);
            }
        }
        private void serialPortTN_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                if (!serialPortTN.IsOpen)
                    return;
                int bytes = serialPortTN.BytesToRead;
                byte[] buffer = new byte[bytes];
                serialPortTN.Read(buffer, 0, bytes);
                if (TN_rx_buffer == null)
                {
                    TN_rx_buffer = buffer;
                }
                else TN_rx_buffer = TN_rx_buffer.Concat(buffer).ToArray();
                TN_rx_counter += bytes;
                if (TN_rx_buffer[TN_rx_buffer.Length - 1] == 10 && TN_rx_buffer.Length >= PACKET_LENGTH)
                {
                    TN_rx_buffer = TN_rx_buffer.Take(TN_rx_buffer.Count() - 1).ToArray();
                }
                if (TN_rx_buffer.Length == PACKET_LENGTH)
                {
                    //Console.Write("TRUE TN");
                    ProcessDataTN("");
                }
                if (TN_rx_buffer != null)
                {
                    if (TN_rx_buffer.Length > PACKET_LENGTH)
                    {
                        TN_rx_buffer = null;
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.StackTrace);
            }
        }
        //private void serialPortMPS_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        //{
        //    try
        //    {
        //        if (!serialPortMPS.IsOpen)
        //        {
        //            return;
        //        }
        //        int bytes = serialPortMPS.BytesToRead;
        //        byte[] buffer = new byte[bytes];
        //        serialPortMPS.Read(buffer, 0, bytes);
        //        for (int i = 0; i < bytes; i++)
        //        {
        //            MPS_rx_buffer[MPS_rx_write++] = buffer[i];
        //            if (MPS_rx_write == 2048)
        //                MPS_rx_write = 0;
        //        }
        //        MPS_rx_counter += bytes;
        //        ProcessDataMPS("");
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show(ex.Message);
        //    }
        //}
        public delegate void DataReceivedEventHandler(object sender, ReceivedEventArgs e);
        public event DataReceivedEventHandler DataReceived;
        public void serialPortSAMP_DataReceived(object sender, SerialDataReceivedEventArgs args)
        {
            //byte[] inputData = new byte[serialPortSAMP.BytesToRead];
            //if (DataReceived != null)
            //{
            //    DataReceived(inputData);
            //}
            
            try
            {
                //MessageBox.Show("testSAMP");
                if (!serialPortSAMP.IsOpen)
                    return;
                int bytes = serialPortSAMP.BytesToRead;
                byte[] buffer = new byte[bytes];
                serialPortSAMP.Read(buffer, 0, bytes);
                //foreach (var a in buffer)
                //{ Console.WriteLine("newmain1 " + a); }
                if (DataReceived != null)
                {
                    ReceivedEventArgs rea = new ReceivedEventArgs { Data = buffer };
                    DataReceived(this, rea);
                }

                if (buffer != null && buffer[0] == 0x06)
                {
                    //MessageBox.Show("Success");
                    isSamp = true;
                    if (SAMP_rx_buffer == null)
                    {
                        SAMP_rx_buffer = null;
                    }
                    else
                    {
                        SAMP_rx_buffer = SAMP_rx_buffer.Concat(buffer).ToArray();
                    }
                    Console.WriteLine("NEW MAIN ACK");
                }
                else if (buffer != null && buffer[0] == 0x15)
                {
                    isSamp = false;
                    //MessageBox.Show("Error");
                    if (SAMP_rx_buffer == null)
                    {
                        SAMP_rx_buffer = null;
                    }
                    else
                    {
                        SAMP_rx_buffer = SAMP_rx_buffer.Concat(buffer).ToArray();
                    }
                    Console.WriteLine("NEW MAIN NAK");
                }
                else
                if (SAMP_rx_buffer == null)
                {
                    SAMP_rx_buffer = buffer;
                }
                else
                {
                    SAMP_rx_buffer = SAMP_rx_buffer.Concat(buffer).ToArray();
                }
                SAMP_rx_counter += bytes;
                //if (TN_rx_buffer[TN_rx_buffer.Length - 1] == 10 && TN_rx_buffer.Length >= SAMP_PACKET_LENGTH)
                //{
                //    TN_rx_buffer = TN_rx_buffer.Take(TN_rx_buffer.Count() - 1).ToArray();
                //}
                if (SAMP_rx_buffer != null)
                {
                    //string raw_data1 = Encoding.ASCII.GetString(SAMP_rx_buffer);
                    //Console.WriteLine("data : " + raw_data1.Length);
                    //Console.WriteLine(raw_data1.Trim());

                    if (SAMP_rx_buffer.Length == SAMP_PACKET_LENGTH)
                    {

                        ProcessDataSAMP("");
                    }
                }
                if (SAMP_rx_buffer != null)
                {
                    if (SAMP_rx_buffer.Length >= SAMP_PACKET_LENGTH)
                    {
                        SAMP_rx_buffer = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }
        //private void serialPortADAM_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{
        //    try
        //    {
        //        //Console.WriteLine("2");
        //        //tmrThreadingTimerStationStatus.Change(Timeout.Infinite, Timeout.Infinite);
        //        //if (!serialPortADAM.IsOpen)
        //        //    return;
        //        //int bytes = serialPortADAM.BytesToRead;
        //        ////Console.WriteLine(bytes);
        //        //byte[] buffer = new byte[bytes];
        //        //serialPortADAM.Read(buffer, 0, bytes);
        //        ////Console.WriteLine(buffer.Length);
        //        ////Console.WriteLine(Encoding.UTF8.GetString(buffer));
        //        //if (ADAM405x_rx_buffer == null)
        //        //{
        //        //    ADAM405x_rx_buffer = buffer;
        //        //}
        //        ////else 
        //        ////if (buffer[0] == 0x21) {
        //        ////    int start4017 = 
        //        ////}
        //        //else
        //        //{
        //        //    ADAM405x_rx_buffer = ADAM405x_rx_buffer.Concat(buffer).ToArray();
        //        //}
        //        //ADAM405x_rx_counter += bytes;
        //        //string raw_data1 = Encoding.UTF8.GetString(ADAM405x_rx_buffer);
        //        //Console.WriteLine("===== \t" + ADAM405x_rx_buffer.Length + "##### \t");
        //        //Console.WriteLine(Encoding.UTF8.GetString(ADAM405x_rx_buffer));
        //        //if (ADAM405x_rx_buffer.Length == ADAM_TEMP_HUMIDITY_PACKET_LENGTH || ADAM405x_rx_buffer.Length == 8)
        //        //{
        //        //string raw_data2 = Encoding.UTF8.GetString(ADAM405x_rx_buffer);
        //        //Console.WriteLine(raw_data2 + "@@@" + ADAM405x_rx_buffer.Length);
        //        //if (ADAM405x_rx_buffer[0] == 0x21) Console.WriteLine("TRUE");
        //        ////ProcessDataADAM("");
        //        //}
        //        //if (ADAM405x_rx_buffer != null)
        //        //{
        //        //    if (ADAM405x_rx_buffer.Length > ADAM_TEMP_HUMIDITY_PACKET_LENGTH || (ADAM405x_rx_buffer.Length == 8 && ADAM405x_rx_buffer[0] == 0x21))
        //        //    {
        //        //        ADAM405x_rx_buffer = null;
        //        //    }
        //        //}


        //        ////MessageBox.Show("testeset");
        //        if (!serialPortADAM.IsOpen)
        //            return;
        //        int bytes = serialPortADAM.BytesToRead;
        //        byte[] buffer = new byte[bytes];
        //        serialPortADAM.Read(buffer, 0, bytes);
        //        //string raw_data1 = Encoding.UTF8.GetString(buffer);

        //        for (int i = 0; i < bytes; i++)
        //        {
        //            ADAM405x_rx_buffer[ADAM405x_rx_write++] = buffer[i];
        //            if (ADAM405x_rx_write >= 2048)
        //                ADAM405x_rx_write = 0;
        //        }
        //        ADAM405x_rx_counter = ADAM405x_rx_counter + bytes;
        //        var new_data = ADAM405x_rx_buffer.TakeWhile((v, index) => ADAM405x_rx_buffer.Skip(index).Any(w => w != 0x00)).ToArray();
        //        //string raw_data2 = Encoding.UTF8.GetString(ADAM405x_rx_buffer);
        //        //Console.WriteLine("ADAM405x_rx_counter : " + ADAM405x_rx_counter);
        //        //Console.WriteLine("data : " + new_data.Length + " : " + Encoding.UTF8.GetString(new_data));
        //        ProcessDataADAM("");

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.StackTrace);
        //    }
        //}
        #endregion

        #region ComPort Process data
        private void ProcessDataTOC(string text)
        {

            try
            {
                if (this.txtData.InvokeRequired)
                {
                    ProcessDataCallback d = new ProcessDataCallback(ProcessDataTOC);
                    this.txtData.Invoke(d, new object[] { text });
                }
                else
                {
                    string temp1 = TOCParseData(TOC_rx_buffer);
                    //if (rbtnTOC.Checked)
                    //{
                    //    txtData.Text = temp1;
                    //}

                    //MessageBox.Show("000:" + txtData.Text);
                    //TOC_rx_counter = 0;
                    //TOC_rx_write = 0;
                }
            }
            catch //(Exception ex)
            {

            }
            finally
            {

            }

        }
        private void ProcessDataTP(string text)
        {

            try
            {
                if (this.txtData.InvokeRequired)
                {
                    ProcessDataCallback d = new ProcessDataCallback(ProcessDataTP);
                    this.txtData.Invoke(d, new object[] { text });
                }
                else
                {
                    string temp1 = TPParseData(TP_rx_buffer);
                    //if (rbtnTP.Checked)
                    //{
                    //    txtData.Text = temp1;
                    //}

                    //MessageBox.Show("000:" + txtData.Text);
                    //TP_rx_counter = 0;
                    //TP_rx_write = 0;
                }
            }
            catch //(Exception ex)
            {

            }
            finally
            {

            }

        }
        private void ProcessDataTN(string text)
        {
            try
            {
                if (this.txtData.InvokeRequired)
                {
                    ProcessDataCallback d = new ProcessDataCallback(ProcessDataTN);
                    this.txtData.Invoke(d, new object[] { text });
                }
                else
                {
                    string temp1 = TNParseData(TN_rx_buffer);
                    //if (rbtnTN.Checked)
                    //{
                    //    txtData.Text = temp1;
                    //}

                    //MessageBox.Show("000:" + txtData.Text);
                    //TN_rx_counter = 0;
                    //TN_rx_write = 0;
                }
            }
            catch //(Exception ex)
            {

            }
            finally
            {

            }
        }
        private void ProcessDataMPS(string text)
        {
            try
            {
                if (this.txtData.InvokeRequired)
                {
                    ProcessDataCallback d = new ProcessDataCallback(ProcessDataMPS);
                    this.txtData.Invoke(d, new object[] { text });
                }
                else
                {
                    byte[] temp = new byte[MPS_rx_counter];
                    for (int i = 0; i < MPS_rx_counter; i++)
                    {
                        temp[i] = MPS_rx_buffer[i];
                    }
                    string temp1 = MPSParseData(temp);
                    //if (rbtnMPS.Checked)
                    //{
                    //    txtData.Text = temp1;
                    //}
                    txtData.Text = temp1;
                    //MessageBox.Show(temp1);
                    //MessageBox.Show("000:" + txtData.Text);
                    MPS_rx_counter = 0;
                    MPS_rx_write = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void setText(string text)
        {
            if (this.txtData.InvokeRequired)
            {
                StringDelegate d = new StringDelegate(setText);
                this.txtData.Invoke(d, new object[] { text });
            }
            else
            {
                txtData.Text = text;
            }
        }
        private void setTextHeadingTimer(string text)
        {
            if (this.txtData.InvokeRequired)
            {
                HeadingTimerDelegate d = new HeadingTimerDelegate(setTextHeadingTimer);
                this.lblHeadingTime.Invoke(d, new object[] { text });
            }
            else
            {
                lblHeadingTime.Text = text;
            }
        }
        private void setTextHeadingLogin(string text)
        {
            if (this.txtData.InvokeRequired)
            {
                SetHeadingLoginNameDelegate d = new SetHeadingLoginNameDelegate(setTextHeadingLogin);
                this.lblLoginDisplayName.Invoke(d, new object[] { text });
            }
            else
            {
                lblLoginDisplayName.Text = text;
            }
        }
        private void setRawText(string text)
        {
            //if (this.txtRawData.InvokeRequired)
            //{
            //    StringDelegate d = new StringDelegate(setRawText);
            //    this.txtRawData.Invoke(d, new object[] { text });
            //}
            //else
            //{
            //    txtRawData.Text = text;
            //}
        }
        public static ASCIIEncoding _encoder = new ASCIIEncoding();
        private string TOCParseData(byte[] text)
        {
            //int bufferIndex = 0;
            string result = "";

            try
            {
                int j = 0;
                //Console.Write("text.Length \t" + text.Length + "\n");
                //Console.Write("PACKET_LENGTH \t" + PACKET_LENGTH + "\n");
                if (text.Length >= PACKET_LENGTH)
                {
                    for (int i = 0; i < text.Length; i++)
                    {
                        j = i;
                        if (text[j] == 0x02 && // STX
                            text[PACKET_LENGTH + j - 1] == 0x0D && // CR
                            text[PACKET_LENGTH + j - 4] == 0x03) // ETX
                        {
                            //Console.Write("TRUE TOC" + "\n");
                            string strDateTime = Encoding.UTF8.GetString(text, j + 5, 14);
                            string strNumberParameter = Encoding.UTF8.GetString(text, j + 19, 2);
                            string strParameter = Encoding.UTF8.GetString(text, j + 21, 5);
                            string strValue = Encoding.UTF8.GetString(text, j + 26, 10);
                            string strInstrumentStatus = Encoding.UTF8.GetString(text, j + 36, 2);
                            string strAdditionInfor = Encoding.UTF8.GetString(text, j + 38, 50);

                            result = string.Format("DateTime: {0}; Number Parameter: {1}; Parameter: {2}; Value: {3}; Instrument Status: {4}; Addition Info: {5}", strDateTime, strNumberParameter, strParameter, strValue, strInstrumentStatus, strAdditionInfor);
                            //Console.Write(result);
                            //if (rbtnTOC.Checked)
                            //{
                            //    byte[] subArray = new byte[PACKET_LENGTH];
                            //    Array.Copy(text, i, subArray, 0, PACKET_LENGTH);
                            //    //setRawText(Encoding.UTF8.GetString(subArray));
                            //}

                            Double dValue = Convert.ToDouble(strValue);
                            objMeasuredDataGlobal.TOC = dValue;
                            //Console.WriteLine("dValue " + dValue);
                            txtTOCValue.Text = dValue.ToString("##0.00");
                            int status = 0;
                            //strAdditionInfor = strAdditionInfor.ToLower();
                            //if (strAdditionInfor.Contains("error"))
                            //{
                            //    status = INT_STATUS_INSTRUMENT_ERROR;
                            //}
                            //else if (strAdditionInfor.Contains("running"))
                            //{
                            //    status = INT_STATUS_NORMAL;
                            //}
                            //else if (strAdditionInfor.Contains("normal"))
                            //{
                            //    status = INT_STATUS_NORMAL;
                            //}
                            //else if (strAdditionInfor.Contains("warning"))
                            //{
                            //    status = INT_STATUS_MAINTENANCE;
                            //}
                            //else if (strAdditionInfor.Contains("system test"))
                            //{
                            //    status = INT_STATUS_INSTRUMENT_ERROR;
                            //}

                            objMeasuredDataGlobal.TOC_status = status = Convert.ToInt32(strInstrumentStatus.Trim());
                            objMeasuredDataGlobal.latest_update_TOC_communication = DateTime.Now;
                            tooltipTOCInfo = strAdditionInfor;

                            //// read data to display
                            //byte[] temp = new byte[PACKET_LENGTH];
                            //Array.Copy(text, i, temp, 0, PACKET_LENGTH);
                            //result += ByteArrayToHexString(temp);
                            ////Remove this packet from temporary buffer
                            //bufferIndex = i + PACKET_LENGTH;
                            //i = i + PACKET_LENGTH - 1;

                        }
                    }
                }
                //Reset buffer
                TOC_buffer_counter = 0;
                Array.Clear(TOC_receive_buffer, 0, TOC_receive_buffer.Length);
                //if (bufferIndex < text.Length)
                //{
                //    TOC_buffer_counter = text.Length - bufferIndex;
                //    //Console.WriteLine("Buffer Counter: " + buffer_counter.ToString() + ", Data Length: " + text.Length);
                //    Array.Copy(text, bufferIndex, TOC_receive_buffer, 0, TOC_buffer_counter);
                //}

                //if (TOC_buffer_counter > PACKET_LENGTH * 2)
                //{
                TOC_rx_buffer = null;
                TOC_rx_write = 0;
                TOC_rx_counter = 0;

                TOC_receive_buffer = new byte[2048];
                TOC_buffer_counter = 0;
                //}

                updateMeasuredDataValue(objMeasuredDataGlobal);

            }
            catch //(Exception ex)
            {
                //MessageBox.Show("0002," + ex.Message);
            }
            if (result == "")
                return result;
            return result;
        }
        private string TPParseData(byte[] text)
        {

            //int bufferIndex = 0;
            string result = "";

            try
            {
                //Console.Write(_encoder.GetString(text) + "\n");
                int j = 0;
                //if (TP_buffer_counter > 0)
                //    text = Combine(TP_receive_buffer, TP_buffer_counter, text);
                if (text.Length >= PACKET_LENGTH)
                {
                    for (int i = 0; i < text.Length; i++)
                    {
                        j = i;
                        if (text[j] == 0x02 && // STX
                            text[PACKET_LENGTH + j - 1] == 0x0D && // CR
                            text[PACKET_LENGTH + j - 4] == 0x03) // ERX
                        {
                            // process data
                            //Console.Write("quyhddddddd \n");
                            string strDateTime = Encoding.UTF8.GetString(text, j + 5, 14);
                            string strNumberParameter = Encoding.UTF8.GetString(text, j + 19, 2);
                            string strParameter = Encoding.UTF8.GetString(text, j + 21, 5);
                            string strValue = Encoding.UTF8.GetString(text, j + 26, 10);
                            string strInstrumentStatus = Encoding.UTF8.GetString(text, j + 36, 2);
                            string strAdditionInfor = Encoding.UTF8.GetString(text, j + 38, 50);


                            result = string.Format("DateTime: {0}; Number Parameter: {1}; Parameter: {2}; Value: {3}; Instrument Status: {4}; Addition Info: {5}", strDateTime, strNumberParameter, strParameter, strValue, strInstrumentStatus, strAdditionInfor);

                            //if (rbtnTP.Checked)
                            //{
                            //    byte[] subArray = new byte[PACKET_LENGTH];
                            //    Array.Copy(text, i, subArray, 0, PACKET_LENGTH);
                            //    //setRawText(Encoding.UTF8.GetString(subArray));
                            //}

                            Double dValue = Convert.ToDouble(strValue);
                            objMeasuredDataGlobal.TP = dValue;
                            objMeasuredDataGlobal.MPS_SS = dValue;
                            objMeasuredDataGlobal.latest_update_TP_communication = DateTime.Now;
                            //txtTPValue.Text = dValue.ToString("##0.00");
                            int status = 0;
                            //strAdditionInfor = strAdditionInfor.ToLower();
                            //if (strAdditionInfor.Contains("error"))
                            //{
                            //    status = INT_STATUS_INSTRUMENT_ERROR;
                            //    this.picTPStatus.BackgroundImage = global::DataLogger.Properties.Resources.Fault;
                            //}
                            //else if (strAdditionInfor.Contains("running"))
                            //{
                            //    status = INT_STATUS_NORMAL;
                            //    this.picTPStatus.BackgroundImage = global::DataLogger.Properties.Resources.Run_56x56;
                            //}
                            //else if (strAdditionInfor.Contains("normal"))
                            //{
                            //    status = INT_STATUS_NORMAL;
                            //    this.picTPStatus.BackgroundImage = global::DataLogger.Properties.Resources.Normal_status;
                            //}
                            //else if (strAdditionInfor.Contains("warning"))
                            //{
                            //    status = INT_STATUS_MAINTENANCE;
                            //}
                            //else if (strAdditionInfor.Contains("measuring"))
                            //{
                            //    status = INT_STATUS_NORMAL;
                            //}
                            //else if (strAdditionInfor.Contains("standby"))
                            //{
                            //    status = INT_STATUS_INSTRUMENT_ERROR;
                            //}
                            objMeasuredDataGlobal.TP_status = status = Convert.ToInt32(strInstrumentStatus.Trim());
                            objMeasuredDataGlobal.MPS_SS_status = Convert.ToInt32(strInstrumentStatus.Trim());
                            tooltipTPInfo = strAdditionInfor;

                            //// read data to display
                            //byte[] temp = new byte[PACKET_LENGTH];
                            //Array.Copy(text, i, temp, 0, PACKET_LENGTH);
                            //result += ByteArrayToHexString(temp);
                            ////Remove this packet from temporary buffer
                            //bufferIndex = i + PACKET_LENGTH;
                            //i = i + PACKET_LENGTH - 1;
                        }
                    }
                }
                //Reset buffer
                TP_buffer_counter = 0;
                Array.Clear(TP_receive_buffer, 0, TP_receive_buffer.Length);
                //if (bufferIndex < text.Length)
                //{
                //    TP_buffer_counter = text.Length - bufferIndex;
                //    //Console.WriteLine("Buffer Counter: " + buffer_counter.ToString() + ", Data Length: " + text.Length);
                //    Array.Copy(text, bufferIndex, TP_receive_buffer, 0, TP_buffer_counter);
                //}

                //if (TP_buffer_counter > PACKET_LENGTH * 2)
                //{
                TP_rx_buffer = null;
                TP_rx_write = 0;
                TP_rx_counter = 0;
                TP_receive_buffer = new byte[2048];
                TP_buffer_counter = 0;
                //}

                updateMeasuredDataValue(objMeasuredDataGlobal);

            }
            catch //(Exception ex)
            {
                //MessageBox.Show("0002," + ex.Message);
            }

            if (result == "")
                return result;
            return result;
        }
        private string TNParseData(byte[] text)
        {

            int bufferIndex = 0;
            string result = "";

            try
            {
                int j = 0;
                //if (TN_buffer_counter > 0)
                //    text = Combine(TN_receive_buffer, TN_buffer_counter, text);
                if (text.Length >= PACKET_LENGTH)
                {
                    for (int i = 0; i < text.Length; i++)
                    {
                        j = i;
                        if (text[j] == 0x02 && // STX
                            text[PACKET_LENGTH + j - 1] == 0x0D && // CR
                            text[PACKET_LENGTH + j - 4] == 0x03) // ERX
                        {
                            string strDateTime = Encoding.UTF8.GetString(text, j + 5, 14);
                            string strNumberParameter = Encoding.UTF8.GetString(text, j + 19, 2);
                            string strParameter = Encoding.UTF8.GetString(text, j + 21, 5);
                            string strValue = Encoding.UTF8.GetString(text, j + 26, 10);
                            string strInstrumentStatus = Encoding.UTF8.GetString(text, j + 36, 2);
                            string strAdditionInfor = Encoding.UTF8.GetString(text, j + 38, 50);

                            result = string.Format("DateTime: {0}; Number Parameter: {1}; Parameter: {2}; Value: {3}; Instrument Status: {4}; Addition Info: {5}", strDateTime, strNumberParameter, strParameter, strValue, strInstrumentStatus, strAdditionInfor);
                            //Console.Write(result);
                            //if (rbtnTN.Checked)
                            //{
                            //    byte[] subArray = new byte[PACKET_LENGTH];
                            //    Array.Copy(text, i, subArray, 0, PACKET_LENGTH);
                            //    //setRawText(Encoding.UTF8.GetString(subArray));
                            //}

                            Double dValue = Convert.ToDouble(strValue);
                            objMeasuredDataGlobal.TN = dValue;
                            objMeasuredDataGlobal.MPS_pH = dValue;
                            objMeasuredDataGlobal.latest_update_TN_communication = DateTime.Now;
                            //txtTNValue.Text = dValue.ToString("##0.00");
                            int status = 0;
                            //strAdditionInfor = strAdditionInfor.ToLower();
                            //if (strAdditionInfor.Contains("error"))
                            //{
                            //    status = INT_STATUS_INSTRUMENT_ERROR;
                            //    this.picTNStatus.BackgroundImage = global::DataLogger.Properties.Resources.Fault;
                            //}
                            //else if (strAdditionInfor.Contains("running"))
                            //{
                            //    status = INT_STATUS_NORMAL;
                            //    this.picTNStatus.BackgroundImage = global::DataLogger.Properties.Resources.Run_56x56;
                            //}
                            //else if (strAdditionInfor.Contains("normal"))
                            //{
                            //    status = INT_STATUS_NORMAL;
                            //    this.picTNStatus.BackgroundImage = global::DataLogger.Properties.Resources.Normal_status;
                            //}
                            //else if (strAdditionInfor.Contains("warning"))
                            //{
                            //    status = INT_STATUS_MAINTENANCE;
                            //}
                            //else if (strAdditionInfor.Contains("measuring"))
                            //{
                            //    status = INT_STATUS_NORMAL;
                            //}
                            //else if (strAdditionInfor.Contains("standby"))
                            //{
                            //    status = INT_STATUS_INSTRUMENT_ERROR;
                            //}
                            objMeasuredDataGlobal.TN_status = status = Convert.ToInt32(strInstrumentStatus.Trim());
                            tooltipTNInfo = strAdditionInfor;

                            //updateCell(CODE_TN, dValue, status);

                            //// read data to display
                            //byte[] temp = new byte[PACKET_LENGTH];
                            //Array.Copy(text, i, temp, 0, PACKET_LENGTH);
                            //result += ByteArrayToHexString(temp);
                            ////Remove this packet from temporary buffer
                            bufferIndex = i + PACKET_LENGTH;
                            i = i + PACKET_LENGTH - 1;
                            //this.picTPStatus.BackgroundImage = global::DataLogger.Properties.Resources.Normal_status;

                        }
                    }
                }
                else
                {
                    //if (text.Length < 3)
                    //{
                    //    this.picTNStatus.BackgroundImage = global::DataLogger.Properties.Resources.Fault;
                    //}
                }
                //Reset buffer
                TN_buffer_counter = 0;
                Array.Clear(TN_receive_buffer, 0, TN_receive_buffer.Length);
                //if (bufferIndex < text.Length)
                //{
                //    TN_buffer_counter = text.Length - bufferIndex;
                //    //Console.WriteLine("Buffer Counter: " + buffer_counter.ToString() + ", Data Length: " + text.Length);
                //    Array.Copy(text, bufferIndex, TN_receive_buffer, 0, TN_buffer_counter);
                //}

                //if (TN_buffer_counter > PACKET_LENGTH * 2)
                //{
                TN_rx_write = 0;
                TN_rx_counter = 0;
                TN_rx_buffer = null;

                TN_receive_buffer = new byte[2048];
                TN_buffer_counter = 0;
                //}
                updateMeasuredDataValue(objMeasuredDataGlobal);
            }
            catch //(Exception ex)
            {
                //MessageBox.Show("0002," + ex.Message);
            }

            if (result == "")
                return result;
            return result;
        }
        private string MPSParseData(byte[] text)
        {

            int bufferIndex = 0;
            string result = "";

            try
            {
                //if (station_status_data_type == ADAM_TEMP_HUMIDITY)
                //{
                if (station_status_data_type_4017 == ADAM_TEMP_HUMIDITY)
                {
                    //MessageBox.Show(test);
                    if (MPS_buffer_counter > 0)
                        text = Combine(MPS_receive_buffer, MPS_buffer_counter, text);
                    if (text.Length >= ADAM_TEMP_HUMIDITY_PACKET_LENGTH)
                    //if (text.Length >= MPS_PACKET_LENGTH)
                    {
                        for (int i = 0; i < text.Length - ADAM_TEMP_HUMIDITY_PACKET_LENGTH; i++)
                        {
                            if (text[i] == 0x0A &&
                                text[i + 1] == 0x04 &&
                                text[i + 2] == 0x18 &&
                                text[i + MPS_PACKET_LENGTH] == 0x0A &&
                                text[i + MPS_PACKET_LENGTH + 1] == 0x04 &&
                                text[i + MPS_PACKET_LENGTH + 2] == 0x18
                                )
                            {
                                // process data
                                //MessageBox.Show(ByteArrayToHexString(SubArray(text, i + 5, 4)));
                                Single valPH = ConvertHexToSingle(ByteArrayToHexString(SubArray(text, i + 5, 4)));
                                Single valEC = ConvertHexToSingle(ByteArrayToHexString(SubArray(text, i + 9, 4)));
                                Single valDO = ConvertHexToSingle(ByteArrayToHexString(SubArray(text, i + 13, 4)));
                                Single valTurbidity = ConvertHexToSingle(ByteArrayToHexString(SubArray(text, i + 17, 4)));
                                Single valORP = ConvertHexToSingle(ByteArrayToHexString(SubArray(text, i + 21, 4)));
                                Single valTemp = ConvertHexToSingle(ByteArrayToHexString(SubArray(text, i + 25, 4)));


                                result = string.Format("PH: {0}; EC: {1}; DO: {2}; Turbidity: {3}; ORP: {4}; Temp: {5}", valPH, valEC, valDO, valTurbidity, valORP, valTemp);

                                //if (rbtnMPS.Checked)
                                //{
                                //    byte[] subArray = new byte[MPS_PACKET_LENGTH];
                                //    Array.Copy(text, i, subArray, 0, MPS_PACKET_LENGTH);
                                //    //setRawText(ByteArrayToHexString(subArray));
                                //}

                                Double pHValue = Convert.ToDouble(valPH);
                                Double ECValue = Convert.ToDouble(valEC);
                                Double DoValue = Convert.ToDouble(valDO);
                                Double TurbidityValue = Convert.ToDouble(valTurbidity);
                                Double ORPValue = Convert.ToDouble(valORP);
                                Double TempValue = Convert.ToDouble(valTemp);
                                int status = 0;


                                objMeasuredDataGlobal.MPS_EC = ECValue;
                                objMeasuredDataGlobal.MPS_pH = pHValue;
                                MessageBox.Show(objMeasuredDataGlobal.MPS_pH.ToString());
                                objMeasuredDataGlobal.MPS_DO = DoValue;
                                objMeasuredDataGlobal.MPS_Turbidity = TurbidityValue;
                                objMeasuredDataGlobal.MPS_ORP = ORPValue;
                                objMeasuredDataGlobal.MPS_Temp = TempValue;
                                objMeasuredDataGlobal.MPS_status = status;
                                objMeasuredDataGlobal.latest_update_MPS_communication = DateTime.Now;
                                //// read data to display
                                //byte[] temp = new byte[PACKET_LENGTH];
                                //Array.Copy(text, i, temp, 0, PACKET_LENGTH);
                                //result += ByteArrayToHexString(temp);
                                ////Remove this packet from temporary buffer
                                bufferIndex = i + MPS_PACKET_LENGTH;
                                i = i + MPS_PACKET_LENGTH - 1;

                                //this.picMPSStatus.BackgroundImage = global::DataLogger.Properties.Resources.Normal_status;
                            }
                        }
                    }
                    else
                    {
                        if (text.Length < 3)
                        {
                            //this.picMPSStatus.BackgroundImage = global::DataLogger.Properties.Resources.Fault;
                            
                        }
                    }

                    updateMeasuredDataValue(objMeasuredDataGlobal);
                    //Reset buffer
                    MPS_buffer_counter = 0;
                    Array.Clear(MPS_receive_buffer, 0, MPS_receive_buffer.Length);
                    if (bufferIndex < text.Length)
                    {
                        MPS_buffer_counter = text.Length - bufferIndex;
                        //Console.WriteLine("Buffer Counter: " + buffer_counter.ToString() + ", Data Length: " + text.Length);
                        Array.Copy(text, bufferIndex, MPS_receive_buffer, 0, MPS_buffer_counter);
                    }

                    if (MPS_buffer_counter > MPS_PACKET_LENGTH * 2)
                    {
                        MPS_rx_write = 0;
                        MPS_rx_counter = 0;
                        MPS_rx_buffer = new byte[2048];

                        MPS_receive_buffer = new byte[2048];
                        MPS_buffer_counter = 0;
                    }
                }
                else
                {

                }
            }
            catch //(Exception ex)
            {
                //MessageBox.Show("MPS 0003," + ex.Message);
            }

            if (result == "")
                return result;
            return result;
        }
        private void ProcessDataSAMP(string text)
        {
            try
            {
                if (this.txtData.InvokeRequired)
                {
                    ProcessDataCallback d = new ProcessDataCallback(ProcessDataSAMP);
                    this.txtData.Invoke(d, new object[] { text });
                }
                else
                {
                    //if (txtData.Lines.Count() > 10)
                    //    txtData.Clear();
                    string temp1 = SAMPParseData(SAMP_rx_buffer);
                    //if (rbtnSAMP.Checked)
                    //{
                    //    if (!string.IsNullOrEmpty(temp1))
                    //    {
                    //        txtData.Text = temp1;
                    //    }

                    //}

                    //MessageBox.Show("000:" + txtData.Text);
                    //SAMP_rx_counter = 0;
                    //SAMP_rx_write = 0;
                }
            }
            catch
            {

            }

        }
        private string SAMPParseData(byte[] text)
        {
            //return "123123123123";
            int bufferIndex = 0;
            string result = "";

            try
            {
                int j = 0;
                if (text.Length >= SAMP_PACKET_LENGTH )
                {
                    //string raw_data = Encoding.ASCII.GetString(text);
                    //Console.WriteLine("data : " + raw_data.Length);
                    //Console.WriteLine(raw_data.Trim());
                    for (int i = 0; i < text.Length; i++)
                    {
                        j = i;
                        if (text[j] == 0x02 &&   //STX
                            text[j + 1] == 0x53 &&   //S
                            text[j + 2] == 0x41 &&   //A
                            text[j + SAMP_PACKET_LENGTH - 1 ] == 0x0D &&   //CR
                            text[j + SAMP_PACKET_LENGTH - 1 - 3] == 0x03   //ETX
                            )
                        {
                            // process data
                            //Console.WriteLine("TRUE2");
                            string equipment_name = "SAMPLER";
                            string response_time = Encoding.UTF8.GetString(SubArray(text, i + 1 + 4, 14));
                            string temp = Encoding.UTF8.GetString(SubArray(text, i + 1 + 4 + 14, 4)); //19-28
                            string bottle_position = Encoding.UTF8.GetString(SubArray(text, i + 1 + 4 + 14 + 10 + 2, 2));
                            string equipment_status = Encoding.UTF8.GetString(SubArray(text, i + 1 + 4 + 14 + 10, 2));
                            //string door_status = Encoding.UTF8.GetString(SubArray(text, i + 33, 2));
                            string ml = Encoding.UTF8.GetString(SubArray(text, i + 1 + 4 + 14 + 10 + 2 + 2, 6));
                            string sample = Encoding.UTF8.GetString(SubArray(text, i + 1 + 4 + 14 + 10 + 2 + 2 + 6, 1));
                            string E00 = Encoding.UTF8.GetString(SubArray(text, i + 1 + 4 + 14 + 10 + 2 + 2 + 6 + 1, 5));
                            string door_status = Encoding.UTF8.GetString(SubArray(text, i + 1 + 4 + 14 + 10 + 2 + 12, 1));
                            string addInfo = ml + sample + E00 + door_status;
                            result = string.Format("equipment_name: {0}; response_time: {1}; temp: {2}; bottle_position: {3}; door_status: {4}; equipment_status: {5}; ml : {6} ; sample : {7} ; E00 : {8}"
                                    , equipment_name, response_time, temp, bottle_position, door_status, equipment_status, ml, sample, E00);
                            //Console.WriteLine(result);
                            //water_sampler obj = new water_sampler();
                            objWaterSamplerGLobal.equipment_name = equipment_name;
                            objWaterSamplerGLobal.comm_port = SAMPComname;
                            objWaterSamplerGLobal.equipment_status = Convert.ToInt32(equipment_status);
                            objWaterSamplerGLobal.status_info = Convert.ToInt32(equipment_status);
                            try
                            {
                                objWaterSamplerGLobal.door_status = Convert.ToInt32(door_status);
                            }
                            catch
                            {
                                objWaterSamplerGLobal.door_status = -1;
                            }
                            objWaterSamplerGLobal.bottle_position = Convert.ToInt32(bottle_position);
                            objWaterSamplerGLobal.refrigeration_Temperature = Convert.ToDouble(temp);
                            objWaterSamplerGLobal.addInfo = addInfo;
                            objWaterSamplerGLobal.created = DateTime.Now;

                            datetime00 = DateTime.Now;
                            tooltipSAMPInfo = addInfo;
                            CultureInfo enUS = new CultureInfo("en-US");
                            DateTime datetimeValue = new DateTime();

                            DateTime.TryParseExact(response_time, "yyyyMMddHHmmss", enUS, DateTimeStyles.None, out datetimeValue);

                            objWaterSamplerGLobal.response_time = datetimeValue;

                        }
                    }
                }
                //Reset buffer
                SAMP_buffer_counter = 0;
                Array.Clear(SAMP_receive_buffer, 0, SAMP_receive_buffer.Length);

                SAMP_rx_write = 0;
                SAMP_rx_counter = 0;
                SAMP_rx_buffer = null;

                SAMP_receive_buffer = new byte[2048];
                ////Reset buffer
                //SAMP_buffer_counter = 0;
                //Array.Clear(SAMP_receive_buffer, 0, SAMP_receive_buffer.Length);
                //if (bufferIndex < text.Length)
                //{
                //    SAMP_buffer_counter = text.Length - bufferIndex;
                //    Array.Copy(text, bufferIndex, SAMP_receive_buffer, 0, SAMP_buffer_counter);
                //}

                //if (SAMP_buffer_counter > SAMP_PACKET_LENGTH * 2)
                //{
                //    SAMP_rx_write = 0;
                //    SAMP_rx_counter = 0;
                //    SAMP_rx_buffer = new byte[2048];

                //    SAMP_receive_buffer = new byte[2048];
                //    SAMP_buffer_counter = 0;
                //}
                objWaterSamplerGLobal.latest_update_communication = DateTime.Now;
                updateCellSamplerStatus(objWaterSamplerGLobal);
            }
            catch //(Exception ex)
            {
                //MessageBox.Show("SAMP 0003," + ex.Message);
            }

            if (result == "")
                return result;
            return result;
        }
        private void ProcessDataADAM(string text)
        {
            try
            {
                if (this.txtData.InvokeRequired)
                {
                    ProcessDataCallback d = new ProcessDataCallback(ProcessDataADAM);
                    this.txtData.Invoke(d, new object[] { text });
                }
                else
                {
                    string temp1 = ADAMParseData(ADAM405x_rx_buffer);
                    //if (rbtnStationStatus.Checked)
                    //{
                    //    if (!string.IsNullOrEmpty(temp1))
                    //    {
                    //        txtData.Text = temp1;
                    //    }

                    //}
                    //txtData.Text = temp1;
                    //MessageBox.Show("000:" + txtData.Text);
                    //ADAM405x_rx_counter = 0;
                    //ADAM405x_rx_write = 0;
                }
            }
            catch
            {

            }
        }
        public void writeLog(string content, string filename)
        {
            try
            {
                if (!File.Exists(filename))
                {
                    File.Create(filename);
                }

                TextWriter twr = new StreamWriter(filename, true);
                DateTime dt = new DateTime();
                dt = DateTime.Now;
                twr.Write(dt.ToString() + " : ");
                twr.WriteLine(content);
                twr.Close();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error: -" + ex.Message);
            }
        }
        private string ADAMParseData(byte[] text)
        {
            //int bufferIndex = 0;
            //tmrThreadingTimerStationStatus.Change(Timeout.Infinite, Timeout.Infinite);
            string result = "";
            try
            {
                int j = 0;
                //Console.Write(station_status_data_type +"\n");
                if (_GROUP == 1 || _GROUP == 2)
                {
                    //Console.WriteLine(text.Length);
                    if (text.Length >= ADAM_TEMP_HUMIDITY_PACKET_LENGTH)
                    {
                        for (int i = 0; i < text.Length - ADAM_TEMP_HUMIDITY_PACKET_LENGTH; i++)
                        {
                            j = i;
                            if (text[j] == 0x3E &&
                                text[j + ADAM_TEMP_HUMIDITY_PACKET_LENGTH - 1] == 0x0D
                                )
                            {
                                // process data
                                string raw_data = Encoding.UTF8.GetString(SubArray(text, i, ADAM_TEMP_HUMIDITY_PACKET_LENGTH));
                                result = String.Format("{0}-{1}", station_status_data_type_4017, string.Format("raw_data: {0}", raw_data));

                                //string[] str_input = raw_data.Split('+');
                                raw_data = raw_data.Split('>')[1];
                                string[] str_input = new string[8];
                                for (int k = 0; k < 8; k++)
                                {
                                    str_input[k] = raw_data.Substring(k * 7, 7);
                                }
                                //foreach (string str in str_input) {
                                //    Console.WriteLine(str);
                                //}
                                IEnumerable<module> ADAM4017Modules = null;
                                if (_GROUP == 1)
                                {
                                    //station_status_data_type == ADAM_4017_1;
                                    ADAM4017Modules = GlobalVar.moduleSettings.Where(m => m.module_id == CommonInfo.INT_ADAM_4017_1);
                                }
                                if (_GROUP == 2)
                                {
                                    ADAM4017Modules = GlobalVar.moduleSettings.Where(m => m.module_id == CommonInfo.INT_ADAM_4017_2);
                                }
                                int indexTemperature = 1;
                                int indexHumidity = 1;
                                int indexpH = 1;
                                int indexOrp = 1;
                                int indexTemp = 1;
                                int indexDO = 1;
                                int indexTurb = 1;
                                int indexCond = 1;

                                Double dec_temperature = -1;
                                Double dec_humidity = -1;
                                Double dec_pH = -1;
                                Double dec_Orp = -1;
                                Double dec_temp = -1;
                                Double dec_do = -1;
                                Double dec_turb = -1;
                                Double dec_cond = -1;

                                Double dec_raw_temperature;
                                Double dec_raw_humidity;
                                Double dec_raw_pH;
                                Double dec_raw_Orp;
                                Double dec_raw_temp;
                                Double dec_raw_do;
                                Double dec_raw_turb;
                                Double dec_raw_cond;
                                //Console.Write("Chuan bi vao vong for \n");
                                foreach (module itemModule in ADAM4017Modules)
                                {
                                    //Console.Write("Da vao vao vong for , chuan bi vao switch \n");
                                    switch (itemModule.item_name.ToLower().Trim())
                                    {
                                        case "temperature":
                                            indexTemperature = itemModule.channel_number;
                                            dec_raw_temperature = Convert.ToDouble(str_input[indexTemperature]);
                                            //dec_temperature = Calculator(dec_raw_temperature, itemModule);
                                            dec_temperature = (Double)(dec_raw_temperature - itemModule.input_min) * (Double)(itemModule.output_max / (itemModule.input_max - itemModule.input_min)) + itemModule.output_min + itemModule.off_set;
                                            break;
                                        case "humidity":
                                            indexHumidity = itemModule.channel_number;
                                            dec_raw_humidity = Convert.ToDouble(str_input[indexHumidity]);
                                            dec_humidity = Calculator(dec_raw_humidity, itemModule);
                                            //Console.Write("1 \t" + dec_humidity + "\n");
                                            break;
                                        case "ph":
                                            indexpH = itemModule.channel_number;
                                            //Console.Write("ph --- " + indexpH + "\n");
                                            //Console.Write("ph --- " + str_input[indexpH] + "\n");
                                            dec_raw_pH = Convert.ToDouble(str_input[indexpH]);
                                            dec_pH = Calculator(dec_raw_pH, itemModule);
                                            //Console.Write("ph --- " + itemModule.input_max + "\n");
                                            //Console.Write("ph --- " + itemModule.input_min + "\n");
                                            //Console.Write("ph --- " + itemModule.output_max + "\n");
                                            //Console.Write("ph --- " + itemModule.output_min + "\n");
                                            //Console.Write("ph --- " + itemModule.off_set + "\n");
                                            //Console.Write("ph --- " + (double)((double)(itemModule.output_min - itemModule.output_max) / (double)(itemModule.input_min - itemModule.input_max)) + "\n");
                                            //Console.Write("ph --- " + (dec_raw_pH - itemModule.input_min) + "\n");
                                            //Console.Write("ph --- " + dec_pH + "\n");
                                            break;
                                        case "orp":
                                            indexOrp = itemModule.channel_number;
                                            dec_raw_Orp = Convert.ToDouble(str_input[indexOrp]);
                                            dec_Orp = Calculator(dec_raw_Orp, itemModule);
                                            break;
                                        case "temp":
                                            indexTemp = itemModule.channel_number;
                                            //Console.Write("do --- " + dec_raw_do + "\n");
                                            dec_raw_temp = Convert.ToDouble(str_input[indexTemp]);
                                            dec_temp = Calculator(dec_raw_temp, itemModule);
                                            break;
                                        case "do":
                                            indexDO = itemModule.channel_number;
                                            dec_raw_do = Convert.ToDouble(str_input[indexDO]);

                                            //Console.Write("do --- " + indexDO + "\n");
                                            //Console.Write("do --- " + str_input[indexDO] + "\n");
                                            //Console.Write("do --- " + dec_raw_do + "\n");
                                            dec_do = Calculator(dec_raw_do, itemModule);
                                            break;
                                        case "turb":
                                            indexTurb = itemModule.channel_number;
                                            dec_raw_turb = Convert.ToDouble(str_input[indexTurb]);
                                            dec_turb = Calculator(dec_raw_turb, itemModule);
                                            break;
                                        case "cond":
                                            indexCond = itemModule.channel_number;
                                            dec_raw_cond = Convert.ToDouble(str_input[indexCond]);
                                            dec_cond = Calculator(dec_raw_cond, itemModule);
                                            break;
                                        default:
                                            break;
                                    }
                                    //Console.Write("Da thoat vong switch \n");
                                }

                                objStationStatusGlobal.module_Temperature = (dec_temperature);
                                //Console.Write("11112222 \t" + objStationStatusGlobal.module_Humidity + "\n");
                                objStationStatusGlobal.module_Humidity = (dec_humidity);
                                //Console.Write("11113333 \t" + objStationStatusGlobal.module_Humidity + "\n");
                                updateCellStationStatus(objStationStatusGlobal);
                                if (dec_cond != -1)
                                {
                                    objMeasuredDataGlobal.MPS_EC = dec_cond;
                                }
                                if (dec_pH != -1)
                                {
                                    objMeasuredDataGlobal.MPS_pH = dec_pH;
                                }
                                if (dec_do != -1)
                                {
                                    objMeasuredDataGlobal.MPS_DO = dec_do;
                                }
                                if (dec_turb != -1)
                                {
                                    objMeasuredDataGlobal.MPS_Turbidity = dec_turb;
                                }
                                if (dec_Orp != -1)
                                {
                                    objMeasuredDataGlobal.MPS_ORP = dec_Orp;
                                }
                                if (dec_temp != -1)
                                {
                                    objMeasuredDataGlobal.MPS_Temp = dec_temp;
                                }
                                objMeasuredDataGlobal.MPS_status = 0;
                                objMeasuredDataGlobal.latest_update_MPS_communication = DateTime.Now;
                                updateMeasuredDataValue(objMeasuredDataGlobal);
                                //if (_GROUP == 1)
                                //{
                                //    _GROUP = 2;
                                //}
                                //else
                                //{
                                //    _GROUP = 1;
                                //}

                                ADAM4017Sign = true;
                                break;

                            }
                        }
                    }
                }
                //else
                //{
                if (text.Length >= ADAM405x_PACKET_LENGTH)
                {
                    for (int i = 0; i < text.Length - ADAM405x_PACKET_LENGTH; i++)
                    {
                        j = i;
                        int n = 0;
                        if (text[j] == 0x21 && text[j + ADAM405x_PACKET_LENGTH - 1] == 0x0D)
                        {
                            n++;
                        }
                        if (text[j] == 0x21 &&
                            text[j + ADAM405x_PACKET_LENGTH - 1] == 0x0D
                            )
                        {
                            // process data
                            string raw_data = Encoding.UTF8.GetString(SubArray(text, i, ADAM405x_PACKET_LENGTH));
                            result = String.Format("{0}-{1}", station_status_data_type_405x, string.Format("raw_data: {0}", raw_data));
                            string str_input = "0x" + raw_data.Substring(3, 2);
                            string str_output = "0x" + raw_data.Substring(1, 2);
                            int intValue = Convert.ToInt32(str_input, 16);
                            int outValue = Convert.ToInt32(str_output, 16);
                            string s = raw_data.Substring(1, 4);
                            string binarystring = String.Join(String.Empty, s.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
                            string value = new string(binarystring.ToCharArray().Reverse().ToArray());
                            int intOutPutValue = Convert.ToInt32(str_output, 16);

                            result += " " + intOutPutValue + " " + intValue;
                            byte[] values = Encoding.UTF8.GetBytes(value);
                            switch (station_status_data_type_405x)
                            {
                                case ADAM_4050:
                                    byte[] ADAM4050 = SubArray(text, i, ADAM405x_PACKET_LENGTH);
                                    //Console.WriteLine("ADAM4050 : " + Encoding.UTF8.GetString(ADAM4050));
                                    result += " - " + ADAM_4050;

                                    var ADAM4050Modules = GlobalVar.moduleSettings.Where(m => m.module_id == CommonInfo.INT_ADAM_4050);
                                    foreach (module itemModule in ADAM4050Modules)
                                    {
                                        checkModuleValue(values, itemModule.channel_number, itemModule);
                                    }

                                    // update relay io control                                        
                                    objRelayIOControlGlobal.DO1_status = (((intOutPutValue % 16) & 1) > 0) ? 1 : 0;
                                    objRelayIOControlGlobal.DO2_status = (((intOutPutValue % 16) & 2) > 0) ? 1 : 0;
                                    objRelayIOControlGlobal.DO3_status = (((intOutPutValue % 16) & 4) > 0) ? 1 : 0;
                                    objRelayIOControlGlobal.DO4_status = (((intOutPutValue % 16) & 8) > 0) ? 1 : 0;
                                    objRelayIOControlGlobal.DO5_status = (((intOutPutValue / 16) & 1) > 0) ? 1 : 0;
                                    objRelayIOControlGlobal.DO6_status = (((intOutPutValue / 16) & 2) > 0) ? 1 : 0;
                                    objRelayIOControlGlobal.DO7_status = (((intOutPutValue / 16) & 4) > 0) ? 1 : 0;
                                    objRelayIOControlGlobal.DO8_status = (((intOutPutValue / 16) & 8) > 0) ? 1 : 0;

                                    result += "- END 4050";
                                    //ADAM405x_rx_buffer = ADAM405x_rx_buffer.Except(ADAM4050).ToArray();
                                    ADAM4050Sign = true;
                                    //Console.WriteLine(" ADAM4050Sign:TRUE");
                                    //Console.WriteLine(binarystring);
                                    break;
                                case ADAM_4051:
                                    byte[] ADAM4051 = SubArray(text, i, ADAM405x_PACKET_LENGTH);
                                    result += " - " + ADAM_4051;
                                    var ADAM4051Modules = GlobalVar.moduleSettings.Where(m => m.module_id == CommonInfo.INT_ADAM_4051);
                                    foreach (module itemModule in ADAM4051Modules)
                                    {
                                        checkModuleValue(values, itemModule.channel_number, itemModule);
                                    }
                                    result += "- 4051";
                                    //ADAM405x_rx_buffer = ADAM405x_rx_buffer.Except(ADAM4051).ToArray();
                                    ADAM4051Sign = true;
                                    //Console.WriteLine(" ADAM4051Sign:TRUE");
                                    //Console.WriteLine(binarystring);
                                    break;
                                default:
                                    break;
                            }
                            updateCellStationStatus(objStationStatusGlobal);
                            updateDOControl(objRelayIOControlGlobal);

                            break;

                        }
                    }
                }

                //}
                //Console.WriteLine(Encoding.UTF8.GetString(ADAM405x_rx_buffer));
                //Reset buffer
                if (((ADAM4050Sign == true) || (ADAM4051Sign == true)) && (ADAM4017Sign == true))
                {
                    ADAM405x_buffer_counter = 0;
                    Array.Clear(ADAM405x_receive_buffer, 0, ADAM405x_receive_buffer.Length);

                    ADAM405x_rx_write = 0;
                    ADAM405x_rx_counter = 0;
                    ADAM405x_rx_buffer = new byte[2048];

                    ADAM405x_receive_buffer = new byte[2048];
                    ADAM405x_buffer_counter = 0;

                    ADAM4050Sign = false;
                    ADAM4051Sign = false;
                    ADAM4017Sign = false;
                }
                //if ((ADAM4050Sign == true) && (ADAM4051Sign == true) && (ADAM4017Sign == true))
                //{
                //    ADAM405x_buffer_counter = 0;
                //    Array.Clear(ADAM405x_receive_buffer, 0, ADAM405x_receive_buffer.Length);
                //    ADAM405x_rx_write = 0;
                //    ADAM405x_rx_counter = 0;
                //    ADAM405x_rx_buffer = null;
                //    ADAM405x_receive_buffer = new byte[2048];
                //    ADAM405x_buffer_counter = 0;
                //    ADAM4050Sign = false;
                //    ADAM4051Sign = false;
                //    ADAM4017Sign = false;
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine("ADAM : " + ex.StackTrace);
            }
            if (result == "")
                return result;
            return result;
        }
        public static string StringToByteArray(string hexstring)
        {
            return String.Join(String.Empty, hexstring
                .Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
        }
        //private bool checkModuleValue(int intValue, int channel_number)
        //{
        //    switch (channel_number)
        //    {
        //        case 0:
        //            break;
        //        case 1:
        //            return ((intValue % 16) & 1) > 0;
        //        case 2:
        //            return ((intValue % 16) & 2) > 0;
        //        case 3:
        //            return ((intValue % 16) & 4) > 0;
        //        case 4:
        //            return ((intValue % 16) & 8) > 0;
        //        case 5:
        //            return ((intValue / 16) & 1) > 0;
        //        case 6:
        //            return ((intValue / 16) & 2) > 0;
        //        case 7:
        //            return ((intValue / 16) & 4) > 0;
        //        case 8:
        //            return ((intValue / 16) & 8) > 0;
        //        default:
        //            break;
        //    }
        //    return false;
        //}

        private int checkModuleValue(byte[] values, int channel_number, module objModule)
        {
            int result = 0;
            bool checkWithChannelNumber = false;
            switch (channel_number)
            {
                case 0:
                    if (values[0] == 49) { checkWithChannelNumber = true; }
                    else { checkWithChannelNumber = false; }
                    break;
                case 1:
                    if (values[1] == 49) { checkWithChannelNumber = true; }
                    else { checkWithChannelNumber = false; }
                    break;
                case 2:
                    if (values[2] == 49) { checkWithChannelNumber = true; }
                    else { checkWithChannelNumber = false; }
                    break;
                case 3:
                    if (values[3] == 49) { checkWithChannelNumber = true; }
                    else { checkWithChannelNumber = false; }
                    break;
                case 4:
                    if (values[4] == 49) { checkWithChannelNumber = true; }
                    else { checkWithChannelNumber = false; }
                    break;
                case 5:
                    if (values[5] == 49) { checkWithChannelNumber = true; }
                    else { checkWithChannelNumber = false; }
                    break;
                case 6:
                    if (values[6] == 49) { checkWithChannelNumber = true; }
                    else { checkWithChannelNumber = false; }
                    break;
                case 7:
                    if (values[7] == 49) { checkWithChannelNumber = true; }
                    else { checkWithChannelNumber = false; }
                    break;
                case 8:
                    if (values[8] == 49) { checkWithChannelNumber = true; }
                    else { checkWithChannelNumber = false; }
                    break;
                case 9:
                    if (values[9] == 49) { checkWithChannelNumber = true; }
                    else { checkWithChannelNumber = false; }
                    break;
                case 10:
                    if (values[10] == 49) { checkWithChannelNumber = true; }
                    else { checkWithChannelNumber = false; }
                    break;
                case 11:
                    if (values[11] == 49) { checkWithChannelNumber = true; }
                    else { checkWithChannelNumber = false; }
                    break;
                case 12:
                    if (values[12] == 49) { checkWithChannelNumber = true; }
                    else { checkWithChannelNumber = false; }
                    break;
                case 13:
                    if (values[13] == 49) { checkWithChannelNumber = true; }
                    else { checkWithChannelNumber = false; }
                    break;
                case 14:
                    if (values[14] == 49) { checkWithChannelNumber = true; }
                    else { checkWithChannelNumber = false; }
                    break;
                case 15:
                    if (values[15] == 49) { checkWithChannelNumber = true; }
                    else { checkWithChannelNumber = false; }
                    break;
                case 16:
                    if (values[16] == 49) { checkWithChannelNumber = true; }
                    else { checkWithChannelNumber = false; }
                    break;
                default:
                    break;
            }

            switch (objModule.item_name.ToLower().Trim())
            {
                case "power":
                    objStationStatusGlobal.module_Power = checkWithChannelNumber ? 1 : 0;
                    break;
                case "ups":
                    objStationStatusGlobal.module_UPS = checkWithChannelNumber ? 1 : 0;
                    break;
                case "door":
                    objStationStatusGlobal.module_Door = checkWithChannelNumber ? 1 : 0;
                    break;
                case "fire":
                    objStationStatusGlobal.module_Fire = checkWithChannelNumber ? 1 : 0;
                    break;
                case "flow":
                    objStationStatusGlobal.module_Flow = checkWithChannelNumber ? 1 : 0;
                    break;
                case "pump (l) a/m":
                    objStationStatusGlobal.module_PumpLAM = checkWithChannelNumber ? 1 : 0;
                    break;
                case "pump (l) r/s":
                    objStationStatusGlobal.module_PumpLRS = checkWithChannelNumber ? 1 : 0;
                    break;
                case "pump (l) flt":
                    objStationStatusGlobal.module_PumpLFLT = checkWithChannelNumber ? 1 : 0;
                    break;
                case "pump (r) a/m":
                    objStationStatusGlobal.module_PumpRAM = checkWithChannelNumber ? 1 : 0;
                    break;
                case "pump (r) r/s":
                    objStationStatusGlobal.module_PumpRRS = checkWithChannelNumber ? 1 : 0;
                    break;
                case "pump (r) flt":
                    objStationStatusGlobal.module_PumpRFLT = checkWithChannelNumber ? 1 : 0;
                    break;
                case "air1":
                    objStationStatusGlobal.module_air1 = checkWithChannelNumber ? 1 : 0;
                    break;
                case "air2":
                    objStationStatusGlobal.module_air2 = checkWithChannelNumber ? 1 : 0;
                    break;
                case "cleaning":
                    objStationStatusGlobal.module_cleaning = checkWithChannelNumber ? 1 : 0;
                    break;
                case "temperature":

                    break;
                case "humidity":

                    break;
                default:
                    break;
            }

            return result;
        }

        private static void requestInforADAM(SerialPort com, string ADAM)
        {
            if (com.IsOpen)
            {
                //frmNewMain _frmNewMain = new frmNewMain();
                //if (_frmNewMain.tmrThreadingTimerStationStatus.) {

                //    _frmNewMain.tmrThreadingTimerStationStatus.Dispose();
                //}
                byte[] packet;
                switch (ADAM)
                {
                    case ADAM_4050: // USED FOR DI/O (BOTH IN OUT CONTROL)
                        // Module 02
                        packet = new byte[6];
                        //Fill to packet
                        packet[0] = 0x02; // STX
                        packet[1] = 0x24; // '$'
                        packet[2] = 0x30; // '0'
                        packet[3] = 0x32; // '2'
                        packet[4] = 0x36; // '6'
                        packet[5] = 0x0D; //
                        com.Write(packet, 0, packet.Length);
                        break;
                    case ADAM_4051:
                        // Module 03
                        packet = new byte[6];
                        //Fill to packet
                        packet[0] = 0x02; // STX
                        packet[1] = 0x24; // '$'
                        packet[2] = 0x30; // '0'
                        packet[3] = 0x33; // '3'
                        packet[4] = 0x36; // '6'
                        packet[5] = 0x0D; //
                        com.Write(packet, 0, packet.Length);
                        break;
                    case ADAM_4017_1:
                        // Module 01
                        packet = new byte[5];
                        //Fill to packet
                        packet[0] = 0x02; // STX
                        packet[1] = 0x23; // '#'
                        packet[2] = 0x30; // '0'
                        if (_GROUP == 1)
                        {
                            packet[3] = 0x31; // '1'
                        }
                        else
                        {
                            packet[3] = 0x34; // '4'
                        }
                        //packet[3] = 0x31; // '1'
                        packet[4] = 0x0D; //
                        com.Write(packet, 0, packet.Length);
                        break;
                    case ADAM_4017_2:
                        // Module 02
                        packet = new byte[5];
                        //Fill to packet
                        packet[0] = 0x02; // STX
                        packet[1] = 0x23; // '#'
                        packet[2] = 0x30; // '0'
                        if (_GROUP == 1)
                        {
                            packet[3] = 0x31; // '1'
                        }
                        else
                        {
                            packet[3] = 0x34; // '4'
                        }
                        packet[4] = 0x0D; //
                        com.Write(packet, 0, packet.Length);

                        break;
                    default:
                        break;
                }
            }

        }

        private void reqestForSettingIOControl(SerialPort com, byte DO_number, byte on_off_IO)
        {
            if (com.IsOpen)
            {
                station_status_data_type_405x = ADAM_4050;
                byte[] packet = new byte[9];
                //Fill to packet
                packet[0] = 0x02; // STX
                packet[1] = 0x23; // '#'
                packet[2] = 0x30; // '0'
                packet[3] = 0x32; // '2'
                // set CO number setting
                packet[4] = 0x31; // '1'
                packet[5] = DO_number; // '0' --> '7', 0x30 --> 0x37
                // set on off
                packet[6] = 0x30; // '0'
                packet[7] = on_off_IO; // '0', '1' --> 0x30, 0x31

                packet[8] = 0x0D; //
                com.Write(packet, 0, packet.Length);
            }

            indexSelectionStation = 2;
            station_status_data_type_405x = ADAM_4050;
            //requestInforADAM(serialPortADAM, ADAM_4050);
        }
        #endregion

        #region threading timer
        public int indexSelection = 0;
        private void tmrThreadingTimer_TimerCallback(object state)
        {
            if (is_close_form)
            {
                try
                {
                    this.Close();
                    //MessageBox.Show("123");
                    if (System.Windows.Forms.Application.MessageLoop)
                    {
                        // WinForms app
                        System.Windows.Forms.Application.Exit();
                    }
                    else
                    {
                        // Console app
                        System.Environment.Exit(Environment.ExitCode);
                    }
                }
                catch
                {

                }
            }
            try {
                setText(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                indexSelection = (indexSelection + 1) % 5;
                switch (indexSelection)
                {
                    case 0: // TOC
                        //Console.WriteLine("request TOC");
                        if (GlobalVar.calibrateTOCStatus == CommonInfo.CALIBRATION_STATUS_DONE
                            || GlobalVar.calibrateTOCStatus == CommonInfo.CALIBRATION_STATUS_STOP)
                        {
                            try
                            {
                                if (!this.serialPortTOC.IsOpen)
                                {
                                    this.serialPortTOC.Open();
                                }
                                requestInfor(serialPortTOC);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.StackTrace);
                            }
                        }
                        break;
                    case 1: // TP
                        //Console.WriteLine("request TP");
                        if (GlobalVar.calibrateTPStatus == CommonInfo.CALIBRATION_STATUS_DONE
                            || GlobalVar.calibrateTPStatus == CommonInfo.CALIBRATION_STATUS_STOP)
                        {
                            try
                            {
                                if (!this.serialPortTP.IsOpen)
                                {
                                    this.serialPortTP.Open();
                                }
                                requestInfor(serialPortTP);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.StackTrace);
                            }
                        }
                        //requestInfor(serialPortTP);
                        break;
                    case 2: // TN
                        //Console.WriteLine("request TN");
                        if (GlobalVar.calibrateTNStatus == CommonInfo.CALIBRATION_STATUS_DONE
                            || GlobalVar.calibrateTNStatus == CommonInfo.CALIBRATION_STATUS_STOP)
                        {
                            try
                            {
                                if (!this.serialPortTN.IsOpen)
                                {
                                    this.serialPortTN.Open();
                                }

                                requestInfor(serialPortTN);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.StackTrace);
                            }
                        }
                        //requestInfor(serialPortTN);
                        break;
                    case 3: // MPS
                            //requestInforMPS(serialPortMPS);
                        break;
                    case 4: // SAMPLER
                            //Console.WriteLine("request sampler");
                        requestInforSAMPLER(serialPortSAMP);
                        break;
                    default:
                        break;

                }
            } catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void tmrThreadingTimer_HeadingTime_TimerCallback(object state)
        {
            if (is_close_form)
            {
                try
                {
                    this.Close();
                    //MessageBox.Show("123");
                    if (System.Windows.Forms.Application.MessageLoop)
                    {
                        // WinForms app
                        System.Windows.Forms.Application.Exit();
                    }
                    else
                    {
                        // Console app
                        System.Environment.Exit(Environment.ExitCode);
                    }
                }
                catch
                {

                }
            }
            string time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            setTextHeadingTimer(time);
            settingForLoginStatus();

        }

        public int indexSelectionStation = 0;
        public static string station_status_data_type_4017 = "";
        public static string station_status_data_type_405x = "";
        private void tmrThreadingTimerStationStatus_TimerCallback(object state)
        {
            if (is_close_form)
            {
                try
                {
                    this.Close();
                    //MessageBox.Show("123");
                    if (System.Windows.Forms.Application.MessageLoop)
                    {
                        // WinForms app
                        System.Windows.Forms.Application.Exit();
                    }
                    else
                    {
                        // Console app
                        System.Environment.Exit(Environment.ExitCode);
                    }
                }
                catch
                {
                }
            }
            //setText(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            indexSelectionStation = (indexSelectionStation + 1) % 4;
            switch (indexSelectionStation)
            {
                case 0: // 4050
                    station_status_data_type_405x = ADAM_4050;
                    //requestInforADAM(serialPortADAM, ADAM_4050);
                    //tmrThreadingTimerStationStatus.Change(Timeout.Infinite, Timeout.Infinite);
                    break;
                case 1: // 4051
                    station_status_data_type_405x = ADAM_4051;
                    //requestInforADAM(serialPortADAM, ADAM_4051);
                    //Console.WriteLine("1");
                    break;
                case 2: // 
                    station_status_data_type_4017 = ADAM_4017_1;
                    _GROUP = 1;
                    //requestInforADAM(serialPortADAM, ADAM_4017_1);
                    //tmrThreadingTimerStationStatus.Change(Timeout.Infinite, Timeout.Infinite);
                    break;
                case 3: // 
                    _GROUP = 2;
                    station_status_data_type_4017 = ADAM_4017_2;
                    //requestInforADAM(serialPortADAM, ADAM_4017_2);
                    //tmrThreadingTimerStationStatus.Change(Timeout.Infinite, Timeout.Infinite);
                    break;
                default:
                    break;
            }
        }

        public static int countingIndex5Minute = 0;
        private void tmrThreadingTimerFor5Minute_TimerCallback(object state)
        {
            try
            {
                if (is_close_form)
                {
                    try
                    {
                        this.Close();
                        //MessageBox.Show("123");
                        if (System.Windows.Forms.Application.MessageLoop)
                        {
                            // WinForms app
                            System.Windows.Forms.Application.Exit();
                        }
                        else
                        {
                            // Console app
                            System.Environment.Exit(Environment.ExitCode);
                        }
                    }
                    catch
                    {

                    }
                }
                // 50 seconds save current time to datavalue table
                if (countingIndex5Minute < 2)
                {
                    countingIndex5Minute++;
                    return;
                }
                else
                {

                }
                checkAllCommunication();

                data_value objDataValue = new data_value();
                // station status
                objDataValue.module_Door = objStationStatusGlobal.module_Door;
                objDataValue.module_Fire = objStationStatusGlobal.module_Fire;
                objDataValue.module_Flow = objStationStatusGlobal.module_Flow;
                objDataValue.module_Humidity = objStationStatusGlobal.module_Humidity;
                objDataValue.module_Power = objStationStatusGlobal.module_Power;
                objDataValue.module_PumpLAM = objStationStatusGlobal.module_PumpLAM;
                objDataValue.module_PumpLFLT = objStationStatusGlobal.module_PumpLFLT;
                objDataValue.module_PumpLRS = objStationStatusGlobal.module_PumpLRS;
                objDataValue.module_PumpRAM = objStationStatusGlobal.module_PumpRAM;
                objDataValue.module_PumpRFLT = objStationStatusGlobal.module_PumpRFLT;
                objDataValue.module_PumpRRS = objStationStatusGlobal.module_PumpRRS;
                objDataValue.module_air1 = objStationStatusGlobal.module_air1;
                objDataValue.module_air2 = objStationStatusGlobal.module_air2;
                objDataValue.module_cleaning = objStationStatusGlobal.module_cleaning;

                objDataValue.module_Temperature = objStationStatusGlobal.module_Temperature;
                objDataValue.module_UPS = objStationStatusGlobal.module_UPS;
                // MPS
                if (objMeasuredDataGlobal.MPS_status < 0)
                {
                    objMeasuredDataGlobal.MPS_status = CommonInfo.INT_STATUS_COMMUNICATION_ERROR;
                    objMeasuredDataGlobal.MPS_DO = -1;
                    objMeasuredDataGlobal.MPS_EC = -1;
                    objMeasuredDataGlobal.MPS_pH = -1;
                    objMeasuredDataGlobal.MPS_Temp = -1;
                    objMeasuredDataGlobal.MPS_ORP = -1;
                    objMeasuredDataGlobal.MPS_Turbidity = -1;
                }
                objDataValue.MPS_DO = objMeasuredDataGlobal.MPS_DO;
                objDataValue.MPS_DO_status = objMeasuredDataGlobal.MPS_status;
                objDataValue.MPS_EC = objMeasuredDataGlobal.MPS_EC;
                objDataValue.MPS_EC_status = objMeasuredDataGlobal.MPS_status;
                objDataValue.MPS_ORP = objMeasuredDataGlobal.MPS_ORP;
                objDataValue.MPS_ORP_status = objMeasuredDataGlobal.MPS_status;

                objDataValue.MPS_Temp = objMeasuredDataGlobal.MPS_Temp;
                objDataValue.MPS_Temp_status = objMeasuredDataGlobal.MPS_status;
                objDataValue.MPS_Turbidity = objMeasuredDataGlobal.MPS_Turbidity;
                objDataValue.MPS_Turbidity_status = objMeasuredDataGlobal.MPS_status;
                objDataValue.MPS_status = objMeasuredDataGlobal.MPS_status;

                //objDataValue.MPS_pH = objMeasuredDataGlobal.MPS_pH;
                //objDataValue.MPS_pH_status = objMeasuredDataGlobal.MPS_status;

                if (objMeasuredDataGlobal.MPS_pH_status < 0)
                {
                    objMeasuredDataGlobal.MPS_pH_status = CommonInfo.INT_STATUS_COMMUNICATION_ERROR;
                    objMeasuredDataGlobal.MPS_pH_status = -1;
                }
                if (objMeasuredDataGlobal.MPS_SS_status < 0)
                {
                    objMeasuredDataGlobal.MPS_SS_status = CommonInfo.INT_STATUS_COMMUNICATION_ERROR;
                    objMeasuredDataGlobal.MPS_SS_status = -1;
                }
                // TN, TOC, TP
                if (objMeasuredDataGlobal.TN_status < 0)
                {
                    objMeasuredDataGlobal.TN_status = CommonInfo.INT_STATUS_COMMUNICATION_ERROR;
                    objMeasuredDataGlobal.TN = -1;
                }
                if (objMeasuredDataGlobal.TOC_status < 0)
                {
                    objMeasuredDataGlobal.TOC_status = CommonInfo.INT_STATUS_COMMUNICATION_ERROR;
                    objMeasuredDataGlobal.TOC = -1;
                }
                if (objMeasuredDataGlobal.TP_status < 0)
                {
                    objMeasuredDataGlobal.TP_status = CommonInfo.INT_STATUS_COMMUNICATION_ERROR;
                    objMeasuredDataGlobal.TP = -1;
                }
                objDataValue.TN = objMeasuredDataGlobal.TN;
                objDataValue.TN_status = objMeasuredDataGlobal.TN_status;
                objDataValue.TOC = objMeasuredDataGlobal.TOC;
                objDataValue.TOC_status = objMeasuredDataGlobal.TOC_status;
                objDataValue.TP = objMeasuredDataGlobal.TP;
                objDataValue.TP_status = objMeasuredDataGlobal.TP_status;

                objDataValue.MPS_pH = objMeasuredDataGlobal.MPS_pH;
                objDataValue.MPS_pH_status = objMeasuredDataGlobal.MPS_pH_status;
                objDataValue.MPS_SS = objMeasuredDataGlobal.MPS_SS;
                objDataValue.MPS_SS_status = objMeasuredDataGlobal.MPS_SS_status;
                // water sampler
                objDataValue.bottle_position = objWaterSamplerGLobal.bottle_position;
                objDataValue.door_status = objWaterSamplerGLobal.door_status;
                objDataValue.equipment_status = objWaterSamplerGLobal.equipment_status;
                objDataValue.refrigeration_temperature = objWaterSamplerGLobal.refrigeration_Temperature;

                // Time
                objDataValue.stored_date = DateTime.Now;
                objDataValue.stored_hour = DateTime.Now.Hour;
                objDataValue.stored_minute = DateTime.Now.Minute;
                if (GlobalVar.isMaintenanceStatus && GlobalVar.maintenanceLog.pumping_system == 1)
                {
                    objDataValue.pumping_system_status = INT_STATUS_MAINTENANCE;
                    //objDataValue.station_status = INT_STATUS_MAINTENANCE;
                }
                Console.WriteLine("SS" + objDataValue.MPS_SS);
                Console.WriteLine("pH" + objDataValue.MPS_pH);
                //// save to data value table
                if (new data_value_repository().add(ref objDataValue) > 0)
                {
                    // ok --> add to 5 _minute data
                    data_value objAdd5Minute = objCalCulationDataValue5Minute.addNewObjFor5Minute(objDataValue);
                    if (objAdd5Minute != null)
                    {
                        // add to 60 _minute data
                        objCalCulationDataValue60Minute.addNewObjFor60Minute(objAdd5Minute);
                    }
                    else
                    {
                        // do nothing
                    }
                }
                else
                {
                    // fail
                }

                // cheking, calculating for saveving to datavalue 5 mintue table from current data
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

        }

        private void tmrThreadingTimerFor60Minute_TimerCallback(object state)
        {
            if (is_close_form)
            {
                try
                {
                    this.Close();
                    //MessageBox.Show("123");
                    if (System.Windows.Forms.Application.MessageLoop)
                    {
                        // WinForms app
                        System.Windows.Forms.Application.Exit();
                    }
                    else
                    {
                        // Console app
                        System.Environment.Exit(Environment.ExitCode);
                    }
                }
                catch
                {

                }
            }
            // checking, calculating for save ving to data value 10 _minute table from 5 _minute data
        }

        private static void requestInfor(SerialPort com)
        {
            try
            {
                if (com.IsOpen)
                {
                    byte[] packet = new byte[9];
                    //Fill to packet
                    packet[0] = 0x02;//STX

                    packet[1] = 0x44;//D
                    packet[2] = 0x41;//A
                    packet[3] = 0x54;//T
                    packet[4] = 0x41;//A
                    packet[5] = 0x03;//ETX
                    packet[6] = 0x31;//CHK
                    packet[7] = 0x3F;//CHK
                    packet[8] = 0x0D;//CR

                    com.Write(packet, 0, 9);
                }
            }
            catch
            {

            }

        }

        private static void requestInforMPS(SerialPort com)
        {
            try
            {
                if (com.IsOpen)
                {
                    // $0A$04$00$00$00$0C$F1$74
                    byte[] packet = new byte[9];
                    //Fill to packet
                    packet[0] = 0x0A;

                    packet[1] = 0x04;
                    packet[2] = 0x00;
                    packet[3] = 0x00;
                    packet[4] = 0x00;
                    packet[5] = 0x0C;
                    packet[6] = 0xF1;
                    packet[7] = 0x74;

                    com.Write(packet, 0, 8);
                }
            }
            catch
            {

            }

        }

        public static void requestInforSAMPLER(SerialPort com)
        {
            try
            {
                if (com.IsOpen)
                {
                    byte[] packet = new byte[11];
                    //Fill to packet
                    packet[0] = 0x02; // STX

                    packet[1] = 0x53; // 'S'
                    packet[2] = 0x41; // 'A'
                    packet[3] = 0x4D; // 'M'
                    packet[4] = 0x50; // 'P'
                    packet[5] = 0x31; // 1
                    packet[6] = 0x30; // 0
                    packet[7] = 0x03; // ETX
                    packet[8] = 0x39; //9
                    packet[9] = 0x37; //7
                    packet[10] = 0x0D;//
                    com.Write(packet, 0, 11);
                }
            }
            catch
            {

            }
            //public byte[] GetSapmlerStatusInfor()
            //{
            //    string str2 = this.MyPort.ReadExisting();
            //    string s = "\x0002";
            //    s = s + "SAMP10" + "\x0003";
            //    byte[] bytes = Encoding.Default.GetBytes(s);
            //    s = s + this.Checksum(bytes) + "\r";
            //    bytes = Encoding.Default.GetBytes(s);
            //    try
            //    {
            //        this.MyPort.Write(bytes, 0, bytes.Length);
            //    }
            //    catch (Exception exception1)
            //    {
            //        ProjectData.SetProjectError(exception1);
            //        Exception exception = exception1;
            //        ProjectData.ClearProjectError();
            //        return null;
            //    }
            //    Thread.Sleep(0x3e8);
            //    str2 = Conversions.ToString(Strings.Chr(this.MyPort.ReadByte()));
            //    if ((Strings.Asc(str2) != 0x15) && (Strings.Asc(str2) == 2))
            //    {
            //        byte[] buffer = new byte[0x56];
            //        this.MyPort.Read(buffer, 0, buffer.Length);
            //        this._Sample_Info = buffer;
            //        return buffer;
            //    }
            //    return null;
            //}


        }

        public static void requestAutoSAMPLER(SerialPort com)
        {
            try
            {
                if (com.IsOpen)
                {
                    byte[] packet = new byte[11];
                    //Fill to packet
                    packet[0] = 0x02; // STX

                    packet[1] = 0x53; // 'S'
                    packet[2] = 0x41; // 'A'
                    packet[3] = 0x4D; // 'M'
                    packet[4] = 0x50; // 'P'
                    packet[5] = 0x30; // 0
                    packet[6] = 0x30; // 0
                    packet[7] = 0x03; // ETX
                    packet[8] = 0x39; // '9'
                    packet[9] = 0x36; // '6'
                    packet[10] = 0x0D;//
                    com.Write(packet, 0, 11);
                }
            }
            catch
            {

            }

            //public bool SendSapmlingCmd()
            //{
            //    string str2 = this.MyPort.ReadExisting();
            //    string s = "\x0002";
            //    s = s + "SAMP00" + "\x0003";
            //    byte[] bytes = Encoding.Default.GetBytes(s);
            //    s = s + this.Checksum(bytes) + "\r";
            //    bytes = Encoding.Default.GetBytes(s);
            //    try
            //    {
            //        this.MyPort.Write(bytes, 0, bytes.Length);
            //    }
            //    catch (Exception exception1)
            //    {
            //        ProjectData.SetProjectError(exception1);
            //        Exception exception = exception1;
            //        ProjectData.ClearProjectError();
            //        return false;
            //    }
            //    Thread.Sleep(200);
            //    str2 = Conversions.ToString(Strings.Chr(this.MyPort.ReadByte()));
            //    if ((Strings.Asc(str2) != 6) && (Strings.Asc(str2) == 0x15))
            //    {
            //        return false;
            //    }
            //    return true;
            //}
        }
        public static void doSAMP()
        {
            //requestAutoSAMPLER(serialPortSAMP);
        }

        #endregion

        #region Utility
        public string HEX_Coding(string aHex)
        {
            switch (aHex)
            {
                case "A":
                    return ":";

                case "B":
                    return ";";

                case "C":
                    return "<";

                case "D":
                    return "=";

                case "E":
                    return ">";

                case "F":
                    return "?";
            }
            return aHex;
        }
        private string Checksum(byte[] ByteArray)
        {
            int num = 0;
            int num2 = ByteArray.Length - 1;
            for (int i = 0; i <= num2; i++)
            {
                num += ByteArray[i];
            }
            num = num % 0x100;
            return (this.HEX_Coding(((int)(num / 0x10)).ToString("X")) + this.HEX_Coding(((int)(num % 0x10)).ToString("X")));
        }

        /// <summary>
        /// Convert Byte Array to Hexa String
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            try
            {
                foreach (byte b in data)
                    sb.Append(Convert.ToString(b, 16).PadLeft(2, '0') + "");
            }
            catch (Exception)
            {
                return "Error";
            }
            return sb.ToString().ToUpper();
        }
        /// <summary>
        /// Convert Hex string to single number
        /// </summary>
        /// <param name="hexVal"></param>
        /// <returns></returns>
        private static Single ConvertHexToSingle(string hexVal)
        {
            try
            {
                int i = 0, j = 0;
                byte[] bArray = new byte[4];


                for (i = 0; i <= hexVal.Length - 1; i += 2)
                {
                    bArray[j] = Byte.Parse(hexVal[i].ToString() + hexVal[i + 1].ToString(), System.Globalization.NumberStyles.HexNumber);
                    j += 1;
                }
                Array.Reverse(bArray);
                Single s = BitConverter.ToSingle(bArray, 0);
                return (s);
            }
            catch (Exception ex)
            {
                throw new FormatException("The supplied hex value is either empty or in an incorrect format. Use the " +
                "following format: 00000000", ex);
            }
        }
        /// <summary>
        /// get subarray
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] SubArray(byte[] data, int index, int length)
        {
            byte[] result = new byte[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
        /// <summary>
        /// Convert byte array to string
        /// </summary>
        /// <param name="ba"></param>
        /// <returns></returns>
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
        /// <summary>
        /// combine 2 array
        /// </summary>
        /// <param name="first"></param>
        /// <param name="first_length"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        private static byte[] Combine(byte[] first, int first_length, byte[] second)
        {
            byte[] ret = new byte[first_length + second.Length];
            try
            {
                Buffer.BlockCopy(first, 0, ret, 0, first_length);
                Buffer.BlockCopy(second, 0, ret, first_length, second.Length);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("0003," + ex.Message);
            }


            return ret;
        }
        #endregion

        #region update data

        private void updateCellStationStatus(station_status obj)
        {
            try
            {
                //if (obj.module_Power > -1)
                //{
                //    //dgvDoControl["CurrentData", 0].Value = (obj.module_Power == 1) ? "NORMAL" : "NOT NORMAL";

                //    if (obj.module_Power == 1)
                //    {
                //        this.picStationStatusMainPower.BackgroundImage = global::DataLogger.Properties.Resources.on_icon_68x68;
                //    }
                //    else
                //    {
                //        this.picStationStatusMainPower.BackgroundImage = global::DataLogger.Properties.Resources.off_icon_68x68;
                //    }
                //}
                //if (obj.module_UPS > -1)
                //{
                //    //dgvDoControl["CurrentData", 1].Value = (obj.module_UPS == 1) ? "NORMAL" : "NOT NORMAL";
                //    if (obj.module_UPS == 1)
                //    {
                //        this.picStationStatusUPS.BackgroundImage = global::DataLogger.Properties.Resources.on_icon_68x68;
                //    }
                //    else
                //    {
                //        this.picStationStatusUPS.BackgroundImage = global::DataLogger.Properties.Resources.off_icon_68x68;
                //    }
                //}
                //if (obj.module_Door > -1)
                //{
                //    //dgvDoControl["CurrentData", 2].Value = (obj.module_Door == 1) ? "CLOSE" : "OPEN";

                //    if (obj.module_Door == 1)
                //    {
                //        this.picStationStatusMainDoor.BackgroundImage = global::DataLogger.Properties.Resources.door_close_68x68;
                //    }
                //    else
                //    {
                //        this.picStationStatusMainDoor.BackgroundImage = global::DataLogger.Properties.Resources.door_open_68x68;
                //    }
                //}
                //if (obj.module_Fire > -1)
                //{
                //    //dgvDoControl["CurrentData", 3].Value = (obj.module_Fire == 1) ? "NORMAL" : "OFF";
                //    if (obj.module_Fire == 0)
                //    {
                //        this.picStationStatusFireDetector.BackgroundImage = global::DataLogger.Properties.Resources.house_normal_68x68;
                //    }
                //    else
                //    {
                //        this.picStationStatusFireDetector.BackgroundImage = global::DataLogger.Properties.Resources.house_fire_68x68;
                //    }
                //}
                //if (obj.module_Flow > -1)
                //{
                //    //dgvDoControl["CurrentData", 4].Value = (obj.module_Flow == 1) ? "NORMAL" : "OFF";

                //    if (obj.module_Flow == 1)
                //    {
                //        this.pictureBoxWater.Image = global::DataLogger.Properties.Resources.SamplerTankerWater;

                //    }
                //    else
                //    {
                //        this.pictureBoxWater.Image = null;
                //        this.picDrainValue.BackgroundImage = global::DataLogger.Properties.Resources.Valve_Close;
                //    }
                //}
                //if (obj.module_PumpLAM > -1)
                //{
                //    //dgvDoControl["CurrentData", 5].Value = (obj.module_PumpLAM == 1) ? "MANUAL" : "AUTO";
                //    if (obj.module_PumpLAM == 1)
                //    {
                //        this.picPump1_RunningType.Image = global::DataLogger.Properties.Resources.Auto_56x56;
                //    }
                //    else
                //    {
                //        this.picPump1_RunningType.Image = global::DataLogger.Properties.Resources.Manual_56x56;
                //    }
                //}
                //if (obj.module_PumpLRS > -1)
                //{
                //    //dgvDoControl["CurrentData", 6].Value = (obj.module_PumpLRS == 1) ? "STOP" : "RUN";
                //    if (obj.module_PumpLRS != 1)
                //    {
                //        this.picPumpingSystemLRS.Image = global::DataLogger.Properties.Resources.Stop_42x42;
                //        this.lblPumpLRS.Text = lang.getText("pumping_system_left_stop");//"Stop";
                //        this.picPumpingSystemLFLT.Image = global::DataLogger.Properties.Resources.Stop_42x42;
                //        this.lblPumpLFLT.Text = lang.getText("pumping_system_left_stop");
                //    }
                //    else
                //    {
                //        this.picPumpingSystemLRS.Image = global::DataLogger.Properties.Resources.Run_42x42;
                //        this.lblPumpLRS.Text = lang.getText("pumping_system_left_run");
                //        this.picPumpingSystemLFLT.Image = global::DataLogger.Properties.Resources.Run_42x42;
                //        this.lblPumpLFLT.Text = lang.getText("pumping_system_left_run");
                //    }
                //}
                //if (obj.module_PumpLFLT > -1)
                //{
                //    //dgvDoControl["CurrentData", 7].Value = (obj.module_PumpLFLT == 1) ? "FAULT" : "NORMAL";
                //    if (obj.module_PumpLFLT == 1)
                //    {
                //        this.picPumpingSystemLFLT.Image = global::DataLogger.Properties.Resources.Fault_42x42;
                //        this.lblPumpLFLT.Text = lang.getText("pumping_system_left_fault");//"Fault";
                //    }
                //    else
                //    {
                //        //this.picPumpingSystemLFLT.Image = global::DataLogger.Properties.Resources.Run_42x42;
                //        //this.lblPumpLFLT.Text = "Normal";
                //    }
                //}
                //if (obj.module_PumpRAM > -1)
                //{
                //    //dgvDoControl["CurrentData", 8].Value = (obj.module_PumpRAM == 1) ? "AUTO" : "RUN";
                //    if (obj.module_PumpRAM == 1)
                //    {
                //        this.picPump2_RunningType.Image = global::DataLogger.Properties.Resources.Auto_56x56;
                //    }
                //    else
                //    {
                //        this.picPump2_RunningType.Image = global::DataLogger.Properties.Resources.Manual_56x56;
                //    }
                //}
                //if (obj.module_PumpRRS > -1)
                //{
                //    //dgvDoControl["CurrentData", 9].Value = (obj.module_PumpRRS == 1) ? "STOP" : "RUN";
                //    if (obj.module_PumpRRS != 1)
                //    {
                //        this.picPumpingSystemRRS.Image = global::DataLogger.Properties.Resources.Stop_42x42;
                //        this.lblPumpRRS.Text = lang.getText("pumping_system_right_stop"); //"Stop";
                //        this.picPumpingSystemRFLT.Image = global::DataLogger.Properties.Resources.Stop_42x42;
                //        this.lblPumpRFLT.Text = lang.getText("pumping_system_right_stop"); //"Stop";
                //    }
                //    else
                //    {
                //        this.picPumpingSystemRRS.Image = global::DataLogger.Properties.Resources.Run_42x42;
                //        this.lblPumpRRS.Text = lang.getText("pumping_system_right_run"); //"Run";
                //        this.picPumpingSystemRFLT.Image = global::DataLogger.Properties.Resources.Run_42x42;
                //        this.lblPumpRFLT.Text = lang.getText("pumping_system_right_run"); //"Run";
                //    }
                //}
                //if (obj.module_PumpRFLT > -1)
                //{
                //    //dgvDoControl["CurrentData", 10].Value = (obj.module_PumpRFLT == 1) ? "FAULT" : "NORMAL";
                //    if (obj.module_PumpRFLT == 1)
                //    {
                //        this.picPumpingSystemRFLT.Image = global::DataLogger.Properties.Resources.Fault_42x42;
                //        this.lblPumpRFLT.Text = lang.getText("pumping_system_right_fault"); //"Fault";
                //    }
                //    else
                //    {
                //        //this.picPumpingSystemRFLT.Image = global::DataLogger.Properties.Resources.Run_42x42;
                //        //this.lblPumpRFLT.Text = "Normal";
                //    }
                //}
                //if (obj.module_air1 > -1)
                //{
                //    //dgvDoControl["CurrentData", 0].Value = (obj.module_Power == 1) ? "NORMAL" : "NOT NORMAL";

                //    if (obj.module_air1 == 1)
                //    {
                //        this.picStationStatusAir1.BackgroundImage = global::DataLogger.Properties.Resources.on_icon_68x68;
                //    }
                //    else
                //    {
                //        this.picStationStatusAir1.BackgroundImage = global::DataLogger.Properties.Resources.off_icon_68x68;
                //    }
                //}
                //if (obj.module_air2 > -1)
                //{
                //    //dgvDoControl["CurrentData", 1].Value = (obj.module_UPS == 1) ? "NORMAL" : "NOT NORMAL";
                //    if (obj.module_air2 == 1)
                //    {
                //        this.picStationStatusAir2.BackgroundImage = global::DataLogger.Properties.Resources.on_icon_68x68;
                //    }
                //    else
                //    {
                //        this.picStationStatusAir2.BackgroundImage = global::DataLogger.Properties.Resources.off_icon_68x68;
                //    }
                //}
                //if (obj.module_cleaning > -1)
                //{
                //    //dgvDoControl["CurrentData", 1].Value = (obj.module_UPS == 1) ? "NORMAL" : "NOT NORMAL";
                //    if (obj.module_cleaning == 1)
                //    {
                //        this.picFilteringSystemCleaning.Image = global::DataLogger.Properties.Resources.Run_42x42;
                //    }
                //    else
                //    {
                //        this.picFilteringSystemCleaning.Image = global::DataLogger.Properties.Resources.Stop_42x42;
                //    }
                //}
                //if (GlobalVar.isMaintenanceStatus && GlobalVar.maintenanceLog.pumping_system == 1)
                //{
                //    // left
                //    this.picPumpingSystemLFLT.Image = global::DataLogger.Properties.Resources.Maintenance_status_42x42;
                //    this.lblPumpLFLT.Text = lang.getText("pumping_system_maintenance");//"Fault";
                //    // right
                //    this.picPumpingSystemRFLT.Image = global::DataLogger.Properties.Resources.Maintenance_status_42x42;
                //    this.lblPumpRFLT.Text = lang.getText("pumping_system_maintenance"); //"Fault";
                //}
                //Console.Write(obj.module_Temperature.ToString("##0.00") + "\n");
                //if (obj.module_Temperature > -1)
                //{
                //    txtStationStatusTemperature.Text = obj.module_Temperature.ToString("##0.00");
                //}

                //if (obj.module_Humidity > -1)
                //{
                //    txtStationStatusHumidity.Text = obj.module_Humidity.ToString("##0.00");
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine("Refresh data result fail; " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void updateDOControl(relay_io_control obj)
        {
            try
            {
                if (firstTimeForIOControl < 2)
                {
                    //#region first time
                    //if (obj.DO1_status == 1)
                    //{
                    //    this.btnDO1.Image = global::DataLogger.Properties.Resources.ON_switch_96x25;
                    //    objRelayIOControlGlobal.DO1_cmd = 1;
                    //    this.picDrainValue.BackgroundImage = global::DataLogger.Properties.Resources.Valve_Open;
                    //}
                    //else
                    //{
                    //    this.btnDO1.Image = global::DataLogger.Properties.Resources.OFF_switch_96x25;
                    //    objRelayIOControlGlobal.DO1_cmd = 0;
                    //    this.picDrainValue.BackgroundImage = global::DataLogger.Properties.Resources.Valve_Close;
                    //}
                    //if (obj.DO2_status == 1)
                    //{
                    //    this.btnDO2.Image = global::DataLogger.Properties.Resources.ON_switch_96x25;
                    //    objRelayIOControlGlobal.DO2_cmd = 1;
                    //}
                    //else
                    //{
                    //    this.btnDO2.Image = global::DataLogger.Properties.Resources.OFF_switch_96x25;
                    //    objRelayIOControlGlobal.DO2_cmd = 0;
                    //}
                    //if (obj.DO3_status == 1)
                    //{
                    //    this.btnDO3.Image = global::DataLogger.Properties.Resources.ON_switch_96x25;
                    //    objRelayIOControlGlobal.DO3_cmd = 1;
                    //}
                    //else
                    //{
                    //    this.btnDO3.Image = global::DataLogger.Properties.Resources.OFF_switch_96x25;
                    //    objRelayIOControlGlobal.DO3_cmd = 0;
                    //}
                    //if (obj.DO4_status == 1)
                    //{
                    //    this.btnDO4.Image = global::DataLogger.Properties.Resources.ON_switch_96x25;
                    //    objRelayIOControlGlobal.DO4_cmd = 1;
                    //}
                    //else
                    //{
                    //    this.btnDO4.Image = global::DataLogger.Properties.Resources.OFF_switch_96x25;
                    //    objRelayIOControlGlobal.DO4_cmd = 0;
                    //}
                    //if (obj.DO5_status == 1)
                    //{
                    //    this.btnDO5.Image = global::DataLogger.Properties.Resources.ON_switch_96x25;
                    //    objRelayIOControlGlobal.DO5_cmd = 1;
                    //}
                    //else
                    //{
                    //    this.btnDO5.Image = global::DataLogger.Properties.Resources.OFF_switch_96x25;
                    //    objRelayIOControlGlobal.DO5_cmd = 0;
                    //}
                    //if (obj.DO6_status == 1)
                    //{
                    //    this.btnDO6.Image = global::DataLogger.Properties.Resources.ON_switch_96x25;
                    //    objRelayIOControlGlobal.DO6_cmd = 1;
                    //}
                    //else
                    //{
                    //    this.btnDO6.Image = global::DataLogger.Properties.Resources.OFF_switch_96x25;
                    //    objRelayIOControlGlobal.DO6_cmd = 0;
                    //}
                    //if (obj.DO7_status == 1)
                    //{
                    //    this.btnDO7.Image = global::DataLogger.Properties.Resources.ON_switch_96x25;
                    //    objRelayIOControlGlobal.DO7_cmd = 1;
                    //}
                    //else
                    //{
                    //    this.btnDO7.Image = global::DataLogger.Properties.Resources.OFF_switch_96x25;
                    //    objRelayIOControlGlobal.DO7_cmd = 1;
                    //}
                    //if (obj.DO8_status == 1)
                    //{
                    //    this.btnDO8.Image = global::DataLogger.Properties.Resources.ON_switch_96x25;
                    //    objRelayIOControlGlobal.DO8_cmd = 1;
                    //}
                    //else
                    //{
                    //    this.btnDO8.Image = global::DataLogger.Properties.Resources.OFF_switch_96x25;
                    //    objRelayIOControlGlobal.DO8_cmd = 1;
                    //}
                    //#endregion

                    firstTimeForIOControl = firstTimeForIOControl + 1;
                }
                else
                {

                }
                //if (obj.DO1_status == 1)
                //{
                //    this.picDO1Status.Image = global::DataLogger.Properties.Resources.Run_2_28x28;
                //    this.picDrainValue.BackgroundImage = global::DataLogger.Properties.Resources.Valve_Open;
                //}
                //else
                //{
                //    this.picDO1Status.Image = global::DataLogger.Properties.Resources.Stop_2_28x28;
                //    this.picDrainValue.BackgroundImage = global::DataLogger.Properties.Resources.Valve_Close;
                //}
                //if (obj.DO2_status == 1)
                //{
                //    this.picDO2Status.Image = global::DataLogger.Properties.Resources.Run_2_28x28;
                //    //this.picFilteringSystemCleaning.Image = global::DataLogger.Properties.Resources.Run_42x42;
                //}
                //else
                //{
                //    this.picDO2Status.Image = global::DataLogger.Properties.Resources.Stop_2_28x28;
                //    //this.picFilteringSystemCleaning.Image = global::DataLogger.Properties.Resources.Stop_42x42;
                //}
                //if (obj.DO3_status == 1)
                //{
                //    this.picDO3Status.Image = global::DataLogger.Properties.Resources.Run_2_28x28;
                //}
                //else
                //{
                //    this.picDO3Status.Image = global::DataLogger.Properties.Resources.Stop_2_28x28;
                //}
                //if (obj.DO4_status == 1)
                //{
                //    this.picDO4Status.Image = global::DataLogger.Properties.Resources.Run_2_28x28;
                //}
                //else
                //{
                //    this.picDO4Status.Image = global::DataLogger.Properties.Resources.Stop_2_28x28;
                //}
                //if (obj.DO5_status == 1)
                //{
                //    this.picDO5Status.Image = global::DataLogger.Properties.Resources.Run_2_28x28;
                //}
                //else
                //{
                //    this.picDO5Status.Image = global::DataLogger.Properties.Resources.Stop_2_28x28;
                //}
                //if (obj.DO6_status == 1)
                //{
                //    this.picDO6Status.Image = global::DataLogger.Properties.Resources.Run_2_28x28;
                //}
                //else
                //{
                //    this.picDO6Status.Image = global::DataLogger.Properties.Resources.Stop_2_28x28;
                //}
                //if (obj.DO7_status == 1)
                //{
                //    this.picDO7Status.Image = global::DataLogger.Properties.Resources.Run_2_28x28;
                //}
                //else
                //{
                //    this.picDO7Status.Image = global::DataLogger.Properties.Resources.Stop_2_28x28;
                //}
                //if (obj.DO8_status == 1)
                //{
                //    this.picDO8Status.Image = global::DataLogger.Properties.Resources.Run_2_28x28;
                //}
                //else
                //{
                //    this.picDO8Status.Image = global::DataLogger.Properties.Resources.Stop_2_28x28;
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine("Refresh data result fail; " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void updateCellSamplerStatus(water_sampler obj)
        {
            try
            {
                if (GlobalVar.isMaintenanceStatus)
                {
                    if (GlobalVar.maintenanceLog.auto_sampler == 1)
                    {
                        objWaterSamplerGLobal.equipment_status = INT_STATUS_MAINTENANCE;
                    }
                }
                if (DateTime.Compare(objWaterSamplerGLobal.latest_update_communication, DateTime.Now.AddSeconds(-PERIOD_CHECK_COMMUNICATION_ERROR)) < 0)
                {
                    objWaterSamplerGLobal.equipment_status = INT_STATUS_COMMUNICATION_ERROR;
                    objWaterSamplerGLobal.bottle_position = 0;
                    objWaterSamplerGLobal.refrigeration_Temperature = -1;
                    this.picAutoSamplerStatus.BackgroundImage = global::DataLogger.Properties.Resources.Communication_fail_status;
                    this.pnbottlePosition.BackgroundImage = global::DataLogger.Properties.Resources.model;
                    txtAutoSamplerTemp.Text = "---";
                    return;
                }

                int bottle_position = Convert.ToInt32(obj.bottle_position);

                if (obj.refrigeration_Temperature > -1)
                {
                    txtAutoSamplerTemp.Text = obj.refrigeration_Temperature.ToString("##0.00");
                }
                else
                {
                    txtAutoSamplerTemp.Text = "---";
                }

                switch (bottle_position)
                {
                    case 0:
                        this.pnbottlePosition.BackgroundImage = global::DataLogger.Properties.Resources.model;
                        break;
                    case 1:
                        this.pnbottlePosition.BackgroundImage = global::DataLogger.Properties.Resources.model_1;
                        break;
                    case 2:
                        this.pnbottlePosition.BackgroundImage = global::DataLogger.Properties.Resources.model_2;
                        break;
                    case 3:
                        this.pnbottlePosition.BackgroundImage = global::DataLogger.Properties.Resources.model_3;
                        break;
                    case 4:
                        this.pnbottlePosition.BackgroundImage = global::DataLogger.Properties.Resources.model_4;
                        break;
                    case 5:
                        this.pnbottlePosition.BackgroundImage = global::DataLogger.Properties.Resources.model_5;
                        break;
                    case 6:
                        this.pnbottlePosition.BackgroundImage = global::DataLogger.Properties.Resources.model_6;
                        break;
                    case 7:
                        this.pnbottlePosition.BackgroundImage = global::DataLogger.Properties.Resources.model_7;
                        break;
                    case 8:
                        this.pnbottlePosition.BackgroundImage = global::DataLogger.Properties.Resources.model_8;
                        break;
                    case 9:
                        this.pnbottlePosition.BackgroundImage = global::DataLogger.Properties.Resources.model_9;
                        break;
                    case 10:
                        this.pnbottlePosition.BackgroundImage = global::DataLogger.Properties.Resources.model_10;
                        break;
                    case 11:
                        this.pnbottlePosition.BackgroundImage = global::DataLogger.Properties.Resources.model_11;
                        break;
                    case 12:
                        this.pnbottlePosition.BackgroundImage = global::DataLogger.Properties.Resources.model_12;
                        break;
                    case 13:
                        this.pnbottlePosition.BackgroundImage = global::DataLogger.Properties.Resources.model_13;
                        break;
                    case 14:
                        this.pnbottlePosition.BackgroundImage = global::DataLogger.Properties.Resources.model_14;
                        break;
                    case 15:
                        this.pnbottlePosition.BackgroundImage = global::DataLogger.Properties.Resources.model_15;
                        break;
                    case 16:
                        this.pnbottlePosition.BackgroundImage = global::DataLogger.Properties.Resources.model_16;
                        break;
                    case 17:
                        this.pnbottlePosition.BackgroundImage = global::DataLogger.Properties.Resources.model_17;
                        break;
                    case 18:
                        this.pnbottlePosition.BackgroundImage = global::DataLogger.Properties.Resources.model_18;
                        break;
                    case 19:
                        this.pnbottlePosition.BackgroundImage = global::DataLogger.Properties.Resources.model_19;
                        break;
                    case 20:
                        this.pnbottlePosition.BackgroundImage = global::DataLogger.Properties.Resources.model_20;
                        break;
                    case 21:
                        this.pnbottlePosition.BackgroundImage = global::DataLogger.Properties.Resources.model_21;
                        break;
                    case 22:
                        this.pnbottlePosition.BackgroundImage = global::DataLogger.Properties.Resources.model_22;
                        break;
                    case 23:
                        this.pnbottlePosition.BackgroundImage = global::DataLogger.Properties.Resources.model_23;
                        break;
                    case 24:
                        this.pnbottlePosition.BackgroundImage = global::DataLogger.Properties.Resources.model_24;
                        break;
                    default:
                        break;
                }


                if (obj.equipment_status == INT_STATUS_NORMAL)
                {
                    tooltipSAMP = "normal";
                    this.picAutoSamplerStatus.BackgroundImage = global::DataLogger.Properties.Resources.Normal;
                }
                else if (obj.equipment_status == INT_STATUS_COMMUNICATION_ERROR)
                {
                    this.picAutoSamplerStatus.BackgroundImage = global::DataLogger.Properties.Resources.Communication_fail_status;
                    //txtAutoSamplerTemp.Text = "---";
                }
                else if (obj.equipment_status == 2)
                {
                    this.picAutoSamplerStatus.BackgroundImage = global::DataLogger.Properties.Resources.Fault_status;
                    //txtAutoSamplerTemp.Text = "---";
                    tooltipSAMP = "fault";
                }
                else if (obj.equipment_status == INT_STATUS_MEASURING_STOP)
                {
                    tooltipSAMP = "run";
                    this.picAutoSamplerStatus.BackgroundImage = global::DataLogger.Properties.Resources.Sampling_water_status;
                }
                else if (obj.equipment_status == INT_STATUS_MAINTENANCE)
                {
                    this.picAutoSamplerStatus.BackgroundImage = global::DataLogger.Properties.Resources.Maintenance_status;
                }
                else
                {
                    this.picAutoSamplerStatus.BackgroundImage = global::DataLogger.Properties.Resources.Fault_status;
                    //txtAutoSamplerTemp.Text = "---";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Refresh data result fail; " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }
        private double Calculator(double D, module mod)
        {
            double A;
            A = (double)((double)((double)(mod.output_min - mod.output_max) / (double)(mod.input_min - mod.input_max))
                * (double)(D - mod.input_min))
                + mod.output_min + mod.off_set;
            return A;
        }
        public void updateMeasuredDataValue(measured_data obj)
        {
            if (GlobalVar.isMaintenanceStatus)
            {
                if (GlobalVar.maintenanceLog.mps == 1)
                {
                    objMeasuredDataGlobal.MPS_status = INT_STATUS_MAINTENANCE;
                    obj.MPS_status = objMeasuredDataGlobal.MPS_status;
                }
                if (GlobalVar.maintenanceLog.toc == 1)
                {
                    objMeasuredDataGlobal.TOC_status = INT_STATUS_MAINTENANCE;
                    obj.TOC_status = objMeasuredDataGlobal.TOC_status;
                }
                if (GlobalVar.maintenanceLog.tn == 1)
                {
                    objMeasuredDataGlobal.TN_status = INT_STATUS_MAINTENANCE;
                    obj.TN_status = objMeasuredDataGlobal.TN_status;
                }
                if (GlobalVar.maintenanceLog.tp == 1)
                {
                    objMeasuredDataGlobal.TP_status = INT_STATUS_MAINTENANCE;
                    obj.TP_status = objMeasuredDataGlobal.TP_status;
                }
            }
            // check latest update communication
            if (DateTime.Compare(objMeasuredDataGlobal.latest_update_MPS_communication, DateTime.Now.AddSeconds(-PERIOD_CHECK_COMMUNICATION_ERROR)) < 0)
            {
                objMeasuredDataGlobal.MPS_status = INT_STATUS_COMMUNICATION_ERROR;
                obj.MPS_status = objMeasuredDataGlobal.MPS_status;
                objMeasuredDataGlobal.MPS_DO = -1;
                objMeasuredDataGlobal.MPS_EC = -1;
                objMeasuredDataGlobal.MPS_pH = -1;
                objMeasuredDataGlobal.MPS_Temp = -1;
                objMeasuredDataGlobal.MPS_ORP = -1;
                objMeasuredDataGlobal.MPS_Turbidity = -1;
            }
            if (DateTime.Compare(objMeasuredDataGlobal.latest_update_TOC_communication, DateTime.Now.AddSeconds(-PERIOD_CHECK_COMMUNICATION_ERROR)) < 0)
            {
                objMeasuredDataGlobal.TOC_status = INT_STATUS_COMMUNICATION_ERROR;
                obj.TOC = objMeasuredDataGlobal.TOC = -1;
                obj.TOC_status = objMeasuredDataGlobal.TOC_status;
            }
            if (DateTime.Compare(objMeasuredDataGlobal.latest_update_TP_communication, DateTime.Now.AddSeconds(-PERIOD_CHECK_COMMUNICATION_ERROR)) < 0)
            {
                objMeasuredDataGlobal.TP_status = INT_STATUS_COMMUNICATION_ERROR;
                obj.TP_status = objMeasuredDataGlobal.TP_status;
                obj.TP = objMeasuredDataGlobal.TP = -1;

                objMeasuredDataGlobal.MPS_SS_status = INT_STATUS_COMMUNICATION_ERROR;
                obj.MPS_SS_status = objMeasuredDataGlobal.MPS_SS_status;
                obj.MPS_SS = objMeasuredDataGlobal.MPS_SS = -1;
            }
            if (DateTime.Compare(objMeasuredDataGlobal.latest_update_TN_communication, DateTime.Now.AddSeconds(-PERIOD_CHECK_COMMUNICATION_ERROR)) < 0)
            {
                objMeasuredDataGlobal.TN_status = INT_STATUS_COMMUNICATION_ERROR;
                obj.TN_status = objMeasuredDataGlobal.TN_status;
                obj.TN = objMeasuredDataGlobal.TN = -1;

                objMeasuredDataGlobal.MPS_pH_status = INT_STATUS_COMMUNICATION_ERROR;
                obj.MPS_pH_status = objMeasuredDataGlobal.MPS_pH_status;
                obj.MPS_pH = objMeasuredDataGlobal.MPS_pH = -1;
            }

            //txtMPSCondValue.Text = "---";
            //txtMPSpHValue.Text = "---";
            //txtMPSDOValue.Text = "---";
            //txtMPSTurbValue.Text = "---";
            //txtMPSORPValue.Text = "---";
            //txtMPSTempValue.Text = "---";
            if (obj.MPS_status != INT_STATUS_COMMUNICATION_ERROR &&
                obj.MPS_status != INT_STATUS_INSTRUMENT_ERROR &&
                obj.MPS_status != INT_STATUS_EMPTY_SAMPLER_RESERVOIR)
            {
                //if (obj.MPS_EC >= 0)
                //{
                //    txtMPSCondValue.Text = obj.MPS_EC.ToString("##0.00");
                //}
                //else
                //{
                //    txtMPSCondValue.Text = "Err";
                //}
                //if (obj.MPS_pH >= 0)
                //{
                //    txtMPSpHValue.Text = obj.MPS_pH.ToString("##0.00");
                //}
                //else
                //{
                //    txtMPSpHValue.Text = "Err";
                //}
                //if (obj.MPS_DO >= 0)
                //{
                //    txtMPSDOValue.Text = obj.MPS_DO.ToString("##0.00");
                //}
                //else
                //{
                //    txtMPSDOValue.Text = "Err";
                //}
                //if (obj.MPS_Turbidity >= 0)
                //{
                //    txtMPSTurbValue.Text = obj.MPS_Turbidity.ToString("##0.00");
                //}
                //else
                //{
                //    txtMPSTurbValue.Text = "Err";
                //}
                //if (obj.MPS_Turbidity >= 0)
                //{
                //    txtMPSTurbValue.Text = obj.MPS_Turbidity.ToString("##0.00");
                //}
                //else
                //{
                //    txtMPSTurbValue.Text = "Err";
                //}
                //if (obj.MPS_ORP >= 0)
                //{
                //    txtMPSORPValue.Text = obj.MPS_ORP.ToString("##0.00");
                //}
                //else
                //{
                //    txtMPSORPValue.Text = "Err";
                //}
                //if (obj.MPS_Temp >= 0)
                //{
                //    txtMPSTempValue.Text = obj.MPS_Temp.ToString("##0.00");
                //}
                //else
                //{
                //    txtMPSTempValue.Text = "Err";
                //}
            }

            //switch (obj.MPS_status)
            //{
            //    case INT_STATUS_COMMUNICATION_ERROR:
            //        this.picMPSStatus.BackgroundImage = global::DataLogger.Properties.Resources.Communication_Fault_status;
            //        break;
            //    case INT_STATUS_INSTRUMENT_ERROR:
            //        this.picMPSStatus.BackgroundImage = global::DataLogger.Properties.Resources.Fault;
            //        break;
            //    case INT_STATUS_MAINTENANCE:
            //        this.picMPSStatus.BackgroundImage = global::DataLogger.Properties.Resources.Maintenance_status;
            //        break;
            //    case INT_STATUS_NORMAL:
            //        this.picMPSStatus.BackgroundImage = global::DataLogger.Properties.Resources.Normal_status;
            //        break;
            //    case INT_STATUS_MEASURING_STOP:
            //        this.picMPSStatus.BackgroundImage = global::DataLogger.Properties.Resources.Fault;
            //        break;
            //    case INT_STATUS_CALIBRATING:
            //        this.picMPSStatus.BackgroundImage = global::DataLogger.Properties.Resources.Calibration_status;
            //        break;
            //    default:
            //        break;
            //}

            // TOC
            if (obj.TOC > -1)
            {
                txtTOCValue.Text = obj.TOC.ToString("##0.00");
                vprgTOCValue.Value = (int)(obj.TOC * 10);
            }
            else if (obj.TOC_status == INT_STATUS_COMMUNICATION_ERROR)
            {
                txtTOCValue.Text = "---";
            }
            else
            {
                txtTOCValue.Text = "ERR";
            }
            tooltipTOC = "";
            switch (obj.TOC_status)
            {
                case INT_STATUS_COMMUNICATION_ERROR:
                    this.picTOCStatus.BackgroundImage = global::DataLogger.Properties.Resources.Communication_Fault_status;
                    break;
                case INT_STATUS_INSTRUMENT_ERROR:
                    this.picTOCStatus.BackgroundImage = global::DataLogger.Properties.Resources.Fault;
                    tooltipTOC = "fault";
                    break;
                case INT_STATUS_MAINTENANCE:
                    this.picTOCStatus.BackgroundImage = global::DataLogger.Properties.Resources.Maintenance_status;
                    break;
                case INT_STATUS_NORMAL:
                    this.picTOCStatus.BackgroundImage = global::DataLogger.Properties.Resources.Normal_status;
                    tooltipTOC = "normal";
                    break;
                case INT_STATUS_MEASURING_STOP:
                    this.picTOCStatus.BackgroundImage = global::DataLogger.Properties.Resources.Fault;
                    tooltipTOC = "stop";
                    break;
                case INT_STATUS_CALIBRATING:
                    this.picTOCStatus.BackgroundImage = global::DataLogger.Properties.Resources.Calibration_status;
                    break;
                default:
                    break;
            }

            // TN
            if (obj.TN > -1)
            {
                txtTNValue.Text = obj.TN.ToString("##0.00");
                vprgTNValue.Value = (int)(obj.TN * 10);
            }
            else if (obj.TN_status == INT_STATUS_COMMUNICATION_ERROR)
            {
                txtTNValue.Text = "---";
            }
            else
            {
                txtTNValue.Text = "ERR";
            }
            tooltipTN = "";
            switch (obj.TN_status)
            {
                case INT_STATUS_COMMUNICATION_ERROR:
                    this.picTNStatus.BackgroundImage = global::DataLogger.Properties.Resources.Communication_Fault_status;
                    break;
                case INT_STATUS_INSTRUMENT_ERROR:
                    this.picTNStatus.BackgroundImage = global::DataLogger.Properties.Resources.Fault;
                    tooltipTN = "fault";
                    break;
                case INT_STATUS_MAINTENANCE:
                    this.picTNStatus.BackgroundImage = global::DataLogger.Properties.Resources.Maintenance_status;
                    break;
                case INT_STATUS_NORMAL:
                    this.picTNStatus.BackgroundImage = global::DataLogger.Properties.Resources.Normal_status;
                    tooltipTN = "normal";
                    break;
                case INT_STATUS_MEASURING_STOP:
                    this.picTNStatus.BackgroundImage = global::DataLogger.Properties.Resources.Fault;
                    tooltipTN = "stop";
                    break;
                case INT_STATUS_CALIBRATING:
                    this.picTNStatus.BackgroundImage = global::DataLogger.Properties.Resources.Calibration_status;
                    break;
                default:
                    break;
            }

            // TP
            if (obj.TP > -1)
            {
                txtTPValue.Text = obj.TP.ToString("##0.000");
                vprgTPValue.Value = (int)(obj.TP * 10);
            }
            else if (obj.TP_status == INT_STATUS_COMMUNICATION_ERROR)
            {
                txtTPValue.Text = "---";
            }
            else
            {
                txtTPValue.Text = "ERR";
            }
            tooltipTP = "";
            switch (obj.TP_status)
            {
                case INT_STATUS_COMMUNICATION_ERROR:
                    this.picTPStatus.BackgroundImage = global::DataLogger.Properties.Resources.Communication_Fault_status;
                    break;
                case INT_STATUS_INSTRUMENT_ERROR:
                    this.picTPStatus.BackgroundImage = global::DataLogger.Properties.Resources.Fault;
                    tooltipTP = "fault";
                    break;
                case INT_STATUS_MAINTENANCE:
                    this.picTPStatus.BackgroundImage = global::DataLogger.Properties.Resources.Maintenance_status;
                    break;
                case INT_STATUS_NORMAL:
                    this.picTPStatus.BackgroundImage = global::DataLogger.Properties.Resources.Normal_status;
                    tooltipTP = "normal";
                    break;
                case INT_STATUS_MEASURING_STOP:
                    this.picTPStatus.BackgroundImage = global::DataLogger.Properties.Resources.Fault;
                    tooltipTP = "stop";
                    break;
                case INT_STATUS_CALIBRATING:
                    this.picTPStatus.BackgroundImage = global::DataLogger.Properties.Resources.Calibration_status;
                    break;
                default:
                    break;
            }
        }
        #endregion

        private void btnDO1_Click(object sender, EventArgs e)
        {

            if (GlobalVar.isLogin)
            {

            }
            else
            {
                frmLogin frm = new frmLogin(lang);
                frm.ShowDialog();
            }
            if (GlobalVar.isAdmin())
            {

                //if (objRelayIOControlGlobal.DO1_cmd == 0)
                //{
                //    objRelayIOControlGlobal.DO1_cmd = 1;
                //    objRelayIOControlGlobal.DO1_status = 1;
                //    this.picDrainValue.BackgroundImage = global::DataLogger.Properties.Resources.Valve_Open;
                //    reqestForSettingIOControl(serialPortADAM, 0x30, 0x31);
                //    this.btnDO1.Image = global::DataLogger.Properties.Resources.ON_switch_96x25;
                //    this.picDO1Status.Image = global::DataLogger.Properties.Resources.Run_2_28x28;
                //}
                //else
                //{
                //    objRelayIOControlGlobal.DO1_cmd = 0;
                //    objRelayIOControlGlobal.DO1_status = 0;
                //    this.picDrainValue.BackgroundImage = global::DataLogger.Properties.Resources.Valve_Close;
                //    reqestForSettingIOControl(serialPortADAM, 0x30, 0x30);
                //    this.btnDO1.Image = global::DataLogger.Properties.Resources.OFF_switch_96x25;
                //    this.picDO1Status.Image = global::DataLogger.Properties.Resources.Stop_2_28x28;
                //}
            }
            else
            {
                MessageBox.Show(lang.getText("right_permission_error"));
            }


        }

        private void btnDO2_Click(object sender, EventArgs e)
        {
            if (GlobalVar.isLogin)
            {

            }
            else
            {
                frmLogin frm = new frmLogin(lang);
                frm.ShowDialog();
            }
            if (GlobalVar.isAdmin())
            {
                //if (objRelayIOControlGlobal.DO2_cmd == 0)
                //{
                //    objRelayIOControlGlobal.DO2_cmd = 1;
                //    objRelayIOControlGlobal.DO2_status = 1;
                //    reqestForSettingIOControl(serialPortADAM, 0x31, 0x31);
                //    this.btnDO2.Image = global::DataLogger.Properties.Resources.ON_switch_96x25;
                //    this.picDO2Status.Image = global::DataLogger.Properties.Resources.Run_2_28x28;
                //    //this.picFilteringSystemCleaning.Image = global::DataLogger.Properties.Resources.Run_42x42;
                //}
                //else
                //{
                //    objRelayIOControlGlobal.DO2_cmd = 0;
                //    objRelayIOControlGlobal.DO2_status = 0;
                //    reqestForSettingIOControl(serialPortADAM, 0x31, 0x30);
                //    this.btnDO2.Image = global::DataLogger.Properties.Resources.OFF_switch_96x25;
                //    this.picDO2Status.Image = global::DataLogger.Properties.Resources.Stop_2_28x28;
                //    //this.picFilteringSystemCleaning.Image = global::DataLogger.Properties.Resources.Stop_42x42;
                //}
            }
            else
            {
                MessageBox.Show(lang.getText("right_permission_error"));
            }


        }

        private void btnDO3_Click(object sender, EventArgs e)
        {
            if (GlobalVar.isLogin)
            {

            }
            else
            {
                frmLogin frm = new frmLogin(lang);
                frm.ShowDialog();
            }
            if (GlobalVar.isAdmin())
            {
                //if (objRelayIOControlGlobal.DO3_cmd == 0)
                //{
                //    objRelayIOControlGlobal.DO3_cmd = 1;
                //    objRelayIOControlGlobal.DO3_status = 1;
                //    reqestForSettingIOControl(serialPortADAM, 0x32, 0x31);
                //    this.btnDO3.Image = global::DataLogger.Properties.Resources.ON_switch_96x25;
                //    this.picDO3Status.Image = global::DataLogger.Properties.Resources.Run_2_28x28;
                //}
                //else
                //{
                //    objRelayIOControlGlobal.DO3_cmd = 0;
                //    objRelayIOControlGlobal.DO3_status = 0;
                //    reqestForSettingIOControl(serialPortADAM, 0x32, 0x30);
                //    this.btnDO3.Image = global::DataLogger.Properties.Resources.OFF_switch_96x25;
                //    this.picDO3Status.Image = global::DataLogger.Properties.Resources.Stop_2_28x28;
                //}
            }
            else
            {
                MessageBox.Show(lang.getText("right_permission_error"));
            }

        }

        private void btnDO4_Click(object sender, EventArgs e)
        {
            if (GlobalVar.isLogin)
            {

            }
            else
            {
                frmLogin frm = new frmLogin(lang);
                frm.ShowDialog();
            }
            if (GlobalVar.isAdmin())
            {
                //if (objRelayIOControlGlobal.DO4_cmd == 0)
                //{
                //    objRelayIOControlGlobal.DO4_cmd = 1;
                //    objRelayIOControlGlobal.DO4_status = 1;
                //    reqestForSettingIOControl(serialPortADAM, 0x33, 0x31);
                //    this.btnDO4.Image = global::DataLogger.Properties.Resources.ON_switch_96x25;
                //    this.picDO4Status.Image = global::DataLogger.Properties.Resources.Run_2_28x28;
                //}
                //else
                //{
                //    objRelayIOControlGlobal.DO4_cmd = 0;
                //    objRelayIOControlGlobal.DO4_status = 1;
                //    reqestForSettingIOControl(serialPortADAM, 0x33, 0x30);
                //    this.btnDO4.Image = global::DataLogger.Properties.Resources.OFF_switch_96x25;
                //    this.picDO4Status.Image = global::DataLogger.Properties.Resources.Stop_2_28x28;
                //}
            }
            else
            {
                MessageBox.Show(lang.getText("right_permission_error"));
            }

        }

        private void btnDO5_Click(object sender, EventArgs e)
        {
            if (GlobalVar.isLogin)
            {

            }
            else
            {
                frmLogin frm = new frmLogin(lang);
                frm.ShowDialog();
            }
            if (GlobalVar.isAdmin())
            {
                //if (objRelayIOControlGlobal.DO5_cmd == 0)
                //{
                //    objRelayIOControlGlobal.DO5_cmd = 1;
                //    objRelayIOControlGlobal.DO5_status = 1;
                //    reqestForSettingIOControl(serialPortADAM, 0x34, 0x31);
                //    this.btnDO5.Image = global::DataLogger.Properties.Resources.ON_switch_96x25;
                //    this.picDO5Status.Image = global::DataLogger.Properties.Resources.Run_2_28x28;
                //}
                //else
                //{
                //    objRelayIOControlGlobal.DO5_cmd = 0;
                //    objRelayIOControlGlobal.DO5_status = 0;
                //    reqestForSettingIOControl(serialPortADAM, 0x34, 0x30);
                //    this.btnDO5.Image = global::DataLogger.Properties.Resources.OFF_switch_96x25;
                //    this.picDO5Status.Image = global::DataLogger.Properties.Resources.Stop_2_28x28;
                //}
            }
            else
            {
                MessageBox.Show(lang.getText("right_permission_error"));
            }

        }

        private void btnDO6_Click(object sender, EventArgs e)
        {
            if (GlobalVar.isLogin)
            {

            }
            else
            {
                frmLogin frm = new frmLogin(lang);
                frm.ShowDialog();
            }
            if (GlobalVar.isAdmin())
            {
                //if (objRelayIOControlGlobal.DO6_cmd == 0)
                //{
                //    objRelayIOControlGlobal.DO6_cmd = 1;
                //    objRelayIOControlGlobal.DO6_status = 1;
                //    reqestForSettingIOControl(serialPortADAM, 0x35, 0x31);
                //    this.btnDO6.Image = global::DataLogger.Properties.Resources.ON_switch_96x25;
                //    this.picDO6Status.Image = global::DataLogger.Properties.Resources.Run_2_28x28;
                //}
                //else
                //{
                //    objRelayIOControlGlobal.DO6_cmd = 0;
                //    objRelayIOControlGlobal.DO6_status = 0;
                //    reqestForSettingIOControl(serialPortADAM, 0x35, 0x30);
                //    this.btnDO6.Image = global::DataLogger.Properties.Resources.OFF_switch_96x25;
                //    this.picDO6Status.Image = global::DataLogger.Properties.Resources.Stop_2_28x28;
                //}
            }
            else
            {
                MessageBox.Show(lang.getText("right_permission_error"));
            }

        }

        private void btnDO7_Click(object sender, EventArgs e)
        {
            if (GlobalVar.isLogin)
            {

            }
            else
            {
                frmLogin frm = new frmLogin(lang);
                frm.ShowDialog();
            }
            if (GlobalVar.isAdmin())
            {

                //MessageBox.Show(Protocol.MyTcpListener.RDAT(objRelayIOControlGlobal,objStationStatusGlobal,objWaterSamplerGLobal,objMeasuredDataGlobal));
                //if (objRelayIOControlGlobal.DO7_cmd == 0)
                //{
                //    objRelayIOControlGlobal.DO7_cmd = 1;
                //    objRelayIOControlGlobal.DO7_status = 1;
                //    reqestForSettingIOControl(serialPortADAM, 0x36, 0x31);
                //    this.btnDO7.Image = global::DataLogger.Properties.Resources.ON_switch_96x25;
                //    this.picDO7Status.Image = global::DataLogger.Properties.Resources.Run_2_28x28;
                //}
                //else
                //{
                //    objRelayIOControlGlobal.DO7_cmd = 0;
                //    objRelayIOControlGlobal.DO7_status = 0;
                //    reqestForSettingIOControl(serialPortADAM, 0x36, 0x30);
                //    this.btnDO7.Image = global::DataLogger.Properties.Resources.OFF_switch_96x25;
                //    this.picDO7Status.Image = global::DataLogger.Properties.Resources.Stop_2_28x28;
                //}
            }
            else
            {
                MessageBox.Show(lang.getText("right_permission_error"));
            }

        }

        private void btnDO8_Click(object sender, EventArgs e)
        {
            if (GlobalVar.isLogin)
            {

            }
            else
            {
                frmLogin frm = new frmLogin(lang);
                frm.ShowDialog();
            }
            if (GlobalVar.isAdmin())
            {
                //if (objRelayIOControlGlobal.DO8_cmd == 0)
                //{
                //    objRelayIOControlGlobal.DO8_cmd = 1;
                //    objRelayIOControlGlobal.DO8_status = 1;
                //    reqestForSettingIOControl(serialPortADAM, 0x37, 0x31);
                //    this.btnDO8.Image = global::DataLogger.Properties.Resources.ON_switch_96x25;
                //    this.picDO8Status.Image = global::DataLogger.Properties.Resources.Run_2_28x28;
                //}
                //else
                //{
                //    objRelayIOControlGlobal.DO8_cmd = 0;
                //    objRelayIOControlGlobal.DO8_status = 0;
                //    reqestForSettingIOControl(serialPortADAM, 0x37, 0x30);
                //    this.btnDO8.Image = global::DataLogger.Properties.Resources.OFF_switch_96x25;
                //    this.picDO8Status.Image = global::DataLogger.Properties.Resources.Stop_2_28x28;
                //}
            }
            else
            {
                MessageBox.Show(lang.getText("right_permission_error"));
            }

        }

        private void btnAutoSamplerTesting_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show("Send SAMP command ?", "Important Question", MessageBoxButtons.YesNo);
            if (result1 == DialogResult.Yes)
            {
                Protocol.MyTcpListener.requestAutoSAMPLER(serialPortSAMP);
                Thread.Sleep(300);
                Form1.datetime10 = DateTime.Now;
                if (Form1.isSamp == 10 || Form1.isSamp == 11) //ACK
                {
                    //btnAutoSamplerTesting.
                    tooltipSAMP = "runACK";
                }
                if (Form1.isSamp == 00 || Form1.isSamp == 01) //NAK
                {
                    //
                    tooltipSAMP = "runNAK";
                }
            }
            //Protocol.MyTcpListener.requestAutoSAMPLER(serialPortSAMP);
            //Thread.Sleep(300);
            //Protocol.MyTcpListener.requestInforSAMPLER(serialPortSAMP);
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            if (GlobalVar.isLogin)
            {

            }
            else
            {
                frmLogin frm = new frmLogin(lang);
                frm.ShowDialog();
                if (!GlobalVar.isLogin)
                {
                    MessageBox.Show(lang.getText("login_before_to_do_this"));
                    return;
                }
            }
            frmConfiguration frmConfig = new frmConfiguration(lang,this);
            frmConfig.ShowDialog();
            initConfig(true);
        }

        private void frmNewMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                is_close_form = true;
                data_value obj = calculateImmediately5Minute();
                data_value obj60min = calculateImmediately60Minute();

                //MessageBox.Show("111");
                if (tmrThreadingTimer != null)
                {
                    tmrThreadingTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
                    tmrThreadingTimer.Dispose();
                }

                if (tmrThreadingTimerStationStatus != null)
                {
                    tmrThreadingTimerStationStatus.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
                    tmrThreadingTimerStationStatus.Dispose();
                }

                Process.GetCurrentProcess().Kill();

                //if (serialPortMPS != null && serialPortMPS.IsOpen)
                //{
                //    serialPortMPS.Close();
                //    serialPortMPS.Dispose();
                //}
                if (serialPortSAMP != null && serialPortSAMP.IsOpen)
                {
                    serialPortSAMP.Close();
                    serialPortSAMP.Dispose();
                }
                if (serialPortTN != null && serialPortTN.IsOpen)
                {
                    serialPortTN.Close();
                    serialPortTN.Dispose();
                }
                if (serialPortTOC != null && serialPortTOC.IsOpen)
                {
                    serialPortTOC.Close();
                    serialPortTOC.Dispose();
                }
                if (serialPortTP != null && serialPortTP.IsOpen)
                {
                    serialPortTP.Close();
                    serialPortTP.Dispose();
                }
                //if (serialPortADAM != null && serialPortADAM.IsOpen)
                //{
                //    serialPortADAM.Close();
                //    serialPortADAM.Dispose();
                //}

                //MessageBox.Show("123");
                if (System.Windows.Forms.Application.MessageLoop)
                {
                    // WinForms app
                    System.Windows.Forms.Application.Exit();
                }
                else
                {
                    // Console app
                    System.Environment.Exit(Environment.ExitCode);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Process.GetCurrentProcess().Kill();
                //Environment.FailFast();
                //Application.Exit();
                //throw ex;
            }
        }
        private data_value calculateImmediately5Minute()
        {
            data_value obj = objCalCulationDataValue5Minute.addNewObjFor5Minute(null, true);

            if (obj == null)
            {
                obj = new data_5minute_value_repository().get_latest_info();
            }
            return obj;
        }
        private void btnMPS5Minute_Click(object sender, EventArgs e)
        {
            data_value obj = calculateImmediately5Minute();
            frm5MinuteMPS frm = new frm5MinuteMPS(obj, lang);
            frm.ShowDialog();
        }


        private void btnTOC5Minute_Click(object sender, EventArgs e)
        {
            data_value obj = calculateImmediately5Minute();
            frm5MinuteTOC frm = new frm5MinuteTOC(obj, lang);
            frm.ShowDialog();
        }

        private void btnTP5Minute_Click(object sender, EventArgs e)
        {
            data_value obj = calculateImmediately5Minute();
            frm5MinuteTP frm = new frm5MinuteTP(obj, lang);
            frm.ShowDialog();
        }

        private void btnTN5Minute_Click(object sender, EventArgs e)
        {
            data_value obj = calculateImmediately5Minute();
            frm5MinuteTN frm = new frm5MinuteTN(obj, lang);
            frm.ShowDialog();
        }
        private data_value calculateImmediately60Minute()
        {
            data_value obj = objCalCulationDataValue5Minute.addNewObjFor60Minute(null, true);

            if (obj == null)
            {
                obj = new data_5minute_value_repository().get_latest_info();
            }
            return obj;
        }
        private void btnMPS1Hour_Click(object sender, EventArgs e)
        {
            data_value obj = calculateImmediately60Minute();
            frm1HourMPS frm = new frm1HourMPS(obj, lang);
            frm.ShowDialog();
        }

        private void btnTOC1Hour_Click(object sender, EventArgs e)
        {
            data_value obj = calculateImmediately60Minute();
            frm1HourTOC frm = new frm1HourTOC(obj, lang);
            frm.ShowDialog();
        }

        private void btnTP1Hour_Click(object sender, EventArgs e)
        {
            data_value obj = calculateImmediately60Minute();
            frm1HourTP frm = new frm1HourTP(obj, lang);
            frm.ShowDialog();
        }


        private void btnTN1Hour_Click(object sender, EventArgs e)
        {
            data_value obj = calculateImmediately60Minute();
            frm1HourTN frm = new frm1HourTN(obj, lang);
            frm.ShowDialog();
        }

        private void btnTOCCalibrate_Click(object sender, EventArgs e)
        {
            try
            {
                this.serialPortTOC.Close();
                GlobalVar.calibrateTOCStatus = CommonInfo.CALIBRATION_STATUS_START;
                frmTOCCalibrate frm = new frmTOCCalibrate(lang);
                frm.ShowDialog();
                GlobalVar.calibrateTOCStatus = CommonInfo.CALIBRATION_STATUS_STOP;
                if (this.serialPortTOC.IsOpen)
                {

                }
                else
                {
                    this.serialPortTOC.Open();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.StackTrace);
            }


        }

        private void btnTPCalibrate_Click(object sender, EventArgs e)
        {
            try
            {
                this.serialPortTP.Close();
                GlobalVar.calibrateTPStatus = CommonInfo.CALIBRATION_STATUS_START;
                frmTPCalibrate frm = new frmTPCalibrate(lang);
                frm.ShowDialog();
                GlobalVar.calibrateTPStatus = CommonInfo.CALIBRATION_STATUS_STOP;
                if (this.serialPortTP.IsOpen)
                {

                }
                else
                {
                    this.serialPortTP.Open();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.StackTrace);
            }


        }

        private void btnTNCalibrate_Click(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show("1" + serialPortTN.PortName);
                this.serialPortTN.Close();
                GlobalVar.calibrateTNStatus = CommonInfo.CALIBRATION_STATUS_START;
                //MessageBox.Show("2"+ serialPortTN.PortName);
                frmTNCalibrate frm = new frmTNCalibrate(lang);
                frm.ShowDialog();
                GlobalVar.calibrateTNStatus = CommonInfo.CALIBRATION_STATUS_STOP;
                if (!serialPortTN.PortName.Equals("COM100"))
                {
                    if (serialPortTN.IsOpen)
                    {

                    }
                    else
                    {
                        serialPortTN.Open();
                    }
                }
                else
                {
                    //MessageBox.Show("Please config TN comport before calibrate !");
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.StackTrace);
                //MessageBox.Show(ex.Message);
            }

        }

        private void btnTOCHistoryData_Click(object sender, EventArgs e)
        {
            frmHistoryTOC frm = new frmHistoryTOC(lang);
            frm.ShowDialog();
        }

        private void btnTPHistoryData_Click(object sender, EventArgs e)
        {
            frmHistoryTP frm = new frmHistoryTP(lang);
            frm.ShowDialog();
        }


        private void btnTNHistoryData_Click(object sender, EventArgs e)
        {
            frmHistoryTN frm = new frmHistoryTN(lang);
            frm.ShowDialog();
        }

        private void btnMPSHistoryData_Click(object sender, EventArgs e)
        {
            frmHistoryMPS frm = new frmHistoryMPS(lang);
            frm.ShowDialog();
        }

        private void btnAutoSamplerHistoryData_Click(object sender, EventArgs e)
        {
            frmHistoryAutoSampler frm = new frmHistoryAutoSampler(lang);
            frm.ShowDialog();
        }

        private void btnAllHistory_Click(object sender, EventArgs e)
        {
            frmHistoryAll frm = new frmHistoryAll(lang);
            frm.ShowDialog();
        }

        private void checkAllCommunication()
        {
            updateCellSamplerStatus(objWaterSamplerGLobal);
            updateCellStationStatus(objStationStatusGlobal);
            updateMeasuredDataValue(objMeasuredDataGlobal);
        }

        private void btnMaintenance_Click(object sender, EventArgs e)
        {
            if (GlobalVar.isLogin)
            {

            }
            else
            {
                frmLogin frm = new frmLogin(lang);
                frm.ShowDialog();
                if (!GlobalVar.isLogin)
                {
                    MessageBox.Show(lang.getText("login_before_to_do_this"));
                    return;
                }
            }
            frmMaintenance objMaintenance = new frmMaintenance(lang);
            //this.Hide();
            objMaintenance.ShowDialog();
            //this.Show();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            if (GlobalVar.isLogin)
            {

            }
            else
            {
                frmLogin frm = new frmLogin(lang);
                frm.ShowDialog();
            }
            if (GlobalVar.isAdmin())
            {
                frmUserManagement frmUM = new frmUserManagement(lang);
                frmUM.ShowDialog();
            }
            else
            {
                MessageBox.Show(lang.getText("right_permission_error"));
            }


        }

        private void btnLoginLogout_Click(object sender, EventArgs e)
        {
            if (GlobalVar.isLogin)
            {
                this.btnLoginLogout.BackgroundImage = global::DataLogger.Properties.Resources.logout;

                GlobalVar.isLogin = false;
                GlobalVar.loginUser = null;
            }
            else
            {
                this.btnLoginLogout.BackgroundImage = global::DataLogger.Properties.Resources.login;
                frmLogin frm = new frmLogin(lang);
                frm.ShowDialog();

                if (GlobalVar.isLogin)
                {
                    this.btnLoginLogout.BackgroundImage = global::DataLogger.Properties.Resources.logout;
                }
            }
        }
        private void settingForLoginStatus()
        {
            if (GlobalVar.isLogin)
            {
                this.btnLoginLogout.BackgroundImage = global::DataLogger.Properties.Resources.logout;
                setTextHeadingLogin("" + lang.getText("main_menu_welcome") + ", " + GlobalVar.loginUser.user_name + " !");
            }
            else
            {
                this.btnLoginLogout.BackgroundImage = global::DataLogger.Properties.Resources.login;
                setTextHeadingLogin("" + lang.getText("main_menu_welcome") + ", " + lang.getText("main_menu_guest") + " !");
            }
        }

        private void btnMonthlyReport_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(lang.getText("monthly_report_yesno_question"), lang.getText("confirm"), MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                btnMonthlyReport.Enabled = false;
                vprgMonthlyReport.Value = 0;
                vprgMonthlyReport.Visible = true;

                bgwMonthlyReport.RunWorkerAsync();

                //Console.Write("1");
            }
        }

        private void btnLanguage_Click(object sender, EventArgs e)
        {
            switch_language();
            initConfig(false);
        }


        #region backgroundWorkerMonthlyReport
        private void backgroundWorkerMonthlyReport_DoWork(object sender, DoWorkEventArgs e)
        {
            string appPath = Path.GetDirectoryName(Application.ExecutablePath);
            string dataFolderName = "data";

            string tempFileName = "monthly_report_template.xlsx";
            string newFileName = "MonthlyReport_" + DateTime.Now.ToString("yyyy (MMddHHmmssfff)");

            string tempFilePath = Path.Combine(appPath, dataFolderName, tempFileName);
            string newFilePath = Path.Combine(appPath, dataFolderName, newFileName);

            if (File.Exists(tempFilePath))
            {
                int year = DateTime.Now.Year;
                double dayOfYearTotal = (new DateTime(year, 12, 31)).DayOfYear;
                double dayOfYear = 0;
                int percent = 0;

                IEnumerable<data_value> allData = db60m.get_all_for_monthly_report(year);

                if (allData != null)
                {
                    Excel.XLWorkbook oExcelWorkbook = new Excel.XLWorkbook(tempFilePath);
                    // Excel.Application oExcelApp = new Excel.Application();
                    // Excel.Workbook oExcelWorkbook = oExcelApp.Workbooks.Open(tempFilePath, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                    const int startRow = 5;
                    int row;

                    List<MonthlyReportInfo> mps_ph = new List<MonthlyReportInfo>();
                    List<MonthlyReportInfo> mps_orp = new List<MonthlyReportInfo>();
                    List<MonthlyReportInfo> mps_do = new List<MonthlyReportInfo>();
                    List<MonthlyReportInfo> mps_turbidity = new List<MonthlyReportInfo>();
                    List<MonthlyReportInfo> mps_ec = new List<MonthlyReportInfo>();
                    List<MonthlyReportInfo> mps_temp = new List<MonthlyReportInfo>();
                    List<MonthlyReportInfo> tn = new List<MonthlyReportInfo>();
                    List<MonthlyReportInfo> tp = new List<MonthlyReportInfo>();
                    List<MonthlyReportInfo> toc = new List<MonthlyReportInfo>();
                    List<MonthlyReportInfo> refrigeration_temperature = new List<MonthlyReportInfo>();
                    List<MonthlyReportInfo> bottle_position = new List<MonthlyReportInfo>();

                    for (int month = 1; month <= 12; month++)
                    {
                        Excel.IXLWorksheet oExcelWorksheet = oExcelWorkbook.Worksheet(month) as Excel.IXLWorksheet;
                        // Excel.IXLWorkSheet oExcelWorksheet = oExcelWorkbook.Worksheets[month] as Excel.Worksheet;

                        //rename the Sheet name
                        oExcelWorksheet.Name = (new DateTime(year, month, 1)).ToString("MMM-yy");
                        oExcelWorksheet.Cell(2, 1).Value = "'" + (new DateTime(year, month, 1)).ToString("MM.");
                        oExcelWorksheet.Cell(2, 17).Value = (new DateTime(year, month, 1)).ToString("MMM-yy");

                        // calculate average value
                        for (int day = 1; day <= DateTime.DaysInMonth(year, month); day++)
                        {
                            // get maintenance by date (year, month, day)
                            string strDate = year + "-" + month + "-" + day;
                            IEnumerable<maintenance_log> onDateMaintenanceLogs = _maintenance_logs.get_all_by_date(strDate);
                            // prepare data for maintenance
                            string maintenance_operator_name = "";
                            string maintenance_start_time = "";
                            string maintenance_end_time = "";
                            string maintenance_equipments = "";

                            Color maintenance_color = StatusColorInfo.COL_STATUS_MAINTENANCE_PERIODIC;
                            if (onDateMaintenanceLogs != null && onDateMaintenanceLogs.Count() > 0)
                            {
                                foreach (maintenance_log itemMaintenanceLog in onDateMaintenanceLogs)
                                {
                                    maintenance_operator_name += itemMaintenanceLog.name + ";";
                                    maintenance_start_time += itemMaintenanceLog.start_time.ToString("HH")
                                                                + "h" + itemMaintenanceLog.start_time.ToString("mm") + ";";
                                    maintenance_end_time += itemMaintenanceLog.end_time.ToString("HH")
                                                                + "h" + itemMaintenanceLog.end_time.ToString("mm") + ";";
                                    if (itemMaintenanceLog.tn == 1)
                                    {
                                        maintenance_equipments += "TN;";
                                    }
                                    if (itemMaintenanceLog.tp == 1)
                                    {
                                        maintenance_equipments += "TP;";
                                    }
                                    if (itemMaintenanceLog.toc == 1)
                                    {
                                        maintenance_equipments += "TOC;";
                                    }
                                    if (itemMaintenanceLog.mps == 1)
                                    {
                                        maintenance_equipments += "MPS;";
                                    }
                                    if (itemMaintenanceLog.pumping_system == 1)
                                    {
                                        maintenance_equipments += "Pumping;";
                                    }
                                    if (itemMaintenanceLog.auto_sampler == 1)
                                    {
                                        maintenance_equipments += "AutoSampler;";
                                    }
                                    if (itemMaintenanceLog.other == 1)
                                    {
                                        maintenance_equipments += itemMaintenanceLog.other_para + ";";
                                    }
                                    if (itemMaintenanceLog.maintenance_reason == 1)
                                    {
                                        maintenance_color = StatusColorInfo.COL_STATUS_MAINTENANCE_INCIDENT;
                                    }
                                }
                                maintenance_operator_name = maintenance_operator_name.Substring(0, maintenance_operator_name.Length - 1);
                                maintenance_start_time = maintenance_start_time.Substring(0, maintenance_start_time.Length - 1);
                                maintenance_end_time = maintenance_end_time.Substring(0, maintenance_end_time.Length - 1);
                                try
                                {
                                    maintenance_equipments = maintenance_equipments.Substring(0, maintenance_equipments.Length - 1);
                                }
                                catch { }
                            }

                            IEnumerable<data_value> dayData = allData.Where(t => t.stored_date.Month == month && t.stored_date.Day == day);
                            mps_ph.Clear();
                            mps_orp.Clear();
                            mps_do.Clear();
                            mps_turbidity.Clear();
                            mps_ec.Clear();
                            mps_temp.Clear();
                            tn.Clear();
                            tp.Clear();
                            toc.Clear();
                            refrigeration_temperature.Clear();
                            bottle_position.Clear();
                            foreach (data_value item in dayData)
                            {
                                mps_ph.AddNewDataValue(item.MPS_pH_status, item.MPS_pH);
                                mps_orp.AddNewDataValue(item.MPS_ORP_status, item.MPS_ORP);
                                mps_do.AddNewDataValue(item.MPS_DO_status, item.MPS_DO);
                                mps_turbidity.AddNewDataValue(item.MPS_Turbidity_status, item.MPS_Turbidity);
                                mps_ec.AddNewDataValue(item.MPS_EC_status, item.MPS_EC);
                                mps_temp.AddNewDataValue(item.MPS_Temp_status, item.MPS_Temp);
                                tn.AddNewDataValue(item.TN_status, item.TN);
                                tp.AddNewDataValue(item.TP_status, item.TP);
                                toc.AddNewDataValue(item.TOC_status, item.TOC);
                                refrigeration_temperature.AddNewDataValue(0, item.refrigeration_temperature);
                                bottle_position.AddNewDataValue(0, item.bottle_position);
                            }

                            // update to excel worksheet
                            row = startRow + day;

                            oExcelWorksheet.Cell(row, 2).Value = mps_ph.GetAverageOfMaxCountAsString();
                            oExcelWorksheet.Cell(row, 3).Value = mps_orp.GetAverageOfMaxCountAsString();
                            oExcelWorksheet.Cell(row, 4).Value = mps_do.GetAverageOfMaxCountAsString();
                            oExcelWorksheet.Cell(row, 5).Value = mps_turbidity.GetAverageOfMaxCountAsString();
                            oExcelWorksheet.Cell(row, 6).Value = mps_ec.GetAverageOfMaxCountAsString();
                            oExcelWorksheet.Cell(row, 7).Value = mps_temp.GetAverageOfMaxCountAsString();
                            oExcelWorksheet.Cell(row, 8).Value = tn.GetAverageOfMaxCountAsString();
                            oExcelWorksheet.Cell(row, 9).Value = tp.GetAverageOfMaxCountAsString();
                            oExcelWorksheet.Cell(row, 10).Value = toc.GetAverageOfMaxCountAsString();
                            oExcelWorksheet.Cell(row, 11).Value = refrigeration_temperature.GetAverageOfMaxCountAsString();
                            oExcelWorksheet.Cell(row, 12).Value = bottle_position.GetAverageOfMaxCountAsString();
                            oExcelWorksheet.Cell(row, 14).Value = maintenance_operator_name;
                            oExcelWorksheet.Cell(row, 15).Value = maintenance_start_time;
                            oExcelWorksheet.Cell(row, 16).Value = maintenance_end_time;
                            oExcelWorksheet.Cell(row, 17).Value = maintenance_equipments;


                            oExcelWorksheet.Range("b" + row).Style.Fill.SetBackgroundColor(Excel.XLColor.FromColor(mps_ph.GetStatusColor()));
                            oExcelWorksheet.Range("c" + row).Style.Fill.SetBackgroundColor(Excel.XLColor.FromColor(mps_orp.GetStatusColor()));
                            oExcelWorksheet.Range("d" + row).Style.Fill.SetBackgroundColor(Excel.XLColor.FromColor(mps_do.GetStatusColor()));
                            oExcelWorksheet.Range("e" + row).Style.Fill.SetBackgroundColor(Excel.XLColor.FromColor(mps_turbidity.GetStatusColor()));
                            oExcelWorksheet.Range("f" + row).Style.Fill.SetBackgroundColor(Excel.XLColor.FromColor(mps_ec.GetStatusColor()));
                            oExcelWorksheet.Range("g" + row).Style.Fill.SetBackgroundColor(Excel.XLColor.FromColor(mps_temp.GetStatusColor()));
                            oExcelWorksheet.Range("h" + row).Style.Fill.SetBackgroundColor(Excel.XLColor.FromColor(tn.GetStatusColor()));
                            oExcelWorksheet.Range("i" + row).Style.Fill.SetBackgroundColor(Excel.XLColor.FromColor(tp.GetStatusColor()));
                            oExcelWorksheet.Range("j" + row).Style.Fill.SetBackgroundColor(Excel.XLColor.FromColor(toc.GetStatusColor()));
                            oExcelWorksheet.Range("k" + row).Style.Fill.SetBackgroundColor(Excel.XLColor.FromColor(refrigeration_temperature.GetStatusColor()));
                            oExcelWorksheet.Range("l" + row).Style.Fill.SetBackgroundColor(Excel.XLColor.FromColor(bottle_position.GetStatusColor()));

                            oExcelWorksheet.Range("n" + row).Style.Fill.SetBackgroundColor(Excel.XLColor.FromColor(maintenance_color));
                            oExcelWorksheet.Range("o" + row).Style.Fill.SetBackgroundColor(Excel.XLColor.FromColor(maintenance_color));
                            oExcelWorksheet.Range("p" + row).Style.Fill.SetBackgroundColor(Excel.XLColor.FromColor(maintenance_color));
                            oExcelWorksheet.Range("q" + row).Style.Fill.SetBackgroundColor(Excel.XLColor.FromColor(maintenance_color));

                            dayOfYear = (new DateTime(year, month, day)).DayOfYear;
                            percent = (int)(dayOfYear * 100d / dayOfYearTotal);
                            bgwMonthlyReport.ReportProgress(percent);

                            //Thread.Sleep(1);
                        }
                    }
                    oExcelWorkbook.SaveAs(newFilePath + ".xlsx");
                    //oExcelWorkbook.SaveAs(newFilePath, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlShared, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                }
            }
            FileInfo fi = new FileInfo(newFilePath + ".xlsx");
            if (fi.Exists)
            {
                System.Diagnostics.Process.Start(newFilePath + ".xlsx");
            }
            else
            {
                //file doesn't exist
            }
        }

        private void backgroundWorkerMonthlyReport_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            vprgMonthlyReport.Value = e.ProgressPercentage;
        }

        private void backgroundWorkerMonthlyReport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnMonthlyReport.Enabled = true;
            vprgMonthlyReport.Visible = false;

            if (!e.Cancelled && e.Error == null)
            {
                MessageBox.Show(lang.getText("successfully"));
            }
            else
            {

            }
        }

        #endregion backgroundWorkerMonthlyReport

        private void vprgMonthlyReport_Load(object sender, EventArgs e)
        {

        }

        private void picTOCStatus_MouseHover(object sender, EventArgs e)
        {
            if (tooltipTOC == "fault" || tooltipTOC == "stop")
            {
                ToolTip tt = new ToolTip();
                if (tooltipTOC == "fault")
                {
                    tt.SetToolTip(this.picTOCStatus, lang.getText(tooltipTOC) + ":" + tooltipTOCInfo);
                }
                else
                {
                    tt.SetToolTip(this.picTOCStatus, lang.getText(tooltipTOC));
                }

            }
        }

        private void picTPStatus_MouseHover(object sender, EventArgs e)
        {
            if (tooltipTP == "fault" || tooltipTP == "stop")
            {
                ToolTip tt = new ToolTip();
                if (tooltipTP == "fault")
                {
                    tt.SetToolTip(this.picTPStatus, lang.getText(tooltipTP) + ":" + tooltipTPInfo);
                }
                else
                {
                    tt.SetToolTip(this.picTPStatus, lang.getText(tooltipTP));
                }
            }
        }

        private void picTNStatus_MouseHover(object sender, EventArgs e)
        {
            if (tooltipTN == "fault" || tooltipTN == "stop")
            {
                ToolTip tt = new ToolTip();
                if (tooltipTN == "fault")
                {
                    tt.SetToolTip(this.picTNStatus, lang.getText(tooltipTN) + ":" + tooltipTNInfo);
                }
                else
                {
                    tt.SetToolTip(this.picTNStatus, lang.getText(tooltipTN));
                }
            }
        }

        private void panel13_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtStationStatusTemperature_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblDrainValve_Click(object sender, EventArgs e)
        {

        }

        private void label43_Click(object sender, EventArgs e)
        {

        }

        private void txtTOCValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void picTOCStatus_Click(object sender, EventArgs e)
        {

        }

        private void label48_Click(object sender, EventArgs e)
        {

        }

        private void txtTPValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void picTPStatus_Click(object sender, EventArgs e)
        {

        }

        private void label49_Click(object sender, EventArgs e)
        {

        }

        private void label47_Click(object sender, EventArgs e)
        {

        }

        private void label53_Click(object sender, EventArgs e)
        {

        }

        private void label52_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label62_Click(object sender, EventArgs e)
        {

        }

        private void lblCleaning_Click(object sender, EventArgs e)
        {

        }

        private void lblPumpRFLT_Click(object sender, EventArgs e)
        {

        }

        private void lblPumpLAM_Click(object sender, EventArgs e)
        {

        }

        private void lblMainPower_Click(object sender, EventArgs e)
        {

        }

        private void picStationStatusMainPower_Click(object sender, EventArgs e)
        {

        }

        private void panel30_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblFilteringSystem_Click(object sender, EventArgs e)
        {

        }

        private void lblStationStatus_Click(object sender, EventArgs e)
        {

        }

        private void lblDO2_Click(object sender, EventArgs e)
        {

        }

        private void lblHumidity_Click(object sender, EventArgs e)
        {

        }

        private void listen_Click(object sender, EventArgs e)
        {

        }

        private void txtMPSpHValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void picDO2Status_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {

        }

        private void vprgTPValue_Load(object sender, EventArgs e)
        {

        }

        private void picAutoSamplerStatus_MouseHover(object sender, EventArgs e)
        {
            if (tooltipSAMP == "fault" || tooltipSAMP == "runACK" || tooltipSAMP == "runNAK" ||
                tooltipSAMP == "run" || tooltipSAMP == "normal"
                )
            {
                ToolTip tt = new ToolTip();
                if (tooltipSAMP == "fault")
                {
                    tt.SetToolTip(this.picAutoSamplerStatus, lang.getText(tooltipSAMP) + " : " + tooltipSAMPInfo + " " + datetime00);
                }
                else if (tooltipSAMP == "run")
                {
                    tt.SetToolTip(this.picAutoSamplerStatus, lang.getText(tooltipSAMP) + " : " + tooltipSAMPInfo + " " + Form1.datetime10);
                }
                //else if (tooltipSAMP == "runACK")
                //{
                //    tt.SetToolTip(this.picAutoSamplerStatus, lang.getText(tooltipSAMP) + " : " + tooltipSAMPInfo + " " + Form1.datetime10);
                //}
                //else if (tooltipSAMP == "runNAK")
                //{
                //    tt.SetToolTip(this.picAutoSamplerStatus, lang.getText(tooltipSAMP) + " : " + tooltipSAMPInfo + " " + Form1.datetime10);
                //}
            }
        }

        private void btnAutoSamplerTesting_MouseHover(object sender, EventArgs e)
        {
            if (tooltipSAMP == "runACK" || tooltipSAMP == "runNAK" || tooltipSAMP == "run")
            {
                ToolTip tt = new ToolTip();
                if (tooltipSAMP == "runACK")
                {
                    tt.SetToolTip(this.btnAutoSamplerTesting, lang.getText(tooltipSAMP) + " : " + "SUCCESS " + Form1.datetime10);
                }
                else if (tooltipSAMP == "runNAK")
                {
                    tt.SetToolTip(this.btnAutoSamplerTesting, lang.getText(tooltipSAMP) + " : " + "FAIL " + Form1.datetime10);
                }
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    public class CalculationDataValue
    {
        public List<data_value> listDataValue = new List<data_value>();
        public int hour { get; set; }
        public int min_minute { get; set; } // start time
        public int max_minute { get; set; } // end time
        public DateTime latestCalculate5Minute = DateTime.Now;
        public DateTime latestCalculate60Minute = DateTime.Now;

        public CalculationDataValue()
        {
            hour = DateTime.Now.Hour;
            min_minute = DateTime.Now.Minute;
            max_minute = DateTime.Now.Minute;
        }
        public data_value addNewObjFor5Minute(data_value obj, bool isImmediatelyCalculation = false)
        {
            // checking execute transaction
            int tempHour = 0;
            if (obj != null)
            {
                tempHour = obj.created.Hour;
            }
            else
            {
                tempHour = DateTime.Now.Hour;

                if (DateTime.Compare(latestCalculate5Minute, DateTime.Now.AddSeconds(-40)) < 0)
                {
                    return null;
                }
            }

            int tempMinute = 0;
            if (obj != null)
            {
                tempMinute = obj.created.Minute;
            }
            else
            {
                tempMinute = DateTime.Now.Minute;
            }

            data_value objDataValue = null;
            data_value objLatest = null;
            int status = 0;
            if (listDataValue.Count > 0)
            {
                if ((tempHour != hour) || ((tempMinute - min_minute) > 1) || isImmediatelyCalculation)
                {
                    if (tempMinute % 5 == 0 || isImmediatelyCalculation)
                    {
                        // ok
                        // calculate and add to database
                        objDataValue = new data_value();
                        // station status
                        objDataValue.module_Door = listDataValue[0].module_Door;
                        objDataValue.module_Fire = listDataValue[0].module_Fire;
                        objDataValue.module_Flow = listDataValue[0].module_Flow;
                        objDataValue.module_Humidity = listDataValue[0].module_Humidity;
                        objDataValue.module_Power = listDataValue[0].module_Power;
                        objDataValue.module_PumpLAM = listDataValue[0].module_PumpLAM;
                        objDataValue.module_PumpLFLT = listDataValue[0].module_PumpLFLT;
                        objDataValue.module_PumpLRS = listDataValue[0].module_PumpLRS;
                        objDataValue.module_PumpRAM = listDataValue[0].module_PumpRAM;
                        objDataValue.module_PumpRFLT = listDataValue[0].module_PumpRFLT;
                        objDataValue.module_PumpRRS = listDataValue[0].module_PumpRRS;
                        objDataValue.module_air1 = listDataValue[0].module_air1;
                        objDataValue.module_air2 = listDataValue[0].module_air2;
                        objDataValue.module_cleaning = listDataValue[0].module_cleaning;

                        objDataValue.module_Temperature = listDataValue[0].module_Temperature;
                        objDataValue.module_UPS = listDataValue[0].module_UPS;
                        // MPS
                        objDataValue.MPS_DO = listDataValue[0].MPS_DO;
                        objDataValue.MPS_DO_status = listDataValue[0].MPS_status;
                        objDataValue.MPS_EC = listDataValue[0].MPS_EC;
                        objDataValue.MPS_EC_status = listDataValue[0].MPS_status;
                        objDataValue.MPS_ORP = listDataValue[0].MPS_ORP;
                        objDataValue.MPS_ORP_status = listDataValue[0].MPS_status;
                        objDataValue.MPS_pH = listDataValue[0].MPS_pH;
                        objDataValue.MPS_pH_status = listDataValue[0].MPS_status;
                        objDataValue.MPS_Temp = listDataValue[0].MPS_Temp;
                        objDataValue.MPS_Temp_status = listDataValue[0].MPS_status;
                        objDataValue.MPS_Turbidity = listDataValue[0].MPS_Turbidity;
                        objDataValue.MPS_Turbidity_status = listDataValue[0].MPS_status;
                        objDataValue.MPS_status = listDataValue[0].MPS_status;
                        // TN, TOC, TP
                        objDataValue.TN = listDataValue[0].TN;
                        objDataValue.TN_status = listDataValue[0].TN_status;
                        objDataValue.TOC = listDataValue[0].TOC;
                        objDataValue.TOC_status = listDataValue[0].TOC_status;
                        objDataValue.TP = listDataValue[0].TP;
                        objDataValue.TP_status = listDataValue[0].TP_status;

                        // water sampler
                        objDataValue.bottle_position = listDataValue[0].bottle_position;
                        objDataValue.door_status = listDataValue[0].door_status;
                        objDataValue.equipment_status = listDataValue[0].equipment_status;
                        objDataValue.refrigeration_temperature = listDataValue[0].refrigeration_temperature;

                        // Time
                        objDataValue.stored_date = listDataValue[0].stored_date;
                        objDataValue.stored_hour = hour;
                        objDataValue.stored_minute = (min_minute / 5) * 5;
                        int count = listDataValue.Count;

                        bool updateMPSFlag = true;
                        bool updateTNFlag = true;
                        bool updateTPFlag = true;
                        bool updateTOCFlag = true;
                        //bool updateStationStatus = true;
                        bool updateWaterSampler = true;
                        int countingMPSCal = 1;
                        int countingStationStatusCal = 1;
                        int countingTNCal = 1;
                        int countingTPCal = 1;
                        int countingTOCCal = 1;
                        int countingWaterSampler = 1;

                        for (int i = 1; i < count; i++)
                        {
                            // MPS
                            if (updateMPSFlag)
                            {
                                if (listDataValue[i].MPS_status == CommonInfo.INT_STATUS_NORMAL)
                                {
                                    objDataValue.MPS_DO = objDataValue.MPS_DO + listDataValue[i].MPS_DO;
                                    objDataValue.MPS_DO_status = listDataValue[i].MPS_status;
                                    objDataValue.MPS_EC = objDataValue.MPS_EC + listDataValue[i].MPS_EC;
                                    objDataValue.MPS_EC_status = listDataValue[i].MPS_status;
                                    objDataValue.MPS_ORP = objDataValue.MPS_ORP + listDataValue[i].MPS_ORP;
                                    objDataValue.MPS_ORP_status = listDataValue[i].MPS_status;
                                    objDataValue.MPS_pH = objDataValue.MPS_pH + listDataValue[i].MPS_pH;
                                    objDataValue.MPS_pH_status = listDataValue[i].MPS_status;
                                    objDataValue.MPS_Temp = objDataValue.MPS_Temp + listDataValue[i].MPS_Temp;
                                    objDataValue.MPS_Temp_status = listDataValue[i].MPS_status;
                                    objDataValue.MPS_Turbidity = objDataValue.MPS_Turbidity + listDataValue[i].MPS_Turbidity;
                                    objDataValue.MPS_Turbidity_status = listDataValue[i].MPS_status;
                                    objDataValue.MPS_status = listDataValue[i].MPS_status;
                                    countingMPSCal++;
                                }
                                else
                                {
                                    objDataValue.MPS_DO = -1;
                                    objDataValue.MPS_DO_status = listDataValue[i].MPS_status;
                                    objDataValue.MPS_EC = -1;
                                    objDataValue.MPS_EC_status = listDataValue[i].MPS_status;
                                    objDataValue.MPS_ORP = -1;
                                    objDataValue.MPS_ORP_status = listDataValue[i].MPS_status;
                                    objDataValue.MPS_pH = -1;
                                    objDataValue.MPS_pH_status = listDataValue[i].MPS_status;
                                    objDataValue.MPS_Temp = -1;
                                    objDataValue.MPS_Temp_status = listDataValue[i].MPS_status;
                                    objDataValue.MPS_Turbidity = -1;
                                    objDataValue.MPS_Turbidity_status = listDataValue[i].MPS_status;
                                    objDataValue.MPS_status = listDataValue[i].MPS_status;
                                    updateMPSFlag = false;
                                }
                            }

                            // TN
                            if (updateTNFlag)
                            {
                                objDataValue.TN_status = listDataValue[i].TN_status;
                                if (objDataValue.TN_status == CommonInfo.INT_STATUS_NORMAL)
                                {
                                    objDataValue.TN = objDataValue.TN + listDataValue[i].TN;
                                    countingTNCal++;
                                }
                                else
                                {
                                    objDataValue.TN = -1;
                                    updateTNFlag = false;
                                }
                            }

                            // TP
                            if (updateTPFlag)
                            {
                                objDataValue.TP_status = listDataValue[i].TP_status;
                                if (objDataValue.TP_status == CommonInfo.INT_STATUS_NORMAL)
                                {
                                    objDataValue.TP = objDataValue.TP + listDataValue[i].TP;
                                    countingTPCal++;
                                }
                                else
                                {
                                    objDataValue.TP = -1;
                                    updateTPFlag = false;
                                }
                            }
                            // TOC
                            if (updateTOCFlag)
                            {
                                objDataValue.TOC_status = listDataValue[i].TOC_status;
                                if (objDataValue.TOC_status == CommonInfo.INT_STATUS_NORMAL)
                                {
                                    objDataValue.TOC = objDataValue.TOC + listDataValue[i].TOC;
                                    countingTOCCal++;
                                }
                                else
                                {
                                    objDataValue.TOC = -1;
                                    updateTOCFlag = false;
                                }
                            }

                            // Station status
                            if (listDataValue[i].module_Temperature > 0)
                            {
                                objDataValue.module_Temperature = objDataValue.module_Temperature
                                    + listDataValue[i].module_Temperature;
                                objDataValue.module_Humidity = objDataValue.module_Humidity
                                        + listDataValue[i].module_Humidity;
                                countingStationStatusCal++;
                            }
                            // Water sampler
                            if (updateWaterSampler)
                            {
                                objDataValue.equipment_status = listDataValue[i].equipment_status;
                                if (objDataValue.equipment_status == CommonInfo.INT_STATUS_NORMAL)
                                {
                                    objDataValue.refrigeration_temperature = objDataValue.refrigeration_temperature
                                        + listDataValue[i].refrigeration_temperature;
                                    objDataValue.bottle_position = objDataValue.bottle_position
                                       + listDataValue[i].bottle_position;
                                    countingWaterSampler++;
                                }
                                else
                                {
                                    objDataValue.refrigeration_temperature = -1;
                                    objDataValue.bottle_position = -1;
                                    updateWaterSampler = false;
                                }
                            }
                        }
                        if (updateMPSFlag)
                        {
                            objDataValue.MPS_DO = (double)objDataValue.MPS_DO / (double)countingMPSCal;
                            objDataValue.MPS_EC = (double)objDataValue.MPS_EC / (double)countingMPSCal;
                            objDataValue.MPS_ORP = (double)objDataValue.MPS_ORP / (double)countingMPSCal;
                            objDataValue.MPS_pH = (double)objDataValue.MPS_pH / (double)countingMPSCal;
                            objDataValue.MPS_Temp = (double)objDataValue.MPS_Temp / (double)countingMPSCal;
                            objDataValue.MPS_Turbidity = (double)objDataValue.MPS_Turbidity / (double)countingMPSCal;
                        }
                        if (updateTNFlag)
                        {
                            objDataValue.TN = (double)objDataValue.TN / (double)countingTNCal;
                        }
                        if (updateTPFlag)
                        {
                            objDataValue.TP = (double)objDataValue.TP / (double)countingTPCal;
                        }
                        if (updateTOCFlag)
                        {
                            objDataValue.TOC = (double)objDataValue.TOC / (double)countingTOCCal;
                        }// Station status
                        if (objDataValue.module_Temperature > 0)
                        {
                            objDataValue.module_Temperature = (double)objDataValue.module_Temperature / (double)countingStationStatusCal;
                            objDataValue.module_Humidity = (double)objDataValue.module_Humidity / (double)countingStationStatusCal;
                        }
                        if (updateWaterSampler)
                        {
                            objDataValue.refrigeration_temperature = (double)objDataValue.refrigeration_temperature
                                     / (double)countingWaterSampler;
                            objDataValue.bottle_position = objDataValue.bottle_position
                               / countingWaterSampler;
                        }
                        // get latest to check before add
                        objLatest = new data_5minute_value_repository().get_latest_info();
                        if (objLatest != null &&
                            objLatest.created.Date == objDataValue.created.Date &&
                            objLatest.created.Month == objDataValue.created.Month &&
                            objLatest.created.Year == objDataValue.created.Year &&
                            objLatest.stored_hour == objDataValue.stored_hour &&
                            objLatest.stored_minute == objDataValue.stored_minute)
                        {
                            // update to
                            // MPS

                            if (objLatest.MPS_status == CommonInfo.INT_STATUS_NORMAL &&
                                objDataValue.MPS_status == CommonInfo.INT_STATUS_NORMAL)
                            {
                                objLatest.MPS_DO = (objLatest.MPS_DO + objDataValue.MPS_DO) / 2;
                                objLatest.MPS_DO_status = objDataValue.MPS_status;
                                objLatest.MPS_EC = (objLatest.MPS_EC + objDataValue.MPS_EC) / 2;
                                objLatest.MPS_EC_status = objDataValue.MPS_status;
                                objLatest.MPS_ORP = (objLatest.MPS_ORP + objDataValue.MPS_ORP) / 2;
                                objLatest.MPS_ORP_status = objDataValue.MPS_status;
                                objLatest.MPS_pH = (objLatest.MPS_pH + objDataValue.MPS_pH) / 2;
                                objLatest.MPS_pH_status = objDataValue.MPS_status;
                                objLatest.MPS_Temp = (objLatest.MPS_Temp + objDataValue.MPS_Temp) / 2;
                                objLatest.MPS_Temp_status = objDataValue.MPS_status;
                                objLatest.MPS_Turbidity = (objLatest.MPS_Turbidity + objDataValue.MPS_Turbidity) / 2;
                                objLatest.MPS_Turbidity_status = objDataValue.MPS_status;
                                objLatest.MPS_status = objDataValue.MPS_status;
                            }
                            else
                            {
                                objLatest.MPS_DO = -1;
                                objLatest.MPS_DO_status = objLatest.MPS_status;
                                objLatest.MPS_EC = -1;
                                objLatest.MPS_EC_status = objLatest.MPS_status;
                                objLatest.MPS_ORP = -1;
                                objLatest.MPS_ORP_status = objLatest.MPS_status;
                                objLatest.MPS_pH = -1;
                                objLatest.MPS_pH_status = objLatest.MPS_status;
                                objLatest.MPS_Temp = -1;
                                objLatest.MPS_Temp_status = objLatest.MPS_status;
                                objLatest.MPS_Turbidity = -1;
                                objLatest.MPS_Turbidity_status = objLatest.MPS_status;
                                objLatest.MPS_status = objLatest.MPS_status;
                                if (objDataValue.MPS_status != CommonInfo.INT_STATUS_NORMAL)
                                {
                                    objLatest.MPS_DO_status = objDataValue.MPS_status;
                                    objLatest.MPS_EC_status = objDataValue.MPS_status;
                                    objLatest.MPS_ORP_status = objDataValue.MPS_status;
                                    objLatest.MPS_pH_status = objDataValue.MPS_status;
                                    objLatest.MPS_Temp_status = objDataValue.MPS_status;
                                    objLatest.MPS_Turbidity_status = objDataValue.MPS_status;
                                    objLatest.MPS_status = objDataValue.MPS_status;
                                }

                            }
                            // TN
                            if (objDataValue.TN_status == CommonInfo.INT_STATUS_NORMAL &&
                                objLatest.TN_status == CommonInfo.INT_STATUS_NORMAL)
                            {
                                objLatest.TN = (objDataValue.TN + objLatest.TN) / 2;
                            }
                            else
                            {
                                objLatest.TN = -1;
                                if (objDataValue.TN_status != CommonInfo.INT_STATUS_NORMAL)
                                {
                                    objLatest.TN_status = objDataValue.TN_status;
                                }
                            }


                            // TP
                            if (objDataValue.TP_status == CommonInfo.INT_STATUS_NORMAL &&
                                objLatest.TP_status == CommonInfo.INT_STATUS_NORMAL)
                            {
                                objLatest.TP = (objDataValue.TP + objLatest.TP) / 2;
                            }
                            else
                            {
                                objLatest.TP = -1;
                                if (objDataValue.TP_status != CommonInfo.INT_STATUS_NORMAL)
                                {
                                    objLatest.TP_status = objDataValue.TP_status;
                                }
                            }
                            // TOC
                            if (objDataValue.TOC_status == CommonInfo.INT_STATUS_NORMAL &&
                                objLatest.TOC_status == CommonInfo.INT_STATUS_NORMAL)
                            {
                                objLatest.TOC = (objDataValue.TOC + objLatest.TOC) / 2;
                            }
                            else
                            {
                                objLatest.TOC = -1;
                                if (objDataValue.TOC_status != CommonInfo.INT_STATUS_NORMAL)
                                {
                                    objLatest.TOC_status = objDataValue.TOC_status;
                                }
                            }

                            // Station status
                            if (objLatest.module_Temperature > 0 && objDataValue.module_Temperature > 0)
                            {
                                objLatest.module_Temperature = (objDataValue.module_Temperature
                                    + objLatest.module_Temperature) / 2;
                                objLatest.module_Humidity = (objDataValue.module_Humidity
                                        + objLatest.module_Humidity) / 2;
                            }
                            // Water sampler


                            if (objDataValue.equipment_status == CommonInfo.INT_STATUS_NORMAL &&
                                objLatest.equipment_status == CommonInfo.INT_STATUS_NORMAL)
                            {
                                objLatest.refrigeration_temperature = (objDataValue.refrigeration_temperature
                                    + objLatest.refrigeration_temperature) / 2;
                                objLatest.bottle_position = (objDataValue.bottle_position
                                   + objLatest.bottle_position) / 2;
                            }
                            else
                            {
                                objLatest.refrigeration_temperature = -1;
                                objLatest.bottle_position = -1;
                                if (objDataValue.equipment_status != CommonInfo.INT_STATUS_NORMAL)
                                {
                                    objLatest.equipment_status = objDataValue.equipment_status;
                                }

                            }
                            //// save to data value table
                            if (new data_5minute_value_repository().update(ref objLatest) > 0)
                            {
                                // ok
                            }
                            else
                            {
                                // fail
                            }
                            status = 1; // update
                        }
                        else
                        {
                            if (GlobalVar.isMaintenanceStatus && GlobalVar.maintenanceLog.pumping_system == 1)
                            {
                                objDataValue.pumping_system_status = CommonInfo.INT_STATUS_MAINTENANCE;
                                //objDataValue.station_status = CommonInfo.INT_STATUS_MAINTENANCE;
                            }
                            //// save to data value table
                            if (new data_5minute_value_repository().add(ref objDataValue) > 0)
                            {
                                // ok
                            }
                            else
                            {
                                // fail
                            }
                            status = 2; // add
                        }
                        ////// save to data value table
                        //if (new data_5minute_value_repository().add(ref objDataValue) > 0)
                        //{
                        //    // ok
                        //}
                        //else
                        //{
                        //    // fail
                        //}
                        min_minute = tempMinute;
                        listDataValue.Clear();
                    }
                    else
                    {
                        // add to list
                    }
                }
                else
                {
                    // add to list
                }
            }
            latestCalculate5Minute = DateTime.Now;
            max_minute = tempMinute;
            hour = tempHour;
            if (obj != null)
            {
                listDataValue.Add(obj);
            }

            if (status == 0)
            {
                return null;
            }
            else if (status == 1)
            {
                return objLatest;
            }
            else
            {
                return objDataValue;
            }

        }
        public data_value addNewObjFor60Minute(data_value obj, bool isImmediatelyCalculation = false)
        {

            // checking execute transaction
            int tempHour = 0;
            if (obj != null)
            {
                tempHour = obj.created.Hour;
            }
            else
            {
                tempHour = DateTime.Now.Hour;
            }

            int tempMinute = 0;
            if (obj != null)
            {
                tempMinute = obj.created.Minute;
            }
            else
            {
                tempMinute = DateTime.Now.Minute;

                if (DateTime.Compare(latestCalculate60Minute, DateTime.Now.AddMinutes(-1)) < 0)
                {
                    return null;
                }
            }
            data_value objDataValue = null;
            data_value objLatest = null;
            int status = 0;

            if (listDataValue.Count > 0)
            {
                if ((tempHour != hour) || isImmediatelyCalculation)
                {
                    // ok
                    // calculate and add to database
                    objDataValue = new data_value();
                    // station status
                    objDataValue.module_Door = listDataValue[0].module_Door;
                    objDataValue.module_Fire = listDataValue[0].module_Fire;
                    objDataValue.module_Flow = listDataValue[0].module_Flow;
                    objDataValue.module_Humidity = listDataValue[0].module_Humidity;
                    objDataValue.module_Power = listDataValue[0].module_Power;
                    objDataValue.module_PumpLAM = listDataValue[0].module_PumpLAM;
                    objDataValue.module_PumpLFLT = listDataValue[0].module_PumpLFLT;
                    objDataValue.module_PumpLRS = listDataValue[0].module_PumpLRS;
                    objDataValue.module_PumpRAM = listDataValue[0].module_PumpRAM;
                    objDataValue.module_PumpRFLT = listDataValue[0].module_PumpRFLT;
                    objDataValue.module_PumpRRS = listDataValue[0].module_PumpRRS;
                    objDataValue.module_air1 = listDataValue[0].module_air1;
                    objDataValue.module_air2 = listDataValue[0].module_air2;
                    objDataValue.module_cleaning = listDataValue[0].module_cleaning;

                    objDataValue.module_Temperature = listDataValue[0].module_Temperature;
                    objDataValue.module_UPS = listDataValue[0].module_UPS;
                    objDataValue.MPS_status = listDataValue[0].MPS_status;
                    // MPS
                    objDataValue.MPS_DO = listDataValue[0].MPS_DO;
                    objDataValue.MPS_DO_status = listDataValue[0].MPS_status;
                    objDataValue.MPS_EC = listDataValue[0].MPS_EC;
                    objDataValue.MPS_EC_status = listDataValue[0].MPS_status;
                    objDataValue.MPS_ORP = listDataValue[0].MPS_ORP;
                    objDataValue.MPS_ORP_status = listDataValue[0].MPS_status;
                    objDataValue.MPS_pH = listDataValue[0].MPS_pH;
                    objDataValue.MPS_pH_status = listDataValue[0].MPS_status;
                    objDataValue.MPS_Temp = listDataValue[0].MPS_Temp;
                    objDataValue.MPS_Temp_status = listDataValue[0].MPS_status;
                    objDataValue.MPS_Turbidity = listDataValue[0].MPS_Turbidity;
                    objDataValue.MPS_Turbidity_status = listDataValue[0].MPS_status;

                    // TN, TOC, TP
                    objDataValue.TN = listDataValue[0].TN;
                    objDataValue.TN_status = listDataValue[0].TN_status;
                    objDataValue.TOC = listDataValue[0].TOC;
                    objDataValue.TOC_status = listDataValue[0].TOC_status;
                    objDataValue.TP = listDataValue[0].TP;
                    objDataValue.TP_status = listDataValue[0].TP_status;

                    // water sampler
                    objDataValue.bottle_position = listDataValue[0].bottle_position;
                    objDataValue.door_status = listDataValue[0].door_status;
                    objDataValue.equipment_status = listDataValue[0].equipment_status;
                    objDataValue.refrigeration_temperature = listDataValue[0].refrigeration_temperature;

                    // Time
                    objDataValue.stored_date = listDataValue[0].stored_date;
                    objDataValue.stored_hour = hour;
                    objDataValue.stored_minute = 0;
                    int count = listDataValue.Count;

                    bool updateMPSFlag = true;
                    bool updateTNFlag = true;
                    bool updateTPFlag = true;
                    bool updateTOCFlag = true;
                    //bool updateStationStatus = true;
                    bool updateWaterSampler = true;
                    int countingMPSCal = 1;
                    int countingStationStatusCal = 1;
                    int countingTNCal = 1;
                    int countingTPCal = 1;
                    int countingTOCCal = 1;
                    int countingWaterSampler = 1;

                    for (int i = 1; i < count; i++)
                    {
                        // MPS
                        if (updateMPSFlag)
                        {
                            if (listDataValue[i].MPS_status == CommonInfo.INT_STATUS_NORMAL)
                            {
                                objDataValue.MPS_DO = objDataValue.MPS_DO + listDataValue[i].MPS_DO;
                                objDataValue.MPS_DO_status = listDataValue[i].MPS_status;
                                objDataValue.MPS_EC = objDataValue.MPS_EC + listDataValue[i].MPS_EC;
                                objDataValue.MPS_EC_status = listDataValue[i].MPS_status;
                                objDataValue.MPS_ORP = objDataValue.MPS_ORP + listDataValue[i].MPS_ORP;
                                objDataValue.MPS_ORP_status = listDataValue[i].MPS_status;
                                objDataValue.MPS_pH = objDataValue.MPS_pH + listDataValue[i].MPS_pH;
                                objDataValue.MPS_pH_status = listDataValue[i].MPS_status;
                                objDataValue.MPS_Temp = objDataValue.MPS_Temp + listDataValue[i].MPS_Temp;
                                objDataValue.MPS_Temp_status = listDataValue[i].MPS_status;
                                objDataValue.MPS_Turbidity = objDataValue.MPS_Turbidity + listDataValue[i].MPS_Turbidity;
                                objDataValue.MPS_Turbidity_status = listDataValue[i].MPS_status;
                                objDataValue.MPS_status = listDataValue[i].MPS_status;
                                countingMPSCal++;
                            }
                            else
                            {
                                objDataValue.MPS_DO = -1;
                                objDataValue.MPS_DO_status = listDataValue[i].MPS_status;
                                objDataValue.MPS_EC = -1;
                                objDataValue.MPS_EC_status = listDataValue[i].MPS_status;
                                objDataValue.MPS_ORP = -1;
                                objDataValue.MPS_ORP_status = listDataValue[i].MPS_status;
                                objDataValue.MPS_pH = -1;
                                objDataValue.MPS_pH_status = listDataValue[i].MPS_status;
                                objDataValue.MPS_Temp = -1;
                                objDataValue.MPS_Temp_status = listDataValue[i].MPS_status;
                                objDataValue.MPS_Turbidity = -1;
                                objDataValue.MPS_Turbidity_status = listDataValue[i].MPS_status;
                                objDataValue.MPS_status = listDataValue[i].MPS_status;
                                updateMPSFlag = false;
                            }
                        }

                        // TN
                        if (updateTNFlag)
                        {
                            objDataValue.TN_status = listDataValue[i].TN_status;
                            if (objDataValue.TN_status == CommonInfo.INT_STATUS_NORMAL)
                            {
                                objDataValue.TN = objDataValue.TN + listDataValue[i].TN;
                                countingTNCal++;
                            }
                            else
                            {
                                objDataValue.TN = -1;
                                updateTNFlag = false;
                            }
                        }

                        // TP
                        if (updateTPFlag)
                        {
                            objDataValue.TP_status = listDataValue[i].TP_status;
                            if (objDataValue.TP_status == CommonInfo.INT_STATUS_NORMAL)
                            {
                                objDataValue.TP = objDataValue.TP + listDataValue[i].TP;
                                countingTPCal++;
                            }
                            else
                            {
                                objDataValue.TP = -1;
                                updateTPFlag = false;
                            }
                        }
                        // TOC
                        if (updateTOCFlag)
                        {
                            objDataValue.TOC_status = listDataValue[i].TOC_status;
                            if (objDataValue.TOC_status == CommonInfo.INT_STATUS_NORMAL)
                            {
                                objDataValue.TOC = objDataValue.TOC + listDataValue[i].TOC;
                                countingTOCCal++;
                            }
                            else
                            {
                                objDataValue.TOC = -1;
                                updateTOCFlag = false;
                            }
                        }

                        // Station status
                        if (listDataValue[i].module_Temperature > 0)
                        {
                            objDataValue.module_Temperature = objDataValue.module_Temperature
                                + listDataValue[i].module_Temperature;
                            objDataValue.module_Humidity = objDataValue.module_Humidity
                                    + listDataValue[i].module_Humidity;
                            countingStationStatusCal++;
                        }
                        // Water sampler
                        if (updateWaterSampler)
                        {
                            objDataValue.equipment_status = listDataValue[i].equipment_status;
                            if (objDataValue.equipment_status == CommonInfo.INT_STATUS_NORMAL)
                            {
                                objDataValue.refrigeration_temperature = objDataValue.refrigeration_temperature
                                    + listDataValue[i].refrigeration_temperature;
                                objDataValue.bottle_position = objDataValue.bottle_position
                                   + listDataValue[i].bottle_position;
                                countingWaterSampler++;
                            }
                            else
                            {
                                objDataValue.refrigeration_temperature = -1;
                                objDataValue.bottle_position = -1;
                                updateWaterSampler = false;
                            }
                        }
                    }
                    if (updateMPSFlag)
                    {
                        objDataValue.MPS_DO = (double)objDataValue.MPS_DO / (double)countingMPSCal;
                        objDataValue.MPS_EC = (double)objDataValue.MPS_EC / (double)countingMPSCal;
                        objDataValue.MPS_ORP = (double)objDataValue.MPS_ORP / (double)countingMPSCal;
                        objDataValue.MPS_pH = (double)objDataValue.MPS_pH / (double)countingMPSCal;
                        objDataValue.MPS_Temp = (double)objDataValue.MPS_Temp / (double)countingMPSCal;
                        objDataValue.MPS_Turbidity = (double)objDataValue.MPS_Turbidity / (double)countingMPSCal;
                    }
                    if (updateTNFlag)
                    {
                        objDataValue.TN = (double)objDataValue.TN / (double)countingTNCal;
                    }
                    if (updateTPFlag)
                    {
                        objDataValue.TP = (double)objDataValue.TP / (double)countingTPCal;
                    }
                    if (updateTOCFlag)
                    {
                        objDataValue.TOC = (double)objDataValue.TOC / (double)countingTOCCal;
                    }// Station status
                    if (objDataValue.module_Temperature > 0)
                    {
                        objDataValue.module_Temperature = (double)objDataValue.module_Temperature / (double)countingStationStatusCal;
                        objDataValue.module_Humidity = (double)objDataValue.module_Humidity / (double)countingStationStatusCal;
                    }
                    if (updateWaterSampler)
                    {
                        objDataValue.refrigeration_temperature = (double)objDataValue.refrigeration_temperature
                                 / (double)countingWaterSampler;
                        objDataValue.bottle_position = objDataValue.bottle_position
                           / countingWaterSampler;
                    }

                    // get latest to check before add
                    objLatest = new data_60minute_value_repository().get_latest_info();
                    if (objLatest != null &&
                        objLatest.created.Date == objDataValue.created.Date &&
                        objLatest.created.Month == objDataValue.created.Month &&
                        objLatest.created.Year == objDataValue.created.Year &&
                        objLatest.stored_hour == objDataValue.stored_hour &&
                        objLatest.stored_minute == objDataValue.stored_minute)
                    {
                        // update to
                        // MPS

                        if (objLatest.MPS_status == CommonInfo.INT_STATUS_NORMAL &&
                            objDataValue.MPS_status == CommonInfo.INT_STATUS_NORMAL)
                        {
                            objLatest.MPS_DO = (objLatest.MPS_DO + objDataValue.MPS_DO) / 2;
                            objLatest.MPS_DO_status = objDataValue.MPS_status;
                            objLatest.MPS_EC = (objLatest.MPS_EC + objDataValue.MPS_EC) / 2;
                            objLatest.MPS_EC_status = objDataValue.MPS_status;
                            objLatest.MPS_ORP = (objLatest.MPS_ORP + objDataValue.MPS_ORP) / 2;
                            objLatest.MPS_ORP_status = objDataValue.MPS_status;
                            objLatest.MPS_pH = (objLatest.MPS_pH + objDataValue.MPS_pH) / 2;
                            objLatest.MPS_pH_status = objDataValue.MPS_status;
                            objLatest.MPS_Temp = (objLatest.MPS_Temp + objDataValue.MPS_Temp) / 2;
                            objLatest.MPS_Temp_status = objDataValue.MPS_status;
                            objLatest.MPS_Turbidity = (objLatest.MPS_Turbidity + objDataValue.MPS_Turbidity) / 2;
                            objLatest.MPS_Turbidity_status = objDataValue.MPS_status;
                            objLatest.MPS_status = objDataValue.MPS_status;
                        }
                        else
                        {
                            objLatest.MPS_DO = -1;
                            objLatest.MPS_DO_status = objLatest.MPS_status;
                            objLatest.MPS_EC = -1;
                            objLatest.MPS_EC_status = objLatest.MPS_status;
                            objLatest.MPS_ORP = -1;
                            objLatest.MPS_ORP_status = objLatest.MPS_status;
                            objLatest.MPS_pH = -1;
                            objLatest.MPS_pH_status = objLatest.MPS_status;
                            objLatest.MPS_Temp = -1;
                            objLatest.MPS_Temp_status = objLatest.MPS_status;
                            objLatest.MPS_Turbidity = -1;
                            objLatest.MPS_Turbidity_status = objLatest.MPS_status;
                            objLatest.MPS_status = objLatest.MPS_status;
                            if (objDataValue.MPS_status != CommonInfo.INT_STATUS_NORMAL)
                            {
                                objLatest.MPS_DO_status = objDataValue.MPS_status;
                                objLatest.MPS_EC_status = objDataValue.MPS_status;
                                objLatest.MPS_ORP_status = objDataValue.MPS_status;
                                objLatest.MPS_pH_status = objDataValue.MPS_status;
                                objLatest.MPS_Temp_status = objDataValue.MPS_status;
                                objLatest.MPS_Turbidity_status = objDataValue.MPS_status;
                                objLatest.MPS_status = objDataValue.MPS_status;
                            }

                        }
                        // TN
                        if (objDataValue.TN_status == CommonInfo.INT_STATUS_NORMAL &&
                            objLatest.TN_status == CommonInfo.INT_STATUS_NORMAL)
                        {
                            objLatest.TN = (objDataValue.TN + objLatest.TN) / 2;
                        }
                        else
                        {
                            objLatest.TN = -1;
                            if (objDataValue.TN_status != CommonInfo.INT_STATUS_NORMAL)
                            {
                                objLatest.TN_status = objDataValue.TN_status;
                            }
                        }


                        // TP
                        if (objDataValue.TP_status == CommonInfo.INT_STATUS_NORMAL &&
                            objLatest.TP_status == CommonInfo.INT_STATUS_NORMAL)
                        {
                            objLatest.TP = (objDataValue.TP + objLatest.TP) / 2;
                        }
                        else
                        {
                            objLatest.TP = -1;
                            if (objDataValue.TP_status != CommonInfo.INT_STATUS_NORMAL)
                            {
                                objLatest.TP_status = objDataValue.TP_status;
                            }
                        }
                        // TOC
                        if (objDataValue.TOC_status == CommonInfo.INT_STATUS_NORMAL &&
                            objLatest.TOC_status == CommonInfo.INT_STATUS_NORMAL)
                        {
                            objLatest.TOC = (objDataValue.TOC + objLatest.TOC) / 2;
                        }
                        else
                        {
                            objLatest.TOC = -1;
                            if (objDataValue.TOC_status != CommonInfo.INT_STATUS_NORMAL)
                            {
                                objLatest.TOC_status = objDataValue.TOC_status;
                            }
                        }

                        // Station status
                        if (objLatest.module_Temperature > 0 && objDataValue.module_Temperature > 0)
                        {
                            objLatest.module_Temperature = (objDataValue.module_Temperature
                                + objLatest.module_Temperature) / 2;
                            objLatest.module_Humidity = (objDataValue.module_Humidity
                                    + objLatest.module_Humidity) / 2;
                        }
                        // Water sampler


                        if (objDataValue.equipment_status == CommonInfo.INT_STATUS_NORMAL &&
                            objLatest.equipment_status == CommonInfo.INT_STATUS_NORMAL)
                        {
                            objLatest.refrigeration_temperature = (objDataValue.refrigeration_temperature
                                + objLatest.refrigeration_temperature) / 2;
                            objLatest.bottle_position = (objDataValue.bottle_position
                               + objLatest.bottle_position) / 2;
                        }
                        else
                        {
                            objLatest.refrigeration_temperature = -1;
                            objLatest.bottle_position = -1;
                            if (objDataValue.equipment_status != CommonInfo.INT_STATUS_NORMAL)
                            {
                                objLatest.equipment_status = objDataValue.equipment_status;
                            }

                        }
                        //// save to data value table
                        if (new data_60minute_value_repository().update(ref objLatest) > 0)
                        {
                            // ok
                        }
                        else
                        {
                            // fail
                        }
                        status = 1;
                    }
                    else
                    {
                        if (GlobalVar.isMaintenanceStatus && GlobalVar.maintenanceLog.pumping_system == 1)
                        {
                            objDataValue.pumping_system_status = CommonInfo.INT_STATUS_MAINTENANCE;
                            //objDataValue.station_status = CommonInfo.INT_STATUS_MAINTENANCE;
                        }
                        //// save to data value table
                        if (new data_60minute_value_repository().add(ref objDataValue) > 0)
                        {
                            // ok
                        }
                        else
                        {
                            // fail
                        }
                        status = 2;
                    }

                    min_minute = tempMinute;
                    listDataValue.Clear();
                }
                else
                {
                    // add to list
                }

            }
            latestCalculate60Minute = DateTime.Now;
            max_minute = tempMinute;
            hour = tempHour;
            if (obj != null)
            {
                listDataValue.Add(obj);
            }
            if (status == 0)
            {
                return null;
            }
            else if (status == 1)
            {
                return objLatest;
            }
            else
            {
                return objDataValue;
            }
        }
    }
    public class ReceivedEventArgs : EventArgs
    {
        public byte[] Data { get; set; }
    }
}
