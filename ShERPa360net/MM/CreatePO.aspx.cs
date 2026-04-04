using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.MM
{
    public partial class CreatePO : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        DataTable dtItem = new DataTable();
        DataTable dtTAX = new DataTable();
        DataTable dtCharges = new DataTable();


        protected void Page_Load(object sender, EventArgs e)
        {

            txtPODATE.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();
            txtDeliveryDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");

            if (!IsPostBack)
            {
                try
                {
                    if (Session["USERID"] != null)
                    {
                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menutabid.Value, "");
                        if (FormRights.bView == false)
                        {
                            //Session.Abandon();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }


                        if (Request.QueryString.Count > 0)
                        {
                            if (Convert.ToString(Request.QueryString["PONO"]) != null && Convert.ToString(Request.QueryString["PONO"]) != string.Empty && Convert.ToString(Request.QueryString["PONO"]) != "")
                            {
                                Session["EditPONo"] = Convert.ToString(Request.QueryString["PONO"]);

                            }
                            else if (Convert.ToString(Request.QueryString["FormName"]) == "FromReq")
                            {
                                Session["ReqNo"] = Convert.ToString(Request.QueryString["ReqNo"]);
                            }
                            else if (Convert.ToString(Request.QueryString["PRNO"]) != null && Convert.ToString(Request.QueryString["PRNO"]) != string.Empty && Convert.ToString(Request.QueryString["PRNO"]) != "")
                            {
                                Session["PRNO"] = Convert.ToString(Request.QueryString["PRNO"]);
                                if (Convert.ToString(Request.QueryString["VENDCODE"]) != null && Convert.ToString(Request.QueryString["VENDCODE"]) != string.Empty && Convert.ToString(Request.QueryString["VENDCODE"]) != "")
                                {
                                    Session["VENDCODE"] = Convert.ToString(Request.QueryString["VENDCODE"]);
                                }
                            }
                            Response.Redirect(Request.Url.AbsolutePath, false);
                        }
                        else
                        {
                            objBindDDL.FillDocType(ddlDoctype, "PO");
                            ddlDoctype.SelectedIndex = 2;
                            ddlDoctype.Enabled = false;

                            objBindDDL.FillDetparment(ddlDepartment);
                            ddlDepartment.SelectedValue = "5";

                            objBindDDL.FillCondition(ddlConditionType);
                            objBindDDL.FillPurChrgType(ddlCharges, objMainClass.intCmpId);

                            objBindDDL.FillPlant(ddlPLant);
                            ddlPLant.SelectedIndex = 1;
                            objBindDDL.FillPayTerm(ddlPaymentTerms);
                            ddlPaymentTerms.SelectedIndex = 1;
                            txtPaymentTermsDesc.Text = Convert.ToString(ddlPaymentTerms.SelectedItem.Text.Split('-')[1].Trim());
                            txtPaymentTermsDesc.Enabled = false;
                            objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);
                            //ddlLocation.SelectedValue = "MS01";
                            if (ddlPLant.SelectedValue == "1001")
                            {
                                ddlLocation.SelectedValue = "M001";
                            }
                            else
                            {
                                ddlLocation.SelectedValue = "MS01";
                            }
                            objBindDDL.FillUOM(ddlUOM);
                            ddlPLant.SelectedIndex = 1;
                            txtPONO.Text = objMainClass.MAXPRNO(ddlDoctype.SelectedValue, "PO");
                            //txtPRDATE.Text = objMainClass.setDateFormat(Convert.ToDateTime(DateTime.Now).ToShortDateString(), true);
                            txtPODATE.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();
                            txtDeliveryDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                            DataTable dt = objMainClass.GetPRFCost(ddlPLant.SelectedValue, Convert.ToString(Session["USERID"]));
                            txtProfitCenter.Text = Convert.ToString(dt.Rows[0]["PRFCNT"]);
                            //ddlCostCenter.SelectedValue = Convert.ToString(dt.Rows[0]["CSTCENTCD"]);

                            objBindDDL.FillCostCenter(ddlCostCenter, ddlPLant.SelectedValue, ddlLocation.SelectedValue);

                            Session["savedet"] = "Save Item";
                            Session["saveall"] = "Save All";
                            Session["saveCharge"] = "Save Charge";
                            SetUpGrid();

                            objBindDDL.FillItemCat(ddlpopCategory);
                            objBindDDL.FillItemGrp(ddlpopGroup);
                            objBindDDL.FillItemSubGrp(ddlpopSubGroup);
                            objBindDDL.FillBrand(ddlpopMake, 0);

                            //objBindDDL.FillItemCat(ddlNewCategory);
                            //ddlNewCategory.SelectedValue = "2";
                            //objBindDDL.FillItemGrp(ddlNewGroup);
                            //ddlNewGroup.SelectedValue = "3";
                            //objBindDDL.FillItemSubGrp(ddlNewSubGroup);
                            //ddlNewSubGroup.SelectedValue = "83";


                            if (Session["EditPONo"] != null && Convert.ToString(Session["EditPONo"]) != "" && Convert.ToString(Session["EditPONo"]) != string.Empty)
                            {
                                DataTable dtPOMST = new DataTable();
                                DataTable dtPODTL = new DataTable();
                                DataTable dtPOCOND = new DataTable();
                                DataTable dtPOCHG = new DataTable();
                                dtPOMST = objMainClass.SelectPOData(objMainClass.intCmpId, Convert.ToString(Session["EditPONo"]), 1);
                                dtPODTL = objMainClass.SelectPOData(objMainClass.intCmpId, Convert.ToString(Session["EditPONo"]), 2);
                                dtPOCOND = objMainClass.SelectPOData(objMainClass.intCmpId, Convert.ToString(Session["EditPONo"]), 3);
                                dtPOCHG = objMainClass.SelectPOData(objMainClass.intCmpId, Convert.ToString(Session["EditPONo"]), 4);
                                if (dtPOMST.Rows.Count > 0)
                                {
                                    ddlDoctype.SelectedItem.Text = Convert.ToString(dtPOMST.Rows[0]["POTYPE"]);
                                    txtPONO.Text = Convert.ToString(dtPOMST.Rows[0]["PONO"]);
                                    txtPODATE.Text = Convert.ToDateTime(dtPOMST.Rows[0]["PODT"]).ToShortDateString();
                                    txtVendor.Text = Convert.ToString(dtPOMST.Rows[0]["VENDCODE"]);
                                    txtVendorName.Text = Convert.ToString(dtPOMST.Rows[0]["VENDNAME"]);
                                    txtTransporter.Text = Convert.ToString(dtPOMST.Rows[0]["TRANCODE"]);
                                    txtTransporterName.Text = Convert.ToString(dtPOMST.Rows[0]["TRANNAME"]);
                                    ddlPaymentTerms.SelectedItem.Text = Convert.ToString(dtPOMST.Rows[0]["PMTTERMS"]);
                                    txtPaymentTermsDesc.Text = Convert.ToString(dtPOMST.Rows[0]["PMTTERMSDESC"]);
                                    txtREMARKS.Text = Convert.ToString(dtPOMST.Rows[0]["REMARK"]);
                                    txtMaterialValue.Text = Convert.ToString(dtPOMST.Rows[0]["NETMATVALUE"]);
                                    txtTaxAmount.Text = Convert.ToString(dtPOMST.Rows[0]["NETTAXAMT"]);
                                    txtDisacount.Text = Convert.ToString(dtPOMST.Rows[0]["DISCOUNT"]);
                                    txtOtherCharges.Text = Convert.ToString(dtPOMST.Rows[0]["OTHERCHARGES"]);
                                    txtNetAmount.Text = Convert.ToString(dtPOMST.Rows[0]["NETPOAMT"]);
                                    ddlDepartment.SelectedValue = Convert.ToString(dtPOMST.Rows[0]["DEPTID"]);

                                    txtPONO.ReadOnly = true;
                                    txtPODATE.ReadOnly = true;

                                    grvListItem.DataSource = dtPODTL;
                                    grvListItem.DataBind();
                                    ViewState["ItemData"] = dtPODTL;

                                    grvTaxation.DataSource = dtPOCOND;
                                    grvTaxation.DataBind();
                                    ViewState["TaxData"] = dtPOCOND;

                                    grvCharges.DataSource = dtPOCHG;
                                    grvCharges.DataBind();
                                    ViewState["ChargesData"] = dtPOCHG;


                                    Session["saveall"] = "Update All";
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Record not found. '" + Convert.ToString(Session["EditPRNo"]) + "'.');$('.close').click(function(){window.location.href ='ViewPR.aspx' });", true);
                                }
                            }
                            else if (Session["PRNO"] != null && Convert.ToString(Session["PRNO"]) != "" && Convert.ToString(Session["PRNO"]) != string.Empty)
                            {
                                if (Session["VENDCODE"] != null && Convert.ToString(Session["VENDCODE"]) != "" && Convert.ToString(Session["VENDCODE"]) != string.Empty)
                                {
                                    txtVendor.Text = Convert.ToString(Session["VENDCODE"]);
                                    txtVendor_TextChanged(1, e);

                                    txtTransporter.Text = Convert.ToString(Session["VENDCODE"]);
                                    txtTransporter_TextChanged(1, e);
                                }
                                ddlDoctype.SelectedValue = "MPO Material PO";
                                txtPRNo.Text = Convert.ToString(Session["PRNO"]);
                                txtPRNo_TextChanged(1, e);
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
                dtColumn.ColumnName = "PRNO";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ID";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "POID";
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
                dtColumn.ColumnName = "POQTY";
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
                dtColumn.ColumnName = "ASSETCODE";
                dtItem.Columns.Add(dtColumn);

                //dtColumn = new DataColumn();
                //dtColumn.ColumnName = "REQUISITIONER";
                //dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "TRACKNO";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMTEXT";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "PARTREQNO";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMREMARKS";
                dtItem.Columns.Add(dtColumn);

                //New Added Start

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "REFNO";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "IMEINO";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "FROMPLANTCD";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "FROMLOCCD";
                dtItem.Columns.Add(dtColumn);

                //New Added End

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "SKU";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "MAKE";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "MODEL";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "DISPNAME";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "DISPMRP";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "VALUELIMIT";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "MAXSTKQTY";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "HSN";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "HSNGROUP";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "HSNGROUPDESC";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "CONDTYPE";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMSTATUS";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ONHANDSTOCK";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "DEVREASON";
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
                dtTaxColumn.ColumnName = "POSRNO";
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
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "fun", "ShowLoading();", true);
                    //string func = "ShowLoading();";
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", func, true);



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

                                string PO = objMainClass.SaveMPO(objMainClass.intCmpId, ddlDoctype.SelectedItem.Text, txtPODATE.Text, txtVendor.Text, txtTransporter.Text, ddlPaymentTerms.SelectedValue, txtPaymentTermsDesc.Text, txtMaterialValue.Text, txtTaxAmount.Text, txtDisacount.Text, txtNetAmount.Text, txtREMARKS.Text, grvListItem, grvTaxation, grvCharges, Convert.ToString(Session["USERID"]), Convert.ToInt32(ddlDepartment.SelectedValue));
                                if (PO != "")
                                {


                                    String strCustContent = "";
                                    strCustContent = fileread();
                                    strCustContent = strCustContent.Replace("###Heading###", "New PO Created by User.");
                                    strCustContent = strCustContent.Replace("###CreateBy###", Convert.ToString(Session["USERNAME"]));
                                    strCustContent = strCustContent.Replace("###CreateDate###", txtPODATE.Text);
                                    strCustContent = strCustContent.Replace("###PRNO###", PO);
                                    strCustContent = strCustContent.Replace("###Message###", "New PO created by user. Details are as per above.");
                                    strCustContent = strCustContent.Replace("###LINK###", "http://14.98.132.190:360/MM/frmApprovePO.aspx");
                                    strCustContent = strCustContent.Replace("###HREFLINK###", "http://14.98.132.190:360/MM/frmApprovePO.aspx");


                                    GridViewRow row = grvListItem.Rows[0];
                                    string plcode = ((Label)row.FindControl("lblGVPlantID")).Text;
                                    DataTable dt = new DataTable();
                                    dt = objMainClass.MailSenderReceiver("PO", 1, Convert.ToInt32(ddlDepartment.SelectedValue), plcode, 12, Convert.ToDecimal(txtNetAmount.Text));
                                    string Reciever = string.Empty;
                                    if (dt.Rows.Count > 0)
                                    {

                                        for (int i = 0; i < dt.Rows.Count; i++)
                                        {

                                            if (Reciever == string.Empty)
                                            {
                                                Reciever = Convert.ToString(dt.Rows[i]["EMAILID"]);
                                            }
                                            else
                                            {
                                                Reciever = Reciever + ";" + Convert.ToString(dt.Rows[i]["EMAILID"]);
                                            }
                                        }
                                        objMainClass.SaveMailNotification(objMainClass.EmailID, objMainClass.Password, Convert.ToString(dt.Rows[0]["EMAILID"]), Reciever, "New PO Created", strCustContent, objMainClass.PORT, PO, Convert.ToString(Session["USERID"]), "MPO");


                                        //objMainClass.SendMail(strCustContent, "New PR Created", dt);
                                    }


                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Record save sucessfully.  PO No. : " + PO + "\");$('.close').click(function(){window.location.href ='ViewPO.aspx' });", true);
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
                    else if (Convert.ToString(Session["saveall"]) == "Update All")
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
                                string PO = objMainClass.UpdateMPO(objMainClass.intCmpId, ddlDoctype.SelectedItem.Text, txtPONO.Text, txtPODATE.Text, txtVendor.Text, txtTransporter.Text, ddlPaymentTerms.SelectedValue, txtPaymentTermsDesc.Text, txtMaterialValue.Text, txtTaxAmount.Text, txtDisacount.Text, txtNetAmount.Text, txtREMARKS.Text, grvListItem, grvTaxation, grvCharges, Convert.ToString(Session["USERID"]));
                                if (PO != "")
                                {

                                    String strCustContent = "";
                                    strCustContent = fileread();
                                    strCustContent = strCustContent.Replace("###Heading###", "PO Updated by User.");
                                    strCustContent = strCustContent.Replace("###CreateBy###", Convert.ToString(Session["USERNAME"]));
                                    strCustContent = strCustContent.Replace("###CreateDate###", txtPODATE.Text);
                                    strCustContent = strCustContent.Replace("###PRNO###", PO);
                                    strCustContent = strCustContent.Replace("###Message###", "PO Updated by user. Details are as per above.");
                                    strCustContent = strCustContent.Replace("###LINK###", "http://14.98.132.190:360/MM/frmApprovePO.aspx");
                                    strCustContent = strCustContent.Replace("###HREFLINK###", "http://14.98.132.190:360/MM/frmApprovePO.aspx");

                                    DataTable dt = new DataTable();
                                    dt = objMainClass.MailSenderReceiver("PO", 1, Convert.ToInt32(ddlDepartment.SelectedValue), null, 2, Convert.ToDecimal(txtNetAmount.Text));
                                    string Reciever = string.Empty;
                                    if (dt.Rows.Count > 0)
                                    {

                                        for (int i = 0; i < dt.Rows.Count; i++)
                                        {

                                            if (Reciever == string.Empty)
                                            {
                                                Reciever = Convert.ToString(dt.Rows[i]["EMAILID"]);
                                            }
                                            else
                                            {
                                                Reciever = Reciever + ";" + Convert.ToString(dt.Rows[i]["EMAILID"]);

                                            }
                                        }
                                        objMainClass.SaveMailNotification(objMainClass.EmailID, objMainClass.Password, Convert.ToString(dt.Rows[0]["EMAILID"]), Reciever, "PO Updated", strCustContent, objMainClass.PORT, PO, Convert.ToString(Session["USERID"]), "MPO");


                                        //objMainClass.SendMail(strCustContent, "New PR Created", dt);
                                    }

                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Record updated sucessfully.  PO No. : " + PO + "\");$('.close').click(function(){window.location.href ='ViewPO.aspx' });", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Record not updated!');", true);
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
                    Session["EditPONo"] = null;
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

        protected static string fileread()
        {
            string actualfol;
            actualfol = HttpContext.Current.Server.MapPath("~/PRMailBody.html");
            StreamReader sr = new StreamReader(actualfol);
            string strcontent = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            return strcontent;
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

            //if (j == "OK")
            //{
            //    string pricerate = price();

            //    if (pricerate == "OK")
            //    {
            //        j = "OK";
            //    }
            //    else
            //    {
            //        j = pricerate;
            //    }
            //}



            return j;
        }


        private int checkMaxStk()
        {
            int stk = 0;
            int newmax = 0;
            int newmin = 0;

            decimal maxstk = objMainClass.SP_GET_STOCK_FUNCTION(objMainClass.intCmpId, objMainClass.getFinYear(objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true)), txtItemCode.Text, objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true), ddlPLant.SelectedValue, ddlLocation.SelectedValue);

            DataTable dt = new DataTable();
            dt = objMainClass.GetMaxMinStk(objMainClass.intCmpId, Convert.ToInt32(txtItemId.Text), ddlPLant.SelectedValue, ddlLocation.SelectedValue, 1, "GETMAXMINSTK");
            if (dt.Rows.Count > 0)
            {
                newmax = Convert.ToInt32(dt.Rows[0]["MAXSTKQTY"]);
                newmin = Convert.ToInt32(dt.Rows[0]["MINSTKQTY"]);
            }

            if (maxstk > newmax)
            {
                stk = 0;
            }
            else
            {
                stk = 1;
            }

            return stk;
        }

        private int checkMaxStkbal()
        {
            int stk = 0;
            decimal maxstk = objMainClass.SP_GET_STOCK_FUNCTION(objMainClass.intCmpId, objMainClass.getFinYear(objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true)), txtItemCode.Text, objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true), ddlPLant.SelectedValue, ddlLocation.SelectedValue);
            stk = Convert.ToInt32(maxstk);
            int stk1 = 0;
            //int.TryParse(txtItemMaxStkQty.Text, out stk1);
            if (Convert.ToDecimal(maxstk) > Convert.ToDecimal(txtItemMaxStkQty.Text))
            {
                stk = 0;
            }
            else
            {
                stk = 1;
            }
            return stk;
        }

        public string price()
        {
            string strResponse = "Price Error.!";
            bool checkSuggestPrice = true;
            bool checkVendorPrice = true;
            bool checkMaxAVGPrice = true;
            decimal suggestedPrice = 0;
            decimal vendorPrice = 0;
            decimal minPrice = 0;
            decimal maxPrice = 0;

            if (txtItemHSNGroup.Text == "10" || txtItemHSNGroup.Text == "168")
            {
                DataTable dtSuggestedPrice = new DataTable();
                dtSuggestedPrice = objMainClass.SuggestedPrice(objMainClass.intCmpId, txtPRNo.Text, "GETSUGGESTEDPRICE");

                if (dtSuggestedPrice.Rows.Count > 0)
                {
                    suggestedPrice = Convert.ToDecimal(Convert.ToString(dtSuggestedPrice.Rows[0]["BASICPURRATE"]) == "" ? 0 : Convert.ToDecimal(dtSuggestedPrice.Rows[0]["BASICPURRATE"]));
                }

                DataTable dtPriceRange = new DataTable();
                dtPriceRange = objMainClass.POPriceRange(objMainClass.intCmpId, txtItemCode.Text, "RANGEPOPRICE");

                if (dtPriceRange.Rows.Count > 0)
                {
                    maxPrice = Convert.ToDecimal(Convert.ToString(dtPriceRange.Rows[0]["MAXAVGAMT"]) == "" ? 0 : Convert.ToDecimal(dtPriceRange.Rows[0]["MAXAVGAMT"]));
                    //maxPrice = Convert.ToDecimal(Convert.ToString(dtPriceRange.Rows[0]["MAXRATE"]) == "" ? 0 : Convert.ToDecimal(dtPriceRange.Rows[0]["MAXRATE"]));
                }

                DataTable dtVendorPrice = new DataTable();
                dtVendorPrice = objMainClass.SuggestedPrice(objMainClass.intCmpId, txtPRNo.Text, "GETVENDORPRICE");

                if (dtVendorPrice.Rows.Count > 0)
                {
                    vendorPrice = Convert.ToDecimal(Convert.ToString(dtVendorPrice.Rows[0]["VENDORPRICE"]) == "" ? 0 : Convert.ToDecimal(dtVendorPrice.Rows[0]["VENDORPRICE"]));
                }

                if (suggestedPrice > 0)
                {
                    if (Convert.ToDecimal(txtItemBRate.Text) <= suggestedPrice)
                    {
                        checkSuggestPrice = false;
                    }
                    else
                    {
                        checkSuggestPrice = true;
                    }
                }
                else
                {
                    checkSuggestPrice = false;
                }

                if (vendorPrice > 0)
                {
                    if (Convert.ToDecimal(txtItemBRate.Text) <= vendorPrice)
                    {
                        checkVendorPrice = false;
                    }
                    else
                    {
                        checkVendorPrice = true;
                    }
                }
                else
                {
                    checkVendorPrice = false;
                }
                //}
                //else
                //{
                //    checkPO = false;
                //}

                if (maxPrice > 0)
                {
                    if (Convert.ToDecimal(txtItemBRate.Text) <= maxPrice)
                    {
                        checkMaxAVGPrice = false;
                    }
                    else
                    {
                        checkMaxAVGPrice = true;
                    }
                }
                else
                {
                    checkMaxAVGPrice = false;
                }



                if (checkSuggestPrice == false && checkVendorPrice == false && checkMaxAVGPrice == false)
                {
                    strResponse = "OK";
                }
                else
                {
                    strResponse = "";
                    if (Convert.ToDecimal(txtItemBRate.Text) > suggestedPrice)
                    {
                        if (strResponse == "")
                        {
                            strResponse = "Price Error.! Entered price is higher than suggested price. Suggested price is " + suggestedPrice + ".";
                        }
                        else
                        {
                            strResponse = strResponse + "Entered price is higher than suggested price. Suggested price is " + suggestedPrice + ".";
                        }
                    }

                    if (Convert.ToDecimal(txtItemBRate.Text) > vendorPrice)
                    {
                        if (strResponse == "")
                        {
                            strResponse = "Price Error.! Entered price is higher than vendor price. Vendor price is " + vendorPrice + ".";
                        }
                        else
                        {
                            strResponse = strResponse + "Entered price is higher than vendor price. Vendor price is " + vendorPrice + ".";
                        }
                    }

                    if (maxPrice > 0)
                    {
                        if (Convert.ToDecimal(txtItemBRate.Text) > maxPrice)
                        {
                            if (strResponse == "")
                            {
                                strResponse = "Price Error.! Entered price is higher than maximum average price. Maximum average price is " + maxPrice + ".";
                            }
                            else
                            {
                                strResponse = strResponse + "Entered price is higher than maximum average price. Maximum average price is " + maxPrice + ".";
                            }
                        }

                    }

                }


            }
            else
            {
                strResponse = "OK";
            }
            return strResponse;
        }


        public string POLockprice()
        {
            string strResponse = "Price Error.!";
            bool checkMaxLockPrice = true;
            decimal MaxLockPrice = 0;

            if (txtItemHSNGroup.Text == "10" || txtItemHSNGroup.Text == "168")
            {
                DataTable dtPOLockPrice = new DataTable();
                dtPOLockPrice = objMainClass.GetLockPrice("", "", "", "", "", "", txtItemDesc.Text);

                if (dtPOLockPrice.Rows.Count > 0)
                {
                    MaxLockPrice = Convert.ToDecimal(Convert.ToString(dtPOLockPrice.Rows[0]["FinalApproveListingAmount"]) == "" ? 0 : Convert.ToDecimal(dtPOLockPrice.Rows[0]["FinalApproveListingAmount"]));
                }




                if (MaxLockPrice > 0)
                {
                    if (Convert.ToDecimal(txtItemBRate.Text) <= MaxLockPrice)
                    {
                        checkMaxLockPrice = false;
                    }
                    else
                    {
                        checkMaxLockPrice = true;
                    }
                }
                else
                {
                    checkMaxLockPrice = false;
                }





                if (checkMaxLockPrice == false)
                {
                    strResponse = "OK";
                }
                else
                {
                    strResponse = "";
                    if (Convert.ToDecimal(txtItemBRate.Text) > MaxLockPrice)
                    {
                        if (strResponse == "")
                        {
                            strResponse = "Price Error.! Entered price is higher than AVG PO/Blocked price. AVG PO/Blocked price is " + MaxLockPrice + ".";
                        }
                        else
                        {
                            strResponse = strResponse + "Entered price is higher than AVG PO/Blocked price. AVG PO/Blocked price is " + MaxLockPrice + ".";
                        }
                    }



                }


            }
            else
            {
                strResponse = "OK";
            }
            return strResponse;
        }
        protected void btnSaveDet_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    txtTrackNo.Text = objMainClass.strConvertZeroPadding(txtTrackNo.Text.Trim());
                    if (txtTrackNo.Text == "0000000000")
                    {
                        txtTrackNo.Text = "";

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid Job id.');", true);
                        txtTrackNo.Text = "";
                    }
                    else
                    {
                        string validation = validateData();
                        if (validation == "OK")
                        {

                            int chekmaxminstk = 0;
                            if (txtItemGroupId.Text == "9")
                            {
                                chekmaxminstk = checkMaxStk();
                            }
                            else
                            {
                                chekmaxminstk = checkMaxStkbal();
                            }


                            if (chekmaxminstk == 0)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Current stock is at its max, you cannot place order more than that!');", true);
                            }
                            else
                            {
                                //if (Convert.ToDecimal(txtOnhandStock.Text) >= Convert.ToDecimal(txtItemMaxStkQty.Text))
                                //{
                                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Current stock is at its max, you cannot place order more than that!');", true);
                                //}
                                //else
                                //{
                                DataTable dtListing = new DataTable();
                                dtListing = objMainClass.SuggestedPrice(objMainClass.intCmpId, txtPRNo.Text, "GETLISTINGID");
                                int ListingID = 0;
                                if (dtListing.Rows.Count > 0)
                                {
                                    ListingID = Convert.ToInt32(dtListing.Rows[0]["LISTINGID"]);
                                }
                                string pricevalidation = "OK";
                                if (ListingID > 0)
                                {
                                    pricevalidation = price();
                                    if (pricevalidation == "OK")
                                    {

                                    }
                                    else
                                    {
                                        pricevalidation = pricevalidation + "Approval required.!";
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + pricevalidation + "\");", true);
                                    }
                                }
                                else
                                {
                                    pricevalidation = POLockprice();
                                    if (pricevalidation == "OK")
                                    {

                                    }
                                    else
                                    {
                                        pricevalidation = pricevalidation + "Approval required.!";
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + pricevalidation + "\");", true);
                                    }
                                }


                                if (Convert.ToString(Session["savedet"]) == "Save Item")
                                {

                                    DataTable dt = (DataTable)ViewState["ItemData"];
                                    DataTable dtTaxation = (DataTable)ViewState["TaxData"];
                                    if (grvListItem.Rows.Count > 0)
                                    {
                                        DataRow lastRow = dt.Rows[dt.Rows.Count - 1];
                                        int id = Convert.ToInt32(lastRow["ID"]) + 1;

                                        dt.Rows.Add(txtPRNo.Text, txtPRSrNo.Text, id, txtItemCode.Text, txtItemDesc.Text, txtItemId.Text, txtItemGroup.Text, txtItemGroupId.Text, ddlUOM.SelectedItem.Text,
                                            ddlUOM.SelectedValue, txtItemQty.Text, txtRate.Text, txtItemBRate.Text, txtAmount.Text, txtDiscount.Text, txtDeliveryDate.Text, txtGLCode.Text, ddlCostCenter.SelectedValue, ddlPLant.SelectedItem.Text,
                                            ddlPLant.SelectedValue, ddlLocation.SelectedItem.Text, ddlLocation.SelectedValue, txtProfitCenter.Text, txtAssetCode.Text, txtTrackNo.Text, txtItemText.Text, txtTrackNo.Text,
                                            txtItemRemark.Text, txtRefNo.Text, txtIMEINo.Text, "", "", txtSku.Text, txtItemMake.Text, txtItemModel.Text, txtItemDispName.Text, txtItemDispMRP.Text, txtItemValueLimit.Text,
                                            txtItemMaxStkQty.Text, txtItemHSN.Text, txtItemHSNGroup.Text, txtItemHSNGroupDesc.Text, txtItemCondType.Text, txtItemStatus.Text, txtOnhandStock.Text, pricevalidation);


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

                                        dt.Rows.Add(txtPRNo.Text, txtPRSrNo.Text, "1", txtItemCode.Text, txtItemDesc.Text, txtItemId.Text, txtItemGroup.Text, txtItemGroupId.Text, ddlUOM.SelectedItem.Text,
                                           ddlUOM.SelectedValue, txtItemQty.Text, txtRate.Text, txtItemBRate.Text, txtAmount.Text, txtDiscount.Text, txtDeliveryDate.Text, txtGLCode.Text, ddlCostCenter.SelectedValue, ddlPLant.SelectedItem.Text,
                                           ddlPLant.SelectedValue, ddlLocation.SelectedItem.Text, ddlLocation.SelectedValue, txtProfitCenter.Text, txtAssetCode.Text, txtTrackNo.Text, txtItemText.Text, txtTrackNo.Text,
                                           txtItemRemark.Text, txtRefNo.Text, txtIMEINo.Text, "", "", txtSku.Text, txtItemMake.Text, txtItemModel.Text, txtItemDispName.Text, txtItemDispMRP.Text, txtItemValueLimit.Text,
                                           txtItemMaxStkQty.Text, txtItemHSN.Text, txtItemHSNGroup.Text, txtItemHSNGroupDesc.Text, txtItemCondType.Text, txtItemStatus.Text, txtOnhandStock.Text, pricevalidation);




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
                                    DataRow dr = ddt.Select("POID = '" + txtSRNo.Text + "'")[0];
                                    dr[0] = txtPRNo.Text;
                                    dr[1] = txtPRSrNo.Text;
                                    dr[2] = txtSRNo.Text;
                                    dr[3] = txtItemCode.Text;
                                    dr[4] = txtItemDesc.Text;
                                    dr[5] = txtItemId.Text;
                                    dr[6] = txtItemGroup.Text;
                                    dr[7] = txtItemGroupId.Text;
                                    dr[8] = ddlUOM.SelectedItem.Text;
                                    dr[9] = ddlUOM.SelectedValue;
                                    dr[10] = txtItemQty.Text;
                                    dr[11] = txtRate.Text;
                                    dr[12] = txtItemBRate.Text;
                                    dr[13] = txtAmount.Text;
                                    dr[14] = txtDiscount.Text;
                                    dr[15] = txtDeliveryDate.Text;
                                    dr[16] = txtGLCode.Text;
                                    dr[17] = ddlCostCenter.SelectedValue;// txtCostCenter.Text;
                                    dr[18] = ddlPLant.SelectedItem.Text;
                                    dr[19] = ddlPLant.SelectedValue;
                                    dr[20] = ddlLocation.SelectedItem.Text;
                                    dr[21] = ddlLocation.SelectedValue;
                                    dr[22] = txtProfitCenter.Text;
                                    dr[23] = txtAssetCode.Text;
                                    dr[24] = txtTrackNo.Text == "" ? "0" : txtTrackNo.Text;
                                    dr[25] = txtItemText.Text;
                                    dr[26] = txtTrackNo.Text == "" ? "0" : txtTrackNo.Text;
                                    dr[27] = txtItemRemark.Text;
                                    dr[28] = txtRefNo.Text;
                                    dr[29] = txtIMEINo.Text;
                                    dr[30] = "";
                                    dr[31] = "";
                                    dr[32] = txtSku.Text;
                                    dr[33] = txtItemMake.Text;
                                    dr[34] = txtItemModel.Text;
                                    dr[35] = txtItemDispName.Text;
                                    dr[36] = txtItemDispMRP.Text;
                                    dr[37] = txtItemValueLimit.Text;
                                    dr[38] = txtItemMaxStkQty.Text;
                                    dr[39] = txtItemHSN.Text;
                                    dr[40] = txtItemHSNGroup.Text;
                                    dr[41] = txtItemHSNGroupDesc.Text;
                                    dr[42] = txtItemCondType.Text;
                                    dr[43] = txtItemStatus.Text;
                                    dr[43] = txtOnhandStock.Text;
                                    dr[45] = pricevalidation;

                                    if (ddlConditionType.SelectedIndex > 0)
                                    {

                                        DataTable dtTax = (DataTable)ViewState["TaxData"];
                                        if (dtTax.Rows.Count > 0)
                                        {
                                            bool Isrecordavailable = IsRecordAvailableAtDataTable(Convert.ToInt32(txtSRNo.Text));
                                            if (Isrecordavailable)
                                            {
                                                DataRow drt = dtTax.Select("POSRNO='" + txtSRNo.Text + "'")[0];
                                                if (drt != null)
                                                {
                                                    drt[0] = ddlOperator.SelectedIndex > 0 ? ddlOperator.SelectedValue : "+";
                                                    drt[2] = txtSRNo.Text;
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
                                                dtTaxation.Rows.Add(ddlOperator.SelectedIndex > 0 ? ddlOperator.SelectedValue : "+", idTax, txtSRNo.Text, ddlConditionType.SelectedValue,
                                                hfRate.Value, txtAmount.Text, txtTaxTAmount.Text, hfPID.Value, hfCONDID.Value);
                                                ViewState["TaxData"] = dtTaxation;
                                            }
                                        }
                                        else
                                        {
                                            DataTable dtTaxation = (DataTable)ViewState["TaxData"];

                                            dtTaxation.Rows.Add(ddlOperator.SelectedIndex > 0 ? ddlOperator.SelectedValue : "+", "1", txtSRNo.Text, ddlConditionType.SelectedValue,
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

                                //txtMaterialValue.Text = Convert.ToString(Convert.ToDecimal(txtMaterialValue.Text) + Convert.ToDecimal(txtAmount.Text));
                                //txtTaxAmount.Text = Convert.ToString(Convert.ToDecimal(txtTaxAmount.Text) + Convert.ToDecimal(txtTaxTAmount.Text));
                                //txtDisacount.Text = Convert.ToString(Convert.ToDecimal(txtDisacount.Text) + Convert.ToDecimal(txtDiscount.Text));
                                ////txtOtherCharges.Text = Convert.ToString(Convert.ToDecimal(txtOtherCharges.Text) + Convert.ToDecimal(txtChgAmt.Text));
                                //txtNetAmount.Text = Convert.ToString(Convert.ToDecimal(txtMaterialValue.Text) + Convert.ToDecimal(txtTaxAmount.Text) - Convert.ToDecimal(txtDisacount.Text) + Convert.ToDecimal(txtOtherCharges.Text));

                                callAMT();

                                EmptyString();
                                //}

                            }



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

        protected void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            try
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
                        ddlUOM.SelectedValue = txtSku.Text;
                        txtItemGroupId.Text = Convert.ToString(dt.Rows[0]["itemgrpid"]);
                        txtItemMake.Text = Convert.ToString(dt.Rows[0]["make"]);
                        txtItemModel.Text = Convert.ToString(dt.Rows[0]["model"]);
                        txtItemDispName.Text = Convert.ToString(dt.Rows[0]["dispname"]);
                        txtItemDispMRP.Text = Convert.ToString(dt.Rows[0]["mrp"]);
                        txtItemValueLimit.Text = Convert.ToString(dt.Rows[0]["valuelimit"]);
                        txtItemMaxStkQty.Text = Convert.ToString(dt.Rows[0]["maxstkqty"]);
                        txtOnhandStock.Text = Convert.ToString(dt.Rows[0]["MAXSTOCKQTY"]);
                        txtItemHSN.Text = Convert.ToString(dt.Rows[0]["hsngrpcode"]);
                        txtItemHSNGroup.Text = Convert.ToString(dt.Rows[0]["hsngrp"]);
                        txtItemHSNGroupDesc.Text = Convert.ToString(dt.Rows[0]["hsngrpdesc"]);
                        txtItemCondType.Text = Convert.ToString(dt.Rows[0]["condtype"]);
                        txtItemStatus.Text = Convert.ToString(dt.Rows[0]["status"]);
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
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lnkOpenPoup_Click(object sender, EventArgs e)
        {
            objBindDDL.FillItemCat(ddlpopCategory);
            objBindDDL.FillItemGrp(ddlpopGroup);
            objBindDDL.FillItemSubGrp(ddlpopSubGroup);
            objBindDDL.FillBrand(ddlpopMake, 0);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-item').modal();", true);
        }

        protected void txtItemRate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //bool checkPO = true;
                //decimal suggestedPrice = 0;
                //decimal minPrice = 0;
                //decimal maxPrice = 0;

                //if (txtItemHSNGroup.Text == "10" || txtItemHSNGroup.Text == "168")
                //{
                //    DataTable dtSuggestedPrice = new DataTable();
                //    dtSuggestedPrice = objMainClass.SuggestedPrice(objMainClass.intCmpId, txtPRNo.Text, "GETSUGGESTEDPRICE");

                //    if (dtSuggestedPrice.Rows.Count > 0)
                //    {
                //        suggestedPrice = Convert.ToDecimal(Convert.ToString(dtSuggestedPrice.Rows[0]["BASICPURRATE"]) == "" ? 0 : Convert.ToDecimal(dtSuggestedPrice.Rows[0]["BASICPURRATE"]));
                //    }

                //    DataTable dtPriceRange = new DataTable();
                //    dtPriceRange = objMainClass.POPriceRange(objMainClass.intCmpId, txtItemCode.Text, "RANGEPOPRICE");

                //    if (dtPriceRange.Rows.Count > 0)
                //    {
                //        minPrice = Convert.ToDecimal(Convert.ToString(dtPriceRange.Rows[0]["MINRATE"]) == "" ? 0 : Convert.ToDecimal(dtPriceRange.Rows[0]["MINRATE"]));
                //        maxPrice = Convert.ToDecimal(Convert.ToString(dtPriceRange.Rows[0]["MAXRATE"]) == "" ? 0 : Convert.ToDecimal(dtPriceRange.Rows[0]["MAXRATE"]));
                //    }

                //    if (suggestedPrice > 0)
                //    {
                //        if (Convert.ToDecimal(txtItemBRate.Text) <= suggestedPrice)
                //        {
                //            checkPO = false;
                //        }
                //        else
                //        {
                //            checkPO = true;
                //        }
                //    }
                //    else
                //    {
                //        checkPO = false;
                //    }

                //    if (maxPrice > 0)
                //    {

                //        if (Convert.ToDecimal(txtItemBRate.Text) <= maxPrice)
                //        {
                //            checkPO = false;
                //        }
                //        else
                //        {
                //            checkPO = true;
                //        }
                //    }
                //    else
                //    {
                //        checkPO = false;
                //    }

                //}
                //else
                //{
                //    checkPO = false;
                //}

                //if (checkPO == false)
                //{

                string priceValidation = price();
                if (priceValidation == "OK")
                {

                }
                else
                {

                    priceValidation = priceValidation + "Approval required.!";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + priceValidation + "\");", true);
                }
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

                //}
                //else if (checkPO == true)
                //{
                //    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('PO is already made for same IMEI No.! PO No. : '" + Convert.ToString(dtSO.Rows[0]["LASTSO"]) + ");$('#txtIMEINo').focus();", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Item rate needs to be Less then or equal to Suggest Price. Suggeste price is :'" + suggestedPrice + " Min Price is : " + minPrice + "Max Price is : " + maxPrice + "); ", true);
                //}

                //txtAmount.Text = Convert.ToString(Convert.ToDecimal(txtItemBRate.Text == "" ? "0" : txtItemBRate.Text) * Convert.ToDecimal(txtItemQty.Text == "" ? "0" : txtItemQty.Text));

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
                if (ddlUOM.SelectedItem.Value.ToString() != txtSku.Text)
                {
                    ddlUOM.SelectedValue = txtSku.Text;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"UOM is not matched with Base Unit of Item!\");", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void txtItemQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(txtItemQty.Text) > Convert.ToDecimal(lblCheckPrQty.Text))
                {
                    txtItemQty.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('PO Quantity can not be higher than PR Quantity! ');", true);
                }
                else
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

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void txtDiscount_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ddlPaymentTerms_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlPaymentTerms.SelectedValue == "OTHR")
                {
                    txtPaymentTermsDesc.Text = string.Empty;
                    txtPaymentTermsDesc.Enabled = true;
                    txtPaymentTermsDesc.Focus();
                }
                else
                {
                    txtPaymentTermsDesc.Text = Convert.ToString(ddlPaymentTerms.SelectedItem.Text.Split('-')[1].Trim()); ;
                    txtPaymentTermsDesc.Enabled = false;
                    txtREMARKS.Focus();
                }

                //if (ddlPaymentTerms.SelectedValue == "ONS")
                //{
                //    ddlLocation.SelectedValue = "CM01";
                //    //ddlLocation.Enabled = false;
                //}
                //else
                //{
                //    ddlLocation.SelectedIndex = 1;
                //    //ddlLocation.Enabled = true;
                //}
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }

        }

        protected void lnkSaveTaxation_Click(object sender, EventArgs e)
        {

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

                            dt.Rows.Add(id, ddlCharges.SelectedItem.Text, txtChgAmt.Text);

                            ViewState["ChargesData"] = dt;
                        }
                        else
                        {
                            dt.Rows.Add("1", ddlCharges.SelectedItem.Text, txtChgAmt.Text);
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

                        dr[1] = ddlCharges.SelectedItem.Text;
                        dr[2] = txtChgAmt.Text;

                        grvCharges.DataSource = (DataTable)ViewState["ChargesData"];
                        grvCharges.DataBind();
                        Session["saveCharge"] = "Save Charge";

                    }

                    //txtOtherCharges.Text = Convert.ToString(Convert.ToDecimal(txtOtherCharges.Text) + Convert.ToDecimal(txtChgAmt.Text));
                    //txtNetAmount.Text = Convert.ToString(Convert.ToDecimal(txtMaterialValue.Text) + Convert.ToDecimal(txtTaxAmount.Text) - Convert.ToDecimal(txtDisacount.Text) + Convert.ToDecimal(txtOtherCharges.Text));

                    callAMT();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                }
                txtSrNoChg.Text = string.Empty;
                txtMaxSrNoChg.Text = string.Empty;
                txtChgAmt.Text = string.Empty;
            }
            catch (Exception ex)
            {
                txtSrNoChg.Text = string.Empty;
                txtMaxSrNoChg.Text = string.Empty;
                txtChgAmt.Text = string.Empty;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void grvTaxation_RowCommand(object sender, GridViewCommandEventArgs e)
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
                DataRow dr = ddt.Select("POID = '" + lblTaxPOSrNo.Text + "'")[0];
                dr[11] = dr[12];
                grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                grvListItem.DataBind();


                callAMT();
            }
            if (e.CommandName == "eEdit")
            {

            }
        }

        protected void grvListItem_RowCommand(object sender, GridViewCommandEventArgs e)
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
                dtTax.Select("POSRNO='" + index + "'").ToList().ForEach(x => x.Delete());
                dtTax.AcceptChanges();
                ViewState["TaxData"] = dtTax;
                grvTaxation.DataSource = (DataTable)ViewState["TaxData"];
                grvTaxation.DataBind();

                callAMT();

            }
            if (e.CommandName == "eEdit")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                txtSRNo.Text = Convert.ToString(index);
                GridViewRow gRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;


                Label lblGVPrNo = (Label)gRow.FindControl("lblGVPrNo");
                Label lblGVPRSrNo = (Label)gRow.FindControl("lblGVPRSrNo");
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
                Label lblGVAssetCode = (Label)gRow.FindControl("lblGVAssetCode");
                Label lblGVTrackNo = (Label)gRow.FindControl("lblGVTrackNo");
                Label lblGVItemText = (Label)gRow.FindControl("lblGVItemText");
                Label lblGVPartReqNo = (Label)gRow.FindControl("lblGVPartReqNo");
                Label lblGVRemarks = (Label)gRow.FindControl("lblGVRemarks");
                Label lblGVRefNo = (Label)gRow.FindControl("lblGVRefNo");
                Label lblGVIMEI = (Label)gRow.FindControl("lblGVIMEI");
                Label lblGVFromPlantCode = (Label)gRow.FindControl("lblGVFromPlantCode");
                Label lblGVFromLocationCode = (Label)gRow.FindControl("lblGVFromLocationCode");
                Label lblSKU = (Label)gRow.FindControl("lblSKU");
                Label lblMake = (Label)gRow.FindControl("lblMake");
                Label lblModel = (Label)gRow.FindControl("lblModel");
                Label lblDispName = (Label)gRow.FindControl("lblDispName");
                Label lblDispMRP = (Label)gRow.FindControl("lblDispMRP");
                Label lblValueLimit = (Label)gRow.FindControl("lblValueLimit");
                Label lblMaxStkQty = (Label)gRow.FindControl("lblMaxStkQty");
                Label lblHSN = (Label)gRow.FindControl("lblHSN");
                Label lblHSNGroup = (Label)gRow.FindControl("lblHSNGroup");
                Label lblHSNGroupDesc = (Label)gRow.FindControl("lblHSNGroupDesc");
                Label lblCondType = (Label)gRow.FindControl("lblCondType");
                Label lblItemStatus = (Label)gRow.FindControl("lblItemStatus");
                Label lblOnHandStock = (Label)gRow.FindControl("lblOnHandStock");
                Label lblGVDiscount = (Label)gRow.FindControl("lblGVDiscount");


                txtSRNo.Text = lblGVID.Text;
                txtPRNo.Text = lblGVPrNo.Text;
                txtPRSrNo.Text = lblGVPRSrNo.Text;
                txtItemCode.Text = lblGVItemCode.Text;
                txtItemDesc.Text = lblGVItemDesc.Text;
                txtItemId.Text = lblGVItemId.Text;
                txtSku.Text = lblSKU.Text;
                txtItemGroup.Text = lblGVItemGroup.Text;
                txtItemGroupId.Text = lblGVGroupId.Text;
                txtItemMake.Text = lblMake.Text;
                txtItemModel.Text = lblModel.Text;
                txtItemDispName.Text = lblDispName.Text;
                txtItemDispMRP.Text = lblDispMRP.Text;
                txtItemValueLimit.Text = lblValueLimit.Text;
                txtItemMaxStkQty.Text = lblMaxStkQty.Text;
                txtOnhandStock.Text = lblOnHandStock.Text;
                txtItemHSN.Text = lblHSN.Text;
                txtItemHSNGroup.Text = lblHSNGroup.Text;
                txtItemHSNGroupDesc.Text = lblHSNGroupDesc.Text;
                txtItemCondType.Text = lblCondType.Text;
                txtItemStatus.Text = lblItemStatus.Text;
                txtGLCode.Text = lblGVGLCode.Text;
                txtProfitCenter.Text = lblGVProfitCenter.Text;
                txtItemText.Text = lblGVItemText.Text;
                txtAssetCode.Text = lblGVAssetCode.Text;
                txtTrackNo.Text = lblGVTrackNo.Text;
                txtItemQty.Text = lblGVQty.Text;
                lblCheckPrQty.Text = lblGVQty.Text;
                ddlUOM.SelectedValue = lblGVUOMID.Text;
                txtRate.Text = lblGVRate.Text;
                txtItemBRate.Text = lblGVBaseRate.Text;
                txtAmount.Text = lblGVAmount.Text;
                txtDiscount.Text = lblGVDiscount.Text;
                txtDeliveryDate.Text = lblGVDeliDate.Text;
                txtRefNo.Text = lblGVRefNo.Text;
                txtIMEINo.Text = lblGVIMEI.Text;
                txtItemRemark.Text = lblGVRemarks.Text;
                ddlPLant.SelectedValue = lblGVPlantID.Text;
                objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);
                ddlLocation.SelectedValue = lblGVLocationCDID.Text;
                objBindDDL.FillCostCenter(ddlCostCenter, ddlPLant.SelectedValue, ddlLocation.SelectedValue);
                ddlCostCenter.SelectedValue = lblGVCostCenter.Text;


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

        public bool IsRecordAvailableAtDataTable(int index)
        {
            bool IsrecordAvailable = false;
            try
            {
                DataTable dtTax = (DataTable)ViewState["TaxData"];
                foreach (DataRow dr in dtTax.Rows)
                {
                    if (dr["POSRNO"].ToString() == index.ToString())
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

        protected void grvCharges_RowCommand(object sender, GridViewCommandEventArgs e)
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
                ddlCharges.SelectedItem.Text = lblChrgCondType.Text;
                txtChgAmt.Text = lblChrgAmount.Text;


                Session["saveCharge"] = "Update Charge";
            }

        }

        protected void txtVendor_TextChanged(object sender, EventArgs e)
        {
            if (txtVendor.Text.Length >= 5)
            {
                lblVendorError.Text = string.Empty;
                lblVendorError.Visible = false;
                DataTable dt = objMainClass.GetVendorDetails(objMainClass.intCmpId, txtVendor.Text, "");
                if (dt.Rows.Count > 0)
                {
                    lblVendorError.Text = string.Empty;
                    lblVendorError.Visible = false;
                    txtVendorName.Text = Convert.ToString(dt.Rows[0]["NAME1"]);
                    txtVendor.Text = Convert.ToString(dt.Rows[0]["VENDCODE"]);
                }
                else
                {
                    lblVendorError.Text = "Invalid Vendor Code. Please Enter Correct Vendor Code.";
                    lblVendorError.Visible = true;
                    txtVendor.Focus();
                    ScriptManager.GetCurrent(this.Page).SetFocus(this.txtVendor);
                }
            }
            else
            {
                lblVendorError.Text = "Minimum 5 digit req.";
                lblVendorError.Visible = true;
                txtVendor.Focus();
                ScriptManager.GetCurrent(this.Page).SetFocus(this.txtVendor);

            }
            txtTransporter.Focus();
        }

        protected void txtTransporter_TextChanged(object sender, EventArgs e)
        {
            if (txtTransporter.Text.Length >= 5)
            {
                lblTransporterError.Text = string.Empty;
                lblTransporterError.Visible = false;
                DataTable dt = objMainClass.GetVendorDetails(objMainClass.intCmpId, txtTransporter.Text, "");
                if (dt.Rows.Count > 0)
                {
                    lblTransporterError.Text = string.Empty;
                    lblTransporterError.Visible = false;
                    txtTransporterName.Text = Convert.ToString(dt.Rows[0]["NAME1"]);
                    txtTransporter.Text = Convert.ToString(dt.Rows[0]["VENDCODE"]);
                }
                else
                {
                    lblTransporterError.Text = "Invalid Transporter Code. Please Enter Correct Transporter Code.";
                    lblTransporterError.Visible = true;
                    txtTransporter.Focus();
                    ScriptManager.GetCurrent(this.Page).SetFocus(this.txtTransporter);
                }
            }
            else
            {
                lblTransporterError.Text = "Minimum 5 digit req.";
                lblTransporterError.Visible = true;
                txtTransporter.Focus();
            }
            ddlPaymentTerms.Focus();
        }

        protected void grvPopItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtItemCode.Text = Convert.ToString(grvPopItem.SelectedRow.Cells[1].Text);
            txtItemCode_TextChanged(1, e);
        }

        protected void ddlpopMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            objBindDDL.FillModel(ddlpopModel, ddlpopMake.SelectedValue);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-item').modal();", true);
        }

        protected void btnShowItem_Click(object sender, EventArgs e)
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

        protected void txtPRNo_TextChanged(object sender, EventArgs e)
        {
            if (txtPRNo.Text.Length > 0)
            {
                DataTable dtApproved = objMainClass.DOC_APRROVAL("PR", txtPRNo.Text, (int)APRVTYPE.APPROVED, 1);
                //if (dtApproved.Rows.Count > 0)
                //{
                if (chkFullPR.Checked == true)
                {
                    DataTable dt = objMainClass.SelectPRMST(txtPRNo.Text, objMainClass.intCmpId);
                    if (dt.Rows.Count > 0)
                    {
                        ddlDepartment.SelectedValue = Convert.ToString(dt.Rows[0]["DEPTID"]);
                        DataTable PRDetailsdt = objMainClass.SelectPRDetail(txtPRNo.Text, 1, 4, "");

                        if (PRDetailsdt.Rows.Count > 0)
                        {

                            if (grvListItem.Rows.Count > 0)
                            {

                            }
                            else
                            {
                                grvListItem.DataSource = PRDetailsdt;
                                grvListItem.DataBind();
                                ViewState["ItemData"] = PRDetailsdt;
                                callAMT();
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"Full Quantity are ordered for these PR.!\");", true);
                            txtPRNo.Text = string.Empty;
                            txtPRSrNo.Text = string.Empty;
                            txtPRNo.Focus();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"Invalid PR No.!\");", true);
                        txtPRNo.Text = string.Empty;
                        txtPRSrNo.Text = string.Empty;
                        txtPRNo.Focus();
                    }
                }
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"PR still not approved. Please approve PR.!\");", true);
                //    txtPRNo.Text = string.Empty;
                //    txtPRSrNo.Text = string.Empty;
                //    txtPRNo.Focus();
                //}
            }
        }

        protected void txtPRSrNo_TextChanged(object sender, EventArgs e)
        {
            double dblPOQty = 0;
            if (txtPRNo.Text.Length > 0)
            {
                if (txtPRSrNo.Text.Length > 0)
                {
                    DataTable PRDetailsdt = objMainClass.SelectPRDetail(txtPRNo.Text, 1, 5, txtPRSrNo.Text);
                    if (PRDetailsdt.Rows.Count > 0)
                    {
                        if (grvListItem.Rows.Count > 0)
                        {
                            for (int i = 0; i < grvListItem.Rows.Count; i++)
                            {
                                GridViewRow row = grvListItem.Rows[i];
                                Label lblGVPRSrNo = (Label)row.FindControl("lblGVPRSrNo");
                                Label lblGVPrNo = (Label)row.FindControl("lblGVPrNo");
                                Label lblGVID = (Label)row.FindControl("lblGVID");
                                Label lblGVQty = (Label)row.FindControl("lblGVQty");


                                if (lblGVPRSrNo.Text == txtPRSrNo.Text && lblGVPrNo.Text == objMainClass.strConvertZeroPadding(txtPRNo.Text) && txtSRNo.Text != lblGVID.Text)
                                {
                                    dblPOQty = Convert.ToDouble(Convert.ToInt32(dblPOQty) + Convert.ToInt32(lblGVQty.Text));
                                }
                            }


                            if (dblPOQty >= Convert.ToDouble(PRDetailsdt.Rows[0]["POQTY"]))
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"Full Quantity are ordered for these PR.!\");", true);
                            }
                            else
                            {
                                txtItemDesc.Text = Convert.ToString(PRDetailsdt.Rows[0]["ITEMDESC"]);
                                txtGLCode.Text = Convert.ToString(PRDetailsdt.Rows[0]["GLCODE"]);
                                txtItemGroup.Text = Convert.ToString(PRDetailsdt.Rows[0]["ITEMGROUP"]);
                                txtItemId.Text = Convert.ToString(PRDetailsdt.Rows[0]["ITEMID"]);
                                txtSku.Text = Convert.ToString(PRDetailsdt.Rows[0]["SKU"]);
                                txtItemGroupId.Text = Convert.ToString(PRDetailsdt.Rows[0]["ITEMGROUPID"]);
                                txtItemMake.Text = Convert.ToString(PRDetailsdt.Rows[0]["MAKE"]);
                                txtItemModel.Text = Convert.ToString(PRDetailsdt.Rows[0]["MODEL"]);
                                txtItemDispName.Text = Convert.ToString(PRDetailsdt.Rows[0]["DISPNAME"]);
                                txtItemDispMRP.Text = Convert.ToString(PRDetailsdt.Rows[0]["DISPNAME"]);
                                txtItemValueLimit.Text = Convert.ToString(PRDetailsdt.Rows[0]["VALUELIMIT"]);
                                txtItemMaxStkQty.Text = Convert.ToString(PRDetailsdt.Rows[0]["MAXSTKQTY"]);
                                txtItemHSN.Text = Convert.ToString(PRDetailsdt.Rows[0]["HSN"]);
                                txtItemHSNGroup.Text = Convert.ToString(PRDetailsdt.Rows[0]["HSNGROUP"]);
                                txtItemHSNGroupDesc.Text = Convert.ToString(PRDetailsdt.Rows[0]["HSNGROUPDESC"]);
                                txtItemCondType.Text = Convert.ToString(PRDetailsdt.Rows[0]["CONDTYPE"]);
                                txtItemStatus.Text = Convert.ToString(PRDetailsdt.Rows[0]["ITEMSTATUS"]);

                                txtItemCode.Text = Convert.ToString(PRDetailsdt.Rows[0]["ITEMCODE"]);
                                txtProfitCenter.Text = Convert.ToString(PRDetailsdt.Rows[0]["PROFITCENTER"]);
                                txtItemText.Text = Convert.ToString(PRDetailsdt.Rows[0]["ITEMTEXT"]);
                                txtItemQty.Text = Convert.ToString(PRDetailsdt.Rows[0]["POQTY"]);
                                lblCheckPrQty.Text = Convert.ToString(PRDetailsdt.Rows[0]["POQTY"]);
                                ddlUOM.SelectedValue = Convert.ToString(PRDetailsdt.Rows[0]["UOMID"]);
                                txtAmount.Text = Convert.ToString(PRDetailsdt.Rows[0]["ITEMAMOUNT"]);
                                txtTrackNo.Text = Convert.ToString(PRDetailsdt.Rows[0]["TRACKNO"]);
                                txtDeliveryDate.Text = Convert.ToString(PRDetailsdt.Rows[0]["DELIVERYDATE"]);
                                ddlPLant.SelectedValue = Convert.ToString(PRDetailsdt.Rows[0]["ITEMPLANTID"]);
                                ddlLocation.SelectedValue = Convert.ToString(PRDetailsdt.Rows[0]["LOCCDID"]);
                                objBindDDL.FillCostCenter(ddlCostCenter, ddlPLant.SelectedValue, ddlLocation.SelectedValue);
                                ddlCostCenter.SelectedValue = Convert.ToString(PRDetailsdt.Rows[0]["COSTCENTER"]);
                                txtAssetCode.Text = Convert.ToString(PRDetailsdt.Rows[0]["ASSETCODE"]);
                                txtRate.Text = Convert.ToString(PRDetailsdt.Rows[0]["ITEMRATE"]);
                                txtItemBRate.Text = Convert.ToString(PRDetailsdt.Rows[0]["ITEMRATE"]);
                            }
                        }
                        else
                        {
                            if (dblPOQty >= Convert.ToDouble(PRDetailsdt.Rows[0]["POQTY"]))
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"Full Quantity are ordered for these PR.!\");", true);
                            }
                            else
                            {
                                txtItemDesc.Text = Convert.ToString(PRDetailsdt.Rows[0]["ITEMDESC"]);
                                txtGLCode.Text = Convert.ToString(PRDetailsdt.Rows[0]["GLCODE"]);
                                txtItemGroup.Text = Convert.ToString(PRDetailsdt.Rows[0]["ITEMGROUP"]);
                                txtItemId.Text = Convert.ToString(PRDetailsdt.Rows[0]["ITEMID"]);
                                txtSku.Text = Convert.ToString(PRDetailsdt.Rows[0]["SKU"]);
                                txtItemGroupId.Text = Convert.ToString(PRDetailsdt.Rows[0]["ITEMGROUPID"]);
                                txtItemMake.Text = Convert.ToString(PRDetailsdt.Rows[0]["MAKE"]);
                                txtItemModel.Text = Convert.ToString(PRDetailsdt.Rows[0]["MODEL"]);
                                txtItemDispName.Text = Convert.ToString(PRDetailsdt.Rows[0]["DISPNAME"]);
                                txtItemDispMRP.Text = Convert.ToString(PRDetailsdt.Rows[0]["DISPMRP"]);
                                txtItemValueLimit.Text = Convert.ToString(PRDetailsdt.Rows[0]["VALUELIMIT"]);
                                txtItemMaxStkQty.Text = Convert.ToString(PRDetailsdt.Rows[0]["MAXSTKQTY"]);
                                txtItemHSN.Text = Convert.ToString(PRDetailsdt.Rows[0]["HSN"]);
                                txtItemHSNGroup.Text = Convert.ToString(PRDetailsdt.Rows[0]["HSNGROUP"]);
                                txtItemHSNGroupDesc.Text = Convert.ToString(PRDetailsdt.Rows[0]["HSNGROUPDESC"]);
                                txtItemCondType.Text = Convert.ToString(PRDetailsdt.Rows[0]["CONDTYPE"]);
                                txtItemStatus.Text = Convert.ToString(PRDetailsdt.Rows[0]["ITEMSTATUS"]);

                                txtItemCode.Text = Convert.ToString(PRDetailsdt.Rows[0]["ITEMCODE"]);
                                txtProfitCenter.Text = Convert.ToString(PRDetailsdt.Rows[0]["PROFITCENTER"]);
                                txtItemText.Text = Convert.ToString(PRDetailsdt.Rows[0]["ITEMTEXT"]);
                                txtItemQty.Text = Convert.ToString(PRDetailsdt.Rows[0]["POQTY"]);
                                lblCheckPrQty.Text = Convert.ToString(PRDetailsdt.Rows[0]["POQTY"]);
                                ddlUOM.SelectedValue = Convert.ToString(PRDetailsdt.Rows[0]["UOMID"]);
                                txtAmount.Text = Convert.ToString(PRDetailsdt.Rows[0]["ITEMAMOUNT"]);
                                txtTrackNo.Text = Convert.ToString(PRDetailsdt.Rows[0]["TRACKNO"]);
                                txtDeliveryDate.Text = Convert.ToString(PRDetailsdt.Rows[0]["DELIVERYDATE"]);
                                ddlPLant.SelectedValue = Convert.ToString(PRDetailsdt.Rows[0]["ITEMPLANTID"]);
                                ddlLocation.SelectedValue = Convert.ToString(PRDetailsdt.Rows[0]["LOCCDID"]);
                                objBindDDL.FillCostCenter(ddlCostCenter, ddlPLant.SelectedValue, ddlLocation.SelectedValue);
                                ddlCostCenter.SelectedValue = Convert.ToString(PRDetailsdt.Rows[0]["COSTCENTER"]) == string.Empty ? "1000" : Convert.ToString(PRDetailsdt.Rows[0]["COSTCENTER"]);
                                txtAssetCode.Text = Convert.ToString(PRDetailsdt.Rows[0]["ASSETCODE"]);
                                txtRate.Text = Convert.ToString(PRDetailsdt.Rows[0]["ITEMRATE"]);
                                txtItemBRate.Text = Convert.ToString(PRDetailsdt.Rows[0]["ITEMRATE"]);
                            }
                        }

                        DataTable dt = new DataTable();
                        dt = objMainClass.GetItemDetails(txtItemCode.Text, ddlPLant.SelectedValue, ddlLocation.SelectedValue);
                        txtOnhandStock.Text = Convert.ToString(dt.Rows[0]["MAXSTOCKQTY"]);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"Invalid PR Sr. No.!\");", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"Invalid PR No.!\");", true);
            }
        }

        protected void txtRefNo_TextChanged(object sender, EventArgs e)
        {
            if (txtRefNo.Text.Trim().Length > 0)
            {
                if (txtVendor.Text.Trim().Length > 0)
                {
                    //SP_SELECT_PO_REFNO_DATA
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetPORefData(txtRefNo.Text, objMainClass.strConvertZeroPadding(txtVendor.Text));
                    if (dt.Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"PO already made for these Bill No. ! PO No. is " + Convert.ToString(dt.Rows[0]["PONO"]) + ".!\");", true);
                        txtVendor.Focus();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"Please Enter Vendor Code.!\");", true);
                }
            }
        }

        protected void ddlConditionType_SelectedIndexChanged(object sender, EventArgs e)
        {
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
        }

        protected void txtTaxPOSrNo_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtBaseAmount_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ddlCharges_SelectedIndexChanged(object sender, EventArgs e)
        {

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

            txtMaterialValue.Text = Convert.ToString(materialvalue);
            txtTaxAmount.Text = Convert.ToString(taxvalue);
            txtDisacount.Text = Convert.ToString(discountvalue);
            txtOtherCharges.Text = Convert.ToString(chargesvalue);
            totalvalue = materialvalue + taxvalue - discountvalue + chargesvalue;
            txtNetAmount.Text = Convert.ToString(totalvalue);
        }



        private void EmptyString()
        {
            txtItemCode.Text = string.Empty;
            txtItemId.Text = string.Empty;
            txtSku.Text = string.Empty;
            txtItemGroup.Text = string.Empty;
            txtItemGroupId.Text = string.Empty;
            txtItemMake.Text = string.Empty;
            txtItemModel.Text = string.Empty;
            txtItemDispName.Text = string.Empty;
            txtItemDispMRP.Text = string.Empty;
            txtItemValueLimit.Text = string.Empty;
            txtItemMaxStkQty.Text = string.Empty;
            txtOnhandStock.Text = string.Empty;
            txtItemHSN.Text = string.Empty;
            txtItemHSNGroup.Text = string.Empty;
            txtItemHSNGroupDesc.Text = string.Empty;
            txtItemCondType.Text = string.Empty;
            txtItemStatus.Text = string.Empty;
            txtGLCode.Text = string.Empty;
            //txtProfitCenter.Text = string.Empty;
            txtItemText.Text = string.Empty;
            txtItemDesc.Text = string.Empty;
            txtItemQty.Text = string.Empty;
            //ddlUOM.SelectedValue = string.Empty;
            txtItemBRate.Text = string.Empty;
            txtAmount.Text = string.Empty;
            txtTrackNo.Text = string.Empty;
            txtDeliveryDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
            txtItemRemark.Text = string.Empty;
            //ddlPLant.SelectedValue = string.Empty;
            //ddlLocation.SelectedValue = string.Empty;
            //txtCostCenter.Text = string.Empty;
            //txtAssetCode.Text = string.Empty;
            ddlOperator.SelectedIndex = 0;
            ddlConditionType.SelectedIndex = 0;
            txtTaxTAmount.Text = string.Empty;
            hfRate.Value = string.Empty;
            hfPID.Value = string.Empty;
            hfCONDID.Value = string.Empty;
            txtPRSrNo.Text = string.Empty;
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
                            //txtCostCenter.Text = Convert.ToString(dt.Rows[0]["CSTCENTCD"]);
                        }
                        else
                        {
                            txtProfitCenter.Text = "1000";
                            //txtCostCenter.Text = "1000";
                        }
                    }
                    else
                    {
                        ddlPLant.SelectedValue = plantArray[0];
                        ddlPLant.Focus();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have plant rights.');$('#txtJobId').focus();", true);
                        ddlPLant.SelectedValue = plantArray[0];
                        ddlPLant.Focus();
                        objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);
                        ddlLocation.SelectedIndex = 1;
                    }

                    objBindDDL.FillCostCenter(ddlCostCenter, ddlPLant.SelectedValue, ddlLocation.SelectedValue);

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
                //if (ddlPaymentTerms.SelectedValue == "ONS" && ddlLocation.SelectedValue != "CM01")
                //{
                //    ddlLocation.SelectedValue = "CM01";
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You cannot select other location than CM01 for On Sales Payment Terms(Jangad).!');$('#txtJobId').focus();", true);
                //    ddlLocation.SelectedValue = "CM01";
                //}
                //else if (ddlLocation.SelectedValue == "CM01")
                //{
                //    ddlPaymentTerms.SelectedValue = "ONS";
                //}

                objBindDDL.FillCostCenter(ddlCostCenter, ddlPLant.SelectedValue, ddlLocation.SelectedValue);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lnkOpenVendorPopup_Click(object sender, EventArgs e)
        {

            objBindDDL.FillVendType(ddlPopupVendType);
            objBindDDL.FillCity(ddlPopupCity);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Vendor').modal();", true);
        }

        protected void grvPopVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtVendor.Text = Convert.ToString(grvPopVendor.SelectedRow.Cells[2].Text);
            txtVendor_TextChanged(1, e);
        }

        protected void lnkPopupVendorShow_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objMainClass.SelectVendor(objMainClass.intCmpId, txtPopupVendorCode.Text, txtPopupVendorName.Text, ddlPopupVendType.SelectedIndex > 0 ? ddlPopupVendType.SelectedValue : "", ddlPopupCity.SelectedIndex > 0 ? ddlPopupCity.SelectedValue : "");
                if (dt.Rows.Count > 0)
                {
                    grvPopVendor.DataSource = dt;
                    grvPopVendor.DataBind();
                }
                else
                {
                    grvPopVendor.DataSource = string.Empty;
                    grvPopVendor.DataBind();
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Vendor').modal();", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);

            }

        }

        protected void lnkPopupTransShow_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objMainClass.SelectVendor(objMainClass.intCmpId, txtPopupTransCode.Text, ddlPopupTransName.Text, ddlPopupTransType.SelectedIndex > 0 ? ddlPopupTransType.SelectedValue : "", ddlPopupTransCity.SelectedIndex > 0 ? ddlPopupTransCity.SelectedValue : "");
                if (dt.Rows.Count > 0)
                {
                    grvPopupTrans.DataSource = dt;
                    grvPopupTrans.DataBind();
                }
                else
                {
                    grvPopupTrans.DataSource = string.Empty;
                    grvPopupTrans.DataBind();
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Trans').modal();", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);

            }
        }

        protected void grvPopupTrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTransporter.Text = Convert.ToString(grvPopupTrans.SelectedRow.Cells[2].Text);
            txtTransporter_TextChanged(1, e);
        }

        protected void lnkOpenTransPopup_Click(object sender, EventArgs e)
        {
            objBindDDL.FillVendType(ddlPopupTransType);
            objBindDDL.FillCity(ddlPopupTransCity);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Trans').modal();", true);
        }

        protected void txtIMEINo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    bool blnPOExist = false;
                    btnSaveDet.Enabled = true;
                    DataTable dtPO = new DataTable();
                    DataTable dtSO = new DataTable();

                    //if (txtItemHSNGroup.Text == "10" || txtItemHSNGroup.Text == "168")
                    //{
                    if (txtIMEINo.Text.Length < 15)
                    {
                        txtIMEINo.Text = "";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('IMEI no should be 15 digit only.');", true);
                        txtIMEINo.Text = "";
                    }
                    else
                    {
                        int icount = 0;
                        for (int i = 0; i < grvListItem.Rows.Count; i++)
                        {
                            GridViewRow row = grvListItem.Rows[i];
                            Label lblGVIMEI = (Label)row.FindControl("lblGVIMEI");
                            Label lblGVGroupId = (Label)row.FindControl("lblGVGroupId");
                            if (lblGVGroupId.Text == "9")
                            {
                                if (lblGVIMEI.Text == txtIMEINo.Text)
                                {
                                    icount = icount + 1;
                                }
                            }
                        }

                        if (icount > 1)
                        {
                            btnSaveDet.Enabled = false;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('IMEI No. already entered for another item.!');$('#txtIMEINo').focus();", true);
                            btnSaveDet.Enabled = false;
                        }
                        else
                        {
                            btnSaveDet.Enabled = true;

                            if (ddlDoctype.SelectedItem.Value != "STO")
                            {

                                dtPO = objMainClass.CheckPOSOImeiNo(objMainClass.intCmpId, txtIMEINo.Text, "CHECKPOWITHRETURN");
                                if (dtPO.Rows.Count > 0)
                                {
                                    if (Convert.ToString(dtPO.Rows[0]["LASTPO"]) != "" && Convert.ToString(dtPO.Rows[0]["LASTPO"]) != string.Empty &&
                                        Convert.ToString(dtPO.Rows[0]["LASTPO"]) != null &&
                                        Convert.ToInt32(Convert.ToString(dtPO.Rows[0]["BALQTY"]) == "" ? 0 : Convert.ToInt32(dtPO.Rows[0]["BALQTY"])) > 0)
                                    {
                                        blnPOExist = true;


                                        dtSO = objMainClass.CheckPOSOImeiNo(objMainClass.intCmpId, txtIMEINo.Text, "CHECKSOWITHRETURN");

                                        if (dtSO.Rows.Count > 0)
                                        {
                                            if (Convert.ToString(dtSO.Rows[0]["LASTSO"]) != "" && Convert.ToString(dtSO.Rows[0]["LASTSO"]) != string.Empty &&
                                        Convert.ToString(dtSO.Rows[0]["LASTSO"]) != null &&
                                        Convert.ToInt32(Convert.ToString(dtSO.Rows[0]["BALQTY"]) == "" ? 0 : Convert.ToInt32(dtSO.Rows[0]["BALQTY"])) > 0)
                                            {
                                                blnPOExist = false;
                                            }

                                        }

                                    }
                                }


                            }


                            if (blnPOExist == true)
                            {
                                btnSaveDet.Enabled = false;
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('PO is already made for same IMEI No.! PO No. : '" + Convert.ToString(dtSO.Rows[0]["LASTSO"]) + ");$('#txtIMEINo').focus();", true);
                                btnSaveDet.Enabled = false;
                            }
                            else
                            {
                                btnSaveDet.Enabled = true;
                            }
                        }
                    }

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
        }

        protected void txtTrackNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    txtTrackNo.Text = objMainClass.strConvertZeroPadding(txtTrackNo.Text.Trim());
                    if (txtTrackNo.Text == "0000000000")
                    {
                        txtTrackNo.Text = "";

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid Job id.');", true);
                        txtTrackNo.Text = "";
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
    }
}
