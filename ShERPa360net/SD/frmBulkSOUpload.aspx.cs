using Newtonsoft.Json;
using RestSharp;
using ShERPa360net.Class;
using ShERPa360net.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.SD
{
    public partial class frmBulkSOUpload : System.Web.UI.Page
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

                //dtColumn = new DataColumn();
                //dtColumn.ColumnName = "DISCOUNT";
                //dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "NETAMT";
                dtItem.Columns.Add(dtColumn);

                //dtColumn = new DataColumn();
                //dtColumn.ColumnName = "DISCBYWHOM";
                //dtItem.Columns.Add(dtColumn);

                //dtColumn = new DataColumn();
                //dtColumn.ColumnName = "DEMOINSTALLATION";
                //dtItem.Columns.Add(dtColumn);

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

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    OleDbConnection MyConnection;
                    string extension = Path.GetExtension(fuImage.FileName);
                    string folderpath = "~/excel/";
                    string filePath = Path.Combine(Server.MapPath(folderpath), Guid.NewGuid().ToString("N") + extension);
                    fuImage.SaveAs(filePath);
                    DataTable dt = new DataTable();
                    if (extension == ".csv")
                    {
                        var items = (from line in System.IO.File.ReadAllLines(filePath)
                                     select Array.ConvertAll(line.Split(','), v => v.ToString().TrimStart("\" ".ToCharArray()).TrimEnd("\" ".ToCharArray()))).ToArray();
                        string[] strarr1 = items[0];
                        for (int x = 0; x <= items[0].Count() - 1; x++)
                            dt.Columns.Add(strarr1[x]);
                        foreach (var a in items)
                        {
                            DataRow dr = dt.NewRow();
                            dr.ItemArray = a;
                            dt.Rows.Add(dr);
                        }
                        if (dt.Rows.Count > 0)
                        {
                            dt.Rows.RemoveAt(0);
                        }
                    }
                    else
                    {
                        MyConnection = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 12.0;");
                        //MyConnection = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + flUpload.FileName + ";Extended Properties=Excel 8.0;");
                        MyConnection.Open();
                        DataTable dtExcelSchema = MyConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null/* TODO Change to default(_) if this is not a reference type */);
                        string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                        using (OleDbCommand cmd = new OleDbCommand())
                        {
                            using (OleDbDataAdapter oda = new OleDbDataAdapter())
                            {
                                cmd.CommandText = (Convert.ToString("SELECT * From [Sheet1$]"));
                                cmd.Connection = MyConnection;
                                oda.SelectCommand = cmd;
                                oda.Fill(dt);
                                MyConnection.Close();
                            }
                        }
                    }
                    File.Delete(filePath);

                    for (int m = 0; m < dt.Rows.Count; m++)
                    {
                        string JOBID = Convert.ToString(dt.Rows[m]["JOB ID"]);
                        string PRICE = Convert.ToString(dt.Rows[m]["Price"]);
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
                            string Message = JOBID + " - this job id entered multiple times. Please enter only once.!";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                            return;
                        }
                        else
                        {
                            DataTable dtJobid = new DataTable();
                            dtJobid = objMainClass.GetJSDetails(JOBID);

                            if (dtJobid.Rows.Count > 0)
                            {
                                string plantcd = Convert.ToString(dtJobid.Rows[0]["PLANTCD"]);

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
                                    if (Convert.ToString(dtJobid.Rows[0]["JOBSTATUS"]) == "23" || Convert.ToString(dtJobid.Rows[0]["JOBSTATUS"]) == "3")
                                    {
                                        string Message = JOBID + " - this job id is already closed/cancelled.!";
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                                        return;
                                    }
                                    else
                                    {
                                        if ((Convert.ToString(dtJobid.Rows[0]["JOBSTATUS"]) == "53" && Convert.ToString(dtJobid.Rows[0]["STAGEID"]) == "46") || (Convert.ToString(dtJobid.Rows[0]["JOBSTATUS"]) == "21" && Convert.ToString(dtJobid.Rows[0]["STAGEID"]) == "24"))
                                        {
                                            string JOBSHEETID = objMainClass.strConvertZeroPadding(Convert.ToString(dtJobid.Rows[0]["JOBID"]));
                                            DataTable dtjobrate = new DataTable();
                                            dtjobrate = objMainClass.CheckRateJobDuplicate(objMainClass.intCmpId, JOBSHEETID, "GETRATEFROMPO");

                                            if (dtjobrate.Rows.Count > 0)
                                            {
                                                decimal jobrate = Convert.ToDecimal(PRICE);
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
                                                    ITEMCODE = Convert.ToString(dtJobid.Rows[0]["ITEMCODE"]);
                                                }
                                                string IMEI = Convert.ToString(dtJobid.Rows[0]["IMEINO"]);
                                                string PLANT = Convert.ToString(dtJobid.Rows[0]["PLANTCD"]);
                                                string LOCATION = Convert.ToString(dtJobid.Rows[0]["LOCCD"]);
                                                string SEGMENT = Convert.ToString(dtJobid.Rows[0]["SEGCODE"]);
                                                string DISTCHNL = Convert.ToString(dtJobid.Rows[0]["DISTCHNL"]);
                                                int deminst = 0;

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
                                                                        dtitemgrid.Rows.Add(id, dtItemDet.Rows[0]["ITEMID"], dtItemDet.Rows[0]["ITEMCODE"], dtItemDet.Rows[0]["ITEMDESC"], dtItemDet.Rows[0]["ITEMGRP"], JOBID, IMEI, 1, dtItemDet.Rows[0]["UOM"], PLANT, LOCATION, jobrate, jobrate, taxrate, condid, conditype, SEGMENT, DISTCHNL, DateTime.Now.AddDays(2).ToShortDateString(), Convert.ToString(dtJobid.Rows[0]["STAGEID"]), deminst);
                                                                    }
                                                                    else
                                                                    {
                                                                        dtitemgrid.Rows.Add(1, dtItemDet.Rows[0]["ITEMID"], dtItemDet.Rows[0]["ITEMCODE"], dtItemDet.Rows[0]["ITEMDESC"], dtItemDet.Rows[0]["ITEMGRP"], JOBID, IMEI, 1, dtItemDet.Rows[0]["UOM"], PLANT, LOCATION, jobrate, jobrate, taxrate, condid, conditype, SEGMENT, DISTCHNL, DateTime.Now.AddDays(2).ToShortDateString(), Convert.ToString(dtJobid.Rows[0]["STAGEID"]), deminst);
                                                                    }

                                                                    ViewState["ItemData"] = dtitemgrid;
                                                                    gvList.DataSource = (DataTable)ViewState["ItemData"];
                                                                    gvList.DataBind();
                                                                }
                                                                else
                                                                {
                                                                    string Message = JOBID + " - Stock not available for this job id..!";
                                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                                                                    return;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                string Message = JOBID + " - Item details not found for this job id..!";
                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                                                                return;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            string Message = JOBID + " - SO or Purchase Return is already created for this job id..!";
                                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                                                            return;
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
                                                                        dtitemgrid.Rows.Add(id, dtItemDet.Rows[0]["ITEMID"], dtItemDet.Rows[0]["ITEMCODE"], dtItemDet.Rows[0]["ITEMDESC"], dtItemDet.Rows[0]["ITEMGRP"], JOBID, IMEI, 1, dtItemDet.Rows[0]["UOM"], PLANT, LOCATION, jobrate, jobrate, taxrate, condid, conditype, SEGMENT, DISTCHNL, DateTime.Now.AddDays(2).ToShortDateString(), Convert.ToString(dtJobid.Rows[0]["STAGEID"]), deminst);
                                                                    }
                                                                    else
                                                                    {
                                                                        dtitemgrid.Rows.Add(1, dtItemDet.Rows[0]["ITEMID"], dtItemDet.Rows[0]["ITEMCODE"], dtItemDet.Rows[0]["ITEMDESC"], dtItemDet.Rows[0]["ITEMGRP"], JOBID, IMEI, 1, dtItemDet.Rows[0]["UOM"], PLANT, LOCATION, jobrate, jobrate, taxrate, condid, conditype, SEGMENT, DISTCHNL, DateTime.Now.AddDays(2).ToShortDateString(), Convert.ToString(dtJobid.Rows[0]["STAGEID"]), deminst);
                                                                    }

                                                                    ViewState["ItemData"] = dtitemgrid;
                                                                    gvList.DataSource = (DataTable)ViewState["ItemData"];
                                                                    gvList.DataBind();
                                                                }
                                                                else
                                                                {
                                                                    string Message = JOBID + " - Stock not available for this job id..!";
                                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                                                                    return;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                string Message = JOBID + " - Item details not found for this job id..!";
                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                                                                return;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            string Message = JOBID + " - SO or Purchase Return is already created for this job id..!";
                                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                                                            return;
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
                                                                    dtitemgrid.Rows.Add(id, dtItemDet.Rows[0]["ITEMID"], dtItemDet.Rows[0]["ITEMCODE"], dtItemDet.Rows[0]["ITEMDESC"], dtItemDet.Rows[0]["ITEMGRP"], JOBID, IMEI, 1, dtItemDet.Rows[0]["UOM"], PLANT, LOCATION, jobrate, jobrate, taxrate, condid, conditype, SEGMENT, DISTCHNL, DateTime.Now.AddDays(2).ToShortDateString(), Convert.ToString(dtJobid.Rows[0]["STAGEID"]), deminst);
                                                                }
                                                                else
                                                                {
                                                                    dtitemgrid.Rows.Add(1, dtItemDet.Rows[0]["ITEMID"], dtItemDet.Rows[0]["ITEMCODE"], dtItemDet.Rows[0]["ITEMDESC"], dtItemDet.Rows[0]["ITEMGRP"], JOBID, IMEI, 1, dtItemDet.Rows[0]["UOM"], PLANT, LOCATION, jobrate, jobrate, taxrate, condid, conditype, SEGMENT, DISTCHNL, DateTime.Now.AddDays(2).ToShortDateString(), Convert.ToString(dtJobid.Rows[0]["STAGEID"]), deminst);
                                                                }

                                                                ViewState["ItemData"] = dtitemgrid;
                                                                gvList.DataSource = (DataTable)ViewState["ItemData"];
                                                                gvList.DataBind();
                                                            }
                                                            else
                                                            {
                                                                string Message = JOBID + " - Stock not available for this job id..!";
                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            string Message = JOBID + " - Item details not found for this job id..!";
                                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                                                            return;
                                                        }
                                                      }
                                                    else
                                                    {
                                                        string Message = JOBID + " - SO or Purchase Return is already created for this job id..!";
                                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                                                        return;
                                                    }
                                                }

                                            }
                                            else
                                            {
                                                string Message = JOBID + " - Purchase details not found for this job id..!";
                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                                                return;
                                            }

                                        }
                                        else
                                        {
                                            string Message = JOBID + " - Job id must be in Phy. Doc. Var. or Forward Waybill Generated Stage..!!";
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                                            return;
                                        }
                                    }
                                }
                                else
                                {
                                    string Message = JOBID + " - this job id created under - " + plantcd + ". You do not have plant rights.!";
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                                    return;
                                }


                            }
                            else
                            {
                                string Message = JOBID + " - Invalid job id.!";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                                return;
                            }
                        }
                    }

                    if (gvList.Rows.Count > 0)
                    {
                        FSCust.Visible = true;
                        FSItem.Visible = true;
                        FSTotal.Visible = true;
                        CalcAmt();
                    }
                    else
                    {
                        FSCust.Visible = false;
                        FSItem.Visible = false;
                        FSTotal.Visible = false;
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
                        string lblNetAmt = ((Label)gvList.Rows[i].FindControl("lblNetAmt")).Text;
                        finalamt = finalamt + Convert.ToDecimal(lblNetAmt);

                        //CheckBox chkDemoInstallation = (CheckBox)gvList.Rows[i].FindControl("chkDemoInstallation");
                        //Label lblDEMINST = (Label)gvList.Rows[i].FindControl("lblDEMINST");

                        //if (lblDEMINST.Text == "1")
                        //{
                        //    chkDemoInstallation.Checked = true;
                        //}
                        //else
                        //{
                        //    chkDemoInstallation.Checked = false;
                        //}

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

        protected void txtMobileNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (objMainClass.strConvertZeroPadding(txtCustomer.Text) == "0000010003")
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

        protected void txtCustomer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetCustData(objMainClass.intCmpId, objMainClass.strConvertZeroPadding(txtCustomer.Text), "", "", "", "CUSTADDDETAIL");
                    if (dt.Rows.Count > 0)
                    {
                        txtCustomerName.Text = Convert.ToString(dt.Rows[0]["NAME"]);
                        txtName.Text = Convert.ToString(dt.Rows[0]["NAME"]);
                        txtAddress.Text = Convert.ToString(dt.Rows[0]["ADDR1"]);
                        txtAddress2.Text = Convert.ToString(dt.Rows[0]["ADDR2"]);
                        txtAddress3.Text = Convert.ToString(dt.Rows[0]["ADDR3"]);
                        txtPincode.Text = Convert.ToString(dt.Rows[0]["POSTALCODE"]);
                        ddlState.SelectedValue = Convert.ToString(dt.Rows[0]["STCD"]);
                        txtMobileNo.Text = Convert.ToString(dt.Rows[0]["MOBILENO"]);
                        ddlState_SelectedIndexChanged(1, e);
                        //ddlCity.SelectedItem.Text = Convert.ToString(dt.Rows[0]["City"]);
                        txtGSTNO.Text = Convert.ToString(dt.Rows[0]["GSTNO"]);
                    }
                    else
                    {
                        txtCustomer.Focus();
                        txtCustomer.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid Customer Code.');", true);
                        txtCustomer.Text = string.Empty;
                        txtCustomer.Focus();
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

        protected void txtShipment_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetCustData(objMainClass.intCmpId, objMainClass.strConvertZeroPadding(txtShipment.Text), "", "", "", "CUSTDETAIL");
                    if (dt.Rows.Count > 0)
                    {
                        txtShipmentName.Text = Convert.ToString(dt.Rows[0]["NAME1"]);
                    }
                    else
                    {
                        txtShipment.Focus();
                        txtShipment.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid Shipment Code.');", true);
                        txtShipment.Text = string.Empty;
                        txtShipment.Focus();
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
                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                decimal totdiscount = 0;
                                decimal totalitemamount = 0;
                                decimal totaltaxamount = 0;
                                decimal totalbaseamount = 0;
                                int srno = 0;
                                string segment = "1043";
                                string distchnl = "50";
                                SOCreateClassNew objSOCreateClass = new SOCreateClassNew();
                                List<LSTITEMDETAILSNEW> ItemDetails = new List<LSTITEMDETAILSNEW>();
                                List<LSTTAXDETAILSNEW> TaxDetails = new List<LSTTAXDETAILSNEW>();

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
                                    //TextBox txtDiscount = (TextBox)gvList.Rows[i].FindControl("txtDiscount");
                                    Label lblNetAmt = (Label)gvList.Rows[i].FindControl("lblNetAmt");
                                    //TextBox txtByWhome = (TextBox)gvList.Rows[i].FindControl("txtByWhome");
                                    //CheckBox chkDemoInstallation = (CheckBox)gvList.Rows[i].FindControl("chkDemoInstallation");
                                    Label lblRATE = (Label)gvList.Rows[i].FindControl("lblRATE");
                                    Label lblCONDID = (Label)gvList.Rows[i].FindControl("lblCONDID");
                                    Label lblCONDTYPE = (Label)gvList.Rows[i].FindControl("lblCONDTYPE");
                                    Label lblSEGMENT = (Label)gvList.Rows[i].FindControl("lblSEGMENT");
                                    Label lblDISTCHNL = (Label)gvList.Rows[i].FindControl("lblDISTCHNL");
                                    TextBox txtDeliveryDate = (TextBox)gvList.Rows[i].FindControl("txtDeliveryDate");
                                    Label lblJOBSTATUS = (Label)gvList.Rows[i].FindControl("lblJOBSTATUS");

                                    segment = lblSEGMENT.Text;
                                    distchnl = lblDISTCHNL.Text;
                                    string costcenter = "1081";
                                    DataTable dtCostCenter = new DataTable();
                                    dtCostCenter = objMainClass.GetCostCenter(lblPLANTCODE.Text, lblLOCCD.Text);
                                    if (dtCostCenter.Rows.Count > 0)
                                    {
                                        costcenter = Convert.ToString(dtCostCenter.Rows[0]["CSTCENTCD"]);
                                    }

                                    decimal discount = Convert.ToDecimal(0);

                                    decimal baseaamt = Convert.ToDecimal((Convert.ToDecimal(lblNetAmt.Text) * 100) / (Convert.ToDecimal(lblRATE.Text) + 100));
                                    decimal taxamt = (baseaamt * Convert.ToDecimal(lblRATE.Text) / 100);
                                    decimal brate = baseaamt + discount;
                                    decimal camount = baseaamt + discount;

                                    int demo = 0;
                                    int installation = 0;
                                    //if (chkDemoInstallation.Checked == true)
                                    //{
                                    //    demo = 1;
                                    //    installation = 1;
                                    //}


                                    totdiscount = totdiscount + discount;
                                    totalitemamount = totalitemamount + brate;
                                    totaltaxamount = totaltaxamount + taxamt;
                                    totalbaseamount = totalbaseamount + baseaamt;


                                    ItemDetails.Add(new LSTITEMDETAILSNEW
                                    {
                                        CAMOUNT = Convert.ToString(camount),
                                        CHANGEREASON = "",
                                        COSTCENTER = costcenter,
                                        CUSTPARTDESC = "",
                                        CUSTPARTNO = "",
                                        DELIDT = txtDeliveryDate.Text,
                                        DISCAMT = Convert.ToString(discount),
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
                                        APPROVEBY = ""

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

                                }


                                objSOCreateClass.BILLTOCODE = objMainClass.strConvertZeroPadding(txtCustomer.Text);
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
                                objSOCreateClass.NETSOAMT = Convert.ToString(totalbaseamount + totaltaxamount);
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
                                objSOCreateClass.REMAINAMT = Convert.ToString((totalbaseamount + totaltaxamount) - Convert.ToDecimal(txtPartialAmount.Text));
                                objSOCreateClass.REMARKS = "SO CREATED FROM BULK SO";
                                objSOCreateClass.SALESFORM = 11411;
                                objSOCreateClass.SCHEMEID = 11913;
                                objSOCreateClass.SEGMENT = segment;
                                objSOCreateClass.SHIPTOCODE = objMainClass.strConvertZeroPadding(txtShipment.Text);
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

                                string PRURL = "http://14.98.132.190:1503/api/CreateSONew";
                                //string PRURL = "http://3.6.38.46/api/CreateSONew";
                                //string PRURL = "http://localhost:44397/api/CreateSONew";
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
                                    if (objSORESPONSE.SONO != "" && objSORESPONSE.SONO != string.Empty && objSORESPONSE.SONO != null)
                                    {
                                        string NEWSONO = objSORESPONSE.SONO;

                                        for (int i = 0; i < gvList.Rows.Count; i++)
                                        {

                                            Label lblJOBID = (Label)gvList.Rows[i].FindControl("lblJobid");
                                            SqlCommand cmdB = new SqlCommand("SP_SO_MASTER", ConnSherpa);
                                            cmdB.Parameters.AddWithValue("@REFNO", objSOCreateClass.REFNO);
                                            cmdB.Parameters.AddWithValue("@JOBID", objMainClass.strConvertZeroPadding(lblJOBID.Text));
                                            cmdB.Parameters.AddWithValue("@ACTION", "UPDATEJWREFCROMA");
                                            cmdB.CommandType = CommandType.StoredProcedure;
                                            cmdB.Connection.Open();
                                            cmdB.ExecuteNonQuery();
                                            cmdB.Connection.Close();
                                        }

                                        string msg = "New Created. SO NO. is : " + objSORESPONSE.SONO;
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" " + msg + "\");$('.close').click(function(){window.location.href ='frmBulkSOUpload.aspx' });", true);
                                    }
                                    else
                                    {
                                        string msg = "Error.!! SO not created.";
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

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    //string filePath = "~/SD/Bulk SO Creation.xlsx";
                    string filePath = Server.MapPath("~/SD/Bulk SO Creation.xlsx");
                    FileInfo file = new FileInfo(filePath);
                    if (file.Exists)
                    {
                        Response.Clear();
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                        Response.AddHeader("Content-Length", file.Length.ToString());
                        Response.ContentType = "text/plain";
                        Response.Flush();
                        Response.TransmitFile(file.FullName);
                        Response.End();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('File not found for Download.!');", true);
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
    }
}