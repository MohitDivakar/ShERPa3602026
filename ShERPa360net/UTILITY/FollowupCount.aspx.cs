using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.UTILITY
{
    public partial class FollowupCount : System.Web.UI.Page
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
                var dt = objMainClass.GetFollowupCount(objMainClass.indianTime.Date.ToString("dd-MM-yyyy"), objMainClass.indianTime.Date.ToString("dd-MM-yyyy"), Convert.ToInt32(Session["USERID"]));
                if(dt.Rows.Count > 0)
                {
                    // Load Today Count
                    lbtodayunderapproval.Text             = dt.Rows[0]["TODAYTESTED"].ToString();
                    lbtodaypickuppending.Text             = dt.Rows[0]["TODAYRESERVED"].ToString();
                    lbtodaydeliStorepending.Text          = dt.Rows[0]["TODAYORDERRECEIVED"].ToString();
                    lbtodayHandoverBdopending.Text        = dt.Rows[0]["TODAYHANDOVERTOBDO"].ToString();
                    lbtodayHandoverVendorpending.Text     = dt.Rows[0]["TODAYHANDOVERTODEALER"].ToString();


                    //// Load Total Count
                    Lbtotalunderapproval.Text             = dt.Rows[0]["TOTALTESTED"].ToString();
                    Lbtotalpickuppending.Text             = dt.Rows[0]["TOTALRESERVED"].ToString();
                    lbtotaldeliStorepending.Text          = dt.Rows[0]["TOTALORDERRECEIVED"].ToString();
                    lbtotalHandoverBdopending.Text        = dt.Rows[0]["TOTALHANDOVERTOBDO"].ToString();
                    lbtotalHandoverVendorpending.Text     = dt.Rows[0]["TOTALHANDOVERTODEALER"].ToString();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}