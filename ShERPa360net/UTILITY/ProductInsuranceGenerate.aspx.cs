using Newtonsoft.Json;
using org.w3c.dom.css;
using RestSharp;
using ShERPa360net.Class;
using ShERPa360net.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.UTILITY
{
    public partial class ProductInsuranceGenerate : System.Web.UI.Page
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
                    BindPageDropDown();
                    txtFromDate.Text = (objMainClass.indianTime.Date.AddDays(-30)).ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    txtToDate.Text   =  objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    if (Session["USERID"] != null)
                    {
                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menutabCheckerid.Value, "");
                        Checkerright = FormRights.bView;

                        if (!Checkerright)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                    }
                }

                if (gvProduct.Rows.Count > 0)
                {
                    gvProduct.HeaderRow.TableSection = TableRowSection.TableHeader;
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
                objBindDDL.FillLists(ddlstatus, "IAS");
                ddlstatus.SelectedValue = "12312";
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
                string filename = "Product Report" + txtFromDate.Text + "-" + txtToDate.Text;
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

        public void BindProductDetail()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    gvProduct.DataSource = null;
                    gvProduct.DataBind();
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetSalesInvoiceforGenerateInsurance(1,txtSINO.Text,Convert.ToInt32(ddlstatus.SelectedValue));
                    gvProduct.DataSource = dt;
                    gvProduct.DataBind();
                    //gvProduct.HeaderRow.TableSection = TableRowSection.TableHeader;
                    lgrecordcount.InnerText = "Records : " + dt.Rows.Count.ToString();
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
                    txtSINO.Text            = String.Empty;
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
        protected void btnGenerateInsurance_Click(object sender, EventArgs e)
        {
            try
            {
                ResetInsurance();
                GridViewRow grdrow  = (GridViewRow)((LinkButton)sender).NamingContainer;
                //Initiate the Control 
                hdSoNo.Value                = ((Label)grdrow.FindControl("lblSoNo")).Text;
                hdDeviceID.Value            = ((Label)grdrow.FindControl("lblDeviceID")).Text;
                hdPlantVariantID.Value      = ((Label)grdrow.FindControl("lblPlanID")).Text;
                hdSrNo.Value                = ((Label)grdrow.FindControl("lblSRNO")).Text;
                txtInvoiceNo.Text           = ((Label)grdrow.FindControl("lblInvoiceNo")).Text;
                txtInvoiceDate.Text         = ((Label)grdrow.FindControl("lblInvoiceDate")).Text;
                txtOrderNo.Text             = ((Label)grdrow.FindControl("lblOrderNo")).Text;
                txtFirstName.Text           = ((Label)grdrow.FindControl("lblfirstname")).Text;
                txtLastName.Text            = ((Label)grdrow.FindControl("lbllastname")).Text;
                txtAddress1.Text            = ((Label)grdrow.FindControl("lblAddress1")).Text;
                txtAddress2.Text            = ((Label)grdrow.FindControl("lblAddress2")).Text;
                txtAddress3.Text            = ((Label)grdrow.FindControl("lblAddress3")).Text;
                txtCity.Text                = ((Label)grdrow.FindControl("lblCity")).Text;
                txtState.Text               = ((Label)grdrow.FindControl("lblState")).Text;
                txtpincode.Text             = ((Label)grdrow.FindControl("lblPincode")).Text;
                txtMobileNumber.Text        = ((Label)grdrow.FindControl("lblMobieNumber")).Text;
                txtEmail.Text               = ((Label)grdrow.FindControl("lblEmail")).Text;
                txtInsurancePlan.Text       = ((Label)grdrow.FindControl("lblItemDetail")).Text;
                DataTable dt                = new DataTable();
                dt                          = objMainClass.GetSalesInvoiceEachDetail(1, hdSoNo.Value);

                if(dt.Rows.Count > 0)
                {
                    txtMake.Text            = dt.Rows[0]["PRODMAKE"].ToString();
                    txtModel.Text           = dt.Rows[0]["PRODMODEL"].ToString();
                    txtRam.Text             = dt.Rows[0]["RAMSIZE"].ToString();
                    txtRom.Text             = dt.Rows[0]["ROMSIZE"].ToString();
                    txtColor.Text           = dt.Rows[0]["COLOR"].ToString();
                    txtIMEINo.Text          = dt.Rows[0]["IMEINO"].ToString();
                    txtPrice.Text           = dt.Rows[0]["CAMOUNT"].ToString();
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                txtInvoiceNo.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }


        protected void btnResetInsurance_Click(object sender, EventArgs e)
        {

        }


        public void ResetInsurance()
        {
            try
            {
                txtInvoiceNo.Text       = string.Empty;
                txtInvoiceDate.Text     = string.Empty;
                txtOrderNo.Text         = string.Empty;
                txtFirstName.Text       = string.Empty;
                txtLastName.Text        = string.Empty;
                txtAddress1.Text        = string.Empty;
                txtAddress2.Text        = string.Empty;
                txtAddress3.Text        = string.Empty;
                txtCity.Text            = string.Empty;
                txtState.Text           = string.Empty;
                txtpincode.Text         = string.Empty;
                txtMobileNumber.Text    = string.Empty;
                txtEmail.Text           = string.Empty;
                txtMake.Text            = string.Empty;
                txtModel.Text           = string.Empty;
                txtRam.Text             = string.Empty;
                txtRom.Text             = string.Empty;
                txtColor.Text           = string.Empty;
                txtIMEINo.Text          = string.Empty;
                txtPrice.Text           = string.Empty;
                txtInsurancePlan.Text   = string.Empty;
                hdDeviceID.Value        = "0";
                hdPlantVariantID.Value  = "0";
                hdSrNo.Value            = "0";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnClaimInsurance_Click(object sender, EventArgs e)
        {
            try
            {
                GenerateInsurance();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }


        public bool GenerateInsurance()
        {
            bool IsgenInsurance = false;
            try
            {
                var objDetail =  AWSFileUpload();
                Decimal purchaseprice                 = 0;
                int purchasevalue                     = 0;
                Decimal.TryParse(txtPrice.Text, out purchaseprice);
                purchasevalue                         = (int)(purchaseprice);
                AcrossAssistanceRequestModel objModel = new AcrossAssistanceRequestModel();
                objModel.planVariantId                = Convert.ToInt32(hdPlantVariantID.Value);
                objModel.partnerRefId                 = txtOrderNo.Text;
                objModel.firstName                    = txtFirstName.Text;
                objModel.lastName                     = txtLastName.Text;
                objModel.mobileNumber                 = txtMobileNumber.Text;
                objModel.email                        = txtEmail.Text;
                objModel.streetAddress1               = txtAddress1.Text;
                objModel.streetAddress2               = txtAddress2.Text + " " + txtAddress3.Text;
                objModel.city                         = txtCity.Text;
                objModel.state                        = txtState.Text;
                objModel.pincode                      = txtpincode.Text;
                objModel.deviceId                     = Convert.ToInt32(hdDeviceID.Value);
                objModel.imei1                        = txtIMEINo.Text;
                objModel.storageCapacityInGB          = Convert.ToInt32(txtRom.Text);
                objModel.ramInGB                      = Convert.ToInt32(txtRam.Text);
                objModel.colour                       = txtColor.Text;
                objModel.purchaseDate                 = txtInvoiceDate.Text;
                objModel.purchasePrice                = purchasevalue;
                objModel.invoiceNumber                = txtInvoiceNo.Text;
                objModel.planPurchasePrice            = 600;
                objModel.manufacturerWarrantyInMonths = 6;
                objModel.invoiceImageUrl              = objDetail.invoiceurl;
                objModel.imeiOrSerialNumberImageUrl   = objDetail.imeiimageurl;
                var jsonobjModel                      = JsonConvert.SerializeObject(objModel);
                var client                            = new RestClient((ConfigurationManager.AppSettings["AcrossCreateInsuranceApi"]));
                client.Timeout                        = -1;
                var request                           = new RestRequest(Method.POST);
                request.AddHeader("x-api-key", ConfigurationManager.AppSettings["Acrossxapikey"]);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", jsonobjModel, ParameterType.RequestBody);
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                IRestResponse response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string jsonconn             = response.Content;
                    jsonconn                    = "[" + jsonconn + "]";
                    //var objResponse             = JsonConvert.DeserializeObject<AcrossAssistanceResponseSuccessModel>(jsonconn);
                    IsgenInsurance              = true;
                    int result = objMainClass.UpdateSalesInvoiceInsuranceStatus(Convert.ToInt32(hdSrNo.Value), hdSoNo.Value, objDetail.invoiceurl, objDetail.imeiimageurl, 12313, Convert.ToInt32(Session["USERID"]));
                    
                    ResetInsurance();
                    BindProductDetail();
                    
                    if(result > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Insurance Generated Succcessfully." + "\");", true);
                    }
                }
                else
                {
                    string jsonconn             = response.Content;
                    jsonconn                    = "[" + jsonconn + "]";
                    //var objErrorResponse        = JsonConvert.DeserializeObject<AcrossAssistanceResponseErrorModel>(jsonconn);
                    IsgenInsurance              = false;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + jsonconn + "\");", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return IsgenInsurance;
        }

        public AcrossAssistanceFileDetail AWSFileUpload()
        {
            AcrossAssistanceFileDetail objdetail = new AcrossAssistanceFileDetail();
            try
            {
                //for invoice
                Stream st                   = fuinvoiceimage.PostedFile.InputStream;
                string name                 = Path.GetFileName(fuinvoiceimage.FileName);
                string actualname           = @name;
                string[] filedetail         = actualname.Split('.');
                string myBucketName         = ConfigurationManager.AppSettings["AWSBucketName"]; //your s3 bucket name goes here  
                string s3DirectoryName      = "";
                string guidfilename         = GeneralFunctionality.GetRandomGuid();
                guidfilename                = guidfilename + filedetail[1];
                string s3FileName           = @guidfilename;
                bool a;
                AWSFileUpload myUploader    = new AWSFileUpload();
                a                           = myUploader.sendMyFileToS3(st, myBucketName, s3DirectoryName, s3FileName);
                if (a == true)
                {
                    objdetail.invoiceurl = ConfigurationManager.AppSettings["ImageBaseURL"] + guidfilename;
                }

                //for imeiimage
                Stream sts = fuImeiImage.PostedFile.InputStream;
                string names = Path.GetFileName(fuImeiImage.FileName);
                string actualnames = @names;
                string[] filedetails = actualnames.Split('.');
                string myBucketNames = ConfigurationManager.AppSettings["AWSBucketName"]; //your s3 bucket name goes here  
                string s3DirectoryNames = "";
                string guidfilenames = GeneralFunctionality.GetRandomGuid();
                guidfilenames = guidfilenames + filedetails[1];
                string s3FileNames = @guidfilenames;
                bool aa;
                AWSFileUpload myUploaders = new AWSFileUpload();
                aa = myUploader.sendMyFileToS3(sts, myBucketNames, s3DirectoryNames, s3FileNames);
                if (aa == true)
                {
                    objdetail.imeiimageurl = ConfigurationManager.AppSettings["ImageBaseURL"] + guidfilenames;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return objdetail;
        }
    }
}