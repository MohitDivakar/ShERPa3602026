using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.UTILITY
{
    public partial class frmSearchAllJobID : System.Web.UI.Page
    {

        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        WAClass objWAClass = new WAClass();
        DataTable dtMain = new DataTable();
        public SqlConnection ConnSherpa = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStringSherpa"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    if (Session["USERID"] != null)
                    {
                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menutabid.Value, "");
                        if (FormRights.bView == false)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        objBindDDL.FillBrandCroma(ddlBrand, "GETBRANDS");
                        objBindDDL.FillBrandwiseCategoryCroma(ddlCategory, objMainClass.intCmpId, ddlBrand.SelectedIndex > 0 ? ddlBrand.SelectedItem.Text : "", "GETBRANDCATEGORY");
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    string itemdesc = "%" + txtItemDesc.Text.Replace(" ", "%%") + "%";
                    dt = objMainClass.dtGetLikeJobData(itemdesc, ddlBrand.SelectedIndex > 0 ? ddlBrand.SelectedValue : "", ddlCategory.SelectedIndex > 0 ? ddlCategory.SelectedValue : "", "GETALLJOBID");
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

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    gvList.DataSource = null;
                    gvList.DataBind();
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

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        Label lblColor = (Label)e.Row.FindControl("lblColor");
                        Label lblAVAILSTAT = (Label)e.Row.FindControl("lblAVAILSTAT");
                        LinkButton btnWhatsapp = (LinkButton)e.Row.FindControl("btnWhatsapp");


                        GridViewRow row = (GridViewRow)e.Row;
                        row.CssClass = lblColor.Text;

                        if (lblAVAILSTAT.Text == "ORDERED")
                        {
                            btnWhatsapp.Visible = false;
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

        protected void lnkJobid_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    string lblJOBID = ((Label)grdrow.FindControl("lblJOBID")).Text;

                    //if (gvList.Rows.Count > 0)
                    //{
                    //    gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //}

                    string path = "frmNewCromaJobID.aspx";
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "?JOBID=" + lblJOBID + "');", true);

                    //if (gvList.Rows.Count > 0)
                    //{
                    //    gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //}

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

        protected void btnWhatsapp_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    Label lblAVAILSTAT = (Label)grdrow.FindControl("lblAVAILSTAT");
                    Label lblJOBID = (Label)grdrow.FindControl("lblJOBID");
                    Label lblCATEGORY = (Label)grdrow.FindControl("lblCATEGORY");
                    Label lblBRAND = (Label)grdrow.FindControl("lblBRAND");
                    Label lblITEMCODE = (Label)grdrow.FindControl("lblITEMCODE");
                    Label lblIETMDESC = (Label)grdrow.FindControl("lblIETMDESC");
                    Label lblSERIALNO = (Label)grdrow.FindControl("lblSERIALNO");
                    Label lblGRADE = (Label)grdrow.FindControl("lblGRADE");
                    Label lblLOCATION = (Label)grdrow.FindControl("lblLOCATION");
                    Label lblLOCCD = (Label)grdrow.FindControl("lblLOCCD");
                    Label lblPLANTCD = (Label)grdrow.FindControl("lblPLANTCD");

                    lblPopAVAILSTAT.Text = lblAVAILSTAT.Text;
                    lblPopJOBID.Text = lblJOBID.Text;
                    lblPopCATEGORY.Text = lblCATEGORY.Text;
                    lblPopBRAND.Text = lblBRAND.Text;
                    lblPopITEMCODE.Text = lblITEMCODE.Text;
                    lblPopIETMDESC.Text = lblIETMDESC.Text;
                    lblPopSERIALNO.Text = lblSERIALNO.Text;
                    lblPopGRADE.Text = lblGRADE.Text;
                    lblPopLOCATION.Text = lblLOCATION.Text;
                    lblPopLOCCD.Text = lblLOCCD.Text;
                    lblPopPLANTCD.Text = lblPLANTCD.Text;

                    objBindDDL.FillLists(ddlReference, "IR");

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);

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


        protected static string fileread()
        {
            string actualfol;
            actualfol = HttpContext.Current.Server.MapPath("InformMailBody.html");
            StreamReader sr = new StreamReader(actualfol);
            string strcontent = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            return strcontent;
        }


        protected void btnCreateDoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    string sendtoname = "";
                    string type = "CLR";
                    DataTable dtSentto = new DataTable();
                    dtSentto = objMainClass.GetReqRcptName(objMainClass.intCmpId, lblPopAVAILSTAT.Text, lblPopPLANTCD.Text, "", "", "", "", "SELECT");
                    if (dtSentto.Rows.Count > 0)
                    {
                        sendtoname = Convert.ToString(dtSentto.Rows[0]["NAME"]);
                    }


                    int i = objMainClass.InsertManualLead(DateTime.Now.ToString(), txtCustName.Text, txtCustMobileNo.Text, "", lblPopBRAND.Text, "", "", "", "", "", Convert.ToString(Session["USERID"]),
                        (int)LeadStatus.Saved, Convert.ToString(Session["USERID"]), lblPopIETMDESC.Text, ddlReference.SelectedItem.Text, "INSERT", objMainClass.intCmpId, "11391",
                        Convert.ToInt32(ddlReference.SelectedValue), 11933, 3026, 1, "AHMEDABAD", lblPopCATEGORY.Text, "", "", type, sendtoname);
                    if (i == 1)
                    {

                        if (dtSentto.Rows.Count > 0)
                        {

                            string mailto = Convert.ToString(dtSentto.Rows[0]["MAILTO"]);
                            string mailcc = Convert.ToString(dtSentto.Rows[0]["MAILCC"]);

                            String strCustContent = "";
                            strCustContent = fileread();
                            strCustContent = strCustContent.Replace("###CUSTNAME###", txtCustName.Text);
                            strCustContent = strCustContent.Replace("###CONTACTNO###", txtCustMobileNo.Text);
                            strCustContent = strCustContent.Replace("###PRODUCTCATEGORY###", lblPopCATEGORY.Text);
                            strCustContent = strCustContent.Replace("###BRAND###", lblPopBRAND.Text);
                            strCustContent = strCustContent.Replace("###ITEM###", lblPopITEMCODE.Text + " - " + lblPopIETMDESC.Text);
                            strCustContent = strCustContent.Replace("###JOBID###", lblPopJOBID.Text + " - " + lblPopSERIALNO.Text + " - " + lblPopAVAILSTAT.Text);
                            strCustContent = strCustContent.Replace("###LOCATION###", lblPopLOCATION.Text + " - " + lblPopLOCCD.Text);
                            strCustContent = strCustContent.Replace("###Message###", "Please contact customer as soon as possible.!");

                            objMainClass.SaveMailNotification(objMainClass.EmailID, objMainClass.Password, mailto, mailcc, "Customer Requirement", strCustContent, objMainClass.PORT, "", Convert.ToString(Session["USERID"]), "CRL");

                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Record added successfully. Mail sent to concern person.!');$('.close').click(function(){window.location.href ='frmSearchAllJobID.aspx' });", true);

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Record added successfully. But not sent to concern person. Please inform concern person manualy.!');$('.close').click(function(){window.location.href ='frmSearchAllJobID.aspx' });", true);
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Something went wrong. Record not added. Please try again.!');", true);
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

        protected void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    objBindDDL.FillLists(ddlNewReference, "IR");
                    objBindDDL.FillProductItemSubGroup(ddlNewProduct, 1);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-New').modal();", true);
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

        protected void ddlNewProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    objBindDDL.FillSpecandValue(ddlNewSpecs, 1, Convert.ToInt32(ddlNewProduct.SelectedItem.Value), "GETSPECMSTID");
                    if (ddlNewSpecs.SelectedIndex > -1)
                    {
                        objBindDDL.FillSpecandValue(ddlNewSpecValue, 1, Convert.ToInt32(ddlNewSpecs.SelectedItem.Value), "GETSPECVALUESMST");
                    }
                    else
                    {
                        ddlNewSpecValue.DataSource = null;
                        ddlNewSpecValue.DataBind();
                    }
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-New').modal();", true);
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

        protected void ddlNewSpecs_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    objBindDDL.FillSpecandValue(ddlNewSpecValue, 1, Convert.ToInt32(ddlNewSpecs.SelectedItem.Value), "GETSPECVALUESMST");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-New').modal();", true);
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

        protected void btnSaveNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    //int i = objMainClass.InsertManualLead(DateTime.Now.ToString(), txtCustName.Text, txtCustMobileNo.Text, "", lblPopBRAND.Text, "", "", "", "", "", Convert.ToString(Session["USERID"]), (int)LeadStatus.Saved,
                    //   Convert.ToString(Session["USERID"]), lblPopIETMDESC.Text, ddlReference.SelectedItem.Text, "INSERT", objMainClass.intCmpId, "11391", Convert.ToInt32(ddlReference.SelectedValue), 11933, 3026, 1, "AHMEDABAD", lblPopCATEGORY.Text, "", "");
                    string sendtoname = "";
                    string type = "CLR";
                    DataTable dtSentto = new DataTable();
                    dtSentto = objMainClass.GetReqRcptName(objMainClass.intCmpId, "NEW", "1019", "", "", "", "", "SELECT");
                    if (dtSentto.Rows.Count > 0)
                    {
                        sendtoname = Convert.ToString(dtSentto.Rows[0]["NAME"]);

                    }

                    int i = objMainClass.InsertManualLead(DateTime.Now.ToString(), txtNewCustName.Text, txtNewContact.Text, "", txtNewMake.Text, txtNewModel.Text, "", "", "", "", Convert.ToString(Session["USERID"]), (int)LeadStatus.Saved,
                        Convert.ToString(Session["USERID"]), txtNewCustRemarks.Text, ddlNewReference.SelectedItem.Text, "INSERT", objMainClass.intCmpId, "11391", Convert.ToInt32(ddlNewReference.SelectedValue), 11933, 3026, 1, "AHMEDABAD",
                        ddlNewProduct.SelectedItem.Text, ddlNewSpecs.Items.Count > 0 ? ddlNewSpecs.SelectedItem.Text : "", ddlNewSpecValue.Items.Count > 0 ? ddlNewSpecValue.SelectedItem.Text : "", type, sendtoname);

                    if (i == 1)
                    {

                        if (dtSentto.Rows.Count > 0)
                        {

                            string mailto = Convert.ToString(dtSentto.Rows[0]["MAILTO"]);
                            string mailcc = Convert.ToString(dtSentto.Rows[0]["MAILCC"]);

                            String strCustContent = "";
                            strCustContent = fileread();
                            strCustContent = strCustContent.Replace("###CUSTNAME###", txtNewCustName.Text);
                            strCustContent = strCustContent.Replace("###CONTACTNO###", txtNewContact.Text);
                            strCustContent = strCustContent.Replace("###PRODUCTCATEGORY###", ddlNewProduct.SelectedItem.Text);
                            strCustContent = strCustContent.Replace("###BRAND###", txtNewMake.Text + " - " + txtNewModel.Text);
                            strCustContent = strCustContent.Replace("###ITEM###", (ddlNewSpecs.Items.Count > 0 ? ddlNewSpecs.SelectedItem.Text : "") + " - " + (ddlNewSpecValue.Items.Count > 0 ? ddlNewSpecValue.SelectedItem.Text : "") + " (" + txtNewCustRemarks.Text + ")");
                            strCustContent = strCustContent.Replace("###JOBID###", "");
                            strCustContent = strCustContent.Replace("###LOCATION###", "");
                            strCustContent = strCustContent.Replace("###Message###", "Please contact customer as soon as possible.!");

                            objMainClass.SaveMailNotification(objMainClass.EmailID, objMainClass.Password, mailto, mailcc, "Customer Requirement", strCustContent, objMainClass.PORT, "", Convert.ToString(Session["USERID"]), "CRL");

                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Record added successfully. Mail sent to concern person.!');$('.close').click(function(){window.location.href ='frmSearchAllJobID.aspx' });", true);

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Record added successfully. But not sent to concern person. Please inform concern person manualy.!');$('.close').click(function(){window.location.href ='frmSearchAllJobID.aspx' });", true);
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Something went wrong. Record not added. Please try again.!');", true);
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

        protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    objBindDDL.FillBrandwiseCategoryCroma(ddlCategory, objMainClass.intCmpId, ddlBrand.SelectedIndex > 0 ? ddlBrand.SelectedItem.Text : "", "GETBRANDCATEGORY");
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