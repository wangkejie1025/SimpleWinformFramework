using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using SmartFrameWork.Interface;

namespace TransportLayer
{
    public enum SerialPortStatus 
    {
        /// <summary>
        /// No error
        /// </summary>
        SerialPort_OK = 1,
        SerialPort_ERROR_OPENFAILED = -1
    }
    public class SerialPortTransportLayer: ITransportLayer
    {
        private SerialPort Port { get; set; }

        public string Name { get; set; }
        public bool Connected { get; set; }

        public SerialPortTransportLayer()
        {
            Port = new SerialPort();
            
        }
        //实例化串口对象
        public SerialPortTransportLayer(string PortName)
        {
            Port = new SerialPort();
            try
            {
                Port.PortName = PortName;
                Port.BaudRate = 9600;
                Port.DataBits = 8;
                Port.Parity = Parity.None;
                Port.StopBits = StopBits.One;
                Port.RtsEnable = true;
                Port.DtrEnable = true;
                Port.ReceivedBytesThreshold = 1;
                Port.Open();
            }
            catch { }
        }
        public SerialPortTransportLayer(string PortName, int Baudrate)
        {

        }
        public void OpenPort(string PortName)
        {
            
        }
        public void OpenPort(string PortName, int Baudrate)
        {
            if (Port != null)
            {
                if (!Port.IsOpen)
                {
                    try
                    {
                        Port.PortName = PortName;
                        Port.BaudRate = Baudrate;
                        Port.DataBits = 8;
                        Port.Parity = Parity.None;
                        Port.StopBits = StopBits.One;
                        Port.RtsEnable = true;
                        Port.DtrEnable = true;
                        Port.ReceivedBytesThreshold = 1;
                        Port.Open();
                    }
                    catch { }
                }
            }
        }
        public void OpenPort(string PortName, int Baudrate, byte DataBit, string Parity, string StopBits, bool RtsEnable, bool DtrEnable)
        {
            if (Port != null)
            {
                if (!Port.IsOpen)
                {
                    try
                    {
                        Port.PortName = PortName;
                        Port.BaudRate = Baudrate;
                        Port.DataBits = DataBit;
                        Port.Parity = (Parity)Enum.Parse(typeof(Parity), Parity);
                        Port.StopBits = (StopBits)Enum.Parse(typeof(StopBits), StopBits);
                        Port.RtsEnable = RtsEnable;
                        Port.DtrEnable = DtrEnable;
                        Port.ReceivedBytesThreshold = 1;
                        Port.Open();
                    }
                    catch {  }
                }
            }
        }
        public void Connect()
        {
            if (Port.IsOpen)
                Port.Close();
        }
        public void Disconnect()
        {

        }
        public void Send(byte[] data)
        {
            if (Port.IsOpen)
            {
                Port.Write(data, 0, data.Length);
            }
        }
    }
}
