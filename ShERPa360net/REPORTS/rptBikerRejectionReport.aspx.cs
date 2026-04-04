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
    public partial class rptBikerRejectionReport : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //SortDirection = "DESC";
                //SortColumn = "AGE";

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

                        string[] date = objMainClass.indianTime.Date.ToString("dd-MM-yyyy").Split('-');
                        DateTime fromdate = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), 1);
                        txtFromDocDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");
                        txtToDocDate.Text   = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");
                        objBindDDL.FillBiker(ddlBiker, "BIKERMENU");
                        objBindDDL.FillCity(ddlCity, 1);
                        //GetData("REPORT", txtFromDocDate.Text, txtToDocDate.Text, 0);
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

        public void GetData(string ACTION, string FROMDATE, string TODATE, int SEARCHBY, string CITY)
        {
            //GetBikerVisitReport
            if (Session["USERID"] != null)
            {
                try
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetBikerRejectionReport(ACTION, FROMDATE, TODATE, SEARCHBY, CITY);
                    if (dt.Rows.Count > 0)
                    {
                        ltgridhtml.Text = dt.Rows[0]["TableHtml"].ToString();
                    }
                }
                catch (Exception ex)
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true); ;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
            }
        }

        protected void lnkSerch_Click(object sender, EventArgs e)
        {
            if (Session["USERID"] != null)
            {
                try
                {
                    GetData("REPORT", txtFromDocDate.Text, txtToDocDate.Text, Convert.ToInt32(Session["USERID"]),ddlCity.SelectedValue);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true); ;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void lnkExport_Click(object sender, EventArgs e)
        {
            if (Session["USERID"] != null)
            {
                try
                {
                    string attachment = "attachment; filename=Biker Reject Report.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vdn.ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    ltgridhtml.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true); ;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
            }
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Decimal gTotal =  Convert.ToDecimal(e.Row.Cells[3].Text) + Convert.ToDecimal(e.Row.Cells[4].Text)
                                + Convert.ToDecimal(e.Row.Cells[5].Text) + Convert.ToDecimal(e.Row.Cells[6].Text);
                
                e.Row.Cells[7].Text = gTotal.ToString();

                if(Convert.ToDecimal(e.Row.Cells[6].Text) == 0)
                {
                    e.Row.Cells[8].Text = "0";
                }
                else
                {
                    e.Row.Cells[8].Text = (Math.Round(((Convert.ToDecimal(e.Row.Cells[6].Text) * 100) / gTotal),2)).ToString();
                }
                
            }
        }
    }
}