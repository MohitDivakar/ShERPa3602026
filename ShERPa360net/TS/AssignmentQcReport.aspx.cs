using Newtonsoft.Json;
using ShERPa360net.Class;
using ShERPa360net.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.TS
{
    public partial class AssignmentQcReport : System.Web.UI.Page
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
                    txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    if (Session["USERID"] != null)
                    {
                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menutabid.Value, "");
                        if (FormRights.bView == false)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        BindPageDropDown();
                        BindGridForAssignmentQcReport();
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

        protected void lnkSearh_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    BindGridForAssignmentQcReport();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void imgReset_Click(object sender, EventArgs e)
        {
            try
            {
                ResetFormControl();
                BindGridForAssignmentQcReport();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lnkExport_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = "Qc Report" + txtFromDate.Text + "-" + txtToDate.Text;
                string attachment = "attachment; filename=" + filename + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vdn.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gvAssignmentQcRepor.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        #region PAGEMETHOD
        public void BindPageDropDown()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    objBindDDL.FillTaTaSkyReqDropDown(ddlEngineer, "", "EMPLOYEE", "Search");
                    ddlEngineer.SelectedValue = "0";

                    objBindDDL.FillTaTaSkyReqDropDown(ddlModel, "MODELS", "REQUESTDROPDOWN", "Search");
                    ddlModel.SelectedValue = "0";

                    objBindDDL.FillTaTaSkyReqDropDown(ddlCondition, "CONDITION", "REQUESTDROPDOWN", "Search");
                    ddlCondition.SelectedValue = "0";

                    objBindDDL.FillTaTaSkyReqDropDown(ddlRepair, "REPAIR", "REQUESTDROPDOWN", "Search");
                    ddlRepair.SelectedValue = "0";

                    objBindDDL.FillTaTaSkyReqDropDown(ddlStatus, "STATUS", "REQUESTDROPDOWN", "Search");
                    ddlRepair.SelectedValue = "0";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public void BindGridForAssignmentQcReport()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GETDAILYREPORT(txtFromDate.Text, txtToDate.Text, txtAssignmentNo.Text, txtNDSNO.Text, txtEsnNo.Text,
                                                                  Convert.ToInt64(ddlEngineer.SelectedValue), Convert.ToInt64(ddlModel.SelectedValue),
                                                                  Convert.ToInt64(ddlCondition.SelectedValue), Convert.ToInt64(ddlRepair.SelectedValue)
                                                                  ,Convert.ToInt32(ddlStatus.SelectedValue));
                    gvAssignmentQcRepor.DataSource = dt;
                    gvAssignmentQcRepor.DataBind();
                    lgrecordcount.InnerText = "Records : " + dt.Rows.Count.ToString();
                    //if (ddlStatus.SelectedValue == "WORKING")
                    //{
                    //    gvAssignmentQcRepor.Columns[2].Visible = true;
                    //    gvAssignmentQcRepor.Columns[3].Visible = true;
                    //    gvAssignmentQcRepor.Columns[4].Visible = false;
                    //}
                    //else
                    //{
                    //    gvAssignmentQcRepor.Columns[2].Visible = false;
                    //    gvAssignmentQcRepor.Columns[3].Visible = false;
                    //    gvAssignmentQcRepor.Columns[4].Visible = true;
                    //}
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public void ResetFormControl()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    txtAssignmentNo.Text = string.Empty;
                    txtNDSNO.Text = string.Empty;
                    txtEsnNo.Text = string.Empty;
                    ddlEngineer.SelectedValue = "0";
                    ddlModel.SelectedValue = "0";
                    ddlCondition.SelectedValue = "0";
                    ddlRepair.SelectedValue = "0";
                    ddlStatus.SelectedValue = "WORKING";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='Login.aspx' });", true);
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

        #endregion


    }
}