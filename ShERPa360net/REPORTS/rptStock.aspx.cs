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
    public partial class rptStock : System.Web.UI.Page
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

        protected void lnkSerch_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    grvData.DataSource = string.Empty;
                    grvData.DataBind();
                    grvChandkheda.DataSource = string.Empty;
                    grvChandkheda.DataBind();
                    //GetStockwithPurchaePrice
                    DataTable dtData = new DataTable();
                    DataTable dtChandkhedaData = new DataTable();
                    dtData = objMainClass.GetStockwithPurchaePrice(objMainClass.intCmpId, "GETSARKHEJSTOCK");
                    if (dtData.Rows.Count > 0)
                    {
                        grvData.DataSource = dtData;
                        grvData.DataBind();
                        grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
                        divMain.Visible = true;
                    }
                    else
                    {
                        grvData.DataSource = string.Empty;
                        grvData.DataBind();
                    }

                    dtChandkhedaData = objMainClass.GetStockwithPurchaePrice(objMainClass.intCmpId, "GETCHANDKHEDASTOCK");
                    if (dtChandkhedaData.Rows.Count > 0)
                    {
                        grvChandkheda.DataSource = dtChandkhedaData;
                        grvChandkheda.DataBind();
                        grvChandkheda.HeaderRow.TableSection = TableRowSection.TableHeader;
                        divMain.Visible = true;
                    }
                    else
                    {
                        grvChandkheda.DataSource = string.Empty;
                        grvChandkheda.DataBind();
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

        protected void btnSarkhejStore_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    divSarkhej.Visible = true;
                    divChandkheda.Visible = false;
                    grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
                    grvChandkheda.HeaderRow.TableSection = TableRowSection.TableHeader;
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

        protected void btnChandkhedaStore_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    divSarkhej.Visible = false;
                    divChandkheda.Visible = true;
                    grvData.HeaderRow.TableSection = TableRowSection.TableHeader;
                    grvChandkheda.HeaderRow.TableSection = TableRowSection.TableHeader;
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