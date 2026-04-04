using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.UTILITY
{
    public partial class frmASMAssign : System.Web.UI.Page
    {

        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        WAClass objWAClass = new WAClass();

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
                            btnAdd.Enabled    = false;
                            btnRemove.Enabled = false;
                            btnSave.Enabled   = false;
                        }

                        objBindDDL.FillSM(ddlASM, 1, (int)SMTYPE.ASM, "GETSM");

                        lblMappedBiker.Text     = "0";
                        lblUnMappedBiker.Text   = "0";
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


        protected void ddlASM_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    if (ddlASM.SelectedIndex > 0)
                    {

                        DataTable dtBDO = new DataTable();
                        DataTable dtNoneBDO = new DataTable();

                        dtBDO = objMainClass.GetBDOASMData(0, 1, (int)SMTYPE.ASM, Convert.ToInt32(ddlASM.SelectedValue), "GETASSIGNEDBDO");
                        if (dtBDO.Rows.Count > 0)
                        {
                            lstBoxAssign.DataSource = dtBDO;
                            lstBoxAssign.DataTextField = "BDO";
                            lstBoxAssign.DataValueField = "ID";
                            lstBoxAssign.DataBind();
                            Session["dtBDO"] = dtBDO;
                            lblMappedBiker.Text = dtBDO.Rows.Count.ToString();
                        }
                        else
                        {
                            lstBoxAssign.DataSource = string.Empty;
                            lstBoxAssign.DataBind();
                            Session["dtBDO"] = "";
                            lblMappedBiker.Text = "0";
                        }

                        dtNoneBDO = objMainClass.GetBDOASMData(0, 1, (int)SMTYPE.ASM, Convert.ToInt32(ddlASM.SelectedValue), "GETNONEBDO");
                        if (dtNoneBDO.Rows.Count > 0)
                        {
                            lstBoxAll.DataSource = dtNoneBDO;
                            lstBoxAll.DataTextField = "BDO";
                            lstBoxAll.DataValueField = "ID";
                            lstBoxAll.DataBind();
                            Session["dtNoneBDO"] = dtNoneBDO;

                            lblUnMappedBiker.Text = dtNoneBDO.Rows.Count.ToString();
                        }
                        else
                        {
                            lstBoxAll.DataSource = string.Empty;
                            lstBoxAll.DataBind();
                            Session["dtNoneBDO"] = null;
                            lblUnMappedBiker.Text = "0";
                        }
                    }
                    else
                    {
                        lstBoxAll.DataSource = string.Empty;
                        lstBoxAll.DataBind();
                        lstBoxAssign.DataSource = string.Empty;
                        lstBoxAssign.DataBind();
                        lblMappedBiker.Text = "0";
                        lblUnMappedBiker.Text = "0";
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {
                    if (lstBoxAll.Items.Count > 0)
                    {
                        if (lstBoxAll.GetSelectedIndices().Count() > 0)
                        {
                            int index = Convert.ToInt32(lstBoxAssign.Items.Count);
                            lstBoxAssign.Items.Insert(index, new ListItem(lstBoxAll.SelectedItem.Text, lstBoxAll.SelectedValue));
                            lstBoxAll.Items.RemoveAt(lstBoxAll.SelectedIndex);

                            lblMappedBiker.Text   = lstBoxAssign.Items.Count.ToString();
                            lblUnMappedBiker.Text = lstBoxAll.Items.Count.ToString();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Select BDO to Assign.');", true);
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

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (lstBoxAssign.Items.Count > 0)
                    {
                        if (lstBoxAssign.GetSelectedIndices().Count() > 0)
                        {
                            int index = Convert.ToInt32(lstBoxAll.Items.Count);
                            lstBoxAll.Items.Insert(index, new ListItem(lstBoxAssign.SelectedItem.Text, lstBoxAssign.SelectedValue));
                            lstBoxAssign.Items.RemoveAt(lstBoxAssign.SelectedIndex);

                            List<ListItem> list = new List<ListItem>(lstBoxAll.Items.Cast<ListItem>());
                            list = list.OrderBy(x => x.Text).ToList<ListItem>();

                            lstBoxAll.Items.Clear();
                            lstBoxAll.Items.AddRange(list.ToArray<ListItem>());

                            lblMappedBiker.Text = lstBoxAssign.Items.Count.ToString();
                            lblUnMappedBiker.Text = lstBoxAll.Items.Count.ToString();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Select BDO to Remove.');", true);
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    if (lstBoxAssign.Items.Count > 0)
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {

                            int iResult = objMainClass.UpdateASM(0, 1, Convert.ToInt32(ddlASM.SelectedValue), Convert.ToInt32(Session["USERID"]), "UPDATENULLASM");

                            if (iResult == 1)
                            {

                                int iReturn = 0;

                                for (int i = 0; i < lstBoxAssign.Items.Count; i++)
                                {
                                    iReturn = objMainClass.UpdateASM(Convert.ToInt32(lstBoxAssign.Items[i].Value), 1, Convert.ToInt32(ddlASM.SelectedValue), Convert.ToInt32(Session["USERID"]), "UPDATEASM");
                                }

                                if (iReturn == 1)
                                {
                                    scope.Complete();
                                    scope.Dispose();
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"BDO mapping updated successfully! \");$('.close').click(function(){window.location.href ='frmASMAssign.aspx' });", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('BDO mapping not updated. Please try again.!');", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('BDO mapping not updated. Please try again.!');", true);
                            }

                        }
                    }
                    else
                    {
                        int iResult = objMainClass.UpdateASM(0, 1, Convert.ToInt32(ddlASM.SelectedValue), Convert.ToInt32(Session["USERID"]), "UPDATENULLASM");

                        if (iResult == 1)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"BDO mapping updated successfully! \");$('.close').click(function(){window.location.href ='frmASMAssign.aspx' });", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('BDO mapping not updated. Please try again.!');", true);
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


    }
}