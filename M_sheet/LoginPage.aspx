<%@ Page Title="" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="M_Sheet.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-12 my-auto">

        <div class="masthead-content text-white py-5 py-md-5">
            <h1 class="mb-3">E-Minute Sheet</h1>
            <p class="mb-5">Digitalized  Minute Sheet ciruclation system</p>
            <%--<div class="input-group input-group-newsletter">--%>
            <div class="form-group">
                <div class="row">
                    <div class="col-5">
                        <asp:Label ID="Label1" runat="server" class="col-form-label" Text="User Type" Visible="false"></asp:Label>
                    </div>
                    <div class="col-7">
                        <asp:DropDownList ID="dpUserTypes" runat="server" class="form-control" Visible="false">
                            <asp:ListItem>User</asp:ListItem>
                            <asp:ListItem>Staff</asp:ListItem>
                            <asp:ListItem>Authorized User</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <br />

                <div class="row">


                    <%--    <div class="input-container">
                            <i class="fa fa-envelope icon form-control " ></i>
                            <asp:TextBox ID="" runat="server" CssClass="form-control" MaxLength="50" placeholder=""></asp:TextBox>--%>

                    <div class="input-container">
                        <i class="fa fa-envelope icon"></i>
                    
                          <asp:TextBox ID="txtUsername" runat="server" TextMode="SingleLine" placeholder="SLN email address"  MaxLength="100" Width ="250px"></asp:TextBox>

                    </div>


                </div>

                <%--</div>--%>



                <div class="row">

         

                      <div class="input-container">
                      <i class="fa fa-key icon"></i>
                           <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password"  MaxLength="50" Width ="250px"></asp:TextBox>

  
                    </div>

                </div>

                <br />
                <div class="row">
                    <div class="col-5 ">
                        <asp:Button ID="btnLogin" runat="server" Width="100px" Height ="40px" OnClick="btnLogin_Click" Text="Login" class="btn btn-primary" />


                    </div>
                    <div class="col-7">
                       
                            <asp:Label ID="lblinfo" runat="server" Class="text-danger" Text=""></asp:Label>
                        
                    </div>
                </div>

            </div>




        </div>
        <%--</div>--%>
    </div>
    &nbsp;
</asp:Content>

