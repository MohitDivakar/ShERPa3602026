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

namespace ShERPa360net.UTILITY
{
    public partial class frmApproveQuotation : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        WAClass objWAClass = new WAClass();
        decimal sumFooterPurchase = 0;
        decimal sumFooterQty = 0;
        decimal sumFooterMRP = 0;
        decimal sumFooteritemrate = 0;
        decimal sumFootercamount = 0;
        decimal sumFooterdiscount = 0;
        decimal sumFootertaxable = 0;
        decimal sumFootertax = 0;


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
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');setTimeout(function () { window.location.href = '../UTILITY/UtilityModuleDashboard.aspx'; }, 2000);", true);
                            return;
                        }
                        GetData();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');setTimeout(function () { window.location.href = '../Login.aspx'; }, 2000);", true);
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
                    DataTable frstRight = objDALUserRights.PO_APPROVE_RIGHTS("QO", Convert.ToString(Session["USERID"]), Convert.ToString(Session["PLANTCD"]), Convert.ToString(Session["DEPTCD"]), 7);
                    if (frstRight.Rows.Count > 0)
                    {
                        DataTable dtMain = new DataTable();
                        for (int i = 0; i < frstRight.Rows.Count; i++)
                        {
                            DataTable dt = new DataTable();
                            string plantcode = Convert.ToString(frstRight.Rows[i]["PLANTCD"]);
                            string deptcode = Convert.ToString(frstRight.Rows[i]["DEPTCD"]);
                            decimal maxamt = Convert.ToDecimal(frstRight.Rows[i]["AMOUNT"]);
                            decimal minamt = Convert.ToDecimal(frstRight.Rows[i]["MINAMT"]);
                            int aprvseq = Convert.ToInt32(frstRight.Rows[i]["APRVSEQ"]);
                            if (Convert.ToInt32(frstRight.Rows[i]["APRVSEQ"]) == 1)
                            {
                                dt = objMainClass.GetQuotforApprove(objMainClass.intCmpId, txtQuotNo.Text, txtFromDate.Text, txtToDate.Text, "", 0, 0, plantcode, maxamt, minamt, "FIRSTAPPROVAL", deptcode);
                            }
                            else
                            {
                                dt = objMainClass.GetQuotforApprove(objMainClass.intCmpId, txtQuotNo.Text, txtFromDate.Text, txtToDate.Text, "", 0, aprvseq, plantcode, maxamt, minamt, "OTHERAPPROVAL", deptcode);
                            }
                            dtMain.Merge(dt);
                        }
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
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorized to view this page');setTimeout(function () { window.location.href = '../UTILITY/UtilityModuleDashboard.aspx'; }, 2000);", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');setTimeout(function () { window.location.href = '../Login.aspx'; }, 2000);", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lnkSearhPO_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GetData();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');setTimeout(function () { window.location.href = '../Login.aspx'; }, 2000);", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lnkExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    string attachment = "attachment; filename=QuotationforApproval.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vdn.ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    gvList.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');setTimeout(function () { window.location.href = '../Login.aspx'; }, 2000);", true);
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
                    lblAPRV1.Visible = false;
                    lblAPRV1.Text = string.Empty;
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    string Quotation = grdrow.Cells[0].Text;
                    string plant = grdrow.Cells[6].Text;
                    string status = grdrow.Cells[2].Text;
                    string seq = grdrow.Cells[7].Text;
                    if (status == "" || status == "&nbsp;")
                    {
                        lnkPopReject.Visible = true;
                        lnkPopApprove.Visible = true;
                        lnkReject.Visible = true;
                        lnkApprove.Visible = true;
                        txtAPREJReason.Visible = true;
                        txtApRejDetReason.Visible = true;
                        lblRightsMessage.Text = string.Empty;
                        lblRightsMessage.Visible = false;
                    }
                    else
                    {
                        lnkPopReject.Visible = false;
                        lnkPopApprove.Visible = false;
                        lnkReject.Visible = false;
                        txtAPREJReason.Visible = false;
                        txtApRejDetReason.Visible = false;
                        lnkApprove.Visible = false;
                        lblRightsMessage.Text = "Quotation is already " + status + "";
                        lblRightsMessage.Visible = true;
                    }

                    DataTable dtPOMST = new DataTable();
                    DataTable dtPODTL = new DataTable();
                    DataTable dtPOCOND = new DataTable();
                    dtPOMST = objMainClass.SelectQuotData(objMainClass.intCmpId, Quotation, 1);
                    dtPODTL = objMainClass.SelectQuotData(objMainClass.intCmpId, Quotation, 2);
                    dtPOCOND = objMainClass.SelectQuotData(objMainClass.intCmpId, Quotation, 3);

                    if (dtPOMST.Rows.Count > 0)
                    {
                        lblDoctype.Text = Convert.ToString(dtPOMST.Rows[0]["DOCTYPE"]);
                        //hfDeptID.Value = Convert.ToString(dtPOMST.Rows[0]["DEPTID"]);
                        hfSeq.Value = seq;
                        hfPlant.Value = plant;
                        hfPODate.Value = Convert.ToDateTime(dtPOMST.Rows[0]["PODTD"]).ToShortDateString();
                        hfMail.Value = Convert.ToString(dtPOMST.Rows[0]["VENDOREMAIL"]);
                        hfMobile.Value = Convert.ToString(dtPOMST.Rows[0]["MOBILENO"]);
                        txtInvoiceTo.Text = Convert.ToString(dtPOMST.Rows[0]["VENDORCODE"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["VENDORNAME"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["VENDOREMAIL"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["VENDADDR1"]) + System.Environment.NewLine +
                            Convert.ToString(dtPOMST.Rows[0]["VENDADDR2"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["VENDADDR3"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["VENDCITY"]) + " " + Convert.ToString(dtPOMST.Rows[0]["VENDPINCODE"])
                            + " " + Convert.ToString(dtPOMST.Rows[0]["VENDSTATE"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["VENDCONTACTINFO"]) + System.Environment.NewLine + "" + "GST No. : " + Convert.ToString(dtPOMST.Rows[0]["VENDGSTNO"]);

                        txtDeliveryTo.Text = Convert.ToString(dtPOMST.Rows[0]["VENDORCODE"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["VENDORNAME"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["VENDOREMAIL"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["VENDADDR1"]) + System.Environment.NewLine +
                            Convert.ToString(dtPOMST.Rows[0]["VENDADDR2"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["VENDADDR3"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["VENDCITY"]) + " " + Convert.ToString(dtPOMST.Rows[0]["VENDPINCODE"])
                            + " " + Convert.ToString(dtPOMST.Rows[0]["VENDSTATE"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["VENDCONTACTINFO"]) + System.Environment.NewLine + "" + "GST No. : " + Convert.ToString(dtPOMST.Rows[0]["VENDGSTNO"]);

                        txtSupplier.Text = Convert.ToString(dtPOMST.Rows[0]["CMPNAME"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["CMPADDR1"]) + System.Environment.NewLine +
                            Convert.ToString(dtPOMST.Rows[0]["CMPADDR2"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["CMPADDR3"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["CMPCITY"]) + " " + Convert.ToString(dtPOMST.Rows[0]["CMPPINCODE"])
                            + " " + Convert.ToString(dtPOMST.Rows[0]["CMPSTATE"]) + System.Environment.NewLine + "" + "GST No. : " + Convert.ToString(dtPOMST.Rows[0]["CMPGST"]);

                        txtPODetail.Text = "Quotation No. : " + Convert.ToString(dtPOMST.Rows[0]["QUOTNO"]) + System.Environment.NewLine + ""
                                         + "Quotation Date : " + Convert.ToDateTime(dtPOMST.Rows[0]["PODTD"]).ToShortDateString() + System.Environment.NewLine + ""
                                         + "Net Amount : " + Convert.ToString(dtPOMST.Rows[0]["QUOTAMT"]);

                        lblPONo.Text = Convert.ToString(dtPOMST.Rows[0]["QUOTNO"]);
                        lblMaterialAmt.Text = Convert.ToString(dtPOMST.Rows[0]["TOTALAMT"]);
                        lblTaxAmt.Text = Convert.ToString(dtPOMST.Rows[0]["TOTALTAXAMT"]);
                        lblDiscountAmt.Text = Convert.ToString(dtPOMST.Rows[0]["TOTALDISCAMT"]);
                        lblDiscountpercentage.Text = Convert.ToString(dtPOMST.Rows[0]["TOTALDISCPER"]);
                        lblOtherChg.Text = Convert.ToString(Convert.ToDecimal(dtPOMST.Rows[0]["TOTALBASEAMT"]) + Convert.ToDecimal(dtPOMST.Rows[0]["TOTALDISCAMT"]));
                        lblPOTotalAmt.Text = Convert.ToString(dtPOMST.Rows[0]["QUOTAMT"]);

                        gvDetail.DataSource = dtPODTL;
                        gvDetail.DataBind();
                        gvDetail.GridLines = GridLines.None;

                        grvTaxation.DataSource = dtPOCOND;
                        grvTaxation.DataBind();


                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);

                        DataTable logDT = new DataTable();
                        logDT = objMainClass.SELECT_REQUISITION_LOG(lblPONo.Text);
                        if (logDT.Rows.Count > 0)
                        {
                            for (int k = 0; k < logDT.Rows.Count; k++)
                            {
                                lblAPRV1.Text = lblAPRV1.Text + " <br/>" + Convert.ToString(logDT.Rows[k]["LISTDESC"]) + "  By  " + Convert.ToString(logDT.Rows[k]["USERNAME"]) + "  On  " + Convert.ToString(logDT.Rows[k]["APRVDATE"]) + " , Reason : " + Convert.ToString(logDT.Rows[k]["REASON"]);
                                lblAPRV1.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        gvDetail.DataSource = string.Empty;
                        gvDetail.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');setTimeout(function() {$('#modal-warning').modal('hide');}, 2000);", true);
                    }

                    if (gvList.Rows.Count > 0)
                    {
                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');setTimeout(function () { window.location.href = '../Login.aspx'; }, 2000);", true);
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
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    string quotation = grdrow.Cells[0].Text;
                    hfPopSeq.Value = grdrow.Cells[7].Text;
                    hfPopPlant.Value = grdrow.Cells[6].Text;
                    lblPopupPONO.Text = objMainClass.strConvertZeroPadding(quotation);
                    string status = grdrow.Cells[2].Text;
                    DataTable dtPOMST = new DataTable();
                    dtPOMST = objMainClass.SelectQuotData(objMainClass.intCmpId, quotation, 1);
                    //hfPopDeptcd.Value = Convert.ToString(dtPOMST.Rows[0]["DEPTID"]);
                    hfPODT.Value = Convert.ToDateTime(dtPOMST.Rows[0]["PODTD"]).ToShortDateString();
                    hfPopMailID.Value = Convert.ToString(dtPOMST.Rows[0]["VENDOREMAIL"]);
                    hfPopMobile.Value = Convert.ToString(dtPOMST.Rows[0]["MOBILENO"]);
                    if (status == "" || status == "&nbsp;")
                    {
                        lnkPopReject.Visible = true;
                        lnkPopApprove.Visible = true;
                        lnkReject.Visible = true;
                        lnkApprove.Visible = true;
                        txtAPREJReason.Visible = true;
                        txtApRejDetReason.Visible = true;
                        lblRightsMessage.Text = string.Empty;
                        lblRightsMessage.Visible = false;
                    }
                    else
                    {
                        lnkPopReject.Visible = false;
                        lnkPopApprove.Visible = false;
                        lnkReject.Visible = false;
                        txtAPREJReason.Visible = false;
                        txtApRejDetReason.Visible = false;
                        lnkApprove.Visible = false;
                        lblRightsMessage.Text = "Quotation is already " + status + "";
                        lblRightsMessage.Visible = true;
                    }

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#mb-aprv').modal();", true);

                    if (gvList.Rows.Count > 0)
                    {
                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');setTimeout(function () { window.location.href = '../Login.aspx'; }, 2000);", true);
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
                    DataTable dtcreate = new DataTable();
                    DataTable dtaprv = new DataTable();
                    dtcreate = objMainClass.MailSenderReceiver("QO", (int)APRVTYPE.REJECT, 0, hfPlant.Value, 7, 0, lblPONo.Text, Convert.ToInt32(hfSeq.Value));
                    dtaprv = objMainClass.MailSenderReceiver("QO", (int)APRVTYPE.REJECT, 0, hfPlant.Value, 8, 0, lblPONo.Text, Convert.ToInt32(hfSeq.Value));
                    dtcreate.Merge(dtaprv);

                    bool result = objMainClass.ApprovrPO("QO", lblPONo.Text, Convert.ToString(Session["USERID"]), txtApRejDetReason.Text, (int)APRVTYPE.REJECT, Convert.ToInt32(hfSeq.Value));

                    if (result == true)
                    {
                        int p = objMainClass.AprvRejQO(objMainClass.intCmpId, lblPONo.Text, (int)APRVTYPE.REJECT, Convert.ToInt32(Session["USERID"]), txtApRejDetReason.Text, "APRVREJQO");

                        string message = "Quotation No. : " + lblPONo.Text + " rejected by " + Convert.ToString(Session["USERNAME"]) + ". Rejection Reason is : " + txtApRejDetReason.Text + "</br></br>";

                        if (dtcreate.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtcreate.Rows.Count; i++)
                            {
                                if (Convert.ToString(dtcreate.Rows[i]["EMAILID"]) != null && Convert.ToString(dtcreate.Rows[i]["EMAILID"]) != string.Empty && Convert.ToString(dtcreate.Rows[i]["EMAILID"]) != "")
                                {
                                    objMainClass.SaveMailNotification(objMainClass.EmailID, objMainClass.Password, Convert.ToString(dtcreate.Rows[i]["EMAILID"]), "", "Quotation Rejected", message, objMainClass.PORT, lblPONo.Text, Convert.ToString(Session["USERID"]), "QO");
                                }
                            }
                        }
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Quotation rejected successfully!');setTimeout(function () { window.location.href = 'frmApproveQuotation.aspx'; }, 2000);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Quotation not Rejected. Something went wrong. Please try again!');setTimeout(function() {$('#modal-warning').modal('hide');}, 2000);", true);
                    }

                    if (gvList.Rows.Count > 0)
                    {
                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');setTimeout(function () { window.location.href = '../Login.aspx'; }, 2000);", true);
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
                    DataTable dtcreate = new DataTable();
                    DataTable dtaprv = new DataTable();
                    dtcreate = objMainClass.MailSenderReceiver("QO", (int)APRVTYPE.APPROVED, 0, hfPlant.Value, 7, 0, lblPONo.Text, Convert.ToInt32(hfSeq.Value));
                    dtaprv = objMainClass.MailSenderReceiver("QO", (int)APRVTYPE.APPROVED, 0, hfPlant.Value, 8, 0, lblPONo.Text, Convert.ToInt32(hfSeq.Value));
                    dtcreate.Merge(dtaprv);

                    bool result = objMainClass.ApprovrPO("QO", lblPONo.Text, Convert.ToString(Session["USERID"]), txtApRejDetReason.Text, (int)APRVTYPE.APPROVED, Convert.ToInt32(hfSeq.Value));
                    if (result == true)
                    {

                        int p = objMainClass.AprvRejQO(objMainClass.intCmpId, lblPONo.Text, (int)APRVTYPE.APPROVED, Convert.ToInt32(Session["USERID"]), txtApRejDetReason.Text, "APRVREJQO");


                        String strCustContent = "";
                        strCustContent = fileread();
                        strCustContent = strCustContent.Replace("###Heading###", "New Quotation Created by User.");
                        strCustContent = strCustContent.Replace("###CreateBy###", Convert.ToString(Session["USERNAME"]));
                        strCustContent = strCustContent.Replace("###CreateDate###", hfPODate.Value);
                        strCustContent = strCustContent.Replace("###PRNO###", lblPONo.Text);
                        strCustContent = strCustContent.Replace("###Message###", "New Quotation created by user. Details are as per above.");
                        strCustContent = strCustContent.Replace("###LINK###", "http://14.98.132.190:360/UTILITY/frmApproveQuotation.aspx");
                        strCustContent = strCustContent.Replace("###HREFLINK###", "http://14.98.132.190:360/UTILITY/frmApproveQuotation.aspx");

                        string message = "Quotation No. : " + lblPONo.Text + " approved by " + Convert.ToString(Session["USERNAME"]) + "</br></br>";

                        if (dtcreate.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtcreate.Rows.Count; i++)
                            {
                                if (Convert.ToString(dtcreate.Rows[i]["EMAILID"]) != null && Convert.ToString(dtcreate.Rows[i]["EMAILID"]) != string.Empty && Convert.ToString(dtcreate.Rows[i]["EMAILID"]) != "")
                                {
                                    objMainClass.SaveMailNotification(objMainClass.EmailID, objMainClass.Password, Convert.ToString(dtcreate.Rows[i]["EMAILID"]), "", "Quotation Approved", message, objMainClass.PORT, lblPONo.Text, Convert.ToString(Session["USERID"]), "QO");
                                }
                            }
                        }

                        DataTable dtNextaprv = new DataTable();
                        int nextaprvseq = Convert.ToInt32(hfSeq.Value) + 1;
                        dtNextaprv = objMainClass.MailSenderReceiver("QO", (int)APRVTYPE.APPROVED, 0, hfPlant.Value, 9, 0, lblPONo.Text, nextaprvseq);
                        if (dtNextaprv.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtNextaprv.Rows.Count; i++)
                            {
                                objMainClass.SaveMailNotification(objMainClass.EmailID, objMainClass.Password, Convert.ToString(dtNextaprv.Rows[i]["EMAILID"]), Convert.ToString(dtNextaprv.Rows[i]["EMAILID"]), "New Quotation Created #" + lblPONo.Text, strCustContent, objMainClass.PORT, lblPONo.Text, Convert.ToString(Session["USERID"]), "QO");
                            }
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Quotation Approved successfully!');setTimeout(function () { window.location.href = 'frmApproveQuotation.aspx'; }, 2000);", true);
                        }
                        else
                        {
                            string URL = DownloadTCR(lblPONo.Text);
                            if (URL != null && URL != "" && URL != string.Empty)
                            {
                                string FileName = lblPONo.Text + "_Quotation.pdf";
                                string PDFURL = "http://14.98.132.190:360/excel/" + FileName;

                                string customermail = hfMail.Value;
                                string customermobile = hfMobile.Value;
                                String strCustContentCust = "";

                                strCustContentCust = filereadCust();
                                strCustContentCust = strCustContentCust.Replace("###Message###", "New Quotation received. Please find attached pdf for more information.");
                                strCustContentCust = strCustContentCust.Replace("###ComplaintNo###", lblPONo.Text);

                                //objWAClass.SendMediaFile("Please check attached Quotation.  - Mobex", "91" + customermobile, Convert.ToString(Session["USERID"]), PDFURL);
                                objWAClass.SendMessageNewAPI("Please check attached Quotation.  - Mobex", "91" + customermobile, Convert.ToString(Session["USERID"]), PDFURL);

                                DataTable dtcreate1 = new DataTable();
                                DataTable dtaprv1 = new DataTable();

                                dtcreate1 = objMainClass.MailSenderReceiver("QO", (int)APRVTYPE.APPROVED, 0, hfPopPlant.Value, 10, 0, lblPONo.Text, Convert.ToInt32(hfSeq.Value));
                                dtaprv1 = objMainClass.MailSenderReceiver("QO", (int)APRVTYPE.APPROVED, 0, hfPopPlant.Value, 11, 0, lblPONo.Text, Convert.ToInt32(hfSeq.Value));
                                dtcreate1.Merge(dtaprv1);

                                if (dtcreate1.Rows.Count > 0)
                                {
                                    for (int i = 0; i < dtcreate1.Rows.Count; i++)
                                    {
                                        if (Convert.ToString(dtcreate1.Rows[i]["MOBILNO"]) != null && Convert.ToString(dtcreate1.Rows[i]["MOBILNO"]) != string.Empty && Convert.ToString(dtcreate1.Rows[i]["MOBILNO"]) != "")
                                        {
                                            //objWAClass.SendMediaFile("Please check attached Quotation.  - Mobex", "91" + Convert.ToString(dtcreate1.Rows[i]["MOBILNO"]), Convert.ToString(Session["USERID"]), PDFURL);
                                            objWAClass.SendMessageNewAPI("Please check attached Quotation.  - Mobex", "91" + Convert.ToString(dtcreate1.Rows[i]["MOBILNO"]), Convert.ToString(Session["USERID"]), PDFURL);
                                        }
                                    }
                                }

                                //objMainClass.SendMailWithAttachment(customermail, "mohit.diwakar@qarmatek.com;dhaval.vakta@qarmatek.com", "info@qarmatek.com", "Hof75626", 587, "New Quotation Received #" + lblPONo.Text, strCustContentCust, URL);
                                File.Delete(URL);

                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Quotation Approved successfully!');setTimeout(function () { window.location.href = 'frmApproveQuotation.aspx'; }, 2000);", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Quotation Approved sucessfully. But Quotation copy not sent to Customer.!');setTimeout(function () { window.location.href = 'frmApproveQuotation.aspx'; }, 2000);", true);
                            }

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Quotation not approved. Something went wrong. Please try again!');setTimeout(function() {$('#modal-warning').modal('hide');}, 2000);", true);
                    }

                    if (gvList.Rows.Count > 0)
                    {
                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');setTimeout(function () { window.location.href = '../Login.aspx'; }, 2000);", true);
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
                    DataTable dtcreate = new DataTable();
                    DataTable dtaprv = new DataTable();
                    dtcreate = objMainClass.MailSenderReceiver("QO", (int)APRVTYPE.REJECT, 0, hfPlant.Value, 7, 0, lblPopupPONO.Text, Convert.ToInt32(hfPopSeq.Value));
                    dtaprv = objMainClass.MailSenderReceiver("QO", (int)APRVTYPE.REJECT, 0, hfPlant.Value, 8, 0, lblPopupPONO.Text, Convert.ToInt32(hfPopSeq.Value));
                    dtcreate.Merge(dtaprv);


                    bool result = objMainClass.ApprovrPO("QO", lblPopupPONO.Text, Convert.ToString(Session["USERID"]), txtAPREJReason.Text, (int)APRVTYPE.REJECT, Convert.ToInt32(hfPopSeq.Value));

                    if (result == true)
                    {

                        int p = objMainClass.AprvRejQO(objMainClass.intCmpId, lblPopupPONO.Text, (int)APRVTYPE.REJECT, Convert.ToInt32(Session["USERID"]), txtAPREJReason.Text, "APRVREJQO");

                        string message = "Quotation No. : " + lblPopupPONO.Text + " rejected by " + Convert.ToString(Session["USERNAME"]) + ". Rejection Reason is : " + txtAPREJReason.Text + "</br></br>";

                        if (dtcreate.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtcreate.Rows.Count; i++)
                            {
                                if (Convert.ToString(dtcreate.Rows[i]["EMAILID"]) != null && Convert.ToString(dtcreate.Rows[i]["EMAILID"]) != string.Empty && Convert.ToString(dtcreate.Rows[i]["EMAILID"]) != "")
                                {
                                    objMainClass.SaveMailNotification(objMainClass.EmailID, objMainClass.Password, Convert.ToString(dtcreate.Rows[i]["EMAILID"]), "", "Quotation Rejected", message, objMainClass.PORT, lblPopupPONO.Text, Convert.ToString(Session["USERID"]), "QO");
                                }
                            }
                        }
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Quotation rejected successfully!');setTimeout(function () { window.location.href = 'frmApproveQuotation.aspx'; }, 2000);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Quotation not Rejected. Something went wrong. Please try again!');setTimeout(function() {$('#modal-warning').modal('hide');}, 2000);", true);
                    }

                    if (gvList.Rows.Count > 0)
                    {
                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');setTimeout(function () { window.location.href = '../Login.aspx'; }, 2000);", true);
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
                    DataTable dtcreate = new DataTable();
                    DataTable dtaprv = new DataTable();
                    dtcreate = objMainClass.MailSenderReceiver("QO", (int)APRVTYPE.APPROVED, 0, hfPlant.Value, 7, 0, lblPopupPONO.Text, Convert.ToInt32(hfPopSeq.Value));
                    dtaprv = objMainClass.MailSenderReceiver("QO", (int)APRVTYPE.APPROVED, 0, hfPlant.Value, 8, 0, lblPopupPONO.Text, Convert.ToInt32(hfPopSeq.Value));
                    dtcreate.Merge(dtaprv);


                    bool result = objMainClass.ApprovrPO("QO", lblPopupPONO.Text, Convert.ToString(Session["USERID"]), txtAPREJReason.Text, (int)APRVTYPE.APPROVED, Convert.ToInt32(hfPopSeq.Value));
                    if (result == true)
                    {

                        int p = objMainClass.AprvRejQO(objMainClass.intCmpId, lblPopupPONO.Text, (int)APRVTYPE.APPROVED, Convert.ToInt32(Session["USERID"]), txtApRejDetReason.Text, "APRVREJQO");

                        String strCustContent = "";
                        strCustContent = fileread();
                        strCustContent = strCustContent.Replace("###Heading###", "New Quotation Created by User.");
                        strCustContent = strCustContent.Replace("###CreateBy###", Convert.ToString(Session["USERNAME"]));
                        strCustContent = strCustContent.Replace("###CreateDate###", hfPODT.Value);
                        strCustContent = strCustContent.Replace("###PRNO###", lblPopupPONO.Text);
                        strCustContent = strCustContent.Replace("###Message###", "New Quotation created by user. Details are as per above.");
                        strCustContent = strCustContent.Replace("###LINK###", "http://14.98.132.190:360/UTILITY/frmApproveQuotation.aspx");
                        strCustContent = strCustContent.Replace("###HREFLINK###", "http://14.98.132.190:360/UTILITY/frmApproveQuotation.aspx");

                        string message = "Quotation No. : " + lblPopupPONO.Text + " approved by " + Convert.ToString(Session["USERNAME"]) + "</br></br>";

                        if (dtcreate.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtcreate.Rows.Count; i++)
                            {
                                if (Convert.ToString(dtcreate.Rows[i]["EMAILID"]) != null && Convert.ToString(dtcreate.Rows[i]["EMAILID"]) != string.Empty && Convert.ToString(dtcreate.Rows[i]["EMAILID"]) != "")
                                {
                                    objMainClass.SaveMailNotification(objMainClass.EmailID, objMainClass.Password, Convert.ToString(dtcreate.Rows[i]["EMAILID"]), "", "Quotation Approved", message, objMainClass.PORT, lblPopupPONO.Text, Convert.ToString(Session["USERID"]), "QO");
                                }
                            }
                        }

                        DataTable dtNextaprv = new DataTable();
                        int nextaprvseq = Convert.ToInt32(hfPopSeq.Value) + 1;
                        dtNextaprv = objMainClass.MailSenderReceiver("QO", (int)APRVTYPE.APPROVED, 0, hfPopPlant.Value, 9, 0, lblPopupPONO.Text, nextaprvseq);
                        if (dtNextaprv.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtNextaprv.Rows.Count; i++)
                            {
                                objMainClass.SaveMailNotification(objMainClass.EmailID, objMainClass.Password, Convert.ToString(dtNextaprv.Rows[i]["EMAILID"]), Convert.ToString(dtNextaprv.Rows[i]["EMAILID"]), "New Quotation Created", strCustContent, objMainClass.PORT, lblPopupPONO.Text, Convert.ToString(Session["USERID"]), "QO");
                            }

                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Quotation Approved successfully!');setTimeout(function () { window.location.href = 'frmApproveQuotation.aspx'; }, 2000);", true);
                        }
                        else
                        {
                            string URL = DownloadTCR(lblPopupPONO.Text);
                            if (URL != null && URL != "" && URL != string.Empty)
                            {
                                string FileName = lblPopupPONO.Text + "_Quotation.pdf";
                                string PDFURL = "http://14.98.132.190:360/excel/" + FileName;

                                string customermail = hfPopMailID.Value;
                                string customermobile = hfPopMobile.Value;
                                String strCustContentCust = "";

                                strCustContentCust = filereadCust();
                                strCustContentCust = strCustContentCust.Replace("###Message###", "New Quotation received. Please find attached pdf for more information.");
                                strCustContentCust = strCustContentCust.Replace("###ComplaintNo###", lblPopupPONO.Text);

                                //objWAClass.SendMediaFile("Please check attached Quotation.  - Mobex", "91" + customermobile, Convert.ToString(Session["USERID"]), PDFURL);
                                objWAClass.SendMessageNewAPI("Please check attached Quotation.  - Mobex", "91" + customermobile, Convert.ToString(Session["USERID"]), PDFURL);

                                DataTable dtcreate1 = new DataTable();
                                DataTable dtaprv1 = new DataTable();

                                dtcreate1 = objMainClass.MailSenderReceiver("QO", (int)APRVTYPE.APPROVED, 0, hfPopPlant.Value, 10, 0, lblPopupPONO.Text, Convert.ToInt32(hfPopSeq.Value));
                                dtaprv1 = objMainClass.MailSenderReceiver("QO", (int)APRVTYPE.APPROVED, 0, hfPopPlant.Value, 11, 0, lblPopupPONO.Text, Convert.ToInt32(hfPopSeq.Value));
                                dtcreate1.Merge(dtaprv1);

                                if (dtcreate1.Rows.Count > 0)
                                {
                                    for (int i = 0; i < dtcreate1.Rows.Count; i++)
                                    {
                                        if (Convert.ToString(dtcreate1.Rows[i]["MOBILNO"]) != null && Convert.ToString(dtcreate1.Rows[i]["MOBILNO"]) != string.Empty && Convert.ToString(dtcreate1.Rows[i]["MOBILNO"]) != "")
                                        {
                                            //objWAClass.SendMediaFile("Please check attached Quotation.  - Mobex", "91" + Convert.ToString(dtcreate1.Rows[i]["MOBILNO"]), Convert.ToString(Session["USERID"]), PDFURL);
                                            objWAClass.SendMessageNewAPI("Please check attached Quotation.  - Mobex", "91" + Convert.ToString(dtcreate1.Rows[i]["MOBILNO"]), Convert.ToString(Session["USERID"]), PDFURL);
                                        }
                                    }
                                }



                                //objMainClass.SendMailWithAttachment(customermail, "mohit.diwakar@qarmatek.com;dhaval.vakta@qarmatek.com", "info@qarmatek.com", "Hof75626", 587, "New Quotation Received #" + lblPopupPONO.Text, strCustContentCust, URL);
                                File.Delete(URL);

                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Quotation Approved successfully!');setTimeout(function () { window.location.href = 'frmApproveQuotation.aspx'; }, 2000);", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Quotation Approved sucessfully. But Quotation copy not sent to Customer.!');setTimeout(function () { window.location.href = 'frmApproveQuotation.aspx'; }, 2000);", true);
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Quotation not approved. Something went wrong. Please try again!');setTimeout(function() {$('#modal-warning').modal('hide');}, 2000);", true);
                    }

                    if (gvList.Rows.Count > 0)
                    {
                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');setTimeout(function () { window.location.href = '../Login.aspx'; }, 2000);", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected static string filereadCust()
        {
            string actualfol;
            actualfol = HttpContext.Current.Server.MapPath("~/QuotationMailBody.html");
            StreamReader sr = new StreamReader(actualfol);
            string strcontent = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            return strcontent;
        }

        public string DownloadTCR(string Quotation)
        {
            string url = "";
            DataTable dtPOMST = new DataTable();
            DataTable dtPODTL = new DataTable();
            DataTable dtPOAPRV = new DataTable();
            DataTable dtPOTAX = new DataTable();
            dtPOMST = objMainClass.SelectQuotData(objMainClass.intCmpId, Quotation, 1);
            dtPODTL = objMainClass.SelectQuotData(objMainClass.intCmpId, Quotation, 2);
            dtPOAPRV = objMainClass.SelectQuotData(objMainClass.intCmpId, Quotation, 4);
            dtPOTAX = objMainClass.SelectQuotData(objMainClass.intCmpId, Quotation, 5);

            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = "Report/rptQuotation.rdlc";
            ReportDataSource rds = new ReportDataSource("DataSet1", dtPOMST);
            ReportDataSource rds1 = new ReportDataSource("DataSet2", dtPODTL);
            ReportDataSource rds2 = new ReportDataSource("DataSet3", dtPOAPRV);
            ReportDataSource rds3 = new ReportDataSource("DataSet4", dtPOTAX);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.DataSources.Add(rds1);
            ReportViewer1.LocalReport.DataSources.Add(rds2);
            ReportViewer1.LocalReport.DataSources.Add(rds3);

            string FileName = Quotation + "_Quotation.pdf";
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

            string folderpath = "~/excel/";
            string filePath = Path.Combine(Server.MapPath(folderpath), FileName);

            string localPath = (Server.MapPath("../img/" + FileName));
            System.IO.File.WriteAllBytes(filePath, mybytes);
            url = filePath;

            return url;
        }

        protected static string fileread()
        {
            string actualfol;
            actualfol = HttpContext.Current.Server.MapPath("~/PRMailBody.html");
            StreamReader sr = new StreamReader(actualfol);
            string strcontent = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            return strcontent;
        }

        protected void lnkQuotEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    string page = "frmQuotationEdit.aspx?QUOTNO=" + lblPONo.Text;

                    Response.Redirect(page, true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');setTimeout(function () { window.location.href = '../Login.aspx'; }, 2000);", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
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
                        string lblPURCHASERATE = ((Label)e.Row.FindControl("lblPURCHASERATE")).Text;
                        string lblMRP = ((Label)e.Row.FindControl("lblMRP")).Text;
                        string lblPOQTY = ((Label)e.Row.FindControl("lblPOQTY")).Text;
                        string lblITEMBRATE = ((Label)e.Row.FindControl("lblITEMBRATE")).Text;
                        string lblCAMOUNT = ((Label)e.Row.FindControl("lblCAMOUNT")).Text;
                        string lblDISCOUNTAMT = ((Label)e.Row.FindControl("lblDISCOUNTAMT")).Text;
                        string lblTAXABLE = ((Label)e.Row.FindControl("lblTAXABLE")).Text;
                        string lblTAXAMT = ((Label)e.Row.FindControl("lblTAXAMT")).Text;

                        sumFooterPurchase += Convert.ToDecimal(lblPURCHASERATE);
                        sumFooterQty += Convert.ToDecimal(lblPOQTY);
                        sumFooterMRP += Convert.ToDecimal(lblMRP);
                        sumFooteritemrate += Convert.ToDecimal(lblITEMBRATE);
                        sumFootercamount += Convert.ToDecimal(lblCAMOUNT);
                        sumFooterdiscount += Convert.ToDecimal(lblDISCOUNTAMT);
                        sumFootertaxable += Convert.ToDecimal(lblTAXABLE);
                        sumFootertax += Convert.ToDecimal(lblTAXAMT);

                    }
                    if (e.Row.RowType == DataControlRowType.Footer)
                    {
                        Label lblPURCHASERATEFooter = (Label)e.Row.FindControl("lblPURCHASERATEFooter");
                        Label lblTotallblMRP = (Label)e.Row.FindControl("lblTotallblMRP");
                        Label lblTotalPOQTY = (Label)e.Row.FindControl("lblTotalPOQTY");
                        Label lblTotalITEMBRATE = (Label)e.Row.FindControl("lblTotalITEMBRATE");
                        Label lblTotalCAMOUNT = (Label)e.Row.FindControl("lblTotalCAMOUNT");
                        Label lblTotalDISCOUNTAMT = (Label)e.Row.FindControl("lblTotalDISCOUNTAMT");
                        Label lblTotalTAXABLE = (Label)e.Row.FindControl("lblTotalTAXABLE");
                        Label lblTotalTAXAMT = (Label)e.Row.FindControl("lblTotalTAXAMT");

                        lblPURCHASERATEFooter.Text = Convert.ToString(sumFooterPurchase);
                        lblTotallblMRP.Text = Convert.ToString(sumFooterMRP);
                        lblTotalPOQTY.Text = Convert.ToString(sumFooterQty);
                        lblTotalITEMBRATE.Text = Convert.ToString(sumFooteritemrate);
                        lblTotalCAMOUNT.Text = Convert.ToString(sumFootercamount);
                        lblTotalDISCOUNTAMT.Text = Convert.ToString(sumFooterdiscount);
                        lblTotalTAXABLE.Text = Convert.ToString(sumFootertaxable);
                        lblTotalTAXAMT.Text = Convert.ToString(sumFootertax);

                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');setTimeout(function () { window.location.href = '../Login.aspx'; }, 2000);", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}