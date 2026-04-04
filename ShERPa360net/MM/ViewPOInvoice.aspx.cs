using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.MM
{
    public partial class ViewPOInvoice : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Request.QueryString.Count > 0)
                    {
                        if (Convert.ToString(Request.QueryString["PONO"]) != null && Convert.ToString(Request.QueryString["PONO"]) != string.Empty && Convert.ToString(Request.QueryString["PONO"]) != "" &&
                            Convert.ToString(Request.QueryString["GRNNO"]) != null && Convert.ToString(Request.QueryString["GRNNO"]) != string.Empty && Convert.ToString(Request.QueryString["GRNNO"]) != "")
                        {
                            GetData(Convert.ToString(Request.QueryString["PONO"]), Convert.ToString(Request.QueryString["GRNNO"]));
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Wrong MR No.!');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Wrong MR No.!');", true);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
                }
            }
        }

        public void GetData(string PONO, string GRNNO)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objMainClass.CheckPOGRN(objMainClass.intCmpId, PONO, GRNNO, "CHECKGRNDOC");

                if (dt.Rows.Count > 0)
                {
                    string fileName, contentType;
                    byte[] data = (byte[])dt.Rows[0]["POIMAGE"];
                    //Response.Clear();
                    //Response.AddHeader("Cache-Control", "no-cache, must-revalidate, post-check=0, pre-check=0");
                    //Response.AddHeader("Pragma", "no-cache");
                    //Response.AddHeader("Content-Description", "File Download");
                    if (Convert.ToString(dt.Rows[0]["POEXTENSION"]) == ".htm" || Convert.ToString(dt.Rows[0]["POEXTENSION"]) == ".html")
                    {
                        contentType = "text/HTML";
                    }
                    else if (Convert.ToString(dt.Rows[0]["POEXTENSION"]) == ".txt")
                    {
                        contentType = "text/plain";
                    }
                    else if (Convert.ToString(dt.Rows[0]["POEXTENSION"]) == ".doc" || Convert.ToString(dt.Rows[0]["POEXTENSION"]) == ".rtf" || Convert.ToString(dt.Rows[0]["POEXTENSION"]) == ".docx")
                    {
                        contentType = "Application/msword";
                    }
                    else if (Convert.ToString(dt.Rows[0]["POEXTENSION"]) == ".xls" || Convert.ToString(dt.Rows[0]["POEXTENSION"]) == ".xlsx")
                    {
                        contentType = "text/x-msexcel";
                    }
                    else if (Convert.ToString(dt.Rows[0]["POEXTENSION"]) == ".jpg" || Convert.ToString(dt.Rows[0]["POEXTENSION"]) == ".jpeg")
                    {
                        contentType = "image/jpeg";
                    }
                    else if (Convert.ToString(dt.Rows[0]["POEXTENSION"]) == ".gif")
                    {
                        contentType = "image/GIF";
                    }
                    else if (Convert.ToString(dt.Rows[0]["POEXTENSION"]) == ".pdf")
                    {
                        contentType = "application/pdf";
                    }
                    else
                    {
                        contentType = "image/jpeg";
                    }

                    //fileName = MRNO + "_Invoice" + Convert.ToString(dt.Rows[0]["POEXTENSION"]);
                    //Response.Clear();
                    //Response.Buffer = true;
                    //Response.Charset = "";
                    //Response.Cache.SetCacheability(HttpCacheability.NoCache);

                    Response.ContentType = contentType;
                    //Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                    Response.AddHeader("content-length", data.Length.ToString());
                    Response.BinaryWrite(data);

                    //Response.Flush();
                    //Response.End();

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invoice not uploaded.!');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}