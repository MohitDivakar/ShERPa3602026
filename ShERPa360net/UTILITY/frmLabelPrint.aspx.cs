using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.UTILITY
{
    public partial class frmLabelPrint : System.Web.UI.Page
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

                        objBindDDL.FillSegmentNew(ddlSegment);

                        DataTable segmentDt = new DataTable();
                        segmentDt = objMainClass.SelectUserSegment(Convert.ToString(Session["USERID"]));
                        string segment = Convert.ToString(segmentDt.Rows[0]["SEGMENT"]);
                        string[] segmentArray = segment.Split(',');
                        ddlSegment.SelectedValue = segmentArray[0];

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
                    dt = objMainClass.GetLabelData(objMainClass.intCmpId, ddlSegment.SelectedValue, (int)JOBSTATUS.ForwDocGen, txtSerialNo.Text, txtJobid.Text, "SEARCH");

                    if (dt.Rows.Count > 0)
                    {
                        gvList.DataSource = dt;
                        gvList.DataBind();
                    }
                    else
                    {
                        gvList.DataSource = string.Empty;
                        gvList.DataBind();
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
                    string attachment = "attachment; filename=MRPLabelJobList.xls";
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    //objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menutabid.Value, "");
                    //if (FormRights.bPrint == false)
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                    //    return;
                    //}

                    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

                    string jobid = grdrow.Cells[11].Text;


                    DataTable dt = new DataTable();
                    dt = objMainClass.GetLabelData(objMainClass.intCmpId, "", 0, "", jobid, "LABELDATA");
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dt.Rows[0]["MRP"]) > 0)
                        {
                            if (Convert.ToString(dt.Rows[0]["SEGMENT"]) == "1015")
                            {
                                objBindDDL.FillLists(ddlPopSalesFrom, "SF");
                                lblPopJobid.Text = objMainClass.strConvertZeroPadding(jobid);
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-detail').modal();", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(Please enter MRP to print label!);", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(No record found to print label!);", true);
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

        protected void ddlSegment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable segmentDt = new DataTable();
                    segmentDt = objMainClass.SelectUserSegment(Convert.ToString(Session["USERID"]));


                    string segment = Convert.ToString(segmentDt.Rows[0]["SEGMENT"]);
                    string[] segmentArray = segment.Split(',');
                    if (segment.Contains(ddlSegment.SelectedValue) == true)
                    {

                    }
                    else
                    {
                        ddlSegment.SelectedValue = segmentArray[0];
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(You do not have plant rights!);", true);
                        ddlSegment.SelectedValue = segmentArray[0];

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

        protected void lnkPrintLabel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    DataTable dt = new DataTable();
                    dt = objMainClass.GetLabelData(objMainClass.intCmpId, "", 0, "", lblPopJobid.Text, "LABELDATA");
                    if (dt.Rows.Count > 0)
                    {

                        if (Convert.ToInt32(dt.Rows[0]["MRP"]) > 0)
                        {
                            if (ddlPopSalesFrom.SelectedItem.Text == "FLIPKART")
                            {
                                PrintDocument pd = new PrintDocument();
                                pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                                pd.DefaultPageSettings.PaperSize = new PaperSize("Custom", 300, 327);
                                // pd.PrinterSettings.PrinterName = "ZDesigner GC420T (EPL)";
                                pd.PrinterSettings.PrinterName = "Microsoft Print to PDF";
                                pd.Print();
                            }
                            else
                            {
                                PrintDocument pd = new PrintDocument();
                                pd.PrintPage += new PrintPageEventHandler(pd_PrintPageElse);
                                pd.DefaultPageSettings.PaperSize = new PaperSize("Custom", 300, 327);
                                //pd.PrinterSettings.PrinterName = "ZDesigner GC420T (EPL)";
                                pd.PrinterSettings.PrinterName = "Microsoft Print to PDF";
                                pd.Print();
                            }

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(Please enter MRP to print label!);", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(No record found to print label!);", true);
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



        void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {

            DataTable dt = new DataTable();
            dt = objMainClass.GetLabelData(objMainClass.intCmpId, "", 0, "", lblPopJobid.Text, "LABELDATA");

            Font printFont = new Font("Ayar Kasone", 4, FontStyle.Bold);
            Font printFont5 = new Font("Ayar Kasone", 5, FontStyle.Bold);
            Font printFontModel = new Font("Ayar Kasone", 5, FontStyle.Bold);
            Font printFontBig = new Font("Ayar Kasone", 6, FontStyle.Regular);
            Font barcode = new Font("3 of 9 Barcode", 17, FontStyle.Regular);

            SolidBrush br = new SolidBrush(Color.Black);

            ev.Graphics.DrawString("NAME OF THE ", printFont, br, 20, 30);
            ev.Graphics.DrawString("COMMODITY : MOBILES", printFont, br, 20, 40);
            ev.Graphics.DrawString("BRAND / PRODUCT : ", printFont, br, 20, 50);
            ev.Graphics.DrawString("GRADE : CERTIFIED REFURBISHED - " + Convert.ToString(dt.Rows[0]["PRODGRADE"]), printFont, br, 20, 60);
            ev.Graphics.DrawString("MRP : RS " + Convert.ToString(dt.Rows[0]["MRP"]) + " /- Incusive of all taxes ", printFont, br, 20, 70);
            ev.Graphics.DrawString("PACKED ON : " + DateTime.Now.Day + " - " + (DateTime.Now).ToString("MMMM") + " - " + DateTime.Now.Year + " ", printFont, br, 20, 80);
            ev.Graphics.DrawString("FSN : " + Convert.ToString(dt.Rows[0]["JWREFNO2"]) + "", printFont, br, 20, 90);
            ev.Graphics.DrawString("PRODUCT DIMESIONS : 20 cm * 11 cm * 5 cm", printFont, br, 20, 100);

            ev.Graphics.DrawString("UNIQUE", printFont, br, 50, 125);
            ev.Graphics.DrawString("ID : ", printFont, br, 60, 135);


            ev.Graphics.DrawString("MOBEX" + lblPopJobid.Text, barcode, br, 80, 120);
            ev.Graphics.DrawString("MOBEX" + lblPopJobid.Text, printFontBig, br, 140, 140);

            ev.Graphics.DrawString("IMEI 1", printFont, br, 85, 160);
            ev.Graphics.DrawString(Convert.ToString(dt.Rows[0]["IMEINO"]), barcode, br, 80, 170);
            ev.Graphics.DrawString(Convert.ToString(dt.Rows[0]["IMEINO"]), printFontBig, br, 140, 190);


            ev.Graphics.DrawString("Manufactured( Refurbished &", printFont5, br, 20, 210);
            ev.Graphics.DrawString("Repacked ) By :- ", printFont5, br, 20, 220);
            ev.Graphics.DrawString("Qarmatek Services Pvt Ltd", printFont5, br, 20, 230);
            ev.Graphics.DrawString("2nd Floor,Shashwat Business Park,", printFont5, br, 20, 240);
            ev.Graphics.DrawString("Opp Soma Textile,", printFont5, br, 20, 250);
            ev.Graphics.DrawString("Rakhiyal, Ahmedabad - ", printFont5, br, 20, 260);
            ev.Graphics.DrawString("380023", printFont5, br, 20, 270);
            ev.Graphics.DrawString("Gujarat", printFont5, br, 20, 280);




            ev.Graphics.DrawString(Convert.ToString(dt.Rows[0]["MAKE"]) + "  " + Convert.ToString(dt.Rows[0]["MODELDESC"]) + "  " + Convert.ToString(dt.Rows[0]["COLOR"]), printFontModel, br, 120, 30);
            ev.Graphics.DrawString("ROM : " + Convert.ToString(dt.Rows[0]["ROM"]) + " RAM " + Convert.ToString(dt.Rows[0]["RAM"]), printFontModel, br, 120, 40);
            ev.Graphics.DrawString("NET QUANTITY : (1 U )", printFont, br, 170, 50);
            ev.Graphics.DrawString("NET CONTENTS : ", printFont, br, 170, 60);
            ev.Graphics.DrawString("1U Cellular Phone With Battery", printFont, br, 170, 70);
            ev.Graphics.DrawString("1U Adapter", printFont, br, 170, 80);
            ev.Graphics.DrawString("1U Charging Cable ", printFont, br, 170, 90);
            ev.Graphics.DrawString("Month  & Year Of Re-packaging : ", printFont, br, 170, 100);
            ev.Graphics.DrawString(DateTime.Now.ToString("MMMM") + " - " + DateTime.Now.Year, printFont, br, 170, 110);


            ev.Graphics.DrawString("For Consumer Complaint / Feedback ", printFont5, br, 145, 210);
            ev.Graphics.DrawString("CONTACT PERSON - SERVICE MANAGER", printFont5, br, 145, 220);
            ev.Graphics.DrawString("AFORESERVE TECHNOLOGIES(P) LIMITED, ", printFont5, br, 145, 230);
            ev.Graphics.DrawString("B - 2 / 17, First Floor, Mohan Co-operative, ", printFont5, br, 145, 240);
            ev.Graphics.DrawString("Industrial Estate, Mathura Road, New Delhi,", printFont5, br, 145, 250);
            ev.Graphics.DrawString("South Delhi, Delhi, India 110044", printFont5, br, 145, 260);


            ev.Graphics.DrawString("Toll Free: +918860396039", printFont5, br, 170, 270);
            ev.Graphics.DrawString("CS Email ID:", printFont5, br, 170, 280);
            ev.Graphics.DrawString("contactus@xtracover.com", printFont5, br, 170, 290);





        }


        void pd_PrintPageElse(object sender, PrintPageEventArgs ev)
        {

            DataTable dt = new DataTable();
            dt = objMainClass.GetLabelData(objMainClass.intCmpId, "", 0, "", lblPopJobid.Text, "LABELDATA");

            Font printFont = new Font("Ayar Kasone", 4, FontStyle.Bold);
            Font printFont5 = new Font("Ayar Kasone", 5, FontStyle.Bold);
            Font printFontModel = new Font("Ayar Kasone", 5, FontStyle.Bold);
            Font printFontBig = new Font("Ayar Kasone", 6, FontStyle.Regular);
            Font barcode = new Font("3 of 9 Barcode", 17, FontStyle.Regular);

            SolidBrush br = new SolidBrush(Color.Black);

            ev.Graphics.DrawString("NAME OF THE ", printFont, br, 20, 30);
            ev.Graphics.DrawString("COMMODITY : MOBILES", printFont, br, 20, 40);
            ev.Graphics.DrawString("BRAND / PRODUCT : ", printFont, br, 20, 50);
            ev.Graphics.DrawString("GRADE : CERTIFIED REFURBISHED - " + Convert.ToString(dt.Rows[0]["PRODGRADE"]), printFont, br, 20, 60);
            ev.Graphics.DrawString("MRP : RS " + Convert.ToString(dt.Rows[0]["MRP"]) + " /- Incusive of all taxes ", printFont, br, 20, 70);
            ev.Graphics.DrawString("PACKED ON : " + DateTime.Now.Day + " - " + DateTime.Now.ToString("MM") + " - " + DateTime.Now.Year + " ", printFont, br, 20, 80);
            ev.Graphics.DrawString("FSN : " + Convert.ToString(dt.Rows[0]["JWREFNO2"]) + "", printFont, br, 20, 90);
            ev.Graphics.DrawString("PRODUCT DIMESIONS : 20 cm * 11 cm * 5 cm", printFont, br, 20, 100);

            
            ev.Graphics.DrawString("UNIQUE", printFont, br, 50, 125);
            ev.Graphics.DrawString("ID : ", printFont, br, 60, 135);


            ev.Graphics.DrawString("MOBEX" + lblPopJobid.Text, barcode, br, 80, 120);
            ev.Graphics.DrawString("MOBEX" + lblPopJobid.Text, printFontBig, br, 140, 140);

            ev.Graphics.DrawString("IMEI 1", printFont, br, 85, 160);
            ev.Graphics.DrawString(Convert.ToString(dt.Rows[0]["IMEINO"]), barcode, br, 80, 170);
            ev.Graphics.DrawString(Convert.ToString(dt.Rows[0]["IMEINO"]), printFontBig, br, 140, 190);


            ev.Graphics.DrawString("Manufactured( Refurbished &", printFont5, br, 20, 210);
            ev.Graphics.DrawString("Repacked ) By :- ", printFont5, br, 20, 220);
            ev.Graphics.DrawString("Qarmatek Services Pvt Ltd", printFont5, br, 20, 230);
            ev.Graphics.DrawString("2nd Floor,Shashwat Business Park,", printFont5, br, 20, 240);
            ev.Graphics.DrawString("Opp Soma Textile,", printFont5, br, 20, 250);
            ev.Graphics.DrawString("Rakhiyal, Ahmedabad - ", printFont5, br, 20, 260);
            ev.Graphics.DrawString("380023", printFont5, br, 20, 270);
            ev.Graphics.DrawString("Gujarat", printFont5, br, 20, 280);




            ev.Graphics.DrawString(Convert.ToString(dt.Rows[0]["MAKE"]) + "  " + Convert.ToString(dt.Rows[0]["MODELDESC"]) + "  " + Convert.ToString(dt.Rows[0]["COLOR"]), printFontModel, br, 120, 30);
            ev.Graphics.DrawString("ROM : " + Convert.ToString(dt.Rows[0]["ROM"]) + " RAM " + Convert.ToString(dt.Rows[0]["RAM"]), printFontModel, br, 120, 40);
            ev.Graphics.DrawString("NET QUANTITY : (1 U )", printFont, br, 170, 50);
            ev.Graphics.DrawString("NET CONTENTS : ", printFont, br, 170, 60);
            ev.Graphics.DrawString("1U Cellular Phone With Battery", printFont, br, 170, 70);
            ev.Graphics.DrawString("1U Adapter", printFont, br, 170, 80);
            ev.Graphics.DrawString("1U Charging Cable ", printFont, br, 170, 90);
            ev.Graphics.DrawString("Month  & Year Of Re-packaging : ", printFont, br, 170, 100);
            ev.Graphics.DrawString(DateTime.Now.ToString("MM") + " - " + DateTime.Now.Year, printFont, br, 170, 110);


            ev.Graphics.DrawString("For Consumer Complaint / Feedback ", printFont5, br, 150, 210);
            ev.Graphics.DrawString("Customer Care No.: ", printFont5, br, 150, 220);
            ev.Graphics.DrawString("+91 7676576765 (Cust. Exe.)", printFont5, br, 150, 230);
            ev.Graphics.DrawString("Email Support: care@mobex.in", printFont5, br, 150, 240);
            ev.Graphics.DrawString("Sanitised For Your Protection!!", printFont5, br, 150, 250);


            //ev.Graphics.DrawString("South Delhi, Delhi, India 110044", printFont5, br, 150, 260);
            //ev.Graphics.DrawString("Toll Free: +918860396039", printFont5, br, 170, 270);
            //ev.Graphics.DrawString("CS Email ID:", printFont5, br, 170, 280);
            //ev.Graphics.DrawString("contactus@xtracover.com", printFont5, br, 170, 290);





        }
    }
}