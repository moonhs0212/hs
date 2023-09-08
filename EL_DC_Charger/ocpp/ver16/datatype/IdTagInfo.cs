using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.datatype
{
    public class IdTagInfo
    {
        public AuthorizationStatus? status;//: ,
        public String expiryDate;//: LocalDateTime? = null,
        public String parentIdTag;//: String? = null
    }
}
