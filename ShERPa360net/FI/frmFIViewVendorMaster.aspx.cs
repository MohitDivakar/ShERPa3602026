using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.FI
{
    public partial class frmFIViewVendorMaster : System.Web.UI.Page
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
                            lnkNewVend.Enabled = false;
                                }
                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();



                        objBindDDL.FillDealer(ddlDealer, 1);
                        objBindDDL.FillCity(ddlCity);
                        objBindDDL.FillVendType(ddlVendType);
                        objBindDDL.FillLists(ddlVendCat, "VC");

                        BindGrid(objMainClass.intCmpId, "", "", 0, 0, "", "", 0, "ACTIVE10", txtFromDate.Text, txtToDate.Text);

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






        public void BindGrid(int CMPID, string VENDCODE, string VENDNAME, int DEALERID, int APPROVED, string CITYID, string VENDTYPE, int VENDCAT, string ACTION, string FROMDATE, string TODATE)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetVendor(CMPID, VENDCODE, VENDNAME, DEALERID, APPROVED, CITYID, VENDTYPE, VENDCAT, ACTION, FROMDATE, TODATE);
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

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
        protected void lnkSearhVend_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    BindGrid(objMainClass.intCmpId, txtVendCode.Text, txtVendName.Text, Convert.ToInt32(ddlDealer.SelectedValue), chkApproved.Checked == true ? 1 : 2,
                        ddlCity.SelectedValue, ddlVendType.SelectedIndex > 0 ? ddlVendType.SelectedValue : "", Convert.ToInt32(ddlVendCat.SelectedValue), "VENDSEARCH",
                        txtFromDate.Text, txtToDate.Text);
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

        protected void lnkExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    string attachment = "attachment; filename=VendorList.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vdn.ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    gvList.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();
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

        protected void btnDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    string vendcode = "";
                    if (hflblVendCode.Value != null && hflblVendCode.Value != "" && hflblVendCode.Value != string.Empty)
                    {
                        vendcode = lblVendCode.Text;
                    }
                    else
                    {
                        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                        vendcode = grdrow.Cells[0].Text;
                    }



                    DataTable dt = new DataTable();
                    dt = objMainClass.GetVendor(objMainClass.intCmpId, objMainClass.strConvertZeroPadding(vendcode), "", 0, 0, "", "", 0, "VENDSEARCHONE", txtFromDate.Text, txtToDate.Text);

                    if (dt.Rows.Count > 0)
                    {
                        lblVendCode.Text = Convert.ToString(dt.Rows[0]["VENDCODE"]);
                        lblVendType.Text = Convert.ToString(dt.Rows[0]["VENDTYPE"]);
                        lblVendorCategory.Text = Convert.ToString(dt.Rows[0]["VENDCAT"]);
                        lblDealer.Text = Convert.ToString(dt.Rows[0]["DEALERNAME"]);
                        lblTitle.Text = Convert.ToString(dt.Rows[0]["TITLE"]);
                        lblVendorName.Text = Convert.ToString(dt.Rows[0]["NAME1"]);
                        lblContactPerson.Text = Convert.ToString(dt.Rows[0]["CONTACTPERSON"]);
                        lblContactNo.Text = Convert.ToString(dt.Rows[0]["CONTACTNO"]);
                        lblMobileNo.Text = Convert.ToString(dt.Rows[0]["MOBILENO"]);
                        lblEmailID.Text = Convert.ToString(dt.Rows[0]["EMAILID"]);
                        lblWebsite.Text = Convert.ToString(dt.Rows[0]["WEBSITE"]);
                        lblStatus.Text = Convert.ToString(dt.Rows[0]["STATUS"]);
                        lblAddress1.Text = Convert.ToString(dt.Rows[0]["ADDRESS"]);
                        //lblAddress2.Text = Convert.ToString(dt.Rows[0]["ADDR2"]);
                        //lblAddress3.Text = Convert.ToString(dt.Rows[0]["ADDR3"]);
                        lblCity.Text = Convert.ToString(dt.Rows[0]["CITY"]);
                        lblPostalCode.Text = Convert.ToString(dt.Rows[0]["POSTALCODE"]);
                        lblState.Text = Convert.ToString(dt.Rows[0]["STATE"]);
                        lblCountry.Text = Convert.ToString(dt.Rows[0]["COUNTRY"]);
                        lblGSTIN.Text = Convert.ToString(dt.Rows[0]["GSTNO"]);
                        lblPAN.Text = Convert.ToString(dt.Rows[0]["PANNO"]);
                        lblAadharno.Text = Convert.ToString(dt.Rows[0]["AADHARNO"]);
                        lblBankName.Text = Convert.ToString(dt.Rows[0]["BANKNAME"]);
                        lblAccountNo.Text = Convert.ToString(dt.Rows[0]["ACCOUNTNO"]);
                        lblIFSCCode.Text = Convert.ToString(dt.Rows[0]["IFSCCODE"]);
                        lblAccountType.Text = Convert.ToString(dt.Rows[0]["ACCTYPE"]);
                        lblUPIWallet.Text = Convert.ToString(dt.Rows[0]["UPIWALLET"]);
                        lblPaymentNo.Text = Convert.ToString(dt.Rows[0]["WALLTEPAYNO"]);
                        lblOwnerName.Text = Convert.ToString(dt.Rows[0]["WALLETOWNERNAME"]);
                        lblUnderMargin.Text = Convert.ToString(dt.Rows[0]["UNDERMARGINSCHEME"]);
                        lblAgreement.Text = Convert.ToString(dt.Rows[0]["AGREEMENTRCVD"]);
                        lblMobileSale.Text = Convert.ToString(dt.Rows[0]["MOBILESALE"]);
                        lblRegisterBy.Text = Convert.ToString(dt.Rows[0]["CREATBY"]);
                        lblRegiDate.Text = Convert.ToString(dt.Rows[0]["CREATEDATE"]);
                        lblAprvBy.Text = Convert.ToString(dt.Rows[0]["APRVBY"]);
                        lblAprvDate.Text = Convert.ToString(dt.Rows[0]["APRVDATE"]);
                        lblRejectReason.Text = Convert.ToString(dt.Rows[0]["REJREASON"]);
                        lblRejectReason.Text = Convert.ToString(dt.Rows[0]["REJREASON"]);

                        grvCommunication.DataSource = dt;
                        grvCommunication.DataBind();
                        hflblVendCode.Value = string.Empty;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
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

        protected void btnImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

                    string vendcode = grdrow.Cells[0].Text;

                    DataTable dt = new DataTable();
                    dt = objMainClass.GetVendor(objMainClass.intCmpId, objMainClass.strConvertZeroPadding(vendcode), "", 0, 0, "", "", 0, "SEARCHIMAGE", txtFromDate.Text, txtToDate.Text);

                    if (dt.Rows.Count > 0)
                    {
                        gvDetail.DataSource = dt;
                        gvDetail.DataBind();
                        hfVendCode.Value = objMainClass.strConvertZeroPadding(vendcode);


                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-image').modal();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
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

        protected void gvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    //if (e.Row.RowType == DataControlRowType.DataRow)
                    //{
                    //    DataRowView dr = (DataRowView)e.Row.DataItem;
                    //    string LISTDESC = e.Row.Cells[0].Text;
                    //    string LISTDESC1 = e.Row.Cells[1].Text;
                    //    string LISTDESC2 = e.Row.Cells[2].Text;

                    //    Label lblImageType = e.Row.FindControl("lblImageType") as Label;
                    //    Label lblImageData = e.Row.FindControl("lblImageData") as Label;
                    //    if (lblImageData.Text != null && lblImageType.Text != null && lblImageData.Text != string.Empty && lblImageType.Text != string.Empty && lblImageData.Text != "" && lblImageType.Text != "")
                    //    {
                    //        string imageUrl = "data:image/jpg;base64," + Convert.ToBase64String((byte[])dr["IMAGE"]);
                    //        (e.Row.FindControl("imgImage") as Image).ImageUrl = imageUrl;
                    //    }
                    //}


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

        protected void ddlTally_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    hflblVendCode.Value = lblVendCode.Text;
                    int iResult = objMainClass.UpdateVendor(objMainClass.intCmpId, objMainClass.strConvertZeroPadding(lblVendCode.Text), "TALLYUPDATE", Convert.ToInt32(ddlTally.SelectedValue), Convert.ToInt32(Session["USERID"]), "");
                    btnDetails_Click(1, e);
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

                        fileName = hfVendCode.Value + " - " + lblImageType.Text + "" + Convert.ToString(dt.Rows[0]["EXTENSION"]);


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
    }
}