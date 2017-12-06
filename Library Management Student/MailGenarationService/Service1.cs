using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace MailGenarationService
{
    public partial class Service1 : ServiceBase
    {
        //Intilizing middle layer
        ServiceMiddleLayer md = new ServiceMiddleLayer();
        public Service1()
        {
            InitializeComponent();
        }

        public void onDebug()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            this.WriteToFile("Simple Service started {0}");
            this.ScheduleService();
        }

        protected override void OnStop()
        {
            this.WriteToFile("Simple Service stopped {0}");
            this.Schedular.Dispose();
        }
        private Timer Schedular;

        public void ScheduleService()
        {
            try
            {
                SendMail();
            }
            catch (Exception ex)
            {
                WriteToFile("Simple Service Error on: {0} " + ex.Message + ex.StackTrace);

                //Stop the Windows Service.
                using (System.ServiceProcess.ServiceController serviceController = new System.ServiceProcess.ServiceController("SimpleService"))
                {
                    serviceController.Stop();
                }
            }
        }

        private void SchedularCallback(object e)
        {
            this.WriteToFile("Simple Service Log: {0}");
            this.ScheduleService();
        }

        private void WriteToFile(string text)
        {
            string path = "D:\\ServiceLog.txt";
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(string.Format(text, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
                writer.Close();
            }
        }
        public void SendMail()
        {
            DataTable dt = new DataTable();
            dt = md.GetPenaltyData();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string userId = dt.Rows[i]["penalty"].ToString();
                    string name = dt.Rows[i]["StudentName"].ToString();
                    string email = dt.Rows[i]["Email"].ToString();
                    string subject = "Return date exceeded";

                    int penalty = Convert.ToInt32(dt.Rows[i]["penalty"].ToString());
                    if (penalty == 10)
                    {

                        string body = "<p>HI " + name + " </p> <p> Account locked because you have exceded the penaulty 10$ </p> <p> If you want to unlock the account please pay 10$ </p> <p> Thanks, </p> <p> Library management </p>";
                        md.sendMail(subject, body, email, userId, name);
                    }
                    else
                    {
                        subject = "Account Locked";
                        string body = "<p>HI " + name + " </p> <p> Account locked because you have exceded the penaulty 10$ </p> <p> If you want to unlock the account please pay 10$ </p> <p> Thanks, </p> <p> Library management </p>";
                        md.sendMail(subject, body, email, userId, name);
                        int result = 0;
                        result = md.UpdatePenaulty(userId);
                    }
                }

            }

        }
    }
}
