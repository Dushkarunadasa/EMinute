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
using Officer_Details;
using System.Windows.Forms;
using System.Web.SessionState;
namespace M_sheet
{
    public partial class NewMinute : System.Web.UI.Page
    {
        string myConnectionString = ConfigurationManager.ConnectionStrings["M_SConnection"].ConnectionString;
        String[,] array = new String[5, 2];
        private String User_Name = "";
        private String Usertype = "";
        private string Area = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            lblDate.Text = DateTime.Today.ToString("dd MMM yy");
            lblDate.Text = lblDate.Text.ToUpper();
            if (Request.Cookies["LocalComputer"] != null)
            {
                HttpCookie _cookie = Request.Cookies["LocalComputer"];
                User_Name = _cookie["OfficialNo"].ToString().Trim();
                Usertype = _cookie["User_Type"].ToString().Trim();
                Area = _cookie["Area"].ToString().Trim();
            }


            if ((User_Name == "NRX465") || (User_Name == "NRX417") || (User_Name == "NRX254") || (User_Name == "NRX524"))
            {
                Button1.Visible = true;
                Button2.Visible = true;
            }
            if ((User_Name == "NRX417") || (User_Name == "NRX524"))
            {
                Button3.Visible = true;
                Button4.Visible = true;
            }

            if (IsPostBack == false)
            {
                dptype.SelectedIndex = 0;
                lblRef1.Text = "";
                lblRef2.Text = "";
                lblRef3.Text = "";
                DRPFill();
                FillBases();
                // FillOfficerList();
                loadsignature();
                makegrid();
                loadpendingMinutes();
                FillGroup();
                VisbileofDel();


            }
            btnConfrim.Enabled = false;

            if (Usertype.Equals("Authorized User"))
            {
                btnConfrim.Visible = false;
            }
            else if (Usertype.Equals("Staff"))
            {
                btnConfrim.Visible = false;
            }
            else
            {
                btnConfrim.Visible = true;
            }


        }
        private void FillBases()
        {
            try
            {
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

            catch (Exception ex)
            {
                if (ex.ToString() == "")
                {
                    MessageBox.Show(ex.ToString());
                    lblinfo.Text = "Error : " + ex.Message;
                }
            }
        }
        private void VisbileofDel()
        {
            if (!lblRef1.Text.Equals(""))
            {
                ImageButton1.Visible = true;
            }
            else
            {
                ImageButton1.Visible = false;
            }
            if (!lblRef2.Text.Equals(""))
            {
                ImageButton2.Visible = true;
            }
            else
            {
                ImageButton2.Visible = false;
            }
            if (!lblRef3.Text.Equals(""))
            {

                ImageButton3.Visible = true;
            }
            else
            {
                ImageButton3.Visible = false;
            }

        }
        private void FillGroup()
        {

            string selectSQL;
            DDGroup.Items.Clear();
            selectSQL = "SELECT Distinct   [GroupName]    FROM [dbo].[Groups]where [User_ID]  ='" + User_Name.ToString().Trim() + "'";

            SqlConnection con = new SqlConnection(myConnectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataReader reader;

            con.Open();
            reader = cmd.ExecuteReader();

            ListItem newItem = new ListItem();
            newItem.Text = "--Groups--";
            newItem.Value = "--Groups--";
            DDGroup.Items.Add(newItem);
            while (reader.Read())
            {


                newItem = new ListItem();
                newItem.Text = reader["GroupName"].ToString().ToUpper();
                newItem.Value = reader["GroupName"].ToString();
                DDGroup.Items.Add(newItem);
            }

        }

        private void loadpendingMinutes()
        {



            SqlConnection con = new SqlConnection();
            con.ConnectionString = myConnectionString;
            con.Open();
            string cmdText = "SELECT [Min_ID]       ,[Topic]         ,[Date_Time]        FROM [dbo].[Min_Master] where [MConfirm]=0 and [User_ID]='" + User_Name + "' Order By [Min_ID] ASC";
            SqlCommand cmd = new SqlCommand(cmdText, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GVSubmit.DataSource = ds;
            GVSubmit.DataBind();
            con.Close();

        }
        public void loadsignature()
        {


            OfficerDetails.ReadOffDtls(User_Name);


            string sqlstr;
            sqlstr = "SELECT imagepath FROM Profile_Master where Officer_Id= '" + OfficerDetails.getConcatOfficialNo() + "'";

            SqlConnection connew = new SqlConnection(myConnectionString);
            SqlCommand cmd = new SqlCommand(sqlstr, connew);
            SqlDataReader reader1;
            connew.Open();
            reader1 = cmd.ExecuteReader();
            while (reader1.Read())
            {

                this.imageSignature.ImageUrl = reader1["imagepath"].ToString().Trim();

            }
            connew.Close();
            connew.Dispose();
            cmd.Dispose();
            reader1.Dispose();

        }
        public void DRPFill()
        {

            OfficerDetails.ReadOffDtls(User_Name);

            if (dpMedium.Text == "English")
            {
                dpBy.Items.Clear();
                dpBy.DataBind();
                dpBy.Items.Insert(0, new ListItem(OfficerDetails.GetFullNameEng().Trim()));
                if (OfficerDetails.GetAppointmentEng().Trim() != "")
                {
                    dpBy.Items.Insert(1, new ListItem(OfficerDetails.GetAppointmentEng().Trim()));
                }
                if (OfficerDetails.GetOtherAppointmentEng().Trim() != "")
                {
                    dpBy.Items.Insert(2, new ListItem(OfficerDetails.GetOtherAppointmentEng().Trim()));
                }
            }
            else
            {
                dpBy.Items.Clear();
                dpBy.DataBind();
                if (OfficerDetails.GetFullNameSin().Trim() != "")
                {
                    dpBy.Items.Insert(0, new ListItem(OfficerDetails.GetFullNameSin().Trim()));
                }
                if (OfficerDetails.GetAppointmentSin().Trim() != "")
                {
                    dpBy.Items.Insert(1, new ListItem(OfficerDetails.GetAppointmentSin().Trim()));
                }
                if (OfficerDetails.GetOtherAppointmentSin().Trim() != "")
                {
                    dpBy.Items.Insert(2, new ListItem(OfficerDetails.GetOtherAppointmentSin().Trim()));
                }
            }

        }

        protected void TextBox3_TextChanged(object sender, EventArgs e)
        {
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
        private void FillOfficerList()
        {
            string selectSQL;

            if (dptype.Text.Equals("Appointment"))
            {

                dltlist.Items.Clear();
                //selectSQL = "SELECT Rank, Name_With_Initial, Appointment,Other_Appointment,User_Name ,Imagepath,Branch+Official_No as ConcatOfficialNo ,[AppointmentSin],[Other_AppointmentSin] ,[RankSin],[Name_With_Initial_Sin]  ,[Officer_Id] FROM Profile_Master where Active=1 and Ship_Establishment ='" + dpBase.SelectedValue + "' order by  Appointment";
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
                        if (reader["AppointmentSin"].ToString().ToUpper().Trim() != "")
                        {
                            ListItem newItem = new ListItem();
                            newItem.Text = reader["AppointmentSin"].ToString().ToUpper();
                            newItem.Value = reader["ConcatOfficialNo"].ToString();
                            dltlist.Items.Add(newItem);
                        }
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
                        if (reader["Name_With_Initial_Sin"].ToString().ToUpper().Trim() != "")
                        {
                            ListItem newItem = new ListItem();
                            newItem.Text = reader["RankSin"].ToString().ToUpper() + "  " + reader["Name_With_Initial_Sin"].ToString().ToUpper();
                            newItem.Value = reader["Officer_Id"].ToString();
                            dltlist.Items.Add(newItem);
                        }

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
                    if (!reader["Other_Appointment"].ToString().ToUpper().Equals("") && (dpMedium.Text.Trim() == "English"))
                    {
                        if (reader["Other_Appointment"].ToString().ToUpper().Trim() != "")
                        {
                            ListItem newItem = new ListItem();
                            newItem.Text = reader["Other_Appointment"].ToString().ToUpper();
                            newItem.Value = reader["Officer_Id"].ToString();
                            dltlist.Items.Add(newItem);
                        }
                    }
                    else
                    {
                        if (reader["Other_AppointmentSin"].ToString().ToUpper().Trim() != "")
                        {
                            ListItem newItem = new ListItem();
                            newItem.Text = reader["Other_AppointmentSin"].ToString().ToUpper();
                            newItem.Value = reader["Officer_Id"].ToString();
                            dltlist.Items.Add(newItem);
                        }

                    }
                }
                reader.Close();
                con.Close();
                con.Dispose();
                reader.Dispose();
                cmd.Dispose();
            }

        }

        protected void dptype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                FillOfficerList();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                lblinfo.Text = "Error : " + ex.Message;
            }
        }



        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int count = 0;
            for (int i = 0; i <= 4; i++)
            {
                if (array[i, 0] != null)
                {
                    count++;
                }
            }
            lblinfo.Text = "";
            try
            {


                OfficerDetails.ReadOffDtls(User_Name);
                string User_ID = OfficerDetails.getConcatOfficialNo();
                if (!DDGroup.Text.Trim().Equals("--Groups--"))
                {
                    if (!DDGroup.Text.Trim().Equals(""))
                    {

                        string groupname = DDGroup.Text.Trim();
                        SqlConnection con1 = new SqlConnection(myConnectionString);
                        con1.ConnectionString = myConnectionString;
                        con1.Open();
                        String cmdText1 = "SELECT * FROM [dbo].[Groups]  where  [User_ID]='" + User_ID.Trim() + "' and [GroupName]='" + groupname + "'  Order By [Minute] asc ";
                        SqlCommand cmd1 = new SqlCommand(cmdText1, con1);
                        SqlDataReader reader1 = cmd1.ExecuteReader();
                        while (reader1.Read())
                        {
                            string stra = reader1["Official_No"].ToString().Trim();
                            DataTable dt3 = (DataTable)ViewState["Details"];
                            bool contains = dt3.AsEnumerable().Any(row => stra == row.Field<String>("OfficialNo"));

                            if (contains.Equals(true))
                            {
                                lblinfo.Text = "Officer is already added";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Selected officer is already added');", true);
                                return;
                            }
                            string strNew = reader1["Official_No"].ToString().Trim();
                            string strNew1 = reader1["Name"].ToString().Trim();
                            DataTable dt1 = (DataTable)ViewState["Details"];
                            dt1.Rows.Add(strNew, strNew1);
                            ViewState["Details"] = dt1;
                            GridView1.DataSource = dt1;
                            GridView1.DataBind();
                            this.GridView1.Columns[0].Visible = false;
                        }
                        con1.Close();
                        con1.Dispose();
                        cmd1.Dispose();
                        reader1.Dispose();
                        DDGroup.Text = "--Groups--";

                    }
                }

                string str = dltlist.Text.Trim();
                string str1 = dltlist.SelectedItem.Text;
                if (dptype.Text.Trim().Equals("Name"))
                {
                    str1 = str1 + "," + str;
                }
                DataTable dt = (DataTable)ViewState["Details"];
                bool contains1 = dt.AsEnumerable().Any(row => str == row.Field<String>("OfficialNo"));
                if (contains1.Equals(true))
                {
                    lblinfo.Text = "Officer already added";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Selected officer is already added');", true);
                    return;
                }

                string selectSQL = "select * from dbo.Profile_Master  where Officer_Id='" + str + "' and [Onleave]=1 ";
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
                    array[count, 0] = str;
                    array[count, 1] = reader["Appointment"].ToString();
                    Session["Leavelist"] = array;

                }

                con.Close();
                cmd.Dispose();
                reader.Dispose();
                con.Open();
                selectSQL = "select * from dbo.Profile_Master  where Officer_Id='" + overseeby + "' ";
                cmd = new SqlCommand(selectSQL, con);
                reader = cmd.ExecuteReader();
                if (overseeby != "")
                {
                    while (reader.Read())
                    {
                        str = reader["Officer_Id"].ToString();
                        str1 = reader["Appointment"].ToString();
                    }
                }

                dt.Rows.Add(str, str1);
                ViewState["Details"] = dt;
                GridView1.DataSource = dt;
                GridView1.DataBind();
                this.GridView1.Columns[0].Visible = false;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                lblinfo.Text = "Error : " + ex.Message;
            }
        }

        private int referenceAvailable()
        {
            string imageurl = lblRef1.Text;
            string imageur2 = lblRef2.Text;
            string imageur3 = lblRef3.Text;
            string imageur4 = lblRef4.Text;
            int chk = 3;
            //reference 01
            if (!imageurl.Trim().Equals(""))
            {
                if (txtref1.Text.Trim().Equals(""))
                {
                    lblinfo.Text = "Reference 01's description cannot be empty in ";
                    return chk = 0;
                }
                else
                {
                    chk = 1;
                }
            }
            if (imageurl.Trim().Equals(""))
            {
                if (!txtref1.Text.Trim().Equals(""))
                {
                    lblRef1.Text = "NULL";
                }
            }
            //Refrence 02
            if (!imageur2.Trim().Equals(""))
            {
                if (txtref2.Text.Trim().Equals(""))
                {
                    lblinfo.Text = "Reference 02's description cannot be empty";
                    return chk = 0;
                }
                else
                {
                    chk = 1;
                }
            }
            if (imageur2.Trim().Equals(""))
            {
                if (!txtref2.Text.Trim().Equals(""))
                {
                    lblRef2.Text = "NULL";
                }
            }
            //Refrence 03
            if (!imageur3.Trim().Equals(""))
            {
                if (txtref3.Text.Trim().Equals(""))
                {
                    lblinfo.Text = "Reference3 description cannot be empty";
                    return chk = 0;
                }
                else
                {
                    chk = 1;
                }
            }
            if (imageur3.Trim().Equals(""))
            {
                if (!txtref3.Text.Trim().Equals(""))
                {
                    lblRef3.Text = "NULL";
                }
            }
            //Refrence 04 //Annexture
            if (!imageur4.Trim().Equals(""))
            {
                if (txtref4.Text.Trim().Equals(""))
                {
                    lblinfo.Text = "Annexure description cannot be empty";
                    return chk = 0;
                }
                else
                {
                    chk = 1;
                }
            }
            if (imageur4.Trim().Equals(""))
            {
                if (!txtref4.Text.Trim().Equals(""))
                {
                    lblinfo.Text = "Annexure should be upload.";
                    return chk = 0;
                }
            }
            return chk;
        }
        private int referenceMake()
        {
            string imageurl = lblRef1.Text;
            string imageur2 = lblRef2.Text;
            string imageur3 = lblRef3.Text;
            string imageur4 = lblRef4.Text;
            int chk = 0;
            //reference 01
            if (!txtref1.Text.Trim().Equals(""))
            {

                string value = txtref1.Text.Trim();
                value = value.Replace("'", "''");
                txtref1.Text = value;
                chk = 1;

            }

            //Refrence 02
            if (!txtref2.Text.Trim().Equals(""))
            {
                string value = txtref2.Text.Trim();
                value = value.Replace("'", "''");
                txtref2.Text = value;
                chk = 1;

            }

            //Reference 03
            if (!txtref3.Text.Trim().Equals(""))
            {

                string value = txtref3.Text.Trim();
                value = value.Replace("'", "''");
                txtref3.Text = value;
                chk = 1;

            }
            //Reference 04
            if (!imageur4.Trim().Equals(""))
            {
                string value = txtref4.Text.Trim();
                value = value.Replace("'", "''");
                txtref4.Text = value;
                chk = 1;
            }

            return chk;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            try
            {

                lblinfo.Text = "";
                if (txtSubject.Text.Trim().Equals(""))
                {
                    lblinfo.Text = "Subject cannot be empty";
                    return;
                }
                else if (txtbody.Text.Trim().Equals(""))
                {
                    lblinfo.Text = "Minute sheet body cannot be empty";
                    return;
                }
                else if (dpBy.Text.Trim().Equals(""))
                {
                    lblinfo.Text = "Minute sheet BY: cannot be empty";
                    return;
                }

                int referencestatus = referenceAvailable();

                if (referencestatus == 0) { return; }
                string body = txtbody.Text;
                body = body.Replace("'", "''");
                txtbody.Text = body;

                body = txtSubject.Text;
                body = body.Replace("'", "''");
                txtSubject.Text = body;

                DataTable dt = (DataTable)ViewState["Details"];
                if (dt.Rows.Count == 0)
                {
                    lblinfo.Text = "Select the persons to forward Minute sheet ";
                    return;
                }
                float lastno;
                string insertCmdText;
                string uid = User_Name;
                SqlConnection GetData = new SqlConnection();
                GetData.ConnectionString = myConnectionString;
                GetData.Open();
                insertCmdText = "Select * from [Min_Master] where [User_ID]='" + uid + "' and [Min_ID]='" + lblMinuteSheetNo.Text + "' ";
                SqlCommand cmds = new SqlCommand(insertCmdText, GetData);
                SqlDataReader readers = cmds.ExecuteReader();
                while (readers.Read())
                {
                    updateminutes();
                    goto jumper;
                }

                lastno = GetLast_Min_No();
                string output = string.Format("{0:00000}", lastno);
                lblMinuteSheetNo.Text = output.ToString();
                string Min_SubID;
                if (dpMedium.Text.Trim() == "English")
                {
                    Min_SubID = "M01";
                }
                else
                {
                    Min_SubID = "මාස01";
                }

                string mindate = DateTime.Now.ToShortDateString();
                SqlConnection conGetData = new SqlConnection();
                conGetData.ConnectionString = myConnectionString;
                conGetData.Open();
                int val = 0;
                if (chkOrder.Checked) { val = 1; } else { val = 0; }

                insertCmdText = "INSERT INTO [dbo].[Min_Master] ([User_ID] ,[Min_ID] ,[Min_SubID] ,[Topic] ,[ByPerson],[Date_Time] ,[MBody],[Order_Follow],[Reply_Min],[RUser_ID],[RMin_ID],[Submit],[SubmitTime],[MConfirm],Medium,Sub_Id)  values('" + uid.Trim() + "','" + output + "',N'" + Min_SubID + "',N'" + txtSubject.Text.Trim() + "',N'" + dpBy.Text + "',CONVERT(datetime, GETDATE()),N'" + txtbody.Text + "'," + val + ",0,'" + uid.Trim() + "','" + output + "',1,CONVERT(datetime, GETDATE()),0,N'" + dpMedium.Text.Trim() + "',1)";
                SqlCommand insertCmd = new SqlCommand(insertCmdText, conGetData);
                insertCmd.ExecuteNonQuery();
                conGetData.Close();
                conGetData.Dispose();
                insertCmd.Dispose();

            jumper:
                //Save details to Ref officers details to min table
                SaveReference();
                Saverefpaths();
                loadpendingMinutes();
                btnConfrim.Enabled = true;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Minute has submitted');", true);

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                lblinfo.Text = "Error : " + ex.Message;
            }
        }

        private void Saverefpaths()
        {

            string output = lblMinuteSheetNo.Text.Trim();
            string uid = User_Name;
            int referencestatus = referenceMake();
            if (referencestatus != 0)
            {
                SqlConnection conGetData = new SqlConnection();
                conGetData.ConnectionString = myConnectionString;
                conGetData.Open();
                string insertCmdText = "INSERT INTO [dbo].[Reference_Master] ([Min_ID],[User_ID] ,[Ref_Order] ,[Description],[Ref_path],[Description1],[Ref_path1],[Description2],[Ref_path2],[Description3],[Ref_path3] )  values('" + output.Trim() + "','" + uid.Trim() + "',1,N'" + txtref1.Text.Trim() + "',N'" + lblRef1.Text.Trim() + "',N'" + txtref2.Text.Trim() + "',N'" + lblRef2.Text.Trim() + "',N'" + txtref3.Text.Trim() + "',N'" + lblRef3.Text.Trim() + "',N'" + txtref4.Text.Trim() + "',N'" + lblRef4.Text.Trim() + "')";
                SqlCommand insertCmd = new SqlCommand(insertCmdText, conGetData);
                insertCmd.ExecuteNonQuery();
                conGetData.Close();
                conGetData.Dispose();
                insertCmd.Dispose();
            }
        }
        private void SaveReference()
        {
            if (Session["Leavelist"] != null)
            {
                array = Session["Leavelist"] as String[,];
            }

            DataTable dt1 = (DataTable)ViewState["Details"];
            int val = 0;

            int count = 1;
            string output = lblMinuteSheetNo.Text.Trim();
            string uid = User_Name;

            //Reading DataTable Rows Column Value using Column Index Number
            foreach (DataRow oRecord in dt1.Rows)
            {
                string User_Name1 = oRecord[0].ToString();
                string Sname = oRecord[1].ToString();
                SqlConnection NewCon = new SqlConnection();
                NewCon.ConnectionString = myConnectionString;
                NewCon.Open();

                string RunQuery = "INSERT INTO [dbo].[Min_References] ([Min_Id],[User_ID],[User_Name] ,[Name] ,[Read_Not],[Order] ,[Res_Required],[Responded],[RespondAllow])  values('" + output.Trim() + "','" + uid.Trim() + "','" + User_Name1.Trim() + "',N'" + Sname.Trim() + "',0," + count + "," + val + ",0,0)";
                SqlCommand newCmd = new SqlCommand(RunQuery, NewCon);
                newCmd.ExecuteNonQuery();
                NewCon.Close();
                NewCon.Dispose();
                newCmd.Dispose();
                count = count + 1;
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

                    string RunQuery = "INSERT INTO [dbo].[Min_References] ([Min_Id],[User_ID],[User_Name] ,[Name] ,[Read_Not],[Order] ,[Res_Required],[Responded],[RespondAllow],[Displayonly])  values('" + output.Trim() + "','" + uid.Trim() + "','" + User_Name1.Trim() + "',N'" + Sname.Trim() + "',0," + count + "," + val + ",1,1,1)";
                    SqlCommand newCmd = new SqlCommand(RunQuery, NewCon);
                    newCmd.ExecuteNonQuery();
                    NewCon.Close();
                    NewCon.Dispose();
                    newCmd.Dispose();

                }

            }

            if (chkOrder.Checked.Equals(true))
            {
                count = count - 1;
                SqlConnection NewCon1 = new SqlConnection();
                NewCon1.ConnectionString = myConnectionString;
                NewCon1.Open();

                string RunQuery1 = "UPDATE [dbo].[Min_References] SET [RespondAllow]=1 where [Min_Id]='" + output.Trim() + "' and [User_ID] ='" + uid.Trim() + "' and [Order]=1 and Displayonly=0";
                SqlCommand newCmd1 = new SqlCommand(RunQuery1, NewCon1);
                newCmd1.ExecuteNonQuery();

                NewCon1.Close();
                NewCon1.Dispose();
                newCmd1.Dispose();
            }

            else
            {
                SqlConnection NewCon1 = new SqlConnection();
                NewCon1.ConnectionString = myConnectionString;
                NewCon1.Open();

                string RunQuery1 = "UPDATE [dbo].[Min_References] SET [RespondAllow]=1 where [Min_Id]='" + output.Trim() + "' and [User_ID] ='" + uid.Trim() + "' and Displayonly=0";
                SqlCommand newCmd1 = new SqlCommand(RunQuery1, NewCon1);
                newCmd1.ExecuteNonQuery();

                NewCon1.Close();
                NewCon1.Dispose();
                newCmd1.Dispose();
            }
        }
        private int GetLast_Min_No()
        {



            string uid = User_Name;
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


        protected void Respond_CheckedChanged(object sender, EventArgs e)
        {
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

        protected void btnUpload1_Click(object sender, EventArgs e)
        {
            try
            {

                SqlConnection con = new SqlConnection();
                con.ConnectionString = myConnectionString;
                con.Open();

                if (fuRef1.PostedFile != null)
                {
                    var folder = Server.MapPath("~/References/" + User_Name);
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }

                    string FileName = Path.GetFileName(fuRef1.PostedFile.FileName);
                    //Save files to images folder
                    fuRef1.SaveAs(folder + "/" + FileName);
                    lblRef1.Text = "References/" + User_Name + "/" + FileName;
                    this.refImage1.ImageUrl = "~/Images/Ok.png";
                    this.refImage1.Visible = true;
                    VisbileofDel();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                lblinfo.Text = "Error : " + ex.Message;
            }
        }



        protected void btnupload2_Click(object sender, EventArgs e)
        {

            try
            {

                if (fuRef2.PostedFile != null)
                {

                    var folder = Server.MapPath("~/References/" + User_Name);
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }

                    string FileName = Path.GetFileName(fuRef2.PostedFile.FileName);
                    //Save files to images folder
                    fuRef2.SaveAs(folder + "/" + FileName);
                    lblRef2.Text = "References/" + User_Name + "/" + FileName;
                    this.refImage2.ImageUrl = "~/Images/Ok.png";
                    this.refImage2.Visible = true;
                    VisbileofDel();


                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                lblinfo.Text = "Error : " + ex.Message;
            }
        }

        protected void btnupload3_Click(object sender, EventArgs e)
        {

            try
            {

                if (fuRef3.PostedFile != null)
                {

                    var folder = Server.MapPath("~/References/" + User_Name);
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }

                    string FileName = Path.GetFileName(fuRef3.PostedFile.FileName);
                    //Save files to images folder
                    fuRef3.SaveAs(folder + "/" + FileName);
                    lblRef3.Text = "References/" + User_Name + "/" + FileName;
                    this.refImage3.ImageUrl = "~/Images/Ok.png";
                    this.refImage3.Visible = true;
                    VisbileofDel();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                lblinfo.Text = "Error : " + ex.Message;
            }
        }
        protected void chkforder_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GVSubmit_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {

                clear();
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVSubmit.Rows[index];
                string MID = row.Cells[1].Text.ToString();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = myConnectionString;
                con.Open();
                string cmdText = "SELECT * FROM [dbo].[Min_Master] where [MConfirm]=0 and [User_ID]='" + User_Name + "' and [Min_ID]='" + MID.Trim() + "'   ";
                SqlCommand cmd = new SqlCommand(cmdText, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dpMedium.Text = reader["Medium"].ToString().Trim();
                    LanguageChange();
                    dpBy.Text = reader["ByPerson"].ToString().Trim();
                    lblMinuteSheetNo.Text = reader["Min_ID"].ToString().Trim();
                    txtSubject.Text = reader["Topic"].ToString().Trim();
                    dpBy.Text = reader["ByPerson"].ToString().Trim();
                    txtbody.Text = reader["MBody"].ToString().Trim();
                    lblDate.Text = reader["Date_Time"].ToString().Trim();
                    if (reader["Order_Follow"].Equals(true)) { chkOrder.Checked = true; }
                    else { chkOrder.Checked = false; }



                }
                con.Close();
                con.Dispose();
                cmd.Dispose();
                reader.Dispose();
                //References
                con.ConnectionString = myConnectionString;
                con.Open();
                cmdText = "SELECT * FROM [dbo].[Reference_Master]  where  [User_ID]='" + User_Name + "' and [Min_ID]='" + MID.Trim() + "'   ";
                cmd = new SqlCommand(cmdText, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!reader["Description"].ToString().Trim().Equals(""))
                    {
                        txtref1.Text = reader["Description"].ToString().Trim();
                        if (!reader["Ref_Path"].ToString().Trim().Equals(""))
                        {
                            lblRef1.Text = reader["Ref_Path"].ToString().Trim();
                            refImage1.ImageUrl = "~/Images/ok.png";
                            refImage1.Visible = true;
                        }
                    }
                    if (!reader["Description1"].ToString().Trim().Equals(""))
                    {
                        txtref2.Text = reader["Description1"].ToString().Trim();
                        if (!reader["Ref_Path1"].ToString().Trim().Equals(""))
                        {
                            lblRef2.Text = reader["Ref_Path1"].ToString().Trim();
                            refImage2.ImageUrl = "~/Images/ok.png";
                            refImage2.Visible = true;
                        }
                    }
                    if (!reader["Description2"].ToString().Trim().Equals(""))
                    {
                        txtref3.Text = reader["Description2"].ToString().Trim();
                        if (!reader["Ref_Path2"].ToString().Trim().Equals(""))
                        {
                            lblRef3.Text = reader["Ref_Path2"].ToString().Trim();
                            refImage3.ImageUrl = "~/Images/ok.png";
                            refImage3.Visible = true;
                        }
                    }
                    if (!reader["Description3"].ToString().Trim().Equals(""))
                    {
                        txtref4.Text = reader["Description3"].ToString().Trim();
                        if (!reader["Ref_Path3"].ToString().Trim().Equals(""))
                        {
                            lblRef4.Text = reader["Ref_Path3"].ToString().Trim();
                            refImage4.ImageUrl = "~/Images/ok.png";
                            refImage4.Visible = true;
                        }
                    }
                    VisbileofDel();
                    break;
                }
                con.Close();
                con.Dispose();
                cmd.Dispose();
                reader.Dispose();

                //To Minutes
                con.ConnectionString = myConnectionString;
                con.Open();
                cmdText = "SELECT * FROM [dbo].[Min_References]  where  [User_ID]='" + User_Name + "' and [Min_ID]='" + MID.Trim() + "' and [Displayonly]=0 Order By [Order] asc ";
                cmd = new SqlCommand(cmdText, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string str = reader["User_Name"].ToString().Trim();
                    string str1 = reader["Name"].ToString().Trim();
                    DataTable dt = (DataTable)ViewState["Details"];
                    dt.Rows.Add(str, str1);
                    ViewState["Details"] = dt;
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    this.GridView1.Columns[0].Visible = false;
                }
                con.Close();
                con.Dispose();
                cmd.Dispose();
                reader.Dispose();

                con.ConnectionString = myConnectionString;
                con.Open();
                cmdText = "SELECT * FROM [dbo].[Min_References]  where  [User_ID]='" + User_Name + "' and [Min_ID]='" + MID.Trim() + "' and [Displayonly]=1 Order By [Order] asc ";
                cmd = new SqlCommand(cmdText, con);
                reader = cmd.ExecuteReader();
                int count = 0;
                while (reader.Read())
                {

                    string str = reader["User_Name"].ToString().Trim();
                    string str1 = reader["Name"].ToString().Trim();
                    array[count, 0] = str;
                    array[count, 1] = str1;
                    Session["Leavelist"] = array;
                }
                con.Close();
                con.Dispose();
                cmd.Dispose();
                reader.Dispose();



            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                lblinfo.Text = "Error : " + ex.Message;
            }
        }

        private void updateminutes()
        {
            String User_Name = this.User_Name;
            String uid = User_Name;
            String Mid = lblMinuteSheetNo.Text.Trim();
            int val = 0;
            if (chkOrder.Checked) { val = 1; } else { val = 0; }
            SqlConnection con = new SqlConnection();
            con.ConnectionString = myConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            String sqlstring = "Update [dbo].[Min_Master] SET [Topic]=N'" + txtSubject.Text.ToUpper() + "' ,[ByPerson]=N'" + dpBy.Text.Trim() + "' ,[Date_Time]=CONVERT(date, GETDATE()) ,[MBody]=N'" + txtbody.Text + "',[Order_Follow]=" + val + ",[Reply_Min]=0,[RUser_ID]='" + uid.Trim() + "',[RMin_ID]= '" + Mid + "',[Submit]=1,[SubmitTime]=CONVERT(date, GETDATE()),[MConfirm]=0 where [User_ID] ='" + uid.Trim() + "' and [Min_ID]='" + Mid + "' ";
            SqlCommand insertCmd = new SqlCommand(sqlstring, con);
            insertCmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            cmd.Dispose();

            //Delete
            con = new SqlConnection();
            con.ConnectionString = myConnectionString;
            cmd = new SqlCommand();
            cmd.CommandText = "DELETE FROM [Min_References]  where [User_ID] ='" + uid.Trim() + "' and [Min_ID]='" + Mid + "' ";
            cmd.Connection = con;
            con.Open();
            int numberDeleted = cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            cmd.Dispose();

            con = new SqlConnection();
            con.ConnectionString = myConnectionString;
            cmd = new SqlCommand();
            cmd.CommandText = "DELETE FROM [Reference_Master]  where [User_ID] ='" + uid.Trim() + "' and [Min_ID]='" + Mid + "' ";
            cmd.Connection = con;
            con.Open();
            numberDeleted = cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            cmd.Dispose();
        }

        protected void btnConfrim_Click(object sender, EventArgs e)
        {
            try
            {
                String User_Name = this.User_Name;
                lblinfo.Text = "";
                if (txtSubject.Text.Trim().Equals(""))
                {
                    lblinfo.Text = "Subject cannot be empty";
                    return;
                }
                else if (txtbody.Text.Trim().Equals(""))
                {
                    lblinfo.Text = "Minute sheet cannot be empty";
                    return;
                }
                else if (dpBy.Text.Trim().Equals(""))
                {
                    lblinfo.Text = "Minute sheet BY: cannot be empty";
                    return;
                }
                int referencestatus = referenceAvailable();
                if (referencestatus == 0) { return; }

                if (!lblMinuteSheetNo.Text.Trim().Equals(""))
                {


                    string insertCmdText;
                    string uid = User_Name;
                    SqlConnection GetData = new SqlConnection();
                    GetData.ConnectionString = myConnectionString;
                    GetData.Open();
                    insertCmdText = "Select * from [Min_Master] where [User_ID]='" + uid + "' and [Min_ID]='" + lblMinuteSheetNo.Text + "' and MConfirm=1";
                    SqlCommand cmds = new SqlCommand(insertCmdText, GetData);
                    SqlDataReader readers = cmds.ExecuteReader();
                    while (readers.Read())
                    {
                        updateminutes();
                        return;
                    }
                    GetData.Close();
                    GetData.Dispose();
                    cmds.Dispose();
                    readers.Dispose();


                    updateminutes();
                    uid = User_Name;
                    String Mid = lblMinuteSheetNo.Text.Trim();
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = myConnectionString;
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    String sqlstring = "Update [dbo].[Min_Master] SET [MConfirm]=1,[Date_Time]=CONVERT(datetime, GETDATE()),ConfirmTime=CONVERT(datetime, GETDATE()) where [User_ID] ='" + uid.Trim() + "' and [Min_ID]='" + Mid + "' ";
                    SqlCommand insertCmd = new SqlCommand(sqlstring, con);
                    insertCmd.ExecuteNonQuery();
                    con.Close();
                    con.Dispose();
                    cmd.Dispose();

                    //Insert
                    SaveReference();
                    Saverefpaths();
                    btnConfrim.Enabled = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Minute is forwarded');", true);
                    loadpendingMinutes();
                    clear();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                lblinfo.Text = "Error : " + ex.Message;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clear();
            loadpendingMinutes();

        }
        private void clear()
        {
            lblRef1.Text = "";
            lblRef2.Text = "";
            lblRef3.Text = "";
            txtSubject.Text = "";
            txtbody.Text = "";
            lblDate.Text = "";
            txtref1.Text = "";
            txtref2.Text = "";
            txtref3.Text = "";
            txtref4.Text = "";
            refImage1.ImageUrl = "";
            refImage2.ImageUrl = "";
            refImage3.ImageUrl = "";
            refImage4.ImageUrl = "";
            lblMinuteSheetNo.Text = "";
            chkOrder.Checked = false;
            makegrid();
        }

        protected void dltlist_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GVSubmit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnupload4_Click(object sender, EventArgs e)
        {
            try
            {
               // String User_Name = Session["User_Name"].ToString().Trim();

                if (fuRef4.PostedFile != null)
                {

                    var folder = Server.MapPath("~/Annexure/" + User_Name);
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }

                    string FileName = Path.GetFileName(fuRef4.PostedFile.FileName);
                    //Save files to images folder
                    fuRef4.SaveAs(folder + "/" + FileName);
                    lblRef4.Text = "Annexure/" + User_Name + "/" + FileName;
                    this.refImage4.ImageUrl = "~/Images/Ok.png";
                    this.refImage4.Visible = true;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                lblinfo.Text = "Error : " + ex.Message;
            }
        }

        protected void dpBy_SelectedIndexChanged(object sender, EventArgs e)
        {

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



        protected void dpMedium_SelectedIndexChanged1(object sender, EventArgs e)
        {
            LanguageChange();
        }
        private void LanguageChange()
        {

            clear();
            DRPFill();
            FillOfficerList();
            if (dpMedium.Text.Trim() == "English")
            {
                lblBy.Visible = true;
                lblMinuteNo.Text = "M01";
                lblMinuteHead.Text = "MINUTE SHEET";
                lblBy.Text = "BY";
                lblRef.Text = "REF";
                lblRefSin.Text = "Reference";
                FillOfficerList();

            }
            else
            {
                lblMinuteNo.Text = "මාස 01";
                lblMinuteHead.Text = "මාණ්ඩලික සටහන්පත";
                lblBy.Visible = false;
                lblRef.Text = "යොමුව";
                lblRefSin.Text = "යොමුව";
                FillOfficerList();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)//Approved
        {
            txtbody.Text = "Ref M         Approved";
        }

        protected void Button2_Click(object sender, EventArgs e)//Not Approved
        {
            txtbody.Text = "Ref M         Not Approved";
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            txtbody.Text = "Ref M      Recommended";
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            txtbody.Text = "Ref M      Not Recommended";
        }

        protected void dpBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            dptype.SelectedIndex = 0;
            dltlist.Items.Clear();

        }




        private void FillOfficerListSecondTime()
        {
            string selectSQL;

            if (dptype.Text.Equals("Appointment") && dltlist.Text != "")
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
                        if (reader["AppointmentSin"].ToString().ToUpper().Trim() != "")
                        {
                            ListItem newItem = new ListItem();
                            newItem.Text = reader["AppointmentSin"].ToString().ToUpper();
                            newItem.Value = reader["ConcatOfficialNo"].ToString();
                            dltlist.Items.Add(newItem);
                        }
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
            else if (dptype.Text.Equals("Name") && dltlist.Text != "")
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
                        if (reader["Name_With_Initial_Sin"].ToString().ToUpper().Trim() != "")
                        {
                            ListItem newItem = new ListItem();
                            newItem.Text = reader["RankSin"].ToString().ToUpper() + "  " + reader["Name_With_Initial_Sin"].ToString().ToUpper();
                            newItem.Value = reader["Officer_Id"].ToString();
                            dltlist.Items.Add(newItem);
                        }

                    }
                }
                con.Close();
                cmd.Dispose();
                reader.Dispose();

            }
            else if (dptype.Text.Equals("Other Appointment") && dltlist.Text != "")
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
                    if (!reader["Other_Appointment"].ToString().ToUpper().Equals("") && (dpMedium.Text.Trim() == "English"))
                    {
                        if (reader["Other_Appointment"].ToString().ToUpper().Trim() != "")
                        {
                            ListItem newItem = new ListItem();
                            newItem.Text = reader["Other_Appointment"].ToString().ToUpper();
                            newItem.Value = reader["Officer_Id"].ToString();
                            dltlist.Items.Add(newItem);
                        }
                    }
                    else
                    {
                        if (reader["Other_AppointmentSin"].ToString().ToUpper().Trim() != "")
                        {
                            ListItem newItem = new ListItem();
                            newItem.Text = reader["Other_AppointmentSin"].ToString().ToUpper();
                            newItem.Value = reader["Officer_Id"].ToString();
                            dltlist.Items.Add(newItem);
                        }

                    }
                }
                reader.Close();
                con.Close();
                con.Dispose();
                reader.Dispose();
                cmd.Dispose();
            }

        }

        protected void DDGroup_PreRender(object sender, EventArgs e)
        {

        }



    }
}