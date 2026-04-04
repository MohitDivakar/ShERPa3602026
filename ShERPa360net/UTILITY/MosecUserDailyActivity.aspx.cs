using Newtonsoft.Json;
using ShERPa360net.Class;
using ShERPa360net.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.UTILITY
{
    public partial class MosecUserDailyActivity : System.Web.UI.Page
    {
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        bool Userright = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtFromDate.Text = (objMainClass.indianTime.Date.AddDays(-1)).ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    txtToDate.Text   = (objMainClass.indianTime.Date.AddDays(-1)).ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    if (Session["USERID"] != null)
                    {
                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menutabCheckerid.Value, "");
                        Userright = FormRights.bView;

                        if (!Userright)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
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

        protected void lnkSearh_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    BindProductDetail();
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


        protected void lnkExport_Click(object sender, EventArgs e)
        {
            try
            {
                string attachment = "attachment; filename=MosecUserDailyActivity.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vdn.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gvMosecuseractivity.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();


                string attachmenttotallisting = "attachment; filename=MosecUserTotallisting.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachmenttotallisting);
                Response.ContentType = "application/vdn.ms-excel";
                StringWriter swtl = new StringWriter();
                HtmlTextWriter htwtl = new HtmlTextWriter(swtl);
                gvMosecuseractivity.RenderControl(htwtl);
                Response.Write(swtl.ToString());
                Response.End();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        #region PAGEMETHOD
        public override void VerifyRenderingInServerForm(Control control)
        {

        }


        public void BindProductDetail()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = objMainClass.GetMosecUserActivityDetail(1, txtFromDate.Text, txtToDate.Text);
                if (ds.Tables.Count > 0)
                {
                    gvMosecuseractivity.DataSource = ds.Tables[0];
                    gvMosecuseractivity.DataBind();
                    gvMosecuseractivity.HeaderRow.TableSection = TableRowSection.TableHeader;

                    gvMosecUserTotalListing.DataSource = ds.Tables[1];
                    gvMosecUserTotalListing.DataBind();
                    gvMosecUserTotalListing.HeaderRow.TableSection = TableRowSection.TableHeader;

                }
                else
                {
                    gvMosecuseractivity.DataSource = string.Empty;
                    gvMosecuseractivity.DataBind();

                    gvMosecUserTotalListing.DataSource = string.Empty;
                    gvMosecUserTotalListing.DataBind();
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