using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net
{
    public partial class CreatePR : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        DataTable dtItem = new DataTable();

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


                        if (Request.QueryString.Count > 0)
                        {
                            if (Convert.ToString(Request.QueryString["PRNO"]) != null && Convert.ToString(Request.QueryString["PRNO"]) != string.Empty && Convert.ToString(Request.QueryString["PRNO"]) != "")
                            {
                                Session["EditPRNo"] = Convert.ToString(Request.QueryString["PRNO"]);

                            }
                            else if (Convert.ToString(Request.QueryString["FormName"]) == "FromReq")
                            {
                                Session["ReqNo"] = Convert.ToString(Request.QueryString["ReqNo"]);
                            }
                            else if (Convert.ToString(Request.QueryString["ITEMSR"]) != null && Convert.ToString(Request.QueryString["ITEMSR"]) != string.Empty && Convert.ToString(Request.QueryString["ITEMSR"]) != "")
                            {
                                Session["MRITEMSR"] = Convert.ToString(Request.QueryString["ITEMSR"]);
                                Session["QMRNO"] = Convert.ToString(Request.QueryString["MRNO"]);

                            }
                            Response.Redirect(Request.Url.AbsolutePath, false);
                        }
                        else
                        {
                            objBindDDL.FillDocType(ddlDoctype, "PR");
                            ddlDoctype.SelectedIndex = 1;
                            ddlDoctype.Enabled = false;

                            objBindDDL.FillDetparment(ddlDepartment);
                            ddlDepartment.SelectedValue = "5";


                            objBindDDL.FillPlant(ddlPLant);
                            string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                            ddlPLant.SelectedValue = plantArray[0];
                            objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);
                            ddlLocation.SelectedIndex = 1;
                            objBindDDL.FillUOM(ddlUOM);
                            //ddlUOM.SelectedIndex = 1;
                            txtPRNO.Text = objMainClass.MAXPRNO(ddlDoctype.SelectedValue, "PR");
                            //txtPRDATE.Text = objMainClass.setDateFormat(Convert.ToDateTime(DateTime.Now).ToShortDateString(), true);
                            txtPRDATE.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                            txtDeliveryDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                            DataTable dt = objMainClass.GetPRFCost(ddlPLant.SelectedValue, Convert.ToString(Session["USERID"]));
                            txtProfitCenter.Text = Convert.ToString(dt.Rows[0]["PRFCNT"]);
                            //txtCostCenter.Text = Convert.ToString(dt.Rows[0]["CSTCENTCD"]);

                            objBindDDL.FillCostCenter(ddlCostCenter, ddlPLant.SelectedValue, ddlLocation.SelectedValue);

                            Session["savedet"] = "Save Item";
                            Session["saveall"] = "Save All";
                            SetUpGrid();

                            objBindDDL.FillItemCat(ddlpopCategory);
                            objBindDDL.FillItemGrp(ddlpopGroup);
                            objBindDDL.FillItemSubGrp(ddlpopSubGroup);
                            objBindDDL.FillBrand(ddlpopMake, 0);

                            objBindDDL.FillItemCat(ddlNewCategory);
                            ddlNewCategory.SelectedValue = "2";
                            objBindDDL.FillItemGrp(ddlNewGroup);
                            ddlNewGroup.SelectedValue = "3";
                            objBindDDL.FillItemSubGrp(ddlNewSubGroup);
                            ddlNewSubGroup.SelectedValue = "83";


                            if (Session["EditPRNo"] != null && Convert.ToString(Session["EditPRNo"]) != "" && Convert.ToString(Session["EditPRNo"]) != string.Empty)
                            {
                                DataTable EditDt = new DataTable();
                                DataTable dtPR = new DataTable();
                                dtPR = objMainClass.SelectPRMST(Convert.ToString(Session["EditPRNo"]), 1);
                                EditDt = objMainClass.SelectPRDetail(Convert.ToString(Session["EditPRNo"]), 1, 0, string.Empty);
                                if (EditDt.Rows.Count > 0)
                                {
                                    ddlDoctype.SelectedValue = Convert.ToString(dtPR.Rows[0]["PRTYPE"]);
                                    txtPRNO.Text = Convert.ToString(dtPR.Rows[0]["PRNO"]);
                                    txtPRDATE.Text = Convert.ToDateTime(dtPR.Rows[0]["PRDT"]).ToShortDateString();
                                    txtREMARKS.Text = Convert.ToString(dtPR.Rows[0]["REMARK"]);
                                    ddlDepartment.SelectedValue = Convert.ToString(dtPR.Rows[0]["DEPTID"]);

                                    txtPRNO.ReadOnly = true;
                                    txtPRDATE.ReadOnly = true;

                                    grvListItem.DataSource = EditDt;
                                    grvListItem.DataBind();
                                    ViewState["ItemData"] = EditDt;

                                    Session["saveall"] = "Update All";
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Record not found. '" + Convert.ToString(Session["EditPRNo"]) + "'.');$('.close').click(function(){window.location.href ='ViewPR.aspx' });", true);
                                }
                            }
                            if (Session["ReqNo"] != null && Convert.ToString(Session["ReqNo"]) != "" && Convert.ToString(Session["ReqNo"]) != string.Empty)
                            {
                                DataTable dtPR = new DataTable();
                                dtPR = objMainClass.GetPartReqData(1, "", Convert.ToString(Session["ReqNo"]), "", "", "", 1, "", "");
                                txtPartReqNo.Text = Convert.ToString(Session["ReqNo"]);
                                txtItemCode.Text = Convert.ToString(dtPR.Rows[0]["ITEMCODE"]); //ITEMCODE
                                txtItemCode_TextChanged(1, e);
                                txtItemRate.Text = "1";
                                txtItemQty.Text = Convert.ToString(Convert.ToInt32(dtPR.Rows[0]["QTY"])); //QTY
                                txtItemQty_TextChanged(1, e);
                                ddlUOM.SelectedValue = Convert.ToString(dtPR.Rows[0]["UOM"]); //UOM
                                txtTrackNo.Text = Convert.ToString(dtPR.Rows[0]["JobId"]); //JobId
                                txtTrackNo_TextChanged(1, e);
                                ddlPLant.SelectedValue = Convert.ToString(dtPR.Rows[0]["PLANTCD"]);
                                ddlLocation.SelectedValue = Convert.ToString(dtPR.Rows[0]["LOCCD"]);
                                txtRequisitioner.Text = Convert.ToString(dtPR.Rows[0]["REQBYNAME"]);
                            }
                            if (Session["MRITEMSR"] != null && Convert.ToString(Session["MRITEMSR"]) != "" && Convert.ToString(Session["MRITEMSR"]) != string.Empty)
                            {
                                string[] itemsr = Convert.ToString(Session["MRITEMSR"]).Split(',');
                                for (int j = 0; j < itemsr.Count(); j++)
                                {

                                    DataTable itemMR = new DataTable();
                                    itemMR = objMainClass.SelectMRDetail(Convert.ToString(Session["QMRNO"]), objMainClass.intCmpId, 2, Convert.ToString(itemsr[j]));

                                    if (itemMR.Rows.Count > 0)
                                    {

                                        ddlDepartment.SelectedValue = Convert.ToString(itemMR.Rows[0]["DEPTID"]);



                                        DataTable Itemdt = new DataTable();
                                        Itemdt = objMainClass.GetItemDetails(Convert.ToString(itemMR.Rows[0]["ITEMCODE"]), Convert.ToString(itemMR.Rows[0]["ITEMPLANTID"]), Convert.ToString(itemMR.Rows[0]["LOCCDID"]));
                                        if (Itemdt.Rows.Count > 0)
                                        {
                                            if (Convert.ToString(Itemdt.Rows[0]["status"]) == "1")
                                            {
                                                txtItemDesc.Text = Convert.ToString(Itemdt.Rows[0]["itemdesc"]);
                                                txtGLCode.Text = Convert.ToString(Itemdt.Rows[0]["glcode"]);
                                                txtItemGroup.Text = Convert.ToString(Itemdt.Rows[0]["itemgrp"]);
                                                txtOnHandStock.Text = Convert.ToString(Itemdt.Rows[0]["maxstockqty"]);
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




                                                DataTable MRdt = (DataTable)ViewState["ItemData"];
                                                if (grvListItem.Rows.Count > 0)
                                                {
                                                    DataRow lastRow = MRdt.Rows[MRdt.Rows.Count - 1];
                                                    int id = Convert.ToInt32(lastRow["ID"]) + 1;
                                                    MRdt.Rows.Add(id, Convert.ToString(itemMR.Rows[0]["ITEMCODE"]), txtItemDesc.Text, txtItemId.Text, txtItemGroup.Text, txtItemGroupId.Text, Convert.ToString(itemMR.Rows[0]["ITEMUOM"]),
                                                        Convert.ToString(itemMR.Rows[0]["UOMID"]), Convert.ToString(itemMR.Rows[0]["ITEMQTY"]), Convert.ToString(itemMR.Rows[0]["ITEMRATE"]), Convert.ToString(itemMR.Rows[0]["ITEMAMOUNT"]), DateTime.Now.ToShortDateString(), txtGLCode.Text, Convert.ToString(itemMR.Rows[0]["COSTCENTER"]),
                                                        Convert.ToString(itemMR.Rows[0]["ITEMPLANTCD"]), Convert.ToString(itemMR.Rows[0]["ITEMPLANTID"]), Convert.ToString(itemMR.Rows[0]["ITEMLOCCD"]), Convert.ToString(itemMR.Rows[0]["LOCCDID"]), txtProfitCenter.Text,
                                                        txtAssetCode.Text, Convert.ToString(itemMR.Rows[0]["REQUISITIONER"]), txtTrackNo.Text, Convert.ToString(itemMR.Rows[0]["ITEMTEXT"]), txtPartReqNo.Text, txtSku.Text,
                                                        txtItemMake.Text, txtItemModel.Text, txtItemDispName.Text, txtItemDispMRP.Text, txtItemValueLimit.Text, txtItemMaxStkQty.Text,
                                                        txtItemHSN.Text, txtItemHSNGroup.Text, txtItemHSNGroupDesc.Text, txtItemStatus.Text, txtItemCondType.Text, txtOnHandStock.Text, Convert.ToString(itemMR.Rows[0]["ID"]));

                                                    ViewState["ItemData"] = MRdt;

                                                }
                                                else
                                                {
                                                    MRdt.Rows.Add("1", Convert.ToString(itemMR.Rows[0]["ITEMCODE"]), txtItemDesc.Text, txtItemId.Text, txtItemGroup.Text, txtItemGroupId.Text, Convert.ToString(itemMR.Rows[0]["ITEMUOM"]),
                                                        Convert.ToString(itemMR.Rows[0]["UOMID"]), Convert.ToString(itemMR.Rows[0]["ITEMQTY"]), Convert.ToString(itemMR.Rows[0]["ITEMRATE"]), Convert.ToString(itemMR.Rows[0]["ITEMAMOUNT"]), DateTime.Now.ToShortDateString(), txtGLCode.Text, Convert.ToString(itemMR.Rows[0]["COSTCENTER"]),
                                                        Convert.ToString(itemMR.Rows[0]["ITEMPLANTCD"]), Convert.ToString(itemMR.Rows[0]["ITEMPLANTID"]), Convert.ToString(itemMR.Rows[0]["ITEMLOCCD"]), Convert.ToString(itemMR.Rows[0]["LOCCDID"]), txtProfitCenter.Text,
                                                        txtAssetCode.Text, Convert.ToString(itemMR.Rows[0]["REQUISITIONER"]), txtTrackNo.Text, Convert.ToString(itemMR.Rows[0]["ITEMTEXT"]), txtPartReqNo.Text, txtSku.Text,
                                                        txtItemMake.Text, txtItemModel.Text, txtItemDispName.Text, txtItemDispMRP.Text, txtItemValueLimit.Text, txtItemMaxStkQty.Text,
                                                        txtItemHSN.Text, txtItemHSNGroup.Text, txtItemHSNGroupDesc.Text, txtItemStatus.Text, txtItemCondType.Text, txtOnHandStock.Text, Convert.ToString(itemMR.Rows[0]["ID"]));

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

                //dtColumn = new DataColumn();
                //dtColumn.ColumnName = "ITEMREMARKS";
                //dtItem.Columns.Add(dtColumn);

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
                dtColumn.ColumnName = "MRID";
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




















            // objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);


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

        protected void txtItemQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtAmount.Text = Convert.ToString(Convert.ToDecimal(txtItemRate.Text == "" ? "0" : txtItemRate.Text) * Convert.ToDecimal(txtItemQty.Text == "" ? "0" : txtItemQty.Text));

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
                DataTable dt = new DataTable();
                dt = objMainClass.GetJobDetails(txtTrackNo.Text);
                if (dt.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('PR already created for the same Job!  PR NO.: " + dt.Rows[0]["PRNO"] + "  PR Dt: " + dt.Rows[0]["PRDT"] + "');", true);
                }
                txtTrackNo.Text = objMainClass.strConvertZeroPadding(txtTrackNo.Text);
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
                            if (Convert.ToString(Session["savedet"]) == "Save Item")
                            {
                                DataTable dt = (DataTable)ViewState["ItemData"];
                                if (grvListItem.Rows.Count > 0)
                                {
                                    DataRow lastRow = dt.Rows[dt.Rows.Count - 1];
                                    int id = Convert.ToInt32(lastRow["ID"]) + 1;
                                    //dt.Rows.Add(id, txtItemCode.Text, txtItemDesc.Text, txtItemId.Text, txtItemGroup.Text, txtItemGroupId.Text, ddlUOM.SelectedItem.Text,
                                    //    ddlUOM.SelectedValue, txtItemQty.Text, txtItemRate.Text, txtAmount.Text, txtDeliveryDate.Text, txtGLCode.Text, ddlCostCenter.SelectedValue,
                                    //    ddlPLant.SelectedItem.Text, ddlPLant.SelectedValue, ddlLocation.SelectedItem.Text, ddlLocation.SelectedValue, txtProfitCenter.Text,
                                    //    txtAssetCode.Text, txtRequisitioner.Text, txtTrackNo.Text, txtItemRemarks.Text, txtPartReqNo.Text, txtItemRemarks.Text, txtSku.Text,
                                    //    txtItemMake.Text, txtItemModel.Text, txtItemDispName.Text, txtItemDispMRP.Text, txtItemValueLimit.Text, txtItemMaxStkQty.Text,
                                    //    txtItemHSN.Text, txtItemHSNGroup.Text, txtItemHSNGroupDesc.Text, txtItemStatus.Text, txtItemCondType.Text, txtOnHandStock.Text, "");

                                    dt.Rows.Add(id, txtItemCode.Text, txtItemDesc.Text, txtItemId.Text, txtItemGroup.Text, txtItemGroupId.Text, ddlUOM.SelectedItem.Text,
                                        ddlUOM.SelectedValue, txtItemQty.Text, txtItemRate.Text, txtAmount.Text, txtDeliveryDate.Text, txtGLCode.Text, ddlCostCenter.SelectedValue,
                                        ddlPLant.SelectedItem.Text, ddlPLant.SelectedValue, ddlLocation.SelectedItem.Text, ddlLocation.SelectedValue, txtProfitCenter.Text,
                                        txtAssetCode.Text, txtRequisitioner.Text, txtTrackNo.Text, txtItemRemarks.Text, txtPartReqNo.Text, txtSku.Text,
                                        txtItemMake.Text, txtItemModel.Text, txtItemDispName.Text, txtItemDispMRP.Text, txtItemValueLimit.Text, txtItemMaxStkQty.Text,
                                        txtItemHSN.Text, txtItemHSNGroup.Text, txtItemHSNGroupDesc.Text, txtItemStatus.Text, txtItemCondType.Text, txtOnHandStock.Text, "");

                                    ViewState["ItemData"] = dt;

                                }
                                else
                                {
                                    //dt.Rows.Add("1", txtItemCode.Text, txtItemDesc.Text, txtItemId.Text, txtItemGroup.Text, txtItemGroupId.Text, ddlUOM.SelectedItem.Text,
                                    //    ddlUOM.SelectedValue, txtItemQty.Text, txtItemRate.Text, txtAmount.Text, txtDeliveryDate.Text, txtGLCode.Text, ddlCostCenter.SelectedValue,
                                    //    ddlPLant.SelectedItem.Text, ddlPLant.SelectedValue, ddlLocation.SelectedItem.Text, ddlLocation.SelectedValue, txtProfitCenter.Text,
                                    //    txtAssetCode.Text, txtRequisitioner.Text, txtTrackNo.Text, txtItemRemarks.Text, txtPartReqNo.Text, txtItemRemarks.Text, txtSku.Text,
                                    //    txtItemMake.Text, txtItemModel.Text, txtItemDispName.Text, txtItemDispMRP.Text, txtItemValueLimit.Text, txtItemMaxStkQty.Text,
                                    //    txtItemHSN.Text, txtItemHSNGroup.Text, txtItemHSNGroupDesc.Text, txtItemStatus.Text, txtItemCondType.Text, txtOnHandStock.Text, "");

                                    dt.Rows.Add("1", txtItemCode.Text, txtItemDesc.Text, txtItemId.Text, txtItemGroup.Text, txtItemGroupId.Text, ddlUOM.SelectedItem.Text,
                                        ddlUOM.SelectedValue, txtItemQty.Text, txtItemRate.Text, txtAmount.Text, txtDeliveryDate.Text, txtGLCode.Text, ddlCostCenter.SelectedValue,
                                        ddlPLant.SelectedItem.Text, ddlPLant.SelectedValue, ddlLocation.SelectedItem.Text, ddlLocation.SelectedValue, txtProfitCenter.Text,
                                        txtAssetCode.Text, txtRequisitioner.Text, txtTrackNo.Text, txtItemRemarks.Text, txtPartReqNo.Text, txtSku.Text,
                                        txtItemMake.Text, txtItemModel.Text, txtItemDispName.Text, txtItemDispMRP.Text, txtItemValueLimit.Text, txtItemMaxStkQty.Text,
                                        txtItemHSN.Text, txtItemHSNGroup.Text, txtItemHSNGroupDesc.Text, txtItemStatus.Text, txtItemCondType.Text, txtOnHandStock.Text, "");

                                    ViewState["ItemData"] = dt;


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
                                DataRow dr = ddt.Select("ID = '" + lblPRSRNO.Text + "'")[0];
                                dr[1] = txtItemCode.Text;
                                dr[2] = txtItemDesc.Text;
                                dr[3] = txtItemId.Text;
                                dr[4] = txtItemGroup.Text;
                                dr[5] = txtItemGroupId.Text;
                                dr[6] = ddlUOM.SelectedItem.Text;
                                dr[7] = ddlUOM.SelectedValue;
                                dr[8] = txtItemQty.Text;
                                dr[9] = txtItemRate.Text;
                                dr[10] = txtAmount.Text;
                                dr[11] = txtDeliveryDate.Text;
                                dr[12] = txtGLCode.Text;
                                dr[13] = ddlCostCenter.SelectedValue;//txtCostCenter.Text;
                                dr[14] = ddlPLant.SelectedItem.Text;
                                dr[15] = ddlPLant.SelectedValue;
                                dr[16] = ddlLocation.SelectedItem.Text;
                                dr[17] = ddlLocation.SelectedValue;
                                dr[18] = txtProfitCenter.Text;
                                dr[19] = txtAssetCode.Text;
                                dr[20] = txtRequisitioner.Text;
                                dr[21] = txtTrackNo.Text;
                                dr[22] = txtItemText.Text;
                                dr[23] = txtPartReqNo.Text;
                                //dr[24] = txtItemRemarks.Text;
                                //dr[25] = txtSku.Text;
                                //dr[26] = ;
                                dr[24] = txtSku.Text;
                                dr[25] = txtItemMake.Text;
                                dr[26] = txtItemModel.Text;
                                dr[27] = txtItemDispName.Text;
                                dr[28] = txtItemDispMRP.Text;
                                dr[29] = txtItemValueLimit.Text;
                                dr[30] = txtItemMaxStkQty.Text;
                                dr[31] = txtItemHSN.Text;
                                dr[32] = txtItemHSNGroup.Text;
                                dr[33] = txtItemHSNGroupDesc.Text;
                                dr[34] = txtItemCondType.Text;
                                dr[35] = txtItemStatus.Text;
                                dr[36] = txtOnHandStock.Text;
                                //if (grvListItem.Rows.Count > 0)
                                //{
                                //    DataRow lastRow = dt.Rows[dt.Rows.Count - 1];
                                //    int id = Convert.ToInt32(lastRow["ID"]) + 1;
                                //    dt.Rows.Add(id, txtItemCode.Text, txtItemDesc.Text, txtItemId.Text, txtItemGroup.Text, txtItemGroupId.Text, ddlUOM.SelectedItem.Text,
                                //        ddlUOM.SelectedValue, txtItemQty.Text, txtItemRate.Text, txtAmount.Text, txtDeliveryDate.Text, txtGLCode.Text, txtCostCenter.Text,
                                //        ddlPLant.SelectedItem.Text, ddlPLant.SelectedValue, ddlLocation.SelectedItem.Text, ddlLocation.SelectedValue, txtProfitCenter.Text,
                                //        txtAssetCode.Text, txtRequisitioner.Text, txtTrackNo.Text, txtItemText.Text, txtPartReqNo.Text, txtItemRemarks.Text, txtSku.Text,
                                //        txtItemMake.Text, txtItemModel.Text, txtItemDispName.Text, txtItemDispMRP.Text, txtItemValueLimit.Text, txtItemMaxStkQty.Text,
                                //        txtItemHSN.Text, txtItemHSNGroup.Text, txtItemHSNGroupDesc.Text, txtItemStatus.Text, txtItemCondType.Text, txtOnHandStock.Text);

                                //    ViewState["ItemData"] = dt;
                                //}
                                //else
                                //{
                                //    dt.Rows.Add("1", txtItemCode.Text, txtItemDesc.Text, txtItemId.Text, txtItemGroup.Text, txtItemGroupId.Text, ddlUOM.SelectedItem.Text,
                                //        ddlUOM.SelectedValue, txtItemQty.Text, txtItemRate.Text, txtAmount.Text, txtDeliveryDate.Text, txtGLCode.Text, txtCostCenter.Text,
                                //        ddlPLant.SelectedItem.Text, ddlPLant.SelectedValue, ddlLocation.SelectedItem.Text, ddlLocation.SelectedValue, txtProfitCenter.Text,
                                //        txtAssetCode.Text, txtRequisitioner.Text, txtTrackNo.Text, txtItemText.Text, txtPartReqNo.Text, txtItemRemarks.Text, txtSku.Text,
                                //        txtItemMake.Text, txtItemModel.Text, txtItemDispName.Text, txtItemDispMRP.Text, txtItemValueLimit.Text, txtItemMaxStkQty.Text,
                                //        txtItemHSN.Text, txtItemHSNGroup.Text, txtItemHSNGroupDesc.Text, txtItemStatus.Text, txtItemCondType.Text, txtOnHandStock.Text);

                                //    ViewState["ItemData"] = dt;


                                //}

                                grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                                grvListItem.DataBind();
                                Session["savedet"] = "Save Item";
                                EmptyString();
                            }
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

            if (maxstk >= newmax)
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
            if (maxstk >= Convert.ToDecimal(txtItemMaxStkQty.Text))
            {
                stk = 0;
            }
            else
            {
                stk = 1;
            }
            return stk;
        }

        private void BindGrid()
        {
            grvListItem.DataSource = (DataTable)ViewState["ItemData"];
            grvListItem.DataBind();
        }

        protected void txtEditItemCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtEditItemCode = (TextBox)sender;
                GridViewRow row = (GridViewRow)txtEditItemCode.NamingContainer;

                DropDownList ddlEditPlantCode = (DropDownList)row.FindControl("ddlEditPlantCode");

                DropDownList ddlEditLocation = (DropDownList)row.FindControl("ddlEditLocation");


                DataTable dt = new DataTable();
                dt = objMainClass.GetItemDetails(txtEditItemCode.Text, ddlEditPlantCode.SelectedValue, ddlEditLocation.SelectedValue);
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

        protected void btnPopup_Click(object sender, EventArgs e)
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

        protected void imgFind_Click(object sender, EventArgs e)
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
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-item').hide();", true);
        }

        protected void grvListItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "eDelete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                DataTable dt = (DataTable)ViewState["ItemData"];
                GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                Label lblMRID = (Label)row.FindControl("lblMRID");
                dt.Rows[row.RowIndex].Delete();
                ViewState["ItemData"] = dt;

                grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                grvListItem.DataBind();

                if (Session["MRITEMSR"] != null && Convert.ToString(Session["MRITEMSR"]) != "" && Convert.ToString(Session["MRITEMSR"]) != string.Empty)
                {
                    string[] MRID = (Session["MRITEMSR"]).ToString().Split(',');
                    MRID = MRID.Where(val => val != lblMRID.Text).ToArray();
                    Session["MRITEMSR"] = string.Join(",", MRID);
                }

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
                //Label lblRemarks = (Label)gRow.FindControl("lblRemarks");
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
                txtItemRate.Text = lblRate.Text;
                txtAmount.Text = lblAmount.Text;
                txtTrackNo.Text = lblTrackNo.Text;
                txtDeliveryDate.Text = lblDeliDate.Text;
                txtRequisitioner.Text = lblRequisitioner.Text;
                txtItemRemarks.Text = lblItemText.Text;
                ddlPLant.SelectedValue = lblPlantID.Text;
                objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);
                ddlLocation.SelectedValue = lblLocationCDID.Text;
                objBindDDL.FillCostCenter(ddlCostCenter, ddlPLant.SelectedValue, ddlLocation.SelectedValue);
                ddlCostCenter.SelectedValue = lblCostCenter.Text;
                txtPartReqNo.Text = lblPartReqNo.Text;
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
            //txtProfitCenter.Text = string.Empty;
            txtItemText.Text = string.Empty;
            txtItemDesc.Text = string.Empty;
            txtItemQty.Text = string.Empty;
            //ddlUOM.SelectedValue = string.Empty;
            txtOnHandStock.Text = string.Empty;
            txtItemRate.Text = string.Empty;
            txtAmount.Text = string.Empty;
            txtTrackNo.Text = string.Empty;
            txtDeliveryDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
            txtRequisitioner.Text = string.Empty;
            txtItemRemarks.Text = string.Empty;
            //ddlPLant.SelectedValue = string.Empty;
            //ddlLocation.SelectedValue = string.Empty;
            //txtCostCenter.Text = string.Empty;
            txtPartReqNo.Text = string.Empty;
            //txtAssetCode.Text = string.Empty;
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
                                string PR = objMainClass.SavePPR(ddlDoctype.SelectedValue, txtPRDATE.Text, txtREMARKS.Text, ddlDepartment.SelectedValue, grvListItem, Convert.ToString(Session["USERID"]), Convert.ToString(Session["MRITEMSR"]), Convert.ToString(Session["QMRNO"]));
                                if (PR != "")
                                {


                                    //String strCustContent = "";
                                    //strCustContent = fileread();
                                    //strCustContent = strCustContent.Replace("###Heading###", "New PR Created by User.");
                                    //strCustContent = strCustContent.Replace("###CreateBy###", Convert.ToString(Session["USERNAME"]));
                                    //strCustContent = strCustContent.Replace("###CreateDate###", txtPRDATE.Text);
                                    //strCustContent = strCustContent.Replace("###PRNO###", PR);
                                    //strCustContent = strCustContent.Replace("###Message###", "New PR created by user. Details are as per above.");
                                    //strCustContent = strCustContent.Replace("###LINK###", "http://14.98.132.190:360/MM/AprvPR.aspx");
                                    //strCustContent = strCustContent.Replace("###HREFLINK###", "http://14.98.132.190:360/MM/AprvPR.aspx");

                                    //DataTable dt = new DataTable();
                                    //dt = objMainClass.MailSenderReceiver("PR", 1, Convert.ToInt32(ddlDepartment.SelectedValue), ((Label)grvListItem.Rows[0].FindControl("lblPlantID")).Text);
                                    //string Reciever = string.Empty;
                                    //if (dt.Rows.Count > 0)
                                    //{

                                    //    for (int i = 0; i < dt.Rows.Count; i++)
                                    //    {

                                    //        if (Reciever == string.Empty)
                                    //        {
                                    //            Reciever = Convert.ToString(dt.Rows[i]["EMAILID"]);
                                    //        }
                                    //        else
                                    //        {
                                    //            Reciever = Reciever + ";" + Convert.ToString(dt.Rows[i]["EMAILID"]);
                                    //        }
                                    //    }
                                    //    objMainClass.SaveMailNotification(objMainClass.EmailID, objMainClass.Password, Convert.ToString(dt.Rows[0]["EMAILID"]), Reciever, "New PR Created", strCustContent, objMainClass.PORT, PR, Convert.ToString(Session["USERID"]), "MPR");


                                    //    //objMainClass.SendMail(strCustContent, "New PR Created", dt);
                                    //}
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Record save sucessfully.  PPR No. : " + PR + "\");$('.close').click(function(){window.location.href ='ViewPR.aspx' });", true);
                                    Session["EditPRNo"] = null;
                                    Session["ReqNo"] = null;
                                    Session["MRITEMSR"] = null;
                                    Session["QMRNO"] = null;
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


                                string PR = objMainClass.UpdatePPR(txtPRNO.Text, ddlDoctype.SelectedValue, txtPRDATE.Text, txtREMARKS.Text, ddlDepartment.SelectedValue, grvListItem, Convert.ToString(Session["USERID"]));
                                if (PR != "")
                                {
                                    //String strCustContent = "";
                                    //strCustContent = fileread();
                                    //strCustContent = strCustContent.Replace("###Heading###", "PR Updated by User.");
                                    //strCustContent = strCustContent.Replace("###CreateBy###", Convert.ToString(Session["USERNAME"]));
                                    //strCustContent = strCustContent.Replace("###CreateDate###", txtPRDATE.Text);
                                    //strCustContent = strCustContent.Replace("###PRNO###", PR);
                                    //strCustContent = strCustContent.Replace("###Message###", "PR updated by user. Details are as per above.");
                                    //strCustContent = strCustContent.Replace("###LINK###", "http://14.98.132.190:360/MM/AprvPR.aspx");
                                    //strCustContent = strCustContent.Replace("###HREFLINK###", "http://14.98.132.190:360/MM/AprvPR.aspx");

                                    //DataTable dt = new DataTable();
                                    //dt = objMainClass.MailSenderReceiver("PR", 1, Convert.ToInt32(ddlDepartment.SelectedValue), ((Label)grvListItem.Rows[0].FindControl("lblPlantID")).Text);
                                    //string Reciever = string.Empty;
                                    //if (dt.Rows.Count > 0)
                                    //{

                                    //    for (int i = 0; i < dt.Rows.Count; i++)
                                    //    {

                                    //        if (Reciever == string.Empty)
                                    //        {
                                    //            Reciever = Convert.ToString(dt.Rows[i]["EMAILID"]);
                                    //        }
                                    //        else
                                    //        {
                                    //            Reciever = Reciever + ";" + Convert.ToString(dt.Rows[i]["EMAILID"]);
                                    //        }
                                    //    }
                                    //    objMainClass.SaveMailNotification(objMainClass.EmailID, objMainClass.Password, Convert.ToString(dt.Rows[0]["EMAILID"]), Reciever, "PR Updated", strCustContent, objMainClass.PORT, PR, Convert.ToString(Session["USERID"]), "MPR");



                                    //    //objMainClass.SendMail(strCustContent, "PR Updated", dt);
                                    //}


                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Record updated sucessfully.  PPR No. : " + PR + "\");$('.close').click(function(){window.location.href ='ViewPR.aspx' });", true);
                                    Session["EditPRNo"] = null;
                                    Session["ReqNo"] = null;
                                    Session["MRITEMSR"] = null;
                                    Session["QMRNO"] = null;
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

        protected void lnkNewPartCode_Click(object sender, EventArgs e)
        {

            objBindDDL.FillItemCat(ddlNewCategory);
            ddlNewCategory.SelectedValue = "2";
            objBindDDL.FillItemGrp(ddlNewGroup);
            ddlNewGroup.SelectedValue = "3";
            objBindDDL.FillItemSubGrp(ddlNewSubGroup);
            ddlNewSubGroup.SelectedValue = "83";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Newitem').modal();", true);

        }

        protected void lnkNewItemSave_Click(object sender, EventArgs e)
        {
            //
            try
            {
                //bool iResult = objMainClass.SaveNewItem(txtNewItemName.Text, txtNewItemDesc.Text, ddlNewCategory.SelectedIndex > 0 ? ddlNewCategory.SelectedValue : "", ddlNewGroup.SelectedIndex > 0 ? ddlNewGroup.SelectedValue : "", ddlNewSubGroup.SelectedIndex > 0 ? ddlNewSubGroup.SelectedValue : "", Convert.ToString(Session["USERID"]));
                bool iResult = objMainClass.SaveNewItem(txtNewItemName.Text, txtNewItemDesc.Text, txtNewItemSpecification.Text, ddlNewCategory.SelectedValue, ddlNewGroup.SelectedValue, ddlNewSubGroup.SelectedValue, Convert.ToString(Session["USERID"]));
                if (iResult == true)
                {
                    String strCustContent = "";
                    strCustContent = fileread();
                    strCustContent = strCustContent.Replace("###Heading###", "Add New Itemcode ");
                    strCustContent = strCustContent.Replace("###CreateBy###", Convert.ToString(Session["USERNAME"]));
                    strCustContent = strCustContent.Replace("###CreateDate###", txtPRDATE.Text);
                    strCustContent = strCustContent.Replace("###PRNO###", "");
                    strCustContent = strCustContent.Replace("###Message###", "User requested to add new Itemcode. Please check and proceed further!");
                    //objMainClass.SendMail(strCustContent, "Add New Itemcode");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('New Item Code Request Inserted Successfully! ');", true);
                    txtNewItemName.Text = string.Empty;
                    txtNewItemDesc.Text = string.Empty;
                    objBindDDL.FillItemCat(ddlNewCategory);
                    ddlNewCategory.SelectedValue = "2";
                    objBindDDL.FillItemGrp(ddlNewGroup);
                    ddlNewGroup.SelectedValue = "3";
                    objBindDDL.FillItemSubGrp(ddlNewSubGroup);
                    ddlNewSubGroup.SelectedValue = "83";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Something went wrong. Please try again! ');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void txtPartReqNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    double lblQty = 0;
                    dt = objMainClass.GetPartReqData(1, "", txtPartReqNo.Text, "", "", "", 1, "", "");
                    if (dt.Rows.Count > 0)
                    {
                        DataTable dtDtl = new DataTable();
                        //dtDtl = objMainClass.GetItemDetailByID(objMainClass.intCmpId, Convert.ToString(dt.Rows[0]["ITEMID"]));
                        dtDtl = objMainClass.GetItemDetails(Convert.ToString(dt.Rows[0]["ITEMCODE"]), ddlPLant.SelectedValue, ddlLocation.SelectedValue);
                        if (dtDtl.Rows.Count > 0)
                        {


                            if (grvListItem.Rows.Count > 0)
                            {
                                foreach (GridViewRow item in grvListItem.Rows)
                                {
                                    Label partreqno = (Label)item.FindControl("lblPartReqNo");
                                    Label gridqty = (Label)item.FindControl("lblQty");
                                    if (partreqno.Text == txtPartReqNo.Text)
                                    {
                                        lblQty = lblQty + Convert.ToDouble(gridqty.Text);
                                    }
                                }


                                if (lblQty >= Convert.ToDouble(dt.Rows[0]["QTY"]))
                                {
                                    txtPartReqNo.Focus();
                                    txtPartReqNo.Text = string.Empty;
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Full Quantity are ordered for these Part Request!');", true);

                                    txtPartReqNo.Focus();
                                    txtPartReqNo.Text = string.Empty;
                                }
                                else
                                {
                                    txtItemCode.Text = Convert.ToString(dtDtl.Rows[0]["ITEMCODE"]);
                                    txtItemId.Text = Convert.ToString(dtDtl.Rows[0]["ITEMID"]);
                                    txtItemDesc.Text = Convert.ToString(dtDtl.Rows[0]["ITEMDESC"]);
                                    ddlPLant.SelectedValue = Convert.ToString(dt.Rows[0]["PLANTCD"]);
                                    ddlLocation.SelectedValue = Convert.ToString(dt.Rows[0]["LOCCD"]);
                                    txtItemQty.Text = Convert.ToString(dt.Rows[0]["QTY"]);
                                    ddlUOM.SelectedValue = Convert.ToString(dt.Rows[0]["UOM"]);
                                    txtTrackNo.Text = Convert.ToString(dt.Rows[0]["JOBID"]);
                                    txtRequisitioner.Text = Convert.ToString(dt.Rows[0]["REQBYNAME"]);
                                    txtItemGroupId.Text = Convert.ToString(dtDtl.Rows[0]["ITEMGRPID"]);
                                    txtItemGroup.Text = Convert.ToString(dtDtl.Rows[0]["ITEMGRP"]);
                                    txtGLCode.Text = Convert.ToString(dtDtl.Rows[0]["GLCODE"]);
                                    txtOnHandStock.Text = Convert.ToString(dtDtl.Rows[0]["MAXSTOCKQTY"]);
                                    txtSku.Text = Convert.ToString(dtDtl.Rows[0]["SKU"]);
                                    txtItemMake.Text = Convert.ToString(dtDtl.Rows[0]["MAKE"]);
                                    txtItemModel.Text = Convert.ToString(dtDtl.Rows[0]["MODEL"]);
                                    txtItemDispName.Text = Convert.ToString(dtDtl.Rows[0]["DISPNAME"]);
                                    txtItemDispMRP.Text = Convert.ToString(dtDtl.Rows[0]["MRP"]);
                                    txtItemValueLimit.Text = Convert.ToString(dtDtl.Rows[0]["VALUELIMIT"]);
                                    txtItemMaxStkQty.Text = Convert.ToString(dtDtl.Rows[0]["MAXSTKQTY"]);
                                    txtItemHSN.Text = Convert.ToString(dtDtl.Rows[0]["HSNGRP"]);
                                    txtItemHSNGroup.Text = Convert.ToString(dtDtl.Rows[0]["HSNGRPCODE"]);
                                    txtItemHSNGroupDesc.Text = Convert.ToString(dtDtl.Rows[0]["HSNGRPDESC"]);
                                    txtItemCondType.Text = Convert.ToString(dtDtl.Rows[0]["CONDTYPE"]);
                                    txtItemStatus.Text = Convert.ToString(dtDtl.Rows[0]["STATUS"]);
                                }
                            }
                            else
                            {
                                txtItemCode.Text = Convert.ToString(dtDtl.Rows[0]["ITEMCODE"]);
                                txtItemId.Text = Convert.ToString(dtDtl.Rows[0]["ITEMID"]);
                                txtItemDesc.Text = Convert.ToString(dtDtl.Rows[0]["ITEMDESC"]);
                                ddlPLant.SelectedValue = Convert.ToString(dt.Rows[0]["PLANTCD"]);
                                ddlLocation.SelectedValue = Convert.ToString(dt.Rows[0]["LOCCD"]);
                                txtItemQty.Text = Convert.ToString(dt.Rows[0]["QTY"]);
                                ddlUOM.SelectedValue = Convert.ToString(dt.Rows[0]["UOM"]);
                                txtTrackNo.Text = Convert.ToString(dt.Rows[0]["JOBID"]);
                                txtRequisitioner.Text = Convert.ToString(dt.Rows[0]["REQBYNAME"]);
                                txtItemGroupId.Text = Convert.ToString(dtDtl.Rows[0]["ITEMGRPID"]);
                                txtItemGroup.Text = Convert.ToString(dtDtl.Rows[0]["ITEMGRP"]);
                                txtGLCode.Text = Convert.ToString(dtDtl.Rows[0]["GLCODE"]);
                                txtOnHandStock.Text = Convert.ToString(dtDtl.Rows[0]["MAXSTOCKQTY"]);
                                txtSku.Text = Convert.ToString(dtDtl.Rows[0]["SKU"]);
                                txtItemMake.Text = Convert.ToString(dtDtl.Rows[0]["MAKE"]);
                                txtItemModel.Text = Convert.ToString(dtDtl.Rows[0]["MODEL"]);
                                txtItemDispName.Text = Convert.ToString(dtDtl.Rows[0]["DISPNAME"]);
                                txtItemDispMRP.Text = Convert.ToString(dtDtl.Rows[0]["MRP"]);
                                txtItemValueLimit.Text = Convert.ToString(dtDtl.Rows[0]["VALUELIMIT"]);
                                txtItemMaxStkQty.Text = Convert.ToString(dtDtl.Rows[0]["MAXSTKQTY"]);
                                txtItemHSN.Text = Convert.ToString(dtDtl.Rows[0]["HSNGRP"]);
                                txtItemHSNGroup.Text = Convert.ToString(dtDtl.Rows[0]["HSNGRPCODE"]);
                                txtItemHSNGroupDesc.Text = Convert.ToString(dtDtl.Rows[0]["HSNGRPDESC"]);
                                txtItemCondType.Text = Convert.ToString(dtDtl.Rows[0]["CONDTYPE"]);
                                txtItemStatus.Text = Convert.ToString(dtDtl.Rows[0]["STATUS"]);
                            }
                        }
                        else
                        {
                            txtPartReqNo.Focus();
                            txtPartReqNo.Text = string.Empty;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + txtPartReqNo.Text + " - Invalid Request No.!\");", true);

                            txtPartReqNo.Focus();
                            txtPartReqNo.Text = string.Empty;
                        }
                    }
                    else
                    {
                        txtPartReqNo.Focus();
                        txtPartReqNo.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + txtPartReqNo.Text + " - Invalid Request No.!\");", true);

                        txtPartReqNo.Focus();
                        txtPartReqNo.Text = string.Empty;
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
                objBindDDL.FillCostCenter(ddlCostCenter, ddlPLant.SelectedValue, ddlLocation.SelectedValue);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}
