using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.TS
{
    public partial class TSDashboard : System.Web.UI.Page
    {
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["USERID"] != null)
                    {
                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menutabid.Value, "");
                        if (FormRights.bView == false)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        LoadDashBoard();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }


        public void LoadDashBoard()
        {
            try
            {
                var dtDashBoard = objMainClass.GetTaTaSkyDashBoard();
                if (dtDashBoard.Rows.Count > 0)
                {
                    dvNotification.InnerHtml = dtDashBoard.Rows[0]["NotificationCount"].ToString();
                    dvPrescanning.InnerHtml = dtDashBoard.Rows[0]["PreScanningCount"].ToString();
                    dvRepaired.InnerHtml = dtDashBoard.Rows[0]["RepairedCount"].ToString();
                    dvIR.InnerHtml = dtDashBoard.Rows[0]["IRCount"].ToString();
                    dvBER.InnerHtml = dtDashBoard.Rows[0]["BERCount"].ToString();
                    dvFailure.InnerHtml = dtDashBoard.Rows[0]["FailedCount"].ToString();
                    dvDispatch.InnerHtml = dtDashBoard.Rows[0]["DispatchCount"].ToString();
                }
            }
            catch (Exception  ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }

        }

    }
}