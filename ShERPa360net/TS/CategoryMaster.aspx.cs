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
    public partial class CategoryMaster : System.Web.UI.Page
    {
        MainClass objMainClass          = new MainClass();
        DALUserRights objDALUserRights  = new DALUserRights();
        BindDDL objBindDDL              = new BindDDL();

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
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        BindPageDropDown();
                        BindCategoryGrid();
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

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlCategory.SelectedValue != "0")
                {
                    BindCategoryGrid();
                }
                else
                {
                    grvCategoryItem.DataSource = null;
                    grvCategoryItem.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
                            IsAddUpdate = objMainClass.SaveCategoryMaster(0, ddlCategory.SelectedValue, txtCategoryValue.Text,txtCategoryCode.Text, Convert.ToInt32(ddlStatus.SelectedValue), "ADD", 
                                Convert.ToString(Session["USERID"]), txtPartName.Text,Convert.ToInt32(ddlModel.SelectedValue),
                                txtMeterialCode.Text);
                            if (IsAddUpdate)
                            {
                                BindCategoryGrid();
                                ResetFormControl();
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + ddlCategory.SelectedValue + " " + txtCategoryValue.Text + " is already Exist." + "\");", true);
                        }
                    }
                    else
                    {
                        if (!CheckIsExist())
                        {
                            IsAddUpdate = objMainClass.SaveCategoryMaster(Convert.ToInt64(hdCategoryId.Value), ddlCategory.SelectedValue, txtCategoryValue.Text,txtCategoryCode.Text, 
                                Convert.ToInt32(ddlStatus.SelectedValue), "UPDATE", Convert.ToString(Session["USERID"])
                                ,txtPartName.Text,Convert.ToInt32(ddlModel.SelectedValue)
                                , txtMeterialCode.Text);
                            if (IsAddUpdate)
                            {
                                BindCategoryGrid();
                                ResetFormControl();
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + ddlCategory.SelectedValue + " " + txtCategoryValue.Text + " is already Exist." + "\");", true);
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
                BindCategoryGrid();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void grvCategoryItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "eEdit")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    Label lblID = (Label)gRow.FindControl("lblID");
                    Label lblCategoryValue = (Label)gRow.FindControl("lblCategoryValue");
                    Label lblCategoryName = (Label)gRow.FindControl("lblCategoryName");
                    Label lblStatusID = (Label)gRow.FindControl("lblStatusID");
                    Label lblCategoryCode = (Label)gRow.FindControl("lblCategoryCode");
                    Label lblPartName = (Label)gRow.FindControl("lblPartName");
                    Label lblModelKey = (Label)gRow.FindControl("lblModelKey");
                    Label lblMaterialCode = (Label)gRow.FindControl("lblMaterialCode");
                    hdCategoryId.Value = lblID.Text;
                    ddlCategory.SelectedValue = lblCategoryName.Text;
                    txtCategoryValue.Text = lblCategoryValue.Text;
                    ddlStatus.SelectedValue = lblStatusID.Text == "True" ? "1" : "0";
                    txtCategoryCode.Text = lblCategoryCode.Text;
                    txtPartName.Text = lblPartName.Text;
                    ddlModel.SelectedValue = lblModelKey.Text;
                    txtMeterialCode.Text = lblMaterialCode.Text;    
                    Session["saveall"]      = "Update All";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        } 
        #endregion

        #region PAGEMETHOD
        public void BindCategoryGrid()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetCategoryMaster(objMainClass.intCmpId, ddlCategory.SelectedValue,txtCategoryValue.Text, txtCategoryCode.Text, "SELECTALL");
                    grvCategoryItem.DataSource = dt;
                    grvCategoryItem.DataBind();
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

        public void BindPageDropDown()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    objBindDDL.FillCategory(ddlCategory);
                    ddlCategory.SelectedValue = "0";
                    objBindDDL.FillStatus(ddlStatus);
                    
                    //Model Bind
                     objBindDDL.FillTaTaSkyReqDropDown(ddlModel, "MODELS");
                     ddlModel.SelectedValue = "0";
                    //Model Bind

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

        public void ResetFormControl()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    ddlCategory.SelectedValue       = "0";
                    txtCategoryValue.Text           = string.Empty;
                    ddlStatus.SelectedValue         = "-1";
                    hdCategoryId.Value              = "0";
                    txtCategoryCode.Text            = string.Empty;
                    txtPartName.Text = string.Empty;
                    ddlModel.SelectedValue          = "0";
                    txtMeterialCode.Text            = string.Empty;
                    Session["saveall"]              = "Save All";
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
                    dt           = objMainClass.GetCategoryMaster(objMainClass.intCmpId, ddlCategory.SelectedValue, txtCategoryValue.Text,txtCategoryCode.Text, "EXIST", Convert.ToInt64(hdCategoryId.Value));
                    if(dt.Rows.Count > 0)
                    {
                        if(dt.Rows[0]["AVAILABLE"].ToString() == "YES")
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
        #endregion
    }
}