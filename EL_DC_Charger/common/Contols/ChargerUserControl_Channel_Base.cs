using EL_DC_Charger.common.application;
using EL_DC_Charger.common.interf;
using EL_DC_Charger.common.interf.uiux;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EL_DC_Charger.common.Contols
{
    abstract public partial class ChargerUserControl_Channel_Base : UserControl, IUC_Channel, IUISetting_ChageUnit
    {
        protected EL_MyApplication_Base mAPplication = null;
        protected int mChannelIndex = 0;
        public ChargerUserControl_Channel_Base(EL_MyApplication_Base application, int channelIndex)
        {
            mAPplication = application;
            mChannelIndex = channelIndex;
        }

        public int getChannelIndex()
        {
            return mChannelIndex;
        }
        public UserControl getUserControl()
        {
            return this;
        }
        public abstract void initVariable();
        public abstract void setText(int indexArray, string text);

        abstract public void setUX_ChargeUnit(bool isMember, float chargeUnit);

        public abstract void setVisibility(int indexArray, bool visible);
        public abstract void updateView();
    }
}
