using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Resources;
using System.Reflection;
using System.Globalization;
using DataLogger.Utils;
using System.IO.Ports;

using DataLogger.Entities;
using DataLogger.Data;
using System.Collections;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using WinformProtocol;

namespace DataLogger
{
    public partial class frmConfiguration : Form
    {
        LanguageService lang;
        // 1: KECO_STD; 2: ANALYZER; 3: MODBUS
        public string[] PROTOCOL_LIST = { "KECO_STD", "ANALYZER", "MODBUS" };

        SerialPort SAMPPort;
        frmNewMain newMain;
        // module configuration list
        public string[] MODULE_CONFIG_LIST = {"Power", "UPS", "Door", "Fire", "Flow", "Pump (L) A/M",
                                             "Pump (L) R/S", "Pump (L) FLT", "Pump (R) A/M",
                                             "Pump (R) R/S", "Pump (R) FLT",
                                             "Air1", "Air2", "Cleaning",
                                             "Temperature", "Humidity","pH","Orp","Temp","DO","Turb","Cond"};
        public string[] MODULE_ID_LIST = { "40171", "4050", "4051", "40172" };

        DataTable dt = new DataTable();

        module_repository _modules = new module_repository();

        public static Form1 protocol;
        public frmConfiguration()
        {
            InitializeComponent();
        }

        public frmConfiguration(LanguageService _lang,frmNewMain newmain)
        {
            InitializeComponent();
            lang = _lang;
            switch_language();
            //SAMPPort = SAMP;
            newMain = newmain;
        }
        private void switch_language()
        {
            this.Text = lang.getText("configuration");
            //this.grbStationConfig.Text = lang.getText("station_configuration");
            //this.grbComportConfiguration.Text = lang.getText("comport_configuration");
            //this.grbModuleConfiguration.Text = lang.getText("module_configuration");
            // statoin configuration            
            lang.setText(lblStationName, "station_name");
            lang.setText(lblStationID, "station_id");
            lang.setText(lblSocketPort, "socket_port");
            // comport configuration           
            lang.setText(lblSamplerComport, "sampler");

            this.lblComport.Text = lang.getText("comport");
            this.lblCommProtocol.Text = lang.getText("protocol");

            // preview                                    
            lang.setText(btnSave, "button_save");

        }
        private void frmConfiguration_Load(object sender, EventArgs e)
        {
            refreshDataForControl();
        }
        private bool isOpen(int port)
        {

            //int port = 456; //<--- This is your value
            int isAvailable = 0;
            // Evaluate current system tcp connections. This is the same information provided
            // by the netstat command line application, just in .Net strongly-typed object
            // form.  We will look through the list, and if our port we would like to use
            // in our TcpClient is occupied, we will set isAvailable to false.
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpListeners();
            foreach (IPEndPoint tcpi in tcpConnInfoArray)
            {
                if (tcpi.Port == port)
                {
                    isAvailable = 1;
                    break;
                }
            }
            if (isAvailable == 1)
            {
                return true;
            }
            else return false;
        }

        private void refreshDataForControl()
        {
            // get all comport name from computer.
            string[] availableComportList;
            availableComportList = SerialPort.GetPortNames();
            if (availableComportList.Length <= 0)
            {
                MessageBox.Show(lang.getText("available_port_null"));
                //this.Close();
                //return;
            }
            // check station info setting
            station existedStationsSetting = new station_repository().get_info();
            if (existedStationsSetting == null)
            {
                existedStationsSetting = new station();
                if (new station_repository().add(ref existedStationsSetting) > 0)
                {
                    // insert ok to database
                }
                else
                {
                    MessageBox.Show(lang.getText("system_error"));
                    return;
                }
            }
            else
            {
                // set data to control from existed station setting
                txtStationName.Text = existedStationsSetting.station_name;
                txtStationID.Text = existedStationsSetting.station_id;
                txtSocketPort.Text = existedStationsSetting.socket_port.ToString();
                if (isOpen(existedStationsSetting.socket_port))
                {
                    this.btnSOCKET.Image = global::DataLogger.Properties.Resources.ON_switch_96x25;
                    btnShow.Enabled = true;
                }
                else
                {
                    this.btnSOCKET.Image = global::DataLogger.Properties.Resources.OFF_switch_96x25;
                    btnShow.Enabled = false;
                }
                var sortComportList = availableComportList.OrderBy(port => Convert.ToInt32(port.Replace("COM", string.Empty)));
                int selectedIndex = 0;
                cbModule.Items.Clear();
                cbSampler.Items.Clear();
                cbMPS.Items.Clear();
                cbTN.Items.Clear();
                cbTP.Items.Clear();
                cbTOC.Items.Clear();
                cbMPSProtocol.Items.Clear();
                cbTNProtocol.Items.Clear();
                cbTPProtocol.Items.Clear();
                cbTOCProtocol.Items.Clear();
                //cbModulePH.Items.Clear();
                //cbModuleOrp.Items.Clear();
                //cbModuleTemp.Items.Clear();
                //cbModuleDO.Items.Clear();
                //cbModuleTurb.Items.Clear();
                //cbModuleCond.Items.Clear();
                //cbBottlepossition.Items.Clear();
                //cbTempinside.Items.Clear();
                //cbAutosampledoor.Items.Clear();
                //cbGetsample.Items.Clear();
                foreach (string itemAvailableComportName in sortComportList)
                {
                    cbModule.Items.Add(itemAvailableComportName);
                    cbSampler.Items.Add(itemAvailableComportName);
                    cbMPS.Items.Add(itemAvailableComportName);
                    cbTN.Items.Add(itemAvailableComportName);
                    cbTP.Items.Add(itemAvailableComportName);
                    cbTOC.Items.Add(itemAvailableComportName);
                    if (existedStationsSetting.module_comport == itemAvailableComportName)
                    {
                        cbModule.SelectedIndex = selectedIndex;
                    }
                    if (existedStationsSetting.sampler_comport == itemAvailableComportName)
                    {
                        cbSampler.SelectedIndex = selectedIndex;
                    }
                    if (existedStationsSetting.mps_comport == itemAvailableComportName)
                    {
                        cbMPS.SelectedIndex = selectedIndex;
                    }
                    if (existedStationsSetting.tn_comport == itemAvailableComportName)
                    {
                        cbTN.SelectedIndex = selectedIndex;
                    }
                    if (existedStationsSetting.tp_comport == itemAvailableComportName)
                    {
                        cbTP.SelectedIndex = selectedIndex;
                    }
                    if (existedStationsSetting.toc_comport == itemAvailableComportName)
                    {
                        cbTOC.SelectedIndex = selectedIndex;
                    }
                    selectedIndex = selectedIndex + 1;
                }

                foreach (string itemProtocol in PROTOCOL_LIST)
                {
                    cbMPSProtocol.Items.Add(itemProtocol);
                    cbTNProtocol.Items.Add(itemProtocol);
                    cbTPProtocol.Items.Add(itemProtocol);
                    cbTOCProtocol.Items.Add(itemProtocol);
                }

                cbMPSProtocol.SelectedIndex = existedStationsSetting.mps_protocol - 1;
                cbTNProtocol.SelectedIndex = existedStationsSetting.tn_protocol - 1;
                cbTPProtocol.SelectedIndex = existedStationsSetting.tp_protocol - 1;
                cbTOCProtocol.SelectedIndex = existedStationsSetting.toc_protocol - 1;

            }
            //foreach (string itemModuleID in MODULE_ID_LIST)
            //{
            //    cbModulePower.Items.Add(itemModuleID);
            //    cbModuleUPS.Items.Add(itemModuleID);
            //    cbModuleDoor.Items.Add(itemModuleID);
            //    cbModuleFire.Items.Add(itemModuleID);
            //    cbModuleFlow.Items.Add(itemModuleID);
            //    cbModulePumpLAM.Items.Add(itemModuleID);
            //    cbModulePumpLRS.Items.Add(itemModuleID);
            //    cbModulePumpLFLT.Items.Add(itemModuleID);
            //    cbModulePumpRAM.Items.Add(itemModuleID);
            //    cbModulePumpRRS.Items.Add(itemModuleID);
            //    cbModulePumpRFLT.Items.Add(itemModuleID);
            //    cbModuleAir1.Items.Add(itemModuleID);
            //    cbModuleAir2.Items.Add(itemModuleID);
            //    cbModuleCleaning.Items.Add(itemModuleID);
            //    cbModulePH.Items.Add(itemModuleID);
            //    cbModuleOrp.Items.Add(itemModuleID);
            //    cbModuleTemp.Items.Add(itemModuleID);
            //    cbModuleDO.Items.Add(itemModuleID);
            //    cbModuleTurb.Items.Add(itemModuleID);
            //    cbModuleCond.Items.Add(itemModuleID);
            //    cbBottlepossition.Items.Add(itemModuleID);
            //    cbTempinside.Items.Add(itemModuleID);
            //    cbAutosampledoor.Items.Add(itemModuleID);
            //    cbGetsample.Items.Add(itemModuleID);
            //}
            //txtDO1.Text = existedStationsSetting.do1_caption;
            //txtDO2.Text = existedStationsSetting.do2_caption;
            //txtDO3.Text = existedStationsSetting.do3_caption;
            //txtDO4.Text = existedStationsSetting.do4_caption;
            //txtDO5.Text = existedStationsSetting.do5_caption;
            //txtDO6.Text = existedStationsSetting.do6_caption;
            //txtDO7.Text = existedStationsSetting.do7_caption;
            //txtDO8.Text = existedStationsSetting.do8_caption;

            //txtDO1_vi.Text = existedStationsSetting.do1_caption_vi;
            //txtDO2_vi.Text = existedStationsSetting.do2_caption_vi;
            //txtDO3_vi.Text = existedStationsSetting.do3_caption_vi;
            //txtDO4_vi.Text = existedStationsSetting.do4_caption_vi;
            //txtDO5_vi.Text = existedStationsSetting.do5_caption_vi;
            //txtDO6_vi.Text = existedStationsSetting.do6_caption_vi;
            //txtDO7_vi.Text = existedStationsSetting.do7_caption_vi;
            //txtDO8_vi.Text = existedStationsSetting.do8_caption_vi;

            IEnumerable<module> moduleConfigurationList = checkAndInsertModuleConfiguration();
            foreach (module itemModuleSetting in moduleConfigurationList)
            {
                displayModuleSetting(itemModuleSetting);
            }

            //dgvModuleConfiguration.DataSource = null;
            //dt.Clear();
            //// module configuration
            //dt.Columns.Add("id", typeof(int));
            //dt.Columns.Add("item_name", typeof(string));
            //dt.Columns.Add("module_id", typeof(int));
            //dt.Columns.Add("channel_number", typeof(int));

            //IEnumerable<module> moduleConfigurationList = checkAndInsertModuleConfiguration();
            //foreach (module itemModuleConfig in moduleConfigurationList)
            //{
            //    dt.Rows.Add(itemModuleConfig.id, itemModuleConfig.item_name, 
            //                itemModuleConfig.module_id,itemModuleConfig.channel_number);
            //}
            //dgvModuleConfiguration.DataSource = dt;
            //DataGridViewComboBoxColumn cbColumn = new DataGridViewComboBoxColumn();
            //cbColumn.HeaderText = "Module ID";
            //cbColumn.Name = "cb_module_id";
            //ArrayList row = new ArrayList();
            //row.Add("4017");
            //row.Add("4050");
            //row.Add("4051");
            //cbColumn.Items.AddRange(row.ToArray());            
            //dgvModuleConfiguration.Columns.Add(cbColumn);

            //dgvModuleConfiguration.Columns["id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dgvModuleConfiguration.Columns["item_name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dgvModuleConfiguration.Columns["module_id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dgvModuleConfiguration.Columns["channel_number"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dgvModuleConfiguration.Columns["cb_module_id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //int rowsNumber = dgvModuleConfiguration.Rows.Count;            
        }

        private void displayModuleSetting(module itemModuleSetting)
        {
            string channel_no = itemModuleSetting.channel_number.ToString("0#");
            int module_id = itemModuleSetting.module_id - 1;
            string on = itemModuleSetting.on_value;
            string off = itemModuleSetting.off_value;
            int input_min = itemModuleSetting.input_min;
            int input_max = itemModuleSetting.input_max;
            int output_min = itemModuleSetting.output_min;
            int output_max = itemModuleSetting.output_max;
            double offset = itemModuleSetting.off_set;
            //switch (itemModuleSetting.item_name.ToLower())
            //{
            //    case "power":
            //        txtPowerChannel.Text = channel_no;
            //        cbModulePower.SelectedIndex = module_id;
            //        txtPowerON.Text = on;
            //        txtPowerOFF.Text = off;
            //        break;
            //    case "ups":
            //        txtUPSChannel.Text = channel_no;
            //        cbModuleUPS.SelectedIndex = module_id;
            //        txtUPSON.Text = on;
            //        txtUPSOFF.Text = off;
            //        break;
            //    case "door":
            //        txtDoorChannel.Text = channel_no;
            //        cbModuleDoor.SelectedIndex = module_id;
            //        txtDoorON.Text = on;
            //        txtDoorOFF.Text = off;
            //        break;
            //    case "fire":
            //        txtFireChannel.Text = channel_no;
            //        cbModuleFire.SelectedIndex = module_id;
            //        txtFireON.Text = on;
            //        txtFireOFF.Text = off;
            //        break;
            //    case "flow":
            //        txtFlowChannel.Text = channel_no;
            //        cbModuleFlow.SelectedIndex = module_id;
            //        txtFlowON.Text = on;
            //        txtFlowOFF.Text = off;
            //        break;
            //    case "pump (l) a/m":
            //        txtPumpLAMChannel.Text = channel_no;
            //        cbModulePumpLAM.SelectedIndex = module_id;
            //        txtPumpLAMON.Text = on;
            //        txtPumpLAMOFF.Text = off;
            //        break;
            //    case "pump (l) r/s":
            //        txtPumpLRSChannel.Text = channel_no;
            //        cbModulePumpLRS.SelectedIndex = module_id;
            //        txtPumpLRSON.Text = on;
            //        txtPumpLRSOFF.Text = off;
            //        break;
            //    case "pump (l) flt":
            //        txtPumpLFLTChannel.Text = channel_no;
            //        cbModulePumpLFLT.SelectedIndex = module_id;
            //        txtPumpLFLTON.Text = on;
            //        txtPumpLFLTOFF.Text = off;
            //        break;
            //    case "pump (r) a/m":
            //        txtPumpRAMChannel.Text = channel_no;
            //        cbModulePumpRAM.SelectedIndex = module_id;
            //        txtPumpRAMON.Text = on;
            //        txtPumpRAMOFF.Text = off;
            //        break;
            //    case "pump (r) r/s":
            //        txtPumpRRSChannel.Text = channel_no;
            //        cbModulePumpRRS.SelectedIndex = module_id;
            //        txtPumpRRSON.Text = on;
            //        txtPumpRRSOFF.Text = off;
            //        break;
            //    case "pump (r) flt":
            //        txtPumpRFLTChannel.Text = channel_no;
            //        cbModulePumpRFLT.SelectedIndex = module_id;
            //        txtPumpRFLTON.Text = on;
            //        txtPumpRFLTOFF.Text = off;
            //        break;
            //    case "air1":
            //        txtAir1Channel.Text = channel_no;
            //        cbModuleAir1.SelectedIndex = module_id;
            //        txtAir1ON.Text = on;
            //        txtAir1OFF.Text = off;
            //        break;
            //    case "air2":
            //        txtAir2Channel.Text = channel_no;
            //        cbModuleAir2.SelectedIndex = module_id;
            //        txtAir2ON.Text = on;
            //        txtAir2OFF.Text = off;
            //        break;
            //    case "cleaning":
            //        txtCleaningChannel.Text = channel_no;
            //        cbModuleCleaning.SelectedIndex = module_id;
            //        txtCleaningON.Text = on;
            //        txtCleaningOFF.Text = off;
            //        break;
            //    case "temperature":
            //        //txtTemperatureChannel.Text = channel_no;
            //        //cbModuleTemperature.SelectedIndex = module_id;
            //        //txtTemperatureInputMin.Text = input_min.ToString();
            //        //txtTemperatureInputMax.Text = input_max.ToString();
            //        //txtTemperatureOutputMin.Text = output_min.ToString();
            //        //txtTemperatureOutputMax.Text = output_max.ToString();
            //        //txtTemperatureOffset.Text = offset.ToString();
            //        break;
            //    case "humidity":
            //        //txtHumidityChannel.Text = channel_no;
            //        //cbModuleHumidity.SelectedIndex = module_id;
            //        //txtHumidityInputMin.Text = input_min.ToString();
            //        //txtHumidityInputMax.Text = input_max.ToString();
            //        //txtHumidityOutputMin.Text = output_min.ToString();
            //        //txtHumidityOutputMax.Text = output_max.ToString();
            //        //txtHumidityOffset.Text = offset.ToString();
            //        break;
            //    case "ph":
            //        txtpHChannel.Text = channel_no;
            //        cbModulePH.SelectedIndex = module_id;
            //        txtpHInputMin.Text = input_min.ToString();
            //        txtpHInputMax.Text = input_max.ToString();
            //        txtpHOutputMin.Text = output_min.ToString();
            //        txtpHOutputMax.Text = output_max.ToString();
            //        txtpHOffset.Text = offset.ToString();
            //        break;
            //    case "orp":
            //        txtOrpChannel.Text = channel_no;
            //        cbModuleOrp.SelectedIndex = module_id;
            //        txtOrpInputMin.Text = input_min.ToString();
            //        txtOrpInputMax.Text = input_max.ToString();
            //        txtOrpOutputMin.Text = output_min.ToString();
            //        txtOrpOutputMax.Text = output_max.ToString();
            //        txtOrpOffset.Text = offset.ToString();
            //        break;
            //    case "temp":
            //        txtTempChannel.Text = channel_no;
            //        cbModuleTemp.SelectedIndex = module_id;
            //        txtTempInputMin.Text = input_min.ToString();
            //        txtTempInputMax.Text = input_max.ToString();
            //        txtTempOutputMin.Text = output_min.ToString();
            //        txtTempOutputMax.Text = output_max.ToString();
            //        txtTempOffset.Text = offset.ToString();
            //        break;
            //    case "do":
            //        txtDOChannel.Text = channel_no;
            //        cbModuleDO.SelectedIndex = module_id;
            //        txtDOInputMin.Text = input_min.ToString();
            //        txtDOInputMax.Text = input_max.ToString();
            //        txtDOOutputMin.Text = output_min.ToString();
            //        txtDOOutputMax.Text = output_max.ToString();
            //        txtDOOffset.Text = offset.ToString();
            //        break;
            //    case "turb":
            //        txtTurbChannel.Text = channel_no;
            //        cbModuleTurb.SelectedIndex = module_id;
            //        txtTurbInputMin.Text = input_min.ToString();
            //        txtTurbInputMax.Text = input_max.ToString();
            //        txtTurbOutputMin.Text = output_min.ToString();
            //        txtTurbOutputMax.Text = output_max.ToString();
            //        txtTurbOffset.Text = offset.ToString();
            //        break;
            //    case "cond":
            //        txtCondChannel.Text = channel_no;
            //        cbModuleCond.SelectedIndex = module_id;
            //        txtCondInputMin.Text = input_min.ToString();
            //        txtCondInputMax.Text = input_max.ToString();
            //        txtCondOutputMin.Text = output_min.ToString();
            //        txtCondOutputMax.Text = output_max.ToString();
            //        txtCondOffset.Text = offset.ToString();
            //        break;
            //    default:
            //        break;
            //}
        }

        private IEnumerable<module> checkAndInsertModuleConfiguration()
        {
            using (module_repository _modules = new module_repository())
            {
                // get all;
                IEnumerable<module> moduleConfigList = _modules.get_all();

                if (moduleConfigList.Count() == MODULE_CONFIG_LIST.Count())
                {
                    return moduleConfigList;
                }
                else
                {
                    foreach (string itemModuleName in MODULE_CONFIG_LIST)
                    {
                        module objExistedModuleByName = _modules.get_info_by_name(itemModuleName);
                        if (objExistedModuleByName != null)
                        {
                            continue;
                        }
                        else
                        {
                            objExistedModuleByName = new module();
                            objExistedModuleByName.item_name = itemModuleName;
                            if (_modules.add(ref objExistedModuleByName) > 0)
                            {
                                // ok
                            }
                            else
                            {
                                // fail
                                MessageBox.Show(lang.getText("system_error"));
                                this.Close();
                                return null;
                            }
                        }
                    }
                    moduleConfigList = _modules.get_all();
                    return moduleConfigList;
                }
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // validation

            // preparation

            // saving to db
            station objStationSetting = new station_repository().get_info();

            if (isOpen(objStationSetting.socket_port) && (!txtSocketPort.Text.Equals(objStationSetting.socket_port.ToString())))
            {
                //this.btnSOCKET.Image = global::DataLogger.Properties.Resources.OFF_switch_96x25;
                //close socket
                try
                {
                    //frmNewMain.tcpListener.Stop();
                    if (Application.OpenForms.OfType<Form1>().Count() == 1)
                    {
                        Application.OpenForms.OfType<Form1>().First().Close();
                        btnShow.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error ! Cant close this socket.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            try
            {
                objStationSetting.station_name = txtStationName.Text;
                objStationSetting.station_id = txtStationID.Text;
                objStationSetting.socket_port = Convert.ToInt32(txtSocketPort.Text.Trim());
                //MessageBox.Show("1");
                objStationSetting.sampler_comport = cbSampler.Text;
                objStationSetting.module_comport = cbModule.Text;
                objStationSetting.tn_comport = cbTN.Text;
                objStationSetting.tp_comport = cbTP.Text;
                objStationSetting.toc_comport = cbTOC.Text;
                objStationSetting.mps_comport = cbMPS.Text;
                objStationSetting.tn_protocol = cbTNProtocol.SelectedIndex + 1;
                objStationSetting.tp_protocol = cbTPProtocol.SelectedIndex + 1;
                objStationSetting.toc_protocol = cbTOCProtocol.SelectedIndex + 1;
                objStationSetting.mps_protocol = cbMPSProtocol.SelectedIndex + 1;
                //MessageBox.Show("2");
                //objStationSetting.do1_caption = txtDO1.Text;
                //objStationSetting.do2_caption = txtDO2.Text;
                //objStationSetting.do3_caption = txtDO3.Text;
                //objStationSetting.do4_caption = txtDO4.Text;
                //objStationSetting.do5_caption = txtDO5.Text;
                //objStationSetting.do6_caption = txtDO6.Text;
                //objStationSetting.do7_caption = txtDO7.Text;
                //objStationSetting.do8_caption = txtDO8.Text;

                //objStationSetting.do1_caption_vi = txtDO1_vi.Text;
                //objStationSetting.do2_caption_vi = txtDO2_vi.Text;
                //objStationSetting.do3_caption_vi = txtDO3_vi.Text;
                //objStationSetting.do4_caption_vi = txtDO4_vi.Text;
                //objStationSetting.do5_caption_vi = txtDO5_vi.Text;
                //objStationSetting.do6_caption_vi = txtDO6_vi.Text;
                //objStationSetting.do7_caption_vi = txtDO7_vi.Text;
                //objStationSetting.do8_caption_vi = txtDO8_vi.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cant SAVE !");
                this.Close();
                return;
            }
            try
            {
                //MessageBox.Show("3");
                if (new station_repository().update(ref objStationSetting) > 0)
                {
                    // ok
                }
                else
                {
                    // fail
                }

                //foreach (string itemModule in MODULE_CONFIG_LIST)
                //{
                //    switch (itemModule.ToLower())
                //    {
                //        case "power":
                //            module objPower = _modules.get_info_by_name(itemModule);

                //            objPower.module_id = cbModulePower.SelectedIndex + 1;
                //            objPower.channel_number = Convert.ToInt32(txtPowerChannel.Text);
                //            objPower.on_value = txtPowerON.Text;
                //            objPower.off_value = txtPowerOFF.Text;
                //            //MessageBox.Show("4");
                //            if (_modules.update(ref objPower) > 0)
                //            {
                //                // ok
                //            }
                //            else
                //            {
                //                // fail
                //            }
                //            break;
                //        case "ups":
                //            module objUPS = _modules.get_info_by_name(itemModule);

                //            objUPS.module_id = cbModuleUPS.SelectedIndex + 1;
                //            objUPS.channel_number = Convert.ToInt32(txtUPSChannel.Text);
                //            objUPS.on_value = txtUPSON.Text;
                //            objUPS.off_value = txtUPSOFF.Text;

                //            if (_modules.update(ref objUPS) > 0)
                //            {
                //                // ok
                //            }
                //            else
                //            {
                //                // fail
                //            }
                //            break;
                //        case "door":
                //            module objDoor = _modules.get_info_by_name(itemModule);

                //            objDoor.module_id = cbModuleDoor.SelectedIndex + 1;
                //            objDoor.channel_number = Convert.ToInt32(txtDoorChannel.Text);
                //            objDoor.on_value = txtDoorON.Text;
                //            objDoor.off_value = txtDoorOFF.Text;

                //            if (_modules.update(ref objDoor) > 0)
                //            {
                //                // ok
                //            }
                //            else
                //            {
                //                // fail
                //            }
                //            break;
                //        case "fire":
                //            module objFire = _modules.get_info_by_name(itemModule);

                //            objFire.module_id = cbModuleFire.SelectedIndex + 1;
                //            objFire.channel_number = Convert.ToInt32(txtFireChannel.Text);
                //            objFire.on_value = txtFireON.Text;
                //            objFire.off_value = txtFireOFF.Text;

                //            if (_modules.update(ref objFire) > 0)
                //            {
                //                // ok
                //            }
                //            else
                //            {
                //                // fail
                //            }
                //            break;
                //        case "flow":
                //            module objFlow = _modules.get_info_by_name(itemModule);

                //            objFlow.module_id = cbModuleFlow.SelectedIndex + 1;
                //            objFlow.channel_number = Convert.ToInt32(txtFlowChannel.Text);
                //            objFlow.on_value = txtFlowON.Text;
                //            objFlow.off_value = txtFlowOFF.Text;

                //            if (_modules.update(ref objFlow) > 0)
                //            {
                //                // ok
                //            }
                //            else
                //            {
                //                // fail
                //            }
                //            break;
                //        case "pump (l) a/m":
                //            module objPumpLAM = _modules.get_info_by_name(itemModule);

                //            objPumpLAM.module_id = cbModulePumpLAM.SelectedIndex + 1;
                //            objPumpLAM.channel_number = Convert.ToInt32(txtPumpLAMChannel.Text);
                //            objPumpLAM.on_value = txtPumpLAMON.Text;
                //            objPumpLAM.off_value = txtPumpLAMOFF.Text;

                //            if (_modules.update(ref objPumpLAM) > 0)
                //            {
                //                // ok
                //            }
                //            else
                //            {
                //                // fail
                //            }
                //            break;
                //        case "pump (l) r/s":
                //            module objPumpLRS = _modules.get_info_by_name(itemModule);

                //            objPumpLRS.module_id = cbModulePumpLRS.SelectedIndex + 1;
                //            objPumpLRS.channel_number = Convert.ToInt32(txtPumpLRSChannel.Text);
                //            objPumpLRS.on_value = txtPumpLRSON.Text;
                //            objPumpLRS.off_value = txtPumpLRSOFF.Text;

                //            if (_modules.update(ref objPumpLRS) > 0)
                //            {
                //                // ok
                //            }
                //            else
                //            {
                //                // fail
                //            }
                //            break;
                //        case "pump (l) flt":
                //            module objPumpLFLT = _modules.get_info_by_name(itemModule);

                //            objPumpLFLT.module_id = cbModulePumpLFLT.SelectedIndex + 1;
                //            objPumpLFLT.channel_number = Convert.ToInt32(txtPumpLFLTChannel.Text);
                //            objPumpLFLT.on_value = txtPumpLFLTON.Text;
                //            objPumpLFLT.off_value = txtPumpLFLTOFF.Text;

                //            if (_modules.update(ref objPumpLFLT) > 0)
                //            {
                //                // ok
                //            }
                //            else
                //            {
                //                // fail
                //            }
                //            break;
                //        case "pump (r) a/m":
                //            module objPumpRAM = _modules.get_info_by_name(itemModule);

                //            objPumpRAM.module_id = cbModulePumpRAM.SelectedIndex + 1;
                //            objPumpRAM.channel_number = Convert.ToInt32(txtPumpRAMChannel.Text);
                //            objPumpRAM.on_value = txtPumpRAMON.Text;
                //            objPumpRAM.off_value = txtPumpRAMOFF.Text;

                //            if (_modules.update(ref objPumpRAM) > 0)
                //            {
                //                // ok
                //            }
                //            else
                //            {
                //                // fail
                //            }
                //            break;
                //        case "pump (r) r/s":
                //            module objPumpRRS = _modules.get_info_by_name(itemModule);

                //            objPumpRRS.module_id = cbModulePumpRRS.SelectedIndex + 1;
                //            objPumpRRS.channel_number = Convert.ToInt32(txtPumpRRSChannel.Text);
                //            objPumpRRS.on_value = txtPumpRRSON.Text;
                //            objPumpRRS.off_value = txtPumpRRSOFF.Text;

                //            if (_modules.update(ref objPumpRRS) > 0)
                //            {
                //                // ok
                //            }
                //            else
                //            {
                //                // fail
                //            }
                //            break;
                //        case "pump (r) flt":
                //            module objPumpRFLT = _modules.get_info_by_name(itemModule);

                //            objPumpRFLT.module_id = cbModulePumpRFLT.SelectedIndex + 1;
                //            objPumpRFLT.channel_number = Convert.ToInt32(txtPumpRFLTChannel.Text);
                //            objPumpRFLT.on_value = txtPumpRFLTON.Text;
                //            objPumpRFLT.off_value = txtPumpRFLTOFF.Text;

                //            if (_modules.update(ref objPumpRFLT) > 0)
                //            {
                //                // ok
                //            }
                //            else
                //            {
                //                // fail
                //            }
                //            break;
                //        case "air1":
                //            module objAir1 = _modules.get_info_by_name(itemModule);

                //            objAir1.module_id = cbModuleAir1.SelectedIndex + 1;
                //            objAir1.channel_number = Convert.ToInt32(txtAir1Channel.Text);
                //            objAir1.on_value = txtAir1ON.Text;
                //            objAir1.off_value = txtAir1OFF.Text;

                //            if (_modules.update(ref objAir1) > 0)
                //            {
                //                // ok
                //            }
                //            else
                //            {
                //                // fail
                //            }
                //            break;
                //        case "air2":
                //            module objAir2 = _modules.get_info_by_name(itemModule);

                //            objAir2.module_id = cbModuleAir2.SelectedIndex + 1;
                //            objAir2.channel_number = Convert.ToInt32(txtAir2Channel.Text);
                //            objAir2.on_value = txtAir2ON.Text;
                //            objAir2.off_value = txtAir2OFF.Text;

                //            if (_modules.update(ref objAir2) > 0)
                //            {
                //                // ok
                //            }
                //            else
                //            {
                //                // fail
                //            }
                //            break;
                //        case "cleaning":
                //            module objCleaning = _modules.get_info_by_name(itemModule);

                //            objCleaning.module_id = cbModuleCleaning.SelectedIndex + 1;
                //            objCleaning.channel_number = Convert.ToInt32(txtCleaningChannel.Text);
                //            objCleaning.on_value = txtCleaningON.Text;
                //            objCleaning.off_value = txtCleaningOFF.Text;

                //            if (_modules.update(ref objCleaning) > 0)
                //            {
                //                // ok
                //            }
                //            else
                //            {
                //                // fail
                //            }
                //            break;
                //        case "temperature":
                //            //module objPumpTemperature = _modules.get_info_by_name(itemModule);

                //            //objPumpTemperature.module_id = cbModuleTemperature.SelectedIndex + 1;
                //            //objPumpTemperature.channel_number = Convert.ToInt32(txtTemperatureChannel.Text);
                //            //objPumpTemperature.input_min = Convert.ToInt32(txtTemperatureInputMin.Text);
                //            //objPumpTemperature.input_max = Convert.ToInt32(txtTemperatureInputMax.Text);
                //            //objPumpTemperature.output_min = Convert.ToInt32(txtTemperatureOutputMin.Text);
                //            //objPumpTemperature.output_max = Convert.ToInt32(txtTemperatureOutputMax.Text);
                //            //objPumpTemperature.off_set = Convert.ToDouble(txtTemperatureOffset.Text);
                //            //if (_modules.update(ref objPumpTemperature) > 0)
                //            //{
                //            //    // ok
                //            //}
                //            //else
                //            //{
                //            //    // fail
                //            //}
                //            break;
                //        case "humidity":
                //            //module objHumidity = _modules.get_info_by_name(itemModule);

                //            //objHumidity.module_id = cbModuleHumidity.SelectedIndex + 1;
                //            //objHumidity.channel_number = Convert.ToInt32(txtHumidityChannel.Text);
                //            //objHumidity.input_min = Convert.ToInt32(txtHumidityInputMin.Text);
                //            //objHumidity.input_max = Convert.ToInt32(txtHumidityInputMax.Text);
                //            //objHumidity.output_min = Convert.ToInt32(txtHumidityOutputMin.Text);
                //            //objHumidity.output_max = Convert.ToInt32(txtHumidityOutputMax.Text);
                //            //objHumidity.off_set = Convert.ToDouble(txtHumidityOffset.Text);
                //            //if (_modules.update(ref objHumidity) > 0)
                //            //{
                //            //    // ok
                //            //}
                //            //else
                //            //{
                //            //    // fail
                //            //}
                //            break;
                //        case "ph":
                //            module objpH = _modules.get_info_by_name(itemModule);

                //            objpH.module_id = cbModulePH.SelectedIndex + 1;
                //            objpH.channel_number = Convert.ToInt32(txtpHChannel.Text);
                //            objpH.input_min = Convert.ToInt32(txtpHInputMin.Text);
                //            objpH.input_max = Convert.ToInt32(txtpHInputMax.Text);
                //            objpH.output_min = Convert.ToInt32(txtpHOutputMin.Text);
                //            objpH.output_max = Convert.ToInt32(txtpHOutputMax.Text);
                //            objpH.off_set = Convert.ToDouble(txtpHOffset.Text);
                //            if (_modules.update(ref objpH) > 0)
                //            {
                //                // ok
                //            }
                //            else
                //            {
                //                // fail
                //            }
                //            break;
                //        case "orp":
                //            module objOrp = _modules.get_info_by_name(itemModule);

                //            objOrp.module_id = cbModuleOrp.SelectedIndex + 1;
                //            objOrp.channel_number = Convert.ToInt32(txtOrpChannel.Text);
                //            objOrp.input_min = Convert.ToInt32(txtOrpInputMin.Text);
                //            objOrp.input_max = Convert.ToInt32(txtOrpInputMax.Text);
                //            objOrp.output_min = Convert.ToInt32(txtOrpOutputMin.Text);
                //            objOrp.output_max = Convert.ToInt32(txtOrpOutputMax.Text);
                //            objOrp.off_set = Convert.ToDouble(txtOrpOffset.Text);
                //            if (_modules.update(ref objOrp) > 0)
                //            {
                //                // ok
                //            }
                //            else
                //            {
                //                // fail
                //            }
                //            break;
                //        case "temp":
                //            module objTemp = _modules.get_info_by_name(itemModule);

                //            objTemp.module_id = cbModuleTemp.SelectedIndex + 1;
                //            objTemp.channel_number = Convert.ToInt32(txtTempChannel.Text);
                //            objTemp.input_min = Convert.ToInt32(txtTempInputMin.Text);
                //            objTemp.input_max = Convert.ToInt32(txtTempInputMax.Text);
                //            objTemp.output_min = Convert.ToInt32(txtTempOutputMin.Text);
                //            objTemp.output_max = Convert.ToInt32(txtTempOutputMax.Text);
                //            objTemp.off_set = Convert.ToDouble(txtTempOffset.Text);
                //            if (_modules.update(ref objTemp) > 0)
                //            {
                //                // ok
                //            }
                //            else
                //            {
                //                // fail
                //            }
                //            break;
                //        case "do":
                //            module objDO = _modules.get_info_by_name(itemModule);

                //            objDO.module_id = cbModuleDO.SelectedIndex + 1;
                //            objDO.channel_number = Convert.ToInt32(txtDOChannel.Text);
                //            objDO.input_min = Convert.ToInt32(txtDOInputMin.Text);
                //            objDO.input_max = Convert.ToInt32(txtDOInputMax.Text);
                //            objDO.output_min = Convert.ToInt32(txtDOOutputMin.Text);
                //            objDO.output_max = Convert.ToInt32(txtDOOutputMax.Text);
                //            objDO.off_set = Convert.ToDouble(txtDOOffset.Text);
                //            if (_modules.update(ref objDO) > 0)
                //            {
                //                // ok
                //            }
                //            else
                //            {
                //                // fail
                //            }
                //            break;
                //        case "turb":
                //            module objTurb = _modules.get_info_by_name(itemModule);

                //            objTurb.module_id = cbModuleTurb.SelectedIndex + 1;
                //            objTurb.channel_number = Convert.ToInt32(txtTurbChannel.Text);
                //            objTurb.input_min = Convert.ToInt32(txtTurbInputMin.Text);
                //            objTurb.input_max = Convert.ToInt32(txtTurbInputMax.Text);
                //            objTurb.output_min = Convert.ToInt32(txtTurbOutputMin.Text);
                //            objTurb.output_max = Convert.ToInt32(txtTurbOutputMax.Text);
                //            objTurb.off_set = Convert.ToDouble(txtTurbOffset.Text);
                //            if (_modules.update(ref objTurb) > 0)
                //            {
                //                // ok
                //            }
                //            else
                //            {
                //                // fail
                //            }
                //            break;
                //        case "cond":
                //            module objCond = _modules.get_info_by_name(itemModule);

                //            objCond.module_id = cbModuleCond.SelectedIndex + 1;
                //            objCond.channel_number = Convert.ToInt32(txtCondChannel.Text);
                //            objCond.input_min = Convert.ToInt32(txtCondInputMin.Text);
                //            objCond.input_max = Convert.ToInt32(txtCondInputMax.Text);
                //            objCond.output_min = Convert.ToInt32(txtCondOutputMin.Text);
                //            objCond.output_max = Convert.ToInt32(txtCondOutputMax.Text);
                //            objCond.off_set = Convert.ToDouble(txtCondOffset.Text);
                //            if (_modules.update(ref objCond) > 0)
                //            {
                //                // ok
                //            }
                //            else
                //            {
                //                // fail
                //            }
                //            break;
                //        default:
                //            break;
                //    }
                //}
                MessageBox.Show("Success", "", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            refreshDataForControl();
        }

        private void grbStationConfig_Enter(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void label40_Click(object sender, EventArgs e)
        {

        }

        private void txtTemperatureOffset_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtHumidityOffset_TextChanged(object sender, EventArgs e)
        {

        }
        public static string StringToByteArray(string hexstring)
        {
            return String.Join(String.Empty,hexstring
                .Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("127.0.0.1");
        }
        private void btnSOCKET_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show("Save change ?", "Important Question", MessageBoxButtons.YesNo);
            if (result1 == DialogResult.Yes)
            {
                station existedStationsSetting = new station_repository().get_info();
                if (existedStationsSetting == null)
                {

                }
                else
                {
                    if (isOpen(existedStationsSetting.socket_port))
                    {
                        this.btnSOCKET.Image = global::DataLogger.Properties.Resources.OFF_switch_96x25;
                        //close socket
                        try
                        {
                            if (Application.OpenForms.OfType<Form1>().Count() == 1)
                            {
                                Application.OpenForms.OfType<Form1>().First().Close();
                                btnShow.Enabled = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error ! Cant close this socket.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        this.btnSOCKET.Image = global::DataLogger.Properties.Resources.ON_switch_96x25;
                        //open socket
                        Int32 port = existedStationsSetting.socket_port;
                        IPAddress localAddr = IPAddress.Parse(frmConfiguration.GetLocalIPAddress());
                        try
                        {
                            //frmNewMain.tcpListener = new TcpListener(localAddr, port);
                            //frmNewMain.tcpListener.Start();
                            if (Application.OpenForms.OfType<Form1>().Count() == 1)
                            {
                                Application.OpenForms.OfType<Form1>().First().Close();
                            }

                            protocol = new Form1(newMain);
                            //protocol = new Form1(this.SAMPPort);
                            protocol.Show();
                            btnShow.Enabled = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error ! Cant open this socket.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Form1>().Count() == 1)
            {
                if (btnShow.Text.Equals("Show"))
                {
                    protocol.Show();
                    btnShow.Text = "Hide";
                }
                if (btnShow.Text.Equals("Hide"))
                {
                    protocol.Hide();
                    btnShow.Text = "Show";
                }
            }
        }
    }
}
