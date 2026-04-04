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
    public partial class rptOpenMR : System.Web.UI.Page
    {

        DALUserRights objDALUserRights = new DALUserRights();
        MainClass objMainClass = new MainClass();

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
                            //Session.Abandon();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../REPORTS/ReportDashboard.aspx' });", true);
                            return;
                        }


                        txtMRFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtMRToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();

                        DataTable dt = new DataTable();
                        dt = objMainClass.GetOpenMRData(objMainClass.intCmpId, txtMRNo.Text, txtMRFromDate.Text, txtMRToDate.Text, 0, "");
                        if (dt.Rows.Count > 0)
                        {
                            gvList.DataSource = dt;
                            gvList.DataBind();
                        }
                        else
                        {
                            gvList.DataSource = string.Empty;
                            gvList.DataBind();
                        }



                    }
                    else
                    {
                        Session.Abandon();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
                }
            }
        }

        protected void lnkSearchMR_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                int APPROVED = 0;
                if (chkApprovedMR.Checked == true)
                {
                    APPROVED = 1;
                }
                dt = objMainClass.GetOpenMRData(objMainClass.intCmpId, txtMRNo.Text, txtMRFromDate.Text, txtMRToDate.Text, APPROVED, "");
                if (dt.Rows.Count > 0)
                {
                    gvList.DataSource = dt;
                    gvList.DataBind();
                }
                else
                {
                    gvList.DataSource = string.Empty;
                    gvList.DataBind();
                }
            }
            catch (Exception ex)
            {
                gvList.DataSource = string.Empty;
                gvList.DataBind();
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
                throw;
            }


        }

        protected void bntView_Click(object sender, EventArgs e)
        {


            try
            {

                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

                string prno = grdrow.Cells[1].Text;

                DataTable dt = new DataTable();
                dt = objMainClass.GetMRDetails(prno, 5);
                if (dt.Rows.Count > 0)
                {

                    lblDoctype.Text = Convert.ToString(dt.Rows[0]["MRTYPE"]);
                    lblMRDate.Text = Convert.ToDateTime(dt.Rows[0]["MRDT"]).ToShortDateString();
                    lblMRNo.Text = Convert.ToString(dt.Rows[0]["MRNO"]);
                    lblRemark.Text = Convert.ToString(dt.Rows[0]["REMARK"]);

                    gvDetail.DataSource = dt;
                    gvDetail.DataBind();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                }
                else
                {
                    gvDetail.DataSource = string.Empty;
                    gvDetail.DataBind();
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