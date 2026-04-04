using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShERPa360net.Models
{
    public class NotificationBeckendPartsUploadValue
    {
        public List<NotificationBeckendPartsDetail> lstNotificationBeckendPartsDetail { get; set; }
        public int totalcorrectvalue { get; set; }
        public int totalrejectvalue { get; set; }
    }
}