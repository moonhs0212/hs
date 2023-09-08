using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.interf.uiux
{
    public interface IUC_Setting_Child : IUC_Channel
    {
        string getTitle();
        string getSubTitle();
    }
}
