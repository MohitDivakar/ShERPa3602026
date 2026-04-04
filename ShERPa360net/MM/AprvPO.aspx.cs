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
    public partial class AprvPO : System.Web.UI.Page
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
                        if (FormRights.bView == false) //if (objDALUserRights.bView == false)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        DataTable frstRight = objDALUserRights.PO_APPROVE_RIGHTS("PO", Convert.ToString(Session["USERID"]), Convert.ToString(Session["PLANTCD"]), Convert.ToString(Session["DEPTCD"]), 7);
                        //SP_SELECT_PO_FOR_APPROVE

                        if (frstRight.Rows.Count > 0)
                        {
                            DataTable dt = new DataTable();
                            //dt = objMainClass.GetPOforApprove(objMainClass.intCmpId, "", txtPONO.Text, txtFromDate.Text, txtToDate.Text, "", 0, 0, null, Convert.ToDecimal(frstRight.Rows[0]["AMOUNT"]), Convert.ToDecimal(frstRight.Rows[0]["MINAMT"]));
                            dt = objMainClass.GetPOforApprove(objMainClass.intCmpId, "", txtPONO.Text, txtFromDate.Text, txtToDate.Text, "", 0, 0, null, 0, 0, "NEWAPPROVESYS");
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
                                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
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
            else 
            {
                if(gvList.Rows.Count > 0)
                {
                    gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected void lnkSearhPR_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable frstRight = objDALUserRights.PO_APPROVE_RIGHTS("PO", Convert.ToString(Session["USERID"]), Convert.ToString(Session["PLANTCD"]), Convert.ToString(Session["DEPTCD"]), 7);

                if (frstRight.Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetPOforApprove(objMainClass.intCmpId, "", txtPONO.Text, txtFromDate.Text, txtToDate.Text, "", 0, 0, null, 0, 0, "NEWAPPROVESYS");
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
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorized to view this page');$('.close').click(function(){window.location.href ='MMDashboard.aspx' });", true);
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

                lblAPRV1.Visible = false;
                lblAPRV1.Text = string.Empty;

                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

                string pono = grdrow.Cells[1].Text;
                string status = grdrow.Cells[6].Text;

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

                    lblDoctype.Text = Convert.ToString(dtPOMST.Rows[0]["POTYPE"]);
                    lblPODate.Text = Convert.ToDateTime(dtPOMST.Rows[0]["PODTD"]).ToShortDateString();
                    lblPONo.Text = Convert.ToString(dtPOMST.Rows[0]["PONO"]);
                    lblRemark.Text = Convert.ToString(dtPOMST.Rows[0]["REMARK"]);

                    lblPOVendor.Text = Convert.ToString(dtPOMST.Rows[0]["VENDNAME"]);
                    lblPOTransporter.Text = Convert.ToString(dtPOMST.Rows[0]["TRANNAME"]);
                    lblPOPayTerms.Text = Convert.ToString(dtPOMST.Rows[0]["PMTTERMS"]);
                    lblPOPayTermsDesc.Text = Convert.ToString(dtPOMST.Rows[0]["PMTTERMSDESC"]);

                    lblNetPOAmt.Text = Convert.ToString(dtPOMST.Rows[0]["NETPOAMT"]);
                    lblMaterialAmt.Text = Convert.ToString(dtPOMST.Rows[0]["NETMATVALUE"]);
                    lblTaxAmt.Text = Convert.ToString(dtPOMST.Rows[0]["NETTAXAMT"]);
                    lblDiscountAmt.Text = Convert.ToString(dtPOMST.Rows[0]["DISCOUNT"]);
                    lblOtherChg.Text = Convert.ToString(dtPOMST.Rows[0]["OTHERCHARGES"]);


                    gvDetail.DataSource = dtPODTL;
                    gvDetail.DataBind();

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

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            string pono = grdrow.Cells[7].Text;
            string poID = ((Label)grdrow.FindControl("lblID")).Text;
            lblPopupPONO.Text = pono;
            hfPopPOId.Value = poID;
            //string status = grdrow.Cells[6].Text;

            //if (status == "" || status == "&nbsp;")
            //{
            //    lnkPopReject.Visible = true;
            //    lnkPopApprove.Visible = true;
            //    lnkReject.Visible = true;
            //    lnkApprove.Visible = true;
            //    txtAPREJReason.Visible = true;
            //    txtApRejDetReason.Visible = true;
            //    lblRightsMessage.Text = string.Empty;
            //    lblRightsMessage.Visible = false;


            //}
            //else
            //{
            //    lnkPopReject.Visible = false;
            //    lnkPopApprove.Visible = false;
            //    lnkReject.Visible = false;
            //    txtAPREJReason.Visible = false;
            //    txtApRejDetReason.Visible = false;
            //    lnkApprove.Visible = false;
            //    lblRightsMessage.Text = "PO is already " + status + "";
            //    lblRightsMessage.Visible = true;
            //}


            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#mb-aprv').modal();", true);
        }

        protected void lnkReject_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable frstRight = objDALUserRights.PO_APPROVE_RIGHTS("PO", Convert.ToString(Session["USERID"]), Convert.ToString(Session["PLANTCD"]), Convert.ToString(Session["DEPTCD"]), 7);
                //bool result = objMainClass.ApprovrPO(lblPONo.Text, (int)APRVTYPE.REJECT, "PR", Convert.ToString(Session["USERID"]), txtApRejDetReason.Text, Convert.ToInt32(frstRight.Rows[0]["APRVSEQ"]));
                bool result = objMainClass.ApprovrPO("PO", lblPONo.Text, Convert.ToString(Session["USERID"]), txtApRejDetReason.Text, (int)APRVTYPE.REJECT, Convert.ToInt32(frstRight.Rows[0]["APRVSEQ"]));
                if (result == true)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('PO rejected successfully!');$('.close').click(function(){window.location.href ='AprvPO.aspx' });", true);
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

        protected void lnkApprove_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable frstRight = objDALUserRights.PO_APPROVE_RIGHTS("PO", Convert.ToString(Session["USERID"]), Convert.ToString(Session["PLANTCD"]), Convert.ToString(Session["DEPTCD"]), 7);
                bool result = objMainClass.ApprovrPO("PO", lblPONo.Text, Convert.ToString(Session["USERID"]), txtApRejDetReason.Text, (int)APRVTYPE.APPROVED, Convert.ToInt32(frstRight.Rows[0]["APRVSEQ"]));
                if (result == true)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('PO Approved successfully!');$('.close').click(function(){window.location.href ='AprvPO.aspx' });", true);
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

        protected void lnkPopReject_Click(object sender, EventArgs e)
        {
            try
            {
                //DataTable frstRight = objDALUserRights.PO_APPROVE_RIGHTS("PO", Convert.ToString(Session["USERID"]), Convert.ToString(Session["PLANTCD"]), Convert.ToString(Session["DEPTCD"]), 7);
                //bool result = objMainClass.ApprovrPO("PO", lblPopupPONO.Text, Convert.ToString(Session["USERID"]), txtAPREJReason.Text, (int)APRVTYPE.REJECT, Convert.ToInt32(frstRight.Rows[0]["APRVSEQ"]));
                bool result = objMainClass.ApprovePO(objMainClass.intCmpId, Convert.ToInt32(hfPopPOId.Value), txtAPREJReason.Text, (int)APRVTYPE.REJECT, Convert.ToInt32(Session["USERID"]), "APPROVEPO");
                if (result == true)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('PO rejected successfully!');$('.close').click(function(){window.location.href ='AprvPO.aspx' });", true);
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
                //DataTable frstRight = objDALUserRights.PO_APPROVE_RIGHTS("PO", Convert.ToString(Session["USERID"]), Convert.ToString(Session["PLANTCD"]), Convert.ToString(Session["DEPTCD"]), 7);
                //bool result = objMainClass.ApprovrPO("PO", lblPopupPONO.Text, Convert.ToString(Session["USERID"]), txtAPREJReason.Text, (int)APRVTYPE.APPROVED, Convert.ToInt32(frstRight.Rows[0]["APRVSEQ"]));
                bool result = objMainClass.ApprovePO(objMainClass.intCmpId, Convert.ToInt32(hfPopPOId.Value), txtAPREJReason.Text, (int)APRVTYPE.APPROVED, Convert.ToInt32(Session["USERID"]), "APPROVEPO");
                if (result == true)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('PO Approved successfully!');$('.close').click(function(){window.location.href ='AprvPO.aspx' });", true);
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

        protected void lnkExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    string attachment = "attachment; filename=ApprovePo.xls";
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

                throw ex;
            }
        }
    }
}