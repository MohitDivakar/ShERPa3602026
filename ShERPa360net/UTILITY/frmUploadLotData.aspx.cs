using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.UTILITY
{
    public partial class frmUploadLotData : System.Web.UI.Page
    {

        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        WAClass objWAClass = new WAClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["CROMALOTDATA"] = null;
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

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    OleDbConnection MyConnection;
                    string extension = Path.GetExtension(fuImage.FileName);
                    string folderpath = "~/excel/";
                    string filePath = Path.Combine(Server.MapPath(folderpath), Guid.NewGuid().ToString("N") + extension);
                    fuImage.SaveAs(filePath);
                    DataTable dt = new DataTable();
                    if (extension == ".csv")
                    {
                        var items = (from line in System.IO.File.ReadAllLines(filePath)
                                     select Array.ConvertAll(line.Split(','), v => v.ToString().TrimStart("\" ".ToCharArray()).TrimEnd("\" ".ToCharArray()))).ToArray();
                        string[] strarr1 = items[0];
                        for (int x = 0; x <= items[0].Count() - 1; x++)
                            dt.Columns.Add(strarr1[x]);
                        foreach (var a in items)
                        {
                            DataRow dr = dt.NewRow();
                            dr.ItemArray = a;
                            dt.Rows.Add(dr);
                        }
                        if (dt.Rows.Count > 0)
                        {
                            dt.Rows.RemoveAt(0);
                        }
                    }
                    else
                    {
                        MyConnection = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 12.0;");
                        //MyConnection = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + flUpload.FileName + ";Extended Properties=Excel 8.0;");
                        MyConnection.Open();
                        DataTable dtExcelSchema = MyConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null/* TODO Change to default(_) if this is not a reference type */);
                        string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                        using (OleDbCommand cmd = new OleDbCommand())
                        {
                            using (OleDbDataAdapter oda = new OleDbDataAdapter())
                            {
                                cmd.CommandText = (Convert.ToString("SELECT * From [Sheet1$]"));
                                cmd.Connection = MyConnection;
                                oda.SelectCommand = cmd;
                                oda.Fill(dt);
                                MyConnection.Close();
                            }
                        }
                    }
                    File.Delete(filePath);

                    for (int i = 0; i < grvLead.Columns.Count; i++)
                    {
                        int k = 0;
                        DataControlField field = grvLead.Columns[i];
                        BoundField bfield = field as BoundField;
                        string cc1 = bfield.DataField;

                        for (int j = 0; j < dt.Columns.Count; j++)
                        {


                            string title = dt.Columns[j].ColumnName;
                            if (cc1 == title)
                            {
                                k++;
                            }
                        }

                        if (k > 0)
                        {

                        }
                        else
                        {
                            string Message = "Column name '" + cc1 + "' not found in uploaded excel.";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + Message + "\");", true);
                            return;
                        }
                    }

                    decimal QTEKPRICESUM = 0;
                    decimal SALEPRICESUM = 0;
                    for (int m = 0; m < dt.Rows.Count; m++)
                    {
                        string RPASiteCode = Convert.ToString(dt.Rows[m]["RPA Site Code"]);
                        string CromaLotNo = Convert.ToString(dt.Rows[m]["Croma Lot No"]);
                        string QTEKLotNo = Convert.ToString(dt.Rows[m]["QTEK Lot No"]);
                        string ArticleNo = Convert.ToString(dt.Rows[m]["Article No"]);
                        string ItemCode = Convert.ToString(dt.Rows[m]["Item Code"]);
                        string ItemDesc = Convert.ToString(dt.Rows[m]["Item Desc"]);
                        string Product = Convert.ToString(dt.Rows[m]["Product"]);
                        string SerialNo = Convert.ToString(dt.Rows[m]["Serial No"]);
                        string Grade = Convert.ToString(dt.Rows[m]["Grade"]);
                        string Brand = Convert.ToString(dt.Rows[m]["Brand"]);
                        string InwardScanID = Convert.ToString(dt.Rows[m]["Inward Scan ID"]);

                        decimal QTEKPrice = Convert.ToDecimal(dt.Rows[m]["QTEK Price"]);
                        decimal SalesPrice = Convert.ToDecimal(dt.Rows[m]["Sales Price"]);

                        if (RPASiteCode == null || RPASiteCode == string.Empty || RPASiteCode == "")
                        {
                            string Message = "RPA Site Code not insered in line number " + (m + 1);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                            return;
                        }

                        if (CromaLotNo == null || CromaLotNo == string.Empty || CromaLotNo == "")
                        {
                            string Message = "Croma Lot No not insered in line number " + (m + 1);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                            return;
                        }

                        if (QTEKLotNo == null || QTEKLotNo == string.Empty || QTEKLotNo == "")
                        {
                            string Message = "QTEK Lot No not insered in line number " + (m + 1);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                            return;
                        }

                        if (ArticleNo == null || ArticleNo == string.Empty || ArticleNo == "")
                        {
                            string Message = "Article No not insered in line number " + (m + 1);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                            return;
                        }

                        if (ItemCode == null || ItemCode == string.Empty || ItemCode == "")
                        {
                            string Message = "Item Code not insered in line number " + (m + 1);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                            return;
                        }

                        if (ItemDesc == null || ItemDesc == string.Empty || ItemDesc == "")
                        {
                            string Message = "Item Desc not insered in line number " + (m + 1);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                            return;
                        }

                        if (Product == null || Product == string.Empty || Product == "")
                        {
                            string Message = "Product not insered in line number " + (m + 1);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                            return;
                        }

                        if (SerialNo == null || SerialNo == string.Empty || SerialNo == "")
                        {
                            string Message = "Serial No not insered in line number " + (m + 1);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                            return;
                        }

                        if (Grade == null || Grade == string.Empty || Grade == "")
                        {
                            string Message = "Grade not insered in line number " + (m + 1);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                            return;
                        }

                        if (InwardScanID == null || InwardScanID == string.Empty || InwardScanID == "")
                        {
                            string Message = "Inward Scan ID not insered in line number " + (m + 1);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                            return;
                        }

                        if (Brand == null || Brand == string.Empty || Brand == "")
                        {
                            string Message = "Brand not insered in line number " + (m + 1);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                            return;
                        }

                        QTEKPRICESUM = QTEKPRICESUM + QTEKPrice;
                        SALEPRICESUM = SALEPRICESUM + SalesPrice;


                    }



                    grvLead.DataSource = string.Empty;
                    grvLead.DataBind();

                    grvLead.DataSource = dt;
                    grvLead.DataBind();

                    if (dt.Rows.Count > 0)
                    {
                        lblRecoretxt.Visible = true;
                        lblRecord.Visible = true;
                        txtQtekPriceSum.Visible = true;
                        txtSalePriceSum.Visible = true;
                        lblSalePricesum.Visible = true;
                        lblQtekPriceSum.Visible = true;

                        lblSalePricesum.Text = Convert.ToString(Math.Round(SALEPRICESUM));
                        lblQtekPriceSum.Text = Convert.ToString(Math.Round(QTEKPRICESUM));
                        lblRecord.Text = dt.Rows.Count.ToString();
                        lnkSave.Visible = true;

                        txtBidStartAmt.Text = lblSalePricesum.Text;
                        txtMinimumBidAmt.Text = "1000";

                        txtBidStartDt.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");
                        txtBidEndDt.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        lblRecoretxt.Visible = false;
                        lblRecord.Visible = false;
                        txtQtekPriceSum.Visible = false;
                        txtSalePriceSum.Visible = false;
                        lblSalePricesum.Visible = false;
                        lblQtekPriceSum.Visible = false;

                        lblSalePricesum.Text = "0";
                        lblQtekPriceSum.Text = "0";
                        lblRecord.Text = "0";
                        lnkSave.Visible = false;
                        txtBidStartAmt.Text = lblSalePricesum.Text;
                        txtMinimumBidAmt.Text = "1000";
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
                    //string filePath = "~/UTILITY/Croma Lot Upload File.xlsx";
                    string filePath = Server.MapPath("~/UTILITY/Croma Lot Upload File.xlsx");
                    FileInfo file = new FileInfo(filePath);
                    if (file.Exists)
                    {
                        Response.Clear();
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                        Response.AddHeader("Content-Length", file.Length.ToString());
                        Response.ContentType = "text/plain";
                        Response.Flush();
                        Response.TransmitFile(file.FullName);
                        Response.End();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('File not found for Download.!');", true);
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

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            string exceptioncu = "";
            DataTable dtReturn = new DataTable();
            dtReturn.Columns.Add("SERIAL NO");
            dtReturn.Columns.Add("MESSAGE");
            dtReturn.Columns.Add("ACTUAL SERIAL No");
            int ierror = 0;
            int isucess = 0;
            //InsertRateCard
            try
            {
                if (Session["USERID"] != null)
                {
                    if (grvLead.Rows.Count > 0)
                    {
                        string startdt, enddt, currdate;
                        startdt = Convert.ToDateTime(txtBidStartDt.Text).ToShortDateString() + " 00:00:00";
                        enddt = Convert.ToDateTime(txtBidEndDt.Text).ToShortDateString() + " 23:59:59";
                        currdate = DateTime.Now.ToShortDateString() + " 00:00:00";


                        if (Convert.ToDateTime(startdt) < Convert.ToDateTime(currdate))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Bid Start Date is always greater than or equal to crrent date..!!');", true);
                        }
                        else
                        {
                            if (startdt == enddt && chkBidSetting.Checked == true)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Bid Start Date and End Date are same. Please change End Date ..!!');", true);
                            }
                            else
                            {
                                if (Convert.ToDateTime(startdt) > Convert.ToDateTime(enddt) && chkBidSetting.Checked == true)
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Bid Start Date is always less than to Bid End date..!!');", true);
                                }
                                else
                                {

                                    if (Convert.ToDecimal(txtBidStartAmt.Text) < Convert.ToDecimal(lblSalePricesum.Text))
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Bid Start Amount should be greater than or equal to Total Sales Price..!!');", true);
                                    }
                                    else
                                    {
                                        if (Convert.ToDecimal(txtMinimumBidAmt.Text) % 1000 == 0)
                                        {


                                            decimal totalqty = 0;
                                            decimal totalmrp = 0;
                                            decimal totalmap = 0;
                                            decimal totalasp = 0;
                                            decimal totalqtekprice = 0;
                                            decimal totalsalesprice = 0;

                                            for (int j = 0; j < grvLead.Rows.Count; j++)
                                            {
                                                GridViewRow row = grvLead.Rows[j];
                                                decimal QTY = Convert.ToDecimal(row.Cells[11].Text);
                                                decimal MRP = Convert.ToDecimal(row.Cells[13].Text);
                                                decimal MAP = Convert.ToDecimal(row.Cells[14].Text);
                                                decimal OnlinePrice = Convert.ToDecimal(row.Cells[15].Text);
                                                decimal QTEKPRICE = Convert.ToDecimal(row.Cells[25].Text);
                                                decimal SALESPRICE = Convert.ToDecimal(row.Cells[27].Text);


                                                totalqty = totalqty + QTY;
                                                totalmrp = totalmrp + MRP;
                                                totalmap = totalmap + MAP;
                                                totalasp = totalasp + OnlinePrice;
                                                totalqtekprice = totalqtekprice + QTEKPRICE;
                                                totalsalesprice = totalsalesprice + SALESPRICE;

                                            }
                                            GridViewRow row1 = grvLead.Rows[0];
                                            string RPASITECODE = Convert.ToString(row1.Cells[0].Text);
                                            string CROMALOTNO = Convert.ToString(row1.Cells[1].Text);
                                            string QTEKLOTNO = Convert.ToString(row1.Cells[2].Text);
                                            string Location = Convert.ToString(row1.Cells[28].Text);

                                            int bid = 0;
                                            string datest = "";
                                            string dateed = "";
                                            string bidamt = "";
                                            string miniamt = "";
                                            int BIDID = 0;

                                            if (chkBidSetting.Checked == true)
                                            {
                                                bid = 1;
                                                datest = txtBidStartDt.Text;
                                                dateed = txtBidEndDt.Text;
                                                bidamt = txtBidStartAmt.Text;
                                                miniamt = txtMinimumBidAmt.Text;

                                                DataTable dtbid = new DataTable();
                                                dtbid = objMainClass.GetCromaLotData(objMainClass.intCmpId, 0, "", "", "", "MAXBIDID");
                                                if (dtbid.Rows.Count > 0)
                                                {
                                                    int idbid = Convert.ToInt32(dtbid.Rows[0]["BIDID"]);
                                                    BIDID = idbid + 1;
                                                }
                                                else
                                                {
                                                    BIDID = BIDID + 1;
                                                }
                                            }

                                            int i = objMainClass.InsertCromaLotNew(objMainClass.intCmpId, RPASITECODE, CROMALOTNO, QTEKLOTNO, totalqty, totalmrp, totalmap, totalasp, totalqtekprice, totalsalesprice, Location,
                                                grvLead, 1, chkShowLot.Checked == true ? 1 : 0, Convert.ToInt32(Session["USERID"]), bid, BIDID, datest, dateed, bidamt != "" ? Convert.ToDecimal(bidamt) : 0,
                                                miniamt != "" ? Convert.ToDecimal(miniamt) : 0, "INSERTMASTER");

                                            if (i == 1)
                                            {
                                                string message = "Lot Uploaded Successfully...!!";
                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Sucess : " + message + "\");$('.close').click(function(){window.location.href ='frmUploadLotData.aspx' });", true);
                                            }
                                            else
                                            {
                                                string message = "Lot Not Uploaded Successfully. Please Try Again...!!";
                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\" Error : " + message + "\");$('.close').click(function(){window.location.href ='frmUploadLotData.aspx' });", true);
                                            }



                                            //for (int i = 0; i < grvLead.Rows.Count; i++)
                                            //{
                                            //    GridViewRow row = grvLead.Rows[i];
                                            //    string RPASITECODE = Convert.ToString(row.Cells[0].Text);
                                            //    string CROMALOTNO = Convert.ToString(row.Cells[1].Text);
                                            //    string QTEKLOTNO = Convert.ToString(row.Cells[2].Text);
                                            //    string INWARDSCANID = Convert.ToString(row.Cells[3].Text);
                                            //    int SRNO = Convert.ToInt32(row.Cells[4].Text);
                                            //    string ARTICLENO = Convert.ToString(row.Cells[5].Text);
                                            //    string ITEMCODE = Convert.ToString(row.Cells[6].Text);
                                            //    string ITEMDESC = Convert.ToString(row.Cells[7].Text);
                                            //    string BRAND = Convert.ToString(row.Cells[8].Text);
                                            //    string PRODUCT = Convert.ToString(row.Cells[9].Text);
                                            //    string SERIALNO = Convert.ToString(row.Cells[10].Text);
                                            //    int QTY = Convert.ToInt32(row.Cells[11].Text);
                                            //    string GRADE = Convert.ToString(row.Cells[12].Text);
                                            //    decimal MRP = Convert.ToDecimal(row.Cells[13].Text);
                                            //    decimal MAP = Convert.ToDecimal(row.Cells[14].Text);
                                            //    decimal OnlinePrice = Convert.ToDecimal(row.Cells[15].Text);
                                            //    decimal ASPGST = Convert.ToDecimal(row.Cells[16].Text);
                                            //    decimal POAMT = Convert.ToDecimal(row.Cells[17].Text);
                                            //    decimal INVVALUE = Convert.ToDecimal(row.Cells[18].Text);
                                            //    decimal AVGRECOVERYPER = Convert.ToDecimal(row.Cells[19].Text);
                                            //    decimal RECOVERYWOMARKUP = Convert.ToDecimal(row.Cells[20].Text);
                                            //    decimal MARKUPBRANDPER = Convert.ToDecimal(row.Cells[21].Text);
                                            //    decimal FINALRECOVERYWOGST = Convert.ToDecimal(row.Cells[22].Text);
                                            //    decimal GSTPER = Convert.ToDecimal(row.Cells[23].Text);
                                            //    decimal GSTAMT = Convert.ToDecimal(row.Cells[24].Text);
                                            //    decimal QTEKPRICE = Convert.ToDecimal(row.Cells[25].Text);
                                            //    decimal OLDPRICE = Convert.ToDecimal(row.Cells[25].Text);
                                            //    decimal SALESPRICE = Convert.ToDecimal(row.Cells[27].Text);
                                            //    string Location = Convert.ToString(row.Cells[28].Text);

                                            //    int isave = objMainClass.InsertCromaLot(objMainClass.intCmpId, RPASITECODE, CROMALOTNO, QTEKLOTNO, INWARDSCANID, SRNO, ARTICLENO, ITEMCODE, ITEMDESC, BRAND, PRODUCT, SERIALNO, QTY, GRADE, MRP, MAP, OnlinePrice, ASPGST, POAMT, INVVALUE, AVGRECOVERYPER, RECOVERYWOMARKUP,
                                            //        MARKUPBRANDPER, FINALRECOVERYWOGST, GSTPER, GSTAMT, QTEKPRICE, OLDPRICE, SALESPRICE, "", "", 1, Convert.ToInt32(Session["USERID"]), Location, "INSERT");
                                            //    if (isave == 1)
                                            //    {
                                            //        dtReturn.Rows.Add(SERIALNO, "This Serial No. updated successfully.!", "");
                                            //        isucess++;
                                            //    }
                                            //    else
                                            //    {
                                            //        dtReturn.Rows.Add(SERIALNO, "This Serial No. not updated successfully.!", "");
                                            //        ierror++;
                                            //    }

                                            //}

                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Minimum bid increament amount should be multiple of 1000 ..!!');", true);

                                        }
                                    }
                                }
                            }
                        }



                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please upload at least one Record to Import.');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Session Expired. Please Log In again.');$('.close').click(function(){window.location.href ='../Login.aspx' });", true);
                }
            }
            catch (Exception ex)
            {
                exceptioncu = ex.Message.Replace("'", "");
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            //finally
            //{
            //    Session["CROMALOTDATA"] = dtReturn;
            //    if (dtReturn.Rows.Count > 0)
            //    {
            //        string message = "Total data uploaded - " + grvLead.Rows.Count + ". Successfully uploaded -" + isucess + ". Error in uploaded data - " + ierror + ". Please check downloaded file for more details.";
            //        if (isucess > 0)
            //        {
            //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Sucess : " + message + "\");$('.close').click(function(){window.location.href ='frmUploadLotData.aspx' });", true);
            //        }
            //        else
            //        {
            //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\" Sucess : " + message + "\");$('.close').click(function(){window.location.href ='frmUploadLotData.aspx' });", true);
            //        }
            //        string path = "frmCromaLotDataDownload.aspx";
            //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "');", true);
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + exceptioncu + "\");", true);

            //        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Something went wrong. Please try again.');$('.close').click(function(){window.location.href ='frmUploadLotData.aspx' });", true);
            //    }
            //}
        }

        protected void chkBidSetting_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (chkBidSetting.Checked == true)
                    {
                        chkShowLot.Checked = true;
                        divBidSetting.Visible = true;
                    }
                    else
                    {
                        chkShowLot.Checked = false;
                        divBidSetting.Visible = false;
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
}