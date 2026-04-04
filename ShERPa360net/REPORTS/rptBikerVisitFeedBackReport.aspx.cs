using Microsoft.Reporting.WebForms;
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
    public partial class rptBikerVisitFeedBackReport : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //SortDirection = "DESC";
                //SortColumn = "AGE";

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
                        string[] date = objMainClass.indianTime.Date.ToString("dd-MM-yyyy").Split('-');
                        DateTime fromdate = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), 1);
                        txtFromDocDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");
                        txtToDocDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");
                        objBindDDL.FillBiker(ddlBiker, "BIKERMENU");
                        objBindDDL.FillCity(ddlCity, 1);
                        //GetData("REPORT", txtFromDocDate.Text, txtToDocDate.Text, 0);
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

        public void GetData(string ACTION, string FROMDATE, string TODATE, int SEARCHBY, int REQBYSYSTEM)
        {
            //GetBikerVisitReport
            if (Session["USERID"] != null)
            {
                try
                {
                    int bdcnt = 0;
                    int krocnt = 0;
                    DataTable dtfinal = new DataTable();
                    DataTable dtfinal1 = new DataTable();

                    DataTable dt = new DataTable();
                    DataTable dt1 = new DataTable();
                    dt  = objMainClass.GetBikerVisitFeedBackReport(ACTION, FROMDATE, TODATE, SEARCHBY, REQBYSYSTEM, ddlCity.SelectedValue);
                    dt1 = objMainClass.GetBikerVisitFeedBackReport(ACTION, FROMDATE, TODATE, SEARCHBY, REQBYSYSTEM, ddlCity.SelectedValue);
                    if (dt.Rows.Count > 0)
                    {
                        //if(REQBYSYSTEM == -1)
                        //{
                        //    DataTable dtkro = new DataTable();
                        //    dtkro           = objMainClass.GetBikerVisitFeedBackReport(ACTION, FROMDATE, TODATE, SEARCHBY, REQBYSYSTEM, ddlCity.SelectedValue);
                        //}
                        DataView dview = dt.DefaultView;
                        DataView dview1 = dt1.DefaultView;

                        //DataView dview = dt.DefaultView;

                        if (REQBYSYSTEM == 1)
                        {
                            //Use the Filter with the use DataView 
                            dview.RowFilter = "ROLE = 'BIKER (BDO)'";
                            //dview.RowStateFilter = DataViewRowState.ModifiedCurrent;
                            DataTable disCreatedBy = dview.ToTable(true, "CREATEBY");
                            bdcnt = disCreatedBy
                                                     .AsEnumerable()
                                                     .Select(r => r.Field<string>("CREATEBY"))
                                                     .Distinct()
                                                     .Count();
                        }
                        else if (REQBYSYSTEM == 2)
                        {
                            dview1.RowFilter = "ROLE = 'KRO(VENDOR)'";
                            DataTable disCreatedBy = dview1.ToTable(true, "CREATEBY");
                           krocnt = disCreatedBy
                                                    .AsEnumerable()
                                                     .Select(r => r.Field<string>("CREATEBY"))
                                                     .Distinct()
                                                     .Count();
                        }

                        else
                        {
                            dview.RowFilter = "ROLE = 'BIKER (BDO)'";
                            DataTable disCreatedBy = dview.ToTable(true, "CREATEBY");
                            bdcnt = disCreatedBy
                                                     .AsEnumerable()
                                                     .Select(r => r.Field<string>("CREATEBY"))
                                                     .Distinct()
                                                     .Count();

                            //KRO Count 
                            dview1.RowFilter = "ROLE = 'KRO(VENDOR)'";
                            DataTable diskro = dview1.ToTable(true, "CREATEBY");
                            krocnt = diskro
                                                     .AsEnumerable()
                                                     .Select(r => r.Field<string>("CREATEBY"))
                                                     .Distinct()
                                                     .Count();


                            //dview.Table.Merge(dview1.Table);
                            dtfinal  = dview.ToTable();
                            dtfinal1 = dview1.ToTable();
                            dtfinal.Merge(dtfinal1);
                        }


                        //Use the Filter with the use DataView 
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;
                        ReportViewer1.LocalReport.ReportPath = "Report/rptBDODailyActivityDetail.rdlc";
                        if(REQBYSYSTEM == 1 )
                        {
                            ReportDataSource rds = new ReportDataSource("DataSet1", dview);
                            ReportViewer1.LocalReport.DataSources.Clear();
                            ReportViewer1.LocalReport.DataSources.Add(rds);
                        }
                        else if (REQBYSYSTEM == 2)
                        {
                            ReportDataSource rds = new ReportDataSource("DataSet1", dview1);
                            ReportViewer1.LocalReport.DataSources.Clear();
                            ReportViewer1.LocalReport.DataSources.Add(rds);
                        }
                        else
                        {
                            ReportDataSource rds = new ReportDataSource("DataSet1", dtfinal);
                            ReportViewer1.LocalReport.DataSources.Clear();
                            ReportViewer1.LocalReport.DataSources.Add(rds);
                        }
                        ReportParameter[] parameters = new ReportParameter[2];
                        parameters[0] = new ReportParameter("BDOCount", bdcnt.ToString(), true);
                        parameters[1] = new ReportParameter("KROCount", krocnt.ToString(), true);
                        ReportViewer1.LocalReport.SetParameters(parameters);
                        ReportViewer1.Visible = true;
                    }


                }
                catch (Exception ex)
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true); ;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
            }
        }



        protected void lnkSerch_Click(object sender, EventArgs e)
        {
            if (Session["USERID"] != null)
            {
                try
                {
                    GetData("REPORT", txtFromDocDate.Text, txtToDocDate.Text, Convert.ToInt32(Session["USERID"]), Convert.ToInt32(ddlUserType.SelectedValue));
                }
                catch (Exception ex)
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true); ;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void lnkExport_Click(object sender, EventArgs e)
        {
            if (Session["USERID"] != null)
            {
                try
                {
                    //string attachment = "attachment; filename=Biker Reject Report.xls";
                    //Response.ClearContent();
                    //Response.AddHeader("content-disposition", attachment);
                    //Response.ContentType = "application/vdn.ms-excel";
                    //StringWriter sw = new StringWriter();
                    //HtmlTextWriter htw = new HtmlTextWriter(sw);
                    //ltgridhtml.RenderControl(htw);
                    //Response.Write(sw.ToString());
                    //Response.End();
                }
                catch (Exception ex)
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true); ;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
            }
        }
    }
}