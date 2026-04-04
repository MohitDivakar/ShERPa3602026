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
    public partial class ViewQuotation : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
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
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='frmApproveQuotation.aspx' });", true);
                            return;
                        }

                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();

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
                    DataTable dtMain = new DataTable();
                    dtMain = objMainClass.GetQuotforApprove(objMainClass.intCmpId, txtQuotNo.Text, txtFromDate.Text, txtToDate.Text, "", 0, 0, "", 0, 0, "GETQUOTMST", "");

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
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lnkSearch_Click(object sender, EventArgs e)
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

        protected void lnkExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    string attachment = "attachment; filename=QuotationList.xls";
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
                    string Quotation = grdrow.Cells[0].Text;
                    string plant = grdrow.Cells[2].Text;

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

        protected void btnDownload_Click(object sender, EventArgs e)
        {

            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    string Quotation = grdrow.Cells[0].Text;
                    string plant = grdrow.Cells[2].Text;

                    DataTable dtPOMST = new DataTable();
                    DataTable dtPODTL = new DataTable();
                    DataTable dtPOAPRV = new DataTable();
                    DataTable dtPOTAX = new DataTable();
                    dtPOMST = objMainClass.SelectQuotData(objMainClass.intCmpId, Quotation, 1);
                    dtPODTL = objMainClass.SelectQuotData(objMainClass.intCmpId, Quotation, 2);
                    dtPOAPRV = objMainClass.SelectQuotData(objMainClass.intCmpId, Quotation, 4);
                    dtPOTAX = objMainClass.SelectQuotData(objMainClass.intCmpId, Quotation, 5);

                    if (dtPOMST.Rows.Count > 0)
                    {
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

                        string extension = ".pdf";
                        string encoding = String.Empty;
                        Warning[] warnings;
                        string mimeType = String.Empty;
                        string[] streams;
                        string contentType = String.Empty;


                        Byte[] mybytes = ReportViewer1.LocalReport.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings);
                        Response.Clear();
                        MemoryStream ms = new MemoryStream(mybytes);
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", "attachment;filename=" + Quotation + "_Quotation.pdf.pdf");
                        Response.Buffer = true;
                        ms.WriteTo(Response.OutputStream);
                        Response.End();

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
    }
}