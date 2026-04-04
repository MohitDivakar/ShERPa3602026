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
    public partial class frmDemoInstallationList : System.Web.UI.Page
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
                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();

                        GetData();
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

        public void GetData()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dtMain = new DataTable();
                    dtMain = objMainClass.GetInstallationData(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, txtSINo.Text, "", 0, "GETDATA", 0, 0, 0);

                    if (dtMain.Rows.Count > 0)
                    {
                        gvList.DataSource = dtMain;
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

        protected void lnkSearhSI_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GetData();
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    GridViewRow grdrow = (GridViewRow)((Button)sender).NamingContainer;
                    Label lblIDv = (Label)grdrow.FindControl("lblID");
                    Label lblSONO = (Label)grdrow.FindControl("lblSONO");
                    Label lblSRNO = (Label)grdrow.FindControl("lblSRNO");

                    TextBox txtActualDispDate = (TextBox)grdrow.FindControl("txtActualDispDate");
                    TextBox txtInstReqOn = (TextBox)grdrow.FindControl("txtInstReqOn");
                    TextBox txtDemoReqOn = (TextBox)grdrow.FindControl("txtDemoReqOn");
                    TextBox txtDemoInstDoneBy = (TextBox)grdrow.FindControl("txtDemoInstDoneBy");
                    TextBox txtInstCompOn = (TextBox)grdrow.FindControl("txtInstCompOn");
                    TextBox txtDemoCompOn = (TextBox)grdrow.FindControl("txtDemoCompOn");
                    TextBox txtChargestobe = (TextBox)grdrow.FindControl("txtChargestobe");
                    TextBox txtChargercvdstore = (TextBox)grdrow.FindControl("txtChargercvdstore");
                    TextBox txtChargesrcvdon = (TextBox)grdrow.FindControl("txtChargesrcvdon");
                    TextBox txtChargercvdaccount = (TextBox)grdrow.FindControl("txtChargercvdaccount");
                    TextBox txtChargesrcvdonaccount = (TextBox)grdrow.FindControl("txtChargesrcvdonaccount");

                    CheckBox chkFinalEntry = (CheckBox)grdrow.FindControl("chkFinalEntry");



                    //UpdateDemoIntsllation

                    int i = objMainClass.UpdateDemoIntsllation(objMainClass.intCmpId, txtActualDispDate.Text, txtInstReqOn.Text, txtDemoReqOn.Text, txtDemoInstDoneBy.Text, txtInstCompOn.Text, txtDemoCompOn.Text, txtChargestobe.Text,
                       txtChargercvdstore.Text, txtChargesrcvdon.Text, txtChargercvdaccount.Text, txtChargesrcvdonaccount.Text, chkFinalEntry.Checked == true ? 1 : 0,
                       Convert.ToInt32(Session["USERID"]), lblSONO.Text, Convert.ToInt32(lblSRNO.Text), "UPDATEDEMOINST");

                    if (i == 1)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Record saved successfully.!');", true);
                        GetData();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Record not savd.!');", true);
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
                        TextBox txtActualDispDate = (TextBox)e.Row.FindControl("txtActualDispDate");
                        //TextBox txtActualDispDateRemarks = (TextBox)e.Row.FindControl("");
                        if (txtActualDispDate.Text != null && txtActualDispDate.Text != "" && txtActualDispDate.Text != string.Empty)
                        {
                            txtActualDispDate.Enabled = false;
                            //txtActualDispDateRemarks.Enabled = false;
                        }

                        TextBox txtInstReqOn = (TextBox)e.Row.FindControl("txtInstReqOn");
                        //TextBox txtInstReqOnRemarks = (TextBox)e.Row.FindControl("");
                        if (txtInstReqOn.Text != null && txtInstReqOn.Text != "" && txtInstReqOn.Text != string.Empty)
                        {
                            txtInstReqOn.Enabled = false;
                            //txtInstReqOnRemarks.Enabled = false;
                        }

                        TextBox txtDemoReqOn = (TextBox)e.Row.FindControl("txtDemoReqOn");
                        //TextBox txtDemoReqOnRemarks = (TextBox)e.Row.FindControl("txtDemoReqOnRemarks");
                        if (txtDemoReqOn.Text != null && txtDemoReqOn.Text != "" && txtDemoReqOn.Text != string.Empty)
                        {
                            txtDemoReqOn.Enabled = false;
                            //txtDemoReqOnRemarks.Enabled = false;
                        }

                        TextBox txtDemoInstDoneByv = (TextBox)e.Row.FindControl("txtDemoInstDoneBy");
                        //TextBox txtDemoInstDoneByRemarks = (TextBox)e.Row.FindControl("txtDemoInstDoneByRemarks");
                        if (txtDemoInstDoneByv.Text != null && txtDemoInstDoneByv.Text != "" && txtDemoInstDoneByv.Text != string.Empty)
                        {
                            txtDemoInstDoneByv.Enabled = false;
                            //txtDemoInstDoneByRemarks.Enabled = false;
                        }

                        TextBox txtInstCompOn = (TextBox)e.Row.FindControl("txtInstCompOn");
                        //TextBox txtInstCompOnRemarks = (TextBox)e.Row.FindControl("txtInstCompOnRemarks");
                        if (txtInstCompOn.Text != null && txtInstCompOn.Text != "" && txtInstCompOn.Text != string.Empty)
                        {
                            txtInstCompOn.Enabled = false;
                            //txtInstCompOnRemarks.Enabled = false;
                        }

                        TextBox txtDemoCompOn = (TextBox)e.Row.FindControl("txtDemoCompOn");
                        //TextBox txtDemoCompOnRemarks = (TextBox)e.Row.FindControl("txtDemoCompOnRemarks");
                        if (txtDemoCompOn.Text != null && txtDemoCompOn.Text != "" && txtDemoCompOn.Text != string.Empty)
                        {
                            txtDemoCompOn.Enabled = false;
                            //txtDemoCompOnRemarks.Enabled = false;
                        }

                        TextBox txtChargestobe = (TextBox)e.Row.FindControl("txtChargestobe");
                        //TextBox txtChargestobeRemarks = (TextBox)e.Row.FindControl("txtChargestobeRemarks");
                        if (txtChargestobe.Text != null && txtChargestobe.Text != "" && txtChargestobe.Text != string.Empty)
                        {
                            txtChargestobe.Enabled = false;
                            //txtChargestobeRemarks.Enabled = false;
                        }

                        TextBox txtChargercvdstore = (TextBox)e.Row.FindControl("txtChargercvdstore");
                        //TextBox txtChargercvdstoreRemarks = (TextBox)e.Row.FindControl("txtChargercvdstoreRemarks");
                        if (txtChargercvdstore.Text != null && txtChargercvdstore.Text != "" && txtChargercvdstore.Text != string.Empty)
                        {
                            txtChargercvdstore.Enabled = false;
                            //txtChargercvdstoreRemarks.Enabled = false;
                        }

                        TextBox txtChargesrcvdon = (TextBox)e.Row.FindControl("txtChargesrcvdon");
                        //TextBox txtChargesrcvdonRemarks = (TextBox)e.Row.FindControl("txtChargesrcvdonRemarks");
                        if (txtChargesrcvdon.Text != null && txtChargesrcvdon.Text != "" && txtChargesrcvdon.Text != string.Empty)
                        {
                            txtChargesrcvdon.Enabled = false;
                            //txtChargesrcvdonRemarks.Enabled = false;
                        }

                        TextBox txtChargercvdaccount = (TextBox)e.Row.FindControl("txtChargercvdaccount");
                        //TextBox txtChargercvdaccountRemarks = (TextBox)e.Row.FindControl("txtChargercvdaccountRemarks");
                        if (txtChargercvdaccount.Text != null && txtChargercvdaccount.Text != "" && txtChargercvdaccount.Text != string.Empty)
                        {
                            txtChargercvdaccount.Enabled = false;
                            //txtChargercvdaccountRemarks.Enabled = false;
                        }

                        TextBox txtChargesrcvdonaccount = (TextBox)e.Row.FindControl("txtChargesrcvdonaccount");
                        //TextBox txtChargesrcvdonaccountRemarks = (TextBox)e.Row.FindControl("txtChargesrcvdonaccountRemarks");
                        if (txtChargesrcvdonaccount.Text != null && txtChargesrcvdonaccount.Text != "" && txtChargesrcvdonaccount.Text != string.Empty)
                        {
                            txtChargesrcvdonaccount.Enabled = false;
                            //txtChargesrcvdonaccountRemarks.Enabled = false;
                        }


                        CheckBox chkFinalEntry = (CheckBox)e.Row.FindControl("chkFinalEntry");
                        Label lblFinalENtry = (Label)e.Row.FindControl("lblFinalENtry");
                        Button btnUpdate = (Button)e.Row.FindControl("btnUpdate");
                        if (lblFinalENtry.Text == "1")
                        {
                            chkFinalEntry.Checked = true;
                            btnUpdate.Enabled = false;
                            chkFinalEntry.Enabled = false;
                        }
                        else
                        {
                            chkFinalEntry.Checked = false;
                            btnUpdate.Enabled = true;
                            chkFinalEntry.Enabled = true;
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