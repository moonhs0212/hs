using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace EL_DC_Charger.Interface_Common
{
    public interface IMainForm
    {
        Panel getPanel_Main();
        Form getForm_Main();
        void initVariable();

        void setPanel_Main_CustomUserControl(UserControl control);
    }
}
