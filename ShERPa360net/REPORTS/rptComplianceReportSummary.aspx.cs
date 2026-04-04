using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.REPORTS
{
    public partial class rptComplianceReportSummary : System.Web.UI.Page
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

                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtTodate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        //objBindDDL.FillAreaQuestion(ddlArea, "", "", "AREA", "");
                        objBindDDL.FillPlant(ddlPlantCode);
                        string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                        ddlPlantCode.SelectedValue = plantArray[0];
                        objBindDDL.FillAreaQuestion(ddlQuestion, "", "", "QUE", "Compliance");
                        GETDATA();
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

        public void GETDATA()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetSafetyReport(0, "SUMMARY", "", txtFromDate.Text, txtTodate.Text, "", ddlPlantCode.SelectedValue, ddlQuestion.SelectedValue, Convert.ToInt32(ddlResult.SelectedValue), "Compliance");
                    if (dt.Rows.Count > 0)
                    {
                        grvComplianceReportSummary.DataSource = dt;
                        grvComplianceReportSummary.DataBind();
                    }
                    else
                    {
                        grvComplianceReportSummary.DataSource = string.Empty;
                        grvComplianceReportSummary.DataBind();
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

        protected void ddlPlantCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (ddlPlantCode.SelectedIndex > 0)
                    {
                        string PLantRights = string.Empty;
                        string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                        for (int i = 0; i < plantArray.Count(); i++)
                        {
                            if (plantArray[i].Trim() == ddlPlantCode.SelectedValue)
                            {
                                PLantRights = ddlPlantCode.SelectedValue;
                            }
                        }

                        if (PLantRights.Length > 0)
                        {

                        }
                        else
                        {
                            ddlPlantCode.SelectedValue = plantArray[0];
                            ddlPlantCode.Focus();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have plant rights.');", true);
                            ddlPlantCode.SelectedValue = plantArray[0];
                            ddlPlantCode.Focus();
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

        //protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Session["USERID"] != null)
        //        {
        //            if (ddlArea.SelectedIndex > 0)
        //            {
        //                objBindDDL.FillAreaQuestion(ddlQuestion, ddlArea.SelectedValue, "", "QUESTION", "Compliance");
        //            }
        //            else
        //            {
        //                objBindDDL.FillAreaQuestion(ddlQuestion, "", "", "QUE", "Compliance");
        //                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Select Area!');", true);
        //            }
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
        //    }
        //}

        protected void lnkSearhSR_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GETDATA();
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

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void lnkExport_Click(object sender, EventArgs e)
        {
            try
            {
                string attachment = "attachment; filename=ComplianceReportSummary" + DateTime.Now + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vdn.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                grvComplianceReportSummary.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}