using EL_DC_Charger.BatteryChange_Charger.SerialPorts.IOBoard;
using EL_DC_Charger.common.ChargerVariable;
using EL_DC_Charger.common.interf;
using EL_DC_Charger.common.Manager;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.ChargerVariable;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.keypad;
using EL_DC_Charger.EL_DC_Charger.SerialPorts.ControlBoard.Packet.Child;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.setting
{
    public partial class UC_Setting_Manual_Each : UserControl, IKeypad_EventListener
    {
        Button[] mButton_Increase = new Button[2];

        static int DEFAULT_VOLTAGE = 400;
        static int DEFAULT_CURRENT = 30;
        int mSettingVoltage = DEFAULT_VOLTAGE;
        int mSettingCurrent = DEFAULT_CURRENT;

        int mChanelIdx = 0;
        Form_Manual_Setting mForm = null;
        public UC_Setting_Manual_Each(Form_Manual_Setting form, int _mChanelIdx)
        {
            mForm = form;
            mChanelIdx = _mChanelIdx;
            InitializeComponent();

            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChanelIdx).mChargingTime.onChargingStop();
            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChanelIdx).mChargingWattage.setChargignStop();

            mButton_Increase[0] = button_command_voltage;
            mButton_Increase[1] = button_command_current;

            DEFAULT_VOLTAGE = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).getSettingData_Int(CONST_INDEX_MAINSETTING.MANUALTEST_OUTPUT_VOLTAGE);
            DEFAULT_CURRENT = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).getSettingData_Int(CONST_INDEX_MAINSETTING.MANUALTEST_OUTPUT_CURRENT);
            mSettingVoltage = DEFAULT_VOLTAGE;
            mSettingCurrent = DEFAULT_CURRENT;
            button_command_voltage.Text = "" + mSettingVoltage;
            button_command_current.Text = "" + mSettingCurrent;
        }

        private void button_command_voltage_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.setVariable(sender, Wev_Keypad_Type.NUMBER, 1000, 10,
                "최대출력전압을 입력해 주세요.", "" + mSettingVoltage,
                "V");
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.setKeypad_EventListener(this);
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.Show();
        }

        private void button_command_current_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.setVariable(sender, Wev_Keypad_Type.NUMBER, 500, 1,
                "최대출력전류을 입력해 주세요.", "" + "" + mSettingCurrent,
                "A");
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.setKeypad_EventListener(this);
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.Show();
        }

        public void onEnterComplete(object obj, int type, string title, string content, string afterContent)
        {
            if (obj.Equals(button_command_voltage))
            {
                if (mForm.mCharge_CH_Type == ECharge_CH_Type.ALL)
                {
                    if ((mSettingCurrent * EL_Manager_Conversion.getInt(afterContent)) > (EL_DC_Charger_MyApplication.getInstance().getChargerPower_kW() * 1000 / 2))
                        return;
                }
                else
                {
                    if ((mSettingCurrent * EL_Manager_Conversion.getInt(afterContent)) > (EL_DC_Charger_MyApplication.getInstance().getChargerPower_kW() * 1000))
                        return;
                }

                EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).setSettingData(CONST_INDEX_MAINSETTING.MANUALTEST_OUTPUT_VOLTAGE, afterContent);
                button_command_voltage.Text = afterContent;
                mSettingVoltage = EL_Manager_Conversion.getInt(afterContent);
                ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChanelIdx).getControlbdComm_PacketManager())
                .packet_z1.mCommand_Output_Voltage = mSettingVoltage * 10;
            }
            else if (obj.Equals(button_command_current))
            {
                if (mForm.mCharge_CH_Type == ECharge_CH_Type.ALL)
                {
                    if ((mSettingVoltage * EL_Manager_Conversion.getInt(afterContent)) > (EL_DC_Charger_MyApplication.getInstance().getChargerPower_kW() * 1000 / 2))
                        return;
                }
                else
                {
                    if ((mSettingVoltage * EL_Manager_Conversion.getInt(afterContent)) > (EL_DC_Charger_MyApplication.getInstance().getChargerPower_kW() * 1000))
                        return;
                }
                EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).setSettingData(CONST_INDEX_MAINSETTING.MANUALTEST_OUTPUT_CURRENT, afterContent);
                button_command_current.Text = afterContent;
                mSettingCurrent = EL_Manager_Conversion.getInt(afterContent);
                ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChanelIdx).getControlbdComm_PacketManager())
                .packet_z1.mCommand_Output_Current = mSettingCurrent * 10;
            }
        }

        public void setOutputInfor(int voltage, int current)
        {
            button_command_current.Text = "" + current;
            mSettingCurrent = current;
            ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChanelIdx).getControlbdComm_PacketManager())
            .packet_z1.mCommand_Output_Current = mSettingCurrent * 10;

            button_command_voltage.Text = "" + voltage;
            mSettingVoltage = voltage;
            ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChanelIdx).getControlbdComm_PacketManager())
            .packet_z1.mCommand_Output_Voltage = mSettingVoltage * 10;
        }

        public void onCancel(object obj, int type, string title, string content)
        {
        }

        private void button_start_output_Click(object sender, EventArgs e)
        {
            button_start_output.Enabled = false;
            button_start_charging.Enabled = false;
            button_stop.Enabled = true;

            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChanelIdx).mChargingTime.onChargingStart();
            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChanelIdx).mChargingWattage.setChargingStart();

            label_total_active_evergy_start.Text = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChanelIdx).mChargingWattage.getChargingWattage_Start_String();
            label_total_active_evergy_finish.Text = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChanelIdx).mChargingWattage.getCurrentWattage_String();
            label_total_active_evergy_charging.Text = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChanelIdx).mChargingWattage.getChargingWattage_String();

            label_state_outputcommand.Text = "출력요청중";
            ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChanelIdx).getControlbdComm_PacketManager())
                .packet_z1.mCommand_Output_Channel1 = 2;

            for (int i = 0; i < mButton_Increase.Length; i++)
            {
                mButton_Increase[i].Enabled = false;
            }
        }

        private void button_start_charging_Click(object sender, EventArgs e)
        {
            button_start_output.Enabled = false;
            button_start_charging.Enabled = false;
            button_stop.Enabled = true;

            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChanelIdx).mChargingTime.onChargingStart();
            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChanelIdx).mChargingWattage.setChargingStart();

            label_total_active_evergy_start.Text = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChanelIdx).mChargingWattage.getChargingWattage_Start_String();
            label_total_active_evergy_finish.Text = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChanelIdx).mChargingWattage.getCurrentWattage_String();
            label_total_active_evergy_charging.Text = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChanelIdx).mChargingWattage.getChargingWattage_String();


            label_state_outputcommand.Text = "충전요청중";
            ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChanelIdx).getControlbdComm_PacketManager())
                .packet_z1.mCommand_Output_Channel1 = 1;

            ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChanelIdx).getControlbdComm_PacketManager())
                .packet_z1.bPowerRelay_Plus = true;
            cb_do_relay_plus.Checked = true;

            ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChanelIdx).getControlbdComm_PacketManager())
                .packet_z1.bPowerRelay_Minus = true;
            cb_do_relay_minus.Checked = true;

            for (int i = 1; i <= EL_DC_Charger_MyApplication.getInstance().getChannelCount(); i++)
            {
                ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i).getControlbdComm_PacketManager())
                .packet_z1.bMC_On = true;
            }

            for (int i = 0; i < mButton_Increase.Length; i++)
            {
                mButton_Increase[i].Enabled = false;
            }
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            button_start_output.Enabled = true;
            button_start_charging.Enabled = true;
            button_stop.Enabled = false;

            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChanelIdx).mChargingTime.onChargingStop();
            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChanelIdx).mChargingWattage.setChargignStop();
            label_total_active_evergy_finish.Text = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChanelIdx).mChargingWattage.getChargingWattage_Stop_String();
            label_total_active_evergy_charging.Text = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChanelIdx).mChargingWattage.getChargingWattage_String();

            label_state_outputcommand.Text = "종료요청중";
            ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChanelIdx).getControlbdComm_PacketManager())
                .packet_z1.mCommand_Output_Channel1 = 0;
            for (int i = 0; i < mButton_Increase.Length; i++)
            {
                mButton_Increase[i].Enabled = true;
            }
        }

        private void cb_do_relay_plus_CheckedChanged(object sender, EventArgs e)
        {
            ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChanelIdx).getControlbdComm_PacketManager())
                .packet_z1.bPowerRelay_Plus = ((CheckBox)sender).Checked;
        }

        private void cb_do_relay_minus_CheckedChanged(object sender, EventArgs e)
        {
            ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChanelIdx).getControlbdComm_PacketManager())
                .packet_z1.bPowerRelay_Minus = ((CheckBox)sender).Checked;
        }

        private void cb_SUM_relay_plus_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 1; i <= EL_DC_Charger_MyApplication.getInstance().getChannelCount(); i++)
            {
                ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i).getControlbdComm_PacketManager())
                .packet_z1.bSumRelay_Plus = ((CheckBox)sender).Checked;
            }
        }

        private void cb_SUM_relay_minus_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 1; i <= EL_DC_Charger_MyApplication.getInstance().getChannelCount(); i++)
            {
                ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i).getControlbdComm_PacketManager())
                .packet_z1.bSumRelay_Minus = ((CheckBox)sender).Checked;
            }
        }
    }
}
