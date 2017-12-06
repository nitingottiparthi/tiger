using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
public class Class1
    {
        //Connection strings to connect with database
        static string strconWFMS = ConfigurationManager.ConnectionStrings["conLibrary"].ConnectionString;
        static SqlConnection con = new SqlConnection(strconWFMS);

        
        public static void Main(string[] args)
        {
            Class1 md = new Class1();
            md.SendMail();
        }

        public void SendMail()
        {
            DataTable dt = new DataTable();
            dt = GetPenaltyData();
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
                       sendMail(subject, body, email, userId, name);
                    }
                    else
                    {
                        subject = "Account Locked";
                        string body = "<p>HI " + name + " </p> <p> Account locked because you have exceded the penaulty 10$ </p> <p> If you want to unlock the account please pay 10$ </p> <p> Thanks, </p> <p> Library management </p>";
                       sendMail(subject, body, email, userId, name);
                        int result = 0;
                        result = UpdatePenaulty(userId);
                    }
                }

            }

        }
        //Method to update Qty
        public int UpdateQty(int qty, int Id)
        {
            try
            {
                int isActiveResult = 0;
                con.Open();
                SqlCommand cmd = new SqlCommand("Update tbl_AddABooks set BookQty = (select BookQty from tbl_AddABooks where  BookId = @id and BookQty>0) - 1 where   BookId = @id", con);
                cmd.Parameters.AddWithValue("@qty", qty);
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

        //Method to send mail
        public void sendMail(string subject, string body, string emailTo, string userId, string name)
        {
            MailMessage mail = new MailMessage();
            //Local edit mail.From = new MailAddress("RCIndiaAdmin@rockwellcollins.com");
            mail.From = new MailAddress("ambareesh.marimekala@rockwellcollins.com");
            mail.To.Add(emailTo);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtpimr.rockwellcollins.com");
            client.Credentials = CredentialCache.DefaultNetworkCredentials;
            client.Send(mail);
        }


        public DataTable GetPenaltyData()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select  distinct StudentId,StudentName,email,penalty from tbl_IssueABook t1 inner join tbl_User t2 on t1.StudentId = t2.Id where ReturnDate < getdate()", con);
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
        public int UpdatePenaulty(string Id)
        {
            try
            {
                int isActiveResult = 0;
                con.Open();
                SqlCommand cmd = new SqlCommand("Update tbl_IssueABook set penalty = (select distinct penalty  from tbl_IssueABook where  StudentId = @Id and ReturnDate < getdate()) + 1 where StudentId = @Id", con);
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

    }






