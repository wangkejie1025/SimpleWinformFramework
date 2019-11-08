using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFrameWork.Interface
{
    public interface IPortListing
    {
        void port_DataReceived(object sender, SerialDataReceivedEventArgs e);
    }
    //用于Modubus传输层接口的定义
    public interface ITransportLayer
    {
        string Name { get; }
        bool Connected { get; set; }
        void Connect();
        void Disconnect();
        void Send(byte[] data);
    }
}
