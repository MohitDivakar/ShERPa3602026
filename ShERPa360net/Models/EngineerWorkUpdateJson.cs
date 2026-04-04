using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShERPa360net.Models
{
    public class EngineerWorkUpdateJson
    {
        public string ASSIGNMENTNO                  { get; set; }
        public string NDSNO                         { get; set; }
        public Int64 OBJECTPARTKEY                  { get; set; }
        public Int64 FAULTOBSERVEDKEY               { get; set; }
        public Int64 FAULTREASONKEY                 { get; set; }
        public Int64 ACTIONKEY                      { get; set; }
        public Int64 REASONFORIRKEY                 { get; set; }
        public string PARTNAME                      { get; set; }
        public string PARTLOCATION                  { get; set; }
    }
}