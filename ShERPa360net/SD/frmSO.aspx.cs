using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ShERPa360net.SD
{
    public partial class frmSO : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        DataTable dtItem = new DataTable();
        DataTable dtTAX = new DataTable();
        DataTable dtCharges = new DataTable();


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

                        if (Request.QueryString.Count > 0)
                        {
                            if (Request.QueryString["CID"] != null)
                            {
                                Session["CallingIdDetail"] = Request.QueryString["CID"].ToString();
                                Session["CN"] = Session["CID"] = Session["CMK"] = Session["CMD"] = Session["SEG"] = null;
                            }
                            else if (Request.QueryString["EditSONo"] != null)
                            {
                                Session["EditSONo"] = Request.QueryString["EditSONo"];
                            }
                            Response.Redirect(Request.Url.AbsolutePath, false);
                        }
                        else
                        {

                            divTransaction.Visible = false;
                            txtSODATE.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();
                            txtRefDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                            txtDeliveryDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();


                            objBindDDL.FillDocType(ddlDoctype, "SO");
                            ddlDoctype.SelectedIndex = 1;
                            txtSONO.Text = objMainClass.MAXPRNO(ddlDoctype.SelectedItem.Text, "SO");
                            objBindDDL.FillSegment(ddlSegment);
                            ddlSegment.SelectedValue = "1015";

                            objBindDDL.FillDistChnl(ddlDistChnl);
                            ddlDistChnl.SelectedValue = "50";

                            objBindDDL.FillLists(ddlSalesChannel, "SF");

                            objBindDDL.FillState(ddlState);
                            objBindDDL.FillCity(ddlCity);
                            objBindDDL.FillCountry(1, ddlCountry);

                            objBindDDL.FillLists(ddlGrade, "BG");

                            objBindDDL.FillPlant(ddlPLant);
                            string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                            ddlPLant.SelectedValue = plantArray[0];
                            objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);
                            ddlLocation.SelectedIndex = 1;

                            objBindDDL.FillCostCenter(ddlCostCenter, ddlPLant.SelectedValue, ddlLocation.SelectedValue);
                            ddlCostCenter.Items.Remove(ddlCostCenter.Items.FindByValue("1000"));


                            objBindDDL.FillCondition(ddlConditionType);
                            objBindDDL.FillUOM(ddlUOM);
                            objBindDDL.FillPurChrgType(ddlCharges, objMainClass.intCmpId);

                            objBindDDL.FillPayTerm(ddlPaymentTerms);
                            objBindDDL.FillLists(ddlSalesScheme, "SS");

                            objBindDDL.FillVendByType(ddlCommAgent, "AGN", objMainClass.intCmpId, 1);
                            objBindDDL.FillPaymentMode(ddlPayMode);



                            DataTable dt = objMainClass.GetPRFCost(ddlPLant.SelectedValue, Convert.ToString(Session["USERID"]));
                            txtProfitCenter.Text = Convert.ToString(dt.Rows[0]["PRFCNT"]);

                            Session["savedet"] = "Save Item";
                            Session["saveall"] = "Save All";
                            Session["saveCharge"] = "Save Charge";
                            SetUpGrid();

                            if (Session["CallingIdDetail"] != null)
                            {
                                DataTable dtLeadData = new DataTable();
                                dtLeadData = objMainClass.GetLeadStatusData(objMainClass.intCmpId, 0, 0, "", "", "GETLEADDATA", Convert.ToInt32(Session["CallingIdDetail"]));
                                if (dtLeadData.Rows.Count > 0)
                                {
                                    txtCustName.Text = Convert.ToString(dtLeadData.Rows[0]["CUSTNAME"]);
                                    txtMobileNo.Text = Convert.ToString(dtLeadData.Rows[0]["CONTACTNO"]);
                                }
                            }
                            else if (Session["EditSONo"] != null && Convert.ToString(Session["EditSONo"]) != "" && Convert.ToString(Session["EditSONo"]) != string.Empty)
                            {
                                DataTable dtSOMST = new DataTable();
                                DataTable dtSODTL = new DataTable();
                                DataTable dtSOCOND = new DataTable();
                                DataTable dtSOCHG = new DataTable();
                                dtSOMST = objMainClass.GetSODetails(objMainClass.intCmpId, "", "", "EDITSOMST", "", Convert.ToString(Session["EditSONo"]));
                                dtSODTL = objMainClass.GetSODetails(objMainClass.intCmpId, "", "", "EDITSODTL", "", Convert.ToString(Session["EditSONo"]));
                                dtSOCOND = objMainClass.GetSODetails(objMainClass.intCmpId, "", "", "EDITSOCOND", "", Convert.ToString(Session["EditSONo"]));
                                dtSOCHG = objMainClass.GetSODetails(objMainClass.intCmpId, "", "", "EDITSOCHG", "", Convert.ToString(Session["EditSONo"]));

                                if (dtSOMST.Rows.Count > 0)
                                {


                                    ddlDoctype.SelectedValue = Convert.ToString(dtSOMST.Rows[0]["SOTYPE"]);
                                    ddlSegment.SelectedValue = Convert.ToString(dtSOMST.Rows[0]["SEGMENT"]);
                                    ddlDistChnl.SelectedValue = Convert.ToString(dtSOMST.Rows[0]["DISTCHNL"]);
                                    txtSODATE.Text = Convert.ToString(dtSOMST.Rows[0]["SODTD"]);
                                    txtSONO.Text = Convert.ToString(dtSOMST.Rows[0]["SONO"]);
                                    txtRefNo.Text = Convert.ToString(dtSOMST.Rows[0]["REFNO"]);
                                    txtRefDate.Text = Convert.ToString(dtSOMST.Rows[0]["REFDTD"]);
                                    ddlCommAgent.SelectedValue = Convert.ToString(dtSOMST.Rows[0]["COMMAGENT"]);
                                    txtCustomer.Text = Convert.ToString(dtSOMST.Rows[0]["BILLTOCODE"]);
                                    txtCustomer_TextChanged(1, e);
                                    //txtCustomerName.Text = Convert.ToString(dtSOMST.Rows[0][""]);
                                    txtShipment.Text = Convert.ToString(dtSOMST.Rows[0]["SHIPTOCODE"]);
                                    txtShipment_TextChanged(1, e);
                                    //txtShipmentName.Text = Convert.ToString(dtSOMST.Rows[0][""]);
                                    ddlPaymentTerms.SelectedValue = Convert.ToString(dtSOMST.Rows[0]["PMTTERMS"]);
                                    ddlSalesScheme.SelectedValue = Convert.ToString(dtSOMST.Rows[0]["SCHEMEID"]);
                                    txtRemarks.Text = Convert.ToString(dtSOMST.Rows[0]["REMARK"]);
                                    txtNetAmount.Text = Convert.ToString(dtSOMST.Rows[0]["NETMATVALUE"]);
                                    txtTaxAmount.Text = Convert.ToString(dtSOMST.Rows[0]["NETTAXAMT"]);
                                    txtDiscount.Text = Convert.ToString(dtSOMST.Rows[0]["DISCOUNT"]);
                                    txtTotalAmount.Text = Convert.ToString(dtSOMST.Rows[0]["NETSOAMT"]);
                                    ddlPayMode.SelectedValue = Convert.ToString(dtSOMST.Rows[0]["PAYMODE"]);




                                    if (ddlPayMode.SelectedValue == "14")
                                    {
                                        txtPartialAmount.Text = string.Empty;
                                        txtPartialAmount.Visible = true;
                                        rfvPartialAmount.Enabled = true;
                                        txtPartialAmount.Text = Convert.ToString(dtSOMST.Rows[0]["PREPAIDAMT"]);

                                        divTransaction.Visible = false;
                                        rfvTXNID.Enabled = false;
                                        rfvTXNDT.Enabled = false;
                                        rfvPayGateway.Enabled = false;
                                        txtTXNID.Text = "";
                                        txtTXNDT.Text = "";
                                        ddlPayGateway.SelectedValue = "";
                                    }
                                    else if ((ddlPayMode.SelectedValue == "5" || ddlPayMode.SelectedValue == "6" || ddlPayMode.SelectedValue == "12" || ddlPayMode.SelectedValue == "15") && ddlSalesChannel.SelectedValue == "11193")
                                    {
                                        txtPartialAmount.Text = string.Empty;
                                        txtPartialAmount.Visible = false;
                                        rfvPartialAmount.Enabled = false;
                                        divTransaction.Visible = true;
                                        rfvTXNID.Enabled = true;
                                        rfvTXNDT.Enabled = true;
                                        rfvPayGateway.Enabled = true;
                                        objBindDDL.FillPaymentGateway(ddlPayGateway);
                                        txtTXNID.Text = Convert.ToString(dtSOMST.Rows[0]["TXNNO"]);
                                        txtTXNDT.Text = Convert.ToString(dtSOMST.Rows[0]["TXNDT"]);
                                        ddlPayGateway.SelectedValue = Convert.ToString(dtSOMST.Rows[0]["PAYGATEWAY"]);

                                    }
                                    else
                                    {
                                        txtPartialAmount.Text = string.Empty;
                                        txtPartialAmount.Visible = false;
                                        rfvPartialAmount.Enabled = false;

                                        divTransaction.Visible = false;
                                        rfvTXNID.Enabled = false;
                                        rfvTXNDT.Enabled = false;
                                        rfvPayGateway.Enabled = false;
                                        txtTXNID.Text = "";
                                        txtTXNDT.Text = "";
                                        ddlPayGateway.SelectedValue = "";
                                    }








                                    if (dtSOCHG.Rows.Count > 0)
                                    {
                                        decimal others = 0;
                                        for (int i = 0; i < dtSOCHG.Rows.Count; i++)
                                        {
                                            others += Convert.ToDecimal(Convert.ToString(dtSOCHG.Rows[i]["CHRGAMOUNT"]));
                                        }
                                        txtOthers.Text = Convert.ToString(others);
                                    }
                                    else
                                    {
                                        txtOthers.Text = "0";
                                    }


                                    txtRoundOff.Text = Convert.ToString(dtSOMST.Rows[0]["NETSOAMT"]);
                                    ddlSalesChannel.SelectedValue = Convert.ToString(dtSOMST.Rows[0]["SALESFROM"]);
                                    txtCustName.Text = Convert.ToString(dtSOMST.Rows[0]["CUSTNAME"]);
                                    txtAddress1.Text = Convert.ToString(dtSOMST.Rows[0]["CUSTADD1"]);
                                    txtAddress2.Text = Convert.ToString(dtSOMST.Rows[0]["CUSTADD2"]);
                                    txtAddress3.Text = Convert.ToString(dtSOMST.Rows[0]["CUSTADD3"]);
                                    txtPincode.Text = Convert.ToString(dtSOMST.Rows[0]["PINCODE"]);
                                    ddlState.SelectedValue = Convert.ToString(dtSOMST.Rows[0]["STATEID"]);
                                    ddlCity.SelectedValue = ddlCity.Items.FindByText(Convert.ToString(dtSOMST.Rows[0]["CITY"])).Value;
                                    //ddlCountry.SelectedValue = Convert.ToString(dtSOMST.Rows[0]["POTYPE"]);
                                    txtMobileNo.Text = Convert.ToString(dtSOMST.Rows[0]["CUSTMOBILENO"]);
                                    txtEmail.Text = Convert.ToString(dtSOMST.Rows[0]["CUSTEMAILID"]);

                                    if (ddlSalesScheme.SelectedValue == "12019")
                                    {
                                        lblOldSoNo.Visible = true;
                                        txtOldSONo.Visible = true;
                                        rfvoldSoNo.Enabled = true;
                                        txtOldSONo.Text = Convert.ToString(dtSOMST.Rows[0]["OLDSONO"]);
                                    }
                                    else
                                    {
                                        lblOldSoNo.Visible = false;
                                        txtOldSONo.Visible = false;
                                        rfvoldSoNo.Enabled = false;
                                        txtOldSONo.Text = "";
                                    }


                                    grvListItem.DataSource = dtSODTL;
                                    grvListItem.DataBind();
                                    ViewState["ItemData"] = dtSODTL;

                                    grvTaxation.DataSource = dtSOCOND;
                                    grvTaxation.DataBind();
                                    ViewState["TaxData"] = dtSOCOND;

                                    grvCharges.DataSource = dtSOCHG;
                                    grvCharges.DataBind();
                                    ViewState["ChargesData"] = dtSOCHG;


                                    Session["saveall"] = "Update All";
                                    //imgSaveAll.OnClientClick = "ShowLoading()";
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

        }


        private void SetUpGrid()
        {
            try
            {

                DataColumn dtColumn;

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ID";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMCODE";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMDESC";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMID";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMGROUP";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMGROUPID";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMUOM";
                dtColumn.ReadOnly = false;
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "UOMID";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "GRADE";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "GRADEID";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMQTY";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMRATE";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMBRATE";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMAMOUNT";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "DISCOUNTAMT";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "DELIVERYDATE";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "GLCODE";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "COSTCENTER";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMPLANTCD";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMPLANTID";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMLOCCD";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "LOCCDID";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "PROFITCENTER";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "JOBID";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMREMARKS";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "IMEINO";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMTEXT";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "CUSTPARTNO";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "CUSTPARTDESC";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "SKU";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "LOCKAMT";
                dtItem.Columns.Add(dtColumn);

                ViewState["ItemData"] = dtItem;

                grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                grvListItem.DataBind();


                //// Tax Details


                DataColumn dtTaxColumn;

                dtTaxColumn = new DataColumn();
                dtTaxColumn.ColumnName = "OPERATOR";
                dtTAX.Columns.Add(dtTaxColumn);

                dtTaxColumn = new DataColumn();
                dtTaxColumn.ColumnName = "TAXSRNO";
                dtTAX.Columns.Add(dtTaxColumn);

                dtTaxColumn = new DataColumn();
                dtTaxColumn.ColumnName = "SOSRNO";
                dtTAX.Columns.Add(dtTaxColumn);

                dtTaxColumn = new DataColumn();
                dtTaxColumn.ColumnName = "CONDTYPE";
                dtTAX.Columns.Add(dtTaxColumn);

                dtTaxColumn = new DataColumn();
                dtTaxColumn.ColumnName = "TAXRATE";
                dtTAX.Columns.Add(dtTaxColumn);

                dtTaxColumn = new DataColumn();
                dtTaxColumn.ColumnName = "TAXBASEAMOUNT";
                dtTAX.Columns.Add(dtTaxColumn);

                dtTaxColumn = new DataColumn();
                dtTaxColumn.ColumnName = "TAXAMOUNT";
                dtTAX.Columns.Add(dtTaxColumn);

                dtTaxColumn = new DataColumn();
                dtTaxColumn.ColumnName = "PID";
                dtTAX.Columns.Add(dtTaxColumn);

                dtTaxColumn = new DataColumn();
                dtTaxColumn.ColumnName = "CONDID";
                dtTAX.Columns.Add(dtTaxColumn);

                ViewState["TaxData"] = dtTAX;

                grvTaxation.DataSource = (DataTable)ViewState["TaxData"];
                grvTaxation.DataBind();


                /// Other Charges 

                DataColumn dtChargesColumn;

                dtChargesColumn = new DataColumn();
                dtChargesColumn.ColumnName = "CHRGSRNO";
                dtCharges.Columns.Add(dtChargesColumn);

                dtChargesColumn = new DataColumn();
                dtChargesColumn.ColumnName = "CHRGTYPE";
                dtCharges.Columns.Add(dtChargesColumn);

                dtChargesColumn = new DataColumn();
                dtChargesColumn.ColumnName = "CHRGAMOUNT";
                dtCharges.Columns.Add(dtChargesColumn);

                ViewState["ChargesData"] = dtCharges;

                grvCharges.DataSource = (DataTable)ViewState["ChargesData"];
                grvCharges.DataBind();


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }


        protected void imgSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (Convert.ToString(Session["saveall"]) == "Save All")
                    {
                        if (grvListItem.Rows.Count > 0)
                        {
                            string PLantRights = string.Empty;
                            string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                            for (int i = 0; i < plantArray.Count(); i++)
                            {
                                if (plantArray[i].Trim() == ((Label)grvListItem.Rows[0].FindControl("lblGVPlantID")).Text)
                                {
                                    PLantRights = ((Label)grvListItem.Rows[0].FindControl("lblGVPlantID")).Text;
                                }
                            }

                            if (PLantRights.Length > 0)
                            {
                                var refdateDiif = (Convert.ToDateTime(txtRefDate.Text) - DateTime.Now).Days;

                                if (refdateDiif <= 2 && refdateDiif >= -2)
                                {
                                    var datediff = (Convert.ToDateTime(txtRefDate.Text) - Convert.ToDateTime(txtSODATE.Text)).Days;

                                    if (datediff <= 0)
                                    {
                                        string prepaidamt, remainamt;
                                        if (ddlPayMode.SelectedValue == "14")
                                        {
                                            prepaidamt = txtPartialAmount.Text;
                                            remainamt = Convert.ToString(Convert.ToDecimal(txtTotalAmount.Text) - Convert.ToDecimal(txtPartialAmount.Text == "" ? "0" : txtPartialAmount.Text));
                                        }
                                        else if (ddlPayMode.SelectedValue == "6" || ddlPayMode.SelectedValue == "10" || ddlPayMode.SelectedValue == "11" || ddlPayMode.SelectedValue == "12")
                                        {
                                            prepaidamt = txtTotalAmount.Text;
                                            remainamt = "0";
                                        }
                                        else if (ddlPayMode.SelectedValue == "8")
                                        {
                                            prepaidamt = "0";
                                            remainamt = "0";
                                        }
                                        else
                                        {
                                            prepaidamt = "0";
                                            remainamt = txtTotalAmount.Text;
                                        }

                                        string txndt = "";
                                        int paygate = 0;
                                        if ((ddlPayMode.SelectedValue == "5" || ddlPayMode.SelectedValue == "6" || ddlPayMode.SelectedValue == "12" || ddlPayMode.SelectedValue == "15") && (ddlSalesChannel.SelectedValue == "11193" || ddlSalesChannel.SelectedValue == "11411"))
                                        {
                                            txndt = txtTXNDT.Text;
                                            paygate = Convert.ToInt32(ddlPayGateway.SelectedValue);
                                        }


                                        string SONO = objMainClass.SaveSO(objMainClass.intCmpId, ddlDoctype.SelectedValue, txtSONO.Text, txtSODATE.Text, ddlSegment.SelectedValue, ddlDistChnl.SelectedValue,
                                        objMainClass.strConvertZeroPadding(txtCustomer.Text), objMainClass.strConvertZeroPadding(txtShipment.Text), ddlPaymentTerms.SelectedValue, txtRemarks.Text,
                                        (int)STATUS.Saved, txtNetAmount.Text, txtTaxAmount.Text, txtDiscount.Text, txtTotalAmount.Text, Convert.ToInt32(Session["USERID"]), txtRefNo.Text,
                                        txtRefDate.Text, Convert.ToInt32(ddlSalesChannel.SelectedValue), txtCustName.Text, txtAddress1.Text, txtAddress2.Text, txtAddress3.Text, ddlCity.SelectedItem.Text,
                                        Convert.ToInt32(ddlState.SelectedValue), txtPincode.Text, txtMobileNo.Text, txtEmail.Text, txtJobId.Text, ddlCommAgent.SelectedValue,
                                        Convert.ToInt32(ddlSalesScheme.SelectedValue), grvListItem, grvTaxation, grvCharges, Convert.ToInt32(ddlPayMode.SelectedValue), prepaidamt, remainamt,
                                        txtGSTNo.Text, txtTXNID.Text, txndt, paygate, txtOldSONo.Text);

                                        if (SONO != "" && SONO != string.Empty && SONO != null)
                                        {
                                            if (Convert.ToInt32(Session["CallingIdDetail"]) > 0)
                                            {
                                                int iLeadResult = objMainClass.UpdateLeadInqNo(Convert.ToInt32(Session["CallingIdDetail"]), SONO, (int)LeadStatus.Converted, Convert.ToInt32(Session["USERID"]), "INSERTSO");
                                            }
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('SO created successfully. SO No. is \"" + SONO + "\".');$('.close').click(function(){window.location.href ='frmViewSO.aspx' });", true);
                                            Session["EditSONo"] = string.Empty;
                                            Session["EditSONo"] = null;
                                            Session["CallingIdDetail"] = string.Empty;
                                            Session["CallingIdDetail"] = null;

                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Record not inserted sucessfully!');", true);
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You cannot select SO date less than Ref. Date.');", true);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You cannot select Ref. date less or greater than 3 days.');", true);
                                }




                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have plant rights.');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please Enter Item Details!');", true);
                        }
                    }
                    else if (Convert.ToString(Session["saveall"]) == "Update All")
                    {
                        if (grvListItem.Rows.Count > 0)
                        {
                            string PLantRights = string.Empty;
                            string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                            for (int i = 0; i < plantArray.Count(); i++)
                            {
                                if (plantArray[i].Trim() == ((Label)grvListItem.Rows[0].FindControl("lblGVPlantCD")).Text)
                                {
                                    PLantRights = ((Label)grvListItem.Rows[0].FindControl("lblGVPlantCD")).Text;
                                }
                            }

                            if (PLantRights.Length > 0)
                            {

                                string prepaidamt, remainamt;
                                if (ddlPayMode.SelectedValue == "14")
                                {
                                    prepaidamt = txtPartialAmount.Text;
                                    remainamt = Convert.ToString(Convert.ToDecimal(txtTotalAmount.Text) - Convert.ToDecimal(txtPartialAmount.Text == "" ? "0" : txtPartialAmount.Text));
                                }
                                else if (ddlPayMode.SelectedValue == "6" || ddlPayMode.SelectedValue == "10" || ddlPayMode.SelectedValue == "11" || ddlPayMode.SelectedValue == "12")
                                {
                                    prepaidamt = txtTotalAmount.Text;
                                    remainamt = "0";
                                }
                                else if (ddlPayMode.SelectedValue == "8")
                                {
                                    prepaidamt = "0";
                                    remainamt = "0";
                                }
                                else
                                {
                                    prepaidamt = "0";
                                    remainamt = txtTotalAmount.Text;
                                }

                                string txndt = "";
                                int paygate = 0;
                                if ((ddlPayMode.SelectedValue == "5" || ddlPayMode.SelectedValue == "6" || ddlPayMode.SelectedValue == "12" || ddlPayMode.SelectedValue == "15") && ddlSalesChannel.SelectedValue == "11193")
                                {
                                    txndt = txtTXNDT.Text;
                                    paygate = Convert.ToInt32(ddlPayGateway.SelectedValue);
                                }

                                string SONO = objMainClass.UpdateSO(objMainClass.intCmpId, ddlDoctype.SelectedValue, txtSONO.Text, txtSODATE.Text, ddlSegment.SelectedValue, ddlDistChnl.SelectedValue,
                                    objMainClass.strConvertZeroPadding(txtCustomer.Text), objMainClass.strConvertZeroPadding(txtShipment.Text), ddlPaymentTerms.SelectedValue, txtRemarks.Text,
                                    (int)STATUS.Saved, txtNetAmount.Text, txtTaxAmount.Text, txtDiscount.Text, txtTotalAmount.Text, Convert.ToInt32(Session["USERID"]), txtRefNo.Text,
                                    txtRefDate.Text, Convert.ToInt32(ddlSalesChannel.SelectedValue), txtCustName.Text, txtAddress1.Text, txtAddress2.Text, txtAddress3.Text, ddlCity.SelectedItem.Text,
                                    Convert.ToInt32(ddlState.SelectedValue), txtPincode.Text, txtMobileNo.Text, txtEmail.Text, txtJobId.Text, ddlCommAgent.SelectedValue,
                                    Convert.ToInt32(ddlSalesScheme.SelectedValue), grvListItem, grvTaxation, grvCharges, Convert.ToInt32(ddlPayMode.SelectedValue), prepaidamt, remainamt,
                                    txtGSTNo.Text, txtTXNID.Text, txndt, paygate, txtOldSONo.Text);

                                if (SONO != "" && SONO != string.Empty && SONO != null)
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('SO updated successfully.');$('.close').click(function(){window.location.href ='frmViewSO.aspx' });", true);
                                    Session["EditSONo"] = string.Empty;
                                    Session["EditSONo"] = null;
                                    Session["CallingIdDetail"] = string.Empty;
                                    Session["CallingIdDetail"] = null;
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Record not inserted sucessfully!');", true);
                                }

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have plant rights.');$('#txtJobId').focus();", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please Enter Item Details!');", true);
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

        protected void txtCustomer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetCustData(objMainClass.intCmpId, objMainClass.strConvertZeroPadding(txtCustomer.Text), "", "", "", "CUSTDETAIL");
                    if (dt.Rows.Count > 0)
                    {
                        txtCustomerName.Text = Convert.ToString(dt.Rows[0]["NAME1"]);
                    }
                    else
                    {
                        txtCustomer.Focus();
                        txtCustomer.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid Customer Code.');", true);
                        txtCustomer.Text = string.Empty;
                        txtCustomer.Focus();
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

        protected void txtShipment_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetCustData(objMainClass.intCmpId, objMainClass.strConvertZeroPadding(txtShipment.Text), "", "", "", "CUSTDETAIL");
                    if (dt.Rows.Count > 0)
                    {
                        txtShipmentName.Text = Convert.ToString(dt.Rows[0]["NAME1"]);
                    }
                    else
                    {
                        txtShipment.Focus();
                        txtShipment.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid Shipment Code.');", true);
                        txtShipment.Text = string.Empty;
                        txtShipment.Focus();
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
                                    ddlCity.DataSource = string.Empty;
                                    ddlCity.DataBind();
                                    objBindDDL.FillCity(ddlCity, ddlState.SelectedValue);
                                    ddlCity.SelectedItem.Text = ds.Rows[0]["CITY_NAME"].ToString();
                                }
                                else
                                {
                                    ddlState.SelectedIndex = 0;
                                    ddlCity.SelectedIndex = 0;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void txtIMEI_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (txtIMEI.Text != string.Empty && txtIMEI.Text != null && txtIMEI.Text != "")
                    {
                        if (txtIMEI.Text.Length < 15 && ddlSegment.SelectedValue != "1043")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid IMEI No.!');", true);
                            txtIMEI.Text = string.Empty;
                        }
                        else
                        {
                            DataTable dtJOBID = new DataTable();
                            dtJOBID = objMainClass.GetJSDetails(txtJobId.Text);
                            if (txtIMEI.Text == Convert.ToString(dtJOBID.Rows[0]["IMEINO"]))
                            {

                                DataTable dtSO = new DataTable();
                                dtSO = objMainClass.GetSODetails(objMainClass.intCmpId, ddlDoctype.SelectedValue, txtIMEI.Text, "SODETAILS", "", "");
                                if (dtSO.Rows.Count > 0)
                                {
                                    DataTable dtSOR = new DataTable();
                                    dtSOR = objMainClass.GetSODetails(objMainClass.intCmpId, "SOR", txtIMEI.Text, "SORDETAILS", "", "");
                                    if (dtSOR.Rows.Count > 0)
                                    {
                                        DataTable dt = new DataTable();
                                        dt = objMainClass.GETPOMASTER(objMainClass.intCmpId, txtIMEI.Text, "IMEIDETAIL");


                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('SO is already created for mentioned IMEI No.! SO No.:" + Convert.ToString(dtSO.Rows[0]["SONO"]) + " ' );", true);
                                        txtIMEI.Text = string.Empty;
                                    }
                                }
                                else
                                {
                                    DataTable dt = new DataTable();
                                    dt = objMainClass.GETPOMASTER(objMainClass.intCmpId, txtIMEI.Text, "IMEIDETAIL");


                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('IMEI No. is not matched with Jobs detail!');", true);
                                txtIMEI.Text = string.Empty;
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



        //        if (dt.Rows.Count > 0)
        //                                        {
        //                                            txtItemCode.Text = Convert.ToString(dt.Rows[0]["ITEMCODE"]);
        //                                            txtItemCode_TextChanged(1, e);

        //                                            if (lblJSIMEI.Text != "" && lblJSIMEI.Text != null && lblJSIMEI.Text != string.Empty)
        //                                            {
        //                                                if (txtIMEI.Text == lblJSIMEI.Text)
        //                                                {

        //                                                }
        //                                                else
        //                                                {
        //                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('IMEI No. is not matched with Jobs detail!');", true);
        //                                                }
        //}
        //                                        }
        //                                        else
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Purchase Entry Found!');", true);
        //}
        protected void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetItemDetails(txtItemCode.Text, ddlPLant.SelectedValue, ddlLocation.SelectedValue);
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToString(dt.Rows[0]["status"]) == "1")
                        {
                            txtItemDesc.Text = Convert.ToString(dt.Rows[0]["itemdesc"]);
                            txtGLCode.Text = Convert.ToString(dt.Rows[0]["glcode"]);
                            txtItemGroup.Text = Convert.ToString(dt.Rows[0]["itemgrp"]);
                            txtItemId.Text = Convert.ToString(dt.Rows[0]["itemid"]);
                            txtSku.Text = Convert.ToString(dt.Rows[0]["sku"]);
                            ddlUOM.SelectedValue = Convert.ToString(dt.Rows[0]["sku"]);
                            txtItemGroupId.Text = Convert.ToString(dt.Rows[0]["itemgrpid"]);
                            //txtItemText.Text = Convert.ToString(dt.Rows[0]["itemgrpid"]);
                            //txtProfitCenter.Text = Convert.ToString(dt.Rows[0]["itemgrpid"]);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + txtItemCode.Text + " - Item code is deactivated, you can't use the same!\");", true);
                            txtItemId.Focus();
                        }
                    }
                    else
                    {
                        txtItemCode.Focus();
                        txtItemCode.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + txtItemCode.Text + " - Item code not found!\");", true);

                        txtItemCode.Focus();
                        txtItemCode.Text = string.Empty;
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

        protected void lnkOpenPoup_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    objBindDDL.FillItemCat(ddlpopCategory);
                    objBindDDL.FillItemGrp(ddlpopGroup);
                    objBindDDL.FillItemSubGrp(ddlpopSubGroup);
                    objBindDDL.FillBrand(ddlpopMake, 0);

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-item').modal();", true);
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

        protected void ddlUOM_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (txtSku.Text != "" && txtSku.Text != null && txtSku.Text != string.Empty)
                    {
                        if (ddlUOM.SelectedItem.Value.ToString() != txtSku.Text)
                        {
                            ddlUOM.SelectedValue = txtSku.Text;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"UOM is not matched with Base Unit of Item!\");", true);
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

        protected void txtItemBRate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    txtAmount.Text = Convert.ToString(Convert.ToDecimal(txtItemBRate.Text == "" ? "0" : txtItemBRate.Text) * Convert.ToDecimal(txtItemQty.Text == "" ? "0" : txtItemQty.Text));
                    txtRate.Text = txtItemBRate.Text;
                    if (ddlConditionType.SelectedIndex == 0)
                    {
                        txtTaxTAmount.Text = string.Empty;
                        txtGLCdTax.Text = string.Empty;
                        ddlOperator.SelectedIndex = 0;
                    }
                    else
                    {
                        DataTable dt = new DataTable();
                        dt = objMainClass.GetTaxCalData(ddlConditionType.SelectedValue);
                        txtRate.Text = Convert.ToString(Convert.ToDecimal(txtItemBRate.Text) + (Convert.ToDecimal(Convert.ToDecimal(txtItemBRate.Text) * Convert.ToDecimal(dt.Rows[0]["rate"])) / 100));
                        txtTaxTAmount.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(txtAmount.Text) * Convert.ToDecimal(dt.Rows[0]["rate"])) / 100);
                        txtTaxTAmount.Text = Math.Round(Convert.ToDecimal(txtTaxTAmount.Text), 2).ToString();
                        hfCONDID.Value = Convert.ToString(dt.Rows[0]["id"]);
                        hfPID.Value = Convert.ToString(dt.Rows[0]["pid"]);
                        hfRate.Value = Convert.ToString(dt.Rows[0]["rate"]);
                    }

                    callAMT();
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

        protected void txtDiscountRate_TextChanged(object sender, EventArgs e)
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

        protected void ddlPLant_SelectedIndexChanged(object sender, EventArgs e)
        {
            objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);

            try
            {
                if (Session["USERID"] != null)
                {
                    string PLantRights = string.Empty;
                    string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                    for (int i = 0; i < plantArray.Count(); i++)
                    {
                        if (plantArray[i].Trim() == ddlPLant.SelectedValue)
                        {
                            PLantRights = ddlPLant.SelectedValue;
                        }
                    }

                    if (PLantRights.Length > 0)
                    {
                        DataTable dt = objMainClass.GetPRFCost(ddlPLant.SelectedValue, Convert.ToString(Session["USERID"]));
                        if (dt.Rows.Count > 0)
                        {
                            txtProfitCenter.Text = Convert.ToString(dt.Rows[0]["PRFCNT"]);
                        }
                        else
                        {
                            txtProfitCenter.Text = "1000";
                        }

                        DataTable dtPlantSales = new DataTable();
                        dtPlantSales = objMainClass.CheckPlantLocationSales(objMainClass.intCmpId, ddlPLant.SelectedValue, "", 1, "CHECKPLANT");
                        if (dtPlantSales.Rows.Count > 0)
                        {

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You can not create SO in this Plant.!');", true);
                            objBindDDL.FillPlant(ddlPLant);
                            ddlPLant.SelectedValue = plantArray[0];
                            objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);
                            ddlLocation.SelectedIndex = 1;
                        }

                    }
                    else
                    {
                        ddlPLant.SelectedValue = plantArray[0];
                        ddlPLant.Focus();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have plant rights.');", true);
                        ddlPLant.SelectedValue = plantArray[0];
                        ddlPLant.Focus();
                        objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);
                        ddlLocation.SelectedIndex = 1;
                    }

                    objBindDDL.FillCostCenter(ddlCostCenter, ddlPLant.SelectedValue, ddlLocation.SelectedValue);
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

        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    DataTable dtLocationSales = new DataTable();
                    dtLocationSales = objMainClass.CheckPlantLocationSales(objMainClass.intCmpId, "", ddlLocation.SelectedValue, 1, "CHECKPLANT");
                    if (dtLocationSales.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You can not create SO in this Location.!');", true);
                        objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);
                        ddlLocation.SelectedIndex = 1;
                    }


                    //if (ddlPLant.SelectedValue == "1001")
                    //{
                    //    if (ddlLocation.SelectedValue == "WMR1")
                    //    {

                    //    }
                    //    else
                    //    {
                    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You can not create SO in any other Location. Please use WMR1 to create SO.!');", true);
                    //        ddlLocation.SelectedValue = "WMR1";
                    //        ddlLocation_SelectedIndexChanged(1, e);
                    //    }

                    //}
                    //else if (ddlPLant.SelectedValue == "1002")
                    //{
                    //    if (ddlLocation.SelectedValue == "WS03")
                    //    {

                    //    }
                    //    else
                    //    {
                    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You can not create SO in any other Location. Please use WS03 to create SO.!');", true);
                    //        ddlLocation.SelectedValue = "WS03";
                    //        ddlLocation_SelectedIndexChanged(1, e);
                    //    }

                    //}
                    //else if (ddlPLant.SelectedValue == "1016")
                    //{
                    //    if (ddlLocation.SelectedValue == "WS03")
                    //    {

                    //    }
                    //    else
                    //    {
                    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You can not create SO in any other Location. Please use WS03 to create SO.!');", true);
                    //        ddlLocation.SelectedValue = "WS03";
                    //        ddlLocation_SelectedIndexChanged(1, e);
                    //    }

                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You can not create SO in any other Plant. Please use 1001,1002,1016 to create SO.!');", true);
                    //    objBindDDL.FillPlant(ddlPLant);
                    //    string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                    //    ddlPLant.SelectedValue = plantArray[0];
                    //    objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);
                    //    ddlLocation.SelectedIndex = 1;
                    //}

                    //objBindDDL.FillCostCenter(ddlCostCenter, ddlPLant.SelectedValue, ddlLocation.SelectedValue);

                    //if (ddlLocation.SelectedValue == "WS14")
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You can not create PO in WS14 Location. Please use WMR1 to create PO.!');", true);
                    //    ddlLocation.SelectedValue = "WMR1";
                    //    ddlLocation_SelectedIndexChanged(1, e);
                    //}
                    //else
                    //{

                    //    ScriptManager src1 = ScriptManager.GetCurrent(Page);
                    //    src1.SetFocus(ddlLocation);
                    //}
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

            //try
            //{
            //    if (Session["USERID"] != null)
            //    {
            //        objBindDDL.FillCostCenter(ddlCostCenter, ddlPLant.SelectedValue, ddlLocation.SelectedValue);
            //        ScriptManager src1 = ScriptManager.GetCurrent(Page);
            //        src1.SetFocus(ddlLocation);
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
            //    }

            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            //}
        }

        protected void ddlConditionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (ddlConditionType.SelectedIndex == 0)
                    {
                        txtTaxTAmount.Text = string.Empty;
                        txtGLCdTax.Text = string.Empty;
                        ddlOperator.SelectedIndex = 0;
                    }
                    else
                    {
                        if (txtItemBRate.Text != string.Empty && txtItemBRate.Text != "" && txtItemBRate.Text != null)
                        {
                            DataTable dt = new DataTable();
                            dt = objMainClass.GetTaxCalData(ddlConditionType.SelectedValue);
                            txtRate.Text = Convert.ToString(Convert.ToDecimal(txtItemBRate.Text) + (Convert.ToDecimal(Convert.ToDecimal(txtItemBRate.Text) * Convert.ToDecimal(dt.Rows[0]["rate"])) / 100));
                            txtTaxTAmount.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(txtAmount.Text) * Convert.ToDecimal(dt.Rows[0]["rate"])) / 100);
                            txtTaxTAmount.Text = Math.Round(Convert.ToDecimal(txtTaxTAmount.Text), 2).ToString();
                            hfCONDID.Value = Convert.ToString(dt.Rows[0]["id"]);
                            hfPID.Value = Convert.ToString(dt.Rows[0]["pid"]);
                            hfRate.Value = Convert.ToString(dt.Rows[0]["rate"]);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please Enter Item Rate.');", true);
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

        protected void ddlCharges_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void lnkSaveCharges_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (Convert.ToString(Session["saveCharge"]) == "Save Charge")
                    {
                        DataTable dt = (DataTable)ViewState["ChargesData"];
                        if (grvCharges.Rows.Count > 0)
                        {
                            DataRow lastRow = dt.Rows[dt.Rows.Count - 1];
                            int id = Convert.ToInt32(lastRow["CHRGSRNO"]) + 1;

                            dt.Rows.Add(id, ddlCharges.SelectedValue, txtChgAmt.Text);

                            ViewState["ChargesData"] = dt;
                        }
                        else
                        {
                            dt.Rows.Add("1", ddlCharges.SelectedValue, txtChgAmt.Text);
                            ViewState["ChargesData"] = dt;
                        }

                        grvCharges.DataSource = (DataTable)ViewState["ChargesData"];
                        grvCharges.DataBind();
                    }
                    else if (Convert.ToString(Session["saveCharge"]) == "Update Charge")
                    {
                        //Session["saveCharge"] = "Update Charge";

                        DataTable dt = (DataTable)ViewState["ChargesData"];
                        DataRow dr = dt.Select("CHRGSRNO = '" + txtSrNoChg.Text + "'")[0];

                        dr[1] = ddlCharges.SelectedValue;
                        dr[2] = txtChgAmt.Text;

                        grvCharges.DataSource = (DataTable)ViewState["ChargesData"];
                        grvCharges.DataBind();
                        Session["saveCharge"] = "Save Charge";

                    }

                    callAMT();

                    txtSrNoChg.Text = string.Empty;
                    txtMaxSrNoChg.Text = string.Empty;
                    txtChgAmt.Text = string.Empty;
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

        private void callAMT()
        {
            decimal materialvalue = 0;
            decimal taxvalue = 0;
            decimal discountvalue = 0;
            decimal chargesvalue = 0;
            decimal totalvalue = 0;

            for (int i = 0; i < grvListItem.Rows.Count; i++)
            {
                GridViewRow row = grvListItem.Rows[i];
                materialvalue = materialvalue + Convert.ToDecimal(((Label)row.FindControl("lblGVAmount")).Text);
                discountvalue = discountvalue + Convert.ToDecimal(((Label)row.FindControl("lblGVDiscount")).Text);
            }

            for (int j = 0; j < grvTaxation.Rows.Count; j++)
            {
                GridViewRow row = grvTaxation.Rows[j];
                string gvoperator = Convert.ToString(((Label)row.FindControl("lblTaxOperator")).Text);
                if (gvoperator == "+")
                {
                    taxvalue = taxvalue + Convert.ToDecimal(((Label)row.FindControl("lblTaxAmount")).Text);
                }
                if (gvoperator == "-")
                {
                    taxvalue = taxvalue - Convert.ToDecimal(((Label)row.FindControl("lblTaxAmount")).Text);
                }

            }

            for (int k = 0; k < grvCharges.Rows.Count; k++)
            {
                GridViewRow row = grvCharges.Rows[k];
                chargesvalue = chargesvalue + Convert.ToDecimal(((Label)row.FindControl("lblChrgAmount")).Text);
            }

            txtNetAmount.Text = Convert.ToString(materialvalue);
            txtTaxAmount.Text = Convert.ToString(taxvalue);
            txtDiscount.Text = Convert.ToString(discountvalue);
            txtOthers.Text = Convert.ToString(chargesvalue);
            totalvalue = materialvalue + taxvalue - discountvalue + chargesvalue;
            txtTotalAmount.Text = Convert.ToString(totalvalue);
            txtRoundOff.Text = Convert.ToString(Math.Round(totalvalue));
        }

        protected void grvListItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (e.CommandName == "eDelete")
                    {
                        int index = Convert.ToInt32(e.CommandArgument);
                        DataTable dt = (DataTable)ViewState["ItemData"];
                        GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                        dt.Rows[row.RowIndex].Delete();
                        ViewState["ItemData"] = dt;
                        grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                        grvListItem.DataBind();

                        DataTable dtTax = (DataTable)ViewState["TaxData"];
                        dtTax.Select("SOSRNO='" + index + "'").ToList().ForEach(x => x.Delete());
                        dtTax.AcceptChanges();
                        ViewState["TaxData"] = dtTax;
                        grvTaxation.DataSource = (DataTable)ViewState["TaxData"];
                        grvTaxation.DataBind();

                        callAMT();

                    }
                    if (e.CommandName == "eEdit")
                    {
                        int index = Convert.ToInt32(e.CommandArgument);
                        txtSRNO.Text = Convert.ToString(index);
                        GridViewRow gRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;


                        Label lblGVID = (Label)gRow.FindControl("lblGVID");
                        Label lblGVItemCode = (Label)gRow.FindControl("lblGVItemCode");
                        Label lblGVItemDesc = (Label)gRow.FindControl("lblGVItemDesc");
                        Label lblGVItemId = (Label)gRow.FindControl("lblGVItemId");
                        Label lblGVItemGroup = (Label)gRow.FindControl("lblGVItemGroup");
                        Label lblGVGroupId = (Label)gRow.FindControl("lblGVGroupId");
                        Label lblGVUOM = (Label)gRow.FindControl("lblGVUOM");
                        Label lblGVUOMID = (Label)gRow.FindControl("lblGVUOMID");
                        Label lblGVQty = (Label)gRow.FindControl("lblGVQty");
                        Label lblGVRate = (Label)gRow.FindControl("lblGVRate");
                        Label lblGVBaseRate = (Label)gRow.FindControl("lblGVBaseRate");
                        Label lblGVAmount = (Label)gRow.FindControl("lblGVAmount");
                        Label lblGVDeliDate = (Label)gRow.FindControl("lblGVDeliDate");
                        Label lblGVGLCode = (Label)gRow.FindControl("lblGVGLCode");
                        Label lblGVCostCenter = (Label)gRow.FindControl("lblGVCostCenter");
                        Label lblGVPlantCD = (Label)gRow.FindControl("lblGVPlantCD");
                        Label lblGVPlantID = (Label)gRow.FindControl("lblGVPlantID");
                        Label lblGVLocationCD = (Label)gRow.FindControl("lblGVLocationCD");
                        Label lblGVLocationCDID = (Label)gRow.FindControl("lblGVLocationCDID");
                        Label lblGVProfitCenter = (Label)gRow.FindControl("lblGVProfitCenter");
                        Label lblGVTrackNo = (Label)gRow.FindControl("lblGVTrackNo");
                        Label lblGVRemarks = (Label)gRow.FindControl("lblGVRemarks");
                        Label lblGVIMEI = (Label)gRow.FindControl("lblGVIMEI");
                        Label lblSKU = (Label)gRow.FindControl("lblSKU");
                        Label lblGVDiscount = (Label)gRow.FindControl("lblGVDiscount");
                        Label lblGVGrade = (Label)gRow.FindControl("lblGVGrade");
                        Label lblGVGradeID = (Label)gRow.FindControl("lblGVGradeID");

                        Label lblGVITEMTEXT = (Label)gRow.FindControl("lblGVITEMTEXT");
                        Label lblGVCUSTPARTNO = (Label)gRow.FindControl("lblGVCUSTPARTNO");
                        Label lblGVCUSTPARTDESC = (Label)gRow.FindControl("lblGVCUSTPARTDESC");


                        txtSRNO.Text = lblGVID.Text;
                        txtItemCode.Text = lblGVItemCode.Text;
                        txtItemDesc.Text = lblGVItemDesc.Text;
                        txtItemId.Text = lblGVItemId.Text;
                        txtSku.Text = lblSKU.Text;
                        txtItemGroup.Text = lblGVItemGroup.Text;
                        txtItemGroupId.Text = lblGVGroupId.Text;
                        txtGLCode.Text = lblGVGLCode.Text;
                        txtProfitCenter.Text = lblGVProfitCenter.Text;
                        txtJobId.Text = lblGVTrackNo.Text;
                        txtItemQty.Text = lblGVQty.Text;
                        ddlUOM.SelectedValue = lblGVUOMID.Text;
                        ddlGrade.SelectedValue = lblGVGradeID.Text;
                        txtRate.Text = lblGVRate.Text;
                        txtItemBRate.Text = lblGVBaseRate.Text;
                        txtAmount.Text = lblGVAmount.Text;
                        txtDiscount.Text = lblGVDiscount.Text;
                        txtDeliveryDate.Text = lblGVDeliDate.Text;
                        txtIMEI.Text = lblGVIMEI.Text;
                        txtItemRemarks.Text = lblGVRemarks.Text;
                        ddlPLant.SelectedValue = lblGVPlantID.Text;
                        objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);
                        ddlLocation.SelectedValue = lblGVLocationCDID.Text;
                        objBindDDL.FillCostCenter(ddlCostCenter, ddlPLant.SelectedValue, ddlLocation.SelectedValue);
                        ddlCostCenter.SelectedValue = lblGVCostCenter.Text;

                        txtItemText.Text = lblGVITEMTEXT.Text;
                        txtCustPartNo.Text = lblGVCUSTPARTNO.Text;
                        txtCustPartDesc.Text = lblGVCUSTPARTDESC.Text;


                        DataTable dtTax = (DataTable)ViewState["TaxData"];
                        if (dtTax.Rows.Count > 0)
                        {

                            bool IsRecordavailable = IsRecordAvailableAtDataTable(index);
                            if (IsRecordavailable)
                            {

                                DataRow dr = dtTax.Select("POSRNO='" + index + "'")[0];
                                if (dr != null)
                                {
                                    ddlOperator.SelectedValue = Convert.ToString(dr[0]);
                                    ddlConditionType.SelectedValue = Convert.ToString(dr[3]);
                                    txtTaxTAmount.Text = Convert.ToString(dr[6]);
                                    hfRate.Value = Convert.ToString(dr[4]);
                                    hfPID.Value = Convert.ToString(dr[7]);
                                    hfCONDID.Value = Convert.ToString(dr[8]);

                                }
                            }
                        }
                        Session["savedet"] = "Update Item";
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


        public bool IsRecordAvailableAtDataTable(int index)
        {
            bool IsrecordAvailable = false;
            try
            {
                DataTable dtTax = (DataTable)ViewState["TaxData"];
                foreach (DataRow dr in dtTax.Rows)
                {
                    if (dr["SOSRNO"].ToString() == index.ToString())
                    {
                        IsrecordAvailable = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return IsrecordAvailable;
        }

        protected void grvTaxation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (e.CommandName == "eDelete")
                    {
                        int index = Convert.ToInt32(e.CommandArgument);
                        DataTable dt = (DataTable)ViewState["TaxData"];
                        GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                        dt.Rows[row.RowIndex].Delete();
                        ViewState["TaxData"] = dt;
                        grvTaxation.DataSource = (DataTable)ViewState["TaxData"];
                        grvTaxation.DataBind();

                        GridViewRow gRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                        Label lblTaxPOSrNo = (Label)gRow.FindControl("lblTaxPOSrNo");
                        DataTable ddt = (DataTable)ViewState["ItemData"];
                        DataRow dr = ddt.Select("ID = '" + lblTaxPOSrNo.Text + "'")[0];
                        dr[11] = dr[12];
                        grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                        grvListItem.DataBind();


                        callAMT();
                    }
                    if (e.CommandName == "eEdit")
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

        protected void grvCharges_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (e.CommandName == "eDelete")
                    {
                        int index = Convert.ToInt32(e.CommandArgument);
                        DataTable dt = (DataTable)ViewState["ChargesData"];
                        GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                        dt.Rows[row.RowIndex].Delete();
                        ViewState["ChargesData"] = dt;
                        grvCharges.DataSource = ViewState["ChargesData"];
                        grvCharges.DataBind();

                        callAMT();
                    }
                    if (e.CommandName == "eEdit")
                    {
                        int index = Convert.ToInt32(e.CommandArgument);
                        txtSrNoChg.Text = Convert.ToString(index);
                        GridViewRow gRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;

                        Label lblChrgSrNo = (Label)gRow.FindControl("lblChrgSrNo");
                        Label lblChrgCondType = (Label)gRow.FindControl("lblChrgCondType");
                        Label lblChrgAmount = (Label)gRow.FindControl("lblChrgAmount");

                        txtSrNoChg.Text = lblChrgSrNo.Text;
                        ddlCharges.SelectedValue = lblChrgCondType.Text;
                        txtChgAmt.Text = lblChrgAmount.Text;


                        Session["saveCharge"] = "Update Charge";
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

        protected void lnkOpenCustomerPopup_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    objBindDDL.FillCustype(ddlPopupCustomerType, "SELECTCUSTTYPE");
                    objBindDDL.FillCity(ddlPopupCustomerCity);

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Customer').modal();", true);
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

        protected void lnkOpenShipmentPopup_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    objBindDDL.FillCustype(ddlPopupShipperType, "SELECTCUSTTYPE");
                    objBindDDL.FillCity(ddlPopupShipperCity);

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Shipment').modal();", true);
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

        protected void lnkPopupCustomerShow_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetCustData(objMainClass.intCmpId, txtPopupCustomerCode.Text == "" ? "" : objMainClass.strConvertZeroPadding(txtPopupCustomerCode.Text),
                       ddlPopupCustomerType.SelectedIndex > 0 ? ddlPopupCustomerType.SelectedValue : "", txtPopupCustomerName.Text, ddlPopupCustomerCity.SelectedIndex > 0 ? ddlPopupCustomerCity.SelectedValue : "",
                       "SEARCHCUST");
                    if (dt.Rows.Count > 0)
                    {
                        grvPopCustomer.DataSource = dt;
                        grvPopCustomer.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Customer').modal();", true);
                    }
                    else
                    {
                        grvPopCustomer.DataSource = string.Empty;
                        grvPopCustomer.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found.');", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Customer').modal();", true);
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

        protected void grvPopCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    txtCustomer.Text = Convert.ToString(grvPopCustomer.SelectedRow.Cells[2].Text);
                    txtCustomer_TextChanged(1, e);
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

        protected void lnkPopupShipperShow_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetCustData(objMainClass.intCmpId, txtPopupShipperCode.Text == "" ? "" : objMainClass.strConvertZeroPadding(txtPopupShipperCode.Text),
                       ddlPopupShipperType.SelectedIndex > 0 ? ddlPopupShipperType.SelectedValue : "", txtPopupShipperCustName.Text, ddlPopupShipperCity.SelectedIndex > 0 ? ddlPopupShipperCity.SelectedValue : "",
                       "SEARCHCUST");
                    if (dt.Rows.Count > 0)
                    {
                        gvShipperList.DataSource = dt;
                        gvShipperList.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Shipment').modal();", true);
                    }
                    else
                    {
                        gvShipperList.DataSource = string.Empty;
                        gvShipperList.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found.');", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Shipment').modal();", true);
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

        protected void gvShipperList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    txtShipment.Text = Convert.ToString(gvShipperList.SelectedRow.Cells[2].Text);
                    txtShipment_TextChanged(1, e);
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

        protected void txtJobId_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (txtJobId.Text.Length >= 7)
                    {
                        DataTable dt = new DataTable();
                        dt = objMainClass.GetJSDetails(txtJobId.Text);
                        if (dt.Rows.Count > 0)
                        {
                            if (Convert.ToString(dt.Rows[0]["JOBSTATUS"]) == "23" || Convert.ToString(dt.Rows[0]["JOBSTATUS"]) == "3")
                            {
                                lblJSIMEI.Text = string.Empty;
                                txtIMEI.Text = string.Empty;
                                txtJobId.Text = string.Empty;
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record found or Job id already Closed/Cancelled.!');", true);
                                lblJSIMEI.Text = string.Empty;
                                txtIMEI.Text = string.Empty;
                                txtJobId.Text = string.Empty;
                            }
                            else
                            {
                                if ((Convert.ToString(dt.Rows[0]["JOBSTATUS"]) == "53" && Convert.ToString(dt.Rows[0]["STAGEID"]) == "46") || (Convert.ToString(dt.Rows[0]["JOBSTATUS"]) == "21" && Convert.ToString(dt.Rows[0]["STAGEID"]) == "24"))
                                {
                                    txtJobId.Text = objMainClass.strConvertZeroPadding(Convert.ToString(dt.Rows[0]["JOBID"]));
                                    txtIMEI.Text = objMainClass.strConvertZeroPadding(Convert.ToString(dt.Rows[0]["IMEINO"]));
                                    ddlPLant.SelectedValue = objMainClass.strConvertZeroPadding(Convert.ToString(dt.Rows[0]["PLANTCD"]));
                                    ddlLocation.SelectedValue = objMainClass.strConvertZeroPadding(Convert.ToString(dt.Rows[0]["LOCCD"]));

                                    txtIMEI_TextChanged(1, e);

                                    DataTable dtItem = new DataTable();
                                    dtItem = objMainClass.GetJSDetailsItem(objMainClass.intCmpId, txtJobId.Text);
                                    if (dtItem.Rows.Count > 0)
                                    {
                                        if (Convert.ToString(dtItem.Rows[0]["ITEMCODE"]) != null && Convert.ToString(dtItem.Rows[0]["ITEMCODE"]) != String.Empty && Convert.ToString(dtItem.Rows[0]["ITEMCODE"]) != "")
                                        {
                                            txtItemCode.Text = Convert.ToString(dtItem.Rows[0]["ITEMCODE"]);
                                            txtItemCode.Enabled = false;
                                            txtItemCode_TextChanged(1, e);
                                        }
                                        else
                                        {
                                            dtItem = objMainClass.GetJSDetailsItemNew(objMainClass.intCmpId, txtJobId.Text);
                                            if (dtItem.Rows.Count > 0)
                                            {
                                                txtItemCode.Text = Convert.ToString(dtItem.Rows[0]["ITEMCODE"]);
                                                txtItemCode.Enabled = false;
                                                txtItemCode_TextChanged(1, e);
                                            }
                                            else
                                            {
                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Item Code not found! Enter manually.!');", true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        dtItem = objMainClass.GetJSDetailsItemNew(objMainClass.intCmpId, txtJobId.Text);
                                        if (dtItem.Rows.Count > 0)
                                        {
                                            txtItemCode.Text = Convert.ToString(dtItem.Rows[0]["ITEMCODE"]);
                                            txtItemCode.Enabled = false;
                                            txtItemCode_TextChanged(1, e);
                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Item Code not found! Enter manually.!');", true);
                                        }
                                    }
                                }
                                else
                                {
                                    lblJSIMEI.Text = string.Empty;
                                    txtIMEI.Text = string.Empty;
                                    txtJobId.Text = string.Empty;
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Job id must be in Phy. Doc. Var. or Forward Waybill Generated Stage..!');", true);
                                    lblJSIMEI.Text = string.Empty;
                                    txtIMEI.Text = string.Empty;
                                    txtJobId.Text = string.Empty;
                                }

                                //lblJSIMEI.Text = Convert.ToString(dt.Rows[0]["IMEINO"]);
                                //if (txtIMEI.Text != "" && txtIMEI.Text != null && txtIMEI.Text != string.Empty)
                                //{
                                //    if (txtIMEI.Text == lblJSIMEI.Text)
                                //    {

                                //    }
                                //    else
                                //    {
                                //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('IMEI No. is not matched with PO detail!');", true);
                                //    }
                                //}
                            }
                        }
                        else
                        {
                            lblJSIMEI.Text = string.Empty;
                            txtIMEI.Text = string.Empty;
                            txtJobId.Text = string.Empty;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid job id.!');", true);
                            lblJSIMEI.Text = string.Empty;
                            txtIMEI.Text = string.Empty;
                            txtJobId.Text = string.Empty;
                        }
                    }
                    else
                    {
                        lblJSIMEI.Text = string.Empty;
                        txtIMEI.Text = string.Empty;
                        txtJobId.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid job id.!');", true);
                        lblJSIMEI.Text = string.Empty;
                        txtIMEI.Text = string.Empty;
                        txtJobId.Text = string.Empty;
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

        protected void ddlSalesChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (ddlSalesChannel.SelectedValue != "582" || ddlSalesChannel.SelectedValue != "11195" || ddlSalesChannel.SelectedValue != "11194")
                    {
                        txtDeliveryDate.Text = objMainClass.indianTime.Date.AddDays(2).ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    }


                    if ((ddlPayMode.SelectedValue == "5" || ddlPayMode.SelectedValue == "6" || ddlPayMode.SelectedValue == "12" || ddlPayMode.SelectedValue == "15") && ddlSalesChannel.SelectedValue == "11193")
                    {
                        txtPartialAmount.Text = string.Empty;
                        txtPartialAmount.Visible = false;
                        rfvPartialAmount.Enabled = false;
                        divTransaction.Visible = true;
                        objBindDDL.FillPaymentGateway(ddlPayGateway);
                        rfvTXNID.Enabled = true;
                        rfvTXNDT.Enabled = true;
                        rfvPayGateway.Enabled = true;
                        txtTXNDT.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        txtPartialAmount.Text = string.Empty;
                        txtPartialAmount.Visible = false;
                        rfvPartialAmount.Enabled = false;

                        divTransaction.Visible = false;
                        rfvTXNID.Enabled = false;
                        rfvTXNDT.Enabled = false;
                        rfvPayGateway.Enabled = false;
                        txtTXNDT.Text = "";
                        txtTXNID.Text = "";
                        ddlPayGateway.SelectedValue = "0";
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

        protected void ddlpopMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    objBindDDL.FillModel(ddlpopModel, ddlpopMake.SelectedValue);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-item').modal();", true);
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

        protected void btnShowItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.SelectItem(ddlpopMake.SelectedIndex > 0 ? ddlpopMake.SelectedItem.Text : "", ddlpopModel.SelectedIndex > 0 ? ddlpopModel.SelectedItem.Text : "", txtpopItemCode.Text, ddlpopGroup.SelectedIndex > 0 ? ddlpopGroup.SelectedValue : "", ddlpopSubGroup.SelectedIndex > 0 ? ddlpopSubGroup.SelectedValue : "", ddlpopCategory.SelectedIndex > 0 ? ddlpopCategory.SelectedValue : "", txtPopupItemDesc.Text);
                    if (dt.Rows.Count > 0)
                    {
                        grvPopItem.DataSource = dt;
                        grvPopItem.DataBind();
                    }
                    else
                    {
                        grvPopItem.DataSource = string.Empty;
                        grvPopItem.DataBind();
                    }
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-item').modal();", true);
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

        protected void grvPopItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    txtItemCode.Text = Convert.ToString(grvPopItem.SelectedRow.Cells[1].Text);
                    txtItemCode_TextChanged(1, e);
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

        protected void btnSaveDet_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dtLock = new DataTable();
                    dtLock = objMainClass.GetLockPrice("", "", "", "", "", "", txtItemDesc.Text);
                    decimal lockprice = 0;
                    if (dtLock.Rows.Count > 0)
                    {
                        lockprice = Convert.ToDecimal(dtLock.Rows[0]["FinalApproveListingAmount"]);
                    }

                    if (txtIMEI.Text != null && txtIMEI.Text != "" && txtIMEI.Text != string.Empty && txtJobId.Text != "" && txtJobId.Text != null && txtJobId.Text != string.Empty)
                    {
                        if (txtIMEI.Text == lblJSIMEI.Text)
                        {


                            string validation = validateData();
                            if (validation == "OK")
                            {


                                if (Convert.ToString(Session["savedet"]) == "Save Item")
                                {


                                    DataTable dt = (DataTable)ViewState["ItemData"];
                                    DataTable dtTaxation = (DataTable)ViewState["TaxData"];
                                    if (grvListItem.Rows.Count > 0)
                                    {
                                        DataRow lastRow = dt.Rows[dt.Rows.Count - 1];
                                        int id = Convert.ToInt32(lastRow["ID"]) + 1;



                                        dt.Rows.Add(id, txtItemCode.Text, txtItemDesc.Text, txtItemId.Text, txtItemGroup.Text, txtItemGroupId.Text, ddlUOM.SelectedItem.Text, ddlUOM.SelectedValue,
                                            ddlGrade.SelectedItem.Text, ddlGrade.SelectedValue, txtItemQty.Text, txtRate.Text, txtItemBRate.Text, txtAmount.Text, txtDiscountRate.Text,
                                            txtDeliveryDate.Text, txtGLCode.Text, ddlCostCenter.SelectedValue, ddlPLant.SelectedItem.Text, ddlPLant.SelectedValue, ddlLocation.SelectedItem.Text,
                                            ddlLocation.SelectedValue, txtProfitCenter.Text, txtJobId.Text, txtItemRemarks.Text, txtIMEI.Text, txtItemText.Text, txtCustPartNo.Text,
                                            txtCustPartDesc.Text, txtSku.Text, lockprice);


                                        ViewState["ItemData"] = dt;

                                        if (ddlConditionType.SelectedIndex > 0)
                                        {
                                            int idTax;
                                            if (grvTaxation.Rows.Count > 0)
                                            {
                                                DataRow lastRowTax = dtTaxation.Rows[dtTaxation.Rows.Count - 1];
                                                idTax = Convert.ToInt32(lastRowTax["TAXSRNO"]) + 1;
                                            }
                                            else
                                            {
                                                idTax = 1;
                                            }


                                            dtTaxation.Rows.Add(ddlOperator.SelectedIndex > 0 ? ddlOperator.SelectedValue : "+", idTax, id, ddlConditionType.SelectedValue,
                                                hfRate.Value, txtAmount.Text, txtTaxTAmount.Text, hfPID.Value, hfCONDID.Value);
                                            ViewState["TaxData"] = dtTaxation;
                                        }

                                    }
                                    else
                                    {

                                        dt.Rows.Add("1", txtItemCode.Text, txtItemDesc.Text, txtItemId.Text, txtItemGroup.Text, txtItemGroupId.Text, ddlUOM.SelectedItem.Text, ddlUOM.SelectedValue,
                                            ddlGrade.SelectedItem.Text, ddlGrade.SelectedValue, txtItemQty.Text, txtRate.Text, txtItemBRate.Text, txtAmount.Text, txtDiscountRate.Text,
                                            txtDeliveryDate.Text, txtGLCode.Text, ddlCostCenter.SelectedValue, ddlPLant.SelectedItem.Text, ddlPLant.SelectedValue, ddlLocation.SelectedItem.Text,
                                            ddlLocation.SelectedValue, txtProfitCenter.Text, txtJobId.Text, txtItemRemarks.Text, txtIMEI.Text, txtItemText.Text, txtCustPartNo.Text,
                                            txtCustPartDesc.Text, txtSku.Text, lockprice);




                                        ViewState["ItemData"] = dt;

                                        if (ddlConditionType.SelectedIndex > 0)
                                        {


                                            dtTaxation.Rows.Add(ddlOperator.SelectedIndex > 0 ? ddlOperator.SelectedValue : "+", "1", "1", ddlConditionType.SelectedValue,
                                                hfRate.Value, txtAmount.Text, txtTaxTAmount.Text, hfPID.Value, hfCONDID.Value); ;
                                            ViewState["TaxData"] = dtTaxation;
                                        }


                                    }

                                    grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                                    grvListItem.DataBind();

                                    grvTaxation.DataSource = (DataTable)ViewState["TaxData"];
                                    grvTaxation.DataBind();

                                    //EmptyString();
                                }
                                else if (Convert.ToString(Session["savedet"]) == "Update Item")
                                {

                                    DataTable ddt = (DataTable)ViewState["ItemData"];
                                    DataRow dr = ddt.Select("ID = '" + txtSRNO.Text + "'")[0];
                                    dr[0] = txtSRNO.Text;
                                    dr[1] = txtItemCode.Text;
                                    dr[2] = txtItemDesc.Text;
                                    dr[3] = txtItemId.Text;
                                    dr[4] = txtItemGroup.Text;
                                    dr[5] = txtItemGroupId.Text;
                                    dr[6] = ddlUOM.SelectedItem.Text;
                                    dr[7] = ddlUOM.SelectedValue;
                                    dr[8] = ddlGrade.SelectedItem.Text;
                                    dr[9] = ddlGrade.SelectedValue;
                                    dr[10] = txtItemQty.Text;
                                    dr[11] = txtRate.Text;
                                    dr[12] = txtItemBRate.Text;
                                    dr[13] = txtAmount.Text;
                                    dr[14] = txtDiscountRate.Text;
                                    dr[15] = txtDeliveryDate.Text;
                                    dr[16] = txtGLCode.Text;
                                    dr[17] = ddlCostCenter.SelectedValue;
                                    dr[18] = ddlPLant.SelectedItem.Text;
                                    dr[19] = ddlPLant.SelectedValue;
                                    dr[20] = ddlLocation.SelectedItem.Text;
                                    dr[21] = ddlLocation.SelectedValue;
                                    dr[22] = txtProfitCenter.Text;
                                    dr[23] = txtJobId.Text == "" ? "0" : txtJobId.Text;
                                    dr[24] = txtItemRemarks.Text;
                                    dr[25] = txtIMEI.Text;
                                    dr[26] = txtItemText.Text;
                                    dr[27] = txtCustPartNo.Text;
                                    dr[28] = txtCustPartDesc.Text;
                                    dr[29] = txtSku.Text;
                                    dr[30] = lockprice;
                                    //dr[27] = "";
                                    //dr[28] = txtSku.Text;

                                    if (ddlConditionType.SelectedIndex > 0)
                                    {

                                        DataTable dtTax = (DataTable)ViewState["TaxData"];
                                        if (dtTax.Rows.Count > 0)
                                        {
                                            bool Isrecordavailable = IsRecordAvailableAtDataTable(Convert.ToInt32(txtSRNO.Text));
                                            if (Isrecordavailable)
                                            {
                                                DataRow drt = dtTax.Select("SOSRNO='" + txtSRNO.Text + "'")[0];
                                                if (drt != null)
                                                {
                                                    drt[0] = ddlOperator.SelectedIndex > 0 ? ddlOperator.SelectedValue : "+";
                                                    drt[2] = txtSRNO.Text;
                                                    drt[3] = ddlConditionType.SelectedValue;
                                                    drt[4] = hfRate.Value;
                                                    drt[5] = txtAmount.Text;
                                                    drt[6] = txtTaxTAmount.Text;
                                                    drt[7] = hfPID.Value;
                                                    drt[8] = hfCONDID.Value;
                                                }
                                            }
                                            else
                                            {
                                                int idTax;
                                                DataTable dtTaxation = (DataTable)ViewState["TaxData"];
                                                DataRow lastRowTax = dtTaxation.Rows[dtTaxation.Rows.Count - 1];
                                                idTax = Convert.ToInt32(lastRowTax["TAXSRNO"]) + 1;
                                                dtTaxation.Rows.Add(ddlOperator.SelectedIndex > 0 ? ddlOperator.SelectedValue : "+", idTax, txtSRNO.Text, ddlConditionType.SelectedValue,
                                                hfRate.Value, txtAmount.Text, txtTaxTAmount.Text, hfPID.Value, hfCONDID.Value);
                                                ViewState["TaxData"] = dtTaxation;
                                            }
                                        }
                                        else
                                        {
                                            DataTable dtTaxation = (DataTable)ViewState["TaxData"];

                                            dtTaxation.Rows.Add(ddlOperator.SelectedIndex > 0 ? ddlOperator.SelectedValue : "+", "1", txtSRNO.Text, ddlConditionType.SelectedValue,
                                            hfRate.Value, txtAmount.Text, txtTaxTAmount.Text, hfPID.Value, hfCONDID.Value);
                                            ViewState["TaxData"] = dtTaxation;
                                        }
                                    }

                                    grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                                    grvListItem.DataBind();

                                    grvTaxation.DataSource = (DataTable)ViewState["TaxData"];
                                    grvTaxation.DataBind();

                                    Session["savedet"] = "Save Item";


                                }
                                callAMT();

                                EmptyString();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + validation + "\");", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('IMEI No. is not matched with Jobs detail!');", true);
                        }
                    }
                    else
                    {
                        string validation = validateData();
                        if (validation == "OK")
                        {

                            if (Convert.ToString(Session["savedet"]) == "Save Item")
                            {

                                DataTable dt = (DataTable)ViewState["ItemData"];
                                DataTable dtTaxation = (DataTable)ViewState["TaxData"];
                                if (grvListItem.Rows.Count > 0)
                                {
                                    DataRow lastRow = dt.Rows[dt.Rows.Count - 1];
                                    int id = Convert.ToInt32(lastRow["ID"]) + 1;

                                    dt.Rows.Add(id, txtItemCode.Text, txtItemDesc.Text, txtItemId.Text, txtItemGroup.Text, txtItemGroupId.Text, ddlUOM.SelectedItem.Text, ddlUOM.SelectedValue,
                                        ddlGrade.SelectedItem.Text, ddlGrade.SelectedValue, txtItemQty.Text, txtRate.Text, txtItemBRate.Text, txtAmount.Text, txtDiscountRate.Text,
                                        txtDeliveryDate.Text, txtGLCode.Text, ddlCostCenter.SelectedValue, ddlPLant.SelectedItem.Text, ddlPLant.SelectedValue, ddlLocation.SelectedItem.Text,
                                        ddlLocation.SelectedValue, txtProfitCenter.Text, txtJobId.Text, txtItemRemarks.Text, txtIMEI.Text, txtItemText.Text, txtCustPartNo.Text,
                                        txtCustPartDesc.Text, txtSku.Text, lockprice);


                                    ViewState["ItemData"] = dt;

                                    if (ddlConditionType.SelectedIndex > 0)
                                    {
                                        int idTax;
                                        if (grvTaxation.Rows.Count > 0)
                                        {
                                            DataRow lastRowTax = dtTaxation.Rows[dtTaxation.Rows.Count - 1];
                                            idTax = Convert.ToInt32(lastRowTax["TAXSRNO"]) + 1;
                                        }
                                        else
                                        {
                                            idTax = 1;
                                        }


                                        dtTaxation.Rows.Add(ddlOperator.SelectedIndex > 0 ? ddlOperator.SelectedValue : "+", idTax, id, ddlConditionType.SelectedValue,
                                            hfRate.Value, txtAmount.Text, txtTaxTAmount.Text, hfPID.Value, hfCONDID.Value);
                                        ViewState["TaxData"] = dtTaxation;
                                    }

                                }
                                else
                                {

                                    dt.Rows.Add("1", txtItemCode.Text, txtItemDesc.Text, txtItemId.Text, txtItemGroup.Text, txtItemGroupId.Text, ddlUOM.SelectedItem.Text, ddlUOM.SelectedValue,
                                        ddlGrade.SelectedItem.Text, ddlGrade.SelectedValue, txtItemQty.Text, txtRate.Text, txtItemBRate.Text, txtAmount.Text, txtDiscountRate.Text,
                                        txtDeliveryDate.Text, txtGLCode.Text, ddlCostCenter.SelectedValue, ddlPLant.SelectedItem.Text, ddlPLant.SelectedValue, ddlLocation.SelectedItem.Text,
                                        ddlLocation.SelectedValue, txtProfitCenter.Text, txtJobId.Text, txtItemRemarks.Text, txtIMEI.Text, txtItemText.Text, txtCustPartNo.Text,
                                        txtCustPartDesc.Text, txtSku.Text, lockprice);




                                    ViewState["ItemData"] = dt;

                                    if (ddlConditionType.SelectedIndex > 0)
                                    {


                                        dtTaxation.Rows.Add(ddlOperator.SelectedIndex > 0 ? ddlOperator.SelectedValue : "+", "1", "1", ddlConditionType.SelectedValue,
                                            hfRate.Value, txtAmount.Text, txtTaxTAmount.Text, hfPID.Value, hfCONDID.Value); ;
                                        ViewState["TaxData"] = dtTaxation;
                                    }


                                }

                                grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                                grvListItem.DataBind();

                                grvTaxation.DataSource = (DataTable)ViewState["TaxData"];
                                grvTaxation.DataBind();

                                //EmptyString();
                            }
                            else if (Convert.ToString(Session["savedet"]) == "Update Item")
                            {

                                DataTable ddt = (DataTable)ViewState["ItemData"];
                                DataRow dr = ddt.Select("ID = '" + txtSRNO.Text + "'")[0];
                                dr[0] = txtSRNO.Text;
                                dr[1] = txtItemCode.Text;
                                dr[2] = txtItemDesc.Text;
                                dr[3] = txtItemId.Text;
                                dr[4] = txtItemGroup.Text;
                                dr[5] = txtItemGroupId.Text;
                                dr[6] = ddlUOM.SelectedItem.Text;
                                dr[7] = ddlUOM.SelectedValue;
                                dr[8] = ddlGrade.SelectedItem.Text;
                                dr[9] = ddlGrade.SelectedValue;
                                dr[10] = txtItemQty.Text;
                                dr[11] = txtRate.Text;
                                dr[12] = txtItemBRate.Text;
                                dr[13] = txtAmount.Text;
                                dr[14] = txtDiscountRate.Text;
                                dr[15] = txtDeliveryDate.Text;
                                dr[16] = txtGLCode.Text;
                                dr[17] = ddlCostCenter.SelectedValue;
                                dr[18] = ddlPLant.SelectedItem.Text;
                                dr[19] = ddlPLant.SelectedValue;
                                dr[20] = ddlLocation.SelectedItem.Text;
                                dr[21] = ddlLocation.SelectedValue;
                                dr[22] = txtProfitCenter.Text;
                                dr[23] = txtJobId.Text == "" ? "0" : txtJobId.Text;
                                dr[24] = txtItemRemarks.Text;
                                dr[25] = txtIMEI.Text;
                                dr[26] = txtItemText.Text;
                                dr[27] = txtCustPartNo.Text;
                                dr[28] = txtCustPartDesc.Text;
                                dr[29] = txtSku.Text;
                                dr[30] = lockprice;
                                //dr[27] = "";
                                //dr[28] = txtSku.Text;

                                if (ddlConditionType.SelectedIndex > 0)
                                {

                                    DataTable dtTax = (DataTable)ViewState["TaxData"];
                                    if (dtTax.Rows.Count > 0)
                                    {
                                        bool Isrecordavailable = IsRecordAvailableAtDataTable(Convert.ToInt32(txtSRNO.Text));
                                        if (Isrecordavailable)
                                        {
                                            DataRow drt = dtTax.Select("SOSRNO='" + txtSRNO.Text + "'")[0];
                                            if (drt != null)
                                            {
                                                drt[0] = ddlOperator.SelectedIndex > 0 ? ddlOperator.SelectedValue : "+";
                                                drt[2] = txtSRNO.Text;
                                                drt[3] = ddlConditionType.SelectedValue;
                                                drt[4] = hfRate.Value;
                                                drt[5] = txtAmount.Text;
                                                drt[6] = txtTaxTAmount.Text;
                                                drt[7] = hfPID.Value;
                                                drt[8] = hfCONDID.Value;
                                            }
                                        }
                                        else
                                        {
                                            int idTax;
                                            DataTable dtTaxation = (DataTable)ViewState["TaxData"];
                                            DataRow lastRowTax = dtTaxation.Rows[dtTaxation.Rows.Count - 1];
                                            idTax = Convert.ToInt32(lastRowTax["TAXSRNO"]) + 1;
                                            dtTaxation.Rows.Add(ddlOperator.SelectedIndex > 0 ? ddlOperator.SelectedValue : "+", idTax, txtSRNO.Text, ddlConditionType.SelectedValue,
                                            hfRate.Value, txtAmount.Text, txtTaxTAmount.Text, hfPID.Value, hfCONDID.Value);
                                            ViewState["TaxData"] = dtTaxation;
                                        }
                                    }
                                    else
                                    {
                                        DataTable dtTaxation = (DataTable)ViewState["TaxData"];

                                        dtTaxation.Rows.Add(ddlOperator.SelectedIndex > 0 ? ddlOperator.SelectedValue : "+", "1", txtSRNO.Text, ddlConditionType.SelectedValue,
                                        hfRate.Value, txtAmount.Text, txtTaxTAmount.Text, hfPID.Value, hfCONDID.Value);
                                        ViewState["TaxData"] = dtTaxation;
                                    }
                                }

                                grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                                grvListItem.DataBind();

                                grvTaxation.DataSource = (DataTable)ViewState["TaxData"];
                                grvTaxation.DataBind();

                                Session["savedet"] = "Save Item";


                            }
                            callAMT();

                            EmptyString();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + validation + "\");", true);
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


        private string validateData()
        {
            string j = "ERROR";
            string PLantRights = string.Empty;
            string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
            for (int i = 0; i < plantArray.Count(); i++)
            {
                if (plantArray[i].Trim() == ddlPLant.SelectedValue)
                {
                    PLantRights = ddlPLant.SelectedValue;
                }
            }
            if (PLantRights.Length > 0)
            {
                j = "OK";
            }
            else
            {
                j = "You do not have plant rights. ";
            }
            return j;
        }


        private void EmptyString()
        {
            txtItemCode.Text = string.Empty;
            txtItemId.Text = string.Empty;
            txtSku.Text = string.Empty;
            txtItemGroup.Text = string.Empty;
            txtItemGroupId.Text = string.Empty;
            txtGLCode.Text = string.Empty;
            txtItemDesc.Text = string.Empty;
            txtItemQty.Text = string.Empty;
            txtItemBRate.Text = string.Empty;
            txtAmount.Text = string.Empty;
            txtJobId.Text = string.Empty;
            txtDeliveryDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
            txtItemRemarks.Text = string.Empty;
            ddlOperator.SelectedIndex = 0;
            ddlConditionType.SelectedIndex = 0;
            txtTaxTAmount.Text = string.Empty;
            hfRate.Value = string.Empty;
            hfPID.Value = string.Empty;
            hfCONDID.Value = string.Empty;
            txtSRNO.Text = string.Empty;

            txtItemText.Text = string.Empty;
            txtCustPartNo.Text = string.Empty;
            txtCustPartDesc.Text = string.Empty;

        }

        protected void txtRefNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    DataTable dtSO = new DataTable();
                    dtSO = objMainClass.GetSODetails(objMainClass.intCmpId, ddlDoctype.SelectedValue, txtIMEI.Text, "CHECKREFNO", txtRefNo.Text, "");
                    if (dtSO.Rows.Count > 0)
                    {
                        txtRefNo.Text = "";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('SO is already created for mentioned Ref. No.! SO No.: " + Convert.ToString(dtSO.Rows[0]["SONO"]) + " ' );", true);
                        txtRefNo.Text = "";
                        //lblSODET.Text = "SO is already created for mentioned Ref. No.! SO No.: " + Convert.ToString(dtSO.Rows[0]["SONO"]);
                        //lblSODETNEW.Text = "Do you want to create SO for same Ref. No.?";
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-SODet').modal();", true);

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

        protected void btnSODETYES_Click(object sender, EventArgs e)
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

        protected void btnSODETNO_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    txtRefNo.Text = string.Empty;
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

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            int index = Int32.Parse(e.Item.Value);
            MultiView1.ActiveViewIndex = index;
        }

        protected void ddlPayMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (ddlPayMode.SelectedValue == "14")
                    {
                        txtPartialAmount.Text = string.Empty;
                        txtPartialAmount.Visible = true;
                        rfvPartialAmount.Enabled = true;

                        divTransaction.Visible = false;
                        rfvTXNID.Enabled = false;
                        rfvTXNDT.Enabled = false;
                        rfvPayGateway.Enabled = false;
                        txtTXNDT.Text = "";
                        txtTXNID.Text = "";
                        ddlPayGateway.SelectedValue = "0";
                    }
                    else if ((ddlPayMode.SelectedValue == "5" || ddlPayMode.SelectedValue == "6" || ddlPayMode.SelectedValue == "12" || ddlPayMode.SelectedValue == "15") && (ddlSalesChannel.SelectedValue == "11193" || ddlSalesChannel.SelectedValue == "11411"))
                    {
                        txtPartialAmount.Text = string.Empty;
                        txtPartialAmount.Visible = false;
                        rfvPartialAmount.Enabled = false;
                        divTransaction.Visible = true;
                        objBindDDL.FillPaymentGateway(ddlPayGateway);
                        rfvTXNID.Enabled = true;
                        rfvTXNDT.Enabled = true;
                        rfvPayGateway.Enabled = true;
                        txtTXNDT.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        txtPartialAmount.Text = string.Empty;
                        txtPartialAmount.Visible = false;
                        rfvPartialAmount.Enabled = false;

                        divTransaction.Visible = false;
                        rfvTXNID.Enabled = false;
                        rfvTXNDT.Enabled = false;
                        rfvPayGateway.Enabled = false;
                        txtTXNDT.Text = "";
                        txtTXNID.Text = "";
                        ddlPayGateway.SelectedValue = "0";
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

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    if (ddlState.SelectedIndex > 0)
                    {
                        objBindDDL.FillCity(ddlCity, ddlState.SelectedValue);
                    }
                    else
                    {
                        objBindDDL.FillCity(ddlCity);
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

        protected void ddlSalesScheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (ddlSalesScheme.SelectedValue == "12019")
                    {
                        lblOldSoNo.Visible = true;
                        txtOldSONo.Visible = true;
                        rfvoldSoNo.Enabled = true;
                    }
                    else
                    {
                        lblOldSoNo.Visible = false;
                        txtOldSONo.Visible = false;
                        rfvoldSoNo.Enabled = false;
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

        protected void txtOldSONo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetSODetails(objMainClass.intCmpId, "", "", "GETSOMST", "", txtOldSONo.Text);

                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToString(dt.Rows[0]["SONO"]) != "" && Convert.ToString(dt.Rows[0]["SONO"]) != string.Empty && Convert.ToString(dt.Rows[0]["SONO"]) != null && Convert.ToString(dt.Rows[0]["STATUS"]) == "57")
                        {

                        }
                        else
                        {
                            txtOldSONo.Text = string.Empty;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Wrong SO No. Please enter correct SO No.!');", true);
                            txtOldSONo.Text = string.Empty;
                        }
                    }
                    else
                    {
                        txtOldSONo.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Wrong SO No. Please enter correct SO No.!');", true);
                        txtOldSONo.Text = string.Empty;
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

        protected void ddlDoctype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    txtSONO.Text = objMainClass.MAXPRNO(ddlDoctype.SelectedItem.Text, "SO");
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