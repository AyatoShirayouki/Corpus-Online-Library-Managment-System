<%@ Page Title="" Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="OnlineLibrary.SignUp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPLaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-8 mx-auto">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <img width="100px" src="img/sign-up%20(1).png" />
                                </center>
                            </div>
                        </div>
                         <div class="row">
                            <div class="col">
                                <center>
                                   <h3>Sign Up</h3>
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
                                <center>
                                    <span class="badge bg-info text-dark">Personal Information</span>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>Full Name:</label>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="TextBox1" placeholder="Full_Name" runat="server">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>Birth Date:</label>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="TextBox2" placeholder="Birth_Date" runat="server" TextMode="Date">
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                         <div class="row">
                            <div class="col-md-6">
                                <label>Phone Number:</label>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="TextBox3" placeholder="Phone_Number" runat="server" TextMode="Phone">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>Email:</label>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="TextBox4" aria-describedby="emailHelp" placeholder="Email" runat="server" TextMode="Email">
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-4">
                                <label>State:</label>
                                <div class="form-group">
                                    <asp:DropDownList class="form-control" ID="DropDownList1" runat="server">
                                        <asp:ListItem Text="Select" Value="Select"/>
                                        <asp:ListItem Text="Blagoevgrad" Value="Blagoevgrad"/>
                                        <asp:ListItem Text="Burgas" Value="Burgas"/>
                                        <asp:ListItem Text="Varna" Value="Varna"/>
                                        <asp:ListItem Text="Veliko Turnovo" Value="Veliko Turnovo"/>
                                        <asp:ListItem Text="Vidin" Value="Vidin"/>
                                        <asp:ListItem Text="Vraca" Value="Vraca"/>
                                        <asp:ListItem Text="Gabrovo" Value="Gabrovo"/>
                                        <asp:ListItem Text="Dobrich" Value="Dobrich"/>
                                        <asp:ListItem Text="Kurdjali" Value="Kurjali"/>
                                        <asp:ListItem Text="Kustendil" Value="Kustendil"/>
                                        <asp:ListItem Text="Lovech" Value="Lovech"/>
                                        <asp:ListItem Text="Montana" Value="Montana"/>
                                        <asp:ListItem Text="Pazarjik" Value="Pazarjik"/>
                                        <asp:ListItem Text="Pernik" Value="Pernik"/>
                                        <asp:ListItem Text="PLeven" Value="Pleven"/>
                                        <asp:ListItem Text="Plovdiv" Value="Plovdiv"/>
                                        <asp:ListItem Text="Razgrad" Value="Razgrad"/>
                                        <asp:ListItem Text="Ruse" Value="Ruse"/>
                                        <asp:ListItem Text="Silistra" Value="Silistra"/>
                                        <asp:ListItem Text="Sliven" Value="Sliven"/>
                                        <asp:ListItem Text="Smolqn" Value="Smolqn"/>
                                        <asp:ListItem Text="Sofiiska Oblast" Value="Sofiiska Oblastt"/>
                                        <asp:ListItem Text="Sofia" Value="Sofia"/>
                                        <asp:ListItem Text="Stara Zagora" Value="Stara Zagora"/>
                                        <asp:ListItem Text="Turgovishte" Value="Turgovishte"/>
                                        <asp:ListItem Text="Haskovo" Value="Haskovo"/>
                                        <asp:ListItem Text="Shumen" Value="Shumen"/>
                                        <asp:ListItem Text="Yambol" Value="Yambol"/>
                                    </asp:DropDownList>
                                </div>
                            </div>
                         
                            <div class="col-md-4">
                                <label>City:</label>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="TextBox6" placeholder="City" runat="server">
                                        
                                    </asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <label>Pin Code:</label>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="TextBox7" placeholder="Pin_Code" runat="server" TextMode="Number">
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                        
                         <div class="row">
                            <div class="col">
                                <label>Address:</label>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="TextBox5" placeholder="Address" runat="server" TextMode="MultiLine">
                                    </asp:TextBox>
                                </div>
                             </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <span class="badge bg-info text-dark">Login Information</span>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>Username:</label>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="TextBox8" placeholder="Username" runat="server">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>Password:</label>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="TextBox9" placeholder="Password" runat="server" TextMode="Password">
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                         <div class="row">
                            <div class="col">
                                <div class="form-group">
                                    <asp:Button class="btn btn-primary btn-block btn-lg" ID="Button1" runat="server" Text="Sign Up" OnClick="Button1_Click" />
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
