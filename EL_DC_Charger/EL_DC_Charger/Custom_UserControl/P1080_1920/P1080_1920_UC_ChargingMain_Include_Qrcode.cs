using EL_DC_Charger.common.application;
using EL_DC_Charger.common.ChargerVariable;
using EL_DC_Charger.common.interf.uiux;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.manager;
using EL_DC_Charger.EL_DC_Charger.Wev02;
using EL_DC_Charger.ocpp.ver16.database;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.P1080_1920
{
    public partial class P1080_1920_UC_ChargingMain_Include_Qrcode : UserControl, IUC_Channel
    {
        protected int mChannelIndex = 0;
        public P1080_1920_UC_ChargingMain_Include_Qrcode(int channelIndex) : this()
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
            label_chargingunit.Text = "회원 " + EL_DC_Charger_MyApplication.getInstance().MemberAmount + " 원/kWh | 비회원 " + EL_DC_Charger_MyApplication.getInstance().NonmemberAmount + "원 / kWh";;
            lbl_cpid.Text = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting_OCPP().getTable_Setting(0).getSettingData((int)CONST_INDEX_OCPP_Setting.infor_ChargeBoxSerialNumber);

        }

        //public void makeQRcode(string url)
        //{
        //    //mTransition = new BunifuAnimatorNS.BunifuTransition();
        //    //bunifuLabel1.Visible = false;

        //    ZXing.BarcodeWriter barcodeWriter = new ZXing.BarcodeWriter();
        //    barcodeWriter.Format = ZXing.BarcodeFormat.QR_CODE;
        //    barcodeWriter.Options.Width = this.pictureBox_qrcode.Width;
        //    barcodeWriter.Options.Height = this.pictureBox_qrcode.Height;
        //    Bitmap image = barcodeWriter.Write(url);
        //    pictureBox_qrcode.Image = image;

        //    //mTransition.ShowSync(bunifuLabel1, false, BunifuAnimatorNS.Animation.HorizSlide);
        //}

        public void makeQRcode_Base64(string base64)
        {
            //mTransition = new BunifuAnimatorNS.BunifuTransition();
            //bunifuLabel1.Visible = false;
            byte[] bytes = Convert.FromBase64String(base64);
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                Bitmap bitmap = new Bitmap(ms);
                pictureBox_qrcode.Image = bitmap;
                // use the bitmap object here
            }



            //mTransition.ShowSync(bunifuLabel1, false, BunifuAnimatorNS.Animation.HorizSlide);
        }

        public void setText(int indexArray, string text)
        {

        }

        public void setVisibility(int indexArray, bool visible)
        {

        }

        public P1080_1920_UC_ChargingMain_Include_Qrcode()
        {
            InitializeComponent();

            if (EL_MyApplication_Base.HMI_SCREEN_MODE == EHmi_Screen_Mode.P1024_600)
            {
                this.Width = 1024;
                this.Height = 600;
            }

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            this.AutoScaleMode = AutoScaleMode.Dpi;
            //EL_UserControl_Manager.SearchAllControl(this);

            Wev02_ImageButtonManager_Back imgButton_Back
                = new Wev02_ImageButtonManager_Back(btn_back);

            Wev02_ImageButtonManager_Home imgButton_Home
                = new Wev02_ImageButtonManager_Home(btn_home);

            EL_DC_Charger_MyApplication.getInstance().setTouchManger(this);            
        }

        private void pb_home_Click(object sender, EventArgs e)
        {

        }

    }
}
