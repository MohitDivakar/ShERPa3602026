using ShERPa360net.Class;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.REPORTS
{
    public partial class rptMaterialMovement : System.Web.UI.Page
    {
        BindDDL objBindDDL = new BindDDL();
        DALUserRights objDALUserRights = new DALUserRights();
        MainClass objMainClass = new MainClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    if (Session["USERID"] != null)
                    {
                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menutibid.Value, "");
                        if (FormRights.bView == false)
                        {
                            //Session.Abandon();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }

                        objBindDDL.FillPlant(ddlPlancode);
                        ddlPlancode.SelectedValue = "1001";
                        objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPlancode.SelectedValue);
                        ddlLocation.SelectedValue = "MS01";
                        objBindDDL.FillSegment(ddlSegment);
                        ddlSegment.SelectedValue = "1015";
                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();

                        //BindGridView();

                        //DropDownList();
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

        private void DropDownList()
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    objBindDDL.FillPlant(ddlPlancode);
                    ddlPlancode.SelectedValue = "0";
                    objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPlancode.SelectedValue);
                    objBindDDL.FillSegment(ddlSegment);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }

        }

        private void BindGridView()
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.Matirial_Movment("", txtFromDate.Text, txtToDate.Text, ddlPlancode.SelectedIndex > 0 ? ddlPlancode.SelectedValue : "", ddlLocation.SelectedIndex > 0 ? ddlLocation.SelectedValue : "", textMatirialCode.Text, ddlSegment.SelectedIndex > 0 ? ddlSegment.SelectedValue : "", textTrackno.Text, textMovmentType.Text, txtImeino.Text, chkvalue.Checked == true ? 1 : 0);
                    if (dt.Rows.Count > 0)
                    {
                        if (chkvalue.Checked == true)
                        {
                            gvList.DataSource = dt;
                            gvList.DataBind();
                            gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                            witValue.Visible = true;
                            woValue.Visible = false;
                        }
                        else if (chkvalue.Checked == false)
                        {
                            gvWOList.DataSource = dt;
                            gvWOList.DataBind();
                            gvWOList.HeaderRow.TableSection = TableRowSection.TableHeader;
                            witValue.Visible = false;
                            woValue.Visible = true;
                        }
                    }

                    else
                    {
                        gvList.DataSource = null;
                        gvList.DataBind();
                        gvWOList.DataSource = null;
                        gvWOList.DataBind();
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


        protected void ddlPlancode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                objBindDDL.FillLocationByPlantCd(ddlLocation, ddlPlancode.SelectedValue);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                objBindDDL.FillLocationByPlantCd(ddlLocation, ddlLocation.SelectedValue);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }

        }
        ////protected void ddlsegment_selectedindexchanged(object sender, eventargs e)
        //{
        //    try
        //    {
        //        objbindddl.fillsegment(ddlsegment, ddlsegment.selectedvalue);
        //    }
        //    catch (exception ex)
        //    {
        //        scriptmanager.registerstartupscript(page, page.gettype(), "mymodal", "$('#modal-danger').modal();$('#lblerrmsg').text(\"" + ex.message + "\");", true);
        //    }

        //}
        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    BindGridView();
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