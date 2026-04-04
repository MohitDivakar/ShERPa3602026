using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.CRM
{
    public partial class MasterCustomerRltionShipMng : System.Web.UI.MasterPage
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
                        lblUserName.InnerText = Convert.ToString(Session["USERNAME"]);
                        labelUserName.InnerText = Convert.ToString(Session["USERNAME"]);

                        if (Session["AGENTID"] != null && Convert.ToString(Session["AGENTID"]) != "" && Convert.ToString(Session["AGENTID"]) != string.Empty)
                        {
                            int iResult = objMainClass.GetPopupStatus(Convert.ToInt32(Session["USERID"]), 1, "GETPOPUPID");
                            if (iResult == 1)
                            {
                                BindTempDate();
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "setTimeout(function () { $('#modal-LiveLead').modal().fadeIn(1500); }, 30000);", true);
                            }
                        }

                        GetNotification();
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

        public void GetNotification()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetCRMNotificationData(0, 0, "GETCOUNT");
                    if (dt.Rows.Count > 0)
                    {
                        lblNotificationCnt.Text = Convert.ToString(dt.Rows[0]["CNT"]);
                    }
                    else
                    {
                        lblNotificationCnt.Text = "0";
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lnkHelp_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(
               this.GetType(), "OpenWindow", "window.open('../HelpViewer/frmLogin.aspx','_newtab');", true);
        }


        public void BindTempDate()
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    DataTable dt = new DataTable();
                    dt = objMainClass.GetLeadDataMethod("GETTEMPDATA");
                    if (dt.Rows.Count > 0)
                    {
                        dlPendingList.DataSource = dt;
                        dlPendingList.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal1", "setTimeout(function(){ window.location.reload(1); }, 90000);", true);

                    }
                    else
                    {
                        dlPendingList.DataSource = string.Empty;
                        dlPendingList.DataBind();
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



        protected void dlPendingList_ItemCommand(object source, DataListCommandEventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    HiddenField hfID = (HiddenField)(e.Item.FindControl("hfID"));
                    Label lblMobileNo = (Label)(e.Item.FindControl("lblMobileNo"));
                    Label lblFullName = (Label)(e.Item.FindControl("lblFullName"));

                    if (e.CommandName == "GrabIt")
                    {
                        DataTable dt = new DataTable();
                        dt = objMainClass.GetLeadStatusData(objMainClass.intCmpId, 0, (int)STATUS.Saved, "", "", "GETBYLEADID", Convert.ToInt32(hfID.Value));

                        if (dt.Rows.Count > 0)
                        {

                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-LiveLead').hide();", true);



                            int i = objMainClass.insertNewLead(dt.Rows[0]["CUSTUPDATEDATE"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dt.Rows[0]["CUSTUPDATEDATE"]),
                                dt.Rows[0]["CUSTNAME"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["CUSTNAME"]),
                                dt.Rows[0]["CONTACTNO"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["CONTACTNO"]),
                                dt.Rows[0]["EMAIL"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["EMAIL"]),
                                dt.Rows[0]["MAKE"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["MAKE"]),
                                dt.Rows[0]["MODEL"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["MODEL"]),
                                dt.Rows[0]["RAM"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["RAM"]),
                                dt.Rows[0]["ROM"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["ROM"]),
                                dt.Rows[0]["COLOR"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["COLOR"]),
                                dt.Rows[0]["PRICERANGE"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["PRICERANGE"]),
                                Convert.ToString(Session["USERID"]),
                                dt.Rows[0]["STATUS"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["STATUS"]),
                                dt.Rows[0]["ADDRESS"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["ADDRESS"]),
                                dt.Rows[0]["CREATEBY"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["CREATEBY"]),
                                dt.Rows[0]["CUSTREMARKS"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["CUSTREMARKS"]),
                                dt.Rows[0]["REFF"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["REFF"]),
                                dt.Rows[0]["CMPID"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["CMPID"]),
                                dt.Rows[0]["INQTYPE"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["INQTYPE"]),
                                dt.Rows[0]["REFFNO"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["REFFNO"]),
                                dt.Rows[0]["LEADTYPE"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["LEADTYPE"]),
                                dt.Rows[0]["PREFIX"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["PREFIX"]),
                                dt.Rows[0]["PHONE"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["PHONE"]),
                                dt.Rows[0]["CATEGORY"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["CATEGORY"]),
                                dt.Rows[0]["CITY"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["CITY"]),
                                dt.Rows[0]["AREA"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["AREA"]),
                                dt.Rows[0]["PINCODE"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["PINCODE"]),
                                dt.Rows[0]["DNCMOBILENO"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["DNCMOBILENO"]),
                                dt.Rows[0]["DNCPHONENO"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["DNCPHONENO"]),
                                dt.Rows[0]["COMPANYNAME"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["COMPANYNAME"]),
                                dt.Rows[0]["BRANCHAREA"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["BRANCHAREA"]),
                                dt.Rows[0]["BRANCHPIN"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["BRANCHPIN"]),
                                dt.Rows[0]["PARENTID"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["PARENTID"]),
                                dt.Rows[0]["REFID"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["REFID"]),
                                dt.Rows[0]["CITYID"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["CITYID"]),
                                dt.Rows[0]["STATEID"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["STATEID"]), 1,
                                dt.Rows[0]["CSID"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["CSID"]),
                                dt.Rows[0]["ISUPDATEDONLAKHU"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["ISUPDATEDONLAKHU"]),
                                dt.Rows[0]["LAKHUUPDATEDDATETIME"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dt.Rows[0]["LAKHUUPDATEDDATETIME"]),
                                "NEWINSERT");

                            if (i >= 0)
                            {
                                hfUpdateID.Value = Convert.ToString(i);
                                txtCustMobileNONew.Text = lblFullName.Text;
                                txtCustNewName.Text = lblMobileNo.Text;
                                int j = objMainClass.DeleteTempLead(objMainClass.intCmpId, Convert.ToInt32(hfID.Value), "DELETETEMP");
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-LiveCall').modal();$('#hfUpdateID').val('" + i + "');$('#txtCustMobileNONew').text('" + lblFullName.Text + "');$('#txtCustNewName').text('" + lblMobileNo.Text + "');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Lead generation failed.!');", true);
                            }

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Sorry..!! This Lead has been grab by another agent.!');", true);
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

        protected void btnCallAttent_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    if (Convert.ToString(Session["AGENTID"]) != null && Convert.ToString(Session["AGENTID"]) != "" && Convert.ToString(Session["AGENTID"]) != string.Empty)
                    {
                        string strResp = objMainClass.ClickToCall("0" + txtCustMobileNONew.Text, "CC", hfUpdateID.Value, Convert.ToString(Session["AGENTID"]));
                        if (strResp.Contains("ERROR:"))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + strResp + "\");", true);
                            Response.Redirect("CallLog.aspx", true);
                        }
                        else if (strResp.Contains("SUCCESS:"))
                        {
                            int iReturn = objMainClass.UpdateStartTime(objMainClass.intCmpId, Convert.ToInt32(hfUpdateID.Value), DateTime.Now.ToString(),
                                Convert.ToInt32(Session["USERID"]), "UPDATESTARTTIME");
                            if (iReturn != 1)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Start Time Not Updated.!');", true);
                            }
                            Response.Redirect("CallLog.aspx", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorized to call!');", true);
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

        protected void lnkPopContactSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    DataTable dt = new DataTable();
                    dt = objMainClass.CheckContactNo(objMainClass.intCmpId, txtPopContactNo.Text, "SEARCHTEMPCONTACT");

                    if (dt.Rows.Count > 0)
                    {
                        dlPendingList.DataSource = dt;
                        dlPendingList.DataBind();
                    }
                    else
                    {
                        dlPendingList.DataSource = string.Empty;
                        dlPendingList.DataBind();
                    }
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "setTimeout(function () { $('#modal-LiveLead').modal().fadeIn(); });", true);


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