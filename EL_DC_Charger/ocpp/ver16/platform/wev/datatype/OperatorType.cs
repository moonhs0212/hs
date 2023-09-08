using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.platform.wev.datatype
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OperatorType
    {
        NM, //비회원
        SB, //소프트베리
        ER, //이엘일렉트릭
        ME  //환경부
    }

}
