using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WPFTest
{
    class Interaction
    {
        public static LockWindow[] ls = new LockWindow[6];
        private static Timer t;
        public static void StartTimer()
        {
            t = new Timer(Timer_Tick, null, 0, 1000);

        }

        private static void Timer_Tick(object state)
        {
            if(Server.IsLock(Session.Token))
            {
                MainWindow.Lock();
            }
            else
            {

            }
        }
    }
}
