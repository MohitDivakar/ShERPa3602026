using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.UTILITY
{
    public partial class frmQuotationEdit : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        WAClass objWAClass = new WAClass();
        BindDDL objBindDDL = new BindDDL();

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
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='frmApproveQuotation.aspx' });", true);
                            return;
                        }


                        if (Request.QueryString.Count > 0)
                        {

                            if (Convert.ToString(Request.QueryString["QUOTNO"]) != null && Convert.ToString(Request.QueryString["QUOTNO"]) != string.Empty && Convert.ToString(Request.QueryString["QUOTNO"]) != "")
                            {
                                Session["EDITQUOT"] = Convert.ToString(Request.QueryString["QUOTNO"]);
                            }
                            Response.Redirect(Request.Url.AbsolutePath, false);
                        }
                        else
                        {

                            GetData(Convert.ToString(Session["EDITQUOT"]));
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


        public void GetData(string QUOTNO)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    DataTable dtPOMST = new DataTable();
                    DataTable dtPODTL = new DataTable();
                    DataTable dtPOCOND = new DataTable();
                    dtPOMST = objMainClass.SelectQuotData(objMainClass.intCmpId, QUOTNO, 1);
                    dtPODTL = objMainClass.SelectQuotData(objMainClass.intCmpId, QUOTNO, 2);
                    dtPOCOND = objMainClass.SelectQuotData(objMainClass.intCmpId, QUOTNO, 3);

                    Session["QUOTDTL"] = dtPODTL;
                    Session["QUOTCOND"] = dtPOCOND;

                    if (dtPOMST.Rows.Count > 0)
                    {
                        lblDoctype.Text = Convert.ToString(dtPOMST.Rows[0]["DOCTYPE"]);
                        hfPODate.Value = Convert.ToDateTime(dtPOMST.Rows[0]["PODTD"]).ToShortDateString();
                        hfMail.Value = Convert.ToString(dtPOMST.Rows[0]["VENDOREMAIL"]);
                        hfMobile.Value = Convert.ToString(dtPOMST.Rows[0]["MOBILENO"]);
                        txtInvoiceTo.Text = Convert.ToString(dtPOMST.Rows[0]["VENDORCODE"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["VENDORNAME"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["VENDOREMAIL"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["VENDADDR1"]) + System.Environment.NewLine +
                            Convert.ToString(dtPOMST.Rows[0]["VENDADDR2"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["VENDADDR3"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["VENDCITY"]) + " " + Convert.ToString(dtPOMST.Rows[0]["VENDPINCODE"])
                            + " " + Convert.ToString(dtPOMST.Rows[0]["VENDSTATE"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["VENDCONTACTINFO"]) + System.Environment.NewLine + "" + "GST No. : " + Convert.ToString(dtPOMST.Rows[0]["VENDGSTNO"]);

                        txtDeliveryTo.Text = Convert.ToString(dtPOMST.Rows[0]["VENDORCODE"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["VENDORNAME"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["VENDOREMAIL"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["VENDADDR1"]) + System.Environment.NewLine +
                            Convert.ToString(dtPOMST.Rows[0]["VENDADDR2"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["VENDADDR3"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["VENDCITY"]) + " " + Convert.ToString(dtPOMST.Rows[0]["VENDPINCODE"])
                            + " " + Convert.ToString(dtPOMST.Rows[0]["VENDSTATE"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["VENDCONTACTINFO"]) + System.Environment.NewLine + "" + "GST No. : " + Convert.ToString(dtPOMST.Rows[0]["VENDGSTNO"]);

                        txtSupplier.Text = Convert.ToString(dtPOMST.Rows[0]["CMPNAME"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["CMPADDR1"]) + System.Environment.NewLine +
                            Convert.ToString(dtPOMST.Rows[0]["CMPADDR2"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["CMPADDR3"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["CMPCITY"]) + " " + Convert.ToString(dtPOMST.Rows[0]["CMPPINCODE"])
                            + " " + Convert.ToString(dtPOMST.Rows[0]["CMPSTATE"]) + System.Environment.NewLine + "" + "GST No. : " + Convert.ToString(dtPOMST.Rows[0]["CMPGST"]);

                        txtPODetail.Text = "Quotation No. : " + Convert.ToString(dtPOMST.Rows[0]["QUOTNO"]) + System.Environment.NewLine + ""
                                         + "Quotation Date : " + Convert.ToDateTime(dtPOMST.Rows[0]["PODTD"]).ToShortDateString() + System.Environment.NewLine + ""
                                         + "Net Amount : " + Convert.ToString(dtPOMST.Rows[0]["QUOTAMT"]);

                        lblPONo.Text = Convert.ToString(dtPOMST.Rows[0]["QUOTNO"]);
                        lblMaterialAmt.Text = Convert.ToString(dtPOMST.Rows[0]["TOTALAMT"]);
                        lblTaxAmt.Text = Convert.ToString(dtPOMST.Rows[0]["TOTALTAXAMT"]);
                        lblDiscountAmt.Text = Convert.ToString(dtPOMST.Rows[0]["TOTALDISCAMT"]);
                        lblDiscountpercentage.Text = Convert.ToString(dtPOMST.Rows[0]["TOTALDISCPER"]);
                        lblOtherChg.Text = Convert.ToString(Convert.ToDecimal(dtPOMST.Rows[0]["TOTALBASEAMT"]) + Convert.ToDecimal(dtPOMST.Rows[0]["TOTALDISCAMT"]));
                        lblPOTotalAmt.Text = Convert.ToString(dtPOMST.Rows[0]["QUOTAMT"]);

                        gvDetail.DataSource = dtPODTL;
                        gvDetail.DataBind();
                        gvDetail.GridLines = GridLines.None;

                        grvTaxation.DataSource = dtPOCOND;
                        grvTaxation.DataBind();


                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);

                        DataTable logDT = new DataTable();
                        logDT = objMainClass.SELECT_REQUISITION_LOG(lblPONo.Text);
                        if (logDT.Rows.Count > 0)
                        {
                            for (int k = 0; k < logDT.Rows.Count; k++)
                            {
                                lblAPRV1.Text = lblAPRV1.Text + " <br/>" + Convert.ToString(logDT.Rows[k]["LISTDESC"]) + "  By  " + Convert.ToString(logDT.Rows[k]["USERNAME"]) + "  On  " + Convert.ToString(logDT.Rows[k]["APRVDATE"]) + " , Reason : " + Convert.ToString(logDT.Rows[k]["REASON"]);
                                lblAPRV1.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        gvDetail.DataSource = string.Empty;
                        gvDetail.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
                    }

                    //if (gvList.Rows.Count > 0)
                    //{
                    //    gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //}
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

        protected void lnkRecalc_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    decimal netsaleamount = 0;
                    decimal netbaseamount = 0;
                    decimal discountper = 0;
                    decimal discountamount = 0;
                    decimal nettaxableamount = 0;
                    decimal nettaxamount = 0;
                    decimal netquotamount = 0;

                    discountper = Convert.ToDecimal(lblDiscountpercentage.Text);
                    for (int i = 0; i < gvDetail.Rows.Count; i++)
                    {
                        GridViewRow row = gvDetail.Rows[i];

                        Label lblITEMBRATE = (Label)row.FindControl("lblITEMRATE");
                        Label lblGSTRATE = (Label)row.FindControl("lblGSTRATE");

                        //Math.Round((),2);

                        decimal saleamt = Math.Round((Convert.ToDecimal(lblITEMBRATE.Text)), 2);
                        decimal baseamt = Math.Round(((saleamt * 100) / (100 + Convert.ToDecimal(lblGSTRATE.Text))), 2);
                        decimal discamt = Math.Round(((baseamt * discountper) / 100), 2);
                        decimal taxable = Math.Round((baseamt - discamt), 2);
                        decimal taxamt = Math.Round(((taxable * Convert.ToDecimal(lblGSTRATE.Text)) / 100), 2);
                        decimal itemrate = taxable + taxamt;

                        netsaleamount = netsaleamount + saleamt;
                        netbaseamount = netbaseamount + baseamt;
                        discountamount = discountamount + discamt;
                        nettaxableamount = nettaxableamount + taxable;
                        nettaxamount = nettaxamount + taxamt;
                        netquotamount = netquotamount + taxable + taxamt;
                    }

                    lblMaterialAmt.Text = Convert.ToString(netsaleamount);
                    lblOtherChg.Text = Convert.ToString(netbaseamount);
                    lblDiscountpercentage.Text = Convert.ToString(discountper);
                    lblDiscountAmt.Text = Convert.ToString(discountamount);
                    lblTaxAmt.Text = Convert.ToString(nettaxamount);
                    lblPOTotalAmt.Text = Convert.ToString(netquotamount);
                    imgSaveAll.Visible = true;
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

        protected void lnkAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    objBindDDL.FillBrandCroma(ddlBrand, "GETBRANDS");
                    objBindDDL.FillCategoryCroma(ddlCategory, "GETCATEGORY");
                    objBindDDL.FillSizeCroma(ddlSize, "GETSIZE");

                    DataTable dtData = new DataTable();
                    dtData = objMainClass.GetRateCard(objMainClass.intCmpId, "", "", "", "", "", 1, "", "", "GETDATA");
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

        protected void lnkSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

                    Label lblCATEGORY = ((Label)grdrow.FindControl("lblCATEGORY"));
                    Label lblBRAND = ((Label)grdrow.FindControl("lblBRAND"));
                    Label lblITEMCODE = ((Label)grdrow.FindControl("lblITEMCODE"));
                    Label lblIETMDESC = ((Label)grdrow.FindControl("lblIETMDESC"));
                    Label lblJOBID = ((Label)grdrow.FindControl("lblJOBID"));
                    Label lblSERIALNO = ((Label)grdrow.FindControl("lblSERIALNO"));
                    Label lblDEALERPRICE = ((Label)grdrow.FindControl("lblDEALERPRICE"));
                    Label lblPURCHASEPRICE = ((Label)grdrow.FindControl("lblPURCHASEPRICE"));
                    Label lblCUSTOMERPRCE = ((Label)grdrow.FindControl("lblCUSTOMERPRCE"));
                    Label lblMRP = ((Label)grdrow.FindControl("lblMRP"));
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
                        //saleprice = Convert.ToDecimal(lblDEALERPRICE.Text);
                        saleprice = Convert.ToDecimal(lblMRP.Text) / 2;

                    }
                    else
                    {
                        saleprice = Convert.ToDecimal(lblCUSTOMERPRCE.Text);
                    }



                    decimal saleamt = Math.Round(saleprice, 2);
                    decimal baseamt = Math.Round(Convert.ToDecimal((saleprice * 100) / (Convert.ToDecimal(lblRATE.Text) + 100)), 2);
                    decimal discount = Math.Round(((baseamt * Convert.ToDecimal(lblDiscountpercentage.Text)) / 100), 2);
                    decimal taxable = Math.Round((baseamt - discount), 2);
                    decimal taxamt = Math.Round(((taxable * Convert.ToDecimal(lblRATE.Text)) / 100), 2);
                    decimal itemrate = Math.Round((taxable + taxamt), 2);
                    decimal itemamt = Math.Round((baseamt - discount + taxamt), 2);

                    int same = 0;
                    for (int i = 0; i < gvDetail.Rows.Count; i++)
                    {
                        GridViewRow row = gvDetail.Rows[i];
                        Label lblTRACKNO = (Label)row.FindControl("lblTRACKNO");
                        if (lblTRACKNO.Text == lblJOBID.Text)
                        {
                            same++;
                        }
                    }

                    if (same > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('This item is already added.!');", true);
                    }
                    else
                    {
                        DataTable dtDtl = (DataTable)Session["QUOTDTL"];
                        DataTable dtCond = (DataTable)Session["QUOTCOND"];

                        DataRow lastRowdtl = dtDtl.Rows[dtDtl.Rows.Count - 1];
                        int iddtl = Convert.ToInt32(lastRowdtl["QUOTID"]) + 1;

                        string plant = Convert.ToString(lastRowdtl["ITEMPLANTID"]);
                        string plantcd = Convert.ToString(lastRowdtl["ITEMPLANTCD"]);

                        string location = Convert.ToString(lastRowdtl["LOCCDID"]);
                        string locationcd = Convert.ToString(lastRowdtl["ITEMLOCCD"]);

                        string glcode = Convert.ToString(lastRowdtl["GLCODE"]);
                        string costcenter = Convert.ToString(lastRowdtl["CSTCENTCD"]);
                        string costcentercode = Convert.ToString(lastRowdtl["COSTCENTER"]);
                        string profitcenter = Convert.ToString(lastRowdtl["PROFITCENTER"]);
                        string itemtext = Convert.ToString(lastRowdtl["ITEMTEXT"]);

                        DataRow lastRowCond = dtCond.Rows[dtCond.Rows.Count - 1];
                        int idcond = Convert.ToInt32(lastRowCond["TAXSRNO"]) + 1;

                        //dtDtl.Rows.Add(1, "", lblPONo.Text, "", iddtl, lblITEMCODE.Text, lblIETMDESC.Text, lblITEMID.Text, lblJOBID.Text, lblSERIALNO.Text, Convert.ToInt32(lblITEMGRP.Text), "", lblUOM.Text, "", 1, itemrate, saleamt, baseamt, discount, taxable, taxamt, plant, plantcd, location, locationcd,
                        //    glcode, costcenter, costcentercode, profitcenter, itemtext, lblBRAND.Text, "", lblRATE.Text);

                        dtDtl.Rows.Add(1, "", lblPONo.Text, "", iddtl, lblITEMCODE.Text, lblIETMDESC.Text, lblITEMID.Text, "", Convert.ToInt32(lblITEMGRP.Text), "", Convert.ToInt32(lblUOM.Text), 1, Convert.ToDecimal(lblMRP.Text), itemrate, saleamt, itemamt, baseamt, taxable, Convert.ToDecimal(discount), "", glcode, costcentercode,
                            costcenter, plantcd, plant, locationcd, location, profitcenter, "", lblJOBID.Text, itemtext, lblJOBID.Text, itemtext, "", lblSERIALNO.Text, "", "", 1, lblBRAND.Text, "", lblIETMDESC.Text, 0, 0, 0, 0, 0, "", lblCONDTYPE.Text, 1, "", "", lblRATE.Text, taxamt, "", "", 0, "", "", 0, 0);

                        //dtCond.Rows.Add(1, lblPONo.Text, idcond, iddtl, lblCONDID.Text, lblCONDTYPE.Text, lblRATE.Text, baseamt, taxamt, "+", "", 0);
                        dtCond.Rows.Add("+", idcond, iddtl, lblCONDTYPE.Text, Convert.ToDecimal(lblRATE.Text), taxable, taxamt, 0, Convert.ToInt32(lblCONDID.Text), idcond, 1, idcond, lblPONo.Text, glcode);


                        Session["QUOTDTL"] = dtDtl;
                        Session["QUOTCOND"] = dtCond;

                        gvDetail.DataSource = dtDtl;
                        gvDetail.DataBind();

                        grvTaxation.DataSource = dtCond;
                        grvTaxation.DataBind();

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

        protected void lnkTempDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

                    DataTable dtDtl = (DataTable)Session["QUOTDTL"];
                    DataTable dtCond = (DataTable)Session["QUOTCOND"];

                    Label lblTRACKNO = (Label)grdrow.FindControl("lblTRACKNO");

                    DataRow dr1 = dtDtl.Select("TRACKNO = '" + lblTRACKNO.Text + "'").FirstOrDefault();
                    if (dr1 != null)
                    {
                        string srno = Convert.ToString(dr1[4]);

                        dtDtl.Select("TRACKNO='" + lblTRACKNO.Text + "'").ToList().ForEach(x => x.Delete());

                        dtCond.Select("POSRNO='" + srno + "'").ToList().ForEach(x => x.Delete());

                        dtDtl.AcceptChanges();
                        Session["QUOTDTL"] = dtDtl;

                        dtCond.AcceptChanges();
                        Session["QUOTCOND"] = dtCond;

                        gvDetail.DataSource = dtDtl;
                        gvDetail.DataBind();

                        grvTaxation.DataSource = dtCond;
                        grvTaxation.DataBind();

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

        protected void imgSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    string quotno = objMainClass.UpdateCromaQuotation(objMainClass.intCmpId, lblPONo.Text, Convert.ToDecimal(lblMaterialAmt.Text), Convert.ToDecimal(lblDiscountpercentage.Text), Convert.ToDecimal(lblDiscountAmt.Text), Convert.ToDecimal(lblTaxAmt.Text),
                        Convert.ToDecimal(lblPOTotalAmt.Text), gvDetail, grvTaxation, Convert.ToInt32(Session["USERID"]), Convert.ToInt32(rblPrice.SelectedValue));
                    if (quotno != null && quotno != "" && quotno != string.Empty)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Quotation updated successfully.  Quotation No. : " + quotno + "\");$('.close').click(function(){window.location.href ='frmApproveQuotation.aspx' });", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Record not inserted. Please try again.!');$('.close').click(function(){window.location.href ='frmApproveQuotation.aspx' });", true);
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

        public void GetData()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dtData = new DataTable();
                    dtData = objMainClass.GetRateCard(objMainClass.intCmpId, txtJobID.Text, txtSerialNo.Text, txtItemCode.Text, ddlBrand.SelectedIndex > 0 ? ddlBrand.SelectedValue : "", ddlCategory.SelectedIndex > 0 ? ddlCategory.SelectedValue : "", chkAll.Checked == true ? 0 : 1, ddlSize.SelectedIndex > 0 ? ddlSize.SelectedValue : "", "", "GETDATA");
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

        //protected void lblDiscountpercentage_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Session["USERID"] != null)
        //        {
        //            if (lblDiscountpercentage.Text != string.Empty && lblDiscountpercentage.Text != null && lblDiscountpercentage.Text != "")
        //            {
        //                //txtFinalAmount.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(txtTotalAmt.Text) - (Convert.ToDecimal((Convert.ToDecimal(txtTotalAmt.Text) * Convert.ToDecimal(lblDiscountpercentage.Text)) / 100))));
        //                //txtDiscountRs.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal((Convert.ToDecimal(txtTotalAmt.Text) * Convert.ToDecimal(lblDiscountpercentage.Text)) / 100)));
        //            }
        //            else
        //            {
        //                //lblDiscountpercentage.Text = "0";
        //                //txtFinalAmount.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(txtTotalAmt.Text) - (Convert.ToDecimal((Convert.ToDecimal(txtTotalAmt.Text) * Convert.ToDecimal(lblDiscountpercentage.Text)) / 100))));
        //                //txtDiscountRs.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal((Convert.ToDecimal(txtTotalAmt.Text) * Convert.ToDecimal(lblDiscountpercentage.Text)) / 100)));
        //            }
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

        //protected void lblDiscountAmt_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Session["USERID"] != null)
        //        {

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
    }
}