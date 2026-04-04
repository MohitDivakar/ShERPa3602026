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
    public partial class ViewPO : System.Web.UI.Page
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
                            //   Session.Abandon();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        if (FormRights.bAdd == false) //if (objDALUserRights.bView == false)
                        {
                            lnkNewPO.Enabled = false;
                        }
                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();

                        DataTable dt = new DataTable();
                        dt = objMainClass.GetPOData(objMainClass.intCmpId, txtPRNO.Text, txtPONO.Text, txtFromDate.Text, txtToDate.Text, "");
                        if (dt.Rows.Count > 0)
                        {
                            gvList.DataSource = dt;
                            gvList.DataBind();
                        }
                        else
                        {
                            gvList.DataSource = string.Empty;
                            gvList.DataBind();
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
                        }


                        //menuRptid
                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menuRptid.Value, "");
                        if (FormRights.bPrint == false)
                        {
                            for (int i = 0; i < gvList.Rows.Count; i++)
                            {
                                LinkButton btnPrint = (LinkButton)gvList.Rows[i].Cells[11].FindControl("btnPrint");
                                Label lblLine = (Label)gvList.Rows[i].Cells[11].FindControl("lblLine");
                                btnPrint.Visible = false;
                                lblLine.Visible = false;
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

        protected void lnkSerchPO_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objMainClass.GetPOData(objMainClass.intCmpId, txtPRNO.Text, txtPONO.Text, txtFromDate.Text, txtToDate.Text, "");

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

                objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menuRptid.Value, "");

                if (FormRights.bPrint == false)
                {
                    for (int i = 0; i < gvList.Rows.Count; i++)
                    {
                        LinkButton btnPrint = (LinkButton)gvList.Rows[i].Cells[11].FindControl("btnPrint");
                        Label lblLine = (Label)gvList.Rows[i].Cells[11].FindControl("lblLine");
                        btnPrint.Visible = false;
                        lblLine.Visible = false;
                    }
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
                string attachment = "attachment; filename=POLIST.xls";
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
                throw ex;
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void bntView_Click(object sender, EventArgs e)
        {

            lblAPRV1.Visible = false;
            lblAPRV1.Text = string.Empty;
            try
            {
                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

                string pono = grdrow.Cells[1].Text;

                DataTable dtPOMST = new DataTable();
                DataTable dtPODTL = new DataTable();
                DataTable dtPOCOND = new DataTable();
                DataTable dtPOCHG = new DataTable();
                dtPOMST = objMainClass.SelectPOData(objMainClass.intCmpId, pono, 1);
                dtPODTL = objMainClass.SelectPOData(objMainClass.intCmpId, pono, 2);
                dtPOCOND = objMainClass.SelectPOData(objMainClass.intCmpId, pono, 3);
                dtPOCHG = objMainClass.SelectPOData(objMainClass.intCmpId, pono, 4);
                if (dtPOMST.Rows.Count > 0)
                {

                    lblDoctype.Text = Convert.ToString(dtPOMST.Rows[0]["POTYPE"]);
                    lblPODate.Text = Convert.ToDateTime(dtPOMST.Rows[0]["PODTD"]).ToShortDateString();
                    lblPONo.Text = Convert.ToString(dtPOMST.Rows[0]["PONO"]);
                    lblRemark.Text = Convert.ToString(dtPOMST.Rows[0]["REMARK"]);

                    lblPOVendor.Text = Convert.ToString(dtPOMST.Rows[0]["VENDNAME"]);
                    lblPOTransporter.Text = Convert.ToString(dtPOMST.Rows[0]["TRANNAME"]);
                    lblPOPayTerms.Text = Convert.ToString(dtPOMST.Rows[0]["PMTTERMS"]);
                    lblPOPayTermsDesc.Text = Convert.ToString(dtPOMST.Rows[0]["PMTTERMSDESC"]);

                    lblNetPOAmt.Text = Convert.ToString(dtPOMST.Rows[0]["NETPOAMT"]);
                    lblMaterialAmt.Text = Convert.ToString(dtPOMST.Rows[0]["NETMATVALUE"]);
                    lblTaxAmt.Text = Convert.ToString(dtPOMST.Rows[0]["NETTAXAMT"]);
                    lblDiscountAmt.Text = Convert.ToString(dtPOMST.Rows[0]["DISCOUNT"]);
                    lblOtherChg.Text = Convert.ToString(dtPOMST.Rows[0]["OTHERCHARGES"]);


                    gvDetail.DataSource = dtPODTL;
                    gvDetail.DataBind();

                    grvTaxation.DataSource = dtPOCOND;
                    grvTaxation.DataBind();

                    grvOtherChg.DataSource = dtPOCHG;
                    grvOtherChg.DataBind();

                    DataTable logDT = new DataTable();
                    logDT = objMainClass.SELECT_REQUISITION_LOG(pono);
                    if (logDT.Rows.Count > 0)
                    {
                        for (int k = 0; k < logDT.Rows.Count; k++)
                        {
                            lblAPRV1.Text = lblAPRV1.Text + " <br/>" + Convert.ToString(logDT.Rows[k]["LISTDESC"]) + "  By  " + Convert.ToString(logDT.Rows[k]["USERNAME"]) + "  On  " + Convert.ToString(logDT.Rows[k]["APRVDATE"]) + " , Reason : " + Convert.ToString(logDT.Rows[k]["REASON"]);
                            lblAPRV1.Visible = true;
                        }
                    }

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                }
                else
                {
                    gvDetail.DataSource = string.Empty;
                    gvDetail.DataBind();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

            string prno = grdrow.Cells[1].Text;

            Response.Redirect("CreatePO.aspx?PONO=" + prno, true);

        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string LISTDESC = e.Row.Cells[9].Text;
                if ((LISTDESC != null && LISTDESC != "&nbsp;" && LISTDESC != "" && LISTDESC != "REJECTED"))
                {
                    //e.Row.FindControl("btnEdit").Visible = false;
                    //e.Row.FindControl("lblStick").Visible = false;
                }
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {

                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                string docno = grdrow.Cells[1].Text;

                string path = "ViewPOPDF.aspx";
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "?PONO=" + docno + "');", true);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}