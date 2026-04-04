using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ShERPa360net.Class
{
    public static  class PushNotificationContentDetail
    {
        public static string GETORDERRECEIVEDPUSHMESSAGE(string listingid,string productdetail)
        {
            try
            {
                string pushContent = "";
                pushContent = "Yay! You have received 1 New order of " + productdetail + " with " + listingid + ".";
                return pushContent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GETORDERRECEIVEDPUSHSUBJECT()
        {
            try
            {
                return "MOBEX-SELLER NEW ORDER RECEIVED";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static string GETINWARDPUSHMESSAGE(string listingid, string productdetail)
        {
            try
            {
                string pushContent = "";
                pushContent        = "Listing ID : " + listingid + " with " + productdetail + " has been received under Pre-Inward Diagnosis.";
                return pushContent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GETINWARDPUSHSUBJECT()
        {
            try
            {
                return "MOBEX-SELLER INWARDED";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GETRETURNREQUESTPUSHMESSAGE(string listingid, string productdetail,string reason)
        {
            try
            {
                string pushContent = "";
                pushContent = "Listing ID : " + listingid + " with " + productdetail + " has been processed for Return to your store due to " + reason + ".";
                return pushContent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GETRETURNREQUESTPUSHSUBJECT()
        {
            try
            {
                return "MOBEX-SELLER RETURN REQUEST";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GETRETURNRECEIVEDPUSHMESSAGE(string listingid, string productdetail)
        {
            try
            {
                string pushContent = "";
                pushContent = "Listing ID : " + listingid + " with " + productdetail + " has been returned to you successfully. Please contact your BDO if device is not received.";
                return pushContent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GETRETURNRECEIVEDPUSHSUBJECT()
        {
            try
            {
                return "MOBEX-SELLER RETURN RECEIVED";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}