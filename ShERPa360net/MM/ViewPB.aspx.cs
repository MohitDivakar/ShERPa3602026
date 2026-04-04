using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.MM
{
    public partial class ViewPB : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["EditPONo"] = string.Empty;
            Session["EditPONo"] = null;
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

                        if (FormRights.bAdd == false) //if (objDALUserRights.bView == false)
                        {
                            lnkNewPB.Enabled = false;
                        }
                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();

                        DataTable dt = new DataTable();
                        dt = objMainClass.GetPurchaseBill(txtFromDate.Text, txtToDate.Text, txtPBNO.Text, txtPONO.Text);
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
        }

        protected void lnkSerchPB_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objMainClass.GetPurchaseBill(txtFromDate.Text, txtToDate.Text, txtPBNO.Text, txtPONO.Text);

                if (dt.Rows.Count > 0)
                {
                    gvList.DataSource = dt;
                    gvList.DataBind();
                }
                else
                {
                    gvList.DataSource = string.Empty;
                    gvList.DataBind();

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
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
                string attachment = "attachment; filename=PurchaseBill.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vdn.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gvList.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
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
                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                string pbno = grdrow.Cells[1].Text;
                DataTable dtPB = new DataTable();
                DataTable dtPBDTL = new DataTable();
                DataTable dtPBCOND = new DataTable();
                DataTable dtPBCHG = new DataTable();
                dtPB = objMainClass.GetEachPurchaseBill(pbno, "PBMASTER");
                dtPBDTL = objMainClass.GetEachPurchaseBill(pbno, "PBDETAIL");
                dtPBCOND = objMainClass.GetEachPurchaseBill(pbno, "PBTAX");
                dtPBCHG = objMainClass.GetEachPurchaseBill(pbno, "PBCHARGES");
                if (dtPB.Rows.Count > 0)
                {

                    lblDoctype.Text = Convert.ToString(dtPB.Rows[0]["PBTYPE"]);
                    lblPBDate.Text = Convert.ToDateTime(dtPB.Rows[0]["PBDT"]).ToShortDateString();
                    lblPBNo.Text = Convert.ToString(dtPB.Rows[0]["PBNO"]);
                    lblBillDate.Text = Convert.ToDateTime(dtPB.Rows[0]["BILLDT"]).ToShortDateString();
                    lblPBNo.Text = Convert.ToString(dtPB.Rows[0]["PBNO"]);
                    lblBillNo.Text = Convert.ToString(dtPB.Rows[0]["BILLNO"]);
                    lblRemark.Text = Convert.ToString(dtPB.Rows[0]["REMARK"]);

                    lblPBVendor.Text = Convert.ToString(dtPB.Rows[0]["VENDNAME"]);
                    lblPBPayTerms.Text = Convert.ToString(dtPB.Rows[0]["PMTTERMS"]);
                    lblPBPayTermsDesc.Text = Convert.ToString(dtPB.Rows[0]["PMTTERMSDESC"]);

                    lblNetPBAmt.Text = Convert.ToString(dtPB.Rows[0]["NETPBAMT"]);
                    lblMaterialAmt.Text = Convert.ToString(dtPB.Rows[0]["NETMATVALUE"]);
                    lblTaxAmt.Text = Convert.ToString(dtPB.Rows[0]["NETTAXAMT"]);
                    lblDiscountAmt.Text = Convert.ToString(dtPB.Rows[0]["NETDISCOUNT"]);
                    lblOtherChg.Text = Convert.ToString(dtPB.Rows[0]["OTHERCHARGES"]);

                    gvDetail.DataSource = dtPBDTL;
                    gvDetail.DataBind();

                    grvTaxation.DataSource = dtPBCOND;
                    grvTaxation.DataBind();

                    grvOtherChg.DataSource = dtPBCHG;
                    grvOtherChg.DataBind();

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                }
                else
                {
                    gvDetail.DataSource = string.Empty;
                    gvDetail.DataBind();

                    grvTaxation.DataSource = string.Empty;
                    grvTaxation.DataBind();

                    grvOtherChg.DataSource = string.Empty;
                    grvOtherChg.DataBind();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                string pbno = grdrow.Cells[1].Text;
                string path = "ViewPurchaseBillPDF.aspx";
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "?PBNO=" + pbno + "');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}