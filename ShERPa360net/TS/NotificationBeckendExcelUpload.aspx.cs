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
    public partial class NotificationBeckendExcelUpload : System.Web.UI.Page
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
                    int result  = objMainClass.BULKADDUPDATEBECKENDNOTIFICATIONENTRY(NotificationImportJson,objMainClass.MAXASSIGNMENTNOPREFIX(Session["PLANTCD"].ToString()), newassigntime, Convert.ToInt32(Session["USERID"].ToString()));
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
                            cmd.CommandText = (Convert.ToString("SELECT * From [Serial detail$]"));
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
                gvAssignment.DataSource = objNotification.lstNotificationBeckendDetail;
                gvAssignment.DataBind();
                lgrecordcount.InnerText = "Ready to Update Record :" + objNotification.totalcorrectvalue + " Not to Update Record :" + objNotification.totalrejectvalue + " From Total Records : " + dt.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public List<NotificationBeckendDetail> GetGridJsonValue()
        {
            List<NotificationBeckendDetail> objlstNotificationImport = new List<NotificationBeckendDetail>();
            string NotificationImportJson = string.Empty;
            try
            {
                for (int i = 0; i < gvAssignment.Rows.Count; i++)
                {
                    GridViewRow row = gvAssignment.Rows[i];
                    if(Convert.ToInt32(((HiddenField)row.FindControl("hdIscorrected")).Value) == 1)
                    {
                        NotificationBeckendDetail objNotificationImport = new NotificationBeckendDetail();
                        
                        string newreceiveddate  = ((Label)row.FindControl("lblNotificationdt")).Text.Replace(".", "-");
                        objNotificationImport.ESNNO = ((Label)row.FindControl("lblESNNo")).Text;
                        objNotificationImport.NDSNO = ((Label)row.FindControl("lblNDSNO")).Text;
                        objNotificationImport.MODELSKEY = Convert.ToInt32(((HiddenField)row.FindControl("hdModelKey")).Value);
                        objNotificationImport.REPARIENGINEERKEY = Convert.ToInt32(((HiddenField)row.FindControl("hdREPARIENGINEERKEY")).Value);
                        objNotificationImport.PRESCANNINGPROBLEMKEY = Convert.ToInt32(((HiddenField)row.FindControl("hdPRESCANNINGPROBLEMKEY")).Value);
                        objNotificationImport.FAULTOBSERVEDKEY = Convert.ToInt32(((HiddenField)row.FindControl("hdFAULTOBSERVEDKEY")).Value);
                        objNotificationImport.OBJECTPARTKEY = Convert.ToInt32(((HiddenField)row.FindControl("hdOBJECTPARTKEY")).Value);
                        objNotificationImport.FAULTREASONKEY = Convert.ToInt32(((HiddenField)row.FindControl("hdFAULTREASONKEY")).Value);
                        objNotificationImport.REPARITASKDESCRIPTIONKEY = Convert.ToInt32(((HiddenField)row.FindControl("hdREPARITASKDESCRIPTIONKEY")).Value);
                        objNotificationImport.ASSIGNDATE = objMainClass.setDateFormat(newreceiveddate, true);
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

        public NotificationBeckendUploadValue GetNotificationDetail(DataTable dtNotification)
        {
            NotificationBeckendUploadValue objNotificationUploadValue = new NotificationBeckendUploadValue();
            try
            {
                List<NotificationBeckendDetail> lstNotificationDetail = new List<NotificationBeckendDetail>();
                int correctvalue              = 0;
                int incorrectvalue            = 0;
                DataRow[] dtModelRows         = null;
                DataRow[] dtPrescaningProRows = null;
                DataRow[] dtObjectPartRows    = null;
                DataRow[] dtFaultReasonRows   = null;
                DataRow[] dtEngineerRows      = null;

                if (dtNotification.Rows.Count > 0)
                {
                    var dtModels        = objMainClass.GETESNTOMODELDETAIL("","SELECTALL");
                    var dtPrescaningPro = objMainClass.GetCategoryMaster(objMainClass.intCmpId, "PRESCANPROBLEM", "", "", "SELECTALL");
                    var dtObjectPart    = objMainClass.GetCategoryMaster(objMainClass.intCmpId, "OBJECTPART", "", "", "SELECTALL");
                    var dtFaultReason   = objMainClass.GetCategoryMaster(objMainClass.intCmpId, "FAULTREASON", "", "", "SELECTALL");
                    var dtEngineer      = objMainClass.GetUserDetail(objMainClass.intCmpId, "", "", 0, "SELECTALL");

                    bool Isincorrect = false;

                    for (int i = 0; i < dtNotification.Rows.Count; i++)
                    {
                        Isincorrect         = false;
                        dtModelRows         = null;
                        dtPrescaningProRows = null;
                        dtObjectPartRows    = null;
                        dtFaultReasonRows   = null;
                        dtEngineerRows      = null;

                        NotificationBeckendDetail objNotificationImport = new NotificationBeckendDetail();
                        objNotificationImport.ESNNO = dtNotification.Rows[i]["SerialNo"].ToString();
                        objNotificationImport.NDSNO = dtNotification.Rows[i]["Notifctn"].ToString();
                        objNotificationImport.ISPFAULTCODE = dtNotification.Rows[i]["ISPCode"].ToString();
                        objNotificationImport.ASSIGNDATE = dtNotification.Rows[i]["Notifdate"].ToString();


                        //Get From Master Field
                        objNotificationImport.MODELSKEY = 0;
                        objNotificationImport.PRESCANNINGPROBLEMKEY = 0;
                        objNotificationImport.FAULTOBSERVEDKEY = 0;
                        objNotificationImport.OBJECTPARTKEY = 0;
                        objNotificationImport.FAULTREASONKEY = 0;
                        objNotificationImport.REPARITASKDESCRIPTIONKEY = 0;

                        if (dtNotification.Rows[i]["SerialNo"].ToString().Length != 12)
                        {
                            objNotificationImport.MODELSKEY = 0;
                            objNotificationImport.ISNOTIFICATIONCORRECTED = 0;
                            Isincorrect = true;
                        }
                        

                        if (dtNotification.Rows[i]["Notifctn"].ToString().Length != 10)
                        {
                            objNotificationImport.ISNOTIFICATIONCORRECTED = 0;
                            Isincorrect = true;
                        }

                        //Pre Scanning KEY
                        dtPrescaningProRows = dtPrescaningPro.Select("CATVALUE='" + dtNotification.Rows[i]["Description"].ToString() + "'");
                        if (dtPrescaningProRows.Count() > 0)
                        {
                            foreach (DataRow eachrow in dtPrescaningProRows)
                            {
                                objNotificationImport.PRESCANNINGPROBLEMKEY = Convert.ToInt32(eachrow["CATID"].ToString());
                                objNotificationImport.PRESCANNINGPROBLEMKEYVALUE = dtNotification.Rows[i]["Description"].ToString();
                            }
                        }
                        else
                        {
                            objNotificationImport.PRESCANNINGPROBLEMKEY = 0;
                        }
                        //Pre Scanning KEY


                        //Fault Observed KEY
                        dtPrescaningProRows = null;
                        dtPrescaningProRows = dtPrescaningPro.Select("CATVALUE='" + dtNotification.Rows[i]["Problemcodetext"].ToString() + "'");
                        if (dtPrescaningProRows.Count() > 0)
                        {
                            foreach (DataRow eachrow in dtPrescaningProRows)
                            {
                                objNotificationImport.FAULTOBSERVEDKEY = Convert.ToInt32(eachrow["CATID"].ToString());
                                objNotificationImport.FAULTOBSERVEDKEYVALUE = dtNotification.Rows[i]["Problemcodetext"].ToString();
                            }
                        }
                        else
                        {
                            objNotificationImport.FAULTOBSERVEDKEY = 0;
                            objNotificationImport.FAULTOBSERVEDKEYVALUE = "";
                        }
                        //Fault Observed KEY


                        //Object part code text
                        dtObjectPartRows = dtObjectPart.Select("CATVALUE='" + dtNotification.Rows[i]["Objectpartcodetext"].ToString() + "'");
                        if (dtObjectPartRows.Count() > 0)
                        {
                            foreach (DataRow eachrow in dtObjectPartRows)
                            {
                                objNotificationImport.OBJECTPARTKEY = Convert.ToInt32(eachrow["CATID"].ToString());
                                objNotificationImport.OBJECTPARTKEYVALUE = dtNotification.Rows[i]["Objectpartcodetext"].ToString();
                            }
                        }
                        else
                        {
                            objNotificationImport.OBJECTPARTKEY = 0;
                        }
                        //Object part code text


                        //Cause code text
                        dtFaultReasonRows = dtFaultReason.Select("CATVALUE='" + dtNotification.Rows[i]["Causecodetext"].ToString() + "'");
                        if (dtFaultReasonRows.Count() > 0)
                        {
                            foreach (DataRow eachrow in dtFaultReasonRows)
                            {
                                objNotificationImport.FAULTREASONKEY = Convert.ToInt32(eachrow["CATID"].ToString());
                                objNotificationImport.FAULTREASONKEYVALUE = dtNotification.Rows[i]["Causecodetext"].ToString();
                            }
                        }
                        else
                        {
                            objNotificationImport.FAULTREASONKEY = 0;
                        }
                        //Cause code text

                        //Engineer code text
                        dtEngineerRows = dtEngineer.Select("USERCODE='" + dtNotification.Rows[i]["Mnwkctr"].ToString() + "'");
                        if (dtEngineerRows.Count() > 0)
                        {
                            foreach (DataRow eachrow in dtEngineerRows)
                            {
                                objNotificationImport.REPARIENGINEERKEY      = Convert.ToInt32(eachrow["USERID"].ToString());
                                objNotificationImport.REPARIENGINEERKEYVALUE = eachrow["USERCODE"].ToString() + " " + eachrow["USERFIRSTNAME"].ToString();
                            }
                        }
                        else
                        {
                            objNotificationImport.REPARIENGINEERKEY = 0;
                        }
                        //Engineer code text


                        //Repaire Status Detail 
                        if ((dtNotification.Rows[i]["Status"].ToString().ToUpper()) == (REPAIRSTATUS.NOTIFICATION.ToString()))
                        {
                            objNotificationImport.REPARITASKDESCRIPTIONKEY = Convert.ToInt32(REPAIRSTATUS.NOTIFICATION);
                            objNotificationImport.REPARITASKDESCRIPTIONKEYVALUE = (REPAIRSTATUS.NOTIFICATION.ToString());
                        }
                        else if ((dtNotification.Rows[i]["Status"].ToString().ToUpper()) == (REPAIRSTATUS.PRESCANNING.ToString()))
                        {
                            objNotificationImport.REPARITASKDESCRIPTIONKEY = Convert.ToInt32(REPAIRSTATUS.PRESCANNING);
                            objNotificationImport.REPARITASKDESCRIPTIONKEYVALUE = (REPAIRSTATUS.PRESCANNING.ToString());
                        }
                        else if ((dtNotification.Rows[i]["Status"].ToString().ToUpper()) == (REPAIRSTATUS.REPAIRED.ToString()))
                        {
                            objNotificationImport.REPARITASKDESCRIPTIONKEY = Convert.ToInt32(REPAIRSTATUS.REPAIRED);
                            objNotificationImport.REPARITASKDESCRIPTIONKEYVALUE = (REPAIRSTATUS.REPAIRED.ToString());
                        }
                        else if((dtNotification.Rows[i]["Status"].ToString().ToUpper()) == (REPAIRSTATUS.BER.ToString()))
                        {
                            objNotificationImport.REPARITASKDESCRIPTIONKEY      = Convert.ToInt32(REPAIRSTATUS.BER);
                            objNotificationImport.REPARITASKDESCRIPTIONKEYVALUE = (REPAIRSTATUS.BER.ToString());
                        }
                        else if ((dtNotification.Rows[i]["Status"].ToString().ToUpper()) == (REPAIRSTATUS.IR.ToString()))
                        {
                            objNotificationImport.REPARITASKDESCRIPTIONKEY = Convert.ToInt32(REPAIRSTATUS.IR);
                            objNotificationImport.REPARITASKDESCRIPTIONKEYVALUE = (REPAIRSTATUS.IR.ToString());
                        }
                        else if ((dtNotification.Rows[i]["Status"].ToString().ToUpper()) == (REPAIRSTATUS.FAILED.ToString()))
                        {
                            objNotificationImport.REPARITASKDESCRIPTIONKEY = Convert.ToInt32(REPAIRSTATUS.FAILED);
                            objNotificationImport.REPARITASKDESCRIPTIONKEYVALUE = (REPAIRSTATUS.FAILED.ToString());
                        }
                        else if ((dtNotification.Rows[i]["Status"].ToString().ToUpper()) == (REPAIRSTATUS.DISPATCH.ToString()))
                        {
                            objNotificationImport.REPARITASKDESCRIPTIONKEY = Convert.ToInt32(REPAIRSTATUS.DISPATCH);
                            objNotificationImport.REPARITASKDESCRIPTIONKEYVALUE = (REPAIRSTATUS.DISPATCH.ToString());
                        }
                        else 
                        {
                            objNotificationImport.REPARITASKDESCRIPTIONKEY = 0;
                            objNotificationImport.REPARITASKDESCRIPTIONKEYVALUE = "INVALID";
                        }
                        //Repaire Status Detail 

                        //MODEL GET KEY
                        if (dtNotification.Rows[i]["SerialNo"].ToString().Length == 12)
                        {
                            string esno = (dtNotification.Rows[i]["SerialNo"].ToString().Length > 0 ? dtNotification.Rows[i]["SerialNo"].ToString().Substring(0, 2) : "");
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

                    objNotificationUploadValue.lstNotificationBeckendDetail     = lstNotificationDetail;
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
                        (e.Row.FindControl("lblESNNo") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblNDSNO") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblDescription") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblNotificationdt") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblISPCode") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblModel") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblEngineerName") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblProblemcode") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblObjectPart") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblCausecode") as Label).ForeColor = System.Drawing.Color.Black;
                        (e.Row.FindControl("lblStatus") as Label).ForeColor = System.Drawing.Color.Black;
                    }
                    else
                    {
                        (e.Row.FindControl("lblESNNo") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblNDSNO") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblDescription") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblNotificationdt") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblISPCode") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblModel") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblEngineerName") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblProblemcode") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblObjectPart") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblCausecode") as Label).ForeColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("lblStatus") as Label).ForeColor = System.Drawing.Color.Red;
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