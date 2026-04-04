using Microsoft.VisualStudio.Shell.Interop;
using Newtonsoft.Json;
using RestSharp;
using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.CRM
{
    public partial class frmVendorMaster : System.Web.UI.Page
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

                        divDealer.Visible = true;
                        divMobileNo.Visible = true;
                        divContactPerson.Visible = true;
                        divContactNo.Visible = true;
                        divSuggestedName.Visible = true;
                        divNext2.Visible = true;

                        objBindDDL.FillVendType(ddlVendType);
                        ddlVendType.SelectedValue = "DOM";
                        ddlVendType.Enabled = true;

                        objBindDDL.FillLists(ddlVendCategory, "VC");
                        ddlVendCategory.SelectedValue = "7";
                        ddlVendCategory.Enabled = true;

                        objBindDDL.FillDealer(ddlDealer, 1);

                        objBindDDL.FillState(ddlState);
                        objBindDDL.FillCountry(1, ddlCountry);
                        objBindDDL.FillLists(ddlAccountType, "ACT");
                        objBindDDL.FillLists(ddlUPIWalletType, "UPI");
                        objBindDDL.FillLists(ddlDesignation, "DI");
                        ddlDesignation.SelectedValue = "171";
                        objBindDDL.FillLists(ddlTallyGroup, "TG");
                        if (Convert.ToInt32(Session["STCD"]) == 18)
                        {
                            ddlTallyGroup.SelectedValue = "12027";
                        }
                        else
                        {
                            ddlTallyGroup.SelectedValue = "11928";
                        }
                        ddlDesignation.Enabled = false;
                        dvPersonalDetail.Visible = true;
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

        protected void lnkNext1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    divDealer.Visible = true;
                    divMobileNo.Visible = true;
                    divContactPerson.Visible = true;
                    divContactNo.Visible = true;
                    divSuggestedName.Visible = true;
                    divNext2.Visible = true;

                    divVendType.Visible = false;
                    divVendCat.Visible = false;
                    divVendCode.Visible = false;
                    divNext1.Visible = false;
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

        protected void txtContactPerson_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (txtContactPerson.Text != "" && txtContactPerson.Text != null && txtContactPerson.Text != string.Empty)
                    {
                        txtSuggestedName.Text = txtContactPerson.Text + " " + ddlDealer.SelectedItem.Text;
                    }
                    else
                    {
                        rfvPersonName.Visible = true;
                        txtContactPerson.Focus();
                        ScriptManager.RegisterStartupScript(this,
                                                     this.GetType(),
                                                     "FocusScript",
                                                     "setTimeout(function(){$get('" + txtContactPerson.ClientID + "').focus();}, 100);",
                                                     true);
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

        protected void lnkPrev2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    divDealer.Visible = false;
                    divMobileNo.Visible = false;
                    divContactPerson.Visible = false;
                    divContactNo.Visible = false;
                    divSuggestedName.Visible = false;
                    divNext2.Visible = false;

                    divVendType.Visible = true;
                    divVendCat.Visible = true;
                    divVendCode.Visible = true;
                    divNext1.Visible = true;
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

        protected void lnkNext2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    dvPersonalDetail.Visible = false;
                    dvContactDetail1.Visible = true;

                    divAddress1.Visible = true;
                    divAddress2.Visible = true;
                    divAddress3.Visible = true;
                    divNext3.Visible = true;


                    divDealer.Visible = false;
                    divMobileNo.Visible = false;
                    divContactPerson.Visible = false;
                    divContactNo.Visible = false;
                    divSuggestedName.Visible = false;
                    divNext2.Visible = false;
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

        protected void lnkPrev3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    dvContactDetail1.Visible = false;
                    dvPersonalDetail.Visible = true;

                    divDealer.Visible = true;
                    divMobileNo.Visible = true;
                    divContactPerson.Visible = true;
                    divContactNo.Visible = true;
                    divSuggestedName.Visible = true;
                    divNext2.Visible = true;


                    divAddress1.Visible = false;
                    divAddress2.Visible = false;
                    divAddress3.Visible = false;
                    divNext3.Visible = false;
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

        protected void lnkNext3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    dvContactDetail1.Visible = false;
                    dvContactDetail2.Visible = true;

                    divAddress1.Visible = false;
                    divAddress2.Visible = false;
                    divAddress3.Visible = false;
                    divNext3.Visible = false;

                    divPincode.Visible = true;
                    divState.Visible = true;
                    divCity.Visible = true;
                    divCountry.Visible = true;
                    divNext4.Visible = true;


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

        protected void lnkPrev4_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    dvContactDetail2.Visible = false;
                    dvContactDetail1.Visible = true;

                    divAddress1.Visible = true;
                    divAddress2.Visible = true;
                    divAddress3.Visible = true;
                    divNext3.Visible = true;

                    divPincode.Visible = false;
                    divState.Visible = false;
                    divCity.Visible = false;
                    divCountry.Visible = false;
                    divNext4.Visible = false;

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

        protected void lnkNext4_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    dvContactDetail2.Visible = false;
                    dvBuisnessDetail1.Visible = true;

                    divPincode.Visible = false;
                    divState.Visible = false;
                    divCity.Visible = false;
                    divCountry.Visible = false;
                    divNext4.Visible = false;

                    divCommunication.Visible = true;
                    divManagerName.Visible = true;
                    divManagerContact.Visible = true;
                    divManagerEmail.Visible = true;
                    divNext5.Visible = true;
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

        protected void lnkPrev5_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    dvBuisnessDetail1.Visible = false;
                    dvContactDetail2.Visible = true;


                    divPincode.Visible = true;
                    divState.Visible = true;
                    divCity.Visible = true;
                    divCountry.Visible = true;
                    divNext4.Visible = true;

                    divCommunication.Visible = false;
                    divManagerName.Visible = false;
                    divManagerContact.Visible = false;
                    divManagerEmail.Visible = false;
                    divNext5.Visible = false;
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

        protected void lnkNext5_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    dvBuisnessDetail1.Visible = false;
                    dvBuisnessDetail2.Visible = true;


                    divCommunication.Visible = false;
                    divManagerName.Visible = false;
                    divManagerContact.Visible = false;
                    divManagerEmail.Visible = false;
                    divNext5.Visible = false;

                    if (txtManagerName.Text != string.Empty && txtManagerName.Text != "" && txtManagerName.Text != null)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Designation"), new DataColumn("Name"), new DataColumn("Contact"), new DataColumn("Email") });
                        dt.Rows.Add(ddlDesignation.SelectedValue, txtManagerName.Text, txtManagerContact.Text, txtManagerEmail.Text);
                        ViewState["Manager"] = dt;
                        this.BindGrid();
                    }

                    divEmail.Visible = true;
                    divWebsite.Visible = true;
                    divAgreement.Visible = true;
                    divSelling.Visible = false;
                    divNext6.Visible = true;
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

        protected void lnkPrev6_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    dvBuisnessDetail2.Visible = false;
                    dvBuisnessDetail1.Visible = true;

                    divCommunication.Visible = true;
                    divManagerName.Visible = true;
                    divManagerContact.Visible = true;
                    divManagerEmail.Visible = true;
                    divNext5.Visible = true;


                    divEmail.Visible = false;
                    divWebsite.Visible = false;
                    divAgreement.Visible = false;
                    divSelling.Visible = false;
                    divNext6.Visible = false;


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

        protected void BindGrid()
        {
            GridView1.DataSource = (DataTable)ViewState["Manager"];
            GridView1.DataBind();
        }

        protected void lnkNext6_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    dvBuisnessDetail2.Visible = false;
                    dvBuisnessDetail3.Visible = true;

                    divEmail.Visible = false;
                    divWebsite.Visible = false;
                    divAgreement.Visible = false;
                    divSelling.Visible = false;
                    divNext6.Visible = false;

                    divPAN.Visible = true;
                    divFUPAN.Visible = true;
                    divAadhar.Visible = true;
                    divIDProof.Visible = true;
                    divNext7.Visible = true;

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

        protected void lnkPrev7_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    dvBuisnessDetail2.Visible = true;
                    dvBuisnessDetail3.Visible = false;

                    divEmail.Visible = true;
                    divWebsite.Visible = true;
                    divAgreement.Visible = true;
                    divSelling.Visible = false;
                    divNext6.Visible = true;

                    divPAN.Visible = false;
                    divFUPAN.Visible = false;
                    divAadhar.Visible = false;
                    divIDProof.Visible = false;
                    divNext7.Visible = false;
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

        protected void lnkNext7_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    dvBuisnessDetail3.Visible = false;
                    dvBuisnessDetail4.Visible = true;

                    divPAN.Visible = false;
                    divFUPAN.Visible = false;
                    divAadhar.Visible = false;
                    divIDProof.Visible = false;
                    divNext7.Visible = false;

                    if (fuPAN != null)
                    {
                        if (fuPAN.HasFiles)
                        {
                            Session["FUPAN"] = fuPAN;
                        }
                    }

                    if (fuIDProof != null)
                    {
                        if (fuIDProof.HasFiles)
                        {
                            Session["FUIDPROOF"] = fuIDProof;
                        }
                    }



                    divMaginScheme.Visible = true;
                    divGST.Visible = true;
                    divFUGST.Visible = true;
                    divMSME.Visible = true;
                    divFUMSME.Visible = true;
                    divNext8.Visible = true;
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

        protected void lnkPrev8_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    dvBuisnessDetail4.Visible = false;
                    dvBuisnessDetail3.Visible = true;

                    divPAN.Visible = true;
                    divFUPAN.Visible = true;
                    divAadhar.Visible = true;
                    divIDProof.Visible = true;
                    divNext7.Visible = true;

                    divMaginScheme.Visible = false;
                    divGST.Visible = false;
                    divFUGST.Visible = false;
                    divMSME.Visible = false;
                    divFUMSME.Visible = false;
                    divNext8.Visible = false;
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

        protected void lnkNext8_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    dvBuisnessDetail4.Visible = false;
                    dvBankDetail1.Visible = true;

                    divMaginScheme.Visible = false;
                    divGST.Visible = false;
                    divFUGST.Visible = false;
                    divMSME.Visible = false;
                    divFUMSME.Visible = false;
                    divNext8.Visible = false;


                    if (fuGSTCerti != null)
                    {
                        if (fuGSTCerti.HasFiles)
                        {
                            Session["FUGSTCERTI"] = fuGSTCerti;
                        }
                    }

                    if (fuMSMECerti != null)
                    {
                        if (fuMSMECerti.HasFiles)
                        {
                            Session["FUMSMECERTI"] = fuMSMECerti;
                        }
                    }


                    divIFSC.Visible = true;
                    divBank.Visible = true;
                    divACNo.Visible = true;
                    divAccType.Visible = true;
                    divCheque.Visible = true;
                    divUPIWallet.Visible = true;
                    divWalletNo.Visible = true;
                    divWalletOwner.Visible = true;
                    divNext9.Visible = true;

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

        protected void lnkPrev9_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    dvBankDetail1.Visible = false;
                    dvBuisnessDetail4.Visible = true;

                    divMaginScheme.Visible = true;
                    divGST.Visible = true;
                    divFUGST.Visible = true;
                    divMSME.Visible = true;
                    divFUMSME.Visible = true;
                    divNext8.Visible = true;

                    divIFSC.Visible = false;
                    divBank.Visible = false;
                    divACNo.Visible = false;
                    divAccType.Visible = false;
                    divNext9.Visible = false;
                    divCheque.Visible = false;
                    divUPIWallet.Visible = false;
                    divWalletNo.Visible = false;
                    divWalletOwner.Visible = false;
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

        protected void lnkNext9_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    dvBankDetail1.Visible = false;
                    dvBankDetail2.Visible = true;

                    divIFSC.Visible = false;
                    divBank.Visible = false;
                    divACNo.Visible = false;
                    divAccType.Visible = false;
                    divCheque.Visible = false;
                    divNext9.Visible = false;

                    divUPIWallet.Visible = true;
                    divWalletNo.Visible = true;
                    divWalletOwner.Visible = true;
                    dviFinal.Visible = true;

                    if (fuCheque != null)
                    {
                        if (fuCheque.HasFiles)
                        {
                            Session["FUCHEQUE"] = fuCheque;
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

        protected void lnkPrev10_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    dvBankDetail2.Visible = false;
                    dvBankDetail1.Visible = true;

                    divIFSC.Visible = true;
                    divBank.Visible = true;
                    divACNo.Visible = true;
                    divAccType.Visible = true;
                    divNext9.Visible = true;
                    divCheque.Visible = true;

                    divUPIWallet.Visible = false;
                    divWalletNo.Visible = false;
                    divWalletOwner.Visible = false;
                    dviFinal.Visible = false;
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
            try
            {
                if (Session["USERID"] != null)
                {
                    if (ddlUPIWalletType.SelectedIndex > 0 || (txtIFSCCode.Text != "" && txtIFSCCode.Text != string.Empty))
                    {
                        if (fuCheque != null)
                        {
                            if (fuCheque.HasFiles)
                            {
                                Session["FUCHEQUE"] = fuCheque;
                            }
                        }

                        string TITLE = "M/s";
                        string INDKEY = "S";
                        string CURRCODE = "INR";
                        string RECAC = "20020001";
                        string PAYMETHOD = "2";
                        string PAYBLKKEY = "F";
                        string TDSCOUNTRY = "IN";

                        string EXVENDTYPE = "DI";
                        string REFTYPE = "VM";
                        string ADDOF = "G";

                        byte[] IDPROOF = null;
                        byte[] PAN = null;
                        byte[] CHEQUE = null;
                        byte[] SHOP = null;
                        byte[] GSTCERTI = null;
                        byte[] MSMECERTI = null;

                        FileUpload fudIDPROOF;
                        FileUpload fudPAN;
                        FileUpload fudCHEQUE;
                        FileUpload fudGST;
                        FileUpload fudMSME;

                        string IDPROOFTYPE = ".jpeg";
                        string PANTYPE = ".jpeg";
                        string CHEQUETYPE = ".jpeg";
                        string GSTCERTITYPE = ".pdf";
                        string MSMECERTITYPE = ".pdf";



                        if (Session["FUIDPROOF"] != null)
                        {
                            fudIDPROOF = Session["FUIDPROOF"] as FileUpload;

                            if (fudIDPROOF.HasFiles)
                            {
                                BinaryReader br1 = new BinaryReader(fudIDPROOF.PostedFile.InputStream);

                                IDPROOF = br1.ReadBytes(fudIDPROOF.PostedFile.ContentLength);
                                IDPROOFTYPE = System.IO.Path.GetExtension(fudIDPROOF.FileName);
                            }
                        }


                        //objMainClass.InsertVendImage("11036", 11317, IDPROOF, 2, "INSERTIMAGE");
                        // objMainClass.InsertVendImage("10747", 11317, IDPROOF, 2, "INSERTIMAGE");

                        if (Session["FUPAN"] != null)
                        {
                            fudPAN = Session["FUPAN"] as FileUpload;
                            if (fudPAN.HasFiles)
                            {
                                using (BinaryReader br = new BinaryReader(fudPAN.PostedFile.InputStream))
                                {
                                    PAN = br.ReadBytes(fudPAN.PostedFile.ContentLength);
                                    PANTYPE = System.IO.Path.GetExtension(fudPAN.FileName);
                                }
                            }
                        }


                        if (Session["FUCHEQUE"] != null)
                        {
                            fudCHEQUE = Session["FUCHEQUE"] as FileUpload;
                            if (fudCHEQUE.HasFiles)
                            {
                                using (BinaryReader br = new BinaryReader(fudCHEQUE.PostedFile.InputStream))
                                {
                                    CHEQUE = br.ReadBytes(fudCHEQUE.PostedFile.ContentLength);
                                    CHEQUETYPE = System.IO.Path.GetExtension(fudCHEQUE.FileName);
                                }
                            }
                        }

                        if (Session["FUGSTCERTI"] != null)
                        {

                            fudGST = Session["FUGSTCERTI"] as FileUpload;
                            if (fudGST.HasFiles)
                            {
                                using (BinaryReader br = new BinaryReader(fudGST.PostedFile.InputStream))
                                {
                                    GSTCERTI = br.ReadBytes(fudGST.PostedFile.ContentLength);
                                    GSTCERTITYPE = System.IO.Path.GetExtension(fudGST.FileName);
                                }
                            }
                        }

                        if (Session["FUMSMECERTI"] != null)
                        {
                            fudMSME = Session["FUMSMECERTI"] as FileUpload;
                            if (fudMSME.HasFiles)
                            {
                                using (BinaryReader br = new BinaryReader(fudMSME.PostedFile.InputStream))
                                {
                                    MSMECERTI = br.ReadBytes(fudMSME.PostedFile.ContentLength);
                                    MSMECERTITYPE = System.IO.Path.GetExtension(fudMSME.FileName);
                                }
                            }
                        }


                        //if (rblUnderMarginScheme.SelectedValue == "0")
                        //{
                        //    ddlVendCategory.SelectedValue = "11240";
                        //}
                        //else if (rblUnderMarginScheme.SelectedValue == "1")
                        //{
                        //    ddlVendCategory.SelectedValue = "11239";
                        //}

                        string DOCNO = objMainClass.InsertVendor(objMainClass.intCmpId, "", "", ddlVendType.SelectedValue, TITLE, txtSuggestedName.Text, "", txtContactPerson.Text, 0, "", "", "",
                                   "", INDKEY, "", "", 0, CURRCODE, "", "", "", "", RECAC, "", PAYMETHOD, PAYBLKKEY, "", "", "", "0", "", TDSCOUNTRY, "", "", "", "", "", "", "", "", "", "",
                                   EXVENDTYPE, txtPAN.Text, Convert.ToInt32(ddlVendCategory.SelectedValue), 0, 0, "", Convert.ToInt32(Session["USERID"]), txtGST.Text, 2, txtBankName.Text,
                                   txtACNo.Text, txtIFSCCode.Text, Convert.ToInt32(rblUnderMarginScheme.SelectedValue), txtAddress1.Text, txtAddress2.Text, txtAddress3.Text, txtCity.Text,
                                   Convert.ToInt32(ddlState.SelectedValue), ddlCountry.SelectedValue, txtPincode.Text, txtContactPerson.Text, txtContactNo.Text, txtContactNo.Text, REFTYPE,
                                   ADDOF, IDPROOF, PAN, CHEQUE, SHOP, Convert.ToInt32(ddlDealer.SelectedValue), txtAadharNo.Text, Convert.ToInt32(ddlAccountType.SelectedValue),
                                   Convert.ToInt32(ddlUPIWalletType.SelectedValue), txtWalletPayNo.Text, txtWalletOwner.Text, Convert.ToInt32(rblAgreement.SelectedValue),
                                   Convert.ToInt32(rblMobileSelling.SelectedValue), Convert.ToInt32(rblMSME.SelectedValue), GSTCERTI,
                                   MSMECERTI, 0, Convert.ToInt32(ddlTallyGroup.SelectedValue), IDPROOFTYPE, PANTYPE, CHEQUETYPE, GSTCERTITYPE, MSMECERTITYPE, GridView1);

                        if (DOCNO != "" && DOCNO != string.Empty && DOCNO != null)
                        {
                            //bool IsSent = false;
                            //MailMessage mail = new MailMessage();
                            //mail.From = new MailAddress("customercare@qarmatek.com", "Vendor Registration");
                            ////mail.To.Add("tejas@qarmatek.com");

                            //mail.To.Add("mohit.diwakar@qarmatek.com");
                            ////mail.To.Add("accounts@qarmatek.com");
                            ////mail.CC.Add("vihar.jethavani @qarmatek.com");
                            ////mail.Bcc.Add("mohit.diwakar@qarmatek.com");
                            ////mail.Bcc.Add("dhaval.vakta@qarmatek.com");
                            ////mail.CC.Add("kushal.shah@qarmatek.com");
                            ////mail.CC.Add("nipun.shah@qarmatek.com");
                            ////mail.CC.Add("jpvaishnav@qarmatek.com");
                            ////mail.Bcc.Add("sentitem@qarmatek.com");
                            //mail.Subject = "Vendor Registration";
                            //mail.Body = "New Vendor Created. " + System.Environment.NewLine + "</br></br> Vendor Code is : " + objMainClass.strConvertZeroPadding(DOCNO) + " " + System.Environment.NewLine + "</br></br> " + System.Environment.NewLine;
                            //mail.IsBodyHtml = true;
                            //SmtpClient SmtpServer = new SmtpClient();
                            //SmtpServer.Port = 587;
                            ////SmtpServer.Host = "smtp.office365.com";
                            //SmtpServer.Host = "smtp.gmail.com";
                            //SmtpServer.Credentials = new System.Net.NetworkCredential("customercare@qarmatek.com", "Saz43287");
                            //SmtpServer.EnableSsl = true;
                            //SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                            //SmtpServer.Send(mail);
                            //IsSent = true;
                            ////return IsSent;
                            ///

                            //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                            MailMessage Msg = new MailMessage();
                            //Msg.To.Add("accounts@qarmatek.com");
                            //Msg.CC.Add("vihar.jethavani @qarmatek.com");
                            //Msg.To.Add("shashin.mehta@qarmatek.com");
                            Msg.To.Add("accounts2@qarma-tek.com");
                            Msg.To.Add("ac4@qarmatek.com");
                            Msg.CC.Add("accounts@qarmatek.com");
                            Msg.CC.Add("mohit.diwakar@qarmatek.com");
                            Msg.From = new MailAddress("info@qarmatek.com", "Vendor Registration");
                            Msg.Subject = "Vendor Registration";
                            Msg.Body = "New Vendor Created. " + System.Environment.NewLine + "</br></br> Vendor Code is : " + objMainClass.strConvertZeroPadding(DOCNO) + " " + System.Environment.NewLine + "</br></br> " + System.Environment.NewLine;
                            Msg.IsBodyHtml = true;
                            SmtpClient smtp = new SmtpClient();
                            smtp.Host = "smtp.office365.com";
                            smtp.EnableSsl = true;

                            smtp.Port = 587;

                            //smtp.Port = 25;
                            //smtp.UseDefaultCredentials = true;

                            smtp.Credentials = new System.Net.NetworkCredential("info@qarmatek.com", "Hof75626");
                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.Send(Msg);

                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Vendor registered successfully! \");$('.close').click(function(){window.location.href ='frmViewVendorMaster.aspx' });", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Vendor not registered sucessfully!');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Enter either Bank Details or UPI Details.');", true);
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




        protected void txtPincode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (txtPincode.Text.Length == 6)
                    {
                        try
                        {

                            if (!System.Text.RegularExpressions.Regex.IsMatch(txtPincode.Text, "[0-9]{6}$"))
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Only Numbers Aloowed.');", true);
                                txtPincode.Text = string.Empty;
                            }
                            else
                            {
                                DataTable ds = new DataTable();
                                ds = objMainClass.SELECT_CITY_BYPINCODE(txtPincode.Text.Trim());
                                if (ds.Rows.Count > 0)
                                {
                                    ddlState.SelectedValue = ds.Rows[0]["STATE_ID"].ToString();
                                    txtCity.Text = ds.Rows[0]["CITY_NAME"].ToString();
                                }
                                else
                                {
                                    ddlState.SelectedIndex = 0;
                                    txtCity.Text = "";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
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

        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (ddlDealer.SelectedIndex > 0)
                    {
                        DataTable dt = new DataTable();
                        dt = objMainClass.FetchDealerData(Convert.ToInt32(ddlDealer.SelectedValue), "FETCHDATA");

                        if (dt.Rows.Count > 0)
                        {
                            txtAddress1.Text = Convert.ToString(dt.Rows[0]["ADDR1"]);
                            txtAddress2.Text = Convert.ToString(dt.Rows[0]["ADDR2"]);
                            txtAddress3.Text = Convert.ToString(dt.Rows[0]["ADDR3"]);
                            txtPincode.Text = Convert.ToString(dt.Rows[0]["POSTALCODE"]);
                            txtMobileNo.Text = Convert.ToString(dt.Rows[0]["CONTACTNO"]);
                            ddlState.SelectedValue = Convert.ToString(dt.Rows[0]["STCD"]);
                            txtCity.Text = Convert.ToString(dt.Rows[0]["CITY"]);
                            ddlCountry.SelectedValue = Convert.ToString(dt.Rows[0]["CNCD"]);

                        }
                        else
                        {
                            txtAddress1.Text = string.Empty;
                            txtAddress2.Text = string.Empty;
                            txtAddress3.Text = string.Empty;
                            txtPincode.Text = string.Empty;

                            ddlState.SelectedIndex = 0;
                            txtCity.Text = string.Empty;
                            ddlCountry.SelectedIndex = 0;

                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Dealer not registered!');", true);
                        }
                    }
                    else
                    {
                        txtAddress1.Text = string.Empty;
                        txtAddress2.Text = string.Empty;
                        txtAddress3.Text = string.Empty;
                        txtPincode.Text = string.Empty;

                        ddlState.SelectedIndex = 0;
                        txtCity.Text = string.Empty;
                        ddlCountry.SelectedIndex = 0;
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

        //protected void txtBankName_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Session["USERID"] != null)
        //        {
        //            if (txtBankName.Text != string.Empty && txtBankName.Text != "" && txtBankName.Text != null)
        //            {
        //                rfvBankName.Enabled = true;
        //                rfvAccountNo.Enabled = true;
        //                rfvIFSCCode.Enabled = true;
        //                rfvAccountType.Enabled = true;
        //            }
        //            else
        //            {
        //                rfvBankName.Enabled = false;
        //                rfvAccountNo.Enabled = false;
        //                rfvIFSCCode.Enabled = false;
        //                rfvAccountType.Enabled = false;

        //                txtBankName.Text = string.Empty;
        //                txtACNo.Text = string.Empty;
        //                txtIFSCCode.Text = string.Empty;
        //                ddlAccountType.SelectedIndex = 0;
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
        //}

        //protected void txtACNo_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Session["USERID"] != null)
        //        {
        //            if (txtACNo.Text != string.Empty && txtACNo.Text != "" && txtACNo.Text != null)
        //            {
        //                rfvBankName.Enabled = true;
        //                rfvAccountNo.Enabled = true;
        //                rfvIFSCCode.Enabled = true;
        //                rfvAccountType.Enabled = true;
        //            }
        //            else
        //            {
        //                rfvBankName.Enabled = false;
        //                rfvAccountNo.Enabled = false;
        //                rfvIFSCCode.Enabled = false;
        //                rfvAccountType.Enabled = false;

        //                txtBankName.Text = string.Empty;
        //                txtACNo.Text = string.Empty;
        //                txtIFSCCode.Text = string.Empty;
        //                ddlAccountType.SelectedIndex = 0;
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
        //}

        protected void txtIFSCCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (txtIFSCCode.Text != string.Empty && txtIFSCCode.Text != "" && txtIFSCCode.Text != null)
                    {
                        DataTable Bankdt = new DataTable();

                        var client = new RestClient(("https://ifsc.firstatom.org/key/84NnS05xbn6U5RKP1dGEBU83g/ifsc/" + txtIFSCCode.Text));
                        client.Timeout = -1;
                        var request = new RestRequest(Method.GET);
                        IRestResponse response = client.Execute(request);
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            string jsonconn = response.Content;
                            jsonconn = "[" + jsonconn + "]";
                            Bankdt = (DataTable)JsonConvert.DeserializeObject(jsonconn, (typeof(DataTable)));


                            if (Bankdt.Rows.Count > 0)
                            {
                                txtBankName.Text = Convert.ToString(Bankdt.Rows[0]["Bank"]) + " - " + Convert.ToString(Bankdt.Rows[0]["Branch"]);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Incorrect IFSC Code. Bank Details Not Found!');", true);
                            }
                            rfvIFSCCode.Enabled = true;
                            rfvBankName.Enabled = true;
                            rfvAccountNo.Enabled = true;
                            rfvAccountType.Enabled = true;
                            rfvFUCheque.Enabled = true;
                            rfvWalletType.Enabled = false;
                            rfvWalletePayNo.Enabled = false;
                            rfvWalleteOwnerName.Enabled = false;
                        }
                        else
                        {

                            rfvIFSCCode.Enabled = true;
                            rfvBankName.Enabled = true;
                            rfvAccountNo.Enabled = true;
                            rfvAccountType.Enabled = true;
                            rfvFUCheque.Enabled = true;
                            //rfvWalletType.Enabled = true;
                            //rfvWalletePayNo.Enabled = true;
                            //rfvWalleteOwnerName.Enabled = true;

                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Incorrect IFSC Code!');", true);
                        }
                    }
                    else
                    {
                        rfvIFSCCode.Enabled = false;
                        rfvBankName.Enabled = false;
                        rfvAccountNo.Enabled = false;
                        rfvAccountType.Enabled = false;
                        rfvFUCheque.Enabled = false;

                        rfvWalletType.Enabled = true;
                        rfvWalletePayNo.Enabled = true;
                        rfvWalleteOwnerName.Enabled = true;
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please Enter IFSC Code!');", true);
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

        //protected void ddlAccountType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Session["USERID"] != null)
        //        {
        //            if (ddlAccountType.SelectedIndex > 0)
        //            {
        //                rfvBankName.Enabled = true;
        //                rfvAccountNo.Enabled = true;
        //                rfvIFSCCode.Enabled = true;
        //                rfvAccountType.Enabled = true;
        //            }
        //            else
        //            {
        //                rfvBankName.Enabled = false;
        //                rfvAccountNo.Enabled = false;
        //                rfvIFSCCode.Enabled = false;
        //                rfvAccountType.Enabled = false;

        //                txtBankName.Text = string.Empty;
        //                txtACNo.Text = string.Empty;
        //                txtIFSCCode.Text = string.Empty;
        //                ddlAccountType.SelectedIndex = 0;
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
        //}

        protected void ddlUPIWalletType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (ddlUPIWalletType.SelectedIndex > 0)
                    {
                        rfvIFSCCode.Enabled = false;
                        rfvBankName.Enabled = false;
                        rfvAccountNo.Enabled = false;
                        rfvAccountType.Enabled = false;
                        rfvFUCheque.Enabled = false;
                        rfvWalletType.Enabled = true;
                        rfvWalletePayNo.Enabled = true;
                        rfvWalleteOwnerName.Enabled = true;
                    }
                    else
                    {
                        rfvIFSCCode.Enabled = true;
                        rfvBankName.Enabled = true;
                        rfvAccountNo.Enabled = true;
                        rfvAccountType.Enabled = true;
                        rfvFUCheque.Enabled = true;

                        rfvWalletType.Enabled = false;
                        rfvWalletePayNo.Enabled = false;
                        rfvWalleteOwnerName.Enabled = false;

                        ddlUPIWalletType.SelectedIndex = 0;
                        txtWalletPayNo.Text = string.Empty;
                        txtWalletOwner.Text = string.Empty;

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

        protected void txtWalletPayNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (txtWalletPayNo.Text != string.Empty && txtWalletPayNo.Text != "" && txtWalletPayNo.Text != null)
                    {
                        rfvWalletType.Enabled = true;
                        rfvWalletePayNo.Enabled = true;
                        rfvWalleteOwnerName.Enabled = true;
                    }
                    else
                    {
                        rfvWalletType.Enabled = false;
                        rfvWalletePayNo.Enabled = false;
                        rfvWalleteOwnerName.Enabled = false;

                        ddlUPIWalletType.SelectedIndex = 0;
                        txtWalletPayNo.Text = string.Empty;
                        txtWalletOwner.Text = string.Empty;

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

        protected void txtWalletOwner_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (txtWalletOwner.Text != string.Empty && txtWalletOwner.Text != "" && txtWalletOwner.Text != null)
                    {
                        rfvWalletType.Enabled = true;
                        rfvWalletePayNo.Enabled = true;
                        rfvWalleteOwnerName.Enabled = true;
                    }
                    else
                    {
                        rfvWalletType.Enabled = false;
                        rfvWalletePayNo.Enabled = false;
                        rfvWalleteOwnerName.Enabled = false;

                        ddlUPIWalletType.SelectedIndex = 0;
                        txtWalletPayNo.Text = string.Empty;
                        txtWalletOwner.Text = string.Empty;
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

        protected void rblUnderMarginScheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (rblUnderMarginScheme.SelectedValue == "0")
                    {
                        rfvGST.Enabled = false;
                        rfvFUGST.Enabled = false;
                        rgvGSTNo.Enabled = false;
                    }
                    else
                    {
                        rfvGST.Enabled = true;
                        rfvFUGST.Enabled = true;
                        rgvGSTNo.Enabled = true;
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                }
                txtGST.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }

        }

        protected void rblMSME_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (rblMSME.SelectedValue == "0")
                    {
                        rfvMSMECerti.Enabled = false;
                    }
                    else
                    {
                        rfvMSMECerti.Enabled = true;
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

        //10-02-2023 swetal
        [WebMethod]
        public static string GetPanno(string panno)
        {
            string status = "false";
            try
            {
                MainClass objMain = new MainClass();
                var detail = objMain.GetExistsPannos(panno);
                if (detail != null && detail.Rows != null && detail.Rows.Count > 0)
                {
                    status = "true";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonConvert.SerializeObject(status);
        }

        //10-02-2023

        [WebMethod]

        public static string GetAdharNo(string aadharno)
        {
            string status1 = "false";
            try
            {
                MainClass objMain = new MainClass();
                var detail = objMain.GetexistsAdharNo(aadharno);
                if (detail != null && detail.Rows != null && detail.Rows.Count > 0)
                {
                    status1 = "true";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonConvert.SerializeObject(status1);
        }
        [WebMethod]
        public static string GetGSTNo(string GSTNO)
        {
            string status3 = "false";
            try
            {
                MainClass objMain = new MainClass();
                var detail = objMain.GetExistsGSTNO(GSTNO);
                if (detail != null && detail.Rows != null && detail.Rows.Count > 0)
                {
                    status3 = "true";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonConvert.SerializeObject(status3);
        }

        [WebMethod]
        public static string GetVendorName(string NAME1)
        {
            string status4 = "false";
            try
            {
                MainClass objMain = new MainClass();
                var detail = objMain.GetVendorNAME(NAME1);
                if (detail != null && detail.Rows != null && detail.Rows.Count > 0)
                {
                    status4 = "true";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonConvert.SerializeObject(status4);
        }

        [WebMethod]

        public static string GETPAYMENTNO(string SHORTNM)
        {
            string status5 = "false";
            try
            {
                MainClass objMain = new MainClass();
                var detail = objMain.GetPaymentTo(SHORTNM);
                if (detail != null && detail.Rows != null && detail.Rows.Count > 0)
                {
                    status5 = "true";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonConvert.SerializeObject(status5);
        }

        [WebMethod]
        public static string GetAccountNumber(string ACCOUNTNO)
        {
            string status4 = "false";
            try
            {
                MainClass objMain = new MainClass();
                var detail = objMain.GetAccountNumber(ACCOUNTNO);
                if (detail != null && detail.Rows != null && detail.Rows.Count > 0)
                {
                    status4 = "true";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonConvert.SerializeObject(status4);
        }
    }
}























