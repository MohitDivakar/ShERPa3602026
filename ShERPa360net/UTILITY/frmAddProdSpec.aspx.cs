using Newtonsoft.Json;
using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.UTILITY
{
    public partial class frmAddProdSpec : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    if (Session["USERID"] != null)
                    {
                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menutabid.Value, "");
                        if (FormRights.bView == false)
                        {
                            //Session.Abandon();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }

                        if (FormRights.bAdd == false)
                        {
                            btnAdd.Enabled   = false;
                            btnReset.Enabled = false;
                        }

                        if (Request.QueryString.Count > 0)
                        {
                            if (Convert.ToString(Request.QueryString["ID"]) != null && Convert.ToString(Request.QueryString["ID"]) != string.Empty && Convert.ToString(Request.QueryString["ID"]) != "")
                            {
                                Session["EditBrandID"] = Convert.ToString(Request.QueryString["ID"]);

                            }
                            Response.Redirect(Request.Url.AbsolutePath, false);
                        }
                        else
                        {
                            BindPageDropDown();

                            if (Session["EditBrandID"] != null && Convert.ToString(Session["EditBrandID"]) != "" && Convert.ToString(Session["EditBrandID"]) != string.Empty)
                            {
                                DataTable dt = new DataTable();
                                dt = objMainClass.GetProdSpec(Convert.ToInt32(Session["EditBrandID"]), 0, 0, "SEARCH",Convert.ToInt32(ddlItemType.SelectedValue),0,0, "ALL","");
                                if (dt.Rows.Count > 0)
                                {
                                    ddlItemType.SelectedValue = Convert.ToString(dt.Rows[0]["ITEMTYPEID"]);
                                    ddlMake.SelectedValue = Convert.ToString(dt.Rows[0]["BRAND_ID"]);
                                    ddlMake_SelectedIndexChanged(1, e);
                                    ddlModel.SelectedValue = Convert.ToString(dt.Rows[0]["MODEL_ID"]);
                                    //BindPageDropDown();
                                    ddlRam.SelectedValue = Convert.ToString(dt.Rows[0]["RAMSIZE"]);
                                    ddlRom.SelectedValue = Convert.ToString(dt.Rows[0]["ROMSIZE"]);
                                    ddlColor.SelectedValue =  Convert.ToString(dt.Rows[0]["COLOR"]).ToUpper();
                                    txtNewRate.Text     = Convert.ToString(dt.Rows[0]["NEWRATE"]);
                                    hdNewRate.Value     = Convert.ToString(dt.Rows[0]["NEWRATE"]);
                                    txtBasiPurRate.Text = Convert.ToString(dt.Rows[0]["BASICPURRATE"]);
                                    txtLaunchYear.Text  = Convert.ToString(dt.Rows[0]["LAUNCHYEAR"]);
                                    hdBasicPrice.Value  = Convert.ToString(dt.Rows[0]["BASICPURRATE"]);
                                    hdLockAmount.Value = Convert.ToString(dt.Rows[0]["FinalApproveListingAmount"]);
                                    txtFinalStockPrice.Text = Convert.ToString(dt.Rows[0]["FinalStockApproveAmount"]);

                                    if (Convert.ToString(dt.Rows[0]["ACTIVE"]) == "1")
                                    {
                                        chkActive.Checked = true;
                                    }
                                    else
                                    {
                                        chkActive.Checked = false;
                                    }

                                    btnAdd.Text = "Update";

                                    //Extra field 
                                    ddlItemGroup.SelectedValue    = Convert.ToString(dt.Rows[0]["ITEMGRPID"]);
                                    ddlItemSubGroup.SelectedValue = Convert.ToString(dt.Rows[0]["ITEMSUBGRPID"]);
                                    txtModelDisplay.Text          = Convert.ToString(dt.Rows[0]["MODELDIPLAYNAME"]);
                                    txtFinalAmount.Text           = Convert.ToString(dt.Rows[0]["FinalApproveListingAmount"]);
                                    txtMntTopSrNo.Text            = Convert.ToString(dt.Rows[0]["MNTTOPSALESSRNO"]);

                                    if (Convert.ToString(dt.Rows[0]["ISINSTANTSELLING"]) == "1")
                                    {
                                        chkInstantSelling.Checked = true;
                                    }
                                    else
                                    {
                                        chkInstantSelling.Checked = false;
                                    }

                                    txtInstantAmount.Text = Convert.ToString(dt.Rows[0]["INSTANTSELLINGAMOUNT"]);
                                    
                                    ShowHideSubGroupSpec(Convert.ToInt32(dt.Rows[0]["ITEMSUBGRPID"]));
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No record found !');$('.close').click(function(){window.location.href ='frmViewProdSpec.aspx' });", true);
                                }   
                            }
                        }
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

        public void ShowHideSubGroupSpec(int subGroupId)
        {
            try
            {
                if (subGroupId == 0)
                {
                    dvModel.Visible = false;
                    dvModelDisplay.Visible = false;
                    dvRam.Visible = false;
                    dvRom.Visible = false;
                    dvColor.Visible = false;
                    dvActive.Visible = false;
                    dvNewRate.Visible = false;
                    dvSuggestRate.Visible = false;
                    dvLaunchYear.Visible = false;
                    dvFinalAmount.Visible = false;
                    dvMntTopSrNo.Visible  = false;
                    dvIsInstantSelling.Visible = false;
                    dvInstantSellingAmount.Visible = false;
                    dvStockFinalPrice.Visible = false;
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GETITEMSUBGRP_SPECDETAIL(subGroupId);
                    if (dt.Rows.Count > 0)
                    {
                        if(Convert.ToInt32(dt.Rows[0]["ITEMSHOWMODEL"]) == 1)
                        {
                            dvModel.Visible = true;
                        }
                        else
                        {
                            dvModel.Visible = false;
                        }

                        if (Convert.ToInt32(dt.Rows[0]["ITEMSHOWRAM"]) == 1)
                        {
                            dvRam.Visible = true;
                        }
                        else
                        {
                            dvRam.Visible = false;
                        }

                        if (Convert.ToInt32(dt.Rows[0]["ITEMSHOWROM"]) == 1)
                        {
                            dvRom.Visible = true;
                        }
                        else
                        {
                            dvRom.Visible = false;
                        }

                        if (Convert.ToInt32(dt.Rows[0]["ITEMSHOWCOLOR"]) == 1)
                        {
                            dvColor.Visible = true;
                        }
                        else
                        {
                            dvColor.Visible = false;
                        }

                        if (Convert.ToInt32(dt.Rows[0]["ITEMSHOWMODELTODISPLAY"]) == 1)
                        {
                            dvModelDisplay.Visible = true;
                        }
                        else
                        {
                            dvModelDisplay.Visible = false;
                        }

                        dvActive.Visible      = true;
                        dvNewRate.Visible     = true;
                        dvSuggestRate.Visible = true;
                        dvLaunchYear.Visible  = true;
                        dvFinalAmount.Visible = true;
                        dvMntTopSrNo.Visible  = true;
                        dvStockFinalPrice.Visible = true;

                        dvIsInstantSelling.Visible = true;
                        dvInstantSellingAmount.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public void ResetFormControl()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    ddlItemType.SelectedValue = "0";
                    ddlMake.SelectedValue = "0";
                    ddlModel.DataSource = string.Empty;
                    ddlModel.DataBind();
                    //ddlModel.SelectedValue = "0";
                    ddlRom.SelectedValue   = "0";
                    ddlRam.SelectedValue   = "0";
                    ddlColor.SelectedValue = "0";
                    ddlItemGroup.SelectedValue = "0";
                    ddlItemSubGroup.SelectedValue = "0";
                    txtNewRate.Text        = string.Empty;
                    txtBasiPurRate.Text    = string.Empty;
                    txtLaunchYear.Text     = string.Empty;
                    hdBasicPrice.Value     = "0";
                    txtFinalAmount.Text = "0";
                    hdLockAmount.Value = "0";
                    txtMntTopSrNo.Text = string.Empty;
                    chkInstantSelling.Checked = false;
                    txtInstantAmount.Text = string.Empty;
                    txtFinalStockPrice.Text = string.Empty;
                    ddlMake.Focus();
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

        public void BindPageDropDown()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    objBindDDL.FillItemType(ddlItemType);
                    ddlItemType.SelectedValue = "0";

                    objBindDDL.FillBrand(ddlMake,1);
                    ddlMake.SelectedValue = "0";

                    objBindDDL.FillAddProductLists(ddlRom, "ROM");
                    ddlRom.SelectedValue = "0";

                    objBindDDL.FillAddProductLists(ddlRam, "RAM");
                    ddlRam.SelectedValue = "0";

                    objBindDDL.FillAddProductLists(ddlColor, "CL");
                    ddlColor.SelectedValue = "0";

                    objBindDDL.FillModelSpecDropDown(ddlItemGroup, "ITEMGROUP");
                    ddlItemGroup.SelectedValue = "0";

                    objBindDDL.FillModelSpecDropDown(ddlItemSubGroup, "ITEMSUBGROUP");
                    ddlItemSubGroup.SelectedValue = "0";
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    //InsertProdSpec

                    if (btnAdd.Text == "Add")
                    {
                        Decimal newrate = 0;
                        Decimal basicpurrate = 0;
                        Decimal instantsellingamt = 0;
                        Decimal finalstockamount = 0;

                        int maxsrno = 0;

                        Decimal.TryParse(txtNewRate.Text, out newrate);
                        Decimal.TryParse(txtBasiPurRate.Text, out basicpurrate);
                        Decimal.TryParse(txtInstantAmount.Text, out instantsellingamt);
                        Decimal.TryParse(txtFinalStockPrice.Text, out finalstockamount);


                        int.TryParse(txtMntTopSrNo.Text, out maxsrno);


                        int iResult = objMainClass.InsertProdSpec(Convert.ToInt32(ddlMake.SelectedValue), ddlMake.SelectedItem.Text, Convert.ToInt32(ddlModel.SelectedValue), ddlModel.SelectedItem.Text, ddlRam.SelectedItem.Text,
                            ddlRom.SelectedItem.Text, ddlColor.SelectedItem.Text,
                            chkActive.Checked == true ? 1 : 0, Convert.ToInt32(Session["USERID"]), "INSERT", newrate, basicpurrate
                            , txtLaunchYear.Text, Convert.ToInt32(ddlItemType.SelectedValue), Convert.ToInt32(ddlItemGroup.SelectedValue)
                            , Convert.ToInt32(ddlItemSubGroup.SelectedValue), txtModelDisplay.Text, Convert.ToDecimal(txtFinalAmount.Text)
                            , maxsrno,chkInstantSelling.Checked == true ? 1 : 0 , instantsellingamt, finalstockamount);

                        if (iResult == 1)
                        {
                            string make = "";
                            string model = "";

                            make = ddlMake.SelectedValue;
                            model = ddlModel.SelectedValue;
                            string url = "frmViewProdSpec.aspx?" + "Make=" + make + "&Model=" + model;
                            Response.Redirect(url);
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Record saved sucessfully.\");$('.close').click(function(){window.location.href ='frmViewProdSpec.aspx'?Make=" + make + "&Model=" + model + "});", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Record not inserted sucessfully!');", true);
                        }
                    }
                    else if (btnAdd.Text == "Update")
                    {
                        Decimal newrate = 0;
                        Decimal basicpurrate = 0;
                        Decimal instantsellingamt = 0;
                        Decimal finalstockamount = 0;
                        int maxsrno = 0;
                        int.TryParse(txtMntTopSrNo.Text, out maxsrno);

                        Decimal.TryParse(txtNewRate.Text, out newrate);
                        Decimal.TryParse(txtBasiPurRate.Text, out basicpurrate);
                        Decimal.TryParse(txtInstantAmount.Text, out instantsellingamt);
                        Decimal.TryParse(txtFinalStockPrice.Text, out finalstockamount);

                        int iResult = objMainClass.UpdateProdSpec(Convert.ToInt32(Session["EditBrandID"]), Convert.ToInt32(ddlMake.SelectedValue), ddlMake.SelectedItem.Text, Convert.ToInt32(ddlModel.SelectedValue), ddlModel.SelectedItem.Text, ddlRam.SelectedItem.Text,
                           ddlRom.SelectedItem.Text, ddlColor.SelectedItem.Text,
                           chkActive.Checked == true ? 1 : 0, Convert.ToInt32(Session["USERID"]), "UPDATE", newrate, basicpurrate
                           , txtLaunchYear.Text, Convert.ToInt32(ddlItemType.SelectedValue)
                            , Convert.ToInt32(ddlItemGroup.SelectedValue)
                            , Convert.ToInt32(ddlItemSubGroup.SelectedValue), txtModelDisplay.Text, Convert.ToDecimal(txtFinalAmount.Text)
                            , maxsrno
                            , chkInstantSelling.Checked == true ? 1 : 0, instantsellingamt, finalstockamount);

                        if (iResult == 1)
                        {
                            string make = "";
                            string model = "";
                            make = ddlMake.SelectedValue;
                            model = ddlModel.SelectedValue;
                            string url = "";
                            if (hdBasicPrice.Value == txtBasiPurRate.Text && hdNewRate.Value == txtNewRate.Text && hdLockAmount.Value == txtFinalAmount.Text)
                            {
                                url = "frmViewProdSpec.aspx?" + "Make=" + make + "&Model=" + model;
                            }
                            else
                            {
                                url = "PriceApproveDetail.aspx";
                            }
                            Response.Redirect(url);
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Record saved sucessfully.\");$('.close').click(function(){window.location.href ='frmViewProdSpec.aspx'?Make=" + make + "&Model=" + model + "});", true);
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Record saved sucessfully.\");$('.close').click(function(){window.location.href ='frmViewProdSpec.aspx' });", true);
                            Session["EditBrandID"] = null;
                            Session["EditBrandID"] = string.Empty;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Record not inserted sucessfully!');", true);
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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                ResetFormControl();
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
                objBindDDL.FillMakeModel(ddlModel, ddlMake.SelectedValue);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void ddlItemSubGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ShowHideSubGroupSpec(Convert.ToInt32(ddlItemSubGroup.SelectedValue));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        [WebMethod]
        public static string GetEachProdSpecPrimaryDetail(string brand, string model, string ram , string rom)
        {
            try
            {
                MainClass objMain = new MainClass();
                var dtDetail      = objMain.GetEachProdSpecPrimaryDetail(brand, model, ram, rom,9,168);
                ProductSpecificationPrimaryDetail objeachSpecPrimary = new ProductSpecificationPrimaryDetail();
                foreach (DataRow row in dtDetail.Rows)
                {
                    objeachSpecPrimary.BRAND_ID                     = Convert.ToInt32(row["BRAND_ID"].ToString());
                    objeachSpecPrimary.BRAND_DESC                   = row["BRAND_DESC"].ToString();
                    objeachSpecPrimary.MODEL_ID                     = Convert.ToInt32(row["MODEL_ID"].ToString());
                    objeachSpecPrimary.MODEL_NAME                   = row["MODEL_NAME"].ToString();
                    objeachSpecPrimary.RAMSIZE                      = row["RAMSIZE"].ToString();
                    objeachSpecPrimary.ROMSIZE                      = row["ROMSIZE"].ToString();
                    objeachSpecPrimary.COLOR                        = row["COLOR"].ToString();
                    objeachSpecPrimary.LAUNCHYEAR                   = row["LAUNCHYEAR"].ToString();
                    objeachSpecPrimary.NEWRATE                      = Convert.ToDecimal(row["NEWRATE"].ToString());
                    objeachSpecPrimary.BASICPURRATE                 = Convert.ToDecimal(row["BASICPURRATE"].ToString());
                    objeachSpecPrimary.BASICPURRATEFORBGRADE        = Convert.ToDecimal(row["BASICPURRATEFORBGRADE"].ToString());
                    objeachSpecPrimary.BASICPURRATEFORCGRADE        = Convert.ToDecimal(row["BASICPURRATEFORCGRADE"].ToString());
                    objeachSpecPrimary.FinalApproveListingAmount    = Convert.ToDecimal(row["FinalApproveListingAmount"].ToString());
                }
                return JsonConvert.SerializeObject(objeachSpecPrimary);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public static string GetEachProdSpecalreadyExist(string brand, string model, string ram, string rom,string color)
        {
            try
            {
                MainClass objMain = new MainClass();
                var dtDetail = objMain.GetEachProdSpecalreadyExist(brand, model, ram, rom, color, 9, 168);
                ProductSpecificationPrimaryDetail objeachSpecPrimary = new ProductSpecificationPrimaryDetail();
                foreach (DataRow row in dtDetail.Rows)
                {
                    objeachSpecPrimary.BRAND_ID = Convert.ToInt32(row["BRAND_ID"].ToString());
                    objeachSpecPrimary.BRAND_DESC = row["BRAND_DESC"].ToString();
                    objeachSpecPrimary.MODEL_ID = Convert.ToInt32(row["MODEL_ID"].ToString());
                    objeachSpecPrimary.MODEL_NAME = row["MODEL_NAME"].ToString();
                    objeachSpecPrimary.RAMSIZE = row["RAMSIZE"].ToString();
                    objeachSpecPrimary.ROMSIZE = row["ROMSIZE"].ToString();
                    objeachSpecPrimary.COLOR = row["COLOR"].ToString();
                    objeachSpecPrimary.LAUNCHYEAR = row["LAUNCHYEAR"].ToString();
                    objeachSpecPrimary.NEWRATE = Convert.ToDecimal(row["NEWRATE"].ToString());
                    objeachSpecPrimary.BASICPURRATE = Convert.ToDecimal(row["BASICPURRATE"].ToString());
                    objeachSpecPrimary.BASICPURRATEFORBGRADE = Convert.ToDecimal(row["BASICPURRATEFORBGRADE"].ToString());
                    objeachSpecPrimary.BASICPURRATEFORCGRADE = Convert.ToDecimal(row["BASICPURRATEFORCGRADE"].ToString());
                    objeachSpecPrimary.FinalApproveListingAmount = Convert.ToDecimal(row["FinalApproveListingAmount"].ToString());
                }
                return JsonConvert.SerializeObject(objeachSpecPrimary);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public static string GetEachProdLaunchDetail(string brand, string model)
        {
            try
            {
                MainClass objMain = new MainClass();
                var dtDetail = objMain.GetEachProdLaunchDetail(brand, model, 9, 168);
                ProductSpecificationPrimaryDetail objeachSpecPrimary = new ProductSpecificationPrimaryDetail();
                foreach (DataRow row in dtDetail.Rows)
                {
                    objeachSpecPrimary.BRAND_ID = Convert.ToInt32(row["BRAND_ID"].ToString());
                    objeachSpecPrimary.BRAND_DESC = row["BRAND_DESC"].ToString();
                    objeachSpecPrimary.MODEL_ID = Convert.ToInt32(row["MODEL_ID"].ToString());
                    objeachSpecPrimary.MODEL_NAME = row["MODEL_NAME"].ToString();
                    objeachSpecPrimary.RAMSIZE = row["RAMSIZE"].ToString();
                    objeachSpecPrimary.ROMSIZE = row["ROMSIZE"].ToString();
                    objeachSpecPrimary.COLOR = row["COLOR"].ToString();
                    objeachSpecPrimary.LAUNCHYEAR = row["LAUNCHYEAR"].ToString();
                    objeachSpecPrimary.NEWRATE = Convert.ToDecimal(row["NEWRATE"].ToString());
                    objeachSpecPrimary.BASICPURRATE = Convert.ToDecimal(row["BASICPURRATE"].ToString());
                    objeachSpecPrimary.BASICPURRATEFORBGRADE = Convert.ToDecimal(row["BASICPURRATEFORBGRADE"].ToString());
                    objeachSpecPrimary.BASICPURRATEFORCGRADE = Convert.ToDecimal(row["BASICPURRATEFORCGRADE"].ToString());
                    objeachSpecPrimary.FinalApproveListingAmount = Convert.ToDecimal(row["FinalApproveListingAmount"].ToString());
                }
                return JsonConvert.SerializeObject(objeachSpecPrimary);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [WebMethod]
        public static ChartData GetChartData()
        {
            try
            {
                // Get the data from database.
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[] {
                new DataColumn("Month"),new DataColumn("Motorcycles"),new DataColumn("Bicycles") });
                dt.Rows.Add("January", 30, 65);
                dt.Rows.Add("February", 50, 60);
                dt.Rows.Add("March", 40, 81);
                dt.Rows.Add("April", 20, 80);
                dt.Rows.Add("May", 80, 60);
                dt.Rows.Add("June", 30, 60);

                ChartData chartData = new ChartData();
                chartData.Labels = dt.AsEnumerable().Select(x => x.Field<string>("Month")).ToArray();
                chartData.DatasetLabels = new string[] { "Motorcycles", "Bicycles" };
                List<int[]> datasetDatas = new List<int[]>();

                List<int> motorcycles = new List<int>();
                List<int> bicycles = new List<int>();
                foreach (DataRow dr in dt.Rows)
                {
                    motorcycles.Add(Convert.ToInt32(dr["Motorcycles"]));
                    bicycles.Add(Convert.ToInt32(dr["Bicycles"]));
                }

                datasetDatas.Add(motorcycles.ToArray());
                datasetDatas.Add(bicycles.ToArray());
                chartData.DatasetDatas = datasetDatas;
                return chartData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}



