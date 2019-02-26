using System.Drawing;
using System.ServiceProcess;
using System.Timers;
using System.Windows.Forms;


//C:\Windows\Microsoft.NET\Framework\v4.0.30319>InstallUtil.exe "C:\Users\Harshal\Desktop\Demo Service\Demo Service\bin\Debug\Demo Service.exe"

namespace Demo_Service
{
    public partial class Service1 : ServiceBase
    {
        System.Timers.Timer timer;
        ProcessInformation pobj;

        public Service1()
        {

            timer = new System.Timers.Timer();
            pobj = new ProcessInformation();
            InitializeComponent();
           
        }

        protected override void OnStart(string[] args)
        {
            timer.Interval = 10000; // 3600000; //  1000 * 60 * 60;
            timer.Enabled = true;

            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);            
        }

        private void OnElapsedTime(object sender, ElapsedEventArgs e)
        {
            string str = null;

            str = pobj.ProcessInfo();
            pobj.CreateLogFile(str);
            //pobj.ShowNotification("Process information has been logged ");
            //ShowNotification("Process information has been logged ");

        }


        protected override void OnStop()
        {
            timer.Enabled = false;

            if (timer != null)
            {
                timer = null;
            }

            if (pobj != null)
            {
                pobj = null;
            }

        }
    }
}
