using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EL_DC_Charger.Utils
{
    public static class CsUtil
    {
        public static void WriteLog(string LogMessage, string FolderName = "Log")
        {
            try
            {
                string LogDate;

                string LogContent;

                LogDate = DateTime.Now.ToString("yyyyMMdd");

                LogContent = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + "] " + LogMessage;

                if (!Directory.Exists(Application.StartupPath + "\\" + FolderName + "\\" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0')))
                {
                    Directory.CreateDirectory(Application.StartupPath + "\\" + FolderName + "\\" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0'));
                }

                if (!File.Exists(Application.StartupPath + "\\" + FolderName + "\\" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + @"\" + LogDate + ".Log"))
                {
                    using (StreamWriter stream = File.CreateText(Application.StartupPath + "\\" + FolderName + "\\" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + @"\" + LogDate + ".Log"))
                    {
                        stream.WriteLine(LogContent);
                    }
                }
                else
                {
                    using (StreamWriter stream = File.AppendText(Application.StartupPath + "\\" + FolderName + "\\" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + @"\" + LogDate + ".Log"))
                    {
                        stream.WriteLine(LogContent);
                    }
                }

            }
            catch (Exception ex)
            {
                WriteLog("로그저장에러 - " + ex.Message);
            }
        }
    }
}
