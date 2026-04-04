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
    public partial class rptFBMFBADetail : System.Web.UI.Page
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

                        //int month = DateTime.Now.Month;
                        //int year = DateTime.Now.Year;

                        //txtFromDate.Text = "01-" + month + "-" + year;//objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        //txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();

                        //FillData();


                        if (Request.QueryString.Count > 0)
                        {
                            if (Convert.ToString(Request.QueryString["LISTINGTYPEID"]) != null && Convert.ToString(Request.QueryString["LISTINGTYPEID"]) != string.Empty && Convert.ToString(Request.QueryString["LISTINGTYPEID"]) != "")
                            {
                                Session["LISTINGTYPEID"] = Convert.ToString(Request.QueryString["LISTINGTYPEID"]);
                                Session["LISTINGTYPE"] = Convert.ToString(Request.QueryString["LISTINGTYPE"]);
                                Session["FROMDATE"] = Convert.ToString(Request.QueryString["FROMDATE"]);
                                Session["TODATE"] = Convert.ToString(Request.QueryString["TODATE"]);
                                Session["ACTION"] = Convert.ToString(Request.QueryString["ACTION"]);
                                Session["HEADINGLABEL"] = Convert.ToString(Request.QueryString["HEADINGLABEL"]);
                            }
                            Response.Redirect(Request.Url.AbsolutePath, false);
                        }
                        else
                        {
                            if (Session["LISTINGTYPE"] != null && Convert.ToString(Session["LISTINGTYPE"]) != "" && Convert.ToString(Session["LISTINGTYPE"]) != string.Empty)
                            {
                                lblHeading.Text = Convert.ToString(Session["HEADINGLABEL"]);
                                FillData(objMainClass.intCmpId, Convert.ToInt32(Session["LISTINGTYPEID"]), Convert.ToString(Session["FROMDATE"]), Convert.ToString(Session["TODATE"]), Convert.ToString(Session["ACTION"]));
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
        }

        public void FillData(int CMPID, int LISTINGTYPE, string FROMDATE, string TODATE, string ACTION)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    string strFromDt = FROMDATE;
                    string strToDt = TODATE;

                    DataTable dt = new DataTable();
                    dt = objMainClass.GetFBAFBMReportData(CMPID, strFromDt, strToDt, LISTINGTYPE, ACTION);

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
    }
}