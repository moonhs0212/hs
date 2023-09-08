using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.Manager
{
    public class EL_Manager_Enum<T>
    {
        public static T Parse(string s)
        {
            return (T)Enum.Parse(typeof(T), s);
        }

    }
}
