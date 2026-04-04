using Newtonsoft.Json;
using ShERPa360net.Class;
using ShERPa360net.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.UTILITY
{
    public partial class ReadyForListingDetail : System.Web.UI.Page
    {
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        bool Listingright = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtFromDate.Text = (objMainClass.indianTime.Date.AddDays(-30)).ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    if (Session["USERID"] != null)
                    {
                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menutabListingid.Value, "");
                        Listingright = FormRights.bView;
                        if (!Listingright)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        dvlisted.Visible = true;
                        BindPageDropDown();
                        BindProductDetail();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lnkSearh_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    BindProductDetail();
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

        protected void imgReset_Click(object sender, EventArgs e)
        {
            try
            {
                ResetFormControl();
                BindProductDetail();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lnkExport_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = "Listed Product Detail" + txtFromDate.Text + "-" + txtToDate.Text;
                string attachment = "attachment; filename=" + filename + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vdn.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gvProduct.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public void ShowHideColumn()
        {
            try
            {
                // APPROVED PRODUCT
                //status == 11235
   
                    gvProduct.Columns[0].Visible = false;
                    gvProduct.Columns[1].Visible = false;

                    gvProduct.Columns[2].Visible = true;

                    gvProduct.Columns[3].Visible = true;
                    gvProduct.Columns[4].Visible = true;
                    gvProduct.Columns[6].Visible = true;

                    //Make Model Detail
                    gvProduct.Columns[7].Visible = true;
                    gvProduct.Columns[8].Visible = true;
                    gvProduct.Columns[9].Visible = true;
                    gvProduct.Columns[10].Visible = true;
                    gvProduct.Columns[11].Visible = true;
                    gvProduct.Columns[12].Visible = true;

                    //VendorName,Grade,EntryDate 
                    gvProduct.Columns[13].Visible = true;
                    gvProduct.Columns[15].Visible = true;
                    gvProduct.Columns[16].Visible = true;
                    stProductDetail.InnerHtml = "<span class=\"fa fa-file\"></span>&nbsp;For Product Listing(LISTED) Action";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }


        #region PAGEMETHOD
        public void BindPageDropDown()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    objBindDDL.FillMobexSellerVendor(ddlVendor);
                    ddlVendor.SelectedValue = "0";
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

        public void BindProductDetail()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    int status = 0;
                    status = Convert.ToInt32(PRODUCTSTATUS.APPROVED);

                    gvProduct.DataSource = null;
                    gvProduct.DataBind();
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetProductEntryDetail(txtFromDate.Text, txtToDate.Text, (ddlVendor.SelectedValue == "0" ? "" : ddlVendor.SelectedItem.Text), status, 0);
                    gvProduct.DataSource = dt;
                    gvProduct.DataBind();
                    lgrecordcount.InnerText = "Records : " + dt.Rows.Count.ToString();
                    //ShowHideColumn();
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

        public void ResetFormControl()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    txtFromDate.Text        = (objMainClass.indianTime.Date.AddDays(-30)).ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    txtToDate.Text          = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    ddlVendor.SelectedValue = "0";
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

        public override void VerifyRenderingInServerForm(Control control)
        {

        }


        #endregion

        protected void btnListed_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                int ID = Convert.ToInt32(((HiddenField)grdrow.FindControl("hdID")).Value);
                objMainClass.UpdateListedDetail(ID);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Product Listed Successfully." + "\");", true);
                BindProductDetail();
            }
            catch (Exception ex)
            {
                throw ex;
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

        public void CheckedAll()
        {
            try
            {
                if(chkSelectAll.Checked == true)
                {
                    for (int i = 0; i < gvProduct.Rows.Count; i++)
                    {
                        GridViewRow row = gvProduct.Rows[i];
                        ((CheckBox)row.FindControl("chkSelection")).Checked = true;
                    }
                }
                else
                {
                    for (int i = 0; i < gvProduct.Rows.Count; i++)
                    {
                        GridViewRow row = gvProduct.Rows[i];
                        ((CheckBox)row.FindControl("chkSelection")).Checked = false;
                    }
                }
                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }


        public int  SelectedCheckBox()
        {
            int selectedcheckbox = 0;
            try
            {
                for (int i = 0; i < gvProduct.Rows.Count; i++)
                {
                    GridViewRow row = gvProduct.Rows[i];
                    if (((CheckBox)row.FindControl("chkSelection")).Checked == true)
                    {
                        selectedcheckbox = selectedcheckbox + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }

            return selectedcheckbox;
        }

        protected void btnSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedchecbox = SelectedCheckBox();
                if (selectedchecbox > 0)
                {
                    string productjson = GetProductJson();
                    int result = objMainClass.ProductBulkListed(productjson,Convert.ToInt32(Session["USERID"].ToString()));
                    if (result > 0)
                    {
                        //DownloadListedProductDetail();
                        BindProductDetail();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Product Listed Successfully." + "\");", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Product not Listed due to some issue.');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please select at least one checkbox to Listed.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public string GetProductJson()
        {
            string productjson = "";
            List<ProductJson> objlstProductJson = new List<ProductJson>();
            try
            {
                for (int i = 0; i < gvProduct.Rows.Count; i++)
                {
                    GridViewRow row = gvProduct.Rows[i];
                    if (((CheckBox)row.FindControl("chkSelection")).Checked == true)
                    {
                        ProductJson objPrud = new ProductJson();
                        objPrud.ID = Convert.ToInt32(((HiddenField)row.FindControl("hdID")).Value);
                        objPrud.STATUS = Convert.ToInt32(PRODUCTSTATUS.LISTED);
                        objlstProductJson.Add(objPrud);
                    }
                }
                productjson = JsonConvert.SerializeObject(objlstProductJson);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return productjson;
        }

        public void DownloadListedProductDetail()
        {
            try
            {
                TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                string filename = "Listed Product Detail" + "-" + "DateTime-" + indianTime.ToString();
                string attachment = "attachment; filename=" + filename + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vdn.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gvProduct.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
               // HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}