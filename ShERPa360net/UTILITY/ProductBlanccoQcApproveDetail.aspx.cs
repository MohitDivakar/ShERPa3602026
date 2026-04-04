using Newtonsoft.Json;
using OnBarcode.Barcode.BarcodeScanner;
using ShERPa360net.Class;
using ShERPa360net.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.UTILITY
{
    public partial class ProductBlanccoQcApproveDetail : System.Web.UI.Page
    {
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        bool Checkerright = false;
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
                        Checkerright = FormRights.bView;

                        if (!Checkerright)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        BindPageDropDown();
                        //BindProductDetail();
                        //if (gvProduct.Rows.Count > 0)
                        //{
                        //    gvProduct.HeaderRow.TableSection = TableRowSection.TableHeader;
                        //}
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                    }
                }
                else
                {
                    BindProductDetail();
                    //if (gvProduct.Rows.Count > 0)
                    //{
                    //    gvProduct.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //}
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
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
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
                string filename = "Listed Product Report" + txtFromDate.Text + "-" + txtToDate.Text;
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
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }


        public void BindDelaerAssociateVendor()
        {
            try
            {
                if (Session["USERID"] != null)
                {
 
                    int dealerid = 0;
                    int.TryParse(hddelaerid.Value, out dealerid);
                    objBindDDL.FillMobexDealerAssociateVendor(ddlDealerVendor, dealerid);
                    ddlDealerVendor.SelectedValue = "0";
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

        public void BindProductDetail()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    int status;
                    status = Convert.ToInt32(PRODUCTSTATUS.LISTEDRESERVED);
                    gvProduct.DataSource = null;
                    gvProduct.DataBind();
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetProductEntryDetail(txtFromDate.Text, txtToDate.Text, (ddlVendor.SelectedValue == "0" ? "" : ddlVendor.SelectedItem.Text), status, Convert.ToInt32(Session["USERID"]));
                    gvProduct.DataSource = dt;
                    gvProduct.DataBind();
                    if (gvProduct.Rows.Count > 0)
                    {
                        gvProduct.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    lgrecordcount.InnerText = "Records : " + dt.Rows.Count.ToString();
                    //if (FormRights.bAdd == false)
                    //{
                    //    gvProduct.Columns[0].Visible = false;
                    //}
                    //else
                    //{
                    //    gvProduct.Columns[0].Visible = true;
                    //}
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

        public void ResetFormControl()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    txtFromDate.Text = (objMainClass.indianTime.Date.AddDays(-30)).ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    ddlVendor.SelectedValue = "0";
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


        #endregion

        protected void btnQc_Click(object sender, EventArgs e)
        {
            try
            {
                ResetQcDetail();
                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                hdprimarykey.Value = ((HiddenField)grdrow.FindControl("hdID")).Value;
                hddelaerid.Value = ((HiddenField)grdrow.FindControl("hdVendorId")).Value;
                hdlisttype.Value = ((HiddenField)grdrow.FindControl("hdListingType")).Value;
                txtIMEINo.Text = ((Label)grdrow.FindControl("lblIMEINo")).Text;
                txtPurchaseRate.Text = ((Label)grdrow.FindControl("lblMobexRate")).Text;
                BindDelaerAssociateVendor();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                ddlQcResult.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
        protected void gvDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "eEdit")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow grdrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    hddelaerid.Value = ((HiddenField)grdrow.FindControl("hdVendorId")).Value;
                    txtIMEINo.Text = ((Label)grdrow.FindControl("lblIMEINo")).Text;
                    txtPurchaseRate.Text = ((Label)grdrow.FindControl("lblMobexRate")).Text;
                    BindDelaerAssociateVendor();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                    ddlQcResult.Focus();
                }
              
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
        protected void btnSaveQc_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = Convert.ToInt32(hdprimarykey.Value);
                int vendordelaerid = Convert.ToInt32(hddelaerid.Value == "" ? "0" : hddelaerid.Value);
                byte[] IMAGE = null;
                byte[] INVOICE = null;

                int Status = 0;
                string rejecreason = "";
                if (ddlBlanccoQcResult.SelectedValue == "NOTAVAILABLE")
                {
                    Status = 11894;
                }
                else if (ddlBlanccoQcResult.SelectedValue == "NOTAVAILABLE")
                {
                    Status = 11894;
                }
                else if (ddlQcResult.SelectedValue == "PASS" && ddlBlanccoQcResult.SelectedValue == "PASS")
                {
                    Status = 11301;
                }
                else
                {
                    if (ddlBlanccoQcResult.SelectedValue != "PASS")
                    {
                        rejecreason = "Blancco Fail";
                    }
                    else
                    {
                        rejecreason = "QC Fail";
                    }
                    Status = 11233;
                }

                if (fuImeiImage.HasFiles)
                {
                    using (BinaryReader br = new BinaryReader(fuImeiImage.PostedFile.InputStream))
                    {
                        IMAGE = br.ReadBytes(fuImeiImage.PostedFile.ContentLength);
                    }
                }


                //Manan 02-02-2023
                if (fuinvoiceimage.HasFiles)
                {
                    using (BinaryReader br = new BinaryReader(fuinvoiceimage.PostedFile.InputStream))
                    {
                        INVOICE = br.ReadBytes(fuinvoiceimage.PostedFile.ContentLength);
                    }
                }

                Decimal purchaserate = 0;
                Decimal.TryParse(txtPurchaseRate.Text, out purchaserate);
                objMainClass.UpdateBlanccoQcDetail(ID, ddlBlanccoQcResult.SelectedValue, txtBlanccoReason.Text,
                ddlQcResult.SelectedValue, txtReason.Text, txtIMEINo.Text, Session["USERID"].ToString(),
                ddlBlanccoGrade.SelectedValue, purchaserate, Status, rejecreason
                , ddlDealerVendor.SelectedValue, IMAGE,INVOICE);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Blancco Qc ID:" + ID + " updated Successfully." + "\");", true);
                BindProductDetail();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnResetQc_Click(object sender, EventArgs e)
        {
            try
            {
                ResetQcDetail();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public void ResetQcDetail()
        {
            try
            {
                ddlBlanccoQcResult.SelectedIndex = -1;
                txtBlanccoReason.Text = string.Empty;
                ddlQcResult.SelectedIndex = -1;
                txtReason.Text = string.Empty;
                txtIMEINo.Text = string.Empty;
                hdprimarykey.Value = "0";
                hddelaerid.Value = "0";
                hdlisttype.Value = "";
                ddlDealerVendor.DataSource = null;
                ddlDealerVendor.DataBind();
                fuSerialNo.Attributes.Clear();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void gvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnUnlist_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                int ID = Convert.ToInt32(((HiddenField)grdrow.FindControl("hdID")).Value);
                objMainClass.UpdateUnListedDetail(ID, Session["USERID"].ToString());
                var mailmessagefor = "Hi There,<br><br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ID is : " + ID.ToString() + " Product : Make-" + ((Label)grdrow.FindControl("lblMake")).Text + " Model-" + ((Label)grdrow.FindControl("lblModel")).Text + " Ram-" + ((Label)grdrow.FindControl("lblRam")).Text + " Rom-" + ((Label)grdrow.FindControl("lblRom")).Text + " Color-" + ((Label)grdrow.FindControl("lblColor")).Text + " Unlisted From " + ((Label)grdrow.FindControl("lblVendorName")).Text + " Vendor and Unlisted By " + Session["USERNAME"].ToString() + " <br>Please do needfull.<br><br>Regard,<br>Mobex Seller System<br><br><br>";
                EmailSend.EmailSent(mailmessagefor, "Product Unlisted From Mobex Seller System", "care@mobex.in", "mobex@123", "dispatch@qarmatek.com");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Product UnListed Successfully." + "\");", true);
                BindProductDetail();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnUploadScan_Click(object sender, EventArgs e)
        {
            if (IsPostBack && fuSerialNo.PostedFile != null)
            {
                //lblmessage.Visible = false;
                //btnSave.Enabled = true;
                if (fuSerialNo.PostedFile.FileName.Length > 0)
                {
                    string filename = Path.GetFileName(fuSerialNo.PostedFile.FileName);
                    string path     = Server.MapPath(fuSerialNo.PostedFile.FileName);
                    fuSerialNo.SaveAs(path);
                    string[] data = BarcodeScanner.Scan(path, BarcodeType.Code128);
                    //string[] data = Spire.Barcode.BarcodeScanner.Scan(path, Spire.Barcode.BarCodeType.Code128);
                    if (data != null &&  data.Count() > 0)
                    {
                        txtIMEINo.Text = Convert.ToString(data[0]);
                        //txtJDARefNo.Text = Convert.ToString(data[0]);
                        File.Delete(path);
                        //txtSerialNo_TextChanged(1, e);
                        //lnkNext0.Visible = true;
                    }
                    else
                    {
                        File.Delete(path);
                        txtIMEINo.Text = string.Empty;
                        //lblmessage.Text = "Image Not Scanned Properly!";
                        //lblmessage.Visible = true;
                        //lnkNext0.Visible = false;
                    }
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                }
                else
                {
                    txtIMEINo.Text = String.Empty;
                }
            }
        }

        protected void gvProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        [WebMethod]
        public static string GetIMEINo(string imeino, string LISTINGTYPE,string ID)
        {
            string status = "false";
            try
            {
                MainClass objMain = new MainClass();
                var detail = objMain.GetExistsImeiNo(imeino, LISTINGTYPE, "GETBLANCCOEXISTSIMEINO", Convert.ToInt32(ID));
                if (detail != null && detail.Rows != null && detail.Rows.Count > 0)
                {
                    status = "true";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonConvert.SerializeObject(status);
        }
    }
}