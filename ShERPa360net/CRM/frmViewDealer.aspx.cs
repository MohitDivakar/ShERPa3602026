using com.sun.corba.se.pept.transport;
using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.CRM
{
    public partial class frmViewDealer : System.Web.UI.Page
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

                        if (FormRights.bAdd == false)
                        {
                            lnkNewDealer.Enabled = false;
                        }

                        string[] date = objMainClass.indianTime.Date.ToString("dd-MM-yyyy").Split('-');
                        DateTime fromdate = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), 1);
                        txtFromDocDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //fromdate.ToString("dd-MM-yyyy");
                        txtToDocDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");


                        objBindDDL.FillLists(ddlCategory, "DC");
                        GetData(objMainClass.intCmpId, "SELECT", "", "", chkStatus.Checked == true ? 1 : 0, txtFromDocDate.Text, txtToDocDate.Text);

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

        public void GetData(int CMPID, string ACTION, string DEALERNAME, string CATEGORY, int STATUS, string FROMDATE, string TODATE)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetDealer(CMPID, ACTION, DEALERNAME, CATEGORY, STATUS, FROMDATE, TODATE);
                    if (dt.Rows.Count > 0)
                    {
                        gvList.DataSource = dt;
                        gvList.DataBind();
                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;

                        //var dtresult =   objDALUserRights.SELECT_MENURIGHTS_BYUSERID(Session["USERID"].ToString(), menucontactno.Value);
                        if (Convert.ToInt32(Session["USERID"].ToString()) == 751 || Convert.ToInt32(Session["USERID"].ToString()) == 1224
                           || Convert.ToInt32(Session["USERID"].ToString()) == 29 )
                        {
                            gvList.Columns[1].Visible = true;
                        }
                        else
                        {
                            gvList.Columns[1].Visible = false;
                        }
                    }
                    else
                    {
                        gvList.DataSource = string.Empty;
                        gvList.DataBind();

                        if (Convert.ToInt32(Session["USERID"].ToString()) == 751 || Convert.ToInt32(Session["USERID"].ToString()) == 1224
                           || Convert.ToInt32(Session["USERID"].ToString()) == 29)
                        {
                            gvList.Columns[1].Visible = true;
                        }
                        else
                        {
                            gvList.Columns[1].Visible = false;
                        }

                        //var dtresult = objDALUserRights.SELECT_MENURIGHTS_BYUSERID(Session["USERID"].ToString(), menucontactno.Value);
                        //if (dtresult.Rows.Count > 0)
                        //{
                        //    gvList.Columns[1].Visible = true;
                        //}
                        //else
                        //{
                        //    gvList.Columns[1].Visible = false;
                        //}
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

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        (e.Row.FindControl("btnUpdate") as LinkButton).Attributes.Add("onclick", "return VALIDATECONTACTNO(" +
                      "'" + (e.Row.FindControl("txtcontactno") as TextBox).ClientID
                       + "','" + (e.Row.FindControl("lblcontactnoalert") as Label).ClientID + "');");

                        DataRowView dr = (DataRowView)e.Row.DataItem;
                        TextBox lblcontactno = e.Row.FindControl("txtcontactno") as TextBox;
                        Label lblImageData = e.Row.FindControl("lblImageData") as Label;
                        if (lblImageData.Text != null && lblImageData.Text != string.Empty && lblImageData.Text != "")
                        {
                            string imageUrl = "data:image/jpg;base64," + Convert.ToBase64String((byte[])dr["SHOPIMAGE"]);
                            (e.Row.FindControl("imgImage") as Image).ImageUrl = imageUrl;
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

        protected void lnkSerch_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GetData(objMainClass.intCmpId, "SELECT", txtDealerName.Text, ddlCategory.SelectedValue == "0" ? "" : ddlCategory.SelectedValue, chkStatus.Checked == true ? 1 : 0, txtFromDocDate.Text, txtToDocDate.Text);
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

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void lnkExport_Click(object sender, EventArgs e)
        {
            try
            {
                string attachment = "attachment; filename=DealerList.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vdn.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gvList.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer; 
                    int ID = Convert.ToInt32(((HiddenField)grdrow.FindControl("HDID")).Value);
                    string contactno = ((TextBox)grdrow.FindControl("txtcontactno")).Text;
                    string contactno2 = ((TextBox)grdrow.FindControl("txtcontactno2")).Text;
                    string contactno3 = ((TextBox)grdrow.FindControl("txtcontactno3")).Text;
                    int result = objMainClass.UpdateContactnNo(ID, contactno, contactno2, contactno3, "UPDATECONTACTNO");
                    GetData(objMainClass.intCmpId, "SELECT", txtDealerName.Text, ddlCategory.SelectedValue == "0" ? "" : ddlCategory.SelectedValue, chkStatus.Checked == true ? 1 : 0, txtFromDocDate.Text, txtToDocDate.Text);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Contact No. Updated Successfully." + "\");", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                }
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}