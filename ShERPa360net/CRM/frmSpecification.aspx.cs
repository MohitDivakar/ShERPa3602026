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
    public partial class frmSpecification : System.Web.UI.Page
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
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }

                        objBindDDL.FillLists(ddlRAM, "RAM");
                        objBindDDL.FillLists(ddlROM, "ROM");
                        objBindDDL.FillLists(ddlColor, "COLOR");
                        ddlColor.SelectedItem.Text = "BLACK";

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

        protected void imgSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    int iResult = objMainClass.CreateJObSpecification(objMainClass.intCmpId, txtJobID.Text, ddlRAM.SelectedItem.Text, ddlROM.SelectedItem.Text, ddlColor.SelectedItem.Text, 0, "GRADE A", txtModel.Text, txtSerialNo.Text, txtMake.Text + "_" + txtModel.Text + "_" + ddlColor.SelectedItem.Text,
                        txtMRP.Text, Convert.ToInt32(Session["USERID"]), "BLACK", "GRADE A", txtItemCode.Text, 0);
                    if (iResult == 1)
                    {
                        txtJobID.Text = string.Empty;
                        txtMake.Text = string.Empty;
                        txtModel.Text = string.Empty;
                        ddlRAM.SelectedIndex = 1;
                        ddlROM.SelectedIndex = 1;
                        ddlColor.SelectedItem.Text = "BLACK";
                        txtSerialNo.Text = string.Empty;
                        txtMRP.Text = string.Empty;
                        txtItemCode.Text = string.Empty;

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Job specification not created. Try again.!');", true);
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

        protected void txtJobID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();

                    dt = objMainClass.SelectJobDetails(txtJobID.Text);

                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToString(dt.Rows[0]["ITEMID"]) != "1")
                        {
                            txtMake.Text = Convert.ToString(dt.Rows[0]["PRODMAKE"]);
                            txtModel.Text = Convert.ToString(dt.Rows[0]["PRODMODEL"]);
                            txtSerialNo.Text = Convert.ToString(dt.Rows[0]["IMEINO"]);
                        }
                        else
                        {
                            txtMake.Text = string.Empty;
                            txtModel.Text = string.Empty;
                            txtSerialNo.Text = string.Empty;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Job id not created for Mobile. Cannot add specification for Mobile.!');", true);
                        }
                    }
                    else
                    {
                        txtMake.Text = string.Empty;
                        txtModel.Text = string.Empty;
                        txtSerialNo.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Record not found with given job id.');", true);
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