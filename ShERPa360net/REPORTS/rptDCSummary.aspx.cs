using Microsoft.Reporting.WebForms;
using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.REPORTS
{
    public partial class rptDCSummary : System.Web.UI.Page
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

        protected void lnkGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    DataTable dt1 = new DataTable();
                    DataTable dt2 = new DataTable();
                    DataTable dt3 = new DataTable();
                    dt = objMainClass.GetDCReport(txtDCNo.Text, rblStatus.SelectedItem.Text, Convert.ToDecimal(txtAmount.Text), rblShipped.SelectedItem.Text, txtHSNNo.Text, "MAIN", txtNoOfBOX.Text);
                    dt1 = objMainClass.GetDCReport(txtDCNo.Text, rblStatus.SelectedItem.Text, Convert.ToDecimal(txtAmount.Text), rblShipped.SelectedItem.Text, txtHSNNo.Text, "MODELRPT", txtNoOfBOX.Text);
                    dt2 = objMainClass.GetDCReport(txtDCNo.Text, rblStatus.SelectedItem.Text, Convert.ToDecimal(txtAmount.Text), rblShipped.SelectedItem.Text, txtHSNNo.Text, "PORPT", txtNoOfBOX.Text);
                    dt3 = objMainClass.GetDCReport(txtDCNo.Text, rblStatus.SelectedItem.Text, Convert.ToDecimal(txtAmount.Text), rblShipped.SelectedItem.Text, txtHSNNo.Text, "DCDATE", txtNoOfBOX.Text);
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;

                    ReportViewer1.LocalReport.ReportPath = "Report/rptJIODC.rdlc";
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportDataSource rds1 = new ReportDataSource("DataSet2", dt1);
                    ReportDataSource rds2 = new ReportDataSource("DataSet3", dt2);
                    ReportDataSource rds3 = new ReportDataSource("DataSet4", dt3);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                    ReportViewer1.LocalReport.DataSources.Add(rds1);
                    ReportViewer1.LocalReport.DataSources.Add(rds2);
                    ReportViewer1.LocalReport.DataSources.Add(rds3);
                    ReportViewer1.Visible = true;

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