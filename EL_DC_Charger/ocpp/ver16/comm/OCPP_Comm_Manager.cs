using EL_DC_Charger.common.application;
using EL_DC_Charger.common.interf;
using EL_DC_Charger.common.item;
using EL_DC_Charger.common.Manager;
using EL_DC_Charger.common.thread;
using EL_DC_Charger.common.variable;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.statemanager;
using EL_DC_Charger.ocpp.ver16.database;
using EL_DC_Charger.ocpp.ver16.infor;
using EL_DC_Charger.ocpp.ver16.interf;
using EL_DC_Charger.ocpp.ver16.packet.cp2csms;
using EL_DC_Charger.ocpp.ver16.statemanager;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;
using Logger = EL_DC_Charger.common.Manager.Logger;

namespace EL_DC_Charger.ocpp.ver16.comm
{
    public class OCPP_Comm_Manager : EL_Thread_Base, ICSMS_ConnectionState_Manager
    {

        protected OCPP_MainInfor mOCPP_MainInfor = null;

        //protected WebSocketListener mSocketListener = null;
        //public void setWebSocketListener(WebSocketListener listener)
        //{
        //    mSocketListener = listener;
        //}
        private const int CLOSE_STATUS = 1000;

        //protected OkHttpClient mClient = null;
        protected WebSocketSharp.WebSocket mWebSocket = null;
        protected OCPP_Manager_Table_Setting mTable_Setting = null;

        protected EL_Thread_Base mReceiveThread = null;

        public class OCPP_Receive_Thread : EL_Thread_Base
        {
            protected OCPP_Comm_Manager mComm_Manager = null;
            public OCPP_Receive_Thread(OCPP_Comm_Manager manager)
                : base(manager.getApplication(), 50, true)
            {
                mComm_Manager = manager;
            }

            public override void initVariable()
            {

            }

            public override void intervalExcute()
            {
                if (mComm_Manager.ws != null && mComm_Manager.ws.State == System.Net.WebSockets.WebSocketState.Open)
                {
                    Task t = task_recieveAsync();
                    try
                    {
                        t.Wait();
                    }
                    catch (Exception e)
                    {
                        Logger.d("Read Failed = " + e.Message);
                        mComm_Manager.onDisconnect_Software();
                    }

                }
            }

            async Task task_recieveAsync()
            {
                ArraySegment<byte> Receive = new ArraySegment<byte>(new byte[2048]);
                WebSocketReceiveResult result = await mComm_Manager.ws.ReceiveAsync(Receive, CancellationToken.None);

                if (result.Count > 5)
                {
                    try
                    {
                        String resultString = Encoding.UTF8.GetString(Receive.Array, 0, result.Count);
                        Logger.d("ReceiveData >> " + resultString);

                        mComm_Manager.mSendMgr.setReceiveData(JArray.Parse(resultString));
                    }
                    catch (Exception e)
                    {
                        Logger.d(e.Message);
                    }


                }

            }
        }

        public OCPP_Comm_Manager(EL_MyApplication_Base application)
            : base(application, 100, true)
        {

            mTable_Setting = (OCPP_Manager_Table_Setting)mApplication.getManager_SQLite_Setting_OCPP().getTable_Setting(0);
        }

        protected OCPP_Comm_SendMgr mSendMgr = null;
        public OCPP_Comm_SendMgr getSendMgr()
        {
            return mSendMgr;
        }
        public bool sendPacket(String tag, String packetString)
        {

            packetString = packetString.Replace("\r\n", "");
            packetString = packetString.Replace(" ", "");
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(packetString);
            if (ws.State == System.Net.WebSockets.WebSocketState.Open)
            {

                ws.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
                Logger.d("＠Send Success＠ " + tag + ": " + packetString);
                return true;
            }
            else
            {
                onDisconnect_Software();
                Logger.d("＠Send Failed＠ " + tag + ": " + packetString);
                return false;
            }

            //mWebSocket.Send(packetString);


            //if ()
            //{
            //    Logger.d("＠Send Success＠ " + tag + packetString);
            //    return true;
            //}
            //else
            //{
            //    Logger.d("＠Send Failed＠ " + tag + packetString);
            //    onDisconnect_Socket();
            //    return false;
            //}

        }

        public void setSendManager()
        {
            mSendMgr = new OCPP_Comm_SendMgr(mApplication, this);
            mSendMgr.initVariable();
        }



        public void openComm()
        {
            mCount_Reconnect++;
            Logger.d("▷▶ OCPP openComm (reConnectCount:" + mCount_Reconnect + ")");
            mTime_Disconnect_Socket = null;
            if (mWebSocket != null)
                closeComm();

            //excuteConnect();
            //excuteConnect_Softberry();

            switch (mApplication.getPlatform())
            {
                case common.variable.EPlatform.WEV:
                    excuteConnect_Wev();
                    break;
                case common.variable.EPlatform.SOFTBERRY:
                    excuteConnect_Softberry();
                    break;
                case common.variable.EPlatform.NICE_CHARGER:
                    break;
                case common.variable.EPlatform.NONE:
                    Logger.d("Not Connect Server Because Plfatform Operator = NONE");
                    break;
                default:
                    excuteConnect();
                    break;
            }

        }
        public void closeComm()
        {
            mWebSocket.Close(CLOSE_STATUS);// close(CLOSE_STATUS, null);
            mWebSocket = null;
        }
        protected bool bIsStart = false;
        public int mCount_Reconnect = 0;
        protected bool bCommand_Connect = false;
        public void setCommand_Connect(bool command_Connect)
        {
            bCommand_Connect = command_Connect;
        }

        public EL_Time mTime_Disconnect_Socket = null;
        public void onDisconnect_Socket()
        {
            Logger.d("▷▶ OCPP onDisonnect HW");


            bIsConnected_HW = false;
            bIsConnected_SW = false;


            if (mTime_Disconnect_Socket == null)
            {
                mTime_Disconnect_Socket = new EL_Time();
                mTime_Disconnect_Socket.setTime();
            }
            else
            {

            }

            if (bOCPP_Receive_BootNotification_First)
            {
                bOCPP_Receive_BootNotification_Socket_Disconnect = false;
                //            mSendMgr.mStateManager_OCPP_Main.mSendManager_OCPP_CP_Req_Normal.sendOCPP_CP_Req_BootNotification_Directly();
            }
        }
        public bool bOCPP_Receive_BootNotification_First = false;
        public bool bOCPP_Receive_BootNotification_Socket_Disconnect = false;
        public bool bOCPP_Receive_StopTransaction = false;
        public bool bOCPP_Receive_MeterValue = false;
        public void onConnect_Socket()
        {
            Logger.d("▷▶ OCPP onConnect HW");
            bIsConnected_HW = true;
            mSendMgr.mCount_Send = 0;

            if (bOCPP_Receive_BootNotification_First)
            {
                if (mApplication.getPlatform_Operator() == EPlatformOperator.WEV)
                {
                    //                for(int i = 0; i < mApplication.getChannelTotalInfor().length; i++)
                    //                {
                    //
                    //                    mApplication.getChannelTotalInfor(i+1).getStateManager_Channel_Ocpp()
                    //                            .mSendManager_OCPP_ChargingReq.sendOCPP_CP_Req_BootNotification_When_SessionStop();
                    //                }
                    //
                    //                mSendMgr.mStateManager_OCPP_Main.mSendManager_OCPP_CP_Req_Normal.sendOCPP_CP_Req_BootNotification_Directly();

                    if (mSendMgr != null)
                    {
                        oCPP_SendBootNotification_When_SessionStop();
                    }
                }
            }
        }

        public void oCPP_SendBootNotification_When_SessionStop()
        {
            switch (mApplication.getPlatform())
            {
                case EPlatform.NONE:
                    break;
                case EPlatform.OCTT_CERTIFICATION:
                    break;
                case EPlatform.WEV:
                    Req_BootNotification bootNotification = new Req_BootNotification();
                    bootNotification.setRequiredValue(
                        ((EL_StateManager_OCPP_Main)mApplication.getStateManager_Main()).getOCPP_MainInfor().getChargePointModel(),
                        ((EL_StateManager_OCPP_Main)mApplication.getStateManager_Main()).getOCPP_MainInfor().getChargePointVendor());

                    bootNotification.setRequiredValue(((EL_StateManager_OCPP_Main)mApplication.getStateManager_Main()).getOCPP_MainInfor().getChargePointModel(),
                        ((EL_StateManager_OCPP_Main)mApplication.getStateManager_Main()).getOCPP_MainInfor().getChargePointVendor());
                    JArray call_Packet = new JArray();
                    call_Packet.Add(2);
                    call_Packet.Add(Guid.NewGuid().ToString());
                    call_Packet.Add("BootNotification");
                    call_Packet.Add(JObject.Parse(JsonConvert.SerializeObject(bootNotification, EL_MyApplication_Base.mJsonSerializerSettings)));
                    sendPacket("Sesson Reconnect", call_Packet.ToString());



                    ((EL_StateManager_OCPP_Main)mApplication.mStateManager_Main).mSendManager_StatusNotification_Main.sendOCPP_CP_Req_StatusNotification_Dreictly_Once();

                    for (int i = 0; i < mApplication.getChannelTotalInfor().Length; i++)
                    {
                        ((EL_StateManager_OCPP_Channel)mApplication.getChannelTotalInfor(i + 1).getStateManager_Channel())
                                .mSendManager_StatusNotification.sendOCPP_CP_Req_StatusNotification_Dreictly_Once();
                    }
                    break;
                case EPlatform.SOFTBERRY:
                    break;
                case EPlatform.NICE_CHARGER:
                    break;
            }
        }

        public void onConnect_Software()
        {
            Logger.d("▷▶ OCPP onConnect SW");
            mCount_Reconnect = 0;
            bIsConnected_HW = true;
            bIsConnected_SW = true;
            mTime_Disconnect_Socket = null;


        }

        public void onDisconnect_Software()
        {
            Logger.d("▷▶ OCPP onDisconnect SW");

            bIsConnected_SW = false;
            if (mTime_Disconnect_Socket == null)
            {
                mTime_Disconnect_Socket = new EL_Time();
                mTime_Disconnect_Socket.setTime();
            }
        }


        public bool bIsConnected_HW = false;
        public bool bIsConnected_SW = false;


        override public void initVariable()
        {
            //mSocketListener = new OCPP_Socket_Listener(this);
            //mClient = new OkHttpClient();
            setSendManager();

            mOCPP_MainInfor = mApplication.getOCPP_MainInfor();

            if (mReceiveThread == null)
            {
                mReceiveThread = new OCPP_Receive_Thread(this);
                mReceiveThread.start();
            }
            //mConnectionInfor = mApplication.mOCPP_ConnectionInfor;

            //        String sendData = "[2,\"cad4de33-180f-44c2-9a27-1a93be97199e\",\"BootNotification\",{\"chargePointModel\":\"EL-Test1\",\"chargePointVendor\":\"EL-ELECTRIC\"}]";
            //        byte[] b = sendData.getBytes(StandardCharsets.US_ASCII);

            //        bSendResult = mWebSocket.send("[2,\"cad4de33-180f-44c2-9a27-1a93be97199e\",\"BootNotification\",{\"chargePointModel\":\"WEV-A07PW\",\"chargePointVendor\":\"ELELECTRIC-NYJ\"}]");
            //        sendResult = webSocket.send(ByteString.encodeString(sendData, StandardCharsets.UTF_16));


            //        mClient.dispatcher().executorService().shutdown();
            //        mClient.dispatcher().executorService().
        }

        protected bool bSendResult = false;


        override public void intervalExcute()
        {
            if (mSendMgr != null)
                mSendMgr.intervalExcuteAsync();
        }


        public bool isEnable_LocalMode()
        {
            bool setting = mApplication.getManager_SQLite_Setting_OCPP().getTable_Setting(0).getSettingData_Boolean((int)CONST_INDEX_OCPP_Setting.LocalAuthListEnabled);
            return setting;
        }


        public bool isLocalMode()
        {
            return false;
        }


        public bool isConnectedServer()
        {
            return false;
        }




        private void excuteConnect()
        {
            String ip = mOCPP_MainInfor.getServerIP() + ":" + mOCPP_MainInfor.getServerPort();//Default

            String serialNo = mOCPP_MainInfor.getChargeBox_SerialNumber();

            //String url2 = "ws://" + ip + "/ocpp/" + serialNo;//Default

            String url2 = "ws://192.168.0.6:12200/ocpp/NYJ-TEST0001";

            //Request request = new Request.Builder().url(url2)
            //        //                .addHeader("", "GET /webServices/ocpp/CS3211 HTTP/1.1")
            //        .addHeader("Host", ip)
            //        .addHeader("Upgrade", "websocket")
            //        .addHeader("Connection", "Upgrade")
            //        .addHeader("Sec-WebSocket-Key", "x3JJHMbDL1EzLkh9GBhXDw==")
            //        .addHeader("Sec-WebSocket-Protocol", "ocpp1.6, ocpp1.5")//Default
            //        .addHeader("Sec-WebSocket-Version", "13")
            //        .cacheControl
            //        (
            //                new CacheControl.Builder().minFresh(1, TimeUnit.DAYS)
            //                        .maxStale(1, TimeUnit.DAYS)
            //                        .maxAge(1, TimeUnit.SECONDS)
            //                        .build()
            //        )
            //        .build();

            //using (var ws = new System.Net.WebSockets.ClientWebSocket())
            //{

            //}


            //ClientWebSocket webSocket = new ClientWebSocket();
            ////webSocket.Options.


            //webSocket.ConnectAsync(new Uri(url2), new System.Threading.CancellationToken(true));
            ////webSocket.


            //while (true)
            //{
            //    Console.WriteLine("webSocket.State = " + webSocket.State);
            //    Thread.Sleep(100);
            //}

            Task t = task_WebSocketClient();
            try
            {
                t.Wait();
            }
            catch (Exception e)
            {
                Logger.d("On Error = " + e.Message);
                if (ws != null && ws.State != System.Net.WebSockets.WebSocketState.Closed)
                {
                    ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);//   (serverUri, CancellationToken.None);
                    ws.Dispose();
                    t.Dispose();

                }
                ws = null;
                onDisconnect_Socket();

            }
            //catch(ObjectDisposedException e1)
            //{
            //    Logger.d("On Error = " + e1.Message);
            //    ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);//   (serverUri, CancellationToken.None);
            //    ws.Dispose();
            //    t.Dispose();
            //    ws = null;
            //    onDisconnect_Socket();
            //}




            //mWebSocket = new WebSocketSharp.WebSocket(url2);

            //mWebSocket.
            //mWebSocket.SetCookie(new WebSocketSharp.Net.Cookie("Host", "192.168.0.7"));
            //mWebSocket.SetCookie(new WebSocketSharp.Net.Cookie("Upgrade", "websocket"));
            //mWebSocket.SetCookie(new WebSocketSharp.Net.Cookie("Connection", "Upgrade"));
            //mWebSocket.SetCookie(new WebSocketSharp.Net.Cookie("Sec-WebSocket-Key", "x3JJHMbDL1EzLkh9GBhXDw=="));
            //mWebSocket.
            //mWebSocket.SetCookie(new WebSocketSharp.Net.Cookie("Sec-WebSocket-Protocol", "ocpp1.6"));
            //mWebSocket.SetCookie(new WebSocketSharp.Net.Cookie("Sec-WebSocket-Version", "13"));

            //.addHeader("Host", ip)
            //    .addHeader("Upgrade", "websocket")
            //    .addHeader("Connection", "Upgrade")
            //    .addHeader("Sec-WebSocket-Key", "x3JJHMbDL1EzLkh9GBhXDw==")
            //    .addHeader("Sec-WebSocket-Protocol", "ocpp1.6, ocpp1.5")//Default
            //    .addHeader("Sec-WebSocket-Version", "13")

            //mWebSocket.SetCredentials("Sec-WebSocket-Protocol", "ocpp1.6, ocpp1.5", true);
            //mWebSocket.OnClose += eventHandler_WebSocket_Close;
            //mWebSocket.OnError += eventHandler_WebSocket_Error;
            //mWebSocket.OnOpen += eventHandler_WebSocket_Open;
            //mWebSocket.OnMessage += eventHandler_WebSocket_Message;
            //mWebSocket.ConnectAsync();

            //while (true)
            //{
            //    Console.WriteLine("webSocket.State = " + mWebSocket.ReadyState);
            //    Thread.Sleep(100);
            //}
            //mClient.newWebSocket(request, mSocketListener);
        }
        public ClientWebSocket ws = null;
        async Task task_WebSocketClient()
        {
            ws = new ClientWebSocket();

            Console.WriteLine("Connect Start~ 1");


            //ws.Options.AddSubProtocol("Upgrade");
            //ws.Options.AddSubProtocol("websocket");
            //ws.Options.AddSubProtocol("ocpp1.6");
            //ws.Options.SetRequestHeader
            //ws.Options.AddSubProtocol("13");
            //ws.Options.SetRequestHeader("Sec-WebSocket-Protocol", "ocpp1.6");
            //ws.Options.SetRequestHeader("Sec-WebSocket-Version", "13");
            //ws.Options.SetRequestHeader("Host", "192.168.0.7:12200");

            //ws.Options.SetRequestHeader("Sec-WebSocket-Key", "x3JJHMbDL1EzLkh9GBhXDw==");
            //CookieContainer cookieContainer = new CookieContainer();
            //cookieContainer.SetCookies(new Uri("Host: 192.168.0.7:12200"), "Connection: Upgrade; Upgrade: websocket; Sec-WebSocket-Key: x3JJHMbDL1EzLkh9GBhXDw==; Sec-WebSocket-Protocol: ocpp1.6; Sec-WebSocket-Version: 13");
            ////cookieContainer.Add(new Cookie("Host", "192.168.0.7:12200"));
            ////cookieContainer.Add(new Cookie("Connection", "Upgrade"));
            ////cookieContainer.Add(new Cookie("Upgrade", "websocket"));
            ////cookieContainer.Add(new Cookie("Sec-WebSocket-Key", "x3JJHMbDL1EzLkh9GBhXDw=="));
            ws.Options.SetRequestHeader("Sec-WebSocket-Protocol", "ocpp1.6");
            ////cookieContainer.Add(new Cookie("Sec-WebSocket-Version", "13"));
            //ws.Options.Cookies = cookieContainer;

            TimeSpan span = new TimeSpan();
            span.Add(new TimeSpan(24, 0, 0));
            ws.Options.KeepAliveInterval = span;
            ws.Options.SetBuffer(5000, 5000);
            //ws.Options.
            //ws.Options.SetRequestHeader("Host", "192.168.0.7:12200");//오류남
            //ws.Options.SetRequestHeader("Connection", "Upgrade");//오류남
            //ws.Options.SetRequestHeader("Upgrade", "websocket");
            //ws.Options.SetRequestHeader("Sec-WebSocket-Key", "x3JJHMbDL1EzLkh9GBhXDw==");
            //ws.Options.SetRequestHeader("Sec-WebSocket-Protocol", "ocpp1.6");
            //ws.Options.SetRequestHeader("Sec-WebSocket-Version", "13");
            Console.WriteLine("Connect Start~2");


            StringBuilder builder = new StringBuilder();

            String ip_header = mApplication.getManager_SQLite_Setting_OCPP().getTable_Setting(0).getSettingData((int)CONST_INDEX_OCPP_Setting.infor_csms_ip_header);
            String ip = mApplication.getManager_SQLite_Setting_OCPP().getTable_Setting(0).getSettingData((int)CONST_INDEX_OCPP_Setting.infor_csms_ip);
            String port = mApplication.getManager_SQLite_Setting_OCPP().getTable_Setting(0).getSettingData((int)CONST_INDEX_OCPP_Setting.infor_csms_port);
            String more = mApplication.getManager_SQLite_Setting_OCPP().getTable_Setting(0).getSettingData((int)CONST_INDEX_OCPP_Setting.infor_csms_ip_more);
            String chargeBoxSerialNumber = mApplication.getManager_SQLite_Setting_OCPP().getTable_Setting(0).getSettingData((int)CONST_INDEX_OCPP_Setting.infor_ChargeBoxSerialNumber);

            string url2;

            // 0이면 포트 기입 안함
            if (port == "0")
                url2 = builder.Append(ip_header).Append("://").Append(ip).Append(more).Append("/").Append(chargeBoxSerialNumber).ToString();
            else
                url2 = builder.Append(ip_header).Append("://").Append(ip).Append(":").Append(port).Append(more).Append("/").Append(chargeBoxSerialNumber).ToString();


            if (EL_DC_Charger_MyApplication.getInstance().offlineTest_isuse)
            {
                url2 = "ws://192.168.0.74:12200/ocpp/";
            }
            else
                url2 = "ws://192.168.0.73:12200/ocpp/";
            //인증모드


            Uri serverUri = new Uri(url2);
            //CancellationToken token = new CancellationToken(true);
            await ws.ConnectAsync(serverUri, CancellationToken.None);

            switch (ws.State)
            {
                case System.Net.WebSockets.WebSocketState.Open:
                    bIsConnected_HW = true;
                    if (mApplication.getStateManager_Main() is SC_1CH_OCPP_StateManager_Main)
                    {
                        if (((SC_1CH_OCPP_StateManager_Main)mApplication.getStateManager_Main()).bIsNormalState)
                        {
                            onConnect_Socket();
                        }
                    }
                    break;
                default:
                    onDisconnect_Software();
                    break;
            }



            //Console.WriteLine("Connect Start~3");
            //while (ws.State == System.Net.WebSockets.WebSocketState.Open)
            //{
            //    Console.WriteLine("state : " + ws.State);


            //    ws.SendAsync(new ArraySegment<byte>()

            //    ArraySegment<byte> Receive = new ArraySegment<byte>(new byte[5000]);
            //    WebSocketReceiveResult result = await ws.ReceiveAsync(Receive, CancellationToken.None);
            //    String resultString = Encoding.UTF8.GetString(Receive.Array, 0, result.Count);

            //}

            //Console.WriteLine("Can't Connect~");



        }

        protected void eventHandler_WebSocket_Open(object sender, EventArgs e)
        {
            onConnect_Socket();
            String message = e.ToString();
            Logger.d("WebSocket Open => " + message);
        }

        protected void eventHandler_WebSocket_Close(object sender, CloseEventArgs e)
        {
            onDisconnect_Software();
            String message = e.Reason;
            Logger.d("WebSocket Close => " + message);
        }

        protected void eventHandler_WebSocket_Error(object sender, ErrorEventArgs e)
        {
            String message = e.Message;

            Logger.d("WebSocket Error => " + message);
        }

        protected void eventHandler_WebSocket_Message(object sender, MessageEventArgs e)
        {
            String message = e.Data;
            JArray jArray = JArray.Parse(message);
            mSendMgr.setReceiveData(jArray);

            Logger.d("WebSocket Receive Data => " + message);
        }

        private void excuteConnect_Softberry()
        {
            String serialNo = mOCPP_MainInfor.getChargeBox_SerialNumber();
            String ip = "wss://dev-hub-ws.soft-berry.com";//Softberry

            String softberry_connectionInfor = "wss://dev-hub-ws.soft-berry.com/ocpp1.6/4b90c398-4df8-48c5-9ee0-644dae0f5f9e/ELCT0001?access_token=eyJraWQiOiJodHRwczpcL1wvb2F1dGgyLmV2LWluZnJhLmNvbSIsImFsZyI6IlJTMjU2In0.eyJzdWIiOiJTb2Z0QmVycnkiLCJleHAiOjE2NzI0OTg4MDAsImlhdCI6MTY3MTA2Mzg2NSwidXNlciI6ImVsX2VsZWN0cmljIiwianRpIjoiYjdmYmNlMTItM2M0MS00MzhmLThlN2YtYWI3OGYxMGMyNjcyIn0.cOcTPVhQSoNsDNurupXngnE_de8hwxfHtDxOFfk_knZ7aGQ9bNq93fthIRSZL6pbC96M7cG84cgxOPv0bLJowlV_5NMU-cgs2a8z2jc568HcjzeiKenRCv5J8OUCSyNecjjOF3XLWlPT4wpzxj7ZR7wyYSdYTT46UTCSSSXZ5tfKwiBJQtJKrxXy28UXt95D1nBfy5s6gpGwVPIanw5zBpomT_AbNjjnRcsyBA_M6ymsiSJ7XifzZH9ed8UWU4iPSI8Sz1toTsk8loJpO6iC0JOpmSWEoGATgRJTistlkUj_r7Kil6AkGVgoLDcvoYyF12ndM40cQ5QuwhmXvc-OAg";
            String url2 = softberry_connectionInfor;//Softberry

            //Request request = new Request.Builder().url(url2)
            //        //                .addHeader("", "GET /webServices/ocpp/CS3211 HTTP/1.1")
            //        //                .addHeader("Host", ip)
            //        //                .addHeader("Upgrade", "websocket")
            //        //                .addHeader("Connection", "Upgrade")
            //        //                .addHeader("Sec-WebSocket-Key", "x3JJHMbDL1EzLkh9GBhXDw==")
            //        //                .addHeader("Sec-WebSocket-Protocol", "ocpp1.6, ocpp1.5")//Default
            //        .addHeader("Sec-WebSocket-Protocol", "ocpp1.6")//Softberry
            //                                                       //                .addHeader("Sec-WebSocket-Version","13")
            //        .build();

            //mWebSocket = mClient.newWebSocket(request, mSocketListener);

        }

        private void excuteConnect_Wev()
        {
            //        String serialNo = mChargerInfor.getChargeBox_SerialNumber();
            //        String ip = "wss://dev-hub-ws.soft-berry.com";//Softberry

            //        String softberry_connectionInfor = "wss://dev.wev-charger.com:12200/v1.6/NYJ-TEST0001";

            String ip_header = mTable_Setting.getSettingData((int)CONST_INDEX_OCPP_Setting.infor_csms_ip_header);
            String ip = mTable_Setting.getSettingData((int)CONST_INDEX_OCPP_Setting.infor_csms_ip);
            String port = mTable_Setting.getSettingData((int)CONST_INDEX_OCPP_Setting.infor_csms_port);
            String more = mTable_Setting.getSettingData((int)CONST_INDEX_OCPP_Setting.infor_csms_ip_more);
            String chargeBoxSerialNumber = mTable_Setting.getSettingData((int)CONST_INDEX_OCPP_Setting.infor_ChargeBoxSerialNumber);

            //        String softberry_connectionInfor = "ws://222.106.156.156:12200/v1.6/NYJ-TEST0001";
            //        String url2 = softberry_connectionInfor;//Softberry

            String url2 = ip_header + "://" + ip + ":" + port + more + "/" + chargeBoxSerialNumber;



            Task t = task_WebSocketClient();
            try
            {
                t.Wait();
            }
            catch (Exception e)
            {
                Logger.d("On Error = " + e.Message);
                if (ws != null && ws.State != System.Net.WebSockets.WebSocketState.Closed)
                {
                    ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);//   (serverUri, CancellationToken.None);
                    ws.Dispose();
                    t.Dispose();

                }
                ws = null;
                onDisconnect_Socket();

            }
            //        url2 = "wss://dev.wev-charger.com:12200/v1.6/FT0001";
            //Request request = new Request.Builder().url(url2)
            //        //                .addHeader("Host", "wss://dev.wev-charger.com")
            //        .addHeader("Upgrade", "websocket")
            //        //                .addHeader("Connection", "Upgrade")
            //        //                .addHeader("Sec-WebSocket-Key", "x3JJHMbDL1EzLkh9GBhXDw==")
            //        .addHeader("Sec-WebSocket-Protocol", "ocpp1.6")//Default
            //        .build();

            //mWebSocket = mClient.newWebSocket(request, mSocketListener);

        }

        private void excuteConnect_Wev_Test()
        {
            //        String serialNo = mChargerInfor.getChargeBox_SerialNumber();
            //        String ip = "wss://dev-hub-ws.soft-berry.com";//Softberry

            //        String softberry_connectionInfor = "wss://dev.wev-charger.com:12200/v1.6/NYJ-TEST0001";

            String ip_header = mTable_Setting.getSettingData((int)CONST_INDEX_OCPP_Setting.infor_csms_ip_header);
            String ip = mTable_Setting.getSettingData((int)CONST_INDEX_OCPP_Setting.infor_csms_ip);
            String port = mTable_Setting.getSettingData((int)CONST_INDEX_OCPP_Setting.infor_csms_port);
            String more = mTable_Setting.getSettingData((int)CONST_INDEX_OCPP_Setting.infor_csms_ip_more);
            String chargeBoxSerialNumber = mTable_Setting.getSettingData((int)CONST_INDEX_OCPP_Setting.infor_ChargeBoxSerialNumber);

            //        String softberry_connectionInfor = "ws://222.106.156.156:12200/v1.6/NYJ-TEST0001";
            //        String url2 = softberry_connectionInfor;//Softberry

            String url2 = ip_header + "://" + ip + ":" + port + more + "/" + chargeBoxSerialNumber;
            //        url2 = "wss://dev.wev-charger.com:12200/v1.6/FT0001";
            //Request request = new Request.Builder().url(url2)
            //        //                .addHeader("Host", "wss://dev.wev-charger.com")
            //        .addHeader("Upgrade", "websocket")
            //        //                .addHeader("Connection", "Upgrade")
            //        //                .addHeader("Sec-WebSocket-Key", "x3JJHMbDL1EzLkh9GBhXDw==")
            //        .addHeader("Sec-WebSocket-Protocol", "ocpp1.6")//Default
            //        .build();

            //mWebSocket = mClient.newWebSocket(request, mSocketListener);
        }
    }

}
