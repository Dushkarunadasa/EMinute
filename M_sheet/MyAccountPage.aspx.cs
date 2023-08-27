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
using System.Windows;



namespace M_sheet
{
    public partial class MyAccountPage : System.Web.UI.Page
    {

        string myConnectionString = ConfigurationManager.ConnectionStrings["M_SConnection"].ConnectionString;

        private string from_min;
        private string From_Id;
        private string uid = "";
        private string Usertype = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.Cookies["LocalComputer"] != null)
                {
                    HttpCookie _cookie = Request.Cookies["LocalComputer"];
                    uid = _cookie["OfficialNo"].ToString().Trim();
                    Usertype = _cookie["User_Type"].ToString().Trim();
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
                    Fill_Inbox();
                    Fill_Option();
                }

            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error Message", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.ToString() + "');", true);
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
        public void Fiter_Inbox(String Type)
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
                    string cmdText = "SELECT [Min_ID] as [ID],[User_ID],[ByPerson] as [FROM], [Topic],[Read_Not] as [READ] ,[RESPONDED] FROM [dbo].[Receivers] where " + Type + " ([User_Name]='" + uid + "' and MConfirm =1) and ( ([Order_Follow]=1 and [RespondAllow]=1) or ([Order_Follow]=0)) Order By Read_Not asc   ,[Date_Time] desc ";
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
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error Message", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        public void Fill_Inbox()
        {
            try
            {

                SqlConnection con = new SqlConnection();
                con.ConnectionString = myConnectionString;
                con.Open();
                string cmdText = "SELECT [Min_ID] as [ID],[User_ID],[ByPerson] as [FROM], [Topic],[Read_Not] as [READ],[RESPONDED] FROM [dbo].[Receivers] where ([User_Name]='" + uid + "' and MConfirm =1 and [Archive]=0) and ( ([Order_Follow]=1 and [RespondAllow]=1 ) or ([Order_Follow]=0)) Order By Read_Not asc   ,[Date_Time] desc ";
                SqlCommand cmd = new SqlCommand(cmdText, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                GVInbox.DataSource = ds;
                GVInbox.DataBind();

                con.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error Message", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
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

                    from_min = rows.Cells[2].Text.ToString();
                    From_Id = rows.Cells[3].Text.ToString();

                    if (!Usertype.Equals("Authorized User"))
                    {
                        SqlConnection con = new SqlConnection();
                        con.ConnectionString = myConnectionString;
                        con.Open();
                        string sqlstring = "Update dbo.Min_References set Archive=1 ,ReadTime=CONVERT(datetime, GETDATE()) where Min_Id='" + from_min.Trim() + "' and User_Id='" + From_Id + "' and User_Name='" + uid.ToString().Trim() + "' AND Read_Not=1";
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
                    from_min = row.Cells[2].Text.ToString().Trim ();
                    From_Id = row.Cells[3].Text.ToString().Trim ();

                    if (!Usertype.Equals("Authorized User"))
                    {
                        SqlConnection con = new SqlConnection();
                        con.ConnectionString = myConnectionString;
                        con.Open();
                        string sqlstring = "Update dbo.Min_References set Read_Not=1 ,ReadTime=CONVERT(datetime, GETDATE()) where Min_Id='" + from_min.Trim() + "' and User_Id='" + From_Id + "' and User_Name='" + uid.ToString().Trim() + "' AND Read_Not=0";
                        SqlCommand cmd = new SqlCommand(sqlstring, con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        con.Dispose();
                        cmd.Dispose();
                    }

                    HttpCookie _cookie2 = new HttpCookie("LocalComputer3");
                    _cookie2.Values.Add("From_Min", from_min.Trim ());
                    _cookie2.Values.Add("From_User", From_Id.Trim ());
                    _cookie2.Expires = DateTime.Now.AddMinutes(15);
                    Response.Cookies.Add(_cookie2);

                    Response.Redirect("Inbox.aspx");
                }


                return;
            //}
            //catch (Exception ex)
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
            

                Fiter_Inbox(dpoption.Text.Trim());

            
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