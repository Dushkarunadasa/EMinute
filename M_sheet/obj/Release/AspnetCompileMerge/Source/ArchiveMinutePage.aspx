<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="ArchiveMinutePage.aspx.cs" Inherits="M_sheet.ArchiveMinutePage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>
        <div class="form-group">
            <div class="row">
                <h5>Archive Minutes...</h5>
            </div>
            <br />
            <div class="row alert alert-dismissible alert-primary">
                <asp:Label ID="Label7" runat="server" class="col-1 col-form-label" Text="Search"></asp:Label>

                <div class="col-2">
                    <asp:DropDownList ID="dpoption" runat="server" Class="form-control">
                    </asp:DropDownList>
                </div>
                <div class="col-3">
                    <asp:TextBox ID="txtsearch" runat="server" class="form-control" placeholder="Default input"></asp:TextBox>
                </div>
                <div class="col-1">

                    <asp:Button ID="btnSearch" runat="server" Text="Search" class="btn btn-primary" OnClick="btnSearch_Click" />
                </div>
            </div>

            <br />
            <div class="row">
                <asp:GridView ID="GVInbox" runat="server" Height="61px" Width="1179px"
                    OnPageIndexChanging="GVInbox_PageIndexChanging"
                    OnRowCommand="GVInbox_RowCommand"
                    OnRowDataBound="GVInbox_RowDataBound"
                    OnSelectedIndexChanged="GVInbox_SelectedIndexChanged"
                    OnRowUpdating="GVInbox_RowUpdating" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:ButtonField CommandName="Update" HeaderText="Restore" ShowHeader="True"
                            Text="Restore" />
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
            <br />
            <div class="row">
                <div class="col-4">
                 <asp:Button ID="btnMinuteIn" runat="server" Text="Minute Sheet In"
                 
                    OnClick="btnMinuteIn_Click" />
                    </div> 

              <div class="col-2">
                  <asp:Button ID="btnMinuteOut" runat="server" Text="Minute Sheet Out"
                
                    OnClick="btnMinuteOut_Click" />
                  </div> 
            </div>
        </div>
    </fieldset>
</asp:Content>
