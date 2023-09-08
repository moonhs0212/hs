using EL_DC_Charger.common.interf;
using EL_DC_Charger.common.interf.uiux;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.keypad;
using EL_DC_Charger.ocpp.ver16.database;
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
    public partial class UC_Setting_CSMS_Infor : UserControl, IUC_Setting_Child, IKeypad_EventListener
    {
        public UC_Setting_CSMS_Infor()
        {
            InitializeComponent();
        }

        public int getChannelIndex()
        {
            return 0;
        }

        public string getSubTitle()
        {
            return "CSMS 정보 설정";
        }

        public string getTitle()
        {
            return "설정";
        }

        public UserControl getUserControl()
        {
            return this;
        }

        public void initVariable()
        {
            label_CSMSIP.Text = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting_OCPP().getList_Manager_Table(0).getSettingData((int)CONST_INDEX_OCPP_Setting.infor_csms_ip);
            label_CSMSPort.Text = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting_OCPP().getList_Manager_Table(0).getSettingData((int)CONST_INDEX_OCPP_Setting.infor_csms_port);
            label_ChargeBox_SerialNo.Text = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting_OCPP().getList_Manager_Table(0).getSettingData((int)CONST_INDEX_OCPP_Setting.infor_ChargeBoxSerialNumber);
            label_CP_Model.Text = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting_OCPP().getList_Manager_Table(0).getSettingData((int)CONST_INDEX_OCPP_Setting.infor_chargePointModel);
            label_CP_Vendor.Text = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting_OCPP().getList_Manager_Table(0).getSettingData((int)CONST_INDEX_OCPP_Setting.infor_chargePointVendor);
            label_IP_Header.Text = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting_OCPP().getList_Manager_Table(0).getSettingData((int)CONST_INDEX_OCPP_Setting.infor_csms_ip_header);
            label_IP_Footer.Text = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting_OCPP().getList_Manager_Table(0).getSettingData((int)CONST_INDEX_OCPP_Setting.infor_csms_ip_more);
        }

        public void setText(int indexArray, string text)
        {

        }

        public void setVisibility(int indexArray, bool visible)
        {


        }

        public void updateView()
        {

        }

        private void label_CSMSIP_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.setVariable(sender, Wev_Keypad_Type.NONFORMAT, 10000, 10,
                "CSMS IP Address", ((Label)sender).Text,
                "");
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.setKeypad_EventListener(this);
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.Show();
        }

        private void label_CSMSPort_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.setVariable(sender, Wev_Keypad_Type.NUMBER, 65535, -1,
                "CSMS Port", ((Label)sender).Text,
                "");
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.setKeypad_EventListener(this);
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.Show();
        }

        private void label_ChargeBox_SerialNo_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.setVariable(sender, Wev_Keypad_Type.NONFORMAT, 65535, 1,
                "ChargeBox SerialNo", ((Label)sender).Text,
                "");
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.setKeypad_EventListener(this);
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.Show();
        }

        private void label_CP_Model_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.setVariable(sender, Wev_Keypad_Type.NONFORMAT, 65535, 1,
                "CP Model", ((Label)sender).Text,
                "");
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.setKeypad_EventListener(this);
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.Show();
        }

        private void label_CP_Vendor_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.setVariable(sender, Wev_Keypad_Type.NONFORMAT, 65535, 1,
                "CP Vendor", ((Label)sender).Text,
                "");
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.setKeypad_EventListener(this);
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.Show();
        }

        private void label_IP_Header_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.setVariable(sender, Wev_Keypad_Type.NONFORMAT, 65535, 1,
                "IP Header", ((Label)sender).Text,
                "");
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.setKeypad_EventListener(this);
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.Show();
        }

        private void label_IP_Footer_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.setVariable(sender, Wev_Keypad_Type.NONFORMAT, 65535, 1,
                "IP Footer", ((Label)sender).Text,
                "");
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.setKeypad_EventListener(this);
            EL_DC_Charger_MyApplication.getInstance().mKeyPad_OnlyNumber.Show();
        }

        public void onEnterComplete(object obj, int type, string title, string content, string afterContent)
        {
            ((Label)obj).Text = afterContent;

            if (obj.Equals(label_CSMSIP))
            {
                EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting_OCPP().getList_Manager_Table(0).setSettingData((int)CONST_INDEX_OCPP_Setting.infor_csms_ip, afterContent);
            }
            else if (obj.Equals(label_CSMSPort))
            {
                EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting_OCPP().getList_Manager_Table(0).setSettingData((int)CONST_INDEX_OCPP_Setting.infor_csms_port, afterContent);
            }
            else if (obj.Equals(label_ChargeBox_SerialNo))
            {
                EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting_OCPP().getList_Manager_Table(0).setSettingData((int)CONST_INDEX_OCPP_Setting.infor_ChargeBoxSerialNumber, afterContent);
            }
            else if (obj.Equals(label_CP_Model))
            {
                EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting_OCPP().getList_Manager_Table(0).setSettingData((int)CONST_INDEX_OCPP_Setting.infor_chargePointModel, afterContent);
            }
            else if (obj.Equals(label_CP_Vendor))
            {
                EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting_OCPP().getList_Manager_Table(0).setSettingData((int)CONST_INDEX_OCPP_Setting.infor_chargePointVendor, afterContent);
            }
            else if (obj.Equals(label_IP_Header))
            {
                EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting_OCPP().getList_Manager_Table(0).setSettingData((int)CONST_INDEX_OCPP_Setting.infor_csms_ip_header, afterContent);
            }
            else if (obj.Equals(label_IP_Footer))
            {
                EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting_OCPP().getList_Manager_Table(0).setSettingData((int)CONST_INDEX_OCPP_Setting.infor_csms_ip_more, afterContent);
            }

        }

        public void onCancel(object obj, int type, string title, string content)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                label_CSMSIP.Text = "csms.wev-charger.com";
                label_CSMSPort.Text = "0";
                label_ChargeBox_SerialNo.Text = "ELNY003";
                label_CP_Model.Text = "WEV-D030M1C";
                label_CP_Vendor.Text = "ELELECTRIC";
            }
            else
            {
                label_CSMSIP.Text = "dev.wev-charger.com";
                label_CSMSPort.Text = "12200";
                label_ChargeBox_SerialNo.Text = "NYJ-TEST0003";
                label_CP_Model.Text = "WEV-D030M1C";
                label_CP_Vendor.Text = "ELELECTRIC-NYJ";
            }
        }
    }
}
