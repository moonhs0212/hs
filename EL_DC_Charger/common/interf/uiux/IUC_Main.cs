using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EL_DC_Charger.common.interf.uiux
{
    public interface IUC_Main
    {
        Panel getPanel_Main();

        UserControl getUserControl();

        void setContent(int channelIndex, UserControl control);


        void setBottombar_Weather();

        void setBottombar_ProcessStep();

        void setPanel_Main_CustomUserControl(UserControl control);

        void setVisible_Button_Back(bool visible);
        void setVisible_Button_Home(bool visible);


        void setText(int indexArray, string text);

        void setHomeBackBtn_Manager(IHomeBackBtn_Manager manager);
    }
}
