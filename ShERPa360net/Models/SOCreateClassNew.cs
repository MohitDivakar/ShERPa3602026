using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShERPa360net.Models
{
    public class SOCreateClassNew
    {

        /// <summary>
        /// PSO
        /// </summary>
        [Required]
        public string SOTYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SONO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string SODATE { get; set; }

        /// <summary>
        /// 1015
        /// </summary>
        [Required]
        public string SEGMENT { get; set; }

        /// <summary>
        /// 50
        /// </summary>
        [Required]
        public string DISTCHNL { get; set; }

        /// <summary>
        /// 0000010003
        /// </summary>
        [Required]
        public string BILLTOCODE { get; set; }

        /// <summary>
        /// 0000010003
        /// </summary>
        [Required]
        public string SHIPTOCODE { get; set; }

        /// <summary>
        /// ADV
        /// </summary>
        [Required]
        public string PAYMENTTERMS { get; set; }

        /// <summary>
        /// CUSTOMER REMARKS
        /// </summary>
        [Required]
        public string REMARKS { get; set; }

        /// <summary>
        /// 57
        /// </summary>
        [Required]
        public int STATUS { get; set; }

        /// <summary>
        /// VALUE OF PRODUCT
        /// </summary>
        [Required]
        public string NETVALUEAMT { get; set; }

        /// <summary>
        /// TAX AMT ON PRODUCT
        /// </summary>
        [Required]
        public string NETTAXAMT { get; set; }

        /// <summary>
        /// DIACOUNT ON PRODUCT
        /// </summary>
        [Required]
        public string TOTALDISCOUNTAMT { get; set; }

        /// <summary>
        /// NET SO AMT
        /// </summary>
        [Required]
        public string NETSOAMT { get; set; }

        /// <summary>
        /// ORDER ID
        /// </summary>
        [Required]
        public string REFNO { get; set; }

        /// <summary>
        /// ORDER DATE
        /// </summary>
        [Required]
        public string REFDT { get; set; }

        /// <summary>
        /// 11193
        /// </summary>
        [Required]
        public int SALESFORM { get; set; }

        /// <summary>
        /// CUSTOMER NAME
        /// </summary>
        [Required]
        public string CUSTNAME { get; set; }

        /// <summary>
        /// ADDRESS LINE 1
        /// </summary>
        [Required]
        public string CUSTADD1 { get; set; }

        /// <summary>
        /// ADDRESS LINE 2
        /// </summary>
        [Required]
        public string CUSTADD2 { get; set; }

        /// <summary>
        /// ADDRESS LINE 3
        /// </summary>
        [Required]
        public string CUSTADD3 { get; set; }

        /// <summary>
        /// CITY
        /// </summary>
        [Required]
        public string CITY { get; set; }

        /// <summary>
        /// STATEID
        /// </summary>
        [Required]
        public int STATEID { get; set; }

        /// <summary>
        /// PINCODE
        /// </summary>
        [Required]
        public string PINCODE { get; set; }

        /// <summary>
        /// CUSTOMER MOBILE NO
        /// </summary>
        [Required]
        public string CUSTMOBILENO { get; set; }

        /// <summary>
        /// CUSTOMER EMAIL ID
        /// </summary>
        public string CUSTEMAILID { get; set; }

        /// <summary>
        /// JOB ID
        /// </summary>
        public string JOBID { get; set; }

        /// <summary>
        /// COMMISSION AGENT ID - 0000050013 - TARAN, 0000050019 - SOHEL, 0000050113 - JYOTI, 0000050103 - KAMINI, 0000050123 - BHOOMIKA
        /// </summary>
        [Required]
        public string COMMAGENT { get; set; }

        /// <summary>
        /// SCHEME ID  11913 - NORMAL PURCHASE, 11914 - DEKH KE LO
        /// </summary>
        [Required]
        public int SCHEMEID { get; set; }

        /// <summary>
        /// PAYMODE - 1	- COD, 5 - NEFT
        /// </summary>
        [Required]
        public int PAYMODE { get; set; }

        /// <summary>
        /// PREPAID AMT
        /// </summary>
        [Required]
        public string PREPAIDAMT { get; set; }

        /// <summary>
        /// REMAIN AMT
        /// </summary>
        [Required]
        public string REMAINAMT { get; set; }

        /// <summary>
        /// TRANSACTION NUMBER / ID
        /// </summary>
        public string TXNNO { get; set; }

        /// <summary>
        /// TRANSACTION DATE
        /// </summary>
        public string TXNDT { get; set; }

        /// <summary>
        /// PAYMENT GATEWAY - 1	FOR RAZOR PAY, 2 FOR DIRECT PMT
        /// </summary>
        public string PAYGATEWAY { get; set; }

        /// <summary>
        /// ENTITY ID - UNIQUE ID OF WEBSITE
        /// </summary>
        public string ENTITYID { get; set; }


        /// <summary>
        /// SOURCE OF ORDER LIKE GOOGLE, INSTAGRAM, WEBSITE, KIOSK, ETC
        /// </summary>
        public string UTMSOURCE { get; set; }

        /// <summary>
        /// CAMPAIGN LIKE FESTIVAL SPECIAL CAMPAIGN, SPECIAL DAYS CAMPAIGN, ETC
        /// </summary>
        public string UTMCAMPAIGN { get; set; }

        /// <summary>
        /// MEDIUM - REFERAL NAME
        /// </summary>
        public string UTMMEDIUM { get; set; }

        /// <summary>
        /// GO KWIK FLAG LIKE LOW RISK, HIGH RISK, MEDIUM RISK, ETC
        /// </summary>
        public string GOKWIKFLAG { get; set; }

        /// <summary>
        /// DETAILS OF ITEM
        /// </summary>
        [Required]
        public List<LSTITEMDETAILSNEW> ITEMDETAILS { get; set; }

        /// <summary>
        /// TAX DETIALS
        /// </summary>
        public List<LSTTAXDETAILSNEW> TAXDETAILS { get; set; }

        /// <summary>
        /// OTHER CHARGES
        /// </summary>
        public List<LSTCHARGEDETAILSNEW> CHARGEDETAILS { get; set; }


        /// <summary>
        /// Create by User ID
        /// </summary>
        public int CREATEBY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GSTNO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string REFERAL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string REFEREALNAME { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class LSTITEMDETAILSNEW
    {
        /// <summary>
        /// SONO
        /// </summary>
        public string SONO { get; set; }

        /// <summary>
        /// SR NO
        /// </summary>
        [Required]
        public string SRNO { get; set; }

        /// <summary>
        /// ITEMCODE
        /// </summary>
        [Required]
        public string ITEMCODE { get; set; }

        /// <summary>
        /// ITEMID
        /// </summary>
        public string ITEMID { get; set; }

        /// <summary>
        /// ITEMDESC
        /// </summary>
        public string ITEMDESC { get; set; }

        /// <summary>
        /// PLANTCODE 1002
        /// </summary>
        [Required]
        public string PLANTCODE { get; set; }

        /// <summary>
        /// LOCATION CODE WS03
        /// </summary>
        [Required]
        public string LOCCD { get; set; }

        /// <summary>
        /// ITEM GROUP ID
        /// </summary>
        public string ITEMGROUPID { get; set; }

        /// <summary>
        /// ITEM QUANTITY
        /// </summary>
        [Required]
        public string SOQTY { get; set; }

        /// <summary>
        /// UNIT OF MEASUREMENT 1
        /// </summary>
        [Required]
        public string UOM { get; set; }

        /// <summary>
        /// ITEM RATE
        /// </summary>
        [Required]
        public string RATE { get; set; }

        /// <summary>
        /// TOTAL AMTOUNT
        /// </summary>
        [Required]
        public string CAMOUNT { get; set; }

        /// <summary>
        /// DISCOUNT ON ITEM
        /// </summary>
        public string DISCAMT { get; set; }

        /// <summary>
        /// DELIVERY DATE
        /// </summary>
        [Required]
        public string DELIDT { get; set; }

        /// <summary>
        /// GL CODE
        /// </summary>
        public string GLCODE { get; set; }

        /// <summary>
        /// COST CENTER
        /// </summary>
        [Required]
        public string COSTCENTER { get; set; }

        /// <summary>
        /// PROFIT COUNT
        /// </summary>
        public string PRFCNT { get; set; }

        /// <summary>
        /// ITEM REMARKS
        /// </summary>
        public string ITEMTEXT { get; set; }

        /// <summary>
        /// TAX ON ITEM
        /// </summary>
        public string TAXAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CUSTPARTNO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CUSTPARTDESC { get; set; }

        /// <summary>
        /// IMEI NO
        /// </summary>
        public string IMEINO { get; set; }

        /// <summary>
        /// OLD ITEM ID
        /// </summary>
        public string OLDITEMID { get; set; }

        /// <summary>
        /// CHANGE REASON
        /// </summary>
        public string CHANGEREASON { get; set; }

        /// <summary>
        /// ITEM GRADE
        /// </summary>
        public string GRADE { get; set; }

        /// <summary>
        /// JOB ID
        /// </summary>
        public string JOBID { get; set; }

        /// <summary>
        /// SCHEME ID 11418
        /// </summary>
        [Required]
        public string SCHEMEID { get; set; }

        /// <summary>
        /// Installation Required for Product or Not
        /// </summary>
        public int INSTALLATION { get; set; }

        /// <summary>
        /// Demo Required for Product or Not
        /// </summary>
        public int DEMO { get; set; }

        /// <summary>
        /// Discount Approve by
        /// </summary>
        public string APPROVEBY { get; set; }
    }

    public class SOCreateClassNewV1
    {

        /// <summary>
        /// PSO
        /// </summary>
        [Required]
        public string SOTYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SONO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string SODATE { get; set; }

        /// <summary>
        /// 1015
        /// </summary>
        [Required]
        public string SEGMENT { get; set; }

        /// <summary>
        /// 50
        /// </summary>
        [Required]
        public string DISTCHNL { get; set; }

        /// <summary>
        /// 0000010003
        /// </summary>
        [Required]
        public string BILLTOCODE { get; set; }

        /// <summary>
        /// 0000010003
        /// </summary>
        [Required]
        public string SHIPTOCODE { get; set; }

        /// <summary>
        /// ADV
        /// </summary>
        [Required]
        public string PAYMENTTERMS { get; set; }

        /// <summary>
        /// CUSTOMER REMARKS
        /// </summary>
        [Required]
        public string REMARKS { get; set; }

        /// <summary>
        /// 57
        /// </summary>
        [Required]
        public int STATUS { get; set; }

        /// <summary>
        /// VALUE OF PRODUCT
        /// </summary>
        [Required]
        public string NETVALUEAMT { get; set; }

        /// <summary>
        /// TAX AMT ON PRODUCT
        /// </summary>
        [Required]
        public string NETTAXAMT { get; set; }

        /// <summary>
        /// DIACOUNT ON PRODUCT
        /// </summary>
        [Required]
        public string TOTALDISCOUNTAMT { get; set; }

        /// <summary>
        /// NET SO AMT
        /// </summary>
        [Required]
        public string NETSOAMT { get; set; }

        /// <summary>
        /// ORDER ID
        /// </summary>
        [Required]
        public string REFNO { get; set; }

        /// <summary>
        /// ORDER DATE
        /// </summary>
        [Required]
        public string REFDT { get; set; }

        /// <summary>
        /// 11193
        /// </summary>
        [Required]
        public int SALESFORM { get; set; }

        /// <summary>
        /// CUSTOMER NAME
        /// </summary>
        [Required]
        public string CUSTNAME { get; set; }

        /// <summary>
        /// ADDRESS LINE 1
        /// </summary>
        [Required]
        public string CUSTADD1 { get; set; }

        /// <summary>
        /// ADDRESS LINE 2
        /// </summary>
        [Required]
        public string CUSTADD2 { get; set; }

        /// <summary>
        /// ADDRESS LINE 3
        /// </summary>
        [Required]
        public string CUSTADD3 { get; set; }

        /// <summary>
        /// CITY
        /// </summary>
        [Required]
        public string CITY { get; set; }

        /// <summary>
        /// STATEID
        /// </summary>
        [Required]
        public int STATEID { get; set; }

        /// <summary>
        /// PINCODE
        /// </summary>
        [Required]
        public string PINCODE { get; set; }

        /// <summary>
        /// CUSTOMER MOBILE NO
        /// </summary>
        [Required]
        public string CUSTMOBILENO { get; set; }

        /// <summary>
        /// CUSTOMER EMAIL ID
        /// </summary>
        public string CUSTEMAILID { get; set; }

        /// <summary>
        /// JOB ID
        /// </summary>
        public string JOBID { get; set; }

        /// <summary>
        /// COMMISSION AGENT ID - 0000050013 - TARAN, 0000050019 - SOHEL, 0000050113 - JYOTI, 0000050103 - KAMINI, 0000050123 - BHOOMIKA
        /// </summary>
        [Required]
        public string COMMAGENT { get; set; }

        /// <summary>
        /// SCHEME ID  11913 - NORMAL PURCHASE, 11914 - DEKH KE LO
        /// </summary>
        [Required]
        public int SCHEMEID { get; set; }

        /// <summary>
        /// PAYMODE - 1	- COD, 5 - NEFT
        /// </summary>
        [Required]
        public int PAYMODE { get; set; }

        /// <summary>
        /// PREPAID AMT
        /// </summary>
        [Required]
        public string PREPAIDAMT { get; set; }

        /// <summary>
        /// REMAIN AMT
        /// </summary>
        [Required]
        public string REMAINAMT { get; set; }

        /// <summary>
        /// TRANSACTION NUMBER / ID
        /// </summary>
        public string TXNNO { get; set; }

        /// <summary>
        /// TRANSACTION DATE
        /// </summary>
        public string TXNDT { get; set; }

        /// <summary>
        /// PAYMENT GATEWAY - 1	FOR RAZOR PAY, 2 FOR DIRECT PMT
        /// </summary>
        public string PAYGATEWAY { get; set; }

        /// <summary>
        /// ENTITY ID - UNIQUE ID OF WEBSITE
        /// </summary>
        public string ENTITYID { get; set; }


        /// <summary>
        /// SOURCE OF ORDER LIKE GOOGLE, INSTAGRAM, WEBSITE, KIOSK, ETC
        /// </summary>
        public string UTMSOURCE { get; set; }

        /// <summary>
        /// CAMPAIGN LIKE FESTIVAL SPECIAL CAMPAIGN, SPECIAL DAYS CAMPAIGN, ETC
        /// </summary>
        public string UTMCAMPAIGN { get; set; }

        /// <summary>
        /// MEDIUM - REFERAL NAME
        /// </summary>
        public string UTMMEDIUM { get; set; }

        /// <summary>
        /// GO KWIK FLAG LIKE LOW RISK, HIGH RISK, MEDIUM RISK, ETC
        /// </summary>
        public string GOKWIKFLAG { get; set; }

        /// <summary>
        /// DETAILS OF ITEM
        /// </summary>
        [Required]
        public List<LSTITEMDETAILSNEWV1> ITEMDETAILS { get; set; }

        /// <summary>
        /// TAX DETIALS
        /// </summary>
        public List<LSTTAXDETAILSNEW> TAXDETAILS { get; set; }

        /// <summary>
        /// OTHER CHARGES
        /// </summary>
        public List<LSTCHARGEDETAILSNEW> CHARGEDETAILS { get; set; }


        /// <summary>
        /// Create by User ID
        /// </summary>
        public int CREATEBY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GSTNO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string REFERAL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string REFEREALNAME { get; set; }
    }
    public class LSTITEMDETAILSNEWV1
    {
        /// <summary>
        /// SONO
        /// </summary>
        public string SONO { get; set; }

        /// <summary>
        /// SR NO
        /// </summary>
        [Required]
        public string SRNO { get; set; }

        /// <summary>
        /// ITEMCODE
        /// </summary>
        [Required]
        public string ITEMCODE { get; set; }

        /// <summary>
        /// ITEMID
        /// </summary>
        public string ITEMID { get; set; }

        /// <summary>
        /// ITEMDESC
        /// </summary>
        public string ITEMDESC { get; set; }

        /// <summary>
        /// PLANTCODE 1002
        /// </summary>
        [Required]
        public string PLANTCODE { get; set; }

        /// <summary>
        /// LOCATION CODE WS03
        /// </summary>
        [Required]
        public string LOCCD { get; set; }

        /// <summary>
        /// ITEM GROUP ID
        /// </summary>
        public string ITEMGROUPID { get; set; }

        /// <summary>
        /// ITEM QUANTITY
        /// </summary>
        [Required]
        public string SOQTY { get; set; }

        /// <summary>
        /// UNIT OF MEASUREMENT 1
        /// </summary>
        [Required]
        public string UOM { get; set; }

        /// <summary>
        /// ITEM RATE
        /// </summary>
        [Required]
        public string RATE { get; set; }

        /// <summary>
        /// TOTAL AMTOUNT
        /// </summary>
        [Required]
        public string CAMOUNT { get; set; }

        /// <summary>
        /// DISCOUNT ON ITEM
        /// </summary>
        public string DISCAMT { get; set; }

        /// <summary>
        /// DELIVERY DATE
        /// </summary>
        [Required]
        public string DELIDT { get; set; }

        /// <summary>
        /// GL CODE
        /// </summary>
        public string GLCODE { get; set; }

        /// <summary>
        /// COST CENTER
        /// </summary>
        [Required]
        public string COSTCENTER { get; set; }

        /// <summary>
        /// PROFIT COUNT
        /// </summary>
        public string PRFCNT { get; set; }

        /// <summary>
        /// ITEM REMARKS
        /// </summary>
        public string ITEMTEXT { get; set; }

        /// <summary>
        /// TAX ON ITEM
        /// </summary>
        public string TAXAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CUSTPARTNO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CUSTPARTDESC { get; set; }

        /// <summary>
        /// IMEI NO
        /// </summary>
        public string IMEINO { get; set; }

        /// <summary>
        /// OLD ITEM ID
        /// </summary>
        public string OLDITEMID { get; set; }

        /// <summary>
        /// CHANGE REASON
        /// </summary>
        public string CHANGEREASON { get; set; }

        /// <summary>
        /// ITEM GRADE
        /// </summary>
        public string GRADE { get; set; }

        /// <summary>
        /// JOB ID
        /// </summary>
        public string JOBID { get; set; }

        /// <summary>
        /// SCHEME ID 11418
        /// </summary>
        [Required]
        public string SCHEMEID { get; set; }

        /// <summary>
        /// Installation Required for Product or Not
        /// </summary>
        public int INSTALLATION { get; set; }

        /// <summary>
        /// Demo Required for Product or Not
        /// </summary>
        public int DEMO { get; set; }

        /// <summary>
        /// Discount Approve by
        /// </summary>
        public string APPROVEBY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int EWID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EWDESC { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal EWPRICE { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class LSTTAXDETAILSNEW
    {
        /// <summary>
        /// TAX SR NO 
        /// </summary>
        [Required]
        public string SRNO { get; set; }

        /// <summary>
        /// SO NO
        /// </summary>
        [Required]
        public string SONO { get; set; }

        /// <summary>
        /// SO SR NO
        /// </summary>
        [Required]
        public string SOSRNO { get; set; }

        /// <summary>
        /// CONDITION ID
        /// </summary>
        [Required]
        public string CONDID { get; set; }

        /// <summary>
        /// CONDITION TYPE
        /// </summary>
        [Required]
        public string CONDTYPE { get; set; }

        /// <summary>
        /// GL CODE
        /// </summary>
        public string GLCODE { get; set; }

        /// <summary>
        /// RATE
        /// </summary>
        [Required]
        public string RATE { get; set; }

        /// <summary>
        /// BASE AMOUNT
        /// </summary>
        [Required]
        public string BASEAMT { get; set; }

        /// <summary>
        /// PID 0
        /// </summary>
        [Required]
        public string PID { get; set; }

        /// <summary>
        /// TAX AMOUNT
        /// </summary>
        [Required]
        public string TAXAMT { get; set; }

        /// <summary>
        /// OPERATOR +
        /// </summary>
        [Required]
        public string OPERATOR { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public class LSTCHARGEDETAILSNEW
    {
        /// <summary>
        /// OTHER CHARGES SR NO
        /// </summary>
        [Required]
        public string SRNO { get; set; }

        /// <summary>
        /// SO NO
        /// </summary>
        [Required]
        public string SONO { get; set; }

        /// <summary>
        /// CHARGE TYPE
        /// </summary>
        [Required]
        public string CHARGETYPE { get; set; }

        /// <summary>
        /// CHARGE AMOUNT
        /// </summary>
        [Required]
        public string CHARGEAMT { get; set; }

    }


    public class SORESPONSENEW
    {
        /// <summary>
        /// 
        /// </summary>
        public string MESSAGE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SONO { get; set; }


    }

    public class CreateDCMST
    {
        public int CMPID { get; set; }

        public string DOTYPE { get; set; }

        public string DOCNO { get; set; }

        public string DODATE { get; set; }

        public string REMARK { get; set; }

        public string SEGMENT { get; set; }

        public string DISTCHNL { get; set; }

        public int STATUS { get; set; }

        public string NETDOAMOUNT { get; set; }

        public int CREATEBY { get; set; }

        public List<CreateDCDTL> CreateDCDTLList { get; set; }
    }

    public class CreateDCDTL
    {
        public int CMPID { get; set; }

        public string DOCNO { get; set; }

        public string SONO { get; set; }

        public string SRNO { get; set; }

        public string SOSRNO { get; set; }

        public string ITEMID { get; set; }

        public string ITEMDESC { get; set; }

        public string PLANTCD { get; set; }

        public string LOCCD { get; set; }

        public string ITEMGRPID { get; set; }

        public string SOQTY { get; set; }

        public string UOM { get; set; }

        public string RATE { get; set; }

        public string CAMOUNT { get; set; }

        public string DISCAMT { get; set; }

        public string GLCODE { get; set; }

        public string CSTCENTCD { get; set; }

        public string PRFCNT { get; set; }

        public string ITEMTEXT { get; set; }

        public string TAXAMT { get; set; }
    }


    public class SORESPONSEDC
    {
        /// <summary>
        /// 
        /// </summary>
        public string MESSAGE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DCNO { get; set; }


    }

    public class CreateSIMstNew
    {
        public int CMPID { get; set; }

        public string SITYPE { get; set; }

        public string SINO { get; set; }

        public string SIDT { get; set; }

        public string BILLTOPARTY { get; set; }

        public string BILLTOPARTYNM { get; set; }

        public string BILLADDID { get; set; }

        public string JOBID { get; set; }

        public string AGENTCD { get; set; }

        public string NETAMT { get; set; }

        public string DISCAMT { get; set; }

        public string OTHERS { get; set; }

        public string ROUNDOFF { get; set; }

        public string TOTAMT { get; set; }

        public string REMARK { get; set; }

        public string SEGMENT { get; set; }

        public string DISTCHNL { get; set; }

        public int STATUS { get; set; }

        public int CREATEBY { get; set; }

        public string CREATEDATE { get; set; }

        public string UPDATEBY { get; set; }

        public string UPDATEDATE { get; set; }

        public string CNCLREASON { get; set; }

        public string COUPONNO { get; set; }

        public string PMTTERMS { get; set; }

        public string SHIPTOCODE { get; set; }

        public string SHIPTOPARTYNM { get; set; }

        public string NETTAXAMT { get; set; }

        public string OTHERDISCAMT { get; set; }

        public string SONO { get; set; }

        public string TRANCHG { get; set; }

        public string PAYMODE { get; set; }

        public string TAXDISC { get; set; }

        public string TAXOTH { get; set; }

        public string REFSINO { get; set; }

        public string PREPAIDAMT { get; set; }

        public string REMAINAMT { get; set; }

        public string ADJAMT { get; set; }

        public string PENDINGAMT { get; set; }

        public string SIGST { get; set; }

        public List<CreateSIDtlNew> CreateSIDtlNewList { get; set; }

        public List<CreateSITaxNew> CreateSITaxNewList { get; set; }

    }


    public class CreateSIDtlNew
    {

        public int CMPID { get; set; }

        public string SITYPE { get; set; }


        public string SINO { get; set; }


        [Required]
        public string SRNO { get; set; }

        public string DCNO { get; set; }

        public string DCSRNO { get; set; }


        [Required]
        public string ITEMCODE { get; set; }


        public string ITEMID { get; set; }


        public string ITEMDESC { get; set; }

        public string QTY { get; set; }

        public string UOM { get; set; }

        public string RATE { get; set; }

        public string CAMOUNT { get; set; }

        public string PLANTCD { get; set; }

        public string LOCCD { get; set; }

        public string PRODMAKE { get; set; }

        public string PRODMODEL { get; set; }

        public string COUPONVALUE { get; set; }

        public string DISCAMT { get; set; }

        public string GLCODE { get; set; }

        public string CSTCENTCD { get; set; }

        public string PRFCNT { get; set; }

        public string ITEMTEXT { get; set; }

        public string ITEMGRPID { get; set; }

        public string JOBID { get; set; }

        public string SALESWARRANTY { get; set; }

        public string SLRREASON { get; set; }

        public string JOBSTATUS { get; set; }
    }

    public class CreateSITaxNew
    {

        public int CMPID { get; set; }

        public string CONDORDER { get; set; }

        /// <summary>
        /// TAX SR NO 
        /// </summary>
        [Required]
        public string SRNO { get; set; }

        /// <summary>
        /// SO NO
        /// </summary>
        [Required]
        public string SINO { get; set; }

        /// <summary>
        /// SO SR NO
        /// </summary>
        [Required]
        public string SISRNO { get; set; }

        /// <summary>
        /// CONDITION ID
        /// </summary>
        [Required]
        public string CONDID { get; set; }

        /// <summary>
        /// CONDITION TYPE
        /// </summary>
        [Required]
        public string CONDTYPE { get; set; }

        /// <summary>
        /// GL CODE
        /// </summary>
        public string GLCODE { get; set; }

        /// <summary>
        /// RATE
        /// </summary>
        [Required]
        public string RATE { get; set; }

        /// <summary>
        /// BASE AMOUNT
        /// </summary>
        [Required]
        public string BASEAMT { get; set; }

        /// <summary>
        /// PID 0
        /// </summary>
        [Required]
        public string PID { get; set; }

        /// <summary>
        /// TAX AMOUNT
        /// </summary>
        [Required]
        public string TAXAMT { get; set; }

        /// <summary>
        /// OPERATOR +
        /// </summary>
        [Required]
        public string OPERATOR { get; set; }
    }


    public class SORESPONSESI
    {
        /// <summary>
        /// 
        /// </summary>
        public string MESSAGE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SINO { get; set; }


    }

}