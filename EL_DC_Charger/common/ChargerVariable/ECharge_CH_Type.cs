using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.ChargerVariable
{
    public enum ECharge_CH_Type
    {
        [Description("좌측")]
        LEFT,
        [Description("우측")]
        RIGHT,
        [Description("공용")]
        ALL
    }
}
