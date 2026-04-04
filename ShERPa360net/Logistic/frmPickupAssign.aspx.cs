using Newtonsoft.Json;
using RestSharp;
using ShERPa360net.Class;
using ShERPa360net.LivePincode;
using ShERPa360net.LiveWayBillGeneration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Windows.Forms;

namespace ShERPa360net.Logistic
{
    public partial class frmPickupAssign : System.Web.UI.Page
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

                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");
                        //txtFromDate.Text = "01-01-2022";//objMainClass.indianTime.Date.ToString("dd-MM-yyyy");
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");
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
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetLogisticData(objMainClass.intCmpId, "SIT", txtFromDate.Text, txtToDate.Text, txtSINo.Text, 0, "", "", 0, "PENDINGSI");

                    if (dt.Rows.Count > 0)
                    {
                        gvList.DataSource = dt;
                        gvList.DataBind();
                    }
                    else
                    {
                        gvList.DataSource = string.Empty;
                        gvList.DataBind();
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

        protected void lnkSerch_Click(object sender, EventArgs e)
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnGenerateWayBill_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((Button)sender).NamingContainer;
                    string sino = grdrow.Cells[0].Text;
                    int sisrno = Convert.ToInt32(grdrow.Cells[1].Text);
                    string jobid = grdrow.Cells[3].Text;
                    string plant = grdrow.Cells[6].Text;
                    string sono = grdrow.Cells[15].Text;
                    string entityid = grdrow.Cells[16].Text;

                    DataTable dtSIData = new DataTable();
                    dtSIData = objMainClass.GetLogisticData(objMainClass.intCmpId, "", "", "", sino, sisrno, "", "", 0, "GETGENERATEDATA");

                    if (dtSIData.Rows.Count > 0)
                    {
                        lblSenderName.Text = Convert.ToString(dtSIData.Rows[0]["COMPANYNAME"]);
                        lblSenderAdd1.Text = Convert.ToString(dtSIData.Rows[0]["ADDR1"]);
                        lblSenderAdd2.Text = Convert.ToString(dtSIData.Rows[0]["ADDR2"]);
                        lblSenderAdd3.Text = Convert.ToString(dtSIData.Rows[0]["ADDR3"]);
                        lblSenderPincode.Text = Convert.ToString(dtSIData.Rows[0]["POSTALCODE"]);
                        lblSenderMobile.Text = Convert.ToString(dtSIData.Rows[0]["CONTACTNO"]);
                        lblSenderTelephone.Text = Convert.ToString(dtSIData.Rows[0]["CONTACTNO"]);
                        lblSenderEmail.Text = Convert.ToString(dtSIData.Rows[0]["EMAILID"]);

                        lblConsiName.Text = Convert.ToString(dtSIData.Rows[0]["RETAILCUSTNAME"]);

                        if (Convert.ToString(dtSIData.Rows[0]["CUSTADD"]).Length > 60)
                        {
                            lblConsiAddr1.Text = Convert.ToString(dtSIData.Rows[0]["CUSTADD"]).Substring(0, 30);
                            lblConsiAddr2.Text = Convert.ToString(dtSIData.Rows[0]["CUSTADD"]).Substring(30, 30);
                            lblConsiAddr3.Text = Convert.ToString(dtSIData.Rows[0]["CUSTADD"]).Substring(60);
                        }
                        else if (Convert.ToString(dtSIData.Rows[0]["CUSTADD"]).Length > 30)
                        {
                            lblConsiAddr1.Text = Convert.ToString(dtSIData.Rows[0]["CUSTADD"]).Substring(0, 30);
                            lblConsiAddr2.Text = Convert.ToString(dtSIData.Rows[0]["CUSTADD"]).Substring(30);
                            lblConsiAddr3.Text = Convert.ToString(dtSIData.Rows[0]["CITY"]);
                        }
                        else
                        {
                            lblConsiAddr1.Text = Convert.ToString(dtSIData.Rows[0]["CUSTADD"]);
                            lblConsiAddr2.Text = Convert.ToString(dtSIData.Rows[0]["CITY"]);
                            lblConsiAddr3.Text = Convert.ToString(dtSIData.Rows[0]["CITY"]);
                        }


                        //lblConsiAddr1.Text = Convert.ToString(dtSIData.Rows[0]["CUSTADD"]).Substring(0, 30);
                        //lblConsiAddr2.Text = Convert.ToString(dtSIData.Rows[0]["CUSTADD"]).Substring(30, 30);
                        //lblConsiAddr3.Text = Convert.ToString(dtSIData.Rows[0]["CUSTADD"]).Substring(61);
                        lblConsiPincode.Text = Convert.ToString(dtSIData.Rows[0]["PINCODE"]);
                        lblConsiMobile.Text = Convert.ToString(dtSIData.Rows[0]["CUSTMOBILENO"]);
                        lblConsiTelephone.Text = Convert.ToString(dtSIData.Rows[0]["CUSTMOBILENO"]);
                        lblConsiAttention.Text = "Mobile Device";

                        lblQty.Text = Convert.ToString(dtSIData.Rows[0]["QTY"]);
                        lblProductCode.Text = "A";
                        if (Convert.ToString(dtSIData.Rows[0]["PAYMODE"]) == "1")
                        {
                            lblSubProdCode.Text = "C";
                            lblDeclareValue.Text = Convert.ToString(dtSIData.Rows[0]["CAMOUNT"]);
                            lblCollectableAmt.Text = Convert.ToString(Convert.ToDecimal(dtSIData.Rows[0]["CAMOUNT"]));// - Convert.ToDecimal(dtSIData.Rows[0]["DISCAMT"]));
                        }
                        else if (Convert.ToString(dtSIData.Rows[0]["PAYMODE"]) == "14")
                        {
                            lblSubProdCode.Text = "C";
                            lblDeclareValue.Text = Convert.ToString(dtSIData.Rows[0]["CAMOUNT"]);
                            lblCollectableAmt.Text = Convert.ToString(Convert.ToDecimal(dtSIData.Rows[0]["CAMOUNT"]) - Convert.ToDecimal(dtSIData.Rows[0]["PREPAIDAMT"]));
                        }
                        else
                        {
                            lblSubProdCode.Text = "P";
                            lblDeclareValue.Text = Convert.ToString(dtSIData.Rows[0]["CAMOUNT"]);
                            lblCollectableAmt.Text = "0";
                        }
                        lblWeight.Text = "0.5";
                        lblInvoiceno.Text = sino;
                        lblSpecialInstruction.Text = "Mobile Device";
                        lblPackType.Text = "A";
                        lblCreditRefNo.Text = Convert.ToString(dtSIData.Rows[0]["JOBID"]) + "" + sino + "" + DateTime.Now.Second;
                        lblCmdtDetail1.Text = Convert.ToString(dtSIData.Rows[0]["ITEMGRP"]);

                        hfOriginArea.Value = Convert.ToString(dtSIData.Rows[0]["ORIGINAREA"]);
                        hfCustCode.Value = Convert.ToString(dtSIData.Rows[0]["CUSTOMERCODE"]);
                        hfPlant.Value = plant;
                        hfSINO.Value = sino;
                        hfjobID.Value = jobid;
                        hfSONO.Value = sono;
                        hfEntityID.Value = entityid;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Data not found for this SI no.!');", true);
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

        public void DownloadWayBillFile()
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Session["AWBNo"].ToString() + ".pdf");
                Response.BinaryWrite(((byte[])Session["AWBPrintContent"]));
                Response.Flush();
                Response.SuppressContent = true; // avoid the "Thread was being aborted" exception
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                Session["AWBNo"] = null;
                Session["AWBPrintContent"] = null;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }

        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                if (Session["USERID"] != null)
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };

                    DataTable dtAPIKEYPIN = new DataTable();
                    dtAPIKEYPIN = objMainClass.GetLogisticData(objMainClass.intCmpId, "", "", "", "", 0, "BLUDARTAPIPIN", hfPlant.Value, 1, "GETAPIKEY");

                    if (dtAPIKEYPIN.Rows.Count > 0)
                    {
                        ProfileClass objProfileClass = new ProfileClass();
                        objProfileClass.Api_type = Convert.ToString(dtAPIKEYPIN.Rows[0]["APITYPE"]);
                        objProfileClass.LicenceKey = Convert.ToString(dtAPIKEYPIN.Rows[0]["KEYVALUE"]);
                        objProfileClass.LoginID = Convert.ToString(dtAPIKEYPIN.Rows[0]["KEYNAME"]);

                        PinCodeCheck objPinCodeCheck = new PinCodeCheck();
                        objPinCodeCheck.pinCode = lblConsiPincode.Text;
                        objPinCodeCheck.profile = objProfileClass;
                        var jsonInput = JsonConvert.SerializeObject(objPinCodeCheck);

                        var client = new RestClient(Convert.ToString(dtAPIKEYPIN.Rows[0]["OTHER"]));
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("JWTToken", Convert.ToString(dtAPIKEYPIN.Rows[0]["JWTTOKEN"]));
                        request.AddParameter("application/json", jsonInput, ParameterType.RequestBody);
                        IRestResponse response = client.Execute(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            PinCodeResponse pbjPinCodeResponse = new PinCodeResponse();
                            string jsonconn = response.Content;
                            pbjPinCodeResponse = JsonConvert.DeserializeObject<PinCodeResponse>(jsonconn);

                            if (pbjPinCodeResponse.GetServicesforPincodeResult.IsError == false)
                            {
                                DataTable dtAPIKEYWB = new DataTable();
                                dtAPIKEYWB = objMainClass.GetLogisticData(objMainClass.intCmpId, "", "", "", "", 0, "BLUDARTAPIWBGEN", hfPlant.Value, 1, "GETAPIKEY");
                                if (dtAPIKEYWB.Rows.Count > 0)
                                {


                                    #region Consignee Data...
                                    ConsigneeNew objConsignee = new ConsigneeNew();
                                    objConsignee.AvailableDays = "";
                                    objConsignee.AvailableTiming = "";
                                    objConsignee.ConsigneeAddress1 = lblConsiAddr1.Text;
                                    objConsignee.ConsigneeAddress2 = lblConsiAddr2.Text;
                                    objConsignee.ConsigneeAddress3 = lblConsiAddr3.Text;
                                    objConsignee.ConsigneeAddressType = "R";
                                    objConsignee.ConsigneeAddressinfo = "";
                                    objConsignee.ConsigneeAttention = lblConsiName.Text;
                                    objConsignee.ConsigneeEmailID = "";
                                    objConsignee.ConsigneeFullAddress = "";
                                    objConsignee.ConsigneeGSTNumber = "";
                                    objConsignee.ConsigneeLatitude = "";
                                    objConsignee.ConsigneeLongitude = "";
                                    objConsignee.ConsigneeMaskedContactNumber = "";
                                    objConsignee.ConsigneeMobile = lblConsiMobile.Text;
                                    objConsignee.ConsigneeName = lblConsiName.Text;
                                    objConsignee.ConsigneePincode = lblConsiPincode.Text;
                                    objConsignee.ConsigneeTelephone = lblConsiTelephone.Text;
                                    #endregion

                                    #region RTO Data...
                                    Returnadds objReturnadds = new Returnadds();
                                    objReturnadds.ManifestNumber = "";
                                    objReturnadds.ReturnAddress1 = lblSenderAdd1.Text;
                                    objReturnadds.ReturnAddress2 = lblSenderAdd2.Text;
                                    objReturnadds.ReturnAddress3 = lblSenderAdd3.Text;
                                    objReturnadds.ReturnAddressinfo = "";
                                    objReturnadds.ReturnContact = lblSenderName.Text;
                                    objReturnadds.ReturnEmailID = lblSenderEmail.Text;
                                    objReturnadds.ReturnLatitude = "";
                                    objReturnadds.ReturnLongitude = "";
                                    objReturnadds.ReturnMaskedContactNumber = "";
                                    objReturnadds.ReturnMobile = lblSenderMobile.Text;
                                    objReturnadds.ReturnPincode = lblSenderPincode.Text;
                                    objReturnadds.ReturnTelephone = lblSenderTelephone.Text;
                                    #endregion

                                    #region Commodity Data...
                                    Commodity objCommodity = new Commodity();
                                    objCommodity.CommodityDetail1 = lblCmdtDetail1.Text;
                                    objCommodity.CommodityDetail2 = "";
                                    objCommodity.CommodityDetail3 = "";
                                    #endregion

                                    #region Dimension Data...
                                    List<DimensionNew> objDimension = new List<DimensionNew>();
                                    objDimension.Add(new DimensionNew
                                    {
                                        Breadth = 10,
                                        Count = 1,
                                        Height = 5,
                                        Length = 20
                                    });
                                    #endregion

                                    #region Item Data...
                                    List<Itemdtl> objItemdtl = new List<Itemdtl>();
                                    objItemdtl.Add(new Itemdtl
                                    {
                                        CGSTAmount = 0,
                                        HSCode = "",
                                        IGSTAmount = 0,
                                        IGSTRate = 0,
                                        Instruction = lblSpecialInstruction.Text,
                                        InvoiceDate = DateTime.Now,
                                        InvoiceNumber = lblInvoiceno.Text,
                                        ItemID = "",
                                        ItemName = "Mobile Device",
                                        ItemValue = Convert.ToDouble(lblDeclareValue.Text),
                                        Itemquantity = 1,
                                        PlaceofSupply = "",
                                        ProductDesc1 = "",
                                        ProductDesc2 = "",
                                        ReturnReason = "",
                                        SGSTAmount = 0,
                                        SKUNumber = "",
                                        SellerGSTNNumber = "",
                                        SellerName = "",
                                        TaxableAmount = 0,
                                        TotalValue = Convert.ToDouble(lblDeclareValue.Text),
                                        cessAmount = 0,
                                        countryOfOrigin = "IN",
                                        docType = "INV",
                                        subSupplyType = 1,
                                        supplyType = "",
                                    });
                                    #endregion

                                    #region Service Data...
                                    ServicesNew objServices = new ServicesNew();
                                    objServices.AWBNo = "";
                                    objServices.ActualWeight = "0.50";
                                    if (lblSubProdCode.Text == "C")
                                    {
                                        objServices.CollectableAmount = Convert.ToDouble(lblCollectableAmt.Text);
                                    }
                                    else
                                    {
                                        objServices.CollectableAmount = 0;
                                    }
                                    objServices.Commodity = objCommodity;
                                    objServices.CreditReferenceNo = lblCreditRefNo.Text;
                                    objServices.CreditReferenceNo2 = "";
                                    objServices.CreditReferenceNo3 = "";
                                    objServices.CurrencyCode = "";
                                    objServices.DeclaredValue = Convert.ToDouble(lblDeclareValue.Text);
                                    objServices.DeliveryTimeSlot = "";
                                    objServices.Dimensions = objDimension;
                                    objServices.FavouringName = "";
                                    objServices.ForwardAWBNo = "";
                                    objServices.ForwardLogisticCompName = "";
                                    objServices.InsurancePaidBy = "";
                                    objServices.InvoiceNo = lblInvoiceno.Text;
                                    objServices.IsChequeDD = "";
                                    objServices.IsDedicatedDeliveryNetwork = false;
                                    objServices.IsForcePickup = false;
                                    objServices.IsPartialPickup = false;
                                    objServices.IsReversePickup = chkIsRVP.Checked ? true : false;
                                    objServices.ItemCount = 1;
                                    objServices.OTPBasedDelivery = "0";
                                    objServices.OTPCode = "";
                                    objServices.Officecutofftime = "";
                                    objServices.PDFOutputNotRequired = rblWaybillPDF.SelectedValue == "1" ? false : true;
                                    //objServices.PackType = lblPackType.Text;
                                    objServices.ParcelShopCode = "";
                                    objServices.PayableAt = "";

                                    TimeSpan timespan = new TimeSpan(15, 00, 00);
                                    DateTime time = DateTime.Today.Add(timespan);
                                    string PTime = time.ToString("hh:mm");
                                    if (Convert.ToInt32(DateTime.Now.ToString("hh")) <= Convert.ToInt32(time.ToString("hh")))
                                    {
                                        objServices.PickupDate = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("dd-MM-yyyy"));
                                    }
                                    else
                                    {
                                        objServices.PickupDate = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"));
                                    }
                                    objServices.PickupMode = "";
                                    objServices.PickupTime = "1500";
                                    objServices.PickupType = "";
                                    objServices.PieceCount = "1";
                                    objServices.PreferredPickupTimeSlot = "";
                                    objServices.ProductCode = lblProductCode.Text;
                                    objServices.ProductFeature = "";
                                    objServices.ProductType = rblProductType.SelectedValue == "0" ? (int)LiveWayBillGeneration.ProductType.Docs : (int)LiveWayBillGeneration.ProductType.Dutiables;
                                    objServices.RegisterPickup = chkPickup.Checked ? true : false;
                                    objServices.SpecialInstruction = lblSpecialInstruction.Text;
                                    objServices.SubProductCode = lblSubProdCode.Text;
                                    //objServices.TotalCashPaytoCustomer = 0;
                                    objServices.itemdtl = objItemdtl;
                                    objServices.noOfDCGiven = 0;
                                    #endregion

                                    #region Shipper Data...
                                    ShipperNew objShipper = new ShipperNew();
                                    objShipper.CustomerAddress1 = lblSenderAdd1.Text;
                                    objShipper.CustomerAddress2 = lblSenderAdd2.Text;
                                    objShipper.CustomerAddress3 = lblSenderAdd3.Text;
                                    objShipper.CustomerAddressinfo = "";
                                    objShipper.CustomerCode = hfCustCode.Value;
                                    objShipper.CustomerEmailID = lblSenderEmail.Text;
                                    objShipper.CustomerGSTNumber = "";
                                    objShipper.CustomerLatitude = "";
                                    objShipper.CustomerLongitude = "";
                                    objShipper.CustomerMaskedContactNumber = "";
                                    objShipper.CustomerMobile = lblSenderMobile.Text;
                                    objShipper.CustomerName = lblSenderName.Text;
                                    objShipper.CustomerPincode = lblSenderPincode.Text;
                                    objShipper.CustomerTelephone = lblSenderTelephone.Text;
                                    //objShipper.IsToPayCustomer = false;
                                    objShipper.OriginArea = hfOriginArea.Value;
                                    objShipper.Sender = lblSenderName.Text;
                                    objShipper.VendorCode = "1";
                                    #endregion

                                    #region Main Request Data...
                                    Request objRequest = new Request();
                                    objRequest.Consignee = objConsignee;
                                    objRequest.Returnadds = objReturnadds;
                                    objRequest.Services = objServices;
                                    objRequest.Shipper = objShipper;
                                    #endregion

                                    WayBillGenClass objWayBillGenClass = new WayBillGenClass();
                                    objWayBillGenClass.Profile = objProfileClass;
                                    objWayBillGenClass.Request = objRequest;

                                    var json = new JavaScriptSerializer().Serialize(objWayBillGenClass);

                                    var clientNew = new RestClient(Convert.ToString(dtAPIKEYWB.Rows[0]["OTHER"]));
                                    var requestNew = new RestRequest(Method.POST);
                                    requestNew.AddHeader("content-type", "application/json");
                                    requestNew.AddHeader("JWTToken", Convert.ToString(dtAPIKEYWB.Rows[0]["JWTTOKEN"]));
                                    requestNew.AddParameter("application/json", json, ParameterType.RequestBody);
                                    IRestResponse responseNew = clientNew.Execute(requestNew);

                                    WayBillResponse objWayBillResponse = new WayBillResponse();
                                    string waybillconn = responseNew.Content;
                                    objWayBillResponse = JsonConvert.DeserializeObject<WayBillResponse>(waybillconn);

                                    if (responseNew.StatusCode == System.Net.HttpStatusCode.OK)
                                    {

                                        if (objWayBillResponse.GenerateWayBillResult.IsError == false)
                                        {
                                            string waybill = objWayBillResponse.GenerateWayBillResult.AWBNo;
                                            int iResult = objMainClass.UpdateLogisticJob(objMainClass.intCmpId, hfjobID.Value, waybill, "", "BLUEDART", Convert.ToInt32(Session["USERID"]), "UPDATEJOBID");
                                            if (iResult == 1)
                                            {
                                                Session["AWBNo"] = waybill;
                                                Session["AWBPrintContent"] = objWayBillResponse.GenerateWayBillResult.AWBPrintContent;

                                                if (hfEntityID.Value != "" && hfEntityID.Value != string.Empty && hfEntityID.Value != null)
                                                {
                                                    int iWSResult = objMainClass.ChangeOrderStatus(objMainClass.intCmpId, hfEntityID.Value, hfSONO.Value, "", "DISPATCH", "WEBSITESTATUSAPI", "MOBEXSHIPAPI", "shipment created Successfully.", "Bluedart", "shipment created Successfully.", waybill, Convert.ToInt32(Session["USERID"]));
                                                }

                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Waybill created successfully. Waybill no. : " + waybill + ". Pickup registration status : " + objWayBillResponse.GenerateWayBillResult.Status[1].StatusCode.ToString() + "\");$('.close').click(function(){window.location.href ='frmPickupAssign.aspx' });", true);

                                                string path = "frmWayBillDownload.aspx";
                                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "');", true);
                                            }
                                            else
                                            {
                                                Session["AWBNo"] = waybill;
                                                Session["AWBPrintContent"] = objWayBillResponse.GenerateWayBillResult.AWBPrintContent;
                                                if (hfEntityID.Value != "" && hfEntityID.Value != string.Empty && hfEntityID.Value != null)
                                                {
                                                    int iWSResult = objMainClass.ChangeOrderStatus(objMainClass.intCmpId, hfEntityID.Value, hfSONO.Value, "", "DISPATCH", "WEBSITESTATUSAPI", "MOBEXSHIPAPI", "shipment created Successfully.", "Bluedart", "shipment created Successfully.", waybill, Convert.ToInt32(Session["USERID"]));
                                                }

                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Waybill created successfully. Waybill no. : " + waybill + ". Pickup registration status : " + objWayBillResponse.GenerateWayBillResult.Status[1].StatusCode.ToString() + "\");$('.close').click(function(){window.location.href ='frmPickupAssign.aspx' });", true);

                                                string path = "frmWayBillDownload.aspx";
                                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "');", true);
                                            }

                                        }
                                        else
                                        {
                                            string errormsg = Convert.ToString(objWayBillResponse.GenerateWayBillResult.Status[0].StatusInformation);
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"Waybill not generated. " + errormsg + "\");", true);
                                        }
                                    }
                                    else
                                    {
                                        string errormsg = Convert.ToString(objWayBillResponse.GenerateWayBillResult.Status[0].StatusInformation);
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"Waybill not generated. " + errormsg + "\");", true);
                                    }

                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Waybill Generation API not found. Please contact administrator.!');", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Customer Pincode not Servicable by Bluedart. Please contact administrator.!');", true);
                            }
                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {

                            DataTable dtAPIKEYJWT = new DataTable();
                            dtAPIKEYJWT = objMainClass.GetLogisticData(objMainClass.intCmpId, "", "", "", "", 0, "BLUDARTAPITOKENGEN", hfPlant.Value, 1, "GETAPIKEY");

                            if (dtAPIKEYJWT.Rows.Count > 0)
                            {
                                var clientKey = new RestClient(Convert.ToString(dtAPIKEYJWT.Rows[0]["OTHER"]));
                                var requestKey = new RestRequest(Method.GET);
                                requestKey.AddHeader("ClientID", Convert.ToString(dtAPIKEYJWT.Rows[0]["KEYVALUE"]));
                                requestKey.AddHeader("clientSecret", Convert.ToString(dtAPIKEYJWT.Rows[0]["KEYNAME"]));
                                IRestResponse responseKey = clientKey.Execute(requestKey);

                                if (responseKey.StatusCode == HttpStatusCode.OK)
                                {
                                    JWTTokenClass objJWTTokenClass = new JWTTokenClass();
                                    string jsonconnKey = responseKey.Content;
                                    objJWTTokenClass = JsonConvert.DeserializeObject<JWTTokenClass>(jsonconnKey);

                                    int i = objMainClass.UpdateAppkey(objJWTTokenClass.JWTToken, "UPDATEJWTTOKEN");

                                    if (i == 1)
                                    {
                                        btnGenerate_Click(1, e);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('JWT Token Updation Error. Please contact administrator.!');", true);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('JWT Token Generation Error. Please contact administrator.!');", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('JWT Token Expired. Please contact administrator.!');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Customer Pincode not Servicable by Bluedart. Please contact administrator.!');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Pincode API key not found. Please contact administrator.!');", true);
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

        #region Commented Code...

        //protected void btnGenerate_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Session["USERID"] != null)
        //        {
        //            DataTable dtAPIKEY = new DataTable();
        //            dtAPIKEY = objMainClass.GetLogisticData(objMainClass.intCmpId, "", "", "", "", 0, "BLUDARTAPI", hfPlant.Value, 1, "GETAPIKEY");

        //            if (dtAPIKEY.Rows.Count > 0)
        //            {
        //                //LivePincode.UserProfile objPincodeUserProfile = new LivePincode.UserProfile();
        //                //objPincodeUserProfile.Api_type = Convert.ToString(dtAPIKEY.Rows[0]["APITYPE"]);
        //                //objPincodeUserProfile.Area = "BLR";//Convert.ToString(dtAPIKEY.Rows[0]["AREA"]);
        //                //objPincodeUserProfile.Customercode = "185113";// Convert.ToString(dtAPIKEY.Rows[0]["UNIQUECODE"]);
        //                //objPincodeUserProfile.LicenceKey = "nsgolko3stjunoqhuhmupqmuqophqkqh";// Convert.ToString(dtAPIKEY.Rows[0]["KEYVALUE"]);
        //                //objPincodeUserProfile.LoginID = "BLR91677";// Convert.ToString(dtAPIKEY.Rows[0]["KEYNAME"]);
        //                //objPincodeUserProfile.Version = Convert.ToString(dtAPIKEY.Rows[0]["VERSION"]);

        //                //LiveWayBillGeneration.UserProfile objWayBillUserProfile = new LiveWayBillGeneration.UserProfile();
        //                //objWayBillUserProfile.Api_type = Convert.ToString(dtAPIKEY.Rows[0]["APITYPE"]);
        //                //objWayBillUserProfile.Area = "BLR";//Convert.ToString(dtAPIKEY.Rows[0]["AREA"]);
        //                //objWayBillUserProfile.Customercode = "185113";// Convert.ToString(dtAPIKEY.Rows[0]["UNIQUECODE"]);
        //                //objWayBillUserProfile.LicenceKey = "nsgolko3stjunoqhuhmupqmuqophqkqh";// Convert.ToString(dtAPIKEY.Rows[0]["KEYVALUE"]);
        //                //objWayBillUserProfile.LoginID = "BLR91677";// Convert.ToString(dtAPIKEY.Rows[0]["KEYNAME"]);
        //                //objWayBillUserProfile.Version = Convert.ToString(dtAPIKEY.Rows[0]["VERSION"]);


        //                LivePincode.UserProfile objPincodeUserProfile = new LivePincode.UserProfile();
        //                objPincodeUserProfile.Api_type = Convert.ToString(dtAPIKEY.Rows[0]["APITYPE"]);
        //                objPincodeUserProfile.Area = Convert.ToString(dtAPIKEY.Rows[0]["AREA"]);
        //                objPincodeUserProfile.Customercode = Convert.ToString(dtAPIKEY.Rows[0]["UNIQUECODE"]);
        //                objPincodeUserProfile.LicenceKey = Convert.ToString(dtAPIKEY.Rows[0]["KEYVALUE"]);
        //                objPincodeUserProfile.LoginID = Convert.ToString(dtAPIKEY.Rows[0]["KEYNAME"]);
        //                objPincodeUserProfile.Version = Convert.ToString(dtAPIKEY.Rows[0]["VERSION"]);

        //                LiveWayBillGeneration.UserProfile objWayBillUserProfile = new LiveWayBillGeneration.UserProfile();
        //                objWayBillUserProfile.Api_type = Convert.ToString(dtAPIKEY.Rows[0]["APITYPE"]);
        //                objWayBillUserProfile.Area = Convert.ToString(dtAPIKEY.Rows[0]["AREA"]);
        //                objWayBillUserProfile.Customercode = Convert.ToString(dtAPIKEY.Rows[0]["UNIQUECODE"]);
        //                objWayBillUserProfile.LicenceKey = Convert.ToString(dtAPIKEY.Rows[0]["KEYVALUE"]);
        //                objWayBillUserProfile.LoginID = Convert.ToString(dtAPIKEY.Rows[0]["KEYNAME"]);
        //                objWayBillUserProfile.Version = Convert.ToString(dtAPIKEY.Rows[0]["VERSION"]);




        //                LivePincode.ServiceFinderQueryClient PincodeSearch = new LivePincode.ServiceFinderQueryClient();
        //                LivePincode.ServiceCenterDetailsReference pincodeDetails = new LivePincode.ServiceCenterDetailsReference();

        //                pincodeDetails = PincodeSearch.GetServicesforPincode(lblConsiPincode.Text, objPincodeUserProfile);

        //                if (pincodeDetails.IsError == false)
        //                {
        //                    LiveWayBillGeneration.WayBillGenerationRequest objWayBillReq = new LiveWayBillGeneration.WayBillGenerationRequest();

        //                    #region Shipper Data...

        //                    //objWayBillReq.Shipper = new LiveWayBillGeneration.Shipper();
        //                    //objWayBillReq.Shipper.OriginArea = "BLR";// hfOriginArea.Value;
        //                    //objWayBillReq.Shipper.CustomerCode = "185113";// hfCustCode.Value;
        //                    //objWayBillReq.Shipper.CustomerName = lblSenderName.Text;
        //                    //objWayBillReq.Shipper.CustomerAddress1 = lblSenderAdd1.Text;
        //                    //objWayBillReq.Shipper.CustomerAddress2 = lblSenderAdd2.Text;
        //                    //objWayBillReq.Shipper.CustomerAddress3 = lblSenderAdd3.Text;
        //                    //objWayBillReq.Shipper.CustomerPincode = "560009"; //lblSenderPincode.Text;
        //                    //objWayBillReq.Shipper.CustomerTelephone = lblSenderTelephone.Text;
        //                    //objWayBillReq.Shipper.CustomerMobile = lblSenderMobile.Text;
        //                    //objWayBillReq.Shipper.CustomerEmailID = lblSenderEmail.Text;
        //                    //objWayBillReq.Shipper.Sender = lblSenderName.Text;
        //                    ////objWayBillReq.Shipper.IsToPayCustomer = false;
        //                    //objWayBillReq.Shipper.VendorCode = "1";




        //                    objWayBillReq.Shipper = new LiveWayBillGeneration.Shipper();
        //                    objWayBillReq.Shipper.OriginArea = hfOriginArea.Value;
        //                    objWayBillReq.Shipper.CustomerCode = hfCustCode.Value;
        //                    objWayBillReq.Shipper.CustomerName = lblSenderName.Text;
        //                    objWayBillReq.Shipper.CustomerAddress1 = lblSenderAdd1.Text;
        //                    objWayBillReq.Shipper.CustomerAddress2 = lblSenderAdd2.Text;
        //                    objWayBillReq.Shipper.CustomerAddress3 = lblSenderAdd3.Text;
        //                    objWayBillReq.Shipper.CustomerPincode = lblSenderPincode.Text;
        //                    objWayBillReq.Shipper.CustomerTelephone = lblSenderTelephone.Text;
        //                    objWayBillReq.Shipper.CustomerMobile = lblSenderMobile.Text;
        //                    objWayBillReq.Shipper.CustomerEmailID = lblSenderEmail.Text;
        //                    objWayBillReq.Shipper.Sender = lblSenderName.Text;
        //                    //objWayBillReq.Shipper.IsToPayCustomer = false;
        //                    objWayBillReq.Shipper.VendorCode = "1";

        //                    #endregion

        //                    #region Consignee Data...

        //                    objWayBillReq.Consignee = new LiveWayBillGeneration.Consignee();
        //                    objWayBillReq.Consignee.ConsigneeName = lblConsiName.Text;
        //                    objWayBillReq.Consignee.ConsigneeAddress1 = lblConsiAddr1.Text;
        //                    objWayBillReq.Consignee.ConsigneeAddress2 = lblConsiAddr2.Text;
        //                    objWayBillReq.Consignee.ConsigneeAddress3 = lblConsiAddr3.Text;
        //                    objWayBillReq.Consignee.ConsigneePincode = lblConsiPincode.Text;
        //                    objWayBillReq.Consignee.ConsigneeTelephone = lblConsiTelephone.Text;
        //                    objWayBillReq.Consignee.ConsigneeMobile = lblConsiMobile.Text;
        //                    objWayBillReq.Consignee.ConsigneeEmailID = "";
        //                    objWayBillReq.Consignee.ConsigneeAttention = lblConsiName.Text;

        //                    #endregion

        //                    #region Service Data...

        //                    objWayBillReq.Services = new LiveWayBillGeneration.Services();
        //                    //objWayBillReq.Services.ProductCode = "A";// lblProductCode.Text;
        //                    objWayBillReq.Services.ProductCode = lblProductCode.Text;
        //                    objWayBillReq.Services.ProductType = rblProductType.SelectedValue == "0" ? LiveWayBillGeneration.ProductType.Docs : LiveWayBillGeneration.ProductType.Dutiables;
        //                    objWayBillReq.Services.SubProductCode = lblSubProdCode.Text;
        //                    //if (lblQty.Text == "1")
        //                    //{
        //                    objWayBillReq.Services.PieceCount = 1;//Convert.ToInt16(lblQty.Text);
        //                                                          //}


        //                    objWayBillReq.Services.ActualWeight = 0.5;
        //                    //objWayBillReq.Services.PackType = lblPackType.Text;
        //                    objWayBillReq.Services.InvoiceNo = hfSINO.Value;
        //                    objWayBillReq.Services.SpecialInstruction = lblSpecialInstruction.Text;
        //                    objWayBillReq.Services.DeclaredValue = Convert.ToDouble(lblDeclareValue.Text);
        //                    if (lblSubProdCode.Text == "C")
        //                    {
        //                        objWayBillReq.Services.CollectableAmount = Convert.ToDouble(lblCollectableAmt.Text);
        //                    }
        //                    objWayBillReq.Services.CreditReferenceNo = lblCreditRefNo.Text;
        //                    //objWayBillReq.Services.PDFOutputNotRequired = rblWaybillPDF.SelectedValue == "1" ? false : true;
        //                    //objWayBillReq.Services.AWBNo = "";
        //                    objWayBillReq.Services.RegisterPickup = chkPickup.Checked ? true : false;
        //                    //objWayBillReq.Services.DeliveryTimeSlot = "";
        //                    //objWayBillReq.Services.IsReversePickup = chkIsRVP.Checked ? true : false;


        //                    LiveWayBillGeneration.Dimension[] d = new LiveWayBillGeneration.Dimension[1];
        //                    d[0] = new LiveWayBillGeneration.Dimension();
        //                    d[0].Breadth = 10;
        //                    d[0].Count = 1;
        //                    d[0].Height = 5;
        //                    d[0].Length = 20;
        //                    objWayBillReq.Services.Dimensions = d;

        //                    //LiveWayBillGeneration.CommodityDetail CD = new LiveWayBillGeneration.CommodityDetail();
        //                    //CD.CommodityDetail1 = lblCmdtDetail1.Text;
        //                    //objWayBillReq.Services.Commodity = CD;

        //                    TimeSpan timespan = new TimeSpan(15, 00, 00);
        //                    DateTime time = DateTime.Today.Add(timespan);
        //                    string PTime = time.ToString("hh:mm");
        //                    if (Convert.ToInt32(DateTime.Now.ToString("hh")) <= Convert.ToInt32(time.ToString("hh")))
        //                    {
        //                        objWayBillReq.Services.PickupDate = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("dd-MM-yyyy"));
        //                    }
        //                    else
        //                    {
        //                        objWayBillReq.Services.PickupDate = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"));

        //                    }
        //                    objWayBillReq.Services.PickupTime = "1500";

        //                    #endregion



        //                    LiveWayBillGeneration.WayBillGenerationResponse objWaybillResponse = new LiveWayBillGeneration.WayBillGenerationResponse();
        //                    LiveWayBillGeneration.WayBillGenerationClient objWaybillClient = new LiveWayBillGeneration.WayBillGenerationClient();

        //                    objWaybillResponse = objWaybillClient.GenerateWayBill(objWayBillReq, objWayBillUserProfile);
        //                    //objWaybillResponse.IsError = false;
        //                    if (objWaybillResponse.IsError == false)
        //                    {

        //                        int iResult =  objMainClass.UpdateLogisticJob(objMainClass.intCmpId, hfjobID.Value, objWaybillResponse.AWBNo, "", "BLUEDART", Convert.ToInt32(Session["USERID"]), "UPDATEJOBID");
        //                        if (iResult == 1)
        //                        {
        //                            //string strURL = @"../Bluedart/";
        //                            //if (!Directory.Exists(strURL))
        //                            //{
        //                            //    Directory.CreateDirectory(strURL);
        //                            //}
        //                            //File.WriteAllBytes(strURL + objWaybillResponse.AWBNo + ".pdf", objWaybillResponse.AWBPrintContent);



        //                            //byte[] byteArray = objWaybillResponse.AWBPrintContent; // Your byte array containing the data to be downloaded
        //                            //string filePath = @"C:\Bluedart\" + objWaybillResponse.AWBNo + ".pdf"; // Your desired file path to save the downloaded data
        //                            ////if (!Directory.Exists(filePath))
        //                            ////{
        //                            ////    Directory.CreateDirectory(filePath);
        //                            ////}
        //                            //// Create a FileStream to write the bytes to the specified file path
        //                            //using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        //                            //{
        //                            //    // Write the bytes to the FileStream
        //                            //    fileStream.Write(byteArray, 0, byteArray.Length);
        //                            //}





        //                            //System.Windows.Forms.NotifyIcon notifyIcon = new System.Windows.Forms.NotifyIcon();
        //                            //notifyIcon.Icon = SystemIcons.Information;
        //                            //notifyIcon.Visible = true;
        //                            //notifyIcon.BalloonTipTitle = "Save As";
        //                            //notifyIcon.BalloonTipText = "Please choose a location to save the file";
        //                            //notifyIcon.ShowBalloonTip(5000);





        //                            //byte[] byteArray = objWaybillResponse.AWBPrintContent; // Your byte array containing the data to be downloaded

        //                            //System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
        //                            //saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*"; // Set the file type filter
        //                            //saveFileDialog.FileName = objWaybillResponse.AWBNo + ".pdf"; // Set the default file name
        //                            //if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //                            //{
        //                            //    // Get the selected file path from the SaveFileDialog
        //                            //    string filePath = saveFileDialog.FileName;

        //                            //    // Create a FileStream to write the bytes to the specified file path
        //                            //    using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        //                            //    {
        //                            //        // Write the bytes to the FileStream
        //                            //        fileStream.Write(byteArray, 0, byteArray.Length);
        //                            //    }
        //                            //}



        //                            //notifyIcon.Visible = false;
        //                            //notifyIcon.Dispose();




        //                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Waybill created successfully. Waybill no. : " + objWaybillResponse.AWBNo + ". Pickup registration status : " + objWaybillResponse.Status[1].StatusCode.ToString() + "\");$('.close').click(function(){window.location.href ='frmPickupAssign.aspx' });", true);

        //                            //Context.Response.Clear();
        //                            //Context.Response.BufferOutput = false;
        //                            //MemoryStream ms = new MemoryStream(objWaybillResponse.AWBPrintContent);
        //                            //Context.Response.ContentType = "application/pdf";
        //                            //Context.Response.AddHeader("content-disposition", "attachment;filename=" + objWaybillResponse.AWBNo + ".pdf");
        //                            //Context.Response.Buffer = true;
        //                            //ms.WriteTo(Context.Response.OutputStream);
        //                            //Context.Response.Flush();
        //                            //Context.Response.Close();
        //                            //Thread.ResetAbort();

        //                            //Context.Response.End();

        //                            //HttpContext.Current.ApplicationInstance.CompleteRequest();
        //                            //Thread.ResetAbort();


        //                            //Response.Clear();
        //                            //Response.ContentType = "application/pdf";
        //                            //Response.AddHeader("Content-Disposition", "attachment; filename=" + objWaybillResponse.AWBNo + ".pdf");
        //                            //Response.BinaryWrite(objWaybillResponse.AWBPrintContent);
        //                            //Response.Flush();
        //                            //Response.SuppressContent = true; // avoid the "Thread was being aborted" exception
        //                            //HttpContext.Current.ApplicationInstance.CompleteRequest();

        //                            Session["AWBNo"] = objWaybillResponse.AWBNo;
        //                            Session["AWBPrintContent"] = objWaybillResponse.AWBPrintContent;

        //                            if (hfEntityID.Value != "" && hfEntityID.Value != string.Empty && hfEntityID.Value != null)
        //                            {
        //                                int iWSResult = objMainClass.ChangeOrderStatus(objMainClass.intCmpId, hfEntityID.Value, hfSONO.Value, "", "DISPATCH", "WEBSITESTATUSAPI", "MOBEXSHIPAPI", "shipment created Successfully.", "Bluedart", "shipment created Successfully.", objWaybillResponse.AWBNo, Convert.ToInt32(Session["USERID"]));
        //                            }

        //                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Waybill created successfully. Waybill no. : " + objWaybillResponse.AWBNo + ". Pickup registration status : " + objWaybillResponse.Status[1].StatusCode.ToString() + "\");$('.close').click(function(){window.location.href ='frmPickupAssign.aspx' });", true);

        //                            string path = "frmWayBillDownload.aspx";
        //                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "');", true);
        //                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Waybill created successfully. Waybill no. : '" + objWaybillResponse.AWBNo + ". Pickup registration status : " + objWaybillResponse.Status[1].StatusCode.ToString() + "'.');$('.close').click(function(){window.location.href ='frmPickupAssign.aspx' });", true);
        //                            //ViewState["response"] = "true";
        //                            //ViewState["waybillno"] = objWaybillResponse.AWBNo;
        //                            //ViewState["message"] = objWaybillResponse.Status[1].StatusCode.ToString();
        //                            //Response.Redirect("frmPickupAssign.aspx", true);
        //                        }
        //                        else
        //                        {
        //                            //string strURL = @"C:/Bluedart";
        //                            //if (!Directory.Exists(strURL))
        //                            //{
        //                            //    Directory.CreateDirectory(strURL);
        //                            //}
        //                            //File.WriteAllBytes(strURL + "/" + objWaybillResponse.AWBNo + ".pdf", objWaybillResponse.AWBPrintContent);

        //                            //Context.Response.Clear();
        //                            //Context.Response.BufferOutput = false;
        //                            //MemoryStream ms = new MemoryStream(objWaybillResponse.AWBPrintContent);
        //                            //Context.Response.ClearContent();
        //                            //Context.Response.ContentType = "application/pdf";
        //                            //Context.Response.AddHeader("content-disposition", "attachment;filename=" + objWaybillResponse.AWBNo + ".pdf");
        //                            //Context.Response.Buffer = true;
        //                            //ms.WriteTo(Context.Response.OutputStream);
        //                            //Context.Response.Flush();
        //                            //Context.Response.Close();
        //                            //Thread.ResetAbort();
        //                            //Context.Response.End();
        //                            //HttpContext.Current.ApplicationInstance.CompleteRequest();
        //                            //Thread.ResetAbort();

        //                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Waybill created successfully. But job id status not changed. Waybill no. : '" + objWaybillResponse.AWBNo + ". Pickup registration status : " + objWaybillResponse.Status[1].StatusCode.ToString() + "'.');$('.close').click(function(){window.location.href ='frmPickupAssign.aspx' });", true);
        //                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"Waybill created successfully. Waybill no. : " + objWaybillResponse.AWBNo + ". Pickup registration status : " + objWaybillResponse.Status[1].StatusCode.ToString() + "\");$('.close').click(function(){window.location.href ='frmPickupAssign.aspx' });", true);

        //                            Session["AWBNo"] = objWaybillResponse.AWBNo;
        //                            Session["AWBPrintContent"] = objWaybillResponse.AWBPrintContent;
        //                            if (hfEntityID.Value != "" && hfEntityID.Value != string.Empty && hfEntityID.Value != null)
        //                            {
        //                                int iWSResult = objMainClass.ChangeOrderStatus(objMainClass.intCmpId, hfEntityID.Value, hfSONO.Value, "", "DISPATCH", "WEBSITESTATUSAPI", "MOBEXSHIPAPI", "shipment created Successfully.", "Bluedart", "shipment created Successfully.", objWaybillResponse.AWBNo, Convert.ToInt32(Session["USERID"]));
        //                            }

        //                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Waybill created successfully. Waybill no. : " + objWaybillResponse.AWBNo + ". Pickup registration status : " + objWaybillResponse.Status[1].StatusCode.ToString() + "\");$('.close').click(function(){window.location.href ='frmPickupAssign.aspx' });", true);

        //                            string path = "frmWayBillDownload.aspx";
        //                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "');", true);


        //                        }




        //                    }
        //                    else
        //                    {
        //                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"Waybill not generated. " + Convert.ToString(objWaybillResponse.Status[0].StatusInformation) + "\");", true);
        //                        //ViewState["response"] = "false";
        //                        //ViewState["waybillno"] = "";
        //                        //ViewState["message"] = Convert.ToString(objWaybillResponse.Status[0].StatusInformation);
        //                    }
        //                    //lnkSerch_Click(1, e);
        //                }
        //                else
        //                {
        //                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Someting went worng while checking Pincode Service. Please contact administrator.!');", true);
        //                }

        //            }
        //            else
        //            {
        //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('API key not found. Please contact administrator.!');", true);
        //            }
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
        //    }
        //    //finally
        //    //{
        //    //    if (Convert.ToString(ViewState["response"]) == "true")
        //    //    {
        //    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Waybill created successfully. Waybill no. : " + Convert.ToString(ViewState["waybillno"]) + ". Pickup registration status : " + Convert.ToString(ViewState["message"]) + "\");$('.close').click(function(){window.location.href ='frmPickupAssign.aspx' });", true);
        //    //    }
        //    //    else if (Convert.ToString(ViewState["response"]) == "false")
        //    //    {
        //    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"Waybill not generated. " + Convert.ToString(ViewState["message"]) + "\");", true);
        //    //    }
        //    //    else
        //    //    {
        //    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Someting went worng. Please contact administrator.!')$('.close').click(function(){window.location.href ='frmPickupAssign.aspx' });", true);
        //    //    }
        //    //}
        //}

        #endregion

    }
}