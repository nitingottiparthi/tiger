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
    public partial class LibraryManager : System.Web.UI.Page
    {
        //Intilizing middle layer
        LibraryMiddleLayer md = new LibraryMiddleLayer();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();

                //Bind Book Details
                dt = md.BindBookDetail();
                gvBooks.DataSource = dt;
                gvBooks.DataBind();
                idBookText.InnerHtml = dt.Rows.Count.ToString();

                //Bind Books issued
                dt = md.BindBookDetailIssuedHome();
                gvBooksIssued.DataSource = dt;
                gvBooksIssued.DataBind();
                idIssuedBooks.InnerHtml = dt.Rows.Count.ToString();


                //Bind Return Book Details
                dt = md.BindReturnBookDetailsHome();
                gvReturnBooks.DataSource = dt;
                gvReturnBooks.DataBind();
                idReturnBooksText.InnerText = dt.Rows.Count.ToString();

                //Bind Student details
                dt = md.BindUserDetail();
                gvUser.DataSource = dt;
                gvUser.DataBind();
                idStudentsText.InnerHtml = dt.Rows.Count.ToString();


            }


        }
    }
}