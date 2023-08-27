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
//using System.Windows.Forms;
using System.Web.SessionState;
namespace M_sheet
{
    public partial class AddUsers : System.Web.UI.Page
    {

        string myConnectionString = ConfigurationManager.ConnectionStrings["M_SConnection"].ConnectionString;
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            lblinfo.Text = "";
            if ((this.txtOfficialNumber.Text.Trim()) == "")
            {
                lblinfo.ForeColor = System.Drawing.Color.Red;
                lblinfo.Text = "Required Official Number for registration";
                lblinfo.Visible = true;
                return;
            }
            if ((this.txtbranch.Text.Trim()) == "")
            {
                lblinfo.ForeColor = System.Drawing.Color.Red;
                lblinfo.Text = "Required Branch for registration";
                lblinfo.Visible = true;
                return;
            }
            if (this.txtName.Text.Trim() == "")
            {
                lblinfo.ForeColor = System.Drawing.Color.Red;
                lblinfo.Text = "Required Name for registration";
                lblinfo.Visible = true;
                return;
            }
            if ((this.txtBase.Text.Trim()) == "")
            {
                lblinfo.ForeColor = System.Drawing.Color.Red;
                lblinfo.Text = "Required Base for registration";
                lblinfo.Visible = true;
                return;
            }
            if ((this.cmbDepartment.Text.Trim()) == "0")
            {
                lblinfo.ForeColor = System.Drawing.Color.Red;
                lblinfo.Text = "Select the Department";
                lblinfo.Visible = true;
                return;
            }

            SqlConnection GetData = new SqlConnection();
            GetData.ConnectionString = myConnectionString;
            GetData.Open();
            string insertCmdText = "Select * from [Profile_Master] where [Official_No]='" + txtOfficialNumber.Text.Trim() + "'";
            SqlCommand cmds = new SqlCommand(insertCmdText, GetData);
            SqlDataReader readers = cmds.ExecuteReader();
            while (readers.Read())
            {

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["M_SConnection"].ConnectionString))
                 {
                    using (SqlCommand cmd = new SqlCommand("SP_UPDATE_REGISTER", con)) 
                    {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Official_No", SqlDbType.NVarChar).Value = txtOfficialNumber.Text.Trim();                
                    cmd.Parameters.AddWithValue("@Branch", SqlDbType.NVarChar).Value = txtbranch.Text.Trim();               
                    cmd.Parameters.AddWithValue("@Officer_Id", SqlDbType.NChar).Value = txtbranch.Text.Trim().ToUpper()+txtOfficialNumber.Text.Trim();
                    
                    con.Open();
                    cmd.ExecuteNonQuery();
                    }
                 }

              
                lblinfo.Text = "User already registered.";
                lblinfo.ForeColor = System.Drawing.Color.Red;
                lblinfo.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('User already registered.');", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('User already" + txtOfficialNumber.Text + " registered.');", true);

               // return;
            }
            lblinfo.Visible = false;
            GetData.Close();
            GetData.Dispose();
            cmds.Dispose();
            readers.Dispose();

           

            string oldPath = Server.MapPath("~/LeaveSys/Signatures/" + txtOfficialNumber.Text.Trim() + ".jpg");
        
            
            string newPath = Server.MapPath("~/Signatures/" + txtOfficialNumber.Text.Trim() + ".jpg");
          

            if (File.Exists(oldPath) == false)
            {
                lblinfo.Text = "Signature image not found.Please contact SDU Unit.";
                lblinfo.Visible = true;

            }
            else
            {

                if (File.Exists(newPath) == true)
                {
                    File.Delete(newPath);
                }
                File.Copy(oldPath, newPath, true);
                newPath = "~/Signatures/" + txtOfficialNumber.Text.Trim() + ".jpg";


                byte[] imageBytes = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(newPath));



                SqlConnection tcon = new SqlConnection();
                tcon.ConnectionString = myConnectionString;
                tcon.Open();


                SqlCommand tcmd = new SqlCommand("Usp_mypicsstore", tcon);

                tcmd.CommandType = CommandType.StoredProcedure;

                tcmd.Parameters.AddWithValue("@User_ID", txtbranch.Text.Trim().ToUpper() + txtOfficialNumber.Text.Trim()); // storing name of image
                tcmd.Parameters.AddWithValue("@ImageName", txtOfficialNumber.Text.Trim() + ".jpg"); // storing name of image
                tcmd.Parameters.AddWithValue("@Image", imageBytes); // bytes
                tcmd.Parameters.AddWithValue("@ImageSize", 20000); // getting file size

                int returnvalue = tcmd.ExecuteNonQuery();

                tcon.Close();

                tcmd.Dispose();

                SqlConnection conGetData = new SqlConnection();
                conGetData.ConnectionString = myConnectionString;
                conGetData.Open();
                string Base = txtBase.Text;
                string otherAppointment = txtOtherAppointment.Text.Trim().ToUpper();
                //Image name should save



                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["M_SConnection"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_INSERT_REGISTER", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Official_No", SqlDbType.NVarChar).Value = txtOfficialNumber.Text.Trim();
                        cmd.Parameters.AddWithValue("@Name_With_Initial", SqlDbType.NVarChar).Value = txtName.Text.Trim().ToUpper();
                        cmd.Parameters.AddWithValue("@Rank", SqlDbType.NVarChar).Value = txtrank.Text.Trim().ToUpper();
                        cmd.Parameters.AddWithValue("@Appointment", SqlDbType.NVarChar).Value = txtAppointment.Text.Trim().ToUpper();
                        cmd.Parameters.AddWithValue("@Area", SqlDbType.NVarChar).Value = txtArea.Text.Trim();
                        cmd.Parameters.AddWithValue("@Ship_Establishment", SqlDbType.NVarChar).Value = txtBase.Text.Trim();
                        cmd.Parameters.AddWithValue("@Department", SqlDbType.NVarChar).Value = cmbDepartment.Text.Trim();
                        cmd.Parameters.AddWithValue("@Branch", SqlDbType.NVarChar).Value = txtbranch.Text.Trim();
                        cmd.Parameters.AddWithValue("@imagePath", SqlDbType.NVarChar).Value = newPath;
                        cmd.Parameters.AddWithValue("@User_Type", SqlDbType.NVarChar).Value = "User";
                        cmd.Parameters.AddWithValue("@Active", SqlDbType.Bit).Value = 1;
                        cmd.Parameters.AddWithValue("@Other_Appointment", SqlDbType.NVarChar).Value = txtOtherAppointment.Text.Trim();
                        cmd.Parameters.AddWithValue("@Onleave", SqlDbType.NVarChar).Value = 0;
                        cmd.Parameters.AddWithValue("@OverSeesBy", SqlDbType.NVarChar).Value = "";
                        cmd.Parameters.AddWithValue("@AppointmentSin", SqlDbType.NVarChar).Value = txtAppointmentSin.Text;
                        cmd.Parameters.AddWithValue("@Other_AppointmentSin", SqlDbType.NVarChar).Value = txtOtherAppointmentSin.Text;
                        cmd.Parameters.AddWithValue("@RankSin", SqlDbType.NVarChar).Value = txtrankSin.Text;
                        cmd.Parameters.AddWithValue("@Name_With_Initial_Sin", SqlDbType.NVarChar).Value = txtnameSin.Text;
                        cmd.Parameters.AddWithValue("@Service_Type", SqlDbType.NChar).Value = ddlServiceType.Text;
                        cmd.Parameters.AddWithValue("@Officer_Id", SqlDbType.NChar).Value = txtbranch.Text.Trim().ToUpper() + txtOfficialNumber.Text.Trim();

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                lblinfo.Text = "User registration completed.";
                lblinfo.ForeColor = System.Drawing.Color.Green;
                lblinfo.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Registration details are saved.);", true);
            }

        }



        protected void Page_Load(object sender, EventArgs e)
        {
            lblinfo.Visible = false;
            if (!IsPostBack)
            {
               // loadDepartment();
            }
        }

     
        private void clear()
        {
            txtOfficialNumber.Text = "";
            txtbranch.Text = "";
            txtAppointment.Text = "";
            txtBase.Text = "";
            txtrank.Text = "";
            txtrankSin.Text = "";
            txtAppointmentSin.Text = "";
            txtOtherAppointmentSin.Text = "";
            txtnameSin.Text = "";
            cmbDepartment.DataSource = null;
            txtName.Text = "";
            txtArea.Text = "";
            txtOtherAppointment.Text = "";

        }


        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtOfficialNumber.Text = "";
            txtbranch.Text = "";
            txtAppointment.Text = "";
            txtBase.Text = "";
            txtrank.Text = "";
            txtrankSin.Text = "";
            txtAppointmentSin.Text = "";
            txtOtherAppointmentSin.Text = "";
            txtnameSin.Text = "";
            cmbDepartment.DataSource = null;
            cmbDepartment.Items.Clear();
            txtName.Text = "";
            txtArea.Text = "";          
            txtOtherAppointment.Text = "";

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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblinfo.Visible = false;
            {
                if (txtOfficialNumber.Text == "")
                {
                    lblinfo.Visible = true;
                    lblinfo.Text = "Please enter person's details correctly";

                }
                else 
                {

                    DataTable dt = new DataTable();

                    dt = loadPerson(txtOfficialNumber.Text, ddlServiceType.Text);
             

                    if (dt.Rows.Count > 0)
                    {
                        txtrank.Text = dt.Rows[0]["RankRate"].ToString();
                        txtName.Text = dt.Rows[0]["namee"].ToString();                        
                        string Appointment =dt.Rows[0]["appointmentCode"].ToString();

                        if (Appointment.Contains("AND"))
                        {
                           string strStart = "AND";
                           int  Start = Appointment.IndexOf(strStart, 0) + strStart.Length;
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
                        txtBase.Text=dt.Rows[0]["baseDescription"].ToString();
                        txtArea.Text = dt.Rows[0]["areaName"].ToString(); 
                        dt = new DataTable();
                        dt = loadDepartment(txtArea.Text.Trim());
                        cmbDepartment.DataSource = dt;

                        cmbDepartment.DataTextField = "departmentName";
                        cmbDepartment.DataValueField = "departmentName";
                        cmbDepartment.DataBind();
                        cmbDepartment.Enabled = true;
                        cmbDepartment.Items.Insert(0, new ListItem("-----Please select-----", "0"));


                        lblinfo.Text = "";
                        cmbDepartment.Focus();
                    }
                    else
                    {
                        clear();
                        lblinfo.Visible = true;
                        lblinfo.Text = "There is no such person";

                    }
                }
            }
        }

    }
}