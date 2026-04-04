using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net
{
    public partial class OpenURL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnOpen_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            MainClass objMainclass = new MainClass();
            dt = objMainclass.OtherData(Convert.ToInt32(txtFromUrlNo.Text), Convert.ToInt32(txtToUrlNo.Text), "OPENURL");

            if (dt.Rows.Count > 0)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    string path = Convert.ToString(dt.Rows[j]["URL"]);//.Replace("https://","").Replace("http://", "");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "');", true);
                    //function myFunction() { window.open("https://www.w3schools.com"); }
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "function ABC() { window.open(" + path + "); }", true);
                    System.Diagnostics.Process.Start("chrome.exe", " /new-tab " + string.Join(" ", path));
                    //System.Diagnostics.Process.Start("chrome.exe", "-new-window " + string.Join(" ", path));
                    if (j != dt.Rows.Count - 1)
                    {
                        Thread.Sleep(10000); // sleep for 30 second
                    }
                }

            }

        }
    }
}