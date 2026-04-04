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
    public partial class frmTRNPending : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        DataTable dtItem = new DataTable();

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
                            //Session.Abandon();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }

                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");//Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();

                        objBindDDL.FillPlant(ddlPlantCode, "SEARCH");
                        string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                        ddlPlantCode.SelectedValue = plantArray[0];

                        GetData();

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


        public void GetData()
        {
            //GetTRNData
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetTRNData(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, ddlPlantCode.SelectedIndex == 0 ? Convert.ToString(Session["PLANTCD"]) : ddlPlantCode.SelectedValue, "GETDATE");

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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true); ;
            }
        }

        protected void ddlPlantCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (ddlPlantCode.SelectedIndex > 0)
                    {
                        string PLantRights = string.Empty;
                        string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                        for (int i = 0; i < plantArray.Count(); i++)
                        {
                            if (plantArray[i] == ddlPlantCode.SelectedValue)
                            {
                                PLantRights = ddlPlantCode.SelectedValue;
                            }
                        }

                        if (PLantRights.Length > 0)
                        {

                        }
                        else
                        {
                            ddlPlantCode.SelectedValue = plantArray[0];
                            ddlPlantCode.Focus();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have plant rights.');$('#txtJobId').focus();", true);
                            ddlPlantCode.SelectedValue = plantArray[0];
                            ddlPlantCode.Focus();
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

        protected void lnkSearhIST_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GetData();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true); ;
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
                    string attachment = "attachment; filename=PendingIST(TRN1).xls";
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


        private void SetUpGrid()
        {
            try
            {

                DataColumn dtColumn;

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ID";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMCODE";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMDESC";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMID";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMGROUP";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMGROUPID";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMUOM";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "UOMID";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMQTY";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMRATE";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMAMOUNT";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "DELIVERYDATE";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "GLCODE";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "COSTCENTER";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMFROMPLANTCD";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMFROMPLANTID";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMFROMLOCCD";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "LOCCDFROMID";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMPLANTCD";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMPLANTID";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMLOCCD";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "LOCCDID";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "PROFITCENTER";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ASSETCODE";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "REQUISITIONER";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "TRACKNO";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMTEXT";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "PARTREQNO";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMREMARKS";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "SKU";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "MAKE";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "MODEL";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "DISPNAME";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "DISPMRP";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "VALUELIMIT";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "MAXSTKQTY";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "HSN";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "HSNGROUP";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "HSNGROUPDESC";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "CONDTYPE";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMSTATUS";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ONHANDSTOCK";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "REFSRNO";
                dtItem.Columns.Add(dtColumn);


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnIST_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    string ITEMID = grdrow.Cells[4].Text;
                    string ITEMCODE = grdrow.Cells[5].Text;
                    string ITEMDESC = grdrow.Cells[6].Text;
                    string qty = grdrow.Cells[7].Text;
                    string CSTCENTCD = grdrow.Cells[9].Text;
                    string PRFCNT = grdrow.Cells[10].Text;
                    string JOBID = grdrow.Cells[11].Text;
                    string ddlPLant = grdrow.Cells[12].Text;
                    string ddlLocation = grdrow.Cells[13].Text;
                    string ACTUALPLANT = grdrow.Cells[14].Text;
                    string ACTUALLOCATION = grdrow.Cells[15].Text;

                    string SONO = grdrow.Cells[18].Text;

                    DataTable dtJobsheet = new DataTable();

                    dtJobsheet = objMainClass.GetJSDetailsByRefjobid(objMainClass.intCmpId, JOBID, "GETJSDATA");

                    if (dtJobsheet.Rows.Count > 0)
                    {
                        int ii = objMainClass.UpdateReturnFlaginJobid(objMainClass.intCmpId, JOBID, Convert.ToInt32(Session["USERID"]), "UPDATERETURNFLAG");
                        decimal bal;
                        bal = objMainClass.SP_GET_STOCK_FUNCTION(objMainClass.intCmpId, objMainClass.getFinYear(objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true)), ITEMCODE, objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true), ddlPLant, ddlLocation);
                        if (bal > 0)
                        {
                            DataTable dt = new DataTable();
                            dt = objMainClass.GetItemDetails(ITEMCODE, ddlPLant, ddlLocation);
                            if (dt.Rows.Count > 0)
                            {
                                if (Convert.ToString(dt.Rows[0]["status"]) == "1")
                                {
                                    Label txtItemDesc = new Label();
                                    txtItemDesc.Text = Convert.ToString(dt.Rows[0]["itemdesc"]);

                                    Label txtGLCode = new Label();
                                    txtGLCode.Text = Convert.ToString(dt.Rows[0]["glcode"]);

                                    Label txtItemGroup = new Label();
                                    txtItemGroup.Text = Convert.ToString(dt.Rows[0]["itemgrp"]);

                                    Label txtItemId = new Label();
                                    txtItemId.Text = Convert.ToString(dt.Rows[0]["itemid"]);

                                    Label txtSku = new Label();
                                    txtSku.Text = Convert.ToString(dt.Rows[0]["sku"]);

                                    //Label ddlUOM = new Label();
                                    //Label ddlUOM.SelectedValue = txtSku.Text;

                                    Label txtItemGroupId = new Label();
                                    txtItemGroupId.Text = Convert.ToString(dt.Rows[0]["itemgrpid"]);

                                    Label txtItemMake = new Label();
                                    txtItemMake.Text = Convert.ToString(dt.Rows[0]["make"]);

                                    Label txtItemModel = new Label();
                                    txtItemModel.Text = Convert.ToString(dt.Rows[0]["model"]);

                                    Label txtItemDispName = new Label();
                                    txtItemDispName.Text = Convert.ToString(dt.Rows[0]["dispname"]);

                                    Label txtItemDispMRP = new Label();
                                    txtItemDispMRP.Text = Convert.ToString(dt.Rows[0]["mrp"]);

                                    Label txtItemValueLimit = new Label();
                                    txtItemValueLimit.Text = Convert.ToString(dt.Rows[0]["valuelimit"]);

                                    Label txtItemMaxStkQty = new Label();
                                    txtItemMaxStkQty.Text = Convert.ToString(dt.Rows[0]["maxstkqty"]);

                                    Label txtItemHSN = new Label();
                                    txtItemHSN.Text = Convert.ToString(dt.Rows[0]["hsngrpcode"]);

                                    Label txtItemHSNGroup = new Label();
                                    txtItemHSNGroup.Text = Convert.ToString(dt.Rows[0]["hsngrp"]);

                                    Label txtItemHSNGroupDesc = new Label();
                                    txtItemHSNGroupDesc.Text = Convert.ToString(dt.Rows[0]["hsngrpdesc"]);

                                    Label txtItemCondType = new Label();
                                    txtItemCondType.Text = Convert.ToString(dt.Rows[0]["condtype"]);

                                    Label txtItemStatus = new Label();
                                    txtItemStatus.Text = Convert.ToString(dt.Rows[0]["status"]);

                                    // dtItem = null;
                                    SetUpGrid();

                                    dtItem.Rows.Add("1", ITEMCODE, txtItemDesc.Text, txtItemId.Text, txtItemGroup.Text, txtItemGroupId.Text, txtSku.Text,
                                           txtSku.Text, qty, "1", "", DateTime.Now.ToShortDateString(), txtGLCode.Text, CSTCENTCD,
                                           ddlPLant, ddlPLant, ddlLocation, ddlLocation,
                                           ACTUALPLANT, ACTUALPLANT, ACTUALLOCATION, ACTUALLOCATION, PRFCNT,
                                           "", "", JOBID, "AUTO IST - TRN TO ACTUAL LOCATION", "", "AUTO IST - TRN TO ACTUAL LOCATION", "",
                                           txtItemMake.Text, txtItemModel.Text, txtItemDispName.Text, txtItemDispMRP.Text, txtItemValueLimit.Text, txtItemMaxStkQty.Text,
                                           txtItemHSN.Text, txtItemHSNGroup.Text, txtItemHSNGroupDesc.Text, txtItemStatus.Text, txtItemCondType.Text, "");

                                    if (dtItem.Rows.Count > 0)
                                    {
                                        grvListItem.DataSource = dtItem;
                                        grvListItem.DataBind();



                                        string DOCNO = objMainClass.InsertMaterialIssue("105", "", DateTime.Now.ToString(), JOBID, "AUTO IST - TRN TO ACTUAL LOCATION", grvListItem, Convert.ToString(Session["USERID"]), objMainClass.intCmpId, "", 5);
                                        if (DOCNO != "")
                                        {
                                            int i = objMainClass.UpdateSalesReturn(objMainClass.intCmpId, SONO == "" ? "" : objMainClass.strConvertZeroPadding(SONO), "UPDATESALESRETURN");

                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Document No. : " + DOCNO + " saved successfully.\");$('.close').click(function(){window.location.href ='frmTRNPending.aspx' });", true);
                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Record not inserted sucessfully!');", true);
                                        }


                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ITEMCODE + " - Details not found.!\");", true);
                                    }

                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ITEMCODE + " - Item code is deactivated, you can't use the same!\");", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ITEMCODE + " - Item code not found!\");", true);

                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ITEMCODE + " - Item code stock not available TRN1.!\");", true);
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + JOBID + " - New job sheet not created for this job id. New job sheet needs to be created for IST.!\");", true);
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
