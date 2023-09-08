using EL_DC_Charger.ocpp.ver16.platform.wev.datatype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.interf
{
    public interface IChargeUnit_ChangeListener
    {
        void onChange_ChargeUnit(int connectorId, String operatorType, List<UnitPriceTable> unitPriceTable);
    }
}
