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
    public partial class ViewInwardPDF : System.Web.UI.Page
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
                            if (Convert.ToString(Request.QueryString["DOCNO"]) != null && Convert.ToString(Request.QueryString["DOCNO"]) != string.Empty && Convert.ToString(Request.QueryString["DOCNO"]) != "")
                            {
                                Session["rptDOCNO"] = Convert.ToString(Request.QueryString["DOCNO"]);
                            }
                            Response.Redirect(Request.Url.AbsolutePath, false);
                        }
                        else
                        {
                            if (Session["rptDOCNO"] != null && Convert.ToString(Session["rptDOCNO"]) != "" && Convert.ToString(Session["rptDOCNO"]) != string.Empty)
                            {
                                DataTable dt = new DataTable();
                                dt = objMainClass.INWARDREPORT(objMainClass.intCmpId, Convert.ToString(Session["rptDOCNO"]));
                                ReportViewer1.ProcessingMode = ProcessingMode.Local;

                                ReportViewer1.LocalReport.ReportPath = "Report/rptMMDoc.rdlc";
                                ReportDataSource rds = new ReportDataSource("dsMMDocs1", dt);
                                ReportViewer1.LocalReport.DataSources.Clear();
                                ReportViewer1.LocalReport.DataSources.Add(rds);
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