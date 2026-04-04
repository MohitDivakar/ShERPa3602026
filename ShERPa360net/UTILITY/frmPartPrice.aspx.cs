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
    public partial class frmPartPrice : System.Web.UI.Page
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

                        if (FormRights.bAdd == false)
                        {
                            btnSaveAll.Enabled = false;
                        }

                        objBindDDL.QuikeBRAND(ddlBrand, "ALL");
                        ddlBrand.SelectedValue = "7";
                        objBindDDL.QuikeModel(ddlModel, Convert.ToInt32(ddlBrand.SelectedValue));
                        ddlModel.SelectedValue = "1842";
                        DataTable dt = new DataTable();
                        dt = objMainClass.GetProblemCharges(Convert.ToInt32(ddlBrand.SelectedValue), Convert.ToInt32(ddlModel.SelectedValue));
                        if (dt.Rows.Count > 0)
                        {
                            gvList.DataSource = dt;
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
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
                }
            }
        }

        protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBrand.SelectedIndex > 0)
            {
                objBindDDL.QuikeModel(ddlModel, Convert.ToInt32(ddlBrand.SelectedValue));
            }
            else
            {
                ddlModel.DataSource = string.Empty;
                ddlModel.DataBind();
            }
        }

        protected void lnkSerch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = objMainClass.GetProblemCharges(Convert.ToInt32(ddlBrand.SelectedValue), Convert.ToInt32(ddlModel.SelectedValue == "" ? "0" : ddlModel.SelectedValue));
            if (dt.Rows.Count > 0)
            {

                gvList.DataSource = dt;
                gvList.DataBind();
            }
            else
            {

                gvList.DataSource = null;
                gvList.DataBind();
            }
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = e.Row.FindControl("chkStatus") as CheckBox;
                if (Convert.ToInt32((e.Row.FindControl("lblStatus") as Label).Text) == 1)
                {
                    chk.Checked = true;
                }
                else
                {
                    chk.Checked = false;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((Button)sender).NamingContainer;
            int ID = Convert.ToInt32(grdrow.Cells[0].Text);
            string amt = (grdrow.FindControl("txtNewAmount") as TextBox).Text;
            if (amt == null || amt == string.Empty || amt == "")
            {
                amt = grdrow.Cells[3].Text;
            }
            int status = 0;
            CheckBox chk = grdrow.FindControl("chkStatus") as CheckBox;
            if (chk.Checked == true)
            {
                status = 1;
            }
            else
            {
                status = 0;
            }

            int iResult = objMainClass.UpdatePartPrice(ID, amt, status);

            if (iResult == 1)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Record Saved Successfully!');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Something went wrong!');", true);
            }
            lnkSerch_Click(1, e);
        }

        protected void btnSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in gvList.Rows)
                {
                    int ID = Convert.ToInt32(row.Cells[0].Text);
                    string amt = (row.FindControl("txtNewAmount") as TextBox).Text;
                    if (amt == null || amt == string.Empty || amt == "")
                    {
                        amt = row.Cells[3].Text;
                    }
                    int status = 0;
                    CheckBox chk = row.FindControl("chkStatus") as CheckBox;
                    if (chk.Checked == true)
                    {
                        status = 1;
                    }
                    else
                    {
                        status = 0;
                    }

                    int iResult = objMainClass.UpdatePartPrice(ID, amt, status);
                    if(iResult!=1)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Something went wrong!');", true);
                        lnkSerch_Click(1, e);
                        return;
                    }
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Record Saved Successfully!');", true);
                lnkSerch_Click(1, e);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}