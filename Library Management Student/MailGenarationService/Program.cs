using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MailGenarationService
{
     class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// //Intilizing middle layer
        ServiceMiddleLayer md = new ServiceMiddleLayer();
        static void Main()
        {
            Program p = new Program();
            p.SendMail();

        }

        public  void SendMail()
        {
            DataTable dt = new DataTable();
            dt = md.GetPenaltyData();
            if (dt.Rows.Count > 0)
            {
                int[] arr = new int[dt.Rows.Count];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string userId = dt.Rows[i]["StudentId"].ToString();
                    string name = dt.Rows[i]["StudentName"].ToString();
                    string email = dt.Rows[i]["Email"].ToString();

                    string subject = "Return date exceeded : "+userId+"- "+name+"";
                    DataTable dt1 = new DataTable();

                    dt1 = md.GetPenaultyBooks(userId);
                    StringBuilder sb1 = new StringBuilder();
                    sb1.Append("<table border='1'> <tr> <th>Book Id </th> <th>Book Name </th> <th>Issue On </th> <th>Return Date </th> <th>Penalty</th>");
                    for (int j=0;j<dt1.Rows.Count;j++)
                    {
                        sb1.Append("<tr> <td> " + dt1.Rows[j]["bookID"] + "" + "</td> <td> " + dt1.Rows[j]["bookName"] + "</td> <td>  " + dt1.Rows[j]["issuedOn"] + "</td><td>" + dt1.Rows[j]["ReturnDate"] + " </td><td>  " + dt1.Rows[j]["penalty"] + "</td></tr>");
                       
                    }
                    sb1.Append("</table>");

                    int penalty = Convert.ToInt32(dt.Rows[i]["penalty"].ToString());
                    if (penalty >= 10)
                    {
                        subject = "Account Locked: " + userId + "- " + name + "";
                        string body = "<p>HI " + name + " </p> <p> Account locked because you have exceded the penaulty $"+penalty+" </p> <p> If you want to unlock the account please pay the penaulty </p> <p> Thanks, </p> <p> Library management </p>";
                        md.sendMail(subject, body, email, userId, name);
                        int res = 0;
                        res = md.UpdateAccount(userId, 0);
                    }
                    else if(penalty>=1 && penalty<10)
                    {
                        
                        int result = 0;
                        result = md.UpdatePenaulty(userId);
                        dt = md.GetPenaltyData();
                        string body = "<p>HI " + name + " </p> <p> Please return the book on time as it exceeded the return date </p>"+sb1+" <p> Thanks, </p> <p> Library management </p>";
                        md.sendMail(subject, body, email, userId, name);
                        
                    }
                }

            }

        }
    }
    
}
