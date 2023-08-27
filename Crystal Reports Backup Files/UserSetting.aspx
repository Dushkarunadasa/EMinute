<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="UserSetting.aspx.cs" Inherits="M_sheet.UserSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <fieldset>
        <div class="form-group">
            <div class="row">
                <h5>User Account details ...   </h5>
            </div>
        </div>
        <div class="row">
            <div class="col-6">
                <div class="row">
                    <div class="col-6">
                        <asp:Label ID="Label2" runat="server" Font-Bold="True"
                            Text="Official No"></asp:Label>
                    </div>
                    <div class="col-6">
                        <asp:TextBox ID="txtid" runat="server" TextMode="SingleLine" class="form-control" placeholder="Default input" OnTextChanged="txtid_TextChanged"
                            MaxLength="7" ReadOnly="True"></asp:TextBox>


                    </div>

                </div>
                <br />
                <div class="row">
                    <div class="col-6">
                        <asp:Label ID="Label17" runat="server" Font-Bold="True"
                            Text="Branch"></asp:Label>
                    </div>
                    <div class="col-6">

                        <asp:TextBox ID="txtbranch" runat="server" TextMode="SingleLine" class="form-control" placeholder="Default input"
                            MaxLength="7" ReadOnly="True"></asp:TextBox>

                    </div>

                </div>
                <br />
                <div class="row">
                    <div class="col-6">
                        <asp:Label ID="Label1" runat="server" Font-Bold="True"
                            Text="Name With Initial :"></asp:Label>
                    </div>
                    <div class="col-6">

                        <asp:TextBox ID="txtName" runat="server" TextMode="SingleLine" class="form-control" placeholder="Default input"
                            MaxLength="50" ReadOnly="True"></asp:TextBox>

                    </div>

                </div>
                <br />
                <div class="row">
                    <div class="col-6">
                        <asp:Label ID="Label18" runat="server" Font-Bold="True"
                            Text="Name With Initial (Sinhala):"></asp:Label>
                    </div>
                    <div class="col-6">

                        <asp:TextBox ID="txtNameSin" runat="server" TextMode="SingleLine" class="form-control" placeholder="Default input"
                            MaxLength="50"></asp:TextBox>
                    </div>

                </div>
                <br />
                <div class="row">
                    <div class="col-6">
                        <asp:Label ID="Label3" runat="server" Font-Bold="True" Text="Rank :"></asp:Label>



                    </div>
                    <div class="col-6">

                        <asp:TextBox ID="txtRank" runat="server" TextMode="SingleLine" class="form-control" placeholder="Default input"
                            MaxLength="50" ReadOnly="True"></asp:TextBox>
                    </div>

                </div>
                <br />
                <div class="row">
                    <div class="col-6">
                        <asp:Label ID="Label19" runat="server" Font-Bold="True" Text="Rank (Sinhala):"></asp:Label>

                    </div>
                    <div class="col-6">

                        <asp:TextBox ID="txtRankSin" runat="server" TextMode="SingleLine" class="form-control" placeholder="Default input"
                            MaxLength="50"></asp:TextBox>
                    </div>

                </div>
                <br />
                <div class="row">
                    <div class="col-6">
                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Appointment :"></asp:Label>

                    </div>
                    <div class="col-6">

                        <asp:TextBox ID="txtAppointment" runat="server" TextMode="SingleLine" class="form-control" placeholder="Default input"
                            OnTextChanged="txtAppointment_TextChanged" MaxLength="50" ReadOnly="True"></asp:TextBox>
                    </div>

                </div>

                <br />
                <div class="row">
                    <div class="col-6">
                        <asp:Label ID="Label15" runat="server" Font-Bold="True"
                            Text="Appointment (Sinhala):"></asp:Label>


                    </div>
                    <div class="col-6">

                        <asp:TextBox ID="txtAppointmentSin" runat="server" TextMode="SingleLine" class="form-control" placeholder="Default input"
                            OnTextChanged="txtAppointment_TextChanged" MaxLength="50"></asp:TextBox>

                    </div>

                </div>
                <br />
                <div class="row">
                    <div class="col-6">
                        <asp:Label ID="Label16" runat="server" Font-Bold="True"
                            Text="Other Appointment :"></asp:Label>


                    </div>
                    <div class="col-6">

                        <asp:TextBox ID="txtOtherAppointment" runat="server" TextMode="SingleLine" class="form-control" placeholder="Default input"
                            MaxLength="50"></asp:TextBox>


                    </div>

                </div>
                <br />
                <div class="row">
                    <div class="col-6">
                        <asp:Label ID="Label13" runat="server" Font-Bold="True"
                            Text="Other Appointment (Sinhala):"></asp:Label>


                    </div>
                    <div class="col-6">

                        <asp:TextBox ID="txtOtherAppointmentSin" runat="server" TextMode="SingleLine" class="form-control" placeholder="Default input" OnTextChanged="txtAppointment_TextChanged"
                            MaxLength="50"></asp:TextBox>
                    </div>

                </div>
                <br />
                <div class="row">
                    <div class="col-6">
                        <asp:Label ID="Label5" runat="server" Font-Bold="True" Text="Area :"></asp:Label>



                    </div>
                    <div class="col-6">

                        <asp:TextBox ID="txtArea" runat="server" TextMode="SingleLine" class="form-control" placeholder="Default input"
                            MaxLength="50" ReadOnly="True"></asp:TextBox>


                    </div>

                </div>
                <br />
                <div class="row">
                    <div class="col-6">
                        <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="Base / Ship :"></asp:Label>


                    </div>
                    <div class="col-6">

                        <asp:TextBox ID="txtBase" runat="server" TextMode="SingleLine" class="form-control" placeholder="Default input"
                            MaxLength="50" ReadOnly="True"></asp:TextBox>



                    </div>

                </div>
                <br />
                <div class="row">
                    <div class="col-6">
                        <asp:Label ID="Label7" runat="server" Font-Bold="True" Text="Department :"></asp:Label>


                    </div>
                    <div class="col-6">

                        <asp:DropDownList ID="txtDepartment" class="form-control" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-6">
                    </div>
                    <div class="col-6">
                        <asp:Label ID="lblimageurl" runat="server" Text="Label" Visible="False"></asp:Label>
                    </div>

                </div>
                <br />
            </div>
            <div class="col-6">
                <div class="row">
                    <div class="col-6">
                        <asp:Label ID="lblinfo0" runat="server" Font-Bold="True" Font-Names="Arial"
                            Font-Size="Medium" ForeColor="#FF5050"
                            Style="text-decoration: underline; color: #000099">SPECIAL FEATURES</asp:Label>
                    </div>
                    <div class="col-6">
                    </div>
                </div>

                <div class="row">
                    <div class="col-6">
                        <asp:Label ID="Label8" runat="server" class="col-2 col-form-label" Text="Name" Visible="false"></asp:Label>
                        <div class="col-3">
                        </div>
                        <div class="col-6">
                        </div>
                    </div>
                </div>
                <br />
                <div class="alert alert-dismissible alert-danger">

                    <div class="row">

                        <div class="col-10">

                            <asp:CheckBox ID="chkleave" runat="server"
                                Text="I am away from the base    " TextAlign="Left"
                                AutoPostBack="true" OnCheckedChanged="chkleave_CheckedChanged" class="form-check disabled" />

                        </div>
                        <div class="col-2">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-6">
                            <asp:TextBox ID="txtsearch0" runat="server" class="form-control" placeholder="Official No" MaxLength="4"></asp:TextBox>
                        </div>
                        <div class="col-6">
                            <asp:Button ID="btnSearch0" runat="server" Text="Search"
                                OnClick="btnSearch0_Click" Width="100px" class="btn btn-primary" Visible="false" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-10">
                            <asp:Label ID="Label20" runat="server"></asp:Label>


                        </div>
                        <div class="col-6">
                            <asp:Image ID="refImage1" runat="server" Height="25px" Width="25px"
                                ImageAlign="Baseline" ImageUrl="~/Images/ok.png" Visible="False" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-6">
                            <asp:Label ID="lblgrank0" runat="server" Text="Rank" ForeColor="#006600"></asp:Label>

                        </div>
                        <div class="col-6">
                            <asp:Label ID="lblgrantedOfficer0" runat="server" Text="Name with initial"
                                ForeColor="#006600"></asp:Label>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-6">
                            <asp:Label ID="lblgAppointment0" runat="server" Text="Appointment"
                                ForeColor="#006600"></asp:Label>

                        </div>
                        <div class="col-6">
                            <asp:Label ID="lblgOfficialNo0" runat="server" Text="Official No"
                                ForeColor="#006600"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-6">
                        <asp:CheckBox ID="chkstaff" runat="server" ForeColor="#000099"
                            AutoPostBack="true" Text="Allow  to staff" TextAlign="Left"
                            OnCheckedChanged="chkstaff_CheckedChanged" Visible="False" />
                    </div>
                    <div class="col-6">
                    </div>
                </div>
                <div class="row">
                    <div class="col-6">
                        <asp:DropDownList ID="dpstaff" runat="server" Height="27px" Width="327px"
                            Visible="False">
                        </asp:DropDownList>
                    </div>
                    <div class="col-6">
                    </div>
                </div>

                <div class="row">
                    <div class="col-6">
                        <asp:CheckBox ID="chknote" runat="server" ForeColor="#000099"
                            AutoPostBack="true" Text="Allow to Officer" TextAlign="Left"
                            OnCheckedChanged="chknote_CheckedChanged" Visible="False" />
                    </div>
                    <div class="col-6">
                    </div>

                </div>
            </div>
            <div class="row">
                <div class="col-6">
                    <asp:TextBox ID="txtsearch" runat="server"></asp:TextBox>

                </div>
                <div class="col-6">
                    <asp:Button ID="btnSearch" runat="server" Text="Search"
                        OnClick="btnSearch_Click" />
                </div>

            </div>
            <div class="row">
                <div class="col-6">
                    <asp:Label ID="lblgAppointment" runat="server" Text="Appointment"
                        ForeColor="#006600"></asp:Label>

                </div>
                <div class="col-6">
                    <asp:Label ID="lblgOfficialNo" runat="server" Text="Official No"
                        ForeColor="#006600"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-6">
                    <asp:Label ID="lblgrank" runat="server" Text="Rank" ForeColor="#006600"></asp:Label>
                </div>
                <div class="col-6">
                    <asp:Label ID="lblgrantedOfficer" runat="server" Text="Name with initial"
                        ForeColor="#006600"></asp:Label>
                </div>
            </div>

        </div>
        <div class="row">
            <asp:Label ID="lblinfo" runat="server" Font-Bold="True" Font-Names="Arial"
                Font-Size="Medium" ForeColor="#FF5050"></asp:Label>
        </div>
        <div class="row">
            <div class="col-5">
            </div>
            <div class="col-7">

                <div class="col-2">
                </div>
                <div class="col-5">
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" class="btn btn-primary"
                        Width="150px" OnClick="btnSubmit_Click" />
                </div>
            </div>



        </div>



    </fieldset>
</asp:Content>
