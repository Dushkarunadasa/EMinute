using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace M_sheet
{
    public partial class UserUpdate : System.Web.UI.Page
    {
        string myConnectionString = ConfigurationManager.ConnectionStrings["M_SConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            
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


        private DataTable loadArea()
        {


            SqlConnection con12 = new SqlConnection(ConfigurationManager.ConnectionStrings["M_SConnection"].ToString());
            con12.Open();
            SqlCommand cmm = new SqlCommand();
            cmm.Connection = con12;


            cmm.CommandType = CommandType.StoredProcedure;
            cmm.CommandText = "getArea";

            SqlDataReader drr;
            drr = cmm.ExecuteReader();
            DataTable person = new DataTable();
            drr.Close();

            person.Load(cmm.ExecuteReader());

            con12.Close();
            drr.Close();

            return person;
        }

        private DataTable loadShipEst()
        {


            SqlConnection con13 = new SqlConnection(ConfigurationManager.ConnectionStrings["M_SConnection"].ToString());
            con13.Open();
            SqlCommand cmm = new SqlCommand();
            cmm.Connection = con13;


            cmm.CommandType = CommandType.StoredProcedure;
            cmm.CommandText = "getShipEstablishment";

            SqlDataReader drr;
            drr = cmm.ExecuteReader();
            DataTable person = new DataTable();
            drr.Close();

            person.Load(cmm.ExecuteReader());

            con13.Close();
            drr.Close();

            return person;
        }

        protected void Button1_Click(object sender, EventArgs e)//Search
        {
            try
            {
                String offNum = TextBox1.Text.ToString();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = myConnectionString;
                con.Open();
                string cmdText = "select [Officer_Id] ,[Appointment],[Other_Appointment],[Area],[Ship_Establishment],[Department] from [dbo].[Profile_Master] where [Officer_Id]='" + offNum + "'";
                SqlCommand cmd = new SqlCommand(cmdText, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                String Officer_Id = ds.Tables[0].Rows[0]["Officer_Id"].ToString();
                String Appointment = ds.Tables[0].Rows[0]["Appointment"].ToString();
                String Other_Appointment = ds.Tables[0].Rows[0]["Other_Appointment"].ToString();
                String Area = ds.Tables[0].Rows[0]["Area"].ToString();
                String Ship_Establishment = ds.Tables[0].Rows[0]["Ship_Establishment"].ToString();
                String Department = ds.Tables[0].Rows[0]["Department"].ToString();
                TextBox2.Text = Appointment.ToString();
                TextBox5.Text = Other_Appointment.ToString();
                TextBox3.Text = Area.ToString();
                TextBox4.Text = Ship_Establishment.ToString();
                TextBox6.Text = Department.ToString();
                cmd.Dispose();
                da.Dispose();
                con.Close();
                Label1.Text = "";

                DataTable dt = new DataTable();
                dt = new DataTable();
                dt = loadDepartment(TextBox3.Text);
                cmbDepartment.DataSource = dt;

                cmbDepartment.DataTextField = "departmentName";
                cmbDepartment.DataValueField = "departmentName";
                cmbDepartment.DataBind();
                cmbDepartment.Visible = true;
                cmbDepartment.Items.Insert(0, new ListItem("-----Please select-----", "0"));

                DataTable dt1 = new DataTable();
                dt1 = new DataTable();
                dt1 = loadArea();
                areaSelect.DataSource = dt1;

                areaSelect.DataTextField = "Area";
                areaSelect.DataValueField = "Area";
                areaSelect.DataBind();
                areaSelect.Visible = true;
                areaSelect.Items.Insert(0, new ListItem("-----Please select-----", "0"));

                DataTable dt2 = new DataTable();
                dt2 = new DataTable();
                dt2 = loadShipEst();
                Ship_EstablishmentSelect.DataSource = dt2;

                Ship_EstablishmentSelect.DataTextField = "Ship_Establishment";
                Ship_EstablishmentSelect.DataValueField = "Ship_Establishment";
                Ship_EstablishmentSelect.DataBind();
                Ship_EstablishmentSelect.Visible = true;
                Ship_EstablishmentSelect.Items.Insert(0, new ListItem("-----Please select-----", "0"));

                
            }
            catch {
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = "Invalid User"; }
        }

        protected void Button2_Click(object sender, EventArgs e)//Update
        {

            try
            {
                SqlConnection con1 = new SqlConnection();
                con1.ConnectionString = myConnectionString;
                con1.Open();
                string sqlstring = "update [dbo].[Profile_Master] set [Appointment]='" + TextBox2.Text + "',[Other_Appointment]='" + TextBox5.Text + "',[Area]='" + TextBox3.Text + "',[Ship_Establishment]='" + TextBox4.Text + "',[Department]='"+TextBox6.Text+"' where [Officer_Id]='" + TextBox1.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlstring, con1);
                cmd.ExecuteNonQuery();
                con1.Close();
                con1.Dispose();
                cmd.Dispose();
                Label1.ForeColor = System.Drawing.Color.Green;
                Label1.Text = "Updated..";
            }
            catch
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = "Error Update..";
            }


        }

        protected void areaSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox3.Text = areaSelect.Text.ToString();
            
        }

        protected void Ship_EstablishmentSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox4.Text = Ship_EstablishmentSelect.Text.ToString();
        }

        protected void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox6.Text = cmbDepartment.Text.ToString();
        }
    }
}