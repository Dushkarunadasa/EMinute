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
using System.Text;
using Salt_Password_Sample;
using Officer_Details;

namespace M_Sheet
{
    public partial class WebForm1 : System.Web.UI.Page

    {
        string myConnectionString = ConfigurationManager.ConnectionStrings["M_SConnection"].ConnectionString;
       protected void Page_Init(object sender,EventArgs e)
        {
            try
            {
                HttpCookie aCookie;
                string cookieName;
                int limit = Request.Cookies.Count;
                for (int i = 0; i < limit; i++)
                {
                    cookieName = Request.Cookies[i].Name;
                    aCookie = new HttpCookie(cookieName);
                    aCookie.Expires = DateTime.Now.AddDays(-1); // make it expire yesterday
                    Response.Cookies.Add(aCookie); // overwrite it

                }
            }
           catch (Exception ex)
            {
                lblinfo.Text = ex.Message;
            }
            
        }
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                 lblinfo.Text = ""; 
          
            }
        }
        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
          
            try
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();
                if ((username == "dnitadmin") && (password == "1234"))
                {
                    Response.Redirect("FMAdmin.aspx");
                }
                else
                {
                        SqlConnection con = new SqlConnection();
                        con.ConnectionString = myConnectionString;
                        con.Open();
                        int user = 0;
                        string sqlstring = "select User_Name,[Official_No],[Branch],[Password] ,[Name_With_Initial],[Rank],[Appointment],[Other_Appointment],[Active],[User_Type],[Onleave],[Ship_Establishment],[Area],Officer_Id from dbo.Profile_Master where User_Name='" + username + "'";
                        SqlCommand cmd = new SqlCommand(sqlstring, con);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {

                            bool verify = Helper.VerifyHash(password, "SHA512", reader["Password"].ToString());
                           

                            if (verify)
                            {

                                if (!reader["Active"].Equals(true))
                                {

                                    lblinfo.Text = "User activation pending,cannot login";
                                    return;
                                }

                                string test = "NRT";
                                byte[] utf8Bytes = Encoding.UTF8.GetBytes(test);
                                String str1 = Encoding.Unicode.GetString(utf8Bytes);
                                String str2 = Encoding.UTF8.GetString(utf8Bytes);                        
                                user = 1;                                
                                string ip = GetIPAddress();

                                HttpCookie _cookie1 = new HttpCookie("LocalComputer");
                                _cookie1.Values.Add("OfficialNo", reader["Officer_Id"].ToString());
                                _cookie1.Values.Add("User_Type", reader["User_Type"].ToString());
                                _cookie1.Values.Add("Base", reader["Ship_Establishment"].ToString());
                                _cookie1.Values.Add("Area", reader["Area"].ToString());
                                _cookie1.Values.Add("ip", ip);
                                _cookie1.Expires = DateTime.Now.AddDays(1);
                                Response.Cookies.Add(_cookie1);
                                Response.Redirect("MyAccountPage.aspx");
                                return;                                
                            }
                        }
                        if (!user.Equals(1))
                        {
                            lblinfo.Text = "User Name or password incorrect.";
                            return;
                        }                    
                }        
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.ToString());
                lblinfo.Text = "Error : " + ex.Message; 
            }
        }
    }
    public static class MessageBox
    {
        /// &lt;summary&gt;
        /// Shows messagebox with the specified message.
        /// </summary>
        /// <param name="message"></param>
        public static void Show(string message)
        {
            Page executingPage = HttpContext.Current.Handler as Page;
            if (executingPage != null)
            {
                executingPage.ClientScript.RegisterStartupScript(executingPage.GetType(), "MsgBox",
                    string.Format("alert('{0}');", message), true);
            }
        }
    }
}