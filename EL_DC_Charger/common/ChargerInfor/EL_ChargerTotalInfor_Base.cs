
using EL_DC_Charger.ChargerVariable;
using EL_DC_Charger.common;
using EL_DC_Charger.common.application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.ChargerInfor
{
    public class EL_ChargerTotalInfor_Base : EL_Object_Channel_Base
    {
        public EL_ChargerTotalInfor_Base(EL_MyApplication_Base application, int channelIndex)
            : base(application, channelIndex)
        {
            
        }

        override public void initVariable()
        {
            
        }
    }
}
