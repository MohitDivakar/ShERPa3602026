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
    public partial class rptOpenGRNReport : System.Web.UI.Page
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

                        //txtAsonDate.Text = firstDayOfMonth;// "01-" + month + "-" + year;//objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtAsonDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();

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
                    string strAsonDt = txtAsonDate.Text;

                    DataTable dt = new DataTable();
                    dt = objMainClass.GetOpenGrn(objMainClass.intCmpId, "REPORTCOUNT");

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

                    ViewState["JSAsonDate"] = strAsonDt;

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
                    if (e.CommandName == "PNDQTY")
                    {
                        //GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                        //if (row != null)
                        //{
                        //    Label lblSTATUSID = (Label)row.FindControl("lblSTATUSID");
                        //    Label lblSTATUS = (Label)row.FindControl("lblSTATUS");
                        //    string PLANT = Convert.ToString(ViewState["JSPlant"]);
                        //    string SEGMENT = Convert.ToString(ViewState["JSSegment"]);
                        //    string STATUS = Convert.ToString(ViewState["JSStatus"]);
                        //    string strFromDt = Convert.ToString(ViewState["JSFromDate"]);
                        //    string strToDt = Convert.ToString(ViewState["JSToDate"]);
                        //    string path = "rptJobsheetData.aspx";

                        //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "?PLANT=" + PLANT + "&SEGMENT=" + SEGMENT + "&FROMDATE=" + strFromDt + "&TODATE=" + strToDt + "&HEADING=" + lblSTATUS.Text + "&FROMDAY=3&TODAY=7&STATUSID=" + lblSTATUSID.Text + "');", true);
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found.!');", true);
                        //}
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