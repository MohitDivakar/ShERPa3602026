using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.FI
{
    public partial class frmClosing : System.Web.UI.Page
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

                        int month = DateTime.Now.Month;
                        int year = DateTime.Now.Year;

                        if (month < 4)
                        {
                            year = year - 1;
                        }
                        string startdt = "01-04-" + year;
                        txtFromDate.Text = startdt;
                        txtToDate.Text = (Convert.ToDateTime(startdt).AddYears(1).AddSeconds(-1)).ToString("dd-MM-yyyy");
                        objBindDDL.FillLists(ddlTallyGroup, "TG");
                        ddlTallyGroup.Items.Remove(ddlTallyGroup.Items.FindByValue("0"));
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
                    lnkExport.Enabled = false;
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetVendorLedger(objMainClass.intCmpId, "", txtFromDate.Text, txtToDate.Text, "ALLCLOSING", Convert.ToInt32(ddlTallyGroup.SelectedValue));
                    if (dt.Rows.Count > 0)
                    {
                        decimal drAMT = 0;
                        decimal crAMT = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            crAMT = crAMT + Convert.ToDecimal(dt.Rows[i]["CR"]);
                            drAMT = drAMT + Convert.ToDecimal(dt.Rows[i]["DR"]);
                        }

                        dt.Rows.Add(1, "", "", crAMT, drAMT, "Total");
                        decimal closing = crAMT + (drAMT);
                        if (closing >= 0)
                        {
                            dt.Rows.Add(1, "", "Closing Balance", closing, 0, "Closing Balance");
                        }
                        if (closing < 0)
                        {
                            dt.Rows.Add(1, "", "Closing Balance", 0, Math.Abs(closing), "Closing Balance");
                        }


                        gvList.DataSource = dt;
                        gvList.DataBind();
                        lnkExport.Enabled = true;
                    }
                    else
                    {
                        gvList.DataSource = string.Empty;
                        gvList.DataBind();
                        lnkExport.Enabled = false;
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


        public override void VerifyRenderingInServerForm(Control control)
        {

        }


        protected void lnkExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    string attachment = "attachment; filename=VendorClosingBalance.xls";
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

        protected void lnkSerch_Click(object sender, EventArgs e)
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
    }
}