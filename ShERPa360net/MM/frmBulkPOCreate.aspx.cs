using Newtonsoft.Json;
using RestSharp;
using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.MM
{
    public partial class frmBulkPOCreate : System.Web.UI.Page
    {

        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        WAClass objWAClass = new WAClass();

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
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();

                        objBindDDL.FillPlant(ddlPlant);
                        objBindDDL.FillSegment(ddlSegment);
                        objBindDDL.FillStatus(ddlStage, 2);
                        ddlSegment.SelectedValue = "1043";
                        ddlSegment_SelectedIndexChanged(1, e);
                        string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                        ddlPlant.SelectedValue = "1019"; //plantArray[0];
                        GetData();
                        //grvData.Columns[14].ItemStyle.Width = 100;
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

        public void GetData()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable segmentDt = new DataTable();
                    segmentDt = objMainClass.SelectUserSegment(Convert.ToString(Session["USERID"]));

                    string segment = Convert.ToString(segmentDt.Rows[0]["SEGMENT"]);

                    DataTable dtData = new DataTable();
                    dtData = objMainClass.GetJobsheetForPO(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, ddlSegment.SelectedIndex > 0 ? ddlSegment.SelectedValue : segment, ddlPlant.SelectedIndex > 0 ? ddlPlant.SelectedValue : Convert.ToString(Session["PLANTCD"]),
                        ddlStage.SelectedIndex > 0 ? Convert.ToInt32(ddlStage.SelectedValue) : 0, (int)STATUS.Canceled, txtJobID.Text, "GETJOBID");
                    if (dtData.Rows.Count > 0)
                    {
                        grvData.DataSource = dtData;
                        grvData.DataBind();
                        grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    else
                    {
                        grvData.DataSource = string.Empty;
                        grvData.DataBind();
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

        protected void ddlSegment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable segmentDt = new DataTable();
                    segmentDt = objMainClass.SelectUserSegment(Convert.ToString(Session["USERID"]));

                    string segment = Convert.ToString(segmentDt.Rows[0]["SEGMENT"]);
                    string[] segmentArray = segment.Split(',');
                    if (segment.Contains(ddlSegment.SelectedValue) == true)
                    {
                        DataTable dtStage = new DataTable();
                        dtStage = objMainClass.GetSegmentStageData(0, ddlSegment.SelectedValue, "GETSTAGEREQ");
                        ddlStage.DataSource = dtStage;
                        ddlStage.DataTextField = "STAGEDESC";
                        ddlStage.DataValueField = "STAGEID";
                        ddlStage.DataBind();
                        ddlStage.SelectedValue = "47";
                    }
                    else
                    {
                        ddlSegment.SelectedValue = segmentArray[0];
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(You do not have plant rights!);", true);
                        ddlSegment.SelectedValue = segmentArray[0];
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

        protected void ddlPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    string PLantRights = string.Empty;
                    string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                    //for (int i = 0; i < plantArray.Count(); i++)
                    //{
                    //    if (plantArray[i].Trim() == ddlPlant.SelectedValue)
                    //    {
                    //        PLantRights = ddlPlant.SelectedValue;
                    //    }
                    //}

                    if (Convert.ToString(Session["PLANTCD"]).Contains(ddlPlant.SelectedValue) == true)
                    {

                    }
                    else
                    {
                        ddlPlant.SelectedValue = plantArray[0];
                        ddlPlant.Focus();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have plant rights.');$('#txtJobId').focus();", true);
                        ddlPlant.SelectedValue = plantArray[0];
                        ddlPlant.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lnkSerch_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GetData();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow hrow = grvData.HeaderRow;
                    CheckBox chkSelectAll = (CheckBox)hrow.FindControl("chkSelectAll");
                    if (chkSelectAll.Checked == true)
                    {
                        for (int i = 0; i < grvData.Rows.Count; i++)
                        {
                            GridViewRow row = grvData.Rows[i];
                            CheckBox chkSelect = ((CheckBox)row.FindControl("chkSelect"));
                            chkSelect.Checked = true;
                        }

                    }
                    else
                    {
                        for (int i = 0; i < grvData.Rows.Count; i++)
                        {
                            GridViewRow row = grvData.Rows[i];
                            CheckBox chkSelect = ((CheckBox)row.FindControl("chkSelect"));
                            chkSelect.Checked = false;
                        }
                    }

                    if (grvData.Rows.Count > 0)
                    {
                        grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
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

        public int GetCount()
        {
            int iReturn = 0;
            try
            {
                if (Session["USERID"] != null)
                {
                    for (int i = 0; i < grvData.Rows.Count; i++)
                    {
                        GridViewRow row = grvData.Rows[i];
                        CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                        if (chkSelect.Checked == true)
                        {
                            iReturn = iReturn + 1;
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
            return iReturn;
        }

        protected void imgSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (GetCount() > 0)
                    {

                        objBindDDL.FillPayTerm(ddlPaymentTerms);
                        ddlPaymentTerms.SelectedIndex = 1;
                        txtPaymentTermsDesc.Text = Convert.ToString(ddlPaymentTerms.SelectedItem.Text.Split('-')[1].Trim());
                        txtPaymentTermsDesc.Enabled = false;
                        objBindDDL.FillLists(ddlPurType, "PUR");
                        ddlPurType.SelectedValue = ddlPurType.Items.FindByText("FRANCHISE").Value;
                        objBindDDL.FillPurChrgType(ddlCharges, objMainClass.intCmpId);
                        objBindDDL.FillCondition(ddlChargeTax);
                        objBindDDL.FillDepartment(ddlDepartment);
                        ddlDepartment.SelectedValue = "1";

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Job ID not selected to create PO. Select atleast one Job ID.!');", true);
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

        protected void txtVendorCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (txtVendorCode.Text.Length >= 5)
                    {
                        lblVendorError.Text = string.Empty;
                        lblVendorError.Visible = false;
                        DataTable dt = objMainClass.GetVendorDetails(objMainClass.intCmpId, txtVendorCode.Text, "");
                        if (dt.Rows.Count > 0)
                        {
                            lblVendorError.Text = string.Empty;
                            lblVendorError.Visible = false;
                            txtVendorName.Text = Convert.ToString(dt.Rows[0]["NAME1"]);
                            txtVendorCode.Text = Convert.ToString(dt.Rows[0]["VENDCODE"]);
                        }
                        else
                        {
                            lblVendorError.Text = "Invalid Vendor Code. Please Enter Correct Vendor Code.";
                            lblVendorError.Visible = true;
                            txtVendorCode.Focus();
                            ScriptManager.GetCurrent(this.Page).SetFocus(this.txtVendorCode);
                        }
                    }
                    else
                    {
                        lblVendorError.Text = "Minimum 5 digit req.";
                        lblVendorError.Visible = true;
                        txtVendorCode.Focus();
                        ScriptManager.GetCurrent(this.Page).SetFocus(this.txtVendorCode);

                    }
                    txtTranCode.Focus();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
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

        protected void txtTranCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    if (txtTranCode.Text.Length >= 5)
                    {
                        lblTransporterError.Text = string.Empty;
                        lblTransporterError.Visible = false;
                        DataTable dt = objMainClass.GetVendorDetails(objMainClass.intCmpId, txtTranCode.Text, "");
                        if (dt.Rows.Count > 0)
                        {
                            lblTransporterError.Text = string.Empty;
                            lblTransporterError.Visible = false;
                            txtTranName.Text = Convert.ToString(dt.Rows[0]["NAME1"]);
                            txtTranCode.Text = Convert.ToString(dt.Rows[0]["VENDCODE"]);
                        }
                        else
                        {
                            lblTransporterError.Text = "Invalid Transporter Code. Please Enter Correct Transporter Code.";
                            lblTransporterError.Visible = true;
                            txtTranCode.Focus();
                            ScriptManager.GetCurrent(this.Page).SetFocus(this.txtTranCode);
                        }
                    }
                    else
                    {
                        lblTransporterError.Text = "Minimum 5 digit req.";
                        lblTransporterError.Visible = true;
                        txtTranCode.Focus();
                    }
                    ddlPaymentTerms.Focus();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
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

        protected void ddlPaymentTerms_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    try
                    {
                        if (ddlPaymentTerms.SelectedValue == "OTHR")
                        {
                            txtPaymentTermsDesc.Text = string.Empty;
                            txtPaymentTermsDesc.Enabled = true;
                            txtPaymentTermsDesc.Focus();
                        }
                        else
                        {
                            txtPaymentTermsDesc.Text = Convert.ToString(ddlPaymentTerms.SelectedItem.Text.Split('-')[1].Trim()); ;
                            txtPaymentTermsDesc.Enabled = false;
                        }
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
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

        protected void btnCreateDoc_Click(object sender, EventArgs e)
        {
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
            DataTable dtPOError = new DataTable("Data");
            dtPOError.Columns.Add("IMEINO");
            dtPOError.Columns.Add("JOBID");
            dtPOError.Columns.Add("ERROR");
            int ierror = 0;
            int isucess = 0;
            int iPRSrno = 1;
            int iPOSrno = 0;
            int iPOCondSrno = 0;
            int iPOChgSrno = 0;

            decimal totalbasicrate = 0;
            decimal totaltaxrate = 0;
            decimal totaltaxamt = 0;
            decimal totalwithtaxamt = 0;

            try
            {
                if (Session["USERID"] != null)
                {
                    List<PRDetails> objPRDetails = new List<PRDetails>();
                    List<POCharges> objPOCharges = new List<POCharges>();
                    List<PODetails> objPODetails = new List<PODetails>();
                    List<POTaxation> objPOTaxation = new List<POTaxation>();

                    foreach (GridViewRow row in grvData.Rows)
                    {
                        CheckBox chkSelect = row.FindControl("chkSelect") as CheckBox;
                        DropDownList ddlGVTax = row.FindControl("ddlGVTax") as DropDownList;
                        Label lblJOBID = row.FindControl("lblJOBID") as Label;
                        Label lblJOBSTATUS = row.FindControl("lblJOBSTATUS") as Label;
                        Label lblJOBSTATDESC = row.FindControl("lblJOBSTATDESC") as Label;
                        Label lblSTAGEID = row.FindControl("lblSTAGEID") as Label;
                        Label lblSEGMENT = row.FindControl("lblSEGMENT") as Label;
                        Label lblDISTCHNL = row.FindControl("lblDISTCHNL") as Label;
                        Label lblPRODMAKE = row.FindControl("lblPRODMAKE") as Label;
                        Label lblPRODMODEL = row.FindControl("lblPRODMODEL") as Label;
                        Label lblIMEINO = row.FindControl("lblIMEINO") as Label;
                        Label lblIMEINO2 = row.FindControl("lblIMEINO2") as Label;
                        Label lblPLANTCD = row.FindControl("lblPLANTCD") as Label;
                        Label lblLOCCD = row.FindControl("lblLOCCD") as Label;
                        Label lblITEMCODE = row.FindControl("lblITEMCODE") as Label;
                        Label lblPRODUCT = row.FindControl("lblPRODUCT") as Label;
                        TextBox txtGVRate = row.FindControl("txtGVRate") as TextBox;

                        string costcenter = "1007";
                        DataTable dtCostCenter = new DataTable();
                        dtCostCenter = objMainClass.GetCostCenter(lblPLANTCD.Text, lblLOCCD.Text);

                        DataTable dtItem = new DataTable();
                        dtItem = objMainClass.SelectItem("", "", lblITEMCODE.Text, "", "", "", "");

                        if (dtCostCenter.Rows.Count > 0)
                        {
                            costcenter = Convert.ToString(dtCostCenter.Rows[0]["CSTCENTCD"]);
                        }

                        if (chkSelect.Checked == true)
                        {
                            if (txtGVRate.Text != "" && txtGVRate.Text != null && txtGVRate.Text != string.Empty)
                            {
                                decimal basicrate = 0;
                                decimal taxrate = 0;
                                int pocondiid = 0;
                                decimal taxamt = 0;
                                decimal withtaxamt = 0;


                                bool blnPOExist = false;
                                DataTable dtPO = new DataTable();
                                DataTable dtSO = new DataTable();
                                dtPO = objMainClass.CheckPOSOImeiNo(objMainClass.intCmpId, lblIMEINO.Text, "CHECKPOWITHRETURN");
                                if (dtPO.Rows.Count > 0)
                                {
                                    if (Convert.ToString(dtPO.Rows[0]["LASTPO"]) != "" && Convert.ToString(dtPO.Rows[0]["LASTPO"]) != string.Empty &&
                                        Convert.ToString(dtPO.Rows[0]["LASTPO"]) != null &&
                                        Convert.ToInt32(Convert.ToString(dtPO.Rows[0]["BALQTY"]) == "" ? 0 : Convert.ToInt32(dtPO.Rows[0]["BALQTY"])) > 0)
                                    {
                                        blnPOExist = true;


                                        dtSO = objMainClass.CheckPOSOImeiNo(objMainClass.intCmpId, lblIMEINO.Text, "CHECKSOWITHRETURN");

                                        if (dtSO.Rows.Count > 0)
                                        {
                                            if (Convert.ToString(dtSO.Rows[0]["LASTSO"]) != "" && Convert.ToString(dtSO.Rows[0]["LASTSO"]) != string.Empty &&
                                        Convert.ToString(dtSO.Rows[0]["LASTSO"]) != null &&
                                        Convert.ToInt32(Convert.ToString(dtSO.Rows[0]["BALQTY"]) == "" ? 0 : Convert.ToInt32(dtSO.Rows[0]["BALQTY"])) > 0)
                                            {
                                                blnPOExist = false;
                                            }

                                        }

                                    }
                                }

                                if (blnPOExist == true)
                                {
                                    dtPOError.Rows.Add(lblIMEINO.Text, lblJOBID.Text, "PO is already made for same IMEI No.! PO No. : " + Convert.ToString(dtPO.Rows[0]["LASTPO"]) + ".!");
                                    ierror++;
                                }
                                else
                                {

                                    if (ddlGVTax.SelectedIndex > 0)
                                    {
                                        DataTable dttax = objMainClass.GetTaxCalData(ddlGVTax.SelectedValue);
                                        if (dttax.Rows.Count > 0)
                                        {
                                            taxrate = Convert.ToDecimal(dttax.Rows[0]["rate"]);
                                            pocondiid = Convert.ToInt32(dttax.Rows[0]["id"]);
                                            basicrate = Convert.ToDecimal(txtGVRate.Text);
                                            taxamt = (Convert.ToDecimal(txtGVRate.Text) * Convert.ToDecimal(dttax.Rows[0]["rate"])) / 100;
                                            withtaxamt = basicrate + taxamt;

                                            objPRDetails.Add(new PRDetails
                                            {
                                                ASSETCD = "",
                                                CAMOUNT = withtaxamt,
                                                CMPID = objMainClass.intCmpId,
                                                CSTCENTCD = costcenter,
                                                DELIDT = DateTime.Now,
                                                GLCD = Convert.ToString(dtItem.Rows[0]["GLCODE"]),  //"10010000",
                                                IMEINO = lblIMEINO.Text,
                                                ITEMDESC = lblPRODUCT.Text,
                                                ITEMGRPID = Convert.ToInt32(dtItem.Rows[0]["ITEMGRP"]),
                                                ITEMID = Convert.ToInt32(dtItem.Rows[0]["ITEMID"]),
                                                ITEMTEXT = lblJOBID.Text + "/" + lblIMEINO.Text + " AUTO PR/PO",
                                                LOCCD = lblLOCCD.Text,
                                                PARTREQNO = 0,
                                                PLANTCD = lblPLANTCD.Text,
                                                PRBY = "",
                                                PRFCNT = "1000",
                                                PRNO = "",
                                                PRQTY = 1,
                                                RATE = withtaxamt,
                                                SRNO = iPRSrno,
                                                STATUS = 2,
                                                TRNUM = lblJOBID.Text,
                                                UOM = Convert.ToInt32(dtItem.Rows[0]["UOM"]),
                                            });

                                            objPOTaxation.Add(new POTaxation
                                            {
                                                BASEAMT = basicrate,
                                                CMPID = objMainClass.intCmpId,
                                                CONDID = pocondiid,
                                                CONDORDER = iPRSrno,
                                                CONDTYPE = ddlGVTax.SelectedValue,
                                                GLCODE = "",
                                                OPERATOR = "+",
                                                PID = 0,
                                                PONO = "",
                                                POSRNO = iPRSrno,
                                                RATE = taxrate,
                                                SRNO = iPRSrno,
                                                TAXAMT = taxamt,
                                            });

                                            objPODetails.Add(new PODetails
                                            {
                                                APRVBY = 17,
                                                APRVDATE = DateTime.Now,
                                                APRVSTATUS = 260,
                                                ASSETCD = "",
                                                BRATE = basicrate,
                                                CAMOUNT = basicrate,
                                                CMPID = objMainClass.intCmpId,
                                                CSTCENTCD = costcenter,
                                                DELIDT = DateTime.Now,
                                                DEVREASON = "OK",
                                                DISCAMT = 0,
                                                FROMLOCCD = "",
                                                FROMPLANTCD = "",
                                                GLCD = "10010000",
                                                IMEINO = lblIMEINO.Text,
                                                ITEMDESC = lblPRODUCT.Text,
                                                ITEMGRPID = Convert.ToInt32(dtItem.Rows[0]["ITEMGRP"]),
                                                ITEMID = Convert.ToInt32(dtItem.Rows[0]["ITEMID"]),
                                                //ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",  //Removed AUTO PO AGAINST PO text from remarks
                                                ITEMTEXT = lblJOBID.Text + "/" + lblIMEINO.Text + " AUTO PR/PO",
                                                LOCCD = lblLOCCD.Text,
                                                LOCKAMT = 0,
                                                PLANTCD = lblPLANTCD.Text,
                                                PONO = "",
                                                POQTY = 1,
                                                PRFCNT = "1000",
                                                PRNO = "",
                                                PRSRNO = iPRSrno,
                                                RATE = withtaxamt,
                                                REFNO = "",
                                                REJREASON = "",
                                                SRNO = iPRSrno,
                                                TAXAMT = 0,
                                                TRNUM = lblJOBID.Text,
                                                UOM = Convert.ToInt32(dtItem.Rows[0]["UOM"]),
                                            });

                                            isucess++;
                                        }
                                        else
                                        {
                                            dtPOError.Rows.Add(lblIMEINO.Text, lblJOBID.Text, "Tax Rate not found with selected tax.!");
                                            ierror++;
                                        }
                                    }
                                    else
                                    {
                                        basicrate = Convert.ToDecimal(txtGVRate.Text);
                                        withtaxamt = basicrate + taxamt;

                                        objPRDetails.Add(new PRDetails
                                        {
                                            ASSETCD = "",
                                            CAMOUNT = withtaxamt,
                                            CMPID = objMainClass.intCmpId,
                                            CSTCENTCD = costcenter,
                                            DELIDT = DateTime.Now,
                                            GLCD = Convert.ToString(dtItem.Rows[0]["GLCODE"]),  //"10010000",
                                            IMEINO = lblIMEINO.Text,
                                            ITEMDESC = lblPRODUCT.Text,
                                            ITEMGRPID = Convert.ToInt32(dtItem.Rows[0]["ITEMGRP"]),
                                            ITEMID = Convert.ToInt32(dtItem.Rows[0]["ITEMID"]),
                                            ITEMTEXT = lblJOBID.Text + "/" + lblIMEINO.Text + " AUTO PR/PO",
                                            LOCCD = lblLOCCD.Text,
                                            PARTREQNO = 0,
                                            PLANTCD = lblPLANTCD.Text,
                                            PRBY = "",
                                            PRFCNT = "1000",
                                            PRNO = "",
                                            PRQTY = 1,
                                            RATE = withtaxamt,
                                            SRNO = iPRSrno,
                                            STATUS = 2,
                                            TRNUM = lblJOBID.Text,
                                            UOM = Convert.ToInt32(dtItem.Rows[0]["UOM"]),
                                        });

                                        objPODetails.Add(new PODetails
                                        {
                                            APRVBY = 17,
                                            APRVDATE = DateTime.Now,
                                            APRVSTATUS = 260,
                                            ASSETCD = "",
                                            BRATE = basicrate,
                                            CAMOUNT = basicrate,
                                            CMPID = objMainClass.intCmpId,
                                            CSTCENTCD = costcenter,
                                            DELIDT = DateTime.Now,
                                            DEVREASON = "OK",
                                            DISCAMT = 0,
                                            FROMLOCCD = "",
                                            FROMPLANTCD = "",
                                            GLCD = "10010000",
                                            IMEINO = lblIMEINO.Text,
                                            ITEMDESC = lblPRODUCT.Text,
                                            ITEMGRPID = Convert.ToInt32(dtItem.Rows[0]["ITEMGRP"]),
                                            ITEMID = Convert.ToInt32(dtItem.Rows[0]["ITEMID"]),
                                            //ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",  //Removed AUTO PO AGAINST PO text from remarks
                                            ITEMTEXT = lblJOBID.Text + "/" + lblIMEINO.Text + " AUTO PR/PO",
                                            LOCCD = lblLOCCD.Text,
                                            LOCKAMT = 0,
                                            PLANTCD = lblPLANTCD.Text,
                                            PONO = "",
                                            POQTY = 1,
                                            PRFCNT = "1000",
                                            PRNO = "",
                                            PRSRNO = iPRSrno,
                                            RATE = basicrate,
                                            REFNO = "",
                                            REJREASON = "",
                                            SRNO = iPRSrno,
                                            TAXAMT = 0,
                                            TRNUM = lblJOBID.Text,
                                            UOM = Convert.ToInt32(dtItem.Rows[0]["UOM"]),
                                        });

                                        isucess++;
                                    }

                                    totalbasicrate = totalbasicrate + basicrate;
                                    totaltaxamt = totaltaxamt + taxamt;
                                    totalwithtaxamt = totalwithtaxamt + withtaxamt;


                                }

                                iPRSrno++;

                            }
                            else
                            {
                                dtPOError.Rows.Add(lblIMEINO.Text, lblJOBID.Text, "Prodcut Rate Not Entered.");
                                ierror++;
                            }
                        }
                    }


                    if (objPRDetails.Count > 0)
                    {

                        DataTable dtPRAPI = new DataTable();
                        dtPRAPI = objMainClass.GetWAData("INSERTPR", 1, "GETWADATA");
                        if (dtPRAPI.Rows.Count > 0)
                        {
                            PRMaster objPRMaster = new PRMaster();
                            objPRMaster.CMPID = objMainClass.intCmpId;
                            objPRMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                            objPRMaster.CREATEDATE = DateTime.Now;
                            objPRMaster.DEPTID = Convert.ToInt32(ddlDepartment.SelectedValue);
                            objPRMaster.ISPRSTO = 0;
                            objPRMaster.LISTINGID = 0;
                            objPRMaster.PRDT = DateTime.Now;
                            objPRMaster.PRNO = "";
                            objPRMaster.PRTYPE = "MPR";
                            objPRMaster.REMARK = "AUTO PR/PO";
                            objPRMaster.STATUS = 57;
                            objPRMaster.VENDCODE = "";
                            objPRMaster.PRDATA = objPRDetails;




                            string PRURL = Convert.ToString(dtPRAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtPRAPI.Rows[0]["TOKEN"]);
                            var clientPR = new RestClient(PRURL);
                            clientPR.Timeout = -1;
                            var requestPR = new RestRequest(Method.POST);
                            requestPR.AddHeader("" + Convert.ToString(dtPRAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtPRAPI.Rows[0]["KEYVALUE"]) + "");
                            requestPR.AddHeader("Content-Type", "application/json");
                            var jsonInputPR = JsonConvert.SerializeObject(objPRMaster);
                            requestPR.AddParameter("application/json", jsonInputPR, ParameterType.RequestBody);
                            IRestResponse responsePR = clientPR.Execute(requestPR);

                            PRRespsonse objPRRespsonse = new PRRespsonse();
                            string jsonconnPR = responsePR.Content;
                            objPRRespsonse = JsonConvert.DeserializeObject<PRRespsonse>(jsonconnPR);

                            if (responsePR.StatusCode == HttpStatusCode.OK)
                            {
                                hfPRNo.Value = Convert.ToString(objMainClass.strConvertZeroPadding(objPRRespsonse.PRNO));

                                DataTable dtPOAPI = new DataTable();
                                dtPOAPI = objMainClass.GetWAData("INSERTPO", 1, "GETWADATA");
                                if (dtPOAPI.Rows.Count > 0)
                                {
                                    for (int podet = 0; podet < objPODetails.Count; podet++)
                                    {
                                        objPODetails[podet].PRNO = hfPRNo.Value;
                                    }

                                    #region PO Charge Data...

                                    if (txtChgAmt.Text != null && txtChgAmt.Text != string.Empty && txtChgAmt.Text != "")
                                    {
                                        if (Convert.ToInt32(txtChgAmt.Text) > 0)
                                        {
                                            DataTable dttaxcharge = new DataTable();
                                            decimal basiccharge = Convert.ToDecimal(txtChgAmt.Text);
                                            decimal condiid = 0;
                                            decimal chgrate = 0;
                                            decimal othchgtax = 0;
                                            if (ddlChargeTax.SelectedIndex > 0)
                                            {
                                                dttaxcharge = objMainClass.GetTaxCalData(ddlChargeTax.SelectedValue);
                                                condiid = Convert.ToDecimal(dttaxcharge.Rows[0]["id"]);
                                                chgrate = Convert.ToDecimal(dttaxcharge.Rows[0]["rate"]);
                                                othchgtax = (basiccharge * chgrate) / 100;

                                                totaltaxamt = totaltaxamt + othchgtax;

                                                totalwithtaxamt = totalwithtaxamt + othchgtax + basiccharge;
                                            }

                                            objPOCharges.Add(new POCharges
                                            {
                                                CHGAMT = basiccharge,
                                                CHGTYPE = ddlCharges.SelectedValue,
                                                CMPID = objMainClass.intCmpId,
                                                CONDID = condiid,
                                                CONDTYPE = ddlChargeTax.SelectedValue,
                                                GLCODE = "",
                                                OPERATOR = "+",
                                                PID = 0,
                                                PONO = "",
                                                RATE = chgrate,
                                                SRNO = 1,
                                                TAXAMT = othchgtax,
                                            });
                                        }

                                    }
                                    #endregion

                                    #region PO Master Data...
                                    POMaster objPOMaster = new POMaster();
                                    objPOMaster.ADJAMT = 0;
                                    objPOMaster.ADVAMT = 0;
                                    objPOMaster.AGENTNAME = "";
                                    objPOMaster.APRVBY = 17;
                                    objPOMaster.APRVDATE = DateTime.Now;
                                    objPOMaster.CMPID = objMainClass.intCmpId;
                                    objPOMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                    objPOMaster.CREATEDATE = DateTime.Now;
                                    objPOMaster.DEPTID = Convert.ToInt32(ddlDepartment.SelectedValue);
                                    objPOMaster.DISCOUNT = 0;
                                    objPOMaster.NETMATVALUE = totalbasicrate;
                                    objPOMaster.NETPOAMT = totalwithtaxamt;
                                    objPOMaster.NETTAXAMT = totaltaxamt;
                                    objPOMaster.OLDPOAMT = 0;
                                    objPOMaster.PENDINGAMT = totalwithtaxamt;
                                    objPOMaster.PMTTERMS = ddlPaymentTerms.SelectedItem.Text;
                                    objPOMaster.PMTTERMSDESC = txtPaymentTermsDesc.Text;
                                    objPOMaster.POCharge = objPOCharges;
                                    objPOMaster.PODetail = objPODetails;
                                    objPOMaster.PODT = DateTime.Now;
                                    objPOMaster.PONO = "";
                                    objPOMaster.POTax = objPOTaxation;
                                    objPOMaster.POTYPE = "MPO";
                                    objPOMaster.PURTYPE = Convert.ToInt32(ddlPurType.SelectedValue);
                                    objPOMaster.REMARK = "AUTO PR/PO";
                                    objPOMaster.STATUS = 57;
                                    objPOMaster.TRANCODE = txtTranCode.Text;
                                    objPOMaster.VENDCODE = txtVendorCode.Text;
                                    #endregion

                                    string POURL = Convert.ToString(dtPOAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtPOAPI.Rows[0]["TOKEN"]);
                                    var clientPO = new RestClient(POURL);
                                    clientPO.Timeout = -1;
                                    var requestPO = new RestRequest(Method.POST);
                                    requestPO.AddHeader("" + Convert.ToString(dtPOAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtPOAPI.Rows[0]["KEYVALUE"]) + "");
                                    requestPO.AddHeader("Content-Type", "application/json");
                                    var jsonInputPO = JsonConvert.SerializeObject(objPOMaster);
                                    requestPO.AddParameter("application/json", jsonInputPO, ParameterType.RequestBody);
                                    IRestResponse responsePO = clientPO.Execute(requestPO);

                                    PORespsonse objPORespsonse = new PORespsonse();
                                    string jsonconnPO = responsePO.Content;
                                    objPORespsonse = JsonConvert.DeserializeObject<PORespsonse>(jsonconnPO);

                                    if (responsePO.StatusCode == HttpStatusCode.OK)
                                    {
                                        hfPONo.Value = objPORespsonse.PONO;

                                        #region Send Mail Code...
                                        String strCustContent = "";
                                        strCustContent = fileread();
                                        strCustContent = strCustContent.Replace("###Heading###", "New PO Created by User.");
                                        strCustContent = strCustContent.Replace("###CreateBy###", Convert.ToString(Session["USERNAME"]));
                                        strCustContent = strCustContent.Replace("###CreateDate###", DateTime.Now.ToShortDateString());
                                        strCustContent = strCustContent.Replace("###PRNO###", hfPONo.Value);
                                        strCustContent = strCustContent.Replace("###Message###", "New PO created by user. Details are as per above.");
                                        strCustContent = strCustContent.Replace("###LINK###", "http://14.98.132.190:360/MM/frmApprovePO.aspx");
                                        strCustContent = strCustContent.Replace("###HREFLINK###", "http://14.98.132.190:360/MM/frmApprovePO.aspx");

                                        string plcode = Convert.ToString(objPRDetails[0].PLANTCD);
                                        DataTable dt = new DataTable();
                                        dt = objMainClass.MailSenderReceiver("PO", 1, Convert.ToInt32(ddlDepartment.SelectedValue), plcode, 12, totalwithtaxamt);
                                        string Reciever = string.Empty;
                                        if (dt.Rows.Count > 0)
                                        {

                                            for (int i = 0; i < dt.Rows.Count; i++)
                                            {

                                                if (Reciever == string.Empty)
                                                {
                                                    Reciever = Convert.ToString(dt.Rows[i]["EMAILID"]);
                                                }
                                                else
                                                {
                                                    Reciever = Reciever + ";" + Convert.ToString(dt.Rows[i]["EMAILID"]);
                                                }
                                            }
                                            objMainClass.SaveMailNotification(objMainClass.EmailID, objMainClass.Password, Convert.ToString(dt.Rows[0]["EMAILID"]), Reciever, "New PO Created", strCustContent, objMainClass.PORT, hfPONo.Value, Convert.ToString(Session["USERID"]), "MPO");


                                            //objMainClass.SendMail(strCustContent, "New PR Created", dt);
                                        }
                                        #endregion

                                        string msg = "New PR Created. PR NO. is : " + hfPRNo.Value + ". New PO Created. PO NO. is : " + hfPONo.Value + ".!";
                                        //AutoEntry();
                                        JobStatusEntry();
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" " + msg + "\");$('.close').click(function(){window.location.href ='frmBulkPOCreate.aspx' });", true);
                                    }
                                    else
                                    {
                                        string msg = "New PR Created. PR NO. is : " + hfPRNo.Value + ". PO not created. " + objPORespsonse.MESSAGE + ".!";
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + msg + "\");$('.close').click(function(){window.location.href ='frmBulkPOCreate.aspx' });", true);
                                    }

                                }
                                else
                                {
                                    string msg = "PO creation API not found. Please contact API provider.! PR Created successfully. PR No. : " + hfPRNo.Value;
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + msg + "\");$('.close').click(function(){window.location.href ='frmBulkPOCreate.aspx' });", true);
                                }


                            }
                            else
                            {
                                string msg = "Something went wrong while creating PR. " + objPRRespsonse.MESSAGE;
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + msg + "\");$('.close').click(function(){window.location.href ='frmBulkPOCreate.aspx' });", true);
                            }


                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('PR creation API not found. Please contact API provider.!');", true);
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
            finally
            {
                if (dtPOError.Rows.Count > 0)
                {
                    Session["poerror"] = dtPOError;

                    string message = "Error in uploaded data - " + ierror + ". Please check downloaded file for more details.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\" Error : " + message + "\");$('.close').click(function(){window.location.href ='frmBulkPOCreate.aspx' });", true);
                    string path = "frmPODataDownload.aspx";
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "');", true);
                }
            }
        }


        public void JobStatusEntry()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    foreach (GridViewRow row in grvData.Rows)
                    {
                        Label lblItemCode = row.FindControl("lblITEMCODE") as Label;
                        Label lblPlant = row.FindControl("lblPLANTCD") as Label;
                        Label lblLocation = row.FindControl("lblLOCCD") as Label;
                        Label lblJobID = row.FindControl("lblJOBID") as Label;
                        CheckBox chkSelect = row.FindControl("chkSelect") as CheckBox;


                        if (chkSelect.Checked == true)
                        {
                            string JOBID = objMainClass.strConvertZeroPadding(lblJobID.Text);
                            string LOCATION = lblLocation.Text;
                            string PLANTCD = lblPlant.Text;
                            string ITEMCODE = lblItemCode.Text;

                            DataTable dtItem = new DataTable();
                            dtItem = objMainClass.SelectItem("", "", ITEMCODE, "", "", "", "");

                            DataTable dtJobStageId = new DataTable();
                            dtJobStageId = objMainClass.GetJobIDStageDtl(objMainClass.intCmpId, JOBID);
                            if (dtJobStageId.Rows.Count > 0)
                            {
                                int currStage = Convert.ToInt32(dtJobStageId.Rows[0]["STAGEID"]);
                                string segment = Convert.ToString(dtJobStageId.Rows[0]["SEGMENT"]);

                                DataTable dtSegmentDtl = new DataTable();
                                dtSegmentDtl = objMainClass.GetSegmentDtl(objMainClass.intCmpId, segment);
                                if (Convert.ToInt32(dtSegmentDtl.Rows[0]["POAUTOENTRY"]) == 1)
                                {
                                    int maxstageautoentry = Convert.ToInt32(dtSegmentDtl.Rows[0]["MAXSTAGEAUTOENTRY"]);

                                    DataTable dtStageSeq = new DataTable();
                                    dtStageSeq = objMainClass.GetSegmentStageData(currStage, segment, "GETSTAGEREQBYSTAGEID");
                                    if (dtStageSeq.Rows.Count > 0)
                                    {
                                        DataTable dtInsertStage = new DataTable();
                                        dtInsertStage = objMainClass.GetWAData("INSERTSTAGE", 1, "GETWADATA");

                                        DataTable dtJobStatus = new DataTable();
                                        dtJobStatus = objMainClass.GetWAData("UPDATESTATUS", 1, "GETWADATA");

                                        if (dtInsertStage.Rows.Count > 0)
                                        {
                                            if (dtJobStatus.Rows.Count > 0)
                                            {
                                                for (int s = 0; s < dtStageSeq.Rows.Count; s++)
                                                {
                                                    if (Convert.ToInt32(dtStageSeq.Rows[s]["STAGEID"]) <= maxstageautoentry)
                                                    {
                                                        int JOBSTAGEID = Convert.ToInt32(dtStageSeq.Rows[s]["STAGEID"]);
                                                        int JOBSTATUSID = objMainClass.GetStatusByStageID(JOBSTAGEID);
                                                        string NEWJOBID = objMainClass.strConvertZeroPadding(JOBID);
                                                        string NEWJCNO = "";
                                                        string lblProductID = "";
                                                        string MODEL = Convert.ToString(dtItem.Rows[0]["MODEL"]);
                                                        DataTable dtProdItem = new DataTable();
                                                        dtProdItem = objMainClass.GetProditembyModel(objMainClass.intCmpId, MODEL);
                                                        if (dtProdItem.Rows.Count > 0)
                                                        {
                                                            lblProductID = Convert.ToString(dtProdItem.Rows[0]["PRODITEMID"]);
                                                        }
                                                        else
                                                        {
                                                            lblProductID = "1";
                                                        }



                                                        string URLStage = Convert.ToString(dtInsertStage.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertStage.Rows[0]["TOKEN"]);
                                                        URLStage = URLStage + "?DOCNO=" + NEWJOBID + "&DOCTYPE=JS&STAGEID=" + JOBSTAGEID + "&STATRES=AUTO ENTRY JOB CREATION&CREATBY=" + Convert.ToInt32(Session["USERID"]);
                                                        var clientStage = new RestClient(URLStage);
                                                        clientStage.Timeout = -1;
                                                        var requestStage = new RestRequest(Method.POST);
                                                        requestStage.AddHeader("" + Convert.ToString(dtInsertStage.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertStage.Rows[0]["KEYVALUE"]) + "");
                                                        requestStage.AddHeader("Content-Type", "application/json");
                                                        IRestResponse responseStage = clientStage.Execute(requestStage);

                                                        string URLStatus = Convert.ToString(dtJobStatus.Rows[0]["OTHER"]) + "" + Convert.ToString(dtJobStatus.Rows[0]["TOKEN"]);
                                                        URLStatus = URLStatus + "?CMPID=" + objMainClass.intCmpId + "&JOBID=" + NEWJOBID + "&STAGEID=" + JOBSTAGEID + "&JOBSTATUS=" + JOBSTATUSID + "&STATRES=AUTO ENTRY JOB CREATION&STATUPDATEDT=" + DateTime.Now.ToString() + "&UPDATEDATE=" + DateTime.Now.ToString() + "&CREATEBY=" + Convert.ToInt32(Session["USERID"]);
                                                        var clientStatus = new RestClient(URLStatus);
                                                        clientStatus.Timeout = -1;
                                                        var requestStatus = new RestRequest(Method.POST);
                                                        requestStatus.AddHeader("" + Convert.ToString(dtJobStatus.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtJobStatus.Rows[0]["KEYVALUE"]) + "");
                                                        requestStatus.AddHeader("Content-Type", "application/json");
                                                        //var jsonInputStatus = JsonConvert.SerializeObject(objReverseWaybillUpdate);
                                                        //requestStatus.AddParameter("application/json", jsonInputStatus, ParameterType.RequestBody);
                                                        IRestResponse responseStatus = clientStatus.Execute(requestStatus);

                                                        if (JOBSTAGEID == 11)
                                                        {
                                                            DataTable dtInsertJC = new DataTable();
                                                            dtInsertJC = objMainClass.GetWAData("CREATEJC", 1, "GETWADATA");
                                                            if (dtInsertJC.Rows.Count > 0)
                                                            {
                                                                JobCardMaster objJobCardMaster = new JobCardMaster();
                                                                objJobCardMaster.BACKCOVERFLAG = "Y";
                                                                objJobCardMaster.CMPID = objMainClass.intCmpId;
                                                                objJobCardMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                objJobCardMaster.CREATEDATE = Convert.ToDateTime(DateTime.Now).ToString();
                                                                objJobCardMaster.ITEMID = Convert.ToInt32(lblProductID);
                                                                objJobCardMaster.JCDT = Convert.ToDateTime(DateTime.Now).ToString();
                                                                objJobCardMaster.JCNO = "";
                                                                objJobCardMaster.JOBID = NEWJOBID;
                                                                objJobCardMaster.JOBIDSRNO = 1;
                                                                objJobCardMaster.JOBSTATUS = (int)STATUS.JCSaved;
                                                                objJobCardMaster.LOCCD = LOCATION;
                                                                objJobCardMaster.PLANTCD = PLANTCD;
                                                                objJobCardMaster.QTY = 1;
                                                                objJobCardMaster.STAGEID = 0;// JOBSTAGEID;
                                                                objJobCardMaster.UOM = 1;
                                                                objJobCardMaster.WRKCNT = "WR01";

                                                                string URLJC = Convert.ToString(dtInsertJC.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertJC.Rows[0]["TOKEN"]);
                                                                var clientJC = new RestClient(URLJC);
                                                                clientJC.Timeout = -1;
                                                                var requestJC = new RestRequest(Method.POST);
                                                                requestJC.AddHeader("" + Convert.ToString(dtInsertJC.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertJC.Rows[0]["KEYVALUE"]) + "");
                                                                requestJC.AddHeader("Content-Type", "application/json");
                                                                var jsonInputJC = JsonConvert.SerializeObject(objJobCardMaster);
                                                                requestJC.AddParameter("application/json", jsonInputJC, ParameterType.RequestBody);
                                                                IRestResponse responseJC = clientJC.Execute(requestJC);

                                                                JobCardResponse objJobCardResponse = new JobCardResponse();
                                                                string jsonconnJC = responseJC.Content;
                                                                objJobCardResponse = JsonConvert.DeserializeObject<JobCardResponse>(jsonconnJC);

                                                                NEWJCNO = objJobCardResponse.JCNO;

                                                                #region Code for Production entry...

                                                                if (Convert.ToInt32(dtSegmentDtl.Rows[0]["JCAUTOENTRY"]) == 1)
                                                                {
                                                                    if (NEWJCNO != null && NEWJCNO != "" && NEWJCNO != string.Empty)
                                                                    {
                                                                        DataTable dtInsertJCDetails = new DataTable();
                                                                        dtInsertJCDetails = objMainClass.GetWAData("INSERTJCDETAILS", 1, "GETWADATA");
                                                                        if (dtInsertJCDetails.Rows.Count > 0)
                                                                        {
                                                                            #region 50 Inward Inspection Entry...

                                                                            JobCardDetails objJobCardDetails = new JobCardDetails();
                                                                            objJobCardDetails.ASCPARTCODE = "";
                                                                            objJobCardDetails.CMPID = objMainClass.intCmpId;
                                                                            objJobCardDetails.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                            objJobCardDetails.CREATEDATE = Convert.ToDateTime(DateTime.Now).ToString();
                                                                            objJobCardDetails.JCNO = NEWJCNO;
                                                                            objJobCardDetails.JOBDONE = "NA";
                                                                            objJobCardDetails.JOBDONEBY = Convert.ToInt32(Session["USERID"]);
                                                                            objJobCardDetails.JOBPROBID = 146;
                                                                            objJobCardDetails.JOBPROBID1 = 0;
                                                                            objJobCardDetails.JOBPROBID2 = 0;
                                                                            objJobCardDetails.JOBPROBID3 = 0;
                                                                            objJobCardDetails.NEWIMEINO = "";
                                                                            objJobCardDetails.NEXTSTAGEREQ = 14;
                                                                            objJobCardDetails.NOTE = "OK FOR CHECK";
                                                                            objJobCardDetails.PARTREPLACED = "";
                                                                            objJobCardDetails.PARTREQ = "";
                                                                            objJobCardDetails.PARTREQID = 0;
                                                                            objJobCardDetails.PROBLEM = "OK FOR CHECK";
                                                                            objJobCardDetails.PROBLEM1 = "";
                                                                            objJobCardDetails.PROBLEM2 = "";
                                                                            objJobCardDetails.PROBLEM3 = "";
                                                                            objJobCardDetails.RESULT = 25;
                                                                            objJobCardDetails.STAGEID = 50;
                                                                            objJobCardDetails.STARTDT = DateTime.Now;
                                                                            objJobCardDetails.ENDDT = DateTime.Now;
                                                                            objJobCardDetails.JOBID = NEWJOBID;

                                                                            string URLJCDetails = Convert.ToString(dtInsertJCDetails.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertJCDetails.Rows[0]["TOKEN"]);
                                                                            var clientJCDetails = new RestClient(URLJCDetails);
                                                                            clientJCDetails.Timeout = -1;
                                                                            var requestJCDetails = new RestRequest(Method.POST);
                                                                            requestJCDetails.AddHeader("" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYVALUE"]) + "");
                                                                            requestJCDetails.AddHeader("Content-Type", "application/json");
                                                                            var jsonInputJCDetails = JsonConvert.SerializeObject(objJobCardDetails);
                                                                            requestJCDetails.AddParameter("application/json", jsonInputJCDetails, ParameterType.RequestBody);
                                                                            IRestResponse responseES = clientJCDetails.Execute(requestJCDetails);

                                                                            #endregion

                                                                            #region 14 ELS Entry...

                                                                            JobCardDetails objJobCardDetails1 = new JobCardDetails();
                                                                            objJobCardDetails1.ASCPARTCODE = "";
                                                                            objJobCardDetails1.CMPID = objMainClass.intCmpId;
                                                                            objJobCardDetails1.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                            objJobCardDetails1.CREATEDATE = Convert.ToDateTime(DateTime.Now).ToString();
                                                                            objJobCardDetails1.JCNO = NEWJCNO;
                                                                            objJobCardDetails1.JOBDONE = "NA";
                                                                            objJobCardDetails1.JOBDONEBY = Convert.ToInt32(Session["USERID"]);
                                                                            objJobCardDetails1.JOBPROBID = 146;
                                                                            objJobCardDetails1.JOBPROBID1 = 0;
                                                                            objJobCardDetails1.JOBPROBID2 = 0;
                                                                            objJobCardDetails1.JOBPROBID3 = 0;
                                                                            objJobCardDetails1.NEWIMEINO = "";
                                                                            objJobCardDetails1.NEXTSTAGEREQ = 20;
                                                                            objJobCardDetails1.NOTE = "OK FOR CHECK";
                                                                            objJobCardDetails1.PARTREPLACED = "";
                                                                            objJobCardDetails1.PARTREQ = "";
                                                                            objJobCardDetails1.PARTREQID = 0;
                                                                            objJobCardDetails1.PROBLEM = "OK FOR CHECK";
                                                                            objJobCardDetails1.PROBLEM1 = "";
                                                                            objJobCardDetails1.PROBLEM2 = "";
                                                                            objJobCardDetails1.PROBLEM3 = "";
                                                                            objJobCardDetails1.RESULT = 25;
                                                                            objJobCardDetails1.STAGEID = 14;
                                                                            objJobCardDetails1.STARTDT = DateTime.Now;
                                                                            objJobCardDetails1.ENDDT = DateTime.Now;
                                                                            objJobCardDetails1.JOBID = NEWJOBID;

                                                                            string URLJCDetails1 = Convert.ToString(dtInsertJCDetails.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertJCDetails.Rows[0]["TOKEN"]);
                                                                            var clientJCDetails1 = new RestClient(URLJCDetails1);
                                                                            clientJCDetails1.Timeout = -1;
                                                                            var requestJCDetails1 = new RestRequest(Method.POST);
                                                                            requestJCDetails1.AddHeader("" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYVALUE"]) + "");
                                                                            requestJCDetails1.AddHeader("Content-Type", "application/json");
                                                                            var jsonInputJCDetails1 = JsonConvert.SerializeObject(objJobCardDetails1);
                                                                            requestJCDetails1.AddParameter("application/json", jsonInputJCDetails1, ParameterType.RequestBody);
                                                                            IRestResponse responseES1 = clientJCDetails1.Execute(requestJCDetails1);

                                                                            #endregion

                                                                            #region 20 QC1 Entry...

                                                                            JobCardDetails objJobCardDetails2 = new JobCardDetails();
                                                                            objJobCardDetails2.ASCPARTCODE = "";
                                                                            objJobCardDetails2.CMPID = objMainClass.intCmpId;
                                                                            objJobCardDetails2.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                            objJobCardDetails2.CREATEDATE = Convert.ToDateTime(DateTime.Now).ToString();
                                                                            objJobCardDetails2.JCNO = NEWJCNO;
                                                                            objJobCardDetails2.JOBDONE = "NA";
                                                                            objJobCardDetails2.JOBDONEBY = Convert.ToInt32(Session["USERID"]);
                                                                            objJobCardDetails2.JOBPROBID = 146;
                                                                            objJobCardDetails2.JOBPROBID1 = 0;
                                                                            objJobCardDetails2.JOBPROBID2 = 0;
                                                                            objJobCardDetails2.JOBPROBID3 = 0;
                                                                            objJobCardDetails2.NEWIMEINO = "";
                                                                            objJobCardDetails2.NEXTSTAGEREQ = 64;
                                                                            objJobCardDetails2.NOTE = "OK FOR CHECK";
                                                                            objJobCardDetails2.PARTREPLACED = "";
                                                                            objJobCardDetails2.PARTREQ = "";
                                                                            objJobCardDetails2.PARTREQID = 0;
                                                                            objJobCardDetails2.PROBLEM = "OK FOR CHECK";
                                                                            objJobCardDetails2.PROBLEM1 = "";
                                                                            objJobCardDetails2.PROBLEM2 = "";
                                                                            objJobCardDetails2.PROBLEM3 = "";
                                                                            objJobCardDetails2.RESULT = 25;
                                                                            objJobCardDetails2.STAGEID = 20;
                                                                            objJobCardDetails2.STARTDT = DateTime.Now;
                                                                            objJobCardDetails2.ENDDT = DateTime.Now;
                                                                            objJobCardDetails2.JOBID = NEWJOBID;

                                                                            string URLJCDetails2 = Convert.ToString(dtInsertJCDetails.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertJCDetails.Rows[0]["TOKEN"]);
                                                                            var clientJCDetails2 = new RestClient(URLJCDetails2);
                                                                            clientJCDetails2.Timeout = -1;
                                                                            var requestJCDetails2 = new RestRequest(Method.POST);
                                                                            requestJCDetails2.AddHeader("" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYVALUE"]) + "");
                                                                            requestJCDetails2.AddHeader("Content-Type", "application/json");
                                                                            var jsonInputJCDetails2 = JsonConvert.SerializeObject(objJobCardDetails2);
                                                                            requestJCDetails2.AddParameter("application/json", jsonInputJCDetails2, ParameterType.RequestBody);
                                                                            IRestResponse responseES2 = clientJCDetails2.Execute(requestJCDetails2);

                                                                            #endregion

                                                                            #region 64 PDI Entry...

                                                                            JobCardDetails objJobCardDetails3 = new JobCardDetails();
                                                                            objJobCardDetails3.ASCPARTCODE = "";
                                                                            objJobCardDetails3.CMPID = objMainClass.intCmpId;
                                                                            objJobCardDetails3.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                            objJobCardDetails3.CREATEDATE = Convert.ToDateTime(DateTime.Now).ToString();
                                                                            objJobCardDetails3.JCNO = NEWJCNO;
                                                                            objJobCardDetails3.JOBDONE = "NA";
                                                                            objJobCardDetails3.JOBDONEBY = Convert.ToInt32(Session["USERID"]);
                                                                            objJobCardDetails3.JOBPROBID = 146;
                                                                            objJobCardDetails3.JOBPROBID1 = 0;
                                                                            objJobCardDetails3.JOBPROBID2 = 0;
                                                                            objJobCardDetails3.JOBPROBID3 = 0;
                                                                            objJobCardDetails3.NEWIMEINO = "";
                                                                            objJobCardDetails3.NEXTSTAGEREQ = 59;
                                                                            objJobCardDetails3.NOTE = "OK FOR CHECK";

                                                                            objJobCardDetails3.PARTREPLACED = "";
                                                                            objJobCardDetails3.PARTREQ = "";
                                                                            objJobCardDetails3.PARTREQID = 0;
                                                                            objJobCardDetails3.PROBLEM = "OK FOR CHECK";
                                                                            objJobCardDetails3.PROBLEM1 = "";
                                                                            objJobCardDetails3.PROBLEM2 = "";
                                                                            objJobCardDetails3.PROBLEM3 = "";
                                                                            objJobCardDetails3.RESULT = 25;
                                                                            objJobCardDetails3.STAGEID = 64;
                                                                            objJobCardDetails3.STARTDT = DateTime.Now;
                                                                            objJobCardDetails3.ENDDT = DateTime.Now;
                                                                            objJobCardDetails3.JOBID = NEWJOBID;

                                                                            string URLJCDetails3 = Convert.ToString(dtInsertJCDetails.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertJCDetails.Rows[0]["TOKEN"]);
                                                                            var clientJCDetails3 = new RestClient(URLJCDetails3);
                                                                            clientJCDetails3.Timeout = -1;
                                                                            var requestJCDetails3 = new RestRequest(Method.POST);
                                                                            requestJCDetails3.AddHeader("" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYVALUE"]) + "");
                                                                            requestJCDetails3.AddHeader("Content-Type", "application/json");
                                                                            var jsonInputJCDetails3 = JsonConvert.SerializeObject(objJobCardDetails3);
                                                                            requestJCDetails3.AddParameter("application/json", jsonInputJCDetails3, ParameterType.RequestBody);
                                                                            IRestResponse responseES3 = clientJCDetails3.Execute(requestJCDetails3);

                                                                            #endregion

                                                                            #region 59 Packed Entry...

                                                                            JobCardDetails objJobCardDetails4 = new JobCardDetails();
                                                                            objJobCardDetails4.ASCPARTCODE = "";
                                                                            objJobCardDetails4.CMPID = objMainClass.intCmpId;
                                                                            objJobCardDetails4.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                            objJobCardDetails4.CREATEDATE = Convert.ToDateTime(DateTime.Now).ToString();
                                                                            objJobCardDetails4.JCNO = NEWJCNO;
                                                                            objJobCardDetails4.JOBDONE = "NA";
                                                                            objJobCardDetails4.JOBDONEBY = Convert.ToInt32(Session["USERID"]);
                                                                            objJobCardDetails4.JOBPROBID = 146;
                                                                            objJobCardDetails4.JOBPROBID1 = 0;
                                                                            objJobCardDetails4.JOBPROBID2 = 0;
                                                                            objJobCardDetails4.JOBPROBID3 = 0;
                                                                            objJobCardDetails4.NEWIMEINO = "";
                                                                            objJobCardDetails4.NEXTSTAGEREQ = 59;
                                                                            objJobCardDetails4.NOTE = "OK FOR CHECK";
                                                                            objJobCardDetails4.PARTREPLACED = "";
                                                                            objJobCardDetails4.PARTREQ = "";
                                                                            objJobCardDetails4.PARTREQID = 0;
                                                                            objJobCardDetails4.PROBLEM = "OK FOR CHECK";
                                                                            objJobCardDetails4.PROBLEM1 = "";
                                                                            objJobCardDetails4.PROBLEM2 = "";
                                                                            objJobCardDetails4.PROBLEM3 = "";
                                                                            objJobCardDetails4.RESULT = 25;
                                                                            objJobCardDetails4.STAGEID = 59;
                                                                            objJobCardDetails4.STARTDT = DateTime.Now;
                                                                            objJobCardDetails4.ENDDT = DateTime.Now;
                                                                            objJobCardDetails4.JOBID = NEWJOBID;

                                                                            string URLJCDetails4 = Convert.ToString(dtInsertJCDetails.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertJCDetails.Rows[0]["TOKEN"]);
                                                                            var clientJCDetails4 = new RestClient(URLJCDetails4);
                                                                            clientJCDetails4.Timeout = -1;
                                                                            var requestJCDetails4 = new RestRequest(Method.POST);
                                                                            requestJCDetails4.AddHeader("" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYVALUE"]) + "");
                                                                            requestJCDetails4.AddHeader("Content-Type", "application/json");
                                                                            var jsonInputJCDetails4 = JsonConvert.SerializeObject(objJobCardDetails4);
                                                                            requestJCDetails4.AddParameter("application/json", jsonInputJCDetails4, ParameterType.RequestBody);
                                                                            IRestResponse responseES4 = clientJCDetails4.Execute(requestJCDetails4);

                                                                            #endregion


                                                                        }
                                                                        else
                                                                        {
                                                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Job Card Details API Not Found. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                                                                        }
                                                                    }
                                                                }
                                                                //else
                                                                //{
                                                                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Job Card Not Created. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                                                                //}
                                                                #endregion

                                                            }
                                                            //else
                                                            //{
                                                            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Job Card Not Created. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                                                            //}
                                                        }

                                                    }
                                                }
                                            }

                                        }

                                    }
                                }
                            }
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
        public void AutoEntry()
        {
            try
            {
                if (Session["USERID"] != null)
                {


                    foreach (GridViewRow row in grvData.Rows)
                    {
                        CheckBox chkSelect = row.FindControl("chkSelect") as CheckBox;
                        DropDownList ddlGVTax = row.FindControl("ddlGVTax") as DropDownList;
                        Label lblJOBID = row.FindControl("lblJOBID") as Label;
                        Label lblJOBSTATUS = row.FindControl("lblJOBSTATUS") as Label;
                        Label lblJOBSTATDESC = row.FindControl("lblJOBSTATDESC") as Label;
                        Label lblSTAGEID = row.FindControl("lblSTAGEID") as Label;
                        Label lblSEGMENT = row.FindControl("lblSEGMENT") as Label;
                        Label lblDISTCHNL = row.FindControl("lblDISTCHNL") as Label;
                        Label lblPRODMAKE = row.FindControl("lblPRODMAKE") as Label;
                        Label lblPRODMODEL = row.FindControl("lblPRODMODEL") as Label;
                        Label lblIMEINO = row.FindControl("lblIMEINO") as Label;
                        Label lblIMEINO2 = row.FindControl("lblIMEINO2") as Label;
                        Label lblPLANTCD = row.FindControl("lblPLANTCD") as Label;
                        Label lblLOCCD = row.FindControl("lblLOCCD") as Label;
                        Label lblITEMCODE = row.FindControl("lblITEMCODE") as Label;
                        Label lblPRODUCT = row.FindControl("lblPRODUCT") as Label;
                        TextBox txtGVRate = row.FindControl("txtGVRate") as TextBox;

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

        protected static string fileread()
        {
            string actualfol;
            actualfol = HttpContext.Current.Server.MapPath("~/PRMailBody.html");
            StreamReader sr = new StreamReader(actualfol);
            string strcontent = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            return strcontent;
        }

        protected void grvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        DataRowView dr = (DataRowView)e.Row.DataItem;
                        DropDownList ddlGVTax = e.Row.FindControl("ddlGVTax") as DropDownList;
                        objBindDDL.FillCondition(ddlGVTax);
                        ddlGVTax.SelectedValue = "GST";
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