using Newtonsoft.Json;
using org.w3c.dom.css;
using ShERPa360net.Class;
using ShERPa360net.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.UTILITY
{
    public partial class ProductInwardDetail : System.Web.UI.Page
    {
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        bool Checkerright = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtFromDate.Text = (objMainClass.indianTime.Date.AddDays(-30)).ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
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
                        //BindProductDetail();
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

        protected void lnkExport_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = "Product Report" + txtFromDate.Text + "-" + txtToDate.Text;
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
                    status = Convert.ToInt32(PRODUCTSTATUS.ORDERRECEIVED);
                    gvProduct.DataSource = null;
                    gvProduct.DataBind();
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetProductEntryDetail(txtFromDate.Text, txtToDate.Text, (ddlVendor.SelectedValue == "0" ? "" : ddlVendor.SelectedItem.Text), status,0, (txtListingID.Text.Length > 0 ? Convert.ToInt32(txtListingID.Text) : 0), "", txtIMEINo.Text);
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
                ResetQcDetail();
                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                hdprimarykey.Value = ((HiddenField)grdrow.FindControl("hdID")).Value;
                //Initiate the Control 
                hdMake.Value = ((Label)grdrow.FindControl("lblMake")).Text;
                hdModel.Value = ((Label)grdrow.FindControl("lblModel")).Text;
                hdRam.Value = ((Label)grdrow.FindControl("lblRam")).Text;
                hdRom.Value = ((Label)grdrow.FindControl("lblRom")).Text;
                hdcolor.Value = ((Label)grdrow.FindControl("lblColor")).Text;
                hdGrade.Value = ((Label)grdrow.FindControl("lblVendorGrade")).Text;
                hdVendorID.Value = ((Label)grdrow.FindControl("lblVendorID")).Text;
                hdlistingtype.Value = ((HiddenField)grdrow.FindControl("hdgrdLISTINGTYPE")).Value;

                txtOrderNo.Text  = ((Label)grdrow.FindControl("lblOrderNo")).Text;
                txtSoNo.Text     = ((Label)grdrow.FindControl("lblSoNo")).Text;
                Label lblImageData = grdrow.FindControl("lblImageData") as Label;
                Label lblInvoiceImageData = grdrow.FindControl("lblInvoiceImageData") as Label;

                //Manan 02-02-2023
                if (!string.IsNullOrEmpty(hdMake.Value) && !string.IsNullOrEmpty(hdModel.Value) && !string.IsNullOrEmpty(hdRam.Value) && !string.IsNullOrEmpty(hdRom.Value))
                {
                    ddlColor.Items.Clear();
                    objBindDDL.FillMobexSellerColorByValue(ddlColor, hdMake.Value, hdModel.Value, hdRam.Value, hdRom.Value);
                    ddlColor.SelectedValue = hdcolor.Value;
                }

                if (lblImageData.Text != null && lblImageData.Text != string.Empty)
                {
                    var dtImeiImage = objMainClass.GetMobileIMEIImageDetail(Convert.ToInt32(((HiddenField)grdrow.FindControl("hdID")).Value));
                    string imageUrl = "data:image/jpg;base64," + Convert.ToBase64String((byte[])dtImeiImage.Rows[0]["IMEIIMAGE"]);
                    imgImage.ImageUrl = imageUrl;
                    //string imageUrl = "data:image/jpg;base64," + Convert.ToBase64String((byte[])(lblImageData.Text));
                }


                if (lblInvoiceImageData.Text != null && lblInvoiceImageData.Text != string.Empty)
                {
                    var dtinvoiceImage = objMainClass.GetMobileINVOICEImageDetail(Convert.ToInt32(((HiddenField)grdrow.FindControl("hdID")).Value));
                    string imageUrl    = "data:image/jpg;base64," + Convert.ToBase64String((byte[])dtinvoiceImage.Rows[0]["INVOICEIMAGE"]);
                    invoiceImage.ImageUrl = imageUrl;
                    //string imageUrl = "data:image/jpg;base64," + Convert.ToBase64String((byte[])(lblImageData.Text));
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                ddlInwardResult.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }


        [WebMethod]
        public static bool CheckSOnumber(string SONo)
        {
            try
            {
                MainClass objMain = new MainClass();
                return objMain.CheckSOnumber(SONo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnSaveQc_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = Convert.ToInt32(hdprimarykey.Value);
                int Status = 0;
                string rejectreason = "";
                string responseStr = "";
                if (ddlInwardResult.SelectedValue == "PASS")
                {
                    Status = Convert.ToInt32(PRODUCTSTATUS.PURCHASE);
                }
                else
                {
                    Status = Convert.ToInt32(PRODUCTSTATUS.REJECTED);
                    rejectreason = "QC Fail";
                }

                string itemdesc = hdMake.Value + " " + hdModel.Value + " "
                + hdRam.Value + " "
                + hdRom.Value + " "
                //+ hdcolor.Value + " MOBILE DEVICE (USED) (GRADE "
                + ddlColor.SelectedItem.Text + " MOBILE DEVICE (USED) (GRADE "
                + (hdGrade.Value == "C" ? hdGrade.Value : "A") + ")";  //ddlInwardGrade.SelectedValue "A"

                string itemcode = GetItemCode(itemdesc, hdMake.Value, hdModel.Value);

                objMainClass.UpdateInwardDetail(ID, ddlInwardResult.SelectedValue, txtInwardReason.Text,
                ddlInwardGrade.SelectedValue, 0, Session["USERID"].ToString(),
                objMainClass.indianTime.Date.ToString("dd-MM-yyyy"), Status,
                rejectreason, txtOrderNo.Text, txtRefNo.Text, txtSoNo.Text, Convert.ToInt32(ddlInvoice.SelectedValue),
                Convert.ToInt32(ddlBox.SelectedValue), Convert.ToInt32(ddlCharger.SelectedValue), Convert.ToInt32(ddlOrignal.SelectedValue)
                , txtChargerWalt.Text, itemcode, ddlColor.SelectedItem.Text);
                //if (ddlInwardResult.SelectedValue == "PASS")
                //{
                //    responseStr = PRPOCREATION(Convert.ToInt32(hdprimarykey.Value));
                //}

                //var mailmessagefor = "Hi There,<br><br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ID is : " + ID.ToString() + " Product : Make-" + hdMake.Value + " Model-" + hdModel.Value + " Ram-" + hdRam.Value + " Rom-" + hdRom.Value + " Color-" + hdcolor.Value + " Grade-" + hdGrade.Value + " Order No-" + txtOrderNo.Text + " Inward Successfully " + " <br>Please do needfull.<br><br>Regard,<br>Mobex Seller System<br><br><br>";
                //EmailSend.EmailSent(mailmessagefor, "Product Order No :" + txtOrderNo.Text + " Purchase Order Received successfully", "care@mobex.in", "mobex@123", "dispatch@qarmatek.com");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Product Inward Successfully. " + responseStr + "." + "\");", true);

                //SEND PUSH NOTIFICATION
                //string productdetail = hdMake.Value + " " + hdModel.Value + " " + hdRam.Value + " " + hdRom.Value + " " + hdcolor.Value + " " + hdGrade.Value;
                //var pushnotificationmsg = PushNotificationContentDetail.GETINWARDPUSHMESSAGE(ID.ToString(), productdetail);
                //SendPushNotification.SendPushNotificaion(PushNotificationContentDetail.GETINWARDPUSHSUBJECT(), pushnotificationmsg, Convert.ToInt32(hdVendorID.Value));
                //SEND PUSH NOTIFICATION
                BindProductDetail();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public string GetItemCode(string itemdesc, string make, string model)
        {
            string itemcode = string.Empty;
            try
            {
                DataTable dtItemDetail = new DataTable();
                int maxItemcode = 0;
                DataRow[] rowItemd = null;
                dtItemDetail = objMainClass.GetItemMaxCode(9, 168, "GETALL");
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
                    dtNewItemCode = objMainClass.GetItemMaxCode(9, 168, "GETMAXITEMCODE");
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

        public string PRPOCREATION(int LISTINGID)
        {
            string responsestr = string.Empty;
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
                            string PRNO = objMainClass.SavePPRFromListing("MPR", DateTime.Now.ToString(), "MOBEX AUTO PR SYSTEM" + Convert.ToString(dtDetails.Rows[0]["DEALERNAME"]) + " " + Convert.ToString(dtDetails.Rows[0]["IMEINO"]), Convert.ToString((int)DEPT.SmartPhone), grvData, objMainClass.strBackGroundUser, Convert.ToString(LISTINGID), Convert.ToString(dtDetails.Rows[0]["VENDCODE"]));
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

        public void ResetQcDetail()
        {
            try
            {
                imgImage.ImageUrl = null;
                ddlInwardResult.SelectedIndex = -1;
                txtInwardReason.Text = string.Empty;
                ddlInwardGrade.SelectedIndex = -1;
                hdprimarykey.Value = "0";
                txtOrderNo.Text = string.Empty;
                txtRefNo.Text = string.Empty;

                // Initiate the control
                hdMake.Value = "";
                hdModel.Value = "";
                hdRam.Value = "";
                hdRom.Value = "";
                hdcolor.Value = "";
                hdGrade.Value = "";
                hdVendorID.Value = "";
                ddlInvoice.SelectedIndex = -1;
                ddlBox.SelectedIndex = -1;
                ddlCharger.SelectedIndex = -1;
                ddlOrignal.SelectedIndex = -1;
                txtChargerWalt.Text = string.Empty;
                hdlistingtype.Value = "0";
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
                        DataRowView dr = (DataRowView)e.Row.DataItem;

                        Label lblImageData = e.Row.FindControl("lblImageData") as Label;
                        Label lblInvoiceImageData = e.Row.FindControl("lblInvoiceImageData") as Label;
                        //LinkButton lnkDownload = e.Row.FindControl("lnkDownload") as LinkButton;

                        if (lblImageData.Text != null && lblImageData.Text != string.Empty)
                        {
                            //if (lblImageExtension.Text == ".jpg" || lblImageExtension.Text == ".jpeg")
                            //{
                            string imageUrl = "data:image/jpg;base64," + Convert.ToBase64String((byte[])dr["IMEIIMAGE"]);
                            
                                (e.Row.FindControl("imgImage") as Image).ImageUrl = imageUrl;
                                (e.Row.FindControl("imgImage") as Image).Visible = true;
                            


                            //lnkDownload.Visible = true;
                            //}
                            //else
                            //{
                            //    (e.Row.FindControl("imgImage") as Image).Visible = false;
                            //    lnkDownload.Visible = true;
                            //}
                        }
                        else
                        {
                            (e.Row.FindControl("imgImage") as Image).Visible = false;
                            //lnkDownload.Visible = false;
                        }
                        if (lblInvoiceImageData.Text != null && lblInvoiceImageData.Text != string.Empty)
                        {
                            string invoiceUrl = "data:image/jpg;base64," + Convert.ToBase64String((byte[])dr["INVOICEIMAGE"]);
                            (e.Row.FindControl("invoiceImage") as Image).ImageUrl = invoiceUrl;
                            (e.Row.FindControl("invoiceImage") as Image).Visible = true;

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
    }
}