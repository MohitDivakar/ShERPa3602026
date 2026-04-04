using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace ShERPa360net.Class
{
    public class WAClass
    {
        public string checkConnectionURL = "https://console.wa0.in/api/checkconnection.php";

        public string reconnectURL = "https://whitelable.website/api/reconnect.php"; //"https://console.wa0.in/api/reconnect.php"; //OLD

        public string sendMessageURL = "https://whitelable.website/api/send.php"; //"https://console.wa0.in/api/send.php"; //OLD

        public string client_id = "3a13f8abba76457a7bd9e6378b42ec15";

        public string instance = "633FC1DCE4FD9";  //"65cd6e0db8fe830fc320bdd5572b9bdf"; // OLD

        public string accesstocken = "47713cdeab2ed4c0b4baefde3f9469f8";

        public string statusTrue = "True";

        public string statusFalse = "False";

        public string msgPaired = "paired";

        public string msgPending = "pending";

        public string msgQue = "successfully queued";

        MainClass objMainClass = new MainClass();

        public bool CheckWAConnection()
        {
            bool iReturn = false;

            try
            {
                var clientConn = new RestClient("https://console.wa0.in/api/checkconnection.php?client_id=" + client_id + "&instance=" + instance);
                clientConn.Timeout = -1;
                var requestConn = new RestRequest(Method.POST);
                IRestResponse responseConn = clientConn.Execute(requestConn);
                if (responseConn.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string jsonconn = responseConn.Content;
                    jsonconn = "[" + jsonconn + "]";
                    DataTable dtValueConn = (DataTable)JsonConvert.DeserializeObject(jsonconn, (typeof(DataTable)));
                    if (Convert.ToString(dtValueConn.Rows[0]["status"]) == statusTrue || Convert.ToString(dtValueConn.Rows[0]["message"]) == msgPaired)
                    {
                        iReturn = true;
                    }
                    else
                    {
                        iReturn = false;
                    }
                }
                else
                {
                    iReturn = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public bool ReConnectWA()
        {
            bool iReturn = false;

            try
            {
                var clientReConn = new RestClient("https://console.wa0.in/api/reconnect.php?client_id=" + client_id + "&instance=" + instance);
                clientReConn.Timeout = -1;
                var requestReConn = new RestRequest(Method.POST);
                IRestResponse responseReConn = clientReConn.Execute(requestReConn);
                if (responseReConn.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string jsonReconn = responseReConn.Content;
                    jsonReconn = "[" + jsonReconn + "]";
                    DataTable dtValueReConn = (DataTable)JsonConvert.DeserializeObject(jsonReconn, (typeof(DataTable)));
                    if (Convert.ToString(dtValueReConn.Rows[0]["status"]) == statusTrue)
                    {
                        iReturn = true;
                    }
                    else
                    {
                        iReturn = false;
                    }
                }
                else
                {
                    iReturn = false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return iReturn;
        }

        public string SendTextMessage(string MESSAGE, string CONTACTNO, string SENTBY)
        {
            string RESPONSE = "";
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                DataTable dt = new DataTable();
                dt = objMainClass.GetWAData("WHATSAPP", 1, "GETWADATA");


                instance = Convert.ToString(dt.Rows[0]["KEYNAME"]);
                accesstocken = Convert.ToString(dt.Rows[0]["KEYVALUE"]);
                sendMessageURL = Convert.ToString(dt.Rows[0]["OTHER"]);

                //var client = new RestClient("https://console.wa0.in/api/send.php?client_id=3a13f8abba76457a7bd9e6378b42ec15&instance=65cd6e0db8fe830fc320bdd5572b9bdf&number=918460591264&message=MESSAGE_HERE&type=text");

                //var client = new RestClient("https://console.wa0.in/api/send.php?client_id=" + client_id + "&instance=" + instance + "&number=" + CONTACTNO + "&message=" + MESSAGE + "&type=text");
                var client = new RestClient(sendMessageURL + "?number=" + CONTACTNO + "&type=text&message=" + MESSAGE + "&instance_id=" + instance + "&access_token=" + accesstocken);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string jsonsend = response.Content;
                    //jsonsend = ("[" + jsonsend + "]").Replace("}{", "},{");
                    //WAResponse objWAResponse = new WAResponse();
                    //objWAResponse = (WAResponse)JsonConvert.DeserializeObject(jsonsend, (typeof(WAResponse)));
                    //DataTable dtValuesend = (DataTable)JsonConvert.DeserializeObject(jsonsend, (typeof(DataTable)));
                    //if (Convert.ToString(dtValuesend.Rows[0]["status"]) == statusTrue || Convert.ToString(dtValuesend.Rows[0]["status"]) == msgQue)

                    JavaScriptSerializer json_serializer = new JavaScriptSerializer();

                    //WAResponse objWAResponse = (WAResponse)json_serializer.DeserializeObject(jsonsend);

                    WAResponse objWAResponse = JsonConvert.DeserializeObject<WAResponse>(jsonsend);

                    if (objWAResponse.status == "success")
                    {
                        objMainClass.WALOG(objMainClass.intCmpId, MESSAGE, CONTACTNO, SENTBY, "");
                        RESPONSE = "1, Mesage sent successfully";
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Mesage sent successfully.');", true);

                    }
                    else
                    {
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"" + Convert.ToString(dtValuesend.Rows[0]["message"]) + "\");", true);
                        RESPONSE = "0, WhatsApp " + Convert.ToString(objWAResponse.message);
                    }

                }
                else
                {
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + response.ErrorMessage + "\");", true);
                    RESPONSE = "0, " + response.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
                RESPONSE = "0, " + ex.Message;
            }

            return RESPONSE;
        }

        public string SendMediaFile(string MESSAGE, string CONTACTNO, string SENTBY, string URL)
        {
            string RESPONSE = "";
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                DataTable dt = new DataTable();
                dt = objMainClass.GetWAData("WHATSAPP", 1, "GETWADATA");


                instance = Convert.ToString(dt.Rows[0]["KEYNAME"]);
                accesstocken = Convert.ToString(dt.Rows[0]["KEYVALUE"]);
                sendMessageURL = Convert.ToString(dt.Rows[0]["OTHER"]);

                //var client = new RestClient("https://console.wa0.in/api/send.php?client_id=3a13f8abba76457a7bd9e6378b42ec15&instance=65cd6e0db8fe830fc320bdd5572b9bdf&number=918460591264&message=MESSAGE_HERE&type=text");

                //var client = new RestClient("https://console.wa0.in/api/send.php?client_id=" + client_id + "&instance=" + instance + "&number=" + CONTACTNO + "&message=" + MESSAGE + "&type=text");
                var client = new RestClient(sendMessageURL + "?number=" + CONTACTNO + "&type=media&message=" + MESSAGE + "&media_url=" + URL + "&instance_id=" + instance + "&access_token=" + accesstocken);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string jsonsend = response.Content;
                    //jsonsend = ("[" + jsonsend + "]").Replace("}{", "},{");
                    //WAResponse objWAResponse = new WAResponse();
                    //objWAResponse = (WAResponse)JsonConvert.DeserializeObject(jsonsend, (typeof(WAResponse)));
                    //DataTable dtValuesend = (DataTable)JsonConvert.DeserializeObject(jsonsend, (typeof(DataTable)));
                    //if (Convert.ToString(dtValuesend.Rows[0]["status"]) == statusTrue || Convert.ToString(dtValuesend.Rows[0]["status"]) == msgQue)

                    JavaScriptSerializer json_serializer = new JavaScriptSerializer();

                    //WAResponse objWAResponse = (WAResponse)json_serializer.DeserializeObject(jsonsend);

                    WAResponse objWAResponse = JsonConvert.DeserializeObject<WAResponse>(jsonsend);

                    if (objWAResponse.status == "success")
                    {
                        objMainClass.WALOG(objMainClass.intCmpId, MESSAGE, CONTACTNO, SENTBY, "");
                        RESPONSE = "1, Mesage sent successfully";
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text('Mesage sent successfully.');", true);

                    }
                    else
                    {
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"" + Convert.ToString(dtValuesend.Rows[0]["message"]) + "\");", true);
                        RESPONSE = "0, WhatsApp " + Convert.ToString(objWAResponse.message);
                    }

                }
                else
                {
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + response.ErrorMessage + "\");", true);
                    RESPONSE = "0, " + response.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
                RESPONSE = "0, " + ex.Message;
            }

            return RESPONSE;
        }


        public string SendMessageNewAPI(string MESSAGE, string CONTACTNO, string SENTBY, string URL)
        {
            string responsemsg = "";
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                DataTable dt = new DataTable();
                dt = objMainClass.GetWAData("WHATSAPP", 1, "GETWADATA");


                instance = Convert.ToString(dt.Rows[0]["KEYNAME"]);
                accesstocken = Convert.ToString(dt.Rows[0]["KEYVALUE"]);
                sendMessageURL = Convert.ToString(dt.Rows[0]["OTHER"]);

                NewWAClass objNewWAClass = new NewWAClass();
                objNewWAClass.number = CONTACTNO;
                objNewWAClass.type = "text";
                objNewWAClass.message = MESSAGE;
                objNewWAClass.media_url = URL;
                objNewWAClass.instance_id = instance;
                objNewWAClass.access_token = accesstocken;

                var client = new RestClient(sendMessageURL);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");

                var body = JsonConvert.SerializeObject(objNewWAClass);

                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string jsonsend = response.Content;
                    WAResponse objWAResponse = JsonConvert.DeserializeObject<WAResponse>(jsonsend);

                    if (objWAResponse.status == "success")
                    {
                        objMainClass.WALOG(objMainClass.intCmpId, MESSAGE, CONTACTNO, SENTBY, "");
                        responsemsg = "1, Mesage sent successfully";

                    }
                    else
                    {
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-success').modal();$('#lblSuccessMsg').text(\"" + Convert.ToString(dtValuesend.Rows[0]["message"]) + "\");", true);
                        responsemsg = "0, WhatsApp " + Convert.ToString(objWAResponse.message);
                    }

                }
                else
                {
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + response.ErrorMessage + "\");", true);
                    responsemsg = "0, " + response.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-danger').modal();$('#lblErrMsg').text(\"" + ex.Message + "\");", true);
                responsemsg = "0, " + ex.Message;
            }
            return responsemsg;
        }

        public class NewWAClass
        {
            public string number { get; set; }
            public string type { get; set; }
            public string message { get; set; }
            public string media_url { get; set; }
            public string instance_id { get; set; }
            public string access_token { get; set; }
        }

    }
}// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
//public class Data
//{
//    public Key key { get; set; }
//    public Message message { get; set; }
//    public string messageTimestamp { get; set; }
//    public string status { get; set; }
//}

//public class ExtendedTextMessage
//{
//    public string text { get; set; }
//}

//public class Key
//{
//    public string remoteJid { get; set; }
//    public bool fromMe { get; set; }
//    public string id { get; set; }
//}

//public class Message
//{
//    public ExtendedTextMessage extendedTextMessage { get; set; }
//}

//public class WAResponse
//{
//    public string status { get; set; }
//    public string message { get; set; }
//    public Data data { get; set; }
//}


public class ExtendedTextMessage
{
    public string text { get; set; }
}

public class Key
{
    public string remoteJid { get; set; }
    public bool fromMe { get; set; }
    public string id { get; set; }
}

public class Message
{
    public Key key { get; set; }
    public Message message { get; set; }
    public string messageTimestamp { get; set; }
    public ExtendedTextMessage extendedTextMessage { get; set; }
}

public class WAResponse
{
    public string status { get; set; }
    public Message message { get; set; }
}
