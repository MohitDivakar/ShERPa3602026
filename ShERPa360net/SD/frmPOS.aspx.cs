using Newtonsoft.Json;
using RestSharp;
using ShERPa360net.Class;
using ShERPa360net.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.SD
{
    public partial class frmPOS : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        DataTable dtItem = new DataTable();
        public SqlConnection ConnSherpa = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStringSherpa"].ConnectionString);

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
                        objBindDDL.FillVendByType(ddlCommAgent, "AGN", objMainClass.intCmpId, 1);
                        objBindDDL.FillPaymentMode(ddlPayMode);
                        objBindDDL.FillPaymentGateway(ddlPayGateway);
                        objBindDDL.FillState(ddlState);
                        objBindDDL.FillLists(ddlReference, "IR");
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
                dtColumn.ColumnName = "SRNO";
                dtItem.Columns.Add(dtColumn);


                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMID";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMCODE";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMDESC";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMGROUPID";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "JOBID";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "SERIALNO";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "SOQTY";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "UOM";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "PLANTCODE";
                dtItem.Columns.Add(dtColumn);


                dtColumn = new DataColumn();
                dtColumn.ColumnName = "LOCCD";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "PRICE";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "DISCOUNT";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "NETAMT";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "DISCBYWHOM";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "DEMOINSTALLATION";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "RATE";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "CONDID";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "CONDTYPE";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "SEGMENT";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "DISTCHNL";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "DELIDATE";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "JOBSTATUS";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "DEMINST";
                dtItem.Columns.Add(dtColumn);


                ViewState["ItemData"] = dtItem;
                gvList.DataSource = (DataTable)ViewState["ItemData"];
                gvList.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void ddlPayMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (ddlPayMode.SelectedValue == "14")
                    {
                        txtPartialAmount.Text = string.Empty;
                        //txtPartialAmount.Visible = true;
                        //rfvPartialAmount.Enabled = true;
                        //divPartial.Visible = true;

                        divTransaction.Visible = false;
                        rfvTXNID.Enabled = false;
                        rfvTXNDT.Enabled = false;
                        rfvPayGateway.Enabled = false;
                        txtTXNDT.Text = "";
                        txtTXNID.Text = "";
                        ddlPayGateway.SelectedValue = "0";
                    }
                    else if (ddlPayMode.SelectedValue == "5" || ddlPayMode.SelectedValue == "6" || ddlPayMode.SelectedValue == "12" || ddlPayMode.SelectedValue == "15")
                    {
                        txtPartialAmount.Text = string.Empty;
                        //txtPartialAmount.Visible = false;
                        //rfvPartialAmount.Enabled = false;
                        //divPartial.Visible = false;
                        divTransaction.Visible = true;
                        objBindDDL.FillPaymentGateway(ddlPayGateway);
                        rfvTXNID.Enabled = true;
                        rfvTXNDT.Enabled = true;
                        rfvPayGateway.Enabled = true;
                        txtTXNDT.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        txtPartialAmount.Text = string.Empty;
                        //txtPartialAmount.Visible = false;
                        //rfvPartialAmount.Enabled = false;
                        //divPartial.Visible = false;

                        divTransaction.Visible = false;
                        rfvTXNID.Enabled = false;
                        rfvTXNDT.Enabled = false;
                        rfvPayGateway.Enabled = false;
                        txtTXNDT.Text = "";
                        txtTXNID.Text = "";
                        ddlPayGateway.SelectedValue = "0";
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (rblSearchBy.SelectedValue == "1")
                    {
                        CheckProduct(txtSearchVaue.Text);

                    }
                    if (rblSearchBy.SelectedValue == "2")
                    {
                        DataTable dtJobidByIMEI = new DataTable();
                        dtJobidByIMEI = objMainClass.GetJobIdByIMEI(objMainClass.intCmpId, txtSearchVaue.Text);

                        if (dtJobidByIMEI.Rows.Count > 0)
                        {
                            string JOBID = Convert.ToString(dtJobidByIMEI.Rows[0]["JOBID"]);
                            CheckProduct(JOBID);
                        }
                        else
                        {
                            txtSearchVaue.Text = string.Empty;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid serial no.!');", true);
                            txtSearchVaue.Text = string.Empty;
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


        public void CalcAmt()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    decimal finalamt = 0;
                    for (int i = 0; i < gvList.Rows.Count; i++)
                    {
                        RadioButtonList rbl = ((RadioButtonList)gvList.Rows[i].FindControl("rblEWCharges"));

                        decimal ewcharges = 0;
                        if (rbl.DataSource != null)
                        {
                            string selectedValue = rbl.SelectedValue;
                            //string selectedText = rbl.SelectedItem.Text;
                            if (selectedValue != "")
                            {
                                DataTable dt = new DataTable();
                                dt = objMainClass.GetEWData(objMainClass.intCmpId, 0, 0, 0, 0, Convert.ToInt32(selectedValue), "GETEWPRICEWITHID");

                                if (dt.Rows.Count > 0)
                                {
                                    ewcharges = Convert.ToDecimal(dt.Rows[0]["PRICE"]);
                                }
                            }
                        }

                        string lblNetAmt = ((Label)gvList.Rows[i].FindControl("lblNetAmt")).Text;
                        finalamt = ewcharges + finalamt + Convert.ToDecimal(lblNetAmt);

                        CheckBox chkDemoInstallation = (CheckBox)gvList.Rows[i].FindControl("chkDemoInstallation");
                        Label lblDEMINST = (Label)gvList.Rows[i].FindControl("lblDEMINST");

                        if (lblDEMINST.Text == "1")
                        {
                            chkDemoInstallation.Checked = true;
                        }
                        else
                        {
                            chkDemoInstallation.Checked = false;
                        }
                    }

                    lblTotalAmt.Text = Convert.ToString(finalamt);

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


        public void CheckProduct(string JOBID)
        {

            try
            {
                if (Session["USERID"] != null)
                {
                    int error = 0;
                    for (int i = 0; i < gvList.Rows.Count; i++)
                    {
                        string gvjobid = ((Label)gvList.Rows[i].FindControl("lblJobid")).Text;
                        if (gvjobid == JOBID)
                        {
                            error++;
                        }
                    }

                    if (error > 0)
                    {
                        txtSearchVaue.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('This job id/Serial no already added for another  line item.!');", true);
                        txtSearchVaue.Text = string.Empty;
                    }
                    else
                    {
                        DataTable dt = new DataTable();
                        dt = objMainClass.GetJSDetails(JOBID);

                        if (dt.Rows.Count > 0)
                        {
                            string plantcd = Convert.ToString(dt.Rows[0]["PLANTCD"]);

                            string PLantRights = string.Empty;
                            string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                            for (int i = 0; i < plantArray.Count(); i++)
                            {
                                if (plantArray[i].Trim() == plantcd)
                                {
                                    PLantRights = plantcd;
                                }
                            }
                            if (PLantRights.Length > 0)
                            {
                                if (Convert.ToString(dt.Rows[0]["JOBSTATUS"]) == "23" || Convert.ToString(dt.Rows[0]["JOBSTATUS"]) == "3")
                                {
                                    txtSearchVaue.Text = string.Empty;
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record found or Job id already Closed/Cancelled.!');", true);
                                    txtSearchVaue.Text = string.Empty;
                                }
                                else
                                {
                                    if ((Convert.ToString(dt.Rows[0]["JOBSTATUS"]) == "53" && Convert.ToString(dt.Rows[0]["STAGEID"]) == "46") || (Convert.ToString(dt.Rows[0]["JOBSTATUS"]) == "21" && Convert.ToString(dt.Rows[0]["STAGEID"]) == "24"))
                                    {
                                        string JOBSHEETID = objMainClass.strConvertZeroPadding(Convert.ToString(dt.Rows[0]["JOBID"]));

                                        DataTable dtjobrate = new DataTable();
                                        dtjobrate = objMainClass.CheckRateJobDuplicate(objMainClass.intCmpId, JOBSHEETID, "GETJOBIDSEARCHNEW");
                                        if (dtjobrate.Rows.Count > 0)
                                        {
                                            decimal jobrate = Convert.ToDecimal(dtjobrate.Rows[0]["CUSTOMERPRCE"]);
                                            decimal taxrate = Convert.ToDecimal(dtjobrate.Rows[0]["RATE"]);
                                            int condid = Convert.ToInt32(dtjobrate.Rows[0]["CONDID"]);
                                            string conditype = Convert.ToString(dtjobrate.Rows[0]["CONDTYPE"]);
                                            string ITEMCODE = "";
                                            DataTable dtPOItem = new DataTable();
                                            dtPOItem = objMainClass.GETITEMFROMPO(JOBSHEETID);
                                            if (dtPOItem.Rows.Count > 0)
                                            {
                                                ITEMCODE = Convert.ToString(dtPOItem.Rows[0]["ITEMCODE"]);

                                            }
                                            else
                                            {
                                                ITEMCODE = Convert.ToString(dt.Rows[0]["ITEMCODE"]);
                                            }
                                            string IMEI = Convert.ToString(dt.Rows[0]["IMEINO"]);
                                            string PLANT = Convert.ToString(dt.Rows[0]["PLANTCD"]);
                                            string LOCATION = Convert.ToString(dt.Rows[0]["LOCCD"]);
                                            string SEGMENT = Convert.ToString(dt.Rows[0]["SEGCODE"]);
                                            string DISTCHNL = Convert.ToString(dt.Rows[0]["DISTCHNL"]);


                                            //MessageBox.Show("Service is running","Info",MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1,MessageBoxOptions.ServiceNotification);

                                            int deminst = 0;
                                            string choice = hdnUserChoice.Value;
                                            if (choice == "Yes")
                                            {
                                                deminst = 1;
                                            }
                                            else
                                            {
                                                deminst = 0;
                                            }
                                            //System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Is Demo/Installation required for this product..?", "Confirmation", System.Windows.Forms.MessageBoxButtons.YesNo);
                                            //if (result == System.Windows.Forms.DialogResult.Yes)
                                            //{
                                            //    deminst = 1;
                                            //}
                                            //else
                                            //{
                                            //    deminst = 0;
                                            //}

                                            DataTable dtSO = new DataTable();
                                            dtSO = objMainClass.GetSODetails(objMainClass.intCmpId, "PSO", IMEI, "SODETAILS", "", "");
                                            if (dtSO.Rows.Count > 0)
                                            {
                                                string sononew = Convert.ToString(dtSO.Rows[0]["SONO"]);
                                                DataTable dtSOR = new DataTable();
                                                dtSOR = objMainClass.GetSODetails(objMainClass.intCmpId, "SOR", IMEI, "SORDETAILSNEW", "", sononew);
                                                if (dtSOR.Rows.Count > 0)
                                                {
                                                    DataTable dtPODtl = new DataTable();
                                                    dtPODtl = objMainClass.CheckPOSOImeiNo(objMainClass.intCmpId, IMEI, "CHECKPOWITHRETURN");
                                                    decimal qty = Convert.ToDecimal(dtPODtl.Rows[0]["BALQTY"]);
                                                    if (qty > 0)
                                                    {
                                                        DataTable dtItemDet = new DataTable();
                                                        dtItemDet = objMainClass.GetItemDetailsByItemCode(objMainClass.intCmpId, ITEMCODE, 1, "ITEMMASTERSEARCH");
                                                        if (dtItemDet.Rows.Count > 0)
                                                        {
                                                            decimal bal = objMainClass.SP_GET_STOCK_FUNCTION(objMainClass.intCmpId, objMainClass.getFinYear(objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true)), ITEMCODE, objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true), PLANT, LOCATION);
                                                            if (bal > 0)
                                                            {

                                                                DataTable dtitemgrid = (DataTable)ViewState["ItemData"];
                                                                if (dtitemgrid.Rows.Count > 0)
                                                                {
                                                                    DataRow lastRow = dtitemgrid.Rows[dtitemgrid.Rows.Count - 1];
                                                                    int id = Convert.ToInt32(lastRow["SRNO"]) + 1;
                                                                    dtitemgrid.Rows.Add(id, dtItemDet.Rows[0]["ITEMID"], dtItemDet.Rows[0]["ITEMCODE"], dtItemDet.Rows[0]["ITEMDESC"], dtItemDet.Rows[0]["ITEMGRP"], JOBID, IMEI, 1, dtItemDet.Rows[0]["UOM"], PLANT, LOCATION, jobrate, 0, jobrate, "", 0, taxrate, condid, conditype, SEGMENT, DISTCHNL, DateTime.Now.AddDays(2).ToShortDateString(), Convert.ToString(dt.Rows[0]["STAGEID"]), deminst);
                                                                }
                                                                else
                                                                {
                                                                    dtitemgrid.Rows.Add(1, dtItemDet.Rows[0]["ITEMID"], dtItemDet.Rows[0]["ITEMCODE"], dtItemDet.Rows[0]["ITEMDESC"], dtItemDet.Rows[0]["ITEMGRP"], JOBID, IMEI, 1, dtItemDet.Rows[0]["UOM"], PLANT, LOCATION, jobrate, 0, jobrate, "", 0, taxrate, condid, conditype, SEGMENT, DISTCHNL, DateTime.Now.AddDays(2).ToShortDateString(), Convert.ToString(dt.Rows[0]["STAGEID"]), deminst);
                                                                }

                                                                ViewState["ItemData"] = dtitemgrid;
                                                                gvList.DataSource = (DataTable)ViewState["ItemData"];
                                                                gvList.DataBind();
                                                            }
                                                            else
                                                            {
                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Stock not available.!' );", true);
                                                                txtSearchVaue.Text = string.Empty;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Item details not found.!' );", true);
                                                            txtSearchVaue.Text = string.Empty;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('SO or Purchase Return is already created for mentioned IMEI No.!' );", true);
                                                        txtSearchVaue.Text = string.Empty;
                                                    }

                                                }
                                                else
                                                {
                                                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('SO is already created for mentioned IMEI No.! SO No.:" + Convert.ToString(dtSO.Rows[0]["SONO"]) + " ' );", true);
                                                    //txtSearchVaue.Text = string.Empty;

                                                    DataTable dtPODtl = new DataTable();
                                                    dtPODtl = objMainClass.CheckPOSOImeiNo(objMainClass.intCmpId, IMEI, "CHECKPOWITHRETURN");
                                                    decimal qty = Convert.ToDecimal(dtPODtl.Rows[0]["BALQTY"]);
                                                    if (qty > 1)
                                                    {
                                                        DataTable dtItemDet = new DataTable();
                                                        dtItemDet = objMainClass.GetItemDetailsByItemCode(objMainClass.intCmpId, ITEMCODE, 1, "ITEMMASTERSEARCH");
                                                        if (dtItemDet.Rows.Count > 0)
                                                        {
                                                            decimal bal = objMainClass.SP_GET_STOCK_FUNCTION(objMainClass.intCmpId, objMainClass.getFinYear(objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true)), ITEMCODE, objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true), PLANT, LOCATION);
                                                            if (bal > 0)
                                                            {

                                                                DataTable dtitemgrid = (DataTable)ViewState["ItemData"];
                                                                if (dtitemgrid.Rows.Count > 0)
                                                                {
                                                                    DataRow lastRow = dtitemgrid.Rows[dtitemgrid.Rows.Count - 1];
                                                                    int id = Convert.ToInt32(lastRow["SRNO"]) + 1;
                                                                    dtitemgrid.Rows.Add(id, dtItemDet.Rows[0]["ITEMID"], dtItemDet.Rows[0]["ITEMCODE"], dtItemDet.Rows[0]["ITEMDESC"], dtItemDet.Rows[0]["ITEMGRP"], JOBID, IMEI, 1, dtItemDet.Rows[0]["UOM"], PLANT, LOCATION, jobrate, 0, jobrate, "", 0, taxrate, condid, conditype, SEGMENT, DISTCHNL, DateTime.Now.AddDays(2).ToShortDateString(), Convert.ToString(dt.Rows[0]["STAGEID"]), deminst);
                                                                }
                                                                else
                                                                {
                                                                    dtitemgrid.Rows.Add(1, dtItemDet.Rows[0]["ITEMID"], dtItemDet.Rows[0]["ITEMCODE"], dtItemDet.Rows[0]["ITEMDESC"], dtItemDet.Rows[0]["ITEMGRP"], JOBID, IMEI, 1, dtItemDet.Rows[0]["UOM"], PLANT, LOCATION, jobrate, 0, jobrate, "", 0, taxrate, condid, conditype, SEGMENT, DISTCHNL, DateTime.Now.AddDays(2).ToShortDateString(), Convert.ToString(dt.Rows[0]["STAGEID"]), deminst);
                                                                }

                                                                ViewState["ItemData"] = dtitemgrid;
                                                                gvList.DataSource = (DataTable)ViewState["ItemData"];
                                                                gvList.DataBind();
                                                            }
                                                            else
                                                            {
                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Stock not available.!' );", true);
                                                                txtSearchVaue.Text = string.Empty;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Item details not found.!' );", true);
                                                            txtSearchVaue.Text = string.Empty;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('SO or Purchase Return is already created for mentioned IMEI No.!' );", true);
                                                        txtSearchVaue.Text = string.Empty;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                DataTable dtPODtl = new DataTable();
                                                dtPODtl = objMainClass.CheckPOSOImeiNo(objMainClass.intCmpId, IMEI, "CHECKPOWITHRETURN");
                                                decimal qty = Convert.ToDecimal(dtPODtl.Rows[0]["BALQTY"]);
                                                if (qty > 0)
                                                {
                                                    DataTable dtItemDet = new DataTable();
                                                    dtItemDet = objMainClass.GetItemDetailsByItemCode(objMainClass.intCmpId, ITEMCODE, 1, "ITEMMASTERSEARCH");
                                                    if (dtItemDet.Rows.Count > 0)
                                                    {
                                                        decimal bal = objMainClass.SP_GET_STOCK_FUNCTION(objMainClass.intCmpId, objMainClass.getFinYear(objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true)), ITEMCODE, objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true), PLANT, LOCATION);
                                                        if (bal > 0)
                                                        {

                                                            DataTable dtitemgrid = (DataTable)ViewState["ItemData"];
                                                            if (dtitemgrid.Rows.Count > 0)
                                                            {
                                                                DataRow lastRow = dtitemgrid.Rows[dtitemgrid.Rows.Count - 1];
                                                                int id = Convert.ToInt32(lastRow["SRNO"]) + 1;
                                                                dtitemgrid.Rows.Add(id, dtItemDet.Rows[0]["ITEMID"], dtItemDet.Rows[0]["ITEMCODE"], dtItemDet.Rows[0]["ITEMDESC"], dtItemDet.Rows[0]["ITEMGRP"], JOBID, IMEI, 1, dtItemDet.Rows[0]["UOM"], PLANT, LOCATION, jobrate, 0, jobrate, "", 0, taxrate, condid, conditype, SEGMENT, DISTCHNL, DateTime.Now.AddDays(2).ToShortDateString(), Convert.ToString(dt.Rows[0]["STAGEID"]), deminst);
                                                            }
                                                            else
                                                            {
                                                                dtitemgrid.Rows.Add(1, dtItemDet.Rows[0]["ITEMID"], dtItemDet.Rows[0]["ITEMCODE"], dtItemDet.Rows[0]["ITEMDESC"], dtItemDet.Rows[0]["ITEMGRP"], JOBID, IMEI, 1, dtItemDet.Rows[0]["UOM"], PLANT, LOCATION, jobrate, 0, jobrate, "", 0, taxrate, condid, conditype, SEGMENT, DISTCHNL, DateTime.Now.AddDays(2).ToShortDateString(), Convert.ToString(dt.Rows[0]["STAGEID"]), deminst);
                                                            }

                                                            ViewState["ItemData"] = dtitemgrid;
                                                            gvList.DataSource = (DataTable)ViewState["ItemData"];
                                                            gvList.DataBind();
                                                        }
                                                        else
                                                        {
                                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Stock not available.!' );", true);
                                                            txtSearchVaue.Text = string.Empty;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Item details not found.!' );", true);
                                                        txtSearchVaue.Text = string.Empty;
                                                    }
                                                }
                                                else
                                                {
                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('SO or Purchase Return is already created for mentioned IMEI No.!' );", true);
                                                    txtSearchVaue.Text = string.Empty;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            txtSearchVaue.Text = string.Empty;
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Job id rate not found in ratecard..!');", true);
                                            txtSearchVaue.Text = string.Empty;
                                        }



                                    }
                                    else
                                    {
                                        txtSearchVaue.Text = string.Empty;
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Job id must be in Phy. Doc. Var. or Forward Waybill Generated Stage..!');", true);
                                        txtSearchVaue.Text = string.Empty;
                                    }
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have plant rights.');$('#txtJobId').focus();", true);
                            }
                        }
                        else
                        {
                            txtSearchVaue.Text = string.Empty;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid job id.!');", true);
                            txtSearchVaue.Text = string.Empty;
                        }

                        CalcAmt();

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

        protected void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((TextBox)sender).NamingContainer;
                    Label lblSRNO = (Label)grdrow.FindControl("lblSRNO");
                    Label lblPrice = (Label)grdrow.FindControl("lblPrice");
                    TextBox txtDiscount = (TextBox)grdrow.FindControl("txtDiscount");
                    Label lblNetAmt = (Label)grdrow.FindControl("lblNetAmt");
                    decimal netamt = Convert.ToDecimal(Convert.ToDecimal(lblPrice.Text) - Convert.ToDecimal(txtDiscount.Text));
                    lblNetAmt.Text = Convert.ToString(netamt);

                    CheckBox chkDemoInstallation = (CheckBox)grdrow.FindControl("chkDemoInstallation");
                    Label DEMINST = (Label)grdrow.FindControl("DEMINST");

                    int demint = 0;
                    if (chkDemoInstallation.Checked == true)
                    {
                        demint = 1;
                    }


                    DataTable dt = (DataTable)ViewState["ItemData"];
                    DataRow dr = dt.Select("SRNO = '" + lblSRNO.Text + "'")[0];
                    dr[12] = txtDiscount.Text;
                    dr[13] = netamt;
                    dr[23] = demint;

                    gvList.DataSource = (DataTable)ViewState["ItemData"];
                    gvList.DataBind();
                    CalcAmt();

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

        protected void txtByWhome_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((TextBox)sender).NamingContainer;
                    Label lblSRNO = (Label)grdrow.FindControl("lblSRNO");
                    TextBox txtByWhome = (TextBox)grdrow.FindControl("txtByWhome");

                    CheckBox chkDemoInstallation = (CheckBox)grdrow.FindControl("chkDemoInstallation");
                    Label DEMINST = (Label)grdrow.FindControl("DEMINST");

                    int demint = 0;
                    if (chkDemoInstallation.Checked == true)
                    {
                        demint = 1;
                    }

                    DataTable dt = (DataTable)ViewState["ItemData"];
                    DataRow dr = dt.Select("SRNO = '" + lblSRNO.Text + "'")[0];
                    dr[14] = txtByWhome.Text;
                    dr[23] = demint;

                    gvList.DataSource = (DataTable)ViewState["ItemData"];
                    gvList.DataBind();
                    CalcAmt();

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

        protected void txtMobileNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dtAdrres = new DataTable();
                    dtAdrres = objMainClass.GetCustAddress(objMainClass.intCmpId, txtMobileNo.Text, "GETCUSTADDRESS");
                    if (dtAdrres.Rows.Count > 0)
                    {
                        txtName.Text = Convert.ToString(dtAdrres.Rows[0]["Cust. Name"]);
                        txtAddress.Text = Convert.ToString(dtAdrres.Rows[0]["Address 1"]);
                        txtAddress2.Text = Convert.ToString(dtAdrres.Rows[0]["Address 2"]);
                        txtAddress3.Text = Convert.ToString(dtAdrres.Rows[0]["Address 3"]);
                        txtPincode.Text = Convert.ToString(dtAdrres.Rows[0]["Pincode"]);
                        //txtPincode_TextChanged(1, e);
                        ddlState.SelectedValue = Convert.ToString(dtAdrres.Rows[0]["State ID"]);
                        ddlState_SelectedIndexChanged(1, e);
                        //ddlCity.SelectedItem.Text = Convert.ToString(dtAdrres.Rows[0]["City"]);

                        //string valuedd = ddlCity.Items.FindByText(Convert.ToString(dtAdrres.Rows[0]["City"])).;
                        //ddlCity.SelectedItem.Value = valuedd;
                        txtGSTNO.Text = Convert.ToString(dtAdrres.Rows[0]["GSTNO"]);

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

        protected void txtPincode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (txtPincode.Text.Length == 6)
                    {
                        try
                        {

                            if (!System.Text.RegularExpressions.Regex.IsMatch(txtPincode.Text, "[0-9]{6}$"))
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Only Numbers Aloowed.');", true);
                                txtPincode.Text = string.Empty;
                            }
                            else
                            {
                                DataTable ds = new DataTable();
                                ds = objMainClass.SELECT_CITY_BYPINCODE(txtPincode.Text.Trim());
                                if (ds.Rows.Count > 0)
                                {
                                    ddlState.SelectedValue = ds.Rows[0]["STATE_ID"].ToString();
                                    ddlCity.DataSource = string.Empty;
                                    ddlCity.DataBind();
                                    objBindDDL.FillCity(ddlCity, ddlState.SelectedValue);
                                    //ddlCity.SelectedItem.Text = ds.Rows[0]["CITY_NAME"].ToString();
                                }
                                else
                                {
                                    ddlState.SelectedIndex = 0;
                                    ddlCity.SelectedIndex = 0;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
                        }
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

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    if (ddlState.SelectedIndex > 0)
                    {
                        objBindDDL.FillCity(ddlCity, ddlState.SelectedValue);
                    }
                    else
                    {
                        objBindDDL.FillCity(ddlCity);
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            btnSave.Visible = false;
            try
            {
                if (Session["USERID"] != null)
                {
                    if (ddlPayMode.SelectedValue == "9" && (Convert.ToDecimal(txtPartialAmount.Text.Trim()) != Convert.ToDecimal(lblTotalAmt.Text.Trim())))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Received Amount and Total amount needs to ba same for Cash Collact..!');", true);
                    }
                    else if ((ddlReference.SelectedValue == "22492" || ddlReference.SelectedValue == "154") && (txtRefName.Text == "" && txtRefName.Text == string.Empty && txtRefName.Text == null))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please enter referal by name..!');", true);
                    }
                    else
                    {
                        if (gvList.Rows.Count > 0)
                        {
                            List<string> distinctValues = new List<string>();
                            foreach (GridViewRow row in gvList.Rows)
                            {
                                // Assuming you want distinct values from the first cell (index 0)
                                string cellValue = row.Cells[9].Text;
                                cellValue = ((Label)row.FindControl("lblPLANTCODE")).Text;
                                if (!distinctValues.Contains(cellValue))
                                {
                                    distinctValues.Add(cellValue);
                                }
                            }

                            if (distinctValues.Count <= 1)
                            {
                                btnSave.Enabled = false;

                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                decimal totdiscount = 0;
                                decimal totalitemamount = 0;
                                decimal totaltaxamount = 0;
                                decimal totalbaseamount = 0;
                                decimal EWTOTAL = 0;
                                int srno = 0;
                                string segment = "1043";
                                string distchnl = "50";
                                SOCreateClassNewV1 objSOCreateClass = new SOCreateClassNewV1();
                                List<LSTITEMDETAILSNEWV1> ItemDetails = new List<LSTITEMDETAILSNEWV1>();
                                List<LSTTAXDETAILSNEW> TaxDetails = new List<LSTTAXDETAILSNEW>();

                                CreateDCMST objCreateDCMST = new CreateDCMST();
                                List<CreateDCDTL> objCreateDCDTL = new List<CreateDCDTL>();

                                CreateSIMstNew objCreateSIMstNew = new CreateSIMstNew();
                                List<CreateSIDtlNew> objCreateSIDtlNew = new List<CreateSIDtlNew>();
                                List<CreateSITaxNew> objCreateSITaxNew = new List<CreateSITaxNew>();

                                for (int i = 0; i < gvList.Rows.Count; i++)
                                {
                                    Label lblSRNO = (Label)gvList.Rows[i].FindControl("lblSRNO");
                                    Label lblItemID = (Label)gvList.Rows[i].FindControl("lblItemID");
                                    Label lblItemCode = (Label)gvList.Rows[i].FindControl("lblItemCode");
                                    Label lblItemDesc = (Label)gvList.Rows[i].FindControl("lblItemDesc");
                                    Label lblITEMGROUPID = (Label)gvList.Rows[i].FindControl("lblITEMGROUPID");
                                    Label lblJobid = (Label)gvList.Rows[i].FindControl("lblJobid");
                                    Label lblSerialNo = (Label)gvList.Rows[i].FindControl("lblSerialNo");
                                    Label lblSOQTY = (Label)gvList.Rows[i].FindControl("lblSOQTY");
                                    Label lblUOM = (Label)gvList.Rows[i].FindControl("lblUOM");
                                    Label lblPLANTCODE = (Label)gvList.Rows[i].FindControl("lblPLANTCODE");
                                    Label lblLOCCD = (Label)gvList.Rows[i].FindControl("lblLOCCD");
                                    Label lblPrice = (Label)gvList.Rows[i].FindControl("lblPrice");
                                    TextBox txtDiscount = (TextBox)gvList.Rows[i].FindControl("txtDiscount");
                                    Label lblNetAmt = (Label)gvList.Rows[i].FindControl("lblNetAmt");
                                    TextBox txtByWhome = (TextBox)gvList.Rows[i].FindControl("txtByWhome");
                                    CheckBox chkDemoInstallation = (CheckBox)gvList.Rows[i].FindControl("chkDemoInstallation");
                                    Label lblRATE = (Label)gvList.Rows[i].FindControl("lblRATE");
                                    Label lblCONDID = (Label)gvList.Rows[i].FindControl("lblCONDID");
                                    Label lblCONDTYPE = (Label)gvList.Rows[i].FindControl("lblCONDTYPE");
                                    Label lblSEGMENT = (Label)gvList.Rows[i].FindControl("lblSEGMENT");
                                    Label lblDISTCHNL = (Label)gvList.Rows[i].FindControl("lblDISTCHNL");
                                    TextBox txtDeliveryDate = (TextBox)gvList.Rows[i].FindControl("txtDeliveryDate");
                                    Label lblJOBSTATUS = (Label)gvList.Rows[i].FindControl("lblJOBSTATUS");

                                    int EWID = 0;
                                    string EWDESC = "";
                                    decimal EWPRICE = 0;

                                    RadioButtonList rblEWCharges = (RadioButtonList)gvList.Rows[i].FindControl("rblEWCharges");
                                    if (rblEWCharges.Items.Count > 0)
                                    {
                                        if (rblEWCharges.SelectedIndex != -1)
                                        {
                                            EWID = Convert.ToInt32(rblEWCharges.SelectedItem.Value);
                                            string[] EWTITLE = Convert.ToString(rblEWCharges.SelectedItem.Text).Split('-');
                                            EWDESC = Convert.ToString(EWTITLE[0]).Trim();
                                            EWPRICE = Convert.ToDecimal(Convert.ToString(EWTITLE[1]).Trim());
                                            EWTOTAL = EWTOTAL + EWPRICE;
                                        }
                                    }

                                    segment = lblSEGMENT.Text;
                                    distchnl = lblDISTCHNL.Text;
                                    string costcenter = "1081";
                                    DataTable dtCostCenter = new DataTable();
                                    dtCostCenter = objMainClass.GetCostCenter(lblPLANTCODE.Text, lblLOCCD.Text);
                                    if (dtCostCenter.Rows.Count > 0)
                                    {
                                        costcenter = Convert.ToString(dtCostCenter.Rows[0]["CSTCENTCD"]);
                                    }

                                    decimal discount = Convert.ToDecimal(txtDiscount.Text);

                                    decimal baseaamt = Convert.ToDecimal((Convert.ToDecimal(lblNetAmt.Text) * 100) / (Convert.ToDecimal(lblRATE.Text) + 100));
                                    decimal taxamt = (baseaamt * Convert.ToDecimal(lblRATE.Text) / 100);
                                    decimal brate = baseaamt + discount;
                                    decimal camount = baseaamt + discount;

                                    int demo = 0;
                                    int installation = 0;
                                    if (chkDemoInstallation.Checked == true)
                                    {
                                        demo = 1;
                                        installation = 1;
                                    }


                                    totdiscount = totdiscount + discount;
                                    totalitemamount = totalitemamount + brate;
                                    totaltaxamount = totaltaxamount + taxamt;
                                    totalbaseamount = totalbaseamount + baseaamt;


                                    ItemDetails.Add(new LSTITEMDETAILSNEWV1
                                    {
                                        CAMOUNT = Convert.ToString(camount),
                                        CHANGEREASON = "",
                                        COSTCENTER = costcenter,
                                        CUSTPARTDESC = "",
                                        CUSTPARTNO = "",
                                        DELIDT = txtDeliveryDate.Text,
                                        DISCAMT = Convert.ToString(txtDiscount.Text),
                                        GLCODE = "10010000",
                                        GRADE = "GRADE A",
                                        IMEINO = lblSerialNo.Text,
                                        ITEMCODE = lblItemCode.Text,
                                        ITEMDESC = lblItemDesc.Text,
                                        ITEMGROUPID = lblITEMGROUPID.Text,
                                        ITEMID = lblItemID.Text,
                                        ITEMTEXT = "SO CREATED FROM POS",
                                        JOBID = lblJobid.Text,
                                        LOCCD = lblLOCCD.Text,
                                        OLDITEMID = "0",
                                        PLANTCODE = lblPLANTCODE.Text,
                                        PRFCNT = "1000",
                                        RATE = Convert.ToString(brate),
                                        SCHEMEID = "0",
                                        SONO = "",
                                        SOQTY = lblSOQTY.Text,
                                        SRNO = Convert.ToString(lblSRNO.Text),
                                        TAXAMT = "0",
                                        UOM = lblUOM.Text,
                                        DEMO = demo,
                                        INSTALLATION = installation,
                                        APPROVEBY = txtByWhome.Text,
                                        EWID = EWID,
                                        EWDESC = EWDESC,
                                        EWPRICE = EWPRICE

                                    });

                                    TaxDetails.Add(new LSTTAXDETAILSNEW
                                    {
                                        BASEAMT = Convert.ToString(baseaamt),
                                        CONDID = lblCONDID.Text,
                                        CONDTYPE = lblCONDTYPE.Text,
                                        GLCODE = "10010000",
                                        OPERATOR = "+",
                                        PID = "0",
                                        RATE = Convert.ToString(lblRATE.Text),
                                        SONO = "",
                                        SOSRNO = Convert.ToString(lblSRNO.Text),
                                        SRNO = Convert.ToString(lblSRNO.Text),
                                        TAXAMT = Convert.ToString(taxamt)
                                    });

                                    objCreateDCDTL.Add(new CreateDCDTL
                                    {
                                        CAMOUNT = Convert.ToString(camount),
                                        CMPID = objMainClass.intCmpId,
                                        CSTCENTCD = costcenter,
                                        DISCAMT = Convert.ToString(txtDiscount.Text),
                                        DOCNO = "",
                                        GLCODE = "10010000",
                                        ITEMDESC = lblItemDesc.Text,
                                        ITEMGRPID = lblITEMGROUPID.Text,
                                        ITEMID = lblItemID.Text,
                                        ITEMTEXT = "DC CREATED FROM POS",
                                        LOCCD = lblLOCCD.Text,
                                        PLANTCD = lblPLANTCODE.Text,
                                        PRFCNT = "1000",
                                        RATE = Convert.ToString(brate),
                                        SONO = "",
                                        SOQTY = lblSOQTY.Text,
                                        SOSRNO = Convert.ToString(lblSRNO.Text),
                                        SRNO = Convert.ToString(lblSRNO.Text),
                                        TAXAMT = "0",
                                        UOM = lblUOM.Text,

                                    });

                                    objCreateSIDtlNew.Add(new CreateSIDtlNew
                                    {
                                        CAMOUNT = Convert.ToString(camount),
                                        CMPID = objMainClass.intCmpId,
                                        //COUPONVALUE = "0",
                                        CSTCENTCD = costcenter,
                                        DCNO = "",
                                        DCSRNO = Convert.ToString(lblSRNO.Text),
                                        DISCAMT = Convert.ToString(txtDiscount.Text),
                                        GLCODE = "10010000",
                                        ITEMCODE = lblItemCode.Text,
                                        ITEMDESC = lblItemDesc.Text,
                                        ITEMGRPID = lblITEMGROUPID.Text,
                                        ITEMID = lblItemID.Text,
                                        ITEMTEXT = "SI CREATED FROM POS",
                                        JOBID = lblJobid.Text,
                                        JOBSTATUS = lblJOBSTATUS.Text,
                                        LOCCD = lblLOCCD.Text,
                                        PLANTCD = lblPLANTCODE.Text,
                                        PRFCNT = "1000",
                                        PRODMAKE = "",
                                        PRODMODEL = "",
                                        QTY = lblSOQTY.Text,
                                        RATE = Convert.ToString(brate),
                                        //SALESWARRANTY = "",
                                        SINO = "",
                                        SITYPE = "SIT",
                                        //SLRREASON = "",
                                        SRNO = Convert.ToString(lblSRNO.Text),
                                        UOM = lblUOM.Text
                                    });

                                    objCreateSITaxNew.Add(new CreateSITaxNew
                                    {
                                        BASEAMT = Convert.ToString(baseaamt),
                                        CMPID = objMainClass.intCmpId,
                                        CONDID = lblCONDID.Text,
                                        CONDORDER = Convert.ToString(lblSRNO.Text),
                                        CONDTYPE = lblCONDTYPE.Text,
                                        GLCODE = "10010000",
                                        OPERATOR = "+",
                                        PID = "0",
                                        RATE = Convert.ToString(lblRATE.Text),
                                        SINO = "",
                                        SISRNO = Convert.ToString(lblSRNO.Text),
                                        SRNO = Convert.ToString(lblSRNO.Text),
                                        TAXAMT = Convert.ToString(taxamt)
                                    });

                                }





                                objSOCreateClass.BILLTOCODE = "0000010003";
                                objSOCreateClass.CITY = ddlCity.SelectedItem.Text;
                                objSOCreateClass.COMMAGENT = ddlCommAgent.SelectedItem.Value;
                                objSOCreateClass.CUSTADD1 = txtAddress.Text.ToUpper();
                                objSOCreateClass.CUSTADD2 = txtAddress2.Text.ToUpper();
                                objSOCreateClass.CUSTADD3 = txtAddress3.Text.ToUpper();
                                objSOCreateClass.CUSTEMAILID = "";
                                objSOCreateClass.CUSTMOBILENO = txtMobileNo.Text;
                                objSOCreateClass.CUSTNAME = txtName.Text.ToUpper();
                                objSOCreateClass.DISTCHNL = distchnl;
                                objSOCreateClass.ENTITYID = "";
                                objSOCreateClass.GOKWIKFLAG = "";
                                objSOCreateClass.JOBID = "";
                                objSOCreateClass.NETSOAMT = Convert.ToString(totalbaseamount + totaltaxamount + EWTOTAL);
                                objSOCreateClass.NETTAXAMT = Convert.ToString(totaltaxamount);
                                objSOCreateClass.NETVALUEAMT = Convert.ToString(totalbaseamount);
                                if (ddlPayMode.SelectedValue == "5" || ddlPayMode.SelectedValue == "6" || ddlPayMode.SelectedValue == "12" || ddlPayMode.SelectedValue == "15")
                                {
                                    objSOCreateClass.PAYGATEWAY = ddlPayGateway.SelectedValue;
                                    objSOCreateClass.TXNDT = txtTXNDT.Text;
                                    objSOCreateClass.TXNNO = txtTXNID.Text;
                                }
                                else
                                {
                                    //objSOCreateClass.PAYGATEWAY = "0";
                                    //objSOCreateClass.TXNDT = "";
                                    //objSOCreateClass.TXNNO = "";
                                }

                                objSOCreateClass.PAYMENTTERMS = "ADV";
                                objSOCreateClass.PAYMODE = Convert.ToInt32(ddlPayMode.SelectedValue);
                                objSOCreateClass.PINCODE = Convert.ToString(txtPincode.Text);
                                objSOCreateClass.PREPAIDAMT = txtPartialAmount.Text;
                                objSOCreateClass.REFDT = DateTime.Now.ToShortDateString();
                                objSOCreateClass.REFNO = Convert.ToString(DateTime.Now).Replace("-", "").Replace(":", "").Replace(".", "").Replace(" ", "");
                                objSOCreateClass.REMAINAMT = Convert.ToString((totalbaseamount + totaltaxamount + EWTOTAL) - Convert.ToDecimal(txtPartialAmount.Text));
                                objSOCreateClass.REMARKS = "SO CREATED FROM POS";
                                objSOCreateClass.SALESFORM = 11411;
                                objSOCreateClass.SCHEMEID = 11913;
                                objSOCreateClass.SEGMENT = segment;
                                objSOCreateClass.SHIPTOCODE = "0000010003";
                                objSOCreateClass.SODATE = DateTime.Now.ToShortDateString();
                                objSOCreateClass.SONO = "";
                                objSOCreateClass.SOTYPE = "PSO";
                                objSOCreateClass.STATEID = Convert.ToInt32(ddlState.SelectedValue);
                                objSOCreateClass.STATUS = 57;
                                objSOCreateClass.TOTALDISCOUNTAMT = Convert.ToString(totdiscount);

                                objSOCreateClass.UTMCAMPAIGN = "";
                                objSOCreateClass.UTMMEDIUM = "";
                                objSOCreateClass.UTMSOURCE = "";
                                objSOCreateClass.TAXDETAILS = TaxDetails;
                                objSOCreateClass.ITEMDETAILS = ItemDetails;
                                objSOCreateClass.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                objSOCreateClass.GSTNO = txtGSTNO.Text;

                                objSOCreateClass.REFERAL = ddlReference.SelectedItem.Text;
                                objSOCreateClass.REFEREALNAME = txtRefName.Text;


                                objCreateDCMST.CMPID = objMainClass.intCmpId;
                                objCreateDCMST.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                objCreateDCMST.DISTCHNL = distchnl;
                                objCreateDCMST.DOCNO = "";
                                objCreateDCMST.DODATE = DateTime.Now.ToShortDateString();
                                objCreateDCMST.DOTYPE = "DCS";
                                objCreateDCMST.NETDOAMOUNT = Convert.ToString(totalbaseamount + totaltaxamount + EWTOTAL);
                                objCreateDCMST.REMARK = "DC CREATED FROM POS";
                                objCreateDCMST.SEGMENT = segment;
                                objCreateDCMST.STATUS = 57;


                                objCreateSIMstNew.ADJAMT = "0";
                                objCreateSIMstNew.AGENTCD = "";
                                //objCreateSIMstNew.BILLADDID = "";
                                objCreateSIMstNew.BILLTOPARTY = "0000010003";
                                objCreateSIMstNew.BILLTOPARTYNM = "RETAIL INVOICE (OOW)";
                                objCreateSIMstNew.CMPID = objMainClass.intCmpId;
                                objCreateSIMstNew.CNCLREASON = "";
                                objCreateSIMstNew.COUPONNO = "";
                                objCreateSIMstNew.CREATEBY = Convert.ToInt32(Session["USERID"]);



                                objCreateSIMstNew.CreateSITaxNewList = objCreateSITaxNew;
                                objCreateSIMstNew.DISCAMT = Convert.ToString(totdiscount);
                                objCreateSIMstNew.DISTCHNL = distchnl;
                                objCreateSIMstNew.JOBID = "";
                                objCreateSIMstNew.NETAMT = Convert.ToString(totalbaseamount);
                                objCreateSIMstNew.NETTAXAMT = Convert.ToString(totaltaxamount);
                                objCreateSIMstNew.OTHERDISCAMT = "0";
                                objCreateSIMstNew.OTHERS = "0";
                                objCreateSIMstNew.PAYMODE = ddlPayMode.SelectedValue;
                                objCreateSIMstNew.PENDINGAMT = Convert.ToString(totalbaseamount + totaltaxamount + EWTOTAL);
                                objCreateSIMstNew.PMTTERMS = "ADV";
                                objCreateSIMstNew.PREPAIDAMT = txtPartialAmount.Text;
                                //objCreateSIMstNew.REFSINO="";
                                objCreateSIMstNew.REMAINAMT = Convert.ToString((totalbaseamount + totaltaxamount + EWTOTAL) - Convert.ToDecimal(txtPartialAmount.Text));
                                objCreateSIMstNew.REMARK = "SI CREATED FROM POS";
                                objCreateSIMstNew.ROUNDOFF = Convert.ToString(Math.Round((totalbaseamount + totaltaxamount + EWTOTAL)));
                                objCreateSIMstNew.SEGMENT = segment;
                                objCreateSIMstNew.SHIPTOCODE = "0000010003";
                                //objCreateSIMstNew.SHIPTOPARTYNM = "";
                                objCreateSIMstNew.SIDT = DateTime.Now.ToShortDateString();
                                //objCreateSIMstNew.SIGST = "";
                                objCreateSIMstNew.SINO = "";
                                objCreateSIMstNew.SITYPE = "SIT";



                                objCreateSIMstNew.STATUS = 57;
                                objCreateSIMstNew.TAXDISC = "0";
                                objCreateSIMstNew.TAXOTH = "0";
                                objCreateSIMstNew.TOTAMT = Convert.ToString(totalbaseamount + totaltaxamount + EWTOTAL);
                                objCreateSIMstNew.TRANCHG = "0";




                                //string PRURL = "http://14.98.132.190:1503/api/CreateSONew";
                                //string PRURL = "http://3.6.38.46/api/CreateSONew";
                                //string PRURL = "http://localhost:44397/api/CreateSONew";
                                //string PRURL = "http://localhost:44397/api/CreateSONewV1";
                                string PRURL = "http://14.98.132.190:1503/api/CreateSONewV1";
                                var client = new RestClient(PRURL);
                                client.Timeout = -1;
                                var request = new RestRequest(Method.POST);
                                request.AddHeader("APP_NAME", "SALESORDER");
                                request.AddHeader("KEY_NAME", "SALES_KEY");
                                request.AddHeader("SALES_KEY", "SALESORDER*&^%$");
                                request.AddHeader("Content-Type", "application/json");
                                var jsonInput = JsonConvert.SerializeObject(objSOCreateClass);
                                request.AddParameter("application/json", jsonInput, ParameterType.RequestBody);
                                IRestResponse response = client.Execute(request);
                                if (response.StatusCode == HttpStatusCode.OK)
                                {

                                    SORESPONSENEW objSORESPONSE = new SORESPONSENEW();
                                    string jsonconn = response.Content;
                                    objSORESPONSE = JsonConvert.DeserializeObject<SORESPONSENEW>(jsonconn);

                                    #region Commented Code...

                                    //SqlCommand cmdc = new SqlCommand("SP_SO_MASTER", ConnSherpa);
                                    //cmdc.Parameters.AddWithValue("@CREATEBY", Convert.ToInt32(Session["USERID"]));
                                    //cmdc.Parameters.AddWithValue("@SONO", objSORESPONSE.SONO);
                                    //cmdc.Parameters.AddWithValue("@ACTION", "UPDATESOCREATEBY");
                                    //cmdc.CommandType = CommandType.StoredProcedure;
                                    //cmdc.Connection.Open();
                                    //cmdc.ExecuteNonQuery();
                                    //cmdc.Connection.Close();

                                    //for (int i = 0; i < gvList.Rows.Count; i++)
                                    //{

                                    //    Label lblJOBID = (Label)gvList.Rows[i].FindControl("lblJobid");

                                    //    SqlCommand cmdA = new SqlCommand("SP_SO_MASTER", ConnSherpa);
                                    //    cmdA.Parameters.AddWithValue("@CUSTADD1", txtAddress.Text.ToUpper());
                                    //    cmdA.Parameters.AddWithValue("@CUSTADD2", txtAddress2.Text.ToUpper());
                                    //    cmdA.Parameters.AddWithValue("@CUSTADD3", txtAddress3.Text.ToUpper());
                                    //    cmdA.Parameters.AddWithValue("@CITY", ddlCity.SelectedItem.Text);
                                    //    cmdA.Parameters.AddWithValue("@STATEID", Convert.ToInt32(ddlState.SelectedValue));
                                    //    cmdA.Parameters.AddWithValue("@PINCODE", txtPincode.Text);
                                    //    cmdA.Parameters.AddWithValue("@CUSTMOBILENO", txtMobileNo.Text);
                                    //    cmdA.Parameters.AddWithValue("@CUSTEMAILID", "");
                                    //    cmdA.Parameters.AddWithValue("@JOBID", objMainClass.strConvertZeroPadding(lblJOBID.Text));
                                    //    cmdA.Parameters.AddWithValue("@CUSTNAME", txtName.Text.ToUpper());
                                    //    cmdA.Parameters.AddWithValue("@UPDATEBY", Convert.ToInt32(Session["USERID"]));
                                    //    cmdA.Parameters.AddWithValue("@ACTION", "UPDATEJOB");
                                    //    cmdA.CommandType = CommandType.StoredProcedure;
                                    //    cmdA.Connection.Open();
                                    //    cmdA.ExecuteNonQuery();
                                    //    cmdA.Connection.Close();

                                    //    SqlCommand cmdB = new SqlCommand("SP_SO_MASTER", ConnSherpa);
                                    //    cmdB.Parameters.AddWithValue("@REFNO", objSOCreateClass.REFNO);
                                    //    cmdB.Parameters.AddWithValue("@JOBID", objMainClass.strConvertZeroPadding(lblJOBID.Text));
                                    //    cmdB.Parameters.AddWithValue("@ACTION", "UPDATEJWREFCROMA");
                                    //    cmdB.CommandType = CommandType.StoredProcedure;
                                    //    cmdB.Connection.Open();
                                    //    cmdB.ExecuteNonQuery();
                                    //    cmdB.Connection.Close();

                                    //} 
                                    #endregion

                                    if (objSORESPONSE.SONO != "" && objSORESPONSE.SONO != string.Empty && objSORESPONSE.SONO != null)
                                    {
                                        string NEWSONO = objSORESPONSE.SONO;

                                        for (int i = 0; i < objCreateDCDTL.Count; i++)
                                        {
                                            objCreateDCDTL[i].SONO = NEWSONO;
                                        }
                                        objCreateDCMST.CreateDCDTLList = objCreateDCDTL;


                                        string PRURLDC = "http://14.98.132.190:1503/api/CreateDCNew";
                                        //string PRURLDC = "http://3.6.38.46/api/CreateDCNew";
                                        //string PRURLDC = "http://localhost:44397/api/CreateDCNew";
                                        var clientDC = new RestClient(PRURLDC);
                                        clientDC.Timeout = -1;
                                        var requestDC = new RestRequest(Method.POST);
                                        requestDC.AddHeader("APP_NAME", "SALESORDER");
                                        requestDC.AddHeader("KEY_NAME", "SALES_KEY");
                                        requestDC.AddHeader("SALES_KEY", "SALESORDER*&^%$");
                                        requestDC.AddHeader("Content-Type", "application/json");
                                        var jsonInputDC = JsonConvert.SerializeObject(objCreateDCMST);
                                        requestDC.AddParameter("application/json", jsonInputDC, ParameterType.RequestBody);
                                        IRestResponse responseDC = clientDC.Execute(requestDC);


                                        if (responseDC.StatusCode == HttpStatusCode.OK)
                                        {
                                            SORESPONSEDC objSORESPONSEDC = new SORESPONSEDC();
                                            string jsonconnDC = responseDC.Content;
                                            objSORESPONSEDC = JsonConvert.DeserializeObject<SORESPONSEDC>(jsonconnDC);

                                            string NEWDCNO = objSORESPONSEDC.DCNO;


                                            objCreateSIMstNew.SONO = NEWSONO;

                                            for (int i = 0; i < objCreateSIDtlNew.Count; i++)
                                            {
                                                objCreateSIDtlNew[i].DCNO = NEWDCNO;
                                            }

                                            objCreateSIMstNew.CreateSIDtlNewList = objCreateSIDtlNew;


                                            string PRURLSI = "http://14.98.132.190:1503/api/CreateSINew";
                                            //string PRURLSI = "http://3.6.38.46/api/CreateSINew";
                                            //string PRURLSI = "http://localhost:44397/api/CreateSINew";
                                            var clientSI = new RestClient(PRURLSI);
                                            clientSI.Timeout = -1;
                                            var requestSI = new RestRequest(Method.POST);
                                            requestSI.AddHeader("APP_NAME", "SALESORDER");
                                            requestSI.AddHeader("KEY_NAME", "SALES_KEY");
                                            requestSI.AddHeader("SALES_KEY", "SALESORDER*&^%$");
                                            requestSI.AddHeader("Content-Type", "application/json");
                                            var jsonInputSI = JsonConvert.SerializeObject(objCreateSIMstNew);
                                            requestSI.AddParameter("application/json", jsonInputSI, ParameterType.RequestBody);
                                            IRestResponse responseSI = clientSI.Execute(requestSI);

                                            if (responseSI.StatusCode == HttpStatusCode.OK)
                                            {

                                                SORESPONSESI objSORESPONSESI = new SORESPONSESI();
                                                string jsonconnSI = responseSI.Content;
                                                objSORESPONSESI = JsonConvert.DeserializeObject<SORESPONSESI>(jsonconnSI);

                                                string NEWSINO = objSORESPONSESI.SINO;

                                                string msg = "New SO, DC & SI Created. SO NO. is : " + objSORESPONSE.SONO + ". DC No. : " + objSORESPONSEDC.DCNO + ". SI No. : " + NEWSINO;
                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" " + msg + "\");$('.close').click(function(){window.location.href ='frmPOS.aspx' });", true);

                                                //string path = "rptSalesInvoiceDownload.aspx?QRPTSINO=" + NEWSINO;
                                                //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "');", true);

                                            }
                                            else
                                            {
                                                string msg = "New SO & DC Created. But SI not created. SO NO. is : " + objSORESPONSE.SONO + " . DC No. : " + objSORESPONSEDC.DCNO;
                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + msg + "\");$('.close').click(function(){window.location.href ='frmPOS.aspx' });", true);
                                            }





                                        }
                                        else
                                        {
                                            string msg = "New SO Created. But DC/SI not created. SO NO. is : " + objSORESPONSE.SONO;
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + msg + "\");$('.close').click(function(){window.location.href ='frmPOS.aspx' });", true);
                                        }




                                    }
                                    else
                                    {
                                        string msg = "Error.!! Invoice not created.";
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + msg + "\");", true);
                                    }



                                }
                                else
                                {
                                    SORESPONSENEW objSORESPONSE = new SORESPONSENEW();
                                    string jsonconn = response.Content;
                                    objSORESPONSE = JsonConvert.DeserializeObject<SORESPONSENEW>(jsonconn);


                                    string msg = "Invoice not created. Error : " + objSORESPONSE.MESSAGE;
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\" " + msg + "\");", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You can not select product from multiple location in Invoice..!');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please add items to create invoce..!');", true);
                        }
                    }
                    btnSave.Enabled = true;
                    btnSave.Visible = true;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                btnSave.Enabled = true;
                btnSave.Visible = true;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void txtDeliveryDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((TextBox)sender).NamingContainer;
                    Label lblSRNO = (Label)grdrow.FindControl("lblSRNO");
                    TextBox txtDeliveryDate = (TextBox)grdrow.FindControl("txtDeliveryDate");

                    CheckBox chkDemoInstallation = (CheckBox)grdrow.FindControl("chkDemoInstallation");
                    Label DEMINST = (Label)grdrow.FindControl("DEMINST");

                    int demint = 0;
                    if (chkDemoInstallation.Checked == true)
                    {
                        demint = 1;
                    }


                    DataTable dt = (DataTable)ViewState["ItemData"];
                    DataRow dr = dt.Select("SRNO = '" + lblSRNO.Text + "'")[0];
                    dr[21] = txtDeliveryDate.Text;
                    dr[23] = demint;

                    gvList.DataSource = (DataTable)ViewState["ItemData"];
                    gvList.DataBind();
                    CalcAmt();

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

        protected void chkDemoInstallation_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((CheckBox)sender).NamingContainer;
                    Label lblSRNO = (Label)grdrow.FindControl("lblSRNO");
                    CheckBox chkDemoInstallation = (CheckBox)grdrow.FindControl("chkDemoInstallation");
                    Label DEMINST = (Label)grdrow.FindControl("DEMINST");

                    int demint = 0;
                    if (chkDemoInstallation.Checked == true)
                    {
                        demint = 1;
                    }

                    DataTable dt = (DataTable)ViewState["ItemData"];
                    DataRow dr = dt.Select("SRNO = '" + lblSRNO.Text + "'")[0];
                    dr[23] = demint;

                    gvList.DataSource = (DataTable)ViewState["ItemData"];
                    gvList.DataBind();
                    CalcAmt();

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

        protected void lnkTempDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    Label lblJOBID = ((Label)grdrow.FindControl("lblJOBID"));

                    DataTable dt = (DataTable)ViewState["ItemData"];
                    dt.Select("JOBID='" + lblJOBID.Text + "'").ToList().ForEach(x => x.Delete());
                    dt.AcceptChanges();
                    ViewState["ItemData"] = dt;
                    gvList.DataSource = dt;
                    gvList.DataBind();
                    CalcAmt();

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

        protected void ddlReference_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    if (ddlReference.SelectedValue == "22492" || ddlReference.SelectedValue == "154")
                    {
                        divrefname.Visible = true;
                        rfvRefName.Enabled = true;
                    }
                    else
                    {
                        divrefname.Visible = false;
                        rfvRefName.Enabled = false;
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

        protected void txtCouponCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    if (txtCouponCode.Text != null && txtCouponCode.Text != "" && txtCouponCode.Text != string.Empty)
                    {
                        if (Convert.ToDecimal(lblTotalAmt.Text) > 0)
                        {
                            DataTable dtCoupon = new DataTable();
                            dtCoupon = objMainClass.GETCOUPONDATA(objMainClass.intCmpId, txtCouponCode.Text, 1, "CHECKCOUPONCODE");
                            if (dtCoupon.Rows.Count > 0)
                            {
                                decimal Discount = Convert.ToDecimal(dtCoupon.Rows[0]["DISCOUNTPER"]);
                                decimal MinimumAmt = Convert.ToDecimal(dtCoupon.Rows[0]["MINIMUNAMT"]);
                                DateTime ExpiryDate = Convert.ToDateTime(dtCoupon.Rows[0]["EXPIRYDATE"]);
                                if (ExpiryDate > DateTime.Now)
                                {
                                    if (Convert.ToDecimal(lblTotalAmt.Text) >= MinimumAmt)
                                    {
                                        DataTable dt = (DataTable)ViewState["ItemData"];
                                        for (int i = 0; i < gvList.Rows.Count; i++)
                                        {
                                            GridViewRow grdrow = gvList.Rows[i];
                                            Label lblSRNO = (Label)grdrow.FindControl("lblSRNO");
                                            Label lblPrice = (Label)grdrow.FindControl("lblPrice");
                                            TextBox txtDiscount = (TextBox)grdrow.FindControl("txtDiscount");
                                            Label lblNetAmt = (Label)grdrow.FindControl("lblNetAmt");
                                            decimal netamt = Convert.ToDecimal(Convert.ToDecimal(lblPrice.Text) - Convert.ToDecimal(txtDiscount.Text));
                                            lblNetAmt.Text = Convert.ToString(netamt);
                                        }
                                    }
                                    else
                                    {
                                        string msg = "Coupon valid for purchase of " + MinimumAmt + " or more..!";
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + msg + "\");", true);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid Coupon Code. Coupon Code Expired.!');", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid Coupon Code.!');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Coupon cannot apply. Please add item.!');", true);
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

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        Label lblItemCode = (Label)e.Row.FindControl("lblItemCode");
                        Label lblNetAmt = (Label)e.Row.FindControl("lblNetAmt");
                        RadioButtonList rblEWCharges = (RadioButtonList)e.Row.FindControl("rblEWCharges");

                        DataTable dtItemDet = new DataTable();
                        dtItemDet = objMainClass.GetItemDetailsByItemCode(objMainClass.intCmpId, lblItemCode.Text, 1, "ITEMMASTERSEARCH");

                        int itemsubgroup = Convert.ToInt32(dtItemDet.Rows[0]["ITEMSUBGRP"]);

                        DataTable dt = new DataTable();
                        dt = objMainClass.GetEWData(objMainClass.intCmpId, itemsubgroup, Convert.ToDecimal(lblNetAmt.Text), 0, 0, 0, "GETEWPRICE");

                        rblEWCharges.DataSource = dt;
                        rblEWCharges.DataTextField = "DESCRIPTION";
                        rblEWCharges.DataValueField = "ID";
                        rblEWCharges.DataBind();
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

        protected void rblEWCharges_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    //RadioButtonList rbl = (RadioButtonList)sender;
                    //GridViewRow row = (GridViewRow)rbl.NamingContainer;
                    //string selectedValue = rbl.SelectedValue;
                    //string selectedText = rbl.SelectedItem.Text;


                    //DataTable dt = new DataTable();
                    //dt = objMainClass.GetEWData(objMainClass.intCmpId, 0, 0, 0, 0, Convert.ToInt32(selectedValue), "GETEWPRICEWITHID");

                    CalcAmtNew();

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

        public void CalcAmtNew()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    decimal finalamt = 0;
                    for (int i = 0; i < gvList.Rows.Count; i++)
                    {
                        RadioButtonList rbl = ((RadioButtonList)gvList.Rows[i].FindControl("rblEWCharges"));

                        decimal ewcharges = 0;
                        //if (rbl.DataSource != null)
                        //{
                        string selectedValue = rbl.SelectedValue;
                        //string selectedText = rbl.SelectedItem.Text;
                        if (selectedValue != "")
                        {
                            DataTable dt = new DataTable();
                            dt = objMainClass.GetEWData(objMainClass.intCmpId, 0, 0, 0, 0, Convert.ToInt32(selectedValue), "GETEWPRICEWITHID");

                            if (dt.Rows.Count > 0)
                            {
                                ewcharges = Convert.ToDecimal(dt.Rows[0]["PRICE"]);
                            }
                        }
                        //}

                        string lblNetAmt = ((Label)gvList.Rows[i].FindControl("lblNetAmt")).Text;
                        finalamt = ewcharges + finalamt + Convert.ToDecimal(lblNetAmt);

                        CheckBox chkDemoInstallation = (CheckBox)gvList.Rows[i].FindControl("chkDemoInstallation");
                        Label lblDEMINST = (Label)gvList.Rows[i].FindControl("lblDEMINST");

                        if (lblDEMINST.Text == "1")
                        {
                            chkDemoInstallation.Checked = true;
                        }
                        else
                        {
                            chkDemoInstallation.Checked = false;
                        }



                    }

                    lblTotalAmt.Text = Convert.ToString(finalamt);

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