using Microsoft.Reporting.WebForms;
using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.UTILITY
{
    public partial class frmViewCromaRateCard : System.Web.UI.Page
    {

        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        WAClass objWAClass = new WAClass();
        DataTable dtItem = new DataTable();
        int rowIndexold = 0;

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
                        objBindDDL.FillBrandCroma(ddlBrand, "GETBRANDS");
                        objBindDDL.FillCategoryCroma(ddlCategory, "GETCATEGORY");
                        objBindDDL.FillSizeCroma(ddlSize, "GETSIZE");
                        objBindDDL.FillGradeCroma(ddlGrade, "GETGRADE");
                        GetData();
                        SetUpGrid();
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
            //if (grvData.Rows.Count > 0)
            //{
            //    grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
            //}
        }


        private void SetUpGrid()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataColumn dtColumn;

                    dtColumn = new DataColumn();
                    dtColumn.ColumnName = "CATEGORY";
                    dtItem.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.ColumnName = "BRAND";
                    dtItem.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.ColumnName = "ITEMCODE";
                    dtItem.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.ColumnName = "IETMDESC";
                    dtItem.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.ColumnName = "JOBID";
                    dtItem.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.ColumnName = "SERIALNO";
                    dtItem.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.ColumnName = "MRP";
                    dtItem.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.ColumnName = "ONLINEPRICE";
                    dtItem.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.ColumnName = "DEALERPRICE";
                    dtItem.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.ColumnName = "PURCHASEPRICE";
                    dtItem.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.ColumnName = "CUSTOMERPRCE";
                    dtItem.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.ColumnName = "DIFFER";
                    dtItem.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.ColumnName = "SALEDONE";
                    dtItem.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.ColumnName = "JOBSTATDESC";
                    dtItem.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.ColumnName = "LOCATION";
                    dtItem.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.ColumnName = "USERNAME";
                    dtItem.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.ColumnName = "CREATEDATE";
                    dtItem.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.ColumnName = "RATE";
                    dtItem.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.ColumnName = "ITEMID";
                    dtItem.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.ColumnName = "ITEMGRP";
                    dtItem.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.ColumnName = "UOM";
                    dtItem.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.ColumnName = "BASEAMOUNT";
                    dtItem.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.ColumnName = "CONDID";
                    dtItem.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.ColumnName = "CONDTYPE";
                    dtItem.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.ColumnName = "SIZE";
                    dtItem.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.ColumnName = "GRADE";
                    dtItem.Columns.Add(dtColumn);

                    ViewState["TempCromaRateCaedSelectedItem"] = dtItem;

                    //grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                    //grvListItem.DataBind();


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
        public void GetData()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dtData = new DataTable();
                    dtData = objMainClass.GetRateCard(objMainClass.intCmpId, txtJobID.Text, txtSerialNo.Text, txtItemCode.Text, ddlBrand.SelectedIndex > 0 ? ddlBrand.SelectedValue : "", ddlCategory.SelectedIndex > 0 ? ddlCategory.SelectedValue : "", chkAll.Checked == true ? 0 : 1,
                        ddlSize.SelectedIndex > 0 ? ddlSize.SelectedValue : "", ddlGrade.SelectedIndex > 0 ? ddlGrade.SelectedValue : "", "GETDATA");
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

        protected void lnkSerch_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GetData();
                    imgSaveAll.Visible = false;

                    lnkCrQuot.Visible = true;
                    lnkCrQuot.Enabled = true;
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

        //protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Session["USERID"] != null)
        //        {
        //            GridViewRow hrow = grvData.HeaderRow;
        //            CheckBox chkSelectAll = (CheckBox)hrow.FindControl("chkSelectAll");
        //            if (chkSelectAll.Checked == true)
        //            {
        //                lblTotalAmt.Text = "0";
        //                lblTotalQty.Text = "0";
        //                for (int i = 0; i < grvData.Rows.Count; i++)
        //                {
        //                    GridViewRow row = grvData.Rows[i];
        //                    CheckBox chkSelect = ((CheckBox)row.FindControl("chkSelect"));
        //                    Label lblCUSTOMERPRCE = ((Label)row.FindControl("lblCUSTOMERPRCE"));
        //                    chkSelect.Checked = true;
        //                    lblTotalQty.Text = Convert.ToString(Convert.ToDecimal(lblTotalQty.Text) + 1);
        //                    lblTotalAmt.Text = Convert.ToString(Convert.ToDecimal(lblTotalAmt.Text) + Convert.ToDecimal(lblCUSTOMERPRCE.Text));
        //                }
        //            }
        //            else
        //            {
        //                for (int i = 0; i < grvData.Rows.Count; i++)
        //                {
        //                    GridViewRow row = grvData.Rows[i];
        //                    CheckBox chkSelect = ((CheckBox)row.FindControl("chkSelect"));
        //                    Label lblCUSTOMERPRCE = ((Label)row.FindControl("lblCUSTOMERPRCE"));
        //                    if (chkSelect.Checked == true)
        //                    {
        //                        chkSelect.Checked = false;
        //                        lblTotalQty.Text = Convert.ToString(Convert.ToDecimal(lblTotalQty.Text) - 1);
        //                        lblTotalAmt.Text = Convert.ToString(Convert.ToDecimal(lblTotalAmt.Text) - Convert.ToDecimal(lblCUSTOMERPRCE.Text));
        //                    }
        //                }
        //            }

        //            if (grvData.Rows.Count > 0)
        //            {
        //                grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
        //            }
        //            ScriptManager.RegisterStartupScript(this, GetType(), "BindMakeAssociateModel", $"BindMakeAssociateModel();", true);
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

        protected void imgSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    //int selectcnt = 0;
                    //for (int i = 0; i < grvData.Rows.Count; i++)
                    //{
                    //    GridViewRow row = grvData.Rows[i];
                    //    CheckBox chkSelect = ((CheckBox)row.FindControl("chkSelect"));
                    //    if (chkSelect.Checked == true)
                    //    {
                    //        selectcnt++;
                    //    }
                    //}

                    if (grvTempData.Rows.Count > 0)
                    {
                        //txtVendorCode.Text = string.Empty;

                        txtVendorName.Text = string.Empty;
                        txtTotalQty.Text = string.Empty;
                        txtTotalAmt.Text = string.Empty;
                        txtDiscountper.Text = string.Empty;
                        txtDiscountRs.Text = string.Empty;

                        txtTotalQty.Text = lblTotalQty.Text;
                        txtTotalAmt.Text = lblTotalAmt.Text;
                        txtFinalAmount.Text = lblTotalAmt.Text;

                        //if (grvData.Rows.Count > 0)
                        //{
                        //    grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
                        //}

                        objBindDDL.FillPlantIsMobex(ddlPlant, 1);
                        ddlPlant.SelectedValue = "1019";
                        ddlPlant.Enabled = false;

                        objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPlant.SelectedValue);
                        ddlLocation.SelectedValue = "WS01";
                        ddlLocation.Enabled = false;

                        objBindDDL.FillPayTerm(ddlPaymentTerms);
                        ddlPaymentTerms.SelectedValue = "ADV";
                        ddlPaymentTerms.Enabled = false;

                        txtDiscountper.Text = "0";
                        txtDiscountRs.Text = "0";

                        objBindDDL.FillCustomer(ddlVendor);
                        ddlVendor.SelectedIndex = 0;

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Select atleast one record to create Quotation.!');", true);
                    }

                    //if (grvData.Rows.Count > 0)
                    //{
                    //    grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //}
                    //ScriptManager.RegisterStartupScript(this, GetType(), "BindMakeAssociateModel", $"BindMakeAssociateModel();", true);
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

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {


                    if (grvData.Rows.Count > 0)
                    {
                        grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                    ScriptManager.RegisterStartupScript(this, GetType(), "BindMakeAssociateModel", $"BindMakeAssociateModel();", true);



                    //GridViewRow grdrow = (GridViewRow)((CheckBox)sender).NamingContainer;
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;


                    int rowIndex1 = grdrow.RowIndex;
                    //CheckBox chkSelect = (CheckBox)grdrow.FindControl("chkSelect");
                    Label lblCATEGORY = ((Label)grdrow.FindControl("lblCATEGORY"));
                    Label lblBRAND = ((Label)grdrow.FindControl("lblBRAND"));
                    Label lblITEMCODE = ((Label)grdrow.FindControl("lblITEMCODE"));
                    Label lblIETMDESC = ((Label)grdrow.FindControl("lblIETMDESC"));
                    Label lblJOBID = ((Label)grdrow.FindControl("lblJOBID"));
                    Label lblSERIALNO = ((Label)grdrow.FindControl("lblSERIALNO"));
                    Label lblMRP = ((Label)grdrow.FindControl("lblMRP"));
                    Label lblONLINEPRICE = ((Label)grdrow.FindControl("lblONLINEPRICE"));
                    Label lblDEALERPRICE = ((Label)grdrow.FindControl("lblDEALERPRICE"));
                    Label lblPURCHASEPRICE = ((Label)grdrow.FindControl("lblPURCHASEPRICE"));
                    Label lblCUSTOMERPRCE = ((Label)grdrow.FindControl("lblCUSTOMERPRCE"));
                    Label lblDIFFER = ((Label)grdrow.FindControl("lblDIFFER"));
                    Label lblSALEDONE = ((Label)grdrow.FindControl("lblSALEDONE"));
                    Label lblLOCATION = ((Label)grdrow.FindControl("lblLOCATION"));
                    Label lblUSERNAME = ((Label)grdrow.FindControl("lblUSERNAME"));
                    Label lblCREATEDATE = ((Label)grdrow.FindControl("lblCREATEDATE"));
                    Label lblRATE = ((Label)grdrow.FindControl("lblRATE"));
                    Label lblITEMID = ((Label)grdrow.FindControl("lblITEMID"));
                    Label lblITEMGRP = ((Label)grdrow.FindControl("lblITEMGRP"));
                    Label lblUOM = ((Label)grdrow.FindControl("lblUOM"));
                    Label lblBASEAMOUNT = ((Label)grdrow.FindControl("lblBASEAMOUNT"));
                    Label lblCONDID = ((Label)grdrow.FindControl("lblCONDID"));
                    Label lblCONDTYPE = ((Label)grdrow.FindControl("lblCONDTYPE"));
                    Label lblJOBSTATDESC = ((Label)grdrow.FindControl("lblJOBSTATDESC"));
                    Label lblSIZE = ((Label)grdrow.FindControl("lblSIZE"));
                    Label lblGrade = ((Label)grdrow.FindControl("lblGrade"));

                    DataTable dt = (DataTable)ViewState["TempCromaRateCaedSelectedItem"];
                    //DataRow dr1 = dt.Select("JOBID = '" + lblJOBID.Text + "'").FirstOrDefault();
                    //if (dr1 != null)
                    //{
                    //    if (Convert.ToString(dr1[0]) != null && Convert.ToString(dr1[0]) != "" && Convert.ToString(dr1[0]) != string.Empty)
                    //    {
                    //        if (chkSelect.Checked == true)
                    //        {
                    //            chkSelect.Checked = false;
                    //        }
                    //        else
                    //        {
                    //            chkSelect.Checked = true;
                    //        }

                    //    }
                    //}

                    //if (chkSelect.Checked == true)
                    //{
                    decimal saleprice = 0;
                    decimal baseamt = 0;
                    if (rblPrice.SelectedValue == "1")
                    {
                        saleprice = Convert.ToDecimal(lblDEALERPRICE.Text);

                    }
                    else if (rblPrice.SelectedValue == "3")
                    {
                        saleprice = Convert.ToDecimal(lblMRP.Text);

                    }
                    else
                    {
                        saleprice = Convert.ToDecimal(lblCUSTOMERPRCE.Text);
                    }
                    baseamt = Math.Round(((saleprice * 100) / (100 + (Convert.ToDecimal(lblRATE.Text)))), 2);


                    lblTotalQty.Text = Convert.ToString(Convert.ToDecimal(lblTotalQty.Text) + 1);
                    lblTotalAmt.Text = Convert.ToString(Convert.ToDecimal(lblTotalAmt.Text) + saleprice);

                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Select("JOBID = '" + lblJOBID.Text + "'").FirstOrDefault();
                        if (dr != null)
                        {
                            if (Convert.ToString(dr[0]) != null && Convert.ToString(dr[0]) != "" && Convert.ToString(dr[0]) != string.Empty)
                            {
                                lblTotalQty.Text = Convert.ToString(Convert.ToDecimal(lblTotalQty.Text) - 1);
                                lblTotalAmt.Text = Convert.ToString(Convert.ToDecimal(lblTotalAmt.Text) - saleprice);
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('This item is already added.!');", true);
                            }
                            else
                            {
                                dt.Rows.Add(lblCATEGORY.Text, lblBRAND.Text, lblITEMCODE.Text, lblIETMDESC.Text, lblJOBID.Text, lblSERIALNO.Text, lblMRP.Text,lblONLINEPRICE.Text, lblDEALERPRICE.Text, lblPURCHASEPRICE.Text, lblCUSTOMERPRCE.Text, lblDIFFER.Text, lblSALEDONE.Text, lblJOBSTATDESC.Text, lblLOCATION.Text, lblUSERNAME.Text, lblCREATEDATE.Text,
                                lblRATE.Text, lblITEMID.Text, lblITEMGRP.Text, lblUOM.Text, baseamt, lblCONDID.Text, lblCONDTYPE.Text, lblSIZE.Text, lblGrade.Text);
                            }
                        }
                        else
                        {
                            dt.Rows.Add(lblCATEGORY.Text, lblBRAND.Text, lblITEMCODE.Text, lblIETMDESC.Text, lblJOBID.Text, lblSERIALNO.Text, lblMRP.Text, lblONLINEPRICE.Text, lblDEALERPRICE.Text, lblPURCHASEPRICE.Text, lblCUSTOMERPRCE.Text, lblDIFFER.Text, lblSALEDONE.Text, lblJOBSTATDESC.Text, lblLOCATION.Text, lblUSERNAME.Text, lblCREATEDATE.Text,
                                lblRATE.Text, lblITEMID.Text, lblITEMGRP.Text, lblUOM.Text, baseamt, lblCONDID.Text, lblCONDTYPE.Text, lblSIZE.Text, lblGrade.Text);
                        }
                    }
                    else
                    {
                        dt.Rows.Add(lblCATEGORY.Text, lblBRAND.Text, lblITEMCODE.Text, lblIETMDESC.Text, lblJOBID.Text, lblSERIALNO.Text, lblMRP.Text, lblONLINEPRICE.Text, lblDEALERPRICE.Text, lblPURCHASEPRICE.Text, lblCUSTOMERPRCE.Text, lblDIFFER.Text, lblSALEDONE.Text, lblJOBSTATDESC.Text, lblLOCATION.Text, lblUSERNAME.Text, lblCREATEDATE.Text,
                            lblRATE.Text, lblITEMID.Text, lblITEMGRP.Text, lblUOM.Text, baseamt, lblCONDID.Text, lblCONDTYPE.Text, lblSIZE.Text, lblGrade.Text);
                    }


                    ViewState["TempCromaRateCaedSelectedItem"] = dt;

                    //}
                    //else if (chkSelect.Checked == false)
                    //{
                    //    lblTotalQty.Text = Convert.ToString(Convert.ToDecimal(lblTotalQty.Text) - 1);
                    //    lblTotalAmt.Text = Convert.ToString(Convert.ToDecimal(lblTotalAmt.Text) - saleprice);



                    //    dt.Select("JOBID='" + lblJOBID.Text + "'").ToList().ForEach(x => x.Delete());
                    //    dt.AcceptChanges();
                    //    ViewState["TempCromaRateCaedSelectedItem"] = dt;

                    //}

                    grvTempData.DataSource = dt;
                    grvTempData.DataBind();

                    //if (grvData.Rows.Count > 0)
                    //{
                    //    grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //}

                    //if (grvData.Rows.Count > 0)
                    //{
                    //    grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //}

                    int rowIndex = grdrow.RowIndex;
                    ScriptManager.RegisterStartupScript(this, GetType(), "ScrollToItem", $"scrollToItem({rowIndex});", true);
                    rowIndexold = grdrow.RowIndex;






                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                }

                if (grvData.Rows.Count > 0)
                {
                    grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "BindMakeAssociateModel", $"BindMakeAssociateModel();", true);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lnkCrQuot_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), "tsmCromaQuotation", "");
                    if (FormRights.bAdd == false)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to create Quotation.');", true);
                    }
                    else
                    {
                        //if (grvData.Rows.Count > 0)
                        //{
                        //    grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
                        //}

                        //GridViewRow hrow = grvData.HeaderRow;
                        //CheckBox chkSelectAll = (CheckBox)hrow.FindControl("chkSelectAll");
                        //chkSelectAll.Enabled = true;

                        for (int i = 0; i < grvData.Rows.Count; i++)
                        {
                            GridViewRow row = grvData.Rows[i];
                            LinkButton chkSelect = ((LinkButton)row.FindControl("chkSelect"));
                            chkSelect.Enabled = true;
                        }

                        imgSaveAll.Visible = true;
                        imgSaveAll.Enabled = true;

                        lnkCrQuot.Visible = false;
                        lnkCrQuot.Enabled = false;


                    }
                    if (grvData.Rows.Count > 0)
                    {
                        grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "BindMakeAssociateModel", $"BindMakeAssociateModel();", true);
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

        //protected void txtVendorCode_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Session["USERID"] != null)
        //        {
        //            if (txtVendorCode.Text.Length >= 5)
        //            {
        //                lblVendorError.Text = string.Empty;
        //                lblVendorError.Visible = false;
        //                DataTable dt = objMainClass.GetVendorDetails(objMainClass.intCmpId, txtVendorCode.Text, "");
        //                if (dt.Rows.Count > 0)
        //                {
        //                    lblVendorError.Text = string.Empty;
        //                    lblVendorError.Visible = false;
        //                    txtVendorName.Text = Convert.ToString(dt.Rows[0]["NAME1"]);
        //                    txtVendorCode.Text = Convert.ToString(dt.Rows[0]["VENDCODE"]);
        //                }
        //                else
        //                {
        //                    lblVendorError.Text = "Invalid Vendor Code. Please Enter Correct Vendor Code.";
        //                    lblVendorError.Visible = true;
        //                    txtVendorCode.Focus();
        //                    ScriptManager.GetCurrent(this.Page).SetFocus(this.txtVendorCode);
        //                }
        //            }
        //            else
        //            {
        //                lblVendorError.Text = "Minimum 5 digit req.";
        //                lblVendorError.Visible = true;
        //                txtVendorCode.Focus();
        //                ScriptManager.GetCurrent(this.Page).SetFocus(this.txtVendorCode);

        //            }
        //            if (grvData.Rows.Count > 0)
        //            {
        //                grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
        //            }
        //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
        //            if (grvData.Rows.Count > 0)
        //            {
        //                grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
        //            }
        //            ScriptManager.RegisterStartupScript(this, GetType(), "BindMakeAssociateModel", $"BindMakeAssociateModel();", true);
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

        protected void txtVendorName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (txtVendorName.Text != string.Empty && txtVendorName.Text != null && txtVendorName.Text != "")
                    {
                        rfvVendorCode.Enabled = false;
                        ddlVendor.SelectedIndex = 0;
                    }
                    else
                    {
                        rfvVendorCode.Enabled = true;
                        rfvVrndorName.Enabled = true;
                    }
                    //if (grvData.Rows.Count > 0)
                    //{
                    //    grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //}
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                    //if (grvData.Rows.Count > 0)
                    //{
                    //    grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //}
                    //ScriptManager.RegisterStartupScript(this, GetType(), "BindMakeAssociateModel", $"BindMakeAssociateModel();", true);
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

        protected void txtDiscountper_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (txtDiscountper.Text != string.Empty && txtDiscountper.Text != null && txtDiscountper.Text != "")
                    {
                        txtFinalAmount.Text = Convert.ToString(Math.Round(Convert.ToDecimal(Convert.ToDecimal(txtTotalAmt.Text) - (Convert.ToDecimal((Convert.ToDecimal(txtTotalAmt.Text) * Convert.ToDecimal(txtDiscountper.Text)) / 100))), 2));
                        txtDiscountRs.Text = Convert.ToString(Math.Round(Convert.ToDecimal(Convert.ToDecimal((Convert.ToDecimal(txtTotalAmt.Text) * Convert.ToDecimal(txtDiscountper.Text)) / 100)), 2));
                    }
                    else
                    {
                        txtDiscountper.Text = "0";
                        txtFinalAmount.Text = Convert.ToString(Math.Round(Convert.ToDecimal(Convert.ToDecimal(txtTotalAmt.Text) - (Convert.ToDecimal((Convert.ToDecimal(txtTotalAmt.Text) * Convert.ToDecimal(txtDiscountper.Text)) / 100))), 2));
                        txtDiscountRs.Text = Convert.ToString(Math.Round(Convert.ToDecimal(Convert.ToDecimal((Convert.ToDecimal(txtTotalAmt.Text) * Convert.ToDecimal(txtDiscountper.Text)) / 100)), 2));
                    }
                    //if (grvData.Rows.Count > 0)
                    //{
                    //    grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //}
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                    //if (grvData.Rows.Count > 0)
                    //{
                    //    grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //}
                    //ScriptManager.RegisterStartupScript(this, GetType(), "BindMakeAssociateModel", $"BindMakeAssociateModel();", true);
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

        protected void txtDiscountRs_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (txtDiscountRs.Text != string.Empty && txtDiscountRs.Text != null && txtDiscountRs.Text != "")
                    {
                        txtFinalAmount.Text = Convert.ToString(Math.Round(Convert.ToDecimal(Convert.ToDecimal(txtTotalAmt.Text) - Convert.ToDecimal(txtDiscountRs.Text)), 2));
                        txtDiscountper.Text = Convert.ToString(Math.Round(Convert.ToDecimal(Convert.ToDecimal((100 * Convert.ToDecimal(txtDiscountRs.Text)) / Convert.ToDecimal(txtTotalAmt.Text))), 2).ToString("#0.00"));
                    }
                    else
                    {
                        txtDiscountRs.Text = "0";
                        txtFinalAmount.Text = Convert.ToString(Math.Round(Convert.ToDecimal(Convert.ToDecimal(txtTotalAmt.Text) - Convert.ToDecimal(txtDiscountRs.Text)), 2));
                        txtDiscountper.Text = Convert.ToString(Math.Round(Convert.ToDecimal(Convert.ToDecimal((100 * Convert.ToDecimal(txtDiscountRs.Text)) / Convert.ToDecimal(txtTotalAmt.Text))), 2).ToString("#0.00"));
                    }
                    //if (grvData.Rows.Count > 0)
                    //{
                    //    grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //}
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                    //if (grvData.Rows.Count > 0)
                    //{
                    //    grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //}
                    //ScriptManager.RegisterStartupScript(this, GetType(), "BindMakeAssociateModel2", $"BindMakeAssociateModel2();", true);
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
            try
            {
                if (Session["USERID"] != null)
                {
                    string quotationno = objMainClass.InsertCromaQuotation(objMainClass.intCmpId, ddlVendor.SelectedValue, txtVendorName.Text, "", Convert.ToDecimal(txtTotalAmt.Text), Convert.ToDecimal(txtDiscountper.Text), Convert.ToDecimal(txtDiscountRs.Text),
                        0, Convert.ToDecimal(txtFinalAmount.Text), grvTempData, ddlPlant.SelectedValue, ddlLocation.SelectedValue, Convert.ToInt32(Session["USERID"]), txtMobileNo.Text, ddlPaymentTerms.SelectedValue, ddlPaymentTerms.SelectedItem.Text, Convert.ToInt32(rblPrice.SelectedValue));
                    if (quotationno != null && quotationno != "" && quotationno != string.Empty)
                    {
                        if (Convert.ToDecimal(txtDiscountper.Text) > Convert.ToDecimal(Session["CROMADISCRATE"]))
                        {
                            String strCustContent = "";
                            strCustContent = fileread();
                            strCustContent = strCustContent.Replace("###Heading###", "New Quotation Created by User.");
                            strCustContent = strCustContent.Replace("###CreateBy###", Convert.ToString(Session["USERNAME"]));
                            strCustContent = strCustContent.Replace("###CreateDate###", Convert.ToString(DateTime.Now));
                            strCustContent = strCustContent.Replace("###QUOTNO###", quotationno);
                            strCustContent = strCustContent.Replace("###Message###", "Quotation created by user. Basic details are as per above.");
                            strCustContent = strCustContent.Replace("###LINK###", "http://14.98.132.190:360/MM/frmApproveQuotation.aspx");
                            strCustContent = strCustContent.Replace("###HREFLINK###", "http://14.98.132.190:360/MM/frmApproveQuotation.aspx");

                            DataTable dt = new DataTable();
                            dt = objMainClass.MailSenderReceiver("QO", 1, 36, null, 2, Convert.ToDecimal(txtFinalAmount.Text));
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
                                objMainClass.SaveMailNotification(objMainClass.EmailID, objMainClass.Password, Convert.ToString(dt.Rows[0]["EMAILID"]), Reciever, "New Quotation Created #" + quotationno, strCustContent, objMainClass.PORT, quotationno, Convert.ToString(Session["USERID"]), "QO");
                            }
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Quotation saved successfully. Send for approval.  Quotation No. : " + quotationno + "\");$('.close').click(function(){window.location.href ='frmViewCromaRateCard.aspx' });", true);
                        }
                        else
                        {

                            String strCustContent = "";
                            strCustContent = fileread();
                            strCustContent = strCustContent.Replace("###Heading###", "New Quotation Created.");
                            strCustContent = strCustContent.Replace("###CreateBy###", Convert.ToString(Session["USERNAME"]));
                            strCustContent = strCustContent.Replace("###CreateDate###", Convert.ToString(DateTime.Now));
                            strCustContent = strCustContent.Replace("###QUOTNO###", quotationno);
                            strCustContent = strCustContent.Replace("###Message###", "Quotation created. Basic details are as per above.");
                            strCustContent = strCustContent.Replace("###LINK###", "http://14.98.132.190:360/MM/ViewQuotation.aspx");
                            strCustContent = strCustContent.Replace("###HREFLINK###", "http://14.98.132.190:360/MM/ViewQuotation.aspx");

                            string FileName = quotationno + "_Quotation.pdf";
                            string PDFURL = "http://14.98.132.190:360/excel/" + FileName;
                            string URL = DownloadTCR(quotationno);

                            //objWAClass.SendMediaFile("Please check attached Quotation.  - Mobex", "91" + txtMobileNo.Text, Convert.ToString(Session["USERID"]), PDFURL);
                            objWAClass.SendMessageNewAPI("Please check attached Quotation.  - Mobex", "91" + txtMobileNo.Text, Convert.ToString(Session["USERID"]), PDFURL);

                            if (Convert.ToString(Session["USERMOBILE"]) != "" && Convert.ToString(Session["USERMOBILE"]) != null && Convert.ToString(Session["USERMOBILE"]) != string.Empty)
                            {
                                //objWAClass.SendMediaFile("Please check attached Quotation.  - Mobex", "91" + Convert.ToString(Session["USERMOBILE"]), Convert.ToString(Session["USERID"]), PDFURL);
                                objWAClass.SendMessageNewAPI("Please check attached Quotation.  - Mobex", "91" + Convert.ToString(Session["USERMOBILE"]), Convert.ToString(Session["USERID"]), PDFURL);
                            }

                            if (Convert.ToString(Session["USEREMAILID"]) != "" && Convert.ToString(Session["USEREMAILID"]) != null && Convert.ToString(Session["USEREMAILID"]) != string.Empty)
                            {
                                //objWAClass.SendMediaFile("Please check attached Quotation.  - Mobex", "91" + Convert.ToString(Session["USERMOBILE"]), Convert.ToString(Session["USERID"]), PDFURL);
                                objMainClass.SendMailWithAttachment(Convert.ToString(Session["USEREMAILID"]), "mohit.diwakar@qarmatek.com", "info@qarmatek.com", "Hof75626", 587, "New Quotation Created #" + quotationno, strCustContent, URL);
                            }

                            File.Delete(URL);

                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Quotation saved successfully.  Quotation No. : " + quotationno + "\");$('.close').click(function(){window.location.href ='frmViewCromaRateCard.aspx' });", true);
                        }


                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Record not inserted. Please try again.!');$('.close').click(function(){window.location.href ='frmViewCromaRateCard.aspx' });", true);
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

        public string DownloadTCR(string Quotation)
        {
            string url = "";
            DataTable dtPOMST = new DataTable();
            DataTable dtPODTL = new DataTable();
            DataTable dtPOAPRV = new DataTable();
            DataTable dtPOTAX = new DataTable();
            dtPOMST = objMainClass.SelectQuotData(objMainClass.intCmpId, Quotation, 1);
            dtPODTL = objMainClass.SelectQuotData(objMainClass.intCmpId, Quotation, 2);
            dtPOAPRV = objMainClass.SelectQuotData(objMainClass.intCmpId, Quotation, 4);
            dtPOTAX = objMainClass.SelectQuotData(objMainClass.intCmpId, Quotation, 5);

            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = "Report/rptQuotation.rdlc";
            ReportDataSource rds = new ReportDataSource("DataSet1", dtPOMST);
            ReportDataSource rds1 = new ReportDataSource("DataSet2", dtPODTL);
            ReportDataSource rds2 = new ReportDataSource("DataSet3", dtPOAPRV);
            ReportDataSource rds3 = new ReportDataSource("DataSet4", dtPOTAX);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.DataSources.Add(rds1);
            ReportViewer1.LocalReport.DataSources.Add(rds2);
            ReportViewer1.LocalReport.DataSources.Add(rds3);

            string FileName = Quotation + "_Quotation.pdf";
            string extension = ".pdf";
            string encoding = String.Empty;
            Warning[] warnings;
            string mimeType = String.Empty;
            string[] streams;
            string[] streamIds;
            string contentType = String.Empty;


            Byte[] mybytes = ReportViewer1.LocalReport.Render("PDF", null,
                    out extension, out encoding,
                    out mimeType, out streams, out warnings);

            string folderpath = "~/excel/";
            string filePath = Path.Combine(Server.MapPath(folderpath), FileName);

            string PDFURL = "http://14.98.132.190:360/excel/" + FileName;

            //string localPath = (Server.MapPath(PDFURL));
            //string localPath = (Server.MapPath("../img/" + FileName));
            System.IO.File.WriteAllBytes(filePath, mybytes);
            //System.IO.File.WriteAllBytes(PDFURL, mybytes);

            url = filePath;

            return url;
        }

        protected static string fileread()
        {
            string actualfol;
            actualfol = HttpContext.Current.Server.MapPath("~/QuotationBody.html");
            StreamReader sr = new StreamReader(actualfol);
            string strcontent = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            return strcontent;
        }

        protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (ddlVendor.SelectedIndex > 0)
                    {
                        lblVendorError.Text = string.Empty;
                        lblVendorError.Visible = false;
                        //DataTable dt = objMainClass.GetVendorDetails(objMainClass.intCmpId, ddlVendor.SelectedValue, "");
                        DataTable dt = objMainClass.GetCustData(objMainClass.intCmpId, ddlVendor.SelectedValue, "", "", "", "CUSTDETAILNEW");
                        if (dt.Rows.Count > 0)
                        {
                            lblVendorError.Text = string.Empty;
                            lblVendorError.Visible = false;
                            txtVendorName.Text = Convert.ToString(dt.Rows[0]["NAME1"]);
                            if (Convert.ToString(dt.Rows[0]["MOBILENO"]) != null && Convert.ToString(dt.Rows[0]["MOBILENO"]) != "" && Convert.ToString(dt.Rows[0]["MOBILENO"]) != string.Empty)
                            {
                                txtMobileNo.Text = Convert.ToString(dt.Rows[0]["MOBILENO"]);
                            }
                            //txtVendorCode.Text = Convert.ToString(dt.Rows[0]["VENDCODE"]);
                        }
                        else
                        {
                            lblVendorError.Text = "Invalid Vendor Code. Please Enter Correct Vendor Code.";
                            lblVendorError.Visible = true;
                            ddlVendor.Focus();
                            ScriptManager.GetCurrent(this.Page).SetFocus(this.ddlVendor);
                        }
                    }
                    else
                    {

                    }

                    //if (grvData.Rows.Count > 0)
                    //{
                    //    grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //}
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                    //if (grvData.Rows.Count > 0)
                    //{
                    //    grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //}
                    //ScriptManager.RegisterStartupScript(this, GetType(), "BindMakeAssociateModel", $"BindMakeAssociateModel();", true);

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

        protected void lnkTempDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;


                    int rowIndex1 = grdrow.RowIndex;
                    //CheckBox chkSelect = (CheckBox)grdrow.FindControl("chkSelect");
                    Label lblCATEGORY = ((Label)grdrow.FindControl("lblCATEGORY"));
                    Label lblBRAND = ((Label)grdrow.FindControl("lblBRAND"));
                    Label lblITEMCODE = ((Label)grdrow.FindControl("lblITEMCODE"));
                    Label lblIETMDESC = ((Label)grdrow.FindControl("lblIETMDESC"));
                    Label lblJOBID = ((Label)grdrow.FindControl("lblJOBID"));
                    Label lblSERIALNO = ((Label)grdrow.FindControl("lblSERIALNO"));
                    Label lblMRP = ((Label)grdrow.FindControl("lblMRP"));
                    Label lblONLINEPRICE = ((Label)grdrow.FindControl("lblONLINEPRICE"));
                    Label lblDEALERPRICE = ((Label)grdrow.FindControl("lblDEALERPRICE"));
                    Label lblPURCHASEPRICE = ((Label)grdrow.FindControl("lblPURCHASEPRICE"));
                    Label lblCUSTOMERPRCE = ((Label)grdrow.FindControl("lblCUSTOMERPRCE"));
                    Label lblDIFFER = ((Label)grdrow.FindControl("lblDIFFER"));
                    Label lblSALEDONE = ((Label)grdrow.FindControl("lblSALEDONE"));
                    Label lblLOCATION = ((Label)grdrow.FindControl("lblLOCATION"));
                    Label lblUSERNAME = ((Label)grdrow.FindControl("lblUSERNAME"));
                    Label lblCREATEDATE = ((Label)grdrow.FindControl("lblCREATEDATE"));
                    Label lblRATE = ((Label)grdrow.FindControl("lblRATE"));
                    Label lblITEMID = ((Label)grdrow.FindControl("lblITEMID"));
                    Label lblITEMGRP = ((Label)grdrow.FindControl("lblITEMGRP"));
                    Label lblUOM = ((Label)grdrow.FindControl("lblUOM"));
                    Label lblBASEAMOUNT = ((Label)grdrow.FindControl("lblBASEAMOUNT"));
                    Label lblCONDID = ((Label)grdrow.FindControl("lblCONDID"));
                    Label lblCONDTYPE = ((Label)grdrow.FindControl("lblCONDTYPE"));
                    Label lblJOBSTATDESC = ((Label)grdrow.FindControl("lblJOBSTATDESC"));

                    decimal saleprice = 0;
                    if (rblPrice.SelectedValue == "1")
                    {
                        saleprice = Convert.ToDecimal(lblDEALERPRICE.Text);
                    }
                    else if (rblPrice.SelectedValue == "3")
                    {
                        saleprice = Convert.ToDecimal(lblMRP.Text);

                    }
                    else
                    {
                        saleprice = Convert.ToDecimal(lblCUSTOMERPRCE.Text);
                    }

                    DataTable dt = (DataTable)ViewState["TempCromaRateCaedSelectedItem"];

                    lblTotalQty.Text = Convert.ToString(Convert.ToDecimal(lblTotalQty.Text) - 1);
                    lblTotalAmt.Text = Convert.ToString(Convert.ToDecimal(lblTotalAmt.Text) - saleprice);



                    dt.Select("JOBID='" + lblJOBID.Text + "'").ToList().ForEach(x => x.Delete());
                    dt.AcceptChanges();
                    ViewState["TempCromaRateCaedSelectedItem"] = dt;

                    //for (int i = 0; i < grvData.Rows.Count; i++)
                    //{
                    //    GridViewRow row = grvData.Rows[i];
                    //    CheckBox chkSelect = ((CheckBox)row.FindControl("chkSelect"));
                    //    Label jobid = ((Label)row.FindControl("lblJOBID"));
                    //    if (jobid.Text == lblJOBID.Text)
                    //    {
                    //        chkSelect.Checked = false;
                    //    }
                    //}


                    grvTempData.DataSource = dt;
                    grvTempData.DataBind();

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

        protected void lnkJobid_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    string lblJOBID = ((Label)grdrow.FindControl("lblJOBID")).Text;

                    if (grvData.Rows.Count > 0)
                    {
                        grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                    string path = "frmCromaJobID.aspx";
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "?JOBID=" + lblJOBID + "');", true);

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

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetItemCode(string prefixText, int count)
        {
            List<string> ItemCode = new List<string>();

            MainClass objMainClass = new MainClass();
            ItemCode = objMainClass.GetLikeJobData(prefixText, "GETLIKEJOBID");

            return ItemCode;

        }
        protected void txtItemDesc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    txtJobID.Text = txtItemDesc.Text.Split('-')[0].Trim().ToString();
                    txtItemDesc.Text = "";
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