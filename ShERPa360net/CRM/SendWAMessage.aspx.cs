using Newtonsoft.Json;
using RestSharp;
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
    public partial class SendWAMessage : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        //DALQRC objDALQRC = new DALQRC();
        BindDDL objBindDDL = new BindDDL();
        DALUserRights objDALUserRights = new DALUserRights();
        WAClass objWAClass = new WAClass();

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
                            //Session.Abandon();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        if (FormRights.bAdd == false)
                        {
                            btnSend.Enabled = false;
                        }

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

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                //SendTextMessage();

                //if (objWAClass.CheckWAConnection() == true)
                //{
                //SendTextMessage();

                //string Response = objWAClass.SendTextMessage(txtMessage.Text, "91" + txtMobileNo.Text, Convert.ToString(Session["USERID"]));
                string Response = objWAClass.SendMessageNewAPI(txtMessage.Text, "91" + txtMobileNo.Text, Convert.ToString(Session["USERID"]), txtURL.Text);
                if (Response != "" && Response != string.Empty && Response != null)
                {
                    string[] response1 = Response.Split(",".ToCharArray());
                    if (Convert.ToInt32(response1[0]) == 1)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"" + Convert.ToString(response1[1]) + "\");", true);
                        txtMessage.Text = string.Empty;
                        txtMobileNo.Text = string.Empty;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + Convert.ToString(response1[1]) + "\");", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Mesage not sent.!');", true);
                }

                //}
                //else
                //{
                //    if (objWAClass.ReConnectWA() == true)
                //    {
                //        if (objWAClass.CheckWAConnection() == true)
                //        {
                //            string Response = objWAClass.SendTextMessage(txtMessage.Text, "91" + txtMobileNo.Text, Convert.ToString(Session["USERID"]));
                //            if (Response != "" && Response != string.Empty && Response != null)
                //            {
                //                string[] response1 = Response.Split(",".ToCharArray());
                //                if (Convert.ToInt32(response1[0]) == 1)
                //                {
                //                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"" + Convert.ToString(response1[1]) + "\");", true);
                //                }
                //                else
                //                {
                //                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + Convert.ToString(response1[1]) + "\");", true);
                //                }
                //            }
                //            else
                //            {
                //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Mesage not sent.!');", true);
                //            }

                //        }
                //        else
                //        {
                //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Device not connected. Please connect device to instance.!');", true);
                //        }
                //    }
                //    else
                //    {
                //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Device not connected. Please connect device to instance.!');", true);
                //    }
                //}

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }


        //private void SendTextMessage()
        //{
        //    try
        //    {
        //        //var client = new RestClient("https://console.wa0.in/api/send.php?client_id=3a13f8abba76457a7bd9e6378b42ec15&instance=65cd6e0db8fe830fc320bdd5572b9bdf&number=918460591264&message=MESSAGE_HERE&type=text");

        //        var client = new RestClient("https://console.wa0.in/api/send.php?client_id=" + objWAClass.client_id + "&instance=" + objWAClass.instance + "&number=91" + txtMobileNo.Text + "&message=" + txtMessage.Text + "&type=text");
        //        client.Timeout = -1;
        //        var request = new RestRequest(Method.POST);
        //        IRestResponse response = client.Execute(request);
        //        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            string jsonsend = response.Content;
        //            jsonsend = "[" + jsonsend + "]";
        //            DataTable dtValuesend = (DataTable)JsonConvert.DeserializeObject(jsonsend, (typeof(DataTable)));
        //            if (Convert.ToString(dtValuesend.Rows[0]["status"]) == objWAClass.statusTrue || Convert.ToString(dtValuesend.Rows[0]["status"]) == objWAClass.msgQue)
        //            {
        //                objMainClass.WALOG(objMainClass.intCmpId, txtMessage.Text, txtMobileNo.Text, Convert.ToString(Session["USERID"]), "");
        //                txtMobileNo.Text = string.Empty;
        //                txtMessage.Text = string.Empty;
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
    }
}


