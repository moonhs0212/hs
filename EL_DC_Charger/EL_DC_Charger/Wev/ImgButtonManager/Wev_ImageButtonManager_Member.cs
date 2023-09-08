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
    public class Wev_ImageButtonManager_Member : ImageButtonManager_Base
    {
        public Wev_ImageButtonManager_Member(PictureBox pb) : base(null, pb, Properties.Resources.img_btn_member_clicked, Properties.Resources.img_btn_member_normal)
        {

        }

    }
}