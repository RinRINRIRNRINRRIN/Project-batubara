using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlTypes;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Functions
{
    internal class Comport
    {
        public Comport()
        {
            _Comport = ConfigurationManager.AppSettings["COMPORT"];
            _BaudRate = int.Parse(ConfigurationManager.AppSettings["BAUDRATE"]);
        }

        private string _Comport { get; set; }
        private int _BaudRate { get; set; }



        public string ERR { get; set; }

        public bool Connect(SerialPort _SerialPort)
        {
            try
            {
                _SerialPort.PortName = _Comport;
                _SerialPort.BaudRate = _BaudRate;
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

        public static int ReceiveWeight(SerialPort _SerialPort)
        {
            int weight = 0;
            try
            {
                string a = _SerialPort.ReadLine();
                string[] b = a.Split(',');
                weight = int.Parse(b[2].Trim());
            }
            catch
            {
                // ERR = ex.Message;
                return weight;
            }
            return weight;
        }



    }
}
