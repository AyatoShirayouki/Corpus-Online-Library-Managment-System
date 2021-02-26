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
    public partial class AdminMemberManagment : System.Web.UI.Page
    {
        string connection = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["role"] == "admin")
            {
                GridView1.DataBind();
            }
            else
            {
                Response.Redirect("Home.aspx");
            }
        }
        //delete user
        protected void Button2_Click(object sender, EventArgs e)
        {
            Delete();
        }
        //searchbyId
        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            GetById();
        }
        //user active
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            UpdateStatus("active");
        }
        //user pending
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            UpdateStatus("pending");
        }
        //block user
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            UpdateStatus("blocked");
        }

        void Delete()
        {
            if (Exists())
            {
                try
                {
                    SqlConnection sqlCon = new SqlConnection(connection);
                    if (sqlCon.State == ConnectionState.Closed)
                    {
                        sqlCon.Open();
                    }

                    SqlCommand cmd = new SqlCommand("DELETE from Users WHERE username = '" + TextBox1.Text.Trim() + "'", sqlCon);

                    cmd.ExecuteNonQuery();
                    sqlCon.Close();

                    //Response.Write("<script>alert('Success!')</script>");
                    Clear();

                    GridView1.DataBind();
                }
                catch (Exception exeption)
                {
                    Response.Write("<script>alert('" + exeption.Message + "')</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Enter username')</script>");
            }
        }

        bool Exists()
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection(connection);
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();

                }

                SqlCommand cmd = new SqlCommand("SELECT * from Users where username = '" + TextBox1.Text.Trim() + "';", sqlCon);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable users = new DataTable();
                adapter.Fill(users);

                if (users.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

                sqlCon.Close();

            }
            catch (Exception exeption)
            {
                Response.Write("<script>alert('" + exeption.Message + "')</script>");
                return false;
            }
        }

        public void GetById()
        {
            if (Exists())
            {
                try
                {
                    SqlConnection sqlCon = new SqlConnection(connection);
                    if (sqlCon.State == ConnectionState.Closed)
                    {
                        sqlCon.Open();
                    }
                    SqlCommand cmd = new SqlCommand("select * from Users where username='" + TextBox1.Text.Trim() + "'", sqlCon);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            TextBox2.Text = reader.GetValue(0).ToString();
                            TextBox7.Text = reader.GetValue(10).ToString();
                            TextBox8.Text = reader.GetValue(1).ToString();
                            TextBox3.Text = reader.GetValue(2).ToString();
                            TextBox4.Text = reader.GetValue(3).ToString();
                            TextBox9.Text = reader.GetValue(4).ToString();
                            TextBox10.Text = reader.GetValue(5).ToString();
                            TextBox11.Text = reader.GetValue(6).ToString();
                            TextBox6.Text = reader.GetValue(7).ToString();
                        }

                    }
                    else
                    {
                        Response.Write("<script>alert('Something went wrong')</script>");
                    }
                }
                catch (Exception exeption)
                {
                    Response.Write("<script>alert('" + exeption.Message + "')</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Enter username')</script>");
            }
        }

        void UpdateStatus(string status)
        {
            if (Exists())
            {
                try
                {
                    SqlConnection sqlCon = new SqlConnection(connection);
                    if (sqlCon.State == ConnectionState.Closed)
                    {
                        sqlCon.Open();
                    }
                    SqlCommand cmd = new SqlCommand("UPDATE Users SET accountStatus='" + status + "' where username='" + TextBox1.Text.Trim() + "'", sqlCon);
                    cmd.ExecuteNonQuery();
                    sqlCon.Close();
                    GridView1.DataBind();
                    TextBox7.Text = status;
                    Clear();
                    //Response.Write("<script>alert('status updated')</script>");
                }
                catch (Exception exeption)
                {
                    Response.Write("<script>alert('" + exeption.Message + "')</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Invalid Username')</script>");
            }
        }
        void Clear()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox6.Text = "";
            TextBox7.Text = "";
            TextBox8.Text = "";
            TextBox9.Text = "";
            TextBox10.Text = "";
            TextBox11.Text = "";
        }
    }
}