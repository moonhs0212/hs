using EL_DC_Charger.BatteryChange_Charger.SerialPorts.IOBoard;
using EL_DC_Charger.Manager;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.ChargerVariable;
using EL_DC_Charger.EL_DC_Charger.SerialPorts.Iksung_RFReader;
using EL_DC_Charger.Interface_Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EL_DC_Charger.common.interf;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.keypad;
using EL_DC_Charger.common.application;
using EL_DC_Charger.EL_DC_Charger.SerialPorts.smartro_tl3500bs;
using EL_DC_Charger.common.Manager;
using EL_DC_Charger.common.interf.uiux;

namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl
{
    public partial class UC_MainPage_Include_Cert_ManualControl_Output : UserControl, IRFCardReader_EventListener, IKeypad_EventListener, IUC_Setting_Child
    {
        Button[] mButton_Increase = new Button[2];

        EL_DC_Charger_MyApplication mApplication = null;
        public UC_MainPage_Include_Cert_ManualControl_Output()
        {
            InitializeComponent();

            mApplication = EL_DC_Charger_MyApplication.getInstance();

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();


            timer_uiupdate.Start();
        }

        public void initVariable()
        {
            mButton_Increase[0] = button_command_voltage;
            mButton_Increase[1] = button_command_current;

            DEFAULT_VOLTAGE = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).
                getSettingData_Int(CONST_INDEX_MAINSETTING.MANUALTEST_OUTPUT_VOLTAGE);
            DEFAULT_CURRENT = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).
                getSettingData_Int(CONST_INDEX_MAINSETTING.MANUALTEST_OUTPUT_CURRENT);
            mSettingVoltage = DEFAULT_VOLTAGE;
            mSettingCurrent = DEFAULT_CURRENT;

            button_command_voltage.Text = "" + mSettingVoltage;
            button_command_current.Text = "" + mSettingCurrent;

            button_start_output.Enabled = true;
            button_start_charging.Enabled = true;
            button_stop.Enabled = false;

            for (int i = 0; i < EL_DC_Charger_MyApplication.getInstance().getChannelCount(); i++)
            {
                ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i + 1).getControlbdComm_PacketManager())
                .packet_z1.mCommand_Output_Voltage = mSettingVoltage * 10;
                ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i + 1).getControlbdComm_PacketManager())
                    .packet_z1.mCommand_Output_Current = mSettingCurrent * 10;
                ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i + 1).getControlbdComm_PacketManager())
                    .packet_z1.initVariable();
            }



            if (EL_DC_Charger_MyApplication.getInstance().SerialPort_Smartro_CardReader != null)
                EL_DC_Charger_MyApplication.getInstance().SerialPort_Smartro_CardReader.setRFCardReader_Listener(this);
        }


        private void button_start_output_Click(object sender, EventArgs e)
        {
            button_start_output.Enabled = false;
            button_start_charging.Enabled = false;
            button_stop.Enabled = true;

            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mChargingTime.onChargingStart();
            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mChargingWattage.setChargingStart();

            label_total_active_evergy_start.Text = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mChargingWattage.getChargingWattage_Start_String();
            label_total_active_evergy_finish.Text = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mChargingWattage.getCurrentWattage_String();
            label_total_active_evergy_charging.Text = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mChargingWattage.getChargingWattage_String();


            label_state_outputcommand.Text = "출력요청중";
            ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager())
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

            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mChargingTime.onChargingStart();
            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mChargingWattage.setChargingStart();

            label_total_active_evergy_start.Text = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mChargingWattage.getChargingWattage_Start_String();
            label_total_active_evergy_finish.Text = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mChargingWattage.getCurrentWattage_String();
            label_total_active_evergy_charging.Text = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mChargingWattage.getChargingWattage_String();


            label_state_outputcommand.Text = "충전요청중";
            ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager())
                .packet_z1.mCommand_Output_Channel1 = 1;

            ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager())
                .packet_z1.bPowerRelay_Plus = true;
            cb_do_relay_plus.Checked = true;

            ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager())
                .packet_z1.bPowerRelay_Minus = true;
            cb_do_relay_minus.Checked = true;

            ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager())
                .packet_z1.bMC_On = true;
            cb_do_mc.Checked = true;


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

            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mChargingTime.onChargingStop();
            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mChargingWattage.setChargignStop();
            label_total_active_evergy_finish.Text = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mChargingWattage.getChargingWattage_Stop_String();
            label_total_active_evergy_charging.Text = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mChargingWattage.getChargingWattage_String();

            label_state_outputcommand.Text = "종료요청중";
            ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager())
                .packet_z1.mCommand_Output_Channel1 = 0;
            for (int i = 0; i < mButton_Increase.Length; i++)
            {
                mButton_Increase[i].Enabled = true;
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        const int MAXIMUM_VOLTAGE = 1000;
        const int MAXIMUM_CURRENT = 100;
        static int DEFAULT_VOLTAGE = 400;
        static int DEFAULT_CURRENT = 30;
        int mSettingVoltage = DEFAULT_VOLTAGE;
        int mSettingCurrent = DEFAULT_CURRENT;

        public void increaseVoltage(int value)
        {
            if (value == 0)
            {
                mSettingVoltage = 0;

            }
            else
            {
                mSettingVoltage += value;
                if (mSettingVoltage > MAXIMUM_VOLTAGE)
                    mSettingVoltage = MAXIMUM_VOLTAGE;
                if (mSettingVoltage < 0)
                    mSettingVoltage = 0;
            }
            ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager())
                .packet_z1.mCommand_Output_Voltage = mSettingVoltage * 10;
            button_command_voltage.Text = "" + mSettingVoltage;

            EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).
                setSettingData(CONST_INDEX_MAINSETTING.MANUALTEST_OUTPUT_VOLTAGE, mSettingVoltage);


        }
        public void increaseCurrent(int value)
        {
            if (value == 0)
            {
                mSettingCurrent = 0;
            }
            else
            {
                mSettingCurrent += value;
                if (mSettingCurrent > MAXIMUM_CURRENT)
                    mSettingCurrent = MAXIMUM_CURRENT;
                if (mSettingCurrent < 0)
                    mSettingCurrent = 0;
            }
            ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager())
                .packet_z1.mCommand_Output_Current = mSettingCurrent * 10;

            button_command_current.Text = "" + mSettingCurrent;

            EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).
                setSettingData(CONST_INDEX_MAINSETTING.MANUALTEST_OUTPUT_CURRENT, mSettingCurrent);
        }

        private void button_rfid_start_Click(object sender, EventArgs e)
        {


            if (EL_DC_Charger_MyApplication.getInstance().RFCardReader_Manager != null && ((Smartro_TL3500BS_Comm_Manager)EL_DC_Charger_MyApplication.getInstance().RFCardReader_Manager).IsConnected_HW)
            {
                EL_DC_Charger_MyApplication.getInstance().RFCardReader_Manager.setRFCardReader_Listener(this);
                EL_DC_Charger_MyApplication.getInstance().RFCardReader_Manager.setCommand_Search_RFCard();
                label_cardnumber.Text = "카드리딩 대기중....";
            }
            else
            {
                label_cardnumber.Text = "카드단말기 연결 오류....";
            }

        }

        private void button_rfid_stop_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().RFCardReader_Manager.setRFCardReader_Listener(null);
            EL_DC_Charger_MyApplication.getInstance().RFCardReader_Manager.setCommand_Ready();
            label_cardnumber.Text = "동작 없음";
        }

        public void onReceive(string rfCardNumber)
        {

            EL_DC_Charger_MyApplication.getInstance().RFCardReader_Manager.setRFCardReader_Listener(null);
            SetTextBox(label_cardnumber, rfCardNumber);

        }

        public void onReceiveFailed(string result)
        {

        }
        public static void SetTextBox(Label tb, string contents)
        {
            //생성된 스레드가 아닌 다른 스레드에서 호출될 경우 true
            if (tb.InvokeRequired)
            {
                tb.Invoke(new MethodInvoker(delegate ()
                {
                    tb.Text = contents;
                }));
            }
            else
            {
                tb.Text = contents;
            }
        }
        private void UC_MainPage_Include_Cert_ManualControl_Output_Load(object sender, EventArgs e)
        {

        }

        private void timer_uiupdate_Tick(object sender, EventArgs e)
        {

            label_total_active_evergy_finish.Text = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mChargingWattage.getCurrentWattage_String();
            label_total_active_evergy_charging.Text = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mChargingWattage.getChargingWattage_String();

            label_chargingtime.Text = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mChargingTime.getChargingTime();
            label_chargingvoltage.Text = "" + EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getAMI_PacketManager().getVoltage();
            label_chargingcurrent.Text = "" + EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getAMI_PacketManager().getCurrent();

            label_chargingstate.Text = "" + ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager()).packet_1z.mChargingProcessState;
            label_errorcode.Text = "" + ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager()).packet_1z.mErrorCode;
            label_soc.Text = "" + ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager()).packet_1z.mSOC;
            label_remaintime.Text = "" + ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager()).packet_1z.mRemainTime_Minute;

            if (mApplication.DI_Manager != null)
            {
                cb_di_emg.Checked = mApplication.DI_Manager.isEmergencyPushed();

                if (mApplication.DI_Manager.isEmergencyPushed())
                {
                    cb_di_emg.Text = "ON";
                    cb_di_emg.ForeColor = Color.Red;
                }
                else
                {
                    cb_di_emg.Text = "OFF";
                    cb_di_emg.ForeColor = Color.Blue;
                }

            }

            cb_di_relay_plus.Checked = ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager()).packet_1z.bFlag1_PowerRelay_Plus;
            if (((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager()).packet_1z.bFlag1_PowerRelay_Plus)
            {
                cb_di_relay_plus.Text = "ON";
                cb_di_relay_plus.ForeColor = Color.Red;
            }
            else
            {
                cb_di_relay_plus.Text = "OFF";
                cb_di_relay_plus.ForeColor = Color.Blue;
            }
            cb_di_relay_minus.Checked = ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager()).packet_1z.bFlag1_PowerRelay_Minus;
            if (((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager()).packet_1z.bFlag1_PowerRelay_Minus)
            {
                cb_di_relay_minus.Text = "ON";
                cb_di_relay_minus.ForeColor = Color.Red;
            }
            else
            {
                cb_di_relay_minus.Text = "OFF";
                cb_di_relay_minus.ForeColor = Color.Blue;
            }
            int high = ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager()).packet_1z.
                mPWM1_High;
            if (high >= 110)
            {
                label_comstate_gun.Text = "커넥터분리";
            }
            else if (high >= 80)
            {
                label_comstate_gun.Text = "커넥터연결";
            }
            else if (high >= 50)
            {
                label_comstate_gun.Text = "충전요청중";
            }
            else
            {
                label_comstate_gun.Text = "알수없음(" + high + ")";
            }

            cb_comstate_ami.Checked = mApplication.getChannelTotalInfor(1).getAMI_PacketManager().isConnected();
            if (mApplication.getChannelTotalInfor(1).getAMI_PacketManager().isConnected())
            {
                cb_comstate_ami.Text = "ON";
                cb_comstate_ami.ForeColor = Color.Red;
            }
            else
            {
                cb_comstate_ami.Text = "OFF";
                cb_comstate_ami.ForeColor = Color.Blue;
            }


            cb_comstate_controlbd.Checked = mApplication.getChannelTotalInfor(1).getControlbdComm_PacketManager().isConnected();
            if (mApplication.getChannelTotalInfor(1).getControlbdComm_PacketManager().isConnected())
            {
                cb_comstate_controlbd.Text = "ON";
                cb_comstate_controlbd.ForeColor = Color.Red;
            }
            else
            {
                cb_comstate_controlbd.Text = "OFF";
                cb_comstate_controlbd.ForeColor = Color.Blue;
            }

            cb_comstate_powerpack.Checked = ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager()).packet_1z.bFlag1_Commstate_Powerpack_01;
            if (((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager()).packet_1z.bFlag1_Commstate_Powerpack_01)
            {
                cb_comstate_powerpack.Text = "ON";
                cb_comstate_powerpack.ForeColor = Color.Red;
            }
            else
            {
                cb_comstate_powerpack.Text = "OFF";
                cb_comstate_powerpack.ForeColor = Color.Blue;
            }



        }


        private void button_command_voltage_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.setVariable(sender, Wev_Keypad_Type.NUMBER, 1000, 10,
                "최대출력전압을 입력해 주세요.", "" + EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).getSettingData_Int(CONST_INDEX_MAINSETTING.MANUALTEST_OUTPUT_VOLTAGE),
                "V");
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.setKeypad_EventListener(this);
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.Show();
        }

        private void button_command_current_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.setVariable(sender, Wev_Keypad_Type.NUMBER, 500, 1,
                "최대출력전류을 입력해 주세요.", "" + EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).getSettingData_Int(CONST_INDEX_MAINSETTING.MANUALTEST_OUTPUT_CURRENT),
                "A");
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.setKeypad_EventListener(this);
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.Show();
        }

        //버튼 이벤트
        public void onEnterComplete(object obj, int type, string title, string content, string afterContent)
        {
            if (obj.Equals(button_command_voltage))
            {
                EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).setSettingData(CONST_INDEX_MAINSETTING.MANUALTEST_OUTPUT_VOLTAGE, afterContent);
                button_command_voltage.Text = afterContent;
                mSettingVoltage = EL_Manager_Conversion.getInt(afterContent);
                ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager())
                .packet_z1.mCommand_Output_Voltage = mSettingVoltage * 10;
            }
            else if (obj.Equals(button_command_current))
            {
                EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).setSettingData(CONST_INDEX_MAINSETTING.MANUALTEST_OUTPUT_CURRENT, afterContent);
                button_command_current.Text = afterContent;
                mSettingCurrent = EL_Manager_Conversion.getInt(afterContent);
                ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager())
                .packet_z1.mCommand_Output_Current = mSettingCurrent * 10;
            }
        }

        public void onCancel(object obj, int type, string title, string content)
        {

        }

        private void cb_do_relay_plus_CheckedChanged(object sender, EventArgs e)
        {
            ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager())
                .packet_z1.bPowerRelay_Plus = ((CheckBox)sender).Checked;
        }

        private void cb_do_relay_minus_CheckedChanged(object sender, EventArgs e)
        {

            ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager())
            .packet_z1.bPowerRelay_Minus = ((CheckBox)sender).Checked;



        }

        private void cb_do_mc_CheckedChanged(object sender, EventArgs e)
        {
            ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager())
                .packet_z1.bMC_On = ((CheckBox)sender).Checked;
        }

        private void cb_do_fan_CheckedChanged(object sender, EventArgs e)
        {
            ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager())
                .packet_z1.bFAN_On = ((CheckBox)sender).Checked;
        }

        private void cb_do_led_red_CheckedChanged(object sender, EventArgs e)
        {
            ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager())
                .packet_z1.bLED1_Red = ((CheckBox)sender).Checked;
        }

        private void cb_do_led_green_CheckedChanged(object sender, EventArgs e)
        {
            ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager())
                .packet_z1.bLED1_Green = ((CheckBox)sender).Checked;
        }

        private void cb_do_led_blue_CheckedChanged(object sender, EventArgs e)
        {
            ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager())
                .packet_z1.bLED1_Blue = ((CheckBox)sender).Checked;
        }

        public string getTitle()
        {
            return "설정";
        }

        public string getSubTitle()
        {
            return "테스트모드 (충전)";
        }

        public UserControl getUserControl()
        {
            return this;
        }

        public int getChannelIndex()
        {
            return 0;
        }

        public void updateView()
        {

        }

        public void setText(int indexArray, string text)
        {

        }

        public void setVisibility(int indexArray, bool visible)
        {

        }
    }
}
