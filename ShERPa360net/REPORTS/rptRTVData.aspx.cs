using Newtonsoft.Json;
using RestSharp;
using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.REPORTS
{
    public partial class rptRTVData : System.Web.UI.Page
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
                            //Session.Abandon();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }

                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");//Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();

                        objBindDDL.FillPlant(ddlPlantCode, "SEARCH");
                        string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                        ddlPlantCode.SelectedValue = plantArray[0];
                        objBindDDL.FillItemGrp(ddlItemGroup);
                        ddlItemGroup.SelectedValue = "9";
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
            //GetTRNData
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dtAPI = new DataTable();
                    dtAPI = objMainClass.GetWAData("GETRTVDATA", 1, "GETWADATA");
                    if (dtAPI.Rows.Count > 0)
                    {
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };


                        string URL = Convert.ToString(dtAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtAPI.Rows[0]["TOKEN"]);
                        string PLANTCD = ddlPlantCode.SelectedIndex == 0 ? Convert.ToString(Session["PLANTCD"]) : ddlPlantCode.SelectedValue;
                        //PLANTCD = PLANTCD.Replace(",", "','");
                        string VENDCODE = "";
                        var client = new RestClient(URL + "?CMPID=" + objMainClass.intCmpId + "&FROMDATE=" + txtFromDate.Text + "&TODATE=" + txtToDate.Text + "&RTVSTATUS=" + ddlRTVStatus.SelectedValue + "&ITEMGRPID=" + ddlItemGroup.SelectedValue + "&PLANTCD=" + PLANTCD + "&VENDCODE=" + VENDCODE + "&ACTION=GETREPORT");
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("" + Convert.ToString(dtAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtAPI.Rows[0]["KEYVALUE"]) + "");
                        request.AddHeader("Content-Type", "application/json");
                        IRestResponse response = client.Execute(request);

                        RTVResponse objRTVResponse = new RTVResponse();
                        string jsonconn = response.Content;
                        objRTVResponse = JsonConvert.DeserializeObject<RTVResponse>(jsonconn);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {

                            if (objRTVResponse.DTRTV.Rows.Count > 0)
                            {
                                gvList.DataSource = objRTVResponse.DTRTV;
                                gvList.DataBind();
                                gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                            }
                            else
                            {
                                gvList.DataSource = string.Empty;
                                gvList.DataBind();
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Data Found.!');", true);
                            }
                        }
                        else
                        {
                            gvList.DataSource = string.Empty;
                            gvList.DataBind();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + objRTVResponse.MESSAGE + "\");", true); ;
                        }


                        string URL1 = Convert.ToString(dtAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtAPI.Rows[0]["TOKEN"]);
                        //PLANTCD = PLANTCD.Replace(",", "','");
                        var client1 = new RestClient(URL1 + "?CMPID=" + objMainClass.intCmpId + "&FROMDATE=" + txtFromDate.Text + "&TODATE=" + txtToDate.Text + "&RTVSTATUS=" + ddlRTVStatus.SelectedValue + "&ITEMGRPID=" + ddlItemGroup.SelectedValue + "&PLANTCD=" + PLANTCD + "&VENDCODE=" + VENDCODE + "&ACTION=GETREPORTSUMMARY");
                        client1.Timeout = -1;
                        var request1 = new RestRequest(Method.POST);
                        request1.AddHeader("" + Convert.ToString(dtAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtAPI.Rows[0]["KEYVALUE"]) + "");
                        request1.AddHeader("Content-Type", "application/json");
                        IRestResponse response1 = client1.Execute(request1);

                        RTVResponse objRTVResponse1 = new RTVResponse();
                        string jsonconn1 = response1.Content;
                        objRTVResponse1 = JsonConvert.DeserializeObject<RTVResponse>(jsonconn1);

                        if (response1.StatusCode == System.Net.HttpStatusCode.OK)
                        {

                            if (objRTVResponse1.DTRTV.Rows.Count > 0)
                            {
                                gvSummary.DataSource = objRTVResponse1.DTRTV;
                                gvSummary.DataBind();
                                gvSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
                            }
                            else
                            {
                                gvSummary.DataSource = string.Empty;
                                gvSummary.DataBind();
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Summary Not Data Found.!');", true);
                            }
                        }
                        else
                        {
                            gvSummary.DataSource = string.Empty;
                            gvSummary.DataBind();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + objRTVResponse1.MESSAGE + "\");", true); ;
                        }


                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('API Not Found. Please Contact API Provider.');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true); ;
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

                    if (gvList.Rows.Count > 0)
                    {
                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
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
        protected void lnkSearh_Click(object sender, EventArgs e)
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true); ;
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
                    string attachment = "attachment; filename=RTVDataReport.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vdn.ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    gvList.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();

                    if (gvList.Rows.Count > 0)
                    {
                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
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