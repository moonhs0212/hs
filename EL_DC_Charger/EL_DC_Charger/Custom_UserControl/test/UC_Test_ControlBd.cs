using EL_DC_Charger.BatteryChange_Charger.SerialPorts.IOBoard;
using EL_DC_Charger.EL_DC_Charger.Applications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.test
{
    public partial class UC_Test_ControlBd : UserControl
    {
        public UC_Test_ControlBd()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
        }

        protected int mChannelIndex = 1;
        public UC_Test_ControlBd(int channelIndex)
        {
            InitializeComponent();
            mChannelIndex = channelIndex;
        }

        EL_DC_Charger_MyApplication getMyApplication()
        {
            return EL_DC_Charger_MyApplication.getInstance();
        }

        private void cb_card_reading_complete_CheckedChanged(object sender, EventArgs e)
        {
            //getMyApplication().Controller_Main.bIsReceive_CardNumber = true;
        }

        private void cb_emg_CheckedChanged(object sender, EventArgs e)
        {
            ((EL_ControlbdComm_PacketManager)getMyApplication().getChannelTotalInfor(mChannelIndex).getControlbdComm_PacketManager()).IsPush_Emg = cb_emg.Checked;
        }

        private void UC_Test_ControlBd_Load(object sender, EventArgs e)
        {

        }

        private void cb_gun_connect_CheckedChanged(object sender, EventArgs e)
        {
            ((EL_ControlbdComm_PacketManager)getMyApplication().getChannelTotalInfor(mChannelIndex).getControlbdComm_PacketManager()).IsConnectedGun_Combo_Type1 = cb_gun_connect.Checked;
        }

        private void cb_charging_CheckedChanged(object sender, EventArgs e)
        {
            if(cb_charging.Checked)
            {
                ((EL_ControlbdComm_PacketManager)getMyApplication().getChannelTotalInfor(mChannelIndex).getControlbdComm_PacketManager()).packet_1z.mChargingProcessState = 100;
            }
            else
            {
                ((EL_ControlbdComm_PacketManager)getMyApplication().getChannelTotalInfor(mChannelIndex).getControlbdComm_PacketManager()).packet_1z.mChargingProcessState = 0;
            }
            
        }
    }
}
