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

namespace ShERPa360net.SD
{

    public partial class BulkSoCreation : System.Web.UI.Page
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
                    var lstbulksocreation = GetGridJsonValue();
                    string BulkSoCreationImportJson = JsonConvert.SerializeObject(lstbulksocreation);
                    int result = objMainClass.BulkSoCreation(BulkSoCreationImportJson);
                    if (result > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Records. :" + lstbulksocreation.Count.ToString() + " So Created sucessfully." + "\");", true);
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
        public List<BulkSoCreationDetails> GetGridJsonValue()
        {
            List<BulkSoCreationDetails> objlstbulksocreation = new List<BulkSoCreationDetails>();
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
                        DataTable dtLock = new DataTable();
                        dtLock = objMainClass.GetLockPrice("", "", "", "", "", "", ((Label)row.FindControl("lblITEMDESC")).Text);
                        decimal lockprice = 0;
                        if (dtLock.Rows.Count > 0)
                        {
                            lockprice = Convert.ToDecimal(dtLock.Rows[0]["FinalApproveListingAmount"]);
                        }

                        BulkSoCreationDetails objbulksocreation = new BulkSoCreationDetails();
                        objbulksocreation.CMPID = Convert.ToInt32(((Label)row.FindControl("lblCMPID")).Text);
                        objbulksocreation.SOTYPE = ((Label)row.FindControl("lblSOTYPE")).Text;
                        objbulksocreation.SONO = ((Label)row.FindControl("lblSONO")).Text;
                        objbulksocreation.SODT = ((Label)row.FindControl("lblSODT")).Text;
                        objbulksocreation.SEGMENT = ((Label)row.FindControl("lblSEGMENT")).Text;
                        objbulksocreation.DISTCHNL = ((Label)row.FindControl("lblDISTCHNL")).Text;
                        objbulksocreation.BILLTOCODE = ((Label)row.FindControl("lblBILLTOCODE")).Text;
                        objbulksocreation.SHIPTOCODE = ((Label)row.FindControl("lblSHIPTOCODE")).Text;
                        objbulksocreation.PMTTERMS = ((Label)row.FindControl("lblPMTTERMS")).Text;
                        objbulksocreation.REMARK = "SO CREATED VIA BULK SO";
                        objbulksocreation.STATUS = Convert.ToInt32(((Label)row.FindControl("lblSTATUS")).Text);
                        objbulksocreation.NETMATVALUE = Convert.ToDecimal(((Label)row.FindControl("lblNETMATVALUE")).Text);
                        objbulksocreation.NETTAXAMT = Convert.ToDecimal(((Label)row.FindControl("lblNETTAXAMT")).Text);
                        objbulksocreation.DISCOUNT = Convert.ToDecimal(((Label)row.FindControl("lblDISCOUNT")).Text);
                        objbulksocreation.NETSOAMT = Convert.ToDecimal(((Label)row.FindControl("lblNETSOAMT")).Text);
                        objbulksocreation.CNCLREASON = ((Label)row.FindControl("lblCNCLREASON")).Text;
                        objbulksocreation.REFNO = ((Label)row.FindControl("lblREFNO")).Text;
                        objbulksocreation.REFDT = ((Label)row.FindControl("lblREFDT")).Text;
                        objbulksocreation.SALESFROM = Convert.ToInt32(((Label)row.FindControl("lblSALESFROM")).Text);
                        objbulksocreation.CUSTNAME = ((Label)row.FindControl("lblCUSTNAME")).Text;
                        objbulksocreation.CUSTADD1 = ((Label)row.FindControl("lblCUSTADD1")).Text;
                        objbulksocreation.CUSTADD2 = ((Label)row.FindControl("lblCUSTADD2")).Text;
                        objbulksocreation.CUSTADD3 = ((Label)row.FindControl("lblCUSTADD3")).Text;
                        objbulksocreation.CITY = ((Label)row.FindControl("lblCITY")).Text;
                        objbulksocreation.STATEID = Convert.ToInt32(((Label)row.FindControl("lblSTATEID")).Text);
                        objbulksocreation.PINCODE = Convert.ToInt32(((Label)row.FindControl("lblPINCODE")).Text);
                        objbulksocreation.CUSTMOBILENO = Convert.ToInt32(((Label)row.FindControl("lblCUSTMOBILENO")).Text);
                        objbulksocreation.CUSTEMAILID = ((Label)row.FindControl("lblCUSTEMAILID")).Text;
                        objbulksocreation.JOBID = ((Label)row.FindControl("lblJOBID")).Text;
                        objbulksocreation.REFSONO = ((Label)row.FindControl("lblREFSONO")).Text;
                        objbulksocreation.COMMAGENT = ((Label)row.FindControl("lblCOMMAGENT")).Text;
                        objbulksocreation.SCHEMEID = Convert.ToInt32(((Label)row.FindControl("lblSCHEMEID")).Text);
                        objbulksocreation.PAYMODE = Convert.ToInt32(((Label)row.FindControl("lblPAYMODE")).Text);
                        objbulksocreation.PREPAIDAMT = Convert.ToDecimal(((Label)row.FindControl("lblPREPAIDAMT")).Text);
                        objbulksocreation.REMAINAMT = Convert.ToDecimal(((Label)row.FindControl("lblREMAINAMT")).Text);
                        objbulksocreation.SRNO = Convert.ToInt32(((Label)row.FindControl("lblSRNO")).Text);
                        objbulksocreation.ITEMID = Convert.ToInt32(((Label)row.FindControl("lblITEMID")).Text);
                        objbulksocreation.ITEMDESC = ((Label)row.FindControl("lblITEMDESC")).Text;
                        objbulksocreation.PLANTCD = ((Label)row.FindControl("lblPLANTCD")).Text;
                        objbulksocreation.LOCCD = ((Label)row.FindControl("lblLOCCD")).Text;
                        objbulksocreation.ITEMGRPID = Convert.ToInt32(((Label)row.FindControl("lblITEMGRPID")).Text);
                        objbulksocreation.SOQTY = Convert.ToInt32(((Label)row.FindControl("lblSOQTY")).Text);
                        objbulksocreation.UOM = Convert.ToInt32(((Label)row.FindControl("lblUOM")).Text);
                        objbulksocreation.RATE = Convert.ToDecimal(((Label)row.FindControl("lblRATE")).Text);
                        objbulksocreation.CAMOUNT = Convert.ToDecimal(((Label)row.FindControl("lblCAMOUNT")).Text);
                        objbulksocreation.DISCAMT = Convert.ToDecimal(((Label)row.FindControl("lblDISCAMT")).Text);
                        objbulksocreation.DELIDT = ((Label)row.FindControl("lblDELIDT")).Text;
                        objbulksocreation.GLCD = ((Label)row.FindControl("lblGLCD")).Text;
                        objbulksocreation.CSTCENTCD = ((Label)row.FindControl("lblCSTCENTCD")).Text;
                        objbulksocreation.PRFCNT = ((Label)row.FindControl("lblPRFCNT")).Text;
                        objbulksocreation.ITEMTEXT = ((Label)row.FindControl("lblITEMTEXT")).Text;
                        objbulksocreation.TAXAMT = Convert.ToInt32(((Label)row.FindControl("lblTAXAMT")).Text);
                        objbulksocreation.CUSTPARTNO = ((Label)row.FindControl("lblCUSTPARTNO")).Text;
                        objbulksocreation.CUSTPARTDESC = ((Label)row.FindControl("lblCUSTPARTDESC")).Text;
                        objbulksocreation.CUSTPARTDESC2 = ((Label)row.FindControl("lblCUSTPARTDESC2")).Text;
                        objbulksocreation.OLDITEMID = Convert.ToInt32(((Label)row.FindControl("lblOLDITEMID")).Text);
                        objbulksocreation.CHANGEREASON = ((Label)row.FindControl("lblCHANGEREASON")).Text;
                        objbulksocreation.PRODGRADE = ((Label)row.FindControl("lblPRODGRADE")).Text;
                        objbulksocreation.LOCKAMT = lockprice;
                        objlstbulksocreation.Add(objbulksocreation);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return objlstbulksocreation;
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

                var objBulkSoCreationValue = GetBulkSoCreationDetail(dt);
                gvbulksoProduct.DataSource = objBulkSoCreationValue.lstBulkSoCreationDetail;
                gvbulksoProduct.DataBind();

                if (objBulkSoCreationValue.lstBulkSoCreationDetail.Count > 0)
                {
                    gvbulksoProduct.HeaderRow.TableSection = TableRowSection.TableHeader;
                    btnSave.Visible = true;
                }
                else
                {
                    btnSave.Visible = false;
                }
                lgrecordcount.InnerText = "Ready to So Insert Record :" + objBulkSoCreationValue.totalcorrectvalue + " Not Insert So Record :" + objBulkSoCreationValue.totalrejectvalue + " From Total Records : " + dt.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
        public BulkSoCreationUploadValue GetBulkSoCreationDetail(DataTable dt)
        {
            BulkSoCreationUploadValue objBulkSoCreationUploadValue = new BulkSoCreationUploadValue();
            try
            {
                List<BulkSoCreationDetails> lstBulkSoDetail = new List<BulkSoCreationDetails>();
                int correctvalue = 0;
                int incorrectvalue = 0;

                if (dt.Rows.Count > 0)
                {
                    bool Isincorrect = false;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Isincorrect = false;
                        BulkSoCreationDetails objBulkSoValue = new BulkSoCreationDetails();

                        var dtAsinDetail = objMainClass.GetASINITEMDETAIL(Convert.ToString(dt.Rows[i]["ASIN"].ToString()));

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
                        objBulkSoValue.REFDT = Convert.ToString(dt.Rows[i]["REFDATE"].ToString()).Length > 0 ? objMainClass.setDateFormat(Convert.ToString(dt.Rows[i]["REFDATE"].ToString()), true) : objMainClass.setDateFormat(objMainClass.indianTime.Date.ToString("dd-MM-yyyy"), true);
                        objBulkSoValue.SALESFROM = 10906;
                        objBulkSoValue.CUSTNAME = "";
                        objBulkSoValue.CUSTADD1 = "";
                        objBulkSoValue.CUSTADD2 = "";
                        objBulkSoValue.CUSTADD3 = "";
                        objBulkSoValue.CITY = "AHMEDABAD";
                        objBulkSoValue.STATEID = 1;
                        objBulkSoValue.PINCODE = 380024;
                        objBulkSoValue.CUSTMOBILENO = 0;
                        objBulkSoValue.CUSTEMAILID = "";
                        objBulkSoValue.JOBID = "";
                        objBulkSoValue.REFSONO = "";
                        objBulkSoValue.COMMAGENT = "0000050096";
                        objBulkSoValue.SCHEMEID = 11913;
                        objBulkSoValue.PAYMODE = 5; //NEFT
                        objBulkSoValue.PREPAIDAMT = Convert.ToDecimal(dt.Rows[i]["Order Value"].ToString()); //NEFT
                        objBulkSoValue.REMAINAMT = 0;
                        //Get From Master Field

                        //Get From Child Field
                        objBulkSoValue.SRNO = 1;
                        if (dtAsinDetail.Rows.Count > 0)
                        {
                            objBulkSoValue.ITEMID = Convert.ToInt32(dtAsinDetail.Rows[0]["ITEMID"].ToString());
                            objBulkSoValue.ITEMDESC = Convert.ToString(dtAsinDetail.Rows[0]["ITEMDESC"].ToString());
                            objBulkSoValue.ITEMGRPID = Convert.ToInt32(dtAsinDetail.Rows[0]["ITEMGRP"].ToString());
                            objBulkSoValue.UOM = Convert.ToInt32(dtAsinDetail.Rows[0]["UOM"].ToString());
                            objBulkSoValue.GLCD = Convert.ToString(dtAsinDetail.Rows[0]["GLCD"].ToString()); ;
                        }
                        else
                        {
                            objBulkSoValue.ITEMID = 0;
                            objBulkSoValue.ITEMDESC = "";
                            objBulkSoValue.ITEMGRPID = 0;
                            objBulkSoValue.UOM = 0;
                            objBulkSoValue.GLCD = "";
                        }

                        if (Convert.ToInt32(ddlPlantLocationUploadFrom.SelectedValue) == 1)
                        {
                            objBulkSoValue.PLANTCD = ddlPLant.SelectedValue;
                            objBulkSoValue.LOCCD = ddlLocation.SelectedValue;
                            objBulkSoValue.CSTCENTCD = ddlCostCenter.SelectedValue;
                        }
                        else
                        {
                            objBulkSoValue.PLANTCD = Convert.ToString(dt.Rows[i]["PLANTCD"].ToString());
                            objBulkSoValue.LOCCD = Convert.ToString(dt.Rows[i]["LOCCD"].ToString());
                            objBulkSoValue.CSTCENTCD = Convert.ToString(dt.Rows[i]["CSTCENTCD"].ToString());
                        }


                        objBulkSoValue.SOQTY = Convert.ToInt32(dt.Rows[i]["Units(QTY)"].ToString());
                        objBulkSoValue.RATE = Convert.ToDecimal(dt.Rows[i]["Order Value"].ToString());
                        objBulkSoValue.CAMOUNT = Convert.ToDecimal(dt.Rows[i]["Order Value"].ToString());
                        objBulkSoValue.DISCAMT = 0;
                        objBulkSoValue.DELIDT = objMainClass.setDateFormat(Convert.ToString(dt.Rows[i]["ExSD"].ToString()), true); //Convert.ToString(dt.Rows[i]["ExSD"].ToString());       //objMainClass.setDateFormat(objMainClass.indianTime.Date.ToString("dd-MM-yyyy"), true);
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
                          || Convert.ToString(dt.Rows[i]["ASIN"].ToString()).Length == 0 || dtAsinDetail.Rows.Count == 0
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
                objBulkSoCreationUploadValue.lstBulkSoCreationDetail = lstBulkSoDetail;
                objBulkSoCreationUploadValue.totalcorrectvalue = correctvalue;
                objBulkSoCreationUploadValue.totalrejectvalue = incorrectvalue;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return objBulkSoCreationUploadValue;
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