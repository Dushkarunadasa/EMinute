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
using System.Web.SessionState;


namespace M_sheet
{
    public partial class ArchiveMinutePage : System.Web.UI.Page
    {

        string myConnectionString = ConfigurationManager.ConnectionStrings["M_SConnection"].ConnectionString;
       private string uid ;
       private string utype;
       private string from_min;
        private string From_Id;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
            if (Request.Cookies["LocalComputer"] != null)
            {
                HttpCookie _cookie = Request.Cookies["LocalComputer"];
                uid = _cookie["OfficialNo"].ToString().Trim();
                utype = _cookie["User_Type"].ToString().Trim();
            }

            if (!IsPostBack)
            {
                
                Fill_Inbox();
                Fill_Option();
                btnMinuteIn.BackColor = System.Drawing.Color.LawnGreen;

            }
            // }

            //catch (NullReferenceException ex)
            //{
            //    Response.Redirect("LoginPage.aspx");
            //}
        }

        public void Fiter_Inbox(String Type)
        {
            //try
            //{
            if (Type == "")
            {
                return;
            }
            if (Type != "---Select---")
            {
                switch (Type)
                {
                    case "ID":
                        Type = "([Min_ID] Like '%" + txtsearch.Text.Trim() + "%' ) and";
                        break;
                    case "FROM":
                        Type = "([ByPerson] Like  '%" + txtsearch.Text.Trim() + "%' ) and";
                        break;
                    case "Topic":

                        Type = "([Topic] Like  '%" + txtsearch.Text.Trim() + "%' ) and";
                        break;
                    default:
                        Type = "";
                        break;
                }
          
                SqlConnection con = new SqlConnection();
                con.ConnectionString = myConnectionString;
                con.Open();
                string cmdText = "SELECT [Min_ID] as [ID],[User_ID],[ByPerson] as [FROM], [Topic],[Read_Not] as [READ],[Res_Required] as [RESPOND REQUIRED] ,[RESPONDED] FROM [dbo].[Receivers] where " + Type + " ([User_Name]='" + uid + "' and MConfirm =1 and Archive=1) and ( ([Order_Follow]=1 and [RespondAllow]=1) or ([Order_Follow]=0)) Order By Read_Not asc   ,[Date_Time] desc ";
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
            //}
            //catch (NullReferenceException ex)
            //{
            //    Response.Redirect("LoginPage.aspx");
            //}
        }
        public void Fill_Inbox()
        {
            //try
            //{
           
            SqlConnection con = new SqlConnection();
            con.ConnectionString = myConnectionString;
            con.Open();
            string cmdText = "SELECT [Min_ID] as [ID],[User_ID],[ByPerson] as [FROM], [Topic],[Read_Not] as [READ],[Res_Required] as [RESPOND REQUIRED] ,[RESPONDED] FROM [dbo].[Receivers] where ([User_Name]='" + uid + "' and MConfirm =1 and [Archive]=1) and ( ([Order_Follow]=1 and [RespondAllow]=1 ) or ([Order_Follow]=0)) Order By Read_Not asc   ,[Date_Time] desc ";
            SqlCommand cmd = new SqlCommand(cmdText, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GVInbox.DataSource = ds;
            GVInbox.DataBind();

            con.Close();
            //  }
            //catch (NullReferenceException ex)
            //{
            //    Response.Redirect("LoginPage.aspx");
            //}
        }
        protected void GVInbox_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            e.Row.Cells[3].Visible = false;
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnNewMinute_Click(object sender, EventArgs e)
        {

        }

        protected void GVInbox_SelectedIndexChanged(object sender, EventArgs e)
        {



        }
        protected void GVInbox_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //try
            //{
      
            if ((e.CommandName == "Update") && (Session["mfrom"] == "Inbox" || Session["mfrom"] == null))
            {
                int indexs = Convert.ToInt32(e.CommandArgument);
                GridViewRow rows = GVInbox.Rows[indexs];

                from_min = rows.Cells[2].Text.ToString();
                From_Id = rows.Cells[3].Text.ToString();
            
                if (!utype.Equals("Authorized User"))
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = myConnectionString;
                    con.Open();
                    string sqlstring = "Update dbo.Min_References set Archive=0 ,ReadTime=CONVERT(datetime, GETDATE()) where Min_Id='" + from_min.Trim() + "' and User_Id='" + From_Id + "' and User_Name='" + uid.ToString().Trim() + "' AND Read_Not=1 and Archive=1";
                    SqlCommand cmd = new SqlCommand(sqlstring, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    con.Dispose();
                    cmd.Dispose();

                }
            }
            else if ((e.CommandName == "Update") && (Session["mfrom"] == "Sent" || Session["mfrom"] == null))
            {
                int indexs = Convert.ToInt32(e.CommandArgument);
                GridViewRow rows = GVInbox.Rows[indexs];
                from_min = rows.Cells[2].Text.ToString();
                
                if (!utype.Equals("Authorized User"))
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = myConnectionString;
                    con.Open();
                    string sqlstring = "Update [dbo].[Min_Master]  set Archive=0 where [MConfirm]=1 and [RUser_ID]='" + uid + "' and [User_ID]='" + uid + "' and [Min_ID]='" + from_min.Trim() + "' and MConfirm =1  and [Archive]=1 ";
                    SqlCommand cmd = new SqlCommand(sqlstring, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    con.Dispose();
                    cmd.Dispose();

                }

            }
            else if (Session["mfrom"] == "Inbox" || Session["mfrom"] == null)
            {


                string Usertype = utype;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVInbox.Rows[index];
                from_min =  row.Cells[2].Text.ToString();
                From_Id = row.Cells[3].Text.ToString();
               // Response.Redirect("ArchiveMinuteSheetOutPage.aspx");
            }
            else
            {
                string Usertype = utype;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVInbox.Rows[index];
                from_min = row.Cells[2].Text.ToString();
            
                //Response.Redirect("ArchiveMinuteSheetInPage.aspx");
            }
            // }
            //catch (NullReferenceException ex)
            //{
            //    Response.Redirect("LoginPage.aspx");
            //}
        }

        protected void GVInbox_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVInbox.PageIndex = e.NewPageIndex;

        }

        protected void btnMinuteOut_Click(object sender, EventArgs e)
        {
            btnMinuteOut.BackColor = System.Drawing.Color.LawnGreen;
            btnMinuteIn.BackColor = System.Drawing.Color.White;

            Session["mfrom"] = "Sent";
            Fill_OutBox();
            FillO_Option();

        }
        private void Fill_OutBox()
        {
            //try
            //{
    

            SqlConnection con = new SqlConnection();
            con.ConnectionString = myConnectionString;
            con.Open();
            string cmdText = "SELECT  [Min_ID] , [User_ID]        ,[Topic]     ,[Date_Time]  FROM [dbo].[Min_Master] where [MConfirm]=1 and [RUser_ID]='" + uid + "' and [User_ID]='" + uid + "'  and MConfirm =1  and [Archive]=1  Order By Min_ID DESC";
            SqlCommand cmd = new SqlCommand(cmdText, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GVInbox.DataSource = ds;
            GVInbox.DataBind();

            con.Close();
            //}
            //catch (NullReferenceException ex)
            //{
            //    Response.Redirect("LoginPage.aspx");
            //}
        }

        private void Filter_OutBox(String Type)
        {
            //try
            //{
            if (Type == "")
            {
                return;
            }
            if (Type != "---Select---")
            {
                switch (Type)
                {
                    case "Min_ID":
                        Type = "([Min_ID] Like '%" + txtsearch.Text.Trim() + "%' ) and";
                        break;
                    case "Topic":

                        Type = "([Topic] Like  '%" + txtsearch.Text.Trim() + "%' ) and";
                        break;

                    case "Date_Time":

                        Type = "([Date_Time] Like  '%" + txtsearch.Text.Trim() + "%' ) and";
                        break;
                    default:
                        Type = "";
                        break;
                }
               

                SqlConnection con = new SqlConnection();
                con.ConnectionString = myConnectionString;
                con.Open();
                string cmdText = "SELECT  [Min_ID] , [User_ID]        ,[Topic]     ,[Date_Time]  FROM [dbo].[Min_Master] where " + Type + "  [MConfirm]=1 and [RUser_ID]='" + uid + "' and [User_ID]='" + uid + "'  and MConfirm =1    Order By Min_ID DESC";
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
            //}
            //catch (NullReferenceException ex)
            //{
            //    Response.Redirect("LoginPage.aspx");
            //}
        }

        private void FillO_Option()
        {

            dpoption.Items.Clear();
            dpoption.Items.Add("---Select---");
            dpoption.Items.Add("Min_ID");
            dpoption.Items.Add("Topic");
            dpoption.Items.Add("Date_Time");
            txtsearch.Text = "";

        }

        protected void btnMinuteIn_Click(object sender, EventArgs e)
        {
            //try
            //{
            btnMinuteOut.BackColor = System.Drawing.Color.White;
            btnMinuteIn.BackColor = System.Drawing.Color.LawnGreen;
            Session["mfrom"] = "Inbox";
            Fill_Option();
            Fill_Inbox();
            //}
            //catch (Exception ex)
            //{
            //    Response.Redirect("LoginPage.aspx");
            //}
        }
        private void Fill_Option()
        {
            dpoption.Items.Clear();
            dpoption.Items.Add("---Select---");
            dpoption.Items.Add("ID");
            dpoption.Items.Add("FROM");
            dpoption.Items.Add("Topic");
            txtsearch.Text = "";

        }
        protected void Button1_Click(object sender, EventArgs e)
        {

        }





        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if ((btnMinuteOut.BackColor.Name == "0") || (btnMinuteOut.BackColor.Name == "White"))
            {

                Fiter_Inbox(dpoption.Text.Trim());

            }
            else
            {
                Filter_OutBox(dpoption.Text.Trim());
            }
        }

        protected void GVInbox_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if ((btnMinuteOut.BackColor.Name == "0") || (btnMinuteOut.BackColor.Name == "White"))
            {
                Fill_Inbox();
            }
            else
            {
                Fill_OutBox();
            }
        }
    }
}