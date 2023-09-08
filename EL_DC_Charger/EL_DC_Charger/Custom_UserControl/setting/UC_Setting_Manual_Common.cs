using EL_DC_Charger.BatteryChange_Charger.SerialPorts.IOBoard;
using EL_DC_Charger.common.interf;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.ChargerVariable;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.keypad;
using EL_DC_Charger.EL_DC_Charger.SerialPorts.ControlBoard.Packet.Child;
using EL_DC_Charger.EL_DC_Charger.SerialPorts.smartro_tl3500bs;
using OpenCvSharp;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.setting
{
    public partial class UC_Setting_Manual_Common : UserControl, IRFCardReader_EventListener, IKeypad_EventListener
    {
        const int MAXIMUM_VOLTAGE = 1000;
        const int MAXIMUM_CURRENT = 100;
        static int DEFAULT_VOLTAGE = 400;
        static int DEFAULT_CURRENT = 30;
        int mSettingVoltage = DEFAULT_VOLTAGE;
        int mSettingCurrent = DEFAULT_CURRENT;

        public bool cctvEnable = false;
        EL_DC_Charger_MyApplication mApplication = EL_DC_Charger_MyApplication.getInstance();
        public UC_Setting_Manual_Common()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = true;            
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
        private void cb_do_mc_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 1; i <= EL_DC_Charger_MyApplication.getInstance().getChannelCount(); i++)
            {
                ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i).getControlbdComm_PacketManager())
                    .packet_z1.bMC_On = ((CheckBox)sender).Checked;
            } 
        }

        private void cb_do_fan_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 1; i <= EL_DC_Charger_MyApplication.getInstance().getChannelCount(); i++)
            {
                ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i).getControlbdComm_PacketManager())
                .packet_z1.bFAN_On = ((CheckBox)sender).Checked;
            }

        }

        private void cb_do_led_red_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 1; i <= EL_DC_Charger_MyApplication.getInstance().getChannelCount(); i++)
            {
                ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i).getControlbdComm_PacketManager())
                .packet_z1.bLED1_Red = ((CheckBox)sender).Checked;
            }

        }

        private void cb_do_led_green_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 1; i <= EL_DC_Charger_MyApplication.getInstance().getChannelCount(); i++)
            {
                ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i).getControlbdComm_PacketManager())
                .packet_z1.bLED1_Green = ((CheckBox)sender).Checked;
            }
        }

        private void cb_do_led_blue_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 1; i <= EL_DC_Charger_MyApplication.getInstance().getChannelCount(); i++)
            {
                ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i).getControlbdComm_PacketManager())
                .packet_z1.bLED1_Blue = ((CheckBox)sender).Checked;
            }

        }

        private void button_rfid_start_Click(object sender, EventArgs e)
        {

        }

        private void button_rfid_stop_Click(object sender, EventArgs e)
        {

        }
        private void button_cardRead_start_Click(object sender, EventArgs e)
        {
            if (EL_DC_Charger_MyApplication.getInstance().RFCardReader_Manager != null && ((Smartro_TL3500BS_Comm_Manager)EL_DC_Charger_MyApplication.getInstance().RFCardReader_Manager).IsConnected_HW)
            {
                EL_DC_Charger_MyApplication.getInstance().RFCardReader_Manager.setRFCardReader_Listener(this);
                EL_DC_Charger_MyApplication.getInstance().RFCardReader_Manager.setCommand_Search_RFCard();
                label_cardnumber_card.Text = "카드리딩 대기중....";
            }
            else
            {
                label_cardnumber_card.Text = "카드단말기 연결 오류....";
            }
        }

        private void button_cardRead_stop_Click(object sender, EventArgs e)
        {
            if (EL_DC_Charger_MyApplication.getInstance().RFCardReader_Manager != null && ((Smartro_TL3500BS_Comm_Manager)EL_DC_Charger_MyApplication.getInstance().RFCardReader_Manager).IsConnected_HW)
            {
                ((EL_DC_Charger_MyApplication)mApplication).SerialPort_Smartro_CardReader.getManager_Send().setCommand(Smartro_TL3500BS_Constants.Command.NONE);
                //EL_DC_Charger_MyApplication.getInstance().RFCardReader_Manager.setRFCardReader_Listener(this);
                //EL_DC_Charger_MyApplication.getInstance().RFCardReader_Manager.setCommand_Search_RFCard();
                label_cardnumber_card.Text = "";
            }
            else
            {
                label_cardnumber_card.Text = "카드단말기 연결 오류....";
            }
        }
        public void onReceive(string rfCardNumber)
        {
            string num;
            if (rfCardNumber.Length == 16)
                num = rfCardNumber.Insert(4, "-").Insert(9, "-").Insert(14, "-");
            else
                num = rfCardNumber;
            this.Invoke(new MethodInvoker(delegate ()
            {
                label_cardnumber_card.Text = num;
            }));

        }

        public void onReceiveFailed(string result)
        {

        }

        public void onEnterComplete(object obj, int type, string title, string content, string afterContent)
        {

        }

        public void onCancel(object obj, int type, string title, string content)
        {
            throw new NotImplementedException();
        }



        private void cb_do_ControlBoadrd_Reset_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cb_do_ir_Led_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 1; i <= EL_DC_Charger_MyApplication.getInstance().getChannelCount(); i++)
            {
                ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i).getControlbdComm_PacketManager())
                .packet_z1.bOn_IRLED = ((CheckBox)sender).Checked;
            }
        }

        private void cb_do_Rfid_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 1; i <= EL_DC_Charger_MyApplication.getInstance().getChannelCount(); i++)
            {
                ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i).getControlbdComm_PacketManager())
                .packet_z1.bOn_RFID_Power = ((CheckBox)sender).Checked;
            }

        }

        private void UC_Setting_Manual_Common_Load(object sender, EventArgs e)
        {
            ui_timer.Enabled = true;
            //if (!bck_camera.IsBusy)
            //    bck_camera.RunWorkerAsync();
        }

        private void ui_timer_Tick(object sender, EventArgs e)
        {

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
            cb_di_emg.Checked = EL_ControlbdComm_Packet_z1_FuncTest_Receive.EmgStatus;
            if (cb_di_emg.Checked)
            {
                cb_di_emg.Text = "ON";
                cb_di_emg.ForeColor = Color.Red;
            }
            else
            {
                cb_di_emg.Text = "OFF";
                cb_di_emg.ForeColor = Color.Blue;
            }

            cb_di_sumRelay_plus.Checked = EL_ControlbdComm_Packet_z1_FuncTest_Receive.SUM_Relay_Plus;
            if (cb_di_sumRelay_plus.Checked)
            {
                cb_di_sumRelay_plus.Text = "ON";
                cb_di_sumRelay_plus.ForeColor = Color.Red;
            }
            else
            {
                cb_di_sumRelay_plus.Text = "OFF";
                cb_di_sumRelay_plus.ForeColor = Color.Blue;
            }
            cb_di_sumRelay_minus.Checked = EL_ControlbdComm_Packet_z1_FuncTest_Receive.SUM_Relay_Minus;
            if (cb_di_sumRelay_minus.Checked)
            {
                cb_di_sumRelay_minus.Text = "ON";
                cb_di_sumRelay_minus.ForeColor = Color.Red;
            }
            else
            {
                cb_di_sumRelay_minus.Text = "OFF";
                cb_di_sumRelay_minus.ForeColor = Color.Blue;
            }

        }


        /// <summary>
        /// 문현성 50,100kw 카메라 모듈 연동 2023-07-07
        /// </summary>
        
        //rtsp 접속 정보
        string rstpAddr = "rtsp://192.168.1.30:554/stream2";
        VideoCapture camera;

        //UI 프리징으로 인해 백그라운드 Thread 사용
        private void bck_camera_DoWork(object sender, DoWorkEventArgs e)
        {
            camera = new VideoCapture(rstpAddr);

            using (Mat cameraImage = new Mat())
            {
                while (cctvEnable)
                {
                    try
                    {
                        //RTSP로 요청한 화면을 불러옴.
                        if (!camera.Read(cameraImage))
                        {                            
                            Cv2.WaitKey();
                        }
                        //받아온 객체의 사이즈 확인
                        if (cameraImage.Size().Width > 0 && cameraImage.Size().Height > 0)
                        {
                            //Mat 파일 Bitmap으로 변환 
                            Bitmap bitmap = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(cameraImage);

                            //pictureBox 이미지 갱신
                            pb_camera.Image = bitmap;
                        }
                        if (Cv2.WaitKey(1) >= 0)
                            break;
                    }
                    catch
                    {

                    }
                }
            }
        }
    }
}
