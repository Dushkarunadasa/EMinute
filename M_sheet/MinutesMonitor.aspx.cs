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
    public partial class MinutesMonitor : System.Web.UI.Page
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
                FillAppointment();
            }

        }

        private void FillAppointment()
        {
          
            DropDownList2.Items.Clear();
            SqlConnection con = new SqlConnection(myConnectionString);
            con.Open();
            SqlCommand command = new SqlCommand("select distinct [Appointment] from [dbo].[Profile_Master] where [Area]='" + Area + "' order by [Appointment] asc ", con);//This query should be changed
            SqlDataReader Reader = command.ExecuteReader();
            ListItem newItem = new ListItem();

            while (Reader.Read())
            {


                newItem = new ListItem();
                newItem.Text = Reader["Appointment"].ToString().ToUpper();
                newItem.Value = Reader["Appointment"].ToString();
                DropDownList2.Items.Add(newItem);
            }

            command.Dispose();
            con.Close();

        }



        protected void Button1_Click(object sender, EventArgs e)//search by Off No
        {
            String offNo = TextBox1.Text.ToString();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = myConnectionString;
            con.Open();
            string cmdText = "select a.[Min_Id] as MIN_ID,b.Rank+' '+b.Name_With_Initial as BY_NAME,b.[Appointment] as BY_APPOINTMENT,a.[Name] as FORWARD_TO,convert(DATE,c.Date_Time) as MINUTE_SHEET_DATE,a.[Read_Not] as READ_,a.[ReadTime] AS READ_TIME,a.[Responded] as RESPOND_,a.[RespondTime] as RESPOND_TIME from [dbo].[Min_References] a,[dbo].[Profile_Master] b,[dbo].[Min_Master] c  where a.[User_Id]='" + offNo + "' and a.User_Id=b.Officer_Id and a.User_Id=c.User_ID and a.Min_Id=c.Min_ID group by a.[Min_Id],b.Rank,b.Name_With_Initial,b.[Appointment],a.[Name],a.[Read_Not],a.[ReadTime],a.[Responded],a.[RespondTime],c.Date_Time";
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

        

        protected void Button2_Click(object sender, EventArgs e)
        {
            String appoint = DropDownList2.Text.ToString();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = myConnectionString;
            con.Open();
            string cmdText = "select a.[Min_Id] as MIN_ID,b.Rank+' '+b.Name_With_Initial as BY_NAME,b.[Appointment] as BY_APPOINTMENT,a.[Name] as FORWARD_TO,convert(DATE,c.Date_Time) as MINUTE_SHEET_DATE,a.[Read_Not] as READ_,a.[ReadTime] AS READ_TIME,a.[Responded] as RESPOND_,a.[RespondTime] as RESPOND_TIME from [dbo].[Min_References] a,[dbo].[Profile_Master] b,[dbo].[Min_Master] c  where b.[Appointment]='" + appoint + "' and a.User_Id=b.Officer_Id and a.User_Id=c.User_ID and a.Min_Id=c.Min_ID group by a.[Min_Id],b.Rank,b.Name_With_Initial,b.[Appointment],a.[Name],a.[Read_Not],a.[ReadTime],a.[Responded],a.[RespondTime],c.Date_Time";
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