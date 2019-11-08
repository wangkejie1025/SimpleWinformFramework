using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialApplicationLayer
{
    //最高位为寄存器数量
    public enum RegistorAddress : UInt16
    {
        ThreeRS232 = 0x0001,
        TwoRS485 = 0x0002,
        Internet = 0x0003,
        HardwareVersion = 0x0004,
        Flash = 0x0005,
        Ferroelectric = 0x0006,
        SRAM = 0x0007,
        Switch = 0x0008,
        USB = 0x0009,
        DO = 0x0020,
        DI = 0x0022,
        WatchDog = 0x0031,
        SystemTick = 0x0032,
        LosePower = 0x0035
    }
    //从机地址类型
    public enum SlaveAddress : byte
    {
        JSRS232 = 0x01,
        COM1RS232 = 0x02,
        COM2RS232 = 0x03,
        J4RS485_1 = 0x05,
        J4RS485_2 = 0x06,
        Internet = 0x08
    }
    public enum RegistorAddressBitMask : UInt16
    {
        HighAddress = 0x0f00,
        LowAddress = 0x00ff,
        RegistorNumber = 0xf000
    }
    public enum Type:byte
    {
        RS232,
        RS485,
        CAN
    }
    public enum Driction:byte
    {
        TX,
        RX
    }
    public class DataFrame
    {
        /// <summary>
        /// 数据标识，用作数据的接收过滤，CAN为CAN ID
        /// </summary>
        public UInt32 DataIdentifier { get; set; }
        public string Time { get { return DateTime.Now.ToString("yyyy-mm-dd:hh:MM:ss"); } }
        public byte[] Data { get; set; }
        public override string ToString()
        {
            return base.ToString();
        }
    }
    public class CANDataFrame:DataFrame
    {
        
    }
}
