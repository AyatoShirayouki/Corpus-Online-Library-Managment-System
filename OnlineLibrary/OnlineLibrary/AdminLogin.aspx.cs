using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineLibrary
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        string connection = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection(connection);
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                SqlCommand cmd = new SqlCommand("select * from Admin where username='" + TextBox1.Text.Trim() + "' AND password='" + TextBox2.Text.Trim() + "'", sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Response.Write("<script>alert('Login Successful!')</script>");
                        Session["username"] = reader.GetValue(0).ToString();
                        Session["fullName"] = reader.GetValue(2).ToString();
                        Session["role"] = "admin";
                    }
                    Response.Redirect("Home.aspx");
                }
                else
                {
                    Response.Write("<script>alert('Invalid Admin')</script>");
                }
            }
            catch (Exception exeption)
            {
                Response.Write("<script>alert('"+exeption.Message+"')</script>");
            }

        }
    }
}