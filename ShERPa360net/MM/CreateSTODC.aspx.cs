using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.MM
{
    public partial class CreateSTODC : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        DataTable dtItem = new DataTable();

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

                        if (FormRights.bAdd == false)
                        {
                            imgSaveAll.Enabled = false;
                        }

                        objBindDDL.FillPlant(ddlFromPLant);
                        ddlFromPLant.SelectedIndex = 1;
                        objBindDDL.FillLocationByPlantCd(ddlFromLocation, ddlFromPLant.SelectedValue);

                        objBindDDL.FillPlant(ddlToPlant);
                        ddlToPlant.SelectedIndex = 1;
                        objBindDDL.FillLocationByPlantCd(ddlToLocation, ddlToPlant.SelectedValue);

                        if (ddlFromPLant.SelectedValue == "1001")
                        {
                            ddlFromLocation.SelectedValue = "M001";
                        }
                        else
                        {
                            ddlFromLocation.SelectedValue = "MS01";
                        }
                        ddlToLocation.SelectedValue = "MS01";


                        objBindDDL.FillDocType(ddlDoctype, "STD");
                        ddlDoctype.SelectedIndex = 1;
                        objBindDDL.FillUOM(ddlUOM);
                        txtDocDt.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        //txtChalanDt.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtDocNo.Text = objMainClass.MAXPRNO(ddlDoctype.SelectedValue, "STD");

                        DataTable dt = objMainClass.GetPRFCost(ddlFromPLant.SelectedValue, Convert.ToString(Session["USERID"]));
                        txtProfitCenter.Text = Convert.ToString(dt.Rows[0]["PRFCNT"]);
                        //txtCostCenter.Text = Convert.ToString(dt.Rows[0]["CSTCENTCD"]);
                        objBindDDL.FillCostCenter(ddlCostCenter, ddlToPlant.SelectedValue, ddlToLocation.SelectedValue);
                        SetUpGrid();
                        Session["savedet"] = "Save Item";
                        Session["saveall"] = "Save All";

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
                dtColumn.ColumnName = "PONO";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "POSRNO";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "SRNO";
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
                dtColumn.ColumnName = "CHLNQTY";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMRATE";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMAMOUNT";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "GLCODE";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "COSTCENTER";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMFROMPLANTCD";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMFROMPLANTID";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMFROMLOCCD";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "FROMLOCCDID";
                dtItem.Columns.Add(dtColumn);


                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMTOPLANTCD";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMTOPLANTID";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMTOLOCCD";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "TOLOCCDID";
                dtItem.Columns.Add(dtColumn);


                dtColumn = new DataColumn();
                dtColumn.ColumnName = "PROFITCENTER";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ASSETCODE";
                dtItem.Columns.Add(dtColumn);



                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMTEXT";
                dtItem.Columns.Add(dtColumn);



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
                dtColumn.ColumnName = "HFPOQTY";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "HFPNDQTY";
                dtItem.Columns.Add(dtColumn);

                ViewState["ItemData"] = dtItem;

                grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                grvListItem.DataBind();










            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }



        protected void imgSaveAll_Click(object sender, EventArgs e)
        {
            //InsertSTODCData

            try
            {
                if (Session["USERID"] != null)
                {
                    if (Convert.ToString(Session["saveall"]) == "Save All")
                    {
                        if (grvListItem.Rows.Count > 0)
                        {
                            //string PLantRights = string.Empty;
                            //string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                            //for (int i = 0; i < plantArray.Count(); i++)
                            //{
                            //    if (plantArray[i].Trim() == ((Label)grvListItem.Rows[0].FindControl("lblGVPlantID")).Text)
                            //    {
                            //        PLantRights = ((Label)grvListItem.Rows[0].FindControl("lblGVPlantID")).Text;
                            //    }
                            //}

                            string validation = validateData();
                            if (validation == "OK")
                            {

                                string STO = objMainClass.InsertSTODCData(objMainClass.intCmpId, ddlDoctype.SelectedItem.Text, "STD", txtDocNo.Text, txtDocDt.Text, txtRefno.Text, txtTranCode.Text,
                                    txtPONO.Text, "", txtRemark.Text, Convert.ToString(Session["USERID"]), "", txtDocDt.Text, "", "", txtDocketNo.Text, txtNoOfBoxes.Text, grvListItem);
                                if (STO != "")
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Record save sucessfully.  STO No. : " + STO + "\");$('.close').click(function(){window.location.href ='ViewMaterialIssue.aspx?REQUESTFORM=STO' });", true);
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



        protected void txtPONO_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPONO.Text.Length > 0)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetPoByPOType(objMainClass.intCmpId, txtPONO.Text, "STO");
                    if (dt.Rows.Count > 0)
                    {
                        txtPONO.Text = objMainClass.strConvertZeroPadding(txtPONO.Text);
                        txtTranCode.Text = Convert.ToString(dt.Rows[0]["TRANCODE"]);


                        ddlFromPLant.Enabled = false;

                        txtTranCode_TextChanged(1, e);
                        txtPONO.Enabled = false;
                    }
                    else
                    {
                        txtPONO.Focus();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid PO No.!');", true);
                        txtPONO.Focus();
                    }
                }
                else
                {
                    txtPONO.Focus();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid PO No.!');", true);
                    txtPONO.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void txtTranCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtTranCode.Text.Length > 0)
                {
                    //GetVendorDetails
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetVendorDetails(objMainClass.intCmpId, txtTranCode.Text, "");
                    if (dt.Rows.Count > 0)
                    {
                        txtTransporterName.Text = Convert.ToString(dt.Rows[0]["NAME1"]);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid Transporter Code!');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid Transporter Code!');", true);
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

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtQty.Text != null && txtQty.Text != "" && txtQty.Text != string.Empty)
                {
                    if (Convert.ToDecimal(txtQty.Text) > 0)
                    {
                        DataTable dt = new DataTable();
                        dt = objMainClass.GetPOSTODtl(objMainClass.intCmpId, txtPONO.Text, txtPOSrNo.Text);
                        if (dt.Rows.Count > 0)
                        {
                            hfPOQty.Value = Convert.ToString(dt.Rows[0]["POQTY"]);
                            hfPndQty.Value = Convert.ToString(dt.Rows[0]["PNDQTY"]);

                            DataTable dtItem = new DataTable();
                            dtItem = objMainClass.GetItemDetails(Convert.ToString(dt.Rows[0]["ITEMCODE"]), ddlFromPLant.SelectedValue, ddlFromLocation.SelectedValue);


                            double RCVQty = 0;
                            if (grvListItem.Rows.Count > 0)
                            {
                                for (int i = 0; i < grvListItem.Rows.Count; i++)
                                {
                                    GridViewRow row = grvListItem.Rows[i];
                                    //Label lblGVPOSrNo = (Label)row.FindControl("lblGVPRSrNo");
                                    //Label lblGVPONo = (Label)row.FindControl("lblGVPrNo");
                                    //Label lblGVID = (Label)row.FindControl("lblGVID");
                                    //Label lblGVQty = (Label)row.FindControl("lblGVQty");


                                    Label lblGVPOSrNo = (Label)row.FindControl("lblGVPoSrNo");
                                    Label lblGVPONo = (Label)row.FindControl("lblGVPONO");
                                    Label lblGVID = (Label)row.FindControl("lblGVSrNo");
                                    Label lblGVQty = (Label)row.FindControl("lblGVPOQty");


                                    if (lblGVPOSrNo.Text == txtPOSrNo.Text && lblGVPONo.Text == objMainClass.strConvertZeroPadding(txtPONO.Text) && txtSrNo.Text != lblGVID.Text)
                                    {
                                        RCVQty = Convert.ToDouble(Convert.ToInt32(RCVQty) + Convert.ToInt32(lblGVQty.Text));
                                    }
                                }

                                if (RCVQty >= Convert.ToDouble(hfPOQty.Value))
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"Full Quantity is issued for these STO!\");", true);
                                }
                                else
                                {
                                    if (dtItem.Rows.Count > 0)
                                    {
                                        if (Convert.ToString(dtItem.Rows[0]["status"]) == "1")
                                        {
                                            txtItemDesc.Text = Convert.ToString(dtItem.Rows[0]["itemdesc"]);
                                            txtGLCode.Text = Convert.ToString(dtItem.Rows[0]["glcode"]);
                                            txtItemGroup.Text = Convert.ToString(dtItem.Rows[0]["itemgrp"]);
                                            txtItemId.Text = Convert.ToString(dtItem.Rows[0]["itemid"]);
                                            txtSku.Text = Convert.ToString(dtItem.Rows[0]["sku"]);
                                            txtItemGroupId.Text = Convert.ToString(dtItem.Rows[0]["itemgrpid"]);
                                            txtItemMake.Text = Convert.ToString(dtItem.Rows[0]["make"]);
                                            txtItemModel.Text = Convert.ToString(dtItem.Rows[0]["model"]);
                                            txtItemDispName.Text = Convert.ToString(dtItem.Rows[0]["dispname"]);
                                            txtItemDispMRP.Text = Convert.ToString(dtItem.Rows[0]["mrp"]);
                                            txtItemValueLimit.Text = Convert.ToString(dtItem.Rows[0]["valuelimit"]);
                                            txtItemMaxStkQty.Text = Convert.ToString(dtItem.Rows[0]["maxstkqty"]);
                                            txtItemHSN.Text = Convert.ToString(dtItem.Rows[0]["hsngrpcode"]);
                                            txtItemHSNGroup.Text = Convert.ToString(dtItem.Rows[0]["hsngrp"]);
                                            txtItemHSNGroupDesc.Text = Convert.ToString(dtItem.Rows[0]["hsngrpdesc"]);
                                            txtItemCondType.Text = Convert.ToString(dtItem.Rows[0]["condtype"]);
                                            txtItemStatus.Text = Convert.ToString(dtItem.Rows[0]["status"]);

                                            txtItemCode.Text = Convert.ToString(dt.Rows[0]["ITEMCODE"]);
                                            txtRate.Text = Convert.ToString(dt.Rows[0]["RATE"]);
                                            //if (txtQty.Text == "0" || txtQty.Text == string.Empty)
                                            //{
                                            //    txtQty.Text = hfPOQty.Value;
                                            //}

                                            //txtQty.Text = hfPndQty.Value;

                                            ddlToPlant.SelectedValue = Convert.ToString(dt.Rows[0]["PLANTCD"]);
                                            ddlToLocation.SelectedValue = Convert.ToString(dt.Rows[0]["LOCCD"]);
                                            ddlToPlant.Enabled = false;
                                            ddlToLocation.Enabled = false;



                                            txtAmount.Text = Convert.ToString(Convert.ToDecimal(txtRate.Text) * Convert.ToDecimal(txtQty.Text));
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
                            }
                            else
                            {
                                if (RCVQty >= Convert.ToDouble(hfPndQty.Value))
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"Full Quantity is issued for these STO!\");", true);
                                }
                                else
                                {
                                    if (dtItem.Rows.Count > 0)
                                    {
                                        if (Convert.ToString(dtItem.Rows[0]["status"]) == "1")
                                        {
                                            txtItemDesc.Text = Convert.ToString(dtItem.Rows[0]["itemdesc"]);
                                            txtGLCode.Text = Convert.ToString(dtItem.Rows[0]["glcode"]);
                                            txtItemGroup.Text = Convert.ToString(dtItem.Rows[0]["itemgrp"]);
                                            txtItemId.Text = Convert.ToString(dtItem.Rows[0]["itemid"]);
                                            txtSku.Text = Convert.ToString(dtItem.Rows[0]["sku"]);
                                            txtItemGroupId.Text = Convert.ToString(dtItem.Rows[0]["itemgrpid"]);
                                            txtItemMake.Text = Convert.ToString(dtItem.Rows[0]["make"]);
                                            txtItemModel.Text = Convert.ToString(dtItem.Rows[0]["model"]);
                                            txtItemDispName.Text = Convert.ToString(dtItem.Rows[0]["dispname"]);
                                            txtItemDispMRP.Text = Convert.ToString(dtItem.Rows[0]["mrp"]);
                                            txtItemValueLimit.Text = Convert.ToString(dtItem.Rows[0]["valuelimit"]);
                                            txtItemMaxStkQty.Text = Convert.ToString(dtItem.Rows[0]["maxstkqty"]);
                                            txtItemHSN.Text = Convert.ToString(dtItem.Rows[0]["hsngrpcode"]);
                                            txtItemHSNGroup.Text = Convert.ToString(dtItem.Rows[0]["hsngrp"]);
                                            txtItemHSNGroupDesc.Text = Convert.ToString(dtItem.Rows[0]["hsngrpdesc"]);
                                            txtItemCondType.Text = Convert.ToString(dtItem.Rows[0]["condtype"]);
                                            txtItemStatus.Text = Convert.ToString(dtItem.Rows[0]["status"]);

                                            txtItemCode.Text = Convert.ToString(dt.Rows[0]["ITEMCODE"]);
                                            txtRate.Text = Convert.ToString(dt.Rows[0]["RATE"]);
                                            //if (txtQty.Text == "0" || txtQty.Text == string.Empty)
                                            //{
                                            //    txtQty.Text = hfPOQty.Value;
                                            //}

                                            //txtQty.Text = hfPndQty.Value;

                                            ddlToPlant.SelectedValue = Convert.ToString(dt.Rows[0]["PLANTCD"]);
                                            ddlToLocation.SelectedValue = Convert.ToString(dt.Rows[0]["LOCCD"]);
                                            ddlToPlant.Enabled = false;
                                            ddlToLocation.Enabled = false;

                                            txtAmount.Text = Convert.ToString(Convert.ToDecimal(txtRate.Text) * Convert.ToDecimal(txtQty.Text));
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
                            }
                            if (Convert.ToDouble(txtQty.Text) > Convert.ToDouble(hfPOQty.Value))
                            {
                                btnSaveDet.Visible = false;
                                txtQty.Focus();
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"Quantity can't be higer than Ordered quantity!\");", true);
                                txtQty.Focus();
                            }
                            else
                            {
                                if (Convert.ToDouble(txtQty.Text) > Convert.ToDouble(hfPndQty.Value))
                                {
                                    btnSaveDet.Visible = false;
                                    txtQty.Focus();
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"Quantity can't be higer than Ordered quantity!\");", true);
                                    txtQty.Focus();
                                }
                                else
                                {
                                    btnSaveDet.Visible = true;
                                }
                            }


                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Enter Invalid PO Sr. No.!');", true);
                        }
                    }
                    else
                    {
                        txtQty.Focus();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Quantity can not be 0!');", true);
                        txtQty.Focus();
                    }
                }
                else
                {
                    txtQty.Focus();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Quantity can not be Null!');", true);
                    txtQty.Focus();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void txtPOSrNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //GetPOSTODtl
                if (txtPONO.Text.Length > 9)
                {
                    if (txtPOSrNo.Text.Length > 0)
                    {
                        DataTable dt = new DataTable();
                        dt = objMainClass.GetPOSTODtl(objMainClass.intCmpId, txtPONO.Text, txtPOSrNo.Text);
                        if (dt.Rows.Count > 0)
                        {
                            hfPOQty.Value = Convert.ToString(dt.Rows[0]["POQTY"]);
                            hfPndQty.Value = Convert.ToString(dt.Rows[0]["PNDQTY"]);
                            hfChlQty.Value = Convert.ToString(dt.Rows[0]["PNDQTY"]);

                            DataTable dtItem = new DataTable();
                            dtItem = objMainClass.GetItemDetails(Convert.ToString(dt.Rows[0]["ITEMCODE"]), ddlFromPLant.SelectedValue, ddlFromLocation.SelectedValue);


                            double RCVQty = 0;
                            if (grvListItem.Rows.Count > 0)
                            {
                                for (int i = 0; i < grvListItem.Rows.Count; i++)
                                {
                                    GridViewRow row = grvListItem.Rows[i];
                                    Label lblGVPOSrNo = (Label)row.FindControl("lblGVPoSrNo");
                                    Label lblGVPONo = (Label)row.FindControl("lblGVPONO");
                                    Label lblGVID = (Label)row.FindControl("lblGVSrNo");
                                    Label lblGVQty = (Label)row.FindControl("lblGVPOQty");


                                    if (lblGVPOSrNo.Text == txtPOSrNo.Text && lblGVPONo.Text == objMainClass.strConvertZeroPadding(txtPONO.Text) && txtSrNo.Text != lblGVID.Text)
                                    {
                                        RCVQty = Convert.ToDouble(Convert.ToInt32(RCVQty) + Convert.ToInt32(lblGVQty.Text));
                                    }
                                }

                                if (RCVQty >= Convert.ToDouble(hfPOQty.Value))
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"Full Quantity is issued for these STO!\");", true);
                                }
                                else
                                {
                                    if (dtItem.Rows.Count > 0)
                                    {
                                        if (Convert.ToString(dtItem.Rows[0]["status"]) == "1")
                                        {
                                            txtItemDesc.Text = Convert.ToString(dtItem.Rows[0]["itemdesc"]);
                                            txtGLCode.Text = Convert.ToString(dtItem.Rows[0]["glcode"]);
                                            txtItemGroup.Text = Convert.ToString(dtItem.Rows[0]["itemgrp"]);
                                            txtItemId.Text = Convert.ToString(dtItem.Rows[0]["itemid"]);
                                            txtSku.Text = Convert.ToString(dtItem.Rows[0]["sku"]);
                                            txtItemGroupId.Text = Convert.ToString(dtItem.Rows[0]["itemgrpid"]);
                                            txtItemMake.Text = Convert.ToString(dtItem.Rows[0]["make"]);
                                            txtItemModel.Text = Convert.ToString(dtItem.Rows[0]["model"]);
                                            txtItemDispName.Text = Convert.ToString(dtItem.Rows[0]["dispname"]);
                                            txtItemDispMRP.Text = Convert.ToString(dtItem.Rows[0]["mrp"]);
                                            txtItemValueLimit.Text = Convert.ToString(dtItem.Rows[0]["valuelimit"]);
                                            txtItemMaxStkQty.Text = Convert.ToString(dtItem.Rows[0]["maxstkqty"]);
                                            txtItemHSN.Text = Convert.ToString(dtItem.Rows[0]["hsngrpcode"]);
                                            txtItemHSNGroup.Text = Convert.ToString(dtItem.Rows[0]["hsngrp"]);
                                            txtItemHSNGroupDesc.Text = Convert.ToString(dtItem.Rows[0]["hsngrpdesc"]);
                                            txtItemCondType.Text = Convert.ToString(dtItem.Rows[0]["condtype"]);
                                            txtItemStatus.Text = Convert.ToString(dtItem.Rows[0]["status"]);


                                            txtItemCode.Text = Convert.ToString(dt.Rows[0]["ITEMCODE"]);
                                            txtRate.Text = Convert.ToString(dt.Rows[0]["RATE"]);
                                            //if (txtQty.Text == "0" || txtQty.Text == string.Empty)
                                            //{
                                            //    txtQty.Text = hfPOQty.Value;
                                            //}

                                            txtQty.Text = hfPndQty.Value;

                                            ddlToPlant.SelectedValue = Convert.ToString(dt.Rows[0]["PLANTCD"]);
                                            ddlToLocation.SelectedValue = Convert.ToString(dt.Rows[0]["LOCCD"]);
                                            ddlToPlant.Enabled = false;
                                            ddlToLocation.Enabled = false;


                                            txtAmount.Text = Convert.ToString(Convert.ToDecimal(txtRate.Text) * Convert.ToDecimal(txtQty.Text));


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
                            }
                            else
                            {
                                if (RCVQty >= Convert.ToDouble(hfPndQty.Value))
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"Full Quantity is issued for these STO!\");", true);
                                }
                                else
                                {
                                    if (dtItem.Rows.Count > 0)
                                    {
                                        if (Convert.ToString(dtItem.Rows[0]["status"]) == "1")
                                        {
                                            txtItemDesc.Text = Convert.ToString(dtItem.Rows[0]["itemdesc"]);
                                            txtGLCode.Text = Convert.ToString(dtItem.Rows[0]["glcode"]);
                                            txtItemGroup.Text = Convert.ToString(dtItem.Rows[0]["itemgrp"]);
                                            txtItemId.Text = Convert.ToString(dtItem.Rows[0]["itemid"]);
                                            txtSku.Text = Convert.ToString(dtItem.Rows[0]["sku"]);
                                            txtItemGroupId.Text = Convert.ToString(dtItem.Rows[0]["itemgrpid"]);
                                            txtItemMake.Text = Convert.ToString(dtItem.Rows[0]["make"]);
                                            txtItemModel.Text = Convert.ToString(dtItem.Rows[0]["model"]);
                                            txtItemDispName.Text = Convert.ToString(dtItem.Rows[0]["dispname"]);
                                            txtItemDispMRP.Text = Convert.ToString(dtItem.Rows[0]["mrp"]);
                                            txtItemValueLimit.Text = Convert.ToString(dtItem.Rows[0]["valuelimit"]);
                                            txtItemMaxStkQty.Text = Convert.ToString(dtItem.Rows[0]["maxstkqty"]);
                                            txtItemHSN.Text = Convert.ToString(dtItem.Rows[0]["hsngrpcode"]);
                                            txtItemHSNGroup.Text = Convert.ToString(dtItem.Rows[0]["hsngrp"]);
                                            txtItemHSNGroupDesc.Text = Convert.ToString(dtItem.Rows[0]["hsngrpdesc"]);
                                            txtItemCondType.Text = Convert.ToString(dtItem.Rows[0]["condtype"]);
                                            txtItemStatus.Text = Convert.ToString(dtItem.Rows[0]["status"]);


                                            txtItemCode.Text = Convert.ToString(dt.Rows[0]["ITEMCODE"]);
                                            txtRate.Text = Convert.ToString(dt.Rows[0]["RATE"]);
                                            //if (txtQty.Text == "0" || txtQty.Text == string.Empty)
                                            //{
                                            txtQty.Text = hfPndQty.Value;
                                            //}

                                            ddlToPlant.SelectedValue = Convert.ToString(dt.Rows[0]["PLANTCD"]);
                                            ddlToLocation.SelectedValue = Convert.ToString(dt.Rows[0]["LOCCD"]);
                                            ddlToPlant.Enabled = false;
                                            ddlToLocation.Enabled = false;


                                            txtAmount.Text = Convert.ToString(Convert.ToDecimal(txtRate.Text) * Convert.ToDecimal(txtQty.Text));


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
                            }
                            if (Convert.ToDouble(txtQty.Text) > Convert.ToDouble(hfPOQty.Value))
                            {
                                btnSaveDet.Visible = false;
                                txtQty.Focus();
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"Quantity can't be higer than Ordered quantity!\");", true);
                                txtQty.Focus();
                            }
                            else
                            {
                                btnSaveDet.Visible = true;
                            }


                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Enter Invalid PO Sr. No.!');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Enter Invalid PO Sr. No.!');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Enter Invalid PO No.!');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void ddlFromPLant_SelectedIndexChanged(object sender, EventArgs e)
        {
            objBindDDL.FillLocationByPlantCd(ddlFromLocation, ddlFromPLant.SelectedValue);

            try
            {
                if (Session["USERID"] != null)
                {
                    string PLantRights = string.Empty;
                    string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                    for (int i = 0; i < plantArray.Count(); i++)
                    {
                        if (plantArray[i].Trim() == ddlFromPLant.SelectedValue)
                        {
                            PLantRights = ddlFromPLant.SelectedValue;
                        }
                    }

                    if (PLantRights.Length > 0)
                    {
                        DataTable dt = objMainClass.GetPRFCost(ddlFromPLant.SelectedValue, Convert.ToString(Session["USERID"]));
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
                        ddlFromPLant.SelectedValue = plantArray[0];
                        ddlFromPLant.Focus();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have plant rights.');$('#txtJobId').focus();", true);
                        ddlFromPLant.SelectedValue = plantArray[0];
                        ddlFromPLant.Focus();
                        objBindDDL.FillLocationByPlantCd(ddlFromLocation, ddlFromPLant.SelectedValue);
                        ddlFromLocation.SelectedIndex = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }

        }

        protected void ddlToPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            objBindDDL.FillLocationByPlantCd(ddlToLocation, ddlToPlant.SelectedValue);

            try
            {
                if (Session["USERID"] != null)
                {
                    string PLantRights = string.Empty;
                    string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                    for (int i = 0; i < plantArray.Count(); i++)
                    {
                        if (plantArray[i].Trim() == ddlToPlant.SelectedValue)
                        {
                            PLantRights = ddlToPlant.SelectedValue;
                        }
                    }

                    if (PLantRights.Length > 0)
                    {
                        DataTable dt = objMainClass.GetPRFCost(ddlToPlant.SelectedValue, Convert.ToString(Session["USERID"]));
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
                        ddlToPlant.SelectedValue = plantArray[0];
                        ddlToPlant.Focus();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have plant rights.');$('#txtJobId').focus();", true);
                        ddlToPlant.SelectedValue = plantArray[0];
                        ddlToPlant.Focus();
                        objBindDDL.FillLocationByPlantCd(ddlToLocation, ddlToPlant.SelectedValue);
                        ddlToLocation.SelectedIndex = 1;
                    }

                    objBindDDL.FillCostCenter(ddlCostCenter, ddlToPlant.SelectedValue, ddlToLocation.SelectedValue);

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
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
            }
            if (e.CommandName == "eEdit")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                txtSrNo.Text = Convert.ToString(index);
                GridViewRow gRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;


                txtPOSrNo.Text = ((Label)gRow.FindControl("lblGVPoSrNo")).Text;
                txtItemCode.Text = ((Label)gRow.FindControl("lblGVItemCode")).Text;
                txtItemId.Text = ((Label)gRow.FindControl("lblGVItemID")).Text;
                txtSku.Text = ((Label)gRow.FindControl("lblSKU")).Text;
                txtItemGroup.Text = ((Label)gRow.FindControl("lblGVItemGroup")).Text;
                txtItemGroupId.Text = ((Label)gRow.FindControl("lblItemGroupID")).Text;
                txtItemMake.Text = ((Label)gRow.FindControl("lblMake")).Text;
                txtItemModel.Text = ((Label)gRow.FindControl("lblModel")).Text;
                txtItemDispName.Text = ((Label)gRow.FindControl("lblDispName")).Text;
                txtItemDispMRP.Text = ((Label)gRow.FindControl("lblDispMRP")).Text;
                txtItemValueLimit.Text = ((Label)gRow.FindControl("lblValueLimit")).Text;
                txtItemMaxStkQty.Text = ((Label)gRow.FindControl("lblMaxStkQty")).Text;
                txtItemHSN.Text = ((Label)gRow.FindControl("lblHSN")).Text;
                txtItemHSNGroup.Text = ((Label)gRow.FindControl("lblHSNGroup")).Text;
                txtItemHSNGroupDesc.Text = ((Label)gRow.FindControl("lblHSNGroupDesc")).Text;
                txtItemCondType.Text = ((Label)gRow.FindControl("lblCondType")).Text;
                txtItemStatus.Text = ((Label)gRow.FindControl("lblItemStatus")).Text;
                txtGLCode.Text = ((Label)gRow.FindControl("lblGVGLCODE")).Text;
                txtProfitCenter.Text = ((Label)gRow.FindControl("lblGVProfitCenter")).Text;
                //txtItemText.Text = ((Label)gRow.FindControl("lblGVItemText")).Text;
                txtAssetCode.Text = ((Label)gRow.FindControl("lblGVAssetcode")).Text;
                txtRate.Text = ((Label)gRow.FindControl("lblGVItemRate")).Text;
                txtAmount.Text = ((Label)gRow.FindControl("lblGVItemAmount")).Text;
                txtItemDesc.Text = ((Label)gRow.FindControl("lblGVItemDesc")).Text;
                txtQty.Text = ((Label)gRow.FindControl("lblGVPOQty")).Text;
                hfPOQty.Value = ((Label)gRow.FindControl("lblHFPOQty")).Text;
                hfPndQty.Value = ((Label)gRow.FindControl("lblHFPndQty")).Text;
                hfChlQty.Value = ((Label)gRow.FindControl("lblGVChlnQty")).Text;
                ddlUOM.SelectedValue = ((Label)gRow.FindControl("lblGVUOMID")).Text;
                ddlFromPLant.SelectedValue = ((Label)gRow.FindControl("lblGVFromPlantID")).Text;
                ddlFromLocation.SelectedValue = ((Label)gRow.FindControl("lblGVFromLocationID")).Text;
                ddlToPlant.SelectedValue = ((Label)gRow.FindControl("lblGVToPlantID")).Text;
                ddlToLocation.SelectedValue = ((Label)gRow.FindControl("lblGVToLocationID")).Text;
                objBindDDL.FillCostCenter(ddlCostCenter, ddlToPlant.SelectedValue, ddlToLocation.SelectedValue);
                ddlCostCenter.SelectedValue = ((Label)gRow.FindControl("lblGVCostCenter")).Text;
                txtItemReamrk.Text = ((Label)gRow.FindControl("lblGVItemText")).Text;


                Session["savedet"] = "Update Item";

            }

        }

        protected void btnSaveDet_Click(object sender, EventArgs e)
        {

            try
            {
                if (Session["USERID"] != null)
                {

                    decimal bal = objMainClass.SP_GET_STOCK_FUNCTION(objMainClass.intCmpId, objMainClass.getFinYear(objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true)), txtItemCode.Text, objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true), ddlFromPLant.SelectedValue, ddlFromLocation.SelectedValue);
                    if (bal > 0)
                    {
                        string validation = validateData();
                        if (validation == "OK")
                        {

                            if (Convert.ToString(Session["savedet"]) == "Save Item")
                            {

                                DataTable dt = (DataTable)ViewState["ItemData"];
                                if (grvListItem.Rows.Count > 0)
                                {
                                    DataRow lastRow = dt.Rows[dt.Rows.Count - 1];
                                    int id = Convert.ToInt32(lastRow["SRNO"]) + 1;

                                    dt.Rows.Add(txtPONO.Text, txtPOSrNo.Text, id, txtItemCode.Text, txtItemDesc.Text, txtItemId.Text, txtItemGroup.Text, txtItemGroupId.Text, ddlUOM.SelectedItem.Text,
                                        ddlUOM.SelectedValue, txtQty.Text, hfChlQty.Value, txtRate.Text, txtAmount.Text, txtGLCode.Text, ddlCostCenter.SelectedValue, ddlFromPLant.SelectedItem.Text, ddlFromPLant.SelectedValue,
                                        ddlFromLocation.SelectedItem.Text, ddlFromLocation.SelectedValue, ddlToPlant.SelectedItem.Text, ddlToPlant.SelectedValue, ddlToLocation.SelectedItem.Text, ddlToLocation.SelectedValue,
                                        txtProfitCenter.Text, txtAssetCode.Text, txtItemReamrk.Text, txtSku.Text, txtItemMake.Text, txtItemModel.Text, txtItemDispName.Text, txtItemDispMRP.Text, txtItemValueLimit.Text,
                                        txtItemMaxStkQty.Text, txtItemHSN.Text, txtItemHSNGroup.Text, txtItemHSNGroupDesc.Text, txtItemCondType.Text, txtItemStatus.Text, hfPOQty.Value, hfPndQty.Value);


                                    ViewState["ItemData"] = dt;
                                }
                                else
                                {
                                    dt.Rows.Add(txtPONO.Text, txtPOSrNo.Text, "1", txtItemCode.Text, txtItemDesc.Text, txtItemId.Text, txtItemGroup.Text, txtItemGroupId.Text, ddlUOM.SelectedItem.Text,
                                        ddlUOM.SelectedValue, txtQty.Text, hfChlQty.Value, txtRate.Text, txtAmount.Text, txtGLCode.Text, ddlCostCenter.SelectedValue, ddlFromPLant.SelectedItem.Text, ddlFromPLant.SelectedValue,
                                        ddlFromLocation.SelectedItem.Text, ddlFromLocation.SelectedValue, ddlToPlant.SelectedItem.Text, ddlToPlant.SelectedValue, ddlToLocation.SelectedItem.Text, ddlToLocation.SelectedValue,
                                        txtProfitCenter.Text, txtAssetCode.Text, txtItemReamrk.Text, txtSku.Text, txtItemMake.Text, txtItemModel.Text, txtItemDispName.Text, txtItemDispMRP.Text, txtItemValueLimit.Text,
                                        txtItemMaxStkQty.Text, txtItemHSN.Text, txtItemHSNGroup.Text, txtItemHSNGroupDesc.Text, txtItemCondType.Text, txtItemStatus.Text, hfPOQty.Value, hfPndQty.Value);
                                }

                                grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                                grvListItem.DataBind();

                            }
                            else if (Convert.ToString(Session["savedet"]) == "Update Item")
                            {


                                DataTable ddt = (DataTable)ViewState["ItemData"];
                                DataRow dr = ddt.Select("SRNO = '" + txtSrNo.Text + "'")[0];

                                dr[0] = txtPONO.Text;
                                dr[1] = txtPOSrNo.Text;
                                dr[2] = txtSrNo.Text;
                                dr[3] = txtItemCode.Text;
                                dr[4] = txtItemDesc.Text;
                                dr[4] = txtItemId.Text;
                                dr[6] = txtItemGroup.Text;
                                dr[7] = txtItemGroupId.Text;
                                dr[8] = ddlUOM.SelectedItem.Text;
                                dr[9] = ddlUOM.SelectedValue;
                                dr[10] = txtQty.Text;
                                dr[11] = hfChlQty.Value;
                                dr[12] = txtRate.Text;
                                dr[13] = txtAmount.Text;
                                dr[14] = txtGLCode.Text;
                                dr[15] = ddlCostCenter.SelectedValue;//txtCostCenter.Text;
                                dr[16] = ddlFromPLant.SelectedItem.Text;
                                dr[17] = ddlFromPLant.SelectedValue;
                                dr[18] = ddlFromLocation.SelectedItem.Text;
                                dr[19] = ddlFromLocation.SelectedValue;
                                dr[20] = ddlToPlant.SelectedItem.Text;
                                dr[21] = ddlToPlant.SelectedValue;
                                dr[22] = ddlToLocation.SelectedItem.Text;
                                dr[23] = ddlToLocation.SelectedValue;
                                dr[24] = txtProfitCenter.Text;
                                dr[25] = txtAssetCode.Text;
                                dr[26] = txtItemReamrk.Text;
                                dr[27] = txtSku.Text;
                                dr[28] = txtItemMake.Text;
                                dr[29] = txtItemModel.Text;
                                dr[30] = txtItemDispName.Text;
                                dr[31] = txtItemDispMRP.Text;
                                dr[32] = txtItemValueLimit.Text;
                                dr[33] = txtItemMaxStkQty.Text;
                                dr[34] = txtItemHSN.Text;
                                dr[35] = txtItemHSNGroup.Text;
                                dr[36] = txtItemHSNGroupDesc.Text;
                                dr[37] = txtItemCondType.Text;
                                dr[38] = txtItemStatus.Text;
                                dr[39] = hfPOQty.Value;
                                dr[40] = hfPndQty.Value;


                                grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                                grvListItem.DataBind();

                                Session["savedet"] = "Save Item";
                            }
                            EmptyString();


                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + validation + "\");", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('There is a deficit of Quantity by " + Convert.ToDecimal(Convert.ToDecimal(txtQty.Text) - Convert.ToDecimal(bal)) + "');", true);
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
                if (plantArray[i].Trim() == ddlFromPLant.SelectedValue)
                {
                    PLantRights = ddlFromPLant.SelectedValue;
                }
            }
            if (PLantRights.Length > 0)
            {
                string ToPLantRights = string.Empty;

                for (int k = 0; k < plantArray.Count(); k++)
                {
                    if (plantArray[k].Trim() == ddlToPlant.SelectedValue)
                    {
                        ToPLantRights = ddlToPlant.SelectedValue;
                    }
                }
                if (ToPLantRights.Length > 0)
                {
                    j = "OK";
                }
                else
                {
                    j = "You do not have plant rights. ";
                }



            }
            else
            {
                j = "You do not have plant rights. ";
            }
            return j;
        }


        private void EmptyString()
        {
            txtSrNo.Text = string.Empty;
            txtPOSrNo.Text = string.Empty;
            txtItemCode.Text = string.Empty;
            txtItemDesc.Text = string.Empty;
            txtItemId.Text = string.Empty;
            txtItemGroup.Text = string.Empty;
            txtItemGroupId.Text = string.Empty;
            txtQty.Text = string.Empty;
            hfChlQty.Value = string.Empty;
            txtRate.Text = string.Empty;
            txtAmount.Text = string.Empty;
            txtGLCode.Text = string.Empty;
            //txtCostCenter.Text = string.Empty;
            txtProfitCenter.Text = string.Empty;
            txtAssetCode.Text = string.Empty;
            txtItemReamrk.Text = string.Empty;
            txtSku.Text = string.Empty;
            txtItemMake.Text = string.Empty;
            txtItemModel.Text = string.Empty;
            txtItemDispName.Text = string.Empty;
            txtItemDispMRP.Text = string.Empty;
            txtItemValueLimit.Text = string.Empty;
            txtItemMaxStkQty.Text = string.Empty;
            txtItemHSN.Text = string.Empty;
            txtItemHSNGroup.Text = string.Empty;
            txtItemHSNGroupDesc.Text = string.Empty;
            txtItemCondType.Text = string.Empty;
            txtItemStatus.Text = string.Empty;
        }

        protected void ddlToLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                objBindDDL.FillCostCenter(ddlCostCenter, ddlToPlant.SelectedValue, ddlToLocation.SelectedValue);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}
