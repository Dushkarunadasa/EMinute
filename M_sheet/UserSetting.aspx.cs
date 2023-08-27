using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections;
using System.Web.SessionState;


namespace M_sheet
{
    public partial class UserSetting : System.Web.UI.Page
    {
        string myConnectionString = ConfigurationManager.ConnectionStrings["M_SConnection"].ConnectionString;

        private string userid = "";
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //try

            //{
            lblinfo.Text = "";
            if ((this.txtRankSin.Text.Trim()) == "")
            {
                lblinfo.ForeColor = System.Drawing.Color.Red;
                lblinfo.Text = "Required Rank in Sinhala Medium";
                lblinfo.Visible = true;
                return;
            }
            if (this.txtNameSin.Text.Trim() == "")
            {
                lblinfo.ForeColor = System.Drawing.Color.Red;
                lblinfo.Text = "Required Name with initial in sinhala medium.";
                lblinfo.Visible = true;
                return;
            }
            if ((this.txtAppointmentSin.Text.Trim()) == "")
            {
                lblinfo.ForeColor = System.Drawing.Color.Red;
                lblinfo.Text = "Required appointment in sinhala medium";
                lblinfo.Visible = true;
                return;
            }

            int onleave = 0;


            if (chkleave.Checked)
            {
                if (lblgOfficialNo0.Text.Trim() == "Official No")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select the oversees officer.');", true);
                    return;
                }


                SqlConnection newcon = new SqlConnection();
                newcon.ConnectionString = myConnectionString;
                newcon.Open();
                string sqlstr = "SELECT [Min_ID] ,[User_ID] ,[User_Name]  FROM [dbo].[Receivers] where [Read_Not]=0 and [MConfirm]=1 and [RespondAllow]=1 and [User_Name]='" + userid + "'  ";
                SqlCommand Newcmd = new SqlCommand(sqlstr, newcon);
                SqlDataReader reader = Newcmd.ExecuteReader();
                while (reader.Read())
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You cannnot active [I am on leave] until clear your Minute Sheet In');", true);
                    return;

                }
                newcon.Close();
                newcon.Dispose();
                Newcmd.Dispose();
                reader.Dispose();

                onleave = 1;

            }
            if (lblgOfficialNo0.Text.Trim() == "Official No")
            {
                lblgOfficialNo0.Text = "";
            }


            //string oldPath = @"C:\inetpub\wwwroot\ems\LeaveSys\Signatures\" + txtid.Text.Trim() + ".jpg";
            //string newPath = @"C:\inetpub\wwwroot\ems\Signatures\" + txtid.Text.Trim() + ".jpg";
           // string oldPath = @"E:\IIS_HOSTED_SYS\003_ems.navy.lk\LeaveSys\Signatures\" + txtid.Text.Trim() + ".jpg";
            //string newPath = @"E:\IIS_HOSTED_SYS\003_ems.navy.lk\Signatures\" + txtid.Text.Trim() + ".jpg";
            //string oldPath = @"C:\Users\122\Desktop\MS_SYS_11_2_2019_FOR_DAKSHINA\LeaveSys\Signatures\" + txtid.Text.Trim() + ".jpg";
            // string newPath = @"C:\Users\122\Desktop\MS_SYS_11_2_2019_FOR_DAKSHINA\Signatures\" + txtid.Text.Trim() + ".jpg";

            string oldPath = Server.MapPath("~/LeaveSys/Signatures/" + txtid.Text.Trim() + ".jpg");


            string newPath = Server.MapPath("~/Signatures/" + txtid.Text.Trim() + ".jpg");


            if (File.Exists(oldPath) == true)
            {
                File.Delete(newPath);
            }


            if (File.Exists(newPath) == true)
            {
                File.Delete(newPath);
            }
            File.Copy(oldPath, newPath, true);
            newPath = "~/Signatures/" + txtid.Text.Trim() + ".jpg";

            byte[] imageBytes = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(newPath));



            SqlConnection tcon = new SqlConnection();
            tcon.ConnectionString = myConnectionString;
            tcon.Open();


            SqlCommand tcmd = new SqlCommand("Usp_mypicsstore", tcon);

            tcmd.CommandType = CommandType.StoredProcedure;

            tcmd.Parameters.AddWithValue("@User_ID", txtbranch.Text.Trim().ToUpper() + txtid.Text.Trim()); // storing name of image
            tcmd.Parameters.AddWithValue("@ImageName", txtid.Text.Trim() + ".jpg"); // storing name of image
            tcmd.Parameters.AddWithValue("@Image", imageBytes); // bytes
            tcmd.Parameters.AddWithValue("@ImageSize", 20000); // getting file size

            int returnvalue = tcmd.ExecuteNonQuery();

            tcon.Close();

            tcmd.Dispose();




            //if (File.Exists(newPath) == true)
            //{
            //    File.Delete(newPath);
            //}
            //SaveACopyfileToServercls fcopy = new SaveACopyfileToServercls();
            //fcopy.SaveACopyfileToServer(oldPath, newPath);
            //fcopy.Transfer(oldPath, newPath);




            // File.Copy(oldPath, newPath,true);
            // newPath="~/Signatures/"+txtid.Text.Trim() + ".jpg";

            using (SqlConnection ncon = new SqlConnection(ConfigurationManager.ConnectionStrings["M_SConnection"].ConnectionString))
            {
                using (SqlCommand gcmd = new SqlCommand("SP_UPDATE_REGISTER", ncon))
                {
                    gcmd.CommandType = CommandType.StoredProcedure;
                    gcmd.Parameters.AddWithValue("@Official_No", SqlDbType.NVarChar).Value = txtid.Text.Trim();
                    gcmd.Parameters.AddWithValue("@Branch", SqlDbType.NVarChar).Value = txtbranch.Text.Trim();
                    gcmd.Parameters.AddWithValue("@Officer_Id", SqlDbType.NChar).Value = txtbranch.Text.Trim().ToUpper() + txtid.Text.Trim();

                    ncon.Open();
                    gcmd.ExecuteNonQuery();
                    ncon.Close();
                }
            }


            string strupdate = "";
            SqlConnection con = new SqlConnection(myConnectionString);



            //strupdate = "Update  dbo.Profile_Master  set Name_With_Initial=@Name_With_Initial,Rank=@Rank,Appointment=@Appointment,Area=@Area,Ship_Establishment=@Ship_Establishment,Department=@Department,[Branch]=@Branch,[imagePath]=@imagePath,[Other_Appointment]=@Other_Appointment,[Onleave]=@Onleave,[OverSeesBy] =@OverSeesBy,[AppointmentSin]=@AppointmentSin,[Other_AppointmentSin]=@Other_AppointmentSin,[RankSin]=@RankSin,[Name_With_Initial_Sin]=@Name_With_Initial_Sin   where Officer_Id='" + userid + "'"; //This is original code with area update
            strupdate = "Update  dbo.Profile_Master  set Name_With_Initial=@Name_With_Initial,Rank=@Rank,Appointment=@Appointment,Ship_Establishment=@Ship_Establishment,Department=@Department,[Branch]=@Branch,[imagePath]=@imagePath,[Other_Appointment]=@Other_Appointment,[Onleave]=@Onleave,[OverSeesBy] =@OverSeesBy,[AppointmentSin]=@AppointmentSin,[Other_AppointmentSin]=@Other_AppointmentSin,[RankSin]=@RankSin,[Name_With_Initial_Sin]=@Name_With_Initial_Sin  ,Area=@Area where Officer_Id='" + userid + "'";

            SqlCommand cmd = new SqlCommand(strupdate, con);
            con.Open();
            cmd.Parameters.AddWithValue("@Name_With_Initial", txtName.Text.Trim().ToUpper());
            cmd.Parameters.AddWithValue("@Rank", txtRank.Text.Trim().ToUpper());
            cmd.Parameters.AddWithValue("@Appointment", txtAppointment.Text.Trim().ToUpper());
            cmd.Parameters.AddWithValue("@Area", txtArea.Text.Trim());
            cmd.Parameters.AddWithValue("@Ship_Establishment", txtBase.Text.Trim());
            cmd.Parameters.AddWithValue("@Department", txtDepartment.Text.Trim());
            cmd.Parameters.AddWithValue("@Branch", txtbranch.Text.Trim());
            cmd.Parameters.AddWithValue("@imagePath", newPath);
            cmd.Parameters.AddWithValue("@Other_Appointment", txtOtherAppointment.Text.Trim());
            cmd.Parameters.AddWithValue("@Onleave", onleave);

            cmd.Parameters.AddWithValue("@OverSeesBy", lblgOfficialNo0.Text.Trim());


            cmd.Parameters.AddWithValue("@AppointmentSin", txtAppointmentSin.Text.Trim());
            cmd.Parameters.AddWithValue("@Other_AppointmentSin", txtOtherAppointmentSin.Text.Trim());
            cmd.Parameters.AddWithValue("@RankSin", txtRankSin.Text.Trim());
            cmd.Parameters.AddWithValue("@Name_With_Initial_Sin", txtNameSin.Text.Trim());


            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            cmd.Dispose();
            if (chkstaff.Checked)
            {
                SqlConnection newcon = new SqlConnection();
                newcon.ConnectionString = myConnectionString;
                newcon.Open();
                string sqlstr = "SELECT [UserId]  ,[Staff] ,[Activate] ,[ActivatedDate]  FROM [dbo].[AllowStaff] where [UserId]='" + userid + "'  ";
                SqlCommand Newcmd = new SqlCommand(sqlstr, newcon);
                SqlDataReader reader = Newcmd.ExecuteReader();
                while (reader.Read())
                {
                    updateStaff();
                    goto jumphere;
                }

                SqlConnection NewCon = new SqlConnection();
                NewCon.ConnectionString = myConnectionString;
                NewCon.Open();
                string sqlstring = "INSERT INTO [dbo].[AllowStaff]  ([UserId],[Staff],[Activate] ,[ActivatedDate])  values('" + userid.ToString().Trim() + "','" + dpstaff.Text.Trim() + "',1,CONVERT(date, GETDATE()))";
                SqlCommand newCmd = new SqlCommand(sqlstring, NewCon);
                newCmd.ExecuteNonQuery();
                NewCon.Close();
                NewCon.Dispose();
                newCmd.Dispose();


            }
            else
            {
                updateStaff();
            }
        jumphere:




            if (chknote.Checked)
            {
                if ((this.lblgOfficialNo.Text.Trim() != "") & (this.lblgOfficialNo.Text.Trim() != "Official No"))
                {
                    SqlConnection newcon = new SqlConnection();
                    newcon.ConnectionString = myConnectionString;
                    newcon.Open();
                    string sqlstr = "SELECT  [UserId]       ,[AuthorizedUserID]      ,[AuthorizedDt]      ,[Active]  FROM [dbo].[Authorized_Users] where [UserId] ='" + userid.ToString().Trim() + "'  ";
                    SqlCommand Newcmd = new SqlCommand(sqlstr, newcon);
                    SqlDataReader reader = Newcmd.ExecuteReader();
                    while (reader.Read())
                    {
                        updateAuthorizedOfficer();
                        goto jump;
                    }

                    SqlConnection NewCon = new SqlConnection();
                    NewCon.ConnectionString = myConnectionString;
                    NewCon.Open();
                    string sqlstring = "INSERT INTO [dbo].[Authorized_Users]  ([UserId],[AuthorizedUserID],[Active] ,[AuthorizedDt])  values('" + userid.ToString().Trim() + "','" + lblgOfficialNo.Text.Trim() + "',1,CONVERT(date, GETDATE()))";
                    SqlCommand newCmd = new SqlCommand(sqlstring, NewCon);
                    newCmd.ExecuteNonQuery();
                    NewCon.Close();
                    NewCon.Dispose();
                    newCmd.Dispose();
                }
                else
                {
                    lblinfo.Text = "Please untick if you want deactivate Authorized User facility.";
                }

            }
            else
            {
                updateAuthorizedOfficer();
            }
        jump:
            lblinfo.ForeColor = System.Drawing.Color.Green;
            lblinfo.Text = "Your details has been updated";
            lblinfo.Visible = true;






            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Registration details has been updated.');", true);
            //}
            //catch (NullReferenceException ex)
            //{
            //    Response.Redirect("LoginPage.aspx");
            //}
        }

        private void updateStaff()
        {

            string sqlstring;
            if (chkstaff.Checked)
            {
                sqlstring = "Update [dbo].[AllowStaff] set [Staff] ='" + dpstaff.Text.Trim() + "' ,[Activate] =1 ,[ActivatedDate]=CONVERT(date, GETDATE()) where [UserId] =  '" + userid.Trim() + "' ";

            }
            else
            {
                sqlstring = "Update [dbo].[AllowStaff] set [Staff] ='" + dpstaff.Text.Trim() + "' ,[Activate] =0,[ActivatedDate]=CONVERT(date, GETDATE()) where [UserId] =  '" + userid.Trim() + "'";

            }
            SqlConnection NewCon = new SqlConnection();
            NewCon.ConnectionString = myConnectionString;
            NewCon.Open();
            SqlCommand newCmd = new SqlCommand(sqlstring, NewCon);
            newCmd.ExecuteNonQuery();
            NewCon.Close();
            NewCon.Dispose();
            newCmd.Dispose();
        }

        private void updateAuthorizedOfficer()
        {

            string sqlstring = "";
            if (chknote.Checked)
            {
                sqlstring = "Update [dbo].[Authorized_Users] set [AuthorizedUserID] ='" + lblgOfficialNo.Text.Trim() + "',[Active]  =1 ,[AuthorizedDt]=CONVERT(date, GETDATE()) where [UserId] =  '" + userid.Trim() + "'";
            }
            else
            {
                SqlConnection newcon = new SqlConnection();
                newcon.ConnectionString = myConnectionString;
                newcon.Open();
                string sqlstr = "SELECT  [UserId]       ,[AuthorizedUserID]      ,[AuthorizedDt]      ,[Active]  FROM [dbo].[Authorized_Users] where [UserId] ='" + userid.Trim() + "' and [AuthorizedUserID]='" + lblgOfficialNo.Text.Trim() + "' ";
                SqlCommand Newcmd = new SqlCommand(sqlstr, newcon);
                SqlDataReader reader = Newcmd.ExecuteReader();
                while (reader.Read())
                {
                    sqlstring = "Update [dbo].[Authorized_Users] set [AuthorizedUserID] ='" + lblgOfficialNo.Text.Trim() + "' ,[Active]  =0 ,[AuthorizedDt]=CONVERT(date, GETDATE()) where [UserId] =  '" + userid.Trim() + "'";
                }
                newcon.Close();
                newcon.Dispose();
                newcon.Dispose();
            }
            SqlConnection NewCon = new SqlConnection();
            NewCon.ConnectionString = myConnectionString;
            NewCon.Open();
            if (sqlstring.Trim().Equals(""))
            {
                return;
            }
            SqlCommand newCmd = new SqlCommand(sqlstring, NewCon);
            newCmd.ExecuteNonQuery();
            NewCon.Close();
            NewCon.Dispose();
            newCmd.Dispose();


        }
        protected void txtBase_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ddListRank_SelectedIndexChanged(object sender, EventArgs e)
        {

        }





        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
            if (Request.Cookies["LocalComputer"] != null)
            {
                HttpCookie _cookie = Request.Cookies["LocalComputer"];
                userid = _cookie["OfficialNo"].ToString().Trim();

            }
            lblinfo.Visible = false;
            if (!IsPostBack)
            {

                string selectSQL = "  select * FROM [M_Sheet].[dbo].[Profile_Master] where [Officer_Id]='" + userid + "' ";
                SqlConnection con = new SqlConnection(myConnectionString);
                SqlCommand cmd = new SqlCommand(selectSQL, con);
                SqlDataReader reader;

                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    LoadHRMS(reader["Official_No"].ToString(), reader["Service_Type"].ToString());
                    if (txtOtherAppointment.Text.Trim() != "")
                    {
                        txtOtherAppointment.Text = reader["Other_Appointment"].ToString();
                    }
                    txtid.Text = reader["Official_No"].ToString();
                    txtAppointmentSin.Text = reader["AppointmentSin"].ToString();
                    if (txtAppointmentSin.Text == "")
                    {
                        txtAppointmentSin.Text = ".";
                    }

                    txtOtherAppointmentSin.Text = reader["Other_AppointmentSin"].ToString();
                    txtRankSin.Text = reader["RankSin"].ToString();

                    if (txtRankSin.Text == "")
                    {
                        txtRankSin.Text = ".";
                    }
                    txtNameSin.Text = reader["Name_With_Initial_Sin"].ToString();

                    if (txtNameSin.Text == "")
                    {
                        txtNameSin.Text = ".";
                    }

                    DataTable dt = new DataTable();
                    dt = loadDepartment(txtArea.Text.Trim());
                    txtDepartment.DataSource = dt;

                    txtDepartment.DataTextField = "departmentName";
                    txtDepartment.DataValueField = "departmentName";
                    txtDepartment.DataBind();
                    txtDepartment.Enabled = true;
                    txtDepartment.Items.Insert(0, new ListItem("-----Please select-----", "0"));


                    lblinfo.Text = "";
                    txtDepartment.Focus();


                    txtDepartment.Text = reader["Department"].ToString();

                    if (reader["Onleave"].Equals(true)) { chkleave.Checked = true; }
                    else { chkleave.Checked = false; }

                }

                reader.Close();
                reader.Dispose();
                con.Close();
                con.Dispose();
                cmd.Dispose();

                dpstaff.Visible = false;
                txtsearch.Visible = false;
                btnSearch.Visible = false;
                refImage1.Visible = false;
                txtsearch0.Visible = false;
                btnSearch0.Visible = false;
                lblgOfficialNo0.Visible = false;
                lblgAppointment0.Visible = false;
                lblgrantedOfficer0.Visible = false;
                lblgrank0.Visible = false;
                lblgAppointment.Visible = false;
                lblgOfficialNo.Visible = false;
                lblgAppointment.Visible = false;
                lblgrank.Visible = false;
                lblgrantedOfficer.Visible = false;



                dpstaff.Items.Clear();
                selectSQL = "select StaffName from dbo.staff Order by StaffName Asc";
                con = new SqlConnection(myConnectionString);
                cmd = new SqlCommand(selectSQL, con);
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    ListItem newItem = new ListItem();
                    newItem.Text = reader["StaffName"].ToString().ToUpper();
                    dpstaff.Items.Add(newItem);

                }
                reader.Close();
                reader.Dispose();
                con.Close();
                con.Dispose();
                cmd.Dispose();
                chkstaff.Checked = false;
                selectSQL = "SELECT [UserId]  ,[Staff] ,[Activate] ,[ActivatedDate]  FROM [dbo].[AllowStaff] where [UserId]='" + userid + "'";
                con = new SqlConnection(myConnectionString);
                cmd = new SqlCommand(selectSQL, con);
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["Activate"].Equals(true))
                    {
                        chkstaff.Checked = true;
                        dpstaff.Visible = true;
                    }

                    dpstaff.Text = reader["Staff"].ToString().Trim();

                }
                reader.Close();
                reader.Dispose();
                con.Close();
                con.Dispose();
                cmd.Dispose();
                chknote.Checked = false;
                selectSQL = @"SELECT        dbo.Profile_Master.Name_With_Initial, dbo.Profile_Master.Rank, dbo.Profile_Master.Appointment , dbo.Authorized_Users.AuthorizedUserID
                                FROM            dbo.Authorized_Users INNER JOIN       dbo.Profile_Master ON dbo.Authorized_Users.AuthorizedUserID = dbo.Profile_Master.Official_No
                                WHERE        (dbo.Authorized_Users.UserId = '" + userid + "') AND (dbo.Authorized_Users.Active = 1) ";


                con = new SqlConnection(myConnectionString);
                cmd = new SqlCommand(selectSQL, con);
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    chknote.Checked = true;
                    lblgAppointment.Visible = true;
                    lblgOfficialNo.Visible = true;
                    lblgAppointment.Visible = true;
                    lblgrank.Visible = true;
                    lblgrantedOfficer.Visible = true;
                    txtsearch.Visible = true;
                    btnSearch.Visible = true;
                    lblgOfficialNo.Text = reader[3].ToString().Trim();
                    lblgrank.Text = reader[1].ToString().Trim();
                    lblgAppointment.Text = reader[2].ToString().Trim();
                    lblgrantedOfficer.Text = reader[0].ToString().Trim();



                }
                reader.Close();
                reader.Dispose();
                con.Close();
                con.Dispose();
                cmd.Dispose();



            }

            //}
            //catch (Exception ex)
            //{
            //    Response.Redirect("LoginPage.aspx");
            //}
        }
        private DataTable loadDepartment(string AreaName)
        {


            SqlConnection con11 = new SqlConnection(ConfigurationManager.ConnectionStrings["M_SConnection"].ToString());
            con11.Open();
            SqlCommand cmm = new SqlCommand();
            cmm.Connection = con11;
            cmm.Parameters.AddWithValue("@AreaName", AreaName);

            cmm.CommandType = CommandType.StoredProcedure;
            cmm.CommandText = "getDepartments";

            SqlDataReader drr;
            drr = cmm.ExecuteReader();
            DataTable person = new DataTable();
            drr.Close();

            person.Load(cmm.ExecuteReader());

            con11.Close();
            drr.Close();

            return person;
        }
        public SqlConnection conn { get; set; }
        public string connstring { get; set; }
        public string sql { get; set; }
        public string cons { get; set; }

        public int updated { get; set; }

        public string uid { get; set; }

        protected void txtAppointment_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            lblgrank.Text = "";
            lblgrantedOfficer.Text = "";
            lblgAppointment.Text = "";
            lblgOfficialNo.Text = "";
            SqlConnection con = new SqlConnection(myConnectionString);
            String selectSQL = @"SELECT          dbo.Authorized_Users.UserId, dbo.Authorized_Users.AuthorizedUserID, dbo.Authorized_Users.AuthorizedDt
FROM            dbo.Authorized_Users INNER JOIN
                         dbo.Profile_Master ON dbo.Authorized_Users.AuthorizedUserID = dbo.Profile_Master.Officer_Id
WHERE        (dbo.Authorized_Users.Active = 1) and dbo.Profile_Master.Official_No='" + txtsearch.Text.Trim() + "'";
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            con.Open();
            string authorizedBy = "";

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                authorizedBy = reader[2].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Selected officer is already added as Authorized officer by " + reader[0].ToString() + " on " + authorizedBy + "  .');", true);
                return;
            }

            reader.Close();
            reader.Dispose();
            con.Close();
            con.Dispose();
            cmd.Dispose();
            con = new SqlConnection();
            selectSQL = @" SELECT        dbo.Profile_Master.Name_With_Initial, dbo.Profile_Master.Rank, dbo.Profile_Master.Appointment, dbo.Profile_Master.Officer_Id
    FROM    dbo.Profile_Master WHERE        (dbo.Profile_Master.Official_No = '" + txtsearch.Text.Trim() + "')";

            con = new SqlConnection(myConnectionString);
            cmd = new SqlCommand(selectSQL, con);
            con.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lblgrank.Text = reader[1].ToString();
                lblgrantedOfficer.Text = reader[0].ToString();
                lblgAppointment.Text = reader[2].ToString();
                lblgOfficialNo.Text = reader[3].ToString();


            }
            reader.Close();
            reader.Dispose();
            con.Close();
            con.Dispose();
            cmd.Dispose();
        }

        protected void chkstaff_CheckedChanged(object sender, EventArgs e)
        {
            if (chkstaff.Checked)
            {
                dpstaff.Visible = true;
            }
            else
            {
                dpstaff.Visible = false;
            }
        }

        protected void chknote_CheckedChanged(object sender, EventArgs e)
        {
            if (chknote.Checked)
            {
                txtsearch.Visible = true;
                btnSearch.Visible = true;
                lblgAppointment.Visible = true;
                lblgOfficialNo.Visible = true;
                lblgAppointment.Visible = true;
                lblgrantedOfficer.Visible = true;
                lblgrank.Visible = true;


            }
            else
            {
                txtsearch.Visible = false;
                btnSearch.Visible = false;
                lblgAppointment.Visible = false;
                lblgOfficialNo.Visible = false;
                lblgAppointment.Visible = false;
                lblgrank.Visible = false;
                lblgrantedOfficer.Visible = false;

            }
        }

        protected void btnSearch0_Click(object sender, EventArgs e)
        {
            lblgrank0.Text = "";
            lblgrantedOfficer0.Text = "";
            lblgAppointment0.Text = "";
            lblgOfficialNo0.Text = "";



            SqlConnection con = new SqlConnection(myConnectionString);
            String selectSQL = @" SELECT        dbo.Profile_Master.Name_With_Initial, dbo.Profile_Master.Rank, dbo.Profile_Master.Appointment, dbo.Profile_Master.Officer_Id
    FROM    dbo.Profile_Master WHERE        (dbo.Profile_Master.[Official_No] = '" + txtsearch0.Text.Trim() + "')";

            con = new SqlConnection(myConnectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lblgrank0.Text = reader[1].ToString();
                lblgrantedOfficer0.Text = reader[0].ToString();
                lblgAppointment0.Text = reader[2].ToString();
                lblgOfficialNo0.Text = reader["Officer_Id"].ToString();


            }
            reader.Close();
            reader.Dispose();
            con.Close();
            con.Dispose();
            cmd.Dispose();
        }

        protected void chkleave_CheckedChanged(object sender, EventArgs e)
        {
            if (chkleave.Checked)
            {
                txtsearch0.Visible = true;
                btnSearch0.Visible = true;
                lblgAppointment0.Visible = true;
                lblgrantedOfficer0.Visible = true;
                lblgrank0.Visible = true;
                lblgOfficialNo0.Visible = true;
                Label20.Text = "Official No: (Example:- 3350)";


            }
            else
            {
                txtsearch0.Visible = false;
                btnSearch0.Visible = false;
                lblgAppointment0.Visible = false;
                lblgrantedOfficer0.Visible = false;
                lblgrank0.Visible = false;
                lblgOfficialNo0.Visible = false;
                Label20.Text = "";

            }
        }

        protected void txtid_TextChanged(object sender, EventArgs e)
        {

        }
        private DataTable loadPerson(string OfficialNo, string ServiceType)
        {

            // SELECT        a.officialNo, a.initials+' '+a.surename as namee,  ra.rankRate , app.appointmentCode ,branch.branchID

            SqlConnection con11 = new SqlConnection(ConfigurationManager.ConnectionStrings["M_SConnection"].ToString());
            con11.Open();
            SqlCommand cmm = new SqlCommand();
            cmm.Connection = con11;
            cmm.Parameters.AddWithValue("@Offno", OfficialNo);
            cmm.Parameters.AddWithValue("@ServiceType", ServiceType);

            cmm.CommandType = CommandType.StoredProcedure;
            cmm.CommandText = "getOfficerDetails";

            SqlDataReader drr;
            drr = cmm.ExecuteReader();
            DataTable person = new DataTable();
            drr.Close();

            person.Load(cmm.ExecuteReader());

            con11.Close();
            drr.Close();

            return person;
        }

        private void LoadHRMS(string Official_No, string ServiceType)
        {


            DataTable dt = new DataTable();

            dt = loadPerson(Official_No, ServiceType);


            if (dt.Rows.Count > 0)
            {
                txtRank.Text = dt.Rows[0]["RankRate"].ToString();
                txtName.Text = dt.Rows[0]["namee"].ToString();
                string Appointment = dt.Rows[0]["appointmentCode"].ToString();

                if (Appointment.Contains("AND"))
                {
                    string strStart = "AND";
                    int Start = Appointment.IndexOf(strStart, 0) + strStart.Length;
                    int End = Appointment.Length;
                    string OtherAppointment = Appointment.Substring(Start, End - Start);
                    Appointment = Appointment.Substring(0, Start - 3);
                    txtAppointment.Text = Appointment;
                    txtOtherAppointment.Text = OtherAppointment.Trim();

                }
                else
                {
                    txtAppointment.Text = Appointment;
                }

                txtbranch.Text = dt.Rows[0]["branchID"].ToString();
                txtBase.Text = dt.Rows[0]["baseDescription"].ToString();
                txtArea.Text = dt.Rows[0]["areaName"].ToString();



            }
        }
    }




}