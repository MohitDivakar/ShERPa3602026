using Newtonsoft.Json;
using RestSharp;
using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.CRM
{
    public partial class frmProdPrice : System.Web.UI.Page
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
                        if (FormRights.bView == false) //if (objDALUserRights.bView == false)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }

                        objBindDDL.FillBrand(ddlMake, 0);
                        objBindDDL.FillLists(ddlRAM, "RAM");
                        objBindDDL.FillLists(ddlROM, "ROM");
                        objBindDDL.FillLists(ddlGrade, "BG");
                        objBindDDL.FillLists(ddlColor, "CL");

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

        protected void ddlMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (ddlMake.SelectedIndex > 0)
                    {
                        objBindDDL.FillModel(ddlModel, ddlMake.SelectedValue);
                    }
                    else
                    {
                        ddlModel.DataSource = string.Empty;
                        ddlModel.DataBind();
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

        protected void lnkSerchItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    string itemcode = string.Empty;
                    string make = string.Empty;
                    string model = string.Empty;
                    string ram = string.Empty;
                    string rom = string.Empty;
                    string color = string.Empty;
                    string grade = string.Empty;
                    if (txtSearchItemDesc.Text != "" && txtSearchItemDesc.Text != string.Empty && txtSearchItemDesc.Text != null)
                    {
                        if (txtSearchItemDesc.Text.ToString().Contains("-"))
                        {

                            itemcode = txtSearchItemDesc.Text.Split('-')[1].Trim().ToString();

                        }
                        else
                        {
                            itemcode = txtSearchItemDesc.Text;
                        }

                    }
                    else
                    {
                        make = ddlMake.SelectedItem.Text;
                        model = ddlModel.SelectedItem.Text;
                        ram = ddlRAM.SelectedItem.Text == "NA" ? "" : ddlRAM.SelectedItem.Text + "GB ";
                        rom = ddlROM.SelectedItem.Text + "GB ";
                        color = ddlColor.SelectedItem.Text;
                        grade = ddlGrade.SelectedItem.Text;

                        itemcode = make + " " + model + " " + ram + "" + rom + "" + color + " MOBILE DEVICE (USED) (" + grade + ")";
                    }

                    if (itemcode != "" && itemcode != null && itemcode != string.Empty)
                    {
                        DataTable dt = new DataTable();
                        DataTable dtPrice = new DataTable();
                        dt = objMainClass.GetItemDetails(objMainClass.intCmpId, itemcode, "ITEMDESCSEARCH");
                        if (dt.Rows.Count > 0)
                        {
                            if (Convert.ToString(dt.Rows[0]["WEBSITE"]) != null && Convert.ToString(dt.Rows[0]["WEBSITE"]) != string.Empty && Convert.ToString(dt.Rows[0]["WEBSITE"]) != "")
                            {
                                ProductPrice objProductPrice = new ProductPrice();
                                objProductPrice = GetPtice(Convert.ToString(dt.Rows[0]["WEBSITE"]));
                                if (objProductPrice.name != null && objProductPrice.name != string.Empty && objProductPrice.name != "")
                                {
                                    lblItemCode.Text = objProductPrice.sku;
                                    lblItemDesc.Text = objProductPrice.name;
                                    lblRegularPrice.Text = objProductPrice.regular_price;
                                    lblSalePrice.Text = objProductPrice.sale_price;
                                    lblStockStatus.Text = objProductPrice.stock_status;
                                    lblAvailStock.Text = Convert.ToString(objProductPrice.stock_quantity);
                                    //gvList.DataSource = dt;
                                    //gvList.DataBind();
                                    GetJobDetails(objMainClass.intCmpId, objProductPrice.sku, (int)JOBSTATUS.PhyDocVar, (int)STAGE.VerifyDocs, "OPENJOBID");
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Price Not Found.!');", true);
                                }

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Website Product ID Not Found.!');", true);
                            }

                        }
                        else
                        {
                            if (make.ToUpper() == "APPLE")
                            {
                                itemcode = make + " " + model + " 0GB " + rom + "" + color + " MOBILE DEVICE (USED) (" + grade + ")";
                                dt = objMainClass.GetItemDetails(objMainClass.intCmpId, itemcode, "ITEMSEARCH");
                                if (dt.Rows.Count > 0)
                                {
                                    if (Convert.ToString(dt.Rows[0]["WEBSITE"]) != null && Convert.ToString(dt.Rows[0]["WEBSITE"]) != string.Empty && Convert.ToString(dt.Rows[0]["WEBSITE"]) != "")
                                    {
                                        ProductPrice objProductPrice = new ProductPrice();
                                        objProductPrice = GetPtice(Convert.ToString(dt.Rows[0]["WEBSITE"]));
                                        if (dtPrice.Rows.Count > 0)
                                        {
                                            lblItemCode.Text = objProductPrice.sku;
                                            lblItemDesc.Text = objProductPrice.name;
                                            lblRegularPrice.Text = objProductPrice.regular_price;
                                            lblSalePrice.Text = objProductPrice.sale_price;
                                            lblStockStatus.Text = objProductPrice.stock_status;
                                            lblAvailStock.Text = Convert.ToString(objProductPrice.stock_quantity);
                                            //gvList.DataSource = dt;
                                            //gvList.DataBind();
                                            GetJobDetails(objMainClass.intCmpId, objProductPrice.sku, (int)JOBSTATUS.PhyDocVar, (int)STAGE.VerifyDocs, "OPENJOBID");
                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Price Not Found.!');", true);
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Website Product ID Not Found.!');", true);
                                    }
                                }
                                else
                                {
                                    itemcode = make + " " + model + " NA " + rom + "" + color + " MOBILE DEVICE (USED) (" + grade + ")";
                                    dt = objMainClass.GetItemDetails(objMainClass.intCmpId, itemcode, "ITEMSEARCH");
                                    if (dt.Rows.Count > 0)
                                    {
                                        if (Convert.ToString(dt.Rows[0]["WEBSITE"]) != null && Convert.ToString(dt.Rows[0]["WEBSITE"]) != string.Empty && Convert.ToString(dt.Rows[0]["WEBSITE"]) != "")
                                        {
                                            ProductPrice objProductPrice = new ProductPrice();
                                            objProductPrice = GetPtice(Convert.ToString(dt.Rows[0]["WEBSITE"]));
                                            if (dtPrice.Rows.Count > 0)
                                            {
                                                lblItemCode.Text = objProductPrice.sku;
                                                lblItemDesc.Text = objProductPrice.name;
                                                lblRegularPrice.Text = objProductPrice.regular_price;
                                                lblSalePrice.Text = objProductPrice.sale_price;
                                                lblStockStatus.Text = objProductPrice.stock_status;
                                                lblAvailStock.Text = Convert.ToString(objProductPrice.stock_quantity);
                                                //gvList.DataSource = dt;
                                                //gvList.DataBind();
                                                GetJobDetails(objMainClass.intCmpId, objProductPrice.sku, (int)JOBSTATUS.PhyDocVar, (int)STAGE.VerifyDocs, "OPENJOBID");
                                            }
                                            else
                                            {
                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Price Not Found.!');", true);
                                            }
                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Website Product ID Not Found.!');", true);
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Item Not Found.!');", true);
                                    }
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Item Not Found.!');", true);
                            }

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('" + itemcode + " Item Desc. is invalid.!');", true);
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

        public void GetJobDetails(int CMPID, string ITEMCODE, int JOBSTATUS, int STAGEID, string ACTION)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetOpenJobId(CMPID, ITEMCODE, JOBSTATUS, STAGEID, ACTION);

                    if (dt.Rows.Count > 0)
                    {
                        gvList.DataSource = dt;
                        gvList.DataBind();
                    }
                    else
                    {
                        gvList.DataSource = string.Empty;
                        gvList.DataBind();
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

        public ProductPrice GetPtice(string PRODID)
        {
            ProductPrice objProductPrice = new ProductPrice();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            DataTable dt = new DataTable();

            var client = new RestClient(("https://mobex.in/wp-json/wc/v3/products/" + PRODID));
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes("ck_a93f292017103d66f94c4ca53e84f1335820f9a1:cs_890bc0545a5bf1d530ff3cb1f5ee4f738fa7d177");
            string val = System.Convert.ToBase64String(plainTextBytes);
            request.AddHeader("Authorization", "Basic Y2tfYTkzZjI5MjAxNzEwM2Q2NmY5NGM0Y2E1M2U4NGYxMzM1ODIwZjlhMTpjc184OTBiYzA1NDVhNWJmMWQ1MzBmZjNjYjFmNWVlNGY3MzhmYTdkMTc3");
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string jsonconn = response.Content;
                //jsonconn = "[" + jsonconn + "]";
                objProductPrice = JsonConvert.DeserializeObject<ProductPrice>(jsonconn);


                //objProductPrice = JsonConvert.DeserializeObject(jsonconn, (typeof(ProductPrice)));
            }
            return objProductPrice;
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetItemCode(string prefixText, int count)
        {
            List<string> ItemCode = new List<string>();

            MainClass objMainClass = new MainClass();
            ItemCode = objMainClass.GetItemDetailsList(objMainClass.intCmpId, prefixText, "ITEMDESCSEARCH");  //objMainClass.GetItemData(prefixText);

            return ItemCode;

        }

        protected void txtSearchItemDesc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (txtSearchItemDesc.Text != "" && txtSearchItemDesc.Text != string.Empty && txtSearchItemDesc.Text != null)
                    {
                        rfvMake.Enabled = false;
                        rfvModel.Enabled = false;
                        rfvRAM.Enabled = false;
                        rfvROM.Enabled = false;
                        rfvColor.Enabled = false;
                        rfvGrade.Enabled = false;
                    }
                    else
                    {
                        rfvMake.Enabled = true;
                        rfvModel.Enabled = true;
                        rfvRAM.Enabled = true;
                        rfvROM.Enabled = true;
                        rfvColor.Enabled = true;
                        rfvGrade.Enabled = true;
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

        protected void lnkEdit_Click(object sender, EventArgs e)
        {

        }
    }
}
