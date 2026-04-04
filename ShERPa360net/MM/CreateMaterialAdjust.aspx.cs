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
    public partial class CreateMaterialAdjust : System.Web.UI.Page
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
                            //Session.Abandon();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;


                        }

                        if (FormRights.bAdd == false)
                        {
                            imgSaveAll.Enabled = false;
                        }

                        objBindDDL.FillDocType(ddlDoctype, "IA");
                        ddlDoctype.SelectedIndex = 1;
                        ddlDoctype.Enabled = false;

                        txtDocDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();

                        objBindDDL.FillPlant(ddlPLant);
                        string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                        ddlPLant.SelectedValue = plantArray[0];
                        objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);
                        ddlLocation.SelectedIndex = 1;
                        objBindDDL.FillUOM(ddlUOM);

                        txtDocNo.Text = objMainClass.MAXPRNO(ddlDoctype.SelectedValue, "IA");

                        objBindDDL.FillItemCat(ddlpopCategory);
                        objBindDDL.FillItemGrp(ddlpopGroup);
                        objBindDDL.FillItemSubGrp(ddlpopSubGroup);
                        objBindDDL.FillBrand(ddlpopMake, 0);

                        DataTable dt = objMainClass.GetPRFCost(ddlPLant.SelectedValue, Convert.ToString(Session["USERID"]));
                        txtProfitCenter.Text = Convert.ToString(dt.Rows[0]["PRFCNT"]);
                        // txtCostCenter.Text = Convert.ToString(dt.Rows[0]["CSTCENTCD"]);

                        objBindDDL.FillCostCenter(ddlCostCenter, ddlPLant.SelectedValue, ddlLocation.SelectedValue);

                        Session["savedet"] = "Save Item";
                        SetUpGrid();

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
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "UOMID";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMQTY";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "CRDR";
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

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMTEXT";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMREMARKS";
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
                dtColumn.ColumnName = "ONHANDSTOCK";
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
            //InsertMaterialAdjust
            try
            {
                if (Session["USERID"] != null)
                {
                    if (grvListItem.Rows.Count > 0)
                    {

                        string PLantRights = string.Empty;
                        string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                        for (int i = 0; i < plantArray.Count(); i++)
                        {
                            if (plantArray[i].Trim() == ((Label)grvListItem.Rows[0].FindControl("lblPlantID")).Text)
                            {
                                PLantRights = ((Label)grvListItem.Rows[0].FindControl("lblPlantID")).Text;
                            }
                        }

                        if (PLantRights.Length > 0)
                        {
                            string docno = objMainClass.InsertMaterialAdjust(objMainClass.intCmpId, ddlDoctype.SelectedItem.Text, "IA", txtDocNo.Text, txtDocDate.Text,
                                txtRefno.Text, txtREMARKS.Text, Convert.ToString(Session["USERID"]), grvListItem);

                            if (docno != "")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Record save sucessfully. Doc No. : " + docno + "\");$('.close').click(function(){window.location.href ='CreateMaterialAdjust.aspx' });", true);
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

                    string validation = validateData();
                    if (validation == "OK")
                    {

                        txtItemQty_TextChanged(1, e);


                        string CRDR;
                        if (Convert.ToString(Session["savedet"]) == "Save Item")
                        {
                            if (txtItemQty.Text.Contains("-"))
                            {
                                string[] qty = txtItemQty.Text.ToString().Split('-');
                                txtItemQty.Text = Convert.ToString(qty[1]);
                                CRDR = "D";
                            }
                            else
                            {
                                CRDR = "C";
                            }
                            DataTable dt = (DataTable)ViewState["ItemData"];
                            if (grvListItem.Rows.Count > 0)
                            {
                                DataRow lastRow = dt.Rows[dt.Rows.Count - 1];
                                int id = Convert.ToInt32(lastRow["ID"]) + 1;
                                dt.Rows.Add(id, txtItemCode.Text, txtItemDesc.Text, txtItemId.Text, txtItemGroup.Text, txtItemGroupId.Text, ddlUOM.SelectedItem.Text,
                                    ddlUOM.SelectedValue, txtItemQty.Text, CRDR, txtGLCode.Text, ddlCostCenter.SelectedValue,
                                    ddlPLant.SelectedItem.Text, ddlPLant.SelectedValue, ddlLocation.SelectedItem.Text, ddlLocation.SelectedValue, txtProfitCenter.Text,
                                    txtAssetCode.Text, txtItemText.Text, txtItemRemarks.Text, txtSku.Text,
                                    txtItemMake.Text, txtItemModel.Text, txtItemDispName.Text, txtItemDispMRP.Text, txtItemValueLimit.Text, txtItemMaxStkQty.Text,
                                    txtItemHSN.Text, txtItemHSNGroup.Text, txtItemHSNGroupDesc.Text, txtItemCondType.Text, txtItemStatus.Text, txtOnHandStock.Text);

                                ViewState["ItemData"] = dt;

                            }
                            else
                            {
                                dt.Rows.Add("1", txtItemCode.Text, txtItemDesc.Text, txtItemId.Text, txtItemGroup.Text, txtItemGroupId.Text, ddlUOM.SelectedItem.Text,
                                    ddlUOM.SelectedValue, txtItemQty.Text, CRDR, txtGLCode.Text, ddlCostCenter.SelectedValue,
                                    ddlPLant.SelectedItem.Text, ddlPLant.SelectedValue, ddlLocation.SelectedItem.Text, ddlLocation.SelectedValue, txtProfitCenter.Text,
                                    txtAssetCode.Text, txtItemText.Text, txtItemRemarks.Text, txtSku.Text,
                                    txtItemMake.Text, txtItemModel.Text, txtItemDispName.Text, txtItemDispMRP.Text, txtItemValueLimit.Text, txtItemMaxStkQty.Text,
                                    txtItemHSN.Text, txtItemHSNGroup.Text, txtItemHSNGroupDesc.Text, txtItemCondType.Text, txtItemStatus.Text, txtOnHandStock.Text);

                                ViewState["ItemData"] = dt;


                            }

                            grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                            grvListItem.DataBind();
                            EmptyString();





                        }
                        else if (Convert.ToString(Session["savedet"]) == "Update Item")
                        {
                            if (txtItemQty.Text.Contains("-"))
                            {
                                txtItemQty.Text = txtItemQty.Text.Remove('-');
                                CRDR = "D";
                            }
                            else
                            {
                                CRDR = "C";
                            }

                            DataTable dt = (DataTable)ViewState["ItemData"];
                            //DataRow dr = new DataRow();
                            DataTable ddt = (DataTable)ViewState["ItemData"];
                            DataRow dr = ddt.Select("ID = '" + lblSRNO.Text + "'")[0];
                            dr[1] = txtItemCode.Text;
                            dr[2] = txtItemDesc.Text;
                            dr[3] = txtItemId.Text;
                            dr[4] = txtItemGroup.Text;
                            dr[5] = txtItemGroupId.Text;
                            dr[6] = ddlUOM.SelectedItem.Text;
                            dr[7] = ddlUOM.SelectedValue;
                            dr[8] = txtItemQty.Text;
                            dr[9] = CRDR;
                            dr[10] = txtGLCode.Text;
                            dr[11] = ddlCostCenter.SelectedValue; // txtCostCenter.Text;
                            dr[12] = ddlPLant.SelectedItem.Text;
                            dr[13] = ddlPLant.SelectedValue;
                            dr[14] = ddlLocation.SelectedItem.Text;
                            dr[15] = ddlLocation.SelectedValue;
                            dr[16] = txtProfitCenter.Text;
                            dr[17] = txtAssetCode.Text;
                            dr[18] = txtItemText.Text;
                            dr[19] = txtItemRemarks.Text;
                            dr[20] = txtSku.Text;
                            dr[21] = txtItemMake.Text;
                            dr[22] = txtItemModel.Text;
                            dr[23] = txtItemDispName.Text;
                            dr[24] = txtItemDispMRP.Text;
                            dr[25] = txtItemValueLimit.Text;
                            dr[26] = txtItemMaxStkQty.Text;
                            dr[27] = txtItemHSN.Text;
                            dr[28] = txtItemHSNGroup.Text;
                            dr[29] = txtItemHSNGroupDesc.Text;
                            dr[30] = txtItemCondType.Text;
                            dr[31] = txtItemStatus.Text;
                            dr[32] = txtOnHandStock.Text;
                            grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                            grvListItem.DataBind();
                            Session["savedet"] = "Save Item";
                            EmptyString();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + validation + "\");", true);
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

        protected void grvPopItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtItemCode.Text = Convert.ToString(grvPopItem.SelectedRow.Cells[1].Text);
            txtItemCode_TextChanged(1, e);
        }

        protected void ddlUOM_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlUOM.SelectedItem.Value.ToString() == txtSku.Text)
                {

                }
                else
                {
                    //ddlUOM.SelectedItem.Value = "1";
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
            if (Convert.ToInt32(txtItemQty.Text) != 0)
            {
                decimal bal;
                decimal chkRepeatItemQty = 0;
                decimal CurrStock;
                if (Convert.ToDecimal(txtItemQty.Text) < 0)
                {
                    if (grvListItem.Rows.Count > 0)
                    {
                        for (int i = 0; i < grvListItem.Rows.Count; i++)
                        {
                            GridViewRow row = grvListItem.Rows[i];

                            Label lblID = (Label)row.FindControl("lblID");
                            Label lblQty = (Label)row.FindControl("lblQty");
                            Label lblItemId = (Label)row.FindControl("lblItemId");
                            Label lblPlantID = (Label)row.FindControl("lblPlantID");
                            Label lblLocationCDID = (Label)row.FindControl("lblLocationCDID");

                            if (lblSRNO.Text != lblID.Text)
                            {
                                if (Convert.ToInt32(lblQty.Text) > 0)
                                {
                                    if (lblItemId.Text == txtItemId.Text && lblPlantID.Text == ddlPLant.SelectedValue && lblLocationCDID.Text == ddlLocation.SelectedValue)
                                    {
                                        chkRepeatItemQty = Convert.ToDecimal(Convert.ToDecimal(chkRepeatItemQty) + Convert.ToDecimal(lblQty.Text));

                                    }
                                }
                            }

                        }


                    }

                    bal = objMainClass.SP_GET_STOCK_FUNCTION(objMainClass.intCmpId, objMainClass.getFinYear(objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true)), txtItemCode.Text, objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true), ddlPLant.SelectedValue, ddlLocation.SelectedValue);
                    CurrStock = bal - chkRepeatItemQty;
                    if (CurrStock >= Math.Abs(Convert.ToDecimal(txtItemQty.Text)))
                    {

                    }
                    else
                    {
                        decimal dQty = Math.Abs(Convert.ToDecimal(txtItemQty.Text)) - Convert.ToDecimal(CurrStock);
                        txtItemQty.Text = string.Empty;
                        txtItemQty.Focus();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"There is a deficit of Quantity by " + dQty + "!\");", true);
                        txtItemQty.Text = string.Empty;
                        txtItemQty.Focus();
                    }
                }

            }
            else
            {
                txtItemQty.Text = string.Empty;
                txtItemQty.Focus();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"Item Quantity can not be 0!\");", true);
                txtItemQty.Text = string.Empty;
                txtItemQty.Focus();
            }
        }

        protected void btnPopup_Click(object sender, EventArgs e)
        {
            objBindDDL.FillItemCat(ddlpopCategory);
            objBindDDL.FillItemGrp(ddlpopGroup);
            objBindDDL.FillItemSubGrp(ddlpopSubGroup);
            objBindDDL.FillBrand(ddlpopMake, 0);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-item').modal();", true);
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
                        txtOnHandStock.Text = Convert.ToString(dt.Rows[0]["maxstockqty"]);
                        txtItemId.Text = Convert.ToString(dt.Rows[0]["itemid"]);
                        txtSku.Text = Convert.ToString(dt.Rows[0]["sku"]);
                        txtItemGroupId.Text = Convert.ToString(dt.Rows[0]["itemgrpid"]);
                        txtItemMake.Text = Convert.ToString(dt.Rows[0]["make"]);
                        txtItemModel.Text = Convert.ToString(dt.Rows[0]["model"]);
                        txtItemDispName.Text = Convert.ToString(dt.Rows[0]["dispname"]);
                        txtItemDispMRP.Text = Convert.ToString(dt.Rows[0]["mrp"]);
                        txtItemValueLimit.Text = Convert.ToString(dt.Rows[0]["valuelimit"]);
                        txtItemMaxStkQty.Text = Convert.ToString(dt.Rows[0]["maxstkqty"]);
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
                lblSRNO.Text = Convert.ToString(index);
                GridViewRow gRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;



                Label lblID = (Label)gRow.FindControl("lblID");
                Label lblItemCode = (Label)gRow.FindControl("lblItemCode");
                Label lblItemDesc = (Label)gRow.FindControl("lblItemDesc");
                Label lblItemId = (Label)gRow.FindControl("lblItemId");
                Label lblItemGroup = (Label)gRow.FindControl("lblItemGroup");
                Label lblGroupId = (Label)gRow.FindControl("lblGroupId");
                Label lblUOM = (Label)gRow.FindControl("lblUOM");
                Label lblUOMID = (Label)gRow.FindControl("lblUOMID");
                Label lblQty = (Label)gRow.FindControl("lblQty");
                Label lblRate = (Label)gRow.FindControl("lblRate");
                Label lblAmount = (Label)gRow.FindControl("lblAmount");
                Label lblDeliDate = (Label)gRow.FindControl("lblDeliDate");
                Label lblGLCode = (Label)gRow.FindControl("lblGLCode");
                Label lblCostCenter = (Label)gRow.FindControl("lblCostCenter");
                Label lblPlantCD = (Label)gRow.FindControl("lblPlantCD");
                Label lblPlantID = (Label)gRow.FindControl("lblPlantID");
                Label lblLocationCD = (Label)gRow.FindControl("lblLocationCD");
                Label lblLocationCDID = (Label)gRow.FindControl("lblLocationCDID");
                Label lblProfitCenter = (Label)gRow.FindControl("lblProfitCenter");
                Label lblAssetCode = (Label)gRow.FindControl("lblAssetCode");
                Label lblRequisitioner = (Label)gRow.FindControl("lblRequisitioner");
                Label lblTrackNo = (Label)gRow.FindControl("lblTrackNo");
                Label lblItemText = (Label)gRow.FindControl("lblItemText");
                Label lblPartReqNo = (Label)gRow.FindControl("lblPartReqNo");
                Label lblRemarks = (Label)gRow.FindControl("lblRemarks");
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

                txtItemCode.Text = lblItemCode.Text;
                txtItemId.Text = lblItemId.Text;
                txtSku.Text = lblSKU.Text;
                txtItemGroup.Text = lblItemGroup.Text;
                txtItemGroupId.Text = lblGroupId.Text;
                txtItemMake.Text = lblMake.Text;
                txtItemModel.Text = lblModel.Text;
                txtItemDispName.Text = lblDispName.Text;
                txtItemDispMRP.Text = lblDispMRP.Text;
                txtItemValueLimit.Text = lblValueLimit.Text;
                txtItemMaxStkQty.Text = lblMaxStkQty.Text;
                txtItemHSN.Text = lblHSN.Text;
                txtItemHSNGroup.Text = lblHSNGroup.Text;
                txtItemHSNGroupDesc.Text = lblHSNGroupDesc.Text;
                txtItemCondType.Text = lblCondType.Text;
                txtItemStatus.Text = lblItemStatus.Text;
                txtGLCode.Text = lblGLCode.Text;
                txtProfitCenter.Text = lblProfitCenter.Text;
                txtItemText.Text = lblItemText.Text;
                txtItemDesc.Text = lblItemDesc.Text;
                txtItemQty.Text = lblQty.Text;
                ddlUOM.SelectedValue = lblUOMID.Text;
                txtOnHandStock.Text = lblOnHandStock.Text;
                txtItemRemarks.Text = lblRemarks.Text;
                ddlPLant.SelectedValue = lblPlantID.Text;
                objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);
                ddlLocation.SelectedValue = lblLocationCDID.Text;
                objBindDDL.FillCostCenter(ddlCostCenter, ddlPLant.SelectedValue, ddlLocation.SelectedValue);
                ddlCostCenter.SelectedValue = lblCostCenter.Text;
                txtAssetCode.Text = lblAssetCode.Text;
                Session["savedet"] = "Update Item";

                DataTable dt = (DataTable)ViewState["ItemData"];
                //dt.Rows[gRow.RowIndex].Delete();
                ViewState["ItemData"] = dt;
                grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                grvListItem.DataBind();

            }
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
            txtItemHSN.Text = string.Empty;
            txtItemHSNGroup.Text = string.Empty;
            txtItemHSNGroupDesc.Text = string.Empty;
            txtItemCondType.Text = string.Empty;
            txtItemStatus.Text = string.Empty;
            txtGLCode.Text = string.Empty;
            txtItemText.Text = string.Empty;
            txtItemDesc.Text = string.Empty;
            txtItemQty.Text = string.Empty;
            txtOnHandStock.Text = string.Empty;
            txtItemRemarks.Text = string.Empty;
        }

        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                objBindDDL.FillCostCenter(ddlCostCenter, ddlPLant.SelectedValue, ddlLocation.SelectedValue);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}