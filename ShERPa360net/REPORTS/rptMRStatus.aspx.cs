using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.REPORTS
{
    public partial class rptMRStatus : System.Web.UI.Page
    {
        DALUserRights objDALUserRights = new DALUserRights();
        MainClass objMainClass = new MainClass();

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
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../REPORTS/ReportDashboard.aspx' });", true);
                            return;
                        }


                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();

                        DataTable dt = new DataTable();
                        dt = objMainClass.GetMRStatusData(objMainClass.intCmpId, txtMRno.Text, txtFromDate.Text, txtToDate.Text, 1, "");
                        if (dt.Rows.Count > 0)
                        {
                            gvDetail.DataSource = dt;
                            gvDetail.DataBind();
                        }
                        else
                        {
                            gvDetail.DataSource = string.Empty;
                            gvDetail.DataBind();
                        }



                    }
                    else
                    {
                        Session.Abandon();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
                }
            }
        }

        protected void lnkSearhMR_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetMRStatusData(objMainClass.intCmpId, txtMRno.Text, txtFromDate.Text, txtToDate.Text, 1, "");
                    if (dt.Rows.Count > 0)
                    {
                        gvDetail.DataSource = dt;
                        gvDetail.DataBind();
                    }
                    else
                    {
                        gvDetail.DataSource = string.Empty;
                        gvDetail.DataBind();
                    }
                }
                else
                {
                    Session.Abandon();
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
                string attachment = "attachment; filename=rptMR.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vdn.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gvDetail.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((Button)sender).NamingContainer;
            string mrid = grdrow.Cells[0].Text;
            string mrno = grdrow.Cells[1].Text;
            string status = grdrow.Cells[16].Text;
            string prno = grdrow.Cells[20].Text;

            if (status == "APPROVAL PENDING")
            {
                DataTable dtAprv = new DataTable();
                dtAprv = objMainClass.GetApprover(objMainClass.intCmpId, mrno, 2);
                if (dtAprv.Rows.Count > 0)
                {
                    lblAprvMRNO.Text = Convert.ToString(dtAprv.Rows[0]["MRNO"]);
                    lblAprvCreateBy.Text = Convert.ToString(dtAprv.Rows[0]["CREATEBY"]);
                    lblAprvReqBy.Text = Convert.ToString(dtAprv.Rows[0]["APPROVER"]);
                    lblAprvMRDate.Text = Convert.ToString(dtAprv.Rows[0]["MRDT"]);

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Aprvdetail').modal();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
                }

            }
            else if (status == "APPROVED")
            {
                DataTable dtAprvd = new DataTable();
                dtAprvd = objMainClass.GetApprover(objMainClass.intCmpId, mrno, 3);
                if (dtAprvd.Rows.Count > 0)
                {
                    lblAprvedMRno.Text = Convert.ToString(dtAprvd.Rows[0]["MRNO"]);
                    lblAprvedMRdate.Text = Convert.ToString(dtAprvd.Rows[0]["MRDT"]);
                    lblAprvedCreateBy.Text = Convert.ToString(dtAprvd.Rows[0]["CREATEBY"]);
                    lblAprvedBY.Text = Convert.ToString(dtAprvd.Rows[0]["APPROVEDBY"]);
                    lblAprvedByDate.Text = Convert.ToString(dtAprvd.Rows[0]["APRVDATE"]);

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Aprveddetail').modal();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
                }
            }
            else if (status == "REJECTED")
            {
                DataTable dtRjckd = new DataTable();
                dtRjckd = objMainClass.GetApprover(objMainClass.intCmpId, mrno, 3);
                if (dtRjckd.Rows.Count > 0)
                {
                    lblRjcktdMRno.Text = Convert.ToString(dtRjckd.Rows[0]["MRNO"]);
                    lblRjcktdMRdate.Text = Convert.ToString(dtRjckd.Rows[0]["MRDT"]);
                    lblRjcktdCreateBy.Text = Convert.ToString(dtRjckd.Rows[0]["CREATEBY"]);
                    lblRjcktdBY.Text = Convert.ToString(dtRjckd.Rows[0]["APPROVEDBY"]);
                    lblRjcktdByDate.Text = Convert.ToString(dtRjckd.Rows[0]["APRVDATE"]);

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Rjcktddetail').modal();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
                }
            }
            else if (status == "PR CREATED")
            {
                Response.Redirect("MRToPBDetail.aspx?PRNO=" + prno, false);
            }
            else if (status == "PO CREATED")
            {
                Response.Redirect("MRToPBDetail.aspx?PRNO=" + prno, false);
            }
            else if (status == "ITEM ISSUED")
            {
                if (prno != "" && prno != null && prno != string.Empty && prno != "&nbsp;")
                {
                    Response.Redirect("MRToPBDetail.aspx?PRNO=" + prno, false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Item directly Isuued against MR. PR / PO Not created.!');", true);
                }
            }


        }
    }
}