<%@ Page Title="" Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true" CodeBehind="Books.aspx.cs" Inherits="OnlineLibrary.Books" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
            $(document).ready(function () {
                $(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
            });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPLaceHolder1" runat="server">
    <div class="container">
            <div class="row">
 
                <div class="col-sm-12">
                    <center>
                        <h2>Our Books</h2>
                    </center>
                    <div class="row">
                        <div class="col-sm-12 col-md-12">
                            <asp:Panel class="alert alert-success" role="alert" ID="Panel1" runat="server" Visible="False">
                                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                            </asp:Panel>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="card">
                            <div class="card-body">
 
                                <div class="row">
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CorpusLibraryDBConnectionString5 %>" SelectCommand="SELECT * FROM [Books]"></asp:SqlDataSource>
                                    <div class="col">
                                        <asp:GridView class="table table-striped table-bordered" ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="bookId" DataSourceID="SqlDataSource1">
                                            <Columns>
                                                <asp:BoundField DataField="bookId" HeaderText="ID" ReadOnly="True" SortExpression="bookId">
                                                    <ControlStyle Font-Bold="True" />
                                                    <ItemStyle Font-Bold="True" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <div class="container-fluid">
                                                            <div class="row">
                                                                <div class="col-lg-10">
                                                                    <div class="row">
                                                                        <div class="col-12">
                                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("bookName") %>' Font-Bold="True" Font-Size="X-Large"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-12">
                                                                            <span>Author - </span>
                                                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Text='<%# Eval("authorName") %>'></asp:Label>
                                                                            &nbsp;| <span><span>&nbsp;</span>Genre - </span>
                                                                            <asp:Label ID="Label3" runat="server" Font-Bold="True" Text='<%# Eval("genre") %>'></asp:Label>
                                                                            &nbsp;|
                                                                            <span>
                                                      Language -<span>&nbsp;</span>
                                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Text='<%# Eval("language") %>'></asp:Label>
                                                                            </span>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-12">
                                                                            Publisher -
                                                                            <asp:Label ID="Label5" runat="server" Font-Bold="True" Text='<%# Eval("publisherName") %>'></asp:Label>
                                                                            &nbsp;| Publish Date -
                                                                            <asp:Label ID="Label6" runat="server" Font-Bold="True" Text='<%# Eval("publishDate") %>'></asp:Label>
                                                                            &nbsp;| Pages -
                                                                            <asp:Label ID="Label7" runat="server" Font-Bold="True" Text='<%# Eval("pages") %>'></asp:Label>
                                                                            &nbsp;| Edition -
                                                                            <asp:Label ID="Label8" runat="server" Font-Bold="True" Text='<%# Eval("edition") %>'></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-12">
                                                                            Cost -
                                                                            <asp:Label ID="Label9" runat="server" Font-Bold="True" Text='<%# Eval("bookCost") %>'></asp:Label>
                                                                            &nbsp;| Actual Stock -
                                                                            <asp:Label ID="Label10" runat="server" Font-Bold="True" Text='<%# Eval("actualStock") %>'></asp:Label>
                                                                            &nbsp;| Available Stock -
                                                                            <asp:Label ID="Label11" runat="server" Font-Bold="True" Text='<%# Eval("currentStock") %>'></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-12">
                                                                            Description -
                                                                            <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Smaller" Text='<%# Eval("bookDescription") %>'></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-2">
                                                                    <asp:Image class="img-fluid" ID="Image1" runat="server" ImageUrl='<%# Eval("bookImgLink") %>' />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <center>
                    <a href="home.aspx"><< Back to Home</a>
                    <span class="clearfix"></span>
                     <br />
                    
                <center>
            </div>
        </div>
</asp:Content>
