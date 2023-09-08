using EL_DC_Charger.Interface_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.interf
{
    public interface ICardDevice_Manager : IRFCardReader_Manager
    {
        void setCardDevice_Listener(IRFCardReader_EventListener listener);

        bool isInclude_RFCardReader();
        bool isCanUse_PartCancel();
        void setCommand_Pay_First(int amount);
        void setCommand_Pay_Cancel(int amount_Cancel, Object obj);

        void setCommand_Pay_Cancel(Object obj);

        bool isCommand_Complete();
        bool isCommand_ErrorOccured();

        string getErrorCode_String();
        int getErrorCode();
    }
}
