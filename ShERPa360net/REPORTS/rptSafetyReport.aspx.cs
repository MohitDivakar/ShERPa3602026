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

namespace ShERPa360net.REPORTS
{
    public partial class rptSafetyReport : System.Web.UI.Page
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
                        if (FormRights.bView == false)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }

                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtTodate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();

                        objBindDDL.FillPlant(ddlPlantCode);
                        string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                        ddlPlantCode.SelectedValue = plantArray[0];
                        GETDATA();


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

        public void GETDATA()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetSafetyReport(0, "SELECTALL", "", txtFromDate.Text, txtTodate.Text, "", ddlPlantCode.SelectedValue, "", 0,"Safety");
                    if (dt.Rows.Count > 0)
                    {
                        grvSafetyReport.DataSource = dt;
                        grvSafetyReport.DataBind();
                    }
                    else
                    {
                        grvSafetyReport.DataSource = string.Empty;
                        grvSafetyReport.DataBind();
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

        protected void ddlPlantCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (ddlPlantCode.SelectedIndex > 0)
                    {
                        string PLantRights = string.Empty;
                        string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                        for (int i = 0; i < plantArray.Count(); i++)
                        {
                            if (plantArray[i].Trim() == ddlPlantCode.SelectedValue)
                            {
                                PLantRights = ddlPlantCode.SelectedValue;
                            }
                        }

                        if (PLantRights.Length > 0)
                        {

                        }
                        else
                        {
                            ddlPlantCode.SelectedValue = plantArray[0];
                            ddlPlantCode.Focus();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have plant rights.');", true);
                            ddlPlantCode.SelectedValue = plantArray[0];
                            ddlPlantCode.Focus();
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


        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected void lnkExport_Click(object sender, EventArgs e)
        {
            try
            {
                string attachment = "attachment; filename=SafetyReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vdn.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                grvSafetyReport.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void lnkSearhSR_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GETDATA();
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
                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                string rptid = grdrow.Cells[0].Text;

                DataTable dt = new DataTable();
                dt = objMainClass.GetSafetyReport(Convert.ToInt32(rptid), "REPORT", "", "", "", "", "", "", 0,"Safety");
                if (dt.Rows.Count > 0)
                {
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    DataTable dtimage = new DataTable();
                    dtimage = objMainClass.GetSafetyReport(Convert.ToInt32(rptid), "REPORTIMAGE", "", "", "", "", "", "", 0,"Safety");

                    ReportViewer1.LocalReport.ReportPath = "Report/rptSafetyReport.rdlc";
                    ReportDataSource rds = new ReportDataSource("DSSafetyReport", dt);
                    ReportDataSource rds1 = new ReportDataSource("DSSafetyReportImage", dtimage);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                    ReportViewer1.LocalReport.DataSources.Add(rds1);
                    //ReportViewer1.Visible = true;

                    string FileName = "SafetyReport_" + Convert.ToString(dt.Rows[0]["LOCATION"]) + "_" + Convert.ToString(dt.Rows[0]["REPORTDATE"]) + ".pdf";
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

                    //Response.Clear();
                    //Response.Buffer = true;
                    //Response.Charset = "";
                    //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    //Response.ContentType = "application/pdf";
                    //Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName + ".pdf");
                    //Response.BinaryWrite(mybytes);
                    //Response.Flush();
                    //Response.End();
                    //HttpContext.Current.ApplicationInstance.CompleteRequest();




                    using (ZipFile zip = new ZipFile())
                    {
                        zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                        for (int i = 0; i < dtimage.Rows.Count; i++)
                        {
                            zip.AddEntry(Convert.ToString(dt.Rows[0]["LOCATION"]) + " - " + Convert.ToString(dtimage.Rows[i]["RPTID"]) + " - "+ Convert.ToString(dtimage.Rows[i]["QUESTIONID"]) + ".jpeg", (byte[])dtimage.Rows[i]["IMAGE"]);
                        }
                        zip.AddEntry(FileName, (byte[])mybytes);

                        Response.Clear();
                        Response.BufferOutput = false;
                        string zipName = String.Format("SafetyReport_" + Convert.ToString(dt.Rows[0]["LOCATION"]) + "_" + Convert.ToString(dt.Rows[0]["REPORTDATE"]) + "_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                        Response.ContentType = "application/zip";
                        Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                        zip.Save(Response.OutputStream);
                        //Response.End();
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }



                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Records Found. Please try again later!');", true);
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}