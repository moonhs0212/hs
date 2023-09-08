using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EL_DC_Charger.EL_DC_Charger.viewmanager
{
    public class Wev_ViewManager_ProgressCircle
    {
        PictureBox mPb = null;
        Timer mTimer = new Timer();
        protected int mIndexArray_Image = 0;
        protected int mIncreaseUnit = 1;
        Image[] mImages = new Image[5];
        public Wev_ViewManager_ProgressCircle(PictureBox pb)
        {
            mPb = pb;
            mTimer.Interval = 1000;
            mTimer.Enabled = true;
            mTimer.Tick += MTimer_Tick;
            mImages[0] = Properties.Resources.wev_img_icon_circleprogress_00;
            mImages[1] = Properties.Resources.wev_img_icon_circleprogress_01;
            mImages[2] = Properties.Resources.wev_img_icon_circleprogress_02;
            mImages[3] = Properties.Resources.wev_img_icon_circleprogress_03;
            mImages[4] = Properties.Resources.wev_img_icon_circleprogress_04;
        }

        private void increaseIndexArray_Image()
        {
            mPb.Image = mImages[mIndexArray_Image];
            mIndexArray_Image += mIncreaseUnit;

            if (mIndexArray_Image >= mImages.Length-1 && mIncreaseUnit > 0)
                mIncreaseUnit = -1;
            else if (mIndexArray_Image <= 0 && mIncreaseUnit < 0)
                mIncreaseUnit = 1;
            
        }

        private void MTimer_Tick(object sender, EventArgs e)
        {
            increaseIndexArray_Image();
        }

        public void Start()
        {
            mTimer.Start();
        }

        public void Stop()
        {
            mTimer.Stop();
        }




    }
}
