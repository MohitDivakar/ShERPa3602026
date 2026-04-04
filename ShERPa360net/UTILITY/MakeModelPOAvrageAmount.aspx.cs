using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ShERPa360net.UTILITY
{
    public partial class MakeModelPOAvrageAmount : System.Web.UI.Page
    {
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();

        public SqlConnection ConnSherpa { get; private set; }

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
                        objBindDDL.FillMobexSellerBrand(ddlMake);
                        ddlMake.SelectedValue = "0";

                        DataTable frstRight = objDALUserRights.PO_APPROVE_RIGHTS("PO", Convert.ToString(Session["USERID"]), Convert.ToString(Session["PLANTCD"]), Convert.ToString(Session["DEPTCD"]), 7);

                        DataTable dt = new DataTable();
                        dt = objMainClass.GetMakeModelPOAvrageAmount(objMainClass.intCmpId, ddlMake.Text, ddlModel.Text);
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
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
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
        protected void lnkSearhMakeModel_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable frstRight = objDALUserRights.PO_APPROVE_RIGHTS("PO", Convert.ToString(Session["USERID"]), Convert.ToString(Session["PLANTCD"]), Convert.ToString(Session["DEPTCD"]), 7);

                if (frstRight.Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetMakeModelPOAvrageAmount(objMainClass.intCmpId, ddlMake.Text, ddlModel.Text);
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
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorized to view this page');$('.close').click(function(){window.location.href ='MMDashboard.aspx' });", true);
                }
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
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

    }
}