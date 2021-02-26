using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineLibrary
{
    public partial class SignUp : System.Web.UI.Page
    {
        string connection = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        // sign up button
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Exists())
            {
                Response.Write("<script>alert('User Already Exists')</script>");
            }
            else
            {
                SignUpUser();
            }
        }

        bool Exists()
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection(connection);
                if (sqlCon.State == ConnectionState.Closed)
                {
                    Response.Write("<script>alert(Open)</script>");
                    sqlCon.Open();

                }

                SqlCommand cmd = new SqlCommand("SELECT * from Users where username = '"+ TextBox8.Text.Trim() +"';", sqlCon);
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

                //Response.Write("<script>alert('SignUp Successful!')</script>");
            }
            catch (Exception exeption)
            {
                Response.Write("<script>alert('" + exeption.Message + "')</script>");
                return false;
            }

            
        }

        public void SignUpUser()
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection(connection);
                if (sqlCon.State == ConnectionState.Closed)
                {
                    Response.Write("<script>alert(Open)</script>");
                    sqlCon.Open();

                }
                SqlCommand cmd = new SqlCommand("INSERT INTO Users(fullName,dob,phoneNumber,email,state,city,pincode,address,username,password,accountStatus) values(@Full_Name," +
                    "@Birth_Date,@Phone_Number,@Email,@State,@City,@Pin_Code,@Address,@Username,@Password,@account_status)"
                    , sqlCon);
                cmd.Parameters.AddWithValue("@Full_Name", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@Birth_Date", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@Phone_Number", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@State", DropDownList1.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@City", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@Pin_Code", TextBox7.Text.Trim());
                cmd.Parameters.AddWithValue("@Address", TextBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@Username", TextBox8.Text.Trim());
                cmd.Parameters.AddWithValue("@Password", TextBox9.Text.Trim());
                cmd.Parameters.AddWithValue("@account_status", "pending");

                cmd.ExecuteNonQuery();
                sqlCon.Close();

                Response.Write("<script>alert('SignUp Successful!')</script>");
            }
            catch (Exception exeption)
            {
                Response.Write("<script>alert('" + exeption.Message + "')</script>");
            }
        } 
    }
}