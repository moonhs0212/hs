using EL_DC_Charger.BatteryChange_Charger.Settings;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.LogSearch;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.setting
{
    public partial class UC_Setting_Main : UserControl
    {
        public UC_Setting_Main()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
        }

        private void button_setting_comm_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().getDataManager_CustomUC_Main().Form_Setting.setContent_CommDeviceSetting();
        }

        private void button_setting_station_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().getDataManager_CustomUC_Main().Form_Setting.setContent_Station_Setting();
        }

        private void button_program_finish_Click(object sender, EventArgs e)
        {
            Form_Confirm_Program_Reset form = new Form_Confirm_Program_Reset();
            form.ShowDialog();
        }

        private void button_setting_socket_Click(object sender, EventArgs e)
        {

        }


        private void button_doorsetting_Click(object sender, EventArgs e)
        {

        }

        private void button_view_charginglist_Click(object sender, EventArgs e)
        {
            frmHistory _frmHistory = new frmHistory();
            _frmHistory.Show();
        }

        private void button_charging_testmode_Click(object sender, EventArgs e)
        {
            if (EL_DC_Charger_MyApplication.getInstance().calibrationMode)
            {
                Form_Manual_Setting frmManualSetting = new Form_Manual_Setting();
                frmManualSetting.Show();
            }
            else
                EL_DC_Charger_MyApplication.getInstance().getDataManager_CustomUC_Main().Form_Setting.setContent_TestMode_Charging();
        }

        private void button_setupforengineer_Click(object sender, EventArgs e)
        {

        }

        private void button_csms_infor_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().getDataManager_CustomUC_Main().Form_Setting.setContent_CSMS_Infor();
        }
    }
}
