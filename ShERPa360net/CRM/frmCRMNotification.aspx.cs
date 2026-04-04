using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.CRM
{
    public partial class frmCRMNotification : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    if (Session["USERID"] != null)
                    {
                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menutabid.Value, "");
                        if (FormRights.bView == false)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }

                        GetData();

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

        public void GetData()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetCRMNotificationData(0, 0, "GETDATA");

                    if (dt.Rows.Count > 0)
                    {
                        gvList.DataSource = dt;
                        gvList.DataBind();
                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            int iReturn = objMainClass.UpdateCRMNotification(Convert.ToInt32(dt.Rows[i]["ID"]), 1, "DATAVIEW");
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

        protected void btnWhatsapp_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                    Label lblCUSTNAME = (Label)grdrow.FindControl("lblCUSTNAME");
                    Label lblContactno = (Label)grdrow.FindControl("lblContactno");
                    Label lblPRODUCT = (Label)grdrow.FindControl("lblPRODUCT");
                    Label lblSPECNAME = (Label)grdrow.FindControl("lblSPECNAME");
                    Label lblSPECVALUE = (Label)grdrow.FindControl("lblSPECVALUE");

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