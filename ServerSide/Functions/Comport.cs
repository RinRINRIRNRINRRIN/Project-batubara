using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Serilog;
using ServerSide.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlTypes;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerSide.Functions
{
    internal class Comport
    {
        public Comport()
        {
            try
            {
                Port = ConfigurationManager.AppSettings["DevicePort"];

                // แปลง string -> int
                BaudRate = int.Parse(ConfigurationManager.AppSettings["BAUDRATE"]);

                // แก้ไข Parity
                // รูปแบบ: (Type)Enum.Parse(typeof(Type), ค่าString)
                string parityStr = ConfigurationManager.AppSettings["PARITY"];
                Parityz = (Parity)Enum.Parse(typeof(Parity), parityStr);

                // แก้ไข StopBits
                string stopBitStr = ConfigurationManager.AppSettings["STOPBIT"];
                Stopbitz = (StopBits)Enum.Parse(typeof(StopBits), stopBitStr);

                // แปลง string -> int
                Databitsz = int.Parse(ConfigurationManager.AppSettings["DATABIT"]);

                ScaleName = ConfigurationManager.AppSettings["SCALENAME"];

            }
            catch (Exception ex)
            {

                Log.Error("Comport,Comport : " + ex.Message);
            }

        }


        public string Port { get; set; }
        public int BaudRate { get; set; }
        public Parity Parityz { get; set; }
        public StopBits Stopbitz { get; set; }
        public int Databitsz { get; set; }

        public string ScaleName { get; set; }

        public string ERR { get; set; }

        public Parity[] GetParityList()
        {
            // ดึงค่า Enum ทั้งหมดแปลงเป็น Array
            return (Parity[])Enum.GetValues(typeof(Parity));
        }

        public StopBits[] GetStopBitsList()
        {
            // ดึงค่า Enum ทั้งหมดแปลงเป็น Array
            return (StopBits[])Enum.GetValues(typeof(StopBits));
        }

        public int[] GetDataBitsList()
        {
            // ค่ามาตรฐานคือ 5, 6, 7, 8 (ส่วนใหญ่ใช้ 8)
            return new int[] { 5, 6, 7, 8 };
        }

        public List<string> getBaudrate()
        {
            List<string> baudRates = new List<string>
    {
        "300",
        "1200",
        "2400",
        "4800",
        "9600",   // Arduino UNO ปกติใช้ค่านี้
        "14400",
        "19200",
        "38400",
        "57600",
        "115200", // ESP32 / ESP8266 นิยมใช้ค่านี้
        "230400",
        "250000",
        "500000",
        "1000000",
        "2000000"
    };
            return baudRates;
        }

        public List<PortsModel> getDeviceName()
        {
            List<PortsModel> lists = new List<PortsModel>();
            try
            {
                Log.Information("==Get all port in computer");
                // ใช้ WMI ค้นหาอุปกรณ์ที่มีคำว่า (COM...) ในชื่อ
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Caption like '%(COM%)'"))
                {
                    var portnames = SerialPort.GetPortNames();
                    var portResults = searcher.Get();

                    foreach (var device in portResults)
                    {
                        string caption = device["Caption"].ToString(); // ชื่อเต็ม เช่น "Arduino Uno (COM3)"

                        // หาว่า Device นี้คือ COM ไหน
                        // วิธีง่ายคือดูว่า Caption มีคำว่า COMx ที่เรามีอยู่ในระบบไหม
                        foreach (string port in portnames)
                        {
                            Log.Information("Port in computer : " + port);
                            if (caption.Contains($"({port})"))
                            {
                                lists.Add(new PortsModel
                                {
                                    DeviceNames = caption,
                                    DevicePort = port
                                });
                                Log.Information("Define port : " + caption + " : " + port);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ERR = ex.Message;
                Log.Error("Comport,getDeviceName : " + ex.Message);
            }
            return lists;
        }

        public List<string> getScaleName()
        {
            List<string> scaleNames = new List<string>();
            scaleNames.Add("HP05");
            scaleNames.Add("3590ET");
            scaleNames.Add("3590ETD");
            scaleNames.Add("IQ-355");
            scaleNames.Add("X3");
            return scaleNames;
        }

        public bool Connect(SerialPort _SerialPort)
        {
            try
            {
                _SerialPort.PortName = Port;
                _SerialPort.BaudRate = BaudRate;
                _SerialPort.Parity = Parityz;
                _SerialPort.StopBits = Stopbitz;
                _SerialPort.DataBits = Databitsz;
                if (_SerialPort.IsOpen)
                    _SerialPort.Close();

                _SerialPort.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ERR = ex.Message;
                return false;
            }
            return true;
        }

        public bool Disconnect(SerialPort _SerialPort)
        {
            try
            {
                if (_SerialPort.IsOpen)
                {
                    _SerialPort.Close();
                    _SerialPort.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ERR = ex.Message;
                return false;
            }
            return true;
        }

        public string ReceiveWeight(SerialPort _SerialPort)
        {
            string weightStr = "ERROR";
            try
            {
                switch (ScaleName)
                {
                    case "HP05":
                        // ข้อมูลดิบอาจมาเป็น: {STX}(0    12560     0{CR}{LF}
                        // 1. ล้างตัวอักษรขยะออกให้หมด
                        string rawData = _SerialPort.ReadLine();
                        string cleanData = rawData.Replace("\x02", "")  // STX
                                               .Replace("\r", "")    // CR
                                               .Replace("\n", "")    // LF
                                               .Replace("(", "")     // วงเล็บเปิด
                                               .Replace(")", "")     // วงเล็บปิด (เผื่อมี)
                                               .Trim();              // ตัดช่องว่างหน้าหลังสุด

                        // 2. แยกชุดข้อมูลด้วยช่องว่าง (Space)
                        // ไม่ว่าจะมีช่องว่างกี่ตัว คำสั่งนี้จะจัดการให้หมด
                        string[] parts = cleanData.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        // 3. เลือกค่าที่เป็นน้ำหนัก (ปกติมักอยู่ตัวที่ 2 หรือ index 1)
                        // ข้อมูลจะเป็นอาร์เรย์ประมาณนี้: ["0", "12560", "0"]
                        if (parts.Length >= 2)
                        {
                            if (parts[0] == "2")
                                weightStr = "-" + parts[1];
                            else
                                weightStr = parts[1];
                        }
                        break;
                    case "3590ETD":
                        string aa = _SerialPort.ReadLine();
                        string[] bb = aa.Split(',');
                        weightStr = bb[2].Trim();
                        break;
                    default:
                        Log.Warning("Scale not match : " + ScaleName);
                        weightStr = "SCALE NOT MATCH";
                        break;
                }

            }
            catch (Exception ex)
            {
                ERR = ex.Message;
                Log.Error("Comports,ReceiveWeight : " + ex.Message);
                return weightStr;
            }
            return weightStr;
        }



    }
}
