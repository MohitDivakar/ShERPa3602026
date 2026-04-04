using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.PP
{
    public partial class ViewPPPartRequest : System.Web.UI.Page
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
                            //Session.Abandon();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();

                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), "ObjAllUsers", "OBJECT");
                        if (FormRights.bView == false)
                        {
                            chkAllUser.Checked = false;
                            chkAllUser.Enabled = false;
                        }
                        else
                        {
                            chkAllUser.Enabled = true;
                            chkAllUser.Checked = true;
                        }
                        chkPendingOnly.Checked = true;
                        objBindDDL.FillSegment(ddlSegment);
                        //SP_SELECT_PARTREQ_LIST
                        DataTable dt = objMainClass.GetPartReqData(1, ddlSegment.SelectedIndex > 0 ? ddlSegment.SelectedValue : "", txtRequestNo.Text, txtJobId.Text, txtFromDate.Text, txtToDate.Text, chkPendingOnly.Checked == true ? 1 : 0, chkAllUser.Checked == true ? "" : Convert.ToString(Session["USERID"]), "");
                        gvList.DataSource = dt;
                        gvList.DataBind();


                        objBindDDL.FillUOM(ddlUOM);
                        objBindDDL.FillPlant(ddlPLant);
                        ddlPLant.SelectedIndex = 1;
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

        protected void lnkSearhPR_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objMainClass.GetPartReqData(1, ddlSegment.SelectedIndex > 0 ? ddlSegment.SelectedValue : "", txtRequestNo.Text, txtJobId.Text, txtFromDate.Text, txtToDate.Text, chkPendingOnly.Checked == true ? 1 : 0, chkAllUser.Checked == true ? "" : Convert.ToString(Session["USERID"]), "");
                if (dt.Rows.Count > 0)
                {
                    gvList.DataSource = dt;
                    gvList.DataBind();
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
            string attachment = "attachment; filename=rptPartRequest.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vdn.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gvList.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "eDelete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                try
                {
                    if (Session["USERID"] != null)
                    {
                        bool iResult = objMainClass.UpdatePartRequest(Convert.ToInt32(index), "", "", "", "", "", "", "", "74", Convert.ToString(Session["USERID"]), "", 2);
                    }

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
                }


            }
        }

        protected void txtJobAddId_TextChanged(object sender, EventArgs e)
        {
            if (txtJobAddId.Text.Length >= 6)
            {
                //SELECT_JOBDETAILNEW
                DataTable dt = new DataTable();
                dt = objMainClass.SelectJobDetails(txtJobAddId.Text);
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
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Job id is closed.');$('#txtJobId').focus();", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warningPR').modal();$('#lblAlertMsgPR').text('Invalid Item Code.');", true);
                        lblAlertMsgPR.Text = "Job id is closed.";
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
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have segment rights.');$('#txtJobId').focus();", true);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warningPR').modal();$('#lblAlertMsgPR').text('You do not have segment rights.');", true);
                            lblAlertMsgPR.Text = "You do not have segment rights.";
                            txtJobId.Focus();
                            txtJobId.Text = string.Empty;
                        }
                    }
                    else
                    {
                        txtJobId.Focus();
                        txtJobId.Text = string.Empty;
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have segment rights.');$('#txtJobId').focus();", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warningPR').modal();$('#lblAlertMsgPR').text('You do not have segment rights.');", true);
                        lblAlertMsgPR.Text = "You do not have segment rights.";
                        txtJobId.Focus();
                        txtJobId.Text = string.Empty;
                    }
                }
                else
                {
                    txtJobId.Focus();
                    txtJobId.Text = string.Empty;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid Job Id.');$('#txtJobId').focus();", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warningPR').modal();$('#lblAlertMsgPR').text('Invalid Job Id.');", true);
                    lblAlertMsgPR.Text = "Invalid Job Id.";
                    txtJobId.Focus();
                    txtJobId.Text = string.Empty;
                }
            }
            else
            {
                txtJobId.Focus();
                txtJobId.Text = string.Empty;
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid Job Id.');$('#txtJobId').focus();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warningPR').modal();$('#lblAlertMsgPR').text('Invalid Job Id.');", true);
                lblAlertMsgPR.Text = "Invalid Job Id.";
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
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warningPR').modal();$('#lblAlertMsgPR').text('Invalid Item Code.');", true);
                    lblAlertMsgPR.Text = "Invalid Item Code.";
                    txtItemCode.Focus();
                    txtItemCode.Text = string.Empty;
                }
            }
            else
            {
                txtItemCode.Focus();
                txtItemCode.Text = string.Empty;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warningPR').modal();$('#lblAlertMsgPR').text('Invalid Item Code.');", true);
                lblAlertMsgPR.Text = "Invalid Item Code.";
                txtItemCode.Focus();
                txtItemCode.Text = string.Empty;
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
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"UOM is not matched with Base Unit of Item!\");", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warningPR').modal();$('#lblAlertMsgPR').text('UOM is not matched with Base Unit of Item!');", true);
                        lblAlertMsgPR.Text = "UOM is not matched with Base Unit of Item!";

                    }
                }
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warningPR').modal();$('#lblAlertMsgPR').text(\"" + ex.Message + "\");", true);
                lblAlertMsgPR.Text = ex.Message;
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
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have plant rights.');$('#txtJobId').focus();", true);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warningPR').modal();$('#lblAlertMsgPR').text('You do not have plant rights.');", true);
                            lblAlertMsgPR.Text = "You do not have plant rights.";
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
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have plant rights.');$('#txtJobId').focus();", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warningPR').modal();$('#lblAlertMsgPR').text('You do not have plant rights.');", true);
                        lblAlertMsgPR.Text = "You do not have plant rights.";
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
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warningPR').modal();$('#lblAlertMsgPR').text(\"" + ex.Message + "\");", true);
                lblAlertMsgPR.Text = ex.Message;
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;


            string lblJobID = grdrow.Cells[3].Text;
            string lblGvSegment = grdrow.Cells[2].Text;
            string lblGvSegmentDesc = grdrow.Cells[21].Text;//
            string lblGvJobStatus = grdrow.Cells[19].Text;
            string lblGvItemCode = grdrow.Cells[4].Text;
            string lblGvItemDesc = grdrow.Cells[5].Text;
            string lblGvItemId = grdrow.Cells[18].Text;
            string lblGvSku = grdrow.Cells[7].Text;
            string lblQTY = grdrow.Cells[6].Text;
            string lblUOM = grdrow.Cells[22].Text;
            string lblPlantcd = grdrow.Cells[8].Text;
            string lblLoccd = grdrow.Cells[9].Text;
            string lblReqBy = grdrow.Cells[23].Text;
            string ReqNo = grdrow.Cells[0].Text;


            objBindDDL.FillUOM(ddlUOM);
            objBindDDL.FillPlant(ddlPLant);
            objBindDDL.FillLocationByPlantCd(ddlLocation, lblPlantcd);
            objBindDDL.Fill_EMP_Enginner(ddlReqBy, 22, 1);

            lblReqNo.InnerText = ReqNo;
            txtJobAddId.Text = lblJobID;
            lblSegment.InnerText = lblGvSegment;
            lblSegmentDesc.InnerText = lblGvSegmentDesc;
            lblJobStatus.InnerText = lblGvJobStatus;
            txtItemCode.Text = lblGvItemCode;
            lblItemDesc.InnerText = lblGvItemDesc;
            lblItemId.InnerText = lblGvItemId;
            lblSKU.InnerText = lblGvSku;
            txtQuantity.Text = lblQTY;
            ddlUOM.SelectedValue = lblUOM;
            ddlPLant.SelectedValue = lblPlantcd;
            ddlLocation.SelectedValue = lblLoccd;
            ddlReqBy.SelectedValue = lblReqBy;



            lblReqNo.Disabled = true;
            txtJobAddId.ReadOnly = true;
            lblSegment.Disabled = true;
            lblSegmentDesc.Disabled = true;
            lblJobStatus.Disabled = true;
            txtItemCode.ReadOnly = true;
            lblItemDesc.Disabled = true;
            lblItemId.Disabled = true;
            lblSKU.Disabled = true;
            txtQuantity.ReadOnly = true;
            ddlUOM.Enabled = false;
            ddlPLant.Enabled = false;
            ddlLocation.Enabled = false;
            ddlReqBy.Enabled = false;


            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);

        }

        protected void lnnkClosePopup_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warningPR').close();", true);
        }
    }
}