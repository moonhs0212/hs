using EL_DC_Charger.BatteryChange_Charger.ChargerVariable;
using EL_DC_Charger.BatteryChange_Charger.SerialPorts.IOBoard;
using EL_DC_Charger.Controller;
using EL_DC_Charger.Interface_Common;
using EL_DC_Charger.common.chargingcontroller;
using EL_DC_Charger.common.interf;
using EL_DC_Charger.common.statemanager;
using EL_DC_Charger.common.thread;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.ChargerInfor;
using EL_DC_Charger.EL_DC_Charger.SerialPorts.Iksung_RFReader;
using EL_DC_Charger.EL_DC_Charger.SerialPorts.ODHitec_AMI;
using EL_DC_Charger.EL_DC_Charger.SerialPorts.smartro_tl3500bs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.keypad;

using EL_DC_Charger.EL_DC_Charger.ChargerVariable;
using common.Database;
using EL_DC_Charger.common.variable;
using EL_DC_Charger.ocpp.ver16.database;
using EL_DC_Charger.ocpp.ver16.comm;
using EL_DC_Charger.ocpp.ver16.interf;
using EL_DC_Charger.EL_DC_Charger.Manager_UserControl;
using EL_DC_Charger.ocpp.ver16.infor;
using Newtonsoft.Json;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.setting;
using ParkingControlCharger.Object;
using EL_DC_Charger.common.INI;
using System.Windows.Forms;
using EL_DC_Charger.common.ChargerVariable;
using EL_DC_Charger.Utils;
using EL_DC_Charger.EL_DC_Charger.ConstVariable;
using AxWMPLib;
using System.Data.SQLite;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.manager;
using System.Windows.Forms.VisualStyles;
using EL_DC_Charger.ChargerInfor;

namespace EL_DC_Charger.common.application
{
    abstract public class EL_MyApplication_Base
    {

        //디버그용 (테스트용)
        public bool debug = false;

        public int getChargerPower_kW()
        {
            return 100;
        }

        public static int SETTING_TOUCH_DELAY = 5;


        //동적 환경설정 변수
        public static List<CsSettingControls> list_setting = new List<CsSettingControls>();



        public static bool IS_HMI_SCREEN_SIZE_VARIABLE = false;
        public static int HMI_SCREEN_INDEX = 0;

        public static EHmi_Screen_Mode HMI_SCREEN_MODE = EHmi_Screen_Mode.P1080_1920;
        public static bool IS_HMI_SCREEN_TOP = false;

        //prefaring 동시 호출로 인해 검교정 화면 두개 뜨는거 방지
        public bool bManualScreenDone = false;

        public const int INDEX_MONITOR = 0;
        public EL_MyApplication_Base()
        {

            mJsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            mJsonSerializerSettings.Formatting = Formatting.None;
            mJsonSerializerSettings.StringEscapeHandling = StringEscapeHandling.EscapeNonAscii;
            //mJsonSerializerSettings.FloatFormatHandling = FloatFormatHandling.String;
            //mJsonSerializerSettings.StringEscapeHandling = StringEscapeHandling.EscapeHtml;
            //mJsonSerializerSettings.ContractResolver.ResolveContract(Type.)
        }

        public static JsonSerializerSettings mJsonSerializerSettings = new JsonSerializerSettings();


        public bool bOCPP_Support_CORE = true;
        public bool bOCPP_Support_FIRMWAREMANAGEMENT = false;
        public bool bOCPP_Support_CORE_LOCALAUTHLISTMANAGEMENT = false;
        public bool bOCPP_Support_CORE_REMOTETRIGGE = false;
        public bool bOCPP_Support_CORE_RESERVATION = false;
        public bool bOCPP_Support_CORE_SMARTCHARGING = false;

        public const bool TEST_OCPP = false;

        public const string PASSWORD_HIGH_ADMIN = "1570953";
        abstract public EAdminMode getAdminMode(string inputpassword);
        abstract protected void setManager_SQLite_Setting();
        abstract protected void setDataManager_CustomUC_Main();
        abstract protected void setStateManager_Main();
        abstract protected void setChannelTotalInfor();



        public static int SCREEN_WIDTH
        {
            get
            {
                switch (HMI_SCREEN_MODE)
                {
                    case EHmi_Screen_Mode.P1080_1920:
                        return 1080;
                    default:
                    case EHmi_Screen_Mode.P1024_600:
                    case EHmi_Screen_Mode.NONE:
                        return 1024;
                }
            }
        }
        public static int SCREEN_HEIGHT
        {
            get
            {
                switch (HMI_SCREEN_MODE)
                {
                    case EHmi_Screen_Mode.P1080_1920:
                        return 1920;
                    default:
                    case EHmi_Screen_Mode.P1024_600:
                    case EHmi_Screen_Mode.NONE:
                        return 600;
                }
            }
        }







        protected EAdminMode mAdminMode = EAdminMode.NONE;
        public EAdminMode getAdminMode()
        {
            return mAdminMode;
        }
        public void setAdminMode(EAdminMode mode)
        {
            mAdminMode = mode;
        }

        Manager_Touch touch_manager;
        public void setTouchManger(UserControl c)
        {
            touch_manager = new Manager_Touch(c);
        }
        public DateTime Touch_dt;
        protected EL_Manager_SQLite_Setting mManager_SQLite_Setting = null;
        public EL_Manager_SQLite_Setting getManager_SQLite_Setting() { return mManager_SQLite_Setting; }


        protected EChargerType mChargerType = EChargerType.CH1_PUBLIC;



        public ECharge_CH_Type _mChargeCH_Type;

        public ECharge_CH_Type mChargeCH_Type
        {
            get
            {
                _mChargeCH_Type = (ECharge_CH_Type)(int)Enum.Parse(typeof(ECharge_CH_Type), CsINIManager.IniReadValue(Application.StartupPath + @"\Config.ini", "MODE", "EACH", "ALL"));
                return _mChargeCH_Type;
            }
            set => _mChargeCH_Type = value;
        }

        public void setChargerType(EChargerType chargerType) { mChargerType = chargerType; }
        public EChargerType getChargerType()
        {

            return mChargerType;
        }
        //비회원가
        public float NonmemberAmount { get; set; }
        //회원가
        public float MemberAmount { get; set; }

        public float CurrentAmount;

        //충전 다되갈때 전압 낮추려고 만듬.
        public bool slowMode;

        protected EChargerMaker mChargerMaker = EChargerMaker.Elelectric;

        public bool TouchManagerInit = false;


        public string lastRebootDate
        {
            get { return CsINIManager.IniReadValue(Application.StartupPath + @"\Config.ini", "REBOOT", "DAY", ""); }
            set { CsINIManager.IniWriteValue(Application.StartupPath + @"\Config.ini", "REBOOT", "DAY", value); }
        }

        public void setChargerMaker(EChargerMaker chargerMaker) { mChargerMaker = chargerMaker; }
        public EChargerMaker getChargerMaker()
        {
            return mChargerMaker;
        }



        public EL_Charger_DataManager_CustomUserControl_Main_Base mPageManager_Main()
        {
            return mDataManager_CustomUC_Main;
        }



        protected EL_Charger_DataManager_CustomUserControl_Main_Base mDataManager_CustomUC_Main = null;
        public EL_Charger_DataManager_CustomUserControl_Main_Base getDataManager_CustomUC_Main()
        {
            return mDataManager_CustomUC_Main;
        }





        protected EPlatform mPlatform = EPlatform.WEV;
        public void setPlatform(EPlatform platform) { mPlatform = platform; }
        public EPlatform getPlatform()
        {
            return mPlatform;
        }

        public bool offlineTest_isuse = false;

        public string fromServiceMonth { get; set; }
        public string fromServiceDay { get; set; }

        public string toServiceMonth { get; set; }
        public string toServiceDay { get; set; }

        public bool calibrationMode { get; set; }

        protected EPlatformOperator mPlatform_Operator = EPlatformOperator.WEV;
        public void setPlatform_Operator(EPlatformOperator platform_operator) { mPlatform_Operator = platform_operator; }
        public EPlatformOperator getPlatform_Operator()
        {
            return mPlatform_Operator;
        }

        public EAmiCompany EAmiCompany { get; set; }
        public bool isTrd { get; set; }

        protected EL_IntervalExcute_Thread mManager_IntervalExcute = null;
        public EL_IntervalExcute_Thread getManager_IntervalExcute() { return mManager_IntervalExcute; }
        protected void setManager_IntervalExcute()
        {
            mManager_IntervalExcute = new EL_IntervalExcute_Thread(this, true);
            mManager_IntervalExcute.start();

        }
        public byte[] lastDealInfo { get; set; }

        public const bool IsShow_Size = false;

        public const int COUNT_SOCKET = 8;

        public static bool bIsScreen_Width_1024 = false;

        protected EL_ChannelTotalInfor_Base[] mChannelTotalInfor = null;




        protected int mChannelCount;
        public EL_ChannelTotalInfor_Base[] getChannelTotalInfor()
        {
            return mChannelTotalInfor;
        }

        public EL_ChannelTotalInfor_Base getChannelTotalInfor(int channelIndex)
        {
            return mChannelTotalInfor[channelIndex - 1];
        }


        public int getChannelCount()
        {
            mChannelCount = Convert.ToInt16(CsINIManager.IniReadValue(Application.StartupPath + @"\Config.ini", "CHANNEL", "CH", "1"));
            return mChannelCount;
        }



        protected int mSystemMode = CSystemMode.NORMAL;

        protected Form_Setting_Main mForm_Setting_Main = null;
        public Form_Setting_Main Form_Setting_Main
        {
            get { return mForm_Setting_Main; }
            set { mForm_Setting_Main = value; }
        }

        public void setSystemMode(int systemMode)
        {
            switch (systemMode)
            {

            }
            mSystemMode = systemMode;
        }

        public int SystemMode
        {
            get { return mSystemMode; }
        }


        public bool IS_TEST
        {
            get { return false; }
        }

        protected static EL_MyApplication_Base myApplication = null;



        protected IMainForm mMainForm = null;
        public IMainForm MainForm
        {
            get { return mMainForm; }
            set
            {
                mMainForm = value;
            }
        }

        protected ChargingController_Base mController_Main = null;
        public ChargingController_Base Controller_Main
        {
            get { return mController_Main; }
        }

        protected ChargingController_Base mController_Cert_Channel = null;
        public ChargingController_Base Controller_Cert_Channel
        {
            get { return mController_Cert_Channel; }
        }

        public EL_StateManager_Main_Base mStateManager_Main = null;

        public EL_StateManager_Main_Base getStateManager_Main()
        {
            return mStateManager_Main;
        }

        public Wev_Form_Keypad_OnlyNumber mKeyPad_OnlyNumber = new Wev_Form_Keypad_OnlyNumber();


        #region #####    OCPP관련
        protected OCPP_Comm_Manager mOCPP_Comm_Manager = null;
        public OCPP_Comm_Manager getOCPP_Comm_Manager()
        {
            return mOCPP_Comm_Manager;
        }

        protected void setOCPP_Comm_Manager()
        {
            mOCPP_Comm_Manager = new OCPP_Comm_Manager(this);
            mOCPP_Comm_Manager.initVariable();
            mOCPP_Comm_Manager.start();
        }

        protected OCPP_Manager_SQLite_Setting mManager_SQLite_Setting_OCPP = null;
        public OCPP_Manager_SQLite_Setting getManager_SQLite_Setting_OCPP() { return mManager_SQLite_Setting_OCPP; }
        protected void setOCPP_Manager_SQLite_Setting()
        {
            mManager_SQLite_Setting_OCPP = new OCPP_Manager_SQLite_Setting();
            //mManager_SQLite_Setting_OCPP.setManager_Table();
        }

        protected OCPP_MainInfor mOCPP_MainInfor = null;
        public OCPP_MainInfor getOCPP_MainInfor()
        {
            return mOCPP_MainInfor;
        }
        #endregion #####    OCPP관련
        virtual public void initVariable()
        {
            //로그 DB오픈
            HistoryDBHelper.SqlliteConn();
            //테이블 없으면 만듬.
            HistoryDBHelper.CreateHistoryTable();
            HistoryDBHelper.CreatebillingInfoTable();
            HistoryDBHelper.CreateunitPriceTable();
            ///////////////////////////////

            setManager_IntervalExcute();
            setManager_SQLite_Setting();

            setOCPP_Manager_SQLite_Setting();

            string strChargerType = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).getSettingData(CONST_INDEX_MAINSETTING.CHARGERTYPE);
            string strChargerMaker = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).getSettingData(CONST_INDEX_MAINSETTING.CHARGERMAKER);
            string strPlatform = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).getSettingData(CONST_INDEX_MAINSETTING.PLATFORM);
            string strPlatformOperator = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).getSettingData(CONST_INDEX_MAINSETTING.PLATFORMOPERATOR);

            EL_DC_Charger_MyApplication.getInstance().setChargerType((EChargerType)Enum.Parse(typeof(EChargerType), strChargerType));
            EL_DC_Charger_MyApplication.getInstance().setChargerMaker((EChargerMaker)Enum.Parse(typeof(EChargerMaker), strChargerMaker));
            EL_DC_Charger_MyApplication.getInstance().setPlatform((EPlatform)Enum.Parse(typeof(EPlatform), strPlatform));
            EL_DC_Charger_MyApplication.getInstance().setPlatform_Operator((EPlatformOperator)Enum.Parse(typeof(EPlatformOperator), strPlatformOperator));


            EL_DC_Charger_MyApplication.getInstance().fromServiceMonth = CsINIManager.IniReadValue(Application.StartupPath + @"\Config.ini", "DAYS", "fromMonth");
            EL_DC_Charger_MyApplication.getInstance().fromServiceDay = CsINIManager.IniReadValue(Application.StartupPath + @"\Config.ini", "DAYS", "fromDay");
            EL_DC_Charger_MyApplication.getInstance().toServiceMonth = CsINIManager.IniReadValue(Application.StartupPath + @"\Config.ini", "DAYS", "toMonth");
            EL_DC_Charger_MyApplication.getInstance().toServiceDay = CsINIManager.IniReadValue(Application.StartupPath + @"\Config.ini", "DAYS", "toDay");
            //검/교정모드
            EL_DC_Charger_MyApplication.getInstance().calibrationMode = Convert.ToBoolean(Convert.ToInt16(CsINIManager.IniReadValue(Application.StartupPath + @"\Config.ini", "MODE", "CALIBRATION", "0")));
            //AMI 제조사 선택
            string strAmiCompany = CsINIManager.IniReadValue(Application.StartupPath + @"\Config.ini", "AMI", "NAME", EAmiCompany.Pilot.ToString());
            EL_DC_Charger_MyApplication.getInstance().EAmiCompany = ((EAmiCompany)Enum.Parse(typeof(EAmiCompany), strAmiCompany));

            //trd버전 여부
            EL_DC_Charger_MyApplication.getInstance().isTrd = Convert.ToBoolean(Convert.ToInt16(CsINIManager.IniReadValue(Application.StartupPath + @"\Config.ini", "BOARD", "TRD_YN", "1")));

            EL_DC_Charger_MyApplication.getInstance().MemberAmount = int.Parse(CsINIManager.IniReadValue(Application.StartupPath + @"\Config.ini", "AMOUNT", "MEMBER", "189"));
            EL_DC_Charger_MyApplication.getInstance().NonmemberAmount = int.Parse(CsINIManager.IniReadValue(Application.StartupPath + @"\Config.ini", "AMOUNT", "NON_MEMBER", "189"));

            string strSizeMode = CsINIManager.IniReadValue(Application.StartupPath + @"\Config.ini", "SIZE", "SIZE", EHmi_Screen_Mode.P1080_1920.ToString());
            HMI_SCREEN_MODE = ((EHmi_Screen_Mode)Enum.Parse(typeof(EHmi_Screen_Mode), strSizeMode));


            //INI 섹션 수를 가져올 수가 없어서 여유롭게 루핑하다 없으면 빠져나오는걸로 함
            for (int i = 0; i < 99; i++)
            {
                string _title = CsINIManager.IniReadValue(Application.StartupPath + @"\Config.ini", "CONTROL-" + i, "TITLE");
                string _name = CsINIManager.IniReadValue(Application.StartupPath + @"\Config.ini", "CONTROL-" + i, "NAME");
                string _type = CsINIManager.IniReadValue(Application.StartupPath + @"\Config.ini", "CONTROL-" + i, "TYPE");
                string _value = CsINIManager.IniReadValue(Application.StartupPath + @"\Config.ini", "CONTROL-" + i, "VALUE");

                if (_name.Equals(""))
                    break;
                else
                    list_setting.Add(new CsSettingControls(_title, _name, _type, _value));
            }

            ///////////////////////////예시////////////////////
            //int a = list_setting.FindIndex(x => x.title.Equals("")); //타이틀 이름으로 INDEX찾음.
            //int b = list_setting.FindIndex(x => x.title.Equals(""));
            //object value1 = list_setting[a].value;
            ///////////////////////////////////////////////////            

            mOCPP_MainInfor = new OCPP_MainInfor(this);
            mOCPP_MainInfor.initVariable();

            setStateManager_Main();

            if (myApplication.getChargerType() == EChargerType.CH1_PUBLIC || myApplication.getChargerType() == EChargerType.CH1_CERT || myApplication.getChargerType() == EChargerType.CH2_CERT
                && myApplication.getPlatform() != EPlatform.NONE)
                setOCPP_Comm_Manager();

            String text = getManager_SQLite_Setting().getTable_Setting(0).getSettingData(CONST_INDEX_MAINSETTING.CHARGERMAKER);
            //try
            //{
            //    mChargerMaker = (EChargerMaker)Enum.Parse(typeof(EChargerMaker), text);
            //}
            //catch(ArgumentException e)
            //{
            //    mChargerMaker = EChargerMaker.NONE;
            //}


            //text = getManager_SQLite_Setting().getTable_Setting(0).getSettingData(CONST_INDEX_MAINSETTING.CHARGERTYPE);
            //try
            //{
            //    mChargerType = (EChargerType)Enum.Parse(typeof(EChargerType), text);
            //}
            //catch (ArgumentException e)
            //{
            //    mChargerType = EChargerType.NONE;
            //}

            //text = getManager_SQLite_Setting().getTable_Setting(0).getSettingData(CONST_INDEX_MAINSETTING.PLATFORM);
            //try
            //{
            //    mPlatform = (EPlatform)Enum.Parse(typeof(EChargerType), text);
            //}
            //catch (ArgumentException e)
            //{
            //    mPlatform = EPlatform.NONE;
            //}


            //text = getManager_SQLite_Setting().getTable_Setting(0).getSettingData(CONST_INDEX_MAINSETTING.PLATFORMOPERATOR);
            //try
            //{
            //    mPlatform_Operator = (EPlatformOperator)Enum.Parse(typeof(EPlatformOperator), text);
            //}
            //catch (ArgumentException e)
            //{
            //    mPlatform_Operator = EPlatformOperator.NONE;
            //}


            setChannelTotalInfor();
            setDataManager_CustomUC_Main();

            mStateManager_Main.initVariable();
            mStateManager_Main.setStart();

            for (int i = 0; i < mChannelTotalInfor.Length; i++)
            {
                mChannelTotalInfor[i].getStateManager_Channel().initVariable();
                mChannelTotalInfor[i].getStateManager_Channel().setStart();
            }
            setManager_DI((IDI_Manager)mSerialPort_ControlBd);
            mSerialPort_ControlBd = new EL_ControlbdComm_Comm_Manager((EL_DC_Charger_MyApplication)this);
            mSerialPort_ControlBd.initVariable();
            mSerialPort_ControlBd.commOpen();
            mSerialPort_ControlBd.setStart();
            getManager_IntervalExcute().addItem(mSerialPort_ControlBd);
            mList_Comm.Add(mSerialPort_ControlBd);

            //mSerialPort_RFReader = new Iksung_RFReader_Comm_Manager((EL_DC_Charger_MyApplication)this);
            //mSerialPort_RFReader.initVariable();
            //mSerialPort_RFReader.commOpen();
            //mSerialPort_RFReader.setStart();
            //getManager_IntervalExcute().addItem(mSerialPort_RFReader);
            //mList_Comm.Add(mSerialPort_RFReader);

            Smartro_TL3500BS_Comm_Manager mSerialPort_Smartro_CardReader = new Smartro_TL3500BS_Comm_Manager((EL_DC_Charger_MyApplication)this);
            mSerialPort_Smartro_CardReader.initVariable();
            mSerialPort_Smartro_CardReader.commOpen();
            mSerialPort_Smartro_CardReader.setStart();
            this.mSerialPort_Smartro_CardReader = mSerialPort_Smartro_CardReader;
            getManager_IntervalExcute().addItem(mSerialPort_Smartro_CardReader);


            CardDevice_Manager = mSerialPort_Smartro_CardReader;
            RFCardReader_Manager = mSerialPort_Smartro_CardReader;

            Form1.mIntervalExcute_List_For_Timer.addItem(mStateManager_Main);

            for (int i = 0; i < mChannelTotalInfor.Length; i++)
            {
                Form1.mIntervalExcute_List_For_Timer.addItem(mChannelTotalInfor[i].getStateManager_Channel());
            }

            mAMI_Comm_Manager = new ODHitec_AMI_Comm_Manager(this, true);
            mAMI_Comm_Manager.start();
            if (mOCPP_Comm_Manager != null)
            {
                mOCPP_Comm_Manager.getSendMgr().setStateManager();
                mOCPP_Comm_Manager.setCommand_Connect(true);
            }
        }

        ODHitec_AMI_Comm_Manager mAMI_Comm_Manager = null;

        protected IDI_Manager mDI_Manager = null;
        public IDI_Manager DI_Manager
        {
            get { return mDI_Manager; }
            set { mDI_Manager = value; }
        }

        //재시작시 금액,충전시작시간 가져올때 사용
        public List<string> ListPrice = new List<string>();


        public void close()
        {
            foreach (MManager_Comm manager in mList_Comm)
            {
                manager.destroy();
            }
            mList_Comm.Clear();
        }

        protected List<MManager_Comm> mList_Comm = new List<MManager_Comm>();


        protected Smartro_TL3500BS_Comm_Manager mSerialPort_Smartro_CardReader = null;
        public Smartro_TL3500BS_Comm_Manager SerialPort_Smartro_CardReader
        {
            get { return mSerialPort_Smartro_CardReader; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected EL_ControlbdComm_Comm_Manager mSerialPort_ControlBd = null;
        protected Iksung_RFReader_Comm_Manager mSerialPort_RFReader = null;

        public EL_ControlbdComm_Comm_Manager getSerialPort_ControlBd()
        {
            return mSerialPort_ControlBd;
        }

        public Iksung_RFReader_Comm_Manager SerialPort_RFReader
        {
            get { return mSerialPort_RFReader; }
        }

        public IRFCardReader_Manager RFCardReader_Manager
        {
            get { return mRFCardReader_Manager; }
            set { mRFCardReader_Manager = value; }
        }

        protected IRFCardReader_Manager mRFCardReader_Manager = null;

        public ICardDevice_Manager CardDevice_Manager
        {
            get { return mCardDevice_Manager; }
            set { mCardDevice_Manager = value; }
        }

        protected ICardDevice_Manager mCardDevice_Manager = null;
        protected IDI_Manager mManager_DI = null;

        public IDI_Manager getManager_DI()
        {
            return mManager_DI;
        }
        public void setManager_DI(IDI_Manager manager_DI)
        {
            mManager_DI = manager_DI;
        }
    }
}