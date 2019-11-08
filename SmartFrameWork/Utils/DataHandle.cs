using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFrameWork.Utils
{
    public class DataHandle
    {
        public static UInt16 GetCRC(byte[] data)  //通过计算，获取校验码
        {
            UInt16 i = 0;
            UInt16 j = 0;
            UInt16 modbus_crc = 0xFFFF;

            for (i = 0; i < data.Length; i++)
            {
                modbus_crc = Convert.ToUInt16((modbus_crc & 0xFF00) | ((modbus_crc & 0x00FF) ^ data[i]));
                for (j = 1; j <= 8; j++)
                {
                    if ((modbus_crc & 0x01) == 1)
                    {
                        modbus_crc = Convert.ToUInt16(modbus_crc >> 1);
                        modbus_crc ^= 0xA001;
                    }
                    else
                    {
                        modbus_crc = Convert.ToUInt16(modbus_crc >> 1);
                    }
                }
            }
            return modbus_crc;
        }
        public static UInt16 GetCRC(byte[] data,int index)  //通过计算，获取校验码
        {
            UInt16 i = 0;
            UInt16 j = 0;
            UInt16 modbus_crc = 0xFFFF;
            if (data.Length <= index)
                return 0;
            byte[] temp = new byte[data.Length - index];
            data = temp;
            for (i = 0; i < data.Length; i++)
            {
                modbus_crc = Convert.ToUInt16((modbus_crc & 0xFF00) | ((modbus_crc & 0x00FF) ^ data[i]));
                for (j = 1; j <= 8; j++)
                {
                    if ((modbus_crc & 0x01) == 1)
                    {
                        modbus_crc = Convert.ToUInt16(modbus_crc >> 1);
                        modbus_crc ^= 0xA001;
                    }
                    else
                    {
                        modbus_crc = Convert.ToUInt16(modbus_crc >> 1);
                    }
                }
            }
            return modbus_crc;
        }
    }
}
