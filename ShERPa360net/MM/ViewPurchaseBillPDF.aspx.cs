using Microsoft.Reporting.WebForms;
using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.MM
{
    public partial class ViewPurchaseBillPDF : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();


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
                            //Session.Abandon();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        if (Request.QueryString.Count > 0)
                        {
                            if (Convert.ToString(Request.QueryString["PBNO"]) != null && Convert.ToString(Request.QueryString["PBNO"]) != string.Empty && Convert.ToString(Request.QueryString["PBNO"]) != "")
                            {
                                Session["rptPBNO"] = Convert.ToString(Request.QueryString["PBNO"]);
                            }
                            Response.Redirect(Request.Url.AbsolutePath, false);
                        }
                        else
                        {
                            if (Session["rptPBNO"] != null && Convert.ToString(Session["rptPBNO"]) != "" && Convert.ToString(Session["rptPBNO"]) != string.Empty)
                            {
                                DataTable dataset1 = new DataTable();
                                DataTable dataset2 = new DataTable();
                                DataTable dataset3 = new DataTable();
                                DataTable dataset4 = new DataTable();
                                dataset1 = objMainClass.PurchaseBillReport(Convert.ToString(Session["rptPBNO"]), "MASTER");
                                dataset2 = objMainClass.PurchaseBillReport(Convert.ToString(Session["rptPBNO"]), "CHARGES");
                                dataset3 = objMainClass.PurchaseBillReport(Convert.ToString(Session["rptPBNO"]), "TAX");
                                dataset4 = objMainClass.PurchaseBillReport(Convert.ToString(Session["rptPBNO"]), "TAXSUM");
                                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                                ReportViewer1.LocalReport.ReportPath = "Report/rptPBDoc.rdlc";
                                ReportDataSource rds1 = new ReportDataSource("DataSet1", dataset1);
                                ReportDataSource rds2 = new ReportDataSource("DataSet2", dataset2);
                                ReportDataSource rds3 = new ReportDataSource("DataSet3", dataset3);
                                ReportDataSource rds4 = new ReportDataSource("DataSet4", dataset4);
                                ReportViewer1.LocalReport.DataSources.Clear();
                                ReportViewer1.LocalReport.DataSources.Add(rds1);
                                ReportViewer1.LocalReport.DataSources.Add(rds2);
                                ReportViewer1.LocalReport.DataSources.Add(rds3);
                                ReportViewer1.LocalReport.DataSources.Add(rds4);
                                ReportViewer1.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        Session.Abandon();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
                }
            }
        }
    }
}