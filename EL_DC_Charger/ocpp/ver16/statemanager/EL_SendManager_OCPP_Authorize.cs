using EL_DC_Charger.common.application;
using EL_DC_Charger.common.item;
using EL_DC_Charger.common.variable;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.ocpp.ver16.comm;
using EL_DC_Charger.ocpp.ver16.datatype;
using EL_DC_Charger.ocpp.ver16.interf;
using EL_DC_Charger.ocpp.ver16.packet;
using EL_DC_Charger.ocpp.ver16.packet.cp2csms;
using EL_DC_Charger.ocpp.ver16.platform.wev.datatype;
using EL_DC_Charger.ocpp.ver16.platform.wev.packet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.statemanager
{
    public class EL_SendManager_OCPP_Authorize : EL_SendManager_OCPP_Base
    {
        public EL_StateManager_OCPP_Channel mStateManager_Channel = null;

        public EL_SendManager_OCPP_Authorize(OCPP_Comm_Manager ocpp_comm_manager, int channelIndex, EL_StateManager_OCPP_Channel stateManager)
            : base(ocpp_comm_manager, channelIndex, false)
        {
            mStateManager_Channel = stateManager;
        }

        override public void initVariable()
        {
            bIsReceivePacket_CallResult = true;
        }

        public void sendOCPP_CP_Req_Authorize(String password)
        {
            switch (mApplication.getPlatform())
            {
                case EPlatform.WEV:
                    sendOCPP_CP_Req_Authorize_Wev(password);
                    break;
                default:
                    mStateManager_Channel.bIsCertificationSuccess = false;
                    mStateManager_Channel.bIsCertificationFailed = false;

                    Req_Authorize authorize = new Req_Authorize();

                    authorize.setRequiredValue(mStateManager_Channel.mCardNumber_Read_Temp);

                    setSendPacket_Call_CP(
                        authorize.GetType().Name.Split(new String[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1], //authorize.GetType().Name.Split("_")[1], 
                        JsonConvert.SerializeObject(authorize, EL_MyApplication_Base.mJsonSerializerSettings)); //mGson.toJson(authorize, authorize.getClass()));
                    break;
            }
        }

        public void sendOCPP_CP_Req_Authorize()
        {
            mStateManager_Channel.bIsCertificationSuccess = false;
            mStateManager_Channel.bIsCertificationFailed = false;

            Req_Authorize authorize = new Req_Authorize();
            switch (mApplication.getPlatform())
            {
                case EPlatform.WEV:
                    authorize.setRequiredValue_Wev_CardTag(mChannelIndex, mStateManager_Channel.mCardNumber_Read_Temp);                    
                    break;
                default:                    

                    authorize.setRequiredValue(mStateManager_Channel.mCardNumber_Read_Temp);                    
                    break;
            }
            setSendPacket_Call_CP(
                        authorize.GetType().Name.Split(new String[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1], //authorize.GetType().Name.Split("_")[1], 
                        JsonConvert.SerializeObject(authorize, EL_MyApplication_Base.mJsonSerializerSettings)); //mGson.toJson(authorize, authorize.getClass()));
        }

        public void sendOCPP_CP_Req_Authorize_Wev(String password)
        {
            mStateManager_Channel.bIsCertificationSuccess = false;
            mStateManager_Channel.bIsCertificationFailed = false;

            WevReq_Authorize authorize = new WevReq_Authorize();

            if (password == null)
                authorize.setRequiredValue_Wev_CardTag(mChannelIndex, mStateManager_Channel.mCardNumber_Read_Temp);
            else
                authorize.setRequiredValue_Wev_CardNumber(mChannelIndex, mStateManager_Channel.mCardNumber_Read_Temp, password);

            setSendPacket_Call_CP(
                authorize.GetType().Name.Split(new String[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1], //authorize.GetType().Name.Split("_")[1], 
                JsonConvert.SerializeObject(authorize, EL_MyApplication_Base.mJsonSerializerSettings)); //mGson.toJson(authorize, authorize.getClass()));
        }

        public void sendOCPP_CP_Req_Authorize(IOCPP_ConfAuthorize_Listener listener)
        {
            mStateManager_Channel.bIsCertificationSuccess = false;
            mStateManager_Channel.bIsCertificationFailed = false;

            setOCPP_Authorize_Listener(listener);
            Req_Authorize authorize = new Req_Authorize();
            authorize.setRequiredValue(mStateManager_Channel.mCardNumber_Read_Temp);

            setSendPacket_Call_CP(
                authorize.GetType().Name.Split(new String[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1], //authorize.GetType().Name.split("_")[1], 
                JsonConvert.SerializeObject(authorize, EL_MyApplication_Base.mJsonSerializerSettings)); //mGson.toJson(authorize, authorize.getClass()));
        }

        protected IOCPP_ConfAuthorize_Listener mAuthorize_Listener = null;
        public void setOCPP_Authorize_Listener(IOCPP_ConfAuthorize_Listener listener)
        {
            mAuthorize_Listener = listener;
        }

        override protected String getLogTag()
        {
            return null;
        }


        override protected void processReceivePacket_CallResult(EL_OCPP_Packet_Wrapper sendPacket, JArray receivePacket)
        {
            EL_Time time_Generate = new EL_Time();
            time_Generate.setTime();
            String time_String = time_Generate.getDateTime_DB();
            if (sendPacket.mActionName.Equals(EOCPP_Action_CP_Call.Authorize.ToString()))
            {
                //try
                //{
                Req_Authorize data_req = null;
                data_req = JsonConvert.DeserializeObject<Req_Authorize>(((JObject)sendPacket.mPacket[3]).ToString());
                //mGson.fromJson(((JSONObject)sendPacket.mPacket.get(3)).toString(), Req_Authorize.class);
                Conf_Authorize data = JsonConvert.DeserializeObject<Conf_Authorize>(((JObject)receivePacket[2]).ToString());
                //mGson.fromJson(((JSONObject)receivePacket.get(2)).toString(), Conf_Authorize.class);
                mApplication.getManager_SQLite_Setting_OCPP().getTable_AuthCache()
                            .updateOrInsertIdTagInfo_Authorize(data_req.idTag, data.idTagInfo, time_String);


                if (data.moreAuthorizeConf != null)
                    EL_DC_Charger_MyApplication.getInstance().CurrentAmount = float.Parse(data.moreAuthorizeConf.currentUnitPrice);

                //switch (data.moreAuthorizeConf.operatorType)
                //{
                //    case "ER":
                //        break;
                //    case "ME":                        
                //        break;
                //}



                if (mAuthorize_Listener != null)
                {
                    mAuthorize_Listener.onAuthorize(data);
                }
                else
                {
                    onAuthorize(data);
                }


                //} catch (JSONException e)
                //{
                //    e.printStackTrace();
                //}
            }


        }


        override protected void processReceivePacket_CallError(EL_OCPP_Packet_Wrapper sendPacket, JArray receivePacket)
        {

        }



        public Conf_Authorize mOCPP_CSMS_Conf_Authorize = null;
        public Conf_Authorize getOCPP_CSMS_Conf_Authorize()
        {
            return mOCPP_CSMS_Conf_Authorize;
        }
        public void setOCPP_CSMS_Conf_Authorize(Conf_Authorize conf)
        {
            mOCPP_CSMS_Conf_Authorize = conf;
        }


        public void onAuthorize(Conf_Authorize packet)
        {
            mOCPP_CSMS_Conf_Authorize = packet;

            switch (packet.idTagInfo.status)
            {
                case AuthorizationStatus.Accepted:
                    mStateManager_Channel.mOCPP_Conf_Authorize = packet;
                    mStateManager_Channel.bIsCertificationSuccess = true;
                    mStateManager_Channel.bIsCertificationFailed = false;
                    break;
                case AuthorizationStatus.Blocked:
                case AuthorizationStatus.Expired:
                case AuthorizationStatus.Invalid:
                case AuthorizationStatus.ConcurrentTx:
                default:
                    mStateManager_Channel.bIsCertificationFailed = true;
                    mStateManager_Channel.bIsCertificationSuccess = false;
                    break;
            }
        }


    }

}
