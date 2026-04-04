using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.CRM
{
    public partial class rptCustReqReport : System.Web.UI.Page
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
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        //lblUserName.InnerText = Convert.ToString(Session["USERNAME"]);
                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        BindCallData();

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

        public void BindCallData()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetCustReq(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, "", "", "", "GETMOBEXCC", 0);
                    if (dt.Rows.Count > 0)
                    {
                        grvDetail.DataSource = dt;
                        grvDetail.DataBind();
                        grvDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    else
                    {
                        grvDetail.DataSource = string.Empty;
                        grvDetail.DataBind();
                    }

                    DataTable dtSummary = new DataTable();
                    dtSummary = objMainClass.GetCustReq(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, "", "", "", "GETCUSTREQSUMMARY", 0);
                    if (dtSummary.Rows.Count > 0)
                    {
                        grvSummary.DataSource = dtSummary;
                        grvSummary.DataBind();
                        grvSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    else
                    {
                        grvSummary.DataSource = string.Empty;
                        grvSummary.DataBind();
                    }

                    DataTable dtMail = new DataTable();
                    dtMail = objMainClass.GetCustReq(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, "", "CRL", "", "GETCUSTMAILDATA", 0);
                    if (dtMail.Rows.Count > 0)
                    {
                        grvmail.DataSource = dtMail;
                        grvmail.DataBind();
                        grvmail.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    else
                    {
                        grvmail.DataSource = string.Empty;
                        grvmail.DataBind();
                    }


                    DataTable dtReq = new DataTable();
                    dtReq = objMainClass.GetCustReq(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, "", "", "", "GETMOBEXREQ", 0);
                    if (dtReq.Rows.Count > 0)
                    {
                        grvReq.DataSource = dtReq;
                        grvReq.DataBind();
                        grvReq.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    else
                    {
                        grvReq.DataSource = string.Empty;
                        grvReq.DataBind();
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

                    BindCallData();

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

        protected void lnkView_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    Label lblBODY = (Label)grdrow.FindControl("lblBODY");

                    divMail.InnerHtml = lblBODY.Text;

                    grvDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
                    grvSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                    grvmail.HeaderRow.TableSection = TableRowSection.TableHeader;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-View').modal();", true);

                    grvDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
                    grvSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                    grvmail.HeaderRow.TableSection = TableRowSection.TableHeader;
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