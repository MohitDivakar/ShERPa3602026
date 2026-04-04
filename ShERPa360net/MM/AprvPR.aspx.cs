using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.MM
{
    public partial class AprvPR : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();

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
                        if (FormRights.bView == false) //if (objDALUserRights.bView == false)
                        {
                            //Session.Abandon();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        //txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");//Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        //txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");//Convert.ToDateTime(DateTime.Now).ToShortDateString();

                        DataTable frstRight = objDALUserRights.FIRST_APPROVE_RIGHTS("PR", Convert.ToString(Session["USERID"]), Convert.ToString(Session["PLANTCD"]), Convert.ToString(Session["DEPTCD"]));

                        if (frstRight.Rows.Count > 0)
                        {

                            string plntcd = string.Empty;
                            for (int u = 0; u < frstRight.Rows.Count; u++)
                            {
                                if (plntcd == string.Empty)
                                {
                                    plntcd = Convert.ToString(frstRight.Rows[u]["PLANTCD"]);
                                }
                                else
                                {
                                    plntcd = plntcd + "," + Convert.ToString(frstRight.Rows[u]["PLANTCD"]);
                                }
                            }



                            string deptcd = string.Empty;
                            for (int u = 0; u < frstRight.Rows.Count; u++)
                            {
                                if (deptcd == string.Empty)
                                {
                                    deptcd = Convert.ToString(frstRight.Rows[u]["DEPTCD"]);
                                }
                                else
                                {
                                    deptcd = deptcd + "," + Convert.ToString(frstRight.Rows[u]["DEPTCD"]);
                                }
                            }

                            ViewState["PlantCode"] = plntcd;
                            ViewState["DeptCode"] = deptcd;



                            DataTable dt = new DataTable();
                            dt = objMainClass.GetAPRVPRData(txtPrno.Text, txtFromDate.Text, txtToDate.Text, "", 0, deptcd, Convert.ToInt32(frstRight.Rows[0]["APRVSEQ"]), Convert.ToInt32(Session["USERID"]), plntcd);
                            if (dt.Rows.Count > 0)
                            {
                                gvList.DataSource = dt;
                                gvList.DataBind();
                            }
                            else
                            {
                                gvList.DataSource = string.Empty;
                                gvList.DataBind();
                                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
                            }





                            //if (Convert.ToString(frstRight.Rows[0]["APRVSEQ"]) == "2")
                            //{
                            //    DataTable dt = new DataTable();
                            //    dt = objMainClass.GetAPRVPRData(txtPrno.Text, txtFromDate.Text, txtToDate.Text, "", 0, 0, 2);
                            //    if (dt.Rows.Count > 0)
                            //    {
                            //        gvList.DataSource = dt;
                            //        gvList.DataBind();
                            //    }
                            //    else
                            //    {
                            //        gvList.DataSource = string.Empty;
                            //        gvList.DataBind();
                            //        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
                            //    }
                            //}
                            //else if (Convert.ToString(frstRight.Rows[0]["APRVSEQ"]) == "1")
                            //{
                            //    DataTable dt = new DataTable();
                            //    dt = objMainClass.GetAPRVPRData(txtPrno.Text, txtFromDate.Text, txtToDate.Text, "", 0, 0, 1);
                            //    if (dt.Rows.Count > 0)
                            //    {
                            //        gvList.DataSource = dt;
                            //        gvList.DataBind();
                            //    }
                            //    else
                            //    {
                            //        gvList.DataSource = string.Empty;
                            //        gvList.DataBind();
                            //        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
                            //    }
                            //}

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorized to view this page');$('.close').click(function(){window.location.href ='MMDashboard.aspx' });", true);
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

        protected void btnDetails_Click(object sender, EventArgs e)
        {
            try
            {
                lblAPRV1.Visible = false;
                lblAPRV2.Visible = false;
                lblAPRV1.Text = string.Empty;
                lblAPRV2.Text = string.Empty;

                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

                string prno = grdrow.Cells[1].Text;
                string status = grdrow.Cells[6].Text;
                if (status == "" || status == "&nbsp;")
                {
                    bool result = objDALUserRights.APPROVE_RIGHTS("PR", Convert.ToString(Session["USERID"]), Convert.ToString(ViewState["DeptCode"]), prno, 4, 1, Convert.ToString(ViewState["PlantCode"]));
                    if (result == false)
                    {
                        lnkPopReject.Visible = false;
                        lnkPopApprove.Visible = false;
                        lnkReject.Visible = false;
                        lnkApprove.Visible = false;
                        txtAPREJReason.Visible = false;
                        txtApRejDetReason.Visible = false;
                        lblRightsMessage.Text = "You are not authorized to Approve or Reject this PR!";
                        lblRightsMessage.Visible = true;
                    }
                    else if (result == true)
                    {
                        lnkPopReject.Visible = true;
                        lnkPopApprove.Visible = true;
                        lnkReject.Visible = true;
                        lnkApprove.Visible = true;
                        txtAPREJReason.Visible = true;
                        txtApRejDetReason.Visible = true;
                        lblRightsMessage.Text = string.Empty;
                        lblRightsMessage.Visible = false;
                    }

                }
                else
                {
                    bool result1 = objDALUserRights.APPROVE_RIGHTS("PR", Convert.ToString(Session["USERID"]), "0", prno, 2, 0, "");
                    if (result1 == false)
                    {

                        lnkPopReject.Visible = false;
                        lnkPopApprove.Visible = false;
                        lnkReject.Visible = false;
                        txtAPREJReason.Visible = false;
                        txtApRejDetReason.Visible = false;
                        lnkApprove.Visible = false;
                        lblRightsMessage.Text = "PR is already " + status + "";
                        lblRightsMessage.Visible = true;
                    }
                    else
                    {
                        DataTable frstRight = objDALUserRights.FIRST_APPROVE_RIGHTS("PR", Convert.ToString(Session["USERID"]), Convert.ToString(ViewState["PlantCode"]), Convert.ToString(ViewState["DeptCode"]));

                        bool result = objDALUserRights.APPROVE_RIGHTS("PR", Convert.ToString(Session["USERID"]), Convert.ToString(ViewState["DeptCode"]), prno, 4, Convert.ToInt32(frstRight.Rows[0]["APRVSEQ"]), Convert.ToString(ViewState["PlantCode"]));
                        if (result == false)
                        {
                            lnkPopReject.Visible = false;
                            lnkPopApprove.Visible = false;
                            lnkReject.Visible = false;
                            lnkApprove.Visible = false;
                            txtAPREJReason.Visible = false;
                            txtApRejDetReason.Visible = false;
                            lblRightsMessage.Text = "PR already " + status + "!";
                            lblRightsMessage.Visible = true;
                        }
                        else if (result == true)
                        {
                            lnkPopReject.Visible = true;
                            lnkPopApprove.Visible = true;
                            lnkReject.Visible = true;
                            lnkApprove.Visible = true;
                            txtAPREJReason.Visible = true;
                            txtApRejDetReason.Visible = true;
                            lblRightsMessage.Text = string.Empty;
                            lblRightsMessage.Visible = false;
                        }
                    }
                }

                DataTable dt = new DataTable();
                dt = objMainClass.GetPRDetails(prno, 3);
                if (dt.Rows.Count > 0)
                {

                    lblDoctype.Text = Convert.ToString(dt.Rows[0]["PRTYPE"]);
                    lblPRDate.Text = Convert.ToDateTime(dt.Rows[0]["PRDT"]).ToShortDateString();
                    lblPRNo.Text = Convert.ToString(dt.Rows[0]["PRNO"]);
                    lblRemark.Text = Convert.ToString(dt.Rows[0]["REMARK"]);

                    gvDetail.DataSource = dt;
                    gvDetail.DataBind();

                    DataTable logDT = new DataTable();
                    logDT = objMainClass.SELECT_REQUISITION_LOG(lblPRNo.Text);
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

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            string prno = grdrow.Cells[1].Text;
            lblPopupPRNO.Text = prno;
            string status = grdrow.Cells[6].Text;
            if (status == "" || status == "&nbsp;")
            {
                bool result = objDALUserRights.APPROVE_RIGHTS("PR", Convert.ToString(Session["USERID"]), Convert.ToString(ViewState["DeptCode"]), prno, 4, 1, Convert.ToString(ViewState["PlantCode"]));
                if (result == false)
                {
                    lnkPopReject.Visible = false;
                    lnkPopApprove.Visible = false;
                    lnkReject.Visible = false;
                    txtAPREJReason.Visible = false;
                    txtApRejDetReason.Visible = false;
                    lnkApprove.Visible = false;
                    lblRightsMessage.Text = "You are not authorized to Approve or Reject this PR!";
                    lblRightsMessage.Visible = true;
                }
                else if (result == true)
                {
                    lnkPopReject.Visible = true;
                    lnkPopApprove.Visible = true;
                    lnkReject.Visible = true;
                    lnkApprove.Visible = true;
                    txtAPREJReason.Visible = true;
                    txtApRejDetReason.Visible = true;
                    lblRightsMessage.Text = string.Empty;
                    lblRightsMessage.Visible = false;
                }


                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#mb-aprv').modal();", true);
            }
            else
            {
                bool result1 = objDALUserRights.APPROVE_RIGHTS("PR", Convert.ToString(Session["USERID"]), "0", prno, 2, 0, "");
                if (result1 == false)
                {

                    lnkPopReject.Visible = false;
                    lnkPopApprove.Visible = false;
                    lnkReject.Visible = false;
                    txtAPREJReason.Visible = false;
                    txtApRejDetReason.Visible = false;
                    lnkApprove.Visible = false;
                    lblRightsMessage.Text = "PR is already " + status + "";
                    lblRightsMessage.Visible = true;
                }
                else
                {

                    DataTable frstRight = objDALUserRights.FIRST_APPROVE_RIGHTS("PR", Convert.ToString(Session["USERID"]), Convert.ToString(ViewState["PlantCode"]), Convert.ToString(ViewState["DeptCode"]));

                    bool result = objDALUserRights.APPROVE_RIGHTS("PR", Convert.ToString(Session["USERID"]), Convert.ToString(ViewState["DeptCode"]), prno, 4, Convert.ToInt32(frstRight.Rows[0]["APRVSEQ"]), Convert.ToString(ViewState["PlantCode"]));
                    if (result == false)
                    {

                        lnkPopReject.Visible = false;
                        lnkPopApprove.Visible = false;
                        lnkReject.Visible = false;
                        txtAPREJReason.Visible = false;
                        txtApRejDetReason.Visible = false;
                        lnkApprove.Visible = false;
                        lblRightsMessage.Text = "You are not authorized to Approve or Reject this PR!";
                        lblRightsMessage.Visible = true;
                    }
                    else if (result == true)
                    {
                        txtAPREJReason.Visible = true;
                        lnkPopReject.Visible = true;
                        lnkPopApprove.Visible = true;
                        lnkReject.Visible = true;
                        txtApRejDetReason.Visible = true;
                        lnkApprove.Visible = true;
                        lblRightsMessage.Text = string.Empty;
                        lblRightsMessage.Visible = false;
                    }
                }
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('PR is already " + status + "');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#mb-aprv').modal();", true);
            }
        }

        protected void lnkSearhPR_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable frstRight = objDALUserRights.FIRST_APPROVE_RIGHTS("PR", Convert.ToString(Session["USERID"]), Convert.ToString(ViewState["PlantCode"]), Convert.ToString(ViewState["DeptCode"]));

                if (frstRight.Rows.Count > 0)
                {
                    string plntcd = string.Empty;
                    for (int u = 0; u < frstRight.Rows.Count; u++)
                    {
                        if (plntcd == string.Empty)
                        {
                            plntcd = Convert.ToString(frstRight.Rows[u]["PLANTCD"]);
                        }
                        else
                        {
                            plntcd = plntcd + "," + Convert.ToString(frstRight.Rows[u]["PLANTCD"]);
                        }
                    }

                    string deptcd = string.Empty;
                    for (int u = 0; u < frstRight.Rows.Count; u++)
                    {
                        if (deptcd == string.Empty)
                        {
                            deptcd = Convert.ToString(frstRight.Rows[u]["DEPTCD"]);
                        }
                        else
                        {
                            deptcd = deptcd + "," + Convert.ToString(frstRight.Rows[u]["DEPTCD"]);
                        }
                    }

                    DataTable dt = new DataTable();
                    dt = objMainClass.GetAPRVPRData(txtPrno.Text, txtFromDate.Text, txtToDate.Text, "", 0, deptcd, Convert.ToInt32(frstRight.Rows[0]["APRVSEQ"]), Convert.ToInt32(Session["USERID"]), plntcd);
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

                    //if (Convert.ToString(frstRight.Rows[0]["APRVSEQ"]) == "2")
                    //{
                    //    DataTable dt = new DataTable();
                    //    dt = objMainClass.GetAPRVPRData(txtPrno.Text, txtFromDate.Text, txtToDate.Text, "", 0, 0, 2);
                    //    if (dt.Rows.Count > 0)
                    //    {
                    //        gvList.DataSource = dt;
                    //        gvList.DataBind();
                    //    }
                    //    else
                    //    {
                    //        gvList.DataSource = string.Empty;
                    //        gvList.DataBind();
                    //        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
                    //    }
                    //}
                    //else if (Convert.ToString(frstRight.Rows[0]["APRVSEQ"]) == "1")
                    //{
                    //    DataTable dt = new DataTable();
                    //    dt = objMainClass.GetAPRVPRData(txtPrno.Text, txtFromDate.Text, txtToDate.Text, "", 0, 0, 1);
                    //    if (dt.Rows.Count > 0)
                    //    {
                    //        gvList.DataSource = dt;
                    //        gvList.DataBind();
                    //    }
                    //    else
                    //    {
                    //        gvList.DataSource = string.Empty;
                    //        gvList.DataBind();
                    //        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
                    //    }
                    //}
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');", true);
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
                DataTable frstRight = objDALUserRights.FIRST_APPROVE_RIGHTS("PR", Convert.ToString(Session["USERID"]), Convert.ToString(ViewState["PlantCode"]), Convert.ToString(ViewState["DeptCode"]));

                bool result = objMainClass.ApprovePR(lblPRNo.Text, (int)APRVTYPE.APPROVED, "PR", Convert.ToString(Session["USERID"]), txtApRejDetReason.Text, Convert.ToInt32(frstRight.Rows[0]["APRVSEQ"]));
                if (result == true)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetPRDetails(lblPRNo.Text, 3);
                    for (int o = 0; o < dt.Rows.Count; o++)
                    {
                        if (Convert.ToString(dt.Rows[o]["STATUSID"]) == "2")
                        {
                            bool iResult = objMainClass.UpdatePRDtlStatus(lblPRNo.Text, Convert.ToString(dt.Rows[o]["ID"]), 1, objMainClass.intCmpId, 1);
                        }
                    }


                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('PR approved successfully!');$('.close').click(function(){window.location.href ='AprvPR.aspx' });", true);
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

        protected void lnkReject_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable frstRight = objDALUserRights.FIRST_APPROVE_RIGHTS("PR", Convert.ToString(Session["USERID"]), Convert.ToString(ViewState["PlantCode"]), Convert.ToString(ViewState["DeptCode"]));

                bool result = objMainClass.ApprovePR(lblPRNo.Text, (int)APRVTYPE.REJECT, "PR", Convert.ToString(Session["USERID"]), txtApRejDetReason.Text, Convert.ToInt32(frstRight.Rows[0]["APRVSEQ"]));
                if (result == true)
                {

                    DataTable dt = new DataTable();
                    dt = objMainClass.GetPRDetails(lblPRNo.Text, 3);
                    for (int o = 0; o < dt.Rows.Count; o++)
                    {
                        if (Convert.ToString(dt.Rows[o]["STATUSID"]) == "2")
                        {
                            bool iResult = objMainClass.UpdatePRDtlStatus(lblPRNo.Text, Convert.ToString(dt.Rows[o]["ID"]), 0, objMainClass.intCmpId, 1);
                        }
                    }

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('PR rejected successfully!');$('.close').click(function(){window.location.href ='AprvPR.aspx' });", true);
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

        protected void lnkPopReject_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable frstRight = objDALUserRights.FIRST_APPROVE_RIGHTS("PR", Convert.ToString(Session["USERID"]), Convert.ToString(ViewState["PlantCode"]), Convert.ToString(ViewState["DeptCode"]));

                bool result = objMainClass.ApprovePR(lblPopupPRNO.Text, (int)APRVTYPE.REJECT, "PR", Convert.ToString(Session["USERID"]), txtAPREJReason.Text, Convert.ToInt32(frstRight.Rows[0]["APRVSEQ"]));
                if (result == true)
                {

                    DataTable dt = new DataTable();
                    dt = objMainClass.GetPRDetails(lblPRNo.Text, 3);
                    for (int o = 0; o < dt.Rows.Count; o++)
                    {
                        if (Convert.ToString(dt.Rows[o]["STATUSID"]) == "2")
                        {
                            bool iResult = objMainClass.UpdatePRDtlStatus(lblPRNo.Text, Convert.ToString(dt.Rows[o]["ID"]), 0, objMainClass.intCmpId, 1);
                        }
                    }

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('PR rejected successfully!');$('.close').click(function(){window.location.href ='AprvPR.aspx' });", true);
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

        protected void lnkPopApprove_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable frstRight = objDALUserRights.FIRST_APPROVE_RIGHTS("PR", Convert.ToString(Session["USERID"]), Convert.ToString(ViewState["PlantCode"]), Convert.ToString(ViewState["DeptCode"]));

                bool result = objMainClass.ApprovePR(lblPopupPRNO.Text, (int)APRVTYPE.APPROVED, "PR", Convert.ToString(Session["USERID"]), txtAPREJReason.Text, Convert.ToInt32(frstRight.Rows[0]["APRVSEQ"]));
                if (result == true)
                {

                    DataTable dt = new DataTable();
                    dt = objMainClass.GetPRDetails(lblPRNo.Text, 3);
                    for (int o = 0; o < dt.Rows.Count; o++)
                    {
                        if (Convert.ToString(dt.Rows[o]["STATUSID"]) == "2")
                        {
                            bool iResult = objMainClass.UpdatePRDtlStatus(lblPRNo.Text, Convert.ToString(dt.Rows[o]["ID"]), 1, objMainClass.intCmpId, 1);
                        }
                    }

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('PR approved successfully!');$('.close').click(function(){window.location.href ='AprvPR.aspx' });", true);
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

        protected void gvDetail_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDetail.EditIndex = e.NewEditIndex;
            DataTable dt = new DataTable();
            dt = objMainClass.GetPRDetails(lblPRNo.Text, 3);
            if (dt.Rows.Count > 0)
            {

                lblDoctype.Text = Convert.ToString(dt.Rows[0]["PRTYPE"]);
                lblPRDate.Text = Convert.ToDateTime(dt.Rows[0]["PRDT"]).ToShortDateString();
                lblPRNo.Text = Convert.ToString(dt.Rows[0]["PRNO"]);
                lblRemark.Text = Convert.ToString(dt.Rows[0]["REMARK"]);

                gvDetail.DataSource = dt;
                gvDetail.DataBind();

                DataTable logDT = new DataTable();
                logDT = objMainClass.SELECT_REQUISITION_LOG(lblPRNo.Text);
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

        protected void gvDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDetail.EditIndex = -1;
            DataTable dt = new DataTable();
            dt = objMainClass.GetPRDetails(lblPRNo.Text, 3);
            if (dt.Rows.Count > 0)
            {

                lblDoctype.Text = Convert.ToString(dt.Rows[0]["PRTYPE"]);
                lblPRDate.Text = Convert.ToDateTime(dt.Rows[0]["PRDT"]).ToShortDateString();
                lblPRNo.Text = Convert.ToString(dt.Rows[0]["PRNO"]);
                lblRemark.Text = Convert.ToString(dt.Rows[0]["REMARK"]);

                gvDetail.DataSource = dt;
                gvDetail.DataBind();

                DataTable logDT = new DataTable();
                logDT = objMainClass.SELECT_REQUISITION_LOG(lblPRNo.Text);
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

        protected void gvDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            RadioButtonList rblTrueFalse = gvDetail.Rows[e.RowIndex].FindControl("rblTrueFalse") as RadioButtonList;
            Label lblID = gvDetail.Rows[e.RowIndex].FindControl("lblID") as Label;
            bool iResult = objMainClass.UpdatePRDtlStatus(lblPRNo.Text, lblID.Text, Convert.ToInt32(rblTrueFalse.SelectedValue), objMainClass.intCmpId, 1);
            gvDetail.EditIndex = -1;
            DataTable dt = new DataTable();
            dt = objMainClass.GetPRDetails(lblPRNo.Text, 3);
            if (dt.Rows.Count > 0)
            {

                lblDoctype.Text = Convert.ToString(dt.Rows[0]["PRTYPE"]);
                lblPRDate.Text = Convert.ToDateTime(dt.Rows[0]["PRDT"]).ToShortDateString();
                lblPRNo.Text = Convert.ToString(dt.Rows[0]["PRNO"]);
                lblRemark.Text = Convert.ToString(dt.Rows[0]["REMARK"]);

                gvDetail.DataSource = dt;
                gvDetail.DataBind();

                DataTable logDT = new DataTable();
                logDT = objMainClass.SELECT_REQUISITION_LOG(lblPRNo.Text);
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('No Record Found!');$('.close').click(function(){window.location.href ='AprvPR.aspx' });", true);
            }
        }
    }
}