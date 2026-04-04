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
    public partial class frmUploadRateCard : System.Web.UI.Page
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
                Session["RATECARDDATA"] = null;
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

                    for (int m = 0; m < dt.Rows.Count; m++)
                    {
                        string itemcode = Convert.ToString(dt.Rows[m]["Article No"]);
                        string itemdesc = Convert.ToString(dt.Rows[m]["Item-Description"]);
                        string brand = Convert.ToString(dt.Rows[m]["Brand"]);
                        string category = Convert.ToString(dt.Rows[m]["Category"]);
                        string jobid = Convert.ToString(dt.Rows[m]["JOB ID"]);
                        string serialno = Convert.ToString(dt.Rows[m]["Serial No"]);
                        string onlineprice = Convert.ToString(dt.Rows[m]["Online Price"]);
                        string MRP = Convert.ToString(dt.Rows[m]["MRP"]);
                        string dealerprice = Convert.ToString(dt.Rows[m]["Dealer Price"]);
                        string customerprice = Convert.ToString(dt.Rows[m]["Customer Price"]);
                        string Size = Convert.ToString(dt.Rows[m]["Size"]);
                        string Grade = Convert.ToString(dt.Rows[m]["Grade"]);

                        if (itemcode == null || itemcode == string.Empty || itemcode == "")
                        {
                            string Message = "Item code not inserted in line number " + (m + 1);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                            return;
                        }

                        if (itemdesc == null || itemdesc == string.Empty || itemdesc == "")
                        {
                            string Message = "Item Description not inserted in line number " + (m + 1);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                            return;
                        }

                        if (brand == null || brand == string.Empty || brand == "")
                        {
                            string Message = "Brand Name not inserted in line number " + (m + 1);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                            return;
                        }

                        if (category == null || category == string.Empty || category == "")
                        {
                            string Message = "Category not inserted in line number " + (m + 1);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                            return;
                        }

                        if (jobid == null || jobid == string.Empty || jobid == "")
                        {
                            string Message = "Job ID not inserted in line number " + (m + 1);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                            return;
                        }

                        if (serialno == null || serialno == string.Empty || serialno == "")
                        {
                            string Message = "Serial No. not inserted in line number " + (m + 1);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                            return;
                        }

                        if (MRP == null || MRP == string.Empty || MRP == "")
                        {
                            string Message = "MRP not inserted in line number " + (m + 1);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                            return;
                        }

                        if (onlineprice == null || onlineprice == string.Empty || onlineprice == "")
                        {
                            string Message = "Onlinr Price not inserted in line number " + (m + 1);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                            return;
                        }

                        if (dealerprice == null || dealerprice == string.Empty || dealerprice == "")
                        {
                            string Message = "Dealer Price not inserted in line number " + (m + 1);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                            return;
                        }

                        if (customerprice == null || customerprice == string.Empty || customerprice == "")
                        {
                            string Message = "Customer Price not inserted in line number " + (m + 1);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                            return;
                        }

                        if (Size == null || Size == string.Empty || Size == "")
                        {
                            string Message = "Size not inserted in line number " + (m + 1);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                            return;
                        }

                        if (Grade == null || Grade == string.Empty || Grade == "")
                        {
                            string Message = "Grade not inserted in line number " + (m + 1);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                            return;
                        }


                    }



                    grvLead.DataSource = string.Empty;
                    grvLead.DataBind();

                    grvLead.DataSource = dt;
                    grvLead.DataBind();

                    if (dt.Rows.Count > 0)
                    {
                        lblRecoretxt.Visible = true;
                        lblRecord.Visible = true;

                        lblRecord.Text = dt.Rows.Count.ToString();
                        lnkSave.Visible = true;
                    }
                    else
                    {
                        lblRecoretxt.Visible = false;
                        lblRecord.Visible = false;

                        lblRecord.Text = "0";
                        lnkSave.Visible = false;
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
            DataTable dtReturn = new DataTable();
            dtReturn.Columns.Add("JOB ID");
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

                        for (int i = 0; i < grvLead.Rows.Count; i++)
                        {
                            GridViewRow row = grvLead.Rows[i];
                            string itemcode = Convert.ToString(row.Cells[0].Text);
                            string itemdesc = Convert.ToString(row.Cells[1].Text);
                            string brand = Convert.ToString(row.Cells[2].Text);
                            string category = Convert.ToString(row.Cells[3].Text);
                            string jobid = Convert.ToString(row.Cells[4].Text);
                            string serialno = Convert.ToString(row.Cells[5].Text);
                            string mrp = Convert.ToString(row.Cells[6].Text);
                            string onlineprice = Convert.ToString(row.Cells[7].Text);
                            string dealerprice = Convert.ToString(row.Cells[8].Text);
                            string customerprice = Convert.ToString(row.Cells[9].Text);
                            string size = Convert.ToString(row.Cells[10].Text);
                            string grade = Convert.ToString(row.Cells[11].Text);
                            string URL = Convert.ToString(row.Cells[12].Text);

                            StringWriter myWriter = new StringWriter();
                            HttpUtility.HtmlDecode(serialno.ToString(), myWriter);
                            serialno = myWriter.ToString();


                            DataTable dtjobimeicheck = new DataTable();
                            dtjobimeicheck = objMainClass.GetJobForLogistic(objMainClass.intCmpId, jobid, "", "", "", "", serialno, "", "", "GETJOBLIST");
                            if (dtjobimeicheck.Rows.Count > 0)
                            {

                                DataTable dtDuplicate = new DataTable();
                                dtDuplicate = objMainClass.CheckRateJobDuplicate(objMainClass.intCmpId, jobid, "CHECKDUPLICATE");
                                if (dtDuplicate.Rows.Count > 0)
                                {
                                    int isave = objMainClass.InsertRateCard(objMainClass.intCmpId, itemcode, itemdesc, brand, category, jobid, serialno, Convert.ToDecimal(dealerprice), Convert.ToDecimal(customerprice), 0, "", "", Convert.ToInt32(Session["USERID"]), "UPDATEPRICE", Convert.ToDecimal(mrp), size, grade, Convert.ToDecimal(onlineprice),URL);
                                    if (isave == 1)
                                    {
                                        dtReturn.Rows.Add(jobid, serialno, "This job id updated successfully.!", "");
                                        isucess++;
                                    }
                                    else
                                    {
                                        dtReturn.Rows.Add(jobid, serialno, "This job id not updated successfully.!", "");
                                        ierror++;
                                    }
                                }
                                else
                                {
                                    int isave = objMainClass.InsertRateCard(objMainClass.intCmpId, itemcode, itemdesc, brand, category, jobid, serialno, Convert.ToDecimal(dealerprice), Convert.ToDecimal(customerprice), 0, "", "", Convert.ToInt32(Session["USERID"]), "INSERT", Convert.ToDecimal(mrp), size, grade, Convert.ToDecimal(onlineprice),URL);
                                    if (isave == 1)
                                    {
                                        dtReturn.Rows.Add(jobid, serialno, "Job id uploaded successfully.!", "");
                                        isucess++;
                                    }
                                    else
                                    {
                                        dtReturn.Rows.Add(jobid, serialno, "This job id not uploaded.!", "");
                                        ierror++;
                                    }
                                }

                            }
                            else
                            {

                                DataTable dtjobimeicheckSer = new DataTable();
                                dtjobimeicheckSer = objMainClass.GetJobForLogistic(objMainClass.intCmpId, jobid, "", "", "", "", serialno, "", "", "GENERATEDATA");
                                if (dtjobimeicheckSer.Rows.Count > 0)
                                {
                                    dtReturn.Rows.Add(jobid, serialno, "Job id and Serial number not match in jobsheet.!", Convert.ToString(dtjobimeicheckSer.Rows[0]["IMEINO"]));
                                }
                                else
                                {
                                    dtReturn.Rows.Add(jobid, serialno, "Invalid Job ID. Job id not found in ShERPa.!", "");
                                }
                                ierror++;
                                //string Message = "Job id and Serial Number not found for line number " + (m + 1);
                                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"" + Message + "\");", true);
                                //return;
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            finally
            {
                Session["RATECARDDATA"] = dtReturn;
                string message = "Total data uploaded - " + grvLead.Rows.Count + ". Successfully uploaded -" + isucess + ". Error in uploaded data - " + ierror + ". Please check downloaded file for more details.";
                if (isucess > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Sucess : " + message + "\");$('.close').click(function(){window.location.href ='frmUploadRateCard.aspx' });", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\" Sucess : " + message + "\");$('.close').click(function(){window.location.href ='frmUploadRateCard.aspx' });", true);
                }
                string path = "frmRateCardDataDownload.aspx";
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + path + "');", true);
            }
        }

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    //string filePath = "~/UTILITY/Croma Ratecard Template.xlsx";
                    string filePath = Server.MapPath("~/UTILITY/Croma Ratecard Template.xlsx");
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
    }
}