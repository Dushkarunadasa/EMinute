using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.SessionState;


namespace Officer_Details
{
   
    public static class OfficerDetails
    {

        public static String FullNameEng;
        public static String FullNameSin;
        public static String RankEng;
        public static String RankSin;
        public static String OfficialNo;
        public static String Branch;
        public static String AppointmentEng;
        public static String AppointmentSin;
        public static String OtherAppointmentEng;
        public static String OtherAppointmentSin;
        public static String myConnectionString = ConfigurationManager.ConnectionStrings["M_SConnection"].ConnectionString;
        public static String concatOfficialNo;

        public static SqlConnection con = new SqlConnection();
        public static SqlDataReader Reader;
        private static string sqlQuery;

        public static void ReadOfficerDetails(string UserName)
        {
            SqlConnection con = new SqlConnection(myConnectionString);
            con.Open();
           sqlQuery = "SELECT  *   FROM [M_Sheet].[dbo].[Profile_Master] where [User_Name]='" + UserName + "'";
           SqlCommand cmd = new SqlCommand(sqlQuery,con);
           SqlDataReader reader = cmd.ExecuteReader();
           while (reader.Read())
           {

               FullNameEng = reader["Name_With_Initial"].ToString();
               FullNameSin = reader["Name_With_Initial_Sin"].ToString();
               RankEng = reader["Rank"].ToString();
               RankSin = reader["RankSin"].ToString();
               AppointmentEng = reader["Appointment"].ToString();
               AppointmentSin = reader["AppointmentSin"].ToString();
               OtherAppointmentEng = reader["Other_Appointment"].ToString();
               OtherAppointmentSin = reader["Other_AppointmentSin"].ToString();
               Branch = reader["Branch"].ToString();
               OfficialNo = reader["Official_No"].ToString();
               concatOfficialNo = reader["Officer_Id"].ToString();
     
           }
           con.Close();
        }

        public static void ReadOffDtls(string concatOffNo)
        {
            SqlConnection con = new SqlConnection(myConnectionString);
            con.Open();
            sqlQuery = "SELECT  *   FROM [M_Sheet].[dbo].[Profile_Master] where [Officer_Id]='" + concatOffNo + "'";
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                FullNameEng = reader["Name_With_Initial"].ToString().Trim();
                FullNameSin = reader["Name_With_Initial_Sin"].ToString().Trim();
                RankEng = reader["Rank"].ToString().Trim();
                RankSin = reader["RankSin"].ToString().Trim();
                AppointmentEng = reader["Appointment"].ToString().Trim();
                AppointmentSin = reader["AppointmentSin"].ToString().Trim();
                OtherAppointmentEng = reader["Other_Appointment"].ToString().Trim();
                OtherAppointmentSin = reader["Other_AppointmentSin"].ToString().Trim();
                Branch = reader["Branch"].ToString().Trim();
                OfficialNo = reader["Official_No"].ToString().Trim();
                concatOfficialNo = reader["Officer_Id"].ToString().Trim();

            }
            con.Close();
        }
    
    
        public static string GetFullNameEng()
        {
            return RankEng + " " + FullNameEng + " ," + Branch + OfficialNo;
        }
        public static string GetFullNameSin()
        {
            if (FullNameSin.Trim() != "")
            {
                return RankSin + " " + FullNameSin + " විසින්";
            }
            else
            {
                return "";
            }
        }

        public static string GetAppointmentEng()
        {
            return AppointmentEng;
        }

        public static string GetAppointmentSin()
        {
            if (AppointmentSin.Trim() != "")
            {
                return AppointmentSin + " විසින්";
            }
            else
            {
                return "";
            }
        }
        public static string GetOtherAppointmentEng()
        {
            if (OtherAppointmentEng.Trim() != "")
            {
                return OtherAppointmentEng;
            }
            else
            {
                return "";
            }
        }
        public static string GetOtherAppointmentSin()
        {
            if (OtherAppointmentSin.Trim() != "")
            {
                return OtherAppointmentSin + " විසින්";
            }
            else
            {
                return "";
            }
        }
        public static string GetBranch()
        {
            return Branch;
        }

        public static string GetOfficialNo()
        {
            return OfficialNo;
        }

        public static string ConcatOfficialNo()
        {
            return Branch + OfficialNo;
        }
        public static string getConcatOfficialNo()
        {
            return concatOfficialNo;
        }
        public static string SplitBranch()
        {
            return concatOfficialNo.Substring(1,3);
        }
        public static string SplitOfficialNo()
        {
            return concatOfficialNo.Substring(3, concatOfficialNo.Length);
        }

        public static bool CheckEnglish(string Min_Id, string User_Id)
        {
            SqlConnection con = new SqlConnection(myConnectionString);
            con.Open();
            Boolean english=true;
            sqlQuery = "SELECT  *   FROM [M_Sheet].[dbo].[Min_Master] where [Min_ID]='" + Min_Id + "' and [User_ID]='" + User_Id + "'";
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                if (reader["Medium"].ToString().Trim() == "English")
                {
                    english=true;
                }
                else
                {
                    english=false;
                }

            }
            con.Close();
            if (english == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}