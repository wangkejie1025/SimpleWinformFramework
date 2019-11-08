using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartFrameWork.Interface;
using System.Net.Sockets;
using System.Net;

namespace EnternetTransportLayer
{
    public class EnternetTransportLayer:ITransportLayer
    {
        public string Name { get { return "Moudbus-TCP"; } }
        public bool Connected { get; set; }
        public string IPAddress { get; set; }
        public int Port { get; set; }
        public EnternetTransportLayer(string ipAddress,int port)
        {
            this.IPAddress = ipAddress;
            this.Port = port;
            TcpClient tcpClient = new TcpClient();
            var result = tcpClient.BeginConnect(IPAddress, Port, null, null);
            //var success = result.AsyncWaitHandle.WaitOne(connectTimeout);
        }
        public void Connect()
        {

        }
        public void Disconnect()
        {

        }
        public void Send(byte[] data)
        {

        }
    }
}
