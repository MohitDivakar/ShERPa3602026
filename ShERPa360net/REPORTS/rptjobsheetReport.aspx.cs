using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.REPORTS
{
    public partial class rptjobsheetReport : System.Web.UI.Page
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
                        if (FormRights.bView == false)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        int month = DateTime.Now.Month;
                        int year = DateTime.Now.Year;
                        DateTime date = DateTime.Now;
                        string firstDayOfMonth = new DateTime(date.Year, date.Month, 1).ToShortDateString();

                        txtFromDate.Text = firstDayOfMonth;// "01-" + month + "-" + year;//objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();

                        objBindDDL.FillSegment(ddlSegment);
                        //string[] segmentArray = Convert.ToString(Session["SEGMENT"]).Split(',');
                        ddlSegment.SelectedValue = "1043";

                        objBindDDL.FillPlant(ddlPLant);
                        //string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                        //ddlPLant.SelectedValue = plantArray[0];

                        objBindDDL.FillStatus(ddlStatus, 2);

                        FillData();
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

        public void FillData()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    string strFromDt = txtFromDate.Text;
                    string strToDt = txtToDate.Text;

                    DataTable dt = new DataTable();
                    dt = objMainClass.GetJobsheetCount(objMainClass.intCmpId, 2, ddlStatus.SelectedIndex > 0 ? Convert.ToInt32(ddlStatus.SelectedValue) : 0, strFromDt, strToDt, 
                        ddlSegment.SelectedIndex == 0 ? Convert.ToString(Session["SEGMENT"]) : ddlSegment.SelectedValue, ddlPLant.SelectedIndex == 0 ? Convert.ToString(Session["PLANTCD"]) : ddlPLant.SelectedValue, "REPORTCOUNTNEW", 0, 0);

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

                    if (ddlSegment.SelectedIndex > 0)
                    {
                        ViewState["JSSegment"] = ddlSegment.SelectedValue;
                    }
                    else
                    {
                        ViewState["JSSegment"] = "";
                    }

                    if (ddlStatus.SelectedIndex > 0)
                    {
                        ViewState["JSStatus"] = ddlStatus.SelectedValue;
                    }
                    else
                    {
                        ViewState["JSStatus"] = "";
                    }

                    if (ddlPLant.SelectedIndex > 0)
                    {
                        ViewState["JSPlant"] = ddlPLant.SelectedValue;
                    }
                    else
                    {
                        ViewState["JSPlant"] = "";
                    }
                    ViewState["JSFromDate"] = strFromDt;
                    ViewState["JSToDate"] = strToDt;

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message.Replace("\r\n", "") + "\");", true);
            }
        }

        protected void lnkSearh_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    FillData();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message.Replace("\r\n", "") + "\");", true);
            }
        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (e.CommandName == "LESSTHANTHREEDAYS")
                    {
                        GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                        if (row != null)
                        {
                            Label lblSTATUSID = (Label)row.FindControl("lblSTATUSID");
                            Label lblSTATUS = (Label)row.FindControl("lblSTATUS");
                            string PLANT = Convert.ToString(ViewState["JSPlant"]);
                            string SEGMENT = Convert.ToString(ViewState["JSSegment"]);
                            string STATUS = Convert.ToString(ViewState["JSStatus"]);
                            string strFromDt = Convert.ToString(ViewState["JSFromDate"]);
                            string strToDt = Convert.ToString(ViewState["JSToDate"]);
                            string path = "rptJobsheetData.aspx";

                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "?PLANT=" + PLANT + "&SEGMENT=" + SEGMENT + "&FROMDATE=" + strFromDt + "&TODATE=" + strToDt + "&HEADING=" + lblSTATUS.Text + "&FROMDAY=-1&TODAY=3&STATUSID=" + lblSTATUSID.Text + "');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found.!');", true);
                        }
                    }
                    else if (e.CommandName == "THREETOSEVENDAYS")
                    {
                        GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                        if (row != null)
                        {
                            Label lblSTATUSID = (Label)row.FindControl("lblSTATUSID");
                            Label lblSTATUS = (Label)row.FindControl("lblSTATUS");
                            string PLANT = Convert.ToString(ViewState["JSPlant"]);
                            string SEGMENT = Convert.ToString(ViewState["JSSegment"]);
                            string STATUS = Convert.ToString(ViewState["JSStatus"]);
                            string strFromDt = Convert.ToString(ViewState["JSFromDate"]);
                            string strToDt = Convert.ToString(ViewState["JSToDate"]);
                            string path = "rptJobsheetData.aspx";

                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "?PLANT=" + PLANT + "&SEGMENT=" + SEGMENT + "&FROMDATE=" + strFromDt + "&TODATE=" + strToDt + "&HEADING=" + lblSTATUS.Text + "&FROMDAY=3&TODAY=7&STATUSID=" + lblSTATUSID.Text + "');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found.!');", true);
                        }
                    }
                    else if (e.CommandName == "SEVENTOFIFTEENDAYS")
                    {
                        GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                        if (row != null)
                        {
                            Label lblSTATUSID = (Label)row.FindControl("lblSTATUSID");
                            Label lblSTATUS = (Label)row.FindControl("lblSTATUS");
                            string PLANT = Convert.ToString(ViewState["JSPlant"]);
                            string SEGMENT = Convert.ToString(ViewState["JSSegment"]);
                            string STATUS = Convert.ToString(ViewState["JSStatus"]);
                            string strFromDt = Convert.ToString(ViewState["JSFromDate"]);
                            string strToDt = Convert.ToString(ViewState["JSToDate"]);
                            string path = "rptJobsheetData.aspx";

                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "?PLANT=" + PLANT + "&SEGMENT=" + SEGMENT + "&FROMDATE=" + strFromDt + "&TODATE=" + strToDt + "&HEADING=" + lblSTATUS.Text + "&FROMDAY=7&TODAY=15&STATUSID=" + lblSTATUSID.Text + "');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found.!');", true);
                        }
                    }
                    else if (e.CommandName == "FIFTEENTOTHIRTYDAYS")
                    {
                        GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                        if (row != null)
                        {
                            Label lblSTATUSID = (Label)row.FindControl("lblSTATUSID");
                            Label lblSTATUS = (Label)row.FindControl("lblSTATUS");
                            string PLANT = Convert.ToString(ViewState["JSPlant"]);
                            string SEGMENT = Convert.ToString(ViewState["JSSegment"]);
                            string STATUS = Convert.ToString(ViewState["JSStatus"]);
                            string strFromDt = Convert.ToString(ViewState["JSFromDate"]);
                            string strToDt = Convert.ToString(ViewState["JSToDate"]);
                            string path = "rptJobsheetData.aspx";

                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "?PLANT=" + PLANT + "&SEGMENT=" + SEGMENT + "&FROMDATE=" + strFromDt + "&TODATE=" + strToDt + "&HEADING=" + lblSTATUS.Text + "&FROMDAY=15&TODAY=30&STATUSID=" + lblSTATUSID.Text + "');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found.!');", true);
                        }
                    }
                    else if (e.CommandName == "THIRTYTOSIXTYDAYS")
                    {
                        GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                        if (row != null)
                        {
                            Label lblSTATUSID = (Label)row.FindControl("lblSTATUSID");
                            Label lblSTATUS = (Label)row.FindControl("lblSTATUS");
                            string PLANT = Convert.ToString(ViewState["JSPlant"]);
                            string SEGMENT = Convert.ToString(ViewState["JSSegment"]);
                            string STATUS = Convert.ToString(ViewState["JSStatus"]);
                            string strFromDt = Convert.ToString(ViewState["JSFromDate"]);
                            string strToDt = Convert.ToString(ViewState["JSToDate"]);
                            string path = "rptJobsheetData.aspx";

                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "?PLANT=" + PLANT + "&SEGMENT=" + SEGMENT + "&FROMDATE=" + strFromDt + "&TODATE=" + strToDt + "&HEADING=" + lblSTATUS.Text + "&FROMDAY=30&TODAY=60&STATUSID=" + lblSTATUSID.Text + "');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found.!');", true);
                        }
                    }
                    else if (e.CommandName == "GREATERTHANSIXTY")
                    {
                        GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                        if (row != null)
                        {
                            Label lblSTATUSID = (Label)row.FindControl("lblSTATUSID");
                            Label lblSTATUS = (Label)row.FindControl("lblSTATUS");
                            string PLANT = Convert.ToString(ViewState["JSPlant"]);
                            string SEGMENT = Convert.ToString(ViewState["JSSegment"]);
                            string STATUS = Convert.ToString(ViewState["JSStatus"]);
                            string strFromDt = Convert.ToString(ViewState["JSFromDate"]);
                            string strToDt = Convert.ToString(ViewState["JSToDate"]);
                            string path = "rptJobsheetData.aspx";

                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "?PLANT=" + PLANT + "&SEGMENT=" + SEGMENT + "&FROMDATE=" + strFromDt + "&TODATE=" + strToDt + "&HEADING=" + lblSTATUS.Text + "&FROMDAY=60&TODAY=-1&STATUSID=" + lblSTATUSID.Text + "');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found.!');", true);
                        }
                    }
                    else if (e.CommandName == "TOTALJOBCOUNT")
                    {
                        GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                        if (row != null)
                        {
                            Label lblSTATUSID = (Label)row.FindControl("lblSTATUSID");
                            Label lblSTATUS = (Label)row.FindControl("lblSTATUS");
                            string PLANT = Convert.ToString(ViewState["JSPlant"]);
                            string SEGMENT = Convert.ToString(ViewState["JSSegment"]);
                            string STATUS = Convert.ToString(ViewState["JSStatus"]);
                            string strFromDt = Convert.ToString(ViewState["JSFromDate"]);
                            string strToDt = Convert.ToString(ViewState["JSToDate"]);
                            string path = "rptJobsheetData.aspx";

                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "?PLANT=" + PLANT + "&SEGMENT=" + SEGMENT + "&FROMDATE=" + strFromDt + "&TODATE=" + strToDt + "&HEADING=" + lblSTATUS.Text + "&FROMDAY=-1&TODAY=-1&STATUSID=" + lblSTATUSID.Text + "');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found.!');", true);
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

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message.Replace("\r\n", "") + "\");", true);
            }
        }

        protected void ddlPLant_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    string PLantRights = string.Empty;
                    string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                    for (int i = 0; i < plantArray.Count(); i++)
                    {
                        if (plantArray[i].Trim() == ddlPLant.SelectedValue)
                        {
                            PLantRights = ddlPLant.SelectedValue;
                        }
                    }

                    if (PLantRights.Length > 0)
                    {

                    }
                    else
                    {
                        ddlPLant.SelectedValue = plantArray[0];
                        ddlPLant.Focus();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have plant rights.');setTimeout(function(){  $('#modal-warning').modal('hide')}, 2000);", true);
                        ddlPLant.SelectedValue = plantArray[0];
                        ddlPLant.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void ddlSegment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    string SegmentRights = string.Empty;
                    string[] segmentArray = Convert.ToString(Session["SEGMENT"]).Split(',');
                    for (int i = 0; i < segmentArray.Count(); i++)
                    {
                        if (segmentArray[i].Trim() == ddlSegment.SelectedValue)
                        {
                            SegmentRights = ddlSegment.SelectedValue;
                        }
                    }

                    if (SegmentRights.Length > 0)
                    {

                    }
                    else
                    {
                        ddlSegment.SelectedValue = segmentArray[0];
                        ddlSegment.Focus();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have segment rights.');setTimeout(function(){  $('#modal-warning').modal('hide')}, 2000);", true);
                        ddlSegment.SelectedValue = segmentArray[0];
                        ddlSegment.Focus();
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