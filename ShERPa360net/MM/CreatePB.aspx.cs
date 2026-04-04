using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.MM
{
    public partial class CreatePB : System.Web.UI.Page
    {
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        DataTable dtItem = new DataTable();
        DataTable dtTAX = new DataTable();
        DataTable dtCharges = new DataTable();
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
                            //Session.Abandon();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }

                        if (FormRights.bAdd == false)
                        {
                            imgSaveAll.Enabled = false;
                        }

                        if (Request.QueryString.Count > 0)
                        {
                            if (Convert.ToString(Request.QueryString["PONO"]) != null && Convert.ToString(Request.QueryString["PONO"]) != string.Empty && Convert.ToString(Request.QueryString["PONO"]) != "")
                            {
                                Session["EditPONo"] = Convert.ToString(Request.QueryString["PONO"]);
                            }
                            else if (Convert.ToString(Request.QueryString["FormName"]) == "FromReq")
                            {
                                Session["ReqNo"] = Convert.ToString(Request.QueryString["ReqNo"]);
                            }
                            Response.Redirect(Request.Url.AbsolutePath, false);
                        }
                        else
                        {
                            objBindDDL.FillDocType(ddlDoctype, "PB");
                            ddlDoctype.SelectedIndex = 1;
                            ddlDoctype.Enabled = false;

                            objBindDDL.FillCondition(ddlConditionType);
                            objBindDDL.FillPurChrgType(ddlCharges, objMainClass.intCmpId);

                            objBindDDL.FillPlant(ddlPLant);
                            ddlPLant.SelectedIndex = 1;
                            objBindDDL.FillPayTerm(ddlPaymentTerms);
                            ddlPaymentTerms.SelectedIndex = 1;
                            txtPaymentTermsDesc.Text = Convert.ToString(ddlPaymentTerms.SelectedItem.Text.Split('-')[1].Trim());
                            objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);
                            if (ddlPLant.SelectedValue == "1001")
                            {
                                ddlLocation.SelectedValue = "M001";
                            }
                            else
                            {
                                ddlLocation.SelectedValue = "MS01";
                            }
                            objBindDDL.FillUOM(ddlUOM);
                            ddlPLant.SelectedIndex = 1;
                            txtPBNO.Text = objMainClass.MAXPRNO(ddlDoctype.SelectedValue, "PB");
                            txtPBDATE.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();
                            DataTable dt = objMainClass.GetPRFCost(ddlPLant.SelectedValue, Convert.ToString(Session["USERID"]));
                            txtProfitCenter.Text = Convert.ToString(dt.Rows[0]["PRFCNT"]);
                            //txtCostCenter.Text    = Convert.ToString(dt.Rows[0]["CSTCENTCD"]);
                            objBindDDL.FillCostCenter(ddlCostCenter, ddlPLant.SelectedValue, ddlLocation.SelectedValue);
                            Session["savedet"] = "Save Item";
                            Session["saveall"] = "Save All";
                            Session["saveCharge"] = "Save Charge";
                            SetUpGrid();
                            ScriptManager.GetCurrent(this.Page).SetFocus(this.txtVendor);
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

        #region ADDUPDATEDELETEPURCHASEBILL
        protected void imgSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    string validatePurchaseBill = validatePurchaseBillData();
                    if (validatePurchaseBill.Length == 0)
                    {
                        if (Convert.ToString(Session["saveall"]) == "Save All")
                        {
                            if (grvListItem.Rows.Count > 0)
                            {
                                string PLantRights = string.Empty;
                                string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                                for (int i = 0; i < plantArray.Count(); i++)
                                {
                                    if (plantArray[i] == ((Label)grvListItem.Rows[0].FindControl("lblGVPlantCD")).Text)
                                    {
                                        PLantRights = ((Label)grvListItem.Rows[0].FindControl("lblGVPlantCD")).Text;
                                    }
                                }

                                if (PLantRights.Length > 0)
                                {
                                    string PBNo = objMainClass.SavePurchaseBill(objMainClass.intCmpId, ddlDoctype.SelectedItem.Text, ddlDoctype.SelectedItem.Text, txtPBDATE.Text
                                                , txtVendor.Text, txtBillNo.Text, txtBillDate.Text, ddlPaymentTerms.SelectedValue, txtPaymentTermsDesc.Text
                                                , Convert.ToDecimal(txtMaterialValue.Text), Convert.ToDecimal(txtTaxAmount.Text), Convert.ToDecimal(txtNetDiscount.Text)
                                                , Convert.ToDecimal(txtNetAmount.Text), txtREMARKS.Text, 1, grvListItem, grvTaxation, grvCharges, Convert.ToString(Session["USERID"])
                                                , "ADD");
                                    if (PBNo != "")
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Record save sucessfully.  PB No. : " + PBNo + "\");$('.close').click(function(){window.location.href ='ViewPB.aspx' });", true);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Record not inserted sucessfully!');", true);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have plant rights.');$('#txtJobId').focus();", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please Enter Item Details!');", true);
                            }
                        }
                        else if (Convert.ToString(Session["saveall"]) == "Update All")
                        {
                            if (grvListItem.Rows.Count > 0)
                            {

                                string PLantRights = string.Empty;
                                string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                                for (int i = 0; i < plantArray.Count(); i++)
                                {
                                    if (plantArray[i] == ((Label)grvListItem.Rows[0].FindControl("lblGVPlantID")).Text)
                                    {
                                        PLantRights = ((Label)grvListItem.Rows[0].FindControl("lblGVPlantID")).Text;
                                    }
                                }

                                if (PLantRights.Length > 0)
                                {
                                    string PBNo = objMainClass.SavePurchaseBill(objMainClass.intCmpId, ddlDoctype.SelectedItem.Text, ddlDoctype.SelectedItem.Text, txtPBDATE.Text
                                    , txtVendor.Text, txtBillNo.Text, txtBillDate.Text, ddlPaymentTerms.SelectedValue, ddlPaymentTerms.SelectedItem.Text
                                    , Convert.ToDecimal(txtMaterialValue.Text), Convert.ToDecimal(txtTaxAmount.Text), Convert.ToDecimal(txtNetDiscount.Text)
                                    , Convert.ToDecimal(txtNetAmount.Text), txtREMARKS.Text, 1, grvListItem, grvTaxation, grvCharges, Convert.ToString(Session["USERID"])
                                    , "UPDATE");
                                    if (PBNo != "")
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Record updated sucessfully.  PB No. : " + PBNo + "\");$('.close').click(function(){window.location.href ='ViewPB.aspx' });", true);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Record not updated!');", true);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have plant rights.');$('#txtJobId').focus();", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please Enter Item Details!');", true);
                            }
                        }
                        Session["EditPONo"] = null;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Record not inserted sucessfully should check Po No GRN No GRN Sr No is valid or Not and Bill No is already exist or not!');", true);
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

        protected void txtVendor_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtVendor.Text.Length >= 5)
                {
                    lblVendorError.Text = string.Empty;
                    lblVendorError.Visible = false;
                    DataTable dt = objMainClass.GetVendorDetails(objMainClass.intCmpId, txtVendor.Text, "");
                    if (dt.Rows.Count > 0)
                    {
                        lblVendorError.Text = string.Empty;
                        lblVendorError.Visible = false;
                        txtVendorName.Text = Convert.ToString(dt.Rows[0]["NAME1"]);
                        txtVendor.Text = Convert.ToString(dt.Rows[0]["VENDCODE"]);
                    }
                    else
                    {
                        lblVendorError.Text = "Invalid Vendor Code. Please Enter Correct Vendor Code.";
                        lblVendorError.Visible = true;
                        txtVendorName.Text = string.Empty;
                        txtVendor.Text = string.Empty;
                        txtVendor.Focus();
                        ScriptManager.GetCurrent(this.Page).SetFocus(this.txtVendor);
                    }
                }
                else
                {
                    lblVendorError.Text = "Minimum 5 digit req.";
                    lblVendorError.Visible = true;
                    txtVendorName.Text = string.Empty;
                    txtVendor.Text = string.Empty;
                    txtVendor.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        private void callAMT()
        {
            decimal materialvalue = 0;
            decimal taxvalue = 0;
            decimal discountvalue = 0;
            decimal chargesvalue = 0;
            decimal totalvalue = 0;

            for (int i = 0; i < grvListItem.Rows.Count; i++)
            {
                GridViewRow row = grvListItem.Rows[i];
                materialvalue = materialvalue + Convert.ToDecimal(((Label)row.FindControl("lblGVAmount")).Text);
                discountvalue = discountvalue + Convert.ToDecimal(((Label)row.FindControl("lblGVDiscount")).Text);
            }

            for (int j = 0; j < grvTaxation.Rows.Count; j++)
            {
                GridViewRow row = grvTaxation.Rows[j];
                string gvoperator = Convert.ToString(((Label)row.FindControl("lblTaxOperator")).Text);
                if (gvoperator == "+")
                {
                    taxvalue = taxvalue + Convert.ToDecimal(((Label)row.FindControl("lblTaxAmount")).Text);
                }
                if (gvoperator == "-")
                {
                    taxvalue = taxvalue - Convert.ToDecimal(((Label)row.FindControl("lblTaxAmount")).Text);
                }
            }

            for (int k = 0; k < grvCharges.Rows.Count; k++)
            {
                GridViewRow row = grvCharges.Rows[k];
                chargesvalue = chargesvalue + Convert.ToDecimal(((Label)row.FindControl("lblChrgAmount")).Text);
            }

            txtMaterialValue.Text = Convert.ToString(materialvalue);
            txtTaxAmount.Text = Convert.ToString(taxvalue);
            txtNetDiscount.Text = Convert.ToString(discountvalue);
            txtOtherCharges.Text = Convert.ToString(chargesvalue);
            totalvalue = materialvalue + taxvalue - discountvalue + chargesvalue;
            txtNetAmount.Text = Convert.ToString(totalvalue);
        }

        private void SetUpGrid()
        {
            try
            {
                DataColumn dtColumn;

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "PONO";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "POSRNO";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "MIRNO";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "MIRSRNO";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "SRNO";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMID";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMCODE";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMDESC";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMGRPID";
                dtColumn.ReadOnly = false;
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMGRP";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "UOM";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "UOMDesc";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "PBQTY";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "BRate";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "RATE";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "CAMOUNT";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "DISCAMT";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "GLCD";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "CSTCENTCD";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "PLANTCD";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "LOCCD";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "PRFCNT";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "TRNUM";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ASSETCD";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMTEXT";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "REFNO";
                dtItem.Columns.Add(dtColumn);

                ViewState["ItemData"] = dtItem;

                grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                grvListItem.DataBind();


                //// Tax Details
                DataColumn dtTaxColumn;
                dtTaxColumn = new DataColumn();
                dtTaxColumn.ColumnName = "OPERATOR";
                dtTAX.Columns.Add(dtTaxColumn);

                dtTaxColumn = new DataColumn();
                dtTaxColumn.ColumnName = "CONDORDER";
                dtTAX.Columns.Add(dtTaxColumn);

                dtTaxColumn = new DataColumn();
                dtTaxColumn.ColumnName = "PBNO";
                dtTAX.Columns.Add(dtTaxColumn);

                dtTaxColumn = new DataColumn();
                dtTaxColumn.ColumnName = "PBSRNO";
                dtTAX.Columns.Add(dtTaxColumn);

                dtTaxColumn = new DataColumn();
                dtTaxColumn.ColumnName = "CONDID";
                dtTAX.Columns.Add(dtTaxColumn);

                dtTaxColumn = new DataColumn();
                dtTaxColumn.ColumnName = "CONDTYPE";
                dtTAX.Columns.Add(dtTaxColumn);

                dtTaxColumn = new DataColumn();
                dtTaxColumn.ColumnName = "GLCODE";
                dtTAX.Columns.Add(dtTaxColumn);

                dtTaxColumn = new DataColumn();
                dtTaxColumn.ColumnName = "RATE";
                dtTAX.Columns.Add(dtTaxColumn);

                dtTaxColumn = new DataColumn();
                dtTaxColumn.ColumnName = "BASEAMT";
                dtTAX.Columns.Add(dtTaxColumn);

                dtTaxColumn = new DataColumn();
                dtTaxColumn.ColumnName = "PID";
                dtTAX.Columns.Add(dtTaxColumn);

                dtTaxColumn = new DataColumn();
                dtTaxColumn.ColumnName = "TAXAMT";
                dtTAX.Columns.Add(dtTaxColumn);

                ViewState["TaxData"] = dtTAX;

                grvTaxation.DataSource = (DataTable)ViewState["TaxData"];
                grvTaxation.DataBind();

                /// Other Charges 

                DataColumn dtChargesColumn;
                dtChargesColumn = new DataColumn();
                dtChargesColumn.ColumnName = "CHRGSRNO";
                dtCharges.Columns.Add(dtChargesColumn);

                dtChargesColumn = new DataColumn();
                dtChargesColumn.ColumnName = "CHRGTYPE";
                dtCharges.Columns.Add(dtChargesColumn);

                dtChargesColumn = new DataColumn();
                dtChargesColumn.ColumnName = "CHRGAMOUNT";
                dtCharges.Columns.Add(dtChargesColumn);

                ViewState["ChargesData"] = dtCharges;

                grvCharges.DataSource = (DataTable)ViewState["ChargesData"];
                grvCharges.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        private string validatePurchaseBillData()
        {
            string errormsg = "";
            try
            {
                if (txtVendor.Text.Length == 0)
                {
                    errormsg += errormsg.Length > 0 ? "\n Please Enter Vendor No." : "Please Enter Vendor No.";
                    rfvVendorCode.Visible = true;
                    ScriptManager.GetCurrent(this.Page).SetFocus(this.txtVendor);
                }
                else
                {
                    if (txtVendorName.Text.Length == 0)
                    {
                        errormsg += errormsg.Length > 0 ? "\n Invalid Vendor Code. Please Enter Correct Vendor Code." : "Invalid Vendor Code. Please Enter Correct Vendor Code.";
                        rfvVendorCode.Visible = true;
                    }
                }

                if (txtBillNo.Text.Length == 0)
                {
                    errormsg += errormsg.Length > 0 ? "\n Please Enter Bill No." : "Please Enter Bill No.";
                    rfvBillNo.Visible = true;
                }
                else
                {
                    DataTable dt = objMainClass.CheckPurchaseBillPoGrNExist(txtPoNo.Text, txtVendor.Text, txtGRNNo.Text, txtGRNSrNo.Text, txtBillNo.Text, "CHECKBILLEXIST");
                    if (dt.Rows.Count > 0)
                    {
                        errormsg += errormsg.Length > 0 ? "\n Purchase Bill already made for this Bill No." : "Purchase Bill already made for this Bill No.";
                        rfvBillNo.Visible = true;
                    }
                }

                if (txtBillDate.Text.Length == 0)
                {
                    errormsg += errormsg.Length > 0 ? "\n Please Enter Bill Date." : "Please Enter Bill Date.";
                    rfvBillDate.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return errormsg;
        }
        #endregion

        #region ADDUPDATEDELETETAX
        protected void ddlPaymentTerms_SelectedIndexChanged(object sender, EventArgs e)
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
                    txtREMARKS.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }

        }

        protected void ddlConditionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TaxCalculate();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public void TaxCalculate()
        {
            try
            {
                if (ddlConditionType.SelectedIndex == 0)
                {
                    txtTaxTAmount.Text = string.Empty;
                    txtGLCdTax.Text = string.Empty;
                    ddlOperator.SelectedIndex = 0;
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetTaxCalData(ddlConditionType.SelectedValue);
                    Decimal itembrate = 0;
                    Decimal.TryParse(txtItemBRate.Text, out itembrate);
                    if (itembrate > 0)
                    {
                        txtRate.Text = Convert.ToString(Convert.ToDecimal(txtItemBRate.Text) + (Convert.ToDecimal(Convert.ToDecimal(txtItemBRate.Text) * Convert.ToDecimal(dt.Rows[0]["rate"])) / 100));
                        txtTaxTAmount.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(txtAmount.Text) * Convert.ToDecimal(dt.Rows[0]["rate"])) / 100);
                        txtTaxTAmount.Text = Math.Round(Convert.ToDecimal(txtTaxTAmount.Text), 2).ToString();
                        hfCONDID.Value = Convert.ToString(dt.Rows[0]["id"]);
                        hfPID.Value = Convert.ToString(dt.Rows[0]["pid"]);
                        hfRate.Value = Convert.ToString(dt.Rows[0]["rate"]);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void grvTaxation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "eDelete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                DataTable dt = (DataTable)ViewState["TaxData"];
                GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                dt.Rows[row.RowIndex].Delete();
                dt.AcceptChanges();
                ViewState["TaxData"] = dt;
                grvTaxation.DataSource = (DataTable)ViewState["TaxData"];
                grvTaxation.DataBind();
                callAMT();
            }
        }
        #endregion

        #region ADDUPDATEDELETEMATERIAL
        protected void txtPoNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPoNo.Text.Length >= 1)
                {
                    DataTable dt = objMainClass.CheckPurchaseBillPoGrNExist(txtPoNo.Text, txtVendor.Text, txtGRNNo.Text, txtGRNSrNo.Text, "", "CHECKPONOEXIST");
                    if (dt.Rows[0]["AVAILABLE"].ToString().ToUpper() == "YES")
                    {
                        rfvPoNo.Visible = false;
                        ScriptManager.GetCurrent(this.Page).SetFocus(this.txtGRNNo);
                    }
                    else
                    {
                        rfvPoNo.Visible = true;
                        ScriptManager.GetCurrent(this.Page).SetFocus(this.txtPoNo);
                    }
                }
                else
                {
                    rfvPoNo.Visible = true;
                    ScriptManager.GetCurrent(this.Page).SetFocus(this.txtPoNo);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void txtGRNNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtGRNNo.Text.Length >= 1)
                {
                    rfvGRNNo.Visible = false;
                    DataTable dt = objMainClass.CheckPurchaseBillPoGrNExist(txtPoNo.Text, txtVendor.Text, txtGRNNo.Text, txtGRNSrNo.Text, "", "CHECKDOCNOEXIST");
                    if (dt.Rows[0]["AVAILABLE"].ToString().ToUpper() == "YES")
                    {
                        rfvGRNNo.Visible = false;
                        ScriptManager.GetCurrent(this.Page).SetFocus(this.txtGRNSrNo);
                    }
                    else
                    {
                        rfvGRNNo.Visible = true;
                        txtGRNNo.Focus();
                        ScriptManager.GetCurrent(this.Page).SetFocus(this.txtGRNNo);
                    }
                }
                else
                {
                    rfvGRNNo.Visible = true;
                    ScriptManager.GetCurrent(this.Page).SetFocus(this.txtGRNNo);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void txtGRNSrNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtGRNSrNo.Text.Length >= 1)
                {
                    rfvGRNSrNo.Visible = false;
                    DataTable dt = objMainClass.CheckPurchaseBillPoGrNExist(txtPoNo.Text, txtVendor.Text, txtGRNNo.Text, txtGRNSrNo.Text, "", "INWARDITEMDETAIL");
                    if (dt.Rows.Count > 0)
                    {
                        LoadEachSrNoItemDetail(dt);
                        LoadItemTaxDetail("GET");
                        ScriptManager.GetCurrent(this.Page).SetFocus(this.txtItemBRate);
                    }
                    else
                    {
                        LoadItemTaxDetail("RESET");
                        rfvGRNSrNo.Visible = true;
                        ScriptManager.GetCurrent(this.Page).SetFocus(this.txtGRNSrNo);
                    }
                }
                else
                {
                    rfvGRNSrNo.Visible = true;
                    ScriptManager.GetCurrent(this.Page).SetFocus(this.txtGRNSrNo);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void txtItemBRate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtAmount.Text = Convert.ToString(Convert.ToDecimal(txtItemBRate.Text == "" ? "0" : txtItemBRate.Text) * Convert.ToDecimal(txtItemQty.Text == "" ? "0" : txtItemQty.Text));
                ddlConditionType_SelectedIndexChanged(sender, e);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtAmount.Text = Convert.ToString(Convert.ToDecimal(txtItemBRate.Text == "" ? "0" : txtItemBRate.Text) * Convert.ToDecimal(txtItemQty.Text == "" ? "0" : txtItemQty.Text));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void ddlUOM_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (ddlUOM.SelectedItem.Value.ToString() != txtSku.Text)
            //    {
            //        ddlUOM.SelectedIndex = 0;
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"UOM is not matched with Base Unit of Item!\");", true);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            //}
        }

        protected void ddlPLant_SelectedIndexChanged(object sender, EventArgs e)
        {
            objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);
            try
            {
                if (Session["USERID"] != null)
                {
                    string PLantRights = string.Empty;
                    string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                    for (int i = 0; i < plantArray.Count(); i++)
                    {
                        if (plantArray[i] == ddlPLant.SelectedValue)
                        {
                            PLantRights = ddlPLant.SelectedValue;
                        }
                    }

                    if (PLantRights.Length > 0)
                    {
                        DataTable dt = objMainClass.GetPRFCost(ddlPLant.SelectedValue, Convert.ToString(Session["USERID"]));
                        if (dt.Rows.Count > 0)
                        {
                            txtProfitCenter.Text = Convert.ToString(dt.Rows[0]["PRFCNT"]);
                            //txtCostCenter.Text = Convert.ToString(dt.Rows[0]["CSTCENTCD"]);
                        }
                        else
                        {
                            txtProfitCenter.Text = "1000";
                            //txtCostCenter.Text = "1000";
                        }
                    }
                    else
                    {
                        ddlPLant.SelectedValue = plantArray[0];
                        ddlPLant.Focus();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have plant rights.');$('#txtJobId').focus();", true);
                        ddlPLant.SelectedValue = plantArray[0];
                        ddlPLant.Focus();
                        objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);
                        ddlLocation.SelectedIndex = 1;
                    }

                    objBindDDL.FillCostCenter(ddlCostCenter, ddlPLant.SelectedValue, ddlLocation.SelectedValue);

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public void LoadEachSrNoItemDetail(DataTable dt)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    txtPoSrNo.Text = dt.Rows[0]["POSRNO"].ToString();
                    txtItemId.Text = dt.Rows[0]["ITEMID"].ToString();
                    txtItemCode.Text = dt.Rows[0]["ITEMCODE"].ToString();
                    txtItemDesc.Text = dt.Rows[0]["ITEMDESC"].ToString();
                    txtItemQty.Text = dt.Rows[0]["QTY"].ToString();
                    ddlUOM.SelectedValue = dt.Rows[0]["UOM"].ToString();
                    txtItemBRate.Text = dt.Rows[0]["BRATE"].ToString();
                    txtAmount.Text = dt.Rows[0]["CAMOUNT"].ToString();
                    txtDiscount.Text = dt.Rows[0]["DISCAMT"].ToString();
                    ddlPLant.SelectedValue = dt.Rows[0]["PLANTCD"].ToString();
                    ddlLocation.SelectedValue = dt.Rows[0]["LOCCD"].ToString();

                    // Initialize Hidden Field 
                    txtItemGroupId.Text = dt.Rows[0]["ITEMGRPID"].ToString();
                    txtItemGroup.Text = dt.Rows[0]["ITEMGRP"].ToString();
                    txtGLCode.Text = dt.Rows[0]["GLCD"].ToString();
                    txtProfitCenter.Text = dt.Rows[0]["PRFCNT"].ToString();
                    //txtCostCenter.Text = dt.Rows[0]["CSTCENTCD"].ToString();
                    ddlCostCenter.SelectedValue = dt.Rows[0]["CSTCENTCD"].ToString();
                    txtRate.Text = dt.Rows[0]["RATE"].ToString();
                    txtItemText.Text = dt.Rows[0]["ITEMTEXT"].ToString();
                    txtReceivedQty.Text = dt.Rows[0]["PNDQTY"].ToString();
                    // Initialize Hidden Field 
                }
                else
                {
                    ResetLoadedItemDetail();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public void LoadItemTaxDetail(string mode)
        {
            try
            {
                if (mode == "RESET")
                {
                    hfCONDID.Value = string.Empty;
                    txtGLCdTax.Text = string.Empty;
                    hfPID.Value = string.Empty;
                    hfRate.Value = string.Empty;
                    ddlOperator.SelectedIndex = 0;
                    ddlConditionType.SelectedIndex = 0;
                }
                else
                {
                    DataTable dt = objMainClass.CheckPurchaseBillPoGrNExist(txtPoNo.Text, txtVendor.Text, txtGRNNo.Text, txtGRNSrNo.Text, "", "ITEMTAXDETAIL");
                    if (dt.Rows.Count > 0)
                    {
                        hfCONDID.Value = Convert.ToString(dt.Rows[0]["CONDID"]);
                        txtGLCdTax.Text = Convert.ToString(dt.Rows[0]["GLCODE"]);
                        hfPID.Value = Convert.ToString(dt.Rows[0]["PID"]);
                        hfRate.Value = Convert.ToString(dt.Rows[0]["RATE"]);
                        ddlOperator.SelectedValue = Convert.ToString(dt.Rows[0]["OPERATOR"]);
                        ddlConditionType.SelectedValue = Convert.ToString(dt.Rows[0]["CONDTYPE"]);
                        TaxCalculate();
                    }
                    else
                    {
                        hfCONDID.Value = string.Empty;
                        txtGLCdTax.Text = string.Empty;
                        hfPID.Value = string.Empty;
                        hfRate.Value = string.Empty;
                        ddlOperator.SelectedIndex = 0;
                        ddlConditionType.SelectedIndex = 0;
                        txtTaxTAmount.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public void ResetLoadedItemDetail()
        {
            try
            {
                txtPoSrNo.Text = string.Empty;
                txtItemId.Text = string.Empty;
                txtItemCode.Text = string.Empty;
                txtItemDesc.Text = string.Empty;
                txtItemQty.Text = string.Empty;
                ddlUOM.SelectedIndex = -1;
                txtItemBRate.Text = string.Empty;
                txtAmount.Text = string.Empty;
                txtDiscount.Text = string.Empty;
                ddlPLant.SelectedIndex = -1;
                ddlLocation.SelectedIndex = -1;

                // Reset Hidden Field 
                txtItemGroupId.Text = string.Empty;
                txtItemGroup.Text = string.Empty;
                txtGLCode.Text = string.Empty;
                txtProfitCenter.Text = string.Empty;
                //txtCostCenter.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtItemText.Text = string.Empty;
                txtReceivedQty.Text = string.Empty;
                // Reset Hidden Field 
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnSaveDet_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    string validation = validateItemData();
                    if (validation.Length == 0)
                    {
                        if (Convert.ToString(Session["savedet"]) == "Save Item")
                        {
                            decimal pndqty;
                            decimal.TryParse(txtReceivedQty.Text, out pndqty);
                            if (pndqty > 0)
                            {
                                DataTable dt = (DataTable)ViewState["ItemData"];
                                DataTable dtTaxation = (DataTable)ViewState["TaxData"];
                                if (grvListItem.Rows.Count > 0)
                                {
                                    DataRow lastRow = dt.Rows[dt.Rows.Count - 1];
                                    int id = Convert.ToInt32(lastRow["SRNO"]) + 1;

                                    dt.Rows.Add(objMainClass.strConvertZeroPadding(txtPoNo.Text), txtPoSrNo.Text, objMainClass.strConvertZeroPadding(txtGRNNo.Text),
                                        txtGRNSrNo.Text, id, txtItemId.Text, txtItemCode.Text, txtItemDesc.Text, txtItemGroupId.Text,
                                        txtItemGroup.Text, ddlUOM.SelectedValue, ddlUOM.SelectedItem.Text, txtItemQty.Text,
                                        txtItemBRate.Text, txtRate.Text, txtAmount.Text, txtDiscount.Text, txtGLCode.Text,
                                        ddlCostCenter.SelectedValue, ddlPLant.SelectedValue, ddlLocation.SelectedValue, txtProfitCenter.Text,
                                        txtTrackNo.Text, txtAssetCode.Text, txtItemRemark.Text, txtRefNo.Text);

                                    ViewState["ItemData"] = dt;
                                    if (ddlConditionType.SelectedIndex > 0)
                                    {
                                        int idTax;
                                        if (grvTaxation.Rows.Count > 0)
                                        {
                                            DataRow lastRowTax = dtTaxation.Rows[dtTaxation.Rows.Count - 1];
                                            idTax = Convert.ToInt32(lastRowTax["CONDORDER"]) + 1;
                                        }
                                        else
                                        {
                                            idTax = 1;
                                        }
                                        dtTaxation.Rows.Add(ddlOperator.SelectedIndex > 0 ? ddlOperator.SelectedValue : "+",
                                            idTax, objMainClass.strConvertZeroPadding(txtPBNO.Text), id, hfCONDID.Value,
                                            ddlConditionType.SelectedValue, txtGLCdTax.Text, hfRate.Value,
                                            txtAmount.Text, hfPID.Value, txtTaxTAmount.Text);
                                        ViewState["TaxData"] = dtTaxation;
                                    }
                                }
                                else
                                {
                                    dt.Rows.Add(objMainClass.strConvertZeroPadding(txtPoNo.Text), txtPoSrNo.Text, objMainClass.strConvertZeroPadding(txtGRNNo.Text),
                                    txtGRNSrNo.Text, "1", txtItemId.Text, txtItemCode.Text, txtItemDesc.Text, txtItemGroupId.Text,
                                    txtItemGroup.Text, ddlUOM.SelectedValue, ddlUOM.SelectedItem.Text, txtItemQty.Text,
                                    txtItemBRate.Text, txtRate.Text, txtAmount.Text, txtDiscount.Text, txtGLCode.Text,
                                    ddlCostCenter.SelectedValue, ddlPLant.SelectedValue, ddlLocation.SelectedValue, txtProfitCenter.Text,
                                    txtTrackNo.Text, txtAssetCode.Text, txtItemRemark.Text, txtRefNo.Text);

                                    ViewState["ItemData"] = dt;
                                    if (ddlConditionType.SelectedIndex > 0)
                                    {
                                        dtTaxation.Rows.Add(ddlOperator.SelectedIndex > 0 ? ddlOperator.SelectedValue : "+",
                                        "1", objMainClass.strConvertZeroPadding(txtPBNO.Text), "1", hfCONDID.Value,
                                        ddlConditionType.SelectedValue, txtGLCdTax.Text, hfRate.Value,
                                        txtAmount.Text, hfPID.Value, txtTaxTAmount.Text);
                                        ViewState["TaxData"] = dtTaxation;
                                    }
                                }
                                grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                                grvListItem.DataBind();

                                grvTaxation.DataSource = (DataTable)ViewState["TaxData"];
                                grvTaxation.DataBind();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Full Quantity booked for these GRN!');", true);
                            }
                        }

                        else if (Convert.ToString(Session["savedet"]) == "Update Item")
                        {
                            DataTable ddt = (DataTable)ViewState["ItemData"];
                            DataRow dr = ddt.Select("SRNO    = '" + txtSRNo.Text + "'")[0];
                            dr[0] = objMainClass.strConvertZeroPadding(txtPoNo.Text);
                            dr[1] = txtPoSrNo.Text;
                            dr[2] = objMainClass.strConvertZeroPadding(txtGRNNo.Text);
                            dr[3] = txtGRNSrNo.Text;
                            dr[4] = txtSRNo.Text;
                            dr[5] = txtItemId.Text;
                            dr[6] = txtItemCode.Text;
                            dr[7] = txtItemDesc.Text;
                            dr[8] = txtItemGroupId.Text;
                            dr[9] = txtItemGroup.Text;
                            dr[10] = ddlUOM.SelectedValue;
                            dr[11] = ddlUOM.SelectedItem.Text;
                            dr[12] = txtItemQty.Text;
                            dr[13] = txtItemBRate.Text;
                            dr[14] = txtRate.Text;
                            dr[15] = txtAmount.Text;
                            dr[16] = txtDiscount.Text;
                            dr[17] = txtGLCode.Text;
                            dr[18] = ddlCostCenter.SelectedValue;//txtCostCenter.Text;
                            //dr[18] = ddlPLant.SelectedItem.Text;
                            dr[19] = ddlPLant.SelectedValue;
                            //dr[20] = ddlLocation.SelectedItem.Text;
                            dr[20] = ddlLocation.SelectedValue;
                            dr[21] = txtProfitCenter.Text;
                            dr[22] = txtTrackNo.Text;
                            dr[23] = txtAssetCode.Text;
                            dr[24] = txtItemRemark.Text;
                            dr[25] = txtRefNo.Text;

                            if (ddlConditionType.SelectedIndex > 0)
                            {
                                DataTable dtTax = (DataTable)ViewState["TaxData"];
                                if (dtTax.Rows.Count > 0)
                                {
                                    bool Isrecordavailable = IsRecordAvailableAtDataTable(Convert.ToInt32(txtSRNo.Text));
                                    if (Isrecordavailable)
                                    {
                                        DataRow drt = dtTax.Select("PBSRNO='" + txtSRNo.Text + "'")[0];
                                        if (drt != null)
                                        {
                                            drt[0] = ddlOperator.SelectedIndex > 0 ? ddlOperator.SelectedValue : "+";
                                            drt[1] = hdTaxSrNo.Value;
                                            drt[2] = objMainClass.strConvertZeroPadding(txtPBNO.Text);
                                            drt[3] = txtSRNo.Text;
                                            drt[4] = hfCONDID.Value;
                                            drt[5] = ddlConditionType.SelectedValue;
                                            drt[6] = txtGLCdTax.Text;
                                            drt[7] = hfRate.Value;
                                            drt[8] = txtAmount.Text;
                                            drt[9] = hfPID.Value;
                                            drt[10] = txtTaxTAmount.Text;
                                        }
                                    }

                                    else
                                    {
                                        int idTax;
                                        DataTable dtTaxation = (DataTable)ViewState["TaxData"];
                                        DataRow lastRowTax = dtTaxation.Rows[dtTaxation.Rows.Count - 1];
                                        idTax = Convert.ToInt32(lastRowTax["CONDORDER"]) + 1;
                                        dtTaxation.Rows.Add(ddlOperator.SelectedIndex > 0 ? ddlOperator.SelectedValue : "+",
                                        idTax, objMainClass.strConvertZeroPadding(txtPBNO.Text), txtSRNo.Text, hfCONDID.Value,
                                        ddlConditionType.SelectedValue, txtGLCdTax.Text, hfRate.Value,
                                        txtAmount.Text, hfPID.Value, txtTaxTAmount.Text);
                                        ViewState["TaxData"] = dtTaxation;
                                    }
                                }
                                else
                                {
                                    DataTable dtTaxation = (DataTable)ViewState["TaxData"];

                                    dtTaxation.Rows.Add(ddlOperator.SelectedIndex > 0 ? ddlOperator.SelectedValue : "+",
                                    "1", objMainClass.strConvertZeroPadding(txtPBNO.Text), txtSRNo.Text, hfCONDID.Value,
                                    ddlConditionType.SelectedValue, txtGLCdTax.Text, hfRate.Value,
                                    txtAmount.Text, hfPID.Value, txtTaxTAmount.Text);
                                    ViewState["TaxData"] = dtTaxation;
                                }
                            }

                            grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                            grvListItem.DataBind();

                            grvTaxation.DataSource = (DataTable)ViewState["TaxData"];
                            grvTaxation.DataBind();

                            Session["savedet"] = "Save Item";
                        }
                        callAMT();
                        EmptyString();
                        EmptyTaxString();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Record not inserted sucessfully should check Vendor Code Po No GRN No GRN Sr No is valid or Not!');", true);
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

        private string validateItemData()
        {
            string errormsg = "";
            try
            {
                string PLantRights = string.Empty;
                string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                for (int i = 0; i < plantArray.Count(); i++)
                {
                    if (plantArray[i] == ddlPLant.SelectedValue)
                    {
                        PLantRights = ddlPLant.SelectedValue;
                    }
                }

                if (PLantRights.Length == 0)
                {
                    errormsg = "You do not have plant rights.";
                }
                if (txtVendor.Text.Length == 0)
                {
                    errormsg += errormsg.Length > 0 ? "\n Please Enter Vendor No" : "Please Enter Vendor No";
                    ScriptManager.GetCurrent(this.Page).SetFocus(this.txtVendor);
                }
                if (txtPoNo.Text.Length == 0)
                {
                    errormsg += errormsg.Length > 0 ? "\nPlease Enter Po No" : "Please Enter Po No";
                    rfvPoNo.Visible = true;
                    ScriptManager.GetCurrent(this.Page).SetFocus(this.txtPoNo);
                }
                else
                {
                    rfvPoNo.Visible = false;
                    DataTable dt = objMainClass.CheckPurchaseBillPoGrNExist(txtPoNo.Text, txtVendor.Text, txtGRNNo.Text, txtGRNSrNo.Text, "", "CHECKPONOEXIST");
                    if (dt.Rows[0]["AVAILABLE"].ToString().ToUpper() == "YES")
                    {
                        rfvPoNo.Visible = false;
                    }
                    else
                    {
                        rfvPoNo.Visible = true;
                        errormsg += errormsg.Length > 0 ? "\nPlease Enter valid Po No" : "Please Enter valid Po No";
                        ScriptManager.GetCurrent(this.Page).SetFocus(this.txtPoNo);
                    }
                }

                if (txtGRNNo.Text.Length == 0)
                {
                    errormsg += errormsg.Length > 0 ? "\nPlease Enter GRN No" : "Please Enter GRN No";
                    rfvGRNNo.Visible = true;
                }
                else
                {
                    rfvGRNNo.Visible = false;
                    DataTable dt = objMainClass.CheckPurchaseBillPoGrNExist(txtPoNo.Text, txtVendor.Text, txtGRNNo.Text, txtGRNSrNo.Text, "", "CHECKDOCNOEXIST");
                    if (dt.Rows[0]["AVAILABLE"].ToString().ToUpper() == "YES")
                    {
                        rfvGRNNo.Visible = false;
                    }
                    else
                    {
                        rfvGRNNo.Visible = true;
                        errormsg += errormsg.Length > 0 ? "\nPlease Enter valid GRN No" : "Please Enter valid GRN No";
                        ScriptManager.GetCurrent(this.Page).SetFocus(this.txtGRNNo);
                    }
                }

                if (txtGRNSrNo.Text.Length == 0)
                {
                    errormsg += errormsg.Length > 0 ? "\nPlease Enter GRN Sr No" : "Please Enter GRN Sr No";
                    rfvGRNSrNo.Visible = true;
                }
                else
                {
                    rfvGRNSrNo.Visible = false;
                    DataTable dt = objMainClass.CheckPurchaseBillPoGrNExist(txtPoNo.Text, txtVendor.Text, txtGRNNo.Text, txtGRNSrNo.Text, "", "INWARDITEMDETAIL");
                    if (dt.Rows.Count > 0)
                    {
                        ScriptManager.GetCurrent(this.Page).SetFocus(this.txtItemBRate);
                    }
                    else
                    {
                        errormsg += errormsg.Length > 0 ? "\nPlease Enter valid GRN Sr No" : "Please Enter valid GRN Sr No";
                        rfvGRNSrNo.Visible = true;
                        ScriptManager.GetCurrent(this.Page).SetFocus(this.txtGRNSrNo);
                    }
                }

                Decimal rate;
                Decimal.TryParse(txtItemBRate.Text, out rate);
                if (rate == 0)
                {
                    errormsg += errormsg.Length > 0 ? "\nPlease Enter the valid Rate" : "Please Enter the valid Rate";
                    rfvItemRate.Visible = true;
                }

                if (errormsg.Length == 0)
                {
                    if (Convert.ToString(Session["savedet"]) != "Update Item")
                    {
                        if (CheckDuplicate())
                        {
                            errormsg += errormsg.Length > 0 ? "\nSr No Item is already Exist in Grid" : "Sr No Item is already Exist in Grid";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return errormsg;
        }

        public bool CheckDuplicate()
        {
            bool Isduplicate = false;
            try
            {
                for (int i = 0; i < grvListItem.Rows.Count; i++)
                {
                    GridViewRow row = grvListItem.Rows[i];
                    string grvPoNo, grvGRNNo, grvGRNSRNo;
                    string enterPoNo, enterGRNNo, enterGRNSRNo;

                    grvPoNo = objMainClass.strConvertZeroPadding(((Label)row.FindControl("lblGVPoNo")).Text);
                    grvGRNNo = objMainClass.strConvertZeroPadding(((Label)row.FindControl("lblGVGRNNo")).Text);
                    grvGRNSRNo = objMainClass.strConvertZeroPadding(((Label)row.FindControl("lblGVGRNSrNo")).Text);

                    enterPoNo = txtPoNo.Text.Length > 0 ? objMainClass.strConvertZeroPadding(txtPoNo.Text) : "";
                    enterGRNNo = txtGRNNo.Text.Length > 0 ? objMainClass.strConvertZeroPadding(txtGRNNo.Text) : "";
                    enterGRNSRNo = txtGRNSrNo.Text.Length > 0 ? objMainClass.strConvertZeroPadding(txtGRNSrNo.Text) : "";

                    if (grvPoNo == enterPoNo && grvGRNNo == enterGRNNo && grvGRNSRNo == enterGRNSRNo)
                    {
                        Isduplicate = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return Isduplicate;
        }

        private void EmptyString()
        {
            try
            {
                txtSRNo.Text = string.Empty;
                txtPoSrNo.Text = string.Empty;
                txtGRNNo.Text = string.Empty;
                txtGRNSrNo.Text = string.Empty;
                txtTrackNo.Text = string.Empty;
                txtItemRemark.Text = string.Empty;
                txtRefNo.Text = string.Empty;
                ResetLoadedItemDetail();
                txtGRNNo.Focus();
                ScriptManager.GetCurrent(this.Page).SetFocus(this.txtGRNNo);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void grvListItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "eDelete")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    DataTable dt = (DataTable)ViewState["ItemData"];
                    GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    dt.Rows[row.RowIndex].Delete();
                    ViewState["ItemData"] = dt;
                    grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                    grvListItem.DataBind();

                    DataTable dtTax = (DataTable)ViewState["TaxData"];
                    dtTax.Select("PBSRNO='" + index + "'").ToList().ForEach(x => x.Delete());
                    dtTax.AcceptChanges();
                    ViewState["TaxData"] = dtTax;
                    grvTaxation.DataSource = (DataTable)ViewState["TaxData"];
                    grvTaxation.DataBind();
                    callAMT();
                }

                if (e.CommandName == "eEdit")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    txtSRNo.Text = Convert.ToString(index);
                    GridViewRow gRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;

                    Label lblGVPoNo = (Label)gRow.FindControl("lblGVPoNo");
                    Label lblGVPoSrNo = (Label)gRow.FindControl("lblGVPoSrNo");
                    Label lblGVGRNNo = (Label)gRow.FindControl("lblGVGRNNo");
                    Label lblGVGRNSrNo = (Label)gRow.FindControl("lblGVGRNSrNo");
                    Label lblGVSrNo = (Label)gRow.FindControl("lblGVSrNo");
                    Label lblGVItemId = (Label)gRow.FindControl("lblGVItemId");
                    Label lblGVItemCode = (Label)gRow.FindControl("lblGVItemCode");
                    Label lblGVItemDesc = (Label)gRow.FindControl("lblGVItemDesc");
                    Label lblGVGroupId = (Label)gRow.FindControl("lblGVGroupId");
                    Label lblGVItemGroup = (Label)gRow.FindControl("lblGVItemGroup");
                    Label lblGVUOMID = (Label)gRow.FindControl("lblGVUOMID");
                    Label lblGVUOM = (Label)gRow.FindControl("lblGVUOM");
                    Label lblGVQty = (Label)gRow.FindControl("lblGVQty");
                    Label lblGVBaseRate = (Label)gRow.FindControl("lblGVBaseRate");
                    Label lblGVRate = (Label)gRow.FindControl("lblGVRate");
                    Label lblGVAmount = (Label)gRow.FindControl("lblGVAmount");
                    Label lblGVDiscount = (Label)gRow.FindControl("lblGVDiscount");
                    Label lblGVGLCode = (Label)gRow.FindControl("lblGVGLCode");
                    Label lblGVCostCenter = (Label)gRow.FindControl("lblGVCostCenter");
                    Label lblGVPlantCD = (Label)gRow.FindControl("lblGVPlantCD");
                    Label lblGVLocationCD = (Label)gRow.FindControl("lblGVLocationCD");
                    Label lblGVProfitCenter = (Label)gRow.FindControl("lblGVProfitCenter");
                    Label lblGVTrackNo = (Label)gRow.FindControl("lblGVTrackNo");
                    Label lblGVAssetCode = (Label)gRow.FindControl("lblGVAssetCode");
                    Label lblGVItemText = (Label)gRow.FindControl("lblGVItemText");
                    Label lblGVRefNo = (Label)gRow.FindControl("lblGVRefNo");

                    txtSRNo.Text = lblGVGRNSrNo.Text;
                    txtPoNo.Text = lblGVPoNo.Text;
                    txtGRNNo.Text = lblGVGRNNo.Text;
                    txtGRNSrNo.Text = lblGVGRNSrNo.Text;
                    txtItemCode.Text = lblGVItemCode.Text;
                    txtItemDesc.Text = lblGVItemDesc.Text;
                    txtItemId.Text = lblGVItemId.Text;
                    txtItemGroupId.Text = lblGVGroupId.Text;
                    txtItemGroup.Text = lblGVItemGroup.Text;
                    txtGLCode.Text = lblGVGLCode.Text;
                    txtAssetCode.Text = lblGVAssetCode.Text;
                    txtProfitCenter.Text = lblGVProfitCenter.Text;
                    txtTrackNo.Text = lblGVTrackNo.Text;
                    txtItemQty.Text = lblGVQty.Text;
                    ddlUOM.SelectedValue = lblGVUOMID.Text;
                    txtItemBRate.Text = lblGVBaseRate.Text;
                    txtAmount.Text = lblGVAmount.Text;
                    txtDiscount.Text = lblGVDiscount.Text;
                    txtRefNo.Text = lblGVRefNo.Text;
                    ddlPLant.SelectedValue = lblGVPlantCD.Text;
                    ddlLocation.SelectedValue = lblGVLocationCD.Text;
                    txtItemRemark.Text = lblGVItemText.Text;
                    //txtCostCenter.Text = lblGVCostCenter.Text;
                    ddlCostCenter.SelectedValue = lblGVCostCenter.Text;

                    DataTable dtTax = (DataTable)ViewState["TaxData"];
                    if (dtTax.Rows.Count > 0)
                    {
                        bool IsRecordavailable = IsRecordAvailableAtDataTable(index);
                        if (IsRecordavailable)
                        {
                            DataRow dr = dtTax.Select("PBSRNO='" + index + "'")[0];
                            if (dr != null)
                            {
                                ddlOperator.SelectedValue = Convert.ToString(dr[0]);
                                ddlConditionType.SelectedValue = Convert.ToString(dr[5]);
                                txtTaxTAmount.Text = Convert.ToString(dr[10]);
                                hfRate.Value = Convert.ToString(dr[7]);
                                hfPID.Value = Convert.ToString(dr[9]);
                                hfCONDID.Value = Convert.ToString(dr[4]);
                                txtGLCdTax.Text = Convert.ToString(dr[6]);
                                hdTaxSrNo.Value = Convert.ToString(dr[1]);
                            }
                        }
                    }
                    Session["savedet"] = "Update Item";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public void EmptyTaxString()
        {
            try
            {
                hdTaxSrNo.Value = string.Empty;
                hfCONDID.Value = string.Empty;
                hfPID.Value = string.Empty;
                hfRate.Value = string.Empty;
                txtGLCdTax.Text = string.Empty;
                txtTaxTAmount.Text = string.Empty;
                ddlConditionType.SelectedIndex = -1;
                ddlOperator.SelectedIndex = -1;
                ddlOperator.Focus();
                ScriptManager.GetCurrent(this.Page).SetFocus(this.ddlOperator);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public bool IsRecordAvailableAtDataTable(int index)
        {
            bool IsrecordAvailable = false;
            try
            {
                DataTable dtTax = (DataTable)ViewState["TaxData"];
                foreach (DataRow dr in dtTax.Rows)
                {
                    if (dr["PBSRNO"].ToString() == index.ToString())
                    {
                        IsrecordAvailable = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return IsrecordAvailable;
        }
        #endregion

        #region ADDUPDATEDELETECHARGES
        protected void lnkSaveCharges_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (Convert.ToString(Session["saveCharge"]) == "Save Charge")
                    {
                        DataTable dt = (DataTable)ViewState["ChargesData"];
                        int id = Convert.ToInt32(txtSrNoChg.Text);
                        bool isavailablerecord = IsChargesRecordAvailableAtDataTable(id);
                        if (isavailablerecord)
                        {
                            DataRow dr = dt.Select("CHRGSRNO = '" + txtSrNoChg.Text + "'")[0];
                            dr[1] = ddlCharges.SelectedItem.Text;
                            dr[2] = txtChgAmt.Text;
                            grvCharges.DataSource = (DataTable)ViewState["ChargesData"];
                            grvCharges.DataBind();
                            Session["saveCharge"] = "Save Charge";
                        }
                        else
                        {
                            dt.Rows.Add(id, ddlCharges.SelectedItem.Text, txtChgAmt.Text);
                            ViewState["ChargesData"] = dt;
                            grvCharges.DataSource = (DataTable)ViewState["ChargesData"];
                            grvCharges.DataBind();
                        }
                    }
                    else if (Convert.ToString(Session["saveCharge"]) == "Update Charge")
                    {
                        DataTable dt = (DataTable)ViewState["ChargesData"];
                        DataRow dr = dt.Select("CHRGSRNO = '" + txtSrNoChg.Text + "'")[0];

                        dr[1] = ddlCharges.SelectedItem.Text;
                        dr[2] = txtChgAmt.Text;

                        grvCharges.DataSource = (DataTable)ViewState["ChargesData"];
                        grvCharges.DataBind();
                        Session["saveCharge"] = "Save Charge";
                    }
                    callAMT();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='Login.aspx' });", true);
                }
                ddlCharges.SelectedIndex = -1;
                txtSrNoChg.Text = string.Empty;
                txtMaxSrNoChg.Text = string.Empty;
                txtChgAmt.Text = string.Empty;
                txtSrNoChg.Enabled = true;
            }
            catch (Exception ex)
            {
                ddlCharges.SelectedIndex = -1;
                txtSrNoChg.Text = string.Empty;
                txtMaxSrNoChg.Text = string.Empty;
                txtChgAmt.Text = string.Empty;
                txtSrNoChg.Enabled = true;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void grvCharges_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "eDelete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                DataTable dt = (DataTable)ViewState["ChargesData"];
                GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                dt.Rows[row.RowIndex].Delete();
                dt.AcceptChanges();
                ViewState["ChargesData"] = dt;
                grvCharges.DataSource = ViewState["ChargesData"];
                grvCharges.DataBind();

                callAMT();
            }

            if (e.CommandName == "eEdit")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                txtSrNoChg.Text = Convert.ToString(index);
                GridViewRow gRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;

                Label lblChrgSrNo = (Label)gRow.FindControl("lblChrgSrNo");
                Label lblChrgCondType = (Label)gRow.FindControl("lblChrgCondType");
                Label lblChrgAmount = (Label)gRow.FindControl("lblChrgAmount");

                txtSrNoChg.Text = lblChrgSrNo.Text;
                ddlCharges.SelectedItem.Text = lblChrgCondType.Text;
                txtChgAmt.Text = lblChrgAmount.Text;
                Session["saveCharge"] = "Update Charge";
                txtSrNoChg.Enabled = false;
            }
        }

        protected void ddlCharges_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public bool IsChargesRecordAvailableAtDataTable(int index)
        {
            bool IsrecordAvailable = false;
            try
            {
                DataTable dtTax = (DataTable)ViewState["ChargesData"];
                foreach (DataRow dr in dtTax.Rows)
                {
                    if (dr["CHRGSRNO"].ToString() == index.ToString())
                    {
                        IsrecordAvailable = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return IsrecordAvailable;
        }
        #endregion
    }
}