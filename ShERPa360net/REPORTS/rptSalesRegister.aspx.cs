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
    public partial class rptSalesRegister : System.Web.UI.Page
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
                        objBindDDL.FillSegment(ddlSegment);
                        objBindDDL.FillDistChnlNew(ddlDistChnl);
                        objBindDDL.FillDocType(ddlDocType, "SI");
                        objBindDDL.FillLists(ddlSalesChnl, "SF");
                        ddlSalesChnl.SelectedValue = "11193";
                        objBindDDL.FillItemGrp(ddlDeviceType, "ENTRY");


                        //BindData();

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
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    if (chkShowSummary.Checked == true)
                    {
                        dt = objMainClass.SalesRegisterData(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, ddlPLant.SelectedIndex > 0 ? ddlPLant.SelectedValue : "",
                        ddlLocation.SelectedIndex > 0 ? ddlLocation.SelectedValue : "", txtSINo.Text, ddlDocType.SelectedIndex > 0 ? ddlDocType.SelectedValue : "", txtRefNo.Text,
                        chkExcludeReturn.Checked == true ? 1 : 0, ddlSegment.SelectedIndex > 0 ? ddlSegment.SelectedValue : "", ddlDistChnl.SelectedIndex > 0 ? ddlDistChnl.SelectedValue : "",
                        txtJobId.Text, txtSONo.Text, txtIMEINo.Text, "SALESSUMMARY", ddlSalesChnl.SelectedIndex > 0 ? ddlSalesChnl.SelectedValue : "", ddlDeviceType.SelectedIndex > 0 ? ddlDeviceType.SelectedValue : "", txtCustName.Text);
                        if (dt.Rows.Count > 0)
                        {
                            gvSummary.DataSource = dt;
                            gvSummary.DataBind();
                            gvSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                            divSummary.Visible = true;
                            divAll.Visible = false;
                            divPurchase.Visible = false;
                            divACFormat.Visible = false;
                        }
                        else
                        {
                            gvSummary.DataSource = string.Empty;
                            gvSummary.DataBind();
                            divSummary.Visible = true;
                            divAll.Visible = false;
                            divPurchase.Visible = false;
                            divACFormat.Visible = false;
                        }
                    }
                    else if (chkPurDet.Checked == true)
                    {
                        dt = objMainClass.SalesRegisterData(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, ddlPLant.SelectedIndex > 0 ? ddlPLant.SelectedValue : "",
                        ddlLocation.SelectedIndex > 0 ? ddlLocation.SelectedValue : "", txtSINo.Text, ddlDocType.SelectedIndex > 0 ? ddlDocType.SelectedValue : "", txtRefNo.Text,
                        chkExcludeReturn.Checked == true ? 1 : 0, ddlSegment.SelectedIndex > 0 ? ddlSegment.SelectedValue : "", ddlDistChnl.SelectedIndex > 0 ? ddlDistChnl.SelectedValue : "",
                        txtJobId.Text, txtSONo.Text, txtIMEINo.Text, "SALESWITHPURCHASE", ddlSalesChnl.SelectedIndex > 0 ? ddlSalesChnl.SelectedValue : "", ddlDeviceType.SelectedIndex > 0 ? ddlDeviceType.SelectedValue : "", txtCustName.Text);
                        if (dt.Rows.Count > 0)
                        {
                            gvPurDetail.DataSource = dt;
                            gvPurDetail.DataBind();
                            gvPurDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
                            divSummary.Visible = false;
                            divAll.Visible = false;
                            divPurchase.Visible = true;
                            divACFormat.Visible = false;
                        }
                        else
                        {
                            gvPurDetail.DataSource = string.Empty;
                            gvPurDetail.DataBind();
                            divSummary.Visible = false;
                            divAll.Visible = false;
                            divPurchase.Visible = true;
                            divACFormat.Visible = false;
                        }
                    }
                    else if (chkAcFormat.Checked == true)
                    {
                        dt = objMainClass.SalesRegisterData(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, ddlPLant.SelectedIndex > 0 ? ddlPLant.SelectedValue : "",
                        ddlLocation.SelectedIndex > 0 ? ddlLocation.SelectedValue : "", txtSINo.Text, ddlDocType.SelectedIndex > 0 ? ddlDocType.SelectedValue : "", txtRefNo.Text,
                        chkExcludeReturn.Checked == true ? 1 : 0, ddlSegment.SelectedIndex > 0 ? ddlSegment.SelectedValue : "", ddlDistChnl.SelectedIndex > 0 ? ddlDistChnl.SelectedValue : "",
                        txtJobId.Text, txtSONo.Text, txtIMEINo.Text, "SALESINACFORMAT", ddlSalesChnl.SelectedIndex > 0 ? ddlSalesChnl.SelectedValue : "", ddlDeviceType.SelectedIndex > 0 ? ddlDeviceType.SelectedValue : "", txtCustName.Text);
                        if (dt.Rows.Count > 0)
                        {
                            gvACFormat.DataSource = dt;
                            gvACFormat.DataBind();
                            gvACFormat.HeaderRow.TableSection = TableRowSection.TableHeader;
                            divSummary.Visible = false;
                            divAll.Visible = false;
                            divPurchase.Visible = false;
                            divACFormat.Visible = true;
                        }
                        else
                        {
                            gvPurDetail.DataSource = string.Empty;
                            gvPurDetail.DataBind();
                            divSummary.Visible = false;
                            divAll.Visible = false;
                            divPurchase.Visible = false;
                            divACFormat.Visible = true;

                        }
                    }
                    else
                    {
                        dt = objMainClass.SalesRegisterData(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, ddlPLant.SelectedIndex > 0 ? ddlPLant.SelectedValue : "",
                        ddlLocation.SelectedIndex > 0 ? ddlLocation.SelectedValue : "", txtSINo.Text, ddlDocType.SelectedIndex > 0 ? ddlDocType.SelectedValue : "", txtRefNo.Text,
                        chkExcludeReturn.Checked == true ? 1 : 0, ddlSegment.SelectedIndex > 0 ? ddlSegment.SelectedValue : "", ddlDistChnl.SelectedIndex > 0 ? ddlDistChnl.SelectedValue : "",
                        txtJobId.Text, txtSONo.Text, txtIMEINo.Text, "SALESREGISTER", ddlSalesChnl.SelectedIndex > 0 ? ddlSalesChnl.SelectedValue : "", ddlDeviceType.SelectedIndex > 0 ? ddlDeviceType.SelectedValue : "", txtCustName.Text);
                        if (dt.Rows.Count > 0)
                        {
                            gvAllList.DataSource = dt;
                            gvAllList.DataBind();
                            gvAllList.HeaderRow.TableSection = TableRowSection.TableHeader;
                            divSummary.Visible = false;
                            divAll.Visible = true;
                            divPurchase.Visible = false;
                            divACFormat.Visible = false;
                        }
                        else
                        {
                            gvAllList.DataSource = string.Empty;
                            gvAllList.DataBind();
                            divSummary.Visible = false;
                            divAll.Visible = true;
                            divPurchase.Visible = false;
                            divACFormat.Visible = false;
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

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void lnkExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    if (divAll.Visible == true)
                    {
                        string attachment = "attachment; filename=SORegister.xls";
                        Response.ClearContent();
                        Response.AddHeader("content-disposition", attachment);
                        Response.ContentType = "application/vdn.ms-excel";
                        StringWriter sw = new StringWriter();
                        HtmlTextWriter htw = new HtmlTextWriter(sw);
                        gvAllList.RenderControl(htw);
                        Response.Write(sw.ToString());
                        Response.End();
                    }
                    else if (gvSummary.Visible == true)
                    {
                        string attachment = "attachment; filename=SORegisterSummary.xls";
                        Response.ClearContent();
                        Response.AddHeader("content-disposition", attachment);
                        Response.ContentType = "application/vdn.ms-excel";
                        StringWriter sw = new StringWriter();
                        HtmlTextWriter htw = new HtmlTextWriter(sw);
                        gvSummary.RenderControl(htw);
                        Response.Write(sw.ToString());
                        Response.End();
                    }
                    else if (gvPurDetail.Visible == true)
                    {
                        string attachment = "attachment; filename=SORegisterwithPurchaseDetails.xls";
                        Response.ClearContent();
                        Response.AddHeader("content-disposition", attachment);
                        Response.ContentType = "application/vdn.ms-excel";
                        StringWriter sw = new StringWriter();
                        HtmlTextWriter htw = new HtmlTextWriter(sw);
                        gvPurDetail.RenderControl(htw);
                        Response.Write(sw.ToString());
                        Response.End();
                    }
                    else
                    {

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