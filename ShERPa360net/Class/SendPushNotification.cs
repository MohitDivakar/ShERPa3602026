using FirebaseAdmin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net.Class
{
    public static class SendPushNotification
    {
        public static void SendPushNotificaion(string SUBJECT , string SENTMSG , int DEALERID)
        {
            try
            {
                string MOBEXSELLERVENDORTOKEN = "";
                MainClass objDetail           = new MainClass();
                MOBEXSELLERVENDORTOKEN        = objDetail.GetMobexSellerTokenFromDealerID(DEALERID);

                //Call the API TO SEND PUSH NOTIFICATION
                var data = new
                {
                    to   = MOBEXSELLERVENDORTOKEN,
                    data = new
                    {
                        message = SENTMSG,
                        name = SUBJECT,
                        userId = "1",
                        status = true
                    },
                    notification = new
                    {
                        title = SUBJECT,
                        text = SENTMSG,
                        sound = "default",
                        body = SENTMSG
                    }
                };

                var json               = JsonConvert.SerializeObject(data);
                Byte[] bytearraydetail = Encoding.UTF8.GetBytes(json);

                //string serverapikey = "AAAAbqO8CRk:APA91bGoHCJ3dCRnUanBHUdS857E03x3LWerzBXeBo18A4NAcvTn5as6YgxqiVZHAsnLi8FNCjXSpIRVEPp4lKi_hy75KroNRO2mArv1IqHbe3U5tbgsVJUqZ2zTWjAJy1tz5KUDxajF";
                //string senderid = "475193411865";
                //475193411865
                WebRequest tRequest = WebRequest.Create(PushNotificationCredential.GETPUSHNOTIFICATIONURL());
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                tRequest.Headers.Add($"Authorization: key={PushNotificationCredential.GETSERVERKEY()}");
                tRequest.Headers.Add($"Sender: id={PushNotificationCredential.GETSENDERID()}");
                tRequest.ContentLength = bytearraydetail.Length;
                Stream dataStream = tRequest.GetRequestStream();
                dataStream.Write(bytearraydetail, 0, bytearraydetail.Length);
                dataStream.Close();

                WebResponse tresponse = tRequest.GetResponse();
                dataStream = tresponse.GetResponseStream();
                StreamReader tReasder = new StreamReader(dataStream);
                string sResponseFromServer = tReasder.ReadToEnd();
                var objResults = JsonConvert.DeserializeObject<PushNotificationResponse>(sResponseFromServer.ToString());
                tReasder.Close();
                dataStream.Close();
                tresponse.Close();

                //ADD NOTIFICATION LOG
                MainClass objAddNotification = new MainClass();
                objAddNotification.AddNotificationLog(SUBJECT, SENTMSG, DEALERID, objResults.success == 1 ? 1 : 0);
                //ADD NOTIFICATION LOG
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}