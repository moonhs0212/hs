using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace EL_DC_Charger.common.Manager
{
    public class EL_Manager_Conversion
    {

        public static String getInt_BCDCode_String(byte data)
        {
            int bcd = getInt_BCDCode(data);

            if (bcd < 10)
                return "0" + bcd;

            return "" + bcd;
        }
        public static bool getBoolean(String text)
        {
            if (text == null || text.Length < 1)
                return false;
            if (text.Equals("1") || text.Equals("TRUE") || text.Equals("true") || text.Equals("True"))
                return true;
            return false;
        }
        public static int getInt_BCDCode(byte data)
        {
            int bcd = (((data >> 4) & 0x0f) * 10) + (data & 0x0f);
            return bcd;
        }

        public static byte[] StringToSortByteASCIIArray(string str, int length)
        {
            byte[] btemp = new byte[length];
            byte[] bstr = Encoding.ASCII.GetBytes(str);
            if (bstr.Length > length)
            {
                // 문자열을 변환했더니 설정한 Length를 넘어갈경우
                Array.Copy(bstr, btemp, length);
                return btemp;
            }
            else
            {
                List<byte> bList = new List<byte>();
                for (int i = length - bstr.Length; i > 0; i--)
                {
                    // i = 최대길이 - 실제길이 : 늘려야할 길이
                    bList.Add(0x30);
                }
                foreach (byte b in bstr)
                {
                    bList.Add(b);
                }

                return bList.ToArray();
            }
        }
        public static byte[] StringToSortByteASCIIArray(string str, int length, bool bnull)
        {
            byte[] btemp = new byte[length];
            byte[] bstr = Encoding.ASCII.GetBytes(str);
            if (bstr.Length > length)
            {
                Array.Copy(bstr, btemp, length);
                return btemp;
            }
            else
            {
                List<byte> bList = new List<byte>();
                for (int i = length - bstr.Length; i > 0; i--)
                {
                    if (bnull) bList.Add(0x20);
                    else bList.Add(0x30);
                }
                foreach (byte b in bstr)
                {
                    bList.Add(b);
                }

                return bList.ToArray();
            }
        }

        public static byte[] makeByteArray(byte[] sourceArray)
        {
            if (sourceArray == null) return null;
            byte[] returndata = new byte[sourceArray.Length];

            Array.Copy(sourceArray, returndata, returndata.Length);
            return returndata;
        }
        public static double getDouble(String data)
        {
            double result = -1;
            try
            {
                result = double.Parse(data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return result;
        }
        public static long getLong(string data)
        {
            long result = -1L;
            try
            {
                result = long.Parse(data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return result;
        }
        public static float getFloat(string data)
        {
            float result = -1;
            try
            {
                result = float.Parse(data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return result;
        }
        public static long byte4arrayToLong_Changed(byte[] data) => data[2] << 24 | data[3] << 16 | data[0] << 8 | data[1];
        public static long byte4arrayToLong(byte[] data) => data[0] << 24 | data[1] << 16 | data[2] << 8 | data[3];

        //public static string byteArrayToBinaryString(byte[] ba) => string.Join(" ", ba.re.Select(x => Convert.ToString(x, 2).PadLeft(8, '0')));

        public static string byteArrayToOct(byte[] ba)
        {
            StringBuilder sb = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            return sb.ToString();
        }
        public static string HextoString(string InputText)
        {

            byte[] bb = Enumerable.Range(0, InputText.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(InputText.Substring(x, 2), 16))
                             .ToArray();
            return System.Text.Encoding.ASCII.GetString(bb);
            // or System.Text.Encoding.UTF7.GetString
            // or System.Text.Encoding.UTF8.GetString
            // or System.Text.Encoding.Unicode.GetString
            // or etc.
        }

        public static byte[] HexStringToByteHex(string strHex)
        {
            byte[] bytes = new byte[strHex.Length / 2];

            for (int count = 0; count < strHex.Length; count += 2)
            {
                bytes[count / 2] = System.Convert.ToByte(strHex.Substring(count, 2), 16);
            }
            return bytes;
        }
        public static string ByteHexToHexString(byte[] hex)
        {
            string result = string.Empty;
            foreach (byte c in hex)
                result += c.ToString("x2").ToUpper();
            return result;
        }
        public static string byteArrayToHex_Without0x(byte[] ba)
        {
            StringBuilder sb = new StringBuilder(ba.Length * 2);
            int count = 0;
            foreach (byte b in ba)
            {
                sb.Append(count);
                sb.Append(":");
                sb.AppendFormat("{0:x2}", b);
                sb.Append("/");
                count++;
            }
            return sb.ToString();
        }

        public static string addString(string origin, string addString, int totalLength, bool isAddFront)
        {
            int length_gap = totalLength - origin.Length;

            //원래 길이가 작으면 붙인다
            if (length_gap > 0)
                for (int i = 0; i < length_gap; i++)
                    if (isAddFront)
                        origin = addString + origin;
                    else
                        origin = origin + addString;
            //원래 길이가 크면 줄인다
            else if (length_gap < 0)
                origin = origin.Substring(0, totalLength);
            return origin;
        }

        public static string asciiByteArrayToString(byte[] array)
        {

            //for (int i = 0; i < array.Length; i++)
            //    if (array[i] == 0)
            //    {
            //        return "";
            //    }

            return ASCIIEncoding.ASCII.GetString(array);
        }
        public static string ByteArrayToString(byte[] array)
        {

            //for (int i = 0; i < array.Length; i++)
            //    if (array[i] == 0)
            //    {
            //        return "";
            //    }
            return Encoding.Default.GetString(array);
        }

        public static string ushortArrayToString(ushort[] array)
        {
            if (array == null)
            {
                return "null";
            }
            Byte[] bytes = new Byte[array.Length];  // Assumption - only need one byte per ushort

            int i = 0;
            foreach (ushort x in array)
            {
                byte[] tmp = System.BitConverter.GetBytes(x);
                bytes[i++] = tmp[0];
                // Note: not using tmp[1] as all characters in 0 < x < 127 use one byte.
            }

            return Encoding.ASCII.GetString(bytes);
        }
        public static string makeStringLength(string str, int length, bool isFront)
        {
            string returnString = "";
            if (str.Length > length)
                return returnString = str.Substring(0, length);
            else if (str.Length < length)
            {
                int remain = length - str.Length;
                for (int i = 0; i < remain; i++)
                    if (isFront) str = "" + str;
                    else str = str + "";

                return returnString;
            }
            return str;
        }

        public static byte[] stringToAsciiByteArray(string str) => ASCIIEncoding.ASCII.GetBytes(str);
        public static byte[] stringToByteArray(string str)
        {
            return Encoding.Default.GetBytes(str);
        }

        public static byte getByte(int data) => (byte)(data & 0x000000ff);
        public static byte getByte(string data) => (byte)(EL_Manager_Conversion.getInt(data) & 0x000000ff);

        public static byte[] intTo2ByteArray(int value)
        {
            byte[] data = new byte[2];
            data[0] = (byte)((value >> 8) & 0x000000ff);
            data[1] = (byte)((value) & 0x000000ff);
            return data;
        }

        public static byte[] combine(params byte[][] combineData)
        {
            int length = 0;
            foreach (byte[] elem in combineData)
            {
                if (elem != null && elem.Length > 0)
                    length += elem.Length;
            }
            int count = 0;
            byte[] total = new byte[length];
            foreach (byte[] elem in combineData)
            {
                if (elem != null && elem.Length > 0)
                    for (int i = 0; i < elem.Length; i++)
                    {
                        total[count] = elem[i];
                        count++;
                    }
            }

            return total;
        }

        public static bool getFlagByByteArray(byte data, int flagArrayIndex)
        {
            bool result = false;
            int flagData = (int)0x00000080;
            if (flagArrayIndex != 0) { flagData = flagData >> flagArrayIndex; }
            if (((data & 0x000000ff) & flagData) != 0)
                result = true;

            return result;
        }

        public static bool getFlagByByteArray(byte[] data, int startArrayIndex, int flagArrayIndex)
        {
            bool result = false;
            int flagData = (int)0x00000080;
            if (flagArrayIndex != 0) { flagData = flagData >> flagArrayIndex; }
            if (((data[startArrayIndex] & 0x000000ff) & flagData) != 0)
                result = true;

            return result;
        }

        public static byte[] getByteArrayByLong(long data, int Length)
        {
            byte[] array = null;
            switch (Length)
            {
                case 1:
                    array = new byte[1];
                    array[0] = (byte)(0x000000ff & data);
                    break;
                case 2:
                    array = new byte[2];
                    array[0] = (byte)(0x000000ff & (data >> 8));
                    array[1] = (byte)(0x000000ff & (data));
                    break;
                case 3:
                    array = new byte[3];
                    array[0] = (byte)(0x000000ff & (data >> 16));
                    array[1] = (byte)(0x000000ff & (data >> 8));
                    array[2] = (byte)(0x000000ff & (data));
                    break;
                case 4:
                    array = new byte[4];
                    array[0] = (byte)(0x000000ff & (data >> 24));
                    array[1] = (byte)(0x000000ff & (data >> 16));
                    array[2] = (byte)(0x000000ff & (data >> 8));
                    array[3] = (byte)(0x000000ff & (data));
                    break;
                default:
                    return null;
            }

            return array;
        }

        public static byte[] getByteArrayByInt(int data, int Length)
        {
            byte[] array = null;
            switch (Length)
            {
                case 1:
                    array = new byte[1];
                    array[0] = (byte)(0x000000ff & data);
                    break;
                case 2:
                    array = new byte[2];
                    array[0] = (byte)(0x000000ff & (data >> 8));
                    array[1] = (byte)(0x000000ff & (data));
                    break;
                case 3:
                    array = new byte[3];
                    array[0] = (byte)(0x000000ff & (data >> 16));
                    array[1] = (byte)(0x000000ff & (data >> 8));
                    array[2] = (byte)(0x000000ff & (data));
                    break;
                case 4:
                    array = new byte[4];
                    array[0] = (byte)(0x000000ff & (data >> 24));
                    array[1] = (byte)(0x000000ff & (data >> 16));
                    array[2] = (byte)(0x000000ff & (data >> 8));
                    array[3] = (byte)(0x000000ff & (data));
                    break;
                default:
                    return null;
            }

            return array;
        }

        public static int getIntByByteArray(byte[] array)
        {
            int returnValue = 0;
            switch (array.Length)
            {
                case 0:
                    break;
                case 1:
                    returnValue =
                    array[0];
                    break;
                case 2:
                    returnValue =
                    array[0] << 8 |
                    array[1];
                    break;
                case 3:
                    returnValue = array[0] << 16 |
                    array[1] << 8 |
                    array[2];
                    break;
                case 4:
                    returnValue = array[0] << 24 |
                    array[1] << 16 |
                    array[2] << 8 |
                    array[3];
                    break;
            }

            return returnValue;
        }

        public static string ByteArrayToHexString(byte[] ba) => BitConverter.ToString(ba).Replace("-", "");
        public static int getInt_2Byte(byte[] data) => (data[0] << 8 | data[1]) & 0x0000ffff;

        public static int getInt_2Byte(byte data1, byte data2) => (data1 << 8 | data2) & 0x0000ffff;
        public static int getInt(byte data) => data & 0x000000ff;
        public static int getInt(string data)
        {
            string temp = data.Replace(",", "").Replace(" ", "");//Replace(String oldValue, String NewValue) -> data의 ","를 ""로 대체한다.
            if (data == "") return 0;
            try
            { // string에 "" 가 들어오는 경우가 많아서 있는 예외처리로 보임
                return Int32.Parse(temp);
            }
            catch (Exception ex)
            {
                //ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
                //logger.Fatal("<<< catch : " + ex.ToString());
                return 0;
            }
        }

        private static readonly uint[] _lookup32 = CreateLookup32();
        private static uint[] CreateLookup32()
        {
            var result = new uint[256];
            for (int i = 0; i < 256; i++)
            {
                string s = i.ToString("X2");
                result[i] = ((uint)s[0]) + ((uint)s[1] << 16);
            }
            return result;
        }
        public static string ByteArrayToHexViaLookup32(byte[] bytes)
        {
            var lookup32 = _lookup32;
            var result = new char[bytes.Length * 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                var val = lookup32[bytes[i]];
                result[2 * i] = (char)val;
                result[2 * i + 1] = (char)(val >> 16);
            }
            return new string(result);
        }

        public static string bcd2Str(byte[] bytes)
        {
            StringBuilder temp = new StringBuilder(bytes.Length * 2);

            for (int i = 0; i < bytes.Length; i++)
            {
                temp.Append((byte)((bytes[i] & 0xf0) >> 4));
                temp.Append((byte)(bytes[i] & 0x0f));
            }

            return temp.ToString().Substring(0, 1).Equals("0") ? temp.ToString().Substring(1) : temp.ToString();
        }

        public static string ConvertToBinaryCodedDecimal(bool isLittleEndian, params byte[] bytes)
        {
            StringBuilder bcd = new StringBuilder(bytes.Length * 2);

            if (isLittleEndian)
                for (int i = bytes.Length - 1; i >= 0; i--)
                {
                    byte bcdByte = bytes[i];
                    int idHigh = bcdByte >> 4;
                    int idLow = bcdByte & 0x0F;
                    if (idHigh > 9 || idLow > 9)
                        throw new ArgumentException(
                            string.Format("One of the argument bytes was not in binary-coded decimal format: byte[{0}] = 0x{1:X2}.", i, bcdByte));

                    bcd.Append(string.Format("{0}{1}", idHigh, idLow));

                }
            else
                for (int i = 0; i < bytes.Length; i++)
                {
                    byte bcdByte = bytes[i];
                    int idHigh = bcdByte >> 4;
                    int idLow = bcdByte & 0x0F;
                    if (idHigh > 9 || idLow > 9)
                        throw new ArgumentException(
                            string.Format("One of the argument bytes was not in binary-coded decimal format: byte[{0}] = 0x{1:X2}.", i, bcdByte));

                    bcd.Append(string.Format("{0}{1}", idHigh, idLow));
                }
            return bcd.ToString();
        }

        /// <summary>
        /// 날짜 문자열이 YYYYMMDD 형식인지 검사한다. (YYYY는 1999부터 2099 까지가 유효한것으로 판단함)
        /// </summary>
        /// <param date="YYYYMMDD or YYYY-MM-DD"></param>
        /// <returns></returns>
        //public static bool isDate(string date)
        //{
        //    bool result = false;
        //    result = Regex.IsMatch(date, @"^(19|20)\d{2}(0[1-9]|1[012])(0[1-9]|[12][0-9]|3[0-1])$");
        //    if (!result) result = Regex.IsMatch(date, @"^(19|20)\d{2}-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[0-1])$");

        //    return result;
        //}

        /// <summary>
        /// 시간 문자열이 HHMMSS 형식인지 검사한다.
        /// </summary>
        /// <param time="HHMMSS"></param>
        /// <returns></returns>
        public static bool isTime(string input)
        {
            TimeSpan dummyOutput;
            return TimeSpan.TryParse(input, out dummyOutput);
        }
    }
}
