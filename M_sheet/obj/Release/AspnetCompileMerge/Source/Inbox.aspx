<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Inbox.aspx.cs" Inherits="M_sheet.Inbox" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <fieldset>

        <div class="form-group">
            <div class="row">
                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                    AutoDataBind="true" HasToggleGroupTreeButton="False" ToggleSidePanel="None" ShowToggleSidePanelButton="False" />
                   
            </div>
            <br />
            <div class="row alert alert-dismissible alert-primary">
                <asp:GridView ID="grReference" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
                    RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White" AlternatingRowStyle-ForeColor="#000"
                    runat="server" AutoGenerateColumns="false" Height="112px"
                    OnSelectedIndexChanged="grReference_SelectedIndexChanged2"
                    Width="484px">
                    <Columns>
                        <asp:BoundField DataField="Path" HeaderText="Path" ItemStyle-Width="50" />
                        <asp:BoundField DataField="Reference" HeaderText="Reference"
                            ItemStyle-Width="250" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <%--     <a href=Eval("Path").ToString() target="_blank">View PDF</a>

                                    <asp:HyperLink ID="HyperLink1" Target="_blank"  runat="server" NavigateUrl='<%# string.Format("~/RefBlankPage.aspx?Path={0}&Reference={1}",
                    HttpUtility.UrlEncode(Eval("Path").ToString()), HttpUtility.UrlEncode(Eval("Reference").ToString())) %>'
                    Text="View Details" />--%>
                                <asp:HyperLink ID="HyperLink1" Target="_blank" runat="server" NavigateUrl='<%# Eval("Path").ToString() %>' Text="View Details" ItemStyle-Width="50" />

                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </div>
            <br />
            <div class="row alert alert-dismissible alert-primary">
                <asp:GridView ID="grReference0" runat="server"
                    AlternatingRowStyle-BackColor="White" AlternatingRowStyle-ForeColor="#000"
                    AutoGenerateColumns="false" HeaderStyle-BackColor="#3AC0F2"
                    HeaderStyle-ForeColor="White" Height="112px"
                    OnSelectedIndexChanged="grReference_SelectedIndexChanged2"
                    RowStyle-BackColor="#A1DCF2" Width="484px">
                    <Columns>
                        <asp:BoundField DataField="Path" HeaderText="Path" ItemStyle-Width="50" />
                        <asp:BoundField DataField="Reference" HeaderText="Enclosure"
                            ItemStyle-Width="250" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <%--     <a href=Eval("Path").ToString() target="_blank">View PDF</a>

                                    <asp:HyperLink ID="HyperLink1" Target="_blank"  runat="server" NavigateUrl='<%# string.Format("~/RefBlankPage.aspx?Path={0}&Reference={1}",
                    HttpUtility.UrlEncode(Eval("Path").ToString()), HttpUtility.UrlEncode(Eval("Reference").ToString())) %>'
                    Text="View Details" />--%>
                                <asp:HyperLink ID="HyperLink2" runat="server" ItemStyle-Width="50"
                                    NavigateUrl='<%# Eval("Path").ToString() %>' Target="_blank"
                                    Text="View Details" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <br />

            <div class="row ">

                <div class="col-9">
                </div>
                <div class="col-2">

                    <asp:Button ID="BtnSubmit" runat="server" OnClick="Button1_Click" Text="Respond"
                        class="form-control btn btn-primary" />

                </div>
                 <div class="col-1">
                </div>

            </div>



        </div>
    </fieldset>
</asp:Content>
