<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="NewMinute.aspx.cs" Inherits="M_sheet.NewMinute" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <fieldset>
        <div class="form-group row">

            <div class="col-3  ">
                <h2>New Minute Sheet</h2>
            </div>
            <div class="col-7">
            </div>
            <div class="col-sm-1">
                <label for="exampleSelect1">Medium</label>
            </div>
            <div class="col-1">
                <asp:DropDownList ID="dpMedium" runat="server"
                    OnSelectedIndexChanged="dpMedium_SelectedIndexChanged1"
                    AutoPostBack="True" CssClass="form-control">
                    <asp:ListItem>English</asp:ListItem>
                    <asp:ListItem>Sinhala</asp:ListItem>
                </asp:DropDownList>
            </div>


        </div>

        <div class="row border-bottom-info ">
            <div class="col-3">
                <asp:Label ID="lblMinuteSheetNo" runat="server"></asp:Label>


            </div>
            <div class="col-9 text-center">
                <u>
                    <asp:Label ID="lblMinuteHead" runat="server" class="blockquote text-dark text-lg text-dark ">MINUTE SHEET</asp:Label>
                </u>
            </div>

        </div>

        <div class="row ">
            <div class="col-3 border-left-info  ">
                <div class="border-bottom-info">
                    <asp:Label ID="lblRef" runat="server" Height="38px" CssClass="text-dark">REFER TO:</asp:Label>

                </div>
                <br />

                <asp:Label ID="lblMinuteNo" runat="server" class="col-sm-2 col-form-label text-dark">0000</asp:Label>

                <br />


                <div class="form-group  ">
                    <asp:DropDownList ID="dpBase" runat="server" class="form-control" OnSelectedIndexChanged="dpBase_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                    <br />
                    <asp:DropDownList ID="dptype" runat="server" class="form-control"
                        AutoPostBack="true" OnSelectedIndexChanged="dptype_SelectedIndexChanged">
                        <asp:ListItem>----Select----</asp:ListItem>
                        <asp:ListItem>Name</asp:ListItem>
                        <asp:ListItem>Appointment</asp:ListItem>
                        <asp:ListItem>Other Appointment</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:DropDownList ID="dltlist" runat="server" class="form-control "
                        OnSelectedIndexChanged="dltlist_SelectedIndexChanged">
                    </asp:DropDownList>
                    <br />
                    <asp:DropDownList ID="DDGroup" runat="server" class="form-control " Visible="False" OnPreRender="DDGroup_PreRender">
                    </asp:DropDownList>
                    <br />
                    <asp:Button ID="btnAdd" runat="server" class="form-control btn btn-primary btn-sm " OnClick="btnAdd_Click" Text="Add" />

                    <br />

                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" class="form-control"
                        OnRowDeleting="GridView1_RowDeleting"
                        OnSelectedIndexChanged="GridView1_SelectedIndexChanged2" Width="192px" 
                        CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="OfficialNo" HeaderText="SNO" />
                            <asp:BoundField DataField="Name" HeaderText="NAME" />
                            <asp:TemplateField HeaderText="DELETE">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="delete">Delete</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>


                </div>
            </div>
            <div class="col-9 text-center border-left-info ">
                <div class="text-center border-bottom-info">
                    <asp:TextBox ID="txtSubject" runat="server" class="form-control text-uppercase text-center" placeholder="Enter Minute's Topic"></asp:TextBox>
                </div>
                <br />
                <div class="text-center ">
                    <u>
                        <asp:Label ID="lblM1" runat="server" class="col-sm-2 col-form-label text-center text-dark">M:1</asp:Label>
                    </u>
                </div>
                <br />
                <div class="form-group row">
                    <asp:Label ID="lblBy" runat="server" class="col-sm-1 col-form-label text-left text-dark">BY :</asp:Label>
                    <div class="col-sm-9">
                        <asp:DropDownList ID="dpBy" runat="server" OnSelectedIndexChanged="dpBy_SelectedIndexChanged" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>

                <div id="accordion">

                    <div class="form-group row">
                        <asp:Label ID="lblRefSin" runat="server" CssClass="col-sm-1 col-form-label text-left text-dark">REF</asp:Label>

                        <div class="col-sm-8">
                            <asp:TextBox ID="txtref1" runat="server" class="form-control" placeholder="Enter first reference"></asp:TextBox>

                        </div>
                        <div class="col-sm-1">
                            <button class="btn btn-primary btn-sm " type="button" data-toggle="collapse" data-target="#collapseRef1" aria-expanded="false" aria-controls="collapseExample">
                                >></button>
                        </div>

                    </div>
                    <div class="collapse" id="collapseRef1">

                        <div class="form-group row">
                            <div class="col-sm-1"></div>
                            <div class="col-sm-4">
                                <div class="input-group mb-3">
                                    <div class="custom-file">
                                        <asp:FileUpload ID="fuRef1" runat="server" class="custom-file-input" />
                                        <label class="custom-file-label" for="inputGroupFile02">Choose file</label>
                                    </div>
                                    <div class="input-group-append">
                                        <asp:Button ID="btnUpload1" runat="server" Text="Upload" OnClick="btnUpload1_Click" class="input-group-text" />

                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-sm-2">

                                <asp:Image ID="refImage1" runat="server" Height="25px" Width="25px" ImageAlign="Baseline" ImageUrl="~/Images/ok.png" Visible="False" />
                            </div>
                            <div class="col-sm-sm-2">

                                <asp:ImageButton ID="ImageButton1" runat="server" EnableTheming="False" ImageUrl="~/Images/Delete_icon.png" OnClick="ImageButton1_Click"
                                    Height="35px" Width="35px" />
                            </div>
                            <div class="col-sm-4">
                                <asp:Label ID="lblRef1" runat="server" Text="Label" Visible="true"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="col-sm-1">
                        </div>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtref2" runat="server" CssClass="form-control" placeholder="Enter second reference"></asp:TextBox>

                        </div>
                        <div class="col-sm-1">
                            <button class="btn btn-primary btn-sm" type="button" data-toggle="collapse" data-target="#collapseRef2" aria-expanded="false" aria-controls="collapseExample">
                                >>
                            </button>
                        </div>
                    </div>
                    <div class="collapse" id="collapseRef2">
                        <div class="form-group row">
                            <div class="col-sm-1"></div>
                            <div class="col-sm-4">
                                <div class="input-group mb-3">
                                    <div class="custom-file">
                                        <asp:FileUpload ID="fuRef2" runat="server" class="custom-file-input" />
                                        <label class="custom-file-label" for="inputGroupFile05">Choose file</label>
                                    </div>
                                    <div class="input-group-append">
                                        <asp:Button ID="btnupload2" runat="server" Text="Upload" OnClick="btnupload2_Click" class="input-group-text" />


                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-sm-2">
                                <asp:Image ID="refImage2" runat="server" Height="25px" Width="25px" ImageAlign="Baseline" ImageUrl="~/Images/Ok.png" Visible="False" />
                            </div>
                            <div class="col-sm-sm-2">
                                <asp:ImageButton ID="ImageButton2" runat="server" EnableTheming="False" Height="25px" Width="25px" ImageUrl="~/Images/Delete_icon.png" OnClick="ImageButton2_Click" />
                            </div>

                            <div class="col-sm-4">
                                <asp:Label ID="lblRef2" runat="server" Text="Label" Visible="true"></asp:Label>
                            </div>


                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-sm-1"></div>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtref3" runat="server" CssClass="form-control" placeholder="Enter third reference"></asp:TextBox>


                        </div>
                        <div class="col-sm-1">
                            <button class="btn btn-primary btn-sm" type="button" data-toggle="collapse" data-target="#collapseRef3" aria-expanded="false" aria-controls="collapseExample">
                                >>
                            </button>
                        </div>
                    </div>

                    <div class="collapse" id="collapseRef3">

                        <div class="form-group row">
                            <div class="col-sm-1"></div>
                            <div class="col-sm-4">
                                <div class="input-group mb-3">
                                    <div class="custom-file">
                                        <asp:FileUpload ID="fuRef3" runat="server" class="custom-file-input" />
                                        <label class="custom-file-label" for="inputGroupFile03">Choose file</label>
                                    </div>
                                    <div class="input-group-append">
                                        <asp:Button ID="btnupload3" runat="server" Text="Upload" OnClick="btnupload3_Click" class="input-group-text" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-sm-2">


                                <asp:Image ID="refImage3" runat="server" Height="25px" Width="25px"
                                    ImageAlign="Baseline" ImageUrl="~/Images/Ok.png" Visible="False" />
                            </div>
                            <div class="col-sm-sm-2">

                                <asp:ImageButton ID="ImageButton3" runat="server" EnableTheming="False" ImageUrl="~/Images/Delete_icon.png" OnClick="ImageButton3_Click" Height="25px" Width="25px" />
                            </div>

                            <div class="col-sm-2">
                                <asp:Label ID="lblRef3" runat="server" Text="Label" Visible="true"></asp:Label>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="form-group row">
                    <div class="col-sm-sm-5"></div>
                    <div class="col-sm-4">
                        <asp:Button ID="Button1" runat="server" Text="APPROVED" OnClick="Button1_Click" Visible="False" BackColor="#66FF33" />
                    </div>
                    <div class="col-sm-1">

                        <asp:Button ID="Button2" runat="server" Text="NOT APPROVED" OnClick="Button2_Click" Visible="False" BackColor="#FF3300" />
                    </div>
                    <div class="col-sm-3">
                        <asp:Button ID="Button3" runat="server" BackColor="#0099FF" OnClick="Button3_Click" Text="REC" Visible="False" />
                    </div>
                    <div class="col-sm-2">

                        <asp:Button ID="Button4" runat="server" BackColor="#FF3300" OnClick="Button4_Click" Text="NOT REC" Visible="False" />
                    </div>
                </div>

                <div class="form-group row">
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtbody" runat="server" Width="649px" Style="text-align: justify; margin-left: 16px"
                            TextMode="MultiLine" placeholder="Minute sheet body"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group row">
                    <div class="col-sm-2 text-dark">
                        <asp:Label ID="lblDate" runat="server"></asp:Label>
                    </div>
                    <div class="offset-8">
                        <asp:Image ID="imageSignature" runat="server" Height="60px" Width="117px"
                            BorderColor="#B0B0FF" BorderStyle="Solid"
                            Style="margin-top: 0px; text-align: right; margin-left: 0px; margin-bottom: 0px;" />
                    </div>
                </div>
                <br />
                <div class="form-group row">
                    <div class="col-sm-1">
                         <small>
                        <asp:Label ID="lblenclose" runat="server" CssClass="col-form-label text-left text-dark">Enclosure</asp:Label>
                                   </small> 
                    </div>
                    <div class="col-sm-8">
                       
                        <asp:TextBox ID="txtref4" runat="server" Class="form-control lbl" placeholder="Enter Enclose reference"></asp:TextBox>
                      
                    </div>
                    <div class="col-sm-1">
                        <button class="btn btn-primary btn-sm" type="button" data-toggle="collapse" data-target="#collapseRef4" aria-expanded="false" aria-controls="collapseExample">
                            >>
                        </button>
                    </div>
                </div>
                <div class="collapse" id="collapseRef4">

                    <div class="form-group row">

                        <div class="col-sm-1"></div>
                        <div class="col-sm-4">
                            <div class="input-group mb-3">
                                <div class="custom-file">
                                    <asp:FileUpload ID="fuRef4" runat="server" class="custom-file-input" />
                                    <label class="custom-file-label" for="inputGroupFile01">Choose file</label>
                                </div>


                        
                                <div class="input-group-append">
                                    <asp:Button ID="btnupload4" runat="server" Text="Upload" OnClick="btnupload4_Click" class="input-group-text" />
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <asp:Label ID="lblRef4" runat="server" Visible="False"></asp:Label>

                        </div>
                        <div class="col-sm-2">
                            <asp:Image ID="refImage4" runat="server" Height="25px" Width="25px"
                                ImageAlign="Baseline" ImageUrl="~/Images/Ok.png" Visible="False" />
                        </div>

                    </div>
                </div>
                <div class="form-group row alert alert-dismissible alert-warning">

                    <div class="col-sm-6">
                        <asp:Label ID="lblinfo" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="Red"
                            Style="font-weight: 700; font-size: medium;"></asp:Label>
                    </div>
                    <div class="col-sm-6">
                        <asp:CheckBox ID="chkOrder" runat="server"
                            OnCheckedChanged="Respond_CheckedChanged"
                            Text="Forward as per the order." Checked="True" />

                    </div>

                </div>

                <div class="form-group row alert alert-dismissible alert-primary">
                    <div class="col-6">
                    </div>
                    <div class="col-2">
                        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click"
                            Text="Save" class="form-control btn btn-primary" />
                    </div>
                    <div class="col-2">
                        <asp:Button ID="btnConfrim" runat="server" OnClick="btnConfrim_Click"
                            Text="Forward" class="form-control  btn btn-primary" />
                    </div>
                    <div class="col-2">
                        <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear"
                            class="form-control btn btn-primary" />
                    </div>

                </div>
                <div class="row alert alert-dismissible alert-secondary">
                    <asp:GridView ID="GVSubmit" runat="server" Height="16px" Width="1037px"
                        OnRowCommand="GVSubmit_RowCommand"
                        OnSelectedIndexChanged="GVSubmit_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
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















            </div>
        </div>
    </fieldset>

</asp:Content>
