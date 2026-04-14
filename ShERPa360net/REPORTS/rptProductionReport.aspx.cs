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
    public partial class rptProductionReport : System.Web.UI.Page
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
                        if (FormRights.bView == false) //if (objDALUserRights.bView == false)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();

                        objBindDDL.FillSegment(ddlSegment);
                        ddlSegment.SelectedValue = "1044";
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
                    DataTable dt = new DataTable();
                    dt = objMainClass.GETALLSTAGEDATA(objMainClass.intCmpId, chkQCDate.Checked == true ? 1 : 0, ddlSegment.SelectedValue, txtFromDate.Text, txtToDate.Text, txtJobID.Text, "GETDATA");

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

        protected void lnkJobStat_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

                    string lblJobID = ((Label)grdrow.FindControl("lblJOBID")).Text;
                    DataTable dt = new DataTable();
                    dt = objMainClass.GETALLSTAGEDATA(objMainClass.intCmpId, 0, "", "", "", lblJobID, "STAGEDATA");

                    if (dt.Rows.Count > 0)
                    {
                        gvStageDetails.DataSource = dt;
                        gvStageDetails.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#StageDetails').modal();", true);
                    }
                    else
                    {
                        gvStageDetails.DataSource = string.Empty;
                        gvStageDetails.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No record found.!');", true);
                    }

                    gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //gvStageDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
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

        protected void lnkQC_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

                    string lblJobID = ((Label)grdrow.FindControl("lblJOBID")).Text;
                    DataTable dt = new DataTable();
                    dt = objMainClass.GETALLSTAGEDATA(objMainClass.intCmpId, 0, "", "", "", lblJobID, "QCDATA");

                    if (dt.Rows.Count > 0)
                    {
                        gvQCDetails.DataSource = dt;
                        gvQCDetails.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#QCDetails').modal();", true);
                    }
                    else
                    {
                        gvQCDetails.DataSource = string.Empty;
                        gvQCDetails.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No record found.!');", true);
                    }

                    gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //gvQCDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
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