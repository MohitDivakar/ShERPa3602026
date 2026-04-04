using Newtonsoft.Json;
using RestSharp;
using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.REPORTS
{
    public partial class rptVendorStatus : System.Web.UI.Page
    {

        DALUserRights objDALUserRights = new DALUserRights();
        MainClass objMainClass = new MainClass();
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
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../REPORTS/ReportDashboard.aspx' });", true);
                            return;
                        }

                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtTodate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();

                        objBindDDL.FillLists(ddlVendCategory, "VC");
                        objBindDDL.FillVendType(ddlVendType);
                        BindData();
                    }
                    else
                    {
                        Session.Abandon();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
                }
            }
        }

        public void BindData()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetVendorReport(objMainClass.intCmpId, txtFromDate.Text, txtTodate.Text, txtVendCode.Text, ddlVendCategory.SelectedIndex > 0 ? Convert.ToInt32(ddlVendCategory.SelectedValue) : 0,
                            ddlVendType.SelectedIndex > 0 ? ddlVendType.SelectedValue : "", chkRejected.Checked == true ? 1 : 0, 0, "REPORT");

                    if (dt.Rows.Count > 0)
                    {
                        grVendorList.DataSource = dt;
                        grVendorList.DataBind();
                        grVendorList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    else
                    {
                        grVendorList.DataSource = string.Empty;
                        grVendorList.DataBind();
                    }
                }
                else
                {
                    Session.Abandon();
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


        protected void lnkExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    string attachment = "attachment; filename=VendorReport.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vdn.ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    grVendorList.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();
                }
                else
                {
                    Session.Abandon();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lnkSearhSR_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    BindData();
                }
                else
                {
                    Session.Abandon();
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
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    string vendcode = grdrow.Cells[1].Text;

                    DataTable dt = new DataTable();
                    dt = objMainClass.GetVendorReport(objMainClass.intCmpId, "", "", vendcode, 0, "", 0, 0, "REJECTEDVENDOR");
                    if (dt.Rows.Count > 0)
                    {
                        objBindDDL.FillLists(ddlAccountType, "ACT");
                        objBindDDL.FillLists(ddlUPIWalletType, "UPI");

                        lblVendCode.Text = Convert.ToString(dt.Rows[0]["VENDCODE"]);
                        hflblVendCode.Value = Convert.ToString(dt.Rows[0]["VENDCODE"]);
                        lblVendName.Text = Convert.ToString(dt.Rows[0]["NAME1"]);
                        lblDealerName.Text = Convert.ToString(dt.Rows[0]["DEALERNAME"]);
                        txtPAN.Text = Convert.ToString(dt.Rows[0]["PANNO"]);
                        txtAadharNo.Text = Convert.ToString(dt.Rows[0]["AADHARNO"]);
                        txtGST.Text = Convert.ToString(dt.Rows[0]["GSTNO"]);
                        if (Convert.ToString(dt.Rows[0]["MSME"]) != string.Empty && Convert.ToString(dt.Rows[0]["MSME"]) != "" && Convert.ToString(dt.Rows[0]["MSME"]) != null)
                        {
                            rblMSME.SelectedValue = Convert.ToString(dt.Rows[0]["MSME"]);
                        }
                        txtIFSCCode.Text = Convert.ToString(dt.Rows[0]["IFSCCODE"]);
                        txtBankName.Text = Convert.ToString(dt.Rows[0]["BANKNAME"]);
                        txtACNo.Text = Convert.ToString(dt.Rows[0]["ACCOUNTNO"]);
                        if (Convert.ToString(dt.Rows[0]["ACCNTTYPE"]) != string.Empty && Convert.ToString(dt.Rows[0]["ACCNTTYPE"]) != "" && Convert.ToString(dt.Rows[0]["ACCNTTYPE"]) != null)
                        {
                            ddlAccountType.SelectedValue = Convert.ToString(dt.Rows[0]["ACCNTTYPE"]);
                        }
                        if (Convert.ToString(dt.Rows[0]["UPIWALLET"]) != string.Empty && Convert.ToString(dt.Rows[0]["UPIWALLET"]) != "" && Convert.ToString(dt.Rows[0]["UPIWALLET"]) != null)
                        {
                            ddlUPIWalletType.SelectedValue = Convert.ToString(dt.Rows[0]["UPIWALLET"]);
                        }
                        txtWalletPayNo.Text = Convert.ToString(dt.Rows[0]["WALLTEPAYNO"]);
                        txtWalletOwner.Text = Convert.ToString(dt.Rows[0]["WALLETOWNERNAME"]);
                        lblRejectBy.Text = Convert.ToString(dt.Rows[0]["REJECTBY"]);
                        lblRejectReason.Text = Convert.ToString(dt.Rows[0]["REJREASON"]);

                        gvDetail.DataSource = dt;
                        gvDetail.DataBind();

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-edit').modal();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
                    }
                }
                else
                {
                    Session.Abandon();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void grVendorList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        DataRowView dr = (DataRowView)e.Row.DataItem;
                        string VENDCODE = e.Row.Cells[1].Text;
                        string REJREASON = e.Row.Cells[28].Text;
                        string STATUS = e.Row.Cells[29].Text;
                        LinkButton lnkEdit = e.Row.FindControl("lnkEdit") as LinkButton;

                        if (REJREASON != "" && REJREASON != string.Empty && REJREASON != null && REJREASON != "&nbsp;" && STATUS == "InActive")
                        {
                            lnkEdit.Visible = true;
                        }
                    }
                }
                else
                {
                    Session.Abandon();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void txtIFSCCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (txtIFSCCode.Text != string.Empty && txtIFSCCode.Text != "" && txtIFSCCode.Text != null)
                    {
                        DataTable Bankdt = new DataTable();

                        var client = new RestClient(("https://ifsc.firstatom.org/key/84NnS05xbn6U5RKP1dGEBU83g/ifsc/" + txtIFSCCode.Text));
                        client.Timeout = -1;
                        var request = new RestRequest(Method.GET);
                        IRestResponse response = client.Execute(request);
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            string jsonconn = response.Content;
                            jsonconn = "[" + jsonconn + "]";
                            Bankdt = (DataTable)JsonConvert.DeserializeObject(jsonconn, (typeof(DataTable)));


                            if (Bankdt.Rows.Count > 0)
                            {
                                txtBankName.Text = Convert.ToString(Bankdt.Rows[0]["Bank"]) + " - " + Convert.ToString(Bankdt.Rows[0]["Branch"]);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Incorrect IFSC Code. Bank Details Not Found!');", true);
                            }
                            rfvIFSCCode.Enabled = true;
                            rfvBankName.Enabled = true;
                            rfvAccountNo.Enabled = true;
                            rfvAccountType.Enabled = true;
                            rfvFUCheque.Enabled = true;
                            rfvWalletType.Enabled = false;
                            rfvWalletePayNo.Enabled = false;
                            rfvWalleteOwnerName.Enabled = false;
                        }
                        else
                        {

                            rfvIFSCCode.Enabled = true;
                            rfvBankName.Enabled = true;
                            rfvAccountNo.Enabled = true;
                            rfvAccountType.Enabled = true;
                            rfvFUCheque.Enabled = true;
                            //rfvWalletType.Enabled = true;
                            //rfvWalletePayNo.Enabled = true;
                            //rfvWalleteOwnerName.Enabled = true;

                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Incorrect IFSC Code!');", true);
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-edit').modal();", true);
                        }
                    }
                    else
                    {
                        rfvIFSCCode.Enabled = false;
                        rfvBankName.Enabled = false;
                        rfvAccountNo.Enabled = false;
                        rfvAccountType.Enabled = false;
                        rfvFUCheque.Enabled = false;

                        rfvWalletType.Enabled = true;
                        rfvWalletePayNo.Enabled = true;
                        rfvWalleteOwnerName.Enabled = true;
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please Enter IFSC Code!');", true);
                    }


                    if (fuPAN != null)
                    {
                        if (fuPAN.HasFiles)
                        {
                            Session["FUPANS"] = fuPAN;
                        }
                    }

                    if (fuIDProof != null)
                    {
                        if (fuIDProof.HasFiles)
                        {
                            Session["FUIDPROOFS"] = fuIDProof;
                        }
                    }

                    if (fuGSTCerti != null)
                    {
                        if (fuGSTCerti.HasFiles)
                        {
                            Session["FUGSTCERIS"] = fuGSTCerti;
                        }
                    }

                    if (fuMSMECerti != null)
                    {
                        if (fuMSMECerti.HasFiles)
                        {
                            Session["FUMSMES"] = fuMSMECerti;
                        }
                    }

                    if (fuCheque != null)
                    {
                        if (fuCheque.HasFiles)
                        {
                            Session["FUCHEQUES"] = fuCheque;
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                }
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-edit').modal();", true);

            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-edit').modal();", true);
            }
        }

        protected void gvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        DataRowView dr = (DataRowView)e.Row.DataItem;
                        string LISTDESC = e.Row.Cells[0].Text;
                        string LISTDESC1 = e.Row.Cells[1].Text;
                        string LISTDESC2 = e.Row.Cells[2].Text;

                        Label lblImageType = e.Row.FindControl("lblImageType") as Label;
                        Label lblImageData = e.Row.FindControl("lblImageData") as Label;
                        Label lblImageID = e.Row.FindControl("lblImageID") as Label;
                        Label lblImageExtension = e.Row.FindControl("lblImageExtension") as Label;
                        LinkButton lnkDownload = e.Row.FindControl("lnkDownload") as LinkButton;

                        if (lblImageData.Text != null && lblImageType.Text != null && lblImageData.Text != string.Empty && lblImageType.Text != string.Empty && lblImageData.Text != "" && lblImageType.Text != "")
                        {
                            if (lblImageExtension.Text == ".jpg" || lblImageExtension.Text == ".jpeg")
                            {
                                string imageUrl = "data:image/jpg;base64," + Convert.ToBase64String((byte[])dr["IMAGE"]);
                                (e.Row.FindControl("imgImage") as Image).ImageUrl = imageUrl;
                                (e.Row.FindControl("imgImage") as Image).Visible = true;
                                //lnkDownload.Visible = false;
                            }
                            else
                            {
                                (e.Row.FindControl("imgImage") as Image).Visible = false;
                                lnkDownload.Visible = true;
                            }
                        }
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

        protected void ddlUPIWalletType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (ddlUPIWalletType.SelectedIndex > 0)
                    {
                        rfvIFSCCode.Enabled = false;
                        rfvBankName.Enabled = false;
                        rfvAccountNo.Enabled = false;
                        rfvAccountType.Enabled = false;
                        rfvFUCheque.Enabled = false;
                        rfvWalletType.Enabled = true;
                        rfvWalletePayNo.Enabled = true;
                        rfvWalleteOwnerName.Enabled = true;
                    }
                    else
                    {
                        rfvIFSCCode.Enabled = true;
                        rfvBankName.Enabled = true;
                        rfvAccountNo.Enabled = true;
                        rfvAccountType.Enabled = true;
                        rfvFUCheque.Enabled = true;

                        rfvWalletType.Enabled = false;
                        rfvWalletePayNo.Enabled = false;
                        rfvWalleteOwnerName.Enabled = false;

                        ddlUPIWalletType.SelectedIndex = 0;
                        txtWalletPayNo.Text = string.Empty;
                        txtWalletOwner.Text = string.Empty;

                    }


                    if (fuPAN != null)
                    {
                        if (fuPAN.HasFiles)
                        {
                            Session["FUPANS"] = fuPAN;
                        }
                    }

                    if (fuIDProof != null)
                    {
                        if (fuIDProof.HasFiles)
                        {
                            Session["FUIDPROOFS"] = fuIDProof;
                        }
                    }

                    if (fuGSTCerti != null)
                    {
                        if (fuGSTCerti.HasFiles)
                        {
                            Session["FUGSTCERIS"] = fuGSTCerti;
                        }
                    }

                    if (fuMSMECerti != null)
                    {
                        if (fuMSMECerti.HasFiles)
                        {
                            Session["FUMSMES"] = fuMSMECerti;
                        }
                    }

                    if (fuCheque != null)
                    {
                        if (fuCheque.HasFiles)
                        {
                            Session["FUCHEQUES"] = fuCheque;
                        }
                    }

                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
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

        protected void rblMSME_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (rblMSME.SelectedValue == "0")
                    {
                        rfvMSMECerti.Enabled = false;
                    }
                    else
                    {
                        rfvMSMECerti.Enabled = true;
                    }



                    if (fuPAN != null)
                    {
                        if (fuPAN.HasFiles)
                        {
                            Session["FUPANS"] = fuPAN;
                        }
                    }

                    if (fuIDProof != null)
                    {
                        if (fuIDProof.HasFiles)
                        {
                            Session["FUIDPROOFS"] = fuIDProof;
                        }
                    }

                    if (fuGSTCerti != null)
                    {
                        if (fuGSTCerti.HasFiles)
                        {
                            Session["FUGSTCERIS"] = fuGSTCerti;
                        }
                    }

                    if (fuMSMECerti != null)
                    {
                        if (fuMSMECerti.HasFiles)
                        {
                            Session["FUMSMES"] = fuMSMECerti;
                        }
                    }

                    if (fuCheque != null)
                    {
                        if (fuCheque.HasFiles)
                        {
                            Session["FUCHEQUES"] = fuCheque;
                        }
                    }

                    //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#modal-detail').modal('show')", false);

                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", false);

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


        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    Label lblImageID = grdrow.FindControl("lblImageID") as Label;
                    Label lblImageType = grdrow.FindControl("lblImageType") as Label;
                    DataTable dt = objMainClass.GetImageByID(Convert.ToInt32(lblImageID.Text), "SELECTIMAGEID");
                    if (dt.Rows.Count > 0)
                    {
                        byte[] bytes;
                        string fileName, contentType;

                        bytes = (byte[])dt.Rows[0]["IMAGE"];
                        if (Convert.ToString(dt.Rows[0]["EXTENSION"]) == ".htm" || Convert.ToString(dt.Rows[0]["EXTENSION"]) == ".html")
                        {
                            contentType = "text/HTML";
                        }
                        else if (Convert.ToString(dt.Rows[0]["EXTENSION"]) == ".txt")
                        {
                            contentType = "text/plain";
                        }
                        else if (Convert.ToString(dt.Rows[0]["EXTENSION"]) == ".doc" || Convert.ToString(dt.Rows[0]["EXTENSION"]) == ".rtf" || Convert.ToString(dt.Rows[0]["EXTENSION"]) == ".docx")
                        {
                            contentType = "Application/msword";
                        }
                        else if (Convert.ToString(dt.Rows[0]["EXTENSION"]) == ".xls" || Convert.ToString(dt.Rows[0]["EXTENSION"]) == ".xlsx")
                        {
                            contentType = "text/x-msexcel";
                        }
                        else if (Convert.ToString(dt.Rows[0]["EXTENSION"]) == ".jpg" || Convert.ToString(dt.Rows[0]["EXTENSION"]) == ".jpeg")
                        {
                            contentType = "image/jpeg";
                        }
                        else if (Convert.ToString(dt.Rows[0]["EXTENSION"]) == ".gif")
                        {
                            contentType = "image/GIF";
                        }
                        else if (Convert.ToString(dt.Rows[0]["EXTENSION"]) == ".pdf")
                        {
                            contentType = "application/pdf";
                        }
                        else
                        {
                            contentType = "image/jpeg";
                        }

                        fileName = lblVendCode.Text + " - " + lblImageType.Text + "" + Convert.ToString(dt.Rows[0]["EXTENSION"]);


                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = contentType;
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                        Response.BinaryWrite(bytes);
                        Response.Flush();
                        Response.End();




                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('File Not Found!');", true);
                    }


                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);



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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (ddlUPIWalletType.SelectedIndex > 0 || (txtIFSCCode.Text != "" && txtIFSCCode.Text != string.Empty))
                    {
                        byte[] IDPROOF = null;
                        byte[] PAN = null;
                        byte[] CHEQUE = null;
                        byte[] SHOP = null;
                        byte[] GSTCERTI = null;
                        byte[] MSMECERTI = null;

                        FileUpload fudIDPROOF;
                        FileUpload fudPAN;
                        FileUpload fudCHEQUE;
                        FileUpload fudGST;
                        FileUpload fudMSME;

                        string IDPROOFTYPE = ".jpeg";
                        string PANTYPE = ".jpeg";
                        string CHEQUETYPE = ".jpeg";
                        string GSTCERTITYPE = ".pdf";
                        string MSMECERTITYPE = ".pdf";


                        if (fuPAN != null)
                        {
                            if (fuPAN.HasFiles)
                            {
                                Session["FUPANS"] = fuPAN;
                            }
                        }

                        if (fuIDProof != null)
                        {
                            if (fuIDProof.HasFiles)
                            {
                                Session["FUIDPROOFS"] = fuIDProof;
                            }
                        }

                        if (fuGSTCerti != null)
                        {
                            if (fuGSTCerti.HasFiles)
                            {
                                Session["FUGSTCERIS"] = fuGSTCerti;
                            }
                        }

                        if (fuMSMECerti != null)
                        {
                            if (fuMSMECerti.HasFiles)
                            {
                                Session["FUMSMES"] = fuMSMECerti;
                            }
                        }

                        if (fuCheque != null)
                        {
                            if (fuCheque.HasFiles)
                            {
                                Session["FUCHEQUES"] = fuCheque;
                            }
                        }


                        if (Session["FUIDPROOFS"] != null && Convert.ToString(Session["FUIDPROOFS"]) != "" && Convert.ToString(Session["FUIDPROOFS"]) != string.Empty)
                        {
                            fudIDPROOF = Session["FUIDPROOFS"] as FileUpload;

                            if (fudIDPROOF.HasFiles)
                            {
                                BinaryReader br1 = new BinaryReader(fudIDPROOF.PostedFile.InputStream);

                                IDPROOF = br1.ReadBytes(fudIDPROOF.PostedFile.ContentLength);
                                IDPROOFTYPE = System.IO.Path.GetExtension(fudIDPROOF.FileName);
                            }
                        }

                        if (Session["FUPANS"] != null)
                        {
                            fudPAN = Session["FUPANS"] as FileUpload;
                            if (fudPAN.HasFiles)
                            {
                                using (BinaryReader br = new BinaryReader(fudPAN.PostedFile.InputStream))
                                {
                                    PAN = br.ReadBytes(fudPAN.PostedFile.ContentLength);
                                    PANTYPE = System.IO.Path.GetExtension(fudPAN.FileName);
                                }
                            }
                        }

                        if (Session["FUCHEQUES"] != null)
                        {
                            fudCHEQUE = Session["FUCHEQUES"] as FileUpload;
                            if (fudCHEQUE.HasFiles)
                            {
                                using (BinaryReader br = new BinaryReader(fudCHEQUE.PostedFile.InputStream))
                                {
                                    CHEQUE = br.ReadBytes(fudCHEQUE.PostedFile.ContentLength);
                                    CHEQUETYPE = System.IO.Path.GetExtension(fudCHEQUE.FileName);
                                }
                            }
                        }

                        if (Session["FUGSTCERIS"] != null)
                        {

                            fudGST = Session["FUGSTCERIS"] as FileUpload;
                            if (fudGST.HasFiles)
                            {
                                using (BinaryReader br = new BinaryReader(fudGST.PostedFile.InputStream))
                                {
                                    GSTCERTI = br.ReadBytes(fudGST.PostedFile.ContentLength);
                                    GSTCERTITYPE = System.IO.Path.GetExtension(fudGST.FileName);
                                }
                            }
                        }

                        if (Session["FUMSMES"] != null)
                        {
                            fudMSME = Session["FUMSMES"] as FileUpload;
                            if (fudMSME.HasFiles)
                            {
                                using (BinaryReader br = new BinaryReader(fudMSME.PostedFile.InputStream))
                                {
                                    MSMECERTI = br.ReadBytes(fudMSME.PostedFile.ContentLength);
                                    MSMECERTITYPE = System.IO.Path.GetExtension(fudMSME.FileName);
                                }
                            }
                        }



                        string DOCNO = objMainClass.UpdateVendorData(objMainClass.intCmpId, lblVendCode.Text, txtPAN.Text, txtAadharNo.Text, txtGST.Text,
                            Convert.ToInt32(rblMSME.SelectedValue), txtIFSCCode.Text, txtBankName.Text, txtACNo.Text, Convert.ToInt32(ddlAccountType.SelectedValue),
                            Convert.ToInt32(ddlUPIWalletType.SelectedValue), txtWalletPayNo.Text, txtWalletOwner.Text, 2, IDPROOF, PAN, CHEQUE, SHOP, GSTCERTI, MSMECERTI, IDPROOFTYPE,
                            PANTYPE, CHEQUETYPE, GSTCERTITYPE, MSMECERTITYPE, Convert.ToInt32(Session["USERID"]), "UPDATEVENDDATA");

                        if (DOCNO != "" && DOCNO != string.Empty && DOCNO != null)
                        {
                            //    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                            //    MailMessage Msg = new MailMessage();
                            //    //Msg.To.Add("accounts@qarmatek.com");
                            //    //Msg.CC.Add("vihar.jethavani @qarmatek.com");
                            //    Msg.To.Add("accounts@qarmatek.com");
                            //    Msg.CC.Add("accounts2@qarma-tek.com");
                            //    Msg.From = new MailAddress("info@qarmatek.com", "Vendor Registration");
                            //    Msg.Subject = "Vendor Registration";
                            //    Msg.Body = "New Vendor Created. " + System.Environment.NewLine + "</br></br> Vendor Code is : " + objMainClass.strConvertZeroPadding(DOCNO) + " " + System.Environment.NewLine + "</br></br> " + System.Environment.NewLine;
                            //    Msg.IsBodyHtml = true;
                            //    SmtpClient smtp = new SmtpClient();
                            //    smtp.Host = "smtp.office365.com";
                            //    smtp.EnableSsl = true;
                            //    //smtp.Port = 587;
                            //    smtp.Port = 25;
                            //    smtp.UseDefaultCredentials = true;
                            //    smtp.Credentials = new System.Net.NetworkCredential("info@qarmatek.com", "Hof75626");
                            //    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            //    smtp.Send(Msg);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Vendor updated successfully! \");$('.close').click(function(){window.location.href ='rptVendorStatus.aspx' });", true);
                            Page_Load(1, e);
                            Session["FUIDPROOFS"] = null; Session["FUIDPROOFS"] = string.Empty;
                            Session["FUPANS"] = null; Session["FUPANS"] = string.Empty;
                            Session["FUGSTCERIS"] = null; Session["FUGSTCERIS"] = string.Empty;
                            Session["FUMSMES"] = null; Session["FUMSMES"] = string.Empty;
                            Session["FUCHEQUES"] = null; Session["FUCHEQUES"] = string.Empty;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Vendor not updated sucessfully!');", true);
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Enter either Bank Details or UPI Details.');", true);
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