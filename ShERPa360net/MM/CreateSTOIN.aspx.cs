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
    public partial class CreateSTOIN : System.Web.UI.Page
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

                        if (Request.QueryString["DOCNO"] != null && Request.QueryString["Mode"] == "U")
                        {
                            LoadData();
                        }
                        else
                        {
                            objBindDDL.FillCostCenter(ddlCostCenter, ddlPLant.SelectedValue, ddlLocation.SelectedValue);
                            Session["savedet"] = "Save Item";
                            objBindDDL.FillPlant(ddlPLant);
                            ddlPLant.SelectedIndex = 1;
                            objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);
                            objBindDDL.FillUOM(ddlUOM);
                            txtDocDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                            txtChalanDt.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                                                                                                    //if (Request.QueryString["Mode"].ToString() == "I")
                                                                                                    //{
                            txtDocNo.Text = objMainClass.MAXPRNO(txtDocType.Text, "STD");
                            //}
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
        }

        public void LoadData()
        {
            try
            {
                var dtMaster = objMainClass.GetEachMaterialInwardPoData(objMainClass.intCmpId, Request.QueryString["DOCNO"].ToString(), "603", "MASTER");
                var dtDetail = objMainClass.GetEachMaterialInwardPoData(objMainClass.intCmpId, Request.QueryString["DOCNO"].ToString(), "603", "DETAIL");
                if (dtMaster.Rows.Count > 0)
                {
                    txtDocType.Text = dtMaster.Rows[0]["DOCTYPE"].ToString();
                    txtDocNo.Text = dtMaster.Rows[0]["DOCNO"].ToString();
                    txtDocDate.Text = Convert.ToDateTime(dtMaster.Rows[0]["DOCDATE"]).ToShortDateString();
                    txtRefNo.Text = dtMaster.Rows[0]["REFNO"].ToString();
                    txtPoNo.Text = dtMaster.Rows[0]["REFDOCNO"].ToString();
                    txtPoNo.Enabled = false;
                    txtTransporter.Text = dtMaster.Rows[0]["TRANCODE"].ToString();
                    DataTable dt = objMainClass.GetVendorDetails(objMainClass.intCmpId, txtTransporter.Text, "");
                    if (dt.Rows.Count > 0)
                    {
                        txtTransporterName.Text = Convert.ToString(dt.Rows[0]["NAME1"]);
                        txtTransporter.Text = Convert.ToString(dt.Rows[0]["VENDCODE"]);
                    }
                    txtChalanNo.Text = dtMaster.Rows[0]["CHLNNO"].ToString();
                    txtChalanDt.Text = Convert.ToDateTime(dtMaster.Rows[0]["CHLNDT"]).ToShortDateString();
                    txtRemark.Text = dtMaster.Rows[0]["REMARK"].ToString();
                }

                if (dtDetail.Rows.Count > 0)
                {
                    gvDetail.DataSource = dtDetail;
                    gvDetail.DataBind();
                }
                imgSaveAll.Enabled = false;
                gvDetail.Columns[0].Visible = false;
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
                    if (gvDetail.Rows.Count > 0)
                    {
                        if (ValidatePlantCode())
                        {
                            //string docNo = objMainClass.SaveMaterialInwardFromPo(objMainClass.getFinYear(txtDocDate.Text), txtDocNo.Text, txtDocType.Text,
                            //                                                    txtDocDate.Text, txtChalanNo.Text, txtChalanDt.Text, txtTransporter.Text,
                            //                                                    txtPoNo.Text, txtRefNo.Text, txtRemark.Text, Request.QueryString["Mode"], gvDetail, Convert.ToString(Session["USERID"]));

                            string docNo = objMainClass.InsertSTODCINData(objMainClass.intCmpId, txtDocType.Text, "STD", txtDocNo.Text, txtDocDate.Text, txtPoNo.Text, txtRefNo.Text, txtTransporter.Text,
                                txtPoNo.Text, txtRemark.Text, Convert.ToString(Session["USERID"]), txtChalanNo.Text, txtChalanDt.Text, gvDetail);
                            if (docNo != "")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Record save sucessfully. Doc No. : " + docNo + "\");$('.close').click(function(){window.location.href ='ViewMaterialIssue.aspx?REQUESTFORM=STOIN' });", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Record not inserted sucessfully!');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + "You don't have the access to this plant!" + "\");", true);
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


        public bool ValidatePlantCode()
        {
            bool Isplantvalidate = false;
            try
            {
                if (Session["USERID"] != null)
                {
                    string plantcode = string.Empty;
                    for (int i = 0; i < gvDetail.Rows.Count; i++)
                    {
                        GridViewRow row = gvDetail.Rows[i];
                        plantcode = ((HiddenField)row.FindControl("hdPlantCode")).Value;
                        if (plantcode.Length > 0)
                        {
                            break;
                        }
                    }
                    string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                    for (int i = 0; i < plantArray.Count(); i++)
                    {
                        if (plantArray[i].Trim() == plantcode)
                        {
                            Isplantvalidate = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return Isplantvalidate;
        }

        protected void txtPoNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPoNo.Text.Length > 0)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetPoByPOType(objMainClass.intCmpId, txtPoNo.Text, "STO");
                    if (dt.Rows.Count > 0)
                    {
                        txtPoNo.Text = objMainClass.strConvertZeroPadding(txtPoNo.Text);
                        txtTransporter.Text = Convert.ToString(dt.Rows[0]["TRANCODE"]);



                        txtTransporter_TextChanged(1, e);
                        txtPoNo.Enabled = false;
                    }
                    else
                    {
                        txtPoNo.Focus();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid PO No.!');", true);
                        txtPoNo.Focus();
                    }
                }
                else
                {
                    txtPoNo.Focus();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid PO No.!');", true);
                    txtPoNo.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public void LoadVendorDetailFromPoNo(string pono)
        {
            try
            {
                var dtVendorDetail = objMainClass.GetVendorDetailFromPoNo(objMainClass.intCmpId, pono);
                if (dtVendorDetail.Rows.Count > 0)
                {
                    txtTransporter.Text = dtVendorDetail.Rows[0]["TRANCODE"].ToString();
                    txtTransporterName.Text = dtVendorDetail.Rows[0]["TRANSPORTERNAME"].ToString();
                }
                else
                {
                    txtTransporter.Text = string.Empty;
                    txtTransporterName.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void txtTransporter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtTransporter.Text.Length > 0)
                {
                    //GetVendorDetails
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetVendorDetails(objMainClass.intCmpId, txtTransporter.Text, "");
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

        protected void btnSaveDet_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    //string validation = validateData();
                    //if (validation == "OK")
                    //{
                    Decimal cAmt = 0;
                    Decimal qty = 0;
                    Decimal rate = 0;
                    if (Convert.ToString(Session["savedet"]) == "Save Item")
                    {
                        if (Convert.ToInt32(hdMItemId.Value) > 0)
                        {
                            DataTable dt = (DataTable)ViewState["ItemData"];
                            //DataRow row = dt.Select("ID='" + hdMID.Value + "'").FirstOrDefault();

                            if (gvDetail.Rows.Count > 0)
                            {
                                DataRow lastRow = dt.Rows[dt.Rows.Count - 1];
                                int id = Convert.ToInt32(lastRow["SRNO"]) + 1;
                                Decimal.TryParse(txtItemQty.Text, out qty);
                                Decimal.TryParse(hdMRate.Value, out rate);
                                cAmt = Math.Round(qty * rate, 2);

                                dt.Rows.Add(hdMID.Value, objMainClass.intCmpId.ToString(), objMainClass.strConvertZeroPadding(txtPoNo.Text), hdMPoSrNo.Value, id,
                                            txtItemCode.Text, hdMItemId.Value, txtItemDesc.Text, ddlPLant.Text, ddlLocation.Text, hdMGrpId.Value, hdMItemGrpDesc.Value, hdMPoQty.Value, txtItemQty.Text,
                                            ddlUOM.SelectedValue, ddlUOM.SelectedItem.Text, hdMRate.Value, cAmt, hdMGlcd.Value, ddlCostCenter.SelectedValue, hdMprfcnt.Value,
                                            hdMAssetCd.Value, txtItemRemark.Text, txtChalanQty.Text);

                                ViewState["ItemData"] = dt;
                                gvDetail.DataSource = (DataTable)ViewState["ItemData"];
                                gvDetail.DataBind();
                                ResetDetailControl();
                            }
                            else
                            {
                                Decimal.TryParse(txtItemQty.Text, out qty);
                                Decimal.TryParse(hdMRate.Value, out rate);
                                cAmt = Math.Round(qty * rate, 2);
                                dt.Rows.Add(hdMID.Value, objMainClass.intCmpId.ToString(), objMainClass.strConvertZeroPadding(txtPoNo.Text), hdMPoSrNo.Value, "1",
                                            txtItemCode.Text, hdMItemId.Value, txtItemDesc.Text, ddlPLant.Text, ddlLocation.Text, hdMGrpId.Value, hdMItemGrpDesc.Value, hdMPoQty.Value, txtItemQty.Text,
                                            ddlUOM.SelectedValue, ddlUOM.SelectedItem.Text, hdMRate.Value, cAmt, hdMGlcd.Value, ddlCostCenter.SelectedValue, hdMprfcnt.Value,
                                            hdMAssetCd.Value, txtItemRemark.Text, txtChalanQty.Text);

                                gvDetail.DataSource = dt;
                                gvDetail.DataBind();
                                ViewState["ItemData"] = (DataTable)ViewState["ItemData"];
                                ResetDetailControl();
                            }
                        }
                    }
                    else if (Convert.ToString(Session["savedet"]) == "Update Item")
                    {
                        Decimal.TryParse(txtItemQty.Text, out qty);
                        Decimal.TryParse(hdMRate.Value, out rate);
                        cAmt = Math.Round(qty * rate, 2);

                        DataTable dt = (DataTable)ViewState["ItemData"];
                        DataTable ddt = (DataTable)ViewState["ItemData"];
                        DataRow dr = ddt.Select("SRNO = '" + txtSRNo.Text + "'")[0];
                        dr[10] = ddlLocation.SelectedValue;
                        dr[13] = txtItemQty.Text;
                        dr[14] = txtItemQty.Text;
                        dr[18] = cAmt;
                        dr[21] = ddlCostCenter.SelectedValue;//txtCostCenter.Text;
                        dr[24] = txtItemRemark.Text;
                        dr[25] = txtChalanQty.Text;
                        gvDetail.DataSource = (DataTable)ViewState["ItemData"];
                        gvDetail.DataBind();
                        Session["savedet"] = "Save Item";
                        ResetDetailControl();
                    }
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + "Quantity can't be higer than Po quantity." + "\");", true);
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

        protected void gvDetail_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    gvDetail.DataSource = (DataTable)ViewState["ItemData"];
                    gvDetail.DataBind();
                }
                if (e.CommandName == "eEdit")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    HiddenField hdPoNo = (HiddenField)gRow.FindControl("hdPoNo");
                    HiddenField hdPoSrNo = (HiddenField)gRow.FindControl("hdPoSrNo");
                    HiddenField hdSrNo = (HiddenField)gRow.FindControl("hdSrNo");
                    HiddenField lblItemId = (HiddenField)gRow.FindControl("lblItemId");
                    HiddenField hdItemCode = (HiddenField)gRow.FindControl("hdItemCode");
                    HiddenField hdItemDesc = (HiddenField)gRow.FindControl("hdItemDesc");
                    HiddenField hdItemGrp = (HiddenField)gRow.FindControl("hdItemGrp");
                    HiddenField hdUomDesc = (HiddenField)gRow.FindControl("hdUomDesc");
                    HiddenField hdQty = (HiddenField)gRow.FindControl("hdQty");
                    HiddenField hdChalanQty = (HiddenField)gRow.FindControl("hdChalanQty");
                    HiddenField hdGlCd = (HiddenField)gRow.FindControl("hdGlCd");
                    HiddenField hdCostCenter = (HiddenField)gRow.FindControl("hdCostCenter");
                    HiddenField hdPlantCode = (HiddenField)gRow.FindControl("hdPlantCode");
                    HiddenField hdLoccd = (HiddenField)gRow.FindControl("hdLoccd");
                    HiddenField hdprfct = (HiddenField)gRow.FindControl("hdprfct");
                    HiddenField hdAssetcd = (HiddenField)gRow.FindControl("hdAssetcd");
                    HiddenField hdItemText = (HiddenField)gRow.FindControl("hdItemText");
                    HiddenField hdITEMID = (HiddenField)gRow.FindControl("hdITEMID");
                    HiddenField hdITEMGRPID = (HiddenField)gRow.FindControl("hdITEMGRPID");
                    HiddenField hdUOMID = (HiddenField)gRow.FindControl("hdUOMID");
                    HiddenField hdRATE = (HiddenField)gRow.FindControl("hdRATE");
                    HiddenField hdCAMOUNT = (HiddenField)gRow.FindControl("hdCAMOUNT");
                    HiddenField hdID = (HiddenField)gRow.FindControl("hdID");
                    HiddenField hdPoQty = (HiddenField)gRow.FindControl("hdPoQty");
                    //HiddenField hdReceivedQty = (HiddenField)gRow.FindControl("hdReceivedQty");
                    // Load Data From the GridView Control 
                    txtSRNo.Text = hdSrNo.Value;
                    txtPoSrNo.Text = hdPoSrNo.Value;
                    txtItemCode.Text = hdItemCode.Value;
                    txtItemDesc.Text = hdItemDesc.Value;
                    txtItemQty.Text = hdQty.Value;
                    txtChalanQty.Text = hdChalanQty.Value;
                    ddlUOM.SelectedValue = hdUOMID.Value;
                    ddlPLant.SelectedValue = hdPlantCode.Value;
                    ddlLocation.SelectedValue = hdLoccd.Value;
                    objBindDDL.FillCostCenter(ddlCostCenter, ddlPLant.SelectedValue, ddlLocation.SelectedValue);
                    ddlCostCenter.SelectedValue = hdCostCenter.Value;
                    txtItemRemark.Text = hdItemText.Value;
                    hdMItemId.Value = hdITEMID.Value;
                    hdMGrpId.Value = hdITEMGRPID.Value;
                    hdMRate.Value = hdRATE.Value;
                    hdMCAmt.Value = hdCAMOUNT.Value;
                    hdMGlcd.Value = hdGlCd.Value;
                    hdMAssetCd.Value = hdAssetcd.Value;
                    hdMprfcnt.Value = hdprfct.Value;
                    hdMID.Value = hdID.Value;
                    hdMPoSrNo.Value = hdPoSrNo.Value;
                    hdMItemGrpDesc.Value = hdItemGrp.Value;
                    hdMPoQty.Value = hdPoQty.Value;
                    txtMPoQty.Text = hdPoQty.Value;
                    //txtReceivedQty.Text = hdReceivedQty.Value;
                    btnSaveDet.Visible = true;
                    Session["savedet"] = "Update Item";

                    // Load Data From the GridView Control 
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        private string validateData()
        {
            try
            {
                string j = "OK";
                Decimal qty;
                Decimal chlqty;
                Decimal poqty;
                Decimal revdqty;
                Decimal.TryParse(txtItemQty.Text, out qty);
                Decimal.TryParse(txtChalanQty.Text, out chlqty);
                //Decimal.TryParse(txtReceivedQty.Text, out revdqty);
                Decimal.TryParse("0", out revdqty);
                Decimal.TryParse(txtMPoQty.Text, out poqty);
                qty = qty + revdqty;
                chlqty = chlqty + revdqty;
                if (qty > poqty || chlqty > poqty)
                {
                    j = "ERROR";
                }
                return j;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
                return string.Empty;
            }
        }

        public void ResetDetailControl()
        {
            try
            {
                txtSRNo.Text = string.Empty;
                txtPoSrNo.Text = string.Empty;
                txtItemCode.Text = string.Empty;
                txtItemDesc.Text = string.Empty;
                txtItemQty.Text = string.Empty;
                txtChalanQty.Text = string.Empty;
                ddlUOM.SelectedIndex = -1;
                ddlPLant.SelectedIndex = -1;
                ddlLocation.SelectedIndex = -1;
                //txtCostCenter.Text = string.Empty;
                txtItemRemark.Text = string.Empty;
                //txtReceivedQty.Text = string.Empty;
                hdMItemId.Value = "0";
                hdMGrpId.Value = "";
                hdMRate.Value = "";
                hdMCAmt.Value = "";
                hdMprfcnt.Value = "";
                hdMGlcd.Value = "";
                hdMAssetCd.Value = "";
                hdMID.Value = "";
                hdMPoSrNo.Value = "";
                hdMItemGrpDesc.Value = "";
                hdMPoQty.Value = "";
                btnSaveDet.Visible = false;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void txtChalanNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPoNo.Text.Length >= 10)
                {
                    if (txtChalanNo.Text.Length > 0)
                    {
                        DataTable dt = new DataTable();
                        dt = objMainClass.GetSTOINData(objMainClass.intCmpId, txtChalanNo.Text);
                        if (dt.Rows.Count > 0)
                        {

                            gvDetail.DataSource = dt;
                            gvDetail.DataBind();
                            txtChalanNo.Enabled = false;

                            ViewState["ItemData"] = dt;

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid Doc. No.!');", true);
                        }
                    }
                }
                else
                {
                    txtChalanNo.Text = string.Empty;
                    txtPoNo.Focus();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Enter PO No.!');", true);
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
                if (txtItemQty.Text != null && txtItemQty.Text != "" && txtItemQty.Text != string.Empty)
                {
                    if (Convert.ToDecimal(txtItemQty.Text) > 0)
                    {
                        DataTable dt = new DataTable();
                        dt = objMainClass.GetPOSTODtl(objMainClass.intCmpId, txtPoNo.Text, txtPoSrNo.Text);
                        if (dt.Rows.Count > 0)
                        {
                            //txtMPoQty.Text = Convert.ToString(dt.Rows[0]["POQTY"]);
                            //hfPndQty.Value = Convert.ToString(dt.Rows[0]["PNDQTY"]);



                            double RCVQty = 0;
                            if (gvDetail.Rows.Count > 0)
                            {
                                for (int i = 0; i < gvDetail.Rows.Count; i++)
                                {
                                    GridViewRow row = gvDetail.Rows[i];
                                    //Label lblGVPOSrNo = (Label)row.FindControl("lblGVPRSrNo");
                                    //Label lblGVPONo = (Label)row.FindControl("lblGVPrNo");
                                    //Label lblGVID = (Label)row.FindControl("lblGVID");
                                    //Label lblGVQty = (Label)row.FindControl("lblGVQty");


                                    HiddenField lblGVPOSrNo = (HiddenField)row.FindControl("hdPoSrNo");
                                    HiddenField lblGVPONo = (HiddenField)row.FindControl("hdPoNo");
                                    HiddenField lblGVID = (HiddenField)row.FindControl("hdSrNo");
                                    HiddenField lblGVQty = (HiddenField)row.FindControl("hdQty");


                                    if (lblGVPOSrNo.Value == txtPoSrNo.Text && lblGVPONo.Value == objMainClass.strConvertZeroPadding(txtPoNo.Text) && txtSRNo.Text != lblGVID.Value)
                                    {
                                        RCVQty = Convert.ToDouble(Convert.ToInt32(RCVQty) + Convert.ToInt32(lblGVQty.Value));
                                    }
                                }

                                if (RCVQty >= Convert.ToDouble(txtMPoQty.Text))
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"Full Quantity is issued for these STO!\");", true);
                                }

                            }
                            else
                            {
                                if (RCVQty >= Convert.ToDouble(hfPndQty.Value))
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"Full Quantity is issued for these STO!\");", true);
                                }

                            }
                            if (Convert.ToDouble(txtItemQty.Text) > Convert.ToDouble(txtMPoQty.Text))
                            {
                                btnSaveDet.Visible = false;
                                txtItemQty.Focus();
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"Quantity can't be higer than Ordered quantity!\");", true);
                                txtItemQty.Focus();
                            }
                            else
                            {

                                if (Convert.ToDouble(txtItemQty.Text) > Convert.ToDouble(txtChalanQty.Text))
                                {
                                    btnSaveDet.Visible = false;
                                    txtItemQty.Focus();
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"Quantity can't be higer than Challan quantity!\");", true);
                                    txtItemQty.Focus();
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
                        txtItemQty.Focus();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Quantity can not be 0!');", true);
                        txtItemQty.Focus();
                    }
                }
                else
                {
                    txtItemQty.Focus();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Quantity can not be Null!');", true);
                    txtItemQty.Focus();
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