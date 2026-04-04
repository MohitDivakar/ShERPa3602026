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
    public partial class rptCavitakReport : System.Web.UI.Page
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
                    if (Session["USERID"] != null)
                    {

                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menutabid.Value, "");
                        if (FormRights.bView == false)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                            return;
                        }
                        //lblUserName.InnerText = Convert.ToString(Session["USERNAME"]);
                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();
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

        protected void lnkSearch_Click(object sender, EventArgs e)
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


        public void BindData()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetCavitakProject(objMainClass.intCmpId, "1044", txtFromDate.Text, txtToDate.Text, "0", "SELECTJOB");
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

                    DataTable dtProductionCount = new DataTable();
                    dtProductionCount = objMainClass.GetCavitakProject(objMainClass.intCmpId, "1044", "", "", "0", "PRODUCTIONCOUNT");
                    if (dtProductionCount.Rows.Count > 0)
                    {
                        gvCards.DataSource = dtProductionCount;
                        gvCards.DataBind();
                    }
                    else
                    {
                        gvCards.DataSource = string.Empty;
                        gvCards.DataBind();
                    }

                    DataTable dtProductionCountToday = new DataTable();
                    dtProductionCountToday = objMainClass.GetCavitakProject(objMainClass.intCmpId, "1044", "", "", "0", "TODAYPRODUCTIONCOUNT");
                    if (dtProductionCountToday.Rows.Count > 0)
                    {
                        gvCardsToday.DataSource = dtProductionCountToday;
                        gvCardsToday.DataBind();
                    }
                    else
                    {
                        gvCardsToday.DataSource = string.Empty;
                        gvCardsToday.DataBind();
                    }


                    DataTable dtTodayInward = new DataTable();
                    dtTodayInward = objMainClass.GetCavitakProject(objMainClass.intCmpId, "1044", "", "", "0", "TODAYSUMMARY");
                    if (dtTodayInward.Rows.Count > 0)
                    {
                        grvTodayInwardSummary.DataSource = dtTodayInward;
                        grvTodayInwardSummary.DataBind();
                        grvTodayInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;

                        //decimal total = dtTodayInward.AsEnumerable().Sum(row => Convert.ToDecimal(row.Field<int>("Count")));
                        //grvTodayInwardSummary.FooterRow.Cells[2].Text = total.ToString("N0");
                        //grvTodayInwardSummary.FooterRow.Cells[1].Text = "Total";
                    }
                    else
                    {
                        grvTodayInwardSummary.DataSource = string.Empty;
                        grvTodayInwardSummary.DataBind();
                    }

                    DataTable dtOverAllInward = new DataTable();
                    dtOverAllInward = objMainClass.GetCavitakProject(objMainClass.intCmpId, "1044", "", "", "0", "TOTALSUMMARY");
                    if (dtOverAllInward.Rows.Count > 0)
                    {
                        grvOverallInwardSummary.DataSource = dtOverAllInward;
                        grvOverallInwardSummary.DataBind();
                        grvOverallInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;

                        //decimal total = dtOverAllInward.AsEnumerable().Sum(row => Convert.ToDecimal(row.Field<int>("Count")));
                        //grvOverallInwardSummary.FooterRow.Cells[2].Text = total.ToString("N0");
                        //grvOverallInwardSummary.FooterRow.Cells[1].Text = "Total";
                    }
                    else
                    {
                        grvOverallInwardSummary.DataSource = string.Empty;
                        grvOverallInwardSummary.DataBind();
                    }

                    DataTable dtDashboard = new DataTable();
                    dtDashboard = objMainClass.GetCavitakProject(objMainClass.intCmpId, "1044", "", "", "0", "DASHBOARDCOUNT");
                    if (dtDashboard.Rows.Count > 0)
                    {
                        lblTodayInward.Text = Convert.ToString(dtDashboard.Rows[0]["Count"]);
                        lblTodayDispatch.Text = Convert.ToString(dtDashboard.Rows[1]["Count"]);
                        lblTodayDelivered.Text = Convert.ToString(dtDashboard.Rows[2]["Count"]);
                        lblTotalInward.Text = Convert.ToString(dtDashboard.Rows[4]["Count"]);
                        lblTotalDispatched.Text = Convert.ToString(dtDashboard.Rows[5]["Count"]);
                        lblTotalDelivered.Text = Convert.ToString(dtDashboard.Rows[6]["Count"]);
                        lblTotalPending.Text = Convert.ToString(dtDashboard.Rows[7]["Count"]);
                    }
                    else
                    {
                        lblTodayInward.Text = "0";
                        lblTodayDispatch.Text = "0";
                        lblTodayDelivered.Text = "0";
                        lblTotalInward.Text = "0";
                        lblTotalDispatched.Text = "0";
                        lblTotalDelivered.Text = "0";
                        lblTotalPending.Text = "0";
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

        protected void lnkInv_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

                    string lblJobID = ((Label)grdrow.FindControl("lblJobID")).Text;
                    DataTable dtInv = new DataTable();
                    dtInv = objMainClass.GetInvData(objMainClass.intCmpId, lblJobID, "GETRCVDINVBYJOBID");

                    if (dtInv.Rows.Count > 0)
                    {
                        grInvRcvd.DataSource = dtInv;
                        grInvRcvd.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#InventoryModal').modal();", true);
                    }
                    else
                    {
                        grInvRcvd.DataSource = string.Empty;
                        grInvRcvd.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No record found.!');", true);
                    }

                    gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    grvTodayInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                    grvOverallInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
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

        protected void lnkQC_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

                    string lblJobID = ((Label)grdrow.FindControl("lblJobID")).Text;
                    string lblBrand = ((Label)grdrow.FindControl("lblBrand")).Text;
                    string lblModel = ((Label)grdrow.FindControl("lblModel")).Text;

                    DataTable dtQC = new DataTable();
                    dtQC = objMainClass.GetQCData(objMainClass.intCmpId, lblJobID, "GETQCPARAMETER");

                    if (dtQC.Rows.Count > 0)
                    {
                        grvQCPara.DataSource = dtQC;
                        grvQCPara.DataBind();
                        lblPopJobid.Text = lblJobID;
                        lblPopMake.Text = lblBrand;
                        lblPopModel.Text = lblModel;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#QCParaModal').modal();", true);
                    }
                    else
                    {
                        grvQCPara.DataSource = string.Empty;
                        grvQCPara.DataBind();
                        lblPopJobid.Text = string.Empty;
                        lblPopMake.Text = string.Empty;
                        lblPopModel.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No record found.!');", true);
                    }

                    gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    grvTodayInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                    grvOverallInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                grvTodayInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                grvOverallInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lnkCard_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    Label lblNewStageID = (Label)grdrow.FindControl("lblNewStageID");
                    Label lblNewStageName = (Label)grdrow.FindControl("lblNewStageName");

                    DataTable dt = new DataTable();
                    dt = objMainClass.GetCavitakProject(objMainClass.intCmpId, "1044", txtFromDate.Text,
                        txtToDate.Text, lblNewStageID.Text == "" || lblNewStageID.Text == null || lblNewStageID.Text == string.Empty ? "" : lblNewStageID.Text, "TODAYSPRODUCTIONDETAILS");
                    if (dt.Rows.Count > 0)
                    {
                        grdPopDetails.DataSource = dt;
                        grdPopDetails.DataBind();
                        grdPopDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                        popHeading.Text = "Today's " + lblNewStageName.Text + " Details";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#DashBoardDetails').modal();", true);
                    }
                    else
                    {
                        grdPopDetails.DataSource = string.Empty;
                        grdPopDetails.DataBind();

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Record Not Found.!');", true);
                    }

                    gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    grvTodayInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                    grvOverallInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                grvTodayInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                grvOverallInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lnkCardAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    Label lblNewStageID = (Label)grdrow.FindControl("lblNewStageID");
                    Label lblNewStageName = (Label)grdrow.FindControl("lblNewStageName");

                    DataTable dt = new DataTable();
                    dt = objMainClass.GetCavitakProject(objMainClass.intCmpId, "1044", txtFromDate.Text, txtToDate.Text,
                        lblNewStageID.Text == "" || lblNewStageID.Text == null || lblNewStageID.Text == string.Empty ? "" : lblNewStageID.Text,
                        "ALLPRODUCTIONDETAILS");
                    if (dt.Rows.Count > 0)
                    {
                        grdPopDetails.DataSource = dt;
                        grdPopDetails.DataBind();
                        grdPopDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                        popHeading.Text = "Over All " + lblNewStageName.Text + " Details";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#DashBoardDetails').modal();", true);
                    }
                    else
                    {
                        grdPopDetails.DataSource = string.Empty;
                        grdPopDetails.DataBind();

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Record Not Found.!');", true);
                    }

                    gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    grvTodayInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                    grvOverallInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                grvTodayInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                grvOverallInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lnkTodayInward_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetCavitakProject(objMainClass.intCmpId, "1044", txtFromDate.Text, txtToDate.Text, "0", "TODAYSINWARDDETAILS");
                    if (dt.Rows.Count > 0)
                    {
                        grdPopDetails.DataSource = dt;
                        grdPopDetails.DataBind();
                        grdPopDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                        popHeading.Text = "Today's Inward Details";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#DashBoardDetails').modal();", true);
                    }
                    else
                    {
                        grdPopDetails.DataSource = string.Empty;
                        grdPopDetails.DataBind();

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Record Not Found.!');", true);
                    }

                    gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    grvTodayInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                    grvOverallInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {

                gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                grvTodayInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                grvOverallInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lnkTodayDispatch_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetCavitakProject(objMainClass.intCmpId, "1044", txtFromDate.Text, txtToDate.Text, "0", "TODAYSDISPATCHDETAILS");
                    if (dt.Rows.Count > 0)
                    {
                        grdPopDetails.DataSource = dt;
                        grdPopDetails.DataBind();
                        grdPopDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                        popHeading.Text = "Today's Disaptch Details";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#DashBoardDetails').modal();", true);
                    }
                    else
                    {
                        grdPopDetails.DataSource = string.Empty;
                        grdPopDetails.DataBind();

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Record Not Found.!');", true);
                    }

                    gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    grvTodayInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                    grvOverallInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {

                gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                grvTodayInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                grvOverallInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lnkTodayDelivered_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetCavitakProject(objMainClass.intCmpId, "1044", txtFromDate.Text, txtToDate.Text, "0", "TODAYSDELIVERDETAILS");
                    if (dt.Rows.Count > 0)
                    {
                        grdPopDetails.DataSource = dt;
                        grdPopDetails.DataBind();
                        grdPopDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                        popHeading.Text = "Today's Delivered Details";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#DashBoardDetails').modal();", true);
                    }
                    else
                    {
                        grdPopDetails.DataSource = string.Empty;
                        grdPopDetails.DataBind();

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Record Not Found.!');", true);
                    }

                    gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    grvTodayInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                    grvOverallInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {

                gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                grvTodayInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                grvOverallInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lnkTotalInward_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    try
                    {
                        if (Session["USERID"] != null)
                        {
                            DataTable dt = new DataTable();
                            dt = objMainClass.GetCavitakProject(objMainClass.intCmpId, "1044", txtFromDate.Text, txtToDate.Text, "0", "OVERALLINWARDDETAILS");
                            if (dt.Rows.Count > 0)
                            {
                                grdPopDetails.DataSource = dt;
                                grdPopDetails.DataBind();
                                grdPopDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                                popHeading.Text = "Overall Inward Details";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#DashBoardDetails').modal();", true);
                            }
                            else
                            {
                                grdPopDetails.DataSource = string.Empty;
                                grdPopDetails.DataBind();

                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Record Not Found.!');", true);
                            }

                            gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                            grvTodayInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                            grvOverallInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                        }
                    }
                    catch (Exception ex)
                    {

                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                        grvTodayInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                        grvOverallInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
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

        protected void lnkTotalDispatched_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    try
                    {
                        if (Session["USERID"] != null)
                        {
                            DataTable dt = new DataTable();
                            dt = objMainClass.GetCavitakProject(objMainClass.intCmpId, "1044", txtFromDate.Text, txtToDate.Text, "0", "OVERALLDISPATCHDETAILS");
                            if (dt.Rows.Count > 0)
                            {
                                grdPopDetails.DataSource = dt;
                                grdPopDetails.DataBind();
                                grdPopDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                                popHeading.Text = "Overall Dispatched Details";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#DashBoardDetails').modal();", true);
                            }
                            else
                            {
                                grdPopDetails.DataSource = string.Empty;
                                grdPopDetails.DataBind();

                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Record Not Found.!');", true);
                            }

                            gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                            grvTodayInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                            grvOverallInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                        }
                    }
                    catch (Exception ex)
                    {

                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                        grvTodayInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                        grvOverallInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
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

        protected void lnkTotalDelivered_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    try
                    {
                        if (Session["USERID"] != null)
                        {
                            try
                            {
                                if (Session["USERID"] != null)
                                {
                                    DataTable dt = new DataTable();
                                    dt = objMainClass.GetCavitakProject(objMainClass.intCmpId, "1044", txtFromDate.Text, txtToDate.Text, "0", "OVERALLDELIVEREDDETAILS");
                                    if (dt.Rows.Count > 0)
                                    {
                                        grdPopDetails.DataSource = dt;
                                        grdPopDetails.DataBind();
                                        grdPopDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                                        popHeading.Text = "Overall Delivered Details";
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#DashBoardDetails').modal();", true);
                                    }
                                    else
                                    {
                                        grdPopDetails.DataSource = string.Empty;
                                        grdPopDetails.DataBind();

                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Record Not Found.!');", true);
                                    }

                                    gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                                    grvTodayInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                                    grvOverallInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;

                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                                }
                            }
                            catch (Exception ex)
                            {

                                gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                                grvTodayInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                                grvOverallInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
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

        protected void lnkTotalPending_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    try
                    {
                        if (Session["USERID"] != null)
                        {
                            try
                            {
                                if (Session["USERID"] != null)
                                {
                                    DataTable dt = new DataTable();
                                    dt = objMainClass.GetCavitakProject(objMainClass.intCmpId, "1044", txtFromDate.Text, txtToDate.Text, "0", "OVERALLPENDINGDETAILS");
                                    if (dt.Rows.Count > 0)
                                    {
                                        grdPopDetails.DataSource = dt;
                                        grdPopDetails.DataBind();
                                        grdPopDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                                        popHeading.Text = "Overall Pending Details";
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#DashBoardDetails').modal();", true);
                                    }
                                    else
                                    {
                                        grdPopDetails.DataSource = string.Empty;
                                        grdPopDetails.DataBind();

                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Record Not Found.!');", true);
                                    }

                                    gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                                    grvTodayInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                                    grvOverallInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;

                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                                }
                            }
                            catch (Exception ex)
                            {

                                gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                                grvTodayInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                                grvOverallInwardSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
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