using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.UTILITY
{
    public partial class frmImageUpload : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();

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
                            //   Session.Abandon();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }

                        if (FormRights.bAdd == false)
                        {
                            lnkUpload.Enabled = false;
                            lnkMobexImage.Enabled = false;
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


        //protected void lnkMobexImage_Click(object sender, EventArgs e)
        //{
        //    DataTable dt = new DataTable();
        //    dt = objMainClass.GetImageUrl(txtJobid.Text);
        //    if (dt.Rows.Count > 0)
        //    {
        //        divPreview.Visible = true;
        //        dlImages.DataSource = dt;
        //        dlImages.DataBind();
        //        divPreview.Visible = true;
        //    }

        //}

        //protected void txtJobid_TextChanged(object sender, EventArgs e)
        //{
        //    //GET_MOBEX_IMAGEURL
        //    DataTable dt = new DataTable();
        //    dt = objMainClass.GetImageUrl(txtJobid.Text);
        //    if (dt.Rows.Count > 0)
        //    {
        //        txtJobid.Text = string.Empty;
        //        string message = "Job Id Already Exists. Please Enter New Job Id!";
        //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //        sb.Append("<script type = 'text/javascript'>");
        //        sb.Append("window.onload=function(){");
        //        sb.Append("alert('");
        //        sb.Append(message);
        //        sb.Append("')};");
        //        sb.Append("</script>");
        //        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

        //    }
        //}



        [WebMethod]
        public static string InsertData(string JOBID, string IMAGEURL)
        {
            MainClass objMainClass = new MainClass();
            string Message = string.Empty;
            int iResult = objMainClass.InsertImageUrl(JOBID, IMAGEURL);

            if (iResult == 1)
            {
                Message = "Image Inserted Successfully.";
            }
            else
            {
                Message = "Image Not Inserted.";
            }

            return Message;

        }
    }
}