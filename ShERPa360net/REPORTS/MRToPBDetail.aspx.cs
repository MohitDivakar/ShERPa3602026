using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.REPORTS
{
    public partial class MRToPBDetail : System.Web.UI.Page
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
                            //   Session.Abandon();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        if (Convert.ToString(Request.QueryString["PRNO"]) != null && Convert.ToString(Request.QueryString["PRNO"]) != string.Empty && Convert.ToString(Request.QueryString["PRNO"]) != "")
                        {
                            txtPRNo.Text = Convert.ToString(Request.QueryString["PRNO"]);
                        }
                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtMrNo.Text = string.Empty;
                        txtPoNo.Text = string.Empty;
                        txtGRNNo.Text = string.Empty;
                        txtPBNo.Text = string.Empty;
                        objBindDDL.FillPlant(ddlPlantCode, "SEARCH");
                        objBindDDL.FillItemGrp(ddlItemGroup, "SEARCH");
                        ddlPlantCode.SelectedValue = "0";
                        ddlItemGroup.SelectedValue = "0";
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


        public override void VerifyRenderingInServerForm(Control control)
        {
        }


        protected void lnkExport_Click(object sender, EventArgs e)
        {
            try
            {
                string attachment = "attachment; filename=rptMRToPurchaseBill.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vdn.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gvMrToPurchaseBill.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        protected void lnkSearhMRToPB_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objMainClass.GETMRTOPURCHASEBILLREPORT(txtFromDate.Text, txtToDate.Text, txtMrNo.Text, txtPRNo.Text, txtPoNo.Text, txtGRNNo.Text, txtPBNo.Text, ddlPlantCode.SelectedValue, Convert.ToInt32(ddlItemGroup.SelectedValue));
                if (dt.Rows.Count > 0)
                {
                    gvMrToPurchaseBill.DataSource = dt;
                    gvMrToPurchaseBill.DataBind();
                    gvMrToPurchaseBill.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                else
                {
                    gvMrToPurchaseBill.DataSource = string.Empty;
                    gvMrToPurchaseBill.DataBind();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}