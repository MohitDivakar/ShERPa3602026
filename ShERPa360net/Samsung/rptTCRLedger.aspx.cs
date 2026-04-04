using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.Samsung
{
    public partial class rptTCRLedger : System.Web.UI.Page
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
                        int month = DateTime.Now.Month;
                        int year = DateTime.Now.Year;

                        if (month < 4)
                        {
                            year = year - 1;
                        }
                        string startdt = "01-04-" + year;
                        txtFromDate.Text = startdt;//objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtToDate.Text = (Convert.ToDateTime(startdt).AddYears(1).AddSeconds(-1)).ToString("dd-MM-yyyy");//objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
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
        }

        public void GetData()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dtDataCenter = new DataTable();
                    dtDataCenter = objMainClass.GetSamsnugTCR(txtFromDate.Text, txtToDate.Text, "", "", "", "", 0, "", "RCVDCENTERREPORT");

                    if (dtDataCenter.Rows.Count > 0)
                    {
                        decimal total = 0;
                        decimal RC = 0;
                        decimal tbrc = 0;
                        decimal RA = 0;
                        decimal tbra = 0;

                        for (int i = 0; i < dtDataCenter.Rows.Count; i++)
                        {
                            total = total + Convert.ToDecimal(dtDataCenter.Rows[i]["Total"]);
                            RC = RC + Convert.ToDecimal(dtDataCenter.Rows[i]["Received at center"]);
                            tbrc = tbrc + Convert.ToDecimal(dtDataCenter.Rows[i]["To be received at Center"]);
                            RA = RA + Convert.ToDecimal(dtDataCenter.Rows[i]["Received at Account"]);
                            tbra = tbra + Convert.ToDecimal(dtDataCenter.Rows[i]["To be received at Account"]);
                        }
                        dtDataCenter.Rows.Add(DateTime.Now, "", "", "", "Total", total, RC, tbrc, RA, tbra);
                        decimal tbrcclosing = total - RC;
                        decimal tbraclosing = RC - RA;
                        dtDataCenter.Rows.Add(DateTime.Now, "", "", "", "Closing", 0, 0, tbrcclosing, 0, tbraclosing);

                        grvDataCenter.DataSource = dtDataCenter;
                        grvDataCenter.DataBind();
                        grvDataCenter.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    else
                    {
                        grvDataCenter.DataSource = string.Empty;
                        grvDataCenter.DataBind();
                    }

                    DataTable dtDataAccount = new DataTable();
                    dtDataAccount = objMainClass.GetSamsnugTCR(txtFromDate.Text, txtToDate.Text, "", "", "", "", 0, "", "RCVDACCOUNTREPORT");

                    if (dtDataAccount.Rows.Count > 0)
                    {
                        decimal total = 0;
                        decimal RA = 0;
                        decimal tbra = 0;

                        for (int i = 0; i < dtDataAccount.Rows.Count; i++)
                        {
                            total = total + Convert.ToDecimal(dtDataAccount.Rows[i]["Total"]);
                            RA = RA + Convert.ToDecimal(dtDataAccount.Rows[i]["Received at Account"]);
                            tbra = tbra + Convert.ToDecimal(dtDataAccount.Rows[i]["To be received at Account"]);
                        }
                        dtDataAccount.Rows.Add(DateTime.Now, "", "", "", "Total", total, RA, tbra);
                        decimal tbraclosing = total - RA;
                        dtDataAccount.Rows.Add(DateTime.Now, "", "", "", "Closing", 0, 0, tbraclosing);

                        grvDataAccount.DataSource = dtDataAccount;
                        grvDataAccount.DataBind();
                        grvDataAccount.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    else
                    {
                        grvDataAccount.DataSource = string.Empty;
                        grvDataAccount.DataBind();
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

        protected void lnkSerch_Click(object sender, EventArgs e)
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
    }
}