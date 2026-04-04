using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.REPORTS
{
    public partial class rptClaimData : System.Web.UI.Page
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
                        string[] date = objMainClass.indianTime.Date.ToString("dd-MM-yyyy").Split('-');
                        DateTime fromdate = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), 1);
                        txtFromDocDate.Text = fromdate.ToString("dd-MM-yyyy");
                        txtToDocDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        objBindDDL.FillPlant(ddlPlantCode, "SEARCH");
                        ddlPlantCode.SelectedValue = "1001";
                        objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPlantCode.SelectedValue);
                        objBindDDL.FillLists(ddlStatus, "CS");

                        BindGrid();
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

        public void BindGrid()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dtDashBoard = new DataTable();
                    dtDashBoard = objMainClass.GetClaimData(objMainClass.intCmpId, ddlStatus.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddlStatus.SelectedValue), txtFromDocDate.Text, txtToDocDate.Text, txtJobID.Text, ddlPlantCode.SelectedIndex == 0 ? "" : ddlPlantCode.SelectedValue, ddlLocation.SelectedIndex == 0 ? "" : ddlLocation.SelectedValue, "CS", "DASHBOARDCLAIM","","");

                    lblPENDINGFORCLAIM.Text = Convert.ToString(dtDashBoard.Rows[0]["CNT"]);
                    lblPENDINGFORSETTLE.Text = Convert.ToString(dtDashBoard.Rows[1]["CNT"]);
                    lblCLAIMSETTLED.Text = Convert.ToString(dtDashBoard.Rows[2]["CNT"]);
                    lblCLAIMREJECTED.Text = Convert.ToString(dtDashBoard.Rows[3]["CNT"]);
                    lblCLAIMCLOSED.Text = Convert.ToString(dtDashBoard.Rows[4]["CNT"]);
                    lblAMOUNTRECEIVED.Text = Convert.ToString(dtDashBoard.Rows[5]["CNT"]);


                    DataTable dtData = new DataTable();
                    dtData = objMainClass.GetClaimData(objMainClass.intCmpId, ddlStatus.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddlStatus.SelectedValue), txtFromDocDate.Text, txtToDocDate.Text, txtJobID.Text, ddlPlantCode.SelectedIndex == 0 ? "" : ddlPlantCode.SelectedValue, ddlLocation.SelectedIndex == 0 ? "" : ddlLocation.SelectedValue, "CS", "CLAIMDATA", "", "");
                    if (dtData.Rows.Count > 0)
                    {
                        gvAllList.DataSource = dtData;
                        gvAllList.DataBind();
                        gvAllList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    else
                    {
                        gvAllList.DataSource = string.Empty;
                        gvAllList.DataBind();
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
                    BindGrid();
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
                            objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPlantCode.SelectedValue);
                        }
                        else
                        {
                            ddlLocation.DataSource = string.Empty;
                            ddlLocation.DataBind();
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
    }
}