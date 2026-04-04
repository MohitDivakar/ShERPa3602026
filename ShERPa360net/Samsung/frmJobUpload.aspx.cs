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

namespace ShERPa360net.Samsung
{
    public partial class frmJobUpload : System.Web.UI.Page
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
            try
            {
                if (Session["USERID"] != null)
                {
                    if (grvLead.Rows.Count > 0)
                    {
                        string alreadycomp = "";
                        int j = 0;
                        GridView gvPartList = new GridView();
                        byte[] bimag = null;
                        string notinserted = "";
                        int k = 0;
                        int l = 0;

                        for (int i = 0; i < grvLead.Rows.Count; i++)
                        {
                            GridViewRow row = grvLead.Rows[i];
                            string compliantno = Convert.ToString(row.Cells[0].Text);
                            string LOCATION = Convert.ToString(row.Cells[1].Text);
                            string GPCODE = Convert.ToString(row.Cells[2].Text);
                            string ENGGNAME = Convert.ToString(row.Cells[3].Text);
                            string SERVICETYPE = Convert.ToString(row.Cells[4].Text);
                            string PRODUCT = Convert.ToString(row.Cells[5].Text);

                            string modelno = Convert.ToString(row.Cells[6].Text);
                            string serialno = Convert.ToString(row.Cells[7].Text);
                            string custname = Convert.ToString(row.Cells[8].Text);
                            string contactno = Convert.ToString(row.Cells[9].Text);

                            string CONTACTNO2 = Convert.ToString(row.Cells[10].Text);

                            string mobexamc = Convert.ToString(row.Cells[11].Text);

                            string address = Convert.ToString(row.Cells[12].Text);

                            DataTable dtCheckComp = new DataTable();
                            dtCheckComp = objMainClass.GetSamsnugTCR("", "", compliantno, "", "", "", 0, "", "CHECKMAINCOMPLAINT");
                            if (dtCheckComp.Rows.Count > 0)
                            {
                                if (alreadycomp == "")
                                {
                                    alreadycomp = compliantno;
                                }
                                else
                                {
                                    alreadycomp = alreadycomp + "," + compliantno;
                                }
                                j++;
                            }
                            else
                            {

                                string rcptno = objMainClass.InsertSamsungTCR("", compliantno, custname, address, contactno, CONTACTNO2, 0, 0, "", 0, 0, 0, "", Convert.ToInt32(Session["USERID"]), "", "", gvPartList, "", modelno, serialno, bimag, "", ENGGNAME, SERVICETYPE, PRODUCT, LOCATION, GPCODE,
                                    Convert.ToInt32(mobexamc), 0, "", "", "INSERTCOMPLAINT");
                                if (rcptno != "" && rcptno != string.Empty && rcptno != null)
                                {
                                    DataTable dtMsgString = new DataTable();
                                    dtMsgString = objMainClass.GetMessageText(objMainClass.intCmpId, objMainClass.intTrigeerId, 207, 0, "1029", "30");
                                    //dtMsgString = objMainClass.GetMessageText(objMainClass.intCmpId, objMainClass.intTrigeerId, 208, 0, "1029", "30");
                                    string strMessage = Convert.ToString(dtMsgString.Rows[0]["msgtxt"]);
                                    DataTable dtGPData = new DataTable();
                                    dtGPData = objMainClass.GetSamsnugTCR("", "", "", "", "", "", 0, GPCODE, "GETGPCODEDATA");


                                    string URL = "";
                                    string AMCURL = "";
                                    string contact = "";
                                    string onlineqrcode = "";

                                    if (dtGPData.Rows.Count > 0)
                                    {
                                        AMCURL = Convert.ToString(dtGPData.Rows[0]["AMCURL"]);
                                        URL = Convert.ToString(dtGPData.Rows[0]["PAYMENTURL"]);
                                        contact = Convert.ToString(dtGPData.Rows[0]["CONTACTNO"]);
                                        onlineqrcode = Convert.ToString(dtGPData.Rows[0]["ONLINEQRCODE"]);
                                    }
                                    else
                                    {
                                        AMCURL = "http://14.98.132.190:360/Login.aspx";
                                        URL = "http://14.98.132.190:360/Samsung/QTEKPAYMENT.png";
                                        contact = "8799050997";
                                        onlineqrcode = "https://qarmatek.com/data/QTEKPAYMENT.png";
                                    }

                                    //if (LOCATION == "AHMEDABAD" || LOCATION == "HO")
                                    //{
                                    //    URL = "http://14.98.132.190:360/Samsung/QTEKPAYMENT.png";
                                    //    contact = "8799050997";
                                    //}
                                    //if (LOCATION == "VADODARA" || LOCATION == "BARODA")
                                    //{
                                    //    URL = "http://14.98.132.190:360/Samsung/QTEKPAYMENTBARODA.png";
                                    //    contact = "8799057238";
                                    //}

                                    //if (LOCATION == "RAJKOT")
                                    //{
                                    //    URL = "http://14.98.132.190:360/Samsung/QTEKPAYMENTRAJKOT.png";
                                    //    contact = "9099980546";
                                    //}

                                    strMessage = strMessage.Replace("@@COMPLAINTNO", compliantno).Replace("@@AMCURL", AMCURL).Replace("@@CONTACT", contact);
                                    //strMessage = strMessage.Replace("@@COMPLAINTNO", compliantno).Replace("@@AMCURL", AMCURL).Replace("@@CONTACT", contact).Replace("@@QRLINK", onlineqrcode);
                                    objMainClass.SaveNotification(objMainClass.intCmpId, "", "", "91" + contactno, "", "", strMessage, "", "TCR", compliantno, "WAMEDIA", URL, Convert.ToInt32(Session["USERID"]), "INSERTNOTIFICATION");
                                    //objMainClass.SaveNotification(objMainClass.intCmpId, "", "", "91" + contactno, "", "", strMessage, "", "TCR", compliantno, "SMS", URL, Convert.ToInt32(Session["USERID"]), "INSERTNOTIFICATION");
                                    l++;
                                }
                                else
                                {
                                    if (notinserted == "")
                                    {
                                        notinserted = compliantno;
                                    }
                                    else
                                    {
                                        notinserted = notinserted + "," + compliantno;
                                    }
                                    k++;
                                }
                            }
                        }

                        if (l == 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text(\"Complaints not saved successfully. Total complaints uploaded : " + lblRecord.Text + ". Saved Complaints : " + l + ". Not Saved complaints : " + k + " (" + notinserted + "). Duplicate complaints : " + j + " ( " + alreadycomp + " ).\");$('.close').click(function(){window.location.href ='frmJobUpload.aspx' });", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"All complaints saved successfully. Total complaints uploaded : " + lblRecord.Text + ". Saved Complaints : " + l + ". Not Saved complaints : " + k + " (" + notinserted + "). Duplicate complaints : " + j + " ( " + alreadycomp + " ).\");$('.close').click(function(){window.location.href ='frmJobUpload.aspx' });", true);
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
        }
    }
}