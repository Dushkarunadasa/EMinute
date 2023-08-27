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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using M_sheet_Details;

namespace M_sheet
{
    public partial class OutBox : System.Web.UI.Page
    {
        string myConnectionString = ConfigurationManager.ConnectionStrings["M_SConnection"].ConnectionString;
       private string  uid ="";
       private string UType = "";
       private string from_min = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{

            if (Request.Cookies["LocalComputer"] != null)
            {
                HttpCookie _cookie = Request.Cookies["LocalComputer"];
                uid = _cookie["OfficialNo"].ToString().Trim();             
                UType = _cookie["User_Type"].ToString().Trim();
            }
            if (Request.Cookies["LocalComputer1"] != null)
            {
                HttpCookie _cookie = Request.Cookies["LocalComputer1"];
                from_min  = _cookie["From_Min"].ToString().Trim();
                
            }
          
               
                
                
                loaddata();
                makegrid();
                Fill_Grid();
                ReportDocument rptDoc = new ReportDocument();
                M_sheet_Details.MinDetails ds = new M_sheet_Details.MinDetails(); // .xsd file name
                DataTable dt = new DataTable();


                // Just set the name of data table
                dt.TableName = "MinDetails";
                dt = getMinDetails(); //This function is located below this function

                ds.Tables[0].Merge(dt);
                string name="";
                if (ds.Tables[0] != null || ds.Tables[0].Rows.Count == 0)
                {

                    name = "Table has data";

                }
                else
                {
                    name = "no data";
                }

                // Your .rpt file path will be below
                rptDoc.Load(Server.MapPath("~/Reports/CryMin.rpt"));

                //set dataset to the report viewer.
                rptDoc.SetDataSource(ds);
                M_sheet_Details.DtRef ds1 = new M_sheet_Details.DtRef(); // .xsd file name

                DataTable dt1 = new DataTable();
                // Just set the name of data table
                dt1.TableName = "Crystal Repor";
                dt1 = getReference(); //This function is located below this function
                ds1.Tables[0].Merge(dt1);

                M_sheet_Details.DtImage ds2 = new M_sheet_Details.DtImage(); // .xsd file name
                DataTable dt2 = new DataTable();
                // Just set the name of data table
                dt2.TableName = "Crystal Image";
                dt2 = getImage(); //This function is located below this function
                ds2.Tables[0].Merge(dt2);

                rptDoc.Subreports[1].SetDataSource(ds1);
                rptDoc.Subreports[0].SetDataSource(ds2);

                CrystalReportViewer1.ReportSource = rptDoc;
                CrystalReportViewer1.DataBind();
                CrystalReportViewer1.RefreshReport();

            //}
            //catch (NullReferenceException ex)
            //{
            //    Response.Redirect("LoginPage.aspx");
            //}
        }
        public DataTable getImage()
        {
            //Connection string replace 'databaseservername' with your db server name
            string sqlCon = "SELECT [User_Id],[Image],[ImageName] from [tempImage] where User_Id='"+ uid +"' ";

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = myConnectionString;
            Con.Open();
            SqlCommand cmd = new SqlCommand(sqlCon, Con);
            DataSet ds = null;
            SqlDataAdapter adapter;
            try
            {
                ds = new DataSet();
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds, "Image");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                //cmd.Dispose();
                //if (Con.State != ConnectionState.Closed)
                //    Con.Close();
            }
            return ds.Tables[0];
        }

        public DataTable getReference()
        {
          
            //Connection string replace 'databaseservername' with your db server name
            string sqlCon = "SELECT [Min_Id]  ,[User_Id],[User_Name],[Name]     FROM [dbo].[Min_References] where   [User_ID]='" + uid.ToString().Trim() + "' and [Min_ID]='" + from_min.ToString().Trim() + "' and Displayonly=0 Order By [Order] ASC";

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = myConnectionString;
            Con.Open();
            SqlCommand cmd = new SqlCommand(sqlCon, Con);
            DataSet ds = null;
            SqlDataAdapter adapter;
            //try
            //{
                ds = new DataSet();
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds, "Reference");
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
            //finally
            //{
                //cmd.Dispose();
                //if (Con.State != ConnectionState.Closed)
                //    Con.Close();
            //}
            return ds.Tables[0];
        }
        public DataTable getMinDetails()
        {
            
            //Connection string replace 'databaseservername' with your db server name
            string sqlCon = "SELECT     [Min_ID]    ,[Min_SubID]  ,[Topic]  ,IIF([Medium]='English','BY '+[ByPerson],[ByPerson]) as ByPerson ,[Date_Time] ,[MBody]   ,[imagePath]  ,[Ref_Order]  ,LTRIM(RTRIM([Description])) as Description ,[Ref_Path] ,[RUser_ID],[RMin_ID] ,LTRIM(RTRIM([Description1])) as Description1 ,[Ref_Path1] ,LTRIM(RTRIM([Description2])) as Description2,[Ref_Path2],[User_ID],iif(LTRIM(RTRIM([Description3]))='' ,' ',[Description3]) as Description3,[Ref_Path3] ,[Medium] FROM [dbo].[FullMin] where   [RUser_ID]='" + uid.ToString().Trim() + "' and [RMin_ID]='" + from_min.ToString().Trim() + "'Order By [Sub_ID] ASC";

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = myConnectionString;
            Con.Open();
            SqlCommand cmd = new SqlCommand(sqlCon, Con);
            DataSet ds = null;
            SqlDataAdapter adapter;
            //try
            //{
                ds = new DataSet();
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds, "Minutes");
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
            //finally
            //{
                cmd.Dispose();
                if (Con.State != ConnectionState.Closed)
                    Con.Close();
            //}
            return ds.Tables[0];
        }
        private void loaddata()
        {
            //try
            //{
                
                SqlConnection con = new SqlConnection();
                con.ConnectionString = myConnectionString;
                con.Open();
                string cmdText = "SELECT [User_Name] as [ID],[Name] as [To], [Topic],[Read_Not] as [READ],[ReadTime] as [READ TIME],[RESPONDED],[RespondTime] as [RESPONDED TIME] FROM [dbo].[Receivers] where [User_ID]='" + uid.ToString().Trim() + "' and Min_ID='" + from_min.ToString().Trim() + "' and Displayonly=0 Order By [Read_Not] ASC";
                SqlCommand cmd = new SqlCommand(cmdText, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                GVSent_Min.DataSource = ds;
                GVSent_Min.DataBind();

                con.Close();
            //}
            //catch (NullReferenceException ex)
            //{
            //    Response.Redirect("LoginPage.aspx");
            //}
        }

        protected void GVSent_Min_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {

            if (UType.ToString().Equals("Admin"))
            {
                Response.Redirect("AdminAccountPage.aspx");
            }
            else if (UType.ToString().Equals("Staff"))
            {
                //Response.Redirect("StaffAccountPage.aspx");
                Response.Redirect("FMAdmin.aspx");//Newly Added byCVR
            }
            else
            {
                Response.Redirect("MyAccountPage1.aspx");
            }
        }

        private void makegrid()
        {

            DataTable dt = new DataTable();

            dt.Columns.Add("Path", typeof(string));
            dt.Columns.Add("Reference", typeof(string));
            ViewState["Details"] = dt;
            grReference.DataSource = dt;
            grReference.DataBind();

            DataTable dt1 = new DataTable();

            dt1.Columns.Add("Path", typeof(string));
            dt1.Columns.Add("Reference", typeof(string));
            ViewState["Detail"] = dt1;
            grReference0.DataSource = dt1;
            grReference0.DataBind();
        }
        private void Fill_Grid()
        {
            //try
            //{
                              
                SqlConnection con = new SqlConnection();
                con.ConnectionString = myConnectionString;
                con.Open();
                string cmdText = "SELECT   [Description]   ,[Ref_Path]  FROM [dbo].[references] where [RUser_ID] ='" + uid.ToString().Trim() + "' and  [RMin_ID] ='" + from_min.ToString().Trim() + "' and ([Ref_Path] not LIKE 'NULL')";
                SqlCommand cmd = new SqlCommand(cmdText, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!reader[0].ToString().Trim().Equals(""))
                    {

                        DataTable dt = (DataTable)ViewState["Details"];
                        dt.Rows.Add(reader[1], reader[0]);
                        ViewState["Details"] = dt;
                        grReference.DataSource = dt;
                        grReference.DataBind();
                    }

                }

                con.Close();
                con.Dispose();
                cmd.Dispose();

                con = new SqlConnection();
                con.ConnectionString = myConnectionString;
                con.Open();
                cmdText = "SELECT   [Description]   ,[Ref_Path]  FROM [dbo].[reference1] where [RUser_ID] ='" + uid.ToString().Trim() + "' and  [RMin_ID] ='" + from_min.ToString().Trim() + "' and ([Ref_Path] not LIKE 'NULL')";
                cmd = new SqlCommand(cmdText, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!reader[0].ToString().Trim().Equals(""))
                    {
                        DataTable dt = (DataTable)ViewState["Details"];
                        dt.Rows.Add(reader[1], reader[0]);
                        ViewState["Details"] = dt;
                        grReference.DataSource = dt;
                        grReference.DataBind();
                    }
                }

                con.Close();
                con.Dispose();
                cmd.Dispose();
                con = new SqlConnection();
                con.ConnectionString = myConnectionString;

                con.Open();
                cmdText = "SELECT   [Description]   ,[Ref_Path]  FROM [dbo].[reference2] where [RUser_ID] ='" + uid.ToString().Trim() + "' and  [RMin_ID] ='" + from_min.ToString().Trim() + "' and ([Ref_Path] not LIKE 'NULL')";
                cmd = new SqlCommand(cmdText, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!reader[0].ToString().Trim().Equals(""))
                    {

                        DataTable dt = (DataTable)ViewState["Details"];
                        dt.Rows.Add(reader[1], reader[0]);
                        ViewState["Details"] = dt;
                        grReference.DataSource = dt;
                        grReference.DataBind();
                    }

                }

                con.Close();
                con.Dispose();
                cmd.Dispose();
                this.grReference.Columns[0].Visible = false;

                DataTable dataTable = ViewState["Details"] as DataTable;
                //if (dataTable != null)
                //{
                //    DataView dataView = new DataView(dataTable);
                //    dataView.Sort = "Path Asc";
                //    grReference.DataSource = dataView;
                //    grReference.DataBind();
                //}
                con = new SqlConnection();
                con.ConnectionString = myConnectionString;
                con.Open();
                cmdText = "SELECT   [Description3]   ,[Ref_Path3]  FROM [dbo].[Annexure] where [RUser_ID] ='" + uid.ToString().Trim() + "' and  [RMin_ID] ='" + from_min.ToString().Trim() + "'";
                cmd = new SqlCommand(cmdText, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!reader[0].ToString().Trim().Equals(""))
                    {

                        DataTable dt = (DataTable)ViewState["Detail"];
                        dt.Rows.Add(reader[1], reader[0]);
                        ViewState["Detail"] = dt;
                        grReference0.DataSource = dt;
                        grReference0.DataBind();
                    }

                }

                con.Close();
                con.Dispose();
                cmd.Dispose();
                this.grReference0.Columns[0].Visible = false;
            //}
            //catch (NullReferenceException ex)
            //{
            //    Response.Redirect("LoginPage.aspx");
            //}

        }


        protected void grReference_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            e.Row.Cells[0].Visible = false;
        }
        protected void grReference_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        protected void grReference_SelectedIndexChanged(object sender, EventArgs e)
        {





        }

        protected void grReference_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void grReference_SelectedIndexChanged2(object sender, EventArgs e)
        {

        }
    }
}