using Microsoft.Reporting.WebForms;
using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Microsoft.Reporting.Microsoft.Reporting.WebForms.HttpHandler;



namespace ShERPa360net
{
    public partial class ViewReport : System.Web.UI.Page
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
                            if (Convert.ToString(Request.QueryString["PRNO"]) != null && Convert.ToString(Request.QueryString["PRNO"]) != string.Empty && Convert.ToString(Request.QueryString["PRNO"]) != "")
                            {
                                Session["rptPRNo"] = Convert.ToString(Request.QueryString["PRNO"]);
                            }
                            Response.Redirect(Request.Url.AbsolutePath, false);
                        }
                        else
                        {
                            if (Session["rptPRNo"] != null && Convert.ToString(Session["rptPRNo"]) != "" && Convert.ToString(Session["rptPRNo"]) != string.Empty)
                            {
                                DataTable dt = new DataTable();
                                dt = objMainClass.PRREPORT("1", Convert.ToString(Session["rptPRNo"]));
                                ReportViewer1.ProcessingMode = ProcessingMode.Local;

                                ReportViewer1.LocalReport.ReportPath = "Report/rptPRDTL.rdlc";
                                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                                ReportViewer1.LocalReport.DataSources.Clear();
                                ReportViewer1.LocalReport.DataSources.Add(rds);
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