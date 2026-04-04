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
    public partial class rptFBAReport : System.Web.UI.Page
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

                        txtFromDate.Text = "01-" + month + "-" + year;//objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();

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
                    dt = objMainClass.GetFBAFBMReportData(objMainClass.intCmpId, strFromDt, strToDt, 0, "SUMMARY");

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
                    if (e.CommandName == "FTD")
                    {
                        //LinkButton lbtn = (LinkButton)sender;
                        //GridViewRow row = (GridViewRow)lbtn.NamingContainer;

                        GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                        if (row != null)
                        {
                            Label lblLISTINGTYPEID = (Label)row.FindControl("lblLISTINGTYPEID");
                            Label lblLISTINGTYPE = (Label)row.FindControl("lblLISTINGTYPE");
                            string path = "rptFBMFBADetail.aspx";
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "?LISTINGTYPEID=" + lblLISTINGTYPEID.Text + "&LISTINGTYPE=" + lblLISTINGTYPE.Text + "&FROMDATE=" + txtFromDate.Text + "&TODATE=" + txtToDate.Text + "&ACTION=FTD&HEADINGLABEL=Inventory Inward FTD - " + lblLISTINGTYPE.Text + "');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found.!');", true);
                        }

                    }
                    else if (e.CommandName == "MTD")
                    {

                        GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                        if (row != null)
                        {
                            Label lblLISTINGTYPEID = (Label)row.FindControl("lblLISTINGTYPEID");
                            Label lblLISTINGTYPE = (Label)row.FindControl("lblLISTINGTYPE");
                            string path = "rptFBMFBADetail.aspx";
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "?LISTINGTYPEID=" + lblLISTINGTYPEID.Text + "&LISTINGTYPE=" + lblLISTINGTYPE.Text + "&FROMDATE=" + txtFromDate.Text + "&TODATE=" + txtToDate.Text + "&ACTION=MTD&HEADINGLABEL=Inventory Inward MTD - " + lblLISTINGTYPE.Text + "');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found.!');", true);
                        }

                    }
                    else if (e.CommandName == "PICKUPTOINWARDAVGHRs")
                    {
                        GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                        if (row != null)
                        {
                            Label lblLISTINGTYPEID = (Label)row.FindControl("lblLISTINGTYPEID");
                            Label lblLISTINGTYPE = (Label)row.FindControl("lblLISTINGTYPE");
                            string path = "rptFBMFBADetail.aspx";
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "?LISTINGTYPEID=" + lblLISTINGTYPEID.Text + "&LISTINGTYPE=" + lblLISTINGTYPE.Text + "&FROMDATE=" + txtFromDate.Text + "&TODATE=" + txtToDate.Text + "&ACTION=PICKUPTOINWARD&HEADINGLABEL=Avg TAT from Receipt to Inward - " + lblLISTINGTYPE.Text + "');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found.!');", true);
                        }
                    }
                    else if (e.CommandName == "INWARDTOJOBSHEETAVGHRs")
                    {
                        GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                        if (row != null)
                        {
                            Label lblLISTINGTYPEID = (Label)row.FindControl("lblLISTINGTYPEID");
                            Label lblLISTINGTYPE = (Label)row.FindControl("lblLISTINGTYPE");
                            string path = "rptFBMFBADetail.aspx";
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "?LISTINGTYPEID=" + lblLISTINGTYPEID.Text + "&LISTINGTYPE=" + lblLISTINGTYPE.Text + "&FROMDATE=" + txtFromDate.Text + "&TODATE=" + txtToDate.Text + "&ACTION=INWARDTOJOBSHEET&HEADINGLABEL=Avg TAT from Pre-Inward to Job Create - " + lblLISTINGTYPE.Text + "');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found.!');", true);
                        }
                    }
                    else if (e.CommandName == "INWARDTOPHYAVGHRs")
                    {
                        GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                        if (row != null)
                        {
                            Label lblLISTINGTYPEID = (Label)row.FindControl("lblLISTINGTYPEID");
                            Label lblLISTINGTYPE = (Label)row.FindControl("lblLISTINGTYPE");
                            string path = "rptFBMFBADetail.aspx";
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "?LISTINGTYPEID=" + lblLISTINGTYPEID.Text + "&LISTINGTYPE=" + lblLISTINGTYPE.Text + "&FROMDATE=" + txtFromDate.Text + "&TODATE=" + txtToDate.Text + "&ACTION=INWARDTOPHY&HEADINGLABEL=Avg TAT from Inward to Ready for FBA Dispatch - " + lblLISTINGTYPE.Text + "');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found.!');", true);
                        }
                    }
                    else if (e.CommandName == "INWARDTOFBAAVGHRs")
                    {
                        GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                        if (row != null)
                        {
                            Label lblLISTINGTYPEID = (Label)row.FindControl("lblLISTINGTYPEID");
                            Label lblLISTINGTYPE = (Label)row.FindControl("lblLISTINGTYPE");
                            string path = "rptFBMFBADetail.aspx";
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "?LISTINGTYPEID=" + lblLISTINGTYPEID.Text + "&LISTINGTYPE=" + lblLISTINGTYPE.Text + "&FROMDATE=" + txtFromDate.Text + "&TODATE=" + txtToDate.Text + "&ACTION=INWARDTOFBA&HEADINGLABEL=Avg TAT from Inward to FBA Dispatch - " + lblLISTINGTYPE.Text + "');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found.!');", true);
                        }
                    }
                    else if (e.CommandName == "PICKUPTOFBAAVGHRs")
                    {
                        GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                        if (row != null)
                        {
                            Label lblLISTINGTYPEID = (Label)row.FindControl("lblLISTINGTYPEID");
                            Label lblLISTINGTYPE = (Label)row.FindControl("lblLISTINGTYPE");
                            string path = "rptFBMFBADetail.aspx";
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "?LISTINGTYPEID=" + lblLISTINGTYPEID.Text + "&LISTINGTYPE=" + lblLISTINGTYPE.Text + "&FROMDATE=" + txtFromDate.Text + "&TODATE=" + txtToDate.Text + "&ACTION=PICKUPTOFBA&HEADINGLABEL=Avg TAT from Procurement to FBA Dispatch - " + lblLISTINGTYPE.Text + "');", true);
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
    }
}