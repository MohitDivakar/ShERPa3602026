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
    public partial class rptJSReturnDetails : System.Web.UI.Page
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

                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menutabid.Value, "");
                        if (FormRights.bView == false)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        string[] date = objMainClass.indianTime.Date.ToString("dd-MM-yyyy").Split('-');
                        DateTime fromdate = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), 1);
                        txtFromDocDate.Text = fromdate.ToString("dd-MM-yyyy");
                        txtToDocDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();

                        objBindDDL.FillPlant(ddlPlant, "ENTRY");
                        objBindDDL.FillSegment(ddlSegment);
                        objBindDDL.FillLists(ddlSalesFrom, "SF");
                        objBindDDL.FillLists(ddlReturnType, "RT");

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

                    DataTable dtData = new DataTable();
                    dtData = objMainClass.GetReturnData(objMainClass.intCmpId, txtFromDocDate.Text, txtToDocDate.Text, ddlSegment.SelectedIndex > 0 ? ddlSegment.SelectedValue : "", ddlPlant.SelectedIndex > 0 ? ddlPlant.SelectedValue : "", "",
                        txtNewJobID.Text, txtOldJobID.Text, txtOrderID.Text, ddlSalesFrom.SelectedIndex > 0 ? Convert.ToInt32(ddlSalesFrom.SelectedValue) : 0, ddlReturnType.SelectedIndex > 0 ? ddlReturnType.SelectedItem.Text : "", "RETURNDATA");
                    if (dtData.Rows.Count > 0)
                    {
                        gvAllList.DataSource = dtData;
                        gvAllList.DataBind();
                        gvAllList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    else
                    {
                        gvAllList.DataSource = string.Empty;
                        gvAllList.DataBind();
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
}