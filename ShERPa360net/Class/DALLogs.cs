using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShERPa360net.Class
{
    public class DALLogs
    {
        MainClass objMainClass = new MainClass();
        public void SaveSMSLogs(string strSMS, string strJobNo, string strMobileNo, string strOrigin, string strDocType, int StageId)
        {
            SqlCommand cmd = new SqlCommand("SP_INSERT_MST_SMSMSG", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@SMSTXT", strSMS);
            cmd.Parameters.AddWithValue("@SMSTO", strMobileNo);
            cmd.Parameters.AddWithValue("@JOBID", strJobNo);
            cmd.Parameters.AddWithValue("@ORIGIN", strOrigin);
            cmd.Parameters.AddWithValue("@DOCTYPE", strDocType);
            cmd.Parameters.AddWithValue("@STAGEID", StageId);
            cmd.Parameters.AddWithValue("@SMSBY", int.Parse(HttpContext.Current.Session["USERID"].ToString()));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
    }
}