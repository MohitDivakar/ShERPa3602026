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
    public partial class CreateMaterialConsumption : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        DataTable dtItem = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RangeValidator1.MaximumValue = Convert.ToString(DateTime.Now.AddDays(1).ToShortDateString());
                RangeValidator1.MinimumValue = Convert.ToString(DateTime.Now.AddDays(-1).ToShortDateString());
                lblPRSRNO.Text = string.Empty;
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

                        // Initiate Master Control    
                        objBindDDL.FillDocType(ddlDoctype, "CM");
                        ddlDoctype.SelectedIndex = 1;
                        ddlDoctype.Enabled = false;
                        txtDocNo.Text = objMainClass.MAXPRNO(ddlDoctype.SelectedValue, "CM");
                        txtDocDt.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();

                        objBindDDL.FillItemCat(ddlpopCategory);
                        objBindDDL.FillItemGrp(ddlpopGroup);
                        objBindDDL.FillItemSubGrp(ddlpopSubGroup);
                        objBindDDL.FillBrand(ddlpopMake, 0);

                        //objBindDDL.FillDepartment(ddlIssueDepartment);
                        //ddlIssueDepartment.SelectedIndex = 1;
                        // Initiate Master Control    

                        // Initiate Child Control    
                        objBindDDL.FillPlant(ddlPlant);
                        string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                        ddlPlant.SelectedValue = plantArray[0];
                        objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPlant.SelectedValue);
                        ddlLocation.SelectedIndex = 1;
                        objBindDDL.FillUOM(ddlUOM);

                        objBindDDL.FillCostCenter(ddlCostCenter, ddlPlant.SelectedValue, ddlLocation.SelectedValue);

                        Session["savedet"] = "Save Item";
                        Session["saveall"] = "Save All";

                        LoadPlantCostCenter();

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
                dtColumn.ColumnName = "GLCODE";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "COSTCENTER";
                dtItem.Columns.Add(dtColumn);

                ///

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
                dtColumn.ColumnName = "LOCCDFROMID";
                dtItem.Columns.Add(dtColumn);


                dtColumn = new DataColumn();
                dtColumn.ColumnName = "PROFITCENTER";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ASSETCODE";
                dtItem.Columns.Add(dtColumn);


                dtColumn = new DataColumn();
                dtColumn.ColumnName = "TRACKNO";
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

                ViewState["ItemData"] = dtItem;
                grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                grvListItem.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        private void LoadPlantCostCenter()
        {
            try
            {
                DataTable dt = objMainClass.GetPRFCost(ddlPlant.SelectedValue, Convert.ToString(Session["USERID"]));
                if (dt.Rows.Count > 0)
                {
                    txtProfitCenter.Text = Convert.ToString(dt.Rows[0]["PRFCNT"]);
                    //txtCostCenter.Text = Convert.ToString(dt.Rows[0]["CSTCENTCD"]);
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
                objBindDDL.FillItemCat(ddlpopCategory);
                objBindDDL.FillItemGrp(ddlpopGroup);
                objBindDDL.FillItemSubGrp(ddlpopSubGroup);
                objBindDDL.FillBrand(ddlpopMake, 0);

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-item').modal();", true);
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
                dt = objMainClass.GetItemDetails(txtItemCode.Text, ddlPlant.SelectedValue, ddlLocation.SelectedValue);
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
                        txtItemHSN.Text = Convert.ToString(dt.Rows[0]["hsngrpcode"]);
                        txtItemHSNGroup.Text = Convert.ToString(dt.Rows[0]["hsngrp"]);
                        txtItemHSNGroupDesc.Text = Convert.ToString(dt.Rows[0]["hsngrpdesc"]);
                        txtItemCondType.Text = Convert.ToString(dt.Rows[0]["condtype"]);
                        txtItemStatus.Text = Convert.ToString(dt.Rows[0]["status"]);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + txtItemCode.Text + " - Item code is deactivated, you can't use the same!\");", true);
                        txtItemCode.Focus();
                    }
                }
                else
                {
                    txtItemCode.Focus();
                    txtItemCode.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + txtItemCode.Text + " - Item code not found!\");", true);
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

        protected void ddlPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPlant.SelectedValue);
            try
            {
                if (Session["USERID"] != null)
                {
                    string PLantRights = string.Empty;
                    string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                    for (int i = 0; i < plantArray.Count(); i++)
                    {
                        if (plantArray[i].Trim() == ddlPlant.SelectedValue)
                        {
                            PLantRights = ddlPlant.SelectedValue;
                        }
                    }

                    if (PLantRights.Length > 0)
                    {
                        DataTable dt = objMainClass.GetPRFCost(ddlPlant.SelectedValue, Convert.ToString(Session["USERID"]));
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
                        ddlPlant.SelectedValue = plantArray[0];
                        ddlPlant.Focus();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have plant rights.');$('#ddlPlant').focus();", true);
                        ddlPlant.SelectedValue = plantArray[0];
                        ddlPlant.Focus();
                        objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPlant.SelectedValue);
                        ddlLocation.SelectedIndex = 1;
                    }

                    objBindDDL.FillCostCenter(ddlCostCenter, ddlPlant.SelectedValue, ddlLocation.SelectedValue);
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
                        if (Convert.ToString(Session["savedet"]) == "Save Item")
                        {
                            DataTable MRdt = (DataTable)ViewState["ItemData"];
                            if (grvListItem.Rows.Count > 0)
                            {
                                DataRow lastRow = MRdt.Rows[MRdt.Rows.Count - 1];
                                int id = Convert.ToInt32(lastRow["ID"]) + 1;

                                MRdt.Rows.Add(id, txtItemCode.Text, txtItemDesc.Text, txtItemId.Text, txtItemGroup.Text, txtItemGroupId.Text,
                                              ddlUOM.SelectedItem.Text, ddlUOM.SelectedValue, txtItemQty.Text, txtGLCode.Text, ddlCostCenter.SelectedValue,
                                              ddlPlant.SelectedItem.Text, ddlPlant.SelectedValue, ddlLocation.SelectedItem.Text, ddlLocation.SelectedValue,
                                              txtProfitCenter.Text, txtAssetCode.Text, txtTrackingNO.Text, txtDetailRemark.Text, txtSku.Text, txtItemMake.Text);

                                ViewState["ItemData"] = MRdt;
                            }
                            else
                            {
                                MRdt.Rows.Add("1", txtItemCode.Text, txtItemDesc.Text, txtItemId.Text, txtItemGroup.Text, txtItemGroupId.Text,
                                ddlUOM.SelectedItem.Text, ddlUOM.SelectedValue, txtItemQty.Text, txtGLCode.Text, ddlCostCenter.SelectedValue,
                                ddlPlant.SelectedItem.Text, ddlPlant.SelectedValue, ddlLocation.SelectedItem.Text, ddlLocation.SelectedValue,
                                txtProfitCenter.Text, txtAssetCode.Text, txtTrackingNO.Text, txtDetailRemark.Text, txtSku.Text, txtItemMake.Text);

                                ViewState["ItemData"] = MRdt;
                            }

                            grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                            grvListItem.DataBind();
                            EmptyString();
                        }
                        else if (Convert.ToString(Session["savedet"]) == "Update Item")
                        {
                            DataTable dt = (DataTable)ViewState["ItemData"];
                            DataTable ddt = (DataTable)ViewState["ItemData"];
                            DataRow dr = ddt.Select("ID = " + lblPRSRNO.Text)[0];
                            dr[1] = txtItemCode.Text;
                            dr[2] = txtItemDesc.Text;
                            dr[3] = txtItemId.Text;
                            dr[4] = txtItemGroup.Text;
                            dr[5] = txtItemGroupId.Text;
                            dr[6] = ddlUOM.SelectedItem.Text;
                            dr[7] = ddlUOM.SelectedValue;
                            dr[8] = txtItemQty.Text;
                            dr[9] = txtGLCode.Text;
                            dr[10] = ddlCostCenter.SelectedValue;//txtCostCenter.Text;
                            dr[11] = ddlPlant.SelectedItem.Text;
                            dr[12] = ddlPlant.SelectedValue;
                            dr[13] = ddlLocation.SelectedItem.Text;
                            dr[14] = ddlLocation.SelectedValue;
                            dr[15] = txtProfitCenter.Text;
                            dr[16] = txtAssetCode.Text;
                            dr[17] = txtTrackingNO.Text;
                            dr[18] = txtDetailRemark.Text;
                            dr[19] = txtSku.Text;
                            dr[20] = txtItemMake.Text;
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
            try
            {
                string PLantRights = string.Empty;
                string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                for (int i = 0; i < plantArray.Count(); i++)
                {
                    if (plantArray[i].Trim() == ddlPlant.SelectedValue)
                    {
                        PLantRights = ddlPlant.SelectedValue;
                    }
                }

                if (PLantRights.Length > 0)
                {
                    j = "OK";
                }
                else
                {
                    j = "You do not have plant rights.";
                }

                if (j == "OK")
                {
                    Decimal bal = objMainClass.SP_GET_STOCK_FUNCTION(objMainClass.intCmpId, objMainClass.getFinYear(objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true)), txtItemCode.Text, objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true), ddlPlant.SelectedValue, ddlLocation.SelectedValue);
                    if (bal == 0)
                    {
                        j = "We don't have " + txtItemDesc.Text + " Item Stock for Material Issue.";
                    }
                    else if (bal < Convert.ToDecimal(txtItemQty.Text))
                    {
                        j = "We don't have sufficient " + txtItemDesc.Text + " Item Stock for Material Issue.";
                    }
                }
                return j;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return j;
        }

        protected void grvListItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        private void EmptyString()
        {
            try
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
                //txtCostCenter.Text = string.Empty;
                txtTrackingNO.Text = string.Empty;
                txtDetailRemark.Text = string.Empty;
                LoadPlantCostCenter();
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
                objBindDDL.FillModel(ddlpopModel, ddlpopMake.SelectedValue);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-item').modal();", true);
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
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void grvPopItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtItemCode.Text = Convert.ToString(grvPopItem.SelectedRow.Cells[1].Text);
                txtItemCode_TextChanged(1, e);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void grvListItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
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
                    lblPRSRNO.Text = Convert.ToString(index);
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
                    Label lblGLCode = (Label)gRow.FindControl("lblGLCode");
                    Label lblCostCenter = (Label)gRow.FindControl("lblCostCenter");
                    Label lblPlantCD = (Label)gRow.FindControl("lblPlantCD");
                    Label lblPlantID = (Label)gRow.FindControl("lblPlantID");
                    Label lblLocationCD = (Label)gRow.FindControl("lblLocationCD");
                    Label lblLocationCDID = (Label)gRow.FindControl("lblLocationCDID");
                    Label lblProfitCenter = (Label)gRow.FindControl("lblProfitCenter");
                    Label lblAssetCode = (Label)gRow.FindControl("lblAssetCode");
                    Label lblTrackNo = (Label)gRow.FindControl("lblTrackNo");
                    Label lblRemarks = (Label)gRow.FindControl("lblRemarks");
                    Label lblSKU = (Label)gRow.FindControl("lblSKU");
                    Label lblMake = (Label)gRow.FindControl("lblMake");

                    //Label lblOnHandStock = (Label)gRow.FindControl("lblOnHandStock");
                    txtItemId.Text = lblItemId.Text;
                    txtItemCode.Text = lblItemCode.Text;
                    txtItemDesc.Text = lblItemDesc.Text;
                    txtItemGroup.Text = lblItemGroup.Text;
                    txtItemGroupId.Text = lblGroupId.Text;
                    ddlUOM.SelectedValue = lblUOMID.Text;
                    txtItemQty.Text = lblQty.Text;
                    txtGLCode.Text = lblGLCode.Text;

                    ddlPlant.SelectedValue = lblPlantID.Text;
                    objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPlant.SelectedValue);
                    ddlLocation.SelectedValue = lblLocationCDID.Text;
                    objBindDDL.FillCostCenter(ddlCostCenter, ddlPlant.SelectedValue, ddlLocation.SelectedValue);
                    ddlCostCenter.SelectedValue = lblCostCenter.Text;
                    txtProfitCenter.Text = lblProfitCenter.Text;
                    txtAssetCode.Text = lblAssetCode.Text;
                    txtTrackingNO.Text = lblTrackNo.Text;
                    txtItemText.Text = lblRemarks.Text;
                    txtDetailRemark.Text = lblRemarks.Text;
                    txtSku.Text = lblSKU.Text;
                    txtItemMake.Text = lblMake.Text;
                    Session["savedet"] = "Update Item";
                    DataTable dt = (DataTable)ViewState["ItemData"];
                    ViewState["ItemData"] = dt;
                    grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                    grvListItem.DataBind();
                }
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
                                if (plantArray[i].Trim() == ((Label)grvListItem.Rows[0].FindControl("lblPlantID")).Text)
                                {
                                    PLantRights = ((Label)grvListItem.Rows[0].FindControl("lblPlantID")).Text;
                                }
                            }

                            if (PLantRights.Length > 0)
                            {
                                string DOCNO = objMainClass.InsertDepartmentMaterialConsume(ddlDoctype.SelectedItem.Text, txtDocNo.Text, txtDocDt.Text, txtRefNo.Text, txtRemarks.Text, grvListItem, Convert.ToString(Session["USERID"]), objMainClass.intCmpId, 5);
                                if (DOCNO != "")
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Document No. : " + DOCNO + " saved successfully.\");$('.close').click(function(){window.location.href ='ViewMaterialIssue.aspx?REQUESTFORM=CM' });", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Record not inserted sucessfully!');", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have plant rights.');$('#txtTrackingNO').focus();", true);
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

        protected void txtTrackingNO_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (txtTrackingNO.Text != null && txtTrackingNO.Text != "" && txtTrackingNO.Text != string.Empty)
                    {
                        DataTable dt = new DataTable();
                        dt = objMainClass.SelectJobDetails(txtTrackingNO.Text);
                        if (dt.Rows.Count > 0)
                        {

                        }
                        else
                        {
                            txtTrackingNO.Text = "";
                            txtTrackingNO.Text = string.Empty;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid Tracking No.');$('#txtTrackingNO').focus();", true);
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

        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                objBindDDL.FillCostCenter(ddlCostCenter, ddlPlant.SelectedValue, ddlLocation.SelectedValue);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}