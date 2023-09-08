using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.ConstVariable
{
    public class CsSettingControls
    {
        public string title { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string value { get; set; }

        public CsSettingControls(string _title, string _name, string _type, string _value)
        {
            this.title = _title;
            this.name = _name;
            this.type = _type;
            this.value = _value;
        }
    }

}
