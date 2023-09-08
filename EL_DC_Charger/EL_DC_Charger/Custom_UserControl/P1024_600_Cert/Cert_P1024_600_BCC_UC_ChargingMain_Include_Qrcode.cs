using EL_DC_Charger.common.interf;
using EL_DC_Charger.common.interf.uiux;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.Wev.ImgButtonManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.P1024_600_Cert
{
    public partial class Cert_P1024_600_BCC_UC_ChargingMain_Include_Qrcode : UserControl, IUC_Channel
    {
        protected int mChannelIndex = 0;
        public Cert_P1024_600_BCC_UC_ChargingMain_Include_Qrcode(int channelIndex) : this()
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

        public Cert_P1024_600_BCC_UC_ChargingMain_Include_Qrcode()
        {
            InitializeComponent();

            Wev_ImageButtonManager_Home btn_Home = new Wev_ImageButtonManager_Home(pb_home);
        }

        private void pb_home_Click(object sender, EventArgs e)
        {

        }

    }
}
