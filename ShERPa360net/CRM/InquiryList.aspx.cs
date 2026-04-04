using ShERPa360net.Class;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace ShERPa360net.CRM
{
    public partial class InquiryList : System.Web.UI.Page
    {
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        DALInquiry objDALInquiry = new DALInquiry();

        #region PageLoadEvent
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["USERID"] != null)
                    {
                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menutabid.Value, "");
                        if (FormRights.bView == false) //if (objDALUserRights.bView == false)
                        {
                            Session.Abandon();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='HomePage.aspx' });", true);
                            return;
                        }
                        if (FormRights.bAdd == false) //if (objDALUserRights.bView == false)
                        {
                            btnNew.Enabled = false;
                        }
                        objBindDDL.FillSegment(ddlSegment);
                        ddlSegment.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                        objBindDDL.FillStatus(ddlStatus, 4);
                        ddlStatus.SelectedIndex = 1;
                        ddlSegment.SelectedIndex = 4;
                        btnNew.Visible = FormRights.bAdd == true ? true : false;
                        txtFromDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); // DateTime.Now.ToString("dd/MM/yyyy");
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //DateTime.Now.ToString("dd/MM/yyyy");
                        FillInquiryList();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                    }

                    if (gvList.Rows.Count > 0)
                    {
                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }
                else
                {
                    if (gvList.Rows.Count > 0)
                    {
                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Method
        protected void FillInquiryList()
        {
            try
            {
                string strFromDt = txtFromDate.Text;
                string strToDt = txtToDate.Text;
                gvList.DataSource = objDALInquiry.SearchInquiryList(ddlSegment.SelectedValue.ToString(), txtInquiryNo.Text, "INQ", int.Parse(ddlStatus.SelectedValue.ToString())
                    , "57,58,59,64,65,66", txtCustomer.Text, txtContactNo.Text.Trim(), txtCardNo.Text, strFromDt, strToDt, chkPending.Checked,
                    false, int.Parse(Session["USERID"].ToString()));
                gvList.DataBind();
                gvList.HeaderRow.TableSection = TableRowSection.TableHeader;

                if (ddlSegment.SelectedValue == "1002" || ddlSegment.SelectedValue == "1003" || ddlSegment.SelectedValue == "1005" || ddlSegment.SelectedValue == "1019" || ddlSegment.SelectedValue == "1099")
                {
                    gvList.Columns[6].Visible = true;
                    //gvList.Columns[7].Visible = true;
                }
                else
                {
                    gvList.Columns[6].Visible = false;
                    //gvList.Columns[7].Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ButtonEvent
        protected void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("Inquiry.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                FillInquiryList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnAssign_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grdrow = (GridViewRow)((Button)sender).NamingContainer;
                string strInqNo = grdrow.Cells[2].Text;

                if (objDALInquiry.CheckAssignedInqNo(strInqNo) == "")
                {
                    string url = "AssignPickup.aspx?InqNo=" + strInqNo;
                    string s = "window.open('" + url + "', '_blank');";
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "script", s, true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Pickup is already assigned.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message.Replace("\r\n", "") + "\");", true);
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                string strInqNo = grdrow.Cells[2].Text;
                string url = "Inquiry.aspx?InqNo=" + strInqNo;
                string s = "window.open('" + url + "', '_blank');";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "script", s, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        //protected void btnCreateJobsheet_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        GridViewRow grdrow = (GridViewRow)((Button)sender).NamingContainer;
        //        //string strInqNo = grdrow.Cells[1].Text;
        //        //DALJobsheet objDALJobsheet = new DALJobsheet();
        //        //string strJobId = objDALJobsheet.CheckInqInJob(strInqNo);
        //        //if (string.IsNullOrEmpty(strJobId))
        //        //{
        //        //    string url = "Jobsheet.aspx?InqNo=" + strInqNo;
        //        //    string s = "window.open('" + url + "', '_blank');";
        //        //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "script", s, true);
        //        //}
        //        //else
        //        //{
        //        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Jobsheet is already created. Job No. is \"" + strJobId + "\" ');", true);
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message.Replace("\r\n", "") + "\");", true);
        //    }
        //}
    }
}