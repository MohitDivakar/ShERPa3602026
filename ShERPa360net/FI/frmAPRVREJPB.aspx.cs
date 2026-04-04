using Ionic.Zip;
using Microsoft.Reporting.WebForms;
using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.FI
{
    public partial class frmAPRVREJPB : System.Web.UI.Page
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
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        BindRcvdBy();
                        BindSentBy();
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


        public void BindRcvdBy()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetRCVAPRVREJPBData(1, "", "", 0, 0, 0, "RCVDBY");

                    if (dt.Rows.Count > 0)
                    {
                        ddlRcvdBy.DataSource = dt;
                        ddlRcvdBy.DataTextField = "USERNAME";
                        ddlRcvdBy.DataValueField = "RCVDBY";
                        ddlRcvdBy.DataBind();
                        ddlRcvdBy.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- SELECT --", "0"));
                    }
                    else
                    {
                        ddlRcvdBy.DataSource = string.Empty;
                        ddlRcvdBy.DataBind();
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

        public void BindSentBy()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetRCVAPRVREJPBData(1, "", "", 0, 0, 0, "SENDBY");

                    if (dt.Rows.Count > 0)
                    {
                        ddlSentBy.DataSource = dt;
                        ddlSentBy.DataTextField = "USERNAME";
                        ddlSentBy.DataValueField = "SENDBYUSER";
                        ddlSentBy.DataBind();
                        ddlSentBy.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- SELECT --", "0"));
                    }
                    else
                    {
                        ddlSentBy.DataSource = string.Empty;
                        ddlSentBy.DataBind();
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

        public void GetData()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetRCVAPRVREJPBData(objMainClass.intCmpId, "PB", txtPBNO.Text, (int)STATUS.PBReceived, Convert.ToInt32(ddlSentBy.SelectedValue), Convert.ToInt32(ddlRcvdBy.SelectedValue), "PENDINGAPRVREJPB");

                    if (dt.Rows.Count > 0)
                    {
                        gvAllList.DataSource = dt;
                        gvAllList.DataBind();
                    }
                    else
                    {
                        gvAllList.DataSource = string.Empty;
                        gvAllList.DataBind();
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

        protected void imgSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    //string PBNOS = "";
                    //string EMAILIDS = "";
                    if (GetCount() > 0)
                    {
                        int iCount = 0;
                        for (int i = 0; i < gvAllList.Rows.Count; i++)
                        {
                            GridViewRow row = gvAllList.Rows[i];
                            CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                            Label lblID = (Label)row.FindControl("lblID");
                            Label lblPBNO = (Label)row.FindControl("lblPBNO");
                            Label lblEMAILID = (Label)row.FindControl("lblEMAILID");

                            if (chkSelect.Checked == true)
                            {
                                int iResult = objMainClass.UpdateDOCList(objMainClass.intCmpId, Convert.ToInt32(lblID.Text), Convert.ToInt32(Session["USERID"]), (int)STATUS.PBApproved, Convert.ToInt32(Session["USERID"]), "RECEIVEPB", "");

                                if (iResult > 0)
                                {
                                    iCount = iCount + 1;
                                    //if (PBNOS == "" && PBNOS == string.Empty && PBNOS == null)
                                    //{
                                    //    PBNOS = lblPBNO.Text;
                                    //}
                                    //else
                                    //{
                                    //    PBNOS = PBNOS + "</br>" + lblPBNO.Text;
                                    //}

                                    //if (lblEMAILID.Text != "" && lblEMAILID.Text != null && lblEMAILID.Text != string.Empty)
                                    //{
                                    //    if (EMAILIDS == "" && EMAILIDS == string.Empty && EMAILIDS == null)
                                    //    {
                                    //        EMAILIDS = lblEMAILID.Text;
                                    //    }
                                    //    else
                                    //    {
                                    //        EMAILIDS = EMAILIDS + "," + lblEMAILID.Text;
                                    //    }
                                    //}

                                    //CommonEmailSend objEmailSend = new CommonEmailSend();
                                    //objEmailSend.CommonEmailSent("customercare@qarmatek.com", "Saz43287", "PB Approved - " + lblPBNO.Text, lblEMAILID.Text, "accounts@qarmatek.com", "", "", "", "", "PB No. " + lblPBNO.Text + " approved by account team. </br></br>", 587, "smtp.office365.com");

                                    if (lblEMAILID.Text != "" && lblEMAILID.Text != null && lblEMAILID.Text != string.Empty)
                                    {
                                        objMainClass.SaveMailNotification(objMainClass.EmailID, objMainClass.Password, lblEMAILID.Text, "accounts@qarmatek.com", "PB Approved - " + lblPBNO.Text, "PB No. " + lblPBNO.Text + " approved by account team. </br></br>", objMainClass.PORT, lblPBNO.Text, Convert.ToString(Session["USERID"]), "PB");
                                    }

                                }
                            }
                        }

                        if (iCount > 0)
                        {
                            //if (EMAILIDS != "" && EMAILIDS != null && EMAILIDS != string.Empty)
                            //{
                            //    CommonEmailSend objEmailSend = new CommonEmailSend();
                            //    objEmailSend.CommonEmailSent("customercare@qarmatek.com", "Saz43287", "PB Approved", EMAILIDS, "accounts@qarmatek.com", "", "", "", "", "PB No. " + PBNOS + " approved by account team. </br></br>", 587, "smtp.office365.com");
                            //}

                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Record save sucessfully.  Record Saved : " + iCount + "\");$('.close').click(function(){window.location.href ='frmAPRVREJPB.aspx' });", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('PB not selected to save. Select atleast one PB.!');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('PB not selected to save. Select atleast one PB.!');", true);
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
                    for (int i = 0; i < gvAllList.Rows.Count; i++)
                    {
                        GridViewRow row = gvAllList.Rows[i];
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


        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void lnkExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    string attachment = "attachment; filename=PBReceive" + DateTime.Now + ".xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vdn.ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    gvAllList.RenderControl(htw);
                    Response.Write(sw.ToString());
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

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow hrow = gvAllList.HeaderRow;
                    CheckBox chkSelectAll = (CheckBox)hrow.FindControl("chkSelectAll");
                    if (chkSelectAll.Checked == true)
                    {
                        for (int i = 0; i < gvAllList.Rows.Count; i++)
                        {
                            GridViewRow row = gvAllList.Rows[i];
                            CheckBox chkSelect = ((CheckBox)row.FindControl("chkSelect"));
                            chkSelect.Checked = true;
                        }

                    }
                    else
                    {
                        for (int i = 0; i < gvAllList.Rows.Count; i++)
                        {
                            GridViewRow row = gvAllList.Rows[i];
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

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((Button)sender).NamingContainer;
                    Label lblID = (Label)grdrow.FindControl("lblID");
                    Label lblPBNO = (Label)grdrow.FindControl("lblPBNO");
                    Label lblEMAILID = (Label)grdrow.FindControl("lblEMAILID");

                    int iResult = objMainClass.UpdateDOCList(objMainClass.intCmpId, Convert.ToInt32(lblID.Text), Convert.ToInt32(Session["USERID"]), (int)STATUS.PBApproved, Convert.ToInt32(Session["USERID"]), "RECEIVEPB", "");

                    if (iResult > 0)
                    {
                        if (lblEMAILID.Text != "" && lblEMAILID.Text != null && lblEMAILID.Text != string.Empty)
                        {
                            //CommonEmailSend objEmailSend = new CommonEmailSend();
                            //objEmailSend.CommonEmailSent("customercare@qarmatek.com", "Saz43287", "PB Approved - " + lblPBNO.Text, lblEMAILID.Text, "accounts@qarmatek.com", "", "", "", "", "PB No. " + lblPBNO.Text + " approved by account team. </br></br>", 25, "smtp.office365.com");

                            objMainClass.SaveMailNotification(objMainClass.EmailID, objMainClass.Password, lblEMAILID.Text, "accounts@qarmatek.com", "PB Approved - " + lblPBNO.Text, "PB No. " + lblPBNO.Text + " approved by account team. </br></br>", objMainClass.PORT, lblPBNO.Text, Convert.ToString(Session["USERID"]), "PB");
                        }



                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Record save sucessfully.  Record Saved : " + iResult + "\");$('.close').click(function(){window.location.href ='frmAPRVREJPB.aspx' });", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('PB not selected to save. Select atleast one PB.!');", true);
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

        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    GridViewRow grdrow = (GridViewRow)((Button)sender).NamingContainer;
                    Label lblID = (Label)grdrow.FindControl("lblID");
                    Label lblPBNO = (Label)grdrow.FindControl("lblPBNO");
                    Label lblEMAILID = (Label)grdrow.FindControl("lblEMAILID");
                    hfUpdateID.Value = lblID.Text;
                    hfPBNo.Value = lblPBNO.Text;
                    hfMailID.Value = lblEMAILID.Text;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Approve').modal();", true);

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

        protected void btnInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    Label lblPBNO = (Label)grdrow.FindControl("lblPBNO");
                    string pbno = lblPBNO.Text;
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetPurchaseBillReg("", "", pbno, "", "GETGRNINVOICE");

                    if (dt.Rows.Count > 0)
                    {


                        DataTable dataset1 = new DataTable();
                        DataTable dataset2 = new DataTable();
                        DataTable dataset3 = new DataTable();
                        DataTable dataset4 = new DataTable();
                        dataset1 = objMainClass.PurchaseBillReport(pbno, "MASTER");
                        dataset2 = objMainClass.PurchaseBillReport(pbno, "CHARGES");
                        dataset3 = objMainClass.PurchaseBillReport(pbno, "TAX");
                        dataset4 = objMainClass.PurchaseBillReport(pbno, "TAXSUM");
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;
                        ReportViewer1.LocalReport.ReportPath = "Report/rptPBDoc.rdlc";
                        ReportDataSource rds1 = new ReportDataSource("DataSet1", dataset1);
                        ReportDataSource rds2 = new ReportDataSource("DataSet2", dataset2);
                        ReportDataSource rds3 = new ReportDataSource("DataSet3", dataset3);
                        ReportDataSource rds4 = new ReportDataSource("DataSet4", dataset4);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.DataSources.Add(rds2);
                        ReportViewer1.LocalReport.DataSources.Add(rds3);
                        ReportViewer1.LocalReport.DataSources.Add(rds4);

                        string FileName = "PB_" + pbno + ".pdf";
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



                        using (ZipFile zip = new ZipFile())
                        {
                            zip.AlternateEncodingUsage = ZipOption.AsNecessary;

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                zip.AddEntry(pbno + " - GRN - " + Convert.ToString(dt.Rows[i]["DOCNO"]) + i + Convert.ToString(dt.Rows[i]["EXTENSION"]), (byte[])dt.Rows[i]["INVIMAGE"]);

                                if (Convert.ToString(dt.Rows[i]["POEXTENSION"]) != "0")
                                {
                                    zip.AddEntry(pbno + " - PO - " + Convert.ToString(dt.Rows[i]["PONO"]) + i + Convert.ToString(dt.Rows[i]["POEXTENSION"]), (byte[])dt.Rows[i]["POIMAGE"]);
                                }

                                //zip.AddEntry(pbno + " - " + Convert.ToString(dt.Rows[i]["PONO"]) + " - " + Convert.ToString(dt.Rows[i]["DOCNO"]) + i + ".jpeg", (byte[])dt.Rows[i]["INVIMAGE"]);
                            }
                            zip.AddEntry(FileName, (byte[])mybytes);

                            Response.BufferOutput = false;
                            Response.Clear();
                            Response.Buffer = true; //New Added 07.02.2025
                            //string zipName = String.Format("PB_" + pbno + "_" + Convert.ToString(DateTime.Now).Replace("/", "").Replace("-", "").Replace("_", "") + "_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                            string zipName = String.Format("PB_" + pbno + ".zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                            Response.ContentType = "application/zip";
                            //Response.AddHeader("content-disposition", "attachment; filename=" + zipName); // OLD
                            Response.AddHeader(name: "Content-Disposition", value: "attachment;filename=" + zipName);//New Added 07.02.2025
                            //zip.Save(Response.OutputStream);
                            //Response.End();
                            //HttpContext.Current.ApplicationInstance.CompleteRequest();


                            using (MemoryStream zipStream = new MemoryStream())
                            {
                                zip.Save(zipStream);
                                zipStream.Position = 0;  // Reset stream position before writing
                                zipStream.CopyTo(Response.OutputStream);
                            }

                            Response.Flush();
                            Response.End();

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invoice Image Not Found!');", true);
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

        protected void btnPopReject_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    int iResult = objMainClass.UpdateDOCList(objMainClass.intCmpId, Convert.ToInt32(hfUpdateID.Value), Convert.ToInt32(Session["USERID"]), (int)STATUS.PBRejected, Convert.ToInt32(Session["USERID"]), "RECEIVEPB", txtRejectReason.Text);

                    if (iResult > 0)
                    {
                        if (hfMailID.Value != "" && hfMailID.Value != null && hfMailID.Value != string.Empty)
                        {
                            //CommonEmailSend objEmailSend = new CommonEmailSend();
                            //objEmailSend.CommonEmailSent("customercare@qarmatek.com", "Saz43287", "PB Rejected - " + hfPBNo.Value, hfMailID.Value, "accounts@qarmatek.com", "", "", "", "", "PB No. " + hfPBNo.Value + " rejected by account team. </br></br> Reject Reason : " + txtRejectReason.Text + " . </br></br>", 587, "smtp.office365.com");

                            objMainClass.SaveMailNotification(objMainClass.EmailID, objMainClass.Password, hfMailID.Value, "accounts@qarmatek.com", "PB Rejected - " + hfPBNo.Value, "PB No. " + hfPBNo.Value + " rejected by account team. </br></br> Reject Reason : " + txtRejectReason.Text + " . </br></br>", objMainClass.PORT, hfPBNo.Value, Convert.ToString(Session["USERID"]), "PB");
                        }
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Record save sucessfully.  Record Saved : " + iResult + "\");$('.close').click(function(){window.location.href ='frmAPRVREJPB.aspx' });", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('PB not selected to save. Select atleast one PB.!');", true);
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