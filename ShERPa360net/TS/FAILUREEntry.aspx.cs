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
using Newtonsoft.Json;
namespace ShERPa360net.TS
{
    public partial class FAILUREEntry : System.Web.UI.Page
    {
        MainClass objMainClass          = new MainClass();
        DALUserRights objDALUserRights  = new DALUserRights();
        BindDDL objBindDDL              = new BindDDL();

        #region PAGEEVENT
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
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        BindPageDropDown();
                        BindFailureRepairGrid();
                        ResetFormControl();
                        ScriptManager.GetCurrent(this.Page).SetFocus(this.txtEsnNo);

                        //txtEsnNo.Focus();
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
                    BindFailureRepairGrid();
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

        #region MASTEREVENT
        protected void imgSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                if (Session["USERID"] != null)
                {
                    result = objMainClass.SAVEFAILUREDETAIL(txtAssignmentNo.Text, txtFailureDate.Text,
                        Convert.ToInt32(ddlFailureStage.SelectedValue), Convert.ToInt32(ddlFailFault.SelectedValue),
                        Convert.ToInt32(ddlInspector.SelectedValue),
                        Convert.ToString(Session["USERID"])
                        );
                    if (result > 0)
                    {
                        ResetFormControl();
                        BindFailureRepairGrid();
                    }
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
                BindFailureRepairGrid();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void gvAssignment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Cancels")
                {
                    //GridViewRow gRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    //Label lblAssignmentNo = (Label)gRow.FindControl("lblAssignmentNo");
                    //Label lblNDSNO = (Label)gRow.FindControl("lblNDSNO");
                    //objMainClass.SAVETATASKYJOBASSIGNMENT(lblAssignmentNo.Text, lblNDSNO.Text, string.Empty, string.Empty, txtDate.Text, txtTime.Text,
                    //                            Convert.ToInt64(ddlEngineer.SelectedValue), Convert.ToInt64(ddlModel.SelectedValue),
                    //                            Convert.ToInt64(ddlCondition.SelectedValue), 0,
                    //                            Convert.ToInt64(ddlRepair.SelectedValue), txtReceivedDate.Text, txtPreScanDate.Text,
                    //                            Convert.ToInt64(ddlPreScanProblem.SelectedValue), string.Empty, string.Empty, txtCIDReason.Text,
                    //                            "CANCEL", Convert.ToString(Session["USERID"]));
                    //BindFailureRepairGrid();
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Assignment No. :" + lblAssignmentNo.Text + " NDS No. :" + lblNDSNO.Text + " cancelled sucessfully." + "\");", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void txtEsnNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (txtEsnNo.Text.Length == 12)
                    {
                        var dtAssigmentDetail = objMainClass.GETTATASKYJOBASSIGNMENTFORPRESCANREPAIRSTAGE(txtEsnNo.Text, "FAILUREEACHSEARCH");
                        if (dtAssigmentDetail.Rows.Count > 0)
                        {
                            ddlModel.SelectedValue                      = dtAssigmentDetail.Rows[0]["MODELSKEY"].ToString();
                            txtISPCode.Text                             = dtAssigmentDetail.Rows[0]["ISPFAULTCODE"].ToString();
                            txtISPFault.Text                            = dtAssigmentDetail.Rows[0]["ISPFAULTVALUE"].ToString();
                            txtCIDReason.Text                           = dtAssigmentDetail.Rows[0]["CIDREASON"].ToString();
                            txtNDSNO.Text                               = dtAssigmentDetail.Rows[0]["NDSNO"].ToString();
                            txtAssignmentNo.Text                        = dtAssigmentDetail.Rows[0]["ASSIGNMENTNO"].ToString();
                            txtReceivedDate.Text                        = dtAssigmentDetail.Rows[0]["RECEIVEDATE"].ToString();
                            txtPreScanDate.Text                         = dtAssigmentDetail.Rows[0]["PRESCANNINGDATE"].ToString();
                            ddlPreScanProblem.SelectedValue             = dtAssigmentDetail.Rows[0]["PRESCANNINGPROBLEMKEY"].ToString();
                            ddlPreScanEngineer.SelectedValue            = dtAssigmentDetail.Rows[0]["PRESCANNINGENGINEERKEY"].ToString();
                            ddlEngineer.SelectedValue                   = dtAssigmentDetail.Rows[0]["ENGINEERKEY"].ToString();
                            txtRepairDate.Text                          = dtAssigmentDetail.Rows[0]["REPAIRDATE"].ToString();
                            ddlRepairTechName.SelectedValue             = dtAssigmentDetail.Rows[0]["REPARIENGINEERKEY"].ToString();
                            ddlObjectPartDescription.SelectedValue      = dtAssigmentDetail.Rows[0]["OBJECTPARTKEY"].ToString();
                            ddlDamageDescription.SelectedValue          = dtAssigmentDetail.Rows[0]["FAULTOBSERVEDKEY"].ToString();
                            ddlCauseDescription.SelectedValue           = dtAssigmentDetail.Rows[0]["FAULTREASONKEY"].ToString();
                            ddlAction.SelectedValue                     = dtAssigmentDetail.Rows[0]["ACTIONKEY"].ToString();
                            ddlRepariTask.SelectedValue                 = dtAssigmentDetail.Rows[0]["REPARITASKDESCRIPTIONKEY"].ToString();
                            txtFailureDate.Text                         = (dtAssigmentDetail.Rows[0]["FAILUREENTRYDATE"].ToString().Length == 0 ?  objMainClass.indianTime.Date.ToString("dd-MM-yyyy") : dtAssigmentDetail.Rows[0]["FAILUREENTRYDATE"].ToString());
                            ddlFailureStage.SelectedValue               = dtAssigmentDetail.Rows[0]["FAILURESTAGEKEY"].ToString();
                            ddlFailFault.SelectedValue                  = dtAssigmentDetail.Rows[0]["FAILUREFAULTEKEY"].ToString();
                            ddlInspector.SelectedValue                  = dtAssigmentDetail.Rows[0]["INSPECTORYKEY"].ToString();
                            lblESNExitalert.Text                        = "ESN Notification Entry Not Available.";
                            lblESNExitalert.Visible                     = false;
                        }
                        else
                        {
                            ddlModel.SelectedValue = "0";
                            txtISPCode.Text = string.Empty;
                            txtISPFault.Text = string.Empty;
                            txtCIDReason.Text = string.Empty;
                            txtNDSNO.Text = string.Empty;
                            txtAssignmentNo.Text = string.Empty;
                            txtReceivedDate.Text = string.Empty;
                            txtPreScanDate.Text = string.Empty;
                            ddlPreScanProblem.SelectedValue = "0";
                            ddlPreScanEngineer.SelectedValue = "0";
                            ddlEngineer.SelectedValue = "0";
                            lblESNExitalert.Text = "ESN Notification Entry Not Available.";
                            lblESNExitalert.Visible = true;
                            txtRepairDate.Text = string.Empty;
                            ddlRepairTechName.SelectedValue = "0";
                            ddlObjectPartDescription.SelectedValue = "0";
                            ddlDamageDescription.SelectedValue = "0";
                            ddlCauseDescription.SelectedValue = "0";
                            ddlAction.SelectedValue = "0";
                            ddlRepariTask.SelectedValue = "0";
                            txtFailureDate.Text           = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");
                            ddlFailureStage.SelectedValue = "0";
                            ddlFailFault.SelectedValue    = "0";
                            ddlInspector.SelectedValue    = "0";
                            txtEsnNo.Focus();
                        }
                    }
                    else
                    {
                        lblESNExitalert.Text = "Please Enter valid ESN No.";
                        lblESNExitalert.Visible = true;
                    }
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
        #endregion


        #endregion

        #region PAGEMETHOD

        #region MASTER
        public void BindPageDropDown()
            {
                try
                {
                if (Session["USERID"] != null)
                {
                    //Model
                    objBindDDL.FillTaTaSkyReqDropDown(ddlModel, "MODELS");
                    ddlModel.SelectedValue = "0";

                    //Prescan Prob
                    objBindDDL.FillTaTaSkyReqDropDown(ddlPreScanProblem, "PRESCANPROBLEM");
                    ddlPreScanProblem.SelectedValue = "0";

                    // Bind the PreScan Engineer 
                    objBindDDL.FillTaTaSkyReqDropDown(ddlPreScanEngineer, "", "EMPLOYEE");
                    ddlPreScanEngineer.SelectedValue = "0";

                    //Tech Allocation Engineer
                    objBindDDL.FillTaTaSkyReqDropDown(ddlEngineer, "", "EMPLOYEE");
                    ddlEngineer.SelectedValue = "0";

                    // Bind the Repair Tech Engineer 
                    objBindDDL.FillTaTaSkyReqDropDown(ddlRepairTechName, "", "EMPLOYEE");
                    ddlRepairTechName.SelectedValue = "0";

                    // Bind the Object Part Description 
                    objBindDDL.FillTaTaSkyReqDropDown(ddlObjectPartDescription, "OBJECTPART");
                    ddlObjectPartDescription.SelectedValue = "0";

                    // Bind the Damage Description 
                    objBindDDL.FillTaTaSkyReqDropDown(ddlDamageDescription, "PRESCANPROBLEM");
                    ddlDamageDescription.SelectedValue = "0";

                    // Bind the Cause Description 
                    objBindDDL.FillTaTaSkyReqDropDown(ddlCauseDescription, "FAULTREASON");
                    ddlCauseDescription.SelectedValue = "0";

                    // Bind the Action 
                    objBindDDL.FillTaTaSkyReqDropDown(ddlAction, "ACTION");
                    ddlAction.SelectedValue = "0";

                    // Bind the Repair Task Description 
                    objBindDDL.FillTaTaSkyReqDropDown(ddlRepariTask, "STATUS");
                    ddlRepariTask.SelectedValue = "0";

                    // Bind the Failure Stage Detail 
                    objBindDDL.FillTaTaSkyReqDropDown(ddlFailureStage, "FAILURESTAGE");
                    ddlFailureStage.SelectedValue = "0";

                    // Bind the Fail Reason Detail 
                    objBindDDL.FillTaTaSkyReqDropDown(ddlFailFault, "FAILUREFAULT");
                    ddlFailFault.SelectedValue = "0";

                    //Inspector Engineer
                    objBindDDL.FillTaTaSkyReqDropDown(ddlInspector, "", "EMPLOYEE");
                    ddlInspector.SelectedValue = "0";
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

        public void BindFailureRepairGrid()
            {
                try
                {
                    if (Session["USERID"] != null)
                    {
                        DataTable dtFailureDetail     = new DataTable();
                        dtFailureDetail               = objMainClass.GETTATASKYJOBASSIGNMENTFORPRESCANREPAIRSTAGE(txtEsnNo.Text, "FAILURESEARCH");
                        gvAssignment.DataSource       = dtFailureDetail;
                        gvAssignment.DataBind();
                        lgrecordcount.InnerText       = "Records : " + dtFailureDetail.Rows.Count.ToString();
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
                        txtEsnNo.Text = string.Empty;
                        ddlModel.SelectedValue = "0";
                        txtISPCode.Text = string.Empty;
                        txtISPFault.Text = string.Empty;
                        txtCIDReason.Text = string.Empty;
                        txtNDSNO.Text = string.Empty;
                        txtAssignmentNo.Text = string.Empty;
                        txtReceivedDate.Text = string.Empty;
                        txtPreScanDate.Text = string.Empty;
                        txtRepairDate.Text  = string.Empty;
                        txtFailureDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");
                        ddlPreScanProblem.SelectedValue = "0";
                        ddlEngineer.SelectedValue = "0";
                        ddlPreScanEngineer.SelectedValue = "0";
                        ddlRepairTechName.SelectedValue = "0";
                        ddlObjectPartDescription.SelectedValue = "0";
                        ddlDamageDescription.SelectedValue = "0";
                        ddlCauseDescription.SelectedValue = "0";
                        ddlAction.SelectedValue = "0";
                        ddlRepariTask.SelectedValue = "0";
                        ddlFailureStage.SelectedValue = "0";
                        ddlFailFault.SelectedValue = "0";
                        ddlInspector.SelectedValue = "0";
                    ScriptManager.GetCurrent(this.Page).SetFocus(this.txtEsnNo);

                    //txtEsnNo.Focus();
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
        #endregion

        #endregion
    }
}