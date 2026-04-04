using Newtonsoft.Json;
using RestSharp;
using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.SD
{
    public partial class frmOpenSO : System.Web.UI.Page
    {

        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
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
                        //objBindDDL.FillPlantIsMobexRadio(rblPlantList, 1);
                        //rblPlantList.SelectedIndex = 0;
                        txtBillDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");
                        BindGrid();

                        if (chkINVPO.Checked == true)
                        {
                            rfvBillNo.Enabled = false;
                        }
                        else
                        {
                            rfvBillNo.Enabled = true;
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

        public void BindGrid()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetOpenSOData(objMainClass.intCmpId, 57, "PSO", Convert.ToString(Session["PLANTCD"]).Length > 4 ? "0" : Convert.ToString(Session["PLANTCD"]), "OPENSOFORFRANCHISEE", 0, "");

                    if (dt.Rows.Count > 0)
                    {
                        gvAllList.DataSource = dt;
                        gvAllList.DataBind();
                        gvAllList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    else
                    {
                        gvAllList.DataSource = string.Empty;
                        gvAllList.DataBind();
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

        protected void lblCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    cltControl();

                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    string lblItemCode = ((Label)grdrow.FindControl("lblItemCode")).Text;
                    string lblItemDesc = ((Label)grdrow.FindControl("lblItemDesc")).Text;
                    string lblMake = ((Label)grdrow.FindControl("lblMake")).Text;
                    string lblModel = ((Label)grdrow.FindControl("lblModel")).Text;
                    string lblSOQty = ((Label)grdrow.FindControl("lblSOQty")).Text;

                    string lblPlantCode = ((Label)grdrow.FindControl("lblPlantCode")).Text;
                    string lblLocCode = ((Label)grdrow.FindControl("lblLocCode")).Text;

                    string lblSONO = ((Label)grdrow.FindControl("lblSONO")).Text;
                    string lblSALESCHANNEL = ((Label)grdrow.FindControl("lblSALESCHANNEL")).Text;
                    string lblCAmount = ((Label)grdrow.FindControl("lblCAmount")).Text;
                    string lblLOCKAMT = ((Label)grdrow.FindControl("lblLOCKAMT")).Text;

                    hfPrice.Value = lblCAmount;
                    hfSalesFrom.Value = lblSALESCHANNEL;
                    hfSONO.Value = lblSONO;
                    hfPlantCd.Value = lblPlantCode;
                    hfLocCd.Value = lblLocCode;
                    hfSOQty.Value = lblSOQty;
                    hfItemCode.Value = lblItemCode;
                    hfItemDesc.Value = lblItemDesc;
                    hfMake.Value = (lblMake).ToUpper();
                    hfModel.Value = (lblModel).ToUpper();
                    hfLockAmt.Value = lblLOCKAMT;

                    if (lblItemCode.Contains("MDUD"))
                    {
                        string[] para = { "GB" };
                        string[] spec1 = lblItemDesc.Split(para, 0);
                        string color = "";
                        string ram = "";
                        string rom = "";

                        string[] para2 = { "MOBILE" };
                        if (hfMake.Value == "APPLE")
                        {
                            string[] spec2 = spec1[1].Split(para2, 0);
                            color = spec2[0].Trim();
                            string[] spec3 = spec1[0].Split(' ');
                            ram = spec3[spec3.Count() - 2].Trim();
                            rom = spec3[spec3.Count() - 1].Trim();
                        }
                        else
                        {
                            string[] spec2 = spec1[2].Split(para2, 0);
                            color = spec2[0].Trim();
                            string[] spec3 = spec1[0].Split(' ');
                            ram = spec3[spec3.Count() - 1].Trim();
                            rom = spec1[1].Trim();
                        }



                        hfColor.Value = color;
                        hfProdItemID.Value = "1";
                        hfProdItemDesc.Value = "LAMD000001 - MOBILE DEVICE";
                        hfRAM.Value = ram;
                        hfROM.Value = rom;
                    }

                    if (lblItemCode.Contains("LTLT"))
                    {
                        string[] spec1 = lblItemDesc.Split('-');
                        string color = spec1[spec1.Count() - 1].Trim();

                        string ram = spec1[2].Replace(" GB ", "").Trim();
                        string rom = spec1[3].Replace(" GB SSD ", "").Replace(" GB HDD ", "").Replace(" TB SSD ", "").Replace(" TB HDD ", "").Trim();

                        if (color.Any(char.IsDigit))
                        {
                            hfColor.Value = "BLACK";
                        }
                        else
                        {
                            if (color.Length >= 5)
                            {
                                hfColor.Value = color;
                            }
                            else if (color.Contains("SIL"))
                            {
                                hfColor.Value = "SILVER";
                            }
                            else
                            {
                                hfColor.Value = "BLACK";
                            }
                        }

                        hfProdItemID.Value = "78754";
                        hfProdItemDesc.Value = "LALT000001 - LAPTOP";
                        hfRAM.Value = ram;
                        hfROM.Value = rom;
                    }

                    objBindDDL.FillPayTerm(ddlPaymentTerms);
                    ddlPaymentTerms.SelectedIndex = 1;
                    txtPaymentTermsDesc.Text = Convert.ToString(ddlPaymentTerms.SelectedItem.Text.Split('-')[1].Trim());
                    txtPaymentTermsDesc.Enabled = false;
                    objBindDDL.FillLists(ddlPurType, "PUR");
                    ddlPurType.SelectedValue = ddlPurType.Items.FindByText("FRANCHISE").Value;
                    objBindDDL.FillPurChrgType(ddlCharges, objMainClass.intCmpId);



                    if (hfProdItemID.Value == "1")
                    {
                        DataTable dtItemDesc = new DataTable();
                        dtItemDesc = objMainClass.GetItemDetailsByItemCode(objMainClass.intCmpId, hfItemCode.Value, 1, "ITEMMASTERSEARCH");

                        int newmax = 0;
                        int newmin = 0;

                        decimal maxstk = objMainClass.SP_GET_STOCK_FUNCTION(objMainClass.intCmpId, objMainClass.getFinYear(objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true)), lblItemCode, objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true), lblPlantCode, lblLocCode);

                        DataTable dt = new DataTable();
                        dt = objMainClass.GetMaxMinStk(objMainClass.intCmpId, Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]), lblPlantCode, lblLocCode, 1, "GETMAXMINSTK");
                        if (dt.Rows.Count > 0)
                        {
                            newmax = Convert.ToInt32(dt.Rows[0]["MAXSTKQTY"]);
                            newmin = Convert.ToInt32(dt.Rows[0]["MINSTKQTY"]);
                        }

                        if (maxstk >= newmax)
                        {
                            lblQtyError.Text = "Current stock is at its max, you cannot place order more than that! Maximum stock : " + newmax + ". Current Stock : " + maxstk + ". Do you want to continue to create Job Sheet Only.?";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Qty').modal();", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
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

        protected void btnCreateDoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    DataTable dt = new DataTable();
                    dt = objMainClass.GetJobStatusByIMEI(objMainClass.intCmpId, "0", txtIMEI.Text, (int)STATUS.Canceled);

                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dt.Rows[0]["jobstatus"]) != (int)STATUS.Closed)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"Last Job ID still Open. Job ID : " + Convert.ToString(dt.Rows[0]["jobid"]) + "\");", true);
                        }
                        else
                        {
                            hfOldJobID.Value = Convert.ToString(dt.Rows[0]["jobid"]);
                            CreateJob();
                        }
                    }
                    else
                    {
                        CreateJob();
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

        public void CreateJob()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dtAPI = new DataTable();
                    dtAPI = objMainClass.GetWAData("CREATEJS", 1, "GETWADATA");

                    if (dtAPI.Rows.Count > 0)
                    {


                        #region JobSheet Details...

                        List<JobSheetDetail> objJobSheetDetail = new List<JobSheetDetail>();
                        objJobSheetDetail.Add(new JobSheetDetail
                        {
                            CMPID = objMainClass.intCmpId,
                            SRNO = 1,
                            ITEMID = Convert.ToInt32(hfProdItemID.Value),
                            ITEMDESC = hfProdItemDesc.Value,
                            QTY = Convert.ToDecimal(hfSOQty.Value),
                            UOM = 1,
                            RATE = Convert.ToDecimal(250),
                            ITEMVALUE = Convert.ToDecimal(250),
                            PLANTCD = hfPlantCd.Value,
                            LOCCD = hfLocCd.Value,
                            EXTWARNO = "NA",
                            PRODMAKE = hfMake.Value,
                            PRODMODEL = hfModel.Value,
                            IMEINO = txtIMEI.Text,
                            JOBTYPE = "OTHER : ONLY SERVICE",
                            JOBDESC = "FOR CHECK",
                            REFINVNO = "-",
                            REFINVDT = Convert.ToDateTime(DateTime.Now).AddYears(-2).ToString(),
                            REFINVAMT = Convert.ToDecimal(10000),
                            INSUCO = "0000040003",
                            NOTE = "",
                            PRODCOND = "",
                            WAYBILLNO = "",
                            REVDCNO = "",
                            FWAYBILLNO = "",
                            DCNO = "",
                            BATTERYNO = hfMake.Value,
                            REVTRANNAME = "",
                            FTRANNAME = "",
                            WAYBILLSTATUS = "",
                            FWAYBILLSTATUS = "",
                            DELICONFDT = "",
                            LOCKCODE = "",
                            REVPICKUPDT = "",
                            BACKCOVERFLAG = "Y",
                            REVDELIDT = "",
                            FPICKUPDT = "",
                            PHYIMEINO = txtIMEI.Text,
                            FESTIDELDT = "",
                            REVESTIDELDT = "",
                            IMEINO2 = txtIMEI2.Text,
                            CODWAYBILLNO = "",
                            FEDEXPICKUP = 0,
                            PRODCOLOR = hfColor.Value,
                            DELIVERYTO = "",
                            ID = 0,
                            JOBID = ""
                        });

                        #endregion

                        #region Addess Details...

                        AddressDetail objAddressDetail = new AddressDetail();
                        objAddressDetail.CMPID = objMainClass.intCmpId;
                        objAddressDetail.REFID = "";
                        objAddressDetail.REFTYPE = "JS";
                        objAddressDetail.ADDOF = "G";
                        objAddressDetail.ADDR1 = "MOBEX REFURB";
                        objAddressDetail.ADDR2 = "MOBEX REFURB";
                        objAddressDetail.ADDR3 = "";
                        objAddressDetail.CITY = "AHMEDABAD";
                        objAddressDetail.STCD = 1;
                        objAddressDetail.CNCD = "IN";
                        objAddressDetail.POSTALCODE = "380023";
                        objAddressDetail.CONTACTPERSON = "";
                        objAddressDetail.CONTACTNO = "";
                        objAddressDetail.CONTACTPERSON2 = "";
                        objAddressDetail.CONTACTNO2 = "";
                        objAddressDetail.CONTACTPERSON3 = "";
                        objAddressDetail.CONTACTNO3 = "";
                        objAddressDetail.MOBILENO = "1234567890";
                        objAddressDetail.MOBILENO2 = "";
                        objAddressDetail.MOBILENO3 = "";
                        objAddressDetail.FAXNO = "";
                        objAddressDetail.EMAILID = "";
                        objAddressDetail.WEBSITE = "";
                        objAddressDetail.LAT = "";
                        objAddressDetail.LONG = "";
                        #endregion

                        #region JobSheet Master...

                        JobSheetMaster objJobSheetMaster = new JobSheetMaster();
                        objJobSheetMaster.CMPID = objMainClass.intCmpId;
                        //objJobSheetMaster.JOBID = "";
                        objJobSheetMaster.JOBDT = Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        objJobSheetMaster.JSTYPE = 17;
                        objJobSheetMaster.SHIPTOPARTY = "0000000001";
                        objJobSheetMaster.BILLTOPARTY = "0000010003";
                        objJobSheetMaster.ENDCUST = "MOBEX REFURB";
                        //objJobSheetMaster.CUSTPONO = "";
                        //objJobSheetMaster.CUSTPODT = "";
                        //objJobSheetMaster.ADDID = "";
                        objJobSheetMaster.REMARK = "";
                        objJobSheetMaster.JOBSTATUS = Convert.ToInt32(JOBSTATUS.Saved);
                        objJobSheetMaster.STATRES = "";
                        objJobSheetMaster.STATUPDDT = Convert.ToDateTime(DateTime.Now).ToString();
                        objJobSheetMaster.STATUPDBY = Convert.ToInt32(Session["USERID"]);
                        objJobSheetMaster.SEGMENT = "1015";
                        objJobSheetMaster.DISTCHNL = "50";
                        objJobSheetMaster.ISRETURN = hfOldJobID.Value == "" || hfOldJobID.Value == null || hfOldJobID.Value == string.Empty ? "N" : "Y";
                        objJobSheetMaster.REFJOBID = hfOldJobID.Value == "" || hfOldJobID.Value == null || hfOldJobID.Value == string.Empty ? "" : objMainClass.strConvertZeroPadding(hfOldJobID.Value);
                        objJobSheetMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                        objJobSheetMaster.CREATEDATE = Convert.ToDateTime(DateTime.Now).ToString();
                        //objJobSheetMaster.UPDATEBY = "";
                        //objJobSheetMaster.UPDATEDATE = "";
                        //objJobSheetMaster.STAGEID = "";
                        //objJobSheetMaster.ONHOLD = "";
                        //objJobSheetMaster.ONHOLDREASON = "";
                        //objJobSheetMaster.ONHOLDDT = "";
                        //objJobSheetMaster.HOLDRELDT = "";
                        objJobSheetMaster.JDAREF = txtVendorCode.Text;//txtReveCourier.Text;//hfSONO.Value;
                        objJobSheetMaster.JDAREFDT = Convert.ToDateTime(DateTime.Now).ToString();
                        objJobSheetMaster.PICKUPFROM = "0000000001";
                        objJobSheetMaster.SHIPTO = "0000000001";
                        objJobSheetMaster.APRVFLAG = "Y";
                        //objJobSheetMaster.PICKUPADDID = "";
                        //objJobSheetMaster.DROPADDID = "";
                        //objJobSheetMaster.AGENTCD = "";
                        objJobSheetMaster.INQNO = 0;
                        objJobSheetMaster.OOW = 0;
                        objJobSheetMaster.JWREFNO = txtReveCourier.Text;//hfSONO.Value;
                        //objJobSheetMaster.JWREFNO2 = "";
                        //objJobSheetMaster.JWREFDT = "";
                        //objJobSheetMaster.JWREFNO3 = "";
                        //objJobSheetMaster.JWREFDT2 = "";
                        //objJobSheetMaster.JWREFDT3 = "";
                        //objJobSheetMaster.JWREFNO4 = "";
                        //objJobSheetMaster.JWREFDT4 = "";
                        objJobSheetMaster.PONO = "";
                        objJobSheetMaster.SLRNO = "";
                        objJobSheetMaster.LISTINGID = 0;
                        objJobSheetMaster.ITEMCODE = hfItemCode.Value;

                        objJobSheetMaster.AddressDetail = objAddressDetail;
                        objJobSheetMaster.lstJobDetail = objJobSheetDetail;
                        #endregion

                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };

                        string URL = Convert.ToString(dtAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtAPI.Rows[0]["TOKEN"]);

                        var client = new RestClient(URL);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("" + Convert.ToString(dtAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtAPI.Rows[0]["KEYVALUE"]) + "");
                        request.AddHeader("Content-Type", "application/json");
                        var jsonInput = JsonConvert.SerializeObject(objJobSheetMaster);
                        request.AddParameter("application/json", jsonInput, ParameterType.RequestBody);
                        IRestResponse response = client.Execute(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            JobSheetResponse objJobSheetResponse = new JobSheetResponse();
                            string jsonconn = response.Content;
                            objJobSheetResponse = JsonConvert.DeserializeObject<JobSheetResponse>(jsonconn);

                            string NEWJOBID = Convert.ToString(objJobSheetResponse.JOBNO);
                            hfNewJobID.Value = NEWJOBID;
                            string NEWESTINO = "";
                            string NEWJCNO = "";
                            dtAPI = null;
                            dtAPI = objMainClass.GetWAData("UPDATEREVWAYBILL", 1, "GETWADATA");

                            if (dtAPI.Rows.Count > 0)
                            {
                                ReverseWaybillUpdate objReverseWaybillUpdate = new ReverseWaybillUpdate();
                                objReverseWaybillUpdate.CMPID = objMainClass.intCmpId;
                                objReverseWaybillUpdate.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                objReverseWaybillUpdate.DOCTYPE = "JS";
                                objReverseWaybillUpdate.JOBID = NEWJOBID;
                                objReverseWaybillUpdate.REVTRANNAME = txtReveCourier.Text;
                                objReverseWaybillUpdate.JOBSTATUS = (int)STATUS.RevWayBillGen;
                                objReverseWaybillUpdate.STAGEID = (int)STAGE.RevWayBillNo;
                                objReverseWaybillUpdate.STATRES = "AUTO ENTRY AGAINST SO";
                                objReverseWaybillUpdate.WAYBILLNO = txtRevWaybill.Text;
                                objReverseWaybillUpdate.WAYBILLSTATUS = "";

                                string URLRevWaybill = Convert.ToString(dtAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtAPI.Rows[0]["TOKEN"]);

                                var clientRevWaybill = new RestClient(URLRevWaybill);
                                clientRevWaybill.Timeout = -1;
                                var requestRevWabill = new RestRequest(Method.POST);
                                requestRevWabill.AddHeader("" + Convert.ToString(dtAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtAPI.Rows[0]["KEYVALUE"]) + "");
                                requestRevWabill.AddHeader("Content-Type", "application/json");
                                var jsonInputRevWaybill = JsonConvert.SerializeObject(objReverseWaybillUpdate);
                                requestRevWabill.AddParameter("application/json", jsonInputRevWaybill, ParameterType.RequestBody);
                                IRestResponse responserevwaybill = clientRevWaybill.Execute(requestRevWabill);
                                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                                {

                                    DataTable dtStageSeq = new DataTable();
                                    dtStageSeq = objMainClass.GetSegmentStageData(11, "1015", "GETSTAGEREQ");

                                    if (dtStageSeq.Rows.Count > 0)
                                    {
                                        DataTable dtInsertStage = new DataTable();
                                        dtInsertStage = objMainClass.GetWAData("INSERTSTAGE", 1, "GETWADATA");

                                        DataTable dtJobStatus = new DataTable();
                                        dtJobStatus = objMainClass.GetWAData("UPDATESTATUS", 1, "GETWADATA");

                                        if (dtInsertStage.Rows.Count > 0)
                                        {
                                            if (dtJobStatus.Rows.Count > 0)
                                            {
                                                for (int s = 0; s < dtStageSeq.Rows.Count; s++)
                                                {
                                                    if (Convert.ToInt32(dtStageSeq.Rows[s]["STAGESEQ"]) < 27)
                                                    {

                                                        int JOBSTAGEID = Convert.ToInt32(dtStageSeq.Rows[s]["STAGEID"]);
                                                        int JOBSTATUSID = objMainClass.GetStatusByStageID(JOBSTAGEID);


                                                        string URLStage = Convert.ToString(dtInsertStage.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertStage.Rows[0]["TOKEN"]);
                                                        URLStage = URLStage + "?DOCNO=" + NEWJOBID + "&DOCTYPE=JS&STAGEID=" + JOBSTAGEID + "&STATRES=AUTO ENTRY AGAINST SO&CREATBY=" + Convert.ToInt32(Session["USERID"]);
                                                        var clientStage = new RestClient(URLStage);
                                                        clientStage.Timeout = -1;
                                                        var requestStage = new RestRequest(Method.POST);
                                                        requestStage.AddHeader("" + Convert.ToString(dtInsertStage.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertStage.Rows[0]["KEYVALUE"]) + "");
                                                        requestStage.AddHeader("Content-Type", "application/json");
                                                        //var jsonInputStage = JsonConvert.SerializeObject(objReverseWaybillUpdate);
                                                        //requestStage.AddParameter("application/json", jsonInputStage, ParameterType.RequestBody);
                                                        IRestResponse responseStage = clientStage.Execute(requestStage);


                                                        string URLStatus = Convert.ToString(dtJobStatus.Rows[0]["OTHER"]) + "" + Convert.ToString(dtJobStatus.Rows[0]["TOKEN"]);
                                                        URLStatus = URLStatus + "?CMPID=" + objMainClass.intCmpId + "&JOBID=" + NEWJOBID + "&STAGEID=" + JOBSTAGEID + "&JOBSTATUS=" + JOBSTATUSID + "&STATRES=AUTO ENTRY AGAINST SO&STATUPDATEDT=" + DateTime.Now.ToString() + "&UPDATEDATE=" + DateTime.Now.ToString() + "&CREATEBY=" + Convert.ToInt32(Session["USERID"]);
                                                        var clientStatus = new RestClient(URLStatus);
                                                        clientStatus.Timeout = -1;
                                                        var requestStatus = new RestRequest(Method.POST);
                                                        requestStatus.AddHeader("" + Convert.ToString(dtJobStatus.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtJobStatus.Rows[0]["KEYVALUE"]) + "");
                                                        requestStatus.AddHeader("Content-Type", "application/json");
                                                        //var jsonInputStatus = JsonConvert.SerializeObject(objReverseWaybillUpdate);
                                                        //requestStatus.AddParameter("application/json", jsonInputStatus, ParameterType.RequestBody);
                                                        IRestResponse responseStatus = clientStatus.Execute(requestStatus);

                                                        if (JOBSTAGEID == 11)
                                                        {
                                                            DataTable dtInsertJC = new DataTable();
                                                            dtInsertJC = objMainClass.GetWAData("CREATEJC", 1, "GETWADATA");
                                                            if (dtInsertJC.Rows.Count > 0)
                                                            {
                                                                JobCardMaster objJobCardMaster = new JobCardMaster();
                                                                objJobCardMaster.BACKCOVERFLAG = "Y";
                                                                objJobCardMaster.CMPID = objMainClass.intCmpId;
                                                                objJobCardMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                objJobCardMaster.CREATEDATE = Convert.ToDateTime(DateTime.Now).ToString();
                                                                objJobCardMaster.ITEMID = Convert.ToInt32(hfProdItemID.Value);
                                                                objJobCardMaster.JCDT = Convert.ToDateTime(DateTime.Now).ToString();
                                                                objJobCardMaster.JCNO = "";
                                                                objJobCardMaster.JOBID = NEWJOBID;
                                                                objJobCardMaster.JOBIDSRNO = 1;
                                                                objJobCardMaster.JOBSTATUS = (int)STATUS.JCSaved;
                                                                objJobCardMaster.LOCCD = hfLocCd.Value;
                                                                objJobCardMaster.PLANTCD = hfPlantCd.Value;
                                                                objJobCardMaster.QTY = 1;
                                                                objJobCardMaster.STAGEID = 0;// JOBSTAGEID;
                                                                objJobCardMaster.UOM = 1;
                                                                objJobCardMaster.WRKCNT = "WR01";

                                                                string URLJC = Convert.ToString(dtInsertJC.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertJC.Rows[0]["TOKEN"]);
                                                                var clientJC = new RestClient(URLJC);
                                                                clientJC.Timeout = -1;
                                                                var requestJC = new RestRequest(Method.POST);
                                                                requestJC.AddHeader("" + Convert.ToString(dtInsertJC.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertJC.Rows[0]["KEYVALUE"]) + "");
                                                                requestJC.AddHeader("Content-Type", "application/json");
                                                                var jsonInputJC = JsonConvert.SerializeObject(objJobCardMaster);
                                                                requestJC.AddParameter("application/json", jsonInputJC, ParameterType.RequestBody);
                                                                IRestResponse responseJC = clientJC.Execute(requestJC);

                                                                JobCardResponse objJobCardResponse = new JobCardResponse();
                                                                string jsonconnJC = responseJC.Content;
                                                                objJobCardResponse = JsonConvert.DeserializeObject<JobCardResponse>(jsonconnJC);

                                                                NEWJCNO = objJobCardResponse.JCNO;

                                                                if (NEWJCNO != null && NEWJCNO != "" && NEWJCNO != string.Empty)
                                                                {
                                                                    hfJCNO.Value = NEWJCNO;
                                                                    DataTable dtInsertJCDetails = new DataTable();
                                                                    dtInsertJCDetails = objMainClass.GetWAData("INSERTJCDETAILS", 1, "GETWADATA");
                                                                    if (dtInsertJCDetails.Rows.Count > 0)
                                                                    {
                                                                        #region 50 Inward Inspection Entry...

                                                                        JobCardDetails objJobCardDetails = new JobCardDetails();
                                                                        objJobCardDetails.ASCPARTCODE = "";
                                                                        objJobCardDetails.CMPID = objMainClass.intCmpId;
                                                                        objJobCardDetails.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                        objJobCardDetails.CREATEDATE = Convert.ToDateTime(DateTime.Now).ToString();
                                                                        objJobCardDetails.JCNO = NEWJCNO;
                                                                        objJobCardDetails.JOBDONE = "NA";
                                                                        objJobCardDetails.JOBDONEBY = Convert.ToInt32(Session["USERID"]);
                                                                        objJobCardDetails.JOBPROBID = 146;
                                                                        objJobCardDetails.JOBPROBID1 = 0;
                                                                        objJobCardDetails.JOBPROBID2 = 0;
                                                                        objJobCardDetails.JOBPROBID3 = 0;
                                                                        objJobCardDetails.NEWIMEINO = "";
                                                                        objJobCardDetails.NEXTSTAGEREQ = 14;
                                                                        objJobCardDetails.NOTE = "OK FOR CHECK";
                                                                        objJobCardDetails.PARTREPLACED = "";
                                                                        objJobCardDetails.PARTREQ = "";
                                                                        objJobCardDetails.PARTREQID = 0;
                                                                        objJobCardDetails.PROBLEM = "OK FOR CHECK";
                                                                        objJobCardDetails.PROBLEM1 = "";
                                                                        objJobCardDetails.PROBLEM2 = "";
                                                                        objJobCardDetails.PROBLEM3 = "";
                                                                        objJobCardDetails.RESULT = 25;
                                                                        objJobCardDetails.STAGEID = 50;
                                                                        objJobCardDetails.STARTDT = DateTime.Now;
                                                                        objJobCardDetails.ENDDT = DateTime.Now;
                                                                        objJobCardDetails.JOBID = NEWJOBID;

                                                                        string URLJCDetails = Convert.ToString(dtInsertJCDetails.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertJCDetails.Rows[0]["TOKEN"]);
                                                                        var clientJCDetails = new RestClient(URLJCDetails);
                                                                        clientJCDetails.Timeout = -1;
                                                                        var requestJCDetails = new RestRequest(Method.POST);
                                                                        requestJCDetails.AddHeader("" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYVALUE"]) + "");
                                                                        requestJCDetails.AddHeader("Content-Type", "application/json");
                                                                        var jsonInputJCDetails = JsonConvert.SerializeObject(objJobCardDetails);
                                                                        requestJCDetails.AddParameter("application/json", jsonInputJCDetails, ParameterType.RequestBody);
                                                                        IRestResponse responseES = clientJCDetails.Execute(requestJCDetails);

                                                                        #endregion

                                                                        #region 14 ELS Entry...

                                                                        JobCardDetails objJobCardDetails1 = new JobCardDetails();
                                                                        objJobCardDetails1.ASCPARTCODE = "";
                                                                        objJobCardDetails1.CMPID = objMainClass.intCmpId;
                                                                        objJobCardDetails1.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                        objJobCardDetails1.CREATEDATE = Convert.ToDateTime(DateTime.Now).ToString();
                                                                        objJobCardDetails1.JCNO = NEWJCNO;
                                                                        objJobCardDetails1.JOBDONE = "NA";
                                                                        objJobCardDetails1.JOBDONEBY = Convert.ToInt32(Session["USERID"]);
                                                                        objJobCardDetails1.JOBPROBID = 146;
                                                                        objJobCardDetails1.JOBPROBID1 = 0;
                                                                        objJobCardDetails1.JOBPROBID2 = 0;
                                                                        objJobCardDetails1.JOBPROBID3 = 0;
                                                                        objJobCardDetails1.NEWIMEINO = "";
                                                                        objJobCardDetails1.NEXTSTAGEREQ = 20;
                                                                        objJobCardDetails1.NOTE = "OK FOR CHECK";
                                                                        objJobCardDetails1.PARTREPLACED = "";
                                                                        objJobCardDetails1.PARTREQ = "";
                                                                        objJobCardDetails1.PARTREQID = 0;
                                                                        objJobCardDetails1.PROBLEM = "OK FOR CHECK";
                                                                        objJobCardDetails1.PROBLEM1 = "";
                                                                        objJobCardDetails1.PROBLEM2 = "";
                                                                        objJobCardDetails1.PROBLEM3 = "";
                                                                        objJobCardDetails1.RESULT = 25;
                                                                        objJobCardDetails1.STAGEID = 14;
                                                                        objJobCardDetails1.STARTDT = DateTime.Now;
                                                                        objJobCardDetails1.ENDDT = DateTime.Now;
                                                                        objJobCardDetails1.JOBID = NEWJOBID;

                                                                        string URLJCDetails1 = Convert.ToString(dtInsertJCDetails.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertJCDetails.Rows[0]["TOKEN"]);
                                                                        var clientJCDetails1 = new RestClient(URLJCDetails1);
                                                                        clientJCDetails1.Timeout = -1;
                                                                        var requestJCDetails1 = new RestRequest(Method.POST);
                                                                        requestJCDetails1.AddHeader("" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYVALUE"]) + "");
                                                                        requestJCDetails1.AddHeader("Content-Type", "application/json");
                                                                        var jsonInputJCDetails1 = JsonConvert.SerializeObject(objJobCardDetails1);
                                                                        requestJCDetails1.AddParameter("application/json", jsonInputJCDetails1, ParameterType.RequestBody);
                                                                        IRestResponse responseES1 = clientJCDetails1.Execute(requestJCDetails1);

                                                                        #endregion

                                                                        #region 20 QC1 Entry...

                                                                        JobCardDetails objJobCardDetails2 = new JobCardDetails();
                                                                        objJobCardDetails2.ASCPARTCODE = "";
                                                                        objJobCardDetails2.CMPID = objMainClass.intCmpId;
                                                                        objJobCardDetails2.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                        objJobCardDetails2.CREATEDATE = Convert.ToDateTime(DateTime.Now).ToString();
                                                                        objJobCardDetails2.JCNO = NEWJCNO;
                                                                        objJobCardDetails2.JOBDONE = "NA";
                                                                        objJobCardDetails2.JOBDONEBY = Convert.ToInt32(Session["USERID"]);
                                                                        objJobCardDetails2.JOBPROBID = 146;
                                                                        objJobCardDetails2.JOBPROBID1 = 0;
                                                                        objJobCardDetails2.JOBPROBID2 = 0;
                                                                        objJobCardDetails2.JOBPROBID3 = 0;
                                                                        objJobCardDetails2.NEWIMEINO = "";
                                                                        objJobCardDetails2.NEXTSTAGEREQ = 64;
                                                                        objJobCardDetails2.NOTE = "OK FOR CHECK";
                                                                        objJobCardDetails2.PARTREPLACED = "";
                                                                        objJobCardDetails2.PARTREQ = "";
                                                                        objJobCardDetails2.PARTREQID = 0;
                                                                        objJobCardDetails2.PROBLEM = "OK FOR CHECK";
                                                                        objJobCardDetails2.PROBLEM1 = "";
                                                                        objJobCardDetails2.PROBLEM2 = "";
                                                                        objJobCardDetails2.PROBLEM3 = "";
                                                                        objJobCardDetails2.RESULT = 25;
                                                                        objJobCardDetails2.STAGEID = 20;
                                                                        objJobCardDetails2.STARTDT = DateTime.Now;
                                                                        objJobCardDetails2.ENDDT = DateTime.Now;
                                                                        objJobCardDetails2.JOBID = NEWJOBID;

                                                                        string URLJCDetails2 = Convert.ToString(dtInsertJCDetails.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertJCDetails.Rows[0]["TOKEN"]);
                                                                        var clientJCDetails2 = new RestClient(URLJCDetails2);
                                                                        clientJCDetails2.Timeout = -1;
                                                                        var requestJCDetails2 = new RestRequest(Method.POST);
                                                                        requestJCDetails2.AddHeader("" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYVALUE"]) + "");
                                                                        requestJCDetails2.AddHeader("Content-Type", "application/json");
                                                                        var jsonInputJCDetails2 = JsonConvert.SerializeObject(objJobCardDetails2);
                                                                        requestJCDetails2.AddParameter("application/json", jsonInputJCDetails2, ParameterType.RequestBody);
                                                                        IRestResponse responseES2 = clientJCDetails2.Execute(requestJCDetails2);

                                                                        #endregion

                                                                        #region 64 PDI Entry...

                                                                        JobCardDetails objJobCardDetails3 = new JobCardDetails();
                                                                        objJobCardDetails3.ASCPARTCODE = "";
                                                                        objJobCardDetails3.CMPID = objMainClass.intCmpId;
                                                                        objJobCardDetails3.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                        objJobCardDetails3.CREATEDATE = Convert.ToDateTime(DateTime.Now).ToString();
                                                                        objJobCardDetails3.JCNO = NEWJCNO;
                                                                        objJobCardDetails3.JOBDONE = "NA";
                                                                        objJobCardDetails3.JOBDONEBY = Convert.ToInt32(Session["USERID"]);
                                                                        objJobCardDetails3.JOBPROBID = 146;
                                                                        objJobCardDetails3.JOBPROBID1 = 0;
                                                                        objJobCardDetails3.JOBPROBID2 = 0;
                                                                        objJobCardDetails3.JOBPROBID3 = 0;
                                                                        objJobCardDetails3.NEWIMEINO = "";
                                                                        objJobCardDetails3.NEXTSTAGEREQ = 59;
                                                                        if (hfSalesFrom.Value == "AMAZON")
                                                                        {
                                                                            objJobCardDetails3.NOTE = "AMAZON";
                                                                        }
                                                                        else
                                                                        {
                                                                            objJobCardDetails3.NOTE = "OK FOR CHECK";
                                                                        }

                                                                        objJobCardDetails3.PARTREPLACED = "";
                                                                        objJobCardDetails3.PARTREQ = "";
                                                                        objJobCardDetails3.PARTREQID = 0;
                                                                        objJobCardDetails3.PROBLEM = "OK FOR CHECK";
                                                                        objJobCardDetails3.PROBLEM1 = "";
                                                                        objJobCardDetails3.PROBLEM2 = "";
                                                                        objJobCardDetails3.PROBLEM3 = "";
                                                                        objJobCardDetails3.RESULT = 25;
                                                                        objJobCardDetails3.STAGEID = 64;
                                                                        objJobCardDetails3.STARTDT = DateTime.Now;
                                                                        objJobCardDetails3.ENDDT = DateTime.Now;
                                                                        objJobCardDetails3.JOBID = NEWJOBID;

                                                                        string URLJCDetails3 = Convert.ToString(dtInsertJCDetails.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertJCDetails.Rows[0]["TOKEN"]);
                                                                        var clientJCDetails3 = new RestClient(URLJCDetails3);
                                                                        clientJCDetails3.Timeout = -1;
                                                                        var requestJCDetails3 = new RestRequest(Method.POST);
                                                                        requestJCDetails3.AddHeader("" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYVALUE"]) + "");
                                                                        requestJCDetails3.AddHeader("Content-Type", "application/json");
                                                                        var jsonInputJCDetails3 = JsonConvert.SerializeObject(objJobCardDetails3);
                                                                        requestJCDetails3.AddParameter("application/json", jsonInputJCDetails3, ParameterType.RequestBody);
                                                                        IRestResponse responseES3 = clientJCDetails3.Execute(requestJCDetails3);

                                                                        #endregion

                                                                        #region 59 Packed Entry...

                                                                        JobCardDetails objJobCardDetails4 = new JobCardDetails();
                                                                        objJobCardDetails4.ASCPARTCODE = "";
                                                                        objJobCardDetails4.CMPID = objMainClass.intCmpId;
                                                                        objJobCardDetails4.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                        objJobCardDetails4.CREATEDATE = Convert.ToDateTime(DateTime.Now).ToString();
                                                                        objJobCardDetails4.JCNO = NEWJCNO;
                                                                        objJobCardDetails4.JOBDONE = "NA";
                                                                        objJobCardDetails4.JOBDONEBY = Convert.ToInt32(Session["USERID"]);
                                                                        objJobCardDetails4.JOBPROBID = 146;
                                                                        objJobCardDetails4.JOBPROBID1 = 0;
                                                                        objJobCardDetails4.JOBPROBID2 = 0;
                                                                        objJobCardDetails4.JOBPROBID3 = 0;
                                                                        objJobCardDetails4.NEWIMEINO = "";
                                                                        objJobCardDetails4.NEXTSTAGEREQ = 59;
                                                                        objJobCardDetails4.NOTE = "OK FOR CHECK";
                                                                        objJobCardDetails4.PARTREPLACED = "";
                                                                        objJobCardDetails4.PARTREQ = "";
                                                                        objJobCardDetails4.PARTREQID = 0;
                                                                        objJobCardDetails4.PROBLEM = "OK FOR CHECK";
                                                                        objJobCardDetails4.PROBLEM1 = "";
                                                                        objJobCardDetails4.PROBLEM2 = "";
                                                                        objJobCardDetails4.PROBLEM3 = "";
                                                                        objJobCardDetails4.RESULT = 25;
                                                                        objJobCardDetails4.STAGEID = 59;
                                                                        objJobCardDetails4.STARTDT = DateTime.Now;
                                                                        objJobCardDetails4.ENDDT = DateTime.Now;
                                                                        objJobCardDetails4.JOBID = NEWJOBID;

                                                                        string URLJCDetails4 = Convert.ToString(dtInsertJCDetails.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertJCDetails.Rows[0]["TOKEN"]);
                                                                        var clientJCDetails4 = new RestClient(URLJCDetails4);
                                                                        clientJCDetails4.Timeout = -1;
                                                                        var requestJCDetails4 = new RestRequest(Method.POST);
                                                                        requestJCDetails4.AddHeader("" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYVALUE"]) + "");
                                                                        requestJCDetails4.AddHeader("Content-Type", "application/json");
                                                                        var jsonInputJCDetails4 = JsonConvert.SerializeObject(objJobCardDetails4);
                                                                        requestJCDetails4.AddParameter("application/json", jsonInputJCDetails4, ParameterType.RequestBody);
                                                                        IRestResponse responseES4 = clientJCDetails4.Execute(requestJCDetails4);

                                                                        #endregion


                                                                    }
                                                                    else
                                                                    {
                                                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Job Card Details API Not Found. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Job Card Not Created. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                                                                }

                                                            }
                                                            else
                                                            {
                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Job Card Not Created. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                                                            }
                                                        }

                                                        if (JOBSTAGEID == 19)
                                                        {
                                                            DataTable dtInsertES = new DataTable();
                                                            dtInsertES = objMainClass.GetWAData("CREATEESTIMATE", 1, "GETWADATA");

                                                            if (dtInsertES.Rows.Count > 0)
                                                            {

                                                                #region Estimate Master...
                                                                EstimateMaster objEstimateMaster = new EstimateMaster();
                                                                objEstimateMaster.ASCCHG = 550;
                                                                objEstimateMaster.CMPID = objMainClass.intCmpId;
                                                                objEstimateMaster.COSTAMT_PART = 0;
                                                                objEstimateMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                objEstimateMaster.CREATEDATE = Convert.ToDateTime(DateTime.Now).ToString();
                                                                objEstimateMaster.DISCAMT = 0;
                                                                objEstimateMaster.ESTIAMT_PART = 0;
                                                                objEstimateMaster.ESTIAMT_SERV = 550;
                                                                objEstimateMaster.ESTIDT = DateTime.Now;
                                                                objEstimateMaster.ESTINO = "";
                                                                objEstimateMaster.ETD = DateTime.Now;
                                                                objEstimateMaster.HSNEW = "N";
                                                                objEstimateMaster.ISRETURN = "N";
                                                                objEstimateMaster.JOBID = NEWJOBID;
                                                                objEstimateMaster.JOBIDSRNO = 1;
                                                                objEstimateMaster.LIQUIDDAMAGE = "N";
                                                                objEstimateMaster.LOGICHG = 0;
                                                                objEstimateMaster.NWREASON = 0;
                                                                objEstimateMaster.PARTDESC = "NA";
                                                                objEstimateMaster.PAYMODE = 8;
                                                                objEstimateMaster.PURDT = DateTime.Now;
                                                                objEstimateMaster.PURREF = "";
                                                                objEstimateMaster.RBATTERYNO = "";
                                                                objEstimateMaster.REMARK = "AUTO GENRATED AGAINST SO";
                                                                objEstimateMaster.RIMEINO = txtIMEI.Text;
                                                                objEstimateMaster.RITEMID = 0;
                                                                objEstimateMaster.RPRODMAKE = hfMake.Value;
                                                                objEstimateMaster.RPRODMODEL = hfModel.Value;
                                                                objEstimateMaster.SERVDESC = "NA";
                                                                objEstimateMaster.STAGEID = (int)STAGE.EstimateCreate;
                                                                objEstimateMaster.STATRES = "AUTO GENRATED AGAINST SO";
                                                                objEstimateMaster.STATUS = (int)STATUS.Estimated;
                                                                objEstimateMaster.TOTALLOSS = "N";
                                                                objEstimateMaster.TRANCHG = 0;
                                                                objEstimateMaster.TRANCHGPCT = 0;

                                                                #endregion

                                                                string URLES = Convert.ToString(dtInsertES.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertES.Rows[0]["TOKEN"]);
                                                                var clientES = new RestClient(URLES);
                                                                clientES.Timeout = -1;
                                                                var requestES = new RestRequest(Method.POST);
                                                                requestES.AddHeader("" + Convert.ToString(dtInsertES.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertES.Rows[0]["KEYVALUE"]) + "");
                                                                requestES.AddHeader("Content-Type", "application/json");
                                                                var jsonInputES = JsonConvert.SerializeObject(objEstimateMaster);
                                                                requestES.AddParameter("application/json", jsonInputES, ParameterType.RequestBody);
                                                                IRestResponse responseES = clientES.Execute(requestES);

                                                                EstimateResponse objEstimateResponse = new EstimateResponse();
                                                                string jsonconnEstimate = responseES.Content;
                                                                objEstimateResponse = JsonConvert.DeserializeObject<EstimateResponse>(jsonconnEstimate);

                                                                NEWESTINO = objEstimateResponse.ESTINO;
                                                                hfEstiNo.Value = NEWESTINO;
                                                            }
                                                            else
                                                            {
                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Estimate Not Created. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                                                            }

                                                        }

                                                        if (JOBSTAGEID == 39)
                                                        {
                                                            if (NEWESTINO != "" && NEWESTINO != null && NEWESTINO != string.Empty)
                                                            {
                                                                DataTable dtInsertESAPRV = new DataTable();
                                                                dtInsertESAPRV = objMainClass.GetWAData("APPROVEESTIMATE", 1, "GETWADATA");

                                                                if (dtInsertESAPRV.Rows.Count > 0)
                                                                {

                                                                    EstimateApproval objEstimateApproval = new EstimateApproval();
                                                                    objEstimateApproval.APRVBY1 = 124;
                                                                    objEstimateApproval.APRVDT1 = DateTime.Now;
                                                                    objEstimateApproval.APRVFLAG = (int)APRVTYPE.APPROVED;
                                                                    objEstimateApproval.APRVNO1 = "APNP";
                                                                    objEstimateApproval.APRVNOTE = "";
                                                                    objEstimateApproval.CMPID = objMainClass.intCmpId;
                                                                    objEstimateApproval.CUSTAPRVBY = "";
                                                                    objEstimateApproval.ESTINO = NEWESTINO;
                                                                    objEstimateApproval.PAYMODE = 8;
                                                                    objEstimateApproval.REJRES = 0;
                                                                    objEstimateApproval.STAGEID = (int)STAGE.EstimatApproved;
                                                                    objEstimateApproval.STATUS = (int)STATUS.Approved;
                                                                    objEstimateApproval.UPDATEBY = Convert.ToInt32(Session["USERID"]);
                                                                    objEstimateApproval.UPDATEDATE = Convert.ToDateTime(DateTime.Now).ToString();


                                                                    string URLESAPRV = Convert.ToString(dtInsertESAPRV.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertESAPRV.Rows[0]["TOKEN"]);
                                                                    var clientESAPRV = new RestClient(URLESAPRV);
                                                                    clientESAPRV.Timeout = -1;
                                                                    var requestESAPRV = new RestRequest(Method.POST);
                                                                    requestESAPRV.AddHeader("" + Convert.ToString(dtInsertESAPRV.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertESAPRV.Rows[0]["KEYVALUE"]) + "");
                                                                    requestESAPRV.AddHeader("Content-Type", "application/json");
                                                                    var jsonInputESAPRV = JsonConvert.SerializeObject(objEstimateApproval);
                                                                    requestESAPRV.AddParameter("application/json", jsonInputESAPRV, ParameterType.RequestBody);
                                                                    IRestResponse responseESAPRV = clientESAPRV.Execute(requestESAPRV);

                                                                    EstimateApprovalResponse objEstimateApprovalResponse = new EstimateApprovalResponse();
                                                                    string jsonconnEstimateAPRV = responseESAPRV.Content;
                                                                    objEstimateApprovalResponse = JsonConvert.DeserializeObject<EstimateApprovalResponse>(jsonconnEstimateAPRV);

                                                                }
                                                                else
                                                                {
                                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Estimate Not Approved. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Estimate Not Approved. Estimate Number not found. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                                                            }
                                                        }

                                                    }
                                                }
                                            }
                                            else
                                            {
                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Job Status Update API not Found. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                                            }
                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Stages Insert API not Found. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                                        }


                                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. Job ID : " + NEWJOBID + "\");", true);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Stages Not Updated. Job ID : " + NEWJOBID + "\");", true);
                                    }



                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Reverse Waybill Update API not Found. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                                }


                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Reverse Waybill Update API not Found. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                            }


                            DataTable dtGetSpec = new DataTable();
                            dtGetSpec = objMainClass.GetWAData("GETJOBSPECIFICATION", 1, "GETWADATA");
                            DataTable dtAddSpec = new DataTable();
                            dtAddSpec = objMainClass.GetWAData("INSERTJOBSPECIFICATION", 1, "GETWADATA");

                            string trimjobid = objMainClass.LtrimZero(NEWJOBID, "0");
                            int isrno = trimjobid.Length + 4;
                            string serialnumber = trimjobid + "" + objMainClass.RevertString(txtIMEI.Text).Substring(0, 15 - isrno) + "" + DateTime.Now.Year;
                            string ramsku = "";
                            if (hfMake.Value == "APPLE")
                            {
                                ramsku = "GB_";
                            }
                            else
                            {
                                ramsku = Convert.ToInt32(hfRAM.Value) > 100 ? "MB_" : "GB_";
                            }

                            string romsku = Convert.ToInt32(hfROM.Value) <= 2 ? "TB_" : "GB_";
                            string sku = hfMake.Value + "_" + hfModel.Value + "_" + hfRAM.Value + "" + ramsku + "" + hfROM.Value + "" + romsku + hfColor.Value + " (REFURB)";

                            if (dtAddSpec.Rows.Count > 0)
                            {

                                txtJobID.Text = objMainClass.strConvertZeroPadding(NEWJOBID);
                                objBindDDL.FillLists(ddlPurGrade, "BG");
                                txtMake.Text = (hfMake.Value).ToUpper();
                                txtModel.Text = (hfModel.Value).ToUpper();
                                objBindDDL.FillLists(ddlRAM, "RAM");
                                objBindDDL.FillLists(ddlROM, "ROM");
                                objBindDDL.FillLists(ddlColor, "CL");
                                objBindDDL.FillLists(ddlGrade, "BG");
                                objBindDDL.FillLists(ddlDispSize, "DS");
                                objBindDDL.FillLists(ddlFrontCam, "FC");
                                objBindDDL.FillLists(ddlBackCam, "BC");
                                objBindDDL.FillLists(ddlLCDColor, "CL");
                                txtMRP.Text = hfPrice.Value;
                                txtSpecIMEI.Text = txtIMEI.Text;
                                objBindDDL.FillLists(ddlCableType, "CT");
                                txtJDARefDt.Text = DateTime.Now.ToString();
                                txtSerialNo.Text = serialnumber;
                                txtSKU.Text = (sku).ToUpper();
                                txtSpeItemCode.Text = hfItemCode.Value;
                                ddlRAM.SelectedValue = ddlRAM.Items.FindByText(hfRAM.Value).Value;//hfRAM.Value;
                                ddlROM.SelectedValue = ddlROM.Items.FindByText(hfROM.Value).Value;//hfROM.Value;
                                ddlColor.SelectedValue = ddlColor.Items.FindByText(hfColor.Value).Value;//hfColor.Value;

                                if (dtGetSpec.Rows.Count > 0)
                                {
                                    JobSpecification objJobSpecification = new JobSpecification();
                                    objJobSpecification.CMPID = objMainClass.intCmpId;
                                    objJobSpecification.ITEMCODE = hfItemCode.Value;
                                    objJobSpecification.MODELDESC = hfModel.Value;
                                    objJobSpecification.RAMSIZE = hfRAM.Value;
                                    objJobSpecification.ROMSIZE = hfROM.Value;
                                    objJobSpecification.COLOR = hfColor.Value;
                                    objJobSpecification.ACTION = "GETDATABYITEMCODE";
                                    string URLGetSpec = Convert.ToString(dtGetSpec.Rows[0]["OTHER"]) + "" + Convert.ToString(dtGetSpec.Rows[0]["TOKEN"]);
                                    var clientGetSpec = new RestClient(URLGetSpec);
                                    clientGetSpec.Timeout = -1;
                                    var requestGetSpec = new RestRequest(Method.POST);
                                    requestGetSpec.AddHeader("" + Convert.ToString(dtGetSpec.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtGetSpec.Rows[0]["KEYVALUE"]) + "");
                                    requestGetSpec.AddHeader("Content-Type", "application/json");
                                    var jsonInputGetSpec = JsonConvert.SerializeObject(objJobSpecification);
                                    requestGetSpec.AddParameter("application/json", jsonInputGetSpec, ParameterType.RequestBody);
                                    IRestResponse responseGetSpec = clientGetSpec.Execute(requestGetSpec);
                                    if (responseGetSpec.StatusCode == HttpStatusCode.OK)
                                    {
                                        JobSpecificationResponse objJobSpecificationResponse = new JobSpecificationResponse();
                                        string jsonGetSpec = responseGetSpec.Content;
                                        objJobSpecificationResponse = JsonConvert.DeserializeObject<JobSpecificationResponse>(jsonGetSpec);
                                        if (objJobSpecificationResponse.Data.Rows.Count > 0)
                                        {
                                            DataTable dtSpecs = new DataTable();
                                            dtSpecs = objJobSpecificationResponse.Data;
                                            if (dtSpecs.Rows.Count > 0)
                                            {
                                                ddlPurGrade.SelectedValue = ddlPurGrade.Items.FindByText(Convert.ToString(dtSpecs.Rows[0]["PURGRADE"])).Value;
                                                ddlRAM.SelectedValue = ddlRAM.Items.FindByText(hfRAM.Value).Value;//hfRAM.Value;
                                                ddlROM.SelectedValue = ddlROM.Items.FindByText(hfROM.Value).Value; //hfROM.Value;
                                                ddlColor.SelectedValue = ddlColor.Items.FindByText(hfColor.Value).Value;
                                                ddlGrade.SelectedValue = ddlGrade.Items.FindByText(Convert.ToString(dtSpecs.Rows[0]["PRODGRADE"])).Value;
                                                if (Convert.ToString(dtSpecs.Rows[0]["DISPLAYSIZE"]) != null && Convert.ToString(dtSpecs.Rows[0]["DISPLAYSIZE"]) != "" && Convert.ToString(dtSpecs.Rows[0]["DISPLAYSIZE"]) != string.Empty)
                                                {
                                                    ddlDispSize.SelectedValue = ddlDispSize.Items.FindByText(Convert.ToString(dtSpecs.Rows[0]["DISPLAYSIZE"])).Value;
                                                }
                                                if (Convert.ToString(dtSpecs.Rows[0]["FRONT_CAMERA"]) != null && Convert.ToString(dtSpecs.Rows[0]["FRONT_CAMERA"]) != "" && Convert.ToString(dtSpecs.Rows[0]["FRONT_CAMERA"]) != string.Empty)
                                                {
                                                    ddlFrontCam.SelectedValue = ddlFrontCam.Items.FindByText(Convert.ToString(dtSpecs.Rows[0]["FRONT_CAMERA"])).Value;
                                                }
                                                if (Convert.ToString(dtSpecs.Rows[0]["BACK_CAMERA"]) != null && Convert.ToString(dtSpecs.Rows[0]["BACK_CAMERA"]) != "" && Convert.ToString(dtSpecs.Rows[0]["BACK_CAMERA"]) != string.Empty)
                                                {
                                                    ddlBackCam.SelectedValue = ddlBackCam.Items.FindByText(Convert.ToString(dtSpecs.Rows[0]["BACK_CAMERA"])).Value;
                                                }
                                                if (Convert.ToString(dtSpecs.Rows[0]["LCDCOLOR"]) != null && Convert.ToString(dtSpecs.Rows[0]["LCDCOLOR"]) != "" && Convert.ToString(dtSpecs.Rows[0]["LCDCOLOR"]) != string.Empty)
                                                {
                                                    ddlLCDColor.SelectedValue = ddlLCDColor.Items.FindByText(Convert.ToString(dtSpecs.Rows[0]["LCDCOLOR"])).Value;
                                                }
                                                if (Convert.ToString(dtSpecs.Rows[0]["CABELTYPE"]) != null && Convert.ToString(dtSpecs.Rows[0]["CABELTYPE"]) != "" && Convert.ToString(dtSpecs.Rows[0]["CABELTYPE"]) != string.Empty)
                                                {
                                                    ddlCableType.SelectedValue = ddlCableType.Items.FindByText(Convert.ToString(dtSpecs.Rows[0]["CABELTYPE"])).Value;
                                                }
                                                if (Convert.ToString(dtSpecs.Rows[0]["VOLTE_4G"]) == "1")
                                                {
                                                    chkVolte.Checked = true;
                                                }
                                                else
                                                {
                                                    chkVolte.Checked = false;
                                                }


                                            }
                                        }
                                        else
                                        {
                                            JobSpecification objJobSpecification1 = new JobSpecification();
                                            objJobSpecification1.CMPID = objMainClass.intCmpId;
                                            objJobSpecification1.ITEMCODE = hfItemCode.Value;
                                            objJobSpecification1.MODELDESC = hfModel.Value;
                                            objJobSpecification1.RAMSIZE = hfRAM.Value;
                                            objJobSpecification1.ROMSIZE = hfROM.Value;
                                            objJobSpecification1.COLOR = hfColor.Value;
                                            objJobSpecification1.ACTION = "GETDATABYITEMSPEC";

                                            string URLGetSpec1 = Convert.ToString(dtGetSpec.Rows[0]["OTHER"]) + "" + Convert.ToString(dtGetSpec.Rows[0]["TOKEN"]);
                                            var clientGetSpec1 = new RestClient(URLGetSpec1);
                                            clientGetSpec1.Timeout = -1;
                                            var requestGetSpec1 = new RestRequest(Method.POST);
                                            requestGetSpec1.AddHeader("" + Convert.ToString(dtGetSpec.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtGetSpec.Rows[0]["KEYVALUE"]) + "");
                                            requestGetSpec1.AddHeader("Content-Type", "application/json");
                                            var jsonInputGetSpec1 = JsonConvert.SerializeObject(objJobSpecification1);
                                            requestGetSpec1.AddParameter("application/json", jsonInputGetSpec1, ParameterType.RequestBody);
                                            IRestResponse responseGetSpec1 = clientGetSpec.Execute(requestGetSpec1);
                                            if (responseGetSpec1.StatusCode == HttpStatusCode.OK)
                                            {
                                                JobSpecificationResponse objJobSpecificationResponse1 = new JobSpecificationResponse();
                                                string jsonGetSpec1 = responseGetSpec1.Content;
                                                objJobSpecificationResponse1 = JsonConvert.DeserializeObject<JobSpecificationResponse>(jsonGetSpec1);
                                                if (objJobSpecificationResponse1.Data.Rows.Count > 0)
                                                {
                                                    DataTable dtSpecs1 = new DataTable();
                                                    dtSpecs1 = objJobSpecificationResponse1.Data;

                                                    if (dtSpecs1.Rows.Count > 0)
                                                    {
                                                        ddlPurGrade.SelectedValue = ddlPurGrade.Items.FindByText(Convert.ToString(dtSpecs1.Rows[0]["PURGRADE"])).Value;
                                                        ddlRAM.SelectedValue = ddlRAM.Items.FindByText(hfRAM.Value).Value;//hfRAM.Value;
                                                        ddlROM.SelectedValue = ddlROM.Items.FindByText(hfROM.Value).Value; //hfROM.Value;
                                                        ddlColor.SelectedValue = ddlColor.Items.FindByText(hfColor.Value).Value;
                                                        ddlGrade.SelectedValue = ddlGrade.Items.FindByText(Convert.ToString(dtSpecs1.Rows[0]["PRODGRADE"])).Value;
                                                        if (Convert.ToString(dtSpecs1.Rows[0]["DISPLAYSIZE"]) != null && Convert.ToString(dtSpecs1.Rows[0]["DISPLAYSIZE"]) != "" && Convert.ToString(dtSpecs1.Rows[0]["DISPLAYSIZE"]) != string.Empty)
                                                        {
                                                            ddlDispSize.SelectedValue = ddlDispSize.Items.FindByText(Convert.ToString(dtSpecs1.Rows[0]["DISPLAYSIZE"])).Value;
                                                        }
                                                        if (Convert.ToString(dtSpecs1.Rows[0]["FRONT_CAMERA"]) != null && Convert.ToString(dtSpecs1.Rows[0]["FRONT_CAMERA"]) != "" && Convert.ToString(dtSpecs1.Rows[0]["FRONT_CAMERA"]) != string.Empty)
                                                        {
                                                            ddlFrontCam.SelectedValue = ddlFrontCam.Items.FindByText(Convert.ToString(dtSpecs1.Rows[0]["FRONT_CAMERA"])).Value;
                                                        }
                                                        if (Convert.ToString(dtSpecs1.Rows[0]["BACK_CAMERA"]) != null && Convert.ToString(dtSpecs1.Rows[0]["BACK_CAMERA"]) != "" && Convert.ToString(dtSpecs1.Rows[0]["BACK_CAMERA"]) != string.Empty)
                                                        {
                                                            ddlBackCam.SelectedValue = ddlBackCam.Items.FindByText(Convert.ToString(dtSpecs1.Rows[0]["BACK_CAMERA"])).Value;
                                                        }
                                                        if (Convert.ToString(dtSpecs1.Rows[0]["LCDCOLOR"]) != null && Convert.ToString(dtSpecs1.Rows[0]["LCDCOLOR"]) != string.Empty && Convert.ToString(dtSpecs1.Rows[0]["LCDCOLOR"]) != "")
                                                        {
                                                            ddlLCDColor.SelectedValue = ddlLCDColor.Items.FindByText(Convert.ToString(dtSpecs1.Rows[0]["LCDCOLOR"])).Value;
                                                        }
                                                        if (Convert.ToString(dtSpecs1.Rows[0]["CABELTYPE"]) != null && Convert.ToString(dtSpecs1.Rows[0]["CABELTYPE"]) != "" && Convert.ToString(dtSpecs1.Rows[0]["CABELTYPE"]) != string.Empty)
                                                        {
                                                            ddlCableType.SelectedValue = ddlCableType.Items.FindByText(Convert.ToString(dtSpecs1.Rows[0]["CABELTYPE"])).Value;
                                                        }
                                                        if (Convert.ToString(dtSpecs1.Rows[0]["VOLTE_4G"]) == "1")
                                                        {
                                                            chkVolte.Checked = true;
                                                        }
                                                        else
                                                        {
                                                            chkVolte.Checked = false;
                                                        }


                                                    }

                                                }
                                            }
                                        }
                                    }

                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Specs').modal();", true);

                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Job Specification API not Found. Please Contact API Provider. Job ID : " + NEWJOBID + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                            }


                        }
                        else
                        {
                            cltControl();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Job Sheet creation API not found in Database. Please Contact API Provider.');", true);
                        }
                    }
                    else
                    {
                        cltControl();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Job Sheet creation API not found in Database. Please Contact API Provider.');", true);
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

        public void cltControl()
        {

            txtIMEI.Text = string.Empty;
            txtIMEI2.Text = string.Empty;
            hfOldJobID.Value = string.Empty;
            hfMake.Value = string.Empty;
            hfModel.Value = string.Empty;
            hfColor.Value = string.Empty;
            hfItemCode.Value = string.Empty;
            hfItemDesc.Value = string.Empty;
            hfProdItemID.Value = string.Empty;
            hfProdItemDesc.Value = string.Empty;
            hfSOQty.Value = string.Empty;
            hfPlantCd.Value = string.Empty;
            hfLocCd.Value = string.Empty;
            hfSONO.Value = string.Empty;
            txtReveCourier.Text = string.Empty;
            txtRevWaybill.Text = string.Empty;
            hfSalesFrom.Value = string.Empty;
            hfRAM.Value = string.Empty;
            hfROM.Value = string.Empty;
            hfPrice.Value = string.Empty;
            hfLockAmt.Value = string.Empty;
            hfPRNo.Value = string.Empty;
            hfPONo.Value = string.Empty;
            hfGRNNo.Value = string.Empty;
            hfPBNo.Value = string.Empty;
            txtVendorCode.Text = string.Empty;
            txtVendorName.Text = string.Empty;
            txtTranCode.Text = string.Empty;
            txtTranName.Text = string.Empty;
            txtPaymentTermsDesc.Text = string.Empty;
            txtBillNo.Text = string.Empty;
            txtChgAmt.Text = string.Empty;

            txtJobID.Text = string.Empty;
            txtMake.Text = string.Empty;
            txtModel.Text = string.Empty;
            txtMRP.Text = string.Empty;
            txtSpecIMEI.Text = string.Empty;
            txtJDARefDt.Text = string.Empty;
            txtSerialNo.Text = string.Empty;
            txtSKU.Text = string.Empty;
            txtSpeItemCode.Text = string.Empty;

        }

        protected void btnSpecs_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dtAddSpec = new DataTable();
                    dtAddSpec = objMainClass.GetWAData("INSERTJOBSPECIFICATION", 1, "GETWADATA");
                    if (dtAddSpec.Rows.Count > 0)
                    {

                        JobSpecification objJobSpecificationAdd1 = new JobSpecification();
                        objJobSpecificationAdd1.ACTION = "INSERTJOBSPECIFICATION";
                        objJobSpecificationAdd1.BACK_CAMERA = ddlBackCam.SelectedItem.Text;
                        objJobSpecificationAdd1.BACK_CAMERA2 = "";
                        objJobSpecificationAdd1.CABELTYPE = ddlCableType.SelectedItem.Text;
                        objJobSpecificationAdd1.CMPID = objMainClass.intCmpId;
                        objJobSpecificationAdd1.COLOR = ddlColor.SelectedItem.Text;
                        objJobSpecificationAdd1.CREATEBY = Convert.ToInt32(Session["USERID"]);
                        objJobSpecificationAdd1.CREATEDATE = DateTime.Now;
                        objJobSpecificationAdd1.DISPLAYSIZE = ddlDispSize.SelectedItem.Text;
                        objJobSpecificationAdd1.FRONT_CAMERA = ddlFrontCam.SelectedItem.Text;
                        objJobSpecificationAdd1.FRONT_CAMERA2 = "";
                        objJobSpecificationAdd1.ITEMCODE = txtSpeItemCode.Text;
                        objJobSpecificationAdd1.JOBID = txtJobID.Text;
                        objJobSpecificationAdd1.LCDCOLOR = ddlLCDColor.SelectedItem.Text;
                        objJobSpecificationAdd1.MODELDESC = txtModel.Text;
                        objJobSpecificationAdd1.MRP = Convert.ToDecimal(txtMRP.Text);
                        objJobSpecificationAdd1.PRODGRADE = ddlGrade.SelectedItem.Text;
                        objJobSpecificationAdd1.PURGRADE = ddlPurGrade.SelectedItem.Text;
                        objJobSpecificationAdd1.RAMSIZE = ddlRAM.SelectedItem.Text;
                        objJobSpecificationAdd1.ROMSIZE = ddlROM.SelectedItem.Text;
                        objJobSpecificationAdd1.SERIALNO = txtSerialNo.Text;
                        objJobSpecificationAdd1.SKU = txtSKU.Text;
                        objJobSpecificationAdd1.VOLTE_4G = chkVolte.Checked == true ? "1" : "0";

                        string URLAddSpec = Convert.ToString(dtAddSpec.Rows[0]["OTHER"]) + "" + Convert.ToString(dtAddSpec.Rows[0]["TOKEN"]);
                        var clientAddSpec = new RestClient(URLAddSpec);
                        clientAddSpec.Timeout = -1;
                        var requestAddSpec = new RestRequest(Method.POST);
                        requestAddSpec.AddHeader("" + Convert.ToString(dtAddSpec.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtAddSpec.Rows[0]["KEYVALUE"]) + "");
                        requestAddSpec.AddHeader("Content-Type", "application/json");
                        var jsonInputAddSpec = JsonConvert.SerializeObject(objJobSpecificationAdd1);
                        requestAddSpec.AddParameter("application/json", jsonInputAddSpec, ParameterType.RequestBody);
                        IRestResponse responseAddSpec = clientAddSpec.Execute(requestAddSpec);

                        if (responseAddSpec.StatusCode == HttpStatusCode.OK)
                        {
                            string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                            if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                            {
                                message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                            }
                            else
                            {
                                message = message + " New Job card not Created.";
                            }

                            if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                            {
                                message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                            }
                            else
                            {
                                message = message + " New Estimate not Created.";
                            }
                            message = message + " Job Specification created successfully.";

                            if (hfProdItemID.Value == "1")
                            {
                                DataTable dtItemDesc = new DataTable();
                                dtItemDesc = objMainClass.GetItemDetailsByItemCode(objMainClass.intCmpId, hfItemCode.Value, 1, "ITEMMASTERSEARCH");

                                int newmax = 0;
                                int newmin = 0;

                                decimal maxstk = objMainClass.SP_GET_STOCK_FUNCTION(objMainClass.intCmpId, objMainClass.getFinYear(objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true)), hfItemCode.Value, objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true), hfPlantCd.Value, hfLocCd.Value);

                                DataTable dt = new DataTable();
                                dt = objMainClass.GetMaxMinStk(objMainClass.intCmpId, Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]), hfPlantCd.Value, hfLocCd.Value, 1, "GETMAXMINSTK");
                                if (dt.Rows.Count > 0)
                                {
                                    newmax = Convert.ToInt32(dt.Rows[0]["MAXSTKQTY"]);
                                    newmin = Convert.ToInt32(dt.Rows[0]["MINSTKQTY"]);
                                }

                                if (maxstk >= newmax)
                                {
                                    message = message + "Current stock is at its max, you cannot place order more than that! Maximum stock : " + newmax + ". Current Stock : " + maxstk + ".!";
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                }
                                else
                                {
                                    CreatePRPO();
                                }
                            }
                            else
                            {
                                CreatePRPO();
                            }


                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                        }
                        else
                        {
                            string message = "New Job Id Created. Job ID is : " + hfNewJobID.Value + ".";
                            if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                            {
                                message = message + "New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                            }
                            else
                            {
                                message = message + "New Job card not Created.";
                            }

                            if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                            {
                                message = message + "New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                            }
                            else
                            {
                                message = message + "New Estimate not Created.";
                            }
                            message = message + "Job Specification not created.";

                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Job Specification API not Found. Please Contact API Provider. Job ID : " + hfNewJobID.Value + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
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


        public void CreatePRPO()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dtPRAPI = new DataTable();
                    dtPRAPI = objMainClass.GetWAData("INSERTPR", 1, "GETWADATA");
                    if (dtPRAPI.Rows.Count > 0)
                    {
                        DataTable dtCOMMISSIONAPI = new DataTable();
                        dtCOMMISSIONAPI = objMainClass.GetWAData("GETCOMMISSION", 1, "GETWADATA");

                        if (dtCOMMISSIONAPI.Rows.Count > 0)
                        {
                            CommissionMaster objCommissionMaster = new CommissionMaster();
                            objCommissionMaster.ITEMCATEGORY = Convert.ToInt32(hfProdItemID.Value);
                            objCommissionMaster.STATUS = 1;
                            objCommissionMaster.ACTION = "GETCOMMISSIONRATENEW";
                            objCommissionMaster.CREATEBY = 0;
                            objCommissionMaster.CREATEDATE = DateTime.Now;
                            objCommissionMaster.PERORFIX = 0;
                            objCommissionMaster.RATE = 0;
                            objCommissionMaster.UPDATEBY = 0;
                            objCommissionMaster.UPDATEDATE = DateTime.Now;
                            objCommissionMaster.VENDCODE = objMainClass.strConvertZeroPadding(txtVendorCode.Text);

                            string URLGetCommission = Convert.ToString(dtCOMMISSIONAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtCOMMISSIONAPI.Rows[0]["TOKEN"]);
                            var clientGetCommission = new RestClient(URLGetCommission);
                            clientGetCommission.Timeout = -1;
                            var requestGetCommission = new RestRequest(Method.POST);
                            requestGetCommission.AddHeader("" + Convert.ToString(dtCOMMISSIONAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtCOMMISSIONAPI.Rows[0]["KEYVALUE"]) + "");
                            requestGetCommission.AddHeader("Content-Type", "application/json");
                            var jsonGetCommission = JsonConvert.SerializeObject(objCommissionMaster);
                            requestGetCommission.AddParameter("application/json", jsonGetCommission, ParameterType.RequestBody);
                            IRestResponse responseGetCommission = clientGetCommission.Execute(requestGetCommission);

                            if (responseGetCommission.StatusCode == HttpStatusCode.OK)
                            {
                                CommissionResponse objCommissionResponse = new CommissionResponse();
                                string jsonconn = responseGetCommission.Content;
                                objCommissionResponse = JsonConvert.DeserializeObject<CommissionResponse>(jsonconn);

                                string costcenter = "1007";
                                DataTable dtCostCenter = new DataTable();
                                dtCostCenter = objMainClass.GetCostCenter(hfPlantCd.Value, hfLocCd.Value);

                                DataTable dtItemDesc = new DataTable();
                                dtItemDesc = objMainClass.GetItemDetailsByItemCode(objMainClass.intCmpId, hfItemCode.Value, 1, "ITEMMASTERSEARCH");

                                if (dtCostCenter.Rows.Count > 0)
                                {
                                    costcenter = Convert.ToString(dtCostCenter.Rows[0]["CSTCENTCD"]);
                                }
                                else
                                {
                                    costcenter = "1007";
                                }


                                if (objCommissionResponse.Data.Rows.Count > 0)
                                {
                                    DataTable dtCommission = new DataTable();
                                    dtCommission = objCommissionResponse.Data;
                                    string CALCON = Convert.ToString(dtCommission.Rows[0]["CALCON"]);
                                    int PERORFIX = Convert.ToInt32(dtCommission.Rows[0]["PERORFIX"]);
                                    decimal RATE = Convert.ToDecimal(dtCommission.Rows[0]["RATE"]);
                                    if (CALCON == "LOCK")
                                    {
                                        if (Convert.ToDecimal(hfLockAmt.Value) > 0)
                                        {
                                            if (PERORFIX == 1)
                                            {
                                                if (chkGST.Checked == true)
                                                {
                                                    decimal purcomm = 0;
                                                    decimal purprice = 0;
                                                    decimal basprice = 0;
                                                    decimal tax = 0;

                                                    decimal othchg = 0;
                                                    decimal othbasechg = 0;
                                                    decimal othchgtax = 0;
                                                    decimal nettaxamt = 0;
                                                    decimal netpoamt = 0;

                                                    purcomm = Math.Round((Convert.ToDecimal(hfLockAmt.Value) * (RATE) / 100), 2);
                                                    purprice = Math.Round((Convert.ToDecimal(hfLockAmt.Value) + Convert.ToDecimal(purcomm)), 2);
                                                    basprice = Math.Round((Convert.ToDecimal(purprice / Convert.ToDecimal("1.18"))), 2);
                                                    tax = Math.Round((purprice - basprice), 2);
                                                    nettaxamt = Math.Round((tax), 2);
                                                    netpoamt = Math.Round((purprice), 2);

                                                    if (txtChgAmt.Text != null && txtChgAmt.Text != string.Empty && txtChgAmt.Text != null)
                                                    {
                                                        if (Convert.ToDecimal(txtChgAmt.Text) > 0)
                                                        {
                                                            othchg = Math.Round((Convert.ToDecimal(txtChgAmt.Text)), 2);
                                                            othbasechg = Math.Round((Convert.ToDecimal(othchg / Convert.ToDecimal("1.18"))), 2);
                                                            othchgtax = Math.Round((othchg - othbasechg), 2);
                                                            nettaxamt = Math.Round((nettaxamt + othchgtax), 2);
                                                            netpoamt = Math.Round((netpoamt + othchg), 2);
                                                        }
                                                    }


                                                    List<PRDetails> objPRDetails = new List<PRDetails>();
                                                    objPRDetails.Add(new PRDetails
                                                    {
                                                        ASSETCD = "",
                                                        CAMOUNT = purprice,
                                                        CMPID = objMainClass.intCmpId,
                                                        CSTCENTCD = costcenter,
                                                        DELIDT = DateTime.Now,
                                                        GLCD = "10010000",
                                                        IMEINO = txtIMEI.Text,
                                                        ITEMDESC = hfItemDesc.Value,
                                                        ITEMGRPID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMGRP"]),
                                                        ITEMID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]),
                                                        ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",
                                                        LOCCD = hfLocCd.Value,
                                                        PARTREQNO = 0,
                                                        PLANTCD = hfPlantCd.Value,
                                                        PRBY = "",
                                                        PRFCNT = "1000",
                                                        PRNO = "",
                                                        PRQTY = 1,
                                                        RATE = purprice,
                                                        SRNO = 1,
                                                        STATUS = 2,
                                                        TRNUM = hfNewJobID.Value,
                                                        UOM = Convert.ToInt32(dtItemDesc.Rows[0]["UOM"]),
                                                    });

                                                    #region Inser PR Method...




                                                    PRMaster objPRMaster = new PRMaster();
                                                    objPRMaster.CMPID = objMainClass.intCmpId;
                                                    objPRMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                    objPRMaster.CREATEDATE = DateTime.Now;
                                                    objPRMaster.DEPTID = 1;
                                                    objPRMaster.ISPRSTO = 0;
                                                    objPRMaster.LISTINGID = 0;
                                                    objPRMaster.PRDT = DateTime.Now;
                                                    objPRMaster.PRNO = "";
                                                    objPRMaster.PRTYPE = "MPR";
                                                    objPRMaster.REMARK = "AUTO PR/PO AGAINST SO";
                                                    objPRMaster.STATUS = 57;
                                                    objPRMaster.VENDCODE = "";
                                                    objPRMaster.PRDATA = objPRDetails;


                                                    string PRURL = Convert.ToString(dtPRAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtPRAPI.Rows[0]["TOKEN"]);
                                                    var clientPR = new RestClient(PRURL);
                                                    clientPR.Timeout = -1;
                                                    var requestPR = new RestRequest(Method.POST);
                                                    requestPR.AddHeader("" + Convert.ToString(dtPRAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtPRAPI.Rows[0]["KEYVALUE"]) + "");
                                                    requestPR.AddHeader("Content-Type", "application/json");
                                                    var jsonInputPR = JsonConvert.SerializeObject(objPRMaster);
                                                    requestPR.AddParameter("application/json", jsonInputPR, ParameterType.RequestBody);
                                                    IRestResponse responsePR = clientPR.Execute(requestPR);


                                                    PRRespsonse objPRRespsonse = new PRRespsonse();
                                                    string jsonconnPR = responsePR.Content;
                                                    objPRRespsonse = JsonConvert.DeserializeObject<PRRespsonse>(jsonconnPR);

                                                    if (responsePR.StatusCode == HttpStatusCode.OK)
                                                    {
                                                        hfPRNo.Value = Convert.ToString(objMainClass.strConvertZeroPadding(objPRRespsonse.PRNO));

                                                        bool blnPOExist = false;
                                                        DataTable dtPO = new DataTable();
                                                        DataTable dtSO = new DataTable();
                                                        dtPO = objMainClass.CheckPOSOImeiNo(objMainClass.intCmpId, txtIMEI.Text, "CHECKPOWITHRETURN");
                                                        if (dtPO.Rows.Count > 0)
                                                        {
                                                            if (Convert.ToString(dtPO.Rows[0]["LASTPO"]) != "" && Convert.ToString(dtPO.Rows[0]["LASTPO"]) != string.Empty &&
                                                                Convert.ToString(dtPO.Rows[0]["LASTPO"]) != null &&
                                                                Convert.ToInt32(Convert.ToString(dtPO.Rows[0]["BALQTY"]) == "" ? 0 : Convert.ToInt32(dtPO.Rows[0]["BALQTY"])) > 0)
                                                            {
                                                                blnPOExist = true;


                                                                dtSO = objMainClass.CheckPOSOImeiNo(objMainClass.intCmpId, txtIMEI.Text, "CHECKSOWITHRETURN");

                                                                if (dtSO.Rows.Count > 0)
                                                                {
                                                                    if (Convert.ToString(dtSO.Rows[0]["LASTSO"]) != "" && Convert.ToString(dtSO.Rows[0]["LASTSO"]) != string.Empty &&
                                                                Convert.ToString(dtSO.Rows[0]["LASTSO"]) != null &&
                                                                Convert.ToInt32(Convert.ToString(dtSO.Rows[0]["BALQTY"]) == "" ? 0 : Convert.ToInt32(dtSO.Rows[0]["BALQTY"])) > 0)
                                                                    {
                                                                        blnPOExist = false;
                                                                    }

                                                                }

                                                            }
                                                        }

                                                        if (blnPOExist == true)
                                                        {
                                                            string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                            if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                            {
                                                                message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                            }
                                                            else
                                                            {
                                                                message = message + " New Job card not Created.";
                                                            }

                                                            if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                            {
                                                                message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                            }
                                                            else
                                                            {
                                                                message = message + " New Estimate not Created.";
                                                            }
                                                            message = message + " Job Specification created successfully. New PR Created PR NO. is : " + objPRRespsonse.PRNO + ". PO not created because PO is already made for same IMEI No. PO No. : " + Convert.ToString(dtPO.Rows[0]["LASTPO"]) + ".!";

                                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                        }
                                                        else
                                                        {

                                                            DataTable dtPOAPI = new DataTable();
                                                            dtPOAPI = objMainClass.GetWAData("INSERTPO", 1, "GETWADATA");
                                                            if (dtPOAPI.Rows.Count > 0)
                                                            {
                                                                List<POCharges> objPOCharges = new List<POCharges>();
                                                                if (othchg > 0)
                                                                {
                                                                    objPOCharges.Add(new POCharges
                                                                    {
                                                                        CHGAMT = othbasechg,
                                                                        CHGTYPE = ddlCharges.SelectedValue,
                                                                        CMPID = objMainClass.intCmpId,
                                                                        CONDID = 12,
                                                                        CONDTYPE = "GST",
                                                                        GLCODE = "",
                                                                        OPERATOR = "+",
                                                                        PID = 0,
                                                                        PONO = "",
                                                                        RATE = 18,
                                                                        SRNO = 1,
                                                                        TAXAMT = othchgtax,
                                                                    });
                                                                }




                                                                List<POTaxation> objPOTaxation = new List<POTaxation>();
                                                                objPOTaxation.Add(new POTaxation
                                                                {
                                                                    BASEAMT = basprice,
                                                                    CMPID = objMainClass.intCmpId,
                                                                    CONDID = 12,
                                                                    CONDORDER = 1,
                                                                    CONDTYPE = "GST",
                                                                    GLCODE = "",
                                                                    OPERATOR = "+",
                                                                    PID = 0,
                                                                    PONO = "",
                                                                    POSRNO = 1,
                                                                    RATE = 18,
                                                                    SRNO = 1,
                                                                    TAXAMT = tax,
                                                                });



                                                                List<PODetails> objPODetails = new List<PODetails>();
                                                                objPODetails.Add(new PODetails
                                                                {
                                                                    APRVBY = 17,
                                                                    APRVDATE = DateTime.Now,
                                                                    APRVSTATUS = 260,
                                                                    ASSETCD = "",
                                                                    BRATE = basprice,
                                                                    CAMOUNT = basprice,
                                                                    CMPID = objMainClass.intCmpId,
                                                                    CSTCENTCD = costcenter,
                                                                    DELIDT = DateTime.Now,
                                                                    DEVREASON = "OK",
                                                                    DISCAMT = 0,
                                                                    FROMLOCCD = "",
                                                                    FROMPLANTCD = "",
                                                                    GLCD = "10010000",
                                                                    IMEINO = txtIMEI.Text,
                                                                    ITEMDESC = hfItemDesc.Value,
                                                                    ITEMGRPID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMGRP"]),
                                                                    ITEMID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]),
                                                                    //ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",  //Removed AUTO PO AGAINST PO text from remarks
                                                                    ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text,
                                                                    LOCCD = hfLocCd.Value,
                                                                    LOCKAMT = 0,
                                                                    PLANTCD = hfPlantCd.Value,
                                                                    PONO = "",
                                                                    POQTY = 1,
                                                                    PRFCNT = "1000",
                                                                    PRNO = hfPRNo.Value,
                                                                    PRSRNO = 1,
                                                                    RATE = purprice,
                                                                    REFNO = "",
                                                                    REJREASON = "",
                                                                    SRNO = 1,
                                                                    TAXAMT = 0,
                                                                    TRNUM = hfNewJobID.Value,
                                                                    UOM = Convert.ToInt32(dtItemDesc.Rows[0]["UOM"]),
                                                                });



                                                                POMaster objPOMaster = new POMaster();
                                                                objPOMaster.ADJAMT = 0;
                                                                objPOMaster.ADVAMT = 0;
                                                                objPOMaster.AGENTNAME = "";
                                                                objPOMaster.APRVBY = 17;
                                                                objPOMaster.APRVDATE = DateTime.Now;
                                                                objPOMaster.CMPID = objMainClass.intCmpId;
                                                                objPOMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                objPOMaster.CREATEDATE = DateTime.Now;
                                                                objPOMaster.DEPTID = 1;
                                                                objPOMaster.DISCOUNT = 0;
                                                                objPOMaster.NETMATVALUE = basprice;
                                                                objPOMaster.NETPOAMT = netpoamt;
                                                                objPOMaster.NETTAXAMT = nettaxamt;
                                                                objPOMaster.OLDPOAMT = 0;
                                                                objPOMaster.PENDINGAMT = netpoamt;
                                                                objPOMaster.PMTTERMS = ddlPaymentTerms.SelectedItem.Text;
                                                                objPOMaster.PMTTERMSDESC = txtPaymentTermsDesc.Text;
                                                                objPOMaster.POCharge = objPOCharges;
                                                                objPOMaster.PODetail = objPODetails;
                                                                objPOMaster.PODT = DateTime.Now;
                                                                objPOMaster.PONO = "";
                                                                objPOMaster.POTax = objPOTaxation;
                                                                objPOMaster.POTYPE = "MPO";
                                                                objPOMaster.PURTYPE = Convert.ToInt32(ddlPurType.SelectedValue);
                                                                objPOMaster.REMARK = "AUTO PR/PO AGAINST SO";
                                                                objPOMaster.STATUS = 57;
                                                                objPOMaster.TRANCODE = txtTranCode.Text;
                                                                objPOMaster.VENDCODE = txtVendorCode.Text;




                                                                string POURL = Convert.ToString(dtPOAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtPOAPI.Rows[0]["TOKEN"]);
                                                                var clientPO = new RestClient(POURL);
                                                                clientPO.Timeout = -1;
                                                                var requestPO = new RestRequest(Method.POST);
                                                                requestPO.AddHeader("" + Convert.ToString(dtPOAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtPOAPI.Rows[0]["KEYVALUE"]) + "");
                                                                requestPO.AddHeader("Content-Type", "application/json");
                                                                var jsonInputPO = JsonConvert.SerializeObject(objPOMaster);
                                                                requestPO.AddParameter("application/json", jsonInputPO, ParameterType.RequestBody);
                                                                IRestResponse responsePO = clientPO.Execute(requestPO);


                                                                PORespsonse objPORespsonse = new PORespsonse();
                                                                string jsonconnPO = responsePO.Content;
                                                                objPORespsonse = JsonConvert.DeserializeObject<PORespsonse>(jsonconnPO);

                                                                if (responsePO.StatusCode == HttpStatusCode.OK)
                                                                {
                                                                    hfPONo.Value = objPORespsonse.PONO;

                                                                    #region GRN CODE...

                                                                    DataTable dtGRNAPI = new DataTable();
                                                                    dtGRNAPI = objMainClass.GetWAData("CREATEGRN", 1, "GETWADATA");

                                                                    if (dtGRNAPI.Rows.Count > 0)
                                                                    {
                                                                        List<GRNDetials> objGRNDetials = new List<GRNDetials>();
                                                                        objGRNDetials.Add(new GRNDetials
                                                                        {
                                                                            ASSETCD = "",
                                                                            CAMOUNT = basprice,
                                                                            CHLNQTY = 1,
                                                                            CMPID = objMainClass.intCmpId,
                                                                            CSTCENTCD = costcenter,
                                                                            DOCNO = "",
                                                                            DOCTYPE = "103",
                                                                            FINYEAR = DateTime.Now.Year,
                                                                            GLCD = "10010000",
                                                                            ITEMDESC = hfItemDesc.Value,
                                                                            ITEMGRPID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMGRP"]),
                                                                            ITEMID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]),
                                                                            //ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",  //Removed AUTO PO AGAINST PO text from remarks
                                                                            ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text,
                                                                            LOCCD = hfLocCd.Value,
                                                                            PLANTCD = hfPlantCd.Value,
                                                                            PONO = hfPONo.Value,
                                                                            POSRNO = 1,
                                                                            PRFCNT = "1000",
                                                                            QTY = 1,
                                                                            RATE = basprice,
                                                                            SRNO = 1,
                                                                            TRACKNO = hfNewJobID.Value,
                                                                            UOM = Convert.ToInt32(dtItemDesc.Rows[0]["UOM"]),
                                                                        });

                                                                        GRNMaster objGRNMaster = new GRNMaster();
                                                                        objGRNMaster.CHLNDT = DateTime.Now;
                                                                        objGRNMaster.CHLNNO = hfPRNo.Value;
                                                                        objGRNMaster.CMPID = objMainClass.intCmpId;
                                                                        objGRNMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                        objGRNMaster.DEPTCD = 0;
                                                                        objGRNMaster.DOCDATE = DateTime.Now;
                                                                        objGRNMaster.DOCNO = "";
                                                                        objGRNMaster.DOCTYPE = "103";
                                                                        objGRNMaster.EMPNAME = "";
                                                                        objGRNMaster.FINYEAR = DateTime.Now.Year;
                                                                        objGRNMaster.GRNData = objGRNDetials;
                                                                        objGRNMaster.POSTDATE = DateTime.Now;
                                                                        objGRNMaster.REFDOCNO = hfPONo.Value;
                                                                        objGRNMaster.REFDOCYEAR = Convert.ToInt32(txtVendorCode.Text);
                                                                        objGRNMaster.REFNO = hfPRNo.Value;
                                                                        objGRNMaster.REMARK = "AUTO PR/PO AGAINST SO";
                                                                        objGRNMaster.TRANCODE = txtTranCode.Text;


                                                                        string GRNURL = Convert.ToString(dtGRNAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtGRNAPI.Rows[0]["TOKEN"]);
                                                                        var clientGRN = new RestClient(GRNURL);
                                                                        clientGRN.Timeout = -1;
                                                                        var requestGRN = new RestRequest(Method.POST);
                                                                        requestGRN.AddHeader("" + Convert.ToString(dtGRNAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtGRNAPI.Rows[0]["KEYVALUE"]) + "");
                                                                        requestGRN.AddHeader("Content-Type", "application/json");
                                                                        var jsonInputGRN = JsonConvert.SerializeObject(objGRNMaster);
                                                                        requestGRN.AddParameter("application/json", jsonInputGRN, ParameterType.RequestBody);
                                                                        IRestResponse responseGRN = clientGRN.Execute(requestGRN);


                                                                        GRNRespsonse objGRNRespsonse = new GRNRespsonse();
                                                                        string jsonconnGRN = responseGRN.Content;
                                                                        objGRNRespsonse = JsonConvert.DeserializeObject<GRNRespsonse>(jsonconnGRN);

                                                                        if (responseGRN.StatusCode == HttpStatusCode.OK)
                                                                        {
                                                                            hfGRNNo.Value = objGRNRespsonse.GRNNO;

                                                                            #region PB Code...

                                                                            DataTable dtPBAPI = new DataTable();
                                                                            dtPBAPI = objMainClass.GetWAData("INSERTPB", 1, "GETWADATA");
                                                                            if (dtPBAPI.Rows.Count > 0)
                                                                            {
                                                                                List<PBCharges> objPBCharges = new List<PBCharges>();
                                                                                if (othchg > 0)
                                                                                {
                                                                                    objPBCharges.Add(new PBCharges
                                                                                    {
                                                                                        CHGAMT = othbasechg,
                                                                                        CHGTYPE = ddlCharges.SelectedValue,
                                                                                        CMPID = objMainClass.intCmpId,
                                                                                        CONDID = 12,
                                                                                        CONDTYPE = "GST",
                                                                                        GLCODE = "",
                                                                                        OPERATOR = "+",
                                                                                        PBNO = "",
                                                                                        PID = 0,
                                                                                        RATE = 18,
                                                                                        SRNO = 1,
                                                                                        TAXAMT = othchgtax,
                                                                                    });
                                                                                }

                                                                                List<PBTaxation> objPBTaxation = new List<PBTaxation>();
                                                                                objPBTaxation.Add(new PBTaxation
                                                                                {
                                                                                    BASEAMT = basprice,
                                                                                    CMPID = objMainClass.intCmpId,
                                                                                    CONDID = 12,
                                                                                    CONDORDER = 1,
                                                                                    CONDTYPE = "GST",
                                                                                    GLCODE = "",
                                                                                    OPERATOR = "+",
                                                                                    PBNO = "",
                                                                                    PBSRNO = 1,
                                                                                    PID = 0,
                                                                                    RATE = 18,
                                                                                    SRNO = 1,
                                                                                    TAXAMT = tax,
                                                                                });

                                                                                List<PBDetails> objPBDetails = new List<PBDetails>();
                                                                                objPBDetails.Add(new PBDetails
                                                                                {
                                                                                    ASSETCD = "",
                                                                                    BRATE = basprice,
                                                                                    CAMOUNT = basprice,
                                                                                    CHALLANNO = "",
                                                                                    CMPID = objMainClass.intCmpId,
                                                                                    CSTCENTCD = costcenter,
                                                                                    DISCAMT = 0,
                                                                                    GLCD = "10010000",
                                                                                    ITEMDESC = hfItemDesc.Value,
                                                                                    ITEMGRPID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMGRP"]),
                                                                                    ITEMID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]),
                                                                                    //ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",  //Removed AUTO PO AGAINST PO text from remarks
                                                                                    ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text,
                                                                                    LOCCD = hfLocCd.Value,
                                                                                    MIRNO = hfGRNNo.Value,
                                                                                    MIRSRNO = 1,
                                                                                    PBNO = "",
                                                                                    PBQTY = 1,
                                                                                    PLANTCD = hfPlantCd.Value,
                                                                                    PONO = hfPONo.Value,
                                                                                    POSRNO = 1,
                                                                                    PRFCNT = "1000",
                                                                                    RATE = purprice,
                                                                                    REFNO = "",
                                                                                    SRNO = 1,
                                                                                    TAXAMT = 0,
                                                                                    TRNUM = hfNewJobID.Value,
                                                                                    UOM = Convert.ToInt32(dtItemDesc.Rows[0]["UOM"]),
                                                                                });

                                                                                PBMaster objPBMaster = new PBMaster();
                                                                                objPBMaster.ADJAMT = 0;
                                                                                objPBMaster.BILLAMT = netpoamt;
                                                                                objPBMaster.BILLDT = Convert.ToDateTime(txtBillDate.Text);
                                                                                if (chkINVPO.Checked == true)
                                                                                {
                                                                                    objPBMaster.BILLNO = hfPONo.Value;
                                                                                }
                                                                                else
                                                                                {
                                                                                    objPBMaster.BILLNO = txtBillNo.Text;
                                                                                }

                                                                                objPBMaster.CMPID = objMainClass.intCmpId;
                                                                                objPBMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                                objPBMaster.DISCOUNT = 0;
                                                                                objPBMaster.NETMATVALUE = basprice;
                                                                                objPBMaster.NETPBAMT = netpoamt;
                                                                                objPBMaster.NETTAXAMT = nettaxamt;
                                                                                objPBMaster.PAIDAMT = 0;
                                                                                objPBMaster.PBCharge = objPBCharges;
                                                                                objPBMaster.PBDetail = objPBDetails;
                                                                                objPBMaster.PBDT = DateTime.Now;
                                                                                objPBMaster.PBNO = "";
                                                                                objPBMaster.PBTax = objPBTaxation;
                                                                                objPBMaster.PBTYPE = "MPB";
                                                                                objPBMaster.PENDINGAMT = netpoamt;
                                                                                objPBMaster.PMTTERMS = ddlPaymentTerms.SelectedItem.Text;
                                                                                objPBMaster.PMTTERMSDESC = txtPaymentTermsDesc.Text;
                                                                                objPBMaster.REMARK = "AUTO PR/PO AGAINST SO";
                                                                                objPBMaster.STATUS = 1;
                                                                                objPBMaster.VENDCODE = txtVendorCode.Text;


                                                                                string PBURL = Convert.ToString(dtPBAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtPBAPI.Rows[0]["TOKEN"]);
                                                                                var clientPB = new RestClient(PBURL);
                                                                                clientPB.Timeout = -1;
                                                                                var requestPB = new RestRequest(Method.POST);
                                                                                requestPB.AddHeader("" + Convert.ToString(dtPBAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtPBAPI.Rows[0]["KEYVALUE"]) + "");
                                                                                requestPB.AddHeader("Content-Type", "application/json");
                                                                                var jsonInputPB = JsonConvert.SerializeObject(objPBMaster);
                                                                                requestPB.AddParameter("application/json", jsonInputPB, ParameterType.RequestBody);
                                                                                IRestResponse responsePB = clientPB.Execute(requestPB);

                                                                                PBRespsonse objPBRespsonse = new PBRespsonse();
                                                                                string jsonconnPB = responsePB.Content;
                                                                                objPBRespsonse = JsonConvert.DeserializeObject<PBRespsonse>(jsonconnPB);

                                                                                if (responsePB.StatusCode == HttpStatusCode.OK)
                                                                                {
                                                                                    hfPBNo.Value = objPBRespsonse.PBNO;

                                                                                    string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                                    if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                                    {
                                                                                        message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        message = message + " New Job card not Created.";
                                                                                    }

                                                                                    if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                                    {
                                                                                        message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        message = message + " New Estimate not Created.";
                                                                                    }
                                                                                    message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                                    message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                                    message = message + " New GRN Created. GRN No. is :" + hfGRNNo.Value + ".!";
                                                                                    message = message + " New PB Created. PB No. is :" + hfPBNo.Value + ".!";

                                                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                                }
                                                                                else
                                                                                {
                                                                                    string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                                    if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                                    {
                                                                                        message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        message = message + " New Job card not Created.";
                                                                                    }

                                                                                    if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                                    {
                                                                                        message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        message = message + " New Estimate not Created.";
                                                                                    }
                                                                                    message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                                    message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                                    message = message + " New GRN Created. GRN No. is :" + objGRNRespsonse.GRNNO + ".";
                                                                                    message = message + " PB not Created." + objPBRespsonse.MESSAGE + ".!";

                                                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                                }

                                                                            }
                                                                            else
                                                                            {
                                                                                string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                                if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                                {
                                                                                    message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                                }
                                                                                else
                                                                                {
                                                                                    message = message + " New Job card not Created.";
                                                                                }

                                                                                if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                                {
                                                                                    message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                                }
                                                                                else
                                                                                {
                                                                                    message = message + " New Estimate not Created.";
                                                                                }
                                                                                message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                                message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                                message = message + " New GRN Created. GRN No. is :" + objGRNRespsonse.GRNNO + ".";
                                                                                message = message + " But PB API not found. Please contact to API provider.!";

                                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                            }

                                                                            #endregion

                                                                        }
                                                                        else
                                                                        {
                                                                            string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                            if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                            {
                                                                                message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                            }
                                                                            else
                                                                            {
                                                                                message = message + " New Job card not Created.";
                                                                            }

                                                                            if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                            {
                                                                                message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                            }
                                                                            else
                                                                            {
                                                                                message = message + " New Estimate not Created.";
                                                                            }
                                                                            message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                            message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                            message = message + " GRN not Created." + objGRNRespsonse.MESSAGE + ".!";

                                                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                        if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                        {
                                                                            message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                        }
                                                                        else
                                                                        {
                                                                            message = message + " New Job card not Created.";
                                                                        }

                                                                        if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                        {
                                                                            message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                        }
                                                                        else
                                                                        {
                                                                            message = message + " New Estimate not Created.";
                                                                        }
                                                                        message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                        message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                        message = message + " But GRN API not found. Please contact API provider.!";

                                                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                    }


                                                                    #endregion


                                                                }
                                                                else
                                                                {
                                                                    string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                    if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                    {
                                                                        message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                    }
                                                                    else
                                                                    {
                                                                        message = message + " New Job card not Created.";
                                                                    }

                                                                    if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                    {
                                                                        message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                    }
                                                                    else
                                                                    {
                                                                        message = message + " New Estimate not Created.";
                                                                    }
                                                                    message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ". PO not created. " + objPORespsonse.MESSAGE + ".!";

                                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                }


                                                            }
                                                            else
                                                            {
                                                                string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                {
                                                                    message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                }
                                                                else
                                                                {
                                                                    message = message + " New Job card not Created.";
                                                                }

                                                                if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                {
                                                                    message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                }
                                                                else
                                                                {
                                                                    message = message + " New Estimate not Created.";
                                                                }
                                                                message = message + " Job Specification created successfully. New PR Created PR NO. is : " + objPRRespsonse.PRNO + ". PO not created because PO API not found. Please contact API provider.!";

                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                            }



                                                        }

                                                    }
                                                    else
                                                    {
                                                        string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                        if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                        {
                                                            message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                        }
                                                        else
                                                        {
                                                            message = message + " New Job card not Created.";
                                                        }

                                                        if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                        {
                                                            message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                        }
                                                        else
                                                        {
                                                            message = message + " New Estimate not Created.";
                                                        }
                                                        message = message + " Job Specification created successfully. " + objPRRespsonse.MESSAGE + ".!";

                                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                    }

                                                    #endregion


                                                }
                                                else
                                                {
                                                    decimal purcomm = 0;
                                                    decimal purprice = 0;
                                                    decimal basprice = 0;
                                                    decimal tax = 0;

                                                    decimal othchg = 0;
                                                    decimal othbasechg = 0;
                                                    decimal othchgtax = 0;
                                                    decimal nettaxamt = 0;
                                                    decimal netpoamt = 0;

                                                    purcomm = Math.Round((Convert.ToDecimal(hfLockAmt.Value) * (RATE) / 100), 2);
                                                    purprice = Math.Round((Convert.ToDecimal(hfLockAmt.Value) + Convert.ToDecimal(purcomm)), 2);
                                                    basprice = Math.Round((Convert.ToDecimal(purprice / Convert.ToDecimal("1.18"))), 2);
                                                    tax = Math.Round((purprice - basprice), 2);

                                                    //nettaxamt = tax;
                                                    netpoamt = Math.Round((purprice), 2);

                                                    if (txtChgAmt.Text != null && txtChgAmt.Text != string.Empty && txtChgAmt.Text != null)
                                                    {
                                                        if (Convert.ToDecimal(txtChgAmt.Text) > 0)
                                                        {
                                                            othchg = Math.Round(Convert.ToDecimal(txtChgAmt.Text), 2);
                                                            othbasechg = Math.Round((Convert.ToDecimal(othchg / Convert.ToDecimal("1.18"))), 2);
                                                            othchgtax = Math.Round((othchg - othbasechg), 2);
                                                            //nettaxamt = nettaxamt + othchgtax;
                                                            netpoamt = Math.Round((netpoamt + othchg), 2);
                                                        }
                                                    }

                                                    List<PRDetails> objPRDetails = new List<PRDetails>();
                                                    objPRDetails.Add(new PRDetails
                                                    {
                                                        ASSETCD = "",
                                                        CAMOUNT = purprice,
                                                        CMPID = objMainClass.intCmpId,
                                                        CSTCENTCD = costcenter,
                                                        DELIDT = DateTime.Now,
                                                        GLCD = "10010000",
                                                        IMEINO = txtIMEI.Text,
                                                        ITEMDESC = hfItemDesc.Value,
                                                        ITEMGRPID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMGRP"]),
                                                        ITEMID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]),
                                                        ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",
                                                        LOCCD = hfLocCd.Value,
                                                        PARTREQNO = 0,
                                                        PLANTCD = hfPlantCd.Value,
                                                        PRBY = "",
                                                        PRFCNT = "1000",
                                                        PRNO = "",
                                                        PRQTY = 1,
                                                        RATE = purprice,
                                                        SRNO = 1,
                                                        STATUS = 2,
                                                        TRNUM = hfNewJobID.Value,
                                                        UOM = Convert.ToInt32(dtItemDesc.Rows[0]["UOM"]),
                                                    });

                                                    #region Inser PR Method...




                                                    PRMaster objPRMaster = new PRMaster();
                                                    objPRMaster.CMPID = objMainClass.intCmpId;
                                                    objPRMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                    objPRMaster.CREATEDATE = DateTime.Now;
                                                    objPRMaster.DEPTID = 1;
                                                    objPRMaster.ISPRSTO = 0;
                                                    objPRMaster.LISTINGID = 0;
                                                    objPRMaster.PRDT = DateTime.Now;
                                                    objPRMaster.PRNO = "";
                                                    objPRMaster.PRTYPE = "MPR";
                                                    objPRMaster.REMARK = "AUTO PR/PO AGAINST SO";
                                                    objPRMaster.STATUS = 57;
                                                    objPRMaster.VENDCODE = "";
                                                    objPRMaster.PRDATA = objPRDetails;


                                                    string PRURL = Convert.ToString(dtPRAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtPRAPI.Rows[0]["TOKEN"]);
                                                    var clientPR = new RestClient(PRURL);
                                                    clientPR.Timeout = -1;
                                                    var requestPR = new RestRequest(Method.POST);
                                                    requestPR.AddHeader("" + Convert.ToString(dtPRAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtPRAPI.Rows[0]["KEYVALUE"]) + "");
                                                    requestPR.AddHeader("Content-Type", "application/json");
                                                    var jsonInputPR = JsonConvert.SerializeObject(objPRMaster);
                                                    requestPR.AddParameter("application/json", jsonInputPR, ParameterType.RequestBody);
                                                    IRestResponse responsePR = clientPR.Execute(requestPR);


                                                    PRRespsonse objPRRespsonse = new PRRespsonse();
                                                    string jsonconnPR = responsePR.Content;
                                                    objPRRespsonse = JsonConvert.DeserializeObject<PRRespsonse>(jsonconnPR);

                                                    if (responsePR.StatusCode == HttpStatusCode.OK)
                                                    {
                                                        hfPRNo.Value = Convert.ToString(objMainClass.strConvertZeroPadding(objPRRespsonse.PRNO));

                                                        bool blnPOExist = false;
                                                        DataTable dtPO = new DataTable();
                                                        DataTable dtSO = new DataTable();
                                                        dtPO = objMainClass.CheckPOSOImeiNo(objMainClass.intCmpId, txtIMEI.Text, "CHECKPOWITHRETURN");
                                                        if (dtPO.Rows.Count > 0)
                                                        {
                                                            if (Convert.ToString(dtPO.Rows[0]["LASTPO"]) != "" && Convert.ToString(dtPO.Rows[0]["LASTPO"]) != string.Empty &&
                                                                Convert.ToString(dtPO.Rows[0]["LASTPO"]) != null &&
                                                                Convert.ToInt32(Convert.ToString(dtPO.Rows[0]["BALQTY"]) == "" ? 0 : Convert.ToInt32(dtPO.Rows[0]["BALQTY"])) > 0)
                                                            {
                                                                blnPOExist = true;


                                                                dtSO = objMainClass.CheckPOSOImeiNo(objMainClass.intCmpId, txtIMEI.Text, "CHECKSOWITHRETURN");

                                                                if (dtSO.Rows.Count > 0)
                                                                {
                                                                    if (Convert.ToString(dtSO.Rows[0]["LASTSO"]) != "" && Convert.ToString(dtSO.Rows[0]["LASTSO"]) != string.Empty &&
                                                                Convert.ToString(dtSO.Rows[0]["LASTSO"]) != null &&
                                                                Convert.ToInt32(Convert.ToString(dtSO.Rows[0]["BALQTY"]) == "" ? 0 : Convert.ToInt32(dtSO.Rows[0]["BALQTY"])) > 0)
                                                                    {
                                                                        blnPOExist = false;
                                                                    }

                                                                }

                                                            }
                                                        }

                                                        if (blnPOExist == true)
                                                        {
                                                            string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                            if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                            {
                                                                message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                            }
                                                            else
                                                            {
                                                                message = message + " New Job card not Created.";
                                                            }

                                                            if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                            {
                                                                message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                            }
                                                            else
                                                            {
                                                                message = message + " New Estimate not Created.";
                                                            }
                                                            message = message + " Job Specification created successfully. New PR Created PR NO. is : " + objPRRespsonse.PRNO + ". PO not created because PO is already made for same IMEI No.! PO No. : " + Convert.ToString(dtPO.Rows[0]["LASTPO"]) + ".!";

                                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                        }
                                                        else
                                                        {

                                                            DataTable dtPOAPI = new DataTable();
                                                            dtPOAPI = objMainClass.GetWAData("INSERTPO", 1, "GETWADATA");
                                                            if (dtPOAPI.Rows.Count > 0)
                                                            {
                                                                List<POCharges> objPOCharges = new List<POCharges>();
                                                                if (othchg > 0)
                                                                {
                                                                    objPOCharges.Add(new POCharges
                                                                    {
                                                                        CHGAMT = othchg,
                                                                        CHGTYPE = ddlCharges.SelectedValue,
                                                                        CMPID = objMainClass.intCmpId,
                                                                        CONDID = 0,
                                                                        CONDTYPE = "",
                                                                        GLCODE = "",
                                                                        OPERATOR = "+",
                                                                        PID = 0,
                                                                        PONO = "",
                                                                        RATE = 0,
                                                                        SRNO = 1,
                                                                        TAXAMT = 0,
                                                                    });
                                                                }

                                                                List<POTaxation> objPOTaxation = new List<POTaxation>();
                                                                //objPOTaxation.Add(new POTaxation
                                                                //{
                                                                //    BASEAMT = basprice,
                                                                //    CMPID = objMainClass.intCmpId,
                                                                //    CONDID = 12,
                                                                //    CONDORDER = 1,
                                                                //    CONDTYPE = "GST",
                                                                //    GLCODE = "",
                                                                //    OPERATOR = "+",
                                                                //    PID = 0,
                                                                //    PONO = "",
                                                                //    POSRNO = 1,
                                                                //    RATE = 18,
                                                                //    SRNO = 1,
                                                                //    TAXAMT = tax,
                                                                //});

                                                                List<PODetails> objPODetails = new List<PODetails>();
                                                                objPODetails.Add(new PODetails
                                                                {
                                                                    APRVBY = 17,
                                                                    APRVDATE = DateTime.Now,
                                                                    APRVSTATUS = 260,
                                                                    ASSETCD = "",
                                                                    BRATE = purprice,
                                                                    CAMOUNT = purprice,
                                                                    CMPID = objMainClass.intCmpId,
                                                                    CSTCENTCD = costcenter,
                                                                    DELIDT = DateTime.Now,
                                                                    DEVREASON = "OK",
                                                                    DISCAMT = 0,
                                                                    FROMLOCCD = "",
                                                                    FROMPLANTCD = "",
                                                                    GLCD = "10010000",
                                                                    IMEINO = txtIMEI.Text,
                                                                    ITEMDESC = hfItemDesc.Value,
                                                                    ITEMGRPID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMGRP"]),
                                                                    ITEMID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]),
                                                                    //ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",  //Removed AUTO PO AGAINST PO text from remarks
                                                                    ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text,
                                                                    LOCCD = hfLocCd.Value,
                                                                    LOCKAMT = 0,
                                                                    PLANTCD = hfPlantCd.Value,
                                                                    PONO = "",
                                                                    POQTY = 1,
                                                                    PRFCNT = "1000",
                                                                    PRNO = hfPRNo.Value,
                                                                    PRSRNO = 1,
                                                                    RATE = purprice,
                                                                    REFNO = "",
                                                                    REJREASON = "",
                                                                    SRNO = 1,
                                                                    TAXAMT = 0,
                                                                    TRNUM = hfNewJobID.Value,
                                                                    UOM = Convert.ToInt32(dtItemDesc.Rows[0]["UOM"]),
                                                                });

                                                                POMaster objPOMaster = new POMaster();
                                                                objPOMaster.ADJAMT = 0;
                                                                objPOMaster.ADVAMT = 0;
                                                                objPOMaster.AGENTNAME = "";
                                                                objPOMaster.APRVBY = 17;
                                                                objPOMaster.APRVDATE = DateTime.Now;
                                                                objPOMaster.CMPID = objMainClass.intCmpId;
                                                                objPOMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                objPOMaster.CREATEDATE = DateTime.Now;
                                                                objPOMaster.DEPTID = 1;
                                                                objPOMaster.DISCOUNT = 0;
                                                                objPOMaster.NETMATVALUE = purprice;
                                                                objPOMaster.NETPOAMT = netpoamt;
                                                                objPOMaster.NETTAXAMT = nettaxamt;
                                                                objPOMaster.OLDPOAMT = 0;
                                                                objPOMaster.PENDINGAMT = netpoamt;
                                                                objPOMaster.PMTTERMS = ddlPaymentTerms.SelectedItem.Text;
                                                                objPOMaster.PMTTERMSDESC = txtPaymentTermsDesc.Text;
                                                                objPOMaster.POCharge = objPOCharges;
                                                                objPOMaster.PODetail = objPODetails;
                                                                objPOMaster.PODT = DateTime.Now;
                                                                objPOMaster.PONO = "";
                                                                objPOMaster.POTax = objPOTaxation;
                                                                objPOMaster.POTYPE = "MPO";
                                                                objPOMaster.PURTYPE = Convert.ToInt32(ddlPurType.SelectedValue);
                                                                objPOMaster.REMARK = "AUTO PR/PO AGAINST SO";
                                                                objPOMaster.STATUS = 57;
                                                                objPOMaster.TRANCODE = txtTranCode.Text;
                                                                objPOMaster.VENDCODE = txtVendorCode.Text;


                                                                string POURL = Convert.ToString(dtPOAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtPOAPI.Rows[0]["TOKEN"]);
                                                                var clientPO = new RestClient(POURL);
                                                                clientPO.Timeout = -1;
                                                                var requestPO = new RestRequest(Method.POST);
                                                                requestPO.AddHeader("" + Convert.ToString(dtPOAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtPOAPI.Rows[0]["KEYVALUE"]) + "");
                                                                requestPO.AddHeader("Content-Type", "application/json");
                                                                var jsonInputPO = JsonConvert.SerializeObject(objPOMaster);
                                                                requestPO.AddParameter("application/json", jsonInputPO, ParameterType.RequestBody);
                                                                IRestResponse responsePO = clientPO.Execute(requestPO);


                                                                PORespsonse objPORespsonse = new PORespsonse();
                                                                string jsonconnPO = responsePO.Content;
                                                                objPORespsonse = JsonConvert.DeserializeObject<PORespsonse>(jsonconnPO);

                                                                if (responsePO.StatusCode == HttpStatusCode.OK)
                                                                {
                                                                    hfPONo.Value = objPORespsonse.PONO;

                                                                    #region GRN CODE...

                                                                    DataTable dtGRNAPI = new DataTable();
                                                                    dtGRNAPI = objMainClass.GetWAData("CREATEGRN", 1, "GETWADATA");

                                                                    if (dtGRNAPI.Rows.Count > 0)
                                                                    {
                                                                        List<GRNDetials> objGRNDetials = new List<GRNDetials>();
                                                                        objGRNDetials.Add(new GRNDetials
                                                                        {
                                                                            ASSETCD = "",
                                                                            CAMOUNT = basprice,
                                                                            CHLNQTY = 1,
                                                                            CMPID = objMainClass.intCmpId,
                                                                            CSTCENTCD = costcenter,
                                                                            DOCNO = "",
                                                                            DOCTYPE = "103",
                                                                            FINYEAR = DateTime.Now.Year,
                                                                            GLCD = "10010000",
                                                                            ITEMDESC = hfItemDesc.Value,
                                                                            ITEMGRPID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMGRP"]),
                                                                            ITEMID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]),
                                                                            //ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",  //Removed AUTO PO AGAINST PO text from remarks
                                                                            ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text,
                                                                            LOCCD = hfLocCd.Value,
                                                                            PLANTCD = hfPlantCd.Value,
                                                                            PONO = hfPONo.Value,
                                                                            POSRNO = 1,
                                                                            PRFCNT = "1000",
                                                                            QTY = 1,
                                                                            RATE = basprice,
                                                                            SRNO = 1,
                                                                            TRACKNO = hfNewJobID.Value,
                                                                            UOM = Convert.ToInt32(dtItemDesc.Rows[0]["UOM"]),
                                                                        });

                                                                        GRNMaster objGRNMaster = new GRNMaster();
                                                                        objGRNMaster.CHLNDT = DateTime.Now;
                                                                        objGRNMaster.CHLNNO = hfPRNo.Value;
                                                                        objGRNMaster.CMPID = objMainClass.intCmpId;
                                                                        objGRNMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                        objGRNMaster.DEPTCD = 0;
                                                                        objGRNMaster.DOCDATE = DateTime.Now;
                                                                        objGRNMaster.DOCNO = "";
                                                                        objGRNMaster.DOCTYPE = "103";
                                                                        objGRNMaster.EMPNAME = "";
                                                                        objGRNMaster.FINYEAR = DateTime.Now.Year;
                                                                        objGRNMaster.GRNData = objGRNDetials;
                                                                        objGRNMaster.POSTDATE = DateTime.Now;
                                                                        objGRNMaster.REFDOCNO = hfPONo.Value;
                                                                        objGRNMaster.REFDOCYEAR = Convert.ToInt32(txtVendorCode.Text);
                                                                        objGRNMaster.REFNO = hfPRNo.Value;
                                                                        objGRNMaster.REMARK = "AUTO PR/PO AGAINST SO";
                                                                        objGRNMaster.TRANCODE = txtTranCode.Text;


                                                                        string GRNURL = Convert.ToString(dtGRNAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtGRNAPI.Rows[0]["TOKEN"]);
                                                                        var clientGRN = new RestClient(GRNURL);
                                                                        clientGRN.Timeout = -1;
                                                                        var requestGRN = new RestRequest(Method.POST);
                                                                        requestGRN.AddHeader("" + Convert.ToString(dtGRNAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtGRNAPI.Rows[0]["KEYVALUE"]) + "");
                                                                        requestGRN.AddHeader("Content-Type", "application/json");
                                                                        var jsonInputGRN = JsonConvert.SerializeObject(objGRNMaster);
                                                                        requestGRN.AddParameter("application/json", jsonInputGRN, ParameterType.RequestBody);
                                                                        IRestResponse responseGRN = clientGRN.Execute(requestGRN);


                                                                        GRNRespsonse objGRNRespsonse = new GRNRespsonse();
                                                                        string jsonconnGRN = responseGRN.Content;
                                                                        objGRNRespsonse = JsonConvert.DeserializeObject<GRNRespsonse>(jsonconnGRN);

                                                                        if (responseGRN.StatusCode == HttpStatusCode.OK)
                                                                        {
                                                                            hfGRNNo.Value = objGRNRespsonse.GRNNO;

                                                                            #region PB Code...

                                                                            DataTable dtPBAPI = new DataTable();
                                                                            dtPBAPI = objMainClass.GetWAData("INSERTPB", 1, "GETWADATA");
                                                                            if (dtPBAPI.Rows.Count > 0)
                                                                            {
                                                                                List<PBCharges> objPBCharges = new List<PBCharges>();
                                                                                if (othchg > 0)
                                                                                {
                                                                                    objPBCharges.Add(new PBCharges
                                                                                    {
                                                                                        CHGAMT = othchg,
                                                                                        CHGTYPE = ddlCharges.SelectedValue,
                                                                                        CMPID = objMainClass.intCmpId,
                                                                                        CONDID = 12,
                                                                                        CONDTYPE = "GST",
                                                                                        GLCODE = "",
                                                                                        OPERATOR = "+",
                                                                                        PBNO = "",
                                                                                        PID = 0,
                                                                                        RATE = 18,
                                                                                        SRNO = 1,
                                                                                        TAXAMT = 0,
                                                                                    });
                                                                                }

                                                                                List<PBTaxation> objPBTaxation = new List<PBTaxation>();
                                                                                //objPBTaxation.Add(new PBTaxation
                                                                                //{
                                                                                //    BASEAMT = basprice,
                                                                                //    CMPID = objMainClass.intCmpId,
                                                                                //    CONDID = 12,
                                                                                //    CONDORDER = 1,
                                                                                //    CONDTYPE = "GST",
                                                                                //    GLCODE = "",
                                                                                //    OPERATOR = "+",
                                                                                //    PBNO = "",
                                                                                //    PBSRNO = 1,
                                                                                //    PID = 0,
                                                                                //    RATE = 18,
                                                                                //    SRNO = 1,
                                                                                //    TAXAMT = tax,
                                                                                //});

                                                                                List<PBDetails> objPBDetails = new List<PBDetails>();
                                                                                objPBDetails.Add(new PBDetails
                                                                                {
                                                                                    ASSETCD = "",
                                                                                    BRATE = purprice,
                                                                                    CAMOUNT = purprice,
                                                                                    CHALLANNO = "",
                                                                                    CMPID = objMainClass.intCmpId,
                                                                                    CSTCENTCD = costcenter,
                                                                                    DISCAMT = 0,
                                                                                    GLCD = "10010000",
                                                                                    ITEMDESC = hfItemDesc.Value,
                                                                                    ITEMGRPID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMGRP"]),
                                                                                    ITEMID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]),
                                                                                    //ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",  //Removed AUTO PO AGAINST PO text from remarks
                                                                                    ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text,
                                                                                    LOCCD = hfLocCd.Value,
                                                                                    MIRNO = hfGRNNo.Value,
                                                                                    MIRSRNO = 1,
                                                                                    PBNO = "",
                                                                                    PBQTY = 1,
                                                                                    PLANTCD = hfPlantCd.Value,
                                                                                    PONO = hfPONo.Value,
                                                                                    POSRNO = 1,
                                                                                    PRFCNT = "1000",
                                                                                    RATE = purprice,
                                                                                    REFNO = "",
                                                                                    SRNO = 1,
                                                                                    TAXAMT = 0,
                                                                                    TRNUM = hfNewJobID.Value,
                                                                                    UOM = Convert.ToInt32(dtItemDesc.Rows[0]["UOM"]),
                                                                                });

                                                                                PBMaster objPBMaster = new PBMaster();
                                                                                objPBMaster.ADJAMT = 0;
                                                                                objPBMaster.BILLAMT = netpoamt;
                                                                                objPBMaster.BILLDT = Convert.ToDateTime(txtBillDate.Text);
                                                                                if (chkINVPO.Checked == true)
                                                                                {
                                                                                    objPBMaster.BILLNO = hfPONo.Value;
                                                                                }
                                                                                else
                                                                                {
                                                                                    objPBMaster.BILLNO = txtBillNo.Text;
                                                                                }
                                                                                objPBMaster.CMPID = objMainClass.intCmpId;
                                                                                objPBMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                                objPBMaster.DISCOUNT = 0;
                                                                                objPBMaster.NETMATVALUE = purprice;
                                                                                objPBMaster.NETPBAMT = netpoamt;
                                                                                objPBMaster.NETTAXAMT = nettaxamt;
                                                                                objPBMaster.PAIDAMT = 0;
                                                                                objPBMaster.PBCharge = objPBCharges;
                                                                                objPBMaster.PBDetail = objPBDetails;
                                                                                objPBMaster.PBDT = DateTime.Now;
                                                                                objPBMaster.PBNO = "";
                                                                                objPBMaster.PBTax = objPBTaxation;
                                                                                objPBMaster.PBTYPE = "MPB";
                                                                                objPBMaster.PENDINGAMT = netpoamt;
                                                                                objPBMaster.PMTTERMS = ddlPaymentTerms.SelectedItem.Text;
                                                                                objPBMaster.PMTTERMSDESC = txtPaymentTermsDesc.Text;
                                                                                objPBMaster.REMARK = "AUTO PR/PO AGAINST SO";
                                                                                objPBMaster.STATUS = 1;
                                                                                objPBMaster.VENDCODE = txtVendorCode.Text;


                                                                                string PBURL = Convert.ToString(dtPBAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtPBAPI.Rows[0]["TOKEN"]);
                                                                                var clientPB = new RestClient(PBURL);
                                                                                clientPB.Timeout = -1;
                                                                                var requestPB = new RestRequest(Method.POST);
                                                                                requestPB.AddHeader("" + Convert.ToString(dtPBAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtPBAPI.Rows[0]["KEYVALUE"]) + "");
                                                                                requestPB.AddHeader("Content-Type", "application/json");
                                                                                var jsonInputPB = JsonConvert.SerializeObject(objPBMaster);
                                                                                requestPB.AddParameter("application/json", jsonInputPB, ParameterType.RequestBody);
                                                                                IRestResponse responsePB = clientPB.Execute(requestPB);

                                                                                PBRespsonse objPBRespsonse = new PBRespsonse();
                                                                                string jsonconnPB = responsePB.Content;
                                                                                objPBRespsonse = JsonConvert.DeserializeObject<PBRespsonse>(jsonconnPB);

                                                                                if (responsePB.StatusCode == HttpStatusCode.OK)
                                                                                {
                                                                                    hfPBNo.Value = objPBRespsonse.PBNO;

                                                                                    string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                                    if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                                    {
                                                                                        message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        message = message + " New Job card not Created.";
                                                                                    }

                                                                                    if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                                    {
                                                                                        message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        message = message + " New Estimate not Created.";
                                                                                    }
                                                                                    message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                                    message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                                    message = message + " New GRN Created. GRN No. is :" + hfGRNNo.Value + ".!";
                                                                                    message = message + " New PB Created. PB No. is :" + hfPBNo.Value + ".!";

                                                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                                }
                                                                                else
                                                                                {
                                                                                    string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                                    if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                                    {
                                                                                        message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        message = message + " New Job card not Created.";
                                                                                    }

                                                                                    if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                                    {
                                                                                        message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        message = message + " New Estimate not Created.";
                                                                                    }
                                                                                    message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                                    message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                                    message = message + " New GRN Created. GRN No. is :" + objGRNRespsonse.GRNNO + ".";
                                                                                    message = message + " PB not Created." + objPBRespsonse.MESSAGE + ".!";

                                                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                                }

                                                                            }
                                                                            else
                                                                            {
                                                                                string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                                if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                                {
                                                                                    message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                                }
                                                                                else
                                                                                {
                                                                                    message = message + " New Job card not Created.";
                                                                                }

                                                                                if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                                {
                                                                                    message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                                }
                                                                                else
                                                                                {
                                                                                    message = message + " New Estimate not Created.";
                                                                                }
                                                                                message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                                message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                                message = message + " New GRN Created. GRN No. is :" + objGRNRespsonse.GRNNO + ".";
                                                                                message = message + " But PB API not found. Please contact to API provider.!";

                                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                            }

                                                                            #endregion
                                                                        }
                                                                        else
                                                                        {
                                                                            string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                            if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                            {
                                                                                message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                            }
                                                                            else
                                                                            {
                                                                                message = message + " New Job card not Created.";
                                                                            }

                                                                            if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                            {
                                                                                message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                            }
                                                                            else
                                                                            {
                                                                                message = message + " New Estimate not Created.";
                                                                            }
                                                                            message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                            message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                            message = message + " GRN not Created." + objGRNRespsonse.MESSAGE + ".!";

                                                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                        }



                                                                    }
                                                                    else
                                                                    {
                                                                        string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                        if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                        {
                                                                            message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                        }
                                                                        else
                                                                        {
                                                                            message = message + " New Job card not Created.";
                                                                        }

                                                                        if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                        {
                                                                            message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                        }
                                                                        else
                                                                        {
                                                                            message = message + " New Estimate not Created.";
                                                                        }
                                                                        message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                        message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                        message = message + " But GRN API not found. Please contact API provider.!";

                                                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                    }


                                                                    #endregion

                                                                }
                                                                else
                                                                {
                                                                    string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                    if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                    {
                                                                        message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                    }
                                                                    else
                                                                    {
                                                                        message = message + " New Job card not Created.";
                                                                    }

                                                                    if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                    {
                                                                        message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                    }
                                                                    else
                                                                    {
                                                                        message = message + " New Estimate not Created.";
                                                                    }
                                                                    message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ". PO not created. " + objPORespsonse.MESSAGE + ".!";

                                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                }


                                                            }
                                                            else
                                                            {
                                                                string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                {
                                                                    message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                }
                                                                else
                                                                {
                                                                    message = message + " New Job card not Created.";
                                                                }

                                                                if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                {
                                                                    message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                }
                                                                else
                                                                {
                                                                    message = message + " New Estimate not Created.";
                                                                }
                                                                message = message + " Job Specification created successfully. New PR Created PR NO. is : " + objPRRespsonse.PRNO + ". PO not created because PO API not found. Please contact API provider.!";

                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                            }



                                                        }
                                                    }
                                                    else
                                                    {
                                                        string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                        if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                        {
                                                            message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                        }
                                                        else
                                                        {
                                                            message = message + " New Job card not Created.";
                                                        }

                                                        if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                        {
                                                            message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                        }
                                                        else
                                                        {
                                                            message = message + " New Estimate not Created.";
                                                        }
                                                        message = message + " Job Specification created successfully. " + objPRRespsonse.MESSAGE + ".!";

                                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                    }

                                                    #endregion


                                                }
                                            }
                                            else
                                            {
                                                if (chkGST.Checked == true)
                                                {
                                                    decimal purcomm = 0;
                                                    decimal purprice = 0;
                                                    decimal basprice = 0;
                                                    decimal tax = 0;

                                                    decimal othchg = 0;
                                                    decimal othbasechg = 0;
                                                    decimal othchgtax = 0;
                                                    decimal nettaxamt = 0;
                                                    decimal netpoamt = 0;

                                                    purcomm = Math.Round(RATE);
                                                    purprice = Math.Round((Convert.ToDecimal(hfLockAmt.Value) + Convert.ToDecimal(purcomm)), 2);
                                                    basprice = Math.Round((Convert.ToDecimal(purprice / Convert.ToDecimal("1.18"))), 2);
                                                    tax = Math.Round((purprice - basprice), 2);

                                                    nettaxamt = Math.Round((tax), 2);
                                                    netpoamt = Math.Round((purprice), 2);


                                                    if (txtChgAmt.Text != null && txtChgAmt.Text != string.Empty && txtChgAmt.Text != null)
                                                    {
                                                        if (Convert.ToDecimal(txtChgAmt.Text) > 0)
                                                        {
                                                            othchg = Math.Round((Convert.ToDecimal(txtChgAmt.Text)), 2);
                                                            othbasechg = Math.Round((Convert.ToDecimal(othchg / Convert.ToDecimal("1.18"))), 2);
                                                            othchgtax = Math.Round((othchg - othbasechg), 2);
                                                            nettaxamt = Math.Round((nettaxamt + othchgtax), 2);
                                                            netpoamt = Math.Round((netpoamt + othchg), 2);
                                                        }
                                                    }

                                                    List<PRDetails> objPRDetails = new List<PRDetails>();
                                                    objPRDetails.Add(new PRDetails
                                                    {
                                                        ASSETCD = "",
                                                        CAMOUNT = purprice,
                                                        CMPID = objMainClass.intCmpId,
                                                        CSTCENTCD = costcenter,
                                                        DELIDT = DateTime.Now,
                                                        GLCD = "10010000",
                                                        IMEINO = txtIMEI.Text,
                                                        ITEMDESC = hfItemDesc.Value,
                                                        ITEMGRPID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMGRP"]),
                                                        ITEMID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]),
                                                        ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",
                                                        LOCCD = hfLocCd.Value,
                                                        PARTREQNO = 0,
                                                        PLANTCD = hfPlantCd.Value,
                                                        PRBY = "",
                                                        PRFCNT = "1000",
                                                        PRNO = "",
                                                        PRQTY = 1,
                                                        RATE = purprice,
                                                        SRNO = 1,
                                                        STATUS = 2,
                                                        TRNUM = hfNewJobID.Value,
                                                        UOM = Convert.ToInt32(dtItemDesc.Rows[0]["UOM"]),
                                                    });

                                                    #region Inser PR Method...




                                                    PRMaster objPRMaster = new PRMaster();
                                                    objPRMaster.CMPID = objMainClass.intCmpId;
                                                    objPRMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                    objPRMaster.CREATEDATE = DateTime.Now;
                                                    objPRMaster.DEPTID = 1;
                                                    objPRMaster.ISPRSTO = 0;
                                                    objPRMaster.LISTINGID = 0;
                                                    objPRMaster.PRDT = DateTime.Now;
                                                    objPRMaster.PRNO = "";
                                                    objPRMaster.PRTYPE = "MPR";
                                                    objPRMaster.REMARK = "AUTO PR/PO AGAINST SO";
                                                    objPRMaster.STATUS = 57;
                                                    objPRMaster.VENDCODE = "";
                                                    objPRMaster.PRDATA = objPRDetails;


                                                    string PRURL = Convert.ToString(dtPRAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtPRAPI.Rows[0]["TOKEN"]);
                                                    var clientPR = new RestClient(PRURL);
                                                    clientPR.Timeout = -1;
                                                    var requestPR = new RestRequest(Method.POST);
                                                    requestPR.AddHeader("" + Convert.ToString(dtPRAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtPRAPI.Rows[0]["KEYVALUE"]) + "");
                                                    requestPR.AddHeader("Content-Type", "application/json");
                                                    var jsonInputPR = JsonConvert.SerializeObject(objPRMaster);
                                                    requestPR.AddParameter("application/json", jsonInputPR, ParameterType.RequestBody);
                                                    IRestResponse responsePR = clientPR.Execute(requestPR);


                                                    PRRespsonse objPRRespsonse = new PRRespsonse();
                                                    string jsonconnPR = responsePR.Content;
                                                    objPRRespsonse = JsonConvert.DeserializeObject<PRRespsonse>(jsonconnPR);

                                                    if (responsePR.StatusCode == HttpStatusCode.OK)
                                                    {
                                                        hfPRNo.Value = Convert.ToString(objMainClass.strConvertZeroPadding(objPRRespsonse.PRNO));

                                                        bool blnPOExist = false;
                                                        DataTable dtPO = new DataTable();
                                                        DataTable dtSO = new DataTable();
                                                        dtPO = objMainClass.CheckPOSOImeiNo(objMainClass.intCmpId, txtIMEI.Text, "CHECKPOWITHRETURN");
                                                        if (dtPO.Rows.Count > 0)
                                                        {
                                                            if (Convert.ToString(dtPO.Rows[0]["LASTPO"]) != "" && Convert.ToString(dtPO.Rows[0]["LASTPO"]) != string.Empty &&
                                                                Convert.ToString(dtPO.Rows[0]["LASTPO"]) != null &&
                                                                Convert.ToInt32(Convert.ToString(dtPO.Rows[0]["BALQTY"]) == "" ? 0 : Convert.ToInt32(dtPO.Rows[0]["BALQTY"])) > 0)
                                                            {
                                                                blnPOExist = true;


                                                                dtSO = objMainClass.CheckPOSOImeiNo(objMainClass.intCmpId, txtIMEI.Text, "CHECKSOWITHRETURN");

                                                                if (dtSO.Rows.Count > 0)
                                                                {
                                                                    if (Convert.ToString(dtSO.Rows[0]["LASTSO"]) != "" && Convert.ToString(dtSO.Rows[0]["LASTSO"]) != string.Empty &&
                                                                Convert.ToString(dtSO.Rows[0]["LASTSO"]) != null &&
                                                                Convert.ToInt32(Convert.ToString(dtSO.Rows[0]["BALQTY"]) == "" ? 0 : Convert.ToInt32(dtSO.Rows[0]["BALQTY"])) > 0)
                                                                    {
                                                                        blnPOExist = false;
                                                                    }

                                                                }

                                                            }
                                                        }

                                                        if (blnPOExist == true)
                                                        {
                                                            string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                            if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                            {
                                                                message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                            }
                                                            else
                                                            {
                                                                message = message + " New Job card not Created.";
                                                            }

                                                            if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                            {
                                                                message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                            }
                                                            else
                                                            {
                                                                message = message + " New Estimate not Created.";
                                                            }
                                                            message = message + " Job Specification created successfully. New PR Created PR NO. is : " + objPRRespsonse.PRNO + ". PO not created because PO is already made for same IMEI No.! PO No. : " + Convert.ToString(dtPO.Rows[0]["LASTPO"]) + ".!";

                                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                        }
                                                        else
                                                        {

                                                            DataTable dtPOAPI = new DataTable();
                                                            dtPOAPI = objMainClass.GetWAData("INSERTPO", 1, "GETWADATA");
                                                            if (dtPOAPI.Rows.Count > 0)
                                                            {
                                                                List<POCharges> objPOCharges = new List<POCharges>();
                                                                if (othchg > 0)
                                                                {
                                                                    objPOCharges.Add(new POCharges
                                                                    {
                                                                        CHGAMT = othbasechg,
                                                                        CHGTYPE = ddlCharges.SelectedValue,
                                                                        CMPID = objMainClass.intCmpId,
                                                                        CONDID = 12,
                                                                        CONDTYPE = "GST",
                                                                        GLCODE = "",
                                                                        OPERATOR = "+",
                                                                        PID = 0,
                                                                        PONO = "",
                                                                        RATE = 18,
                                                                        SRNO = 1,
                                                                        TAXAMT = othchgtax,
                                                                    });
                                                                }

                                                                List<POTaxation> objPOTaxation = new List<POTaxation>();
                                                                objPOTaxation.Add(new POTaxation
                                                                {
                                                                    BASEAMT = basprice,
                                                                    CMPID = objMainClass.intCmpId,
                                                                    CONDID = 12,
                                                                    CONDORDER = 1,
                                                                    CONDTYPE = "GST",
                                                                    GLCODE = "",
                                                                    OPERATOR = "+",
                                                                    PID = 0,
                                                                    PONO = "",
                                                                    POSRNO = 1,
                                                                    RATE = 18,
                                                                    SRNO = 1,
                                                                    TAXAMT = tax,
                                                                });

                                                                List<PODetails> objPODetails = new List<PODetails>();
                                                                objPODetails.Add(new PODetails
                                                                {
                                                                    APRVBY = 17,
                                                                    APRVDATE = DateTime.Now,
                                                                    APRVSTATUS = 260,
                                                                    ASSETCD = "",
                                                                    BRATE = basprice,
                                                                    CAMOUNT = basprice,
                                                                    CMPID = objMainClass.intCmpId,
                                                                    CSTCENTCD = costcenter,
                                                                    DELIDT = DateTime.Now,
                                                                    DEVREASON = "OK",
                                                                    DISCAMT = 0,
                                                                    FROMLOCCD = "",
                                                                    FROMPLANTCD = "",
                                                                    GLCD = "10010000",
                                                                    IMEINO = txtIMEI.Text,
                                                                    ITEMDESC = hfItemDesc.Value,
                                                                    ITEMGRPID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMGRP"]),
                                                                    ITEMID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]),
                                                                    //ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",  //Removed AUTO PO AGAINST PO text from remarks
                                                                    ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text,
                                                                    LOCCD = hfLocCd.Value,
                                                                    LOCKAMT = 0,
                                                                    PLANTCD = hfPlantCd.Value,
                                                                    PONO = "",
                                                                    POQTY = 1,
                                                                    PRFCNT = "1000",
                                                                    PRNO = hfPRNo.Value,
                                                                    PRSRNO = 1,
                                                                    RATE = purprice,
                                                                    REFNO = "",
                                                                    REJREASON = "",
                                                                    SRNO = 1,
                                                                    TAXAMT = 0,
                                                                    TRNUM = hfNewJobID.Value,
                                                                    UOM = Convert.ToInt32(dtItemDesc.Rows[0]["UOM"]),
                                                                });

                                                                POMaster objPOMaster = new POMaster();
                                                                objPOMaster.ADJAMT = 0;
                                                                objPOMaster.ADVAMT = 0;
                                                                objPOMaster.AGENTNAME = "";
                                                                objPOMaster.APRVBY = 17;
                                                                objPOMaster.APRVDATE = DateTime.Now;
                                                                objPOMaster.CMPID = objMainClass.intCmpId;
                                                                objPOMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                objPOMaster.CREATEDATE = DateTime.Now;
                                                                objPOMaster.DEPTID = 1;
                                                                objPOMaster.DISCOUNT = 0;
                                                                objPOMaster.NETMATVALUE = basprice;
                                                                objPOMaster.NETPOAMT = netpoamt;
                                                                objPOMaster.NETTAXAMT = nettaxamt;
                                                                objPOMaster.OLDPOAMT = 0;
                                                                objPOMaster.PENDINGAMT = netpoamt;
                                                                objPOMaster.PMTTERMS = ddlPaymentTerms.SelectedItem.Text;
                                                                objPOMaster.PMTTERMSDESC = txtPaymentTermsDesc.Text;
                                                                objPOMaster.POCharge = objPOCharges;
                                                                objPOMaster.PODetail = objPODetails;
                                                                objPOMaster.PODT = DateTime.Now;
                                                                objPOMaster.PONO = "";
                                                                objPOMaster.POTax = objPOTaxation;
                                                                objPOMaster.POTYPE = "MPO";
                                                                objPOMaster.PURTYPE = Convert.ToInt32(ddlPurType.SelectedValue);
                                                                objPOMaster.REMARK = "AUTO PR/PO AGAINST SO";
                                                                objPOMaster.STATUS = 57;
                                                                objPOMaster.TRANCODE = txtTranCode.Text;
                                                                objPOMaster.VENDCODE = txtVendorCode.Text;


                                                                string POURL = Convert.ToString(dtPOAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtPOAPI.Rows[0]["TOKEN"]);
                                                                var clientPO = new RestClient(POURL);
                                                                clientPO.Timeout = -1;
                                                                var requestPO = new RestRequest(Method.POST);
                                                                requestPO.AddHeader("" + Convert.ToString(dtPOAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtPOAPI.Rows[0]["KEYVALUE"]) + "");
                                                                requestPO.AddHeader("Content-Type", "application/json");
                                                                var jsonInputPO = JsonConvert.SerializeObject(objPOMaster);
                                                                requestPO.AddParameter("application/json", jsonInputPO, ParameterType.RequestBody);
                                                                IRestResponse responsePO = clientPO.Execute(requestPO);


                                                                PORespsonse objPORespsonse = new PORespsonse();
                                                                string jsonconnPO = responsePO.Content;
                                                                objPORespsonse = JsonConvert.DeserializeObject<PORespsonse>(jsonconnPO);

                                                                if (responsePO.StatusCode == HttpStatusCode.OK)
                                                                {

                                                                    hfPONo.Value = objPORespsonse.PONO;

                                                                    #region GRN CODE...

                                                                    DataTable dtGRNAPI = new DataTable();
                                                                    dtGRNAPI = objMainClass.GetWAData("CREATEGRN", 1, "GETWADATA");

                                                                    if (dtGRNAPI.Rows.Count > 0)
                                                                    {
                                                                        List<GRNDetials> objGRNDetials = new List<GRNDetials>();
                                                                        objGRNDetials.Add(new GRNDetials
                                                                        {
                                                                            ASSETCD = "",
                                                                            CAMOUNT = basprice,
                                                                            CHLNQTY = 1,
                                                                            CMPID = objMainClass.intCmpId,
                                                                            CSTCENTCD = costcenter,
                                                                            DOCNO = "",
                                                                            DOCTYPE = "103",
                                                                            FINYEAR = DateTime.Now.Year,
                                                                            GLCD = "10010000",
                                                                            ITEMDESC = hfItemDesc.Value,
                                                                            ITEMGRPID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMGRP"]),
                                                                            ITEMID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]),
                                                                            //ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",  //Removed AUTO PO AGAINST PO text from remarks
                                                                            ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text,
                                                                            LOCCD = hfLocCd.Value,
                                                                            PLANTCD = hfPlantCd.Value,
                                                                            PONO = hfPONo.Value,
                                                                            POSRNO = 1,
                                                                            PRFCNT = "1000",
                                                                            QTY = 1,
                                                                            RATE = basprice,
                                                                            SRNO = 1,
                                                                            TRACKNO = hfNewJobID.Value,
                                                                            UOM = Convert.ToInt32(dtItemDesc.Rows[0]["UOM"]),
                                                                        });

                                                                        GRNMaster objGRNMaster = new GRNMaster();
                                                                        objGRNMaster.CHLNDT = DateTime.Now;
                                                                        objGRNMaster.CHLNNO = hfPRNo.Value;
                                                                        objGRNMaster.CMPID = objMainClass.intCmpId;
                                                                        objGRNMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                        objGRNMaster.DEPTCD = 0;
                                                                        objGRNMaster.DOCDATE = DateTime.Now;
                                                                        objGRNMaster.DOCNO = "";
                                                                        objGRNMaster.DOCTYPE = "103";
                                                                        objGRNMaster.EMPNAME = "";
                                                                        objGRNMaster.FINYEAR = DateTime.Now.Year;
                                                                        objGRNMaster.GRNData = objGRNDetials;
                                                                        objGRNMaster.POSTDATE = DateTime.Now;
                                                                        objGRNMaster.REFDOCNO = hfPONo.Value;
                                                                        objGRNMaster.REFDOCYEAR = Convert.ToInt32(txtVendorCode.Text);
                                                                        objGRNMaster.REFNO = hfPRNo.Value;
                                                                        objGRNMaster.REMARK = "AUTO PR/PO AGAINST SO";
                                                                        objGRNMaster.TRANCODE = txtTranCode.Text;


                                                                        string GRNURL = Convert.ToString(dtGRNAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtGRNAPI.Rows[0]["TOKEN"]);
                                                                        var clientGRN = new RestClient(GRNURL);
                                                                        clientGRN.Timeout = -1;
                                                                        var requestGRN = new RestRequest(Method.POST);
                                                                        requestGRN.AddHeader("" + Convert.ToString(dtGRNAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtGRNAPI.Rows[0]["KEYVALUE"]) + "");
                                                                        requestGRN.AddHeader("Content-Type", "application/json");
                                                                        var jsonInputGRN = JsonConvert.SerializeObject(objGRNMaster);
                                                                        requestGRN.AddParameter("application/json", jsonInputGRN, ParameterType.RequestBody);
                                                                        IRestResponse responseGRN = clientGRN.Execute(requestGRN);


                                                                        GRNRespsonse objGRNRespsonse = new GRNRespsonse();
                                                                        string jsonconnGRN = responseGRN.Content;
                                                                        objGRNRespsonse = JsonConvert.DeserializeObject<GRNRespsonse>(jsonconnGRN);

                                                                        if (responseGRN.StatusCode == HttpStatusCode.OK)
                                                                        {
                                                                            hfGRNNo.Value = objGRNRespsonse.GRNNO;

                                                                            #region PB Code...

                                                                            DataTable dtPBAPI = new DataTable();
                                                                            dtPBAPI = objMainClass.GetWAData("INSERTPB", 1, "GETWADATA");
                                                                            if (dtPBAPI.Rows.Count > 0)
                                                                            {
                                                                                List<PBCharges> objPBCharges = new List<PBCharges>();
                                                                                if (othchg > 0)
                                                                                {
                                                                                    objPBCharges.Add(new PBCharges
                                                                                    {
                                                                                        CHGAMT = othbasechg,
                                                                                        CHGTYPE = ddlCharges.SelectedValue,
                                                                                        CMPID = objMainClass.intCmpId,
                                                                                        CONDID = 12,
                                                                                        CONDTYPE = "GST",
                                                                                        GLCODE = "",
                                                                                        OPERATOR = "+",
                                                                                        PBNO = "",
                                                                                        PID = 0,
                                                                                        RATE = 18,
                                                                                        SRNO = 1,
                                                                                        TAXAMT = othchgtax,
                                                                                    });
                                                                                }

                                                                                List<PBTaxation> objPBTaxation = new List<PBTaxation>();
                                                                                objPBTaxation.Add(new PBTaxation
                                                                                {
                                                                                    BASEAMT = basprice,
                                                                                    CMPID = objMainClass.intCmpId,
                                                                                    CONDID = 12,
                                                                                    CONDORDER = 1,
                                                                                    CONDTYPE = "GST",
                                                                                    GLCODE = "",
                                                                                    OPERATOR = "+",
                                                                                    PBNO = "",
                                                                                    PBSRNO = 1,
                                                                                    PID = 0,
                                                                                    RATE = 18,
                                                                                    SRNO = 1,
                                                                                    TAXAMT = tax,
                                                                                });

                                                                                List<PBDetails> objPBDetails = new List<PBDetails>();
                                                                                objPBDetails.Add(new PBDetails
                                                                                {
                                                                                    ASSETCD = "",
                                                                                    BRATE = basprice,
                                                                                    CAMOUNT = basprice,
                                                                                    CHALLANNO = "",
                                                                                    CMPID = objMainClass.intCmpId,
                                                                                    CSTCENTCD = costcenter,
                                                                                    DISCAMT = 0,
                                                                                    GLCD = "10010000",
                                                                                    ITEMDESC = hfItemDesc.Value,
                                                                                    ITEMGRPID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMGRP"]),
                                                                                    ITEMID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]),
                                                                                    //ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",  //Removed AUTO PO AGAINST PO text from remarks
                                                                                    ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text,
                                                                                    LOCCD = hfLocCd.Value,
                                                                                    MIRNO = hfGRNNo.Value,
                                                                                    MIRSRNO = 1,
                                                                                    PBNO = "",
                                                                                    PBQTY = 1,
                                                                                    PLANTCD = hfPlantCd.Value,
                                                                                    PONO = hfPONo.Value,
                                                                                    POSRNO = 1,
                                                                                    PRFCNT = "1000",
                                                                                    RATE = purprice,
                                                                                    REFNO = "",
                                                                                    SRNO = 1,
                                                                                    TAXAMT = 0,
                                                                                    TRNUM = hfNewJobID.Value,
                                                                                    UOM = Convert.ToInt32(dtItemDesc.Rows[0]["UOM"]),
                                                                                });

                                                                                PBMaster objPBMaster = new PBMaster();
                                                                                objPBMaster.ADJAMT = 0;
                                                                                objPBMaster.BILLAMT = netpoamt;
                                                                                objPBMaster.BILLDT = Convert.ToDateTime(txtBillDate.Text);
                                                                                if (chkINVPO.Checked == true)
                                                                                {
                                                                                    objPBMaster.BILLNO = hfPONo.Value;
                                                                                }
                                                                                else
                                                                                {
                                                                                    objPBMaster.BILLNO = txtBillNo.Text;
                                                                                }
                                                                                objPBMaster.CMPID = objMainClass.intCmpId;
                                                                                objPBMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                                objPBMaster.DISCOUNT = 0;
                                                                                objPBMaster.NETMATVALUE = basprice;
                                                                                objPBMaster.NETPBAMT = netpoamt;
                                                                                objPBMaster.NETTAXAMT = nettaxamt;
                                                                                objPBMaster.PAIDAMT = 0;
                                                                                objPBMaster.PBCharge = objPBCharges;
                                                                                objPBMaster.PBDetail = objPBDetails;
                                                                                objPBMaster.PBDT = DateTime.Now;
                                                                                objPBMaster.PBNO = "";
                                                                                objPBMaster.PBTax = objPBTaxation;
                                                                                objPBMaster.PBTYPE = "MPB";
                                                                                objPBMaster.PENDINGAMT = netpoamt;
                                                                                objPBMaster.PMTTERMS = ddlPaymentTerms.SelectedItem.Text;
                                                                                objPBMaster.PMTTERMSDESC = txtPaymentTermsDesc.Text;
                                                                                objPBMaster.REMARK = "AUTO PR/PO AGAINST SO";
                                                                                objPBMaster.STATUS = 1;
                                                                                objPBMaster.VENDCODE = txtVendorCode.Text;


                                                                                string PBURL = Convert.ToString(dtPBAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtPBAPI.Rows[0]["TOKEN"]);
                                                                                var clientPB = new RestClient(PBURL);
                                                                                clientPB.Timeout = -1;
                                                                                var requestPB = new RestRequest(Method.POST);
                                                                                requestPB.AddHeader("" + Convert.ToString(dtPBAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtPBAPI.Rows[0]["KEYVALUE"]) + "");
                                                                                requestPB.AddHeader("Content-Type", "application/json");
                                                                                var jsonInputPB = JsonConvert.SerializeObject(objPBMaster);
                                                                                requestPB.AddParameter("application/json", jsonInputPB, ParameterType.RequestBody);
                                                                                IRestResponse responsePB = clientPB.Execute(requestPB);

                                                                                PBRespsonse objPBRespsonse = new PBRespsonse();
                                                                                string jsonconnPB = responsePB.Content;
                                                                                objPBRespsonse = JsonConvert.DeserializeObject<PBRespsonse>(jsonconnPB);

                                                                                if (responsePB.StatusCode == HttpStatusCode.OK)
                                                                                {
                                                                                    hfPBNo.Value = objPBRespsonse.PBNO;

                                                                                    string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                                    if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                                    {
                                                                                        message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        message = message + " New Job card not Created.";
                                                                                    }

                                                                                    if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                                    {
                                                                                        message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        message = message + " New Estimate not Created.";
                                                                                    }
                                                                                    message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                                    message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                                    message = message + " New GRN Created. GRN No. is :" + hfGRNNo.Value + ".!";
                                                                                    message = message + " New PB Created. PB No. is :" + hfPBNo.Value + ".!";

                                                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                                }
                                                                                else
                                                                                {
                                                                                    string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                                    if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                                    {
                                                                                        message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        message = message + " New Job card not Created.";
                                                                                    }

                                                                                    if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                                    {
                                                                                        message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        message = message + " New Estimate not Created.";
                                                                                    }
                                                                                    message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                                    message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                                    message = message + " New GRN Created. GRN No. is :" + objGRNRespsonse.GRNNO + ".";
                                                                                    message = message + " PB not Created." + objPBRespsonse.MESSAGE + ".!";

                                                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                                }

                                                                            }
                                                                            else
                                                                            {
                                                                                string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                                if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                                {
                                                                                    message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                                }
                                                                                else
                                                                                {
                                                                                    message = message + " New Job card not Created.";
                                                                                }

                                                                                if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                                {
                                                                                    message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                                }
                                                                                else
                                                                                {
                                                                                    message = message + " New Estimate not Created.";
                                                                                }
                                                                                message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                                message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                                message = message + " New GRN Created. GRN No. is :" + objGRNRespsonse.GRNNO + ".";
                                                                                message = message + " But PB API not found. Please contact to API provider.!";

                                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                            }

                                                                            #endregion

                                                                        }
                                                                        else
                                                                        {
                                                                            string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                            if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                            {
                                                                                message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                            }
                                                                            else
                                                                            {
                                                                                message = message + " New Job card not Created.";
                                                                            }

                                                                            if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                            {
                                                                                message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                            }
                                                                            else
                                                                            {
                                                                                message = message + " New Estimate not Created.";
                                                                            }
                                                                            message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                            message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                            message = message + " GRN not Created." + objGRNRespsonse.MESSAGE + ".!";

                                                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                        }



                                                                    }
                                                                    else
                                                                    {
                                                                        string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                        if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                        {
                                                                            message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                        }
                                                                        else
                                                                        {
                                                                            message = message + " New Job card not Created.";
                                                                        }

                                                                        if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                        {
                                                                            message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                        }
                                                                        else
                                                                        {
                                                                            message = message + " New Estimate not Created.";
                                                                        }
                                                                        message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                        message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                        message = message + " But GRN API not found. Please contact API provider.!";

                                                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                    }


                                                                    #endregion

                                                                }
                                                                else
                                                                {
                                                                    string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                    if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                    {
                                                                        message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                    }
                                                                    else
                                                                    {
                                                                        message = message + " New Job card not Created.";
                                                                    }

                                                                    if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                    {
                                                                        message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                    }
                                                                    else
                                                                    {
                                                                        message = message + " New Estimate not Created.";
                                                                    }
                                                                    message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ". PO not created. " + objPORespsonse.MESSAGE + ".!";

                                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                }


                                                            }
                                                            else
                                                            {
                                                                string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                {
                                                                    message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                }
                                                                else
                                                                {
                                                                    message = message + " New Job card not Created.";
                                                                }

                                                                if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                {
                                                                    message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                }
                                                                else
                                                                {
                                                                    message = message + " New Estimate not Created.";
                                                                }
                                                                message = message + " Job Specification created successfully. New PR Created PR NO. is : " + objPRRespsonse.PRNO + ". PO not created because PO API not found. Please contact API provider.!";

                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                            }



                                                        }
                                                    }
                                                    else
                                                    {
                                                        string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                        if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                        {
                                                            message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                        }
                                                        else
                                                        {
                                                            message = message + " New Job card not Created.";
                                                        }

                                                        if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                        {
                                                            message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                        }
                                                        else
                                                        {
                                                            message = message + " New Estimate not Created.";
                                                        }
                                                        message = message + " Job Specification created successfully. " + objPRRespsonse.MESSAGE + ".!";

                                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                    }

                                                    #endregion


                                                }
                                                else
                                                {
                                                    decimal purcomm = 0;
                                                    decimal purprice = 0;
                                                    decimal basprice = 0;
                                                    decimal tax = 0;

                                                    decimal othchg = 0;
                                                    decimal othbasechg = 0;
                                                    decimal othchgtax = 0;
                                                    decimal nettaxamt = 0;
                                                    decimal netpoamt = 0;

                                                    purcomm = Math.Round(RATE);
                                                    purprice = Math.Round((Convert.ToDecimal(hfLockAmt.Value) + Convert.ToDecimal(purcomm)), 2);
                                                    basprice = Math.Round((Convert.ToDecimal(purprice / Convert.ToDecimal("1.18"))), 2);
                                                    tax = Math.Round((purprice - basprice), 2);

                                                    //nettaxamt = tax;
                                                    netpoamt = Math.Round((purprice), 2);

                                                    if (txtChgAmt.Text != null && txtChgAmt.Text != string.Empty && txtChgAmt.Text != null)
                                                    {
                                                        if (Convert.ToDecimal(txtChgAmt.Text) > 0)
                                                        {
                                                            othchg = Math.Round((Convert.ToDecimal(txtChgAmt.Text)), 2);
                                                            othbasechg = Math.Round((Convert.ToDecimal(othchg / Convert.ToDecimal("1.18"))), 2);
                                                            othchgtax = Math.Round((othchg - othbasechg), 2);
                                                            //nettaxamt = nettaxamt + othchgtax;
                                                            netpoamt = Math.Round((netpoamt + othchg), 2);
                                                        }
                                                    }

                                                    List<PRDetails> objPRDetails = new List<PRDetails>();
                                                    objPRDetails.Add(new PRDetails
                                                    {
                                                        ASSETCD = "",
                                                        CAMOUNT = purprice,
                                                        CMPID = objMainClass.intCmpId,
                                                        CSTCENTCD = costcenter,
                                                        DELIDT = DateTime.Now,
                                                        GLCD = "10010000",
                                                        IMEINO = txtIMEI.Text,
                                                        ITEMDESC = hfItemDesc.Value,
                                                        ITEMGRPID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMGRP"]),
                                                        ITEMID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]),
                                                        ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",
                                                        LOCCD = hfLocCd.Value,
                                                        PARTREQNO = 0,
                                                        PLANTCD = hfPlantCd.Value,
                                                        PRBY = "",
                                                        PRFCNT = "1000",
                                                        PRNO = "",
                                                        PRQTY = 1,
                                                        RATE = purprice,
                                                        SRNO = 1,
                                                        STATUS = 2,
                                                        TRNUM = hfNewJobID.Value,
                                                        UOM = Convert.ToInt32(dtItemDesc.Rows[0]["UOM"]),
                                                    });

                                                    #region Inser PR Method...




                                                    PRMaster objPRMaster = new PRMaster();
                                                    objPRMaster.CMPID = objMainClass.intCmpId;
                                                    objPRMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                    objPRMaster.CREATEDATE = DateTime.Now;
                                                    objPRMaster.DEPTID = 1;
                                                    objPRMaster.ISPRSTO = 0;
                                                    objPRMaster.LISTINGID = 0;
                                                    objPRMaster.PRDT = DateTime.Now;
                                                    objPRMaster.PRNO = "";
                                                    objPRMaster.PRTYPE = "MPR";
                                                    objPRMaster.REMARK = "AUTO PR/PO AGAINST SO";
                                                    objPRMaster.STATUS = 57;
                                                    objPRMaster.VENDCODE = "";
                                                    objPRMaster.PRDATA = objPRDetails;


                                                    string PRURL = Convert.ToString(dtPRAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtPRAPI.Rows[0]["TOKEN"]);
                                                    var clientPR = new RestClient(PRURL);
                                                    clientPR.Timeout = -1;
                                                    var requestPR = new RestRequest(Method.POST);
                                                    requestPR.AddHeader("" + Convert.ToString(dtPRAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtPRAPI.Rows[0]["KEYVALUE"]) + "");
                                                    requestPR.AddHeader("Content-Type", "application/json");
                                                    var jsonInputPR = JsonConvert.SerializeObject(objPRMaster);
                                                    requestPR.AddParameter("application/json", jsonInputPR, ParameterType.RequestBody);
                                                    IRestResponse responsePR = clientPR.Execute(requestPR);


                                                    PRRespsonse objPRRespsonse = new PRRespsonse();
                                                    string jsonconnPR = responsePR.Content;
                                                    objPRRespsonse = JsonConvert.DeserializeObject<PRRespsonse>(jsonconnPR);

                                                    if (responsePR.StatusCode == HttpStatusCode.OK)
                                                    {
                                                        hfPRNo.Value = Convert.ToString(objMainClass.strConvertZeroPadding(objPRRespsonse.PRNO));

                                                        bool blnPOExist = false;
                                                        DataTable dtPO = new DataTable();
                                                        DataTable dtSO = new DataTable();
                                                        dtPO = objMainClass.CheckPOSOImeiNo(objMainClass.intCmpId, txtIMEI.Text, "CHECKPOWITHRETURN");
                                                        if (dtPO.Rows.Count > 0)
                                                        {
                                                            if (Convert.ToString(dtPO.Rows[0]["LASTPO"]) != "" && Convert.ToString(dtPO.Rows[0]["LASTPO"]) != string.Empty &&
                                                                Convert.ToString(dtPO.Rows[0]["LASTPO"]) != null &&
                                                                Convert.ToInt32(Convert.ToString(dtPO.Rows[0]["BALQTY"]) == "" ? 0 : Convert.ToInt32(dtPO.Rows[0]["BALQTY"])) > 0)
                                                            {
                                                                blnPOExist = true;


                                                                dtSO = objMainClass.CheckPOSOImeiNo(objMainClass.intCmpId, txtIMEI.Text, "CHECKSOWITHRETURN");

                                                                if (dtSO.Rows.Count > 0)
                                                                {
                                                                    if (Convert.ToString(dtSO.Rows[0]["LASTSO"]) != "" && Convert.ToString(dtSO.Rows[0]["LASTSO"]) != string.Empty &&
                                                                Convert.ToString(dtSO.Rows[0]["LASTSO"]) != null &&
                                                                Convert.ToInt32(Convert.ToString(dtSO.Rows[0]["BALQTY"]) == "" ? 0 : Convert.ToInt32(dtSO.Rows[0]["BALQTY"])) > 0)
                                                                    {
                                                                        blnPOExist = false;
                                                                    }

                                                                }

                                                            }
                                                        }

                                                        if (blnPOExist == true)
                                                        {
                                                            string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                            if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                            {
                                                                message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                            }
                                                            else
                                                            {
                                                                message = message + " New Job card not Created.";
                                                            }

                                                            if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                            {
                                                                message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                            }
                                                            else
                                                            {
                                                                message = message + " New Estimate not Created.";
                                                            }
                                                            message = message + " Job Specification created successfully. New PR Created PR NO. is : " + objPRRespsonse.PRNO + ". PO not created because PO is already made for same IMEI No.! PO No. : " + Convert.ToString(dtPO.Rows[0]["LASTPO"]) + ".!";

                                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                        }
                                                        else
                                                        {

                                                            DataTable dtPOAPI = new DataTable();
                                                            dtPOAPI = objMainClass.GetWAData("INSERTPO", 1, "GETWADATA");
                                                            if (dtPOAPI.Rows.Count > 0)
                                                            {
                                                                List<POCharges> objPOCharges = new List<POCharges>();
                                                                if (othchg > 0)
                                                                {
                                                                    objPOCharges.Add(new POCharges
                                                                    {
                                                                        CHGAMT = othchg,
                                                                        CHGTYPE = ddlCharges.SelectedValue,
                                                                        CMPID = objMainClass.intCmpId,
                                                                        CONDID = 0,
                                                                        CONDTYPE = "",
                                                                        GLCODE = "",
                                                                        OPERATOR = "+",
                                                                        PID = 0,
                                                                        PONO = "",
                                                                        RATE = 0,
                                                                        SRNO = 1,
                                                                        TAXAMT = 0,
                                                                    });
                                                                }

                                                                List<POTaxation> objPOTaxation = new List<POTaxation>();
                                                                //objPOTaxation.Add(new POTaxation
                                                                //{
                                                                //    BASEAMT = basprice,
                                                                //    CMPID = objMainClass.intCmpId,
                                                                //    CONDID = 12,
                                                                //    CONDORDER = 1,
                                                                //    CONDTYPE = "GST",
                                                                //    GLCODE = "",
                                                                //    OPERATOR = "+",
                                                                //    PID = 0,
                                                                //    PONO = "",
                                                                //    POSRNO = 1,
                                                                //    RATE = 18,
                                                                //    SRNO = 1,
                                                                //    TAXAMT = tax,
                                                                //});

                                                                List<PODetails> objPODetails = new List<PODetails>();
                                                                objPODetails.Add(new PODetails
                                                                {
                                                                    APRVBY = 17,
                                                                    APRVDATE = DateTime.Now,
                                                                    APRVSTATUS = 260,
                                                                    ASSETCD = "",
                                                                    BRATE = purprice,
                                                                    CAMOUNT = purprice,
                                                                    CMPID = objMainClass.intCmpId,
                                                                    CSTCENTCD = costcenter,
                                                                    DELIDT = DateTime.Now,
                                                                    DEVREASON = "OK",
                                                                    DISCAMT = 0,
                                                                    FROMLOCCD = "",
                                                                    FROMPLANTCD = "",
                                                                    GLCD = "10010000",
                                                                    IMEINO = txtIMEI.Text,
                                                                    ITEMDESC = hfItemDesc.Value,
                                                                    ITEMGRPID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMGRP"]),
                                                                    ITEMID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]),
                                                                    //ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",  //Removed AUTO PO AGAINST PO text from remarks
                                                                    ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text,
                                                                    LOCCD = hfLocCd.Value,
                                                                    LOCKAMT = 0,
                                                                    PLANTCD = hfPlantCd.Value,
                                                                    PONO = "",
                                                                    POQTY = 1,
                                                                    PRFCNT = "1000",
                                                                    PRNO = hfPRNo.Value,
                                                                    PRSRNO = 1,
                                                                    RATE = purprice,
                                                                    REFNO = "",
                                                                    REJREASON = "",
                                                                    SRNO = 1,
                                                                    TAXAMT = 0,
                                                                    TRNUM = hfNewJobID.Value,
                                                                    UOM = Convert.ToInt32(dtItemDesc.Rows[0]["UOM"]),
                                                                });

                                                                POMaster objPOMaster = new POMaster();
                                                                objPOMaster.ADJAMT = 0;
                                                                objPOMaster.ADVAMT = 0;
                                                                objPOMaster.AGENTNAME = "";
                                                                objPOMaster.APRVBY = 17;
                                                                objPOMaster.APRVDATE = DateTime.Now;
                                                                objPOMaster.CMPID = objMainClass.intCmpId;
                                                                objPOMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                objPOMaster.CREATEDATE = DateTime.Now;
                                                                objPOMaster.DEPTID = 1;
                                                                objPOMaster.DISCOUNT = 0;
                                                                objPOMaster.NETMATVALUE = purprice;
                                                                objPOMaster.NETPOAMT = netpoamt;
                                                                objPOMaster.NETTAXAMT = nettaxamt;
                                                                objPOMaster.OLDPOAMT = 0;
                                                                objPOMaster.PENDINGAMT = netpoamt;
                                                                objPOMaster.PMTTERMS = ddlPaymentTerms.SelectedItem.Text;
                                                                objPOMaster.PMTTERMSDESC = txtPaymentTermsDesc.Text;
                                                                objPOMaster.POCharge = objPOCharges;
                                                                objPOMaster.PODetail = objPODetails;
                                                                objPOMaster.PODT = DateTime.Now;
                                                                objPOMaster.PONO = "";
                                                                objPOMaster.POTax = objPOTaxation;
                                                                objPOMaster.POTYPE = "MPO";
                                                                objPOMaster.PURTYPE = Convert.ToInt32(ddlPurType.SelectedValue);
                                                                objPOMaster.REMARK = "AUTO PR/PO AGAINST SO";
                                                                objPOMaster.STATUS = 57;
                                                                objPOMaster.TRANCODE = txtTranCode.Text;
                                                                objPOMaster.VENDCODE = txtVendorCode.Text;


                                                                string POURL = Convert.ToString(dtPOAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtPOAPI.Rows[0]["TOKEN"]);
                                                                var clientPO = new RestClient(POURL);
                                                                clientPO.Timeout = -1;
                                                                var requestPO = new RestRequest(Method.POST);
                                                                requestPO.AddHeader("" + Convert.ToString(dtPOAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtPOAPI.Rows[0]["KEYVALUE"]) + "");
                                                                requestPO.AddHeader("Content-Type", "application/json");
                                                                var jsonInputPO = JsonConvert.SerializeObject(objPOMaster);
                                                                requestPO.AddParameter("application/json", jsonInputPO, ParameterType.RequestBody);
                                                                IRestResponse responsePO = clientPO.Execute(requestPO);


                                                                PORespsonse objPORespsonse = new PORespsonse();
                                                                string jsonconnPO = responsePO.Content;
                                                                objPORespsonse = JsonConvert.DeserializeObject<PORespsonse>(jsonconnPO);

                                                                if (responsePO.StatusCode == HttpStatusCode.OK)
                                                                {

                                                                    hfPONo.Value = objPORespsonse.PONO;

                                                                    #region GRN CODE...

                                                                    DataTable dtGRNAPI = new DataTable();
                                                                    dtGRNAPI = objMainClass.GetWAData("CREATEGRN", 1, "GETWADATA");

                                                                    if (dtGRNAPI.Rows.Count > 0)
                                                                    {
                                                                        List<GRNDetials> objGRNDetials = new List<GRNDetials>();
                                                                        objGRNDetials.Add(new GRNDetials
                                                                        {
                                                                            ASSETCD = "",
                                                                            CAMOUNT = purprice,
                                                                            CHLNQTY = 1,
                                                                            CMPID = objMainClass.intCmpId,
                                                                            CSTCENTCD = costcenter,
                                                                            DOCNO = "",
                                                                            DOCTYPE = "103",
                                                                            FINYEAR = DateTime.Now.Year,
                                                                            GLCD = "10010000",
                                                                            ITEMDESC = hfItemDesc.Value,
                                                                            ITEMGRPID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMGRP"]),
                                                                            ITEMID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]),
                                                                            //ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",  //Removed AUTO PO AGAINST PO text from remarks
                                                                            ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text,
                                                                            LOCCD = hfLocCd.Value,
                                                                            PLANTCD = hfPlantCd.Value,
                                                                            PONO = hfPONo.Value,
                                                                            POSRNO = 1,
                                                                            PRFCNT = "1000",
                                                                            QTY = 1,
                                                                            RATE = purprice,
                                                                            SRNO = 1,
                                                                            TRACKNO = hfNewJobID.Value,
                                                                            UOM = Convert.ToInt32(dtItemDesc.Rows[0]["UOM"]),
                                                                        });

                                                                        GRNMaster objGRNMaster = new GRNMaster();
                                                                        objGRNMaster.CHLNDT = DateTime.Now;
                                                                        objGRNMaster.CHLNNO = hfPRNo.Value;
                                                                        objGRNMaster.CMPID = objMainClass.intCmpId;
                                                                        objGRNMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                        objGRNMaster.DEPTCD = 0;
                                                                        objGRNMaster.DOCDATE = DateTime.Now;
                                                                        objGRNMaster.DOCNO = "";
                                                                        objGRNMaster.DOCTYPE = "103";
                                                                        objGRNMaster.EMPNAME = "";
                                                                        objGRNMaster.FINYEAR = DateTime.Now.Year;
                                                                        objGRNMaster.GRNData = objGRNDetials;
                                                                        objGRNMaster.POSTDATE = DateTime.Now;
                                                                        objGRNMaster.REFDOCNO = hfPONo.Value;
                                                                        objGRNMaster.REFDOCYEAR = Convert.ToInt32(txtVendorCode.Text);
                                                                        objGRNMaster.REFNO = hfPRNo.Value;
                                                                        objGRNMaster.REMARK = "AUTO PR/PO AGAINST SO";
                                                                        objGRNMaster.TRANCODE = txtTranCode.Text;


                                                                        string GRNURL = Convert.ToString(dtGRNAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtGRNAPI.Rows[0]["TOKEN"]);
                                                                        var clientGRN = new RestClient(GRNURL);
                                                                        clientGRN.Timeout = -1;
                                                                        var requestGRN = new RestRequest(Method.POST);
                                                                        requestGRN.AddHeader("" + Convert.ToString(dtGRNAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtGRNAPI.Rows[0]["KEYVALUE"]) + "");
                                                                        requestGRN.AddHeader("Content-Type", "application/json");
                                                                        var jsonInputGRN = JsonConvert.SerializeObject(objGRNMaster);
                                                                        requestGRN.AddParameter("application/json", jsonInputGRN, ParameterType.RequestBody);
                                                                        IRestResponse responseGRN = clientGRN.Execute(requestGRN);


                                                                        GRNRespsonse objGRNRespsonse = new GRNRespsonse();
                                                                        string jsonconnGRN = responseGRN.Content;
                                                                        objGRNRespsonse = JsonConvert.DeserializeObject<GRNRespsonse>(jsonconnGRN);

                                                                        if (responseGRN.StatusCode == HttpStatusCode.OK)
                                                                        {
                                                                            hfGRNNo.Value = objGRNRespsonse.GRNNO;

                                                                            #region PB Code...

                                                                            DataTable dtPBAPI = new DataTable();
                                                                            dtPBAPI = objMainClass.GetWAData("INSERTPB", 1, "GETWADATA");
                                                                            if (dtPBAPI.Rows.Count > 0)
                                                                            {
                                                                                List<PBCharges> objPBCharges = new List<PBCharges>();
                                                                                if (othchg > 0)
                                                                                {
                                                                                    objPBCharges.Add(new PBCharges
                                                                                    {
                                                                                        CHGAMT = othchg,
                                                                                        CHGTYPE = ddlCharges.SelectedValue,
                                                                                        CMPID = objMainClass.intCmpId,
                                                                                        CONDID = 12,
                                                                                        CONDTYPE = "GST",
                                                                                        GLCODE = "",
                                                                                        OPERATOR = "+",
                                                                                        PBNO = "",
                                                                                        PID = 0,
                                                                                        RATE = 18,
                                                                                        SRNO = 1,
                                                                                        TAXAMT = 0,
                                                                                    });
                                                                                }

                                                                                List<PBTaxation> objPBTaxation = new List<PBTaxation>();
                                                                                //objPBTaxation.Add(new PBTaxation
                                                                                //{
                                                                                //    BASEAMT = basprice,
                                                                                //    CMPID = objMainClass.intCmpId,
                                                                                //    CONDID = 12,
                                                                                //    CONDORDER = 1,
                                                                                //    CONDTYPE = "GST",
                                                                                //    GLCODE = "",
                                                                                //    OPERATOR = "+",
                                                                                //    PBNO = "",
                                                                                //    PBSRNO = 1,
                                                                                //    PID = 0,
                                                                                //    RATE = 18,
                                                                                //    SRNO = 1,
                                                                                //    TAXAMT = tax,
                                                                                //});

                                                                                List<PBDetails> objPBDetails = new List<PBDetails>();
                                                                                objPBDetails.Add(new PBDetails
                                                                                {
                                                                                    ASSETCD = "",
                                                                                    BRATE = purprice,
                                                                                    CAMOUNT = purprice,
                                                                                    CHALLANNO = "",
                                                                                    CMPID = objMainClass.intCmpId,
                                                                                    CSTCENTCD = costcenter,
                                                                                    DISCAMT = 0,
                                                                                    GLCD = "10010000",
                                                                                    ITEMDESC = hfItemDesc.Value,
                                                                                    ITEMGRPID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMGRP"]),
                                                                                    ITEMID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]),
                                                                                    //ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",  //Removed AUTO PO AGAINST PO text from remarks
                                                                                    ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text,
                                                                                    LOCCD = hfLocCd.Value,
                                                                                    MIRNO = hfGRNNo.Value,
                                                                                    MIRSRNO = 1,
                                                                                    PBNO = "",
                                                                                    PBQTY = 1,
                                                                                    PLANTCD = hfPlantCd.Value,
                                                                                    PONO = hfPONo.Value,
                                                                                    POSRNO = 1,
                                                                                    PRFCNT = "1000",
                                                                                    RATE = purprice,
                                                                                    REFNO = "",
                                                                                    SRNO = 1,
                                                                                    TAXAMT = 0,
                                                                                    TRNUM = hfNewJobID.Value,
                                                                                    UOM = Convert.ToInt32(dtItemDesc.Rows[0]["UOM"]),
                                                                                });

                                                                                PBMaster objPBMaster = new PBMaster();
                                                                                objPBMaster.ADJAMT = 0;
                                                                                objPBMaster.BILLAMT = netpoamt;
                                                                                objPBMaster.BILLDT = Convert.ToDateTime(txtBillDate.Text);
                                                                                if (chkINVPO.Checked == true)
                                                                                {
                                                                                    objPBMaster.BILLNO = hfPONo.Value;
                                                                                }
                                                                                else
                                                                                {
                                                                                    objPBMaster.BILLNO = txtBillNo.Text;
                                                                                }
                                                                                objPBMaster.CMPID = objMainClass.intCmpId;
                                                                                objPBMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                                objPBMaster.DISCOUNT = 0;
                                                                                objPBMaster.NETMATVALUE = purprice;
                                                                                objPBMaster.NETPBAMT = netpoamt;
                                                                                objPBMaster.NETTAXAMT = nettaxamt;
                                                                                objPBMaster.PAIDAMT = 0;
                                                                                objPBMaster.PBCharge = objPBCharges;
                                                                                objPBMaster.PBDetail = objPBDetails;
                                                                                objPBMaster.PBDT = DateTime.Now;
                                                                                objPBMaster.PBNO = "";
                                                                                objPBMaster.PBTax = objPBTaxation;
                                                                                objPBMaster.PBTYPE = "MPB";
                                                                                objPBMaster.PENDINGAMT = netpoamt;
                                                                                objPBMaster.PMTTERMS = ddlPaymentTerms.SelectedItem.Text;
                                                                                objPBMaster.PMTTERMSDESC = txtPaymentTermsDesc.Text;
                                                                                objPBMaster.REMARK = "AUTO PR/PO AGAINST SO";
                                                                                objPBMaster.STATUS = 1;
                                                                                objPBMaster.VENDCODE = txtVendorCode.Text;


                                                                                string PBURL = Convert.ToString(dtPBAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtPBAPI.Rows[0]["TOKEN"]);
                                                                                var clientPB = new RestClient(PBURL);
                                                                                clientPB.Timeout = -1;
                                                                                var requestPB = new RestRequest(Method.POST);
                                                                                requestPB.AddHeader("" + Convert.ToString(dtPBAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtPBAPI.Rows[0]["KEYVALUE"]) + "");
                                                                                requestPB.AddHeader("Content-Type", "application/json");
                                                                                var jsonInputPB = JsonConvert.SerializeObject(objPBMaster);
                                                                                requestPB.AddParameter("application/json", jsonInputPB, ParameterType.RequestBody);
                                                                                IRestResponse responsePB = clientPB.Execute(requestPB);

                                                                                PBRespsonse objPBRespsonse = new PBRespsonse();
                                                                                string jsonconnPB = responsePB.Content;
                                                                                objPBRespsonse = JsonConvert.DeserializeObject<PBRespsonse>(jsonconnPB);

                                                                                if (responsePB.StatusCode == HttpStatusCode.OK)
                                                                                {
                                                                                    hfPBNo.Value = objPBRespsonse.PBNO;

                                                                                    string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                                    if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                                    {
                                                                                        message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        message = message + " New Job card not Created.";
                                                                                    }

                                                                                    if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                                    {
                                                                                        message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        message = message + " New Estimate not Created.";
                                                                                    }
                                                                                    message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                                    message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                                    message = message + " New GRN Created. GRN No. is :" + hfGRNNo.Value + ".!";
                                                                                    message = message + " New PB Created. PB No. is :" + hfPBNo.Value + ".!";

                                                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                                }
                                                                                else
                                                                                {
                                                                                    string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                                    if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                                    {
                                                                                        message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        message = message + " New Job card not Created.";
                                                                                    }

                                                                                    if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                                    {
                                                                                        message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        message = message + " New Estimate not Created.";
                                                                                    }
                                                                                    message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                                    message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                                    message = message + " New GRN Created. GRN No. is :" + objGRNRespsonse.GRNNO + ".";
                                                                                    message = message + " PB not Created." + objPBRespsonse.MESSAGE + ".!";

                                                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                                }

                                                                            }
                                                                            else
                                                                            {
                                                                                string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                                if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                                {
                                                                                    message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                                }
                                                                                else
                                                                                {
                                                                                    message = message + " New Job card not Created.";
                                                                                }

                                                                                if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                                {
                                                                                    message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                                }
                                                                                else
                                                                                {
                                                                                    message = message + " New Estimate not Created.";
                                                                                }
                                                                                message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                                message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                                message = message + " New GRN Created. GRN No. is :" + objGRNRespsonse.GRNNO + ".";
                                                                                message = message + " But PB API not found. Please contact to API provider.!";

                                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                            }

                                                                            #endregion
                                                                        }
                                                                        else
                                                                        {
                                                                            string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                            if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                            {
                                                                                message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                            }
                                                                            else
                                                                            {
                                                                                message = message + " New Job card not Created.";
                                                                            }

                                                                            if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                            {
                                                                                message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                            }
                                                                            else
                                                                            {
                                                                                message = message + " New Estimate not Created.";
                                                                            }
                                                                            message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                            message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                            message = message + " GRN not Created." + objGRNRespsonse.MESSAGE + ".!";

                                                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                        }



                                                                    }
                                                                    else
                                                                    {
                                                                        string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                        if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                        {
                                                                            message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                        }
                                                                        else
                                                                        {
                                                                            message = message + " New Job card not Created.";
                                                                        }

                                                                        if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                        {
                                                                            message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                        }
                                                                        else
                                                                        {
                                                                            message = message + " New Estimate not Created.";
                                                                        }
                                                                        message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                        message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                        message = message + " But GRN API not found. Please contact API provider.!";

                                                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                    }


                                                                    #endregion

                                                                }
                                                                else
                                                                {
                                                                    string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                    if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                    {
                                                                        message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                    }
                                                                    else
                                                                    {
                                                                        message = message + " New Job card not Created.";
                                                                    }

                                                                    if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                    {
                                                                        message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                    }
                                                                    else
                                                                    {
                                                                        message = message + " New Estimate not Created.";
                                                                    }
                                                                    message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ". PO not created. " + objPORespsonse.MESSAGE + ".!";

                                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                }


                                                            }
                                                            else
                                                            {
                                                                string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                {
                                                                    message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                }
                                                                else
                                                                {
                                                                    message = message + " New Job card not Created.";
                                                                }

                                                                if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                {
                                                                    message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                }
                                                                else
                                                                {
                                                                    message = message + " New Estimate not Created.";
                                                                }
                                                                message = message + " Job Specification created successfully. New PR Created PR NO. is : " + objPRRespsonse.PRNO + ". PO not created because PO API not found. Please contact API provider.!";

                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                            }



                                                        }
                                                    }
                                                    else
                                                    {
                                                        string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                        if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                        {
                                                            message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                        }
                                                        else
                                                        {
                                                            message = message + " New Job card not Created.";
                                                        }

                                                        if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                        {
                                                            message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                        }
                                                        else
                                                        {
                                                            message = message + " New Estimate not Created.";
                                                        }
                                                        message = message + " Job Specification created successfully. " + objPRRespsonse.MESSAGE + ".!";

                                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                    }

                                                    #endregion


                                                }
                                            }
                                        }
                                        else
                                        {
                                            string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                            if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                            {
                                                message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                            }
                                            else
                                            {
                                                message = message + " New Job card not Created.";
                                            }

                                            if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                            {
                                                message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                            }
                                            else
                                            {
                                                message = message + " New Estimate not Created.";
                                            }
                                            message = message + " Job Specification created successfully. But Lock Amount is 0. Please enter valid Lock Amount.!";

                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                        }
                                    }
                                    else
                                    {
                                        if (PERORFIX == 1)
                                        {
                                            if (chkGST.Checked == true)
                                            {
                                                decimal purcomm = 0;
                                                decimal purprice = 0;
                                                decimal basprice = 0;
                                                decimal tax = 0;

                                                decimal othchg = 0;
                                                decimal othbasechg = 0;
                                                decimal othchgtax = 0;
                                                decimal nettaxamt = 0;
                                                decimal netpoamt = 0;

                                                purcomm = Math.Round((Convert.ToDecimal(hfPrice.Value) * (RATE) / 100), 2);
                                                purprice = Math.Round((Convert.ToDecimal(hfPrice.Value) - Convert.ToDecimal(purcomm)), 2);
                                                basprice = Math.Round((Convert.ToDecimal(purprice / Convert.ToDecimal("1.18"))), 2);
                                                tax = Math.Round((purprice - basprice), 2);

                                                nettaxamt = Math.Round((tax), 2);
                                                netpoamt = Math.Round((purprice), 2);

                                                if (txtChgAmt.Text != null && txtChgAmt.Text != string.Empty && txtChgAmt.Text != null)
                                                {
                                                    if (Convert.ToDecimal(txtChgAmt.Text) > 0)
                                                    {
                                                        othchg = Math.Round((Convert.ToDecimal(txtChgAmt.Text)), 2);
                                                        othbasechg = Math.Round((Convert.ToDecimal(othchg / Convert.ToDecimal("1.18"))), 2);
                                                        othchgtax = Math.Round((othchg - othbasechg), 2);
                                                        nettaxamt = Math.Round((nettaxamt + othchgtax), 2);
                                                        netpoamt = Math.Round((netpoamt + othchg), 2);
                                                    }
                                                }

                                                List<PRDetails> objPRDetails = new List<PRDetails>();
                                                objPRDetails.Add(new PRDetails
                                                {
                                                    ASSETCD = "",
                                                    CAMOUNT = purprice,
                                                    CMPID = objMainClass.intCmpId,
                                                    CSTCENTCD = costcenter,
                                                    DELIDT = DateTime.Now,
                                                    GLCD = "10010000",
                                                    IMEINO = txtIMEI.Text,
                                                    ITEMDESC = hfItemDesc.Value,
                                                    ITEMGRPID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMGRP"]),
                                                    ITEMID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]),
                                                    UOM = Convert.ToInt32(dtItemDesc.Rows[0]["UOM"]),
                                                    ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",
                                                    LOCCD = hfLocCd.Value,
                                                    PARTREQNO = 0,
                                                    PLANTCD = hfPlantCd.Value,
                                                    PRBY = "",
                                                    PRFCNT = "1000",
                                                    PRNO = "",
                                                    PRQTY = 1,
                                                    RATE = purprice,
                                                    SRNO = 1,
                                                    STATUS = 2,
                                                    TRNUM = hfNewJobID.Value,

                                                });

                                                #region Inser PR Method...




                                                PRMaster objPRMaster = new PRMaster();
                                                objPRMaster.CMPID = objMainClass.intCmpId;
                                                objPRMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                objPRMaster.CREATEDATE = DateTime.Now;
                                                objPRMaster.DEPTID = 1;
                                                objPRMaster.ISPRSTO = 0;
                                                objPRMaster.LISTINGID = 0;
                                                objPRMaster.PRDT = DateTime.Now;
                                                objPRMaster.PRNO = "";
                                                objPRMaster.PRTYPE = "MPR";
                                                objPRMaster.REMARK = "AUTO PR/PO AGAINST SO";
                                                objPRMaster.STATUS = 57;
                                                objPRMaster.VENDCODE = "";
                                                objPRMaster.PRDATA = objPRDetails;


                                                string PRURL = Convert.ToString(dtPRAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtPRAPI.Rows[0]["TOKEN"]);
                                                var clientPR = new RestClient(PRURL);
                                                clientPR.Timeout = -1;
                                                var requestPR = new RestRequest(Method.POST);
                                                requestPR.AddHeader("" + Convert.ToString(dtPRAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtPRAPI.Rows[0]["KEYVALUE"]) + "");
                                                requestPR.AddHeader("Content-Type", "application/json");
                                                var jsonInputPR = JsonConvert.SerializeObject(objPRMaster);
                                                requestPR.AddParameter("application/json", jsonInputPR, ParameterType.RequestBody);
                                                IRestResponse responsePR = clientPR.Execute(requestPR);


                                                PRRespsonse objPRRespsonse = new PRRespsonse();
                                                string jsonconnPR = responsePR.Content;
                                                objPRRespsonse = JsonConvert.DeserializeObject<PRRespsonse>(jsonconnPR);

                                                if (responsePR.StatusCode == HttpStatusCode.OK)
                                                {
                                                    hfPRNo.Value = Convert.ToString(objMainClass.strConvertZeroPadding(objPRRespsonse.PRNO));

                                                    bool blnPOExist = false;
                                                    DataTable dtPO = new DataTable();
                                                    DataTable dtSO = new DataTable();
                                                    dtPO = objMainClass.CheckPOSOImeiNo(objMainClass.intCmpId, txtIMEI.Text, "CHECKPOWITHRETURN");
                                                    if (dtPO.Rows.Count > 0)
                                                    {
                                                        if (Convert.ToString(dtPO.Rows[0]["LASTPO"]) != "" && Convert.ToString(dtPO.Rows[0]["LASTPO"]) != string.Empty &&
                                                            Convert.ToString(dtPO.Rows[0]["LASTPO"]) != null &&
                                                            Convert.ToInt32(Convert.ToString(dtPO.Rows[0]["BALQTY"]) == "" ? 0 : Convert.ToInt32(dtPO.Rows[0]["BALQTY"])) > 0)
                                                        {
                                                            blnPOExist = true;


                                                            dtSO = objMainClass.CheckPOSOImeiNo(objMainClass.intCmpId, txtIMEI.Text, "CHECKSOWITHRETURN");

                                                            if (dtSO.Rows.Count > 0)
                                                            {
                                                                if (Convert.ToString(dtSO.Rows[0]["LASTSO"]) != "" && Convert.ToString(dtSO.Rows[0]["LASTSO"]) != string.Empty &&
                                                            Convert.ToString(dtSO.Rows[0]["LASTSO"]) != null &&
                                                            Convert.ToInt32(Convert.ToString(dtSO.Rows[0]["BALQTY"]) == "" ? 0 : Convert.ToInt32(dtSO.Rows[0]["BALQTY"])) > 0)
                                                                {
                                                                    blnPOExist = false;
                                                                }

                                                            }

                                                        }
                                                    }

                                                    if (blnPOExist == true)
                                                    {
                                                        string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                        if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                        {
                                                            message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                        }
                                                        else
                                                        {
                                                            message = message + " New Job card not Created.";
                                                        }

                                                        if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                        {
                                                            message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                        }
                                                        else
                                                        {
                                                            message = message + " New Estimate not Created.";
                                                        }
                                                        message = message + " Job Specification created successfully. New PR Created PR NO. is : " + objPRRespsonse.PRNO + ". PO not created because PO is already made for same IMEI No.! PO No. : " + Convert.ToString(dtPO.Rows[0]["LASTPO"]) + ".!";

                                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                    }
                                                    else
                                                    {

                                                        DataTable dtPOAPI = new DataTable();
                                                        dtPOAPI = objMainClass.GetWAData("INSERTPO", 1, "GETWADATA");
                                                        if (dtPOAPI.Rows.Count > 0)
                                                        {
                                                            List<POCharges> objPOCharges = new List<POCharges>();
                                                            if (othchg > 0)
                                                            {
                                                                objPOCharges.Add(new POCharges
                                                                {
                                                                    CHGAMT = othbasechg,
                                                                    CHGTYPE = ddlCharges.SelectedValue,
                                                                    CMPID = objMainClass.intCmpId,
                                                                    CONDID = 12,
                                                                    CONDTYPE = "GST",
                                                                    GLCODE = "",
                                                                    OPERATOR = "+",
                                                                    PID = 0,
                                                                    PONO = "",
                                                                    RATE = 18,
                                                                    SRNO = 1,
                                                                    TAXAMT = othchgtax,
                                                                });
                                                            }

                                                            List<POTaxation> objPOTaxation = new List<POTaxation>();
                                                            objPOTaxation.Add(new POTaxation
                                                            {
                                                                BASEAMT = basprice,
                                                                CMPID = objMainClass.intCmpId,
                                                                CONDID = 12,
                                                                CONDORDER = 1,
                                                                CONDTYPE = "GST",
                                                                GLCODE = "",
                                                                OPERATOR = "+",
                                                                PID = 0,
                                                                PONO = "",
                                                                POSRNO = 1,
                                                                RATE = 18,
                                                                SRNO = 1,
                                                                TAXAMT = tax,
                                                            });

                                                            List<PODetails> objPODetails = new List<PODetails>();
                                                            objPODetails.Add(new PODetails
                                                            {
                                                                APRVBY = 17,
                                                                APRVDATE = DateTime.Now,
                                                                APRVSTATUS = 260,
                                                                ASSETCD = "",
                                                                BRATE = basprice,
                                                                CAMOUNT = basprice,
                                                                CMPID = objMainClass.intCmpId,
                                                                CSTCENTCD = costcenter,
                                                                DELIDT = DateTime.Now,
                                                                DEVREASON = "OK",
                                                                DISCAMT = 0,
                                                                FROMLOCCD = "",
                                                                FROMPLANTCD = "",
                                                                GLCD = "10010000",
                                                                IMEINO = txtIMEI.Text,
                                                                ITEMDESC = hfItemDesc.Value,
                                                                ITEMGRPID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMGRP"]),
                                                                ITEMID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]),
                                                                //ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",  //Removed AUTO PO AGAINST PO text from remarks
                                                                ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text,
                                                                LOCCD = hfLocCd.Value,
                                                                LOCKAMT = 0,
                                                                PLANTCD = hfPlantCd.Value,
                                                                PONO = "",
                                                                POQTY = 1,
                                                                PRFCNT = "1000",
                                                                PRNO = hfPRNo.Value,
                                                                PRSRNO = 1,
                                                                RATE = purprice,
                                                                REFNO = "",
                                                                REJREASON = "",
                                                                SRNO = 1,
                                                                TAXAMT = 0,
                                                                TRNUM = hfNewJobID.Value,
                                                                UOM = Convert.ToInt32(dtItemDesc.Rows[0]["UOM"]),
                                                            });

                                                            POMaster objPOMaster = new POMaster();
                                                            objPOMaster.ADJAMT = 0;
                                                            objPOMaster.ADVAMT = 0;
                                                            objPOMaster.AGENTNAME = "";
                                                            objPOMaster.APRVBY = 17;
                                                            objPOMaster.APRVDATE = DateTime.Now;
                                                            objPOMaster.CMPID = objMainClass.intCmpId;
                                                            objPOMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                            objPOMaster.CREATEDATE = DateTime.Now;
                                                            objPOMaster.DEPTID = 1;
                                                            objPOMaster.DISCOUNT = 0;
                                                            objPOMaster.NETMATVALUE = basprice;
                                                            objPOMaster.NETPOAMT = netpoamt;
                                                            objPOMaster.NETTAXAMT = nettaxamt;
                                                            objPOMaster.OLDPOAMT = 0;
                                                            objPOMaster.PENDINGAMT = netpoamt;
                                                            objPOMaster.PMTTERMS = ddlPaymentTerms.SelectedItem.Text;
                                                            objPOMaster.PMTTERMSDESC = txtPaymentTermsDesc.Text;
                                                            objPOMaster.POCharge = objPOCharges;
                                                            objPOMaster.PODetail = objPODetails;
                                                            objPOMaster.PODT = DateTime.Now;
                                                            objPOMaster.PONO = "";
                                                            objPOMaster.POTax = objPOTaxation;
                                                            objPOMaster.POTYPE = "MPO";
                                                            objPOMaster.PURTYPE = Convert.ToInt32(ddlPurType.SelectedValue);
                                                            objPOMaster.REMARK = "AUTO PR/PO AGAINST SO";
                                                            objPOMaster.STATUS = 57;
                                                            objPOMaster.TRANCODE = txtTranCode.Text;
                                                            objPOMaster.VENDCODE = txtVendorCode.Text;


                                                            string POURL = Convert.ToString(dtPOAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtPOAPI.Rows[0]["TOKEN"]);
                                                            var clientPO = new RestClient(POURL);
                                                            clientPO.Timeout = -1;
                                                            var requestPO = new RestRequest(Method.POST);
                                                            requestPO.AddHeader("" + Convert.ToString(dtPOAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtPOAPI.Rows[0]["KEYVALUE"]) + "");
                                                            requestPO.AddHeader("Content-Type", "application/json");
                                                            var jsonInputPO = JsonConvert.SerializeObject(objPOMaster);
                                                            requestPO.AddParameter("application/json", jsonInputPO, ParameterType.RequestBody);
                                                            IRestResponse responsePO = clientPO.Execute(requestPO);


                                                            PORespsonse objPORespsonse = new PORespsonse();
                                                            string jsonconnPO = responsePO.Content;
                                                            objPORespsonse = JsonConvert.DeserializeObject<PORespsonse>(jsonconnPO);

                                                            if (responsePO.StatusCode == HttpStatusCode.OK)
                                                            {

                                                                hfPONo.Value = objPORespsonse.PONO;

                                                                #region GRN CODE...

                                                                DataTable dtGRNAPI = new DataTable();
                                                                dtGRNAPI = objMainClass.GetWAData("CREATEGRN", 1, "GETWADATA");

                                                                if (dtGRNAPI.Rows.Count > 0)
                                                                {
                                                                    List<GRNDetials> objGRNDetials = new List<GRNDetials>();
                                                                    objGRNDetials.Add(new GRNDetials
                                                                    {
                                                                        ASSETCD = "",
                                                                        CAMOUNT = basprice,
                                                                        CHLNQTY = 1,
                                                                        CMPID = objMainClass.intCmpId,
                                                                        CSTCENTCD = costcenter,
                                                                        DOCNO = "",
                                                                        DOCTYPE = "103",
                                                                        FINYEAR = DateTime.Now.Year,
                                                                        GLCD = "10010000",
                                                                        ITEMDESC = hfItemDesc.Value,
                                                                        ITEMGRPID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMGRP"]),
                                                                        ITEMID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]),
                                                                        //ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",  //Removed AUTO PO AGAINST PO text from remarks
                                                                        ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text,
                                                                        LOCCD = hfLocCd.Value,
                                                                        PLANTCD = hfPlantCd.Value,
                                                                        PONO = hfPONo.Value,
                                                                        POSRNO = 1,
                                                                        PRFCNT = "1000",
                                                                        QTY = 1,
                                                                        RATE = basprice,
                                                                        SRNO = 1,
                                                                        TRACKNO = hfNewJobID.Value,
                                                                        UOM = Convert.ToInt32(dtItemDesc.Rows[0]["UOM"]),
                                                                    });

                                                                    GRNMaster objGRNMaster = new GRNMaster();
                                                                    objGRNMaster.CHLNDT = DateTime.Now;
                                                                    objGRNMaster.CHLNNO = hfPRNo.Value;
                                                                    objGRNMaster.CMPID = objMainClass.intCmpId;
                                                                    objGRNMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                    objGRNMaster.DEPTCD = 0;
                                                                    objGRNMaster.DOCDATE = DateTime.Now;
                                                                    objGRNMaster.DOCNO = "";
                                                                    objGRNMaster.DOCTYPE = "103";
                                                                    objGRNMaster.EMPNAME = "";
                                                                    objGRNMaster.FINYEAR = DateTime.Now.Year;
                                                                    objGRNMaster.GRNData = objGRNDetials;
                                                                    objGRNMaster.POSTDATE = DateTime.Now;
                                                                    objGRNMaster.REFDOCNO = hfPONo.Value;
                                                                    objGRNMaster.REFDOCYEAR = Convert.ToInt32(txtVendorCode.Text);
                                                                    objGRNMaster.REFNO = hfPRNo.Value;
                                                                    objGRNMaster.REMARK = "AUTO PR/PO AGAINST SO";
                                                                    objGRNMaster.TRANCODE = txtTranCode.Text;


                                                                    string GRNURL = Convert.ToString(dtGRNAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtGRNAPI.Rows[0]["TOKEN"]);
                                                                    var clientGRN = new RestClient(GRNURL);
                                                                    clientGRN.Timeout = -1;
                                                                    var requestGRN = new RestRequest(Method.POST);
                                                                    requestGRN.AddHeader("" + Convert.ToString(dtGRNAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtGRNAPI.Rows[0]["KEYVALUE"]) + "");
                                                                    requestGRN.AddHeader("Content-Type", "application/json");
                                                                    var jsonInputGRN = JsonConvert.SerializeObject(objGRNMaster);
                                                                    requestGRN.AddParameter("application/json", jsonInputGRN, ParameterType.RequestBody);
                                                                    IRestResponse responseGRN = clientGRN.Execute(requestGRN);


                                                                    GRNRespsonse objGRNRespsonse = new GRNRespsonse();
                                                                    string jsonconnGRN = responseGRN.Content;
                                                                    objGRNRespsonse = JsonConvert.DeserializeObject<GRNRespsonse>(jsonconnGRN);

                                                                    if (responseGRN.StatusCode == HttpStatusCode.OK)
                                                                    {
                                                                        hfGRNNo.Value = objGRNRespsonse.GRNNO;

                                                                        #region PB Code...

                                                                        DataTable dtPBAPI = new DataTable();
                                                                        dtPBAPI = objMainClass.GetWAData("INSERTPB", 1, "GETWADATA");
                                                                        if (dtPBAPI.Rows.Count > 0)
                                                                        {
                                                                            List<PBCharges> objPBCharges = new List<PBCharges>();
                                                                            if (othchg > 0)
                                                                            {
                                                                                objPBCharges.Add(new PBCharges
                                                                                {
                                                                                    CHGAMT = othbasechg,
                                                                                    CHGTYPE = ddlCharges.SelectedValue,
                                                                                    CMPID = objMainClass.intCmpId,
                                                                                    CONDID = 12,
                                                                                    CONDTYPE = "GST",
                                                                                    GLCODE = "",
                                                                                    OPERATOR = "+",
                                                                                    PBNO = "",
                                                                                    PID = 0,
                                                                                    RATE = 18,
                                                                                    SRNO = 1,
                                                                                    TAXAMT = othchgtax,
                                                                                });
                                                                            }

                                                                            List<PBTaxation> objPBTaxation = new List<PBTaxation>();
                                                                            objPBTaxation.Add(new PBTaxation
                                                                            {
                                                                                BASEAMT = basprice,
                                                                                CMPID = objMainClass.intCmpId,
                                                                                CONDID = 12,
                                                                                CONDORDER = 1,
                                                                                CONDTYPE = "GST",
                                                                                GLCODE = "",
                                                                                OPERATOR = "+",
                                                                                PBNO = "",
                                                                                PBSRNO = 1,
                                                                                PID = 0,
                                                                                RATE = 18,
                                                                                SRNO = 1,
                                                                                TAXAMT = tax,
                                                                            });

                                                                            List<PBDetails> objPBDetails = new List<PBDetails>();
                                                                            objPBDetails.Add(new PBDetails
                                                                            {
                                                                                ASSETCD = "",
                                                                                BRATE = basprice,
                                                                                CAMOUNT = basprice,
                                                                                CHALLANNO = "",
                                                                                CMPID = objMainClass.intCmpId,
                                                                                CSTCENTCD = costcenter,
                                                                                DISCAMT = 0,
                                                                                GLCD = "10010000",
                                                                                ITEMDESC = hfItemDesc.Value,
                                                                                ITEMGRPID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMGRP"]),
                                                                                ITEMID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]),
                                                                                //ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",  //Removed AUTO PO AGAINST PO text from remarks
                                                                                ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text,
                                                                                LOCCD = hfLocCd.Value,
                                                                                MIRNO = hfGRNNo.Value,
                                                                                MIRSRNO = 1,
                                                                                PBNO = "",
                                                                                PBQTY = 1,
                                                                                PLANTCD = hfPlantCd.Value,
                                                                                PONO = hfPONo.Value,
                                                                                POSRNO = 1,
                                                                                PRFCNT = "1000",
                                                                                RATE = purprice,
                                                                                REFNO = "",
                                                                                SRNO = 1,
                                                                                TAXAMT = 0,
                                                                                TRNUM = hfNewJobID.Value,
                                                                                UOM = Convert.ToInt32(dtItemDesc.Rows[0]["UOM"]),
                                                                            });

                                                                            PBMaster objPBMaster = new PBMaster();
                                                                            objPBMaster.ADJAMT = 0;
                                                                            objPBMaster.BILLAMT = netpoamt;
                                                                            objPBMaster.BILLDT = Convert.ToDateTime(txtBillDate.Text);
                                                                            if (chkINVPO.Checked == true)
                                                                            {
                                                                                objPBMaster.BILLNO = hfPONo.Value;
                                                                            }
                                                                            else
                                                                            {
                                                                                objPBMaster.BILLNO = txtBillNo.Text;
                                                                            }
                                                                            objPBMaster.CMPID = objMainClass.intCmpId;
                                                                            objPBMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                            objPBMaster.DISCOUNT = 0;
                                                                            objPBMaster.NETMATVALUE = basprice;
                                                                            objPBMaster.NETPBAMT = netpoamt;
                                                                            objPBMaster.NETTAXAMT = nettaxamt;
                                                                            objPBMaster.PAIDAMT = 0;
                                                                            objPBMaster.PBCharge = objPBCharges;
                                                                            objPBMaster.PBDetail = objPBDetails;
                                                                            objPBMaster.PBDT = DateTime.Now;
                                                                            objPBMaster.PBNO = "";
                                                                            objPBMaster.PBTax = objPBTaxation;
                                                                            objPBMaster.PBTYPE = "MPB";
                                                                            objPBMaster.PENDINGAMT = netpoamt;
                                                                            objPBMaster.PMTTERMS = ddlPaymentTerms.SelectedItem.Text;
                                                                            objPBMaster.PMTTERMSDESC = txtPaymentTermsDesc.Text;
                                                                            objPBMaster.REMARK = "AUTO PR/PO AGAINST SO";
                                                                            objPBMaster.STATUS = 1;
                                                                            objPBMaster.VENDCODE = txtVendorCode.Text;


                                                                            string PBURL = Convert.ToString(dtPBAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtPBAPI.Rows[0]["TOKEN"]);
                                                                            var clientPB = new RestClient(PBURL);
                                                                            clientPB.Timeout = -1;
                                                                            var requestPB = new RestRequest(Method.POST);
                                                                            requestPB.AddHeader("" + Convert.ToString(dtPBAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtPBAPI.Rows[0]["KEYVALUE"]) + "");
                                                                            requestPB.AddHeader("Content-Type", "application/json");
                                                                            var jsonInputPB = JsonConvert.SerializeObject(objPBMaster);
                                                                            requestPB.AddParameter("application/json", jsonInputPB, ParameterType.RequestBody);
                                                                            IRestResponse responsePB = clientPB.Execute(requestPB);

                                                                            PBRespsonse objPBRespsonse = new PBRespsonse();
                                                                            string jsonconnPB = responsePB.Content;
                                                                            objPBRespsonse = JsonConvert.DeserializeObject<PBRespsonse>(jsonconnPB);

                                                                            if (responsePB.StatusCode == HttpStatusCode.OK)
                                                                            {
                                                                                hfPBNo.Value = objPBRespsonse.PBNO;

                                                                                string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                                if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                                {
                                                                                    message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                                }
                                                                                else
                                                                                {
                                                                                    message = message + " New Job card not Created.";
                                                                                }

                                                                                if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                                {
                                                                                    message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                                }
                                                                                else
                                                                                {
                                                                                    message = message + " New Estimate not Created.";
                                                                                }
                                                                                message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                                message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                                message = message + " New GRN Created. GRN No. is :" + hfGRNNo.Value + ".!";
                                                                                message = message + " New PB Created. PB No. is :" + hfPBNo.Value + ".!";

                                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                            }
                                                                            else
                                                                            {
                                                                                string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                                if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                                {
                                                                                    message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                                }
                                                                                else
                                                                                {
                                                                                    message = message + " New Job card not Created.";
                                                                                }

                                                                                if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                                {
                                                                                    message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                                }
                                                                                else
                                                                                {
                                                                                    message = message + " New Estimate not Created.";
                                                                                }
                                                                                message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                                message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                                message = message + " New GRN Created. GRN No. is :" + objGRNRespsonse.GRNNO + ".";
                                                                                message = message + " PB not Created." + objPBRespsonse.MESSAGE + ".!";

                                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                            }

                                                                        }
                                                                        else
                                                                        {
                                                                            string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                            if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                            {
                                                                                message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                            }
                                                                            else
                                                                            {
                                                                                message = message + " New Job card not Created.";
                                                                            }

                                                                            if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                            {
                                                                                message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                            }
                                                                            else
                                                                            {
                                                                                message = message + " New Estimate not Created.";
                                                                            }
                                                                            message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                            message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                            message = message + " New GRN Created. GRN No. is :" + objGRNRespsonse.GRNNO + ".";
                                                                            message = message + " But PB API not found. Please contact to API provider.!";

                                                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                        }

                                                                        #endregion

                                                                    }
                                                                    else
                                                                    {
                                                                        string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                        if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                        {
                                                                            message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                        }
                                                                        else
                                                                        {
                                                                            message = message + " New Job card not Created.";
                                                                        }

                                                                        if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                        {
                                                                            message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                        }
                                                                        else
                                                                        {
                                                                            message = message + " New Estimate not Created.";
                                                                        }
                                                                        message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                        message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                        message = message + " GRN not Created." + objGRNRespsonse.MESSAGE + ".!";

                                                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                    }



                                                                }
                                                                else
                                                                {
                                                                    string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                    if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                    {
                                                                        message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                    }
                                                                    else
                                                                    {
                                                                        message = message + " New Job card not Created.";
                                                                    }

                                                                    if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                    {
                                                                        message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                    }
                                                                    else
                                                                    {
                                                                        message = message + " New Estimate not Created.";
                                                                    }
                                                                    message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                    message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                    message = message + " But GRN API not found. Please contact API provider.!";

                                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                }


                                                                #endregion

                                                            }
                                                            else
                                                            {
                                                                string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                {
                                                                    message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                }
                                                                else
                                                                {
                                                                    message = message + " New Job card not Created.";
                                                                }

                                                                if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                {
                                                                    message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                }
                                                                else
                                                                {
                                                                    message = message + " New Estimate not Created.";
                                                                }
                                                                message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ". PO not created. " + objPORespsonse.MESSAGE + ".!";

                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                            }


                                                        }
                                                        else
                                                        {
                                                            string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                            if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                            {
                                                                message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                            }
                                                            else
                                                            {
                                                                message = message + " New Job card not Created.";
                                                            }

                                                            if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                            {
                                                                message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                            }
                                                            else
                                                            {
                                                                message = message + " New Estimate not Created.";
                                                            }
                                                            message = message + " Job Specification created successfully. New PR Created PR NO. is : " + objPRRespsonse.PRNO + ". PO not created because PO API not found. Please contact API provider.!";

                                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                        }



                                                    }
                                                }
                                                else
                                                {
                                                    string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                    if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                    {
                                                        message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                    }
                                                    else
                                                    {
                                                        message = message + " New Job card not Created.";
                                                    }

                                                    if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                    {
                                                        message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                    }
                                                    else
                                                    {
                                                        message = message + " New Estimate not Created.";
                                                    }
                                                    message = message + " Job Specification created successfully. " + objPRRespsonse.MESSAGE + ".!";

                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                }

                                                #endregion


                                            }
                                            else
                                            {
                                                decimal purcomm = 0;
                                                decimal purprice = 0;
                                                decimal basprice = 0;
                                                decimal tax = 0;

                                                decimal othchg = 0;
                                                decimal othbasechg = 0;
                                                decimal othchgtax = 0;
                                                decimal nettaxamt = 0;
                                                decimal netpoamt = 0;

                                                purcomm = Math.Round((Convert.ToDecimal(hfPrice.Value) * (RATE) / 100), 2);
                                                purprice = Math.Round((Convert.ToDecimal(hfPrice.Value) - Convert.ToDecimal(purcomm)), 2);
                                                basprice = Math.Round((Convert.ToDecimal(purprice / Convert.ToDecimal("1.18"))), 2);
                                                tax = Math.Round((purprice - basprice), 2);

                                                //nettaxamt = tax;
                                                netpoamt = Math.Round((purprice), 2);

                                                if (txtChgAmt.Text != null && txtChgAmt.Text != string.Empty && txtChgAmt.Text != null)
                                                {
                                                    if (Convert.ToDecimal(txtChgAmt.Text) > 0)
                                                    {
                                                        othchg = Math.Round((Convert.ToDecimal(txtChgAmt.Text)), 2);
                                                        othbasechg = Math.Round((Convert.ToDecimal(othchg / Convert.ToDecimal("1.18"))), 2);
                                                        othchgtax = Math.Round((othchg - othbasechg), 2);
                                                        //nettaxamt = nettaxamt + othchgtax;
                                                        netpoamt = Math.Round((netpoamt + othchg), 2);
                                                    }
                                                }

                                                List<PRDetails> objPRDetails = new List<PRDetails>();
                                                objPRDetails.Add(new PRDetails
                                                {
                                                    ASSETCD = "",
                                                    CAMOUNT = purprice,
                                                    CMPID = objMainClass.intCmpId,
                                                    CSTCENTCD = costcenter,
                                                    DELIDT = DateTime.Now,
                                                    GLCD = "10010000",
                                                    IMEINO = txtIMEI.Text,
                                                    ITEMDESC = hfItemDesc.Value,
                                                    ITEMGRPID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMGRP"]),
                                                    ITEMID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]),
                                                    ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",
                                                    LOCCD = hfLocCd.Value,
                                                    PARTREQNO = 0,
                                                    PLANTCD = hfPlantCd.Value,
                                                    PRBY = "",
                                                    PRFCNT = "1000",
                                                    PRNO = "",
                                                    PRQTY = 1,
                                                    RATE = purprice,
                                                    SRNO = 1,
                                                    STATUS = 2,
                                                    TRNUM = hfNewJobID.Value,
                                                    UOM = Convert.ToInt32(dtItemDesc.Rows[0]["UOM"]),
                                                });

                                                #region Inser PR Method...




                                                PRMaster objPRMaster = new PRMaster();
                                                objPRMaster.CMPID = objMainClass.intCmpId;
                                                objPRMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                objPRMaster.CREATEDATE = DateTime.Now;
                                                objPRMaster.DEPTID = 1;
                                                objPRMaster.ISPRSTO = 0;
                                                objPRMaster.LISTINGID = 0;
                                                objPRMaster.PRDT = DateTime.Now;
                                                objPRMaster.PRNO = "";
                                                objPRMaster.PRTYPE = "MPR";
                                                objPRMaster.REMARK = "AUTO PR/PO AGAINST SO";
                                                objPRMaster.STATUS = 57;
                                                objPRMaster.VENDCODE = "";
                                                objPRMaster.PRDATA = objPRDetails;


                                                string PRURL = Convert.ToString(dtPRAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtPRAPI.Rows[0]["TOKEN"]);
                                                var clientPR = new RestClient(PRURL);
                                                clientPR.Timeout = -1;
                                                var requestPR = new RestRequest(Method.POST);
                                                requestPR.AddHeader("" + Convert.ToString(dtPRAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtPRAPI.Rows[0]["KEYVALUE"]) + "");
                                                requestPR.AddHeader("Content-Type", "application/json");
                                                var jsonInputPR = JsonConvert.SerializeObject(objPRMaster);
                                                requestPR.AddParameter("application/json", jsonInputPR, ParameterType.RequestBody);
                                                IRestResponse responsePR = clientPR.Execute(requestPR);


                                                PRRespsonse objPRRespsonse = new PRRespsonse();
                                                string jsonconnPR = responsePR.Content;
                                                objPRRespsonse = JsonConvert.DeserializeObject<PRRespsonse>(jsonconnPR);

                                                if (responsePR.StatusCode == HttpStatusCode.OK)
                                                {
                                                    hfPRNo.Value = Convert.ToString(objMainClass.strConvertZeroPadding(objPRRespsonse.PRNO));

                                                    bool blnPOExist = false;
                                                    DataTable dtPO = new DataTable();
                                                    DataTable dtSO = new DataTable();
                                                    dtPO = objMainClass.CheckPOSOImeiNo(objMainClass.intCmpId, txtIMEI.Text, "CHECKPOWITHRETURN");
                                                    if (dtPO.Rows.Count > 0)
                                                    {
                                                        if (Convert.ToString(dtPO.Rows[0]["LASTPO"]) != "" && Convert.ToString(dtPO.Rows[0]["LASTPO"]) != string.Empty &&
                                                            Convert.ToString(dtPO.Rows[0]["LASTPO"]) != null &&
                                                            Convert.ToInt32(Convert.ToString(dtPO.Rows[0]["BALQTY"]) == "" ? 0 : Convert.ToInt32(dtPO.Rows[0]["BALQTY"])) > 0)
                                                        {
                                                            blnPOExist = true;


                                                            dtSO = objMainClass.CheckPOSOImeiNo(objMainClass.intCmpId, txtIMEI.Text, "CHECKSOWITHRETURN");

                                                            if (dtSO.Rows.Count > 0)
                                                            {
                                                                if (Convert.ToString(dtSO.Rows[0]["LASTSO"]) != "" && Convert.ToString(dtSO.Rows[0]["LASTSO"]) != string.Empty &&
                                                            Convert.ToString(dtSO.Rows[0]["LASTSO"]) != null &&
                                                            Convert.ToInt32(Convert.ToString(dtSO.Rows[0]["BALQTY"]) == "" ? 0 : Convert.ToInt32(dtSO.Rows[0]["BALQTY"])) > 0)
                                                                {
                                                                    blnPOExist = false;
                                                                }

                                                            }

                                                        }
                                                    }

                                                    if (blnPOExist == true)
                                                    {
                                                        string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                        if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                        {
                                                            message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                        }
                                                        else
                                                        {
                                                            message = message + " New Job card not Created.";
                                                        }

                                                        if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                        {
                                                            message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                        }
                                                        else
                                                        {
                                                            message = message + " New Estimate not Created.";
                                                        }
                                                        message = message + " Job Specification created successfully. New PR Created PR NO. is : " + objPRRespsonse.PRNO + ". PO not created because PO is already made for same IMEI No.! PO No. : " + Convert.ToString(dtPO.Rows[0]["LASTPO"]) + ".!";

                                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                    }
                                                    else
                                                    {

                                                        DataTable dtPOAPI = new DataTable();
                                                        dtPOAPI = objMainClass.GetWAData("INSERTPO", 1, "GETWADATA");
                                                        if (dtPOAPI.Rows.Count > 0)
                                                        {
                                                            List<POCharges> objPOCharges = new List<POCharges>();
                                                            if (othchg > 0)
                                                            {
                                                                objPOCharges.Add(new POCharges
                                                                {
                                                                    CHGAMT = othchg,
                                                                    CHGTYPE = ddlCharges.SelectedValue,
                                                                    CMPID = objMainClass.intCmpId,
                                                                    CONDID = 0,
                                                                    CONDTYPE = "",
                                                                    GLCODE = "",
                                                                    OPERATOR = "+",
                                                                    PID = 0,
                                                                    PONO = "",
                                                                    RATE = 0,
                                                                    SRNO = 1,
                                                                    TAXAMT = 0,
                                                                });
                                                            }

                                                            List<POTaxation> objPOTaxation = new List<POTaxation>();
                                                            //objPOTaxation.Add(new POTaxation
                                                            //{
                                                            //    BASEAMT = basprice,
                                                            //    CMPID = objMainClass.intCmpId,
                                                            //    CONDID = 12,
                                                            //    CONDORDER = 1,
                                                            //    CONDTYPE = "GST",
                                                            //    GLCODE = "",
                                                            //    OPERATOR = "+",
                                                            //    PID = 0,
                                                            //    PONO = "",
                                                            //    POSRNO = 1,
                                                            //    RATE = 18,
                                                            //    SRNO = 1,
                                                            //    TAXAMT = tax,
                                                            //});

                                                            List<PODetails> objPODetails = new List<PODetails>();
                                                            objPODetails.Add(new PODetails
                                                            {
                                                                APRVBY = 17,
                                                                APRVDATE = DateTime.Now,
                                                                APRVSTATUS = 260,
                                                                ASSETCD = "",
                                                                BRATE = purprice,
                                                                CAMOUNT = purprice,
                                                                CMPID = objMainClass.intCmpId,
                                                                CSTCENTCD = costcenter,
                                                                DELIDT = DateTime.Now,
                                                                DEVREASON = "OK",
                                                                DISCAMT = 0,
                                                                FROMLOCCD = "",
                                                                FROMPLANTCD = "",
                                                                GLCD = "10010000",
                                                                IMEINO = txtIMEI.Text,
                                                                ITEMDESC = hfItemDesc.Value,
                                                                ITEMGRPID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMGRP"]),
                                                                ITEMID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]),
                                                                //ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",  //Removed AUTO PO AGAINST PO text from remarks
                                                                ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text,
                                                                LOCCD = hfLocCd.Value,
                                                                LOCKAMT = 0,
                                                                PLANTCD = hfPlantCd.Value,
                                                                PONO = "",
                                                                POQTY = 1,
                                                                PRFCNT = "1000",
                                                                PRNO = hfPRNo.Value,
                                                                PRSRNO = 1,
                                                                RATE = purprice,
                                                                REFNO = "",
                                                                REJREASON = "",
                                                                SRNO = 1,
                                                                TAXAMT = 0,
                                                                TRNUM = hfNewJobID.Value,
                                                                UOM = Convert.ToInt32(dtItemDesc.Rows[0]["UOM"]),
                                                            });

                                                            POMaster objPOMaster = new POMaster();
                                                            objPOMaster.ADJAMT = 0;
                                                            objPOMaster.ADVAMT = 0;
                                                            objPOMaster.AGENTNAME = "";
                                                            objPOMaster.APRVBY = 17;
                                                            objPOMaster.APRVDATE = DateTime.Now;
                                                            objPOMaster.CMPID = objMainClass.intCmpId;
                                                            objPOMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                            objPOMaster.CREATEDATE = DateTime.Now;
                                                            objPOMaster.DEPTID = 1;
                                                            objPOMaster.DISCOUNT = 0;
                                                            objPOMaster.NETMATVALUE = purprice;
                                                            objPOMaster.NETPOAMT = netpoamt;
                                                            objPOMaster.NETTAXAMT = nettaxamt;
                                                            objPOMaster.OLDPOAMT = 0;
                                                            objPOMaster.PENDINGAMT = netpoamt;
                                                            objPOMaster.PMTTERMS = ddlPaymentTerms.SelectedItem.Text;
                                                            objPOMaster.PMTTERMSDESC = txtPaymentTermsDesc.Text;
                                                            objPOMaster.POCharge = objPOCharges;
                                                            objPOMaster.PODetail = objPODetails;
                                                            objPOMaster.PODT = DateTime.Now;
                                                            objPOMaster.PONO = "";
                                                            objPOMaster.POTax = objPOTaxation;
                                                            objPOMaster.POTYPE = "MPO";
                                                            objPOMaster.PURTYPE = Convert.ToInt32(ddlPurType.SelectedValue);
                                                            objPOMaster.REMARK = "AUTO PR/PO AGAINST SO";
                                                            objPOMaster.STATUS = 57;
                                                            objPOMaster.TRANCODE = txtTranCode.Text;
                                                            objPOMaster.VENDCODE = txtVendorCode.Text;


                                                            string POURL = Convert.ToString(dtPOAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtPOAPI.Rows[0]["TOKEN"]);
                                                            var clientPO = new RestClient(POURL);
                                                            clientPO.Timeout = -1;
                                                            var requestPO = new RestRequest(Method.POST);
                                                            requestPO.AddHeader("" + Convert.ToString(dtPOAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtPOAPI.Rows[0]["KEYVALUE"]) + "");
                                                            requestPO.AddHeader("Content-Type", "application/json");
                                                            var jsonInputPO = JsonConvert.SerializeObject(objPOMaster);
                                                            requestPO.AddParameter("application/json", jsonInputPO, ParameterType.RequestBody);
                                                            IRestResponse responsePO = clientPO.Execute(requestPO);


                                                            PORespsonse objPORespsonse = new PORespsonse();
                                                            string jsonconnPO = responsePO.Content;
                                                            objPORespsonse = JsonConvert.DeserializeObject<PORespsonse>(jsonconnPO);

                                                            if (responsePO.StatusCode == HttpStatusCode.OK)
                                                            {

                                                                hfPONo.Value = objPORespsonse.PONO;

                                                                #region GRN CODE...

                                                                DataTable dtGRNAPI = new DataTable();
                                                                dtGRNAPI = objMainClass.GetWAData("CREATEGRN", 1, "GETWADATA");

                                                                if (dtGRNAPI.Rows.Count > 0)
                                                                {
                                                                    List<GRNDetials> objGRNDetials = new List<GRNDetials>();
                                                                    objGRNDetials.Add(new GRNDetials
                                                                    {
                                                                        ASSETCD = "",
                                                                        CAMOUNT = purprice,
                                                                        CHLNQTY = 1,
                                                                        CMPID = objMainClass.intCmpId,
                                                                        CSTCENTCD = costcenter,
                                                                        DOCNO = "",
                                                                        DOCTYPE = "103",
                                                                        FINYEAR = DateTime.Now.Year,
                                                                        GLCD = "10010000",
                                                                        ITEMDESC = hfItemDesc.Value,
                                                                        ITEMGRPID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMGRP"]),
                                                                        ITEMID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]),
                                                                        //ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",  //Removed AUTO PO AGAINST PO text from remarks
                                                                        ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text,
                                                                        LOCCD = hfLocCd.Value,
                                                                        PLANTCD = hfPlantCd.Value,
                                                                        PONO = hfPONo.Value,
                                                                        POSRNO = 1,
                                                                        PRFCNT = "1000",
                                                                        QTY = 1,
                                                                        RATE = purprice,
                                                                        SRNO = 1,
                                                                        TRACKNO = hfNewJobID.Value,
                                                                        UOM = Convert.ToInt32(dtItemDesc.Rows[0]["UOM"]),
                                                                    });

                                                                    GRNMaster objGRNMaster = new GRNMaster();
                                                                    objGRNMaster.CHLNDT = DateTime.Now;
                                                                    objGRNMaster.CHLNNO = hfPRNo.Value;
                                                                    objGRNMaster.CMPID = objMainClass.intCmpId;
                                                                    objGRNMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                    objGRNMaster.DEPTCD = 0;
                                                                    objGRNMaster.DOCDATE = DateTime.Now;
                                                                    objGRNMaster.DOCNO = "";
                                                                    objGRNMaster.DOCTYPE = "103";
                                                                    objGRNMaster.EMPNAME = "";
                                                                    objGRNMaster.FINYEAR = DateTime.Now.Year;
                                                                    objGRNMaster.GRNData = objGRNDetials;
                                                                    objGRNMaster.POSTDATE = DateTime.Now;
                                                                    objGRNMaster.REFDOCNO = hfPONo.Value;
                                                                    objGRNMaster.REFDOCYEAR = Convert.ToInt32(txtVendorCode.Text);
                                                                    objGRNMaster.REFNO = hfPRNo.Value;
                                                                    objGRNMaster.REMARK = "AUTO PR/PO AGAINST SO";
                                                                    objGRNMaster.TRANCODE = txtTranCode.Text;


                                                                    string GRNURL = Convert.ToString(dtGRNAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtGRNAPI.Rows[0]["TOKEN"]);
                                                                    var clientGRN = new RestClient(GRNURL);
                                                                    clientGRN.Timeout = -1;
                                                                    var requestGRN = new RestRequest(Method.POST);
                                                                    requestGRN.AddHeader("" + Convert.ToString(dtGRNAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtGRNAPI.Rows[0]["KEYVALUE"]) + "");
                                                                    requestGRN.AddHeader("Content-Type", "application/json");
                                                                    var jsonInputGRN = JsonConvert.SerializeObject(objGRNMaster);
                                                                    requestGRN.AddParameter("application/json", jsonInputGRN, ParameterType.RequestBody);
                                                                    IRestResponse responseGRN = clientGRN.Execute(requestGRN);


                                                                    GRNRespsonse objGRNRespsonse = new GRNRespsonse();
                                                                    string jsonconnGRN = responseGRN.Content;
                                                                    objGRNRespsonse = JsonConvert.DeserializeObject<GRNRespsonse>(jsonconnGRN);

                                                                    if (responseGRN.StatusCode == HttpStatusCode.OK)
                                                                    {
                                                                        hfGRNNo.Value = objGRNRespsonse.GRNNO;

                                                                        #region PB Code...

                                                                        DataTable dtPBAPI = new DataTable();
                                                                        dtPBAPI = objMainClass.GetWAData("INSERTPB", 1, "GETWADATA");
                                                                        if (dtPBAPI.Rows.Count > 0)
                                                                        {
                                                                            List<PBCharges> objPBCharges = new List<PBCharges>();
                                                                            if (othchg > 0)
                                                                            {
                                                                                objPBCharges.Add(new PBCharges
                                                                                {
                                                                                    CHGAMT = othchg,
                                                                                    CHGTYPE = ddlCharges.SelectedValue,
                                                                                    CMPID = objMainClass.intCmpId,
                                                                                    CONDID = 12,
                                                                                    CONDTYPE = "GST",
                                                                                    GLCODE = "",
                                                                                    OPERATOR = "+",
                                                                                    PBNO = "",
                                                                                    PID = 0,
                                                                                    RATE = 18,
                                                                                    SRNO = 1,
                                                                                    TAXAMT = 0,
                                                                                });
                                                                            }

                                                                            List<PBTaxation> objPBTaxation = new List<PBTaxation>();
                                                                            //objPBTaxation.Add(new PBTaxation
                                                                            //{
                                                                            //    BASEAMT = basprice,
                                                                            //    CMPID = objMainClass.intCmpId,
                                                                            //    CONDID = 12,
                                                                            //    CONDORDER = 1,
                                                                            //    CONDTYPE = "GST",
                                                                            //    GLCODE = "",
                                                                            //    OPERATOR = "+",
                                                                            //    PBNO = "",
                                                                            //    PBSRNO = 1,
                                                                            //    PID = 0,
                                                                            //    RATE = 18,
                                                                            //    SRNO = 1,
                                                                            //    TAXAMT = tax,
                                                                            //});

                                                                            List<PBDetails> objPBDetails = new List<PBDetails>();
                                                                            objPBDetails.Add(new PBDetails
                                                                            {
                                                                                ASSETCD = "",
                                                                                BRATE = purprice,
                                                                                CAMOUNT = purprice,
                                                                                CHALLANNO = "",
                                                                                CMPID = objMainClass.intCmpId,
                                                                                CSTCENTCD = costcenter,
                                                                                DISCAMT = 0,
                                                                                GLCD = "10010000",
                                                                                ITEMDESC = hfItemDesc.Value,
                                                                                ITEMGRPID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMGRP"]),
                                                                                ITEMID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]),
                                                                                //ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",  //Removed AUTO PO AGAINST PO text from remarks
                                                                                ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text,
                                                                                LOCCD = hfLocCd.Value,
                                                                                MIRNO = hfGRNNo.Value,
                                                                                MIRSRNO = 1,
                                                                                PBNO = "",
                                                                                PBQTY = 1,
                                                                                PLANTCD = hfPlantCd.Value,
                                                                                PONO = hfPONo.Value,
                                                                                POSRNO = 1,
                                                                                PRFCNT = "1000",
                                                                                RATE = purprice,
                                                                                REFNO = "",
                                                                                SRNO = 1,
                                                                                TAXAMT = 0,
                                                                                TRNUM = hfNewJobID.Value,
                                                                                UOM = Convert.ToInt32(dtItemDesc.Rows[0]["UOM"]),
                                                                            });

                                                                            PBMaster objPBMaster = new PBMaster();
                                                                            objPBMaster.ADJAMT = 0;
                                                                            objPBMaster.BILLAMT = netpoamt;
                                                                            objPBMaster.BILLDT = Convert.ToDateTime(txtBillDate.Text);
                                                                            if (chkINVPO.Checked == true)
                                                                            {
                                                                                objPBMaster.BILLNO = hfPONo.Value;
                                                                            }
                                                                            else
                                                                            {
                                                                                objPBMaster.BILLNO = txtBillNo.Text;
                                                                            }
                                                                            objPBMaster.CMPID = objMainClass.intCmpId;
                                                                            objPBMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                            objPBMaster.DISCOUNT = 0;
                                                                            objPBMaster.NETMATVALUE = purprice;
                                                                            objPBMaster.NETPBAMT = netpoamt;
                                                                            objPBMaster.NETTAXAMT = nettaxamt;
                                                                            objPBMaster.PAIDAMT = 0;
                                                                            objPBMaster.PBCharge = objPBCharges;
                                                                            objPBMaster.PBDetail = objPBDetails;
                                                                            objPBMaster.PBDT = DateTime.Now;
                                                                            objPBMaster.PBNO = "";
                                                                            objPBMaster.PBTax = objPBTaxation;
                                                                            objPBMaster.PBTYPE = "MPB";
                                                                            objPBMaster.PENDINGAMT = netpoamt;
                                                                            objPBMaster.PMTTERMS = ddlPaymentTerms.SelectedItem.Text;
                                                                            objPBMaster.PMTTERMSDESC = txtPaymentTermsDesc.Text;
                                                                            objPBMaster.REMARK = "AUTO PR/PO AGAINST SO";
                                                                            objPBMaster.STATUS = 1;
                                                                            objPBMaster.VENDCODE = txtVendorCode.Text;


                                                                            string PBURL = Convert.ToString(dtPBAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtPBAPI.Rows[0]["TOKEN"]);
                                                                            var clientPB = new RestClient(PBURL);
                                                                            clientPB.Timeout = -1;
                                                                            var requestPB = new RestRequest(Method.POST);
                                                                            requestPB.AddHeader("" + Convert.ToString(dtPBAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtPBAPI.Rows[0]["KEYVALUE"]) + "");
                                                                            requestPB.AddHeader("Content-Type", "application/json");
                                                                            var jsonInputPB = JsonConvert.SerializeObject(objPBMaster);
                                                                            requestPB.AddParameter("application/json", jsonInputPB, ParameterType.RequestBody);
                                                                            IRestResponse responsePB = clientPB.Execute(requestPB);

                                                                            PBRespsonse objPBRespsonse = new PBRespsonse();
                                                                            string jsonconnPB = responsePB.Content;
                                                                            objPBRespsonse = JsonConvert.DeserializeObject<PBRespsonse>(jsonconnPB);

                                                                            if (responsePB.StatusCode == HttpStatusCode.OK)
                                                                            {
                                                                                hfPBNo.Value = objPBRespsonse.PBNO;

                                                                                string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                                if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                                {
                                                                                    message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                                }
                                                                                else
                                                                                {
                                                                                    message = message + " New Job card not Created.";
                                                                                }

                                                                                if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                                {
                                                                                    message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                                }
                                                                                else
                                                                                {
                                                                                    message = message + " New Estimate not Created.";
                                                                                }
                                                                                message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                                message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                                message = message + " New GRN Created. GRN No. is :" + hfGRNNo.Value + ".!";
                                                                                message = message + " New PB Created. PB No. is :" + hfPBNo.Value + ".!";

                                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                            }
                                                                            else
                                                                            {
                                                                                string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                                if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                                {
                                                                                    message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                                }
                                                                                else
                                                                                {
                                                                                    message = message + " New Job card not Created.";
                                                                                }

                                                                                if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                                {
                                                                                    message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                                }
                                                                                else
                                                                                {
                                                                                    message = message + " New Estimate not Created.";
                                                                                }
                                                                                message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                                message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                                message = message + " New GRN Created. GRN No. is :" + objGRNRespsonse.GRNNO + ".";
                                                                                message = message + " PB not Created." + objPBRespsonse.MESSAGE + ".!";

                                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                            }

                                                                        }
                                                                        else
                                                                        {
                                                                            string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                            if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                            {
                                                                                message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                            }
                                                                            else
                                                                            {
                                                                                message = message + " New Job card not Created.";
                                                                            }

                                                                            if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                            {
                                                                                message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                            }
                                                                            else
                                                                            {
                                                                                message = message + " New Estimate not Created.";
                                                                            }
                                                                            message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                            message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                            message = message + " New GRN Created. GRN No. is :" + objGRNRespsonse.GRNNO + ".";
                                                                            message = message + " But PB API not found. Please contact to API provider.!";

                                                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                        }

                                                                        #endregion

                                                                    }
                                                                    else
                                                                    {
                                                                        string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                        if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                        {
                                                                            message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                        }
                                                                        else
                                                                        {
                                                                            message = message + " New Job card not Created.";
                                                                        }

                                                                        if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                        {
                                                                            message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                        }
                                                                        else
                                                                        {
                                                                            message = message + " New Estimate not Created.";
                                                                        }
                                                                        message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                        message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                        message = message + " GRN not Created." + objGRNRespsonse.MESSAGE + ".!";

                                                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                    }



                                                                }
                                                                else
                                                                {
                                                                    string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                    if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                    {
                                                                        message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                    }
                                                                    else
                                                                    {
                                                                        message = message + " New Job card not Created.";
                                                                    }

                                                                    if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                    {
                                                                        message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                    }
                                                                    else
                                                                    {
                                                                        message = message + " New Estimate not Created.";
                                                                    }
                                                                    message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                    message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                    message = message + " But GRN API not found. Please contact API provider.!";

                                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                }


                                                                #endregion

                                                            }
                                                            else
                                                            {
                                                                string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                {
                                                                    message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                }
                                                                else
                                                                {
                                                                    message = message + " New Job card not Created.";
                                                                }

                                                                if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                {
                                                                    message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                }
                                                                else
                                                                {
                                                                    message = message + " New Estimate not Created.";
                                                                }
                                                                message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ". PO not created. " + objPORespsonse.MESSAGE + ".!";

                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                            }


                                                        }
                                                        else
                                                        {
                                                            string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                            if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                            {
                                                                message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                            }
                                                            else
                                                            {
                                                                message = message + " New Job card not Created.";
                                                            }

                                                            if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                            {
                                                                message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                            }
                                                            else
                                                            {
                                                                message = message + " New Estimate not Created.";
                                                            }
                                                            message = message + " Job Specification created successfully. New PR Created PR NO. is : " + objPRRespsonse.PRNO + ". PO not created because PO API not found. Please contact API provider.!";

                                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                        }



                                                    }
                                                }
                                                else
                                                {
                                                    string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                    if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                    {
                                                        message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                    }
                                                    else
                                                    {
                                                        message = message + " New Job card not Created.";
                                                    }

                                                    if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                    {
                                                        message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                    }
                                                    else
                                                    {
                                                        message = message + " New Estimate not Created.";
                                                    }
                                                    message = message + " Job Specification created successfully. " + objPRRespsonse.MESSAGE + ".!";

                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                }

                                                #endregion


                                            }
                                        }
                                        else
                                        {
                                            if (chkGST.Checked == true)
                                            {
                                                decimal purcomm = 0;
                                                decimal purprice = 0;
                                                decimal basprice = 0;
                                                decimal tax = 0;

                                                decimal othchg = 0;
                                                decimal othbasechg = 0;
                                                decimal othchgtax = 0;
                                                decimal nettaxamt = 0;
                                                decimal netpoamt = 0;

                                                purcomm = Math.Round(RATE);
                                                purprice = Math.Round((Convert.ToDecimal(hfPrice.Value) - Convert.ToDecimal(purcomm)), 2);
                                                basprice = Math.Round((Convert.ToDecimal(purprice / Convert.ToDecimal("1.18"))), 2);
                                                tax = Math.Round((purprice - basprice), 2);

                                                nettaxamt = Math.Round((tax), 2);
                                                netpoamt = Math.Round((purprice), 2);


                                                if (txtChgAmt.Text != null && txtChgAmt.Text != string.Empty && txtChgAmt.Text != null)
                                                {
                                                    if (Convert.ToDecimal(txtChgAmt.Text) > 0)
                                                    {
                                                        othchg = Math.Round((Convert.ToDecimal(txtChgAmt.Text)), 2);
                                                        othbasechg = Math.Round((Convert.ToDecimal(othchg / Convert.ToDecimal("1.18"))), 2);
                                                        othchgtax = Math.Round((othchg - othbasechg), 2);
                                                        nettaxamt = Math.Round((nettaxamt + othchgtax), 2);
                                                        netpoamt = Math.Round((netpoamt + othchg), 2);
                                                    }
                                                }

                                                List<PRDetails> objPRDetails = new List<PRDetails>();
                                                objPRDetails.Add(new PRDetails
                                                {
                                                    ASSETCD = "",
                                                    CAMOUNT = purprice,
                                                    CMPID = objMainClass.intCmpId,
                                                    CSTCENTCD = costcenter,
                                                    DELIDT = DateTime.Now,
                                                    GLCD = "10010000",
                                                    IMEINO = txtIMEI.Text,
                                                    ITEMDESC = hfItemDesc.Value,
                                                    ITEMGRPID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMGRP"]),
                                                    ITEMID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]),
                                                    ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",
                                                    LOCCD = hfLocCd.Value,
                                                    PARTREQNO = 0,
                                                    PLANTCD = hfPlantCd.Value,
                                                    PRBY = "",
                                                    PRFCNT = "1000",
                                                    PRNO = "",
                                                    PRQTY = 1,
                                                    RATE = purprice,
                                                    SRNO = 1,
                                                    STATUS = 2,
                                                    TRNUM = hfNewJobID.Value,
                                                    UOM = Convert.ToInt32(dtItemDesc.Rows[0]["UOM"]),
                                                });

                                                #region Inser PR Method...




                                                PRMaster objPRMaster = new PRMaster();
                                                objPRMaster.CMPID = objMainClass.intCmpId;
                                                objPRMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                objPRMaster.CREATEDATE = DateTime.Now;
                                                objPRMaster.DEPTID = 1;
                                                objPRMaster.ISPRSTO = 0;
                                                objPRMaster.LISTINGID = 0;
                                                objPRMaster.PRDT = DateTime.Now;
                                                objPRMaster.PRNO = "";
                                                objPRMaster.PRTYPE = "MPR";
                                                objPRMaster.REMARK = "AUTO PR/PO AGAINST SO";
                                                objPRMaster.STATUS = 57;
                                                objPRMaster.VENDCODE = "";
                                                objPRMaster.PRDATA = objPRDetails;


                                                string PRURL = Convert.ToString(dtPRAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtPRAPI.Rows[0]["TOKEN"]);
                                                var clientPR = new RestClient(PRURL);
                                                clientPR.Timeout = -1;
                                                var requestPR = new RestRequest(Method.POST);
                                                requestPR.AddHeader("" + Convert.ToString(dtPRAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtPRAPI.Rows[0]["KEYVALUE"]) + "");
                                                requestPR.AddHeader("Content-Type", "application/json");
                                                var jsonInputPR = JsonConvert.SerializeObject(objPRMaster);
                                                requestPR.AddParameter("application/json", jsonInputPR, ParameterType.RequestBody);
                                                IRestResponse responsePR = clientPR.Execute(requestPR);


                                                PRRespsonse objPRRespsonse = new PRRespsonse();
                                                string jsonconnPR = responsePR.Content;
                                                objPRRespsonse = JsonConvert.DeserializeObject<PRRespsonse>(jsonconnPR);

                                                if (responsePR.StatusCode == HttpStatusCode.OK)
                                                {
                                                    hfPRNo.Value = Convert.ToString(objMainClass.strConvertZeroPadding(objPRRespsonse.PRNO));

                                                    bool blnPOExist = false;
                                                    DataTable dtPO = new DataTable();
                                                    DataTable dtSO = new DataTable();
                                                    dtPO = objMainClass.CheckPOSOImeiNo(objMainClass.intCmpId, txtIMEI.Text, "CHECKPOWITHRETURN");
                                                    if (dtPO.Rows.Count > 0)
                                                    {
                                                        if (Convert.ToString(dtPO.Rows[0]["LASTPO"]) != "" && Convert.ToString(dtPO.Rows[0]["LASTPO"]) != string.Empty &&
                                                            Convert.ToString(dtPO.Rows[0]["LASTPO"]) != null &&
                                                            Convert.ToInt32(Convert.ToString(dtPO.Rows[0]["BALQTY"]) == "" ? 0 : Convert.ToInt32(dtPO.Rows[0]["BALQTY"])) > 0)
                                                        {
                                                            blnPOExist = true;


                                                            dtSO = objMainClass.CheckPOSOImeiNo(objMainClass.intCmpId, txtIMEI.Text, "CHECKSOWITHRETURN");

                                                            if (dtSO.Rows.Count > 0)
                                                            {
                                                                if (Convert.ToString(dtSO.Rows[0]["LASTSO"]) != "" && Convert.ToString(dtSO.Rows[0]["LASTSO"]) != string.Empty &&
                                                            Convert.ToString(dtSO.Rows[0]["LASTSO"]) != null &&
                                                            Convert.ToInt32(Convert.ToString(dtSO.Rows[0]["BALQTY"]) == "" ? 0 : Convert.ToInt32(dtSO.Rows[0]["BALQTY"])) > 0)
                                                                {
                                                                    blnPOExist = false;
                                                                }

                                                            }

                                                        }
                                                    }

                                                    if (blnPOExist == true)
                                                    {
                                                        string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                        if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                        {
                                                            message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                        }
                                                        else
                                                        {
                                                            message = message + " New Job card not Created.";
                                                        }

                                                        if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                        {
                                                            message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                        }
                                                        else
                                                        {
                                                            message = message + " New Estimate not Created.";
                                                        }
                                                        message = message + " Job Specification created successfully. New PR Created PR NO. is : " + objPRRespsonse.PRNO + ". PO not created because PO is already made for same IMEI No.! PO No. : " + Convert.ToString(dtPO.Rows[0]["LASTPO"]) + ".!";

                                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                    }
                                                    else
                                                    {

                                                        DataTable dtPOAPI = new DataTable();
                                                        dtPOAPI = objMainClass.GetWAData("INSERTPO", 1, "GETWADATA");
                                                        if (dtPOAPI.Rows.Count > 0)
                                                        {
                                                            List<POCharges> objPOCharges = new List<POCharges>();
                                                            if (othchg > 0)
                                                            {
                                                                objPOCharges.Add(new POCharges
                                                                {
                                                                    CHGAMT = othbasechg,
                                                                    CHGTYPE = ddlCharges.SelectedValue,
                                                                    CMPID = objMainClass.intCmpId,
                                                                    CONDID = 12,
                                                                    CONDTYPE = "GST",
                                                                    GLCODE = "",
                                                                    OPERATOR = "+",
                                                                    PID = 0,
                                                                    PONO = "",
                                                                    RATE = 18,
                                                                    SRNO = 1,
                                                                    TAXAMT = othchgtax,
                                                                });
                                                            }

                                                            List<POTaxation> objPOTaxation = new List<POTaxation>();
                                                            objPOTaxation.Add(new POTaxation
                                                            {
                                                                BASEAMT = basprice,
                                                                CMPID = objMainClass.intCmpId,
                                                                CONDID = 12,
                                                                CONDORDER = 1,
                                                                CONDTYPE = "GST",
                                                                GLCODE = "",
                                                                OPERATOR = "+",
                                                                PID = 0,
                                                                PONO = "",
                                                                POSRNO = 1,
                                                                RATE = 18,
                                                                SRNO = 1,
                                                                TAXAMT = tax,
                                                            });

                                                            List<PODetails> objPODetails = new List<PODetails>();
                                                            objPODetails.Add(new PODetails
                                                            {
                                                                APRVBY = 17,
                                                                APRVDATE = DateTime.Now,
                                                                APRVSTATUS = 260,
                                                                ASSETCD = "",
                                                                BRATE = basprice,
                                                                CAMOUNT = basprice,
                                                                CMPID = objMainClass.intCmpId,
                                                                CSTCENTCD = costcenter,
                                                                DELIDT = DateTime.Now,
                                                                DEVREASON = "OK",
                                                                DISCAMT = 0,
                                                                FROMLOCCD = "",
                                                                FROMPLANTCD = "",
                                                                GLCD = "10010000",
                                                                IMEINO = txtIMEI.Text,
                                                                ITEMDESC = hfItemDesc.Value,
                                                                ITEMGRPID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMGRP"]),
                                                                ITEMID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]),
                                                                //ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",  //Removed AUTO PO AGAINST PO text from remarks
                                                                ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text,
                                                                LOCCD = hfLocCd.Value,
                                                                LOCKAMT = 0,
                                                                PLANTCD = hfPlantCd.Value,
                                                                PONO = "",
                                                                POQTY = 1,
                                                                PRFCNT = "1000",
                                                                PRNO = hfPRNo.Value,
                                                                PRSRNO = 1,
                                                                RATE = purprice,
                                                                REFNO = "",
                                                                REJREASON = "",
                                                                SRNO = 1,
                                                                TAXAMT = 0,
                                                                TRNUM = hfNewJobID.Value,
                                                                UOM = Convert.ToInt32(dtItemDesc.Rows[0]["UOM"]),
                                                            });

                                                            POMaster objPOMaster = new POMaster();
                                                            objPOMaster.ADJAMT = 0;
                                                            objPOMaster.ADVAMT = 0;
                                                            objPOMaster.AGENTNAME = "";
                                                            objPOMaster.APRVBY = 17;
                                                            objPOMaster.APRVDATE = DateTime.Now;
                                                            objPOMaster.CMPID = objMainClass.intCmpId;
                                                            objPOMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                            objPOMaster.CREATEDATE = DateTime.Now;
                                                            objPOMaster.DEPTID = 1;
                                                            objPOMaster.DISCOUNT = 0;
                                                            objPOMaster.NETMATVALUE = basprice;
                                                            objPOMaster.NETPOAMT = netpoamt;
                                                            objPOMaster.NETTAXAMT = nettaxamt;
                                                            objPOMaster.OLDPOAMT = 0;
                                                            objPOMaster.PENDINGAMT = netpoamt;
                                                            objPOMaster.PMTTERMS = ddlPaymentTerms.SelectedItem.Text;
                                                            objPOMaster.PMTTERMSDESC = txtPaymentTermsDesc.Text;
                                                            objPOMaster.POCharge = objPOCharges;
                                                            objPOMaster.PODetail = objPODetails;
                                                            objPOMaster.PODT = DateTime.Now;
                                                            objPOMaster.PONO = "";
                                                            objPOMaster.POTax = objPOTaxation;
                                                            objPOMaster.POTYPE = "MPO";
                                                            objPOMaster.PURTYPE = Convert.ToInt32(ddlPurType.SelectedValue);
                                                            objPOMaster.REMARK = "AUTO PR/PO AGAINST SO";
                                                            objPOMaster.STATUS = 57;
                                                            objPOMaster.TRANCODE = txtTranCode.Text;
                                                            objPOMaster.VENDCODE = txtVendorCode.Text;


                                                            string POURL = Convert.ToString(dtPOAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtPOAPI.Rows[0]["TOKEN"]);
                                                            var clientPO = new RestClient(POURL);
                                                            clientPO.Timeout = -1;
                                                            var requestPO = new RestRequest(Method.POST);
                                                            requestPO.AddHeader("" + Convert.ToString(dtPOAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtPOAPI.Rows[0]["KEYVALUE"]) + "");
                                                            requestPO.AddHeader("Content-Type", "application/json");
                                                            var jsonInputPO = JsonConvert.SerializeObject(objPOMaster);
                                                            requestPO.AddParameter("application/json", jsonInputPO, ParameterType.RequestBody);
                                                            IRestResponse responsePO = clientPO.Execute(requestPO);


                                                            PORespsonse objPORespsonse = new PORespsonse();
                                                            string jsonconnPO = responsePO.Content;
                                                            objPORespsonse = JsonConvert.DeserializeObject<PORespsonse>(jsonconnPO);

                                                            if (responsePO.StatusCode == HttpStatusCode.OK)
                                                            {

                                                                hfPONo.Value = objPORespsonse.PONO;

                                                                #region GRN CODE...

                                                                DataTable dtGRNAPI = new DataTable();
                                                                dtGRNAPI = objMainClass.GetWAData("CREATEGRN", 1, "GETWADATA");

                                                                if (dtGRNAPI.Rows.Count > 0)
                                                                {
                                                                    List<GRNDetials> objGRNDetials = new List<GRNDetials>();
                                                                    objGRNDetials.Add(new GRNDetials
                                                                    {
                                                                        ASSETCD = "",
                                                                        CAMOUNT = purprice,
                                                                        CHLNQTY = 1,
                                                                        CMPID = objMainClass.intCmpId,
                                                                        CSTCENTCD = costcenter,
                                                                        DOCNO = "",
                                                                        DOCTYPE = "103",
                                                                        FINYEAR = DateTime.Now.Year,
                                                                        GLCD = "10010000",
                                                                        ITEMDESC = hfItemDesc.Value,
                                                                        ITEMGRPID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMGRP"]),
                                                                        ITEMID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]),
                                                                        //ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",  //Removed AUTO PO AGAINST PO text from remarks
                                                                        ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text,
                                                                        LOCCD = hfLocCd.Value,
                                                                        PLANTCD = hfPlantCd.Value,
                                                                        PONO = hfPONo.Value,
                                                                        POSRNO = 1,
                                                                        PRFCNT = "1000",
                                                                        QTY = 1,
                                                                        RATE = purprice,
                                                                        SRNO = 1,
                                                                        TRACKNO = hfNewJobID.Value,
                                                                        UOM = Convert.ToInt32(dtItemDesc.Rows[0]["UOM"]),
                                                                    });

                                                                    GRNMaster objGRNMaster = new GRNMaster();
                                                                    objGRNMaster.CHLNDT = DateTime.Now;
                                                                    objGRNMaster.CHLNNO = hfPRNo.Value;
                                                                    objGRNMaster.CMPID = objMainClass.intCmpId;
                                                                    objGRNMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                    objGRNMaster.DEPTCD = 0;
                                                                    objGRNMaster.DOCDATE = DateTime.Now;
                                                                    objGRNMaster.DOCNO = "";
                                                                    objGRNMaster.DOCTYPE = "103";
                                                                    objGRNMaster.EMPNAME = "";
                                                                    objGRNMaster.FINYEAR = DateTime.Now.Year;
                                                                    objGRNMaster.GRNData = objGRNDetials;
                                                                    objGRNMaster.POSTDATE = DateTime.Now;
                                                                    objGRNMaster.REFDOCNO = hfPONo.Value;
                                                                    objGRNMaster.REFDOCYEAR = Convert.ToInt32(txtVendorCode.Text);
                                                                    objGRNMaster.REFNO = hfPRNo.Value;
                                                                    objGRNMaster.REMARK = "AUTO PR/PO AGAINST SO";
                                                                    objGRNMaster.TRANCODE = txtTranCode.Text;


                                                                    string GRNURL = Convert.ToString(dtGRNAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtGRNAPI.Rows[0]["TOKEN"]);
                                                                    var clientGRN = new RestClient(GRNURL);
                                                                    clientGRN.Timeout = -1;
                                                                    var requestGRN = new RestRequest(Method.POST);
                                                                    requestGRN.AddHeader("" + Convert.ToString(dtGRNAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtGRNAPI.Rows[0]["KEYVALUE"]) + "");
                                                                    requestGRN.AddHeader("Content-Type", "application/json");
                                                                    var jsonInputGRN = JsonConvert.SerializeObject(objGRNMaster);
                                                                    requestGRN.AddParameter("application/json", jsonInputGRN, ParameterType.RequestBody);
                                                                    IRestResponse responseGRN = clientGRN.Execute(requestGRN);


                                                                    GRNRespsonse objGRNRespsonse = new GRNRespsonse();
                                                                    string jsonconnGRN = responseGRN.Content;
                                                                    objGRNRespsonse = JsonConvert.DeserializeObject<GRNRespsonse>(jsonconnGRN);

                                                                    if (responseGRN.StatusCode == HttpStatusCode.OK)
                                                                    {

                                                                        hfGRNNo.Value = objGRNRespsonse.GRNNO;

                                                                        #region PB Code...

                                                                        DataTable dtPBAPI = new DataTable();
                                                                        dtPBAPI = objMainClass.GetWAData("INSERTPB", 1, "GETWADATA");
                                                                        if (dtPBAPI.Rows.Count > 0)
                                                                        {
                                                                            List<PBCharges> objPBCharges = new List<PBCharges>();
                                                                            if (othchg > 0)
                                                                            {
                                                                                objPBCharges.Add(new PBCharges
                                                                                {
                                                                                    CHGAMT = othbasechg,
                                                                                    CHGTYPE = ddlCharges.SelectedValue,
                                                                                    CMPID = objMainClass.intCmpId,
                                                                                    CONDID = 12,
                                                                                    CONDTYPE = "GST",
                                                                                    GLCODE = "",
                                                                                    OPERATOR = "+",
                                                                                    PBNO = "",
                                                                                    PID = 0,
                                                                                    RATE = 18,
                                                                                    SRNO = 1,
                                                                                    TAXAMT = othchgtax,
                                                                                });
                                                                            }

                                                                            List<PBTaxation> objPBTaxation = new List<PBTaxation>();
                                                                            objPBTaxation.Add(new PBTaxation
                                                                            {
                                                                                BASEAMT = basprice,
                                                                                CMPID = objMainClass.intCmpId,
                                                                                CONDID = 12,
                                                                                CONDORDER = 1,
                                                                                CONDTYPE = "GST",
                                                                                GLCODE = "",
                                                                                OPERATOR = "+",
                                                                                PBNO = "",
                                                                                PBSRNO = 1,
                                                                                PID = 0,
                                                                                RATE = 18,
                                                                                SRNO = 1,
                                                                                TAXAMT = tax,
                                                                            });

                                                                            List<PBDetails> objPBDetails = new List<PBDetails>();
                                                                            objPBDetails.Add(new PBDetails
                                                                            {
                                                                                ASSETCD = "",
                                                                                BRATE = basprice,
                                                                                CAMOUNT = basprice,
                                                                                CHALLANNO = "",
                                                                                CMPID = objMainClass.intCmpId,
                                                                                CSTCENTCD = costcenter,
                                                                                DISCAMT = 0,
                                                                                GLCD = "10010000",
                                                                                ITEMDESC = hfItemDesc.Value,
                                                                                ITEMGRPID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMGRP"]),
                                                                                ITEMID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]),
                                                                                //ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",  //Removed AUTO PO AGAINST PO text from remarks
                                                                                ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text,
                                                                                LOCCD = hfLocCd.Value,
                                                                                MIRNO = hfGRNNo.Value,
                                                                                MIRSRNO = 1,
                                                                                PBNO = "",
                                                                                PBQTY = 1,
                                                                                PLANTCD = hfPlantCd.Value,
                                                                                PONO = hfPONo.Value,
                                                                                POSRNO = 1,
                                                                                PRFCNT = "1000",
                                                                                RATE = purprice,
                                                                                REFNO = "",
                                                                                SRNO = 1,
                                                                                TAXAMT = 0,
                                                                                TRNUM = hfNewJobID.Value,
                                                                                UOM = Convert.ToInt32(dtItemDesc.Rows[0]["UOM"]),
                                                                            });

                                                                            PBMaster objPBMaster = new PBMaster();
                                                                            objPBMaster.ADJAMT = 0;
                                                                            objPBMaster.BILLAMT = netpoamt;
                                                                            objPBMaster.BILLDT = Convert.ToDateTime(txtBillDate.Text);
                                                                            if (chkINVPO.Checked == true)
                                                                            {
                                                                                objPBMaster.BILLNO = hfPONo.Value;
                                                                            }
                                                                            else
                                                                            {
                                                                                objPBMaster.BILLNO = txtBillNo.Text;
                                                                            }
                                                                            objPBMaster.CMPID = objMainClass.intCmpId;
                                                                            objPBMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                            objPBMaster.DISCOUNT = 0;
                                                                            objPBMaster.NETMATVALUE = basprice;
                                                                            objPBMaster.NETPBAMT = netpoamt;
                                                                            objPBMaster.NETTAXAMT = nettaxamt;
                                                                            objPBMaster.PAIDAMT = 0;
                                                                            objPBMaster.PBCharge = objPBCharges;
                                                                            objPBMaster.PBDetail = objPBDetails;
                                                                            objPBMaster.PBDT = DateTime.Now;
                                                                            objPBMaster.PBNO = "";
                                                                            objPBMaster.PBTax = objPBTaxation;
                                                                            objPBMaster.PBTYPE = "MPB";
                                                                            objPBMaster.PENDINGAMT = netpoamt;
                                                                            objPBMaster.PMTTERMS = ddlPaymentTerms.SelectedItem.Text;
                                                                            objPBMaster.PMTTERMSDESC = txtPaymentTermsDesc.Text;
                                                                            objPBMaster.REMARK = "AUTO PR/PO AGAINST SO";
                                                                            objPBMaster.STATUS = 1;
                                                                            objPBMaster.VENDCODE = txtVendorCode.Text;


                                                                            string PBURL = Convert.ToString(dtPBAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtPBAPI.Rows[0]["TOKEN"]);
                                                                            var clientPB = new RestClient(PBURL);
                                                                            clientPB.Timeout = -1;
                                                                            var requestPB = new RestRequest(Method.POST);
                                                                            requestPB.AddHeader("" + Convert.ToString(dtPBAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtPBAPI.Rows[0]["KEYVALUE"]) + "");
                                                                            requestPB.AddHeader("Content-Type", "application/json");
                                                                            var jsonInputPB = JsonConvert.SerializeObject(objPBMaster);
                                                                            requestPB.AddParameter("application/json", jsonInputPB, ParameterType.RequestBody);
                                                                            IRestResponse responsePB = clientPB.Execute(requestPB);

                                                                            PBRespsonse objPBRespsonse = new PBRespsonse();
                                                                            string jsonconnPB = responsePB.Content;
                                                                            objPBRespsonse = JsonConvert.DeserializeObject<PBRespsonse>(jsonconnPB);

                                                                            if (responsePB.StatusCode == HttpStatusCode.OK)
                                                                            {
                                                                                hfPBNo.Value = objPBRespsonse.PBNO;

                                                                                string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                                if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                                {
                                                                                    message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                                }
                                                                                else
                                                                                {
                                                                                    message = message + " New Job card not Created.";
                                                                                }

                                                                                if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                                {
                                                                                    message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                                }
                                                                                else
                                                                                {
                                                                                    message = message + " New Estimate not Created.";
                                                                                }
                                                                                message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                                message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                                message = message + " New GRN Created. GRN No. is :" + hfGRNNo.Value + ".!";
                                                                                message = message + " New PB Created. PB No. is :" + hfPBNo.Value + ".!";

                                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                            }
                                                                            else
                                                                            {
                                                                                string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                                if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                                {
                                                                                    message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                                }
                                                                                else
                                                                                {
                                                                                    message = message + " New Job card not Created.";
                                                                                }

                                                                                if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                                {
                                                                                    message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                                }
                                                                                else
                                                                                {
                                                                                    message = message + " New Estimate not Created.";
                                                                                }
                                                                                message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                                message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                                message = message + " New GRN Created. GRN No. is :" + objGRNRespsonse.GRNNO + ".";
                                                                                message = message + " PB not Created." + objPBRespsonse.MESSAGE + ".!";

                                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                            }

                                                                        }
                                                                        else
                                                                        {
                                                                            string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                            if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                            {
                                                                                message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                            }
                                                                            else
                                                                            {
                                                                                message = message + " New Job card not Created.";
                                                                            }

                                                                            if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                            {
                                                                                message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                            }
                                                                            else
                                                                            {
                                                                                message = message + " New Estimate not Created.";
                                                                            }
                                                                            message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                            message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                            message = message + " New GRN Created. GRN No. is :" + objGRNRespsonse.GRNNO + ".";
                                                                            message = message + " But PB API not found. Please contact to API provider.!";

                                                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                        }

                                                                        #endregion


                                                                    }
                                                                    else
                                                                    {
                                                                        string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                        if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                        {
                                                                            message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                        }
                                                                        else
                                                                        {
                                                                            message = message + " New Job card not Created.";
                                                                        }

                                                                        if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                        {
                                                                            message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                        }
                                                                        else
                                                                        {
                                                                            message = message + " New Estimate not Created.";
                                                                        }
                                                                        message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                        message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                        message = message + " GRN not Created." + objGRNRespsonse.MESSAGE + ".!";

                                                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                    }



                                                                }
                                                                else
                                                                {
                                                                    string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                    if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                    {
                                                                        message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                    }
                                                                    else
                                                                    {
                                                                        message = message + " New Job card not Created.";
                                                                    }

                                                                    if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                    {
                                                                        message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                    }
                                                                    else
                                                                    {
                                                                        message = message + " New Estimate not Created.";
                                                                    }
                                                                    message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                    message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                    message = message + " But GRN API not found. Please contact API provider.!";

                                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                }


                                                                #endregion

                                                            }
                                                            else
                                                            {
                                                                string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                {
                                                                    message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                }
                                                                else
                                                                {
                                                                    message = message + " New Job card not Created.";
                                                                }

                                                                if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                {
                                                                    message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                }
                                                                else
                                                                {
                                                                    message = message + " New Estimate not Created.";
                                                                }
                                                                message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ". PO not created. " + objPORespsonse.MESSAGE + ".!";

                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                            }


                                                        }
                                                        else
                                                        {
                                                            string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                            if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                            {
                                                                message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                            }
                                                            else
                                                            {
                                                                message = message + " New Job card not Created.";
                                                            }

                                                            if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                            {
                                                                message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                            }
                                                            else
                                                            {
                                                                message = message + " New Estimate not Created.";
                                                            }
                                                            message = message + " Job Specification created successfully. New PR Created PR NO. is : " + objPRRespsonse.PRNO + ". PO not created because PO API not found. Please contact API provider.!";

                                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                        }



                                                    }
                                                }
                                                else
                                                {
                                                    string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                    if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                    {
                                                        message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                    }
                                                    else
                                                    {
                                                        message = message + " New Job card not Created.";
                                                    }

                                                    if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                    {
                                                        message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                    }
                                                    else
                                                    {
                                                        message = message + " New Estimate not Created.";
                                                    }
                                                    message = message + " Job Specification created successfully. " + objPRRespsonse.MESSAGE + ".!";

                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                }

                                                #endregion


                                            }
                                            else
                                            {
                                                decimal purcomm = 0;
                                                decimal purprice = 0;
                                                decimal basprice = 0;
                                                decimal tax = 0;

                                                decimal othchg = 0;
                                                decimal othbasechg = 0;
                                                decimal othchgtax = 0;
                                                decimal nettaxamt = 0;
                                                decimal netpoamt = 0;

                                                purcomm = Math.Round(RATE);
                                                purprice = Math.Round((Convert.ToDecimal(hfPrice.Value) - Convert.ToDecimal(purcomm)), 2);
                                                basprice = Math.Round((Convert.ToDecimal(purprice / Convert.ToDecimal("1.18"))), 2);
                                                tax = Math.Round((purprice - basprice), 2);

                                                //nettaxamt = tax;
                                                netpoamt = Math.Round((purprice), 2);

                                                if (txtChgAmt.Text != null && txtChgAmt.Text != string.Empty && txtChgAmt.Text != null)
                                                {
                                                    if (Convert.ToDecimal(txtChgAmt.Text) > 0)
                                                    {
                                                        othchg = Math.Round((Convert.ToDecimal(txtChgAmt.Text)), 2);
                                                        othbasechg = Math.Round((Convert.ToDecimal(othchg / Convert.ToDecimal("1.18"))), 2);
                                                        othchgtax = Math.Round((othchg - othbasechg), 2);
                                                        //nettaxamt = nettaxamt + othchgtax;
                                                        netpoamt = Math.Round((netpoamt + othchg), 2);
                                                    }
                                                }

                                                List<PRDetails> objPRDetails = new List<PRDetails>();
                                                objPRDetails.Add(new PRDetails
                                                {
                                                    ASSETCD = "",
                                                    CAMOUNT = purprice,
                                                    CMPID = objMainClass.intCmpId,
                                                    CSTCENTCD = costcenter,
                                                    DELIDT = DateTime.Now,
                                                    GLCD = "10010000",
                                                    IMEINO = txtIMEI.Text,
                                                    ITEMDESC = hfItemDesc.Value,
                                                    ITEMGRPID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMGRP"]),
                                                    ITEMID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]),
                                                    ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",
                                                    LOCCD = hfLocCd.Value,
                                                    PARTREQNO = 0,
                                                    PLANTCD = hfPlantCd.Value,
                                                    PRBY = "",
                                                    PRFCNT = "1000",
                                                    PRNO = "",
                                                    PRQTY = 1,
                                                    RATE = purprice,
                                                    SRNO = 1,
                                                    STATUS = 2,
                                                    TRNUM = hfNewJobID.Value,
                                                    UOM = Convert.ToInt32(dtItemDesc.Rows[0]["UOM"]),
                                                });

                                                #region Inser PR Method...




                                                PRMaster objPRMaster = new PRMaster();
                                                objPRMaster.CMPID = objMainClass.intCmpId;
                                                objPRMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                objPRMaster.CREATEDATE = DateTime.Now;
                                                objPRMaster.DEPTID = 1;
                                                objPRMaster.ISPRSTO = 0;
                                                objPRMaster.LISTINGID = 0;
                                                objPRMaster.PRDT = DateTime.Now;
                                                objPRMaster.PRNO = "";
                                                objPRMaster.PRTYPE = "MPR";
                                                objPRMaster.REMARK = "AUTO PR/PO AGAINST SO";
                                                objPRMaster.STATUS = 57;
                                                objPRMaster.VENDCODE = "";
                                                objPRMaster.PRDATA = objPRDetails;


                                                string PRURL = Convert.ToString(dtPRAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtPRAPI.Rows[0]["TOKEN"]);
                                                var clientPR = new RestClient(PRURL);
                                                clientPR.Timeout = -1;
                                                var requestPR = new RestRequest(Method.POST);
                                                requestPR.AddHeader("" + Convert.ToString(dtPRAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtPRAPI.Rows[0]["KEYVALUE"]) + "");
                                                requestPR.AddHeader("Content-Type", "application/json");
                                                var jsonInputPR = JsonConvert.SerializeObject(objPRMaster);
                                                requestPR.AddParameter("application/json", jsonInputPR, ParameterType.RequestBody);
                                                IRestResponse responsePR = clientPR.Execute(requestPR);


                                                PRRespsonse objPRRespsonse = new PRRespsonse();
                                                string jsonconnPR = responsePR.Content;
                                                objPRRespsonse = JsonConvert.DeserializeObject<PRRespsonse>(jsonconnPR);

                                                if (responsePR.StatusCode == HttpStatusCode.OK)
                                                {
                                                    hfPRNo.Value = Convert.ToString(objMainClass.strConvertZeroPadding(objPRRespsonse.PRNO));

                                                    bool blnPOExist = false;
                                                    DataTable dtPO = new DataTable();
                                                    DataTable dtSO = new DataTable();
                                                    dtPO = objMainClass.CheckPOSOImeiNo(objMainClass.intCmpId, txtIMEI.Text, "CHECKPOWITHRETURN");
                                                    if (dtPO.Rows.Count > 0)
                                                    {
                                                        if (Convert.ToString(dtPO.Rows[0]["LASTPO"]) != "" && Convert.ToString(dtPO.Rows[0]["LASTPO"]) != string.Empty &&
                                                            Convert.ToString(dtPO.Rows[0]["LASTPO"]) != null &&
                                                            Convert.ToInt32(Convert.ToString(dtPO.Rows[0]["BALQTY"]) == "" ? 0 : Convert.ToInt32(dtPO.Rows[0]["BALQTY"])) > 0)
                                                        {
                                                            blnPOExist = true;


                                                            dtSO = objMainClass.CheckPOSOImeiNo(objMainClass.intCmpId, txtIMEI.Text, "CHECKSOWITHRETURN");

                                                            if (dtSO.Rows.Count > 0)
                                                            {
                                                                if (Convert.ToString(dtSO.Rows[0]["LASTSO"]) != "" && Convert.ToString(dtSO.Rows[0]["LASTSO"]) != string.Empty &&
                                                            Convert.ToString(dtSO.Rows[0]["LASTSO"]) != null &&
                                                            Convert.ToInt32(Convert.ToString(dtSO.Rows[0]["BALQTY"]) == "" ? 0 : Convert.ToInt32(dtSO.Rows[0]["BALQTY"])) > 0)
                                                                {
                                                                    blnPOExist = false;
                                                                }

                                                            }

                                                        }
                                                    }

                                                    if (blnPOExist == true)
                                                    {
                                                        string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                        if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                        {
                                                            message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                        }
                                                        else
                                                        {
                                                            message = message + " New Job card not Created.";
                                                        }

                                                        if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                        {
                                                            message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                        }
                                                        else
                                                        {
                                                            message = message + " New Estimate not Created.";
                                                        }
                                                        message = message + " Job Specification created successfully. New PR Created PR NO. is : " + objPRRespsonse.PRNO + ". PO not created because PO is already made for same IMEI No.! PO No. : " + Convert.ToString(dtPO.Rows[0]["LASTPO"]) + ".!";

                                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                    }
                                                    else
                                                    {

                                                        DataTable dtPOAPI = new DataTable();
                                                        dtPOAPI = objMainClass.GetWAData("INSERTPO", 1, "GETWADATA");
                                                        if (dtPOAPI.Rows.Count > 0)
                                                        {
                                                            List<POCharges> objPOCharges = new List<POCharges>();
                                                            if (othchg > 0)
                                                            {
                                                                objPOCharges.Add(new POCharges
                                                                {
                                                                    CHGAMT = othchg,
                                                                    CHGTYPE = ddlCharges.SelectedValue,
                                                                    CMPID = objMainClass.intCmpId,
                                                                    CONDID = 0,
                                                                    CONDTYPE = "",
                                                                    GLCODE = "",
                                                                    OPERATOR = "+",
                                                                    PID = 0,
                                                                    PONO = "",
                                                                    RATE = 0,
                                                                    SRNO = 1,
                                                                    TAXAMT = 0,
                                                                });
                                                            }

                                                            List<POTaxation> objPOTaxation = new List<POTaxation>();
                                                            //objPOTaxation.Add(new POTaxation
                                                            //{
                                                            //    BASEAMT = basprice,
                                                            //    CMPID = objMainClass.intCmpId,
                                                            //    CONDID = 12,
                                                            //    CONDORDER = 1,
                                                            //    CONDTYPE = "GST",
                                                            //    GLCODE = "",
                                                            //    OPERATOR = "+",
                                                            //    PID = 0,
                                                            //    PONO = "",
                                                            //    POSRNO = 1,
                                                            //    RATE = 18,
                                                            //    SRNO = 1,
                                                            //    TAXAMT = tax,
                                                            //});

                                                            List<PODetails> objPODetails = new List<PODetails>();
                                                            objPODetails.Add(new PODetails
                                                            {
                                                                APRVBY = 17,
                                                                APRVDATE = DateTime.Now,
                                                                APRVSTATUS = 260,
                                                                ASSETCD = "",
                                                                BRATE = purprice,
                                                                CAMOUNT = purprice,
                                                                CMPID = objMainClass.intCmpId,
                                                                CSTCENTCD = costcenter,
                                                                DELIDT = DateTime.Now,
                                                                DEVREASON = "OK",
                                                                DISCAMT = 0,
                                                                FROMLOCCD = "",
                                                                FROMPLANTCD = "",
                                                                GLCD = "10010000",
                                                                IMEINO = txtIMEI.Text,
                                                                ITEMDESC = hfItemDesc.Value,
                                                                ITEMGRPID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMGRP"]),
                                                                ITEMID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]),
                                                                //ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",  //Removed AUTO PO AGAINST PO text from remarks
                                                                ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text,
                                                                LOCCD = hfLocCd.Value,
                                                                LOCKAMT = 0,
                                                                PLANTCD = hfPlantCd.Value,
                                                                PONO = "",
                                                                POQTY = 1,
                                                                PRFCNT = "1000",
                                                                PRNO = hfPRNo.Value,
                                                                PRSRNO = 1,
                                                                RATE = purprice,
                                                                REFNO = "",
                                                                REJREASON = "",
                                                                SRNO = 1,
                                                                TAXAMT = 0,
                                                                TRNUM = hfNewJobID.Value,
                                                                UOM = Convert.ToInt32(dtItemDesc.Rows[0]["UOM"]),
                                                            });

                                                            POMaster objPOMaster = new POMaster();
                                                            objPOMaster.ADJAMT = 0;
                                                            objPOMaster.ADVAMT = 0;
                                                            objPOMaster.AGENTNAME = "";
                                                            objPOMaster.APRVBY = 17;
                                                            objPOMaster.APRVDATE = DateTime.Now;
                                                            objPOMaster.CMPID = objMainClass.intCmpId;
                                                            objPOMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                            objPOMaster.CREATEDATE = DateTime.Now;
                                                            objPOMaster.DEPTID = 1;
                                                            objPOMaster.DISCOUNT = 0;
                                                            objPOMaster.NETMATVALUE = purprice;
                                                            objPOMaster.NETPOAMT = netpoamt;
                                                            objPOMaster.NETTAXAMT = nettaxamt;
                                                            objPOMaster.OLDPOAMT = 0;
                                                            objPOMaster.PENDINGAMT = netpoamt;
                                                            objPOMaster.PMTTERMS = ddlPaymentTerms.SelectedItem.Text;
                                                            objPOMaster.PMTTERMSDESC = txtPaymentTermsDesc.Text;
                                                            objPOMaster.POCharge = objPOCharges;
                                                            objPOMaster.PODetail = objPODetails;
                                                            objPOMaster.PODT = DateTime.Now;
                                                            objPOMaster.PONO = "";
                                                            objPOMaster.POTax = objPOTaxation;
                                                            objPOMaster.POTYPE = "MPO";
                                                            objPOMaster.PURTYPE = Convert.ToInt32(ddlPurType.SelectedValue);
                                                            objPOMaster.REMARK = "AUTO PR/PO AGAINST SO";
                                                            objPOMaster.STATUS = 57;
                                                            objPOMaster.TRANCODE = txtTranCode.Text;
                                                            objPOMaster.VENDCODE = txtVendorCode.Text;


                                                            string POURL = Convert.ToString(dtPOAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtPOAPI.Rows[0]["TOKEN"]);
                                                            var clientPO = new RestClient(POURL);
                                                            clientPO.Timeout = -1;
                                                            var requestPO = new RestRequest(Method.POST);
                                                            requestPO.AddHeader("" + Convert.ToString(dtPOAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtPOAPI.Rows[0]["KEYVALUE"]) + "");
                                                            requestPO.AddHeader("Content-Type", "application/json");
                                                            var jsonInputPO = JsonConvert.SerializeObject(objPOMaster);
                                                            requestPO.AddParameter("application/json", jsonInputPO, ParameterType.RequestBody);
                                                            IRestResponse responsePO = clientPO.Execute(requestPO);


                                                            PORespsonse objPORespsonse = new PORespsonse();
                                                            string jsonconnPO = responsePO.Content;
                                                            objPORespsonse = JsonConvert.DeserializeObject<PORespsonse>(jsonconnPO);

                                                            if (responsePO.StatusCode == HttpStatusCode.OK)
                                                            {

                                                                hfPONo.Value = objPORespsonse.PONO;

                                                                #region GRN CODE...

                                                                DataTable dtGRNAPI = new DataTable();
                                                                dtGRNAPI = objMainClass.GetWAData("CREATEGRN", 1, "GETWADATA");

                                                                if (dtGRNAPI.Rows.Count > 0)
                                                                {
                                                                    List<GRNDetials> objGRNDetials = new List<GRNDetials>();
                                                                    objGRNDetials.Add(new GRNDetials
                                                                    {
                                                                        ASSETCD = "",
                                                                        CAMOUNT = purprice,
                                                                        CHLNQTY = 1,
                                                                        CMPID = objMainClass.intCmpId,
                                                                        CSTCENTCD = costcenter,
                                                                        DOCNO = "",
                                                                        DOCTYPE = "103",
                                                                        FINYEAR = DateTime.Now.Year,
                                                                        GLCD = "10010000",
                                                                        ITEMDESC = hfItemDesc.Value,
                                                                        ITEMGRPID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMGRP"]),
                                                                        ITEMID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]),
                                                                        //ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",  //Removed AUTO PO AGAINST PO text from remarks
                                                                        ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text,
                                                                        LOCCD = hfLocCd.Value,
                                                                        PLANTCD = hfPlantCd.Value,
                                                                        PONO = hfPONo.Value,
                                                                        POSRNO = 1,
                                                                        PRFCNT = "1000",
                                                                        QTY = 1,
                                                                        RATE = purprice,
                                                                        SRNO = 1,
                                                                        TRACKNO = hfNewJobID.Value,
                                                                        UOM = Convert.ToInt32(dtItemDesc.Rows[0]["UOM"]),
                                                                    });

                                                                    GRNMaster objGRNMaster = new GRNMaster();
                                                                    objGRNMaster.CHLNDT = DateTime.Now;
                                                                    objGRNMaster.CHLNNO = hfPRNo.Value;
                                                                    objGRNMaster.CMPID = objMainClass.intCmpId;
                                                                    objGRNMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                    objGRNMaster.DEPTCD = 0;
                                                                    objGRNMaster.DOCDATE = DateTime.Now;
                                                                    objGRNMaster.DOCNO = "";
                                                                    objGRNMaster.DOCTYPE = "103";
                                                                    objGRNMaster.EMPNAME = "";
                                                                    objGRNMaster.FINYEAR = DateTime.Now.Year;
                                                                    objGRNMaster.GRNData = objGRNDetials;
                                                                    objGRNMaster.POSTDATE = DateTime.Now;
                                                                    objGRNMaster.REFDOCNO = hfPONo.Value;
                                                                    objGRNMaster.REFDOCYEAR = Convert.ToInt32(txtVendorCode.Text);
                                                                    objGRNMaster.REFNO = hfPRNo.Value;
                                                                    objGRNMaster.REMARK = "AUTO PR/PO AGAINST SO";
                                                                    objGRNMaster.TRANCODE = txtTranCode.Text;


                                                                    string GRNURL = Convert.ToString(dtGRNAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtGRNAPI.Rows[0]["TOKEN"]);
                                                                    var clientGRN = new RestClient(GRNURL);
                                                                    clientGRN.Timeout = -1;
                                                                    var requestGRN = new RestRequest(Method.POST);
                                                                    requestGRN.AddHeader("" + Convert.ToString(dtGRNAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtGRNAPI.Rows[0]["KEYVALUE"]) + "");
                                                                    requestGRN.AddHeader("Content-Type", "application/json");
                                                                    var jsonInputGRN = JsonConvert.SerializeObject(objGRNMaster);
                                                                    requestGRN.AddParameter("application/json", jsonInputGRN, ParameterType.RequestBody);
                                                                    IRestResponse responseGRN = clientGRN.Execute(requestGRN);


                                                                    GRNRespsonse objGRNRespsonse = new GRNRespsonse();
                                                                    string jsonconnGRN = responseGRN.Content;
                                                                    objGRNRespsonse = JsonConvert.DeserializeObject<GRNRespsonse>(jsonconnGRN);

                                                                    if (responseGRN.StatusCode == HttpStatusCode.OK)
                                                                    {

                                                                        hfGRNNo.Value = objGRNRespsonse.GRNNO;

                                                                        #region PB Code...

                                                                        DataTable dtPBAPI = new DataTable();
                                                                        dtPBAPI = objMainClass.GetWAData("INSERTPB", 1, "GETWADATA");
                                                                        if (dtPBAPI.Rows.Count > 0)
                                                                        {
                                                                            List<PBCharges> objPBCharges = new List<PBCharges>();
                                                                            if (othchg > 0)
                                                                            {
                                                                                objPBCharges.Add(new PBCharges
                                                                                {
                                                                                    CHGAMT = othchg,
                                                                                    CHGTYPE = ddlCharges.SelectedValue,
                                                                                    CMPID = objMainClass.intCmpId,
                                                                                    CONDID = 12,
                                                                                    CONDTYPE = "GST",
                                                                                    GLCODE = "",
                                                                                    OPERATOR = "+",
                                                                                    PBNO = "",
                                                                                    PID = 0,
                                                                                    RATE = 18,
                                                                                    SRNO = 1,
                                                                                    TAXAMT = 0,
                                                                                });
                                                                            }

                                                                            List<PBTaxation> objPBTaxation = new List<PBTaxation>();
                                                                            //objPBTaxation.Add(new PBTaxation
                                                                            //{
                                                                            //    BASEAMT = basprice,
                                                                            //    CMPID = objMainClass.intCmpId,
                                                                            //    CONDID = 12,
                                                                            //    CONDORDER = 1,
                                                                            //    CONDTYPE = "GST",
                                                                            //    GLCODE = "",
                                                                            //    OPERATOR = "+",
                                                                            //    PBNO = "",
                                                                            //    PBSRNO = 1,
                                                                            //    PID = 0,
                                                                            //    RATE = 18,
                                                                            //    SRNO = 1,
                                                                            //    TAXAMT = tax,
                                                                            //});

                                                                            List<PBDetails> objPBDetails = new List<PBDetails>();
                                                                            objPBDetails.Add(new PBDetails
                                                                            {
                                                                                ASSETCD = "",
                                                                                BRATE = purprice,
                                                                                CAMOUNT = purprice,
                                                                                CHALLANNO = "",
                                                                                CMPID = objMainClass.intCmpId,
                                                                                CSTCENTCD = costcenter,
                                                                                DISCAMT = 0,
                                                                                GLCD = "10010000",
                                                                                ITEMDESC = hfItemDesc.Value,
                                                                                ITEMGRPID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMGRP"]),
                                                                                ITEMID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]),
                                                                                //ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",  //Removed AUTO PO AGAINST PO text from remarks
                                                                                ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text,
                                                                                LOCCD = hfLocCd.Value,
                                                                                MIRNO = hfGRNNo.Value,
                                                                                MIRSRNO = 1,
                                                                                PBNO = "",
                                                                                PBQTY = 1,
                                                                                PLANTCD = hfPlantCd.Value,
                                                                                PONO = hfPONo.Value,
                                                                                POSRNO = 1,
                                                                                PRFCNT = "1000",
                                                                                RATE = purprice,
                                                                                REFNO = "",
                                                                                SRNO = 1,
                                                                                TAXAMT = 0,
                                                                                TRNUM = hfNewJobID.Value,
                                                                                UOM = Convert.ToInt32(dtItemDesc.Rows[0]["UOM"]),
                                                                            });

                                                                            PBMaster objPBMaster = new PBMaster();
                                                                            objPBMaster.ADJAMT = 0;
                                                                            objPBMaster.BILLAMT = netpoamt;
                                                                            objPBMaster.BILLDT = Convert.ToDateTime(txtBillDate.Text);
                                                                            if (chkINVPO.Checked == true)
                                                                            {
                                                                                objPBMaster.BILLNO = hfPONo.Value;
                                                                            }
                                                                            else
                                                                            {
                                                                                objPBMaster.BILLNO = txtBillNo.Text;
                                                                            }
                                                                            objPBMaster.CMPID = objMainClass.intCmpId;
                                                                            objPBMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                            objPBMaster.DISCOUNT = 0;
                                                                            objPBMaster.NETMATVALUE = purprice;
                                                                            objPBMaster.NETPBAMT = netpoamt;
                                                                            objPBMaster.NETTAXAMT = nettaxamt;
                                                                            objPBMaster.PAIDAMT = 0;
                                                                            objPBMaster.PBCharge = objPBCharges;
                                                                            objPBMaster.PBDetail = objPBDetails;
                                                                            objPBMaster.PBDT = DateTime.Now;
                                                                            objPBMaster.PBNO = "";
                                                                            objPBMaster.PBTax = objPBTaxation;
                                                                            objPBMaster.PBTYPE = "MPB";
                                                                            objPBMaster.PENDINGAMT = netpoamt;
                                                                            objPBMaster.PMTTERMS = ddlPaymentTerms.SelectedItem.Text;
                                                                            objPBMaster.PMTTERMSDESC = txtPaymentTermsDesc.Text;
                                                                            objPBMaster.REMARK = "AUTO PR/PO AGAINST SO";
                                                                            objPBMaster.STATUS = 1;
                                                                            objPBMaster.VENDCODE = txtVendorCode.Text;


                                                                            string PBURL = Convert.ToString(dtPBAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtPBAPI.Rows[0]["TOKEN"]);
                                                                            var clientPB = new RestClient(PBURL);
                                                                            clientPB.Timeout = -1;
                                                                            var requestPB = new RestRequest(Method.POST);
                                                                            requestPB.AddHeader("" + Convert.ToString(dtPBAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtPBAPI.Rows[0]["KEYVALUE"]) + "");
                                                                            requestPB.AddHeader("Content-Type", "application/json");
                                                                            var jsonInputPB = JsonConvert.SerializeObject(objPBMaster);
                                                                            requestPB.AddParameter("application/json", jsonInputPB, ParameterType.RequestBody);
                                                                            IRestResponse responsePB = clientPB.Execute(requestPB);

                                                                            PBRespsonse objPBRespsonse = new PBRespsonse();
                                                                            string jsonconnPB = responsePB.Content;
                                                                            objPBRespsonse = JsonConvert.DeserializeObject<PBRespsonse>(jsonconnPB);

                                                                            if (responsePB.StatusCode == HttpStatusCode.OK)
                                                                            {
                                                                                hfPBNo.Value = objPBRespsonse.PBNO;

                                                                                string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                                if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                                {
                                                                                    message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                                }
                                                                                else
                                                                                {
                                                                                    message = message + " New Job card not Created.";
                                                                                }

                                                                                if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                                {
                                                                                    message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                                }
                                                                                else
                                                                                {
                                                                                    message = message + " New Estimate not Created.";
                                                                                }
                                                                                message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                                message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                                message = message + " New GRN Created. GRN No. is :" + hfGRNNo.Value + ".!";
                                                                                message = message + " New PB Created. PB No. is :" + hfPBNo.Value + ".!";

                                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                            }
                                                                            else
                                                                            {
                                                                                string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                                if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                                {
                                                                                    message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                                }
                                                                                else
                                                                                {
                                                                                    message = message + " New Job card not Created.";
                                                                                }

                                                                                if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                                {
                                                                                    message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                                }
                                                                                else
                                                                                {
                                                                                    message = message + " New Estimate not Created.";
                                                                                }
                                                                                message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                                message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                                message = message + " New GRN Created. GRN No. is :" + objGRNRespsonse.GRNNO + ".";
                                                                                message = message + " PB not Created." + objPBRespsonse.MESSAGE + ".!";

                                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                            }

                                                                        }
                                                                        else
                                                                        {
                                                                            string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                            if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                            {
                                                                                message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                            }
                                                                            else
                                                                            {
                                                                                message = message + " New Job card not Created.";
                                                                            }

                                                                            if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                            {
                                                                                message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                            }
                                                                            else
                                                                            {
                                                                                message = message + " New Estimate not Created.";
                                                                            }
                                                                            message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                            message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                            message = message + " New GRN Created. GRN No. is :" + objGRNRespsonse.GRNNO + ".";
                                                                            message = message + " But PB API not found. Please contact to API provider.!";

                                                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                        }

                                                                        #endregion

                                                                    }
                                                                    else
                                                                    {
                                                                        string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                        if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                        {
                                                                            message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                        }
                                                                        else
                                                                        {
                                                                            message = message + " New Job card not Created.";
                                                                        }

                                                                        if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                        {
                                                                            message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                        }
                                                                        else
                                                                        {
                                                                            message = message + " New Estimate not Created.";
                                                                        }
                                                                        message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                        message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                        message = message + " GRN not Created." + objGRNRespsonse.MESSAGE + ".!";

                                                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                    }



                                                                }
                                                                else
                                                                {
                                                                    string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                    if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                    {
                                                                        message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                    }
                                                                    else
                                                                    {
                                                                        message = message + " New Job card not Created.";
                                                                    }

                                                                    if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                    {
                                                                        message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                    }
                                                                    else
                                                                    {
                                                                        message = message + " New Estimate not Created.";
                                                                    }
                                                                    message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ".";
                                                                    message = message + " New PO created. PO No. is : " + hfPONo.Value + ".";
                                                                    message = message + " But GRN API not found. Please contact API provider.!";

                                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                                }


                                                                #endregion

                                                            }
                                                            else
                                                            {
                                                                string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                                if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                                {
                                                                    message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                                }
                                                                else
                                                                {
                                                                    message = message + " New Job card not Created.";
                                                                }

                                                                if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                                {
                                                                    message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                                }
                                                                else
                                                                {
                                                                    message = message + " New Estimate not Created.";
                                                                }
                                                                message = message + " Job Specification created successfully. New PR Created. PR NO. is : " + hfPRNo.Value + ". PO not created. " + objPORespsonse.MESSAGE + ".!";

                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                            }


                                                        }
                                                        else
                                                        {
                                                            string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                            if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                            {
                                                                message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                            }
                                                            else
                                                            {
                                                                message = message + " New Job card not Created.";
                                                            }

                                                            if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                            {
                                                                message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                            }
                                                            else
                                                            {
                                                                message = message + " New Estimate not Created.";
                                                            }
                                                            message = message + " Job Specification created successfully. New PR Created PR NO. is : " + objPRRespsonse.PRNO + ". PO not created because PO API not found. Please contact API provider.!";

                                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                        }



                                                    }
                                                }
                                                else
                                                {
                                                    string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                                    if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                                    {
                                                        message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                                    }
                                                    else
                                                    {
                                                        message = message + " New Job card not Created.";
                                                    }

                                                    if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                                    {
                                                        message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                                    }
                                                    else
                                                    {
                                                        message = message + " New Estimate not Created.";
                                                    }
                                                    message = message + " Job Specification created successfully. " + objPRRespsonse.MESSAGE + ".!";

                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                }

                                                #endregion


                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                    if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                    {
                                        message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                    }
                                    else
                                    {
                                        message = message + " New Job card not Created.";
                                    }

                                    if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                    {
                                        message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                    }
                                    else
                                    {
                                        message = message + " New Estimate not Created.";
                                    }
                                    message = message + " Job Specification created successfully. But Commission Percentage or Amount not found. Please contact Administrator.!";

                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                }

                            }
                            else
                            {
                                string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                                if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                                {
                                    message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                                }
                                else
                                {
                                    message = message + " New Job card not Created.";
                                }

                                if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                                {
                                    message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                                }
                                else
                                {
                                    message = message + " New Estimate not Created.";
                                }
                                message = message + " Job Specification created successfully. But Commission Percentage or Amount not found. Please contact Administrator.!";

                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                            }

                        }
                        else
                        {
                            string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                            if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                            {
                                message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                            }
                            else
                            {
                                message = message + " New Job card not Created.";
                            }

                            if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                            {
                                message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                            }
                            else
                            {
                                message = message + " New Estimate not Created.";
                            }
                            message = message + " Job Specification created successfully. But Commission API not found. Please Contact API Provider.!";

                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                        }

                    }
                    else
                    {
                        string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
                        if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
                        {
                            message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
                        }
                        else
                        {
                            message = message + " New Job card not Created.";
                        }

                        if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
                        {
                            message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
                        }
                        else
                        {
                            message = message + " New Estimate not Created.";
                        }
                        message = message + " Job Specification created successfully. But PR API not found. Please Contact API Provider.!";

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
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

        protected void txtVendorCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (txtVendorCode.Text.Length >= 5)
                    {
                        lblVendorError.Text = string.Empty;
                        lblVendorError.Visible = false;
                        DataTable dt = objMainClass.GetVendorDetails(objMainClass.intCmpId, txtVendorCode.Text, "");
                        if (dt.Rows.Count > 0)
                        {
                            lblVendorError.Text = string.Empty;
                            lblVendorError.Visible = false;
                            txtVendorName.Text = Convert.ToString(dt.Rows[0]["NAME1"]);
                            txtVendorCode.Text = Convert.ToString(dt.Rows[0]["VENDCODE"]);
                        }
                        else
                        {
                            lblVendorError.Text = "Invalid Vendor Code. Please Enter Correct Vendor Code.";
                            lblVendorError.Visible = true;
                            txtVendorCode.Focus();
                            ScriptManager.GetCurrent(this.Page).SetFocus(this.txtVendorCode);
                        }
                    }
                    else
                    {
                        lblVendorError.Text = "Minimum 5 digit req.";
                        lblVendorError.Visible = true;
                        txtVendorCode.Focus();
                        ScriptManager.GetCurrent(this.Page).SetFocus(this.txtVendorCode);

                    }
                    txtTranCode.Focus();
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

        protected void txtTranCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    if (txtTranCode.Text.Length >= 5)
                    {
                        lblTransporterError.Text = string.Empty;
                        lblTransporterError.Visible = false;
                        DataTable dt = objMainClass.GetVendorDetails(objMainClass.intCmpId, txtTranCode.Text, "");
                        if (dt.Rows.Count > 0)
                        {
                            lblTransporterError.Text = string.Empty;
                            lblTransporterError.Visible = false;
                            txtTranName.Text = Convert.ToString(dt.Rows[0]["NAME1"]);
                            txtTranCode.Text = Convert.ToString(dt.Rows[0]["VENDCODE"]);
                        }
                        else
                        {
                            lblTransporterError.Text = "Invalid Transporter Code. Please Enter Correct Transporter Code.";
                            lblTransporterError.Visible = true;
                            txtTranCode.Focus();
                            ScriptManager.GetCurrent(this.Page).SetFocus(this.txtTranCode);
                        }
                    }
                    else
                    {
                        lblTransporterError.Text = "Minimum 5 digit req.";
                        lblTransporterError.Visible = true;
                        txtTranCode.Focus();
                    }
                    ddlPaymentTerms.Focus();
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

        protected void ddlPaymentTerms_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    try
                    {
                        if (ddlPaymentTerms.SelectedValue == "OTHR")
                        {
                            txtPaymentTermsDesc.Text = string.Empty;
                            txtPaymentTermsDesc.Enabled = true;
                            txtPaymentTermsDesc.Focus();
                        }
                        else
                        {
                            txtPaymentTermsDesc.Text = Convert.ToString(ddlPaymentTerms.SelectedItem.Text.Split('-')[1].Trim()); ;
                            txtPaymentTermsDesc.Enabled = false;
                        }
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
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

        public string ReturnMessage()
        {
            string message = "New Job ID created. Job ID is : " + hfNewJobID.Value + ".";
            if (hfJCNO.Value != null && hfJCNO.Value != string.Empty && hfJCNO.Value != "")
            {
                message = message + " New Job card Created. JC No. is : " + hfJCNO.Value + ".";
            }
            else
            {
                message = message + " New Job card not Created.";
            }

            if (hfEstiNo.Value != null && hfEstiNo.Value != string.Empty && hfEstiNo.Value != "")
            {
                message = message + " New Estimate Created. Estimate No. is : " + hfEstiNo.Value + ".";
            }
            else
            {
                message = message + " New Estimate not Created. Job Specification created successfully.";
            }
            message = message + " Job Specification created successfully.";
            return message;
        }

        public void CreatePRPONew()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    string message = ReturnMessage();
                    DataTable dtCOMMISSIONAPI = new DataTable();
                    dtCOMMISSIONAPI = objMainClass.GetWAData("GETCOMMISSION", 1, "GETWADATA");
                    if (dtCOMMISSIONAPI.Rows.Count > 0)
                    {
                        DataTable dtPRAPI = new DataTable();
                        dtPRAPI = objMainClass.GetWAData("INSERTPR", 1, "GETWADATA");
                        if (dtPRAPI.Rows.Count > 0)
                        {
                            DataTable dtPOAPI = new DataTable();
                            dtPOAPI = objMainClass.GetWAData("INSERTPO", 1, "GETWADATA");
                            if (dtPOAPI.Rows.Count > 0)
                            {
                                DataTable dtGRNAPI = new DataTable();
                                dtGRNAPI = objMainClass.GetWAData("CREATEGRN", 1, "GETWADATA");
                                if (dtGRNAPI.Rows.Count > 0)
                                {
                                    DataTable dtPBAPI = new DataTable();
                                    dtPBAPI = objMainClass.GetWAData("INSERTPB", 1, "GETWADATA");
                                    if (dtPBAPI.Rows.Count > 0)
                                    {
                                        CommissionMaster objCommissionMaster = new CommissionMaster();
                                        objCommissionMaster.ITEMCATEGORY = Convert.ToInt32(hfProdItemID.Value);
                                        objCommissionMaster.STATUS = 1;
                                        objCommissionMaster.ACTION = "GETCOMMISSIONRATENEW";
                                        objCommissionMaster.CREATEBY = 0;
                                        objCommissionMaster.CREATEDATE = DateTime.Now;
                                        objCommissionMaster.PERORFIX = 0;
                                        objCommissionMaster.RATE = 0;
                                        objCommissionMaster.UPDATEBY = 0;
                                        objCommissionMaster.UPDATEDATE = DateTime.Now;
                                        objCommissionMaster.VENDCODE = objMainClass.strConvertZeroPadding(txtVendorCode.Text);

                                        string URLGetCommission = Convert.ToString(dtCOMMISSIONAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtCOMMISSIONAPI.Rows[0]["TOKEN"]);
                                        var clientGetCommission = new RestClient(URLGetCommission);
                                        clientGetCommission.Timeout = -1;
                                        var requestGetCommission = new RestRequest(Method.POST);
                                        requestGetCommission.AddHeader("" + Convert.ToString(dtCOMMISSIONAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtCOMMISSIONAPI.Rows[0]["KEYVALUE"]) + "");
                                        requestGetCommission.AddHeader("Content-Type", "application/json");
                                        var jsonGetCommission = JsonConvert.SerializeObject(objCommissionMaster);
                                        requestGetCommission.AddParameter("application/json", jsonGetCommission, ParameterType.RequestBody);
                                        IRestResponse responseGetCommission = clientGetCommission.Execute(requestGetCommission);

                                        if (responseGetCommission.StatusCode == HttpStatusCode.OK)
                                        {
                                            CommissionResponse objCommissionResponse = new CommissionResponse();
                                            string jsonconn = responseGetCommission.Content;
                                            objCommissionResponse = JsonConvert.DeserializeObject<CommissionResponse>(jsonconn);
                                            if (objCommissionResponse.Data.Rows.Count > 0)
                                            {

                                                bool blnPOExist = false;
                                                DataTable dtPO = new DataTable();
                                                DataTable dtSO = new DataTable();
                                                dtPO = objMainClass.CheckPOSOImeiNo(objMainClass.intCmpId, txtIMEI.Text, "CHECKPOWITHRETURN");
                                                if (dtPO.Rows.Count > 0)
                                                {
                                                    if (Convert.ToString(dtPO.Rows[0]["LASTPO"]) != "" && Convert.ToString(dtPO.Rows[0]["LASTPO"]) != string.Empty &&
                                                        Convert.ToString(dtPO.Rows[0]["LASTPO"]) != null &&
                                                        Convert.ToInt32(Convert.ToString(dtPO.Rows[0]["BALQTY"]) == "" ? 0 : Convert.ToInt32(dtPO.Rows[0]["BALQTY"])) > 0)
                                                    {
                                                        blnPOExist = true;


                                                        dtSO = objMainClass.CheckPOSOImeiNo(objMainClass.intCmpId, txtIMEI.Text, "CHECKSOWITHRETURN");

                                                        if (dtSO.Rows.Count > 0)
                                                        {
                                                            if (Convert.ToString(dtSO.Rows[0]["LASTSO"]) != "" && Convert.ToString(dtSO.Rows[0]["LASTSO"]) != string.Empty &&
                                                        Convert.ToString(dtSO.Rows[0]["LASTSO"]) != null &&
                                                        Convert.ToInt32(Convert.ToString(dtSO.Rows[0]["BALQTY"]) == "" ? 0 : Convert.ToInt32(dtSO.Rows[0]["BALQTY"])) > 0)
                                                            {
                                                                blnPOExist = false;
                                                            }

                                                        }

                                                    }
                                                }

                                                if (blnPOExist == false)
                                                {
                                                    string costcenter = "1007";
                                                    DataTable dtCostCenter = new DataTable();
                                                    dtCostCenter = objMainClass.GetCostCenter(hfPlantCd.Value, hfLocCd.Value);
                                                    if (dtCostCenter.Rows.Count > 0)
                                                    {
                                                        costcenter = Convert.ToString(dtCostCenter.Rows[0]["CSTCENTCD"]);
                                                    }

                                                    DataTable dtItemDesc = new DataTable();
                                                    dtItemDesc = objMainClass.GetItemDetailsByItemCode(objMainClass.intCmpId, hfItemCode.Value, 1, "ITEMMASTERSEARCH");

                                                    DataTable dtCommission = new DataTable();
                                                    dtCommission = objCommissionResponse.Data;
                                                    string CALCON = Convert.ToString(dtCommission.Rows[0]["CALCON"]);
                                                    int PERORFIX = Convert.ToInt32(dtCommission.Rows[0]["PERORFIX"]);
                                                    decimal RATE = Convert.ToDecimal(dtCommission.Rows[0]["RATE"]);

                                                    decimal purcomm = 0;
                                                    decimal purprice = 0;
                                                    decimal basprice = 0;
                                                    decimal tax = 0;

                                                    decimal othchg = 0;
                                                    decimal othbasechg = 0;
                                                    decimal othchgtax = 0;
                                                    decimal nettaxamt = 0;
                                                    decimal netpoamt = 0;

                                                    int ITEMGRPID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMGRP"]);
                                                    int ITEMID = Convert.ToInt32(dtItemDesc.Rows[0]["ITEMID"]);
                                                    int UOM = Convert.ToInt32(dtItemDesc.Rows[0]["UOM"]);
                                                    if (CALCON == "LOCK")
                                                    {
                                                        if (Convert.ToDecimal(hfLockAmt.Value) > 0)
                                                        {
                                                            if (PERORFIX == 1)
                                                            {
                                                                if (chkGST.Checked == true)
                                                                {

                                                                }
                                                                else
                                                                {

                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (chkGST.Checked == true)
                                                                {

                                                                }
                                                                else
                                                                {

                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            message = message + " But Lock Amount is 0. Please enter valid Lock Amount.!";
                                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (PERORFIX == 1)
                                                        {
                                                            if (chkGST.Checked == true)
                                                            {

                                                            }
                                                            else
                                                            {

                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (chkGST.Checked == true)
                                                            {

                                                            }
                                                            else
                                                            {

                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    message = message + " PO not created because PO is already made for same IMEI No.! PO No. : " + Convert.ToString(dtPO.Rows[0]["LASTPO"]) + ".!";
                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                                }
                                            }
                                            else
                                            {
                                                message = message + " But Commission Percentage or Amount not found. Please contact Administrator.!";
                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                            }

                                        }
                                        else
                                        {
                                            message = message + " But Commission Percentage or Amount not found. Please contact Administrator.!";
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                        }
                                    }
                                    else
                                    {
                                        message = message + " But PB API not found. Please Contact API Provider.!";
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                    }
                                }
                                else
                                {
                                    message = message + " But GRN API not found. Please Contact API Provider.!";
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                                }
                            }
                            else
                            {
                                message = message + " But PO API not found. Please Contact API Provider.!";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                            }
                        }
                        else
                        {
                            message = message + " But PR API not found. Please Contact API Provider.!";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
                        }
                    }
                    else
                    {
                        message = message + " But Commission API not found. Please Contact API Provider.!";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + message + "\");$('.close').click(function(){window.location.href ='frmOpenSO.aspx' });", true);
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

        protected void txtBillNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dtBillExist = objMainClass.CheckPurchaseBillPoGrNExist("", txtVendorCode.Text, "", "", txtBillNo.Text, "CHECKBILLEXIST");
                    if (dtBillExist.Rows.Count > 0)
                    {
                        BillError.Text = "Purchase Bill already made for this Bill No. PB No. : " + Convert.ToString(dtBillExist.Rows[0]["PBNO"]);
                        BillError.Visible = true;
                        btnCreateDoc.Enabled = false;
                    }
                    else
                    {
                        BillError.Text = string.Empty;
                        BillError.Visible = false;
                        btnCreateDoc.Enabled = true;
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

        protected void btnQtyYes_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
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

        protected void btnQtyNo_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

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

        protected void chkINVPO_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (chkINVPO.Checked == true)
                    {
                        rfvBillNo.Enabled = false;
                    }
                    else
                    {
                        rfvBillNo.Enabled = true;
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