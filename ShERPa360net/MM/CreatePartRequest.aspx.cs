using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.MM
{
    public partial class CreatePartRequest : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        DataTable dtItem = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["EditPRNo"] = null;
            Session["ReqNo"] = null;

            if (!IsPostBack)
            {
                try
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
                            lnkSaveAll.Enabled = false;
                        }

                        objBindDDL.FillUOM(ddlUOM);
                        objBindDDL.FillPlant(ddlPLant);
                        ddlPLant.SelectedIndex = 1;
                        objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);
                        //ddlLocation.SelectedValue = "MS01";
                        if (ddlPLant.SelectedValue == "1001")
                        {
                            ddlPLant.SelectedValue = "M001";
                        }
                        else
                        {
                            ddlPLant.SelectedValue = "MS01";
                        }
                        ddlUOM.SelectedIndex = 0;
                        objBindDDL.Fill_EMP_Enginner(ddlReqBy, 22, 1);
                        if (Convert.ToString(Session["EMPID"]) != string.Empty && Convert.ToString(Session["EMPID"]) != "")
                        {
                            ddlReqBy.SelectedValue = Convert.ToString(Session["EMPID"]);
                        }
                        else
                        {
                            ddlReqBy.SelectedIndex = 0;
                        }

                        objBindDDL.FillItemCat(ddlpopCategory);
                        objBindDDL.FillItemGrp(ddlpopGroup);
                        objBindDDL.FillItemSubGrp(ddlpopSubGroup);
                        objBindDDL.FillBrand(ddlpopMake, 0);


                        objBindDDL.FillItemCat(ddlNewCategory);
                        ddlNewCategory.SelectedValue = "2";
                        objBindDDL.FillItemGrp(ddlNewGroup);
                        ddlNewGroup.SelectedValue = "3";
                        objBindDDL.FillItemSubGrp(ddlNewSubGroup);
                        ddlNewSubGroup.SelectedValue = "83";

                        SetUpGrid();
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


        private void SetUpGrid()
        {
            try
            {

                DataColumn dtColumn;

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ID";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "JOBID";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "DOCDT";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMID";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "UOM";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "QTY";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "PLANTCD";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "LOCCD";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "REQBY";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "STATUS";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "CREATEBY";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "REQBYNAME";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMDESC";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "UOMNAME";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMCODE";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "SEGMENT";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "SEGMENTDESC";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "JOBSTATUS";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "SKU";
                dtItem.Columns.Add(dtColumn);

                ViewState["ItemData"] = dtItem;

                grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                grvListItem.DataBind();




            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }



        protected void ddlPLant_SelectedIndexChanged(object sender, EventArgs e)
        {
            objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);

            try
            {
                if (Session["USERID"] != null)
                {
                    string PLantRights = string.Empty;
                    DataTable dtS = new DataTable();
                    dtS = objMainClass.SelectUserSegment(Convert.ToString(Session["USERID"]));
                    if (dtS.Rows.Count > 0)
                    {
                        string[] plantArray = Convert.ToString(dtS.Rows[0]["PLANTCD"]).Split(',');
                        for (int i = 0; i < plantArray.Count(); i++)
                        {
                            if (plantArray[i].Trim() == ddlPLant.SelectedValue)
                            {
                                PLantRights = ddlPLant.SelectedValue;
                            }
                        }

                        if (PLantRights.Length > 0)
                        {
                            return;
                        }
                        else
                        {
                            ddlPLant.SelectedValue = "1001";
                            ddlPLant.Focus();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have plant rights.');$('#txtJobId').focus();", true);
                            ddlPLant.SelectedValue = "1001";
                            ddlPLant.Focus();
                            objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);
                            //ddlLocation.SelectedValue = "MS01";
                            if (ddlPLant.SelectedValue == "1001")
                            {
                                ddlLocation.SelectedValue = "M001";
                            }
                            else
                            {
                                ddlLocation.SelectedValue = "MS01";
                            }
                        }
                    }
                    else
                    {
                        ddlPLant.SelectedValue = "1001";
                        ddlPLant.Focus();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have plant rights.');$('#txtJobId').focus();", true);
                        ddlPLant.SelectedValue = "1001";
                        ddlPLant.Focus();
                        objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);
                        //ddlLocation.SelectedValue = "MS01";
                        if (ddlPLant.SelectedValue == "1001")
                        {
                            ddlLocation.SelectedValue = "M001";
                        }
                        else
                        {
                            ddlLocation.SelectedValue = "MS01";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }

        }

        protected void ddlUOM_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lblSKU.InnerText != null)
                {
                    if (ddlUOM.SelectedItem.Value.ToString() == lblSKU.InnerText)
                    {

                    }
                    else
                    {
                        //ddlUOM.SelectedItem.Value = "1";
                        ddlUOM.SelectedValue = lblSKU.InnerText;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"UOM is not matched with Base Unit of Item!\");", true);

                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnSaveDet_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    string PLantRights = string.Empty;
                    DataTable dtS = new DataTable();
                    dtS = objMainClass.SelectUserSegment(Convert.ToString(Session["USERID"]));
                    if (dtS.Rows.Count > 0)
                    {
                        string[] plantArray = Convert.ToString(dtS.Rows[0]["PLANTCD"]).Split(',');
                        for (int i = 0; i < plantArray.Count(); i++)
                        {
                            if (plantArray[i].Trim() == ddlPLant.SelectedValue)
                            {
                                PLantRights = ddlPLant.SelectedValue;
                            }
                        }

                        if (PLantRights.Length > 0)
                        {

                            DataTable dt = (DataTable)ViewState["ItemData"];
                            if (grvListItem.Rows.Count > 0)
                            {
                                DataRow lastRow = dt.Rows[dt.Rows.Count - 1];
                                int id = Convert.ToInt32(lastRow["ID"]) + 1;

                                dt.Rows.Add(id, txtJobId.Text, DateTime.Now.ToShortDateString(), lblItemId.InnerText, ddlUOM.SelectedValue, txtQuantity.Text, ddlPLant.SelectedValue, ddlLocation.SelectedValue, ddlReqBy.SelectedValue, "71", Session["USERID"], ddlReqBy.SelectedItem.Text, lblItemDesc.InnerText, ddlUOM.SelectedItem.Text, txtItemCode.Text, lblSegment.InnerText, lblSegmentDesc.InnerText, lblJobStatus.InnerText, lblSKU.InnerText);

                                ViewState["ItemData"] = dt;
                            }
                            else
                            {

                                dt.Rows.Add("1", txtJobId.Text, DateTime.Now.ToShortDateString(), lblItemId.InnerText, ddlUOM.SelectedValue, txtQuantity.Text, ddlPLant.SelectedValue, ddlLocation.SelectedValue, ddlReqBy.SelectedValue, "71", Session["USERID"], ddlReqBy.SelectedItem.Text, lblItemDesc.InnerText, ddlUOM.SelectedItem.Text, txtItemCode.Text, lblSegment.InnerText, lblSegmentDesc.InnerText, lblJobStatus.InnerText, lblSKU.InnerText);

                                ViewState["ItemData"] = dt;
                            }

                            grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                            grvListItem.DataBind();
                            EmptyString();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have plant rights.');$('#txtJobId').focus();", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have plant rights.');$('#txtJobId').focus();", true);
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

        protected void lnkSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (grvListItem.Rows.Count > 0)
                    {
                        //INSERT_TRAN_PARTREQ
                        bool iResult = objMainClass.SavePartRequest(grvListItem, Convert.ToString(Session["USERID"]));
                        if (iResult != false)
                        {
                            String strCustContent = "";
                            strCustContent = fileread();
                            strCustContent = strCustContent.Replace("###Heading###", "New Part Request Created by User.");
                            strCustContent = strCustContent.Replace("###CreateBy###", Convert.ToString(Session["USERNAME"]));
                            strCustContent = strCustContent.Replace("###CreateDate###", DateTime.Now.ToShortDateString());
                            strCustContent = strCustContent.Replace("###PRNO###", "");
                            strCustContent = strCustContent.Replace("###Message###", "New Part Request created by user. Details are as per above.");
                            //objMainClass.SendMail(strCustContent, "New Part Request Created");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Part Request save sucessfully.\");$('.close').click(function(){window.location.href ='ViewPartRequest.aspx' });", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Record not inserted sucessfully!');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please Enter Item Details!');", true);
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

        protected void txtJobId_TextChanged(object sender, EventArgs e)
        {
            if (txtJobId.Text.Length >= 6)
            {
                //SELECT_JOBDETAILNEW
                DataTable dt = new DataTable();
                dt = objMainClass.SelectJobDetails(txtJobId.Text);
                if (dt.Rows.Count > 0)
                {
                    lblSegment.InnerText = Convert.ToString(dt.Rows[0]["SEGMENT"]);// + " - " + Convert.ToString(dt.Rows[0]["segmentdesc"]);
                    lblSegmentDesc.InnerText = Convert.ToString(dt.Rows[0]["SEGMENT"]) + " - " + Convert.ToString(dt.Rows[0]["segmentdesc"]);
                    lblJobStatus.InnerText = Convert.ToString(dt.Rows[0]["jobstatdesc"]);
                    DataTable dtL = new DataTable();
                    dtL = objMainClass.SelectLocationBySegment(Convert.ToString(dt.Rows[0]["SEGMENT"]));
                    if (dtL.Rows.Count > 0)
                    {
                        ddlLocation.SelectedValue = Convert.ToString(dtL.Rows[0]["LOCCD"]);
                    }

                    if (Convert.ToString(dt.Rows[0]["JOBSTATUS"]) == "23")
                    {
                        txtJobId.Focus();
                        txtJobId.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Job id is closed.');$('#txtJobId').focus();", true);
                        txtJobId.Focus();
                        txtJobId.Text = string.Empty;
                    }
                    string SegmentRight = string.Empty;
                    DataTable dtS = new DataTable();
                    dtS = objMainClass.SelectUserSegment(Convert.ToString(Session["USERID"]));
                    if (dtS.Rows.Count > 0)
                    {
                        string[] segArray = Convert.ToString(dtS.Rows[0]["SEGMENT"]).Split(',');
                        for (int i = 0; i < segArray.Count(); i++)
                        {
                            if (segArray[i] == lblSegment.InnerText)
                            {
                                SegmentRight = lblSegment.InnerText;
                            }
                        }

                        if (SegmentRight.Length > 0)
                        {
                            return;
                        }
                        else
                        {
                            txtJobId.Focus();
                            txtJobId.Text = string.Empty;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have segment rights.');$('#txtJobId').focus();", true);
                            txtJobId.Focus();
                            txtJobId.Text = string.Empty;
                        }
                    }
                    else
                    {
                        txtJobId.Focus();
                        txtJobId.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have segment rights.');$('#txtJobId').focus();", true);
                        txtJobId.Focus();
                        txtJobId.Text = string.Empty;
                    }
                }
                else
                {
                    txtJobId.Focus();
                    txtJobId.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid Job Id.');$('#txtJobId').focus();", true);
                    txtJobId.Focus();
                    txtJobId.Text = string.Empty;
                }
            }
            else
            {
                txtJobId.Focus();
                txtJobId.Text = string.Empty;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid Job Id.');$('#txtJobId').focus();", true);
                txtJobId.Focus();
                txtJobId.Text = string.Empty;
            }
        }

        protected void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            if (txtItemCode.Text.Length == 10)
            {
                //SP_GET_ITEM_DETAILS
                DataTable dt = new DataTable();
                dt = objMainClass.GetItemDetails(txtItemCode.Text, ddlPLant.SelectedValue, ddlLocation.SelectedValue);
                if (dt.Rows.Count > 0)
                {
                    lblItemDesc.InnerText = Convert.ToString(dt.Rows[0]["ITEMDESC"]);
                    lblItemId.InnerText = Convert.ToString(dt.Rows[0]["ITEMID"]);
                    ddlUOM.SelectedValue = Convert.ToString(dt.Rows[0]["SKU"]);
                    lblSKU.InnerText = Convert.ToString(dt.Rows[0]["SKU"]);
                }
                else
                {
                    txtItemCode.Focus();
                    txtItemCode.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid Item Code.');", true);
                    txtItemCode.Focus();
                    txtItemCode.Text = string.Empty;
                }
            }
            else
            {
                txtItemCode.Focus();
                txtItemCode.Text = string.Empty;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid Item Code.');", true);
                txtItemCode.Focus();
                txtItemCode.Text = string.Empty;
            }
        }

        protected void lnkOpenPoup_Click(object sender, EventArgs e)
        {
            objBindDDL.FillItemCat(ddlpopCategory);
            objBindDDL.FillItemGrp(ddlpopGroup);
            objBindDDL.FillItemSubGrp(ddlpopSubGroup);
            objBindDDL.FillBrand(ddlpopMake, 0);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-item').modal();", true);
        }

        protected void lnkNewPartCode_Click(object sender, EventArgs e)
        {

            objBindDDL.FillItemCat(ddlNewCategory);
            ddlNewCategory.SelectedValue = "2";
            objBindDDL.FillItemGrp(ddlNewGroup);
            ddlNewGroup.SelectedValue = "3";
            objBindDDL.FillItemSubGrp(ddlNewSubGroup);
            ddlNewSubGroup.SelectedValue = "83";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Newitem').modal();", true);
        }

        protected static string fileread()
        {
            string actualfol;
            actualfol = HttpContext.Current.Server.MapPath("~/PRMailBody.html");
            StreamReader sr = new StreamReader(actualfol);
            string strcontent = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            return strcontent;
        }

        protected void lnkNewItemSave_Click(object sender, EventArgs e)
        {

            try
            {
                //bool iResult = objMainClass.SaveNewItem(txtNewItemName.Text, txtNewItemDesc.Text, ddlNewCategory.SelectedIndex > 0 ? ddlNewCategory.SelectedValue : "", ddlNewGroup.SelectedIndex > 0 ? ddlNewGroup.SelectedValue : "", ddlNewSubGroup.SelectedIndex > 0 ? ddlNewSubGroup.SelectedValue : "", Convert.ToString(Session["USERID"]));
                bool iResult = objMainClass.SaveNewItem(txtNewItemName.Text, txtNewItemDesc.Text, txtNewItemSpecification.Text, ddlNewCategory.SelectedValue, ddlNewGroup.SelectedValue, ddlNewSubGroup.SelectedValue, Convert.ToString(Session["USERID"]));
                if (iResult == true)
                {
                    String strCustContent = "";
                    strCustContent = fileread();
                    strCustContent = strCustContent.Replace("###Heading###", "Add New Itemcode ");
                    strCustContent = strCustContent.Replace("###CreateBy###", Convert.ToString(Session["USERNAME"]));
                    strCustContent = strCustContent.Replace("###CreateDate###", Convert.ToDateTime(DateTime.Now).ToShortDateString());
                    strCustContent = strCustContent.Replace("###PRNO###", "");
                    strCustContent = strCustContent.Replace("###Message###", "User requested to add new Itemcode. Please check and proceed further!");
                    //objMainClass.SendMail(strCustContent, "Add New Itemcode");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('New Item Code Request Inserted Successfully! ');", true);
                    txtNewItemName.Text = string.Empty;
                    txtNewItemDesc.Text = string.Empty;
                    objBindDDL.FillItemCat(ddlNewCategory);
                    ddlNewCategory.SelectedValue = "2";
                    objBindDDL.FillItemGrp(ddlNewGroup);
                    ddlNewGroup.SelectedValue = "3";
                    objBindDDL.FillItemSubGrp(ddlNewSubGroup);
                    ddlNewSubGroup.SelectedValue = "83";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Something went wrong. Please try again! ');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void grvPopItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtItemCode.Text = Convert.ToString(grvPopItem.SelectedRow.Cells[1].Text);
            txtItemCode_TextChanged(1, e);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-item').hide();", true);
            txtItemCode.Text = Convert.ToString(grvPopItem.SelectedRow.Cells[1].Text);
        }

        protected void btnShowItem_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = objMainClass.SelectItem(ddlpopMake.SelectedIndex > 0 ? ddlpopMake.SelectedItem.Text : "", ddlpopModel.SelectedIndex > 0 ? ddlpopModel.SelectedItem.Text : "", txtpopItemCode.Text, ddlpopGroup.SelectedIndex > 0 ? ddlpopGroup.SelectedValue : "", ddlpopSubGroup.SelectedIndex > 0 ? ddlpopSubGroup.SelectedValue : "", ddlpopCategory.SelectedIndex > 0 ? ddlpopCategory.SelectedValue : "", txtPopupItemDesc.Text);
            if (dt.Rows.Count > 0)
            {
                grvPopItem.DataSource = dt;
                grvPopItem.DataBind();
            }
            else
            {
                grvPopItem.DataSource = string.Empty;
                grvPopItem.DataBind();
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-item').modal();", true);
        }

        protected void ddlpopMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            objBindDDL.FillModel(ddlpopModel, ddlpopMake.SelectedValue);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-item').modal();", true);
        }

        protected void grvListItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "eDelete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                DataTable dt = (DataTable)ViewState["ItemData"];
                GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                dt.Rows[row.RowIndex].Delete();
                ViewState["ItemData"] = dt;
                grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                grvListItem.DataBind();
            }
            if (e.CommandName == "eEdit")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                GridViewRow gRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;



                Label lblJobID = (Label)gRow.FindControl("lblJobID");
                Label lblGvItemId = (Label)gRow.FindControl("lblGvItemId");
                Label lblGvItemDesc = (Label)gRow.FindControl("lblGvItemDesc");
                Label lblUOM = (Label)gRow.FindControl("lblUOM");
                Label lblUOMName = (Label)gRow.FindControl("lblUOMName");
                Label lblQTY = (Label)gRow.FindControl("lblQTY");
                Label lblPlantcd = (Label)gRow.FindControl("lblPlantcd");
                Label lblLoccd = (Label)gRow.FindControl("lblLoccd");
                Label lblReqBy = (Label)gRow.FindControl("lblReqBy");
                Label lblReqByName = (Label)gRow.FindControl("lblReqByName");
                Label lblStatus = (Label)gRow.FindControl("lblStatus");
                Label lblGvItemCode = (Label)gRow.FindControl("lblGvItemCode");
                Label lblGvSegment = (Label)gRow.FindControl("lblGvSegment");
                Label lblGvSegmentDesc = (Label)gRow.FindControl("lblGvSegmentDesc");
                Label lblGvJobStatus = (Label)gRow.FindControl("lblGvJobStatus");
                Label lblGvSku = (Label)gRow.FindControl("lblGvSku");

                txtJobId.Text = lblJobID.Text;
                lblSegment.InnerText = lblGvSegment.Text;
                lblSegmentDesc.InnerText = lblGvSegmentDesc.Text;
                lblJobStatus.InnerText = lblGvJobStatus.Text;
                txtItemCode.Text = lblGvItemCode.Text;
                lblItemDesc.InnerText = lblGvItemDesc.Text;
                lblItemId.InnerText = lblGvItemId.Text;
                lblSKU.InnerText = lblGvSku.Text;
                txtQuantity.Text = lblQTY.Text;
                ddlUOM.SelectedValue = lblUOM.Text;
                ddlPLant.SelectedValue = lblPlantcd.Text;
                ddlLocation.SelectedValue = lblLoccd.Text;
                ddlReqBy.SelectedValue = lblReqBy.Text;



                DataTable dt = (DataTable)ViewState["ItemData"];
                dt.Rows[gRow.RowIndex].Delete();
                ViewState["ItemData"] = dt;
                grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                grvListItem.DataBind();
            }
        }


        private void EmptyString()
        {
            txtJobId.Text = string.Empty;
            txtItemCode.Text = string.Empty;
            txtQuantity.Text = "1";
            ddlUOM.SelectedIndex = 0;
            if (Convert.ToString(Session["EMPID"]) != string.Empty && Convert.ToString(Session["EMPID"]) != "")
            {
                ddlReqBy.SelectedValue = Convert.ToString(Session["EMPID"]);
            }
            else
            {
                ddlReqBy.SelectedIndex = 0;
            }
            lblSegment.InnerText = string.Empty;
            lblJobStatus.InnerText = string.Empty;
            lblItemDesc.InnerText = string.Empty;
            lblItemId.InnerText = string.Empty;
            lblSKU.InnerText = string.Empty;
            ddlPLant.SelectedIndex = 1;
            //ddlLocation.SelectedValue = "MS01";
            if (ddlPLant.SelectedValue == "1001")
            {
                ddlLocation.SelectedValue = "M001";
            }
            else
            {
                ddlLocation.SelectedValue = "MS01";
            }
            lblSegmentDesc.InnerText = string.Empty;

        }
    }
}