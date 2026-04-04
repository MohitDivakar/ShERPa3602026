using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.MM
{
    public partial class ViewNewItemReq : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Session["USERID"] != null)
                    {

                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menutabid.Value, "");
                        if (FormRights.bView == false) //if (objDALUserRights.bView == false)
                        {
                            //Session.Abandon();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        chkPendingOnly.Checked = true;
                        DataTable dt = new DataTable();
                        dt = objMainClass.GetNewItemReq(1, txtFromDate.Text, txtToDate.Text, chkPendingOnly.Checked == true ? 1 : 0, "");
                        gvList.DataSource = dt;
                        gvList.DataBind();
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

        protected void lnkSearhPR_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objMainClass.GetNewItemReq(1, txtFromDate.Text, txtToDate.Text, chkPendingOnly.Checked == true ? 1 : 0, "");
                if (dt.Rows.Count > 0)
                {
                    gvList.DataSource = dt;
                    gvList.DataBind();
                }
                else
                {
                    gvList.DataSource = string.Empty;
                    gvList.DataBind();

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            string prno = grdrow.Cells[0].Text;
            lblPopupPRNO.Text = prno;
            string status = grdrow.Cells[7].Text;
            if (status == "Approval Pending")
            {
                //bool result = objDALUserRights.APPROVE_RIGHTS("PR", Convert.ToString(Session["USERID"]), Convert.ToString(Session["DEPTCD"]), prno);
                //if (result == false)
                //{
                //    lnkPopReject.Visible = false;
                //    lnkPopApprove.Visible = false;
                //    //lnkReject.Visible = false;
                //    //lnkApprove.Visible = false;
                //    lblRightsMessage.Text = "You are not authorized to Approve or Reject this PR!";
                //    lblRightsMessage.Visible = true;
                //}
                //else if (result == true)
                //{
                lnkPopReject.Visible = true;
                lnkPopApprove.Visible = true;
                //    //lnkReject.Visible = true;
                //    //lnkApprove.Visible = true;
                lblRightsMessage.Text = string.Empty;
                lblRightsMessage.Visible = false;
                //}


                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#mb-aprv').modal();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('PR is already " + status + "');", true);
            }
        }

        protected void lnkPopReject_Click(object sender, EventArgs e)
        {
            try
            {
                //SP_UPDATE_NEW_ITEMREQ
                bool result = objMainClass.UpdateNewItemReq(Convert.ToInt32(lblPopupPRNO.Text), (int)STATUS.Cancelled, Convert.ToString(Session["USERID"]));
                if (result == true)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetNewItemReq(1, txtFromDate.Text, txtToDate.Text, chkPendingOnly.Checked == true ? 1 : 0, "");
                    gvList.DataSource = dt;
                    gvList.DataBind();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Item Request Cancelled successfully!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Please try again!');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lnkPopApprove_Click(object sender, EventArgs e)
        {
            try
            {
                bool result = objMainClass.UpdateNewItemReq(Convert.ToInt32(lblPopupPRNO.Text), (int)STATUS.Approved, Convert.ToString(Session["USERID"]));
                if (result == true)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetNewItemReq(1, txtFromDate.Text, txtToDate.Text, chkPendingOnly.Checked == true ? 1 : 0, "");
                    gvList.DataSource = dt;
                    gvList.DataBind();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Item Request approved successfully!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Please try again!');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}