<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="MinutesMonitor.aspx.cs" Inherits="M_sheet.MinutesMonitor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background-color:#0099FF; color:White"><b style="font-size: large">MINUTES MONITOR</b></div>


        <br />

 <table width=100% >
 
      
        <tr>
            <td class="style61">
                By Off No <span class="style62">(Ex: NRT3350)</span></td>
             <td>
                 <asp:TextBox ID="TextBox1" runat="server" Width="75px"></asp:TextBox>
                 <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Search" />
            </td>
             <td class="style60">
                 &nbsp;</td>

            <td class="style27">
                &nbsp;</td>
                <td class="style52">
                    &nbsp;</td>
        </tr>
      
        <tr>
            <td class="style61">
                By Appointment</td>
             <td class="style60">
                <asp:DropDownList ID="DropDownList2" runat="server">
                </asp:DropDownList>
                 <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Search" />
                </td>

            <td class="style27">
                &nbsp;</td>
                <td class="style52">
                    &nbsp;</td>
        </tr>
      
        <tr>
            <td class="style51" colspan="4">
             <asp:GridView ID="GVInbox" runat="server" class="table table-hover" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"  EnableSortingAndPagingCallbacks="True" BorderWidth="1px" CellPadding="3">
                <Columns>
                <asp:BoundField DataField="MIN_ID" HeaderText="M_ID" />
                   <asp:BoundField DataField="BY_NAME" HeaderText="BY_NAME"/>
                    <asp:BoundField DataField="BY_APPOINTMENT" HeaderText="BY_APPOINTMENT" />
                    <asp:BoundField DataField="FORWARD_TO" HeaderText="FORWARD_TO" />
                    <asp:BoundField DataField="MINUTE_SHEET_DATE" HeaderText="DATE"  DataFormatString="{0:MM/dd/yyyy}" />
                    <asp:BoundField DataField="READ_" HeaderText="READ_" />
                    <asp:BoundField DataField="READ_TIME" HeaderText="READ_TIME" />
                    <asp:BoundField DataField="RESPOND_" HeaderText="RESPOND_" />
                    <asp:BoundField DataField="RESPOND_TIME" HeaderText="RESPOND_TIME" />

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
                </td>
        </tr>
        </table>
</asp:Content>
