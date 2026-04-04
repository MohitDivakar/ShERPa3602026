using Newtonsoft.Json;
using RestSharp;
using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ShERPa360net.CRM
{
    public partial class CallLog : System.Web.UI.Page
    {

        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        WAClass objWAClass = new WAClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["CallingIdDetail"] = string.Empty;
            //Session["CallingIdDetail"] = null;
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

                        if (Request.QueryString.Count > 0)
                        {
                            if (Convert.ToString(Request.QueryString["LeadID"]) != null && Convert.ToString(Request.QueryString["LeadID"]) != string.Empty && Convert.ToString(Request.QueryString["LeadID"]) != "")
                            {
                                Session["LeadID"] = Convert.ToString(Request.QueryString["LeadID"]);

                            }
                            Response.Redirect(Request.Url.AbsolutePath, false);
                        }
                        else
                        {

                            if (Convert.ToString(Session["LeadID"]) != null && Convert.ToString(Session["LeadID"]) != string.Empty && Convert.ToString(Session["LeadID"]) != "")
                            {

                            }
                            else
                            {
                                if (Convert.ToString(Session["fromcrmdate"]) != null && Convert.ToString(Session["fromcrmdate"]) != string.Empty && Convert.ToString(Session["fromcrmdate"]) != "")
                                {
                                    txtFromDate.Text = Convert.ToString(Session["fromcrmdate"]);

                                }
                                else
                                {
                                    txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();
                                }

                                if (Convert.ToString(Session["tocrmdate"]) != null && Convert.ToString(Session["tocrmdate"]) != string.Empty && Convert.ToString(Session["tocrmdate"]) != "")
                                {
                                    txtToDate.Text = Convert.ToString(Session["tocrmdate"]);
                                }
                                else
                                {
                                    txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();
                                }

                            }
                            txtPostponedDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //fromdate.ToString("dd-MM-yyyy");
                            txtPostponedTime.Text = objMainClass.indianTime.Date.ToString("hh\\:mm");
                            objBindDDL.FillLists(ddlReference, "IR");
                            objBindDDL.GetLeadProduct(ddlProduct, "GETLEADPRODUCT");
                            BindCallData();
                            Session["LeadID"] = null;
                            Session["LeadID"] = string.Empty;
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


        public void BindCallData()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    Session["fromcrmdate"] = txtFromDate.Text;
                    Session["tocrmdate"] = txtToDate.Text;

                    DataTable dt = new DataTable();
                    int LEADID = 0;
                    if (Convert.ToString(Session["LeadID"]) != null && Convert.ToString(Session["LeadID"]) != string.Empty && Convert.ToString(Session["LeadID"]) != "")
                    {
                        LEADID = Convert.ToInt32(Session["LeadID"]);
                    }
                    else
                    {
                        LEADID = 0;
                    }

                    dt = objMainClass.GetLeadStatusData(objMainClass.intCmpId, Convert.ToInt32(Session["USERID"]), (int)LeadStatus.Saved, txtFromDate.Text, txtToDate.Text, "GETSTATUSDATA", LEADID, ddlReference.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddlReference.SelectedValue),
                        txtContactNo.Text, ddlProduct.SelectedIndex == 0 ? "" : ddlProduct.SelectedItem.Text);
                    //dt = objMainClass.GetLeadStatusData(objMainClass.intCmpId, Convert.ToInt32(Session["USERID"]), (int)LeadStatus.Saved, "", "", "GETSTATUSDATA");

                    if (dt.Rows.Count > 0)
                    {
                        dlListSaved.DataSource = dt;
                        dlListSaved.DataBind();

                        //if (Convert.ToString(Session["LeadID"]) != null && Convert.ToString(Session["LeadID"]) != string.Empty && Convert.ToString(Session["LeadID"]) != "")
                        //{
                        //    tab_1.Attributes["class"] = "tab-pane active";
                        //    tab_2.Attributes["class"] = "tab-pane";
                        //    tab_3.Attributes["class"] = "tab-pane";
                        //    tab_4.Attributes["class"] = "tab-pane";
                        //}
                    }
                    else
                    {
                        dlListSaved.DataSource = string.Empty;
                        dlListSaved.DataBind();
                    }

                    //dlHList
                    DataTable dtHold = new DataTable();
                    dtHold = objMainClass.GetLeadStatusData(objMainClass.intCmpId, Convert.ToInt32(Session["USERID"]), (int)LeadStatus.OnHold, txtFromDate.Text, txtToDate.Text, "GETPOSTPONEDSTATUSDATA", LEADID, ddlReference.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddlReference.SelectedValue),
                        txtContactNo.Text, ddlProduct.SelectedIndex == 0 ? "" : ddlProduct.SelectedItem.Text);
                    //dtHold = objMainClass.GetLeadStatusData(objMainClass.intCmpId, Convert.ToInt32(Session["USERID"]), (int)LeadStatus.OnHold, "", "", "GETPOSTPONEDSTATUSDATA");

                    if (dtHold.Rows.Count > 0)
                    {
                        dlHList.DataSource = dtHold;
                        dlHList.DataBind();

                        //if (Convert.ToString(Session["LeadID"]) != null && Convert.ToString(Session["LeadID"]) != string.Empty && Convert.ToString(Session["LeadID"]) != "")
                        //{
                        //    tab_1.Attributes["class"] = "tab-pane";
                        //    tab_2.Attributes["class"] = "tab-pane active";
                        //    tab_3.Attributes["class"] = "tab-pane";
                        //    tab_4.Attributes["class"] = "tab-pane";
                        //}
                    }
                    else
                    {
                        dlHList.DataSource = string.Empty;
                        dlHList.DataBind();
                    }

                    DataTable dtCancel = new DataTable();
                    dtCancel = objMainClass.GetLeadStatusData(objMainClass.intCmpId, Convert.ToInt32(Session["USERID"]), (int)LeadStatus.Cancelled, txtFromDate.Text, txtToDate.Text, "GETSTATUSUPDATEDATA", LEADID, ddlReference.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddlReference.SelectedValue),
                        txtContactNo.Text, ddlProduct.SelectedIndex == 0 ? "" : ddlProduct.SelectedItem.Text);
                    //dtCancel = objMainClass.GetLeadStatusData(objMainClass.intCmpId, Convert.ToInt32(Session["USERID"]), (int)LeadStatus.Cancelled, DateTime.Now.ToString(), DateTime.Now.ToString(), "GETSTATUSUPDATEDATA");

                    if (dtCancel.Rows.Count > 0)
                    {
                        dlCList.DataSource = dtCancel;
                        dlCList.DataBind();

                        //if (Convert.ToString(Session["LeadID"]) != null && Convert.ToString(Session["LeadID"]) != string.Empty && Convert.ToString(Session["LeadID"]) != "")
                        //{
                        //    tab_1.Attributes["class"] = "tab-pane";
                        //    tab_2.Attributes["class"] = "tab-pane";
                        //    tab_3.Attributes["class"] = "tab-pane active";
                        //    tab_4.Attributes["class"] = "tab-pane";
                        //}
                    }
                    else
                    {
                        dlCList.DataSource = string.Empty;
                        dlCList.DataBind();
                    }

                    DataTable dtConverted = new DataTable();
                    dtConverted = objMainClass.GetLeadStatusData(objMainClass.intCmpId, Convert.ToInt32(Session["USERID"]), (int)LeadStatus.Converted, txtFromDate.Text, txtToDate.Text, "GETSTATUSUPDATEDATA", LEADID,
                        ddlReference.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddlReference.SelectedValue), txtContactNo.Text, ddlProduct.SelectedIndex == 0 ? "" : ddlProduct.SelectedItem.Text);
                    //dtConverted = objMainClass.GetLeadStatusData(objMainClass.intCmpId, Convert.ToInt32(Session["USERID"]), (int)LeadStatus.Converted, (DateTime.Now.AddMonths(-3)).ToString(), DateTime.Now.ToString(), "GETSTATUSUPDATEDATA");

                    if (dtConverted.Rows.Count > 0)
                    {
                        ConvertedList.DataSource = dtConverted;
                        ConvertedList.DataBind();

                        //if (Convert.ToString(Session["LeadID"]) != null && Convert.ToString(Session["LeadID"]) != string.Empty && Convert.ToString(Session["LeadID"]) != "")
                        //{
                        //    tab_1.Attributes["class"] = "tab-pane";
                        //    tab_2.Attributes["class"] = "tab-pane";
                        //    tab_3.Attributes["class"] = "tab-pane";
                        //    tab_4.Attributes["class"] = "tab-pane active";
                        //}
                    }
                    else
                    {
                        ConvertedList.DataSource = string.Empty;
                        ConvertedList.DataBind();
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

        protected void dlListSaved_ItemCommand(object source, DataListCommandEventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    HiddenField lblID = (HiddenField)(e.Item.FindControl("hfID"));
                    //i spancall = (i)(e.Item.FindControl("spancall"));
                    HtmlAnchor Anchor = (HtmlAnchor)e.Item.FindControl("spancall");
                    Label lblCallText = (Label)(e.Item.FindControl("lblCallText"));
                    Label lblFullName = (Label)(e.Item.FindControl("lblFullName"));



                    if (e.CommandName == "CallAttend")
                    {

                        if (Convert.ToString(Session["AGENTID"]) != null && Convert.ToString(Session["AGENTID"]) != "" && Convert.ToString(Session["AGENTID"]) != string.Empty)
                        {
                            //var client = new RestClient("http://192.168.1.103/agent/api.php?source=click_to_call&user=1001&pass=elision123&agent_user=1002&function=external_dial&value=8460591264&phone_code=&search=YES&preview=NO&focus=YES&vendor_id=CC&1&dial_prefix=&group_alias=");
                            //client.Timeout = -1;
                            //var request = new RestRequest(Method.POST);
                            //IRestResponse response = client.Execute(request);
                            //string jsonsend = response.Content;


                            //Label lblMobileNo = (Label)(e.Item.FindControl("lblMobileNo"));
                            string strResp = objMainClass.ClickToCall("0" + lblFullName.Text, "CC", lblID.Value, Convert.ToString(Session["AGENTID"]));
                            if (strResp.Contains("ERROR:"))
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + strResp + "\");", true);
                            }
                            else if (strResp.Contains("SUCCESS:"))
                            {
                                int iReturn = objMainClass.UpdateStartTime(objMainClass.intCmpId, Convert.ToInt32(lblID.Value), DateTime.Now.ToString(),
                                    Convert.ToInt32(Session["USERID"]), "UPDATESTARTTIME");
                                if (iReturn != 1)
                                {

                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Start Time Not Updated.!');", true);
                                }
                                BindCallData();
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorized to call!');", true);
                        }

                    }
                    else if (e.CommandName == "Hold")
                    {

                        objBindDDL.FillListsByValue(ddlHoldReason, "WCR", 1, "1", "EXTFIELDVALUE");
                        //ddlHoldReason.Items.Remove(ddlHoldReason.Items.FindByValue("223"));
                        //ddlHoldReason.Items.Remove(ddlHoldReason.Items.FindByValue("227"));
                        //ddlHoldReason.Items.Remove(ddlHoldReason.Items.FindByValue("299"));
                        //ddlHoldReason.Items.Remove(ddlHoldReason.Items.FindByValue("573"));
                        //ddlHoldReason.Items.Remove(ddlHoldReason.Items.FindByValue("574"));
                        hfHoldID.Value = lblID.Value;
                        hfHoldMobileNo.Value = ((Label)(e.Item.FindControl("lblFullName"))).Text;
                        hfHoldInqType.Value = ((Label)(e.Item.FindControl("lblInquiryType"))).Text;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Holdstatusupdate').modal();", true);
                    }
                    else if (e.CommandName == "Cancel")
                    {
                        objBindDDL.FillListsByValue(ddlCancelReason, "CR", 1, "1", "EXTFIELDVALUE");
                        //ddlCancelReason.Items.Remove(ddlCancelReason.Items.FindByValue("177")); ddlCancelReason.Items.Remove(ddlCancelReason.Items.FindByValue("185"));
                        //ddlCancelReason.Items.Remove(ddlCancelReason.Items.FindByValue("186")); ddlCancelReason.Items.Remove(ddlCancelReason.Items.FindByValue("187"));
                        //ddlCancelReason.Items.Remove(ddlCancelReason.Items.FindByValue("188")); ddlCancelReason.Items.Remove(ddlCancelReason.Items.FindByValue("214"));
                        //ddlCancelReason.Items.Remove(ddlCancelReason.Items.FindByValue("216")); ddlCancelReason.Items.Remove(ddlCancelReason.Items.FindByValue("230"));
                        //ddlCancelReason.Items.Remove(ddlCancelReason.Items.FindByValue("235")); ddlCancelReason.Items.Remove(ddlCancelReason.Items.FindByValue("236"));
                        //ddlCancelReason.Items.Remove(ddlCancelReason.Items.FindByValue("237")); ddlCancelReason.Items.Remove(ddlCancelReason.Items.FindByValue("238"));
                        //ddlCancelReason.Items.Remove(ddlCancelReason.Items.FindByValue("240")); ddlCancelReason.Items.Remove(ddlCancelReason.Items.FindByValue("271"));
                        //ddlCancelReason.Items.Remove(ddlCancelReason.Items.FindByValue("273")); ddlCancelReason.Items.Remove(ddlCancelReason.Items.FindByValue("309"));
                        //ddlCancelReason.Items.Remove(ddlCancelReason.Items.FindByValue("336")); ddlCancelReason.Items.Remove(ddlCancelReason.Items.FindByValue("337"));
                        //ddlCancelReason.Items.Remove(ddlCancelReason.Items.FindByValue("339")); ddlCancelReason.Items.Remove(ddlCancelReason.Items.FindByValue("340"));
                        //ddlCancelReason.Items.Remove(ddlCancelReason.Items.FindByValue("342")); ddlCancelReason.Items.Remove(ddlCancelReason.Items.FindByValue("343"));
                        //ddlCancelReason.Items.Remove(ddlCancelReason.Items.FindByValue("379")); ddlCancelReason.Items.Remove(ddlCancelReason.Items.FindByValue("380"));
                        //ddlCancelReason.Items.Remove(ddlCancelReason.Items.FindByValue("382"));
                        //ddlCancelReason.Items.Remove(ddlCancelReason.Items.FindByValue("484")); ddlCancelReason.Items.Remove(ddlCancelReason.Items.FindByValue("491"));
                        //ddlCancelReason.Items.Remove(ddlCancelReason.Items.FindByValue("223"));
                        hfCancelID.Value = lblID.Value;
                        hfCancelMobileNo.Value = ((Label)(e.Item.FindControl("lblFullName"))).Text;
                        hfCancelInqType.Value = ((Label)(e.Item.FindControl("lblInquiryType"))).Text;

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Cancelstatusupdate').modal();", true);
                    }
                    else if (e.CommandName == "CallDetails")
                    {
                        try
                        {
                            if (Session["USERID"] != null)
                            {
                                hfHisID.Value = lblID.Value;

                                DataTable dt = new DataTable();
                                dt = objMainClass.GetLeadStatusData(objMainClass.intCmpId, Convert.ToInt32(Session["USERID"]), (int)LeadStatus.Cancelled, "", "", "HISTORY", Convert.ToInt32(hfHisID.Value));
                                if (dt.Rows.Count > 0)
                                {

                                    lblHisContactNo.Text = Convert.ToString(dt.Rows[0]["CONTACTNO"]);
                                    lblHisCurrStatus.Text = Convert.ToString(dt.Rows[0]["STATUSDESC"]);
                                    lblHisCustName.Text = Convert.ToString(dt.Rows[0]["CUSTNAME"]);
                                    lblHisMail.Text = Convert.ToString(dt.Rows[0]["EMAIL"]);
                                    lblHisMake.Text = Convert.ToString(dt.Rows[0]["MAKE"]);
                                    lblHisModel.Text = Convert.ToString(dt.Rows[0]["MODEL"]);


                                    gvDetail.DataSource = dt;
                                    gvDetail.DataBind();
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-CallHistory').modal();", true);
                                }
                                else
                                {
                                    gvDetail.DataSource = string.Empty;
                                    gvDetail.DataBind();
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
                    else if (e.CommandName == "EditName")
                    {
                        hfUpdateID.Value = lblID.Value;
                        Label lblMobileNo = (Label)(e.Item.FindControl("lblMobileNo"));
                        txtCustNewName.Text = lblMobileNo.Text;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-EditName').modal();", true);

                    }
                    else if (e.CommandName == "ViewNotification")
                    {

                        DataTable dt = new DataTable();
                        dt = objMainClass.GetCRMNotificationData(1, Convert.ToInt32(lblID.Value), "GETCALLTONOTIDATA");
                        if (dt.Rows.Count > 0)
                        {
                            gvList.DataSource = dt;
                            gvList.DataBind();
                            gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-notification').modal();", true);
                        }
                        else
                        {
                            gvList.DataSource = string.Empty;
                            gvList.DataBind();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
                        }


                    }
                    else if (e.CommandName == "EnterSO")
                    {
                        hfSOUpdateID.Value = lblID.Value;
                        Label lblMobileNo = (Label)(e.Item.FindControl("lblMobileNo"));
                        txtCustNewName.Text = lblMobileNo.Text;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-EnterSO').modal();", true);
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

        protected void btnHoldSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (hfHoldID.Value != null && hfHoldID.Value != "" && hfHoldID.Value != string.Empty)
                    {
                        int iResult = objMainClass.UpdateLeadData(Convert.ToInt32(hfHoldID.Value), (int)LeadStatus.OnHold, Convert.ToInt32(Session["USERID"]), txtQTEKHoldRemarks.Text,
                            txtCustHoldRemark.Text, "", Convert.ToInt32(ddlHoldReason.SelectedValue), DateTime.Parse(txtPostponedDate.Text), TimeSpan.Parse(txtPostponedTime.Text),
                            "HOLDUPDATE", DateTime.Now.ToString(), 0);
                        if (iResult == 1)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Call updated successfully!');$('.close').click(function(){window.location.href ='CallLog.aspx' });", true);
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Call updated successfully!');", true);
                            //BindCallData();
                            //SendWAMessage(hfHoldMobileNo.Value, "Dear Customer, " + System.Environment.NewLine + "Your " + hfHoldInqType.Value + " is on hold. Reason is " + ddlHoldReason.SelectedItem.Text + "." + System.Environment.NewLine + " Our customer representative will call on " + txtPostponedDate.Text + " @ " + txtPostponedTime.Text + "." + System.Environment.NewLine + "Qarmatek");
                            ddlHoldReason.SelectedIndex = 0;
                            txtCustHoldRemark.Text = string.Empty;
                            txtQTEKHoldRemarks.Text = string.Empty;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Please try again!');$('.close').click(function(){window.location.href ='CallLog.aspx' });", true);
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Please try again!');", true);
                            //BindCallData();
                            ddlHoldReason.SelectedIndex = 0;
                            txtCustHoldRemark.Text = string.Empty;
                            txtQTEKHoldRemarks.Text = string.Empty;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('ID not fetched. Contact to administrator!');", true);
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (hfCancelID.Value != null && hfCancelID.Value != "" && hfCancelID.Value != string.Empty)
                    {
                        int iResult = objMainClass.CancelUpdateLeadData(Convert.ToInt32(hfCancelID.Value), (int)LeadStatus.Cancelled, Convert.ToInt32(Session["USERID"]), txtQTEKCancelRemarks.Text,
                            txtCustCancelReamarka.Text, "", Convert.ToInt32(ddlCancelReason.SelectedValue), "CANCELUPDATE", DateTime.Now.ToString(), 0);
                        if (iResult == 1)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Call updated successfully!');$('.close').click(function(){window.location.href ='CallLog.aspx' });", true);
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Call updated successfully!');", true);
                            //BindCallData();
                            //SendWAMessage(hfCancelMobileNo.Value, "Dear Customer, " + System.Environment.NewLine + "Your " + hfCancelInqType.Value + " is cancelled. Reason is " + ddlCancelReason.SelectedItem.Text + "." + System.Environment.NewLine + "Qarmatek");
                            ddlCancelReason.SelectedIndex = 0;
                            txtCustHoldRemark.Text = string.Empty;
                            txtQTEKHoldRemarks.Text = string.Empty;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Please try again!');$('.close').click(function(){window.location.href ='CallLog.aspx' });", true);
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Please try again!');", true);
                            //BindCallData();
                            ddlCancelReason.SelectedIndex = 0;
                            txtCustHoldRemark.Text = string.Empty;
                            txtQTEKHoldRemarks.Text = string.Empty;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('ID not fetched. Contact to administrator!');", true);
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


        public void SendWAMessage(string MOBILENO, string MSGTEXT)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (objWAClass.CheckWAConnection() == true)
                    {
                        SendTextMessage(MOBILENO, MSGTEXT);

                    }
                    else
                    {
                        if (objWAClass.ReConnectWA() == true)
                        {
                            if (objWAClass.CheckWAConnection() == true)
                            {
                                SendTextMessage(MOBILENO, MSGTEXT);

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Device not connected. Please connect device to instance.!');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Device not connected. Please connect device to instance.!');", true);
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

        private void SendTextMessage(string MOBILENO, string MSGTEXT)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    var client = new RestClient("https://console.wa0.in/api/send.php?client_id=" + objWAClass.client_id + "&instance=" + objWAClass.instance + "&number=91" + MOBILENO + "&message=" + MSGTEXT + "&type=text");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    IRestResponse response = client.Execute(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string jsonsend = response.Content;
                        jsonsend = "[" + jsonsend + "]";
                        DataTable dtValuesend = (DataTable)JsonConvert.DeserializeObject(jsonsend, (typeof(DataTable)));
                        if (Convert.ToString(dtValuesend.Rows[0]["status"]) == objWAClass.statusTrue || Convert.ToString(dtValuesend.Rows[0]["status"]) == objWAClass.msgQue)
                        {
                            objMainClass.WALOG(objMainClass.intCmpId, MSGTEXT, MOBILENO, Convert.ToString(Session["USERID"]), "");

                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Mesage sent successfully.');", true);

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"" + Convert.ToString(dtValuesend.Rows[0]["message"]) + "\");", true);
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + response.ErrorMessage + "\");", true);
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

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    BindCallData();
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

        protected void btnNameUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    if (hfUpdateID.Value != null && hfUpdateID.Value != "" && hfUpdateID.Value != string.Empty)
                    {
                        int iResult = objMainClass.UpdateCustName(objMainClass.intCmpId, Convert.ToInt32(hfUpdateID.Value), txtCustNewName.Text, "UPDATENAME");
                        if (iResult == 1)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Customer Name updated successfully!');$('.close').click(function(){window.location.href ='CallLog.aspx' });", true);
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Customer Name updated successfully!');", true);
                            //BindCallData();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Please try again!');$('.close').click(function(){window.location.href ='CallLog.aspx' });", true);
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Please try again!');", true);
                            //BindCallData();
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

        protected void btnSOUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    if (hfSOUpdateID.Value != null && hfSOUpdateID.Value != "" && hfSOUpdateID.Value != string.Empty)
                    {
                        int iLeadResult = objMainClass.UpdateLeadInqNo(Convert.ToInt32(hfSOUpdateID.Value), objMainClass.strConvertZeroPadding(txtSONO.Text), (int)LeadStatus.Converted, Convert.ToInt32(Session["USERID"]), "INSERTSO");
                        //int iResult = objMainClass.UpdateCustName(objMainClass.intCmpId, Convert.ToInt32(hfUpdateID.Value), txtCustNewName.Text, "UPDATENAME");
                        if (iLeadResult == 1)
                        {
                            txtSONO.Text = string.Empty;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('SO Number updated successfully!');$('.close').click(function(){window.location.href ='CallLog.aspx' });", true);
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('SO Number updated successfully!');", true);
                            //BindCallData();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Please try again!');$('.close').click(function(){window.location.href ='CallLog.aspx' });", true);
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Please try again!');", true);
                            //BindCallData();
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('SO number not updated.!');", true);
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

        protected void txtSONO_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    lblSOError.Visible = false;
                    btnSOUpdate.Enabled = true;
                    if (txtSONO.Text != null && txtSONO.Text != "" && txtSONO.Text != string.Empty)
                    {
                        bool checkSO = objMainClass.CheckSOnumber(txtSONO.Text);
                        if (checkSO == true)
                        {
                            lblSOError.Visible = false;
                            btnSOUpdate.Enabled = true;
                        }
                        else
                        {
                            lblSOError.Text = "Invalid SO Number.";
                            lblSOError.Visible = true;
                            btnSOUpdate.Enabled = false;
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid SO number. Please enter correct SO Number.!');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('SO number not updated.!');", true);
                    }
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-EnterSO').modal();", true);
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