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
    public partial class ViewPOPDF : System.Web.UI.Page
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
                            if (Convert.ToString(Request.QueryString["PONO"]) != null && Convert.ToString(Request.QueryString["PONO"]) != string.Empty && Convert.ToString(Request.QueryString["PONO"]) != "")
                            {
                                Session["rptPONo"] = Convert.ToString(Request.QueryString["PONO"]);
                            }
                            Response.Redirect(Request.Url.AbsolutePath, false);
                        }
                        else
                        {

                            if (Session["rptPONo"] != null && Convert.ToString(Session["rptPONo"]) != "" && Convert.ToString(Session["rptPONo"]) != string.Empty)
                            {

                                DataTable dataset1 = new DataTable();
                                DataTable dataset2 = new DataTable();
                                DataTable dataset3 = new DataTable();
                                DataTable dataset4 = new DataTable();
                                dataset1 = objMainClass.PurchaseOrderReport(objMainClass.intCmpId, Convert.ToString(Session["rptPONo"]), "PODTL");
                                dataset2 = objMainClass.PurchaseOrderReport(objMainClass.intCmpId, Convert.ToString(Session["rptPONo"]), "CHARGES");
                                dataset3 = objMainClass.PurchaseOrderReport(objMainClass.intCmpId, Convert.ToString(Session["rptPONo"]), "POCOND");
                                dataset4 = objMainClass.PurchaseOrderReport(objMainClass.intCmpId, Convert.ToString(Session["rptPONo"]), "MRPR");
                                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                                ReportViewer1.LocalReport.ReportPath = "Report/rptPODoc.rdlc";
                                ReportDataSource rds1 = new ReportDataSource("DataSet1", dataset1);
                                ReportDataSource rds2 = new ReportDataSource("DataSet2", dataset2);
                                ReportDataSource rds3 = new ReportDataSource("DataSet4", dataset3);
                                ReportDataSource rds4 = new ReportDataSource("DataSet5", dataset4);
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