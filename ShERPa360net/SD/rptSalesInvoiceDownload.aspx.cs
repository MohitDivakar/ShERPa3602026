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

namespace ShERPa360net.SD
{
    public partial class rptSalesInvoiceDownload : System.Web.UI.Page
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

                        if (Request.QueryString.Count > 0)
                        {
                            if (Convert.ToString(Request.QueryString["QRPTSINO"]) != null && Convert.ToString(Request.QueryString["QRPTSINO"]) != string.Empty && Convert.ToString(Request.QueryString["QRPTSINO"]) != "")
                            {
                                Session["RPTSINO"] = Convert.ToString(Request.QueryString["QRPTSINO"]);
                            }
                            Response.Redirect(Request.Url.AbsolutePath, false);
                        }
                        else
                        {
                            if (Session["RPTSINO"] != null && Convert.ToString(Session["RPTSINO"]) != "" && Convert.ToString(Session["RPTSINO"]) != string.Empty)
                            {
                                DownloadSI(Convert.ToString(Session["RPTSINO"]));
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
        }

        public void DownloadSI(string SINO)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objMainClass.GetSalesReport(objMainClass.intCmpId, SINO, "SIDATA");

                DataTable dt2 = new DataTable();
                dt2 = objMainClass.GetSalesReport(objMainClass.intCmpId, SINO, "SICOND");

                if (dt.Rows.Count > 0)
                {
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = "Report/rptSIDoc.rdlc";
                    ReportDataSource rds = new ReportDataSource("DSSI", dt);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);

                    ReportDataSource rdstax = new ReportDataSource("DSSITAX", dt2);
                    ReportViewer1.LocalReport.DataSources.Add(rdstax);

                    string extension = ".pdf";
                    string encoding = String.Empty;
                    Warning[] warnings;
                    string mimeType = String.Empty;
                    string[] streams;

                    //ReportViewer1.Visible = true;

                    Byte[] mybytes = ReportViewer1.LocalReport.Render(".pdf", null, out extension, out encoding, out mimeType, out streams, out warnings);
                    using (FileStream fs = File.Create(Server.MapPath("~/download/") + SINO))
                    {
                        fs.Write(mybytes, 0, mybytes.Length);
                    }

                    Response.ClearHeaders();
                    Response.ClearContent();
                    Response.Buffer = true;
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + SINO);
                    Response.WriteFile(Server.MapPath("~/download/" + SINO));
                    Response.Flush();
                    Response.Close();
                    Response.End();

                    this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Record not found for SI No.!');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }

        }
    }
}