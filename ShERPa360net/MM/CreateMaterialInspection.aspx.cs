using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace ShERPa360net.MM
{
    public partial class CreateMaterialInspection : System.Web.UI.Page
    {
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        DataTable dtItem = new DataTable();
        public SqlConnection ConnSherpa = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStringSherpa"].ConnectionString);

        #region EVENTS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
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
                                imgSaveAll.Enabled = false;
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
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void txtDocNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //string strMsg = "Registration was done after 7 days of handset purchase. Do you want to still generate Inquiry ?";
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-confirm').modal();$('#lblConfirmMsg').text(\"" + strMsg + "\");", true);
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.CheckDocNoExistForMaterialInspection(txtDocNo.Text, "103", objMainClass.intCmpId, "CHECKDOCISEXIST");
                    if (dt.Rows.Count > 0)
                    {
                        BindGrid();
                    }
                    else
                    {
                        grvListItem.DataSource = null;
                        grvListItem.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Invalid Document No.');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void imgSaveAll_Click(object sender, EventArgs e)
        {
            MainClass objMainClass = new MainClass();
            int result = 0;
            try
            {
                if (Session["USERID"] != null)
                {
                    if(grvListItem.Rows.Count > 0)
                    {
                        if (IsGridValidateQty())
                        {
                            using (TransactionScope scope = new TransactionScope())
                            {
                                for (int i = 0; i < grvListItem.Rows.Count; i++)
                                {
                                    GridViewRow row = grvListItem.Rows[i];
                                    string lblQtyValue = ((Label)row.FindControl("lblQty")).Text;
                                    string txtAcceptedQtyValue = ((TextBox)row.FindControl("txtAcceptedQty")).Text;
                                    string DOCNO = ((Label)row.FindControl("lblDocumentNo")).Text;
                                    string SrNo = ((Label)row.FindControl("lblSrNo")).Text;
                                    string DOCTYPE = "103";
                                    Decimal Qty, AcceptedQty = 0;
                                    Decimal.TryParse(lblQtyValue, out Qty);
                                    Decimal.TryParse(txtAcceptedQtyValue, out AcceptedQty);

                                    //crudoperation
                                    SqlCommand cmdD = new SqlCommand("SP_MATERIALINSPECTIONCRUDOPERATION", ConnSherpa);
                                    cmdD.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                                    cmdD.Parameters.AddWithValue("@DOCNO", objMainClass.strConvertZeroPadding(DOCNO));
                                    cmdD.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                                    cmdD.Parameters.AddWithValue("@SRNO", SrNo);
                                    cmdD.Parameters.AddWithValue("@ACPTQTY", AcceptedQty);
                                    cmdD.Parameters.AddWithValue("@USERID", Convert.ToInt64(Convert.ToString(Session["USERID"])));
                                    cmdD.Parameters.AddWithValue("@Action", "UPDATE");
                                    cmdD.CommandType = CommandType.StoredProcedure;
                                    cmdD.Connection.Open();
                                    result = cmdD.ExecuteNonQuery();
                                    cmdD.Connection.Close();
                                    //crudoperation
                                }
                                scope.Complete();
                                scope.Dispose();
                            }
                            ResetFormControl();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Material inspection completed successfully.');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Accept Qty must not be more than challan Qty.');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('At Least One Item Required to Save Material Inspection.');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void lnkAcceptAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    for (int i = 0; i < grvListItem.Rows.Count; i++)
                    {
                        GridViewRow row     = grvListItem.Rows[i];
                        string lblQtyValue  = ((Label)row.FindControl("lblQty")).Text;
                        var txtAcceptedQty  = ((TextBox)row.FindControl("txtAcceptedQty"));
                        txtAcceptedQty.Text = lblQtyValue;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        } 
        #endregion

        #region METHOD
        public void BindGrid()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    var dtInwardedMaterialDetail = objMainClass.GetMaterialDetailForInspection(txtDocNo.Text, "103", objMainClass.intCmpId, "GETPOINWARDITEMDETAIL");
                    if(dtInwardedMaterialDetail.Rows.Count > 0)
                    {
                        grvListItem.DataSource = dtInwardedMaterialDetail;
                        grvListItem.DataBind();
                    }
                    else
                    {
                        grvListItem.DataSource = null;
                        grvListItem.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Material inspection already completed of Document No " + txtDocNo.Text + ".');", true);
                    }
                }
                else
                {
                    
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
        
        public void SetAcceptAllQty()
        {
            try
            {
                for (int i = 0; i < grvListItem.Rows.Count; i++)
                {
                    GridViewRow row = grvListItem.Rows[i];
                    string lblQtyValue = ((Label)row.FindControl("lblQty")).Text;
                    var txtAcceptedQty = ((TextBox)row.FindControl("txtAcceptedQty"));
                    txtAcceptedQty.Text = lblQtyValue;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public bool IsGridValidateQty()
        {
            bool IsGridValidate = true;
            try
            {
                for (int i = 0; i < grvListItem.Rows.Count; i++)
                {
                    GridViewRow row = grvListItem.Rows[i];
                    string lblQtyValue = ((Label)row.FindControl("lblQty")).Text;
                    string txtAcceptedQtyValue = ((TextBox)row.FindControl("txtAcceptedQty")).Text;
                    Decimal Qty, AcceptedQty = 0;
                    Decimal.TryParse(lblQtyValue, out Qty);
                    Decimal.TryParse(txtAcceptedQtyValue, out AcceptedQty);
                    if (AcceptedQty > Qty)
                    {
                        IsGridValidate = false;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                IsGridValidate = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return IsGridValidate;
        }

        public void ResetFormControl()
        {
            try
            {
                txtDocNo.Text = string.Empty;
                grvListItem.DataSource = null;
                grvListItem.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        } 
        #endregion
    }
}