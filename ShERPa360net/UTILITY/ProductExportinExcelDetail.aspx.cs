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
    public partial class ProductExportinExcelDetail : System.Web.UI.Page
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
                    txtFromDate.Text = (objMainClass.indianTime.Date.AddDays(-30)).ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    txtToDate.Text   = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    if (Session["USERID"] != null)
                    {
                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menutabCheckerid.Value, "");
                        Userright = FormRights.bView;

                        if (!Userright)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        objBindDDL.FillCity(ddlCity,1);
                        LoadDashBoardCount();
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
                dt = objMainClass.GetProductStatusWiseDetail(txtFromDate.Text, txtToDate.Text, Convert.ToInt32(ddlStatus.SelectedValue)
                                    , ddlActualDays.SelectedValue, ddlRate.SelectedValue, ddlGrade.SelectedValue
                                    , Convert.ToInt32(Session["USERID"])
                                    , ddlCity.SelectedValue);

                if (dt.Columns.Count > 0)
                {
                    TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                    string attachment = "attachment; filename=" + "ProductStatusReport-Status-" + ddlStatus.SelectedItem.Text +"-DateTime-" + indianTime + ".xls";
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

        public void DowloadStatuswiseReport(int status,string statusstring)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objMainClass.GetProductStatusWiseDetail("01-01-2000", txtToDate.Text, status
                                    , "ALL", "ALL", "ALL"
                                    , Convert.ToInt32(Session["USERID"])
                                    , "ALL");

                if (dt.Columns.Count > 0)
                {
                    TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                    string attachment = "attachment; filename=" + "ProductStatusReport-Status-" + statusstring + "-DateTime-" + indianTime + ".xls";
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


        public void LoadDashBoardCount()
        {
            try
            {
                var dt = objMainClass.GetFollowupCount(objMainClass.indianTime.Date.ToString("dd-MM-yyyy"), objMainClass.indianTime.Date.ToString("dd-MM-yyyy"), Convert.ToInt32(Session["USERID"]));
                if (dt.Rows.Count > 0)
                {
                    // Load Today Count
                    lbtodayunderapproval.Text = dt.Rows[0]["TODAYTESTED"].ToString();
                    lbtodaypickuppending.Text = dt.Rows[0]["TODAYRESERVED"].ToString();
                    lbtodaydeliStorepending.Text = dt.Rows[0]["TODAYORDERRECEIVED"].ToString();
                    lbtodayHandoverBdopending.Text = dt.Rows[0]["TODAYHANDOVERTOBDO"].ToString();
                    lbtodayHandoverVendorpending.Text = dt.Rows[0]["TODAYHANDOVERTODEALER"].ToString();


                    //// Load Total Count
                    Lbtotalunderapproval.Text = dt.Rows[0]["TOTALTESTED"].ToString();
                    Lbtotalpickuppending.Text = dt.Rows[0]["TOTALRESERVED"].ToString();
                    lbtotaldeliStorepending.Text = dt.Rows[0]["TOTALORDERRECEIVED"].ToString();
                    lbtotalHandoverBdopending.Text = dt.Rows[0]["TOTALHANDOVERTOBDO"].ToString();
                    lbtotalHandoverVendorpending.Text = dt.Rows[0]["TOTALHANDOVERTODEALER"].ToString();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lnkTested_Click(object sender, EventArgs e)
        {
            try
            {
                DowloadStatuswiseReport(Convert.ToInt32(PRODUCTSTATUS.TESTED), Convert.ToString(PRODUCTSTATUS.TESTED));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lnkReserved_Click(object sender, EventArgs e)
        {
            try
            {
                DowloadStatuswiseReport(Convert.ToInt32(PRODUCTSTATUS.RESERVED), Convert.ToString(PRODUCTSTATUS.RESERVED));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }

        }

        protected void lnkOrderReceived_Click(object sender, EventArgs e)
        {
            try
            {
                DowloadStatuswiseReport(Convert.ToInt32(PRODUCTSTATUS.ORDERRECEIVED), Convert.ToString(PRODUCTSTATUS.ORDERRECEIVED));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }

        }

        protected void lnkReturnRequestGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                DowloadStatuswiseReport(Convert.ToInt32(PRODUCTSTATUS.RETURNREQUESTGENERATED), Convert.ToString(PRODUCTSTATUS.RETURNREQUESTGENERATED));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lnkReturnHandoverVendor_Click(object sender, EventArgs e)
        {
            try
            {
                DowloadStatuswiseReport(Convert.ToInt32(PRODUCTSTATUS.RETURNHANDOVERTOBDO), Convert.ToString(PRODUCTSTATUS.RETURNHANDOVERTOBDO));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}