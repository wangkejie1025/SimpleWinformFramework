using System;
using System.IO.Ports;
using TransportLayer;
using SmartFrameWork.Services;
using System.Collections.Concurrent;
using System.Collections.Generic;
using SmartFrameWork.Interface;

namespace SerialApplicationLayer
{
    //功能码
    public enum FunctionCode:byte
    {
        ReadRegistor=0x03,
        WriteRegistor=0x06,
        WriteMultRegistors=0x10
    }
   
    //应用层依赖具体的传输层对象
    public class Modbus
    {
        public ITransportLayer PortLayer { get; set; }
        private BlockingCollection<DataFrame> response;
        public Modbus(ITransportLayer portLayer)
        {
            this.PortLayer = portLayer;
            response = new BlockingCollection<DataFrame>();
        }

        public void DataHandler(byte data)
        {

        }
        public void ReadRegistor(byte slaveAddress,byte functionCode, RegistorAddress registorAddress)
        {
            byte[] commond = new byte[8];
            byte registorNumber = (byte)((UInt16)registorAddress & 0xf000);
            commond[0] = slaveAddress;
            commond[1] = functionCode;
            commond[2] = (byte)((UInt16)registorAddress & 0x0f00);
            commond[3] = (byte)((UInt16)registorAddress & 0x00ff);
            commond[4] = 0;
            commond[5] = registorNumber;//寄存器个数
            UInt16 crc16 = SmartFrameWork.Utils.DataHandle.GetCRC(commond, 2);
            commond[6] = (byte)(crc16 & 0xff00);
            commond[7] = (byte)(crc16 & 0x00ff);

            PortLayer.Send(commond);
        }
        public void WriteRegistor(byte slaveAddress, byte functionCode, RegistorAddress registorAddress,byte[] data)
        {
            byte[] commond = new byte[8];
            commond[0] = slaveAddress;
            commond[1] = functionCode;
            commond[2] = (byte)((UInt16)registorAddress & 0x0f00);
            commond[3]= (byte)((UInt16)registorAddress & 0x00ff);
            commond[4] = data[0];
            commond[5] = data[1];
            UInt16 crc16 = SmartFrameWork.Utils.DataHandle.GetCRC(commond,2);
            commond[6] = (byte)(crc16 & 0xff00);
            commond[7]= (byte)(crc16 & 0x00ff);

            PortLayer.Send(commond);
        }
        public void WriteRegistors(byte slaveAddress, byte functionCode, RegistorAddress registorAddress,byte registorNumber, byte[] data)
        {
            List<byte> commond = new List<byte>();
            byte byteNumber = (byte)(registorNumber * 2);
            commond.Add(slaveAddress);
            commond.Add(functionCode);
            commond.Add((byte)((UInt16)registorAddress & 0x0f00));
            commond.Add((byte)((UInt16)registorAddress & 0x00ff));
            commond.Add(0);
            commond.Add(registorNumber);
            commond.Add(byteNumber);
            commond.AddRange(data);
            UInt16 crc16= SmartFrameWork.Utils.DataHandle.GetCRC(commond.ToArray());
            commond.Add((byte)(crc16 & 0xff00));
            commond.Add((byte)(crc16 & 0xff00));

            PortLayer.Send(commond.ToArray());
        }
    }
}
