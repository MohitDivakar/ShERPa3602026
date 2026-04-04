using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShERPa360net.Class
{
    public class ProductSpecificationPrimaryDetail
    {
        public int     BRAND_ID   { get; set; }
        public string  BRAND_DESC { get; set;  }
        public int     MODEL_ID { get; set; }

        public string MODEL_NAME { get; set; }

        public string RAMSIZE { get; set; }

        public string ROMSIZE { get; set; }

        public string COLOR { get; set; }

        public string LAUNCHYEAR { get; set; }
        public Decimal NEWRATE { get; set; }
        public Decimal BASICPURRATE { get; set; }
        public Decimal BASICPURRATEFORBGRADE { get; set; }
        public Decimal BASICPURRATEFORCGRADE { get; set; }
        public Decimal FinalApproveListingAmount { get; set; }
    }
}