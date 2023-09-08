using EL_DC_Charger.common;
using EL_DC_Charger.common.application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.infor
{
    public class OCPP_ChannelInfor : EL_Object_Channel_Base
    {
        public OCPP_ChannelInfor(EL_MyApplication_Base application, int channelIndex) : base(application, channelIndex)
        {
            
        }

        protected String mIdTag = null;
        public String getIdTag()
        {
            return mIdTag;
        }
        public void setIdTag(String idTag)
        {
            mIdTag = idTag;
        }


        public override void initVariable()
        {
            base.initVariable();
            mIdTag = null;
        }
    }
}
