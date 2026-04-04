using Ionic.Zip;
using iTextSharp.text;
using iTextSharp.text.pdf;
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

namespace ShERPa360net.REPORTS
{
    public partial class rptPBRegister : System.Web.UI.Page
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
                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();

                        DataTable dt = new DataTable();
                        dt = objMainClass.GetPurchaseBillReg(txtFromDate.Text, txtToDate.Text, txtPBNO.Text, txtPONO.Text, "SELECTALLPB");
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
        }



        protected void lnkSerchPB_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetPurchaseBillReg(txtFromDate.Text, txtToDate.Text, txtPBNO.Text, txtPONO.Text, "SELECTALLPB");

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

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
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

        protected void lnkExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    string attachment = "attachment; filename=PurchaseBill.xls";
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

        protected void bntView_Click(object sender, EventArgs e)
        {

            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    string pbno = grdrow.Cells[1].Text;
                    DataTable dtPB = new DataTable();
                    DataTable dtPBDTL = new DataTable();
                    DataTable dtPBCOND = new DataTable();
                    DataTable dtPBCHG = new DataTable();
                    dtPB = objMainClass.GetEachPurchaseBill(pbno, "PBMASTER");
                    dtPBDTL = objMainClass.GetEachPurchaseBill(pbno, "PBDETAIL");
                    dtPBCOND = objMainClass.GetEachPurchaseBill(pbno, "PBTAX");
                    dtPBCHG = objMainClass.GetEachPurchaseBill(pbno, "PBCHARGES");
                    if (dtPB.Rows.Count > 0)
                    {

                        lblDoctype.Text = Convert.ToString(dtPB.Rows[0]["PBTYPE"]);
                        lblPBDate.Text = Convert.ToDateTime(dtPB.Rows[0]["PBDT"]).ToShortDateString();
                        lblPBNo.Text = Convert.ToString(dtPB.Rows[0]["PBNO"]);
                        lblBillDate.Text = Convert.ToDateTime(dtPB.Rows[0]["BILLDT"]).ToShortDateString();
                        lblPBNo.Text = Convert.ToString(dtPB.Rows[0]["PBNO"]);
                        lblBillNo.Text = Convert.ToString(dtPB.Rows[0]["BILLNO"]);
                        lblRemark.Text = Convert.ToString(dtPB.Rows[0]["REMARK"]);

                        lblPBVendor.Text = Convert.ToString(dtPB.Rows[0]["VENDNAME"]);
                        lblPBPayTerms.Text = Convert.ToString(dtPB.Rows[0]["PMTTERMS"]);
                        lblPBPayTermsDesc.Text = Convert.ToString(dtPB.Rows[0]["PMTTERMSDESC"]);

                        lblNetPBAmt.Text = Convert.ToString(dtPB.Rows[0]["NETPBAMT"]);
                        lblMaterialAmt.Text = Convert.ToString(dtPB.Rows[0]["NETMATVALUE"]);
                        lblTaxAmt.Text = Convert.ToString(dtPB.Rows[0]["NETTAXAMT"]);
                        lblDiscountAmt.Text = Convert.ToString(dtPB.Rows[0]["NETDISCOUNT"]);
                        lblOtherChg.Text = Convert.ToString(dtPB.Rows[0]["OTHERCHARGES"]);

                        gvDetail.DataSource = dtPBDTL;
                        gvDetail.DataBind();

                        grvTaxation.DataSource = dtPBCOND;
                        grvTaxation.DataBind();

                        grvOtherChg.DataSource = dtPBCHG;
                        grvOtherChg.DataBind();

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                    }
                    else
                    {
                        gvDetail.DataSource = string.Empty;
                        gvDetail.DataBind();

                        grvTaxation.DataSource = string.Empty;
                        grvTaxation.DataBind();

                        grvOtherChg.DataSource = string.Empty;
                        grvOtherChg.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    string pbno = grdrow.Cells[1].Text;
                    string path = "../MM/ViewPurchaseBillPDF.aspx";
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "?PBNO=" + pbno + "');", true);
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
                    string pbno = grdrow.Cells[1].Text;
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
                                zip.AddEntry(pbno + "" + Convert.ToString(dt.Rows[i]["PONO"]) + "" + Convert.ToString(dt.Rows[i]["DOCNO"]) + i + Convert.ToString(dt.Rows[i]["EXTENSION"]), (byte[])dt.Rows[i]["INVIMAGE"]);
                                //zip.AddEntry(pbno + " - " + Convert.ToString(dt.Rows[i]["PONO"]) + " - " + Convert.ToString(dt.Rows[i]["DOCNO"]) + i + ".jpeg", (byte[])dt.Rows[i]["INVIMAGE"]);
                            }
                            zip.AddEntry(FileName, (byte[])mybytes);

                            Response.Clear();
                            Response.BufferOutput = false;
                            string zipName = String.Format("PB_" + pbno + "_" + Convert.ToString(DateTime.Now).Replace("/", "").Replace("-", "").Replace("_", "") + "_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                            Response.ContentType = "application/zip";
                            Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                            zip.Save(Response.OutputStream);
                            Response.End();
                            //HttpContext.Current.ApplicationInstance.CompleteRequest();
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
    }
}