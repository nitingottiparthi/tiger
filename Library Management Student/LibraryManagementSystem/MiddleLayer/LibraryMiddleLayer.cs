using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Services;

namespace LibraryManagementSystem.MiddleLayer
{
    public class LibraryMiddleLayer
    {
        //Connection strings to connect with database
        static string strconWFMS = ConfigurationManager.ConnectionStrings["conLibrary"].ConnectionString;
        static SqlConnection con = new SqlConnection(strconWFMS);

        //Method to add Student
        public int InsertUserDetails(string name, string department, string email, string phone, string address)
        {

            try
            {
                int isActiveResult = 0;
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_User(Name,Department,Email,Phone,Address,Password,Isactive,lastUpdatedDate) VALUES(@Name,@Department,@Email,@Phone,@Address,@Password,@Isactive,@lastUpdatedDate)", con);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Department", department);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@Password", "");
                cmd.Parameters.AddWithValue("@Isactive", true);
                cmd.Parameters.AddWithValue("@lastUpdatedDate", DateTime.Now.ToString());
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


        //Method to check email id already registered or not
        public int CheckEmailId(string emaildId)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Email from tbl_User where Email = '" + emaildId + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return dt.Rows.Count;
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

        //Method to genearate Student Id 
        public string  GenarateId()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 Id from tbl_User order by lastUpdatedDate Desc", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return dt.Rows[0][0].ToString();

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

        //Method to check authentication 
        public int CheckLoginCredentials(string Id, string password)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from tbl_User where Id = '" + Id + "' AND Password = '" + password + "' AND IsActive = 1", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return dt.Rows.Count;

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

        //Method to genearate Student Details 
        public DataTable BindUserDetail()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from tbl_User where isActive = 1 order by lastUpdatedDate Desc", con);
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


        //Method to Bind Books 
        public DataTable BindBookDetail()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from tbl_AddABooks where isActive = 1 order by lastUpdatedDate Desc", con);
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
        public DataTable BindUserDetailWithId(string value)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from tbl_User where isActive = 1 and Id = '" + value + "' order by lastUpdatedDate Desc", con);
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

        //Method to genearate Student Details locked 
        public DataTable BindUserDetailLocked()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from tbl_User where isActive = 0 order by lastUpdatedDate Desc", con);
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
        //Method to add Student
        public int InsertBookDetails(string bookName, string bookPrice, string bookCategory, string bookQty, string bookRack)
        {

            try
            {
                int isActiveResult = 0;
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_AddABooks(BookName,BookCategory,BookPrice,BookQty,BookRack,lastUpdatedDate,IsActive) VALUES(@BookName,@BookCategory,@BookPrice,@BookQty,@BookRack,@lastUpdatedDate,@IsActive)", con);
                cmd.Parameters.AddWithValue("@BookName", bookName);
                cmd.Parameters.AddWithValue("@BookCategory", bookCategory);
                cmd.Parameters.AddWithValue("@BookPrice", bookPrice);
                cmd.Parameters.AddWithValue("@BookQty", bookQty);
                cmd.Parameters.AddWithValue("@BookRack", bookRack);
                cmd.Parameters.AddWithValue("@lastUpdatedDate", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@Isactive", true);

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
        public int UpdateAccount(string value, string val)
        {
            try
            {
                int isActiveResult = 0;
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE  tbl_User SET IsActive = '" + val + "',LastUpdatedDate='"+DateTime.Now.ToString()+"' Where Id = @value ", con);
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

        //Method to genearate Student Details 
        public DataTable GetUserDetailsOnSelectedId(string value)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from tbl_User where isActive = 1 and Id='"+value+"' order by lastUpdatedDate Desc", con);
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

        //Method to genearate Student Details 
        public DataTable BindUserDetailLoginPage()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from tbl_User where isActive = 1 order by Id ASC", con);
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

        //Update Password for User
        public int UpdateAccountByUserPassword(string email, string pwd, string address,string value)
        {
            try
            {
                int isActiveResult = 0;
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE  tbl_User SET Email = '"+email+"',Password='"+pwd+ "',Address = '"+address+"'  Where Id = @value ", con);
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

        public int UpdateUserDeatils(string name,string department,string email, string phone, string address)
        {
            try
            {
                int isActiveResult = 0;
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE  tbl_User SET Email = '" + email + "',Name='" + name + "',Address = '" + address + "',Department = '"+department+"',Phone = '"+phone+"'  Where Email = @value ", con);
                cmd.Parameters.AddWithValue("@value", email);
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

        //Method to add Student
        public int InsertCategory(string name)
        {

            try
            {
                int isActiveResult = 0;
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_Category(Name,lastUpdatedDate,Isactive) VALUES(@Name,@lastUpdatedDate,@Isactive)", con);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Isactive", true);
                cmd.Parameters.AddWithValue("@lastUpdatedDate", DateTime.Now.ToString());
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

       
        public DataTable BindCategoryDetails()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from tbl_Category where isActive = 1 order by lastUpdatedDate Desc", con);
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


        //Method to genearate Student Details 
        public DataTable GetCategorysOnSelectedId(string value)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from tbl_Category where isActive = 1 and Id='" + value + "' order by lastUpdatedDate Desc", con);
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

        public int UpdateCategory(string Id,string name)
        {
            try
            {
                int isActiveResult = 0;
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE  tbl_Category SET Name = '" + name + "' Where Id = @value ", con);
                cmd.Parameters.AddWithValue("@value", Id);
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

        public DataTable BindDepartmentDetails()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from tbl_Department where isActive = 1 order by lastUpdatedDate Desc", con);
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

        //Method to add Student
        public int InsertDepartment(string name)
        {

            try
            {
                int isActiveResult = 0;
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_Department(Name,lastUpdatedDate,Isactive) VALUES(@Name,@lastUpdatedDate,@Isactive)", con);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Isactive", true);
                cmd.Parameters.AddWithValue("@lastUpdatedDate", DateTime.Now.ToString());
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



        //Method to genearate Student Details 
        public DataTable GetDepartmentOnSelectedId(string value)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from tbl_Department where isActive = 1 and Id='" + value + "' order by lastUpdatedDate Desc", con);
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

        public int UpdateDepartment(string Id, string name)
        {
            try
            {
                int isActiveResult = 0;
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE  tbl_Department SET Name = '" + name + "' Where Id = @value ", con);
                cmd.Parameters.AddWithValue("@value", Id);
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
        //Method to books issued
        public int InsertIssueBookDetails(int bookId, string bookName, int StudentId, string StudentName, string IssuedOn, string ReturnDate)
        {

            try
            {
                int isActiveResult = 0;
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_IssueABook(BookId,BookName,StudentId,StudentName,IssuedOn,ReturnDate,IsActive,ReturnDateUpdated) VALUES(@BookId,@BookName,@StudentId,@StudentName,@IssuedOn,@ReturnDate,@IsActive,@ReturnDateUpdated)", con);
                cmd.Parameters.AddWithValue("@BookId", bookId);
                cmd.Parameters.AddWithValue("@BookName", bookName);
                cmd.Parameters.AddWithValue("@StudentId", StudentId);
                cmd.Parameters.AddWithValue("@StudentName", StudentName);
                cmd.Parameters.AddWithValue("@IssuedOn", IssuedOn);
                cmd.Parameters.AddWithValue("@ReturnDate", ReturnDate);
                cmd.Parameters.AddWithValue("@Isactive", true);
                cmd.Parameters.AddWithValue("@ReturnDateUpdated", "");
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

        //Method to Bind Books 
        public DataTable BindBookDetailIssued(string value)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from tbl_IssueABook where  StudentId = '" + value + "'and penalty is not null ", con);
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

        //Method to books Updated
        public int UpdateIssueBookDetails(int id, string returnDate)
        {
            try
            {
                int isActiveResult = 0;
                con.Open();
                SqlCommand cmd = new SqlCommand("Update  tbl_IssueABook Set ReturnDateUpdated = @ReturnDate,IsActive = @IsActive where IssueId = '"+id+"'", con);
               
                cmd.Parameters.AddWithValue("@ReturnDate", returnDate);
                cmd.Parameters.AddWithValue("@Isactive", false);

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


        public DataTable BindReturnBookDetails(string value)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from tbl_IssueABook where isActive = 0 AND StudentId = '" + value + "'", con);
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

        public DataTable BindReturnBookDetailsHome()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from tbl_IssueABook where isActive = 0", con);
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


        //Method to Bind Books 
        public DataTable BindBookDetailIssuedHome()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from tbl_IssueABook where isActive = 1", con);
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

        //Method to add Notificatinos
        public int InsertNotificationDetails(string Title, string Description, string Id, string name)
        {

            try
            {
                int isActiveResult = 0;
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_Notification(Title,Description,StudentId,StudentName,lastUpdatedDate,Isactive) VALUES(@Title,@Description,@StudentId,@StudentName,@lastUpdatedDate,@Isactive)", con);
                cmd.Parameters.AddWithValue("@Title", Title);
                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@StudentId", Id);
                cmd.Parameters.AddWithValue("@StudentName", name);
                cmd.Parameters.AddWithValue("@lastUpdatedDate", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@Isactive", true);
          
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

        //Method to Bind Notifications 
        public DataTable BindNotificationDetails(string value )
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from tbl_Notification where studentId = '"+value+"' AND isActive = 1", con);
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
        //Method to Bind Notifications library management
        public DataTable BindNotificationDetailsLibrary()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from tbl_Notification Where isActive = 1", con);
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

        //Method to update notificatin reply from library manager
        public int UpdateNotification(string value, string val)
        {
            try
            {
                int isActiveResult = 0;
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE  tbl_Notification SET CommentsByLibrary = '" + val + "',LastUpdatedDate='" + DateTime.Now.ToString() + "' Where Id = @value ", con);
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

        public DataTable BindSearchDetail(string value)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from tbl_AddABooks where bookId = '"+value+"' and  isActive = 1 order by lastUpdatedDate Desc", con);
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

        //Show penaulty students list
        public DataTable PenaltyStudents()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from tbl_IssueABook where  penalty>0", con);
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

        //Method to books checkedOut
        public int InsertCheckOutBookDetails(int bookId, string bookName, int StudentId)
        {

            try
            {
                int isActiveResult = 0;
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_CheckOutBooks(BookId,BookName,UserId,LastUpdated,IsActive) VALUES(@BookId,@BookName,@StudentId,@LastUpdatedDate,@IsActive)", con);
                cmd.Parameters.AddWithValue("@BookId", bookId);
                cmd.Parameters.AddWithValue("@BookName", bookName);
                cmd.Parameters.AddWithValue("@StudentId", StudentId);
                cmd.Parameters.AddWithValue("@LastUpdatedDate", DateTime.Now.ToShortDateString());
                cmd.Parameters.AddWithValue("@Isactive", true);
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

        //Show Checkout book list
        public DataTable BindCheckOutbooks(string userId)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from tbl_CheckOutBooks where  UserId = '"+userId+"'", con);
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

        public DataTable UnlockPenaulty(string value)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE tbl_IssueABook Set penalty = 0 where bookId = '" + value + "' ", con);
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
    
