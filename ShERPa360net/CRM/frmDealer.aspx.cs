using Newtonsoft.Json;
using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.CRM
{
    public partial class frmDealer : System.Web.UI.Page
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
                            lnkSave.Enabled = false;
                        }
                        objBindDDL.FillCountry(1, ddlCountry);
                        objBindDDL.FillState(ddlState);
                        objBindDDL.FillLists(ddlCategory, "DC");
                        objBindDDL.FillArea(ddlArea);
                        objBindDDL.FillBiker(ddlBiker, "DEALERBIKER");
                        objBindDDL.FillLists(ddlmaxday, "LED");
                        ddlmaxday.SelectedValue = "0";
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

        protected void lnkNext1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    divAddress1.Visible = true;
                    divAddress2.Visible = true;
                    divAddress3.Visible = true;
                    divArea.Visible = true;
                    divNext2.Visible = true;

                    divName.Visible = false;
                    divCategory.Visible = false;
                    divContactNo.Visible = false;
                    divContactNo2.Visible = false;
                    divContactNo3.Visible = false;
                    divNext1.Visible = false;
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

        protected void lnkPrevious2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    divAddress1.Visible = false;
                    divAddress2.Visible = false;
                    divAddress3.Visible = false;
                    divArea.Visible = false;
                    divNext2.Visible = false;

                    divName.Visible = true;
                    divCategory.Visible = true;
                    divContactNo.Visible = true;
                    divContactNo2.Visible = true;
                    divContactNo3.Visible = true;
                    divNext1.Visible = true;
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

        protected void lnkNext2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    divAddress1.Visible = false;
                    divAddress2.Visible = false;
                    divAddress3.Visible = false;
                    divArea.Visible = false;
                    divNext2.Visible = false;

                    divPincode.Visible = true;
                    divState.Visible = true;
                    divCity.Visible = true;
                    divCountry.Visible = true;
                    divNext3.Visible = true;

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

        protected void txtPincode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (txtPincode.Text.Length == 6)
                    {
                        try
                        {
                            DataTable ds = new DataTable();
                            ds = objMainClass.SELECT_CITY_BYPINCODE(txtPincode.Text.Trim());
                            if (ds.Rows.Count > 0)
                            {
                                ddlState.SelectedValue = ds.Rows[0]["STATE_ID"].ToString();
                                txtCity.Text = ds.Rows[0]["CITY_NAME"].ToString();
                            }
                            else
                            {
                                ddlState.SelectedIndex = 0;
                                txtCity.Text = "";
                            }
                        }
                        catch (Exception ex)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
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

        protected void lnkPrevious3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    divAddress1.Visible = true;
                    divAddress2.Visible = true;
                    divAddress3.Visible = true;
                    divArea.Visible = true;
                    divNext2.Visible = true;

                    divPincode.Visible = false;
                    divState.Visible = false;
                    divCity.Visible = false;
                    divCountry.Visible = false;
                    divNext3.Visible = false;
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

        protected void lnkNext3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    divPincode.Visible = false;
                    divState.Visible = false;
                    divCity.Visible = false;
                    divCountry.Visible = false;
                    divNext3.Visible = false;

                    divShop.Visible = true;
                    dviFinal.Visible = true;
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

        protected void lnkPrevious7_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    divPincode.Visible = true;
                    divState.Visible = true;
                    divCity.Visible = true;
                    divCountry.Visible = true;
                    divNext3.Visible = true;

                    divShop.Visible = false;
                    dviFinal.Visible = false;
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

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    byte[] SHOP = null;

                    if (fuShopImage != null)
                    {
                        if (fuShopImage.HasFiles)
                        {
                            using (BinaryReader br = new BinaryReader(fuShopImage.PostedFile.InputStream))
                            {
                                SHOP = br.ReadBytes(fuShopImage.PostedFile.ContentLength);
                            }
                        }
                    }
                    int iResult = objMainClass.InsertDealer(objMainClass.intCmpId, txtName.Text, Convert.ToInt32(ddlCategory.SelectedValue), txtAddress1.Text, txtAddress2.Text, txtAddress3.Text, txtCity.Text,
                        Convert.ToInt32(ddlState.SelectedValue), ddlCountry.SelectedValue, txtPincode.Text, SHOP,
                        Convert.ToInt32(Session["USERID"]), "INSERT", 1, ddlArea.SelectedValue, Convert.ToInt32(ddlBiker.SelectedValue),txtContactNo.Text , txtContactNo2.Text, txtContactNo3.Text, "" , chkIsKro.Checked == true ? 1 : 0, Convert.ToInt32(ddlmaxday.SelectedItem.Text));

                    if (iResult == 1)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Dealer registered successfully! \");$('.close').click(function(){window.location.href ='frmViewDealer.aspx' });", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Dealer not registered sucessfully!');", true);
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

        protected void txtName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetDealer(objMainClass.intCmpId, "CHKDUPLICATE", txtName.Text, "", 0, "", "");
                    
                    if (dt.Rows.Count > 0)
                    {
                        //txtName.Focus();
                        txtContactNo.Focus();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Dealer name already exists.');", true);
                        lnkNext1.Enabled = false;
                    }
                    else
                    {
                       
                        lnkNext1.Enabled = true;
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

        //24-02-2023 swetal start

        [WebMethod]
        public static string GetSHOPNAME(string dealername)
        {
            string status1 = "false";
            try
            {
                MainClass objMain = new MainClass();
                var detail = objMain.GetShopName(dealername);
                if (detail != null && detail.Rows != null && detail.Rows.Count > 0)
                {
                    status1 = "true";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonConvert.SerializeObject(status1);
        }
        //end
    }
}