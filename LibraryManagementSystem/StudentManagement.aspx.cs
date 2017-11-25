using LibraryManagementSystem.MiddleLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LibraryManagementSystem
{
    public partial class StudentManagement : System.Web.UI.Page
    {
        static string Id = string.Empty;
        //Intilizing middle layer
        LibraryMiddleLayer md = new LibraryMiddleLayer();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Id = Request.QueryString["Id"].ToString().Trim();
                DataTable dt = new DataTable();
                dt = md.BindUserDetailWithId(Id);
                idName.InnerHtml = dt.Rows[0]["Name"].ToString();

                //Bind Issued Books 
                dt = md.BindBookDetailIssued(Id);
                gvBooksIssued.DataSource = dt;
                gvBooksIssued.DataBind();
                idIssuedBooks.InnerHtml = dt.Rows.Count.ToString();
                //Bind Return Books
                dt = md.BindReturnBookDetails(Id);
                gvReturnBooks.DataSource = dt;
                gvReturnBooks.DataBind();
                idReturnBooks.InnerHtml = dt.Rows.Count.ToString();

                //Bind book Details
                dt = md.BindBookDetail();
                gvBooks.DataSource = dt;
                gvBooks.DataBind();
                idBooks.InnerHtml = dt.Rows.Count.ToString();

                //Bind notifications 
                dt = md.BindNotificationDetails(Id);
                gvNotifications.DataSource = dt;
                gvNotifications.DataBind();
                idNotifications.InnerHtml = dt.Rows.Count.ToString();

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string studentId = Id;
            string name = idName.InnerText;
            string title = txtTitle.Text;
            string Description = txtDescription.Text;
            if (title != "" && Description != "")
            {
                int result = md.InsertNotificationDetails(title, Description, studentId, name);
                if (result == 1)
                {
                    divMsg.Visible = true;
                    divMsg.InnerHtml = "Notification send";
                    txtDescription.Text = "";
                    txtTitle.Text = "";

                    //Bind notifications 
                    DataTable dt = new DataTable();
                    dt = md.BindNotificationDetails(studentId);
                    gvNotifications.DataSource = dt;
                    gvNotifications.DataBind();
                    idNotifications.InnerHtml = dt.Rows.Count.ToString();
                }
            }
            else
            {
                divMsg.Visible = true;
                divMsg.Style.Add("Background-color", "tomato");
                divMsg.InnerHtml = "Please fill required fileds";
            }
        }
    }
}