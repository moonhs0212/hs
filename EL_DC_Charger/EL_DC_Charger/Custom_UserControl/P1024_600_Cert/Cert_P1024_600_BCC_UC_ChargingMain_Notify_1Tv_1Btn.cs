using EL_DC_Charger.common.interf;
using EL_DC_Charger.common.interf.uiux;
using EL_DC_Charger.EL_DC_Charger.Applications;
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

namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.P1024_600_Cert
{
    public partial class Cert_P1024_600_BCC_UC_ChargingMain_Notify_1Tv_1Btn : UserControl, IUC_Channel_Button
    {
        protected int mChannelIndex = 0;
        public Cert_P1024_600_BCC_UC_ChargingMain_Notify_1Tv_1Btn(int channelIndex) : this()
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

        public Cert_P1024_600_BCC_UC_ChargingMain_Notify_1Tv_1Btn()
        {
            InitializeComponent();
        }

        private void pb_confirm_MouseUp(object sender, MouseEventArgs e)
        {
            pb_confirm.Image = Properties.Resources.img_btn_confirm_normal;
        }

        private void pb_confirm_MouseLeave(object sender, EventArgs e)
        {
            pb_confirm.Image = Properties.Resources.img_btn_confirm_normal;
        }

        private void pb_confirm_MouseEnter(object sender, EventArgs e)
        {
            pb_confirm.Image = Properties.Resources.img_btn_confirm_clicked;
        }

        private void pb_confirm_Click(object sender, EventArgs e)
        {
            pb_confirm.Image = Properties.Resources.img_btn_confirm_clicked;
            onClick(sender);
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

        protected IOnClickListener_Button mOnClickListener_NotifyButton = null;
        

        public void onClick(object obj)
        {
            if (mOnClickListener_NotifyButton != null)
            {
                mOnClickListener_NotifyButton.onClick_Confirm(obj);
            }else
            {
                EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getStateManager_Channel().bIsClick_Notify_1Button = true;
            }


        }

        public void setOnClickListener(IOnClickListener_Button listener)
        {
            mOnClickListener_NotifyButton = listener;
        }

        public void setVisibility(int indexArray, bool visible)
        {
            throw new NotImplementedException();
        }
    }
}
