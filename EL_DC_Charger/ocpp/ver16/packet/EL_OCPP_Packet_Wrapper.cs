using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.packet
{
    public class EL_OCPP_Packet_Wrapper
    {
        public JArray mPacket = null;
        public String mActionName = null;
        public String mUniqueId = null;



        public EL_OCPP_Packet_Wrapper(String actionName, String uniqueId, JArray packet)
        {
            mPacket = packet;
            mActionName = actionName;
            mUniqueId = uniqueId;
        }

        public bool bIsNeedRemove = false;
        public bool setReceiveData(JArray packet)
        {
            //try
            //{
                if (packet[1].ToString().Equals(mUniqueId))
                {
                    bIsNeedRemove = true;
                    return true;
                }
            //}
            //catch (JSONException e)
            //{
            //    e.printStackTrace();
            //}
            return false;
        }



    }
}
