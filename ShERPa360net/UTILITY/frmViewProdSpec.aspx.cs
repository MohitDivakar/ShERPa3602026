using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.UTILITY
{
    public partial class frmViewProdSpec : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["EditBrandID"] = null;
            Session["EditBrandID"] = string.Empty;
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

                        if (FormRights.bAdd == false)
                        {
                            lnkNewMR.Enabled = false;
                        }

                        BindPageDropDown();
                        if (Request.QueryString.Count > 0)
                        {
                            if (Convert.ToString(Request.QueryString["Make"]) != null && Convert.ToString(Request.QueryString["Make"]) != string.Empty)
                            {
                                ddlMake.SelectedValue = Convert.ToString(Request.QueryString["Make"]);
                            }

                            BindMakeorModelAssociateDetail("Model");

                            if (Convert.ToString(Request.QueryString["Model"]) != null && Convert.ToString(Request.QueryString["Model"]) != string.Empty)
                            {
                                ddlModel.SelectedValue = Convert.ToString(Request.QueryString["Model"]);
                            }
                        }


                        SearchFunctionality();

                        //DataTable dt = new DataTable();
                        //dt = objMainClass.GetProdSpec(0, 0, 0, "SELECT10");
                        //if (dt.Rows.Count > 0)
                        //{
                        //    gvList.DataSource = dt;
                        //    gvList.DataBind();
                        //}
                        //else
                        //{
                        //    gvList.DataSource = dt;
                        //    gvList.DataBind();
                        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
                        //}
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

        public void BindPageDropDown()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    objBindDDL.FillItemType(ddlItemType);
                    ddlItemType.SelectedValue = "0";

                    objBindDDL.FillMobexSellerBrand(ddlMake);
                    ddlMake.SelectedValue = "0";

                    objBindDDL.FillModelSpecDropDown(ddlItemGroup, "ITEMGROUP");
                    ddlItemGroup.SelectedValue = "0";

                    objBindDDL.FillModelSpecDropDown(ddlItemSubGroup, "ITEMSUBGROUP");
                    ddlItemSubGroup.SelectedValue = "0";

                    //objBindDDL.FillLists(ddlRom, "ROM");
                    //ddlRom.SelectedValue = "0";

                    //objBindDDL.FillLists(ddlRam, "RAM");
                    //ddlRam.SelectedValue = "0";

                    //objBindDDL.FillLists(ddlColor, "CL");
                    //ddlColor.SelectedValue = "0";
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

        protected void lnkSerchSpec_Click(object sender, EventArgs e)
        {
            try
            {
                SearchFunctionality();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void lnkExport_Click(object sender, EventArgs e)
        {
            try
            {
                string attachment = "attachment; filename=rptMR.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vdn.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gvList.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void ddlMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindMakeorModelAssociateDetail("Model");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public void BindMakeorModelAssociateDetail(string reqdropdown)
        {
            try
            {
                if (reqdropdown == "Model")
                {
                    if (ddlMake.SelectedValue != "0")
                    {
                        ddlModel.Items.Clear();
                        objBindDDL.FillMobexSellerSearchModel(ddlModel, Convert.ToInt32(ddlMake.SelectedValue));
                    }
                    else
                    {
                        ddlModel.Items.Clear();
                        ddlModel.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                        ddlModel.SelectedValue = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {

            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    string ID = grdrow.Cells[0].Text;
                    Response.Redirect("frmAddProdSpec.aspx?ID=" + ID);
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

        public void SearchFunctionality()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    int modelid = 0;
                    modelid = ddlModel.SelectedValue == "" ? 0 : Convert.ToInt32(ddlModel.SelectedValue);
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetProdSpec(0, Convert.ToInt32(ddlMake.SelectedValue), modelid, "SEARCH",Convert.ToInt32(ddlItemType.SelectedValue)
                        ,Convert.ToInt32(ddlItemGroup.SelectedValue), Convert.ToInt32(ddlItemSubGroup.SelectedValue)
                        , ddlMnthTop10.SelectedValue, txtAsin.Text);
                    if (dt.Rows.Count > 0)
                    {
                        gvList.DataSource = dt;
                        gvList.DataBind();
                    }
                    else
                    {
                        gvList.DataSource = dt;
                        gvList.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
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