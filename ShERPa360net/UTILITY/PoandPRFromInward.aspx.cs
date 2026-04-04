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
    public partial class PoandPRFromInward : System.Web.UI.Page
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
                    txtFromDate.Text = (objMainClass.indianTime.Date.AddDays(-30)).ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
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
                    dt = objMainClass.GetPrPoDataDetail(txtFromDate.Text, txtToDate.Text, (ddlVendor.SelectedValue == "0" ? "" : ddlVendor.SelectedItem.Text), status, 0,0,"", txtIMEINo.Text);
                    gvProduct.DataSource = dt;
                    gvProduct.DataBind();
                    //gvProduct.HeaderRow.TableSection = TableRowSection.TableHeader;
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
                    txtFromDate.Text = (objMainClass.indianTime.Date.AddDays(-30)).ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
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
                GridViewRow grdrow  = (GridViewRow)((LinkButton)sender).NamingContainer;
                string ID           = ((HiddenField)grdrow.FindControl("hdID")).Value;
                string inwardresult = ((Label)grdrow.FindControl("lblInwardResult")).Text;
                string hdinwardechange = ((HiddenField)grdrow.FindControl("hdinwardechange")).Value;
                if (inwardresult == "PASS")
                {
                    PRPOCREATION(Convert.ToInt32(ID),Convert.ToInt32(hdinwardechange));
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Auto Po and PR not possible due to Inward Result is Fail.');", true);
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
    }
}