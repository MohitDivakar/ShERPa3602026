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
    public partial class CreateMaterialSplit : System.Web.UI.Page
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


                        objBindDDL.FillDocType(ddlDoctype, "IS");
                        ddlDoctype.SelectedIndex = 1;
                        ddlDoctype.Enabled = false;
                        txtDocNo.Text = objMainClass.MAXPRNO(ddlDoctype.SelectedValue, "IS");
                        txtDocDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();

                        objBindDDL.FillPlant(ddlPlant);
                        string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                        ddlPlant.SelectedValue = plantArray[0];
                        objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPlant.SelectedValue);
                        ddlLocation.SelectedIndex = 1;

                        objBindDDL.FillPlant(ddlToPlant);
                        ddlToPlant.SelectedValue = plantArray[0];
                        objBindDDL.FillLocationByPlantCd(ddlToLocation, ddlToPlant.SelectedValue);
                        ddlToLocation.SelectedIndex = 1;

                        objBindDDL.FillCostCenter(ddlCostCenter, ddlPlant.SelectedValue, ddlLocation.SelectedValue);

                        objBindDDL.FillItemCat(ddlpopCategory);
                        objBindDDL.FillItemGrp(ddlpopGroup);
                        objBindDDL.FillItemSubGrp(ddlpopSubGroup);
                        objBindDDL.FillBrand(ddlpopMake, 0);

                        objBindDDL.FillItemCat(ddlpopToCategory);
                        objBindDDL.FillItemGrp(ddlpopToGroup);
                        objBindDDL.FillItemSubGrp(ddlpopToSubGroup);
                        objBindDDL.FillBrand(ddlpopToMake, 0);


                        objBindDDL.FillUOM(ddlUOM);




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

                    decimal Total = 0;

                    for (int i = 0; i < grvListItem.Rows.Count; i++)
                    {

                        Total = Total + Convert.ToDecimal(((Label)grvListItem.Rows[i].FindControl("lblRate")).Text.Trim());

                    }


                    if (Convert.ToString(Math.Round(Convert.ToDecimal(txtMRate.Text), 2)) != Convert.ToString(Math.Round(Total, 2)))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Distribution must be 100%, should not be higher or lower!');", true);
                    }
                    else
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
                            string DOCNO = objMainClass.InsertIS(objMainClass.intCmpId, ddlDoctype.SelectedItem.Text, txtDocNo.Text, txtDocDate.Text, txtRefno.Text, txtREMARKS.Text, grvListItem, Convert.ToString(Session["USERID"]));
                            if (DOCNO != "")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Document No. : " + DOCNO + " saved successfully.\");$('.close').click(function(){window.location.href ='ViewMaterialIssue.aspx?REQUESTFORM=IS' });", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Record not inserted sucessfully!');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have plant rights.');", true);
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

        protected void btnSaveDet_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    string CRDR;

                    if (txtQty.Text.Contains("-"))
                    {
                        txtQty.Text = txtQty.Text.Remove('-');
                        CRDR = "D";
                    }
                    else
                    {
                        CRDR = "C";
                    }


                    string validation = validateData();
                    if (validation == "OK")
                    {
                        DataTable dt = (DataTable)ViewState["ItemData"];
                        //DataRow dr = new DataRow();
                        DataTable ddt = (DataTable)ViewState["ItemData"];
                        DataRow dr = ddt.Select("SRNO = '" + txtSrNo.Text + "'")[0];
                        dr[1] = txtFromItemCode.Text;
                        dr[2] = txtFromItemDesc.Text;
                        dr[3] = txtFromItemId.Text;
                        dr[4] = txtToItemId.Text;
                        dr[5] = txtToItemCode.Text;
                        dr[6] = txtToItemDesc.Text;
                        dr[7] = txtToItemGroup.Text;
                        dr[8] = txtToItemGroupId.Text;
                        dr[9] = ddlUOM.SelectedItem.Text;
                        dr[10] = ddlUOM.SelectedValue;
                        dr[11] = txtQty.Text;
                        dr[12] = CRDR;
                        dr[13] = txtToRate.Text;
                        dr[14] = txtToAmount.Text;
                        dr[15] = txtToGLCode.Text;
                        dr[16] = ddlCostCenter.SelectedValue;//txtCostCenter.Text;
                        dr[17] = ddlToPlant.SelectedItem.Text;
                        dr[18] = ddlToPlant.SelectedValue;
                        dr[19] = ddlToLocation.SelectedItem.Text;
                        dr[20] = ddlToLocation.SelectedValue;
                        dr[21] = txtToProfitCenter.Text;
                        dr[22] = txtToAssetCode.Text;
                        dr[23] = txtToItemText.Text;
                        dr[24] = txtItemReamrk.Text;
                        dr[25] = txtToSku.Text;
                        dr[26] = txtToItemMake.Text;
                        dr[27] = txtToItemModel.Text;
                        dr[28] = txtToItemDispName.Text;
                        dr[29] = txtToItemDispMRP.Text;
                        dr[30] = txtToItemValueLimit.Text;
                        dr[31] = txtToItemMaxStkQty.Text;
                        dr[32] = txtToItemHSN.Text;
                        dr[33] = txtToItemHSNGroup.Text;
                        dr[34] = txtToItemHSNGroupDesc.Text;
                        dr[35] = txtToItemCondType.Text;
                        dr[36] = txtToItemStatus.Text;
                        dr[37] = txtToOnHandStock.Text;

                        grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                        grvListItem.DataBind();

                        //Session["savedet"] = "Save Item";

                        decimal Total = 0;

                        for (int i = 0; i < grvListItem.Rows.Count; i++)
                        {

                            Total = Total + Convert.ToDecimal(((Label)grvListItem.Rows[i].FindControl("lblRate")).Text.Trim());


                        }

                        txtPending.Text = Convert.ToString(Math.Round((100 - (Total * 100 / Convert.ToDecimal(txtMRate.Text))), 2));



                        EmptyString();
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

        private void EmptyString()
        {
            txtToItemCode.Text = string.Empty;
            txtToItemId.Text = string.Empty;
            txtToSku.Text = string.Empty;
            txtToItemGroup.Text = string.Empty;
            txtToItemGroupId.Text = string.Empty;
            txtToItemMake.Text = string.Empty;
            txtToItemModel.Text = string.Empty;
            txtToItemDispName.Text = string.Empty;
            txtToItemDispMRP.Text = string.Empty;
            txtToItemValueLimit.Text = string.Empty;
            txtToItemMaxStkQty.Text = string.Empty;
            txtToItemHSN.Text = string.Empty;
            txtToItemHSNGroup.Text = string.Empty;
            txtToItemHSNGroupDesc.Text = string.Empty;
            txtToItemCondType.Text = string.Empty;
            txtToItemStatus.Text = string.Empty;
            txtToGLCode.Text = string.Empty;
            //txtProfitCenter.Text = string.Empty;
            txtToItemText.Text = string.Empty;
            txtToItemDesc.Text = string.Empty;
            txtQty.Text = string.Empty;
            //ddlUOM.SelectedValue = string.Empty;
            txtToRate.Text = string.Empty;
            txtToAmount.Text = string.Empty;
            txtItemReamrk.Text = string.Empty;
            //ddlPLant.SelectedValue = string.Empty;
            //ddlLocation.SelectedValue = string.Empty;
            //txtCostCenter.Text = string.Empty;
            //txtAssetCode.Text = string.Empty;
            txtDist.Text = string.Empty;
        }

        private string validateData()
        {
            string j = "ERROR";
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
                j = "You do not have plant rights. ";
            }
            return j;
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
                    txtSrNo.Text = Convert.ToString(index);
                    GridViewRow gRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    Label lblID = (Label)gRow.FindControl("lblID");
                    Label lblItemCode = (Label)gRow.FindControl("lblItemCode");
                    Label lblItemDesc = (Label)gRow.FindControl("lblItemDesc");
                    Label lblItemId = (Label)gRow.FindControl("lblItemId");
                    Label lblToItemId = (Label)gRow.FindControl("lblToItemId");
                    Label lblToItemCode = (Label)gRow.FindControl("lblToItemCode");
                    Label lblToItemDesc = (Label)gRow.FindControl("lblToItemDesc");
                    Label lblItemGroup = (Label)gRow.FindControl("lblItemGroup");
                    Label lblGroupId = (Label)gRow.FindControl("lblGroupId");
                    Label lblUOM = (Label)gRow.FindControl("lblUOM");
                    Label lblUOMID = (Label)gRow.FindControl("lblUOMID");
                    Label lblQty = (Label)gRow.FindControl("lblQty");
                    Label lblCRDR = (Label)gRow.FindControl("lblCRDR");
                    Label lblRate = (Label)gRow.FindControl("lblRate");
                    Label lblAmount = (Label)gRow.FindControl("lblAmount");
                    Label lblGLCode = (Label)gRow.FindControl("lblGLCode");
                    Label lblCostCenter = (Label)gRow.FindControl("lblCostCenter");
                    Label lblPlantCD = (Label)gRow.FindControl("lblPlantCD");
                    Label lblPlantID = (Label)gRow.FindControl("lblPlantID");
                    Label lblLocationCD = (Label)gRow.FindControl("lblLocationCD");
                    Label lblLocationCDID = (Label)gRow.FindControl("lblLocationCDID");
                    Label lblProfitCenter = (Label)gRow.FindControl("lblProfitCenter");
                    Label lblAssetCode = (Label)gRow.FindControl("lblAssetCode");
                    Label lblItemText = (Label)gRow.FindControl("lblItemText");
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

                    txtToItemCode.Text = lblToItemCode.Text;
                    ddlToPlant.SelectedValue = lblPlantID.Text;
                    objBindDDL.FillLocationByPlantCd(ddlToLocation, ddlToPlant.SelectedValue);
                    ddlToLocation.SelectedValue = lblLocationCDID.Text;

                    //txtToItemCode_TextChanged(1, e);

                    DataTable dt1 = new DataTable();
                    dt1 = objMainClass.GetItemDetails(txtToItemCode.Text, ddlToPlant.SelectedValue, ddlToLocation.SelectedValue);
                    if (dt1.Rows.Count > 0)
                    {
                        if (Convert.ToString(dt1.Rows[0]["status"]) == "1")
                        {
                            txtToItemDesc.Text = Convert.ToString(dt1.Rows[0]["itemdesc"]);
                            txtToGLCode.Text = Convert.ToString(dt1.Rows[0]["glcode"]);
                            txtToItemGroup.Text = Convert.ToString(dt1.Rows[0]["itemgrp"]);
                            txtToItemId.Text = Convert.ToString(dt1.Rows[0]["itemid"]);
                            txtToSku.Text = Convert.ToString(dt1.Rows[0]["sku"]);
                            txtToItemGroupId.Text = Convert.ToString(dt1.Rows[0]["itemgrpid"]);
                            txtToItemMake.Text = Convert.ToString(dt1.Rows[0]["make"]);
                            txtToItemModel.Text = Convert.ToString(dt1.Rows[0]["model"]);
                            txtToItemDispName.Text = Convert.ToString(dt1.Rows[0]["dispname"]);
                            txtToItemDispMRP.Text = Convert.ToString(dt1.Rows[0]["mrp"]);
                            txtToItemValueLimit.Text = Convert.ToString(dt1.Rows[0]["valuelimit"]);
                            txtToItemMaxStkQty.Text = Convert.ToString(dt1.Rows[0]["maxstkqty"]);
                            txtToItemHSN.Text = Convert.ToString(dt1.Rows[0]["hsngrpcode"]);
                            txtToItemHSNGroup.Text = Convert.ToString(dt1.Rows[0]["hsngrp"]);
                            txtToItemHSNGroupDesc.Text = Convert.ToString(dt1.Rows[0]["hsngrpdesc"]);
                            txtToItemCondType.Text = Convert.ToString(dt1.Rows[0]["condtype"]);
                            txtToItemStatus.Text = Convert.ToString(dt1.Rows[0]["status"]);

                            ddlUOM.SelectedValue = txtToSku.Text;

                            DataTable dtPrCt = objMainClass.GetPRFCost(ddlToPlant.SelectedValue, Convert.ToString(Session["USERID"]));
                            txtToProfitCenter.Text = Convert.ToString(dtPrCt.Rows[0]["PRFCNT"]);
                            //txtCostCenter.Text = Convert.ToString(dtPrCt.Rows[0]["CSTCENTCD"]);
                            objBindDDL.FillCostCenter(ddlCostCenter, ddlPlant.SelectedValue, ddlLocation.SelectedValue);
                            ddlCostCenter.SelectedValue = lblCostCenter.Text;

                            txtToItemText.Text = lblItemText.Text;
                            txtToRate.Text = lblRate.Text;
                            txtToAmount.Text = lblAmount.Text;
                            txtQty.Text = lblQty.Text;
                            txtItemReamrk.Text = lblRemarks.Text;
                            txtToOnHandStock.Text = lblOnHandStock.Text;
                            //Session["savedet"] = "Update Item";
                            DataTable dt = (DataTable)ViewState["ItemData"];
                            ViewState["ItemData"] = dt;
                            grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                            grvListItem.DataBind();
                        }
                        else
                        {


                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + txtToItemCode.Text + " - Item code is deactivated, you can't use the same!\");", true);
                            txtToItemCode.Focus();

                            txtToItemCode.Text = string.Empty;
                            ddlToPlant.SelectedValue = ddlPlant.SelectedValue;
                            objBindDDL.FillLocationByPlantCd(ddlToLocation, ddlToPlant.SelectedValue);
                            ddlToLocation.SelectedValue = ddlLocation.SelectedValue;
                        }
                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + txtToItemCode.Text + " - Item code not found!\");", true);

                        txtToItemCode.Text = string.Empty;
                        ddlToPlant.SelectedValue = ddlPlant.SelectedValue;
                        objBindDDL.FillLocationByPlantCd(ddlToLocation, ddlToPlant.SelectedValue);
                        ddlToLocation.SelectedValue = ddlLocation.SelectedValue;
                        txtToItemCode.Focus();
                        txtToItemCode.Text = string.Empty;
                    }


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
                txtFromItemCode.Text = Convert.ToString(grvPopItem.SelectedRow.Cells[1].Text);
                txtFromItemCode_TextChanged(1, e);
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
                            txtToProfitCenter.Text = Convert.ToString(dt.Rows[0]["PRFCNT"]);
                            //txtCostCenter.Text = Convert.ToString(dt.Rows[0]["CSTCENTCD"]);
                        }
                        else
                        {
                            txtToProfitCenter.Text = "1000";
                            //txtCostCenter.Text = "1000";
                        }
                    }
                    else
                    {
                        ddlToPlant.SelectedValue = plantArray[0];
                        ddlToPlant.Focus();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have plant rights.');$('#ddlToPlant').focus();", true);
                        ddlToPlant.SelectedValue = plantArray[0];
                        ddlToPlant.Focus();
                        objBindDDL.FillLocationByPlantCd(ddlToLocation, ddlToPlant.SelectedValue);
                        ddlToLocation.SelectedIndex = 1;
                    }
                    objBindDDL.FillCostCenter(ddlCostCenter, ddlPlant.SelectedValue, ddlLocation.SelectedValue);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        //protected void txtPending_TextChanged(object sender, EventArgs e)
        //{

        //}

        protected void txtDist_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtDist.Text) > 0)
            {
                txtToRate.Text = Convert.ToString((Convert.ToDecimal(txtMRate.Text) * Convert.ToDecimal(txtDist.Text)) / 100);
                txtToRate.Text = Convert.ToString(Math.Round(Convert.ToDecimal(txtToRate.Text), 2));

                txtQty_TextChanged(1, e);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"Dist. percentage can not be 0 or Minus.!\");", true);
            }
        }

        protected void ddlUOM_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlUOM.SelectedItem.Value.ToString() != txtToSku.Text)
                {
                    ddlUOM.SelectedValue = txtToSku.Text;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"UOM is not matched with Base Unit of Item!\");", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void ddlpopToMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                objBindDDL.FillModel(ddlpopToModel, ddlpopToMake.SelectedValue);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Toitem').modal();", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnToShowItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objMainClass.SelectItem(ddlpopToMake.SelectedIndex > 0 ? ddlpopToMake.SelectedItem.Text : "", ddlpopToModel.SelectedIndex > 0 ? ddlpopToModel.SelectedItem.Text : "", txtpopToItemCode.Text, ddlpopToGroup.SelectedIndex > 0 ? ddlpopToGroup.SelectedValue : "", ddlpopToSubGroup.SelectedIndex > 0 ? ddlpopToSubGroup.SelectedValue : "", ddlpopToCategory.SelectedIndex > 0 ? ddlpopToCategory.SelectedValue : "", txtPopupToItemDesc.Text);
                if (dt.Rows.Count > 0)
                {
                    grvToPopItem.DataSource = dt;
                    grvToPopItem.DataBind();
                }
                else
                {
                    grvToPopItem.DataSource = string.Empty;
                    grvToPopItem.DataBind();
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Toitem').modal();", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void grvToPopItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtToItemCode.Text = Convert.ToString(grvToPopItem.SelectedRow.Cells[1].Text);
                txtToItemCode_TextChanged(1, e);
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

        protected void lnkToOpenPoup_Click(object sender, EventArgs e)
        {
            try
            {
                objBindDDL.FillItemCat(ddlpopToCategory);
                objBindDDL.FillItemGrp(ddlpopToGroup);
                objBindDDL.FillItemSubGrp(ddlpopToSubGroup);
                objBindDDL.FillBrand(ddlpopToMake, 0);

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Toitem').modal();", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void txtFromItemCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlPlant.SelectedIndex > 0 && ddlLocation.SelectedIndex > 0)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetItemDetails(txtFromItemCode.Text, ddlPlant.SelectedValue, ddlLocation.SelectedValue);
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToString(dt.Rows[0]["status"]) == "1")
                        {


                            txtFromItemDesc.Text = Convert.ToString(dt.Rows[0]["itemdesc"]);
                            txtFromGLCode.Text = Convert.ToString(dt.Rows[0]["glcode"]);
                            txtFromItemGroup.Text = Convert.ToString(dt.Rows[0]["itemgrp"]);
                            txtFromItemId.Text = Convert.ToString(dt.Rows[0]["itemid"]);
                            txtFromSku.Text = Convert.ToString(dt.Rows[0]["sku"]);
                            txtFromItemGroupId.Text = Convert.ToString(dt.Rows[0]["itemgrpid"]);
                            txtFromItemMake.Text = Convert.ToString(dt.Rows[0]["make"]);
                            txtFromItemModel.Text = Convert.ToString(dt.Rows[0]["model"]);
                            txtFromItemDispName.Text = Convert.ToString(dt.Rows[0]["dispname"]);
                            txtFromItemDispMRP.Text = Convert.ToString(dt.Rows[0]["mrp"]);
                            txtFromItemValueLimit.Text = Convert.ToString(dt.Rows[0]["valuelimit"]);
                            txtFromItemMaxStkQty.Text = Convert.ToString(dt.Rows[0]["maxstkqty"]);
                            txtFromItemHSN.Text = Convert.ToString(dt.Rows[0]["hsngrpcode"]);
                            txtFromItemHSNGroup.Text = Convert.ToString(dt.Rows[0]["hsngrp"]);
                            txtFromItemHSNGroupDesc.Text = Convert.ToString(dt.Rows[0]["hsngrpdesc"]);
                            txtFromItemCondType.Text = Convert.ToString(dt.Rows[0]["condtype"]);
                            txtFromItemStatus.Text = Convert.ToString(dt.Rows[0]["status"]);

                            txtMRate.Text = Convert.ToString(objMainClass.SP_CAL_AVGRATE(objMainClass.intCmpId, ddlPlant.SelectedValue, ddlLocation.SelectedValue, txtFromSku.Text, txtFromItemId.Text));

                            decimal bal = objMainClass.SP_GET_STOCK_FUNCTION(objMainClass.intCmpId, objMainClass.getFinYear(objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true)), txtFromItemCode.Text, objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true), ddlPlant.SelectedValue, ddlLocation.SelectedValue);
                            txtCurrStock.Text = Convert.ToString(bal);
                            if (bal <= 0)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"There is a deficit of Quantity by " + (1 - bal) + " !\");", true);
                                grvListItem.DataSource = string.Empty;
                                grvListItem.DataBind();

                                ViewState["ItemData"] = null;
                            }
                            else
                            {
                                DataTable Splitdt = new DataTable();
                                Splitdt = objMainClass.GetSplitItem(objMainClass.intCmpId, txtFromItemMake.Text, txtFromItemModel.Text, ddlPlant.SelectedValue, ddlLocation.SelectedValue, ddlPlant.SelectedItem.Text, ddlLocation.SelectedItem.Text);
                                if (Splitdt.Rows.Count > 0)
                                {
                                    grvListItem.DataSource = Splitdt;
                                    grvListItem.DataBind();

                                    ViewState["ItemData"] = Splitdt;

                                    txtFromItemCode.Enabled = false;
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"There is a deficit of Quantity by " + (1 - bal) + " !\");", true);
                                }
                            }



                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + txtFromItemCode.Text + " - Item code not found!\");", true);
                            txtFromItemCode.Focus();
                        }
                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + txtFromItemCode.Text + " - Item code not found!\");", true);
                        txtFromItemCode.Focus();
                        txtFromItemCode.Text = string.Empty;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"Select Plant & Location code!\");", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void txtToItemCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objMainClass.GetItemDetails(txtToItemCode.Text, ddlToPlant.SelectedValue, ddlToLocation.SelectedValue);
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToString(dt.Rows[0]["status"]) == "1")
                    {
                        txtToItemDesc.Text = Convert.ToString(dt.Rows[0]["itemdesc"]);
                        txtToGLCode.Text = Convert.ToString(dt.Rows[0]["glcode"]);
                        txtToItemGroup.Text = Convert.ToString(dt.Rows[0]["itemgrp"]);
                        txtToItemId.Text = Convert.ToString(dt.Rows[0]["itemid"]);
                        txtToSku.Text = Convert.ToString(dt.Rows[0]["sku"]);
                        txtToItemGroupId.Text = Convert.ToString(dt.Rows[0]["itemgrpid"]);
                        txtToItemMake.Text = Convert.ToString(dt.Rows[0]["make"]);
                        txtToItemModel.Text = Convert.ToString(dt.Rows[0]["model"]);
                        txtToItemDispName.Text = Convert.ToString(dt.Rows[0]["dispname"]);
                        txtToItemDispMRP.Text = Convert.ToString(dt.Rows[0]["mrp"]);
                        txtToItemValueLimit.Text = Convert.ToString(dt.Rows[0]["valuelimit"]);
                        txtToItemMaxStkQty.Text = Convert.ToString(dt.Rows[0]["maxstkqty"]);
                        txtToItemHSN.Text = Convert.ToString(dt.Rows[0]["hsngrpcode"]);
                        txtToItemHSNGroup.Text = Convert.ToString(dt.Rows[0]["hsngrp"]);
                        txtToItemHSNGroupDesc.Text = Convert.ToString(dt.Rows[0]["hsngrpdesc"]);
                        txtToItemCondType.Text = Convert.ToString(dt.Rows[0]["condtype"]);
                        txtToItemStatus.Text = Convert.ToString(dt.Rows[0]["status"]);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + txtToItemCode.Text + " - Item code is deactivated, you can't use the same!\");", true);
                        txtToItemCode.Focus();
                    }
                }
                else
                {
                    txtToItemCode.Focus();
                    txtToItemCode.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + txtToItemCode.Text + " - Item code not found!\");", true);
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
                            txtFromProfitCenter.Text = Convert.ToString(dt.Rows[0]["PRFCNT"]);
                            //txtCostCenter.Text = Convert.ToString(dt.Rows[0]["CSTCENTCD"]);
                        }
                        else
                        {
                            txtFromProfitCenter.Text = "1000";
                            //txtCostCenter.Text = "1000";
                        }

                        ddlToPlant.SelectedValue = ddlPlant.SelectedValue;
                        objBindDDL.FillLocationByPlantCd(ddlToLocation, ddlToPlant.SelectedValue);
                        ddlLocation.SelectedIndex = 1;
                        ddlToLocation.SelectedIndex = 1;


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
                        ddlToPlant.SelectedValue = ddlPlant.SelectedValue;
                        objBindDDL.FillLocationByPlantCd(ddlToLocation, ddlToPlant.SelectedValue);
                        ddlToLocation.SelectedIndex = 1;
                    }

                    if (txtFromItemCode.Text != "" && txtFromItemCode.Text != string.Empty && txtFromItemCode.Text != null)
                    {
                        txtFromItemCode_TextChanged(1, e);
                    }
                    objBindDDL.FillCostCenter(ddlCostCenter, ddlPlant.SelectedValue, ddlLocation.SelectedValue);
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
                txtToAmount.Text = Convert.ToString(Convert.ToDecimal(txtToRate.Text == "" ? "0" : txtToRate.Text) * Convert.ToDecimal(txtQty.Text == "" ? "0" : txtQty.Text));

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlToLocation.SelectedValue = ddlLocation.SelectedValue;

            if (txtFromItemCode.Text != "" && txtFromItemCode.Text != string.Empty && txtFromItemCode.Text != null)
            {
                txtFromItemCode_TextChanged(1, e);
            }
            objBindDDL.FillCostCenter(ddlCostCenter, ddlPlant.SelectedValue, ddlLocation.SelectedValue);
        }
    }
}