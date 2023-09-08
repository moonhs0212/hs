using EL_DC_Charger.Controller;
using EL_DC_Charger.common.item;
using EL_DC_Charger.EL_DC_Charger.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.SerialPorts.smartro_tl3500bs
{
    public class Smartro_TL3500BS_PacketSendManager : Controller_Base
    {
        protected Smartro_TL3500BS_Comm_Manager mCommPort_Manager = null;
        public Smartro_TL3500BS_PacketSendManager(Smartro_TL3500BS_Comm_Manager commPort_Manager)
            : base(commPort_Manager.getApplication(), 0)
        {
            mCommPort_Manager = commPort_Manager;
            setMode(Smartro_TL3500BS_Constants.PacketSendState.READY);
            mPacketManager = new Smartro_TL3500BS_PacketManager((EL_DC_Charger_MyApplication)commPort_Manager.getApplication(), 0);
        }

        protected Smartro_TL3500BS_PacketManager mPacketManager = null;
        public Smartro_TL3500BS_PacketManager PacketManager
        {
            get { return mPacketManager; }
        }


        public void setCommand(int command)
        {
            if (mCommand_Next == mCommand_Processing
                && command != Smartro_TL3500BS_Constants.Command.NONE)
            {
                mCommand_Processing = Smartro_TL3500BS_Constants.Command.NONE;
            }
            mCommand_Next = command;

            initFlag();
        }

        protected void initFlag()
        {
            bIsReceive_ResponseAck = false;
            bIsReceive_Deal_Cancel = false;
            bIsReceive_CardNumber = false;

            bIsCommandComplete_OccuredError = false;
            bIsCommandComplete = false;
        }

        public bool bIsCommandComplete_OccuredError = false;
        public bool bIsCommandComplete = false;

        public bool bIsReceive_ResponseAck = false;
        public bool bIsReceive_Deal_Cancel = false;
        protected EL_Time mTime_Request = new EL_Time();

        public bool bIsReceive_CardNumber = false;
        protected String mCardNumber = "";
        public String getCardNumber()
        {
            return mCardNumber;
        }
        public void setCardNumber(String number)
        {
            if (number == null || number.Length < 16)
            {
                bIsReceive_CardNumber = false;
                mCardNumber = "";
                return;
            }

            mCardNumber = number;
            bIsReceive_CardNumber = true;
        }


        ////////////////////////////////////////////////////////////////////////////////////
        public bool bIsReceive_Deal_Approval = false;

        ////////////////////////////////////////////////////////////////////////////////////


        ////////////////////////////////////////////////////////////////////////////////////
        public void responseAck()
        {
            mCommPort_Manager.write(new byte[] { 0x06 });
        }

        public void responseNak()
        {
            mCommPort_Manager.write(new byte[] { 0x15 });
        }


        public override void setMode(int mode)
        {
            base.setMode(mode);
        }

        public override void process()
        {
            switch (mMode)
            {
                case Smartro_TL3500BS_Constants.PacketSendState.READY:
                    mCommPort_Manager.write(mPacketManager.Packet_Deal_Wait_Send_Only.send_makeSendData());
                    //if (state == CONST_STATE_UIFLOWTEST.STATE_READY)
                    //{
                    //    //카드결제 대기
                    //    ((EL_DC_Charger_MyApplication)mApplication).SerialPort_Smartro_CardReader.getManager_Send().setCommand(Smartro_TL3500BS_Constants.Command.DEAL_WAIT);
                    //}
                    //mCommPort_Manager.write(mPacketManager.Packet_DealCancel_Send.send_makeSendData());
                    //                mCommPort_Manager.writeData(mPacket_ALL_Card_Auto_Reading);
                    setMode(Smartro_TL3500BS_Constants.PacketSendState.READY + 1);
                    break;
                case Smartro_TL3500BS_Constants.PacketSendState.READY + 1:

                    if (isTimer(TIMER_5S))
                    {

                    }
                    else
                    {
                        switch (mCommand_Next)
                        {
                            case Smartro_TL3500BS_Constants.Command.CARD_CHECK:
                                setMode(Smartro_TL3500BS_Constants.PacketSendState.CARD_CHECK);
                                break;

                            case Smartro_TL3500BS_Constants.Command.DEAL_APPROVAL:
                                setMode(Smartro_TL3500BS_Constants.PacketSendState.DEAL_APPROVAL);
                                break;
                            case Smartro_TL3500BS_Constants.Command.DEAL_CANCEL:
                                setMode(Smartro_TL3500BS_Constants.PacketSendState.DEAL_CANCEL);
                                break;
                            //결제대기
                            case Smartro_TL3500BS_Constants.Command.DEAL_WAIT:
                                setMode(Smartro_TL3500BS_Constants.PacketSendState.DEAL_WAIT);
                                break;
                            case Smartro_TL3500BS_Constants.Command.RESET:
                                setMode(Smartro_TL3500BS_Constants.PacketSendState.RESET);
                                break;
                            case Smartro_TL3500BS_Constants.Command.DEVICE_CHECK:
                                setMode(Smartro_TL3500BS_Constants.PacketSendState.DEVICE_CHECK);
                                break;
                        }

                        if (mMode != Smartro_TL3500BS_Constants.PacketSendState.READY + 1)
                        {
                            mCommand_Processing = mCommand_Next;
                        }
                    }
                    break;

                case Smartro_TL3500BS_Constants.PacketSendState.CARD_CHECK:
                    bIsReceive_ResponseAck = false;
                    bIsReceive_CardNumber = false;
                    mCommPort_Manager.write(mPacketManager.Packet_CardCheck_Send.send_makeSendData());
                    mTime_Request.setTime();
                    setMode(Smartro_TL3500BS_Constants.PacketSendState.CARD_CHECK + 1);
                    break;

                case Smartro_TL3500BS_Constants.PacketSendState.CARD_CHECK + 1:
                    if (!bIsReceive_ResponseAck && mTime_Request.getSecond_WastedTime() >= 3)
                    {
                        mCommPort_Manager.write(mPacketManager.Packet_CardCheck_Send.send_makeSendData());
                        mTime_Request.setTime();
                    }
                    if (processCheck_ReturnReady())
                    {

                    }
                    else if (bIsReceive_CardNumber)
                    {

                    }
                    break;
                /////////////////////////////////////////////////////////////////////////////////////////
                case Smartro_TL3500BS_Constants.PacketSendState.DEAL_APPROVAL:
                    bIsReceive_ResponseAck = false;
                    bIsReceive_Deal_Approval = false;
                    mCommPort_Manager.write(mPacketManager.Packet_AddInfor_Deal_Request_Send.send_makeSendData());
                    mTime_Request.setTime();
                    setMode(Smartro_TL3500BS_Constants.PacketSendState.DEAL_APPROVAL + 1);
                    break;

                case Smartro_TL3500BS_Constants.PacketSendState.DEAL_APPROVAL + 1:
                    if (processCheck_ReturnReady())
                    {
                        //setMode(Smartro_TL3500BS_Constants.PacketSendState.READY);
                    }
                    break;
                /////////////////////////////////////////////////////////////////////////////////////////                


                case Smartro_TL3500BS_Constants.PacketSendState.DEAL_CANCEL:
                    Console.WriteLine("Smartro_TL3500BS_Constants.PacketSendState.DEAL_CANCEL Excute");
                    bIsReceive_ResponseAck = false;
                    bIsReceive_Deal_Cancel = false;
                    mCommPort_Manager.write(mPacketManager.Packet_DealCancel_Send.send_makeSendData());
                    mTime_Request.setTime();
                    setMode(Smartro_TL3500BS_Constants.PacketSendState.DEAL_CANCEL + 1);
                    break;

                case Smartro_TL3500BS_Constants.PacketSendState.DEAL_CANCEL + 1:
                    if (processCheck_ReturnReady())
                    {
                        //setMode(Smartro_TL3500BS_Constants.PacketSendState.READY);
                    }
                    break;

                /////////////////////////////////////////////////////////////////////////////////////////
                case Smartro_TL3500BS_Constants.PacketSendState.RESET:
                    bIsReceive_ResponseAck = false;
                    mCommPort_Manager.write(mPacketManager.Packet_Reset_Send.send_makeSendData());
                    mTime_Request.setTime();
                    setMode(Smartro_TL3500BS_Constants.PacketSendState.RESET + 1);
                    break;

                case Smartro_TL3500BS_Constants.PacketSendState.RESET + 1:
                    if (processCheck_ReturnReady())
                    {
                        //setMode(Smartro_TL3500BS_Constants.PacketSendState.READY);
                    }
                    //else if(mTime_Mode.getSecond > 60)
                    break;
                /////////////////////////////////////////////////////////////////////////////////////////////

                case Smartro_TL3500BS_Constants.PacketSendState.DEAL_WAIT:

                    //byte[] a = mPacketManager.Packet_Deal_Wait_Send_Only.sendPacket();
                    mCommPort_Manager.write(mPacketManager.Packet_Deal_Wait_Send_Only.send_makeSendData());
                    mTime_Request.setTime();
                    setMode(Smartro_TL3500BS_Constants.PacketSendState.DEAL_WAIT + 1);
                    break;
                case Smartro_TL3500BS_Constants.PacketSendState.DEAL_WAIT + 1:
                    if (processCheck_ReturnReady())
                    {
                    }
                    break;
            }
        }




        public int Command_Processing
        {
            get { return mCommand_Processing; }
            set { mCommand_Processing = value; }
        }


        protected int mCommand_Processing = Smartro_TL3500BS_Constants.Command.NONE;
        protected int mCommand_Next = Smartro_TL3500BS_Constants.Command.NONE;

        protected bool processCheck_ReturnReady()
        {
            if (mCommand_Next == Smartro_TL3500BS_Constants.Command.NONE)
            {
                setMode(Smartro_TL3500BS_Constants.PacketSendState.READY);
                return true;
            }

            if (mCommand_Processing != mCommand_Next)
            {
                setMode(Smartro_TL3500BS_Constants.PacketSendState.READY);
                return true;
            }

            return false;
        }

    }
}
