using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace ShERPa360net.TS
{
    public partial class UserMaster : System.Web.UI.Page
    {
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        
        #region PAGEEVENT
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
                            //Session.Abandon();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        BindPageDropDown();
                        BindUserMasterGrid();
                        ResetFormControl();
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

        protected void imgSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                bool IsAddUpdate = false;
                if (Session["USERID"] != null)
                {
                    if (Convert.ToString(Session["saveall"]) == "Save All")
                    {
                        if (!CheckIsExist())
                        {
                            IsAddUpdate = objMainClass.SaveUserDetail(0, txtfirstName.Text, txtlastName.Text,txtUserCode.Text, ddlPlant.SelectedValue,Convert.ToInt64(ddlDepartment.SelectedValue),
                                                                      ddlRole.SelectedItem.Text, Convert.ToInt32(ddlStatus.SelectedValue), "ADD", Convert.ToString(Session["USERID"]));
                            if (IsAddUpdate)
                            {
                                ResetFormControl();
                                BindUserMasterGrid();
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + txtfirstName.Text + " " + txtlastName.Text + " is already Exist." + "\");", true);
                        }
                    }
                    else
                    {
                        if (!CheckIsExist())
                        {
                            IsAddUpdate = objMainClass.SaveUserDetail(Convert.ToInt32(hdUserId.Value), txtfirstName.Text, txtlastName.Text, txtUserCode.Text, ddlPlant.SelectedValue, Convert.ToInt64(ddlDepartment.SelectedValue),
                                          ddlRole.SelectedItem.Text, Convert.ToInt32(ddlStatus.SelectedValue), "UPDATE", Convert.ToString(Session["USERID"]));
                            if (IsAddUpdate)
                            {
                                ResetFormControl();
                                BindUserMasterGrid();
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + txtfirstName.Text + " " + txtlastName.Text + " is already Exist." + "\");", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void imgReset_Click(object sender, EventArgs e)
        {
            try
            {
                ResetFormControl();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void grvUserMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "eEdit")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    Label lblID = (Label)gRow.FindControl("lblID");
                    Label lblFirstName = (Label)gRow.FindControl("lblFirstName");
                    Label lblLastName = (Label)gRow.FindControl("lblLastName");
                    Label lblPlantId = (Label)gRow.FindControl("lblPlantId");
                    Label lblDepartmentId = (Label)gRow.FindControl("lblDepartmentId");
                    Label lblPlant = (Label)gRow.FindControl("lblPlant");
                    Label lblDepartment = (Label)gRow.FindControl("lblDepartment");
                    Label lblRole = (Label)gRow.FindControl("lblRole");
                    Label lblStatus = (Label)gRow.FindControl("lblStatus");
                    Label lblUserCode = (Label)gRow.FindControl("lblUserCode");
                    var lstitemrole       = ddlRole.Items.FindByText(lblRole.Text);
                    var lstitemstatus     = ddlStatus.Items.FindByText(lblStatus.Text);
                    hdUserId.Value          = lblID.Text;
                    txtfirstName.Text = lblFirstName.Text;
                    txtlastName.Text = lblLastName.Text;
                    ddlPlant.SelectedValue = lblPlantId.Text;
                    ddlDepartment.SelectedValue = lblDepartmentId.Text;
                    ddlRole.SelectedValue = lstitemrole.Value;
                    ddlStatus.SelectedValue = lstitemstatus.Value;
                    txtUserCode.Text = lblUserCode.Text;
                    Session["saveall"] = "Update All";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lnkSearh_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt            = new DataTable();
                    dt                      = objMainClass.SearchUserDetail(txtfirstName.Text,txtlastName.Text,txtUserCode.Text, ddlPlant.SelectedValue,
                                                                            Convert.ToInt64(ddlDepartment.SelectedValue), ddlRole.SelectedItem.Text,
                                                                            Convert.ToInt32(ddlStatus.SelectedValue), "SEARCH");
                    grvUserMaster.DataSource = dt;
                    grvUserMaster.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
        #endregion

        #region PAGEMETHOD
        public void BindPageDropDown()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    objBindDDL.FillTaTaSkyReqDropDown(ddlPlant, "", "PLANT");
                    ddlPlant.SelectedValue = "0";

                    objBindDDL.FillTaTaSkyReqDropDown(ddlDepartment, "", "DEPARTMENT");
                    ddlDepartment.SelectedValue = "0";

                    objBindDDL.FillTaTaSkyReqDropDown(ddlRole, "ROLE");
                    ddlRole.SelectedValue = "0";

                    objBindDDL.FillStatus(ddlStatus);
                    ddlStatus.SelectedValue = "-1";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public void BindUserMasterGrid()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt             = new DataTable();
                    dt                       = objMainClass.GetUserDetail(objMainClass.intCmpId, "", "", 0, "SELECTALL");
                    grvUserMaster.DataSource = dt;
                    grvUserMaster.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public bool CheckIsExist()
        {
            try
            {
                bool Isavailable = false;
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetUserDetail(objMainClass.intCmpId, txtfirstName.Text, txtlastName.Text, Convert.ToInt64(hdUserId.Value), "EXIST");
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["AVAILABLE"].ToString() == "YES")
                        {
                            Isavailable = true;
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='Login.aspx' });", true);
                }
                return Isavailable;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
                return false;
            }
        }

        public void ResetFormControl()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    txtfirstName.Text = string.Empty;
                    txtlastName.Text = string.Empty;
                    ddlPlant.SelectedValue = "0";
                    ddlDepartment.SelectedValue = "0";
                    ddlRole.SelectedValue = "0";
                    ddlStatus.SelectedValue = "-1";
                    hdUserId.Value = "0";
                    txtUserCode.Text = string.Empty;
                    Session["saveall"] = "Save All";
                    BindUserMasterGrid();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
        #endregion

        
    }
}