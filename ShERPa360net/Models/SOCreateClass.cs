using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShERPa360net.Models
{
    public class SOCreateClass
    {
        public string SOTYPE { get; set; }

        public string SONO { get; set; }

        public string SODATE { get; set; }

        public string SEGMENT { get; set; }

        public string DISTCHNL { get; set; }

        public string BILLTOCODE { get; set; }

        public string SHIPTOCODE { get; set; }

        public string PAYMENTTERMS { get; set; }

        public string REMARKS { get; set; }

        public int STATUS { get; set; }

        public string NETVALUEAMT { get; set; }

        public string NETTAXAMT { get; set; }

        public string TOTALDISCOUNTAMT { get; set; }

        public string NETSOAMT { get; set; }

        public string REFNO { get; set; }

        public string REFDT { get; set; }

        public int SALESFORM { get; set; }

        public string CUSTNAME { get; set; }

        public string CUSTADD1 { get; set; }

        public string CUSTADD2 { get; set; }

        public string CUSTADD3 { get; set; }

        public string CITY { get; set; }
        public int STATEID { get; set; }

        public string PINCODE { get; set; }

        public string CUSTMOBILENO { get; set; }

        public string CUSTEMAILID { get; set; }

        public string JOBID { get; set; }

        public string COMMAGENT { get; set; }

        public int SCHEMEID { get; set; }

        public int PAYMODE { get; set; }

        public string PREPAIDAMT { get; set; }

        public string REMAINAMT { get; set; }

        public string TXNNO { get; set; }

        public string TXNDT { get; set; }

        public string PAYGATEWAY { get; set; }

        public string ENTITYID { get; set; }

        public string UTMSOURCE { get; set; }

        public string UTMCAMPAIGN { get; set; }

        public string UTMMEDIUM { get; set; }

        public string GOKWIKFLAG { get; set; }

        public List<ITEMDETAIL> ITEMDETAILS { get; set; }

        public List<TAXDETAIL> TAXDETAILS { get; set; }

        public List<CHARGEDETAIL> CHARGEDETAILS { get; set; }
    }

    public class CHARGEDETAIL
    {
        public string SRNO { get; set; }

        public string SONO { get; set; }

        public string CHARGETYPE { get; set; }

        public string CHARGEAMT { get; set; }
    }

    public class ITEMDETAIL
    {
        public string SONO { get; set; }

        public string SRNO { get; set; }

        public string ITEMCODE { get; set; }

        public string ITEMID { get; set; }

        public string ITEMDESC { get; set; }

        public string PLANTCODE { get; set; }

        public string LOCCD { get; set; }

        public string ITEMGROUPID { get; set; }

        public string SOQTY { get; set; }

        public string UOM { get; set; }

        public string RATE { get; set; }

        public string CAMOUNT { get; set; }

        public string DISCAMT { get; set; }

        public string DELIDT { get; set; }

        public string GLCODE { get; set; }

        public string COSTCENTER { get; set; }

        public string PRFCNT { get; set; }

        public string ITEMTEXT { get; set; }

        public string TAXAMT { get; set; }

        public string CUSTPARTNO { get; set; }

        public string CUSTPARTDESC { get; set; }

        public string IMEINO { get; set; }

        public string OLDITEMID { get; set; }

        public string CHANGEREASON { get; set; }

        public string GRADE { get; set; }

        public string JOBID { get; set; }

        public string SCHEMEID { get; set; }
    }

    public class TAXDETAIL
    {
        public string SRNO { get; set; }

        public string SONO { get; set; }

        public string SOSRNO { get; set; }

        public string CONDID { get; set; }

        public string CONDTYPE { get; set; }

        public string GLCODE { get; set; }

        public string RATE { get; set; }

        public string BASEAMT { get; set; }

        public string PID { get; set; }

        public string TAXAMT { get; set; }

        public string OPERATOR { get; set; }
    }

    public class SORESPONSE
    {
        public string MESSAGE { get; set; }

        public string SONO { get; set; }


    }

}