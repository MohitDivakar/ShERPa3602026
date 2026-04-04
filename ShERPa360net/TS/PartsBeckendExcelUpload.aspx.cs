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
    public partial class PartsBeckendExcelUpload : System.Web.UI.Page
    {
        MainClass objMainClass         = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL             = new BindDDL();
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
                    var lstNotification           = GetGridJsonValue();
                    string NotificationImportJson = JsonConvert.SerializeObject(lstNotification);
                    string newassigndate = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");
                    string newassigntime = DateTime.Now.ToString("h:mm tt", CultureInfo.InvariantCulture);
                    int result  = objMainClass.BULKADDUPDATEBECKENDPARTSENTRY(NotificationImportJson, Convert.ToInt32(Session["USERID"].ToString()));
                    if (result > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Records. :" + lstNotification.Count.ToString() + " Notification Parts Import sucessfully." + "\");", true);
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
                string extension    = Path.GetExtension(flNotification.FileName);
                string folderpath   = "~/assets/";
                string filePath     = Path.Combine(Server.MapPath(folderpath), Guid.NewGuid().ToString("N") + extension);
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
                        DataRow dr   = dt.NewRow();
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
                            cmd.CommandText = (Convert.ToString("SELECT * From [PARTS$]"));
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

                var objNotification     = GetNotificationPartsDetail(dt);
                gvAssignment.DataSource = objNotification.lstNotificationBeckendPartsDetail;
                gvAssignment.DataBind();
                lgrecordcount.InnerText = "Ready to Update Record :" + objNotification.totalcorrectvalue + " Not to Update Record :" + objNotification.totalrejectvalue + " From Total Records : " + dt.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public List<NotificationBeckendPartsDetail> GetGridJsonValue()
        {
            List<NotificationBeckendPartsDetail> objlstNotificationImport = new List<NotificationBeckendPartsDetail>();
            string NotificationImportJson = string.Empty;
            try
            {
                for (int i = 0; i < gvAssignment.Rows.Count; i++)
                {
                    GridViewRow row = gvAssignment.Rows[i];
                    if(Convert.ToInt32(((HiddenField)row.FindControl("hdIscorrected")).Value) == 1)
                    {
                        NotificationBeckendPartsDetail objNotificationImport = new NotificationBeckendPartsDetail();

                        objNotificationImport.NDSNO        = ((Label)row.FindControl("lblNDSNO")).Text;
                        objNotificationImport.LOCATIONNAME = ((Label)row.FindControl("lblParts")).Text;
                        objlstNotificationImport.Add(objNotificationImport);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return objlstNotificationImport;
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

        public NotificationBeckendPartsUploadValue GetNotificationPartsDetail(DataTable dtNotificationParts)
        {
            NotificationBeckendPartsUploadValue objNotificationUploadValue = new NotificationBeckendPartsUploadValue();
            try
            {
                List<NotificationBeckendPartsDetail> lstNotificationDetail = new List<NotificationBeckendPartsDetail>();
                int correctvalue              = 0;
                int incorrectvalue            = 0;
                if (dtNotificationParts.Rows.Count > 0)
                {
                    bool Isincorrect = false;

                    for (int i = 0; i < dtNotificationParts.Rows.Count; i++)
                    {
                        Isincorrect         = false;

                        NotificationBeckendPartsDetail objNotificationImport = new NotificationBeckendPartsDetail();
                        objNotificationImport.NDSNO                     = dtNotificationParts.Rows[i]["Notifctn"].ToString();
                        objNotificationImport.LOCATIONNAME              = dtNotificationParts.Rows[i]["Text"].ToString();
                       
                        if (dtNotificationParts.Rows[i]["Notifctn"].ToString().Length != 10)
                        {
                            objNotificationImport.ISNOTIFICATIONCORRECTED = 0;
                            Isincorrect = true;
                        }

                        if(Isincorrect)
                        {
                            incorrectvalue      = incorrectvalue + 1;
                            objNotificationImport.ISNOTIFICATIONCORRECTED = 0;
                        }
                        else
                        {
                            correctvalue        = correctvalue + 1;
                            objNotificationImport.ISNOTIFICATIONCORRECTED = 1;
                        }
                        lstNotificationDetail.Add(objNotificationImport);
                    }

                    objNotificationUploadValue.lstNotificationBeckendPartsDetail     = lstNotificationDetail;
                    objNotificationUploadValue.totalcorrectvalue                = correctvalue;
                    objNotificationUploadValue.totalrejectvalue                 = incorrectvalue;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return objNotificationUploadValue;
        }

        protected void gvAssignment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int IsNotificationcorrected = Convert.ToInt32((e.Row.FindControl("hdIscorrected") as HiddenField).Value);
                    if(IsNotificationcorrected == 1)
                    {
                        (e.Row.FindControl("lblNDSNO") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblParts") as Label).ForeColor = System.Drawing.Color.Black;
                    }
                    else
                    {
                        (e.Row.FindControl("lblNDSNO") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblParts") as Label).ForeColor = System.Drawing.Color.Black;
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