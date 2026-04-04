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
    public partial class IREntry : System.Web.UI.Page
    {
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        DataTable dtPartDetail = new DataTable();

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
                        BindIRRepairGrid();
                        ResetFormControl();
                        SetupPartGrid();
                        ScriptManager.GetCurrent(this.Page).SetFocus(this.txtEsnNo);
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
                    BindIRRepairGrid();
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
                    string partjson = GetPartJson();
                    string irdate = "";
                    string berdate = "";
                    int irkey = 0;
                    int berkey = 0;
                    int repairstatus = 0;
                    if(Convert.ToInt32(ddlAction.SelectedValue) == Convert.ToInt32(REPAIRACTION.IR))
                    {
                        irdate = txtIRDate.Text;
                        irkey = Convert.ToInt32(ddlIRReason.SelectedValue);
                        repairstatus = Convert.ToInt32(REPAIRSTATUS.IR);
                    }
                    else if (Convert.ToInt32(ddlAction.SelectedValue) == Convert.ToInt32(REPAIRACTION.BER))
                    {
                        berdate = txtIRDate.Text;
                        berkey = Convert.ToInt32(ddlIRReason.SelectedValue);
                        repairstatus = Convert.ToInt32(REPAIRSTATUS.BER);
                    }


                    result = objMainClass.SAVEIRDETAIL(txtAssignmentNo.Text, irdate,
                        irkey,
                        Convert.ToInt32(ddlRepairTechName.SelectedValue),
                        Convert.ToInt32(ddlObjectPartDescription.SelectedValue),
                        Convert.ToInt32(ddlDamageDescription.SelectedValue),
                        Convert.ToInt32(ddlCauseDescription.SelectedValue),
                        Convert.ToInt32(ddlAction.SelectedValue),
                        repairstatus,
                        Convert.ToString(Session["USERID"]),
                        partjson, berdate, berkey
                        );
                    if (result > 0)
                    {
                        ResetFormControl();
                        ResetPartControl();
                        BindIRRepairGrid();
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
                BindIRRepairGrid();
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
                    //BindIRRepairGrid();
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
                        var dtAssigmentDetail = objMainClass.GETTATASKYJOBASSIGNMENTFORPRESCANREPAIRSTAGE(txtEsnNo.Text, "IREACHSEARCH");
                        if (dtAssigmentDetail.Rows.Count > 0)
                        {
                            ddlModel.SelectedValue = dtAssigmentDetail.Rows[0]["MODELSKEY"].ToString();
                            txtISPCode.Text = dtAssigmentDetail.Rows[0]["ISPFAULTCODE"].ToString();
                            txtISPFault.Text = dtAssigmentDetail.Rows[0]["ISPFAULTVALUE"].ToString();
                            txtCIDReason.Text = dtAssigmentDetail.Rows[0]["CIDREASON"].ToString();
                            txtNDSNO.Text = dtAssigmentDetail.Rows[0]["NDSNO"].ToString();
                            txtAssignmentNo.Text = dtAssigmentDetail.Rows[0]["ASSIGNMENTNO"].ToString();
                            txtReceivedDate.Text = dtAssigmentDetail.Rows[0]["RECEIVEDATE"].ToString();
                            txtPreScanDate.Text = dtAssigmentDetail.Rows[0]["PRESCANNINGDATE"].ToString();
                            ddlPreScanProblem.SelectedValue = dtAssigmentDetail.Rows[0]["PRESCANNINGPROBLEMKEY"].ToString();
                            ddlPreScanEngineer.SelectedValue = dtAssigmentDetail.Rows[0]["PRESCANNINGENGINEERKEY"].ToString() == "" ? "0" : dtAssigmentDetail.Rows[0]["PRESCANNINGENGINEERKEY"].ToString();
                            ddlEngineer.SelectedValue = dtAssigmentDetail.Rows[0]["ENGINEERKEY"].ToString() == "" ? "0" : dtAssigmentDetail.Rows[0]["ENGINEERKEY"].ToString();
                            txtRepairDate.Text = dtAssigmentDetail.Rows[0]["REPAIRDATE"].ToString();
                            ddlRepairTechName.SelectedValue = dtAssigmentDetail.Rows[0]["REPARIENGINEERKEY"].ToString();
                            ddlObjectPartDescription.SelectedValue = dtAssigmentDetail.Rows[0]["OBJECTPARTKEY"].ToString();
                            ddlDamageDescription.SelectedValue = dtAssigmentDetail.Rows[0]["FAULTOBSERVEDKEY"].ToString();
                            ddlCauseDescription.SelectedValue = dtAssigmentDetail.Rows[0]["FAULTREASONKEY"].ToString();
                            ddlAction.SelectedValue = dtAssigmentDetail.Rows[0]["ACTIONKEY"].ToString();
                            ddlRepariTask.SelectedValue = dtAssigmentDetail.Rows[0]["REPARITASKDESCRIPTIONKEY"].ToString();
                            txtRepairDate.Text = dtAssigmentDetail.Rows[0]["IRDATE"].ToString();
                            ddlIRReason.SelectedValue = dtAssigmentDetail.Rows[0]["REASONFORIRKEY"].ToString();


                            // Bind the Part Location 
                            objBindDDL.FillTaTaSkyPartLocation(ddlPartLocation, "PARTLOCATION", "PARTLOCATION", "Entry"
                                , Convert.ToInt32(dtAssigmentDetail.Rows[0]["MODELSKEY"].ToString()));
                            ddlPartLocation.SelectedValue = "0";

                            //Load IR Parts Detail
                            var dtPartDetail = objMainClass.GetPartsDetail(txtAssignmentNo.Text, "IR");
                            if (dtPartDetail.Rows.Count > 0)
                            {
                                ViewState["PartData"] = dtPartDetail;
                                gvPartDetail.DataSource = (DataTable)ViewState["PartData"];
                                gvPartDetail.DataBind();
                            }
                            else
                            {
                                ViewState["PartData"] = null;
                                gvPartDetail.DataSource = (DataTable)ViewState["PartData"];
                                gvPartDetail.DataBind();
                                SetupPartGrid();
                            }
                            //Load IR Parts Detail

                            lblESNExitalert.Text = "ESN Notification Entry Not Available.";
                            lblESNExitalert.Visible = false;
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
                            ddlIRReason.SelectedValue = "0";
                            txtIRDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");
                            ViewState["PartData"] = null;
                            gvPartDetail.DataSource = (DataTable)ViewState["PartData"];
                            gvPartDetail.DataBind();
                            SetupPartGrid();
                            txtEsnNo.Focus();
                            ddlPartLocation.Items.Clear();
                            ddlPartLocation.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                            ddlPartType.SelectedValue = "0";
                        }
                    }
                    else
                    {
                        lblESNExitalert.Text = "Please Enter valid ESN No.";
                        lblESNExitalert.Visible = true;

                        ddlPartLocation.Items.Clear();
                        ddlPartLocation.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                        ddlPartType.SelectedValue = "0";
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

        #region CHILDEVENT
        protected void btnAddPart_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = (DataTable)ViewState["PartData"];
                    if (gvPartDetail.Rows.Count > 0)
                    {
                        DataRow lastRow = dt.Rows[dt.Rows.Count - 1];
                        int id = Convert.ToInt32(lastRow["ID"]) + 1;
                        string location = "", partname = "";
                        string[] partsList = ddlPartLocation.SelectedItem.Text.Split('-');
                        int i = 1;
                        foreach (string eachdetail in partsList)
                        {
                            if (i == 1)
                            {
                                location = eachdetail;
                            }
                            else
                            {
                                partname = eachdetail;
                            }
                            i = i + 1;
                        }

                        dt.Rows.Add(id, Convert.ToInt32(ddlPartLocation.SelectedValue), location,
                                0, partname, txtAssignmentNo.Text, 0, Convert.ToInt32(ddlPartType.SelectedValue), ddlPartType.SelectedItem.Text);

                        ViewState["PartData"] = dt;
                        gvPartDetail.DataSource = (DataTable)ViewState["PartData"];
                        gvPartDetail.DataBind();
                        ResetPartControl("entry");
                    }
                    else
                    {
                        string location = "", partname = "";
                        string[] partsList = ddlPartLocation.SelectedItem.Text.Split('-');
                        int i = 1;
                        foreach (string eachdetail in partsList)
                        {
                            if (i == 1)
                            {
                                location = eachdetail;
                            }
                            else
                            {
                                partname = eachdetail;
                            }
                            i = i + 1;
                        }


                        dt.Rows.Add(Convert.ToInt32(1), Convert.ToInt32(ddlPartLocation.SelectedValue), location,
                                    0, partname, txtAssignmentNo.Text, Convert.ToInt32(0), Convert.ToInt32(ddlPartType.SelectedValue), ddlPartType.SelectedItem.Text);
                        ViewState["PartData"] = dt;
                        gvPartDetail.DataSource = (DataTable)ViewState["PartData"];
                        gvPartDetail.DataBind();
                        ResetPartControl("entry");
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

        protected void btnCancelPart_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    ResetPartControl("entry");
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

        protected void gvPartDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (e.CommandName == "eDelete")
                    {
                        int index = Convert.ToInt32(e.CommandArgument);
                        GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                        int partassigmentkey = Convert.ToInt32(((HiddenField)row.FindControl("hdpartyprimarykey")).Value);

                        if (partassigmentkey > 0)
                        {
                            int result = objMainClass.DELETEREPAIRPARTDETAIL(txtAssignmentNo.Text, partassigmentkey);
                            if (result > 0)
                            {
                                DataTable dt = (DataTable)ViewState["PartData"];
                                dt.Rows[row.RowIndex].Delete();
                                ViewState["PartData"] = dt;
                                gvPartDetail.DataSource = (DataTable)ViewState["PartData"];
                                gvPartDetail.DataBind();
                            }
                        }
                        else
                        {
                            DataTable dt = (DataTable)ViewState["PartData"];
                            dt.Rows[row.RowIndex].Delete();
                            ViewState["PartData"] = dt;
                            gvPartDetail.DataSource = (DataTable)ViewState["PartData"];
                            gvPartDetail.DataBind();
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

                    // Bind the IRReason Detail 
                    objBindDDL.FillTaTaSkyReqDropDown(ddlIRReason, "REASONFORIR");
                    ddlIRReason.SelectedValue = "0";

                    // Bind the Part Type 
                    objBindDDL.FillTaTaSkyReqDropDown(ddlPartType, "PARTTYPE");
                    ddlPartType.SelectedValue = "0";
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

        public void BindIRRepairGrid()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dtIRDetail = new DataTable();
                    dtIRDetail = objMainClass.GETTATASKYJOBASSIGNMENTFORPRESCANREPAIRSTAGE(txtEsnNo.Text, "IRSEARCH");
                    gvAssignment.DataSource = dtIRDetail;
                    gvAssignment.DataBind();
                    lgrecordcount.InnerText = "Records : " + dtIRDetail.Rows.Count.ToString();
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
                    txtRepairDate.Text = string.Empty;
                    txtIRDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");
                    ddlPreScanProblem.SelectedValue = "0";
                    ddlEngineer.SelectedValue = "0";
                    ddlPreScanEngineer.SelectedValue = "0";
                    ddlRepairTechName.SelectedValue = "0";
                    ddlObjectPartDescription.SelectedValue = "0";
                    ddlDamageDescription.SelectedValue = "0";
                    ddlCauseDescription.SelectedValue = "0";
                    ddlAction.SelectedValue = "0";
                    ddlRepariTask.SelectedValue = "0";
                    ddlIRReason.SelectedValue = "0";
                    ddlPartLocation.Items.Clear();
                    ddlPartLocation.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                    ddlPartType.SelectedValue = "0";
                    ResetPartControl();
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

        #region CHILD
        public void SetupPartGrid()
        {
            try
            {
                DataColumn dtColumn;

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ID";
                //dtColumn.DataType = typeof(Int32);
                dtPartDetail.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "PARTLOCATIONKEY";
                //dtColumn.DataType = typeof(Int32);
                dtPartDetail.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "PARTLOCATIONNAME";
                dtColumn.DataType = typeof(string);
                dtPartDetail.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "PARTNAMEKEY";
                //dtColumn.DataType = typeof(Int32);
                dtPartDetail.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "PARTNAME";
                dtColumn.DataType = typeof(string);
                dtPartDetail.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ASSIGNMENTNO";
                dtColumn.DataType = typeof(string);
                dtPartDetail.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "PARTASSIGMENTKEY";
                //dtColumn.DataType = typeof(Int32);
                dtPartDetail.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "PARTTYPEKEY";
                dtPartDetail.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "PARTTYPEKEYVALUE";
                dtPartDetail.Columns.Add(dtColumn);

                ViewState["PartData"] = dtPartDetail;
                gvPartDetail.DataSource = (DataTable)ViewState["PartData"];
                gvPartDetail.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public void ResetPartControl(string req = "main")
        {
            try
            {
                if (req == "main")
                {
                    ddlPartLocation.SelectedValue = "0";
                    ddlPartType.SelectedValue = "0";
                    ViewState["PartData"] = null;
                    gvPartDetail.DataSource = (DataTable)ViewState["PartData"];
                    gvPartDetail.DataBind();
                }
                else
                {
                    ddlPartLocation.SelectedValue = "0";
                    ddlPartType.SelectedValue = "0";
                    ddlPartLocation.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public string GetPartJson()
        {
            string partjson = "";
            try
            {
                List<PartJson> objlstPartJson = new List<PartJson>();
                for (int i = 0; i < gvPartDetail.Rows.Count; i++)
                {
                    GridViewRow row = gvPartDetail.Rows[i];
                    PartJson objPartJson = new PartJson();
                    if (Convert.ToInt32(((HiddenField)row.FindControl("hdpartyprimarykey")).Value) == 0)
                    {
                        objPartJson.PARTASSIGMENTKEY = Convert.ToInt32(((HiddenField)row.FindControl("hdpartyprimarykey")).Value);
                        objPartJson.PARTNAMEKEY = 0;
                        objPartJson.PARTNAME = ((Label)row.FindControl("lblPartName")).Text;
                        objPartJson.PARTLOCATIONKEY = Convert.ToInt32(((HiddenField)row.FindControl("hdPartLocationKey")).Value);
                        objPartJson.PARTLOCATIONNAME = ((Label)row.FindControl("lblPartLocation")).Text;
                        objPartJson.PARTTYPEKEY = Convert.ToInt32(((HiddenField)row.FindControl("hdparttypekey")).Value);
                        objPartJson.PARTTYPEKEYVALUE = ((Label)row.FindControl("lblPartType")).Text;
                        objlstPartJson.Add(objPartJson);
                    }
                }
                partjson = JsonConvert.SerializeObject(objlstPartJson);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return partjson;
        }
        #endregion
        #endregion
    }
}