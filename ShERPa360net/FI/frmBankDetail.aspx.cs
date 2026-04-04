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

namespace ShERPa360net.FI
{
    public partial class frmBankDetail : System.Web.UI.Page
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
                        objBindDDL.FillLists(ddlAccountType, "ACT");
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

        protected void lnkSearhVend_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetUnregiVendor(objMainClass.intCmpId, txtVendCode.Text, "SELECTONE");

                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dt.Rows[0]["ISACTIVE"]) != 1)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Vendor deactivated or not approved!');", true);
                        }
                        txtVendorCode.Text = Convert.ToString(dt.Rows[0]["VENDCODE"]);
                        txtBankName.Text = Convert.ToString(dt.Rows[0]["BANKNAME"]);
                        txtIFSCCode.Text = Convert.ToString(dt.Rows[0]["IFSCCODE"]);
                        txtACNo.Text = Convert.ToString(dt.Rows[0]["ACCOUNTNO"]);
                        ddlAccountType.SelectedValue = Convert.ToString(dt.Rows[0]["ACCNTTYPE"]);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
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

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    string result = CheckAccountNumber(txtACNo.Text);
                    if(result== "true")
                    {
                        lblAccntNoExist.Visible = true;
                    }
                    else
                    {
                        lblAccntNoExist.Visible = false;
                        byte[] CHEQUE = null;
                        string CHEQUETYPE = ".jpeg";
                        FileUpload fudCHEQUE;

                        if (fuCheque != null)
                        {
                            if (fuCheque.HasFiles)
                            {
                                Session["FUCHEQUES"] = fuCheque;
                            }
                        }

                        if (Session["FUCHEQUES"] != null)
                        {
                            fudCHEQUE = Session["FUCHEQUES"] as FileUpload;
                            if (fudCHEQUE.HasFiles)
                            {
                                using (BinaryReader br = new BinaryReader(fudCHEQUE.PostedFile.InputStream))
                                {
                                    CHEQUE = br.ReadBytes(fudCHEQUE.PostedFile.ContentLength);
                                    CHEQUETYPE = System.IO.Path.GetExtension(fudCHEQUE.FileName);
                                }
                            }
                        }


                        //int iResult = objMainClass.UpdateVendorBank(objMainClass.intCmpId, txtVendorCode.Text, txtBankName.Text, txtIFSCCode.Text, txtACNo.Text,
                        //    Convert.ToInt32(ddlAccountType.SelectedValue), Convert.ToInt32(Session["USERID"]), "UPDATEBANKDETAIL", CHEQUETYPE, CHEQUE);

                        //if (iResult == 1)
                        //{
                        //    UpdateTallyData();
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Bank details not updated in SHERPA and Tally.!');", true);
                        //}
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


        public void UpdateTallyData()
        {
            try
            {
                if (Session["USERID"] != null)
                {


                    string RequestXML = TallyXMLData(txtVendorCode.Text);
                    if (!RequestXML.Contains("Error"))
                    {
                        if (RequestXML != "" && RequestXML != null && RequestXML != string.Empty)
                        {
                            WebRequest TallyRequest;
                            //TallyRequest = WebRequest.Create("http://localhost:9000");
                            TallyRequest = WebRequest.Create("http://DESKTOP-C7VLIGF:9000");
                            ((HttpWebRequest)TallyRequest).UserAgent = ".NET Framework Example Client";
                            TallyRequest.Method = "Post";
                            string postData = RequestXML;
                            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(postData);
                            TallyRequest.ContentType = "application/x-www-form-urlencoded";
                            TallyRequest.ContentLength = byteArray.Length;
                            Stream dataStream = TallyRequest.GetRequestStream();
                            dataStream.Write(byteArray, 0, byteArray.Length);
                            dataStream.Close();
                            WebResponse response = TallyRequest.GetResponse();
                            string Response = (((HttpWebResponse)response).StatusDescription).ToString();
                            dataStream = response.GetResponseStream();
                            StreamReader reader = new StreamReader(dataStream);
                            string responseFromTallyServer = reader.ReadToEnd().ToString();
                            System.Data.DataSet TallyResponseDataSet = new System.Data.DataSet();
                            TallyResponseDataSet.ReadXml(new StringReader(responseFromTallyServer));
                            reader.Close();
                            dataStream.Close();
                            response.Close();
                            if (TallyResponseDataSet.Tables.Count > 0)
                            {
                                DataTable TallyResponseDataTable = TallyResponseDataSet.Tables[0];

                                if (TallyResponseDataTable.Rows.Count > 0)
                                {
                                    if (responseFromTallyServer.Contains("<LINEERROR>"))
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(" + Convert.ToString(TallyResponseDataTable.Rows[0]["LINEERROR"]) + ");", true);
                                    }
                                    else
                                    {
                                        if (Convert.ToString(TallyResponseDataTable.Rows[0]["ALTERED"]) == "1")
                                        {
                                            txtVendorCode.Text = string.Empty;
                                            txtIFSCCode.Text = string.Empty;
                                            txtBankName.Text = string.Empty;
                                            txtACNo.Text = string.Empty;
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Bank detials updated in SHERPA and Tally.!');", true);
                                            txtVendorCode.Text = string.Empty;
                                            txtIFSCCode.Text = string.Empty;
                                            txtBankName.Text = string.Empty;
                                            txtACNo.Text = string.Empty;
                                        }
                                        else if (Convert.ToString(TallyResponseDataTable.Rows[0]["ERRORS"]) == "1")
                                        {
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Bank details updated in SHERPA. But, not in Tally.!');", true);
                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Bank details updated in SHERPA. But, not in Tally.!');", true);
                                        }
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Vendor Not Approved .  Vendor Not Created in Tally.!');", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Vendor Not   Approved in ShERPa.  Vendor Not Created in Tally');", true);
                            }
                        }
                        else
                        {
                            RequestXML = "XML not created as required for Tally request.!";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + RequestXML + "\");", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + RequestXML + "\");", true);
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


        protected void txtIFSCCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (txtIFSCCode.Text != string.Empty && txtIFSCCode.Text != "" && txtIFSCCode.Text != null)
                    {
                        DataTable Bankdt = new DataTable();
                        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                        var client = new RestClient(("https://ifsc.firstatom.org/key/84NnS05xbn6U5RKP1dGEBU83g/ifsc/" + txtIFSCCode.Text));
                        client.Timeout = -1;
                        var request = new RestRequest(Method.GET);
                        IRestResponse response = client.Execute(request);
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            string jsonconn = response.Content;
                            jsonconn = "[" + jsonconn + "]";
                            Bankdt = (DataTable)JsonConvert.DeserializeObject(jsonconn, (typeof(DataTable)));


                            if (Bankdt.Rows.Count > 0)
                            {
                                txtBankName.Text = Convert.ToString(Bankdt.Rows[0]["Bank"]) + " - " + Convert.ToString(Bankdt.Rows[0]["Branch"]);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Incorrect IFSC Code. Bank Details Not Found!');$('#txtIFSCCode').focus();", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Incorrect IFSC Code!');$('#txtIFSCCode').focus();", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please enter IFSC Code.!');$('#txtIFSCCode').focus();", true);
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


        public string TallyXMLData(string VENDORCODE)
        {
            string TallyData = "";
            DataTable dt = new DataTable();
            dt = objMainClass.GetUnregiVendor(objMainClass.intCmpId, objMainClass.strConvertZeroPadding(VENDORCODE), "SELECTONE");

            string createdate = DateTime.Now.ToString("yyyy/MM/dd").Replace("/", "").Replace("-", "");

            if (dt.Rows.Count > 0)
            {

                TallyData = "<ENVELOPE>";
                TallyData = (TallyData + "<HEADER>");
                TallyData = (TallyData + "<TALLYREQUEST>Import Data</TALLYREQUEST>");
                TallyData = (TallyData + "</HEADER>");
                TallyData = (TallyData + "<BODY>");
                TallyData = (TallyData + "<IMPORTDATA>");
                TallyData = (TallyData + "<REQUESTDESC>");
                TallyData = (TallyData + "<REPORTNAME>All Masters</REPORTNAME>");
                TallyData = (TallyData + "<STATICVARIABLES>");
                //TallyData = (TallyData + "<SVCURRENTCOMPANY>Demo</SVCURRENTCOMPANY>");
                TallyData = (TallyData + "<SVCURRENTCOMPANY>QarmaTek Services Pvt.Ltd.</SVCURRENTCOMPANY>");
                TallyData = (TallyData + "</STATICVARIABLES>");
                TallyData = (TallyData + "</REQUESTDESC>");
                TallyData = (TallyData + "<REQUESTDATA>");
                TallyData = (TallyData + "<TALLYMESSAGE xmlns:UDF='TallyUDF'>");
                TallyData = (TallyData + "<LEDGER NAME = '" + Convert.ToString(dt.Rows[0]["SHOPNAME"]) + "' ACTION = 'UPDATE'>");
                TallyData = (TallyData + "<ADDRESS.LIST>");
                TallyData = (TallyData + "<ADDRESS> " + Convert.ToString(dt.Rows[0]["ADDR1"]) + " </ADDRESS>");
                if (Convert.ToString(dt.Rows[0]["ADDR3"]) != "" && Convert.ToString(dt.Rows[0]["ADDR3"]) != null && Convert.ToString(dt.Rows[0]["ADDR3"]) != string.Empty)
                {
                    TallyData = (TallyData + "<ADDRESS> " + Convert.ToString(dt.Rows[0]["ADDR2"]) + Convert.ToString(dt.Rows[0]["ADDR3"]) + " </ADDRESS>");
                }
                else
                {
                    TallyData = (TallyData + "<ADDRESS> " + Convert.ToString(dt.Rows[0]["ADDR2"]) + " </ADDRESS>");
                }
                if (Convert.ToString(dt.Rows[0]["AADHARNO"]) != "" && Convert.ToString(dt.Rows[0]["AADHARNO"]) != null && Convert.ToString(dt.Rows[0]["AADHARNO"]) != string.Empty)
                {
                    TallyData = (TallyData + "<ADDRESS> Aadhar No - " + Convert.ToString(dt.Rows[0]["AADHARNO"]) + "</ADDRESS>");
                }
                TallyData = (TallyData + "</ADDRESS.LIST>");
                TallyData = (TallyData + "<STATENAME>" + Convert.ToString(dt.Rows[0]["STATE"]) + "</STATENAME>");
                TallyData = (TallyData + "<PINCODE>" + Convert.ToString(dt.Rows[0]["POSTALCODE"]) + "</PINCODE>");
                TallyData = (TallyData + "<LEDGERCONTACT>" + Convert.ToString(dt.Rows[0]["MOBILENO"]) + "</LEDGERCONTACT>");
                TallyData = (TallyData + "<LEDGERPHONE>" + Convert.ToString(dt.Rows[0]["MOBILENO"]) + "</LEDGERPHONE>");
                TallyData = (TallyData + "<LEDGERFAX></LEDGERFAX>");
                TallyData = (TallyData + "<LEDGERMOBILE>" + Convert.ToString(dt.Rows[0]["MOBILENO"]) + "</LEDGERMOBILE>");
                TallyData = (TallyData + "<EMAIL>" + Convert.ToString(dt.Rows[0]["EMAILID"]) + " </EMAIL>");
                TallyData = (TallyData + "<MAILINGNAME.LIST>");
                TallyData = (TallyData + "<MAILINGNAME>" + Convert.ToString(dt.Rows[0]["SHOPNAME"]) + "</MAILINGNAME>");
                TallyData = (TallyData + "</MAILINGNAME.LIST>");
                TallyData = (TallyData + "<OLDAUDITENTRYIDS.LIST TYPE = 'Number'>");
                TallyData = (TallyData + "<OLDAUDITENTRYIDS> -1 </OLDAUDITENTRYIDS>");
                TallyData = (TallyData + "</OLDAUDITENTRYIDS.LIST>");
                TallyData = (TallyData + "<PRIORSTATENAME>" + Convert.ToString(dt.Rows[0]["STATE"]) + "</PRIORSTATENAME>");
                TallyData = (TallyData + "<INCOMETAXNUMBER>" + Convert.ToString(dt.Rows[0]["PANNO"]) + "</INCOMETAXNUMBER>");
                TallyData = (TallyData + "<VATTINNUMBER></VATTINNUMBER>");
                TallyData = (TallyData + "<CSTNUMBER></CSTNUMBER>");
                TallyData = (TallyData + "<OPENINGBALANCE>0</OPENINGBALANCE>");
                TallyData = (TallyData + "<COUNTRYNAME>" + Convert.ToString(dt.Rows[0]["COUNTRY"]) + "</COUNTRYNAME>");
                TallyData = (TallyData + "<GSTREGISTRATIONTYPE> Regular </GSTREGISTRATIONTYPE>");
                TallyData = (TallyData + "<VATDEALERTYPE> Regular </VATDEALERTYPE>");
                //if (Convert.ToString(dt.Rows[0]["TALLYGROUP"]) != string.Empty && Convert.ToString(dt.Rows[0]["TALLYGROUP"]) != null && Convert.ToString(dt.Rows[0]["TALLYGROUP"]) != "")
                //{
                //    TallyData = (TallyData + "<PARENT>" + Convert.ToString(dt.Rows[0]["TALLYGROUP"]) + "</PARENT>"); //Needs to be change
                //}
                //else
                //{
                //    TallyData = (TallyData + "<PARENT>Mobex Refurb Creditor - Retail</PARENT>"); //Needs to be change
                //}

                if (Convert.ToString(dt.Rows[0]["TALLYGROUP"]) != string.Empty && Convert.ToString(dt.Rows[0]["TALLYGROUP"]) != null && Convert.ToString(dt.Rows[0]["TALLYGROUP"]) != "")
                {
                    TallyData = (TallyData + "<PARENT>" + Convert.ToString(dt.Rows[0]["TALLYGROUP"]) + "</PARENT>"); //Needs to be change
                }
                else
                {
                    if (Convert.ToString(dt.Rows[0]["STATEID"]) == "1")
                    {
                        TallyData = (TallyData + "<PARENT>Mobex Refurb Creditor - Retail</PARENT>"); //Needs to be change
                    }
                    else
                    {
                        TallyData = (TallyData + "<PARENT>Mobex Refurb Creditor- Retail Bangalore</PARENT>"); //Needs to be change
                    }
                }


                TallyData = (TallyData + "<LANGUAGENAME.LIST>");
                TallyData = (TallyData + "<NAME.LIST>");
                TallyData = (TallyData + "<NAME>" + Convert.ToString(dt.Rows[0]["SHOPNAME"]) + "</NAME>");
                TallyData = (TallyData + "<NAME>" + Convert.ToString(dt.Rows[0]["VENDCODE"]).Substring(5, 5) + "</NAME>");
                TallyData = (TallyData + "</NAME.LIST>");
                TallyData = (TallyData + "<LANGUAGEID>1033</LANGUAGEID>");
                TallyData = (TallyData + "</LANGUAGENAME.LIST>");
                TallyData = (TallyData + "<PAYMENTDETAILS.LIST>");
                TallyData = (TallyData + "<IFSCODE>" + Convert.ToString(dt.Rows[0]["IFSCCODE"]) + "</IFSCODE>");
                TallyData = (TallyData + "<BANKNAME>" + Convert.ToString(dt.Rows[0]["BANKNAME"]) + "</BANKNAME>");
                TallyData = (TallyData + "<ACCOUNTNUMBER>" + Convert.ToString(dt.Rows[0]["ACCOUNTNO"]) + "</ACCOUNTNUMBER>");
                TallyData = (TallyData + "<PAYMENTFAVOURING>" + Convert.ToString(dt.Rows[0]["SHOPNAME"]) + "</PAYMENTFAVOURING>");
                TallyData = (TallyData + "<TRANSACTIONNAME>Primary</TRANSACTIONNAME>");
                TallyData = (TallyData + "<BANKID>3</BANKID>");
                TallyData = (TallyData + "<SETASDEFAULT>Yes</SETASDEFAULT>");
                TallyData = (TallyData + "<DEFAULTTRANSACTIONTYPE>Inter Bank Transfer</DEFAULTTRANSACTIONTYPE>");
                TallyData = (TallyData + "<BENEFICIARYCODEDETAILS.LIST></BENEFICIARYCODEDETAILS.LIST>");
                TallyData = (TallyData + "</PAYMENTDETAILS.LIST>");
                TallyData = (TallyData + "<CREATEDDATE>" + createdate + "</CREATEDDATE>");
                TallyData = (TallyData + "<CREATEDBY> manmohan1 </CREATEDBY>");
                TallyData = (TallyData + "<TAXCLASSIFICATIONNAME/>");
                TallyData = (TallyData + "<TAXTYPE> Others </TAXTYPE>");
                TallyData = (TallyData + "<COUNTRYOFRESIDENCE>" + Convert.ToString(dt.Rows[0]["COUNTRY"]) + "</COUNTRYOFRESIDENCE>");
                TallyData = (TallyData + "<GSTTYPE/>");
                TallyData = (TallyData + "<APPROPRIATEFOR/>");
                TallyData = (TallyData + "<USEDFORTAXTYPE/>");
                TallyData = (TallyData + "<PARTYGSTIN>" + Convert.ToString(dt.Rows[0]["GSTNO"]) + "</PARTYGSTIN>");
                TallyData = (TallyData + "<LEDSTATENAME> " + Convert.ToString(dt.Rows[0]["COUNTRY"]) + " </LEDSTATENAME>");
                TallyData = (TallyData + "<EXCISELEDGERCLASSIFICATION/>");
                TallyData = (TallyData + "<EXCISEDUTYTYPE/>");
                TallyData = (TallyData + "<EXCISENATUREOFPURCHASE/>");
                TallyData = (TallyData + "<LEDGERFBTCATEGORY/>");
                TallyData = (TallyData + "<ISSUBLEDGER>No</ISSUBLEDGER>");
                TallyData = (TallyData + "<ISBILLWISEON>Yes</ISBILLWISEON>");
                TallyData = (TallyData + "<ISCOSTCENTRESON> No </ISCOSTCENTRESON>");
                TallyData = (TallyData + "<ISINTERESTON> No </ISINTERESTON>");
                TallyData = (TallyData + "<ALLOWINMOBILE> No </ALLOWINMOBILE>");
                TallyData = (TallyData + "<ISCOSTTRACKINGON>No</ISCOSTTRACKINGON>");
                TallyData = (TallyData + "<ISBENEFICIARYCODEON>No</ISBENEFICIARYCODEON>");
                TallyData = (TallyData + "<ISEXPORTONVCHCREATE>No</ISEXPORTONVCHCREATE>");
                TallyData = (TallyData + "<PLASINCOMEEXPENSE>No</PLASINCOMEEXPENSE>");
                TallyData = (TallyData + "<ISUPDATINGTARGETID>No</ISUPDATINGTARGETID>");
                TallyData = (TallyData + "<ASORIGINAL>Yes</ASORIGINAL>");
                TallyData = (TallyData + "<ISCONDENSED>No</ISCONDENSED>");
                TallyData = (TallyData + "<AFFECTSSTOCK>No</AFFECTSSTOCK>");
                TallyData = (TallyData + "<ISRATEINCLUSIVEVAT>No</ISRATEINCLUSIVEVAT>");
                TallyData = (TallyData + "<FORPAYROLL>No</FORPAYROLL>");
                TallyData = (TallyData + "<ISABCENABLED>No</ISABCENABLED>");
                TallyData = (TallyData + "<ISCREDITDAYSCHKON>No</ISCREDITDAYSCHKON>");
                TallyData = (TallyData + "<INTERESTONBILLWISE>No</INTERESTONBILLWISE>");
                TallyData = (TallyData + "<OVERRIDEINTEREST>No</OVERRIDEINTEREST>");
                TallyData = (TallyData + "<OVERRIDEADVINTEREST>No</OVERRIDEADVINTEREST>");
                TallyData = (TallyData + "<USEFORVAT>No</USEFORVAT>");
                TallyData = (TallyData + "<IGNORETDSEXEMPT>No</IGNORETDSEXEMPT>");
                TallyData = (TallyData + "<ISTCSAPPLICABLE>No</ISTCSAPPLICABLE>");
                TallyData = (TallyData + "<ISTDSAPPLICABLE>No</ISTDSAPPLICABLE>");
                TallyData = (TallyData + "<ISFBTAPPLICABLE>No</ISFBTAPPLICABLE>");
                TallyData = (TallyData + "<ISGSTAPPLICABLE>No</ISGSTAPPLICABLE>");
                TallyData = (TallyData + "<ISEXCISEAPPLICABLE>No</ISEXCISEAPPLICABLE>");
                TallyData = (TallyData + "<ISTDSEXPENSE>No</ISTDSEXPENSE>");
                TallyData = (TallyData + "<ISEDLIAPPLICABLE>No</ISEDLIAPPLICABLE>");
                TallyData = (TallyData + "<ISRELATEDPARTY>No</ISRELATEDPARTY>");
                TallyData = (TallyData + "<USEFORESIELIGIBILITY>No</USEFORESIELIGIBILITY>");
                TallyData = (TallyData + "<ISINTERESTINCLLASTDAY>No</ISINTERESTINCLLASTDAY>");
                TallyData = (TallyData + "<APPROPRIATETAXVALUE>No</APPROPRIATETAXVALUE>");
                TallyData = (TallyData + "<ISBEHAVEASDUTY>No</ISBEHAVEASDUTY>");
                TallyData = (TallyData + "<INTERESTINCLDAYOFADDITION>No</INTERESTINCLDAYOFADDITION>");
                TallyData = (TallyData + "<INTERESTINCLDAYOFDEDUCTION>No</INTERESTINCLDAYOFDEDUCTION>");
                TallyData = (TallyData + "<ISOTHTERRITORYASSESSEE>No</ISOTHTERRITORYASSESSEE>");
                TallyData = (TallyData + "<IGNOREMISMATCHWITHWARNING>No</IGNOREMISMATCHWITHWARNING>");
                TallyData = (TallyData + "<USEASNOTIONALBANK>No</USEASNOTIONALBANK>");
                TallyData = (TallyData + "<OVERRIDECREDITLIMIT>No</OVERRIDECREDITLIMIT>");
                TallyData = (TallyData + "<ISAGAINSTFORMC>No</ISAGAINSTFORMC>");
                TallyData = (TallyData + "<ISCHEQUEPRINTINGENABLED>Yes</ISCHEQUEPRINTINGENABLED>");
                TallyData = (TallyData + "<ISPAYUPLOAD>No</ISPAYUPLOAD>");
                TallyData = (TallyData + "<ISPAYBATCHONLYSAL>No</ISPAYBATCHONLYSAL>");
                TallyData = (TallyData + "<ISBNFCODESUPPORTED>No</ISBNFCODESUPPORTED>");
                TallyData = (TallyData + "<ALLOWEXPORTWITHERRORS>No</ALLOWEXPORTWITHERRORS>");
                TallyData = (TallyData + "<CONSIDERPURCHASEFOREXPORT>No</CONSIDERPURCHASEFOREXPORT>");
                TallyData = (TallyData + "<ISTRANSPORTER>No</ISTRANSPORTER>");
                TallyData = (TallyData + "<USEFORNOTIONALITC>No</USEFORNOTIONALITC>");
                TallyData = (TallyData + "<ISECOMMOPERATOR>No</ISECOMMOPERATOR>");
                TallyData = (TallyData + "<OVERRIDEBASEDONREALIZATION>No</OVERRIDEBASEDONREALIZATION>");
                TallyData = (TallyData + "<SHOWINPAYSLIP>No</SHOWINPAYSLIP>");
                TallyData = (TallyData + "<USEFORGRATUITY>No</USEFORGRATUITY>");
                TallyData = (TallyData + "<ISTDSPROJECTED>No</ISTDSPROJECTED>");
                TallyData = (TallyData + "<FORSERVICETAX>No</FORSERVICETAX>");
                TallyData = (TallyData + "<ISINPUTCREDIT>No</ISINPUTCREDIT>");
                TallyData = (TallyData + "<ISEXEMPTED>No</ISEXEMPTED>");
                TallyData = (TallyData + "<ISABATEMENTAPPLICABLE>No</ISABATEMENTAPPLICABLE>");
                TallyData = (TallyData + "<ISSTXPARTY>No</ISSTXPARTY>");
                TallyData = (TallyData + "<ISSTXNONREALIZEDTYPE>No</ISSTXNONREALIZEDTYPE>");
                TallyData = (TallyData + "<ISUSEDFORCVD>No</ISUSEDFORCVD>");
                TallyData = (TallyData + "<LEDBELONGSTONONTAXABLE>No</LEDBELONGSTONONTAXABLE>");
                TallyData = (TallyData + "<ISEXCISEMERCHANTEXPORTER>No</ISEXCISEMERCHANTEXPORTER>");
                TallyData = (TallyData + "<ISPARTYEXEMPTED>No</ISPARTYEXEMPTED>");
                TallyData = (TallyData + "<ISSEZPARTY>No</ISSEZPARTY>");
                TallyData = (TallyData + "<TDSDEDUCTEEISSPECIALRATE>No</TDSDEDUCTEEISSPECIALRATE>");
                TallyData = (TallyData + "<ISECHEQUESUPPORTED>No</ISECHEQUESUPPORTED>");
                TallyData = (TallyData + "<ISEDDSUPPORTED>No</ISEDDSUPPORTED>");
                TallyData = (TallyData + "<HASECHEQUEDELIVERYMODE>No</HASECHEQUEDELIVERYMODE>");
                TallyData = (TallyData + "<HASECHEQUEDELIVERYTO>No</HASECHEQUEDELIVERYTO>");
                TallyData = (TallyData + "<HASECHEQUEPRINTLOCATION>No</HASECHEQUEPRINTLOCATION>");
                TallyData = (TallyData + "<HASECHEQUEPAYABLELOCATION>No</HASECHEQUEPAYABLELOCATION>");
                TallyData = (TallyData + "<HASECHEQUEBANKLOCATION>No</HASECHEQUEBANKLOCATION>");
                TallyData = (TallyData + "<HASEDDDELIVERYMODE>No</HASEDDDELIVERYMODE>");
                TallyData = (TallyData + "<HASEDDDELIVERYTO>No</HASEDDDELIVERYTO>");
                TallyData = (TallyData + "<HASEDDPRINTLOCATION>No</HASEDDPRINTLOCATION>");
                TallyData = (TallyData + "<HASEDDPAYABLELOCATION>No</HASEDDPAYABLELOCATION>");
                TallyData = (TallyData + "<HASEDDBANKLOCATION>No</HASEDDBANKLOCATION>");
                TallyData = (TallyData + "<ISEBANKINGENABLED>No</ISEBANKINGENABLED>");
                TallyData = (TallyData + "<ISEXPORTFILEENCRYPTED>No</ISEXPORTFILEENCRYPTED>");
                TallyData = (TallyData + "<ISBATCHENABLED>No</ISBATCHENABLED>");
                TallyData = (TallyData + "<ISPRODUCTCODEBASED>No</ISPRODUCTCODEBASED>");
                TallyData = (TallyData + "<HASEDDCITY>No</HASEDDCITY>");
                TallyData = (TallyData + "<HASECHEQUECITY>No</HASECHEQUECITY>");
                TallyData = (TallyData + "<ISFILENAMEFORMATSUPPORTED>No</ISFILENAMEFORMATSUPPORTED>");
                TallyData = (TallyData + "<HASCLIENTCODE>No</HASCLIENTCODE>");
                TallyData = (TallyData + "<PAYINSISBATCHAPPLICABLE>No</PAYINSISBATCHAPPLICABLE>");
                TallyData = (TallyData + "<PAYINSISFILENUMAPP>No</PAYINSISFILENUMAPP>");
                TallyData = (TallyData + "<ISSALARYTRANSGROUPEDFORBRS>No</ISSALARYTRANSGROUPEDFORBRS>");
                TallyData = (TallyData + "<ISEBANKINGSUPPORTED>No</ISEBANKINGSUPPORTED>");
                TallyData = (TallyData + "<ISSCBUAE>No</ISSCBUAE>");
                TallyData = (TallyData + "<ISBANKSTATUSAPP>No</ISBANKSTATUSAPP>");
                TallyData = (TallyData + "<ISSALARYGROUPED>No</ISSALARYGROUPED>");
                TallyData = (TallyData + "<USEFORPURCHASETAX>No</USEFORPURCHASETAX>");
                TallyData = (TallyData + "<AUDITED>No</AUDITED>");
                TallyData = (TallyData + "<SORTPOSITION>1000</SORTPOSITION>");
                TallyData = (TallyData + "<SERVICETAXDETAILS.LIST></SERVICETAXDETAILS.LIST>");
                TallyData = (TallyData + "<LBTREGNDETAILS.LIST></LBTREGNDETAILS.LIST>");
                TallyData = (TallyData + "<VATDETAILS.LIST></VATDETAILS.LIST>");
                TallyData = (TallyData + "<SALESTAXCESSDETAILS.LIST></SALESTAXCESSDETAILS.LIST>");
                TallyData = (TallyData + "<GSTDETAILS.LIST></GSTDETAILS.LIST>");



                //TallyData = (TallyData + "<SALESTAXNUMBER>" + Convert.ToString(dt.Rows[0]["PANNO"]) + "</SALESTAXNUMBER>");
                //TallyData = (TallyData + "<ALTERID>71386</ALTERID>");
                //TallyData = (TallyData + "<GUID>029a43ec-735b-405d-8c8b-a77aff3152d7-00002715</GUID>");
                //TallyData = (TallyData + "<CREATEDDATE> 20210611 </CREATEDDATE>");
                //TallyData = (TallyData + "<ALTEREDON> 20211214 </ALTEREDON>");
                //TallyData = (TallyData + "<CURRENCYNAME>₹</CURRENCYNAME>");
                //TallyData = (TallyData + "<ALTEREDBY> audit </ALTEREDBY>");
                //TallyData = (TallyData + "<SERVICECATEGORY>Not Applicable</SERVICECATEGORY>");
                //TallyData = (TallyData + "<XBRLDETAIL.LIST></XBRLDETAIL.LIST>");
                //TallyData = (TallyData + "<AUDITDETAILS.LIST></AUDITDETAILS.LIST>");
                //TallyData = (TallyData + "<SCHVIDETAILS.LIST></SCHVIDETAILS.LIST>");
                //TallyData = (TallyData + "<EXCISETARIFFDETAILS.LIST></EXCISETARIFFDETAILS.LIST>");
                //TallyData = (TallyData + "<TCSCATEGORYDETAILS.LIST></TCSCATEGORYDETAILS.LIST>");
                //TallyData = (TallyData + "<TDSCATEGORYDETAILS.LIST></TDSCATEGORYDETAILS.LIST>");
                //TallyData = (TallyData + "<SLABPERIOD.LIST></SLABPERIOD.LIST>");
                //TallyData = (TallyData + "<GRATUITYPERIOD.LIST></GRATUITYPERIOD.LIST>");
                //TallyData = (TallyData + "<ADDITIONALCOMPUTATIONS.LIST></ADDITIONALCOMPUTATIONS.LIST>");
                //TallyData = (TallyData + "<EXCISEJURISDICTIONDETAILS.LIST></EXCISEJURISDICTIONDETAILS.LIST>");
                //TallyData = (TallyData + "<EXCLUDEDTAXATIONS.LIST></EXCLUDEDTAXATIONS.LIST>");
                //TallyData = (TallyData + "<BANKALLOCATIONS.LIST></BANKALLOCATIONS.LIST>");
                //TallyData = (TallyData + "<BANKEXPORTFORMATS.LIST></BANKEXPORTFORMATS.LIST>");
                //TallyData = (TallyData + "<BILLALLOCATIONS.LIST></BILLALLOCATIONS.LIST>");
                //TallyData = (TallyData + "<INTERESTCOLLECTION.LIST></INTERESTCOLLECTION.LIST>");
                //TallyData = (TallyData + "<LEDGERCLOSINGVALUES.LIST></LEDGERCLOSINGVALUES.LIST>");
                //TallyData = (TallyData + "<LEDGERAUDITCLASS.LIST></LEDGERAUDITCLASS.LIST>");
                //TallyData = (TallyData + "<OLDAUDITENTRIES.LIST></OLDAUDITENTRIES.LIST>");
                //TallyData = (TallyData + "<TDSEXEMPTIONRULES.LIST></TDSEXEMPTIONRULES.LIST>");
                //TallyData = (TallyData + "<DEDUCTINSAMEVCHRULES.LIST></DEDUCTINSAMEVCHRULES.LIST>");
                //TallyData = (TallyData + "<LOWERDEDUCTION.LIST></LOWERDEDUCTION.LIST>");
                //TallyData = (TallyData + "<STXABATEMENTDETAILS.LIST></STXABATEMENTDETAILS.LIST>");
                //TallyData = (TallyData + "<LEDMULTIADDRESSLIST.LIST></LEDMULTIADDRESSLIST.LIST>");
                //TallyData = (TallyData + "<STXTAXDETAILS.LIST></STXTAXDETAILS.LIST>");
                //TallyData = (TallyData + "<CHEQUERANGE.LIST></CHEQUERANGE.LIST>");
                //TallyData = (TallyData + "<DEFAULTVCHCHEQUEDETAILS.LIST></DEFAULTVCHCHEQUEDETAILS.LIST>");
                //TallyData = (TallyData + "<ACCOUNTAUDITENTRIES.LIST></ACCOUNTAUDITENTRIES.LIST>");
                //TallyData = (TallyData + "<AUDITENTRIES.LIST></AUDITENTRIES.LIST>");
                //TallyData = (TallyData + "<BRSIMPORTEDINFO.LIST></BRSIMPORTEDINFO.LIST>");
                //TallyData = (TallyData + "<AUTOBRSCONFIGS.LIST></AUTOBRSCONFIGS.LIST>");
                //TallyData = (TallyData + "<BANKURENTRIES.LIST></BANKURENTRIES.LIST>");
                //TallyData = (TallyData + "<DEFAULTCHEQUEDETAILS.LIST></DEFAULTCHEQUEDETAILS.LIST>");
                //TallyData = (TallyData + "<DEFAULTOPENINGCHEQUEDETAILS.LIST></DEFAULTOPENINGCHEQUEDETAILS.LIST>");
                //TallyData = (TallyData + "<CANCELLEDPAYALLOCATIONS.LIST></CANCELLEDPAYALLOCATIONS.LIST>");
                //TallyData = (TallyData + "<ECHEQUEPRINTLOCATION.LIST></ECHEQUEPRINTLOCATION.LIST>");
                //TallyData = (TallyData + "<ECHEQUEPAYABLELOCATION.LIST></ECHEQUEPAYABLELOCATION.LIST>");
                //TallyData = (TallyData + "<EDDPRINTLOCATION.LIST></EDDPRINTLOCATION.LIST>");
                //TallyData = (TallyData + "<EDDPAYABLELOCATION.LIST></EDDPAYABLELOCATION.LIST>");
                //TallyData = (TallyData + "<AVAILABLETRANSACTIONTYPES.LIST></AVAILABLETRANSACTIONTYPES.LIST>");
                //TallyData = (TallyData + "<LEDPAYINSCONFIGS.LIST></LEDPAYINSCONFIGS.LIST>");
                //TallyData = (TallyData + "<TYPECODEDETAILS.LIST></TYPECODEDETAILS.LIST>");
                //TallyData = (TallyData + "<FIELDVALIDATIONDETAILS.LIST></FIELDVALIDATIONDETAILS.LIST>");
                //TallyData = (TallyData + "<INPUTCRALLOCS.LIST></INPUTCRALLOCS.LIST>");
                //TallyData = (TallyData + "<TCSMETHODOFCALCULATION.LIST></TCSMETHODOFCALCULATION.LIST>");
                //TallyData = (TallyData + "<GSTCLASSFNIGSTRATES.LIST></GSTCLASSFNIGSTRATES.LIST>");
                //TallyData = (TallyData + "<EXTARIFFDUTYHEADDETAILS.LIST></EXTARIFFDUTYHEADDETAILS.LIST>");
                //TallyData = (TallyData + "<VOUCHERTYPEPRODUCTCODES.LIST></VOUCHERTYPEPRODUCTCODES.LIST>");
                TallyData = (TallyData + "</LEDGER>");
                TallyData = (TallyData + "</TALLYMESSAGE>");
                TallyData = (TallyData + "</REQUESTDATA>");
                TallyData = (TallyData + "</IMPORTDATA>");
                TallyData = (TallyData + "</BODY>");
                TallyData = (TallyData + "</ENVELOPE>");
            }
            else
            {
                TallyData = "Error : Vendor Not Generated in Tally..!";
            }

            return TallyData;
        }

        protected void txtACNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    string result = CheckAccountNumber(txtACNo.Text);
                    if (result == "true")
                    {
                        lblAccntNoExist.Visible = true;
                    }
                    else
                    {
                        lblAccntNoExist.Visible = false;
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

        public string CheckAccountNumber(string ACCOUNTNO)
        {
            string iResult = "false";
            try
            {
                MainClass objMain = new MainClass();
                var detail = objMain.GetAccountNumber(ACCOUNTNO);
                if (detail != null && detail.Rows != null && detail.Rows.Count > 0)
                {
                    iResult = "true";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iResult;
        }
    }
}