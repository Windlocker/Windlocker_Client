using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace WPFTest
{
    class Interaction
    {
        public static LockWindow[] ls = new LockWindow[6];
        private static DispatcherTimer t;
        public static void StartTimer()
        {
            t = new DispatcherTimer();
            t.Tick += Timer_Tick;
            t.Interval = TimeSpan.FromSeconds(1);
            t.Start();
        }

        private static void Timer_Tick(object state, EventArgs e)
        {
            if(Server.IsLock(Session.Token))
            {
                MainWindow.Lock();
            }
            else
            {
                MainWindow.UnLock();
            }
        }
    }
}
