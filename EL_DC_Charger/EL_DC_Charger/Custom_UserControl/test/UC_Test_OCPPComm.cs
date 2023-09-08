using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.ocpp.ver16.packet.cp2csms;
using EL_DC_Charger.ocpp.ver16.statemanager;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.test
{
    public partial class UC_Test_OCPPComm : UserControl
    {
        public UC_Test_OCPPComm()
        {
            InitializeComponent();
        }





        private void button_bootnotification_Click_1(object sender, EventArgs e)
        {
            Conf_BootNotification conf_BootNotification = new Conf_BootNotification();
            conf_BootNotification.status = ocpp.ver16.datatype.RegistrationStatus.Accepted;            
            conf_BootNotification.interval = 10;
            ((EL_StateManager_OCPP_Main)EL_DC_Charger_MyApplication.getInstance().getStateManager_Main()).mOCPP_CSMS_Conf_BootNotification = conf_BootNotification;
            ((EL_StateManager_OCPP_Main)EL_DC_Charger_MyApplication.getInstance().getStateManager_Main()).bOCPP_IsReceivePacket_CallResult_BootNotification = true;
        }

        private void button_starttransaction_Click(object sender, EventArgs e)
        {
            Conf_StartTransaction conf = new Conf_StartTransaction();
            conf.idTagInfo = new ocpp.ver16.datatype.IdTagInfo();
            conf.idTagInfo.status = ocpp.ver16.datatype.AuthorizationStatus.Accepted;
            conf.transactionId = 3;

            JArray jarray = new JArray();
            jarray.Add("3");
            jarray.Add("aaaaaaaaaaaaaaaaaaaaaaa");
            jarray.Add(JsonConvert.SerializeObject(conf));
            //conf.
            //((EL_StateManager_OCPP_Channel)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getStateManager_Channel()).StartTransaction = conf_BootNotification;
            ((EL_StateManager_OCPP_Channel)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getStateManager_Channel())
                .mSendManager_OCPP_ChargingReq.setOCPP_CSMS_Conf_StartTransaction(conf, jarray);
        }

        private void button_stoptransaction_Click(object sender, EventArgs e)
        {
            Conf_StopTransaction conf = new Conf_StopTransaction();
            conf.idTagInfo = new ocpp.ver16.datatype.IdTagInfo();
            conf.idTagInfo.status = ocpp.ver16.datatype.AuthorizationStatus.Accepted;
            
            JArray jarray = new JArray();
            jarray.Add("3");
            jarray.Add("aaaaaaaaaaaaaaaaaaaaaaa");
            jarray.Add(JsonConvert.SerializeObject(conf));
            //conf.
            //((EL_StateManager_OCPP_Channel)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getStateManager_Channel()).StartTransaction = conf_BootNotification;
            ((EL_StateManager_OCPP_Channel)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getStateManager_Channel())
                .mSendManager_OCPP_ChargingReq.setOCPP_Conf_StopTransaction(conf, jarray);
        }

        private void button_authorize_Click_1(object sender, EventArgs e)
        {
            Conf_Authorize conf = new Conf_Authorize();
            conf.idTagInfo = new ocpp.ver16.datatype.IdTagInfo();
            conf.idTagInfo.status = ocpp.ver16.datatype.AuthorizationStatus.Accepted;
            //conf.idTagInfo = 3;
            //conf.
            //((EL_StateManager_OCPP_Channel)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getStateManager_Channel()).StartTransaction = conf_BootNotification;
            ((EL_StateManager_OCPP_Channel)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getStateManager_Channel()).bIsCertificationSuccess = true;
        }

        private void button_statusnotification_Click(object sender, EventArgs e)
        {

        }
    }
}
