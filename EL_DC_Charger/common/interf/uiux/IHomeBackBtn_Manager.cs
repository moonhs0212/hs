using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.interf.uiux
{
    public interface IHomeBackBtn_Manager
    {
        void setVisibility_HomeButton(bool visible);
        void setVisibility_BackButton(bool visible);
    }
}
