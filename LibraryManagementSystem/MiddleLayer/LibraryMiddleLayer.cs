using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

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
                SqlCommand cmd = new SqlCommand("SELECT * from tbl_IssueABook where isActive = 1 AND StudentId = '" + value + "'", con);
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
    }
}
    
