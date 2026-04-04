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
    public partial class ApprovedMR : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();


        protected void Page_Load(object sender, EventArgs e)
        {

            lblAPRV1.Visible = false;
            lblAPRV2.Visible = false;
            lblAPRV1.Text = string.Empty;
            lblAPRV2.Text = string.Empty;

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
                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        objBindDDL.FillPlant(ddlPlantCode);
                        string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                        ddlPlantCode.SelectedValue = plantArray[0];
                        DataTable dt = new DataTable();
                        dt = objMainClass.GetApprovedMR(txtMrno.Text, txtFromDate.Text, txtToDate.Text, "MR", (int)APRVTYPE.APPROVED, objMainClass.intCmpId, ddlPlantCode.SelectedIndex > 0 ? ddlPlantCode.SelectedValue : "");
                        if (dt.Rows.Count > 0)
                        {
                            gvList.DataSource = dt;
                            gvList.DataBind();
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
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
                }
            }
        }
        protected void lnkSearhMR_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objMainClass.GetApprovedMR(txtMrno.Text, txtFromDate.Text, txtToDate.Text, "MR", (int)APRVTYPE.APPROVED, objMainClass.intCmpId, ddlPlantCode.SelectedIndex > 0 ? ddlPlantCode.SelectedValue : Convert.ToString(Session["PLANTCD"]));
                if (dt.Rows.Count > 0)
                {
                    gvList.DataSource = dt;
                    gvList.DataBind();
                }
                else
                {
                    gvList.DataSource = string.Empty;
                    gvList.DataBind();
                }
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnDetails_Click(object sender, EventArgs e)
        {
            lblAPRV1.Visible = false;
            lblAPRV2.Visible = false;
            lblAPRV1.Text = string.Empty;
            lblAPRV2.Text = string.Empty;

            try
            {
                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                string mrno = grdrow.Cells[1].Text;

                DataTable dt = new DataTable();
                dt = objMainClass.GetMRDetails(mrno, 4);
                if (dt.Rows.Count > 0)
                {

                    lblDoctype.Text = Convert.ToString(dt.Rows[0]["MRTYPE"]);
                    lblMRDate.Text = Convert.ToDateTime(dt.Rows[0]["MRDT"]).ToShortDateString();
                    lblMRNo.Text = Convert.ToString(dt.Rows[0]["MRNO"]);
                    lblRemark.Text = Convert.ToString(dt.Rows[0]["REMARK"]);
                    hfMREXTENSION.Value = Convert.ToString(dt.Rows[0]["MREXTENSION"]);

                    gvDetail.DataSource = dt;
                    gvDetail.DataBind();

                    decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("ITEMQTY"));
                    gvDetail.FooterRow.Cells[5].Text = total.ToString("N3");

                    decimal ITEMAMOUNT = dt.AsEnumerable().Sum(row => row.Field<decimal>("ITEMAMOUNT"));
                    gvDetail.FooterRow.Cells[7].Text = ITEMAMOUNT.ToString("N2");

                    DataTable logDT = new DataTable();
                    logDT = objMainClass.SELECT_REQUISITION_LOG(lblMRNo.Text);
                    //if (logDT.Rows.Count == 1)
                    //{
                    //    lblAPRV1.Text = "Approval 1 :  " + Convert.ToString(logDT.Rows[0]["LISTDESC"]) + "  By  " + Convert.ToString(logDT.Rows[0]["USERNAME"]) + "  On  " + Convert.ToString(logDT.Rows[0]["APRVDATE"]);//APRVDATE
                    //    lblAPRV1.Visible = true;
                    //}
                    //else if (logDT.Rows.Count == 2)
                    //{
                    //    lblAPRV1.Text = "Approval 1 :  " + Convert.ToString(logDT.Rows[0]["LISTDESC"]) + "  By  " + Convert.ToString(logDT.Rows[0]["USERNAME"]) + "  On  " + Convert.ToString(logDT.Rows[0]["APRVDATE"]);//APRVDATE
                    //    lblAPRV2.Text = "Approval 2 :  " + Convert.ToString(logDT.Rows[1]["LISTDESC"]) + "  By  " + Convert.ToString(logDT.Rows[1]["USERNAME"]) + "  On  " + Convert.ToString(logDT.Rows[1]["APRVDATE"]);//APRVDATE
                    //    lblAPRV1.Visible = true;
                    //    lblAPRV2.Visible = true;
                    //}

                    if (logDT.Rows.Count > 0)
                    {
                        for (int k = 0; k < logDT.Rows.Count; k++)
                        {
                            lblAPRV1.Text = lblAPRV1.Text + " <br/>" + Convert.ToString(logDT.Rows[k]["LISTDESC"]) + "  By  " + Convert.ToString(logDT.Rows[k]["USERNAME"]) + "  On  " + Convert.ToString(logDT.Rows[k]["APRVDATE"]) + " , Reason : " + Convert.ToString(logDT.Rows[k]["REASON"]);
                            lblAPRV1.Visible = true;
                        }
                    }



                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                }
                else
                {
                    gvDetail.DataSource = string.Empty;
                    gvDetail.DataBind();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }



        protected void gvDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDetail.EditIndex = -1;
            DataTable dt = new DataTable();
            dt = objMainClass.GetMRDetails(lblMRNo.Text, 4);
            gvDetail.DataSource = dt;
            gvDetail.DataBind();

            decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("ITEMQTY"));
            gvDetail.FooterRow.Cells[5].Text = total.ToString("N3");

            decimal ITEMAMOUNT = dt.AsEnumerable().Sum(row => row.Field<decimal>("ITEMAMOUNT"));
            gvDetail.FooterRow.Cells[7].Text = ITEMAMOUNT.ToString("N2");

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
        }

        protected void gvDetail_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //string mrno = Convert.ToString(gvDetail.Rows[e.NewEditIndex].Cells[1].Text);
            if (FormRights.bAdd == false)
            {

            }
            else
            {
                gvDetail.EditIndex = e.NewEditIndex;
                DataTable dt = new DataTable();
                dt = objMainClass.GetMRDetails(lblMRNo.Text, 4);
                gvDetail.DataSource = dt;
                gvDetail.DataBind();

                decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("ITEMQTY"));
                gvDetail.FooterRow.Cells[5].Text = total.ToString("N3");

                decimal ITEMAMOUNT = dt.AsEnumerable().Sum(row => row.Field<decimal>("ITEMAMOUNT"));
                gvDetail.FooterRow.Cells[7].Text = ITEMAMOUNT.ToString("N2");

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
            }
        }

        protected void gvDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {



            TextBox txtItemcode = gvDetail.Rows[e.RowIndex].FindControl("txtItemcode") as TextBox;
            Label lblID = gvDetail.Rows[e.RowIndex].FindControl("lblID") as Label;

            Label lblUOMID = gvDetail.Rows[e.RowIndex].FindControl("lblUOMID") as Label;

            TextBox txtItemQty = gvDetail.Rows[e.RowIndex].FindControl("txtItemQty") as TextBox;
            if (Convert.ToDecimal(txtItemQty.Text) > 0)
            {
                txtItemcode.Text = txtItemcode.Text.Split('-')[0].Trim();

                bool iResult = objMainClass.UpdateMRDtl(lblMRNo.Text, lblID.Text, txtItemcode.Text, objMainClass.intCmpId, 1, "", lblUOMID.Text, txtItemQty.Text);
                gvDetail.EditIndex = -1;
                DataTable dt = new DataTable();
                dt = objMainClass.GetMRDetails(lblMRNo.Text, 4);
                gvDetail.DataSource = dt;
                gvDetail.DataBind();

                decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("ITEMQTY"));
                gvDetail.FooterRow.Cells[5].Text = total.ToString("N3");

                decimal ITEMAMOUNT = dt.AsEnumerable().Sum(row => row.Field<decimal>("ITEMAMOUNT"));
                gvDetail.FooterRow.Cells[7].Text = ITEMAMOUNT.ToString("N2");

            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);

        }

        protected void gvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblItemCode = (e.Row.FindControl("lblItemCode") as Label);
                if (lblItemCode != null)
                {

                    if (lblItemCode.Text != null && lblItemCode.Text != "&nbsp;" && lblItemCode.Text != "")
                    {
                        Label lblITEMQTY = (e.Row.FindControl("lblITEMQTY") as Label);
                        Label lblISSUEQTY = (e.Row.FindControl("lblISSUEQTY") as Label);

                        if (Convert.ToDecimal(lblITEMQTY.Text) <= Convert.ToDecimal(lblISSUEQTY.Text))
                        {

                        }
                        else
                        {
                            //Label lblPRNO = (e.Row.FindControl("lblPRNO") as Label);
                            //if (lblPRNO.Text != null)
                            //{


                            decimal bal;
                            Label lblITEMPLANTCD = (e.Row.FindControl("lblITEMPLANTCD") as Label);
                            Label lblITEMLOCCD = (e.Row.FindControl("lblITEMLOCCD") as Label);
                            if (lblITEMPLANTCD.Text.ToString().Split('-')[0].Trim() == "1001")
                            {
                                if (lblITEMLOCCD.Text.ToString().Split('-')[0].Trim() == "WS19")
                                {
                                    bal = objMainClass.SP_GET_STOCK_FUNCTION(objMainClass.intCmpId, objMainClass.getFinYear(objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true)), lblItemCode.Text, objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true), lblITEMPLANTCD.Text.ToString().Split('-')[0].Trim(), "TS01");
                                }
                                else
                                {
                                    bal = objMainClass.SP_GET_STOCK_FUNCTION(objMainClass.intCmpId, objMainClass.getFinYear(objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true)), lblItemCode.Text, objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true), lblITEMPLANTCD.Text.ToString().Split('-')[0].Trim(), "M001");
                                }
                            }
                            else
                            {
                                bal = objMainClass.SP_GET_STOCK_FUNCTION(objMainClass.intCmpId, objMainClass.getFinYear(objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true)), lblItemCode.Text, objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true), lblITEMPLANTCD.Text.ToString().Split('-')[0].Trim(), "MS01");
                            }
                            //bal = bal + objMainClass.SP_GET_STOCK_FUNCTION(objMainClass.intCmpId, objMainClass.getFinYear(objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true)), lblItemCode.Text, objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true), lblITEMPLANTCD.Text.ToString().Split('-')[0].Trim(), "M001");
                            //decimal bal = objMainClass.SP_GET_STOCK_FUNCTION(objMainClass.intCmpId, objMainClass.getFinYear(objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true)), lblItemCode.Text, objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true), "1001", "MS01");
                            //bal = bal + objMainClass.SP_GET_STOCK_FUNCTION(objMainClass.intCmpId, objMainClass.getFinYear(objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true)), lblItemCode.Text, objMainClass.setDateFormat(DateTime.Now.ToShortDateString(), true), "1001", "M001");
                            if (bal > 0)
                            {

                                Label lblAVAILQTY = (e.Row.FindControl("lblAVAILQTY") as Label);
                                lblAVAILQTY.Text = Convert.ToString(bal);
                                //issue

                                if (bal >= Convert.ToDecimal(lblITEMQTY.Text))
                                {
                                    Button btnIssue = (e.Row.FindControl("btnIssue") as Button);
                                    Button btnPR = (e.Row.FindControl("btnPR") as Button);
                                    btnIssue.Text = "ISSUE";
                                    btnIssue.Visible = true;
                                    btnPR.Visible = false;
                                }
                                else
                                {
                                    Label lblPRNO = (e.Row.FindControl("lblPRNO") as Label);
                                    if (lblPRNO.Text != null && lblPRNO.Text != "&nbsp;" && lblPRNO.Text != string.Empty)
                                    {
                                        Button btnPR = (e.Row.FindControl("btnPR") as Button);
                                        btnPR.Text = "PR";
                                        btnPR.Visible = false;
                                        if (bal > 0)
                                        {
                                            Button btnIssue = (e.Row.FindControl("btnIssue") as Button);
                                            btnIssue.Text = "ISSUE";
                                            btnIssue.Visible = true;
                                        }
                                    }
                                    else
                                    {

                                        if (bal > 0)
                                        {
                                            Button btnIssue = (e.Row.FindControl("btnIssue") as Button);
                                            btnIssue.Text = "ISSUE";
                                            btnIssue.Visible = true;
                                        }

                                        Button btnPR = (e.Row.FindControl("btnPR") as Button);
                                        btnPR.Text = "PR";
                                        btnPR.Visible = true;
                                    }
                                }
                            }
                            else
                            {
                                //pr
                                //lblPRNO
                                Label lblPRNO = (e.Row.FindControl("lblPRNO") as Label);
                                if (lblPRNO.Text != null && lblPRNO.Text != "&nbsp;" && lblPRNO.Text != string.Empty)
                                {
                                    Button btnIssue = (e.Row.FindControl("btnIssue") as Button);
                                    btnIssue.Visible = false;
                                }
                                else
                                {


                                    Button btnIssue = (e.Row.FindControl("btnIssue") as Button);
                                    Button btnPR = (e.Row.FindControl("btnPR") as Button);
                                    btnPR.Text = "PR";
                                    btnIssue.Visible = false;
                                    btnPR.Visible = true;
                                }

                            }
                            //}
                        }
                    }
                    else
                    {
                        Button btnIssue = (e.Row.FindControl("btnIssue") as Button);
                        btnIssue.Text = "";
                        btnIssue.Visible = false;
                        Button btnPR = (e.Row.FindControl("btnPR") as Button);
                        btnPR.Text = "";
                        btnPR.Visible = false;
                    }

                }
            }
        }

        protected void btnIssue_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((Button)sender).NamingContainer;
            string ITEMSR = string.Empty;
            for (int i = 0; i < gvDetail.Rows.Count; i++)
            {
                Button btnIssue = gvDetail.Rows[i].FindControl("btnIssue") as Button;
                if (btnIssue.Text == "ISSUE")
                {
                    if (ITEMSR == string.Empty)
                    {
                        ITEMSR = (gvDetail.Rows[i].FindControl("lblID") as Label).Text;
                    }
                    else
                    {
                        ITEMSR = ITEMSR + "," + (gvDetail.Rows[i].FindControl("lblID") as Label).Text;
                    }
                }
            }
            Response.Redirect("CreateIST.aspx?ITEMSR=" + ITEMSR + "&MRNO=" + lblMRNo.Text, true);
        }

        protected void btnPR_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((Button)sender).NamingContainer;
            string ITEMSR = string.Empty;
            for (int i = 0; i < gvDetail.Rows.Count; i++)
            {
                Button btnPR = gvDetail.Rows[i].FindControl("btnPR") as Button;
                Label lblPRNO = gvDetail.Rows[i].FindControl("lblPRNO") as Label;
                Label lblAVAILQTY = gvDetail.Rows[i].FindControl("lblAVAILQTY") as Label;

                if (btnPR.Text == "PR")
                {
                    if (ITEMSR == string.Empty)
                    {
                        ITEMSR = (gvDetail.Rows[i].FindControl("lblID") as Label).Text;
                    }
                    else
                    {
                        ITEMSR = ITEMSR + "," + (gvDetail.Rows[i].FindControl("lblID") as Label).Text;
                    }
                }
            }
            if (ITEMSR != string.Empty)
            {
                Response.Redirect("CreatePR.aspx?ITEMSR=" + ITEMSR + "&MRNO=" + lblMRNo.Text, true);
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetItemCode(string prefixText, int count)
        {
            List<string> ItemCode = new List<string>();

            MainClass objMainClass = new MainClass();
            ItemCode = objMainClass.GetItemData(prefixText);

            return ItemCode;

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected void lnkExport_Click(object sender, EventArgs e)
        {
            try
            {
                string attachment = "attachment; filename=ApprovedMR.xls";
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

                throw ex;
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
                            if (plantArray[i].Trim() == ddlPlantCode.SelectedValue)
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

        protected void txtItemcode_TextChanged(object sender, EventArgs e)
        {
            //gvDetail.EditIndex = e.NewEditIndex;
            //DataTable dt = new DataTable();
            //dt = objMainClass.GetMRDetails(lblMRNo.Text, 4);
            //gvDetail.DataSource = dt;
            //gvDetail.DataBind();

            //GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent.Parent.Parent;
            GridViewRow currentRow = (sender as TextBox).NamingContainer as GridViewRow;


            TextBox txtItemcode = (TextBox)currentRow.FindControl("txtItemcode");

            Label lblITEMUOM = (Label)currentRow.FindControl("lblITEMUOM");

            Label lblITEMPLANTCD = (Label)currentRow.FindControl("lblITEMPLANTCD");

            Label lblITEMLOCCD = (Label)currentRow.FindControl("lblITEMLOCCD");

            Label lblUOMID = (Label)currentRow.FindControl("lblUOMID");


            //Label lblITEMUOM = (Label)gvList.Rows[gvList.EditIndex].FindControl("lblITEMUOM");

            //TextBox txtItemcode = (TextBox)gvList.Rows[gvList.EditIndex].FindControl("txtItemcode");

            //Label lblITEMPLANTCD = (Label)gvList.Rows[gvList.EditIndex].FindControl("lblITEMPLANTCD");

            //Label lblITEMLOCCD = (Label)gvList.Rows[gvList.EditIndex].FindControl("lblITEMLOCCD");

            DataTable dt = new DataTable();
            dt = objMainClass.GetItemDetails(txtItemcode.Text.Split('-')[0].Trim().ToString(), lblITEMPLANTCD.Text.Split('-')[0].Trim().ToString(), lblITEMLOCCD.Text.Split('-')[0].Trim().ToString());
            if (dt.Rows.Count > 0)
            {
                lblITEMUOM.Text = Convert.ToString(dt.Rows[0]["UNIT"]);
                lblUOMID.Text = Convert.ToString(dt.Rows[0]["sku"]);
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            string MRNO = grdrow.Cells[1].Text;
            LinkButton btnApprove = (grdrow.FindControl("btnApprove") as LinkButton);
            if (btnApprove.Text == "Reject")
            {
                lblPopupMRNO.Text = MRNO;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#mb-aprv').modal();", true);
            }
            else if (btnApprove.Text == "Close")
            {
                lblPopupCloseMRNO.Text = MRNO;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#mb-close').modal();", true);
            }
            //string MRNO = grdrow.Cells[1].Text;
            //DataTable dt = new DataTable();
            //dt = objMainClass.GetMRDetails(MRNO, 4);
            //if (dt.Rows.Count > 0)
            //{
            //    string prno = string.Empty;
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        if (prno == string.Empty)
            //        {
            //            prno = Convert.ToString(dt.Rows[i]["PRNO"]);
            //        }
            //        else
            //        {
            //            prno = prno + " , " + Convert.ToString(dt.Rows[i]["PRNO"]);
            //        }
            //    }

            //    if (prno == string.Empty)
            //    {
            //        lblPopupMRNO.Text = MRNO;
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#mb-aprv').modal();", true);
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('PR is created. So, You cannot reject this MR.!');", true);
            //    }

            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You cannot reject this MR.!');", true);
            //}
        }

        protected void lnkPopReject_Click(object sender, EventArgs e)
        {
            try
            {
                //DataTable frstRight = objDALUserRights.FIRST_APPROVE_RIGHTS("MR", Convert.ToString(Session["USERID"]), Convert.ToString(ViewState["PlantCode"]), Convert.ToString(ViewState["DeptCode"]));

                bool result = objMainClass.ApproveMRNEW(lblPopupMRNO.Text, (int)APRVTYPE.REJECT, "MR", Convert.ToString(Session["USERID"]), txtAPREJReason.Text, 1);//Convert.ToInt32(frstRight.Rows[0]["APRVSEQ"])
                if (result == true)
                {

                    DataTable dt = new DataTable();
                    dt = objMainClass.GetMRDetails(lblMRNo.Text, 1);
                    //STATUSID
                    for (int o = 0; o < dt.Rows.Count; o++)
                    {
                        if (Convert.ToString(dt.Rows[o]["STATUSID"]) == "2")
                        {
                            bool iResult = objMainClass.UpdateMRDtlStatus(lblMRNo.Text, Convert.ToString(dt.Rows[o]["ID"]), 0, objMainClass.intCmpId, 4);
                        }
                    }

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('MR rejected successfully!');$('.close').click(function(){window.location.href ='ApprovedMR.aspx' });", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Please try again!');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Convert.ToString(Session["DEPTCD"]) == "5" || Convert.ToString(Session["DEPTCD"]) == "10" || Convert.ToString(Session["DEPTCD"]) == "15")
                {
                    string MRNO = Convert.ToString(e.Row.Cells[1].Text);
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetMRDetails(MRNO, 4);
                    if (dt.Rows.Count > 0)
                    {
                        string prno = string.Empty;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (prno == string.Empty)
                            {
                                prno = Convert.ToString(dt.Rows[i]["PRNO"]);
                            }
                            else
                            {
                                prno = prno + " , " + Convert.ToString(dt.Rows[i]["PRNO"]);
                            }
                        }
                        LinkButton btnApprove = (e.Row.FindControl("btnApprove") as LinkButton);
                        Label lblStick = (e.Row.FindControl("lblStick") as Label);

                        if (prno == string.Empty)
                        {
                            btnApprove.Text = "Reject";
                            btnApprove.Visible = true;
                            lblStick.Visible = true;
                        }
                        else
                        {
                            btnApprove.Text = "Close";
                            btnApprove.Visible = true;
                            lblStick.Visible = true;
                        }

                    }
                }
            }
        }

        protected void lnkShortClose_Click(object sender, EventArgs e)
        {
            try
            {
                bool result = objMainClass.UpdateMRDtl(lblPopupCloseMRNO.Text, "", "", objMainClass.intCmpId, 6, "", "", "");
                if (result == true)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('MR Closed successfully!');$('.close').click(function(){window.location.href ='ApprovedMR.aspx' });", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Please try again!');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lnkInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

                    string extension = ((Label)grdrow.FindControl("lblMREXTENSION")).Text;
                    string mrno = grdrow.Cells[1].Text;
                    if (extension != null && extension != "" && extension != string.Empty)
                    {
                        string url = "ViewMRInvoice.aspx?MRNO=" + mrno;
                        string s = "window.open('" + url + "', 'popup_window', 'width=500,height=500,left=500,top=100,resizable=yes');";
                        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invoice not Uploaded for this MR!');", true);
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

        protected void lnkViewInv_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    string extension = hfMREXTENSION.Value;
                    string mrno = lblMRNo.Text;
                    if (extension != null && extension != "" && extension != string.Empty)
                    {
                        string url = "ViewMRInvoice.aspx?MRNO=" + mrno;
                        string s = "window.open('" + url + "', 'popup_window', 'width=500,height=500,left=500,top=100,resizable=yes');";
                        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invoice not Uploaded for this MR!');", true);
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
    }
}