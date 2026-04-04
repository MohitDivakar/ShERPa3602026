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
    public partial class ProductUnListedEntry : System.Web.UI.Page
    {
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        
        #region PAGEEVENT
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
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
                        BindPageDropDown();
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
                    if(Convert.ToInt32(Session["USERID"]) == 109 || Convert.ToInt32(Session["USERID"]) == 78)
                    {
                        objBindDDL.FillMobexSellerAllVendorForUnliste(ddlVendor);
                        ddlVendor.SelectedValue = "0";
                    }
                    else
                    {
                        objBindDDL.FillMobexSellerVendorByBikerAreaWise(ddlVendor, Convert.ToInt32(Session["USERID"].ToString()),"not regular");
                        ddlVendor.SelectedIndex = -1;
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
                    ddlVendor.SelectedValue = "0";
                    ddlVendor.Focus();
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
        #endregion

        //protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        BindProductDetail();
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
        //    }
        //}

        public void BindProductDetail()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    //if(ddlVendor.SelectedValue != "0")
                    //{
                        int id;
                        int.TryParse(txtID.Text, out id);
                        int status = 0;
                        status = Convert.ToInt32(PRODUCTSTATUS.LISTEDRESERVED);
                        gvProduct.DataSource = null;
                        gvProduct.DataBind();
                        DataTable dt = new DataTable();
                        dt = objMainClass.GetAllStatusProductDetail(((ddlVendor.SelectedValue == "0" || ddlVendor.SelectedValue == "")  ? ""  : ddlVendor.SelectedItem.Text), status,id,Convert.ToInt32(Session["USERID"]),Convert.ToInt32(ddlAging.SelectedValue));
                        gvProduct.DataSource = dt;
                        gvProduct.DataBind();
                        if(dt.Rows.Count > 0)
                        {
                            gvProduct.HeaderRow.TableSection = TableRowSection.TableHeader;
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myjquery", "InitiateUnlistedDataTable();", true);
                        }
                        lgrecordcount.InnerText = "Records : " + dt.Rows.Count.ToString();

                        if (FormRights.bAdd == false)
                        {
                            gvProduct.Columns[0].Visible  = false;
                        }
                        else
                        {
                            gvProduct.Columns[0].Visible  = true;
                        }

                    //}
                    //else
                    //{
                    //    gvProduct.DataSource = null;
                    //    gvProduct.DataBind();
                    //    lgrecordcount.InnerText = "Records : " + "0";
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

        protected void btnUnlist_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                int ID = Convert.ToInt32(((HiddenField)grdrow.FindControl("hdID")).Value);
                objMainClass.UpdateUnListedDetail(ID, Session["USERID"].ToString());
                var mailmessagefor = "Hi There,<br><br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ID is : " + ID.ToString() +  " Unlisted From " + ((Label)grdrow.FindControl("lblVendorName")).Text + " Vendor and Unlisted By " + Session["USERNAME"].ToString() + " <br>Please do needfull.<br><br>Regard,<br>Mobex Seller System<br><br><br>";
                EmailSend.EmailSent(mailmessagefor, "Product Unlisted From Mobex Seller System", "care@mobex.in", "mobex@123", "dispatch@qarmatek.com");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Product UnListed Successfully." + "\");", true);
                BindProductDetail();
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
                BindProductDetail();
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
                    (e.Row.FindControl("btnReserved") as LinkButton).Attributes.Add("onclick", "return ValidateReservedOrderNoRequired(" +
                    "'" + (e.Row.FindControl("txtorderno") as TextBox).ClientID
                    + "','" + (e.Row.FindControl("lblordernoalert") as Label).ClientID + "');");

                    if ((e.Row.FindControl("lblStatus") as Label).Text == "RESERVED")
                    {
                        (e.Row.FindControl("btnUnlist") as LinkButton).Visible   = true;
                        (e.Row.FindControl("btnConfirm") as LinkButton).Visible  = false;
                        (e.Row.FindControl("btnReserved") as LinkButton).Visible = false;
                    }
                    else if ((e.Row.FindControl("lblStatus") as Label).Text == "UNLISTED")
                    {
                        (e.Row.FindControl("btnUnlist") as LinkButton).Visible = false;
                        (e.Row.FindControl("btnConfirm") as LinkButton).Visible = false;
                        (e.Row.FindControl("btnReserved") as LinkButton).Visible = false;
                    }
                    else
                    {
                        (e.Row.FindControl("btnUnlist") as LinkButton).Attributes.Add("onclick", "return UnListedLoadingFunctionality('" +
                            (e.Row.FindControl("btnUnlist") as LinkButton).ClientID
                        + "');");

                        (e.Row.FindControl("btnUnlist") as LinkButton).Visible = true;
                        (e.Row.FindControl("btnConfirm") as LinkButton).Visible = true;
                        (e.Row.FindControl("btnReserved") as LinkButton).Visible = true;
                    }

                    if ((e.Row.FindControl("lblISAPPLICABLE") as Label).Text == "1")
                    {
                        (e.Row.FindControl("btnConfirm") as LinkButton).Visible = true;
                    }
                    else
                    {
                        (e.Row.FindControl("btnConfirm") as LinkButton).Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grdrow          = (GridViewRow)((LinkButton)sender).NamingContainer;
                int ID                      = Convert.ToInt32(((HiddenField)grdrow.FindControl("hdID")).Value);
                string NGEAPRV = "APPROVED";
                Decimal lockamount = 0;
                Decimal vendorprice = 0;
                int status = 0;
                string lblMake = (((Label)grdrow.FindControl("lblMake")).Text);
                string lblModel = (((Label)grdrow.FindControl("lblModel")).Text);
                string lblRom = (((Label)grdrow.FindControl("lblRom")).Text);
                string lblRam = (((Label)grdrow.FindControl("lblRam")).Text);
                string lblColor = (((Label)grdrow.FindControl("lblColor")).Text);
                status = Convert.ToInt32(((HiddenField)grdrow.FindControl("hdACTUALSTATUS")).Value);
                string lblVendorGrade = (((Label)grdrow.FindControl("lblVendorGrade")).Text);
                string lblVENDORPRICE = (((Label)grdrow.FindControl("lblVENDORPRICE")).Text);
                vendorprice = Convert.ToDecimal(lblVENDORPRICE);
                var dtlockamount = objMainClass.GetProductFinalListingRate(lblMake, lblModel, lblRam, lblRom, lblVendorGrade, lblColor);
                if (dtlockamount.Rows.Count > 0)
                {
                    lockamount = Convert.ToDecimal(dtlockamount.Rows[0]["FinalApproveListingAmount"]);
                }


                if (vendorprice > lockamount)
                {
                    status = 11233;
                    NGEAPRV = "REJECTED";
                }

                string lblFIRSTCREATEDDATE  = ((HiddenField)grdrow.FindControl("lblFIRSTCREATEDDATE")) == null ? "" : ((HiddenField)grdrow.FindControl("lblFIRSTCREATEDDATE")).Value;
                objMainClass.UpdateConfirmDetail(ID, Session["USERID"].ToString(), status, NGEAPRV, lockamount, lblFIRSTCREATEDDATE);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Product Relist Successfully." + "\");", true);
                BindProductDetail();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnReserved_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                int ID = Convert.ToInt32(((HiddenField)grdrow.FindControl("hdID")).Value);
                string orderno = ((TextBox)grdrow.FindControl("txtorderno")).Text;
                int result = objMainClass.UPDATEORDERNO(ID, Session["USERID"].ToString(), orderno);
                //SEND PUSH NOTIFICATION
                //string VENDORID = ((Label)grdrow.FindControl("lblVendorID")).Text;
                //string productdetail = ((Label)grdrow.FindControl("lblMake")).Text + " " + ((Label)grdrow.FindControl("lblModel")).Text + " " + ((Label)grdrow.FindControl("lblRam")).Text + " " + ((Label)grdrow.FindControl("lblRom")).Text + " " + ((Label)grdrow.FindControl("lblColor")).Text + " " + ((Label)grdrow.FindControl("lblVendorGrade")).Text;
                //var pushnotificationmsg = PushNotificationContentDetail.GETORDERRECEIVEDPUSHMESSAGE(ID.ToString(), productdetail);
                //SendPushNotification.SendPushNotificaion(PushNotificationContentDetail.GETORDERRECEIVEDPUSHSUBJECT(), pushnotificationmsg, Convert.ToInt32(VENDORID));
                //SEND PUSH NOTIFICATION
                BindProductDetail();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Product Reserved Successfully." + "\");", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}