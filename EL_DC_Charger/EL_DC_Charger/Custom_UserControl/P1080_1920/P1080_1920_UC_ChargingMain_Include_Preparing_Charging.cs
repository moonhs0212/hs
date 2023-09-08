using EL_DC_Charger.common.application;
using EL_DC_Charger.common.ChargerVariable;
using EL_DC_Charger.common.interf.uiux;
using EL_DC_Charger.common.item;
using EL_DC_Charger.common.variable;
using EL_DC_Charger.EL_DC_Charger.Applications;
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
    public partial class P1080_1920_UC_ChargingMain_Include_Preparing_Charging : UserControl, IUC_Channel
    {
        public P1080_1920_UC_ChargingMain_Include_Preparing_Charging()
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

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            this.AutoScaleMode = AutoScaleMode.Font;
            switch (EL_DC_Charger_MyApplication.getInstance().getChargerType())
            {
                case EChargerType.NONE:
                case EChargerType.CH1_NOT_PUBLIC:
                    label_chargingunit.Visible = false;
                    break;
            }
            Wev02_ImageButtonManager_Back imgButton_Back
                = new Wev02_ImageButtonManager_Back(pb_back);

            Wev02_ImageButtonManager_Home imgButton_Home
                = new Wev02_ImageButtonManager_Home(pb_home);


            mImage_Blank = Properties.Resources.wev02_img_icon_progress_00;

            mList_ProgressItem[0] = new ItemInfor_Progress(-5, 9, Properties.Resources.wev02_img_icon_progress_01);
            mList_ProgressItem[1] = new ItemInfor_Progress(-4, 8, Properties.Resources.wev02_img_icon_progress_02);
            mList_ProgressItem[2] = new ItemInfor_Progress(-3, 7, Properties.Resources.wev02_img_icon_progress_03);
            mList_ProgressItem[3] = new ItemInfor_Progress(-2, 6, Properties.Resources.wev02_img_icon_progress_04);
            mList_ProgressItem[4] = new ItemInfor_Progress(-1, 5, Properties.Resources.wev02_img_icon_progress_05);

            mPb[0] = pb_progress0;
            mPb[1] = pb_progress1;
            mPb[2] = pb_progress2;
            mPb[3] = pb_progress3;
            mPb[4] = pb_progress4;


            EL_DC_Charger_MyApplication.getInstance().setTouchManger(this);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;    // Turn on WS_EX_COMPOSITED
                //cp.ExStyle |= 0x00080000;
                return cp;
            }
        }
        PictureBox[] mPb = new PictureBox[5];
        ItemInfor_Progress[] mList_ProgressItem = new ItemInfor_Progress[5];

        class ItemInfor_Progress
        {
            public int mNormalIndex = 0;
            public int mReverseIndex = 0;

            public int mCurrentIndex = 0;
            public bool bIsReverse = false;
            public Image mResource = null;
            public ItemInfor_Progress(int normalIndex, int reverseIndex, Image resource)
            {
                mNormalIndex = normalIndex;
                mReverseIndex = reverseIndex;

                mCurrentIndex = mNormalIndex;
                bIsReverse = false;
                mResource = resource;
            }

            public void setDirection(bool isReverse)
            {
                if (!isReverse)
                {
                    //Normal
                    mCurrentIndex = mNormalIndex;
                    bIsReverse = false;
                }
                else
                {
                    mCurrentIndex = mReverseIndex;
                    bIsReverse = true;
                }
            }
        }


        Image mImage_Blank = null;
        bool bReverse = false;
        bool[] setting = new bool[5];
        public bool updateProgress()
        {
            int count = 0;
            for (int i = 0; i < setting.Length; i++)
                setting[i] = false;
            int unit = 0;
            if (!bReverse)
                unit = 1;
            else
                unit = -1;
            for (int i = 0; i < mList_ProgressItem.Length; i++)
            {
                mList_ProgressItem[i].mCurrentIndex += unit;
                if (mList_ProgressItem[i].mCurrentIndex > -1 && mList_ProgressItem[i].mCurrentIndex < 5)
                {
                    mPb[mList_ProgressItem[i].mCurrentIndex].Image = mList_ProgressItem[i].mResource;
                    count++;
                    setting[mList_ProgressItem[i].mCurrentIndex] = true;
                }
            }

            for (int i = 0; i < setting.Length; i++)
            {
                if (!setting[i])
                {
                    mPb[i].Image = mImage_Blank;
                }
            }

            if (count > 0)
                return true;
            return false;

        }

        public UserControl getUserControl()
        {
            return this;
        }

        EL_Time mTime_Update = new EL_Time();

        EL_Time mTime_Update_UI = new EL_Time();
        public void updateUI()
        {
            if (mTime_Update.getMiliSecond_WastedTime() >= 800)
            {


                int second = (int)(mRemainSecond - mTime_Update.getSecond_WastedTime());
                label_remaintime.Text = "" + second;



            }
            if (mTime_Update_UI.getMiliSecond_WastedTime() > 500)
            {
                mTime_Update_UI.setTime();
                if (!updateProgress())
                {
                    bReverse = !bReverse;
                    for (int i = 0; i < mList_ProgressItem.Length; i++)
                    {
                        mList_ProgressItem[i].setDirection(bReverse);
                    }
                }
            }


        }

        public int getRemainTime()
        {
            return mRemainSecond;
        }
        protected int mRemainSecond = 100;

        public void initVariable()
        {
            mRemainSecond = 100;
            label_remaintime.Text = "100";
            mTime_Update.setTime();
        }

        public int getChannelIndex()
        {
            return 1;
        }

        public void updateView()
        {
            label_chargingunit.Text = "회원 " + EL_DC_Charger_MyApplication.getInstance().MemberAmount + " 원/kWh | 비회원 " + EL_DC_Charger_MyApplication.getInstance().NonmemberAmount + "원 / kWh";;
            lbl_cpid.Text = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting_OCPP().getTable_Setting(0).getSettingData((int)CONST_INDEX_OCPP_Setting.infor_ChargeBoxSerialNumber);

            updateUI();
        }

        public void setText(int indexArray, string text)
        {
            
        }

        public void setVisibility(int indexArray, bool visible)
        {
            
        }
    }
}
