using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;

namespace ShERPa360net.Models
{
    public static class SMSSend
    {

        public static bool SendSMSOld(string mobileno, string sms)
        {
            bool IsSend = false;
            try
            {
                //Send SMS API 
                HttpWebRequest request;
                string url;
                string username;
                string password;
                string host;

                host = "http://alerts.sinfini.com";
                username = "qarmatek";
                password = "dhaval12345";

                url = host + "/api/web2sms.php?"
                + "username=" + username
                + "&password=" + password
                + "&sender=" + "MOBEXX"
                + "&to=" + mobileno
                + "&message=" + sms;
                request = (HttpWebRequest)HttpWebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusDescription == "OK")
                {
                    IsSend = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IsSend;
        }

        public static bool SendSMS(string mobileno, string sms)
        {
            bool IsSend = false;
            try
            {
                MainClass objMainClass = new MainClass();

                //Send SMS API 
                HttpWebRequest request;
                string url;
                string username;
                string password;
                string host;
                string originator;
                originator = "MOBEXX";
                string APIKEY, CHANNEL, DCS, FLASH, ROUTE, MPREFIX;

                DataTable dtAPI = new DataTable();
                dtAPI = objMainClass.GetWAData("NEWSMSNEW", 1, "GETWADATA");

                if (dtAPI.Rows.Count > 0)
                {

                    host = Convert.ToString(dtAPI.Rows[0]["OTHER"]);

                    APIKEY = Convert.ToString(dtAPI.Rows[0]["KEYVALUE"]);
                    CHANNEL = Convert.ToString(dtAPI.Rows[0]["TOKEN"]);
                    DCS = Convert.ToString(dtAPI.Rows[0]["VERSION"]);
                    FLASH = Convert.ToString(dtAPI.Rows[0]["AREA"]);
                    ROUTE = Convert.ToString(dtAPI.Rows[0]["APITYPE"]);
                    MPREFIX = Convert.ToString(dtAPI.Rows[0]["UNIQUECODE"]);


                    host = host.Replace("@APIKEY", APIKEY).Replace("@SENDER", originator).Replace("@CHANL", CHANNEL).Replace("@DCS", DCS).Replace("@FLASH", FLASH).Replace("@ROUTE", ROUTE).Replace("@SMSTRN", sms).Replace("@CONTACT", MPREFIX + "" + mobileno);

                    //host = "http://alerts.sinfini.com";
                    //username = "qarmatek";
                    //password = "dhaval12345";

                    //url = host + "/api/web2sms.php?"
                    //+ "username=" + username
                    //+ "&password=" + password
                    //+ "&sender=" + "MOBEXX"
                    //+ "&to=" + mobileno
                    //+ "&message=" + sms;

                    url = host;

                    request = (HttpWebRequest)HttpWebRequest.Create(url);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    if (response.StatusDescription == "OK")
                    {
                        IsSend = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IsSend;
        }


    }
}