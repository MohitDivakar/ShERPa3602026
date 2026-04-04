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
    public partial class CreateMR : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        DataTable dtItem = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMRSRNO.Text = string.Empty;
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

                        if (Request.QueryString.Count > 0)
                        {
                            if (Convert.ToString(Request.QueryString["MRNO"]) != null && Convert.ToString(Request.QueryString["MRNO"]) != string.Empty && Convert.ToString(Request.QueryString["MRNO"]) != "")
                            {
                                Session["EditMRNo"] = Convert.ToString(Request.QueryString["MRNO"]);

                            }
                            else if (Convert.ToString(Request.QueryString["FormName"]) == "FromReq")
                            {
                                Session["ReqNo"] = Convert.ToString(Request.QueryString["ReqNo"]);
                            }
                            Response.Redirect(Request.Url.AbsolutePath, false);
                        }
                        else
                        {
                            objBindDDL.FillDocType(ddlDoctype, "MMR");
                            ddlDoctype.SelectedIndex = 1;
                            ddlDoctype.Enabled = false;
                            objBindDDL.FillItemGrp(ddlItemGroup);
                            ddlItemGroup.SelectedIndex = 1;
                            objBindDDL.FillDetparment(ddlDepartment);
                            ddlDepartment.SelectedValue = "5";

                            objBindDDL.FillPlant(ddlPLant);
                            string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                            ddlPLant.SelectedValue = plantArray[0];
                            objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);
                            ddlLocation.SelectedIndex = 1;
                            objBindDDL.FillUOM(ddlUOM);
                            //ddlUOM.SelectedIndex = 1;
                            txtMRNO.Text = objMainClass.MAXPRNO(ddlDoctype.SelectedValue, "MMR");
                            //txtPRDATE.Text = objMainClass.setDateFormat(Convert.ToDateTime(DateTime.Now).ToShortDateString(), true);
                            txtMRDATE.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                            DataTable dt = objMainClass.GetPRFCost(ddlPLant.SelectedValue, Convert.ToString(Session["USERID"]));
                            //txtCostCenter.Text = Convert.ToString(dt.Rows[0]["CSTCENTCD"]);

                            objBindDDL.FillCostCenter(ddlCostCenter, ddlPLant.SelectedValue, ddlLocation.SelectedValue);
                            ddlCostCenter.Items.Remove(ddlCostCenter.Items.FindByValue("1000"));
                            Session["savedet"] = "Save Item";
                            Session["saveall"] = "Save All";
                            SetUpGrid();

                            if (Session["EditMRNo"] != null && Convert.ToString(Session["EditMRNo"]) != "" && Convert.ToString(Session["EditMRNo"]) != string.Empty)
                            {
                                DataTable EditDt = new DataTable();
                                DataTable dtMR = new DataTable();
                                dtMR = objMainClass.SelectMRMST(Convert.ToString(Session["EditMRNo"]), 1);
                                EditDt = objMainClass.SelectMRDetail(Convert.ToString(Session["EditMRNo"]), 1, 0, string.Empty);
                                if (EditDt.Rows.Count > 0)
                                {
                                    ddlDoctype.SelectedValue = Convert.ToString(dtMR.Rows[0]["MRTYPE"]);
                                    txtMRNO.Text = Convert.ToString(dtMR.Rows[0]["MRNO"]);
                                    txtMRDATE.Text = Convert.ToDateTime(dtMR.Rows[0]["MRDT"]).ToShortDateString();
                                    txtREMARKS.Text = Convert.ToString(dtMR.Rows[0]["REMARK"]);
                                    ddlDepartment.SelectedValue = Convert.ToString(dtMR.Rows[0]["DEPTID"]);

                                    txtMRNO.ReadOnly = true;
                                    txtMRDATE.ReadOnly = true;

                                    grvListItem.DataSource = EditDt;
                                    grvListItem.DataBind();
                                    ViewState["ItemData"] = EditDt;

                                    Session["saveall"] = "Update All";
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Record not found. '" + Convert.ToString(Session["EditMRNo"]) + "'.');$('.close').click(function(){window.location.href ='ViewMR.aspx' });", true);
                                }
                            }
                            if (Session["ReqNo"] != null && Convert.ToString(Session["ReqNo"]) != "" && Convert.ToString(Session["ReqNo"]) != string.Empty)
                            {
                                DataTable dtMR = new DataTable();
                                dtMR = objMainClass.GetPartReqData(1, "", Convert.ToString(Session["ReqNo"]), "", "", "", 1, "", "");
                                txtItemSpec.Text = Convert.ToString(dtMR.Rows[0]["ITEMCODE"]); //ITEMCODE
                                txtItemRate.Text = "1";
                                txtItemQty.Text = Convert.ToString(Convert.ToInt32(dtMR.Rows[0]["QTY"])); //QTY
                                txtItemQty_TextChanged(1, e);
                                ddlUOM.SelectedValue = Convert.ToString(dtMR.Rows[0]["UOM"]); //UOM
                                txtTrackNo.Text = Convert.ToString(dtMR.Rows[0]["JobId"]); //JobId
                                ddlPLant.SelectedValue = Convert.ToString(dtMR.Rows[0]["PLANTCD"]);
                                ddlLocation.SelectedValue = Convert.ToString(dtMR.Rows[0]["LOCCD"]);
                                txtRequisitioner.Text = Convert.ToString(dtMR.Rows[0]["REQBYNAME"]);
                            }
                        }
                    }
                    else
                    {
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');setTimeout(function(){  $('#modal-warning').modal('hide')}, 2000); setTimeout(function(){  window.location.href ='../Login.aspx'}, 2000);", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                    }
                }
                catch (Exception ex)
                {
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");setTimeout(function(){  $('#modal-warning').modal('hide')}, 2000);", true);
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
                dtColumn.ColumnName = "ITEMSPEC";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMDESC";
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
                dtColumn.ColumnName = "COSTCENTER";
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
                dtColumn.ColumnName = "REQUISITIONER";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "TRACKNO";
                dtItem.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.ColumnName = "ITEMTEXT";
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

        protected void imgSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (Convert.ToString(Session["saveall"]) == "Save All")
                    {
                        if (grvListItem.Rows.Count > 0)
                        {

                            string PLantRights = string.Empty;
                            string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                            for (int i = 0; i < plantArray.Count(); i++)
                            {
                                if (plantArray[i].Trim() == ((Label)grvListItem.Rows[0].FindControl("lblPlantID")).Text)
                                {
                                    PLantRights = ((Label)grvListItem.Rows[0].FindControl("lblPlantID")).Text;
                                }
                            }

                            if (PLantRights.Length > 0)
                            {


                                byte[] bytes;
                                string extension = ".jpeg";

                                using (BinaryReader br = new BinaryReader(fuInvDoc.PostedFile.InputStream))
                                {
                                    bytes = br.ReadBytes(fuInvDoc.PostedFile.ContentLength);
                                    extension = System.IO.Path.GetExtension(fuInvDoc.FileName);
                                }

                                string PR = objMainClass.SaveMMR(ddlDoctype.SelectedValue, txtMRDATE.Text, txtREMARKS.Text, ddlDepartment.SelectedValue, grvListItem, Convert.ToString(Session["USERID"]), bytes, extension);
                                if (PR != "")
                                {

                                    String strCustContent = "";
                                    strCustContent = fileread();
                                    strCustContent = strCustContent.Replace("###Heading###", "New MR Created by User.");
                                    strCustContent = strCustContent.Replace("###CreateBy###", Convert.ToString(Session["USERNAME"]));
                                    strCustContent = strCustContent.Replace("###CreateDate###", txtMRDATE.Text);
                                    strCustContent = strCustContent.Replace("###PRNO###", PR);
                                    strCustContent = strCustContent.Replace("###Message###", "New MR created by user. Details are as per above.");
                                    strCustContent = strCustContent.Replace("###LINK###", "http://14.98.132.190:360/MM/AprvMR.aspx");
                                    strCustContent = strCustContent.Replace("###HREFLINK###", "http://14.98.132.190:360/MM/AprvMR.aspx");

                                    DataTable dt = new DataTable();
                                    dt = objMainClass.MailSenderReceiver("MR", 1, Convert.ToInt32(ddlDepartment.SelectedValue), ((Label)grvListItem.Rows[0].FindControl("lblPlantID")).Text);
                                    string Reciever = string.Empty;
                                    if (dt.Rows.Count > 0)
                                    {
                                        for (int i = 0; i < dt.Rows.Count; i++)
                                        {

                                            if (Reciever == string.Empty)
                                            {
                                                Reciever = Convert.ToString(dt.Rows[i]["EMAILID"]);
                                            }
                                            else
                                            {
                                                Reciever = Reciever + ";" + Convert.ToString(dt.Rows[i]["EMAILID"]);
                                            }
                                        }
                                        //objMainClass.SendMail(strCustContent, "New MR Created", dt);
                                        objMainClass.SaveMailNotification(objMainClass.EmailID, objMainClass.Password, Convert.ToString(dt.Rows[0]["EMAILID"]), Reciever, "New MR Created", strCustContent, objMainClass.PORT, PR, Convert.ToString(Session["USERID"]), "MMR");
                                    }

                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Record save sucessfully.  MMR No. : " + PR + "\");$('.close').click(function(){window.location.href ='ViewMR.aspx' });", true);
                                    Session["EditMRNo"] = null;
                                    Session["ReqNo"] = null;
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Record not inserted sucessfully!');", true);
                                }

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have plant rights.');$('#txtJobId').focus();", true);
                            }

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please Enter Item Details!'); setTimeout(function(){  $('#modal-warning').modal('hide')}, 2000);", true);
                        }
                    }
                    else if (Convert.ToString(Session["saveall"]) == "Update All")
                    {
                        if (grvListItem.Rows.Count > 0)
                        {

                            string PLantRights = string.Empty;
                            string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                            for (int i = 0; i < plantArray.Count(); i++)
                            {
                                if (plantArray[i].Trim() == ddlPLant.SelectedValue)
                                {
                                    PLantRights = ((Label)grvListItem.Rows[0].FindControl("lblPlantID")).Text;
                                }
                            }

                            if (PLantRights.Length > 0)
                            {

                                string PR = objMainClass.UpdateMMR(txtMRNO.Text, ddlDoctype.SelectedValue, txtMRDATE.Text, txtREMARKS.Text, ddlDepartment.SelectedValue, grvListItem, Convert.ToString(Session["USERID"]));
                                if (PR != "")
                                {
                                    String strCustContent = "";
                                    strCustContent = fileread();
                                    strCustContent = strCustContent.Replace("###Heading###", "MR Updated by User.");
                                    strCustContent = strCustContent.Replace("###CreateBy###", Convert.ToString(Session["USERNAME"]));
                                    strCustContent = strCustContent.Replace("###CreateDate###", txtMRDATE.Text);
                                    strCustContent = strCustContent.Replace("###PRNO###", PR);
                                    strCustContent = strCustContent.Replace("###Message###", "MR updated by user. Details are as per above.");
                                    strCustContent = strCustContent.Replace("###LINK###", "http://14.98.132.190:360/MM/AprvMR.aspx");
                                    strCustContent = strCustContent.Replace("###HREFLINK###", "http://14.98.132.190:360/MM/AprvMR.aspx");
                                    int cnt = grvListItem.Rows.Count;
                                    DataTable dt = new DataTable();
                                    dt = objMainClass.MailSenderReceiver("MR", 1, Convert.ToInt32(ddlDepartment.SelectedValue), ((Label)grvListItem.Rows[0].FindControl("lblPlantID")).Text);
                                    string Reciever = string.Empty;
                                    if (dt.Rows.Count > 0)
                                    {

                                        for (int i = 0; i < dt.Rows.Count; i++)
                                        {

                                            if (Reciever == string.Empty)
                                            {
                                                Reciever = Convert.ToString(dt.Rows[i]["EMAILID"]);
                                            }
                                            else
                                            {
                                                Reciever = Reciever + ";" + Convert.ToString(dt.Rows[i]["EMAILID"]);
                                            }
                                        }
                                        objMainClass.SaveMailNotification(objMainClass.EmailID, objMainClass.Password, Convert.ToString(dt.Rows[0]["EMAILID"]), Reciever, "MR Updated", strCustContent, objMainClass.PORT, PR, Convert.ToString(Session["USERID"]), "MMR");


                                        //objMainClass.SendMail(strCustContent, "MR Updated", dt);
                                    }


                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Record updated sucessfully.  MMR No. : " + PR + "\");$('.close').click(function(){window.location.href ='ViewMR.aspx' });", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Record not updated!');", true);
                                }

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have plant rights.');$('#txtJobId').focus();", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please Enter Item Details!');", true);
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

        protected void ddlPLant_SelectedIndexChanged(object sender, EventArgs e)
        {
            objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);

            try
            {
                if (Session["USERID"] != null)
                {
                    string PLantRights = string.Empty;
                    string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                    for (int i = 0; i < plantArray.Count(); i++)
                    {
                        if (plantArray[i].Trim() == ddlPLant.SelectedValue)
                        {
                            PLantRights = ddlPLant.SelectedValue;
                        }
                    }

                    if (PLantRights.Length > 0)
                    {
                        DataTable dt = objMainClass.GetPRFCost(ddlPLant.SelectedValue, Convert.ToString(Session["USERID"]));
                        if (dt.Rows.Count > 0)
                        {
                            //txtCostCenter.Text = Convert.ToString(dt.Rows[0]["CSTCENTCD"]);
                        }
                        else
                        {
                            //txtCostCenter.Text = "1000";
                        }
                    }
                    else
                    {
                        ddlPLant.SelectedValue = plantArray[0];
                        ddlPLant.Focus();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have plant rights.');setTimeout(function(){  $('#modal-warning').modal('hide')}, 2000);", true);
                        ddlPLant.SelectedValue = plantArray[0];
                        ddlPLant.Focus();
                        objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);
                        ddlLocation.SelectedIndex = 1;
                    }

                    objBindDDL.FillCostCenter(ddlCostCenter, ddlPLant.SelectedValue, ddlLocation.SelectedValue);

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }

        }


        private string validateData()
        {
            string j = "ERROR";
            string PLantRights = string.Empty;
            string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
            for (int i = 0; i < plantArray.Count(); i++)
            {
                if (plantArray[i].Trim() == ddlPLant.SelectedValue)
                {
                    PLantRights = ddlPLant.SelectedValue;
                }
            }
            if (PLantRights.Length > 0)
            {
                j = "OK";
            }
            else
            {
                j = "You do not have plant rights. ";
            }
            return j;
        }

        protected void btnSaveDet_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    string validation = validateData();
                    if (validation == "OK")
                    {





                        if (Convert.ToString(Session["savedet"]) == "Save Item")
                        {
                            DataTable dt = (DataTable)ViewState["ItemData"];
                            if (grvListItem.Rows.Count > 0)
                            {
                                DataRow lastRow = dt.Rows[dt.Rows.Count - 1];
                                int id = Convert.ToInt32(lastRow["ID"]) + 1;
                                dt.Rows.Add(id, txtItemSpec.Text, txtItemDesc.Text, ddlItemGroup.SelectedItem.Text, ddlItemGroup.SelectedValue, ddlUOM.SelectedItem.Text,
                                    ddlUOM.SelectedValue, txtItemQty.Text, txtItemRate.Text, txtAmount.Text, ddlCostCenter.SelectedValue,
                                    ddlPLant.SelectedItem.Text, ddlPLant.SelectedValue, ddlLocation.SelectedItem.Text, ddlLocation.SelectedValue,
                                    txtRequisitioner.Text, txtTrackNo.Text, txtItemRemarks.Text);

                                ViewState["ItemData"] = dt;

                            }
                            else
                            {
                                dt.Rows.Add("1", txtItemSpec.Text, txtItemDesc.Text, ddlItemGroup.SelectedItem.Text, ddlItemGroup.SelectedValue, ddlUOM.SelectedItem.Text,
                                    ddlUOM.SelectedValue, txtItemQty.Text, txtItemRate.Text, txtAmount.Text, ddlCostCenter.SelectedValue,
                                    ddlPLant.SelectedItem.Text, ddlPLant.SelectedValue, ddlLocation.SelectedItem.Text, ddlLocation.SelectedValue,
                                    txtRequisitioner.Text, txtTrackNo.Text, txtItemRemarks.Text);

                                ViewState["ItemData"] = dt;


                            }

                            grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                            grvListItem.DataBind();
                            EmptyString();





                        }
                        else if (Convert.ToString(Session["savedet"]) == "Update Item")
                        {
                            DataTable dt = (DataTable)ViewState["ItemData"];
                            DataTable ddt = (DataTable)ViewState["ItemData"];
                            DataRow dr = ddt.Select("ID = '" + lblMRSRNO.Text + "'")[0];
                            dr[1] = txtItemSpec.Text;
                            dr[2] = txtItemDesc.Text;
                            dr[3] = ddlItemGroup.SelectedItem.Text;
                            dr[4] = ddlItemGroup.SelectedValue;
                            dr[5] = ddlUOM.SelectedItem.Text;
                            dr[6] = ddlUOM.SelectedValue;
                            dr[7] = txtItemQty.Text;
                            dr[8] = txtItemRate.Text;
                            dr[9] = txtAmount.Text;
                            dr[10] = ddlCostCenter.SelectedValue; //txtCostCenter.Text;
                            dr[11] = ddlPLant.SelectedItem.Text;
                            dr[12] = ddlPLant.SelectedValue;
                            dr[13] = ddlLocation.SelectedItem.Text;
                            dr[14] = ddlLocation.SelectedValue;
                            dr[15] = txtRequisitioner.Text;
                            dr[16] = txtTrackNo.Text;
                            dr[17] = txtItemRemarks.Text;

                            grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                            grvListItem.DataBind();
                            Session["savedet"] = "Save Item";
                            EmptyString();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + validation + "\");", true);
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

        private void EmptyString()
        {
            txtItemSpec.Text = string.Empty;
            txtItemDesc.Text = string.Empty;
            txtItemQty.Text = string.Empty;
            txtItemRate.Text = string.Empty;
            txtAmount.Text = string.Empty;
            txtTrackNo.Text = string.Empty;
            txtRequisitioner.Text = string.Empty;
            txtItemRemarks.Text = string.Empty;
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
                lblMRSRNO.Text = Convert.ToString(index);
                GridViewRow gRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;



                Label lblID = (Label)gRow.FindControl("lblID");
                Label lblItemSpec = (Label)gRow.FindControl("lblItemSpec");
                Label lblItemDesc = (Label)gRow.FindControl("lblItemDesc");
                Label lblGroupId = (Label)gRow.FindControl("lblGroupId");
                Label lblUOM = (Label)gRow.FindControl("lblUOM");
                Label lblUOMID = (Label)gRow.FindControl("lblUOMID");
                Label lblQty = (Label)gRow.FindControl("lblQty");
                Label lblRate = (Label)gRow.FindControl("lblRate");
                Label lblAmount = (Label)gRow.FindControl("lblAmount");
                Label lblCostCenter = (Label)gRow.FindControl("lblCostCenter");
                Label lblPlantCD = (Label)gRow.FindControl("lblPlantCD");
                Label lblPlantID = (Label)gRow.FindControl("lblPlantID");
                Label lblLocationCD = (Label)gRow.FindControl("lblLocationCD");
                Label lblLocationCDID = (Label)gRow.FindControl("lblLocationCDID");
                Label lblRequisitioner = (Label)gRow.FindControl("lblRequisitioner");
                Label lblTrackNo = (Label)gRow.FindControl("lblTrackNo");
                Label lblItemText = (Label)gRow.FindControl("lblItemText");

                txtItemSpec.Text = lblItemSpec.Text;
                txtItemDesc.Text = lblItemDesc.Text;
                txtItemQty.Text = lblQty.Text;
                ddlUOM.SelectedValue = lblUOMID.Text;
                txtItemRate.Text = lblRate.Text;
                txtAmount.Text = lblAmount.Text;
                txtTrackNo.Text = lblTrackNo.Text;
                txtRequisitioner.Text = lblRequisitioner.Text;
                txtItemRemarks.Text = lblItemText.Text;
                ddlPLant.SelectedValue = lblPlantID.Text;
                objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPLant.SelectedValue);
                ddlLocation.SelectedValue = lblLocationCDID.Text;
                objBindDDL.FillCostCenter(ddlCostCenter, ddlPLant.SelectedValue, ddlLocation.SelectedValue);
                ddlCostCenter.SelectedValue = lblCostCenter.Text;
                ddlItemGroup.SelectedValue = lblGroupId.Text;
                Session["savedet"] = "Update Item";

                DataTable dt = (DataTable)ViewState["ItemData"];
                //dt.Rows[gRow.RowIndex].Delete();
                ViewState["ItemData"] = dt;
                grvListItem.DataSource = (DataTable)ViewState["ItemData"];
                grvListItem.DataBind();

            }
        }

        protected void txtItemQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtItemRate.Text == "" || txtItemQty.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Rate and Quantity should not be Empty.!');", true);
                }
                else
                {
                    if (Convert.ToDecimal(txtItemRate.Text) <= 0 || Convert.ToDecimal(txtItemQty.Text) <= 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Rate and Quantity should be greater than Zero.!');", true);
                    }
                    else
                    {
                        txtAmount.Text = Convert.ToString(Convert.ToDecimal(txtItemRate.Text == "" ? "0" : txtItemRate.Text) * Convert.ToDecimal(txtItemQty.Text == "" ? "0" : txtItemQty.Text));
                    }
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                objBindDDL.FillCostCenter(ddlCostCenter, ddlPLant.SelectedValue, ddlLocation.SelectedValue);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}