using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WindlockerService
{
    public partial class Service1 : ServiceBase
    {
        private Timer ProcessT = new Timer();
        private Timer UsageT = new Timer();
        private Timer LockT = new Timer();
        private PerformanceCounter cpuCounter;
        private PerformanceCounter ramCounter;

        public Service1()
        {
            InitializeComponent();
            ProcessT.Interval = 5000;

        }

        protected override void OnStart(string[] args)
        {
            /*
             * 1. Lock상태였는지 Registry에서 가져온다
             * 2. Network에 연결되어있다면, 등록된 계정에서 info를 수신한다.
             * 3. 지속적으로 Process info와 Resource info를 보낸다.
             * 4. system 실행시 자동 실행된다.
             */
        }

        protected override void OnStop()
        {
        }
    }
}
