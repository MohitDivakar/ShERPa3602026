using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.UTILITY
{
    public partial class AddNewMobileBrandList : System.Web.UI.Page
    {
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        DataTable dtItem = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["USERID"] != null)
                {
                    ddlstatus.SelectedValue = "1";
                }
            }
        }

        protected void imgSaveAll_Click(object sender, EventArgs e)
        {
            int mresult = 0;
            if (Session["USERID"] != null)
            {
                mresult = objMainClass.SaveMobileBrandList(txtBrandName.Text,Convert.ToInt32(ddlstatus.SelectedValue), Convert.ToInt32(Session["USERID"]));
                if(mresult > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Record Add Successfully." + "\");", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='Login.aspx' });", true);
            }
            Response.Redirect("MobileBrand.aspx");
        }
    }
}