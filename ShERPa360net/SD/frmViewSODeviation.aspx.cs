using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.SD
{
    public partial class frmViewSODeviation : System.Web.UI.Page
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
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }

                        if (FormRights.bAdd == false)
                        {
                            lnkNewSODeviation.Enabled = false;
                        }

                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();

                        objBindDDL.FillStatuses(ddlStatus);
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("1")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("2")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("3"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("4")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("5")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("6"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("7")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("8")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("9"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("10")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("11")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("12"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("13")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("14")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("15"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("16")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("17")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("19"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("20")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("21")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("22"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("23")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("26")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("28"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("29")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("30")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("31"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("33")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("34"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("37")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("38")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("39"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("40")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("41")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("42"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("43")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("44")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("45"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("46")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("47")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("48"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("49")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("50")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("52"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("53")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("54")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("55"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("56")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("57")); 
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("59")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("60")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("61"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("62")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("63")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("64"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("65")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("67"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("68")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("69")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("70"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("71")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("72")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("73"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("74")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("75")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("76"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("77")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("78")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("79"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("80")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("81")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("82"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("83")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("84"));
                        ddlStatus.SelectedValue = "66";
                        GetData();
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

        public void GetData()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.SearchSODeviation(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, txtSONO.Text, Convert.ToInt32(ddlStatus.SelectedValue), "SEARCHDEVIATION", 0);
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
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lnkSerchSO_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GetData();
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

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void lnkExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    try
                    {
                        if (Session["USERID"] != null)
                        {
                            string attachment = "attachment; filename=SODEVIATIONLIST.xls";
                            Response.ClearContent();
                            Response.AddHeader("content-disposition", attachment);
                            Response.ContentType = "application/vdn.ms-excel";
                            StringWriter sw = new StringWriter();
                            HtmlTextWriter htw = new HtmlTextWriter(sw);
                            gvList.RenderControl(htw);
                            Response.Write(sw.ToString());
                            Response.End();
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