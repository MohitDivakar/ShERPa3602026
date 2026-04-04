using Microsoft.Reporting.WebForms;
using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.REPORTS
{
    public partial class rptCheckList : System.Web.UI.Page
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
                        if (FormRights.bView == false)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }

                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtTodate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();

                        GETDATA();


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

        public void GETDATA()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetCheckListData(objMainClass.intCmpId, 0, txtJobid.Text, txtFromDate.Text, txtTodate.Text, "GETVIEWPAGEDATA");
                    if (dt.Rows.Count > 0)
                    {
                        grvCheckListData.DataSource = dt;
                        grvCheckListData.DataBind();
                        grvCheckListData.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    else
                    {
                        grvCheckListData.DataSource = string.Empty;
                        grvCheckListData.DataBind();
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

        protected void lnkSearhSR_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GETDATA();
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

        protected void lnkView_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    string checkid = grdrow.Cells[0].Text;

                    DataTable dt = new DataTable();
                    dt = objMainClass.GetCheckListData(objMainClass.intCmpId, Convert.ToInt32(checkid), txtJobid.Text, txtFromDate.Text, txtTodate.Text, "GETDETIALVIEWPAGE");
                    if (dt.Rows.Count > 0)
                    {

                        hfChkID.Value = Convert.ToString(dt.Rows[0]["ID"]);
                        lblDOCDATE.Text = Convert.ToString(dt.Rows[0]["DOCDATE"]);
                        lblJOBID.Text = Convert.ToString(dt.Rows[0]["JOBID"]);
                        lblSERIALNO.Text = Convert.ToString(dt.Rows[0]["SERIALNO"]);
                        lblPROJECT.Text = Convert.ToString(dt.Rows[0]["PROJECT"]);
                        lblSTATUS.Text = Convert.ToString(dt.Rows[0]["STATUS"]);
                        lblGRADE.Text = Convert.ToString(dt.Rows[0]["GRADE"]);
                        lblMAKE.Text = Convert.ToString(dt.Rows[0]["MAKE"]);
                        lblMODEL.Text = Convert.ToString(dt.Rows[0]["MODEL"]);
                        lblCOLOR.Text = Convert.ToString(dt.Rows[0]["COLOR"]);
                        lblCHECKBY.Text = Convert.ToString(dt.Rows[0]["CHECKBY"]);
                        lblVERIFIEDBY.Text = Convert.ToString(dt.Rows[0]["VERIFIEDBY"]);
                        lblCREATEDBY.Text = Convert.ToString(dt.Rows[0]["CREATEDBY"]);
                        lblCREATEDATE.Text = Convert.ToString(dt.Rows[0]["CREATEDATE"]);


                        gvDetail.DataSource = dt;
                        gvDetail.DataBind();

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Record not found for selected check list.');", true);
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

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    string checkid = grdrow.Cells[0].Text;
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetCheckListData(objMainClass.intCmpId, Convert.ToInt32(checkid), txtJobid.Text, txtFromDate.Text, txtTodate.Text, "GETDETIALVIEWPAGE");
                    if (dt.Rows.Count > 0)
                    {
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;
                        ReportViewer1.LocalReport.ReportPath = "Report/rptCheckList.rdlc";
                        ReportDataSource rds = new ReportDataSource("DataSetCheckList", dt);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds);
                        ReportViewer1.Visible = true;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-report').modal();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Record not found for selected check list.');", true);
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