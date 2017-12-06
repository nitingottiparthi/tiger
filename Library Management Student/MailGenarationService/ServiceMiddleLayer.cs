using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MailGenarationService
{
    class ServiceMiddleLayer
    {
        //Connection strings to connect with database
        static string strconWFMS = ConfigurationManager.ConnectionStrings["conLibrary"].ConnectionString;
        static SqlConnection con = new SqlConnection(strconWFMS);

        //Method to send mail
        public void sendMail(string subject, string body, string emailTo, string userId, string name)
        {
            MailMessage mm = new MailMessage("sender@gmail.com", emailTo);
            mm.Subject = subject;
            mm.Body = body;
            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;

            NetworkCredential NetworkCred = new NetworkCredential();
            NetworkCred.UserName = "libmagsys2017@gmail.com";
            NetworkCred.Password = "testing@123";
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.Send(mm);
        }


        public DataTable GetPenaltyData()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select  distinct StudentId,StudentName,email,ceiling(convert(float, abs(datediff(day, issuedOn, ReturnDate))) / 7) as 'penalty' from tbl_IssueABook t1 inner join tbl_User t2 on t1.StudentId = t2.Id where ReturnDate < getdate()", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return dt;

            }
            catch (Exception ex)
            {
                throw ex; //TODO: Please log it or remove the catch
            }
            finally
            {
                con.Close();
            }
        }


        //Method to update Qty
        //Method to update Qty
        public int UpdatePenaulty(string Id)
        {
            try
            {
                int isActiveResult = 0;
                con.Open();
                SqlCommand cmd = new SqlCommand("Update tbl_IssueABook set penalty = (select top 1  distinct ceiling(convert(float, abs(datediff(day, issuedOn, ReturnDate))) / 7) from tbl_IssueABook where StudentId = @Id) where StudentId = @Id", con);
                cmd.Parameters.AddWithValue("@Id", Id);
                isActiveResult = cmd.ExecuteNonQuery();
                con.Close();
                return isActiveResult;

            }
            catch (Exception ex)
            {
                throw ex; //TODO: Please log it or remove the catch
            }
            finally
            {
                con.Close();
            }

        }


        //Method to lock the account 
        public int UpdateAccount(string value, int val)
        {
            try
            {
                int isActiveResult = 0;
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE  tbl_User SET IsActive = 0 ,LastUpdatedDate='" + DateTime.Now.ToString() + "' Where Id = @value ", con);
                cmd.Parameters.AddWithValue("@value", value);
                isActiveResult = cmd.ExecuteNonQuery();
                con.Close();
                return isActiveResult;

            }
            catch (Exception ex)
            {
                throw ex; //TODO: Please log it or remove the catch
            }
            finally
            {
                con.Close();
            }
            
        }
        public DataTable GetPenaultyBooks(string userID)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from tbl_IssueABook where studentId = '"+userID+"' and penalty >=1", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return dt;

            }
            catch (Exception ex)
            {
                throw ex; //TODO: Please log it or remove the catch
            }
            finally
            {
                con.Close();
            }
        }

    }
}
