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
    public partial class rptPORegister : System.Web.UI.Page
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

                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();

                        objBindDDL.FillPlant(ddlPLant);
                        ddlPLant.SelectedValue = "1001";
                        objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);
                        ddlLocation.SelectedValue = "WMR1";
                        objBindDDL.FillItemGrp(ddlItemGroup, "ENTRY");

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
                    if (chkSummary.Checked == false)
                    {
                        DataTable dtAll = new DataTable();

                        dtAll = objMainClass.GetPORegister(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, ddlPLant.SelectedValue, ddlLocation.SelectedValue, txtPONo.Text, txtPRNo.Text,
                            txtTRNo.Text, txtRefNo.Text, txtVendCode.Text, txtItemCode.Text, "POALLDATA", txtIMEINo.Text, ddlItemGroup.SelectedIndex > 0 ? ddlItemGroup.SelectedValue : "");
                        if (dtAll.Rows.Count > 0)
                        {
                            gvAllList.DataSource = dtAll;
                            gvAllList.DataBind();
                            gvAllList.HeaderRow.TableSection = TableRowSection.TableHeader;
                            divAll.Visible = true;
                            divSummary.Visible = false;
                        }
                        else
                        {
                            gvAllList.DataSource = string.Empty;
                            gvAllList.DataBind();
                            divAll.Visible = true;
                            divSummary.Visible = false;
                        }
                    }
                    else if (chkSummary.Checked == true)
                    {
                        DataTable dtSummary = new DataTable();
                        dtSummary = objMainClass.GetPORegister(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, ddlPLant.SelectedValue, ddlLocation.SelectedValue, txtPONo.Text, txtPRNo.Text,
                            txtTRNo.Text, txtRefNo.Text, txtVendCode.Text, txtItemCode.Text, "SHOWSUMMARY", txtIMEINo.Text, ddlItemGroup.SelectedIndex > 0 ? ddlItemGroup.SelectedValue : "");
                        if (dtSummary.Rows.Count > 0)
                        {
                            gvSummary.DataSource = dtSummary;
                            gvSummary.DataBind();
                            gvSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                            divAll.Visible = false;
                            divSummary.Visible = true;
                        }
                        else
                        {
                            gvSummary.DataSource = string.Empty;
                            gvSummary.DataBind();
                            divAll.Visible = false;
                            divSummary.Visible = true;
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

        protected void ddlPLant_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (ddlPLant.SelectedIndex > 0)
                    {
                        objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);
                    }
                    else
                    {
                        ddlLocation.DataSource = string.Empty;
                        ddlLocation.DataBind();
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

        protected void lnkSearh_Click(object sender, EventArgs e)
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



        protected void lnkExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {




                    if (gvAllList.Visible == true)
                    {
                        string attachment = "attachment; filename=PORegister.xls";
                        Response.ClearContent();
                        Response.AddHeader("content-disposition", attachment);
                        Response.ContentType = "application/vdn.ms-excel";
                        StringWriter sw = new StringWriter();
                        HtmlTextWriter htw = new HtmlTextWriter(sw);
                        gvAllList.RenderControl(htw);
                        Response.Write(sw.ToString());
                        Response.End();
                    }

                    if (gvSummary.Visible == true)
                    {
                        string attachment = "attachment; filename=POSummary.xls";
                        Response.ClearContent();
                        Response.AddHeader("content-disposition", attachment);
                        Response.ContentType = "application/vdn.ms-excel";
                        StringWriter sw = new StringWriter();
                        HtmlTextWriter htw = new HtmlTextWriter(sw);
                        gvSummary.RenderControl(htw);
                        Response.Write(sw.ToString());
                        Response.End();
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
    }
}