using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.UTILITY
{
    public partial class frmUnMappedItemMapping : System.Web.UI.Page
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
                        SearchUnMappedItemDetail();
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
            else
            {
                if (gvList.Rows.Count > 0)
                {
                    gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        public void SearchUnMappedItemDetail()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objMainClass.GetUnMappedItemDetail(Convert.ToInt32(Session["USERID"]), "DETAIL");
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
            catch (Exception ex)
            {
               ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lnkSerchMapping_Click(object sender, EventArgs e)
        {
            try
            {
                SearchUnMappedItemDetail();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lnkCreateMapped_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow      = (GridViewRow)((LinkButton)sender).NamingContainer;
                    Label lblITEMCODE       = (Label)grdrow.FindControl("lblITEMCODE");
                    Response.Redirect("frmItemMapping.aspx?ITEMCODE=" + lblITEMCODE.Text, false);
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