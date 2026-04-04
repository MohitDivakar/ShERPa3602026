using Microsoft.Reporting.WebForms;
using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.Samsung
{
    public partial class frmTCR : System.Web.UI.Page
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
                    imgSaveAll.Enabled = true;
                    if (Session["USERID"] != null)
                    {
                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menutabid.Value, "");
                        if (FormRights.bView == false)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }

                        DataTable dtRcptno = new DataTable();
                        dtRcptno = objMainClass.GetSamsnugTCR("", "", "", "", "", "", 0, "", "MAXRCPTNO");
                        if (dtRcptno.Rows.Count > 0)
                        {



                            txtRcptno.Text = Convert.ToString(Convert.ToInt32(dtRcptno.Rows[0]["RCPTNO"]) + 1);
                            imgSaveAll.Enabled = true;
                        }
                        else
                        {
                            imgSaveAll.Enabled = false;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Receipt number not found. Please try again.');", true);
                            imgSaveAll.Enabled = false;
                        }

                        objBindDDL.FillPaymentMode(ddlPayMode);
                        ddlPayMode.SelectedValue = "9";
                        //txtTransactionID.Text = "CASH COLLECT";

                        //DataTable dt = new DataTable();
                        //dt.Columns.AddRange(new DataColumn[2] { new DataColumn("PartName"), new DataColumn("PartCost") });
                        //ViewState["PartCost"] = dt;

                        //DownloadTCR("8541201236", "27513");

                        if (Request.QueryString.Count > 0)
                        {
                            if (Convert.ToString(Request.QueryString["COMPLAINTNO"]) != null && Convert.ToString(Request.QueryString["COMPLAINTNO"]) != string.Empty && Convert.ToString(Request.QueryString["COMPLAINTNO"]) != "")
                            {
                                Session["COMPLAINTNO"] = Convert.ToString(Request.QueryString["COMPLAINTNO"]);

                            }
                            Response.Redirect(Request.Url.AbsolutePath, false);
                        }
                        else
                        {
                            if (Session["COMPLAINTNO"] != null && Convert.ToString(Session["COMPLAINTNO"]) != "" && Convert.ToString(Session["COMPLAINTNO"]) != string.Empty)
                            {
                                txtComplaintNo.Text = Convert.ToString(Session["COMPLAINTNO"]);
                                txtComplaintNo_TextChanged(1, e);
                                DataTable dtCompData = new DataTable();
                                dtCompData = objMainClass.GetSamsnugTCR("", "", txtComplaintNo.Text, "", "", "", 0, "", "SEARCHCOMPLAINT");
                                if (dtCompData.Rows.Count > 0)
                                {
                                    txtCustName.Text = Convert.ToString(dtCompData.Rows[0]["CUSTNAME"]);
                                    txtAddress.Text = Convert.ToString(dtCompData.Rows[0]["ADDRESS"]);
                                    txtMobileNo.Text = Convert.ToString(dtCompData.Rows[0]["MOBILENO"]);
                                    txtModelNo.Text = Convert.ToString(dtCompData.Rows[0]["MODELNO"]);
                                    txtSerialNo.Text = Convert.ToString(dtCompData.Rows[0]["SERIALNO"]);
                                    txtOthrContact.Text = Convert.ToString(dtCompData.Rows[0]["CONTACTNO"]);
                                    if (Convert.ToInt32(dtCompData.Rows[0]["MOBEXAMC"]) == 1)
                                    {
                                        lblAMC.Text = "Mobex AMC";
                                        lblAMC.Visible = true;
                                    }
                                    else
                                    {
                                        lblAMC.Text = "";
                                        lblAMC.Visible = false;
                                    }

                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Data not found for this Complaint number. Please enter dat manually.');", true);
                                }

                            }
                            Session["COMPLAINTNO"] = null;
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

        protected void imgSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if ((ddlPayMode.SelectedValue == "2" || ddlPayMode.SelectedValue == "5" || ddlPayMode.SelectedValue == "6" || ddlPayMode.SelectedValue == "10" || ddlPayMode.SelectedValue == "11" || ddlPayMode.SelectedValue == "12" ||
                        ddlPayMode.SelectedValue == "14" || ddlPayMode.SelectedValue == "15") && fuImgDoc.HasFiles == false)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please upload image for this payment mode.');", true);
                    }
                    else
                    {
                        if (Convert.ToDecimal(txtTotalCost.Text) > 0)
                        {
                            GridView gvPartList = new GridView();
                            //decimal partcst = PartCost();

                            string IMGTYPE = "";
                            byte[] IMG = null;

                            if (fuImgDoc != null)
                            {
                                if (fuImgDoc.HasFiles)
                                {
                                    IMGTYPE = ".jpeg";
                                    using (BinaryReader br = new BinaryReader(fuImgDoc.PostedFile.InputStream))
                                    {
                                        IMG = br.ReadBytes(fuImgDoc.PostedFile.ContentLength);
                                        IMGTYPE = System.IO.Path.GetExtension(fuImgDoc.FileName);
                                    }
                                }
                            }

                            string rcptno = objMainClass.InsertSamsungTCR(txtRcptno.Text, txtComplaintNo.Text, txtCustName.Text, txtAddress.Text, txtMobileNo.Text, txtOthrContact.Text, 0, 0, "",
                                0, Convert.ToDecimal(txtTotalCost.Text), Convert.ToInt32(ddlPayMode.SelectedValue), txtTransactionID.Text, Convert.ToInt32(Session["USERID"]), "", "", gvPartList, txtEmailID.Text, txtModelNo.Text, txtSerialNo.Text, IMG, IMGTYPE, "", "", "", "", "", 0,
                                chkGST.Checked == true ? 1 : 0, txtGSTNO.Text, txtGSTFirm.Text, "INSERT");

                            if (rcptno != "" && rcptno != string.Empty && rcptno != null)
                            {
                                string URL = DownloadTCR(txtComplaintNo.Text, rcptno);
                                if (URL != null && URL != "" && URL != string.Empty)
                                {
                                    DataTable dtMsgString = new DataTable();
                                    dtMsgString = objMainClass.GetMessageText(objMainClass.intCmpId, objMainClass.intTrigeerId, 209, 0, "1029", "30");
                                    string strMessage = Convert.ToString(dtMsgString.Rows[0]["msgtxt"]);
                                    strMessage = strMessage.Replace("@@AMOUNT", txtTotalCost.Text).Replace("@@TCRLINK", "http://14.98.132.190:360/frmTCRPDF.aspx?TCRNO=" + rcptno);
                                    string FileName = txtComplaintNo.Text + "_" + rcptno + "_TCR.pdf";
                                    string PDFURL = "http://14.98.132.190:360/excel/" + FileName;
                                    //objWAClass.SendMediaFile("Dear Sir/Mam We have received, an Amount of Rs " + Convert.ToDecimal(txtTotalCost.Text) + ". Please check attached Technician Collection Receipt.", "91" + txtMobileNo.Text, Convert.ToString(Session["USERID"]), PDFURL);
                                    objWAClass.SendMessageNewAPI("Dear Sir/Mam We have received, an Amount of Rs " + Convert.ToDecimal(txtTotalCost.Text) + ". Please check attached Technician Collection Receipt.", "91" + txtMobileNo.Text, Convert.ToString(Session["USERID"]), PDFURL);

                                    //objMainClass.SaveNotification(objMainClass.intCmpId, "", "", "91" + txtMobileNo.Text, "", "", strMessage, "", "TCR", rcptno, "SMS", URL, Convert.ToInt32(Session["USERID"]), "INSERTNOTIFICATION");

                                    //objMainClass.SaveNotification(objMainClass.intCmpId, "info@qarmatek.com", "Hof75626", txtEmailID.Text, "", "Samsung Payment Receipt " + txtComplaintNo.Text + "_" + rcptno, "Thank you for making payment to Samsung. Please check attached pdf for your payment receipt.", "587", "TCR", rcptno, "MAIL", URL, Convert.ToInt32(Session["USERID"]), "INSERTNOTIFICATION");

                                    //Thread.Sleep(5000);
                                    String strCustContent = "";
                                    strCustContent = fileread();
                                    strCustContent = strCustContent.Replace("###Heading###", "New TCR Created by Engineer.");
                                    strCustContent = strCustContent.Replace("###CreateBy###", Convert.ToString(Session["USERNAME"]));
                                    strCustContent = strCustContent.Replace("###CreateDate###", DateTime.Now.ToString());
                                    strCustContent = strCustContent.Replace("###ComplaintNo###", txtComplaintNo.Text);
                                    strCustContent = strCustContent.Replace("###TCrNO###", rcptno);
                                    strCustContent = strCustContent.Replace("###Message###", "New TCR created by engineer. Please check attached PDF file for other details.");


                                    String strCustContentCust = "";
                                    strCustContentCust = filereadCust();
                                    strCustContentCust = strCustContentCust.Replace("###Message###", "Thank you for making payment. Please find attached pdf for Payment Receipt.");
                                    strCustContentCust = strCustContentCust.Replace("###ComplaintNo###", txtComplaintNo.Text);
                                    strCustContentCust = strCustContentCust.Replace("###TCrNO###", rcptno);
                                    strCustContentCust = strCustContentCust.Replace("###AMTPAID###", "Rs. " + Convert.ToDecimal(txtTotalCost.Text));
                                    strCustContentCust = strCustContentCust.Replace("###PAIDON###", DateTime.Now.ToString());



                                    string MAILTO = "";
                                    DataTable dtGPData = new DataTable();
                                    dtGPData = objMainClass.GetSamsnugTCR("", "", "", "", "", "", 0, Convert.ToString(Session["GPCODE"]), "GETGPCODEDATA");
                                    if (dtGPData.Rows.Count > 0)
                                    {
                                        MAILTO = Convert.ToString(dtGPData.Rows[0]["EMAILID"]);
                                    }
                                    else
                                    {
                                        MAILTO = "mohit.diwakar@qarmatek.com";
                                    }
                                    //using (System.IO.StreamReader reader = new System.IO.StreamReader(URL))
                                    //{
                                    objMainClass.SendMailWithAttachment(MAILTO, "", "info@qarmatek.com", "Hof75626", 587, "New TCR Created" + txtComplaintNo.Text + "_" + rcptno, strCustContent, URL);

                                    //objMainClass.SendMailWithAttachment(txtEmailID.Text, "", "info@qarmatek.com", "Hof75626", 587, "Samsung Payment Receipt " + txtComplaintNo.Text + "_" + rcptno, strCustContentCust, URL);
                                    //reader.Dispose();
                                    //}
                                    //Thread.Sleep(5000);
                                    File.Delete(URL);
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('TCR saved sucessfully. TCR copy sent to Customer.!');$('.close').click(function(){window.location.href ='frmViewTCR.aspx' });", true);

                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('TCR saved sucessfully. But TCR copy not sent to Customer.!');$('.close').click(function(){window.location.href ='frmViewTCR.aspx' });", true);
                                }

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. TCR not created.!');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You cannot save TCR for 0 Rupee. Please enter some amount for Labour or Part Cost.');", true);
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

        protected static string fileread()
        {
            string actualfol;
            actualfol = HttpContext.Current.Server.MapPath("~/TCRMailBody.html");
            StreamReader sr = new StreamReader(actualfol);
            string strcontent = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            return strcontent;
        }

        protected static string filereadCust()
        {
            string actualfol;
            actualfol = HttpContext.Current.Server.MapPath("~/TCRCustomerMailBody.html");
            StreamReader sr = new StreamReader(actualfol);
            string strcontent = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            return strcontent;
        }

        //protected void txtLabourCost_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Session["USERID"] != null)
        //        {
        //            txtTotalCost.Text = "0";
        //            if (txtLabourCost.Text == null || txtLabourCost.Text == string.Empty || txtLabourCost.Text == "")
        //            {
        //                txtLabourCost.Text = "0";
        //            }
        //            if (txtOtherCost.Text == null || txtOtherCost.Text == string.Empty || txtOtherCost.Text == "")
        //            {
        //                txtOtherCost.Text = "0";
        //            }
        //            //if (txtPartCost.Text == null || txtPartCost.Text == string.Empty || txtPartCost.Text == "")
        //            //{
        //            //    txtPartCost.Text = "0";
        //            //}

        //            decimal totalpartcost = PartCost();
        //            txtTotalCost.Text = Convert.ToString(Convert.ToDecimal(txtLabourCost.Text) + Convert.ToDecimal(txtOtherCost.Text) + totalpartcost);

        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
        //    }
        //}

        protected void txtComplaintNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dtComplaintno = new DataTable();
                    dtComplaintno = objMainClass.GetSamsnugTCR("", "", txtComplaintNo.Text, "", "", "", 0, "", "CHECKCOMPNO");
                    if (dtComplaintno.Rows.Count > 0)
                    {
                        imgSaveAll.Enabled = false;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('TCR alrady generated for this Complaint No.');", true);
                        imgSaveAll.Enabled = false;
                    }
                    else
                    {
                        imgSaveAll.Enabled = true;
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

        protected void ddlPayMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (ddlPayMode.SelectedValue == "12")
                    {
                        //if (Convert.ToString(Session["EMPPLANT"]) == "1001")
                        //{
                        //    div1001.Visible = true;
                        //    div1014.Visible = false;
                        //    div1007.Visible = false;
                        //}
                        //else if (Convert.ToString(Session["EMPPLANT"]) == "1014")
                        //{
                        //    div1001.Visible = false;
                        //    div1014.Visible = true;
                        //    div1007.Visible = false;
                        //}
                        //else if (Convert.ToString(Session["EMPPLANT"]) == "1007")
                        //{
                        //    div1001.Visible = false;
                        //    div1014.Visible = false;
                        //    div1007.Visible = true;
                        //}

                        DataTable dtGPData = new DataTable();
                        dtGPData = objMainClass.GetSamsnugTCR("", "", "", "", "", "", 0, Convert.ToString(Session["GPCODE"]), "GETGPCODEDATA");
                        if (dtGPData.Rows.Count > 0)
                        {
                            imgQRCode.ImageUrl = Convert.ToString(dtGPData.Rows[0]["PAYMENTURL"]);
                        }
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                        rfvImgUpload.Enabled = true;
                        //rfvImgUpload.Visible = true;
                    }
                    else if (ddlPayMode.SelectedValue == "2" || ddlPayMode.SelectedValue == "5" || ddlPayMode.SelectedValue == "6" || ddlPayMode.SelectedValue == "10" || ddlPayMode.SelectedValue == "11" || ddlPayMode.SelectedValue == "12" ||
                        ddlPayMode.SelectedValue == "14" || ddlPayMode.SelectedValue == "15")
                    {
                        rfvImgUpload.Enabled = true;
                        //rfvImgUpload.Visible = true;
                    }
                    else
                    {
                        rfvImgUpload.Enabled = false;
                        //rfvImgUpload.Visible = false;
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

        protected void chkGST_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (chkGST.Checked == true)
                    {
                        divGSTNO.Visible = true;
                        divGSTName.Visible = true;
                        rfvGSTNo.Enabled = true;
                        rfvGSTFirm.Enabled = true;
                    }
                    else
                    {
                        divGSTNO.Visible = false;
                        divGSTName.Visible = false;
                        rfvGSTNo.Enabled = false;
                        rfvGSTFirm.Enabled = false;
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

        //protected void btnAdd_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Session["USERID"] != null)
        //        {
        //            DataTable dt = (DataTable)ViewState["PartCost"];
        //            dt.Rows.Add(txtPartname.Text.Trim(), txtPartCost.Text.Trim());
        //            ViewState["PartCost"] = dt;
        //            gvPartList.DataSource = dt;
        //            gvPartList.DataBind();
        //            txtPartname.Text = string.Empty;
        //            txtPartCost.Text = string.Empty;
        //            txtTotalCost.Text = "0";
        //            decimal totalpartcost = PartCost();
        //            if (txtLabourCost.Text == null || txtLabourCost.Text == string.Empty || txtLabourCost.Text == "")
        //            {
        //                txtLabourCost.Text = "0";
        //            }
        //            if (txtOtherCost.Text == null || txtOtherCost.Text == string.Empty || txtOtherCost.Text == "")
        //            {
        //                txtOtherCost.Text = "0";
        //            }
        //            txtTotalCost.Text = Convert.ToString(Convert.ToDecimal(txtLabourCost.Text) + Convert.ToDecimal(txtOtherCost.Text) + totalpartcost);
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
        //    }
        //}

        //public decimal PartCost()
        //{
        //    decimal totalpartcost = 0;
        //    try
        //    {
        //        if (Session["USERID"] != null)
        //        {
        //            if (gvPartList.Rows.Count > 0)
        //            {
        //                foreach (GridViewRow row in gvPartList.Rows)
        //                {
        //                    if (row.RowType == DataControlRowType.DataRow)
        //                    {
        //                        totalpartcost = totalpartcost + Convert.ToDecimal(row.Cells[1].Text);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                totalpartcost = 0;
        //            }

        //            return totalpartcost;
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
        //            return totalpartcost;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
        //        return totalpartcost;
        //    }
        //}
    }
}