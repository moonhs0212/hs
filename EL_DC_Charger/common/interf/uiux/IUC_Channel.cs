using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EL_DC_Charger.common.interf.uiux
{
    public interface IUC_Channel
    {
        UserControl getUserControl();
        int getChannelIndex();

        void initVariable();
        void updateView();
        
        void setText(int indexArray, string text);
        void setVisibility(int indexArray, bool visible);
    }
}
