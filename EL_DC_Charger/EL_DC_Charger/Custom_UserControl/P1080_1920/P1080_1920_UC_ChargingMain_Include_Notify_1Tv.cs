﻿using EL_DC_Charger.common.application;
using EL_DC_Charger.common.ChargerVariable;
using EL_DC_Charger.common.interf.uiux;
using EL_DC_Charger.common.variable;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.keypad;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.manager;
using EL_DC_Charger.EL_DC_Charger.Wev02;
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

namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.P1080_1920
{
    public partial class P1080_1920_UC_ChargingMain_Include_Notify_1Tv : UserControl,
        IUC_Channel
    {

        protected int mChannelIndex = 0;
        public P1080_1920_UC_ChargingMain_Include_Notify_1Tv(int channelIndex) : this()
        {
            mChannelIndex = channelIndex;

        }

        public int getChannelIndex()
        {
            return mChannelIndex;
        }

        public UserControl getUserControl()
        {
            return this;
        }

        public void initVariable()
        {

        }

        public void updateView()
        {
            label_chargingunit.Text = "회원 " + EL_DC_Charger_MyApplication.getInstance().MemberAmount + " 원/kWh | 비회원 " + EL_DC_Charger_MyApplication.getInstance().NonmemberAmount + "원 / kWh"; ;
            lbl_cpid.Text = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting_OCPP().getTable_Setting(0).getSettingData((int)CONST_INDEX_OCPP_Setting.infor_ChargeBoxSerialNumber);

        }
        public P1080_1920_UC_ChargingMain_Include_Notify_1Tv()
        {
            InitializeComponent();

            if (EL_MyApplication_Base.HMI_SCREEN_MODE == EHmi_Screen_Mode.P1024_600)
            {
                this.Width = 1024;
                this.Height = 600;
            }
            else if ((EL_DC_Charger_MyApplication.getInstance().getChargerType()) == EChargerType.CH2_CERT)
            {
                this.Width = 540;
                this.Height = 720;
            }

            Wev02_ImageButtonManager_Back imgButton_Back
                = new Wev02_ImageButtonManager_Back(btn_back);

            Wev02_ImageButtonManager_Home imgButton_Home
                = new Wev02_ImageButtonManager_Home(btn_home);

            EL_DC_Charger_MyApplication.getInstance().setTouchManger(this);
        }
        public void setVisibility(int indexArray, bool visible)
        {
            switch (indexArray)
            {
                case 0:
                    btn_home.Visible = visible;
                    break;
            }
        }

        public void setText(int indexArray, string text)
        {
            switch (indexArray)
            {
                case 0:
                    tv_content_1.Text = text;
                    break;
            }
        }
        DateTime lastTouch = new DateTime();
        private void pn1_Click(object sender, EventArgs e)
        {
            lastTouch = DateTime.Now;
        }

        private void pn2_Click(object sender, EventArgs e)
        {
            if (lastTouch.AddSeconds(1) >= DateTime.Now)
            {
                Wev_Form_Keypad wev_Form_Keypad = new Wev_Form_Keypad();
                wev_Form_Keypad.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().offlineTest_isuse = false;
        }
    }
}
