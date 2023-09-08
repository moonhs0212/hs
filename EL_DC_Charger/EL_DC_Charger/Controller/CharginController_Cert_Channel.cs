using EL_DC_Charger.Controller;
using EL_DC_Charger.common.application;
using EL_DC_Charger.common.chargingcontroller;
using EL_DC_Charger.EL_DC_Charger.ChargerVariable;
using EL_DC_Charger.Interface_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EL_DC_Charger.common.interf;

namespace EL_DC_Charger.EL_DC_Charger.Controller
{
    public class CharginController_Cert_Channel : ChargingController_Base, IRFCardReader_EventListener
    {
        public CharginController_Cert_Channel(EL_MyApplication_Base application, int channelIndex) : base(application, channelIndex)
        {
            setTime_NextTime(60);
        }

        public void onReceive(string rfCardNumber)
        {
            
        }

        public void onReceiveFailed(string result)
        {
            
        }

        public override void process()
        {
            switch (mMode)
            {
                ///////////////////////////////////////////////////////////////
                case CMODE_CERT_CHANNEL.MODE_BOOT_ON:
                    setMode(CMODE_CERT_CHANNEL.MODE_BOOT_ON + 1);
                    break;
                case CMODE_CERT_CHANNEL.MODE_BOOT_ON + 1:
                    setMode(CMODE_CERT_CHANNEL.MODE_PREPARE);
                    break;
                ///////////////////////////////////////////////////////////////
                case CMODE_CERT_CHANNEL.MODE_PREPARE:
                    setMode(CMODE_CERT_CHANNEL.MODE_PREPARE + 1);
                    break;
                case CMODE_CERT_CHANNEL.MODE_PREPARE + 1:
                    setMode(CMODE_CERT_CHANNEL.MODE_MAIN);
                    break;
                ///////////////////////////////////////////////////////////////
                case CMODE_CERT_CHANNEL.MODE_MAIN:
                    setMode(CMODE_CERT_CHANNEL.MODE_MAIN + 1);
                    break;
                case CMODE_CERT_CHANNEL.MODE_MAIN + 1:
                    setMode(CMODE_CERT_CHANNEL.MODE_CHARGING);
                    break;
                ///////////////////////////////////////////////////////////////
                case CMODE_CERT_CHANNEL.MODE_CHARGING:
                    setMode(CMODE_CERT_CHANNEL.MODE_CHARGING + 1);
                    break;
                case CMODE_CERT_CHANNEL.MODE_CHARGING + 1:
                    setMode(CMODE_CERT_CHANNEL.MODE_CHARGING_COMPLETE);
                    break;
                ///////////////////////////////////////////////////////////////
                case CMODE_CERT_CHANNEL.MODE_CHARGING_COMPLETE:
                    setMode(CMODE_CERT_CHANNEL.MODE_CHARGING_COMPLETE + 1);
                    break;
                case CMODE_CERT_CHANNEL.MODE_CHARGING_COMPLETE + 1:
                    setMode(CMODE_CERT_CHANNEL.MODE_MAIN);
                    break;
                ///////////////////////////////////////////////////////////////
                case CMODE_CERT_CHANNEL.MODE_EMERGENCY:
                    setMode(CMODE_CERT_CHANNEL.MODE_EMERGENCY + 1);
                    break;
                case CMODE_CERT_CHANNEL.MODE_EMERGENCY + 1:
                    setMode(CMODE_CERT_CHANNEL.MODE_MAIN);
                    break;
            }
            
    }

        protected override bool process_EmergencyButton()
        {
            throw new NotImplementedException();
        }
    }
}
