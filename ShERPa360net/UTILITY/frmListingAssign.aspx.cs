using Newtonsoft.Json;
using RestSharp;
using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ShERPa360net.UTILITY
{
    public partial class frmListingAssign : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
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
                    DataTable dt = new DataTable();
                    dt = GetData(objMainClass.intCmpId, txtListingID.Text, "GETLISTINGDATA");

                    if (dt.Rows.Count > 0)
                    {
                        gvList.DataSource = dt;
                        gvList.DataBind();
                    }
                    else
                    {
                        gvList.DataSource = string.Empty;
                        gvList.DataBind();
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

        public DataTable GetData(int CMPID, string LISTINGID, string ACTION)
        {
            DataTable dt = new DataTable();
            try
            {
                if (Session["USERID"] != null)
                {
                    dt = objMainClass.GetListingAssign(CMPID, LISTINGID, ACTION);
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
            return dt;
        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    lnkAllAssignToPorter.Enabled = false;
                    lnkAllAssignToPorter.Visible = false;

                    GridViewRow hrow = gvList.HeaderRow;
                    CheckBox chkSelectAll = (CheckBox)hrow.FindControl("chkSelectAll");
                    if (chkSelectAll.Checked == true)
                    {
                        for (int i = 0; i < gvList.Rows.Count; i++)
                        {
                            GridViewRow row = gvList.Rows[i];
                            CheckBox chkSelect = ((CheckBox)row.FindControl("chkSelect"));
                            Label lblLAT = ((Label)row.FindControl("lblLAT"));
                            Label lblLONG = ((Label)row.FindControl("lblLONG"));
                            if (lblLAT.Text != string.Empty && lblLAT.Text != null && lblLAT.Text != "" &&
                                lblLONG.Text != string.Empty && lblLONG.Text != null && lblLONG.Text != "")
                            {
                                chkSelect.Checked = true;
                            }
                            else
                            {
                                chkSelect.Checked = false;
                            }
                        }
                        lnkAllAssignToPorter.Enabled = true;
                        lnkAllAssignToPorter.Visible = true;
                    }
                    else
                    {
                        for (int i = 0; i < gvList.Rows.Count; i++)
                        {
                            GridViewRow row = gvList.Rows[i];
                            CheckBox chkSelect = ((CheckBox)row.FindControl("chkSelect"));
                            chkSelect.Checked = false;
                        }
                        lnkAllAssignToPorter.Enabled = false;
                        lnkAllAssignToPorter.Visible = false;
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

        protected void btnAssign_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

                    Label lblID = (Label)grdrow.FindControl("lblID");
                    Label lblVENDORID = (Label)grdrow.FindControl("lblVENDORID");

                    Label lblMAKE = (Label)grdrow.FindControl("lblMAKE");
                    Label lblMODEL = (Label)grdrow.FindControl("lblMODEL");
                    Label lblCOLOR = (Label)grdrow.FindControl("lblCOLOR");
                    Label lblRAM = (Label)grdrow.FindControl("lblRAM");
                    Label lblROM = (Label)grdrow.FindControl("lblROM");
                    Label lblVENDORGRADE = (Label)grdrow.FindControl("lblVENDORGRADE");
                    Label lblVENDORNAME = (Label)grdrow.FindControl("lblVENDORNAME");


                    Label lblLAT = (Label)grdrow.FindControl("lblLAT");
                    Label lblLONG = (Label)grdrow.FindControl("lblLONG");

                    if (lblLAT.Text != "" && lblLAT.Text != string.Empty && lblLAT.Text != null)
                    {
                        if (lblLONG.Text != "" && lblLONG.Text != string.Empty && lblLONG.Text != null)
                        {
                            DataTable dtPickUpAddress = new DataTable();
                            dtPickUpAddress = objMainClass.FetchDealerData(Convert.ToInt32(lblVENDORID.Text), "DEALERBYID");
                            if (dtPickUpAddress.Rows.Count > 0)
                            {
                                DataTable dtDropAddress = new DataTable();
                                dtDropAddress = objMainClass.FetchDealerData(1797278, "DROPBYID");
                                if (dtDropAddress.Rows.Count > 0)
                                {
                                    PorterClass objPorterClass = new PorterClass();
                                    DeliveryInstructions objDeliveryInstructions = new DeliveryInstructions();

                                    PickupDetails objPickupDetails = new PickupDetails();
                                    DropDetails objDropDetails = new DropDetails();
                                    Address objPickUpAddress = new Address();
                                    Address objDropAddress = new Address();
                                    List<InstructionsList> objInstructionsList = new List<InstructionsList>();
                                    ContactDetails objPickUpContactDetailsList = new ContactDetails();
                                    ContactDetails objDropContactDetailsList = new ContactDetails();
                                    objPickUpContactDetailsList.name = Convert.ToString(dtPickUpAddress.Rows[0]["DEALERNAME"]);
                                    objPickUpContactDetailsList.phone_number = Convert.ToString(dtPickUpAddress.Rows[0]["CONTACTNO"]);
                                    objDropContactDetailsList.name = "Qarmatek Services PVT. LTD.";
                                    objDropContactDetailsList.phone_number = Convert.ToString(dtDropAddress.Rows[0]["CONTACTNO"]);

                                    objInstructionsList.Add(new InstructionsList
                                    {
                                        type = "text",
                                        description = lblMAKE.Text + " " + lblMODEL.Text + " " + lblRAM.Text + " " + lblROM.Text + " " + lblCOLOR.Text + " " + lblVENDORGRADE.Text
                                    });

                                    objPickUpAddress.contact_details = objPickUpContactDetailsList;
                                    objPickUpAddress.city = Convert.ToString(dtPickUpAddress.Rows[0]["CITY"]);
                                    objPickUpAddress.country = Convert.ToString(dtPickUpAddress.Rows[0]["COUNTRY"]);
                                    objPickUpAddress.landmark = Convert.ToString(dtPickUpAddress.Rows[0]["ADDR3"]);
                                    objPickUpAddress.lat = Convert.ToDouble(dtPickUpAddress.Rows[0]["LAT"]);
                                    objPickUpAddress.lng = Convert.ToDouble(dtPickUpAddress.Rows[0]["LONG"]);
                                    objPickUpAddress.pincode = Convert.ToString(dtPickUpAddress.Rows[0]["POSTALCODE"]);
                                    objPickUpAddress.state = Convert.ToString(dtPickUpAddress.Rows[0]["STATE"]);
                                    objPickUpAddress.apartment_address = Convert.ToString(dtPickUpAddress.Rows[0]["DEALERNAME"]);
                                    objPickUpAddress.street_address1 = Convert.ToString(dtPickUpAddress.Rows[0]["ADDR1"]);
                                    objPickUpAddress.street_address2 = Convert.ToString(dtPickUpAddress.Rows[0]["ADDR2"]);

                                    objDropAddress.contact_details = objDropContactDetailsList;
                                    objDropAddress.city = Convert.ToString(dtDropAddress.Rows[0]["CITY"]);
                                    objDropAddress.country = Convert.ToString(dtDropAddress.Rows[0]["COUNTRY"]);
                                    objDropAddress.landmark = Convert.ToString(dtDropAddress.Rows[0]["ADDR3"]);
                                    objDropAddress.lat = Convert.ToDouble(dtDropAddress.Rows[0]["LAT"]);
                                    objDropAddress.lng = Convert.ToDouble(dtDropAddress.Rows[0]["LONG"]);
                                    objDropAddress.pincode = Convert.ToString(dtDropAddress.Rows[0]["POSTALCODE"]);
                                    objDropAddress.state = Convert.ToString(dtDropAddress.Rows[0]["STATE"]);
                                    objDropAddress.apartment_address = "Qarmatek Services PVT. LTD.";
                                    objDropAddress.street_address1 = Convert.ToString(dtDropAddress.Rows[0]["ADDR1"]);
                                    objDropAddress.street_address2 = Convert.ToString(dtDropAddress.Rows[0]["ADDR2"]);


                                    objDeliveryInstructions.instructions_list = objInstructionsList;

                                    objPickupDetails.address = objPickUpAddress;

                                    objDropDetails.address = objDropAddress;

                                    objPorterClass.delivery_instructions = objDeliveryInstructions;
                                    objPorterClass.drop_details = objDropDetails;
                                    objPorterClass.pickup_details = objPickupDetails;
                                    objPorterClass.request_id = lblID.Text;



                                    DataTable dtAPIKEY = new DataTable();
                                    dtAPIKEY = objMainClass.GetWAData("PORTERAPI", 1, "GETWADATA");

                                    if (dtAPIKEY.Rows.Count > 0)
                                    {
                                        string KEYNAME = Convert.ToString(dtAPIKEY.Rows[0]["KEYNAME"]);
                                        string KEYVALUE = Convert.ToString(dtAPIKEY.Rows[0]["KEYVALUE"]);
                                        string URL = Convert.ToString(dtAPIKEY.Rows[0]["OTHER"]);
                                        //var client = new RestClient("https://pfe-apigw-uat.porter.in/v1/orders/create");
                                        var client = new RestClient(URL + "/v1/orders/create");

                                        client.Timeout = -1;
                                        var request = new RestRequest(Method.POST);
                                        request.AddHeader(KEYNAME, KEYVALUE);
                                        request.AddHeader("Content-Type", "application/json");

                                        var body = JsonConvert.SerializeObject(objPorterClass);

                                        request.AddParameter("application/json", body, ParameterType.RequestBody);
                                        IRestResponse response = client.Execute(request);
                                        if (Convert.ToString(response.StatusCode) == "Created")
                                        {
                                            string jsonsend = response.Content;
                                            PorterSuccess objPorterSuccess = new PorterSuccess();

                                            objPorterSuccess = JsonConvert.DeserializeObject<PorterSuccess>(jsonsend);

                                            string orderid = Convert.ToString(objPorterSuccess.order_id);
                                            string requestid = Convert.ToString(objPorterSuccess.request_id);
                                            long estimatetimestamp = Convert.ToInt64(objPorterSuccess.estimated_pickup_time) / 1000;
                                            //long estimatetimestamp = Convert.ToInt64(objPorterSuccess.estimated_pickup_time);
                                            string trackingurl = Convert.ToString(objPorterSuccess.tracking_url);
                                            int amt = (objPorterSuccess.estimated_fare_details.minor_amount) / 100;

                                            //System.DateTime dat_Time = new System.DateTime(1965, 1, 1, 0, 0, 0, 0);
                                            //dat_Time = dat_Time.AddSeconds(estimatetimestamp / 100d);
                                            //string estidate = dat_Time.ToShortDateString();
                                            //string estitime = dat_Time.ToShortTimeString();


                                            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(estimatetimestamp);
                                            string[] datetime = (dateTimeOffset.ToString()).Split(new char[] { ' ', '+' });
                                            string estidate = datetime[0];
                                            string estitime = datetime[1];


                                            int iResult = objMainClass.InsertLogisticDetail(Convert.ToString(dtPickUpAddress.Rows[0]["DEALERNAME"]),
                                                Convert.ToString(dtPickUpAddress.Rows[0]["ADDR1"]), Convert.ToString(dtPickUpAddress.Rows[0]["CITY"]),
                                                Convert.ToString(dtPickUpAddress.Rows[0]["STATE"]), Convert.ToString(dtPickUpAddress.Rows[0]["POSTALCODE"]),
                                                Convert.ToString(dtPickUpAddress.Rows[0]["CONTACTNO"]), "", estidate, estitime, "Qarmatek Services PVT. LTD.",
                                                Convert.ToString(dtDropAddress.Rows[0]["ADDR1"]), Convert.ToString(dtDropAddress.Rows[0]["CITY"]), Convert.ToString(dtDropAddress.Rows[0]["STATE"]),
                                                Convert.ToString(dtDropAddress.Rows[0]["POSTALCODE"]), Convert.ToString(dtDropAddress.Rows[0]["CONTACTNO"]), "", "Pickup", "", requestid, Convert.ToInt32(Session["USERID"]),
                                                requestid, lblMAKE.Text, lblMODEL.Text, "", Convert.ToString(dtPickUpAddress.Rows[0]["ADDR2"]), Convert.ToString(dtDropAddress.Rows[0]["ADDR2"]), "", requestid, "LISTING",
                                                "", orderid, "BIKER", "PORTER", amt, "57", 0, trackingurl, "", orderid, "", 0, 11925);
                                            if (iResult > 0)
                                            {
                                                DataTable dtSMS = new DataTable();
                                                dtSMS = objMainClass.GetContactDetails(objMainClass.intCmpId, 1, "FETCHCONTACTDETAILS");
                                                if (dtSMS.Rows.Count > 0)
                                                {
                                                    if (Convert.ToString(dtSMS.Rows[0]["BSM"]) != "" && Convert.ToString(dtSMS.Rows[0]["BSM"]) != string.Empty && Convert.ToString(dtSMS.Rows[0]["BSM"]) != "")
                                                    {
                                                        WAClass objWAClass = new WAClass();
                                                        objWAClass.SendTextMessage("Pickup Request generated with Porter Courier Service. Listing ID is " + lblID.Text + ". Shop name is " + lblVENDORNAME.Text + ". Tracking No. is " + orderid + ". Tracking URL is " + trackingurl, "91" + Convert.ToString(dtSMS.Rows[0]["BSM"]), Convert.ToString(Session["USERID"]));
                                                    }
                                                }
                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Pickup request generated in Porter and updated in ShERPa.!');$('.close').click(function(){window.location.href ='frmListingAssign.aspx'});", true);
                                            }
                                            else
                                            {
                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Pickup request generated in Porter but not updated in ShERpa. Tracking URL is : " + objPorterSuccess.tracking_url.ToString() + " .Tracking No. is : " + objPorterSuccess.order_id.ToString() + "');", true);
                                            }

                                        }
                                        else
                                        {
                                            string jsonsend = response.Content;
                                            PorterError objPorterError = new PorterError();
                                            objPorterError = JsonConvert.DeserializeObject<PorterError>(jsonsend);
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('" + objPorterError.message.ToString() + "');", true);
                                        }

                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('API key not Found!');", true);
                                    }

                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Drop to data not Found!');", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Pickup from data not Found!');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Pickup from longitude not given!');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Pickup from latitude not given!');", true);
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

        protected void lnkAllAssignToPorter_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    string listingIDs = string.Empty;

                    for (int i = 0; i < gvList.Rows.Count; i++)
                    {
                        GridViewRow grdrow = gvList.Rows[i];
                        CheckBox chkSelect = (CheckBox)grdrow.FindControl("chkSelect");
                        if (chkSelect.Checked == true)
                        {
                            Label lblID = (Label)grdrow.FindControl("lblID");
                            Label lblVENDORID = (Label)grdrow.FindControl("lblVENDORID");

                            Label lblMAKE = (Label)grdrow.FindControl("lblMAKE");
                            Label lblMODEL = (Label)grdrow.FindControl("lblMODEL");
                            Label lblCOLOR = (Label)grdrow.FindControl("lblCOLOR");
                            Label lblRAM = (Label)grdrow.FindControl("lblRAM");
                            Label lblROM = (Label)grdrow.FindControl("lblROM");
                            Label lblVENDORGRADE = (Label)grdrow.FindControl("lblVENDORGRADE");
                            Label lblVENDORNAME = (Label)grdrow.FindControl("lblVENDORNAME");


                            Label lblLAT = (Label)grdrow.FindControl("lblLAT");
                            Label lblLONG = (Label)grdrow.FindControl("lblLONG");

                            if (lblLAT.Text != "" && lblLAT.Text != string.Empty && lblLAT.Text != null)
                            {
                                if (lblLONG.Text != "" && lblLONG.Text != string.Empty && lblLONG.Text != null)
                                {
                                    DataTable dtPickUpAddress = new DataTable();
                                    dtPickUpAddress = objMainClass.FetchDealerData(Convert.ToInt32(lblVENDORID.Text), "DEALERBYID");
                                    if (dtPickUpAddress.Rows.Count > 0)
                                    {
                                        DataTable dtDropAddress = new DataTable();
                                        dtDropAddress = objMainClass.FetchDealerData(1797278, "DROPBYID");
                                        if (dtDropAddress.Rows.Count > 0)
                                        {
                                            PorterClass objPorterClass = new PorterClass();
                                            DeliveryInstructions objDeliveryInstructions = new DeliveryInstructions();

                                            PickupDetails objPickupDetails = new PickupDetails();
                                            DropDetails objDropDetails = new DropDetails();
                                            Address objPickUpAddress = new Address();
                                            Address objDropAddress = new Address();
                                            List<InstructionsList> objInstructionsList = new List<InstructionsList>();
                                            ContactDetails objPickUpContactDetailsList = new ContactDetails();
                                            ContactDetails objDropContactDetailsList = new ContactDetails();
                                            objPickUpContactDetailsList.name = Convert.ToString(dtPickUpAddress.Rows[0]["DEALERNAME"]);
                                            objPickUpContactDetailsList.phone_number = Convert.ToString(dtPickUpAddress.Rows[0]["CONTACTNO"]);
                                            objDropContactDetailsList.name = "Qarmatek Services PVT. LTD.";
                                            objDropContactDetailsList.phone_number = Convert.ToString(dtDropAddress.Rows[0]["CONTACTNO"]);

                                            objInstructionsList.Add(new InstructionsList
                                            {
                                                type = "text",
                                                description = lblMAKE.Text + " " + lblMODEL.Text + " " + lblRAM.Text + " " + lblROM.Text + " " + lblCOLOR.Text + " " + lblVENDORGRADE.Text
                                            });

                                            objPickUpAddress.contact_details = objPickUpContactDetailsList;
                                            objPickUpAddress.city = Convert.ToString(dtPickUpAddress.Rows[0]["CITY"]);
                                            objPickUpAddress.country = Convert.ToString(dtPickUpAddress.Rows[0]["COUNTRY"]);
                                            objPickUpAddress.landmark = Convert.ToString(dtPickUpAddress.Rows[0]["ADDR3"]);
                                            objPickUpAddress.lat = Convert.ToDouble(dtPickUpAddress.Rows[0]["LAT"]);
                                            objPickUpAddress.lng = Convert.ToDouble(dtPickUpAddress.Rows[0]["LONG"]);
                                            objPickUpAddress.pincode = Convert.ToString(dtPickUpAddress.Rows[0]["POSTALCODE"]);
                                            objPickUpAddress.state = Convert.ToString(dtPickUpAddress.Rows[0]["STATE"]);
                                            objPickUpAddress.apartment_address = Convert.ToString(dtPickUpAddress.Rows[0]["DEALERNAME"]);
                                            objPickUpAddress.street_address1 = Convert.ToString(dtPickUpAddress.Rows[0]["ADDR1"]);
                                            objPickUpAddress.street_address2 = Convert.ToString(dtPickUpAddress.Rows[0]["ADDR2"]);

                                            objDropAddress.contact_details = objDropContactDetailsList;
                                            objDropAddress.city = Convert.ToString(dtDropAddress.Rows[0]["CITY"]);
                                            objDropAddress.country = Convert.ToString(dtDropAddress.Rows[0]["COUNTRY"]);
                                            objDropAddress.landmark = Convert.ToString(dtDropAddress.Rows[0]["ADDR3"]);
                                            objDropAddress.lat = Convert.ToDouble(dtDropAddress.Rows[0]["LAT"]);
                                            objDropAddress.lng = Convert.ToDouble(dtDropAddress.Rows[0]["LONG"]);
                                            objDropAddress.pincode = Convert.ToString(dtDropAddress.Rows[0]["POSTALCODE"]);
                                            objDropAddress.state = Convert.ToString(dtDropAddress.Rows[0]["STATE"]);
                                            objDropAddress.apartment_address = "Qarmatek Services PVT. LTD.";
                                            objDropAddress.street_address1 = Convert.ToString(dtDropAddress.Rows[0]["ADDR1"]);
                                            objDropAddress.street_address2 = Convert.ToString(dtDropAddress.Rows[0]["ADDR2"]);


                                            objDeliveryInstructions.instructions_list = objInstructionsList;

                                            objPickupDetails.address = objPickUpAddress;

                                            objDropDetails.address = objDropAddress;

                                            objPorterClass.delivery_instructions = objDeliveryInstructions;
                                            objPorterClass.drop_details = objDropDetails;
                                            objPorterClass.pickup_details = objPickupDetails;
                                            objPorterClass.request_id = lblID.Text;



                                            DataTable dtAPIKEY = new DataTable();
                                            dtAPIKEY = objMainClass.GetWAData("PORTERAPI", 1, "GETWADATA");

                                            if (dtAPIKEY.Rows.Count > 0)
                                            {
                                                string KEYNAME = Convert.ToString(dtAPIKEY.Rows[0]["KEYNAME"]);
                                                string KEYVALUE = Convert.ToString(dtAPIKEY.Rows[0]["KEYVALUE"]);
                                                string URL = Convert.ToString(dtAPIKEY.Rows[0]["OTHER"]);
                                                //var client = new RestClient("https://pfe-apigw-uat.porter.in/v1/orders/create");
                                                var client = new RestClient(URL + "/v1/orders/create");

                                                client.Timeout = -1;
                                                var request = new RestRequest(Method.POST);
                                                request.AddHeader(KEYNAME, KEYVALUE);
                                                request.AddHeader("Content-Type", "application/json");

                                                var body = JsonConvert.SerializeObject(objPorterClass);

                                                request.AddParameter("application/json", body, ParameterType.RequestBody);
                                                IRestResponse response = client.Execute(request);
                                                if (Convert.ToString(response.StatusCode) == "Created")
                                                {
                                                    string jsonsend = response.Content;
                                                    PorterSuccess objPorterSuccess = new PorterSuccess();

                                                    objPorterSuccess = JsonConvert.DeserializeObject<PorterSuccess>(jsonsend);

                                                    //string orderid = Convert.ToString(objPorterSuccess.order_id);
                                                    //string requestid = Convert.ToString(objPorterSuccess.request_id);
                                                    //long estimatetimestamp = Convert.ToInt64(objPorterSuccess.estimated_pickup_time);
                                                    //string trackingurl = Convert.ToString(objPorterSuccess.tracking_url);
                                                    //int amt = objPorterSuccess.estimated_fare_details.minor_amount;

                                                    //System.DateTime dat_Time = new System.DateTime(1965, 1, 1, 0, 0, 0, 0);
                                                    //dat_Time = dat_Time.AddSeconds(estimatetimestamp / 100d);
                                                    //string estidate = dat_Time.ToShortDateString();
                                                    //string estitime = dat_Time.ToShortTimeString();



                                                    string orderid = Convert.ToString(objPorterSuccess.order_id);
                                                    string requestid = Convert.ToString(objPorterSuccess.request_id);
                                                    long estimatetimestamp = Convert.ToInt64(objPorterSuccess.estimated_pickup_time) / 1000;
                                                    //long estimatetimestamp = Convert.ToInt64(objPorterSuccess.estimated_pickup_time);
                                                    string trackingurl = Convert.ToString(objPorterSuccess.tracking_url);
                                                    int amt = (objPorterSuccess.estimated_fare_details.minor_amount) / 100;

                                                    //System.DateTime dat_Time = new System.DateTime(1965, 1, 1, 0, 0, 0, 0);
                                                    //dat_Time = dat_Time.AddSeconds(estimatetimestamp / 100d);
                                                    //string estidate = dat_Time.ToShortDateString();
                                                    //string estitime = dat_Time.ToShortTimeString();


                                                    DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(estimatetimestamp);
                                                    string[] datetime = (dateTimeOffset.ToString()).Split(new char[] { ' ', '+' });
                                                    string estidate = datetime[0];
                                                    string estitime = datetime[1];


                                                    int iResult = objMainClass.InsertLogisticDetail(Convert.ToString(dtPickUpAddress.Rows[0]["DEALERNAME"]),
                                                        Convert.ToString(dtPickUpAddress.Rows[0]["ADDR1"]), Convert.ToString(dtPickUpAddress.Rows[0]["CITY"]),
                                                        Convert.ToString(dtPickUpAddress.Rows[0]["STATE"]), Convert.ToString(dtPickUpAddress.Rows[0]["POSTALCODE"]),
                                                        Convert.ToString(dtPickUpAddress.Rows[0]["CONTACTNO"]), "", estidate, estitime, "Qarmatek Services PVT. LTD.",
                                                        Convert.ToString(dtDropAddress.Rows[0]["ADDR1"]), Convert.ToString(dtDropAddress.Rows[0]["CITY"]), Convert.ToString(dtDropAddress.Rows[0]["STATE"]),
                                                        Convert.ToString(dtDropAddress.Rows[0]["POSTALCODE"]), Convert.ToString(dtDropAddress.Rows[0]["CONTACTNO"]), "", "Pickup", "", requestid, Convert.ToInt32(Session["USERID"]),
                                                        requestid, lblMAKE.Text, lblMODEL.Text, "", Convert.ToString(dtPickUpAddress.Rows[0]["ADDR2"]), Convert.ToString(dtDropAddress.Rows[0]["ADDR2"]), "", requestid, "LISTING",
                                                        "", orderid, "BIKER", "PORTER", amt, "57", 0, trackingurl, "", orderid, "", 0, 11925);
                                                    if (iResult > 0)
                                                    {
                                                        DataTable dtSMS = new DataTable();
                                                        dtSMS = objMainClass.GetContactDetails(objMainClass.intCmpId, 1, "FETCHCONTACTDETAILS");
                                                        if (dtSMS.Rows.Count > 0)
                                                        {
                                                            if (Convert.ToString(dtSMS.Rows[0]["BSM"]) != "" && Convert.ToString(dtSMS.Rows[0]["BSM"]) != string.Empty && Convert.ToString(dtSMS.Rows[0]["BSM"]) != "")
                                                            {
                                                                WAClass objWAClass = new WAClass();
                                                                objWAClass.SendTextMessage("Pickup Request generated with Porter Courier Service. Listing ID is " + lblID.Text + ". Shop name id " + lblVENDORNAME.Text + ". Tracking No. is " + orderid + ". Tracking URL is " + trackingurl, "91" + Convert.ToString(dtSMS.Rows[0]["BSM"]), Convert.ToString(Session["USERID"]));
                                                            }
                                                        }
                                                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Pickup request generated in Porter and updated in ShERPa.!');", true);
                                                    }
                                                    else
                                                    {
                                                        if (listingIDs == string.Empty)
                                                        {
                                                            listingIDs = lblID.Text;
                                                        }
                                                        else
                                                        {
                                                            listingIDs = listingIDs + "," + lblID.Text;
                                                        }
                                                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Pickup request generated in Porter but not updated in ShERpa. Tracking URL is : " + objPorterSuccess.tracking_url.ToString() + " .Tracking No. is : " + objPorterSuccess.order_id.ToString() + "');", true);
                                                    }

                                                }
                                                else
                                                {
                                                    if (listingIDs == string.Empty)
                                                    {
                                                        listingIDs = lblID.Text;
                                                    }
                                                    else
                                                    {
                                                        listingIDs = listingIDs + "," + lblID.Text;
                                                    }
                                                    //string jsonsend = response.Content;
                                                    //PorterError objPorterError = new PorterError();
                                                    //objPorterError = JsonConvert.DeserializeObject<PorterError>(jsonsend);
                                                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('" + objPorterError.message.ToString() + "');", true);
                                                }

                                            }
                                            else
                                            {
                                                if (listingIDs == string.Empty)
                                                {
                                                    listingIDs = lblID.Text;
                                                }
                                                else
                                                {
                                                    listingIDs = listingIDs + "," + lblID.Text;
                                                }
                                                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('API key not Found!');", true);
                                            }

                                        }
                                        else
                                        {
                                            if (listingIDs == string.Empty)
                                            {
                                                listingIDs = lblID.Text;
                                            }
                                            else
                                            {
                                                listingIDs = listingIDs + "," + lblID.Text;
                                            }
                                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Drop to data not Found!');", true);
                                        }
                                    }
                                    else
                                    {
                                        if (listingIDs == string.Empty)
                                        {
                                            listingIDs = lblID.Text;
                                        }
                                        else
                                        {
                                            listingIDs = listingIDs + "," + lblID.Text;
                                        }
                                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Pickup from data not Found!');", true);
                                    }
                                }
                                else
                                {
                                    if (listingIDs == string.Empty)
                                    {
                                        listingIDs = lblID.Text;
                                    }
                                    else
                                    {
                                        listingIDs = listingIDs + "," + lblID.Text;
                                    }
                                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Pickup from longitude not given!');", true);
                                }
                            }
                            else
                            {
                                if (listingIDs == string.Empty)
                                {
                                    listingIDs = lblID.Text;
                                }
                                else
                                {
                                    listingIDs = listingIDs + "," + lblID.Text;
                                }
                                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Pickup from latitude not given!');", true);
                            }
                        }
                    }

                    if (listingIDs != string.Empty)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Pickup not generated for given Listing ID : " + listingIDs + "');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Pickup request generated for all selected listing id in Porter and updated in ShERPa.!');$('.close').click(function(){window.location.href ='frmListingAssign.aspx'});", true);
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

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    GridViewRow grdrow = (GridViewRow)((CheckBox)sender).NamingContainer;
                    CheckBox chkSelect = ((CheckBox)grdrow.FindControl("chkSelect"));
                    if (chkSelect.Checked == true)
                    {
                        Label lblLAT = (Label)grdrow.FindControl("lblLAT");
                        Label lblLONG = (Label)grdrow.FindControl("lblLONG");

                        if (lblLAT.Text != "" && lblLAT.Text != string.Empty && lblLAT.Text != null)
                        {
                            if (lblLONG.Text != "" && lblLONG.Text != string.Empty && lblLONG.Text != null)
                            {

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You cannot generate Pickup. Pickup from longitude not given!');", true);
                                chkSelect.Checked = false;
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You cannot generate Pickup. Pickup from latitude not given!');", true);
                            chkSelect.Checked = false;
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