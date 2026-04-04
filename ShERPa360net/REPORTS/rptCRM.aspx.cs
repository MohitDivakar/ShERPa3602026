using ClosedXML.Excel;
using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.REPORTS
{
    public partial class rptCRM : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    if (Session["USERID"] != null)
                    {
                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menutabid.Value, "");
                        if (FormRights.bView == false)
                        {
                            //Session.Abandon();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }

                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        objBindDDL.FillAgentName(ddlAgent);

                        GetData();
                        divLead.Visible = true;
                        //if (gvNewLead.Rows.Count > 0 || gvFollowUps.Rows.Count > 0 || gvSO.Rows.Count > 0 || gvCancelled.Rows.Count > 0 || gvSOINQ.Rows.Count > 0 || gvReassign.Rows.Count > 0 || gvPending.Rows.Count > 0)
                        //{
                        //    lnkDownLoadAll.Visible = true;
                        //}
                        //else
                        //{
                        //    lnkDownLoadAll.Visible = false;
                        //}
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public void GetData()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dtNewLead = new DataTable();
                    dtNewLead = objMainClass.GetLeadData(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, 0, 0, 0, Convert.ToInt32(ddlAgent.SelectedValue), "RTPCREATED");
                    if (dtNewLead.Rows.Count > 0)
                    {
                        gvNewLead.DataSource = dtNewLead;
                        gvNewLead.DataBind();
                        gvNewLead.HeaderRow.TableSection = TableRowSection.TableHeader;
                        lblNewLead.Text = Convert.ToString(dtNewLead.Rows.Count);
                        Session["dtNewLead"] = dtNewLead;
                    }
                    else
                    {
                        gvNewLead.DataSource = string.Empty;
                        gvNewLead.DataBind();
                        lblNewLead.Text = "0";
                        Session["dtNewLead"] = dtNewLead;
                    }

                    DataTable dtFollowUps = new DataTable();
                    dtFollowUps = objMainClass.GetLeadData(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, (int)LeadStatus.OnHold, 0,
                        Convert.ToInt32(ddlAgent.SelectedValue), 0, "RTPUPDATED");
                    if (dtFollowUps.Rows.Count > 0)
                    {
                        gvFollowUps.DataSource = dtFollowUps;
                        gvFollowUps.DataBind();
                        gvFollowUps.HeaderRow.TableSection = TableRowSection.TableHeader;
                        lblFollowUp.Text = Convert.ToString(dtFollowUps.Rows.Count);
                        Session["dtFollowUps"] = dtFollowUps;
                    }
                    else
                    {
                        gvFollowUps.DataSource = string.Empty;
                        gvFollowUps.DataBind();
                        lblFollowUp.Text = "0";
                        Session["dtFollowUps"] = dtFollowUps;
                    }
                    //gvPending

                    DataTable dtPending = new DataTable();
                    dtPending = objMainClass.GetLeadData(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, (int)LeadStatus.Saved, 0,
                        Convert.ToInt32(ddlAgent.SelectedValue), 0, "RTPPENDING");
                    if (dtPending.Rows.Count > 0)
                    {
                        gvPending.DataSource = dtPending;
                        gvPending.DataBind();
                        gvPending.HeaderRow.TableSection = TableRowSection.TableHeader;
                        lblPending.Text = Convert.ToString(dtPending.Rows.Count);
                        Session["dtPending"] = dtPending;
                    }
                    else
                    {
                        gvPending.DataSource = string.Empty;
                        gvPending.DataBind();
                        lblPending.Text = "0";
                        Session["dtPending"] = dtPending;
                    }

                    //DataTable dtConverted = new DataTable();
                    //dtConverted = objMainClass.GetLeadData(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, (int)LeadStatus.Converted, 0,
                    //    Convert.ToInt32(ddlAgent.SelectedValue), 0, "RTPUPDATED");
                    //if (dtConverted.Rows.Count > 0)
                    //{
                    //    gvConverted.DataSource = dtConverted;
                    //    gvConverted.DataBind();
                    //    gvConverted.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //    lblConvertedCall.Text = Convert.ToString(dtConverted.Rows.Count);
                    //}
                    //else
                    //{
                    //    gvConverted.DataSource = string.Empty;
                    //    gvConverted.DataBind();
                    //    lblConvertedCall.Text = "0";
                    //}

                    DataTable dtINQSO = new DataTable();
                    dtINQSO = objMainClass.GetLeadData(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, (int)LeadStatus.Converted, 0,
                        Convert.ToInt32(ddlAgent.SelectedValue), 0, "INQGENERATED");
                    if (dtINQSO.Rows.Count > 0)
                    {
                        gvSOINQ.DataSource = dtINQSO;
                        gvSOINQ.DataBind();
                        gvSOINQ.HeaderRow.TableSection = TableRowSection.TableHeader;
                        lblSOGene.Text = Convert.ToString(dtINQSO.Rows.Count);
                        Session["dtINQSO"] = dtINQSO;
                    }
                    else
                    {
                        gvSOINQ.DataSource = string.Empty;
                        gvSOINQ.DataBind();
                        lblSOGene.Text = "0";
                        Session["dtINQSO"] = dtINQSO;
                    }


                    DataTable dtSO = new DataTable();
                    dtSO = objMainClass.GetLeadData(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, (int)LeadStatus.Converted, 0,
                        Convert.ToInt32(ddlAgent.SelectedValue), 0, "SOGENERATED");
                    if (dtSO.Rows.Count > 0)
                    {
                        gvSO.DataSource = dtSO;
                        gvSO.DataBind();
                        gvSO.HeaderRow.TableSection = TableRowSection.TableHeader;
                        lblSOGeneratedCall.Text = Convert.ToString(dtSO.Rows.Count);
                        Session["dtSO"] = dtSO;
                    }
                    else
                    {
                        gvSO.DataSource = string.Empty;
                        gvSO.DataBind();
                        lblSOGeneratedCall.Text = "0";
                        Session["dtSO"] = dtSO;
                    }



                    DataTable dtCancelled = new DataTable();
                    dtCancelled = objMainClass.GetLeadData(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, (int)LeadStatus.Cancelled, 0,
                        Convert.ToInt32(ddlAgent.SelectedValue), 0, "RTPUPDATED");
                    if (dtCancelled.Rows.Count > 0)
                    {
                        gvCancelled.DataSource = dtCancelled;
                        gvCancelled.DataBind();
                        gvCancelled.HeaderRow.TableSection = TableRowSection.TableHeader;
                        lblCancelledCall.Text = Convert.ToString(dtCancelled.Rows.Count);
                        Session["dtCancelled"] = dtCancelled;
                    }
                    else
                    {
                        gvCancelled.DataSource = string.Empty;
                        gvCancelled.DataBind();
                        lblCancelledCall.Text = "0";
                        Session["dtCancelled"] = dtCancelled;
                    }

                    DataTable dtReassign = new DataTable();
                    dtReassign = objMainClass.GetLeadData(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, 0, Convert.ToInt32(ddlAgent.SelectedValue), 0, 0, "RPTREASSIGN");
                    if (dtReassign.Rows.Count > 0)
                    {
                        gvReassign.DataSource = dtReassign;
                        gvReassign.DataBind();
                        gvReassign.HeaderRow.TableSection = TableRowSection.TableHeader;
                        lblReassignCall.Text = Convert.ToString(dtReassign.Rows.Count);
                        Session["dtReassign"] = dtReassign;
                    }
                    else
                    {
                        gvReassign.DataSource = string.Empty;
                        gvReassign.DataBind();
                        lblReassignCall.Text = "0";
                        Session["dtReassign"] = dtReassign;
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

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {
                    GetData();
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

        protected void lnkNewLead_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {
                    divLead.Visible = true;
                    divFollowUp.Visible = false;
                    divSO.Visible = false;
                    divCancelled.Visible = false;
                    divSOINQ.Visible = false;
                    divReassign.Visible = false;
                    divPending.Visible = false;
                    if (gvNewLead.Rows.Count > 0)
                    {
                        gvNewLead.HeaderRow.TableSection = TableRowSection.TableHeader;

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

        protected void lnkFollowUp_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {
                    divLead.Visible = false;
                    divFollowUp.Visible = true;
                    divSO.Visible = false;
                    divCancelled.Visible = false;
                    divSOINQ.Visible = false;
                    divReassign.Visible = false;
                    divPending.Visible = false;
                    if (gvFollowUps.Rows.Count > 0)
                    {
                        gvFollowUps.HeaderRow.TableSection = TableRowSection.TableHeader;

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

        protected void lnkConvertedCall_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {
                    divLead.Visible = false;
                    divFollowUp.Visible = false;
                    divSO.Visible = true;
                    divCancelled.Visible = false;
                    divSOINQ.Visible = false;
                    divReassign.Visible = false;
                    divPending.Visible = false;
                    if (gvSO.Rows.Count > 0)
                    {
                        gvSO.HeaderRow.TableSection = TableRowSection.TableHeader;

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

        protected void lnkCancelledCall_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {
                    divLead.Visible = false;
                    divFollowUp.Visible = false;
                    divSO.Visible = false;
                    divCancelled.Visible = true;
                    divSOINQ.Visible = false;
                    divReassign.Visible = false;
                    divPending.Visible = false;
                    if (gvCancelled.Rows.Count > 0)
                    {
                        gvCancelled.HeaderRow.TableSection = TableRowSection.TableHeader;

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

        protected void lnkSOGene_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {
                    divLead.Visible = false;
                    divFollowUp.Visible = false;
                    divSO.Visible = false;
                    divCancelled.Visible = false;
                    divSOINQ.Visible = true;
                    divReassign.Visible = false;
                    divPending.Visible = false;
                    if (gvSOINQ.Rows.Count > 0)
                    {
                        gvSOINQ.HeaderRow.TableSection = TableRowSection.TableHeader;

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

        protected void lnkReassignCall_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {
                    divLead.Visible = false;
                    divFollowUp.Visible = false;
                    divSO.Visible = false;
                    divCancelled.Visible = false;
                    divSOINQ.Visible = false;
                    divReassign.Visible = true;
                    divPending.Visible = false;
                    if (gvReassign.Rows.Count > 0)
                    {
                        gvReassign.HeaderRow.TableSection = TableRowSection.TableHeader;

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

        protected void lnkNewLeadDwn_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {
                    string attachment = "attachment; filename=NewLeadList.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vdn.ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    gvNewLead.RenderControl(htw);
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

        protected void lnkFollowUpDwn_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {
                    string attachment = "attachment; filename=NewLeadList.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vdn.ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    gvFollowUps.RenderControl(htw);
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

        protected void lnkConvertedDwn_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {
                    string attachment = "attachment; filename=NewLeadList.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vdn.ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    gvSO.RenderControl(htw);
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

        protected void lnkCancelledDwn_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {
                    string attachment = "attachment; filename=NewLeadList.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vdn.ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    gvCancelled.RenderControl(htw);
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

        protected void lnkInqSODwn_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {
                    string attachment = "attachment; filename=NewLeadList.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vdn.ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    gvSOINQ.RenderControl(htw);
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

        protected void lnkReassignDwn_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {
                    string attachment = "attachment; filename=NewLeadList.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vdn.ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    gvReassign.RenderControl(htw);
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

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void lnkDownLoadAll_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {
                    //DataTable dtNewLead = objMainClass.GetDataTable(gvNewLead, 1);
                    //DataTable dtFollowUps = objMainClass.GetDataTable(gvFollowUps, 1);
                    //DataTable dtSO = objMainClass.GetDataTable(gvSO, 1);
                    //DataTable dtSOINQ = objMainClass.GetDataTable(gvSOINQ, 1);
                    //DataTable dtPending = objMainClass.GetDataTable(gvPending, 1);
                    //DataTable dtCancelled = objMainClass.GetDataTable(gvCancelled, 1);
                    //DataTable dtReassign = objMainClass.GetDataTable(gvReassign, 1);


                    DataTable dtNewLead = (DataTable)Session["dtNewLead"];
                    DataTable dtFollowUps = (DataTable)Session["dtFollowUps"];
                    DataTable dtSO = (DataTable)Session["dtSO"];
                    DataTable dtSOINQ = (DataTable)Session["dtINQSO"];
                    DataTable dtPending = (DataTable)Session["dtPending"];
                    DataTable dtCancelled = (DataTable)Session["dtCancelled"];
                    DataTable dtReassign = (DataTable)Session["dtReassign"];

                    DataSet ds = new DataSet();
                    if (dtNewLead.Rows.Count > 0)
                    {
                        dtNewLead.TableName = "Lead Genereated";
                        ds.Tables.Add(dtNewLead);
                    }
                    if (dtFollowUps.Rows.Count > 0)
                    {
                        dtFollowUps.TableName = "Follow Up Calls";
                        ds.Tables.Add(dtFollowUps);
                    }
                    if (dtSO.Rows.Count > 0)
                    {
                        dtSO.TableName = "SO Generated";
                        ds.Tables.Add(dtSO);
                    }
                    if (dtSOINQ.Rows.Count > 0)
                    {
                        dtSOINQ.TableName = "INQ Generated";
                        ds.Tables.Add(dtSOINQ);
                    }
                    if (dtPending.Rows.Count > 0)
                    {
                        dtPending.TableName = "Pending Calls";
                        ds.Tables.Add(dtPending);
                    }
                    if (dtCancelled.Rows.Count > 0)
                    {
                        dtCancelled.TableName = "Cancelled Calls";
                        ds.Tables.Add(dtCancelled);
                    }
                    if (dtReassign.Rows.Count > 0)
                    {
                        dtReassign.TableName = "Re-Assign Calls";
                        ds.Tables.Add(dtReassign);
                    }



                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        foreach (DataTable dt in ds.Tables)
                        {
                            wb.Worksheets.Add(dt);
                        }

                        //Export the Excel file.
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=CallLogs.xlsx");
                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
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

        protected void lnkFolloupHistory_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {


                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    hfHisID.Value = ((Label)grdrow.FindControl("lblID")).Text;

                    DataTable dt = new DataTable();
                    dt = objMainClass.GetLeadStatusData(objMainClass.intCmpId, Convert.ToInt32(Session["USERID"]), (int)LeadStatus.Cancelled, "", "", "HISTORY", Convert.ToInt32(hfHisID.Value));
                    if (dt.Rows.Count > 0)
                    {

                        lblHisContactNo.Text = Convert.ToString(dt.Rows[0]["CONTACTNO"]);
                        lblHisCurrStatus.Text = Convert.ToString(dt.Rows[0]["STATUSDESC"]);
                        lblHisCustName.Text = Convert.ToString(dt.Rows[0]["CUSTNAME"]);
                        lblHisMail.Text = Convert.ToString(dt.Rows[0]["EMAIL"]);
                        lblHisMake.Text = Convert.ToString(dt.Rows[0]["MAKE"]);
                        lblHisModel.Text = Convert.ToString(dt.Rows[0]["MODEL"]);


                        gvDetail.DataSource = dt;
                        gvDetail.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-CallHistory').modal();", true);
                    }
                    else
                    {
                        gvDetail.DataSource = string.Empty;
                        gvDetail.DataBind();
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

        protected void lnkReassign_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {


                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    hfHisID.Value = ((Label)grdrow.FindControl("lblLEADID")).Text;

                    DataTable dt = new DataTable();
                    dt = objMainClass.GetLeadStatusData(objMainClass.intCmpId, Convert.ToInt32(Session["USERID"]), (int)LeadStatus.Cancelled, "", "", "HISTORY", Convert.ToInt32(hfHisID.Value));
                    if (dt.Rows.Count > 0)
                    {

                        lblHisContactNo.Text = Convert.ToString(dt.Rows[0]["CONTACTNO"]);
                        lblHisCurrStatus.Text = Convert.ToString(dt.Rows[0]["STATUSDESC"]);
                        lblHisCustName.Text = Convert.ToString(dt.Rows[0]["CUSTNAME"]);
                        lblHisMail.Text = Convert.ToString(dt.Rows[0]["EMAIL"]);
                        lblHisMake.Text = Convert.ToString(dt.Rows[0]["MAKE"]);
                        lblHisModel.Text = Convert.ToString(dt.Rows[0]["MODEL"]);


                        gvDetail.DataSource = dt;
                        gvDetail.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-CallHistory').modal();", true);
                    }
                    else
                    {
                        gvDetail.DataSource = string.Empty;
                        gvDetail.DataBind();
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

        protected void lnkSODwn_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {
                    string attachment = "attachment; filename=SOList.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vdn.ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    gvSO.RenderControl(htw);
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

        protected void lnkSO_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {
                    divLead.Visible = false;
                    divFollowUp.Visible = false;
                    divSO.Visible = true;
                    divCancelled.Visible = false;
                    divSOINQ.Visible = false;
                    divReassign.Visible = false;
                    if (gvSO.Rows.Count > 0)
                    {
                        gvSO.HeaderRow.TableSection = TableRowSection.TableHeader;

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

        protected void lnkPending_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {
                    divLead.Visible = false;
                    divFollowUp.Visible = false;
                    divSO.Visible = false;
                    divCancelled.Visible = false;
                    divSOINQ.Visible = false;
                    divReassign.Visible = false;
                    divPending.Visible = true;
                    if (gvPending.Rows.Count > 0)
                    {
                        gvPending.HeaderRow.TableSection = TableRowSection.TableHeader;

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

        protected void lnkPendingDwn_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {
                    string attachment = "attachment; filename=PendingList.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vdn.ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    gvPending.RenderControl(htw);
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
    }
}