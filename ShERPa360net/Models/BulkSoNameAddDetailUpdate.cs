using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShERPa360net.Models
{
    public class BulkSoNameAddDetailUpdate
    {
        public string REFNO         { get; set; }
        public string SONO          { get; set; }
        public string CUSTNAME      { get; set; }
        public string CUSTADD1      { get; set; }
        public string CUSTADD2      { get; set; }
        public string CITY          { get; set; }
        public int STATEID          { get; set; }
        public string STATENAME     { get; set; }
        public int PINCODE          { get; set; }
        public int ISVALIDSO        { get; set; }
        public string ERRORMSG      { get; set; } = "";

    }
}