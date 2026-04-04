using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.SD
{
    public partial class frmSOSIUpdate : System.Web.UI.Page
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

        protected void lnkSearh_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();

                    if (rblSOSI.SelectedValue == "SO")
                    {
                        dt = objMainClass.GetSISOData(objMainClass.intCmpId, txtSOSINO.Text, "", "SOSEARCH");
                    }
                    else if (rblSOSI.SelectedValue == "SI")
                    {
                        dt = objMainClass.GetSISOData(objMainClass.intCmpId, "", txtSOSINO.Text, "SISEARCH");
                    }

                    if (dt.Rows.Count > 0)
                    {
                        lblSIType.Text = Convert.ToString(dt.Rows[0]["SITYPE"]);
                        lblSINO.Text = Convert.ToString(dt.Rows[0]["SINO"]);
                        lblSIDT.Text = Convert.ToString(dt.Rows[0]["SIDT"]);
                        lblSICreateBy.Text = Convert.ToString(dt.Rows[0]["SICREATEBY"]);
                        lblSICreateDt.Text = Convert.ToString(dt.Rows[0]["SICREATEDATE"]);
                        lblSIRemarks.Text = Convert.ToString(dt.Rows[0]["SIREMARK"]);
                        lblSOType.Text = Convert.ToString(dt.Rows[0]["SOTYPE"]);
                        lblSONO.Text = Convert.ToString(dt.Rows[0]["SONO"]);
                        lblSORefNo.Text = Convert.ToString(dt.Rows[0]["SOREFNO"]);
                        lblSOCreateBy.Text = Convert.ToString(dt.Rows[0]["SOCREATEBY"]);
                        lblSOCreateDt.Text = Convert.ToString(dt.Rows[0]["SOCREATEDATE"]);
                        lblSORemarks.Text = Convert.ToString(dt.Rows[0]["SOREMARK"]);
                        lblTotalAmt.Text = Convert.ToString(dt.Rows[0]["NETMATVALUE"]);
                        lblDiscount.Text = Convert.ToString(dt.Rows[0]["DISCOUNT"]);
                        lblNetAmt.Text = Convert.ToString(dt.Rows[0]["NETSOAMT"]);
                        txtCustName.Text = Convert.ToString(dt.Rows[0]["CUSTNAME"]);
                        txtMobileNo.Text = Convert.ToString(dt.Rows[0]["CUSTMOBILENO"]);
                        txtEmail.Text = Convert.ToString(dt.Rows[0]["CUSTEMAILID"]);
                        txtAdd1.Text = Convert.ToString(dt.Rows[0]["CUSTADD1"]);
                        txtAdd2.Text = Convert.ToString(dt.Rows[0]["CUSTADD2"]);
                        txtAdd3.Text = Convert.ToString(dt.Rows[0]["CUSTADD3"]);
                        txtPincode.Text = Convert.ToString(dt.Rows[0]["PINCODE"]);
                        txtGSTNO.Text = Convert.ToString(dt.Rows[0]["GSTNO"]);


                        objBindDDL.FillState(ddlState);
                        ddlState.SelectedValue = Convert.ToString(dt.Rows[0]["STATEID"]);
                        objBindDDL.FillCity(ddlCity, ddlState.SelectedValue);
                        ddlCity.SelectedValue = ddlCity.Items.FindByText(Convert.ToString(dt.Rows[0]["CITY"])).Value;



                        gvList.DataSource = dt;
                        gvList.DataBind();
                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;

                        divData.Visible = true;
                    }
                    else
                    {

                        gvList.DataSource = string.Empty;
                        gvList.DataBind();

                        divData.Visible = false;

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('SI/SO number is wrong or SI/SO Cancelled or Returned.!');", true);
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

        protected void txtPincode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (txtPincode.Text.Length == 6)
                    {
                        try
                        {

                            if (!System.Text.RegularExpressions.Regex.IsMatch(txtPincode.Text, "[0-9]{6}$"))
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Only Numbers Aloowed.');", true);
                                txtPincode.Text = string.Empty;
                            }
                            else
                            {
                                DataTable ds = new DataTable();
                                ds = objMainClass.SELECT_CITY_BYPINCODE(txtPincode.Text.Trim());
                                if (ds.Rows.Count > 0)
                                {
                                    ddlState.SelectedValue = ds.Rows[0]["STATE_ID"].ToString();
                                    objBindDDL.FillCity(ddlCity, ddlState.SelectedValue);
                                    ddlCity.SelectedValue = ddlCity.Items.FindByText(ds.Rows[0]["CITY_NAME"].ToString()).Value;
                                }
                                else
                                {
                                    ddlState.SelectedIndex = 0;
                                    ddlCity.SelectedIndex = 0;
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

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    string SISONO = objMainClass.UpdateSISOAddress(objMainClass.intCmpId, lblSONO.Text, txtCustName.Text, txtAdd1.Text, txtAdd2.Text, txtAdd3.Text, ddlCity.SelectedItem.Text,
                        Convert.ToInt32(ddlState.SelectedValue), txtPincode.Text, txtMobileNo.Text == "" ? "0" : txtMobileNo.Text, txtEmail.Text, Convert.ToInt32(Session["USERID"]), "UPDATESOADDRESS", txtGSTNO.Text, gvList);
                    if (SISONO != "" && SISONO != string.Empty && SISONO != null)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('SO/SI updated successfully.');$('.close').click(function(){window.location.href ='frmSOSIUpdate.aspx' });", true);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('SI/SO not updated. Something went wrong.!');", true);
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

        protected void lnkRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    Response.Redirect("frmSOSIUpdate.aspx", true);
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

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (ddlState.SelectedIndex > 0)
                    {
                        objBindDDL.FillCity(ddlCity, ddlState.SelectedValue);
                    }
                    else
                    {
                        ddlCity.DataSource = string.Empty;
                        ddlCity.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please Select State.!');", true);
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