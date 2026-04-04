using ShERPa360net.Class;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.REPORTS
{
    public partial class frmMSLReport : System.Web.UI.Page
    {
        BindDDL objBindDDL = new BindDDL();
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["UserId"] != null)
                    {
                        objDALUserRights.CHECK_FORMRIGHTS(Session["UserId"].ToString(), menutibid.Value, "");
                        if (FormRights.bView == false)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }

                        //objBindDDL.FillPlant(ddlPlancode);
                        //ddlPlancode.SelectedValue = "1001";



                        BindGridView();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);

            }
        }

        //private void DropDownList()
        //{
        //    try
        //    {
        //        if (Session["UserId"] != null)
        //        {
        //            objBindDDL.FillPlant(ddlPlancode);
        //            ddlPlancode.SelectedValue = "0";

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
        //    }

        //}


        private void BindGridView()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();

                    //string CheckLocation = string.Empty;
                    //for (int i = 0; i < chkLocation.Items.Count; i++)
                    //{
                    //    if (chkLocation.Items[i].Selected == true)
                    //    {
                    //        if (CheckLocation != "" && CheckLocation != string.Empty && CheckLocation != null)
                    //        {
                    //            CheckLocation = CheckLocation + ',' + chkLocation.Items[i].Text;
                    //        }
                    //        else
                    //        {
                    //            CheckLocation = chkLocation.Items[i].Text;

                    //        }
                    //    }
                    //}

                    dt = objMainClass.Msl_Report("ALLMSL", "", "");
                    if (dt.Rows.Count > 0)
                    {


                        decimal MSL = 0, STKHO = 0, STKFB01 = 0, STKFB02 = 0, STKFB03 = 0, STKFB04 = 0, STKBNGLR = 0, REQQTY = 0, HOLISTING = 0, BLRLISTING = 0, LISTING = 0, PURCHASE = 0, DISPATCH = 0, TOTALSTKAVAIL = 0, TOTAlLISTINGAVAIL = 0;
                        int MSLCNT = 0, STKHOCNT = 0, STKFB01CNT = 0, STKFB02CNT = 0, STKFB03CNT = 0, STKFB04CNT = 0, STKBNGLRCNT = 0, REQQTYCNT = 0, HOLISTINGCNT = 0, BLRLISTINGCNT = 0, LISTINGCNT = 0, PURCHASECNT = 0, DISPATCHCNT = 0, TOTALSTKAVAILCNT = 0, TOTAlLISTINGAVAILCNT = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            MSL = MSL + Convert.ToDecimal(dt.Rows[i]["MSL"]);
                            STKHO = STKHO + Convert.ToDecimal(dt.Rows[i]["STKHO"]);
                            STKFB01 = STKFB01 + Convert.ToDecimal(dt.Rows[i]["STKFB01"]);
                            STKFB02 = STKFB02 + Convert.ToDecimal(dt.Rows[i]["STKFB02"]);
                            STKFB03 = STKFB03 + Convert.ToDecimal(dt.Rows[i]["STKFB03"]);
                            STKFB04 = STKFB04 + Convert.ToDecimal(dt.Rows[i]["STKFB04"]);
                            STKBNGLR = STKBNGLR + Convert.ToDecimal(dt.Rows[i]["STKBLR"]);
                            if (!Convert.ToString(dt.Rows[i]["REQQTY"]).Contains("-"))
                            {
                                REQQTY = REQQTY + Convert.ToDecimal(dt.Rows[i]["REQQTY"]);
                            }
                            LISTING = LISTING + Convert.ToDecimal(dt.Rows[i]["LISTING"]);

                            HOLISTING = HOLISTING + Convert.ToDecimal(dt.Rows[i]["HOLISTING"]);
                            BLRLISTING = BLRLISTING + Convert.ToDecimal(dt.Rows[i]["BLRLISTING"]);

                            PURCHASE = PURCHASE + Convert.ToDecimal(dt.Rows[i]["PURCHASE"]);
                            DISPATCH = DISPATCH + Convert.ToDecimal(dt.Rows[i]["DISPATCH"]);
                            TOTALSTKAVAIL = TOTALSTKAVAIL + Convert.ToDecimal(dt.Rows[i]["TOTALAVAILSTOCK"]);
                            TOTAlLISTINGAVAIL = TOTAlLISTINGAVAIL + Convert.ToDecimal(dt.Rows[i]["AVAILATLISTING"]);

                            if (Convert.ToInt32(dt.Rows[i]["MSL"]) > 0)
                            {
                                //MSLCNT = MSLCNT + Convert.ToInt32(dt.Rows[i]["MSL"]);
                                MSLCNT = MSLCNT + 1;
                            }
                            if (Convert.ToInt32(dt.Rows[i]["STKHO"]) > 0)
                            {
                                //STKHOCNT = STKHOCNT + Convert.ToInt32(dt.Rows[i]["STKHO"]);
                                STKHOCNT = STKHOCNT + 1;
                            }
                            if (Convert.ToInt32(dt.Rows[i]["STKFB01"]) > 0)
                            {
                                //STKFB01CNT = STKFB01CNT + Convert.ToInt32(dt.Rows[i]["STKFB01"]);
                                STKFB01CNT = STKFB01CNT + 1;
                            }
                            if (Convert.ToInt32(dt.Rows[i]["STKFB02"]) > 0)
                            {
                                //STKFB02CNT = STKFB02CNT + Convert.ToInt32(dt.Rows[i]["STKFB02"]);
                                STKFB02CNT = STKFB02CNT + 1;
                            }
                            if (Convert.ToInt32(dt.Rows[i]["STKFB03"]) > 0)
                            {
                                //STKFB03CNT = STKFB03CNT + Convert.ToInt32(dt.Rows[i]["STKFB03"]);
                                STKFB03CNT = STKFB03CNT + 1;
                            }
                            if (Convert.ToInt32(dt.Rows[i]["STKFB04"]) > 0)
                            {
                                //STKFB04CNT = STKFB04CNT + Convert.ToInt32(dt.Rows[i]["STKFB04"]);
                                STKFB04CNT = STKFB04CNT + 1;
                            }
                            if (Convert.ToInt32(dt.Rows[i]["STKBLR"]) > 0)
                            {
                                //STKBNGLRCNT = STKBNGLRCNT + Convert.ToInt32(dt.Rows[i]["STKBLR"]);
                                STKBNGLRCNT = STKBNGLRCNT + 1;
                            }
                            if (Convert.ToInt32(dt.Rows[i]["REQQTY"]) > 0)
                            {
                                //REQQTYCNT = REQQTYCNT + Convert.ToInt32(dt.Rows[i]["REQQTY"]);
                                REQQTYCNT = REQQTYCNT + 1;
                            }
                            if (Convert.ToInt32(dt.Rows[i]["LISTING"]) > 0)
                            {
                                //LISTINGCNT = LISTINGCNT + Convert.ToInt32(dt.Rows[i]["LISTING"]);
                                LISTINGCNT = LISTINGCNT + 1;
                            }

                            if (Convert.ToInt32(dt.Rows[i]["HOLISTING"]) > 0)
                            {
                                //HOLISTINGCNT = HOLISTINGCNT + Convert.ToInt32(dt.Rows[i]["HOLISTING"]);
                                HOLISTINGCNT = HOLISTINGCNT + 1;
                            }
                            if (Convert.ToInt32(dt.Rows[i]["BLRLISTING"]) > 0)
                            {
                                //BLRLISTINGCNT = BLRLISTINGCNT + Convert.ToInt32(dt.Rows[i]["BLRLISTING"]);
                                BLRLISTINGCNT = BLRLISTINGCNT + 1;
                            }

                            if (Convert.ToInt32(dt.Rows[i]["PURCHASE"]) > 0)
                            {
                                //PURCHASECNT = PURCHASECNT + Convert.ToInt32(dt.Rows[i]["PURCHASE"]);
                                PURCHASECNT = PURCHASECNT + 1;
                            }
                            if (Convert.ToInt32(dt.Rows[i]["DISPATCH"]) > 0)
                            {
                                //DISPATCHCNT = DISPATCHCNT + Convert.ToInt32(dt.Rows[i]["DISPATCH"]);
                                DISPATCHCNT = DISPATCHCNT + 1;
                            }
                            if (Convert.ToInt32(dt.Rows[i]["TOTALAVAILSTOCK"]) > 0)
                            {
                                TOTALSTKAVAILCNT = TOTALSTKAVAILCNT + 1;
                            }
                            if (Convert.ToInt32(dt.Rows[i]["AVAILATLISTING"]) > 0)
                            {
                                TOTAlLISTINGAVAILCNT = TOTAlLISTINGAVAILCNT + 1;
                            }




                            //gvMslReport.FooterRow.Cells[3].Text = "Total";
                            //gvMslReport.FooterRow.Cells[4].Text = MSL.ToString("N0");
                            //gvMslReport.FooterRow.Cells[5].Text = STKHO.ToString("N0");
                            //gvMslReport.FooterRow.Cells[6].Text = STKFB01.ToString("N0");
                            //gvMslReport.FooterRow.Cells[7].Text = STKFB02.ToString("N0");
                            //gvMslReport.FooterRow.Cells[8].Text = STKFB03.ToString("N0");
                            //gvMslReport.FooterRow.Cells[9].Text = STKFB04.ToString("N0");
                            //gvMslReport.FooterRow.Cells[10].Text = STKBNGLR.ToString("N0");
                            //gvMslReport.FooterRow.Cells[11].Text = REQQTY.ToString("N0");
                            //gvMslReport.FooterRow.Cells[12].Text = LISTING.ToString("N0");
                            //gvMslReport.FooterRow.Cells[13].Text = PURCHASE.ToString("N0");
                            //gvMslReport.FooterRow.Cells[14].Text = DISPATCH.ToString("N0");


                            ////gvMslReport.FooterRow.Cells[0].BackColor = System.Drawing.Color.IndianRed;
                            ////gvMslReport.FooterRow.Cells[1].BackColor = System.Drawing.Color.IndianRed;
                            ////gvMslReport.FooterRow.Cells[2].BackColor = System.Drawing.Color.IndianRed;
                            //gvMslReport.FooterRow.Cells[3].BackColor = System.Drawing.Color.IndianRed;
                            //gvMslReport.FooterRow.Cells[4].BackColor = System.Drawing.Color.IndianRed;
                            //gvMslReport.FooterRow.Cells[5].BackColor = System.Drawing.Color.IndianRed;
                            //gvMslReport.FooterRow.Cells[6].BackColor = System.Drawing.Color.IndianRed;
                            //gvMslReport.FooterRow.Cells[7].BackColor = System.Drawing.Color.IndianRed;
                            //gvMslReport.FooterRow.Cells[8].BackColor = System.Drawing.Color.IndianRed;
                            //gvMslReport.FooterRow.Cells[9].BackColor = System.Drawing.Color.IndianRed;
                            //gvMslReport.FooterRow.Cells[10].BackColor = System.Drawing.Color.IndianRed;
                            //gvMslReport.FooterRow.Cells[11].BackColor = System.Drawing.Color.IndianRed;
                            //gvMslReport.FooterRow.Cells[12].BackColor = System.Drawing.Color.IndianRed;
                            //gvMslReport.FooterRow.Cells[13].BackColor = System.Drawing.Color.IndianRed;
                            //gvMslReport.FooterRow.Cells[14].BackColor = System.Drawing.Color.IndianRed;
                            //gvMslReport.FooterRow.Cells[13].BackColor = System.Drawing.Color.IndianRed;
                        }


                        dt.Rows.Add(1, "", "", "Total", MSL, STKHO, STKBNGLR, TOTALSTKAVAIL, REQQTY, HOLISTING, BLRLISTING, LISTING, TOTAlLISTINGAVAIL, STKFB01, STKFB02, STKFB03, STKFB04, PURCHASE, DISPATCH);
                        dt.Rows.Add(1, "", "", "SKU", MSLCNT, STKHOCNT, STKBNGLRCNT, TOTALSTKAVAILCNT, REQQTYCNT, HOLISTINGCNT, BLRLISTINGCNT, LISTINGCNT, TOTAlLISTINGAVAILCNT, STKFB01CNT, STKFB02CNT, STKFB03CNT, STKFB04CNT, PURCHASECNT, DISPATCHCNT);


                        gvMslReport.DataSource = dt;
                        gvMslReport.DataBind();
                        gvMslReport.HeaderRow.TableSection = TableRowSection.TableHeader;

                        //gvMslReport.FooterRow.Cells[0].BackColor = System.Drawing.Color.IndianRed;
                        //gvMslReport.FooterRow.Cells[1].BackColor = System.Drawing.Color.IndianRed;
                        //gvMslReport.FooterRow.Cells[2].BackColor = System.Drawing.Color.IndianRed;
                        //gvMslReport.Rows[-1].Cells[3].BackColor = System.Drawing.Color.IndianRed;
                        //gvMslReport.FooterRow.Cells[4].BackColor = System.Drawing.Color.IndianRed;
                        //gvMslReport.FooterRow.Cells[5].BackColor = System.Drawing.Color.IndianRed;
                        //gvMslReport.FooterRow.Cells[6].BackColor = System.Drawing.Color.IndianRed;
                        //gvMslReport.FooterRow.Cells[7].BackColor = System.Drawing.Color.IndianRed;
                        //gvMslReport.FooterRow.Cells[8].BackColor = System.Drawing.Color.IndianRed;
                        //gvMslReport.FooterRow.Cells[9].BackColor = System.Drawing.Color.IndianRed;
                        //gvMslReport.FooterRow.Cells[10].BackColor = System.Drawing.Color.IndianRed;
                        //gvMslReport.FooterRow.Cells[11].BackColor = System.Drawing.Color.IndianRed;
                        //gvMslReport.FooterRow.Cells[12].BackColor = System.Drawing.Color.IndianRed;
                        //gvMslReport.FooterRow.Cells[13].BackColor = System.Drawing.Color.IndianRed;
                        //gvMslReport.FooterRow.Cells[14].BackColor = System.Drawing.Color.IndianRed;
                        //gvMslReport.FooterRow.Cells[15].BackColor = System.Drawing.Color.IndianRed;
                        //gvMslReport.FooterRow.Cells[16].BackColor = System.Drawing.Color.IndianRed;


                    }
                    else
                    {
                        gvMslReport.DataSource = string.Empty;
                        gvMslReport.DataBind();
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

        //protected void ddlPlancode_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        objBindDDL.Checkboxlist(chkLocation, ddlPlancode.SelectedValue);
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
        //    }
        //}
        //protected void lnkSearch_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Session["UserId"] != null)
        //        {
        //            BindGridView();
        //        }

        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
        //    }
        //}

    }
}

