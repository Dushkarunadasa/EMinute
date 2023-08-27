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
using Officer_Details;



namespace M_sheet
{
    public partial class MyAccountPage1 : System.Web.UI.Page
    {

        string myConnectionString = ConfigurationManager.ConnectionStrings["M_SConnection"].ConnectionString;

        private string from_min;
        private string From_Id;
        private string uid = "";
        private string UserType = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.Cookies["LocalComputer"] != null)
                {
                    HttpCookie _cookie = Request.Cookies["LocalComputer"];
                    uid = _cookie["OfficialNo"].ToString().Trim ();
                    UserType = _cookie["User_Type"].ToString().Trim().ToUpper() ;
                }
                if (!IsPostBack)
                {

                    



                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = myConnectionString;
                    con.Open();
                    string sqlstring = "select * from dbo.Profile_Master where Officer_Id='" + uid + "'";
                    SqlCommand cmd = new SqlCommand(sqlstring, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        txtName.Text = reader[2].ToString();
                        txtRank.Text = reader[3].ToString();
                        txtAppointment.Text = reader[4].ToString();
                        txtDepartment.Text = reader[7].ToString();
                        txtBase.Text = reader[6].ToString();
                        txtCommand.Text = reader[5].ToString();

                        if (reader["Onleave"].Equals(true))
                        {
                            onleave();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('On leave feature is dactivated now.');", true);
                        }
                    }
                    con.Close();
                    con.Dispose();
                    cmd.Dispose();
                    reader.Dispose();
                    Fill_OutBox();
                    FillO_Option();
                }
            }

            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.ToString() + "');", true);
            }
        }

        public void onleave()
        {
            
            SqlConnection newcon = new SqlConnection();
            newcon.ConnectionString = myConnectionString;
            newcon.Open();
            SqlCommand newcmd = new SqlCommand();
            String newsqlstring = "Update [dbo].[Profile_Master] SET Onleave=0 ,[OverSeesBy] =''  where [Officer_Id] ='" + uid + "'  ";
            SqlCommand insertCmd = new SqlCommand(newsqlstring, newcon);
            insertCmd.ExecuteNonQuery();
            newcon.Close();
            newcon.Dispose();
            newcmd.Dispose();


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
          
         
           if (e.CommandName == "Update") 
            {
                int indexs = Convert.ToInt32(e.CommandArgument);
                GridViewRow rows = GVInbox.Rows[indexs];
                from_min   = rows.Cells[2].Text.ToString();               
                if (!UserType.Equals("Authorized User"))
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = myConnectionString;
                    con.Open();
                    string sqlstring = "Update [dbo].[Min_Master]  set Archive=1 where [MConfirm]=1 and [RUser_ID]='" + uid + "' and [User_ID]='" + uid + "' and [Min_ID]='" + from_min.Trim() + "' and MConfirm =1  and [Archive]=0 ";
                    SqlCommand cmd = new SqlCommand(sqlstring, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    con.Dispose();
                    cmd.Dispose();

                }

            }
           else
           {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVInbox.Rows[index];
                from_min = row.Cells[2].Text.ToString();             
                if ((UserType.Equals("ADMIN")) || (UserType.Equals("USER")))
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = myConnectionString;
                    con.Open();
                    string sqlstring = "Update dbo.Min_References set [RespondRead]=1  where Min_Id='" + from_min.Trim() + "' and User_Id='" + uid + "' AND [Responded]=1";
                    SqlCommand cmd = new SqlCommand(sqlstring, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    con.Dispose();
                    cmd.Dispose();

                }
                if (from_min != "")
                {
                    HttpCookie _cookie2 = new HttpCookie("LocalComputer1");
                    _cookie2.Values.Add("From_Min", from_min);
                    _cookie2.Expires = DateTime.Now.AddMinutes(15);
                    Response.Cookies.Add(_cookie2);
                }
                Response.Redirect("OutBox.aspx");
            }
           
            //}
            //catch (Exception  ex)
            //{
            //    System.Windows.Forms.MessageBox.Show(ex.Message, "Error Message", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                
            //}
        }

        protected void GVInbox_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVInbox.PageIndex = e.NewPageIndex;

        }

        protected void btnMinuteOut_Click(object sender, EventArgs e)
        {
            //btnMinuteOut.BackColor = System.Drawing.Color.LawnGreen;
            //btnMinuteIn.BackColor = System.Drawing.Color.White; 
            //Session["mfrom"] = "Sent";
            //Fill_OutBox();
            //FillO_Option();

        }
        private void Fill_OutBox()
        {
            try
            {
          

            SqlConnection con = new SqlConnection();
            con.ConnectionString = myConnectionString;
            con.Open();
            string cmdText = "SELECT  [Min_ID] , [User_ID]        ,[Topic]     ,[Date_Time] as Date_Time  FROM [dbo].[Min_Master] where [MConfirm]=1 and [RUser_ID]='" + uid + "' and [User_ID]='" + uid + "'  and MConfirm =1  and [Archive]=0  Order By Min_ID DESC";
            SqlCommand cmd = new SqlCommand(cmdText, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GVInbox.DataSource = ds;
            GVInbox.DataBind();

            con.Close();
            }
            catch (Exception  ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error Message", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                
            }
        }

        private void Filter_OutBox(String Type)
        {
            try
            {
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
                string cmdText = "SELECT  [Min_ID] , [User_ID]        ,[Topic]     ,CONVERT(date, CONVERT(varchar, [Date_Time], 101)) as Date_Time  FROM [dbo].[Min_Master] where " + Type + "  [MConfirm]=1 and [RUser_ID]='" + uid + "' and [User_ID]='" + uid + "'  and MConfirm =1    Order By Min_ID DESC";
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
            catch (Exception  ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error Message", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                
            }
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
            ////try
            ////{
            //    btnMinuteOut.BackColor = System.Drawing.Color.White;
            //    btnMinuteIn.BackColor = System.Drawing.Color.LawnGreen; 
            //    Session["mfrom"] = "Inbox";
            //    Fill_Option();
            //    Fill_Inbox();
            ////}
            ////catch (Exception ex)
            ////{
            ////    Response.Redirect("LoginPage.aspx");
            ////}
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

        protected void btnUserManual_Click(object sender, EventArgs e)
        {
            Response.ContentType = "Application/doc";
            Response.AppendHeader("Content-Disposition", "attachment; filename=User_manual.doc");
            Response.TransmitFile(Server.MapPath("~/References/User_manual.doc"));
            Response.End();
        }



        protected void btnSearch_Click(object sender, EventArgs e)
        {
           
                Filter_OutBox(dpoption.Text.Trim());
            
        }

        protected void GVInbox_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //if ((btnMinuteOut.BackColor.Name == "0") || (btnMinuteOut.BackColor.Name == "White"))
            //{
            //    Fill_Inbox();
            //}
            //else
            //{
            //    Fill_OutBox();
            //}
        }
    }
}