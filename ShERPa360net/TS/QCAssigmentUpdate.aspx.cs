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
    public partial class QCAssigmentUpdate : System.Web.UI.Page
    {
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        DataTable dtQcResult = new DataTable();
        DataTable dtQcFailReason = new DataTable();
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

        protected void lnkSearh_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    BindGridForQcUpdate();
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

        protected void imgSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvAssignment.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please search record and then Checked the record to Qc update.');", true);
                }
                else
                {
                    var QcGridValidationJsonValue = GetGridValidationandJsonValue();
                    if (QcGridValidationJsonValue.noofCheckedRecord == 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please Qc Result and also select the Fail reason if Qc Result is contains FAIL.');", true);
                    }
                    {
                        int result = objMainClass.SAVEQCBULKUPDATE(QcGridValidationJsonValue.QCBULKUPDATEJSON, Convert.ToString(Session["USERID"]));
                        if (result > 0)
                        {
                            BindGridForQcUpdate();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Records. :" + QcGridValidationJsonValue.noofCheckedRecord + " Qc update sucessfully." + "\");", true);
                        }
                    }

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
                BindGridForQcUpdate();
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
                    GridViewRow gRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    Label lblAssignmentNo = (Label)gRow.FindControl("lblAssignmentNo");
                    Label lblNDSNO = (Label)gRow.FindControl("lblNDSNO");
                    //objMainClass.SAVETATASKYJOBASSIGNMENT(lblAssignmentNo.Text, lblNDSNO.Text, string.Empty, string.Empty, txtFromDate.Text, "",
                    //                            Convert.ToInt64(ddlEngineer.SelectedValue), Convert.ToInt64(ddlModel.SelectedValue),
                    //                            Convert.ToInt64(ddlCondition.SelectedValue), Convert.ToInt64("0"),
                    //                            Convert.ToInt64(ddlRepair.SelectedValue), string.Empty, string.Empty, 0, string.Empty, string.Empty, string.Empty, "CANCEL", Convert.ToString(Session["USERID"]));
                    BindGridForQcUpdate();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Assignment No. :" + lblAssignmentNo.Text + " NDS No. :" + lblNDSNO.Text + " cancelled sucessfully." + "\");", true);
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

        protected void gvAssignment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlQcFailReason = (e.Row.FindControl("ddlQcFailReason") as DropDownList);
                    DropDownList ddlQcResult = (e.Row.FindControl("ddlQcResult") as DropDownList);
                    if (ddlQcFailReason.Items.Count > 0)
                    {
                        ddlQcFailReason.Items.Clear();
                    }

                    if (ddlQcResult.Items.Count > 0)
                    {
                        ddlQcResult.Items.Clear();
                    }

                    if (dtQcResult.Rows.Count > 0)
                    {
                        ddlQcResult.DataSource = dtQcResult;
                        ddlQcResult.DataTextField = "COMBONAME";
                        ddlQcResult.DataValueField = "COMBOID";
                        ddlQcResult.DataBind();
                        ddlQcResult.SelectedValue = "0";
                    }

                    if (dtQcFailReason.Rows.Count > 0)
                    {
                        ddlQcFailReason.DataSource = dtQcFailReason;
                        ddlQcFailReason.DataTextField = "COMBONAME";
                        ddlQcFailReason.DataValueField = "COMBOID";
                        ddlQcFailReason.DataBind();
                        ddlQcFailReason.SelectedValue = "0";
                    }

                    //objBindDDL.FillTaTaSkyReqDropDown(ddlQcFailReason, "PRESCANPROBLEM");
                    //ddlQcFailReason.SelectedValue = "0";

                    //objBindDDL.FillTaTaSkyReqDropDown(ddlQcResult, "QCRESULT", "REQUESTDROPDOWN");
                    //ddlQcResult.SelectedValue = "0";
                }
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

        public void BindGridForQcUpdate()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    LoadDropDownDataTable();
                    DataTable dt = new DataTable();
                    dt = objMainClass.GETCALLASSIGMENTFORQCUPDATE(txtFromDate.Text, txtToDate.Text, txtAssignmentNo.Text, txtNDSNO.Text, txtEsnNo.Text,
                                                                  Convert.ToInt64(ddlEngineer.SelectedValue), Convert.ToInt64(ddlModel.SelectedValue),
                                                                  Convert.ToInt64(ddlCondition.SelectedValue), Convert.ToInt64(ddlRepair.SelectedValue)
                                                                  );
                    gvAssignment.DataSource = dt;
                    gvAssignment.DataBind();
                    lgrecordcount.InnerText = "Records : " + dt.Rows.Count.ToString();
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
                    ddlEngineer.SelectedValue = "0";
                    txtEsnNo.Text = string.Empty;
                    ddlModel.SelectedValue = "0";
                    ddlCondition.SelectedValue = "0";
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

        public QcGridValidationandJsonValue GetGridValidationandJsonValue()
        {
            QcGridValidationandJsonValue objQcGridValidationandJsonValue = new QcGridValidationandJsonValue();
            objQcGridValidationandJsonValue.noofCheckedRecord = 0;
            List<QCBULKUPDATEJSON> lstQCBULKUPDATEJSON = new List<QCBULKUPDATEJSON>();
            int noofselectedcount = 0;
            try
            {
                for (int i = 0; i < gvAssignment.Rows.Count; i++)
                {
                    GridViewRow row = gvAssignment.Rows[i];
                    if (((DropDownList)row.FindControl("ddlQcResult")).SelectedValue != "0")
                    {
                        if (((DropDownList)row.FindControl("ddlQcResult")).SelectedItem.Text.ToUpper().Contains("FAIL") == true
                            )
                        {
                            if (((DropDownList)row.FindControl("ddlQcFailReason")).SelectedValue != "0")
                            {
                                QCBULKUPDATEJSON objQCBULKUPDATEJSON = new QCBULKUPDATEJSON();
                                objQCBULKUPDATEJSON.ASSIGNMENTNO = ((Label)row.FindControl("lblAssignmentNo")).Text;
                                objQCBULKUPDATEJSON.NDSNO = ((Label)row.FindControl("lblNDSNO")).Text;
                                objQCBULKUPDATEJSON.QCFAILREASONKEY = Convert.ToInt64(((DropDownList)row.FindControl("ddlQcFailReason")).SelectedValue);
                                objQCBULKUPDATEJSON.QCRESULTKEY = Convert.ToInt64(((DropDownList)row.FindControl("ddlQcResult")).SelectedValue);
                                lstQCBULKUPDATEJSON.Add(objQCBULKUPDATEJSON);
                                noofselectedcount = noofselectedcount + 1;
                            }
                        }
                        else
                        {
                            QCBULKUPDATEJSON objQCBULKUPDATEJSON = new QCBULKUPDATEJSON();
                            objQCBULKUPDATEJSON.ASSIGNMENTNO = ((Label)row.FindControl("lblAssignmentNo")).Text;
                            objQCBULKUPDATEJSON.NDSNO = ((Label)row.FindControl("lblNDSNO")).Text;
                            objQCBULKUPDATEJSON.QCFAILREASONKEY = Convert.ToInt64(((DropDownList)row.FindControl("ddlQcFailReason")).SelectedValue);
                            objQCBULKUPDATEJSON.QCRESULTKEY = Convert.ToInt64(((DropDownList)row.FindControl("ddlQcResult")).SelectedValue);
                            lstQCBULKUPDATEJSON.Add(objQCBULKUPDATEJSON);
                            noofselectedcount = noofselectedcount + 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            objQcGridValidationandJsonValue.noofCheckedRecord = noofselectedcount;
            objQcGridValidationandJsonValue.QCBULKUPDATEJSON = lstQCBULKUPDATEJSON.Count > 0 ? JsonConvert.SerializeObject(lstQCBULKUPDATEJSON) : "";
            return objQcGridValidationandJsonValue;
        }


        public void LoadDropDownDataTable()
        {
            try
            {
                dtQcResult = objMainClass.GetTaTaSkyReqDropDown("QCRESULT", "REQUESTDROPDOWN");
                dtQcFailReason = objMainClass.GetTaTaSkyReqDropDown("PRESCANPROBLEM", "REQUESTDROPDOWN");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
        #endregion
    }
}