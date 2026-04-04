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
    public partial class frmConsumptionReport : System.Web.UI.Page
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
                        BindIRReport();
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
                    BindIRReport();
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
                BindIRReport();
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
                string filename = "Consumption Report" + txtFromDate.Text + "-" + txtToDate.Text;
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
        public void BindIRReport()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt                   = new DataTable();
                    dt                             = objMainClass.GetTaTaSkyConsumptionReportforSAPUpload(txtFromDate.Text, txtToDate.Text);
                    gvRepairReport.DataSource      = dt;
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
                        Label lblCom1   = e.Row.FindControl("lblCom1") as Label;
                        Label lblCom2   = e.Row.FindControl("lblCom2") as Label;
                        Label lblCom3   = e.Row.FindControl("lblCom3") as Label;
                        Label lblCom4   = e.Row.FindControl("lblCom4") as Label;
                        Label lblCom5   = e.Row.FindControl("lblCom5") as Label;
                        Label lblCom6   = e.Row.FindControl("lblCom6") as Label;
                        Label lblCom7   = e.Row.FindControl("lblCom7") as Label;
                        Label lblCom8   = e.Row.FindControl("lblCom8") as Label;
                        Label lblCom9   = e.Row.FindControl("lblCom9") as Label;
                        Label lblCom10  = e.Row.FindControl("lblCom10") as Label;
                        Label lblCom11  = e.Row.FindControl("lblCom11") as Label;
                        Label lblCom12  = e.Row.FindControl("lblCom12") as Label;
                        Label lblCom13  = e.Row.FindControl("lblCom13") as Label;
                        Label lblCom14  = e.Row.FindControl("lblCom14") as Label;
                        Label lblCom15  = e.Row.FindControl("lblCom15") as Label;
                        Label lblCom16  = e.Row.FindControl("lblCom16") as Label;

                        if (hdpartvals.Value != null && hdpartvals.Value != string.Empty)
                        {
                            string partsvalue   = hdpartvals.Value;
                            string[] partsList  = partsvalue.Split(',');
                            if(partsList.Count()>=2)
                            {
                                lblCom1.Text = partsList[1].ToString();
                            }

                            if (partsList.Count() >= 3)
                            {
                                lblCom2.Text = partsList[2].ToString();
                            }

                            if (partsList.Count() >= 4)
                            {
                                lblCom3.Text = partsList[3].ToString();
                            }

                            if (partsList.Count() >= 5)
                            {
                                lblCom4.Text = partsList[4].ToString();
                            }

                            if (partsList.Count() >= 6)
                            {
                                lblCom5.Text = partsList[5].ToString();
                            }

                            if (partsList.Count() >= 7)
                            {
                                lblCom6.Text = partsList[6].ToString();
                            }

                            if (partsList.Count() >= 8)
                            {
                                lblCom7.Text = partsList[7].ToString();
                            }

                            if (partsList.Count() >= 9)
                            {
                                lblCom8.Text = partsList[8].ToString();
                            }

                            if (partsList.Count() >= 10)
                            {
                                lblCom9.Text = partsList[9].ToString();
                            }

                            if (partsList.Count() >= 11)
                            {
                                lblCom10.Text = partsList[10].ToString();
                            }

                            if (partsList.Count() >= 12)
                            {
                                lblCom11.Text = partsList[11].ToString();
                            }

                            if (partsList.Count() >= 13)
                            {
                                lblCom12.Text = partsList[12].ToString();
                            }

                            if (partsList.Count() >= 14)
                            {
                                lblCom13.Text = partsList[13].ToString();
                            }

                            if (partsList.Count() >= 15)
                            {
                                lblCom14.Text = partsList[14].ToString();
                            }

                            if (partsList.Count() >= 16)
                            {
                                lblCom15.Text = partsList[15].ToString();
                            }

                            if (partsList.Count() >= 17)
                            {
                                lblCom16.Text = partsList[16].ToString();
                            }
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