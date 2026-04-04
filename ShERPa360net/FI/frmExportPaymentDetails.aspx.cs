using ClosedXML.Excel;
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
    public partial class frmExportPaymentDetails : System.Web.UI.Page
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

                        GetData();

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

        public void GetData()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.ExportPaymentEntry(objMainClass.intCmpId, "PAYMENTEXPORT");

                    if (dt.Rows.Count > 0)
                    {
                        gvList.DataSource = dt;
                        gvList.DataBind();

                        decimal amt = 0;
                        decimal drcr = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (Convert.ToString(dt.Rows[i]["DOCAMT"]) != "" && Convert.ToString(dt.Rows[i]["DOCAMT"]) != null && Convert.ToString(dt.Rows[i]["DOCAMT"]) != string.Empty)
                            {
                                amt = amt + Convert.ToDecimal(dt.Rows[i]["DOCAMT"]);
                            }


                            if (Convert.ToString(dt.Rows[i]["CRDR"]) == "CR")
                            {
                                drcr = drcr + Convert.ToDecimal(dt.Rows[i]["ADJTB"]);
                            }
                            else
                            {
                                drcr = drcr - Convert.ToDecimal(dt.Rows[i]["ADJTB"]);
                            }
                        }



                        gvList.FooterRow.Cells[2].Text = "Total";
                        gvList.FooterRow.Cells[3].Text = amt.ToString("N2");
                        gvList.FooterRow.Cells[5].Text = drcr.ToString("N2");
                        gvList.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                        gvList.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                        gvList.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;


                        gvList.HeaderRow.Cells[0].ForeColor = System.Drawing.Color.White;
                        gvList.HeaderRow.Cells[1].ForeColor = System.Drawing.Color.White;
                        gvList.HeaderRow.Cells[2].ForeColor = System.Drawing.Color.White;
                        gvList.HeaderRow.Cells[3].ForeColor = System.Drawing.Color.White;
                        gvList.HeaderRow.Cells[4].ForeColor = System.Drawing.Color.White;
                        gvList.HeaderRow.Cells[5].ForeColor = System.Drawing.Color.White;
                        gvList.HeaderRow.Cells[6].ForeColor = System.Drawing.Color.White;
                        gvList.HeaderRow.Cells[7].ForeColor = System.Drawing.Color.White;
                        gvList.HeaderRow.Cells[8].ForeColor = System.Drawing.Color.White;


                        gvList.HeaderRow.Cells[0].BackColor = System.Drawing.Color.Indigo;
                        gvList.HeaderRow.Cells[1].BackColor = System.Drawing.Color.Indigo;
                        gvList.HeaderRow.Cells[2].BackColor = System.Drawing.Color.Indigo;
                        gvList.HeaderRow.Cells[3].BackColor = System.Drawing.Color.Indigo;
                        gvList.HeaderRow.Cells[4].BackColor = System.Drawing.Color.Indigo;
                        gvList.HeaderRow.Cells[5].BackColor = System.Drawing.Color.Indigo;
                        gvList.HeaderRow.Cells[6].BackColor = System.Drawing.Color.Indigo;
                        gvList.HeaderRow.Cells[7].BackColor = System.Drawing.Color.Indigo;
                        gvList.HeaderRow.Cells[8].BackColor = System.Drawing.Color.Indigo;


                        gvList.FooterRow.Cells[0].BackColor = System.Drawing.Color.IndianRed;
                        gvList.FooterRow.Cells[1].BackColor = System.Drawing.Color.IndianRed;
                        gvList.FooterRow.Cells[2].BackColor = System.Drawing.Color.IndianRed;
                        gvList.FooterRow.Cells[3].BackColor = System.Drawing.Color.IndianRed;
                        gvList.FooterRow.Cells[4].BackColor = System.Drawing.Color.IndianRed;
                        gvList.FooterRow.Cells[5].BackColor = System.Drawing.Color.IndianRed;
                        gvList.FooterRow.Cells[6].BackColor = System.Drawing.Color.IndianRed;
                        gvList.FooterRow.Cells[7].BackColor = System.Drawing.Color.IndianRed;
                        gvList.FooterRow.Cells[8].BackColor = System.Drawing.Color.IndianRed;


                        gvList.FooterRow.Cells[0].ForeColor = System.Drawing.Color.White;
                        gvList.FooterRow.Cells[1].ForeColor = System.Drawing.Color.White;
                        gvList.FooterRow.Cells[2].ForeColor = System.Drawing.Color.White;
                        gvList.FooterRow.Cells[3].ForeColor = System.Drawing.Color.White;
                        gvList.FooterRow.Cells[4].ForeColor = System.Drawing.Color.White;
                        gvList.FooterRow.Cells[5].ForeColor = System.Drawing.Color.White;
                        gvList.FooterRow.Cells[6].ForeColor = System.Drawing.Color.White;
                        gvList.FooterRow.Cells[7].ForeColor = System.Drawing.Color.White;
                        gvList.FooterRow.Cells[8].ForeColor = System.Drawing.Color.White;

                        Session["PaymentEntry"] = dt;
                    }

                    else
                    {
                        gvList.DataSource = string.Empty;
                        gvList.DataBind();
                        Session["PaymentEntry"] = dt;
                        Session["PaymentEntry"] = null;
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

        protected void lnkDownLoadAll_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["USERID"] != null)
                {



                    string attachment = "attachment; filename=Payment Entry " + DateTime.Now + ".xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vdn.ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    gvList.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();


                    //DataTable PaymentEntry = (DataTable)Session["PaymentEntry"];


                    //DataSet ds = new DataSet();
                    //if (PaymentEntry.Rows.Count > 0)
                    //{
                    //    PaymentEntry.TableName = "Lead Genereated";
                    //    ds.Tables.Add(PaymentEntry);
                    //}


                    //using (XLWorkbook wb = new XLWorkbook())
                    //{
                    //    foreach (DataTable dt in ds.Tables)
                    //    {
                    //        wb.Worksheets.Add(dt);
                    //    }

                    //    //Export the Excel file.
                    //    Response.Clear();
                    //    Response.Buffer = true;
                    //    Response.Charset = "";
                    //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    //    Response.AddHeader("content-disposition", "attachment;filename=Payment Entry" + DateTime.Now + ".xlsx");
                    //    using (MemoryStream MyMemoryStream = new MemoryStream())
                    //    {
                    //        wb.SaveAs(MyMemoryStream);
                    //        MyMemoryStream.WriteTo(Response.OutputStream);
                    //        Response.Flush();
                    //        Response.End();
                    //    }
                    //}

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