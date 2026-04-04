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
    public partial class NotificationExcelUpload : System.Web.UI.Page
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
                    int result  = objMainClass.BULKADDUPDATENOTIFICATIONENTRY(NotificationImportJson,objMainClass.MAXASSIGNMENTNOPREFIX(Session["PLANTCD"].ToString()),objMainClass.setDateFormat(newassigndate,true), newassigntime, Convert.ToInt32(Session["USERID"].ToString()));
                    if (result > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Records. :" + lstNotification.Count.ToString() + " Notification Import sucessfully." + "\");", true);
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

                var objNotification = GetNotificationDetail(dt);
                gvAssignment.DataSource = objNotification.lstNotificationDetail;
                gvAssignment.DataBind();
                lgrecordcount.InnerText = "Ready to Update Record :" + objNotification.totalcorrectvalue + " Not to Update Record :" + objNotification.totalrejectvalue + " From Total Records : " + dt.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public List<NotificationDetail> GetGridJsonValue()
        {
            List<NotificationDetail> objlstNotificationImport = new List<NotificationDetail>();
            string NotificationImportJson = string.Empty;
            try
            {
                for (int i = 0; i < gvAssignment.Rows.Count; i++)
                {
                    GridViewRow row = gvAssignment.Rows[i];
                    if(Convert.ToInt32(((HiddenField)row.FindControl("hdIscorrected")).Value) == 1)
                    {
                        NotificationDetail objNotificationImport = new NotificationDetail();
                        
                        string newreceiveddate  = ((Label)row.FindControl("lblReceivedDate")).Text.Replace(".", "-");
                        objNotificationImport.ESNNO = ((Label)row.FindControl("lblESNNo")).Text;
                        objNotificationImport.MODELSKEY = Convert.ToInt32(((HiddenField)row.FindControl("hdModelKey")).Value);
                        objNotificationImport.ISPFAULTCODE = ((Label)row.FindControl("lblISPCode")).Text;
                        objNotificationImport.TAGKEY = Convert.ToInt32(((HiddenField)row.FindControl("hdTag")).Value);
                        objNotificationImport.FAULTREPORTEDKEY = Convert.ToInt32(((HiddenField)row.FindControl("hdFaultReported")).Value);
                        objNotificationImport.NDSNO = ((Label)row.FindControl("lblNDSNO")).Text;
                        objNotificationImport.RECEIVEDATE = objMainClass.setDateFormat(newreceiveddate, true);
                        objNotificationImport.BOXNO = ((Label)row.FindControl("lblBoxNo")).Text;
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

        public NotificationUploadValue GetNotificationDetail(DataTable dtNotification)
        {
            NotificationUploadValue objNotificationUploadValue = new NotificationUploadValue();
            try
            {
                List<NotificationDetail> lstNotificationDetail = new List<NotificationDetail>();
                int correctvalue   = 0;
                int incorrectvalue = 0;
                DataRow[] dtTagRows           = null;
                DataRow[] dtFaultReportedRows = null;
                DataRow[] dtModelRows = null;

                if (dtNotification.Rows.Count > 0)
                {
                    var dtModels        = objMainClass.GETESNTOMODELDETAIL("","SELECTALL");
                    var dtTag           = objMainClass.GetCategoryMaster(objMainClass.intCmpId, "TAG", "", "", "SELECTALL");
                    var dtFaultReported = objMainClass.GetCategoryMaster(objMainClass.intCmpId, "FAULTREPORTED", "", "", "SELECTALL");
                    bool Isincorrect = false;

                    for (int i = 0; i < dtNotification.Rows.Count; i++)
                    {
                        Isincorrect = false;
                        dtTagRows = null;
                        dtFaultReportedRows = null;
                        dtModelRows = null;

                        NotificationDetail objNotificationImport = new NotificationDetail();
                        objNotificationImport.ESNNO = dtNotification.Rows[i]["ESN NO"].ToString();
                        objNotificationImport.NDSNO = dtNotification.Rows[i]["Notification No"].ToString();
                        objNotificationImport.ISPFAULTCODE = dtNotification.Rows[i]["ISP Code"].ToString();
                        objNotificationImport.FAULTREPORTEDKEYVALUE = dtNotification.Rows[i]["ISP Fault"].ToString();
                        objNotificationImport.TAGKEYVALUE = dtNotification.Rows[i]["Tag"].ToString();
                        objNotificationImport.RECEIVEDATE = dtNotification.Rows[i]["Received Date"].ToString();
                        objNotificationImport.BOXNO = dtNotification.Rows[i]["Box No"].ToString();

                        //Get From Master Field
                        objNotificationImport.FAULTREPORTEDKEY = 0;
                        objNotificationImport.TAGKEY = 0;
                        objNotificationImport.MODELSKEY = 0;
                        if (dtNotification.Rows[i]["ESN NO"].ToString().Length != 12)
                        {
                            objNotificationImport.MODELSKEY = 0;
                            objNotificationImport.ISNOTIFICATIONCORRECTED = 0;
                            Isincorrect = true;
                        }
                        

                        if (dtNotification.Rows[i]["Notification No"].ToString().Length != 10)
                        {
                            objNotificationImport.ISNOTIFICATIONCORRECTED = 0;
                            Isincorrect = true;
                        }
                        

                        //ISP FAULT GET KEY
                        dtFaultReportedRows = dtFaultReported.Select("CATVALUE='" + dtNotification.Rows[i]["ISP Fault"].ToString() + "'");
                        if (dtFaultReportedRows.Count() > 0)
                        {
                            foreach (DataRow eachrow in dtFaultReportedRows)
                            {
                                objNotificationImport.FAULTREPORTEDKEY = Convert.ToInt32(eachrow["CATID"].ToString());
                            }
                        }
                        else
                        {
                            objNotificationImport.FAULTREPORTEDKEY = 0;
                            Isincorrect = true;
                        }

                        //TAG GET KEY
                        dtTagRows = dtTag.Select("CATVALUE='" + dtNotification.Rows[i]["Tag"].ToString() + "'");
                        if (dtTagRows.Count() > 0)
                        {
                            foreach (DataRow eachrow in dtTagRows)
                            {
                                objNotificationImport.TAGKEY = Convert.ToInt32(eachrow["CATID"].ToString());
                            }
                        }
                        else
                        {
                            objNotificationImport.TAGKEY = 0;
                            Isincorrect = true;
                        }

                        //MODEL GET KEY
                        if (dtNotification.Rows[i]["ESN NO"].ToString().Length == 12)
                        {
                            string esno = (dtNotification.Rows[i]["ESN NO"].ToString().Length > 0 ? dtNotification.Rows[i]["ESN NO"].ToString().Substring(0, 2) : "");
                            dtModelRows = dtModels.Select("ESNSTARTNO='" + esno + "'");
                            if (dtModelRows.Count() > 0)
                            {
                                foreach (DataRow eachrow in dtModelRows)
                                {
                                    objNotificationImport.MODELSKEY      = Convert.ToInt32(eachrow["MODELID"].ToString());
                                    objNotificationImport.MODELSKEYVALUE = eachrow["MODELNAME"].ToString();
                                }
                            }
                            else
                            {
                                objNotificationImport.MODELSKEY = 0;
                                objNotificationImport.MODELSKEYVALUE = "";

                                Isincorrect = true;
                            }
                        }
                        else
                        {
                            Isincorrect = true;
                            objNotificationImport.MODELSKEY         = 0;
                            objNotificationImport.MODELSKEYVALUE    = "";
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

                    objNotificationUploadValue.lstNotificationDetail = lstNotificationDetail;
                    objNotificationUploadValue.totalcorrectvalue = correctvalue;
                    objNotificationUploadValue.totalrejectvalue  = incorrectvalue;
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
                        (e.Row.FindControl("lblESNNo") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblModel") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblISPCode") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblTAG") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblFaultReported") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblReceivedDate") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblBoxNo") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblNDSNO") as Label).ForeColor = System.Drawing.Color.Black;
                    }
                    else
                    {
                        (e.Row.FindControl("lblESNNo") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblModel") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblISPCode") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblTAG") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblFaultReported") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblReceivedDate") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblBoxNo") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblNDSNO") as Label).ForeColor = System.Drawing.Color.Red;
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