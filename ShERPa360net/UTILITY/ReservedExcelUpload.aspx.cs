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
namespace ShERPa360net.UTILITY
{
    public partial class ReservedExcelUpload : System.Web.UI.Page
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

                        if (FormRights.bAdd == false)
                        {
                            btnUpload.Enabled = false;
                            btnCancel.Enabled = false;
                            btnSaveDetail.Enabled = false;
                        }
                        else
                        {
                            btnUpload.Enabled = true;
                            btnCancel.Enabled = true;
                            btnSaveDetail.Enabled = true;
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
                if (gvProduct.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please upload at least one Record to Import.');", true);
                }
                else
                {
                    var lstMobexSellerDetail              = GetGridJsonValue();
                    string lstMobexSellerDetailImportJson = JsonConvert.SerializeObject(lstMobexSellerDetail);
                    int result                            = objMainClass.SAVEMOBEXSELLERBULKRESERVEDDETAIL(lstMobexSellerDetailImportJson, Session["USERID"].ToString());
                    if (result > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Records. :" + lstMobexSellerDetail.Count.ToString() + " Reserved sucessfully." + "\");", true);
                        gvProduct.DataSource              = null;
                        gvProduct.DataBind();
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
                string extension    = Path.GetExtension(flReserUpload.FileName);
                string folderpath   = "~/assets/";
                string filePath     = Path.Combine(Server.MapPath(folderpath), Guid.NewGuid().ToString("N") + extension);
                flReserUpload.SaveAs(filePath);
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
                            cmd.CommandText     = (Convert.ToString("SELECT * From [Sheet1$]"));
                            cmd.Connection      = MyConnection;
                            oda.SelectCommand   = cmd;
                            oda.Fill(dt);
                            MyConnection.Close();
                        }
                    }
                }
                File.Delete(filePath);

                gvProduct.DataSource = null;
                gvProduct.DataBind();

                gvProduct.DataSource = dt;
                gvProduct.DataBind();
                lgrecordcount.InnerText = "Count :" + dt.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }

        public List<MobexSellerReserveUploadDetail> GetGridJsonValue()
        {
            List<MobexSellerReserveUploadDetail> objlstMobexSellerReserveUploadDetailImport = new List<MobexSellerReserveUploadDetail>();
            string objMobexSellerReserveImportJson = string.Empty;
            try
            {
                for (int i = 0; i < gvProduct.Rows.Count; i++)
                {
                    GridViewRow row                         = gvProduct.Rows[i];
                    MobexSellerReserveUploadDetail objMobexSellerReserveUploadDetail = new MobexSellerReserveUploadDetail();
                    objMobexSellerReserveUploadDetail.ID    =  Convert.ToInt32(((Label)row.FindControl("lblID")).Text);
                    objlstMobexSellerReserveUploadDetailImport.Add(objMobexSellerReserveUploadDetail);

                    //SEND PUSH NOTIFICATION
                    //string VENDORID         = ((Label)row.FindControl("lblVendorID")).Text;
                    //string productdetail    = ((Label)row.FindControl("lblMake")).Text + " " + ((Label)row.FindControl("lblMODEL")).Text + " " + ((Label)row.FindControl("lblRam")).Text + " " + ((Label)row.FindControl("lblROM")).Text + " " + ((Label)row.FindControl("lblCOLOR")).Text + " " + ((Label)row.FindControl("lblVENDORGRADE")).Text;
                    //var pushnotificationmsg = PushNotificationContentDetail.GETORDERRECEIVEDPUSHMESSAGE(ID.ToString(), productdetail);
                    //SendPushNotification.SendPushNotificaion(PushNotificationContentDetail.GETORDERRECEIVEDPUSHSUBJECT(), pushnotificationmsg, Convert.ToInt32(VENDORID));
                    //SEND PUSH NOTIFICATION
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
            return objlstMobexSellerReserveUploadDetailImport;
        }

        public void ResetFormControl()
        {
            try
            {
                gvProduct.DataSource    = null;
                gvProduct.DataBind();
                lgrecordcount.InnerText = "0";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}