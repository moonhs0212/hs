using EL_DC_Charger.ChargerInfor;
using EL_DC_Charger.common.application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.SerialPorts.AMI
{
    public class ODHitec_AMI_PacketManager : EL_AMI_PacketManager_Base
    {
        public ODHitec_AMI_PacketManager(EL_MyApplication_Base application, int channelIndex) : base(application, channelIndex)
        {
        }

        public override bool isConnected_Comm()
        {
            return true;
        }
    }
}
