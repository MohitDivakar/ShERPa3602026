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

namespace ShERPa360net.UTILITY
{
    public partial class frmListableDataUpload : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["UploadData"] = null;
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

                        //objBindDDL.FillPlantIsMobexCHK(lstLocation, 1);
                        //objBindDDL.FillCityLST(lstCity);
                        //objBindDDL.FillListsLST(lstGrade, "BG");
                        //objBindDDL.FillListsLST(lstStockType, "STT");

                        objBindDDL.FillPlantIsMobexCHK(chkLocation, 1);
                        chkLocationAll.Checked = true;
                        chkLocationAll_CheckedChanged(1, e);

                        //objBindDDL.FillCityCHK(chkCity, "");
                        //chkCityAll.Checked = true;
                        //chkCityAll_CheckedChanged(1, e);

                        objBindDDL.FillListCHK(chkGrade, "BG");
                        chkGradeAll.Checked = true;
                        chkGradeAll_CheckedChanged(1, e);

                        objBindDDL.FillListsCHK(chkStockType, "STT");
                        chkStockTypeAll.Checked = true;
                        chkStockTypeAll_CheckedChanged(1, e);

                        DataTable dt = new DataTable();
                        dt = objMainClass.GetCommonLists("QTL", 0);
                        if (dt.Rows.Count > 0)
                        {
                            grvQtyLimit.DataSource = dt;
                            grvQtyLimit.DataBind();
                        }
                        else
                        {
                            grvQtyLimit.DataSource = null;
                            grvQtyLimit.DataBind();
                        }
                        //grvQtyLimit

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
                    string stocktype = "";
                    foreach (ListItem item in chkStockType.Items)
                    {
                        if (item.Selected == true)
                        {
                            if (stocktype == "")
                            {
                                stocktype = "'" + item.Text + "'";
                            }
                            else
                            {
                                stocktype = stocktype + ",'" + item.Text + "'";
                            }
                        }
                    }

                    string plantcd = "";
                    foreach (ListItem item in chkLocation.Items)
                    {
                        if (item.Selected == true)
                        {
                            if (plantcd == "")
                            {
                                plantcd = "'" + item.Value + "'";
                            }
                            else
                            {
                                plantcd = plantcd + ",'" + item.Value + "'";
                            }
                        }
                    }

                    string grade = "";
                    foreach (ListItem item in chkGrade.Items)
                    {
                        if (item.Selected == true)
                        {
                            if (grade == "")
                            {
                                grade = "'" + item.Text + "'";
                            }
                            else
                            {
                                grade = grade + ",'" + item.Text + "'";
                            }
                        }
                    }

                    DataTable dt = new DataTable();
                    dt = objMainClass.GetListingUploadData(objMainClass.intCmpId, plantcd, stocktype, grade, "GETUPLOADDATA");

                    if (dt.Rows.Count > 0)
                    {
                        gvList.DataSource = dt;
                        gvList.DataBind();
                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                        Session["UploadData"] = dt;
                    }
                    else
                    {
                        gvList.DataSource = null;
                        gvList.DataBind();
                        Session["UploadData"] = null;
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

        protected void lnkSearh_Click(object sender, EventArgs e)
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

        //protected void btnCondition_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Session["USERID"] != null)
        //        {
        //            if (btnCondition.Text == "=")
        //            {
        //                btnCondition.Text = ">";
        //            }
        //            else
        //            {
        //                btnCondition.Text = "=";
        //            }
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

        protected void chkStockType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    int selectedcount = 0;
                    foreach (ListItem item in chkStockType.Items)
                    {
                        if (item.Selected == true)
                        {
                            selectedcount++;
                        }
                    }

                    if (selectedcount == chkStockType.Items.Count)
                    {
                        chkStockTypeAll.Checked = true;
                    }
                    else
                    {
                        chkStockTypeAll.Checked = false;
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

        protected void chkStockTypeAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (chkStockTypeAll.Checked == true)
                    {
                        foreach (ListItem item in chkStockType.Items)
                        {
                            item.Selected = true;
                        }
                    }
                    else
                    {
                        foreach (ListItem item in chkStockType.Items)
                        {
                            item.Selected = false;
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

        protected void chkLocationAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    string plantcd = "";
                    if (chkLocationAll.Checked == true)
                    {
                        foreach (ListItem item in chkLocation.Items)
                        {
                            item.Selected = true;
                            if (plantcd == "")
                            {
                                plantcd = item.Value;
                            }
                            else
                            {
                                plantcd = plantcd + ',' + item.Value;
                            }
                        }
                    }
                    else
                    {
                        foreach (ListItem item in chkLocation.Items)
                        {
                            item.Selected = false;
                            plantcd = "";
                        }
                    }
                    //objBindDDL.FillCityCHK(chkCity, plantcd);

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

        //protected void chkCityAll_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Session["USERID"] != null)
        //        {
        //            if (chkCityAll.Checked == true)
        //            {
        //                foreach (ListItem item in chkCity.Items)
        //                {
        //                    item.Selected = true;
        //                }
        //            }
        //            else
        //            {
        //                foreach (ListItem item in chkCity.Items)
        //                {
        //                    item.Selected = false;
        //                }
        //            }
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

        protected void chkGradeAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (chkGradeAll.Checked == true)
                    {
                        foreach (ListItem item in chkGrade.Items)
                        {
                            item.Selected = true;
                        }
                    }
                    else
                    {
                        foreach (ListItem item in chkGrade.Items)
                        {
                            item.Selected = false;
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

        protected void chkLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    string plantcd = "";
                    int selectedcount = 0;
                    foreach (ListItem item in chkLocation.Items)
                    {
                        if (item.Selected == true)
                        {
                            selectedcount++;
                            if (plantcd == "")
                            {
                                plantcd = item.Value;
                            }
                            else
                            {
                                plantcd = plantcd + ',' + item.Value;
                            }
                        }
                    }

                    if (selectedcount == chkLocation.Items.Count)
                    {
                        chkLocationAll.Checked = true;
                    }
                    else
                    {
                        chkLocationAll.Checked = false;
                    }

                    //objBindDDL.FillCityCHK(chkCity, plantcd);
                    //chkCityAll.Checked = true;
                    //chkCityAll_CheckedChanged(1, e);

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

        //protected void chkCity_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Session["USERID"] != null)
        //        {
        //            int selectedcount = 0;
        //            foreach (ListItem item in chkCity.Items)
        //            {
        //                if (item.Selected == true)
        //                {
        //                    selectedcount++;
        //                }
        //            }

        //            if (selectedcount == chkCity.Items.Count)
        //            {
        //                chkCityAll.Checked = true;
        //            }
        //            else
        //            {
        //                chkCityAll.Checked = false;
        //            }
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

        protected void chkGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    int selectedcount = 0;
                    foreach (ListItem item in chkGrade.Items)
                    {
                        if (item.Selected == true)
                        {
                            selectedcount++;
                        }
                    }

                    if (selectedcount == chkGrade.Items.Count)
                    {
                        chkGradeAll.Checked = true;
                    }
                    else
                    {
                        chkGradeAll.Checked = false;
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



        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = (DataTable)Session["UploadData"];


                    var query = from row in dt.AsEnumerable()
                                group row by row.Field<string>("ITEMCODE") into IC
                                where IC.Count() > 0
                                select new
                                {
                                    Name = IC.Key,
                                    CountOfClients = IC.Count()
                                };

                    DataTable dtUnique = new DataTable("Data");
                    dtUnique.Columns.Add("ItemCode");
                    dtUnique.Columns.Add("Qty");
                    dtUnique.Columns.Add("UploadQty");
                    dtUnique.Columns.Add("Price");
                    dtUnique.Columns.Add("MRP");
                    dtUnique.Columns.Add("Status");

                    foreach (var ICD in query)
                    {
                        if (ICD.Name != "" && ICD.Name != null && ICD.Name != string.Empty)
                        {

                            var sumqty = dt.AsEnumerable().Where(r => r.Field<string>("ITEMCODE") == ICD.Name).Sum(x => Convert.ToDecimal(x["QTY"]));
                            var price = dt.AsEnumerable().Where(r => r.Field<string>("ITEMCODE") == ICD.Name).Select(x => Convert.ToDecimal(x["SALEPRICE"])).First();
                            var mrp = dt.AsEnumerable().Where(r => r.Field<string>("ITEMCODE") == ICD.Name).Select(x => Convert.ToDecimal(x["MRP"])).First();

                            //string sa = ICD.Name;
                            //int na = sumqty;
                            int upqty = 0;
                            foreach (GridViewRow row in grvQtyLimit.Rows)
                            {
                                TextBox txtqty = row.FindControl("txtToBeQty") as TextBox;
                                Label lblStkQty = row.FindControl("lblStkQty") as Label;
                                int stkqty = 0;
                                if (lblStkQty.Text == ">4")
                                {
                                    stkqty = 5;
                                }
                                else
                                {
                                    stkqty = Convert.ToInt32(lblStkQty.Text);
                                }

                                if (sumqty >= stkqty)
                                {
                                    if (txtqty.Text == "")
                                    {
                                        upqty = -1;
                                    }
                                    else
                                    {
                                        upqty = Convert.ToInt32(txtqty.Text);
                                    }

                                }

                            }


                            //if (upqty > 0)
                            //{
                            if (sumqty > 4 && upqty == -1)
                            {
                                dtUnique.Rows.Add(ICD.Name, sumqty, sumqty, price, mrp, 1);
                            }
                            else
                            {
                                if (sumqty < upqty)
                                {
                                    dtUnique.Rows.Add(ICD.Name, sumqty, sumqty, price, mrp, 1);
                                }
                                else
                                {
                                    dtUnique.Rows.Add(ICD.Name, sumqty, upqty, price, mrp, 1);
                                }
                            }
                            //}
                            //else
                            //{
                            //    dtUnique.Rows.Add(ICD.Name, sumqty, sumqty, mrp, 1);
                            //}
                        }
                    }

                    dt = dtUnique;

                    int totalsku = dt.Rows.Count;
                    int totalqty = 0;
                    int totaluploadqty = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        totalqty += Convert.ToInt32(dt.Rows[i]["Qty"]);
                        totaluploadqty += Convert.ToInt32(dt.Rows[i]["UploadQty"]);
                    }


                    if (gvList.Rows.Count > 0)
                    {
                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                    XLWorkbook wb = new XLWorkbook();
                    wb.Worksheets.Add(dt, "SKU Data");
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.Buffer = true;
                    HttpContext.Current.Response.Charset = "";
                    //HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    HttpContext.Current.Response.ContentType = "application/vdn.ms-excel";
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=Product Upload Data" + DateTime.Now + ".xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        HttpContext.Current.Response.Flush();
                        HttpContext.Current.Response.End();
                        //HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }


                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Total SKU : " + totalsku + ". Total Qty : " + totalqty + ". Total Qty to be upload : " + totaluploadqty + "\");", true);


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