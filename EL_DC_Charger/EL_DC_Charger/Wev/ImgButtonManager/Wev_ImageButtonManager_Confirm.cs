using EL_DC_Charger.common.ImgButtonManager;
using EL_DC_Charger.common.interf;
using EL_DC_Charger.EL_DC_Charger.Applications;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EL_DC_Charger.EL_DC_Charger.Wev.ImgButtonManager
{
    public class Wev_ImageButtonManager_Confirm : ImageButtonManager_Base, IOnClickListener_Button
    {
        public Wev_ImageButtonManager_Confirm(PictureBox pb) : base(null, pb, Properties.Resources.img_btn_confirm_normal, Properties.Resources.img_btn_confirm_clicked)
        {
            setOnClickListener(this);
        }
       
        public void onClick_Confirm(object sender)
        {
            //EL_DC_Charger_MyApplication.getInstance().Controller_Main.bIsClick_Confirm_ErrorReson_BeforeCharging = true;

            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mStateManager_Channel.bIsClick_Notify_1Button = true;
        }

        public void onClick_Cancel(object sender)
        {
            throw new NotImplementedException();
        }
    }
}
