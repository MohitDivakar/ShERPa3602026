using Newtonsoft.Json;
using ShERPa360net.Class;
using ShERPa360net.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.UTILITY
{
    public partial class PickedUpProductReturn : System.Web.UI.Page
    {
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        bool Checkerright = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtFromDate.Text = (objMainClass.indianTime.Date.AddDays(-30)).ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    BindPageDropDown();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public void BindPageDropDown()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    objBindDDL.FillMobexSellerVendor(ddlVendor);
                    ddlVendor.SelectedValue = "0";
                    objBindDDL.FillLists(ddllistype, "LTP");
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


        protected void lnkSearh_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    BindProductDetail();
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

        public void BindProductDetail()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    int status;
                    status = Convert.ToInt32(ddlStatus.SelectedValue);
                    gvProduct.DataSource = null;
                    gvProduct.DataBind();
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetInwardedProductEntryDetail(txtFromDate.Text, txtToDate.Text, (ddlVendor.SelectedValue == "0" ? "" : ddlVendor.SelectedItem.Text), status, Convert.ToInt32(Session["USERID"].ToString()),  txtIMEINumber.Text);
                    gvProduct.DataSource = dt;
                    gvProduct.DataBind();
                    if (gvProduct.Rows.Count > 0)
                    {
                        gvProduct.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                    if (status == 11301)
                    {
                        gvProduct.Columns[0].Visible = false;
                        gvProduct.Columns[2].Visible = false;
                    }

                    lgrecordcount.InnerText = "Records : " + dt.Rows.Count.ToString();
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

        public void ResetFormControl()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    txtFromDate.Text = (objMainClass.indianTime.Date.AddDays(-30)).ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    ddlVendor.SelectedValue = "0";
                    ddllistype.SelectedValue = "0";
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
        protected void imgReset_Click(object sender, EventArgs e)
        {
            try
            {
                ResetFormControl();
                BindProductDetail();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                int _min = 1000;
                int _max = 9999;
                Random _rdm = new Random();
                int iRandomNumber = _rdm.Next(_min, _max);
                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                hdprimarykey.Value = ((HiddenField)grdrow.FindControl("hdID")).Value;
                txtMake.Text = ((Label)grdrow.FindControl("lblMake")).Text;
                txtModel.Text = ((Label)grdrow.FindControl("lblModel")).Text;
                txtRom.Text = ((Label)grdrow.FindControl("lblRom")).Text;
                txtRam.Text = ((Label)grdrow.FindControl("lblRam")).Text;
                txtColor.Text = ((Label)grdrow.FindControl("lblColor")).Text;
                txtVendorDetail.Text = ((Label)grdrow.FindControl("lblVendorName")).Text;
                txtIMEINo.Text = ((Label)grdrow.FindControl("lblIMEINo")).Text;
                hdBikerContactNo.Value = ((Label)grdrow.FindControl("lblBikerContactNo")).Text;
                hdAsmContactNo.Value = ((Label)grdrow.FindControl("lblAsmContactNo")).Text;
                hdJpBhaiContactNo.Value = ((Label)grdrow.FindControl("lblJpBhaiContactNo")).Text;
                hdVendorID.Value = ((HiddenField)grdrow.FindControl("hdVendorId")).Value;
                hdDealerContactNo.Value = ((Label)grdrow.FindControl("lblDealerContactNo")).Text;
                hdDealerContactNo2.Value = ((Label)grdrow.FindControl("lblDealerContactNo2")).Text;
                hdDealerContactNo3.Value = ((Label)grdrow.FindControl("lblDealerContactNo3")).Text;

                WAClass objSendSMS = new WAClass();
                string textMessage = "Your One Time Password is " + iRandomNumber.ToString() + ". Please use this OTP to login. Do not share this OTP to anyone for security reasons.\nMobex";

                //Send To BDO SMS Detail
                if (hdDealerContactNo.Value.Length > 0)
                {
                    Session["VENDOROTP"] = iRandomNumber.ToString();
                    objSendSMS.SendTextMessage(textMessage, "91" + hdDealerContactNo.Value, Session["USERID"].ToString());

                    //SMSSend.SendSMS(hdDealerContactNo.Value, textMessage);
                    //objSendSMS.SendTextMessage(textMessage, "91" + hdDealerContactNo.Value, Session["USERID"].ToString());
                }

                if (hdDealerContactNo2.Value.Length > 0)
                {
                    Session["VENDOROTP"] = iRandomNumber.ToString();
                    objSendSMS.SendTextMessage(textMessage, "91" + hdDealerContactNo2.Value, Session["USERID"].ToString());

                   // SMSSend.SendSMS(hdDealerContactNo2.Value, textMessage);
                }

                if (hdDealerContactNo3.Value.Length > 0)
                {
                    Session["VENDOROTP"] = iRandomNumber.ToString();
                    objSendSMS.SendTextMessage(textMessage, "91" + hdDealerContactNo3.Value, Session["USERID"].ToString());

                    //SMSSend.SendSMS(hdDealerContactNo3.Value, textMessage);
                }

               
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                //txtReturnReason.Focus();
                txtHandoverReturnBDOOTP.Focus();
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        //update return reason
        protected void btnSaveReturn_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtHandoverReturnBDOOTP.Text == Session["VENDOROTP"].ToString())
                {
                    int ID = Convert.ToInt32(hdprimarykey.Value);
                    int Status = Convert.ToInt32(PRODUCTSTATUS.RETURNED);
                    objMainClass.UpdateReturnVendorBDOStatus(txtReturnReason.Text,Status, Convert.ToInt32(Session["USERID"]), ID);
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Return Request Successfully Handover to Vendor." + "\");", true);
                        BindProductDetail();
                    }
                }
                else
                {
                    lblvalidotpalert.Visible = true;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                }
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}