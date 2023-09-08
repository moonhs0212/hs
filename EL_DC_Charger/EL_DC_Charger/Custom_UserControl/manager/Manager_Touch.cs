using EL_DC_Charger.common.application;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.statemanager;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.Control;

namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.manager
{
    //화면 밝기 조정하기위해만듬 2023-08-12 //moon
    public class Manager_Touch
    {

        public Manager_Touch(UserControl con)
        {
            if (!EL_DC_Charger_MyApplication.getInstance().TouchManagerInit)
            {
                EL_DC_Charger_MyApplication.getInstance().TouchManagerInit = true;
                EL_DC_Charger_MyApplication.getInstance().Touch_dt = DateTime.Now;
                MonitorBirghtness(100);
            }

            if (con != null)
            {
                EnumerateChildren(con);
            }
        }
        private void EnumerateChildren(Control root)
        {
            foreach (Control control in root.Controls)
            {
                if (control.Controls != null)
                {
                    control.Click += Control_Click1;
                    EnumerateChildren(control);
                }
            }
        }

        private void Control_Click1(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().Touch_dt = DateTime.Now;
            SetBrightness((byte)100);
            //ineedSetBrightness(100);

        }

        protected Thread mThread = null;
        protected bool initBirghtness = false;
        protected bool needMiniumBirghtness = false;
        protected bool needMaxinumBirghtness = false;
        protected bool needRecoveryBrightness = false;
        protected bool needSetBrightness = false;
        protected int setBrightness = 80;
        protected int reverseBirghtness = 80;
        protected int baseBrightness = 80;
        public void bneedMiniumBirghtness() => needMiniumBirghtness = true;
        public void bneedRecoveryBrightness() => needRecoveryBrightness = true;

        public void ineedSetBrightness(int i)
        {
            needSetBrightness = true;
            setBrightness = i;
        }
        public void MonitorBirghtness(int _baseBrightness)
        {
            mThread = new Thread(run);
            mThread.Start();
            baseBrightness = _baseBrightness;
        }
        protected void run()
        {
            while (true)
            {
                try
                {
                    if (!initBirghtness)
                    {
                        initBirghtness = true;
                        SetBrightness((byte)baseBrightness);
                    }

                    //if (needSetBrightness)
                    //{
                    //    needSetBrightness = false;
                    //    SetBrightness((byte)setBrightness);
                    //}

                    //if (needMiniumBirghtness)
                    //{
                    //    needMiniumBirghtness = false;
                    //    reverseBirghtness = baseBrightness;
                    //    SetBrightness(0x01);
                    //}

                    //if (needRecoveryBrightness)
                    //{
                    //    needRecoveryBrightness = false;
                    //    SetBrightness((byte)reverseBirghtness);
                    //}

                    //5분                    
                    if (EL_DC_Charger_MyApplication.getInstance().Touch_dt.AddMinutes(5) <= DateTime.Now)
                    {

                        //EL_DC_Charger_MyApplication.getInstance().Touch_dt = DateTime.Now.AddHours(10);
                        //SetBrightness((byte)30);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Thread.Sleep(1000);
            }
        }

        //get the actual percentage of brightness
        public static int GetCurrentBrightness()
        {
            //define scope (namespace)
            System.Management.ManagementScope s = new System.Management.ManagementScope("root\\WMI");
            //define query
            System.Management.SelectQuery q = new System.Management.SelectQuery("WmiMonitorBrightness");
            //output current brightness
            System.Management.ManagementObjectSearcher mos = new System.Management.ManagementObjectSearcher(s, q);
            System.Management.ManagementObjectCollection moc = mos.Get();
            //store result
            byte curBrightness = 0;
            foreach (System.Management.ManagementObject o in moc)
            {
                curBrightness = (byte)o.GetPropertyValue("CurrentBrightness");
                break; //only work on the first object
            }
            moc.Dispose();
            mos.Dispose();
            return (int)curBrightness;
        }

        //array of valid brightness values in percent
        public static byte[] GetBrightnessLevels()
        {
            //define scope (namespace)
            System.Management.ManagementScope s = new System.Management.ManagementScope("root\\WMI");
            //define query
            System.Management.SelectQuery q = new System.Management.SelectQuery("WmiMonitorBrightness");
            //output current brightness
            System.Management.ManagementObjectSearcher mos = new System.Management.ManagementObjectSearcher(s, q);
            byte[] BrightnessLevels = new byte[0];

            System.Management.ManagementObjectCollection moc = mos.Get();
            //store result
            foreach (System.Management.ManagementObject o in moc)
            {
                BrightnessLevels = (byte[])o.GetPropertyValue("Level");
                break; //only work on the first object
            }
            moc.Dispose();
            mos.Dispose();

            return BrightnessLevels;
        }

        protected static void SetBrightness(byte targetBrightness)
        {
            //define scope (namespace)
            System.Management.ManagementScope s = new System.Management.ManagementScope("root\\WMI");
            //define query
            System.Management.SelectQuery q = new System.Management.SelectQuery("WmiMonitorBrightnessMethods");
            //output current brightness
            System.Management.ManagementObjectSearcher mos = new System.Management.ManagementObjectSearcher(s, q);
            System.Management.ManagementObjectCollection moc = mos.Get();
            foreach (System.Management.ManagementObject o in moc)
            {
                o.InvokeMethod("WmiSetBrightness", new object[] { uint.MaxValue, targetBrightness });
                //note the reversed order - won't work otherwise!
                break; //only work on the first object
            }

            moc.Dispose();
            mos.Dispose();
        }
    }
}
