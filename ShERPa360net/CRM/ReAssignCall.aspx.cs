using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.CRM
{





    public partial class ReAssignCall : System.Web.UI.Page
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
                        if (FormRights.bAdd == false)
                        {
                            lnkReAssignAll.Enabled = false;
                        }
                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");
                        objBindDDL.FillAgentName(ddlAgentName);
                        objBindDDL.FillStatuseCRM(ddlStatus);

                        objBindDDL.FillAgentName(ddlGVAgedtList);

                        BindData();

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


        public void BindData()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetReAssignData(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text,
                        ddlAgentName.SelectedIndex > 0 ? Convert.ToInt32(ddlAgentName.SelectedValue) : 0, Convert.ToInt32(ddlStatus.SelectedValue), "REASSIGNDATA");

                    if (dt.Rows.Count > 0)
                    {
                        gvList.DataSource = dt;
                        gvList.DataBind();
                        //gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    else
                    {
                        gvList.DataSource = dt;
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

        protected void lnkSearhCall_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    BindData();
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

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        DropDownList ddlGVAgedtList = e.Row.FindControl("ddlGVAgedtList") as DropDownList;
                        LinkButton btnReAssign = e.Row.FindControl("btnReAssign") as LinkButton;
                        objBindDDL.FillAgentName(ddlGVAgedtList);

                        if (FormRights.bAdd == false)
                        {
                            btnReAssign.Enabled = false;
                        }

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

        protected void btnReAssign_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    DropDownList ddlGVAgedtList = grdrow.FindControl("ddlGVAgedtList") as DropDownList;
                    if (ddlGVAgedtList.SelectedIndex > 0)
                    {
                        string LEADID = ((Label)grdrow.FindControl("lblGVID")).Text;
                        string OLDID = ((Label)grdrow.FindControl("lblGVASSIGNINT")).Text;



                        int iResult = objMainClass.UpdateReassingCall(objMainClass.intCmpId, Convert.ToInt32(LEADID), Convert.ToInt32(ddlGVAgedtList.SelectedValue), "REASSIGNUPDATEDATA", Convert.ToInt32(Session["USERID"]), Convert.ToInt32(OLDID));
                        if (iResult > 0)
                        {
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Call Re-Assigned successfully! \");$('.close').click(function(){window.location.href ='ReAssignCall.aspx' });", true);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Call Re-Assigned successfully! \");", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Call Nor Re-Assigned.');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Select Agent Name to Re-Assign Call.');", true);
                    }
                    ddlGVAgedtList.SelectedIndex = 0;
                    lnkSearhCall_Click(1, e);
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

        protected void lnkReAssignAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (ddlGVAgedtList.SelectedIndex > 0)
                    {
                        int iResult = objMainClass.UpdateAllReassignData(objMainClass.intCmpId, "REASSIGNUPDATEDATA", Convert.ToInt32(Session["USERID"]), gvList, Convert.ToInt32(ddlGVAgedtList.SelectedValue));

                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Call Re-Assigned successfully! \");$('.close').click(function(){window.location.href ='ReAssignCall.aspx' });", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Call Re-Assigned successfully! \");", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Select Agent Name to Re-Assign Call.');", true);
                    }
                    ddlGVAgedtList.SelectedIndex = 0;
                    lnkSearhCall_Click(1, e);

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

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow hrow = gvList.HeaderRow;
                    CheckBox chkSelectAll = (CheckBox)hrow.FindControl("chkSelectAll");
                    if (chkSelectAll.Checked == true)
                    {
                        for (int i = 0; i < gvList.Rows.Count; i++)
                        {
                            GridViewRow row = gvList.Rows[i];
                            CheckBox chkSelect = ((CheckBox)row.FindControl("chkSelect"));
                            chkSelect.Checked = true;
                        }
                    }
                    else if (chkSelectAll.Checked == false)
                    {
                        for (int i = 0; i < gvList.Rows.Count; i++)
                        {
                            GridViewRow row = gvList.Rows[i];
                            CheckBox chkSelect = ((CheckBox)row.FindControl("chkSelect"));
                            chkSelect.Checked = false;
                        }
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