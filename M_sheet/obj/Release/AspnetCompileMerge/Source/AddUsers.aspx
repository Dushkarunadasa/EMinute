<%@ Page Title="Add Users" Language="C#"  AutoEventWireup="true" CodeFile="AddUsers.aspx.cs" Inherits="M_sheet.AddUsers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>


  <style type="text/css">
        .style1
        {
            height: 27px;
            width: 584px;
        }
        .style3
        {
            height: 27px;
            text-align: right;
            font-weight: 700;
            color: #FF0000;
        }
        .style5
        {
            width: 120px;
        }
        .style9
        {
            height: 1px;
            width: 584px;
        }
        .style11
        {
            height: 24px;
            width: 584px;
        }
        .style12
        {
            width: 120px;
            height: 29px;
        }
        .style13
        {
            height: 21px;
            width: 584px;
        }
        .style14
        {
            height: 27px;
            width: 449px;
        }
        .style15
        {
            height: 1px;
            width: 449px;
        }
        .style16
        {
            height: 24px;
            width: 449px;
        }
        .style17
        {
            width: 449px;
        }
        .style18
        {
            height: 21px;
            width: 449px;
        }
        .style20
        {
            height: 30px;
            width: 449px;
            text-align: justify;
        }
        .style21
        {
            height: 30px;
            width: 584px;
        }
        .style22
        {
            width: 584px;
        }
        #Text1
        {
            width: 298px;
        }
        #txtOffNo
        {
            width: 167px;
        }
        #Select1
        {
            width: 271px;
        }
        .style24
        {
            height: 30px;
            width: 197px;
        }
        .style25
        {
            height: 30px;
            width: 120px;
            font-size: small;
        }
        .style29
        {
            height: 30px;
            text-align: right;
            font-weight: 700;
            color: #FF0000;
        }
        .style30
        {
            width: 120px;
            height: 18px;
        }
        .style31
        {
            height: 18px;
            width: 449px;
        }
        .style32
        {
            height: 18px;
            width: 584px;
        }
        .style33
        {
            height: 28px;
            text-align: right;
            font-weight: 700;
            color: #FF0000;
        }
        .style34
        {
            height: 28px;
            width: 449px;
        }
        .style35
        {
            height: 28px;
            width: 584px;
        }
        .style36
        {
            height: 29px;
            width: 449px;
        }
        .style37
        {
            height: 29px;
            width: 584px;
        }
        .style38
        {
            color: #0000FF;
        }
    </style>

<body>
    <form id="form1" runat="server">
    <div>
    <table style="width: 99%; height: 529px;" title="Registration">
        <tr>
            <td class="style3" bgcolor="#CC99FF" colspan="5" style="color: #FFFFFF">
                &nbsp; <b>&nbsp;User Registration ...</b></td>
        </tr>
        <tr>
            <td class="style3">
                </td>
            <td class="style14">
                </td>
            <td class="style1" colspan="2">
                <asp:Label ID="lblinfo" runat="server" Font-Bold="True" Font-Names="Arial" 
                    Font-Size="Medium" ForeColor="Red"></asp:Label>
            </td>
            <td class="style1">
                </td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td class="rightColumn">
                <asp:Label ID="Label13" runat="server" BorderStyle="None" Font-Bold="True" 
                    Text="Official No (Ex. 3350)"></asp:Label>
            &nbsp;:
            </td>
            <td class="style1" colspan="2">
                <asp:TextBox ID="txtOfficialNumber" runat="server" BorderColor="#B0B0FF"
                    BorderStyle="Solid" Width="152px" MaxLength="4" TextMode="Number" ></asp:TextBox>
                &nbsp;&nbsp;<asp:DropDownList ID="ddlServiceType" runat="server" Height="25px" 
                    Width="74px">
                    <asp:ListItem>RNF</asp:ListItem>
                    <asp:ListItem>VNF</asp:ListItem>
                    <asp:ListItem>RNR</asp:ListItem>
                    <asp:ListItem>VNR</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnSearch" runat="server" onclick="btnSearch_Click" 
                    Text="Search" ForeColor="#FF3300" />
                </td>
            <td class="style1">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td class="style15">
                <asp:Label ID="Label223" runat="server" BorderStyle="None" Font-Bold="True" 
                    Text="Branch :"></asp:Label>
            </td>
            <td class="style9" colspan="2">
                <asp:TextBox ID="txtbranch" runat="server" BorderColor="#B0B0FF"
                    BorderStyle="Solid" Width="80px" MaxLength="7" ></asp:TextBox>
            </td>
            <td class="style9">
                <span class="style38"><strong>In Sinhala Medium</strong></span></td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td class="style16">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" 
                    Text="Name With Initial :"></asp:Label>
            </td>
            <td class="style11" colspan="2">
                <asp:TextBox ID="txtName" runat="server" Width="274px" BorderColor="#B0B0FF" 
                    BorderStyle="Solid" MaxLength="50"></asp:TextBox>
            </td>
            <td class="style11">
                <asp:TextBox ID="txtnameSin" runat="server" Width="285px" 
                    BorderColor="#B0B0FF" BorderStyle="Solid" MaxLength="50" 
                    BackColor="#CCCCCC"></asp:TextBox>
                <br />
                </td>
        </tr>
        <tr>
           <td class="style3">
                &nbsp;</td>
            <td class="style14">
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Text="Rank :"></asp:Label>
            </td>
            <td class="style1" colspan="2">
                <asp:TextBox ID="txtrank" runat="server" Width="234px" 
                    BorderColor="#B0B0FF" BorderStyle="Solid" MaxLength="50"></asp:TextBox>
            </td>
            <td class="style1">
                <asp:TextBox ID="txtrankSin" runat="server" Width="285px" 
                    BorderColor="#B0B0FF" BorderStyle="Solid" MaxLength="50" 
                    BackColor="#CCCCCC"></asp:TextBox>
                </td>
        </tr>
        <tr>
           <td class="style3">
                &nbsp;</td>
            <td class="style18">
                <asp:Label ID="Label222" runat="server" Font-Bold="True" Text="Appointment :"></asp:Label>
            </td>
            <td class="style13" colspan="2">
                <asp:TextBox ID="txtAppointment" runat="server" Width="303px" 
                    BorderColor="#B0B0FF" BorderStyle="Solid" MaxLength="50"></asp:TextBox>
            </td>
            <td class="style13">
                <asp:TextBox ID="txtAppointmentSin" runat="server" Width="285px" 
                    BorderColor="#B0B0FF" BorderStyle="Solid" MaxLength="50" 
                    BackColor="#CCCCCC"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td class="style33">
                &nbsp;</td>
            <td class="style34">
                <asp:Label ID="Label4" runat="server" Font-Bold="True" 
                    Text="Other Appointment :"></asp:Label>
            </td>
            <td class="style35" colspan="2">
                <asp:TextBox ID="txtOtherAppointment" runat="server" Width="303px" 
                    BorderColor="#B0B0FF" BorderStyle="Solid" MaxLength="50"></asp:TextBox>
            </td>
            <td class="style35">
                <asp:TextBox ID="txtOtherAppointmentSin" runat="server" Width="286px" 
                    BorderColor="#B0B0FF" BorderStyle="Solid" MaxLength="50" 
                    BackColor="#CCCCCC"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td class="style17">
                <asp:Label ID="Label5" runat="server" Font-Bold="True" Text="Command :"></asp:Label>
            </td>
            <td class="style22" colspan="2">
                <asp:TextBox ID="txtArea" runat="server" Width="303px" 
                    BorderColor="#B0B0FF" BorderStyle="Solid" MaxLength="50"></asp:TextBox>
            </td>
            <td class="style22">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style12">
                </td>
            <td class="style36">
                <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="Base / Ship :"></asp:Label>
            </td>
            <td class="style37" colspan="2">
                <asp:TextBox ID="txtBase" runat="server" Width="303px" 
                    BorderColor="#B0B0FF" BorderStyle="Solid" MaxLength="50"></asp:TextBox>
            </td>
            <td class="style37">
                </td>
        </tr>
        <tr>
          <td class="style3">
                &nbsp;</td>
            <td class="style17">
                <asp:Label ID="Label7" runat="server" Font-Bold="True" Text="Department :"></asp:Label>
            </td>
            <td class="style22" colspan="2">
                <asp:DropDownList ID="cmbDepartment" runat="server" Height="25px" Width="315px">
                </asp:DropDownList>
            </td>
            <td class="style22">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td class="style17">
                &nbsp;</td>
            <td class="style22" colspan="2">
                &nbsp;</td>
            <td class="style22">
                &nbsp;</td>
        </tr>
        <tr>
          <td class="style29">
                &nbsp;</td>
            <td class="style20">
                &nbsp;</td>
            <td class="style24">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSubmit" runat="server" Text="Save" Width="99px" 
                    onclick="btnSubmit_Click" style="height: 26px" /></td>
            <td class="style25">
                <asp:Button ID="btnClear" runat="server" Text="Clear" Width="106px" 
                    onclick="btnClear_Click" />
            
            </td>
            <td class="style21">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style30">
                </td>
            <td class="style31">
                </td>
            <td class="style32" colspan="2">
                <br />
                <br />
                <br />
            </td>
            <td class="style32">
                </td>
        </tr>
        <tr>
            <td class="style5">
                &nbsp;</td>
            <td class="style17">
                &nbsp;</td>
            <td class="style22" colspan="2">
                &nbsp;</td>
            <td class="style22">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style5">
                &nbsp;</td>
            <td class="style17">
                &nbsp;</td>
            <td class="style22" colspan="2">
                &nbsp;</td>
            <td class="style22">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style5">
                &nbsp;</td>
            <td class="style17">
                &nbsp;</td>
            <td class="style22" colspan="2">
                &nbsp;</td>
            <td class="style22">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style5">
                &nbsp;</td>
            <td class="style17">
                &nbsp;</td>
            <td class="style22" colspan="2">
                &nbsp;</td>
            <td class="style22">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style5">
                &nbsp;</td>
            <td class="style17">
                &nbsp;</td>
            <td class="style22" colspan="2">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            
            </td>
            <td class="style22">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style5">
                &nbsp;</td>
            <td class="style17">
                &nbsp;</td>
            <td class="style22" colspan="2">
                &nbsp;</td>
            <td class="style22">
                &nbsp;</td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
