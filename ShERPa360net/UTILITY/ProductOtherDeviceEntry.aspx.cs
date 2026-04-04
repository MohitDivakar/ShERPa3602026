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
using ShERPa360net.Models;

namespace ShERPa360net.UTILITY
{
    public partial class ProductOtherDeviceEntry : System.Web.UI.Page
    {
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();

        //Item Creation Variable
        int ITEMGRP = 0;
        int ITEMSUBGRP = 0;


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
                            btnAdd.Enabled   = false;
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


        protected void imgSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myLoadingJs", "ShowProgressBaar();", true);
                int result = 0;
                byte[] IMAGE = null;

                if (Session["USERID"] != null)
                {
                    Double fkAmt = 0, amzAmt = 0, webAmt = 0, dcpurvalue = 0, PURFKAMT = 0, PURAMZAMT = 0, PURWEBAMT = 0, PURFKPER = 0, PURAMZPER = 0, PURWEBPER = 0, PURCHASEPERONVENDORPRICE = 0,
                    dcnewrate = 0, dcpurchaseper = 0, fkper = 0, amzper = 0, webper = 0, minautoAprrecomendamt = 0;
                    int islistedfk = 0, islistedamz = 0, islistedweb = 0;
                    int ispurlistedfk = 0, ispurlistedamz = 0, ispurlistedweb = 0;
                    string itemcode = "";
                    int status = 0;
                    string MNLAPRREASON = "";
                    string rejectreason = "";
                    string NGEAPRV = "";
                    Double recomendedrate = 0, recompurchper = 0, actlrecomendedrate = 0;
                    Double.TryParse(txtRecomendRate.Text, out actlrecomendedrate);
                    recomendedrate = GeneralFunctionality.GetAGradeSuggestAmount(txtRecomendRate.Text, ddlGrade.SelectedItem.Text);
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
                        minautoAprrecomendamt = Math.Round(((actlrecomendedrate) - ((actlrecomendedrate * 20) / 100)), 0);

                        //Calculate PlatForm Selling Amount     
                        if (dcpurvalue > 0)
                        {
                            fkAmt = (((recomendedrate)) * (1.234));
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

                            amzAmt = (((recomendedrate + 700)) * (1.175));
                            amzAmt = (Math.Floor(amzAmt * Math.Pow(10, -2)) / Math.Pow(10, -2));

                            if (amzAmt <= dcpurvalue)
                            {
                                amzAmt = dcpurvalue + (Math.Round(((dcpurvalue * 8) / 100), 2));
                                amzAmt = (Math.Floor(amzAmt * Math.Pow(10, -2)) / Math.Pow(10, -2));
                            }

                            //Amazon Calculate Amount on Vendor Price 
                            PURAMZAMT = (((dcpurvalue + 700)) * (1.175));
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
                            dcpurchaseper = Math.Round(((recomendedrate * 100)) / (dcnewrate), 0);
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
                        ispurlistedamz = PURAMZPER >= 90 ? 0 : 1;
                        ispurlistedweb = PURWEBPER >= 90 ? 0 : 1;


                        //Extra Condition Code
                        string make = ddlMake.SelectedItem.Text;
                        string mobexgrade = ddlGrade.SelectedItem.Text;
                        string vendorgrade = ddlGrade.SelectedItem.Text;

                        //for Amazon
                        if (make.ToUpper() == "APPLE")
                        {
                            islistedamz = 0;
                            ispurlistedamz = 0;
                        }
                        else
                        {
                            //mobexgrade == "B" ||  vendorgrade == "B" ||
                            if ((mobexgrade == "C") || (vendorgrade == "C"))
                            {
                                islistedamz = 0;
                                ispurlistedamz = 0;
                            }
                            else
                            {
                                var dtAmazone = objMainClass.GetPlatFormNotListed((PLATFORM.Amazon.ToString()), make, ddlModel.SelectedItem.Text, ddlGrade.SelectedItem.Text, Convert.ToInt32(Session["USERID"].ToString()));
                                if (dtAmazone.Rows.Count > 0)
                                {
                                    islistedamz = 0;
                                    ispurlistedamz = 0;
                                }
                            }
                        }

                        //for Amazon

                        //mobexgrade == "B" || vendorgrade == "B" ||
                        //for Web
                        if ((mobexgrade == "C") || (vendorgrade == "C"))
                        {
                            islistedweb = 0;
                            ispurlistedweb = 0;
                        }
                        //for Web


                        //for Flipcart
                        if (fkAmt > 45000)
                        {
                            islistedfk = 0;
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
                                string itemdesc = make + " " + ddlModel.SelectedItem.Text + " " + ddlItemSubGroup.SelectedItem.Text
                                + " (" + ddlColor.SelectedItem.Text + ") (GRADE "
                                + ddlGrade.SelectedItem.Text + ")";

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
                            rejectreason = "Price Constraint";
                            status = Convert.ToInt32(PRODUCTSTATUS.REJECTED);
                            NGEAPRV = "REJECTED";

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
                        status = Convert.ToInt32(PRODUCTSTATUS.TESTED);
                    }
                    //result = 
                    objMainClass.SaveProductEntry(ddlMake.SelectedItem.Text, ddlModel.SelectedItem.Text,
                        ddlColor.SelectedItem.Text, ddlRam.SelectedItem.Text, ddlRom.SelectedItem.Text, ddlGrade.SelectedItem.Text,
                        Convert.ToDecimal(txtVendorRate.Text), txtVendorStock.Text, ddlVendor.SelectedItem.Text,
                        Session["USERID"].ToString(), Convert.ToInt32(ddlInvoice.SelectedValue), Convert.ToInt32(ddlBox.SelectedValue),
                        Convert.ToInt32(ddlCharger.SelectedValue), txtIMEINo.Text, Convert.ToInt32(ddlVendor.SelectedValue),
                        rejectreason,
                        NGEAPRV, dcnewrate,
                        PURCHASEPERONVENDORPRICE, islistedfk, islistedamz, islistedweb, fkAmt, amzAmt, webAmt, fkper, amzper, webper, itemcode, status, IMAGE, MNLAPRREASON, Convert.ToDecimal(recomendedrate)
                        , PURFKAMT, PURAMZAMT, PURWEBAMT, PURFKPER, PURAMZPER, PURWEBPER, dcpurchaseper, ispurlistedfk, ispurlistedamz, ispurlistedweb
                        , Convert.ToInt32(ddlItemGroup.SelectedValue), Convert.ToInt32(ddlItemSubGroup.SelectedValue), ddlModel.SelectedItem.Text,"","",0, 12233);
                    //if (result > 0)
                    //{
                    ResetFormControl();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myVendorJs", "LoadVendorAutoSuggestJs();", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Record Add Successfully." + "\");", true);
                    //}
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='Login.aspx' });", true);
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

                    objBindDDL.FillModelSpecDropDown(ddlItemGroup, "ITEMGROUP");
                    ddlItemGroup.SelectedValue = "0";

                    objBindDDL.FillModelSpecDropDown(ddlItemSubGroup, "ITEMSUBGROUP");
                    ddlItemSubGroup.SelectedValue = "0";
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

                    ddlGrade.SelectedValue = "SELECT";
                    txtVendorStock.Text = "1";
                    txtVendorRate.Text = string.Empty;
                    ddlInvoice.SelectedValue = "-1";
                    ddlBox.SelectedValue = "-1";
                    ddlCharger.SelectedValue = "-1";
                    ddlMake.SelectedValue = "0";
                    txtRecomendRate.Text = string.Empty;
                    txtNewRate.Text = string.Empty;
                    hdNewRate.Value = "0";
                    txtIMEINo.Text = string.Empty;
                    ddlMake.Focus();
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


        public void BindMakeorModelAssociateDetail(string reqdropdown)
        {
            try
            {
                if (reqdropdown == "ItemGroup")
                {
                    if (ddlItemGroup.SelectedValue == "0")
                    {
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

                        txtNewRate.Text = string.Empty;
                        hdNewRate.Value = "0";
                        txtRecomendRate.Text = string.Empty;
                        hdNewRate.Value = "0";
                    }
                }
                else if (reqdropdown == "ItemSubGroup")
                {
                    if (ddlItemGroup.SelectedValue == "0")
                    {
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

                        txtNewRate.Text = string.Empty;
                        hdNewRate.Value = "0";
                        txtRecomendRate.Text = string.Empty;
                        hdNewRate.Value = "0";
                    }
                }
                else if (reqdropdown == "Model")
                {
                    if (ddlMake.SelectedValue != "0")
                    {
                        ddlModel.Items.Clear();
                        objBindDDL.FillMobexSellerOtherDeviceModel(ddlModel, Convert.ToInt32(ddlMake.SelectedValue), "entry",
                            Convert.ToInt32(ddlItemGroup.SelectedValue), Convert.ToInt32(ddlItemSubGroup.SelectedValue));
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
                        LoadNewRate();
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
                            objBindDDL.FillMobexSellerOtherDeviceRam(ddlRam, Convert.ToInt32(ddlMake.SelectedValue),
                            Convert.ToInt32(ddlModel.SelectedValue), Convert.ToInt32(ddlItemGroup.SelectedValue), Convert.ToInt32(ddlItemSubGroup.SelectedValue));
                        }

                        objBindDDL.FillMobexSellerOtherDeviceRom(ddlRom, Convert.ToInt32(ddlMake.SelectedValue),
                                    Convert.ToInt32(ddlModel.SelectedValue), Convert.ToInt32(ddlItemGroup.SelectedValue), Convert.ToInt32(ddlItemSubGroup.SelectedValue));

                        ddlColor.Items.Clear();
                        ddlColor.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                        ddlColor.SelectedValue = "0";
                        LoadRecomendedRate();
                        LoadNewRate();
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
                    }
                }
                else if (reqdropdown == "Color")
                {
                    if (ddlItemGroup.SelectedValue != "0" && ddlItemSubGroup.SelectedValue != "0")
                    {
                        //ddlColor.Items.Clear();
                        ddlColor.DataSource = null;
                        ddlColor.DataBind();

                        objBindDDL.FillMobexSellerOtherDeviceColor(ddlColor, Convert.ToInt32(ddlMake.SelectedValue),
                                   Convert.ToInt32(ddlModel.SelectedValue), ddlRam.Items.Count > 0 ? ddlRam.SelectedItem.Text : "",
                                   ddlRom.Items.Count > 0 ? ddlRom.SelectedItem.Text : ""
                                   , Convert.ToInt32(ddlItemGroup.SelectedValue), Convert.ToInt32(ddlItemSubGroup.SelectedValue));
                        ddlColor.SelectedValue = "0";
                        LoadRecomendedRate();
                        LoadNewRate();
                    }
                    else
                    {
                        txtNewRate.Text = string.Empty;
                        hdNewRate.Value = "0";
                        txtRecomendRate.Text = string.Empty;
                        ddlColor.Items.Clear();

                        ddlColor.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                        ddlColor.SelectedValue = "0";
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
                var dtRecomendedRate = objMainClass.GetOtherProductRecomendedRate(ddlMake.SelectedItem.Text, ddlModel.SelectedItem.Text,
                ddlRam.Items.Count > 0 ? ddlRam.SelectedItem.Text : "", ddlRom.Items.Count > 0 ? ddlRom.SelectedItem.Text : "", ddlGrade.SelectedItem.Text, ddlColor.SelectedItem.Text
                , Convert.ToInt32(ddlItemGroup.SelectedValue), Convert.ToInt32(ddlItemSubGroup.SelectedValue));

                if (dtRecomendedRate.Rows.Count > 0)
                {
                    txtRecomendRate.Text = dtRecomendedRate.Rows[0]["PURCHASERATE"].ToString();
                }
                else
                {
                    txtRecomendRate.Text = string.Empty;
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

                var dtNewRate = objMainClass.GetOtherProductNewRate(ddlMake.SelectedItem.Text, ddlModel.SelectedItem.Text,
                ddlRam.Items.Count > 0 ? ddlRam.SelectedItem.Text : "", ddlRom.Items.Count > 0 ? ddlRom.SelectedItem.Text : "", ddlColor.SelectedItem.Text,
                Convert.ToInt32(ddlItemGroup.SelectedValue), Convert.ToInt32(ddlItemSubGroup.SelectedValue)
                );

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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myVendorJs", "LoadVendorAutoSuggestJs();", true);
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
                if (Convert.ToInt32(ddlItemSubGroup.SelectedValue) == 205 ||
                  Convert.ToInt32(ddlItemSubGroup.SelectedValue) == 199 ||
                  Convert.ToInt32(ddlItemSubGroup.SelectedValue) == 170 ||
                  Convert.ToInt32(ddlItemSubGroup.SelectedValue) == 208
                  )
                {
                    BindMakeorModelAssociateDetail("Color");
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myVendorJs", "LoadVendorAutoSuggestJs();", true);
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myVendorJs", "LoadVendorAutoSuggestJs();", true);
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myVendorJs", "LoadVendorAutoSuggestJs();", true);
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myVendorJs", "LoadVendorAutoSuggestJs();", true);
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
                ITEMGRP = Convert.ToInt32(ddlItemGroup.SelectedValue);
                ITEMSUBGRP = Convert.ToInt32(ddlItemSubGroup.SelectedValue);
                var objGroupSubGroupDetail = GetGroupSubGroupShortName(ITEMGRP, ITEMSUBGRP);
                DataTable dtItemDetail = new DataTable();
                int maxItemcode = 0;
                DataRow[] rowItemd = null;
                dtItemDetail = objMainClass.GetItemMaxCode(ITEMGRP, ITEMSUBGRP, "GETALL");
                rowItemd = dtItemDetail.Select("ITEMDESC='" + itemdesc + "'");
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
                    dtNewItemCode = objMainClass.GetItemMaxCode(ITEMGRP, ITEMSUBGRP, "GETMAXITEMCODE");
                    if (dtNewItemCode.Rows.Count > 0)
                    {
                        maxItemcode = Convert.ToInt32(dtNewItemCode.Rows[0]["MAXITEMCODE"].ToString());
                        itemcode = objGroupSubGroupDetail.Group + objGroupSubGroupDetail.SubGroup + objMainClass.strConvertZeroPadding((maxItemcode.ToString()), "0", 6);
                        objMainClass.INSERTITEMDETAIL(1, itemcode, 17, 2, ITEMGRP, ITEMSUBGRP, 1, 1, 1, 1, 0, 1, 1,
                            itemdesc, itemdesc, 0, make, model, ITEMSUBGRP, 1);
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
                LoadNewRate();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myVendorJs", "LoadVendorAutoSuggestJs();", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void ddlItemSubGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ShowHideSubGroupSpec(Convert.ToInt32(ddlItemSubGroup.SelectedValue));
                BindMakeorModelAssociateDetail("ItemSubGroup");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myVendorJs", "LoadVendorAutoSuggestJs();", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public void ShowHideSubGroupSpec(int subGroupId)
        {
            try
            {
                if (subGroupId == 0)
                {
                    dvRam.Visible = false;
                    dvRom.Visible = false;
                    dvColor.Visible = false;
                    dvGrade.Visible = false;
                    dvVendorStock.Visible = false;
                    dvRecomendedRate.Visible = false;
                    dvSuggestRate.Visible = false;
                    dvVendorName.Visible = false;
                    dvInvoice.Visible = false;
                    dvBox.Visible = false;
                    dvCharger.Visible = false;
                    dvIMEI.Visible = false;
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GETITEMSUBGRP_SPECDETAIL(subGroupId);
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dt.Rows[0]["ITEMSHOWRAM"]) == 1)
                        {
                            dvRam.Visible = true;
                        }
                        else
                        {
                            dvRam.Visible = false;
                        }

                        if (Convert.ToInt32(dt.Rows[0]["ITEMSHOWROM"]) == 1)
                        {
                            dvRom.Visible = true;
                        }
                        else
                        {
                            dvRom.Visible = false;
                        }

                        if (Convert.ToInt32(dt.Rows[0]["ITEMSHOWCOLOR"]) == 1)
                        {
                            dvColor.Visible = true;
                        }
                        else
                        {
                            dvColor.Visible = false;
                        }

                        dvGrade.Visible = true;
                        dvVendorStock.Visible = true;
                        dvRecomendedRate.Visible = true;
                        dvSuggestRate.Visible = true;
                        dvVendorName.Visible = true;
                        dvInvoice.Visible = true;
                        dvBox.Visible = true;
                        dvCharger.Visible = true;
                        dvIMEI.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void ddlItemGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindMakeorModelAssociateDetail("ItemGroup");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public GroupSubGroupShortName GetGroupSubGroupShortName(int ITEMGRPID, int ITEMSUBGRPID)
        {
            GroupSubGroupShortName objclas = new GroupSubGroupShortName();
            try
            {
                DataTable dt = new DataTable();
                dt = objMainClass.GetMainGroupSubGroupShortName(ITEMGRPID, ITEMSUBGRPID);
                if (dt.Rows.Count > 0)
                {
                    objclas.Group = dt.Rows[0]["ITEMGRPSHORTNAME"].ToString();
                    objclas.SubGroup = dt.Rows[0]["ITEMSUBGRPSHORTNAME"].ToString();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return objclas;
        }


        // Auto Item Code Functionality 
    }
}