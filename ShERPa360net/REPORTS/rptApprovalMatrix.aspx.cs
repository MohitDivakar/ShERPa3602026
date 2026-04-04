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
    public partial class rptApprovalMatrix : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();

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
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');setTimeout(function () { window.location.href = '../HomePage.aspx'; }, 2000);", true);
                            return;
                        }
                        //GetData();
                        BindDropDown();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');setTimeout(function () { window.location.href = '../Login.aspx'; }, 2000);", true);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
                }
            }
        }

        public void BindDropDown()
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    DataTable dtDoctype = new DataTable();
                    dtDoctype = objMainClass.GetApprovalData("", "", "", "GETDOCTYPE");
                    if (dtDoctype.Rows.Count > 0)
                    {
                        ddlDoctype.DataSource = dtDoctype;
                        ddlDoctype.DataTextField = "DOCTYPE";
                        ddlDoctype.DataValueField = "DOCTYPE";
                        ddlDoctype.DataBind();
                        ddlDoctype.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                    }
                    else
                    {
                        ddlDoctype.DataSource = null;
                        ddlDoctype.DataBind();
                    }

                    DataTable dtPlantCode = new DataTable();
                    dtPlantCode = objMainClass.GetApprovalData("", "", "", "GETPLANT");
                    if (dtPlantCode.Rows.Count > 0)
                    {
                        ddlPlant.DataSource = dtPlantCode;
                        ddlPlant.DataTextField = "PLANTDESC";
                        ddlPlant.DataValueField = "PLANTCD";
                        ddlPlant.DataBind();
                        ddlPlant.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                    }
                    else
                    {
                        ddlPlant.DataSource = null;
                        ddlPlant.DataBind();
                    }
                    //ddlPlant_SelectedIndexChanged(1, EventArgs e);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');setTimeout(function () { window.location.href = '../Login.aspx'; }, 2000);", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lnkSearhPO_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GetData();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');setTimeout(function () { window.location.href = '../Login.aspx'; }, 2000);", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void ddlPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dtDeptcd = new DataTable();
                    dtDeptcd = objMainClass.GetApprovalData("", ddlPlant.SelectedIndex > 0 ? ddlPlant.SelectedValue : "", "", "GETDEPT");
                    if (dtDeptcd.Rows.Count > 0)
                    {
                        ddlDept.DataSource = dtDeptcd;
                        ddlDept.DataTextField = "DEPTDESC";
                        ddlDept.DataValueField = "DEPTCD";
                        ddlDept.DataBind();
                        ddlDept.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                    }
                    else
                    {
                        ddlDept.DataSource = null;
                        ddlDept.DataBind();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');setTimeout(function () { window.location.href = '../Login.aspx'; }, 2000);", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public void GetData()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dtData = new DataTable();
                    dtData = objMainClass.GetApprovalData(ddlDoctype.SelectedIndex > 0 ? ddlDoctype.SelectedValue : "", ddlPlant.SelectedIndex > 0 ? ddlPlant.SelectedValue : "", ddlDept.SelectedIndex > 0 ? ddlDept.SelectedValue : "", "GETDATA");
                    if (dtData.Rows.Count > 0)
                    {
                        gvList.DataSource = dtData;
                        gvList.DataBind();
                    }
                    else
                    {
                        gvList.DataSource = null;
                        gvList.DataBind();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');setTimeout(function () { window.location.href = '../Login.aspx'; }, 2000);", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}