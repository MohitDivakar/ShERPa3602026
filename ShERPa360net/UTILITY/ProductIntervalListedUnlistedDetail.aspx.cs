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
    public partial class ProductIntervalListedUnlistedDetail : System.Web.UI.Page
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
                    if (Session["USERID"] != null)
                    {
                        txtFromDate.Text = (objMainClass.indianTime.Date.AddDays(-250)).ToString("dd-MM-yyyy");
                        txtToDate.Text   = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");
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


        protected void lnkExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                string fromdate, todate = "";
                fromdate = objMainClass.setDateFormat(txtFromDate.Text,true);
                todate   = objMainClass.setDateFormat(txtToDate.Text, true);
                fromdate = fromdate + " " + txtFromTime.Text + ":00";
                todate   = todate   + " " + txtToTime.Text   + ":59";

                dt = objMainClass.GetIntervalListedUnlistedReport(fromdate, todate, Convert.ToInt32(Session["USERID"]));
                if (dt.Columns.Count > 0)
                {
                    TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                    string attachment = "attachment; filename=" + "ProductIntervalListedUnlistedReport-DateTime-" + indianTime + " FromTime-" + txtFromTime.Text + " ToTime-" + txtToTime.Text + ".xls";
                    Context.Response.ClearContent();
                    Context.Response.AddHeader("content-disposition", attachment);
                    Context.Response.ContentType = "application/vnd.ms-excel";
                    int headeri = 1;
                    int bodyi = 1;
                    foreach (DataColumn dc in dt.Columns)
                    {
                        //if(headeri == 1)
                        //{
                        //    Context.Response.Write(dc.ColumnName);
                        //    headeri = headeri + 1;
                        //}
                        //else
                        //{
                            Context.Response.Write("\t" + dc.ColumnName);
                        //}
                    }
                    Context.Response.Write("\n");
                    int i;
                    foreach (DataRow dr in dt.Rows)
                    {
                        for (i = 0; i < dt.Columns.Count; i++)
                        {
                            //if (bodyi == 1)
                            //{
                            //    Context.Response.Write(dr[i].ToString());
                            //    bodyi = bodyi + 1;
                            //}
                            //else
                            //{
                                Context.Response.Write("\t" + dr[i].ToString());
                            //}
                        }
                        bodyi = 1;
                        Context.Response.Write("\n");
                    }
                    Context.Response.Flush();
                    Context.Response.Close();
                    Context.Response.End();
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        #region PAGEMETHOD
        public void BindPageDropDown()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                   
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

        public override void VerifyRenderingInServerForm(Control control)
        {

        }
        #endregion

    }
}