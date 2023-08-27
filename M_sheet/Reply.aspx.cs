using System;
using System.Data;
using System.Linq;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Officer_Details;
using System.Web.SessionState;
namespace M_sheet
{
    public partial class Reply : System.Web.UI.Page
    {
        string myConnectionString = ConfigurationManager.ConnectionStrings["M_SConnection"].ConnectionString;
        String[,] array = new String[5, 2];
        private string uid;
        private string Usertype;
        private string from_Id ;
        private string from_Min;
        private string Toid;
        private string Area;
        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
          
                lblDate.Text = DateTime.Today.ToString("dd MM yyyy");           
                   btnSubmit.Visible = true;
                   if (Request.Cookies["LocalComputer"] != null)
                   {
                       HttpCookie _cookie = Request.Cookies["LocalComputer"];
                       uid = _cookie["OfficialNo"].ToString().Trim();
                       Usertype = _cookie["User_Type"].ToString().Trim();
                       Toid = _cookie["OfficialNo"].ToString().Trim();
                       Area = _cookie["Area"].ToString().Trim();
                   }
                   if (Request.Cookies["LocalComputer3"] != null)
                   {
                       HttpCookie _cookie = Request.Cookies["LocalComputer3"];
                       this.from_Min = _cookie["From_Min"].ToString().Trim();
                       from_Id = _cookie["From_User"].ToString().Trim();

                   }


             if (!IsPostBack)
                {
                    dptype.SelectedIndex = 0;
                    //this.btnclose.Attributes.Add("OnClick", "self.close()");

                    if ((uid == "NRX465") || (uid == "NRX417") || (uid == "NRX254") || (uid == "NRX524"))
                    {
                        Button1.Visible = true;
                        Button2.Visible = true;
                    }
                    if((uid == "NRX417")||(uid == "NRX524"))
                    {
                        Button3.Visible = true;
                        Button4.Visible = true;
                    }

                    //string Toid = Session["To"].ToString().Trim();
                    //string from_Id = Session["From_User"].ToString().Trim();
                    //string from_Min = Session["From_Min"].ToString().Trim();

                    lblRef1.Text = "";
                    lblRef2.Text = "";
                    lblRef3.Text = "";
                    LoadDRPFill();
                    FillBases();
                    loadsignature();
                    checkExist();
                    makegrid();
                }
            //}
            //catch 
            //{
            //    Response.Redirect("LoginPage.aspx");
            //}

        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            lblRef1.Text = "";
            refImage1.Visible = false;
            txtref1.Text = "";
            ImageButton1.Visible = false;

        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            lblRef2.Text = "";
            refImage2.Visible = false;
            txtref2.Text = "";
            ImageButton2.Visible = false;
        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            lblRef3.Text = "";
            refImage3.Visible = false;
            txtref3.Text = "";
            ImageButton3.Visible = false;
        }
        private void FillBases()
        {
            //String ares = Session["Area"].ToString();//This is newly added code for filter areawise
            dpBase.Items.Clear();
            SqlConnection con = new SqlConnection(myConnectionString);
            con.Open();
            //SqlCommand command = new SqlCommand("SELECT [Ship_Establishment]  FROM [M_Sheet].[dbo].[View_Base]", con);
            SqlCommand command = new SqlCommand("SELECT TOP 1000 [Ship_Establishment]  FROM [M_Sheet].[dbo].[View_Base] WHERE [Area]='" + Area + "'", con);//This query should be changed
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


        protected void dpMedium_SelectedIndexChanged1(object sender, EventArgs e)
        {
        
            DRPFill();
            changeMedium();
        }

        private void changeMedium()
        {
            if (dpMedium.Text.Trim() == "English")
            {

                lblMSubId.Text = "M";
                lblMinuteHead.Text = "MINUTE SHEET";
                lblMBy.Visible = true;
                lblMReference.Text = "Reference ";
                lblMReference.Visible = true;

            }
            else
            {
                lblMSubId.Text = "මාස";
                lblMinuteHead.Text = "මාණ්ඩලික සටහන්පත";
                lblMBy.Visible = false;
                lblMReference.Text = "යොමුව";
                lblMReference.Visible = false;
            }
        }

        private void checkExist()
        {
            //try
            //{

            //string Toid = Session["To"].ToString().Trim();
            //string from_Min = Session["From_Min"].ToString().Trim();
            //string from_Id = Session["From_User"].ToString().Trim();
            //string from_min = Session["From_Min"].ToString().Trim();
                string uid = Toid.ToString().Trim();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = myConnectionString;
                con.Open();
                string sqlstring = "select * from dbo.[Min_References] where User_ID='" + from_Id.ToString().Trim() + "' and User_Name='" + uid.ToString().Trim() + "' and  [RespondAllow]=1 and [Responded]=0";
                SqlCommand cmd = new SqlCommand(sqlstring, con);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    return;
                }
                con.Close();
                cmd.Dispose();
                read.Dispose();
                 con = new SqlConnection();
                con.ConnectionString = myConnectionString;
                con.Open();
                 sqlstring = "select * from dbo.Min_Master where RUser_ID='" +from_Id.ToString().Trim() + "' and RMin_ID='" + from_Min.ToString().Trim() + "' and User_ID='" + Toid.ToString().Trim() + "'";
                 cmd = new SqlCommand(sqlstring, con);
                 read = cmd.ExecuteReader();
                while (read.Read())
                {
                    //if (uid == Session["From"].ToString().Trim())
                    //{
                    //    return;
                    //}
                    lblMinuteSheetNo.Text = read["Min_ID"].ToString().Trim();
                    lblMSubId.Text = read["Min_SubID"].ToString().Trim();
                    txtbody.Text = read["Mbody"].ToString().Trim();
                    lblDate.Text = read["Date_Time"].ToString().Trim();
                    dpMedium.Text = read["Medium"].ToString().Trim();
                    btnSubmit.Visible = false;
                }
                con.Close();
                cmd.Dispose();
                read.Dispose();
                con.Open();
                sqlstring = "Select * from dbo.Reference_Master where User_ID='" + uid + "' and Min_ID='" + lblMinuteSheetNo.Text.Trim() + "'";
                cmd = new SqlCommand(sqlstring, con);
                read = cmd.ExecuteReader();
                while (read.Read())
                {

                    txtref1.Text = read["Description"].ToString().Trim();
                    lblRef1.Text  = read["Ref_Path"].ToString().Trim();
                    txtref2.Text = read["Description1"].ToString().Trim();
                    lblRef2.Text = read["Ref_Path1"].ToString().Trim();
                    txtref3.Text = read["Description2"].ToString().Trim();
                    lblRef3.Text = read["Ref_Path2"].ToString().Trim();

                }
                LoadDRPFill();
            //}
            //catch (Exception ex)
            //{
            //    Response.Redirect("LoginPage.aspx");
            //}
        }
        public void loadsignature()
        {
            //try
            //{
                //string uid = Session["User_Name"].ToString().Trim();
            string sqlstr;
            sqlstr = "SELECT imagePath FROM Profile_Master where [Officer_Id]= '" + uid.ToString() + "' ";

                SqlConnection connew = new SqlConnection(myConnectionString);
                SqlCommand cmd = new SqlCommand(sqlstr, connew);
                SqlDataReader reader1;                
                connew.Open();               
                reader1 = cmd.ExecuteReader();
                while (reader1.Read())
                {
                    this.imageSignature.ImageUrl = reader1["imagePath"].ToString().Trim();
                }
                connew.Close();
                connew.Dispose();
                cmd.Dispose();
                reader1.Dispose();
            
          //}
          //  catch (NullReferenceException ex)
          //  {
          //      Response.Redirect("LoginPage.aspx");
          //  }
        }
        public void LoadDRPFill()
        {
            //try
            //{
                //string Toid = Session["To"].ToString().Trim();
                //string from_Min = Session["From_Min"].ToString().Trim();
                //string from_Id = Session["From_User"].ToString().Trim();
                //string from_min = Session["From_Min"].ToString().Trim();
                string bycontent="";
                 
                 OfficerDetails.ReadOffDtls(from_Id.ToString().Trim());
                 string SinhalaOk = OfficerDetails.GetFullNameSin().Trim();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = myConnectionString;
                con.Open();
                string sqlstring = "select * from dbo.Receivers where User_ID='" + from_Id.ToString().Trim() + "' and Min_ID='" + from_Min.ToString().Trim() + "' and User_Name='"+ Toid.ToString().Trim() +"'";
                SqlCommand cmd = new SqlCommand(sqlstring, con);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {                       
                     lblTo.Text = read["ByPerson"].ToString().Trim();
                     bycontent = read["Name"].ToString().Trim();
                     lblTopic.Text = read["Topic"].ToString().Trim();
                }
                con.Close();
                con.Dispose();
                cmd.Dispose();
                read.Dispose();

                if (OfficerDetails.CheckEnglish(from_Min, from_Id) == true)
                {
                    
                    dpMedium.Text = "English";
                    dpBy.Items.Clear();
                    dpBy.ClearSelection();  
                    DpByFill();
                    lblMBy.Visible = true;
                    dpBy.Text = bycontent;
                }
                else
                {
                    dpMedium.Text = "Sinhala";
                    DpByFill();
                    lblMBy.Visible = false;
                    dpBy.Text = bycontent + " විසින්";                    
                    changeMedium();
                }

              
                
            //}
            //catch (NullReferenceException ex)
            //{
            //    Response.Redirect("LoginPage.aspx");
            //}
         }


        public void DRPFill()
        {
            //try
            //{
                //string Toid = Session["To"].ToString().Trim();
                //string from_Min = Session["From_Min"].ToString().Trim();
                //string from_Id = Session["From_User"].ToString().Trim();
                //string from_min = Session["From_Min"].ToString().Trim();
                //OfficerDetails officer = new OfficerDetails();
                //officer.ReadOffDtls(from_Id.ToString().Trim());
                //string SinhalaOk = officer.GetFullNameSin().Trim();
                //SqlConnection con = new SqlConnection();
                //con.ConnectionString = myConnectionString;
                //con.Open();
                //string sqlstring = "select * from dbo.Receivers where User_ID='" + from_Id.ToString().Trim() + "' and Min_ID='" + from_Min.ToString().Trim() + "' and User_Name='" + Toid.ToString().Trim() + "'";
                //SqlCommand cmd = new SqlCommand(sqlstring, con);
                //SqlDataReader read = cmd.ExecuteReader();
                //while (read.Read())
                //{
                //    lblTo.Text = read["ByPerson"].ToString().Trim();
                //    dpBy.Text = read["Name"].ToString().Trim();
                //    lblTopic.Text = read["Topic"].ToString().Trim();
                //}
                //con.Close();
                //con.Dispose();
                //cmd.Dispose();
                //read.Dispose();

                DpByFill();
                    changeMedium();
               

            //}
            //catch (NullReferenceException ex)
            //{
            //    Response.Redirect("LoginPage.aspx");
            //}
        }
        protected void TextBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private string Get_MinuteSub_ID(string UID, string MinID)
        {

            int count=0;
            SqlConnection newcon = new SqlConnection();
            newcon.ConnectionString = myConnectionString;
            newcon.Open();
            string sqlText = "Select * from dbo.Min_Master where [RUser_ID]='" + UID + "' and [RMin_ID]='" + MinID + "' ";
            SqlCommand cmds = new SqlCommand(sqlText, newcon);
            SqlDataReader readers = cmds.ExecuteReader();
            while (readers.Read())
            {
                count = count + 1;
            }
            newcon.Close();
            cmds.Dispose();
            newcon.Dispose();
            readers.Dispose();
            return (count+1).ToString();
         
          
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //try
            //{

                //string from_Min = Session["From_Min"].ToString().Trim();
                //string from_Id = Session["From_User"].ToString().Trim();
                //string uid = Session["User_Name"].ToString().Trim();
                lblinfo.Text = "";
                if (txtbody.Text.Trim().Equals(""))
                {
                    lblinfo.Text = "Minute sheet cannot be empty";
                    return;
                }


                int referencestatus = referenceAvailable();
                if (referencestatus == 0) { return; }


                float lastno;
                string insertCmdText;
               

                SqlConnection GetData = new SqlConnection();
                GetData.ConnectionString = myConnectionString;
                GetData.Open();
                insertCmdText = "Select * from [Min_Master] where [User_ID]='" + uid + "' and [Min_ID]='" + lblMinuteSheetNo.Text + "' ";
                SqlCommand cmds = new SqlCommand(insertCmdText, GetData);
                SqlDataReader readers = cmds.ExecuteReader();
                while (readers.Read())
                {
                    lblinfo.Text = "Minutes is already  forwarded";
                    return;
                }

                string MinSub_Id = Get_MinuteSub_ID(from_Id.ToString().Trim(), from_Min.ToString().Trim());
                lastno = Convert.ToInt16(MinSub_Id);
                MinSub_Id = string.Format("{0:00}", lastno);
                float msub = lastno;
                MinSub_Id = lblMSubId.Text.Trim() + MinSub_Id;


                string head = lblTopic.Text;
                head = head.Replace("'", "''");
                lblTopic.Text = head;
                string body = txtbody.Text;
                body = body.Replace("'", "''");
                txtbody.Text = body;
                lastno = GetLast_Min_No();
                string output = string.Format("{0:00000}", lastno);
                lblMinuteSheetNo.Text = output.ToString();

                string mindate = DateTime.Now.ToShortDateString();
                SqlConnection conGetData = new SqlConnection();
                conGetData.ConnectionString = myConnectionString;
                conGetData.Open();

                insertCmdText = "INSERT INTO [dbo].[Min_Master] ([User_ID] ,[Min_ID] ,[Min_SubID] ,[Topic] ,[ByPerson],[Date_Time] ,[MBody],[Reply_Min],[RUser_ID],[RMin_ID],[Order_Follow],[Submit],[SubmitTime],[MConfirm],[ConfirmTime],Sub_Id,[Medium])  values('" + uid.Trim() + "','" + output + "',N'" + MinSub_Id + "',N'" + lblTopic.Text.ToUpper() + "',N'" + dpBy.Text.Trim() + "',CONVERT(datetime, GETDATE()),N'" + txtbody.Text + "',1,'" + from_Id.ToString().Trim() + "','" + from_Min.ToString().Trim() + "',0,1,CONVERT(date, GETDATE()),1,CONVERT(date, GETDATE())," + msub + ",'"+ dpMedium.Text.Trim() +"')";
                SqlCommand insertCmd = new SqlCommand(insertCmdText, conGetData);
                insertCmd.ExecuteNonQuery();
                conGetData.Close();
                conGetData.Dispose();
                insertCmd.Dispose();



                Saverefpaths();

                conGetData = new SqlConnection();
                conGetData.ConnectionString = myConnectionString;
                conGetData.Open();
            //Following Code is Original and Updated on 23rd Aug 2019
                //insertCmdText = "UPDATE [dbo].[Min_References] set [Responded]=1, RespondTime =CONVERT(datetime, GETDATE()) where [Min_Id] ='" + from_Min.ToString().Trim() + "'and [User_ID]='" + from_Id.ToString().Trim() + "' and User_Name='" + uid.ToString().Trim() + "' ";

                insertCmdText = "UPDATE [dbo].[Min_References] set [Responded]=1, RespondTime =CONVERT(datetime, GETDATE()),[Archive]=1 where [Min_Id] ='" + from_Min.ToString().Trim() + "'and [User_ID]='" + from_Id.ToString().Trim() + "' and User_Name='" + uid.ToString().Trim() + "' ";
                insertCmd = new SqlCommand(insertCmdText, conGetData);
                insertCmd.ExecuteNonQuery();
                conGetData.Close();
                conGetData.Dispose();
                insertCmd.Dispose();


                GetData = new SqlConnection();
                GetData.ConnectionString = myConnectionString;
                GetData.Open();
                insertCmdText = "Select * from  [dbo].[Min_References] where [Min_Id] ='" + from_Min.ToString().Trim() + "'and [User_ID]='" + from_Id.ToString().Trim() + "' and User_Name='" + uid.ToString().Trim() + "' and RespondAllow=1 ";
                cmds = new SqlCommand(insertCmdText, GetData);
                readers = cmds.ExecuteReader();
                int nextOrder = 0;
                while (readers.Read())
                {
                    nextOrder = nextOrder + 1;
                }
                SaveReference();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Minute Sheet reply has been forwarded');", true);
            //}
              
            //catch (NullReferenceException ex)
            //{
            //    Response.Redirect("LoginPage.aspx");
            //}
        }
        private void SaveReference()
        {
            if (Session["Leavelist"] != null)
            {
                array = Session["Leavelist"] as String[,];
            }
            //String uid = Session["User_Name"].ToString().Trim();
            //String from_Min = Session["From_Min"].ToString().Trim();
            //String from_Id = Session["From_User"].ToString().Trim();

            SqlConnection GetData = new SqlConnection();
            GetData.ConnectionString = myConnectionString;
            GetData.Open();
            string insertCmdText = "Select * from  [dbo].[Min_References] where [Min_Id] ='" + from_Min.ToString().Trim() + "'and [User_ID]='" + from_Id.ToString().Trim() + "'  and RespondAllow=1 and Displayonly=0";
            SqlCommand cmds = new SqlCommand(insertCmdText, GetData);
            SqlDataReader readers = cmds.ExecuteReader();
            float nextOrder = 0;
            while (readers.Read())
            {
           //     nextOrder = Convert.ToInt32(readers["Order"].ToString());
                nextOrder = nextOrder + 1;
            }
            cmds.Dispose();
            readers.Dispose();
            string output = from_Min;
           
            string fuid = from_Id;
            string RunQuery="";

            string User_Name="";
            Boolean forwarded = false;
            //Reading DataTable Rows Column Value using Column Index Number
            DataTable dt1 = (DataTable)ViewState["Details"];
            foreach (DataRow oRecord in dt1.Rows)
            {
                nextOrder = (float)(nextOrder + 1);
                User_Name = oRecord[0].ToString();
                string Sname = oRecord[1].ToString();
                SqlConnection NewCon = new SqlConnection();
                NewCon.ConnectionString = myConnectionString;
                NewCon.Open();
                RunQuery = "INSERT INTO [dbo].[Min_References] ([Min_Id],[User_ID],[User_Name] ,[Name] ,[Read_Not],[Order] ,[Res_Required],[Responded],[RespondAllow])  values('" + output.Trim() + "','" + fuid.Trim() + "',N'" + User_Name.Trim() + "',N'" + Sname.Trim() + "',0," + nextOrder + ",1,0,1)";
                SqlCommand newCmd = new SqlCommand(RunQuery, NewCon);
                newCmd.ExecuteNonQuery();
                NewCon.Close();
                NewCon.Dispose();
                newCmd.Dispose();
                forwarded = true;
            }
            for (int i = 0; i <= 4; i++)
            {
                if (array.GetValue(i, 0) != null)
                {
                    string User_Name1 = array[i, 0];
                    string Sname = array[i, 1];
                    SqlConnection NewCon = new SqlConnection();
                    NewCon.ConnectionString = myConnectionString;
                    NewCon.Open();

                     RunQuery = "INSERT INTO [dbo].[Min_References] ([Min_Id],[User_ID],[User_Name] ,[Name] ,[Read_Not],[Order] ,[Res_Required],[Responded],[RespondAllow],[Displayonly])  values('" + output.Trim() + "','" + uid.Trim() + "',N'" + User_Name1.Trim() + "',N'" + Sname.Trim() + "',0, 0 , 0 ,1,0,1)";
                    SqlCommand newCmd = new SqlCommand(RunQuery, NewCon);
                    newCmd.ExecuteNonQuery();
                    NewCon.Close();
                    NewCon.Dispose();
                    newCmd.Dispose();

                }

            }
            SqlConnection NewCon1 = new SqlConnection();
            NewCon1.ConnectionString = myConnectionString;
            NewCon1.Open();
            if (forwarded == true)
            {

                RunQuery = "UPDATE [dbo].[Min_References] SET  [Order]=[Order]+1 where [Min_Id]='" + output.Trim() + "' and [User_ID] ='" + fuid.Trim() + "'  and [RespondAllow]=0 and Displayonly=0";
               
            }
            else
            {
                RunQuery = "UPDATE [dbo].[Min_References] SET  [RespondAllow]=1 where [Min_Id]='" + output.Trim() + "' and [User_ID] ='" + fuid.Trim() + "'  and [RespondAllow]=0 and  [Order]=" + (nextOrder + 1) + " and Displayonly=0 ";
               
            }

            SqlCommand newCmd1 = new SqlCommand(RunQuery, NewCon1);
            newCmd1.ExecuteNonQuery();

            NewCon1.Close();
            NewCon1.Dispose();
            newCmd1.Dispose();


                //RunQuery = "UPDATE [dbo].[Min_References] set [RespondAllow]=1 where [Min_Id] ='" + output.Trim() + "'and [User_ID]='" + fuid.Trim() + "' and [Order]=" + (nextOrder + 1) + " ";
                //NewCon1 = new SqlConnection();
                //NewCon1.ConnectionString = myConnectionString;
                //NewCon1.Open();
                //newCmd1 = new SqlCommand(RunQuery, NewCon1);
                //newCmd1.ExecuteNonQuery();

                //NewCon1.Close();
                //NewCon1.Dispose();
                //newCmd1.Dispose();
           
        }
        private int GetLast_Min_No()
        {

            //string uid = Session["User_Name"].ToString().Trim();
                
                int Sr_No = 0;
                string selectSQL = "SELECT [Minute]  FROM [dbo].[Last_No] where User_Name='" + uid + "'";
                SqlConnection con = new SqlConnection(myConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand(selectSQL, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Sr_No = Convert.ToInt32(reader["Minute"]);
                }
                if (Sr_No == 0)
                {
                    Sr_No = 1;
                    string strsql = "Insert into  [dbo].[Last_No]   ( Minute,User_Name) values (1,'" + uid + "' )";
                    SqlConnection ncon = new SqlConnection(myConnectionString);
                    ncon.Open();
                    SqlCommand ncmd = new SqlCommand(strsql, ncon);
                    ncmd.ExecuteNonQuery();
                    ncon.Close();
                }
                else
                {
                    Sr_No = Sr_No + 1;
                    string strsql = "Update [dbo].[Last_No]  set Minute=Minute+1 where User_Name='" + uid + "' ";
                    SqlConnection ncon = new SqlConnection(myConnectionString);
                    ncon.Open();
                    SqlCommand ncmd = new SqlCommand(strsql, ncon);
                    ncmd.ExecuteNonQuery();
                    ncon.Close();
                }

                con.Close();
                con.Dispose();
                cmd.Dispose();
                return Sr_No;
             
          
        }

      
      

        protected void btnUpload1_Click(object sender, EventArgs e)
        {
            //try
            //{
                //string Toid = Session["To"].ToString().Trim();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = myConnectionString;
                con.Open();

                if (fuRef1.PostedFile != null)
                {

                    var folder = Server.MapPath("~/References/" + Toid);
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    
                    string FileName = Path.GetFileName(fuRef1.PostedFile.FileName);
                    //Save files to images folder
                    fuRef1.SaveAs(folder + "/" + FileName);
                    lblRef1.Text = "References/" +  Toid + "/"+ FileName;
                    this.refImage1.ImageUrl = "~/Images/Ok.png";
                    this.refImage1.Visible = true;
                }
            //}
            //catch (Exception ex)
            //{
            //    Response.Redirect("LoginPage.aspx");
            //}
        }
        protected void btnUpload2_Click(object sender, EventArgs e)
        {
            //try
            //{
                //string Toid = Session["To"].ToString().Trim();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = myConnectionString;
                con.Open();

                if (fuRef2.PostedFile != null)
                {

                    var folder = Server.MapPath("~/References/" + Toid);
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }

                    string FileName = Path.GetFileName(fuRef2.PostedFile.FileName);
                    //Save files to images folder
                    fuRef2.SaveAs(folder + "/" + FileName);
                    lblRef2.Text= "References/" + Toid + "/" + FileName;
                    this.refImage2.ImageUrl = "~/Images/Ok.png";
                    this.refImage2.Visible = true;
                }
            //}
            //catch (NullReferenceException ex)
            //{
            //    Response.Redirect("LoginPage.aspx");
            //}
        }


        protected void btnUpload3_Click(object sender, EventArgs e)
        {
            //try
            //{
                //string Toid = Session["To"].ToString().Trim();
                if (fuRef3.PostedFile != null)
                {

                    var folder = Server.MapPath("~/References/" + Toid);
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }

                    string FileName = Path.GetFileName(fuRef3.PostedFile.FileName);
                    //Save files to images folder
                    fuRef3.SaveAs(folder + "/" + FileName);
                    lblRef3.Text = "References/" + Toid + "/" + FileName;
                    this.refImage3.ImageUrl = "~/Images/Ok.png";
                    this.refImage3.Visible = true;
                }
            //}
            //catch (NullReferenceException ex)
            //{
            //    Response.Redirect("LoginPage.aspx");
            //}
        }
        private void Saverefpaths()
        {
            //string uid = Session["User_Name"].ToString().Trim();
            string output = lblMinuteSheetNo.Text.Trim();
           
           int referencestatus = referenceAvailable();
            if (referencestatus != 3)
            {
                SqlConnection conGetData = new SqlConnection();
                conGetData.ConnectionString = myConnectionString;
                conGetData.Open();
                string insertCmdText = "INSERT INTO [dbo].[Reference_Master] ([Min_ID],[User_ID] ,[Ref_Order] ,[Description],[Ref_path],[Description1],[Ref_path1],[Description2],[Ref_path2] )  values('" + output.Trim() + "','" + uid.Trim() + "',1,N'" + txtref1.Text.Trim() + "','" + lblRef1.Text + "',N'" + txtref2.Text.Trim() + "','" + lblRef2.Text + "',N'" + txtref3.Text.Trim() + "','" + lblRef3.Text + "')";
                SqlCommand insertCmd = new SqlCommand(insertCmdText, conGetData);
                insertCmd.ExecuteNonQuery();
                conGetData.Close();
                conGetData.Dispose();
                insertCmd.Dispose();
            }
        }

        private int referenceAvailable()
        {
            string imageurl = lblRef1.Text;
            string imageur2 = lblRef2.Text;
            string imageur3 = lblRef3.Text;
            int chk = 3;
            //reference 01
            if (!imageurl.Trim().Equals(""))
            {
                if (txtref1.Text.Trim().Equals(""))
                {
                    lblinfo.Text = "Reference1 description cannot be empty";
                    return chk=0;
                }
                else
                {
                    string value = txtref1.Text.Trim();
                    value = value.Replace("'", "''");
                    txtref1.Text = value;
                    chk = 1;
                }
            }
            if (imageurl.Trim().Equals(""))
            {
                if (!txtref1.Text.Trim().Equals(""))
                {
                    //lblinfo.Text = "Reference1 should be upload.";
                    return chk = 1;
                }
            }
            //Refrence 02
            if (!imageur2.Trim().Equals(""))
            {
                if (txtref2.Text.Trim().Equals(""))
                {
                    lblinfo.Text = "Reference2 description cannot be empty";
                    return chk=0;
                }
                else
                {
                    string value = txtref2.Text.Trim();
                    value = value.Replace("'", "''");
                    txtref2.Text = value;
                     chk = 1;
                }
            }
            if (imageur2.Trim().Equals(""))
            {
                if (!txtref2.Text.Trim().Equals(""))
                {
                    //lblinfo.Text = "Reference2 should be upload.";
                    return chk = 1;
                }
            }
            //Refrence 03
            if (!imageur3.Trim().Equals(""))
            {
                if (txtref3.Text.Trim().Equals(""))
                {
                    lblinfo.Text = "Reference3 description cannot be empty";
                    return chk=0;
                }
                else
                {
                    string value = txtref3.Text.Trim();
                    value = value.Replace("'", "''");
                    txtref3.Text = value;
                    chk = 1;
                }
            }
            if (imageur3.Trim().Equals(""))
            {
                if (!txtref3.Text.Trim().Equals(""))
                {
                   // lblinfo.Text = "Reference3 should be upload.";
                    return chk = 1;
                }
            }
            return chk ;
        }

        protected void txtbody_TextChanged(object sender, EventArgs e)
        {

        }

        protected void dptype_TextChanged(object sender, EventArgs e)
        {
        }
        protected void dptype_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillOfficerList();
        }

        protected void dptype_Init(object sender, EventArgs e)
        {
            FillOfficerList();
        }
   
      
        protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
        {
            GridView1.DeleteRow(GridView1.SelectedIndex);
        }
        public void PendingRecordsGridview_RowDeleting(Object sender, GridViewDeleteEventArgs e)
        {

        }
        protected void GridView1_SelectedIndexChanged2(object sender, EventArgs e)
        {
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt1 = (DataTable)ViewState["Details"];
            // DataTable dt = new DataTable();
            if (dt1.Rows.Count > 0)
            {
                dt1.Rows[e.RowIndex].Delete();
                GridView1.DataSource = dt1;
                GridView1.DataBind();
            }
        }
        private void makegrid()
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("OfficialNo", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            ViewState["Details"] = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                for (int i = 0; i <= 4; i++)
                {
                    if (array[i, 0] != null)
                    {
                        count++;
                    }
                }

                  int counta = 0;
                  DataTable dt1 = (DataTable)ViewState["Details"];
                  foreach (DataRow oRecord in dt1.Rows)
                  {
                      counta = counta + 1;
                  }
                  if (count == 1) 
                  {
                     
                      ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You cannot add more than one');", true);
                      return;
                  }
                string str = dltlist.Text.Trim();
                string str1 = dltlist.SelectedItem.Text;
                DataTable dt = (DataTable)ViewState["Details"];
                bool contains = dt.AsEnumerable().Any(row => str == row.Field<String>("OfficialNo"));

                if (contains.Equals(true))
                {
                    lblinfo.Text = "Officer is  already added";
                    return;
                }

                string selectSQL = "select * from dbo.Profile_Master  where User_Name='" + str + "' and [Onleave]=1 ";
                SqlConnection con = new SqlConnection(myConnectionString);
                SqlCommand cmd = new SqlCommand(selectSQL, con);
                SqlDataReader reader;
                string overseeby = "";
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Selected officer is on leave and Minute will forward be to oversees officer');", true);
                    overseeby = reader["OverSeesBy"].ToString();
                    array[counta, 0] = str;
                    array[counta, 1] = reader["Appointment"].ToString();
                    Session["Leavelist"] = array;

                }

                con.Close();
                cmd.Dispose();
                reader.Dispose();
                con.Open();
                selectSQL = "select * from dbo.Profile_Master  where User_Name='" + overseeby + "' ";
                cmd = new SqlCommand(selectSQL, con);
                reader = cmd.ExecuteReader();
                if (overseeby != "")
                {
                    while (reader.Read())
                    {
                        str = reader["User_Name"].ToString();
                        str1 = reader["Appointment"].ToString();
                    }
                }
                dt.Rows.Add(str, str1);
                ViewState["Details"] = dt;
                GridView1.DataSource = dt;
                GridView1.DataBind();
                this.GridView1.Columns[0].Visible = false;
            }
            catch 
            {
                return;
            }
        }

        private void FillOfficerList()
        {
            string selectSQL;

            if (dptype.Text.Equals("Appointment"))
            {

                dltlist.Items.Clear();
                selectSQL = "SELECT Rank, Name_With_Initial, Appointment,Other_Appointment,User_Name ,Imagepath,Branch+Official_No as ConcatOfficialNo ,[AppointmentSin],[Other_AppointmentSin] ,[RankSin],[Name_With_Initial_Sin]  ,[Officer_Id] FROM Profile_Master where Active=1 and Ship_Establishment ='" + dpBase.SelectedValue + "' order by  Appointment";
                SqlConnection con = new SqlConnection(myConnectionString);
                SqlCommand cmd = new SqlCommand(selectSQL, con);
                SqlDataReader reader;
                string selected = dptype.SelectedValue;
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if ((!reader["Appointment"].ToString().ToUpper().Equals("")) && (dpMedium.Text.Trim() == "English"))
                    {
                        ListItem newItem = new ListItem();
                        newItem.Text = reader["Appointment"].ToString().ToUpper();
                        newItem.Value = reader["ConcatOfficialNo"].ToString();
                        dltlist.Items.Add(newItem);
                    }
                    else if ((dpMedium.Text.Trim() == "Sinhala") && (!reader["Appointment"].ToString().ToUpper().Trim().Equals("")))
                    {
                        ListItem newItem = new ListItem();
                        newItem.Text = reader["AppointmentSin"].ToString().ToUpper();
                        newItem.Value = reader["ConcatOfficialNo"].ToString();
                        dltlist.Items.Add(newItem);
                    }
                    else
                    {
                        lblinfo.Text = "Your details should update";
                    }
                }

                con.Close();
                cmd.Dispose();
                reader.Dispose();
            }
            else if (dptype.Text.Equals("Name"))
            {
                dltlist.Items.Clear();
                selectSQL = "SELECT Rank, Name_With_Initial, Appointment,Other_Appointment,User_Name ,[imagePath] ,[AppointmentSin],[Other_AppointmentSin] ,[RankSin],[Name_With_Initial_Sin]  ,[Officer_Id] FROM Profile_Master where Active=1 and Ship_Establishment ='" + dpBase.SelectedValue + "' order by  Rank";
                SqlConnection con = new SqlConnection(myConnectionString);
                SqlCommand cmd = new SqlCommand(selectSQL, con);
                SqlDataReader reader;
                string selected = dptype.SelectedValue;
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (dpMedium.Text.Trim() == "English")
                    {
                        ListItem newItem = new ListItem();
                        newItem.Text = reader["Rank"].ToString().ToUpper() + "  " + reader["Name_With_Initial"].ToString().ToUpper();
                        newItem.Value = reader["Officer_Id"].ToString();
                        dltlist.Items.Add(newItem);
                    }
                    else
                    {
                        ListItem newItem = new ListItem();
                        newItem.Text = reader["RankSin"].ToString().ToUpper() + "  " + reader["Name_With_Initial_Sin"].ToString().ToUpper();
                        newItem.Value = reader["Officer_Id"].ToString();
                        dltlist.Items.Add(newItem);

                    }
                }
                con.Close();
                cmd.Dispose();
                reader.Dispose();

            }
            else if (dptype.Text.Equals("Other Appointment"))
            {

                dltlist.Items.Clear();
                selectSQL = "SELECT Rank, Name_With_Initial, Appointment,Other_Appointment,User_Name ,imagePath ,[AppointmentSin],[Other_AppointmentSin] ,[RankSin],[Name_With_Initial_Sin]  ,[Officer_Id] FROM Profile_Master where Active=1 and Ship_Establishment ='" + dpBase.SelectedValue + "' order by  Other_Appointment";
                SqlConnection con = new SqlConnection(myConnectionString);
                SqlCommand cmd = new SqlCommand(selectSQL, con);
                SqlDataReader reader;
                string selected = dptype.SelectedValue;
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!reader["Other_Appointment"].ToString().ToUpper().Equals(""))
                    {
                        ListItem newItem = new ListItem();
                        newItem.Text = reader["Other_AppointmentSin"].ToString().ToUpper();
                        newItem.Value = reader["Officer_Id"].ToString();
                        dltlist.Items.Add(newItem);
                    }
                }
                reader.Close();
                con.Close();
                con.Dispose();
                reader.Dispose();
                cmd.Dispose();
            }

        }

        //private void FillOfficerList()
        //{
        //    string selectSQL;

        //    if (dptype.Text.Equals("Appointment"))
        //    {

        //        dltlist.Items.Clear();
        //        selectSQL = "SELECT Rank, Name_With_Initial, Appointment,Other_Appointment,User_Name ,imagePath FROM Profile_Master where Active=1 and Ship_Establishment ='" + dpBase.SelectedValue + "' order by  Appointment";
        //        SqlConnection con = new SqlConnection(myConnectionString);
        //        SqlCommand cmd = new SqlCommand(selectSQL, con);
        //        SqlDataReader reader;
        //        string selected = dptype.SelectedValue;
        //        con.Open();
        //        reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            if (!reader["Appointment"].ToString().ToUpper().Equals(""))
        //            {
        //                ListItem newItem = new ListItem();
        //                newItem.Text = reader["Appointment"].ToString().ToUpper();
        //                newItem.Value = reader["User_Name"].ToString();
        //                dltlist.Items.Add(newItem);
        //            }
        //        }

        //        con.Close();
        //        cmd.Dispose();
        //        reader.Dispose();
        //    }
        //    else if (dptype.Text.Equals("Name"))
        //    {
        //        dltlist.Items.Clear();
        //        selectSQL = "SELECT Rank, Name_With_Initial, Appointment,Other_Appointment,User_Name ,imagePath FROM Profile_Master where Active=1 and Ship_Establishment ='" + dpBase.SelectedValue + "' order by  Rank";
        //        SqlConnection con = new SqlConnection(myConnectionString);
        //        SqlCommand cmd = new SqlCommand(selectSQL, con);
        //        SqlDataReader reader;
        //        string selected = dptype.SelectedValue;
        //        con.Open();
        //        reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            ListItem newItem = new ListItem();
        //            newItem.Text = reader["Rank"].ToString().ToUpper() + "  " + reader["Name_With_Initial"].ToString().ToUpper();
        //            newItem.Value = reader["User_Name"].ToString();
        //            dltlist.Items.Add(newItem);
        //        }
        //        con.Close();
        //        cmd.Dispose();
        //        reader.Dispose();

        //    }
        //    else if (dptype.Text.Equals("Other Appointment"))
        //    {

        //        dltlist.Items.Clear();
        //        selectSQL = "SELECT Rank, Name_With_Initial, Appointment,Other_Appointment,User_Name ,imagePath FROM Profile_Master where Active=1 and Ship_Establishment ='" + dpBase.SelectedValue + "' order by  Other_Appointment";
        //        SqlConnection con = new SqlConnection(myConnectionString);
        //        SqlCommand cmd = new SqlCommand(selectSQL, con);
        //        SqlDataReader reader;
        //        string selected = dptype.SelectedValue;
        //        con.Open();
        //        reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            if (!reader["Other_Appointment"].ToString().ToUpper().Equals(""))
        //            {
        //                ListItem newItem = new ListItem();
        //                newItem.Text = reader["Other_Appointment"].ToString().ToUpper();
        //                newItem.Value = reader["User_Name"].ToString();
        //                dltlist.Items.Add(newItem);
        //            }
        //        }
        //        reader.Close();
        //        con.Close();
        //        con.Dispose();
        //        reader.Dispose();
        //        cmd.Dispose();
        //    }

        //}
     
        //private void FillOfficerList()
        //{
        //    string selectSQL;
        //    dltlist.Items.Clear();
        //    selectSQL = "SELECT Rank, Name_With_Initial, Appointment,Other_Appointment,User_Name ,imagename FROM Profile_Master where Active=1 and Ship_Establishment ='" + dpBase.SelectedValue + "'";

        //    SqlConnection con = new SqlConnection(myConnectionString);
        //    SqlCommand cmd = new SqlCommand(selectSQL, con);
        //    SqlDataReader reader;
        //    string selected = dptype.SelectedValue;
        //    con.Open();
        //    reader = cmd.ExecuteReader();

        //    while (reader.Read())
        //    {
        //        if (dptype.Text.Equals("Appointment"))
        //        {

        //            ListItem newItem = new ListItem();
        //            newItem.Text = reader["Appointment"].ToString().ToUpper();
        //            newItem.Value = reader["User_Name"].ToString();
        //            dltlist.Items.Add(newItem);
        //        }
        //        else if (dptype.Text.Equals("Name"))
        //        {
        //            ListItem newItem = new ListItem();
        //            newItem.Text = reader["Rank"].ToString().ToUpper() + "  " + reader["Name_With_Initial"].ToString().ToUpper();
        //            newItem.Value = reader["User_Name"].ToString();
        //            dltlist.Items.Add(newItem);

        //        }
        //        else if (!reader["Other_Appointment"].ToString().Trim().Equals(""))
        //        {
        //            ListItem newItem = new ListItem();
        //            newItem.Text = reader["Other_Appointment"].ToString().ToUpper();
        //            newItem.Value = reader["User_Name"].ToString();
        //            dltlist.Items.Add(newItem);
        //        }
        //    }
        //    reader.Close();
        //    con.Close();
        //    con.Dispose();
        //    reader.Dispose();
        //    cmd.Dispose();
        //}

        protected void lblby_TextChanged(object sender, EventArgs e)
        {

        }


        public void DpByFill()
        {
            // details = new OfficerDetails();
             OfficerDetails.ReadOffDtls(uid );
            //try
            //{
                if (dpMedium.Text == "English")
                {
                    dpBy.Items.Clear();
                    dpBy.SelectedValue = null;
                    dpBy.DataBind();
                    dpBy.Items.Insert(0, new ListItem(OfficerDetails.GetFullNameEng().Trim()));
                    dpBy.Items.Insert(1, new ListItem(OfficerDetails.GetAppointmentEng().Trim()));
                    dpBy.Items.Insert(2, new ListItem(OfficerDetails.GetOtherAppointmentEng().Trim()));
                }
                else
                {
                    dpBy.Items.Clear();
                    dpBy.DataBind();
                    dpBy.Items.Insert(0, new ListItem(OfficerDetails.GetFullNameSin().Trim()));
                    dpBy.Items.Insert(1, new ListItem(OfficerDetails.GetAppointmentSin().Trim()));
                    dpBy.Items.Insert(2, new ListItem(OfficerDetails.GetOtherAppointmentSin().Trim()));
                }
            //}
            //catch (NullReferenceException ex)
            //{
            //    Response.Redirect("LoginPage.aspx");
            //}
        }

        protected void dpBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            DpByFill();
        }

        protected void Button1_Click(object sender, EventArgs e)//Approved
        {
            txtbody.Text = "Ref M       Approved";
        }

        protected void Button2_Click(object sender, EventArgs e)//Not Approved
        {
            txtbody.Text = "Ref M      Not Approved";
        }

        protected void Button3_Click(object sender, EventArgs e)// Recommanded
        {
            txtbody.Text = "Ref M      Recommended";
        }

        protected void Button4_Click(object sender, EventArgs e)// Not Recommanded
        {
            txtbody.Text = "Ref M      Not Recommended";
        }

        protected void dpBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillOfficerListSecondTime();
        }

        protected void dpBase_TextChanged(object sender, EventArgs e)
        {
            FillOfficerListSecondTime();
        }

        private void FillOfficerListSecondTime()
        {
            string selectSQL;

            if (dptype.Text.Equals("Appointment")&& (dltlist.Text!=""))
            {

                dltlist.Items.Clear();
                selectSQL = "SELECT Rank, Name_With_Initial, Appointment,Other_Appointment,User_Name ,Imagepath,Branch+Official_No as ConcatOfficialNo ,[AppointmentSin],[Other_AppointmentSin] ,[RankSin],[Name_With_Initial_Sin]  ,[Officer_Id] FROM Profile_Master where Active=1 and Ship_Establishment ='" + dpBase.SelectedValue + "' order by  Appointment";
                SqlConnection con = new SqlConnection(myConnectionString);
                SqlCommand cmd = new SqlCommand(selectSQL, con);
                SqlDataReader reader;
                string selected = dptype.SelectedValue;
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if ((!reader["Appointment"].ToString().ToUpper().Equals("")) && (dpMedium.Text.Trim() == "English"))
                    {
                        ListItem newItem = new ListItem();
                        newItem.Text = reader["Appointment"].ToString().ToUpper();
                        newItem.Value = reader["ConcatOfficialNo"].ToString();
                        dltlist.Items.Add(newItem);
                    }
                    else if ((dpMedium.Text.Trim() == "Sinhala") && (!reader["Appointment"].ToString().ToUpper().Trim().Equals("")))
                    {
                        ListItem newItem = new ListItem();
                        newItem.Text = reader["AppointmentSin"].ToString().ToUpper();
                        newItem.Value = reader["ConcatOfficialNo"].ToString();
                        dltlist.Items.Add(newItem);
                    }
                    else
                    {
                        lblinfo.Text = "Your details should update";
                    }
                }

                con.Close();
                cmd.Dispose();
                reader.Dispose();
            }
            else if (dptype.Text.Equals("Name")&& (dltlist.Text!=""))
            {
                dltlist.Items.Clear();
                selectSQL = "SELECT Rank, Name_With_Initial, Appointment,Other_Appointment,User_Name ,[imagePath] ,[AppointmentSin],[Other_AppointmentSin] ,[RankSin],[Name_With_Initial_Sin]  ,[Officer_Id] FROM Profile_Master where Active=1 and Ship_Establishment ='" + dpBase.SelectedValue + "' order by  Rank";
                SqlConnection con = new SqlConnection(myConnectionString);
                SqlCommand cmd = new SqlCommand(selectSQL, con);
                SqlDataReader reader;
                string selected = dptype.SelectedValue;
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (dpMedium.Text.Trim() == "English")
                    {
                        ListItem newItem = new ListItem();
                        newItem.Text = reader["Rank"].ToString().ToUpper() + "  " + reader["Name_With_Initial"].ToString().ToUpper();
                        newItem.Value = reader["Officer_Id"].ToString();
                        dltlist.Items.Add(newItem);
                    }
                    else
                    {
                        ListItem newItem = new ListItem();
                        newItem.Text = reader["RankSin"].ToString().ToUpper() + "  " + reader["Name_With_Initial_Sin"].ToString().ToUpper();
                        newItem.Value = reader["Officer_Id"].ToString();
                        dltlist.Items.Add(newItem);

                    }
                }
                con.Close();
                cmd.Dispose();
                reader.Dispose();

            }
            else if (dptype.Text.Equals("Other Appointment")&& (dltlist.Text!=""))
            {

                dltlist.Items.Clear();
                selectSQL = "SELECT Rank, Name_With_Initial, Appointment,Other_Appointment,User_Name ,imagePath ,[AppointmentSin],[Other_AppointmentSin] ,[RankSin],[Name_With_Initial_Sin]  ,[Officer_Id] FROM Profile_Master where Active=1 and Ship_Establishment ='" + dpBase.SelectedValue + "' order by  Other_Appointment";
                SqlConnection con = new SqlConnection(myConnectionString);
                SqlCommand cmd = new SqlCommand(selectSQL, con);
                SqlDataReader reader;
                string selected = dptype.SelectedValue;
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!reader["Other_Appointment"].ToString().ToUpper().Equals(""))
                    {
                        ListItem newItem = new ListItem();
                        newItem.Text = reader["Other_AppointmentSin"].ToString().ToUpper();
                        newItem.Value = reader["Officer_Id"].ToString();
                        dltlist.Items.Add(newItem);
                    }
                }
                reader.Close();
                con.Close();
                con.Dispose();
                reader.Dispose();
                cmd.Dispose();
            }

        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
            Response.Write("<script language='javascript'> { self.close() }</script>");
        }



    }






}