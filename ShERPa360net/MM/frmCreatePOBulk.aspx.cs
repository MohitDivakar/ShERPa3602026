using Newtonsoft.Json;
using RestSharp;
using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.MM
{
    public partial class frmCreatePOBulk : System.Web.UI.Page
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
                    OleDbConnection MyConnection;
                    string extension = Path.GetExtension(fuImage.FileName);
                    string folderpath = "~/excel/";
                    string filePath = Path.Combine(Server.MapPath(folderpath), Guid.NewGuid().ToString("N") + extension);
                    fuImage.SaveAs(filePath);
                    DataTable dt = new DataTable();
                    if (extension == ".csv")
                    {
                        var items = (from line in System.IO.File.ReadAllLines(filePath)
                                     select Array.ConvertAll(line.Split(','), v => v.ToString().TrimStart("\" ".ToCharArray()).TrimEnd("\" ".ToCharArray()))).ToArray();
                        string[] strarr1 = items[0];
                        for (int x = 0; x <= items[0].Count() - 1; x++)
                            dt.Columns.Add(strarr1[x]);
                        foreach (var a in items)
                        {
                            DataRow dr = dt.NewRow();
                            dr.ItemArray = a;
                            dt.Rows.Add(dr);
                        }
                        if (dt.Rows.Count > 0)
                        {
                            dt.Rows.RemoveAt(0);
                        }
                    }
                    else
                    {
                        MyConnection = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 12.0;");
                        //MyConnection = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + flUpload.FileName + ";Extended Properties=Excel 8.0;");
                        MyConnection.Open();
                        DataTable dtExcelSchema = MyConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null/* TODO Change to default(_) if this is not a reference type */);
                        string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                        using (OleDbCommand cmd = new OleDbCommand())
                        {
                            using (OleDbDataAdapter oda = new OleDbDataAdapter())
                            {
                                cmd.CommandText = (Convert.ToString("SELECT * From [Sheet1$]"));
                                cmd.Connection = MyConnection;
                                oda.SelectCommand = cmd;
                                oda.Fill(dt);
                                MyConnection.Close();
                            }
                        }
                    }
                    File.Delete(filePath);

                    gvData.DataSource = string.Empty;
                    gvData.DataBind();

                    gvData.DataSource = dt;
                    gvData.DataBind();

                    if (dt.Rows.Count > 0)
                    {
                        lblRecoretxt.Visible = true;
                        lblRecord.Visible = true;
                        chkOnlyPO.Visible = true;
                        lblRecord.Text = dt.Rows.Count.ToString();
                        lnkSave.Visible = true;
                    }
                    else
                    {
                        lblRecoretxt.Visible = false;
                        lblRecord.Visible = false;
                        chkOnlyPO.Visible = false;
                        lblRecord.Text = "0";
                        lnkSave.Visible = false;
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
            DataTable dtPurchase = new DataTable("Data");
            dtPurchase.Columns.Add("BILLNO");
            dtPurchase.Columns.Add("PRNO");
            dtPurchase.Columns.Add("PONO");
            dtPurchase.Columns.Add("GRNNO");
            dtPurchase.Columns.Add("PBNO");
            dtPurchase.Columns.Add("MESSAGE");
            int ierror = 0;
            int isucess = 0;

            try
            {
                if (Session["USERID"] != null)
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };

                    List<PRDetails> objPRDetails = new List<PRDetails>();
                    List<PODetails> objPODetails = new List<PODetails>();
                    List<POTaxation> objPOTaxation = new List<POTaxation>();
                    List<POCharges> objPOCharges = new List<POCharges>();
                    List<PBCharges> objPBCharges = new List<PBCharges>();
                    List<GRNDetials> objGRNDetials = new List<GRNDetials>();
                    List<PBTaxation> objPBTaxation = new List<PBTaxation>();
                    List<PBDetails> objPBDetails = new List<PBDetails>();
                    string BILLNO = "";
                    int iPRSrno = 1;
                    decimal totalbaseamount = 0;
                    decimal totaltaxamount = 0;
                    decimal totaldiscountamount = 0;



                    GridViewRow row1 = gvData.Rows[gvData.Rows.Count - 1];
                    GridViewRow rowfirst = gvData.Rows[0];

                    foreach (GridViewRow row in gvData.Rows)
                    {
                        Label lblVendorCode = row.FindControl("lblVendorCode") as Label;
                        Label lblTranCode = row.FindControl("lblTranCode") as Label;
                        Label lblItemCode = row.FindControl("lblItemCode") as Label;
                        Label lblQty = row.FindControl("lblQty") as Label;
                        Label lblRate = row.FindControl("lblRate") as Label;
                        Label lblDiscount = row.FindControl("lblDiscount") as Label;
                        Label lblNetAmount = row.FindControl("lblNetAmount") as Label;
                        Label lblTaxRate = row.FindControl("lblTaxRate") as Label;
                        Label lblTaxAmount = row.FindControl("lblTaxAmount") as Label;
                        Label lblTotalAmount = row.FindControl("lblTotalAmount") as Label;
                        Label lblPlant = row.FindControl("lblPlant") as Label;
                        Label lblLocation = row.FindControl("lblLocation") as Label;
                        Label lblJobID = row.FindControl("lblJobID") as Label;
                        Label lblIMEI = row.FindControl("lblIMEI") as Label;
                        Label lblRemarks = row.FindControl("lblRemarks") as Label;
                        Label lblBillNo = row.FindControl("lblBillNo") as Label;
                        Label lblBillDate = row.FindControl("lblBillDate") as Label;
                        Label lblBillAmount = row.FindControl("lblBillAmount") as Label;
                        Label lblPayTerms = row.FindControl("lblPayTerms") as Label;
                        Label lblDepartment = row.FindControl("lblDepartment") as Label;
                        Label lblPurchaseType = row.FindControl("lblPurchaseType") as Label;
                        Label lblCostCenter = row.FindControl("lblCostCenter") as Label;


                        string[] payterms = lblPayTerms.Text.Split('-');
                        string payment = payterms.Count() > 0 ? Convert.ToString(payterms[0]).Trim() : "";
                        string paymentdesc = payterms.Count() > 1 ? Convert.ToString(payterms[1]).Trim() : "";

                        string[] department = lblDepartment.Text.Split('-');
                        string depart = department.Count() > 0 ? Convert.ToString(department[0]).Trim() : "";

                        string[] purchasetype = lblPurchaseType.Text.Split('-');
                        string purchase = purchasetype.Count() > 0 ? Convert.ToString(purchasetype[0]).Trim() : "";


                        string costcenter = "1007";
                        if (lblCostCenter.Text != "" && lblCostCenter.Text != null && lblCostCenter.Text != string.Empty)
                        {
                            costcenter = lblCostCenter.Text;
                        }
                        //DataTable dtCostCenter = new DataTable();
                        //dtCostCenter = objMainClass.GetCostCenter(lblPlant.Text, lblLocation.Text);
                        //if (dtCostCenter.Rows.Count > 0)
                        //{
                        //    costcenter = Convert.ToString(dtCostCenter.Rows[0]["CSTCENTCD"]);
                        //}

                        DataTable dtItem = new DataTable();
                        dtItem = objMainClass.SelectItem("", "", lblItemCode.Text, "", "", "", "");

                        DataTable dtax = new DataTable();
                        if (lblTaxRate.Text != string.Empty && lblTaxRate.Text != null && lblTaxRate.Text != "" && Convert.ToInt32(lblTaxRate.Text) > 0)
                        {
                            dtax = objMainClass.GetTaxByRate(Convert.ToDecimal(lblTaxRate.Text), "GETDATABYRATE");
                        }


                        if (BILLNO == lblBillNo.Text)
                        {
                            objPRDetails.Add(new PRDetails
                            {
                                ASSETCD = "",
                                CAMOUNT = Convert.ToDecimal(lblTotalAmount.Text),
                                CMPID = objMainClass.intCmpId,
                                CSTCENTCD = costcenter,
                                DELIDT = DateTime.Now,
                                GLCD = Convert.ToString(dtItem.Rows[0]["GLCODE"]),  //"10010000",
                                IMEINO = lblIMEI.Text,
                                ITEMDESC = Convert.ToString(dtItem.Rows[0]["Desciption"]),
                                ITEMGRPID = Convert.ToInt32(dtItem.Rows[0]["ITEMGRP"]),
                                ITEMID = Convert.ToInt32(dtItem.Rows[0]["ITEMID"]),
                                ITEMTEXT = "AUTO PR/PO",
                                LOCCD = lblLocation.Text,
                                PARTREQNO = 0,
                                PLANTCD = lblPlant.Text,
                                PRBY = "",
                                PRFCNT = "1000",
                                PRNO = "",
                                PRQTY = Convert.ToDecimal(lblQty.Text),
                                RATE = Convert.ToDecimal(lblTotalAmount.Text) / Convert.ToDecimal(lblQty.Text),
                                SRNO = iPRSrno,
                                STATUS = 2,
                                TRNUM = objMainClass.strConvertZeroPadding(lblJobID.Text),
                                UOM = Convert.ToInt32(dtItem.Rows[0]["UOM"]),
                            });

                            objPODetails.Add(new PODetails
                            {
                                APRVBY = 17,
                                APRVDATE = DateTime.Now,
                                APRVSTATUS = 260,
                                ASSETCD = "",
                                BRATE = Convert.ToDecimal(lblRate.Text),
                                CAMOUNT = Convert.ToDecimal(lblRate.Text) * Convert.ToDecimal(lblQty.Text),
                                CMPID = objMainClass.intCmpId,
                                CSTCENTCD = costcenter,
                                DELIDT = DateTime.Now,
                                DEVREASON = "OK",
                                DISCAMT = Convert.ToDecimal(lblDiscount.Text),
                                FROMLOCCD = "",
                                FROMPLANTCD = "",
                                GLCD = "10010000",
                                IMEINO = lblIMEI.Text,
                                ITEMDESC = Convert.ToString(dtItem.Rows[0]["Desciption"]),
                                ITEMGRPID = Convert.ToInt32(dtItem.Rows[0]["ITEMGRP"]),
                                ITEMID = Convert.ToInt32(dtItem.Rows[0]["ITEMID"]),
                                //ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",  //Removed AUTO PO AGAINST PO text from remarks
                                ITEMTEXT = "",
                                LOCCD = lblLocation.Text,
                                LOCKAMT = 0,
                                PLANTCD = lblPlant.Text,
                                PONO = "",
                                POQTY = Convert.ToDecimal(lblQty.Text),
                                PRFCNT = "1000",
                                PRNO = "",
                                PRSRNO = iPRSrno,
                                RATE = Convert.ToDecimal(lblRate.Text) + (Convert.ToDecimal(lblTaxAmount.Text) / Convert.ToDecimal(lblQty.Text)),
                                REFNO = "",
                                REJREASON = "",
                                SRNO = iPRSrno,
                                TAXAMT = 0,
                                TRNUM = objMainClass.strConvertZeroPadding(lblJobID.Text),
                                UOM = Convert.ToInt32(dtItem.Rows[0]["UOM"]),
                            });

                            objPBDetails.Add(new PBDetails
                            {
                                ASSETCD = "",
                                BRATE = Convert.ToDecimal(lblRate.Text),
                                CAMOUNT = Convert.ToDecimal(lblRate.Text) * Convert.ToDecimal(lblQty.Text),
                                CHALLANNO = "",
                                CMPID = objMainClass.intCmpId,
                                CSTCENTCD = costcenter,
                                DISCAMT = Convert.ToDecimal(lblDiscount.Text),
                                GLCD = "10010000",
                                ITEMDESC = Convert.ToString(dtItem.Rows[0]["Desciption"]),
                                ITEMGRPID = Convert.ToInt32(dtItem.Rows[0]["ITEMGRP"]),
                                ITEMID = Convert.ToInt32(dtItem.Rows[0]["ITEMID"]),
                                //ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",  //Removed AUTO PO AGAINST PO text from remarks
                                ITEMTEXT = "",
                                LOCCD = lblLocation.Text,
                                MIRNO = hfGRNNo.Value,
                                MIRSRNO = iPRSrno,
                                PBNO = "",
                                PBQTY = Convert.ToDecimal(lblQty.Text),
                                PLANTCD = lblPlant.Text,
                                PONO = hfPONo.Value,
                                POSRNO = iPRSrno,
                                PRFCNT = "1000",
                                RATE = Convert.ToDecimal(lblRate.Text) + (Convert.ToDecimal(lblTaxAmount.Text) / Convert.ToDecimal(lblQty.Text)),
                                REFNO = "",
                                SRNO = iPRSrno,
                                TAXAMT = 0,
                                TRNUM = objMainClass.strConvertZeroPadding(lblJobID.Text),
                                UOM = Convert.ToInt32(dtItem.Rows[0]["UOM"]),
                            });

                            if (dtax.Rows.Count > 0)
                            {
                                objPOTaxation.Add(new POTaxation
                                {
                                    BASEAMT = (Convert.ToDecimal(lblRate.Text) * Convert.ToDecimal(lblQty.Text)) - (Convert.ToDecimal(lblDiscount.Text)),
                                    CMPID = objMainClass.intCmpId,
                                    CONDID = Convert.ToInt32(dtax.Rows[0]["ID"]),
                                    CONDORDER = iPRSrno,
                                    CONDTYPE = Convert.ToString(dtax.Rows[0]["CONDTYPE"]),
                                    GLCODE = "",
                                    OPERATOR = "+",
                                    PID = 0,
                                    PONO = "",
                                    POSRNO = iPRSrno,
                                    RATE = Convert.ToDecimal(dtax.Rows[0]["RATE"]),
                                    SRNO = iPRSrno,
                                    TAXAMT = Convert.ToDecimal(lblTaxAmount.Text),
                                });

                                objPBTaxation.Add(new PBTaxation
                                {
                                    BASEAMT = (Convert.ToDecimal(lblRate.Text) * Convert.ToDecimal(lblQty.Text)) - (Convert.ToDecimal(lblDiscount.Text)),
                                    CMPID = objMainClass.intCmpId,
                                    CONDID = Convert.ToInt32(dtax.Rows[0]["ID"]),
                                    CONDORDER = iPRSrno,
                                    CONDTYPE = Convert.ToString(dtax.Rows[0]["CONDTYPE"]),
                                    GLCODE = "",
                                    OPERATOR = "+",
                                    PBNO = "",
                                    PBSRNO = iPRSrno,
                                    PID = 0,
                                    RATE = Convert.ToDecimal(dtax.Rows[0]["RATE"]),
                                    SRNO = iPRSrno,
                                    TAXAMT = Convert.ToDecimal(lblTaxAmount.Text),
                                });
                            }

                            objGRNDetials.Add(new GRNDetials
                            {
                                ASSETCD = "",
                                CAMOUNT = Convert.ToDecimal(lblRate.Text) * Convert.ToDecimal(lblQty.Text),
                                CHLNQTY = 1,
                                CMPID = objMainClass.intCmpId,
                                CSTCENTCD = costcenter,
                                DOCNO = "",
                                DOCTYPE = "103",
                                FINYEAR = DateTime.Now.Year,
                                GLCD = "10010000",
                                ITEMDESC = Convert.ToString(dtItem.Rows[0]["Desciption"]),
                                ITEMGRPID = Convert.ToInt32(dtItem.Rows[0]["ITEMGRP"]),
                                ITEMID = Convert.ToInt32(dtItem.Rows[0]["ITEMID"]),
                                //ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",  //Removed AUTO PO AGAINST PO text from remarks
                                ITEMTEXT = "",
                                LOCCD = lblLocation.Text,
                                PLANTCD = lblPlant.Text,
                                PONO = hfPONo.Value,
                                POSRNO = iPRSrno,
                                PRFCNT = "1000",
                                QTY = Convert.ToDecimal(lblQty.Text),
                                RATE = Convert.ToDecimal(lblRate.Text),
                                SRNO = iPRSrno,
                                TRACKNO = objMainClass.strConvertZeroPadding(lblJobID.Text),
                                UOM = Convert.ToInt32(dtItem.Rows[0]["UOM"]),
                            });



                            iPRSrno++;

                            totalbaseamount = totalbaseamount + ((Convert.ToDecimal(lblRate.Text) * Convert.ToDecimal(lblQty.Text)));
                            totaltaxamount = totaltaxamount + (Convert.ToDecimal(lblTaxAmount.Text));
                            totaldiscountamount = totaldiscountamount + (Convert.ToDecimal(lblDiscount.Text));

                            if (row == row1)
                            {
                                if (objPRDetails.Count > 0)
                                {
                                    DataTable dtPRAPI = new DataTable();
                                    dtPRAPI = objMainClass.GetWAData("INSERTPR", 1, "GETWADATA");

                                    PRMaster objPRMaster = new PRMaster();
                                    objPRMaster.CMPID = objMainClass.intCmpId;
                                    objPRMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                    objPRMaster.CREATEDATE = DateTime.Now;
                                    objPRMaster.DEPTID = Convert.ToInt32(depart);
                                    objPRMaster.ISPRSTO = 0;
                                    objPRMaster.LISTINGID = 0;
                                    objPRMaster.PRDT = DateTime.Now;
                                    objPRMaster.PRNO = "";
                                    objPRMaster.PRTYPE = "MPR";
                                    objPRMaster.REMARK = lblRemarks.Text;
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

                                        for (int podet = 0; podet < objPODetails.Count; podet++)
                                        {
                                            objPODetails[podet].PRNO = hfPRNo.Value;
                                        }

                                        #region PO Master Data...
                                        POMaster objPOMaster = new POMaster();
                                        objPOMaster.ADJAMT = 0;
                                        objPOMaster.ADVAMT = 0;
                                        objPOMaster.AGENTNAME = "";
                                        objPOMaster.APRVBY = 17;
                                        objPOMaster.APRVDATE = DateTime.Now;
                                        objPOMaster.CMPID = objMainClass.intCmpId;
                                        objPOMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                        objPOMaster.CREATEDATE = DateTime.Now;
                                        objPOMaster.DEPTID = Convert.ToInt32(depart);
                                        objPOMaster.DISCOUNT = totaldiscountamount;
                                        objPOMaster.NETMATVALUE = totalbaseamount;
                                        objPOMaster.NETPOAMT = totalbaseamount + totaltaxamount - totaldiscountamount;
                                        objPOMaster.NETTAXAMT = totaltaxamount;
                                        objPOMaster.OLDPOAMT = 0;
                                        objPOMaster.PENDINGAMT = totalbaseamount + totaltaxamount - totaldiscountamount;
                                        objPOMaster.PMTTERMS = payment;
                                        objPOMaster.PMTTERMSDESC = paymentdesc;
                                        objPOMaster.PODetail = objPODetails;
                                        objPOMaster.PODT = DateTime.Now;
                                        objPOMaster.PONO = "";
                                        objPOMaster.POTax = objPOTaxation;
                                        objPOMaster.POCharge = new List<POCharges>();
                                        objPOMaster.POTYPE = "MPO";
                                        objPOMaster.PURTYPE = Convert.ToInt32(purchase);
                                        objPOMaster.REMARK = lblRemarks.Text;
                                        objPOMaster.STATUS = 57;
                                        objPOMaster.TRANCODE = lblTranCode.Text;
                                        objPOMaster.VENDCODE = lblVendorCode.Text;
                                        #endregion

                                        DataTable dtPOAPI = new DataTable();
                                        dtPOAPI = objMainClass.GetWAData("INSERTPO", 1, "GETWADATA");
                                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                        System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                                        string POURL = Convert.ToString(dtPOAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtPOAPI.Rows[0]["TOKEN"]);
                                        //string POURL = "https://localhost:44397/api/InsertPO";
                                        var clientPO = new RestClient(POURL);
                                        clientPO.Timeout = -1;
                                        var requestPO = new RestRequest(Method.POST);
                                        requestPO.AddHeader("" + Convert.ToString(dtPOAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtPOAPI.Rows[0]["KEYVALUE"]) + "");
                                        //requestPO.AddHeader("AUTH_KEY", "MOBEXAPP123");
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

                                            for (int podet = 0; podet < objGRNDetials.Count; podet++)
                                            {
                                                objGRNDetials[podet].PONO = hfPONo.Value;
                                            }
                                            if (chkOnlyPO.Checked == false)
                                            {
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
                                                objGRNMaster.REFDOCYEAR = Convert.ToInt32(lblVendorCode.Text);
                                                objGRNMaster.REFNO = hfPRNo.Value;
                                                objGRNMaster.REMARK = lblRemarks.Text;
                                                objGRNMaster.TRANCODE = lblTranCode.Text;

                                                DataTable dtGRNAPI = new DataTable();
                                                dtGRNAPI = objMainClass.GetWAData("CREATEGRN", 1, "GETWADATA");

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

                                                    for (int podet = 0; podet < objPBDetails.Count; podet++)
                                                    {
                                                        objPBDetails[podet].PONO = hfPONo.Value;
                                                    }

                                                    for (int podet = 0; podet < objPBDetails.Count; podet++)
                                                    {
                                                        objPBDetails[podet].MIRNO = hfGRNNo.Value;
                                                    }

                                                    PBMaster objPBMaster = new PBMaster();
                                                    objPBMaster.ADJAMT = 0;
                                                    objPBMaster.BILLAMT = Convert.ToDecimal(lblBillAmount.Text);
                                                    objPBMaster.BILLDT = Convert.ToDateTime(lblBillDate.Text);
                                                    objPBMaster.BILLNO = BILLNO;
                                                    objPBMaster.CMPID = objMainClass.intCmpId;
                                                    objPBMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                    objPBMaster.DISCOUNT = totaldiscountamount;
                                                    objPBMaster.NETMATVALUE = totalbaseamount;
                                                    objPBMaster.NETPBAMT = totalbaseamount + totaltaxamount - totaldiscountamount;
                                                    objPBMaster.NETTAXAMT = totaltaxamount;
                                                    objPBMaster.PAIDAMT = 0;
                                                    objPBMaster.PBCharge = new List<PBCharges>();
                                                    objPBMaster.PBDetail = objPBDetails;
                                                    objPBMaster.PBDT = DateTime.Now;
                                                    objPBMaster.PBNO = "";
                                                    objPBMaster.PBTax = objPBTaxation;
                                                    objPBMaster.PBTYPE = "MPB";
                                                    objPBMaster.PENDINGAMT = totalbaseamount + totaltaxamount - totaldiscountamount;
                                                    objPBMaster.PMTTERMS = payment;
                                                    objPBMaster.PMTTERMSDESC = paymentdesc;
                                                    objPBMaster.REMARK = lblRemarks.Text;
                                                    objPBMaster.STATUS = 1;
                                                    objPBMaster.VENDCODE = lblVendorCode.Text;

                                                    DataTable dtPBAPI = new DataTable();
                                                    dtPBAPI = objMainClass.GetWAData("INSERTPB", 1, "GETWADATA");

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

                                                        dtPurchase.Rows.Add(BILLNO, hfPRNo.Value, hfPONo.Value, hfGRNNo.Value, hfPBNo.Value, "All purchase documents successfully created for this Bill No.!");
                                                        isucess++;
                                                        JobStatusEntry();
                                                    }
                                                    else
                                                    {
                                                        dtPurchase.Rows.Add(BILLNO, hfPRNo.Value, hfPONo.Value, hfGRNNo.Value, "", "Something went wrong while creating PB for this Bill No.!");
                                                        ierror++;
                                                    }
                                                }
                                                else
                                                {
                                                    dtPurchase.Rows.Add(BILLNO, hfPRNo.Value, hfPONo.Value, "", "", "Something went wrong while creating GRN for this Bill No.!");
                                                    ierror++;
                                                }
                                            }
                                            else
                                            {
                                                dtPurchase.Rows.Add(lblBillNo.Text, hfPRNo.Value, hfPONo.Value, hfGRNNo.Value, hfPBNo.Value, "PR/PO successfully created.!");
                                                isucess++;
                                                SendMail(Convert.ToInt32(depart), Convert.ToString(objPRDetails[0].PLANTCD), objPOMaster.NETPOAMT);
                                                JobStatusEntry();
                                            }
                                        }
                                        else
                                        {
                                            dtPurchase.Rows.Add(BILLNO, hfPRNo.Value, "", "", "", "Something went wrong while creating PO for this Bill No.!");
                                            ierror++;
                                        }
                                    }
                                    else
                                    {
                                        dtPurchase.Rows.Add(BILLNO, "", "", "", "", "Something went wrong while creating PR for this Bill No.!");
                                        ierror++;
                                    }
                                }
                                else
                                {
                                    dtPurchase.Rows.Add(BILLNO, "", "", "", "", "Line item not added for this Bill No.!");
                                    ierror++;
                                }
                            }
                            BILLNO = lblBillNo.Text;
                        }
                        else
                        {

                            if (objPRDetails.Count > 0)
                            {
                                DataTable dtPRAPI = new DataTable();
                                dtPRAPI = objMainClass.GetWAData("INSERTPR", 1, "GETWADATA");

                                PRMaster objPRMaster = new PRMaster();
                                objPRMaster.CMPID = objMainClass.intCmpId;
                                objPRMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                objPRMaster.CREATEDATE = DateTime.Now;
                                objPRMaster.DEPTID = Convert.ToInt32(depart);
                                objPRMaster.ISPRSTO = 0;
                                objPRMaster.LISTINGID = 0;
                                objPRMaster.PRDT = DateTime.Now;
                                objPRMaster.PRNO = "";
                                objPRMaster.PRTYPE = "MPR";
                                objPRMaster.REMARK = lblRemarks.Text;
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

                                    for (int podet = 0; podet < objPODetails.Count; podet++)
                                    {
                                        objPODetails[podet].PRNO = hfPRNo.Value;
                                    }

                                    #region PO Master Data...
                                    POMaster objPOMaster = new POMaster();
                                    objPOMaster.ADJAMT = 0;
                                    objPOMaster.ADVAMT = 0;
                                    objPOMaster.AGENTNAME = "";
                                    objPOMaster.APRVBY = 17;
                                    objPOMaster.APRVDATE = DateTime.Now;
                                    objPOMaster.CMPID = objMainClass.intCmpId;
                                    objPOMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                    objPOMaster.CREATEDATE = DateTime.Now;
                                    objPOMaster.DEPTID = Convert.ToInt32(depart);
                                    objPOMaster.DISCOUNT = totaldiscountamount;
                                    objPOMaster.NETMATVALUE = totalbaseamount;
                                    objPOMaster.NETPOAMT = totalbaseamount + totaltaxamount - totaldiscountamount;
                                    objPOMaster.NETTAXAMT = totaltaxamount;
                                    objPOMaster.OLDPOAMT = 0;
                                    objPOMaster.PENDINGAMT = totalbaseamount + totaltaxamount - totaldiscountamount;
                                    objPOMaster.PMTTERMS = payment;
                                    objPOMaster.PMTTERMSDESC = paymentdesc;
                                    objPOMaster.POCharge = new List<POCharges>();
                                    objPOMaster.PODetail = objPODetails;
                                    objPOMaster.PODT = DateTime.Now;
                                    objPOMaster.PONO = "";
                                    objPOMaster.POTax = objPOTaxation;
                                    objPOMaster.POTYPE = "MPO";
                                    objPOMaster.PURTYPE = Convert.ToInt32(purchase);
                                    objPOMaster.REMARK = lblRemarks.Text;
                                    objPOMaster.STATUS = 57;
                                    objPOMaster.TRANCODE = lblTranCode.Text;
                                    objPOMaster.VENDCODE = lblVendorCode.Text;
                                    #endregion

                                    DataTable dtPOAPI = new DataTable();
                                    dtPOAPI = objMainClass.GetWAData("INSERTPO", 1, "GETWADATA");
                                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                    System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                                    string POURL = Convert.ToString(dtPOAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtPOAPI.Rows[0]["TOKEN"]);
                                    //string POURL = "https://localhost:44397/api/InsertPO";
                                    var clientPO = new RestClient(POURL);
                                    clientPO.Timeout = -1;
                                    var requestPO = new RestRequest(Method.POST);
                                    requestPO.AddHeader("" + Convert.ToString(dtPOAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtPOAPI.Rows[0]["KEYVALUE"]) + "");
                                    //requestPO.AddHeader("AUTH_KEY", "MOBEXAPP123");
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

                                        for (int podet = 0; podet < objGRNDetials.Count; podet++)
                                        {
                                            objGRNDetials[podet].PONO = hfPONo.Value;
                                        }
                                        if (chkOnlyPO.Checked == false)
                                        {
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
                                            objGRNMaster.REFDOCYEAR = Convert.ToInt32(lblVendorCode.Text);
                                            objGRNMaster.REFNO = hfPRNo.Value;
                                            objGRNMaster.REMARK = lblRemarks.Text;
                                            objGRNMaster.TRANCODE = lblTranCode.Text;

                                            DataTable dtGRNAPI = new DataTable();
                                            dtGRNAPI = objMainClass.GetWAData("CREATEGRN", 1, "GETWADATA");

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

                                                for (int podet = 0; podet < objPBDetails.Count; podet++)
                                                {
                                                    objPBDetails[podet].PONO = hfPONo.Value;
                                                }

                                                for (int podet = 0; podet < objPBDetails.Count; podet++)
                                                {
                                                    objPBDetails[podet].MIRNO = hfGRNNo.Value;
                                                }

                                                PBMaster objPBMaster = new PBMaster();
                                                objPBMaster.ADJAMT = 0;
                                                objPBMaster.BILLAMT = Convert.ToDecimal(lblBillAmount.Text);
                                                objPBMaster.BILLDT = Convert.ToDateTime(lblBillDate.Text);
                                                objPBMaster.BILLNO = BILLNO;
                                                objPBMaster.CMPID = objMainClass.intCmpId;
                                                objPBMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                objPBMaster.DISCOUNT = totaldiscountamount;
                                                objPBMaster.NETMATVALUE = totalbaseamount;
                                                objPBMaster.NETPBAMT = totalbaseamount + totaltaxamount - totaldiscountamount;
                                                objPBMaster.NETTAXAMT = totaltaxamount;
                                                objPBMaster.PAIDAMT = 0;
                                                objPBMaster.PBCharge = new List<PBCharges>();
                                                objPBMaster.PBDetail = objPBDetails;
                                                objPBMaster.PBDT = DateTime.Now;
                                                objPBMaster.PBNO = "";
                                                objPBMaster.PBTax = objPBTaxation;
                                                objPBMaster.PBTYPE = "MPB";
                                                objPBMaster.PENDINGAMT = totalbaseamount + totaltaxamount - totaldiscountamount;
                                                objPBMaster.PMTTERMS = payment;
                                                objPBMaster.PMTTERMSDESC = paymentdesc;
                                                objPBMaster.REMARK = lblRemarks.Text;
                                                objPBMaster.STATUS = 1;
                                                objPBMaster.VENDCODE = lblVendorCode.Text;

                                                DataTable dtPBAPI = new DataTable();
                                                dtPBAPI = objMainClass.GetWAData("INSERTPB", 1, "GETWADATA");

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

                                                    dtPurchase.Rows.Add(BILLNO, hfPRNo.Value, hfPONo.Value, hfGRNNo.Value, hfPBNo.Value, "All purchase documents successfully created for this Bill No.!");
                                                    isucess++;
                                                    JobStatusEntry();

                                                }
                                                else
                                                {
                                                    dtPurchase.Rows.Add(BILLNO, hfPRNo.Value, hfPONo.Value, hfGRNNo.Value, "", "Something went wrong while creating PB for this Bill No.!");
                                                    ierror++;
                                                }
                                            }
                                            else
                                            {
                                                dtPurchase.Rows.Add(BILLNO, hfPRNo.Value, hfPONo.Value, "", "", "Something went wrong while creating GRN for this Bill No.!");
                                                ierror++;
                                            }
                                        }
                                        else
                                        {
                                            dtPurchase.Rows.Add(lblBillNo.Text, hfPRNo.Value, hfPONo.Value, hfGRNNo.Value, hfPBNo.Value, "PR/PO successfully created.!");
                                            isucess++;
                                            SendMail(Convert.ToInt32(depart), Convert.ToString(objPRDetails[0].PLANTCD), objPOMaster.NETPOAMT);
                                            JobStatusEntry();
                                        }
                                    }
                                    else
                                    {
                                        dtPurchase.Rows.Add(BILLNO, hfPRNo.Value, "", "", "", "Something went wrong while creating PO for this Bill No.!");
                                        ierror++;
                                    }
                                }
                                else
                                {
                                    dtPurchase.Rows.Add(BILLNO, "", "", "", "", "Something went wrong while creating PR for this Bill No.!");
                                    ierror++;
                                }
                            }
                            else
                            {
                                if (row == rowfirst)
                                {

                                }
                                else
                                {
                                    dtPurchase.Rows.Add(BILLNO, "", "", "", "", "Line item not added for this Bill No.!");
                                    ierror++;
                                }
                            }

                            iPRSrno = 1;
                            objPRDetails = new List<PRDetails>();
                            objPODetails = new List<PODetails>();
                            objPOTaxation = new List<POTaxation>();
                            objPOCharges = new List<POCharges>();
                            objPBCharges = new List<PBCharges>();
                            objGRNDetials = new List<GRNDetials>();
                            objPBTaxation = new List<PBTaxation>();
                            objPBDetails = new List<PBDetails>();


                            objPRDetails.Add(new PRDetails
                            {
                                ASSETCD = "",
                                CAMOUNT = Convert.ToDecimal(lblTotalAmount.Text),
                                CMPID = objMainClass.intCmpId,
                                CSTCENTCD = costcenter,
                                DELIDT = DateTime.Now,
                                GLCD = Convert.ToString(dtItem.Rows[0]["GLCODE"]),  //"10010000",
                                IMEINO = lblIMEI.Text,
                                ITEMDESC = Convert.ToString(dtItem.Rows[0]["Desciption"]),
                                ITEMGRPID = Convert.ToInt32(dtItem.Rows[0]["ITEMGRP"]),
                                ITEMID = Convert.ToInt32(dtItem.Rows[0]["ITEMID"]),
                                ITEMTEXT = "AUTO PR/PO",
                                LOCCD = lblLocation.Text,
                                PARTREQNO = 0,
                                PLANTCD = lblPlant.Text,
                                PRBY = "",
                                PRFCNT = "1000",
                                PRNO = "",
                                PRQTY = Convert.ToDecimal(lblQty.Text),
                                RATE = Convert.ToDecimal(lblTotalAmount.Text) / Convert.ToDecimal(lblQty.Text),
                                SRNO = iPRSrno,
                                STATUS = 2,
                                TRNUM = objMainClass.strConvertZeroPadding(lblJobID.Text),
                                UOM = Convert.ToInt32(dtItem.Rows[0]["UOM"]),
                            });

                            objPODetails.Add(new PODetails
                            {
                                APRVBY = 17,
                                APRVDATE = DateTime.Now,
                                APRVSTATUS = 260,
                                ASSETCD = "",
                                BRATE = Convert.ToDecimal(lblRate.Text),
                                CAMOUNT = Convert.ToDecimal(lblRate.Text) * Convert.ToDecimal(lblQty.Text),
                                CMPID = objMainClass.intCmpId,
                                CSTCENTCD = costcenter,
                                DELIDT = DateTime.Now,
                                DEVREASON = "OK",
                                DISCAMT = Convert.ToDecimal(lblDiscount.Text),
                                FROMLOCCD = "",
                                FROMPLANTCD = "",
                                GLCD = "10010000",
                                IMEINO = lblIMEI.Text,
                                ITEMDESC = Convert.ToString(dtItem.Rows[0]["Desciption"]),
                                ITEMGRPID = Convert.ToInt32(dtItem.Rows[0]["ITEMGRP"]),
                                ITEMID = Convert.ToInt32(dtItem.Rows[0]["ITEMID"]),
                                //ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",  //Removed AUTO PO AGAINST PO text from remarks
                                ITEMTEXT = "",
                                LOCCD = lblLocation.Text,
                                LOCKAMT = 0,
                                PLANTCD = lblPlant.Text,
                                PONO = "",
                                POQTY = Convert.ToDecimal(lblQty.Text),
                                PRFCNT = "1000",
                                PRNO = "",
                                PRSRNO = iPRSrno,
                                RATE = Convert.ToDecimal(lblRate.Text) + (Convert.ToDecimal(lblTaxAmount.Text) / Convert.ToDecimal(lblQty.Text)),
                                REFNO = "",
                                REJREASON = "",
                                SRNO = iPRSrno,
                                TAXAMT = 0,
                                TRNUM = objMainClass.strConvertZeroPadding(lblJobID.Text),
                                UOM = Convert.ToInt32(dtItem.Rows[0]["UOM"]),
                            });

                            objPBDetails.Add(new PBDetails
                            {
                                ASSETCD = "",
                                BRATE = Convert.ToDecimal(lblRate.Text),
                                CAMOUNT = Convert.ToDecimal(lblRate.Text) * Convert.ToDecimal(lblQty.Text),
                                CHALLANNO = "",
                                CMPID = objMainClass.intCmpId,
                                CSTCENTCD = costcenter,
                                DISCAMT = Convert.ToDecimal(lblDiscount.Text),
                                GLCD = "10010000",
                                ITEMDESC = Convert.ToString(dtItem.Rows[0]["Desciption"]),
                                ITEMGRPID = Convert.ToInt32(dtItem.Rows[0]["ITEMGRP"]),
                                ITEMID = Convert.ToInt32(dtItem.Rows[0]["ITEMID"]),
                                //ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",  //Removed AUTO PO AGAINST PO text from remarks
                                ITEMTEXT = "",
                                LOCCD = lblLocation.Text,
                                MIRNO = hfGRNNo.Value,
                                MIRSRNO = iPRSrno,
                                PBNO = "",
                                PBQTY = Convert.ToDecimal(lblQty.Text),
                                PLANTCD = lblPlant.Text,
                                PONO = hfPONo.Value,
                                POSRNO = iPRSrno,
                                PRFCNT = "1000",
                                RATE = Convert.ToDecimal(lblRate.Text) + (Convert.ToDecimal(lblTaxAmount.Text) / Convert.ToDecimal(lblQty.Text)),
                                REFNO = "",
                                SRNO = iPRSrno,
                                TAXAMT = 0,
                                TRNUM = objMainClass.strConvertZeroPadding(lblJobID.Text),
                                UOM = Convert.ToInt32(dtItem.Rows[0]["UOM"]),
                            });

                            if (dtax.Rows.Count > 0)
                            {
                                objPOTaxation.Add(new POTaxation
                                {
                                    BASEAMT = (Convert.ToDecimal(lblRate.Text) * Convert.ToDecimal(lblQty.Text)) - (Convert.ToDecimal(lblDiscount.Text)),
                                    CMPID = objMainClass.intCmpId,
                                    CONDID = Convert.ToInt32(dtax.Rows[0]["ID"]),
                                    CONDORDER = iPRSrno,
                                    CONDTYPE = Convert.ToString(dtax.Rows[0]["CONDTYPE"]),
                                    GLCODE = "",
                                    OPERATOR = "+",
                                    PID = 0,
                                    PONO = "",
                                    POSRNO = iPRSrno,
                                    RATE = Convert.ToDecimal(dtax.Rows[0]["RATE"]),
                                    SRNO = iPRSrno,
                                    TAXAMT = Convert.ToDecimal(lblTaxAmount.Text),
                                });

                                objPBTaxation.Add(new PBTaxation
                                {
                                    BASEAMT = (Convert.ToDecimal(lblRate.Text) * Convert.ToDecimal(lblQty.Text)) - (Convert.ToDecimal(lblDiscount.Text)),
                                    CMPID = objMainClass.intCmpId,
                                    CONDID = Convert.ToInt32(dtax.Rows[0]["ID"]),
                                    CONDORDER = iPRSrno,
                                    CONDTYPE = Convert.ToString(dtax.Rows[0]["CONDTYPE"]),
                                    GLCODE = "",
                                    OPERATOR = "+",
                                    PBNO = "",
                                    PBSRNO = iPRSrno,
                                    PID = 0,
                                    RATE = Convert.ToDecimal(dtax.Rows[0]["RATE"]),
                                    SRNO = iPRSrno,
                                    TAXAMT = Convert.ToDecimal(lblTaxAmount.Text),
                                });
                            }

                            objGRNDetials.Add(new GRNDetials
                            {
                                ASSETCD = "",
                                CAMOUNT = Convert.ToDecimal(lblRate.Text) * Convert.ToDecimal(lblQty.Text),
                                CHLNQTY = 1,
                                CMPID = objMainClass.intCmpId,
                                CSTCENTCD = costcenter,
                                DOCNO = "",
                                DOCTYPE = "103",
                                FINYEAR = DateTime.Now.Year,
                                GLCD = "10010000",
                                ITEMDESC = Convert.ToString(dtItem.Rows[0]["Desciption"]),
                                ITEMGRPID = Convert.ToInt32(dtItem.Rows[0]["ITEMGRP"]),
                                ITEMID = Convert.ToInt32(dtItem.Rows[0]["ITEMID"]),
                                //ITEMTEXT = hfNewJobID.Value + "/" + txtIMEI.Text + " AUTO PO AGAINST SO",  //Removed AUTO PO AGAINST PO text from remarks
                                ITEMTEXT = "",
                                LOCCD = lblLocation.Text,
                                PLANTCD = lblPlant.Text,
                                PONO = hfPONo.Value,
                                POSRNO = iPRSrno,
                                PRFCNT = "1000",
                                QTY = Convert.ToDecimal(lblQty.Text),
                                RATE = Convert.ToDecimal(lblRate.Text),
                                SRNO = iPRSrno,
                                TRACKNO = objMainClass.strConvertZeroPadding(lblJobID.Text),
                                UOM = Convert.ToInt32(dtItem.Rows[0]["UOM"]),
                            });

                            iPRSrno++;

                            totalbaseamount = 0;
                            totaltaxamount = 0;
                            totaldiscountamount = 0;
                            totalbaseamount = totalbaseamount + ((Convert.ToDecimal(lblRate.Text) * Convert.ToDecimal(lblQty.Text)));
                            totaltaxamount = totaltaxamount + (Convert.ToDecimal(lblTaxAmount.Text));
                            totaldiscountamount = totaldiscountamount + (Convert.ToDecimal(lblDiscount.Text));



                            if (objPRDetails.Count > 0 && row == row1 && row1 == rowfirst && row == rowfirst)
                            {
                                DataTable dtPRAPI = new DataTable();
                                dtPRAPI = objMainClass.GetWAData("INSERTPR", 1, "GETWADATA");

                                PRMaster objPRMaster = new PRMaster();
                                objPRMaster.CMPID = objMainClass.intCmpId;
                                objPRMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                objPRMaster.CREATEDATE = DateTime.Now;
                                objPRMaster.DEPTID = Convert.ToInt32(depart);
                                objPRMaster.ISPRSTO = 0;
                                objPRMaster.LISTINGID = 0;
                                objPRMaster.PRDT = DateTime.Now;
                                objPRMaster.PRNO = "";
                                objPRMaster.PRTYPE = "MPR";
                                objPRMaster.REMARK = lblRemarks.Text;
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

                                    for (int podet = 0; podet < objPODetails.Count; podet++)
                                    {
                                        objPODetails[podet].PRNO = hfPRNo.Value;
                                    }

                                    #region PO Master Data...
                                    POMaster objPOMaster = new POMaster();
                                    objPOMaster.ADJAMT = 0;
                                    objPOMaster.ADVAMT = 0;
                                    objPOMaster.AGENTNAME = "";
                                    objPOMaster.APRVBY = 17;
                                    objPOMaster.APRVDATE = DateTime.Now;
                                    objPOMaster.CMPID = objMainClass.intCmpId;
                                    objPOMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                    objPOMaster.CREATEDATE = DateTime.Now;
                                    objPOMaster.DEPTID = Convert.ToInt32(depart);
                                    objPOMaster.DISCOUNT = totaldiscountamount;
                                    objPOMaster.NETMATVALUE = totalbaseamount;
                                    objPOMaster.NETPOAMT = totalbaseamount + totaltaxamount - totaldiscountamount;
                                    objPOMaster.NETTAXAMT = totaltaxamount;
                                    objPOMaster.OLDPOAMT = 0;
                                    objPOMaster.PENDINGAMT = totalbaseamount + totaltaxamount - totaldiscountamount;
                                    objPOMaster.PMTTERMS = payment;
                                    objPOMaster.PMTTERMSDESC = paymentdesc;
                                    objPOMaster.POCharge = new List<POCharges>();
                                    objPOMaster.PODetail = objPODetails;
                                    objPOMaster.PODT = DateTime.Now;
                                    objPOMaster.PONO = "";
                                    objPOMaster.POTax = objPOTaxation;
                                    objPOMaster.POTYPE = "MPO";
                                    objPOMaster.PURTYPE = Convert.ToInt32(purchase);
                                    objPOMaster.REMARK = lblRemarks.Text;
                                    objPOMaster.STATUS = 57;
                                    objPOMaster.TRANCODE = lblTranCode.Text;
                                    objPOMaster.VENDCODE = lblVendorCode.Text;
                                    #endregion

                                    DataTable dtPOAPI = new DataTable();
                                    dtPOAPI = objMainClass.GetWAData("INSERTPO", 1, "GETWADATA");
                                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                    System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                                    string POURL = Convert.ToString(dtPOAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtPOAPI.Rows[0]["TOKEN"]);
                                    //string POURL = "https://localhost:44397/api/InsertPO";
                                    var clientPO = new RestClient(POURL);
                                    clientPO.Timeout = -1;
                                    var requestPO = new RestRequest(Method.POST);
                                    requestPO.AddHeader("" + Convert.ToString(dtPOAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtPOAPI.Rows[0]["KEYVALUE"]) + "");
                                    //requestPO.AddHeader("AUTH_KEY", "MOBEXAPP123");
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

                                        for (int podet = 0; podet < objGRNDetials.Count; podet++)
                                        {
                                            objGRNDetials[podet].PONO = hfPONo.Value;
                                        }
                                        if (chkOnlyPO.Checked == false)
                                        {


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
                                            objGRNMaster.REFDOCYEAR = Convert.ToInt32(lblVendorCode.Text);
                                            objGRNMaster.REFNO = hfPRNo.Value;
                                            objGRNMaster.REMARK = lblRemarks.Text;
                                            objGRNMaster.TRANCODE = lblTranCode.Text;

                                            DataTable dtGRNAPI = new DataTable();
                                            dtGRNAPI = objMainClass.GetWAData("CREATEGRN", 1, "GETWADATA");

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

                                                for (int podet = 0; podet < objPBDetails.Count; podet++)
                                                {
                                                    objPBDetails[podet].PONO = hfPONo.Value;
                                                }

                                                for (int podet = 0; podet < objPBDetails.Count; podet++)
                                                {
                                                    objPBDetails[podet].MIRNO = hfGRNNo.Value;
                                                }

                                                PBMaster objPBMaster = new PBMaster();
                                                objPBMaster.ADJAMT = 0;
                                                objPBMaster.BILLAMT = Convert.ToDecimal(lblBillAmount.Text);
                                                objPBMaster.BILLDT = Convert.ToDateTime(lblBillDate.Text);
                                                objPBMaster.BILLNO = lblBillNo.Text;
                                                objPBMaster.CMPID = objMainClass.intCmpId;
                                                objPBMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                objPBMaster.DISCOUNT = totaldiscountamount;
                                                objPBMaster.NETMATVALUE = totalbaseamount;
                                                objPBMaster.NETPBAMT = totalbaseamount + totaltaxamount - totaldiscountamount;
                                                objPBMaster.NETTAXAMT = totaltaxamount;
                                                objPBMaster.PAIDAMT = 0;
                                                objPBMaster.PBCharge = new List<PBCharges>();
                                                objPBMaster.PBDetail = objPBDetails;
                                                objPBMaster.PBDT = DateTime.Now;
                                                objPBMaster.PBNO = "";
                                                objPBMaster.PBTax = objPBTaxation;
                                                objPBMaster.PBTYPE = "MPB";
                                                objPBMaster.PENDINGAMT = totalbaseamount + totaltaxamount - totaldiscountamount;
                                                objPBMaster.PMTTERMS = payment;
                                                objPBMaster.PMTTERMSDESC = paymentdesc;
                                                objPBMaster.REMARK = lblRemarks.Text;
                                                objPBMaster.STATUS = 1;
                                                objPBMaster.VENDCODE = lblVendorCode.Text;

                                                DataTable dtPBAPI = new DataTable();
                                                dtPBAPI = objMainClass.GetWAData("INSERTPB", 1, "GETWADATA");

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

                                                    dtPurchase.Rows.Add(lblBillNo.Text, hfPRNo.Value, hfPONo.Value, hfGRNNo.Value, hfPBNo.Value, "All purchase documents successfully created for this Bill No.!");
                                                    isucess++;
                                                    JobStatusEntry();
                                                }
                                                else
                                                {
                                                    dtPurchase.Rows.Add(lblBillNo.Text, hfPRNo.Value, hfPONo.Value, hfGRNNo.Value, "", "Something went wrong while creating PB for this Bill No.!");
                                                    ierror++;
                                                }
                                            }
                                            else
                                            {
                                                dtPurchase.Rows.Add(lblBillNo.Text, hfPRNo.Value, hfPONo.Value, "", "", "Something went wrong while creating GRN for this Bill No.!");
                                                ierror++;
                                            }
                                        }
                                        else
                                        {
                                            dtPurchase.Rows.Add(lblBillNo.Text, hfPRNo.Value, hfPONo.Value, hfGRNNo.Value, hfPBNo.Value, "PR/PO successfully created.!");
                                            isucess++;
                                            SendMail(Convert.ToInt32(depart), Convert.ToString(objPRDetails[0].PLANTCD), objPOMaster.NETPOAMT);
                                            JobStatusEntry();
                                        }
                                    }
                                    else
                                    {
                                        dtPurchase.Rows.Add(lblBillNo.Text, hfPRNo.Value, "", "", "", "Something went wrong while creating PO for this Bill No.!");
                                        ierror++;
                                    }
                                }
                                else
                                {
                                    dtPurchase.Rows.Add(lblBillNo.Text, "", "", "", "", "Something went wrong while creating PR for this Bill No.!");
                                    ierror++;
                                }
                            }
                            else
                            {
                                if (row == rowfirst)
                                {

                                }
                                else
                                {
                                    if (row == row1)
                                    {
                                        if (objPRDetails.Count > 0)
                                        {
                                            DataTable dtPRAPI = new DataTable();
                                            dtPRAPI = objMainClass.GetWAData("INSERTPR", 1, "GETWADATA");

                                            PRMaster objPRMaster = new PRMaster();
                                            objPRMaster.CMPID = objMainClass.intCmpId;
                                            objPRMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                            objPRMaster.CREATEDATE = DateTime.Now;
                                            objPRMaster.DEPTID = Convert.ToInt32(depart);
                                            objPRMaster.ISPRSTO = 0;
                                            objPRMaster.LISTINGID = 0;
                                            objPRMaster.PRDT = DateTime.Now;
                                            objPRMaster.PRNO = "";
                                            objPRMaster.PRTYPE = "MPR";
                                            objPRMaster.REMARK = lblRemarks.Text;
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

                                                for (int podet = 0; podet < objPODetails.Count; podet++)
                                                {
                                                    objPODetails[podet].PRNO = hfPRNo.Value;
                                                }

                                                #region PO Master Data...
                                                POMaster objPOMaster = new POMaster();
                                                objPOMaster.ADJAMT = 0;
                                                objPOMaster.ADVAMT = 0;
                                                objPOMaster.AGENTNAME = "";
                                                objPOMaster.APRVBY = 17;
                                                objPOMaster.APRVDATE = DateTime.Now;
                                                objPOMaster.CMPID = objMainClass.intCmpId;
                                                objPOMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                objPOMaster.CREATEDATE = DateTime.Now;
                                                objPOMaster.DEPTID = Convert.ToInt32(depart);
                                                objPOMaster.DISCOUNT = totaldiscountamount;
                                                objPOMaster.NETMATVALUE = totalbaseamount;
                                                objPOMaster.NETPOAMT = totalbaseamount + totaltaxamount - totaldiscountamount;
                                                objPOMaster.NETTAXAMT = totaltaxamount;
                                                objPOMaster.OLDPOAMT = 0;
                                                objPOMaster.PENDINGAMT = totalbaseamount + totaltaxamount - totaldiscountamount;
                                                objPOMaster.PMTTERMS = payment;
                                                objPOMaster.PMTTERMSDESC = paymentdesc;
                                                objPOMaster.PODetail = objPODetails;
                                                objPOMaster.PODT = DateTime.Now;
                                                objPOMaster.PONO = "";
                                                objPOMaster.POTax = objPOTaxation;
                                                objPOMaster.POCharge = new List<POCharges>();
                                                objPOMaster.POTYPE = "MPO";
                                                objPOMaster.PURTYPE = Convert.ToInt32(purchase);
                                                objPOMaster.REMARK = lblRemarks.Text;
                                                objPOMaster.STATUS = 57;
                                                objPOMaster.TRANCODE = lblTranCode.Text;
                                                objPOMaster.VENDCODE = lblVendorCode.Text;
                                                #endregion

                                                DataTable dtPOAPI = new DataTable();
                                                dtPOAPI = objMainClass.GetWAData("INSERTPO", 1, "GETWADATA");
                                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                                System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                                                string POURL = Convert.ToString(dtPOAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtPOAPI.Rows[0]["TOKEN"]);
                                                //string POURL = "https://localhost:44397/api/InsertPO";
                                                var clientPO = new RestClient(POURL);
                                                clientPO.Timeout = -1;
                                                var requestPO = new RestRequest(Method.POST);
                                                requestPO.AddHeader("" + Convert.ToString(dtPOAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtPOAPI.Rows[0]["KEYVALUE"]) + "");
                                                //requestPO.AddHeader("AUTH_KEY", "MOBEXAPP123");
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

                                                    for (int podet = 0; podet < objGRNDetials.Count; podet++)
                                                    {
                                                        objGRNDetials[podet].PONO = hfPONo.Value;
                                                    }
                                                    if (chkOnlyPO.Checked == false)
                                                    {
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
                                                        objGRNMaster.REFDOCYEAR = Convert.ToInt32(lblVendorCode.Text);
                                                        objGRNMaster.REFNO = hfPRNo.Value;
                                                        objGRNMaster.REMARK = lblRemarks.Text;
                                                        objGRNMaster.TRANCODE = lblTranCode.Text;

                                                        DataTable dtGRNAPI = new DataTable();
                                                        dtGRNAPI = objMainClass.GetWAData("CREATEGRN", 1, "GETWADATA");

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

                                                            for (int podet = 0; podet < objPBDetails.Count; podet++)
                                                            {
                                                                objPBDetails[podet].PONO = hfPONo.Value;
                                                            }

                                                            for (int podet = 0; podet < objPBDetails.Count; podet++)
                                                            {
                                                                objPBDetails[podet].MIRNO = hfGRNNo.Value;
                                                            }

                                                            PBMaster objPBMaster = new PBMaster();
                                                            objPBMaster.ADJAMT = 0;
                                                            objPBMaster.BILLAMT = Convert.ToDecimal(lblBillAmount.Text);
                                                            objPBMaster.BILLDT = Convert.ToDateTime(lblBillDate.Text);
                                                            objPBMaster.BILLNO = BILLNO;
                                                            objPBMaster.CMPID = objMainClass.intCmpId;
                                                            objPBMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                            objPBMaster.DISCOUNT = totaldiscountamount;
                                                            objPBMaster.NETMATVALUE = totalbaseamount;
                                                            objPBMaster.NETPBAMT = totalbaseamount + totaltaxamount - totaldiscountamount;
                                                            objPBMaster.NETTAXAMT = totaltaxamount;
                                                            objPBMaster.PAIDAMT = 0;
                                                            objPBMaster.PBCharge = new List<PBCharges>();
                                                            objPBMaster.PBDetail = objPBDetails;
                                                            objPBMaster.PBDT = DateTime.Now;
                                                            objPBMaster.PBNO = "";
                                                            objPBMaster.PBTax = objPBTaxation;
                                                            objPBMaster.PBTYPE = "MPB";
                                                            objPBMaster.PENDINGAMT = totalbaseamount + totaltaxamount - totaldiscountamount;
                                                            objPBMaster.PMTTERMS = payment;
                                                            objPBMaster.PMTTERMSDESC = paymentdesc;
                                                            objPBMaster.REMARK = lblRemarks.Text;
                                                            objPBMaster.STATUS = 1;
                                                            objPBMaster.VENDCODE = lblVendorCode.Text;

                                                            DataTable dtPBAPI = new DataTable();
                                                            dtPBAPI = objMainClass.GetWAData("INSERTPB", 1, "GETWADATA");

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

                                                                dtPurchase.Rows.Add(BILLNO, hfPRNo.Value, hfPONo.Value, hfGRNNo.Value, hfPBNo.Value, "All purchase documents successfully created for this Bill No.!");
                                                                isucess++;
                                                                JobStatusEntry();
                                                            }
                                                            else
                                                            {
                                                                dtPurchase.Rows.Add(BILLNO, hfPRNo.Value, hfPONo.Value, hfGRNNo.Value, "", "Something went wrong while creating PB for this Bill No.!");
                                                                ierror++;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            dtPurchase.Rows.Add(BILLNO, hfPRNo.Value, hfPONo.Value, "", "", "Something went wrong while creating GRN for this Bill No.!");
                                                            ierror++;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        dtPurchase.Rows.Add(lblBillNo.Text, hfPRNo.Value, hfPONo.Value, hfGRNNo.Value, hfPBNo.Value, "PR/PO successfully created.!");
                                                        isucess++;
                                                        SendMail(Convert.ToInt32(depart), Convert.ToString(objPRDetails[0].PLANTCD), objPOMaster.NETPOAMT);
                                                        JobStatusEntry();
                                                    }
                                                }
                                                else
                                                {
                                                    dtPurchase.Rows.Add(BILLNO, hfPRNo.Value, "", "", "", "Something went wrong while creating PO for this Bill No.!");
                                                    ierror++;
                                                }
                                            }
                                            else
                                            {
                                                dtPurchase.Rows.Add(BILLNO, "", "", "", "", "Something went wrong while creating PR for this Bill No.!");
                                                ierror++;
                                            }
                                        }
                                        //else
                                        //{
                                        //    dtPurchase.Rows.Add(BILLNO, "", "", "", "", "Line item not added for this Bill No.!");
                                        //    ierror++;
                                        //}
                                    }
                                    //else
                                    //{
                                    //    dtPurchase.Rows.Add(BILLNO, "", "", "", "", "Line item not added for this Bill No.!");
                                    //    ierror++;
                                    //}
                                }
                            }
                            BILLNO = lblBillNo.Text;

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
            finally
            {
                Session["dtPurchase"] = dtPurchase;
                string MESSAGE = "Please check downloaded excel for view result.!l";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Sucess : " + MESSAGE + "\");$('.close').click(function(){window.location.href ='frmCreatePOBulk.aspx' });", true);

                string path = "frmPurchaseDownload.aspx";
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "');", true);

            }
        }

        protected static string fileread()
        {
            string actualfol;
            actualfol = HttpContext.Current.Server.MapPath("~/PRMailBody.html");
            StreamReader sr = new StreamReader(actualfol);
            string strcontent = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            return strcontent;
        }

        public void JobStatusEntry()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    foreach (GridViewRow row in gvData.Rows)
                    {
                        Label lblItemCode = row.FindControl("lblItemCode") as Label;
                        Label lblPlant = row.FindControl("lblPlant") as Label;
                        Label lblLocation = row.FindControl("lblLocation") as Label;
                        Label lblJobID = row.FindControl("lblJobID") as Label;

                        string JOBID = objMainClass.strConvertZeroPadding(lblJobID.Text);
                        string LOCATION = lblLocation.Text;
                        string PLANTCD = lblPlant.Text;
                        string ITEMCODE = lblItemCode.Text;

                        DataTable dtItem = new DataTable();
                        dtItem = objMainClass.SelectItem("", "", ITEMCODE, "", "", "", "");

                        DataTable dtJobStageId = new DataTable();
                        dtJobStageId = objMainClass.GetJobIDStageDtl(objMainClass.intCmpId, JOBID);
                        if (dtJobStageId.Rows.Count > 0)
                        {
                            int currStage = Convert.ToInt32(dtJobStageId.Rows[0]["STAGEID"]);
                            string segment = Convert.ToString(dtJobStageId.Rows[0]["SEGMENT"]);

                            DataTable dtSegmentDtl = new DataTable();
                            dtSegmentDtl = objMainClass.GetSegmentDtl(objMainClass.intCmpId, segment);
                            if (Convert.ToInt32(dtSegmentDtl.Rows[0]["POAUTOENTRY"]) == 1)
                            {
                                int maxstageautoentry = Convert.ToInt32(dtSegmentDtl.Rows[0]["MAXSTAGEAUTOENTRY"]);

                                DataTable dtStageSeq = new DataTable();
                                dtStageSeq = objMainClass.GetSegmentStageData(currStage, segment, "GETSTAGEREQBYSTAGEID");
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
                                                if (Convert.ToInt32(dtStageSeq.Rows[s]["STAGEID"]) <= maxstageautoentry)
                                                {
                                                    int JOBSTAGEID = Convert.ToInt32(dtStageSeq.Rows[s]["STAGEID"]);
                                                    int JOBSTATUSID = objMainClass.GetStatusByStageID(JOBSTAGEID);
                                                    string NEWJOBID = objMainClass.strConvertZeroPadding(JOBID);
                                                    string NEWJCNO = "";
                                                    string lblProductID = "";
                                                    string MODEL = Convert.ToString(dtItem.Rows[0]["MODEL"]);
                                                    DataTable dtProdItem = new DataTable();
                                                    dtProdItem = objMainClass.GetProditembyModel(objMainClass.intCmpId, MODEL);
                                                    if (dtProdItem.Rows.Count > 0)
                                                    {
                                                        lblProductID = Convert.ToString(dtProdItem.Rows[0]["PRODITEMID"]);
                                                    }
                                                    else
                                                    {
                                                        lblProductID = "1";
                                                    }



                                                    string URLStage = Convert.ToString(dtInsertStage.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertStage.Rows[0]["TOKEN"]);
                                                    URLStage = URLStage + "?DOCNO=" + NEWJOBID + "&DOCTYPE=JS&STAGEID=" + JOBSTAGEID + "&STATRES=AUTO ENTRY JOB CREATION&CREATBY=" + Convert.ToInt32(Session["USERID"]);
                                                    var clientStage = new RestClient(URLStage);
                                                    clientStage.Timeout = -1;
                                                    var requestStage = new RestRequest(Method.POST);
                                                    requestStage.AddHeader("" + Convert.ToString(dtInsertStage.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertStage.Rows[0]["KEYVALUE"]) + "");
                                                    requestStage.AddHeader("Content-Type", "application/json");
                                                    IRestResponse responseStage = clientStage.Execute(requestStage);

                                                    string URLStatus = Convert.ToString(dtJobStatus.Rows[0]["OTHER"]) + "" + Convert.ToString(dtJobStatus.Rows[0]["TOKEN"]);
                                                    URLStatus = URLStatus + "?CMPID=" + objMainClass.intCmpId + "&JOBID=" + NEWJOBID + "&STAGEID=" + JOBSTAGEID + "&JOBSTATUS=" + JOBSTATUSID + "&STATRES=AUTO ENTRY JOB CREATION&STATUPDATEDT=" + DateTime.Now.ToString() + "&UPDATEDATE=" + DateTime.Now.ToString() + "&CREATEBY=" + Convert.ToInt32(Session["USERID"]);
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
                                                            objJobCardMaster.ITEMID = Convert.ToInt32(lblProductID);
                                                            objJobCardMaster.JCDT = Convert.ToDateTime(DateTime.Now).ToString();
                                                            objJobCardMaster.JCNO = "";
                                                            objJobCardMaster.JOBID = NEWJOBID;
                                                            objJobCardMaster.JOBIDSRNO = 1;
                                                            objJobCardMaster.JOBSTATUS = (int)STATUS.JCSaved;
                                                            objJobCardMaster.LOCCD = LOCATION;
                                                            objJobCardMaster.PLANTCD = PLANTCD;
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

                                                            #region Code for Production entry...

                                                            if (Convert.ToInt32(dtSegmentDtl.Rows[0]["JCAUTOENTRY"]) == 1)
                                                            {
                                                                if (NEWJCNO != null && NEWJCNO != "" && NEWJCNO != string.Empty)
                                                                {
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
                                                                        objJobCardDetails3.NOTE = "OK FOR CHECK";

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
                                                            }
                                                            //else
                                                            //{
                                                            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Job Card Not Created. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                                                            //}
                                                            #endregion

                                                        }
                                                        //else
                                                        //{
                                                        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Job Card Not Created. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                                                        //}
                                                    }

                                                }
                                            }
                                        }

                                    }

                                }
                            }
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

        public void SendMail(int DeptID, string PlantCD, decimal totalwithtaxamt)
        {
            #region Send Mail Code...
            String strCustContent = "";
            strCustContent = fileread();
            strCustContent = strCustContent.Replace("###Heading###", "New PO Created by User.");
            strCustContent = strCustContent.Replace("###CreateBy###", Convert.ToString(Session["USERNAME"]));
            strCustContent = strCustContent.Replace("###CreateDate###", DateTime.Now.ToShortDateString());
            strCustContent = strCustContent.Replace("###PRNO###", hfPONo.Value);
            strCustContent = strCustContent.Replace("###Message###", "New PO created by user. Details are as per above.");
            strCustContent = strCustContent.Replace("###LINK###", "http://14.98.132.190:360/MM/frmApprovePO.aspx");
            strCustContent = strCustContent.Replace("###HREFLINK###", "http://14.98.132.190:360/MM/frmApprovePO.aspx");

            DataTable dt = new DataTable();
            dt = objMainClass.MailSenderReceiver("PO", 1, DeptID, PlantCD, 12, totalwithtaxamt);
            string Reciever = string.Empty;
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    if (Reciever == string.Empty)
                    {
                        Reciever = Convert.ToString(dt.Rows[i]["EMAILID"]);
                    }
                    else
                    {
                        Reciever = Reciever + ";" + Convert.ToString(dt.Rows[i]["EMAILID"]);
                    }
                }
                objMainClass.SaveMailNotification(objMainClass.EmailID, objMainClass.Password, Convert.ToString(dt.Rows[0]["EMAILID"]), Reciever, "New PO Created", strCustContent, objMainClass.PORT, hfPONo.Value, Convert.ToString(Session["USERID"]), "MPO");


                //objMainClass.SendMail(strCustContent, "New PR Created", dt);
            }
            #endregion
        }

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    string filePath = Server.MapPath("~/MM/BulkPOTemplet.xlsx");
                    FileInfo file = new FileInfo(filePath);
                    if (file.Exists)
                    {
                        Response.Clear();
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                        Response.AddHeader("Content-Length", file.Length.ToString());
                        Response.ContentType = "text/plain";
                        Response.Flush();
                        Response.TransmitFile(file.FullName);
                        Response.End();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('File not found for Download.!');", true);
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