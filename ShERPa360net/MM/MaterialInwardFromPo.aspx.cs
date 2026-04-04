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
    public partial class MaterialInwardFromPo : System.Web.UI.Page
    {
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        DataTable dtItem = new DataTable();

        #region PageLoadEvent
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

                        if (Request.QueryString["DOCNO"] != null && Request.QueryString["Mode"] == "U")
                        {
                            LoadData();
                        }
                        else
                        {
                            Session["savedet"] = "Save Item";
                            objBindDDL.FillPlant(ddlPLant);
                            ddlPLant.SelectedIndex = 1;
                            objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);
                            objBindDDL.FillUOM(ddlUOM);
                            txtDocDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                            txtChalanDt.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                            if (Request.QueryString["Mode"].ToString() == "I")
                            {
                                txtDocNo.Text = objMainClass.MAXPRNO(txtDocType.Text, "MIR");
                            }
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
        #endregion

        #region MASTEREVENT
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
                            int iCount = RejectNotAprv();
                            if (iCount == 0)
                            {


                                byte[] bytes;
                                string extension = ".jpeg";

                                using (BinaryReader br = new BinaryReader(fuInvDoc.PostedFile.InputStream))
                                {
                                    bytes = br.ReadBytes(fuInvDoc.PostedFile.ContentLength);
                                    extension = System.IO.Path.GetExtension(fuInvDoc.FileName);
                                }

                                byte[] PObytes;
                                string POextension = ".jpeg";
                                using (BinaryReader br = new BinaryReader(fuPODoc.PostedFile.InputStream))
                                {
                                    PObytes = br.ReadBytes(fuPODoc.PostedFile.ContentLength);
                                    POextension = System.IO.Path.GetExtension(fuPODoc.FileName);
                                }

                                string docNo = objMainClass.SaveMaterialInwardFromPo(objMainClass.getFinYear(txtDocDate.Text), txtDocNo.Text, txtDocType.Text,
                                                                                    txtDocDate.Text, txtChalanNo.Text, txtChalanDt.Text, txtTransporter.Text,
                                                                                    txtPoNo.Text, txtRefNo.Text, txtRemark.Text, Request.QueryString["Mode"], gvDetail, Convert.ToString(Session["USERID"]),
                                                                                    bytes, extension, PObytes, POextension);
                                if (docNo != "")
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Record save sucessfully. Doc No. : " + docNo + "\");$('.close').click(function(){window.location.href ='ViewMaterialInwardList.aspx' });", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Record not inserted sucessfully!');", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"Record not saved. Count of rejected or not approved PO in list is : " + iCount + "\");", true);
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


        public int RejectNotAprv()
        {
            int iCount = 0;

            for (int i = 0; i < gvDetail.Rows.Count; i++)
            {
                GridViewRow row = gvDetail.Rows[i];
                Label lblAPRVSTATUS = (Label)row.FindControl("lblAPRVSTATUS");
                if (lblAPRVSTATUS.Text == "260")
                {

                }
                else
                {
                    iCount = iCount + 1;
                }
            }

            return iCount;
        }


        protected void imgCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("MaterialInwardFromPo.aspx?Mode=I", false);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void txtPoNo_TextChanged(object sender, EventArgs e)
        {
            //New code aaded by Mohit on 23.8.2023
            try
            {
                string pono = txtPoNo.Text;
                if (pono.Length > 0)
                {
                    dtItem = objMainClass.GetPOMaterialInwardItemDetail(objMainClass.intCmpId, pono, txtDocNo.Text, "103", Request.QueryString["Mode"].ToString());
                    if (dtItem.Rows.Count > 0)
                    {
                        if (Convert.ToDateTime(dtItem.Rows[0]["PODT"]) > Convert.ToDateTime("2024-03-01"))
                        {
                            int deptid = Convert.ToInt32(dtItem.Rows[0]["DEPTID"]);
                            DataTable dtDeptAprv = new DataTable();
                            dtDeptAprv = objDALUserRights.PO_APPROVE_RIGHTS("PO", "0", "", "", 9);
                            string strDept = string.Empty;
                            for (int i = 0; i < dtDeptAprv.Rows.Count; i++)
                            {
                                if (Convert.ToInt32(dtDeptAprv.Rows[i]["DEPTCD"]) == deptid)
                                {
                                    strDept = strDept + Convert.ToInt32(dtDeptAprv.Rows[i]["DEPTCD"]);
                                }
                            }
                            //if (Convert.ToInt32(dtItem.Rows[0]["DEPTID"]) == 10)
                            if (strDept.Length > 0)
                            {

                                DataTable dtAprvSeq = new DataTable();
                                dtAprvSeq = objDALUserRights.PO_APPROVE_RIGHTS("PO", "0",Convert.ToString(dtItem.Rows[0]["PLANTCD"]) , Convert.ToString(deptid), 6);
                                int maxseq = Convert.ToInt32(dtAprvSeq.Rows[0]["CNT"]);

                                DataTable dtApproved = objMainClass.DOC_APRROVAL("PO", txtPoNo.Text, (int)APRVTYPE.APPROVED, 1);
                                bool seqtrue = false;
                                for (int j = 0; j < dtApproved.Rows.Count; j++)
                                {
                                    if (Convert.ToInt32(dtApproved.Rows[j]["STAGESEQ"]) == maxseq)
                                    {
                                        seqtrue = true;
                                    }
                                    //else
                                    //{
                                    //    seqtrue = false;
                                    //}
                                }

                                //if (dtApproved.Rows.Count > 0)
                                if (seqtrue == true)
                                {
                                    if (Convert.ToInt32(dtApproved.Rows[0]["STATUS"]) == (int)APRVTYPE.APPROVED)
                                    {
                                        if (dtItem.Rows.Count > 0)
                                        {

                                            ViewState["ItemData"] = dtItem;
                                            gvDetail.DataSource = (DataTable)ViewState["ItemData"];
                                            gvDetail.DataBind();
                                            LoadVendorDetailFromPoNo(pono);
                                            txtPoNo.Enabled = false;
                                        }
                                        else
                                        {
                                            gvDetail.DataSource = string.Empty;
                                            gvDetail.DataBind();
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"PO rejected by Approver..!\");", true);
                                        txtPoNo.Text = string.Empty;
                                        txtPoNo.Text = string.Empty;
                                        txtPoNo.Focus();
                                    }
                                }
                                else
                                {
                                    DataTable dtRejected = objMainClass.DOC_APRROVAL("PO", txtPoNo.Text, (int)APRVTYPE.REJECT, 1);
                                    if (dtRejected.Rows.Count>0)
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"PO rejected.!\");", true);
                                        txtPoNo.Text = string.Empty;
                                        txtPoNo.Text = string.Empty;
                                        txtPoNo.Focus();
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"PO still not approved. Please approve PO.!\");", true);
                                        txtPoNo.Text = string.Empty;
                                        txtPoNo.Text = string.Empty;
                                        txtPoNo.Focus();
                                    }
                                }
                            }
                            else
                            {
                                if (dtItem.Rows.Count > 0)
                                {

                                    ViewState["ItemData"] = dtItem;
                                    gvDetail.DataSource = (DataTable)ViewState["ItemData"];
                                    gvDetail.DataBind();
                                    LoadVendorDetailFromPoNo(pono);
                                    txtPoNo.Enabled = false;
                                }
                                else
                                {
                                    gvDetail.DataSource = string.Empty;
                                    gvDetail.DataBind();
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
                                }
                            }
                        }
                        else
                        {
                            ViewState["ItemData"] = dtItem;
                            gvDetail.DataSource = (DataTable)ViewState["ItemData"];
                            gvDetail.DataBind();
                            LoadVendorDetailFromPoNo(pono);
                            txtPoNo.Enabled = false;
                        }

                    }
                    else
                    {
                        gvDetail.DataSource = string.Empty;
                        gvDetail.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
                    }
                }
            }
                catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public void LoadData()
        {
            try
            {
                var dtMaster = objMainClass.GetEachMaterialInwardPoData(objMainClass.intCmpId, Request.QueryString["DOCNO"].ToString(), "103", "MASTER");
                var dtDetail = objMainClass.GetEachMaterialInwardPoData(objMainClass.intCmpId, Request.QueryString["DOCNO"].ToString(), "103", "DETAIL");
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

        protected void txtTransporter_TextChanged(object sender, EventArgs e)
        {
            try
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
                        txtTransporter.Text = string.Empty;
                        txtTransporterName.Text = string.Empty;
                        txtTransporter.Focus();
                        ScriptManager.GetCurrent(this.Page).SetFocus(this.txtTransporter);
                    }
                }
                else
                {
                    lblTransporterError.Text = "Minimum 5 digit req.";
                    lblTransporterError.Visible = true;
                    txtTransporter.Text = string.Empty;
                    txtTransporterName.Text = string.Empty;
                    txtTransporter.Focus();
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

        #endregion


        #region DETAILEVENT
        protected void btnSaveDet_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    string validation = validateData();
                    if (validation == "OK")
                    {
                        imgSaveAll.Enabled = true;
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
                                                hdMAssetCd.Value, txtItemRemark.Text, txtChalanQty.Text, txtReceivedQty.Text, hfdAPRVSTATUS.Value, hfdAPRVSTATUSDESC.Value);

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
                                                hdMAssetCd.Value, txtItemRemark.Text, txtChalanQty.Text, txtReceivedQty.Text, hfdAPRVSTATUS.Value, hfdAPRVSTATUSDESC.Value);

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
                            dr[9] = ddlLocation.SelectedValue;
                            dr[13] = txtItemQty.Text;
                            dr[17] = cAmt;
                            dr[19] = ddlCostCenter.SelectedValue;//txtCostCenter.Text;
                            dr[22] = txtItemRemark.Text;
                            dr[26] = txtChalanQty.Text;
                            gvDetail.DataSource = (DataTable)ViewState["ItemData"];
                            gvDetail.DataBind();
                            Session["savedet"] = "Save Item";
                            ResetDetailControl();
                        }

                        if (Convert.ToString(hfdAPRVSTATUS.Value) == string.Empty || Convert.ToString(hfdAPRVSTATUS.Value) == "" || Convert.ToString(hfdAPRVSTATUS.Value) == null)
                        {
                            imgSaveAll.Enabled = false;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + "This PO Sr. No. still not approved/rejected." + "\");", true);
                        }
                        else if (Convert.ToInt32(hfdAPRVSTATUS.Value) == 261)//rejected
                        {
                            imgSaveAll.Enabled = false;

                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + "This PO Sr. No. Rejected." + "\");", true);
                            imgSaveAll.Enabled = false;
                        }
                        else if (Convert.ToInt32(hfdAPRVSTATUS.Value) == 260) // approved
                        {
                            imgSaveAll.Enabled = true;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + "This PO Sr. No. is approved." + "\");", true);
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + "Quantity can't be higer than Po quantity." + "\");", true);
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

        public void ResetDetailControl()
        {
            try
            {
                txtSRNo.Text = string.Empty;
                txtPRNo.Text = string.Empty;
                txtItemCode.Text = string.Empty;
                txtItemDesc.Text = string.Empty;
                txtItemQty.Text = string.Empty;
                txtChalanQty.Text = string.Empty;
                ddlUOM.SelectedIndex = -1;
                ddlPLant.SelectedIndex = -1;
                ddlLocation.SelectedIndex = -1;
                //txtCostCenter.Text = string.Empty;
                txtItemRemark.Text = string.Empty;
                txtReceivedQty.Text = string.Empty;
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
                    HiddenField hdReceivedQty = (HiddenField)gRow.FindControl("hdReceivedQty");
                    HiddenField hdAPRVSTATUS = (HiddenField)gRow.FindControl("hdAPRVSTATUS");
                    HiddenField hdAPRVSTATUSDESC = (HiddenField)gRow.FindControl("hdAPRVSTATUSDESC");
                    // Load Data From the GridView Control 
                    txtSRNo.Text = hdSrNo.Value;
                    txtPRNo.Text = hdPoSrNo.Value;
                    txtItemCode.Text = hdItemCode.Value;
                    txtItemDesc.Text = hdItemDesc.Value;
                    txtItemQty.Text = hdQty.Value;
                    txtChalanQty.Text = hdChalanQty.Value;
                    ddlUOM.SelectedValue = hdUOMID.Value;
                    ddlPLant.SelectedValue = hdPlantCode.Value;
                    objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);
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
                    txtReceivedQty.Text = hdReceivedQty.Value;

                    hfdAPRVSTATUS.Value = hdAPRVSTATUS.Value;
                    hfdAPRVSTATUSDESC.Value = hdAPRVSTATUSDESC.Value;

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
                Decimal.TryParse(txtReceivedQty.Text, out revdqty);
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
        #endregion

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