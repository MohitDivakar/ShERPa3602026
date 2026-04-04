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
namespace ShERPa360net.TS
{
    public partial class DispatchEntry : System.Web.UI.Page
    {
        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        #region PAGEEVENT
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["USERID"] != null)
                    {
                        objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), menutabid.Value, "");
                        if (FormRights.bView == false)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }

                        txtDispatchDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");
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
        #endregion

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                UploadExcelFileDetail();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ResetFormControl();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        protected void btnSaveDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvAssignment.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please upload at least one Record to Import.');", true);
                }
                else
                {
                    var lstDispatch = GetGridJsonValue();
                    string DispatchImportJson = JsonConvert.SerializeObject(lstDispatch);
                   
                    int result = objMainClass.BULKADDUPDATEDISPATCHENTRY(DispatchImportJson, txtDispatchDate.Text, Convert.ToInt32(Session["USERID"].ToString()));
                    if (result > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Records. :" + lstDispatch.Count.ToString() + " Dipatch Import sucessfully." + "\");", true);
                        gvAssignment.DataSource = null;
                        gvAssignment.DataBind();
                        lgrecordcount.InnerText = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UploadExcelFileDetail()
        {
            try
            {
                OleDbConnection MyConnection;
                string extension = Path.GetExtension(flNotification.FileName);
                string folderpath = "~/assets/";
                string filePath = Path.Combine(Server.MapPath(folderpath), Guid.NewGuid().ToString("N") + extension);
                flNotification.SaveAs(filePath);
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

                gvAssignment.DataSource = null;
                gvAssignment.DataBind();

                var objDispatch = GetDispatchDetail(dt);
                gvAssignment.DataSource = objDispatch.lstDispatchDetail;
                gvAssignment.DataBind();
                lgrecordcount.InnerText = "Ready to Update Record :" + objDispatch.totalcorrectvalue + " Not to Update Record :" + objDispatch.totalrejectvalue + " From Total Records : " + dt.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public List<DispatchDetail> GetGridJsonValue()
        {
            List<DispatchDetail> objlstDispatchImport = new List<DispatchDetail>();
            string DispatchImportJson = string.Empty;
            try
            {
                for (int i = 0; i < gvAssignment.Rows.Count; i++)
                {
                    GridViewRow row = gvAssignment.Rows[i];
                    if (Convert.ToInt32(((HiddenField)row.FindControl("hdIscorrected")).Value) == 1)
                    {
                        DispatchDetail objDispatchDetail = new DispatchDetail();
                        objDispatchDetail.ESNNO = ((Label)row.FindControl("lblESNNo")).Text;
                        objlstDispatchImport.Add(objDispatchDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return objlstDispatchImport;
        }

        public void ResetFormControl()
        {
            try
            {
                gvAssignment.DataSource = null;
                gvAssignment.DataBind();
                lgrecordcount.InnerText = "0";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public DispatchUploadValue GetDispatchDetail(DataTable dtDispatchDetail)
        {
            DispatchUploadValue objDispatchUploadValue = new DispatchUploadValue();
            try
            {
                List<DispatchDetail> lstDispatchDetail = new List<DispatchDetail>();
                int correctvalue = 0;
                int incorrectvalue = 0;

                if (dtDispatchDetail.Rows.Count > 0)
                {
                    for (int i = 0; i < dtDispatchDetail.Rows.Count; i++)
                    {
                        DispatchDetail objNotificationImport = new DispatchDetail();
                        if (dtDispatchDetail.Rows[i]["ESN NO"].ToString().Length != 12)
                        {
                            objNotificationImport.ESNNO = dtDispatchDetail.Rows[i]["ESN NO"].ToString();
                            objNotificationImport.ISDISPATCHCORRECTED = 0;
                            incorrectvalue = incorrectvalue + 1;
                        }
                        else
                        {
                            objNotificationImport.ESNNO               = dtDispatchDetail.Rows[i]["ESN NO"].ToString();
                            objNotificationImport.ISDISPATCHCORRECTED = 1;
                            correctvalue                              = correctvalue + 1;
                        }
                        lstDispatchDetail.Add(objNotificationImport);
                    }
                    objDispatchUploadValue.lstDispatchDetail = lstDispatchDetail;
                    objDispatchUploadValue.totalcorrectvalue = correctvalue;
                    objDispatchUploadValue.totalrejectvalue = incorrectvalue;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return objDispatchUploadValue;
        }

        protected void gvAssignment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int IsDispatchcorrected = Convert.ToInt32((e.Row.FindControl("hdIscorrected") as HiddenField).Value);
                    if (IsDispatchcorrected == 1)
                    {
                        (e.Row.FindControl("lblESNNo") as Label).ForeColor = System.Drawing.Color.Black;
                    }
                    else
                    {
                        (e.Row.FindControl("lblESNNo") as Label).ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}
