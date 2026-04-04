using Newtonsoft.Json;
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

namespace ShERPa360net.CRM
{
    public partial class LeadGeneration : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnManualSave.Enabled = true;
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





                        txtInqDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        objBindDDL.FillLists(ddlReference, "IR");
                        objBindDDL.FillLists(ddlInqType, "INT");
                        objBindDDL.FillLists(ddlLeadType, "LT");
                        objBindDDL.FillState(ddlState);
                        objBindDDL.FillProductItemSubGroup(ddlProduct, 1);
                        DataTable dtAgents = objMainClass.GetLeadDataMethod("AGENTCNT");
                        if (dtAgents.Rows.Count > 0)
                        {
                            chkAgentList.DataTextField = "USERNAME";
                            chkAgentList.DataValueField = "ID";
                            chkAgentList.DataSource = dtAgents;
                            chkAgentList.DataBind();

                            lnkUpload.Enabled = true;
                        }
                        else
                        {
                            lnkUpload.Enabled = false;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Agent not declared to assign. Please contact to administrator!');", true);
                        }
                        if (FormRights.bAdd == false)
                        {
                            lnkUpload.Enabled = false;
                            lnkUpload.Visible = false;
                            btnManualSave.Enabled = false;
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



        protected void lnkUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    OleDbConnection MyConnection;
                    string extension = Path.GetExtension(flUpload.FileName);
                    string folderpath = "~/excel/";
                    string filePath = Path.Combine(Server.MapPath(folderpath), Guid.NewGuid().ToString("N") + extension);
                    flUpload.SaveAs(filePath);
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
            if (grvLead.Rows.Count > 0)
            {
                int i = 0;
                foreach (ListItem listItem in chkAgentList.Items)
                {
                    if (listItem.Selected)
                    {
                        i++;
                    }
                }

                if (i > 0)
                {
                    try
                    {
                        if (Session["USERID"] != null)
                        {
                            string SalesImportJson = GetGridJsonValue();
                            int iResult = objMainClass.InsertLeadGeneration(SalesImportJson, Convert.ToString(Session["USERID"]), "IMPORT", (int)LeadStatus.Saved);
                            if (iResult > 0)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" " + grvLead.Rows.Count.ToString() + " record added successfully." + "\");", true);

                                grvLead.DataSource = string.Empty;
                                grvLead.DataBind();
                                lblRecoretxt.Visible = true;
                                lblRecord.Visible = true;
                                lblRecord.Text = "0";

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Something went wrong. Record not added.');", true);
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
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Agent not selected to assign.!');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please upload at least one Record to Import.');", true);
            }
        }

        public string GetGridJsonValue()
        {
            List<LeadDetail> objlstLeadImport = new List<LeadDetail>();
            string objLeadImportjson = string.Empty;



            try
            {
                if (Session["USERID"] != null)
                {
                    //DataTable dtAgent = objMainClass.GetLeadDataMethod("AGENTCNT");
                    DataTable dtAgent = new DataTable();
                    dtAgent.Clear();
                    dtAgent.Columns.Add("ID");


                    foreach (ListItem listItem in chkAgentList.Items)
                    {
                        if (listItem.Selected)
                        {
                            DataRow dtRow = dtAgent.NewRow();
                            dtRow["ID"] = listItem.Value;
                            dtAgent.Rows.Add(dtRow);
                        }
                    }






                    if (dtAgent.Rows.Count > 0)
                    {
                        if (dtAgent.Rows.Count == 1)
                        {
                            for (int i = 0; i < grvLead.Rows.Count; i++)
                            {
                                GridViewRow row = grvLead.Rows[i];
                                LeadDetail objLeadsImport = new LeadDetail();
                                objLeadsImport.INQDATE = (Convert.ToDateTime(row.Cells[0].Text));
                                objLeadsImport.CUSTNAME = Convert.ToString(row.Cells[1].Text);
                                objLeadsImport.CONTACTNO = Convert.ToString(row.Cells[2].Text);
                                objLeadsImport.EMAIL = Convert.ToString(row.Cells[3].Text == "&nbsp;" ? "" : row.Cells[3].Text);
                                objLeadsImport.MAKE = Convert.ToString(row.Cells[4].Text == "&nbsp;" ? "" : row.Cells[4].Text);
                                objLeadsImport.MODEL = Convert.ToString(row.Cells[5].Text == "&nbsp;" ? "" : row.Cells[5].Text);
                                objLeadsImport.RAM = Convert.ToString(row.Cells[6].Text == "&nbsp;" ? "" : row.Cells[6].Text);
                                objLeadsImport.ROM = Convert.ToString(row.Cells[7].Text == "&nbsp;" ? "" : row.Cells[7].Text);
                                objLeadsImport.COLOR = Convert.ToString(row.Cells[8].Text == "&nbsp;" ? "" : row.Cells[8].Text);
                                objLeadsImport.PRICE = Convert.ToString(row.Cells[9].Text) == "&nbsp;" ? "" : row.Cells[9].Text.Replace("&gt;", ">").Replace("&lt;", "<");
                                objLeadsImport.CUSTREMARKS = Convert.ToString(row.Cells[10].Text);
                                //objLeadsImport.ASSIGNTO = Convert.ToInt32(dtAgent.Rows[0]["ID"]);
                                objLeadsImport.ASSIGNTO = Convert.ToInt32(Convert.ToString(dtAgent.Rows[0]["ID"]) == "" ? 0 : dtAgent.Rows[0]["ID"]);
                                objLeadsImport.CMPID = objMainClass.intCmpId;
                                objLeadsImport.INQTYPE = Convert.ToInt32(Convert.ToString(row.Cells[12].Text).Split('-')[0].Trim());
                                objLeadsImport.REFERENCE = Convert.ToString(Convert.ToString(row.Cells[11].Text).Split('-')[1].Trim());
                                objLeadsImport.REFID = Convert.ToInt32(Convert.ToString(row.Cells[11].Text).Split('-')[0].Trim());
                                objLeadsImport.PRODUCT = Convert.ToString(row.Cells[13].Text);

                                objLeadsImport.ATTRIBUTE = Convert.ToString(row.Cells[14].Text);
                                objLeadsImport.ATTRVALUE = Convert.ToString(row.Cells[15].Text);

                                objLeadsImport.LTYPE = Convert.ToInt32(Convert.ToString(row.Cells[16].Text).Split('-')[0].Trim());
                                objLeadsImport.LEADTYPE = Convert.ToString(Convert.ToString(row.Cells[16].Text).Split('-')[1].Trim());
                                objLeadsImport.STATEID = Convert.ToInt32(Convert.ToString(row.Cells[17].Text).Split('-')[0].Trim());
                                objLeadsImport.CITYID = Convert.ToInt32(Convert.ToString(row.Cells[18].Text).Split('-')[0].Trim());
                                objLeadsImport.CITY = Convert.ToString(Convert.ToString(row.Cells[18].Text).Split('-')[1].Trim());
                                objlstLeadImport.Add(objLeadsImport);

                            }
                        }
                        else
                        {
                            //if (grvLead.Rows.Count > dtAgent.Rows.Count)
                            //{
                            int avg = Convert.ToInt32(grvLead.Rows.Count / dtAgent.Rows.Count);
                            int assign = 1;

                            //for (int p = 0; p < dtAgent.Rows.Count; p++)
                            //{
                            int p = 0;

                            for (int i = 0; i < grvLead.Rows.Count; i++)
                            {
                                if (p <= dtAgent.Rows.Count)
                                {
                                    if (assign <= avg)
                                    {
                                        GridViewRow row = grvLead.Rows[i];
                                        LeadDetail objLeadsImport = new LeadDetail();
                                        objLeadsImport.INQDATE = (Convert.ToDateTime(row.Cells[0].Text));
                                        objLeadsImport.CUSTNAME = Convert.ToString(row.Cells[1].Text);
                                        objLeadsImport.CONTACTNO = Convert.ToString(row.Cells[2].Text);
                                        objLeadsImport.EMAIL = Convert.ToString(row.Cells[3].Text) == "&nbsp;" ? "" : Convert.ToString(row.Cells[3].Text);
                                        objLeadsImport.MAKE = Convert.ToString(row.Cells[4].Text) == "&nbsp;" ? "" : Convert.ToString(row.Cells[4].Text);
                                        objLeadsImport.MODEL = Convert.ToString(row.Cells[5].Text) == "&nbsp;" ? "" : Convert.ToString(row.Cells[5].Text);
                                        objLeadsImport.RAM = Convert.ToString(row.Cells[6].Text) == "&nbsp;" ? "" : Convert.ToString(row.Cells[6].Text);
                                        objLeadsImport.ROM = Convert.ToString(row.Cells[7].Text) == "&nbsp;" ? "" : Convert.ToString(row.Cells[7].Text);
                                        objLeadsImport.COLOR = Convert.ToString(row.Cells[8].Text) == "&nbsp;" ? "" : Convert.ToString(row.Cells[8].Text);
                                        //objLeadsImport.PRICE = Convert.ToDecimal(row.Cells[9].Text);
                                        objLeadsImport.PRICE = Convert.ToString(row.Cells[9].Text) == "&nbsp;" ? "" : row.Cells[9].Text.Replace("&gt;", ">").Replace("&lt;", "<");
                                        objLeadsImport.CUSTREMARKS = Convert.ToString(row.Cells[10].Text) == "&nbsp;" ? "" : Convert.ToString(row.Cells[10].Text);
                                        //objLeadsImport.ASSIGNTO = Convert.ToInt32(dtAgent.Rows[p]["ID"]);
                                        objLeadsImport.ASSIGNTO = Convert.ToInt32(Convert.ToString(dtAgent.Rows[p]["ID"]) == "" ? 0 : dtAgent.Rows[p]["ID"]);
                                        objLeadsImport.REFERENCE = Convert.ToString(Convert.ToString(row.Cells[11].Text).Split('-')[1].Trim());
                                        objLeadsImport.REFID = Convert.ToInt32(Convert.ToString(row.Cells[11].Text).Split('-')[0].Trim());
                                        objLeadsImport.CMPID = objMainClass.intCmpId;
                                        objLeadsImport.INQTYPE = Convert.ToInt32(Convert.ToString(row.Cells[12].Text).Split('-')[0].Trim());
                                        objLeadsImport.PRODUCT = Convert.ToString(row.Cells[13].Text);

                                        objLeadsImport.ATTRIBUTE = Convert.ToString(row.Cells[14].Text);
                                        objLeadsImport.ATTRVALUE = Convert.ToString(row.Cells[15].Text);

                                        objLeadsImport.LTYPE = Convert.ToInt32(Convert.ToString(row.Cells[16].Text).Split('-')[0].Trim());
                                        objLeadsImport.LEADTYPE = Convert.ToString(Convert.ToString(row.Cells[16].Text).Split('-')[1].Trim());
                                        objLeadsImport.STATEID = Convert.ToInt32(Convert.ToString(row.Cells[17].Text).Split('-')[0].Trim());
                                        objLeadsImport.CITYID = Convert.ToInt32(Convert.ToString(row.Cells[18].Text).Split('-')[0].Trim());
                                        objLeadsImport.CITY = Convert.ToString(Convert.ToString(row.Cells[18].Text).Split('-')[1].Trim());
                                        objlstLeadImport.Add(objLeadsImport);
                                        assign++;
                                    }
                                    else
                                    {
                                        assign = 0;
                                        p++;

                                        GridViewRow row = grvLead.Rows[i];
                                        LeadDetail objLeadsImport = new LeadDetail();
                                        objLeadsImport.INQDATE = (Convert.ToDateTime(row.Cells[0].Text));
                                        objLeadsImport.CUSTNAME = Convert.ToString(row.Cells[1].Text);
                                        objLeadsImport.CONTACTNO = Convert.ToString(row.Cells[2].Text);
                                        objLeadsImport.EMAIL = Convert.ToString(row.Cells[3].Text) == "" ? "&nbsp;" : Convert.ToString(row.Cells[3].Text);
                                        //objLeadsImport.MAKE = Convert.ToString(row.Cells[4].Text);
                                        objLeadsImport.MAKE = Convert.ToString(row.Cells[4].Text) == "&nbsp;" ? string.Empty : Convert.ToString(row.Cells[4].Text);
                                        objLeadsImport.MODEL = Convert.ToString(row.Cells[5].Text) == "&nbsp;" ? "" : Convert.ToString(row.Cells[5].Text);
                                        objLeadsImport.RAM = Convert.ToString(row.Cells[6].Text) == "&nbsp;" ? "" : Convert.ToString(row.Cells[6].Text);
                                        objLeadsImport.ROM = Convert.ToString(row.Cells[7].Text) == "&nbsp;" ? "" : Convert.ToString(row.Cells[7].Text);
                                        objLeadsImport.COLOR = Convert.ToString(row.Cells[8].Text) == "&nbsp;" ? "" : Convert.ToString(row.Cells[8].Text);
                                        //objLeadsImport.PRICE = Convert.ToDecimal(row.Cells[9].Text);
                                        objLeadsImport.PRICE = Convert.ToString(row.Cells[9].Text) == "" ? "" : row.Cells[9].Text.Replace("&gt;", ">").Replace("&lt;", "<");
                                        objLeadsImport.CUSTREMARKS = Convert.ToString(row.Cells[10].Text) == "&nbsp;" ? "" : Convert.ToString(row.Cells[10].Text);
                                        objLeadsImport.ASSIGNTO = Convert.ToInt32(Convert.ToString(dtAgent.Rows[p]["ID"]) == "" ? 0 : dtAgent.Rows[p]["ID"]);
                                        objLeadsImport.REFERENCE = Convert.ToString(Convert.ToString(row.Cells[11].Text).Split('-')[1].Trim());
                                        objLeadsImport.REFID = Convert.ToInt32(Convert.ToString(row.Cells[11].Text).Split('-')[0].Trim());
                                        objLeadsImport.CMPID = objMainClass.intCmpId;
                                        objLeadsImport.INQTYPE = Convert.ToInt32(Convert.ToString(row.Cells[12].Text).Split('-')[0].Trim());
                                        objLeadsImport.PRODUCT = Convert.ToString(row.Cells[13].Text);

                                        objLeadsImport.ATTRIBUTE = Convert.ToString(row.Cells[14].Text);
                                        objLeadsImport.ATTRVALUE = Convert.ToString(row.Cells[15].Text);

                                        objLeadsImport.LTYPE = Convert.ToInt32(Convert.ToString(row.Cells[16].Text).Split('-')[0].Trim());
                                        objLeadsImport.LEADTYPE = Convert.ToString(Convert.ToString(row.Cells[16].Text).Split('-')[1].Trim());
                                        objLeadsImport.STATEID = Convert.ToInt32(Convert.ToString(row.Cells[17].Text).Split('-')[0].Trim());
                                        objLeadsImport.CITYID = Convert.ToInt32(Convert.ToString(row.Cells[18].Text).Split('-')[0].Trim());
                                        objLeadsImport.CITY = Convert.ToString(Convert.ToString(row.Cells[18].Text).Split('-')[1].Trim());
                                        objlstLeadImport.Add(objLeadsImport);
                                        assign++;

                                    }
                                }
                                // }


                            }





                            //}
                            //else
                            //{
                            //    for (int i = 0; i < grvLead.Rows.Count; i++)
                            //    {
                            //        GridViewRow row = grvLead.Rows[i];
                            //        LeadDetail objLeadsImport = new LeadDetail();
                            //        objLeadsImport.INQDATE = (Convert.ToDateTime(row.Cells[0].Text));
                            //        objLeadsImport.CUSTNAME = Convert.ToString(row.Cells[1].Text);
                            //        objLeadsImport.CONTACTNO = Convert.ToString(row.Cells[2].Text);
                            //        objLeadsImport.EMAIL = Convert.ToString(row.Cells[3].Text);
                            //        objLeadsImport.MAKE = Convert.ToString(row.Cells[4].Text);
                            //        objLeadsImport.MODEL = Convert.ToString(row.Cells[5].Text);
                            //        objLeadsImport.RAM = Convert.ToString(row.Cells[6].Text);
                            //        objLeadsImport.ROM = Convert.ToString(row.Cells[7].Text);
                            //        objLeadsImport.COLOR = Convert.ToString(row.Cells[8].Text);
                            //        objLeadsImport.PRICE = Convert.ToDecimal(row.Cells[9].Text);
                            //        objLeadsImport.CUSTREMARKS = Convert.ToString(row.Cells[10].Text);
                            //        objLeadsImport.ASSIGNTO = Convert.ToInt32(dtAgent.Rows[0]["ID"]);
                            //        objLeadsImport.REFERENCE = Convert.ToString(row.Cells[11].Text);
                            //        objLeadsImport.CMPID = objMainClass.intCmpId;
                            //        objlstLeadImport.Add(objLeadsImport);

                            //    }
                            //}
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Agent not declared to assign. Please contact to administrator!');", true);
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


            objLeadImportjson = JsonConvert.SerializeObject(objlstLeadImport);
            return objLeadImportjson;

        }

        protected void btnManualSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    //int iResult = objMainClass.InsertManualLead(txtInqDate.Text, txtCustName.Text, txtContactNo.Text, txtEmail.Text, txtMake.Text, txtModel.Text, txtRAM.Text, txtROM.Text,
                    //"", txtPrice.Text, Convert.ToString(Session["USERID"]), (int)LeadStatus.Saved, Convert.ToString(Session["USERID"]), txtCustRemarks.Text,
                    //ddlReference.SelectedItem.Text, "INSERT", objMainClass.intCmpId, ddlInqType.SelectedValue, Convert.ToInt32(ddlReference.SelectedValue),
                    //Convert.ToInt32(ddlLeadType.SelectedValue), Convert.ToInt32(ddlState.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlCity.SelectedValue), Convert.ToInt32(ddlState.SelectedValue),
                    //Convert.ToInt32(ddlState.SelectedValue) == 0 ? "" : ddlCity.SelectedItem.Text, ddlProduct.SelectedItem.Text);

                    //RAM ROM removed by Mohit on 15.4.2025
                    int iResult = objMainClass.InsertManualLead(txtInqDate.Text, txtCustName.Text, txtContactNo.Text, txtEmail.Text, txtMake.Text, txtModel.Text, "", "",
                    "", txtPrice.Text, Convert.ToString(Session["USERID"]), (int)LeadStatus.Saved, Convert.ToString(Session["USERID"]), txtCustRemarks.Text,
                    ddlReference.SelectedItem.Text, "INSERT", objMainClass.intCmpId, ddlInqType.SelectedValue, Convert.ToInt32(ddlReference.SelectedValue),
                    Convert.ToInt32(ddlLeadType.SelectedValue), Convert.ToInt32(ddlState.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlCity.SelectedValue), Convert.ToInt32(ddlState.SelectedValue),
                    Convert.ToInt32(ddlState.SelectedValue) == 0 ? "" : ddlCity.SelectedItem.Text, ddlProduct.SelectedItem.Text, ddlSpecs.Items.Count > 0 ? ddlSpecs.SelectedItem.Text : "", ddlSpecValue.Items.Count > 0 ? ddlSpecValue.SelectedItem.Text : "","","");
                    if (iResult > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Record added successfully.');$('.close').click(function(){window.location.href ='LeadGeneration.aspx' });", true);
                        //CollapsiblePanelExtender3.Collapsed = true;

                        ClearText();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Something went wrong. Record not added.');", true);
                        //CollapsiblePanelExtender3.Collapsed = true;
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

        public void ClearText()
        {
            txtInqDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");
            txtCustName.Text = string.Empty;
            txtContactNo.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtMake.Text = string.Empty;
            txtModel.Text = string.Empty;
            //txtRAM.Text = string.Empty;
            //txtROM.Text = string.Empty;
            //txtColor.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtCustRemarks.Text = string.Empty;
            ddlReference.SelectedIndex = 0;
            ddlProduct.SelectedIndex = 0;
        }

        protected void txtContactNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    if (txtContactNo.Text != string.Empty && txtContactNo.Text != null && txtContactNo.Text != "")
                    {
                        DataTable dt = new DataTable();
                        dt = objMainClass.CheckContactNo(objMainClass.intCmpId, txtContactNo.Text, "CHECKCONTACT");
                        if (dt.Rows.Count > 0)
                        {
                            lblDetails.Text = "Lead Already Generated With This Contact No. !";
                            lblCustName.Text = Convert.ToString(dt.Rows[0]["CUSTNAME"]);
                            lblContactNo.Text = Convert.ToString(dt.Rows[0]["CONTACTNO"]);
                            lblReff.Text = Convert.ToString(dt.Rows[0]["REFF"]);
                            lblStatus.Text = Convert.ToString(dt.Rows[0]["STATUSDESC"]);
                            lblCreateBy.Text = Convert.ToString(dt.Rows[0]["CREATEBY"]);
                            lblAssignTo.Text = Convert.ToString(dt.Rows[0]["ASSIGNTO"]);
                            lblUpdateBy.Text = Convert.ToString(dt.Rows[0]["UPDATEBY"]);
                            lblHOLDCUSTREMARKS.Text = Convert.ToString(dt.Rows[0]["HOLDCUSTREMARKS"]);
                            lblCALLREMARKS.Text = Convert.ToString(dt.Rows[0]["CALLREMARKS"]);

                            btnManualSave.Enabled = false;
                            txtContactNo.Text = string.Empty;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Check').modal();", true);
                            txtContactNo.Text = string.Empty;
                            btnManualSave.Enabled = false;
                        }
                        else
                        {
                            btnManualSave.Enabled = true;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please Enter Customer Contact No.');", true);
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

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    objBindDDL.FillCity(ddlCity, ddlState.SelectedValue);
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

        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    objBindDDL.FillSpecandValue(ddlSpecs, 1, Convert.ToInt32(ddlProduct.SelectedItem.Value), "GETSPECMSTID");
                    if (ddlSpecs.SelectedIndex > -1)
                    {
                        objBindDDL.FillSpecandValue(ddlSpecValue, 1, Convert.ToInt32(ddlSpecs.SelectedItem.Value), "GETSPECVALUESMST");
                    }
                    else
                    {
                        ddlSpecValue.DataSource = null;
                        ddlSpecValue.DataBind();
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

        protected void ddlSpecs_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    objBindDDL.FillSpecandValue(ddlSpecValue, 1, Convert.ToInt32(ddlSpecs.SelectedItem.Value), "GETSPECVALUESMST");
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