using Ionic.Zip;
using Microsoft.Reporting.WebForms;
using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.Samsung
{
    public partial class frmViewTCR : System.Web.UI.Page
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
                        objBindDDL.FillPaymentMode(ddlPayMode);
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
                    dtData = objMainClass.GetSamsnugTCR(txtFromDate.Text, txtToDate.Text, txtComplaintNo.Text, txtMobileNo.Text, txtReceipt.Text, "", ddlPayMode.SelectedIndex > 0 ? Convert.ToInt32(ddlPayMode.SelectedValue) : 0, "", "SELECT");
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

        protected void lnkSerch_Click(object sender, EventArgs e)
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
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    string lblRCPTNO = ((Label)grdrow.FindControl("lblRCPTNO")).Text;
                    string lblCOMPLAINTNO = ((Label)grdrow.FindControl("lblCOMPLAINTNO")).Text;
                    string lblMOBILENO = ((Label)grdrow.FindControl("lblMOBILENO")).Text;
                    string lblTOTAL = ((Label)grdrow.FindControl("lblTOTAL")).Text;
                    string URL = DownloadTCR(lblCOMPLAINTNO, lblRCPTNO);
                    if (URL != null && URL != "" && URL != string.Empty)
                    {
                        string FileName = lblCOMPLAINTNO + "_" + lblRCPTNO + "_TCR.pdf";
                        string PDFURL = "http://14.98.132.190:360/excel/" + FileName;
                        //objWAClass.SendMediaFile("Dear Sir/Mam We have received, an Amount of Rs " + lblTOTAL + ". Please check attached Technician Collection Receipt.", "91" + lblMOBILENO, Convert.ToString(Session["USERID"]), PDFURL);
                        objWAClass.SendMessageNewAPI("Dear Sir/Mam We have received, an Amount of Rs " + lblTOTAL + ". Please check attached Technician Collection Receipt.", "91" + lblMOBILENO, Convert.ToString(Session["USERID"]), PDFURL);
                        //Thread.Sleep(5000);
                        File.Delete(URL);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('TCR copy sent to Customer.!');$('.close').click(function(){window.location.href ='frmViewTCR.aspx' });", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('TCR copy not sent to Customer.!');$('.close').click(function(){window.location.href ='frmViewTCR.aspx' });", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                }
                if (grvData.Rows.Count > 0)
                {
                    grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public string DownloadTCR(string COMPLAINTNO, string RCPTNO)
        {
            string url = "";
            DataTable dtTCR = new DataTable();
            dtTCR = objMainClass.GetSamsnugTCR("", "", COMPLAINTNO, "", RCPTNO, "", 0, "", "SELECT");
            DataTable dtTCRParts = new DataTable();
            dtTCRParts = objMainClass.GetSamsnugTCR("", "", COMPLAINTNO, "", RCPTNO, "", 0, "", "PARTDETAILS");
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = "Report/rptTCR.rdlc";
            ReportDataSource rds = new ReportDataSource("DSTCR", dtTCR);
            ReportDataSource rds1 = new ReportDataSource("DSTCRPARTS", dtTCRParts);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.DataSources.Add(rds1);

            string FileName = COMPLAINTNO + "_" + RCPTNO + "_TCR.pdf";
            string extension = ".pdf";
            string encoding = String.Empty;
            Warning[] warnings;
            string mimeType = String.Empty;
            string[] streams;
            string[] streamIds;
            string contentType = String.Empty;

            Byte[] mybytes = ReportViewer1.LocalReport.Render("PDF", null,
                    out extension, out encoding,
                    out mimeType, out streams, out warnings);

            //string localPath = AppDomain.CurrentDomain.BaseDirectory;

            //string localPath = Server.MapPath(@"http://14.98.132.190:360/img/") + FileName;
            string folderpath = "~/excel/";
            string filePath = Path.Combine(Server.MapPath(folderpath), FileName);

            string localPath = (Server.MapPath("../img/" + FileName));
            //localPath = localPath + FileName;
            //System.IO.File.WriteAllBytes(localPath, mybytes);
            System.IO.File.WriteAllBytes(filePath, mybytes);
            //url = localPath;
            url = filePath;

            //url = "http://14.98.132.190:360/img/" + FileName;
            return url;
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
    }
}