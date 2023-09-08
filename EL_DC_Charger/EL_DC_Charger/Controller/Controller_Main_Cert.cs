
using EL_DC_Charger.Controller;
using EL_DC_Charger.common.application;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.ChargerVariable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.Controller
{
    public class Controller_Main_Cert : Controller_Base
    {
        public Controller_Main_Cert(EL_MyApplication_Base application) : base(application, 0)
        {
        }

        public override void process()
        {
            ///////////////////////////////////////////////////////////////
            switch (mMode)
            {
                ///////////////////////////////////////////////////////////////
                case CMODE_MAIN.MODE_BOOT_ON:
                    EL_DC_Charger_MyApplication.getInstance().getDataManager_CustomUC_Main().setView_MainPanel_Main();
                    EL_DC_Charger_MyApplication.getInstance().getDataManager_CustomUC_Main().setView_MainContent_Cert_ManualControl_Output();
                    setMode(CMODE_MAIN.MODE_BOOT_ON + 1);
                    break;
                case CMODE_MAIN.MODE_BOOT_ON + 1:
                    setMode(CMODE_MAIN.MODE_MAIN);
                    break;

                ///////////////////////////////////////////////////////////////
                case CMODE_MAIN.MODE_MAIN:
                    setMode(CMODE_MAIN.MODE_BOOT_ON + 1);
                    break;
                case CMODE_MAIN.MODE_MAIN + 1:
                    
                    break;
            }
        }
    }
}
