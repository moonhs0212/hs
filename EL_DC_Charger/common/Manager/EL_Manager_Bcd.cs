using System;
using System.Collections.Generic;
using System.Text;

namespace EL_DC_Charger.common.Manager
{
    public class EL_Manager_Bcd
    {
        public static string BCDtoString(byte[] bcd)
        {
            StringBuilder temp = new StringBuilder(bcd.Length * 2);

            for (int i = 0; i < bcd.Length; i++)
            {
                temp.Append((byte)((bcd[i] & 0xf0) >> 4));
                temp.Append((byte)(bcd[i] & 0x0f));
            }

            return temp.ToString();
        }

        public static byte[] stringToBCD(string bcdString)
        {
            int length = bcdString.Length;

            if (length % 2 != 0)
            {
                length += 1;
                bcdString = "0" + bcdString;
            }

            byte[] bcd_Date = new byte[length / 2];

            int indexArray = 0;
            int bytearray = 0;
            for (int i = 0; i < bcdString.Length; i += 2)
            {
                bcd_Date[bytearray] = Convert.ToByte(bcdString[indexArray].ToString(), 16); indexArray++;
                bcd_Date[bytearray] <<= 4;
                bcd_Date[bytearray] |= Convert.ToByte(bcdString[indexArray].ToString(), 16); indexArray++;
                bytearray++;
            }
            return bcd_Date;
        }
    }
}
