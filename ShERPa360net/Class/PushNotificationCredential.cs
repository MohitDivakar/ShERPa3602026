using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ShERPa360net.Class
{
    public static class PushNotificationCredential
    {
        public static string GETSERVERKEY()
        {
            return ConfigurationManager.AppSettings["SERVERAPIKEY"]; ;
        }

        public static string GETSENDERID()
        {
            return ConfigurationManager.AppSettings["SENDERID"] ;
        }

        public static string GETPUSHNOTIFICATIONURL()
        {
            return ConfigurationManager.AppSettings["PUSHNOTIFICATIONURL"]; ;
        }
    }
}