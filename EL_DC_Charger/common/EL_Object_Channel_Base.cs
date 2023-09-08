using EL_DC_Charger.common.application;
using EL_DC_Charger.EL_DC_Charger.ChargerInfor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common
{
    public class EL_Object_Channel_Base : IEL_Object_Channel_Base
    {
        protected EL_MyApplication_Base mApplication = null;
        protected EL_ChannelTotalInfor_Base mChannelTotalInfor = null;
        protected int mChannelIndex = 0;
        public EL_Object_Channel_Base(EL_ChannelTotalInfor_Base channelTotalInfor)
            : this(channelTotalInfor.getApplication(), channelTotalInfor.getChannelIndex())
        {
            
        }
        public EL_Object_Channel_Base(EL_MyApplication_Base application, int channelIndex)
        {
            mApplication = application;
            
            mChannelIndex = channelIndex;
            if(channelIndex > 0)
                mChannelTotalInfor = mApplication.getChannelTotalInfor(channelIndex);
        }

        public EL_MyApplication_Base getApplication()
        {
            return mApplication;
        }
        virtual public void initVariable()
        {
            
        }


        
        public int getChannelIndex()
        {
            return mChannelIndex;
        }

        
        public void setChannelIndex(int channelIndex)
        {
            mChannelIndex = channelIndex;
        }
    }
}
