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
using Newtonsoft;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System.Web.Services;

namespace ShERPa360net.UTILITY
{
    public partial class ProductEntry : System.Web.UI.Page
    {
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        
        //Item Creation Variable
        int ITEMGRP            = 9;
        int ITEMSUBGRP         = 168;

        #region PAGEEVENT
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //SendPushNotification();
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

                        if (FormRights.bAdd == false)
                        {
                            btnAdd.Enabled = false;
                            btnReset.Enabled = false;
                        }

                        BindPageDropDown();
                        if (Request.QueryString.Count > 0)
                        {
                            if (Request.QueryString["VENDORNAME"] != null)
                            {
                                Session["VENDORNAME"] = Request.QueryString["VENDORNAME"].ToString();
                                Response.Redirect(Request.Url.AbsolutePath, false);
                            }
                        }
                        else
                        {
                            if (Session["VENDORNAME"] != null)
                            {
                                ddlVendor.SelectedValue = Session["VENDORNAME"].ToString();
                                Session["VENDORNAME"] = null;
                            }
                        }
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

        public void AllowCGradeandAddPartsDetail()
        {
            try
            {
                if(ddlMake.SelectedValue != "0" && ddlModel.SelectedValue != "0" && ddlColor.SelectedValue != "0"
                  )
                {
                    if (lstPartAssign.Items.Count > 0 && ddlGrade.SelectedValue != "C")
                    {
                        lstPartAssign.DataSource = null;
                        lstPartAssign.DataBind();
                        lstPartAssign.Items.Clear();
                        dvParts.Visible = false;

                        //if(lstPartAssign.Items.Count > 0)
                        //{
                        //    ListItem item = ddlGrade.Items.FindByText("C");
                        //    if (item != null)
                        //    {
                        //        ddlGrade.Items.Remove(item);
                        //    }
                        //}
                    }
                    else
                    {
                        var dtMakeModelParts = objMainClass.GetMakeModelPartsDetail(ddlMake.SelectedItem.Text, ddlModel.SelectedItem.Text, ddlColor.SelectedItem.Text);
                        if (dtMakeModelParts.Rows.Count > 0)
                        {
                            dvParts.Visible = true;
                            lstPartAssign.DataSource = dtMakeModelParts;
                            lstPartAssign.DataTextField = "COMBONAME";
                            lstPartAssign.DataValueField = "COMBOID";
                            lstPartAssign.DataBind();

                            //Need to remove item detail 
                            //ListItem item = ddlGrade.Items.FindByText("C");
                            //if (item == null)
                            //{
                            //    ddlGrade.Items.Add(new ListItem { Text = "C", Value = "C" });
                            //    //ddlGrade.Items.Remove(item);
                            //}
                            //Need to remove item detail 
                            if(ddlGrade.SelectedValue == "C")
                            {
                                dvParts.Visible = true;
                            }
                            else
                            {
                                dvParts.Visible = false;
                            }
                        }
                        else
                        {
                            lstPartAssign.DataSource = null;
                            lstPartAssign.DataBind();
                            lstPartAssign.Items.Clear();
                            dvParts.Visible = false;

                            //ListItem item = ddlGrade.Items.FindByText("C");
                            //if (item != null)
                            //{
                            //    ddlGrade.Items.Remove(item);
                            //}
                        }
                    }
                }
                else
                {
                    lstPartAssign.DataSource = null;
                    lstPartAssign.DataBind();
                    lstPartAssign.Items.Clear();
                    dvParts.Visible = false;

                    //ListItem item = ddlGrade.Items.FindByText("C");
                    //if (item != null)
                    //{
                    //    ddlGrade.Items.Remove(item);
                    //}
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
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myLoadingJs", "ShowProgressBaar();", true);
                int result          = 0;
                byte[] IMAGE        = null;
                
                if (Session["USERID"] != null)
                {
                    Double fkAmt = 0, amzAmt = 0, webAmt = 0, dcpurvalue = 0, PURFKAMT = 0 , PURAMZAMT = 0 , PURWEBAMT = 0 , PURFKPER = 0 , PURAMZPER = 0 , PURWEBPER = 0,PURCHASEPERONVENDORPRICE = 0,
                    dcnewrate = 0, dcpurchaseper = 0, fkper = 0, amzper = 0, webper = 0, minautoAprrecomendamt = 0 , dcFinalListing = 0;
                    int islistedfk = 0, islistedamz = 0, islistedweb = 0;
                    int ispurlistedfk = 0, ispurlistedamz = 0, ispurlistedweb = 0;
                    string itemcode     = "";
                    int status          = 0;
                    string MNLAPRREASON = "";
                    string rejectreason = "";
                    string NGEAPRV      = "";
                    string partsname    = "";
                    string partsrate    = "";
                    Double recomendedrate = 0, recompurchper = 0 , actlrecomendedrate = 0;
                    Double.TryParse(hdSuggestRate.Value, out actlrecomendedrate);
                    Double.TryParse(hdLockAmount.Value, out dcFinalListing);
                    recomendedrate = GeneralFunctionality.GetAGradeSuggestAmount(hdSuggestRate.Value, ddlGrade.SelectedItem.Text);
                    if (fuImage.HasFiles)
                    {
                        using (BinaryReader br = new BinaryReader(fuImage.PostedFile.InputStream))
                        {
                            IMAGE = br.ReadBytes(fuImage.PostedFile.ContentLength);
                        }
                    }

                    // Calculate Amount 
                    Double.TryParse(txtNewRate.Text, out dcnewrate);
                    Double.TryParse(txtVendorRate.Text, out dcpurvalue);

                    if (dcnewrate > 0 && recomendedrate > 0)
                    {
                        minautoAprrecomendamt = Math.Round(((actlrecomendedrate) - ((actlrecomendedrate * 35) / 100)),0);

                        //Calculate PlatForm Selling Amount     
                        if (dcpurvalue > 0)
                        {
                            //Our New Logic 21-10-2022
                            if (dcpurvalue <= dcFinalListing)
                            {
                                ispurlistedamz = 1;
                                ispurlistedweb = 1;
                            }

                            // Parts Calculation
                            //if ((ddlGrade.SelectedItem.Text == "C"))
                            //{
                            //    var objPartsDetail = GetSelectedPartsDetail();
                            //    partsname  = objPartsDetail.PARTSNAME;
                            //    partsrate  = objPartsDetail.PARTSRATE;
                            //    dcpurvalue = dcpurvalue + objPartsDetail.PARTSTOTALAMOUNT;
                            //}

                            fkAmt  = (((dcpurvalue)) * (1.234));
                            fkAmt  = (Math.Floor(fkAmt * Math.Pow(10, -2)) / Math.Pow(10, -2));

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

                            webAmt = (amzAmt) - (350);

                            //Web Calculate Amount on Vendor Price 
                            PURWEBAMT = (PURAMZAMT) - (350);
                            //Web Calculate Amount on Vendor Price
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
                        //Amazon PlatForm Per Amount on Purchase Price 

                        if (recomendedrate > 0 && dcnewrate > 0)
                        {
                            dcpurchaseper = Math.Round(((dcpurvalue * 100)) / (dcnewrate), 0);
                        }

                        if (dcpurvalue > 0 && dcnewrate > 0)
                        {
                            PURCHASEPERONVENDORPRICE = Math.Round(((dcpurvalue * 100)) / (dcnewrate), 0);
                        }

                        //Calculate PlatForm Listed Detail 
                        islistedfk  = fkper  >= 90 ? 0  : 1;
                        islistedamz = amzper >= 90 ? 0 : 1;
                        islistedweb = webper >= 90 ? 0 : 1;

                        //Calculate Purchase PlatForm Listed Detail 
                        //ispurlistedfk  = PURFKPER >= 90 ? 0 : 1;
                        //ispurlistedamz = PURAMZPER >= 90 ? 0 : 1;
                        //ispurlistedweb = PURWEBPER >= 90 ? 0 : 1;


                        //Extra Condition Code
                        string make         = ddlMake.SelectedItem.Text;
                        string mobexgrade   = ddlGrade.SelectedItem.Text;
                        string vendorgrade  = ddlGrade.SelectedItem.Text;

                        //for Amazon
                        if (make.ToUpper() == "APPLE")
                        {
                            islistedamz = 0;
                            ispurlistedamz = 0;
                        }
                        else
                        {
                            //mobexgrade == "B" ||  vendorgrade == "B" ||
                            //if (( mobexgrade == "C") || ( vendorgrade == "C"))
                            //{
                            //    islistedamz = 0;
                            //    ispurlistedamz = 0;

                            //    var objPartsDetail = GetSelectedPartsDetail();
                            //    partsname = objPartsDetail.PARTSNAME;
                            //    partsrate = objPartsDetail.PARTSRATE;
                            //}
                            //else
                            //{
                            var dtAmazone      =  objMainClass.GetPlatFormNotListed((PLATFORM.Amazon.ToString()),make, ddlModel.SelectedItem.Text, ddlGrade.SelectedItem.Text,Convert.ToInt32(Session["USERID"].ToString()));
                            if(dtAmazone.Rows.Count > 0)
                            {
                                islistedamz    = 0;
                                ispurlistedamz = 0;
                            }
                            //}
                        }

                        

                        //for Amazon

                        //mobexgrade == "B" || vendorgrade == "B" ||
                        //for Web
                        //if ((mobexgrade == "C") || (vendorgrade == "C"))
                        //{
                        //    islistedweb    = 0;
                        //    ispurlistedweb = 0;
                        //}
                        //for Web


                        //for Flipcart
                        if (fkAmt > 45000)
                        {
                            islistedfk    = 0;
                            ispurlistedfk = 0;
                        }
                        //for Flipcart

                        //Extra Condition Code


                        // Item Code Creation

                        //Get Item  Code 

                        // Basic Purchase Price Rejection Condition
                        //if(dcpurvalue > recomendedrate)
                        //{
                        //    islistedfk  = 0;
                        //    islistedamz = 0;
                        //    islistedweb = 0;
                        //}
                        // Basic Purchase Price Rejection Condition

                        if ((ispurlistedamz == 1 || ispurlistedweb == 1))
                        {
                                if (dcpurvalue >= minautoAprrecomendamt)
                                {
                                    string itemdesc = make + " " + ddlModel.SelectedItem.Text + " "
                                    + ddlRam.SelectedItem.Text + " "
                                    + ddlRom.SelectedItem.Text + " "
                                    + ddlColor.SelectedItem.Text + " MOBILE DEVICE (USED) (GRADE "
                                    + (ddlGrade.SelectedItem.Text == "C" ?  ddlGrade.SelectedItem.Text : "A") + ")"; // "A"

                                itemcode = GetItemCode(itemdesc, make, ddlModel.SelectedItem.Text);
                                    status = Convert.ToInt32(PRODUCTSTATUS.LISTED);
                                    NGEAPRV = "APPROVED";
                                }
                                else
                                {
                                    MNLAPRREASON = "Suggest price:" + recomendedrate + " New Rate:" + dcnewrate + " Minimu Auto Apr Amt:" + minautoAprrecomendamt;
                                    status = Convert.ToInt32(PRODUCTSTATUS.TESTED);
                                }
                        }
                        else
                        {
                            rejectreason    = "Price Constraint";
                            status          = Convert.ToInt32(PRODUCTSTATUS.REJECTED);
                            NGEAPRV         = "REJECTED";
                            
                            //if ((dcpurvalue > recomendedrate))
                            //{
                            //    MNLAPRREASON = "Vendor Rate > Basic Rate Issue";    
                            //    rejectreason = "Higher Sales Price";
                            //    NGEAPRV = "REJECTED";
                            //    status = Convert.ToInt32(PRODUCTSTATUS.REJECTED);
                            //}
                            //else
                            //{
                            //    rejectreason = "Price Constraint";
                            //    status = Convert.ToInt32(PRODUCTSTATUS.REJECTED);
                            //    NGEAPRV = "REJECTED";
                            //}
                        }
                        //Get Item  Code 
                    }
                    else
                    {
                        MNLAPRREASON = "New Price or Suggest Price is not available";
                        status       = Convert.ToInt32(PRODUCTSTATUS.TESTED);
                    }
                    //result = 
                    objMainClass.SaveProductEntry(ddlMake.SelectedItem.Text, ddlModel.SelectedItem.Text, 
                        ddlColor.SelectedItem.Text, ddlRam.SelectedItem.Text, ddlRom.SelectedItem.Text, ddlGrade.SelectedItem.Text,
                        Convert.ToDecimal(dcpurvalue), txtVendorStock.Text,ddlVendor.SelectedItem.Text, 
                        Session["USERID"].ToString(), Convert.ToInt32(ddlInvoice.SelectedValue), Convert.ToInt32(ddlBox.SelectedValue),
                        Convert.ToInt32(ddlCharger.SelectedValue),txtIMEINo.Text, Convert.ToInt32(ddlVendor.SelectedValue),
                        rejectreason,
                        NGEAPRV, dcnewrate,
                        PURCHASEPERONVENDORPRICE , islistedfk,islistedamz,islistedweb,fkAmt,amzAmt,webAmt,fkper,amzper,webper,itemcode, status, IMAGE, MNLAPRREASON,Convert.ToDecimal(recomendedrate)
                        ,PURFKAMT, PURAMZAMT, PURWEBAMT, PURFKPER, PURAMZPER, PURWEBPER, dcpurchaseper, ispurlistedfk, ispurlistedamz, ispurlistedweb
                        ,9,168,"",partsname,partsrate,dcFinalListing,Convert.ToInt32(ddllistype.SelectedValue));
                    //if (result > 0)
                    //{
                        ResetFormControl();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myVendorJs", "LoadVendorAutoSuggestJs();", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Record Add Successfully." + "\");", true);
                    //}
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myUnLoadingJs", "HideProgressBaar();", true);
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
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        #endregion

        #region PAGEMETHOD
        public void BindPageDropDown()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    objBindDDL.FillMobexSellerBrand(ddlMake);
                    ddlMake.SelectedValue = "0";

                    ddlModel.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                    ddlModel.SelectedValue = "0";

                    ddlRam.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                    ddlRam.SelectedValue = "0";

                    ddlRom.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                    ddlRom.SelectedValue = "0";

                    ddlColor.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                    ddlColor.SelectedValue = "0";

                    objBindDDL.FillMobexSellerVendorByBikerAreaWise(ddlVendor, Convert.ToInt32(Session["USERID"].ToString()));
                    ddlVendor.SelectedValue = "0";

                    objBindDDL.FillLists(ddllistype, "LTP");

                    //objBindDDL.FillMobexSellerRom(ddlRom);
                    //ddlRom.SelectedValue = "0";

                    //objBindDDL.FillMobexSellerRam(ddlRam);
                    //ddlRam.SelectedValue = "0";

                    //objBindDDL.FillMobexSellerColor(ddlColor);
                    //ddlColor.SelectedValue = "0";
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

        public void ResetFormControl()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    //ddlMake.SelectedValue    = "0";
                    //ddlModel.SelectedValue   = "0";
                    //ddlRom.SelectedValue     = "0";
                    //ddlRam.SelectedValue     = "0";
                    //ddlColor.SelectedValue   = "0";

                    ddlModel.Items.Clear();
                    ddlRam.Items.Clear();
                    ddlRom.Items.Clear();
                    ddlColor.Items.Clear();

                    ddlModel.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                    ddlModel.SelectedValue = "0";

                    ddlRam.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                    ddlRam.SelectedValue = "0";

                    ddlRom.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                    ddlRom.SelectedValue = "0";

                    ddlColor.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                    ddlColor.SelectedValue = "0";

                    ddlGrade.SelectedValue   = "SELECT";
                    txtVendorStock.Text      = "1";
                    txtVendorRate.Text       = string.Empty;
                    ddlInvoice.SelectedValue = "-1";
                    ddlBox.SelectedValue     = "-1";
                    ddlCharger.SelectedValue = "-1";
                    ddlMake.SelectedValue    = "0";
                    txtRecomendRate.Text = string.Empty;
                    txtNewRate.Text = string.Empty;
                    hdNewRate.Value = "0";
                    txtIMEINo.Text = string.Empty;
                    hdLockAmount.Value = string.Empty;
                    ddlMake.Focus();
                    lstPartAssign.DataSource = null;
                    lstPartAssign.DataBind();
                    dvParts.Visible = false;
                    ddllistype.SelectedValue = "0";
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


        public void BindMakeorModelAssociateDetail(string reqdropdown)
        {
            try
            {
                if(reqdropdown == "Model")
                {
                    if (ddlMake.SelectedValue != "0")
                    {
                        ddlModel.Items.Clear();


                        objBindDDL.FillMobexSellerModel(ddlModel, Convert.ToInt32(ddlMake.SelectedValue));

                        ddlRam.Items.Clear();
                        ddlRom.Items.Clear();
                        ddlColor.Items.Clear();

                        ddlRam.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                        ddlRam.SelectedValue = "0";

                        ddlRom.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                        ddlRom.SelectedValue = "0";

                        ddlColor.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                        ddlColor.SelectedValue = "0";
                        LoadRecomendedRate();
                        LoadFinalListingRate();
                        LoadNewRate();
                        //AllowCGradeandAddPartsDetail();
                    }
                    else
                    {
                        txtNewRate.Text = string.Empty;
                        hdNewRate.Value = "0";
                        txtRecomendRate.Text = string.Empty;
                        ddlModel.Items.Clear();
                        hdNewRate.Value = "0";
                        ddlRam.Items.Clear();
                        ddlRom.Items.Clear();
                        ddlColor.Items.Clear();

                        ddlModel.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                        ddlModel.SelectedValue = "0";

                        ddlRam.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                        ddlRam.SelectedValue = "0";

                        ddlRom.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                        ddlRom.SelectedValue = "0";

                        ddlColor.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                        ddlColor.SelectedValue = "0";
                        //AllowCGradeandAddPartsDetail();
                    }
                }
                else if (reqdropdown == "RamRom")
                {
                    if (ddlModel.SelectedValue != "0")
                    {
                        ddlRam.Items.Clear();
                        ddlRom.Items.Clear();
                        

                        if (ddlMake.SelectedItem.Text.ToUpper() == "APPLE")
                        {
                            ddlRam.Items.Insert(0, new ListItem("NA", "NA"));
                            ddlRam.SelectedValue = "NA";
                        }
                        else
                        {
                            objBindDDL.FillMobexSellerRam(ddlRam, Convert.ToInt32(ddlMake.SelectedValue),
                            Convert.ToInt32(ddlModel.SelectedValue));
                        }

                        objBindDDL.FillMobexSellerRom(ddlRom, Convert.ToInt32(ddlMake.SelectedValue),
                                    Convert.ToInt32(ddlModel.SelectedValue));

                        ddlColor.Items.Clear();
                        ddlColor.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                        ddlColor.SelectedValue = "0";
                        LoadRecomendedRate();
                        LoadFinalListingRate();
                        LoadNewRate();
                        //AllowCGradeandAddPartsDetail();
                    }
                    else
                    {
                        ddlRam.Items.Clear();
                        ddlRom.Items.Clear();
                        ddlColor.Items.Clear();
                        txtNewRate.Text = string.Empty;
                        hdNewRate.Value = "0";
                        txtRecomendRate.Text = string.Empty;

                        ddlRam.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                        ddlRam.SelectedValue = "0";

                        ddlRom.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                        ddlRom.SelectedValue = "0";

                        ddlColor.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                        ddlColor.SelectedValue = "0";
                        //AllowCGradeandAddPartsDetail();
                    }
                }
                else if (reqdropdown == "Color")
                {
                    if (ddlRam.SelectedValue != "0" || ddlRom.SelectedValue != "0")
                    {
                        ddlColor.Items.Clear();

                        objBindDDL.FillMobexSellerColor(ddlColor, Convert.ToInt32(ddlMake.SelectedValue),
                                   Convert.ToInt32(ddlModel.SelectedValue),ddlRam.SelectedItem.Text, ddlRom.SelectedItem.Text);
                        LoadRecomendedRate();
                        LoadFinalListingRate();
                        LoadNewRate();
                        //AllowCGradeandAddPartsDetail();
                    }
                    else
                    {
                        txtNewRate.Text = string.Empty;
                        hdNewRate.Value = "0";
                        txtRecomendRate.Text = string.Empty;
                        hdSuggestRate.Value = string.Empty;
                        ddlColor.Items.Clear();

                        ddlColor.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                        ddlColor.SelectedValue = "0";
                        //AllowCGradeandAddPartsDetail();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public void LoadRecomendedRate()
        {
            try
            {
                if (ddlMake.SelectedValue != "0" &&  ddlModel.SelectedValue != "0" && ddlRam.SelectedValue != "0"
                    && ddlRom.SelectedValue != "0" && ddlGrade.SelectedValue != "SELECT" && ddlColor.SelectedValue != "0")
                {
                    var dtRecomendedRate = objMainClass.GetProductRecomendedRate(ddlMake.SelectedItem.Text, ddlModel.SelectedItem.Text,
                    ddlRam.SelectedItem.Text, ddlRom.SelectedItem.Text, ddlGrade.SelectedItem.Text,ddlColor.SelectedItem.Text);

                    if (dtRecomendedRate.Rows.Count > 0)
                    {
                        txtRecomendRate.Text   = dtRecomendedRate.Rows[0]["PURCHASERATE"].ToString();
                        hdSuggestRate.Value    = dtRecomendedRate.Rows[0]["PURCHASERATE"].ToString();
                        //txtNewRate.Text      = dtRecomendedRate.Rows[0]["MOBILENEWRATE"].ToString();
                    }
                    else
                    {
                        txtRecomendRate.Text   = string.Empty;
                        hdSuggestRate.Value    = string.Empty;
                        //txtNewRate.Text = string.Empty;
                    }
                }
                else
                {
                    txtRecomendRate.Text = string.Empty;
                    hdSuggestRate.Value  = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }

        }

        public void LoadFinalListingRate()
        {
            try
            {
                if (ddlMake.SelectedValue != "0" && ddlModel.SelectedValue != "0" && ddlRam.SelectedValue != "0"
                    && ddlRom.SelectedValue != "0" && ddlGrade.SelectedValue != "SELECT" && ddlColor.SelectedValue != "0"
                    && ddllistype.SelectedValue != "0"
                    )
                {
                    var dtFinalListingRate = objMainClass.GetProductFinalListingRate(ddlMake.SelectedItem.Text, ddlModel.SelectedItem.Text,
                    ddlRam.SelectedItem.Text, ddlRom.SelectedItem.Text, ddlGrade.SelectedItem.Text, ddlColor.SelectedItem.Text);

                    if (dtFinalListingRate.Rows.Count > 0)
                    {
                        if(Convert.ToInt32(ddllistype.SelectedValue) == 12309)
                        {
                            hdLockAmount.Value = dtFinalListingRate.Rows[0]["FinalStockApproveAmount"].ToString();
                        }
                        else
                        {
                            hdLockAmount.Value = dtFinalListingRate.Rows[0]["FinalApproveListingAmount"].ToString();
                        }
                    }
                    else
                    {
                        hdLockAmount.Value= string.Empty;
                    }
                }
                else
                {
                    hdLockAmount.Value = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }

        }

        public void LoadLaunchYear()
        {
            try
            {
                if (ddlMake.SelectedValue != "0" && ddlModel.SelectedValue != "0")
                {
                    var dtLaunchYear = objMainClass.GetMakeModelLaunchYear(ddlMake.SelectedItem.Text, ddlModel.SelectedItem.Text, "LOADLAUNCHYEAR");

                    if(dtLaunchYear.Rows.Count > 0)
                    {
                        hdLaunchyear.Value      = dtLaunchYear.Rows[0]["LAUNCHYEAR"].ToString();
                    }
                    else
                    {
                        hdLaunchyear.Value      = string.Empty;
                    }
                }
                else
                {
                    hdLaunchyear.Value          = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }


        public void LoadNewRate()
        {
            try
            {
                if (ddlMake.SelectedValue != "0" && ddlModel.SelectedValue != "0" && ddlRam.SelectedValue != "0"
                    && ddlRom.SelectedValue != "0" )
                {
                    var dtNewRate = objMainClass.GetMobileNewRate(ddlMake.SelectedItem.Text, ddlModel.SelectedItem.Text,
                    ddlRam.SelectedItem.Text, ddlRom.SelectedItem.Text);

                    if (dtNewRate.Rows.Count > 0)
                    {
                        txtNewRate.Text = dtNewRate.Rows[0]["MOBILENEWRATE"].ToString();
                        hdNewRate.Value = dtNewRate.Rows[0]["MOBILENEWRATE"].ToString();
                    }
                    else
                    {
                        txtNewRate.Text = string.Empty;
                        hdNewRate.Value = "0";
                    }
                }
                else
                {
                    txtNewRate.Text = string.Empty;
                    hdNewRate.Value = "0";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        #endregion

        protected void ddlMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindMakeorModelAssociateDetail("Model");
                LoadLaunchYear();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void ddlModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindMakeorModelAssociateDetail("RamRom");
                LoadLaunchYear();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void ddlRam_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindMakeorModelAssociateDetail("Color");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void ddlRom_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindMakeorModelAssociateDetail("Color");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void ddlGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadRecomendedRate();
                LoadFinalListingRate();
                //AllowCGradeandAddPartsDetail();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        // Auto Item Code Functionality 
        public string GetItemCode(string itemdesc, string make, string model)
        {
            string itemcode = string.Empty;
            try
            {
                DataTable dtItemDetail = new DataTable();
                int maxItemcode = 0;
                DataRow[] rowItemd = null;
                dtItemDetail       = objMainClass.GetItemMaxCode(ITEMGRP, ITEMSUBGRP, "GETALL");
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
                    if (dtNewItemCode.Rows.Count > 0)
                    {
                        maxItemcode = Convert.ToInt32(dtNewItemCode.Rows[0]["MAXITEMCODE"].ToString());
                        itemcode    = "MD" + "UD" + objMainClass.strConvertZeroPadding((maxItemcode.ToString()), "0", 6);
                        objMainClass.INSERTITEMDETAIL(1, itemcode, 17, 2, 9, 168, 1, 1, 1, 1, 0, 1, 1,
                            itemdesc, itemdesc, 0, make, model, 168, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return itemcode;
        }

        protected void ddlColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadRecomendedRate();
                LoadFinalListingRate();
                //AllowCGradeandAddPartsDetail();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }


        public PartsDetail GetSelectedPartsDetail()
        {
            PartsDetail objParts = new PartsDetail();
            try
            {
                foreach (ListItem listItem in lstPartAssign.Items)
                {
                    if (listItem.Selected)
                    {
                        if(objParts.PARTSNAME.Length > 0)
                        {
                            objParts.PARTSNAME = objParts.PARTSNAME +   "," + listItem.Text;
                        }
                        else
                        {
                            objParts.PARTSNAME = listItem.Text;
                        }

                        if (objParts.PARTSRATE.Length > 0)
                        {
                            objParts.PARTSRATE = objParts.PARTSRATE + "," + listItem.Value;
                        }
                        else
                        {
                            objParts.PARTSRATE = listItem.Value;
                        }
                        objParts.PARTSTOTALAMOUNT = objParts.PARTSTOTALAMOUNT + Convert.ToDouble(listItem.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return objParts;
        }


        public PartsDetail TotalPartsAmount()
        {
            PartsDetail objPartsDetail = new PartsDetail();
            try
            {
                foreach (ListItem listItem in lstPartAssign.Items)
                {
                    if (listItem.Selected)
                    {
                        objPartsDetail.PARTSTOTALAMOUNT = objPartsDetail.PARTSTOTALAMOUNT + Convert.ToDouble(listItem.Value);
                    }
                    else
                    {
                        objPartsDetail.PARTSUNCHECKEDTOTALAMOUNT = objPartsDetail.PARTSUNCHECKEDTOTALAMOUNT + Convert.ToDouble(listItem.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return objPartsDetail;
        }
        protected void lstPartAssign_SelectedIndexChanged(object sender, EventArgs e)
        {
            Double suggestAmount = 0;
            try
            {
                Double.TryParse(hdSuggestRate.Value, out suggestAmount);
                if (suggestAmount != 0)
                {
                    var objPartsDetail = TotalPartsAmount();
                    txtRecomendRate.Text = (suggestAmount - objPartsDetail.PARTSTOTALAMOUNT).ToString(); // totalpartAmount
                    //txtRecomendRate.Text = (suggestAmount + objPartsDetail.PARTSUNCHECKEDTOTALAMOUNT - 0).ToString(); //totalpartAmount
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        //Check whether IMEI exists or not
        [WebMethod]
        public static string GetIMEINo(string imeino, string LISTINGTYPE)
        {
            string status = "false";
            try
            {
                MainClass objMain = new MainClass();
                var detail = objMain.GetExistsImeiNo(imeino, LISTINGTYPE, "GETEXISTSIMEINO");
                if (detail != null && detail.Rows != null && detail.Rows.Count > 0)
                {
                    status = "true";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonConvert.SerializeObject(status);
        }

        protected void ddllistype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadFinalListingRate();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}