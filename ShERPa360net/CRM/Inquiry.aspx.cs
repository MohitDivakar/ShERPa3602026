using ShERPa360net.Class;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Transactions;

namespace ShERPa360net.CRM
{
    public partial class Inquiry : System.Web.UI.Page
    {
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        DALInquiry objDALInquiry = new DALInquiry();
        DALEstInq objDALEstInq = new DALEstInq();
        DALJobsheet objDALJobsheet = new DALJobsheet();
        DALCustReg objDALCustReg = new DALCustReg();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menutabid.Value, "");
                    if (!IsPostBack)
                    {
                        try
                        {
                            if (FormRights.bView == false)
                            {
                                Session.Abandon();
                                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                                return;
                            }
                            if (FormRights.bAdd == false)
                            {

                                btnSave.Enabled = false;
                            }

                            if (Request.QueryString.Count > 0)
                            {
                                if (Request.QueryString["InqNo"] != null)
                                {
                                    Session["InqNo"] = Request.QueryString["InqNo"];
                                    Session["CN"] = Session["CID"] = Session["CMK"] = Session["CMD"] = Session["SEG"] = null;
                                }
                                else if (Request.QueryString["CID"] != null)
                                {
                                    Session["CallingIdDetail"] = Request.QueryString["CID"];
                                    Session["CN"] = Session["CID"] = Session["CMK"] = Session["CMD"] = Session["SEG"] = null;
                                }
                                else
                                {
                                    Session["InqNo"] = null;
                                    Session["CN"] = Request.QueryString["CN"];
                                    Session["CID"] = Request.QueryString["CID"];
                                    Session["CMK"] = Request.QueryString["CMK"];
                                    Session["CMD"] = Request.QueryString["CMD"];
                                    Session["SEG"] = Request.QueryString["SEG"];
                                }
                                Response.Redirect(Request.Url.AbsolutePath, false);
                            }
                            else
                            {
                                txtPostCode.Attributes.Add("onblur", "OnLeavePincode();");
                                objBindDDL.FillSegment(ddlSegment);
                                objBindDDL.FillDistChnl(ddlDistChnl);
                                objBindDDL.FillLists(ddlRef, "IR");
                                if (Session["CN"] != null)
                                {
                                    if (Convert.ToString(Session["SEG"]) != "1003")
                                    {
                                        //objBindDDL.FillBrand(ddlBrand, 0);
                                    }
                                    else
                                    {
                                        txtCardNo.Text = MainClass.CommonQTEKCardNo;
                                        //objBindDDL.FillBrand(ddlBrand, 1);
                                    }
                                }
                                else if (Session["InqNo"] != null)
                                {
                                    //objBindDDL.FillBrand(ddlBrand, 0);
                                }
                                else
                                {
                                    //objBindDDL.FillBrand(ddlBrand, 1);
                                }
                                objBindDDL.FillBrand(ddlBrand, 0);
                                objBindDDL.FillSimCarrier(ddlSimCarrier);
                                objBindDDL.FillState(ddlState);
                                objBindDDL.FillPaymentMode(ddlPayMode);
                                //objBindDDL.FillParts(ddlParts);
                                //objBindDDL.FillLists(ddlColor, "CL");
                                //objBindDDL.FillApproved(ddlApproved);
                                objBindDDL.FillArea(ddlArea);
                                objBindDDL.FillItemByCat(ddlProduct, "13");
                                ddlProduct.SelectedValue = "1";
                                ddlSegment.SelectedValue = "1003";
                                ddlDistChnl.SelectedValue = "30";
                                txtInquiryNo.Text = objDALInquiry.SELECT_MAX_ORDERID(ddlSegment.SelectedValue).ToString();
                                txtInqDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");
                                //txtPickupDt.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");

                                ddlRef.SelectedValue = Session["ROLEID"].ToString() == "18" ? "284" : "0"; // ASCENDUM OR CALL CENTER UPSALE
                                ddlSegment.Enabled = ddlDistChnl.Enabled = (Session["ROLEID"].ToString() == "18" || Session["ROLEID"].ToString() == "21") ? false : true; //ASCENDUM
                                if (Session["InqNo"] == null)
                                {
                                    ddlSegment.Items.Remove(ddlSegment.Items.FindByValue("1001"));
                                    ddlSegment.Items.Remove(ddlSegment.Items.FindByValue("1004"));
                                    //ddlSegment.Items.Remove(ddlSegment.Items.FindByValue("1005"));
                                    ddlSegment.Items.Remove(ddlSegment.Items.FindByValue("1007"));
                                    ddlSegment.Items.Remove(ddlSegment.Items.FindByValue("1008"));
                                    //ddlSegment.Items.Remove(ddlSegment.Items.FindByValue("1009"));
                                    ddlSegment.Items.Remove(ddlSegment.Items.FindByValue("1010"));
                                    ddlSegment.Items.Remove(ddlSegment.Items.FindByValue("1011"));
                                    //ddlSegment.Items.Remove(ddlSegment.Items.FindByValue("1012"));
                                    ddlSegment.Items.Remove(ddlSegment.Items.FindByValue("1013"));
                                    ddlSegment.Items.Remove(ddlSegment.Items.FindByValue("1014"));
                                    ddlSegment.Items.Remove(ddlSegment.Items.FindByValue("1099"));
                                }

                                btnSave.Enabled = FormRights.bAdd == true ? true : false;
                                if (Session["InqNo"] != null || Session["EstInqId"] != null)
                                {
                                    btnHold.Enabled = FormRights.bEdit == true ? true : false;
                                    ddlBrand.Enabled = Session["ROLEID"].ToString() == "1" ? true : false;
                                    if (txtTXNId.Text != "" && txtTXNId.Text != "0")
                                    {
                                        btnHold.Enabled = false;
                                    }
                                }
                                ddlDistChnl.Enabled = false;
                                if (Session["CallingIdDetail"] != null)
                                {
                                    DataTable dtLeadData = new DataTable();
                                    dtLeadData = objMainClass.GetLeadStatusData(objMainClass.intCmpId, 0, 0, "", "", "GETLEADDATA", Convert.ToInt32(Session["CallingIdDetail"]));
                                    if (dtLeadData.Rows.Count > 0)
                                    {
                                        int BrandID = objMainClass.BRANDID(Convert.ToString(dtLeadData.Rows[0]["MAKE"]));
                                        if (BrandID > 0)
                                        {
                                            ddlBrand.SelectedValue = Convert.ToString(BrandID);
                                            objBindDDL.FillModel(ddlModel, ddlBrand.SelectedValue);
                                            int ModelID = objMainClass.MODELID(Convert.ToString(dtLeadData.Rows[0]["MODEL"]));
                                            if (ModelID > 0)
                                            {
                                                ddlModel.SelectedValue = Convert.ToString(ModelID);
                                            }
                                            else
                                            {
                                                txtModelName.Text = Convert.ToString(dtLeadData.Rows[0]["MODEL"]);
                                            }
                                        }
                                        txtName.Text = Convert.ToString(dtLeadData.Rows[0]["CUSTNAME"]);
                                        txtMobileNo.Text = Convert.ToString(dtLeadData.Rows[0]["CONTACTNO"]);
                                        txtEmailId.Text = Convert.ToString(dtLeadData.Rows[0]["EMAIL"]);
                                        txtProblems.Text = Convert.ToString(dtLeadData.Rows[0]["CUSTREMARKS"]);
                                        //ddlRef.SelectedItem.Text = Convert.ToString(dtLeadData.Rows[0]["REFF"]);
                                        ddlLeads.SelectedValue = "Outbound";
                                        ddlRef.SelectedIndex = ddlRef.Items.IndexOf(ddlRef.Items.FindByText(dtLeadData.Rows[0]["REFF"].ToString()));
                                        rfvCardNo.Enabled = false;


                                    }
                                    else
                                    {
                                        txtPickupDt.Enabled = true;
                                        dtpPickupTime.Enabled = true;
                                        ddlSegment.SelectedValue = Convert.ToString(Session["SEG"]);
                                        ddlDistChnl.SelectedValue = ddlSegment.SelectedValue == "1003" ? "30" : "40";
                                        txtMobileNo.Text = Session["CN"].ToString();
                                        ddlBrand.SelectedValue = Session["CMK"].ToString();
                                        ddlBrand_SelectedIndexChanged(sender, e);
                                        string strModel = Session["CMD"].ToString();
                                        if (ddlModel.Items.FindByText(strModel) != null)
                                        {
                                            ddlModel.SelectedIndex = ddlModel.Items.IndexOf(ddlModel.Items.FindByText(strModel));
                                        }
                                        else
                                        {
                                            ddlModel.SelectedIndex = ddlModel.Items.IndexOf(ddlModel.Items.FindByText("OTHER, MODEL NOT IN LIST"));
                                            txtModelName.Text = strModel;
                                        }
                                    }
                                }
                                else if (Session["CN"] != null)
                                {
                                    txtPickupDt.Enabled = true;
                                    dtpPickupTime.Enabled = true;
                                    ddlSegment.SelectedValue = Convert.ToString(Session["SEG"]);
                                    ddlDistChnl.SelectedValue = ddlSegment.SelectedValue == "1003" ? "30" : "40";
                                    txtMobileNo.Text = Session["CN"].ToString();
                                    //btnJobSheet.Enabled = false;
                                    ddlBrand.SelectedValue = Session["CMK"].ToString();
                                    ddlBrand_SelectedIndexChanged(sender, e);
                                    string strModel = Session["CMD"].ToString();
                                    if (ddlModel.Items.FindByText(strModel) != null)
                                    {
                                        ddlModel.SelectedIndex = ddlModel.Items.IndexOf(ddlModel.Items.FindByText(strModel));
                                    }
                                    else
                                    {
                                        ddlModel.SelectedIndex = ddlModel.Items.IndexOf(ddlModel.Items.FindByText("OTHER, MODEL NOT IN LIST"));
                                        txtModelName.Text = strModel;
                                    }
                                    //ddlSegment_SelectedIndexChanged(sender, e);
                                }
                                else if (Session["InqNo"] != null)
                                {
                                    GetData();
                                }
                                else if (Session["EstInqId"] != null)
                                {
                                    DataTable dt = new DataTable();
                                    dt = objDALEstInq.SELECT_ESTINQUIRY_ID(Session["EstInqId"].ToString());
                                    txtName.Text = dt.Rows[0]["CUSTNAME"].ToString();
                                    txtMobileNo.Text = dt.Rows[0]["MOBILENO"].ToString();
                                    txtProblems.Text = dt.Rows[0]["HANDSETPRBLM"].ToString();
                                    ddlBrand.SelectedValue = dt.Rows[0]["BRANDID"].ToString();
                                    objBindDDL.FillModel(ddlModel, ddlBrand.SelectedValue);
                                    ddlModel.SelectedValue = dt.Rows[0]["MODELID"].ToString();
                                    if (ddlModel.SelectedItem.Text.ToUpper().Contains("OTHER"))
                                    {
                                        txtModelName.Enabled = true;
                                        txtModelName.Text = dt.Rows[0]["MODELNAME"].ToString();
                                    }
                                    else
                                    {
                                        txtModelName.Enabled = false;
                                    }
                                }

                                if (ddlPayMode.SelectedIndex == 0)
                                {
                                    ddlPayMode.SelectedValue = ddlSegment.SelectedValue == "1003" ? "8" : "1";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            btnSave.Enabled = false;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message.Replace("\r\n", "") + "\");", true);
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
                throw ex;
            }
        }

        public void GetData()
        {
            try
            {
                DataTable objDt = new DataTable();
                string strInqNo = Session["InqNo"].ToString();
                objDt = objDALInquiry.SELECT_TRAN_INQMST_BYINQNO(strInqNo);
                //objDt = objMainClass.SELECT_TRAN_INQMST_BYINQNO("9000015460");
                if (objDt.Rows.Count > 0)
                {
                    //ViewState["FLAG"] = "Update";
                    ddlProduct.SelectedValue = objDt.Rows[0]["ITEMID"].ToString();
                    objBindDDL.FillBrandByItem(ddlBrand, ddlProduct.SelectedValue);
                    ddlLeads.SelectedValue = objDt.Rows[0]["LEADS"].ToString();
                    ddlSegment.SelectedValue = objDt.Rows[0]["SEGMENT"].ToString();
                    ddlDistChnl.SelectedValue = objDt.Rows[0]["DISTCHNL"].ToString();
                    ddlSegment.Enabled = ddlDistChnl.Enabled = false;
                    btnCallHistory.Visible = true;
                    txtInquiryNo.Text = objDt.Rows[0]["ORDER_ID"].ToString();
                    txtCardNo.Text = objDt.Rows[0]["COUPANNO"].ToString();
                    ddlRef.SelectedIndex = ddlRef.Items.IndexOf(ddlRef.Items.FindByText(objDt.Rows[0]["REF"].ToString()));
                    txtComments.Text = objDt.Rows[0]["COMMENT"].ToString().ToUpper();
                    ddlBrand.SelectedValue = objDt.Rows[0]["BRAND_ID"].ToString();
                    //objBindDDL.FillModel(ddlModel, ddlBrand.SelectedValue);
                    objBindDDL.FillModelByItem(ddlModel, ddlBrand.SelectedValue, ddlProduct.SelectedValue);
                    ddlModel.SelectedValue = objDt.Rows[0]["MODEL_ID"].ToString();
                    txtModelName.Text = objDt.Rows[0]["MODELNAME"].ToString();

                    if (txtModelName.Text != "")
                    {
                        ddlModel.SelectedIndex = ddlModel.Items.IndexOf(ddlModel.Items.FindByText("OTHER, MODEL NOT IN LIST"));
                    }
                    txtModelName.Enabled = ddlModel.SelectedItem.Text.Contains("OTHER,") ? true : false;

                    if (!string.IsNullOrEmpty(objDt.Rows[0]["CARRIER_ID"].ToString()))
                    {
                        ddlSimCarrier.SelectedValue = objDt.Rows[0]["CARRIER_ID"].ToString();
                    }

                    txtIMEINo.Text = objDt.Rows[0]["IMEINO"].ToString();
                    txtDevicePass.Text = objDt.Rows[0]["PASSCODE"].ToString();

                    txtServiceCntrName.Text = objDt.Rows[0]["OTHASC"].ToString();
                    if (!string.IsNullOrEmpty(txtServiceCntrName.Text))
                    {
                        chkServiceCntr.Checked = true;
                    }
                    else
                    {
                        chkServiceCntr.Checked = false;
                    }
                    txtName.Text = objDt.Rows[0]["FULLNAME"].ToString();
                    //txtCompanyName.Text = objDt.Rows[0]["COMPANYNAME"].ToString();
                    txtMobileNo.Text = objDt.Rows[0]["MOB_NO"].ToString();
                    txtAltMobNo.Text = objDt.Rows[0]["LANDLINE"].ToString();
                    txtEmailId.Text = objDt.Rows[0]["EMAILID"].ToString();
                    txtAddr1.Text = objDt.Rows[0]["ADDRESS1"].ToString();
                    txtAddr2.Text = objDt.Rows[0]["ADDRESS2"].ToString();
                    ddlState.SelectedValue = objDt.Rows[0]["STATE_ID"].ToString();
                    objBindDDL.FillCity(ddlCity, ddlState.SelectedValue);
                    ddlCity.SelectedValue = objDt.Rows[0]["CITY_ID"].ToString();
                    if (ddlCity.SelectedIndex == 0)
                    {
                        ddlCity.SelectedIndex = ddlCity.Items.IndexOf(ddlCity.Items.FindByText(Convert.ToString(objDt.Rows[0]["CITYNAME"])));
                    }
                    txtLandmark.Text = objDt.Rows[0]["LANDMARK"].ToString();
                    txtPostCode.Text = objDt.Rows[0]["POSTCODE"].ToString();

                    txtPostCode_TextChanged(this, new EventArgs());
                    ddlArea.SelectedValue = objDt.Rows[0]["AREAID"].ToString();

                    if (Session["ROLEID"].ToString() != "1" && Session["ROLEID"].ToString() != "7") // ADMIN, CALL CENTER INCHARGE
                    {
                        txtCardNo.Enabled = false;
                        if (Session["ROLEID"].ToString() != "21") // ASCENDUM MANAGER
                        {
                            ddlRef.Enabled = ddlRef.SelectedIndex > 0 ? false : true;
                        }
                    }
                    txtPickupDt.Text = objDt.Rows[0]["PICKUPDATE"].ToString();
                    txtInqDate.Text = objDt.Rows[0]["CREATEDATE"].ToString();
                    string strCallId = string.IsNullOrEmpty(objDt.Rows[0]["CALLID"].ToString()) == true ? "0" : objDt.Rows[0]["CALLID"].ToString();
                    if (!string.IsNullOrEmpty(objDt.Rows[0]["PICKUPTIME"].ToString()))
                    {
                        TimeSpan ts = TimeSpan.Parse(objDt.Rows[0]["PICKUPTIME"].ToString());
                        dtpPickupTime.Text = ts.ToString("hh\\:mm");
                    }
                    string strOtherPrblms = objDt.Rows[0]["OTHERPRBLMS"].ToString();
                    string strPrblmSummry = objDt.Rows[0]["PRBLMSUMMARY"].ToString();

                    if (strPrblmSummry.Trim() != strOtherPrblms.Trim())
                    {
                        txtProblems.Text = strPrblmSummry + " " + strOtherPrblms;
                    }
                    else
                    {
                        txtProblems.Text = strOtherPrblms;
                    }

                    ddlPayMode.SelectedValue = objDt.Rows[0]["PAYMODE"].ToString();
                    txtTXNId.Text = objDt.Rows[0]["WEBINQTXNID"].ToString();
                    //chkFedexLoc.Checked = objDt.Rows[0]["FEDEXLOC"].ToString() == "1" ? true : false;
                    //txtFedexLoc.Enabled = chkFedexLoc.Checked == true ? true : false;
                    //txtFedexLoc.Text = objDt.Rows[0]["FEDEXLOC"].ToString() == "1" ? objDt.Rows[0]["FEDEXADD"].ToString() : "";
                    int Status = int.Parse(objDt.Rows[0]["STATUS"].ToString());

                    //if (!string.IsNullOrEmpty(objDt.Rows[0]["ESTDESCR"].ToString()))
                    //{
                    //    txtPartDesc.Text = objDt.Rows[0]["ESTDESCR"].ToString();
                    //    lblTotEst.Text = objDt.Rows[0]["ESTAMT"].ToString();
                    //    txtEstComment.Text = objDt.Rows[0]["ESTCOMMENT"].ToString();
                    //    txtReason.Text = objDt.Rows[0]["NTAPRVRSN"].ToString();
                    //    ddlApproved.SelectedValue = objDt.Rows[0]["APRVFLAG"].ToString();
                    //}
                    txtEstAmt.Text = objDt.Rows[0]["ESTAMT"].ToString();
                    chkLoaner.Checked = objDt.Rows[0]["LOANER"].ToString() == "1" ? true : false;
                    btnHold.Visible = FormRights.bEdit == true ? true : false;

                    //if (string.IsNullOrEmpty(objDt.Rows[0]["REASON2"].ToString()) == false)
                    //{
                    //    if (!string.IsNullOrEmpty(objDt.Rows[0]["REASON2"].ToString()))
                    //    {
                    if (Status == 58)
                    {
                        btnSave.Enabled = btnHold.Enabled = false;
                        if (Session["ROLEID"].ToString() == "1")
                        {
                            btnHold.Text = "Re-Open";
                            btnHold.Enabled = true;
                        }

                        string strMsg = "Inquiry has been cancelled due to " + objDt.Rows[0]["REASON2"].ToString() + "! You can't edit inquiry.";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + strMsg + "\");", true);
                    }
                    if (Status == 59)
                    {
                        string strMsg = "Inquiry has been hold due to " + objDt.Rows[0]["REASON2"] + "!";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + strMsg + "\");", true);
                    }

                    if (Status == 64)
                    {
                        string strJobId = objDALJobsheet.CheckInqInJob(txtInquiryNo.Text);
                        if (strJobId != "")
                        {
                            btnHold.Enabled = false;
                            btnSave.Enabled = Session["ROLEID"].ToString() != "1" ? false : true;
                            string strMsg = "Jobsheet has been already prepared for this Inquiry! Jobsheet No. : " + strJobId + "! You can't edit the inquiry.";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + strMsg + "\");", true);
                        }
                    }

                    //}
                    //}
                }
            }
            catch (Exception ex)
            {
                btnHold.Enabled = false;
                btnSave.Enabled = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message.Replace("\r\n", "") + "\");", true);
            }
        }

        protected void ddlModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtModelName.Enabled = false;
                if (ddlModel.SelectedItem.Text.Contains("NOT IN LIST"))
                {
                    rfvtxtModelName.Enabled = true;
                    txtModelName.Enabled = true;
                    txtModelName.Focus();
                }
                else
                {
                    rfvtxtModelName.Enabled = false;
                    btnSave.Enabled = true;
                    txtModelName.Enabled = false;
                }
                txtModelName.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                objBindDDL.FillModelByItem(ddlModel, ddlBrand.SelectedValue, ddlProduct.SelectedValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                objBindDDL.FillCity(ddlCity, ddlState.SelectedValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnShowRegDtl_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCardNo.Text.Trim().Length > 0 && txtCardNo.Text.Trim().ToUpper() != MainClass.CommonQTEKCardNo)
                {
                    DataTable dt = new DataTable();
                    dt = objDALCustReg.SELECT_CUSTREGDTL_BYCARDNO(txtCardNo.Text.Trim());
                    dlRegDtl.DataSource = dt;
                    dlRegDtl.DataBind();
                    //txtCardNo_TextChanged(sender, e);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-default').modal();", true);
                }
                else if (ddlSegment.SelectedValue == "1012")
                {
                    if (txtIMEINo.Text.Trim().Length > 0)
                    {
                        DataTable dt = new DataTable();
                        dt = objDALCustReg.SELECT_CUSTREG_BY_IMEINO(txtIMEINo.Text.Trim());
                        if (dt.Rows.Count > 0)
                        {
                            dlActivation.DataSource = dt;
                            dlActivation.DataBind();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-blynk').modal();", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnYes_ServerClick(object sender, EventArgs e)
        {
            try
            {
                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtCardNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSegment.SelectedValue == "1003" || ddlSegment.SelectedValue == "1006")
                {
                    int iCheck = 1;
                    if (txtCardNo.Text.Trim().Length >= 13)
                    {
                        //if (ddlSegment.SelectedValue == "1003" || ddlSegment.SelectedValue == "1006")
                        //{
                        if (!string.IsNullOrEmpty(objMainClass.Validate_CardNo(txtCardNo.Text.Trim(), 0)))
                        {
                            if (ddlSegment.SelectedValue == "1003" && txtCardNo.Text.Trim().ToUpper() != MainClass.CommonQTEKCardNo && txtCardNo.Text.Trim().ToUpper() != "QTEKWIDECAREMBL")
                            {
                                //objBindDDL.FillBrand(ddlBrand, 0);

                                DataTable dt = new DataTable();
                                dt = objDALCustReg.SELECTCUSTREG_BYCARDNO(txtCardNo.Text.Trim());
                                if (dt.Rows.Count > 0)
                                {
                                    //ddlRef.SelectedIndex = ddlRef.Items.IndexOf(ddlRef.Items.FindByText(dt.Rows[0]["REF"].ToString()));
                                    //txtName.Text = dt.Rows[0]["FULLNAME"].ToString();
                                    //txtMobileNo.Text = dt.Rows[0]["MOBILENO"].ToString();
                                    //txtEmailId.Text = dt.Rows[0]["EMAILID"].ToString();
                                    ddlBrand.SelectedValue = dt.Rows[0]["BRANDID"].ToString();
                                    objBindDDL.FillModel(ddlModel, ddlBrand.SelectedValue);
                                    txtModelName.Text = dt.Rows[0]["MODELNAME"].ToString();

                                    if (txtModelName.Text != "")
                                    {
                                        ddlModel.SelectedIndex = ddlModel.Items.IndexOf(ddlModel.Items.FindByText("OTHER, MODEL NOT IN LIST"));
                                    }
                                    txtModelName.Enabled = ddlModel.SelectedItem.Text.Contains("OTHER,") ? true : false;
                                    //if (!string.IsNullOrEmpty(dt.Rows[0]["MODELID"].ToString()))
                                    //{
                                    //    ddlModel.SelectedValue = dt.Rows[0]["MODELID"].ToString();
                                    //    if (ddlModel.SelectedItem.Text.Contains("NOT IN LIST"))
                                    //    {
                                    //        txtModelName.Enabled = true;
                                    //        txtModelName.Text = dt.Rows[0]["MODELNAME"].ToString();
                                    //    }
                                    //    else
                                    //    {
                                    //        txtModelName.Enabled = false;
                                    //    }
                                    //}

                                    //if (!string.IsNullOrEmpty(dt.Rows[0]["CITYID"].ToString()))
                                    //{
                                    //    ddlState.SelectedValue = dt.Rows[0]["STATEID"].ToString();
                                    //    objBindDDL.FillCity(ddlCity, ddlState.SelectedValue);
                                    //    ddlCity.SelectedValue = dt.Rows[0]["CITYID"].ToString();
                                    //}

                                    txtIMEINo.Text = dt.Rows[0]["IMEINO"].ToString();


                                    string strHSValue = dt.Rows[0]["HSVALUE"].ToString();
                                    string strHPurDt = dt.Rows[0]["HANDSETPURDATE"].ToString();
                                    string strPurDt = dt.Rows[0]["PURDATE"].ToString();
                                    string strPackID = dt.Rows[0]["ITEMID"].ToString();
                                    string strPackName = dt.Rows[0]["ITEMDESC"].ToString();

                                    DateTime dtPurDate = Convert.ToDateTime(strPurDt);
                                    DateTime dtHPurDate = Convert.ToDateTime(strHPurDt);

                                    if (strPurDt != "" && strHPurDt != "" && strHSValue != "")
                                    {
                                        if (!strPackName.Contains("REPAIR NOW PACK"))
                                        {
                                            TimeSpan iDiff = dtPurDate - dtHPurDate;
                                            if (iDiff.TotalDays > 6)
                                            {
                                                TimeSpan iDiff1 = Convert.ToDateTime(objMainClass.indianTime.Date) - dtPurDate;
                                                if (iDiff1.TotalDays <= 30)
                                                {
                                                    //iCheck = 0;
                                                    btnSave.Enabled = false;
                                                    //if (Session["ROLEID"].ToString() == "7" || Session["ROLEID"].ToString() == "1") // CALL CENTER HEAD & ADMIN
                                                    //{
                                                    string strMsg = "Registration was done after 7 days of handset purchase. Do you want to still generate Inquiry ?";
                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-confirm').modal();$('#lblConfirmMsg').text(\"" + strMsg + "\");", true);
                                                    //}
                                                    //else
                                                    //{
                                                    //    string strMsg = "Registration was done after 7 days of handset purchase. Please create Inquiry after 30 days of pack purchase.";
                                                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + strMsg + "\");", true);
                                                    //}
                                                    return;
                                                }
                                            }
                                        }

                                        DateDifference datediff = new DateDifference(objMainClass.indianTime, dtPurDate);

                                        int days = datediff.Days;
                                        int months = datediff.Months;
                                        int years = datediff.Years;

                                        if (strPackID == "4000" || strPackID == "9049" || strPackID == "5519" || strPackID == "4003" || strPackID == "9864") // FOR PACK 149,REPAIR NOW,499 BRONZE,599
                                        {
                                            if (years > 1 || (years == 1 && months > 0) || (years == 1 && days > 0))
                                            {
                                                btnSave.Enabled = false;
                                                if (Session["ROLEID"].ToString() == "7" || Session["ROLEID"].ToString() == "1") // CALL CENTER HEAD & ADMIN
                                                {
                                                    string strMsg = "Validity for " + strPackName + " is Expired. Do you want to still generate Inquiry ?";
                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-confirm').modal();$('#lblConfirmMsg').text(\"" + strMsg + "\");", true);
                                                }
                                                else
                                                {
                                                    string strMsg = "Validity for " + strPackName + " is Expired.";
                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + strMsg + "\");", true);
                                                }
                                                return;
                                            }
                                        }

                                        else if (strPackID == "4001" || strPackID == "4002" || strPackID == "5521" || strPackID == "5524" || strPackID == "5525" || strPackID == "5526") // FOR PACK 299,499,999,1499,1999,2499
                                        {
                                            if ((years == 1 && months > 0) || (years == 1 && days > 0) || (years == 2 && months == 0 && days == 0))
                                            {

                                            }
                                            else
                                            {
                                                btnSave.Enabled = false;
                                                if (Session["ROLEID"].ToString() == "7" || Session["ROLEID"].ToString() == "1") // CALL CENTER HEAD & ADMIN
                                                {
                                                    string strMsg = "Either validity for " + strPackName + " is expired or Invalid time to create inquiry. Do you want to still generate Inquiry ?";
                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-confirm').modal();$('#lblConfirmMsg').text(\"" + strMsg + "\");", true);
                                                }
                                                else
                                                {
                                                    string strMsg = "Validity for " + strPackName + " is Expired.";
                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + strMsg + "\");", true);
                                                }
                                                return;

                                            }
                                        }

                                        else if (strPackName == "SMART PACK")
                                        {
                                            if (months < 6 || (months == 6 && days == 0))
                                            {

                                            }
                                            else
                                            {
                                                btnSave.Enabled = false;
                                                if (Session["ROLEID"].ToString() == "7" || Session["ROLEID"].ToString() == "1") // CALL CENTER HEAD & ADMIN
                                                {
                                                    string strMsg = "Either validity of Smart Pack is expired or Invalid time to create inquiry. Do you want to still generate Inquiry ?";
                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-confirm').modal();$('#lblConfirmMsg').text(\"" + strMsg + "\");", true);
                                                }
                                                else
                                                {
                                                    string strMsg = "Validity of Smart Pack is Expired.";
                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + strMsg + "\");", true);
                                                }
                                                return;
                                            }
                                        }

                                        else if (strPackName == "BASE PACK" || strPackName == "ADD ON UPSALE")
                                        {

                                            //DataTable objdt = new DataTable();
                                            //objdt = objDALCustReg.SelectProtHistoryByRegId_Sherpa(dt.Rows[0]["REGID"].ToString());

                                            int iCount = dt.Rows.Count;

                                            //if (iCount == 0)

                                            string strFeatureID = dt.Rows[0]["FEATUREID"].ToString();
                                            if (strFeatureID == "")
                                            {
                                                if (years > 1 || (years == 1 && months > 0) || (years == 1 && days > 0))
                                                {
                                                    btnSave.Enabled = false;
                                                    if (Session["ROLEID"].ToString() == "7" || Session["ROLEID"].ToString() == "1") // CALL CENTER HEAD & ADMIN
                                                    {
                                                        string strMsg = "Validity of Base Pack is expired. Do you want to still generate Inquiry ?";
                                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-confirm').modal();$('#lblConfirmMsg').text(\"" + strMsg + "\");", true);
                                                    }
                                                    else
                                                    {
                                                        string strMsg = "Validity of Base Pack is Expired.";
                                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + strMsg + "\");", true);
                                                    }
                                                    return;
                                                }
                                            }

                                            else
                                            {
                                                int iTot = 0;
                                                //for (iTot = 0; iTot < iCount; iTot++)
                                                //{
                                                //    string strValidity = objdt.Rows[iTot]["VALIDITY"].ToString();
                                                //    string strFeature = objdt.Rows[iTot]["FEATURE"].ToString();

                                                for (iTot = 0; iTot < iCount; iTot++)
                                                {
                                                    string strValidity = dt.Rows[iTot]["VALIDITY"].ToString();
                                                    string strFeature = dt.Rows[iTot]["FEATURE"].ToString();
                                                    if (strValidity == "For 1st Year")
                                                    {
                                                        if (years > 1 || (years == 1 && months > 0) || (years == 1 && days > 0))
                                                        {
                                                            btnSave.Enabled = false;
                                                            if (Session["ROLEID"].ToString() == "7" || Session["ROLEID"].ToString() == "1") // CALL CENTER HEAD & ADMIN
                                                            {
                                                                string strMsg = "Validity of Base Pack is expired. Do you want to still generate Inquiry ?";
                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-confirm').modal();$('#lblConfirmMsg').text(\"" + strMsg + "\");", true);
                                                            }
                                                            else
                                                            {
                                                                string strMsg = "Validity of Base Pack is Expired.";
                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + strMsg + "\");", true);
                                                            }
                                                            return;
                                                        }
                                                    }
                                                    else if (strValidity == "For 2nd Year")
                                                    {
                                                        if ((years == 1 && months > 0) || (years == 1 && days > 0) || (years == 2 && months == 0 && days == 0))
                                                        {

                                                        }
                                                        else if (years == 0 || years == 1 && months == 0 && days == 0)
                                                        {
                                                            string strMsg = "Only Pick up and Delivery is applicable for 1st year. Damage repair will not be applicable !";
                                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + strMsg + "\");", true);
                                                            return;
                                                        }
                                                        else
                                                        {
                                                            btnSave.Enabled = false;
                                                            if (Session["ROLEID"].ToString() == "7" || Session["ROLEID"].ToString() == "1") // CALL CENTER HEAD & ADMIN
                                                            {
                                                                string strMsg = "Validity of Base Pack is expired. Do you want to still generate Inquiry ?";
                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-confirm').modal();$('#lblConfirmMsg').text(\"" + strMsg + "\");", true);
                                                            }
                                                            else
                                                            {
                                                                string strMsg = "Validity of Base Pack is expired";
                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + strMsg + "\");", true);
                                                            }
                                                            return;
                                                        }
                                                    }
                                                    else if (strValidity == "For 1st and 2nd Year")
                                                    {
                                                        if (years > 2 || (years == 2 && months > 0) || (years == 2 && days > 0))
                                                        {
                                                            btnSave.Enabled = false;
                                                            if (Session["ROLEID"].ToString() == "7" || Session["ROLEID"].ToString() == "1") // CALL CENTER HEAD & ADMIN
                                                            {
                                                                string strMsg = "Validity of Base Pack is expired. Do you want to still generate Inquiry ?";
                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-confirm').modal();$('#lblConfirmMsg').text(\"" + strMsg + "\");", true);
                                                            }
                                                            else
                                                            {
                                                                string strMsg = "Validity of Base Pack is expired";
                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + strMsg + "\");", true);
                                                            }
                                                            return;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            btnSave.Enabled = false;
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid Pack.');", true);
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        btnSave.Enabled = false;
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid Handset or Pack purchase detail.');", true);
                                        return;
                                    }
                                }

                                if (!string.IsNullOrEmpty(objMainClass.Validate_CardNo(txtCardNo.Text.Trim(), 1)))
                                {
                                    if (!string.IsNullOrEmpty(objMainClass.Validate_CardNo(txtCardNo.Text.Trim(), 2)))
                                    {
                                        if (string.IsNullOrEmpty(objMainClass.Validate_BasepackExpireRWR(txtCardNo.Text.Trim())))
                                        {
                                            string strReturn = objMainClass.Validate_CloseCouponNo(txtCardNo.Text.Trim());
                                            if (strReturn == "0" || strReturn == "")
                                            {
                                                //if (Session["ROLEID"].ToString() == "7" || Session["ROLEID"].ToString() == "1") // CALL CENTER HEAD & ADMIN
                                                //{
                                                string strMsg = "Duplicate card No. found ! Do you want to reuse card No. " + txtCardNo.Text.Trim() + " ?";
                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-confirm').modal();$('#lblConfirmMsg').text(\"" + strMsg + "\");", true);
                                                //}
                                                //else
                                                //{
                                                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Duplicate card No. not allowed!');", true);
                                                //}
                                                iCheck = 0;
                                            }
                                            else
                                            {
                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Full & Final payment had been made for the Coupon!');", true);
                                                iCheck = 0;
                                            }
                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Previous case was RWR, Pack is expired!');", true);
                                            iCheck = 0;
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Customer Registration is still pending!');", true);
                                        iCheck = 0;
                                    }
                                }


                            }
                            //else if ((ddlSegment.SelectedValue == "1006" && txtCardNo.Text.Trim().ToUpper() != "QTEKWIDECAREMBL") || (ddlSegment.SelectedValue == "1003" && txtCardNo.Text.Trim().ToUpper() == "QTEKWIDECAREMBL"))
                            //{
                            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid Card No!');", true);
                            //    iCheck = 0;
                            //}
                            else if ((ddlSegment.SelectedValue == "1006" && txtCardNo.Text.Trim().ToUpper() == "QTEKWIDECAREMBL") || (ddlSegment.SelectedValue == "1003" && txtCardNo.Text.Trim().ToUpper() == MainClass.CommonQTEKCardNo))
                            {
                                //objBindDDL.FillBrand(ddlBrand, 1);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid Card No!');", true);
                                iCheck = 0;
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid Card No!');", true);
                            iCheck = 0;
                        }
                        //}
                        //else
                        //{

                        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid Segment Selected !');", true);
                        //    iCheck = 0;

                        //}
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid Card No!');", true);
                        iCheck = 0;
                    }

                    btnSave.Enabled = iCheck == 0 ? false : true;
                }
            }
            catch (Exception ex)
            {
                btnHold.Visible = false;
                btnSave.Enabled = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message.Replace("\r\n", "") + "\");", true);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate("aValidationGroup");
                if (Page.IsValid)
                {
                    if (Session["InqNo"] != null)
                    {
                        if (FormRights.bEdit == true && Session["ROLEID"].ToString() != "18")
                        {
                            int iResult = objDALInquiry.UPDATE_TRAN_INQMST_WITH_ITEMID(0, ddlRef.SelectedIndex > 0 ? ddlRef.SelectedItem.Text.ToUpper() : "", txtComments.Text,
                                    txtInquiryNo.Text, int.Parse(ddlBrand.SelectedValue.ToString()), int.Parse(ddlModel.SelectedValue.ToString()), int.Parse(ddlSimCarrier.SelectedValue.ToString()),
                            txtModelName.Text, txtIMEINo.Text, txtDevicePass.Text, txtName.Text, "", txtMobileNo.Text, txtAltMobNo.Text, txtEmailId.Text, txtCardNo.Text.Trim(), txtAddr1.Text,
                            txtAddr2.Text, int.Parse(ddlState.SelectedValue.ToString()), int.Parse(ddlCity.SelectedValue.ToString()), txtLandmark.Text, txtPostCode.Text,
                            Int32.Parse(ddlArea.SelectedValue), DateTime.Parse(txtPickupDt.Text), TimeSpan.Parse(dtpPickupTime.Text), txtProblems.Text, txtProblems.Text, "",
                            "0", int.Parse(Session["USERID"].ToString()), "0", "",
                            "", ddlSegment.SelectedValue != "1004" ? 8 : int.Parse(ddlPayMode.SelectedValue), 0, "", txtServiceCntrName.Text, chkLoaner.Checked == true ? 1 : 0,
                            int.Parse(Session["USERID"].ToString()), ddlProduct.SelectedValue);
                            Session["InqNo"] = null;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Inquiry updated successfully.');$('.close').click(function(){window.location.href ='InquiryList.aspx' });", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to edit inquiry.');", true);
                            return;
                        }
                    }

                    else
                    {
                        string strpickuptime = TimeSpan.Parse(dtpPickupTime.Text).ToString("hh\\:mm");
                        string twentyFourHour = objMainClass.indianTime.Hour.ToString("00") + ":" + objMainClass.indianTime.Minute.ToString("00");
                        TimeSpan objts = TimeSpan.Parse("00:00");

                        if (DateTime.Parse(txtPickupDt.Text).Date == objMainClass.indianTime.Date && TimeSpan.Parse(strpickuptime) < TimeSpan.Parse(twentyFourHour).Add(objts))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Pickup time must be future.');", true);
                            return;
                        }
                        else
                        {

                            using (TransactionScope scope = new TransactionScope())
                            {
                                int iCallid = 0;
                                if (Session["CN"] != null)
                                {
                                    iCallid = int.Parse(Session["CID"].ToString());
                                }

                                txtInquiryNo.Text = objDALInquiry.SELECT_MAX_ORDERID(ddlSegment.SelectedValue).ToString();
                                int iResult = 0;
                                if (ddlSegment.SelectedValue == "1009")
                                {
                                    objDALInquiry.INSERT_TRAN_INQMST_JEEVES(0, "", "", txtCardNo.Text.Trim(), long.Parse(txtInquiryNo.Text), ddlBrand.SelectedItem.Text, 1, ddlModel.SelectedItem.Text, txtIMEINo.Text,
                                        "", "", "", txtName.Text.Trim(), "", "", txtMobileNo.Text, txtAltMobNo.Text, txtEmailId.Text, "", txtAddr1.Text, txtAddr2.Text, "IN",
                                        int.Parse(ddlState.SelectedValue), ddlCity.SelectedItem.Text, txtLandmark.Text, txtPostCode.Text, DateTime.Parse(txtPickupDt.Text), TimeSpan.Parse(dtpPickupTime.Text)
                                        , 0, 57, txtProblems.Text, "", ddlSegment.SelectedValue, ddlDistChnl.SelectedValue, int.Parse(Session["USERID"].ToString()));
                                }
                                else if (ddlSegment.SelectedValue == "1012")
                                {
                                    objDALInquiry.INSERT_TRAN_INQMST_ONLINEPORTAL(0, ddlRef.SelectedIndex > 0 ? ddlRef.SelectedItem.Text.ToUpper() : "", txtComments.Text.ToUpper(), long.Parse(txtInquiryNo.Text), int.Parse(ddlBrand.SelectedValue), int.Parse(ddlSimCarrier.SelectedValue), ddlModel.SelectedItem.Text, txtIMEINo.Text,
                                        "", "", "", txtName.Text.Trim(), "", "", txtMobileNo.Text, txtAltMobNo.Text, txtEmailId.Text, "", txtAddr1.Text, txtAddr2.Text, "IN",
                                        int.Parse(ddlState.SelectedValue), ddlCity.SelectedItem.Text, txtLandmark.Text, txtPostCode.Text, DateTime.Parse(txtPickupDt.Text), TimeSpan.Parse(dtpPickupTime.Text)
                                        , 0, 57, txtProblems.Text, txtProblems.Text, ddlSegment.SelectedValue, ddlDistChnl.SelectedValue, int.Parse(Session["USERID"].ToString()));


                                    String strCustContent = "";
                                    strCustContent = fileread();
                                    strCustContent = strCustContent.Replace("###IMAGEPATH###", "<img src='" + ConfigurationManager.AppSettings["blynklogopath"] + "' />");
                                    strCustContent = strCustContent.Replace("###IMAGEPATH1###", "<img src='" + ConfigurationManager.AppSettings["blynklogopath1"] + "' />");
                                    strCustContent = strCustContent.Replace("#InqNo", txtInquiryNo.Text);
                                    objMainClass.SendSMSToServer("Dear Customer," + System.Environment.NewLine + "Your query reference number is " + txtInquiryNo.Text + ", Our technical team will call you soon to schedule a pickup for your Blynk device. Please keep your warranty details ready for verification.", "", txtMobileNo.Text, "", false, 0, "QRMTEK", "");
                                    iResult = objMainClass.SendEmail(txtEmailId.Text.Trim(), "Welcome to Blynk Mobile Solutions, Claim Registration No. " + txtInquiryNo.Text + "", strCustContent);
                                }
                                else
                                {
                                    iResult = objDALInquiry.INSERT_TRAN_INQMST_WITH_ITEMID(0, ddlRef.SelectedIndex > 0 ? ddlRef.SelectedItem.Text.ToUpper() : "", txtComments.Text.ToUpper(),
                                       txtInquiryNo.Text, int.Parse(ddlBrand.SelectedValue), int.Parse(ddlModel.SelectedValue), int.Parse(ddlSimCarrier.SelectedValue),
                                       txtModelName.Text, txtIMEINo.Text, txtDevicePass.Text, txtName.Text, "", txtMobileNo.Text, txtAltMobNo.Text, txtEmailId.Text,
                                       txtCardNo.Text.Trim().ToUpper(), txtAddr1.Text, txtAddr2.Text, int.Parse(ddlState.SelectedValue), int.Parse(ddlCity.SelectedValue),
                                       txtLandmark.Text, txtPostCode.Text, Int32.Parse(ddlArea.SelectedValue), DateTime.Parse(txtPickupDt.Text), TimeSpan.Parse(dtpPickupTime.Text),
                                       iCallid, txtProblems.Text.ToUpper(), txtProblems.Text, "", "0",
                                       int.Parse(Session["USERID"].ToString()), "0", "", "", ddlSegment.SelectedValue,
                                       ddlDistChnl.SelectedValue, int.Parse(ddlPayMode.SelectedValue), 0, "", txtServiceCntrName.Text, Session["EstInqId"],
                                       chkLoaner.Checked == true ? 1 : 0, int.Parse(Session["USERID"].ToString()), int.Parse(Session["USERID"].ToString()), ddlLeads.SelectedItem.Text, ddlProduct.SelectedValue);
                                }
                                if (Session["CN"] != null)
                                {
                                    objMainClass.UpdateCallAttempt(iCallid.ToString(), "", Session["USERID"].ToString(), "E");
                                }
                                Session["CN"] = Session["InqNo"] = Session["EstInqId"] = null;

                                if (Session["CallingIdDetail"] != null)
                                {
                                    int iLeadResult = objMainClass.UpdateLeadInqNo(Convert.ToInt32(Session["CallingIdDetail"]), txtInquiryNo.Text, (int)LeadStatus.Converted, Convert.ToInt32(Session["USERID"]), "INSERTINQUIRY");

                                    if (iLeadResult > 0)
                                    {
                                        scope.Complete();
                                        scope.Dispose();

                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Inquiry created successfully. Inquiry No. is \"" + txtInquiryNo.Text + "\".');$('.close').click(function(){window.location.href ='InquiryList.aspx' });", true);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Something went wrong.');", true);
                                    }

                                }
                                else
                                {
                                    scope.Complete();
                                    scope.Dispose();

                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Inquiry created successfully. Inquiry No. is \"" + txtInquiryNo.Text + "\".');$('.close').click(function(){window.location.href ='InquiryList.aspx' });", true);
                                }





                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message.Replace("\r\n", "") + "\");", true);
            }
        }

        protected void dlRegDtl_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item)
                {
                    DataRowView drv = e.Item.DataItem as DataRowView;
                    GridView innerGridView = e.Item.FindControl("gvFeatures") as GridView;
                    DataTable objdt = new DataTable();
                    Label lblRegId = e.Item.FindControl("lblRegId") as Label;
                    objdt = objDALCustReg.SelectProtHistoryByRegId_Sherpa(lblRegId.Text);
                    innerGridView.DataSource = objdt;
                    innerGridView.DataBind();

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message.Replace("\r\n", "") + "\");", true);
            }
        }


        protected static string fileread()
        {
            string actualfol;
            actualfol = HttpContext.Current.Server.MapPath("MailTemplate/Blynkmail.html");
            StreamReader sr = new StreamReader(actualfol);
            string strcontent = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            return strcontent;
        }

        protected void chkServiceCntr_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkServiceCntr.Checked == true)
                {
                    txtServiceCntrName.Enabled = true;
                    rfvServCntr.Enabled = true;
                }
                else
                {
                    txtServiceCntrName.Enabled = false;
                    rfvServCntr.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnHold_Click(object sender, EventArgs e)
        {
            if (btnHold.Text == "Re-Open")
            {
                try
                {
                    objDALInquiry.UPDATE_INQUIRYSTATUSWITHREASON(txtInquiryNo.Text, 57, "", "", Session["USERID"].ToString());
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message.Replace("\r\n", "") + "\");", true);
                }
            }
            else
            {
                Response.Redirect("StatusUpdate.aspx?INID=" + Session["InqNo"].ToString());
            }
        }

        protected void ddlSegment_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtInquiryNo.Text = objDALInquiry.SELECT_MAX_ORDERID(ddlSegment.SelectedValue).ToString();
            txtCardNo.Text = "";
            if (ddlSegment.SelectedValue == "1003")
            {
                //objBindDDL.FillBrand(ddlBrand, 1);
                ddlDistChnl.SelectedValue = "30";
            }
            else
            {
                //objBindDDL.FillBrand(ddlBrand, 0);
                if (ddlSegment.SelectedValue == "1002")
                {
                    ddlDistChnl.SelectedValue = "40";
                }
                else if (ddlSegment.SelectedValue == "1006" || ddlSegment.SelectedValue == "1009" || ddlSegment.SelectedValue == "1005" || ddlSegment.SelectedValue == "1035")
                {
                    ddlDistChnl.SelectedValue = "20";
                }
                else if (ddlSegment.SelectedValue == "1012" || ddlSegment.SelectedValue == "1038")
                {
                    ddlDistChnl.SelectedValue = "40";
                }
            }

            string response = validateIMEI();
            if (response != string.Empty && response != null && response != "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('" + response + "');", true);
            }

        }

        protected void txtPostCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPostCode.Text.Trim().Length == 6)
                {
                    if (Session["InqNo"] == null)
                    {
                        DataTable dt = new DataTable();
                        dt = objMainClass.SELECT_CITY_BYPINCODE(txtPostCode.Text.Trim());
                        if (dt.Rows.Count > 0)
                        {
                            ddlState.SelectedValue = dt.Rows[0]["STATE_ID"].ToString();
                            ddlCity.Items.Clear();
                            ddlCity.SelectedIndex = -1;
                            ddlCity.SelectedValue = null;
                            ddlCity.ClearSelection();
                            objBindDDL.FillCity(ddlCity, ddlState.SelectedValue);
                            //if (ddlCity.Items.FindByValue(dt.Rows[0]["city_id"].ToString().Trim()) != null)
                            //{
                            //    ddlCity.SelectedValue = dt.Rows[0]["city_id"].ToString().Trim();
                            //}
                            //ddlCity.SelectedValue = dt.Rows[0]["city_id"].ToString();
                            ddlCity.SelectedIndex = ddlCity.Items.IndexOf(ddlCity.Items.FindByValue(dt.Rows[0]["city_id"].ToString()));
                            if (ddlCity.SelectedValue == "0")
                            {
                                ddlCity.SelectedIndex = ddlCity.Items.IndexOf(ddlCity.Items.FindByText(dt.Rows[0]["city_name"].ToString().ToUpper()));
                            }
                        }
                        else
                        {
                            ddlState.SelectedIndex = 0;
                            ddlCity.SelectedIndex = -1;
                        }
                    }
                    DataTable objdt = new DataTable();
                    objdt = objMainClass.GetPincodeDetail(txtPostCode.Text);
                    int FedexCheck = 1;
                    lblFedexCap.Visible = true;
                    if (objdt.Rows.Count > 0)
                    {

                        lblFedexCap.Text = "Fedex : " + objdt.Rows[0]["STATUS"].ToString() + ", " + objdt.Rows[0]["CODCAPABILITY"].ToString() + ", " + objdt.Rows[0]["fdxcapability"].ToString();

                        if (objdt.Rows[0]["STATUS"].ToString() != "Regular")
                        {
                            FedexCheck = 0;
                            lblFedexCap.ForeColor = Color.Red;
                            ddlPayMode.SelectedValue = "5";
                            //if (Session["InqNo"] == null)
                            //{
                            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Out of courier service.');", true);
                            //}
                        }
                        else if (objdt.Rows[0]["fdxcapability"].ToString() != "Pickup & Delivery")
                        {
                            lblFedexCap.ForeColor = Color.Red;
                            ddlPayMode.SelectedValue = "5";
                            //if (Session["InqNo"] == null)
                            //{
                            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + objdt.Rows[0]["fdxcapability"].ToString() + "\");", true);
                            //}
                        }
                        else if (objdt.Rows[0]["CODCAPABILITY"].ToString() != "COD")
                        {
                            lblFedexCap.ForeColor = Color.Red;
                            ddlPayMode.SelectedValue = "5";
                            //if (Session["InqNo"] == null)
                            //{
                            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('COD option is not available.');", true);
                            //}
                        }
                        else
                        {
                            ddlPayMode.SelectedValue = "1";
                            lblFedexCap.ForeColor = Color.DarkGreen;
                        }
                    }
                    else
                    {
                        FedexCheck = 0;
                        ddlPayMode.SelectedValue = "5";
                        lblFedexCap.Text = "Fedex : " + "Pincode is out of courier service!";
                        lblFedexCap.ForeColor = Color.Red;
                    }

                    // BIZLOG
                    string strBizlogcheck = objMainClass.Select_BizlogPostalcode(txtPostCode.Text.Trim());

                    // ECOM
                    //var EComCheck = (object)DBNull.Value;
                    //var pairs = new List<KeyValuePair<string, string>>
                    //    {
                    //        new KeyValuePair<string, string>("username", "qarmatekservices272012_riv"),
                    //        new KeyValuePair<string, string>("password", "XfGhRSDRATj65Dna")
                    //    };
                    //var content = new FormUrlEncodedContent(pairs);
                    //var client = new HttpClient { BaseAddress = new Uri("https://api.ecomexpress.in/") };
                    //var response = client.PostAsync("apiv2/pincodes/", content);
                    //if (response.Result.IsSuccessStatusCode)
                    //{
                    //    string resultContent = response.Result.Content.ReadAsStringAsync().Result;
                    //    var o = JValue.Parse(resultContent);
                    //    foreach (var type3Resource in o.Where(obj => obj["pincode"].Value<string>() == txtPostCode.Text.Trim()))
                    //    {
                    //        EComCheck = type3Resource["route"];
                    //    }
                    //}

                    lblInvalidPincode.Text = "Available Couriers : ";
                    lblInvalidPincode.ForeColor = Color.DarkGreen;
                    if (FedexCheck == 1)
                    {
                        lblInvalidPincode.Text += "Fedex ";
                    }
                    //if (!string.IsNullOrEmpty(EComCheck.ToString()))
                    //{
                    //    lblInvalidPincode.Text += "ECom ";
                    //}
                    if (strBizlogcheck != "")
                    {
                        lblInvalidPincode.Text += "Bizlog";
                    }

                    //for temparary Ecom service closed string.IsNullOrEmpty(EComCheck.ToString()) && 
                    if (strBizlogcheck == "" && FedexCheck == 0)
                    {
                        lblFedexCap.Visible = false;
                        lblInvalidPincode.Text += "Pincode is out of courier service!";
                        lblInvalidPincode.ForeColor = Color.Red;
                    }

                    if (ddlSegment.SelectedValue != "1004") //|| (ddlSegment.SelectedValue == "1003" && (!ddlRef.SelectedItem.Text.Contains("WEB ESTIMATE") && !ddlRef.SelectedItem.Text.Contains("WEBMAIL") && ddlRef.SelectedIndex > 0)))
                    {
                        ddlPayMode.SelectedValue = "8";
                    }
                    rfvArea.Enabled = ddlCity.SelectedValue == "3026" ? true : false;
                }
            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message.Replace("\r\n", "") + "\");", true);
            }
        }

        protected void btnCallHistory_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "CallHistory.aspx?DocNo=" + Session["InqNo"].ToString();
                string s = "window.open('" + url + "', '_blank');";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "script", s, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtMobileNo_TextChanged(object sender, EventArgs e)
        {
            if (Session["InqNo"] == null)
            {
                try
                {
                    string ContactNo = (sender as TextBox).Text;
                    string strReturn = objDALInquiry.CheckOpenInquiryByContactNo(ContactNo);
                    if (strReturn != "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Inquiry is already open with current contact number. Inquiry No. is \"" + strReturn + "\"!');", true);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message.Replace("\r\n", "") + "\");", true);
                }
            }
        }

        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                objBindDDL.FillBrandByItem(ddlBrand, ddlProduct.SelectedValue);
                ddlModel.DataSource = string.Empty;
                ddlModel.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            rfvArea.Enabled = ddlCity.SelectedValue == "3026" ? true : false;
        }

        protected void txtIMEINo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    if (txtIMEINo.Text != string.Empty && txtIMEINo.Text != null && txtIMEINo.Text != "")
                    {
                        string response = validateIMEI();
                        if (response != string.Empty && response != null && response != "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('" + response + "');", true);
                        }
                    }
                    else
                    {

                    }



                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string validateIMEI()
        {
            string response = "";

            try
            {
                if (Session["USERID"] != null)
                {
                    if (txtIMEINo.Text != string.Empty && txtIMEINo.Text != null && txtIMEINo.Text != "")
                    {
                        DataTable dt = new DataTable();
                        dt = objMainClass.GetSODetails(objMainClass.intCmpId, "", txtIMEINo.Text, "CHECKWARRANTY", "", "");
                        if (dt.Rows.Count > 0)
                        {
                            if (Convert.ToString(dt.Rows[0]["DELIVERYDATE"]) != null && Convert.ToString(dt.Rows[0]["DELIVERYDATE"]) != "" && Convert.ToString(dt.Rows[0]["DELIVERYDATE"]) != string.Empty)
                            {
                                DateTime olddate, newdate;
                                TimeSpan daydiff;

                                olddate = Convert.ToDateTime(dt.Rows[0]["DELIVERYDATE"]);
                                newdate = DateTime.Now;

                                int month = 12 * (newdate.Year - olddate.Year) + newdate.Month - olddate.Month;

                                daydiff = newdate - olddate;

                                if (month <= Convert.ToInt32(dt.Rows[0]["WARRANTY"]))
                                {
                                    if (ddlSegment.SelectedValue == "1038")
                                    {
                                        btnSave.Enabled = true;
                                    }
                                    else
                                    {
                                        response = "It is Mobex Warranty case. You can create inquiry under 1038 segment.!";
                                    }

                                }
                                else
                                {
                                    if (ddlSegment.SelectedValue == "1038" || ddlSegment.SelectedValue == "1015")
                                    {
                                        response = "Out of Warranty. You can not create inquiry under 1038 or 1015 segment.!";
                                        btnSave.Enabled = false;
                                    }
                                    else
                                    {
                                        btnSave.Enabled = true;
                                    }
                                }

                            }
                            else
                            {
                                response = "Old job Id still open. Cannot create inquiry. Job ID is " + Convert.ToString(dt.Rows[0]["JOBID"]);
                                btnSave.Enabled = false;
                            }
                        }
                        else
                        {
                            if (ddlSegment.SelectedValue == "1038")
                            {
                                response = "You cannot create Inquiry in Mobex Warranty Segment. This IMEI is wrong or SI/SO Cancelled or Returned.!";
                                btnSave.Enabled = false;
                            }
                            else
                            {
                                btnSave.Enabled = true;
                            }
                        }

                    }
                    else
                    {

                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
    }
}