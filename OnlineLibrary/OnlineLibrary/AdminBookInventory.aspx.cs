using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineLibrary
{
    public partial class AdminBookInventory : System.Web.UI.Page
    {
        string connection = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        static string globalFilepath;
        static int globalActualStock, globalCurrentStock, globalIssuedBooks;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["role"] == "admin")
            {
                if (!IsPostBack)
                {
                    addAuthorAndPublisher();
                }
                GridView1.DataBind();
            }
            else
            {
                Response.Redirect("Home.aspx");
            }
        }
        //gobutton
        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            GetById();
        }
        //add
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Exists())
            {
                Response.Write("<script>alert('Book already exists!')</script>");
            }
            else
            {
                AddBook();
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
                Response.Write("<script>alert('Book does not exists!')</script>");
            }
        }
        //delete
        protected void Button2_Click(object sender, EventArgs e)
        {
            Delete();
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

                    SqlCommand cmd = new SqlCommand("DELETE from Books WHERE bookId = '" + TextBox1.Text.Trim() + "'", sqlCon);

                    cmd.ExecuteNonQuery();
                    sqlCon.Close();

                    //Response.Write("<script>alert('Success!')</script>");

                    GridView1.DataBind();
                }
                catch (Exception exeption)
                {
                    Response.Write("<script>alert('" + exeption.Message + "')</script>");
                }
            }
        }

        void Update()
        {
            if (Exists())
            {
                try
                {
                    int actual_stock = Convert.ToInt32(TextBox4.Text.Trim());
                    int current_stock = Convert.ToInt32(TextBox5.Text.Trim());

                    if (globalActualStock == actual_stock)
                    {

                    }
                    else
                    {
                        if (actual_stock < globalIssuedBooks)
                        {
                            Response.Write("<script>alert('Actual Stock value cannot be less than the Issued books');</script>");
                            return;


                        }
                        else
                        {
                            current_stock = actual_stock - globalIssuedBooks;
                            TextBox5.Text = "" + current_stock;
                        }
                    }

                    string genres = "";

                    foreach (int i in ListBox1.GetSelectedIndices())
                    {
                        genres = genres + ListBox1.Items[i] + ",";
                    }

                    genres = genres.Remove(genres.Length - 1);

                    string filepath = "~/book_inventory/books.png";
                    string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    if (filename == "" || filename == null)
                    {
                        filepath = globalFilepath;
                    }
                    else
                    {
                        FileUpload1.SaveAs(Server.MapPath("book_inventory/" + filename));
                        filepath = "~/book_inventory/" + filename;
                    }

                    SqlConnection sqlCon = new SqlConnection(connection);
                    if (sqlCon.State == ConnectionState.Closed)
                    {
                        sqlCon.Open();
                    }
                    SqlCommand cmd = new SqlCommand("UPDATE Books SET bookName=@bookName, genre=@genre, authorName=@authorName, publisherName=@publisherName," +
                        " publishDate=@publishDate, language=@language, edition=@edition, bookCost=@bookCost, " +
                        "pages=@pages, bookDescription=@bookDescription, actualStock=@actualStock, currentStock=@currentStock, " +
                        "bookImgLink=@bookImgLink where bookId='" + TextBox1.Text.Trim() + "'", sqlCon);

                    cmd.Parameters.AddWithValue("@bookName", TextBox2.Text.Trim());
                    cmd.Parameters.AddWithValue("@genre", genres);
                    cmd.Parameters.AddWithValue("@authorName", DropDownList3.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@publisherName", DropDownList2.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@publishDate", TextBox3.Text.Trim());
                    cmd.Parameters.AddWithValue("@language", DropDownList1.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@edition", TextBox9.Text.Trim());
                    cmd.Parameters.AddWithValue("@bookCost", TextBox10.Text.Trim());
                    cmd.Parameters.AddWithValue("@pages", TextBox11.Text.Trim());
                    cmd.Parameters.AddWithValue("@bookDescription", TextBox6.Text.Trim());
                    cmd.Parameters.AddWithValue("@actualStock", TextBox4.Text.Trim());
                    cmd.Parameters.AddWithValue("@currentStock", TextBox4.Text.Trim());
                    cmd.Parameters.AddWithValue("@bookImgLink", filepath);


                    cmd.ExecuteNonQuery();
                    sqlCon.Close();
                    GridView1.DataBind();
                    
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
                    SqlCommand cmd = new SqlCommand("select * from Books where bookId='" + TextBox1.Text.Trim() + "'", sqlCon);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable booksTable = new DataTable();
                    adapter.Fill(booksTable);

                    if (booksTable.Rows.Count >= 1)
                    {
                        
                        TextBox2.Text = booksTable.Rows[0]["bookName"].ToString().Trim(); 
                        TextBox3.Text = booksTable.Rows[0]["publishDate"].ToString().Trim(); 
                        TextBox9.Text = booksTable.Rows[0]["edition"].ToString().Trim(); 
                        TextBox10.Text = booksTable.Rows[0]["bookCost"].ToString().Trim(); 
                        TextBox11.Text = booksTable.Rows[0]["pages"].ToString().Trim(); 
                        TextBox4.Text = booksTable.Rows[0]["actualStock"].ToString().Trim(); 
                        TextBox5.Text = booksTable.Rows[0]["currentStock"].ToString().Trim();
                        TextBox6.Text = booksTable.Rows[0]["bookDescription"].ToString().Trim();
                        DropDownList1.SelectedValue = booksTable.Rows[0]["language"].ToString().Trim();
                        DropDownList2.SelectedValue = booksTable.Rows[0]["publisherName"].ToString().Trim();
                        DropDownList3.SelectedValue = booksTable.Rows[0]["authorName"].ToString().Trim();
                        TextBox7.Text = (int.Parse(booksTable.Rows[0]["actualStock"].ToString()) - int.Parse(booksTable.Rows[0]["currentStock"].ToString())).ToString();
                        



                        ListBox1.ClearSelection();
                        string[] genre = booksTable.Rows[0]["genre"].ToString().Trim().Split(',');

                        for (int i = 0; i < genre.Length; i++)
                        {
                            for (int j = 0; j < ListBox1.Items.Count; j++)
                            {
                                if (ListBox1.Items[j].ToString() == genre[i])
                                {
                                    ListBox1.Items[j].Selected = true;
                                }
                            }
                        }

                        globalActualStock = int.Parse(booksTable.Rows[0]["actualStock"].ToString().Trim());
                        globalCurrentStock = int.Parse(booksTable.Rows[0]["currentStock"].ToString().Trim());
                        globalIssuedBooks = globalActualStock - globalCurrentStock;
                        globalFilepath = booksTable.Rows[0]["bookImgLink"].ToString();
                    }
                    else
                    {
                        Response.Write("<script>alert('Invalid Book ID!')</script>");
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

        void addAuthorAndPublisher()
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection(connection);
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT authorName from Authors", sqlCon);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable authorsTable = new DataTable();
                adapter.Fill(authorsTable);

                DropDownList3.DataSource = authorsTable;
                DropDownList3.DataValueField = "authorName";
                DropDownList3.DataBind();


                SqlCommand cmd2 = new SqlCommand("SELECT publisherName from Publishers", sqlCon);
                SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
                DataTable publishersTable = new DataTable();
                adapter2.Fill(publishersTable);

                DropDownList2.DataSource = publishersTable;
                DropDownList2.DataValueField = "publisherName";
                DropDownList2.DataBind();


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

                SqlCommand cmd = new SqlCommand("SELECT * from Books where bookId = '" + TextBox1.Text.Trim() + "' OR bookName = '"+ TextBox2.Text.Trim() + "';", sqlCon);
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
        void AddBook()
        {
            try
            {
                string genres = "";

                foreach (int i in ListBox1.GetSelectedIndices())
                {
                    genres = genres + ListBox1.Items[i] + ",";
                }

                genres = genres.Remove(genres.Length - 1);

                string filepath = "~/book_inventory/books.png";
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.SaveAs(Server.MapPath("book_inventory/" + filename));
                filepath = "~/book_inventory/" + filename;

                SqlConnection sqlCon = new SqlConnection(connection);
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO Books(bookId,bookName,genre,authorName,publisherName," +
                    "publishDate,language,edition,bookCost,pages,bookDescription,actualStock,currentStock,bookImgLink)" +
                    " values(@bookId,@bookName,@genre,@authorName,@publisherName,@publishDate,@language," +
                    "@edition,@bookCost,@pages,@bookDescription,@actualStock,@currentStock,@bookImgLink)", sqlCon);


                cmd.Parameters.AddWithValue("@bookId", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@bookName", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@genre", genres);
                cmd.Parameters.AddWithValue("@authorName", DropDownList3.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@publisherName", DropDownList2.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@publishDate", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@language", DropDownList1.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@edition", TextBox9.Text.Trim());
                cmd.Parameters.AddWithValue("@bookCost", TextBox10.Text.Trim());
                cmd.Parameters.AddWithValue("@pages", TextBox11.Text.Trim());
                cmd.Parameters.AddWithValue("@bookDescription", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@actualStock", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@currentStock", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@bookImgLink", filepath);

                cmd.ExecuteNonQuery();
                sqlCon.Close();
                Response.Write("<script>alert('Adna go we!!')</script>");
                GridView1.DataBind();

            }
            catch (Exception exeption)
            {
                Response.Write("<script>alert('" + exeption.Message + "')</script>");
            }
        }
    }
}