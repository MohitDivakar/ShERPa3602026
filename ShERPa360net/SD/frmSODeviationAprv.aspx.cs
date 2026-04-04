using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.SD
{
    public partial class frmSODeviationAprv : System.Web.UI.Page
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
                            gvList.Columns[18].Visible = false;
                        }
                        else
                        {
                            gvList.Columns[18].Visible = true;
                        }


                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();

                        objBindDDL.FillStatuses(ddlStatus);
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("1")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("2")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("3"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("4")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("5")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("6"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("7")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("8")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("9"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("10")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("11")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("12"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("13")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("14")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("15"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("16")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("17")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("19"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("20")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("21")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("22"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("23")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("26")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("28"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("29")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("30")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("31"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("33")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("34"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("37")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("38")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("39"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("40")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("41")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("42"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("43")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("44")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("45"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("46")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("47")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("48"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("49")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("50")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("52"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("53")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("54")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("55"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("56")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("57"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("59")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("60")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("61"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("62")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("63")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("64"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("65")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("67"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("68")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("69")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("70"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("71")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("72")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("73"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("74")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("75")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("76"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("77")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("78")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("79"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("80")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("81")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("82"));
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("83")); ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("84"));
                        ddlStatus.SelectedValue = "66";
                        GetData(0);
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

        public void GetData(int ID)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.SearchSODeviation(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, txtSONO.Text, Convert.ToInt32(ddlStatus.SelectedValue), "SEARCHDEVIATION", ID);
                    if (dt.Rows.Count > 0)
                    {
                        gvList.DataSource = dt;
                        gvList.DataBind();

                        if (Convert.ToInt32(dt.Rows[0]["STATUS"]) == (int)STATUS.ApprovalPending)
                        {
                            gvList.Columns[5].Visible = false;
                            gvList.Columns[9].Visible = false;
                            gvList.Columns[15].Visible = false;
                            gvList.Columns[16].Visible = false;
                            gvList.Columns[17].Visible = false;

                            gvList.Columns[18].Visible = true;
                        }
                        else if (Convert.ToInt32(dt.Rows[0]["STATUS"]) == (int)STATUS.Cancelled)
                        {
                            gvList.Columns[5].Visible = true;
                            gvList.Columns[9].Visible = true;
                            gvList.Columns[15].Visible = true;
                            gvList.Columns[16].Visible = true;
                            gvList.Columns[17].Visible = true;

                            gvList.Columns[18].Visible = false;
                        }
                        else if (Convert.ToInt32(dt.Rows[0]["STATUS"]) == (int)STATUS.Approved)
                        {
                            gvList.Columns[5].Visible = true;
                            gvList.Columns[9].Visible = true;
                            gvList.Columns[15].Visible = true;
                            gvList.Columns[16].Visible = true;
                            gvList.Columns[17].Visible = true;

                            gvList.Columns[18].Visible = false;
                        }
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

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void lnkExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    try
                    {
                        if (Session["USERID"] != null)
                        {
                            string attachment = "attachment; filename=SODEVIATIONLIST.xls";
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

        protected void lnkSerchSO_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GetData(0);
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

        protected void btnDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    Label lblGVID = (Label)grdrow.FindControl("lblGVID");
                    string ID = lblGVID.Text;

                    DataTable dt = new DataTable();
                    dt = objMainClass.SearchSODeviation(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, txtSONO.Text, Convert.ToInt32(ddlStatus.SelectedValue), "SEARCHDEVIATION", Convert.ToInt32(ID));
                    if (dt.Rows.Count > 0)
                    {

                        lblSONO.Text = Convert.ToString(dt.Rows[0]["SONO"]);
                        lblSOID.Text = Convert.ToString(dt.Rows[0]["ID"]);
                        lblSOSRNO.Text = Convert.ToString(dt.Rows[0]["SRNO"]);
                        lblOldItemCode.Text = Convert.ToString(dt.Rows[0]["OLDITEMCODE"]);
                        lblOldItemID.Text = Convert.ToString(dt.Rows[0]["OLDITEMID"]);
                        txtItemcode.Text = Convert.ToString(dt.Rows[0]["NEWITEMCODE"]);
                        lblNewItemID.Text = Convert.ToString(dt.Rows[0]["NEWITEMID"]);
                        lblOldItemDesc.Text = Convert.ToString(dt.Rows[0]["OLDITEMDESC"]);
                        lblNewItemDesc.Text = Convert.ToString(dt.Rows[0]["NEWITEMDESC"]);
                        lblOldItemGrade.Text = Convert.ToString(dt.Rows[0]["OLDITEMGRADE"]);
                        ddlItemGrade.Text = Convert.ToString(dt.Rows[0]["NEWITEMGRADE"]);
                        txtReason.Text = Convert.ToString(dt.Rows[0]["REMARKS"]);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
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

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    Label lblGVID = (Label)grdrow.FindControl("lblGVID");
                    string ID = lblGVID.Text;
                    string SONO = grdrow.Cells[1].Text;
                    string SOSRNO = grdrow.Cells[2].Text;
                    string REMARKS = grdrow.Cells[11].Text;

                    string NEWITEMDESC = grdrow.Cells[9].Text;
                    string NEWGRADE = grdrow.Cells[10].Text;

                    Label lblGVOldItemID = (Label)grdrow.FindControl("lblGVOldItemID");
                    Label lblGVNewItemID = (Label)grdrow.FindControl("lblGVNewItemID");

                    lblPopupSOID.Text = ID;
                    lblPopupSONO.Text = SONO;
                    lblPopupSOSrNo.Text = SOSRNO;
                    lblPopupSOReason.Text = REMARKS;

                    lblPopupNewItemID.Text = lblGVNewItemID.Text;
                    lblPopupNewItemDesc.Text = NEWITEMDESC;
                    lblPopupNewGrade.Text = NEWGRADE;
                    lblPopupOldItemID.Text = lblGVOldItemID.Text;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#mb-aprv').modal();", true);

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

        protected void lnkReject_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    int iResult = objMainClass.UpdateSODeviation(objMainClass.intCmpId, (int)STATUS.Cancelled, txtApRejDetReason.Text, Convert.ToInt32(Session["USERID"]),
                        Convert.ToInt32(lblSOID.Text), "UPDATESTATUS", 0, "", "", 0, "", "", 0);

                    if (iResult == 1)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('SO Deviation Rejected Successfully!');$('.close').click(function(){window.location.href ='frmSODeviationAprv.aspx' });", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Please try again!');", true);
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

        protected void lnkApprove_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    int iResult = objMainClass.UpdateSODeviation(objMainClass.intCmpId, (int)STATUS.Approved, txtApRejDetReason.Text, Convert.ToInt32(Session["USERID"]),
                        Convert.ToInt32(lblSOID.Text), "UPDATESTATUS", Convert.ToInt32(lblNewItemID.Text), lblNewItemDesc.Text, ddlItemGrade.Text, Convert.ToInt32(lblOldItemID.Text),
                        txtReason.Text, objMainClass.strConvertZeroPadding(lblSONO.Text), Convert.ToInt32(lblSOSRNO.Text));

                    if (iResult == 1)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('SO Deviation Approved Successfully!');$('.close').click(function(){window.location.href ='frmSODeviationAprv.aspx' });", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Please try again!');", true);
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

        protected void lnkPopReject_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    int iResult = objMainClass.UpdateSODeviation(objMainClass.intCmpId, (int)STATUS.Cancelled, txtAPREJReason.Text, Convert.ToInt32(Session["USERID"]),
                        Convert.ToInt32(lblPopupSOID.Text), "UPDATESTATUS", 0, "", "", 0, "", "", 0);

                    if (iResult == 1)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('SO Deviation Approved Successfully!');$('.close').click(function(){window.location.href ='frmSODeviationAprv.aspx' });", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Please try again!');", true);
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

        protected void lnkPopApprove_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    int iResult = objMainClass.UpdateSODeviation(objMainClass.intCmpId, (int)STATUS.Approved, txtAPREJReason.Text, Convert.ToInt32(Session["USERID"]),
                        Convert.ToInt32(lblPopupSOID.Text), "UPDATESTATUS", Convert.ToInt32(lblPopupNewItemID.Text), lblPopupNewItemDesc.Text, lblPopupNewGrade.Text,
                        Convert.ToInt32(lblPopupOldItemID.Text), lblPopupSOReason.Text, objMainClass.strConvertZeroPadding(lblPopupSONO.Text), Convert.ToInt32(lblPopupSOSrNo.Text));

                    if (iResult == 1)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('SO Deviation Approved Successfully!');$('.close').click(function(){window.location.href ='frmSODeviationAprv.aspx' });", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Please try again!');", true);
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