using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.CRM
{
    public partial class frmCustAvailability : System.Web.UI.Page
    {

        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
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
                        if (FormRights.bView == false)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }

                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        objBindDDL.GetLeadProduct(ddlProduct, "GETLEADPRODUCT");
                        BindCallData();

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

        public void BindCallData()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetCustReq(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, txtContactNo.Text, ddlProduct.SelectedIndex == 0 ? "" : ddlProduct.SelectedItem.Text, txtCustName.Text, "GETCUSTREQ", 0);
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

        protected void lnkSearch_Click(object sender, EventArgs e)
        {

            try
            {
                if (Session["USERID"] != null)
                {

                    BindCallData();

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
                        Label lblID = (Label)e.Row.FindControl("lblID");
                        GridView gvInnerList = e.Row.FindControl("gvInnerList") as GridView;

                        DataTable dt = new DataTable();
                        dt = objMainClass.GetCustReq(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, txtContactNo.Text, ddlProduct.SelectedIndex == 0 ? "" : ddlProduct.SelectedItem.Text, txtCustName.Text, "GETAVAILDATA", Convert.ToInt32(lblID.Text));
                        if (dt.Rows.Count > 0)
                        {
                            gvInnerList.DataSource = dt;
                            gvInnerList.DataBind();
                            e.Row.CssClass = "rowGreen";
                        }
                        else
                        {
                            gvInnerList.DataSource = string.Empty;
                            gvInnerList.DataBind();
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

        protected void btnWhatsapp_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    Label lblCUSTNAME = (Label)grdrow.FindControl("lblCUSTNAME");
                    Label lblContactno = (Label)grdrow.FindControl("lblCONTACTNO");
                    Label lblPRODUCT = (Label)grdrow.FindControl("lblPRODUCT");
                    Label lblSPECNAME = (Label)grdrow.FindControl("lblATTRIBUTE");
                    Label lblSPECVALUE = (Label)grdrow.FindControl("lblATTRVALUE");

                    string MSG1 = "Dear " + lblCUSTNAME.Text + " Ji,  %0A%0A";
                    string MSG2 = "Product : " + lblPRODUCT.Text + " " + lblSPECNAME.Text + " : " + lblSPECVALUE.Text + " has arrived at our store. %0A%0AKindly visit our store at the soonest. %0A%0ALooking forward to seeing you soon. %0A%0A";
                    string MSG3 = "Location @Sarkhej : https://maps.app.goo.gl/HWdumeZn5Jp6ZPNM9 %0A%0A";
                    string MSG4 = "Location @Chandkheda : https://maps.app.goo.gl/WbdSSTjLJ7j1YifW8 %0A%0A";
                    string MSG5 = "Team Mobex";

                    string MSG = MSG1 + "" + MSG2 + "" + MSG3 + "" + MSG4 + "" + MSG5;

                    string url = "https://web.whatsapp.com/send?phone=+91" + lblContactno.Text + "&text=" + MSG;
                    //Response.Redirect("https://web.whatsapp.com/send?phone=+91" + lblContactno.Text + "&text=" + MSG);


                    string script = $"window.open('{url}', '_blank');";
                    ClientScript.RegisterStartupScript(this.GetType(), "openPage", script, true);

                    //WebClient webClient = new WebClient();
                    //string result = webClient.DownloadString(url);
                    // Process.Start(url);

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

        protected void bntView_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    Label lblID = (Label)grdrow.FindControl("lblID");
                    Label lblCUSTNAME = (Label)grdrow.FindControl("lblCUSTNAME");
                    Label lblCONTACTNO = (Label)grdrow.FindControl("lblCONTACTNO");
                    Label lblPRODUCT = (Label)grdrow.FindControl("lblPRODUCT");
                    Label lblATTRIBUTE = (Label)grdrow.FindControl("lblATTRIBUTE");
                    Label lblATTRVALUE = (Label)grdrow.FindControl("lblATTRVALUE");




                    DataTable dt = new DataTable();
                    dt = objMainClass.GetCustReq(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, txtContactNo.Text, ddlProduct.SelectedIndex == 0 ? "" : ddlProduct.SelectedItem.Text, txtCustName.Text, "GETAVAILDATA", Convert.ToInt32(lblID.Text));
                    if (dt.Rows.Count > 0)
                    {
                        gvInnerListNew.DataSource = dt;
                        gvInnerListNew.DataBind();

                        lblCUSTNAMEPOP.Text = lblCUSTNAME.Text;
                        lblCONTACTNOPOP.Text = lblCONTACTNO.Text;
                        lblPRODUCTPOP.Text = lblPRODUCT.Text;
                        lblSpecNamePOP.Text = lblATTRIBUTE.Text;
                        lblSpecValuePOP.Text = lblATTRVALUE.Text;

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);

                    }
                    else
                    {
                        gvInnerListNew.DataSource = string.Empty;
                        gvInnerListNew.DataBind();
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

        protected void btnWhatsappNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    Label lblCUSTNAME = (Label)grdrow.FindControl("lblCUSTNAME");
                    Label lblContactno = (Label)grdrow.FindControl("lblCONTACTNO");
                    Label lblPRODUCT = (Label)grdrow.FindControl("lblPRODUCT");
                    Label lblSPECNAME = (Label)grdrow.FindControl("lblATTRIBUTE");
                    Label lblSPECVALUE = (Label)grdrow.FindControl("lblATTRVALUE");

                    string MSG1 = "Dear " + lblCUSTNAME.Text + " Ji,  %0A%0A";
                    string MSG2 = "Product : " + lblPRODUCT.Text + " " + lblSPECNAME.Text + " : " + lblSPECVALUE.Text + " has arrived at our store. %0A%0AKindly visit our store at the soonest. %0A%0ALooking forward to seeing you soon. %0A%0A";
                    string MSG3 = "Location @Sarkhej : https://maps.app.goo.gl/HWdumeZn5Jp6ZPNM9 %0A%0A";
                    string MSG4 = "Location @Chandkheda : https://maps.app.goo.gl/WbdSSTjLJ7j1YifW8 %0A%0A";
                    string MSG5 = "Team Mobex";

                    string MSG = MSG1 + "" + MSG2 + "" + MSG3 + "" + MSG4 + "" + MSG5;

                    string url = "https://web.whatsapp.com/send?phone=+91" + lblContactno.Text + "&text=" + MSG;
                    //Response.Redirect("https://web.whatsapp.com/send?phone=+91" + lblContactno.Text + "&text=" + MSG);


                    string script = $"window.open('{url}', '_blank');";
                    ClientScript.RegisterStartupScript(this.GetType(), "openPage", script, true);

                    //WebClient webClient = new WebClient();
                    //string result = webClient.DownloadString(url);
                    // Process.Start(url);

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