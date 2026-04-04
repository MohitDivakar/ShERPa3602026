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
    public partial class MakeModelSuggestPriceHistoryReportItemCode : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();

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
                        //SearchFunctionality();
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
                    objBindDDL.FillMobexSellerBrand(ddlMake);
                    ddlMake.SelectedValue = "0";
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
                string attachment = "attachment; filename=MakeModelSuggestPriceHistorywithItemCode.xls";
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

        public void SearchFunctionality()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    int modelid = 0;
                    modelid = ddlModel.SelectedValue == "" ? 0 : Convert.ToInt32(ddlModel.SelectedValue);
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetMakeModelSuggestPriceHistorywithItemCode(0, Convert.ToInt32(ddlMake.SelectedValue), modelid,
                                                                        txtFromDate.Text, txtToDate.Text);
                    if (dt.Rows.Count > 0)
                    {
                        gvList.DataSource = dt;
                        gvList.DataBind();
                        lblRowCount.Text = "RowCount : " + dt.Rows.Count;
                    }
                    else
                    {
                        gvList.DataSource = dt;
                        gvList.DataBind();
                        lblRowCount.Text = "RowCount : " + "0";
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

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string newrate = e.Row.Cells[8].Text;
                    string suggestrate = e.Row.Cells[10].Text;
                    Double dcnewrate = 0, dcpurchaseper = 0, dcsuugestrate = 0;
                    Double.TryParse(newrate, out dcnewrate);
                    Double.TryParse(suggestrate, out dcsuugestrate);
                    if (dcsuugestrate > 0 && dcnewrate > 0)
                    {
                        dcpurchaseper = Math.Round(((dcsuugestrate * 100)) / (dcnewrate), 0);
                        (e.Row.FindControl("lblSuggestPer") as Label).Text = dcpurchaseper + "%";
                    }
                    else
                    {
                        (e.Row.FindControl("lblSuggestPer") as Label).Text = "0%";
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