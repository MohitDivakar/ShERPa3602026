using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.UI.DataVisualization.Charting;

namespace ShERPa360net.REPORTS
{
    public partial class rptBudgetReport : System.Web.UI.Page
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
                        if (FormRights.bView == false)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }

                        DataTable dtPlant = new DataTable();
                        dtPlant = objMainClass.GetBudgetData("", 1, "", "GETPLANT");
                        if (dtPlant.Rows.Count > 0)
                        {
                            ddlPlantCode.DataSource = dtPlant;
                            ddlPlantCode.DataValueField = "PLANTCODE";
                            ddlPlantCode.DataTextField = "PLANT";
                            ddlPlantCode.DataBind();
                            ddlPlantCode.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                        }
                        else
                        {
                            ddlPlantCode.DataSource = string.Empty;
                            ddlPlantCode.DataBind();
                            ddlPlantCode.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                        }
                        ddlPlantCode_SelectedIndexChanged(1, e);
                        lnkSearh_Click(1, e);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "GetGraph()", true);

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

        protected void ddlPlantCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dtBudget = new DataTable();
                    dtBudget = objMainClass.GetBudgetData(ddlPlantCode.SelectedIndex > 0 ? ddlPlantCode.SelectedValue : "", 1, "", "GETBUDGETCODE");
                    if (dtBudget.Rows.Count > 0)
                    {
                        ddlBudgetCode.DataSource = dtBudget;
                        ddlBudgetCode.DataValueField = "BUDGETCODE";
                        ddlBudgetCode.DataTextField = "BUDGETFOR";
                        ddlBudgetCode.DataBind();
                        ddlBudgetCode.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                    }
                    else
                    {
                        ddlBudgetCode.DataSource = string.Empty;
                        ddlBudgetCode.DataBind();
                        ddlBudgetCode.Items.Insert(0, new ListItem("-- SELECT --", "0"));
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

        protected void lnkSearh_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dtData = new DataTable();
                    dtData = objMainClass.GetBudgetData(ddlPlantCode.SelectedIndex > 0 ? ddlPlantCode.SelectedValue : "", 1, ddlBudgetCode.SelectedIndex > 0 ? ddlBudgetCode.SelectedValue : "", "PLANTBUDGET");
                    if (dtData.Rows.Count > 0)
                    {
                        gvList.DataSource = dtData;
                        gvList.DataBind();
                    }
                    else
                    {
                        gvList.DataSource = string.Empty;
                        gvList.DataBind();
                    }
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "GetGraph()", true);
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
                        Label lblPLANTCODE = (Label)e.Row.FindControl("lblPLANTCODE");
                        GridView gvInnerList = e.Row.FindControl("gvInnerList") as GridView;
                        DataTable dt = new DataTable();
                        dt = objMainClass.GetBudgetData(lblPLANTCODE.Text, 1, ddlBudgetCode.SelectedIndex > 0 ? ddlBudgetCode.SelectedValue : "", "PLANTWISEBUDGET");
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

        protected void gvInnerList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        Label lblBUDGETCODE = (Label)e.Row.FindControl("lblBUDGETCODE");
                        GridView gvInnerListNew = e.Row.FindControl("gvInnerListNew") as GridView;
                        DataTable dt = new DataTable();
                        dt = objMainClass.GetBudgetData("", 1, lblBUDGETCODE.Text, "BUDGETCODEWISE");
                        if (dt.Rows.Count > 0)
                        {
                            gvInnerListNew.DataSource = dt;
                            gvInnerListNew.DataBind();
                        }
                        else
                        {
                            gvInnerListNew.DataSource = string.Empty;
                            gvInnerListNew.DataBind();
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


        [WebMethod]
        public static List<object> GetChartData1(string PLANTCODE, string BUDGETCODE)
        {
            //FROMDATE = Convert.ToDateTime(FROMDATE).ToString("yyyy-MM-dd");
            //TODATE = Convert.ToDateTime(TODATE).ToString("yyyy-MM-dd");
            string query = "SP_BUDGETREPORT";
            string constr = ConfigurationManager.ConnectionStrings["ConnStringSherpa"].ConnectionString;
            List<object> chartData = new List<object>();
            chartData.Add(new object[]
            {
        "BUDGETFOR","TOTALBUDGET", "USED","REMAIN"
            });
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@PLANTCODE", PLANTCODE == "0" ? "" : PLANTCODE);
                    cmd.Parameters.AddWithValue("@STATUS", 1);
                    cmd.Parameters.AddWithValue("@BUDGETCODE", BUDGETCODE == "0" ? "" : BUDGETCODE);
                    cmd.Parameters.AddWithValue("@ACTION", "PLANTWISEBUDGET");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    con.Open();
                    //SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //DataTable dddttt = new DataTable();
                    //da.Fill(dddttt);

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            chartData.Add(new object[]
                            {
                        sdr["BUDGETFOR"]+" - "+ sdr["PLANTCODE"], sdr["TOTALBUDGET"], sdr["USED"], sdr["REMAIN"]
                            });
                        }
                    }
                    con.Close();
                    return chartData;
                }
            }
        }

        [WebMethod]
        public static List<object> GetChartData3(string PLANTCODE, string BUDGETCODE)
        {
            //FROMDATE = Convert.ToDateTime(FROMDATE).ToString("yyyy-MM-dd");
            //TODATE = Convert.ToDateTime(TODATE).ToString("yyyy-MM-dd");
            string query = "SP_BUDGETREPORT";
            string constr = ConfigurationManager.ConnectionStrings["ConnStringSherpa"].ConnectionString;
            List<object> chartData = new List<object>();
            chartData.Add(new object[]
            {
        "PLANT", "USEDPER"
            });
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@PLANTCODE", PLANTCODE == "0" ? "" : PLANTCODE);
                    cmd.Parameters.AddWithValue("@STATUS", 1);
                    cmd.Parameters.AddWithValue("@BUDGETCODE", BUDGETCODE == "0" ? "" : BUDGETCODE);
                    cmd.Parameters.AddWithValue("@ACTION", "PLANTBUDGET");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    con.Open();
                    //SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //DataTable dddttt = new DataTable();
                    //da.Fill(dddttt);

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            string[] para = { " - " };
                            chartData.Add(new object[]
                             {
                        //Convert.ToString(sdr["PLANT"]).Split(para,0)[0] + System.Environment.NewLine + Convert.ToString(sdr["PLANT"]).Split(para,0)[1] +" " +Convert.ToString(sdr["PLANT"]).Split(para,0)[2], sdr["USEDPER"]
                        //Convert.ToString(sdr["PLANT"]).Split(para,0)[0] + "<br/>" + Convert.ToString(sdr["PLANT"]).Split(para,0)[1] , sdr["USEDPER"]
                        Convert.ToString(sdr["PLANT1"]), sdr["USEDPER"]
                             });
                        }
                    }
                    con.Close();
                    return chartData;
                }
            }
        }

    }
}