using ShERPa360net.Class;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace ShERPa360net.REPORTS
{
    public partial class rptSOAging : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();


        //public string SortColumn
        //{
        //    get { return Convert.ToString(ViewState["SortColumn"]); }
        //    set { ViewState["SortColumn"] = value; }
        //}

        //public string SortDirection
        //{
        //    get { return Convert.ToString(ViewState["SortDirection"]); }
        //    set { ViewState["SortDirection"] = value; }
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //SortDirection = "DESC";
                //SortColumn = "AGE";

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

                        string[] date = objMainClass.indianTime.Date.ToString("dd-MM-yyyy").Split('-');
                        DateTime fromdate = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), 1);
                        txtFromDocDate.Text = fromdate.ToString("dd-MM-yyyy");
                        txtToDocDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();




                        DataTable dt = new DataTable();
                        dt = objMainClass.GetSOAge(objMainClass.intCmpId, txtFromDocDate.Text, txtToDocDate.Text, "", "", "");

                        //string[] columnNames = dt.Columns.Cast<DataColumn>()
                        //         .Select(x => x.ColumnName)
                        //         .ToArray();

                        if (dt.Rows.Count > 0)
                        {
                            //dt.DefaultView.Sort = SortColumn + " " + SortDirection;
                            gvList.DataSource = dt;
                            gvList.DataBind();
                            //ViewState["SOPENDINGDATA"] = dt;
                            gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                            //ShowingGroupingDataInGridView(gvList.Rows, 0, 3);

                            int grater10 = 0;
                            int btw8to10 = 0;
                            int btw5to7 = 0;
                            int less4 = 0;

                            string find10 = "AGE > '10'";
                            DataRow[] foundRows10 = dt.Select(find10);
                            if (foundRows10.Length > 0)
                            {
                                grater10 = foundRows10.Length;
                            }


                            string find8to10 = "AGE >= '8' AND AGE <= '10'";
                            DataRow[] foundRows8to10 = dt.Select(find8to10);
                            if (foundRows8to10.Length > 0)
                            {
                                btw8to10 = foundRows8to10.Length;
                            }

                            string find8to7 = "AGE >= '5' AND AGE <= '7'";
                            DataRow[] foundRows5to7 = dt.Select(find8to7);
                            if (foundRows5to7.Length > 0)
                            {
                                btw5to7 = foundRows5to7.Length;
                            }

                            string findless4 = "AGE <= '4'";
                            DataRow[] foundRowsless4 = dt.Select(findless4);
                            if (foundRowsless4.Length > 0)
                            {
                                less4 = foundRowsless4.Length;
                            }


                            btnGreater10.Text = "> 10  - (" + grater10 + ")";
                            btnbtw8to10.Text = ">= 8 to <= 10 - (" + btw8to10 + ")";
                            btnbtw5to7.Text = ">= 5 to <= 7 - (" + btw5to7 + ")";
                            btnless4.Text = "<= 4 - (" + less4 + ")";

                            if (grater10 == 0)
                            {
                                btnGreater10.Enabled = false;
                            }
                            else
                            {
                                btnGreater10.Enabled = true;
                            }
                            if (btw8to10 == 0)
                            {
                                btnbtw8to10.Enabled = false;
                            }
                            else
                            {
                                btnbtw8to10.Enabled = true;
                            }
                            if (btw5to7 == 0)
                            {
                                btnbtw5to7.Enabled = false;
                            }
                            else
                            {
                                btnbtw5to7.Enabled = true;
                            }
                            if (less4 == 0)
                            {
                                btnless4.Enabled = false;
                            }
                            else
                            {
                                btnless4.Enabled = true;
                            }




                            //GridViewHelper

                        }
                        else
                        {
                            // dt.DefaultView.Sort = SortColumn + " " + SortDirection;
                            gvList.DataSource = string.Empty;
                            gvList.DataBind();
                            //ViewState["SOPENDINGDATA"] = dt;

                            btnGreater10.Enabled = false;
                            btnbtw8to10.Enabled = false;
                            btnbtw5to7.Enabled = false;
                            btnless4.Enabled = false;
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

        protected void lnkSerch_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = objMainClass.GetSOAge(objMainClass.intCmpId, txtFromDocDate.Text, txtToDocDate.Text, txtDocNo.Text == "" ? "" : txtDocNo.Text, "", "");
                    if (dt.Rows.Count > 0)
                    {
                        //dt.DefaultView.Sort = SortColumn + " " + SortDirection;
                        gvList.DataSource = dt;
                        gvList.DataBind();
                        //ViewState["SOPENDINGDATA"] = dt;
                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;

                        //ShowingGroupingDataInGridView(gvList.Rows, 0, 3);
                        int grater10 = 0;
                        int btw8to10 = 0;
                        int btw5to7 = 0;
                        int less4 = 0;

                        string find10 = "AGE > '10'";
                        DataRow[] foundRows10 = dt.Select(find10);
                        if (foundRows10.Length > 0)
                        {
                            grater10 = foundRows10.Length;
                        }


                        string find8to10 = "AGE >= '8' AND AGE <= '10'";
                        DataRow[] foundRows8to10 = dt.Select(find8to10);
                        if (foundRows8to10.Length > 0)
                        {
                            btw8to10 = foundRows8to10.Length;
                        }

                        string find8to7 = "AGE >= '5' AND AGE <= '7'";
                        DataRow[] foundRows5to7 = dt.Select(find8to7);
                        if (foundRows5to7.Length > 0)
                        {
                            btw5to7 = foundRows5to7.Length;
                        }

                        string findless4 = "AGE <= '4'";
                        DataRow[] foundRowsless4 = dt.Select(findless4);
                        if (foundRowsless4.Length > 0)
                        {
                            less4 = foundRowsless4.Length;
                        }


                        btnGreater10.Text = "> 10  - (" + grater10 + ")";
                        btnbtw8to10.Text = ">= 8 to <= 10 - (" + btw8to10 + ")";
                        btnbtw5to7.Text = ">= 5 to <= 7 - (" + btw5to7 + ")";
                        btnless4.Text = "<= 4 - (" + less4 + ")";

                        if (grater10 == 0)
                        {
                            btnGreater10.Enabled = false;
                        }
                        else
                        {
                            btnGreater10.Enabled = true;
                        }
                        if (btw8to10 == 0)
                        {
                            btnbtw8to10.Enabled = false;
                        }
                        else
                        {
                            btnbtw8to10.Enabled = true;
                        }
                        if (btw5to7 == 0)
                        {
                            btnbtw5to7.Enabled = false;
                        }
                        else
                        {
                            btnbtw5to7.Enabled = true;
                        }
                        if (less4 == 0)
                        {
                            btnless4.Enabled = false;
                        }
                        else
                        {
                            btnless4.Enabled = true;
                        }

                    }
                    else
                    {
                        //dt.DefaultView.Sort = SortColumn + " " + SortDirection;
                        gvList.DataSource = string.Empty;
                        gvList.DataBind();
                        //ViewState["SOPENDINGDATA"] = dt;
                        btnGreater10.Enabled = false;
                        btnbtw8to10.Enabled = false;
                        btnbtw5to7.Enabled = false;
                        btnless4.Enabled = false;
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
                string attachment = "attachment; filename=PendingSO.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vdn.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gvList.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void gvList_Sorting(object sender, GridViewSortEventArgs e)
        {
            //SortDirection = (SortDirection == "ASC") ? "DESC" : "ASC";
            //SortColumn = e.SortExpression;

            //DataTable dt = (DataTable)ViewState["SOPENDINGDATA"];
            //dt.DefaultView.Sort = SortColumn + " " + SortDirection;
            //gvList.DataSource = dt;
            //gvList.DataBind();
            //ViewState["SOPENDINGDATA"] = dt;

        }

        protected void btnGreater10_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    string AGE = " AND DATEDIFF(DAY,A.SODT,GETDATE()) > 10 ";
                    dt = objMainClass.GetSOAge(objMainClass.intCmpId, txtFromDocDate.Text, txtToDocDate.Text, "", "", AGE);

                    if (dt.Rows.Count > 0)
                    {
                        gvList.DataSource = dt;
                        gvList.DataBind();
                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
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

        protected void btnbtw8to10_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    string AGE = " AND DATEDIFF(DAY,A.SODT,GETDATE()) >= 8 AND DATEDIFF(DAY,A.SODT,GETDATE()) <= 10 ";
                    dt = objMainClass.GetSOAge(objMainClass.intCmpId, txtFromDocDate.Text, txtToDocDate.Text, "", "", AGE);

                    if (dt.Rows.Count > 0)
                    {
                        gvList.DataSource = dt;
                        gvList.DataBind();
                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
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

        protected void btnbtw5to7_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    string AGE = " AND DATEDIFF(DAY,A.SODT,GETDATE()) >= 5 AND DATEDIFF(DAY,A.SODT,GETDATE()) <= 7 ";
                    dt = objMainClass.GetSOAge(objMainClass.intCmpId, txtFromDocDate.Text, txtToDocDate.Text, "", "", AGE);

                    if (dt.Rows.Count > 0)
                    {
                        gvList.DataSource = dt;
                        gvList.DataBind();
                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
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

        protected void btnless4_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    DataTable dt = new DataTable();
                    string AGE = "  AND DATEDIFF(DAY,A.SODT,GETDATE()) <= 4 ";
                    dt = objMainClass.GetSOAge(objMainClass.intCmpId, txtFromDocDate.Text, txtToDocDate.Text, "", "", AGE);

                    if (dt.Rows.Count > 0)
                    {
                        gvList.DataSource = dt;
                        gvList.DataBind();
                        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
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


        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    lnkSerch_Click(1, e);
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

        //protected void btnGroupBy_Click(object sender, EventArgs e)
        //{
        //    GridViewHelper
        //}


        //void ShowingGroupingDataInGridView(GridViewRowCollection gridViewRows, int startIndex, int totalColumns)
        //{
        //    if (totalColumns == 0) return;
        //    int i, count = 1;
        //    ArrayList lst = new ArrayList();
        //    lst.Add(gridViewRows[0]);
        //    var ctrl = gridViewRows[0].Cells[startIndex];
        //    for (i = 1; i < gridViewRows.Count; i++)
        //    {
        //        TableCell nextTbCell = gridViewRows[i].Cells[startIndex];
        //        if (ctrl.Text == nextTbCell.Text)
        //        {
        //            count++;
        //            nextTbCell.Visible = false;
        //            lst.Add(gridViewRows[i]);
        //        }
        //        else
        //        {
        //            if (count > 1)
        //            {
        //                ctrl.RowSpan = count;
        //                ShowingGroupingDataInGridView(new GridViewRowCollection(lst), startIndex + 1, totalColumns - 1);
        //            }
        //            count = 1;
        //            lst.Clear();
        //            ctrl = gridViewRows[i].Cells[startIndex];
        //            lst.Add(gridViewRows[i]);
        //        }
        //    }
        //    if (count > 1)
        //    {
        //        ctrl.RowSpan = count;
        //        ShowingGroupingDataInGridView(new GridViewRowCollection(lst), startIndex + 1, totalColumns - 1);
        //    }
        //    count = 1;
        //    lst.Clear();
        //}


    }
}