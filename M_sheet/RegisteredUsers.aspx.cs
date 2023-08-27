using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace M_sheet
{
    public partial class RegisteredUsers : System.Web.UI.Page
    {
        string myConnectionString = ConfigurationManager.ConnectionStrings["M_SConnection"].ConnectionString;
        private string Area;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.Cookies["LocalComputer"] != null)
            {
                HttpCookie _cookie = Request.Cookies["LocalComputer"];
                //uid = _cookie["OfficialNo"].ToString().Trim();
                //Usertype = _cookie["User_Type"].ToString().Trim();
                //Toid = _cookie["OfficialNo"].ToString().Trim();
                Area = _cookie["Area"].ToString().Trim();
            }

            if (!IsPostBack)
            {
                FillBases();
            }

        }

        private void FillBases()
        {
            
            dpBase.Items.Clear();
            SqlConnection con = new SqlConnection(myConnectionString);
            con.Open();
            SqlCommand command = new SqlCommand("select distinct [Ship_Establishment] from [dbo].[Profile_Master] where [Area]='" + Area + "' order by [Ship_Establishment] asc ", con);//This query should be changed
            SqlDataReader Reader = command.ExecuteReader();
            ListItem newItem = new ListItem();

            while (Reader.Read())
            {


                newItem = new ListItem();
                newItem.Text = Reader["Ship_Establishment"].ToString().ToUpper();
                newItem.Value = Reader["Ship_Establishment"].ToString();
                dpBase.Items.Add(newItem);
            }

            command.Dispose();
            con.Close();

        }

        protected void btnStaff_Click(object sender, EventArgs e)
        {
            String shipEst = dpBase.Text.ToString();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = myConnectionString;
            con.Open();
            string cmdText = "select [Officer_Id] as OFFICIAL_NO,[Rank]+' '+[Name_With_Initial] as NAME,[Appointment] as APPOINTMENT from [dbo].[Profile_Master] where [Ship_Establishment]='" + shipEst + "' order by [Appointment] asc ";
            SqlCommand cmd = new SqlCommand(cmdText, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GVInbox.DataSource = ds;
            GVInbox.DataBind();
            cmd.Dispose();
            da.Dispose();
            con.Close();
        }


    }


}