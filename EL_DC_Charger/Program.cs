using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.LogSearch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EL_DC_Charger
{
    static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Thread.Sleep(1000);
            Process[] processes = null;
            processes = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
            if (processes.Length > 1)
            {
                MessageBox.Show(string.Format("'{0}' 프로그램이 이미 실행 중입니다.", Process.GetCurrentProcess().ProcessName));
                return;
            }
            ProcessCall(Process.GetCurrentProcess().ProcessName);
            CurPath = Application.StartupPath;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }


        public static string CurPath = string.Empty;

        [DllImport("user32")]
        private static extern bool SetForegroundWindow(IntPtr handle);
        [DllImport("User32")]
        private static extern int ShowWindow(IntPtr hwnd, int nCmdShow);
        [DllImport("User32")]
        private static extern void BringWindowToTop(IntPtr hwnd);
        private static void ProcessCall(string processName)
        {
            foreach (Process process in Process.GetProcesses())
            {
                if (process.ProcessName == processName)
                {
                    ShowWindow(process.MainWindowHandle, 9);
                    BringWindowToTop(process.MainWindowHandle);
                    SetForegroundWindow(process.MainWindowHandle);
                }
            }
        }
    }
}
