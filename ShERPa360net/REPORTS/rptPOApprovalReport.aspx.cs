using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.REPORTS
{
    public partial class rptPOApprovalReport : System.Web.UI.Page
    {

        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
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
                        if (FormRights.bView == false)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }

                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();

                        BindGrid();
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

        public void BindGrid()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetSeqApprovedPO(Convert.ToInt32(Session["USERID"]), txtFromDate.Text, txtToDate.Text, "PO", txtPONO.Text, "APPROVEDPOSEQ");
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


        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void lnkExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    string attachment = "attachment; filename=ApprovedPO.xls";
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

        protected void lnkSearhPO_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    BindGrid();
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
                    string pono = grdrow.Cells[1].Text;

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
                        lblAPRV1.Text = string.Empty;
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
    }
}