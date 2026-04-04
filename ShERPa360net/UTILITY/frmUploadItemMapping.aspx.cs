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
    public partial class frmUploadItemMapping : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        DataTable dtItem = new DataTable();

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

                        if (FormRights.bAdd == false)
                        {
                            btnSave.Enabled = false;
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
                    UploadExcelFileDetail();
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

        public void UploadExcelFileDetail()
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    OleDbConnection MyConnection;
                    string extension = Path.GetExtension(fuImage.FileName);
                    string folderpath = "~/assets/";
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
                                cmd.CommandText = (Convert.ToString("SELECT * From [" + sheetName + "]"));
                                cmd.Connection = MyConnection;
                                oda.SelectCommand = cmd;
                                oda.Fill(dt);
                                MyConnection.Close();
                            }
                        }
                    }
                    File.Delete(filePath);

                    gvDetail.DataSource = null;
                    gvDetail.DataBind();

                    //var objBulkSoCreationValue = GetBulkSoCreationDetail(dt);
                    gvDetail.DataSource = dt;
                    gvDetail.DataBind();

                    if (gvDetail.Rows.Count > 0)
                    {
                        gvDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
                        btnSave.Visible = true;
                        if (FormRights.bAdd == false)
                        {
                            btnSave.Enabled = false;
                        }
                        else
                        {
                            btnSave.Enabled = true;
                        }
                    }
                    else
                    {
                        btnSave.Visible = false;
                        if (FormRights.bAdd == false)
                        {
                            btnSave.Enabled = false;
                        }
                        else
                        {
                            btnSave.Enabled = true;
                        }

                    }
                    //lgrecordcount.InnerText = "Ready to So Insert Record :" + objBulkSoCreationValue.totalcorrectvalue + " Not Insert So Record :" + objBulkSoCreationValue.totalrejectvalue + " From Total Records : " + dt.Rows.Count.ToString();
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    int NEWADD = 0;
                    int OLDUPDATE = 0;
                    if (gvDetail.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvDetail.Rows.Count; i++)
                        {
                            GridViewRow row = gvDetail.Rows[i];
                            string ITEMCODE = Convert.ToString(Convert.ToString(row.Cells[1].Text) == "&nbsp;" ? "" : Convert.ToString(row.Cells[1].Text));
                            string ITEMDESC = Convert.ToString(Convert.ToString(row.Cells[2].Text) == "&nbsp;" ? "" : Convert.ToString(row.Cells[2].Text));
                            string SKU = Convert.ToString(Convert.ToString(row.Cells[3].Text) == "&nbsp;" ? "" : Convert.ToString(row.Cells[3].Text));
                            string FLIPKART = Convert.ToString(Convert.ToString(row.Cells[4].Text) == "&nbsp;" ? "" : Convert.ToString(row.Cells[4].Text));
                            string AMAZON = Convert.ToString(Convert.ToString(row.Cells[5].Text) == "&nbsp;" ? "" : Convert.ToString(row.Cells[5].Text));
                            string NEWAMAZON = Convert.ToString(Convert.ToString(row.Cells[6].Text) == "&nbsp;" ? "" : Convert.ToString(row.Cells[6].Text));
                            string WEBSITE = Convert.ToString(Convert.ToString(row.Cells[7].Text) == "&nbsp;" ? "" : Convert.ToString(row.Cells[7].Text));
                            string SCWEBSITE = Convert.ToString(Convert.ToString(row.Cells[8].Text) == "&nbsp;" ? "" : Convert.ToString(row.Cells[8].Text));
                            string CFURL = Convert.ToString(Convert.ToString(row.Cells[9].Text) == "&nbsp;" ? "" : Convert.ToString(row.Cells[9].Text));
                            DataTable dt = new DataTable();
                            dt = objMainClass.GetItemMappingData(objMainClass.intCmpId, ITEMCODE, "", "", "", "", 0, "SEARCH", 0, 0);

                            if (dt.Rows.Count > 0)
                            {
                                int iResult = objMainClass.InsertItemMapping(objMainClass.intCmpId, ITEMCODE, ITEMDESC, SKU, FLIPKART, AMAZON, WEBSITE, 1,
                            Convert.ToInt32(Session["USERID"]), "", "", "", "UPDATEITEMMAPPED", SCWEBSITE, NEWAMAZON, CFURL);
                                if (iResult == 1)
                                {
                                    OLDUPDATE = OLDUPDATE + 1;
                                }

                            }
                            else
                            {
                                int iResult = objMainClass.InsertItemMapping(objMainClass.intCmpId, ITEMCODE, ITEMDESC, SKU, FLIPKART, AMAZON, WEBSITE, 1,
                             Convert.ToInt32(Session["USERID"]), "", "", "", "INSERTMAPPEDITEM", SCWEBSITE, NEWAMAZON, CFURL);
                                if (iResult == 1)
                                {
                                    NEWADD = NEWADD + 1;
                                }
                            }
                        }

                        int totalcnt = gvDetail.Rows.Count;
                        int remain = totalcnt - (NEWADD + OLDUPDATE);

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"Total Records : " + totalcnt + ". New Added : " + NEWADD + ". Updated Records : " + OLDUPDATE + ". Remain : " + remain + "! \");$('.close').click(function(){window.location.href ='frmViewMappedItem.aspx' });", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Data not available.');", true);
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