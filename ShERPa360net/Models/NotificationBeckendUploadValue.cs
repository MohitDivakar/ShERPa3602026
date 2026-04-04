using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShERPa360net.Models
{
    public class NotificationBeckendUploadValue
    {
        public List<NotificationBeckendDetail> lstNotificationBeckendDetail { get; set; }
        public int totalcorrectvalue { get; set; }
        public int totalrejectvalue { get; set; }
    }
}