using Microsoft.VisualStudio.OLE.Interop;
using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.SD
{
    public partial class frmCustomerList : System.Web.UI.Page
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
                        if (FormRights.bAdd == false) //if (objDALUserRights.bView == false)
                        {
                            lnkAddNewCustomer.Enabled = false;
                        }
                        objBindDDL.FillCustype(ddlCustomerType, "SELECTCUSTTYPE");
                        objBindDDL.FillCity(ddlCity);
                        objBindDDL.FillLists(ddlzone, "ZO");
                        BindGrid();
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
                if (gvList.Rows.Count > 0)
                {
                    gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                }

            }
        }

        protected void lnkSerch_Click(object sender, EventArgs e)
        {
            try
            {
                BindGrid();
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
                    string customercode = "";

                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    customercode = grdrow.Cells[0].Text;



                    DataSet ds = new DataSet();
                    ds = objMainClass.CustomerListByCode(customercode);
                    DataTable dt = ds.Tables[0];
                    DataTable dt1 = ds.Tables[1];

                    if (dt.Rows.Count > 0)
                    {
                        lblCustomerType.Text = Convert.ToString(dt.Rows[0]["CustomerType"]);
                        lblCustomerCode.Text = Convert.ToString(dt.Rows[0]["CUSTCODE"]);
                        lblCategoryType.Text = Convert.ToString(dt.Rows[0]["CategoryName"]);
                        lblTitle.Text = Convert.ToString(dt.Rows[0]["TITLE"]);
                        lblOldCustomerCode.Text = Convert.ToString(dt.Rows[0]["OLDACCODE"]);
                        lblName.Text = Convert.ToString(dt.Rows[0]["NAME1"]);
                        lblName2.Text = Convert.ToString(dt.Rows[0]["NAME2"]);
                        lblShortName.Text = Convert.ToString(dt.Rows[0]["SHORTNM"]);
                        lblCustomerGroup.Text = Convert.ToString(dt.Rows[0]["CustomerGroup"]);
                        lblAddress1.Text = Convert.ToString(dt.Rows[0]["ADDR1"]);
                        lblAddress2.Text = Convert.ToString(dt.Rows[0]["ADDR2"]);
                        lblAddress3.Text = Convert.ToString(dt.Rows[0]["ADDR3"]);
                        lblCountry.Text = Convert.ToString(dt.Rows[0]["COUNTRY"]);
                        lblState.Text = Convert.ToString(dt.Rows[0]["STATE"]);
                        lblCity.Text = Convert.ToString(dt.Rows[0]["CITY"]);
                        lblPincode.Text = Convert.ToString(dt.Rows[0]["POSTALCODE"]);
                        lblMobile.Text = Convert.ToString(dt.Rows[0]["MOBILENO"]);
                        lblEmail.Text = Convert.ToString(dt.Rows[0]["EMAILID"]);
                        lblContactPerson.Text = Convert.ToString(dt.Rows[0]["CONTACTPERSON"]);
                        lblContactNo.Text = Convert.ToString(dt.Rows[0]["CONTACTNO"]);
                        lblZone.Text = Convert.ToString(dt.Rows[0]["ZONE1"]);
                        lblRegion.Text = Convert.ToString(dt.Rows[0]["REGION"]);
                        lblWebsite.Text = Convert.ToString(dt.Rows[0]["WEBSITE"]);
                        lblCSTNo.Text = Convert.ToString(dt.Rows[0]["CSTNO"]);
                        lblCSTDate.Text = Convert.ToString(dt.Rows[0]["CSTDTf"]);
                        lblLSTNo.Text = Convert.ToString(dt.Rows[0]["LSTNO"]);
                        lblLSTDate.Text = Convert.ToString(dt.Rows[0]["LSTDTf"]);
                        lblPanCardNo.Text = Convert.ToString(dt.Rows[0]["PANNO"]);
                        lblAdharNo.Text = Convert.ToString(dt.Rows[0]["AADHARNO"]);
                        lblService.Text = Convert.ToString(dt.Rows[0]["STREGNO"]);
                        lblGstNo.Text = Convert.ToString(dt.Rows[0]["GSTNO"]);
                        lblECCNo.Text = Convert.ToString(dt.Rows[0]["ECCNO"]);
                        lblExciseRegisterNo.Text = Convert.ToString(dt.Rows[0]["EXREGNO"]);
                        lblExciseRange.Text = Convert.ToString(dt.Rows[0]["EXRANGE"]);
                        lblExciseDivision.Text = Convert.ToString(dt.Rows[0]["EXDIVISION"]);
                        lblExciseComm.Text = Convert.ToString(dt.Rows[0]["EXCOMM"]);
                        lblExciseVend.Text = Convert.ToString(dt.Rows[0]["VENDROTYPE"]);
                        lblBankName.Text = Convert.ToString(dt.Rows[0]["BANKNAME"]);
                        lblAccountNo.Text = Convert.ToString(dt.Rows[0]["ACCOUNTNO"]);
                        lblISFCCode.Text = Convert.ToString(dt.Rows[0]["IFSCCODE"]);
                        lblAccountType.Text = Convert.ToString(dt.Rows[0]["ACCOUNTTYPE"]);


                        grvCommunication.DataSource = dt1;
                        grvCommunication.DataBind();
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

                    string Customercode = grdrow.Cells[0].Text;

                    DataTable dt = new DataTable();
                    DataSet ds = objMainClass.CustomerListByCode(Customercode);
                    if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                    {
                        dt = ds.Tables[2];
                    }
                    if (dt.Rows.Count > 0)
                    {
                        gvDetail.DataSource = dt;
                        gvDetail.DataBind();
                        hfcustomercode.Value = objMainClass.strConvertZeroPadding(Customercode);


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
                    //    Label lblImageExtension = e.Row.FindControl("lblImageExtension") as Label;
                    //    if (lblImageData.Text != null && lblImageType.Text != null && lblImageData.Text != string.Empty && lblImageType.Text != string.Empty && lblImageData.Text != "" && lblImageType.Text != "")
                    //    {
                    //        if (lblImageExtension.Text == ".jpg" || lblImageExtension.Text == ".jpeg")
                    //        {
                    //            string imageUrl = "data:image/jpg;base64," + Convert.ToBase64String((byte[])dr["IMAGE"]);
                    //            (e.Row.FindControl("imgImage") as Image).ImageUrl = imageUrl;
                    //        }
                    //        else if (lblImageExtension.Text == ".png")
                    //        {
                    //            string imageUrl = "data:image/png;base64," + Convert.ToBase64String((byte[])dr["IMAGE"]);
                    //            (e.Row.FindControl("imgImage") as Image).ImageUrl = imageUrl;
                    //        }
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
                            else if (lblImageExtension.Text == ".png")
                            {
                                string imageUrl = "data:image/png;base64," + Convert.ToBase64String((byte[])dr["IMAGE"]);
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

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    Label lblImageID = grdrow.FindControl("lblImageID") as Label;
                    Label lblImageType = grdrow.FindControl("lblImageType") as Label;
                    // DataTable dt = objMainClass.GetImageByID(Convert.ToInt32(lblImageID.Text), "SELECTIMAGEID");
                    DataTable dt = new DataTable();
                    dt = objMainClass.CustomerListByCode(hfcustomercode.Value, Convert.ToInt32(lblImageID.Text), "SELECTIMAGEID").Tables[0];
                    //if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                    //{
                    //    dt = ds.Tables[2];
                    //}
                    if (dt.Rows.Count > 0)
                    {
                        byte[] bytes;
                        string fileName, contentType = string.Empty;

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
                        else if (Convert.ToString(dt.Rows[0]["EXTENSION"]) == ".png" || Convert.ToString(dt.Rows[0]["EXTENSION"]) == ".png")
                        {
                            contentType = "image/png";
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

                        fileName = hfcustomercode.Value + " - " + lblImageType.Text + "" + Convert.ToString(dt.Rows[0]["EXTENSION"]);


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

        protected void ddlTally_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    // hflblCustomerCode.Value = lblCustomerCode.Text;
                    //  int iResult = objMainClass.UpdateCustomer(objMainClass.intCmpId, objMainClass.strConvertZeroPadding(lblCustomerCode.Text), "TALLYUPDATE", Convert.ToInt32(ddlTally.SelectedValue), Convert.ToInt32(Session["USERID"]), "");
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

        public void BindGrid()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objMainClass.CustomerList(txtCustomerCode.Text, (ddlCustomerType.SelectedValue == "0") ? "" : ddlCustomerType.SelectedValue, txtName.Text, ((ddlzone.SelectedIndex > 0) ? ddlzone.SelectedItem.Text : ""), (ddlCity.SelectedIndex == 0) ? "" : ddlCity.SelectedItem.Text, txtPostalCode.Text);
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
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
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
                string attachment = "attachment; filename=CustomerLists.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vdn.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gvList.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }


       
    }
}
