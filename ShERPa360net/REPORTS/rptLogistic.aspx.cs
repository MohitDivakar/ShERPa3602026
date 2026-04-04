using DocumentFormat.OpenXml.Drawing;
using Newtonsoft.Json;
using ShERPa360net.Class;
using ShERPa360net.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ShERPa360net.REPORTS
{
    public partial class rptLogistic : System.Web.UI.Page
    {
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        bool Userright = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtFromDate.Text = (objMainClass.indianTime.Date.AddDays(-30)).ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    if (Session["USERID"] != null)
                    {
                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menutabCheckerid.Value, "");
                        Userright = FormRights.bView;

                        //System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
                        //timer1.Interval = 1;//5 seconds
                        //timer1.Tick += new System.EventHandler(timer_Tick);
                        //timer1.Start();

                        if (!Userright)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        BindPageDropDown();
                        BindProductDetail();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
        protected void tmrLive_Tick(object sender, EventArgs e)
        {
            //do whatever you want 
            //RefreshControls();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void lnkSearh_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    BindProductDetail();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void imgReset_Click(object sender, EventArgs e)
        {
            try
            {
                ResetFormControl();
                BindProductDetail();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
        #region PAGEMETHOD
        public void BindPageDropDown()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    //objBindDDL.FillPlant(ddlPlantCode);
                    //ddlPlantCode.SelectedValue = "0";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public void BindProductDetail()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    gvProduct.DataSource = null;
                    gvProduct.DataBind();
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetLogistic(txtFromDate.Text, txtToDate.Text,txtCRNNO.Text,txtJOBNO.Text, "SELECTALL").Tables[0];
                    try
                    {
                        string INTERVAL = Convert.ToString(objMainClass.GetLogistic(txtFromDate.Text, txtToDate.Text, txtCRNNO.Text, txtJOBNO.Text, "SELECTALL").Tables[1].Rows[0]["AUTOSERSETTINTERVAL"]);
                        tmrLive.Dispose();
                        tmrLive.Interval = Convert.ToInt32(INTERVAL)* 60000;
                    }
                    catch (Exception EX)
                    {
                        tmrLive.Interval = Convert.ToInt32(5) * 60000;
                    }
                  
                    if (dt.Rows.Count > 0)
                    {
                        gvProduct.DataSource = dt;
                        gvProduct.DataBind();
                        gvProduct.HeaderRow.TableSection = TableRowSection.TableHeader;
                        lgrecordcount.InnerText = "Records : " + dt.Rows.Count.ToString();
                    }
                    else
                    {
                        gvProduct.DataSource = dt;
                        gvProduct.DataBind();
                        gvProduct.HeaderRow.TableSection = TableRowSection.TableHeader;
                        lgrecordcount.InnerText = "Records : " + "0";
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
        //static double GetInterval(int TIME)
        //{
        //    DateTime now = DateTime.Now.AddSeconds(TIME*60);
        //    return ((60 - now.Second) * 1000 - now.Millisecond);
        //}

        public void ResetFormControl()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    txtFromDate.Text = (objMainClass.indianTime.Date.AddDays(-30)).ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                   // ddlPlantCode.SelectedValue = "0";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='Login.aspx' });", true);
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


        #endregion

        protected void gvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if ((e.Row.FindControl("lblTRACKINGURL") as Label) != null && !string.IsNullOrEmpty((e.Row.FindControl("lblTRACKINGURL") as Label).Text))
                {
                    HtmlAnchor lnkVoucher = (e.Row.FindControl("aTRACKINGURL") as HtmlAnchor);
                    lnkVoucher.HRef = ((e.Row.FindControl("lblTRACKINGURL") as Label).Text);
                    lnkVoucher.InnerHtml = (e.Row.FindControl("lblTRACKINGURL") as Label).Text;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#mod-al-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}