<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="RegisteredUsers.aspx.cs" Inherits="M_sheet.RegisteredUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<br />
 <div style="background-color:#0099FF; color:White"><b style="font-size: large">
     Registered Users</b></div>


        <br />

 <table width=100% >
 
      
        <tr>
            <td class="style61">
                Ship / Establishments</td>
             <td>
                <asp:DropDownList ID="dpBase" runat="server">
                </asp:DropDownList>
            </td>
             <td class="style60">
                <asp:Button 
                        ID="btnStaff" runat="server" 
                        onclick="btnStaff_Click" Text="SEARCH" Width="126px" />
            </td>

            <td class="style27">
                &nbsp;</td>
                <td class="style52">
                    &nbsp;</td>
        </tr>
      
        <tr>
            <td class="style61">
                &nbsp;</td>
             <td class="style60">
                 &nbsp;</td>

            <td class="style27">
                &nbsp;</td>
                <td class="style52">
                    &nbsp;</td>
        </tr>
      
        <tr>
            <td class="style51" colspan="4">
                <asp:GridView 
                    ID="GVInbox" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="1205px">
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
                </td>
        </tr>
        </table>
        <br />
   
</asp:Content>
