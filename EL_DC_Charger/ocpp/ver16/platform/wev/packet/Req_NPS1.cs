using EL_DC_Charger.common.item;
using EL_DC_Charger.EL_DC_Charger.SerialPorts.smartro_tl3500bs.Packet.Child;
using EL_DC_Charger.ocpp.ver16.platform.wev.datatype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.platform.wev.packet
{
    public class Req_NPS1
    {
        public int connectorId;
        public int paymentResult;
        public String chargingLimitProfile;
        public String unitAmount;
        public BillingInfor billingInfo;

        public void setRequiredValue(
            int connectorId,
            String chargingLimitProfile,
            Smartro_TL3500BS_Packet_AddInfor_Deal_Request_Receive_By_Request packet_firstDeal)

        {
            this.connectorId = connectorId;
            this.chargingLimitProfile = chargingLimitProfile;

            if (packet_firstDeal.Deal_DivideCode != (byte)'X')
                paymentResult = 1;
            else
                paymentResult = 0;

            unitAmount = "" + packet_firstDeal.Amount_Approval;

            billingInfo = new BillingInfor();
            billingInfo.payCharge = packet_firstDeal.Amount_Approval;
            EL_Time time = new EL_Time();
            //time.setTime(packet_firstDeal.Selling_Date + " " + packet_firstDeal.Selling_Time);
            billingInfo.payTransactionDT = time.toString_OCPP();
            billingInfo.payApprovalNumber = packet_firstDeal.Approval_Number;
            billingInfo.payUniqueNumber = packet_firstDeal.DealUniqNumber;
            if (packet_firstDeal.ResponseCode != null && packet_firstDeal.ResponseCode.Length > 0)
                billingInfo.payResponseCode = packet_firstDeal.ResponseCode;
            //billingInfo.payResponseMessage = packet_firstDeal.;
            //billingInfo.cardResponseCode = packet_firstDeal.

        }
    }

}
