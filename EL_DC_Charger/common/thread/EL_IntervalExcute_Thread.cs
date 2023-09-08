using EL_DC_Charger.common.application;

using ParkingControlCharger.baseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.thread
{
    public class EL_IntervalExcute_Thread : EL_Thread_Base
    {
        protected List<EL_IntervalExcute_Item_Base> mItems = new List<EL_IntervalExcute_Item_Base>();

        public void addItem(EL_IntervalExcute_Item_Base item)
        {
            lock(mItems)
            {
                if(!mItems.Contains(item))
                {
                    mItems.Add(item);
                }
            }
        }

        public void removeItem(EL_IntervalExcute_Item_Base item)
        {
            lock (mItems)
            {
                mItems.Remove(item);    
            }
        }

        public EL_IntervalExcute_Thread(EL_MyApplication_Base application, bool isNeedAdd) : base(application, 20, isNeedAdd)
        {
        }

        public override void initVariable()
        {
            
        }

        public override void intervalExcute()
        {
            lock (mItems)
            {
                foreach (EL_IntervalExcute_Item_Base item in mItems)
                {
                    item.run();
                }
            }
                
        }
    }
}
