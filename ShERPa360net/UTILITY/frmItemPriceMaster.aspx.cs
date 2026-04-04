using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net
{
    public partial class frmItemPriceMaster : System.Web.UI.Page
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

                        if (FormRights.bAdd == false)
                        {
                            btnSaveAll.Enabled = false;
                        }


                        objBindDDL.FillBrand(ddlMake, 1);
                        ddlMake.SelectedValue = "0";

                        objBindDDL.FillLists(ddlRam, "RAM");
                        objBindDDL.FillLists(ddlRom, "ROM");
                        objBindDDL.FillLists(ddlColor, "CL");

                        //ddlModel.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                        //ddlModel.SelectedValue = "0";

                        //ddlRam.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                        //ddlRam.SelectedValue = "0";

                        //ddlRom.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                        //ddlRom.SelectedValue = "0";

                        //ddlColor.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                        //ddlColor.SelectedValue = "0";

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

        protected void lnkSerch_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    string itemcode = string.Empty;

                    if (txtSearchItemDesc.Text != "" && txtSearchItemDesc.Text != null && txtSearchItemDesc.Text != string.Empty)
                    {
                        if (txtSearchItemDesc.Text.ToString().Contains("-"))
                        {

                            itemcode = txtSearchItemDesc.Text.Split('-')[0].Trim().ToString();

                        }
                        else
                        {
                            itemcode = txtSearchItemDesc.Text;
                        }

                        DataTable dt = new DataTable();
                        DataTable dtPrice = new DataTable();
                        dt = objMainClass.GetItemDetailsByItemCode(objMainClass.intCmpId, itemcode, 1, "ITEMMASTERSEARCH");
                        if (dt.Rows.Count > 0)
                        {
                            gvList.DataSource = dt;
                            gvList.DataBind();
                        }
                        else
                        {
                            gvList.DataSource = string.Empty;
                            gvList.DataBind();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Item Not Found.!');", true);
                        }


                    }
                    else
                    {

                        string make = string.Empty;
                        string model = string.Empty;
                        string ram = string.Empty;
                        string rom = string.Empty;
                        string color = string.Empty;
                        string grade = string.Empty;

                        if (Convert.ToInt32(ddlMake.SelectedValue) > 0)
                        {
                            make = ddlMake.SelectedItem.Text;
                        }
                        else
                        {
                            make = "%%";
                        }
                        if (Convert.ToInt32(ddlModel.SelectedValue) > 0)
                        {
                            model = ddlModel.SelectedItem.Text;
                        }
                        else
                        {
                            model = "%%";
                        }
                        if (Convert.ToInt32(ddlRam.SelectedItem.Text == "NA" ? 0 : Convert.ToInt32(ddlRam.SelectedIndex)) > 0)
                        {
                            ram = ddlRam.SelectedItem.Text == "NA" ? "" : ddlRam.SelectedItem.Text + "GB ";
                        }
                        else
                        {
                            ram = "%%";
                        }
                        if (Convert.ToInt32(ddlRom.SelectedIndex) > 0)
                        {
                            rom = ddlRom.SelectedItem.Text + "GB ";
                        }
                        else
                        {
                            rom = "%%";
                        }
                        if (Convert.ToInt32(ddlColor.SelectedIndex) > 0)
                        {
                            color = ddlColor.SelectedItem.Text;
                        }
                        else
                        {
                            color = "%%";
                        }
                        if (Convert.ToInt32(ddlGrade.SelectedIndex) > 0)
                        {
                            grade = ddlGrade.SelectedItem.Text;
                        }
                        else
                        {
                            grade = "%%";
                        }



                        itemcode = make + " " + model + " " + ram + "" + rom + "" + color + " MOBILE DEVICE (USED) (" + grade + ")";

                        if (itemcode != "" && itemcode != null && itemcode != string.Empty)
                        {
                            DataTable dt = new DataTable();
                            DataTable dtPrice = new DataTable();
                            dt = objMainClass.GetItemDetails(objMainClass.intCmpId, itemcode, "SEARCHITEMMRP");
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
                                    dt = objMainClass.GetItemDetails(objMainClass.intCmpId, itemcode, "SEARCHITEMMRP");
                                    if (dt.Rows.Count > 0)
                                    {
                                        gvList.DataSource = dt;
                                        gvList.DataBind();
                                    }
                                    else
                                    {
                                        itemcode = make + " " + model + " NA " + rom + "" + color + " MOBILE DEVICE (USED) (" + grade + ")";
                                        dt = objMainClass.GetItemDetails(objMainClass.intCmpId, itemcode, "SEARCHITEMMRP");
                                        if (dt.Rows.Count > 0)
                                        {
                                            gvList.DataSource = dt;
                                            gvList.DataBind();
                                        }
                                        else
                                        {
                                            gvList.DataSource = string.Empty;
                                            gvList.DataBind();
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Item Not Found.!');", true);
                                        }
                                    }
                                }
                                else
                                {
                                    gvList.DataSource = string.Empty;
                                    gvList.DataBind();
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Item Not Found.!');", true);
                                }

                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('" + itemcode + " Item Desc. is invalid.!');", true);
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

        protected void btnSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    foreach (GridViewRow row in gvList.Rows)
                    {
                        int ITEMID = Convert.ToInt32(row.Cells[0].Text);
                        string amt = (row.FindControl("txtNewAmount") as TextBox).Text;
                        if (amt == null || amt == string.Empty || amt == "")
                        {
                            amt = row.Cells[3].Text;
                        }
                        //int status = 0;
                        //CheckBox chk = row.FindControl("chkStatus") as CheckBox;
                        //if (chk.Checked == true)
                        //{
                        //    status = 1;
                        //}
                        //else
                        //{
                        //    status = 0;
                        //}

                        //int iResult = objMainClass.UpdatePartPrice(ITEMID, amt, status);

                        int iResult = objMainClass.UpdateItemPrice(objMainClass.intCmpId, ITEMID, amt, Convert.ToInt32(Session["USERID"]), "ITEMPRICEUPDATE");

                        if (iResult != 1)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Something went wrong!');", true);
                            lnkSerch_Click(1, e);
                            return;
                        }
                    }
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Record Saved Successfully!');", true);
                    lnkSerch_Click(1, e);
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
                    GridViewRow grdrow = (GridViewRow)((Button)sender).NamingContainer;
                    int ITEMID = Convert.ToInt32(grdrow.Cells[0].Text);
                    string amt = (grdrow.FindControl("txtNewAmount") as TextBox).Text;
                    if (amt == null || amt == string.Empty || amt == "")
                    {
                        amt = grdrow.Cells[3].Text;
                    }
                    //int status = 0;
                    //CheckBox chk = grdrow.FindControl("chkStatus") as CheckBox;
                    //if (chk.Checked == true)
                    //{
                    //    status = 1;
                    //}
                    //else
                    //{
                    //    status = 0;
                    //}

                    int iResult = objMainClass.UpdateItemPrice(objMainClass.intCmpId, ITEMID, amt, Convert.ToInt32(Session["USERID"]), "ITEMPRICEUPDATE");

                    if (iResult == 1)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Record Saved Successfully!');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Something went wrong!');", true);
                    }
                    lnkSerch_Click(1, e);
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

        protected void ddlModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    //BindMakeorModelAssociateDetail("RamRom");
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

        protected void ddlRAM_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    //BindMakeorModelAssociateDetail("Color");
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

        protected void ddlROM_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    //BindMakeorModelAssociateDetail("Color");
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

        protected void ddlColor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlGrade_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    BindMakeorModelAssociateDetail("Model");
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

        public void BindMakeorModelAssociateDetail(string reqdropdown)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (reqdropdown == "Model")
                    {
                        if (ddlMake.SelectedValue != "0")
                        {
                            ddlModel.Items.Clear();


                            objBindDDL.FillModel(ddlModel, ddlMake.SelectedValue);

                            //ddlRam.Items.Clear();
                            //ddlRom.Items.Clear();
                            //ddlColor.Items.Clear();

                            //ddlRam.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                            //ddlRam.SelectedValue = "0";

                            //ddlRom.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                            //ddlRom.SelectedValue = "0";

                            //ddlColor.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                            //ddlColor.SelectedValue = "0";
                        }
                        else
                        {
                            ddlModel.Items.Clear();
                            //ddlRam.Items.Clear();
                            //ddlRom.Items.Clear();
                            //ddlColor.Items.Clear();

                            ddlModel.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                            ddlModel.SelectedValue = "0";

                            //ddlRam.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                            //ddlRam.SelectedValue = "0";

                            //ddlRom.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                            //ddlRom.SelectedValue = "0";

                            //ddlColor.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                            //ddlColor.SelectedValue = "0";
                        }
                    }
                    else if (reqdropdown == "RamRom")
                    {
                        if (ddlModel.SelectedValue != "0")
                        {
                            ddlRam.Items.Clear();
                            ddlRom.Items.Clear();


                            if (ddlMake.SelectedItem.Text.ToUpper() == "APPLE")
                            {
                                ddlRam.Items.Insert(0, new ListItem("NA", "NA"));
                                ddlRam.SelectedValue = "NA";
                            }
                            else
                            {
                                //objBindDDL.FillMobexSellerRam(ddlRam, Convert.ToInt32(ddlMake.SelectedValue),
                                //Convert.ToInt32(ddlModel.SelectedValue));
                                objBindDDL.FillLists(ddlRam, "RAM");
                            }

                            //objBindDDL.FillMobexSellerRom(ddlRom, Convert.ToInt32(ddlMake.SelectedValue),
                            //            Convert.ToInt32(ddlModel.SelectedValue));
                            objBindDDL.FillLists(ddlRom, "ROM");

                            ddlColor.Items.Clear();
                            ddlColor.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                            ddlColor.SelectedValue = "0";
                        }
                        else
                        {
                            ddlRam.Items.Clear();
                            ddlRom.Items.Clear();
                            ddlColor.Items.Clear();

                            ddlRam.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                            ddlRam.SelectedValue = "0";

                            ddlRom.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                            ddlRom.SelectedValue = "0";

                            ddlColor.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                            ddlColor.SelectedValue = "0";
                        }
                    }
                    else if (reqdropdown == "Color")
                    {
                        //if (ddlRam.SelectedValue != "0" || ddlRom.SelectedValue != "0")
                        //{
                            ddlColor.Items.Clear();

                            //objBindDDL.FillMobexSellerColor(ddlColor, Convert.ToInt32(ddlMake.SelectedValue),
                            //           Convert.ToInt32(ddlModel.SelectedValue), ddlRam.SelectedItem.Text, ddlRom.SelectedItem.Text);
                            objBindDDL.FillLists(ddlColor, "COLOR");
                        //}
                        //else
                        //{
                        //    ddlColor.Items.Clear();

                        //    ddlColor.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                        //    ddlColor.SelectedValue = "0";
                        //}
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

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetItemCode(string prefixText, int count)
        {
            List<string> ItemCode = new List<string>();

            MainClass objMainClass = new MainClass();
            ItemCode = objMainClass.GetItemDetailsList(objMainClass.intCmpId, prefixText, "ITEMDESCSEARCHBYDESC");  //objMainClass.GetItemData(prefixText);

            return ItemCode;

        }

    }
}