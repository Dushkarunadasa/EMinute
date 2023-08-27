<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="Reply.aspx.cs" Inherits="M_sheet.Reply" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>
        <div class="form-group row">
            <div class="col-4  ">
                <h2>
                    Minute Sheet Reply</h2>
            </div>
            <div class="col-4">
            </div>
            <div class="col-1">
                <asp:Label ID="lblMinuteSheetNo" runat="server"></asp:Label>
            </div>
            <div class="col-sm-1">
                <label for="exampleSelect1">
                    Medium</label>
            </div>
            <div class="col-2">
                <asp:DropDownList ID="dpMedium" runat="server" OnSelectedIndexChanged="dpMedium_SelectedIndexChanged1"
                    AutoPostBack="True" CssClass="form-control">
                    <asp:ListItem>English</asp:ListItem>
                    <asp:ListItem>Sinhala</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="row border-bottom-info ">
            <div class="col-3">
                <asp:Label ID="Label1" runat="server" class="col-form-label text-dark "></asp:Label>
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
                <div class="form-group">
                    <asp:TextBox ID="lblTo" runat="server" class="form-control  text-dark"></asp:TextBox>
                </div>
                <br />
                <div class="form-group  ">
                    <asp:DropDownList ID="dpBase" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dpBase_SelectedIndexChanged"
                        OnTextChanged="dpBase_TextChanged" OnInit="dptype_Init" class="form-control ">
                    </asp:DropDownList>
                    <br />
                    <asp:DropDownList ID="dptype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dptype_SelectedIndexChanged"
                        OnTextChanged="dptype_TextChanged" OnInit="dptype_Init" class="form-control">
                        <asp:ListItem>----select----</asp:ListItem>
                        <asp:ListItem>Name</asp:ListItem>
                        <asp:ListItem>Appointment</asp:ListItem>
                        <asp:ListItem>Other Appointment</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:DropDownList ID="dltlist" runat="server" class="form-control">
                    </asp:DropDownList>
                    <br />
                    <asp:Button ID="btnAdd" runat="server" class="form-control" Text="Add" OnClick="btnAdd_Click" />
                    <br />
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDeleting="GridView1_RowDeleting"
                        OnSelectedIndexChanged="GridView1_SelectedIndexChanged2" class="form-control">
                        <Columns>
                            <asp:BoundField DataField="OfficialNo" HeaderText="SNO" />
                            <asp:BoundField DataField="Name" HeaderText="NAME" />
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="delete">Delete</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="col-9 text-center border-left-info ">
                <div class="text-center border-bottom-info">
                    <asp:TextBox ID="lblTopic" runat="server" class="form-control text-uppercase text-center"
                        ReadOnly="True"></asp:TextBox>
                </div>
                <br />
                <div class="text-center ">
                    <u>
                        <asp:Label ID="lblMSubId" runat="server" class="col-sm-2 col-form-label text-center text-dark">M:</asp:Label>
                    </u>
                </div>
                <div class="form-group row">
                    <asp:Label ID="lblMBy" runat="server" class="col-sm-1 col-form-label text-left text-dark">BY :</asp:Label>
                    <div class="col-sm-9">
                        <asp:DropDownList ID="dpBy" runat="server" class="form-control " OnSelectedIndexChanged="dpBy_SelectedIndexChanged"
                            Style="text-decoration: underline">
                        </asp:DropDownList>
                    </div>
                </div>
                <div id="accordion">
                    <div class="form-group row">
                        <asp:Label ID="lblMReference" runat="server" CssClass="col-sm-1 col-form-label text-left text-dark">Ref</asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtref1" runat="server" class="form-control" placeholder="Enter first reference"></asp:TextBox>
                        </div>
                        <div class="col-sm-1">
                            <button class="btn btn-primary btn-sm" type="button" data-toggle="collapse" data-target="#collapseRef1"
                                aria-expanded="false" aria-controls="collapseExample">
                                >></button>
                        </div>
                    </div>
                    <div class="collapse" id="collapseRef1">
                        <div class="form-group row">
                            <div class="col-sm-1">
                            </div>
                            <div class="col-sm-4">
                                <div class="input-group mb-3">
                                    <div class="custom-file">
                                        <asp:FileUpload ID="fuRef1" runat="server" class="custom-file-input" />
                                        <label class="custom-file-label" for="inputGroupFile02">
                                            Choose file</label>
                                    </div>
                                    <div class="input-group-append">
                                        <asp:Button ID="btnUpload1" runat="server" Text="Upload" OnClick="btnUpload1_Click"
                                            class="input-group-text" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-sm-2">
                                <asp:Image ID="refImage1" runat="server" Height="25px" Width="25px" ImageAlign="Baseline"
                                    ImageUrl="~/Images/ok.png" Visible="False" />
                            </div>
                            <div class="col-sm-sm-2">
                                <asp:ImageButton ID="ImageButton1" runat="server" EnableTheming="False" ImageUrl="~/Images/Delete_icon.png"
                                    OnClick="ImageButton1_Click" Height="35px" Width="35px" />
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
                            <button class="btn btn-primary btn-sm" type="button" data-toggle="collapse" data-target="#collapseRef2"
                                aria-expanded="false" aria-controls="collapseExample">
                                >>
                            </button>
                        </div>
                    </div>
                    <div class="collapse" id="collapseRef2">
                        <div class="form-group row">
                            <div class="col-sm-1">
                            </div>
                            <div class="col-sm-4">
                                <div class="input-group mb-3">
                                    <div class="custom-file">
                                        <asp:FileUpload ID="fuRef2" runat="server" class="custom-file-input" />
                                        <label class="custom-file-label" for="inputGroupFile05">
                                            Choose file</label>
                                    </div>
                                    <div class="input-group-append">
                                        <asp:Button ID="btnUpload2" runat="server" Text="Upload" OnClick="btnUpload2_Click"
                                            class="input-group-text" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-sm-2">
                                <asp:Image ID="refImage2" runat="server" Height="25px" Width="25px" ImageAlign="Baseline"
                                    ImageUrl="~/Images/Ok.png" Visible="False" />
                            </div>
                            <div class="col-sm-sm-2">
                                <asp:ImageButton ID="ImageButton2" runat="server" EnableTheming="False" Height="25px"
                                    Width="25px" ImageUrl="~/Images/Delete_icon.png" OnClick="ImageButton2_Click" />
                            </div>
                            <div class="col-sm-4">
                                <asp:Label ID="lblRef2" runat="server" Text="Label" Visible="true"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-sm-1">
                        </div>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtref3" runat="server" CssClass="form-control" placeholder="Enter third reference"></asp:TextBox>
                        </div>
                        <div class="col-sm-1">
                            <button class="btn btn-primary btn-sm" type="button" data-toggle="collapse" data-target="#collapseRef3"
                                aria-expanded="false" aria-controls="collapseExample">
                                >>
                            </button>
                        </div>
                    </div>
                    <div class="collapse" id="collapseRef3">
                        <div class="form-group row">
                            <div class="col-sm-1">
                            </div>
                            <div class="col-sm-4">
                                <div class="input-group mb-3">
                                    <div class="custom-file">
                                        <asp:FileUpload ID="fuRef3" runat="server" class="custom-file-input" />
                                        <label class="custom-file-label" for="inputGroupFile03">
                                            Choose file</label>
                                    </div>
                                    <div class="input-group-append">
                                        <asp:Button ID="btnUpload3" runat="server" Text="Upload" OnClick="btnUpload3_Click"
                                            class="input-group-text" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-sm-2">
                                <asp:Image ID="refImage3" runat="server" Height="25px" Width="25px" ImageAlign="Baseline"
                                    ImageUrl="~/Images/Ok.png" Visible="False" />
                            </div>
                            <div class="col-sm-sm-2">
                                <asp:ImageButton ID="ImageButton3" runat="server" EnableTheming="False" ImageUrl="~/Images/Delete_icon.png"
                                    OnClick="ImageButton3_Click" Height="25px" Width="25px" />
                            </div>
                            <div class="col-sm-2">
                                <asp:Label ID="lblRef3" runat="server" Text="Label" Visible="true"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col-sm-sm-5">
                    </div>
                    <div class="col-sm-4">
                        <asp:Button ID="Button1" runat="server" Text="APPROVED" OnClick="Button1_Click" Visible="False"
                            BackColor="#66FF33" class="form-control" />
                    </div>
                    <div class="col-sm-1">
                        <asp:Button ID="Button2" runat="server" Text="NOT APPROVED" OnClick="Button2_Click"
                            Visible="False" BackColor="#FF3300" class="form-control" />
                    </div>
                    <div class="col-sm-3">
                        <asp:Button ID="Button3" runat="server" BackColor="#0099FF" OnClick="Button3_Click"
                            Text="REC" Visible="False" />
                    </div>
                    <div class="col-sm-2">
                        <asp:Button ID="Button4" runat="server" BackColor="#FF3300" OnClick="Button4_Click"
                            Text="NOT REC" Visible="False" class="form-control" />
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col-sm-2">
                    </div>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtbody" runat="server" Width="576px" Style="text-align: justify;"
                            TextMode="MultiLine" placeholder="Minute sheet body" class="form-control"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="form-group row">
                    <div class="col-sm-2 text-dark">
                        <asp:Label ID="lblDate" runat="server"></asp:Label>
                    </div>
                    <div class="offset-8">
                        <asp:Image ID="imageSignature" runat="server" Height="60px" Width="117px" BorderColor="#B0B0FF"
                            BorderStyle="Solid" Style="margin-top: 0px; text-align: right; margin-left: 0px;
                            margin-bottom: 4px;" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-8">
            </div>
            <div class="col-2">
                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Save" class="form-control btn btn-primary btn-sm" />
            </div>
            <div class="col-2">
                <asp:Button ID="btnClose" runat="server" OnClick="btnclose_Click" Text="Close" class="form-control btn btn-primary btn-sm" />
            </div>
        </div>
        <div class="row">
            <div class="col-1">
            </div>
            <div class="col-11">
                <asp:Label ID="lblinfo" runat="server" ForeColor="#CC0000"></asp:Label>
            </div>
        </div>
    </fieldset>
</asp:Content>
