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
    public partial class ProductReturnBDOToVendorDetail : System.Web.UI.Page
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
                    status               = Convert.ToInt32(PRODUCTSTATUS.RETURNHANDOVERTOBDO);
                    gvProduct.DataSource = null;
                    gvProduct.DataBind();
                    DataTable dt         = new DataTable();
                    dt                   = objMainClass.GetInwardedProductEntryDetail(txtFromDate.Text, txtToDate.Text, (ddlVendor.SelectedValue == "0" ? "" : ddlVendor.SelectedItem.Text), status, 0);
                    gvProduct.DataSource = dt;
                    gvProduct.DataBind();
                    if (gvProduct.Rows.Count > 0)
                    {
                        gvProduct.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    lgrecordcount.InnerText = "Records : " + dt.Rows.Count.ToString();

                    //if (FormRights.bAdd == false)
                    //{
                    //    gvProduct.Columns[1].Visible = false;
                    //}
                    //else
                    //{
                    //    gvProduct.Columns[1].Visible = true;
                    //}
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
                WAClass objWAClass = new WAClass();
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
                hdDealerContactNo.Value = ((Label)grdrow.FindControl("lblDealerContactNo")).Text;
                hdDealerContactNo2.Value = ((Label)grdrow.FindControl("lblDealerContactNo2")).Text;
                hdDealerContactNo3.Value = ((Label)grdrow.FindControl("lblDealerContactNo3")).Text;
                hdVendorID.Value = ((Label)grdrow.FindControl("lblVendorID")).Text;
                
                //string textMessage = iRandomNumber.ToString() +" is your handset return OTP for " +txtHandoverBDOMake.Text + " " + txtHandoverBDOModel.Text + " " + txtHandoverBDORam.Text + " " + txtHandoverBDORom.Text + " " + txtHandoverBDOColor.Text +" with Listing ID " + hdHandoverBDOprimarykey.Value + ". Please do not share with anyone except assigned MOBEX BDO." + System.Environment.NewLine + "Mobex";
                string textMessage = "Your One Time Password is " + iRandomNumber.ToString() + ". Please use this OTP to login. Do not share this OTP to anyone for security reasons.\nMobex";

                //string textMessage = "Listing ID: " + hdHandoverBDOprimarykey.Value + " Product : " + txtHandoverBDOMake.Text + " " + txtHandoverBDOModel.Text + " " + txtHandoverBDORam.Text + " " + txtHandoverBDORom.Text + " " + txtHandoverBDOColor.Text + " " + " Handover to BDO OTP is:" + iRandomNumber.ToString() + " so please give the OTP detail to store and collect the Mobile.";

                //WAClass objSendSMS = new WAClass();
                //Send To BDO SMS Detail
                if (hdDealerContactNo.Value.Length > 0)
                {
                    Session["VENDOROTP"] = iRandomNumber.ToString();
                    objWAClass.SendTextMessage(textMessage, "91" + hdDealerContactNo.Value, Session["USERID"].ToString());
                    //SMSSend.SendSMS(hdDealerContactNo.Value, textMessage);
                    //objSendSMS.SendTextMessage(textMessage, "91" + hdDealerContactNo.Value, Session["USERID"].ToString());
                }

                if (hdDealerContactNo2.Value.Length > 0)
                {
                    Session["VENDOROTP"] = iRandomNumber.ToString();
                    objWAClass.SendTextMessage(textMessage, "91" + hdDealerContactNo2.Value, Session["USERID"].ToString());

                    //SMSSend.SendSMS(hdDealerContactNo2.Value, textMessage);
                }

                if (hdDealerContactNo3.Value.Length > 0)
                {
                    Session["VENDOROTP"] = iRandomNumber.ToString();
                    objWAClass.SendTextMessage(textMessage, "91" + hdDealerContactNo3.Value, Session["USERID"].ToString());

                    //WAClass.(hdDealerContactNo3.Value, textMessage);
                }


                //objSendSMS.SendTextMessage(textMessage, "91" + "9374332580", Session["USERID"].ToString());
                //Send To BDO SMS Detail
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modalHandovertoVendor').modal();", true);
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
                if(txtHandoverBDOOTP.Text == Session["VENDOROTP"].ToString())
                {
                    int ID          = Convert.ToInt32(hdHandoverBDOprimarykey.Value);
                    int Status      = Convert.ToInt32(PRODUCTSTATUS.RETURNED);
                    objMainClass.UPDATERETURNVENDORETAIL(Status, Convert.ToInt32(Session["USERID"]), ID);
                    
                    //SEND PUSH NOTIFICATION
                    //string VENDORID         = hdVendorID.Value;
                    //string productdetail    = txtHandoverBDOMake.Text + " " + txtHandoverBDOModel.Text + " " + txtHandoverBDORam.Text + " " + txtHandoverBDORom.Text + " " + txtHandoverBDOColor.Text;
                    //var pushnotificationmsg = PushNotificationContentDetail.GETRETURNRECEIVEDPUSHMESSAGE(ID.ToString(), productdetail);
                    //SendPushNotification.SendPushNotificaion(PushNotificationContentDetail.GETRETURNRECEIVEDPUSHSUBJECT(), pushnotificationmsg, Convert.ToInt32(VENDORID));
                    //SEND PUSH NOTIFICATION

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Return Request Successfully Handover to Vendor." + "\");", true);
                    BindProductDetail();
                }
                else
                {
                    lblHandoverInvalidOTP.Visible = true;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modalHandovertoVendor').modal();", true);
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
                hdDealerContactNo.Value             = string.Empty;
                hdDealerContactNo2.Value = string.Empty;
                hdDealerContactNo3.Value = string.Empty;
                lblHandoverInvalidOTP.Visible       = false;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}