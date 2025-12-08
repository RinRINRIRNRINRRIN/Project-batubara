using Guna.UI2.WinForms;
using Serilog;
using ServerSide.Functions;
using ServerSide.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Forms;

namespace ServerSide.Pages_Client_Site
{
    public partial class frmSetting : Form
    {
        public frmSetting()
        {
            InitializeComponent();

            //Port = ConfigurationManager.AppSettings["COMPORT"];
            //BaudRate = int.Parse(ConfigurationManager.AppSettings["BAUDRATE"]);
            //string parityStr = ConfigurationManager.AppSettings["PARITY"];
            //Parityz = (Parity)Enum.Parse(typeof(Parity), parityStr);
            //string stopBitStr = ConfigurationManager.AppSettings["STOPBIT"];
            //Stopbitz = (StopBits)Enum.Parse(typeof(StopBits), stopBitStr);
            //Databitsz = int.Parse(ConfigurationManager.AppSettings["DATABIT"]);
        }

        string Port { get; set; }
        int BaudRate { get; set; }
        Parity Parityz { get; set; }
        StopBits Stopbitz { get; set; }
        int Databitsz { get; set; }
        string ScaleName { get; set; }

        string SelectScaleName { get; set; }
        string DevicePortName { get; set; }
        string DevicePort { get; set; }
        void defineParameter()
        {
            txtEndpoint.Text = ConfigurationManager.AppSettings["EndpointAPIs"];
            txtPrinterThermal.Text = ConfigurationManager.AppSettings["PrinterName"];
            DevicePort = ConfigurationManager.AppSettings["DevicePort"];
            DevicePortName = ConfigurationManager.AppSettings["DevicePortName"];
            cbbScaleName.Items.Clear();
            cbbScaleName.Items.Add(ConfigurationManager.AppSettings["SCALENAME"]);
            cbbScaleName.SelectedIndex = 0;
            cbbPort.Items.Clear();
            cbbPort.Items.Add(DevicePortName);
            cbbPort.SelectedIndex = 0;
            cbbBaudrate.Items.Clear();
            cbbBaudrate.Items.Add(ConfigurationManager.AppSettings["BAUDRATE"]);
            cbbBaudrate.SelectedIndex = 0;
            cbbParity.Items.Clear();
            cbbParity.Items.Add(ConfigurationManager.AppSettings["PARITY"]);
            cbbParity.SelectedIndex = 0;
            cbbDatabit.Items.Clear();
            cbbDatabit.Items.Add(ConfigurationManager.AppSettings["DATABIT"]);
            cbbDatabit.SelectedIndex = 0;
            cbbStopbit.Items.Clear();
            cbbStopbit.Items.Add(ConfigurationManager.AppSettings["STOPBIT"]);
            cbbStopbit.SelectedIndex = 0;
        }

        private bool ConnectToPort()
        {
            try
            {
                // ปิด Port เก่าก่อนถ้าเปิดอยู่
                if (serialPort1.IsOpen)
                    serialPort1.Close();

                serialPort1.PortName = DevicePort;
                serialPort1.BaudRate = int.Parse(cbbBaudrate.Text);
                serialPort1.DataBits = int.Parse(cbbDatabit.Text);
                serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), cbbParity.Text);
                serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cbbStopbit.Text);

                serialPort1.Open();
                pnScale.Enabled = false;
                SelectScaleName = cbbScaleName.Text;
                // MessageBox.Show($"Connected to {portName} successfully!", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Log.Error("frmSetting,ConnectToPort : " + ex.Message);
                MessageBox.Show($"Cannot connect to {DevicePort}. \nReason: {ex.Message}", "เกิดข้อผิดผลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private bool disConnect()
        {
            try
            {
                if (serialPort1.IsOpen)
                    serialPort1.Close();
                pnScale.Enabled = true;
                txtWeight.Clear();
            }
            catch (Exception ex)
            {
                Log.Error("frmSetting,disconnect : " + ex.Message);
                return false;
            }
            return true;
        }

        void saveConfig()
        {
            try
            {
                // check value empty
                if (string.IsNullOrEmpty(DevicePort) || string.IsNullOrEmpty(DevicePortName) || cbbBaudrate.Text == "" || cbbParity.Text == "" || cbbDatabit.Text == "" || cbbStopbit.Text == "" || cbbScaleName.Text == "")
                {
                    MessageBox.Show("กรุณากรอกข้อมูลเครื่องชั่งให้ครบก่อนบันทึก", "ข้อมูลเครื่องชั่งไม่ครบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtEndpoint.Text == "")
                {
                    MessageBox.Show("กรุณากรอกข้อมูล Endpoint ของ ERP หากยังไม่มีให้ใส่ค่า - ", "Endpoint ว่าง", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtPrinterThermal.Text == "")
                {
                    MessageBox.Show("กรุณากรอกชื่อปริ้นเตอร์ทีต้องการพิมพ์ก่อน", "ชื่อปริ้นเตอร์ว่าง", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Log.Information("Save config");
                Configuration Config1 = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                Config1.AppSettings.Settings["DevicePortName"].Value = DevicePortName;
                Config1.AppSettings.Settings["DevicePort"].Value = DevicePort;
                Config1.AppSettings.Settings["BAUDRATE"].Value = cbbBaudrate.Text;
                Config1.AppSettings.Settings["PARITY"].Value = cbbParity.Text;
                Config1.AppSettings.Settings["DATABIT"].Value = cbbDatabit.Text;
                Config1.AppSettings.Settings["STOPBIT"].Value = cbbStopbit.Text;
                Config1.AppSettings.Settings["PrinterName"].Value = txtPrinterThermal.Text;
                Config1.AppSettings.Settings["EndpointAPIs"].Value = txtEndpoint.Text;

                Log.Information($"DevicePortName :  " + DevicePortName);
                Log.Information($"DevicePort :  " + DevicePortName);
                Log.Information($"BAUDRATE :  " + cbbBaudrate.Text);
                Log.Information($"PARITY :  " + cbbParity.Text);
                Log.Information($"DATABIT :  " + cbbDatabit.Text);
                Log.Information($"STOPBIT :  " + cbbStopbit.Text);
                Log.Information($"PrinterName :  " + txtPrinterThermal.Text);
                Log.Information($"EndpointAPIs :  " + txtEndpoint.Text);
                Config1.Save(ConfigurationSaveMode.Modified);
                Log.Information("Save config success");
                MessageBox.Show("บันทึกรายการสำเร็จ กรุณาเปิดโปรแกรมใหม่อีกคร้ัง", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
            catch (Exception ex)
            {
                Log.Error("frmSetting,saveConfig :  " + ex.Message);
                MessageBox.Show("เกิดข้อผิดผลาดในการบันทึกข้อมูลโปรแกรม \n" +
                    "Error : " + ex.Message, "ข้อผิดผลาดในการบันทึก", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmSetting_Load(object sender, EventArgs e)
        {
            try
            {
                Port = ConfigurationManager.AppSettings["COMPORT"];
                BaudRate = int.Parse(ConfigurationManager.AppSettings["BAUDRATE"]);
                string parityStr = ConfigurationManager.AppSettings["PARITY"];
                Parityz = (Parity)Enum.Parse(typeof(Parity), parityStr);
                string stopBitStr = ConfigurationManager.AppSettings["STOPBIT"];
                Stopbitz = (StopBits)Enum.Parse(typeof(StopBits), stopBitStr);
                Databitsz = int.Parse(ConfigurationManager.AppSettings["DATABIT"]);

                defineParameter();
            }
            catch (Exception ex)
            {
                Log.Error("frmSetting,frmSetting_Load : " + ex.Message);
            }

        }

        private void cbbPort_DropDown(object sender, EventArgs e)
        {
            Comport port = new Comport();
            List<PortsModel> ports = port.getDeviceName();
            if (ports == null)
            {
                MessageBox.Show("เกิดข้อผิดผลาดในการดึง comport \n" +
                    "Error : " + port.ERR, "Get port computer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cbbPort.Items.Clear();
            foreach (var item in ports)
            {
                cbbPort.Items.Add(item);
            }
        }



        // อย่าลืมปิด Port เมื่อปิดโปรแกรม

        private void cbbPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbPort.SelectedItem is PortsModel selectedDevice)
            {
                // กำหนดค่าไปที่ Prop 
                DevicePortName = selectedDevice.DeviceNames;
                DevicePort = selectedDevice.DevicePort;
            }
        }

        private void btnScaleConnect_Click(object sender, EventArgs e)
        {
            Guna2Button btn = sender as Guna2Button;
            switch (btn.Text)
            {
                case "เชื่อมต่อ":
                    if (ConnectToPort())
                        btn.Text = "ยกเลิกเชื่อมต่อ";
                    break;
                case "ยกเลิกเชื่อมต่อ":
                    if (disConnect())
                        btn.Text = "เชื่อมต่อ";
                    break;
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int weigh = 0;
                switch (SelectScaleName)
                {
                    case "HP05":
                        break;
                    case "3590ETD":
                        string a = serialPort1.ReadLine();
                        string[] b = a.Split(',');
                        if (int.TryParse(b[2].Trim(), out weigh))
                            BeginInvoke(new MethodInvoker(delegate ()
                            {
                                txtWeight.Text = weigh.ToString();
                            }));
                        break;
                    default:
                        Log.Warning("Scale not match : " + ScaleName);
                        BeginInvoke(new MethodInvoker(delegate ()
                        {
                            txtWeight.Text = "SCALE NOT MATCH";
                        }));
                        break;
                }

            }
            catch (Exception ex)
            {
                Log.Error("frmSetting,serialPort1_DataReceived : " + ex.Message);
            }
        }

        private void cbbBaudrate_DropDown(object sender, EventArgs e)
        {
            Comport comport = new Comport();
            List<string> lists = comport.getBaudrate();
            if (lists == null)
            {
                MessageBox.Show("เกิดข้อผิดผลาดในการดึง baudrate \n" +
                   "Error : " + comport.ERR, "Get baudrate", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cbbBaudrate.Items.Clear();
            foreach (var item in lists)
            {
                cbbBaudrate.Items.Add(item);
            }
        }

        private void cbbParity_DropDown(object sender, EventArgs e)
        {
            Comport comport = new Comport();
            Parity[] parities = comport.GetParityList();
            if (parities == null)
            {
                MessageBox.Show("เกิดข้อผิดผลาดในการดึง parities \n" +
                   "Error : " + comport.ERR, "Get parities", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            cbbParity.Items.Clear();
            foreach (var item in parities)
            {
                cbbParity.Items.Add(item);
            }
        }

        private void cbbStopbit_DropDown(object sender, EventArgs e)
        {
            Comport comport = new Comport();
            StopBits[] stopBits = comport.GetStopBitsList();
            if (stopBits == null)
            {
                MessageBox.Show("เกิดข้อผิดผลาดในการดึง stopBits \n" +
                   "Error : " + comport.ERR, "Get stopBits", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cbbStopbit.Items.Clear();
            foreach (var item in stopBits)
            {
                cbbStopbit.Items.Add(item);
            }
        }

        private void cbbDatabit_DropDown(object sender, EventArgs e)
        {
            Comport comport = new Comport();
            int[] databit = comport.GetDataBitsList();
            if (databit == null)
            {
                MessageBox.Show("เกิดข้อผิดผลาดในการดึง databit \n" +
                   "Error : " + comport.ERR, "Get databit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cbbDatabit.Items.Clear();
            foreach (var item in databit)
            {
                cbbDatabit.Items.Add(item);
            }
        }

        private void cbbScaleName_DropDown(object sender, EventArgs e)
        {
            Comport comport = new Comport();
            List<string> scalename = comport.getScaleName();
            if (scalename == null)
            {
                MessageBox.Show("เกิดข้อผิดผลาดในการดึง scalename \n" +
                   "Error : " + comport.ERR, "Get scalename", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cbbScaleName.Items.Clear();
            foreach (var item in scalename)
            {
                cbbScaleName.Items.Add(item);
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void cbbDatabit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            saveConfig();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                    serialPort1.Close();
            }
            catch
            {


            }
        }
    }
}
