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
    public partial class frmCheckWarrantyaspx : System.Web.UI.Page
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

        protected void lnkCheckIMEI_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetSODetails(objMainClass.intCmpId, "", txtIMEINO.Text, "CHECKWARRANTY", "", "");

                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToString(dt.Rows[0]["DELIVERYDATE"]) != null && Convert.ToString(dt.Rows[0]["DELIVERYDATE"]) != "" && Convert.ToString(dt.Rows[0]["DELIVERYDATE"]) != string.Empty)
                        {
                            DateTime olddate, newdate;
                            TimeSpan daydiff;

                            olddate = Convert.ToDateTime(dt.Rows[0]["DELIVERYDATE"]);
                            newdate = DateTime.Now;

                            int month = 12 * (newdate.Year - olddate.Year) + newdate.Month - olddate.Month;

                            daydiff = newdate - olddate;

                            if (month <= Convert.ToInt32(dt.Rows[0]["WARRANTY"]))
                            {
                                lblWarrantyStatus.Text = "In-Warranty (" + daydiff.Days + " Days)";
                                lblWarrantyStatus.ForeColor = System.Drawing.Color.Blue;
                            }
                            else
                            {
                                lblWarrantyStatus.Text = "Out-Warranty (" + daydiff.Days + " Days)";
                                lblWarrantyStatus.ForeColor = System.Drawing.Color.Red;
                            }

                            lblJobid.Text = Convert.ToString(dt.Rows[0]["JOBID"]);
                            lblDeliveryDate.Text = Convert.ToString(dt.Rows[0]["DELIVERYDATE"]);
                            lblCustname.Text = Convert.ToString(dt.Rows[0]["CUSTNAME"]);
                            lblSODdate.Text = Convert.ToString(dt.Rows[0]["SODT"]);
                            lblSOCreatedate.Text = Convert.ToString(dt.Rows[0]["SOCREATEDTAE"]);
                            lblSIDT.Text = Convert.ToString(dt.Rows[0]["SIDT"]);
                            lblSICreatedate.Text = Convert.ToString(dt.Rows[0]["SICREATEDATE"]);


                            gvList.DataSource = dt;
                            gvList.DataBind();
                            divData.Visible = true;
                        }
                        else
                        {
                            gvList.DataSource = string.Empty;
                            gvList.DataBind();
                            divData.Visible = false;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"Old job Id still open. Cannot check warranty. Job ID = " + Convert.ToString(dt.Rows[0]["JOBID"]) + ".!\");", true);
                        }
                    }
                    else
                    {
                        gvList.DataSource = string.Empty;
                        gvList.DataBind();
                        divData.Visible = false;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('IMEI number is wrong or SI/SO Cancelled or Returned.!');", true);

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