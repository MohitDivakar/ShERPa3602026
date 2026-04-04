using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.UTILITY
{
    public partial class frmViewCromaLotData : System.Web.UI.Page
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
                Session["CROMALOTDATA"] = null;
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
                        BindLotData();
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


        public void BindLotData()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetCromaLotData(objMainClass.intCmpId, 1, "", "", "", "GETLOTNEW");
                    if (dt.Rows.Count > 0)
                    {
                        //gvList.DataSource = dt;
                        //gvList.DataBind();

                        GridViewParent.DataSource = dt;
                        GridViewParent.DataBind();
                    }
                    else
                    {
                        //gvList.DataSource = string.Empty;
                        //gvList.DataBind();

                        GridViewParent.DataSource = string.Empty;
                        GridViewParent.DataBind();
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

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        LinkButton lblCROMALOTNO = (LinkButton)e.Row.FindControl("lblCROMALOTNO");
                        GridView gvInnerList = e.Row.FindControl("gvInnerList") as GridView;

                        DataTable dt = new DataTable();
                        dt = objMainClass.GetCromaLotData(objMainClass.intCmpId, 1, lblCROMALOTNO.Text, "", "", "GETLOTDETAIL");
                        if (dt.Rows.Count > 0)
                        {
                            gvInnerList.DataSource = dt;
                            gvInnerList.DataBind();
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

        protected void lblCROMALOTNO_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

                    LinkButton lblCROMALOTNO = (LinkButton)grdrow.FindControl("lblCROMALOTNO");

                    DataTable dt = new DataTable();
                    dt = objMainClass.GetCromaLotData(objMainClass.intCmpId, 1, lblCROMALOTNO.Text, "", "", "GETLOTALLDETAILS");

                    if (dt.Rows.Count > 0)
                    {

                        lblPopCromaLotNo.Text = Convert.ToString(dt.Rows[0]["CROMALOTNO"]);
                        lblPopQtekLotNo.Text = Convert.ToString(dt.Rows[0]["QTEKLOTNO"]);

                        gvPopDetail.DataSource = dt;
                        gvPopDetail.DataBind();
                        gvPopDetail.HeaderRow.TableSection = TableRowSection.TableHeader;

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                    }
                    else
                    {
                        gvPopDetail.DataSource = string.Empty;
                        gvPopDetail.DataBind();
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

        protected void lblQTEKLOTNO_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

                    LinkButton lblQTEKLOTNO = (LinkButton)grdrow.FindControl("lblQTEKLOTNO");

                    DataTable dt = new DataTable();
                    dt = objMainClass.GetCromaLotData(objMainClass.intCmpId, 1, "", lblQTEKLOTNO.Text, "", "GETLOTALLDETAILS");

                    if (dt.Rows.Count > 0)
                    {
                        lblPopCromaLotNo.Text = Convert.ToString(dt.Rows[0]["CROMALOTNO"]);
                        lblPopQtekLotNo.Text = Convert.ToString(dt.Rows[0]["QTEKLOTNO"]);

                        gvPopDetail.DataSource = dt;
                        gvPopDetail.DataBind();
                        gvPopDetail.HeaderRow.TableSection = TableRowSection.TableHeader;

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                    }
                    else
                    {
                        gvPopDetail.DataSource = string.Empty;
                        gvPopDetail.DataBind();
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

        protected void lblPRODUCT_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

                    LinkButton lblPRODUCT = (LinkButton)grdrow.FindControl("lblPRODUCT");
                    Label lblCROMACATLOTNO = (Label)grdrow.FindControl("lblCROMACATLOTNO");

                    DataTable dt = new DataTable();
                    dt = objMainClass.GetCromaLotData(objMainClass.intCmpId, 1, lblCROMACATLOTNO.Text, "", lblPRODUCT.Text, "GETLOTALLDETAILS");

                    if (dt.Rows.Count > 0)
                    {
                        lblPopCromaLotNo.Text = Convert.ToString(dt.Rows[0]["CROMALOTNO"]);
                        lblPopQtekLotNo.Text = Convert.ToString(dt.Rows[0]["QTEKLOTNO"]);

                        gvPopDetail.DataSource = dt;
                        gvPopDetail.DataBind();
                        gvPopDetail.HeaderRow.TableSection = TableRowSection.TableHeader;

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                    }
                    else
                    {
                        gvPopDetail.DataSource = string.Empty;
                        gvPopDetail.DataBind();
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

        protected void GridViewParent_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        HiddenField hfCromaLot = (HiddenField)e.Row.FindControl("hfCromaLot");
                        GridView GridViewChild = e.Row.FindControl("GridViewChild") as GridView;
                        Label lblSHOWLOT = e.Row.FindControl("lblSHOWLOT") as Label;
                        Label lblLotVisible = e.Row.FindControl("lblLotVisible") as Label;

                        if (lblSHOWLOT.Text == "1")
                        {
                            lblLotVisible.Text = "Yes";
                        }
                        else
                        {
                            lblLotVisible.Text = "No";
                        }


                        DataTable dt = new DataTable();
                        dt = objMainClass.GetCromaLotData(objMainClass.intCmpId, 1, hfCromaLot.Value, "", "", "GETLOTDETAIL");
                        if (dt.Rows.Count > 0)
                        {
                            GridViewChild.DataSource = dt;
                            GridViewChild.DataBind();
                        }
                        else
                        {
                            GridViewChild.DataSource = string.Empty;
                            GridViewChild.DataBind();
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

        public string FormatIndianCurrency(decimal amount)
        {
            System.Globalization.CultureInfo indCulture = new System.Globalization.CultureInfo("en-IN");
            return string.Format(indCulture, "{0:N2}", amount);
        }

        protected void lnkSetting_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-BidSetting').modal();", true);
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