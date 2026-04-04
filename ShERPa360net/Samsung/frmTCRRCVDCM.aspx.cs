using Ionic.Zip;
using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.Samsung
{
    public partial class frmTCRRCVDCM : System.Web.UI.Page
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
                    DataTable dtData = new DataTable();
                    dtData = objMainClass.GetSamsnugTCR("", "", "", "", "", "", 0, "", "RCVDFORCM");
                    if (dtData.Rows.Count > 0)
                    {
                        grvData.DataSource = dtData;
                        grvData.DataBind();
                        grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
                        lnkDownloadImages.Enabled = true;
                    }
                    else
                    {
                        grvData.DataSource = string.Empty;
                        grvData.DataBind();
                        lnkDownloadImages.Enabled = false;
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

        protected void lnkDownloadImages_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (grvData.Rows.Count > 0)
                    {
                        grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                    using (ZipFile zip = new ZipFile())
                    {
                        zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                        foreach (GridViewRow row in grvData.Rows)
                        {
                            Label lblRCPTNO = row.FindControl("lblRCPTNO") as Label;
                            Label lblCOMPLAINTNO = row.FindControl("lblCOMPLAINTNO") as Label;

                            DataTable dt = objMainClass.GetSamsnugTCR("", "", lblCOMPLAINTNO.Text, "", lblRCPTNO.Text, "", 0, "", "SELECT");
                            if (Convert.ToString(dt.Rows[0]["IMAGE"]) == "" || Convert.ToString(dt.Rows[0]["IMAGE"]) == string.Empty || Convert.ToString(dt.Rows[0]["IMAGE"]) == null)
                            {

                            }
                            else
                            {
                                zip.AddEntry(lblCOMPLAINTNO.Text + "_" + lblRCPTNO.Text + "" + Convert.ToString(dt.Rows[0]["EXTENSION"]), (byte[])dt.Rows[0]["IMAGE"]);
                            }
                        }

                        Response.Clear();
                        Response.BufferOutput = false;
                        string zipName = String.Format("TCR Transaction Images_" + Convert.ToString(DateTime.Now).Replace("/", "").Replace("-", "").Replace("_", "") + "_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                        Response.ContentType = "application/zip";
                        Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                        zip.Save(Response.OutputStream);
                        Response.End();
                    }

                    if (grvData.Rows.Count > 0)
                    {
                        grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
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
            finally
            {
                if (grvData.Rows.Count > 0)
                {
                    grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        protected void btnDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    string lblRCPTNO = ((Label)grdrow.FindControl("lblRCPTNO")).Text;
                    string lblCOMPLAINTNO = ((Label)grdrow.FindControl("lblCOMPLAINTNO")).Text;

                    DataTable dtData = new DataTable();
                    dtData = objMainClass.GetSamsnugTCR("", "", lblCOMPLAINTNO, "", lblRCPTNO, "", 0, "", "SELECT");
                    if (dtData.Rows.Count > 0)
                    {
                        lblPopRcptNo.Text = Convert.ToString(dtData.Rows[0]["RCPTNO"]);
                        lblPopComplaintNo.Text = Convert.ToString(dtData.Rows[0]["COMPLAINTNO"]);
                        lblPopCustName.Text = Convert.ToString(dtData.Rows[0]["CUSTNAME"]);
                        lblPopMobileNo.Text = Convert.ToString(dtData.Rows[0]["MOBILENO"]);
                        lblPopAddress.Text = Convert.ToString(dtData.Rows[0]["ADDRESS"]);
                        lblPopContactno.Text = Convert.ToString(dtData.Rows[0]["CONTACTNO"]);
                        lblPopModelNo.Text = Convert.ToString(dtData.Rows[0]["MODELNO"]);
                        lblPopSerialNo.Text = Convert.ToString(dtData.Rows[0]["SERIALNO"]);
                        lblPopTotalCost.Text = Convert.ToString(dtData.Rows[0]["TOTAL"]);
                        lblPopPaymode.Text = Convert.ToString(dtData.Rows[0]["PAYMODE"]);
                        lblPopTransactionID.Text = Convert.ToString(dtData.Rows[0]["TRANSACTIONID"]);
                        lblPopEntryBy.Text = Convert.ToString(dtData.Rows[0]["ENTRYBY"]);
                        lblPopEntryDate.Text = Convert.ToString(dtData.Rows[0]["CREATEDATE"]);

                        gvDetail.DataSource = dtData;
                        gvDetail.DataBind();

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
                    }

                    if (grvData.Rows.Count > 0)
                    {
                        grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);

                    if (grvData.Rows.Count > 0)
                    {
                        grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);

                if (grvData.Rows.Count > 0)
                {
                    grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {

            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    Label lblRCPTNO = (Label)grdrow.FindControl("lblRCPTNO");
                    Label lblCOMPLAINTNO = (Label)grdrow.FindControl("lblCOMPLAINTNO");

                    lblTCRNO.Text = lblRCPTNO.Text;
                    hfComplaintNo.Value = lblCOMPLAINTNO.Text;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#mb-aprv').modal();", true);

                    if (grvData.Rows.Count > 0)
                    {
                        grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);

                    if (grvData.Rows.Count > 0)
                    {
                        grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);

                if (grvData.Rows.Count > 0)
                {
                    grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }

        }

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    Label lblImageData = grdrow.FindControl("lblImageData") as Label;
                    Label lblImageExtension = grdrow.FindControl("lblImageExtension") as Label;
                    Label lblGVRcptNo = grdrow.FindControl("lblGVRcptNo") as Label;
                    Label lblGVComplaintNo = grdrow.FindControl("lblGVComplaintNo") as Label;
                    Image imgImage = grdrow.FindControl("imgImage") as Image;
                    DataTable dt = objMainClass.GetSamsnugTCR("", "", lblGVComplaintNo.Text, "", lblGVRcptNo.Text, "", 0, "", "SELECT");
                    byte[] bytes;
                    string fileName, contentType;
                    bytes = (byte[])dt.Rows[0]["IMAGE"];


                    if (lblImageExtension.Text == ".htm" || lblImageExtension.Text == ".html")
                    {
                        contentType = "text/HTML";
                    }
                    else if (lblImageExtension.Text == ".txt")
                    {
                        contentType = "text/plain";
                    }
                    else if (lblImageExtension.Text == ".doc" || lblImageExtension.Text == ".rtf" || lblImageExtension.Text == ".docx")
                    {
                        contentType = "Application/msword";
                    }
                    else if (lblImageExtension.Text == ".xls" || lblImageExtension.Text == ".xlsx")
                    {
                        contentType = "text/x-msexcel";
                    }
                    else if (lblImageExtension.Text == ".jpg" || lblImageExtension.Text == ".jpeg")
                    {
                        contentType = "image/jpeg";
                    }
                    //else if (lblImageExtension.Text == ".png" || lblImageExtension.Text == ".PNG")
                    //{
                    //    lblImageExtension.Text = ".jpeg";
                    //    contentType = "image/jpeg";
                    //}
                    else if (lblImageExtension.Text == ".gif")
                    {
                        contentType = "image/GIF";
                    }
                    else if (lblImageExtension.Text == ".pdf")
                    {
                        contentType = "application/pdf";
                    }
                    else
                    {
                        contentType = "image/jpeg";
                    }

                    fileName = lblGVComplaintNo.Text + "_" + lblGVRcptNo.Text + "" + lblImageExtension.Text;

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
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);

                if (grvData.Rows.Count > 0)
                {
                    grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        protected void gvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        DataRowView dr = (DataRowView)e.Row.DataItem;

                        //Label lblImageType = e.Row.FindControl("lblImageType") as Label;
                        Label lblImageData = e.Row.FindControl("lblImageData") as Label;
                        //Label lblImageID = e.Row.FindControl("lblImageID") as Label;
                        Label lblImageExtension = e.Row.FindControl("lblImageExtension") as Label;
                        LinkButton lnkDownload = e.Row.FindControl("lnkDownload") as LinkButton;

                        if (lblImageData.Text != null && lblImageData.Text != string.Empty && lblImageData.Text != "")
                        {
                            if (lblImageExtension.Text == ".jpg" || lblImageExtension.Text == ".jpeg" || lblImageExtension.Text == ".png")
                            {
                                string imageUrl = "data:image/jpg;base64," + Convert.ToBase64String((byte[])dr["IMAGE"]);
                                (e.Row.FindControl("imgImage") as Image).ImageUrl = imageUrl;
                                (e.Row.FindControl("imgImage") as Image).Visible = true;
                                //lnkDownload.Visible = false;
                            }
                            else
                            {
                                (e.Row.FindControl("imgImage") as Image).Visible = false;
                                lnkDownload.Visible = true;
                            }
                        }
                        else
                        {
                            (e.Row.FindControl("imgImage") as Image).Visible = false;
                            lnkDownload.Visible = false;
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

        protected void lnkPopReceived_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    int i = objMainClass.ReceiveTCR(lblTCRNO.Text, hfComplaintNo.Value, Convert.ToInt32(Session["USERID"]), "UPDATERCVDCM");

                    if (i == 1)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('TCR Amount received successfully!');$('.close').click(function(){window.location.href ='frmTCRRCVDCM.aspx' });", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Please try again!');", true);
                    }

                    if (grvData.Rows.Count > 0)
                    {
                        grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
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

        protected void lnkReceive_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    int i = objMainClass.ReceiveTCR(lblPopRcptNo.Text, lblPopComplaintNo.Text, Convert.ToInt32(Session["USERID"]), "UPDATERCVDCM");

                    if (i == 1)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('TCR Amount received successfully!');$('.close').click(function(){window.location.href ='frmTCRRCVDCM.aspx' });", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Please try again!');", true);
                    }

                    if (grvData.Rows.Count > 0)
                    {
                        grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
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

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow hrow = grvData.HeaderRow;
                    CheckBox chkSelectAll = (CheckBox)hrow.FindControl("chkSelectAll");
                    if (chkSelectAll.Checked == true)
                    {
                        for (int i = 0; i < grvData.Rows.Count; i++)
                        {
                            GridViewRow row = grvData.Rows[i];
                            CheckBox chkSelect = ((CheckBox)row.FindControl("chkSelect"));
                            chkSelect.Checked = true;
                        }

                    }
                    else
                    {
                        for (int i = 0; i < grvData.Rows.Count; i++)
                        {
                            GridViewRow row = grvData.Rows[i];
                            CheckBox chkSelect = ((CheckBox)row.FindControl("chkSelect"));
                            chkSelect.Checked = false;
                        }
                    }

                    if (grvData.Rows.Count > 0)
                    {
                        grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
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

        public int GetCount()
        {
            int iReturn = 0;
            try
            {
                if (Session["USERID"] != null)
                {
                    for (int i = 0; i < grvData.Rows.Count; i++)
                    {
                        GridViewRow row = grvData.Rows[i];
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

        protected void imgSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (GetCount() > 0)
                    {
                        int iCount = 0;
                        for (int i = 0; i < grvData.Rows.Count; i++)
                        {
                            GridViewRow row = grvData.Rows[i];
                            CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                            Label lblRCPTNO = (Label)row.FindControl("lblRCPTNO");
                            Label lblCOMPLAINTNO = (Label)row.FindControl("lblCOMPLAINTNO");

                            if (chkSelect.Checked == true)
                            {
                                int iResult = objMainClass.ReceiveTCR(lblRCPTNO.Text, lblCOMPLAINTNO.Text, Convert.ToInt32(Session["USERID"]), "UPDATERCVDCM");

                                if (iResult > 0)
                                {
                                    iCount = iCount + 1;
                                }
                            }
                        }

                        if (iCount > 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Record save sucessfully.  Record Saved : " + iCount + "\");$('.close').click(function(){window.location.href ='frmTCRRCVDCM.aspx' });", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('PB not selected to save. Select atleast one PB.!');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('TCR not selected to Receive. Select atleast one TCR.!');", true);
                    }

                    if (grvData.Rows.Count > 0)
                    {
                        grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
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