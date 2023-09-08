using EL_DC_Charger.common.item;
using EL_DC_Charger.common.Manager;
using EL_DC_Charger.common.statemanager;
using EL_DC_Charger.ocpp.ver16.comm;
using EL_DC_Charger.ocpp.ver16.database;
using EL_DC_Charger.ocpp.ver16.packet;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.statemanager
{
    abstract public class EL_SendManager_OCPP_Base : EL_StateManager_Base
    {

        protected OCPP_Comm_Manager mComm_Manager = null;
        protected OCPP_Comm_SendMgr mComm_SendMgr = null;

        protected OCPP_Manager_Table_Setting mSettingData_OCPP_Table = null;

        protected bool bSendType_IsList = false;

        protected EL_StateManager_OCPP_Main mStateManager_OCPP_Main = null;

        protected OCPP_EL_Manager_Table_TransactionInfor mTable_TransactionInfor = null;
        protected OCPP_EL_Manager_Table_Transaction_Metervalues mTable_Transaction_Metervalues = null;

        public EL_StateManager_OCPP_Channel mStateManager_OCPP_Channel = null;
        public EL_SendManager_OCPP_Base(OCPP_Comm_Manager ocpp_comm_manager, int channelIndex, bool sendType_isList)
            : base(ocpp_comm_manager.getMyApplication(), channelIndex)
        {

            mComm_Manager = ocpp_comm_manager;
            mComm_SendMgr = mComm_Manager.getSendMgr();
            bSendType_IsList = sendType_isList;
            mSettingData_OCPP_Table = ((OCPP_Manager_Table_Setting)mApplication.getManager_SQLite_Setting_OCPP().getList_Manager_Table()[0]);

            if (mChannelIndex > 0)
                if (mApplication.getChannelTotalInfor(mChannelIndex).getStateManager_Channel().GetType() == typeof(EL_StateManager_OCPP_Channel))
                    mStateManager_OCPP_Channel = (EL_StateManager_OCPP_Channel)mApplication.getChannelTotalInfor(mChannelIndex).getStateManager_Channel();
            mStateManager_OCPP_Main = (EL_StateManager_OCPP_Main)mApplication.mStateManager_Main;

            mTable_TransactionInfor = mApplication.getManager_SQLite_Setting_OCPP().getTable_TransactionInfor();
            mTable_Transaction_Metervalues = mApplication.getManager_SQLite_Setting_OCPP().getTable_Transaction_Metervalues();
        }

        public int getCount_SendList()
        {
            return mPacket_SendList.Count;
        }


        override public void initVariable()
        {

        }



        override public void intervalExcuteAsync()
        {
            sendPacket();
        }

        abstract protected String getLogTag();

        protected int mCount_Send = 0;
        protected int mCount_SendFault = 0;

        protected EL_OCPP_Packet_Wrapper mPacket_SendPacket_Call_CP = null;
        public void setSendPacket_Call_CP(JArray packet)
        {
            if (packet == null)
            {
                mPacket_SendPacket_Call_CP = null;
                mCount_Send = 0;
                bIsReceivePacket_CallResult = false;
                return;
            }
            //try
            //{
            mPacket_SendPacket_Call_CP =
                    new EL_OCPP_Packet_Wrapper(packet[2].ToString(), packet[1].ToString(), packet);
            mCount_Send = 0;
            bIsReceivePacket_CallResult = false;

            //}
            //catch (JSONException e)
            //{
            //    e.printStackTrace();
            //}
        }


        virtual public void setSendPacket_Call_CP(String actionName, String payloadString)
        {
            if (actionName == null || payloadString == null)
            {
                mPacket_SendPacket_Call_CP = null;
                mCount_Send = 0;
                bIsReceivePacket_CallResult = false;
                return;
            }

            JArray call_Packet = new JArray();
            call_Packet.Add(2);
            call_Packet.Add(Guid.NewGuid().ToString());
            call_Packet.Add(actionName);
            if (payloadString != null && payloadString.Length > 0)
            {
                //try
                //{
                call_Packet.Add(JObject.Parse(payloadString));

                //}
                //catch (JSONException e)
                //{
                //    e.printStackTrace();
                //}
            }
            //try
            //{
            mPacket_SendPacket_Call_CP =
                    new EL_OCPP_Packet_Wrapper(call_Packet[2].ToString(), call_Packet[1].ToString(), call_Packet);
            mCount_Send = 0;
            bIsReceivePacket_CallResult = false;            
            //}
            //catch (JSONException e)
            //{
            //    e.printStackTrace();
            //}
        }

        virtual public void setSendPacket_CallResult_CP(String actionName, String payloadString)
        {
            JArray call_Packet = new JArray();
            call_Packet.Add(2);
            call_Packet.Add(Guid.NewGuid().ToString());
            call_Packet.Add(actionName);
            if (payloadString != null && payloadString.Length > 0)
            {
                //try
                //{
                call_Packet.Add(JObject.Parse(payloadString));

                //}
                //catch (JSONException e)
                //{
                //    e.printStackTrace();
                //}
            }
            mComm_Manager.sendPacket(getLogTag(), call_Packet.ToString());
        }


        protected bool bIsReceivePacket_CallResult = false;


        virtual public EL_OCPP_Packet_Wrapper setReceivePacket_CallResult(JArray JArray)
        {
            if (mPacket_SendPacket_Call_CP == null || JArray == null)
                return null;
            String uniqueId = "";
            //try
            //{
            uniqueId = JArray[1].ToString();
            //}
            //catch (JSONException e)
            //{
            //    e.printStackTrace();
            //    return null;
            //}
            if (uniqueId.Equals(mPacket_SendPacket_Call_CP.mUniqueId))
            {
                if (mPacket_SendPacket_Call_CP.mActionName.Equals(EOCPP_Action_CP_Call.Heartbeat.ToString()))
                {

                }
                else
                {
                    mPacket_SendPacket_Call_CP.bIsNeedRemove = true;
                    bIsReceivePacket_CallResult = true;
                }
                processReceivePacket_CallResult(mPacket_SendPacket_Call_CP, JArray);
                return mPacket_SendPacket_Call_CP;
            }

            return null;
        }
        abstract protected void processReceivePacket_CallResult(EL_OCPP_Packet_Wrapper sendPacket, JArray receivePacket);

        virtual public EL_OCPP_Packet_Wrapper setReceivePacket_CallError(JArray jArray)
        {
            if (mPacket_SendPacket_Call_CP == null || jArray == null)
                return null;
            String uniqueId = "";
            //try
            //{
            uniqueId = jArray[1].ToString();
            //}
            //catch (JSONException e)
            //{
            //    e.printStackTrace();
            //    return null;
            //}
            if (uniqueId.Equals(mPacket_SendPacket_Call_CP.mUniqueId))
            {
                if (mPacket_SendPacket_Call_CP.mActionName.Equals(EOCPP_Action_CP_Call.Heartbeat.ToString()))
                {

                }
                else
                {
                    mPacket_SendPacket_Call_CP.bIsNeedRemove = true;
                    bIsReceivePacket_CallResult = true;
                }
                processReceivePacket_CallError(mPacket_SendPacket_Call_CP, jArray);
                return mPacket_SendPacket_Call_CP;
            }

            return null;
        }
        abstract protected void processReceivePacket_CallError(EL_OCPP_Packet_Wrapper sendPacket, JArray receivePacket);


        protected List<EL_OCPP_Packet_Wrapper> mPacket_SendList = new List<EL_OCPP_Packet_Wrapper>();

        virtual public JArray addReq_By_PayloadString_First(String actionName, String payloadString)
        {
            JArray call_Packet = new JArray();
            call_Packet.Add(2);
            call_Packet.Add(Guid.NewGuid().ToString());
            call_Packet.Add(actionName);
            if (payloadString != null && payloadString.Length > 0)
            {
                //try
                //{
                call_Packet.Add(JObject.Parse(payloadString));

                //}
                //catch (JSONException e)
                //{
                //    e.printStackTrace();
                //}
            }
            Logger.d("OCPP *=*=* Add Req |||  " + call_Packet.ToString());
            lock (mPacket_SendList)
            {
                //try
                //{
                mPacket_SendList.Insert(0, new EL_OCPP_Packet_Wrapper(actionName, call_Packet[1].ToString(), call_Packet));
                //}
                //catch (JSONException e)
                //{
                //    e.printStackTrace();
                //}
            }
            return call_Packet;
        }

        virtual public JArray addReq_By_AllPacket(String actionName, String allPacket)
        {
            JArray call_Packet = null;
            //try
            //{
            call_Packet = JArray.Parse(allPacket);
            lock (mPacket_SendList)
            {
                mPacket_SendList.Add(new EL_OCPP_Packet_Wrapper(actionName, call_Packet[1].ToString(), call_Packet));
            }
            //}
            //catch (JSONException e)
            //{
            //    e.printStackTrace();
            //}
            return call_Packet;
        }


        virtual public JArray addReq_By_PayloadString(String actionName, String payloadString)
        {
            JArray call_Packet = new JArray();
            call_Packet.Add(2);
            call_Packet.Add(Guid.NewGuid().ToString());
            call_Packet.Add(actionName);
            if (payloadString != null && payloadString.Length > 0)
            {
                //try
                //{
                call_Packet.Add(JObject.Parse(payloadString));
                //}
                //catch (JSONException e)
                //{
                //    e.printStackTrace();
                //}
            }
            Logger.d("OCPP *=*=* Add Req |||  " + call_Packet.ToString());
            lock (mPacket_SendList)
            {
                //try
                //{
                mPacket_SendList.Add(new EL_OCPP_Packet_Wrapper(actionName, call_Packet[1].ToString(), call_Packet));
                //}
                //catch (JSONException e)
                //{
                //    e.printStackTrace();
                //}
            }
            return call_Packet;
        }

        //    protected bool removeSendPacket()
        //    {
        //        bool remove = false;
        //        for(int i = 0; i < mPacket_SendList.size(); i++)
        //        {
        //            if(mPacket_SendList.get(i).bIsNeedRemove)
        //            {
        //                mPacket_SendList.remove(i);
        //                remove = true;
        //                break;
        //            }
        //        }
        //
        //        if(mPacket_SendList.size() > 0 && mPacket_SendPacket_Call_CP != null && mPacket_SendPacket_Call_CP.bIsNeedRemove)
        //        {
        //            if(mPacket_SendList.get(0).mUniqueId.equals(mPacket_SendPacket_Call_CP.mUniqueId))
        //            {
        //                mPacket_SendList.remove(0);
        //                remove = true;
        //
        //            }
        //        }
        //        return remove;
        //    }

        public void clearAll()
        {
            bIsReceivePacket_CallResult = true;
            lock (mPacket_SendList)
            {
                mPacket_SendList.Clear();
            }
        }

        public void clearCurrent()
        {
            bIsReceivePacket_CallResult = true;
        }

        virtual protected void sendPacket()
        {
            if (!mComm_Manager.bIsConnected_HW)
                return;

            if (bSendType_IsList)
            {
                //            while (true)
                //            {
                //                if(!removeSendPacket())
                //                    break;
                //            }



                lock (mPacket_SendList)
                {
                    if (bIsReceivePacket_CallResult)
                    {
                        if (mPacket_SendList.Count > 0)
                            mPacket_SendList.RemoveAt(0);

                        mPacket_SendPacket_Call_CP = null;
                        bIsReceivePacket_CallResult = false;
                    }
                    if (mPacket_SendPacket_Call_CP == null && mPacket_SendList.Count > 0)
                    {

                        mPacket_SendPacket_Call_CP = mPacket_SendList[0];

                        mCount_Send = 1;
                        //mComm_Manager.sendPacket(getLogTag(), mPacket_SendPacket_Call_CP.mPacket.ToString());
                        //mTime_Send.setTime();
                    }
                }



                if (mCount_Send >= 2 && mPacket_SendPacket_Call_CP != null && !mPacket_SendPacket_Call_CP.bIsNeedRemove)
                {
                    if (mTime_Send.getSecond_WastedTime() >= getDelay_Second())
                    {
                        mComm_Manager.sendPacket(getLogTag(), mPacket_SendPacket_Call_CP.mPacket.ToString());
                        mCount_Send++;
                        if (mCount_Send >= 5)
                        {
                            bIsReceivePacket_CallResult = true;
                        }
                        mTime_Send.setTime();
                    }
                }
                else if (mCount_Send < 2 && mPacket_SendPacket_Call_CP != null && !mPacket_SendPacket_Call_CP.bIsNeedRemove)
                {
                    if (mTime_Send.getSecond_WastedTime() >= getDelay_First())
                    {
                        mComm_Manager.sendPacket(getLogTag(), mPacket_SendPacket_Call_CP.mPacket.ToString());
                        mCount_Send++;
                        if (mCount_Send >= 5)
                        {
                            bIsReceivePacket_CallResult = true;
                        }
                        mTime_Send.setTime();
                    }
                }

            }
            else
            {
                if (mPacket_SendPacket_Call_CP == null)
                    return;
                if (mPacket_SendPacket_Call_CP.bIsNeedRemove)
                {
                    bIsReceivePacket_CallResult = true;
                    return;
                }

                if (mCount_Send >= 2 && mPacket_SendPacket_Call_CP != null)
                {
                    if (mTime_Send.getSecond_WastedTime() >= getDelay_Second())
                    {
                        mComm_Manager.sendPacket(getLogTag(), mPacket_SendPacket_Call_CP.mPacket.ToString());
                        mCount_Send++;
                        if (mCount_Send >= 5)
                        {
                            bIsReceivePacket_CallResult = true;
                        }
                        mTime_Send.setTime();
                    }
                }
                else
                {
                    if (mTime_Send.getSecond_WastedTime() >= getDelay_First())
                    {
                        mComm_Manager.sendPacket(getLogTag(), mPacket_SendPacket_Call_CP.mPacket.ToString());
                        
                        mCount_Send++;
                        mTime_Send.setTime();
                    }
                }

            }


        }

        virtual protected void setSendPack(EL_OCPP_Packet_Wrapper packet)
        {
            mPacket_SendPacket_Call_CP = packet;
            mCount_Send = 0;
            mTime_Send.setTime();
        }


        protected int mDelay_First = 0;
        protected EL_Time mTime_Send = new EL_Time();
        public void initTime_Send()
        {
            mTime_Send.setTime();
        }

        virtual protected int getDelay_Second()
        {
            if (mPacket_SendPacket_Call_CP != null && mPacket_SendPacket_Call_CP.mActionName.Equals(EOCPP_Action_CP_Call.Heartbeat.ToString()))
                return mStateManager_OCPP_Main.getOCPP_Interval_Send_BootNotification();
            else
                return 10;
        }

        public void setDelay_First(int delay)
        {

            mDelay_First = delay;
        }

        virtual protected int getDelay_First()
        {
            //if (mPacket_SendPacket_Call_CP != null && mPacket_SendPacket_Call_CP.mActionName.Equals("Heartbeat")
            //    && !mStateManager_OCPP_Main.bOCPP_IsReceivePacket_CallResult_HeartBeat)
            //    return mStateManager_OCPP_Main.getOCPP_Interval_Send_BootNotification();
            //else
                return mDelay_First;
        }
    }

}
