using Newtonsoft.Json;
using ShERPa360net.Class;
using ShERPa360net.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Windows.Forms;

namespace ShERPa360net.SD
{

    public partial class BulkWebsiteSoCreation : System.Web.UI.Page
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
                            btnUpload.Enabled = false;
                            btnSave.Enabled = false;
                        }

                        objBindDDL.FillPlant(ddlPLant);
                        ddlPLant.SelectedValue = "0";
                        objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);
                        ddlLocation.SelectedIndex = -1;
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

        protected void ddlPLant_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);
                    objBindDDL.FillCostCenter(ddlCostCenter, ddlPLant.SelectedValue, ddlLocation.SelectedValue);
                }
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
                UploadExcelFileDetail();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
        protected void btnSaveDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvbulksoProduct.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please upload at least one Record to Import.');", true);
                }
                else
                {
                    var lstBulkWebsiteSoCreation = GetGridJsonValue();
                    string BulkWebsiteSoCreationImportJson = JsonConvert.SerializeObject(lstBulkWebsiteSoCreation);
                    int result = objMainClass.BulkWebsiteSoCreation(BulkWebsiteSoCreationImportJson);
                    if (result > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Records. :" + lstBulkWebsiteSoCreation.Count.ToString() + " So Created sucessfully." + "\");", true);
                        gvbulksoProduct.DataSource = null;
                        gvbulksoProduct.DataBind();
                        lgrecordcount.InnerText = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<BulkWebsiteSoCreationDetails> GetGridJsonValue()
        {
            List<BulkWebsiteSoCreationDetails> objlstBulkWebsiteSoCreation = new List<BulkWebsiteSoCreationDetails>();
            string NotificationImportJson = string.Empty;
            try
            {
                for (int i = 0; i < gvbulksoProduct.Rows.Count; i++)
                {
                    GridViewRow row = gvbulksoProduct.Rows[i];
                    if (Convert.ToInt32(((Label)row.FindControl("lblISSOALREADYCREATED")).Text) == 0
                        && Convert.ToInt32(((Label)row.FindControl("lblISVALIDREQUEST")).Text) == 1
                       )
                    {
                        BulkWebsiteSoCreationDetails objBulkWebsiteSoCreation = new BulkWebsiteSoCreationDetails();
                        objBulkWebsiteSoCreation.CMPID = Convert.ToInt32(((Label)row.FindControl("lblCMPID")).Text);
                        objBulkWebsiteSoCreation.SOTYPE = ((Label)row.FindControl("lblSOTYPE")).Text;
                        objBulkWebsiteSoCreation.SONO = ((Label)row.FindControl("lblSONO")).Text;
                        objBulkWebsiteSoCreation.SODT = ((Label)row.FindControl("lblSODT")).Text;
                        objBulkWebsiteSoCreation.SEGMENT = ((Label)row.FindControl("lblSEGMENT")).Text;
                        objBulkWebsiteSoCreation.DISTCHNL = ((Label)row.FindControl("lblDISTCHNL")).Text;
                        objBulkWebsiteSoCreation.BILLTOCODE = ((Label)row.FindControl("lblBILLTOCODE")).Text;
                        objBulkWebsiteSoCreation.SHIPTOCODE = ((Label)row.FindControl("lblSHIPTOCODE")).Text;
                        objBulkWebsiteSoCreation.PMTTERMS = ((Label)row.FindControl("lblPMTTERMS")).Text;
                        objBulkWebsiteSoCreation.REMARK = "SO CREATED VIA BULK SO";
                        objBulkWebsiteSoCreation.STATUS = Convert.ToInt32(((Label)row.FindControl("lblSTATUS")).Text);
                        objBulkWebsiteSoCreation.NETMATVALUE = Convert.ToDecimal(((Label)row.FindControl("lblNETMATVALUE")).Text);
                        objBulkWebsiteSoCreation.NETTAXAMT = Convert.ToDecimal(((Label)row.FindControl("lblNETTAXAMT")).Text);
                        objBulkWebsiteSoCreation.DISCOUNT = Convert.ToDecimal(((Label)row.FindControl("lblDISCOUNT")).Text);
                        objBulkWebsiteSoCreation.NETSOAMT = Convert.ToDecimal(((Label)row.FindControl("lblNETSOAMT")).Text);
                        objBulkWebsiteSoCreation.CNCLREASON = ((Label)row.FindControl("lblCNCLREASON")).Text;
                        objBulkWebsiteSoCreation.REFNO = ((Label)row.FindControl("lblREFNO")).Text;
                        objBulkWebsiteSoCreation.REFDT = ((Label)row.FindControl("lblREFDT")).Text;
                        objBulkWebsiteSoCreation.SALESFROM = Convert.ToInt32(((Label)row.FindControl("lblSALESFROM")).Text);
                        objBulkWebsiteSoCreation.CUSTNAME = ((Label)row.FindControl("lblCUSTNAME")).Text;
                        objBulkWebsiteSoCreation.CUSTADD1 = ((Label)row.FindControl("lblCUSTADD1")).Text;
                        objBulkWebsiteSoCreation.CUSTADD2 = ((Label)row.FindControl("lblCUSTADD2")).Text;
                        objBulkWebsiteSoCreation.CUSTADD3 = ((Label)row.FindControl("lblCUSTADD3")).Text;
                        objBulkWebsiteSoCreation.CITY = ((Label)row.FindControl("lblCITY")).Text;
                        objBulkWebsiteSoCreation.STATEID = Convert.ToInt32(((Label)row.FindControl("lblSTATEID")).Text);
                        objBulkWebsiteSoCreation.PINCODE = Convert.ToInt32(((Label)row.FindControl("lblPINCODE")).Text);
                        objBulkWebsiteSoCreation.CUSTMOBILENO = Convert.ToInt64(((Label)row.FindControl("lblCUSTMOBILENO")).Text);
                        objBulkWebsiteSoCreation.CUSTEMAILID = ((Label)row.FindControl("lblCUSTEMAILID")).Text;
                        objBulkWebsiteSoCreation.JOBID = ((Label)row.FindControl("lblJOBID")).Text;
                        objBulkWebsiteSoCreation.REFSONO = ((Label)row.FindControl("lblREFSONO")).Text;
                        objBulkWebsiteSoCreation.COMMAGENT = ((Label)row.FindControl("lblCOMMAGENT")).Text;
                        objBulkWebsiteSoCreation.SCHEMEID = Convert.ToInt32(((Label)row.FindControl("lblSCHEMEID")).Text);
                        objBulkWebsiteSoCreation.PAYMODE = Convert.ToInt32(((Label)row.FindControl("lblPAYMODE")).Text);
                        objBulkWebsiteSoCreation.PREPAIDAMT = Convert.ToDecimal(((Label)row.FindControl("lblPREPAIDAMT")).Text);
                        objBulkWebsiteSoCreation.REMAINAMT = Convert.ToDecimal(((Label)row.FindControl("lblREMAINAMT")).Text);
                        objBulkWebsiteSoCreation.SRNO = Convert.ToInt32(((Label)row.FindControl("lblSRNO")).Text);
                        objBulkWebsiteSoCreation.ITEMID = Convert.ToInt32(((Label)row.FindControl("lblITEMID")).Text);
                        objBulkWebsiteSoCreation.ITEMDESC = ((Label)row.FindControl("lblITEMDESC")).Text;
                        objBulkWebsiteSoCreation.PLANTCD = ((Label)row.FindControl("lblPLANTCD")).Text;
                        objBulkWebsiteSoCreation.LOCCD = ((Label)row.FindControl("lblLOCCD")).Text;
                        objBulkWebsiteSoCreation.ITEMGRPID = Convert.ToInt32(((Label)row.FindControl("lblITEMGRPID")).Text);
                        objBulkWebsiteSoCreation.SOQTY = Convert.ToInt32(((Label)row.FindControl("lblSOQTY")).Text);
                        objBulkWebsiteSoCreation.UOM = Convert.ToInt32(((Label)row.FindControl("lblUOM")).Text);
                        objBulkWebsiteSoCreation.RATE = Convert.ToDecimal(((Label)row.FindControl("lblRATE")).Text);
                        objBulkWebsiteSoCreation.CAMOUNT = Convert.ToDecimal(((Label)row.FindControl("lblCAMOUNT")).Text);
                        objBulkWebsiteSoCreation.DISCAMT = Convert.ToDecimal(((Label)row.FindControl("lblDISCAMT")).Text);
                        objBulkWebsiteSoCreation.DELIDT = ((Label)row.FindControl("lblDELIDT")).Text;
                        objBulkWebsiteSoCreation.GLCD = ((Label)row.FindControl("lblGLCD")).Text;
                        objBulkWebsiteSoCreation.CSTCENTCD = ((Label)row.FindControl("lblCSTCENTCD")).Text;
                        objBulkWebsiteSoCreation.PRFCNT = ((Label)row.FindControl("lblPRFCNT")).Text;
                        objBulkWebsiteSoCreation.ITEMTEXT = ((Label)row.FindControl("lblITEMTEXT")).Text;
                        objBulkWebsiteSoCreation.TAXAMT = Convert.ToInt32(((Label)row.FindControl("lblTAXAMT")).Text);
                        objBulkWebsiteSoCreation.CUSTPARTNO = ((Label)row.FindControl("lblCUSTPARTNO")).Text;
                        objBulkWebsiteSoCreation.CUSTPARTDESC = ((Label)row.FindControl("lblCUSTPARTDESC")).Text;
                        objBulkWebsiteSoCreation.CUSTPARTDESC2 = ((Label)row.FindControl("lblCUSTPARTDESC2")).Text;
                        objBulkWebsiteSoCreation.OLDITEMID = Convert.ToInt32(((Label)row.FindControl("lblOLDITEMID")).Text);
                        objBulkWebsiteSoCreation.CHANGEREASON = ((Label)row.FindControl("lblCHANGEREASON")).Text;
                        objBulkWebsiteSoCreation.PRODGRADE = ((Label)row.FindControl("lblPRODGRADE")).Text;
                        objlstBulkWebsiteSoCreation.Add(objBulkWebsiteSoCreation);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return objlstBulkWebsiteSoCreation;
        }
        public void UploadExcelFileDetail()
        {
            try
            {
                OleDbConnection MyConnection;
                string extension = Path.GetExtension(fuImage.FileName);
                string folderpath = "~/assets/";
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

                gvbulksoProduct.DataSource = null;
                gvbulksoProduct.DataBind();

                var objBulkWebsiteSoCreationValue = GetBulkWebsiteSoCreationDetail(dt);
                gvbulksoProduct.DataSource = objBulkWebsiteSoCreationValue.lstBulkWebsiteSoCreationDetail;
                gvbulksoProduct.DataBind();

                if (objBulkWebsiteSoCreationValue.lstBulkWebsiteSoCreationDetail.Count > 0)
                {
                    gvbulksoProduct.HeaderRow.TableSection = TableRowSection.TableHeader;
                    btnSave.Visible = true;
                }
                else
                {
                    btnSave.Visible = false;
                }
                lgrecordcount.InnerText = "Ready to So Insert Record :" + objBulkWebsiteSoCreationValue.totalcorrectvalue + " Not Insert So Record :" + objBulkWebsiteSoCreationValue.totalrejectvalue + " From Total Records : " + dt.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
        public BulkWebsiteSoCreationUploadValue GetBulkWebsiteSoCreationDetail(DataTable dt)
        {
            BulkWebsiteSoCreationUploadValue objBulkWebsiteSoCreationUploadValue = new BulkWebsiteSoCreationUploadValue();
            try
            {
                List<BulkWebsiteSoCreationDetails> lstBulkSoDetail = new List<BulkWebsiteSoCreationDetails>();
                int correctvalue = 0;
                int incorrectvalue = 0;

                if (dt.Rows.Count > 0)
                {
                    bool Isincorrect = false;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Isincorrect = false;
                        BulkWebsiteSoCreationDetails objBulkSoValue = new BulkWebsiteSoCreationDetails();

                        //var dtAsinDetail = objMainClass.GetASINITEMDETAIL(Convert.ToString(dt.Rows[i]["ASIN"].ToString()));
                        var dtItemDetail = objMainClass.GetItemDetailsByItemCode(objMainClass.intCmpId, Convert.ToString(dt.Rows[i]["ITEMCODE"]), 1, "ITEMMASTERSEARCH");

                        //Get From Master Field
                        objBulkSoValue.CMPID = 1;
                        objBulkSoValue.SOTYPE = "PSO";
                        objBulkSoValue.SONO = "";
                        objBulkSoValue.SODT = objMainClass.setDateFormat(Convert.ToString(dt.Rows[i]["Shipment Creation Date(SO DATE)"].ToString()), true);      //objMainClass.setDateFormat(objMainClass.indianTime.Date.ToString("dd-MM-yyyy"), true) ;
                        objBulkSoValue.SEGMENT = "1015";
                        objBulkSoValue.DISTCHNL = "50";
                        objBulkSoValue.BILLTOCODE = objMainClass.strConvertZeroPadding("10003");
                        objBulkSoValue.SHIPTOCODE = objMainClass.strConvertZeroPadding("10003");
                        objBulkSoValue.PMTTERMS = "ADV";
                        objBulkSoValue.REMARK = "";
                        objBulkSoValue.STATUS = 57;

                        //Calculated Amountc
                        objBulkSoValue.NETMATVALUE = Convert.ToDecimal(dt.Rows[i]["Order Value"].ToString());
                        objBulkSoValue.NETTAXAMT = 0;
                        objBulkSoValue.DISCOUNT = 0;
                        objBulkSoValue.NETSOAMT = Convert.ToDecimal(dt.Rows[i]["Order Value"].ToString());
                        //Calculated Amount

                        objBulkSoValue.CNCLREASON = "";
                        objBulkSoValue.REFNO = Convert.ToString(dt.Rows[i]["Customer Order ID(REF NO)"].ToString());
                        objBulkSoValue.REFDT = objMainClass.setDateFormat(objMainClass.indianTime.Date.ToString("dd-MM-yyyy"), true);
                        objBulkSoValue.SALESFROM = 11193;
                        objBulkSoValue.CUSTNAME = Convert.ToString(dt.Rows[i]["CUSTNAME"]); ;
                        objBulkSoValue.CUSTADD1 = Convert.ToString(dt.Rows[i]["address1"]); ;
                        objBulkSoValue.CUSTADD2 = Convert.ToString(dt.Rows[i]["address2"]); ;
                        objBulkSoValue.CUSTADD3 = Convert.ToString(dt.Rows[i]["address3"]); ;
                        objBulkSoValue.CITY = Convert.ToString(dt.Rows[i]["City"]);
                        objBulkSoValue.PINCODE = Convert.ToInt32(dt.Rows[i]["Pincode"]);//380024;
                        objBulkSoValue.CUSTMOBILENO = Convert.ToInt64(dt.Rows[i]["CUSTMOBILENO"]);
                        objBulkSoValue.CUSTEMAILID = Convert.ToString(dt.Rows[i]["CUSTEMAILID"]);
                        objBulkSoValue.ITEMCODE = Convert.ToString(dt.Rows[i]["ITEMCODE"]);
                        try
                        {
                            DataTable ds = new DataTable();
                            ds = objMainClass.SELECT_CITY_BYPINCODE((Convert.ToString(dt.Rows[i]["Pincode"].ToString())).Trim());
                            if (ds.Rows.Count > 0)
                            {
                                objBulkSoValue.STATEID = Convert.ToInt32(ds.Rows[0]["STATE_ID"].ToString());
                                objBulkSoValue.CITY = ds.Rows[0]["CITY_NAME"].ToString();
                                //objBulkSoValue.STATENAME = Convert.ToString(ds.Rows[0]["State"]);
                                objBulkSoValue.PINCODE = Convert.ToInt32((Convert.ToString(dt.Rows[i]["Pincode"].ToString())).Trim());
                            }
                            else
                            {
                                objBulkSoValue.STATEID = 0;
                                objBulkSoValue.CITY = (Convert.ToString(dt.Rows[i]["City"].ToString())).Trim();
                                //objBulkSoValue.STATENAME = (Convert.ToString(dt.Rows[i]["State"])).Trim();
                                objBulkSoValue.PINCODE = Convert.ToInt32((Convert.ToString(dt.Rows[i]["Pincode"].ToString())).Trim());
                                //objBulkSoValue.ERRORMSG = "Pincode is invalid or not available in DB";
                                Isincorrect = true;
                            }
                        }
                        catch (Exception)
                        {
                        }
                        objBulkSoValue.JOBID = "";
                        objBulkSoValue.REFSONO = "";
                        objBulkSoValue.COMMAGENT = "0000050121";
                        objBulkSoValue.SCHEMEID = 11913;
                        objBulkSoValue.PAYMODE = 5; //NEFT
                        objBulkSoValue.PREPAIDAMT = Convert.ToDecimal(dt.Rows[i]["Order Value"].ToString()); //NEFT
                        objBulkSoValue.REMAINAMT = 0;
                        //Get From Master Field

                        //Get From Child Field
                        objBulkSoValue.SRNO = 1;
                        if (dtItemDetail.Rows.Count > 0)
                        {
                            objBulkSoValue.ITEMID       = Convert.ToInt32(dtItemDetail.Rows[0]["ITEMID"].ToString());
                            objBulkSoValue.ITEMDESC     = Convert.ToString(dtItemDetail.Rows[0]["ITEMDESC"].ToString());
                            objBulkSoValue.ITEMGRPID    = Convert.ToInt32(dtItemDetail.Rows[0]["ITEMGRP"].ToString());
                             objBulkSoValue.UOM          = Convert.ToInt32(dtItemDetail.Rows[0]["UOM"].ToString());
                           // objBulkSoValue.GLCD = Convert.ToString(dtItemDetail.Rows[0]["GLCD"].ToString()); ;
                        }
                        else
                        {
                            objBulkSoValue.ITEMID = 0;
                            objBulkSoValue.ITEMDESC = "";
                            objBulkSoValue.ITEMGRPID = 0;
                            objBulkSoValue.UOM = 0;
                            objBulkSoValue.GLCD = "";
                        }

                        objBulkSoValue.PLANTCD = ddlPLant.SelectedValue;
                        objBulkSoValue.LOCCD = ddlLocation.SelectedValue;
                        objBulkSoValue.SOQTY = Convert.ToInt32(dt.Rows[i]["Units(QTY)"].ToString());
                        objBulkSoValue.RATE = Convert.ToDecimal(dt.Rows[i]["Order Value"].ToString());
                        objBulkSoValue.CAMOUNT = Convert.ToDecimal(dt.Rows[i]["Order Value"].ToString());
                        objBulkSoValue.DISCAMT = 0;
                        objBulkSoValue.DELIDT = objMainClass.setDateFormat(Convert.ToString(dt.Rows[i]["ExSD"].ToString()), true); //Convert.ToString(dt.Rows[i]["ExSD"].ToString());       //objMainClass.setDateFormat(objMainClass.indianTime.Date.ToString("dd-MM-yyyy"), true);
                        objBulkSoValue.CSTCENTCD = ddlCostCenter.SelectedValue;
                        objBulkSoValue.PRFCNT = "1000";
                        objBulkSoValue.ITEMTEXT = "";
                        objBulkSoValue.TAXAMT = 0;
                        objBulkSoValue.CUSTPARTNO = "";
                        objBulkSoValue.CUSTPARTDESC = "";
                        objBulkSoValue.CUSTPARTDESC2 = "";
                        objBulkSoValue.OLDITEMID = 0;
                        objBulkSoValue.CHANGEREASON = "";
                        objBulkSoValue.PRODGRADE = "GRADE A";
                        objBulkSoValue.JOBID = "";
                        //Get From Child Field

                        var dtIsExistSo = objMainClass.GetSODetail(Convert.ToString(dt.Rows[i]["Customer Order ID(REF NO)"].ToString()));
                        if (dtIsExistSo.Rows.Count > 0)
                        {
                            objBulkSoValue.ALREADYCREATEDSONO = dtIsExistSo.Rows[0]["SONO"].ToString();
                            objBulkSoValue.ISSOALREADYCREATED = 1;
                            Isincorrect = true;
                        }

                        if (Convert.ToString(dt.Rows[i]["Customer Order ID(REF NO)"].ToString()).Length == 0 || Convert.ToInt32(dt.Rows[i]["Units(QTY)"].ToString()) == 0
                          || Convert.ToDecimal(dt.Rows[i]["Order Value"].ToString()) == 0
                          
                          )
                        {
                            objBulkSoValue.ISVALIDREQUEST = 0;
                            Isincorrect = true;
                        }

                        if (Isincorrect == true)
                        {
                            incorrectvalue = incorrectvalue + 1;
                        }
                        else
                        {
                            correctvalue = correctvalue + 1;
                        }
                        lstBulkSoDetail.Add(objBulkSoValue);
                    }

                }
                objBulkWebsiteSoCreationUploadValue.lstBulkWebsiteSoCreationDetail = lstBulkSoDetail;
                objBulkWebsiteSoCreationUploadValue.totalcorrectvalue = correctvalue;
                objBulkWebsiteSoCreationUploadValue.totalrejectvalue = incorrectvalue;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return objBulkWebsiteSoCreationUploadValue;
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