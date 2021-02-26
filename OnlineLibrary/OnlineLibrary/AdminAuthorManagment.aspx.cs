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
    public partial class AdminAuthorManagment : System.Web.UI.Page
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
        //add
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (Exists())
            {
                Response.Write("<script>alert('An Author with this Id already exists')</script>");
            }
            else
            {
                Add();
            }
        }
        //update
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (Exists())
            {
                Update();
            }
            else
            {
                Response.Write("<script>alert('An Author with this Id doesnt exists')</script>");
            }
        }
        //delete
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (Exists())
            {
                Delete();
            }
            else
            {
                Response.Write("<script>alert('An Author with this Id doesnt exists')</script>");
            }
        }
        //searchById
        protected void Button1_Click(object sender, EventArgs e)
        {
            GetById();
        }

        void GetById()
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection(connection);
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();

                }

                SqlCommand cmd = new SqlCommand("SELECT * from Authors where authorId = '" + TextBox1.Text.Trim() + "';", sqlCon);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable authors = new DataTable();
                adapter.Fill(authors);

                if (authors.Rows.Count >= 1)
                {
                    TextBox2.Text = authors.Rows[0][1].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Invalid AuthorId')</script>");
                }

                sqlCon.Close();

            }
            catch (Exception exeption)
            {
                Response.Write("<script>alert('" + exeption.Message + "')</script>");
            }
        }

        void Delete()
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection(connection);
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }

                SqlCommand cmd = new SqlCommand("DELETE from Authors WHERE authorId = '"+ TextBox1.Text.Trim() +"'", sqlCon);

                cmd.ExecuteNonQuery();
                sqlCon.Close();

                Response.Write("<script>alert('Success!')</script>");
                Clear();

                GridView1.DataBind();
            }
            catch (Exception exeption)
            {
                Response.Write("<script>alert('" + exeption.Message + "')</script>");
            }
        }

        void Update()
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection(connection);
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                SqlCommand cmd = new SqlCommand("UPDATE Authors SET authorName = @AuthorName where authorId = '" + TextBox1.Text.Trim() +"'", sqlCon);
                cmd.Parameters.AddWithValue("@AuthorName", TextBox2.Text.Trim());

                cmd.ExecuteNonQuery();
                sqlCon.Close();

                Response.Write("<script>alert('Success!')</script>");
                Clear();

                GridView1.DataBind();
            }
            catch (Exception exeption)
            {
                Response.Write("<script>alert('" + exeption.Message + "')</script>");
            }
        }

        void Add()
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection(connection);
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                SqlCommand cmd = new SqlCommand("INSERT INTO Authors(authorId,authorName) values(@AuthorID,@AuthorName)", sqlCon);
                cmd.Parameters.AddWithValue("@AuthorID", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@AuthorName", TextBox2.Text.Trim());

                cmd.ExecuteNonQuery();
                sqlCon.Close();

                Response.Write("<script>alert('Success!')</script>");
                Clear();

                GridView1.DataBind();
            }
            catch (Exception exeption)
            {
                Response.Write("<script>alert('" + exeption.Message + "')</script>");
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

                SqlCommand cmd = new SqlCommand("SELECT * from Authors where authorId = '" + TextBox1.Text.Trim() + "';", sqlCon);
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

        void Clear()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
        }
    }
}