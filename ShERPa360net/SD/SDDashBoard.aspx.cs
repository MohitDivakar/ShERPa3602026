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
    public partial class SDDashBoard : System.Web.UI.Page
    {

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
                        string[] date = objMainClass.indianTime.Date.ToString("dd-MM-yyyy").Split('-');
                        DateTime fromdate = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), 1);
                        txtFromDate.Text = fromdate.ToString("dd-MM-yyyy");
                        txtToDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");// Convert.ToDateTime(DateTime.Now).ToShortDateString();

                        GetData();
                        GetData2();
                        GetData3();

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
                    dt = objMainClass.GetSDDashboard(objMainClass.intCmpId, txtFromDate.Text, txtToDate.Text, "SDDASHBOARD");

                    if (dt.Rows.Count > 0)
                    {
                        gvList.DataSource = dt;
                        gvList.DataBind();

                        decimal totalORDERBOOKED = 0, totalRETURNORDER = 0, totalORDERCANCEL = 0, totalDISPATCH = 0, totalDELIVERED = 0, totalINVOICE = 0;
                        for (int i = 0; i < gvList.Rows.Count; i++)
                        {
                            totalORDERBOOKED = totalORDERBOOKED + Convert.ToDecimal(dt.Rows[i]["ORDERBOOKED"]);
                            totalRETURNORDER = totalRETURNORDER + Convert.ToDecimal(dt.Rows[i]["RETURNORDER"]);
                            totalORDERCANCEL = totalORDERCANCEL + Convert.ToDecimal(dt.Rows[i]["ORDERCANCEL"]);
                            totalDISPATCH = totalDISPATCH + Convert.ToDecimal(dt.Rows[i]["DISPATCH"]);
                            totalDELIVERED = totalDELIVERED + Convert.ToDecimal(dt.Rows[i]["DELIVERED"]);
                            totalINVOICE = totalINVOICE + Convert.ToDecimal(dt.Rows[i]["INVOICE"]);
                        }

                        gvList.FooterRow.Cells[0].Text = "Total";
                        gvList.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                        gvList.FooterRow.Cells[2].Text = totalORDERBOOKED.ToString("N2");
                        gvList.FooterRow.Cells[3].Text = totalRETURNORDER.ToString("N2");
                        gvList.FooterRow.Cells[4].Text = totalORDERCANCEL.ToString("N2");
                        gvList.FooterRow.Cells[5].Text = totalDISPATCH.ToString("N2");
                        gvList.FooterRow.Cells[6].Text = totalDELIVERED.ToString("N2");
                        gvList.FooterRow.Cells[7].Text = totalINVOICE.ToString("N2");

                        gvList.FooterRow.Cells[0].Font.Bold = true;
                        gvList.FooterRow.Cells[1].Font.Bold = true;
                        gvList.FooterRow.Cells[2].Font.Bold = true;
                        gvList.FooterRow.Cells[3].Font.Bold = true;
                        gvList.FooterRow.Cells[4].Font.Bold = true;
                        gvList.FooterRow.Cells[5].Font.Bold = true;
                        gvList.FooterRow.Cells[6].Font.Bold = true;
                        gvList.FooterRow.Cells[7].Font.Bold = true;
                        gvList.FooterRow.Cells[0].BackColor = System.Drawing.Color.IndianRed;
                        gvList.FooterRow.Cells[1].BackColor = System.Drawing.Color.IndianRed;
                        gvList.FooterRow.Cells[2].BackColor = System.Drawing.Color.IndianRed;
                        gvList.FooterRow.Cells[3].BackColor = System.Drawing.Color.IndianRed;
                        gvList.FooterRow.Cells[4].BackColor = System.Drawing.Color.IndianRed;
                        gvList.FooterRow.Cells[5].BackColor = System.Drawing.Color.IndianRed;
                        gvList.FooterRow.Cells[6].BackColor = System.Drawing.Color.IndianRed;
                        gvList.FooterRow.Cells[7].BackColor = System.Drawing.Color.IndianRed;


                        gvList.HeaderRow.Cells[0].BackColor = System.Drawing.Color.DarkBlue;
                        gvList.HeaderRow.Cells[1].BackColor = System.Drawing.Color.DarkBlue;
                        gvList.HeaderRow.Cells[2].BackColor = System.Drawing.Color.DarkBlue;
                        gvList.HeaderRow.Cells[3].BackColor = System.Drawing.Color.DarkBlue;
                        gvList.HeaderRow.Cells[4].BackColor = System.Drawing.Color.DarkBlue;
                        gvList.HeaderRow.Cells[5].BackColor = System.Drawing.Color.DarkBlue;
                        gvList.HeaderRow.Cells[6].BackColor = System.Drawing.Color.DarkBlue;
                        gvList.HeaderRow.Cells[7].BackColor = System.Drawing.Color.DarkBlue;


                        gvList.FooterRow.BackColor = System.Drawing.Color.IndianRed;
                        gvList.FooterRow.ForeColor = System.Drawing.Color.White;
                        gvList.HeaderRow.ForeColor = System.Drawing.Color.White;
                        gvList.FooterRow.Font.Size = 10;


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


        public void GetData2()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetSDDashboard(objMainClass.intCmpId, "", "", "OPENSO");

                    if (dt.Rows.Count > 0)
                    {
                        gvList2.DataSource = dt;
                        gvList2.DataBind();

                        decimal totalAmazon = 0, total2GUD = 0, totalAffiliate = 0, totalFlipkart = 0, totalMobex = 0, totalStall = 0;
                        for (int i = 0; i < gvList2.Rows.Count; i++)
                        {
                            totalAmazon = totalAmazon + Convert.ToDecimal(dt.Rows[i]["AMAZON"]);
                            total2GUD = total2GUD + Convert.ToDecimal(dt.Rows[i]["2GUD"]);
                            totalAffiliate = totalAffiliate + Convert.ToDecimal(dt.Rows[i]["AFFILIATE"]);
                            totalFlipkart = totalFlipkart + Convert.ToDecimal(dt.Rows[i]["FLIPKART"]);
                            totalMobex = totalMobex + Convert.ToDecimal(dt.Rows[i]["MOBEX WEBSITE"]);
                            totalStall = totalStall + Convert.ToDecimal(dt.Rows[i]["STALL RAKHIAL"]);
                        }

                        gvList2.FooterRow.Cells[0].Text = "Total";
                        //gvList.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                        gvList2.FooterRow.Cells[1].Text = totalAmazon.ToString("N2");
                        gvList2.FooterRow.Cells[2].Text = total2GUD.ToString("N2");
                        gvList2.FooterRow.Cells[3].Text = totalAffiliate.ToString("N2");
                        gvList2.FooterRow.Cells[4].Text = totalFlipkart.ToString("N2");
                        gvList2.FooterRow.Cells[5].Text = totalMobex.ToString("N2");
                        gvList2.FooterRow.Cells[6].Text = totalStall.ToString("N2");

                        gvList2.FooterRow.Cells[0].Font.Bold = true;
                        gvList2.FooterRow.Cells[1].Font.Bold = true;
                        gvList2.FooterRow.Cells[2].Font.Bold = true;
                        gvList2.FooterRow.Cells[3].Font.Bold = true;
                        gvList2.FooterRow.Cells[4].Font.Bold = true;
                        gvList2.FooterRow.Cells[5].Font.Bold = true;
                        gvList2.FooterRow.Cells[6].Font.Bold = true;
                        gvList2.FooterRow.Cells[7].Font.Bold = true;
                        gvList2.FooterRow.Cells[0].BackColor = System.Drawing.Color.IndianRed;
                        gvList2.FooterRow.Cells[1].BackColor = System.Drawing.Color.IndianRed;
                        gvList2.FooterRow.Cells[2].BackColor = System.Drawing.Color.IndianRed;
                        gvList2.FooterRow.Cells[3].BackColor = System.Drawing.Color.IndianRed;
                        gvList2.FooterRow.Cells[4].BackColor = System.Drawing.Color.IndianRed;
                        gvList2.FooterRow.Cells[5].BackColor = System.Drawing.Color.IndianRed;
                        gvList2.FooterRow.Cells[6].BackColor = System.Drawing.Color.IndianRed;
                        gvList2.FooterRow.Cells[7].BackColor = System.Drawing.Color.IndianRed;


                        gvList2.HeaderRow.Cells[0].BackColor = System.Drawing.Color.DarkBlue;
                        gvList2.HeaderRow.Cells[1].BackColor = System.Drawing.Color.DarkBlue;
                        gvList2.HeaderRow.Cells[2].BackColor = System.Drawing.Color.DarkBlue;
                        gvList2.HeaderRow.Cells[3].BackColor = System.Drawing.Color.DarkBlue;
                        gvList2.HeaderRow.Cells[4].BackColor = System.Drawing.Color.DarkBlue;
                        gvList2.HeaderRow.Cells[5].BackColor = System.Drawing.Color.DarkBlue;
                        gvList2.HeaderRow.Cells[6].BackColor = System.Drawing.Color.DarkBlue;
                        gvList2.HeaderRow.Cells[7].BackColor = System.Drawing.Color.DarkBlue;

                        gvList2.FooterRow.ForeColor = System.Drawing.Color.White;
                        gvList2.HeaderRow.ForeColor = System.Drawing.Color.White;
                        gvList2.FooterRow.Font.Size = 10;


                    }
                    else
                    {
                        gvList2.DataSource = string.Empty;
                        gvList2.DataBind();
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

        public void GetData3()
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    DataTable dt = new DataTable();
                    dt = objMainClass.GetSDDashboard(objMainClass.intCmpId, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortDateString(), "NEWDATE");

                    if (dt.Rows.Count > 0)
                    {
                        decimal totalORDERBOOKED = 0, totalRETURNORDER = 0, totalORDERCANCEL = 0, totalDISPATCH = 0, totalDELIVERED = 0, totalINVOICE = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            totalORDERBOOKED = totalORDERBOOKED + Convert.ToDecimal(dt.Rows[i]["ORDERBOOKED"]);
                            totalRETURNORDER = totalRETURNORDER + Convert.ToDecimal(dt.Rows[i]["RETURNORDER"]);
                            totalORDERCANCEL = totalORDERCANCEL + Convert.ToDecimal(dt.Rows[i]["ORDERCANCEL"]);
                            totalDISPATCH = totalDISPATCH + Convert.ToDecimal(dt.Rows[i]["DISPATCH"]);
                            totalDELIVERED = totalDELIVERED + Convert.ToDecimal(dt.Rows[i]["DELIVERED"]);
                            totalINVOICE = totalINVOICE + Convert.ToDecimal(dt.Rows[i]["INVOICE"]);
                        }
                        lblOrderBooked.Text = Convert.ToString(totalORDERBOOKED);
                        lblOrderReturned.Text = Convert.ToString(totalRETURNORDER);
                        lblDispatched.Text = Convert.ToString(totalDISPATCH);
                        lblOrderCancelled.Text = Convert.ToString(totalORDERCANCEL);
                        lblDelivered.Text = Convert.ToString(totalDELIVERED);
                        lblInovice.Text = Convert.ToString(totalINVOICE);

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
        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

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
}