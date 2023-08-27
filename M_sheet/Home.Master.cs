using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M_sheet
{
    public partial class Home : System.Web.UI.MasterPage
    {
        private string Utype;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.Cookies["LocalComputer"] != null)
                {
                    HttpCookie _cookie = Request.Cookies["LocalComputer"];
                    Utype = _cookie["User_Type"].ToString().Trim().ToUpper();
                }
                if (Utype.Trim().ToUpper() == "ADMIN")
                {
                    User_aUpdate.Visible = true;
                    MinutesInspection.Visible = true;
                    Registered_Users.Visible = true;
                    AdminFaciliy.Visible = true;
                }
                if (Utype.Trim().ToUpper() == "USER")
                {
                    User_aUpdate.Visible = false;
                    MinutesInspection.Visible = false;
                    Registered_Users.Visible = false;
                    AdminFaciliy.Visible = false;
                }
            }
            catch (Exception ex)
            {
                if (Utype == null )
                {
                                   
                    Response.Redirect("LoginPage.aspx");
                }
            }
        }


        
    }
}