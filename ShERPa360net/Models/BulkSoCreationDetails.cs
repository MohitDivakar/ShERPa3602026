using java.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShERPa360net.Models
{
    public class BulkSoCreationDetails
    {
        public int CMPID { get; set; }
        public string SOTYPE { get; set; }
        public string SONO { get; set; }
        public string SODT  { get; set; }
        public string SEGMENT{ get; set; }
        public string DISTCHNL{ get; set; }
        public string BILLTOCODE{ get; set; }
        public string SHIPTOCODE{ get; set; }
        public string PMTTERMS{ get; set; }
        public string REMARK{ get; set; }
        public int STATUS{ get; set; }
        public Decimal NETMATVALUE { get; set; }
        public Decimal NETTAXAMT { get; set; }
        public Decimal DISCOUNT { get; set; }
        public Decimal NETSOAMT { get; set; }
        public string CNCLREASON {  get; set; }
        public string REFNO { get; set; }
        public string REFDT { get; set; }
        public int SALESFROM { get; set; }
        public string CUSTNAME { get; set; }
        public string CUSTADD1 { get; set; }
        public string CUSTADD2 { get; set; }
        public string CUSTADD3 { get; set; }
        public string CITY { get; set; }    
        public int STATEID { get; set; }
        public int PINCODE { get; set; }
        public int CUSTMOBILENO { get; set; }
        public string CUSTEMAILID { get; set; }
        public string JOBID { get; set; }
        public string REFSONO { get; set; }
        public string COMMAGENT { get; set; }
        public int SCHEMEID { get; set; }
        public int PAYMODE { get; set; }
        public Decimal PREPAIDAMT{ get; set; }
        public Decimal REMAINAMT { get; set; }
        public int SRNO { get; set; }   
        public int ITEMID { get; set; }
        public string ITEMDESC { get; set; }
        public string PLANTCD { get; set; }
        public string LOCCD { get; set; }
        public int ITEMGRPID { get; set; }
        public int SOQTY { get; set; }
        public int UOM { get; set; }
        public decimal RATE { get; set; }
        public decimal CAMOUNT { get; set; }
        public decimal DISCAMT { get; set; }
        public string DELIDT { get; set; }
        public string GLCD { get; set; }
        public string CSTCENTCD { get; set; }
        public string PRFCNT { get; set; }
        public string ITEMTEXT{ get; set; }
        public Decimal TAXAMT { get; set; }
        public string CUSTPARTNO { get; set; }
        public string CUSTPARTDESC { get; set; }
        public string CUSTPARTDESC2 { get; set; }
        public int OLDITEMID { get; set; }
        public string CHANGEREASON { get; set; }
        public string PRODGRADE { get; set; }
        public int ISSOALREADYCREATED { get; set; } = 0;
        public int ISVALIDREQUEST { get; set; } = 1;
        public string ALREADYCREATEDSONO { get; set; }
        public decimal LOCKAMT { get; set; }
    }
}