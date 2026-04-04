using ShERPa360net.Class;
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
    public partial class PrescanningEntry : System.Web.UI.Page
    {
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
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
                            //Session.Abandon();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        BindPageDropDown();
                        BindAssignmentGrid();
                        ResetFormControl();
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


        protected void imgSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                if (Session["USERID"] != null)
                {
                    if (Convert.ToString(Session["saveall"]) == "Save All")
                    {
                        //DataTable dt = new DataTable();
                        //dt = objMainClass.GETTATASKYJOBASSIGNMENT(string.Empty, txtNDSNO.Text, string.Empty, txtEsnNo.Text, Convert.ToInt64(ddlEngineer.SelectedValue),
                        //                                                               Convert.ToInt64(ddlModel.SelectedValue), Convert.ToInt64(ddlCondition.SelectedValue)
                        //                                                               , 0, Convert.ToInt64(ddlRepair.SelectedValue), "NDSNOAVAILABLE", Convert.ToInt64(Session["USERID"]));
                        //if (dt.Rows.Count > 0)
                        //{
                        //    if (dt.Rows[0]["AVAILABLE"].ToString() == "NO")
                        //    {
                                result = objMainClass.SAVEPRESCANDETAIL(txtAssignmentNo.Text,txtPreScanDate.Text,Convert.ToInt32(ddlPreScanProblem.SelectedValue), Convert.ToInt32(ddlPreScanEngineer.SelectedValue),Convert.ToInt32(ddlEngineer.SelectedValue), Convert.ToString(Session["USERID"]));
                                if (result > 0)
                                {
                                    ResetFormControl();
                                    BindAssignmentGrid();
                                }
                            //}
                            //else
                            //{
                            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" NDSNO.: " + txtNDSNO.Text + " call is already assigment to " + dt.Rows[0]["ENGINEERNAME"].ToString() + " Engineer! " + "\");", true);
                            //}
                        //}
                    }
                    //else
                    //{
                    //    ASSIGNMENTNo = objMainClass.SAVETATASKYJOBASSIGNMENT(txtAssignmentNo.Text, txtNDSNO.Text, txtEsnNo.Text, string.Empty, txtDate.Text, txtTime.Text,
                    //                                               Convert.ToInt64(ddlEngineer.SelectedValue), Convert.ToInt64(ddlModel.SelectedValue),
                    //                                               Convert.ToInt64(ddlCondition.SelectedValue), 0,
                    //                                               Convert.ToInt64(ddlRepair.SelectedValue), txtReceivedDate.Text, txtPreScanDate.Text,
                    //                                               Convert.ToInt64(ddlPreScanProblem.SelectedValue), txtISPCode.Text, txtISPFault.Text,
                    //                                               txtCIDReason.Text, "UPDATE", Convert.ToString(Session["USERID"]));
                    //    if (ASSIGNMENTNo.Length > 0)
                    //    {
                    //        ResetFormControl();
                    //        BindAssignmentGrid();
                    //    }
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

        protected void imgReset_Click(object sender, EventArgs e)
        {
            try
            {
                ResetFormControl();
                BindAssignmentGrid();
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
                    //BindAssignmentGrid();
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Assignment No. :" + lblAssignmentNo.Text + " NDS No. :" + lblNDSNO.Text + " cancelled sucessfully." + "\");", true);
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
                    BindAssignmentGrid();
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

        protected void gvAssignment_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }
        #endregion

        #region PAGEMETHOD
        public void BindPageDropDown()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    objBindDDL.FillTaTaSkyReqDropDown(ddlEngineer, "", "EMPLOYEE");
                    ddlEngineer.SelectedValue = "0";

                    objBindDDL.FillTaTaSkyReqDropDown(ddlModel, "MODELS");
                    ddlModel.SelectedValue = "0";

                    //objBindDDL.FillTaTaSkyReqDropDown(ddlCondition, "CONDITION");
                    //ddlCondition.SelectedValue = "0";

                    //objBindDDL.FillTaTaSkyReqDropDown(ddlRepair, "REPAIR");
                    //ddlRepair.SelectedValue = "0";

                    objBindDDL.FillTaTaSkyReqDropDown(ddlPreScanProblem, "PRESCANPROBLEM");
                    ddlPreScanProblem.SelectedValue = "0";

                    // Bind the PreScan Engineer 
                    objBindDDL.FillTaTaSkyReqDropDown(ddlPreScanEngineer, "", "EMPLOYEE");
                    ddlPreScanEngineer.SelectedValue = "0";
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

        public void BindAssignmentGrid()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dtAssigmentDetail     = new DataTable();
                    dtAssigmentDetail               = objMainClass.GETTATASKYJOBASSIGNMENTFORPRESCANREPAIRSTAGE(txtEsnNo.Text, "PRESCANNINGSEARCH");
                    gvAssignment.DataSource         = dtAssigmentDetail;
                    gvAssignment.DataBind();
                    lgrecordcount.InnerText         = "Records : " + dtAssigmentDetail.Rows.Count.ToString();
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
                    txtEsnNo.Text           = string.Empty;
                    ddlModel.SelectedValue  = "0";
                    txtISPCode.Text = string.Empty;
                    txtISPFault.Text = string.Empty;
                    txtCIDReason.Text = string.Empty;
                    txtNDSNO.Text = string.Empty;
                    txtAssignmentNo.Text = string.Empty;
                    txtReceivedDate.Text = string.Empty;
                    txtPreScanDate.Text     = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    ddlPreScanProblem.SelectedValue = "0";
                    ddlEngineer.SelectedValue = "0";
                    ddlPreScanEngineer.SelectedValue = "0";
                    Session["saveall"] = "Save All";
                    ScriptManager.GetCurrent(this.Page).SetFocus(this.txtEsnNo);
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

        protected void txtEsnNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
               if(txtEsnNo.Text.Length == 12)
               {
                    var dtAssigmentDetail = objMainClass.GETTATASKYJOBASSIGNMENTFORPRESCANREPAIRSTAGE(txtEsnNo.Text, "PRESCANNINGEACH");
                    if(dtAssigmentDetail.Rows.Count > 0)
                    {
                        ddlModel.SelectedValue  = dtAssigmentDetail.Rows[0]["MODELSKEY"].ToString();
                        txtISPCode.Text         = dtAssigmentDetail.Rows[0]["ISPFAULTCODE"].ToString();
                        txtISPFault.Text        = dtAssigmentDetail.Rows[0]["TAGKEYVALUE"].ToString();
                        txtCIDReason.Text       = dtAssigmentDetail.Rows[0]["FAULTREPORTEDKEYVALUE"].ToString();
                        txtNDSNO.Text           = dtAssigmentDetail.Rows[0]["NDSNO"].ToString();     
                        txtAssignmentNo.Text    = dtAssigmentDetail.Rows[0]["ASSIGNMENTNO"].ToString();
                        txtReceivedDate.Text    = dtAssigmentDetail.Rows[0]["RECEIVEDATE"].ToString();
                        ddlPreScanProblem.SelectedValue = dtAssigmentDetail.Rows[0]["PRESCANNINGPROBLEMKEY"].ToString();
                        ddlPreScanEngineer.SelectedValue = dtAssigmentDetail.Rows[0]["PRESCANNINGENGINEERKEY"].ToString();
                        ddlEngineer.SelectedValue = dtAssigmentDetail.Rows[0]["ENGINEERKEY"].ToString();
                        lblESNExitalert.Text    = "ESN Notification Entry Not Available.";
                        lblESNExitalert.Visible = false;
                    }
                    else
                    {
                        ddlModel.SelectedValue          = "0";
                        txtISPCode.Text                 = string.Empty;
                        txtISPFault.Text                = string.Empty;
                        txtCIDReason.Text               = string.Empty;
                        txtNDSNO.Text                   = string.Empty;
                        txtAssignmentNo.Text            = string.Empty;
                        txtReceivedDate.Text            = string.Empty;
                        txtPreScanDate.Text             = string.Empty;
                        ddlPreScanProblem.SelectedValue = "0";
                        ddlEngineer.SelectedValue       = "0";
                        ddlPreScanEngineer.SelectedValue = "0";
                        lblESNExitalert.Text = "ESN Notification Entry Not Available.";
                        lblESNExitalert.Visible = true;
                        txtEsnNo.Focus();
                    }
               }
               else
                {
                    lblESNExitalert.Text = "Please Enter valid ESN No.";
                    lblESNExitalert.Visible = true;
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('.ddlPreScanProblem').select2();$('.ddlPreScanEngineer').select2();$('.ddlEngineer').select2();", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}