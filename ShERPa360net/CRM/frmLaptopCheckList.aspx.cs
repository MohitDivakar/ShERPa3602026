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
    public partial class frmLaptopCheckList : System.Web.UI.Page
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

                        txtDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        DataTable dtItemCat = new DataTable();
                        dtItemCat = objMainClass.SelectItem("", "", "", "", "", "13", "");
                        if (dtItemCat.Rows.Count > 0)
                        {
                            ddlProject.DataSource = dtItemCat;
                            ddlProject.DataTextField = "Desciption";
                            ddlProject.DataValueField = "Desciption";
                            ddlProject.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                            ddlProject.DataBind();
                            ddlProject.SelectedValue = "LAPTOP";
                        }
                        else
                        {
                            ddlProject.DataSource = null;
                            ddlProject.DataBind();
                            ddlProject.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                        }

                        objBindDDL.FillLists(ddlColor, "CL");
                        ddlColor.SelectedValue = "166";
                        //ddlColor.Enabled = false;

                        objBindDDL.FillLists(ddlStatus, "PRS");
                        //ddlStatus.SelectedValue = "11926";
                        ddlStatus.SelectedValue = "12369";
                        //ddlStatus.Enabled = false;

                        objBindDDL.FillLists(ddlGrade, "BG");
                        ddlGrade.SelectedValue = "11219";
                        //ddlGrade.Enabled = false;

                        objBindDDL.FillLists(ddlVerifiedBy, "VRB");
                        //ddlVerifiedBy.SelectedValue = "11219";

                        BindCheckList();


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

        public void BindCheckList()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetSafetyQuestion(1, "LAPTOP");
                    if (dt.Rows.Count > 0)
                    {
                        grvCheckList.DataSource = dt;
                        grvCheckList.DataBind();
                    }
                    else
                    {
                        grvCheckList.DataSource = string.Empty;
                        grvCheckList.DataBind();
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

        protected void grvCheckList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (Session["USERID"] != null)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string pr = ((Label)e.Row.FindControl("lblPhotoReq")).Text;
                    if (pr == "1")
                    {
                        e.Row.Cells[6].Visible = true;
                    }
                    else
                    {
                        e.Row.Cells[6].Visible = false;
                    }

                    string lblID = ((Label)e.Row.FindControl("lblID")).Text;
                    RadioButtonList rblAnswer = ((RadioButtonList)e.Row.FindControl("rblAnswer"));
                    TextBox txtRemarks = ((TextBox)e.Row.FindControl("txtRemarks"));
                    RequiredFieldValidator rfvRemarks = (RequiredFieldValidator)e.Row.FindControl("rfvRemarks");

                    DropDownList ddlRemarks = ((DropDownList)e.Row.FindControl("ddlRemarks"));
                    RequiredFieldValidator rfvDDLRemarks = (RequiredFieldValidator)e.Row.FindControl("rfvDDLRemarks");

                    if (lblID == "6")
                    {
                        objBindDDL.FillLists(ddlRemarks, "DSR");
                    }
                    else if (lblID == "7")
                    {
                        objBindDDL.FillLists(ddlRemarks, "IOP");
                    }
                    else if (lblID == "8")
                    {
                        objBindDDL.FillLists(ddlRemarks, "CHR");
                    }
                    else if (lblID == "9")
                    {
                        objBindDDL.FillLists(ddlRemarks, "KBD");
                    }
                    else if (lblID == "10")
                    {
                        objBindDDL.FillLists(ddlRemarks, "SPR");
                    }
                    else if (lblID == "11")
                    {
                        objBindDDL.FillLists(ddlRemarks, "MIC");
                    }
                    else if (lblID == "13")
                    {
                        objBindDDL.FillLists(ddlRemarks, "AUQ");
                    }
                    else if (lblID == "14")
                    {
                        objBindDDL.FillLists(ddlRemarks, "WEC");
                    }
                    else if (lblID == "18")
                    {
                        objBindDDL.FillLists(ddlRemarks, "CFW");
                    }
                    else if (lblID == "19")
                    {
                        objBindDDL.FillLists(ddlRemarks, "ACC");
                    }


                    if (lblID == "29")
                    {
                        rblAnswer.Items[0].Text = "Yes";
                        rblAnswer.Items[1].Text = "No";
                        rblAnswer.Items[1].Selected = true;
                        txtRemarks.Visible = false;
                        rfvRemarks.Visible = false;
                        rfvRemarks.Enabled = false;

                        ddlRemarks.Visible = false;
                        rfvDDLRemarks.Visible = false;
                        rfvDDLRemarks.Enabled = false;
                    }



                    if (lblID == "3" || lblID == "4" || lblID == "5" || lblID == "15" || lblID == "17")
                    {
                        txtRemarks.Visible = true;
                        rfvRemarks.Visible = true;
                        rfvRemarks.Enabled = true;
                        rfvRemarks.Style.Add("display", "block");

                        ddlRemarks.Visible = false;
                        rfvDDLRemarks.Visible = false;
                        rfvDDLRemarks.Enabled = false;
                    }

                    //if (lblID == "6" || lblID == "7" || lblID == "8" || lblID == "9" || lblID == "10" || lblID == "11" || lblID == "13" || lblID == "14" || lblID == "18" || lblID == "19")
                    //{
                    //    txtRemarks.Visible = false;
                    //    rfvRemarks.Visible = false;
                    //    rfvRemarks.Enabled = false;


                    //    ddlRemarks.Visible = true;
                    //    rfvDDLRemarks.Visible = true;
                    //    rfvDDLRemarks.Enabled = true;
                    //    rfvDDLRemarks.Style.Add("display", "block");
                    //}



                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
            }
        }

        protected void rblAnswer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["USERID"] != null)
            {
                RadioButtonList rbl = sender as RadioButtonList;

                GridViewRow row = ((GridViewRow)((RadioButtonList)sender).NamingContainer);
                TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                RequiredFieldValidator rfvRemarks = (RequiredFieldValidator)row.FindControl("rfvRemarks");

                DropDownList ddlRemarks = (DropDownList)row.FindControl("ddlRemarks");
                RequiredFieldValidator rfvDDLRemarks = (RequiredFieldValidator)row.FindControl("rfvDDLRemarks");

                Label lblID = (Label)row.FindControl("lblID");
                if (rbl.SelectedValue == "0")
                {
                    if (lblID.Text == "29")
                    {
                        txtRemarks.Visible = false;
                        rfvRemarks.Visible = false;
                        rfvRemarks.Enabled = false;

                        ddlRemarks.Visible = false;
                        rfvDDLRemarks.Visible = false;
                        rfvDDLRemarks.Enabled = false;
                    }
                    else if (lblID.Text == "6" || lblID.Text == "7" || lblID.Text == "8" || lblID.Text == "9" || lblID.Text == "10" || lblID.Text == "11" || lblID.Text == "13" || lblID.Text == "14" || lblID.Text == "18" || lblID.Text == "19")
                    {
                        txtRemarks.Visible = false;
                        rfvRemarks.Visible = false;
                        rfvRemarks.Enabled = false;


                        ddlRemarks.Visible = true;
                        rfvDDLRemarks.Visible = true;
                        rfvDDLRemarks.Enabled = true;
                    }
                    else
                    {
                        txtRemarks.Visible = true;
                        rfvRemarks.Visible = true;
                        rfvRemarks.Enabled = true;
                        rfvRemarks.Style.Add("display", "block");

                        ddlRemarks.Visible = false;
                        rfvDDLRemarks.Visible = false;
                        rfvDDLRemarks.Enabled = false;
                    }
                }
                else
                {
                    if (lblID.Text == "29")
                    {
                        txtRemarks.Visible = true;
                        rfvRemarks.Visible = true;
                        rfvRemarks.Enabled = true;
                        rfvRemarks.Style.Add("display", "block");
                    }
                    else if (lblID.Text == "6" || lblID.Text == "7" || lblID.Text == "8" || lblID.Text == "9" || lblID.Text == "10" || lblID.Text == "11" || lblID.Text == "13" || lblID.Text == "14" || lblID.Text == "18" || lblID.Text == "19")
                    {
                        txtRemarks.Visible = false;
                        rfvRemarks.Visible = false;
                        rfvRemarks.Enabled = false;


                        ddlRemarks.Visible = false;
                        rfvDDLRemarks.Visible = false;
                        rfvDDLRemarks.Enabled = false;
                    }
                    else
                    {
                        txtRemarks.Visible = false;
                        rfvRemarks.Visible = false;
                        rfvRemarks.Enabled = false;
                    }
                }

                if (lblID.Text == "3" || lblID.Text == "4" || lblID.Text == "5" || lblID.Text == "15" || lblID.Text == "17")
                {
                    txtRemarks.Visible = true;
                    rfvRemarks.Visible = true;
                    rfvRemarks.Enabled = true;
                    rfvRemarks.Style.Add("display", "block");
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Session["USERID"] != null)
            {
                try
                {
                    int iResult = objMainClass.InsertCheckList(objMainClass.intCmpId, txtDate.Text, txtJobID.Text, txtSRNo.Text, ddlProject.SelectedItem.Text, ddlStatus.SelectedItem.Text, ddlGrade.SelectedItem.Text, txtMake.Text, txtModel.Text, ddlColor.SelectedItem.Text,
                        Convert.ToInt32(Session["USERID"]), Convert.ToInt32(ddlVerifiedBy.SelectedValue), Convert.ToInt32(Session["USERID"]), grvCheckList);
                    if (iResult == 1)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Check list updated successfully! \");$('.close').click(function(){window.location.href ='frmViewLaptopCheckList.aspx' });", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Check list not updated sucessfully!');", true);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
            }
        }

        protected void txtJobID_TextChanged(object sender, EventArgs e)
        {
            if (Session["USERID"] != null)
            {
                try
                {
                    DataTable dtexists = new DataTable();
                    dtexists = objMainClass.GetCheckListData(objMainClass.intCmpId, 0, txtJobID.Text, "", "", "SELECTREPORTMSTBYJOBID");
                    if (dtexists.Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Check list already updated for this job id..!!');", true);
                    }
                    else
                    {
                        DataTable dtJobDtl = new DataTable();
                        dtJobDtl = objMainClass.SelectJobDetails(txtJobID.Text);
                        if (dtJobDtl.Rows.Count > 0)
                        {
                            if (Convert.ToString(dtJobDtl.Rows[0]["ITEMID"]) == "78754")
                            {
                                txtMake.Text = Convert.ToString(dtJobDtl.Rows[0]["PRODMAKE"]);
                                txtModel.Text = Convert.ToString(dtJobDtl.Rows[0]["PRODMODEL"]);
                                txtSRNo.Text = Convert.ToString(dtJobDtl.Rows[0]["IMEINO"]);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Job id not relate to Laptop..!!');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid job id..!!');", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
            }
        }
    }
}