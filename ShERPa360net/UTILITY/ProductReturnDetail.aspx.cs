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
    public partial class ProductReturnDetail : System.Web.UI.Page
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
                    if (Request.QueryString.Count > 0)
                    {
                        if (Convert.ToString(Request.QueryString["LISTINGID"]) != null && Convert.ToString(Request.QueryString["LISTINGID"]) != string.Empty && Convert.ToString(Request.QueryString["LISTINGID"]) != "")
                        {
                            Session["LISTINGID"] = Convert.ToString(Request.QueryString["LISTINGID"]);
                        }
                        Response.Redirect(Request.Url.AbsolutePath, false);
                    }

                    if(Session["LISTINGID"]!=null)
                    {
                        txtListingID.Text = Session["LISTINGID"].ToString();
                    }

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
                    status               = Convert.ToInt32(ddlStatus.SelectedValue);
                    gvProduct.DataSource = null;
                    gvProduct.DataBind();
                    DataTable dt         = new DataTable();
                    dt                   = objMainClass.GetInwardedProductEntryDetail(txtFromDate.Text, txtToDate.Text, (ddlVendor.SelectedValue == "0" ? "" : ddlVendor.SelectedItem.Text), status,Convert.ToInt32(Session["USERID"].ToString()), txtIMEINumber.Text, (txtListingID.Text.Length > 0 ? Convert.ToInt32(txtListingID.Text) : 0));
                    gvProduct.DataSource = dt;
                    gvProduct.DataBind();
                    if (gvProduct.Rows.Count > 0)
                    {
                        gvProduct.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                    if (FormRights.bAdd == false)
                    {
                        gvProduct.Columns[1].Visible = false;
                        gvProduct.Columns[2].Visible = false;
                    }
                    else
                    {
                        gvProduct.Columns[1].Visible = true;
                        gvProduct.Columns[2].Visible = true;
                    }

                    if (status == 11398)
                    {
                        gvProduct.Columns[0].Visible = false;
                        gvProduct.Columns[1].Visible = true;
                        gvProduct.Columns[2].Visible = false;
                    }
                    else if (status == 11999)
                    {
                        gvProduct.Columns[0].Visible = true;
                        gvProduct.Columns[1].Visible = false;
                        gvProduct.Columns[2].Visible = true;
                    }
                    else if (status == 12000)
                    {
                        gvProduct.Columns[0].Visible = true;
                        gvProduct.Columns[1].Visible = false;
                        gvProduct.Columns[2].Visible = false;
                    }
                    else
                    {
                        gvProduct.Columns[0].Visible = true;
                        gvProduct.Columns[1].Visible = false;
                        gvProduct.Columns[2].Visible = false;
                    }


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

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                ResetReturnDetail();
                GridViewRow grdrow      = (GridViewRow)((LinkButton)sender).NamingContainer;
                hdprimarykey.Value      = ((HiddenField)grdrow.FindControl("hdID")).Value;
                txtMake.Text            = ((Label)grdrow.FindControl("lblMake")).Text;
                txtModel.Text           = ((Label)grdrow.FindControl("lblModel")).Text;
                txtRom.Text             = ((Label)grdrow.FindControl("lblRom")).Text;
                txtRam.Text             = ((Label)grdrow.FindControl("lblRam")).Text;
                txtColor.Text           = ((Label)grdrow.FindControl("lblColor")).Text;
                txtVendorDetail.Text    = ((Label)grdrow.FindControl("lblVendorName")).Text;
                txtInwardBy.Text        = ((Label)grdrow.FindControl("lblInwardBy")).Text;
                txtInwardDate.Text      = ((Label)grdrow.FindControl("lblInwardDate")).Text;
                txtIMEINo.Text          = ((Label)grdrow.FindControl("lblIMEINo")).Text;
                hdBikerContactNo.Value  = ((Label)grdrow.FindControl("lblBikerContactNo")).Text;
                hdAsmContactNo.Value    = ((Label)grdrow.FindControl("lblAsmContactNo")).Text;
                hdJpBhaiContactNo.Value = ((Label)grdrow.FindControl("lblJpBhaiContactNo")).Text;
                hdVendorID.Value        = ((HiddenField)grdrow.FindControl("hdVendorId")).Value;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                txtReturnReason.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public void ResetReturnDetail()
        {
            try
            {
                hdprimarykey.Value = "0";
                txtIMEINo.Text = string.Empty;
                txtReturnReason.Text = string.Empty;
                hdBikerContactNo.Value  = string.Empty;
                hdAsmContactNo.Value    = string.Empty;
                hdJpBhaiContactNo.Value = string.Empty;
                hdVendorID.Value = string.Empty;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnSaveReturn_Click(object sender, EventArgs e)
        {
            try
            {
                int ID              = Convert.ToInt32(hdprimarykey.Value);
                int Status          = Convert.ToInt32(PRODUCTSTATUS.RETURNREQUESTGENERATED);
                //var dtIMEIJobsheet = objMainClass.CheckIMEINOJobExist(txtIMEINo.Text);
                //if (dtIMEIJobsheet.Rows.Count > 0)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" Unable to Return Becuase JobSheet is already exist." + "\");", true);
                //}
                //else
                //{
                    string textMessage = "Listing ID : " + hdprimarykey.Value + " with " + txtMake.Text + " " + txtModel.Text + " " + txtRam.Text + " " + txtRom.Text + " " + txtColor.Text + " has been processed for RETURN due to " + txtReturnReason.Text +". Please collect and return to " + txtVendorDetail.Text + "." + System.Environment.NewLine + "Mobex";
                    //string textMessage = "Listing ID: " + hdprimarykey.Value + " Product : " + txtMake.Text + " " + txtModel.Text + " " + txtRam.Text + " " + txtRom.Text + " " + txtColor.Text +  " " + " Return Request has been generated due to " + txtReturnReason.Text + " Reason." + " So Please collect mobile from Store and return to " +  txtVendorDetail.Text + " Vendor.";
                    objMainClass.UpdateReturnStatus(txtReturnReason.Text, Status, Convert.ToInt32(Session["USERID"]), ID);
                    WAClass objSendSMS = new WAClass();

                    //Send To SMS Detail
                    if (hdBikerContactNo.Value.Length > 0)
                    {
                        objSendSMS.SendTextMessage(textMessage, "91" + hdBikerContactNo.Value, Session["USERID"].ToString());
                    }
                    if (hdAsmContactNo.Value.Length > 0)
                    {
                        objSendSMS.SendTextMessage(textMessage, "91" + hdAsmContactNo.Value, Session["USERID"].ToString());
                    }
                    if (hdJpBhaiContactNo.Value.Length > 0)
                    {
                        objSendSMS.SendTextMessage(textMessage, "91" + hdJpBhaiContactNo.Value, Session["USERID"].ToString());
                    }
                    objSendSMS.SendTextMessage(textMessage, "91" + "9998995184", Session["USERID"].ToString());
                    //Send To SMS Detail

                    //SEND PUSH NOTIFICATION
                    //string VENDORID             = hdVendorID.Value;
                    //string productdetail        = txtMake.Text + " " + txtModel.Text + " " + txtRam.Text + " " + txtRom.Text + " " + txtColor.Text;
                    //var pushnotificationmsg     = PushNotificationContentDetail.GETRETURNREQUESTPUSHMESSAGE(ID.ToString(), productdetail,txtReturnReason.Text);
                    //SendPushNotification.SendPushNotificaion(PushNotificationContentDetail.GETRETURNREQUESTPUSHSUBJECT(), pushnotificationmsg, Convert.ToInt32(VENDORID));
                    //SEND PUSH NOTIFICATION
                    BindProductDetail();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Return Request update Successfully." + "\");", true);
                    string path = "ViewMobexSellerReturnPDF.aspx";
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "?MobexSellerReturnID=" + ID + "');", true);
                //}
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnResetReturn_Click(object sender, EventArgs e)
        {
            try
            {
                ResetReturnDetail();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                string ID          = ((HiddenField)grdrow.FindControl("hdID")).Value;
                string path        = "ViewMobexSellerReturnPDF.aspx";
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "?MobexSellerReturnID=" + ID + "');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnHandoverToBDO_Click(object sender, EventArgs e)
        {
            try
            {
                ResetHandoverBDODetail();
                int _min = 1000;
                int _max = 9999;
                Random _rdm = new Random();
                int iRandomNumber = _rdm.Next(_min, _max);
                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                hdHandoverBDOprimarykey.Value = ((HiddenField)grdrow.FindControl("hdID")).Value;
                txtHandoverBDOMake.Text = ((Label)grdrow.FindControl("lblMake")).Text;
                txtHandoverBDOModel.Text = ((Label)grdrow.FindControl("lblModel")).Text;
                txtHandoverBDORom.Text = ((Label)grdrow.FindControl("lblRom")).Text;
                txtHandoverBDORam.Text = ((Label)grdrow.FindControl("lblRam")).Text;
                txtHandoverBDOColor.Text = ((Label)grdrow.FindControl("lblColor")).Text;
                txtHandoverBDOVendor.Text = ((Label)grdrow.FindControl("lblVendorName")).Text;
                txtHandoverBDOInwardBy.Text = ((Label)grdrow.FindControl("lblInwardBy")).Text;
                txtHandoverBDOInwardDate.Text = ((Label)grdrow.FindControl("lblInwardDate")).Text;
                txtHandoverBDOIMEINo.Text = ((Label)grdrow.FindControl("lblIMEINo")).Text;
                hdHandoverBDOBikerContactNo.Value = ((Label)grdrow.FindControl("lblBikerContactNo")).Text;
                hdHandoverBDOAsmContactNo.Value = ((Label)grdrow.FindControl("lblAsmContactNo")).Text;
                hdHandoverBDOJpBhaiContactNo.Value = ((Label)grdrow.FindControl("lblJpBhaiContactNo")).Text;

                
                //string textMessage = iRandomNumber.ToString() + " is your Mobex RETURN OTP for Listing ID " + hdHandoverBDOprimarykey.Value  + ". Please share it with STORE to process collection." + System.Environment.NewLine + "Mobex";
                string textMessage = "Your One Time Password is " + iRandomNumber.ToString() + ". Please use this OTP to login. Do not share this OTP to anyone for security reasons.\nMobex";
                WAClass objSendSMS = new WAClass();
                //Send To BDO SMS Detail
                if (hdHandoverBDOBikerContactNo.Value.Length > 0)
                {
                    objSendSMS.SendTextMessage(textMessage, "91" + hdHandoverBDOBikerContactNo.Value, Session["USERID"].ToString());
                    //SMSSend.SendSMS(hdHandoverBDOBikerContactNo.Value, textMessage);
                }
                Session["BDOOTP"] = iRandomNumber.ToString();
                if (((Label)grdrow.FindControl("lblDealerCity")).Text.ToUpper() == "BANGLORE" || ((Label)grdrow.FindControl("lblDealerCity")).Text.ToUpper() == "BANGALORE")
                {
                    objSendSMS.SendTextMessage(textMessage, "91" + "9035866995", Session["USERID"].ToString());
                    //SMSSend.SendSMS("9035866995", textMessage);
                }
                if (((Label)grdrow.FindControl("lblDealerCity")).Text.ToUpper() == "NEW DELHI" || ((Label)grdrow.FindControl("lblDealerCity")).Text.ToUpper() == "NOIDA")
                {
                    objSendSMS.SendTextMessage(textMessage, "91" + "9711290173", Session["USERID"].ToString());
                    objSendSMS.SendTextMessage(textMessage, "91" + "8285875987", Session["USERID"].ToString());
                    //SMSSend.SendSMS("9711290173", textMessage);
                }
                else if (((Label)grdrow.FindControl("lblDealerCity")).Text.ToUpper() == "AHMEDABAD" ||
                    ((Label)grdrow.FindControl("lblDealerCity")).Text.ToUpper() == "INDORE" ||
                    ((Label)grdrow.FindControl("lblDealerCity")).Text.ToUpper() == "SURAT")
                {
                    objSendSMS.SendTextMessage(textMessage, "91" + "9998995184", Session["USERID"].ToString());
                    if (((Label)grdrow.FindControl("lblDealerCity")).Text.ToUpper() == "SURAT")
                    {
                        objSendSMS.SendTextMessage(textMessage, "91" + "8555554680", Session["USERID"].ToString());
                    }
                    //SMSSend.SendSMS("9998995184", textMessage);
                }
                else
                {
                    objSendSMS.SendTextMessage(textMessage, "91" + "9998995184", Session["USERID"].ToString());
                    if (((Label)grdrow.FindControl("lblDealerCity")).Text.ToUpper() == "SURAT")
                    {
                        objSendSMS.SendTextMessage(textMessage, "91" + "8555554680", Session["USERID"].ToString());
                    }

                    if (((Label)grdrow.FindControl("lblStateDetail")).Text.ToUpper() == "DELHI" ||
                        ((Label)grdrow.FindControl("lblStateDetail")).Text.ToUpper() == "UTTAR PRADESH")
                    {
                        objSendSMS.SendTextMessage(textMessage, "91" + "9711290173", Session["USERID"].ToString());
                        objSendSMS.SendTextMessage(textMessage, "91" + "8285875987", Session["USERID"].ToString());
                    }
                    //SMSSend.SendSMS("9998995184", textMessage);
                }

                //SMSSend.SendSMS("9067277577", textMessage);
                //objSendSMS.SendTextMessage(textMessage, "91" + "9374332580", Session["USERID"].ToString());
                //objSendSMS.SendTextMessage(textMessage, "91" + "9067277577", Session["USERID"].ToString());
                //Send To BDO SMS Detail
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modalHandovertoBDO').modal();", true);
                txtHandoverBDOOTP.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnSaveHandover_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtHandoverBDOOTP.Text == Session["BDOOTP"].ToString())
                {
                    int ID          = Convert.ToInt32(hdHandoverBDOprimarykey.Value);
                    int Status      = Convert.ToInt32(PRODUCTSTATUS.RETURNHANDOVERTOBDO);
                    objMainClass.UPDATERETURNGENERATEDTOBDOHANDOVERDETAIL(Status, Convert.ToInt32(Session["USERID"]), ID);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Return Request Successfully Handover to BDO." + "\");", true);
                    BindProductDetail();
                }
                else
                {
                    lblHandoverInvalidOTP.Visible  = true;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modalHandovertoBDO').modal();", true);
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" Invalid OTP Please enter valid OTP." + "\");", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnResetHandovertoBDO_Click(object sender, EventArgs e)
        {
            try
            {
                ResetHandoverBDODetail();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public void ResetHandoverBDODetail()
        {
            try
            {
                hdHandoverBDOprimarykey.Value       = "0";
                txtHandoverBDOMake.Text             = string.Empty;
                txtHandoverBDOModel.Text            = string.Empty;
                txtHandoverBDORam.Text              = string.Empty;
                txtHandoverBDORom.Text              = string.Empty;
                txtHandoverBDOColor.Text            = string.Empty;
                txtHandoverBDOVendor.Text           = string.Empty;
                txtHandoverBDOOTP.Text              = string.Empty;
                txtHandoverBDOInwardBy.Text         = string.Empty;
                txtHandoverBDOInwardDate.Text       = string.Empty;
                txtHandoverBDOIMEINo.Text           = string.Empty;
                txtHandoverBDOOTP.Text              = string.Empty;
                hdHandoverBDOBikerContactNo.Value   = string.Empty;
                hdHandoverBDOAsmContactNo.Value     = string.Empty;
                hdHandoverBDOJpBhaiContactNo.Value  = string.Empty;
                lblHandoverInvalidOTP.Visible       = false;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}