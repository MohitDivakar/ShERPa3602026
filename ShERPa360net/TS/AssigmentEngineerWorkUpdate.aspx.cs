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
    public partial class AssigmentEngineerWorkUpdate : System.Web.UI.Page
    {
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        DataTable dtObjectPart = new DataTable();
        DataTable dtFaultObserved = new DataTable();
        DataTable dtFaultReason = new DataTable();
        DataTable dtAction = new DataTable();
        DataTable dtReasonforIR = new DataTable();

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

                    BindGridForEngineerWorkUpdate();
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
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please search record to update the Engineer Work Update.');", true);
                }
                else
                {
                    var EngineerWorkUpdateJsonValue = GetGridValidationandJsonValue();
                    int result = objMainClass.SAVEQCBULKUPDATE(EngineerWorkUpdateJsonValue.ENGINEERWORKUPDATEJSON, Convert.ToString(Session["USERID"]), "ENGINEERWORKBULKUPDATE");
                    if (result > 0)
                    {
                        BindGridForEngineerWorkUpdate();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Records. :" + EngineerWorkUpdateJsonValue.noofCheckedRecord + " Engineer work update sucessfully." + "\");", true);
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
                BindGridForEngineerWorkUpdate();
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
                    //                            0, Convert.ToInt64("0"),
                    //                            0, string.Empty, string.Empty, 0, string.Empty, string.Empty, string.Empty, "CANCEL", Convert.ToString(Session["USERID"]));
                    BindGridForEngineerWorkUpdate();
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
                    DropDownList ddlObjectPart = (e.Row.FindControl("ddlObjectPart") as DropDownList);
                    DropDownList ddlFaultObjserved = (e.Row.FindControl("ddlFaultObjserved") as DropDownList);
                    DropDownList ddlFaultReason = (e.Row.FindControl("ddlFaultReason") as DropDownList);
                    DropDownList ddlAction = (e.Row.FindControl("ddlAction") as DropDownList);
                    DropDownList ddlReasonForIR = (e.Row.FindControl("ddlReasonForIR") as DropDownList);

                    HiddenField hdObjectPartKey = (e.Row.FindControl("hdObjectPartKey") as HiddenField);
                    HiddenField hdFaultObjservedKey = (e.Row.FindControl("hdFaultObjservedKey") as HiddenField);
                    HiddenField hdFaultReasonKey = (e.Row.FindControl("hdFaultReasonKey") as HiddenField);
                    HiddenField hdActionKey = (e.Row.FindControl("hdActionKey") as HiddenField);
                    HiddenField hdReasonforIRKey = (e.Row.FindControl("hdReasonforIRKey") as HiddenField);

                    if (ddlObjectPart.Items.Count > 0)
                    {
                        ddlObjectPart.Items.Clear();
                    }

                    if (ddlFaultObjserved.Items.Count > 0)
                    {
                        ddlFaultObjserved.Items.Clear();
                    }

                    if (ddlFaultReason.Items.Count > 0)
                    {
                        ddlFaultReason.Items.Clear();
                    }

                    if (ddlAction.Items.Count > 0)
                    {
                        ddlAction.Items.Clear();
                    }

                    if (ddlReasonForIR.Items.Count > 0)
                    {
                        ddlReasonForIR.Items.Clear();
                    }

                    if (dtObjectPart.Rows.Count > 0)
                    {
                        ddlObjectPart.DataSource = dtObjectPart;
                        ddlObjectPart.DataTextField = "COMBONAME";
                        ddlObjectPart.DataValueField = "COMBOID";
                        ddlObjectPart.DataBind();
                        ddlObjectPart.SelectedValue = hdObjectPartKey.Value == "0" ? "0" : hdObjectPartKey.Value;
                    }

                    if (dtFaultObserved.Rows.Count > 0)
                    {
                        ddlFaultObjserved.DataSource = dtFaultObserved;
                        ddlFaultObjserved.DataTextField = "COMBONAME";
                        ddlFaultObjserved.DataValueField = "COMBOID";
                        ddlFaultObjserved.DataBind();
                        ddlFaultObjserved.SelectedValue = hdFaultObjservedKey.Value == "0" ? "0" : hdFaultObjservedKey.Value;
                    }

                    if (dtFaultReason.Rows.Count > 0)
                    {
                        ddlFaultReason.DataSource = dtFaultReason;
                        ddlFaultReason.DataTextField = "COMBONAME";
                        ddlFaultReason.DataValueField = "COMBOID";
                        ddlFaultReason.DataBind();
                        ddlFaultReason.SelectedValue = hdFaultReasonKey.Value == "0" ? "0" : hdFaultReasonKey.Value;
                    }

                    if (dtAction.Rows.Count > 0)
                    {
                        ddlAction.DataSource = dtAction;
                        ddlAction.DataTextField = "COMBONAME";
                        ddlAction.DataValueField = "COMBOID";
                        ddlAction.DataBind();
                        ddlAction.SelectedValue = hdActionKey.Value == "0" ? "0" : hdActionKey.Value;
                    }

                    if (dtReasonforIR.Rows.Count > 0)
                    {
                        ddlReasonForIR.DataSource = dtReasonforIR;
                        ddlReasonForIR.DataTextField = "COMBONAME";
                        ddlReasonForIR.DataValueField = "COMBOID";
                        ddlReasonForIR.DataBind();
                        ddlReasonForIR.SelectedValue = hdReasonforIRKey.Value == "0" ? "0" : hdReasonforIRKey.Value;
                    }

                    //objBindDDL.FillTaTaSkyReqDropDown(ddlObjectPart, "OBJECTPART", "REQUESTDROPDOWN");
                    //ddlObjectPart.SelectedValue = hdObjectPartKey.Value == "0" ?  "0" : hdObjectPartKey.Value;

                    //objBindDDL.FillTaTaSkyReqDropDown(ddlFaultObjserved, "FAULTOBSERVED", "REQUESTDROPDOWN");
                    //ddlFaultObjserved.SelectedValue = hdFaultObjservedKey.Value == "0" ? "0" : hdFaultObjservedKey.Value;

                    //objBindDDL.FillTaTaSkyReqDropDown(ddlFaultReason, "FAULTREASON", "REQUESTDROPDOWN");
                    //ddlFaultReason.SelectedValue = hdFaultReasonKey.Value == "0" ? "0" : hdFaultReasonKey.Value;

                    //objBindDDL.FillTaTaSkyReqDropDown(ddlAction, "ACTION", "REQUESTDROPDOWN");
                    //ddlAction.SelectedValue = hdActionKey.Value == "0" ? "0" : hdActionKey.Value;

                    //objBindDDL.FillTaTaSkyReqDropDown(ddlReasonForIR, "REASONFORIR", "REQUESTDROPDOWN");
                    //ddlReasonForIR.SelectedValue = hdReasonforIRKey.Value == "0" ? "0" : hdReasonforIRKey.Value;
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

        public void BindGridForEngineerWorkUpdate()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    LoadDropDownDataTable();
                    DataTable dt = new DataTable();
                    dt = objMainClass.GETCALLASSIGMENTFORQCUPDATE(txtFromDate.Text, txtToDate.Text, txtAssignmentNo.Text, txtNDSNO.Text, txtEsnNo.Text,
                                                                  Convert.ToInt64(ddlEngineer.SelectedValue), Convert.ToInt64(ddlModel.SelectedValue),
                                                                  0, 0
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

        public EngineerWorkUpdateJsonValue GetGridValidationandJsonValue()
        {
            EngineerWorkUpdateJsonValue objEngineerWorkUpdateJsonValue = new EngineerWorkUpdateJsonValue();
            objEngineerWorkUpdateJsonValue.noofCheckedRecord = 0;
            List<EngineerWorkUpdateJson> lstENGINEERWORKUPDATEJSON = new List<EngineerWorkUpdateJson>();
            int noofselectedcount = 0;
            try
            {
                for (int i = 0; i < gvAssignment.Rows.Count; i++)
                {
                    GridViewRow row = gvAssignment.Rows[i];
                    EngineerWorkUpdateJson objEngineerWorkUpdateJson = new EngineerWorkUpdateJson();
                    objEngineerWorkUpdateJson.ASSIGNMENTNO = ((Label)row.FindControl("lblAssignmentNo")).Text;
                    objEngineerWorkUpdateJson.NDSNO = ((Label)row.FindControl("lblNDSNO")).Text;
                    objEngineerWorkUpdateJson.OBJECTPARTKEY = Convert.ToInt64(((DropDownList)row.FindControl("ddlObjectPart")).SelectedValue);
                    objEngineerWorkUpdateJson.FAULTOBSERVEDKEY = Convert.ToInt64(((DropDownList)row.FindControl("ddlFaultObjserved")).SelectedValue);
                    objEngineerWorkUpdateJson.FAULTREASONKEY = Convert.ToInt64(((DropDownList)row.FindControl("ddlFaultReason")).SelectedValue);
                    objEngineerWorkUpdateJson.ACTIONKEY = Convert.ToInt64(((DropDownList)row.FindControl("ddlAction")).SelectedValue);
                    objEngineerWorkUpdateJson.REASONFORIRKEY = Convert.ToInt64(((DropDownList)row.FindControl("ddlReasonForIR")).SelectedValue);
                    objEngineerWorkUpdateJson.PARTNAME = ((TextBox)row.FindControl("txtPartName")).Text;
                    objEngineerWorkUpdateJson.PARTLOCATION = ((TextBox)row.FindControl("txtPartLocation")).Text;
                    lstENGINEERWORKUPDATEJSON.Add(objEngineerWorkUpdateJson);
                    noofselectedcount = noofselectedcount + 1;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            objEngineerWorkUpdateJsonValue.noofCheckedRecord = noofselectedcount;
            objEngineerWorkUpdateJsonValue.ENGINEERWORKUPDATEJSON = lstENGINEERWORKUPDATEJSON.Count > 0 ? JsonConvert.SerializeObject(lstENGINEERWORKUPDATEJSON) : "";
            return objEngineerWorkUpdateJsonValue;
        }

        public void LoadDropDownDataTable()
        {
            try
            {
                dtObjectPart = objMainClass.GetTaTaSkyReqDropDown("OBJECTPART", "REQUESTDROPDOWN");
                dtFaultObserved = objMainClass.GetTaTaSkyReqDropDown("FAULTOBSERVED", "REQUESTDROPDOWN");
                dtFaultReason = objMainClass.GetTaTaSkyReqDropDown("FAULTREASON", "REQUESTDROPDOWN");
                dtAction = objMainClass.GetTaTaSkyReqDropDown("ACTION", "REQUESTDROPDOWN");
                dtReasonforIR = objMainClass.GetTaTaSkyReqDropDown("REASONFORIR", "REQUESTDROPDOWN");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
        #endregion
    }
}