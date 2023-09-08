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

    abstract public partial class ChargerUserControl_Main_Base : UserControl, IEL_Object_Base, IUC_Main, IUISetting_ChageUnit
    {

        protected EL_MyApplication_Base mApplication = null;
        public ChargerUserControl_Main_Base(EL_MyApplication_Base application)
        {
            mApplication = application;
        }

        public EL_MyApplication_Base getApplication()
        {
            return mApplication;
        }


        public abstract Panel getPanel_Main();
        public abstract UserControl getUserControl();

        public void initVariable()
        {
            throw new NotImplementedException();
        }

        public abstract void setBottombar_ProcessStep();
        public abstract void setBottombar_Weather();

        public void setChannelIndex(int channelIndex)
        {
            throw new NotImplementedException();
        }

        public abstract void setContent(int channelIndex, UserControl control);
        public abstract void setHomeBackBtn_Manager(IHomeBackBtn_Manager manager);
        public abstract void setPanel_Main_CustomUserControl(UserControl control);
        public abstract void setText(int indexArray, string text);
        public abstract void setUX_ChargeUnit(bool isMember, float chargeUnit);
        public abstract void setVisible_Button_Back(bool visible);
        public abstract void setVisible_Button_Home(bool visible);
    }
}
