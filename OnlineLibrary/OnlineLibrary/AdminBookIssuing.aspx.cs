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
    public partial class AdminBookIssuing : System.Web.UI.Page
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
        //go
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (BookExists() && UserExists())
            {
                GetById();
            }
            else
            {
                Response.Write("<script>alert('Invalid Username or BookID')</script>");
            }
        }
        //issue
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (BookExists() && UserExists())
            {
                if (checkIfIssueEntryExist())
                {
                    Response.Write("<script>alert('Issue already Exists')</script>");
                }
                else
                {
                    issueBook();
                }
            }
            else
            {
                Response.Write("<script>alert('Invalid Username or BookID')</script>");
            }
        }
        //return
        protected void Button4_Click(object sender, EventArgs e)
        {

            if (BookExists() && UserExists())
            {
                if (checkIfIssueEntryExist())
                {
                    returnBook();
                }
                else
                {
                    Response.Write("<script>alert('Entry does not exist')</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Invalid Username or BookID')</script>");
            }
        }

        bool checkIfIssueEntryExist()
        {
            try
            {
                SqlConnection con = new SqlConnection(connection);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("select * from BookIssue WHERE username='" + TextBox2.Text.Trim() + "' AND bookId='" + TextBox1.Text.Trim() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        bool BookExists()
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection(connection);
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();

                }

                SqlCommand cmd = new SqlCommand("SELECT * from Books where bookId = '" + TextBox1.Text.Trim() + "' AND currentStock > 0", sqlCon);
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
        bool UserExists()
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection(connection);
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();

                }

                SqlCommand cmd = new SqlCommand("SELECT * from Users where username = '" + TextBox2.Text.Trim() + "';", sqlCon);
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

        void GetById()
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection(connection);
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();

                }

                SqlCommand cmd = new SqlCommand("SELECT * from Books where bookId = '" + TextBox1.Text.Trim() + "';", sqlCon);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);

                if (table.Rows.Count >= 1)
                {
                    TextBox4.Text = table.Rows[0]["bookName"].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Invalid BookId')</script>");
                }

                cmd = new SqlCommand("SELECT * from Users where username = '" + TextBox2.Text.Trim() + "';", sqlCon);
                adapter = new SqlDataAdapter(cmd);
                table = new DataTable();
                adapter.Fill(table);

                if (table.Rows.Count >= 1)
                {
                    TextBox3.Text = table.Rows[0]["fullName"].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Invalid username')</script>");
                }

                sqlCon.Close();

            }
            catch (Exception exeption)
            {
                Response.Write("<script>alert('" + exeption.Message + "')</script>");
            }
        }

        void returnBook()
        {
            try
            {
                SqlConnection con = new SqlConnection(connection);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


                SqlCommand cmd = new SqlCommand("Delete from BookIssue WHERE bookId='" + TextBox1.Text.Trim() + "' AND username='" + TextBox2.Text.Trim() + "'", con);
                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {

                    cmd = new SqlCommand("update Books set currentStock = currentStock+1 WHERE bookId='" + TextBox1.Text.Trim() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Response.Write("<script>alert('Book Returned Successfully');</script>");
                    GridView1.DataBind();

                    con.Close();
                    Clear();
                }
                else
                {
                    Response.Write("<script>alert('Error - Invalid details');</script>");
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void issueBook()
        {
            try
            {
                SqlConnection con = new SqlConnection(connection);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO BookIssue(username,userFullName,bookId,bookName,issueDate,dueDate)" +
                    " values(@member_id,@member_name,@book_id,@book_name,@issue_date,@due_date)", con);

                cmd.Parameters.AddWithValue("@member_id", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@member_name", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@book_name", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@issue_date", TextBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@due_date", TextBox6.Text.Trim());

                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("update Books set currentStock = currentStock-1 WHERE bookId='" + TextBox1.Text.Trim() + "'", con);

                cmd.ExecuteNonQuery();

                con.Close();
                Response.Write("<script>alert('Book Issued Successfully');</script>");

                GridView1.DataBind();
                Clear();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
        void Clear()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //Check your condition here
                    DateTime dt = Convert.ToDateTime(e.Row.Cells[5].Text);
                    DateTime today = DateTime.Today;
                    if (today > dt)
                    {
                        e.Row.BackColor = System.Drawing.Color.PaleVioletRed;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}