using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShERPa360net.Models
{
    public class WEBSITERATEQTYUPDATEHISTORYLOG
    {
        public string ITEMCODE { get; set; }
        public string ITEMDESC { get; set; }
        public string WEBSITE { get; set; }
        public int    UPDATEDQTY { get; set; }
        public decimal UPDATEDRATE { get; set; }
    }
}