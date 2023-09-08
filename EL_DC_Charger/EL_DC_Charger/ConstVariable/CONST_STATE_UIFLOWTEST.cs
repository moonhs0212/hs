﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.ConstVariable
{
    public class CONST_STATE_UIFLOWTEST
    {
        public const int STATE_PREFARING = 0;
        public const int STATE_READY = STATE_PREFARING + 1000;

        public const int STATE_REMOTE_START_TRANSACTION = STATE_READY + 1000;

        public const int STATE_RESERVATION = STATE_REMOTE_START_TRANSACTION + 1000;

        public const int STATE_SELECT_CONNECTORTYPE = STATE_RESERVATION + 1000;
        public const int STATE_SELECT_MEMBERTYPE = STATE_SELECT_CONNECTORTYPE + 1000;

        public const int STATE_NONMEMBER_SELECT_PAYMENTTYPE = STATE_SELECT_MEMBERTYPE + 1000;

        public const int STATE_NONMEMBER_CARDDEVICE = STATE_NONMEMBER_SELECT_PAYMENTTYPE + 1000;

        public const int STATE_NONMEMBER_QRCODE = STATE_NONMEMBER_CARDDEVICE + 1000;

        
        public const int STATE_NONMEMBER_WAIT_CERTIFICATION = STATE_NONMEMBER_QRCODE + 1000;

        public const int STATE_WAIT_CARDTAG = STATE_NONMEMBER_WAIT_CERTIFICATION + 1000;

        public const int STATE_WAIT_RECEIVE_CHARGEUNIT = STATE_WAIT_CARDTAG + 1000;

        public const int STATE_WAIT_CONNECTCONNECTOR = STATE_WAIT_RECEIVE_CHARGEUNIT + 1000;

        public const int STATE_WAIT_STARTTRANSACTION = STATE_WAIT_CONNECTCONNECTOR + 1000;

        public const int STATE_CHARGING_PREPARE = STATE_WAIT_STARTTRANSACTION + 1000;

        public const int STATE_ERROR_BEFORE_CHARGING = STATE_CHARGING_PREPARE + 1000;

        public const int STATE_CHARGING = STATE_ERROR_BEFORE_CHARGING + 1000;
        public const int STATE_CHARGINGINFOR = STATE_CHARGING + 1000;
        public const int STATE_CHARGING_COMPLETE = STATE_CHARGINGINFOR + 1000;
        public const int STATE_WAIT_SEPERATE_CONNECTOR = STATE_CHARGING_COMPLETE + 1000;
        public const int STATE_WAIT_USECOMPLETE = STATE_WAIT_SEPERATE_CONNECTOR + 1000;

        public const int STATE_EMERGENCY = STATE_WAIT_USECOMPLETE + 1000;

        public const int STATE_DISABLE = STATE_EMERGENCY + 1000;
        public const int STATE_RESET = STATE_DISABLE + 1000;

        public const int STATE_RE_CHARGING = STATE_RESET + 1000;

    }
}
