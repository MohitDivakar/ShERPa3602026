using Newtonsoft.Json;
using RestSharp;
using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.CRM
{
    public partial class frmBulkJobSheet : System.Web.UI.Page
    {

        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
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

                        objBindDDL.FillSegment(ddlSegment);
                        objBindDDL.FillDistChnlNew(ddlDistChnl);

                        objBindDDL.FillItemByCat(ddlProduct, "13");
                        ddlProduct.SelectedValue = "1";
                        objBindDDL.FillBrandByItem(ddlMake, ddlProduct.SelectedValue);
                        objBindDDL.FillModel(ddlModel, ddlMake.SelectedValue);

                        objBindDDL.FillPlant(ddlPLant);
                        ddlPLant.SelectedValue = "1001";
                        objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);
                        ddlLocation.SelectedValue = "WMR1";
                        //ddlLocation_SelectedIndexChanged(1, e);

                        objBindDDL.FillLists(ddlColor, "CL");



                        DataTable dt = new DataTable();
                        dt.Columns.AddRange(new DataColumn[10] {
                            //new DataColumn("ProductID"),
                            //new DataColumn("ProductName"),
                            new DataColumn("ItemCcde"),
                            new DataColumn("Make"),
                            new DataColumn("Model"),
                            new DataColumn("IMEI1"),
                            new DataColumn("IMEI2"),
                            new DataColumn("ReverseCourier"),
                            new DataColumn("ReverseWaybill"),
                            new DataColumn("Plant"),
                            new DataColumn("Location"),
                            new DataColumn("Color")//,
                            //new DataColumn("OldJobId"),
                            //new DataColumn("CostCenter"),
                            //new DataColumn("GLCODE"),
                            //new DataColumn("PRFCNT")
                            });
                        ViewState["JobSheetData"] = dt;

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

        protected void ddlDistChnl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    if (ddlDistChnl.SelectedIndex > 0)
                    {
                        dt = objMainClass.GetSegmentDistValid(objMainClass.intCmpId, ddlSegment.SelectedValue, ddlDistChnl.SelectedValue);
                        if (dt.Rows.Count > 0)
                        {

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid Segment or Dist. Chnl. selection.');", true);
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
            //SelectItem
            try
            {
                if (Session["USERID"] != null)
                {
                    if (txtItemCode.Text != "" && txtItemCode.Text != string.Empty && txtItemCode.Text != null)
                    {
                        DataTable dtItem = new DataTable();
                        dtItem = objMainClass.SelectItem("", "", txtItemCode.Text, "", "", "", "");
                        if (dtItem.Rows.Count > 0)
                        {
                            //hfItemDesc.Value = Convert.ToString(dtItem.Rows[0]["Desciption"]);
                            //hfGLCODE.Value = Convert.ToString(dtItem.Rows[0]["GLCODE"]);

                            ddlMake.SelectedIndex = ddlMake.Items.IndexOf(ddlMake.Items.FindByText(Convert.ToString(dtItem.Rows[0]["MAKE"])));

                            if (ddlMake.SelectedIndex > 0)
                            {
                                ddlMake_SelectedIndexChanged(1, e);

                                ddlModel.SelectedIndex = ddlModel.Items.IndexOf(ddlModel.Items.FindByText(Convert.ToString(dtItem.Rows[0]["MODEL"])));
                            }

                            if (txtItemCode.Text.Contains("MDUD"))
                            {
                                string[] para = { "GB" };
                                string[] spec1 = hfItemDesc.Value.Split(para, 0);
                                string color = "";
                                string ram = "";
                                string rom = "";

                                string[] para2 = { "MOBILE" };
                                if (ddlMake.SelectedItem.Text == "APPLE")
                                {
                                    string[] spec2 = spec1[1].Split(para2, 0);
                                    color = spec2[0].Trim();
                                    string[] spec3 = spec1[0].Split(' ');
                                    ram = spec3[spec3.Count() - 2].Trim();
                                    rom = spec3[spec3.Count() - 1].Trim();
                                }
                                else
                                {
                                    string[] spec2 = spec1[2].Split(para2, 0);
                                    color = spec2[0].Trim();
                                    string[] spec3 = spec1[0].Split(' ');
                                    ram = spec3[spec3.Count() - 1].Trim();
                                    rom = spec1[1].Trim();
                                }



                                hfColor.Value = color;
                                ddlColor.SelectedIndex = ddlColor.Items.IndexOf(ddlColor.Items.FindByText(color));
                            }

                            if (txtItemCode.Text.Contains("LTLT"))
                            {
                                string[] spec1 = hfItemDesc.Value.Split('-');
                                string color = spec1[spec1.Count() - 1].Trim();

                                string ram = spec1[2].Replace(" GB ", "").Trim();
                                string rom = spec1[3].Replace(" GB SSD ", "").Replace(" GB HDD ", "").Replace(" TB SSD ", "").Replace(" TB HDD ", "").Trim();

                                if (color.Any(char.IsDigit))
                                {
                                    hfColor.Value = "BLACK";
                                }
                                else
                                {
                                    if (color.Length >= 5)
                                    {
                                        hfColor.Value = color;
                                    }
                                    else if (color.Contains("SIL"))
                                    {
                                        hfColor.Value = "SILVER";
                                    }
                                    else
                                    {
                                        hfColor.Value = "BLACK";
                                    }
                                }
                                ddlColor.SelectedIndex = ddlColor.Items.IndexOf(ddlColor.Items.FindByText(color));
                            }



                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No record found for this item code. Plese enter valid itemcode.');", true);
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

        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    objBindDDL.FillBrandByItem(ddlMake, ddlProduct.SelectedValue);
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
                    objBindDDL.FillModel(ddlModel, ddlMake.SelectedValue);
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

        protected void ddlPLant_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);
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

        protected void imgAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetJobStatusByIMEI(objMainClass.intCmpId, "0", txtImei1.Text, (int)STATUS.Canceled);

                    if (dt.Rows.Count > 0)
                    {

                        if (Convert.ToInt32(dt.Rows[0]["jobstatus"]) != (int)STATUS.Closed)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"Last Job ID still Open. Job ID : " + Convert.ToString(dt.Rows[0]["jobid"]) + "\");", true);
                        }
                        else
                        {
                            hfOldJobID.Value = Convert.ToString(dt.Rows[0]["jobid"]);

                            CreateJob();
                        }
                    }
                    else
                    {
                        CreateJob();
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


        public void CreateJob()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dtData = (DataTable)ViewState["JobSheetData"];
                    dtData.Rows.Add(txtItemCode.Text, ddlMake.SelectedItem.Text, ddlModel.SelectedItem.Text, txtImei1.Text, txtImei2.Text, txtReverseCourier.Text, txtReverseWaybill.Text, ddlPLant.SelectedValue,
                        ddlLocation.SelectedValue, ddlColor.SelectedItem.Text);
                    //dtData.Rows.Add(ddlProduct.SelectedValue, ddlProduct.SelectedItem.Text, txtItemCode.Text, ddlMake.SelectedItem.Text, ddlModel.SelectedItem.Text, txtImei1.Text, txtImei2.Text, txtReverseCourier.Text, txtReverseWaybill.Text, ddlPLant.SelectedValue,
                    //    ddlLocation.SelectedValue, ddlColor.SelectedItem.Text, hfOldJobID.Value, hfCostCenter.Value, hfGLCODE.Value, hfPrfcntr.Value);

                    gvData.DataSource = dtData;
                    gvData.DataBind();

                    ViewState["JobSheetData"] = dtData;

                    CrearText();

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

        //protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Session["USERID"] != null)
        //        {
        //            string costcenter = "1007";
        //            DataTable dtCostCenter = new DataTable();
        //            dtCostCenter = objMainClass.GetCostCenter(ddlPLant.SelectedValue, ddlLocation.SelectedValue);

        //            if (dtCostCenter.Rows.Count > 0)
        //            {
        //                costcenter = Convert.ToString(dtCostCenter.Rows[0]["CSTCENTCD"]);
        //            }
        //            else
        //            {
        //                costcenter = "1007";
        //            }

        //            hfCostCenter.Value = costcenter;
        //            //hfPrfcntr.Value = "1000";
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
        //    }

        //}

        public void CrearText()
        {
            txtItemCode.Text = string.Empty;
            txtImei1.Text = string.Empty;
            txtImei2.Text = string.Empty;
            txtReverseCourier.Text = string.Empty;
            txtReverseWaybill.Text = string.Empty;
        }

        protected void gvData_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dtData = (DataTable)ViewState["JobSheetData"];

                    dtData.Rows.RemoveAt(e.RowIndex);
                    gvData.DataSource = dtData;
                    gvData.DataBind();

                    ViewState["JobSheetData"] = dtData;

                    if (dtData.Rows.Count > 0)
                    {
                        lblRecoretxt.Visible = true;
                        lblRecord.Visible = true;

                        lblRecord.Text = dtData.Rows.Count.ToString();
                        lnkSave.Visible = true;
                    }
                    else
                    {
                        lblRecoretxt.Visible = false;
                        lblRecord.Visible = false;

                        lblRecord.Text = "0";
                        lnkSave.Visible = false;
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

        protected void imgSaveAll_Click(object sender, EventArgs e)
        {
            DataTable dtJobId = new DataTable("Data");
            dtJobId.Columns.Add("IMEINO");
            dtJobId.Columns.Add("JOBID");
            dtJobId.Columns.Add("ERROR");
            dtJobId.Columns.Add("OLDJOBID");
            int ierror = 0;
            int isucess = 0;

            try
            {
                if (Session["USERID"] != null)
                {
                    if (gvData.Rows.Count > 0)
                    {
                        DataTable dtAPI = new DataTable();
                        dtAPI = objMainClass.GetWAData("CREATEJS", 1, "GETWADATA");

                        if (dtAPI.Rows.Count > 0)
                        {
                            foreach (GridViewRow row in gvData.Rows)
                            {
                                //Label lblProductID = row.FindControl("lblProductID") as Label;
                                //Label lblProductName = row.FindControl("lblProductName") as Label;
                                string lblProductID = "";
                                string lblProductName = "";
                                string lblOldJobId = "";
                                Label lblItemCcde = row.FindControl("lblItemCcde") as Label;
                                Label lblMake = row.FindControl("lblMake") as Label;
                                Label lblModel = row.FindControl("lblModel") as Label;
                                Label lblIMEI1 = row.FindControl("lblIMEI1") as Label;
                                Label lblIMEI2 = row.FindControl("lblIMEI2") as Label;
                                Label lblReverseCourier = row.FindControl("lblReverseCourier") as Label;
                                Label lblReverseWaybill = row.FindControl("lblReverseWaybill") as Label;
                                Label lblPlant = row.FindControl("lblPlant") as Label;
                                Label lblLocation = row.FindControl("lblLocation") as Label;
                                Label lblColor = row.FindControl("lblColor") as Label;

                                Label lblSegment = row.FindControl("lblSegment") as Label;
                                Label lblDistChnl = row.FindControl("lblDistChnl") as Label;

                                Label lblGrade = row.FindControl("lblGrade") as Label;

                                Label lblPrice = row.FindControl("lblPrice") as Label;

                                //Label lblOldJobId = row.FindControl("lblOldJobId") as Label;
                                //Label lblCostCenter = row.FindControl("lblCostCenter") as Label;
                                //Label lblGLCODE = row.FindControl("lblGLCODE") as Label;
                                //Label lblPRFCNT = row.FindControl("lblPRFCNT") as Label;
                                DataTable dtProdItem = new DataTable();
                                dtProdItem = objMainClass.GetProditembyModel(objMainClass.intCmpId, lblModel.Text);
                                if (dtProdItem.Rows.Count > 0)
                                {
                                    lblProductID = Convert.ToString(dtProdItem.Rows[0]["PRODITEMID"]);
                                    lblProductName = Convert.ToString(dtProdItem.Rows[0]["PRODITEMDESC"]);



                                    DataTable dtOldJobID = new DataTable();
                                    dtOldJobID = objMainClass.GetJobStatusByIMEI(objMainClass.intCmpId, "0", lblIMEI1.Text, (int)STATUS.Canceled);
                                    if (dtOldJobID.Rows.Count > 0)
                                    {
                                        lblOldJobId = Convert.ToString(dtOldJobID.Rows[0]["jobid"]);
                                        if (Convert.ToInt32(dtOldJobID.Rows[0]["jobstatus"]) != (int)STATUS.Closed)
                                        {
                                            dtJobId.Rows.Add("'" + lblIMEI1.Text, "", "Last Job ID Still Open.!", Convert.ToString(dtOldJobID.Rows[0]["jobid"]));
                                            ierror++;
                                        }
                                        else
                                        {

                                            #region Job Id Creation Code...
                                            #region JobSheet Details...

                                            List<JobSheetDetail> objJobSheetDetail = new List<JobSheetDetail>();
                                            objJobSheetDetail.Add(new JobSheetDetail
                                            {
                                                CMPID = objMainClass.intCmpId,
                                                SRNO = 1,
                                                ITEMID = Convert.ToInt32(lblProductID),
                                                ITEMDESC = lblProductName,
                                                QTY = 1,
                                                UOM = 1,
                                                ITEMVALUE = Convert.ToDecimal(250),
                                                RATE = Convert.ToDecimal(250),
                                                PLANTCD = lblPlant.Text,
                                                LOCCD = lblLocation.Text,
                                                EXTWARNO = "NA",
                                                PRODMAKE = lblMake.Text,
                                                PRODMODEL = lblModel.Text,
                                                IMEINO = lblIMEI1.Text,
                                                JOBTYPE = "OTHER : ONLY SERVICE",
                                                JOBDESC = "FOR CHECK",
                                                REFINVNO = "-",
                                                REFINVDT = Convert.ToDateTime(DateTime.Now).AddYears(-2).ToString(),
                                                REFINVAMT = Convert.ToDecimal(10000),
                                                INSUCO = "0000040003",
                                                NOTE = "",
                                                PRODCOND = "",
                                                WAYBILLNO = "",
                                                REVDCNO = "",
                                                FWAYBILLNO = "",
                                                DCNO = "",
                                                BATTERYNO = lblMake.Text,
                                                REVTRANNAME = "",
                                                FTRANNAME = "",
                                                WAYBILLSTATUS = "",
                                                FWAYBILLSTATUS = "",
                                                DELICONFDT = "",
                                                LOCKCODE = "",
                                                REVPICKUPDT = "",
                                                BACKCOVERFLAG = "Y",
                                                REVDELIDT = "",
                                                FPICKUPDT = "",
                                                PHYIMEINO = lblIMEI1.Text,
                                                FESTIDELDT = "",
                                                REVESTIDELDT = "",
                                                IMEINO2 = lblIMEI2.Text,
                                                CODWAYBILLNO = "",
                                                FEDEXPICKUP = 0,
                                                PRODCOLOR = hfColor.Value,
                                                DELIVERYTO = "",
                                                ID = 0,
                                                JOBID = "",
                                                GRADE = lblGrade.Text,
                                                BILLAMT = Convert.ToDecimal(lblPrice.Text)
                                            });

                                            #endregion

                                            #region Addess Details...

                                            AddressDetail objAddressDetail = new AddressDetail();
                                            objAddressDetail.CMPID = objMainClass.intCmpId;
                                            objAddressDetail.REFID = "";
                                            objAddressDetail.REFTYPE = "JS";
                                            objAddressDetail.ADDOF = "G";
                                            objAddressDetail.ADDR1 = "MOBEX";
                                            objAddressDetail.ADDR2 = "MOBEX";
                                            objAddressDetail.ADDR3 = "";
                                            objAddressDetail.CITY = "AHMEDABAD";
                                            objAddressDetail.STCD = 1;
                                            objAddressDetail.CNCD = "IN";
                                            objAddressDetail.POSTALCODE = "380023";
                                            objAddressDetail.CONTACTPERSON = "";
                                            objAddressDetail.CONTACTNO = "";
                                            objAddressDetail.CONTACTPERSON2 = "";
                                            objAddressDetail.CONTACTNO2 = "";
                                            objAddressDetail.CONTACTPERSON3 = "";
                                            objAddressDetail.CONTACTNO3 = "";
                                            objAddressDetail.MOBILENO = "1234567890";
                                            objAddressDetail.MOBILENO2 = "";
                                            objAddressDetail.MOBILENO3 = "";
                                            objAddressDetail.FAXNO = "";
                                            objAddressDetail.EMAILID = "";
                                            objAddressDetail.WEBSITE = "";
                                            objAddressDetail.LAT = "";
                                            objAddressDetail.LONG = "";
                                            #endregion

                                            #region JobSheet Master...

                                            JobSheetMaster objJobSheetMaster = new JobSheetMaster();
                                            objJobSheetMaster.CMPID = objMainClass.intCmpId;
                                            objJobSheetMaster.JOBDT = Convert.ToDateTime(DateTime.Now).ToShortDateString();
                                            objJobSheetMaster.JSTYPE = 17;
                                            objJobSheetMaster.SHIPTOPARTY = "0000000001";
                                            objJobSheetMaster.BILLTOPARTY = "0000010003";
                                            objJobSheetMaster.ENDCUST = "MOBEX";
                                            objJobSheetMaster.REMARK = "";
                                            objJobSheetMaster.JOBSTATUS = Convert.ToInt32(JOBSTATUS.Saved);
                                            objJobSheetMaster.STATRES = "";
                                            objJobSheetMaster.STATUPDDT = Convert.ToDateTime(DateTime.Now).ToString();
                                            objJobSheetMaster.STATUPDBY = Convert.ToInt32(Session["USERID"]);
                                            objJobSheetMaster.SEGMENT = lblSegment.Text;
                                            objJobSheetMaster.DISTCHNL = lblDistChnl.Text;
                                            objJobSheetMaster.ISRETURN = lblOldJobId == "" || lblOldJobId == null || lblOldJobId == string.Empty ? "N" : "Y";
                                            objJobSheetMaster.REFJOBID = lblOldJobId == "" || lblOldJobId == null || lblOldJobId == string.Empty ? "" : objMainClass.strConvertZeroPadding(lblOldJobId);
                                            objJobSheetMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                            objJobSheetMaster.CREATEDATE = Convert.ToDateTime(DateTime.Now).ToString();
                                            objJobSheetMaster.JDAREF = lblReverseCourier.Text;//hfSONO.Value;
                                            objJobSheetMaster.JDAREFDT = Convert.ToDateTime(DateTime.Now).ToString();
                                            objJobSheetMaster.PICKUPFROM = "0000000001";
                                            objJobSheetMaster.SHIPTO = "0000000001";
                                            objJobSheetMaster.APRVFLAG = "Y";
                                            objJobSheetMaster.INQNO = 0;
                                            objJobSheetMaster.OOW = 0;
                                            objJobSheetMaster.JWREFNO = lblReverseCourier.Text;//hfSONO.Value;
                                            objJobSheetMaster.PONO = "";
                                            objJobSheetMaster.SLRNO = "";
                                            objJobSheetMaster.LISTINGID = 0;
                                            objJobSheetMaster.ITEMCODE = lblItemCcde.Text;
                                            objJobSheetMaster.AddressDetail = objAddressDetail;
                                            objJobSheetMaster.lstJobDetail = objJobSheetDetail;
                                            #endregion




                                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };

                                            string URL = Convert.ToString(dtAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtAPI.Rows[0]["TOKEN"]);

                                            var client = new RestClient(URL);
                                            client.Timeout = -1;
                                            var request = new RestRequest(Method.POST);
                                            request.AddHeader("" + Convert.ToString(dtAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtAPI.Rows[0]["KEYVALUE"]) + "");
                                            request.AddHeader("Content-Type", "application/json");
                                            var jsonInput = JsonConvert.SerializeObject(objJobSheetMaster);
                                            request.AddParameter("application/json", jsonInput, ParameterType.RequestBody);
                                            IRestResponse response = client.Execute(request);

                                            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                                            {


                                                JobSheetResponse objJobSheetResponse = new JobSheetResponse();
                                                string jsonconn = response.Content;
                                                objJobSheetResponse = JsonConvert.DeserializeObject<JobSheetResponse>(jsonconn);

                                                string NEWJOBID = Convert.ToString(objJobSheetResponse.JOBNO);

                                                dtJobId.Rows.Add("'" + lblIMEI1.Text, NEWJOBID, "", "");
                                                isucess++;

                                                hfNewJobID.Value = NEWJOBID;
                                                string NEWESTINO = "";
                                                string NEWJCNO = "";
                                                DataTable dtReverseWayAPI = new DataTable();
                                                dtReverseWayAPI = objMainClass.GetWAData("UPDATEREVWAYBILL", 1, "GETWADATA");

                                                if (dtReverseWayAPI.Rows.Count > 0)
                                                {
                                                    ReverseWaybillUpdate objReverseWaybillUpdate = new ReverseWaybillUpdate();
                                                    objReverseWaybillUpdate.CMPID = objMainClass.intCmpId;
                                                    objReverseWaybillUpdate.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                    objReverseWaybillUpdate.DOCTYPE = "JS";
                                                    objReverseWaybillUpdate.JOBID = NEWJOBID;
                                                    objReverseWaybillUpdate.REVTRANNAME = lblReverseCourier.Text;
                                                    objReverseWaybillUpdate.JOBSTATUS = (int)STATUS.RevWayBillGen;
                                                    objReverseWaybillUpdate.STAGEID = (int)STAGE.RevWayBillNo;
                                                    objReverseWaybillUpdate.STATRES = "AUTO ENTRY JOB CREATION";
                                                    objReverseWaybillUpdate.WAYBILLNO = lblReverseWaybill.Text;
                                                    objReverseWaybillUpdate.WAYBILLSTATUS = "";

                                                    string URLRevWaybill = Convert.ToString(dtReverseWayAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtReverseWayAPI.Rows[0]["TOKEN"]);

                                                    var clientRevWaybill = new RestClient(URLRevWaybill);
                                                    clientRevWaybill.Timeout = -1;
                                                    var requestRevWabill = new RestRequest(Method.POST);
                                                    requestRevWabill.AddHeader("" + Convert.ToString(dtReverseWayAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtReverseWayAPI.Rows[0]["KEYVALUE"]) + "");
                                                    requestRevWabill.AddHeader("Content-Type", "application/json");
                                                    var jsonInputRevWaybill = JsonConvert.SerializeObject(objReverseWaybillUpdate);
                                                    requestRevWabill.AddParameter("application/json", jsonInputRevWaybill, ParameterType.RequestBody);
                                                    IRestResponse responserevwaybill = clientRevWaybill.Execute(requestRevWabill);
                                                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                                                    {

                                                        DataTable dtStageSeq = new DataTable();
                                                        dtStageSeq = objMainClass.GetSegmentStageData(11, lblSegment.Text, "GETSTAGEREQ");

                                                        if (dtStageSeq.Rows.Count > 0)
                                                        {
                                                            DataTable dtInsertStage = new DataTable();
                                                            dtInsertStage = objMainClass.GetWAData("INSERTSTAGE", 1, "GETWADATA");

                                                            DataTable dtJobStatus = new DataTable();
                                                            dtJobStatus = objMainClass.GetWAData("UPDATESTATUS", 1, "GETWADATA");

                                                            if (dtInsertStage.Rows.Count > 0)
                                                            {
                                                                if (dtJobStatus.Rows.Count > 0)
                                                                {
                                                                    for (int s = 0; s < dtStageSeq.Rows.Count; s++)
                                                                    {
                                                                        if (Convert.ToInt32(dtStageSeq.Rows[s]["STAGESEQ"]) < 15)
                                                                        {

                                                                            int JOBSTAGEID = Convert.ToInt32(dtStageSeq.Rows[s]["STAGEID"]);
                                                                            int JOBSTATUSID = objMainClass.GetStatusByStageID(JOBSTAGEID);


                                                                            string URLStage = Convert.ToString(dtInsertStage.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertStage.Rows[0]["TOKEN"]);
                                                                            URLStage = URLStage + "?DOCNO=" + NEWJOBID + "&DOCTYPE=JS&STAGEID=" + JOBSTAGEID + "&STATRES=AUTO ENTRY JOB CREATION&CREATBY=" + Convert.ToInt32(Session["USERID"]);
                                                                            var clientStage = new RestClient(URLStage);
                                                                            clientStage.Timeout = -1;
                                                                            var requestStage = new RestRequest(Method.POST);
                                                                            requestStage.AddHeader("" + Convert.ToString(dtInsertStage.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertStage.Rows[0]["KEYVALUE"]) + "");
                                                                            requestStage.AddHeader("Content-Type", "application/json");
                                                                            //var jsonInputStage = JsonConvert.SerializeObject(objReverseWaybillUpdate);
                                                                            //requestStage.AddParameter("application/json", jsonInputStage, ParameterType.RequestBody);
                                                                            IRestResponse responseStage = clientStage.Execute(requestStage);


                                                                            string URLStatus = Convert.ToString(dtJobStatus.Rows[0]["OTHER"]) + "" + Convert.ToString(dtJobStatus.Rows[0]["TOKEN"]);
                                                                            URLStatus = URLStatus + "?CMPID=" + objMainClass.intCmpId + "&JOBID=" + NEWJOBID + "&STAGEID=" + JOBSTAGEID + "&JOBSTATUS=" + JOBSTATUSID + "&STATRES=AUTO ENTRY JOB CREATION&STATUPDATEDT=" + DateTime.Now.ToString() + "&UPDATEDATE=" + DateTime.Now.ToString() + "&CREATEBY=" + Convert.ToInt32(Session["USERID"]);
                                                                            var clientStatus = new RestClient(URLStatus);
                                                                            clientStatus.Timeout = -1;
                                                                            var requestStatus = new RestRequest(Method.POST);
                                                                            requestStatus.AddHeader("" + Convert.ToString(dtJobStatus.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtJobStatus.Rows[0]["KEYVALUE"]) + "");
                                                                            requestStatus.AddHeader("Content-Type", "application/json");
                                                                            //var jsonInputStatus = JsonConvert.SerializeObject(objReverseWaybillUpdate);
                                                                            //requestStatus.AddParameter("application/json", jsonInputStatus, ParameterType.RequestBody);
                                                                            IRestResponse responseStatus = clientStatus.Execute(requestStatus);

                                                                            if (JOBSTAGEID == 11)
                                                                            {
                                                                                DataTable dtInsertJC = new DataTable();
                                                                                dtInsertJC = objMainClass.GetWAData("CREATEJC", 1, "GETWADATA");
                                                                                if (dtInsertJC.Rows.Count > 0)
                                                                                {
                                                                                    JobCardMaster objJobCardMaster = new JobCardMaster();
                                                                                    objJobCardMaster.BACKCOVERFLAG = "Y";
                                                                                    objJobCardMaster.CMPID = objMainClass.intCmpId;
                                                                                    objJobCardMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                                    objJobCardMaster.CREATEDATE = Convert.ToDateTime(DateTime.Now).ToString();
                                                                                    objJobCardMaster.ITEMID = Convert.ToInt32(lblProductID);
                                                                                    objJobCardMaster.JCDT = Convert.ToDateTime(DateTime.Now).ToString();
                                                                                    objJobCardMaster.JCNO = "";
                                                                                    objJobCardMaster.JOBID = NEWJOBID;
                                                                                    objJobCardMaster.JOBIDSRNO = 1;
                                                                                    objJobCardMaster.JOBSTATUS = (int)STATUS.JCSaved;
                                                                                    objJobCardMaster.LOCCD = lblLocation.Text;
                                                                                    objJobCardMaster.PLANTCD = lblPlant.Text;
                                                                                    objJobCardMaster.QTY = 1;
                                                                                    objJobCardMaster.STAGEID = 0;// JOBSTAGEID;
                                                                                    objJobCardMaster.UOM = 1;
                                                                                    objJobCardMaster.WRKCNT = "WR01";

                                                                                    string URLJC = Convert.ToString(dtInsertJC.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertJC.Rows[0]["TOKEN"]);
                                                                                    var clientJC = new RestClient(URLJC);
                                                                                    clientJC.Timeout = -1;
                                                                                    var requestJC = new RestRequest(Method.POST);
                                                                                    requestJC.AddHeader("" + Convert.ToString(dtInsertJC.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertJC.Rows[0]["KEYVALUE"]) + "");
                                                                                    requestJC.AddHeader("Content-Type", "application/json");
                                                                                    var jsonInputJC = JsonConvert.SerializeObject(objJobCardMaster);
                                                                                    requestJC.AddParameter("application/json", jsonInputJC, ParameterType.RequestBody);
                                                                                    IRestResponse responseJC = clientJC.Execute(requestJC);

                                                                                    JobCardResponse objJobCardResponse = new JobCardResponse();
                                                                                    string jsonconnJC = responseJC.Content;
                                                                                    objJobCardResponse = JsonConvert.DeserializeObject<JobCardResponse>(jsonconnJC);

                                                                                    NEWJCNO = objJobCardResponse.JCNO;

                                                                                    if (NEWJCNO != null && NEWJCNO != "" && NEWJCNO != string.Empty)
                                                                                    {
                                                                                        hfJCNO.Value = NEWJCNO;
                                                                                        DataTable dtInsertJCDetails = new DataTable();
                                                                                        dtInsertJCDetails = objMainClass.GetWAData("INSERTJCDETAILS", 1, "GETWADATA");
                                                                                        if (dtInsertJCDetails.Rows.Count > 0)
                                                                                        {
                                                                                            #region 50 Inward Inspection Entry...

                                                                                            JobCardDetails objJobCardDetails = new JobCardDetails();
                                                                                            objJobCardDetails.ASCPARTCODE = "";
                                                                                            objJobCardDetails.CMPID = objMainClass.intCmpId;
                                                                                            objJobCardDetails.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                                            objJobCardDetails.CREATEDATE = Convert.ToDateTime(DateTime.Now).ToString();
                                                                                            objJobCardDetails.JCNO = NEWJCNO;
                                                                                            objJobCardDetails.JOBDONE = "NA";
                                                                                            objJobCardDetails.JOBDONEBY = Convert.ToInt32(Session["USERID"]);
                                                                                            objJobCardDetails.JOBPROBID = 146;
                                                                                            objJobCardDetails.JOBPROBID1 = 0;
                                                                                            objJobCardDetails.JOBPROBID2 = 0;
                                                                                            objJobCardDetails.JOBPROBID3 = 0;
                                                                                            objJobCardDetails.NEWIMEINO = "";
                                                                                            objJobCardDetails.NEXTSTAGEREQ = 14;
                                                                                            objJobCardDetails.NOTE = "OK FOR CHECK";
                                                                                            objJobCardDetails.PARTREPLACED = "";
                                                                                            objJobCardDetails.PARTREQ = "";
                                                                                            objJobCardDetails.PARTREQID = 0;
                                                                                            objJobCardDetails.PROBLEM = "OK FOR CHECK";
                                                                                            objJobCardDetails.PROBLEM1 = "";
                                                                                            objJobCardDetails.PROBLEM2 = "";
                                                                                            objJobCardDetails.PROBLEM3 = "";
                                                                                            objJobCardDetails.RESULT = 25;
                                                                                            objJobCardDetails.STAGEID = 50;
                                                                                            objJobCardDetails.STARTDT = DateTime.Now;
                                                                                            objJobCardDetails.ENDDT = DateTime.Now;
                                                                                            objJobCardDetails.JOBID = NEWJOBID;

                                                                                            string URLJCDetails = Convert.ToString(dtInsertJCDetails.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertJCDetails.Rows[0]["TOKEN"]);
                                                                                            var clientJCDetails = new RestClient(URLJCDetails);
                                                                                            clientJCDetails.Timeout = -1;
                                                                                            var requestJCDetails = new RestRequest(Method.POST);
                                                                                            requestJCDetails.AddHeader("" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYVALUE"]) + "");
                                                                                            requestJCDetails.AddHeader("Content-Type", "application/json");
                                                                                            var jsonInputJCDetails = JsonConvert.SerializeObject(objJobCardDetails);
                                                                                            requestJCDetails.AddParameter("application/json", jsonInputJCDetails, ParameterType.RequestBody);
                                                                                            IRestResponse responseES = clientJCDetails.Execute(requestJCDetails);

                                                                                            #endregion

                                                                                            #region 14 ELS Entry...

                                                                                            JobCardDetails objJobCardDetails1 = new JobCardDetails();
                                                                                            objJobCardDetails1.ASCPARTCODE = "";
                                                                                            objJobCardDetails1.CMPID = objMainClass.intCmpId;
                                                                                            objJobCardDetails1.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                                            objJobCardDetails1.CREATEDATE = Convert.ToDateTime(DateTime.Now).ToString();
                                                                                            objJobCardDetails1.JCNO = NEWJCNO;
                                                                                            objJobCardDetails1.JOBDONE = "NA";
                                                                                            objJobCardDetails1.JOBDONEBY = Convert.ToInt32(Session["USERID"]);
                                                                                            objJobCardDetails1.JOBPROBID = 146;
                                                                                            objJobCardDetails1.JOBPROBID1 = 0;
                                                                                            objJobCardDetails1.JOBPROBID2 = 0;
                                                                                            objJobCardDetails1.JOBPROBID3 = 0;
                                                                                            objJobCardDetails1.NEWIMEINO = "";
                                                                                            objJobCardDetails1.NEXTSTAGEREQ = 20;
                                                                                            objJobCardDetails1.NOTE = "OK FOR CHECK";
                                                                                            objJobCardDetails1.PARTREPLACED = "";
                                                                                            objJobCardDetails1.PARTREQ = "";
                                                                                            objJobCardDetails1.PARTREQID = 0;
                                                                                            objJobCardDetails1.PROBLEM = "OK FOR CHECK";
                                                                                            objJobCardDetails1.PROBLEM1 = "";
                                                                                            objJobCardDetails1.PROBLEM2 = "";
                                                                                            objJobCardDetails1.PROBLEM3 = "";
                                                                                            objJobCardDetails1.RESULT = 25;
                                                                                            objJobCardDetails1.STAGEID = 14;
                                                                                            objJobCardDetails1.STARTDT = DateTime.Now;
                                                                                            objJobCardDetails1.ENDDT = DateTime.Now;
                                                                                            objJobCardDetails1.JOBID = NEWJOBID;

                                                                                            string URLJCDetails1 = Convert.ToString(dtInsertJCDetails.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertJCDetails.Rows[0]["TOKEN"]);
                                                                                            var clientJCDetails1 = new RestClient(URLJCDetails1);
                                                                                            clientJCDetails1.Timeout = -1;
                                                                                            var requestJCDetails1 = new RestRequest(Method.POST);
                                                                                            requestJCDetails1.AddHeader("" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYVALUE"]) + "");
                                                                                            requestJCDetails1.AddHeader("Content-Type", "application/json");
                                                                                            var jsonInputJCDetails1 = JsonConvert.SerializeObject(objJobCardDetails1);
                                                                                            requestJCDetails1.AddParameter("application/json", jsonInputJCDetails1, ParameterType.RequestBody);
                                                                                            IRestResponse responseES1 = clientJCDetails1.Execute(requestJCDetails1);

                                                                                            #endregion

                                                                                            #region 20 QC1 Entry...

                                                                                            JobCardDetails objJobCardDetails2 = new JobCardDetails();
                                                                                            objJobCardDetails2.ASCPARTCODE = "";
                                                                                            objJobCardDetails2.CMPID = objMainClass.intCmpId;
                                                                                            objJobCardDetails2.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                                            objJobCardDetails2.CREATEDATE = Convert.ToDateTime(DateTime.Now).ToString();
                                                                                            objJobCardDetails2.JCNO = NEWJCNO;
                                                                                            objJobCardDetails2.JOBDONE = "NA";
                                                                                            objJobCardDetails2.JOBDONEBY = Convert.ToInt32(Session["USERID"]);
                                                                                            objJobCardDetails2.JOBPROBID = 146;
                                                                                            objJobCardDetails2.JOBPROBID1 = 0;
                                                                                            objJobCardDetails2.JOBPROBID2 = 0;
                                                                                            objJobCardDetails2.JOBPROBID3 = 0;
                                                                                            objJobCardDetails2.NEWIMEINO = "";
                                                                                            objJobCardDetails2.NEXTSTAGEREQ = 64;
                                                                                            objJobCardDetails2.NOTE = "OK FOR CHECK";
                                                                                            objJobCardDetails2.PARTREPLACED = "";
                                                                                            objJobCardDetails2.PARTREQ = "";
                                                                                            objJobCardDetails2.PARTREQID = 0;
                                                                                            objJobCardDetails2.PROBLEM = "OK FOR CHECK";
                                                                                            objJobCardDetails2.PROBLEM1 = "";
                                                                                            objJobCardDetails2.PROBLEM2 = "";
                                                                                            objJobCardDetails2.PROBLEM3 = "";
                                                                                            objJobCardDetails2.RESULT = 25;
                                                                                            objJobCardDetails2.STAGEID = 20;
                                                                                            objJobCardDetails2.STARTDT = DateTime.Now;
                                                                                            objJobCardDetails2.ENDDT = DateTime.Now;
                                                                                            objJobCardDetails2.JOBID = NEWJOBID;

                                                                                            string URLJCDetails2 = Convert.ToString(dtInsertJCDetails.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertJCDetails.Rows[0]["TOKEN"]);
                                                                                            var clientJCDetails2 = new RestClient(URLJCDetails2);
                                                                                            clientJCDetails2.Timeout = -1;
                                                                                            var requestJCDetails2 = new RestRequest(Method.POST);
                                                                                            requestJCDetails2.AddHeader("" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYVALUE"]) + "");
                                                                                            requestJCDetails2.AddHeader("Content-Type", "application/json");
                                                                                            var jsonInputJCDetails2 = JsonConvert.SerializeObject(objJobCardDetails2);
                                                                                            requestJCDetails2.AddParameter("application/json", jsonInputJCDetails2, ParameterType.RequestBody);
                                                                                            IRestResponse responseES2 = clientJCDetails2.Execute(requestJCDetails2);

                                                                                            #endregion

                                                                                            #region 64 PDI Entry...

                                                                                            JobCardDetails objJobCardDetails3 = new JobCardDetails();
                                                                                            objJobCardDetails3.ASCPARTCODE = "";
                                                                                            objJobCardDetails3.CMPID = objMainClass.intCmpId;
                                                                                            objJobCardDetails3.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                                            objJobCardDetails3.CREATEDATE = Convert.ToDateTime(DateTime.Now).ToString();
                                                                                            objJobCardDetails3.JCNO = NEWJCNO;
                                                                                            objJobCardDetails3.JOBDONE = "NA";
                                                                                            objJobCardDetails3.JOBDONEBY = Convert.ToInt32(Session["USERID"]);
                                                                                            objJobCardDetails3.JOBPROBID = 146;
                                                                                            objJobCardDetails3.JOBPROBID1 = 0;
                                                                                            objJobCardDetails3.JOBPROBID2 = 0;
                                                                                            objJobCardDetails3.JOBPROBID3 = 0;
                                                                                            objJobCardDetails3.NEWIMEINO = "";
                                                                                            objJobCardDetails3.NEXTSTAGEREQ = 59;
                                                                                            //if (hfSalesFrom.Value == "AMAZON")
                                                                                            //{
                                                                                            //    objJobCardDetails3.NOTE = "AMAZON";
                                                                                            //}
                                                                                            //else
                                                                                            //{
                                                                                            objJobCardDetails3.NOTE = "OK FOR CHECK";
                                                                                            //}

                                                                                            objJobCardDetails3.PARTREPLACED = "";
                                                                                            objJobCardDetails3.PARTREQ = "";
                                                                                            objJobCardDetails3.PARTREQID = 0;
                                                                                            objJobCardDetails3.PROBLEM = "OK FOR CHECK";
                                                                                            objJobCardDetails3.PROBLEM1 = "";
                                                                                            objJobCardDetails3.PROBLEM2 = "";
                                                                                            objJobCardDetails3.PROBLEM3 = "";
                                                                                            objJobCardDetails3.RESULT = 25;
                                                                                            objJobCardDetails3.STAGEID = 64;
                                                                                            objJobCardDetails3.STARTDT = DateTime.Now;
                                                                                            objJobCardDetails3.ENDDT = DateTime.Now;
                                                                                            objJobCardDetails3.JOBID = NEWJOBID;

                                                                                            string URLJCDetails3 = Convert.ToString(dtInsertJCDetails.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertJCDetails.Rows[0]["TOKEN"]);
                                                                                            var clientJCDetails3 = new RestClient(URLJCDetails3);
                                                                                            clientJCDetails3.Timeout = -1;
                                                                                            var requestJCDetails3 = new RestRequest(Method.POST);
                                                                                            requestJCDetails3.AddHeader("" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYVALUE"]) + "");
                                                                                            requestJCDetails3.AddHeader("Content-Type", "application/json");
                                                                                            var jsonInputJCDetails3 = JsonConvert.SerializeObject(objJobCardDetails3);
                                                                                            requestJCDetails3.AddParameter("application/json", jsonInputJCDetails3, ParameterType.RequestBody);
                                                                                            IRestResponse responseES3 = clientJCDetails3.Execute(requestJCDetails3);

                                                                                            #endregion

                                                                                            #region 59 Packed Entry...

                                                                                            JobCardDetails objJobCardDetails4 = new JobCardDetails();
                                                                                            objJobCardDetails4.ASCPARTCODE = "";
                                                                                            objJobCardDetails4.CMPID = objMainClass.intCmpId;
                                                                                            objJobCardDetails4.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                                            objJobCardDetails4.CREATEDATE = Convert.ToDateTime(DateTime.Now).ToString();
                                                                                            objJobCardDetails4.JCNO = NEWJCNO;
                                                                                            objJobCardDetails4.JOBDONE = "NA";
                                                                                            objJobCardDetails4.JOBDONEBY = Convert.ToInt32(Session["USERID"]);
                                                                                            objJobCardDetails4.JOBPROBID = 146;
                                                                                            objJobCardDetails4.JOBPROBID1 = 0;
                                                                                            objJobCardDetails4.JOBPROBID2 = 0;
                                                                                            objJobCardDetails4.JOBPROBID3 = 0;
                                                                                            objJobCardDetails4.NEWIMEINO = "";
                                                                                            objJobCardDetails4.NEXTSTAGEREQ = 59;
                                                                                            objJobCardDetails4.NOTE = "OK FOR CHECK";
                                                                                            objJobCardDetails4.PARTREPLACED = "";
                                                                                            objJobCardDetails4.PARTREQ = "";
                                                                                            objJobCardDetails4.PARTREQID = 0;
                                                                                            objJobCardDetails4.PROBLEM = "OK FOR CHECK";
                                                                                            objJobCardDetails4.PROBLEM1 = "";
                                                                                            objJobCardDetails4.PROBLEM2 = "";
                                                                                            objJobCardDetails4.PROBLEM3 = "";
                                                                                            objJobCardDetails4.RESULT = 25;
                                                                                            objJobCardDetails4.STAGEID = 59;
                                                                                            objJobCardDetails4.STARTDT = DateTime.Now;
                                                                                            objJobCardDetails4.ENDDT = DateTime.Now;
                                                                                            objJobCardDetails4.JOBID = NEWJOBID;

                                                                                            string URLJCDetails4 = Convert.ToString(dtInsertJCDetails.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertJCDetails.Rows[0]["TOKEN"]);
                                                                                            var clientJCDetails4 = new RestClient(URLJCDetails4);
                                                                                            clientJCDetails4.Timeout = -1;
                                                                                            var requestJCDetails4 = new RestRequest(Method.POST);
                                                                                            requestJCDetails4.AddHeader("" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYVALUE"]) + "");
                                                                                            requestJCDetails4.AddHeader("Content-Type", "application/json");
                                                                                            var jsonInputJCDetails4 = JsonConvert.SerializeObject(objJobCardDetails4);
                                                                                            requestJCDetails4.AddParameter("application/json", jsonInputJCDetails4, ParameterType.RequestBody);
                                                                                            IRestResponse responseES4 = clientJCDetails4.Execute(requestJCDetails4);

                                                                                            #endregion


                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Job Card Details API Not Found. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Job Card Not Created. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                                                                                    }

                                                                                }
                                                                                else
                                                                                {
                                                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Job Card Not Created. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                                                                                }
                                                                            }

                                                                            if (JOBSTAGEID == 19)
                                                                            {
                                                                                DataTable dtInsertES = new DataTable();
                                                                                dtInsertES = objMainClass.GetWAData("CREATEESTIMATE", 1, "GETWADATA");

                                                                                if (dtInsertES.Rows.Count > 0)
                                                                                {

                                                                                    #region Estimate Master...
                                                                                    EstimateMaster objEstimateMaster = new EstimateMaster();
                                                                                    objEstimateMaster.ASCCHG = 550;
                                                                                    objEstimateMaster.CMPID = objMainClass.intCmpId;
                                                                                    objEstimateMaster.COSTAMT_PART = 0;
                                                                                    objEstimateMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                                    objEstimateMaster.CREATEDATE = Convert.ToDateTime(DateTime.Now).ToString();
                                                                                    objEstimateMaster.DISCAMT = 0;
                                                                                    objEstimateMaster.ESTIAMT_PART = 0;
                                                                                    objEstimateMaster.ESTIAMT_SERV = 550;
                                                                                    objEstimateMaster.ESTIDT = DateTime.Now;
                                                                                    objEstimateMaster.ESTINO = "";
                                                                                    objEstimateMaster.ETD = DateTime.Now;
                                                                                    objEstimateMaster.HSNEW = "N";
                                                                                    objEstimateMaster.ISRETURN = "N";
                                                                                    objEstimateMaster.JOBID = NEWJOBID;
                                                                                    objEstimateMaster.JOBIDSRNO = 1;
                                                                                    objEstimateMaster.LIQUIDDAMAGE = "N";
                                                                                    objEstimateMaster.LOGICHG = 0;
                                                                                    objEstimateMaster.NWREASON = 0;
                                                                                    objEstimateMaster.PARTDESC = "NA";
                                                                                    objEstimateMaster.PAYMODE = 8;
                                                                                    objEstimateMaster.PURDT = DateTime.Now;
                                                                                    objEstimateMaster.PURREF = "";
                                                                                    objEstimateMaster.RBATTERYNO = "";
                                                                                    objEstimateMaster.REMARK = "AUTO GENRATED AGAINST SO";
                                                                                    objEstimateMaster.RIMEINO = lblIMEI1.Text;
                                                                                    objEstimateMaster.RITEMID = 0;
                                                                                    objEstimateMaster.RPRODMAKE = lblMake.Text;
                                                                                    objEstimateMaster.RPRODMODEL = lblModel.Text;
                                                                                    objEstimateMaster.SERVDESC = "NA";
                                                                                    objEstimateMaster.STAGEID = (int)STAGE.EstimateCreate;
                                                                                    objEstimateMaster.STATRES = "AUTO GENRATED AGAINST SO";
                                                                                    objEstimateMaster.STATUS = (int)STATUS.Estimated;
                                                                                    objEstimateMaster.TOTALLOSS = "N";
                                                                                    objEstimateMaster.TRANCHG = 0;
                                                                                    objEstimateMaster.TRANCHGPCT = 0;

                                                                                    #endregion

                                                                                    string URLES = Convert.ToString(dtInsertES.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertES.Rows[0]["TOKEN"]);
                                                                                    var clientES = new RestClient(URLES);
                                                                                    clientES.Timeout = -1;
                                                                                    var requestES = new RestRequest(Method.POST);
                                                                                    requestES.AddHeader("" + Convert.ToString(dtInsertES.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertES.Rows[0]["KEYVALUE"]) + "");
                                                                                    requestES.AddHeader("Content-Type", "application/json");
                                                                                    var jsonInputES = JsonConvert.SerializeObject(objEstimateMaster);
                                                                                    requestES.AddParameter("application/json", jsonInputES, ParameterType.RequestBody);
                                                                                    IRestResponse responseES = clientES.Execute(requestES);

                                                                                    EstimateResponse objEstimateResponse = new EstimateResponse();
                                                                                    string jsonconnEstimate = responseES.Content;
                                                                                    objEstimateResponse = JsonConvert.DeserializeObject<EstimateResponse>(jsonconnEstimate);

                                                                                    NEWESTINO = objEstimateResponse.ESTINO;
                                                                                    hfEstiNo.Value = NEWESTINO;
                                                                                }
                                                                                else
                                                                                {
                                                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Estimate Not Created. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                                                                                }

                                                                            }

                                                                            if (JOBSTAGEID == 39)
                                                                            {
                                                                                if (NEWESTINO != "" && NEWESTINO != null && NEWESTINO != string.Empty)
                                                                                {
                                                                                    DataTable dtInsertESAPRV = new DataTable();
                                                                                    dtInsertESAPRV = objMainClass.GetWAData("APPROVEESTIMATE", 1, "GETWADATA");

                                                                                    if (dtInsertESAPRV.Rows.Count > 0)
                                                                                    {

                                                                                        EstimateApproval objEstimateApproval = new EstimateApproval();
                                                                                        objEstimateApproval.APRVBY1 = 124;
                                                                                        objEstimateApproval.APRVDT1 = DateTime.Now;
                                                                                        objEstimateApproval.APRVFLAG = (int)APRVTYPE.APPROVED;
                                                                                        objEstimateApproval.APRVNO1 = "APNP";
                                                                                        objEstimateApproval.APRVNOTE = "";
                                                                                        objEstimateApproval.CMPID = objMainClass.intCmpId;
                                                                                        objEstimateApproval.CUSTAPRVBY = "";
                                                                                        objEstimateApproval.ESTINO = NEWESTINO;
                                                                                        objEstimateApproval.PAYMODE = 8;
                                                                                        objEstimateApproval.REJRES = 0;
                                                                                        objEstimateApproval.STAGEID = (int)STAGE.EstimatApproved;
                                                                                        objEstimateApproval.STATUS = (int)STATUS.Approved;
                                                                                        objEstimateApproval.UPDATEBY = Convert.ToInt32(Session["USERID"]);
                                                                                        objEstimateApproval.UPDATEDATE = Convert.ToDateTime(DateTime.Now).ToString();


                                                                                        string URLESAPRV = Convert.ToString(dtInsertESAPRV.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertESAPRV.Rows[0]["TOKEN"]);
                                                                                        var clientESAPRV = new RestClient(URLESAPRV);
                                                                                        clientESAPRV.Timeout = -1;
                                                                                        var requestESAPRV = new RestRequest(Method.POST);
                                                                                        requestESAPRV.AddHeader("" + Convert.ToString(dtInsertESAPRV.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertESAPRV.Rows[0]["KEYVALUE"]) + "");
                                                                                        requestESAPRV.AddHeader("Content-Type", "application/json");
                                                                                        var jsonInputESAPRV = JsonConvert.SerializeObject(objEstimateApproval);
                                                                                        requestESAPRV.AddParameter("application/json", jsonInputESAPRV, ParameterType.RequestBody);
                                                                                        IRestResponse responseESAPRV = clientESAPRV.Execute(requestESAPRV);

                                                                                        EstimateApprovalResponse objEstimateApprovalResponse = new EstimateApprovalResponse();
                                                                                        string jsonconnEstimateAPRV = responseESAPRV.Content;
                                                                                        objEstimateApprovalResponse = JsonConvert.DeserializeObject<EstimateApprovalResponse>(jsonconnEstimateAPRV);

                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Estimate Not Approved. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Estimate Not Approved. Estimate Number not found. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                                                                                }
                                                                            }

                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Job Status Update API not Found. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Stages Insert API not Found. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                                                            }


                                                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. Job ID : " + NEWJOBID + "\");", true);
                                                        }
                                                        else
                                                        {
                                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Stages Not Updated. Job ID : " + NEWJOBID + "\");", true);
                                                        }



                                                    }
                                                    else
                                                    {
                                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Reverse Waybill Update API not Found. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                                                    }


                                                }
                                                else
                                                {
                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Reverse Waybill Update API not Found. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                                                }

                                            }
                                            else
                                            {
                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Job Sheet creation API not found in Database. Please Contact API Provider.');", true);
                                            }
                                            #endregion

                                        }
                                    }
                                    else
                                    {

                                        #region Job Id Creation Code...
                                        #region JobSheet Details...

                                        List<JobSheetDetail> objJobSheetDetail = new List<JobSheetDetail>();
                                        objJobSheetDetail.Add(new JobSheetDetail
                                        {
                                            CMPID = objMainClass.intCmpId,
                                            SRNO = 1,
                                            ITEMID = Convert.ToInt32(lblProductID),
                                            ITEMDESC = lblProductName,
                                            QTY = 1,
                                            UOM = 1,
                                            ITEMVALUE = Convert.ToDecimal(250),
                                            RATE = Convert.ToDecimal(250),
                                            PLANTCD = lblPlant.Text,
                                            LOCCD = lblLocation.Text,
                                            EXTWARNO = "NA",
                                            PRODMAKE = lblMake.Text,
                                            PRODMODEL = lblModel.Text,
                                            IMEINO = lblIMEI1.Text,
                                            JOBTYPE = "OTHER : ONLY SERVICE",
                                            JOBDESC = "FOR CHECK",
                                            REFINVNO = "-",
                                            REFINVDT = Convert.ToDateTime(DateTime.Now).AddYears(-2).ToString(),
                                            REFINVAMT = Convert.ToDecimal(10000),
                                            INSUCO = "0000040003",
                                            NOTE = "",
                                            PRODCOND = "",
                                            WAYBILLNO = "",
                                            REVDCNO = "",
                                            FWAYBILLNO = "",
                                            DCNO = "",
                                            BATTERYNO = lblMake.Text,
                                            REVTRANNAME = "",
                                            FTRANNAME = "",
                                            WAYBILLSTATUS = "",
                                            FWAYBILLSTATUS = "",
                                            DELICONFDT = "",
                                            LOCKCODE = "",
                                            REVPICKUPDT = "",
                                            BACKCOVERFLAG = "Y",
                                            REVDELIDT = "",
                                            FPICKUPDT = "",
                                            PHYIMEINO = lblIMEI1.Text,
                                            FESTIDELDT = "",
                                            REVESTIDELDT = "",
                                            IMEINO2 = lblIMEI2.Text,
                                            CODWAYBILLNO = "",
                                            FEDEXPICKUP = 0,
                                            PRODCOLOR = hfColor.Value,
                                            DELIVERYTO = "",
                                            ID = 0,
                                            JOBID = "",
                                            GRADE = lblGrade.Text,
                                            BILLAMT = Convert.ToDecimal(lblPrice.Text)
                                        });

                                        #endregion

                                        #region Addess Details...

                                        AddressDetail objAddressDetail = new AddressDetail();
                                        objAddressDetail.CMPID = objMainClass.intCmpId;
                                        objAddressDetail.REFID = "";
                                        objAddressDetail.REFTYPE = "JS";
                                        objAddressDetail.ADDOF = "G";
                                        objAddressDetail.ADDR1 = "MOBEX";
                                        objAddressDetail.ADDR2 = "MOBEX";
                                        objAddressDetail.ADDR3 = "";
                                        objAddressDetail.CITY = "AHMEDABAD";
                                        objAddressDetail.STCD = 1;
                                        objAddressDetail.CNCD = "IN";
                                        objAddressDetail.POSTALCODE = "380023";
                                        objAddressDetail.CONTACTPERSON = "";
                                        objAddressDetail.CONTACTNO = "";
                                        objAddressDetail.CONTACTPERSON2 = "";
                                        objAddressDetail.CONTACTNO2 = "";
                                        objAddressDetail.CONTACTPERSON3 = "";
                                        objAddressDetail.CONTACTNO3 = "";
                                        objAddressDetail.MOBILENO = "1234567890";
                                        objAddressDetail.MOBILENO2 = "";
                                        objAddressDetail.MOBILENO3 = "";
                                        objAddressDetail.FAXNO = "";
                                        objAddressDetail.EMAILID = "";
                                        objAddressDetail.WEBSITE = "";
                                        objAddressDetail.LAT = "";
                                        objAddressDetail.LONG = "";
                                        #endregion

                                        #region JobSheet Master...

                                        JobSheetMaster objJobSheetMaster = new JobSheetMaster();
                                        objJobSheetMaster.CMPID = objMainClass.intCmpId;
                                        objJobSheetMaster.JOBDT = Convert.ToDateTime(DateTime.Now).ToShortDateString();
                                        objJobSheetMaster.JSTYPE = 17;
                                        objJobSheetMaster.SHIPTOPARTY = "0000000001";
                                        objJobSheetMaster.BILLTOPARTY = "0000010003";
                                        objJobSheetMaster.ENDCUST = "MOBEX";
                                        objJobSheetMaster.REMARK = "";
                                        objJobSheetMaster.JOBSTATUS = Convert.ToInt32(JOBSTATUS.Saved);
                                        objJobSheetMaster.STATRES = "";
                                        objJobSheetMaster.STATUPDDT = Convert.ToDateTime(DateTime.Now).ToString();
                                        objJobSheetMaster.STATUPDBY = Convert.ToInt32(Session["USERID"]);
                                        objJobSheetMaster.SEGMENT = lblSegment.Text;
                                        objJobSheetMaster.DISTCHNL = lblDistChnl.Text;
                                        objJobSheetMaster.ISRETURN = lblOldJobId == "" || lblOldJobId == null || lblOldJobId == string.Empty ? "N" : "Y";
                                        objJobSheetMaster.REFJOBID = lblOldJobId == "" || lblOldJobId == null || lblOldJobId == string.Empty ? "" : objMainClass.strConvertZeroPadding(lblOldJobId);
                                        objJobSheetMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                        objJobSheetMaster.CREATEDATE = Convert.ToDateTime(DateTime.Now).ToString();
                                        objJobSheetMaster.JDAREF = lblReverseWaybill.Text;//hfSONO.Value;
                                        objJobSheetMaster.JDAREFDT = Convert.ToDateTime(DateTime.Now).ToString();
                                        objJobSheetMaster.PICKUPFROM = "0000000001";
                                        objJobSheetMaster.SHIPTO = "0000000001";
                                        objJobSheetMaster.APRVFLAG = "Y";
                                        objJobSheetMaster.INQNO = 0;
                                        objJobSheetMaster.OOW = 0;
                                        objJobSheetMaster.JWREFNO = lblReverseCourier.Text;//hfSONO.Value;
                                        objJobSheetMaster.PONO = "";
                                        objJobSheetMaster.SLRNO = "";
                                        objJobSheetMaster.LISTINGID = 0;
                                        objJobSheetMaster.ITEMCODE = lblItemCcde.Text;
                                        objJobSheetMaster.AddressDetail = objAddressDetail;
                                        objJobSheetMaster.lstJobDetail = objJobSheetDetail;
                                        #endregion




                                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                        System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };

                                        string URL = Convert.ToString(dtAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtAPI.Rows[0]["TOKEN"]);

                                        var client = new RestClient(URL);
                                        client.Timeout = -1;
                                        var request = new RestRequest(Method.POST);
                                        request.AddHeader("" + Convert.ToString(dtAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtAPI.Rows[0]["KEYVALUE"]) + "");
                                        request.AddHeader("Content-Type", "application/json");
                                        var jsonInput = JsonConvert.SerializeObject(objJobSheetMaster);
                                        request.AddParameter("application/json", jsonInput, ParameterType.RequestBody);
                                        IRestResponse response = client.Execute(request);

                                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                                        {


                                            JobSheetResponse objJobSheetResponse = new JobSheetResponse();
                                            string jsonconn = response.Content;
                                            objJobSheetResponse = JsonConvert.DeserializeObject<JobSheetResponse>(jsonconn);

                                            string NEWJOBID = Convert.ToString(objJobSheetResponse.JOBNO);

                                            dtJobId.Rows.Add("'" + lblIMEI1.Text, NEWJOBID, "", "");
                                            isucess++;

                                            hfNewJobID.Value = NEWJOBID;
                                            string NEWESTINO = "";
                                            string NEWJCNO = "";
                                            DataTable dtReverseWayAPI = new DataTable();
                                            dtReverseWayAPI = objMainClass.GetWAData("UPDATEREVWAYBILL", 1, "GETWADATA");

                                            if (dtReverseWayAPI.Rows.Count > 0)
                                            {
                                                ReverseWaybillUpdate objReverseWaybillUpdate = new ReverseWaybillUpdate();
                                                objReverseWaybillUpdate.CMPID = objMainClass.intCmpId;
                                                objReverseWaybillUpdate.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                objReverseWaybillUpdate.DOCTYPE = "JS";
                                                objReverseWaybillUpdate.JOBID = NEWJOBID;
                                                objReverseWaybillUpdate.REVTRANNAME = lblReverseCourier.Text;
                                                objReverseWaybillUpdate.JOBSTATUS = (int)STATUS.RevWayBillGen;
                                                objReverseWaybillUpdate.STAGEID = (int)STAGE.RevWayBillNo;
                                                objReverseWaybillUpdate.STATRES = "AUTO ENTRY JOB CREATION";
                                                objReverseWaybillUpdate.WAYBILLNO = lblReverseWaybill.Text;
                                                objReverseWaybillUpdate.WAYBILLSTATUS = "";

                                                string URLRevWaybill = Convert.ToString(dtReverseWayAPI.Rows[0]["OTHER"]) + "" + Convert.ToString(dtReverseWayAPI.Rows[0]["TOKEN"]);

                                                var clientRevWaybill = new RestClient(URLRevWaybill);
                                                clientRevWaybill.Timeout = -1;
                                                var requestRevWabill = new RestRequest(Method.POST);
                                                requestRevWabill.AddHeader("" + Convert.ToString(dtReverseWayAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtReverseWayAPI.Rows[0]["KEYVALUE"]) + "");
                                                requestRevWabill.AddHeader("Content-Type", "application/json");
                                                var jsonInputRevWaybill = JsonConvert.SerializeObject(objReverseWaybillUpdate);
                                                requestRevWabill.AddParameter("application/json", jsonInputRevWaybill, ParameterType.RequestBody);
                                                IRestResponse responserevwaybill = clientRevWaybill.Execute(requestRevWabill);
                                                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                                                {

                                                    DataTable dtStageSeq = new DataTable();
                                                    dtStageSeq = objMainClass.GetSegmentStageData(11, lblSegment.Text, "GETSTAGEREQ");

                                                    if (dtStageSeq.Rows.Count > 0)
                                                    {
                                                        DataTable dtInsertStage = new DataTable();
                                                        dtInsertStage = objMainClass.GetWAData("INSERTSTAGE", 1, "GETWADATA");

                                                        DataTable dtJobStatus = new DataTable();
                                                        dtJobStatus = objMainClass.GetWAData("UPDATESTATUS", 1, "GETWADATA");

                                                        if (dtInsertStage.Rows.Count > 0)
                                                        {
                                                            if (dtJobStatus.Rows.Count > 0)
                                                            {
                                                                for (int s = 0; s < dtStageSeq.Rows.Count; s++)
                                                                {
                                                                    if (Convert.ToInt32(dtStageSeq.Rows[s]["STAGESEQ"]) < 15)
                                                                    {

                                                                        int JOBSTAGEID = Convert.ToInt32(dtStageSeq.Rows[s]["STAGEID"]);
                                                                        int JOBSTATUSID = objMainClass.GetStatusByStageID(JOBSTAGEID);


                                                                        string URLStage = Convert.ToString(dtInsertStage.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertStage.Rows[0]["TOKEN"]);
                                                                        URLStage = URLStage + "?DOCNO=" + NEWJOBID + "&DOCTYPE=JS&STAGEID=" + JOBSTAGEID + "&STATRES=AUTO ENTRY JOB CREATION&CREATBY=" + Convert.ToInt32(Session["USERID"]);
                                                                        var clientStage = new RestClient(URLStage);
                                                                        clientStage.Timeout = -1;
                                                                        var requestStage = new RestRequest(Method.POST);
                                                                        requestStage.AddHeader("" + Convert.ToString(dtInsertStage.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertStage.Rows[0]["KEYVALUE"]) + "");
                                                                        requestStage.AddHeader("Content-Type", "application/json");
                                                                        //var jsonInputStage = JsonConvert.SerializeObject(objReverseWaybillUpdate);
                                                                        //requestStage.AddParameter("application/json", jsonInputStage, ParameterType.RequestBody);
                                                                        IRestResponse responseStage = clientStage.Execute(requestStage);


                                                                        string URLStatus = Convert.ToString(dtJobStatus.Rows[0]["OTHER"]) + "" + Convert.ToString(dtJobStatus.Rows[0]["TOKEN"]);
                                                                        URLStatus = URLStatus + "?CMPID=" + objMainClass.intCmpId + "&JOBID=" + NEWJOBID + "&STAGEID=" + JOBSTAGEID + "&JOBSTATUS=" + JOBSTATUSID + "&STATRES=AUTO ENTRY JOB CREATION&STATUPDATEDT=" + DateTime.Now.ToString() + "&UPDATEDATE=" + DateTime.Now.ToString() + "&CREATEBY=" + Convert.ToInt32(Session["USERID"]);
                                                                        var clientStatus = new RestClient(URLStatus);
                                                                        clientStatus.Timeout = -1;
                                                                        var requestStatus = new RestRequest(Method.POST);
                                                                        requestStatus.AddHeader("" + Convert.ToString(dtJobStatus.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtJobStatus.Rows[0]["KEYVALUE"]) + "");
                                                                        requestStatus.AddHeader("Content-Type", "application/json");
                                                                        //var jsonInputStatus = JsonConvert.SerializeObject(objReverseWaybillUpdate);
                                                                        //requestStatus.AddParameter("application/json", jsonInputStatus, ParameterType.RequestBody);
                                                                        IRestResponse responseStatus = clientStatus.Execute(requestStatus);

                                                                        if (JOBSTAGEID == 11)
                                                                        {
                                                                            DataTable dtInsertJC = new DataTable();
                                                                            dtInsertJC = objMainClass.GetWAData("CREATEJC", 1, "GETWADATA");
                                                                            if (dtInsertJC.Rows.Count > 0)
                                                                            {
                                                                                JobCardMaster objJobCardMaster = new JobCardMaster();
                                                                                objJobCardMaster.BACKCOVERFLAG = "Y";
                                                                                objJobCardMaster.CMPID = objMainClass.intCmpId;
                                                                                objJobCardMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                                objJobCardMaster.CREATEDATE = Convert.ToDateTime(DateTime.Now).ToString();
                                                                                objJobCardMaster.ITEMID = Convert.ToInt32(lblProductID);
                                                                                objJobCardMaster.JCDT = Convert.ToDateTime(DateTime.Now).ToString();
                                                                                objJobCardMaster.JCNO = "";
                                                                                objJobCardMaster.JOBID = NEWJOBID;
                                                                                objJobCardMaster.JOBIDSRNO = 1;
                                                                                objJobCardMaster.JOBSTATUS = (int)STATUS.JCSaved;
                                                                                objJobCardMaster.LOCCD = lblLocation.Text;
                                                                                objJobCardMaster.PLANTCD = lblPlant.Text;
                                                                                objJobCardMaster.QTY = 1;
                                                                                objJobCardMaster.STAGEID = 0;// JOBSTAGEID;
                                                                                objJobCardMaster.UOM = 1;
                                                                                objJobCardMaster.WRKCNT = "WR01";

                                                                                string URLJC = Convert.ToString(dtInsertJC.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertJC.Rows[0]["TOKEN"]);
                                                                                var clientJC = new RestClient(URLJC);
                                                                                clientJC.Timeout = -1;
                                                                                var requestJC = new RestRequest(Method.POST);
                                                                                requestJC.AddHeader("" + Convert.ToString(dtInsertJC.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertJC.Rows[0]["KEYVALUE"]) + "");
                                                                                requestJC.AddHeader("Content-Type", "application/json");
                                                                                var jsonInputJC = JsonConvert.SerializeObject(objJobCardMaster);
                                                                                requestJC.AddParameter("application/json", jsonInputJC, ParameterType.RequestBody);
                                                                                IRestResponse responseJC = clientJC.Execute(requestJC);

                                                                                JobCardResponse objJobCardResponse = new JobCardResponse();
                                                                                string jsonconnJC = responseJC.Content;
                                                                                objJobCardResponse = JsonConvert.DeserializeObject<JobCardResponse>(jsonconnJC);

                                                                                NEWJCNO = objJobCardResponse.JCNO;

                                                                                if (NEWJCNO != null && NEWJCNO != "" && NEWJCNO != string.Empty)
                                                                                {
                                                                                    hfJCNO.Value = NEWJCNO;
                                                                                    DataTable dtInsertJCDetails = new DataTable();
                                                                                    dtInsertJCDetails = objMainClass.GetWAData("INSERTJCDETAILS", 1, "GETWADATA");
                                                                                    if (dtInsertJCDetails.Rows.Count > 0)
                                                                                    {
                                                                                        #region 50 Inward Inspection Entry...

                                                                                        JobCardDetails objJobCardDetails = new JobCardDetails();
                                                                                        objJobCardDetails.ASCPARTCODE = "";
                                                                                        objJobCardDetails.CMPID = objMainClass.intCmpId;
                                                                                        objJobCardDetails.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                                        objJobCardDetails.CREATEDATE = Convert.ToDateTime(DateTime.Now).ToString();
                                                                                        objJobCardDetails.JCNO = NEWJCNO;
                                                                                        objJobCardDetails.JOBDONE = "NA";
                                                                                        objJobCardDetails.JOBDONEBY = Convert.ToInt32(Session["USERID"]);
                                                                                        objJobCardDetails.JOBPROBID = 146;
                                                                                        objJobCardDetails.JOBPROBID1 = 0;
                                                                                        objJobCardDetails.JOBPROBID2 = 0;
                                                                                        objJobCardDetails.JOBPROBID3 = 0;
                                                                                        objJobCardDetails.NEWIMEINO = "";
                                                                                        objJobCardDetails.NEXTSTAGEREQ = 14;
                                                                                        objJobCardDetails.NOTE = "OK FOR CHECK";
                                                                                        objJobCardDetails.PARTREPLACED = "";
                                                                                        objJobCardDetails.PARTREQ = "";
                                                                                        objJobCardDetails.PARTREQID = 0;
                                                                                        objJobCardDetails.PROBLEM = "OK FOR CHECK";
                                                                                        objJobCardDetails.PROBLEM1 = "";
                                                                                        objJobCardDetails.PROBLEM2 = "";
                                                                                        objJobCardDetails.PROBLEM3 = "";
                                                                                        objJobCardDetails.RESULT = 25;
                                                                                        objJobCardDetails.STAGEID = 50;
                                                                                        objJobCardDetails.STARTDT = DateTime.Now;
                                                                                        objJobCardDetails.ENDDT = DateTime.Now;
                                                                                        objJobCardDetails.JOBID = NEWJOBID;

                                                                                        string URLJCDetails = Convert.ToString(dtInsertJCDetails.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertJCDetails.Rows[0]["TOKEN"]);
                                                                                        var clientJCDetails = new RestClient(URLJCDetails);
                                                                                        clientJCDetails.Timeout = -1;
                                                                                        var requestJCDetails = new RestRequest(Method.POST);
                                                                                        requestJCDetails.AddHeader("" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYVALUE"]) + "");
                                                                                        requestJCDetails.AddHeader("Content-Type", "application/json");
                                                                                        var jsonInputJCDetails = JsonConvert.SerializeObject(objJobCardDetails);
                                                                                        requestJCDetails.AddParameter("application/json", jsonInputJCDetails, ParameterType.RequestBody);
                                                                                        IRestResponse responseES = clientJCDetails.Execute(requestJCDetails);

                                                                                        #endregion

                                                                                        #region 14 ELS Entry...

                                                                                        JobCardDetails objJobCardDetails1 = new JobCardDetails();
                                                                                        objJobCardDetails1.ASCPARTCODE = "";
                                                                                        objJobCardDetails1.CMPID = objMainClass.intCmpId;
                                                                                        objJobCardDetails1.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                                        objJobCardDetails1.CREATEDATE = Convert.ToDateTime(DateTime.Now).ToString();
                                                                                        objJobCardDetails1.JCNO = NEWJCNO;
                                                                                        objJobCardDetails1.JOBDONE = "NA";
                                                                                        objJobCardDetails1.JOBDONEBY = Convert.ToInt32(Session["USERID"]);
                                                                                        objJobCardDetails1.JOBPROBID = 146;
                                                                                        objJobCardDetails1.JOBPROBID1 = 0;
                                                                                        objJobCardDetails1.JOBPROBID2 = 0;
                                                                                        objJobCardDetails1.JOBPROBID3 = 0;
                                                                                        objJobCardDetails1.NEWIMEINO = "";
                                                                                        objJobCardDetails1.NEXTSTAGEREQ = 20;
                                                                                        objJobCardDetails1.NOTE = "OK FOR CHECK";
                                                                                        objJobCardDetails1.PARTREPLACED = "";
                                                                                        objJobCardDetails1.PARTREQ = "";
                                                                                        objJobCardDetails1.PARTREQID = 0;
                                                                                        objJobCardDetails1.PROBLEM = "OK FOR CHECK";
                                                                                        objJobCardDetails1.PROBLEM1 = "";
                                                                                        objJobCardDetails1.PROBLEM2 = "";
                                                                                        objJobCardDetails1.PROBLEM3 = "";
                                                                                        objJobCardDetails1.RESULT = 25;
                                                                                        objJobCardDetails1.STAGEID = 14;
                                                                                        objJobCardDetails1.STARTDT = DateTime.Now;
                                                                                        objJobCardDetails1.ENDDT = DateTime.Now;
                                                                                        objJobCardDetails1.JOBID = NEWJOBID;

                                                                                        string URLJCDetails1 = Convert.ToString(dtInsertJCDetails.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertJCDetails.Rows[0]["TOKEN"]);
                                                                                        var clientJCDetails1 = new RestClient(URLJCDetails1);
                                                                                        clientJCDetails1.Timeout = -1;
                                                                                        var requestJCDetails1 = new RestRequest(Method.POST);
                                                                                        requestJCDetails1.AddHeader("" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYVALUE"]) + "");
                                                                                        requestJCDetails1.AddHeader("Content-Type", "application/json");
                                                                                        var jsonInputJCDetails1 = JsonConvert.SerializeObject(objJobCardDetails1);
                                                                                        requestJCDetails1.AddParameter("application/json", jsonInputJCDetails1, ParameterType.RequestBody);
                                                                                        IRestResponse responseES1 = clientJCDetails1.Execute(requestJCDetails1);

                                                                                        #endregion

                                                                                        #region 20 QC1 Entry...

                                                                                        JobCardDetails objJobCardDetails2 = new JobCardDetails();
                                                                                        objJobCardDetails2.ASCPARTCODE = "";
                                                                                        objJobCardDetails2.CMPID = objMainClass.intCmpId;
                                                                                        objJobCardDetails2.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                                        objJobCardDetails2.CREATEDATE = Convert.ToDateTime(DateTime.Now).ToString();
                                                                                        objJobCardDetails2.JCNO = NEWJCNO;
                                                                                        objJobCardDetails2.JOBDONE = "NA";
                                                                                        objJobCardDetails2.JOBDONEBY = Convert.ToInt32(Session["USERID"]);
                                                                                        objJobCardDetails2.JOBPROBID = 146;
                                                                                        objJobCardDetails2.JOBPROBID1 = 0;
                                                                                        objJobCardDetails2.JOBPROBID2 = 0;
                                                                                        objJobCardDetails2.JOBPROBID3 = 0;
                                                                                        objJobCardDetails2.NEWIMEINO = "";
                                                                                        objJobCardDetails2.NEXTSTAGEREQ = 64;
                                                                                        objJobCardDetails2.NOTE = "OK FOR CHECK";
                                                                                        objJobCardDetails2.PARTREPLACED = "";
                                                                                        objJobCardDetails2.PARTREQ = "";
                                                                                        objJobCardDetails2.PARTREQID = 0;
                                                                                        objJobCardDetails2.PROBLEM = "OK FOR CHECK";
                                                                                        objJobCardDetails2.PROBLEM1 = "";
                                                                                        objJobCardDetails2.PROBLEM2 = "";
                                                                                        objJobCardDetails2.PROBLEM3 = "";
                                                                                        objJobCardDetails2.RESULT = 25;
                                                                                        objJobCardDetails2.STAGEID = 20;
                                                                                        objJobCardDetails2.STARTDT = DateTime.Now;
                                                                                        objJobCardDetails2.ENDDT = DateTime.Now;
                                                                                        objJobCardDetails2.JOBID = NEWJOBID;

                                                                                        string URLJCDetails2 = Convert.ToString(dtInsertJCDetails.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertJCDetails.Rows[0]["TOKEN"]);
                                                                                        var clientJCDetails2 = new RestClient(URLJCDetails2);
                                                                                        clientJCDetails2.Timeout = -1;
                                                                                        var requestJCDetails2 = new RestRequest(Method.POST);
                                                                                        requestJCDetails2.AddHeader("" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYVALUE"]) + "");
                                                                                        requestJCDetails2.AddHeader("Content-Type", "application/json");
                                                                                        var jsonInputJCDetails2 = JsonConvert.SerializeObject(objJobCardDetails2);
                                                                                        requestJCDetails2.AddParameter("application/json", jsonInputJCDetails2, ParameterType.RequestBody);
                                                                                        IRestResponse responseES2 = clientJCDetails2.Execute(requestJCDetails2);

                                                                                        #endregion

                                                                                        #region 64 PDI Entry...

                                                                                        JobCardDetails objJobCardDetails3 = new JobCardDetails();
                                                                                        objJobCardDetails3.ASCPARTCODE = "";
                                                                                        objJobCardDetails3.CMPID = objMainClass.intCmpId;
                                                                                        objJobCardDetails3.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                                        objJobCardDetails3.CREATEDATE = Convert.ToDateTime(DateTime.Now).ToString();
                                                                                        objJobCardDetails3.JCNO = NEWJCNO;
                                                                                        objJobCardDetails3.JOBDONE = "NA";
                                                                                        objJobCardDetails3.JOBDONEBY = Convert.ToInt32(Session["USERID"]);
                                                                                        objJobCardDetails3.JOBPROBID = 146;
                                                                                        objJobCardDetails3.JOBPROBID1 = 0;
                                                                                        objJobCardDetails3.JOBPROBID2 = 0;
                                                                                        objJobCardDetails3.JOBPROBID3 = 0;
                                                                                        objJobCardDetails3.NEWIMEINO = "";
                                                                                        objJobCardDetails3.NEXTSTAGEREQ = 59;
                                                                                        //if (hfSalesFrom.Value == "AMAZON")
                                                                                        //{
                                                                                        //    objJobCardDetails3.NOTE = "AMAZON";
                                                                                        //}
                                                                                        //else
                                                                                        //{
                                                                                        objJobCardDetails3.NOTE = "OK FOR CHECK";
                                                                                        //}

                                                                                        objJobCardDetails3.PARTREPLACED = "";
                                                                                        objJobCardDetails3.PARTREQ = "";
                                                                                        objJobCardDetails3.PARTREQID = 0;
                                                                                        objJobCardDetails3.PROBLEM = "OK FOR CHECK";
                                                                                        objJobCardDetails3.PROBLEM1 = "";
                                                                                        objJobCardDetails3.PROBLEM2 = "";
                                                                                        objJobCardDetails3.PROBLEM3 = "";
                                                                                        objJobCardDetails3.RESULT = 25;
                                                                                        objJobCardDetails3.STAGEID = 64;
                                                                                        objJobCardDetails3.STARTDT = DateTime.Now;
                                                                                        objJobCardDetails3.ENDDT = DateTime.Now;
                                                                                        objJobCardDetails3.JOBID = NEWJOBID;

                                                                                        string URLJCDetails3 = Convert.ToString(dtInsertJCDetails.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertJCDetails.Rows[0]["TOKEN"]);
                                                                                        var clientJCDetails3 = new RestClient(URLJCDetails3);
                                                                                        clientJCDetails3.Timeout = -1;
                                                                                        var requestJCDetails3 = new RestRequest(Method.POST);
                                                                                        requestJCDetails3.AddHeader("" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYVALUE"]) + "");
                                                                                        requestJCDetails3.AddHeader("Content-Type", "application/json");
                                                                                        var jsonInputJCDetails3 = JsonConvert.SerializeObject(objJobCardDetails3);
                                                                                        requestJCDetails3.AddParameter("application/json", jsonInputJCDetails3, ParameterType.RequestBody);
                                                                                        IRestResponse responseES3 = clientJCDetails3.Execute(requestJCDetails3);

                                                                                        #endregion

                                                                                        #region 59 Packed Entry...

                                                                                        JobCardDetails objJobCardDetails4 = new JobCardDetails();
                                                                                        objJobCardDetails4.ASCPARTCODE = "";
                                                                                        objJobCardDetails4.CMPID = objMainClass.intCmpId;
                                                                                        objJobCardDetails4.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                                        objJobCardDetails4.CREATEDATE = Convert.ToDateTime(DateTime.Now).ToString();
                                                                                        objJobCardDetails4.JCNO = NEWJCNO;
                                                                                        objJobCardDetails4.JOBDONE = "NA";
                                                                                        objJobCardDetails4.JOBDONEBY = Convert.ToInt32(Session["USERID"]);
                                                                                        objJobCardDetails4.JOBPROBID = 146;
                                                                                        objJobCardDetails4.JOBPROBID1 = 0;
                                                                                        objJobCardDetails4.JOBPROBID2 = 0;
                                                                                        objJobCardDetails4.JOBPROBID3 = 0;
                                                                                        objJobCardDetails4.NEWIMEINO = "";
                                                                                        objJobCardDetails4.NEXTSTAGEREQ = 59;
                                                                                        objJobCardDetails4.NOTE = "OK FOR CHECK";
                                                                                        objJobCardDetails4.PARTREPLACED = "";
                                                                                        objJobCardDetails4.PARTREQ = "";
                                                                                        objJobCardDetails4.PARTREQID = 0;
                                                                                        objJobCardDetails4.PROBLEM = "OK FOR CHECK";
                                                                                        objJobCardDetails4.PROBLEM1 = "";
                                                                                        objJobCardDetails4.PROBLEM2 = "";
                                                                                        objJobCardDetails4.PROBLEM3 = "";
                                                                                        objJobCardDetails4.RESULT = 25;
                                                                                        objJobCardDetails4.STAGEID = 59;
                                                                                        objJobCardDetails4.STARTDT = DateTime.Now;
                                                                                        objJobCardDetails4.ENDDT = DateTime.Now;
                                                                                        objJobCardDetails4.JOBID = NEWJOBID;

                                                                                        string URLJCDetails4 = Convert.ToString(dtInsertJCDetails.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertJCDetails.Rows[0]["TOKEN"]);
                                                                                        var clientJCDetails4 = new RestClient(URLJCDetails4);
                                                                                        clientJCDetails4.Timeout = -1;
                                                                                        var requestJCDetails4 = new RestRequest(Method.POST);
                                                                                        requestJCDetails4.AddHeader("" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertJCDetails.Rows[0]["KEYVALUE"]) + "");
                                                                                        requestJCDetails4.AddHeader("Content-Type", "application/json");
                                                                                        var jsonInputJCDetails4 = JsonConvert.SerializeObject(objJobCardDetails4);
                                                                                        requestJCDetails4.AddParameter("application/json", jsonInputJCDetails4, ParameterType.RequestBody);
                                                                                        IRestResponse responseES4 = clientJCDetails4.Execute(requestJCDetails4);

                                                                                        #endregion


                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Job Card Details API Not Found. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Job Card Not Created. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                                                                                }

                                                                            }
                                                                            else
                                                                            {
                                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Job Card Not Created. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                                                                            }
                                                                        }

                                                                        if (JOBSTAGEID == 19)
                                                                        {
                                                                            DataTable dtInsertES = new DataTable();
                                                                            dtInsertES = objMainClass.GetWAData("CREATEESTIMATE", 1, "GETWADATA");

                                                                            if (dtInsertES.Rows.Count > 0)
                                                                            {

                                                                                #region Estimate Master...
                                                                                EstimateMaster objEstimateMaster = new EstimateMaster();
                                                                                objEstimateMaster.ASCCHG = 550;
                                                                                objEstimateMaster.CMPID = objMainClass.intCmpId;
                                                                                objEstimateMaster.COSTAMT_PART = 0;
                                                                                objEstimateMaster.CREATEBY = Convert.ToInt32(Session["USERID"]);
                                                                                objEstimateMaster.CREATEDATE = Convert.ToDateTime(DateTime.Now).ToString();
                                                                                objEstimateMaster.DISCAMT = 0;
                                                                                objEstimateMaster.ESTIAMT_PART = 0;
                                                                                objEstimateMaster.ESTIAMT_SERV = 550;
                                                                                objEstimateMaster.ESTIDT = DateTime.Now;
                                                                                objEstimateMaster.ESTINO = "";
                                                                                objEstimateMaster.ETD = DateTime.Now;
                                                                                objEstimateMaster.HSNEW = "N";
                                                                                objEstimateMaster.ISRETURN = "N";
                                                                                objEstimateMaster.JOBID = NEWJOBID;
                                                                                objEstimateMaster.JOBIDSRNO = 1;
                                                                                objEstimateMaster.LIQUIDDAMAGE = "N";
                                                                                objEstimateMaster.LOGICHG = 0;
                                                                                objEstimateMaster.NWREASON = 0;
                                                                                objEstimateMaster.PARTDESC = "NA";
                                                                                objEstimateMaster.PAYMODE = 8;
                                                                                objEstimateMaster.PURDT = DateTime.Now;
                                                                                objEstimateMaster.PURREF = "";
                                                                                objEstimateMaster.RBATTERYNO = "";
                                                                                objEstimateMaster.REMARK = "AUTO GENRATED AGAINST SO";
                                                                                objEstimateMaster.RIMEINO = lblIMEI1.Text;
                                                                                objEstimateMaster.RITEMID = 0;
                                                                                objEstimateMaster.RPRODMAKE = lblMake.Text;
                                                                                objEstimateMaster.RPRODMODEL = lblModel.Text;
                                                                                objEstimateMaster.SERVDESC = "NA";
                                                                                objEstimateMaster.STAGEID = (int)STAGE.EstimateCreate;
                                                                                objEstimateMaster.STATRES = "AUTO GENRATED AGAINST SO";
                                                                                objEstimateMaster.STATUS = (int)STATUS.Estimated;
                                                                                objEstimateMaster.TOTALLOSS = "N";
                                                                                objEstimateMaster.TRANCHG = 0;
                                                                                objEstimateMaster.TRANCHGPCT = 0;

                                                                                #endregion

                                                                                string URLES = Convert.ToString(dtInsertES.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertES.Rows[0]["TOKEN"]);
                                                                                var clientES = new RestClient(URLES);
                                                                                clientES.Timeout = -1;
                                                                                var requestES = new RestRequest(Method.POST);
                                                                                requestES.AddHeader("" + Convert.ToString(dtInsertES.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertES.Rows[0]["KEYVALUE"]) + "");
                                                                                requestES.AddHeader("Content-Type", "application/json");
                                                                                var jsonInputES = JsonConvert.SerializeObject(objEstimateMaster);
                                                                                requestES.AddParameter("application/json", jsonInputES, ParameterType.RequestBody);
                                                                                IRestResponse responseES = clientES.Execute(requestES);

                                                                                EstimateResponse objEstimateResponse = new EstimateResponse();
                                                                                string jsonconnEstimate = responseES.Content;
                                                                                objEstimateResponse = JsonConvert.DeserializeObject<EstimateResponse>(jsonconnEstimate);

                                                                                NEWESTINO = objEstimateResponse.ESTINO;
                                                                                hfEstiNo.Value = NEWESTINO;
                                                                            }
                                                                            else
                                                                            {
                                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Estimate Not Created. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                                                                            }

                                                                        }

                                                                        if (JOBSTAGEID == 39)
                                                                        {
                                                                            if (NEWESTINO != "" && NEWESTINO != null && NEWESTINO != string.Empty)
                                                                            {
                                                                                DataTable dtInsertESAPRV = new DataTable();
                                                                                dtInsertESAPRV = objMainClass.GetWAData("APPROVEESTIMATE", 1, "GETWADATA");

                                                                                if (dtInsertESAPRV.Rows.Count > 0)
                                                                                {

                                                                                    EstimateApproval objEstimateApproval = new EstimateApproval();
                                                                                    objEstimateApproval.APRVBY1 = 124;
                                                                                    objEstimateApproval.APRVDT1 = DateTime.Now;
                                                                                    objEstimateApproval.APRVFLAG = (int)APRVTYPE.APPROVED;
                                                                                    objEstimateApproval.APRVNO1 = "APNP";
                                                                                    objEstimateApproval.APRVNOTE = "";
                                                                                    objEstimateApproval.CMPID = objMainClass.intCmpId;
                                                                                    objEstimateApproval.CUSTAPRVBY = "";
                                                                                    objEstimateApproval.ESTINO = NEWESTINO;
                                                                                    objEstimateApproval.PAYMODE = 8;
                                                                                    objEstimateApproval.REJRES = 0;
                                                                                    objEstimateApproval.STAGEID = (int)STAGE.EstimatApproved;
                                                                                    objEstimateApproval.STATUS = (int)STATUS.Approved;
                                                                                    objEstimateApproval.UPDATEBY = Convert.ToInt32(Session["USERID"]);
                                                                                    objEstimateApproval.UPDATEDATE = Convert.ToDateTime(DateTime.Now).ToString();


                                                                                    string URLESAPRV = Convert.ToString(dtInsertESAPRV.Rows[0]["OTHER"]) + "" + Convert.ToString(dtInsertESAPRV.Rows[0]["TOKEN"]);
                                                                                    var clientESAPRV = new RestClient(URLESAPRV);
                                                                                    clientESAPRV.Timeout = -1;
                                                                                    var requestESAPRV = new RestRequest(Method.POST);
                                                                                    requestESAPRV.AddHeader("" + Convert.ToString(dtInsertESAPRV.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtInsertESAPRV.Rows[0]["KEYVALUE"]) + "");
                                                                                    requestESAPRV.AddHeader("Content-Type", "application/json");
                                                                                    var jsonInputESAPRV = JsonConvert.SerializeObject(objEstimateApproval);
                                                                                    requestESAPRV.AddParameter("application/json", jsonInputESAPRV, ParameterType.RequestBody);
                                                                                    IRestResponse responseESAPRV = clientESAPRV.Execute(requestESAPRV);

                                                                                    EstimateApprovalResponse objEstimateApprovalResponse = new EstimateApprovalResponse();
                                                                                    string jsonconnEstimateAPRV = responseESAPRV.Content;
                                                                                    objEstimateApprovalResponse = JsonConvert.DeserializeObject<EstimateApprovalResponse>(jsonconnEstimateAPRV);

                                                                                }
                                                                                else
                                                                                {
                                                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Estimate Not Approved. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Estimate Not Approved. Estimate Number not found. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                                                                            }
                                                                        }

                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Job Status Update API not Found. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Stages Insert API not Found. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                                                        }


                                                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. Job ID : " + NEWJOBID + "\");", true);
                                                    }
                                                    else
                                                    {
                                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Stages Not Updated. Job ID : " + NEWJOBID + "\");", true);
                                                    }



                                                }
                                                else
                                                {
                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Reverse Waybill Update API not Found. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                                                }


                                            }
                                            else
                                            {
                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"New Job ID created. But Reverse Waybill Update API not Found. Please Contact API Provider. Job ID : " + NEWJOBID + "\");", true);
                                            }

                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Job Sheet creation API not found in Database. Please Contact API Provider.');", true);
                                        }
                                        #endregion

                                    }
                                }
                                else
                                {

                                    dtJobId.Rows.Add("'" + lblIMEI1.Text, "", lblModel.Text + " - Model not added.! ", "");
                                    ierror++;

                                    //if (txtItemCode.Text.Contains("MDUD"))
                                    //{
                                    //    lblProductID = "1";
                                    //    lblProductName = "LAMD000001 - MOBILE DEVICE";
                                    //}
                                    //else if (txtItemCode.Text.Contains("LTLT"))
                                    //{
                                    //    lblProductID = "78754";
                                    //    lblProductName = "LALT000001 - LAPTOP";
                                    //}
                                    //else
                                    //{
                                    //    lblProductID = "1";
                                    //    lblProductName = "LAMD000001 - MOBILE DEVICE";
                                    //}
                                }



                            }

                            //if (dtJobId.Rows.Count > 0)
                            //{
                            //    Session["JOBIDDATA"] = dtJobId;
                            //    string message = dtJobId.Rows.Count + " new job id created.";

                            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Sucess : " + message + "\");$('.close').click(function(){window.location.href ='frmBulkJobSheet.aspx' });", true);
                            //    DownloadErrorFile();

                            //}
                            //else
                            //{
                            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Job id not created. Please try again.!');", true);
                            //}
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Job Sheet creation API not found in Database. Please Contact API Provider.');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Data not added in jobsheet list.');", true);
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
            finally
            {
                if (dtJobId.Rows.Count > 0)
                {
                    Session["JOBSHEETDATA"] = dtJobId;
                    string message = "Total data uploaded - " + gvData.Rows.Count + ". New job id created -" + isucess + ". Error in uploaded data - " + ierror + ". Please check downloaded file for more details.";

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Sucess : " + message + "\");$('.close').click(function(){window.location.href ='frmBulkJobSheet.aspx' });", true);
                    //DownloadErrorFile();
                    string path = "frmJobSheetDataDownload.aspx";
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Job id not created. Please try again.!');", true);
                }
            }
        }

        public void DownloadErrorFile()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)Session["JOBIDDATA"];
                string attachment = "attachment; filename=JobSheetCreated on " + DateTime.Now + ".xls";
                Response.Clear();
                Response.ContentType = "application/vdn.ms-excel";
                Response.AddHeader("Content-Disposition", attachment);
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GridView grvDL = new GridView();
                grvDL.DataSource = dt;
                grvDL.DataBind();
                grvDL.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.Flush();
                Response.SuppressContent = true; // avoid the "Thread was being aborted" exception
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    OleDbConnection MyConnection;
                    string extension = Path.GetExtension(fuImage.FileName);
                    string folderpath = "~/excel/";
                    string filePath = Path.Combine(Server.MapPath(folderpath), Guid.NewGuid().ToString("N") + extension);
                    fuImage.SaveAs(filePath);
                    DataTable dt = new DataTable();
                    if (extension == ".csv")
                    {
                        var items = (from line in System.IO.File.ReadAllLines(filePath)
                                     select Array.ConvertAll(line.Split(','), v => v.ToString().TrimStart("\" ".ToCharArray()).TrimEnd("\" ".ToCharArray()))).ToArray();
                        string[] strarr1 = items[0];
                        for (int x = 0; x <= items[0].Count() - 1; x++)
                            dt.Columns.Add(strarr1[x]);
                        foreach (var a in items)
                        {
                            DataRow dr = dt.NewRow();
                            dr.ItemArray = a;
                            dt.Rows.Add(dr);
                        }
                        if (dt.Rows.Count > 0)
                        {
                            dt.Rows.RemoveAt(0);
                        }
                    }
                    else
                    {
                        MyConnection = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 12.0;");
                        //MyConnection = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + flUpload.FileName + ";Extended Properties=Excel 8.0;");
                        MyConnection.Open();
                        DataTable dtExcelSchema = MyConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null/* TODO Change to default(_) if this is not a reference type */);
                        string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                        using (OleDbCommand cmd = new OleDbCommand())
                        {
                            using (OleDbDataAdapter oda = new OleDbDataAdapter())
                            {
                                cmd.CommandText = (Convert.ToString("SELECT * From [Sheet1$]"));
                                cmd.Connection = MyConnection;
                                oda.SelectCommand = cmd;
                                oda.Fill(dt);
                                MyConnection.Close();
                            }
                        }
                    }
                    File.Delete(filePath);

                    gvData.DataSource = string.Empty;
                    gvData.DataBind();



                    int error = 0;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string price = Convert.ToString(dt.Rows[i]["Price"]);

                        if (price != "" && price != string.Empty && price != null)
                        {
                            if (price == "0")
                            {
                                error++;
                            }
                        }
                        else
                        {
                            error++;
                        }

                    }

                    if (error > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please enter price for all material.!');", true);
                    }
                    else
                    {
                        gvData.DataSource = dt;
                        gvData.DataBind();

                        ViewState["JobSheetData"] = dt;

                        if (dt.Rows.Count > 0)
                        {
                            lblRecoretxt.Visible = true;
                            lblRecord.Visible = true;

                            lblRecord.Text = dt.Rows.Count.ToString();
                            lnkSave.Visible = true;
                        }
                        else
                        {
                            lblRecoretxt.Visible = false;
                            lblRecord.Visible = false;

                            lblRecord.Text = "0";
                            lnkSave.Visible = false;
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
                    //string filePath = "~/CRM/JobSheet Creation Demo Excel.xlsx";
                    string filePath = Server.MapPath("~/CRM/JobSheet Creation Demo Excel.xlsx");
                    FileInfo file = new FileInfo(filePath);
                    if (file.Exists)
                    {
                        Response.Clear();
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                        Response.AddHeader("Content-Length", file.Length.ToString());
                        Response.ContentType = "text/plain";
                        Response.Flush();
                        Response.TransmitFile(file.FullName);
                        Response.End();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('File not found for Download.!');", true);
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