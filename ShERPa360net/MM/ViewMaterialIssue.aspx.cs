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
    public partial class ViewMaterialIssue : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["MRITEMSR"] = string.Empty;
            Session["QMRNO"] = string.Empty;

            if (!IsPostBack)
            {
                try
                {
                    if (Session["USERID"] != null)
                    {
                        if (Request.QueryString.Count > 0)
                        {
                            if (Convert.ToString(Request.QueryString["REQUESTFORM"]) != null && Convert.ToString(Request.QueryString["REQUESTFORM"]) != string.Empty && Convert.ToString(Request.QueryString["REQUESTFORM"]) != "")
                            {
                                Session["REQUESTFORM"] = Convert.ToString(Request.QueryString["REQUESTFORM"]);
                            }
                            //Response.Redirect(Request.Url.AbsolutePath, false);
                        }

                        if (Session["REQUESTFORM"].ToString() == "IST")
                        {
                            ltTitle.Text = "<h3 class=\"panel-title\"><strong><span class=\"fa fa-file\"></span>&nbsp; View  </strong>IST</h3>";
                            objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menutabid.Value, "");
                        }
                        else if (Session["REQUESTFORM"].ToString() == "MR")
                        {
                            ltTitle.Text = "<h3 class=\"panel-title\"><strong><span class=\"fa fa-file\"></span>&nbsp; View  </strong>Material Return</h3>";
                            objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menutabid.Value, "");
                        }
                        else if (Session["REQUESTFORM"].ToString() == "CM")
                        {
                            ltTitle.Text = "<h3 class=\"panel-title\"><strong><span class=\"fa fa-file\"></span>&nbsp; View  </strong>Material Consumption</h3>";
                            objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menuCMID.Value, "");
                        }
                        else if (Session["REQUESTFORM"].ToString() == "STO")
                        {
                            ltTitle.Text = "<h3 class=\"panel-title\"><strong><span class=\"fa fa-file\"></span>&nbsp; View  </strong>Delivery Challan (STO)</h3>";
                            objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menuSTO.Value, "");
                        }
                        else if (Session["REQUESTFORM"].ToString() == "STOIN")
                        {
                            ltTitle.Text = "<h3 class=\"panel-title\"><strong><span class=\"fa fa-file\"></span>&nbsp; View  </strong>Material Inward (STO)</h3>";
                            objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menuSTOIN.Value, "");
                        }
                        else if (Session["REQUESTFORM"].ToString() == "IS")
                        {
                            ltTitle.Text = "<h3 class=\"panel-title\"><strong><span class=\"fa fa-file\"></span>&nbsp; View  </strong>Material Split (IS)</h3>";
                            objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menuIS.Value, "");
                        }
                        else
                        {
                            ltTitle.Text = "<h3 class=\"panel-title\"><strong><span class=\"fa fa-file\"></span>&nbsp; View  </strong>Material Issue</h3>";
                            objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menuMIid.Value, "");
                        }

                        if (FormRights.bView == false) //if (objDALUserRights.bView == false)
                        {
                            //   Session.Abandon();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        if (FormRights.bAdd == false) //if (objDALUserRights.bView == false)
                        {
                            lnkNewIST.Enabled = false;
                        }

                        //if (FormRights.bExport == false) //if (objDALUserRights.bView == false)
                        //{
                        //    lnkExport.Visible = false;
                        //}


                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");//Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();

                        objBindDDL.FillPlant(ddlPlantCode, "SEARCH");
                        string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                        ddlPlantCode.SelectedValue = plantArray[0];


                        DataTable dt = new DataTable();
                        if (Session["REQUESTFORM"].ToString() == "IST")
                        {
                            dt = objMainClass.GetISTData(objMainClass.intCmpId, "105", txtDocNo.Text, txtFromDate.Text, txtToDate.Text, ddlPlantCode.SelectedIndex > 0 ? ddlPlantCode.SelectedValue : Convert.ToString(Session["PLANTCD"]));
                            gvList.Columns[5].Visible = false;
                            gvList.Columns[6].Visible = false;
                            gvList.Columns[7].Visible = false;
                        }
                        else if (Session["REQUESTFORM"].ToString() == "MR")
                        {
                            dt = objMainClass.GetISTData(objMainClass.intCmpId, "107", txtDocNo.Text, txtFromDate.Text, txtToDate.Text, ddlPlantCode.SelectedIndex > 0 ? ddlPlantCode.SelectedValue : Convert.ToString(Session["PLANTCD"]));
                            gvList.Columns[5].Visible = false;
                            gvList.Columns[6].Visible = false;
                        }
                        else if (Session["REQUESTFORM"].ToString() == "CM")
                        {
                            dt = objMainClass.GetISTData(objMainClass.intCmpId, "303", txtDocNo.Text, txtFromDate.Text, txtToDate.Text, ddlPlantCode.SelectedIndex > 0 ? ddlPlantCode.SelectedValue : Convert.ToString(Session["PLANTCD"]));
                            gvList.Columns[5].Visible = false;
                            gvList.Columns[6].Visible = false;
                            gvList.Columns[7].Visible = false;
                        }
                        else if (Session["REQUESTFORM"].ToString() == "STO")
                        {
                            dt = objMainClass.GetISTData(objMainClass.intCmpId, "601", txtDocNo.Text, txtFromDate.Text, txtToDate.Text, ddlPlantCode.SelectedIndex > 0 ? ddlPlantCode.SelectedValue : Convert.ToString(Session["PLANTCD"]));
                            gvList.Columns[5].Visible = false;
                            gvList.Columns[6].Visible = false;
                            gvList.Columns[7].Visible = true;
                        }
                        else if (Session["REQUESTFORM"].ToString() == "STOIN")
                        {
                            dt = objMainClass.GetISTData(objMainClass.intCmpId, "603", txtDocNo.Text, txtFromDate.Text, txtToDate.Text, ddlPlantCode.SelectedIndex > 0 ? ddlPlantCode.SelectedValue : Convert.ToString(Session["PLANTCD"]));
                            gvList.Columns[5].Visible = false;
                            gvList.Columns[6].Visible = false;
                            gvList.Columns[7].Visible = true;
                        }
                        else if (Session["REQUESTFORM"].ToString() == "IS")
                        {
                            dt = objMainClass.GetISTData(objMainClass.intCmpId, "401", txtDocNo.Text, txtFromDate.Text, txtToDate.Text, ddlPlantCode.SelectedIndex > 0 ? ddlPlantCode.SelectedValue : Convert.ToString(Session["PLANTCD"]));
                            gvList.Columns[5].Visible = false;
                            gvList.Columns[6].Visible = false;
                            gvList.Columns[7].Visible = false;
                        }
                        else
                        {
                            dt = objMainClass.GetIssueRegisterData(objMainClass.intCmpId, "301", txtDocNo.Text, txtFromDate.Text, txtToDate.Text, ddlPlantCode.SelectedIndex > 0 ? ddlPlantCode.SelectedValue : Convert.ToString(Session["PLANTCD"]));
                            gvList.Columns[5].Visible = true;
                            gvList.Columns[6].Visible = true;
                            gvList.Columns[7].Visible = false;
                        }

                        if (dt.Rows.Count > 0)
                        {
                            gvList.DataSource = dt;
                            gvList.DataBind();

                            if (Session["REQUESTFORM"].ToString() == "IST")
                            {
                                //tsmRptDocISSUE
                                objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), "tsmRptDocISSUE", "");
                            }

                            if (Session["REQUESTFORM"].ToString() == "STO")
                            {
                                //tsmRptDocISSUE
                                objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), "tsmRptDocSTODc", "");
                            }

                            if (Session["REQUESTFORM"].ToString() == "STOIN")
                            {
                                //tsmRptDocISSUE
                                objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), "tsmRptDocMIR", "");
                            }

                            if (FormRights.bPrint == false)
                            {
                                for (int i = 0; i < gvList.Rows.Count; i++)
                                {
                                    LinkButton btnPrint = (LinkButton)gvList.Rows[i].Cells[8].FindControl("btnPrint");
                                    Label lblLine = (Label)gvList.Rows[i].Cells[8].FindControl("lblLine");
                                    btnPrint.Visible = false;
                                    lblLine.Visible = false;
                                }
                            }
                        }
                        else
                        {
                            gvList.DataSource = string.Empty;
                            gvList.DataBind();
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
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

        protected void lnkSearhIST_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                if (Session["REQUESTFORM"].ToString() == "IST")
                {
                    dt = objMainClass.GetISTData(objMainClass.intCmpId, "105", txtDocNo.Text, txtFromDate.Text, txtToDate.Text, ddlPlantCode.SelectedIndex > 0 ? ddlPlantCode.SelectedValue : Convert.ToString(Session["PLANTCD"]));
                    gvList.Columns[5].Visible = false;
                    gvList.Columns[6].Visible = false;
                    gvList.Columns[7].Visible = false;
                }
                else if (Session["REQUESTFORM"].ToString() == "MR")
                {
                    dt = objMainClass.GetISTData(objMainClass.intCmpId, "107", txtDocNo.Text, txtFromDate.Text, txtToDate.Text, ddlPlantCode.SelectedIndex > 0 ? ddlPlantCode.SelectedValue : Convert.ToString(Session["PLANTCD"]));
                    gvList.Columns[5].Visible = false;
                    gvList.Columns[6].Visible = false;
                }
                else if (Session["REQUESTFORM"].ToString() == "CM")
                {
                    dt = objMainClass.GetISTData(objMainClass.intCmpId, "303", txtDocNo.Text, txtFromDate.Text, txtToDate.Text, ddlPlantCode.SelectedIndex > 0 ? ddlPlantCode.SelectedValue : Convert.ToString(Session["PLANTCD"]));
                    gvList.Columns[5].Visible = false;
                    gvList.Columns[6].Visible = false;
                    gvList.Columns[7].Visible = false;
                }
                else if (Session["REQUESTFORM"].ToString() == "STO")
                {
                    dt = objMainClass.GetISTData(objMainClass.intCmpId, "601", txtDocNo.Text, txtFromDate.Text, txtToDate.Text, ddlPlantCode.SelectedIndex > 0 ? ddlPlantCode.SelectedValue : Convert.ToString(Session["PLANTCD"]));
                    gvList.Columns[5].Visible = false;
                    gvList.Columns[6].Visible = false;
                    gvList.Columns[7].Visible = true;
                }
                else if (Session["REQUESTFORM"].ToString() == "STOIN")
                {
                    dt = objMainClass.GetISTData(objMainClass.intCmpId, "603", txtDocNo.Text, txtFromDate.Text, txtToDate.Text, ddlPlantCode.SelectedIndex > 0 ? ddlPlantCode.SelectedValue : Convert.ToString(Session["PLANTCD"]));
                    gvList.Columns[5].Visible = false;
                    gvList.Columns[6].Visible = false;
                    gvList.Columns[7].Visible = true;
                }
                else if (Session["REQUESTFORM"].ToString() == "IS")
                {
                    dt = objMainClass.GetISTData(objMainClass.intCmpId, "401", txtDocNo.Text, txtFromDate.Text, txtToDate.Text, ddlPlantCode.SelectedIndex > 0 ? ddlPlantCode.SelectedValue : Convert.ToString(Session["PLANTCD"]));
                    gvList.Columns[5].Visible = false;
                    gvList.Columns[6].Visible = false;
                    gvList.Columns[7].Visible = false;
                }
                else
                {
                    dt = objMainClass.GetIssueRegisterData(objMainClass.intCmpId, "301", txtDocNo.Text, txtFromDate.Text, txtToDate.Text, ddlPlantCode.SelectedIndex > 0 ? ddlPlantCode.SelectedValue : Convert.ToString(Session["PLANTCD"]));
                    gvList.Columns[5].Visible = true;
                    gvList.Columns[6].Visible = true;
                    gvList.Columns[7].Visible = false;
                }
                if (dt.Rows.Count > 0)
                {
                    gvList.DataSource = dt;
                    gvList.DataBind();

                    if (Session["REQUESTFORM"].ToString() == "IST")
                    {
                        //tsmRptDocISSUE
                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), "tsmRptDocISSUE", "");
                    }
                    if (Session["REQUESTFORM"].ToString() == "STO")
                    {
                        //tsmRptDocSTODc
                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), "tsmRptDocSTODc", "");
                    }

                    if (Session["REQUESTFORM"].ToString() == "STOIN")
                    {
                        //tsmRptDocMIR
                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), "tsmRptDocMIR", "");
                    }

                    if (FormRights.bPrint == false)
                    {
                        for (int i = 0; i < gvList.Rows.Count; i++)
                        {
                            LinkButton btnPrint = (LinkButton)gvList.Rows[i].Cells[8].FindControl("btnPrint");
                            Label lblLine = (Label)gvList.Rows[i].Cells[8].FindControl("lblLine");
                            btnPrint.Visible = false;
                            lblLine.Visible = false;
                        }
                    }
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

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected void lnkExport_Click(object sender, EventArgs e)
        {
            try
            {
                string attachment = "attachment; filename=rptPR.xls";
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

                throw;
            }
        }

        protected void bntView_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["REQUESTFORM"].ToString() == "IST")
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    string docno = grdrow.Cells[1].Text;
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetISTDetails(docno, objMainClass.intCmpId, 1);
                    if (dt.Rows.Count > 0)
                    {
                        lblDoctype.Text = Convert.ToString(dt.Rows[0]["DOCTYPE"]);
                        lblDocNo.Text = Convert.ToString(dt.Rows[0]["DOCNO"]);
                        lblDate.Text = Convert.ToString(dt.Rows[0]["DOCKDT"]);
                        lblRemark.Text = Convert.ToString(dt.Rows[0]["VENDOR"]);
                        lblRefNo.Text = Convert.ToString(dt.Rows[0]["REFNO"]);

                        gvDetail.DataSource = dt;
                        gvDetail.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                    }
                    else
                    {
                        gvDetail.DataSource = string.Empty;
                        gvDetail.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
                    }
                }
                else if (Session["REQUESTFORM"].ToString() == "MR")
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    string docno = grdrow.Cells[1].Text;
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetEachMaterialDetails(docno, objMainClass.intCmpId, 1);
                    if (dt.Rows.Count > 0)
                    {
                        lblMrDocType.Text = Convert.ToString(dt.Rows[0]["DOCTYPE"]);
                        lblMrDocNo.Text = Convert.ToString(dt.Rows[0]["DOCNO"]);
                        lblMrDocDate.Text = Convert.ToString(dt.Rows[0]["DOCKDT"]);
                        lblMrRemark.Text = Convert.ToString(dt.Rows[0]["VENDOR"]);
                        lblMrRefNo.Text = Convert.ToString(dt.Rows[0]["REFNO"]);
                        lblVendorName.Text = Convert.ToString(dt.Rows[0]["VENDNAME"]);

                        gvMaterialReturn.DataSource = dt;
                        gvMaterialReturn.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-materialreturndetail').modal();", true);
                    }
                    else
                    {
                        gvMaterialReturn.DataSource = string.Empty;
                        gvMaterialReturn.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
                    }
                }
                else if (Session["REQUESTFORM"].ToString() == "CM")
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    string doctype = grdrow.Cells[0].Text;
                    string docno = grdrow.Cells[1].Text;
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetMMDtlDoctypewise(objMainClass.intCmpId, doctype, docno);
                    if (dt.Rows.Count > 0)
                    {
                        lblCMDoctype.Text = Convert.ToString(dt.Rows[0]["DOCTYPE"]);
                        lblCMDocNo.Text = Convert.ToString(dt.Rows[0]["DOCNO"]);
                        lblCMDate.Text = Convert.ToString(dt.Rows[0]["DOCKDT"]);
                        lblCMRefNo.Text = Convert.ToString(dt.Rows[0]["REFNO"]);
                        lblCMRemark.Text = Convert.ToString(dt.Rows[0]["REMARK"]);

                        grvConsume.DataSource = dt;
                        grvConsume.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Consume').modal();", true);
                    }
                    else
                    {
                        grvConsume.DataSource = string.Empty;
                        grvConsume.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
                    }
                }
                else if (Session["REQUESTFORM"].ToString() == "STO")
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    string doctype = grdrow.Cells[0].Text;
                    string docno = grdrow.Cells[1].Text;
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetMMDtlDoctypewise(objMainClass.intCmpId, doctype, docno);
                    if (dt.Rows.Count > 0)
                    {
                        lblSTODocType.Text = Convert.ToString(dt.Rows[0]["DOCTYPE"]);
                        lblSTODocNo.Text = Convert.ToString(dt.Rows[0]["DOCNO"]);
                        lblSTODate.Text = Convert.ToString(dt.Rows[0]["DOCKDT"]);
                        lblSTORemark.Text = Convert.ToString(dt.Rows[0]["REMARK"]);
                        lblSTORefNo.Text = Convert.ToString(dt.Rows[0]["REFNO"]);
                        lblSTOPONO.Text = Convert.ToString(dt.Rows[0]["PONO"]);
                        lblSTODocketNo.Text = Convert.ToString(dt.Rows[0]["DOCKETNO"]);
                        lblSTONoOfBoxes.Text = Convert.ToString(dt.Rows[0]["NOOFBOX"]);
                        lblSTOTranName.Text = Convert.ToString(dt.Rows[0]["TRANNAME"]);
                        lblSTOTranCode.Text = Convert.ToString(dt.Rows[0]["TRANCODE"]);

                        grvSTO.DataSource = dt;
                        grvSTO.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-STO').modal();", true);
                    }
                    else
                    {
                        grvSTO.DataSource = string.Empty;
                        grvSTO.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
                    }
                }
                else if (Session["REQUESTFORM"].ToString() == "STOIN")
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    string doctype = grdrow.Cells[0].Text;
                    string docno = grdrow.Cells[1].Text;
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetMMDtlDoctypewise(objMainClass.intCmpId, doctype, docno);
                    if (dt.Rows.Count > 0)
                    {
                        lblSTOINDocType.Text = Convert.ToString(dt.Rows[0]["DOCTYPE"]);
                        lblSTOINDocNo.Text = Convert.ToString(dt.Rows[0]["DOCNO"]);
                        lblSTOINDocDt.Text = Convert.ToString(dt.Rows[0]["DOCKDT"]);
                        lblSTOINRefNo.Text = Convert.ToString(dt.Rows[0]["REFNO"]);
                        lblSTOINRemark.Text = Convert.ToString(dt.Rows[0]["REMARK"]);
                        lblSTOINPoNo.Text = Convert.ToString(dt.Rows[0]["REFDOCNO"]);
                        lblSTOINChallanNo.Text = Convert.ToString(dt.Rows[0]["CHLNNO"]);
                        lblSTOINChallanDt.Text = Convert.ToString(dt.Rows[0]["CHLNDT"]);
                        lblSTOINTranspoerter.Text = Convert.ToString(dt.Rows[0]["TRANCODE"]);
                        lblSTOINTranspoerterName.Text = Convert.ToString(dt.Rows[0]["TRANNAME"]);

                        grvSTOINList.DataSource = dt;
                        grvSTOINList.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-STOIN').modal();", true);
                    }
                    else
                    {
                        grvSTOINList.DataSource = string.Empty;
                        grvSTOINList.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
                    }
                }
                else if (Session["REQUESTFORM"].ToString() == "IS")
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    string doctype = grdrow.Cells[0].Text;
                    string docno = grdrow.Cells[1].Text;
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetISData(objMainClass.intCmpId, doctype, docno);
                    if (dt.Rows.Count > 0)
                    {
                        lblISDocType.Text = Convert.ToString(dt.Rows[0]["DOCTYPE"]);
                        lblISDocNo.Text = Convert.ToString(dt.Rows[0]["DOCNO"]);
                        lblISDocDt.Text = Convert.ToString(dt.Rows[0]["DOCKDT"]);
                        lblISRefNo.Text = Convert.ToString(dt.Rows[0]["REFNO"]);
                        lblISRemark.Text = Convert.ToString(dt.Rows[0]["REMARK"]);
                        //lblSTOINPoNo.Text = Convert.ToString(dt.Rows[0]["REFDOCNO"]);
                        //lblSTOINChallanNo.Text = Convert.ToString(dt.Rows[0]["CHLNNO"]);
                        //lblSTOINChallanDt.Text = Convert.ToString(dt.Rows[0]["CHLNDT"]);
                        //lblSTOINTranspoerter.Text = Convert.ToString(dt.Rows[0]["TRANCODE"]);
                        //lblSTOINTranspoerterName.Text = Convert.ToString(dt.Rows[0]["TRANNAME"]);

                        grvISList.DataSource = dt;
                        grvISList.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-IS').modal();", true);
                    }
                    else
                    {
                        grvSTOINList.DataSource = string.Empty;
                        grvSTOINList.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
                    }
                }
                else
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    string docno = grdrow.Cells[1].Text;
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetMaterialIssueDetails(docno, objMainClass.intCmpId);
                    if (dt.Rows.Count > 0)
                    {
                        lblmiDoctType.Text = Convert.ToString(dt.Rows[0]["DOCTYPE"]);
                        lblmiDocNo.Text = Convert.ToString(dt.Rows[0]["DOCNO"]);
                        lblmiDocDate.Text = Convert.ToString(dt.Rows[0]["DOCKDT"]);
                        lblmiIssueDept.Text = Convert.ToString(dt.Rows[0]["DEPARTMENT"]);
                        lblmiEmpName.Text = Convert.ToString(dt.Rows[0]["EMPLOYEENAME"]);
                        lblmiRemark.Text = Convert.ToString(dt.Rows[0]["VENDOR"]);
                        lblmiRefNo.Text = Convert.ToString(dt.Rows[0]["REFNO"]);

                        gvMaterialIssue.DataSource = dt;
                        gvMaterialIssue.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-materialissue').modal();", true);
                    }
                    else
                    {
                        gvMaterialIssue.DataSource = string.Empty;
                        gvMaterialIssue.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["REQUESTFORM"].ToString() == "IST")
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    string docno = grdrow.Cells[1].Text;

                    string path = "ViewISTPDF.aspx";
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "?DOCNO=" + docno + "');", true);
                }
                else if (Session["REQUESTFORM"].ToString() == "STO")
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    string docno = grdrow.Cells[1].Text;

                    string path = "ViewSTODCPDF.aspx";
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "?DOCNO=" + docno + "');", true);
                }
                else if (Session["REQUESTFORM"].ToString() == "STOIN")
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    string docno = grdrow.Cells[1].Text;

                    string path = "ViewInwardPDF.aspx";
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "?DOCNO=" + docno + "');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lnkNewIST_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["REQUESTFORM"].ToString() == "IST")
                {
                    Response.Redirect("CreateIST.aspx", true);
                }
                else if (Session["REQUESTFORM"].ToString() == "MR")
                {
                    Response.Redirect("CreateMaterialReturn.aspx", true);
                }
                else if (Session["REQUESTFORM"].ToString() == "CM")
                {
                    Response.Redirect("CreateMaterialConsumption.aspx", true);
                }
                else if (Session["REQUESTFORM"].ToString() == "STO")
                {
                    Response.Redirect("CreateSTODC.aspx", true);
                }
                else if (Session["REQUESTFORM"].ToString() == "STOIN")
                {
                    Response.Redirect("CreateSTOIN.aspx", true);
                }
                else if (Session["REQUESTFORM"].ToString() == "IS")
                {
                    Response.Redirect("CreateMaterialSplit.aspx", true);
                }
                else
                {
                    Response.Redirect("CreateMaterialIssue.aspx", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void ddlPlantCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (ddlPlantCode.SelectedIndex > 0)
                    {
                        string PLantRights = string.Empty;
                        string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                        for (int i = 0; i < plantArray.Count(); i++)
                        {
                            if (plantArray[i] == ddlPlantCode.SelectedValue)
                            {
                                PLantRights = ddlPlantCode.SelectedValue;
                            }
                        }

                        if (PLantRights.Length > 0)
                        {

                        }
                        else
                        {
                            ddlPlantCode.SelectedValue = plantArray[0];
                            ddlPlantCode.Focus();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have plant rights.');$('#txtJobId').focus();", true);
                            ddlPlantCode.SelectedValue = plantArray[0];
                            ddlPlantCode.Focus();
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