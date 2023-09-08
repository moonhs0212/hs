using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EL_DC_Charger.common.Manager
{
    public class EL_Manager_Application
    {
        public static void restartApplication_ConfirmPopup()
        {
            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("프로그램을 재시작 하시겠습니까?", "경고", MessageBoxButtons.YesNo);

            if (dialog == DialogResult.Yes)
            {
                try
                {
                    Process.Start(Application.StartupPath + "\\EL_DC_Charger.exe");
                    Process.GetCurrentProcess().Kill();
                }
                catch { }
            }
        }

        public static void restartApplication()
        {
            try
            {
                Process.Start(Application.StartupPath + "\\EL_DC_Charger.exe");
                Process.GetCurrentProcess().Kill();
            }
            catch { }
        }

        public static void restartSystem_ConfirmPopup()
        {
            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("시스템을 재시작하시겠습니까?", "경고", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
                restartSystem(); //to restart
        }

        public static void restartSystem() => Process.Start("ShutDown", "-r -t 1");

        public static void finishApplication_ConfirmPopup()
        {
            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("프로그램을 종료하시겠습니까?", "경고", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
                Environment.Exit(1);
        }

        public static void finishApplication() => Environment.Exit(1);
    }
}
