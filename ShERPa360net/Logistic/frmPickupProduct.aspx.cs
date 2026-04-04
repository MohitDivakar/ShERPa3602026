using ShERPa360net.Class;
//using ShERPa360net.DemoPincode;
//using ShERPa360net.DemoWayBillGeneration;
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
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.Logistic
{
    public partial class frmPickupProduct : System.Web.UI.Page
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
                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");
                        objBindDDL.FillSegment(ddlSegment);
                        ddlSegment.SelectedValue = "1038";
                        
                        objBindDDL.FillDistChnlNew(ddlDistChnl);
                        ddlDistChnl.SelectedValue = "40";
                        objBindDDL.FillPlant(ddlPlant, "ENTRY");
                        objBindDDL.FillStatus(ddlStatus, 2);
                        ddlStatus.SelectedValue = "4";
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
                    dt = objMainClass.GetJobForLogistic(objMainClass.intCmpId, txtJobID.Text, ddlStatus.SelectedIndex > 0 ? ddlStatus.SelectedValue : "", ddlPlant.SelectedIndex > 0 ? ddlPlant.SelectedValue : Convert.ToString(Session["PLANTCD"]),
                        ddlSegment.SelectedIndex > 0 ? ddlSegment.SelectedValue : "", ddlDistChnl.SelectedIndex > 0 ? ddlDistChnl.SelectedValue : "", txtIMEINo.Text, txtFromDate.Text, txtToDate.Text, "GETJOBLIST");

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
                    string jobid = grdrow.Cells[4].Text;

                    DataTable dt = new DataTable();
                    dt = objMainClass.GetJobForLogistic(objMainClass.intCmpId, jobid, "", "", "", "", "", "", "", "GENERATEDATA");

                    if (dt.Rows.Count > 0)
                    {
                        lblSenderName.Text = Convert.ToString(dt.Rows[0]["ENDCUST"]);
                        lblSenderAdd1.Text = Convert.ToString(dt.Rows[0]["CUSTADDR1"]);
                        lblSenderAdd2.Text = Convert.ToString(dt.Rows[0]["CUSTADDR2"]);
                        lblSenderAdd3.Text = Convert.ToString(dt.Rows[0]["CUSTADDR3"]);
                        lblSenderPincode.Text = Convert.ToString(dt.Rows[0]["PINCODE"]);
                        lblSenderMobile.Text = Convert.ToString(dt.Rows[0]["CUSTMOBILENO"]);
                        lblSenderTelephone.Text = Convert.ToString(dt.Rows[0]["CUSTMOBILENO"]);
                        lblSenderEmail.Text = Convert.ToString(dt.Rows[0]["CUSTEMAIL"]);

                        lblConsiName.Text = Convert.ToString(dt.Rows[0]["COMPANYNAME"]);

                        if (Convert.ToString(dt.Rows[0]["DROPADD"]).Length > 60)
                        {
                            lblConsiAddr1.Text = Convert.ToString(dt.Rows[0]["DROPADD"]).Substring(0, 30);
                            lblConsiAddr2.Text = Convert.ToString(dt.Rows[0]["DROPADD"]).Substring(30, 30);
                            lblConsiAddr3.Text = Convert.ToString(dt.Rows[0]["DROPADD"]).Substring(60);
                        }
                        else if (Convert.ToString(dt.Rows[0]["DROPADD"]).Length > 30)
                        {
                            lblConsiAddr1.Text = Convert.ToString(dt.Rows[0]["DROPADD"]).Substring(0, 30);
                            lblConsiAddr2.Text = Convert.ToString(dt.Rows[0]["DROPADD"]).Substring(30);
                            lblConsiAddr3.Text = Convert.ToString(dt.Rows[0]["DROPCITY"]);
                        }
                        else
                        {
                            lblConsiAddr1.Text = Convert.ToString(dt.Rows[0]["DROPADD"]);
                            lblConsiAddr2.Text = Convert.ToString(dt.Rows[0]["DROPCITY"]);
                            lblConsiAddr3.Text = Convert.ToString(dt.Rows[0]["DROPCITY"]);
                        }


                        //lblConsiAddr1.Text = Convert.ToString(dt.Rows[0]["CUSTADD"]).Substring(0, 30);
                        //lblConsiAddr2.Text = Convert.ToString(dt.Rows[0]["CUSTADD"]).Substring(30, 30);
                        //lblConsiAddr3.Text = Convert.ToString(dt.Rows[0]["CUSTADD"]).Substring(61);
                        lblConsiPincode.Text = Convert.ToString(dt.Rows[0]["POSTALCODE"]);
                        lblConsiMobile.Text = Convert.ToString(dt.Rows[0]["CONTACTNO"]);
                        lblConsiTelephone.Text = Convert.ToString(dt.Rows[0]["CONTACTNO"]);
                        lblConsiAttention.Text = Convert.ToString(dt.Rows[0]["ITEMGRP"]);//"Mobile Device";

                        lblQty.Text = Convert.ToString(dt.Rows[0]["QTY"]);
                        lblProductCode.Text = "A";
                        //if (Convert.ToString(dt.Rows[0]["PAYMODE"]) == "1")
                        //{
                        //    lblSubProdCode.Text = "C";
                        //    lblDeclareValue.Text = Convert.ToString(dt.Rows[0]["CAMOUNT"]);
                        //    lblCollectableAmt.Text = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["CAMOUNT"]));// - Convert.ToDecimal(dt.Rows[0]["DISCAMT"]));
                        //}
                        //else if (Convert.ToString(dt.Rows[0]["PAYMODE"]) == "14")
                        //{
                        //    lblSubProdCode.Text = "C";
                        //    lblDeclareValue.Text = Convert.ToString(dt.Rows[0]["CAMOUNT"]);
                        //    lblCollectableAmt.Text = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["CAMOUNT"]) - Convert.ToDecimal(dt.Rows[0]["PREPAIDAMT"]));
                        //}
                        //else
                        //{
                        lblSubProdCode.Text = "P";
                        lblDeclareValue.Text = Convert.ToString(dt.Rows[0]["REFINVAMT"]);
                        lblCollectableAmt.Text = "0";
                        //}
                        lblWeight.Text = "0.5";
                        lblInvoiceno.Text = objMainClass.strConvertZeroPadding(jobid);
                        lblSpecialInstruction.Text = "WARRANTY RETURN CASE";//Convert.ToString(dt.Rows[0]["ITEMGRP"]); //"Mobile Device";
                        lblPackType.Text = "A";
                        lblCreditRefNo.Text = Convert.ToString(dt.Rows[0]["JOBID"]) + "" + DateTime.Now.Day + "" + DateTime.Now.Month + "" + DateTime.Now.Year + "" + DateTime.Now.Second;
                        lblCmdtDetail1.Text = Convert.ToString(dt.Rows[0]["ITEMGRP"]);

                        hfOriginArea.Value = Convert.ToString(dt.Rows[0]["ORIGINAREA"]);
                        hfCustCode.Value = Convert.ToString(dt.Rows[0]["CUSTOMERCODE"]);
                        hfPlant.Value = Convert.ToString(dt.Rows[0]["PLANTCD"]);
                        hfSINO.Value = objMainClass.strConvertZeroPadding(jobid);
                        hfjobID.Value = objMainClass.strConvertZeroPadding(jobid);
                        hfSONO.Value = Convert.ToString(dt.Rows[0]["IMEINO"]);
                        hfSenderCity.Value = Convert.ToString(dt.Rows[0]["CUSTCITY"]);
                        hfSenderState.Value = Convert.ToString(dt.Rows[0]["CUSTSTATE"]);

                        hfDropCity.Value = Convert.ToString(dt.Rows[0]["DROPCITY"]);
                        hfDropState.Value = Convert.ToString(dt.Rows[0]["DROPSTATE"]);

                        hfDropEmail.Value = Convert.ToString(dt.Rows[0]["EMAILID"]);
                        hfMake.Value = Convert.ToString(dt.Rows[0]["PRODMAKE"]);
                        hfModel.Value = Convert.ToString(dt.Rows[0]["PRODMODEL"]);
                        //hfEntityID.Value = entityid;
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

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dtAPIKEY = new DataTable();
                    dtAPIKEY = objMainClass.GetLogisticData(objMainClass.intCmpId, "", "", "", "", 0, "BLUDARTAPI", hfPlant.Value, 1, "GETAPIKEY");

                    if (dtAPIKEY.Rows.Count > 0)
                    {
                        LivePincode.UserProfile objPincodeUserProfile = new LivePincode.UserProfile();
                        //DemoPincode.UserProfile objPincodeUserProfile = new DemoPincode.UserProfile();
                        objPincodeUserProfile.Api_type = Convert.ToString(dtAPIKEY.Rows[0]["APITYPE"]);
                        objPincodeUserProfile.Area = Convert.ToString(dtAPIKEY.Rows[0]["AREA"]);
                        objPincodeUserProfile.Customercode = Convert.ToString(dtAPIKEY.Rows[0]["UNIQUECODE"]);
                        objPincodeUserProfile.LicenceKey = Convert.ToString(dtAPIKEY.Rows[0]["KEYVALUE"]);
                        objPincodeUserProfile.LoginID = Convert.ToString(dtAPIKEY.Rows[0]["KEYNAME"]);
                        objPincodeUserProfile.Version = Convert.ToString(dtAPIKEY.Rows[0]["VERSION"]);

                        LiveWayBillGeneration.UserProfile objWayBillUserProfile = new LiveWayBillGeneration.UserProfile();
                        //DemoWayBillGeneration.UserProfile objWayBillUserProfile = new DemoWayBillGeneration.UserProfile();
                        objWayBillUserProfile.Api_type = Convert.ToString(dtAPIKEY.Rows[0]["APITYPE"]);
                        objWayBillUserProfile.Area = Convert.ToString(dtAPIKEY.Rows[0]["AREA"]);
                        objWayBillUserProfile.Customercode = Convert.ToString(dtAPIKEY.Rows[0]["UNIQUECODE"]);
                        objWayBillUserProfile.LicenceKey = Convert.ToString(dtAPIKEY.Rows[0]["KEYVALUE"]);
                        objWayBillUserProfile.LoginID = Convert.ToString(dtAPIKEY.Rows[0]["KEYNAME"]);
                        objWayBillUserProfile.Version = Convert.ToString(dtAPIKEY.Rows[0]["VERSION"]);

                        LivePincode.ServiceFinderQueryClient PincodeSearch = new LivePincode.ServiceFinderQueryClient();
                        //DemoPincode.ServiceFinderQueryClient PincodeSearch = new DemoPincode.ServiceFinderQueryClient();


                        LivePincode.ServiceCenterDetailsReference pincodeDetails = new LivePincode.ServiceCenterDetailsReference();
                        //DemoPincode.ServiceCenterDetailsReference pincodeDetails = new DemoPincode.ServiceCenterDetailsReference();

                        pincodeDetails = PincodeSearch.GetServicesforPincode(lblConsiPincode.Text, objPincodeUserProfile);

                        if (pincodeDetails.IsError == false)
                        {
                            LiveWayBillGeneration.WayBillGenerationRequest objWayBillReq = new LiveWayBillGeneration.WayBillGenerationRequest();
                            //DemoWayBillGeneration.WayBillGenerationRequest objWayBillReq = new DemoWayBillGeneration.WayBillGenerationRequest();

                            #region Shipper Data...

                            objWayBillReq.Shipper = new LiveWayBillGeneration.Shipper();
                            //objWayBillReq.Shipper = new DemoWayBillGeneration.Shipper();
                            objWayBillReq.Shipper.OriginArea = hfOriginArea.Value;
                            objWayBillReq.Shipper.CustomerCode = hfCustCode.Value;
                            objWayBillReq.Shipper.CustomerName = lblSenderName.Text;
                            objWayBillReq.Shipper.CustomerAddress1 = lblSenderAdd1.Text;
                            objWayBillReq.Shipper.CustomerAddress2 = lblSenderAdd2.Text;
                            objWayBillReq.Shipper.CustomerAddress3 = lblSenderAdd3.Text;
                            objWayBillReq.Shipper.CustomerPincode = lblSenderPincode.Text;
                            objWayBillReq.Shipper.CustomerTelephone = lblSenderTelephone.Text;
                            objWayBillReq.Shipper.CustomerMobile = lblSenderMobile.Text;
                            objWayBillReq.Shipper.CustomerEmailID = lblSenderEmail.Text;
                            objWayBillReq.Shipper.Sender = lblSenderName.Text;
                            objWayBillReq.Shipper.IsToPayCustomer = true;
                            objWayBillReq.Shipper.VendorCode = "1";

                            #endregion

                            #region Consignee Data...

                            objWayBillReq.Consignee = new LiveWayBillGeneration.Consignee();
                            //objWayBillReq.Consignee = new DemoWayBillGeneration.Consignee();
                            objWayBillReq.Consignee.ConsigneeName = lblConsiName.Text;
                            objWayBillReq.Consignee.ConsigneeAddress1 = lblConsiAddr1.Text;
                            objWayBillReq.Consignee.ConsigneeAddress2 = lblConsiAddr2.Text;
                            objWayBillReq.Consignee.ConsigneeAddress3 = lblConsiAddr3.Text;
                            objWayBillReq.Consignee.ConsigneePincode = lblConsiPincode.Text;
                            objWayBillReq.Consignee.ConsigneeTelephone = lblConsiTelephone.Text;
                            objWayBillReq.Consignee.ConsigneeMobile = lblConsiMobile.Text;
                            objWayBillReq.Consignee.ConsigneeEmailID = "";
                            objWayBillReq.Consignee.ConsigneeAttention = lblConsiName.Text;

                            #endregion

                            #region Service Data...

                            objWayBillReq.Services = new LiveWayBillGeneration.Services();
                            //objWayBillReq.Services = new DemoWayBillGeneration.Services();


                            //objWayBillReq.Services.ProductCode = "A";// lblProductCode.Text;
                            objWayBillReq.Services.ProductCode = lblProductCode.Text;
                            objWayBillReq.Services.ProductType = rblProductType.SelectedValue == "0" ? LiveWayBillGeneration.ProductType.Docs : LiveWayBillGeneration.ProductType.Dutiables;
                            //objWayBillReq.Services.ProductType = rblProductType.SelectedValue == "0" ? DemoWayBillGeneration.ProductType.Docs : DemoWayBillGeneration.ProductType.Dutiables;
                            objWayBillReq.Services.SubProductCode = lblSubProdCode.Text;
                            //if (lblQty.Text == "1")
                            //{
                            objWayBillReq.Services.PieceCount = 1;//Convert.ToInt16(lblQty.Text);
                                                                  //}


                            objWayBillReq.Services.ActualWeight = 0.5;
                            //objWayBillReq.Services.PackType = lblPackType.Text;
                            objWayBillReq.Services.InvoiceNo = hfSINO.Value;
                            objWayBillReq.Services.SpecialInstruction = lblSpecialInstruction.Text;
                            objWayBillReq.Services.DeclaredValue = Convert.ToDouble(lblDeclareValue.Text);
                            if (lblSubProdCode.Text == "C")
                            {
                                objWayBillReq.Services.CollectableAmount = Convert.ToDouble(lblCollectableAmt.Text);
                            }
                            objWayBillReq.Services.CreditReferenceNo = lblCreditRefNo.Text;
                            //objWayBillReq.Services.PDFOutputNotRequired = rblWaybillPDF.SelectedValue == "1" ? false : true;
                            //objWayBillReq.Services.AWBNo = "";
                            objWayBillReq.Services.RegisterPickup = chkPickup.Checked ? true : false;
                            //objWayBillReq.Services.DeliveryTimeSlot = "";
                            objWayBillReq.Services.IsReversePickup = chkIsRVP.Checked ? true : false;


                            LiveWayBillGeneration.Dimension[] d = new LiveWayBillGeneration.Dimension[1];
                            //DemoWayBillGeneration.Dimension[] d = new DemoWayBillGeneration.Dimension[1];

                            d[0] = new LiveWayBillGeneration.Dimension();
                            //d[0] = new DemoWayBillGeneration.Dimension();

                            d[0].Breadth = 10;
                            d[0].Count = 1;
                            d[0].Height = 5;
                            d[0].Length = 20;
                            objWayBillReq.Services.Dimensions = d;

                            LiveWayBillGeneration.CommodityDetail CD = new LiveWayBillGeneration.CommodityDetail();
                            CD.CommodityDetail1 = lblCmdtDetail1.Text;
                            objWayBillReq.Services.Commodity = CD;

                            TimeSpan timespan = new TimeSpan(15, 00, 00);
                            DateTime time = DateTime.Today.Add(timespan);
                            string PTime = time.ToString("hh:mm");
                            if (Convert.ToInt32(DateTime.Now.ToString("hh")) <= Convert.ToInt32(time.ToString("hh")))
                            {
                                objWayBillReq.Services.PickupDate = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("dd-MM-yyyy"));
                            }
                            else
                            {
                                objWayBillReq.Services.PickupDate = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"));

                            }
                            objWayBillReq.Services.PickupTime = "1500";

                            #endregion


                            LiveWayBillGeneration.WayBillGenerationResponse objWaybillResponse = new LiveWayBillGeneration.WayBillGenerationResponse();
                            //DemoWayBillGeneration.WayBillGenerationResponse objWaybillResponse = new DemoWayBillGeneration.WayBillGenerationResponse();


                            LiveWayBillGeneration.WayBillGenerationClient objWaybillClient = new LiveWayBillGeneration.WayBillGenerationClient();
                            //DemoWayBillGeneration.WayBillGenerationClient objWaybillClient = new DemoWayBillGeneration.WayBillGenerationClient();



                            objWaybillResponse = objWaybillClient.GenerateWayBill(objWayBillReq, objWayBillUserProfile);

                            if (objWaybillResponse.IsError == false)
                            {
                                Session["AWBNo"] = objWaybillResponse.AWBNo;
                                Session["AWBPrintContent"] = objWaybillResponse.AWBPrintContent;

                                string path = "frmWayBillDownload.aspx";
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "');", true);

                                int iResult = objMainClass.InsertUpdateJobDetails(objMainClass.intCmpId, hfjobID.Value, (int)STATUS.RevWayBillGen, "", "", "", "", "", "", "BLUDART", objWaybillResponse.AWBNo, "WAYBILL GENERATED VIA API", "JS",
                                    (int)STAGE.RevWayBillNo, "WAYBILL GENERATED VIA API", Convert.ToInt32(Session["USERID"]), "UPDATEJOBID");

                                int iResult1 = objMainClass.InsertLogisticDetail(lblSenderName.Text, lblSenderAdd1.Text, hfSenderCity.Value, hfSenderState.Value, lblSenderPincode.Text, lblSenderMobile.Text, lblSenderEmail.Text, Convert.ToDateTime(DateTime.Now).ToShortDateString(),
                                    Convert.ToDateTime(DateTime.Now).ToShortTimeString(), lblConsiName.Text, lblConsiAddr1.Text, hfDropCity.Value, hfDropState.Value, lblConsiPincode.Text, lblConsiMobile.Text, hfDropEmail.Value, "Pickup", "", hfjobID.Value, Convert.ToInt32(Session["USERID"]),
                                    hfjobID.Value, hfMake.Value, hfModel.Value, "", lblSenderAdd2.Text, lblConsiAddr2.Text, "", hfjobID.Value, "JOBSHEET", "", objWaybillResponse.AWBNo, "BLUDART", "BLUEDART", 0, "57", 0, "https://www.bluedart.com/", "", objWaybillResponse.AWBNo, "", 0, 0);
                                //Convert.ToString(dtPickUpAddress.Rows[0]["ADDR1"]), Convert.ToString(dtPickUpAddress.Rows[0]["CITY"]),
                                //Convert.ToString(dtPickUpAddress.Rows[0]["STATE"]), Convert.ToString(dtPickUpAddress.Rows[0]["POSTALCODE"]),
                                //Convert.ToString(dtPickUpAddress.Rows[0]["CONTACTNO"]), "", estidate, estitime, "Qarmatek Services PVT. LTD.",
                                //Convert.ToString(dtDropAddress.Rows[0]["ADDR1"]), Convert.ToString(dtDropAddress.Rows[0]["CITY"]), Convert.ToString(dtDropAddress.Rows[0]["STATE"]),
                                //Convert.ToString(dtDropAddress.Rows[0]["POSTALCODE"]), Convert.ToString(dtDropAddress.Rows[0]["CONTACTNO"]), "", "Pickup", "", requestid, Convert.ToInt32(Session["USERID"]),
                                //requestid, lblMAKE.Text, lblMODEL.Text, "", Convert.ToString(dtPickUpAddress.Rows[0]["ADDR2"]), Convert.ToString(dtDropAddress.Rows[0]["ADDR2"]), "", requestid, "LISTING",
                                //"", orderid, "BIKER", "PORTER", amt, "57", 0, trackingurl, "", orderid, "", 0, 11925);

                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Waybill created successfully. Waybill no. : " + objWaybillResponse.AWBNo + ". Pickup registration status : " + objWaybillResponse.Status[1].StatusCode.ToString() + "\");$('.close').click(function(){window.location.href ='frmPickupProduct.aspx' });", true);

                                
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"Waybill not generated. " + Convert.ToString(objWaybillResponse.Status[0].StatusInformation) + "\");", true);
                                //ViewState["response"] = "false";
                                //ViewState["waybillno"] = "";
                                //ViewState["message"] = Convert.ToString(objWaybillResponse.Status[0].StatusInformation);
                            }


                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Someting went worng while checking Pincode Service. Please contact administrator.!');", true);
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('API key not found. Please contact administrator.!');", true);
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