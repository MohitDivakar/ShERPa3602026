using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using ShERPa360net.Class;
using ShERPa360net.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.REPORTS
{
    public partial class SkuUpload : System.Web.UI.Page
    {
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        DataTable dtItem = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                UploadSKUFile();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').moal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public void UploadSKUFile()
        {
            try
            {
                OleDbConnection MyConnection;
                string extension = Path.GetExtension(skufile.FileName);
                string folderpath = "~/assets/";
                string filePath = Path.Combine(Server.MapPath(folderpath), Guid.NewGuid().ToString("N") + extension);
                skufile.SaveAs(filePath);
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
                    MyConnection.Open();
                    DataTable dtExcelSchema = MyConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null/* TODO Change to default(_) if this is not a reference type */);
                    string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        using (OleDbDataAdapter od = new OleDbDataAdapter())
                        {
                            cmd.CommandText = (Convert.ToString("SELECT * From [Sheet1$]"));
                            cmd.Connection = MyConnection;
                            od.SelectCommand = cmd;
                            od.Fill(dt);
                            MyConnection.Close();
                        }
                    }

                }
                File.Delete(filePath);

                gvasindetail.DataSource = null;
                gvasindetail.DataBind();

                var objBulkAsinCreationValue = GetBulkasindetail(dt);
                gvasindetail.DataSource = objBulkAsinCreationValue.lstBulkAsinCreationDetail;
                gvasindetail.DataBind();

                if (objBulkAsinCreationValue.lstBulkAsinCreationDetail.Count > 0)
                {
                    gvasindetail.HeaderRow.TableSection = TableRowSection.TableHeader;
                    btnSave.Visible = true;
                }
                else
                {
                    btnSave.Visible = false;
                }
                lgrecordcount.InnerText = "Ready to Insert Record :" + objBulkAsinCreationValue.totalcorrectvalue + " Not Insert Record :" + objBulkAsinCreationValue.totalrejectvalue + " From Total Records : " + dt.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public Bulkasindetail GetBulkasindetail(DataTable dt)
        {
            Bulkasindetail objBulkAsinCreationUploadValue = new Bulkasindetail();
            try
            {
                List<BulkAsinCreationDetails> lstBulkAsinCreationDetail = new List<BulkAsinCreationDetails>();
                int correctvalue = 0;
                int incorrectvalue = 0;

                if (dt.Rows.Count > 0)
                {
                    bool Isincorrect = false;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Isincorrect = false;
                        BulkAsinCreationDetails objAsinValue = new BulkAsinCreationDetails();

                        objAsinValue.SKUASIN_NO = Convert.ToString(dt.Rows[i]["asin"].ToString());
                        objAsinValue.SKUASIN_ITEMNAME = Convert.ToString(dt.Rows[i]["item name"].ToString()); ;
                        lstBulkAsinCreationDetail.Add(objAsinValue);
                    }
                }
                objBulkAsinCreationUploadValue.lstBulkAsinCreationDetail = lstBulkAsinCreationDetail;
                objBulkAsinCreationUploadValue.totalcorrectvalue = correctvalue;
                objBulkAsinCreationUploadValue.totalrejectvalue = incorrectvalue;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return objBulkAsinCreationUploadValue;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvasindetail.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please upload at least one Record to Import.');", true);
                }
                else
                {
                    var lstasincreation = GetGridJsonValue();
                    string BulkAsinImportJson = JsonConvert.SerializeObject(lstasincreation);
                    int result = objMainClass.BulkAsinCreation(BulkAsinImportJson, "UPDATE");

                    if (result > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Records. :" + lstasincreation.Count.ToString() + "  Created sucessfully." + "\");", true);
                        gvasindetail.DataSource = null;
                        gvasindetail.DataBind();
                        lgrecordcount.InnerText = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<BulkAsinCreationDetails> GetGridJsonValue()
        {
            List<BulkAsinCreationDetails> objlstasinvalue = new List<BulkAsinCreationDetails>();
            string NotificationImportJson = string.Empty;
            try
            {
                for (int i = 0; i < gvasindetail.Rows.Count; i++)
                {
                    BulkAsinCreationDetails objAsinValue = new BulkAsinCreationDetails();
                    GridViewRow row = gvasindetail.Rows[i];
                    objAsinValue.SKUASIN_NO = ((Label)row.FindControl("lblasin")).Text;
                    objAsinValue.SKUASIN_ITEMNAME = ((Label)row.FindControl("lblitemname")).Text;
                    objlstasinvalue.Add(objAsinValue);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return objlstasinvalue;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int result = objMainClass.BulkAsinCreation("","DELETE");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Records  Deleted sucessfully." + "\");", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

