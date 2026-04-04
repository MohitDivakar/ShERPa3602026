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
    public partial class rptPendingSCM : System.Web.UI.Page
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
                            //Session.Abandon();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }

                        GetData();
                        rblSIType_SelectedIndexChanged(1, e);

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
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetPendingSCM(objMainClass.intCmpId, "FRANCHISE", "PENDINGSCM");

                    if (dt.Rows.Count > 0)
                    {
                        gvList.DataSource = dt;
                        gvList.DataBind();
                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal1", "setTimeout(function(){ window.location.reload(1); }, 60000);", true);
                    }
                    else
                    {
                        gvList.DataSource = string.Empty;
                        gvList.DataBind();
                    }

                    DataTable dtRTN = new DataTable();
                    dtRTN = objMainClass.GetPendingSCM(objMainClass.intCmpId, "FRANCHISE", "PENDINGSCR");

                    if (dtRTN.Rows.Count > 0)
                    {
                        gvListReturn.DataSource = dtRTN;
                        gvListReturn.DataBind();
                        gvListReturn.HeaderRow.TableSection = TableRowSection.TableHeader;
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal1", "setTimeout(function(){ window.location.reload(1); }, 60000);", true);
                    }
                    else
                    {
                        gvListReturn.DataSource = string.Empty;
                        gvListReturn.DataBind();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true); ;
            }
        }

        protected void rblSIType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (rblSIType.SelectedValue == "1")
                    {
                        divSCM.Visible = true;
                        divSCR.Visible = false;
                    }
                    else
                    {
                        divSCM.Visible = false;
                        divSCR.Visible = true;
                    }


                    if (gvList.Rows.Count > 0)
                    {
                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                    if (gvListReturn.Rows.Count > 0)
                    {
                        gvListReturn.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true); ;
            }
        }
    }
}