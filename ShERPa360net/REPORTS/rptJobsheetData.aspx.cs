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
    public partial class rptJobsheetData : System.Web.UI.Page
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
                        objBindDDL.FillPlant(ddlPLant);
                        objBindDDL.FillStatus(ddlStatus, 2);

                        if (Request.QueryString.Count > 0)
                        {
                            if (Convert.ToString(Request.QueryString["PLANT"]) != null && Convert.ToString(Request.QueryString["PLANT"]) != string.Empty && Convert.ToString(Request.QueryString["PLANT"]) != "")
                            {
                                Session["JSDPLANT"] = Convert.ToString(Request.QueryString["PLANT"]);
                            }
                            else
                            {
                                Session["JSDPLANT"] = null;
                            }

                            if (Convert.ToString(Request.QueryString["SEGMENT"]) != null && Convert.ToString(Request.QueryString["SEGMENT"]) != string.Empty && Convert.ToString(Request.QueryString["SEGMENT"]) != "")
                            {
                                Session["JSDSEGMENT"] = Convert.ToString(Request.QueryString["SEGMENT"]);
                            }
                            else
                            {
                                Session["JSDSEGMENT"] = null;
                            }

                            if (Convert.ToString(Request.QueryString["FROMDATE"]) != null && Convert.ToString(Request.QueryString["FROMDATE"]) != string.Empty && Convert.ToString(Request.QueryString["FROMDATE"]) != "")
                            {
                                Session["JSDFROMDATE"] = Convert.ToString(Request.QueryString["FROMDATE"]);
                            }
                            else
                            {
                                Session["JSDFROMDATE"] = null;
                            }

                            if (Convert.ToString(Request.QueryString["TODATE"]) != null && Convert.ToString(Request.QueryString["TODATE"]) != string.Empty && Convert.ToString(Request.QueryString["TODATE"]) != "")
                            {
                                Session["JSDTODATE"] = Convert.ToString(Request.QueryString["TODATE"]);
                            }
                            else
                            {
                                Session["JSDTODATE"] = null;
                            }

                            if (Convert.ToString(Request.QueryString["HEADING"]) != null && Convert.ToString(Request.QueryString["HEADING"]) != string.Empty && Convert.ToString(Request.QueryString["HEADING"]) != "")
                            {
                                Session["JSDHEADING"] = Convert.ToString(Request.QueryString["HEADING"]);
                            }
                            else
                            {
                                Session["JSDHEADING"] = null;
                            }

                            if (Convert.ToString(Request.QueryString["STATUSID"]) != null && Convert.ToString(Request.QueryString["STATUSID"]) != string.Empty && Convert.ToString(Request.QueryString["STATUSID"]) != "")
                            {
                                Session["JSDSTATUS"] = Convert.ToString(Request.QueryString["STATUSID"]);
                            }
                            else
                            {
                                Session["JSDSTATUS"] = null;
                            }

                            if (Convert.ToString(Request.QueryString["FROMDAY"]) != null && Convert.ToString(Request.QueryString["FROMDAY"]) != string.Empty && Convert.ToString(Request.QueryString["FROMDAY"]) != "")
                            {
                                Session["JSDFROMDAY"] = Convert.ToString(Request.QueryString["FROMDAY"]);
                            }
                            else
                            {
                                Session["JSDFROMDAY"] = null;
                            }

                            if (Convert.ToString(Request.QueryString["TODAY"]) != null && Convert.ToString(Request.QueryString["TODAY"]) != string.Empty && Convert.ToString(Request.QueryString["TODAY"]) != "")
                            {
                                Session["JSDTODAY"] = Convert.ToString(Request.QueryString["TODAY"]);
                            }
                            else
                            {
                                Session["JSDTODAY"] = null;
                            }

                            Response.Redirect(Request.Url.AbsolutePath, false);
                        }
                        else
                        {
                            if (Convert.ToString(Session["JSDPLANT"]) != null && Convert.ToString(Session["JSDPLANT"]) != string.Empty && Convert.ToString(Session["JSDPLANT"]) != "")
                            {
                                ddlPLant.SelectedValue = Convert.ToString(Session["JSDPLANT"]);
                            }
                            if (Convert.ToString(Session["JSDSEGMENT"]) != null && Convert.ToString(Session["JSDSEGMENT"]) != string.Empty && Convert.ToString(Session["JSDSEGMENT"]) != "")
                            {
                                ddlSegment.SelectedValue = Convert.ToString(Session["JSDSEGMENT"]);
                            }
                            if (Convert.ToString(Session["JSDFROMDATE"]) != null && Convert.ToString(Session["JSDFROMDATE"]) != string.Empty && Convert.ToString(Session["JSDFROMDATE"]) != "")
                            {
                                txtFromDate.Text = Convert.ToString(Session["JSDFROMDATE"]);
                            }
                            if (Convert.ToString(Session["JSDTODATE"]) != null && Convert.ToString(Session["JSDTODATE"]) != string.Empty && Convert.ToString(Session["JSDTODATE"]) != "")
                            {
                                txtToDate.Text = Convert.ToString(Session["JSDTODATE"]);
                            }
                            if (Convert.ToString(Session["JSDHEADING"]) != null && Convert.ToString(Session["JSDHEADING"]) != string.Empty && Convert.ToString(Session["JSDHEADING"]) != "")
                            {
                                lblHeading.Text = " - " + Convert.ToString(Session["JSDHEADING"]);
                            }
                            if (Convert.ToString(Session["JSDSTATUS"]) != null && Convert.ToString(Session["JSDSTATUS"]) != string.Empty && Convert.ToString(Session["JSDSTATUS"]) != "")
                            {
                                ddlStatus.SelectedValue = Convert.ToString(Session["JSDSTATUS"]);
                            }

                            FillData();
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


        public void FillData()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    string strFromDt = txtFromDate.Text;
                    string strToDt = txtToDate.Text;

                    DataTable dt = new DataTable();
                    //dt = objMainClass.GetJobsheetCount(objMainClass.intCmpId, 2, ddlStatus.SelectedIndex > 0 ? Convert.ToInt32(ddlStatus.SelectedValue) : 0, strFromDt, strToDt, ddlSegment.SelectedIndex == 0 ? Convert.ToString(Session["SEGMENT"]) : ddlSegment.SelectedValue, ddlPLant.SelectedIndex == 0 ? Convert.ToString(Session["PLANTCD"]) : ddlPLant.SelectedValue, "DETAILDATA", Convert.ToInt32(Session["JSDFROMDAY"]), Convert.ToInt32(Session["JSDTODAY"]));
                    dt = objMainClass.GetJobsheetCountNew(objMainClass.intCmpId, 2, Convert.ToString(Session["JSDSTATUS"]), strFromDt, strToDt, ddlSegment.SelectedIndex == 0 ? Convert.ToString(Session["SEGMENT"]) : ddlSegment.SelectedValue, ddlPLant.SelectedIndex == 0 ? Convert.ToString(Session["PLANTCD"]) : ddlPLant.SelectedValue, "DETAILDATANEW", Convert.ToInt32(Session["JSDFROMDAY"]), Convert.ToInt32(Session["JSDTODAY"]));

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
    }
}