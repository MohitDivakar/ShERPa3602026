using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.FI
{
    public partial class frmLedger : System.Web.UI.Page
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
                        if (FormRights.bView == false) //if (objDALUserRights.bView == false)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }

                        int month = DateTime.Now.Month;
                        int year = DateTime.Now.Year;

                        if (month < 4)
                        {
                            year = year - 1;
                        }

                        // if month is october or later, the FY started 10-1 of this year
                        // else it started 10-1 of last year
                        //return month > 9 ? new DateTime(year, 10, 1) : new DateTime(year - 1, 10, 1);

                        //int month = DateTime.Now.Month;
                        //int year = DateTime.Now.Year;

                        // if month is october or later, the FY ends 9/30 next year
                        // else it ends 9-30 of this year

                        string startdt = "01-04-" + year;
                        txtFromDate.Text = startdt;//objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        txtToDate.Text = (Convert.ToDateTime(startdt).AddYears(1).AddSeconds(-1)).ToString("dd-MM-yyyy");//objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();

                        objBindDDL.FillVendor(ddlVendor);


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

        protected void lnkSerch_Click(object sender, EventArgs e)
        {

            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetVendorLedger(objMainClass.intCmpId, ddlVendor.SelectedValue, txtFromDate.Text, txtToDate.Text, "GETLEDGER");

                    if (dt.Rows.Count > 0)
                    {




                        //gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                        decimal drAMT = 0;
                        decimal crAMT = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            crAMT = crAMT + Convert.ToDecimal(dt.Rows[i]["CR"]);
                            drAMT = drAMT + Convert.ToDecimal(dt.Rows[i]["DR"]);
                        }
                        //gvList.FooterRow.Cells[3].Text = "Total";
                        //gvList.FooterRow.Cells[4].Text = crAMT.ToString("N2");
                        //gvList.FooterRow.Cells[5].Text = drAMT.ToString("N2");
                        //gvList.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                        //gvList.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                        //gvList.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;

                        //gvList.FooterRow.Cells[0].BackColor = System.Drawing.Color.IndianRed;
                        //gvList.FooterRow.Cells[1].BackColor = System.Drawing.Color.IndianRed;
                        //gvList.FooterRow.Cells[2].BackColor = System.Drawing.Color.IndianRed;
                        //gvList.FooterRow.Cells[3].BackColor = System.Drawing.Color.IndianRed;
                        //gvList.FooterRow.Cells[4].BackColor = System.Drawing.Color.IndianRed;
                        //gvList.FooterRow.Cells[5].BackColor = System.Drawing.Color.IndianRed;


                        //gvList.FooterRow.Cells[0].ForeColor = System.Drawing.Color.White;
                        //gvList.FooterRow.Cells[1].ForeColor = System.Drawing.Color.White;
                        //gvList.FooterRow.Cells[2].ForeColor = System.Drawing.Color.White;
                        //gvList.FooterRow.Cells[3].ForeColor = System.Drawing.Color.White;
                        //gvList.FooterRow.Cells[4].ForeColor = System.Drawing.Color.White;
                        //gvList.FooterRow.Cells[5].ForeColor = System.Drawing.Color.White;

                        dt.Rows.Add(1, "", "", DateTime.Now, DateTime.Now, "Total", crAMT, drAMT);
                        decimal closing = crAMT - drAMT;
                        if (closing >= 0)
                        {
                            dt.Rows.Add(1, "", "", DateTime.Now, DateTime.Now, "Closing Balance", closing, 0);
                        }
                        if (closing < 0)
                        {
                            dt.Rows.Add(1, "", "", DateTime.Now, DateTime.Now, "Closing Balance", 0, Math.Abs(closing));
                        }


                        gvList.DataSource = dt;
                        gvList.DataBind();
                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;


                        //lblClosing.Text = Convert.ToString(crAMT - drAMT);


                        //lblHeading.Visible = true;
                        //lblClosing.Visible = true;
                    }
                    else
                    {
                        gvList.DataSource = string.Empty;
                        gvList.DataBind();

                        lblHeading.Visible = false;
                        lblClosing.Visible = false;
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

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void lnkExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    string attachment = "attachment; filename=VendorLedger" + ddlVendor.SelectedItem.Text + ".xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vdn.ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    gvList.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}