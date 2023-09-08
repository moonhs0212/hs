using EL_DC_Charger.common.application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.thread
{
    abstract public class EL_Thread_Base : EL_Object_Base
    {

        protected int mInterval = 0;

        protected Thread mThread = null;
        protected bool bIsNeedAdd = false;

        public EL_Thread_Base(EL_MyApplication_Base application, int interval, bool isNeedAdd) : base(application)
        {
            mInterval = interval;
            bIsNeedAdd = isNeedAdd;
            if (mInterval > 0)
            {
                bCommand_Stop = false;
                mThread = new Thread(run);
                mThread.IsBackground = true;
            }


        }

        protected bool bCommand_Pause = false;
        public void setCommand_Pause(bool setting)
        {
            bCommand_Pause = setting;
        }

        private void run()
        {
            while (!bCommand_Stop)
            {
                intervalExcute();
                Thread.Sleep(mInterval);
            }
        }

        abstract public void intervalExcute();

        public void start()
        {
            if (mInterval > 0)
            {
                bCommand_Stop = false; ;
                mThread.Start();
            }

        }
        public void stop()
        {
            bCommand_Stop = true;
            if (mThread != null)
            {
                mThread.Abort();
                mThread = null;
            }
        }

        protected bool bCommand_Stop = false;
    }
}
