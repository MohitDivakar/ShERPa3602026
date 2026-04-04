using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.CRM
{
    public partial class frmBikerVendorReg : System.Web.UI.Page
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

                        //objBindDDL.FillCountry(1, ddlCountry);
                        //objBindDDL.FillState(ddlState);
                        objBindDDL.FillDealer(ddlDealer, 1);

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

                divDealer.Visible = false;
                divContactPerson.Visible = false;
                divContactNo.Visible = false;
                divSuggestedName.Visible = false;
                divNext1.Visible = false;

                divPAN.Visible = true;
                divGST.Visible = true;
                divMaginScheme.Visible = true;
                divNext5.Visible = true;

                ScriptManager.RegisterStartupScript(this,
                                                 this.GetType(),
                                                 "FocusScript",
                                                 "setTimeout(function(){$get('" + txtPAN.ClientID + "').focus();}, 100);",
                                                 true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }



        protected void lnkPrevious5_Click(object sender, EventArgs e)
        {
            try
            {
                divDealer.Visible = true;
                divContactPerson.Visible = true;
                divContactNo.Visible = true;
                divSuggestedName.Visible = true;
                divNext1.Visible = true;

                divPAN.Visible = false;
                divGST.Visible = false;
                divMaginScheme.Visible = false;
                divNext5.Visible = false;

                ScriptManager.RegisterStartupScript(this,
                                                 this.GetType(),
                                                 "FocusScript",
                                                 "setTimeout(function(){$get('" + ddlDealer.ClientID + "').focus();}, 100);",
                                                 true);
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
                divPAN.Visible = false;
                divGST.Visible = false;
                divMaginScheme.Visible = false;
                divNext5.Visible = false;

                divBank.Visible = true;
                divACNo.Visible = true;
                divIFSC.Visible = true;
                divNext6.Visible = true;

                ScriptManager.RegisterStartupScript(this,
                                                this.GetType(),
                                                "FocusScript",
                                                "setTimeout(function(){$get('" + txtBankName.ClientID + "').focus();}, 100);",
                                                true);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lnkNext6_Click(object sender, EventArgs e)
        {
            try
            {
                divBank.Visible = false;
                divACNo.Visible = false;
                divIFSC.Visible = false;
                divNext6.Visible = false;

                divIDProof.Visible = true;
                divGSTCerti.Visible = true;
                divCheque.Visible = true;
                //divShop.Visible = true;
                dviFinal.Visible = true;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lnkPrevious6_Click(object sender, EventArgs e)
        {
            try
            {
                divPAN.Visible = true;
                divGST.Visible = true;
                divMaginScheme.Visible = true;
                divNext5.Visible = true;

                divBank.Visible = false;
                divACNo.Visible = false;
                divIFSC.Visible = false;
                divNext6.Visible = false;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lnkPrevious7_Click(object sender, EventArgs e)
        {
            try
            {
                //divIDProof.Visible = true;
                //divGSTCerti.Visible = true;
                //divCheque.Visible = true;
                //divNext7.Visible = true;


                //div1.Visible = false;
                //div2.Visible = false;
                //div3.Visible = false;
                //div4.Visible = false;
                //div5.Visible = false;
                //div6.Visible = false;
                //div7.Visible = false;
                //div8.Visible = false;
                //div9.Visible = false;
                //div10.Visible = false;
                //div11.Visible = false;
                //div12.Visible = false;
                //div13.Visible = false;
                //div14.Visible = false;
                //div15.Visible = false;
                //div16.Visible = false;
                //div17.Visible = false;
                //div18.Visible = false;

                //div19.Visible = false;
                //div20.Visible = false;
                //div21.Visible = false;

                //dviFinal.Visible = false;


                divBank.Visible = true;
                divACNo.Visible = true;
                divIFSC.Visible = true;
                divNext6.Visible = true;

                divIDProof.Visible = false;
                divGSTCerti.Visible = false;
                divCheque.Visible = false;
                //divShop.Visible = false;
                dviFinal.Visible = false;





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

                    DataTable dt = new DataTable();
                    dt = objMainClass.FetchDealerData(Convert.ToInt32(ddlDealer.SelectedValue), "FETCHDATA");
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dt.Rows[0]["STATUS"]) != 0)
                        {




                            string VENDTYPE = "DOM";
                            string TITLE = "M/s";
                            string INDKEY = "S";
                            string CURRCODE = "INR";
                            string RECAC = "20020001";
                            string PAYMETHOD = "2";
                            string PAYBLKKEY = "F";
                            string TDSCOUNTRY = "IN";
                            int VENDCAT = 11240;// 11157;

                            byte[] IDPROOF = null;
                            byte[] GSTCERTI = null;
                            byte[] CHEQUE = null;
                            byte[] SHOP = null;


                            if (fuIDProof != null)
                            {
                                if (fuIDProof.HasFiles)
                                {
                                    using (BinaryReader br = new BinaryReader(fuIDProof.PostedFile.InputStream))
                                    {
                                        IDPROOF = br.ReadBytes(fuIDProof.PostedFile.ContentLength);
                                    }
                                }
                            }

                            if (fuGSTCerti != null)
                            {
                                if (fuGSTCerti.HasFiles)
                                {
                                    using (BinaryReader br = new BinaryReader(fuGSTCerti.PostedFile.InputStream))
                                    {
                                        GSTCERTI = br.ReadBytes(fuGSTCerti.PostedFile.ContentLength);
                                    }
                                }
                            }

                            if (fuCheque != null)
                            {
                                if (fuCheque.HasFiles)
                                {
                                    using (BinaryReader br = new BinaryReader(fuCheque.PostedFile.InputStream))
                                    {
                                        CHEQUE = br.ReadBytes(fuCheque.PostedFile.ContentLength);
                                    }
                                }
                            }




                            int iResult = objMainClass.InsertVendor(objMainClass.intCmpId, "", "", VENDTYPE, TITLE, txtSuggestedName.Text, "", txtContactPerson.Text, 0, "", "", "", "", INDKEY, "", "",
                                0, CURRCODE, "", "", "", "", RECAC, "", PAYMETHOD, PAYBLKKEY, "", "", "", "0", "", TDSCOUNTRY, "", "", "", "", "", "", "", "", "", "", "DI", txtPAN.Text,
                                VENDCAT, 0, 0, "", Convert.ToInt32(Session["USERID"]), txtGST.Text, 2, txtBankName.Text, txtACNo.Text, txtIFSCCode.Text,
                                chkUnderMarginScheme.Checked == true ? 1 : 0, Convert.ToString(dt.Rows[0]["ADDR1"]), Convert.ToString(dt.Rows[0]["ADDR2"]), Convert.ToString(dt.Rows[0]["ADDR3"]), Convert.ToString(dt.Rows[0]["CITY"]), Convert.ToInt32(dt.Rows[0]["STCD"]),
                                Convert.ToString(dt.Rows[0]["CNCD"]), Convert.ToString(dt.Rows[0]["POSTALCODE"]), txtContactPerson.Text, txtContactNo.Text, txtContactNo.Text, "VM", "G", IDPROOF, GSTCERTI, CHEQUE, SHOP, Convert.ToInt32(ddlDealer.SelectedValue));

                            if (iResult == 1)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Vendor registered successfully! \");$('.close').click(function(){window.location.href ='frmBikerVendorReg.aspx' });", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Vendor not registered sucessfully!');", true);
                            }


                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Dealer not registered!');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Dealer not registered!');", true);
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

        protected void txtContactPerson_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (txtContactPerson.Text != "" && txtContactPerson.Text != null && txtContactPerson.Text != string.Empty)
                    {
                        txtSuggestedName.Text = txtContactPerson.Text + " - " + ddlDealer.SelectedItem.Text;
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

        //protected void lnkPrevious8_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        divBank.Visible = true;
        //        divACNo.Visible = true;
        //        divIFSC.Visible = true;
        //        divNext6.Visible = true;

        //        divIDProof.Visible = false;
        //        divGSTCerti.Visible = false;
        //        divCheque.Visible = false;
        //        //divShop.Visible = false;
        //        dviFinal.Visible = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
        //    }
        //}

        //protected void lnkNext8_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        divIDProof.Visible = false;
        //        divGSTCerti.Visible = false;
        //        divCheque.Visible = false;
        //        //divShop.Visible = false;
        //        dviFinal.Visible = false;


        //        div1.Visible = true;
        //        div2.Visible = true;
        //        div3.Visible = true;
        //        div4.Visible = true;
        //        div5.Visible = true;
        //        div6.Visible = true;
        //        div7.Visible = true;
        //        div8.Visible = true;
        //        div9.Visible = true;
        //        div10.Visible = true;
        //        div11.Visible = true;
        //        div12.Visible = true;
        //        div13.Visible = true;
        //        div14.Visible = true;
        //        div15.Visible = true;
        //        div16.Visible = true;
        //        div17.Visible = true;
        //        div18.Visible = true;

        //        //div19.Visible = true;
        //        //div20.Visible = true;
        //        //div21.Visible = true;

        //        dviFinal.Visible = true;

        //        lblVendorName.Text = txtName.Text;
        //        lblShortName.Text = txtShortName.Text;
        //        lblAddress1.Text = txtAddress1.Text;
        //        lblAddress2.Text = txtAddress2.Text;
        //        lblAddress3.Text = txtAddress3.Text;
        //        lblPincode.Text = txtPincode.Text;
        //        lblCity.Text = txtCity.Text;
        //        lblState.Text = ddlState.SelectedItem.Text;
        //        lblCountry.Text = ddlCountry.SelectedItem.Text;
        //        lblMobileNo.Text = txtMobileNo.Text;
        //        lblContactPerson.Text = txtContactPerson.Text;
        //        lblContactNo.Text = txtContactNo.Text;
        //        lblPAN.Text = txtPAN.Text;
        //        lblGST.Text = txtGST.Text;
        //        lblUnderMargin.Text = chkUnderMarginScheme.Checked == true ? "YES" : "NO";
        //        lblBankName.Text = txtBankName.Text;
        //        lblAccountNo.Text = txtACNo.Text;
        //        lblIFSCCode.Text = txtIFSCCode.Text;

        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
        //    }
        //}

        //protected void lnkNext2_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        divAddress1.Visible = false;
        //        divAddress2.Visible = false;
        //        divAddress3.Visible = false;
        //        divNext2.Visible = false;

        //        divPincode.Visible = true;
        //        divState.Visible = true;
        //        divCity.Visible = true;
        //        divCountry.Visible = true;
        //        divNext3.Visible = true;

        //        ScriptManager.RegisterStartupScript(this,
        //                                             this.GetType(),
        //                                             "FocusScript",
        //                                             "setTimeout(function(){$get('" + txtPincode.ClientID + "').focus();}, 100);",
        //                                             true);
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
        //    }

        //}

        //protected void lnkPrevious2_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        divName.Visible = true;
        //        divShortname.Visible = true;
        //        divNext1.Visible = true;

        //        divAddress1.Visible = false;
        //        divAddress2.Visible = false;
        //        divAddress3.Visible = false;
        //        divNext2.Visible = false;

        //        ScriptManager.RegisterStartupScript(this,
        //                                             this.GetType(),
        //                                             "FocusScript",
        //                                             "setTimeout(function(){$get('" + txtName.ClientID + "').focus();}, 100);",
        //                                             true);
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
        //    }
        //}

        //protected void txtPincode_TextChanged(object sender, EventArgs e)
        //{
        //    if (txtPincode.Text.Length == 6)
        //    {
        //        try
        //        {
        //            DataTable ds = new DataTable();
        //            ds = objMainClass.SELECT_CITY_BYPINCODE(txtPincode.Text.Trim());
        //            if (ds.Rows.Count > 0)
        //            {
        //                ddlState.SelectedValue = ds.Rows[0]["STATE_ID"].ToString();
        //                txtCity.Text = ds.Rows[0]["CITY_NAME"].ToString();
        //            }
        //            else
        //            {
        //                ddlState.SelectedIndex = 0;
        //                txtCity.Text = "";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
        //        }
        //    }
        //}

        //protected void lnkPrevious3_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        divAddress1.Visible = true;
        //        divAddress2.Visible = true;
        //        divAddress3.Visible = true;
        //        divNext2.Visible = true;

        //        divPincode.Visible = false;
        //        divState.Visible = false;
        //        divCity.Visible = false;
        //        divCountry.Visible = false;
        //        divNext3.Visible = false;

        //        ScriptManager.RegisterStartupScript(this,
        //                                             this.GetType(),
        //                                             "FocusScript",
        //                                             "setTimeout(function(){$get('" + txtAddress1.ClientID + "').focus();}, 100);",
        //                                             true);
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
        //    }

        //}

        //protected void lnkNext3_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        divPincode.Visible = false;
        //        divState.Visible = false;
        //        divCity.Visible = false;
        //        divCountry.Visible = false;
        //        divNext3.Visible = false;

        //        divMobile.Visible = true;
        //        divContactPerson.Visible = true;
        //        divContactNo.Visible = true;
        //        divNext4.Visible = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
        //    }
        //}

        //protected void lnkPrevious4_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        divPincode.Visible = true;
        //        divState.Visible = true;
        //        divCity.Visible = true;
        //        divCountry.Visible = true;
        //        divNext3.Visible = true;

        //        divMobile.Visible = false;
        //        divContactPerson.Visible = false;
        //        divContactNo.Visible = false;
        //        divNext4.Visible = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
        //    }
        //}

        //protected void lnkNext4_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        divMobile.Visible = false;
        //        divContactPerson.Visible = false;
        //        divContactNo.Visible = false;
        //        divNext4.Visible = false;

        //        divPAN.Visible = true;
        //        divGST.Visible = true;
        //        divMaginScheme.Visible = true;
        //        divNext5.Visible = true;


        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
        //    }
        //}


        //protected void txtName_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (txtName.Text != string.Empty && txtName.Text != null && txtName.Text != "" && txtShortName.Text != string.Empty && txtShortName.Text != null && txtShortName.Text != "")
        //        {
        //            divName.Visible = false;
        //            divShortname.Visible = false;
        //            divNext1.Visible = false;

        //            divAddress1.Visible = true;
        //            divAddress2.Visible = true;
        //            divAddress3.Visible = true;
        //            divNext2.Visible = true;
        //            //txtShortName.Focus();

        //            ScriptManager.RegisterStartupScript(this,
        //                                              this.GetType(),
        //                                              "FocusScript",
        //                                              "setTimeout(function(){$get('" + txtShortName.ClientID + "').focus();}, 100);",
        //                                              true);

        //        }
        //        else
        //        {
        //            // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please Enter Vendor Name!');", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
        //    }
        //}

        //protected void txtShortName_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (txtName.Text != string.Empty && txtName.Text != null && txtName.Text != "")// && txtShortName.Text != string.Empty && txtShortName.Text != null && txtShortName.Text != "")
        //        {
        //            divName.Visible = false;
        //            divShortname.Visible = false;
        //            divNext1.Visible = false;

        //            divAddress1.Visible = true;
        //            divAddress2.Visible = true;
        //            divAddress3.Visible = true;
        //            divNext2.Visible = true;
        //            //txtAddress1.Focus();

        //            ScriptManager.RegisterStartupScript(this,
        //                                             this.GetType(),
        //                                             "FocusScript",
        //                                             "setTimeout(function(){$get('" + txtAddress1.ClientID + "').focus();}, 100);",
        //                                             true);
        //        }
        //        else
        //        {
        //            // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please Enter Vendor Name!');", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
        //    }
        //}


        //if (fuShopImage != null)
        //{
        //    if (fuShopImage.HasFiles)
        //    {
        //        using (BinaryReader br = new BinaryReader(fuShopImage.PostedFile.InputStream))
        //        {
        //            SHOP = br.ReadBytes(fuShopImage.PostedFile.ContentLength);
        //        }
        //    }
        //}


    }
}