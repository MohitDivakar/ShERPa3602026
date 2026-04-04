using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.UTILITY
{
    public partial class frmViewBooking : System.Web.UI.Page
    {

        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        WAClass objWAClass = new WAClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["CROMALOTDATA"] = null;
                try
                {
                    if (Session["USERID"] != null)
                    {
                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menutabid.Value, "");
                        if (FormRights.bView == false)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        BindLotData();
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

        public void BindLotData()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetCromaBookingData(objMainClass.intCmpId, Convert.ToInt32(Session["USERID"]), "GETDATANEW");
                    if (dt.Rows.Count > 0)
                    {
                        gvList.DataSource = dt;
                        gvList.DataBind();
                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    else
                    {
                        gvList.DataSource = string.Empty;
                        gvList.DataBind();
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

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        Label lblSTATUS = (Label)e.Row.FindControl("lblSTATUS");
                        LinkButton btnAccept = (LinkButton)e.Row.FindControl("btnAccept");
                        LinkButton btnCancel = (LinkButton)e.Row.FindControl("btnCancel");
                        LinkButton btnReturn = (LinkButton)e.Row.FindControl("btnReturn");
                        LinkButton btnPurchase = (LinkButton)e.Row.FindControl("btnPurchase");


                        if (lblSTATUS.Text == "ACCEPTED")
                        {
                            btnAccept.Visible = false;
                            btnAccept.Enabled = false;

                            btnPurchase.Visible = true;
                            btnPurchase.Enabled = true;
                        }

                        if (lblSTATUS.Text == "CANCELLED")
                        {
                            btnAccept.Visible = false;
                            btnAccept.Enabled = false;

                            btnCancel.Visible = false;
                            btnCancel.Enabled = false;

                            btnPurchase.Visible = false;
                            btnPurchase.Enabled = false;
                        }

                        if (lblSTATUS.Text == "REFUNDED")
                        {
                            btnAccept.Visible = false;
                            btnAccept.Enabled = false;

                            btnCancel.Visible = false;
                            btnCancel.Enabled = false;

                            btnReturn.Visible = false;
                            btnReturn.Enabled = false;

                            btnPurchase.Visible = false;
                            btnPurchase.Enabled = false;
                        }

                        if (lblSTATUS.Text == "PURCHASED")
                        {
                            btnAccept.Visible = false;
                            btnAccept.Enabled = false;

                            btnCancel.Visible = false;
                            btnCancel.Enabled = false;

                            btnReturn.Visible = false;
                            btnReturn.Enabled = false;

                            btnPurchase.Visible = false;
                            btnPurchase.Enabled = false;
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    try
                    {
                        if (Session["USERID"] != null)
                        {
                            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

                            Label lblID = (Label)grdrow.FindControl("lblID");
                            Label lblCROMALOTNO = (Label)grdrow.FindControl("lblCROMALOTNO");
                            Label lblQTEKLOTNO = (Label)grdrow.FindControl("lblQTEKLOTNO");
                            Label lblBOOKIGTYPE = (Label)grdrow.FindControl("lblBOOKIGTYPE");
                            Label lblSALESPRICE = (Label)grdrow.FindControl("lblSALESPRICE");
                            Label lblBOOKINGAMT = (Label)grdrow.FindControl("lblBOOKINGAMT");


                            lblCancelCromaLotNo.Text = lblCROMALOTNO.Text;
                            lblCancelID.Text = lblID.Text;
                            lblCancelQtekLotNo.Text = lblQTEKLOTNO.Text;
                            lblCancelSalesPrice.Text = lblBOOKIGTYPE.Text;
                            lblCancelBookingType.Text = lblSALESPRICE.Text;
                            lblCancelBookingAmt.Text = lblBOOKINGAMT.Text;

                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Cancelled').modal();", true);

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

        protected void btnCancel_Click1(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    int i = objMainClass.UpdateLotCancel(objMainClass.intCmpId, 1, Convert.ToInt32(Session["USERID"]), txtCancelReacon.Text, 3, Convert.ToInt32(lblCancelID.Text), "UPDATECANCEL");

                    if (i == 1)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Booking Cancelled successfully!');$('.close').click(function(){window.location.href ='frmViewBooking.aspx' });", true);
                    }
                    else
                    {
                        string msg = "Booking not Cancelled. Please try again later..!";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + msg + "\");$('.close').click(function(){window.location.href ='frmViewBooking.aspx' });", true);
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

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

                    Label lblID = (Label)grdrow.FindControl("lblID");
                    Label lblCROMALOTNO = (Label)grdrow.FindControl("lblCROMALOTNO");
                    Label lblQTEKLOTNO = (Label)grdrow.FindControl("lblQTEKLOTNO");
                    Label lblBOOKIGTYPE = (Label)grdrow.FindControl("lblBOOKIGTYPE");
                    Label lblSALESPRICE = (Label)grdrow.FindControl("lblSALESPRICE");
                    Label lblBOOKINGAMT = (Label)grdrow.FindControl("lblBOOKINGAMT");


                    lblCromaVarLotNo.Text = lblCROMALOTNO.Text;
                    lblVarID.Text = lblID.Text;
                    lblCromaVarQtekNo.Text = lblQTEKLOTNO.Text;
                    lblVarSalesPrice.Text = lblBOOKIGTYPE.Text;
                    lblVarBookingType.Text = lblSALESPRICE.Text;
                    lblVarBookingAmt.Text = lblBOOKINGAMT.Text;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Varified').modal();", true);

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

        protected void btnSaveVarified_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    int i = objMainClass.UpdateLotAccept(objMainClass.intCmpId, Convert.ToDecimal(txtvarifiedAmount.Text), 1, Convert.ToInt32(Session["USERID"]), txtVarifiedRemarks.Text, 2, Convert.ToInt32(lblVarID.Text), "UPDATEACCEPT");

                    if (i == 1)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Booking Accepted successfully!');$('.close').click(function(){window.location.href ='frmViewBooking.aspx' });", true);
                    }
                    else
                    {
                        string msg = "Booking not accepted. Please try again later..!";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + msg + "\");$('.close').click(function(){window.location.href ='frmViewBooking.aspx' });", true);
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

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

                    Label lblID = (Label)grdrow.FindControl("lblID");
                    Label lblCROMALOTNO = (Label)grdrow.FindControl("lblCROMALOTNO");
                    Label lblQTEKLOTNO = (Label)grdrow.FindControl("lblQTEKLOTNO");
                    Label lblBOOKIGTYPE = (Label)grdrow.FindControl("lblBOOKIGTYPE");
                    Label lblSALESPRICE = (Label)grdrow.FindControl("lblSALESPRICE");
                    Label lblBOOKINGAMT = (Label)grdrow.FindControl("lblBOOKINGAMT");


                    lblReturnCromaLotNo.Text = lblCROMALOTNO.Text;
                    lblReturnID.Text = lblID.Text;
                    lblReturnQtekLotNo.Text = lblQTEKLOTNO.Text;
                    lblReturnSalesPrice.Text = lblBOOKIGTYPE.Text;
                    lblReturnBookingType.Text = lblSALESPRICE.Text;
                    lblReturnBookingAmt.Text = lblBOOKINGAMT.Text;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Returned').modal();", true);
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

        protected void btnReturn_Click1(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    int i = objMainClass.UpdateLotReturn(objMainClass.intCmpId, 1, Convert.ToInt32(Session["USERID"]), txtReturnRemarks.Text, txtReturnUTRNo.Text, 4, Convert.ToInt32(lblReturnID.Text), "UPDATERETURN");

                    if (i == 1)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Booking Returned successfully!');$('.close').click(function(){window.location.href ='frmViewBooking.aspx' });", true);
                    }
                    else
                    {
                        string msg = "Booking not Returned. Please try again later..!";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + msg + "\");$('.close').click(function(){window.location.href ='frmViewBooking.aspx' });", true);
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

        protected void btnPurchase_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

                    Label lblID = (Label)grdrow.FindControl("lblID");
                    int i = objMainClass.UpdateLotPurchase(objMainClass.intCmpId, Convert.ToInt32(Session["USERID"]), 5, "", Convert.ToInt32(lblID.Text), "UPDATEPURCHASED");

                    if (i == 1)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Booking Purchased successfully!');$('.close').click(function(){window.location.href ='frmViewBooking.aspx' });", true);
                    }
                    else
                    {
                        string msg = "Booking not Purchased. Please try again later..!";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + msg + "\");$('.close').click(function(){window.location.href ='frmViewBooking.aspx' });", true);
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