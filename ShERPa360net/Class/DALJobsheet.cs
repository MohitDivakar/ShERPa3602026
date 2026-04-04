using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShERPa360net.Class
{
    public class DALJobsheet
    {
        MainClass objMainClass = new MainClass();
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        public string CheckInqInJob(string strInqNo)
        {
            string strReturn = "";
            SqlCommand cmd = new SqlCommand("SP_SELECT_CHECKINQINJOB", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@INQNO", strInqNo);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            object obj = cmd.ExecuteScalar();
            cmd.Connection.Close();
            if (obj != null)
            {
                strReturn = obj.ToString();
            }
            return strReturn;
        }

    }
}