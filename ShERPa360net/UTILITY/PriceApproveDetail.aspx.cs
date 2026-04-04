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

namespace ShERPa360net.UTILITY
{
    public partial class PriceApproveDetail : System.Web.UI.Page
    {
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        bool Approverright = false;
        int ITEMGRP = 0;
        int ITEMSUBGRP       = 0;
        string ITEMGRPSHORTNAME = "";
        string ITEMSUBGRPSHORTNAME = "";
        string ITEMGRPNAME = "";
        string ITEMSUBGRPNAME = "";
        DataRow[] rowItemd    = null;
        Decimal   maxItemcode = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtFromDate.Text = (objMainClass.indianTime.Date.AddDays(-30)).ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    txtToDate.Text   = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    if (Session["USERID"] != null)
                    {
                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menutabApproverid.Value, "");
                        Approverright = FormRights.bView;
                        if (!Approverright)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }

                        if (FormRights.bAdd == false)
                        {
                            btnSaveAll.Enabled = false;
                        }

                        BindPageDropDown();
                        BindProductDetail();
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
                    BindProductDetail();
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
                BindProductDetail();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lnkExport_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = "Approval Report" + txtFromDate.Text + "-" + txtToDate.Text;
                string attachment = "attachment; filename=" + filename + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vdn.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gvProduct.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public void ShowHideColumn()
        {
            try
            {
                gvProduct.Columns[0].Visible = false;
                gvProduct.Columns[2].Visible = false;

                gvProduct.Columns[1].Visible = true;
                gvProduct.Columns[5].Visible = true;
                gvProduct.Columns[6].Visible = true;

                //Make Model Detail
                gvProduct.Columns[7].Visible = true;
                gvProduct.Columns[8].Visible = true;
                gvProduct.Columns[9].Visible = true;
                gvProduct.Columns[10].Visible = true;
                gvProduct.Columns[11].Visible = true;
                gvProduct.Columns[12].Visible = true;

                //VendorName,Grade,EntryDate 
                gvProduct.Columns[13].Visible = true;
                gvProduct.Columns[14].Visible = true;
                gvProduct.Columns[15].Visible = true;
                gvProduct.Columns[16].Visible = true;

                stProductDetail.InnerHtml = "<span class=\"fa fa-file\"></span>&nbsp;For Product Approve(APPROVED) Action";

                if (FormRights.bAdd == false)
                {
                    gvProduct.Columns[1].Visible = false;
                }
                else
                {
                    gvProduct.Columns[1].Visible = true;
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
                    objBindDDL.FillMobexSellerVendor(ddlVendor);
                    ddlVendor.SelectedValue = "0";
                    //objBindDDL.FillTaTaSkyReqDropDown(ddlEngineer, "", "EMPLOYEE", "Search");
                    //ddlEngineer.SelectedValue = "0";

                    //objBindDDL.FillTaTaSkyReqDropDown(ddlModel, "MODELS", "REQUESTDROPDOWN", "Search");
                    //ddlModel.SelectedValue = "0";

                    //objBindDDL.FillTaTaSkyReqDropDown(ddlCondition, "CONDITION", "REQUESTDROPDOWN", "Search");
                    //ddlCondition.SelectedValue = "0";

                    //objBindDDL.FillTaTaSkyReqDropDown(ddlRepair, "REPAIR", "REQUESTDROPDOWN", "Search");
                    //ddlRepair.SelectedValue = "0";
                    //ddlStatus.SelectedValue = "WORKING";
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

        public void BindProductDetail()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    int status = 0;
                    status = Convert.ToInt32(PRODUCTSTATUS.TESTED);

                    gvProduct.DataSource = null;
                    gvProduct.DataBind();

                    DataTable dt = new DataTable();
                    dt = objMainClass.GetProductEntryDetail(txtFromDate.Text, txtToDate.Text, (ddlVendor.SelectedValue == "0" ? "" : ddlVendor.SelectedItem.Text), status, 0,0,"");
                    gvProduct.DataSource = dt;
                    gvProduct.DataBind();
                    lgrecordcount.InnerText = "Records : " + dt.Rows.Count.ToString();
                    ShowHideColumn();
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
                    txtFromDate.Text        = (objMainClass.indianTime.Date.AddDays(-300)).ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    txtToDate.Text          = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    ddlVendor.SelectedValue = "0";
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

        public override void VerifyRenderingInServerForm(Control control)
        {

        }


        #endregion

        protected void btnQc_Click(object sender, EventArgs e)
        {
            try
            {
                ResetQcDetail();
                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                hdprimarykey.Value = ((HiddenField)grdrow.FindControl("hdID")).Value;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                ddlQcResult.Focus();
                //if (((DropDownList)grdrow.FindControl("ddlQcResult")).SelectedValue == "SELECT")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please select the Qc Result to update.');", true);
                //}
                //else
                //{
                //    objMainClass.UpdateQcDetail(((DropDownList)grdrow.FindControl("ddlQcResult")).SelectedValue, Session["USERID"].ToString(),
                //    Convert.ToInt32(((HiddenField)grdrow.FindControl("hdID")).Value));
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Qc update Successfully." + "\");", true);
                //    BindProductDetail();
                //}

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnSaveQc_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = Convert.ToInt32(hdprimarykey.Value);
                objMainClass.UpdateQcDetail(ddlQcResult.SelectedValue, ddlQcGrade.SelectedValue, txtReason.Text,
                    Session["USERID"].ToString(), ID);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Qc update Successfully." + "\");", true);
                BindProductDetail();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnResetQc_Click(object sender, EventArgs e)
        {
            try
            {
                ResetQcDetail();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grdrow      = (GridViewRow)((LinkButton)sender).NamingContainer;
                string purrate          = ((TextBox)grdrow.FindControl("txtMobexRate")).Text;
                //string purqty = ((TextBox)grdrow.FindControl("txtPurchaseqty")).Text;
                string newrate          = ((TextBox)grdrow.FindControl("txtNewRate")).Text;
                string lockamt          = ((TextBox)grdrow.FindControl("txtFinalListingAmount")).Text;

                string recomendedrate   = ((Label)grdrow.FindControl("lblPurPrice")).Text;
                Decimal dcconvertedrate = 0;
                Decimal.TryParse(recomendedrate, out dcconvertedrate);
                int ID               = Convert.ToInt32(((HiddenField)grdrow.FindControl("hdID")).Value);
                if (purrate.Length == 0 || newrate.Length == 0 || dcconvertedrate == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Basic Pur Rate and Mobex Rate and Purchase Qty and  is required for Approved Listed.');", true);
                }
                else
                {
                    //string purchaseper  = ((Label)grdrow.FindControl("lblPercentageValue")).Text;
                    Double fkAmt = 0, amzAmt = 0, webAmt = 0, dcpurvalue = 0, dcnewrate = 0, dcpurchaseper = 0, fkper = 0, amzper = 0, webper = 0,dcrecomendedrate = 0
                    , PURFKAMT = 0, PURAMZAMT = 0, PURWEBAMT = 0, PURFKPER = 0, PURAMZPER = 0, PURWEBPER = 0, PURCHASEPERONVENDORPRICE = 0, dcFinalListing = 0;
                    Double.TryParse(newrate, out dcnewrate);  
                    Double.TryParse(purrate, out dcpurvalue);
                    Double.TryParse(lockamt, out dcFinalListing);
                    dcrecomendedrate = GeneralFunctionality.GetAGradeSuggestAmount(recomendedrate, (grdrow.FindControl("hdVendorGrade") as HiddenField).Value);
                    //Double.TryParse(recomendedrate, out dcrecomendedrate);
                    int islistedfk = 0, islistedamz = 0, islistedweb = 0;
                    int ispurlistedfk = 0, ispurlistedamz = 0, ispurlistedweb = 0;
                    //Calculate PlatForm Selling Amount 
                    if (dcpurvalue > 0)
                    {
                            //Our New Logic 21-10-2022
                            if (dcpurvalue <= dcFinalListing)
                            {
                                ispurlistedamz = 1;
                                ispurlistedweb = 1;
                            }


                            fkAmt = (((dcpurvalue)) * (1.234));
                            fkAmt = (Math.Floor(fkAmt * Math.Pow(10, -2)) / Math.Pow(10, -2));

                            if (fkAmt <= dcpurvalue)
                            {
                                fkAmt = dcpurvalue + (Math.Round(((dcpurvalue * 8) / 100), 2));
                                fkAmt = (Math.Floor(fkAmt * Math.Pow(10, -2)) / Math.Pow(10, -2));
                            }

                            //Flipkart Calculate Amount on Vendor Price 
                            PURFKAMT = (((dcpurvalue)) * (1.234));
                            PURFKAMT = (Math.Floor(PURFKAMT * Math.Pow(10, -2)) / Math.Pow(10, -2));
                            //Flipkart Calculate Amount on Vendor Price 

                            amzAmt = (((dcpurvalue)) * (1.175));
                            amzAmt = (Math.Floor(amzAmt * Math.Pow(10, -2)) / Math.Pow(10, -2));

                            if (amzAmt <= dcpurvalue)
                            {
                                amzAmt = dcpurvalue + (Math.Round(((dcpurvalue * 8) / 100), 2));
                                amzAmt = (Math.Floor(amzAmt * Math.Pow(10, -2)) / Math.Pow(10, -2));
                            }

                            //Amazon Calculate Amount on Vendor Price 
                            PURAMZAMT = (((dcpurvalue)) * (1.175));
                            PURAMZAMT = (Math.Floor(PURAMZAMT * Math.Pow(10, -2)) / Math.Pow(10, -2));
                            //Amazon Calculate Amount on Vendor Price 

                            webAmt = (((amzAmt)) - (350));

                            //Web Calculate Amount on Vendor Price 
                            PURWEBAMT = (PURAMZAMT) - (350);
                            //Web Calculate Amount on Vendor Price

                            //if (dcpurvalue >= 1 && dcpurvalue <= 7000)
                            //{
                            //    webAmt = dcpurvalue + ((dcpurvalue * 15) / 100);
                            //}
                            //else if (dcpurvalue >= 7001 && dcpurvalue <= 15000)
                            //{
                            //    webAmt = dcpurvalue + ((dcpurvalue * 10) / 100);
                            //}
                            //else if (dcpurvalue >= 15001 && dcpurvalue <= 30000)
                            //{
                            //    webAmt = dcpurvalue + ((dcpurvalue * 8) / 100);
                            //}
                            //else if (dcpurvalue >= 30001 && dcpurvalue <= 45000)
                            //{
                            //    webAmt = dcpurvalue + ((dcpurvalue * 5) / 100);
                            //}
                            //else if (dcpurvalue >= 45001 && dcpurvalue <= 60000)
                            //{
                            //    webAmt = dcpurvalue + ((dcpurvalue * 4) / 100);
                            //}
                            //else
                            //{
                            //    webAmt = dcpurvalue + ((dcpurvalue * 3) / 100);
                            //}
                    }

                    //Calculate PlatForm Per Amount 
                    if (fkAmt != 0)
                    {
                        fkper = Math.Round((((fkAmt * 100)) / dcnewrate), 0);
                    }

                    //FK PlatForm Per Amount on Purchase Price 
                    if (PURFKAMT != 0)
                    {
                        PURFKPER = Math.Round((((PURFKAMT * 100)) / dcnewrate), 0);
                    }
                    //FK PlatForm Per Amount on Purchase Price 

                    if (amzAmt != 0)
                    {
                        amzper = Math.Round((((amzAmt * 100)) / dcnewrate), 0);
                    }

                    //Amazon PlatForm Per Amount on Purchase Price 
                    if (PURAMZAMT != 0)
                    {
                        PURAMZPER = Math.Round((((PURAMZAMT * 100)) / dcnewrate), 0);
                    }
                    //Amazon PlatForm Per Amount on Purchase Price 

                    if (webAmt != 0)
                    {
                        webper = Math.Round((((webAmt * 100)) / dcnewrate), 0);
                    }

                    //Web PlatForm Per Amount on Purchase Price 
                    if (PURWEBAMT != 0)
                    {
                        PURWEBPER = Math.Round((((PURWEBAMT * 100)) / dcnewrate), 0);
                    }
                    //Web PlatForm Per Amount on Purchase Price 

                    if (dcrecomendedrate > 0 && dcnewrate > 0)
                    {
                        dcpurchaseper = Math.Round(((dcpurvalue * 100)) / (dcnewrate), 0);
                    }

                    if (dcpurvalue > 0 && dcnewrate > 0)
                    {
                        PURCHASEPERONVENDORPRICE = Math.Round(((dcpurvalue * 100)) / (dcnewrate), 0);
                    }

                    //Calculate PlatForm Listed Detail 
                    islistedfk  = fkper >= 90 ? 0 : 1;
                    islistedamz = amzper >= 90 ? 0 : 1;
                    islistedweb = webper >= 90 ? 0 : 1;

                    //Calculate Purchase PlatForm Listed Detail 
                    ispurlistedfk   = PURFKPER  >= 90 ? 0 : 1;
                    //ispurlistedamz  = PURAMZPER >= 90 ? 0 : 1;
                    //ispurlistedweb  = PURWEBPER >= 90 ? 0 : 1;

                    //Extra Condition Code
                    string make         = (grdrow.FindControl("lblMake") as Label).Text;
                    string mobexgrade   = (grdrow.FindControl("lblMobexGrade") as Label).Text;
                    string vendorgrade  = (grdrow.FindControl("hdVendorGrade") as HiddenField).Value;

                    //for Amazon
                    if (make.ToUpper() == "APPLE")
                    {
                        islistedamz = 0;
                        ispurlistedamz = 0;
                    }
                    else
                    {
                        // mobexgrade == "B" || vendorgrade == "B" ||
                        //if ((mobexgrade == "C") || (vendorgrade == "C"))
                        //{
                        //    islistedamz = 0;
                        //    ispurlistedamz = 0;
                        //}
                        //else
                        //{
                            var dtAmazone = objMainClass.GetPlatFormNotListed((PLATFORM.Amazon.ToString()), make, (grdrow.FindControl("lblModel") as Label).Text, (grdrow.FindControl("lblVendorGrade") as Label).Text, Convert.ToInt32(Session["USERID"].ToString()));
                            if (dtAmazone.Rows.Count > 0)
                            {
                                islistedamz = 0;
                                ispurlistedamz = 0;
                            }
                        //}
                    }

                    //for Amazon

                    //for Web
                    //mobexgrade == "B" || vendorgrade == "B" ||
                    //if ((mobexgrade == "C") || (vendorgrade == "C"))
                    //{
                    //    islistedweb = 0;
                    //    ispurlistedweb = 0;
                    //}
                    //for Web


                    //for Flipcart
                    if (fkAmt > 45000)//|| make.ToUpper() == "APPLE"
                    {
                        islistedfk = 0;
                        ispurlistedfk = 0;
                    }
                    //for Flipcart

                    //Extra Condition Code

                    // Basic Purchase Price Rejection Condition
                    //if (dcpurvalue > dcrecomendedrate)
                    //{
                    //    islistedfk = 0;
                    //    islistedamz = 0;
                    //    islistedweb = 0;
                    //}
                    // Basic Purchase Price Rejection Condition

                    //Get Item  Code 
                    string itemcode = "";
                    string itemdesc = "";
                    //ispurlistedfk == 1 ||
                    ITEMGRP = Convert.ToInt32((grdrow.FindControl("hdItemGrpId") as HiddenField).Value);
                    ITEMSUBGRP = Convert.ToInt32((grdrow.FindControl("hdItemSubGrpId") as HiddenField).Value);

                    ITEMGRPNAME = (grdrow.FindControl("hdItemGrpName") as HiddenField).Value;
                    ITEMSUBGRPNAME = (grdrow.FindControl("hdItemSubGrpName") as HiddenField).Value;

                    ITEMGRPSHORTNAME = (grdrow.FindControl("hdItemGrpShortName") as HiddenField).Value;
                    ITEMSUBGRPSHORTNAME = (grdrow.FindControl("hdItemSubGrpShortName") as HiddenField).Value;

                    //Our New Logic 21-10-2022
                    //if (dcpurvalue > dcFinalListing)
                    //{
                    //    ispurlistedamz = 0;
                    //    ispurlistedweb = 0;
                    //}
                    //else
                    //{
                    //    ispurlistedamz = 1;
                    //    ispurlistedweb = 1;
                    //}

                    if (ispurlistedamz == 1 || ispurlistedweb == 1)
                    {
                        if (ITEMGRP == 9 && ITEMSUBGRP == 168)
                        {
                            itemdesc = make + " " + (grdrow.FindControl("lblModel") as Label).Text + " "
                                    + (grdrow.FindControl("lblRam") as Label).Text + " "
                                    + (grdrow.FindControl("lblRom") as Label).Text + " "
                                    + (grdrow.FindControl("lblColor") as Label).Text + " MOBILE DEVICE (USED) (GRADE "
                                    + ((grdrow.FindControl("lblVendorGrade") as Label).Text == "C" ? (grdrow.FindControl("lblVendorGrade") as Label).Text : "A") + ")"; //(grdrow.FindControl("lblVendorGrade") as Label).Text
                        }
                        else
                        {
                            itemdesc = make + " " + (grdrow.FindControl("lblModel") as Label).Text + " " + ITEMSUBGRPNAME
                                    + " (" + (grdrow.FindControl("lblColor") as Label).Text + ") (GRADE "
                                    + "A" + ")"; //(grdrow.FindControl("lblVendorGrade") as Label).Text 
                        }
                        itemcode = GetItemCode(itemdesc, make, (grdrow.FindControl("lblModel") as Label).Text);
                    }

                    //Get Item  Code 
                    //ispurlistedfk == 1 ||
                    //ispurlistedfk == 1 ||
                    objMainClass.UpdatePurchaseDetail("1", Convert.ToDecimal(purrate),
                    ((ispurlistedamz == 1 || ispurlistedweb == 1) ? "APPROVED" : "REJECTED"),
                    Convert.ToInt32(Session["USERID"].ToString()),
                    (( ispurlistedamz == 1 || ispurlistedweb == 1) ? 11235 : 11233), ID, dcnewrate, PURCHASEPERONVENDORPRICE, islistedfk
                    , islistedamz, islistedweb, fkAmt, amzAmt, webAmt, fkper, amzper, webper
                    , (( ispurlistedamz == 1 || ispurlistedweb == 1) ? "" : "Price Constraint"), itemcode, dcrecomendedrate
                    , PURFKAMT, PURAMZAMT, PURWEBAMT, PURFKPER, PURAMZPER, PURWEBPER, dcpurchaseper, 
                      ispurlistedfk, ispurlistedamz, ispurlistedweb,dcFinalListing);
                        
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Product Approved Successfully." + "\");", true);
                    BindProductDetail();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grdrow      = (GridViewRow)((LinkButton)sender).NamingContainer;
                string purrate          = ((TextBox)grdrow.FindControl("txtMobexRate")).Text;
                //string purqty = ((TextBox)grdrow.FindControl("txtPurchaseqty")).Text;
                string newrate          = ((TextBox)grdrow.FindControl("txtNewRate")).Text;
                string recomendedrate   = ((Label)grdrow.FindControl("lblPurPrice")).Text;
                string lockamt          = ((TextBox)grdrow.FindControl("txtFinalListingAmount")).Text;
                Decimal dcconvertedrate = 0;
                Decimal.TryParse(recomendedrate, out dcconvertedrate);

                if (((TextBox)grdrow.FindControl("txtRejectReason")).Text.Length == 0 || (purrate.Length == 0 || newrate.Length == 0 || dcconvertedrate == 0))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please enter the Reject Reason,Mobex Rate,New Rate for Reject.');", true);
                }
                else
                {
                    int ID = Convert.ToInt32(((HiddenField)grdrow.FindControl("hdID")).Value);
                    Double fkAmt = 0, amzAmt = 0, webAmt = 0, dcpurvalue = 0, dcnewrate = 0, dcpurchaseper = 0, fkper = 0, amzper = 0, webper = 0, dcrecomendedrate = 0
                    ,PURFKAMT = 0, PURAMZAMT = 0, PURWEBAMT = 0, PURFKPER = 0, PURAMZPER = 0, PURWEBPER = 0, PURCHASEPERONVENDORPRICE = 0, dcFinalListing = 0; ;
                    Double.TryParse(newrate, out dcnewrate);
                    Double.TryParse(purrate, out dcpurvalue);
                    Double.TryParse(lockamt, out dcFinalListing);
                    dcrecomendedrate = GeneralFunctionality.GetAGradeSuggestAmount(recomendedrate, (grdrow.FindControl("hdVendorGrade") as HiddenField).Value);
                    //Double.TryParse(recomendedrate, out dcrecomendedrate);

                    //Calculate PlatForm Selling Amount 
                    if (dcpurvalue > 0)
                    {
                        fkAmt = (((dcpurvalue)) * (1.234));
                        fkAmt = (Math.Floor(fkAmt * Math.Pow(10, -2)) / Math.Pow(10, -2));

                        if (fkAmt <= dcpurvalue)
                        {
                            fkAmt = dcpurvalue + (Math.Round(((dcpurvalue * 8) / 100), 2));
                            fkAmt = (Math.Floor(fkAmt * Math.Pow(10, -2)) / Math.Pow(10, -2));
                        }

                        //Flipkart Calculate Amount on Vendor Price 
                        PURFKAMT = (((dcpurvalue)) * (1.234));
                        PURFKAMT = (Math.Floor(PURFKAMT * Math.Pow(10, -2)) / Math.Pow(10, -2));
                        //Flipkart Calculate Amount on Vendor Price 

                        amzAmt = (((dcpurvalue)) * (1.175));
                        amzAmt = (Math.Floor(amzAmt * Math.Pow(10, -2)) / Math.Pow(10, -2));

                        if (amzAmt <= dcpurvalue)
                        {
                            amzAmt = dcpurvalue + (Math.Round(((dcpurvalue * 8) / 100), 2));
                            amzAmt = (Math.Floor(amzAmt * Math.Pow(10, -2)) / Math.Pow(10, -2));
                        }

                        //Amazon Calculate Amount on Vendor Price 
                        PURAMZAMT = (((dcpurvalue)) * (1.175));
                        PURAMZAMT = (Math.Floor(PURAMZAMT * Math.Pow(10, -2)) / Math.Pow(10, -2));
                        //Amazon Calculate Amount on Vendor Price 

                        webAmt = (((amzAmt)) - (350));

                        //Web Calculate Amount on Vendor Price 
                        PURWEBAMT = (PURAMZAMT) - (350);
                        //Web Calculate Amount on Vendor Price

                        //if (dcpurvalue >= 1 && dcpurvalue <= 7000)
                        //{
                        //    webAmt = dcpurvalue + ((dcpurvalue * 15) / 100);
                        //}
                        //else if (dcpurvalue >= 7001 && dcpurvalue <= 15000)
                        //{
                        //    webAmt = dcpurvalue + ((dcpurvalue * 10) / 100);
                        //}
                        //else if (dcpurvalue >= 15001 && dcpurvalue <= 30000)
                        //{
                        //    webAmt = dcpurvalue + ((dcpurvalue * 8) / 100);
                        //}
                        //else if (dcpurvalue >= 30001 && dcpurvalue <= 45000)
                        //{
                        //    webAmt = dcpurvalue + ((dcpurvalue * 5) / 100);
                        //}
                        //else if (dcpurvalue >= 45001 && dcpurvalue <= 60000)
                        //{
                        //    webAmt = dcpurvalue + ((dcpurvalue * 4) / 100);
                        //}
                        //else
                        //{
                        //    webAmt = dcpurvalue + ((dcpurvalue * 3) / 100);
                        //}
                    }

                    //Calculate PlatForm Per Amount 
                    if (fkAmt != 0)
                    {
                        fkper = Math.Round((((fkAmt * 100)) / dcnewrate), 2);
                    }

                    //FK PlatForm Per Amount on Purchase Price 
                    if (PURFKAMT != 0)
                    {
                        PURFKPER = Math.Round((((PURFKAMT * 100)) / dcnewrate), 0);
                    }
                    //FK PlatForm Per Amount on Purchase Price 

                    if (amzAmt != 0)
                    {
                        amzper = Math.Round((((amzAmt * 100)) / dcnewrate), 2);
                    }

                    //Amazon PlatForm Per Amount on Purchase Price 
                    if (PURAMZAMT != 0)
                    {
                        PURAMZPER = Math.Round((((PURAMZAMT * 100)) / dcnewrate), 0);
                    }
                    //Amazon PlatForm Per Amount on Purchase Price 



                    if (webAmt != 0)
                    {
                        webper = Math.Round((((webAmt * 100)) / dcnewrate), 2);
                    }

                    //Web PlatForm Per Amount on Purchase Price 
                    if (PURWEBAMT != 0)
                    {
                        PURWEBPER = Math.Round((((PURWEBAMT * 100)) / dcnewrate), 0);
                    }
                    //Web PlatForm Per Amount on Purchase Price

                    if (dcrecomendedrate > 0 && dcnewrate > 0)
                    {
                        dcpurchaseper = Math.Round(((dcpurvalue * 100)) / (dcnewrate), 2);
                    }

                    if (dcpurvalue > 0 && dcnewrate > 0)
                    {
                        PURCHASEPERONVENDORPRICE = Math.Round(((dcpurvalue * 100)) / (dcnewrate), 0);
                    }

                    objMainClass.UpdatePurchaseDetail("1", Convert.ToDecimal(purrate), "REJECTED",
                    Convert.ToInt32(Session["USERID"].ToString()), 11233, ID, dcnewrate, PURCHASEPERONVENDORPRICE, 0
                    , 0, 0, fkAmt, amzAmt, webAmt, fkper, amzper, webper, "","", dcrecomendedrate
                    , PURFKAMT, PURAMZAMT, PURWEBAMT, PURFKPER, PURAMZPER, PURWEBPER, dcpurchaseper, 0, 0, 0,dcFinalListing);

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Product Reject Successfully." + "\");", true);
                    BindProductDetail();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public void ResetQcDetail()
        {
            try
            {
                ddlQcResult.SelectedIndex = -1;
                ddlQcGrade.SelectedIndex = -1;
                txtReason.Text = string.Empty;
                hdprimarykey.Value = "0";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }



        protected void btnListed_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                int ID = Convert.ToInt32(((HiddenField)grdrow.FindControl("hdID")).Value);
                objMainClass.UpdateListedDetail(ID);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Product Listed Successfully." + "\");", true);
                BindProductDetail();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    (e.Row.FindControl("txtMobexRate") as TextBox).Attributes.Add("onkeyup", "CalculatePurchasePercentageAmt(" +
                    (e.Row.FindControl("lblVendorRate") as Label).Text
                    + ",'" + (e.Row.FindControl("txtMobexRate") as TextBox).ClientID
                    + "','" + (e.Row.FindControl("txtNewRate") as TextBox).ClientID
                    + "','" + (e.Row.FindControl("txtFKRate") as TextBox).ClientID
                    + "','" + (e.Row.FindControl("txtAmzRate") as TextBox).ClientID
                    + "','" + (e.Row.FindControl("txtWebRate") as TextBox).ClientID
                    + "','" + (e.Row.FindControl("lblPurcPerValue") as Label).ClientID
                    + "','" + (e.Row.FindControl("lblFkPerValue") as Label).ClientID
                    + "','" + (e.Row.FindControl("lblAmzPerValue") as Label).ClientID
                    + "','" + (e.Row.FindControl("lblWebPerValue") as Label).ClientID
                    + "','" + (e.Row.FindControl("lblMake") as Label).Text
                    + "','" + (e.Row.FindControl("lblMobexGrade") as Label).Text
                    + "','" + (e.Row.FindControl("hdVendorGrade") as HiddenField).Value
                    + "','" + (e.Row.FindControl("lblPurPrice") as Label).Text + "');");


                    (e.Row.FindControl("txtNewRate") as TextBox).Attributes.Add("onkeyup", "CalculatePurchasePercentageAmt(" +
                    (e.Row.FindControl("lblVendorRate") as Label).Text
                    + ",'" + (e.Row.FindControl("txtMobexRate") as TextBox).ClientID
                    + "','" + (e.Row.FindControl("txtNewRate") as TextBox).ClientID
                    + "','" + (e.Row.FindControl("txtFKRate") as TextBox).ClientID
                    + "','" + (e.Row.FindControl("txtAmzRate") as TextBox).ClientID
                    + "','" + (e.Row.FindControl("txtWebRate") as TextBox).ClientID
                    + "','" + (e.Row.FindControl("lblPurcPerValue") as Label).ClientID
                    + "','" + (e.Row.FindControl("lblFkPerValue") as Label).ClientID
                    + "','" + (e.Row.FindControl("lblAmzPerValue") as Label).ClientID
                    + "','" + (e.Row.FindControl("lblWebPerValue") as Label).ClientID
                    + "','" + (e.Row.FindControl("lblMake") as Label).Text
                    + "','" + (e.Row.FindControl("hdVendorGrade") as HiddenField).Value
                    + "','" + (e.Row.FindControl("lblPurPrice") as Label).Text + "');");

                    (e.Row.FindControl("txtNewRate") as TextBox).Text   = (e.Row.FindControl("lblNewPrice") as Label).Text;
                    (e.Row.FindControl("txtMobexRate") as TextBox).Text = (e.Row.FindControl("lblVendorRate") as Label).Text;
                    //Initiate Value 
                    string chknewrate = (e.Row.FindControl("txtNewRate") as TextBox).Text;
                    Double dcchknewrate = 0;
                    Double.TryParse(chknewrate, out dcchknewrate);
                    string recomended = (e.Row.FindControl("lblPurPrice") as Label).Text;
                    Decimal dcprerecomenededrate = 0;
                    Decimal.TryParse(recomended, out dcprerecomenededrate);
                    //Convert.ToDecimal((e.Row.FindControl("lblNewPrice") as Label).Text)
                    if (dcchknewrate != 0 && dcprerecomenededrate !=0)
                    {
                        //Calculate FK,AMZ,WEB Amount
                        string purrate    = (e.Row.FindControl("lblVendorRate") as Label).Text;
                        string newrate    = (e.Row.FindControl("txtNewRate") as TextBox).Text;
                        string lockamt    = (e.Row.FindControl("txtFinalListingAmount") as TextBox).Text; 
                        Double fkAmt = 0, amzAmt = 0, webAmt = 0, dcpurvalue = 0, dcnewrate = 0, dcpurchaseper = 0, fkper = 0, amzper = 0, webper = 0, dcrecomendedrate = 0
                        , PURFKAMT = 0, PURAMZAMT = 0, PURWEBAMT = 0, PURFKPER = 0, PURAMZPER = 0, PURWEBPER = 0, PURCHASEPERONVENDORPRICE = 0
                        , dcFinalListing = 0;
                        Double.TryParse(newrate, out dcnewrate);
                        Double.TryParse(purrate, out dcpurvalue);
                        Double.TryParse(lockamt, out dcFinalListing);
                        dcrecomendedrate = GeneralFunctionality.GetAGradeSuggestAmount(recomended, (e.Row.FindControl("hdVendorGrade") as HiddenField).Value);
                        //Double.TryParse(recomended, out dcrecomendedrate);
                        int islistedfk = 0, islistedamz = 0, islistedweb = 0;
                        int ispurlistedfk = 0, ispurlistedamz = 0, ispurlistedweb = 0;

                        //Calculate PlatForm Selling Amount 
                        if (dcpurvalue > 0)
                        {
                                //Our New Logic 21-10-2022
                                if (dcpurvalue <= dcFinalListing)
                                {
                                    ispurlistedamz = 1;
                                    ispurlistedweb = 1;
                                }

                                fkAmt = (((dcpurvalue)) * (1.234));
                                fkAmt = (Math.Floor(fkAmt * Math.Pow(10, -2)) / Math.Pow(10, -2));

                                if (fkAmt <= dcpurvalue)
                                {
                                    fkAmt = dcpurvalue + (Math.Round(((dcpurvalue * 8) / 100), 2));
                                    fkAmt = (Math.Floor(fkAmt * Math.Pow(10, -2)) / Math.Pow(10, -2));
                                }

                                //Flipkart Calculate Amount on Vendor Price 
                                PURFKAMT = (((dcpurvalue)) * (1.234));
                                PURFKAMT = (Math.Floor(PURFKAMT * Math.Pow(10, -2)) / Math.Pow(10, -2));
                                //Flipkart Calculate Amount on Vendor Price 

                                amzAmt = (((dcpurvalue)) * (1.175));
                                amzAmt = (Math.Floor(amzAmt * Math.Pow(10, -2)) / Math.Pow(10, -2));

                                if (amzAmt <= dcpurvalue)
                                {
                                    amzAmt = dcpurvalue + (Math.Round(((dcpurvalue * 8) / 100), 2));
                                    amzAmt = (Math.Floor(amzAmt * Math.Pow(10, -2)) / Math.Pow(10, -2));
                                }

                                //Amazon Calculate Amount on Vendor Price 
                                PURAMZAMT = (((dcpurvalue)) * (1.175));
                                PURAMZAMT = (Math.Floor(PURAMZAMT * Math.Pow(10, -2)) / Math.Pow(10, -2));
                                //Amazon Calculate Amount on Vendor Price

                                webAmt = (((amzAmt)) - (350));
                                //webAmt = (Math.Floor(webAmt * Math.Pow(10, -2)) / Math.Pow(10, -2));

                                //Web Calculate Amount on Vendor Price 
                                PURWEBAMT = (PURAMZAMT) - (350);
                                //Web Calculate Amount on Vendor Price

                                //if (dcpurvalue >= 1 && dcpurvalue <= 7000)
                                //{
                                //    webAmt = dcpurvalue + ((dcpurvalue * 15) / 100);
                                //}
                                //else if (dcpurvalue >= 7001 && dcpurvalue <= 15000)
                                //{
                                //    webAmt = dcpurvalue + ((dcpurvalue * 10) / 100);
                                //}
                                //else if (dcpurvalue >= 15001 && dcpurvalue <= 30000)
                                //{
                                //    webAmt = dcpurvalue + ((dcpurvalue * 8) / 100);
                                //}
                                //else if (dcpurvalue >= 30001 && dcpurvalue <= 45000)
                                //{
                                //    webAmt = dcpurvalue + ((dcpurvalue * 5) / 100);
                                //}
                                //else if (dcpurvalue >= 45001 && dcpurvalue <= 60000)
                                //{
                                //    webAmt = dcpurvalue + ((dcpurvalue * 4) / 100);
                                //}
                                //else
                                //{
                                //    webAmt = dcpurvalue + ((dcpurvalue * 3) / 100);
                                //}
                        }

                        //Calculate PlatForm Per Amount 
                        if (fkAmt != 0)
                        {
                            fkper = Math.Round((((fkAmt * 100)) / dcnewrate), 0);
                        }

                        //FK PlatForm Per Amount on Purchase Price 
                        if (PURFKAMT != 0)
                        {
                            PURFKPER = Math.Round((((PURFKAMT * 100)) / dcnewrate), 0);
                        }
                        //FK PlatForm Per Amount on Purchase Price 

                        if (amzAmt != 0)
                        {
                            amzper = Math.Round((((amzAmt * 100)) / dcnewrate), 0);
                        }

                        //Amazon PlatForm Per Amount on Purchase Price 
                        if (PURAMZAMT != 0)
                        {
                            PURAMZPER = Math.Round((((PURAMZAMT * 100)) / dcnewrate), 0);
                        }
                        //Amazon PlatForm Per Amount on Purchase Price 

                        if (webAmt != 0)
                        {
                            webper = Math.Round((((webAmt * 100)) / dcnewrate), 0);
                        }

                        //Web PlatForm Per Amount on Purchase Price 
                        if (PURWEBAMT != 0)
                        {
                            PURWEBPER = Math.Round((((PURWEBAMT * 100)) / dcnewrate), 0);
                        }
                        //Web PlatForm Per Amount on Purchase Price

                        if (dcrecomendedrate > 0 && dcnewrate > 0)
                        {
                            dcpurchaseper = Math.Round(((dcpurvalue * 100)) / (dcnewrate), 0);
                        }

                        if (dcpurvalue > 0 && dcnewrate > 0)
                        {
                            PURCHASEPERONVENDORPRICE = Math.Round(((dcpurvalue * 100)) / (dcnewrate), 0);
                        }

                        //Calculate PlatForm Listed Detail 
                        islistedfk = fkper >= 90 ? 0 : 1;
                        islistedamz = amzper >= 90 ? 0 : 1;
                        islistedweb = webper >= 90 ? 0 : 1;

                        //Calculate Purchase PlatForm Listed Detail 
                        ispurlistedfk = PURFKPER >= 90 ? 0 : 1;
                        //ispurlistedamz = PURAMZPER >= 90 ? 0 : 1;
                        //ispurlistedweb = PURWEBPER >= 90 ? 0 : 1;


                        //Extra Condition Code
                        string make = (e.Row.FindControl("lblMake") as Label).Text;
                        string mobexgrade = (e.Row.FindControl("lblMobexGrade") as Label).Text;
                        string vendorgrade = (e.Row.FindControl("hdVendorGrade") as HiddenField).Value;

                        //for Amazon
                        if (make.ToUpper() == "APPLE")
                        {
                            islistedamz = 0;
                            ispurlistedamz = 0;
                        }
                        //else
                        //{
                        //    //mobexgrade == "B" || vendorgrade == "B" ||
                        //    if (( mobexgrade == "C") || ( vendorgrade == "C"))
                        //    {
                        //        islistedamz = 0;
                        //        ispurlistedamz = 0;
                        //    }
                        //}

                        //for Amazon

                        //mobexgrade == "B" || vendorgrade == "B" ||

                        //for Web
                        //if ((mobexgrade == "C") || (vendorgrade == "C"))
                        //{
                        //    islistedweb = 0;
                        //    ispurlistedweb = 0;
                        //}
                        //for Web


                        //for Flipcart
                        if (fkAmt > 45000) //|| make.ToUpper() == "APPLE"
                        {
                            islistedfk = 0;
                            ispurlistedfk = 0;
                        }
                        //for Flipcart

                        //Extra Condition Code

                        // Basic Purchase Price Rejection Condition
                        //if (dcpurvalue > dcrecomendedrate)
                        //{
                        //    islistedfk  = 0;
                        //    islistedamz = 0;
                        //    islistedweb = 0;
                        //}
                        // Basic Purchase Price Rejection Condition

                        //Our New Logic 21-10-2022
                        //if (dcpurvalue > dcFinalListing)
                        //{
                        //    ispurlistedamz = 0;
                        //    ispurlistedweb = 0;
                        //}
                        //else
                        //{
                        //    ispurlistedamz = 1;
                        //    ispurlistedweb = 1;
                        //}


                        (e.Row.FindControl("txtFKRate") as TextBox).Text  = fkAmt.ToString();
                        (e.Row.FindControl("txtAmzRate") as TextBox).Text = amzAmt.ToString();
                        (e.Row.FindControl("txtWebRate") as TextBox).Text = webAmt.ToString();
                        (e.Row.FindControl("lblPurcPerValue") as Label).Text = "PUR:" + dcpurchaseper.ToString() + "%";
                        (e.Row.FindControl("lblFkPerValue") as Label).Text = "FK:" + fkper.ToString() + "%";
                        (e.Row.FindControl("lblAmzPerValue") as Label).Text = "AMZ:" + amzper.ToString() + "%";
                        (e.Row.FindControl("lblWebPerValue") as Label).Text = "WEB:" + webper.ToString() + "%";

                        if (ispurlistedfk == 1)
                        {
                            (e.Row.FindControl("lblFkPerValue") as Label).ForeColor = System.Drawing.Color.Black;
                            (e.Row.FindControl("lblFkPerValue") as Label).Attributes.Add("color", "black");
                        }
                        else
                        {
                            (e.Row.FindControl("lblFkPerValue") as Label).ForeColor = System.Drawing.Color.Red;
                            (e.Row.FindControl("lblFkPerValue") as Label).Attributes.Add("color", "red");
                        }

                        if (ispurlistedamz == 1)
                        {
                            (e.Row.FindControl("lblAmzPerValue") as Label).ForeColor = System.Drawing.Color.Black;
                            (e.Row.FindControl("lblAmzPerValue") as Label).Attributes.Add("color", "black");
                        }
                        else
                        {
                            (e.Row.FindControl("lblAmzPerValue") as Label).ForeColor = System.Drawing.Color.Red;
                            (e.Row.FindControl("lblAmzPerValue") as Label).Attributes.Add("color", "red");
                        }

                        if (ispurlistedweb == 1)
                        {
                            (e.Row.FindControl("lblWebPerValue") as Label).ForeColor = System.Drawing.Color.Black;
                            (e.Row.FindControl("lblWebPerValue") as Label).Attributes.Add("color", "black");
                        }
                        else
                        {
                            (e.Row.FindControl("lblWebPerValue") as Label).ForeColor = System.Drawing.Color.Red;
                            (e.Row.FindControl("lblWebPerValue") as Label).Attributes.Add("color", "red");
                        }
                    }
                    //Calculate FK,AMZ,WEB Amount
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
                CheckedAll();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public void CheckedAll()
        {
            try
            {
                //if(chkSelectAll.Checked == true)
                //{
                //    for (int i = 0; i < gvProduct.Rows.Count; i++)
                //    {
                //        GridViewRow row = gvProduct.Rows[i];
                //        ((CheckBox)row.FindControl("chkSelection")).Checked = true;
                //    }
                //}
                //else
                //{
                //    for (int i = 0; i < gvProduct.Rows.Count; i++)
                //    {
                //        GridViewRow row = gvProduct.Rows[i];
                //        ((CheckBox)row.FindControl("chkSelection")).Checked = false;
                //    }
                //}

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }


        public int SelectedCheckBox()
        {
            int selectedcheckbox = 0;
            try
            {
                for (int i = 0; i < gvProduct.Rows.Count; i++)
                {
                    GridViewRow row = gvProduct.Rows[i];
                    if (((CheckBox)row.FindControl("chkSelection")).Checked == true)
                    {
                        selectedcheckbox = selectedcheckbox + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }

            return selectedcheckbox;
        }

        protected void btnSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                int totalrecord = 0, approved = 0, rejected = 0;
                Decimal actualnewrate = 0;
                Decimal actualmobexrate = 0;
                Decimal actualrecomendedrate = 0;

                List<ProductListedDetailJson> objlstProductListedDetail = new List<ProductListedDetailJson>();
                if (gvProduct.Rows.Count > 0)
                {
                    for (int i = 0; i < gvProduct.Rows.Count; i++)
                    {
                        totalrecord = totalrecord + 1;

                        GridViewRow row = gvProduct.Rows[i];
                        Decimal.TryParse((row.FindControl("txtNewRate") as TextBox).Text, out actualnewrate);
                        Decimal.TryParse((row.FindControl("txtMobexRate") as TextBox).Text, out actualmobexrate);
                        Decimal.TryParse((row.FindControl("lblPurPrice") as Label).Text, out actualrecomendedrate);

                        if (actualnewrate != 0
                            && actualmobexrate != 0
                            && actualrecomendedrate != 0
                           )
                        {
                            ProductListedDetailJson objProductListedDetailJson = new ProductListedDetailJson();

                            //Calculate FK,AMZ,WEB Amount
                            string purrate = (row.FindControl("txtMobexRate") as TextBox).Text;
                            string newrate = (row.FindControl("txtNewRate") as TextBox).Text;
                            string lockamt = (row.FindControl("txtFinalListingAmount") as TextBox).Text;


                            Double fkAmt = 0, amzAmt = 0, webAmt = 0, dcpurvalue = 0, dcnewrate = 0, dcpurchaseper = 0, fkper = 0, amzper = 0, webper = 0,dcrecomendedrate = 0
                            ,PURFKAMT = 0, PURAMZAMT = 0, PURWEBAMT = 0, PURFKPER = 0, PURAMZPER = 0, PURWEBPER = 0, PURCHASEPERONVENDORPRICE = 0 
                            , dcFinalListing = 0;
                            Double.TryParse(newrate, out dcnewrate);
                            Double.TryParse(purrate, out dcpurvalue);
                            Double.TryParse(lockamt, out dcFinalListing);
                            dcrecomendedrate = GeneralFunctionality.GetAGradeSuggestAmount((row.FindControl("lblPurPrice") as Label).Text, (row.FindControl("hdVendorGrade") as HiddenField).Value);
                            //Double.TryParse((row.FindControl("lblPurPrice") as Label).Text, out dcrecomendedrate);
                            int islistedfk = 0, islistedamz = 0, islistedweb = 0;
                            int ispurlistedfk = 0, ispurlistedamz = 0, ispurlistedweb = 0;
                            //Calculate PlatForm Selling Amount 
                            if (dcpurvalue > 0)
                            {
                                //Our New Logic 21-10-2022
                                if (dcpurvalue <= dcFinalListing)
                                {
                                    ispurlistedamz = 1;
                                    ispurlistedweb = 1;
                                }

                                fkAmt = (((dcpurvalue)) * (1.234));
                                fkAmt = (Math.Floor(fkAmt * Math.Pow(10, -2)) / Math.Pow(10, -2));

                                if (fkAmt <= dcpurvalue)
                                {
                                    fkAmt = dcpurvalue + (Math.Round(((dcpurvalue * 8) / 100), 2));
                                    fkAmt = (Math.Floor(fkAmt * Math.Pow(10, -2)) / Math.Pow(10, -2));
                                }

                                //Flipkart Calculate Amount on Vendor Price 
                                PURFKAMT = (((dcpurvalue)) * (1.234));
                                PURFKAMT = (Math.Floor(PURFKAMT * Math.Pow(10, -2)) / Math.Pow(10, -2));
                                //Flipkart Calculate Amount on Vendor Price 

                                amzAmt = (((dcpurvalue)) * (1.175));
                                amzAmt = (Math.Floor(amzAmt * Math.Pow(10, -2)) / Math.Pow(10, -2));

                                if (amzAmt <= dcpurvalue)
                                {
                                    amzAmt = dcpurvalue + (Math.Round(((dcpurvalue * 8) / 100), 2));
                                    amzAmt = (Math.Floor(amzAmt * Math.Pow(10, -2)) / Math.Pow(10, -2));
                                }

                                //Amazon Calculate Amount on Vendor Price 
                                PURAMZAMT = (((dcpurvalue)) * (1.175));
                                PURAMZAMT = (Math.Floor(PURAMZAMT * Math.Pow(10, -2)) / Math.Pow(10, -2));
                                //Amazon Calculate Amount on Vendor Price 

                                webAmt = (((amzAmt)) - (350));
                                //webAmt = (Math.Floor(webAmt * Math.Pow(10, -2)) / Math.Pow(10, -2));

                                //Web Calculate Amount on Vendor Price 
                                PURWEBAMT = (PURAMZAMT) - (350);
                                //Web Calculate Amount on Vendor Price

                                //if (dcpurvalue >= 3000 && dcpurvalue <= 7000)
                                //{
                                //    webAmt = dcpurvalue + ((dcpurvalue * 15) / 100) + 700;
                                //}
                                //else if (dcpurvalue >= 7001 && dcpurvalue <= 15000)
                                //{
                                //    webAmt = dcpurvalue + ((dcpurvalue * 10) / 100) + 700;
                                //}
                                //else if (dcpurvalue >= 15001 && dcpurvalue <= 30000)
                                //{
                                //    webAmt = dcpurvalue + ((dcpurvalue * 8) / 100) + 700;
                                //}
                                //else if (dcpurvalue >= 30001 && dcpurvalue <= 45000)
                                //{
                                //    webAmt = dcpurvalue + ((dcpurvalue * 5) / 100) + 700;
                                //}
                                //else if (dcpurvalue >= 45001 && dcpurvalue <= 60000)
                                //{
                                //    webAmt = dcpurvalue + ((dcpurvalue * 4) / 100) + 700;
                                //}
                                //else
                                //{
                                //    webAmt = dcpurvalue + ((dcpurvalue * 3) / 100) + 700;
                                //}
                            }

                            //Calculate PlatForm Per Amount 
                            if (fkAmt != 0)
                            {
                                fkper = Math.Round((((fkAmt * 100)) / dcnewrate), 0);
                            }

                            //FK PlatForm Per Amount on Purchase Price 
                            if (PURFKAMT != 0)
                            {
                                PURFKPER = Math.Round((((PURFKAMT * 100)) / dcnewrate), 0);
                            }
                            //FK PlatForm Per Amount on Purchase Price 

                            if (amzAmt != 0)
                            {
                                amzper = Math.Round((((amzAmt * 100)) / dcnewrate), 0);
                            }

                            //Amazon PlatForm Per Amount on Purchase Price 
                            if (PURAMZAMT != 0)
                            {
                                PURAMZPER = Math.Round((((PURAMZAMT * 100)) / dcnewrate), 0);
                            }
                            //Amazon PlatForm Per Amount on Purchase Price 

                            if (webAmt != 0)
                            {
                                webper = Math.Round((((webAmt * 100)) / dcnewrate), 0);
                            }

                            //Web PlatForm Per Amount on Purchase Price 
                            if (PURWEBAMT != 0)
                            {
                                PURWEBPER = Math.Round((((PURWEBAMT * 100)) / dcnewrate), 0);
                            }
                            //Web PlatForm Per Amount on Purchase Price

                            if (dcrecomendedrate > 0 && dcnewrate > 0)
                            {
                                dcpurchaseper = Math.Round(((dcpurvalue * 100)) / (dcnewrate), 0);
                            }

                            if (dcpurvalue > 0 && dcnewrate > 0)
                            {
                                PURCHASEPERONVENDORPRICE = Math.Round(((dcpurvalue * 100)) / (dcnewrate), 0);
                            }

                            //Calculate PlatForm Listed Detail 
                            islistedfk = fkper >= 90   ? 0 : 1;
                            islistedamz = amzper >= 90 ? 0 : 1;
                            islistedweb = webper >= 90 ? 0 : 1;

                            //Calculate Purchase PlatForm Listed Detail 
                            ispurlistedfk = PURFKPER >= 90 ? 0 : 1;
                            //ispurlistedamz = PURAMZPER >= 90 ? 0 : 1;
                            //ispurlistedweb = PURWEBPER >= 90 ? 0 : 1;


                            //Extra Condition Code
                            string make = (row.FindControl("lblMake") as Label).Text;
                            string mobexgrade = (row.FindControl("lblMobexGrade") as Label).Text;
                            string vendorgrade = (row.FindControl("hdVendorGrade") as HiddenField).Value;

                            //for Amazon
                            if (make.ToUpper() == "APPLE")
                            {
                                islistedamz = 0;
                                ispurlistedamz = 0;
                            }
                            else
                            {
                                //mobexgrade == "B" || vendorgrade == "B" ||
                                //if (( mobexgrade == "C") || ( vendorgrade == "C"))
                                //{
                                //    islistedamz = 0;
                                //    ispurlistedamz = 0;
                                //}
                                //else
                                //{
                                    var dtAmazone = objMainClass.GetPlatFormNotListed((PLATFORM.Amazon.ToString()), make, (row.FindControl("lblModel") as Label).Text, (row.FindControl("lblVendorGrade") as Label).Text, Convert.ToInt32(Session["USERID"].ToString()));
                                    if (dtAmazone.Rows.Count > 0)
                                    {
                                        islistedamz = 0;
                                        ispurlistedamz = 0;
                                    }
                                //}
                            }

                            //for Amazon

                            //for Web
                            //mobexgrade == "B" || vendorgrade == "B" ||
                            //if ((mobexgrade == "C") || (vendorgrade == "C"))
                            //{
                            //    islistedweb = 0;
                            //    ispurlistedweb = 0;
                            //}
                            //for Web


                            //for Flipcart
                            if (fkAmt > 45000) // || make.ToUpper() == "APPLE"
                            {
                                islistedfk    = 0;
                                ispurlistedfk = 0;
                            }
                            //for Flipcart

                            // Basic Purchase Price Rejection Condition
                            //if (dcpurvalue > dcrecomendedrate)
                            //{
                            //    islistedfk = 0;
                            //    islistedamz = 0;
                            //    islistedweb = 0;
                            //}
                            // Basic Purchase Price Rejection Condition

                            //Extra Condition Code
                            objProductListedDetailJson.PURQTY = "1";
                            objProductListedDetailJson.MOBEXPRICE = Convert.ToDecimal(dcpurvalue);
                            objProductListedDetailJson.MOBILENEWRATE = Convert.ToDecimal(dcnewrate);
                            objProductListedDetailJson.MOBILEPURCHASEPERCENTAGE = Convert.ToDecimal(PURCHASEPERONVENDORPRICE);
                            objProductListedDetailJson.NEGAPRVBY = Convert.ToInt32(Session["USERID"]);
                            objProductListedDetailJson.ISAPPROVEDFK = islistedfk;
                            objProductListedDetailJson.ISAPPROVEDAMZ = islistedamz;
                            objProductListedDetailJson.ISAPPROVEDWEB = islistedweb;
                            objProductListedDetailJson.FKAMT = Convert.ToDecimal(fkAmt);
                            objProductListedDetailJson.AMZAMT = Convert.ToDecimal(amzAmt);
                            objProductListedDetailJson.WEBAMT = Convert.ToDecimal(webAmt);
                            objProductListedDetailJson.FKPER = Convert.ToDecimal(fkper);
                            objProductListedDetailJson.AMZPER = Convert.ToDecimal(amzper);
                            objProductListedDetailJson.WEBPER = Convert.ToDecimal(webper);
                            objProductListedDetailJson.PURFKAMT = Convert.ToDecimal(PURFKAMT);
                            objProductListedDetailJson.PURAMZAMT = Convert.ToDecimal(PURAMZAMT);
                            objProductListedDetailJson.PURWEBAMT = Convert.ToDecimal(PURWEBAMT);
                            objProductListedDetailJson.PURFKPER = Convert.ToDecimal(PURFKPER);
                            objProductListedDetailJson.PURAMZPER = Convert.ToDecimal(PURAMZPER);
                            objProductListedDetailJson.PURWEBPER = Convert.ToDecimal(PURWEBPER);
                            objProductListedDetailJson.PURCHASEPERONVENDORPRICE = Convert.ToDecimal(dcpurchaseper); 
                            objProductListedDetailJson.ISPURAPPROVEDFK = ispurlistedfk;
                            objProductListedDetailJson.ISPURAPPROVEDAMZ = ispurlistedamz;
                            objProductListedDetailJson.ISPURAPPROVEDWEB = ispurlistedweb;
                            objProductListedDetailJson.BASICPURRATE = Convert.ToDecimal(dcrecomendedrate);
                            objProductListedDetailJson.FinalApproveListingAmount = Convert.ToDecimal(dcFinalListing);
                            objProductListedDetailJson.ID = Convert.ToInt32((row.FindControl("lblID") as Label).Text);

                            //ispurlistedfk == 0 &&
                            if ( ispurlistedamz == 0 && ispurlistedweb == 0)
                            {
                                objProductListedDetailJson.STATUS = Convert.ToInt32(PRODUCTSTATUS.REJECTED);
                                objProductListedDetailJson.NGEAPRV = "REJECTED";
                                rejected = rejected + 1;
                                objProductListedDetailJson.REJECTREASON = "Price Constraint";
                            }
                            else
                            {
                                objProductListedDetailJson.STATUS = Convert.ToInt32(PRODUCTSTATUS.APPROVED);
                                objProductListedDetailJson.NGEAPRV = "APPROVED";
                                objProductListedDetailJson.REJECTREASON = "";
                                approved = approved + 1;
                                ITEMGRP    = Convert.ToInt32((row.FindControl("hdItemGrpId") as HiddenField).Value);
                                ITEMSUBGRP = Convert.ToInt32((row.FindControl("hdItemSubGrpId") as HiddenField).Value);

                                ITEMGRPNAME    = (row.FindControl("hdItemGrpName") as HiddenField).Value;
                                ITEMSUBGRPNAME = (row.FindControl("hdItemSubGrpName") as HiddenField).Value;

                                ITEMGRPSHORTNAME    = (row.FindControl("hdItemGrpShortName") as HiddenField).Value;
                                ITEMSUBGRPSHORTNAME = (row.FindControl("hdItemSubGrpShortName") as HiddenField).Value;

                                string itemdesc = "";
                                if (ITEMGRP == 9 && ITEMSUBGRP == 168)
                                {
                                    itemdesc = make + " " + (row.FindControl("lblModel") as Label).Text + " "
                                    + (row.FindControl("lblRam") as Label).Text + " "
                                    + (row.FindControl("lblRom") as Label).Text + " "
                                    + (row.FindControl("lblColor") as Label).Text + " MOBILE DEVICE (USED) (GRADE "
                                    + ((row.FindControl("lblVendorGrade") as Label).Text == "C" ? (row.FindControl("lblVendorGrade") as Label).Text : "A") + ")"; //(row.FindControl("lblVendorGrade") as Label).Text 
                                }
                                else
                                {
                                    itemdesc = make + " " + (row.FindControl("lblModel") as Label).Text + " " + ITEMSUBGRPNAME
                                            + " (" + (row.FindControl("lblColor") as Label).Text + ") (GRADE "
                                            + (row.FindControl("lblVendorGrade") as Label).Text == "C" ? (row.FindControl("lblVendorGrade") as Label).Text : "A" + ")"; //(row.FindControl("lblVendorGrade") as Label).Text 
                                }
                                objProductListedDetailJson.ITEMCODE = GetItemCode(itemdesc, make, (row.FindControl("lblModel") as Label).Text);
                            }
                            objlstProductListedDetail.Add(objProductListedDetailJson);
                        }
                    }

                    if (objlstProductListedDetail.Count > 0)
                    {
                        string productdetailjson = JsonConvert.SerializeObject(objlstProductListedDetail);
                        int result = objMainClass.ProductBulkApproveRejectUpdate(productdetailjson);
                        if (result > 0)
                        {
                            BindProductDetail();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Total Record :" + totalrecord + " Total Approved Record : " + approved + " Total Reject Record : " + rejected + " Update Successfully." + "\");", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Atleast One Record Mobex Rate and Purchase Qty is required for Update.');", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('There is no record to for Update.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public string GetProductJson()
        {
            string productjson = "";
            List<ProductJson> objlstProductJson = new List<ProductJson>();
            try
            {
                for (int i = 0; i < gvProduct.Rows.Count; i++)
                {
                    GridViewRow row = gvProduct.Rows[i];
                    if (((CheckBox)row.FindControl("chkSelection")).Checked == true)
                    {
                        ProductJson objPrud = new ProductJson();
                        objPrud.ID = Convert.ToInt32(((HiddenField)row.FindControl("hdID")).Value);
                        objPrud.STATUS = Convert.ToInt32(PRODUCTSTATUS.LISTED);
                        objlstProductJson.Add(objPrud);
                    }
                }
                productjson = JsonConvert.SerializeObject(objlstProductJson);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return productjson;
        }

        public void DownloadListedProductDetail()
        {
            try
            {
                TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                string filename = "Listed Product Detail" + "-" + "DateTime-" + indianTime.ToString();
                string attachment = "attachment; filename=" + filename + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vdn.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gvProduct.RenderControl(htw);
                Response.Write(sw.ToString());
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                //Response.End();
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }


        // Auto Item Code Functionality 
        public string GetItemCode(string itemdesc,string make,string model)
        {
            string itemcode = string.Empty; 
            try
            {
                    DataTable dtItemDetail = new DataTable();
                    rowItemd               = null;
                    dtItemDetail       = objMainClass.GetItemMaxCode(ITEMGRP,ITEMSUBGRP, "GETALL");
                    rowItemd           = dtItemDetail.Select("ITEMDESC='" + itemdesc + "'");
                    if (rowItemd.Count() > 0)
                    {
                        foreach (DataRow eachrow in rowItemd)
                        {
                            itemcode = eachrow["ITEMCODE"].ToString();
                        }
                    }
                    else
                    {
                        DataTable dtNewItemCode = new DataTable();
                        dtNewItemCode           = objMainClass.GetItemMaxCode(ITEMGRP, ITEMSUBGRP, "GETMAXITEMCODE");
                        if(dtNewItemCode.Rows.Count > 0)
                        {
                            maxItemcode = Convert.ToInt32(dtNewItemCode.Rows[0]["MAXITEMCODE"].ToString());
                            itemcode    = ITEMGRPSHORTNAME + ITEMSUBGRPSHORTNAME  + objMainClass.strConvertZeroPadding((maxItemcode.ToString()), "0", 6);
                            objMainClass.INSERTITEMDETAIL(1, itemcode, 17, 2, ITEMGRP, ITEMSUBGRP, 1,1,1,1,0,1,1,
                                itemdesc, itemdesc,0, make,model, ITEMSUBGRP, 1);
                        }
                    }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return itemcode;
        }
        // Auto Item Code Functionality 
    }
}