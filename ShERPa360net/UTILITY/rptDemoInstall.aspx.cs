using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.UTILITY
{

    public partial class rptDemoInstall : System.Web.UI.Page
    {

        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        WAClass objWAClass = new WAClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Session["USERID"] != null)
                    {
                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menutabid.Value, "");
                        if (FormRights.bView == false)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();

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
                    DataTable dtMain = new DataTable();
                    dtMain = objMainClass.GetInstallationData(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, txtSINo.Text, "", 0, "GETDATAREPORT1", Convert.ToInt32(ddlCustType.SelectedItem.Value), 
                        Convert.ToInt32(ddlFinalEntry.SelectedItem.Value), Convert.ToInt32(ddlActualDispatch.SelectedItem.Value));

                    if (dtMain.Rows.Count > 0)
                    {
                        gvList.DataSource = dtMain;
                        gvList.DataBind();
                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                        Session["DataDemoInstallation"] = dtMain;
                    }
                    else
                    {
                        gvList.DataSource = string.Empty;
                        gvList.DataBind();
                        Session["DataDemoInstallation"] = null;
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

        protected void lnkSearhSI_Click(object sender, EventArgs e)
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

        protected void btnDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    Label lblSONO = (Label)grdrow.FindControl("lblSONO");
                    Label lblSRNO = (Label)grdrow.FindControl("lblSRNO");

                    Label lblSINO = (Label)grdrow.FindControl("lblSINO");
                    Label lblITEMCODE = (Label)grdrow.FindControl("lblITEMCODE");
                    Label lblITEMDESC = (Label)grdrow.FindControl("lblITEMDESC");
                    Label lblJOBID = (Label)grdrow.FindControl("lblJOBID");
                    Label lblCUSTNAME = (Label)grdrow.FindControl("lblCUSTNAME");
                    Label lblCUSTMOBILENO = (Label)grdrow.FindControl("lblCUSTMOBILENO");

                    DataTable dtDetails = new DataTable();
                    dtDetails = objMainClass.GetInstallationData(objMainClass.intCmpId, "", "", "", lblSONO.Text, Convert.ToInt32(lblSRNO.Text), "GETDEMOINSTDATA", 0, 0,0);

                    if (dtDetails.Rows.Count > 0)
                    {
                        lblPopSINO.Text = lblSINO.Text;
                        lblPopSONO.Text = lblSONO.Text;
                        lblPopSrNo.Text = lblSRNO.Text;
                        lblPopJobid.Text = lblJOBID.Text;
                        lblPopCustName.Text = lblCUSTNAME.Text;
                        lblPopContactNo.Text = lblCUSTMOBILENO.Text;
                        lblPopItemCode.Text = lblITEMCODE.Text;
                        lblPopItemDesc.Text = lblITEMDESC.Text;


                        gvDetails.DataSource = dtDetails;
                        gvDetails.DataBind();

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');setTimeout(function() {$('#modal-warning').modal('hide');}, 2000);", true);
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

        protected void lnkExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = Session["DataDemoInstallation"] as DataTable;
                    string fileName = "DemoInstallationList";
                    HttpContext context = HttpContext.Current;
                    context.Response.Clear();
                    context.Response.Buffer = true;
                    context.Response.ContentType = "application/vnd.ms-excel";
                    context.Response.AddHeader("content-disposition", $"attachment;filename={fileName}.xls");

                    using (StringWriter sw = new StringWriter())
                    {
                        using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                        {
                            GridView gv = new GridView();
                            gv.DataSource = dt;
                            gv.DataBind();
                            gv.RenderControl(hw);

                            context.Response.Output.Write(sw.ToString());
                            context.Response.Flush();
                            context.Response.End();
                        }
                    }


                    //string attachment = "attachment; filename=KeepaPrice.xls";
                    //Response.ClearContent();
                    //Response.AddHeader("content-disposition", attachment);
                    //Response.ContentType = "application/vdn.ms-excel";
                    //StringWriter sw = new StringWriter();
                    //HtmlTextWriter htw = new HtmlTextWriter(sw);
                    //gvList.RenderControl(htw);
                    //Response.Write(sw.ToString());
                    //Response.End();
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