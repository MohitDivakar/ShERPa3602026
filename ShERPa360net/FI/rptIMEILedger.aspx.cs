using Newtonsoft.Json;
using RestSharp;
using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.FI
{
    public partial class rptIMEILedger : System.Web.UI.Page
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
                        if (FormRights.bView == false) //if (objDALUserRights.bView == false)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }

                        int month = DateTime.Now.Month;
                        int year = DateTime.Now.Year;

                        // if month is october or later, the FY started 10-1 of this year
                        // else it started 10-1 of last year
                        //return month > 9 ? new DateTime(year, 10, 1) : new DateTime(year - 1, 10, 1);

                        //int month = DateTime.Now.Month;
                        //int year = DateTime.Now.Year;

                        // if month is october or later, the FY ends 9/30 next year
                        // else it ends 9-30 of this year
                        if (month < 4)
                        {
                            year = year - 1;
                        }
                        string startdt = "01-04-" + year;
                        txtFromDate.Text = startdt;//objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtToDate.Text = (Convert.ToDateTime(startdt).AddYears(1).AddSeconds(-1)).ToString("dd-MM-yyyy");//objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();

                        objBindDDL.FillVendor(ddlVendor);


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

        protected void lnkSerch_Click(object sender, EventArgs e)
        {

            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    //dt = objMainClass.GetVendorLedger(objMainClass.intCmpId, ddlVendor.SelectedValue, txtFromDate.Text, txtToDate.Text, "IMEIWISELEDGER");

                    string fromdate = objMainClass.setDateFormat(txtFromDate.Text, true);
                    string todate = objMainClass.setDateFormat(txtToDate.Text, true);
                    //var client = new RestClient("http://localhost:59657/api/MobexVendorLedgerIMEIWise?VENDORCODE=" + ddlVendor.SelectedValue + "&FROMDATE=" + fromdate + "&TODATE=" + todate);
                    //var client = new RestClient("http://3.6.38.46/api/MobexVendorLedgerIMEIWise?VENDORCODE=" + ddlVendor.SelectedValue + "&FROMDATE=" + fromdate + "&TODATE=" + todate);
                    var client = new RestClient("http://14.98.132.190:1503/api/MobexVendorLedgerIMEIWise?VENDORCODE=" + ddlVendor.SelectedValue + "&FROMDATE=" + fromdate + "&TODATE=" + todate);
                    client.Timeout = -1;

                    var request = new RestRequest(Method.GET);
                    request.AddHeader("AUTH_KEY", "MOBEX123");
                    IRestResponse response = client.Execute(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string jsonconn = response.Content;
                        LedgerResponse objWAResponse = JsonConvert.DeserializeObject<LedgerResponse>(jsonconn);
                        dt = objWAResponse.DATA;
                        if (dt.Rows.Count > 0)
                        {
                            gvList.DataSource = dt;
                            gvList.DataBind();
                        }
                        else
                        {
                            gvList.DataSource = string.Empty;
                            gvList.DataBind();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Convert.ToString(objWAResponse.MESSAGE) + "\");", true);
                        }
                    }
                    else
                    {
                        string jsonconn = Convert.ToString(response.StatusCode);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + jsonconn + "\");", true);
                    }

                    #region Old Commented Code...
                    //if (dt.Rows.Count > 0)
                    //{
                    //    //dt = dt.DefaultView.ToTable(true, "DOCNO", "PARTYAC", "IMEINO");
                    //    string refno = "";
                    //    string partyac = "";
                    //    string IMEI = "";
                    //    for (int i = 0; i < dt.Rows.Count; i++)
                    //    {
                    //        if (Convert.ToString(dt.Rows[i]["DOCNO"]) == refno)
                    //        {
                    //            refno = Convert.ToString(dt.Rows[i]["DOCNO"]);
                    //            dt.Rows[i]["DOCNO"] = "";
                    //            dt.Rows[i]["DOCAMT"] = DBNull.Value;
                    //            dt.Rows[i]["DOCDT"] = DBNull.Value;
                    //        }
                    //        else
                    //        {
                    //            refno = Convert.ToString(dt.Rows[i]["DOCNO"]);
                    //        }

                    //        if (Convert.ToString(dt.Rows[i]["PARTYAC"]) == partyac)
                    //        {
                    //            partyac = Convert.ToString(dt.Rows[i]["PARTYAC"]);
                    //            dt.Rows[i]["PARTYAC"] = "";
                    //        }
                    //        else
                    //        {
                    //            partyac = Convert.ToString(dt.Rows[i]["PARTYAC"]);
                    //        }

                    //        if (Convert.ToString(dt.Rows[i]["IMEINO"]) == IMEI)
                    //        {
                    //            IMEI = Convert.ToString(dt.Rows[i]["IMEINO"]);
                    //            dt.Rows[i]["IMEINO"] = "";
                    //        }
                    //        else
                    //        {
                    //            IMEI = Convert.ToString(dt.Rows[i]["IMEINO"]);
                    //        }

                    //        if (Convert.ToString(dt.Rows[i]["ITEM"]) != null && Convert.ToString(dt.Rows[i]["ITEM"]) != string.Empty && Convert.ToString(dt.Rows[i]["ITEM"]) != "")
                    //        {
                    //            string[] para = { "MOBILE" };
                    //            var item = Convert.ToString(dt.Rows[i]["ITEM"]).Split(para, 0);
                    //            dt.Rows[i]["ITEM"] = Convert.ToString(item[0]);
                    //        }

                    //        if (i == 0)
                    //        {
                    //            refno = Convert.ToString(dt.Rows[i]["DOCNO"]);
                    //            partyac = Convert.ToString(dt.Rows[i]["PARTYAC"]);
                    //            IMEI = Convert.ToString(dt.Rows[i]["IMEINO"]);
                    //        }
                    //        //else if()
                    //        //{
                    //        //    refno = Convert.ToString(dt.Rows[i]["DOCNO"]);
                    //        //    partyac = Convert.ToString(dt.Rows[i]["PARTYAC"]);
                    //        //    IMEI = Convert.ToString(dt.Rows[i]["IMEINO"]);
                    //        //}


                    //    }

                    //    gvList.DataSource = dt;
                    //    gvList.DataBind();
                    //} 
                    #endregion
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
                    string attachment = "attachment; filename=VendorIMEILedger" + ddlVendor.SelectedItem.Text + ".xls";
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
        public class LedgerResponse
        {
            /// <summary>
            /// 
            /// </summary>
            public string MESSAGE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string STATUS_CODE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public DataTable DATA { get; set; }
        }
    }


}