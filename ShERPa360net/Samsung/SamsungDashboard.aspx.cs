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
    public partial class SamsungDashboard : System.Web.UI.Page
    {

        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
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
                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), mainmenuid.Value, "");
                        if (FormRights.bView == false)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        BindGrid();
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

        public void BindGrid()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    //GetSamsnugTCR
                    DataTable dtDashboard = new DataTable();
                    dtDashboard = objMainClass.GetSamsnugTCR("", "", "", "", "", "", 0, "", "DASHBOARD");

                    if (dtDashboard.Rows.Count > 0)
                    {
                        lblTotalSO.Text = Convert.ToString(dtDashboard.Rows[0]["SOCNT"]);
                        lblTotalTCR.Text = Convert.ToString(dtDashboard.Rows[0]["TCRCNT"]);
                        lblPendingSO.Text = Convert.ToString(dtDashboard.Rows[0]["PENDINGCNT"]);
                    }
                    else
                    {
                        lblTotalSO.Text = "0";
                        lblTotalTCR.Text = "0";
                        lblPendingSO.Text = "0";
                    }

                    DataTable dtDashboardData = new DataTable();
                    dtDashboardData = objMainClass.GetSamsnugTCR("", "", "", "", "", "", 0, "", "DASHBOARDDATA");

                    if (dtDashboardData.Rows.Count > 0)
                    {
                        gvAllList.DataSource = dtDashboardData;
                        gvAllList.DataBind();
                        gvAllList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    else
                    {
                        gvAllList.DataSource = string.Empty;
                        gvAllList.DataBind();
                    }

                    DataTable dtDashboardAmtData = new DataTable();
                    dtDashboardAmtData = objMainClass.GetSamsnugTCR("", "", "", "", "", "", 0, "", "DASHBOARDWITHRCVDAMT");

                    if (dtDashboardAmtData.Rows.Count > 0)
                    {
                        gvAmtData.DataSource = dtDashboardAmtData;
                        gvAmtData.DataBind();
                        gvAmtData.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    else
                    {
                        gvAmtData.DataSource = string.Empty;
                        gvAmtData.DataBind();
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