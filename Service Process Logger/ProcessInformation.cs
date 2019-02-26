using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;

namespace Demo_Service
{
    public class ProcessInformation 
    {
       // public NotifyIcon notifyIcon;

        public ProcessInformation()
        {
         //   notifyIcon = new NotifyIcon();
            
        }

        
        public String ProcessInfo()
        {
            String ProcessInfo = null;

            Process[] processlist = Process.GetProcesses();

            foreach (Process p in processlist)
            {
                ProcessInfo += "Process: " + p.ProcessName + " ID: " + p.Id + " Threads: " + p.Threads; 
                ProcessInfo += "\n\n";
            }

            return ProcessInfo;
        }

        public void CreateLogFile(string str)
        {
            string LogPath = AppDomain.CurrentDomain.BaseDirectory + @"\Logs";
            string year = LogPath + @"\" + DateTime.Now.ToString("yyyy");
            string month = year + @"\" + DateTime.Now.ToString("MMMM");
            string date = month + @"\ProcessLog_" + DateTime.Now.ToShortDateString().Replace('-', '_') + ".txt";
            
            
            string filePath = date;

            if (!Directory.Exists(LogPath))
            {
                Directory.CreateDirectory(LogPath);
            }

            if (!Directory.Exists(year))
            {
                string path1 = LogPath + @"\" + DateTime.Now.AddYears(-1).ToString("yyyy");
                string path2 = path1 + ".zip";

                if (Directory.Exists(path1))
                {
                    ZipFile.CreateFromDirectory(path1, path2,CompressionLevel.Optimal,true);
                    Directory.Delete(path1, recursive: true);
                }

                Directory.CreateDirectory(year);
            }

            if (!Directory.Exists(month))
            {
                Directory.CreateDirectory(month);
            }

            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine(str);
                   
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine(str);
                   
                }
            }


        }

        /*
        public void ShowNotification(string NotificationMessage)
        {
            notifyIcon.Visible = true;
            notifyIcon.Icon = SystemIcons.Information;
            notifyIcon.ShowBalloonTip(100, "Imortant Notice", NotificationMessage, ToolTipIcon.Info);
           // notifyIcon.ShowBalloonTip(5000);
            notifyIcon.Dispose();
        }*/

    }
}
