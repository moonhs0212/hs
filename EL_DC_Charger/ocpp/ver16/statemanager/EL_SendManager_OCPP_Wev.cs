using common.Database;
using EL_DC_Charger.common.application;
using EL_DC_Charger.common.ChargerInfor;
using EL_DC_Charger.common.interf;
using EL_DC_Charger.common.item;
using EL_DC_Charger.common.Manager;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.ConstVariable;
using EL_DC_Charger.EL_DC_Charger.statemanager;
using EL_DC_Charger.ocpp.ver16.comm;
using EL_DC_Charger.ocpp.ver16.database;
using EL_DC_Charger.ocpp.ver16.datatype;
using EL_DC_Charger.ocpp.ver16.packet;
using EL_DC_Charger.ocpp.ver16.packet.common_action;
using EL_DC_Charger.ocpp.ver16.platform.wev.datatype;
using EL_DC_Charger.ocpp.ver16.platform.wev.packet;
using EL_DC_Charger.ocpp.ver16.platform.wev.statemanager;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EL_DC_Charger.ocpp.ver16.statemanager
{
    public class EL_SendManager_OCPP_Wev : EL_SendManager_OCPP_Base
    {
        protected Wev_StateManager_OCPP_Channel mStateManager_Channel = null;
        public OCPP_Manager_Table_Setting mTable_Setting = null;
        public EL_SendManager_OCPP_Wev(OCPP_Comm_Manager ocpp_comm_manager, int channelIndex, Wev_StateManager_OCPP_Channel stateManager)
            : base(ocpp_comm_manager, channelIndex, true)
        {
            mStateManager_Channel = stateManager;
            mTable_Setting = (OCPP_Manager_Table_Setting)EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting_OCPP().getTable_Setting(0);
        }

        override protected String getLogTag()
        {
            return "OCPP_Wev";
        }


        override protected void processReceivePacket_CallResult(EL_OCPP_Packet_Wrapper sendPacket, JArray receivePacket)
        {
            EL_Time time_Generate = new EL_Time();
            time_Generate.setTime();
            String time_String = time_Generate.getDateTime_DB();

            if (sendPacket.mActionName.Equals(EOCPP_Action_CP_Call.DataTransfer.ToString()))
            {
                JObject receivePayload = (JObject)receivePacket[2];
                String messageId = (((JObject)sendPacket.mPacket[3])["messageId"]).ToString();   //getString("messageId");

                //operatorType 타입이 의도한대로 들어오지않아 sendPacket에서 가져옴.
                string data = (((JObject)sendPacket.mPacket[3])["data"]).ToString();
                JObject jobjOpType = JObject.Parse(data);
                ////////////////////////////////////////////////////////////////////

                Conf_DataTransfer confDataTranfer = JsonConvert.DeserializeObject<Conf_DataTransfer>(receivePayload.ToString()); //mGson.fromJson(receivePayload.toString(), Conf_DataTransfer.class);

                if (messageId.Equals("NPQ1"))
                {

                    Conf_NPQ1 npq1 = JsonConvert.DeserializeObject<Conf_NPQ1>(confDataTranfer.data); //mGson.fromJson(confDataTranfer.data, Conf_NPQ1.class);

                    mStateManager_Channel.wev_setNonmember_Receive_NPQ1_Conf(npq1);

                }
                else if (messageId.Equals("NPS1"))
                {
                    Conf_NPS1 confNPS1 = JsonConvert.DeserializeObject<Conf_NPS1>(confDataTranfer.data);//mGson.fromJson(receivePayload.toString(), Conf_NPS1.class);
                }
                else if (messageId.Equals("NPS2"))
                {
                    Conf_NPS2 confNPS2 = JsonConvert.DeserializeObject<Conf_NPS2>(confDataTranfer.data); //mGson.fromJson(confDataTranfer.data, Conf_NPS2.class);
                }
                else if (messageId.Equals("CPT1"))
                {
                    //공개키 요청
                    Conf_CPT1 confCPT1 = JsonConvert.DeserializeObject<Conf_CPT1>(confDataTranfer.data); //mGson.fromJson(confDataTranfer.data, Conf_CPT1.class);
                }
                else if (messageId.Equals("CPT2"))
                {
                    Conf_CPT2 confCPT2 = JsonConvert.DeserializeObject<Conf_CPT2>(confDataTranfer.data); //mGson.fromJson(confDataTranfer.data, Conf_CPT2.class);
                                                                                                         //대칭키 전달
                }
                else if (messageId.Equals("CTP1"))
                {
                    Req_DataTransfer req_dataTransfer = JsonConvert.DeserializeObject<Req_DataTransfer>(sendPacket.mPacket[3].ToString());
                    Req_CTP1 req_ctp1 = JsonConvert.DeserializeObject<Req_CTP1>(req_dataTransfer.data);


                    if (confDataTranfer.status == DataTransferStatus.Accepted)
                    {
                        //단가 정보 요청
                        Conf_CTP1 confCTP1 = JsonConvert.DeserializeObject<Conf_CTP1>(confDataTranfer.data); //mGson.fromJson(confDataTranfer.data, Conf_CTP1.class);
                        List<UnitPriceTable> unitPriceTable = confCTP1.unitPriceTables;




                        if (mChargeUnit_ChangeListener == null)
                        {
                            if (req_ctp1.connectorId > 0)
                            {
                                ((EL_Manager_SQLite_Setting)mApplication.getManager_SQLite_Setting()).getManager_Table_ChargeUnit()
                                        .addChargeUnit(req_ctp1.connectorId, req_ctp1.operatorType, unitPriceTable);

                                mChannelTotalInfor = mApplication.getChannelTotalInfor(mChannelIndex);
                                switch (req_ctp1.operatorType)
                                {
                                    case "ER":
                                        setCurrentChargeUnit(mChannelTotalInfor.mChargingCharge.getChargeUnit_Member(),
                                                req_ctp1.operatorType, unitPriceTable);
                                        break;
                                    case "NM":
                                        setCurrentChargeUnit(mChannelTotalInfor.mChargingCharge.getChargeUnit_Nonmember(),
                                                req_ctp1.operatorType, unitPriceTable);
                                        break;
                                }

                            }
                        }
                        else
                        {
                            mApplication.getManager_SQLite_Setting().getManager_Table_ChargeUnit()
                                .addChargeUnit(req_ctp1.connectorId, req_ctp1.operatorType, unitPriceTable);
                            mChargeUnit_ChangeListener.onChange_ChargeUnit(req_ctp1.connectorId, req_ctp1.operatorType, unitPriceTable);
                        }
                        EL_DC_Charger_MyApplication.getInstance().getDataManager_CustomUC_Main().mUC_Touch_Screen[0].updateView();
                    }
                    else
                    {
                        if (req_ctp1.connectorId > 0)
                        {
                            mStateManager_Channel.moveToError(Const_ErrorCode.CODE_0012_SERVER_CERTIFICATION_ERROR,
                                    Reason.Other,
                                    ChargePointStatus.Faulted);
                        }
                    }
                }
            }
        }
        public void setCurrentChargeUnit(EL_Manager_ChargeUnit chargeUnit, string operatorType, List<UnitPriceTable> unitPriceTable)
        {
            chargeUnit.setMemberType(operatorType);

            int index = -1;
            DateTime toDay = DateTime.Now;
            List<DateTime> dateArray = new List<DateTime>();
            for (int i = 0; i < unitPriceTable.Count; i++)
            {
                string startDate = unitPriceTable[i].startDate;
                dateArray.Add(DateTime.Parse(startDate));
            }

            DateTime closestDate = DateTime.MinValue;
            long minDifference = long.MaxValue;
            int countIndex = -1;
            for (int i = 0; i < dateArray.Count; i++)
            {
                DateTime date = dateArray[i];
                long difference = Math.Abs((long)(toDay.Date - date.Date).TotalDays);
                if (difference < minDifference)
                {
                    minDifference = difference;
                    closestDate = date;
                    countIndex = i;
                    break;
                }
            }
            if (countIndex >= 0)
            {
                chargeUnit.setStartDate(unitPriceTable[countIndex].startDate);
                List<string> unitList = unitPriceTable[countIndex].price;
                for (int i = 0; i < unitList.Count; i++)
                {
                    chargeUnit.setChargeUnit(i, EL_Manager_Conversion.getDouble(unitList[i]));
                }
            }
        }

        public void setChargeUnit_ChangeListener(IChargeUnit_ChangeListener chargeUnit_ChangeListener)
        {
            mChargeUnit_ChangeListener = chargeUnit_ChangeListener;
        }
        protected IChargeUnit_ChangeListener mChargeUnit_ChangeListener = null;

        override protected void processReceivePacket_CallError(EL_OCPP_Packet_Wrapper sendPacket, JArray receivePacket)
        {

        }

        public void sendOCPP_CP_Req_NPQ1_Req()
        {
            Req_DataTransfer dataTransfer = new Req_DataTransfer();
            dataTransfer.messageId = "NPQ1";
            //        dataTransfer.vendorId = "";//mTable_Setting.getSettingData(CONST_OCPP_Setting.infor_chargePointVendor.getValue());
            Req_NPQ1 npq1_Req = new Req_NPQ1();
            npq1_Req.setRequiredValue(mChannelIndex);

            dataTransfer.data = JsonConvert.SerializeObject(npq1_Req, EL_MyApplication_Base.mJsonSerializerSettings);

            String jsonText = JsonConvert.SerializeObject(dataTransfer, EL_MyApplication_Base.mJsonSerializerSettings);
            String actionName = dataTransfer.GetType().Name.Split(new String[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1];

            addReq_By_PayloadString(
                    actionName,
                    jsonText);
        }




        public void sendOCPP_CP_Req_CTP1_Req(OperatorType type)
        {
            Req_DataTransfer dataTransfer = new Req_DataTransfer();
            dataTransfer.messageId = "CTP1";
            //        dataTransfer.vendorId = "";//mTable_Setting.getSettingData(CONST_OCPP_Setting.infor_chargePointVendor.getValue());
            Req_CTP1 ctp1_Req = new Req_CTP1();
            ctp1_Req.setRequiredValue(mChannelIndex, type.ToString());

            dataTransfer.data = JsonConvert.SerializeObject(ctp1_Req, EL_MyApplication_Base.mJsonSerializerSettings);

            String jsonText = JsonConvert.SerializeObject(dataTransfer, EL_MyApplication_Base.mJsonSerializerSettings);
            String actionName = dataTransfer.GetType().Name.Split(new String[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1];
            addReq_By_PayloadString(
                    actionName,
                    jsonText);

        }
        public void sendOCPP_CP_Req_CTP1_Req(String type)
        {
            Req_DataTransfer dataTransfer = new Req_DataTransfer();
            dataTransfer.messageId = "CTP1";
            //        dataTransfer.vendorId = "";//mTable_Setting.getSettingData(CONST_OCPP_Setting.infor_chargePointVendor.getValue());
            Req_CTP1 ctp1_Req = new Req_CTP1();
            ctp1_Req.setRequiredValue(mChannelIndex, type);

            dataTransfer.data = JsonConvert.SerializeObject(ctp1_Req, EL_MyApplication_Base.mJsonSerializerSettings);

            String jsonText = JsonConvert.SerializeObject(dataTransfer, EL_MyApplication_Base.mJsonSerializerSettings);
            String actionName = dataTransfer.GetType().Name.Split(new String[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1];
            addReq_By_PayloadString(
                    actionName,
                    jsonText);
        }
        public void sendOCPP_CP_Req_AMI1_Req(String membershipCardPassword)
        {
            Req_DataTransfer dataTransfer = new Req_DataTransfer();
            dataTransfer.messageId = "AMI1";
            //        dataTransfer.vendorId = "";//mTable_Setting.getSettingData(CONST_OCPP_Setting.infor_chargePointVendor.getValue());
            Req_AMI1 ami1_Req = new Req_AMI1();
            ami1_Req.setRequiredValue(mChannelIndex);
            if (membershipCardPassword != null && membershipCardPassword.Length >= 3)
            {
                ami1_Req.membershipCardPassword = membershipCardPassword;
            }

            dataTransfer.data = JsonConvert.SerializeObject(ami1_Req, EL_MyApplication_Base.mJsonSerializerSettings);


            String jsonText = JsonConvert.SerializeObject(dataTransfer, EL_MyApplication_Base.mJsonSerializerSettings);
            String actionName = dataTransfer.GetType().Name.Split(new String[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1];
            addReq_By_PayloadString(
                    actionName,
                    jsonText);
        }


        public void sendOCPP_CP_Req_NPS1_Req(Req_NPS1 nps1)
        {
            Req_DataTransfer dataTransfer = new Req_DataTransfer();
            dataTransfer.messageId = "NPS1";
            //        dataTransfer.vendorId = "";//mTable_Setting.getSettingData(CONST_OCPP_Setting.infor_chargePointVendor.getValue());


            dataTransfer.data = JsonConvert.SerializeObject(nps1, EL_MyApplication_Base.mJsonSerializerSettings);

            String jsonText = JsonConvert.SerializeObject(dataTransfer, EL_MyApplication_Base.mJsonSerializerSettings);
            String actionName = dataTransfer.GetType().Name.Split(new String[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1];
            addReq_By_PayloadString(
                    actionName,
                    jsonText);
        }

        public void sendOCPP_CP_Req_NPS2_Req(Req_NPS2 nps2)
        {
            Req_DataTransfer dataTransfer = new Req_DataTransfer();
            dataTransfer.messageId = "NPS2";

            dataTransfer.data = JsonConvert.SerializeObject(nps2, EL_MyApplication_Base.mJsonSerializerSettings);

            String jsonText = JsonConvert.SerializeObject(dataTransfer, EL_MyApplication_Base.mJsonSerializerSettings);
            String actionName = dataTransfer.GetType().Name.Split(new String[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1];
            addReq_By_PayloadString(
                    actionName,
                    jsonText);
        }

    }
}
