using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.INI
{
    public static class CsINIManager
    {
        #region INI 읽기/쓰기

        // ==========ini 파일 의 읽고 쓰기를 위한 API 함수 선언 =================
        [DllImport("kernel32")]
        static extern int GetPrivateProfileString(string Section, int Key,
              string Value, [MarshalAs(UnmanagedType.LPArray)] byte[] Result,
              int Size, string FileName);

        // Third Method
        [DllImport("kernel32")]
        static extern int GetPrivateProfileString(int Section, string Key,
               string Value, [MarshalAs(UnmanagedType.LPArray)] byte[] Result,
               int Size, string FileName);

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(    // ini Read 함수
                    String section,
                    String key,
                     String def,
                    StringBuilder retVal,
                    int size,
                    String filePath);

        [DllImport("kernel32.dll")]
        private static extern long WritePrivateProfileString(  // ini Write 함수
                    String section,
                    String key,
                    String val,
                    String filePath);
        //==========================================================================
        public static int CompareSec(DateTime before, DateTime current)
        {
            TimeSpan datediff = current - before;
            return datediff.Seconds;
        }

        public static string[] IniReadSectionNames(string iniPath)
        {
            for (int maxsize = 500; true; maxsize *= 2)
            {
                byte[] bytes = new byte[maxsize];
                int size = GetPrivateProfileString(0, "", "", bytes, maxsize, iniPath);

                if (size < maxsize - 2)
                {
                    string Selected = Encoding.ASCII.GetString(bytes, 0,
                                               size - (size > 0 ? 1 : 0));
                    return Selected.Split(new char[] { '\0' });
                }
            }
        }

        public static string[] IniReadEntryNames(string iniPath, string section)
        {
            for (int maxsize = 500; true; maxsize *= 2)
            {
                byte[] bytes = new byte[maxsize];
                int size = GetPrivateProfileString(section, 0, "", bytes, maxsize, iniPath);

                if (size < maxsize - 2)
                {
                    string entries = Encoding.ASCII.GetString(bytes, 0,
                                              size - (size > 0 ? 1 : 0));
                    return entries.Split(new char[] { '\0' });
                }
            }
        }

        // ini파일에 쓰기
        public static void IniWriteValue(string iniPath, string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, iniPath);
        }
        public static void IniWriteValue(string iniPath, string Section, string Key, int Value)
        {
            WritePrivateProfileString(Section, Key, Value.ToString(), iniPath);
        }
        public static void IniWriteValue(string iniPath, string Section, string Key, double Value)
        {
            WritePrivateProfileString(Section, Key, Value.ToString(), iniPath);
        }
        public static void IniWriteValue(string iniPath, string Section, string Key, bool Value)
        {
            WritePrivateProfileString(Section, Key, Value ? "1" : "0", iniPath);
        }

        // ini파일에서 읽어 오기
        public static string IniReadValue(string iniPath, string Section, string Key)
        {

            StringBuilder temp = new StringBuilder(2000);

            int i = GetPrivateProfileString(Section, Key, "", temp, 2000, iniPath);

            return temp.ToString();
        }
        public static string IniReadValue(string iniPath, string Section, string Key, string defaltValue)
        {

            StringBuilder temp = new StringBuilder(2000);

            int i = GetPrivateProfileString(Section, Key, "", temp, 2000, iniPath);

            if (temp.ToString() == "")
                return defaltValue;

            return temp.ToString();
        }
        #endregion
    }
}
