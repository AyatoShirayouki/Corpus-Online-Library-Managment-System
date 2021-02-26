<%@ Page Title="" Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs" Inherits="OnlineLibrary.AdminLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPLaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-6 mx-auto">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <img width="250px" src="img/admin-with-cogwheels.png" />
                                </center>
                            </div>
                        </div>
                         <div class="row">
                            <div class="col">
                                <center>
                                   <h3>Login As Admin</h3>
                                </center>
                            </div>
                        </div>
                         <div class="row">
                            <div class="col">
                                <center>
                                    <hr />
                                </center>
                            </div>
                        </div>
                         <div class="row">
                            <div class="col">
                                <label>Username:</label>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="TextBox1" placeholder="Username" runat="server">
                                    </asp:TextBox>
                                </div>
                                 <label>Password:</label>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="TextBox2" placeholder="Password" runat="server" TextMode="Password">
                                    </asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Button class="btn btn-primary btn-block btn-lg" ID="Button1" runat="server" Text="Login" OnClick="Button1_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <a href="Home.aspx"><--- Home</a>
                <br /><br />
            </div>
        </div>
    </div>
    
</asp:Content>
