using Newtonsoft.Json;
using RestSharp;
using ShERPa360net.Class;
using ShERPa360net.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.SD
{
    public partial class frmSOItemAssign : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        WAClass objWAClass = new WAClass();

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




                        if (Request.QueryString.Count > 0)
                        {
                            if (Convert.ToString(Request.QueryString["SONO"]) != null && Convert.ToString(Request.QueryString["SONO"]) != string.Empty && Convert.ToString(Request.QueryString["SONO"]) != "")
                            {
                                Session["SONO"] = Convert.ToString(Request.QueryString["SONO"]);

                            }
                            if (Convert.ToString(Request.QueryString["PLANT"]) != null && Convert.ToString(Request.QueryString["PLANT"]) != string.Empty && Convert.ToString(Request.QueryString["PLANT"]) != "")
                            {
                                Session["PLANT"] = Convert.ToString(Request.QueryString["PLANT"]);

                            }

                            Response.Redirect(Request.Url.AbsolutePath, false);
                        }
                        else
                        {
                            if (Session["SONO"] != null && Convert.ToString(Session["SONO"]) != "" && Convert.ToString(Session["SONO"]) != string.Empty && Session["PLANT"] != null && Convert.ToString(Session["PLANT"]) != "" && Convert.ToString(Session["PLANT"]) != string.Empty)
                            {
                                txtDocNo.Text = Convert.ToString(Session["SONO"]);

                                string[] date = objMainClass.indianTime.Date.ToString("dd-MM-yyyy").Split('-');
                                DateTime fromdate = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), 1);
                                //txtFromDocDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");
                                //txtToDocDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                                objBindDDL.FillPlant(ddlPlant);
                                ddlPlant.SelectedValue = "1001";
                                ddlPlant.SelectedValue = Convert.ToString(Session["PLANT"]);

                                DataTable dt = new DataTable();
                                //dt = objMainClass.GetSOAge(objMainClass.intCmpId, txtFromDocDate.Text, txtToDocDate.Text, "", "", "", ddlPlant.SelectedValue);
                                //dt = objMainClass.GetSOAge(objMainClass.intCmpId, "01-01-2023", "", "", "", "", ddlPlant.SelectedValue, Convert.ToInt32(rblsalesFrom.SelectedValue));
                                dt = objMainClass.GetSOAge(objMainClass.intCmpId, "01-04-2022", "", txtDocNo.Text == "" ? "" : txtDocNo.Text, "", "", ddlPlant.SelectedValue, Convert.ToInt32(rblsalesFrom.SelectedValue));


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
                                string[] date = objMainClass.indianTime.Date.ToString("dd-MM-yyyy").Split('-');
                                DateTime fromdate = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), 1);
                                //txtFromDocDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");
                                //txtToDocDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                                objBindDDL.FillPlant(ddlPlant);
                                ddlPlant.SelectedValue = "1001";

                                DataTable dt = new DataTable();
                                //dt = objMainClass.GetSOAge(objMainClass.intCmpId, txtFromDocDate.Text, txtToDocDate.Text, "", "", "", ddlPlant.SelectedValue);
                                //dt = objMainClass.GetSOAge(objMainClass.intCmpId, "01-01-2023", "", "", "", "", ddlPlant.SelectedValue, Convert.ToInt32(rblsalesFrom.SelectedValue));
                                dt = objMainClass.GetSOAge(objMainClass.intCmpId, "01-04-2022", "", txtDocNo.Text == "" ? "" : "", "", "", ddlPlant.SelectedValue, Convert.ToInt32(rblsalesFrom.SelectedValue));


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

                            Session["SONO"] = null;
                            Session["PLANT"] = null;
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

        public void BindDataGrid()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetSOAge(objMainClass.intCmpId, "01-04-2022", "", txtDocNo.Text == "" ? "" : txtDocNo.Text, "", "", ddlPlant.SelectedValue, Convert.ToInt32(rblsalesFrom.SelectedValue));
                    //dt = objMainClass.GetSOAge(objMainClass.intCmpId, txtFromDocDate.Text, txtToDocDate.Text, txtDocNo.Text == "" ? "" : txtDocNo.Text, "", "", ddlPlant.SelectedValue);
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

        protected void lnkSerch_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    //DataTable dt = new DataTable();
                    //dt = objMainClass.GetSOAge(objMainClass.intCmpId, "01-04-2022", "", txtDocNo.Text == "" ? "" : txtDocNo.Text, "", "", ddlPlant.SelectedValue, Convert.ToInt32(rblsalesFrom.SelectedValue));
                    ////dt = objMainClass.GetSOAge(objMainClass.intCmpId, txtFromDocDate.Text, txtToDocDate.Text, txtDocNo.Text == "" ? "" : txtDocNo.Text, "", "", ddlPlant.SelectedValue);
                    //if (dt.Rows.Count > 0)
                    //{
                    //    gvList.DataSource = dt;
                    //    gvList.DataBind();
                    //}
                    //else
                    //{
                    //    gvList.DataSource = string.Empty;
                    //    gvList.DataBind();
                    //}

                    BindDataGrid();

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
                    string attachment = "attachment; filename=PendingSO.xls";
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

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        Label lblITEMCODE = (Label)e.Row.FindControl("lblITEMCODE");
                        Label lblSONO = (Label)e.Row.FindControl("lblSONO");
                        HiddenField hdCount = (HiddenField)e.Row.FindControl("hdCount");
                        GridView gvInnerList = e.Row.FindControl("gvInnerList") as GridView;

                        TextBox txtbid = e.Row.FindControl("txtbid") as TextBox;
                        Button btnBid = e.Row.FindControl("btnBid") as Button;
                        Label lblBid = (Label)e.Row.FindControl("lblBid");

                        if (txtbid != null && btnBid != null && lblBid != null)
                        {
                            if (hdCount != null && Convert.ToInt32(hdCount.Value == null ? "0" : hdCount.Value) > 0)
                            {
                                txtbid.Visible = true;
                                btnBid.Visible = true;
                                lblBid.Visible = false;
                            }
                            else
                            {
                                txtbid.Visible = false;
                                btnBid.Visible = false;
                                lblBid.Visible = true;
                            }
                        }

                        DataTable dt = new DataTable();
                        dt = objMainClass.GetVendList(objMainClass.intCmpId, 11229, lblITEMCODE.Text, "GETVENDLIST", lblSONO.Text);

                        if (dt.Rows.Count > 0)
                        {
                            gvInnerList.DataSource = dt;
                            gvInnerList.DataBind();
                        }
                        else
                        {
                            gvInnerList.DataSource = string.Empty;
                            gvInnerList.DataBind();
                            e.Row.BackColor = System.Drawing.Color.IndianRed;
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

        protected void gvParentGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        //Label lblITEMCODE = (Label)e.Row.FindControl("lblITEMCODE");
                        string lblITEMCODE = Convert.ToString(e.Row.Cells[1].Text);
                        Label lblSONO = (Label)e.Row.FindControl("lblSONO");
                        GridView gv = (GridView)e.Row.FindControl("gvChildGrid");
                        DataTable dt = new DataTable();
                        dt = objMainClass.GetVendList(objMainClass.intCmpId, 11229, lblITEMCODE, "GETVENDLIST", lblSONO.Text);

                        if (dt.Rows.Count > 0)
                        {
                            gv.DataSource = dt;
                            gv.DataBind();
                        }
                        else
                        {
                            gv.DataSource = string.Empty;
                            gv.DataBind();
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

        protected void lnkSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    foreach (GridViewRow row in gvList.Rows)
                    {
                        GridView gvInnerList = row.FindControl("gvInnerList") as GridView;
                        foreach (GridViewRow row1 in gvInnerList.Rows)
                        {
                            CheckBox chkItem = row1.FindControl("chkItem") as CheckBox;
                            if (chkItem.Checked == true)
                            {
                                //Label lblMobile = row1.FindControl("lblMobile") as Label;

                                string mobileNo = "";
                                Label lblBIKERCONTACT = row1.FindControl("lblBIKERCONTACT") as Label;
                                Label lblASMCONTACT = row1.FindControl("lblASMCONTACT") as Label;
                                Label lblPM = row1.FindControl("lblPM") as Label;

                                Label lblFinalID = row1.FindControl("lblID") as Label;

                                Label lblBSM = row1.FindControl("lblBSM") as Label;
                                Label lblZSM = row1.FindControl("lblZSM") as Label;
                                Label lblRSM = row1.FindControl("lblRSM") as Label;
                                Label lblNSM = row1.FindControl("lblNSM") as Label;

                                //Label lblFRANCHISECONTNO = row1.FindControl("lblFRANCHISECONTNO") as Label;
                                //Label lblNEWASMCONTACT = row1.FindControl("lblNEWASMCONTACT") as Label;

                                Label lblPLANTCD = row1.FindControl("lblPLANTCD") as Label;
                                if (lblBIKERCONTACT.Text != null && lblBIKERCONTACT.Text != "" && lblBIKERCONTACT.Text != string.Empty)
                                {
                                    var dtDealereligibleforNotification = objMainClass.GetDealerEligibleForNotification(Convert.ToInt32(lblFinalID.Text));
                                    if (dtDealereligibleforNotification.Rows.Count > 0)
                                    {
                                        if (Convert.ToInt32(dtDealereligibleforNotification.Rows[0]["ISPARTNERDEALER"]) == 1)
                                        {
                                            mobileNo = "91" + lblBIKERCONTACT.Text;
                                        }
                                    }
                                }
                                if (lblASMCONTACT.Text != null && lblASMCONTACT.Text != "" && lblASMCONTACT.Text != string.Empty)
                                {
                                    if (mobileNo == string.Empty)
                                    {
                                        mobileNo = "91" + lblASMCONTACT.Text;
                                    }
                                    else
                                    {
                                        mobileNo = mobileNo + ",91" + lblASMCONTACT.Text;
                                    }
                                }
                                if (lblPM.Text != null && lblPM.Text != "" && lblPM.Text != string.Empty)
                                {
                                    if (mobileNo == string.Empty)
                                    {
                                        mobileNo = "91" + lblPM.Text;
                                    }
                                    else
                                    {
                                        mobileNo = mobileNo + ",91" + lblPM.Text;
                                    }
                                }

                                if (lblBSM.Text != null && lblBSM.Text != "" && lblBSM.Text != string.Empty)
                                {
                                    if (mobileNo == string.Empty)
                                    {
                                        mobileNo = "91" + lblBSM.Text;
                                    }
                                    else
                                    {
                                        mobileNo = mobileNo + ",91" + lblBSM.Text;
                                    }
                                }

                                if (lblZSM.Text != null && lblZSM.Text != "" && lblZSM.Text != string.Empty)
                                {
                                    if (mobileNo == string.Empty)
                                    {
                                        mobileNo = "91" + lblZSM.Text;
                                    }
                                    else
                                    {
                                        mobileNo = mobileNo + ",91" + lblZSM.Text;
                                    }
                                }

                                if (lblRSM.Text != null && lblRSM.Text != "" && lblRSM.Text != string.Empty)
                                {
                                    if (mobileNo == string.Empty)
                                    {
                                        mobileNo = "91" + lblRSM.Text;
                                    }
                                    else
                                    {
                                        mobileNo = mobileNo + ",91" + lblRSM.Text;
                                    }
                                }

                                if (lblNSM.Text != null && lblNSM.Text != "" && lblNSM.Text != string.Empty)
                                {
                                    if (mobileNo == string.Empty)
                                    {
                                        mobileNo = "91" + lblNSM.Text;
                                    }
                                    else
                                    {
                                        mobileNo = mobileNo + ",91" + lblNSM.Text;
                                    }
                                }

                                //if (lblFRANCHISECONTNO.Text != null && lblFRANCHISECONTNO.Text != "" && lblFRANCHISECONTNO.Text != string.Empty)
                                //{
                                //    if (mobileNo == string.Empty)
                                //    {
                                //        mobileNo = "91" + lblFRANCHISECONTNO.Text;
                                //    }
                                //    else
                                //    {
                                //        mobileNo = mobileNo + ",91" + lblFRANCHISECONTNO.Text;
                                //    }
                                //}

                                //if (lblNEWASMCONTACT.Text != null && lblNEWASMCONTACT.Text != "" && lblNEWASMCONTACT.Text != string.Empty)
                                //{
                                //    if (mobileNo == string.Empty)
                                //    {
                                //        mobileNo = "91" + lblNEWASMCONTACT.Text;
                                //    }
                                //    else
                                //    {
                                //        mobileNo = mobileNo + ",91" + lblNEWASMCONTACT.Text;
                                //    }
                                //}

                                Label lblITEMDESC = row.FindControl("lblITEMDESC") as Label;
                                Label lblSONO = row.FindControl("lblSONO") as Label;
                                Label lblREFNO = row.FindControl("lblREFNO") as Label;
                                Label lblDELIDT = row.FindControl("lblDELIDT") as Label;
                                Label lblENTITYID = row.FindControl("lblENTITYID") as Label;


                                Label lblVENDORNAME = row1.FindControl("lblVENDORNAME") as Label;
                                Label lblVENDORPRICE = row1.FindControl("lblVENDORPRICE") as Label;
                                Label lblID = row1.FindControl("lblID") as Label;
                                Label lblSTATUS = row1.FindControl("lblSTATUS") as Label;

                                Label lblPRODMAKE = row1.FindControl("lblPRODMAKE") as Label;
                                Label lblMODELDESC = row1.FindControl("lblMODELDESC") as Label;
                                Label lblRAMSIZE = row1.FindControl("lblRAMSIZE") as Label;
                                Label lblROMSIZE = row1.FindControl("lblROMSIZE") as Label;
                                Label lblPRODCOLOR = row1.FindControl("lblPRODCOLOR") as Label;

                                if (lblSTATUS.Text == "53" || lblSTATUS.Text == "20" || lblSTATUS.Text == "33" || lblSTATUS.Text == "35")
                                {
                                    //SendTextMessage(mobileNo, "Hi, " + System.Environment.NewLine + "Please ready this device to dispatch. Device - " + lblITEMDESC.Text + ". Job id - " + lblVENDORNAME.Text);
                                    //string Response = objWAClass.SendTextMessage("Hi, " + System.Environment.NewLine + "Please ready this device to dispatch. Device - " + lblITEMDESC.Text + ". Job id - " + lblVENDORNAME.Text + ". SO No. - " + lblSONO.Text + " Order No - " + lblREFNO.Text + " Delivery Date - " + Convert.ToDateTime(lblDELIDT.Text).ToShortDateString(), mobileNo, Convert.ToString(Session["USERID"]));
                                    //if (Response != "" && Response != string.Empty && Response != null)
                                    //{
                                    //    string[] response1 = Response.Split(",".ToCharArray());
                                    //    if (Convert.ToInt32(response1[0]) == 1)
                                    //    {
                                    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"" + Convert.ToString(response1[1]) + "\");", true);
                                    //    }
                                    //    else
                                    //    {
                                    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + Convert.ToString(response1[1]) + "\");", true);
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Mesage not sent.!');", true);
                                    //}
                                    //string MSGTEXT = "Hi, " + System.Environment.NewLine + "Please ready this device to dispatch. Device - " + lblITEMDESC.Text + ". Job id - " + lblVENDORNAME.Text + ". SO No. - " + lblSONO.Text + " Order No - " + lblREFNO.Text + " Delivery Date - " + Convert.ToDateTime(lblDELIDT.Text).ToShortDateString();
                                    //objMainClass.SaveWAMSGNotification(objMainClass.intCmpId, mobileNo, MSGTEXT, "SO", lblSONO.Text, Convert.ToInt32(Session["USERID"]), "INSERTMSG");
                                    DataTable dtMsgString = new DataTable();
                                    dtMsgString = objMainClass.GetMessageText(objMainClass.intCmpId, objMainClass.intTrigeerId, 199, 0, "1015", "50");
                                    string strMessage = Convert.ToString(dtMsgString.Rows[0]["msgtxt"]);

                                    strMessage = strMessage.Replace("@@DEVICE", (lblPRODMAKE.Text + " " + lblMODELDESC.Text + " " + lblRAMSIZE.Text + " " + lblROMSIZE.Text + " " + lblPRODCOLOR.Text)).Replace("@@JOBID", lblVENDORNAME.Text).Replace("@@SONO", lblSONO.Text).Replace("@@ORDERNO", lblREFNO.Text).Replace("@@DATE", Convert.ToDateTime(lblDELIDT.Text).ToShortDateString());

                                    objMainClass.SaveNotification(objMainClass.intCmpId, "", "", mobileNo, "", "", strMessage, "", "SO", lblSONO.Text, "SMS", "", Convert.ToInt32(Session["USERID"]), "INSERTNOTIFICATION");
                                    objMainClass.SaveNotification(objMainClass.intCmpId, "", "", mobileNo, "", "", strMessage, "", "SO", lblSONO.Text, "WA", "", Convert.ToInt32(Session["USERID"]), "INSERTNOTIFICATION");
                                    //objMainClass.SaveNotification(objMainClass.intCmpId, objMainClass.EmailID, objMainClass.Password, "mohit.diwakar@qarmatek.com", "mohit.diwakar@qarmatek.com", "TEST", strMessage, objMainClass.PORT, "SO", lblSONO.Text, "MAIL", Convert.ToInt32(Session["USERID"]), "INSERTNOTIFICATION");
                                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Record Updated.!');", true);
                                    int i = objMainClass.UpdateJWREFNo(objMainClass.intCmpId, lblID.Text, lblREFNO.Text, "UPDATEJWREF");
                                    if (i == 1)
                                    {
                                        if (lblENTITYID.Text != "" && lblENTITYID.Text != string.Empty && lblENTITYID.Text != null)
                                        {
                                            int iWSResult = objMainClass.ChangeOrderStatus(objMainClass.intCmpId, lblENTITYID.Text, lblSONO.Text, "", lblPLANTCD.Text, "WEBSITESTATUSAPI", "MOBEXAPI", "Order is being processed.", "", "", "", Convert.ToInt32(Session["USERID"]));
                                        }
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Record Updated.!');", true);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Not Reserved.! Something went wrong.!');", true);
                                    }
                                }
                                else
                                {
                                    //SendTextMessage(mobileNo, "Hi, " + System.Environment.NewLine + "Please Pickup this device - " + lblITEMDESC.Text + " from Vendor - " + lblVENDORNAME.Text + " Price @ " + lblVENDORPRICE.Text);
                                    //string Response = objWAClass.SendTextMessage("Hi, " + System.Environment.NewLine + "Please Pickup this device - " + lblITEMDESC.Text + " from Vendor - " + lblVENDORNAME.Text + " Price @ " + lblVENDORPRICE.Text + ". Delivery Date - " + Convert.ToDateTime(lblDELIDT.Text).ToShortDateString(), mobileNo, Convert.ToString(Session["USERID"]));
                                    //if (Response != "" && Response != string.Empty && Response != null)
                                    //{
                                    //    string[] response1 = Response.Split(",".ToCharArray());
                                    //    if (Convert.ToInt32(response1[0]) == 1)
                                    //    {
                                    //        int i = objMainClass.UpdateEachListingReservedDetail(Convert.ToInt32(lblID.Text), Convert.ToString(Session["USERID"]));
                                    //        if (i == -1)
                                    //        {
                                    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"" + Convert.ToString(response1[1]) + "\");", true);
                                    //        }
                                    //        else
                                    //        {
                                    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Not Reserved.! Something went wrong.!');", true);
                                    //        }
                                    //    }
                                    //    else
                                    //    {
                                    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + Convert.ToString(response1[1]) + "\");", true);
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Mesage not sent.!');", true);
                                    //}

                                    //string MSGTEXT = "Hi, " + System.Environment.NewLine + "Please Pickup this device - " + lblITEMDESC.Text + " from Vendor - " + lblVENDORNAME.Text + " Price @ " + lblVENDORPRICE.Text + ". Delivery Date - " + Convert.ToDateTime(lblDELIDT.Text).ToShortDateString();
                                    //objMainClass.SaveWAMSGNotification(objMainClass.intCmpId, mobileNo, MSGTEXT, "SO", lblSONO.Text, Convert.ToInt32(Session["USERID"]), "INSERTMSG");

                                    DataTable dtMsgString = new DataTable();
                                    dtMsgString = objMainClass.GetMessageText(objMainClass.intCmpId, objMainClass.intTrigeerId, 198, 0, "1015", "50"); //Commented for franchisee module, msg should be sent to franchisee only.  on 07.02.2024
                                    //dtMsgString = objMainClass.GetMessageText(objMainClass.intCmpId, objMainClass.intTrigeerId, 200, 0, "1015", "50");
                                    string strMessage = Convert.ToString(dtMsgString.Rows[0]["msgtxt"]);
                                    string price = decimal.Truncate(Convert.ToDecimal(lblVENDORPRICE.Text)).ToString();
                                    //strMessage = strMessage.Replace("@@DEVICE", lblITEMDESC.Text).Replace("@@VEND", lblVENDORNAME.Text).Replace("@@PRICE", price).Replace("@@DATE", Convert.ToDateTime(lblDELIDT.Text).ToShortDateString());

                                    strMessage = strMessage.Replace("@@MAKE", lblPRODMAKE.Text).Replace("@@MODEL", lblMODELDESC.Text).Replace("@@RAM", lblRAMSIZE.Text).Replace("@@ROM", lblROMSIZE.Text).Replace("@@COLOR", lblPRODCOLOR.Text).Replace("@@VENDOR", lblVENDORNAME.Text).Replace("@@PRICE", price).Replace("@@DATE", Convert.ToDateTime(lblDELIDT.Text).ToShortDateString());
                                    //strMessage = strMessage.Replace("@@MAKE", lblPRODMAKE.Text).Replace("@@MODEL", lblMODELDESC.Text).Replace("@@RAM", lblRAMSIZE.Text).Replace("@@ROM", lblROMSIZE.Text).Replace("@@COLOR", lblPRODCOLOR.Text).Replace("@@VENDOR", lblVENDORNAME.Text).Replace("@@DATE", Convert.ToDateTime(lblDELIDT.Text).ToShortDateString()); //Commented for franchisee module, msg should be sent to franchisee only.  on 07.02.2024

                                    objMainClass.SaveNotification(objMainClass.intCmpId, "", "", mobileNo, "", "", strMessage, "", "SO", lblSONO.Text, "SMS", "", Convert.ToInt32(Session["USERID"]), "INSERTNOTIFICATION");
                                    objMainClass.SaveNotification(objMainClass.intCmpId, "", "", mobileNo, "", "", strMessage, "", "SO", lblSONO.Text, "WA", "", Convert.ToInt32(Session["USERID"]), "INSERTNOTIFICATION");
                                    //objMainClass.SaveNotification(objMainClass.intCmpId, objMainClass.EmailID, objMainClass.Password, "mohit.diwakar@qarmatek.com", "mohit.diwakar@qarmatek.com", "TEST", strMessage, objMainClass.PORT, "SO", lblSONO.Text, "MAIL", Convert.ToInt32(Session["USERID"]), "INSERTNOTIFICATION");
                                    int i = objMainClass.UpdateEachListingReservedDetail(Convert.ToInt32(lblID.Text), Convert.ToString(Session["USERID"]));
                                    if (i == -1)
                                    {
                                        if (lblENTITYID.Text != "" && lblENTITYID.Text != string.Empty && lblENTITYID.Text != null)
                                        {
                                            int iWSResult = objMainClass.ChangeOrderStatus(objMainClass.intCmpId, lblENTITYID.Text, lblSONO.Text, "", lblPLANTCD.Text, "WEBSITESTATUSAPI", "MOBEXAPI", "Order is being processed.", "", "", "", Convert.ToInt32(Session["USERID"]));
                                        }
                                        objMainClass.UpdateSOORDER(lblREFNO.Text, lblSONO.Text, Convert.ToInt32(lblID.Text), "UPDATEORDERDET");
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Record Updated.!');", true);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Not Reserved.! Something went wrong.!');", true);
                                    }

                                }
                            }
                        }
                        // Response.Redirect("frmSOItemAssign.aspx", true);
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




        //private void SendTextMessage(string MOBILENO, string MESSAGE)
        //{
        //    try
        //    {
        //        //var client = new RestClient("https://console.wa0.in/api/send.php?client_id=3a13f8abba76457a7bd9e6378b42ec15&instance=65cd6e0db8fe830fc320bdd5572b9bdf&number=918460591264&message=MESSAGE_HERE&type=text");

        //        var client = new RestClient("https://console.wa0.in/api/send.php?client_id=" + objWAClass.client_id + "&instance=" + objWAClass.instance + "&number=" + MOBILENO + "&message=" + MESSAGE + "&type=text");
        //        client.Timeout = -1;
        //        var request = new RestRequest(Method.POST);
        //        IRestResponse response = client.Execute(request);
        //        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            string jsonsend = response.Content;
        //            jsonsend = "[" + jsonsend + "]";
        //            DataTable dtValuesend = (DataTable)JsonConvert.DeserializeObject(jsonsend.Replace("}{", "},{"), (typeof(DataTable)));
        //            if (Convert.ToString(dtValuesend.Rows[0]["status"]) == objWAClass.statusTrue || Convert.ToString(dtValuesend.Rows[0]["status"]) == objWAClass.msgQue)
        //            {
        //                objMainClass.WALOG(objMainClass.intCmpId, MESSAGE, MOBILENO, Convert.ToString(Session["USERID"]), "");
        //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Mesage sent successfully.');", true);

        //            }
        //            else
        //            {
        //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"" + Convert.ToString(dtValuesend.Rows[0]["message"]) + "\");", true);
        //            }

        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + response.ErrorMessage + "\");", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
        //    }
        //}

        protected void lnkSendSingle_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    Label lblITEMDESC = grdrow.FindControl("lblITEMDESC") as Label;
                    GridView gvInnerList = grdrow.FindControl("gvInnerList") as GridView;

                    GridViewRow optimisedParentRow = gvInnerList.NamingContainer as GridViewRow;


                    //GridView childGrid = grdrow.NamingContainer as GridView;
                    //GridViewRow parentRow = childGrid.NamingContainer as GridViewRow;
                    //GridViewRow optimisedParentRow = ((GridViewRow)((LinkButton)sender).NamingContainer.NamingContainer.NamingContainer as GridViewRow);

                    //GridViewRow gvMasterRow = (GridViewRow)gvInnerList.Parent.Parent.Parent;
                    string lblSONO = ((Label)optimisedParentRow.FindControl("lblSONO")).Text;
                    string lblREFNO = ((Label)optimisedParentRow.FindControl("lblREFNO")).Text;
                    string lblDELIDT = ((Label)optimisedParentRow.FindControl("lblDELIDT")).Text;
                    string lblENTITYID = ((Label)optimisedParentRow.FindControl("lblENTITYID")).Text;


                    foreach (GridViewRow row1 in gvInnerList.Rows)
                    {
                        CheckBox chkItem = row1.FindControl("chkItem") as CheckBox;
                        if (chkItem.Checked == true)
                        {
                            //Label lblMobile = row1.FindControl("lblMobile") as Label;
                            string mobileNo = "";
                            Label lblBIKERCONTACT = row1.FindControl("lblBIKERCONTACT") as Label;
                            Label lblASMCONTACT = row1.FindControl("lblASMCONTACT") as Label;
                            Label lblPM = row1.FindControl("lblPM") as Label;

                            Label lblBSM = row1.FindControl("lblBSM") as Label;
                            Label lblZSM = row1.FindControl("lblZSM") as Label;
                            Label lblRSM = row1.FindControl("lblRSM") as Label;
                            Label lblNSM = row1.FindControl("lblNSM") as Label;

                            //Label lblFRANCHISECONTNO = row1.FindControl("lblFRANCHISECONTNO") as Label;
                            //Label lblNEWASMCONTACT = row1.FindControl("lblNEWASMCONTACT") as Label;

                            Label lblPLANTCD = row1.FindControl("lblPLANTCD") as Label;
                            Label lblFinalID = row1.FindControl("lblID") as Label;

                            if (lblBIKERCONTACT.Text != null && lblBIKERCONTACT.Text != "" && lblBIKERCONTACT.Text != string.Empty)
                            {
                                var dtDealereligibleforNotification = objMainClass.GetDealerEligibleForNotification(Convert.ToInt32(lblFinalID.Text));
                                if (dtDealereligibleforNotification.Rows.Count > 0)
                                {
                                    if (Convert.ToInt32(dtDealereligibleforNotification.Rows[0]["ISPARTNERDEALER"]) == 1)
                                    {
                                        mobileNo = "91" + lblBIKERCONTACT.Text;
                                    }
                                }
                            }

                            if (lblASMCONTACT.Text != null && lblASMCONTACT.Text != "" && lblASMCONTACT.Text != string.Empty)
                            {
                                if (mobileNo == string.Empty)
                                {
                                    mobileNo = "91" + lblASMCONTACT.Text;
                                }
                                else
                                {
                                    mobileNo = mobileNo + ",91" + lblASMCONTACT.Text;
                                }
                            }
                            if (lblPM.Text != null && lblPM.Text != "" && lblPM.Text != string.Empty)
                            {
                                if (mobileNo == string.Empty)
                                {
                                    mobileNo = "91" + lblPM.Text;
                                }
                                else
                                {
                                    mobileNo = mobileNo + ",91" + lblPM.Text;
                                }
                            }

                            if (lblBSM.Text != null && lblBSM.Text != "" && lblBSM.Text != string.Empty)
                            {
                                if (mobileNo == string.Empty)
                                {
                                    mobileNo = "91" + lblBSM.Text;
                                }
                                else
                                {
                                    mobileNo = mobileNo + ",91" + lblBSM.Text;
                                }
                            }

                            if (lblZSM.Text != null && lblZSM.Text != "" && lblZSM.Text != string.Empty)
                            {
                                if (mobileNo == string.Empty)
                                {
                                    mobileNo = "91" + lblZSM.Text;
                                }
                                else
                                {
                                    mobileNo = mobileNo + ",91" + lblZSM.Text;
                                }
                            }

                            if (lblRSM.Text != null && lblRSM.Text != "" && lblRSM.Text != string.Empty)
                            {
                                if (mobileNo == string.Empty)
                                {
                                    mobileNo = "91" + lblRSM.Text;
                                }
                                else
                                {
                                    mobileNo = mobileNo + ",91" + lblRSM.Text;
                                }
                            }

                            if (lblNSM.Text != null && lblNSM.Text != "" && lblNSM.Text != string.Empty)
                            {
                                if (mobileNo == string.Empty)
                                {
                                    mobileNo = "91" + lblNSM.Text;
                                }
                                else
                                {
                                    mobileNo = mobileNo + ",91" + lblNSM.Text;
                                }
                            }

                            //if (lblFRANCHISECONTNO.Text != null && lblFRANCHISECONTNO.Text != "" && lblFRANCHISECONTNO.Text != string.Empty)
                            //{
                            //    if (mobileNo == string.Empty)
                            //    {
                            //        mobileNo = "91" + lblFRANCHISECONTNO.Text;
                            //    }
                            //    else
                            //    {
                            //        mobileNo = mobileNo + ",91" + lblFRANCHISECONTNO.Text;
                            //    }
                            //}

                            //if (lblNEWASMCONTACT.Text != null && lblNEWASMCONTACT.Text != "" && lblNEWASMCONTACT.Text != string.Empty)
                            //{
                            //    if (mobileNo == string.Empty)
                            //    {
                            //        mobileNo = "91" + lblNEWASMCONTACT.Text;
                            //    }
                            //    else
                            //    {
                            //        mobileNo = mobileNo + ",91" + lblNEWASMCONTACT.Text;
                            //    }
                            //}

                            Label lblVENDORNAME = row1.FindControl("lblVENDORNAME") as Label;
                            Label lblVENDORPRICE = row1.FindControl("lblVENDORPRICE") as Label;
                            Label lblID = row1.FindControl("lblID") as Label;

                            Label lblSTATUS = row1.FindControl("lblSTATUS") as Label;


                            Label lblPRODMAKE = row1.FindControl("lblPRODMAKE") as Label;
                            Label lblMODELDESC = row1.FindControl("lblMODELDESC") as Label;
                            Label lblRAMSIZE = row1.FindControl("lblRAMSIZE") as Label;
                            Label lblROMSIZE = row1.FindControl("lblROMSIZE") as Label;
                            Label lblPRODCOLOR = row1.FindControl("lblPRODCOLOR") as Label;


                            if (lblSTATUS.Text == "53" || lblSTATUS.Text == "20" || lblSTATUS.Text == "33" || lblSTATUS.Text == "35")
                            {
                                //SendTextMessage(mobileNo, "Hi, " + System.Environment.NewLine + "Please ready this device to dispatch. Device - " + lblITEMDESC.Text + ". Job id - " + lblVENDORNAME.Text);
                                //string Response2 = objWAClass.SendTextMessage("Hi, " + System.Environment.NewLine + "Please ready this device to dispatch. Device - " + lblITEMDESC.Text + ". Job id - " + lblVENDORNAME.Text + ". SO No. - " + lblSONO + " Order No. - " + lblREFNO + " Delivery Date - " + Convert.ToDateTime(lblDELIDT).ToShortDateString(), mobileNo, Convert.ToString(Session["USERID"]));
                                //if (Response2 != "" && Response2 != string.Empty && Response2 != null)
                                //{
                                //    string[] response1 = Response2.Split(",".ToCharArray());
                                //    if (Convert.ToInt32(response1[0]) == 1)
                                //    {
                                //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"" + Convert.ToString(response1[1]) + "\");", true);
                                //    }
                                //    else
                                //    {
                                //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + Convert.ToString(response1[1]) + "\");", true);
                                //    }
                                //}
                                //else
                                //{
                                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Mesage not sent.!');", true);
                                //}

                                //string MSGTEXT = "Hi, " + System.Environment.NewLine + "Please ready this device to dispatch. Device - " + lblITEMDESC.Text + ". Job id - " + lblVENDORNAME.Text + ". SO No. - " + lblSONO + " Order No - " + lblREFNO + " Delivery Date - " + Convert.ToDateTime(lblDELIDT).ToShortDateString();
                                //objMainClass.SaveWAMSGNotification(objMainClass.intCmpId, mobileNo, MSGTEXT, "SO", lblSONO, Convert.ToInt32(Session["USERID"]), "INSERTMSG");

                                DataTable dtMsgString = new DataTable();
                                dtMsgString = objMainClass.GetMessageText(objMainClass.intCmpId, objMainClass.intTrigeerId, 199, 0, "1015", "50");
                                string strMessage = Convert.ToString(dtMsgString.Rows[0]["msgtxt"]);

                                strMessage = strMessage.Replace("@@DEVICE", (lblPRODMAKE.Text + " " + lblMODELDESC.Text + " " + lblRAMSIZE.Text + " " + lblROMSIZE.Text + " " + lblPRODCOLOR.Text)).Replace("@@JOBID", lblVENDORNAME.Text).Replace("@@SONO", lblSONO).Replace("@@ORDERNO", lblREFNO).Replace("@@DATE", Convert.ToDateTime(lblDELIDT).ToShortDateString());

                                objMainClass.SaveNotification(objMainClass.intCmpId, "", "", mobileNo, "", "", strMessage, "", "SO", lblSONO, "SMS", "", Convert.ToInt32(Session["USERID"]), "INSERTNOTIFICATION");
                                objMainClass.SaveNotification(objMainClass.intCmpId, "", "", mobileNo, "", "", strMessage, "", "SO", lblSONO, "WA", "", Convert.ToInt32(Session["USERID"]), "INSERTNOTIFICATION");
                                //objMainClass.SaveNotification(objMainClass.intCmpId, objMainClass.EmailID, objMainClass.Password, "mohit.diwakar@qarmatek.com", "mohit.diwakar@qarmatek.com", "TEST", strMessage, objMainClass.PORT, "SO", lblSONO, "MAIL", Convert.ToInt32(Session["USERID"]), "INSERTNOTIFICATION");
                                int i = objMainClass.UpdateJWREFNo(objMainClass.intCmpId, lblID.Text, lblREFNO, "UPDATEJWREF");
                                if (i == 1)
                                {
                                    if (lblENTITYID != "" && lblENTITYID != string.Empty && lblENTITYID != null)
                                    {
                                        int iWSResult = objMainClass.ChangeOrderStatus(objMainClass.intCmpId, lblENTITYID, lblSONO, "", lblPLANTCD.Text, "WEBSITESTATUSAPI", "MOBEXAPI", "Order is being processed.", "", "", "", Convert.ToInt32(Session["USERID"]));
                                    }

                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Record Updated.!');", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Not Reserved.! Something went wrong.!');", true);
                                }
                                //Response.Redirect("frmSOItemAssign.aspx", true);
                            }
                            else
                            {

                                //SendTextMessage(mobileNo, "Hi, " + System.Environment.NewLine + "Please Pickup this device - " + lblITEMDESC.Text + " from Vendor - " + lblVENDORNAME.Text + " Price @ " + lblVENDORPRICE.Text);
                                //string Response = objWAClass.SendTextMessage("Hi, " + System.Environment.NewLine + "Please Pickup this device - " + lblITEMDESC.Text + " from Vendor - " + lblVENDORNAME.Text + " Price @ " + lblVENDORPRICE.Text + " Delivery Date - " + Convert.ToDateTime(lblDELIDT).ToShortDateString(), mobileNo, Convert.ToString(Session["USERID"]));
                                //if (Response != "" && Response != string.Empty && Response != null)
                                //{
                                //    string[] response1 = Response.Split(",".ToCharArray());
                                //    if (Convert.ToInt32(response1[0]) == 1)
                                //    {
                                //        int i = objMainClass.UpdateEachListingReservedDetail(Convert.ToInt32(lblID.Text), Convert.ToString(Session["USERID"]));
                                //        if (i == -1)
                                //        {
                                //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"" + Convert.ToString(response1[1]) + "\");", true);
                                //        }
                                //        else
                                //        {
                                //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Not Reserved.! Something went wrong.!');", true);
                                //        }
                                //    }
                                //    else
                                //    {
                                //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + Convert.ToString(response1[1]) + "\");", true);
                                //    }
                                //}
                                //else
                                //{
                                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Mesage not sent.!');", true);
                                //}

                                //string MSGTEXT = "Hi, " + System.Environment.NewLine + "Please Pickup this device - " + lblITEMDESC.Text + " from Vendor - " + lblVENDORNAME.Text + " Price @ " + lblVENDORPRICE.Text + ". Delivery Date - " + Convert.ToDateTime(lblDELIDT).ToShortDateString();
                                //objMainClass.SaveWAMSGNotification(objMainClass.intCmpId, mobileNo, MSGTEXT, "SO", lblSONO, Convert.ToInt32(Session["USERID"]), "INSERTMSG");

                                DataTable dtMsgString = new DataTable();
                                dtMsgString = objMainClass.GetMessageText(objMainClass.intCmpId, objMainClass.intTrigeerId, 198, 0, "1015", "50"); //Commented for franchisee module, msg should be sent to franchisee only.  on 07.02.2024
                                //dtMsgString = objMainClass.GetMessageText(objMainClass.intCmpId, objMainClass.intTrigeerId, 200, 0, "1015", "50");
                                string strMessage = Convert.ToString(dtMsgString.Rows[0]["msgtxt"]);
                                string price = decimal.Truncate(Convert.ToDecimal(lblVENDORPRICE.Text)).ToString();
                                strMessage = strMessage.Replace("@@MAKE", lblPRODMAKE.Text).Replace("@@MODEL", lblMODELDESC.Text).Replace("@@RAM", lblRAMSIZE.Text).Replace("@@ROM", lblROMSIZE.Text).Replace("@@COLOR", lblPRODCOLOR.Text).Replace("@@VENDOR", lblVENDORNAME.Text).Replace("@@PRICE", price).Replace("@@DATE", Convert.ToDateTime(lblDELIDT).ToShortDateString()); //Commented for franchisee module, msg should be sent to franchisee only. on 07.02.2024
                                //strMessage = strMessage.Replace("@@MAKE", lblPRODMAKE.Text).Replace("@@MODEL", lblMODELDESC.Text).Replace("@@RAM", lblRAMSIZE.Text).Replace("@@ROM", lblROMSIZE.Text).Replace("@@COLOR", lblPRODCOLOR.Text).Replace("@@VENDOR", lblVENDORNAME.Text).Replace("@@DATE", Convert.ToDateTime(lblDELIDT).ToShortDateString());

                                objMainClass.SaveNotification(objMainClass.intCmpId, "", "", mobileNo, "", "", strMessage, "", "SO", lblSONO, "SMS", "", Convert.ToInt32(Session["USERID"]), "INSERTNOTIFICATION");
                                objMainClass.SaveNotification(objMainClass.intCmpId, "", "", mobileNo, "", "", strMessage, "", "SO", lblSONO, "WA", "", Convert.ToInt32(Session["USERID"]), "INSERTNOTIFICATION");
                                //objMainClass.SaveNotification(objMainClass.intCmpId, objMainClass.EmailID, objMainClass.Password, "mohit.diwakar@qarmatek.com", "mohit.diwakar@qarmatek.com", "TEST", strMessage, objMainClass.PORT, "SO", lblSONO, "MAIL", Convert.ToInt32(Session["USERID"]), "INSERTNOTIFICATION");
                                int i = objMainClass.UpdateEachListingReservedDetail(Convert.ToInt32(lblID.Text), Convert.ToString(Session["USERID"]));
                                if (i == -1)
                                {
                                    if (lblENTITYID != "" && lblENTITYID != string.Empty && lblENTITYID != null)
                                    {
                                        int iWSResult = objMainClass.ChangeOrderStatus(objMainClass.intCmpId, lblENTITYID, lblSONO, "", lblPLANTCD.Text, "WEBSITESTATUSAPI", "MOBEXAPI", "Order is being processed.", "", "", "", Convert.ToInt32(Session["USERID"]));
                                    }

                                    objMainClass.UpdateSOORDER(lblREFNO, lblSONO, Convert.ToInt32(lblID.Text), "UPDATEORDERDET");
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Record Updated.!');", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Not Reserved.! Something went wrong.!');", true);
                                }

                                //Response.Redirect("frmSOItemAssign.aspx", true);
                            }



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

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    foreach (GridViewRow row in gvList.Rows)
                    {
                        GridView gvInnerList = row.FindControl("gvInnerList") as GridView;
                        if (gvInnerList.Rows.Count > 0)
                        {
                            GridViewRow grv = gvInnerList.Rows[0];
                            CheckBox chkItem = (CheckBox)grv.FindControl("chkItem");
                            chkItem.Checked = true;
                        }
                        foreach (GridViewRow row1 in gvInnerList.Rows)
                        {

                        }
                        // Response.Redirect("frmSOItemAssign.aspx", true);
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


        protected void btnUnlist_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                int ID = Convert.ToInt32(((Label)grdrow.FindControl("lblID")).Text);
                objMainClass.UpdateUnListedDetail(ID, Session["USERID"].ToString());
                var mailmessagefor = "Hi There,<br><br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ID is : " + ID.ToString() + " Unlisted From " + ((Label)grdrow.FindControl("lblVendorName")).Text + " Vendor and Unlisted By " + Session["USERNAME"].ToString() + " <br>Please do needfull.<br><br>Regard,<br>Mobex Seller System<br><br><br>";
                EmailSend.EmailSent(mailmessagefor, "Product Unlisted From Mobex Seller System", "care@mobex.in", "mobex@123", "dispatch@qarmatek.com");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Product UnListed Successfully." + "\");", true);
                BindDataGrid();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void gvInnerList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        Label lblSTATUSDESC = (Label)e.Row.FindControl("lblSTATUSDESC");

                        if (lblSTATUSDESC.Text == "LISTED" || lblSTATUSDESC.Text == "REJECTED" || lblSTATUSDESC.Text == "PURCHASE")
                        {
                            (e.Row.FindControl("btnUnlist") as LinkButton).Visible = true;
                            (e.Row.FindControl("btnCallAttent") as LinkButton).Visible = true;
                        }
                        else
                        {
                            (e.Row.FindControl("btnUnlist") as LinkButton).Visible = false;
                            (e.Row.FindControl("btnCallAttent") as LinkButton).Visible = false;
                        }

                        Label lblColor = (Label)e.Row.FindControl("lblColor");
                        if (lblColor.Text != "" && lblColor.Text != null && lblColor.Text != string.Empty)
                        {
                            //e.Row.CssClass = lblColor.Text;
                            GridViewRow gvMasterRow = (GridViewRow)e.Row.Parent.Parent.Parent.Parent;
                            gvMasterRow.CssClass = lblColor.Text;
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

        protected void btnCallAttent_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (Convert.ToString(Session["AGENTID"]) != null && Convert.ToString(Session["AGENTID"]) != "" && Convert.ToString(Session["AGENTID"]) != string.Empty)
                    {
                        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                        Label lblCONTACTNO = (Label)grdrow.FindControl("lblCONTACTNO");
                        Label lblID = (Label)grdrow.FindControl("lblID");

                        string strResp = objMainClass.ClickToCall("0" + lblCONTACTNO.Text, "CC", lblID.Text, Convert.ToString(Session["AGENTID"]));
                        if (strResp.Contains("ERROR:"))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + strResp + "\");", true);
                        }
                        else if (strResp.Contains("SUCCESS:"))
                        {
                            //int iReturn = objMainClass.UpdateStartTime(objMainClass.intCmpId, Convert.ToInt32(lblID.Value), DateTime.Now.ToString(),
                            //    Convert.ToInt32(Session["USERID"]), "UPDATESTARTTIME");
                            //if (iReturn != 1)
                            //{

                            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Start Time Not Updated.!');", true);
                            //}
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorized to call!');", true);
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


        [WebMethod]
        public static bool SubmitBid(string ItemCode, string BidAmount)
        {
            try
            {
                var client = new RestClient(("http://3.6.38.46/" + "api/SubmitBid"));
                //var client = new RestClient(("https://localhost:44397/" + "api/SubmitBid"));
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddParameter("ItemCode", ItemCode);
                request.AddParameter("BidAmount", BidAmount);
                var response = client.Execute(request);
                //IRestResponse response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}