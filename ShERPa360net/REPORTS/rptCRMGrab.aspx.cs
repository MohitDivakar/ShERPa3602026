using ClosedXML.Excel;
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
    public partial class rptCRMGrab : System.Web.UI.Page
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

                        GetData();
                        divAllLead.Visible = true;

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
                    DataTable dtAllLead = new DataTable();
                    dtAllLead = objMainClass.GetLeadData(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, 0, 0, 0, 0, "GRABREPORT");
                    if (dtAllLead.Rows.Count > 0)
                    {
                        gvAllGrab.DataSource = dtAllLead;
                        gvAllGrab.DataBind();
                        gvAllGrab.HeaderRow.TableSection = TableRowSection.TableHeader;
                        lblAllGrab.Text = Convert.ToString(dtAllLead.Rows.Count);
                        Session["dtAllLead"] = dtAllLead;
                    }
                    else
                    {
                        gvAllGrab.DataSource = string.Empty;
                        gvAllGrab.DataBind();
                        lblAllGrab.Text = "0";
                        Session["dtAllLead"] = dtAllLead;
                    }


                    DataTable dtGrabbedDate = new DataTable();
                    dtGrabbedDate = objMainClass.GetLeadData(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, 0, 0, 0, 0, "GRABBEDREPORTDATE");
                    if (dtGrabbedDate.Rows.Count > 0)
                    {
                        gvGrabDate.DataSource = dtGrabbedDate;
                        gvGrabDate.DataBind();
                        gvGrabDate.HeaderRow.TableSection = TableRowSection.TableHeader;
                        lblGrabByDate.Text = Convert.ToString(dtGrabbedDate.Rows.Count);
                        lblGrabbedOutOf.Text = " / " + Convert.ToString(dtAllLead.Rows.Count);
                        Session["dtGrabbedDate"] = dtGrabbedDate;
                    }
                    else
                    {
                        gvGrabDate.DataSource = string.Empty;
                        gvGrabDate.DataBind();
                        lblGrabByDate.Text = "0";
                        lblGrabbedOutOf.Text = " / " + Convert.ToString(dtAllLead.Rows.Count);
                        Session["dtGrabbedDate"] = dtGrabbedDate;
                    }



                    DataTable dtGrabbed = new DataTable();
                    dtGrabbed = objMainClass.GetLeadData(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, 0, 0, 0, 0, "GRABBEDREPORT");
                    if (dtGrabbed.Rows.Count > 0)
                    {
                        gvGrabbedByAgent.DataSource = dtGrabbed;
                        gvGrabbedByAgent.DataBind();
                        gvGrabbedByAgent.HeaderRow.TableSection = TableRowSection.TableHeader;
                        lblGrabBy.Text = Convert.ToString(dtGrabbed.Rows.Count);
                        Session["dtGrabbed"] = dtGrabbed;
                    }
                    else
                    {
                        gvGrabbedByAgent.DataSource = string.Empty;
                        gvGrabbedByAgent.DataBind();
                        lblGrabBy.Text = "0";
                        Session["dtGrabbed"] = dtGrabbed;
                    }
                    //gvPending

                    DataTable dtPending = new DataTable();
                    dtPending = objMainClass.GetLeadData(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, 0, 0, 0, 0, "GRABPENDING");
                    if (dtPending.Rows.Count > 0)
                    {
                        gvGrabPending.DataSource = dtPending;
                        gvGrabPending.DataBind();
                        gvGrabPending.HeaderRow.TableSection = TableRowSection.TableHeader;
                        lblPending.Text = Convert.ToString(dtPending.Rows.Count);
                        lblPendingOutOf.Text = " / " + Convert.ToString(dtAllLead.Rows.Count);
                        Session["dtPending"] = dtPending;
                    }
                    else
                    {
                        gvGrabPending.DataSource = string.Empty;
                        gvGrabPending.DataBind();
                        lblPending.Text = "0";
                        lblPendingOutOf.Text = " / " + Convert.ToString(dtAllLead.Rows.Count);
                        Session["dtPending"] = dtPending;
                    }

                    DataTable dtGrabSummary = new DataTable();
                    dtGrabSummary = objMainClass.GetGrabSummary(objMainClass.intCmpId, 1, txtFromDate.Text, txtToDate.Text, "GRABSUMMARY");
                    if (dtGrabSummary.Rows.Count > 0)
                    {
                        gvGrabbSummary.DataSource = dtGrabSummary;
                        gvGrabbSummary.DataBind();
                        gvGrabbSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    else
                    {
                        gvGrabbSummary.DataSource = string.Empty;
                        gvGrabbSummary.DataBind();
                    }


                    DataTable dtCancelled = new DataTable();
                    dtCancelled = objMainClass.GetLeadData(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, 0, 0, 0, 0, "GRABCANCELLED");
                    if (dtCancelled.Rows.Count > 0)
                    {
                        grvCancelled.DataSource = dtCancelled;
                        grvCancelled.DataBind();
                        grvCancelled.HeaderRow.TableSection = TableRowSection.TableHeader;
                        lblCancelledCall.Text = Convert.ToString(dtCancelled.Rows.Count);
                        lblCancelledOutOf.Text = " / " + Convert.ToString(dtAllLead.Rows.Count);
                        Session["dtCancelled"] = dtCancelled;
                    }
                    else
                    {
                        grvCancelled.DataSource = string.Empty;
                        grvCancelled.DataBind();
                        lblCancelledCall.Text = "0";
                        lblCancelledOutOf.Text = " / " + Convert.ToString(dtAllLead.Rows.Count);
                        Session["dtCancelled"] = dtCancelled;
                    }

                    DataTable dtAllPending = new DataTable();
                    dtAllPending = objMainClass.GetLeadData(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, 0, 0, 0, 0, "GRABALLPENDING");
                    if (dtAllPending.Rows.Count > 0)
                    {
                        grvAllPending.DataSource = dtAllPending;
                        grvAllPending.DataBind();
                        grvAllPending.HeaderRow.TableSection = TableRowSection.TableHeader;
                        lblAllGrabPending.Text = Convert.ToString(dtAllPending.Rows.Count);
                        Session["dtAllPending"] = dtAllPending;
                    }
                    else
                    {
                        grvAllPending.DataSource = string.Empty;
                        grvAllPending.DataBind();
                        lblAllGrabPending.Text = "0";
                        Session["dtAllPending"] = dtAllPending;
                    }

                    DataTable dtAllCancelled = new DataTable();
                    dtAllCancelled = objMainClass.GetLeadData(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, 0, 0, 0, 0, "ALLGRABCANCELLED");
                    if (dtAllCancelled.Rows.Count > 0)
                    {
                        grvAllCancelled.DataSource = dtAllCancelled;
                        grvAllCancelled.DataBind();
                        grvAllCancelled.HeaderRow.TableSection = TableRowSection.TableHeader;
                        lblAllCancelled.Text = Convert.ToString(dtAllCancelled.Rows.Count);
                        Session["dtAllCancelled"] = dtAllCancelled;
                    }
                    else
                    {
                        grvAllCancelled.DataSource = string.Empty;
                        grvAllCancelled.DataBind();
                        lblAllGrabPending.Text = "0";
                        Session["dtAllCancelled"] = dtAllCancelled;
                    }

                    DataTable dtConverted = new DataTable();
                    dtConverted = objMainClass.GetLeadData(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, 0, 0, 0, 0, "GRABCONVERTED");
                    if (dtConverted.Rows.Count > 0)
                    {
                        gvSO.DataSource = dtConverted;
                        gvSO.DataBind();
                        gvSO.HeaderRow.TableSection = TableRowSection.TableHeader;
                        lblConvertedCall.Text = Convert.ToString(dtConverted.Rows.Count);
                        lblConvertedOutOf.Text = " / " + Convert.ToString(dtGrabbedDate.Rows.Count);
                        Session["dtConverted"] = dtConverted;
                    }
                    else
                    {
                        gvSO.DataSource = string.Empty;
                        gvSO.DataBind();
                        lblConvertedCall.Text = "0";
                        lblConvertedOutOf.Text = " / " + Convert.ToString(dtGrabbedDate.Rows.Count);
                        Session["dtConverted"] = dtConverted;
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

        protected void lnkDownLoadAll_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {
                    DataTable dtAllLead = (DataTable)Session["dtAllLead"];
                    DataTable dtGrabbed = (DataTable)Session["dtGrabbed"];
                    DataTable dtPending = (DataTable)Session["dtPending"];
                    DataTable dtGrabbedDate = (DataTable)Session["dtGrabbedDate"];
                    DataTable dtCancelled = (DataTable)Session["dtCancelled"];
                    DataTable dtAllPending = (DataTable)Session["dtAllPending"];
                    DataTable dtAllCancelled = (DataTable)Session["dtAllCancelled"];
                    DataTable dtConverted = (DataTable)Session["dtConverted"];


                    DataSet ds = new DataSet();
                    if (dtAllLead.Rows.Count > 0)
                    {
                        dtAllLead.TableName = "Lead Genereated";
                        ds.Tables.Add(dtAllLead);
                    }
                    if (dtGrabbedDate.Rows.Count > 0)
                    {
                        dtGrabbedDate.TableName = "Follow Up Calls";
                        ds.Tables.Add(dtGrabbedDate);
                    }
                    if (dtGrabbed.Rows.Count > 0)
                    {
                        dtGrabbed.TableName = "Ttoal Grabbed By Agent";
                        ds.Tables.Add(dtGrabbed);
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
                    if (dtAllPending.Rows.Count > 0)
                    {
                        dtAllPending.TableName = "All Pending Calls";
                        ds.Tables.Add(dtAllPending);
                    }
                    if (dtAllCancelled.Rows.Count > 0)
                    {
                        dtAllCancelled.TableName = "All Cancelled Calls";
                        ds.Tables.Add(dtAllCancelled);
                    }
                    if (dtConverted.Rows.Count > 0)
                    {
                        dtConverted.TableName = "Converted Calls";
                        ds.Tables.Add(dtConverted);
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
                        Response.AddHeader("content-disposition", "attachment;filename=GrabCallLogs" + DateTime.Now + ".xlsx");
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

        protected void lnkAllGrab_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {
                    divAllLead.Visible = true;
                    divGrabbed.Visible = false;
                    divPending.Visible = false;
                    divGrabbedDate.Visible = false;
                    divCancelled.Visible = false;
                    divAllPending.Visible = false;
                    divAllCancelled.Visible = false;
                    divSO.Visible = false;
                    if (gvAllGrab.Rows.Count > 0)
                    {
                        gvAllGrab.HeaderRow.TableSection = TableRowSection.TableHeader;

                    }
                    if (gvGrabbSummary.Rows.Count > 0)
                    {
                        gvGrabbSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
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

        protected void lnkGrabBy_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {
                    divAllLead.Visible = false;
                    divGrabbed.Visible = true;
                    divPending.Visible = false;
                    divGrabbedDate.Visible = false;
                    divCancelled.Visible = false;
                    divAllPending.Visible = false;
                    divAllCancelled.Visible = false;
                    divSO.Visible = false;
                    if (gvGrabbedByAgent.Rows.Count > 0)
                    {
                        gvGrabbedByAgent.HeaderRow.TableSection = TableRowSection.TableHeader;

                    }
                    if (gvGrabbSummary.Rows.Count > 0)
                    {
                        gvGrabbSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
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
                    divAllLead.Visible = false;
                    divGrabbed.Visible = false;
                    divPending.Visible = true;
                    divGrabbedDate.Visible = false;
                    divCancelled.Visible = false;
                    divAllPending.Visible = false;
                    divAllCancelled.Visible = false;
                    divSO.Visible = false;
                    if (gvGrabPending.Rows.Count > 0)
                    {
                        gvGrabPending.HeaderRow.TableSection = TableRowSection.TableHeader;

                    }
                    if (gvGrabbSummary.Rows.Count > 0)
                    {
                        gvGrabbSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
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

        protected void lnkNewAllGenerated_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {
                    string attachment = "attachment; filename=AllGrabGenerated" + DateTime.Now + ".xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vdn.ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    gvAllGrab.RenderControl(htw);
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

        protected void lnkNewGrabbedBy_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {
                    string attachment = "attachment; filename=GrabbedByAgent" + DateTime.Now + ".xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vdn.ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    gvGrabbedByAgent.RenderControl(htw);
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

        protected void lnkNewPending_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {
                    string attachment = "attachment; filename=GrabbedPending" + DateTime.Now + ".xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vdn.ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    gvGrabPending.RenderControl(htw);
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

        protected void lnkGrabByDate_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {
                    divAllLead.Visible = false;
                    divGrabbed.Visible = false;
                    divPending.Visible = false;
                    divGrabbedDate.Visible = true;
                    divCancelled.Visible = false;
                    divAllPending.Visible = false;
                    divAllCancelled.Visible = false;
                    divSO.Visible = false;
                    if (gvGrabDate.Rows.Count > 0)
                    {
                        gvGrabDate.HeaderRow.TableSection = TableRowSection.TableHeader;

                    }
                    if (gvGrabbSummary.Rows.Count > 0)
                    {
                        gvGrabbSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
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

        protected void lnkNewGrabbedDate_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {
                    string attachment = "attachment; filename=GrabbedByDate" + DateTime.Now + ".xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vdn.ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    gvGrabDate.RenderControl(htw);
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

        protected void lnkCancelledCall_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {
                    divAllLead.Visible = false;
                    divGrabbed.Visible = false;
                    divPending.Visible = false;
                    divGrabbedDate.Visible = false;
                    divCancelled.Visible = true;
                    divAllPending.Visible = false;
                    divAllCancelled.Visible = false;
                    divSO.Visible = false;
                    if (grvCancelled.Rows.Count > 0)
                    {
                        grvCancelled.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    if (gvGrabbSummary.Rows.Count > 0)
                    {
                        gvGrabbSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
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

        protected void lnkDateCancelled_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {
                    string attachment = "attachment; filename=Cancelled" + DateTime.Now + ".xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vdn.ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    grvCancelled.RenderControl(htw);
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

        protected void lblAllPending_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {
                    string attachment = "attachment; filename=AllPending" + DateTime.Now + ".xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vdn.ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    grvAllPending.RenderControl(htw);
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

        protected void lnkAllPending_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {
                    divAllLead.Visible = false;
                    divGrabbed.Visible = false;
                    divPending.Visible = false;
                    divGrabbedDate.Visible = false;
                    divCancelled.Visible = false;
                    divAllPending.Visible = true;
                    divAllCancelled.Visible = false;
                    divSO.Visible = false;
                    if (grvAllPending.Rows.Count > 0)
                    {
                        grvAllPending.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    if (gvGrabbSummary.Rows.Count > 0)
                    {
                        gvGrabbSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
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

        protected void lnkAllCancelled_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {
                    string attachment = "attachment; filename=AllCancelled" + DateTime.Now + ".xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vdn.ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    grvAllCancelled.RenderControl(htw);
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

        protected void lnkAllCancelledCall_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {
                    divAllLead.Visible = false;
                    divGrabbed.Visible = false;
                    divPending.Visible = false;
                    divGrabbedDate.Visible = false;
                    divCancelled.Visible = false;
                    divAllPending.Visible = false;
                    divAllCancelled.Visible = true;
                    divSO.Visible = false;
                    if (grvAllCancelled.Rows.Count > 0)
                    {
                        grvAllCancelled.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    if (gvGrabbSummary.Rows.Count > 0)
                    {
                        gvGrabbSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
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
                    string attachment = "attachment; filename=Converted" + DateTime.Now + ".xls";
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

        protected void lnkConverted_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {
                    divAllLead.Visible = false;
                    divGrabbed.Visible = false;
                    divPending.Visible = false;
                    divGrabbedDate.Visible = false;
                    divCancelled.Visible = false;
                    divAllPending.Visible = false;
                    divAllCancelled.Visible = false;
                    divSO.Visible = true;
                    if (gvSO.Rows.Count > 0)
                    {
                        gvSO.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    if (gvGrabbSummary.Rows.Count > 0)
                    {
                        gvGrabbSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
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