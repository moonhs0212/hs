using EL_DC_Charger.common.interf;
using EL_DC_Charger.Interface_Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EL_DC_Charger.common.ImgButtonManager
{
    abstract public class ImageButtonManager_Base
    {
        protected PictureBox mPb = null;
        protected Panel mPanel = null;
        protected Image mImage_Normal = null;
        protected Image mImage_Click = null;
        public ImageButtonManager_Base(ArrayList labels, PictureBox pb, Image normal, Image click)
        {
            mPb = pb;
            mImage_Normal = normal;
            mImage_Click = click;
            if (labels != null)
            {
                for (int i = 0; i < labels.Count; i++)
                {
                    ((Label)labels[i]).Click += Pb_chargingcomplete_Click;
                    ((Label)labels[i]).MouseEnter += Pb_chargingcomplete_MouseEnter;
                    ((Label)labels[i]).MouseLeave += Pb_chargingcomplete_MouseLeave;
                    ((Label)labels[i]).MouseUp += Pb_chargingcomplete_MouseUp;
                }
            }
            
            mPb.Click += Pb_chargingcomplete_Click;
            mPb.MouseEnter += Pb_chargingcomplete_MouseEnter;
            mPb.MouseLeave += Pb_chargingcomplete_MouseLeave;
            mPb.MouseUp += Pb_chargingcomplete_MouseUp;
        }

        public ImageButtonManager_Base(ArrayList labels, Panel pb, Image normal, Image click)
        {
            mPanel = pb;
            mImage_Normal = normal;
            mImage_Click = click;
            if (labels != null)
            {
                for (int i = 0; i < labels.Count; i++)
                {
                    ((Label)labels[i]).Click += Pb_chargingcomplete_Click;
                    ((Label)labels[i]).MouseEnter += Pb_chargingcomplete_MouseEnter;
                    ((Label)labels[i]).MouseLeave += Pb_chargingcomplete_MouseLeave;
                    ((Label)labels[i]).MouseUp += Pb_chargingcomplete_MouseUp;
                }
            }

            mPb.Click += Pb_chargingcomplete_Click;
            mPb.MouseEnter += Pb_chargingcomplete_MouseEnter;
            mPb.MouseLeave += Pb_chargingcomplete_MouseLeave;
            mPb.MouseUp += Pb_chargingcomplete_MouseUp;
        }
        private void Pb_chargingcomplete_MouseUp(object sender, MouseEventArgs e)
        {
            mPb.Image = mImage_Normal;
        }

        private void Pb_chargingcomplete_MouseLeave(object sender, EventArgs e)
        {
            mPb.Image = mImage_Normal;
        }

        private void Pb_chargingcomplete_MouseEnter(object sender, EventArgs e)
        {
            mPb.Image = mImage_Click;
        }

        private void Pb_chargingcomplete_Click(object sender, EventArgs e)
        {
            mPb.Image = mImage_Click;
            if (mClickListener != null)
                mClickListener.onClick_Confirm(sender);
        }

        protected IOnClickListener_Button mClickListener = null;
        public void setOnClickListener(IOnClickListener_Button clickListener)
        {
            mClickListener = clickListener;
        }

        //abstract protected Image getImage_Normal();
        //abstract protected Image getImage_Click();

    }
}
