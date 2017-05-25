using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;
using System.Globalization;
using DataLogger.Utils;
using DataLogger.Entities;
namespace DataLogger
{
    public partial class frmTPCalibrate : Form
    {
        //ResourceManager res_man;    // declare Resource manager to access to specific cultureinfo
        //CultureInfo cul;            //declare culture info
        LanguageService lang;

        private System.Threading.Timer tmrThreadingTimer;
        internal delegate void setTextb(string data);
        private delegate void setTexta(string text);
        // TP
        private int TP_rx_write = 0;
        private int TP_rx_counter = 0;
        private byte[] TP_rx_buffer = null;
        private byte[] TP_rx_buffer_ACK = null;

        private byte[] TP_receive_buffer = new byte[2048];
        private int TP_buffer_counter = 0;
        private const int PACKET_LENGTH = 100;
        public frmTPCalibrate(LanguageService _lang)
        {
            //MessageBox.Show(frmNewMain.TPComname);
            if (!frmNewMain.TPComname.Equals("COM100"))
            {
                InitializeComponent();
                serialPortTP.PortName = frmNewMain.TPComname;
                //res_man = obj_res_man;
                //cul = obj_cul;
                lang = _lang;
                switch_language();
                try
                {
                    //Console.Write("\n 2:" + serialPortTN.PortName);
                    serialPortTP.Open();
                }
                catch
                {
                    //MessageBox.Show("Can not open Comport for TN. Please check again!");
                }
            }
            else
            {

                MessageBox.Show("Please config TN comport before calibrate !");
                this.Close();
            }
        }
        private void switch_language()
        {
            this.Text = lang.getText("form_calibrate_data_title");
            this.lblFormula.Text = lang.getText("form_calibrate_formula_used");
            this.lblReCalibration.Text = lang.getText("form_calibrate_re_calibration");
        }

        private void serialPortTP_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                //if (!serialPortTN.IsOpen)
                //    return;
                //int bytes = serialPortTN.BytesToRead;
                //byte[] buffer = new byte[bytes];
                //serialPortTN.Read(buffer, 0, bytes);
                //for (int i = 0; i < bytes; i++)
                //{
                //    TN_rx_buffer[TN_rx_write++] = buffer[i];
                //    if (TN_rx_write == 2048)
                //        TN_rx_write = 0;
                //}
                //TN_rx_counter += bytes;
                //ProcessDataTN("");

                if (!serialPortTP.IsOpen)
                    return;
                int bytes = serialPortTP.BytesToRead;
                byte[] buffer = new byte[bytes];
                serialPortTP.Read(buffer, 0, bytes);
                //Console.Write("buffer \t"+ _encoder.GetString(buffer) + "\n");
                //Console.Write("buffer.length \t" + buffer.Length + "\n");
                //for (int i = 0; i < bytes; i++)
                //{
                //    TOC_rx_buffer[TOC_rx_write++] = buffer[i];
                //}
                if (TP_rx_buffer == null)
                {
                    TP_rx_buffer = buffer;
                }
                else TP_rx_buffer = TP_rx_buffer.Concat(buffer).ToArray();
                //Console.Write("TOC \t"+_encoder.GetString(TOC_rx_buffer) + "\n");
                TP_rx_counter += bytes;
                if (TP_rx_buffer[TP_rx_buffer.Length - 1] == 10 && TP_rx_buffer.Length >= PACKET_LENGTH)
                {
                    TP_rx_buffer = TP_rx_buffer.Take(TP_rx_buffer.Count() - 1).ToArray();
                }
                if ((TP_rx_buffer.Length == PACKET_LENGTH) || (TP_rx_buffer[0] == 0x06 && TP_rx_buffer.Length == 1))
                {
                    if ((TP_rx_buffer[0] == 0x06 && TP_rx_buffer.Length == 1))
                    {
                        MessageBox.Show("Success!");
                    }
                    else
                    {
                        ProcessDataTP("");
                    }
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
                MessageBox.Show(ex.Message);
            }
        }
        private void ProcessDataTP(string text)
        {
            try
            {
                if (this.txta.InvokeRequired)
                {
                    setTexta d = new setTexta(ProcessDataTP);
                    this.txta.Invoke(d, new object[] { text });
                }
                else
                {
                    TPParseData(TP_rx_buffer);
                    //TP_rx_counter = 0;
                    //TP_rx_write = 0;
                }
            }
            catch// (Exception ex)
            {

            }
            finally
            {

            }
        }

        private string TPParseData(byte[] text)
        {
            int bufferIndex = 0;
            string result = "";

            try
            {
                if (text.Length >= PACKET_LENGTH)
                {
                    // MessageBox.Show(Encoding.UTF8.GetString(text, 0, text.Length));
                    //for (int i = 0; i < text.Length - PACKET_LENGTH; i++)
                    for (int i = 0; i < text.Length; i++)
                    {
                        if (text[i] == 0x02 &&      // STX
                            text[i + 1] == 0x43 &&    // 'C'
                            text[i + 1] == 0x43 &&      // 'C'
                            text[PACKET_LENGTH + i - 1] == 0x0D && // CR
                            text[PACKET_LENGTH + i - 4] == 0x03) // ETX
                        {
                            // process data
                            string strDateTime = Encoding.UTF8.GetString(text, i + 5, 14);
                            string strN = Encoding.UTF8.GetString(text, i + 19, 2);
                            string strParameter = Encoding.UTF8.GetString(text, i + 21, 3);

                            string strStatus = Encoding.UTF8.GetString(text, i + 24, 2);
                            string strValueB = Encoding.UTF8.GetString(text, i + 26, 10);
                            string strValueA = Encoding.UTF8.GetString(text, i + 36, 10);
                            // add info 46,50 ----- 96
                            txta.Text = strValueA;
                            txtb.Text = strValueB;

                            DateTime dt = DateTime.ParseExact(strDateTime.Trim(), "yyyyMMddHHmmss", null);
                            txtDate.Text = dt.ToString("dd-MM-yyyy");
                            txtTime.Text = dt.ToString("HH:mm:ss");
                            result = string.Format("DateTime: {0}; Number Parameter: {1}; Parameter: {2}; Value A: {3}; Value B: {4}; Addition Info: {5}", strDateTime, strN, strParameter, strValueA, strValueB, strStatus);
                            //MessageBox.Show(result);
                            bufferIndex = i + PACKET_LENGTH;
                            i = i + PACKET_LENGTH - 1;
                        }
                    }
                }
                else
                {
                }
                //Reset buffer
                TP_buffer_counter = 0;
                Array.Clear(TP_receive_buffer, 0, TP_receive_buffer.Length);

                TP_rx_write = 0;
                TP_rx_counter = 0;
                TP_rx_buffer = null;

                TP_receive_buffer = new byte[2048];
                TP_buffer_counter = 0;
            }
            catch //(Exception ex)
            {
                //MessageBox.Show("0002," + ex.Message);
            }

            if (result == "")
                return result;
            return result;
        }

        private void bTPCalibrate_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.isLogin)
            {
                MessageBox.Show(lang.getText("login_before_to_do_this"));
                return;
            }
            else
            {
                tmrThreadingTimer.Change(Timeout.Infinite, Timeout.Infinite);
                requestInforRCHK(serialPortTP);
                //int bytes = serialPortTP.BytesToRead;
                //byte[] buffer = new byte[bytes];
                //serialPortTP.Read(buffer, 0, bytes);
                //TP_rx_buffer_ACK = buffer;
                //if (TP_rx_buffer_ACK[0] == 0x02)
                //{
                //    MessageBox.Show("Success!");
                //}
                //else
                //{
                //    MessageBox.Show("Fail!");
                //}
                //MessageBox.Show("Sended RCHK command !");
                tmrThreadingTimer.Change(0, 4000);
            }
        }

        private void frmTPCalibrate_Load(object sender, EventArgs e)
        {
            //requestInfor(serialPortTP);
            tmrThreadingTimer = new System.Threading.Timer(new TimerCallback(tmrThreadingTimer_TimerCallback), null, System.Threading.Timeout.Infinite, 4000);
            tmrThreadingTimer.Change(0, 4000);
        }

        public int countForRequest = 0;
        private void tmrThreadingTimer_TimerCallback(object state)
        {
            if (countForRequest > 5)
            {
                GlobalVar.calibrateTPStatus = CommonInfo.CALIBRATION_STATUS_DONE;
                //this.serialPortTP.Close();
            }
            else
            {
                GlobalVar.calibrateTPStatus = CommonInfo.CALIBRATION_STATUS_IN_PROGRESS;
                if (countForRequest < 2)
                {
                    requestInfor(serialPortTP);
                }
                countForRequest++;
            }
        }
        private static void requestInfor(SerialPort com)
        {
            if (com.IsOpen)
            {
                byte[] packet = new byte[9];
                //Fill to packet
                packet[0] = 0x02;//STX
                packet[1] = 0x43;//C
                packet[2] = 0x43;//C
                packet[3] = 0x48;//H
                packet[4] = 0x4B;//K
                packet[5] = 0x03;//ETX
                packet[6] = 0x31;//ETX
                packet[7] = 0x3E;//CHK
                packet[8] = 0x0D;//CR

                com.Write(packet, 0, 9);
            }
        }
        private static void requestInforRCHK(SerialPort com)
        {
            if (com.IsOpen)
            {
                byte[] packet = new byte[9];
                //Fill to packet
                packet[0] = 0x02;//STX
                packet[1] = 0x52;//R
                packet[2] = 0x43;//C
                packet[3] = 0x48;//H
                packet[4] = 0x4B;//K
                packet[5] = 0x03;//ETX
                packet[6] = 0x32;//CHK
                packet[7] = 0x3D;//CHK
                packet[8] = 0x0D;//CR

                com.Write(packet, 0, 9);
            }
        }
        private void frmTPCalibrate_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                try
                {
                    //serialPortTP.Close();
                }
                catch
                {

                }
                //this.Close();
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
