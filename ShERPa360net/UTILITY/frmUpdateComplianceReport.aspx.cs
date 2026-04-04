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
    public partial class frmUpdateComplianceReport : System.Web.UI.Page
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
                            btnSave.Enabled = false;
                        }

                        txtDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        objBindDDL.FillPlant(ddlLocation);
                        BindQuestion();
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

        public void BindQuestion()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objMainClass.GetSafetyQuestion(1, "Compliance");
                if (dt.Rows.Count > 0)
                {
                    grvQuestions.DataSource = dt;
                    grvQuestions.DataBind();
                }
                else
                {
                    grvQuestions.DataSource = string.Empty;
                    grvQuestions.DataBind();
                }


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
                if (Session["USERID"] != null)
                {
                    if (ddlLocation.SelectedIndex > 0)
                    {
                        string PLantRights = string.Empty;
                        string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                        for (int i = 0; i < plantArray.Count(); i++)
                        {
                            if (plantArray[i].Trim() == ddlLocation.SelectedValue)
                            {
                                PLantRights = ddlLocation.SelectedValue;
                            }
                        }

                        if (PLantRights.Length > 0)
                        {

                        }
                        else
                        {
                            ddlLocation.SelectedValue = plantArray[0];
                            ddlLocation.Focus();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You do not have plant rights.');", true);
                            ddlLocation.SelectedValue = plantArray[0];
                            ddlLocation.Focus();
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

        public bool ValidatePlantCode()
        {
            bool Isplantvalidate = false;
            try
            {
                if (Session["USERID"] != null)
                {
                    string plantcode = ddlLocation.SelectedValue;
                    string[] plantArray = Convert.ToString(Session["PLANTCD"]).Split(',');
                    for (int i = 0; i < plantArray.Count(); i++)
                    {
                        if (plantArray[i].Trim() == plantcode)
                        {
                            Isplantvalidate = true;
                            break;
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
            return Isplantvalidate;
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (ValidatePlantCode())
                    {
                        //byte[] IMAGE1 = null;
                        //if (fuImage1 != null)
                        //{
                        //    if (fuImage1.HasFiles)
                        //    {
                        //        using (BinaryReader br = new BinaryReader(fuImage1.PostedFile.InputStream))
                        //        {
                        //            IMAGE1 = br.ReadBytes(fuImage1.PostedFile.ContentLength);
                        //        }
                        //    }
                        //}

                        //byte[] IMAGE2 = null;
                        //if (fuImage2 != null)
                        //{
                        //    if (fuImage2.HasFiles)
                        //    {
                        //        using (BinaryReader br = new BinaryReader(fuImage2.PostedFile.InputStream))
                        //        {
                        //            IMAGE2 = br.ReadBytes(fuImage2.PostedFile.ContentLength);
                        //        }
                        //    }
                        //}

                        //byte[] IMAGE3 = null;
                        //if (fuImage3 != null)
                        //{
                        //    if (fuImage3.HasFiles)
                        //    {
                        //        using (BinaryReader br = new BinaryReader(fuImage3.PostedFile.InputStream))
                        //        {
                        //            IMAGE3 = br.ReadBytes(fuImage3.PostedFile.ContentLength);
                        //        }
                        //    }
                        //}


                        int iResult = objMainClass.SPComplianceReport(txtDate.Text, ddlLocation.SelectedValue, txtInspectedBy.Text, Convert.ToString(Session["USERID"]), grvQuestions, "Compliance");//, IMAGE1, IMAGE2, IMAGE3);

                        if (iResult == 1)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Compliance Report updated successfully! \");$('.close').click(function(){window.location.href ='frmUpdateComplianceReport.aspx' });", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Compliance report not updated sucessfully!');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + "You don't have the access to this plant!" + "\");", true);
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

        protected void rblAnswer_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioButtonList rbl = sender as RadioButtonList;

            GridViewRow row = ((GridViewRow)((RadioButtonList)sender).NamingContainer);
            TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
            RequiredFieldValidator rfvRemarks = (RequiredFieldValidator)row.FindControl("rfvRemarks");
            Label lblID = (Label)row.FindControl("lblID");

            if (rbl.SelectedValue == "0")
            {
                //if (lblID.Text == "5" || lblID.Text == "6")
                //{
                //    txtRemarks.Visible = false;
                //    rfvRemarks.Visible = false;
                //    rfvRemarks.Enabled = false;
                //}
                //else
                //{
                txtRemarks.Visible = true;
                rfvRemarks.Visible = true;
                rfvRemarks.Enabled = true;
                rfvRemarks.Style.Add("display", "block");
                //  }
            }
            //else if (rbl.SelectedValue == "1" && (lblID.Text == "5" || lblID.Text == "6"))
            //{
            //    txtRemarks.Visible = true;
            //    rfvRemarks.Visible = true;
            //    rfvRemarks.Enabled = true;
            //    rfvRemarks.Style.Add("display", "block");
            //}
            else
            {
                txtRemarks.Visible = false;
                rfvRemarks.Visible = false;
                rfvRemarks.Enabled = false;
            }
        }

        protected void grvQuestions_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string pr = ((Label)e.Row.FindControl("lblPhotoReq")).Text;
                Label lblID = (Label)e.Row.FindControl("lblID");

                FileUpload fuImage = ((FileUpload)e.Row.FindControl("fuImage"));
                FileUpload fuImage1 = ((FileUpload)e.Row.FindControl("fuImage1"));
                FileUpload fuImage2 = ((FileUpload)e.Row.FindControl("fuImage2"));
                //string photoreq = e.Row.Cells[5].Text;//Convert.ToInt32(e.Row.Cells[5].Text == "" ? "0" : e.Row.Cells[5].Text);
                if (pr == "1")
                {
                    if (lblID.Text == "15")
                    {
                        e.Row.Cells[6].Visible = true;
                        fuImage.Visible = true;
                        fuImage1.Visible = true;
                        fuImage2.Visible = true;
                    }
                    else
                    {
                        e.Row.Cells[6].Visible = true;
                    }

                }
                else
                {
                    e.Row.Cells[6].Visible = false;
                }

                //Label lblID = (Label)e.Row.FindControl("lblID");
                //RadioButtonList rblAnswer = (RadioButtonList)e.Row.FindControl("rblAnswer");

                //if (lblID.Text == "5" || lblID.Text == "6")
                //{

                //    rblAnswer.SelectedValue = "0";

                //}
            }
        }
    }
}