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
    public partial class CreateIST : System.Web.UI.Page
    {
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        DataTable dtItem = new DataTable();

        #region ISTMASTERDEAILEVENTS
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
                        if (Request.QueryString.Count > 0)
                        {
                            if (Convert.ToString(Request.QueryString["ITEMSR"]) != null && Convert.ToString(Request.QueryString["ITEMSR"]) != string.Empty && Convert.ToString(Request.QueryString["ITEMSR"]) != "")
                            {
                                Session["MRITEMSR"] = Convert.ToString(Request.QueryString["ITEMSR"]);
                                Session["QMRNO"] = Convert.ToString(Request.QueryString["MRNO"]);
                            }
                            Response.Redirect(Request.Url.AbsolutePath, false);
                        }
                        else
                        {
                            //Session["MRITEMSR"] = null;
                            //Session["QMRNO"]    = null;

                            objBindDDL.FillDocType(ddlDoctype, "IST");
                            ddlDoctype.SelectedIndex = 1;
                            ddlDoctype.Enabled = false;

                            objBindDDL.FillPlant(ddlPLant);
                            string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                            ddlPLant.SelectedValue = plantArray[0];
                            objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);

                            objBindDDL.FillPlant(ddlFromPlant);
                            string[] plantFromArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                            ddlFromPlant.SelectedValue = plantFromArray[0];
                            objBindDDL.FillLocationByPlantCd(ddlFromLocation, ddlFromPlant.SelectedValue);

                            ddlLocation.SelectedIndex = 1;
                            ddlFromLocation.SelectedValue = "MS01";
                            objBindDDL.FillUOM(ddlUOM);
                            txtDocNo.Text = objMainClass.MAXPRNO(ddlDoctype.SelectedValue, "IST");
                            txtDocDt.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                            LoadPlantCostCenter();

                            objBindDDL.FillCostCenter(ddlCostCenter, ddlFromPlant.SelectedValue, ddlFromLocation.SelectedValue);
                            Session["savedet"] = "Save Item";
                            Session["saveall"] = "Save All";
                            SetUpGrid();

                            objBindDDL.FillItemCat(ddlpopCategory);
                            objBindDDL.FillItemGrp(ddlpopGroup);
                            objBindDDL.FillItemSubGrp(ddlpopSubGroup);
                            objBindDDL.FillBrand(ddlpopMake, 0);

                            if (Session["MRITEMSR"] != null && Convert.ToString(Session["MRITEMSR"]) != "" && Convert.ToString(Session["MRITEMSR"]) != string.Empty)
                            {
                                txtRefNo.Text = Convert.ToString(Session["QMRNO"]);
                                string[] itemsr = Convert.ToString(Session["MRITEMSR"]).Split(',');
                                for (int j = 0; j < itemsr.Count(); j++)
                                {
                                    DataTable itemMR = new DataTable();
                                    itemMR = objMainClass.SelectMRDetail(Convert.ToString(Session["QMRNO"]), objMainClass.intCmpId, 2, Convert.ToString(itemsr[j]));

                                    if (itemMR.Rows.Count > 0)
                                    {
                                        DataTable Itemdt = new DataTable();
                                        Itemdt = objMainClass.GetItemDetails(Convert.ToString(itemMR.Rows[0]["ITEMCODE"]), Convert.ToString(itemMR.Rows[0]["ITEMPLANTID"]), Convert.ToString(itemMR.Rows[0]["LOCCDID"]));
                                        if (Itemdt.Rows.Count > 0)
                                        {
                                            if (Convert.ToString(Itemdt.Rows[0]["status"]) == "1")
                                            {
                                                txtItemDesc.Text = Convert.ToString(Itemdt.Rows[0]["itemdesc"]);
                                                txtGLCode.Text = Convert.ToString(Itemdt.Rows[0]["glcode"]);
                                                txtItemGroup.Text = Convert.ToString(Itemdt.Rows[0]["itemgrp"]);
                                                txtItemId.Text = Convert.ToString(Itemdt.Rows[0]["itemid"]);
                                                txtSku.Text = Convert.ToString(Itemdt.Rows[0]["sku"]);
                                                txtItemGroupId.Text = Convert.ToString(Itemdt.Rows[0]["itemgrpid"]);
                                                txtItemMake.Text = Convert.ToString(Itemdt.Rows[0]["make"]);
                                                txtItemModel.Text = Convert.ToString(Itemdt.Rows[0]["model"]);
                                                txtItemDispName.Text = Convert.ToString(Itemdt.Rows[0]["dispname"]);
                                                txtItemDispMRP.Text = Convert.ToString(Itemdt.Rows[0]["mrp"]);
                                                txtItemValueLimit.Text = Convert.ToString(Itemdt.Rows[0]["valuelimit"]);
                                                txtItemMaxStkQty.Text = Convert.ToString(Itemdt.Rows[0]["maxstkqty"]);
                                                txtItemHSN.Text = Convert.ToString(Itemdt.Rows[0]["hsngrpcode"]);
                                                txtItemHSNGroup.Text = Convert.ToString(Itemdt.Rows[0]["hsngrp"]);
                                                txtItemHSNGroupDesc.Text = Convert.ToString(Itemdt.Rows[0]["hsngrpdesc"]);
                                                txtItemCondType.Text = Convert.ToString(Itemdt.Rows[0]["condtype"]);
                                                txtItemStatus.Text = Convert.ToString(Itemdt.Rows[0]["status"]);
                                                ddlFromPlant.SelectedValue = Convert.ToString(itemMR.Rows[0]["ITEMPLANTID"]);
                                                objBindDDL.FillLocationByPlantCd(ddlFromLocation, ddlFromPlant.SelectedValue);
                                                //ddlFromLocation.SelectedValue = "MS01";
                                                if (ddlFromPlant.SelectedValue == "1001")
                                                {
                                                    if (Convert.ToString(itemMR.Rows[0]["LOCCDID"]) == "WS19")
                                                    {
                                                        ddlFromLocation.SelectedValue = "TS01";
                                                    }
                                                    else
                                                    {
                                                        ddlFromLocation.SelectedValue = "M001";
                                                    }
                                                }
                                                else
                                                {
                                                    ddlFromLocation.SelectedValue = "MS01";
                                                }

                                                DataTable MRdt = (DataTable)ViewState["ItemData"];
                                                if (grvListItem.Rows.Count > 0)
                                                {
                                                    DataRow lastRow = MRdt.Rows[MRdt.Rows.Count - 1];
                                                    int id = Convert.ToInt32(lastRow["ID"]) + 1;
                                                    MRdt.Rows.Add(id, Convert.ToString(itemMR.Rows[0]["ITEMCODE"]), txtItemDesc.Text, txtItemId.Text, txtItemGroup.Text, txtItemGroupId.Text, Convert.ToString(itemMR.Rows[0]["ITEMUOM"]),
                                                        Convert.ToString(itemMR.Rows[0]["UOMID"]), Convert.ToString(itemMR.Rows[0]["ITEMQTY"]), Convert.ToString(itemMR.Rows[0]["ITEMRATE"]), Convert.ToString(itemMR.Rows[0]["ITEMAMOUNT"]), DateTime.Now.ToShortDateString(), txtGLCode.Text, Convert.ToString(itemMR.Rows[0]["COSTCENTER"]),
                                                        ddlFromPlant.SelectedItem.Text, ddlFromPlant.SelectedValue, ddlFromLocation.SelectedItem.Text, ddlFromLocation.SelectedValue,
                                                        Convert.ToString(itemMR.Rows[0]["ITEMPLANTCD"]), Convert.ToString(itemMR.Rows[0]["ITEMPLANTID"]), Convert.ToString(itemMR.Rows[0]["ITEMLOCCD"]), Convert.ToString(itemMR.Rows[0]["LOCCDID"]), txtProfitCenter.Text,
                                                        txtAssetCode.Text, Convert.ToString(itemMR.Rows[0]["REQUISITIONER"]), txtTrackingNO.Text, txtItemText.Text, "", Convert.ToString(itemMR.Rows[0]["ITEMTEXT"]), txtSku.Text,
                                                        txtItemMake.Text, txtItemModel.Text, txtItemDispName.Text, txtItemDispMRP.Text, txtItemValueLimit.Text, txtItemMaxStkQty.Text,
                                                        txtItemHSN.Text, txtItemHSNGroup.Text, txtItemHSNGroupDesc.Text, txtItemStatus.Text, txtItemCondType.Text, "", Convert.ToString(itemsr[j]));

                                                    ViewState["ItemData"] = MRdt;
                                                }
                                                else
                                                {
                                                    MRdt.Rows.Add("1", Convert.ToString(itemMR.Rows[0]["ITEMCODE"]), txtItemDesc.Text, txtItemId.Text, txtItemGroup.Text, txtItemGroupId.Text, Convert.ToString(itemMR.Rows[0]["ITEMUOM"]),
                                                        Convert.ToString(itemMR.Rows[0]["UOMID"]), Convert.ToString(itemMR.Rows[0]["ITEMQTY"]), Convert.ToString(itemMR.Rows[0]["ITEMRATE"]), Convert.ToString(itemMR.Rows[0]["ITEMAMOUNT"]), DateTime.Now.ToShortDateString(), txtGLCode.Text, Convert.ToString(itemMR.Rows[0]["COSTCENTER"]),
                                                        ddlFromPlant.SelectedItem.Text, ddlFromPlant.SelectedValue, ddlFromLocation.SelectedItem.Text, ddlFromLocation.SelectedValue,
                                                        Convert.ToString(itemMR.Rows[0]["ITEMPLANTCD"]), Convert.ToString(itemMR.Rows[0]["ITEMPLANTID"]), Convert.ToString(itemMR.Rows[0]["ITEMLOCCD"]), Convert.ToString(itemMR.Rows[0]["LOCCDID"]), txtProfitCenter.Text,
                                                        txtAssetCode.Text, Convert.ToString(itemMR.Rows[0]["REQUISITIONER"]), txtTrackingNO.Text, txtItemText.Text, "", Convert.ToString(itemMR.Rows[0]["ITEMTEXT"]), txtSku.Text,
                                                        txtItemMake.Text, txtItemModel.Text, txtItemDispName.Text, txtItemDispMRP.Text, txtItemValueLimit.Text, txtItemMaxStkQty.Text,
                                                        txtItemHSN.Text, txtItemHSNGroup.Text, txtItemHSNGroupDesc.Text, txtItemStatus.Text, txtItemCondType.Text, "", Convert.ToString(itemsr[j]));

                                                    ViewState["ItemData"] = MRdt;
                                                }
                                            }
                                        }
                                        grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                                        grvListItem.DataBind();
                                        EmptyString();
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

                                string DOCNO = objMainClass.InsertMaterialIssue(ddlDoctype.SelectedItem.Text, txtDocNo.Text, txtDocDt.Text, txtRefNo.Text, txtRemarks.Text, grvListItem, Convert.ToString(Session["USERID"]), objMainClass.intCmpId, Convert.ToString(Session["QMRNO"]), 5);
                                if (DOCNO != "")
                                {
                                    //string[] itemsr = Convert.ToString(Session["MRITEMSR"]).Split(',');
                                    //for (int j = 0; j < itemsr.Count(); j++)
                                    //{

                                    //    bool iResult = objMainClass.UpdateMRDtl(Convert.ToString(Session["QMRNO"]), Convert.ToString(itemsr[j]), DOCNO, objMainClass.intCmpId, 3);
                                    //}
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Document No. : " + DOCNO + " saved successfully.\");$('.close').click(function(){window.location.href ='ViewMaterialIssue.aspx?REQUESTFORM=IST' });", true);
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
                                if (plantArray[i].Trim() == ddlPLant.SelectedValue)
                                {
                                    PLantRights = ((Label)grvListItem.Rows[0].FindControl("lblPlantID")).Text;
                                }
                            }

                            if (PLantRights.Length > 0)
                            {

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
        #endregion

        #region ISTITEMDETAILMETHODANDEVENTS

        #region METHODS
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
                dtColumn.ColumnName = "ITEMRATE";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMAMOUNT";
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

                ///
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
                dtColumn.ColumnName = "REQUISITIONER";
                dtItem.Columns.Add(dtColumn);

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
                dtColumn.ColumnName = "REFSRNO";
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
                string FROMPLantRights = string.Empty;
                string[] FromplantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                for (int i = 0; i < FromplantArray.Count(); i++)
                {
                    if (FromplantArray[i].Trim() == ddlFromPlant.SelectedValue)
                    {
                        FROMPLantRights = ddlFromPlant.SelectedValue;
                    }
                }

                if (FROMPLantRights.Length > 0)
                {
                    j = "OK";
                }
            }
            else
            {
                j = "You do not have plant rights. ";
            }

            if (j == "OK")
            {
                Decimal bal = objMainClass.SP_GET_STOCK_FUNCTION(objMainClass.intCmpId, objMainClass.getFinYear(objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true)), txtItemCode.Text, objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true), ddlFromPlant.SelectedValue, ddlFromLocation.SelectedValue);
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
            //txtProfitCenter.Text = string.Empty;
            txtItemText.Text = string.Empty;
            txtItemDesc.Text = string.Empty;
            txtItemQty.Text = string.Empty;
            //txtCostCenter.Text = string.Empty;
            txtTrackingNO.Text = string.Empty;
            txtRemark.Text = string.Empty;
            LoadPlantCostCenter();
            //ddlUOM.SelectedValue = string.Empty;
            //ddlPLant.SelectedValue = string.Empty;
            //ddlLocation.SelectedValue = string.Empty;
            //txtCostCenter.Text = string.Empty;
            //txtAssetCode.Text = string.Empty;
        }

        private void LoadPlantCostCenter()
        {
            try
            {
                DataTable dt = objMainClass.GetPRFCost(ddlFromLocation.SelectedValue, Convert.ToString(Session["USERID"]));
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
        #endregion


        #region EVENTS
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

        protected void ddlFromPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            objBindDDL.FillLocationByPlantCd(ddlFromLocation, ddlFromPlant.SelectedValue);

            try
            {
                if (Session["USERID"] != null)
                {
                    string PLantRights = string.Empty;
                    string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                    for (int i = 0; i < plantArray.Count(); i++)
                    {
                        if (plantArray[i].Trim() == ddlFromPlant.SelectedValue)
                        {
                            PLantRights = ddlFromPlant.SelectedValue;
                        }
                    }

                    if (PLantRights.Length > 0)
                    {
                        DataTable dt = objMainClass.GetPRFCost(ddlFromPlant.SelectedValue, Convert.ToString(Session["USERID"]));
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
                        ddlFromPlant.SelectedValue = plantArray[0];
                        ddlFromPlant.Focus();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have plant rights.');$('#txtJobId').focus();", true);
                        ddlFromPlant.SelectedValue = plantArray[0];
                        ddlFromPlant.Focus();
                        objBindDDL.FillLocationByPlantCd(ddlFromLocation, ddlFromPlant.SelectedValue);
                        ddlFromLocation.SelectedIndex = 1;


                    }
                    objBindDDL.FillCostCenter(ddlCostCenter, ddlFromPlant.SelectedValue, ddlFromLocation.SelectedValue);
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
                        //DataTable dt = objMainClass.GetPRFCost(ddlPLant.SelectedValue, Convert.ToString(Session["USERID"]));
                        //if (dt.Rows.Count > 0)
                        //{
                        //    txtProfitCenter.Text = Convert.ToString(dt.Rows[0]["PRFCNT"]);
                        //    txtCostCenter.Text = Convert.ToString(dt.Rows[0]["CSTCENTCD"]);
                        //}
                        //else
                        //{
                        //    txtProfitCenter.Text = "1000";
                        //    txtCostCenter.Text = "1000";
                        //}
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
                                MRdt.Rows.Add(id, txtItemCode.Text, txtItemDesc.Text, txtItemId.Text, txtItemGroup.Text, txtItemGroupId.Text, ddlUOM.SelectedItem.Text,
                                    ddlUOM.SelectedValue, txtItemQty.Text, "1", "", DateTime.Now.ToShortDateString(), txtGLCode.Text, ddlCostCenter.SelectedValue,
                                    ddlFromPlant.SelectedItem.Text, ddlFromPlant.SelectedValue, ddlFromLocation.SelectedItem.Text, ddlFromLocation.SelectedValue,
                                    ddlPLant.SelectedItem.Text, ddlPLant.SelectedValue, ddlLocation.SelectedItem.Text, ddlLocation.SelectedValue, txtProfitCenter.Text,
                                    txtAssetCode.Text, "", txtTrackingNO.Text, txtRemark.Text, "", txtRemark.Text,
                                    txtItemMake.Text, txtItemModel.Text, txtItemDispName.Text, txtItemDispMRP.Text, txtItemValueLimit.Text, txtItemMaxStkQty.Text,
                                    txtItemHSN.Text, txtItemHSNGroup.Text, txtItemHSNGroupDesc.Text, txtItemStatus.Text, txtItemCondType.Text, "");

                                ViewState["ItemData"] = MRdt;

                            }
                            else
                            {
                                MRdt.Rows.Add("1", txtItemCode.Text, txtItemDesc.Text, txtItemId.Text, txtItemGroup.Text, txtItemGroupId.Text, ddlUOM.SelectedItem.Text,
                                    ddlUOM.SelectedValue, txtItemQty.Text, "1", "", DateTime.Now.ToShortDateString(), txtGLCode.Text, ddlCostCenter.SelectedValue,
                                    ddlFromPlant.SelectedItem.Text, ddlFromPlant.SelectedValue, ddlFromLocation.SelectedItem.Text, ddlFromLocation.SelectedValue,
                                    ddlPLant.SelectedItem.Text, ddlPLant.SelectedValue, ddlLocation.SelectedItem.Text, ddlLocation.SelectedValue, txtProfitCenter.Text,
                                    txtAssetCode.Text, "", txtTrackingNO.Text, txtRemark.Text, "", txtRemark.Text,
                                    txtItemMake.Text, txtItemModel.Text, txtItemDispName.Text, txtItemDispMRP.Text, txtItemValueLimit.Text, txtItemMaxStkQty.Text,
                                    txtItemHSN.Text, txtItemHSNGroup.Text, txtItemHSNGroupDesc.Text, txtItemStatus.Text, txtItemCondType.Text, "");

                                ViewState["ItemData"] = MRdt;


                            }

                            grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                            grvListItem.DataBind();
                            EmptyString();
                        }
                        else if (Convert.ToString(Session["savedet"]) == "Update Item")
                        {
                            DataTable dt = (DataTable)ViewState["ItemData"];
                            //DataRow dr = new DataRow();
                            DataTable ddt = (DataTable)ViewState["ItemData"];
                            DataRow dr = ddt.Select("ID='" + lblPRSRNO.Text + "'")[0];
                            dr[1] = txtItemCode.Text;
                            dr[2] = txtItemDesc.Text;
                            dr[3] = txtItemId.Text;
                            dr[4] = txtItemGroup.Text;
                            dr[5] = txtItemGroupId.Text;
                            dr[6] = ddlUOM.SelectedItem.Text;
                            dr[7] = ddlUOM.SelectedValue;
                            dr[8] = txtItemQty.Text;

                            dr[9] = "1";
                            dr[10] = "";
                            dr[11] = DateTime.Now.ToShortDateString();

                            dr[12] = txtGLCode.Text;
                            dr[13] = ddlCostCenter.SelectedValue;// txtCostCenter.Text;

                            dr[14] = ddlFromPlant.SelectedItem.Text;
                            dr[15] = ddlFromPlant.SelectedValue;
                            dr[16] = ddlFromLocation.SelectedItem.Text;
                            dr[17] = ddlFromLocation.SelectedValue;

                            dr[18] = ddlPLant.SelectedItem.Text;
                            dr[19] = ddlPLant.SelectedValue;
                            dr[20] = ddlLocation.SelectedItem.Text;
                            dr[21] = ddlLocation.SelectedValue;


                            dr[22] = txtProfitCenter.Text;
                            dr[23] = txtAssetCode.Text;
                            dr[24] = "";
                            dr[25] = txtTrackingNO.Text;
                            dr[26] = txtRemark.Text;
                            dr[27] = "";
                            dr[28] = txtItemText.Text;
                            //dr[25] = txtSku.Text;
                            //dr[26] = ;
                            dr[29] = txtSku.Text;
                            dr[30] = txtItemMake.Text;
                            dr[31] = txtItemModel.Text;
                            dr[32] = txtItemDispName.Text;
                            dr[33] = txtItemDispMRP.Text;
                            dr[34] = txtItemValueLimit.Text;
                            dr[35] = txtItemMaxStkQty.Text;
                            dr[36] = txtItemHSN.Text;
                            dr[37] = txtItemHSNGroup.Text;
                            dr[38] = txtItemHSNGroupDesc.Text;
                            dr[39] = txtItemCondType.Text;
                            dr[40] = txtItemStatus.Text;
                            dr[41] = "";

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
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void grvListItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Session["MRITEMSR"] != null && Convert.ToString(Session["MRITEMSR"]) != "" && Convert.ToString(Session["MRITEMSR"]) != string.Empty)
                {
                    Label lblFromPlantID = (e.Row.FindControl("lblFromPlantID") as Label);
                    Label lblLocationCDID = (e.Row.FindControl("lblLocationCDID") as Label);
                    Label lblItemCode = (e.Row.FindControl("lblItemCode") as Label);
                    Label lblQty = (e.Row.FindControl("lblQty") as Label);

                    decimal bal;
                    if (lblFromPlantID.Text == "1001")
                    {
                        if (lblLocationCDID.Text == "WS19")
                        {
                            bal = objMainClass.SP_GET_STOCK_FUNCTION(objMainClass.intCmpId, objMainClass.getFinYear(objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true)), lblItemCode.Text, objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true), lblFromPlantID.Text, "TS01");
                        }
                        else
                        {
                            bal = objMainClass.SP_GET_STOCK_FUNCTION(objMainClass.intCmpId, objMainClass.getFinYear(objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true)), lblItemCode.Text, objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true), lblFromPlantID.Text, "M001");
                        }


                    }
                    else
                    {
                        bal = objMainClass.SP_GET_STOCK_FUNCTION(objMainClass.intCmpId, objMainClass.getFinYear(objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true)), lblItemCode.Text, objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true), lblFromPlantID.Text, "MS01");
                    }

                    if (bal >= Convert.ToDecimal(lblQty.Text))
                    {

                    }
                    else
                    {
                        lblQty.Text = Convert.ToString(bal);
                    }
                }
                else
                {
                    Label lblItemCode = (e.Row.FindControl("lblItemCode") as Label);
                    Label lblQty = (e.Row.FindControl("lblQty") as Label);
                    decimal bal;
                    bal = objMainClass.SP_GET_STOCK_FUNCTION(objMainClass.intCmpId, objMainClass.getFinYear(objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true)), lblItemCode.Text, objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true), ddlFromPlant.SelectedValue, ddlFromLocation.SelectedValue);
                    if (bal >= Convert.ToDecimal(lblQty.Text))
                    {

                    }
                    else
                    {
                        lblQty.Text = Convert.ToString(bal);
                    }
                }
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
                txtRemark.Text = lblItemText.Text;
                txtItemDesc.Text = lblItemDesc.Text;
                txtItemQty.Text = lblQty.Text;
                ddlUOM.SelectedValue = lblUOMID.Text;
                txtTrackingNO.Text = lblTrackNo.Text;
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

        #region ITEMMODELEVENTS
        protected void lnkOpenPoup_Click(object sender, EventArgs e)
        {
            objBindDDL.FillItemCat(ddlpopCategory);
            objBindDDL.FillItemGrp(ddlpopGroup);
            objBindDDL.FillItemSubGrp(ddlpopSubGroup);
            objBindDDL.FillBrand(ddlpopMake, 0);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-item').modal();", true);
        }

        protected void ddlpopMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            objBindDDL.FillModel(ddlpopModel, ddlpopMake.SelectedValue);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-item').modal();", true);
        }

        protected void grvPopItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtItemCode.Text = Convert.ToString(grvPopItem.SelectedRow.Cells[1].Text);
            txtItemCode_TextChanged(1, e);
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

        #endregion

        #endregion

        #endregion


        protected void ddlFromLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                objBindDDL.FillCostCenter(ddlCostCenter, ddlFromPlant.SelectedValue, ddlFromLocation.SelectedValue);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}