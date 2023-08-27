<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="UserUpdate.aspx.cs" Inherits="M_sheet.UserUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   <div style="background-color:#0099FF; color:White"><b style="font-size: large">EDIT 
       USERS</b></div>


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
                Appointment</td>
             <td class="style60" colspan="4">
                 <asp:TextBox ID="TextBox2" runat="server" Width="100%"></asp:TextBox>
                </td>

        </tr>
      
        <tr>
            <td class="style61">
                Other
                Appointment</td>
             <td class="style60" colspan="4">
                 <asp:TextBox ID="TextBox5" runat="server" Width="100%"></asp:TextBox>
                </td>

        </tr>
      
        <tr>
            <td class="style61">
                Area</td>
             <td class="style60" colspan="3">
                 <asp:TextBox ID="TextBox3" runat="server" Width="80%" ReadOnly="True"></asp:TextBox>
                </td>

            <td>
                <asp:DropDownList ID="areaSelect" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="areaSelect_SelectedIndexChanged" Visible="False">
                </asp:DropDownList>
            </td>

        </tr>
      
        <tr>
            <td class="style61">
                Ship/Establishment</td>
             <td class="style60" colspan="3">
                 <asp:TextBox ID="TextBox4" runat="server" Width="80%" ReadOnly="True"></asp:TextBox>
                </td>

            <td>
                <asp:DropDownList ID="Ship_EstablishmentSelect" runat="server" Visible="False" 
                    AutoPostBack="True" 
                    onselectedindexchanged="Ship_EstablishmentSelect_SelectedIndexChanged">
                </asp:DropDownList>
            </td>

        </tr>
      
        <tr>
            <td class="style61">
                Department</td>
             <td class="style60" colspan="3">
                 <asp:TextBox ID="TextBox6" runat="server" Width="50%" ReadOnly="True"></asp:TextBox>
                </td>

            <td>
                <asp:DropDownList ID="cmbDepartment" runat="server" Visible="False" 
                    AutoPostBack="True" onselectedindexchanged="cmbDepartment_SelectedIndexChanged">
                </asp:DropDownList>
            </td>

        </tr>
      
        <tr>
            <td class="style51" colspan="2">
                <asp:Label ID="Label1" runat="server" ForeColor="#00CC66"></asp:Label>
            </td>
            <td class="style51" colspan="2">
                &nbsp;</td>
            <td>
                <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="UPDATE" 
                    Width="109px" />
            </td>
        </tr>
        </table>
</asp:Content>

