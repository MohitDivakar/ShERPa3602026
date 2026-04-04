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
    public partial class rptGRNInvoice : System.Web.UI.Page
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
                        if (FormRights.bView == false) //if (objDALUserRights.bView == false)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }

                        string[] date = objMainClass.indianTime.Date.ToString("dd-MM-yyyy").Split('-');
                        DateTime fromdate = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), 1);
                        txtFromDocDate.Text = fromdate.ToString("dd-MM-yyyy");
                        txtToDocDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        objBindDDL.FillPlant(ddlPlantCode, "SEARCH");
                        string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                        ddlPlantCode.SelectedValue = plantArray[0];
                        BindData();

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

        public void BindData()
        {
            //GetGRMInvoiceRPT
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetGRMInvoiceRPT(txtFromDocDate.Text, txtToDocDate.Text, ddlPlantCode.SelectedValue, txtDocNo.Text, "", txtPoNo.Text);
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

        protected void lnkSerch_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    BindData();
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
                            if (plantArray[i] == ddlPlantCode.SelectedValue)
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
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have plant rights.');$('#txtJobId').focus();", true);
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

        protected void btnInv_Click(object sender, EventArgs e)
        {
            try
            {

                objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), "tsmTranMMMIRPOINV", "");
                if (FormRights.bView == false) //if (objDALUserRights.bView == false)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to download invoice image.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                    //return;
                }
                else
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    string docno = grdrow.Cells[0].Text;
                    string pono = grdrow.Cells[2].Text;
                    DataTable dt = new DataTable();
                    dt = objMainClass.SelectGRNIMAGE(pono, docno);
                    if (dt.Rows.Count > 0)
                    {
                        string fileName, contentType;
                        byte[] data = (byte[])dt.Rows[0]["INVIMAGE"];


                        if (Convert.ToString(dt.Rows[0]["EXTENSION"]) == ".htm" || Convert.ToString(dt.Rows[0]["EXTENSION"]) == ".html")
                        {
                            contentType = "text/HTML";
                        }
                        else if (Convert.ToString(dt.Rows[0]["EXTENSION"]) == ".txt")
                        {
                            contentType = "text/plain";
                        }
                        else if (Convert.ToString(dt.Rows[0]["EXTENSION"]) == ".doc" || Convert.ToString(dt.Rows[0]["EXTENSION"]) == ".rtf" || Convert.ToString(dt.Rows[0]["EXTENSION"]) == ".docx")
                        {
                            contentType = "Application/msword";
                        }
                        else if (Convert.ToString(dt.Rows[0]["EXTENSION"]) == ".xls" || Convert.ToString(dt.Rows[0]["EXTENSION"]) == ".xlsx")
                        {
                            contentType = "text/x-msexcel";
                        }
                        else if (Convert.ToString(dt.Rows[0]["EXTENSION"]) == ".jpg" || Convert.ToString(dt.Rows[0]["EXTENSION"]) == ".jpeg")
                        {
                            contentType = "image/jpeg";
                        }
                        else if (Convert.ToString(dt.Rows[0]["EXTENSION"]) == ".gif")
                        {
                            contentType = "image/GIF";
                        }
                        else if (Convert.ToString(dt.Rows[0]["EXTENSION"]) == ".pdf")
                        {
                            contentType = "application/pdf";
                        }
                        else
                        {
                            contentType = "image/jpeg";
                        }

                        fileName = docno + Convert.ToString(dt.Rows[0]["EXTENSION"]);
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = contentType;
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                        Response.BinaryWrite(data);
                        Response.Flush();
                        Response.End();

                        //Response.Clear();
                        //Response.AddHeader("Cache-Control", "no-cache, must-revalidate, post-check=0, pre-check=0");
                        //Response.AddHeader("Pragma", "no-cache");
                        //Response.AddHeader("Content-Description", "File Download");
                        //Response.AddHeader("Content-Type", "application/force-download");
                        //Response.AddHeader("Content-Transfer-Encoding", "binary\n");
                        //Response.AddHeader("content-disposition", "attachment;filename=" + docno + ".jpeg");
                        //Response.BinaryWrite(data);
                        //Response.End();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invoice not uploaded.!');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string status = e.Row.Cells[4].Text;
                LinkButton btnInv = (LinkButton)e.Row.FindControl("btnInv");
                if (status == "INVOICE NOT UPLOADED")
                {
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
                    btnInv.Visible = false;
                }
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected void lnkExport_Click(object sender, EventArgs e)
        {
            try
            {
                string attachment = "attachment; filename=GRNInvoice.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vdn.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gvList.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}