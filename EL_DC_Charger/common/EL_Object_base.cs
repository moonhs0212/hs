using EL_DC_Charger.common.application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common
{
    abstract public class EL_Object_Base : IEL_Object_Base
    {
        protected EL_MyApplication_Base mApplication = null;

        public EL_Object_Base(EL_MyApplication_Base application)
        {
            mApplication = application;
        }


        public EL_MyApplication_Base getApplication()
        {
            return mApplication;
        }

        public EL_MyApplication_Base getMyApplication()
        {
            return mApplication;
        }

        abstract public void initVariable();
    }
}
