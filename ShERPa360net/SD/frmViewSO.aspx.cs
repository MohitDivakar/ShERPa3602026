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
    public partial class frmViewSO : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["EditSONo"] = string.Empty;
            Session["EditSONo"] = null;
            Session["CallingIdDetail"] = string.Empty;
            Session["CallingIdDetail"] = null;
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
                            lnkNewSO.Enabled = false;
                        }

                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();

                        objBindDDL.FillDocType(ddlSOTYPE, "SO");
                        ddlSOTYPE.SelectedIndex = 1;

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
                    dt = objMainClass.GetSOSearchData(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, txtSONO.Text, "SEARCHSO", Convert.ToInt32(ddlStatus.SelectedValue),
                        ddlSOTYPE.SelectedValue, chkPending.Checked == true ? 1 : 0);
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

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

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
                    string attachment = "attachment; filename=SOLIST.xls";
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

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

                    string SOno = grdrow.Cells[1].Text;
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetSODetails(objMainClass.intCmpId, "", "", "GETDCSO", "", SOno);
                    if (dt.Rows.Count > 0)
                    {
                        lblSODET.Text = "DC created for this SO.You cannot edit the SO.";
                        lblSONO.Text = "SO No. :-  " + Convert.ToString(dt.Rows[0]["SONO"]);
                        lblDCNO.Text = "DC No. :-  " + Convert.ToString(dt.Rows[0]["DOCNO"]);
                        lblSINO.Text = "SO No. :-  " + Convert.ToString(dt.Rows[0]["SINO"]);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-SODet').modal();", true);
                    }
                    else
                    {
                        Response.Redirect("frmSO.aspx?EditSONo=" + SOno, false);
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

        protected void bntView_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

                    string SOno = grdrow.Cells[1].Text;

                    DataTable dtSOMST = new DataTable();
                    DataTable dtSODTL = new DataTable();
                    DataTable dtSOCOND = new DataTable();
                    DataTable dtSOCHG = new DataTable();
                    dtSOMST = objMainClass.GetSODetails(objMainClass.intCmpId, "", "", "GETSOMST", "", SOno);
                    dtSODTL = objMainClass.GetSODetails(objMainClass.intCmpId, "", "", "EDITSODTL", "", SOno);
                    dtSOCOND = objMainClass.GetSODetails(objMainClass.intCmpId, "", "", "EDITSOCOND", "", SOno);
                    dtSOCHG = objMainClass.GetSODetails(objMainClass.intCmpId, "", "", "EDITSOCHG", "", SOno);
                    if (dtSOMST.Rows.Count > 0)
                    {

                        lblPopDocType.Text = Convert.ToString(dtSOMST.Rows[0]["SOTYPE"]);
                        lblPopSegment.Text = Convert.ToString(dtSOMST.Rows[0]["SEGMENT"]);
                        lblPopDistChnl.Text = Convert.ToString(dtSOMST.Rows[0]["DISTCHNL"]);
                        lblPopSODate.Text = Convert.ToString(dtSOMST.Rows[0]["SODTD"]);
                        lblPopSONO.Text = Convert.ToString(dtSOMST.Rows[0]["SONO"]);
                        lblPopRefNo.Text = Convert.ToString(dtSOMST.Rows[0]["REFNO"]);
                        lblPopRefDate.Text = Convert.ToString(dtSOMST.Rows[0]["REFDTD"]);
                        lblPopAgent.Text = Convert.ToString(dtSOMST.Rows[0]["AGENT"]);
                        lblPopCustomer.Text = Convert.ToString(dtSOMST.Rows[0]["CUSTNAME"]);
                        lblPopShipper.Text = Convert.ToString(dtSOMST.Rows[0]["SHIPNAME"]);
                        lblPopPayterms.Text = Convert.ToString(dtSOMST.Rows[0]["PMTT"]);
                        lblPopSaleChnl.Text = Convert.ToString(dtSOMST.Rows[0]["SCHEME"]);
                        lblPopRemarks.Text = Convert.ToString(dtSOMST.Rows[0]["REMARK"]);
                        lblPopNetAmt.Text = Convert.ToString(dtSOMST.Rows[0]["NETMATVALUE"]);
                        lblPopTaxAmt.Text = Convert.ToString(dtSOMST.Rows[0]["NETTAXAMT"]);
                        lblPopDiscount.Text = Convert.ToString(dtSOMST.Rows[0]["DISCOUNT"]);
                        lblPopTotalAmt.Text = Convert.ToString(dtSOMST.Rows[0]["NETSOAMT"]);

                        if (dtSOCHG.Rows.Count > 0)
                        {
                            decimal others = 0;
                            for (int i = 0; i < dtSOCHG.Rows.Count; i++)
                            {
                                others += Convert.ToDecimal(Convert.ToString(dtSOCHG.Rows[i]["CHRGAMOUNT"]));
                            }
                            lblPopOtherCharges.Text = Convert.ToString(others);
                        }
                        else
                        {
                            lblPopOtherCharges.Text = "0";
                        }


                        lblPopSalesChnl.Text = Convert.ToString(dtSOMST.Rows[0]["SALESFROMDESC"]);
                        lblPopRetialCust.Text = Convert.ToString(dtSOMST.Rows[0]["CUSTNAME"]);
                        txtPopAddress1.Text = Convert.ToString(dtSOMST.Rows[0]["CUSTADD1"]);
                        txtPopAddress2.Text = Convert.ToString(dtSOMST.Rows[0]["CUSTADD2"]);
                        txtPopAddress3.Text = Convert.ToString(dtSOMST.Rows[0]["CUSTADD3"]);
                        lblPopPincode.Text = Convert.ToString(dtSOMST.Rows[0]["PINCODE"]);
                        lblPopCity.Text = Convert.ToString(dtSOMST.Rows[0]["CITY"]);
                        lblPopState.Text = Convert.ToString(dtSOMST.Rows[0]["STATE"]);
                        lblPopMobileNo.Text = Convert.ToString(dtSOMST.Rows[0]["CUSTMOBILENO"]);
                        lblPopEmail.Text = Convert.ToString(dtSOMST.Rows[0]["CUSTEMAILID"]);

                        grvListItem.DataSource = dtSODTL;
                        grvListItem.DataBind();

                        grvTaxation.DataSource = dtSOCOND;
                        grvTaxation.DataBind();

                        grvCharges.DataSource = dtSOCHG;
                        grvCharges.DataBind();



                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-SOView').modal();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Records Found With This SO No.!');", true);
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