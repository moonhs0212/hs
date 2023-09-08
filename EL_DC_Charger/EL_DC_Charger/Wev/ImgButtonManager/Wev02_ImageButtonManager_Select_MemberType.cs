using EL_DC_Charger.common.ImgButtonManager;
using EL_DC_Charger.common.interf;
using EL_DC_Charger.EL_DC_Charger.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EL_DC_Charger.EL_DC_Charger.Wev.ImgButtonManager
{
    
    public class Wev02_ImageButtonManager_Select_MemberType : ImageButtonManager_Base
    {
        public Wev02_ImageButtonManager_Select_MemberType(PictureBox pb) : base(null, pb, Properties.Resources.wev02_horizontal_img_btn_membertype_normal, Properties.Resources.wev02_horizontal_img_btn_membertype_clicked)
        {
            
        }

        
    }
}

