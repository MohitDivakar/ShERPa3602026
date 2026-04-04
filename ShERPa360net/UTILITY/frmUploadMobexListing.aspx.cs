using Newtonsoft.Json;
using RestSharp;
using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.UTILITY
{
    public partial class frmUploadMobexListing : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();

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
                            lnkUpload.Enabled = false;
                            lnkUpload.Visible = false;
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

        protected void lnkUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    OleDbConnection MyConnection;
                    string extension = Path.GetExtension(flUpload.FileName);
                    string folderpath = "~/excel/";
                    string filePath = Path.Combine(Server.MapPath(folderpath), Guid.NewGuid().ToString("N") + extension);
                    flUpload.SaveAs(filePath);
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
                                cmd.CommandText = (Convert.ToString("SELECT * From [" + sheetName + "]"));
                                cmd.Connection = MyConnection;
                                oda.SelectCommand = cmd;
                                oda.Fill(dt);
                                MyConnection.Close();
                            }
                        }
                    }
                    File.Delete(filePath);


                    grvList.DataSource = string.Empty;
                    grvList.DataBind();

                    grvList.DataSource = dt;
                    grvList.DataBind();

                    if (dt.Rows.Count > 0)
                    {
                        lblRecoretxt.Visible = true;
                        lblRecord.Visible = true;

                        lblRecord.Text = dt.Rows.Count.ToString();
                        lnkSave.Visible = true;
                    }
                    else
                    {
                        lblRecoretxt.Visible = false;
                        lblRecord.Visible = false;

                        lblRecord.Text = "0";
                        lnkSave.Visible = false;
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

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };


                    DataTable dtPriceAPI = new DataTable();
                    dtPriceAPI = objMainClass.GetWAData("MOBEXPRICEUPDATE", 1, "GETWADATA");
                    if (dtPriceAPI.Rows.Count > 0)
                    {
                        DataTable dtQtyAPI = new DataTable();
                        dtQtyAPI = objMainClass.GetWAData("MOBEXQTYUPDATE", 1, "GETWADATA");
                        if (dtQtyAPI.Rows.Count > 0)
                        {
                            DataTable dtprice = new DataTable("Data");
                            dtprice.Columns.Add("ItemCode");
                            dtprice.Columns.Add("Error");

                            foreach (GridViewRow row in grvList.Rows)
                            {

                                string SKU = Convert.ToString(row.Cells[0].Text);
                                decimal AVAILQTY = Convert.ToDecimal(row.Cells[1].Text);
                                decimal QUANTITY = Convert.ToDecimal(row.Cells[2].Text);
                                decimal PRICE = Convert.ToDecimal(row.Cells[3].Text);
                                decimal MRP = Convert.ToDecimal(row.Cells[4].Text);
                                int STATUS = Convert.ToInt32(row.Cells[5].Text);

                                List<SourceItem> objSourceItem = new List<SourceItem>();
                                objSourceItem.Add(new SourceItem
                                {
                                    quantity = QUANTITY,
                                    sku = SKU,
                                    source_code = "default",
                                    status = STATUS,
                                });


                                MobexListing objMobexListing = new MobexListing();
                                objMobexListing.sourceItems = objSourceItem;

                                var jsonqty = new JavaScriptSerializer().Serialize(objMobexListing);

                                List<CustomAttribute> objMobexAttriute = new List<CustomAttribute>();
                                objMobexAttriute.Add(new CustomAttribute
                                {
                                    attribute_code = "special_price",
                                    value = PRICE
                                });

                                Product objMobexProduct = new Product();
                                objMobexProduct.price = MRP;
                                objMobexProduct.custom_attributes = objMobexAttriute;

                                MobexPrice objMobexPrice = new MobexPrice();
                                objMobexPrice.product = objMobexProduct;

                                var json = new JavaScriptSerializer().Serialize(objMobexPrice);

                                string URL = Convert.ToString(dtPriceAPI.Rows[0]["OTHER"]);
                                string KEYNAME = Convert.ToString(dtPriceAPI.Rows[0]["KEYNAME"]);
                                string KEYVALUE = Convert.ToString(dtPriceAPI.Rows[0]["KEYVALUE"]);

                                URL = URL.Replace("@sku", SKU);

                                var client = new RestClient(URL);
                                client.Timeout = -1;
                                var request = new RestRequest(Method.PUT);
                                request.AddHeader("content-type", "application/json");
                                request.AddHeader("Authorization", "" + KEYNAME + " " + KEYVALUE + "");
                                request.AddParameter("application/json", json, ParameterType.RequestBody);
                                IRestResponse response = client.Execute(request);
                                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    MobexPriceResponse objMobexPriceResponse = new MobexPriceResponse();
                                    string jsonres = response.Content;
                                    objMobexPriceResponse = JsonConvert.DeserializeObject<MobexPriceResponse>(jsonres);


                                    string URLqty = Convert.ToString(dtQtyAPI.Rows[0]["OTHER"]);
                                    string KEYNAMEqty = Convert.ToString(dtQtyAPI.Rows[0]["KEYNAME"]);
                                    string KEYVALUEqty = Convert.ToString(dtQtyAPI.Rows[0]["KEYVALUE"]);

                                    var clientqty = new RestClient(URLqty);
                                    clientqty.Timeout = -1;
                                    var requestqty = new RestRequest(Method.POST);
                                    requestqty.AddHeader("content-type", "application/json");
                                    requestqty.AddHeader("Authorization", "" + KEYNAMEqty + " " + KEYVALUEqty + "");
                                    requestqty.AddParameter("application/json", jsonqty, ParameterType.RequestBody);
                                    IRestResponse responseqty = clientqty.Execute(requestqty);
                                    if (responseqty.StatusCode == System.Net.HttpStatusCode.OK)
                                    {


                                        if (objMobexPriceResponse.price == MRP)
                                        {
                                            int i = objMainClass.insertListingLog(objMainClass.intCmpId, SKU, PRICE, QUANTITY, AVAILQTY, MRP, STATUS, json, jsonqty, Convert.ToInt32(Session["USERID"]), "Price and Quantity Updated.", "", "INSERTLOG");
                                        }
                                        else
                                        {
                                            dtprice.Rows.Add(SKU, "Quantity Updated. Price not Updated.");
                                            int i = objMainClass.insertListingLog(objMainClass.intCmpId, SKU, PRICE, QUANTITY, AVAILQTY, MRP, STATUS, json, jsonqty, Convert.ToInt32(Session["USERID"]), "Quantity Updated. Price not Updated.", "Quantity Updated. Price not Updated.", "INSERTLOG");
                                            int ii = objMainClass.insertListingLog(objMainClass.intCmpId, SKU, PRICE, QUANTITY, AVAILQTY, MRP, STATUS, json, jsonqty, Convert.ToInt32(Session["USERID"]), "Quantity Updated. Price not Updated.", "Quantity Updated. Price not Updated.", "INSERTERRORLOG");
                                        }
                                    }
                                    else
                                    {
                                        if (objMobexPriceResponse.price == MRP)
                                        {
                                            dtprice.Rows.Add(SKU, "Price Updated. Quantity not Updated.");
                                            int i = objMainClass.insertListingLog(objMainClass.intCmpId, SKU, PRICE, QUANTITY, AVAILQTY, MRP, STATUS, json, jsonqty, Convert.ToInt32(Session["USERID"]), "Price Updated. Quantity not Updated.", "Price Updated. Quantity not Updated.", "INSERTLOG");
                                            int ii = objMainClass.insertListingLog(objMainClass.intCmpId, SKU, PRICE, QUANTITY, AVAILQTY, MRP, STATUS, json, jsonqty, Convert.ToInt32(Session["USERID"]), "Price Updated. Quantity not Updated.", "Price Updated. Quantity not Updated.", "INSERTERRORLOG");
                                        }
                                        else
                                        {
                                            dtprice.Rows.Add(SKU, "Price and Quantity not Updated.");
                                            int ii = objMainClass.insertListingLog(objMainClass.intCmpId, SKU, PRICE, QUANTITY, AVAILQTY, MRP, STATUS, json, jsonqty, Convert.ToInt32(Session["USERID"]), "Price and Quantity not Updated.", "Price and Quantity not Updated.", "INSERTERRORLOG");
                                        }

                                    }

                                }
                                else
                                {
                                    if (response.StatusCode == HttpStatusCode.BadRequest)
                                    {
                                        string jsonres = response.Content;
                                        MobexBadRequestResponse objMobexBadRequestResponse = new MobexBadRequestResponse();
                                        objMobexBadRequestResponse = JsonConvert.DeserializeObject<MobexBadRequestResponse>(jsonres);
                                        string errmsg = "Price and Quantity not Updated. " + objMobexBadRequestResponse.message;
                                        dtprice.Rows.Add(SKU, errmsg);
                                        int ii = objMainClass.insertListingLog(objMainClass.intCmpId, SKU, PRICE, QUANTITY, AVAILQTY, MRP, STATUS, json, jsonqty, Convert.ToInt32(Session["USERID"]), errmsg, errmsg, "INSERTERRORLOG");
                                    }
                                    else
                                    {
                                        dtprice.Rows.Add(SKU, "Price and Quantity not Updated.");
                                        int ii = objMainClass.insertListingLog(objMainClass.intCmpId, SKU, PRICE, QUANTITY, AVAILQTY, MRP, STATUS, json, jsonqty, Convert.ToInt32(Session["USERID"]), "Price and Quantity not Updated.", "Price and Quantity not Updated.", "INSERTERRORLOG");
                                    }


                                }

                            }

                            if (dtprice.Rows.Count > 0)
                            {
                                Session["ERRORSKU"] = dtprice;
                                int total = grvList.Rows.Count;
                                int update = grvList.Rows.Count - dtprice.Rows.Count;
                                int remain = dtprice.Rows.Count;

                                string errormessage = update + " SKUs uploaded out of " + total + ". " + remain + " SKUs not uploaded due to some error. Please refer downloaded file for Information.";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Sucess : " + errormessage + "\");$('.close').click(function(){window.location.href ='frmUploadMobexListing.aspx' });", true);
                                string path = "frmErrorSKUDownload.aspx";
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Quantity and Price Uploaded Successfully.!');$('.close').click(function(){window.location.href ='frmUploadMobexListing.aspx' });", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Quantity Update API Not Found. Please contact administrator.!');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Price Update API Not Found. Please contact administrator.!');", true);
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