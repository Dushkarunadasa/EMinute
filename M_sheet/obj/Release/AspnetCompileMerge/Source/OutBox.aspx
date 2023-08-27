<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="OutBox.aspx.cs" Inherits="M_sheet.OutBox" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>
        <div class="form-group">
            <div class="row">
                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                    AutoDataBind="True" HasToggleGroupTreeButton="False"   />

                   
            </div>
            <br />
            <div class="row">
                <asp:GridView ID="grReference" runat="server"
                    AllowSorting="True" AlternatingRowStyle-BackColor="White"
                    AlternatingRowStyle-ForeColor="#000" AutoGenerateColumns="False"
                    HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White" Height="112px"
                    OnSelectedIndexChanged="grReference_SelectedIndexChanged2"
                    RowStyle-BackColor="#A1DCF2" Width="484px" CellPadding="4" 
                    ForeColor="#333333" GridLines="None">
<AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                    <Columns>
                        <asp:BoundField DataField="Path" HeaderText="Path"
                            ItemStyle-Width="50" >
<ItemStyle Width="50px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Reference"
                            HeaderText="Reference" ItemStyle-Width="250" SortExpression="Path" >
<ItemStyle Width="250px"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <%--     <a href=Eval("Path").ToString() target="_blank">View PDF</a>

                                    <asp:HyperLink ID="HyperLink1" Target="_blank"  runat="server" NavigateUrl='<%# string.Format("~/RefBlankPage.aspx?Path={0}&Reference={1}",
                    HttpUtility.UrlEncode(Eval("Path").ToString()), HttpUtility.UrlEncode(Eval("Reference").ToString())) %>'
                    Text="View Details" />--%>
                                <asp:HyperLink ID="HyperLink1" runat="server"
                                    ItemStyle-Width="50" NavigateUrl='<%# Eval("Path").ToString() %>'
                                    Target="_blank" Text="View Details" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />

<HeaderStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True"></HeaderStyle>

                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />

<RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </div>
            <br />
            <div class="row">
                <asp:GridView ID="grReference0"
                    HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
                    RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White" AlternatingRowStyle-ForeColor="#000"
                    runat="server" AutoGenerateColumns="False" Height="112px"
                    OnSelectedIndexChanged="grReference_SelectedIndexChanged2"
                    Width="484px" AllowSorting="True" BackColor="White" BorderColor="White" 
                    BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" 
                    GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="Path" HeaderText="Path" ItemStyle-Width="50" >
<ItemStyle Width="50px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Reference" HeaderText="Annexure"
                            ItemStyle-Width="250" SortExpression="Path" >
<ItemStyle Width="250px"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <%--     <a href=Eval("Path").ToString() target="_blank">View PDF</a>

                                    <asp:HyperLink ID="HyperLink1" Target="_blank"  runat="server" NavigateUrl='<%# string.Format("~/RefBlankPage.aspx?Path={0}&Reference={1}",
                    HttpUtility.UrlEncode(Eval("Path").ToString()), HttpUtility.UrlEncode(Eval("Reference").ToString())) %>'
                    Text="View Details" />--%>
                                <asp:HyperLink ID="HyperLink2" Target="_blank" runat="server"
                                    NavigateUrl='<%# Eval("Path").ToString() %>' Text="View Details"
                                    ItemStyle-Width="50" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />

<HeaderStyle BackColor="#4A3C8C" ForeColor="#E7E7FF" Font-Bold="True"></HeaderStyle>

                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />

<RowStyle BackColor="#DEDFDE" ForeColor="Black"></RowStyle>
                    <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#594B9C" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#33276A" />
                </asp:GridView>
            </div>
            <br />
            <div class="row">
                  <asp:GridView ID="GVSent_Min" runat="server" Height="92px"
                OnSelectedIndexChanged="GVSent_Min_SelectedIndexChanged"
                Style="margin-left: 0px" Width="1170px" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
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

        </div>

    </fieldset>   
</asp:Content>
