using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.database
{
    public enum CONST_INDEX_OCPP_Setting
    {
        ////////////////////////////////////////////0
        AllowOfflineTxForUnknownId = 0,
        AuthorizationCacheEnabled,
        AuthorizeRemoteTxRequests,
        BlinkRepeat,
        ClockAlignedDataInterval,

        ConnectionTimeOut,
        ConnectorPhaseRotation,
        ConnectorPhaseRotationMaxLength,
        GetConfigurationMaxKeys,
        HeartbeatInterval,
        //////////////////////////////////////////10
        LightIntensity,
        //whether the Charge Point, when offline, will start a transaction for locally-authorized identifiers.
        //청구 지점이 오프라인일 때 현지에서 승인된 식별자에 대한 트랜잭션을 시작할지 여부.
        LocalAuthorizeOffline,
        //whether the Charge Point, when online, will start a transaction for locally-authorized identifiers without waiting for or
        //requesting an Authorize.conf from the Central System
        //충전 지점이 온라인일 때 중앙 시스템에서 Authorize.conf를 기다리거나 요청하지 않고 로컬에서 인증된 식별자에 대한 트랜잭션을 시작할지 여부
        LocalPreAuthorize,
        MaxEnergyOnInvalidId,
        MeterValuesAlignedData,

        MeterValuesAlignedDataMaxLength,
        MeterValuesSampledData,
        MeterValuesSampledDataMaxLength,
        MeterValueSampleInterval,
        MinimumStatusDuration,
        //////////////////////////////////////////20
        NumberOfConnectors,
        ResetRetries,
        StopTransactionOnEVSideDisconnect,
        StopTransactionOnInvalidId,
        StopTxnAlignedData,

        StopTxnAlignedDataMaxLength,
        StopTxnSampledData,
        StopTxnSampledDataMaxLength,
        SupportedFeatureProfiles,
        SupportedFeatureProfilesMaxLength,
        //////////////////////////////////////////30
        TransactionMessageAttempts,
        TransactionMessageRetryInterval,
        UnlockConnectorOnEVSideDisconnect,
        WebSocketPingInterval,
        //9.2. Local Auth List Management Profile
        LocalAuthListEnabled,

        LocalAuthListMaxLength,
        SendLocalListMaxLength,
        //9.3. Reservation Profile
        ReserveConnectorZeroSupported,
        //9.4. Smart Charging Profile
        ChargeProfileMaxStackLevel,
        ChargingScheduleAllowedChargingRateUnit,
        //////////////////////////////////////////
        ChargingScheduleMaxPeriods,
        ConnectorSwitch3to1PhaseSupported,
        MaxChargingProfilesInstalled,
        /////////////////////////
        LocalListVersion,
        infor_ChargeBoxSerialNumber,
        infor_chargePointModel,
        infor_chargePointSerialNumber,
        infor_chargePointVendor,
        infor_iccid,
        infor_imsi,
        infor_meterSerialNumber,
        infor_meterType,
        infor_interval_Heartbeat,
        infor_csms_ip,
        infor_csms_port,
        infor_csms_ip_more,
        infor_csms_ip_header

    }

    
}
