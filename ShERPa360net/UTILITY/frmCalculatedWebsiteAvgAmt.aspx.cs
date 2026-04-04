using Newtonsoft.Json;
using RestSharp;
using ShERPa360net.Class;
using ShERPa360net.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.UTILITY
{
    public partial class frmCalculatedWebsiteAvgAmt : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ITEMCODE"] = string.Empty;
            Session["ITEMCODE"] = null;

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
                        BindPageDropDown();

                        if (FormRights.bAdd == false)
                        {
                            lnkUpdateSelectedAllStock.Enabled = false;
                        }
                        //GetTotalUnMappedItemCount();
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
            else
            {
                if(gvList.Rows.Count > 0)
                {
                    gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        public void BindPageDropDown()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    objBindDDL.FillMobexSellerBrand(ddlMake);
                    ddlMake.SelectedValue = "0";
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
        public int GetTotalUnMappedItemCount()
        {
            int totalunmappeditem = 0;
            try
            {
                DataTable dt = new DataTable();
                dt = objMainClass.GetUnMappedItemDetail(Convert.ToInt32(Session["USERID"]), "TOTALCOUNT");
                if (dt.Rows.Count > 0)
                {
                    lnkTotalUnmappedItem.Visible    = true;
                    lnkTotalUnmappedItem.Text       = "Total Unmapped " + dt.Rows.Count.ToString();
                }
                else
                {
                    lnkTotalUnmappedItem.Visible    = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message.Replace("\r\n", "") + "\");", true);
            }
            return totalunmappeditem;
        }
        public void FillData()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetCalculatedWebsiteAvgAmt(Convert.ToInt32(Session["USERID"]), ddlMake.SelectedItem.Text, ddlSearchOperator.SelectedValue);
                    if (dt.Rows.Count > 0)
                    {
                        gvList.DataSource = dt;
                        gvList.DataBind();
                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
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

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message.Replace("\r\n", "") + "\");", true);
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }
        protected void lnkExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GetTotalUnMappedItemCount();
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetCalculatedWebsiteAvgAmt(Convert.ToInt32(Session["USERID"]),ddlMake.SelectedItem.Text,ddlSearchOperator.SelectedValue);
                    if (dt.Columns.Count > 0)
                    {
                        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                        string attachment = "attachment; filename=CalculateWebsiteAvgAmt-" + "-DateTime-" + indianTime + ".xls";
                        Context.Response.ClearContent();
                        Context.Response.AddHeader("content-disposition", attachment);
                        Context.Response.ContentType = "application/vnd.ms-excel";
                        int headeri = 1;
                        int bodyi = 1;
                        foreach (DataColumn dc in dt.Columns)
                        {
                            //if(headeri == 1)
                            //{
                            //    Context.Response.Write(dc.ColumnName);
                            //    headeri = headeri + 1;
                            //}
                            //else
                            //{
                            Context.Response.Write("\t" + dc.ColumnName);
                            //}
                        }
                        Context.Response.Write("\n");
                        int i;
                        foreach (DataRow dr in dt.Rows)
                        {
                            for (i = 0; i < dt.Columns.Count; i++)
                            {
                                //if (bodyi == 1)
                                //{
                                //    Context.Response.Write(dr[i].ToString());
                                //    bodyi = bodyi + 1;
                                //}
                                //else
                                //{
                                Context.Response.Write("\t" + dr[i].ToString());
                                //}
                            }
                            bodyi = 1;
                            Context.Response.Write("\n");
                        }
                        Context.Response.Flush();
                        Context.Response.Close();
                        Context.Response.End();
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
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
        protected void lnkSerchCalcWebAmt_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GetTotalUnMappedItemCount();
                    FillData();
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
        protected void btnUpdateStock_Click(object sender, EventArgs e)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                if (Session["USERID"] != null)
                {
                    ProductPriceUpdate objProductPriceUpdate = new ProductPriceUpdate();
                    GridViewRow grdrow      = (GridViewRow)((LinkButton)sender).NamingContainer;
                    string  Website         = ((Label)grdrow.FindControl("lblWEBSITE")).Text;
                    string AverageAmt       = ((Label)grdrow.FindControl("lblAverageAmt")).Text;
                    string totalQty         = ((Label)grdrow.FindControl("lblTotalQty")).Text;

                    objProductPriceUpdate.sale_price            = AverageAmt;
                    objProductPriceUpdate.stock_quantity        = Convert.ToInt32(Math.Round(Convert.ToDecimal(totalQty),0));
                    var jsonobjProductPriceUpdate               = JsonConvert.SerializeObject(objProductPriceUpdate);
                    ProductPrice objProductPrice = new ProductPrice();
                    DataTable dt = new DataTable();
                    //var client = new RestClient(("https://mobex.in/wp-json/wc/v3/products/" + "31173"));
                    var client = new RestClient(("https://mobex.in/wp-json/wc/v3/products/" + Website));
                    client.Timeout = -1;
                    var request = new RestRequest(Method.PUT);
                    request.AddHeader("authorization", "Basic Y2tfNjM0N2UwODc1NTczYWI5ZTY1Njc1NzMyM2E1ZWY2ZjI5YjAwZTcyODpjc19mMGI1ODQ4MTBiYmZkNWYxNDA3OTk1MTUwNmFmMmU2MGVkNjBjM2Q5");
                    request.AddHeader("Content-Type", "application/json");
                    request.AddParameter("application/json", jsonobjProductPriceUpdate, ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string jsonconn = response.Content;
                        //jsonconn = "[" + jsonconn + "]";
                        //objProductPrice = JsonConvert.DeserializeObject<ProductPrice>(jsonconn);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Rate Update Successfully." + "\");", true);
                        //objProductPrice = JsonConvert.DeserializeObject(jsonconn, (typeof(ProductPrice)));
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
        protected void lnkTotalUnmappedItem_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("frmUnMappedItemMapping.aspx");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message.Replace("\r\n", "") + "\");", true);
            }
        }
        protected void lnkUpdateSelectedAllStock_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedchecbox = SelectedCheckBox();
                if (selectedchecbox > 0)
                {
                    List<WEBSITERATEQTYUPDATEHISTORYLOG> objlst = new List<WEBSITERATEQTYUPDATEHISTORYLOG>();
                    int totalrecord = 0, totalupdaterecord = 0;
                    if (gvList.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvList.Rows.Count; i++)
                        {
                            GridViewRow row = gvList.Rows[i];
                            if (((CheckBox)row.FindControl("chkSelection")).Checked == true)
                            {
                                if (((Label)row.FindControl("lblWEBSITE")).Text.Length > 0)
                                {
                                    ProductPriceUpdate objProductPriceUpdate = new ProductPriceUpdate();
                                    totalrecord = totalrecord + 1;
                                    string Website = ((Label)row.FindControl("lblWEBSITE")).Text;
                                    string AverageAmt = ((Label)row.FindControl("lblAverageAmt")).Text;
                                    string totalQty = ((Label)row.FindControl("lblTotalQty")).Text;
                                    //string itemcode = ((Label)row.FindControl("lblITEMCODE")).Text;
                                    string itemdesc = ((Label)row.FindControl("lblMake")).Text + " " + ((Label)row.FindControl("lblModel")).Text
                                                      + " "  + ((Label)row.FindControl("lblRam")).Text + " " + ((Label)row.FindControl("lblRom")).Text
                                                      + " " + ((Label)row.FindControl("lblColor")).Text;

                                    objProductPriceUpdate.sale_price = AverageAmt;
                                    objProductPriceUpdate.stock_quantity = Convert.ToInt32(Math.Round(Convert.ToDecimal(totalQty), 0));
                                    var jsonobjProductPriceUpdate = JsonConvert.SerializeObject(objProductPriceUpdate);
                                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                    //var client = new RestClient(("https://mobex.in/wp-json/wc/v3/products/" + "31173"));
                                    var client = new RestClient(("https://mobex.in/wp-json/wc/v3/products/" + Website));
                                    client.Timeout = -1;
                                    var request = new RestRequest(Method.PUT);
                                    request.AddHeader("Authorization", "Basic Y2tfNjM0N2UwODc1NTczYWI5ZTY1Njc1NzMyM2E1ZWY2ZjI5YjAwZTcyODpjc19mMGI1ODQ4MTBiYmZkNWYxNDA3OTk1MTUwNmFmMmU2MGVkNjBjM2Q5");
                                    request.AddHeader("Content-Type", "application/json");
                                    request.AddParameter("application/json", jsonobjProductPriceUpdate, ParameterType.RequestBody);
                                    IRestResponse response = client.Execute(request);
                                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                                    {
                                        WEBSITERATEQTYUPDATEHISTORYLOG objeachupdate = new WEBSITERATEQTYUPDATEHISTORYLOG();
                                        //objeachupdate.ITEMCODE = itemcode;
                                        objeachupdate.ITEMDESC = itemdesc;
                                        objeachupdate.WEBSITE = Website;
                                        objeachupdate.UPDATEDQTY = Convert.ToInt32(totalQty);
                                        objeachupdate.UPDATEDRATE = Convert.ToDecimal(AverageAmt);
                                        objlst.Add(objeachupdate);
                                        string jsonconn   = response.Content;
                                        totalupdaterecord = totalupdaterecord + 1;
                                    }
                                }
                            }
                        }
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Total Record :" + totalrecord + " Total Updated Record : " + totalupdaterecord + " Total Pending Record to Update : " + (totalrecord - totalupdaterecord) + " Update Successfully." + "\");", true);
                    }
                    string WEBSITERATEQTYUPDATEJSON = "";
                    WEBSITERATEQTYUPDATEJSON = JsonConvert.SerializeObject(objlst);
                    int result = objMainClass.BULKWEBSITERATEQTYUPDATEHISTORYLOG(WEBSITERATEQTYUPDATEJSON, Convert.ToInt32(Session["USERID"].ToString()));
                    if (result > 0)
                    {
                        FillData();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Total Record :" + totalrecord + " Total Updated Record : " + totalupdaterecord + " Total not Updated Record : " + (totalrecord - totalupdaterecord) + " Update Successfully." + "\");", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please select at least one checkbox to Update.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message.Replace("\r\n", "") + "\");", true);
            }
        }
        public int SelectedCheckBox()
        {
            int selectedcheckbox = 0;
            try
            {
                for (int i = 0; i < gvList.Rows.Count; i++)
                {
                    GridViewRow row = gvList.Rows[i];
                    if (((CheckBox)row.FindControl("chkSelection")).Checked == true)
                    {
                        if(((Label)row.FindControl("lblWEBSITE")).Text.Length > 0)
                        {
                            selectedcheckbox = selectedcheckbox + 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }

            return selectedcheckbox;
        }
        public void CheckedAll()
        {
            try
            {
                if (chkSelectAll.Checked == true)
                {
                    for (int i = 0; i < gvList.Rows.Count; i++)
                    {
                        GridViewRow row = gvList.Rows[i];
                        ((CheckBox)row.FindControl("chkSelection")).Checked = true;
                    }
                }
                else
                {
                    for (int i = 0; i < gvList.Rows.Count; i++)
                    {
                        GridViewRow row = gvList.Rows[i];
                        ((CheckBox)row.FindControl("chkSelection")).Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckedAll();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}