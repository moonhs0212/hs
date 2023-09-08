using EL_DC_Charger.common.ImgButtonManager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EL_DC_Charger.EL_DC_Charger.Wev.ImgButtonManager
{
    public class Wev_ImageButtonManager_ChargingStop : ImageButtonManager_Base
    {
        public Wev_ImageButtonManager_ChargingStop(PictureBox pb) : base(null, pb, Properties.Resources.wev_img_btn_chargingstop_normal, Properties.Resources.wev_img_btn_chargingstop_clicked)
        {

        }

    }
}
