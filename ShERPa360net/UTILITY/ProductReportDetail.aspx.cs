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
    public partial class ProductReportDetail : System.Web.UI.Page
    {
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        bool Userright = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtFromDate.Text = (objMainClass.indianTime.Date.AddDays(-30)).ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    txtToDate.Text   = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    if (Session["USERID"] != null)
                    {
                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menutabCheckerid.Value, "");
                        Userright = FormRights.bView;

                        if (!Userright)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        BindPageDropDown();
                        //BindProductDetail();
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
                string attachment = "attachment; filename=ProductStatuswise.xls";
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

        protected void ddlMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindMakeorModelAssociateDetail("Model");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        #region PAGEMETHOD
        public void BindMakeorModelAssociateDetail(string reqdropdown)
        {
            try
            {
                if (reqdropdown == "Model")
                {
                    if (ddlMake.SelectedValue != "0")
                    {
                        ddlModel.Items.Clear();
                        objBindDDL.FillMobexSellerSearchModel(ddlModel, Convert.ToInt32(ddlMake.SelectedValue));
                    }
                    else
                    {
                        ddlModel.Items.Clear();
                        ddlModel.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                        ddlModel.SelectedValue = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
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
        public void BindProductDetail()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    gvProduct.DataSource = null;
                    gvProduct.DataBind();
                    DataTable dt = new DataTable();
                    dt           = objMainClass.GetProductReportDetail(txtFromDate.Text, txtToDate.Text,Convert.ToInt32(ddlStatus.SelectedValue)
                        ,ddlActualDays.SelectedValue,ddlRate.SelectedValue,ddlGrade.SelectedValue, Convert.ToInt32(Session["USERID"]),ddlMake.SelectedItem.Text,
                        (ddlModel.SelectedItem == null ? string.Empty : ddlModel.SelectedItem.Text));
                    if(dt.Rows.Count > 0)
                    {
                        gvProduct.DataSource = dt;
                        gvProduct.DataBind();
                        gvProduct.HeaderRow.TableSection = TableRowSection.TableHeader;
                        lgrecordcount.InnerText          = "Records : " + dt.Rows.Count.ToString();
                    }
                    else
                    {
                        gvProduct.DataSource = dt;
                        gvProduct.DataBind();
                        gvProduct.HeaderRow.TableSection = TableRowSection.TableHeader;
                        lgrecordcount.InnerText = "Records : " + "0";
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

        public void ResetFormControl()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    txtFromDate.Text = (objMainClass.indianTime.Date.AddDays(-30)).ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    ddlStatus.SelectedValue = "0";
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

        protected void gvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (Convert.ToInt32((e.Row.FindControl("hdActualoldDays") as HiddenField).Value) > 10)
                    {
                        (e.Row.FindControl("lblID") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblDate") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblMake") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblModel") as Label).ForeColor = System.Drawing.Color.Red;

                        (e.Row.FindControl("lblRam") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblRom") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblColor") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblYear") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblOrignalKit") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblIMEINo") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblVendorName") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblVendorGrade") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblMobexGrade") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblVendorStock") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblVendorRate") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblNewRate") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblMobexPurchaseRate") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblMobexPurchasePer") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblStatus") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblListedFK") as Label).ForeColor = System.Drawing.Color.Red;

                        (e.Row.FindControl("lblFKAmt") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblFKPer") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblListedAmz") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblAmzAmt") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblAmzPer") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblListedWeb") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblWebAmt") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblWebPer") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblEntryBy") as Label).ForeColor = System.Drawing.Color.Red;

                        (e.Row.FindControl("lblRejectReason") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblASMName") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblAreaName") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblUnlistedDate") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblUnlistedBy") as Label).ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        (e.Row.FindControl("lblID") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblDate") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblMake") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblModel") as Label).ForeColor = System.Drawing.Color.Black;

                        (e.Row.FindControl("lblRam") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblRom") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblColor") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblYear") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblOrignalKit") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblIMEINo") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblVendorName") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblVendorGrade") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblMobexGrade") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblVendorStock") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblVendorRate") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblNewRate") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblMobexPurchaseRate") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblMobexPurchasePer") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblStatus") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblListedFK") as Label).ForeColor = System.Drawing.Color.Black;

                        (e.Row.FindControl("lblFKAmt") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblFKPer") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblListedAmz") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblAmzAmt") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblAmzPer") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblListedWeb") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblWebAmt") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblWebPer") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblEntryBy") as Label).ForeColor = System.Drawing.Color.Black;

                        (e.Row.FindControl("lblRejectReason") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblASMName") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblAreaName") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblUnlistedDate") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblUnlistedBy") as Label).ForeColor = System.Drawing.Color.Black;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#mod-al-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}