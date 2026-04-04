using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.UTILITY
{
    public partial class frmItemMapping : System.Web.UI.Page
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


                        if (Request.QueryString.Count > 0)
                        {
                            if (Convert.ToString(Request.QueryString["ITEMCODE"]) != null && Convert.ToString(Request.QueryString["ITEMCODE"]) != string.Empty && Convert.ToString(Request.QueryString["ITEMCODE"]) != "")
                            {
                                Session["ITEMCODE"] = Convert.ToString(Request.QueryString["ITEMCODE"]);
                            }
                            Response.Redirect(Request.Url.AbsolutePath, false);
                        }
                        else
                        {


                            objBindDDL.FillBrand(ddlMake, 0);
                            objBindDDL.FillLists(ddlRAM, "RAM");
                            objBindDDL.FillLists(ddlROM, "ROM");
                            objBindDDL.FillLists(ddlGrade, "BG");
                            objBindDDL.FillLists(ddlColor, "CL");







                            if (Session["ITEMCODE"] != null && Convert.ToString(Session["ITEMCODE"]) != "" && Convert.ToString(Session["ITEMCODE"]) != string.Empty)
                            {
                                DataTable dt = new DataTable();
                                dt = objMainClass.GetItemMappingData(objMainClass.intCmpId, Convert.ToString(Session["ITEMCODE"]), "", "", "", "", 1, "ITEMSEARCH", 0, 0);

                                if (dt.Rows.Count > 0)
                                {
                                    txtItemCode.Text = Convert.ToString(dt.Rows[0]["ITEMCODE"]);
                                    txtItemDesc.Text = Convert.ToString(dt.Rows[0]["ITEMDESC"]);
                                    txtSKU.Text = Convert.ToString(dt.Rows[0]["SKU"]);
                                    txtFlipakrt.Text = Convert.ToString(dt.Rows[0]["FLIPKART"]);
                                    txtAmazon.Text = Convert.ToString(dt.Rows[0]["AMAZON"]);
                                    txtWebsite.Text = Convert.ToString(dt.Rows[0]["WEBSITE"]);
                                    txtSCWebsite.Text = Convert.ToString(dt.Rows[0]["SCWEBSITE"]);

                                    txtNewAmazon.Text = Convert.ToString(dt.Rows[0]["NEWAMAZON"]);
                                    txtCFURL.Text = Convert.ToString(dt.Rows[0]["CFURL"]);

                                    txtItemCode.Enabled = false;
                                    imgSaveAll.Text = "Update";
                                }
                                else
                                {

                                    dt = objMainClass.GetItemMappingData(objMainClass.intCmpId, Convert.ToString(Session["ITEMCODE"]), "", "", "", "", 1, "ITEMMASTERSEARCH", 0, 0);

                                    if (dt.Rows.Count > 0)
                                    {
                                        txtItemCode.Text = Convert.ToString(dt.Rows[0]["ITEMCODE"]);
                                        txtItemDesc.Text = Convert.ToString(dt.Rows[0]["ITEMDESC"]);
                                        imgSaveAll.Text = "Save";
                                    }
                                    else
                                    {
                                        imgSaveAll.Enabled = false;
                                        imgSaveAll.Text = "Save";
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found With This Item Code.!');", true);
                                        imgSaveAll.Enabled = false;
                                        imgSaveAll.Text = "Save";
                                    }

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
        }

        protected void imgSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (imgSaveAll.Text == "Save")
                    {
                        int iResult = objMainClass.InsertItemMapping(objMainClass.intCmpId, txtItemCode.Text, txtItemDesc.Text, txtSKU.Text, txtFlipakrt.Text, txtAmazon.Text, txtWebsite.Text, 1,
                             Convert.ToInt32(Session["USERID"]), "", "", "", "INSERTMAPPEDITEM", txtSCWebsite.Text, txtNewAmazon.Text, txtCFURL.Text);
                        if (iResult == 1)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Record Inserted successfully! \");$('.close').click(function(){window.location.href ='frmViewMappedItem.aspx' });", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Recort Not Inserted sucessfully!');", true);
                        }
                    }
                    else if (imgSaveAll.Text == "Update")
                    {
                        int iResult = objMainClass.InsertItemMapping(objMainClass.intCmpId, txtItemCode.Text, txtItemDesc.Text, txtSKU.Text, txtFlipakrt.Text, txtAmazon.Text, txtWebsite.Text, 1,
                            Convert.ToInt32(Session["USERID"]), "", "", "", "UPDATEITEMMAPPED", txtSCWebsite.Text, txtNewAmazon.Text, txtCFURL.Text);
                        if (iResult == 1)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Record Updated successfully! \");$('.close').click(function(){window.location.href ='frmViewMappedItem.aspx' });", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Recort Not Updated sucessfully!');", true);
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

        protected void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetItemMappingData(objMainClass.intCmpId, txtItemCode.Text, "", "", "", "", 1, "ITEMSEARCH", 0, 0);

                    if (dt.Rows.Count > 0)
                    {
                        txtItemCode.Text = Convert.ToString(dt.Rows[0]["ITEMCODE"]);
                        txtItemDesc.Text = Convert.ToString(dt.Rows[0]["ITEMDESC"]);
                        txtSKU.Text = Convert.ToString(dt.Rows[0]["SKU"]);
                        txtFlipakrt.Text = Convert.ToString(dt.Rows[0]["FLIPKART"]);
                        txtAmazon.Text = Convert.ToString(dt.Rows[0]["AMAZON"]);
                        txtWebsite.Text = Convert.ToString(dt.Rows[0]["WEBSITE"]);
                        txtItemCode.Enabled = false;
                        imgSaveAll.Text = "Update";
                    }
                    else
                    {

                        dt = objMainClass.GetItemMappingData(objMainClass.intCmpId, txtItemCode.Text, "", "", "", "", 1, "ITEMMASTERSEARCH", 0, 0);

                        if (dt.Rows.Count > 0)
                        {
                            txtItemCode.Text = Convert.ToString(dt.Rows[0]["ITEMCODE"]);
                            txtItemDesc.Text = Convert.ToString(dt.Rows[0]["ITEMDESC"]);
                            imgSaveAll.Text = "Save";
                        }
                        else
                        {
                            imgSaveAll.Enabled = false;
                            imgSaveAll.Text = "Save";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found With This Item Code.!');", true);
                            imgSaveAll.Enabled = false;
                            imgSaveAll.Text = "Save";
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

        protected void txtSKU_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (txtSKU.Text != "" && txtSKU.Text != string.Empty && txtSKU.Text != null)
                    {
                        rfvFlipkart.Enabled = false;
                        rfvAmazon.Enabled = false;
                        rfvWebsite.Enabled = false;
                        rfvSKU.Enabled = true;
                        rfvScWebsite.Enabled = false;
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

        protected void txtFlipakrt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (txtFlipakrt.Text != "" && txtFlipakrt.Text != string.Empty && txtFlipakrt.Text != null)
                    {
                        rfvFlipkart.Enabled = true;
                        rfvAmazon.Enabled = false;
                        rfvWebsite.Enabled = false;
                        rfvSKU.Enabled = false;
                        rfvScWebsite.Enabled = false;
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

        protected void txtAmazon_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (txtAmazon.Text != "" && txtAmazon.Text != string.Empty && txtAmazon.Text != null)
                    {
                        rfvFlipkart.Enabled = false;
                        rfvAmazon.Enabled = true;
                        rfvWebsite.Enabled = false;
                        rfvSKU.Enabled = false;
                        rfvScWebsite.Enabled = false;
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

        protected void txtWebsite_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (txtWebsite.Text != "" && txtWebsite.Text != string.Empty && txtWebsite.Text != null)
                    {
                        rfvFlipkart.Enabled = false;
                        rfvAmazon.Enabled = false;
                        rfvWebsite.Enabled = true;
                        rfvSKU.Enabled = false;
                        rfvScWebsite.Enabled = false;
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

                    string make = ddlMake.SelectedItem.Text;
                    string model = ddlModel.SelectedItem.Text;
                    string ram = ddlRAM.SelectedItem.Text == "NA" ? "" : ddlRAM.SelectedItem.Text + "GB ";
                    string rom = ddlROM.SelectedItem.Text + "GB ";
                    string color = ddlColor.SelectedItem.Text;
                    string grade = ddlGrade.SelectedItem.Text;

                    string itemcode = make + " " + model + " " + ram + "" + rom + "" + color + " MOBILE DEVICE (USED) (" + grade + ")";

                    if (itemcode != "" && itemcode != null && itemcode != string.Empty)
                    {
                        DataTable dt = new DataTable();
                        dt = objMainClass.GetItemDetails(objMainClass.intCmpId, itemcode, "SEARCHITEM");
                        if (dt.Rows.Count > 0)
                        {
                            gvList.DataSource = dt;
                            gvList.DataBind();
                        }
                        else
                        {
                            if (make.ToUpper() == "APPLE")
                            {
                                itemcode = make + " " + model + " 0GB " + rom + "" + color + " MOBILE DEVICE (USED) (" + grade + ")";
                                dt = objMainClass.GetItemDetails(objMainClass.intCmpId, itemcode, "SEARCHITEM");
                                if (dt.Rows.Count > 0)
                                {
                                    gvList.DataSource = dt;
                                    gvList.DataBind();
                                }
                                else
                                {
                                    itemcode = make + " " + model + " NA " + rom + "" + color + " MOBILE DEVICE (USED) (" + grade + ")";
                                    dt = objMainClass.GetItemDetails(objMainClass.intCmpId, itemcode, "SEARCHITEM");
                                    if (dt.Rows.Count > 0)
                                    {
                                        gvList.DataSource = dt;
                                        gvList.DataBind();
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

        protected void lnkSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    Label lblItemcode = (Label)grdrow.FindControl("lblItemcode");

                    txtItemCode.Text = lblItemcode.Text;
                    txtItemCode_TextChanged(1, e);

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

        protected void txtSCWebsite_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (txtSCWebsite.Text != "" && txtSCWebsite.Text != string.Empty && txtSCWebsite.Text != null)
                    {
                        rfvFlipkart.Enabled = false;
                        rfvAmazon.Enabled = false;
                        rfvWebsite.Enabled = false;
                        rfvScWebsite.Enabled = true;
                        rfvSKU.Enabled = false;
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