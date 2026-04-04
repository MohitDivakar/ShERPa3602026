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
    public partial class frmErrorSKUDownload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Session["USERID"] != null)
                    {
                        if (Session["ERRORSKU"] != null && Session["ERRORSKU"].ToString() != null)
                        {
                            DownloadErrorFile();
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


        public void DownloadErrorFile()
        {
            try
            {
                string attachment = "attachment; filename=ErrorSKU" + DateTime.Now + ".xls";
                Response.Clear();
                Response.ContentType = "application/vdn.ms-excel";
                Response.AddHeader("Content-Disposition", attachment);

                DataTable dtprice = new DataTable();
                dtprice = (DataTable)Session["ERRORSKU"];
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GridView grvDL = new GridView();
                grvDL.DataSource = dtprice;
                grvDL.DataBind();
                grvDL.RenderControl(htw);
                Response.Write(sw.ToString());

                Response.Flush();
                Response.SuppressContent = true; // avoid the "Thread was being aborted" exception
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                Session["ERRORSKU"] = null;
                Session["ERRORSKU"] = null;
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }

        }

    }
}