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
    public partial class JangadKROListing : System.Web.UI.Page
    {
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        bool Checkerright = false;
        //Item Creation Variable
        int ITEMGRP = 9;
        int ITEMSUBGRP = 168;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    objBindDDL.FillLists(ddlSelectFBALocation, "FBAL");
                    txtFromDate.Text = (objMainClass.indianTime.Date.AddDays(-400)).ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    txtToDate.Text   = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    if (Session["USERID"] != null)
                    {
                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menutabCheckerid.Value, "");
                        Checkerright = FormRights.bView;
                        
                        if (!Checkerright)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        BindPageDropDown();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                    }
                }

                if (gvProduct.Rows.Count > 0)
                {
                    gvProduct.HeaderRow.TableSection = TableRowSection.TableHeader;
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
        public void BindPageDropDown()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    objBindDDL.FillMobexSellerVendor(ddlVendor);
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

        public void BindProductDetail()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    int status;
                    status = Convert.ToInt32(PRODUCTSTATUS.PURCHASE);
                    gvProduct.DataSource = null;
                    gvProduct.DataBind();
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetJangadListing(txtFromDate.Text, txtToDate.Text, (ddlVendor.SelectedValue == "0" ? "" : ddlVendor.SelectedItem.Text),ddlListingType.SelectedValue, status, Convert.ToInt32(Session["USERID"].ToString()) ,Convert.ToInt32(txtListingID.Text.Length > 0 ? txtListingID.Text : "0"),"", txtIMEINo.Text);
                    gvProduct.DataSource = dt;
                    gvProduct.DataBind();
                    gvProduct.HeaderRow.TableSection = TableRowSection.TableHeader;
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

        public void ResetFormControl()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    txtFromDate.Text = (objMainClass.indianTime.Date.AddDays(-30)).ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    txtToDate.Text   = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
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
                GridViewRow grdrow          = (GridViewRow)((LinkButton)sender).NamingContainer;
                string ID                   = ((HiddenField)grdrow.FindControl("hdID")).Value;
                string inwardresult         = ((Label)grdrow.FindControl("lblInwardResult")).Text;
                string hdinwardechange      = ((HiddenField)grdrow.FindControl("hdinwardechange")).Value;
                string sono                 = ((Label)grdrow.FindControl("lblSONO")).Text;
                int lblDCNO                 = Convert.ToInt32(((Label)grdrow.FindControl("lblDCNO")).Text);

                if (sono.Length == 0 || sono == "4000039858" || sono == "4000033043" || sono == "4000043615" ||
                    sono == "4000043616")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('PR and PO is not possible because we have not received the order against this listing.');", true);
                }
                //else if (lblDCNO == 0 && IsPossibletoCreateReturnableDc(sono) == true)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('PR and PO is not possible because sales order is a Dekh ke Lo so you should create the Returnable DC First.');", true);
                //}
                else
                {
                    if (inwardresult == "PASS")
                    {
                        PRPOCREATION(Convert.ToInt32(ID), Convert.ToInt32(hdinwardechange));
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Auto Po and PR not possible due to Inward Result is Fail.');", true);
                    }   
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
        public string PRPOCREATION(int LISTINGID, int hdinwardechange)
        {
            string responsestr = string.Empty;
            // Item code creation if not exist 
            DataTable dtDetailsCheck = new DataTable();
            dtDetailsCheck = objMainClass.GetDetailFromListingID(objMainClass.intCmpId, LISTINGID, "GETDETAILFROMLISTINGID");
            if (Convert.ToString(dtDetailsCheck.Rows[0]["ITEMCODE"]) == null || Convert.ToString(dtDetailsCheck.Rows[0]["ITEMCODE"]) == string.Empty
                || Convert.ToString(dtDetailsCheck.Rows[0]["ITEMCODE"]) == "")
            {
                string itemcode = "";
                string itemdesc = Convert.ToString(dtDetailsCheck.Rows[0]["MAKE"]) + " " + Convert.ToString(dtDetailsCheck.Rows[0]["MODEL"]) + " "
                                       + Convert.ToString(dtDetailsCheck.Rows[0]["RAM"]) + " "
                                       + Convert.ToString(dtDetailsCheck.Rows[0]["ROM"]) + " "
                                       + Convert.ToString(dtDetailsCheck.Rows[0]["COLOR"]) + " MOBILE DEVICE (USED) (GRADE "
                                       + "A" + ")"; // Convert.ToString(dtDetailsCheck.Rows[0]["VENDORGRADE"])
                itemcode = GetItemCode(itemdesc, Convert.ToString(dtDetailsCheck.Rows[0]["MAKE"]), Convert.ToString(dtDetailsCheck.Rows[0]["MODEL"]));

                objMainClass.UpdateItemBeforeCreatePRPO(objMainClass.intCmpId, LISTINGID, itemcode);
            }
            // Item code creation if not exist 


            DataTable dtDetails = new DataTable();
            dtDetails = objMainClass.GetDetailFromListingID(objMainClass.intCmpId, LISTINGID, "GETDETAILFROMLISTINGID");

            if (dtDetails.Rows.Count > 0)
            {
                if (Convert.ToString(dtDetails.Rows[0]["VENDCODE"]) != null && Convert.ToString(dtDetails.Rows[0]["VENDCODE"]) != string.Empty && Convert.ToString(dtDetails.Rows[0]["VENDCODE"]) != "")
                {
                    if (Convert.ToString(dtDetails.Rows[0]["ITEMCODE"]) != null && Convert.ToString(dtDetails.Rows[0]["ITEMCODE"]) != string.Empty && Convert.ToString(dtDetails.Rows[0]["ITEMCODE"]) != "")
                    {
                        if (Convert.ToString(dtDetails.Rows[0]["ITEMCODE"]) != null && Convert.ToString(dtDetails.Rows[0]["ITEMCODE"]) != string.Empty && Convert.ToString(dtDetails.Rows[0]["ITEMCODE"]) != "")
                        {
                            GridView grvData = new GridView();
                            grvData.DataSource = dtDetails;
                            grvData.DataBind();
                            string PRNO = objMainClass.SavePPRFromListing("MPR", DateTime.Now.ToString(), "MOBEX AUTO PR SYSTEM" + Convert.ToString(dtDetails.Rows[0]["DEALERNAME"])+ " " + Convert.ToString(dtDetails.Rows[0]["IMEINO"]), Convert.ToString((int)DEPT.SmartPhone), grvData, objMainClass.strBackGroundUser, Convert.ToString(LISTINGID), Convert.ToString(dtDetails.Rows[0]["VENDCODE"]));
                            DataTable dt = objMainClass.GetItemID("MPMC000633", "GetItemID");
                            DataTable dt1 = objMainClass.GetItemID("MPDC000185", "GetItemID");
                            string ItemId1 = Convert.ToString(dt.Rows[0]["ITEMID"]);
                            string ItemDec1 = Convert.ToString(dt.Rows[0]["ITEMDESC"]);
                            string ItemGroupId1 = Convert.ToString(dt.Rows[0]["ITEMGRP"]);

                            string ItemId2 = Convert.ToString(dt1.Rows[0]["ITEMID"]);
                            string ItemDec2 = Convert.ToString(dt1.Rows[0]["ITEMDESC"]);
                            string ItemGroupId2 = Convert.ToString(dt.Rows[0]["ITEMGRP"]);


                            if (hdinwardechange == 1)
                            {
                                grvData.Rows[0].Cells[5].Text = ItemId1;
                                grvData.Rows[0].Cells[18].Text = "1";
                                grvData.Rows[0].Cells[25].Text = "1";
                                grvData.Rows[0].Cells[6].Text = ItemDec1;
                                grvData.Rows[0].Cells[7].Text = ItemGroupId1;
                                objMainClass.SavePRdetails(grvData, PRNO, "MPR", "2");
                            }
                            else if (hdinwardechange == 2)
                            {
                                grvData.Rows[0].Cells[5].Text = ItemId2;
                                grvData.Rows[0].Cells[18].Text = "1";
                                grvData.Rows[0].Cells[25].Text = "1";
                                grvData.Rows[0].Cells[6].Text = ItemDec2;
                                grvData.Rows[0].Cells[7].Text = ItemGroupId2;
                                objMainClass.SavePRdetails(grvData, PRNO, "MPR", "2");
                            }
                            else if (hdinwardechange == 3)
                            {
                                grvData.Rows[0].Cells[5].Text = ItemId1;
                                grvData.Rows[0].Cells[18].Text = "1";
                                grvData.Rows[0].Cells[25].Text = "1";
                                grvData.Rows[0].Cells[7].Text = ItemGroupId2;
                                grvData.Rows[0].Cells[6].Text = ItemDec1;
                                objMainClass.SavePRdetails(grvData, PRNO, "MPR", "2");
                                grvData.Rows[0].Cells[5].Text = ItemId2;
                                grvData.Rows[0].Cells[18].Text = "1";
                                grvData.Rows[0].Cells[25].Text = "1";
                                grvData.Rows[0].Cells[6].Text = ItemDec2;
                                grvData.Rows[0].Cells[7].Text = ItemGroupId2;
                                objMainClass.SavePRdetails(grvData, PRNO, "MPR", "3");
                            }

                            if (PRNO != "" && PRNO != "" && PRNO != string.Empty)
                            {
                                responsestr = "PR created successfully. PR No. : " + PRNO;
                                Response.Redirect("../MM/CreatePO.aspx?PRNO=" + PRNO + "&VENDCODE=" + Convert.ToString(dtDetails.Rows[0]["VENDCODE"]), false);
                            }
                            else
                            {
                                responsestr = "Something went wrong. PR not created.";
                            }
                        }
                        else
                        {
                            responsestr = "Plant and Location code not declared for this area. Area is " + Convert.ToString(dtDetails.Rows[0]["AREA"]);
                        }
                    }
                    else
                    {
                        responsestr = "Item code not created for this Listing ID " + LISTINGID;
                    }
                }
                else
                {
                    responsestr = "Vendor not created for dealer. Dealer name is " + Convert.ToString(dtDetails.Rows[0]["DEALERNAME"]);
                }
            }
            else
            {
                responsestr = "No data found with this Listing ID " + LISTINGID;
            }


            return responsestr;
        }

        public string GetItemCode(string itemdesc, string make, string model)
        {
            string itemcode = string.Empty;
            try
            {
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
                        itemcode = "MD" + "UD" + objMainClass.strConvertZeroPadding((maxItemcode.ToString()), "0", 6);
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

        protected void btRelist_Click(object sender, EventArgs e)
        {
            try
            {
                ResetRelist();
                Decimal lockamount = 0;
                GridViewRow grdrow      = (GridViewRow)((LinkButton)sender).NamingContainer;
                hdprimarykey.Value      = ((HiddenField)grdrow.FindControl("hdID")).Value;
                hdMake.Value            = ((Label)grdrow.FindControl("lblMake")).Text;
                hdModel.Value           = ((Label)grdrow.FindControl("lblModel")).Text;
                hdRam.Value             = ((Label)grdrow.FindControl("lblRam")).Text;
                hdRom.Value             = ((Label)grdrow.FindControl("lblRom")).Text;
                hdcolor.Value           = ((Label)grdrow.FindControl("lblColor")).Text;
                hdGrade.Value           = ((Label)grdrow.FindControl("lblVendorGrade")).Text;
                txtVendorprice.Text     = ((Label)grdrow.FindControl("lblVendorRate")).Text;
                var dtLockamount        = objMainClass.GetProductFinalListingRate(hdMake.Value,
                                              hdModel.Value,hdRam.Value,hdRom.Value,hdGrade.Value,hdcolor.Value);
                if(dtLockamount.Rows.Count > 0)
                {
                    lockamount = Convert.ToDecimal(dtLockamount.Rows[0]["FinalApproveListingAmount"]);
                }
                hdLockAmount.Value      = lockamount.ToString();
                objBindDDL.FillLists(ddlmaxday, "LED");
                ddlmaxday.SelectedValue = "0";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                ddlmaxday.Focus();
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
                if (Session["USERID"] != null)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        string jangadolddays        = ((Label)e.Row.FindControl("lblJangadOldDays")).Text;
                        string fbalocation          = ((Label)e.Row.FindControl("lblFBALocation")).Text;
                        int lblDCNO                 = Convert.ToInt32(((Label)e.Row.FindControl("lblDCNO")).Text);
                        string dcstatus             = Convert.ToString(((Label)e.Row.FindControl("lblDcStatus")).Text);
                        int schemeid                = Convert.ToInt32(((Label)e.Row.FindControl("lblSchemeID")).Text);
                        string sono                 = ((Label)e.Row.FindControl("lblSONO")).Text;

                        DropDownList ddlFBALocation = ((DropDownList)e.Row.FindControl("ddlFBALocation"));
                        int jangadolddaysvalue;
                        int.TryParse(jangadolddays,out jangadolddaysvalue);
                        ((LinkButton)e.Row.FindControl("btRelist")).Visible = true;

                        if(schemeid == 11914)
                        {
                            if((dcstatus.ToUpper()) == "DELIVERED")
                            {
                                ((LinkButton)e.Row.FindControl("btRtnDc")).Visible = false;
                                ((LinkButton)e.Row.FindControl("btnRtnDcprint")).Visible = false;
                                ((LinkButton)e.Row.FindControl("btnRtnDcView")).Visible = true;
                                if (fbalocation == null || fbalocation.Length == 0 || fbalocation == "--")
                                {
                                    ((LinkButton)e.Row.FindControl("btnRemoveFromFBA")).Visible = false;
                                    
                                    ((LinkButton)e.Row.FindControl("btReturn")).Visible         = false;
                                    ((LinkButton)e.Row.FindControl("btRelist")).Visible         = true;
                                    ((LinkButton)e.Row.FindControl("btnQc")).Visible            = true;
                                }
                                else
                                {
                                    ((LinkButton)e.Row.FindControl("btnRemoveFromFBA")).Visible = true;
                                    ((LinkButton)e.Row.FindControl("btReturn")).Visible = false;
                                    ((LinkButton)e.Row.FindControl("btRelist")).Visible = true;
                                    ((LinkButton)e.Row.FindControl("btnQc")).Visible = false;
                                }
                            }
                            else if ((dcstatus.ToUpper()) == "PENDING")
                            {
                                ((LinkButton)e.Row.FindControl("btRtnDc")).Visible = false;
                                ((LinkButton)e.Row.FindControl("btnRtnDcprint")).Visible = true;
                                ((LinkButton)e.Row.FindControl("btnRtnDcView")).Visible = true;
                                if (fbalocation == null || fbalocation.Length == 0 || fbalocation == "--")
                                {
                                    ((LinkButton)e.Row.FindControl("btnRemoveFromFBA")).Visible = false;
                                    ((LinkButton)e.Row.FindControl("btReturn")).Visible = false;
                                    ((LinkButton)e.Row.FindControl("btRelist")).Visible = true;
                                    ((LinkButton)e.Row.FindControl("btnQc")).Visible = false;
                                }
                                else
                                {
                                    ((LinkButton)e.Row.FindControl("btnRemoveFromFBA")).Visible = true;
                                    ((LinkButton)e.Row.FindControl("btReturn")).Visible = false;
                                    ((LinkButton)e.Row.FindControl("btRelist")).Visible = true;
                                    ((LinkButton)e.Row.FindControl("btnQc")).Visible = false;
                                }
                            }
                            else if ((dcstatus.ToUpper()) == "RETURNREQUESTGENERATED")
                            {
                                ((LinkButton)e.Row.FindControl("btRtnDc")).Visible = false;
                                ((LinkButton)e.Row.FindControl("btnRtnDcprint")).Visible = false;
                                ((LinkButton)e.Row.FindControl("btnRtnDcView")).Visible = true;
                                if (fbalocation == null || fbalocation.Length == 0 || fbalocation == "--")
                                {
                                    ((LinkButton)e.Row.FindControl("btnRemoveFromFBA")).Visible = false;
                                    ((LinkButton)e.Row.FindControl("btReturn")).Visible = false;
                                    ((LinkButton)e.Row.FindControl("btRelist")).Visible = false;
                                    ((LinkButton)e.Row.FindControl("btnQc")).Visible = false;

                                }
                                else
                                {
                                    ((LinkButton)e.Row.FindControl("btnRemoveFromFBA")).Visible = true;
                                    ((LinkButton)e.Row.FindControl("btReturn")).Visible = false;
                                    ((LinkButton)e.Row.FindControl("btRelist")).Visible = false;
                                    ((LinkButton)e.Row.FindControl("btnQc")).Visible = false;
                                }
                            }
                            else if ((dcstatus.ToUpper()) == "RETURNED")
                            {
                                ((LinkButton)e.Row.FindControl("btRtnDc")).Visible = true;
                                ((LinkButton)e.Row.FindControl("btnRtnDcprint")).Visible = false;
                                ((LinkButton)e.Row.FindControl("btnRtnDcView")).Visible = false;
                                if (fbalocation == null || fbalocation.Length == 0 || fbalocation == "--")
                                {
                                    ((LinkButton)e.Row.FindControl("btnRemoveFromFBA")).Visible = false;

                                    ((LinkButton)e.Row.FindControl("btReturn")).Visible         = true;
                                    ((LinkButton)e.Row.FindControl("btRelist")).Visible         = true;
                                    ((LinkButton)e.Row.FindControl("btnQc")).Visible            = false;
                                }
                                else
                                {
                                    ((LinkButton)e.Row.FindControl("btnRemoveFromFBA")).Visible = true;
                                    ((LinkButton)e.Row.FindControl("btReturn")).Visible         = false;
                                    ((LinkButton)e.Row.FindControl("btRelist")).Visible         = false;
                                    ((LinkButton)e.Row.FindControl("btnQc")).Visible            = false;
                                }
                        
                            }
                            else if ((dcstatus.ToUpper()) == "")
                            {
                                if (sono.Length == 0 || sono == "4000039858" || sono == "4000033043" || sono == "4000043615" ||
                                    sono == "4000043616"
                                   )
                                {
                                    ((LinkButton)e.Row.FindControl("btRtnDc")).Visible = false;
                                }
                                else
                                {
                                    ((LinkButton)e.Row.FindControl("btRtnDc")).Visible = true;
                                }

                                ((LinkButton)e.Row.FindControl("btnRtnDcprint")).Visible = false;
                                ((LinkButton)e.Row.FindControl("btnRtnDcView")).Visible = false;

                                if (fbalocation == null || fbalocation.Length == 0 || fbalocation == "--")
                                {
                                    ((LinkButton)e.Row.FindControl("btnRemoveFromFBA")).Visible = false;
                                    ((LinkButton)e.Row.FindControl("btReturn")).Visible = true;
                                    ((LinkButton)e.Row.FindControl("btRelist")).Visible = true;
                                    ((LinkButton)e.Row.FindControl("btnQc")).Visible = false;
                                }
                                else
                                {
                                    ((LinkButton)e.Row.FindControl("btnRemoveFromFBA")).Visible = true;
                                    ((LinkButton)e.Row.FindControl("btReturn")).Visible = false;
                                    ((LinkButton)e.Row.FindControl("btRelist")).Visible = false;
                                    ((LinkButton)e.Row.FindControl("btnQc")).Visible = false;
                                }
                            }
                        }
                        else
                        {
                            if (fbalocation == null || fbalocation.Length == 0 || fbalocation == "--")
                            {
                                ((LinkButton)e.Row.FindControl("btnRemoveFromFBA")).Visible = false;
                                ((LinkButton)e.Row.FindControl("btReturn")).Visible         = true;
                                ((LinkButton)e.Row.FindControl("btRelist")).Visible         = true;
                                ((LinkButton)e.Row.FindControl("btnQc")).Visible            = true;
                            }
                            else
                            {
                                ((LinkButton)e.Row.FindControl("btnRemoveFromFBA")).Visible = true;
                                ((LinkButton)e.Row.FindControl("btReturn")).Visible         = false;
                                ((LinkButton)e.Row.FindControl("btRelist")).Visible         = true;

                                if (sono.Length == 0 || sono == "4000039858" || sono == "4000033043" || sono == "4000043615" ||
                                    sono == "4000043616" 
                                   )
                                {
                                    ((LinkButton)e.Row.FindControl("btnQc")).Visible = false;
                                }
                            }
                            
                            ((LinkButton)e.Row.FindControl("btRtnDc")).Visible       = false;
                            ((LinkButton)e.Row.FindControl("btnRtnDcprint")).Visible = false;
                            ((LinkButton)e.Row.FindControl("btnRtnDcView")).Visible  = false;
                        }

                        //if(lblDCNO == 0)
                        //{
                        //    ((LinkButton)e.Row.FindControl("btRtnDc")).Visible        = true;
                        //    ((LinkButton)e.Row.FindControl("btnRtnDcprint")).Visible  = false;
                        //}
                        //else
                        //{
                        //    ((LinkButton)e.Row.FindControl("btRtnDc")).Visible       = false;
                        //    ((LinkButton)e.Row.FindControl("btnRtnDcprint")).Visible = true;
                        //}

                        //if (jangadolddaysvalue > 0 && jangadolddaysvalue <= 5)
                        //{
                        //    ((LinkButton)e.Row.FindControl("btRelist")).Visible = true;
                        //}
                        //else
                        //{
                        //    ((LinkButton)e.Row.FindControl("btRelist")).Visible = false;
                        //}

                        //if (jangadolddaysvalue <= 2)
                        //{
                        //    if(fbalocation == null || fbalocation.Length == 0 || fbalocation == "--")
                        //    {
                        //        ((LinkButton)e.Row.FindControl("btReturn")).Visible = true;
                        //    }
                        //    else
                        //    {
                        //        ((LinkButton)e.Row.FindControl("btReturn")).Visible = false;

                        //    }
                        //}
                        //else
                        //{
                        //    ((LinkButton)e.Row.FindControl("btReturn")).Visible = false;
                        //}
                        objBindDDL.FillFBALocation(ddlFBALocation, Convert.ToInt32(((Label)e.Row.FindControl("lblLISTINGTYPEID")).Text));
                        if (((Label)e.Row.FindControl("lblFBALocation")).Text.Contains("FB"))
                        {
                            ddlFBALocation.SelectedValue = ((HiddenField)e.Row.FindControl("hdFBLOCATIONID")).Value;
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

        protected void lnkExport_Click(object sender, EventArgs e)
        {
            try
            {
                string attachment = "attachment; filename=JangadKROListingStock.xls";
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

        protected void btReturn_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                Response.Redirect("ProductReturnDetail.aspx?LISTINGID=" + ((HiddenField)grdrow.FindControl("hdID")).Value);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnExtendDays_Click(object sender, EventArgs e)
        {
            try
            {
                string NGEAPRV = "APPROVED";
                Double newrate = 0;
                int status = 11398;
                var dtNewRate = objMainClass.GetMobileNewRate(hdMake.Value, hdModel.Value,
                   hdRam.Value, hdRom.Value);
                if (dtNewRate.Rows.Count > 0)
                {
                    newrate = Convert.ToDouble(dtNewRate.Rows[0]["MOBILENEWRATE"]);
                }
                Double vendorprice = 0, locamount = 0, MOBILENEWRATE = 0, fkAmt = 0, amzAmt = 0, webAmt = 0, fkper = 0, amzper = 0, webper = 0, PURFKAMT = 0, PURAMZAMT = 0, PURWEBAMT = 0,
                PURFKPER = 0, PURAMZPER = 0, PURWEBPER = 0, PURCHASEPERONVENDORPRICE = 0;
                int islistedfk = 0, islistedamz = 0, islistedweb = 0;
                int ispurlistedfk = 0, ispurlistedamz = 0, ispurlistedweb = 0;

                Double.TryParse(txtVendorprice.Text, out vendorprice);
                Double.TryParse(hdLockAmount.Value, out newrate);
                Double.TryParse(hdLockAmount.Value, out locamount);
                if (vendorprice > newrate)
                {
                    status = 11233;
                    NGEAPRV = "REJECTED";
                }

                if (vendorprice > 0)
                {
                    if (vendorprice <= newrate)
                    {
                        ispurlistedamz = 1;
                        ispurlistedweb = 1;
                    }

                    fkAmt = (((vendorprice)) * (1.234));
                    fkAmt = (Math.Floor(fkAmt * Math.Pow(10, -2)) / Math.Pow(10, -2));

                    if (fkAmt <= vendorprice)
                    {
                        fkAmt = vendorprice + (Math.Round(((vendorprice * 8) / 100), 2));
                        fkAmt = (Math.Floor(fkAmt * Math.Pow(10, -2)) / Math.Pow(10, -2));
                    }

                    //Flipkart Calculate Amount on Vendor Price 
                    PURFKAMT = (((vendorprice)) * (1.234));
                    PURFKAMT = (Math.Floor(PURFKAMT * Math.Pow(10, -2)) / Math.Pow(10, -2));
                    //Flipkart Calculate Amount on Vendor Price 

                    amzAmt = (((vendorprice)) * (1.175));
                    amzAmt = (Math.Floor(amzAmt * Math.Pow(10, -2)) / Math.Pow(10, -2));

                    if (amzAmt <= vendorprice)
                    {
                        amzAmt = vendorprice + (Math.Round(((vendorprice * 8) / 100), 2));
                        amzAmt = (Math.Floor(amzAmt * Math.Pow(10, -2)) / Math.Pow(10, -2));
                    }

                    //Amazon Calculate Amount on Vendor Price 
                    PURAMZAMT = (((vendorprice)) * (1.175));
                    PURAMZAMT = (Math.Floor(PURAMZAMT * Math.Pow(10, -2)) / Math.Pow(10, -2));
                    //Amazon Calculate Amount on Vendor Price 

                    webAmt = (amzAmt) - (350);

                    //Web Calculate Amount on Vendor Price 
                    PURWEBAMT = (PURAMZAMT) - (350);
                    //Web Calculate Amount on Vendor Price


                    if (fkAmt != 0)
                    {
                        fkper = Math.Round((((fkAmt * 100)) / vendorprice), 0);
                    }
                    //FK PlatForm Per Amount on Purchase Price 

                    if (PURFKAMT != 0)
                    {
                        PURFKPER = Math.Round((((PURFKAMT * 100)) / vendorprice), 0);
                    }
                    //FK PlatForm Per Amount on Purchase Price

                    if (amzAmt != 0)
                    {
                        amzper = Math.Round((((amzAmt * 100)) / vendorprice), 0);
                    }

                    //Amazon PlatForm Per Amount on Purchase Price 

                    if (PURAMZAMT != 0)
                    {
                        PURAMZPER = Math.Round((((PURAMZAMT * 100)) / vendorprice), 0);
                    }
                    //Amazon PlatForm Per Amount on Purchase Price 

                    if (webAmt != 0)
                    {
                        webper = Math.Round((((webAmt * 100)) / vendorprice), 0);
                    }

                    //Web PlatForm Per Amount on Purchase Price 
                    if (PURWEBAMT != 0)
                    {
                        PURWEBPER = Math.Round((((PURWEBAMT * 100)) / vendorprice), 0);
                    }

                    //Amazon PlatForm Per Amount on Purchase Price 

                    if (newrate > 0 && vendorprice > 0)
                    {
                        PURCHASEPERONVENDORPRICE = Math.Round(((newrate * 100)) / (vendorprice), 0);
                    }

                    //Calculate PlatForm Listed Detail 
                    islistedfk = fkper >= 90 ? 0 : 1;
                    islistedamz = amzper >= 90 ? 0 : 1;
                    islistedweb = webper >= 90 ? 0 : 1;


                    if (hdMake.Value.ToUpper() == "APPLE")
                    {
                        islistedamz = 0;
                        ispurlistedamz = 0;
                    }
                    else
                    {
                        var dtAmazone = objMainClass.GetPlatFormNotListed((PLATFORM.Amazon.ToString()), hdMake.Value, hdModel.Value, hdGrade.Value, Convert.ToInt32(Session["USERID"].ToString()));
                        if (dtAmazone.Rows.Count > 0)
                        {
                            islistedamz = 0;
                            ispurlistedamz = 0;
                        }
                    }
                    if (fkAmt > 45000)
                    {
                        islistedfk = 0;
                        ispurlistedfk = 0;
                    }

                    int result = objMainClass.JangadRelisting(Convert.ToInt32(hdprimarykey.Value), Convert.ToInt32(ddlmaxday.SelectedItem.Text), status
                                                             ,NGEAPRV, MOBILENEWRATE, islistedfk, islistedamz, islistedweb, fkAmt, amzAmt, webAmt, fkper, amzper, webper, PURFKAMT,
                                                              PURAMZAMT, PURWEBAMT, PURFKPER, PURAMZPER, PURWEBPER, PURCHASEPERONVENDORPRICE, ispurlistedfk, ispurlistedamz, ispurlistedweb, Convert.ToDecimal(locamount),vendorprice, Convert.ToInt32(Session["USERID"]));
                    
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Relisting update Successfully." + "\");", true);
                    BindProductDetail();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Relisting is not Updated." + "\");", true);
                }
                //if (result > 0)
                //{
                //}
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnResetExtend_Click(object sender, EventArgs e)
        {
            try
            {
                ResetRelist();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public void ResetRelist()
        {
            try
            {
                hdprimarykey.Value        = "0";
                hdMake.Value              = string.Empty;
                hdModel.Value             = string.Empty;
                hdRam.Value               = string.Empty;
                hdRom.Value               = string.Empty;
                hdcolor.Value             = string.Empty;
                hdLockAmount.Value        = "0";
                txtVendorprice.Text       = string.Empty;
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
                if (chkSelectAll.Checked == true)
                {
                    for (int i = 0; i < gvProduct.Rows.Count; i++)
                    {
                        GridViewRow row = gvProduct.Rows[i];
                        ((CheckBox)row.FindControl("chkSelection")).Checked = true;
                        ((DropDownList)row.FindControl("ddlFBALocation")).SelectedValue = ddlSelectFBALocation.SelectedValue;
                    }
                }
                else
                {
                    for (int i = 0; i < gvProduct.Rows.Count; i++)
                    {
                        GridViewRow row = gvProduct.Rows[i];
                        ((CheckBox)row.FindControl("chkSelection")).Checked = false;
                        ((DropDownList)row.FindControl("ddlFBALocation")).SelectedValue = "0";
                    }
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

        protected void lnkUpdateSelectedinFBA_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedchecbox = SelectedCheckBox();
                if (selectedchecbox > 0)
                {
                    List<AllocateinFBADetail> objlst = new List<AllocateinFBADetail>();
                    int totalrecord = 0, totalupdaterecord = 0;
                    if (gvProduct.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvProduct.Rows.Count; i++)
                        {
                            totalrecord = totalrecord + 1;
                            GridViewRow row = gvProduct.Rows[i];
                            if (((CheckBox)row.FindControl("chkSelection")).Checked == true)
                            {
                                if (((DropDownList)row.FindControl("ddlFBALocation")).SelectedValue != "0")
                                {
                                    AllocateinFBADetail objAllocateinFBADetail = new AllocateinFBADetail();
                                    totalrecord                                = totalrecord + 1;
                                    objAllocateinFBADetail.ID                  = Convert.ToInt32(((Label)row.FindControl("lblID")).Text);
                                    objAllocateinFBADetail.FBALOCATION         = 0;
                                    String[] actual                            = ((DropDownList)row.FindControl("ddlFBALocation")).SelectedValue.Split(',');
                                    objAllocateinFBADetail.PLANTCD             = actual[0];
                                    objAllocateinFBADetail.LOCCD               = actual[1];
                                    objlst.Add(objAllocateinFBADetail);
                                    totalupdaterecord                          = totalupdaterecord + 1;
                                }
                            }
                        }
                    }
                    string AllocateinFBADetailJSON = "";
                    AllocateinFBADetailJSON = JsonConvert.SerializeObject(objlst);
                    int result = objMainClass.ProductBulkFBAUpdate(AllocateinFBADetailJSON, Convert.ToInt32(Session["USERID"].ToString()));
                    if (result > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Total Record :" + totalrecord + " Total Updated Record : " + totalupdaterecord + " Total Pending Record to Update : " + (totalrecord - totalupdaterecord) + " Update Successfully." + "\");", true);
                        BindProductDetail();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Total Record :" + totalrecord + " Total Updated Record : " + totalupdaterecord + " Total Pending Record to Update : " + (totalrecord - totalupdaterecord) + " Update Successfully." + "\");", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please select at least one checkbox to Update.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message.Replace("\r\n", "") + "\");", true);
            }
        }

        protected void ddlDownloadColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ShowHideColumn();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message.Replace("\r\n", "") + "\");", true);
            }
        }

        public void ShowHideColumn()
        {
            try
            {
                if(Convert.ToInt32(ddlDownloadColumn.SelectedValue) == 1)
                {
                    gvProduct.Columns[0].Visible = true;
                    gvProduct.Columns[1].Visible = true;
                    gvProduct.Columns[4].Visible = true;
                }
                else
                {
                    gvProduct.Columns[1].Visible = false;
                    gvProduct.Columns[4].Visible = false;
                    gvProduct.Columns[0].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message.Replace("\r\n", "") + "\");", true);
            }
        }

        protected void btnRemoveFromFBA_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grdrow  = (GridViewRow)((LinkButton)sender).NamingContainer;
                int result          = objMainClass.RemovefromFBA(Convert.ToInt32(((HiddenField)grdrow.FindControl("hdID")).Value), Convert.ToInt32(Session["USERID"]));
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" FBA Removed Successfully." + "\");", true);
                BindProductDetail();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message.Replace("\r\n", "") + "\");", true);
            }
        }

        protected void lnkRemoveUpdateFromFBA_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedchecbox = SelectedCheckBox();
                if (selectedchecbox > 0)
                {
                    List<AllocateinFBADetail> objlst = new List<AllocateinFBADetail>();
                    int totalrecord = 0, totalupdaterecord = 0;
                    if (gvProduct.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvProduct.Rows.Count; i++)
                        {
                            totalrecord = totalrecord + 1;
                            GridViewRow row = gvProduct.Rows[i];
                            if (((CheckBox)row.FindControl("chkSelection")).Checked == true)
                            {
                                //if (((DropDownList)row.FindControl("ddlFBALocation")).SelectedValue != "0")
                                //{
                                    AllocateinFBADetail objAllocateinFBADetail = new AllocateinFBADetail();
                                    totalrecord = totalrecord + 1;
                                    objAllocateinFBADetail.ID = Convert.ToInt32(((Label)row.FindControl("lblID")).Text);
                                    objAllocateinFBADetail.FBALOCATION = 0;//Convert.ToInt32(((DropDownList)row.FindControl("ddlFBALocation")).SelectedValue);
                                   // String[] actual = ((DropDownList)row.FindControl("ddlFBALocation")).SelectedValue.Split(',');
                                    objAllocateinFBADetail.PLANTCD = string.Empty;
                                    objAllocateinFBADetail.LOCCD = string.Empty;
                                    objlst.Add(objAllocateinFBADetail);
                                    totalupdaterecord = totalupdaterecord + 1;
                               // }
                            }
                        }
                    }
                    string AllocateinFBADetailJSON = "";
                    AllocateinFBADetailJSON = JsonConvert.SerializeObject(objlst);
                    int result = objMainClass.ProductBulkFBAUpdate(AllocateinFBADetailJSON, Convert.ToInt32(Session["USERID"].ToString()), "REMOVEFBAALLOTMENT");
                    if (result > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Total Record :" + totalrecord + " Total Updated Record : " + totalupdaterecord + " Total Pending Record to Update : " + (totalrecord - totalupdaterecord) + " Update Successfully." + "\");", true);
                        BindProductDetail();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Total Record :" + totalrecord + " Total Updated Record : " + totalupdaterecord + " Total Pending Record to Update : " + (totalrecord - totalupdaterecord) + " Update Successfully." + "\");", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please select at least one checkbox to Update.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message.Replace("\r\n", "") + "\");", true);
            }
        }

        protected void btRtnDc_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                string sono        = ((Label)grdrow.FindControl("lblSONO")).Text;
                string JobId       = ((Label)grdrow.FindControl("lblJobId")).Text;
                string itemcode    = ((Label)grdrow.FindControl("lblItemCode")).Text;
                string listingid   = ((Label)grdrow.FindControl("lblID")).Text;

                
                if (sono.Length == 0 || sono == "4000039858" || sono == "4000033043" || sono == "4000043615" ||
                    sono == "4000043616"
                   )
                {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Returnable DC is not possible because we have not received the order against this listing.');", true);
                }
                else if(JobId ==  null  || JobId.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Returnable DC is not possible because you should create the Job Id First.');", true);
                }
                else if(IsPossibletoCreateReturnableDc(sono) == false)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Returnable DC is not possible because sales order schemen does have Dekh ke Lo.');", true);
                }

                else if (CheckAssignmentAvaibility(JobId) == false)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Returnable DC is not possible because Biker Assignment is pending.');", true);
                }
                else
                {
                    var dtResult = objMainClass.GenerateReturnableDc(JobId, sono, itemcode, Convert.ToInt32(listingid), Convert.ToInt32(Session["USERID"].ToString()));
                    if(dtResult.Rows.Count > 0)
                    {
                        int DCNO    = Convert.ToInt32(dtResult.Rows[0]["DCNO"].ToString());
                        BindProductDetail();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Returnable DC Generated Successfully." + "\");", true);
                        string path = "ViewReturnableDCPDF.aspx";
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "?DCNO=" + DCNO + "');", true);
                    }
                }
            }   
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message.Replace("\r\n", "") + "\");", true);
            }
        }


        public bool IsPossibletoCreateReturnableDc(string sono)
        {
            bool IsrtnDc = false;
            try
            {
                var dtresult = objMainClass.CheckReturnableSoAvaibility(sono);
                if(dtresult.Rows.Count > 0)
                {
                    IsrtnDc = true;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message.Replace("\r\n", "") + "\");", true);
            }
            return IsrtnDc;
        }

        public bool CheckAssignmentAvaibility(string JOBID)
        {
            bool Isavaible = false;
            try
            {
                var dtresult = objMainClass.CheckAssignmentAvaibility(JOBID);
                if (dtresult.Rows.Count > 0)
                {
                    Isavaible = true;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message.Replace("\r\n", "") + "\");", true);
            }
            return Isavaible;
        }

        protected void btnRtnDcprint_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                string DCNO = ((Label)grdrow.FindControl("lblDCNO")).Text;
                string path = "ViewReturnableDCPDF.aspx";
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "?DCNO=" + DCNO + "');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnRtnDcView_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}