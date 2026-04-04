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
    public partial class ViewMaterialInwardList : System.Web.UI.Page
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

        protected void lnkSerchMI_Click(object sender, EventArgs e)
        {
            try
            {
                BindGrid();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }


        public void BindGrid()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objMainClass.GetMaterialInwardFromPO(objMainClass.intCmpId, txtDocNo.Text, txtDocType.Text, txtFromDocDate.Text, txtToDocDate.Text, txtRefDocNo.Text, txtRefNo.Text, "", ddlPlantCode.SelectedIndex > 0 ? ddlPlantCode.SelectedValue : Convert.ToString(Session["PLANTCD"]));
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
                string attachment = "attachment; filename=MaterialInwardedDetailFromPo.xls";
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void bntView_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                string docno = grdrow.Cells[1].Text;
                DataTable dt = new DataTable();
                dt = objMainClass.GetEachMaterialInwardDetail(objMainClass.intCmpId, docno, "103");
                if (dt.Rows.Count > 0)
                {
                    lblDoctType.Text = Convert.ToString(dt.Rows[0]["DOCTYPE"]);
                    lblDocNo.Text = Convert.ToString(dt.Rows[0]["DOCNO"]);
                    lblDocDate.Text = Convert.ToString(dt.Rows[0]["DOCKDT"]);
                    lblPoNo.Text = Convert.ToString(dt.Rows[0]["REFDOCNO"]);
                    lblTransporter.Text = Convert.ToString(dt.Rows[0]["VENDORNAME"]);
                    lblchalanNo.Text = Convert.ToString(dt.Rows[0]["CHLNNO"]);
                    lblchalanDate.Text = Convert.ToString(dt.Rows[0]["CHLNDT"]);
                    lblRemark.Text = Convert.ToString(dt.Rows[0]["REMARK"]);

                    gvMaterialInward.DataSource = dt;
                    gvMaterialInward.DataBind();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                }
                else
                {
                    gvMaterialInward.DataSource = string.Empty;
                    gvMaterialInward.DataBind();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {

            try
            {

                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                string docno = grdrow.Cells[1].Text;

                string path = "ViewInwardPDF.aspx";
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "?DOCNO=" + docno + "');", true);
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
                    string docno = grdrow.Cells[1].Text;
                    string pono = grdrow.Cells[4].Text;
                    DataTable dt = new DataTable();
                    dt = objMainClass.SelectGRNIMAGE(pono, docno);
                    if (dt.Rows.Count > 0)
                    {
                        string fileName, contentType;
                        byte[] data = (byte[])dt.Rows[0]["INVIMAGE"];
                        //Response.Clear();
                        //Response.AddHeader("Cache-Control", "no-cache, must-revalidate, post-check=0, pre-check=0");
                        //Response.AddHeader("Pragma", "no-cache");
                        //Response.AddHeader("Content-Description", "File Download");
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
                        //Response.AddHeader("Content-Transfer-Encoding", "binary\n");
                        //Response.AddHeader("content-disposition", "attachment;filename=" + grdrow.Cells[1].Text + ".jpeg");
                        //Response.BinaryWrite(data);
                        //Response.End();

                        fileName = grdrow.Cells[1].Text + "Invoice" + Convert.ToString(dt.Rows[0]["EXTENSION"]);
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = contentType;
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                        Response.BinaryWrite(data);
                        Response.Flush();
                        Response.End();

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

        protected void btnPO_Click(object sender, EventArgs e)
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
                    string docno = grdrow.Cells[1].Text;
                    string pono = grdrow.Cells[4].Text;
                    DataTable dt = new DataTable();
                    dt = objMainClass.SelectGRNIMAGE(pono, docno);
                    if (dt.Rows.Count > 0)
                    {
                        if(Convert.ToString(dt.Rows[0]["POEXTENSION"]) != "")
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
                            //Response.AddHeader("Content-Transfer-Encoding", "binary\n");
                            //Response.AddHeader("content-disposition", "attachment;filename=" + grdrow.Cells[1].Text + ".jpeg");
                            //Response.BinaryWrite(data);
                            //Response.End();

                            fileName = grdrow.Cells[1].Text + "PO" + Convert.ToString(dt.Rows[0]["POEXTENSION"]);
                            Response.Clear();
                            Response.Buffer = true;
                            Response.Charset = "";
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            Response.ContentType = contentType;
                            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                            Response.BinaryWrite(data);
                            Response.Flush();
                            Response.End();

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('PO not uploaded.!');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('PO not uploaded.!');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}
