using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Transactions;
using Microsoft.Reporting.WebForms;
using System.IO;

namespace ShERPa360net
{
    public partial class ViewPR : System.Web.UI.Page
    {
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["EditPRNo"] = string.Empty;
            Session["ReqNo"] = string.Empty;
            Session["EditPRNo"] = null;
            Session["ReqNo"] = null;
            Session["MRITEMSR"] = string.Empty;
            Session["MRITEMSR"] = null;
            lblAPRV1.Visible = false;
            lblAPRV2.Visible = false;
            lblAPRV1.Text = string.Empty;
            lblAPRV2.Text = string.Empty;
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
                            lnkNewPR.Enabled = false;
                        }

                        if (FormRights.bExport == false) //if (objDALUserRights.bView == false)
                        {
                            lnkExport.Visible = false;
                        }



                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();

                        DataTable dt = new DataTable();
                        dt = objMainClass.GetPRData(txtPrno.Text, txtFromDate.Text, txtToDate.Text, "", Convert.ToInt32(Session["USERID"]), 0);
                        if (dt.Rows.Count > 0)
                        {
                            gvList.DataSource = dt;
                            gvList.DataBind();
                            //if (FormRights.bPrint == false)
                            //{
                            //    for (int i = 0; i < gvList.Rows.Count; i++)
                            //    {
                            //        //8
                            //        LinkButton btnPrint = (LinkButton)gvList.Rows[i].Cells[8].FindControl("btnPrint");
                            //        btnPrint.Enabled = false;
                            //    }
                            //}
                            if (FormRights.bEdit == false)
                            {
                                for (int i = 0; i < gvList.Rows.Count; i++)
                                {
                                    //8
                                    LinkButton btnEdit = (LinkButton)gvList.Rows[i].Cells[8].FindControl("btnEdit");
                                    btnEdit.Enabled = false;
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

        protected void imgNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("CreatePR.aspx", false);
        }

        protected void imgFind_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objMainClass.GetPRData(txtPrno.Text, txtFromDate.Text, txtToDate.Text, "", Convert.ToInt32(Session["USERID"]), 0);
                if (dt.Rows.Count > 0)
                {
                    gvList.DataSource = dt;
                    gvList.DataBind();

                    //if (FormRights.bPrint == false)
                    //{
                    //    for (int i = 0; i < gvList.Rows.Count; i++)
                    //    {
                    //        //8
                    //        LinkButton btnPrint = (LinkButton)gvList.Rows[i].Cells[8].FindControl("btnPrint");
                    //        btnPrint.Enabled = false;
                    //    }
                    //}
                    if (FormRights.bEdit == false)
                    {
                        for (int i = 0; i < gvList.Rows.Count; i++)
                        {
                            //8
                            LinkButton btnEdit = (LinkButton)gvList.Rows[i].Cells[8].FindControl("btnEdit");
                            btnEdit.Enabled = false;
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

        protected void ImgExport_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void bntView_Click(object sender, EventArgs e)
        {
            try
            {
                lblAPRV1.Visible = false;
                lblAPRV2.Visible = false;
                lblAPRV1.Text = string.Empty;
                lblAPRV2.Text = string.Empty;

                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

                string prno = grdrow.Cells[1].Text;

                DataTable dt = new DataTable();
                dt = objMainClass.GetPRDetails(prno, 1);
                if (dt.Rows.Count > 0)
                {

                    lblDoctype.Text = Convert.ToString(dt.Rows[0]["PRTYPE"]);
                    lblPRDate.Text = Convert.ToDateTime(dt.Rows[0]["PRDT"]).ToShortDateString();
                    lblPRNo.Text = Convert.ToString(dt.Rows[0]["PRNO"]);
                    lblRemark.Text = Convert.ToString(dt.Rows[0]["REMARK"]);

                    gvDetail.DataSource = dt;
                    gvDetail.DataBind();


                    DataTable logDT = new DataTable();
                    logDT = objMainClass.SELECT_REQUISITION_LOG(lblPRNo.Text);
                    //if (logDT.Rows.Count == 1)
                    //{
                    //    lblAPRV1.Text = "Approval 1 :  " + Convert.ToString(logDT.Rows[0]["LISTDESC"]) + "  By  " + Convert.ToString(logDT.Rows[0]["USERNAME"]) + "  On  " + Convert.ToString(logDT.Rows[0]["APRVDATE"]);//APRVDATE
                    //    lblAPRV1.Visible = true;
                    //}
                    //else if (logDT.Rows.Count == 2)
                    //{
                    //    lblAPRV1.Text = "Approval 1 :  " + Convert.ToString(logDT.Rows[0]["LISTDESC"]) + "  By  " + Convert.ToString(logDT.Rows[0]["USERNAME"]) + "  On  " + Convert.ToString(logDT.Rows[0]["APRVDATE"]);//APRVDATE
                    //    lblAPRV2.Text = "Approval 2 :  " + Convert.ToString(logDT.Rows[1]["LISTDESC"]) + "  By  " + Convert.ToString(logDT.Rows[1]["USERNAME"]) + "  On  " + Convert.ToString(logDT.Rows[1]["APRVDATE"]);//APRVDATE
                    //    lblAPRV1.Visible = true;
                    //    lblAPRV2.Visible = true;
                    //}


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
            string status = grdrow.Cells[6].Text;
            if (status == "" || status == "&nbsp;" || status == "REJECTED")
            {
                Response.Redirect("CreatePR.aspx?PRNO=" + prno, true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You can not edit this PR. PR is already " + status + "');", true);

            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {

                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                string prno = grdrow.Cells[1].Text;

                string path = "ViewReport.aspx";
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "?PRNO=" + prno + "');", true);
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

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string LISTDESC = e.Row.Cells[6].Text;
                if ((LISTDESC != null && LISTDESC != "&nbsp;" && LISTDESC != "" && LISTDESC != "REJECTED"))
                {
                    e.Row.FindControl("btnEdit").Visible = false;
                    e.Row.FindControl("lblStick").Visible = false;
                }
            }
        }

        protected void lnkInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

                    string extension = ((Label)grdrow.FindControl("lblMREXTENSION")).Text;
                    string mrno = ((Label)grdrow.FindControl("lblMRNO")).Text;
                    if (extension != null && extension != "" && extension != string.Empty)
                    {
                        string url = "ViewMRInvoice.aspx?MRNO=" + mrno;
                        string s = "window.open('" + url + "', 'popup_window', 'width=500,height=500,left=500,top=100,resizable=yes');";
                        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invoice not Uploaded for this MR!');", true);
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