using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.MM
{
    public partial class frmUploadPODoc : System.Web.UI.Page
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

        protected void txtPONo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    gvPOList.DataSource = string.Empty;
                    gvPOList.DataBind();
                    gvGRNList.DataSource = string.Empty;
                    gvGRNList.DataBind();

                    btnSaveDet.Enabled = true;
                    if (txtPONo.Text != null && txtPONo.Text != "" && txtPONo.Text != string.Empty)
                    {
                        if (txtGRNNo.Text != null && txtGRNNo.Text != "" && txtGRNNo.Text != string.Empty)
                        {
                            DataTable dtPBStatus = new DataTable();
                            dtPBStatus = objMainClass.CheckPOGRN(objMainClass.intCmpId, txtPONo.Text, txtGRNNo.Text, "CHECKPBSTATUS");

                            if (dtPBStatus.Rows.Count > 0)
                            {


                                if (Convert.ToString(dtPBStatus.Rows[0]["STATUSDESC"]) != "APPROVED")
                                {
                                    DataTable dt = new DataTable();
                                    dt = objMainClass.CheckPOGRN(objMainClass.intCmpId, txtPONo.Text, txtGRNNo.Text, "CHECKGRNDOC");
                                    if (dt.Rows.Count > 0)
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Documents Already Uploaded!');", true);
                                        gvPOList.DataSource = dt;
                                        gvPOList.DataBind();
                                        gvGRNList.DataSource = dt;
                                        gvGRNList.DataBind();
                                        DataTable dtPOGRN = new DataTable();
                                        dtPOGRN = objMainClass.CheckPOGRN(objMainClass.intCmpId, txtPONo.Text, txtGRNNo.Text, "CHECKPOGRN");

                                        if (dtPOGRN.Rows.Count > 0)
                                        {
                                            btnSaveDet.Enabled = true;
                                        }
                                        else
                                        {
                                            btnSaveDet.Enabled = false;
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Wrong PO/GRN No.!');", true);
                                            btnSaveDet.Enabled = false;
                                        }
                                    }
                                    else
                                    {
                                        DataTable dtPOGRN = new DataTable();
                                        dtPOGRN = objMainClass.CheckPOGRN(objMainClass.intCmpId, txtPONo.Text, txtGRNNo.Text, "CHECKPOGRN");

                                        if (dtPOGRN.Rows.Count > 0)
                                        {
                                            btnSaveDet.Enabled = true;
                                        }
                                        else
                                        {
                                            btnSaveDet.Enabled = false;
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Wrong PO/GRN No.!');", true);
                                            btnSaveDet.Enabled = false;
                                        }
                                    }
                                }
                                else
                                {
                                    btnSaveDet.Enabled = false;
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('PB Already Approved. You cannot update Documents!');", true);
                                    btnSaveDet.Enabled = false;
                                }
                            }
                            else
                            {
                                DataTable dt = new DataTable();
                                dt = objMainClass.CheckPOGRN(objMainClass.intCmpId, txtPONo.Text, txtGRNNo.Text, "CHECKGRNDOC");
                                if (dt.Rows.Count > 0)
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Documents Already Uploaded!');", true);
                                    gvPOList.DataSource = dt;
                                    gvPOList.DataBind();
                                    gvGRNList.DataSource = dt;
                                    gvGRNList.DataBind();
                                    DataTable dtPOGRN = new DataTable();
                                    dtPOGRN = objMainClass.CheckPOGRN(objMainClass.intCmpId, txtPONo.Text, txtGRNNo.Text, "CHECKPOGRN");

                                    if (dtPOGRN.Rows.Count > 0)
                                    {
                                        btnSaveDet.Enabled = true;
                                    }
                                    else
                                    {
                                        btnSaveDet.Enabled = false;
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Wrong PO/GRN No.!');", true);
                                        btnSaveDet.Enabled = false;
                                    }
                                }
                                else
                                {
                                    DataTable dtPOGRN = new DataTable();
                                    dtPOGRN = objMainClass.CheckPOGRN(objMainClass.intCmpId, txtPONo.Text, txtGRNNo.Text, "CHECKPOGRN");

                                    if (dtPOGRN.Rows.Count > 0)
                                    {
                                        btnSaveDet.Enabled = true;
                                    }
                                    else
                                    {
                                        btnSaveDet.Enabled = false;
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Wrong PO/GRN No.!');", true);
                                        btnSaveDet.Enabled = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            DataTable dtPO = new DataTable();
                            dtPO = objMainClass.CheckPOGRN(objMainClass.intCmpId, txtPONo.Text, txtGRNNo.Text, "CHECKPO");

                            if (dtPO.Rows.Count > 0)
                            {
                                btnSaveDet.Enabled = true;
                            }
                            else
                            {
                                btnSaveDet.Enabled = false;
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Wrong PO No.!');", true);
                                btnSaveDet.Enabled = false;
                            }
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

        protected void txtGRNNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    gvPOList.DataSource = string.Empty;
                    gvPOList.DataBind();
                    gvGRNList.DataSource = string.Empty;
                    gvGRNList.DataBind();

                    if (txtGRNNo.Text != null && txtGRNNo.Text != "" && txtGRNNo.Text != string.Empty)
                    {
                        if (txtPONo.Text != null && txtPONo.Text != "" && txtPONo.Text != string.Empty)
                        {
                            DataTable dtPBStatus = new DataTable();
                            dtPBStatus = objMainClass.CheckPOGRN(objMainClass.intCmpId, txtPONo.Text, txtGRNNo.Text, "CHECKPBSTATUS");
                            if (dtPBStatus.Rows.Count > 0)
                            {


                                if (Convert.ToString(dtPBStatus.Rows[0]["STATUSDESC"]) != "APPROVED")
                                {
                                    DataTable dt = new DataTable();
                                    dt = objMainClass.CheckPOGRN(objMainClass.intCmpId, txtPONo.Text, txtGRNNo.Text, "CHECKGRNDOC");

                                    if (dt.Rows.Count > 0)
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Documents Already Uploaded!');", true);
                                        gvPOList.DataSource = dt;
                                        gvPOList.DataBind();
                                        gvGRNList.DataSource = dt;
                                        gvGRNList.DataBind();

                                        DataTable dtPOGRN = new DataTable();
                                        dtPOGRN = objMainClass.CheckPOGRN(objMainClass.intCmpId, txtPONo.Text, txtGRNNo.Text, "CHECKPOGRN");

                                        if (dtPOGRN.Rows.Count > 0)
                                        {
                                            btnSaveDet.Enabled = true;
                                        }
                                        else
                                        {
                                            btnSaveDet.Enabled = false;
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Wrong PO/GRN No.!');", true);
                                            btnSaveDet.Enabled = false;
                                        }
                                    }
                                    else
                                    {
                                        DataTable dtPOGRN = new DataTable();
                                        dtPOGRN = objMainClass.CheckPOGRN(objMainClass.intCmpId, txtPONo.Text, txtGRNNo.Text, "CHECKPOGRN");

                                        if (dtPOGRN.Rows.Count > 0)
                                        {
                                            btnSaveDet.Enabled = true;
                                        }
                                        else
                                        {
                                            btnSaveDet.Enabled = false;
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Wrong PO/GRN No.!');", true);
                                            btnSaveDet.Enabled = false;
                                        }
                                    }
                                }
                                else
                                {
                                    btnSaveDet.Enabled = false;
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('PB Already Approved. You cannot update Documents!');", true);
                                    btnSaveDet.Enabled = false;
                                }
                            }
                            else
                            {
                                DataTable dt = new DataTable();
                                dt = objMainClass.CheckPOGRN(objMainClass.intCmpId, txtPONo.Text, txtGRNNo.Text, "CHECKGRNDOC");

                                if (dt.Rows.Count > 0)
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Documents Already Uploaded!');", true);
                                    gvPOList.DataSource = dt;
                                    gvPOList.DataBind();
                                    gvGRNList.DataSource = dt;
                                    gvGRNList.DataBind();

                                    DataTable dtPOGRN = new DataTable();
                                    dtPOGRN = objMainClass.CheckPOGRN(objMainClass.intCmpId, txtPONo.Text, txtGRNNo.Text, "CHECKPOGRN");

                                    if (dtPOGRN.Rows.Count > 0)
                                    {
                                        btnSaveDet.Enabled = true;
                                    }
                                    else
                                    {
                                        btnSaveDet.Enabled = false;
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Wrong PO/GRN No.!');", true);
                                        btnSaveDet.Enabled = false;
                                    }
                                }
                                else
                                {
                                    DataTable dtPOGRN = new DataTable();
                                    dtPOGRN = objMainClass.CheckPOGRN(objMainClass.intCmpId, txtPONo.Text, txtGRNNo.Text, "CHECKPOGRN");

                                    if (dtPOGRN.Rows.Count > 0)
                                    {
                                        btnSaveDet.Enabled = true;
                                    }
                                    else
                                    {
                                        btnSaveDet.Enabled = false;
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Wrong PO/GRN No.!');", true);
                                        btnSaveDet.Enabled = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            DataTable dtGRN = new DataTable();
                            dtGRN = objMainClass.CheckPOGRN(objMainClass.intCmpId, txtPONo.Text, txtGRNNo.Text, "CHECKGRNNO");

                            if (dtGRN.Rows.Count > 0)
                            {
                                btnSaveDet.Enabled = true;
                            }
                            else
                            {
                                btnSaveDet.Enabled = false;
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Wrong GRN No.!');", true);
                                btnSaveDet.Enabled = false;
                            }
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

        protected void btnSaveDet_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    byte[] bytes;
                    string extension = ".jpeg";

                    using (BinaryReader br = new BinaryReader(fuInvDoc.PostedFile.InputStream))
                    {
                        bytes = br.ReadBytes(fuInvDoc.PostedFile.ContentLength);
                        extension = System.IO.Path.GetExtension(fuInvDoc.FileName);
                    }

                    byte[] PObytes;
                    string POextension = ".jpeg";
                    using (BinaryReader br = new BinaryReader(fuPODoc.PostedFile.InputStream))
                    {
                        PObytes = br.ReadBytes(fuPODoc.PostedFile.ContentLength);
                        POextension = System.IO.Path.GetExtension(fuPODoc.FileName);
                    }


                    int i = objMainClass.UploadPODoc(txtPONo.Text, txtGRNNo.Text, bytes, extension, PObytes, POextension);

                    if (i == 1)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Documents uploaded sucessfully!');$('.close').click(function(){window.location.href ='frmUploadPODoc.aspx' });", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Documents not uploaded sucessfully!');", true);
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

        protected void lnkPOInv_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

                    string extension = ((Label)grdrow.FindControl("lblPOEXTENSION")).Text;
                    string pono = ((Label)grdrow.FindControl("lblPONO")).Text;
                    string grnno = ((Label)grdrow.FindControl("lblDOCNO")).Text;
                    if (extension != null && extension != "" && extension != string.Empty)
                    {
                        string url = "ViewPOInvoice.aspx?PONO=" + pono + "&GRNNO=" + grnno;
                        string s = "window.open('" + url + "', 'popup_window', 'width=500,height=500,left=500,top=100,resizable=yes');";
                        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invoice not Uploaded for this MR!');", true);
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

        protected void Invoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

                    string extension = ((Label)grdrow.FindControl("lblEXTENSION")).Text;
                    string pono = ((Label)grdrow.FindControl("lblPONO")).Text;
                    string grnno = ((Label)grdrow.FindControl("lblDOCNO")).Text;
                    if (extension != null && extension != "" && extension != string.Empty)
                    {
                        string url = "ViewGRNInvoice.aspx?PONO=" + pono + "&GRNNO=" + grnno;
                        string s = "window.open('" + url + "', 'popup_window', 'width=500,height=500,left=500,top=100,resizable=yes');";
                        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invoice not Uploaded for this MR!');", true);
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
}