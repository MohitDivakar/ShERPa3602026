using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.FI
{
    public partial class frmBankPayment : System.Web.UI.Page
    {
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        //decimal docamt = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {
                    if (Session["USERID"] != null)
                    {
                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), mainmenuid.Value, "");
                        if (FormRights.bView == false) //if (objDALUserRights.bView == false)
                        {
                            //Session.Abandon();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }

                        objBindDDL.FillDocTypeNew(ddlDoctype, "BP");
                        objBindDDL.FillPaymentMode(ddlPaymentMode);
                        ddlPaymentMode.SelectedValue = "5";
                        //objBindDDL.FillLists(ddlBankAccount, "BA");
                        ddlDoctype.SelectedIndex = 1;
                        ddlDoctype.Enabled = false;
                        txtDocDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtDocNo.Text = objMainClass.MAXFIRANGENO(objMainClass.intCmpId, objMainClass.getFinYear(objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true)), "BP");

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

                    DataTable dtSameAmt = objMainClass.CheckSameAmt(objMainClass.intCmpId, txtVendCode.Text, Convert.ToDecimal(txtDocAmt.Text), "CHECKSAMEAMT");
                    if (dtSameAmt.Rows.Count > 0)
                    {
                        lblAlertMessageAmt.Text = "Payment already done with same amount to " + Convert.ToString(dtSameAmt.Rows[0]["PARTY"]) + " as " + Convert.ToString(dtSameAmt.Rows[0]["PAYMENTFLAG"]) + " by " + Convert.ToString(dtSameAmt.Rows[0]["USERNAME"]) + " on " + Convert.ToString(dtSameAmt.Rows[0]["CREATEDATE"]) + " with Remarks : " + Convert.ToString(dtSameAmt.Rows[0]["REMARK"]) + ". Doc. date is : " + Convert.ToString(dtSameAmt.Rows[0]["DOCDT"]) + " . Doc. No. is : " + Convert.ToString(dtSameAmt.Rows[0]["DOCNO"]) + " . Payment mode is : " + Convert.ToString(dtSameAmt.Rows[0]["PAYMODE"]) + " . Do you want to continue this entry..?";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-YesNo').modal();", true);

                    }
                    else
                    {


                        if (rblPOPB.SelectedValue == "OA")
                        {
                            //int iResult = objMainClass.InsertOnAccountBankEntry(objMainClass.intCmpId, ddlDoctype.SelectedValue, txtDocNo.Text, txtDocDate.Text, ddlBankAccount.SelectedItem.Text, 0, 1, "", txtVendCode.Text,
                            int iResult = objMainClass.InsertOnAccountBankEntry(objMainClass.intCmpId, ddlDoctype.SelectedValue, txtDocNo.Text, txtDocDate.Text, "", 0, 1, "", txtVendCode.Text,
                                txtDIscAC.Text, txtRemarks.Text, txtDiscountAmt.Text, txtDocAmt.Text, txtAdjAmt.Text, Convert.ToInt32(Session["USERID"]), txtTXNID.Text, Convert.ToInt32(ddlPaymentMode.SelectedValue), "BANKPAYMENTENTRY");
                            if (iResult == 1)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Record save sucessfully.!');$('.close').click(function(){window.location.href ='frmBankPayment.aspx' });", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Record not inserted sucessfully!');", true);
                            }

                        }
                        if (rblPOPB.SelectedValue == "PO")
                        {
                            int i = CheckSelectedPO();
                            if (i > 0 && Convert.ToDecimal(txtDocAmt.Text) > 0)
                            {
                                //int iReturn = objMainClass.InsertAdvanceBankEntry(objMainClass.intCmpId, ddlDoctype.SelectedValue, txtDocNo.Text, txtDocDate.Text, ddlBankAccount.SelectedItem.Text, 1, 0, txtVendCode.Text,
                                int iReturn = objMainClass.InsertAdvanceBankEntry(objMainClass.intCmpId, ddlDoctype.SelectedValue, txtDocNo.Text, txtDocDate.Text, "", 1, 0, txtVendCode.Text,
                                    txtDIscAC.Text, txtRemarks.Text, txtDiscountAmt.Text, Convert.ToInt32(Session["USERID"]), txtTXNID.Text, Convert.ToInt32(ddlPaymentMode.SelectedValue), gvPOList, "BANKPAYMENTENTRY");
                                if (iReturn == 1)
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Record save sucessfully.!');$('.close').click(function(){window.location.href ='frmBankPayment.aspx' });", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Record not inserted sucessfully!');", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please select PO from PO list.!');", true);
                            }

                        }
                        if (rblPOPB.SelectedValue == "PB")
                        {
                            int i = CheckSelectedPB();
                            if (i > 0)  //&& Convert.ToDecimal(txtDocAmt.Text) > 0)
                            {
                                //int iReturn = objMainClass.InsertPBBankEntry(objMainClass.intCmpId, ddlDoctype.SelectedValue, txtDocNo.Text, txtDocDate.Text, ddlBankAccount.SelectedItem.Text, 0, 0, txtVendCode.Text,
                                int iReturn = objMainClass.InsertPBBankEntry(objMainClass.intCmpId, ddlDoctype.SelectedValue, txtDocNo.Text, txtDocDate.Text, "", 0, 0, txtVendCode.Text,
                                    txtDIscAC.Text, txtRemarks.Text, txtDiscountAmt.Text, txtDocAmt.Text, Convert.ToInt32(Session["USERID"]), txtTXNID.Text, Convert.ToInt32(ddlPaymentMode.SelectedValue), gvPBAdjData, "BANKPAYMENTENTRY");
                                if (iReturn == 1)
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Record save sucessfully.!');$('.close').click(function(){window.location.href ='frmBankPayment.aspx' });", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Record not inserted sucessfully!');", true);
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


        public int CheckSelectedPO()
        {
            int i = 0;
            try
            {
                if (Session["USERID"] != null)
                {

                    for (int j = 0; j < gvPOList.Rows.Count; j++)
                    {
                        GridViewRow row = gvPOList.Rows[j];
                        CheckBox chkSelect = row.FindControl("chkSelect") as CheckBox;

                        if (chkSelect.Checked == true)
                        {
                            i++;
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
                return i;
            }
            return i;
        }

        public int CheckSelectedPB()
        {
            int i = 0;
            try
            {
                if (Session["USERID"] != null)
                {
                    for (int j = 0; j < gvPBAdjData.Rows.Count; j++)
                    {
                        GridViewRow row = gvPBAdjData.Rows[j];
                        CheckBox chkSelectPBAdj = row.FindControl("chkSelectPBAdj") as CheckBox;

                        if (chkSelectPBAdj.Checked == true)
                        {
                            i++;
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
                return i;
            }
            return i;
        }

        protected void txtVendCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    txtDiscountAmt.Text = "0";
                    txtDocAmt.Text = "0";
                    txtAdjAmt.Text = "0";
                    txtCRAmt.Text = "0";
                    txtDRAmt.Text = "0";
                    txtTAdjAmt.Text = "0";

                    if (txtVendCode.Text != null && txtVendCode.Text != string.Empty && txtVendCode.Text != "")
                    {
                        if (rblPOPB.SelectedValue == "PO" && chkAdvance.Checked == true)
                        {
                            DataTable dt = new DataTable();
                            dt = objMainClass.GetAdvPOData(objMainClass.intCmpId, txtVendCode.Text, 0, 0, "PODATA");
                            if (dt.Rows.Count > 0)
                            {
                                gvPOList.DataSource = dt;
                                gvPOList.DataBind();
                                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-POData').modal();", true);
                                divGrid.Visible = true;
                                divPB.Visible = false;
                                divPBAdjust.Visible = false;
                                divDNAdjust.Visible = false;
                                divOACAdjust.Visible = false;
                            }
                            else
                            {
                                gvPOList.DataSource = string.Empty;
                                gvPOList.DataBind();
                                divGrid.Visible = false;
                                divPB.Visible = false;
                                divPBAdjust.Visible = false;
                                divDNAdjust.Visible = false;
                                divOACAdjust.Visible = false;
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('PO Data not found with entered Vendor Code.!');", true);
                            }
                        }
                        else if (rblPOPB.SelectedValue == "PB")
                        {
                            DataTable dtPB = new DataTable();
                            dtPB = objMainClass.GetAdvPOData(objMainClass.intCmpId, txtVendCode.Text, 1, 1, "ALLADJDATA");
                            if (dtPB.Rows.Count > 0)
                            {
                                gvPBAdjData.DataSource = dtPB;
                                gvPBAdjData.DataBind();
                                divGrid.Visible = false;
                                divPB.Visible = false;
                                divPBAdjust.Visible = true;
                                divDNAdjust.Visible = false;
                                divOACAdjust.Visible = false;
                                //gvPBAdjData.HeaderRow.TableSection = TableRowSection.TableHeader;

                            }
                            else
                            {
                                gvPBAdjData.DataSource = string.Empty;
                                gvPBAdjData.DataBind();
                                divGrid.Visible = false;
                                divPB.Visible = false;
                                divPBAdjust.Visible = true;
                                divDNAdjust.Visible = false;
                                divOACAdjust.Visible = false;
                            }
                        }

                        //else if (rblPOPB.SelectedValue == "PB")
                        //{
                        //    DataTable dtPB = new DataTable();
                        //    dtPB = objMainClass.GetAdvPOData(objMainClass.intCmpId, txtVendCode.Text, 0, 0, "PBDATA");
                        //    if (dtPB.Rows.Count > 0)
                        //    {
                        //        gvPBList.DataSource = dtPB;
                        //        gvPBList.DataBind();
                        //        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-POData').modal();", true);
                        //        divGrid.Visible = false;
                        //        divPB.Visible = true;
                        //        divPBAdjust.Visible = false;
                        //        divDNAdjust.Visible = false;
                        //        divOACAdjust.Visible = false;

                        //    }
                        //    else
                        //    {
                        //        gvPBList.DataSource = string.Empty;
                        //        gvPBList.DataBind();
                        //        divGrid.Visible = false;
                        //        divPB.Visible = false;
                        //        divPBAdjust.Visible = false;
                        //        divDNAdjust.Visible = false;
                        //        divOACAdjust.Visible = false;
                        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('PB Data not found with entered Vendor Code.!');", true);
                        //    }
                        //}
                        //else if (rblPOPB.SelectedValue != "PO" && rblPOPB.SelectedValue != "PB" && rblPOPB.SelectedValue != "OA")
                        //{
                        //    DataTable dtPBAdj = new DataTable();
                        //    dtPBAdj = objMainClass.GetAdvPOData(objMainClass.intCmpId, txtVendCode.Text, 0, 0, "PBADJDATA");
                        //    if (dtPBAdj.Rows.Count > 0)
                        //    {
                        //        gvPBAdjData.DataSource = dtPBAdj;
                        //        gvPBAdjData.DataBind();
                        //        divGrid.Visible = false;
                        //        divPB.Visible = false;
                        //        divPBAdjust.Visible = true;
                        //    }
                        //    else
                        //    {
                        //        gvPBAdjData.DataSource = string.Empty;
                        //        gvPBAdjData.DataBind();
                        //        divGrid.Visible = false;
                        //        divPB.Visible = false;
                        //    }

                        //    DataTable dtDNData = new DataTable();
                        //    dtDNData = objMainClass.GetAdvPOData(objMainClass.intCmpId, txtVendCode.Text, 0, 0, "DEBITNOTE");
                        //    if (dtDNData.Rows.Count > 0)
                        //    {
                        //        gvDNData.DataSource = dtDNData;
                        //        gvDNData.DataBind();
                        //        divGrid.Visible = false;
                        //        divPB.Visible = false;
                        //        divDNAdjust.Visible = true;
                        //    }
                        //    else
                        //    {
                        //        gvDNData.DataSource = string.Empty;
                        //        gvDNData.DataBind();
                        //        divGrid.Visible = false;
                        //        divPB.Visible = false;
                        //    }

                        //    DataTable dtOACData = new DataTable();
                        //    dtOACData = objMainClass.GetAdvPOData(objMainClass.intCmpId, txtVendCode.Text, 0, 1, "ONACDATA");
                        //    if (dtOACData.Rows.Count > 0)
                        //    {
                        //        gvOACData.DataSource = dtOACData;
                        //        gvOACData.DataBind();
                        //        divGrid.Visible = false;
                        //        divPB.Visible = false;
                        //        divOACAdjust.Visible = true;
                        //    }
                        //    else
                        //    {
                        //        gvOACData.DataSource = string.Empty;
                        //        gvOACData.DataBind();
                        //        divGrid.Visible = false;
                        //        divPB.Visible = false;
                        //    }
                        //}

                        DataTable dtVend = new DataTable();
                        dtVend = objMainClass.GetVendor(objMainClass.intCmpId, txtVendCode.Text, "", 0, 0, "0", "", 0, "SELECTONE", "", "");
                        if (dtVend.Rows.Count > 0)
                        {
                            divVendDetails.Visible = true;
                            txtVendName.Text = Convert.ToString(dtVend.Rows[0]["SHOPNAME"]);
                            txtBankName.Text = Convert.ToString(dtVend.Rows[0]["BANKNAME"]);
                            txtACNO.Text = Convert.ToString(dtVend.Rows[0]["ACCOUNTNO"]);
                            txtIFSCCode.Text = Convert.ToString(dtVend.Rows[0]["IFSCCODE"]);
                        }
                        else
                        {
                            divVendDetails.Visible = false;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Vendor details not found.!');", true);
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Vendor Code needs to be enter.!');", true);
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

        protected void rblPOPB_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    txtDiscountAmt.Text = "0";
                    txtDocAmt.Text = "0";
                    txtAdjAmt.Text = "0";
                    if (rblPOPB.SelectedValue == "PO")
                    {
                        chkAdvance.Checked = true;
                        //txtAdjAmt.Visible = false;
                        divAdj.Visible = false;
                        if (txtVendCode.Text != null && txtVendCode.Text != "" && txtVendCode.Text != string.Empty)
                        {
                            txtVendCode_TextChanged(1, e);
                        }
                    }
                    else if (rblPOPB.SelectedValue == "OA")
                    {
                        chkAdvance.Checked = false;
                        //txtAdjAmt.Visible = false;

                        divAdj.Visible = false;
                        gvPOList.DataSource = string.Empty;
                        gvPOList.DataBind();
                        divGrid.Visible = false;


                        gvPBList.DataSource = string.Empty;
                        gvPBList.DataBind();
                        divPB.Visible = false;

                        gvPBAdjData.DataSource = string.Empty;
                        gvPBAdjData.DataBind();
                        divPBAdjust.Visible = false;

                        if (txtVendCode.Text != null && txtVendCode.Text != "" && txtVendCode.Text != string.Empty)
                        {
                            txtVendCode_TextChanged(1, e);
                        }
                    }
                    else
                    {
                        chkAdvance.Checked = false;
                        //txtAdjAmt.Visible = false;
                        divAdj.Visible = true;
                        if (txtVendCode.Text != null && txtVendCode.Text != "" && txtVendCode.Text != string.Empty)
                        {
                            txtVendCode_TextChanged(1, e);
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

        protected void chkAdvance_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    if (chkAdvance.Checked == true)
                    {
                        rblPOPB.SelectedValue = "PO";
                        //txtAdjAmt.Visible = false;
                        divAdj.Visible = false;
                        if (txtVendCode.Text != null && txtVendCode.Text != "" && txtVendCode.Text != string.Empty)
                        {
                            txtVendCode_TextChanged(1, e);
                        }
                    }
                    else
                    {
                        rblPOPB.SelectedIndex = -1;
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
                    Label lblPayableAmt = ((Label)grdrow.FindControl("lblPayableAmt"));
                    TextBox txtPayableAmt = ((TextBox)grdrow.FindControl("txtPayableAmt"));
                    if (txtPayableAmt.Text != null && txtPayableAmt.Text != "" && txtPayableAmt.Text != string.Empty)
                    {
                        if (Convert.ToDecimal(txtPayableAmt.Text) > Convert.ToDecimal(lblPayableAmt.Text))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Entered amount is less than or equal to payable amount.');", true);
                        }
                        else
                        {
                            if (chkSelect.Checked == true)
                            {
                                txtDocAmt.Text = Convert.ToString(Convert.ToDecimal(txtDocAmt.Text) + Convert.ToDecimal(txtPayableAmt.Text));
                            }
                            if (chkSelect.Checked == false)
                            {
                                txtDocAmt.Text = Convert.ToString(Convert.ToDecimal(txtDocAmt.Text) - Convert.ToDecimal(txtPayableAmt.Text));
                            }
                        }
                    }
                    else
                    {
                        txtPayableAmt.Text = lblPayableAmt.Text;
                        if (chkSelect.Checked == true)
                        {
                            txtDocAmt.Text = Convert.ToString(Convert.ToDecimal(txtDocAmt.Text) + Convert.ToDecimal(txtPayableAmt.Text));
                        }
                        if (chkSelect.Checked == false)
                        {
                            txtDocAmt.Text = Convert.ToString(Convert.ToDecimal(txtDocAmt.Text) - Convert.ToDecimal(txtPayableAmt.Text));
                        }
                    }



                    //txtDocAmt.Text = Convert.ToString(docamt);

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

        protected void chkSelectPB_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {



                    GridViewRow grdrow = (GridViewRow)((CheckBox)sender).NamingContainer;
                    CheckBox chkSelectPB = ((CheckBox)grdrow.FindControl("chkSelectPB"));
                    Label lblPayableAmt = ((Label)grdrow.FindControl("lblPayableAmt"));
                    TextBox txtPayableAmt = ((TextBox)grdrow.FindControl("txtPayableAmt"));

                    if (txtPayableAmt.Text != null && txtPayableAmt.Text != "" && txtPayableAmt.Text != string.Empty)
                    {
                        if (Convert.ToDecimal(txtPayableAmt.Text) > Convert.ToDecimal(lblPayableAmt.Text))
                        {
                            if (chkSelectPB.Checked == true)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Entered amount is less than or equal to payable amount.');", true);
                                chkSelectPB.Checked = false;
                            }
                            else
                            {
                                txtPayableAmt.Text = string.Empty;
                            }
                        }
                        else
                        {
                            if (chkSelectPB.Checked == true)
                            {
                                txtDocAmt.Text = Convert.ToString(Convert.ToDecimal(txtDocAmt.Text) + Convert.ToDecimal(txtPayableAmt.Text));
                                txtPayableAmt.Enabled = false;
                            }
                            if (chkSelectPB.Checked == false)
                            {
                                txtDocAmt.Text = Convert.ToString(Convert.ToDecimal(txtDocAmt.Text) - Convert.ToDecimal(txtPayableAmt.Text));
                                txtPayableAmt.Text = string.Empty;
                                txtPayableAmt.Enabled = true;
                            }
                        }
                    }
                    else
                    {
                        txtPayableAmt.Text = lblPayableAmt.Text;
                        if (chkSelectPB.Checked == true)
                        {
                            txtDocAmt.Text = Convert.ToString(Convert.ToDecimal(txtDocAmt.Text) + Convert.ToDecimal(txtPayableAmt.Text));
                            txtPayableAmt.Enabled = false;
                        }
                        if (chkSelectPB.Checked == false)
                        {
                            txtDocAmt.Text = Convert.ToString(Convert.ToDecimal(txtDocAmt.Text) - Convert.ToDecimal(txtPayableAmt.Text));
                            txtPayableAmt.Text = string.Empty;
                            txtPayableAmt.Enabled = true;
                        }
                    }



                    //txtDocAmt.Text = Convert.ToString(docamt);

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

        protected void chkSelectPBAdj_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    GridViewRow grdrow = (GridViewRow)((CheckBox)sender).NamingContainer;
                    Label lblPBTYPE = ((Label)grdrow.FindControl("lblPBTYPE"));
                    CheckBox chkSelectPBAdj = ((CheckBox)grdrow.FindControl("chkSelectPBAdj"));
                    Label lblPayableAmt = ((Label)grdrow.FindControl("lblPayableAmt"));
                    TextBox txtPayableAmt = ((TextBox)grdrow.FindControl("txtPayableAmt"));
                    if (txtPayableAmt.Text != null && txtPayableAmt.Text != "" && txtPayableAmt.Text != string.Empty)
                    {
                        if (Convert.ToDecimal(txtPayableAmt.Text) > Convert.ToDecimal(lblPayableAmt.Text))
                        {
                            if (chkSelectPBAdj.Checked == true)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Entered amount is less than or equal to payable amount.');", true);
                                chkSelectPBAdj.Checked = false;
                            }
                            else
                            {
                                txtPayableAmt.Text = string.Empty;
                            }
                        }
                        else
                        {
                            if (lblPBTYPE.Text == "MPB" || lblPBTYPE.Text == "SCR")
                            {
                                if (chkSelectPBAdj.Checked == true)
                                {
                                    txtDocAmt.Text = Convert.ToString(Convert.ToDecimal(txtDocAmt.Text) + Convert.ToDecimal(txtPayableAmt.Text));
                                    txtCRAmt.Text = Convert.ToString(Convert.ToDecimal(txtCRAmt.Text) + Convert.ToDecimal(txtPayableAmt.Text));
                                    txtPayableAmt.Enabled = false;
                                }
                                else if (chkSelectPBAdj.Checked == false)
                                {
                                    txtDocAmt.Text = Convert.ToString(Convert.ToDecimal(txtDocAmt.Text) - Convert.ToDecimal(txtPayableAmt.Text));
                                    txtCRAmt.Text = Convert.ToString(Convert.ToDecimal(txtCRAmt.Text) - Convert.ToDecimal(txtPayableAmt.Text));
                                    txtPayableAmt.Text = string.Empty;
                                    txtPayableAmt.Enabled = true;
                                }

                            }
                            if (lblPBTYPE.Text == "DN" || lblPBTYPE.Text == "BP" || lblPBTYPE.Text == "OB" || lblPBTYPE.Text == "SCM" || lblPBTYPE.Text == "SIT")
                            {
                                if (chkSelectPBAdj.Checked == true)
                                {
                                    txtDocAmt.Text = Convert.ToString(Convert.ToDecimal(txtDocAmt.Text) + Convert.ToDecimal(txtPayableAmt.Text));
                                    txtAdjAmt.Text = Convert.ToString(Convert.ToDecimal(txtAdjAmt.Text) + Convert.ToDecimal(txtPayableAmt.Text));
                                    txtDRAmt.Text = Convert.ToString(Convert.ToDecimal(txtDRAmt.Text) + Convert.ToDecimal(txtPayableAmt.Text));
                                    txtPayableAmt.Enabled = false;
                                }
                                else if (chkSelectPBAdj.Checked == false)
                                {
                                    txtDocAmt.Text = Convert.ToString(Convert.ToDecimal(txtDocAmt.Text) - Convert.ToDecimal(txtPayableAmt.Text));
                                    txtAdjAmt.Text = Convert.ToString(Convert.ToDecimal(txtAdjAmt.Text) - Convert.ToDecimal(txtPayableAmt.Text));
                                    txtDRAmt.Text = Convert.ToString(Convert.ToDecimal(txtDRAmt.Text) - Convert.ToDecimal(txtPayableAmt.Text));
                                    txtPayableAmt.Text = string.Empty;
                                    txtPayableAmt.Enabled = true;
                                }
                            }

                            txtTAdjAmt.Text = Convert.ToString(Convert.ToDecimal(txtCRAmt.Text) + Convert.ToDecimal(txtDRAmt.Text));
                        }
                    }
                    else
                    {
                        txtPayableAmt.Text = lblPayableAmt.Text;
                        if (lblPBTYPE.Text == "MPB" || lblPBTYPE.Text == "SCR")
                        {
                            if (chkSelectPBAdj.Checked == true)
                            {
                                txtDocAmt.Text = Convert.ToString(Convert.ToDecimal(txtDocAmt.Text) + Convert.ToDecimal(txtPayableAmt.Text));
                                txtCRAmt.Text = Convert.ToString(Convert.ToDecimal(txtCRAmt.Text) + Convert.ToDecimal(txtPayableAmt.Text));
                                txtPayableAmt.Enabled = false;
                            }
                            else if (chkSelectPBAdj.Checked == false)
                            {
                                txtDocAmt.Text = Convert.ToString(Convert.ToDecimal(txtDocAmt.Text) - Convert.ToDecimal(txtPayableAmt.Text));
                                txtCRAmt.Text = Convert.ToString(Convert.ToDecimal(txtCRAmt.Text) - Convert.ToDecimal(txtPayableAmt.Text));
                                txtPayableAmt.Text = string.Empty;
                                txtPayableAmt.Enabled = true;
                            }

                        }
                        if (lblPBTYPE.Text == "DN" || lblPBTYPE.Text == "BP" || lblPBTYPE.Text == "OB" || lblPBTYPE.Text == "SCM" || lblPBTYPE.Text == "SIT")
                        {
                            if (chkSelectPBAdj.Checked == true)
                            {
                                txtDocAmt.Text = Convert.ToString(Convert.ToDecimal(txtDocAmt.Text) + Convert.ToDecimal(txtPayableAmt.Text));
                                txtAdjAmt.Text = Convert.ToString(Convert.ToDecimal(txtAdjAmt.Text) + Convert.ToDecimal(txtPayableAmt.Text));
                                txtDRAmt.Text = Convert.ToString(Convert.ToDecimal(txtDRAmt.Text) + Convert.ToDecimal(txtPayableAmt.Text));
                                txtPayableAmt.Enabled = false;
                            }
                            else if (chkSelectPBAdj.Checked == false)
                            {
                                txtDocAmt.Text = Convert.ToString(Convert.ToDecimal(txtDocAmt.Text) - Convert.ToDecimal(txtPayableAmt.Text));
                                txtAdjAmt.Text = Convert.ToString(Convert.ToDecimal(txtAdjAmt.Text) - Convert.ToDecimal(txtPayableAmt.Text));
                                txtDRAmt.Text = Convert.ToString(Convert.ToDecimal(txtDRAmt.Text) - Convert.ToDecimal(txtPayableAmt.Text));
                                txtPayableAmt.Text = string.Empty;
                                txtPayableAmt.Enabled = true;
                            }
                        }

                        txtTAdjAmt.Text = Convert.ToString(Convert.ToDecimal(txtCRAmt.Text) + Convert.ToDecimal(txtDRAmt.Text));
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

        protected void chkSelectDNAdj_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chkSelectOACAdj_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void txtDiscountAmt_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (Session["USERID"] != null)
                {
                    if (txtDiscountAmt.Text != "" && txtDiscountAmt.Text != null && txtDiscountAmt.Text != string.Empty)
                    {
                        txtDocAmt.Text = Convert.ToString(Convert.ToDecimal(txtDocAmt.Text) - Convert.ToDecimal(txtDiscountAmt.Text));
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }

        }

        protected void chkSelectAllPB_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow hrow = gvPBAdjData.HeaderRow;
                    CheckBox chkSelectAllPB = (CheckBox)hrow.FindControl("chkSelectAllPB");
                    if (chkSelectAllPB.Checked == true)
                    {
                        decimal dr = 0;
                        decimal cr = 0;
                        decimal adj = 0;
                        for (int i = 0; i < gvPBAdjData.Rows.Count; i++)
                        {
                            GridViewRow row = gvPBAdjData.Rows[i];
                            Label lblPayableAmt = ((Label)row.FindControl("lblPayableAmt"));
                            CheckBox chkSelectPBAdj = ((CheckBox)row.FindControl("chkSelectPBAdj"));
                            TextBox txtPayableAmt = ((TextBox)row.FindControl("txtPayableAmt"));
                            if (txtPayableAmt.Text != null && txtPayableAmt.Text != "" && txtPayableAmt.Text != string.Empty)
                            {

                            }
                            else
                            {
                                txtPayableAmt.Text = lblPayableAmt.Text;

                            }
                            chkSelectPBAdj.Checked = true;
                            if (lblPayableAmt.Text.Contains("-") == true)
                            {
                                dr = Convert.ToDecimal(Convert.ToDecimal(dr) + Convert.ToDecimal(lblPayableAmt.Text));
                            }
                            else
                            {
                                cr = Convert.ToDecimal(Convert.ToDecimal(cr) + Convert.ToDecimal(lblPayableAmt.Text));
                            }

                        }

                        adj = cr + dr;

                        txtDRAmt.Text = Convert.ToString(dr);
                        txtCRAmt.Text = Convert.ToString(cr);
                        txtTAdjAmt.Text = Convert.ToString(adj);

                        txtDocAmt.Text = txtTAdjAmt.Text;
                        txtAdjAmt.Text = txtDRAmt.Text;
                    }
                    else
                    {

                        for (int i = 0; i < gvPBAdjData.Rows.Count; i++)
                        {
                            GridViewRow row = gvPBAdjData.Rows[i];
                            CheckBox chkSelectPBAdj = ((CheckBox)row.FindControl("chkSelectPBAdj"));
                            TextBox txtPayableAmt = ((TextBox)row.FindControl("txtPayableAmt"));
                            chkSelectPBAdj.Checked = false;
                            txtPayableAmt.Text = string.Empty;
                        }
                        txtDRAmt.Text = "0";
                        txtCRAmt.Text = "0";
                        txtTAdjAmt.Text = "0";

                        txtDocAmt.Text = "0";
                        txtAdjAmt.Text = "0";
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

        protected void btnYes_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (rblPOPB.SelectedValue == "OA")
                    {
                        //int iResult = objMainClass.InsertOnAccountBankEntry(objMainClass.intCmpId, ddlDoctype.SelectedValue, txtDocNo.Text, txtDocDate.Text, ddlBankAccount.SelectedItem.Text, 0, 1, "", txtVendCode.Text,
                        int iResult = objMainClass.InsertOnAccountBankEntry(objMainClass.intCmpId, ddlDoctype.SelectedValue, txtDocNo.Text, txtDocDate.Text, "", 0, 1, "", txtVendCode.Text,
                            txtDIscAC.Text, txtRemarks.Text, txtDiscountAmt.Text, txtDocAmt.Text, txtAdjAmt.Text, Convert.ToInt32(Session["USERID"]), txtTXNID.Text, Convert.ToInt32(ddlPaymentMode.SelectedValue), "BANKPAYMENTENTRY");
                        if (iResult == 1)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Record save sucessfully.!');$('.close').click(function(){window.location.href ='frmBankPayment.aspx' });", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Record not inserted sucessfully!');", true);
                        }

                    }
                    if (rblPOPB.SelectedValue == "PO")
                    {
                        int i = CheckSelectedPO();
                        if (i > 0 && Convert.ToDecimal(txtDocAmt.Text) > 0)
                        {
                            //int iReturn = objMainClass.InsertAdvanceBankEntry(objMainClass.intCmpId, ddlDoctype.SelectedValue, txtDocNo.Text, txtDocDate.Text, ddlBankAccount.SelectedItem.Text, 1, 0, txtVendCode.Text,
                            int iReturn = objMainClass.InsertAdvanceBankEntry(objMainClass.intCmpId, ddlDoctype.SelectedValue, txtDocNo.Text, txtDocDate.Text, "", 1, 0, txtVendCode.Text,
                                txtDIscAC.Text, txtRemarks.Text, txtDiscountAmt.Text, Convert.ToInt32(Session["USERID"]), txtTXNID.Text, Convert.ToInt32(ddlPaymentMode.SelectedValue), gvPOList, "BANKPAYMENTENTRY");
                            if (iReturn == 1)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Record save sucessfully.!');$('.close').click(function(){window.location.href ='frmBankPayment.aspx' });", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Record not inserted sucessfully!');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please select PO from PO list.!');", true);
                        }

                    }
                    if (rblPOPB.SelectedValue == "PB")
                    {
                        int i = CheckSelectedPB();
                        if (i > 0)  //&& Convert.ToDecimal(txtDocAmt.Text) > 0)
                        {
                            //int iReturn = objMainClass.InsertPBBankEntry(objMainClass.intCmpId, ddlDoctype.SelectedValue, txtDocNo.Text, txtDocDate.Text, ddlBankAccount.SelectedItem.Text, 0, 0, txtVendCode.Text,
                            int iReturn = objMainClass.InsertPBBankEntry(objMainClass.intCmpId, ddlDoctype.SelectedValue, txtDocNo.Text, txtDocDate.Text, "", 0, 0, txtVendCode.Text,
                                txtDIscAC.Text, txtRemarks.Text, txtDiscountAmt.Text, txtDocAmt.Text, Convert.ToInt32(Session["USERID"]), txtTXNID.Text, Convert.ToInt32(ddlPaymentMode.SelectedValue), gvPBAdjData, "BANKPAYMENTENTRY");
                            if (iReturn == 1)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Record save sucessfully.!');$('.close').click(function(){window.location.href ='frmBankPayment.aspx' });", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Record not inserted sucessfully!');", true);
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

        protected void btnNo_Click(object sender, EventArgs e)
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
    }
}