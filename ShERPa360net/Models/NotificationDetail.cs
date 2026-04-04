using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShERPa360net.Models
{
    public class NotificationDetail
    {
        public string ESNNO                          { get; set; }
        public int MODELSKEY                    { get; set; }
        public string MODELSKEYVALUE { get; set; }

        public string ISPFAULTCODE                   { get; set; }
        public int FAULTREPORTEDKEY                  { get; set; }
        public string FAULTREPORTEDKEYVALUE { get; set; }

        public int TAGKEY                            { get; set; }
        public string TAGKEYVALUE { get; set; }

        public string NDSNO                          { get; set; }
        public string RECEIVEDATE                    { get; set; }
        public string BOXNO                          { get; set; }
        public int  ISNOTIFICATIONCORRECTED          { get; set; }
    }
}