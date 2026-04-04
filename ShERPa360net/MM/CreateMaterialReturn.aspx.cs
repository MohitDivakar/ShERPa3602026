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
    public partial class CreateMaterialReturn : System.Web.UI.Page
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
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }

                        if (FormRights.bAdd == false)
                        {
                            imgSaveAll.Enabled = false;
                        }
                        // Initiate Master Control    
                        objBindDDL.FillDocType(ddlDoctype, "MR");
                        ddlDoctype.SelectedIndex = 1;
                        ddlDoctype.Enabled = false;
                        txtDocNo.Text = objMainClass.MAXPRNO(ddlDoctype.SelectedItem.Text, "MR");
                        txtDocDt.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();

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
                                if (plantArray[i] == ((Label)grvListItem.Rows[0].FindControl("lblPlant")).Text)
                                {
                                    PLantRights = ((Label)grvListItem.Rows[0].FindControl("lblPlant")).Text;
                                }
                            }

                            if (PLantRights.Length > 0)
                            {
                                string DOCNO = objMainClass.SaveMaterialReturn(objMainClass.getFinYear(txtDocDt.Text), txtDocNo.Text,
                                                                               ddlDoctype.SelectedItem.Text, txtDocDt.Text, txtVendor.Text, txtPoNo.Text,
                                                                               txtRemark.Text, "I", grvListItem, Convert.ToString(Session["USERID"])
                                                                               );
                                if (DOCNO != "")
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Document No. : " + DOCNO + " saved successfully.\");$('.close').click(function(){window.location.href ='ViewMaterialIssue.aspx?REQUESTFORM=MR' });", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Record not inserted sucessfully!');", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have plant rights.');$('#txtVendor').focus();", true);
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
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='Login.aspx' });", true);
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
                dtColumn.ColumnName = "DOCNO";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ID";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "PONO";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "POSRNO";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "QTY";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "CHLNQTY";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ACPTQTY";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "RTNQTY";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "MMDOCNO";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "MMFINYEAR";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "MMSRNO";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMCODE";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMDESC";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "UOMDesc";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "PLANTCD";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "LOCCD";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMID";
                dtItem.Columns.Add(dtColumn);


                dtColumn = new DataColumn();
                dtColumn.ColumnName = "UOMID";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "RATE";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "CAMOUNT";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMTEXT";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "CSTCENTCD";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "PRFCNT";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "GLCD";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ASSETCD";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMGRPID";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "TRACKNO";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "PLANTTEXT";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "LOCTEXT";
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

        private string validateItemDetail()
        {
            string errormsg = "";
            try
            {
                string PLantRights = string.Empty;
                string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                for (int i = 0; i < plantArray.Count(); i++)
                {
                    if (plantArray[i] == ddlPlant.SelectedValue)
                    {
                        PLantRights = ddlPlant.SelectedValue;
                    }
                }

                if (PLantRights.Length == 0)
                {
                    errormsg = "You do not have plant rights.";
                }

                if (txtVendor.Text.Length == 0)
                {
                    errormsg += errormsg.Length > 0 ? "\n Please Enter Vendor No" : "Please Enter Vendor No";
                    ScriptManager.GetCurrent(this.Page).SetFocus(this.txtVendor);
                }

                if (txtPoNo.Text.Length == 0)
                {
                    errormsg += errormsg.Length > 0 ? "\n Please Enter Po No" : "Please Enter Po No";
                    ScriptManager.GetCurrent(this.Page).SetFocus(this.txtPoNo);
                }
                else
                {
                    lblPoNoError.Text = string.Empty;
                    lblPoNoError.Visible = false;
                    DataTable dt = objMainClass.GetMaterialDetailFromPoDocNo(txtVendor.Text, txtPoNo.Text, "", "", 0, 0, "CHECKPONOEXIST");
                    if (dt.Rows[0]["AVAILABLE"].ToString().ToUpper() == "YES")
                    {
                        lblPoNoError.Text = string.Empty;
                        lblPoNoError.Visible = false;
                    }
                    else
                    {
                        lblPoNoError.Text = "Please Enter valid Po No.";
                        lblPoNoError.Visible = true;
                        errormsg += errormsg.Length > 0 ? "\n Please Enter valid Po No" : "Please Enter valid Po No";
                        ScriptManager.GetCurrent(this.Page).SetFocus(this.txtPoNo);
                        txtPoNo.Focus();
                    }
                }

                if (txtMMDocNo.Text.Length == 0)
                {
                    errormsg += errormsg.Length > 0 ? "\n Please Enter Item Doc No" : "Please Enter Item Doc No";
                }
                else
                {
                    lblMDocNoError.Text = string.Empty;
                    lblMDocNoError.Visible = false;
                    DataTable dt = objMainClass.GetMaterialDetailFromPoDocNo(txtVendor.Text, txtPoNo.Text, txtMMDocNo.Text, ddlDoctype.SelectedValue, 0, 0, "CHECKDOCNOEXIST");
                    if (dt.Rows.Count > 0)
                    {
                        lblMDocNoError.Text = string.Empty;
                        lblMDocNoError.Visible = false;
                    }
                    else
                    {
                        lblMDocNoError.Text = "Please Enter valid Item Doc No.";
                        lblMDocNoError.Visible = true;
                        txtMMDocNo.Focus();
                        errormsg += errormsg.Length > 0 ? "\n Please Enter valid Item Doc No" : "Please Enter valid Item Doc No";
                        ScriptManager.GetCurrent(this.Page).SetFocus(this.txtMMDocNo);
                    }
                }

                if (txtMMSrNo.Text.Length == 0)
                {
                    errormsg += errormsg.Length > 0 ? "\n Please Enter MM Sr No" : "Please Enter MM Sr No";
                }
                else
                {
                    lblMMSrNoError.Text = string.Empty;
                    lblMMSrNoError.Visible = false;
                    DataTable dt = objMainClass.GetMaterialDetailFromPoDocNo(txtVendor.Text, txtPoNo.Text, txtMMDocNo.Text, ddlDoctype.SelectedValue, Convert.ToInt64(txtFinalYear.Text), Convert.ToInt64(txtMMSrNo.Text), "INWARDITEMDETAIL");
                    if (dt.Rows.Count > 0)
                    {
                        lblMMSrNoError.Text = string.Empty;
                        lblMMSrNoError.Visible = false;
                    }
                    else
                    {
                        lblMMSrNoError.Text = "Please Enter the valid MM Sr No.";
                        lblMMSrNoError.Visible = true;
                        txtMMSrNo.Focus();
                        errormsg += errormsg.Length > 0 ? "\n Please Enter the valid MM Sr No." : "Please Enter the valid MM Sr No.";
                        ScriptManager.GetCurrent(this.Page).SetFocus(this.txtMMSrNo);
                    }
                }

                if (txtFinalYear.Text.Length == 0)
                {
                    errormsg += errormsg.Length > 0 ? "\n Please Enter Final Year" : "Please Enter Final Year";
                }
                else
                {
                    if (txtFinalYear.Text.Length != 4)
                    {
                        errormsg += errormsg.Length > 0 ? "\n Please Enter valid Final Year" : "Please Enter valid Final Year";
                    }
                }

                if (ddlPlant.SelectedValue == "0")
                {
                    errormsg += errormsg.Length > 0 ? "\n Please Select the Plant" : "Please Select the Plant";
                }

                if (ddlLocation.SelectedValue == "0")
                {
                    errormsg += errormsg.Length > 0 ? "\n Please Select the Location" : "Please Select the Location";
                }

                if (txtReturnQty.Text.Length == 0)
                {
                    errormsg += errormsg.Length > 0 ? "\n Please Enter the Return Qty" : "Please Enter the Return Qty";
                }

                Decimal alreadyrallotedqty = chkRepeatItemQty();
                Decimal acceptedqty, returnqty = 0;
                Decimal.TryParse(txtAcceptedQty.Text, out acceptedqty);
                acceptedqty = acceptedqty - alreadyrallotedqty;
                Decimal.TryParse(txtReturnQty.Text, out returnqty);
                if (!chkStkBal(alreadyrallotedqty))
                {
                    errormsg += errormsg.Length > 0 ? "\n There is a deficit of Quantity by" : "There is a deficit of Quantity by";
                }

                if (returnqty > acceptedqty)
                {
                    errormsg += errormsg.Length > 0 ? "\n Return Qty should be Less then equal to Accepted Qty" : "Return Qty should be Less then equal to Accepted Qty";
                }
                return errormsg;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return errormsg;
        }

        private void EmptyString()
        {
            try
            {
                txtSRNo.Text = string.Empty;
                txtPoNo.Text = string.Empty;
                txtMMDocNo.Text = string.Empty;
                txtFinalYear.Text = string.Empty;
                txtMMSrNo.Text = string.Empty;
                ResetLoadedItemDetail();
                //LoadPlantCostCenter();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public string strReplicate(string str, int intD)
        {
            string functionReturnValue = null;
            int i = 0;
            functionReturnValue = "";
            for (i = 1; i <= intD; i++)
            {
                functionReturnValue = functionReturnValue + str;
            }
            return functionReturnValue;
        }

        public string strConvertZeroPadding(string strText, string padChar = "0", int intTimes = 10)
        {
            strText = strReplicate(padChar, intTimes - strText.Length) + strText;
            return strText;
        }

        public void LoadEachSrNoItemDetail(DataTable dt)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    txtPoSrNo.Text = dt.Rows[0]["posrno"].ToString();
                    txtGLCode.Text = dt.Rows[0]["glcd"].ToString();
                    txtProfitCenter.Text = dt.Rows[0]["prfcnt"].ToString();
                    txtItemGroupId.Text = dt.Rows[0]["ITEMGRPID"].ToString();
                    txtItemId.Text = dt.Rows[0]["ITEMID"].ToString();
                    txtItemCode.Text = dt.Rows[0]["itemcode"].ToString();
                    txtItemDesc.Text = dt.Rows[0]["itemdesc"].ToString();
                    txtrate.Text = dt.Rows[0]["rate"].ToString();
                    txtAcceptedQty.Text = (Convert.ToDecimal(dt.Rows[0]["QTY"].ToString()) - Convert.ToDecimal(dt.Rows[0]["RTNQTY"].ToString())).ToString();
                    txtPreviousQty.Text = dt.Rows[0]["RTNQTY"].ToString();
                    txtTotalQty.Text = dt.Rows[0]["QTY"].ToString();
                    txtchalanqty.Text = dt.Rows[0]["CHLNQTY"].ToString();
                    ddlPlant.SelectedValue = dt.Rows[0]["PLANTCD"].ToString();
                    ddlLocation.SelectedValue = dt.Rows[0]["LOCCD"].ToString();
                    ddlUOM.SelectedValue = dt.Rows[0]["UOM"].ToString();
                    txtCAAmount.Text = dt.Rows[0]["CAMOUNT"].ToString();
                    ddlCostCenter.SelectedValue = dt.Rows[0]["CSTCENTCD"].ToString();
                    txtTrackingNO.Text = dt.Rows[0]["TRACKNO"].ToString();
                    txtItemText.Text = dt.Rows[0]["ITEMTEXT"].ToString();
                    //txtSku.Text = dt.Rows[0]["SKU"].ToString();
                }
                else
                {
                    ResetLoadedItemDetail();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public void ResetLoadedItemDetail()
        {
            try
            {
                txtPoSrNo.Text = string.Empty;
                txtGLCode.Text = string.Empty;
                txtProfitCenter.Text = string.Empty;
                txtItemGroupId.Text = string.Empty;
                txtItemId.Text = string.Empty;
                txtItemCode.Text = string.Empty;
                txtItemDesc.Text = string.Empty;
                txtrate.Text = string.Empty;
                txtAcceptedQty.Text = string.Empty;
                txtReturnQty.Text = string.Empty;
                txtPreviousQty.Text = string.Empty;
                txtTotalQty.Text = string.Empty;
                txtchalanqty.Text = string.Empty;
                ddlPlant.SelectedIndex = -1;
                ddlLocation.SelectedIndex = -1;
                ddlUOM.SelectedIndex = -1;
                txtCAAmount.Text = string.Empty;
                //txtCostCenter.Text = string.Empty;
                txtTrackingNO.Text = string.Empty;
                txtItemText.Text = string.Empty;
                txtDetailRemark.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public Decimal chkRepeatItemQty()
        {
            DataTable MRdt = (DataTable)ViewState["ItemData"];
            Decimal dblIssueQty = 0;
            int srno = 0;
            try
            {
                if (grvListItem.Rows.Count > 0)
                {
                    DataRow lastRow = MRdt.Rows[MRdt.Rows.Count - 1];
                    srno = Convert.ToInt32(lastRow["ID"]) + 1;
                }
                else
                {
                    srno = 1;
                }

                for (int i = 0; i < grvListItem.Rows.Count; i++)
                {
                    GridViewRow row = grvListItem.Rows[i];
                    int gsrno = Convert.ToInt32(((Label)row.FindControl("lblID")).Text);
                    int gitemid = Convert.ToInt32(((Label)row.FindControl("lblItemId")).Text);
                    string gplantcode = ((Label)row.FindControl("lblPlant")).Text;
                    string glocation = ((Label)row.FindControl("lblLocation")).Text;
                    if (gsrno != srno)
                    {
                        if (gitemid == Convert.ToInt32(txtItemId.Text) && gplantcode == ddlPlant.SelectedValue.ToString() &&
                            glocation == ddlLocation.SelectedValue.ToString())
                        {
                            dblIssueQty = dblIssueQty + Convert.ToDecimal(((Label)row.FindControl("lblReturnQty")).Text);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return dblIssueQty;
        }

        public bool chkStkBal(Decimal IssuedStk)
        {
            bool Isvalidate = true;
            try
            {
                Decimal rtnqty = 0;
                Decimal.TryParse(txtReturnQty.Text, out rtnqty);
                Decimal CurrStock = objMainClass.SP_GET_STOCK_FUNCTION(objMainClass.intCmpId, objMainClass.getFinYear(objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true)), txtItemCode.Text, objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true), ddlPlant.SelectedValue, ddlLocation.SelectedValue);
                CurrStock = CurrStock - IssuedStk;
                if (CurrStock < rtnqty)
                {
                    Isvalidate = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return Isvalidate;
        }

        //private void LoadPlantCostCenter()
        //{
        //    try
        //    {
        //        DataTable dt = objMainClass.GetPRFCost(ddlPlant.SelectedValue, Convert.ToString(Session["USERID"]));
        //        if (dt.Rows.Count > 0)
        //        {
        //            txtProfitCenter.Text = Convert.ToString(dt.Rows[0]["PRFCNT"]);
        //            txtCostCenter.Text   = Convert.ToString(dt.Rows[0]["CSTCENTCD"]);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
        //    }
        //}
        #endregion

        #region EVENTS
        protected void txtVendor_TextChanged(object sender, EventArgs e)
        {
            try
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
                        txtVendorName.Text = string.Empty;
                        txtVendor.Text = string.Empty;
                        txtVendor.Focus();
                        ScriptManager.GetCurrent(this.Page).SetFocus(this.txtVendor);
                    }
                }
                else
                {
                    lblVendorError.Text = "Minimum 5 digit req.";
                    lblVendorError.Visible = true;
                    txtVendorName.Text = string.Empty;
                    txtVendor.Text = string.Empty;
                    txtVendor.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void txtPoNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPoNo.Text.Length >= 1)
                {
                    lblPoNoError.Text = string.Empty;
                    lblPoNoError.Visible = false;
                    DataTable dt = objMainClass.GetMaterialDetailFromPoDocNo(txtVendor.Text, txtPoNo.Text, "", "", 0, 0, "CHECKPONOEXIST");
                    if (dt.Rows[0]["AVAILABLE"].ToString().ToUpper() == "YES")
                    {
                        lblPoNoError.Text = string.Empty;
                        lblPoNoError.Visible = false;
                        ScriptManager.GetCurrent(this.Page).SetFocus(this.txtMMDocNo);
                    }
                    else
                    {
                        lblPoNoError.Text = "Po No is not Exist.";
                        lblPoNoError.Visible = true;
                        ScriptManager.GetCurrent(this.Page).SetFocus(this.txtPoNo);
                    }
                }
                else
                {
                    lblPoNoError.Text = "Please Enter the Po No.";
                    lblPoNoError.Visible = true;
                    ScriptManager.GetCurrent(this.Page).SetFocus(this.txtPoNo);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void txtMMDocNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtMMDocNo.Text.Length >= 1)
                {
                    lblMDocNoError.Text = string.Empty;
                    lblMDocNoError.Visible = false;
                    DataTable dt = objMainClass.GetMaterialDetailFromPoDocNo(txtVendor.Text, txtPoNo.Text, txtMMDocNo.Text, ddlDoctype.SelectedItem.Text, 0, 0, "CHECKDOCNOEXIST");
                    if (dt.Rows[0]["AVAILABLE"].ToString().ToUpper() == "YES")
                    {
                        lblMDocNoError.Text = string.Empty;
                        lblMDocNoError.Visible = false;
                        ScriptManager.GetCurrent(this.Page).SetFocus(this.txtFinalYear);
                    }
                    else
                    {
                        lblMDocNoError.Text = "Doc No is not Exist.";
                        lblMDocNoError.Visible = true;
                        txtMMDocNo.Focus();
                        ScriptManager.GetCurrent(this.Page).SetFocus(this.txtMMDocNo);
                    }
                }
                else
                {
                    lblMDocNoError.Text = "Please Enter the Item Doc No.";
                    lblMDocNoError.Visible = true;
                    ScriptManager.GetCurrent(this.Page).SetFocus(this.txtMMDocNo);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void txtMMSrNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtMMDocNo.Text.Length >= 1)
                {
                    lblMMSrNoError.Text = string.Empty;
                    lblMMSrNoError.Visible = false;
                    DataTable dt = objMainClass.GetMaterialDetailFromPoDocNo(txtVendor.Text, txtPoNo.Text, txtMMDocNo.Text, ddlDoctype.SelectedItem.Text, Convert.ToInt64(txtFinalYear.Text), Convert.ToInt64(txtMMSrNo.Text), "INWARDITEMDETAIL");
                    if (dt.Rows.Count > 0)
                    {
                        lblMMSrNoError.Text = string.Empty;
                        lblMMSrNoError.Visible = false;
                        LoadEachSrNoItemDetail(dt);
                        ScriptManager.GetCurrent(this.Page).SetFocus(this.txtReturnQty);
                    }
                    else
                    {
                        lblMMSrNoError.Text = "MM Sr is not Exist.";
                        lblMMSrNoError.Visible = true;
                        ScriptManager.GetCurrent(this.Page).SetFocus(this.txtMMSrNo);
                    }
                }
                else
                {
                    lblMMSrNoError.Text = "MM Sr is not Exist.";
                    lblMMSrNoError.Visible = true;
                    ScriptManager.GetCurrent(this.Page).SetFocus(this.txtMMSrNo);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void txtFinalYear_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtFinalYear.Text.Length >= 1)
                {
                    ScriptManager.GetCurrent(this.Page).SetFocus(this.txtMMSrNo);
                }
                else
                {

                    ScriptManager.GetCurrent(this.Page).SetFocus(this.txtFinalYear);
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
                        if (plantArray[i] == ddlPlant.SelectedValue)
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
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have plant rights.');$('#txtJobId').focus();", true);
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
                    string erromsg = validateItemDetail();
                    if (erromsg.Length == 0)
                    {
                        if (Convert.ToString(Session["savedet"]) == "Save Item")
                        {
                            Decimal rate, qty, camount = 0;
                            Decimal.TryParse(txtrate.Text, out rate);
                            Decimal.TryParse(txtReturnQty.Text, out qty);
                            camount = Math.Round((rate * qty), 2);
                            DataTable MRdt = (DataTable)ViewState["ItemData"];
                            if (grvListItem.Rows.Count > 0)
                            {
                                DataRow lastRow = MRdt.Rows[MRdt.Rows.Count - 1];
                                int id = Convert.ToInt32(lastRow["ID"]) + 1;

                                MRdt.Rows.Add(strConvertZeroPadding(txtDocNo.Text), id, strConvertZeroPadding(txtPoNo.Text), txtPoSrNo.Text,
                                              txtTotalQty.Text, txtchalanqty.Text, txtAcceptedQty.Text, txtReturnQty.Text, strConvertZeroPadding(txtMMDocNo.Text), txtFinalYear.Text, txtMMSrNo.Text, txtItemCode.Text,
                                              txtItemDesc.Text, ddlUOM.SelectedItem.Text, ddlPlant.SelectedValue, ddlLocation.SelectedValue, txtItemId.Text,
                                              ddlUOM.SelectedValue, txtrate.Text, camount.ToString(), txtDetailRemark.Text, ddlCostCenter.SelectedValue, txtProfitCenter.Text,
                                              txtGLCode.Text, txtAssetCode.Text, txtItemGroupId.Text, txtTrackingNO.Text, ddlPlant.SelectedItem.Text, ddlLocation.SelectedItem.Text);
                                ViewState["ItemData"] = MRdt;
                            }
                            else
                            {
                                MRdt.Rows.Add(strConvertZeroPadding(txtDocNo.Text), "1", strConvertZeroPadding(txtPoNo.Text), txtPoSrNo.Text,
                                              txtTotalQty.Text, txtchalanqty.Text, txtAcceptedQty.Text, txtReturnQty.Text, strConvertZeroPadding(txtMMDocNo.Text), txtFinalYear.Text, txtMMSrNo.Text, txtItemCode.Text,
                                              txtItemDesc.Text, ddlUOM.SelectedItem.Text, ddlPlant.SelectedValue, ddlLocation.SelectedValue, txtItemId.Text,
                                              ddlUOM.SelectedValue, txtrate.Text, camount.ToString(), txtDetailRemark.Text, ddlCostCenter.SelectedValue, txtProfitCenter.Text,
                                              txtGLCode.Text, txtAssetCode.Text, txtItemGroupId.Text, txtTrackingNO.Text, ddlPlant.SelectedItem.Text, ddlLocation.SelectedItem.Text);
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
                            dr[0] = strConvertZeroPadding(txtMMDocNo.Text);
                            dr[2] = strConvertZeroPadding(txtPoNo.Text);
                            dr[3] = txtPoSrNo.Text;
                            dr[4] = txtReturnQty.Text;
                            dr[5] = txtchalanqty.Text;
                            dr[6] = txtAcceptedQty.Text;
                            dr[7] = txtFinalYear.Text;
                            dr[8] = txtMMSrNo.Text;
                            dr[9] = txtItemCode.Text;
                            dr[10] = txtItemDesc.Text;
                            dr[11] = ddlUOM.SelectedItem.Text;
                            dr[12] = ddlPlant.SelectedValue;
                            dr[13] = ddlLocation.SelectedValue;
                            dr[14] = txtItemId.Text;
                            dr[15] = txtrate.Text;
                            dr[16] = txtCAAmount.Text;
                            dr[17] = txtItemText.Text;
                            dr[18] = ddlCostCenter.SelectedValue;//txtCostCenter.Text;
                            dr[19] = txtProfitCenter.Text;
                            dr[20] = txtGLCode.Text;
                            dr[21] = txtAssetCode.Text;
                            dr[22] = txtItemGroupId.Text;
                            dr[23] = txtTrackingNO.Text;
                            grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                            grvListItem.DataBind();
                            Session["savedet"] = "Save Item";
                            EmptyString();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + erromsg + "\");", true);
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
            try
            {
                //if (e.Row.RowType == DataControlRowType.DataRow)
                //{
                //    Label lblItemCode       = (e.Row.FindControl("lblItemCode") as Label);
                //    Label lblPlant        = (e.Row.FindControl("lblPlant") as Label);
                //    Label lblLocationCDID   = (e.Row.FindControl("lblLocationCDID") as Label);
                //    Label lblQty            = (e.Row.FindControl("lblQty") as Label);
                //    decimal bal;
                //    bal                     = objMainClass.SP_GET_STOCK_FUNCTION(objMainClass.intCmpId, objMainClass.getFinYear(objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true)), lblItemCode.Text, objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true), lblPlant.Text, lblLocationCDID.Text);
                //    if (bal >= Convert.ToDecimal(lblQty.Text))
                //    {

                //    }
                //    else
                //    {
                //        lblQty.Text = Convert.ToString(bal);
                //    }
                //}
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
                    Label lblPlant = (Label)gRow.FindControl("lblPlant");
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
                    txtReturnQty.Text = lblQty.Text;
                    txtGLCode.Text = lblGLCode.Text;
                    
                    ddlPlant.SelectedValue = lblPlant.Text;
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
        #endregion

        #endregion

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