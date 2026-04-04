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
    public partial class frmExchangePriceAprv : System.Web.UI.Page
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
                        objBindDDL.FillStoreData(ddlStore, Convert.ToString(Session["CROMASTOREID"]), "GETSTOREDATA");
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
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetCromaPickupData(objMainClass.intCmpId, ddlStore.SelectedIndex > 0 ? ddlStore.SelectedValue : Convert.ToString(Session["CROMASTOREID"]), txtFromDate.Text, txtToDate.Text, "GETPRICEAPPROVAL");
                    if (dt.Rows.Count > 0)
                    {
                        gvList.DataSource = dt;
                        gvList.DataBind();
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


        protected void lnkSearh_Click(object sender, EventArgs e)
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

        public int GetCount()
        {
            int iReturn = 0;
            try
            {
                if (Session["USERID"] != null)
                {
                    for (int i = 0; i < gvList.Rows.Count; i++)
                    {
                        GridViewRow row = gvList.Rows[i];
                        CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                        if (chkSelect.Checked == true)
                        {
                            iReturn = iReturn + 1;
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
            return iReturn;
        }



        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow hrow = gvList.HeaderRow;
                    CheckBox chkSelectAll = (CheckBox)hrow.FindControl("chkSelectAll");
                    if (chkSelectAll.Checked == true)
                    {
                        for (int i = 0; i < gvList.Rows.Count; i++)
                        {
                            GridViewRow row = gvList.Rows[i];
                            CheckBox chkSelect = ((CheckBox)row.FindControl("chkSelect"));
                            chkSelect.Checked = true;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < gvList.Rows.Count; i++)
                        {
                            GridViewRow row = gvList.Rows[i];
                            CheckBox chkSelect = ((CheckBox)row.FindControl("chkSelect"));
                            chkSelect.Checked = false;
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

        protected void lnkApproveReject_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (GetCount() > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Select atleast one Record to Approve/Reject data.!');", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ShowLoadingReverse();", true);
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

        protected void lnkApprove_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (GetCount() > 0)
                    {
                        int savedt = 0;
                        int sel = 0;
                        for (int i = 0; i < gvList.Rows.Count; i++)
                        {
                            GridViewRow row = gvList.Rows[i];
                            CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                            Label lblID = (Label)row.FindControl("lblID");
                            if (chkSelect.Checked == true)
                            {
                                sel = sel + 1;
                                int iResult = objMainClass.UpdatePriceApproval(objMainClass.intCmpId, Convert.ToInt32(lblID.Text), (int)STATUS.PBApproved, Convert.ToString(Session["USERNAME"]), txtReason.Text, "APPROVEREJPRICE");
                                if (iResult == 1)
                                {
                                    savedt = savedt + 1;
                                }
                            }
                        }

                        if (savedt == 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"Price approval not saved. Please try again. Selected record : " + sel + ". Upadated record : " + savedt + "\");$('.close').click(function(){window.location.href ='frmExchangePriceAprv.aspx' });", true);
                        }
                        else if (savedt == sel)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Price approval saved successfully. Selected record : " + sel + ". Upadated record : " + savedt + "\");$('.close').click(function(){window.location.href ='frmExchangePriceAprv.aspx' });", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Select atleast one Record to Approve data.!');", true);
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
        protected void lnkReject_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (GetCount() > 0)
                    {
                        int savedt = 0;
                        int sel = 0;
                        for (int i = 0; i < gvList.Rows.Count; i++)
                        {
                            GridViewRow row = gvList.Rows[i];
                            CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                            Label lblID = (Label)row.FindControl("lblID");
                            if (chkSelect.Checked == true)
                            {
                                sel = sel + 1;
                                int iResult = objMainClass.UpdatePriceApproval(objMainClass.intCmpId, Convert.ToInt32(lblID.Text), (int)STATUS.PBRejected, Convert.ToString(Session["USERNAME"]), txtReason.Text, "APPROVEREJPRICE");
                                if (iResult == 1)
                                {
                                    savedt = savedt + 1;
                                }
                            }
                        }

                        if (savedt == 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"Price not Rejected. Please try again. Selected record : " + sel + ". Upadated record : " + savedt + "\");$('.close').click(function(){window.location.href ='frmExchangePriceAprv.aspx' });", true);
                        }
                        else if (savedt == sel)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Price Rejected successfully. Selected record : " + sel + ". Upadated record : " + savedt + "\");$('.close').click(function(){window.location.href ='frmExchangePriceAprv.aspx' });", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Select atleast one Record to Approve data.!');", true);
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

        protected void btnApproveReject_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    Label lblID = (Label)grdrow.FindControl("lblID");
                    hfbuybackid.Value = lblID.Text;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Popdetail').modal();", true);
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

        protected void lnkPopApprove_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    int iResult = objMainClass.UpdatePriceApproval(objMainClass.intCmpId, Convert.ToInt32(hfbuybackid.Value), (int)STATUS.PBApproved, Convert.ToString(Session["USERNAME"]), txtPopReason.Text, "APPROVEREJPRICE");
                    if (iResult == 1)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Price Approved successfully!');$('.close').click(function(){window.location.href ='frmExchangePriceAprv.aspx' });", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Price Not Approved successfully. Please try again.!');$('.close').click(function(){window.location.href ='frmExchangePriceAprv.aspx' });", true);
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

        protected void lnkPopReject_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    int iResult = objMainClass.UpdatePriceApproval(objMainClass.intCmpId, Convert.ToInt32(hfbuybackid.Value), (int)STATUS.PBRejected, Convert.ToString(Session["USERNAME"]), txtPopReason.Text, "APPROVEREJPRICE");
                    if (iResult == 1)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Price Rejcted successfully!');$('.close').click(function(){window.location.href ='frmExchangePriceAprv.aspx' });", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Price Not Rejcted successfully. Please try again.!');$('.close').click(function(){window.location.href ='frmExchangePriceAprv.aspx' });", true);
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

        protected void lnkInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    Label lblID = (Label)grdrow.FindControl("lblID");
                    Label lblINVIMAGE = (Label)grdrow.FindControl("lblINVIMAGE");
                    Label lblFULLNAME = (Label)grdrow.FindControl("lblFULLNAME");

                    if (lblINVIMAGE.Text != null && lblINVIMAGE.Text != string.Empty && lblINVIMAGE.Text != "")
                    {
                        byte[] bytes = null;
                        string fileName = "", contentType = "", extension = "";

                        if (lblINVIMAGE.Text.Contains("/pdf") || lblINVIMAGE.Text.Contains("/PDF"))
                        {
                            string data = lblINVIMAGE.Text.Replace("data:application/pdf;base64,", "");
                            contentType = "application/pdf";
                            bytes = Convert.FromBase64String(data);
                            extension = "pdf";
                        }
                        else if (lblINVIMAGE.Text.Contains("/png") || lblINVIMAGE.Text.Contains("/PNG"))
                        {
                            string data = lblINVIMAGE.Text.Replace("data:image/png;base64,", "");
                            contentType = "image/png";
                            bytes = Convert.FromBase64String(data);
                            extension = "png";
                        }
                        else if (lblINVIMAGE.Text.Contains("/jpg") || lblINVIMAGE.Text.Contains("/JPG"))
                        {
                            string data = lblINVIMAGE.Text.Replace("data:image/jpg;base64,", "");
                            contentType = "image/jpg";
                            bytes = Convert.FromBase64String(data);
                            extension = "jpg";
                        }
                        else if (lblINVIMAGE.Text.Contains("/jpeg") || lblINVIMAGE.Text.Contains("/JPEG"))
                        {
                            string data = lblINVIMAGE.Text.Replace("data:image/jpeg;base64,", "");
                            contentType = "image/jpeg";
                            bytes = Convert.FromBase64String(data);
                            extension = "jpeg";
                        }

                        fileName = lblFULLNAME.Text + "_Invoice." + extension;

                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = contentType;
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                        Response.BinaryWrite(bytes);
                        Response.Flush();
                        Response.End();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invoice not uploaded.!');", true);
                    }
                }
                else
                {
                    Session.Abandon();
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