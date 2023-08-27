<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="MyAccountPage.aspx.cs" Inherits="M_sheet.MyAccountPage" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>

        <div class="form-group">
            <div class="row">
                <div class="col-6">
                    <div class="form-group row">
                        <asp:Label ID="Label1" runat="server" class="col-3 col-form-label" Text="Name"></asp:Label>
                        <div class="col-6">
                            <asp:TextBox ID="txtName" runat="server" TextMode="SingleLine" class="form-control "></asp:TextBox>
                        </div>
                    </div>
                   
                    <div class="form-group row">
                        <asp:Label ID="Label2" runat="server" class="col-3 col-form-label" Text="Rank"></asp:Label>
                        <div class="col-6">
                            <asp:TextBox ID="txtRank" runat="server" TextMode="SingleLine" class="form-control "></asp:TextBox>
                        </div>
                    </div>
                    
                    <div class="form-group row">
                        <asp:Label ID="Label3" runat="server" class="col-3 col-form-label" Text="Department"></asp:Label>
                        <div class="col-6">
                            <asp:TextBox ID="txtDepartment" runat="server" TextMode="SingleLine" class="form-control "></asp:TextBox>
                        </div>
                    </div>
                </div>
                
                <div class="col-6">
                    <div class="form-group row">
                        <asp:Label ID="Label4" runat="server" class="col-3 col-form-label" Text="Appointment"></asp:Label>
                        <div class="col-9">
                            <asp:TextBox ID="txtAppointment" runat="server" TextMode="SingleLine" class="form-control "></asp:TextBox>
                        </div>
                    </div>
                 
                    <div class="form-group row">
                        <asp:Label ID="Label5" runat="server" class="col-3 col-form-label" Text="Ship/Base"></asp:Label>
                        <div class="col-9">
                            <asp:TextBox ID="txtBase" runat="server" TextMode="SingleLine" class="form-control "></asp:TextBox>
                        </div>
                    </div>
                   
                    <div class="form-group row">
                        <asp:Label ID="Label6" runat="server" class="col-3 col-form-label" Text="Naval Area"></asp:Label>
                        <div class="col-9">
                            <asp:TextBox ID="txtCommand" runat="server" TextMode="SingleLine" class="form-control "></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <h4 class="card-title">Minutes Inbox</h4>
            </div>
            <br />
            <div class="row alert alert-dismissible alert-primary">
                <asp:Label ID="Label7" runat="server" class="col-1 col-form-label" Text="Search"></asp:Label>

                <div class="col-2">
                    <asp:DropDownList ID="dpoption" runat="server" Class="form-control">
                    </asp:DropDownList>
                </div>
                <div class="col-3">
                    <asp:TextBox ID="txtsearch" runat="server" class="form-control" placeholder="Search text"></asp:TextBox>
                </div>
                <div class="col-1">

                    <asp:Button ID="btnSearch" runat="server" Text="Search" class="btn btn-primary" OnClick="btnSearch_Click" />
                </div>
            </div>

            <div class="row card text-white bg-dark mb-3">
                <asp:GridView ID="GVInbox" runat="server" Height="67px" Width="1285px"
                    OnPageIndexChanging="GVInbox_PageIndexChanging"
                    OnRowCommand="GVInbox_RowCommand"
                    OnRowDataBound="GVInbox_RowDataBound"
                    OnSelectedIndexChanged="GVInbox_SelectedIndexChanged"
                    OnRowUpdating="GVInbox_RowUpdating" BackColor="White" 
                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:ButtonField CommandName="Update" HeaderText="Archive" ShowHeader="True"
                            Text="Archive" />
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                </asp:GridView>

            </div>




        </div>
    </fieldset>
</asp:Content>
