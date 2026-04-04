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
    public partial class RejectedPriceApproveDetail : System.Web.UI.Page
    {
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        bool Approverright = false;
        int ITEMGRP = 0;
        int ITEMSUBGRP       = 0;
        DataRow[] rowItemd    = null;
        Decimal   maxItemcode = 0;
        string ITEMGRPSHORTNAME = "";
        string ITEMSUBGRPSHORTNAME = "";
        string ITEMGRPNAME = "";
        string ITEMSUBGRPNAME = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["USERID"] != null)
                    {
                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menutabApproverid.Value, "");
                        Approverright = FormRights.bView;
                        if (!Approverright)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
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

        #region PAGEMETHOD
        public void BindProductDetail()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    int status  = 0;
                    int ID      = 0;
                    int.TryParse(txtID.Text, out ID);

                    status      = Convert.ToInt32(PRODUCTSTATUS.REJECTED);
                    gvProduct.DataSource = null;
                    gvProduct.DataBind();

                    DataTable dt = new DataTable();
                    dt = objMainClass.GetProductEntryDetail("", "", "", status, 0, ID);
                    gvProduct.DataSource = dt;
                    gvProduct.DataBind();
                    lgrecordcount.InnerText = "Records : " + dt.Rows.Count.ToString();

                    if (FormRights.bAdd == false)
                    {
                        gvProduct.Columns[0].Visible = false;
                    }
                    else
                    {
                        gvProduct.Columns[0].Visible = true;
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

        public void ResetFormControl()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    txtID.Text = string.Empty;
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
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grdrow   = (GridViewRow)((LinkButton)sender).NamingContainer;
                //string purrate     = ((TextBox)grdrow.FindControl("txtMobexRate")).Text;
                //string purqty      = ((TextBox)grdrow.FindControl("txtPurchaseqty")).Text;
                string newrate       = ((TextBox)grdrow.FindControl("txtNewRate")).Text;
                int ID               = Convert.ToInt32(((HiddenField)grdrow.FindControl("hdID")).Value);
                //if (purrate.Length == 0 || newrate.Length == 0)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Mobex Rate and Purchase Qty is required for Listed.');", true);
                //}
                //else
                //{
                    //string purchaseper  = ((Label)grdrow.FindControl("lblPercentageValue")).Text;
                    //Double fkAmt = 0, amzAmt = 0, webAmt = 0, dcpurvalue = 0, dcnewrate = 0, dcpurchaseper = 0, fkper = 0, amzper = 0, webper = 0;
                    //Double.TryParse(newrate, out dcnewrate);
                    //Double.TryParse(purrate, out dcpurvalue);
                    //int islistedfk = 0, islistedamz = 0, islistedweb = 0;

                    //Calculate PlatForm Selling Amount 
                    //if (dcpurvalue > 0)
                    //{
                    //    fkAmt = (((dcpurvalue + 700)) * (1.21));
                    //    fkAmt = (Math.Floor(fkAmt * Math.Pow(10, -2)) / Math.Pow(10, -2));

                    //    amzAmt = (((dcpurvalue + 700)) * (1.165));
                    //    amzAmt = (Math.Floor(amzAmt * Math.Pow(10, -2)) / Math.Pow(10, -2));

                    //    webAmt = (((dcpurvalue + 1400)) * (1.1));
                    //    webAmt = (Math.Floor(webAmt * Math.Pow(10, -2)) / Math.Pow(10, -2));
                    //}



                    //Calculate PlatForm Per Amount 
                    //if (fkAmt != 0)
                    //{
                    //    fkper = Math.Round((((fkAmt * 100)) / dcnewrate), 0);
                    //}

                    //if (amzAmt != 0)
                    //{
                    //    amzper = Math.Round((((amzAmt * 100)) / dcnewrate), 0);
                    //}

                    //if (webAmt != 0)
                    //{
                    //    webper = Math.Round((((webAmt * 100)) / dcnewrate), 0);
                    //}

                    //if (dcpurvalue > 0 && dcnewrate > 0)
                    //{
                    //    dcpurchaseper = Math.Round(((dcpurvalue * 100)) / (dcnewrate), 0);
                    //}

                    //Calculate PlatForm Listed Detail 
                    //islistedfk = fkper >= 91 ? 0 : 1;
                    //islistedamz = amzper >= 91 ? 0 : 1;
                    //islistedweb = webper >= 91 ? 0 : 1;

                    //Extra Condition Code
                    string make         = (grdrow.FindControl("lblMake") as Label).Text;
                    string mobexgrade   = (grdrow.FindControl("lblVendorGrade") as Label).Text;
                    string vendorgrade  = (grdrow.FindControl("hdVendorGrade") as HiddenField).Value;

                    ITEMGRP = Convert.ToInt32((grdrow.FindControl("hdItemGrpId") as HiddenField).Value);
                    ITEMSUBGRP = Convert.ToInt32((grdrow.FindControl("hdItemSubGrpId") as HiddenField).Value);

                    ITEMGRPNAME = (grdrow.FindControl("hdItemGrpName") as HiddenField).Value;
                    ITEMSUBGRPNAME = (grdrow.FindControl("hdItemSubGrpName") as HiddenField).Value;

                    ITEMGRPSHORTNAME = (grdrow.FindControl("hdItemGrpShortName") as HiddenField).Value;
                    ITEMSUBGRPSHORTNAME = (grdrow.FindControl("hdItemSubGrpShortName") as HiddenField).Value;

                //for Amazon
                //if (make.ToUpper() == "APPLE")
                //{
                //    islistedamz = 0;
                //}
                //else
                //{
                //    if ((mobexgrade == "B" || mobexgrade == "C") || (vendorgrade == "B" || vendorgrade == "C"))
                //    {
                //        islistedamz = 0;
                //    }
                //    else
                //    {
                //        var dtAmazone = objMainClass.GetPlatFormNotListed((PLATFORM.Amazon.ToString()), make, (grdrow.FindControl("lblModel") as Label).Text, (grdrow.FindControl("lblVendorGrade") as Label).Text, Convert.ToInt32(Session["USERID"].ToString()));
                //        if (dtAmazone.Rows.Count > 0)
                //        {
                //            islistedamz = 0;
                //        }
                //    }
                //}

                //for Amazon

                //for Web
                //if ((mobexgrade == "C") || (vendorgrade == "C"))
                //{
                //    islistedweb = 0;
                //}
                //for Web


                //for Flipcart
                //if (fkAmt > 15000)
                //{
                //    islistedfk = 0;
                //}
                //for Flipcart

                //Extra Condition Code

                //Get Item  Code 
                string itemcode = "";
                string itemdesc = "";
                //if(islistedfk == 1 || islistedamz == 1 || islistedweb == 1)
                //{

                if (ITEMGRP == 9 && ITEMSUBGRP == 168)
                {
                    itemdesc = make + " " + (grdrow.FindControl("lblModel") as Label).Text + " "
                                    + (grdrow.FindControl("lblRam") as Label).Text + " "
                                    + (grdrow.FindControl("lblRom") as Label).Text + " "
                                    + (grdrow.FindControl("lblColor") as Label).Text + " MOBILE DEVICE (USED) (GRADE "
                                    + "A"  + ")"; // (grdrow.FindControl("lblVendorGrade") as Label).Text
                }
                else
                {
                    itemdesc = make + " " + (grdrow.FindControl("lblModel") as Label).Text + " " + ITEMSUBGRPNAME
                                    + " (" + (grdrow.FindControl("lblColor") as Label).Text + ") (GRADE "
                                    + "A" + ")"; //(grdrow.FindControl("lblVendorGrade") as Label).Text
                }

                 itemcode = GetItemCode(itemdesc, make, (grdrow.FindControl("lblModel") as Label).Text);
                //}
                //Get Item  Code 

                objMainClass.RejectedMobileApproved(ID, "APPROVED", Convert.ToInt32(Session["USERID"].ToString()), 11229, itemcode);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Product Approved Successfully." + "\");", true);
                BindProductDetail();
                //}
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
                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                string purrate = ((TextBox)grdrow.FindControl("txtMobexRate")).Text;
                //string purqty = ((TextBox)grdrow.FindControl("txtPurchaseqty")).Text;
                string newrate = ((TextBox)grdrow.FindControl("txtNewRate")).Text;

                if (((TextBox)grdrow.FindControl("txtRejectReason")).Text.Length == 0 || (purrate.Length == 0 || newrate.Length == 0))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please enter the Reject Reason,Mobex Rate,New Rate for Reject.');", true);
                }
                else
                {
                    int ID = Convert.ToInt32(((HiddenField)grdrow.FindControl("hdID")).Value);
                    Double fkAmt = 0, amzAmt = 0, webAmt = 0, dcpurvalue = 0, dcnewrate = 0, dcpurchaseper = 0, fkper = 0, amzper = 0, webper = 0;
                    Double.TryParse(newrate, out dcnewrate);
                    Double.TryParse(purrate, out dcpurvalue);

                    //Calculate PlatForm Selling Amount 
                    if (dcpurvalue > 0)
                    {
                        fkAmt = (((dcpurvalue + 700)) * (1.21));
                        fkAmt = (Math.Floor(fkAmt * Math.Pow(10, -2)) / Math.Pow(10, -2));

                        amzAmt = (((dcpurvalue + 700)) * (1.165));
                        amzAmt = (Math.Floor(amzAmt * Math.Pow(10, -2)) / Math.Pow(10, -2));

                        webAmt = (((dcpurvalue + 1400)) * (1.1));
                        webAmt = (Math.Floor(webAmt * Math.Pow(10, -2)) / Math.Pow(10, -2));
                    }

                    //Calculate PlatForm Per Amount 
                    if (fkAmt != 0)
                    {
                        fkper = Math.Round((((fkAmt * 100)) / dcnewrate), 2);
                    }

                    if (amzAmt != 0)
                    {
                        amzper = Math.Round((((amzAmt * 100)) / dcnewrate), 2);
                    }

                    if (webAmt != 0)
                    {
                        webper = Math.Round((((webAmt * 100)) / dcnewrate), 2);
                    }

                    if (dcpurvalue > 0 && dcnewrate > 0)
                    {
                        dcpurchaseper = Math.Round(((dcpurvalue * 100)) / (dcnewrate), 2);
                    }

                    //objMainClass.UpdatePurchaseDetail("1", Convert.ToDecimal(purrate), "REJECTED",
                    //Convert.ToInt32(Session["USERID"].ToString()), 11233, ID, dcnewrate, dcpurchaseper, 0
                    //, 0, 0, fkAmt, amzAmt, webAmt, fkper, amzper, webper, "","",0);

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Product Reject Successfully." + "\");", true);
                    BindProductDetail();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
        protected void gvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    var dtRecomendedRate = objMainClass.GetProductRecomendedRate((e.Row.FindControl("lblMake") as Label).Text,
                        (e.Row.FindControl("lblModel") as Label).Text,
                    (e.Row.FindControl("lblRam") as Label).Text, (e.Row.FindControl("lblRom") as Label).Text, (e.Row.FindControl("lblVendorGrade") as Label).Text, (e.Row.FindControl("lblColor") as Label).Text);

                    if (dtRecomendedRate.Rows.Count > 0)
                    {
                        (e.Row.FindControl("lblRecomendedRate") as Label).Text = dtRecomendedRate.Rows[0]["PURCHASERATE"].ToString();
                    }

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
                    + "','" + (e.Row.FindControl("lblVendorGrade") as Label).Text
                    + "','" + (e.Row.FindControl("hdVendorGrade") as HiddenField).Value + "');");


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
                    + "','" + (e.Row.FindControl("lblVendorGrade") as Label).Text
                    + "','" + (e.Row.FindControl("hdVendorGrade") as HiddenField).Value + "');");

                    (e.Row.FindControl("txtNewRate") as TextBox).Text = (e.Row.FindControl("lblNewPrice") as Label).Text;
                    (e.Row.FindControl("txtMobexRate") as TextBox).Text = (e.Row.FindControl("lblVendorRate") as Label).Text;
                    //Initiate Value 
                    string chknewrate = (e.Row.FindControl("txtNewRate") as TextBox).Text;
                    Double dcchknewrate = 0;
                    Double.TryParse(chknewrate, out dcchknewrate);

                    //Convert.ToDecimal((e.Row.FindControl("lblNewPrice") as Label).Text)
                    if (dcchknewrate != 0)
                    {

                        //Calculate FK,AMZ,WEB Amount
                        string purrate = (e.Row.FindControl("lblVendorRate") as Label).Text;
                        string newrate = (e.Row.FindControl("txtNewRate") as TextBox).Text;

                        Double fkAmt = 0, amzAmt = 0, webAmt = 0, dcpurvalue = 0, dcnewrate = 0, dcpurchaseper = 0, fkper = 0, amzper = 0, webper = 0;
                        Double.TryParse(newrate, out dcnewrate);
                        Double.TryParse(purrate, out dcpurvalue);
                        int islistedfk = 0, islistedamz = 0, islistedweb = 0;

                        //Calculate PlatForm Selling Amount 
                        if (dcpurvalue > 0)
                        {
                            fkAmt = (((dcpurvalue + 700)) * (1.21));
                            fkAmt = (Math.Floor(fkAmt * Math.Pow(10, -2)) / Math.Pow(10, -2));

                            amzAmt = (((dcpurvalue + 700)) * (1.165));
                            amzAmt = (Math.Floor(amzAmt * Math.Pow(10, -2)) / Math.Pow(10, -2));

                            webAmt = (((dcpurvalue + 1400)) * (1.1));
                            webAmt = (Math.Floor(webAmt * Math.Pow(10, -2)) / Math.Pow(10, -2));
                        }

                        //Calculate PlatForm Per Amount 
                        if (fkAmt != 0)
                        {
                            fkper = Math.Round((((fkAmt * 100)) / dcnewrate), 0);
                        }

                        if (amzAmt != 0)
                        {
                            amzper = Math.Round((((amzAmt * 100)) / dcnewrate), 0);
                        }

                        if (webAmt != 0)
                        {
                            webper = Math.Round((((webAmt * 100)) / dcnewrate), 0);
                        }

                        if (dcpurvalue > 0 && dcnewrate > 0)
                        {
                            dcpurchaseper = Math.Round(((dcpurvalue * 100)) / (dcnewrate), 0);
                        }

                        //Calculate PlatForm Listed Detail 
                        islistedfk = fkper >= 91 ? 0 : 1;
                        islistedamz = amzper >= 91 ? 0 : 1;
                        islistedweb = webper >= 91 ? 0 : 1;

                        //Extra Condition Code
                        string make = (e.Row.FindControl("lblMake") as Label).Text;
                        string mobexgrade = (e.Row.FindControl("lblVendorGrade") as Label).Text;
                        string vendorgrade = (e.Row.FindControl("hdVendorGrade") as HiddenField).Value;

                        //for Amazon
                        if (make.ToUpper() == "APPLE")
                        {
                            islistedamz = 0;
                        }
                        else
                        {
                            if ((mobexgrade == "B" || mobexgrade == "C") || (vendorgrade == "B" || vendorgrade == "C"))
                            {
                                islistedamz = 0;
                            }
                        }

                        //for Amazon

                        //for Web
                        if ((mobexgrade == "C") || (vendorgrade == "C"))
                        {
                            islistedweb = 0;
                        }
                        //for Web


                        //for Flipcart
                        if (fkAmt > 15000)
                        {
                            islistedfk = 0;
                        }
                        //for Flipcart

                        //Extra Condition Code
                        (e.Row.FindControl("txtFKRate") as TextBox).Text = fkAmt.ToString();
                        (e.Row.FindControl("txtAmzRate") as TextBox).Text = amzAmt.ToString();
                        (e.Row.FindControl("txtWebRate") as TextBox).Text = webAmt.ToString();
                        (e.Row.FindControl("lblPurcPerValue") as Label).Text = "PUR:" + dcpurchaseper.ToString() + "%";
                        (e.Row.FindControl("lblFkPerValue") as Label).Text = "FK:" + fkper.ToString() + "%";
                        (e.Row.FindControl("lblAmzPerValue") as Label).Text = "AMZ:" + amzper.ToString() + "%";
                        (e.Row.FindControl("lblWebPerValue") as Label).Text = "WEB:" + webper.ToString() + "%";

                        if (islistedfk == 1)
                        {
                            (e.Row.FindControl("lblFkPerValue") as Label).ForeColor = System.Drawing.Color.Black;
                            (e.Row.FindControl("lblFkPerValue") as Label).Attributes.Add("color", "black");
                        }
                        else
                        {
                            (e.Row.FindControl("lblFkPerValue") as Label).ForeColor = System.Drawing.Color.Red;
                            (e.Row.FindControl("lblFkPerValue") as Label).Attributes.Add("color", "red");
                        }

                        if (islistedamz == 1)
                        {
                            (e.Row.FindControl("lblAmzPerValue") as Label).ForeColor = System.Drawing.Color.Black;
                            (e.Row.FindControl("lblAmzPerValue") as Label).Attributes.Add("color", "black");
                        }
                        else
                        {
                            (e.Row.FindControl("lblAmzPerValue") as Label).ForeColor = System.Drawing.Color.Red;
                            (e.Row.FindControl("lblAmzPerValue") as Label).Attributes.Add("color", "red");
                        }

                        if (islistedweb == 1)
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
                            objMainClass.INSERTITEMDETAIL(1, itemcode, 17, 2,9, 168,1,1,1,1,0,1,1,
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