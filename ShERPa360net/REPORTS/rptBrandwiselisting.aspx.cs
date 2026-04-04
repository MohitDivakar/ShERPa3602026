using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.REPORTS
{
    public partial class rptBrandwiselisting : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
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

                        string[] date = objMainClass.indianTime.Date.ToString("dd-MM-yyyy").Split('-');
                        DateTime fromdate = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), 1);
                        txtFromDocDate.Text = objMainClass.indianTime.Date.AddDays(-15).ToString("dd-MM-yyyy");
                        txtToDocDate.Text   = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");
                       //objBindDDL.FillState(ddlState);
                         objBindDDL.FillPlant(ddlPlantCode);
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

        public void GetData(string ACTION, string FROMDATE, string TODATE, int SEARCHBY, string CITY)
        {
            //GetBikerVisitReport
            if (Session["USERID"] != null)
            {
                try
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetBikerRejectionReport(ACTION, FROMDATE, TODATE, SEARCHBY, CITY);
                    if (dt.Rows.Count > 0)
                    {
                    }
                }
                catch (Exception ex)
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true); ;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
            }
        }

        protected void lnkSerch_Click(object sender, EventArgs e)
        {
            if (Session["USERID"] != null)
            {
                try
                {
                    //GetData("REPORT", txtFromDocDate.Text, txtToDocDate.Text, Convert.ToInt32(Session["USERID"]),ddlCity.SelectedValue);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true); ;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        [WebMethod]
        public static ChartData GetChartData(string frmdate, string todate, string plantid)
        {
            // Get the data from database.
            try
            {
                MainClass objMainClass = new MainClass();
                ChartData chartData = new ChartData();

                DataTable dt = new DataTable();
                dt = objMainClass.GetBrandwidelisting(frmdate, todate, Convert.ToInt32(plantid));


                //chartData.Labels = dt.AsEnumerable().Select(x => x.Field<string>("ReportDate")).ToArray();
                chartData.Labels = new string[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    chartData.Labels[i] = dt.Rows[i]["ReportDate"].ToString();    
                }
                chartData.DatasetLabels = new string[] { "ANDROID", "APPLE" };
                List<int[]> datasetDatas = new List<int[]>();

                List<int> motorcycles = new List<int>();
                List<int> bicycles = new List<int>();
                foreach (DataRow dr in dt.Rows)
                {
                    motorcycles.Add(Convert.ToInt32(dr["SamsungCount"]));
                    bicycles.Add(Convert.ToInt32(dr["AppleCount"]));
                }

                datasetDatas.Add(motorcycles.ToArray());
                datasetDatas.Add(bicycles.ToArray());
                chartData.DatasetDatas = datasetDatas;

                //DataTable dt = new DataTable();
                //dt.Columns.AddRange(new DataColumn[] {
                //new DataColumn("Month"),new DataColumn("Motorcycles"),new DataColumn("Bicycles") });
                //dt.Rows.Add("January", 30, 65);
                //dt.Rows.Add("February", 50, 60);
                //dt.Rows.Add("March", 40, 81);
                //dt.Rows.Add("April", 20, 80);
                //dt.Rows.Add("May", 80, 60);
                //dt.Rows.Add("June", 30, 60);

                //ChartData chartData         = new ChartData();
                //chartData.Labels            = dt.AsEnumerable().Select(x => x.Field<string>("Month")).ToArray();
                //chartData.DatasetLabels     = new string[] { "Motorcycles", "Bicycles" };
                //List<int[]> datasetDatas    = new List<int[]>();

                //List<int> motorcycles       = new List<int>();
                //List<int> bicycles          = new List<int>();
                //foreach (DataRow dr in dt.Rows)
                //{
                //    motorcycles.Add(Convert.ToInt32(dr["Motorcycles"]));
                //    bicycles.Add(Convert.ToInt32(dr["Bicycles"]));
                //}

                //datasetDatas.Add(motorcycles.ToArray());
                //datasetDatas.Add(bicycles.ToArray());
                //chartData.DatasetDatas = datasetDatas;
                return chartData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public class ChartData
        {
            public string[] Labels { get; set; }
            public string[] DatasetLabels { get; set; }
            public List<int[]> DatasetDatas { get; set; }
        }
    }
}