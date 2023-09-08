using EL_DC_Charger.common.ImgButtonManager;
using EL_DC_Charger.common.interf;
using EL_DC_Charger.EL_DC_Charger.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EL_DC_Charger.EL_DC_Charger.Wev02
{
    public class Wev02_ImageButtonManager_UsingStart : ImageButtonManager_Base, IOnClickListener_Button
    {
        public Wev02_ImageButtonManager_UsingStart(PictureBox pb) : base(null, pb, Properties.Resources.Group_255, Properties.Resources.Group_256)
        {
            setOnClickListener(this);
        }

        public void onClick_Cancel(object sender)
        {

        }

        public void onClick_Confirm(object sender)
        {
            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(2).mStateManager_Channel.bIsCommand_UsingStart= true;
        }
    }
}