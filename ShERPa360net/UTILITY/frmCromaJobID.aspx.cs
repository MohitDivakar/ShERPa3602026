using Newtonsoft.Json;
using RestSharp;
using ShERPa360net.Class;
using ShERPa360net.Models;
using Spire.Barcode;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.UTILITY
{
    public partial class frmCromaJobID : System.Web.UI.Page
    {

        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        WAClass objWAClass = new WAClass();
        DataTable dtMain = new DataTable();
        public SqlConnection ConnSherpa = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStringSherpa"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    if (Session["USERID"] != null)
                    {
                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menutabid.Value, "");
                        if (FormRights.bView == false)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }

                        if (Request.QueryString.Count > 0)
                        {
                            if (Convert.ToString(Request.QueryString["JOBID"]) != null && Convert.ToString(Request.QueryString["JOBID"]) != string.Empty && Convert.ToString(Request.QueryString["JOBID"]) != "")
                            {
                                Session["JOBID"] = Convert.ToString(Request.QueryString["JOBID"]);

                            }
                            Response.Redirect(Request.Url.AbsolutePath, false);
                        }
                        else
                        {
                            if (Session["JOBID"] != null && Convert.ToString(Session["JOBID"]) != "" && Convert.ToString(Session["JOBID"]) != string.Empty)
                            {
                                txtJobid.Text = Convert.ToString(Session["JOBID"]);
                                btnSearch_Click(1, e);
                                Session["JOBID"] = null;
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

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (((Page)this).IsPostBack && fuImage.PostedFile != null && fuImage.PostedFile.FileName.Length > 0)
                    {
                        string fileName = Path.GetFileName(fuImage.PostedFile.FileName);
                        string text = ((Page)this).Server.MapPath(fuImage.PostedFile.FileName);
                        fuImage.SaveAs(text);
                        string[] array = BarcodeScanner.Scan(text, (BarCodeType)9);
                        if (array.Count() > 1)
                        {
                            txtJobid.Text = Convert.ToString(array[1]);
                        }
                        else
                        {
                            txtJobid.Text = Convert.ToString(array[0]);
                        }

                        File.Delete(text);
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (ViewState["CromaJobIDSearch"] != null)
                    {
                        dtMain = (DataTable)ViewState["CromaJobIDSearch"];
                    }

                    DataTable dt = new DataTable();
                    dt = objMainClass.CheckRateJobDuplicate(objMainClass.intCmpId, txtJobid.Text, "GETJOBIDSEARCHNEW");
                    if (dt.Rows.Count > 0)
                    {
                        if (dtMain.Rows.Count > 0)
                        {
                            dtMain.Merge(dt);
                        }
                        else
                        {
                            dtMain = dt;
                        }
                        ViewState["CromaJobIDSearch"] = dtMain;
                        //gvList.DataSource = dtMain;
                        //gvList.DataBind();
                        dlListSaved.DataSource = dtMain;
                        dlListSaved.DataBind();
                        imgSaveAll.Visible = true;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
                    }
                    txtJobid.Text = string.Empty;
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

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    dtMain = null;
                    txtJobid.Text = string.Empty;
                    ViewState["CromaJobIDSearch"] = null;
                    //gvList.DataSource = null;
                    //gvList.DataBind();
                    dlListSaved.DataSource = null;
                    dlListSaved.DataBind();
                    imgSaveAll.Visible = false;
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

        protected void dlListSaved_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    Label lblJOBID = (Label)(e.Item.FindControl("lblJOBID"));
                    Label lblDEALERPRICE = (Label)(e.Item.FindControl("lblDEALERPRICE"));
                    GridView grvData = (GridView)(e.Item.FindControl("grvData"));
                    DataTable dt = new DataTable();
                    dt = objMainClass.CheckRateJobDuplicate(objMainClass.intCmpId, lblJOBID.Text, "GETJOBIDSEARCH");
                    grvData.DataSource = dt;
                    grvData.DataBind();
                    if (chkPartnerPrice.Checked == true)
                    {
                        lblDEALERPRICE.Visible = true;
                    }
                    else
                    {
                        lblDEALERPRICE.Visible = false;
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

        protected void imgSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (GetCount() > 0)
                    {
                        objBindDDL.FillState(ddlState);
                        ddlState.SelectedValue = "1";
                        ddlState_SelectedIndexChanged(1, e);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Job ID not selected to create SO. Select atleast one Job ID.!');", true);
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

        public int GetCount()
        {
            int iReturn = 0;
            try
            {
                if (Session["USERID"] != null)
                {
                    for (int i = 0; i < dlListSaved.Items.Count; i++)
                    {
                        DataListItem listItem = dlListSaved.Items[i];
                        CheckBox chkSelect = (CheckBox)listItem.FindControl("chkSelect");
                        if (chkSelect.Checked == true)
                        {
                            iReturn = iReturn + 1;
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
            return iReturn;
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    objBindDDL.FillCity(ddlCity, ddlState.SelectedValue);
                    if (ddlState.SelectedValue == "1")
                    {
                        ddlCity.SelectedValue = "3026";
                    }
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
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


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetItemCode(string prefixText, int count)
        {
            List<string> ItemCode = new List<string>();

            MainClass objMainClass = new MainClass();
            ItemCode = objMainClass.GetLikeJobData(prefixText, "GETLIKEJOBID");

            return ItemCode;

        }

        protected void btnCreateDoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    decimal totdiscount = 0;
                    decimal totalitemamount = 0;
                    decimal totaltaxamount = 0;
                    decimal totalbaseamount = 0;
                    int srno = 0;
                    string segment = "1043";
                    string distchnl = "50";
                    SOCreateClass objSOCreateClass = new SOCreateClass();
                    List<ITEMDETAIL> ItemDetails = new List<ITEMDETAIL>();
                    List<TAXDETAIL> TaxDetails = new List<TAXDETAIL>();

                    for (int i = 0; i < dlListSaved.Items.Count; i++)
                    {
                        DataListItem listItem = dlListSaved.Items[i];
                        CheckBox chkSelect = (CheckBox)listItem.FindControl("chkSelect");
                        Label lblBRAND = (Label)listItem.FindControl("lblBRAND");
                        Label lblSEGMENT = (Label)listItem.FindControl("lblSEGMENT");
                        Label lblDISTCHNL = (Label)listItem.FindControl("lblDISTCHNL");
                        Label lblITEMCODE = (Label)listItem.FindControl("lblITEMCODE");
                        Label lblITEMID = (Label)listItem.FindControl("lblITEMID");
                        Label lblIETMDESC = (Label)listItem.FindControl("lblIETMDESC");
                        Label lblITEMGRP = (Label)listItem.FindControl("lblITEMGRP");
                        Label lblJOBID = (Label)listItem.FindControl("lblJOBID");
                        Label lblPLANTCD = (Label)listItem.FindControl("lblPLANTCD");
                        Label lblLOCCD = (Label)listItem.FindControl("lblLOCCD");
                        Label lblSERIALNO = (Label)listItem.FindControl("lblSERIALNO");
                        Label lblMRP = (Label)listItem.FindControl("lblMRP");
                        Label lblCUSTOMERPRCE = (Label)listItem.FindControl("lblCUSTOMERPRCE");
                        TextBox txtFinalAmount = (TextBox)listItem.FindControl("txtFinalAmount");

                        Label lblCONDID = (Label)listItem.FindControl("lblCONDID");
                        Label lblCONDTYPE = (Label)listItem.FindControl("lblCONDTYPE");
                        Label lblRATE = (Label)listItem.FindControl("lblRATE");
                        segment = lblSEGMENT.Text;
                        distchnl = lblDISTCHNL.Text;
                        string costcenter = "1007";
                        DataTable dtCostCenter = new DataTable();
                        dtCostCenter = objMainClass.GetCostCenter(lblPLANTCD.Text, lblLOCCD.Text);
                        if (dtCostCenter.Rows.Count > 0)
                        {
                            costcenter = Convert.ToString(dtCostCenter.Rows[0]["CSTCENTCD"]);
                        }

                        decimal discount = Convert.ToDecimal(Convert.ToDecimal(lblCUSTOMERPRCE.Text) - Convert.ToDecimal(txtFinalAmount.Text));
                        decimal itemamount = Convert.ToDecimal(txtFinalAmount.Text);
                        decimal itemsaleamt = Convert.ToDecimal(lblCUSTOMERPRCE.Text);

                        decimal baseamount = Convert.ToDecimal(Convert.ToDecimal(itemamount * 100) / Convert.ToDecimal(100 + Convert.ToDecimal(lblRATE.Text)));
                        decimal taxamount = Convert.ToDecimal(baseamount * Convert.ToDecimal(lblRATE.Text) / 100);

                        if (chkSelect.Checked == true)
                        {
                            srno++;
                            totdiscount = totdiscount + discount;
                            totalitemamount = totalitemamount + itemsaleamt;
                            totaltaxamount = totaltaxamount + taxamount;
                            totalbaseamount = totalbaseamount + baseamount;

                            ItemDetails.Add(new ITEMDETAIL
                            {
                                CAMOUNT = Convert.ToString(baseamount),
                                CHANGEREASON = "",
                                COSTCENTER = costcenter,
                                CUSTPARTDESC = "",
                                CUSTPARTNO = "",
                                DELIDT = Convert.ToString(DateTime.Now.AddDays(2)),
                                DISCAMT = Convert.ToString(0),
                                GLCODE = "10010000",
                                GRADE = "GRADE A",
                                IMEINO = lblSERIALNO.Text,
                                ITEMCODE = lblITEMCODE.Text,
                                ITEMDESC = lblIETMDESC.Text,
                                ITEMGROUPID = lblITEMGRP.Text,
                                ITEMID = lblITEMID.Text,
                                ITEMTEXT = "SO CREATED FROM RATECARD",
                                JOBID = lblJOBID.Text,
                                LOCCD = lblLOCCD.Text,
                                OLDITEMID = "0",
                                PLANTCODE = lblPLANTCD.Text,
                                PRFCNT = "1000",
                                RATE = Convert.ToString(baseamount),
                                SCHEMEID = "0",
                                SONO = "",
                                SOQTY = "1",
                                SRNO = Convert.ToString(srno),
                                TAXAMT = "0",
                                UOM = "1"
                            });

                            TaxDetails.Add(new TAXDETAIL
                            {
                                BASEAMT = Convert.ToString(baseamount),
                                CONDID = lblCONDID.Text,
                                CONDTYPE = lblCONDTYPE.Text,
                                GLCODE = "10010000",
                                OPERATOR = "+",
                                PID = "0",
                                RATE = Convert.ToString(lblRATE.Text),
                                SONO = "",
                                SOSRNO = Convert.ToString(srno),
                                SRNO = Convert.ToString(srno),
                                TAXAMT = Convert.ToString(taxamount)
                            });

                        }

                    }

                    objSOCreateClass.BILLTOCODE = "0000010003";
                    objSOCreateClass.CITY = ddlCity.SelectedItem.Text;
                    objSOCreateClass.COMMAGENT = Convert.ToString(Session["COMMAGENT"]);
                    objSOCreateClass.CUSTADD1 = txtAddress1.Text.ToUpper();
                    objSOCreateClass.CUSTADD2 = txtAddress2.Text.ToUpper();
                    objSOCreateClass.CUSTADD3 = txtAddress3.Text.ToUpper();
                    objSOCreateClass.CUSTEMAILID = "";
                    objSOCreateClass.CUSTMOBILENO = txtCustMobileNo.Text;
                    objSOCreateClass.CUSTNAME = txtCustName.Text.ToUpper();
                    objSOCreateClass.DISTCHNL = distchnl;
                    objSOCreateClass.ENTITYID = "";
                    objSOCreateClass.GOKWIKFLAG = "";
                    objSOCreateClass.JOBID = "";
                    objSOCreateClass.NETSOAMT = Convert.ToString(totalbaseamount + totaltaxamount);
                    objSOCreateClass.NETTAXAMT = Convert.ToString(totaltaxamount);
                    objSOCreateClass.NETVALUEAMT = Convert.ToString(totalbaseamount);
                    objSOCreateClass.PAYGATEWAY = "0";
                    objSOCreateClass.PAYMENTTERMS = "ADV";
                    objSOCreateClass.PAYMODE = 1;
                    objSOCreateClass.PINCODE = Convert.ToString(txtPinCode.Text);
                    objSOCreateClass.PREPAIDAMT = "0";
                    objSOCreateClass.REFDT = DateTime.Now.ToShortDateString();
                    objSOCreateClass.REFNO = Convert.ToString(DateTime.Now).Replace("-", "").Replace(":", "").Replace(".", "").Replace(" ", "");
                    objSOCreateClass.REMAINAMT = Convert.ToString(totalbaseamount + totaltaxamount);
                    objSOCreateClass.REMARKS = "SO CREATED FROM RATECARD";
                    objSOCreateClass.SALESFORM = 11411;
                    objSOCreateClass.SCHEMEID = 11913;
                    objSOCreateClass.SEGMENT = segment;
                    objSOCreateClass.SHIPTOCODE = "0000010003";
                    objSOCreateClass.SODATE = DateTime.Now.ToShortDateString();
                    objSOCreateClass.SONO = "";
                    objSOCreateClass.SOTYPE = "PSO";
                    objSOCreateClass.STATEID = Convert.ToInt32(ddlState.SelectedValue);
                    objSOCreateClass.STATUS = 57;
                    objSOCreateClass.TOTALDISCOUNTAMT = Convert.ToString(0);
                    objSOCreateClass.TXNDT = "";
                    objSOCreateClass.TXNNO = "";
                    objSOCreateClass.UTMCAMPAIGN = "";
                    objSOCreateClass.UTMMEDIUM = "";
                    objSOCreateClass.UTMSOURCE = "";
                    objSOCreateClass.TAXDETAILS = TaxDetails;
                    objSOCreateClass.ITEMDETAILS = ItemDetails;
                    //objSOCreateClass.CHARGEDETAILS =;


                    string PRURL = "http://14.98.132.190:1503/api/CreateSO";
                    //string PRURL = "http://3.6.38.46/api/CreateSO";
                    //string PRURL = "http://localhost:44397/api/CreateSO";
                    var client = new RestClient(PRURL);
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("APP_NAME", "SALESORDER");
                    request.AddHeader("KEY_NAME", "SALES_KEY");
                    request.AddHeader("SALES_KEY", "SALESORDER*&^%$");
                    request.AddHeader("Content-Type", "application/json");
                    var jsonInput = JsonConvert.SerializeObject(objSOCreateClass);
                    request.AddParameter("application/json", jsonInput, ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        SORESPONSE objSORESPONSE = new SORESPONSE();
                        string jsonconn = response.Content;
                        objSORESPONSE = JsonConvert.DeserializeObject<SORESPONSE>(jsonconn);

                        SqlCommand cmdc = new SqlCommand("SP_SO_MASTER", ConnSherpa);
                        cmdc.Parameters.AddWithValue("@CREATEBY", Convert.ToInt32(Session["USERID"]));
                        cmdc.Parameters.AddWithValue("@SONO", objSORESPONSE.SONO);
                        cmdc.Parameters.AddWithValue("@ACTION", "UPDATESOCREATEBY");
                        cmdc.CommandType = CommandType.StoredProcedure;
                        cmdc.Connection.Open();
                        cmdc.ExecuteNonQuery();
                        cmdc.Connection.Close();


                        for (int i = 0; i < dlListSaved.Items.Count; i++)
                        {
                            DataListItem listItem = dlListSaved.Items[i];
                            CheckBox chkSelect = (CheckBox)listItem.FindControl("chkSelect");
                            Label lblJOBID = (Label)listItem.FindControl("lblJOBID");
                            if (chkSelect.Checked == true)
                            {
                                SqlCommand cmdA = new SqlCommand("SP_SO_MASTER", ConnSherpa);
                                cmdA.Parameters.AddWithValue("@CUSTADD1", txtAddress1.Text.ToUpper());
                                cmdA.Parameters.AddWithValue("@CUSTADD2", txtAddress2.Text.ToUpper());
                                cmdA.Parameters.AddWithValue("@CUSTADD3", txtAddress3.Text.ToUpper());
                                cmdA.Parameters.AddWithValue("@CITY", ddlCity.SelectedItem.Text);
                                cmdA.Parameters.AddWithValue("@STATEID", Convert.ToInt32(ddlState.SelectedValue));
                                cmdA.Parameters.AddWithValue("@PINCODE", txtPinCode.Text);
                                cmdA.Parameters.AddWithValue("@CUSTMOBILENO", txtCustMobileNo.Text);
                                cmdA.Parameters.AddWithValue("@CUSTEMAILID", "");
                                cmdA.Parameters.AddWithValue("@JOBID", objMainClass.strConvertZeroPadding(lblJOBID.Text));
                                cmdA.Parameters.AddWithValue("@CUSTNAME", txtCustName.Text.ToUpper());
                                cmdA.Parameters.AddWithValue("@UPDATEBY", Convert.ToInt32(Session["USERID"]));
                                cmdA.Parameters.AddWithValue("@ACTION", "UPDATEJOB");
                                cmdA.CommandType = CommandType.StoredProcedure;
                                cmdA.Connection.Open();
                                cmdA.ExecuteNonQuery();
                                cmdA.Connection.Close();

                                SqlCommand cmdB = new SqlCommand("SP_SO_MASTER", ConnSherpa);
                                cmdB.Parameters.AddWithValue("@REFNO", objSOCreateClass.REFNO);
                                cmdB.Parameters.AddWithValue("@JOBID", objMainClass.strConvertZeroPadding(lblJOBID.Text));
                                cmdB.Parameters.AddWithValue("@ACTION", "UPDATEJWREFCROMA");
                                cmdB.CommandType = CommandType.StoredProcedure;
                                cmdB.Connection.Open();
                                cmdB.ExecuteNonQuery();
                                cmdB.Connection.Close();

                            }
                        }

                        string msg = "New SO Created. SO NO. is : " + objSORESPONSE.SONO;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" " + msg + "\");$('.close').click(function(){window.location.href ='frmCromaJobID.aspx' });", true);

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

        protected void txtItemDesc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    txtJobid.Text = txtItemDesc.Text.Split('-')[0].Trim().ToString();
                    txtItemDesc.Text = "";
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

        protected void chkPartnerPrice_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    for (int i = 0; i < dlListSaved.Items.Count; i++)
                    {
                        DataListItem listItem = dlListSaved.Items[i];
                        Label lblDEALERPRICE = (Label)listItem.FindControl("lblDEALERPRICE");
                        if (chkPartnerPrice.Checked == true)
                        {
                            lblDEALERPRICE.Visible = true;
                        }
                        else
                        {
                            lblDEALERPRICE.Visible = false;
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