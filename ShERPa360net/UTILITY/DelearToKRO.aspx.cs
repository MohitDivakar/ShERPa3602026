using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.UTILITY
{
    public partial class DelearToKRO : System.Web.UI.Page
    {
        MainClass objMainClass = new MainClass();
        DataTable dt = new DataTable();
        BindDDL objBindDDL = new BindDDL();
        DALUserRights objDALUserRights = new DALUserRights();
        bool Userright = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if(!IsPostBack)
                {
                    if (Session["USERID"] != null)
                    {
                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menutabid.Value, "");
                        Userright = FormRights.bView;

                        if (!Userright)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }
                        BindPageDown();
                    }
                }
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public void BindPageDown()
        {
            try
            {
                if (Session["USERID"]!= null)
                {
                    objBindDDL.FillMobexSellerVendorByBikerAreaWise(ddlVendor, Convert.ToInt32(Session["USERID"].ToString()));
                    ddlVendor.SelectedValue = "0";
                    objBindDDL.FillLists(ddlmaxday, "LED");
                    ddlmaxday.SelectedValue = "12237";
                }   
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnmakekro_Click(object sender, EventArgs e)
        {
            int mresult = 0;
            if (Session["USERID"] != null)
            {
                mresult = objMainClass.VendortoKro("UPDATE", Convert.ToInt32(ddlVendor.SelectedValue), Convert.ToInt32(ddlmaxday.SelectedItem.Text), Convert.ToInt32(Session["USERID"]));
                if(mresult > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Record Updated Successfully." + "\");", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='Login.aspx' });", true);
            }
        }

        protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if(ddlVendor.SelectedIndex > 0)
                    {
                        string vendorcodedetail = "";
                        DataTable dt = new DataTable();
                        dt = objMainClass.FetchDealerId(Convert.ToInt32(ddlVendor.SelectedValue), "DEALERASSOCIATEVENDOR");
                        foreach(DataRow dr in dt.Rows)
                        {
                            if(vendorcodedetail.Length > 0)
                            {
                                vendorcodedetail = vendorcodedetail + "," + dr["VENDCODE"].ToString(); 
                            }
                            else
                            {
                                vendorcodedetail = dr["VENDCODE"].ToString();
                            }
                        }
                        txtVendorCode.Text = vendorcodedetail;
                    }
                    else
                    {
                        txtVendorCode.Text = string.Empty;
                    }
                }
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}