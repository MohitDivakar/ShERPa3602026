using Newtonsoft.Json;
using RestSharp;
using ShERPa360net.Class;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;

namespace ShERPa360net
{
    public partial class Login : System.Web.UI.Page
    {
        MainClass objMainClass = new MainClass();
        string resulterror = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //CODEGIVENBYBLANCCO_20062023();
            //CODEGIVENBYBLANCCO_21062023();

            //SendNotification();
            if (!IsPostBack)
            {
                if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null)
                {
                    txtUserName.Text = Request.Cookies["UserName"].Value;
                    txtPassword.Text = Request.Cookies["Password"].Value;
                    if (Session["USERID"] != null)
                    {
                        //btnLogin_Click(sender, e);
                    }
                }
                if (Session["LoginID"] != null)
                {
                    objMainClass.LogoutTrace(Convert.ToInt64(Session["LoginID"]));
                }
                Session.Abandon();
                Session.RemoveAll();
            }
        }
        public string setDateFormat(string StrDate, bool ForQuery)
        {
            string strDateFormat = string.Empty;
            string strDateSeprator = string.Empty;
            string strSysDateFormat = string.Empty;
            string[] strDt;
            string strString = string.Empty;
            strSysDateFormat = StrDate;//System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;

            if (strSysDateFormat.Contains("-"))
            {
                strDateSeprator = "-";
            }
            else if (strSysDateFormat.Contains("/"))
            {
                strDateSeprator = "/";
            }
            if (strSysDateFormat.StartsWith("M"))
            {
                strSysDateFormat = "MM" + strDateSeprator + "dd" + strDateSeprator + "yyyy";
            }
            else
            {
                strSysDateFormat = "dd" + strDateSeprator + "mm" + strDateSeprator + "yyyy";
            }


            string str = string.Empty;
            if (StrDate == "  /  /" || StrDate == "  -  -" || StrDate == "")
            {

            }
            else
            {
                strDt = StrDate.Split(Convert.ToChar(strDateSeprator));

                if (strDt[0].Length == 1)
                {
                    strDt[0] = strDt[0].PadLeft(2, '0');
                }
                else
                {
                    strDt[0] = strDt[0];
                }

                if (strDt[1].Length == 1)
                {
                    strDt[1] = strDt[1].PadLeft(2, '0');
                }
                else
                {
                    strDt[1] = strDt[1];
                    strDt[2] = strDt[2].Substring(0, 4);
                }
                str = strDt[2] + "-" + strDt[1] + "-" + strDt[0];

            }

            return str;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            try
            {
                if (objMainClass.ConnSherpa == null)
                {
                    objMainClass.ShowMessage(this, "Error: Server Connection not found, Please try after sometime.");
                    return;
                }

                if (txtUserName.Text.Trim() == "" || txtPassword.Text.Trim() == "")
                {
                    objMainClass.ShowMessage(this, "Username & Password are required.");
                }
                else
                {
                    DataTable dt = new DataTable();
                    if (txtUserName.Text.ToUpper() == "SYSADMIN")
                    {
                        txtPassword.Text = objMainClass.Encrypt(txtPassword.Text);
                        dt = objMainClass.SysAuthenticateLogin(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["PASSWORD"].ToString() == txtPassword.Text)
                            {
                                if (dt.Rows[0]["INTACCESS"].ToString() == "1")
                                {
                                    Response.Cookies["UserName"].Value = txtUserName.Text.Trim();
                                    Response.Cookies["Password"].Value = txtPassword.Text.Trim();
                                    Session["ROLEID"] = dt.Rows[0]["ROLEID"].ToString();
                                    Session["USERID"] = dt.Rows[0]["ID"].ToString();
                                    Session["EMPID"] = dt.Rows[0]["EMPID"].ToString();
                                    Session["AGENTID"] = dt.Rows[0]["AGENTID"].ToString();
                                    Session["USERNAME"] = dt.Rows[0]["USERNAME"].ToString();
                                    Session["PLANTCD"] = dt.Rows[0]["PLANTCD"].ToString();
                                    Session["SEGMENT"] = dt.Rows[0]["SEGMENT"].ToString();
                                    Session["STCD"] = dt.Rows[0]["STCD"].ToString();
                                    Session["EMPPLANT"] = dt.Rows[0]["EMPPLANT"].ToString();
                                    Session["GPCODE"] = dt.Rows[0]["GPCODE"].ToString();
                                    Session["CROMADISCRATE"] = dt.Rows[0]["CROMADISCRATE"].ToString();
                                    Session["USERMOBILE"] = dt.Rows[0]["MOBILNO"].ToString();
                                    Session["CROMASTOREID"] = dt.Rows[0]["CROMASTOREID"].ToString();
                                    Session["COMMAGENT"] = dt.Rows[0]["COMMAGENT"].ToString();
                                    Session["USEREMAILID"] = dt.Rows[0]["EMAILID"].ToString();

                                    DataTable dtDept = objMainClass.SELECT_USER_DEPT(Convert.ToInt32(Session["EMPID"]));
                                    if (dtDept.Rows.Count > 0)
                                    {
                                        Session["DEPTCD"] = dtDept.Rows[0]["DEPTCD"].ToString();
                                    }
                                    else
                                    {
                                        Session["DEPTCD"] = "";
                                    }

                                    long LoginID = objMainClass.LoginTrace(Convert.ToInt32(Session["USERID"]), objMainClass.IPADDRESS());
                                    Session["LoginID"] = LoginID;

                                    Session.Timeout = 900;
                                    DALUserRights objDALUserRights = new DALUserRights();
                                    objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), "tsmRptSDDashboardNew", "");
                                    if (FormRights.bView == true)
                                    {
                                        Response.Redirect("SalesDashboard.aspx", false);
                                    }
                                    else
                                    {
                                        if (Convert.ToString(dt.Rows[0]["CUSTCODE"]) == "0")
                                        {
                                            Response.Redirect("HomePage.aspx", false);
                                        }
                                        else
                                        {
                                            if (Convert.ToString(Session["USERID"]) == "1")
                                            {
                                                Response.Redirect("HomePage.aspx", false);
                                            }
                                            else
                                            {
                                                objMainClass.ShowMessage(this, "Error: You are not authorised to access this application.");
                                                return;
                                            }

                                        }
                                    }
                                    //Response.Redirect("frmResponse.aspx", false);
                                }
                                else
                                {
                                    objMainClass.ShowMessage(this, "Error: You are not authorised to access this application.");
                                    return;
                                }
                            }
                            else
                            {
                                objMainClass.ShowMessage(this, "Error: Invalid Username or Password.");
                                return;
                            }

                        }
                        else
                        {
                            objMainClass.ShowMessage(this, "Error: Invalid Username or Password.");
                            return;
                        }
                    }
                    else
                    {
                        dt = objMainClass.AuthenticateLogin(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["PASSWORD"].ToString() == txtPassword.Text)
                            {
                                if (dt.Rows[0]["INTACCESS"].ToString() == "1")
                                {

                                    Response.Cookies["UserName"].Value = txtUserName.Text.Trim();
                                    Response.Cookies["Password"].Value = txtPassword.Text.Trim();
                                    Session["ROLEID"] = dt.Rows[0]["ROLEID"].ToString();
                                    Session["USERID"] = dt.Rows[0]["ID"].ToString();
                                    Session["EMPID"] = dt.Rows[0]["EMPID"].ToString();
                                    Session["AGENTID"] = dt.Rows[0]["AGENTID"].ToString();
                                    Session["USERNAME"] = dt.Rows[0]["USERNAME"].ToString();
                                    Session["PLANTCD"] = dt.Rows[0]["PLANTCD"].ToString();
                                    Session["SEGMENT"] = dt.Rows[0]["SEGMENT"].ToString();
                                    Session["STCD"] = dt.Rows[0]["STCD"].ToString();
                                    Session["EMPPLANT"] = dt.Rows[0]["EMPPLANT"].ToString();
                                    Session["GPCODE"] = dt.Rows[0]["GPCODE"].ToString();
                                    Session["CROMADISCRATE"] = dt.Rows[0]["CROMADISCRATE"].ToString();
                                    Session["USERMOBILE"] = dt.Rows[0]["MOBILNO"].ToString();
                                    Session["CROMASTOREID"] = dt.Rows[0]["CROMASTOREID"].ToString();
                                    Session["COMMAGENT"] = dt.Rows[0]["COMMAGENT"].ToString();
                                    Session["USEREMAILID"] = dt.Rows[0]["EMAILID"].ToString();
                                    DataTable dtDept = objMainClass.SELECT_USER_DEPT(Convert.ToInt32((Session["EMPID"].ToString().Length > 0 ? Session["EMPID"].ToString() : "0")));
                                    if (dtDept.Rows.Count > 0)
                                    {
                                        Session["DEPTCD"] = dtDept.Rows[0]["DEPTCD"].ToString();
                                    }
                                    else
                                    {
                                        Session["DEPTCD"] = "";
                                    }
                                    //objMainClass.ShowMessage(this, "Error: Notification not sent successfully.ss");
                                    //return;

                                    long LoginID = objMainClass.LoginTrace(Convert.ToInt32(Session["USERID"]), objMainClass.IPADDRESS());
                                    Session["LoginID"] = LoginID;

                                    Session.Timeout = 120;

                                    if (Convert.ToString(Session["USERID"]) == "2296")
                                    {
                                        Response.Redirect("MobexCC.aspx", false);
                                    }
                                    else if (Convert.ToString(Session["USERID"]) == "2323")
                                    {
                                        Response.Redirect("CavitakReport.aspx", false);
                                    }
                                    else
                                    {
                                        if (Convert.ToString(Session["DEPTCD"]) == "27" || Convert.ToString(Session["DEPTCD"]) == "20" || Convert.ToString(Session["DEPTCD"]) == "14")
                                        {
                                            if (Convert.ToString(dt.Rows[0]["CUSTCODE"]) == "0")
                                            {
                                                Response.Redirect("~/Samsung/frmViewComplaint.aspx", false);
                                            }
                                            else
                                            {
                                                if (Convert.ToString(Session["USERID"]) == "29")
                                                {
                                                    Response.Redirect("~/Samsung/frmViewComplaint.aspx", false);
                                                }
                                                else
                                                {
                                                    objMainClass.ShowMessage(this, "Error: You are not authorised to access this application.");
                                                    return;
                                                }
                                            }

                                        }
                                        else
                                        {
                                            DALUserRights objDALUserRights = new DALUserRights();
                                            objDALUserRights.CHECK_FORMRIGHTS(Session["USERID"].ToString(), "tsmRptSDDashboardNew", "");
                                            if (FormRights.bView == true)
                                            {
                                                Response.Redirect("SalesDashboard.aspx", false);
                                            }
                                            else
                                            {
                                                if (Convert.ToString(dt.Rows[0]["CUSTCODE"]) == "0")
                                                {
                                                    Response.Redirect("HomePage.aspx", false);
                                                }
                                                else
                                                {
                                                    if (Convert.ToString(Session["USERID"]) == "29")
                                                    {
                                                        Response.Redirect("HomePage.aspx", false);
                                                    }
                                                    else
                                                    {
                                                        objMainClass.ShowMessage(this, "Error: You are not authorised to access this application.");
                                                        return;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    objMainClass.ShowMessage(this, "Error: You are not authorised to access this application.");
                                    return;
                                }
                            }
                            else
                            {
                                objMainClass.ShowMessage(this, "Error: Invalid Username or Password.");
                                return;
                            }
                        }
                        else
                        {
                            objMainClass.ShowMessage(this, "Error: Invalid Username or Password.");
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objMainClass.ShowMessage(this, "Error: " + ex.Message);
                return;
            }
        }

        public bool SendNotification()
        {
            bool IsSent = false;
            string result = "";
            try
            {
                var data = new
                {
                    to = "frWk94LDThinjFEcFVkwRL:APA91bHLMo04oW3_vv73YsFlZtZkt75Y_tJ-8adE4RZgTCdvDblg5xshmM6wW82KYiZZwJxitJmW0qfgGk8J_UoeHDL3xMdzaZNDUkeqUUotuJekyt8yM05uUVe4P2ZbdsLmMM5JAssO",
                    data = new
                    {
                        message = "Login done succeessfully.",
                        name = "Mobex Seller System",
                        userId = "1",
                        status = true
                    },
                    notification = new
                    {
                        title = "ServiceNow",
                        text = "Click me to open an Activity!",
                        sound = "default",
                        body = "Kamlesh bhai Here Anroid Master Developer"
                    }
                };

                var json = JsonConvert.SerializeObject(data);
                Byte[] bytearraydetail = Encoding.UTF8.GetBytes(json);

                string serverapikey = "AAAAbqO8CRk:APA91bGoHCJ3dCRnUanBHUdS857E03x3LWerzBXeBo18A4NAcvTn5as6YgxqiVZHAsnLi8FNCjXSpIRVEPp4lKi_hy75KroNRO2mArv1IqHbe3U5tbgsVJUqZ2zTWjAJy1tz5KUDxajF";
                string senderid = "475193411865";
                //475193411865
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                tRequest.Headers.Add($"Authorization: key={serverapikey}");
                tRequest.Headers.Add($"Sender: id={senderid}");
                tRequest.ContentLength = bytearraydetail.Length;
                Stream dataStream = tRequest.GetRequestStream();
                dataStream.Write(bytearraydetail, 0, bytearraydetail.Length);
                dataStream.Close();

                WebResponse tresponse = tRequest.GetResponse();
                dataStream = tresponse.GetResponseStream();
                StreamReader tReasder = new StreamReader(dataStream);

                string sResponseFromServer = tReasder.ReadToEnd();
                result = sResponseFromServer;
                var objResults = JsonConvert.DeserializeObject<PushNotificationResponse>(sResponseFromServer.ToString());
                if (objResults.success == 1)
                {
                    IsSent = true;
                    resulterror = objResults.canonical_ids.ToString();
                }
                else
                {
                    IsSent = false;
                }

                tReasder.Close();
                dataStream.Close();
                tresponse.Close();
            }
            catch (Exception ex)
            {
                IsSent = false;
            }
            return IsSent;
        }

        public void CODEGIVENBYBLANCCO_20062023()
        {
            try
            {
                string method = "GET";
                long epoch = DateTimeOffset.Now.ToUnixTimeMilliseconds(); // Hardcoding for this example
                string uri = "/api/v5/diagnostics/customer/device/imei/869344041795131/results";
                string newline = "\n";

                string privateKey = "aGr5Ex3FJZBkIAJmJ0dBJHDbmjwQBhaoG0xaws2TRRnYZVvEw9i6jhgi7DIJ8w2vPy99IgvSC/15xjt6sE1RzQ==";
                string publicKey = "2aebf2ca6df09b13c9c2eb6dacbd6b57f";


                string plainText = method + newline + epoch + newline + Uri.UnescapeDataString(uri) + newline;
                byte[] keyBytes = Encoding.UTF8.GetBytes(privateKey);
                KeyedHashAlgorithm algorithm = new HMACSHA1(keyBytes);
                string signatureHash = BitConverter.ToString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(plainText))).Replace("-", string.Empty).ToLower();

                signatureHash = publicKey + ":" + signatureHash;
                string authorization = Convert.ToBase64String(Encoding.UTF8.GetBytes(signatureHash));

                string fulluri = "https://mobex.inhancemobile.com" + uri;
                var client = new RestClient(fulluri);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", authorization);
                request.AddHeader("X-DATE", epoch.ToString());
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/json");
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                IRestResponse response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string jsonconn = response.Content;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CODEGIVENBYBLANCCO_21062023()
        {
            try
            {
                string method = "GET";
                long epoch = DateTimeOffset.Now.ToUnixTimeMilliseconds(); // Hardcoding for this example
                string uri = "/api/v5/diagnostics/customer/device/imei/865139041625039/results";
                string newline = "\n";

                string privateKey = "aGr5Ex3FJZBkIAJmJ0dBJHDbmjwQBhaoG0xaws2TRRnYZVvEw9i6jhgi7DIJ8w2vPy99IgvSC/15xjt6sE1RzQ==";
                string publicKey = "2aebf2ca6df09b13c9c2eb6dacbd6b57f";

                string plainText = method + Environment.NewLine + epoch + Environment.NewLine + Uri.UnescapeDataString(uri) + Environment.NewLine;
                byte[] keyBytes = Encoding.UTF8.GetBytes(privateKey);
                KeyedHashAlgorithm algorithm = new HMACSHA1(keyBytes);
                string signatureHash = BitConverter.ToString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(plainText))).Replace("-", string.Empty).ToLower();


                Console.WriteLine(publicKey);
                Console.WriteLine(signatureHash);

                signatureHash = publicKey + ":" + signatureHash;



                string authorization = Convert.ToBase64String(Encoding.UTF8.GetBytes(signatureHash));

                string apiUrl = "https://mobex.inhancemobile.com" + uri;

                // Create an HTTP request
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
                request.Method = method;
                request.Headers["Authorization"] = authorization;
                request.Headers["X-Date"] = epoch + "";
                //request.Headers["Content-Type"] = "application/json";
                request.ContentType = "application/json charset=utf-8";
                request.Accept = "application/json charset=utf-8";
                try
                {
                    // Send the request and get the response
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    string responseBody;
                    using (var streamReader = new System.IO.StreamReader(response.GetResponseStream()))
                    {
                        responseBody = streamReader.ReadToEnd();
                    }

                    // Print the response body
                    Console.WriteLine(responseBody);

                    response.Close();
                }
                catch (WebException ex)
                {
                    // Handle exceptions, if any
                    // ...
                    Console.WriteLine(ex.Message);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
            }
        }
    }
}