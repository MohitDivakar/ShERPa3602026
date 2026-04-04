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
    public partial class Assignment : System.Web.UI.Page
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
                string ASSIGNMENTNo = "";
                if (Session["USERID"] != null)
                {
                    if (hdAssignmentId.Value == "0")
                    {
                        DataTable dt = new DataTable();
                        dt = objMainClass.GETTATASKYJOBASSIGNMENT(string.Empty, txtNDSNO.Text, string.Empty, txtEsnNo.Text, Convert.ToInt64(ddlEngineer.SelectedValue),
                                                                                       Convert.ToInt64(ddlModel.SelectedValue), Convert.ToInt64(ddlCondition.SelectedValue)
                                                                                       , 0, Convert.ToInt64(ddlRepair.SelectedValue), "NDSNOAVAILABLE", Convert.ToInt64(Session["USERID"]));
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["AVAILABLE"].ToString() == "NO")
                            {
                                ASSIGNMENTNo = objMainClass.SAVETATASKYJOBASSIGNMENT(txtAssignmentNo.Text, txtNDSNO.Text, txtEsnNo.Text, string.Empty, txtDate.Text, txtTime.Text,
                                                                  Convert.ToInt64(ddlEngineer.SelectedValue), Convert.ToInt64(ddlModel.SelectedValue),
                                                                  Convert.ToInt64(ddlCondition.SelectedValue), 0,
                                                                  Convert.ToInt64(ddlRepair.SelectedValue), txtReceivedDate.Text, txtPreScanDate.Text,
                                                                  Convert.ToInt64(ddlPreScanProblem.SelectedValue), txtISPCode.Text, "",
                                                                  "", "ADD", Convert.ToInt32(ddlTAG.SelectedValue), Convert.ToInt32(ddLFaultReported.SelectedValue),
                                                                  Convert.ToString(Session["USERID"]), Session["PLANTCD"].ToString()
                                                                  );
                                if (ASSIGNMENTNo.Length > 0)
                                {
                                    ResetFormControl();
                                    BindAssignmentGrid();
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" Notification is not add " + "successfully due to some issue." + "\");", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" NDSNO.: " + txtNDSNO.Text + " call is already assigment to " + dt.Rows[0]["ENGINEERNAME"].ToString() + " Engineer! " + "\");", true);
                            }
                        }
                    }
                    else
                    {
                        int result = 0;
                        result = objMainClass.UPDATENOTIFICATIONENTRY(txtAssignmentNo.Text,
                            Convert.ToInt32(ddlModel.SelectedValue), txtISPCode.Text,
                            Convert.ToInt32(ddlTAG.SelectedValue), Convert.ToInt32(ddLFaultReported.SelectedValue),
                            txtReceivedDate.Text, Convert.ToString(Session["USERID"]));
                        if (result > 0)
                        {
                            ResetFormControl();
                            BindAssignmentGrid();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" Notification is not update " + "successfully due to some issue." + "\");", true);
                        }
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
                    GridViewRow gRow      = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    Label lblAssignmentNo = (Label)gRow.FindControl("lblAssignmentNo");
                    Label lblNDSNO        = (Label)gRow.FindControl("lblNDSNO");
                    //objMainClass.SAVETATASKYJOBASSIGNMENT(lblAssignmentNo.Text, lblNDSNO.Text, string.Empty, string.Empty, txtDate.Text, txtTime.Text,
                    //                            Convert.ToInt64(ddlEngineer.SelectedValue), Convert.ToInt64(ddlModel.SelectedValue),
                    //                            Convert.ToInt64(ddlCondition.SelectedValue), 0,
                    //                            Convert.ToInt64(ddlRepair.SelectedValue), txtReceivedDate.Text, txtPreScanDate.Text,
                    //                            Convert.ToInt64(ddlPreScanProblem.SelectedValue), string.Empty, string.Empty, "",
                    //                            "CANCEL", Convert.ToString(Session["USERID"]));
                    BindAssignmentGrid();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Assignment No. :" + lblAssignmentNo.Text + " NDS No. :" + lblNDSNO.Text + " cancelled sucessfully." + "\");", true);
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

                    objBindDDL.FillTaTaSkyReqDropDown(ddlCondition, "CONDITION");
                    ddlCondition.SelectedValue = "0";

                    objBindDDL.FillTaTaSkyReqDropDown(ddlRepair, "REPAIR");
                    ddlRepair.SelectedValue = "0";

                    objBindDDL.FillTaTaSkyReqDropDown(ddlPreScanProblem, "PRESCANPROBLEM");
                    ddlPreScanProblem.SelectedValue = "0";

                    objBindDDL.FillTaTaSkyReqDropDown(ddlTAG, "TAG");
                    ddlTAG.SelectedValue = "0";

                    objBindDDL.FillTaTaSkyReqDropDown(ddLFaultReported, "FAULTREPORTED");
                    ddLFaultReported.SelectedValue = "0";
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
                    DataTable dt = new DataTable();
                    dt = objMainClass.GETTATASKYJOBASSIGNMENT(string.Empty, txtNDSNO.Text, string.Empty, txtEsnNo.Text, Convert.ToInt64(ddlEngineer.SelectedValue),
                                                                                   Convert.ToInt64(ddlModel.SelectedValue), 0
                                                                                   , 0, 0, "SEARCH", Convert.ToInt64(Session["USERID"]));
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

        public void ResetFormControl(string type = "regular")
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    txtDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    txtTime.Text = DateTime.Now.ToString("h:mm tt", CultureInfo.InvariantCulture);
                    ddlEngineer.SelectedValue = "0";
                    ddlModel.SelectedValue = "0";
                    ddlCondition.SelectedValue = "0";
                    ddlRepair.SelectedValue = "0";
                    hdAssignmentId.Value = "0";
                    txtNDSNO.Text = string.Empty;
                    if(type == "regular")
                    {
                        txtEsnNo.Text = string.Empty;
                    }
                    txtAssignmentNo.Text = objMainClass.strConvertZeroPadding(objMainClass.MAXASSIGNMENTNO(Session["PLANTCD"].ToString()));
                    txtReceivedDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    txtPreScanDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    ddlPreScanProblem.SelectedValue = "0";
                    txtISPCode.Text = string.Empty;
                    ddlTAG.SelectedValue = "0";
                    ddLFaultReported.SelectedValue = "0";
                    hdAssignmentId.Value = "0";
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
                    var dtAssigmentDetail = objMainClass.GETTATASKYJOBASSIGNMENTFORPRESCANREPAIRSTAGE(txtEsnNo.Text, "NOTIFICATIONEACHSEARCH");
                    if (dtAssigmentDetail.Rows.Count > 0)
                    {
                        txtDate.Text = dtAssigmentDetail.Rows[0]["ASSIGNDATE"].ToString();
                        txtTime.Text = dtAssigmentDetail.Rows[0]["ASSIGNTIME"].ToString();
                        ddlModel.SelectedValue = dtAssigmentDetail.Rows[0]["MODELSKEY"].ToString();
                        txtISPCode.Text = dtAssigmentDetail.Rows[0]["ISPFAULTCODE"].ToString();
                        ddlTAG.SelectedValue = dtAssigmentDetail.Rows[0]["TAGKEY"].ToString();
                        ddLFaultReported.SelectedValue = dtAssigmentDetail.Rows[0]["FAULTREPORTEDKEY"].ToString();
                        txtNDSNO.Text = dtAssigmentDetail.Rows[0]["NDSNO"].ToString();
                        txtAssignmentNo.Text = dtAssigmentDetail.Rows[0]["ASSIGNMENTNO"].ToString();
                        txtReceivedDate.Text = dtAssigmentDetail.Rows[0]["RECEIVEDATE"].ToString();
                        lblESNExitalert.Text = "ESN already moved on Prescanning Entry.";
                        lblESNExitalert.Visible = false;
                        hdAssignmentId.Value = "1";
                    }
                    else
                    {
                        ResetFormControl("NotRegular");
                        string esno = (txtEsnNo.Text.Length > 0 ? txtEsnNo.Text.Substring(0,2) : "");
                        var dtesnDetail = objMainClass.GETESNTOMODELDETAIL(esno);
                        if (dtesnDetail.Rows.Count > 0)
                        {
                            ddlModel.SelectedValue = dtesnDetail.Rows[0]["MODELID"].ToString();
                        }
                        else
                        {
                            ddlModel.SelectedValue = "0";
                        }
                    }
               }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}