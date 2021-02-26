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
    public partial class AdminPublisherManagment : System.Web.UI.Page
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
        //get by Id
        protected void Button1_Click(object sender, EventArgs e)
        {
            GetById();
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

        void GetById()
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection(connection);
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();

                }

                SqlCommand cmd = new SqlCommand("SELECT * from Publishers where publisherId = '" + TextBox1.Text.Trim() + "';", sqlCon);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable publishers = new DataTable();
                adapter.Fill(publishers);

                if (publishers.Rows.Count >= 1)
                {
                    TextBox2.Text = publishers.Rows[0][1].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Invalid PublisherId')</script>");
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

                SqlCommand cmd = new SqlCommand("DELETE from Publishers WHERE publisherId = '" + TextBox1.Text.Trim() + "'", sqlCon);

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
                SqlCommand cmd = new SqlCommand("UPDATE Publishers SET publisherName = @PublisherName where publisherId = '" + TextBox1.Text.Trim() + "'", sqlCon);
                cmd.Parameters.AddWithValue("@PublisherName", TextBox2.Text.Trim());

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
                SqlCommand cmd = new SqlCommand("INSERT INTO Publishers(publisherId,publisherName) values(@PublisherId,@PublisherName)", sqlCon);
                cmd.Parameters.AddWithValue("@PublisherId", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@PublisherName", TextBox2.Text.Trim());

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

                SqlCommand cmd = new SqlCommand("SELECT * from Publishers where publisherId = '" + TextBox1.Text.Trim() + "';", sqlCon);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable publishers = new DataTable();
                adapter.Fill(publishers);

                if (publishers.Rows.Count >= 1)
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