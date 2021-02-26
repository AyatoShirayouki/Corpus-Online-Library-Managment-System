using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineLibrary
{
    public partial class Index : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
               
                if (string.IsNullOrEmpty((string)Session["role"]))
                {
                     LinkButton2.Visible = true; //login
                     LinkButton3.Visible = true; //signup

                     LinkButton4.Visible = false; //logout
                     LinkButton5.Visible = false; //hello user

                     LinkButton6.Visible = true;//admin login

                     LinkButton8.Visible = false; //book inventory
                     LinkButton9.Visible = false; //book issuing
                     LinkButton10.Visible = false; //user managment
                     LinkButton11.Visible = false; //auth management
                     LinkButton12.Visible = false; //publ managment
                }       
                
                else if (Session["role"].Equals("user"))
                {
                    LinkButton2.Visible = false; //login
                    LinkButton3.Visible = false; //signup

                    LinkButton4.Visible = true; //logout
                    LinkButton5.Visible = true; //hello user
                    LinkButton5.Text = "Greetings " + Session["username"].ToString();

                    LinkButton6.Visible = true;//admin login

                    LinkButton8.Visible = false; //book inventory
                    LinkButton9.Visible = false; //book issuing
                    LinkButton10.Visible = false; //user managment
                    LinkButton11.Visible = false; //auth management
                    LinkButton12.Visible = false; //publ managment
                }
                else if (Session["role"].Equals("admin"))
                {
                    LinkButton2.Visible = false; //login
                    LinkButton3.Visible = false; //signup

                    LinkButton4.Visible = true; //logout
                    LinkButton5.Visible = true; //hello user
                    LinkButton5.Text = "Admin: " + Session["username"].ToString();

                    LinkButton6.Visible = false;//admin login

                    LinkButton8.Visible = true; //book inventory
                    LinkButton9.Visible = true; //book issuing
                    LinkButton10.Visible = true; //user managment
                    LinkButton11.Visible = true; //auth management
                    LinkButton12.Visible = true; //publ managment
                }
            }

            catch (Exception exeption)
            {

                throw;
            }
        }

        protected void LinkButton6_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminLogin.aspx");
        }

        protected void LinkButton11_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminAuthorManagment.aspx");
        }

        protected void LinkButton12_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminPublisherManagment.aspx");
        }

        protected void LinkButton8_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminBookInventory.aspx");
        }

        protected void LinkButton9_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminBookIssuing.aspx");
        }

        protected void LinkButton10_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminMemberManagment.aspx");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Books.aspx");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserLogin.aspx");
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Response.Redirect("SignUp.aspx");
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            Session["username"] = "";
            Session["fullName"] = "";
            Session["role"] = "";
            Session["status"] = "";

            LinkButton2.Visible = true; //login
            LinkButton3.Visible = true; //signup

            LinkButton4.Visible = false; //logout
            LinkButton5.Visible = false; //hello user

            LinkButton6.Visible = true;//admin login

            LinkButton8.Visible = false; //book inventory
            LinkButton9.Visible = false; //book issuing
            LinkButton10.Visible = false; //user managment
            LinkButton11.Visible = false; //auth management
            LinkButton12.Visible = false; //publ managment

            Response.Redirect("Home.aspx");
        }
    }
}