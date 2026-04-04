using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.UTILITY
{
    public partial class UtilityModuleDashboard : System.Web.UI.Page
    {
        MainClass objMainClass = new MainClass();
        BindDDL objBindDDL = new BindDDL();
        DALUserRights objDALUserRights = new DALUserRights();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Session["USERID"] != null)
                    {
                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), mainmenuid.Value, "");
                        if (FormRights.bView == false) //if (objDALUserRights.bView == false)
                        {
                            //Session.Abandon();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        LoadDashBoardCount();
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

        public void LoadDashBoardCount()
        {
            try
            {
                var dt = objMainClass.GetDashBoardCount(objMainClass.indianTime.Date.ToString("dd-MM-yyyy"), objMainClass.indianTime.Date.ToString("dd-MM-yyyy"), Convert.ToInt32(Session["USERID"]));
                if(dt.Rows.Count > 0)
                {
                    // Load Today Count
                    lbtdlisted.Text = dt.Rows[0]["TODAYLISTED"].ToString();
                    lbltdtested.Text = dt.Rows[0]["TODAYTESTED"].ToString();
                    lbltdpurchase.Text = dt.Rows[0]["TODAYPURCHASE"].ToString();
                    lbtdorderreceived.Text = dt.Rows[0]["TODAYORDERRECEIVED"].ToString();
                    lbtdreturn.Text = dt.Rows[0]["TODAYRETURN"].ToString();
                    lbtdrejected.Text = dt.Rows[0]["TODAYREJECTED"].ToString();
                    lbtdapproved.Text = dt.Rows[0]["TODAYAPPROVED"].ToString();

                    // Load Total Count
                    lbovlisted.Text = dt.Rows[0]["TOTALLISTED"].ToString();
                    lbovtested.Text = dt.Rows[0]["TOTALTESTED"].ToString();
                    lbovpurchase.Text = dt.Rows[0]["TOTALPURCHASE"].ToString();
                    lbovorderreceived.Text = dt.Rows[0]["TOTALORDERRECEIVED"].ToString();
                    lbovreturn.Text = dt.Rows[0]["TOTALRETURN"].ToString();
                    lbovrejected.Text = dt.Rows[0]["TOTALREJECTED"].ToString();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}