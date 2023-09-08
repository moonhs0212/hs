using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.variable
{
    public enum EPlatformOperator
    {
        [Description("선택없음")]
        NONE,
        [Description("WEV")]
        WEV,
        [Description("한국전자금융")]
        NICETCM,
        [Description("볼타주식회사")]
        VOLTA,
        [Description("OCPP테스트")]
        OCPPTEST
    }

    
}
