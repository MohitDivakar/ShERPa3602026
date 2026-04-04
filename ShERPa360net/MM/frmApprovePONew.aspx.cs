using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.MM
{
    public partial class frmApprovePONew : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();

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
                    DataTable frstRight = objDALUserRights.PO_APPROVE_RIGHTS("PO", Convert.ToString(Session["USERID"]), Convert.ToString(Session["PLANTCD"]), Convert.ToString(Session["DEPTCD"]), 7);
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
                                dt = objMainClass.GetPOforApprove(objMainClass.intCmpId, "", txtPONO.Text, txtFromDate.Text, txtToDate.Text, "", 0, 0, plantcode, maxamt, minamt, "", deptcode);
                            }
                            else
                            {
                                dt = objMainClass.GetPOforApprove(objMainClass.intCmpId, "", txtPONO.Text, txtFromDate.Text, txtToDate.Text, "", 0, aprvseq, plantcode, maxamt, minamt, "SECONDAPRV", deptcode);
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
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorized to view this page');$('.close').click(function(){window.location.href ='MMDashboard.aspx' });", true);
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
                    string attachment = "attachment; filename=POforApproval.xls";
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
                    lblAPRV1.Visible = false;
                    lblAPRV1.Text = string.Empty;

                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

                    string pono = grdrow.Cells[1].Text;
                    string plant = grdrow.Cells[4].Text;
                    string status = grdrow.Cells[7].Text;
                    string seq = grdrow.Cells[10].Text;

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
                        lblRightsMessage.Text = "PO is already " + status + "";
                        lblRightsMessage.Visible = true;
                    }


                    DataTable dtPOMST = new DataTable();
                    DataTable dtPODTL = new DataTable();
                    DataTable dtPOCOND = new DataTable();
                    DataTable dtPOCHG = new DataTable();
                    dtPOMST = objMainClass.SelectPOData(objMainClass.intCmpId, pono, 1);
                    dtPODTL = objMainClass.SelectPOData(objMainClass.intCmpId, pono, 2);
                    dtPOCOND = objMainClass.SelectPOData(objMainClass.intCmpId, pono, 3);
                    dtPOCHG = objMainClass.SelectPOData(objMainClass.intCmpId, pono, 4);
                    if (dtPOMST.Rows.Count > 0)
                    {

                        lblDoctype.Text = Convert.ToString(dtPOMST.Rows[0]["DOCTYPE"]);
                        hfDeptID.Value = Convert.ToString(dtPOMST.Rows[0]["DEPTID"]);
                        hfSeq.Value = seq;
                        hfPlant.Value = plant;
                        hfPODate.Value = Convert.ToDateTime(dtPOMST.Rows[0]["PODTD"]).ToShortDateString();
                        txtInvoiceTo.Text = Convert.ToString(dtPOMST.Rows[0]["CMPNAME"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["CMPADDR1"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["CMPADDR2"]) + " " +
                            Convert.ToString(dtPOMST.Rows[0]["CMPADDR3"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["CMPCITY"]) + " " + Convert.ToString(dtPOMST.Rows[0]["CMPPINCODE"]) + " " + Convert.ToString(dtPOMST.Rows[0]["CMPSTATE"])
                            + System.Environment.NewLine + "" + "GST No. : " + Convert.ToString(dtPOMST.Rows[0]["CMPGST"]);

                        txtDeliveryTo.Text = Convert.ToString(dtPOMST.Rows[0]["DELIVERTO"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["DELIADDR1"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["DELIADDR2"]) + System.Environment.NewLine +
                            Convert.ToString(dtPOMST.Rows[0]["DELIADDR3"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["DELICITY"]) + " " + Convert.ToString(dtPOMST.Rows[0]["DELIPINCODE"]) + " " + Convert.ToString(dtPOMST.Rows[0]["DELISTATE"])
                            + System.Environment.NewLine + "" + "GST No. : " + Convert.ToString(dtPOMST.Rows[0]["DELIGST"]);

                        txtSupplier.Text = Convert.ToString(dtPOMST.Rows[0]["VENDNAME"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["VENDCODE"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["VENDADDR1"]) + System.Environment.NewLine +
                            Convert.ToString(dtPOMST.Rows[0]["VENDADDR2"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["VENDADDR3"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["VENDCITY"]) + " " + Convert.ToString(dtPOMST.Rows[0]["VENDPINCODE"])
                            + " " + Convert.ToString(dtPOMST.Rows[0]["VENDSTATE"]) + System.Environment.NewLine + Convert.ToString(dtPOMST.Rows[0]["VENDCONTACTINFO"]) + System.Environment.NewLine + "" + "GST No. : " + Convert.ToString(dtPOMST.Rows[0]["VENDGSTNO"]);

                        txtPODetail.Text = "Order No. : " + Convert.ToString(dtPOMST.Rows[0]["PONO"]) + System.Environment.NewLine + ""
                                         + "Order Date : " + Convert.ToDateTime(dtPOMST.Rows[0]["PODTD"]).ToShortDateString() + System.Environment.NewLine + ""
                                         + "Terms of Payment : " + Convert.ToString(dtPOMST.Rows[0]["PMTTERMS"]) + " - " + Convert.ToString(dtPOMST.Rows[0]["PMTTERMSDESC"]) + System.Environment.NewLine + ""
                                         + "Net PO Amount : " + Convert.ToString(dtPOMST.Rows[0]["NETPOAMT"]);

                        lblPONo.Text = Convert.ToString(dtPOMST.Rows[0]["PONO"]);
                        lblMaterialAmt.Text = Convert.ToString(dtPOMST.Rows[0]["NETMATVALUE"]);
                        lblTaxAmt.Text = Convert.ToString(dtPOMST.Rows[0]["NETTAXAMT"]);
                        lblDiscountAmt.Text = Convert.ToString(dtPOMST.Rows[0]["DISCOUNT"]);
                        lblOtherChg.Text = Convert.ToString(dtPOMST.Rows[0]["OTHERCHARGES"]);
                        lblPOTotalAmt.Text = Convert.ToString(dtPOMST.Rows[0]["NETPOAMT"]);

                        gvDetail.DataSource = dtPODTL;
                        gvDetail.DataBind();
                        gvDetail.GridLines = GridLines.None;


                        grvTaxation.DataSource = dtPOCOND;
                        grvTaxation.DataBind();

                        grvOtherChg.DataSource = dtPOCHG;
                        grvOtherChg.DataBind();

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
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
                    }

                    if (gvList.Rows.Count > 0)
                    {
                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
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
                    DataTable dtcreate = new DataTable();
                    DataTable dtaprv = new DataTable();
                    dtcreate = objMainClass.MailSenderReceiver("PO", (int)APRVTYPE.REJECT, Convert.ToInt32(hfDeptID.Value), hfPlant.Value, 4, 0, lblPONo.Text, Convert.ToInt32(hfSeq.Value));
                    dtaprv = objMainClass.MailSenderReceiver("PO", (int)APRVTYPE.REJECT, Convert.ToInt32(hfDeptID.Value), hfPlant.Value, 5, 0, lblPONo.Text, Convert.ToInt32(hfSeq.Value));
                    dtcreate.Merge(dtaprv);

                    bool result = objMainClass.ApprovrPO("PO", lblPONo.Text, Convert.ToString(Session["USERID"]), txtApRejDetReason.Text, (int)APRVTYPE.REJECT, Convert.ToInt32(hfSeq.Value));
                    if (result == true)
                    {
                        string message = "PO No. : " + lblPONo.Text + " rejected by " + Convert.ToString(Session["USERNAME"]) + ". Rejection Reason is : " + txtApRejDetReason.Text + "</br></br>";

                        if (dtcreate.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtcreate.Rows.Count; i++)
                            {
                                if (Convert.ToString(dtcreate.Rows[i]["EMAILID"]) != null && Convert.ToString(dtcreate.Rows[i]["EMAILID"]) != string.Empty && Convert.ToString(dtcreate.Rows[i]["EMAILID"]) != "")
                                {
                                    //objMainClass.SaveMailNotification(objMainClass.EmailID, objMainClass.Password, Convert.ToString(dtcreate.Rows[i]["EMAILID"]), "", "PO Rejected", message, objMainClass.PORT, lblPONo.Text, Convert.ToString(Session["USERID"]), "MPO");
                                }
                            }
                        }
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('PO rejected successfully!');$('.close').click(function(){window.location.href ='frmApprovePONew.aspx' });", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('PO not Rejected. Something went wrong. Please try again!');", true);
                    }

                    if (gvList.Rows.Count > 0)
                    {
                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
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
                    DataTable dtcreate = new DataTable();
                    DataTable dtaprv = new DataTable();
                    dtcreate = objMainClass.MailSenderReceiver("PO", (int)APRVTYPE.APPROVED, Convert.ToInt32(hfDeptID.Value), hfPlant.Value, 4, 0, lblPONo.Text, Convert.ToInt32(hfSeq.Value));
                    dtaprv = objMainClass.MailSenderReceiver("PO", (int)APRVTYPE.APPROVED, Convert.ToInt32(hfDeptID.Value), hfPlant.Value, 5, 0, lblPONo.Text, Convert.ToInt32(hfSeq.Value));
                    dtcreate.Merge(dtaprv);

                    bool result = objMainClass.ApprovrPO("PO", lblPONo.Text, Convert.ToString(Session["USERID"]), txtApRejDetReason.Text, (int)APRVTYPE.APPROVED, Convert.ToInt32(hfSeq.Value));
                    if (result == true)
                    {
                        String strCustContent = "";
                        strCustContent = fileread();
                        strCustContent = strCustContent.Replace("###Heading###", "New PO Created by User.");
                        strCustContent = strCustContent.Replace("###CreateBy###", Convert.ToString(Session["USERNAME"]));
                        strCustContent = strCustContent.Replace("###CreateDate###", hfPODate.Value);
                        strCustContent = strCustContent.Replace("###PRNO###", lblPONo.Text);
                        strCustContent = strCustContent.Replace("###Message###", "New PO created by user. Details are as per above.");
                        strCustContent = strCustContent.Replace("###LINK###", "http://14.98.132.190:360/MM/frmApprovePO.aspx");
                        strCustContent = strCustContent.Replace("###HREFLINK###", "http://14.98.132.190:360/MM/frmApprovePO.aspx");

                        string message = "PO No. : " + lblPONo.Text + " approved by " + Convert.ToString(Session["USERNAME"]) + "</br></br>";



                        if (dtcreate.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtcreate.Rows.Count; i++)
                            {
                                if (Convert.ToString(dtcreate.Rows[i]["EMAILID"]) != null && Convert.ToString(dtcreate.Rows[i]["EMAILID"]) != string.Empty && Convert.ToString(dtcreate.Rows[i]["EMAILID"]) != "")
                                {
                                    //objMainClass.SaveMailNotification(objMainClass.EmailID, objMainClass.Password, Convert.ToString(dtcreate.Rows[i]["EMAILID"]), "", "PO Approved", message, objMainClass.PORT, lblPONo.Text, Convert.ToString(Session["USERID"]), "MPO");
                                }
                            }
                        }

                        DataTable dtNextaprv = new DataTable();
                        int nextaprvseq = Convert.ToInt32(hfSeq.Value) + 1;
                        dtNextaprv = objMainClass.MailSenderReceiver("PO", (int)APRVTYPE.APPROVED, Convert.ToInt32(hfDeptID.Value), hfPlant.Value, 6, 0, lblPONo.Text, nextaprvseq);
                        if (dtNextaprv.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtNextaprv.Rows.Count; i++)
                            {
                                //objMainClass.SaveMailNotification(objMainClass.EmailID, objMainClass.Password, Convert.ToString(dtNextaprv.Rows[i]["EMAILID"]), Convert.ToString(dtNextaprv.Rows[i]["EMAILID"]), "New PO Created", strCustContent, objMainClass.PORT, lblPONo.Text, Convert.ToString(Session["USERID"]), "MPO");
                            }
                        }
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('PO Approved successfully!');$('.close').click(function(){window.location.href ='frmApprovePONew.aspx' });", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('PO not approved. Something went wrong. Please try again!');", true);
                    }

                    if (gvList.Rows.Count > 0)
                    {
                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
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
            actualfol = HttpContext.Current.Server.MapPath("~/PRMailBody.html");
            StreamReader sr = new StreamReader(actualfol);
            string strcontent = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            return strcontent;
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    string pono = grdrow.Cells[1].Text;
                    hfPopSeq.Value = grdrow.Cells[10].Text;
                    hfPopPlant.Value = grdrow.Cells[4].Text;
                    //string poID = ((Label)grdrow.FindControl("lblID")).Text;
                    lblPopupPONO.Text = objMainClass.strConvertZeroPadding(pono);
                    //hfPopPOId.Value = poID;
                    string status = grdrow.Cells[7].Text;
                    DataTable dtPOMST = new DataTable();
                    dtPOMST = objMainClass.SelectPOData(objMainClass.intCmpId, pono, 1);
                    hfPopDeptcd.Value = Convert.ToString(dtPOMST.Rows[0]["DEPTID"]);
                    hfPODT.Value = Convert.ToDateTime(dtPOMST.Rows[0]["PODTD"]).ToShortDateString();

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
                        lblRightsMessage.Text = "PO is already " + status + "";
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
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lblOldPONo_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((Button)sender).NamingContainer;
                    Label lblItemCode = (Label)grdrow.FindControl("lblItemCode");

                    DataTable dt = new DataTable();
                    dt = objMainClass.SelectOldPOData(objMainClass.intCmpId, lblPONo.Text, lblItemCode.Text, 5);

                    if (dt.Rows.Count > 0)
                    {

                        gvOldPOData.DataSource = dt;
                        gvOldPOData.DataBind();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$('#modal-oldpo').modal('show');</script>", false);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                    }
                    if (gvList.Rows.Count > 0)
                    {
                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
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
                    DataTable dtcreate = new DataTable();
                    DataTable dtaprv = new DataTable();
                    dtcreate = objMainClass.MailSenderReceiver("PO", (int)APRVTYPE.REJECT, Convert.ToInt32(hfPopDeptcd.Value), hfPopPlant.Value, 4, 0, lblPopupPONO.Text, Convert.ToInt32(hfPopSeq.Value));
                    dtaprv = objMainClass.MailSenderReceiver("PO", (int)APRVTYPE.REJECT, Convert.ToInt32(hfPopDeptcd.Value), hfPopPlant.Value, 5, 0, lblPopupPONO.Text, Convert.ToInt32(hfPopSeq.Value));
                    dtcreate.Merge(dtaprv);

                    bool result = objMainClass.ApprovrPO("PO", lblPopupPONO.Text, Convert.ToString(Session["USERID"]), txtAPREJReason.Text, (int)APRVTYPE.REJECT, Convert.ToInt32(hfPopSeq.Value));

                    if (result == true)
                    {
                        string message = "PO No. : " + lblPopupPONO.Text + " rejected by " + Convert.ToString(Session["USERNAME"]) + ". Rejection Reason is : " + txtAPREJReason.Text + "</br></br>";



                        if (dtcreate.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtcreate.Rows.Count; i++)
                            {
                                if (Convert.ToString(dtcreate.Rows[i]["EMAILID"]) != null && Convert.ToString(dtcreate.Rows[i]["EMAILID"]) != string.Empty && Convert.ToString(dtcreate.Rows[i]["EMAILID"]) != "")
                                {
                                    //objMainClass.SaveMailNotification(objMainClass.EmailID, objMainClass.Password, Convert.ToString(dtcreate.Rows[i]["EMAILID"]), "", "PO Rejected", message, objMainClass.PORT, lblPONo.Text, Convert.ToString(Session["USERID"]), "MPO");
                                }
                            }
                        }
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('PO rejected successfully!');$('.close').click(function(){window.location.href ='frmApprovePONew.aspx' });", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('PO not Rejected. Something went wrong. Please try again!');", true);
                    }

                    if (gvList.Rows.Count > 0)
                    {
                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
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

        protected void lnkPopApprove_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dtcreate = new DataTable();
                    DataTable dtaprv = new DataTable();

                    dtcreate = objMainClass.MailSenderReceiver("PO", (int)APRVTYPE.APPROVED, Convert.ToInt32(hfPopDeptcd.Value), hfPopPlant.Value, 4, 0, lblPopupPONO.Text, Convert.ToInt32(hfPopSeq.Value));
                    dtaprv = objMainClass.MailSenderReceiver("PO", (int)APRVTYPE.APPROVED, Convert.ToInt32(hfPopDeptcd.Value), hfPopPlant.Value, 5, 0, lblPopupPONO.Text, Convert.ToInt32(hfPopSeq.Value));

                    dtcreate.Merge(dtaprv);
                    bool result = objMainClass.ApprovrPO("PO", lblPopupPONO.Text, Convert.ToString(Session["USERID"]), txtAPREJReason.Text, (int)APRVTYPE.APPROVED, Convert.ToInt32(hfPopSeq.Value));
                    if (result == true)
                    {
                        String strCustContent = "";
                        strCustContent = fileread();
                        strCustContent = strCustContent.Replace("###Heading###", "New PO Created by User.");
                        strCustContent = strCustContent.Replace("###CreateBy###", Convert.ToString(Session["USERNAME"]));
                        strCustContent = strCustContent.Replace("###CreateDate###", hfPODT.Value);
                        strCustContent = strCustContent.Replace("###PRNO###", lblPopupPONO.Text);
                        strCustContent = strCustContent.Replace("###Message###", "New PO created by user. Details are as per above.");
                        strCustContent = strCustContent.Replace("###LINK###", "http://14.98.132.190:360/MM/frmApprovePO.aspx");
                        strCustContent = strCustContent.Replace("###HREFLINK###", "http://14.98.132.190:360/MM/frmApprovePO.aspx");

                        string message = "PO No. : " + lblPopupPONO.Text + " approved by " + Convert.ToString(Session["USERNAME"]) + "</br></br>";

                        if (dtcreate.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtcreate.Rows.Count; i++)
                            {
                                if (Convert.ToString(dtcreate.Rows[i]["EMAILID"]) != null && Convert.ToString(dtcreate.Rows[i]["EMAILID"]) != string.Empty && Convert.ToString(dtcreate.Rows[i]["EMAILID"]) != "")
                                {
                                    objMainClass.SaveMailNotification(objMainClass.EmailID, objMainClass.Password, Convert.ToString(dtcreate.Rows[i]["EMAILID"]), "", "PO Approved", message, objMainClass.PORT, lblPopupPONO.Text, Convert.ToString(Session["USERID"]), "MPO");
                                }
                            }
                        }

                        DataTable dtNextaprv = new DataTable();
                        int nextaprvseq = Convert.ToInt32(hfPopSeq.Value) + 1;
                        dtNextaprv = objMainClass.MailSenderReceiver("PO", (int)APRVTYPE.APPROVED, Convert.ToInt32(hfPopDeptcd.Value), hfPopPlant.Value, 6, 0, lblPopupPONO.Text, nextaprvseq);
                        if (dtNextaprv.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtNextaprv.Rows.Count; i++)
                            {
                                //objMainClass.SaveMailNotification(objMainClass.EmailID, objMainClass.Password, Convert.ToString(dtNextaprv.Rows[i]["EMAILID"]), Convert.ToString(dtNextaprv.Rows[i]["EMAILID"]), "New PO Created", strCustContent, objMainClass.PORT, lblPopupPONO.Text, Convert.ToString(Session["USERID"]), "MPO");
                            }
                        }
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('PO Approved successfully!');$('.close').click(function(){window.location.href ='frmApprovePONew.aspx' });", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('PO not approved. Something went wrong. Please try again!');", true);
                    }

                    if (gvList.Rows.Count > 0)
                    {
                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
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