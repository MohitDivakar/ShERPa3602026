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

namespace ShERPa360net
{
    public partial class frmTCRPDF : System.Web.UI.Page
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

                    if (Request.QueryString.Count > 0)
                    {
                        if (Convert.ToString(Request.QueryString["TCRNO"]) != null && Convert.ToString(Request.QueryString["TCRNO"]) != string.Empty && Convert.ToString(Request.QueryString["TCRNO"]) != "")
                        {
                            Session["TCRNO"] = Convert.ToString(Request.QueryString["TCRNO"]);
                        }
                        Response.Redirect(Request.Url.AbsolutePath, false);
                    }
                    else
                    {
                        if (Session["TCRNO"] != null && Session["TCRNO"].ToString() != null)
                        {
                            DownloadTCR("", Convert.ToString(Session["TCRNO"]));
                        }
                        else
                        {
                            string message = "TCR number is wrong";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + message + "\");", true);
                        }
                    }

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
                }
            }
        }

        public void DownloadTCR(string COMPLAINTNO, string RCPTNO)
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

            ReportViewer1.Visible = true;

            string FileName = COMPLAINTNO + "_" + RCPTNO + "_TCR.pdf";
            string extension = ".pdf";
            string encoding = String.Empty;
            Warning[] warnings;
            string mimeType = String.Empty;
            string[] streams;
            string[] streamIds;
            string contentType = String.Empty;

            //Byte[] mybytes = ReportViewer1.LocalReport.Render("PDF", null,
            //        out extension, out encoding,
            //        out mimeType, out streams, out warnings);

            ////string localPath = AppDomain.CurrentDomain.BaseDirectory;

            ////string localPath = Server.MapPath(@"http://14.98.132.190:360/img/") + FileName;

            //string folderpath = "~/excel/";
            //string filePath = Path.Combine(Server.MapPath(folderpath), FileName);

            //string localPath = (Server.MapPath("../img/" + FileName));
            ////localPath = localPath + FileName;
            ////System.IO.File.WriteAllBytes(localPath, mybytes);
            //System.IO.File.WriteAllBytes(filePath, mybytes);
            ////url = localPath;
            //url = filePath;

            //url = "http://14.98.132.190:360/img/" + FileName;
            //return url;
        }
    }
}