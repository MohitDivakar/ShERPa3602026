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

namespace ShERPa360net.TS
{
    public partial class frmBERReport : System.Web.UI.Page
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
                    txtFromDate.Text    = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); 
                    txtToDate.Text      = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); 
                    if (Session["USERID"] != null)
                    {
                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menutabid.Value, "");
                        if (FormRights.bView == false)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        BindBERReport();
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
                    BindBERReport();
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

        protected void imgReset_Click(object sender, EventArgs e)
        {
            try
            {
                ResetFormControl();
                BindBERReport();
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
                string filename = "BER Report" + txtFromDate.Text + "-" + txtToDate.Text;
                string attachment = "attachment; filename=" + filename + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vdn.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gvRepairReport.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        #region PAGEMETHOD
        public void BindBERReport()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt                   = new DataTable();
                    dt                             = objMainClass.GetTaTaSkyBERReportforSAPUpload(txtFromDate.Text, txtToDate.Text);
                    gvRepairReport.DataSource = dt;
                    gvRepairReport.DataBind();
                    lgrecordcount.InnerText        = "Records : " + dt.Rows.Count.ToString();
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

        public void ResetFormControl()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
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

        protected void gvRepairReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        DataRowView dr = (DataRowView)e.Row.DataItem;
                        string LISTDESC = e.Row.Cells[0].Text;
                        string LISTDESC1 = e.Row.Cells[1].Text;
                        string LISTDESC2 = e.Row.Cells[2].Text;

                        HiddenField hdpartvals = e.Row.FindControl("hdPartsValue") as HiddenField;

                        // Initialize the value
                        Label lblActivity1 = e.Row.FindControl("lblActivity1") as Label;
                        Label lblActivity2 = e.Row.FindControl("lblActivity2") as Label;
                        Label lblActivity3 = e.Row.FindControl("lblActivity3") as Label;
                        Label lblActivity4 = e.Row.FindControl("lblActivity4") as Label;
                        Label lblActivity5 = e.Row.FindControl("lblActivity5") as Label;
                        //Label lblActivity6 = e.Row.FindControl("lblActivity6") as Label;
                        //Label lblActivity7 = e.Row.FindControl("lblActivity7") as Label;
                        //Label lblActivity8 = e.Row.FindControl("lblActivity8") as Label;
                        //Label lblActivity9 = e.Row.FindControl("lblActivity9") as Label;

                        if (hdpartvals.Value != null && hdpartvals.Value != string.Empty)
                        {
                            string partsvalue   = hdpartvals.Value;
                            string[] partsList  = partsvalue.Split(',');
                            if(partsList.Count()>=2)
                            {
                                lblActivity1.Text = partsList[1].ToString();
                            }

                            if (partsList.Count() >= 3)
                            {
                                lblActivity2.Text = partsList[2].ToString();
                            }

                            if (partsList.Count() >= 4)
                            {
                                lblActivity3.Text = partsList[3].ToString();
                            }

                            if (partsList.Count() >= 5)
                            {
                                lblActivity4.Text = partsList[4].ToString();
                            }

                            if (partsList.Count() >= 6)
                            {
                                lblActivity5.Text = partsList[5].ToString();
                            }

                            //if (partsList.Count() >= 7)
                            //{
                            //    lblActivity6.Text = partsList[6].ToString();
                            //}

                            //if (partsList.Count() >= 8)
                            //{
                            //    lblActivity7.Text = partsList[7].ToString();
                            //}

                            //if (partsList.Count() >= 9)
                            //{
                            //    lblActivity8.Text = partsList[8].ToString();
                            //}

                            //if (partsList.Count() >= 10)
                            //{
                            //    lblActivity9.Text = partsList[9].ToString();
                            //}
                        }
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