using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.variable
{
    public enum EPlatform
    {
        [Description("선택없음")]
        NONE,
        [Description("OCTT 인증시험")]
        OCTT_CERTIFICATION,
        [Description("WEV")]
        WEV,
        [Description("소프트베리")]
        SOFTBERRY,
        [Description("나이스차저")]
        NICE_CHARGER

    }
}
