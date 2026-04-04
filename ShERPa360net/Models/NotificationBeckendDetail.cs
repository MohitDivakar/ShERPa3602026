using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShERPa360net.Models
{
    public class NotificationBeckendDetail
    {
        public string ESNNO { get; set; }
        public string NDSNO { get; set; }
        public int MODELSKEY { get; set; }
        public string MODELSKEYVALUE { get; set; }
        public string ISPFAULTCODE { get; set; }
        public int REPARIENGINEERKEY { get; set; }
        public string REPARIENGINEERKEYVALUE { get; set; }
        public int PRESCANNINGPROBLEMKEY { get; set; }
        public string PRESCANNINGPROBLEMKEYVALUE { get; set; }
        public int FAULTOBSERVEDKEY { get; set; }
        public string FAULTOBSERVEDKEYVALUE { get; set; }
        public int OBJECTPARTKEY { get; set; }
        public string OBJECTPARTKEYVALUE { get; set; }
        public int FAULTREASONKEY { get; set; }
        public string FAULTREASONKEYVALUE { get; set; }
        public int REPARITASKDESCRIPTIONKEY { get; set; }
        public string REPARITASKDESCRIPTIONKEYVALUE { get; set; }
        public string ASSIGNDATE { get; set; }
        public int ISNOTIFICATIONCORRECTED { get; set; }
    }
}