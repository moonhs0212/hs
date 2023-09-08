using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.ConstVariable
{
    public class Const_ErrorCode
    {
        public const int DIVIDECODE_HMI = 100000;
        public const int CODE_0001_CAR_FULL_CHARGING_COMPLETE = 1;
        public const int CODE_0002_USER_COMPLETE = 2;
        public const int CODE_0003_CONNECTOR_DISCONNECT = 3;
        public const int CODE_0004_SERVER_COMMAND_STOP = 4;
        public const int CODE_0005_SOFT_RESET = 5;
        public const int CODE_0006_HARD_RESET = 6;
        public const int CODE_0007_CHARGINGSTOP_BY_TARGET_EP = 7;
        public const int CODE_0008_CHARGINGSTOP_BY_TARGET_VALUE = 8;
        public const int CODE_0009_CHARGINGSTOP_BY_TARGET_SOC = 9;
        public const int CODE_0010_OPERATION_STOP = 10;
        public const int CODE_0011_REMOTE_STOP_TRANSACTION = 11;
        public const int CODE_0012_SERVER_CERTIFICATION_ERROR = 12;
        public const int CODE_0013_NONMEMBER_PAYMENT_ERROR = 13;
        public const int CODE_0014_OVERWAITTIME_CONNECTCONNECTOR = 14;
        public const int CODE_0015_OVERWAITTIME_CHARGINGSTART = 15;
        public const int CODE_0016_CLICK_BACKBUTTON = 16;
        public const int CODE_0017_CLICK_CHARGINGSTOPBUTTON = 17;
        public const int CODE_0018_OVERWAITTIME = 18;
        public const int CODE_0020_COMMERROR_AMI = 20;


        public const int CODE_0100_OVER_VOLTAGE_INPUT = 100;
        public const int CODE_0101_LOW_VOLTAGE_INPUT = 101;
        public const int CODE_0102_OVER_CURRENT_INPUT = 102;
        public const int CODE_0103_OVER_VOLTAGE_OUTPUT = 103;
        public const int CODE_0104_LOW_VOLTAGE_OUTPUT = 104;
        public const int CODE_0105_OVER_CURRENT_OUTPUT = 105;
        public const int CODE_0106_MC_ERROR_INPUT = 106;
        public const int CODE_0107_MC_ERROR_OUTPUT = 107;
        public const int CODE_0108_COMM_ERROR_RFID = 108;
        public const int CODE_0109_COMM_ERROR_MODEM = 109;
        public const int CODE_0110_COMM_ERROR_CONTROLBOARD = 110;
        public const int CODE_0111_COMM_ERROR_AMI = 111;
        public const int CODE_0112_COMM_ERROR_PLCMODEM = 112;
        public const int CODE_0113_COMM_ERROR_CAN = 113;
        public const int CODE_0114_COMM_ERROR_POWERPACK = 114;
        public const int CODE_0115_COMM_ERROR_CP = 115;
        public const int CODE_0116_EMERGENCY = 116;


        public static String getErrorMessage(int message)
        {
            switch (message)
            {
                case CODE_0001_CAR_FULL_CHARGING_COMPLETE:
                    return "차량 완충 종료";
                case CODE_0002_USER_COMPLETE:
                    return "사용자 종료";
                case CODE_0003_CONNECTOR_DISCONNECT:
                    return "커넥터 분리";
                case CODE_0004_SERVER_COMMAND_STOP:
                    return "서버 요청 종료";
                case CODE_0005_SOFT_RESET:
                    return "소프트 리셋 ";
                case CODE_0006_HARD_RESET:
                    return "하드 리셋";
                case CODE_0007_CHARGINGSTOP_BY_TARGET_EP:
                    return "전력량 도달 종료";
                case CODE_0008_CHARGINGSTOP_BY_TARGET_VALUE:
                    return "금액 도달 종료";
                case CODE_0009_CHARGINGSTOP_BY_TARGET_SOC:
                    return "SOC 도달 종료";
                case CODE_0010_OPERATION_STOP:
                    return "운영 중지";
                case CODE_0011_REMOTE_STOP_TRANSACTION:
                    return "거래 종료 요청";
                case CODE_0012_SERVER_CERTIFICATION_ERROR:
                    return "서버인증오류";

                case CODE_0100_OVER_VOLTAGE_INPUT:
                    return "입력과전압";
                case CODE_0101_LOW_VOLTAGE_INPUT:
                    return "입력저전압";
                case CODE_0102_OVER_CURRENT_INPUT:
                    return "입력과전류";
                case CODE_0103_OVER_VOLTAGE_OUTPUT:
                    return "출력과전압";
                case CODE_0104_LOW_VOLTAGE_OUTPUT:
                    return "출력과전류";
                case CODE_0105_OVER_CURRENT_OUTPUT:
                    return "충전기 과온";
                case CODE_0106_MC_ERROR_INPUT:
                    return "입력 MC 오류";
                case CODE_0107_MC_ERROR_OUTPUT:
                    return "출력 MC 오류";
                case CODE_0108_COMM_ERROR_RFID:
                    return "RFID 통신 오류";
                case CODE_0109_COMM_ERROR_MODEM:
                    return "통신 모뎀 오류";
                case CODE_0110_COMM_ERROR_CONTROLBOARD:
                    return "제어보드 통신 오류";
                case CODE_0111_COMM_ERROR_AMI:
                    return "전력량계 통신 오류";
                case CODE_0112_COMM_ERROR_PLCMODEM:
                    return "PLC 모뎀 통신 오류";
                case CODE_0113_COMM_ERROR_CAN:
                    return "CAN 통신 오류";
                case CODE_0114_COMM_ERROR_POWERPACK:
                    return "파워팩 통신 오류";
                case CODE_0115_COMM_ERROR_CP:
                    return "CP 오류";
                case CODE_0116_EMERGENCY:
                    return "비상 버튼 동작";
                default:
                    return "알수없음";
            }
        }
    }
}
