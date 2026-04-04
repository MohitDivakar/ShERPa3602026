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
    
    public partial class BulkCreatedSoNameAddUpdate : System.Web.UI.Page
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
                if (gvbulksoAddressupdate.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please upload at least one Record to Import.');", true);
                }
                else
                {
                    var lstbulksocreation           = GetGridJsonValue();
                    string BulkSoCreationImportJson = JsonConvert.SerializeObject(lstbulksocreation);
                    int result                      = objMainClass.BulkSoAddUpdate(BulkSoCreationImportJson,Convert.ToInt32(Session["USERID"].ToString()));
                    if (result > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Records. :" + lstbulksocreation.Count.ToString() + " So Address update sucessfully." + "\");", true);
                        gvbulksoAddressupdate.DataSource = null;
                        gvbulksoAddressupdate.DataBind();
                        lgrecordcount.InnerText = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<BulkSoNameAddDetailJson> GetGridJsonValue()
        {
            List<BulkSoNameAddDetailJson> objlstbulksoaddressupdate = new List<BulkSoNameAddDetailJson>();
            try
            {
                for (int i = 0; i < gvbulksoAddressupdate.Rows.Count; i++)
                {
                    GridViewRow row = gvbulksoAddressupdate.Rows[i];
                    if (((Label)row.FindControl("lblERRORMSG")).Text.Length == 0
                        && Convert.ToInt32(((Label)row.FindControl("lblISVALIDREQUEST")).Text) == 1
                       )
                    {
                        BulkSoNameAddDetailJson objbulksoaddress   = new BulkSoNameAddDetailJson();
                        objbulksoaddress.SONO                      = ((Label)row.FindControl("lblSONO")).Text;
                        objbulksoaddress.REFNO                     = ((Label)row.FindControl("lblREFNO")).Text;
                        objbulksoaddress.CUSTNAME                  = ((Label)row.FindControl("lblCUSTNAME")).Text.ToUpper();
                        objbulksoaddress.CUSTADD1                  = ((Label)row.FindControl("lblCUSTADD1")).Text.ToUpper();
                        objbulksoaddress.CUSTADD2                  = ((Label)row.FindControl("lblCUSTADD2")).Text.ToUpper();
                        objbulksoaddress.CITY                      = ((Label)row.FindControl("lblCITY")).Text.ToUpper();
                        objbulksoaddress.STATEID                   = Convert.ToInt32(((Label)row.FindControl("lblSTATEID")).Text);
                        objbulksoaddress.PINCODE                   = Convert.ToInt32(((Label)row.FindControl("lblPINCODE")).Text);
                        objlstbulksoaddressupdate.Add(objbulksoaddress);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return objlstbulksoaddressupdate;
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

                gvbulksoAddressupdate.DataSource = null;
                gvbulksoAddressupdate.DataBind();

                var objBulkSoAddressValue        = GetBulkSoAddressupdateDetail(dt);
                gvbulksoAddressupdate.DataSource = objBulkSoAddressValue.lstBulkSoAddressDetail;
                gvbulksoAddressupdate.DataBind();

                if(objBulkSoAddressValue.lstBulkSoAddressDetail.Count > 0)
                {
                    gvbulksoAddressupdate.HeaderRow.TableSection = TableRowSection.TableHeader;
                    btnSave.Visible = true;
                }
                else
                {
                    btnSave.Visible = false;
                }
                lgrecordcount.InnerText = "Ready to Address Update Record :" + objBulkSoAddressValue.totalcorrectvalue + " Not Address Update Record :" + objBulkSoAddressValue.totalrejectvalue + " From Total Records : " + dt.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
        public BulkSoAddUpdateValue GetBulkSoAddressupdateDetail(DataTable dt)
        {
            BulkSoAddUpdateValue objBulkSoAddressUploadValue = new BulkSoAddUpdateValue();
            try
            {
                List<BulkSoNameAddDetailUpdate> lstBulkSoAddressDetail = new List<BulkSoNameAddDetailUpdate>();
                int correctvalue = 0;
                int incorrectvalue = 0;

                if (dt.Rows.Count > 0)
                {
                    bool Isincorrect = false;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Isincorrect = false;
                        BulkSoNameAddDetailUpdate objBulkSoValue = new BulkSoNameAddDetailUpdate();
                        var dtIsExistSo                          = objMainClass.GetSODetail(Convert.ToString(dt.Rows[i]["OrderNumber"].ToString()));
                        objBulkSoValue.REFNO                     = (Convert.ToString(dt.Rows[i]["OrderNumber"].ToString())).Trim();
                        objBulkSoValue.CUSTNAME                  = (Convert.ToString(dt.Rows[i]["customer_name"].ToString())).Trim().ToUpper();
                        if (dtIsExistSo.Rows.Count > 0)
                        {
                            objBulkSoValue.SONO               = dtIsExistSo.Rows[0]["SONO"].ToString();
                        }
                        else
                        {
                            objBulkSoValue.SONO               = "";
                            objBulkSoValue.ERRORMSG           = "SO IS NOT AVAILABLE";
                            Isincorrect                       = true;
                        }

                        DataTable ds = new DataTable();
                        ds = objMainClass.SELECT_CITY_BYPINCODE((Convert.ToString(dt.Rows[i]["Pincode"].ToString())).Trim());
                        if (ds.Rows.Count > 0)
                        {
                            objBulkSoValue.STATEID        = Convert.ToInt32(ds.Rows[0]["STATE_ID"].ToString());
                            objBulkSoValue.CITY           = ds.Rows[0]["CITY_NAME"].ToString();
                            objBulkSoValue.STATENAME      = ds.Rows[0]["STATENAME"].ToString();
                            objBulkSoValue.PINCODE        = Convert.ToInt32((Convert.ToString(dt.Rows[i]["Pincode"].ToString())).Trim());
                        }
                        else
                        {
                            objBulkSoValue.STATEID        = 0;
                            objBulkSoValue.CITY           = (Convert.ToString(dt.Rows[i]["City"].ToString())).Trim();
                            objBulkSoValue.STATENAME      = (Convert.ToString(dt.Rows[i]["State"].ToString())).Trim();
                            objBulkSoValue.PINCODE        = Convert.ToInt32((Convert.ToString(dt.Rows[i]["Pincode"].ToString())).Trim());
                            objBulkSoValue.ERRORMSG       = "Pincode is invalid or not available in DB";
                            Isincorrect                   = true;
                        }
                        string actualaddress              = (Convert.ToString(dt.Rows[i]["Address"].ToString())).Trim().ToUpper();
                        actualaddress                     = actualaddress.Replace( (", " + ((Convert.ToString(dt.Rows[i]["Pincode"].ToString())).Trim().ToUpper()) + " IN"),"");
                        actualaddress                     = actualaddress.Replace(((Convert.ToString(dt.Rows[i]["City"].ToString())).Trim().ToUpper()), "");
                        actualaddress                     = actualaddress.Replace(((Convert.ToString(dt.Rows[i]["State"].ToString())).Trim().ToUpper()), "");
                        if(actualaddress.Length > 50)
                        {
                            string address1               = "";
                            string address2               = "";
                            address1                      = actualaddress.Substring(0,50);
                            address2                      = actualaddress.Substring(50);
                            objBulkSoValue.CUSTADD1       = address1;
                            objBulkSoValue.CUSTADD2       = address2;
                        }
                        else
                        {
                            objBulkSoValue.CUSTADD1       = actualaddress;
                            objBulkSoValue.CUSTADD2       = "";  
                        }

                        if (Isincorrect == true)
                        {
                            incorrectvalue                 = incorrectvalue + 1;
                            objBulkSoValue.ISVALIDSO       = 0;  
                        }
                        else
                        {
                            correctvalue = correctvalue + 1;
                            objBulkSoValue.ISVALIDSO       = 1;
                        }
                        lstBulkSoAddressDetail.Add(objBulkSoValue);
                    }
                }
                objBulkSoAddressUploadValue.lstBulkSoAddressDetail = lstBulkSoAddressDetail;
                objBulkSoAddressUploadValue.totalcorrectvalue = correctvalue;
                objBulkSoAddressUploadValue.totalrejectvalue = incorrectvalue;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return objBulkSoAddressUploadValue;
        }
    }
}