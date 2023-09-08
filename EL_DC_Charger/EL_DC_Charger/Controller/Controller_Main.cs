using EL_DC_Charger.Controller;
using EL_DC_Charger.Manager;
using EL_DC_Charger.common.application;
using EL_DC_Charger.common.Manager;
using EL_DC_Charger.EL_DC_Charger.ChargerVariable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.Controller
{
    public class Controller_Main : Controller_Base
    {
        public Controller_Main(EL_MyApplication_Base application) : base(application, 0)
        {
        }

        public override void process()
        {

            ///////////////////////////////////////////////////////////////
            switch (mMode)
            {
                ///////////////////////////////////////////////////////////////
                case CMODE_MAIN.MODE_BOOT_ON:
                    setMode(CMODE_MAIN.MODE_BOOT_ON + 1);
                    break;
                case CMODE_MAIN.MODE_BOOT_ON + 1:
                    setMode(CMODE_MAIN.MODE_FIRST_SETTING);
                    break;

                ///////////////////////////////////////////////////////////////
                case CMODE_MAIN.MODE_FIRST_SETTING:
                    setMode(CMODE_MAIN.MODE_FIRST_SETTING + 1);
                    break;
                case CMODE_MAIN.MODE_FIRST_SETTING + 1:
                    setMode(CMODE_MAIN.MODE_SELF_DIAGNOSIS_MAIN);
                    break;

                ///////////////////////////////////////////////////////////////
                case CMODE_MAIN.MODE_SELF_DIAGNOSIS_MAIN:
                    setMode(CMODE_MAIN.MODE_SELF_DIAGNOSIS_MAIN + 1);
                    break;
                case CMODE_MAIN.MODE_SELF_DIAGNOSIS_MAIN + 1:
                    setMode(CMODE_MAIN.MODE_SELF_DIAGNOSIS_MAIN_TEST);
                    break;

                ///////////////////////////////////////////////////////////////
                case CMODE_MAIN.MODE_SELF_DIAGNOSIS_MAIN_TEST:
                    setMode(CMODE_MAIN.MODE_SELF_DIAGNOSIS_MAIN_TEST + 1);
                    break;
                case CMODE_MAIN.MODE_SELF_DIAGNOSIS_MAIN_TEST + 1:
                    setMode(CMODE_MAIN.MODE_PREPARE);
                    break;

                ///////////////////////////////////////////////////////////////
                case CMODE_MAIN.MODE_SELF_DIAGNOSIS_AUTO:
                    setMode(CMODE_MAIN.MODE_SELF_DIAGNOSIS_AUTO + 1);
                    break;
                case CMODE_MAIN.MODE_SELF_DIAGNOSIS_AUTO + 1:
                    setMode(CMODE_MAIN.MODE_PREPARE);
                    break;

                ///////////////////////////////////////////////////////////////
                case CMODE_MAIN.MODE_SELF_DIAGNOSIS_MANUAL:
                    setMode(CMODE_MAIN.MODE_SELF_DIAGNOSIS_MANUAL + 1);
                    break;
                case CMODE_MAIN.MODE_SELF_DIAGNOSIS_MANUAL + 1:
                    setMode(CMODE_MAIN.MODE_PREPARE);
                    break;

                ///////////////////////////////////////////////////////////////
                case CMODE_MAIN.MODE_PREPARE:
                    setMode(CMODE_MAIN.MODE_PREPARE + 1);
                    break;
                case CMODE_MAIN.MODE_PREPARE + 1:
                    setMode(CMODE_MAIN.MODE_MAIN);
                    break;

                ///////////////////////////////////////////////////////////////
                case CMODE_MAIN.MODE_MAIN:
                    setMode(CMODE_MAIN.MODE_MAIN + 1);
                    break;
                case CMODE_MAIN.MODE_MAIN + 1:
                    setMode(CMODE_MAIN.MODE_RESTART_PROGRAM); 
                    break;

                ///////////////////////////////////////////////////////////////
                case CMODE_MAIN.MODE_RESTART_PROGRAM:
                    setMode(CMODE_MAIN.MODE_RESTART_PROGRAM + 1);
                    break;
                case CMODE_MAIN.MODE_RESTART_PROGRAM + 1:
                    EL_Manager_Application.restartApplication();
                    break;

                ///////////////////////////////////////////////////////////////
                case CMODE_MAIN.MODE_RESTART_SYSTEM:
                    setMode(CMODE_MAIN.MODE_RESTART_SYSTEM + 1);
                    break;
                case CMODE_MAIN.MODE_RESTART_SYSTEM + 1:
                    EL_Manager_Application.finishApplication();
                    EL_Manager_Application.restartSystem();
                    break;


            }
        }
    }
}
