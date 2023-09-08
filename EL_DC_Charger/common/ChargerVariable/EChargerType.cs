using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.variable
{

    public enum EChargerType
    {
        [Description("비선택")]
        NONE,
        [Description("1채널 인증용")]
        CH1_CERT,
        [Description("1채널 비공용")]
        CH1_NOT_PUBLIC,
        [Description("1채널 공용")]
        CH1_PUBLIC,
        [Description("2채널 인증용")]
        CH2_CERT
    }
}
