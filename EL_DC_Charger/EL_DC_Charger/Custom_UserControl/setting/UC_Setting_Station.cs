
using EL_DC_Charger.BatteryChange_Charger.SerialPorts.IOBoard;
using EL_DC_Charger.common;
using EL_DC_Charger.common.application;
using EL_DC_Charger.common.ChargerVariable;
using EL_DC_Charger.common.INI;
using EL_DC_Charger.common.interf;
using EL_DC_Charger.common.Manager;
using EL_DC_Charger.common.variable;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.ChargerVariable;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.keypad;
using EL_DC_Charger.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.setting
{
    public partial class UC_Setting_Station : UserControl, IKeypad_EventListener
    {
        public UC_Setting_Station()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            cb_firstsetting_complete.Checked = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).getSettingData_Boolean(CONST_INDEX_MAINSETTING.IS_COMPLETE_FIRSTSETTING);
            cb_testform.Checked = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).getSettingData_Boolean(CONST_INDEX_MAINSETTING.IS_SHOW_TEST_FORM);

        }

        private void button_auto_start_confirm_Click(object sender, EventArgs e)
        {
            string result = "";
            if (EL_Mananger_System.taskManager_IsExistTask(EL_DC_Charger_MyApplication.NAME_SERVICE))
                result = "서비스가 등록되어 있습니다.";
            else
                result = "서비스가 등록되어 있지 않습니다..";
            label_auto_start_add.Text = result;
        }

        private void button_auto_start_add_Click(object sender, EventArgs e)
        {
            string result = EL_Mananger_System.taskManager_AddTask(EL_DC_Charger_MyApplication.NAME_SERVICE, EL_DC_Charger_MyApplication.DESCRIPTION);
            label_auto_start_add.Text = result;
        }

        private void button_auto_start_remove_Click(object sender, EventArgs e)
        {
            string result = EL_Mananger_System.taskManager_RemoveTask(EL_DC_Charger_MyApplication.NAME_SERVICE);
            label_auto_start_add.Text = result;
        }


        private void cb_firstsetting_complete_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).setSettingData(CONST_INDEX_MAINSETTING.IS_COMPLETE_FIRSTSETTING, cb_firstsetting_complete.Checked);
        }

        private void cb_testform_CheckedChanged(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).setSettingData(CONST_INDEX_MAINSETTING.IS_SHOW_TEST_FORM, cb_testform.Checked);
        }

        private void initForm()
        {
            foreach (EChargerType item in Enum.GetValues(typeof(EChargerType)))
            {
                string value = item.ToString();
                cb_chargerType.Items.Add(value);
            }

            foreach (EChargerMaker item in Enum.GetValues(typeof(EChargerMaker)))
            {
                string value = item.ToString();
                cb_ChargerMaker.Items.Add(value);
            }

            foreach (EPlatform item in Enum.GetValues(typeof(EPlatform)))
            {
                string value = item.ToString();
                cb_Platform.Items.Add(value);
            }

            foreach (EPlatformOperator item in Enum.GetValues(typeof(EPlatformOperator)))
            {
                string value = item.ToString();
                cb_PlatformOper.Items.Add(value);
            }

            foreach (EHmi_Screen_Mode item in Enum.GetValues(typeof(EHmi_Screen_Mode)))
            {
                string value = item.ToString();
                cb_sizeMode.Items.Add(value);
            }

            if (EL_DC_Charger_MyApplication.getInstance().EAmiCompany == EAmiCompany.Odhitec)
                rb_odhitec.Checked = true;
            else
                rb_pilot.Checked = true;
            cb_trd.Checked = EL_DC_Charger_MyApplication.getInstance().isTrd;

            if (EL_DC_Charger_MyApplication.getInstance().getChannelCount() == 1)
                rb_ch1.Checked = true;
            else
                rb_ch2.Checked = true;

            cb_chargerType.Text = EL_DC_Charger_MyApplication.getInstance().getChargerType().ToString();
            cb_ChargerMaker.Text = EL_DC_Charger_MyApplication.getInstance().getChargerMaker().ToString();
            cb_Platform.Text = EL_DC_Charger_MyApplication.getInstance().getPlatform().ToString();
            cb_PlatformOper.Text = EL_DC_Charger_MyApplication.getInstance().getPlatform_Operator().ToString();

            cb_sizeMode.Text = EL_DC_Charger_MyApplication.HMI_SCREEN_MODE.ToString();

            tb_fromMonth.Text = CsINIManager.IniReadValue(Application.StartupPath + @"\Config.ini", "DAYS", "fromMonth");
            tb_fromDay.Text = CsINIManager.IniReadValue(Application.StartupPath + @"\Config.ini", "DAYS", "fromDay");
            tb_fromDay.Text = CsINIManager.IniReadValue(Application.StartupPath + @"\Config.ini", "DAYS", "toMonth");
            tb_toDay.Text = CsINIManager.IniReadValue(Application.StartupPath + @"\Config.ini", "DAYS", "toDay");
            cb_calibrationMode.Checked = EL_DC_Charger_MyApplication.getInstance().calibrationMode;

            btn_member_amount.Text = EL_DC_Charger_MyApplication.getInstance().MemberAmount.ToString();
            btn_nonmember_amount.Text = EL_DC_Charger_MyApplication.getInstance().NonmemberAmount.ToString();


            for (int i = 0; i < EL_MyApplication_Base.list_setting.Count; i++)
            {
                string _name = EL_MyApplication_Base.list_setting[i].name;
                string _type = EL_MyApplication_Base.list_setting[i].type;
                string _value = EL_MyApplication_Base.list_setting[i].value;

                var ctl = tableLayoutPanel1.Controls.Find(_name, false);

                //if (ctl != null && ctl.Length > 0)
                //{
                //    if (_type == "TEXTBOX")
                //    {
                //        (ctl[0] as UC_config).TITLE_TEXT = EL_MyApplication_Base.list_setting[i].title;
                //        (ctl[0] as UC_config).VALUE = _value;
                //    }
                //    else
                //    {
                //        (ctl[0] as UC_config).TITLE_TEXT = EL_MyApplication_Base.list_setting[i].title;
                //        (ctl[0] as UC_config).CHECKED = Tobool(_value);
                //    }

                //}
            }

        }

        private bool Tobool(string data)
        {
            return (data == "1");
        }
        private void button_save_Click(object sender, EventArgs e)
        {
            //DB저장
            EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).setSettingData(CONST_INDEX_MAINSETTING.CHARGERTYPE, cb_chargerType.Text);
            EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).setSettingData(CONST_INDEX_MAINSETTING.CHARGERMAKER, cb_ChargerMaker.Text);
            EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).setSettingData(CONST_INDEX_MAINSETTING.PLATFORM, cb_Platform.Text);
            EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).setSettingData(CONST_INDEX_MAINSETTING.PLATFORMOPERATOR, cb_PlatformOper.Text);

            //저장된 enum 이름
            string getNameType = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).getSettingData(CONST_INDEX_MAINSETTING.CHARGERTYPE);
            string getNameMaker = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).getSettingData(CONST_INDEX_MAINSETTING.CHARGERMAKER);
            string getNameFlatform = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).getSettingData(CONST_INDEX_MAINSETTING.PLATFORM);
            string getNameFlatformOper = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).getSettingData(CONST_INDEX_MAINSETTING.PLATFORMOPERATOR);


            //enum 숫자 항목 순번 찾기
            int numType = (int)(EChargerType)System.Enum.Parse(typeof(EChargerType), getNameType);
            int numMaker = (int)(EChargerMaker)System.Enum.Parse(typeof(EChargerMaker), getNameMaker);
            int numFlatform = (int)(EPlatform)System.Enum.Parse(typeof(EPlatform), getNameFlatform);
            int numFlatformOper = (int)(EPlatformOperator)System.Enum.Parse(typeof(EPlatformOperator), getNameFlatformOper);
            //변수 셋팅
            EL_DC_Charger_MyApplication.getInstance().setChargerType((EChargerType)numType);
            EL_DC_Charger_MyApplication.getInstance().setChargerMaker((EChargerMaker)numMaker);
            EL_DC_Charger_MyApplication.getInstance().setPlatform((EPlatform)numFlatform);
            EL_DC_Charger_MyApplication.getInstance().setPlatform_Operator((EPlatformOperator)numFlatformOper);



            CsINIManager.IniWriteValue(Application.StartupPath + @"\Config.ini", "DAYS", "fromMonth", tb_fromMonth.Text);
            CsINIManager.IniWriteValue(Application.StartupPath + @"\Config.ini", "DAYS", "fromDay", tb_fromDay.Text);
            CsINIManager.IniWriteValue(Application.StartupPath + @"\Config.ini", "DAYS", "toMonth", tb_fromDay.Text);
            CsINIManager.IniWriteValue(Application.StartupPath + @"\Config.ini", "DAYS", "toDay", tb_toDay.Text);
            CsINIManager.IniWriteValue(Application.StartupPath + @"\Config.ini", "MODE", "CALIBRATION", cb_calibrationMode.Checked);
            EL_DC_Charger_MyApplication.getInstance().calibrationMode = cb_calibrationMode.Checked;

            if (rb_odhitec.Checked)
                CsINIManager.IniWriteValue(Application.StartupPath + @"\Config.ini", "AMI", "NAME", EAmiCompany.Odhitec.ToString());
            else
                CsINIManager.IniWriteValue(Application.StartupPath + @"\Config.ini", "AMI", "NAME", EAmiCompany.Pilot.ToString());

            CsINIManager.IniWriteValue(Application.StartupPath + @"\Config.ini", "BOARD", "TRD_YN", cb_trd.Checked);

            if (rb_ch1.Checked)
                CsINIManager.IniWriteValue(Application.StartupPath + @"\Config.ini", "CHANNEL", "CH", "1");
            else
                CsINIManager.IniWriteValue(Application.StartupPath + @"\Config.ini", "CHANNEL", "CH", "2");


            CsINIManager.IniWriteValue(Application.StartupPath + @"\Config.ini", "AMOUNT", "MEMBER", btn_member_amount.Text);
            CsINIManager.IniWriteValue(Application.StartupPath + @"\Config.ini", "AMOUNT", "NON_MEMBER", btn_nonmember_amount.Text);
            CsINIManager.IniWriteValue(Application.StartupPath + @"\Config.ini", "SIZE", "SIZE", cb_sizeMode.Text);


            EL_DC_Charger_MyApplication.list_setting.Clear();
            //conList.Clear();
            ////컨트롤 갯수 구함
            //foreach (Control control in tableLayoutPanel1.Controls)
            //{
            //    if (control is UC_config)
            //    {
            //        conList.Add(control as UC_config);
            //    }
            //}

            ////동적으로 생성한 변수 INI에 저장함   
            //for (int i = 0; i < conList.Count; i++)
            //{

            //    if (conList[i].TYPE.ToString() == "TEXTBOX")
            //    {
            //        CsINIManager.IniWriteValue(Application.StartupPath + @"\Config.ini", "CONTROL-" + i, "TITLE", conList[i].TITLE_TEXT);
            //        CsINIManager.IniWriteValue(Application.StartupPath + @"\Config.ini", "CONTROL-" + i, "NAME", conList[i].Name);
            //        CsINIManager.IniWriteValue(Application.StartupPath + @"\Config.ini", "CONTROL-" + i, "VALUE", conList[i].VALUE);
            //        CsINIManager.IniWriteValue(Application.StartupPath + @"\Config.ini", "CONTROL-" + i, "TYPE", conList[i].TYPE.ToString());
            //    }
            //    else
            //    {
            //        CsINIManager.IniWriteValue(Application.StartupPath + @"\Config.ini", "CONTROL-" + i, "TITLE", conList[i].TITLE_TEXT);
            //        CsINIManager.IniWriteValue(Application.StartupPath + @"\Config.ini", "CONTROL-" + i, "NAME", conList[i].Name);
            //        CsINIManager.IniWriteValue(Application.StartupPath + @"\Config.ini", "CONTROL-" + i, "VALUE", conList[i].CHECKED);
            //        CsINIManager.IniWriteValue(Application.StartupPath + @"\Config.ini", "CONTROL-" + i, "TYPE", conList[i].TYPE.ToString());
            //    }
            //}

        }

        private void timer_init_Tick(object sender, EventArgs e)
        {
            initForm();
            timer_init.Enabled = false;
        }

        private void cb_isDebug_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_isDebug.Checked)
                EL_DC_Charger_MyApplication.getInstance().debug = true;
            else
                EL_DC_Charger_MyApplication.getInstance().debug = false;
        }

        public void onEnterComplete(object obj, int type, string title, string content, string afterContent)
        {
            if (obj.Equals(btn_member_amount))
            {
                btn_member_amount.Text = afterContent;
            }
            else if (obj.Equals(btn_nonmember_amount))
            {
                btn_nonmember_amount.Text = afterContent;
            }
        }

        public void onCancel(object obj, int type, string title, string content)
        {

        }

        private void btn_member_amount_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.setVariable(sender, Wev_Keypad_Type.NUMBER, 9999, 10,
            "회원가 입력.", "" + EL_DC_Charger_MyApplication.getInstance().MemberAmount, "원");
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.setKeypad_EventListener(this);
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.Show();
        }

        private void btn_nonmember_amount_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.setVariable(sender, Wev_Keypad_Type.NUMBER, 9999, 10,
            "비회원가 입력.", "" + EL_DC_Charger_MyApplication.getInstance().NonmemberAmount, "원");
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.setKeypad_EventListener(this);
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.Show();
        }
    }
}
