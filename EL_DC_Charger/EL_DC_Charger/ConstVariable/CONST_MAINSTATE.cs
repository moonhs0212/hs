using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.ConstVariable
{
    public class CONST_MAINSTATE
    {
        public const int INIT = 0;

        public const int BOOT_NOTIFICATION = INIT + 100;


        public const int MAIN = BOOT_NOTIFICATION + 100;


        public const int STATE_DISABLE = MAIN + 1000;
        public const int REBOOT = STATE_DISABLE + 100;


    }
}
