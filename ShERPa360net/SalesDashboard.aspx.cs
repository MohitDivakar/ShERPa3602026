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
using System.Web.UI.DataVisualization.Charting;
using System.Web.Script.Serialization;

namespace ShERPa360net
{
    public partial class SalesDashboard : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        WAClass objWAClass = new WAClass();

        protected decimal GaugeValue = 0; // This can come from SQL
        protected decimal GaugeValue2 = 0; // This can come from SQL
        protected decimal GaugeValue3 = 0; // This can come from SQL
        protected decimal GaugeValue4 = 0; // This can come from SQL
        protected decimal GaugeValue5 = 0; // This can come from SQL
        protected decimal GaugeValue6 = 0; // This can come from SQL
        protected decimal GaugeValue7 = 0; // This can come from SQL

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                try
                {
                    if (Session["USERID"] != null)
                    {
                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menutabid.Value, "");
                        if (FormRights.bView == false)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        //string[] date = objMainClass.indianTime.Date.ToString("dd-MM-yyyy").Split('-');
                        //DateTime fromdate = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), 1);
                        //txtFromDate.Text = fromdate.ToString("dd-MM-yyyy");
                        //txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();

                        GetData();
                        //GetData2();
                        //GetData3();

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
                }
            }
        }


        public void GetData()
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    string[] date = objMainClass.indianTime.Date.ToString("dd-MM-yyyy").Split('-');
                    DateTime fromdate = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), 1);
                    string thismonth = fromdate.ToString();
                    string today = DateTime.Now.ToShortDateString();

                    DateTime first = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 1);
                    DateTime last = first.AddMonths(1).AddSeconds(-1);
                    string lastmonthfirst = first.ToShortDateString();
                    string lastmonthlast = last.ToShortDateString();


                    DataTable dt = new DataTable();
                    dt = objMainClass.SalesDashBoard(objMainClass.intCmpId, today, thismonth, lastmonthfirst, lastmonthlast, "DASHBOARDNEW");
                    if (dt.Rows.Count > 0)
                    {
                        gvList.DataSource = dt;
                        gvList.DataBind();
                    }
                    else
                    {
                        gvList.DataSource = string.Empty;
                        gvList.DataBind();
                    }

                    decimal todays = 0;
                    DataTable dtToday = new DataTable();
                    dtToday = objMainClass.SalesDashBoard(objMainClass.intCmpId, today, thismonth, lastmonthfirst, lastmonthlast, "TODAYSALES");
                    if (dtToday.Rows.Count > 0)
                    {
                        todays = Convert.ToDecimal(dtToday.Rows[0]["SALE"]);
                    }
                    lblTodaysCount.Text = todays.ToString("N0") + "  Rs.";
                    GaugeValue = todays;

                    decimal thismonths = 0;
                    DataTable dtthismonths = new DataTable();
                    dtthismonths = objMainClass.SalesDashBoard(objMainClass.intCmpId, today, thismonth, lastmonthfirst, lastmonthlast, "THISMONTH");
                    if (dtthismonths.Rows.Count > 0)
                    {
                        thismonths = Convert.ToDecimal(dtthismonths.Rows[0]["SALE"]);
                    }
                    lblThisMonthCount.Text = thismonths.ToString("N0") + "  Rs.";
                    GaugeValue2 = thismonths;

                    decimal lastmonths = 0;
                    DataTable dtlastmonths = new DataTable();
                    dtlastmonths = objMainClass.SalesDashBoard(objMainClass.intCmpId, today, thismonth, lastmonthfirst, lastmonthlast, "LASTMONTH");
                    if (dtlastmonths.Rows.Count > 0)
                    {
                        lastmonths = Convert.ToDecimal(dtlastmonths.Rows[0]["SALE"]);
                    }
                    lblLastMonthCount.Text = lastmonths.ToString("N0") + "  Rs.";
                    GaugeValue3 = lastmonths;

                    decimal thismonthslr = 0;
                    DataTable dtthismonthslr = new DataTable();
                    dtthismonthslr = objMainClass.SalesDashBoard(objMainClass.intCmpId, today, thismonth, lastmonthfirst, lastmonthlast, "THISMONTHSLR");
                    if (dtthismonthslr.Rows.Count > 0)
                    {
                        thismonthslr = Convert.ToDecimal(dtthismonthslr.Rows[0]["SALE"]);
                    }
                    lblThisMonthSalrCount.Text = thismonthslr.ToString("N0") + "  Rs.";
                    GaugeValue4 = thismonthslr;


                    DataTable dtPurchase = new DataTable();
                    dtPurchase = objMainClass.SalesDashBoard(objMainClass.intCmpId, today, thismonth, lastmonthfirst, lastmonthlast, "DASHBOARDPURCHASE");
                    if (dtPurchase.Rows.Count > 0)
                    {
                        grvPurchase.DataSource = dtPurchase;
                        grvPurchase.DataBind();
                    }
                    else
                    {
                        grvPurchase.DataSource = string.Empty;
                        grvPurchase.DataBind();
                    }



                    decimal todayespurchase = 0;
                    DataTable dttodayespurchase = new DataTable();
                    dttodayespurchase = objMainClass.SalesDashBoard(objMainClass.intCmpId, today, thismonth, lastmonthfirst, lastmonthlast, "TODAYPURCHASE");
                    if (dttodayespurchase.Rows.Count > 0)
                    {
                        todayespurchase = Convert.ToDecimal(dttodayespurchase.Rows[0]["PURCHASE"]);
                    }
                    lblTodaysPurchase.Text = todayespurchase.ToString("N0") + "  Rs.";
                    GaugeValue5 = todayespurchase;

                    decimal thismonthpurchase = 0;
                    DataTable dtthismonthpurchase = new DataTable();
                    dtthismonthpurchase = objMainClass.SalesDashBoard(objMainClass.intCmpId, today, thismonth, lastmonthfirst, lastmonthlast, "THISMONTHPURCHASE");
                    if (dtthismonthpurchase.Rows.Count > 0)
                    {
                        thismonthpurchase = Convert.ToDecimal(dtthismonthpurchase.Rows[0]["PURCHASE"]);
                    }
                    lblThisMonthPurchase.Text = thismonthpurchase.ToString("N0") + "  Rs.";
                    GaugeValue6 = thismonthpurchase;

                    decimal lastmonthspurchase = 0;
                    DataTable dtlastmonthspurchase = new DataTable();
                    dtlastmonthspurchase = objMainClass.SalesDashBoard(objMainClass.intCmpId, today, thismonth, lastmonthfirst, lastmonthlast, "LASTMONTHPURCHASE");
                    if (dtlastmonthspurchase.Rows.Count > 0)
                    {
                        lastmonthspurchase = Convert.ToDecimal(dtlastmonthspurchase.Rows[0]["PURCHASE"]);
                    }
                    lblLastMonthPurchase.Text = lastmonthspurchase.ToString("N0") + "  Rs.";
                    GaugeValue7 = lastmonthspurchase;




                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        [WebMethod]
        public static List<object> BarChart1()
        {
            List<object> chartData = new List<object>();
            chartData.Add(new object[]
            {
        "PLANTCD", "PURCHASE"
            });

            MainClass objMainClass = new MainClass();

            string[] date = objMainClass.indianTime.Date.ToString("dd-MM-yyyy").Split('-');
            DateTime fromdate = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), 1);
            string thismonth = fromdate.ToString();
            string today = DateTime.Now.ToShortDateString();


            DataTable dt = new DataTable();
            dt = objMainClass.SalesDashBoard(objMainClass.intCmpId, today, thismonth, "", "", "TODAYPLANTWISEPURCHASE");

            foreach (DataRow row in dt.Rows)
            {
                chartData.Add(new object[]
                           {
                        Convert.ToString(row["PLANTCD"]), Convert.ToDecimal(row["PURCHASE"])
                           });
            }

            return chartData;
        }

        [WebMethod]
        public static List<object> BarChart2()
        {
            List<object> chartData = new List<object>();
            chartData.Add(new object[]
            {
        "PLANTCD", "PURCHASE"
            });

            MainClass objMainClass = new MainClass();

            string[] date = objMainClass.indianTime.Date.ToString("dd-MM-yyyy").Split('-');
            DateTime fromdate = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), 1);
            string thismonth = fromdate.ToString();
            string today = DateTime.Now.ToShortDateString();


            DataTable dt = new DataTable();
            dt = objMainClass.SalesDashBoard(objMainClass.intCmpId, today, thismonth, "", "", "THISMONTHPLANTWISEPURCHASE");

            foreach (DataRow row in dt.Rows)
            {
                chartData.Add(new object[]
                           {
                        Convert.ToString(row["PLANTCD"]), Convert.ToDecimal(row["PURCHASE"])
                           });
            }

            return chartData;
        }

    }
}