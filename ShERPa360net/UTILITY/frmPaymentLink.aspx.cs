using Razorpay.Api;
using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.UTILITY
{
    public partial class frmPaymentLink : System.Web.UI.Page
    {

        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        WAClass objWAClass = new WAClass();

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
                        DataTable dtAPI = new DataTable();
                        dtAPI = objMainClass.GetWAData("RAZORPAYAPI", 1, "GETWADATA");
                        string KEY = Convert.ToString(dtAPI.Rows[0]["KEYNAME"]);
                        string SECRET = Convert.ToString(dtAPI.Rows[0]["KEYVALUE"]);

                        RazorpayClient client = new RazorpayClient(KEY, SECRET);

                        string paymentLinkId = "plink_PQkId3q09FUQIf";

                        PaymentLink paymentlink = client.PaymentLink.Fetch(paymentLinkId);
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

        protected void imgSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    divLink.Visible = true;
                    lblLink.Text = string.Empty;

                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    DataTable dtAPI = new DataTable();
                    dtAPI = objMainClass.GetWAData("RAZORPAYAPI", 1, "GETWADATA");
                    string refid = txtCustName.Text.Substring(0, 3) + "" + txtAmount.Text + "" + DateTime.Now.Year + "" + DateTime.Now.Month + "" + DateTime.Now.Day + "" + DateTime.Now.Hour + "" + DateTime.Now.Minute + "" + DateTime.Now.Second;
                    if (dtAPI.Rows.Count > 0)
                    {
                        DateTime currentTime = DateTime.Now.AddHours(24);
                        long unixTime = ((DateTimeOffset)currentTime).ToUnixTimeSeconds();

                        string KEY = Convert.ToString(dtAPI.Rows[0]["KEYNAME"]);
                        string SECRET = Convert.ToString(dtAPI.Rows[0]["KEYVALUE"]);

                        RazorpayClient client = new RazorpayClient(KEY, SECRET);

                        Dictionary<string, object> paymentLinkRequest = new Dictionary<string, object>();
                        paymentLinkRequest.Add("amount", Convert.ToDecimal(txtAmount.Text) * 100);
                        paymentLinkRequest.Add("currency", "INR");
                        paymentLinkRequest.Add("accept_partial", false);
                        //paymentLinkRequest.Add("first_min_partial_amount", 100);
                        paymentLinkRequest.Add("expire_by", unixTime);
                        //paymentLinkRequest.Add("reference_id", "TS1989");
                        paymentLinkRequest.Add("reference_id", txtrefno.Text);
                        paymentLinkRequest.Add("description", txtDescription.Text);

                        Dictionary<string, object> customer = new Dictionary<string, object>();
                        customer.Add("contact", "+91" + txtContactNo.Text);
                        customer.Add("name", txtCustName.Text);
                        customer.Add("email", txtEmailID.Text);
                        paymentLinkRequest.Add("customer", customer);

                        Dictionary<string, object> notify = new Dictionary<string, object>();
                        notify.Add("sms", true);
                        notify.Add("email", true);
                        paymentLinkRequest.Add("notify", notify);
                        paymentLinkRequest.Add("reminder_enable", true);

                        Dictionary<string, object> notes = new Dictionary<string, object>();
                        notes.Add("remarks", txtRemarks.Text);
                        paymentLinkRequest.Add("notes", notes);

                        PaymentLink paymentlink = client.PaymentLink.Create(paymentLinkRequest);

                        string paymentlnk = Convert.ToString(paymentlink.Attributes["short_url"]);
                        string pmtID = Convert.ToString(paymentlink.Attributes["id"]);
                        if (paymentlnk != null && paymentlnk != "" && paymentlnk != string.Empty)
                        {
                            lblLink.Text = paymentlnk;
                            divLink.Visible = true;
                            string envi = "";

                            if (KEY.Contains("test"))
                            {
                                envi = "TEST";
                            }
                            if (KEY.Contains("live"))
                            {
                                envi = "LIVE";
                            }

                            int i = objMainClass.InsertPaymentLink(objMainClass.intCmpId, txtCustName.Text, txtContactNo.Text, txtEmailID.Text, txtDescription.Text, Convert.ToDecimal(txtAmount.Text), txtrefno.Text,
                                txtRemarks.Text, currentTime, paymentlnk, "", envi, "RAZOR PAY", Convert.ToInt32(Session["USERID"]), pmtID, "INSERTDATA");

                            ClearData();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Payment Link not Generated. Please contact admin.');", true);
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Payment Link Generation API not found. Please contact API admin.');", true);
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

        public void ClearData()
        {
            txtCustName.Text = string.Empty;
            txtContactNo.Text = string.Empty;
            txtEmailID.Text = string.Empty;
            txtAmount.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtrefno.Text = string.Empty;
        }

        protected void imgCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    txtCustName.Text = string.Empty;
                    txtContactNo.Text = string.Empty;
                    txtEmailID.Text = string.Empty;
                    txtAmount.Text = string.Empty;
                    txtDescription.Text = string.Empty;
                    txtRemarks.Text = string.Empty;
                    txtrefno.Text = string.Empty;
                    lblLink.Text = string.Empty;
                    divLink.Visible = false;
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