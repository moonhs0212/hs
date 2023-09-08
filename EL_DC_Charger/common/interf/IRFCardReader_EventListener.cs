﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.interf
{
    public interface IRFCardReader_EventListener
    {
        void onReceive(string rfCardNumber);
        void onReceiveFailed(string result);
    }
}
