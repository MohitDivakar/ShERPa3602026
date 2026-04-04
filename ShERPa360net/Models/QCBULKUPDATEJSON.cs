using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShERPa360net.Models
{
    public class QCBULKUPDATEJSON
    {
        public string ASSIGNMENTNO          { get; set; } = "";
        public string NDSNO                 { get; set; } = "";
        public Int64 QCSTAGEKEY             { get; set; } = 0;
        public Int64 QCRESULTKEY            { get; set; } = 0;
        public string QCFAILREASON          { get; set; } = "";
        public Int64 QCFAILREASONKEY        { get; set; } = 0 ;
    }
}