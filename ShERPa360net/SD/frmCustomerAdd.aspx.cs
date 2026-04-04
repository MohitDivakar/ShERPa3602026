using Newtonsoft.Json;
using RestSharp;
using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.SD
{
    public partial class frmCustomerAdd : System.Web.UI.Page
    {

        MainClass objMainClass = new MainClass();
        DALUserRights objDALUserRights = new DALUserRights();
        BindDDL objBindDDL = new BindDDL();
        DataTable dtItem = new DataTable();
        DataTable dtTAX = new DataTable();
        DataTable dtCharges = new DataTable();
       

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
                            //Session.Abandon();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('You are not authorised to access this page.');$('.close').click(function(){window.location.href ='../HomePage.aspx' });", true);
                            return;
                        }


                        objBindDDL.FillLists(ddlCategory, "CC");
                        objBindDDL.FillCustype(ddlCustomerType, "SELECTCUSTTYPE");
                        objBindDDL.FillCountry(1, ddlcountry);
                        objBindDDL.FillState(ddlstate);
                        objBindDDL.FillCity(ddlcity, 0);
                        objBindDDL.FillLists(ddlzone, "ZO");
                        objBindDDL.FillLists(ddlDesignationdes, "Di");
                        objBindDDL.FillCustGroup(ddlCustomerGroup);
                        objBindDDL.FillExciseVendType(ddlVendorType);
                        ddlVendorType.SelectedIndex = 1;
                        ddlDesignationdes.SelectedValue = "Di";
                        txtLstDate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy");
                        txtCstdate.Text = objMainClass.indianTime.Date.ToString("dd-MM-yyyy"); //Convert.ToDateTime(DateTime.Now).ToShortDateString();
                        objBindDDL.FillLists(ddlAccountType, "ACT");



                        if (ViewState["Communication"] == null)

                        {
                            DataTable dt = new DataTable();
                            dt.TableName = "grvCommunication";
                            dt.Columns.Add(new DataColumn("ID", typeof(string)));
                            dt.Columns.Add(new DataColumn("Designation", typeof(string)));
                            dt.Columns.Add(new DataColumn("DesignationID", typeof(string)));
                            dt.Columns.Add(new DataColumn("Name", typeof(string)));
                            dt.Columns.Add(new DataColumn("ContactNo", typeof(string)));
                            dt.Columns.Add(new DataColumn("Email", typeof(string)));
                            //DataRow dr1 =  dt.NewRow();
                            //dt.Rows.Add(dr1);
                            ViewState["Communication"] = dt;
                            grvCommunication.DataSource = dt;
                            grvCommunication.DataBind();
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
                hdnSelectedTab.Value = "tab-first";
            }
        }
        protected void txtPincode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (txtPincode.Text.Length == 6)
                    {
                        try
                        {

                            if (!System.Text.RegularExpressions.Regex.IsMatch(txtPincode.Text, "[0-9]{6}$"))
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Only Numbers Aloowed.');", true);
                                txtPincode.Text = string.Empty;
                            }
                            else
                            {
                                DataTable ds = new DataTable();
                                ds = objMainClass.SELECT_CITY_BYPINCODE(txtPincode.Text.Trim());
                                if (ds.Rows.Count > 0)
                                {
                                    ddlstate.SelectedValue = ds.Rows[0]["STATE_ID"].ToString();
                                    ddlcity.SelectedValue = ds.Rows[0]["CITY_NAME"].ToString();
                                }
                                else
                                {
                                    //ddlState.SelectedIndex = 0;
                                    //txtCity.Text = "";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
                        }
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


        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (args.Value.Length < 5)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }


        [WebMethod]

        public static string GetName(string name1)
        {
            string status1 = "false";
            try
            {
                MainClass objMain = new MainClass();
                var result = objMain.CheckCustomerName(name1);
                if (result)
                {
                    status1 = "true";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonConvert.SerializeObject(status1);
        }

        [WebMethod]

        public static string  GetAdharNo(string aadharno)
        {
            string status1 = "false";
            try
            {
                MainClass objMain = new MainClass();
                var result = objMain.CheckCustomerName("", "", "", aadharno);
                if (result)
                {
                    status1 = "true";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonConvert.SerializeObject(status1);
        }

        [WebMethod]

        public static string GetPanno(string panno)
        {
            string status1 = "false";
            try
            {
                MainClass objMain = new MainClass();
                var result = objMain.CheckCustomerName("", "", panno);
                if (result)
                {
                    status1 = "true";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonConvert.SerializeObject(status1);
        }

        [WebMethod]

        public static string GetGST(string gstno)
        {
            string status1 = "false";
            try
            {
                MainClass objMain = new MainClass();
                var result = objMain.CheckCustomerName("", "", "", "",gstno);
                if (result)
                {
                    status1 = "true";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonConvert.SerializeObject(status1);
        }


        protected void txtCstNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (!string.IsNullOrEmpty(txtCstNo.Text))
                    {
                        RequiredFieldValidator8.Enabled = true;
                    }
                    else
                    {
                        RequiredFieldValidator8.Enabled = false;
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

        protected void txtLSTNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (!string.IsNullOrEmpty(txtLSTNo.Text))
                    {
                        RequiredFieldValidator10.Enabled = true;
                    }
                    else
                    {
                        RequiredFieldValidator10.Enabled = false;
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


        protected void txtIFSCCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    if (txtISFC.Text != string.Empty && txtISFC.Text != "" && txtISFC.Text != null)
                    {
                        DataTable Bankdt = new DataTable();

                        var client = new RestClient(("https://ifsc.firstatom.org/key/84NnS05xbn6U5RKP1dGEBU83g/ifsc/" + txtISFC.Text));
                        client.Timeout = -1;
                        var request = new RestRequest(Method.GET);
                        IRestResponse response = client.Execute(request);
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            string jsonconn = response.Content;
                            jsonconn = "[" + jsonconn + "]";
                            Bankdt = (DataTable)JsonConvert.DeserializeObject(jsonconn, (typeof(DataTable)));


                            if (Bankdt.Rows.Count > 0)
                            {
                                txtBankName.Text = Convert.ToString(Bankdt.Rows[0]["Bank"]) + " - " + Convert.ToString(Bankdt.Rows[0]["Branch"]);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Incorrect IFSC Code. Bank Details Not Found!');", true);
                            }

                        }
                        else
                        {


                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text('Incorrect IFSC Code!');", true);
                        }
                    }
                    else
                    {

                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-warning').modal();$('#lblAlertMsg').text('Please Enter IFSC Code!');", true);
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

        protected void imgSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {


                    if (Session["USERID"] != null)
                    {
                        string lresult = "";
                        DataTable dt = new DataTable();
                        if (ViewState["Communication"] != null)
                            dt = ViewState["Communication"] as DataTable;




                        byte[] IDPROOF = null;
                        byte[] PAN = null;
                        byte[] CHEQUE = null;
                        byte[] SHOP = null;
                        byte[] GSTCERTI = null;
                        byte[] MSMECERTI = null;

                        string IDPROOFTYPE = ".jpeg";
                        string PANTYPE = ".jpeg";
                        string CHEQUETYPE = ".jpeg";
                        string GSTCERTITYPE = ".pdf";
                        string MSMECERTITYPE = ".pdf";




                        if (fuIDProof.HasFiles)
                        {
                            BinaryReader br1 = new BinaryReader(fuIDProof.PostedFile.InputStream);

                            IDPROOF = br1.ReadBytes(fuIDProof.PostedFile.ContentLength);
                            IDPROOFTYPE = System.IO.Path.GetExtension(fuIDProof.FileName);
                        }

                        if (fuPAN.HasFiles)
                        {
                            BinaryReader br1 = new BinaryReader(fuPAN.PostedFile.InputStream);

                            PAN = br1.ReadBytes(fuPAN.PostedFile.ContentLength);
                            PANTYPE = System.IO.Path.GetExtension(fuPAN.FileName);
                        }


                        if (fuGSTCerti.HasFiles)
                        {
                            BinaryReader br1 = new BinaryReader(fuGSTCerti.PostedFile.InputStream);

                            GSTCERTI = br1.ReadBytes(fuGSTCerti.PostedFile.ContentLength);
                            GSTCERTITYPE = System.IO.Path.GetExtension(fuGSTCerti.FileName);
                        }

                        if (fuCheque.HasFiles)
                        {
                            BinaryReader br1 = new BinaryReader(fuCheque.PostedFile.InputStream);

                            CHEQUE = br1.ReadBytes(fuCheque.PostedFile.ContentLength);
                            CHEQUETYPE = System.IO.Path.GetExtension(fuCheque.FileName);
                        }



                        lresult = objMainClass.saveCustomerdetails(ddlCustomerType.SelectedValue, txtCustomerCode.Text, ddlTitle.SelectedIndex > 0 ? ddlTitle.SelectedItem.Text : "", txtName.Text, txtName2.Text, txtShortname.Text, "INR", ddlVendorType.SelectedValue, ddlCategory.SelectedValue,
                           ddlCustomerGroup.SelectedValue, ddlzone.SelectedValue, txtBankName.Text, txtAdharNo.Text, txtPanno.Text, txtAccountNo.Text, txtISFC.Text, ((Session["USERID"] != null) ? Convert.ToInt32(Session["USERID"]) : 0)
                           , txtAddress1.Text, txtAddress2.Text, txtAdddress3.Text, ddlcity.SelectedItem.Text, ddlstate.SelectedValue, ddlcountry.SelectedValue, txtPincode.Text, txtContactPerson.Text, txtContactNo.Text, txtMobile.Text, txtEmail.Text,
                           dt, txtCstNo.Text, txtCstdate.Text, txtLSTNo.Text, txtLstDate.Text, txtServiceRNo.Text, txtECC.Text, txtExciseRNO.Text, txtExciseRange.Text, TextBox7.Text, txtExciseCommisionrate.Text, txtoldCustmerCode.Text, txtWebsite.Text,
                           txtRegion.Text,
                           IDPROOF, IDPROOFTYPE, PAN, PANTYPE, CHEQUE, CHEQUETYPE, GSTCERTI, GSTCERTITYPE, ddlAccountType.SelectedValue, txtGstNo.Text
                           );

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\" Customer Add Successfully. Customer Code:" + txtCustomerCode.Text + "\");$('.close').click(function(){window.location.href ='frmCustomerList.aspx' });", true);
                        //   Response.Redirect("frmCustomerList.aspx");

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
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {

                    if (ddlstate.SelectedIndex > 0)
                    {
                        objBindDDL.FillCity(ddlcity, ddlstate.SelectedValue);
                    }
                    else
                    {
                        objBindDDL.FillCity(ddlcity);
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
        protected static string fileread()
        {
            string actualfol;
            actualfol = HttpContext.Current.Server.MapPath("~/PRMailBody.html");
            StreamReader sr = new StreamReader(actualfol);
            string strcontent = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            return strcontent;
        }

        protected void lnkSaveCharges_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] != null)
                {
                    //if (ViewState["Communication"] == null)

                    //{
                    //    DataTable dt = new DataTable();
                    //    dt.TableName = "grvCommunication";
                    //    dt.Columns.Add(new DataColumn("ID", typeof(string)));
                    //    dt.Columns.Add(new DataColumn("Designation", typeof(string)));
                    //    dt.Columns.Add(new DataColumn("DesignationID", typeof(string)));
                    //    dt.Columns.Add(new DataColumn("Name", typeof(string)));
                    //    dt.Columns.Add(new DataColumn("ContactNo", typeof(string)));
                    //    dt.Columns.Add(new DataColumn("Email", typeof(string)));
                    //    //DataRow dr1 =  dt.NewRow();
                    //    //dt.Rows.Add(dr1);
                    //    ViewState["Communication"] = dt;
                    //}
                    DataTable dataTable = ViewState["Communication"] as DataTable;
                    if (hdISEdit.Value == "true")
                    {
                        hdISEdit.Value = "false";

                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            if (Convert.ToString(dataTable.Rows[i]["ID"]) == hndID.Value)
                            {
                                dataTable.Rows[i]["Designation"] = ddlDesignationdes.SelectedItem.Text;
                                dataTable.Rows[i]["DesignationID"] = ddlDesignationdes.SelectedValue;

                                dataTable.Rows[i]["Name"] = txtNamedes.Text;
                                dataTable.Rows[i]["ContactNo"] = txtContactNodes.Text;
                                dataTable.Rows[i]["Email"] = txtEmaildes.Text;
                                ViewState["Communication"] = dataTable;
                                grvCommunication.DataSource = dataTable;
                                grvCommunication.DataBind();
                                resetdata();
                            }
                        }

                    }
                    else
                    {
                        hndID.Value = Convert.ToString(dataTable.Rows.Count + 1);
                        DataRow dr = dataTable.NewRow();
                        dr["ID"] = hndID.Value;
                        dr["Designation"] = ddlDesignationdes.SelectedItem.Text;
                        dr["DesignationID"] = ddlDesignationdes.SelectedValue;
                        dr["Name"] = txtNamedes.Text;
                        dr["ContactNo"] = txtContactNodes.Text;
                        dr["Email"] = txtEmaildes.Text;
                        dataTable.Rows.Add(dr);

                        ViewState["Communication"] = dataTable;
                        grvCommunication.DataSource = dataTable;
                        grvCommunication.DataBind();
                        resetdata();
                    }




                    hdnSelectedTab.Value = "tab-second";


                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#tabfirstli').removeClass('active');$('#tabfirst').removeClass('active'); $('#tabsecondli').addClass('active');$('#tabsecond').addClass('active');", true);

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

        protected void grvCommunication_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "eDelete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                DataTable dt = (DataTable)ViewState["Communication"];
                GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                dt.Rows[row.RowIndex].Delete();
                ViewState["Communication"] = dt;
                grvCommunication.DataSource = ViewState["Communication"];
                grvCommunication.DataBind();

            }
            if (e.CommandName == "eEdit")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                DataTable dt = (DataTable)ViewState["Communication"];

                hndID.Value = Convert.ToString(dt.Rows[index - 1]["ID"]);
                ddlDesignationdes.SelectedValue = Convert.ToString(dt.Rows[index - 1]["DesignationID"]);
                txtNamedes.Text = Convert.ToString(dt.Rows[index - 1]["Name"]);
                txtContactNodes.Text = Convert.ToString(dt.Rows[index - 1]["ContactNo"]);
                txtEmaildes.Text = Convert.ToString(dt.Rows[index - 1]["Email"]);
                hdISEdit.Value = "true";

                Session["saveCharge"] = "Update Charge";
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#tabfirstli').removeClass('active');$('#tabfirst').removeClass('active'); $('#tabsecondli').addClass('active');$('#tabsecond').addClass('active');", true);


        }

        public void resetdata()
        {
            ddlDesignationdes.SelectedValue = "0";
            txtNamedes.Text = string.Empty;
            txtContactNodes.Text = string.Empty;
            txtEmaildes.Text = string.Empty;
        }

        protected void ddlCustomerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var result = objMainClass.MAXCUSCODENO(ddlCustomerType.SelectedValue);
            if (result != null && result != "")
            {
                txtCustomerCode.Text = objMainClass.strConvertZeroPadding((Convert.ToInt32(objMainClass.MAXCUSCODENO(ddlCustomerType.SelectedValue)) + 1).ToString());
                txtCustomerCode.Enabled = false;
            }
            else
            {
                txtCustomerCode.Enabled = true;
            }
        }


    }
}
