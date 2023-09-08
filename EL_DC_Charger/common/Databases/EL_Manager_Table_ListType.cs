using common.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.Databases
{
    abstract public class EL_Manager_Table_ListType : EL_Manager_Table
    {

        public EL_Manager_Table_ListType(EL_Manager_SQLite manager_SQLite, string tableName, bool maketable) : base(manager_SQLite, tableName, maketable)
        {
            
        }



    }
}
