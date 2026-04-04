using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.CRM
{
    public partial class rptCRMReports : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    if (Session["USERID"] != null)
                    {
                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menutabid.Value, "");
                        if (FormRights.bView == false)
                        {
                            //Session.Abandon();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        string[] date = objMainClass.indianTime.Date.ToString("dd-MM-yyyy").Split('-');
                        DateTime fromdate = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), 1);
                        txtFromDate.Text = fromdate.ToString("dd-MM-yyyy");

                        //txtFromDate.Text = objMainClass.indianTime.Day(-7).Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }

        }

        [WebMethod]
        public static List<object> GetChartData(string FROMDATE, string TODATE)
        {
            FROMDATE = Convert.ToDateTime(FROMDATE).ToString("yyyy-MM-dd");
            TODATE = Convert.ToDateTime(TODATE).ToString("yyyy-MM-dd");
            //string query = "SELECT DISTINCT(REFF) as REFF,COUNT(*) AS COUNT FROM TRAN_LEAD_DATA AS A WITH(NOLOCK) WHERE A.CREATEDATE >= '" + FROMDATE + " 00:00:00.000' AND A.CREATEDATE <= '" + TODATE + " 23:59:59.000' GROUP BY REFF ORDER BY REFF";
            string query = "SP_LEAD_GENERATION";
            string constr = ConfigurationManager.ConnectionStrings["ConnStringSherpa"].ConnectionString;
            List<object> chartData = new List<object>();
            chartData.Add(new object[]
            {
        "REFF", "COUNT"
            });
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@FROMDATE", FROMDATE + " 00:00:00.000");
                    cmd.Parameters.AddWithValue("@TODATE", TODATE + " 23:59:59.000");
                    cmd.Parameters.AddWithValue("@ACTION", "CHARTREFWISE");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            chartData.Add(new object[]
                            {
                        sdr["REFF"], sdr["COUNT"]
                            });
                        }
                    }
                    con.Close();
                    return chartData;
                }
            }
        }

        [WebMethod]
        public static List<object> GetChartData1(string FROMDATE, string TODATE)
        {
            FROMDATE = Convert.ToDateTime(FROMDATE).ToString("yyyy-MM-dd");
            TODATE = Convert.ToDateTime(TODATE).ToString("yyyy-MM-dd");
            //string query = "SELECT DISTINCT(B.USERNAME),COUNT(*) AS COUNT FROM TRAN_LEAD_DATA AS A WITH(NOLOCK) LEFT OUTER JOIN MST_USER AS B WITH(NOLOCK) ON A.ASSIGNTO = B.ID WHERE A.CREATEDATE >= '" + FROMDATE + " 00:00:00.000' AND A.CREATEDATE <= '" + TODATE + " 23:59:59:00' GROUP BY B.USERNAME";
            string query = "SP_LEAD_GENERATION";
            string constr = ConfigurationManager.ConnectionStrings["ConnStringSherpa"].ConnectionString;
            List<object> chartData = new List<object>();
            chartData.Add(new object[]
            {
        "USERNAME", "COUNT"
            });
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@FROMDATE", FROMDATE + " 00:00:00.000");
                    cmd.Parameters.AddWithValue("@TODATE", TODATE + " 23:59:59.000");
                    cmd.Parameters.AddWithValue("@ACTION", "CHARTASSIGNTOAGENT");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            chartData.Add(new object[]
                            {
                        sdr["USERNAME"], sdr["COUNT"]
                            });
                        }
                    }
                    con.Close();
                    return chartData;
                }
            }
        }

        [WebMethod]
        public static List<object> GetChartData2(string FROMDATE, string TODATE)
        {

            FROMDATE = Convert.ToDateTime(FROMDATE).ToString("yyyy-MM-dd");
            TODATE = Convert.ToDateTime(TODATE).ToString("yyyy-MM-dd");

            //string query = "select DISTINCT CAST(CREATEDATE AS DATE) AS DT,COUNT(*) COUNT FROM TRAN_LEAD_DATA WHERE CREATEDATE >= DATEADD(day, -30, GETDATE()) GROUP BY CAST(CREATEDATE AS DATE) ORDER BY CAST(CREATEDATE AS DATE)";
            //string query = "SELECT DISTINCT CAST(AA.CREATEDATE AS DATE) AS DT,SUM(GCNT) AS LGCNT,SUM(ACNT) AS ATTCNT FROM (SELECT CREATEDATE,COUNT(*) GCNT," +
            //    "SUM(CASE WHEN STATUS <> 57 THEN 1 ELSE 0 END) ACNT FROM TRAN_LEAD_DATA WHERE CREATEDATE >= '" + FROMDATE + " 00:00:00.000' AND CREATEDATE <= '" + TODATE + " 23:59:59.000' " +
            //    "GROUP BY CREATEDATE) AS AA GROUP BY CAST(AA.CREATEDATE AS DATE) ORDER BY CAST(AA.CREATEDATE AS DATE)";
            string query = "SP_LEAD_GENERATION";
            string constr = ConfigurationManager.ConnectionStrings["ConnStringSherpa"].ConnectionString;
            List<object> chartData = new List<object>();
            chartData.Add(new object[]
            {
        "DT", "Lead Generated","Call Attempted"
            });
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@FROMDATE", FROMDATE + " 00:00:00.000");
                    cmd.Parameters.AddWithValue("@TODATE", TODATE + " 23:59:59.000");
                    cmd.Parameters.AddWithValue("@ACTION", "CHARTLEADCALL");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            chartData.Add(new object[]
                            {
                        Convert.ToDateTime(sdr["DT"]).ToShortDateString(), sdr["LGCNT"],sdr["ATTCNT"]
                            });
                        }
                    }
                    con.Close();
                    return chartData;
                }
            }
        }


        [WebMethod]
        public static List<object> GetChartData3(string FROMDATE, string TODATE)
        {
            FROMDATE = Convert.ToDateTime(FROMDATE).ToString("yyyy-MM-dd");
            TODATE = Convert.ToDateTime(TODATE).ToString("yyyy-MM-dd");
            //string query = "SELECT DISTINCT(B.USERNAME),COUNT(*) AS COUNT FROM TRAN_LEAD_DATA AS A WITH(NOLOCK) LEFT OUTER JOIN MST_USER AS B WITH(NOLOCK) ON A.ASSIGNTO = B.ID WHERE A.CREATEDATE >= '" + FROMDATE + " 00:00:00.000' AND A.CREATEDATE <= '" + TODATE + " 23:59:59:00' GROUP BY B.USERNAME";
            string query = "SP_LEAD_GENERATION";
            string constr = ConfigurationManager.ConnectionStrings["ConnStringSherpa"].ConnectionString;
            List<object> chartData = new List<object>();
            chartData.Add(new object[]
            {
        "USERNAME", "AVGCALL"
            });
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@FROMDATE", FROMDATE + " 00:00:00.000");
                    cmd.Parameters.AddWithValue("@TODATE", TODATE + " 23:59:59.000");
                    cmd.Parameters.AddWithValue("@ACTION", "CHARTAVGCALL");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            chartData.Add(new object[]
                            {
                        sdr["USERNAME"], sdr["AVGCALL"]
                            });
                        }
                    }
                    con.Close();
                    return chartData;
                }
            }
        }


    }
}